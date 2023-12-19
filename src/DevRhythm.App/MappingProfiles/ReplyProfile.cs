using AutoMapper;
using DevRhythm.App.DTOs;
using DevRhythm.Core.Entities;

namespace DevRhythm.App.MappingProfiles
{
    public class ReplyProfile : Profile
    {
        public ReplyProfile() 
        {
            CreateMap<Reply, ReplyDto>();
        }
    }
}
