using HRRecruitingApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace HRRecruitingApp.Controllers;

[ApiController]
[Route("api/metrics")]
public class MetricsController : ControllerBase
{
    private readonly IMetricsService _service;
    public MetricsController(IMetricsService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var metrics = await _service.GetMetricsAsync();
        return Ok(metrics);
    }
}
