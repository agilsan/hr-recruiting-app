using HRRecruitingApp.DTOs;

namespace HRRecruitingApp.Services;

public interface ICVService
{
    Task<CVUploadResultDto> UploadAsync(IFormFile file);
}
