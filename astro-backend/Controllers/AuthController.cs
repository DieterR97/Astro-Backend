using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using astro_backend.Services;
using astro_backend.models;
using Isopoh.Cryptography.Argon2;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;

namespace astro_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //dependency injection
        private readonly EmailSender _emailSender;
        private readonly AppDbContext _context;

        private readonly IConfiguration _configuration;

        //constructor to initialise AuthController with needed dependencies
        public AuthController(AppDbContext context, EmailSender emailSender, IConfiguration configuration)
        {
            _context = context;
            _emailSender = emailSender;
            _configuration = configuration;
        }



        //TODO: test the sending of our email
        [HttpGet("test-email")]
        public async Task<IActionResult> TestEmail()
        {
            string toEmail = "21100366@virtualwindow.co.za";
            string subject = "Test Email";
            string body = "This is a test email to verify email sending functionality.";

            await _emailSender.SendEmailAsync(toEmail, body, subject);
            return Ok("Test email sent.");
        }


        //TODO: API to generate and save the OTP (send the email)
        [HttpPost("generate-otp")]
        public async Task<IActionResult> GenerateOtp([FromBody] string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest("Email is required");
            }

            var user = await _context.Users.SingleOrDefaultAsync(u => u.email == email);
            if (user == null) return NotFound("User not found");

            user.GenerateOTP();
            await _context.SaveChangesAsync();

            var otpMsg = $"Your OTP is {user.Otp}. It will expire in 5 min.";
            await _emailSender.SendEmailAsync(user.email, otpMsg, "One Time Pin for Astro");

            // return Ok("OTP Sent.");
            return Ok(new { message = "OTP Sent." });
        }


        // //TODO: API validate if the user has entered the correct OTP
        // [HttpPost("validate—otp")]
        // public async Task<IActionResult> ValidateOtp(OtpEmail otpEmail)
        // {
        //     //Check to make sure that this email exists as a user in our table
        //     var user = await _context.Users.SingleOrDefaultAsync(u => u.email == otpEmail.Email);
        //     if (user == null) return NotFound("User not found");

        //     if (user.ValidateOTP(otpEmail.Otp))
        //     {
        //         //valid otp
        //         // TODO self (optional): JWT - pass the token here
        //         return Ok("OTP is valid. Let me into the site.");
        //     }
        //     else
        //     {
        //         return BadRequest("Invalid OTP.");
        //     }

        // }


        private string GenerateJwtToken(User user)
        {
            // var jwtSettings = _configuration.GetSection("JwtSettings");

            var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
            // var jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");
            var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET");
            // var jwtExpiresInMinutes = int.Parse(Environment.GetEnvironmentVariable("JWT_EXPIRES_IN_MINUTES"));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.user_id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.email),
                new Claim(ClaimTypes.Role, user.role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                // audience: jwtSettings["Audience"],
                claims: claims,
                // expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["ExpiryInMinutes"])),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private bool VerifyPassword(string plainPassword, string hashedPassword)
        {
            try
            {
                return Argon2.Verify(hashedPassword, plainPassword);
            }
            catch
            {
                // Handle exceptions (e.g., invalid hash format)
                return false;
            }
        }


        [HttpPost("validate-otp")]
        public async Task<IActionResult> ValidateOtp(OtpEmail otpEmail)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.email == otpEmail.Email);
            if (user == null) return NotFound("User not found");

            if (user.ValidateOTP(otpEmail.Otp))
            {
                // Send welcome email
                await _emailSender.SendEmailAsync(user.email, "Welcome to Astro!", "Welcome email content...");

                // Log the user in (e.g., generate JWT)
                var token = GenerateJwtToken(user);

                return Ok(new { message = "OTP is valid. You are now logged in.", token });
            }
            else
            {
                return BadRequest("Invalid OTP.");
            }
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto dto)
        {
            try
            {
                // Check if the email or username already exists
                if (await _context.Users.AnyAsync(u => u.email == dto.Email || u.username == dto.Username))
                {
                    return BadRequest(new { message = "Email or username already taken" });
                }

                // Hash the password using Argon2
                var argon2 = new Argon2Hash(dto.Password);
                var hashedPassword = argon2.Hash();

                // Create user
                var user = new User
                {
                    username = dto.Username,
                    email = dto.Email,
                    role = "user",
                    created_at = DateTime.UtcNow
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                // Generate and send OTP
                user.GenerateOTP(); //Call the functionality and save it for the user that we found in the object
                await _context.SaveChangesAsync(); //sync the new data to our tables in the db

                // Create associated User_security record
                var userSecurity = new User_security
                {
                    user_id = user.user_id,
                    password_hash = hashedPassword,
                    latest_otp_secret = user.Otp,
                    updated_at = DateTime.UtcNow
                };

                _context.User_securitys.Add(userSecurity);
                await _context.SaveChangesAsync();

                // Create associated Account record
                var account = new Account
                {
                    user_id = user.user_id,
                    account_status_id = 1, // Default status for new accounts
                    balance = 0,
                    active = true
                };

                _context.Accounts.Add(account);
                await _context.SaveChangesAsync();

                // Generate and send OTP
                //// user.GenerateOTP(); //Call the functionality and save it for the user that we found in the object
                // await _context.SaveChangesAsync(); //sync the new data to our tables in the db
                await _emailSender.SendEmailAsync(user.email, $"Your OTP is {user.Otp}. It will expire in 5 min.", "OTP for Astro Registration"); //sending otp via email

                // return Ok("Registration successful. Please verify your OTP.");
                return Ok(new { message = "Registration successful. Please verify your OTP." });

                //encrypt otp before saving
                //TODO SELF: encrypt the OTP - Argon2 (own research)
            }
            catch (Exception ex)
            {
                // Log the exception if necessary
                return StatusCode(500, new { message = "An error occurred during registration. Please try again." });
            }
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto dto)
        {

            // Check the dto
            if (dto == null)
            {
                return BadRequest("Invalid request");
            }

            if (string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Password))
            {
                return BadRequest("Email and password are required");
            }


            // Check if user exists
            var user = await _context.Users.SingleOrDefaultAsync(u => u.email == dto.Email);
            if (user == null)
                return Unauthorized("Invalid credentials");

            // Get user security details
            var userSecurity = await _context.User_securitys.SingleOrDefaultAsync(us => us.user_id == user.user_id);
            if (userSecurity == null)
                return Unauthorized("Invalid credentials");

            // Verify password
            if (!VerifyPassword(dto.Password, userSecurity.password_hash))
                return Unauthorized("Invalid credentials");

            // Generate JWT token
            var token = GenerateJwtToken(user);

            // Log authentication details
            var authLog = new Authentication_log
            {
                login_time = DateTime.UtcNow,
                logout_time = DateTime.MinValue, // Assume not logged out yet
                ip_address = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown",
                device_info = HttpContext.Request.Headers["User-Agent"].ToString(),
                user_id = user.user_id
            };

            _context.Authentication_logs.Add(authLog);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Login successful", token });
        }


        public class OtpEmail
        {
            public string Email { get; set; }
            public string Otp { get; set; }
        }

    }

}