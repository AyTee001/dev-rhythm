using AutoMapper;
using DevRhythm.App.DTOs;
using DevRhythm.App.Interfaces;
using DevRhythm.App.Services.Base;
using DevRhythm.App.Services.Helpers;
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
                    if (await AddVoteAsync<PostVote>(newVoteDto))
                    {
                        await IssuePostVoteNotificationAsync(newVoteDto);
                    };
                    break;
                case VoteType.CommentVote:
                    await AddVoteAsync<CommentVote>(newVoteDto);
                    break;
                case VoteType.ReplyVote:
                    await AddVoteAsync<ReplyVote>(newVoteDto);
                    break;
            }

        }
        private async Task<bool> AddVoteAsync<T>(NewVoteDto newVoteDto) where T : Vote
        {
            bool hasNewVoteBeenAdded = true;

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
                hasNewVoteBeenAdded = false;
            }
            else
            {
                existingVote.IsUpvote = newVoteDto.IsUpvote;
            }
            await _context.SaveChangesAsync();

            return hasNewVoteBeenAdded;
        }

        private async Task IssuePostVoteNotificationAsync(NewVoteDto newVoteDto)
        {
            var post = _context.Posts.FirstOrDefault(x => x.Id == newVoteDto.EntityId) 
                ?? throw new NotFoundException(nameof(Post), newVoteDto.EntityId);

            var newNotificationDto = new NotificationDto
            {
                ReceiverId = post.CreatedBy,
                Message = NotificationMessageHelper.GetVoteNotificationMessage(newVoteDto.IsUpvote, newVoteDto.VoteType),
                SentAt = DateTime.UtcNow,
                NotificationType = NotificationType.VoteNotification,
                PostDto = _mapper.Map<PostNotificationDto>(post),
                Sender = _mapper.Map<UserNotificationDto>(_context.Users.FirstOrDefault(x => x.Id == newVoteDto.UserId))
            };

            await _notificationService.AddNotificationToStorageAsync(newNotificationDto);
            _notificationService.SendNotification(newNotificationDto);

        }
    }
}
