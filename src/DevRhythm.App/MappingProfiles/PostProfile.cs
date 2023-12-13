using AutoMapper;
using DevRhythm.App.DTOs;
using DevRhythm.Core.Entities;

namespace DevRhythm.App.MappingProfiles
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<Post, PostShortDto>();
            CreateMap<Post, PostFullDto>();
        } 
    }
}
