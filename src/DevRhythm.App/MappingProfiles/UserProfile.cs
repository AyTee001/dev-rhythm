using AutoMapper;
using DevRhythm.App.DTOs;
using DevRhythm.Core.Entities;

namespace DevRhythm.App.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        { 
            CreateMap<User, UserShortDto>();
        }
    }
}
