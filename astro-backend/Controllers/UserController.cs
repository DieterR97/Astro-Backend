using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using astro_backend;
using astro_backend.models;

namespace astro_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.user_id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.user_id }, user);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.user_id == id);
        }

        [HttpGet("email")]
        public async Task<ActionResult<UserDto>> GetUserByEmail(string email)
        {
            var user = await _context.Users
                                      .Include(u => u.Account)
                                      .ThenInclude(a => a.Assets) // Including assets to calculate balance
                                      .Include(u => u.Account.Status)
                                      .Include(u => u.Account.TransactionsFrom)
                                      .Include(u => u.Account.TransactionsTo)
                                      .FirstOrDefaultAsync(u => u.email == email);

            if (user == null)
            {
                return NotFound();
            }

            // Calculate the total balance based on the assets
            user.Account.balance = user.Account.Assets.Sum(asset => asset.price * asset.amount);

            // Save the updated balance to the database
            _context.Accounts.Update(user.Account);
            await _context.SaveChangesAsync();

            // Return the user data as a DTO
            var userDto = new UserDto
            {
                Username = user.username,
                Email = user.email,
                Account = new AccountDto
                {
                    AccountId = user.Account.account_id,
                    Balance = user.Account.balance,
                    Active = user.Account.active,
                    Account_status_id = user.Account.account_status_id,
                    Status = user.Account.Status,
                    Assets = user.Account.Assets.Select(asset => new AssetDto
                    {
                        AssetId = asset.asset_id,
                        Name = asset.name,
                        Abbreviation = asset.abbreviation,
                        Price = asset.price,
                        Amount = asset.amount
                    }).ToList(),
                    TransactionsFrom = user.Account.TransactionsFrom.ToList(),
                    TransactionsTo = user.Account.TransactionsTo.ToList()
                }
            };

            return userDto;
        }




    }
}
