using HRRecruitingApp.DTOs;
using HRRecruitingApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRRecruitingApp.Controllers;

[ApiController]
[Route("api/cv")]
public class CvController : ControllerBase
{
    private readonly ICVService _service;

    public CvController(ICVService service)
    {
        _service = service;
    }

    [HttpPost("upload")]
    [Authorize]
    public async Task<ActionResult<CVUploadResultDto>> Upload([FromForm] IFormFile file)
    {
        if (file == null) return BadRequest();
        var result = await _service.UploadAsync(file);
        return Ok(result);
    }
}
