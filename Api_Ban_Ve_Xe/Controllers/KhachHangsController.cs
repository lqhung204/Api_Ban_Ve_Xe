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
    public class KhachHangsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public KhachHangsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/KhachHangs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KhachHang>>> GetKhachHangs()
        {
            using (var dbContext = new AppDbContext())
            {
                // Lấy danh sách các sản phẩm từ cơ sở dữ liệu
                var products = dbContext.KhachHangs.ToList();

                // In ra danh sách các sản phẩm
                foreach (var product in products)
                {
                    Console.WriteLine($"Product: {product.TenKhachHang}");
                }
                return products;
            }
          //  if (_context.KhachHangs == null)
          //{
          //    return NotFound();
          //}
          //Console.WriteLine(_context.KhachHangs.FirstAsync().ToString());
          //  return await _context.KhachHangs.ToListAsync();
        }

        // GET: api/KhachHangs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KhachHang>> GetKhachHang(int id)
        {
            using (var _dbContext = new AppDbContext())
            {
                if (_dbContext.KhachHangs == null)
                {
                    return NotFound();
                }
                var khachHang = await _dbContext.KhachHangs.FindAsync(id);

                if (khachHang == null)
                {
                    return NotFound();
                }

                return khachHang;
            }
        }

        // PUT: api/KhachHangs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKhachHang(int id, KhachHang khachHang)
        {
            if (id != khachHang.MaKhachHang)
            {
                return BadRequest();
            }

            _context.Entry(khachHang).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KhachHangExists(id))
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

        // POST: api/KhachHangs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<KhachHang>> PostKhachHang(KhachHang khachHang)
        {
          if (_context.KhachHangs == null)
          {
              return Problem("Entity set 'AppDbContext.KhachHangs'  is null.");
          }
            _context.KhachHangs.Add(khachHang);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (KhachHangExists(khachHang.MaKhachHang))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetKhachHang", new { id = khachHang.MaKhachHang }, khachHang);
        }

        // DELETE: api/KhachHangs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKhachHang(int id)
        {
            if (_context.KhachHangs == null)
            {
                return NotFound();
            }
            var khachHang = await _context.KhachHangs.FindAsync(id);
            if (khachHang == null)
            {
                return NotFound();
            }

            _context.KhachHangs.Remove(khachHang);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KhachHangExists(int id)
        {
            return (_context.KhachHangs?.Any(e => e.MaKhachHang == id)).GetValueOrDefault();
        }
    }
}
