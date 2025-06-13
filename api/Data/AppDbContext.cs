using HRRecruitingApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HRRecruitingApp.Data;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Candidate> Candidates => Set<Candidate>();
    public DbSet<CV> CVs => Set<CV>();
    public DbSet<Interview> Interviews => Set<Interview>();
    public DbSet<Note> Notes => Set<Note>();
    public DbSet<DailyRanking> DailyRankings => Set<DailyRanking>();
    public DbSet<DiscardReasonRef> DiscardReasonRefs => Set<DiscardReasonRef>();
    public DbSet<DiscardReason> DiscardReasons => Set<DiscardReason>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<DiscardReasonRef>().HasData(
            new DiscardReasonRef { Id = 1, Name = "Falta de experiencia" },
            new DiscardReasonRef { Id = 2, Name = "No encaja culturalmente" }
        );
    }
}
