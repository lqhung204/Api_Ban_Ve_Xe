using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api_Ban_Ve_Xe.Models;
using System.Data.Entity;
using EntityState = Microsoft.EntityFrameworkCore.EntityState;

namespace Api_Ban_Ve_Xe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaiXesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TaiXesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/TaiXes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaiXe>>> GetTaiXes()
        {
          if (_context.TaiXes == null)
          {
              return NotFound();
          }
            return await _context.TaiXes.ToListAsync();
        }

        // GET: api/TaiXes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaiXe>> GetTaiXe(int id)
        {
            using (var _context = new AppDbContext())
            {
                if (_context.TaiXes == null)
                {
                    return NotFound();
                }
                var taiXe = await _context.TaiXes.FindAsync(id);

                if (taiXe == null)
                {
                    return NotFound();
                }

                return taiXe;
            }
        }

        // PUT: api/TaiXes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaiXe(int id, TaiXe taiXe)
        {
            if (id != taiXe.MaTaiXe)
            {
                return BadRequest();
            }

            _context.Entry(taiXe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaiXeExists(id))
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

        // POST: api/TaiXes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TaiXe>> PostTaiXe(TaiXe taiXe)
        {
          if (_context.TaiXes == null)
          {
              return Problem("Entity set 'AppDbContext.TaiXes'  is null.");
          }
            _context.TaiXes.Add(taiXe);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TaiXeExists(taiXe.MaTaiXe))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTaiXe", new { id = taiXe.MaTaiXe }, taiXe);
        }

        // DELETE: api/TaiXes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaiXe(int id)
        {
            if (_context.TaiXes == null)
            {
                return NotFound();
            }
            var taiXe = await _context.TaiXes.FindAsync(id);
            if (taiXe == null)
            {
                return NotFound();
            }

            _context.TaiXes.Remove(taiXe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaiXeExists(int id)
        {
            return (_context.TaiXes?.Any(e => e.MaTaiXe == id)).GetValueOrDefault();
        }
    }
}
