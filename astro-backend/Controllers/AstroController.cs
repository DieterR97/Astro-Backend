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
    public class AstroController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AstroController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Astro
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Astro>>> GetAstros()
        {
            return await _context.Astros.ToListAsync();
        }

        // GET: api/Astro/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Astro>> GetAstro(int id)
        {
            var astro = await _context.Astros.FindAsync(id);

            if (astro == null)
            {
                return NotFound();
            }

            return astro;
        }

        // PUT: api/Astro/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAstro(int id, Astro astro)
        {
            if (id != astro.astro_id)
            {
                return BadRequest();
            }

            _context.Entry(astro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AstroExists(id))
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

        // POST: api/Astro
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Astro>> PostAstro(Astro astro)
        {
            _context.Astros.Add(astro);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAstro", new { id = astro.astro_id }, astro);
        }

        // DELETE: api/Astro/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAstro(int id)
        {
            var astro = await _context.Astros.FindAsync(id);
            if (astro == null)
            {
                return NotFound();
            }

            _context.Astros.Remove(astro);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AstroExists(int id)
        {
            return _context.Astros.Any(e => e.astro_id == id);
        }
    }
}
