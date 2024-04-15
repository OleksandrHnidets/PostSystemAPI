using AutoMapper;
using PostSystemAPI.DAL.Models;

namespace PostSystemAPI.Domain.Profiles;

public class PositionProfile: Profile
{
    public PositionProfile()
    {
        CreateMap<Position, UpdateMarkerViewModel>()
            .ForMember(dest => dest.DriverId, opt => opt.MapFrom(src => src.UserId));
    }
}