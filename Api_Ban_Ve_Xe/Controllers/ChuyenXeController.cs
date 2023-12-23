using Api_Ban_Ve_Xe.Models;
using Api_Ban_Ve_Xe.ModelsView;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ChuyenXeController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ChuyenXeViewModel>>> GetChuyenXes()
    {
        using (var dbContext = new AppDbContext())
        {
            var chuyenXes = await dbContext.ChuyenXes
                .Include(c => c.MaTuyenNavigation)
                .Select(c => new ChuyenXeViewModel
                {
                    MaChuyenXe = c.MaChuyenXe,
                    TenChuyenXe = c.TenChuyenXe,
                    GioDi = c.GioDi,
                    GioDen = c.GioDen,
                    ChoTrong = c.ChoTrong,
                    DiemDi = c.MaTuyenNavigation.DiemDi,
                    DiemDen = c.MaTuyenNavigation.DiemDen,
                    BangGia = c.MaTuyenNavigation.BangGia,
                    NgayDi = c.GetNgayDi()
                })
                .ToListAsync();

            return chuyenXes;
        }
    }

    [HttpGet("search/dep")]
    public async Task<ActionResult<IEnumerable<ChuyenXeViewModel>>> SearchChuyenXeByDeparture(string departure)
    {
        using (var dbContext = new AppDbContext())
        {
            var chuyenXes = await dbContext.ChuyenXes
                .Include(c => c.MaTuyenNavigation)
                .Where(c => c.MaTuyenNavigation.DiemDi.ToLower().Contains(departure.ToLower()))
                .Select(c => new ChuyenXeViewModel
                {
                    MaChuyenXe = c.MaChuyenXe,
                    TenChuyenXe = c.TenChuyenXe,
                    GioDi = c.GioDi,
                    GioDen = c.GioDen,
                    ChoTrong = c.ChoTrong,
                    DiemDi = c.MaTuyenNavigation.DiemDi,
                    DiemDen = c.MaTuyenNavigation.DiemDen,
                    BangGia = c.MaTuyenNavigation.BangGia,
                    NgayDi = c.GetNgayDi()
                })
                .ToListAsync();

            return chuyenXes;
        }
    }

    [HttpGet("search/depdes")]
    public async Task<ActionResult<IEnumerable<ChuyenXeViewModel>>> SearchChuyenXeByDestinationDeparture(string departure, string destination)
    {
        using (var dbContext = new AppDbContext())
        {
            var chuyenXes = await dbContext.ChuyenXes
                .Include(c => c.MaTuyenNavigation)
                .Where(c => c.MaTuyenNavigation.DiemDen.ToLower().Contains(destination.ToLower()) &&
                            c.MaTuyenNavigation.DiemDi.ToLower().Contains(departure.ToLower()))
                .Select(c => new ChuyenXeViewModel
                {
                    MaChuyenXe = c.MaChuyenXe,
                    TenChuyenXe = c.TenChuyenXe,
                    GioDi = c.GioDi,
                    GioDen = c.GioDen,
                    ChoTrong = c.ChoTrong,
                    DiemDi = c.MaTuyenNavigation.DiemDi,
                    DiemDen = c.MaTuyenNavigation.DiemDen,
                    BangGia = c.MaTuyenNavigation.BangGia,
                    NgayDi = c.GetNgayDi()
                })
                .ToListAsync();

            return chuyenXes;
        }
    }

    [HttpGet("search/depdesdate")]
    public async Task<ActionResult<IEnumerable<ChuyenXeViewModel>>> SearchChuyenXeByDestinationDepartureDate(string departure, string destination, DateTime departureDate)
    {
        using (var dbContext = new AppDbContext())
        {
            var chuyenXes = await dbContext.ChuyenXes
            .Include(c => c.MaTuyenNavigation)
            .Where(c => c.MaTuyenNavigation.DiemDen.ToLower().Contains(destination.ToLower()) &&
            c.MaTuyenNavigation.DiemDi.ToLower().Contains(departure.ToLower()) &&
            c.NgayDi.Date == departureDate.Date)
            .Select(c => new ChuyenXeViewModel
            {
                MaChuyenXe = c.MaChuyenXe,
                TenChuyenXe = c.TenChuyenXe ?? string.Empty,
                GioDi = c.GioDi,
                GioDen = c.GioDen,
                ChoTrong = c.ChoTrong,
                DiemDi = c.MaTuyenNavigation.DiemDi ?? string.Empty,
                DiemDen = c.MaTuyenNavigation.DiemDen ?? string.Empty,
                BangGia = c.MaTuyenNavigation.BangGia,
                NgayDi = c.GetNgayDi()
            })
            .ToListAsync();

            return chuyenXes;
        }
    }

    [HttpGet("search/id")]
    public async Task<ActionResult<IEnumerable<ChuyenXeViewModel>>> SearchChuyenXeById(int departure)
    {
        using (var dbContext = new AppDbContext())
        {
            var chuyenXes = await dbContext.ChuyenXes
                .Include(c => c.MaTuyenNavigation)
                .Where(c => c.MaChuyenXe ==departure)
                .Select(c => new ChuyenXeViewModel
                {
                    MaChuyenXe = c.MaChuyenXe,
                    TenChuyenXe = c.TenChuyenXe,
                    GioDi = c.GioDi,
                    GioDen = c.GioDen,
                    ChoTrong = c.ChoTrong,
                    DiemDi = c.MaTuyenNavigation.DiemDi,
                    DiemDen = c.MaTuyenNavigation.DiemDen,
                    BangGia = c.MaTuyenNavigation.BangGia,
                    NgayDi = c.GetNgayDi()
                })
                .ToListAsync();

            return chuyenXes;
        }
    }
}