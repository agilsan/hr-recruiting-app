using HRRecruitingApp.Data;
using HRRecruitingApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRRecruitingApp.Controllers;

[ApiController]
[Route("api/rankings")]
[Authorize]
public class RankingsController : ControllerBase
{
    private readonly AppDbContext _db;

    public RankingsController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet("{date}")]
    public async Task<ActionResult<IEnumerable<DailyRanking>>> Get(DateOnly date)
    {
        var rankings = await _db.DailyRankings.Where(r => r.Date == date)
            .OrderBy(r => r.Rank).ToListAsync();
        return Ok(rankings);
    }

    [HttpPut("{date}")]
    public async Task<IActionResult> Update(DateOnly date, [FromBody] List<DailyRanking> list)
    {
        var existing = _db.DailyRankings.Where(r => r.Date == date);
        _db.DailyRankings.RemoveRange(existing);
        await _db.DailyRankings.AddRangeAsync(list);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
