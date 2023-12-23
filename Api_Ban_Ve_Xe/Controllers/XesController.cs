using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api_Ban_Ve_Xe.Models;

namespace Api_Ban_Ve_Xe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class XesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public XesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Xes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Xe>>> GetXes()
        {
          if (_context.Xes == null)
          {
              return NotFound();
          }
            return await _context.Xes.ToListAsync();
        }

        // GET: api/Xes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Xe>> GetXe(int id)
        {
            using (var _dbContext = new AppDbContext())
            {
                if (_dbContext.Xes == null)
                {
                    return NotFound();
                }
                var xe = await _dbContext.Xes.FindAsync(id);

                if (xe == null)
                {
                    return NotFound();
                }

                return xe;
            }
        }

        // PUT: api/Xes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutXe(int id, Xe xe)
        {
            if (id != xe.MaXe)
            {
                return BadRequest();
            }

            _context.Entry(xe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!XeExists(id))
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

        // POST: api/Xes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Xe>> PostXe(Xe xe)
        {
          if (_context.Xes == null)
          {
              return Problem("Entity set 'AppDbContext.Xes'  is null.");
          }
            _context.Xes.Add(xe);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (XeExists(xe.MaXe))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetXe", new { id = xe.MaXe }, xe);
        }

        // DELETE: api/Xes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteXe(int id)
        {
            if (_context.Xes == null)
            {
                return NotFound();
            }
            var xe = await _context.Xes.FindAsync(id);
            if (xe == null)
            {
                return NotFound();
            }

            _context.Xes.Remove(xe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool XeExists(int id)
        {
            return (_context.Xes?.Any(e => e.MaXe == id)).GetValueOrDefault();
        }
    }
}
