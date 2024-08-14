using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace astro_backend.models;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int user_id { get; set; }

    [Required]
    public string username { get; set; }

    [Required]
    [EmailAddress] // Helps ensure valid email format
    public string email { get; set; }

    [Required]
    public string role { get; set; }

    public DateTime created_at { get; set; }



        // OTP
    public string? Otp { get; set; }

    public DateTime? OtpExpiry { get; set; }

    //method to control our otp
    public void GenerateOTP()
    {
        //6 digits otp
        var random = new Random();
        Otp = random.Next(100000, 999999).ToString(); // generate 6-digit otp
        //TODO SELF: encrypt the OTP - Argon2 (own research)
        OtpExpiry = DateTime.UtcNow.AddMinutes(5); // 5 minutes expiry
    }

    public bool ValidateOTP(string receivedOTP)
    {
        // check if the received OTP/emailed is valid/match, and are we still in the 5 minute expiry window
        return Otp == receivedOTP && OtpExpiry > DateTime.UtcNow;

        // TODO self (optional): generate the JWT token

    }


    public ICollection<Authentication_log>? Authentication_logs { get; set; }
    public User_security? User_security { get; set; }
    public Account? Account { get; set; }


}
