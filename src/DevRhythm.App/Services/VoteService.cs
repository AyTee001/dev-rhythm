using AutoMapper;
using DevRhythm.App.Interfaces;
using DevRhythm.App.Services.Base;
using DevRhythm.Core.Entities;
using DevRhythm.Core.Entities.Base;
using DevRhythm.Infrastructure.Data;
using DevRhythm.Shared.DTOs;
using DevRhythm.Shared.Enums;

namespace DevRhythm.App.Services
{
    public class VoteService(DevRhythmDbContext context, IMapper mapper) : BaseService(context, mapper), IVoteService
    {

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
