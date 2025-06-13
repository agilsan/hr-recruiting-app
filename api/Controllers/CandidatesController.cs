using AutoMapper;
using HRRecruitingApp.Data;
using HRRecruitingApp.DTOs;
using HRRecruitingApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRRecruitingApp.Controllers;

[ApiController]
[Route("api/candidates")]
[Authorize]
public class CandidatesController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public CandidatesController(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CandidateDto>> Get(int id)
    {
        var candidate = await _db.Candidates.Include(c => c.CVs)
            .FirstOrDefaultAsync(c => c.Id == id);
        if (candidate == null) return NotFound();
        return Ok(_mapper.Map<CandidateDto>(candidate));
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] CandidateStatusDto dto)
    {
        var candidate = await _db.Candidates.FindAsync(id);
        if (candidate == null) return NotFound();
        if (!Enum.TryParse<CandidateStatus>(dto.Status, out var status)) return BadRequest();
        candidate.Status = status;
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpGet("silver")]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<CandidateDto>>> GetSilver()
    {
        var silver = await _db.Candidates.Where(c => c.Status == CandidateStatus.Silver).ToListAsync();
        return Ok(_mapper.Map<IEnumerable<CandidateDto>>(silver));
    }
}
