using AutoMapper;
using Azure;
using DevRhythm.App.DTOs;
using DevRhythm.App.Interfaces;
using DevRhythm.App.Services.Base;
using DevRhythm.Core.Entities;
using DevRhythm.Infrastructure.Data;
using DevRhythm.Shared.Entities;
using DevRhythm.Shared.Enums;
using DevRhythm.Shared.Exceptions;
using DevRhythm.Shared.Settings;
using Microsoft.EntityFrameworkCore;

namespace DevRhythm.App.Services
{
    public class PostService(DevRhythmDbContext context, IMapper mapper) 
        : BaseService(context, mapper), 
        IPostService
    {
        public async Task<PostFullDto> GetPostByIdAsync(long id)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.Id == id) 
                ?? throw new NotFoundException(nameof(Post), id);

            return _mapper.Map<PostFullDto>(post);
        }

        public async Task<ICollection<PostShortDto>> GetPostPreviewsAsync(PageSettings? pageSettings, SortSettings? sortSettings, ICollection<long> tagIds)
        {
            var posts = _context.Posts
                .Include(p => p.Author)
                .Include(p => p.Tags)
                .AsNoTracking();

            if (tagIds.Count != 0)
            {
                posts = posts.Where(p => tagIds.Intersect(p.Tags.Select(t => t.Id)).Any());
            }

            if(sortSettings is not null)
            {
                posts = OrderPosts(posts, sortSettings);
            }

            if (pageSettings is not null && pageSettings.PageSize > 0 && pageSettings.PageNumber > 0)
            {
                posts = posts.Skip(pageSettings.PageSize * (pageSettings.PageNumber - 1))
                .Take(pageSettings.PageSize);
            }

                return _mapper.Map<List<PostShortDto>>(await posts.ToListAsync());
        }

        private static IOrderedQueryable<Post> OrderPosts(IQueryable<Post> posts, SortSettings sortSettings)
        {
            return sortSettings switch
            {
                { SortOrder: SortOrder.Ascending, SortProperty: SortProperty.VoteResult } => posts.OrderBy(e => e.VoteResult),
                { SortOrder: SortOrder.Ascending, SortProperty: SortProperty.CreatedAt } => posts.OrderBy(e => e.CreatedAt),
                { SortOrder: SortOrder.Ascending, SortProperty: SortProperty.Title } => posts.OrderBy(e => e.Heading),
                { SortOrder: SortOrder.Descending, SortProperty: SortProperty.VoteResult } => posts.OrderByDescending(e => e.VoteResult),
                { SortOrder: SortOrder.Descending, SortProperty: SortProperty.CreatedAt } => posts.OrderByDescending(e => e.CreatedAt),
                { SortOrder: SortOrder.Descending, SortProperty: SortProperty.Title } => posts.OrderByDescending(e => e.Heading),
                _ => throw new NotFoundException(nameof(SortSettings))
            };
        } 
    }
}
