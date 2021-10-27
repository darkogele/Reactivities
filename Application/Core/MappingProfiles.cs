using Application.Account.DTOs;
using AutoMapper;
using Domain;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Activity, Activity>();
            CreateMap<AppUser, UserDto>();
            CreateMap<RegisterDto, AppUser>();
        }
    }
}