using AutoMapper;
using DevRhythm.Core.Entities;
using DevRhythm.Shared.DTOs;

namespace DevRhythm.App.MappingProfiles
{
    public class VoteProfile : Profile
    {
        public VoteProfile() 
        {
            CreateMap<NewVoteDto, PostVote>();
            CreateMap<NewVoteDto, CommentVote>();
            CreateMap<NewVoteDto, ReplyVote>();
        }
    }
}
