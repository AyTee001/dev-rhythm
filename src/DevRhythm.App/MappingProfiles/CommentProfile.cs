using AutoMapper;
using DevRhythm.App.DTOs;
using DevRhythm.Core.Entities;

namespace DevRhythm.App.MappingProfiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile() 
        { 
            CreateMap<Comment, CommentDto>();
        }
    }
}
