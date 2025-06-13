namespace HRRecruitingApp.Models;

public enum CandidateStatus
{
    New,
    InterviewScheduled,
    Interviewed,
    Finalist,
    Accepted,
    RejectedByCompany,
    RejectedByCandidate,
    Silver
}

public class Candidate
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? PhotoPath { get; set; }
    public CandidateStatus Status { get; set; } = CandidateStatus.New;
    public ICollection<CV> CVs { get; set; } = new List<CV>();
    public ICollection<Interview> Interviews { get; set; } = new List<Interview>();
}

public class CV
{
    public int Id { get; set; }
    public int CandidateId { get; set; }
    public Candidate Candidate { get; set; } = null!;
    public string FilePath { get; set; } = string.Empty;
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
}

public class Interview
{
    public int Id { get; set; }
    public int CandidateId { get; set; }
    public Candidate Candidate { get; set; } = null!;
    public DateTime DateTime { get; set; }
    public string Interviewer { get; set; } = string.Empty;
    public bool Completed { get; set; } = false;
    public ICollection<Note> Notes { get; set; } = new List<Note>();
}

public class Note
{
    public int Id { get; set; }
    public int CandidateId { get; set; }
    public int? InterviewId { get; set; }
    public string Author { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class DailyRanking
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public int CandidateId { get; set; }
    public int Rank { get; set; }
}

public class DiscardReasonRef
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class DiscardReason
{
    public int Id { get; set; }
    public int CandidateId { get; set; }
    public int? RefId { get; set; }
    public string? OtherText { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
