using AutoMapper;
using HRRecruitingApp.DTOs;
using HRRecruitingApp.Models;

namespace HRRecruitingApp;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Candidate, CandidateDto>()
            .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.ToString()));
    }
}
