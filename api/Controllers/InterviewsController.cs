using HRRecruitingApp.Data;
using HRRecruitingApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRRecruitingApp.Controllers;

[ApiController]
[Route("api/interviews")]
[Authorize]
public class InterviewsController : ControllerBase
{
    private readonly AppDbContext _db;

    public InterviewsController(AppDbContext db)
    {
        _db = db;
    }

    [HttpPost("{candidateId}")]
    public async Task<IActionResult> Create(int candidateId, [FromBody] Interview interview)
    {
        interview.CandidateId = candidateId;
        _db.Interviews.Add(interview);
        await _db.SaveChangesAsync();
        return CreatedAtAction(null, new { id = interview.Id });
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Interview>>> List([FromQuery] DateTime? date)
    {
        var query = _db.Interviews.AsQueryable();
        if (date.HasValue)
        {
            query = query.Where(i => i.DateTime.Date == date.Value.Date);
        }
        return Ok(await query.Include(i => i.Candidate).ToListAsync());
    }

    [HttpPut("{id}/complete")]
    public async Task<IActionResult> Complete(int id)
    {
        var interview = await _db.Interviews.FindAsync(id);
        if (interview == null) return NotFound();
        interview.Completed = true;
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpPost("{id}/notes")]
    public async Task<IActionResult> AddNote(int id, [FromBody] Note note)
    {
        note.InterviewId = id;
        _db.Notes.Add(note);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
