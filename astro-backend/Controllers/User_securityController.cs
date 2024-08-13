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
    public class User_securityController : ControllerBase
    {
        private readonly AppDbContext _context;

        public User_securityController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/User_security
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User_security>>> GetUser_securitys()
        {
            return await _context.User_securitys.ToListAsync();
        }

        // GET: api/User_security/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User_security>> GetUser_security(int id)
        {
            var user_security = await _context.User_securitys.FindAsync(id);

            if (user_security == null)
            {
                return NotFound();
            }

            return user_security;
        }

        // PUT: api/User_security/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser_security(int id, User_security user_security)
        {
            if (id != user_security.security_id)
            {
                return BadRequest();
            }

            _context.Entry(user_security).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!User_securityExists(id))
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

        // POST: api/User_security
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User_security>> PostUser_security(User_security user_security)
        {
            _context.User_securitys.Add(user_security);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser_security", new { id = user_security.security_id }, user_security);
        }

        // DELETE: api/User_security/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser_security(int id)
        {
            var user_security = await _context.User_securitys.FindAsync(id);
            if (user_security == null)
            {
                return NotFound();
            }

            _context.User_securitys.Remove(user_security);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool User_securityExists(int id)
        {
            return _context.User_securitys.Any(e => e.security_id == id);
        }
    }
}
