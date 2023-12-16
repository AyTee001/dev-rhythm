using AutoMapper;
using DevRhythm.App.DTOs;
using DevRhythm.Core.Entities;

namespace DevRhythm.App.MappingProfiles
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<Post, PostShortDto>()
                .ForMember(dest => dest.PreviewContent, opt => opt.MapFrom(e => e.Content.Length <= 250 ? e.Content : e.Content.Substring(0, 250)));
            CreateMap<Post, PostFullDto>();
        } 
    }
}
