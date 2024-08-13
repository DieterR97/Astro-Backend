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
    public class Authentication_logController : ControllerBase
    {
        private readonly AppDbContext _context;

        public Authentication_logController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Authentication_log
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Authentication_log>>> GetAuthentication_logs()
        {
            return await _context.Authentication_logs.ToListAsync();
        }

        // GET: api/Authentication_log/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Authentication_log>> GetAuthentication_log(int id)
        {
            var authentication_log = await _context.Authentication_logs.FindAsync(id);

            if (authentication_log == null)
            {
                return NotFound();
            }

            return authentication_log;
        }

        // PUT: api/Authentication_log/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthentication_log(int id, Authentication_log authentication_log)
        {
            if (id != authentication_log.log_id)
            {
                return BadRequest();
            }

            _context.Entry(authentication_log).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Authentication_logExists(id))
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

        // POST: api/Authentication_log
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Authentication_log>> PostAuthentication_log(Authentication_log authentication_log)
        {
            _context.Authentication_logs.Add(authentication_log);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuthentication_log", new { id = authentication_log.log_id }, authentication_log);
        }

        // DELETE: api/Authentication_log/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthentication_log(int id)
        {
            var authentication_log = await _context.Authentication_logs.FindAsync(id);
            if (authentication_log == null)
            {
                return NotFound();
            }

            _context.Authentication_logs.Remove(authentication_log);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Authentication_logExists(int id)
        {
            return _context.Authentication_logs.Any(e => e.log_id == id);
        }
    }
}
