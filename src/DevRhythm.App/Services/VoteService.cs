using AutoMapper;
using DevRhythm.App.DTOs;
using DevRhythm.App.Interfaces;
using DevRhythm.App.Services.Base;
using DevRhythm.Core.Entities;
using DevRhythm.Core.Entities.Base;
using DevRhythm.Core.Enums;
using DevRhythm.Infrastructure.Data;
using DevRhythm.Shared.DTOs;
using DevRhythm.Shared.Enums;
using DevRhythm.Shared.Exceptions;

namespace DevRhythm.App.Services
{
    public class VoteService(DevRhythmDbContext context, IMapper mapper, INotificationService notificationService) : BaseService(context, mapper), IVoteService
    {
        private readonly INotificationService _notificationService = notificationService;

        public async Task UpdateVoteAsync(NewVoteDto newVoteDto)
        {
            switch (newVoteDto.VoteType)
            {
                case VoteType.PostVote:
                    await ManageVoteAsync<PostVote>(newVoteDto);
                    break;
                case VoteType.CommentVote:
                    await ManageVoteAsync<CommentVote>(newVoteDto);
                    break;
                case VoteType.ReplyVote:
                    await ManageVoteAsync<ReplyVote>(newVoteDto);
                    break;
            }

            var post = _context.Posts.FirstOrDefault(x => x.Id == newVoteDto.EntityId) ?? throw new NotFoundException(nameof(Post), newVoteDto.EntityId);

            var newNotificationDto = new NotificationDto
            {
                ReceiverId = post.CreatedBy,
                Message = "Your post was " + (newVoteDto.IsUpvote ? "upvoted" : "downvoted") + " by this user",
                SentAt = DateTime.UtcNow,
                NotificationType = NotificationType.VoteNotification,
                PostDto = _mapper.Map<PostNotificationDto>(post),
                Sender = _mapper.Map<UserNotificationDto>(_context.Users.FirstOrDefault(x => x.Id == newVoteDto.UserId))
            };

            _notificationService.SendNotification(newNotificationDto);
        }
        private async Task ManageVoteAsync<T>(NewVoteDto newVoteDto) where T : Vote
        {
            var existingVote = _context.Set<T>()
                    .Where(x => x.UserId == newVoteDto.UserId 
                        && x.EntityId == newVoteDto.EntityId)
                    .FirstOrDefault();

            var newVoteEntity = _mapper.Map<T>(newVoteDto);

            if (existingVote is null)
            {
                await _context.Set<T>().AddAsync(newVoteEntity);
            }
            else if (existingVote.IsUpvote == newVoteDto.IsUpvote)
            {
                _context.Set<T>().Remove(existingVote);
            }
            else
            {
                existingVote.IsUpvote = newVoteDto.IsUpvote;
            }

            await _context.SaveChangesAsync();
        }
    }
}
