using AutoMapper;
using BugBountyRadar.Api.Domain.Entities;

namespace BugBountyRadar.Api.Infrastructure.External.HackerOne;

public class HackerOneProfile : Profile
{
    public HackerOneProfile()
    {
        CreateMap<HackerOneProgramWrapper, BountyProgram>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))

            .ForMember(d => d.Name,      o => o.MapFrom(s => s.Attributes.Name))
            .ForMember(d => d.MinReward, o => o.MapFrom(s => s.Attributes.MinBounty))
            .ForMember(d => d.Platform,  o => o.MapFrom(_ => "HackerOne"))
            .ForMember(d => d.Technologies,
                o => o.MapFrom(s => s.Attributes.Technology ?? Array.Empty<string>()));
    }
}
