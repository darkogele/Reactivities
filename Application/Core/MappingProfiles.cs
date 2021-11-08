using Application.Account.DTOs;
using Application.Activities.DTOs;
using AutoMapper;
using Domain;
using System.Linq;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Activity, Activity>();
            CreateMap<AppUser, UserDto>();
            CreateMap<RegisterDto, AppUser>();

            CreateMap<Activity, ActivityDto>()
                .ForMember(d => d.HostUserName,
                    o => o.MapFrom(s => s.Attendees.FirstOrDefault(x => x.IsHost).AppUser.UserName));

            CreateMap<ActivityAttendee, Profiles.Profile>()
                .ForMember(d => d.DisplayName, o => o.MapFrom(s => s.AppUser.DisplayName))
                .ForMember(d => d.Bio, o => o.MapFrom(s => s.AppUser.Bio))
                .ForMember(d => d.UserName, o => o.MapFrom(s => s.AppUser.UserName));
        }
    }
}