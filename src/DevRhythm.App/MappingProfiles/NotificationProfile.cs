using AutoMapper;
using DevRhythm.App.DTOs;
using DevRhythm.Core.Entities;

namespace DevRhythm.App.MappingProfiles
{
    public class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            CreateMap<NotificationDto, Notification>()
                .ForMember(dest => dest.SenderId, opt => opt.MapFrom(e => e.Sender!.Id))
                .ForMember(dest => dest.Post, opt => opt.Ignore())
                .ForMember(dest => dest.PostId, opt => opt.MapFrom(x => x.PostDto!.Id))
                .ForMember(dest => dest.Sender, opt => opt.Ignore());

        }
    }
}
