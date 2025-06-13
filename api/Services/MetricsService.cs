using HRRecruitingApp.Data;
using Microsoft.EntityFrameworkCore;

namespace HRRecruitingApp.Services;

public class MetricsService : IMetricsService
{
    private readonly AppDbContext _db;

    public MetricsService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<object> GetMetricsAsync()
    {
        // TODO: compute real metrics
        return new
        {
            averageHireDays = 0,
            cvSources = new { email = 0, teams = 0 },
            counts = new { interviewed = 0, accepted = 0, discarded = 0 }
        };
    }
}
