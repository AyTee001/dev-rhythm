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
            CreateMap<Post, PostNotificationDto>();
            CreateMap<PostCreateDto, Post>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.VoteResult, opt => opt.Ignore())
                .ForMember(dest => dest.Author, opt => opt.Ignore())
                .ForMember(dest => dest.Comments, opt => opt.Ignore())
                .ForMember(dest => dest.Tags, opt => opt.Ignore());
        } 
    }
}
