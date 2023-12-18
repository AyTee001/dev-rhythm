using AutoMapper;
using DevRhythm.App.DTOs;
using DevRhythm.Core.Entities;

namespace DevRhythm.App.MappingProfiles
{
    public class TagProfile : Profile
    {
        public TagProfile() 
        { 
            CreateMap<Tag, TagShortDto>();
        }
    }
}
