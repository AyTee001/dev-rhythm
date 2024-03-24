using AutoMapper;
using DevRhythm.App.DTOs;
using DevRhythm.App.Interfaces;
using DevRhythm.App.Services.Base;
using DevRhythm.Core.Entities;
using DevRhythm.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DevRhythm.App.Services
{
    public class TagService(DevRhythmDbContext context, IMapper mapper) : BaseService(context, mapper), ITagService
    {
        public async Task<ICollection<TagFilterDto>> GetTagsForFilterAsync()
        {
            return await _context.Tags.AsNoTracking()
                         .Select(tag => _mapper.Map<TagFilterDto>(tag))
                         .ToListAsync();
        }

        public async Task AddPostTagsAsync(IEnumerable<long> tagIds, long postId)
        {
            var postTags = tagIds.Select(x => new PostTag { PostId = postId, TagId = x });
            await _context.PostTags.AddRangeAsync(postTags);
        }
    }
}
