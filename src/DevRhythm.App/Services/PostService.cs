using AutoMapper;
using DevRhythm.App.DTOs;
using DevRhythm.App.Interfaces;
using DevRhythm.App.Services.Base;
using DevRhythm.Core.Entities;
using DevRhythm.Infrastructure.Data;
using DevRhythm.Shared.Entities;
using DevRhythm.Shared.Enums;
using DevRhythm.Shared.Exceptions;
using DevRhythm.Shared.Interfaces;
using DevRhythm.Shared.Settings;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DevRhythm.App.Services
{
    public class PostService(DevRhythmDbContext context, IMapper mapper, IUserInfoProvider userInfoProvider) 
        : BaseService(context, mapper), 
        IPostService
    {
        private readonly IUserInfoProvider _userInfo = userInfoProvider;
        public async Task<PostFullDto> GetPostByIdAsync(long id)
        {
            var post = await _context.Posts
                .Include(p => p.Tags)
                .Include(p => p.Author)
                .Include(p => p.Comments)
                    .ThenInclude(c =>c.Replies)
                        .ThenInclude(a => a.Author)
                .Include(c => c.Comments)
                    .ThenInclude(c => c.Author)
                .AsSplitQuery()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new NotFoundException(nameof(Post), id);

            return _mapper.Map<PostFullDto>(post);
        }

        public async Task<IReadOnlyList<PostShortDto>> GetPostPreviewsAsync(PageSettings? pageSettings, SortSettings? sortSettings, ICollection<long> tagIds)
        {
            var posts = _context.Posts
                .Include(p => p.Author)
                .Include(p => p.Tags)
                .AsNoTracking();

            if (tagIds.Count != 0)
            {
                posts = posts.Where(p => tagIds.Intersect(p.Tags.Select(t => t.Id)).Any());
            }

            if (sortSettings is not null && sortSettings.SortProperty != SortProperty.None)
            {
                posts = OrderPosts(posts, sortSettings);
            }

            if (pageSettings is not null && pageSettings.PageSize > 0 && pageSettings.PageNumber > 0)
            {
                posts = posts.Skip(pageSettings.PageSize * (pageSettings.PageNumber - 1))
                .Take(pageSettings.PageSize);
            }

            var postsWithVotes = posts 
                .GroupJoin(_context.PostVotes,
                    p => p.Id, pv => pv.EntityId,
                    (p, pv) => new { Post = p, Votes = pv })
                .Select(x => new { x.Post, CurrentUserVote = x.Votes.SingleOrDefault(x => x.UserId == _userInfo.Id) });


            var postList = await postsWithVotes.ToListAsync();

            return postList.Select(x =>
            {
                var postDto = _mapper.Map<PostShortDto>(x.Post);
                postDto.HasUserUpvoted = x.CurrentUserVote is not null && x.CurrentUserVote.IsUpvote;
                postDto.HasUserDownvoted = x.CurrentUserVote is not null && !x.CurrentUserVote.IsUpvote;
                return postDto;
            }).ToList().AsReadOnly();
        }

        private static IOrderedQueryable<Post> OrderPosts(IQueryable<Post> posts, SortSettings sortSettings)
        {
            return sortSettings.SortOrder switch
            {
                SortOrder.Ascending => posts.OrderBy(CreatePostPropSelector(sortSettings.SortProperty)),
                SortOrder.Descending => posts.OrderByDescending(CreatePostPropSelector(sortSettings.SortProperty)),
                _ => throw new NotFoundException(nameof(SortSettings))
            };
        }

        private static Expression<Func<Post, object>> CreatePostPropSelector(SortProperty property)
        {
            var parameter = Expression.Parameter(typeof(Post), "x");
            Expression propertyExpression = property switch
            {
                SortProperty.CreatedAt => Expression.Property(parameter, nameof(Post.CreatedAt)),
                SortProperty.VoteResult => Expression.Property(parameter, nameof(Post.VoteResult)),
                _ => throw new ArgumentException($"Unsupported property: {property}"),
            };

            var convertedExpression = Expression.Convert(propertyExpression, typeof(object));

            return Expression.Lambda<Func<Post, object>>(convertedExpression, parameter);
        }
    }
}
