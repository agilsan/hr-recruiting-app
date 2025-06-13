namespace HRRecruitingApp.DTOs;

public record CandidateDto(int Id, string Name, string? Email, string? PhotoPath, string Status);

public record CandidateStatusDto(string Status);
