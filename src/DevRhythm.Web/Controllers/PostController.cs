using DevRhythm.App.DTOs;
using DevRhythm.App.Interfaces;
using DevRhythm.Shared.Entities;
using DevRhythm.Shared.Settings;
using DevRhythm.Web.DTOs;
using DevRhythm.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static System.Net.WebRequestMethods;

namespace DevRhythm.Web.Controllers
{
    public class PostController(IPostService postService, ITagService tagService) : Controller
    {
        private readonly IPostService _postService = postService;
        private readonly ITagService _tagService = tagService;

        public async Task<IActionResult> Index()
        {
            return View(
                new MainPostPageModel
                {
                    Tags = await _tagService.GetTagsForFilterAsync()
                });
        }

        public async Task<IActionResult> InitPostsWithTags(ICollection<long> tagIds)
        {
            var tagDtos = await _tagService.GetTagsForFilterAsync();
            var checkedTags = tagDtos.Select(t =>
            {
                t.IsChecked = tagIds.Contains(t.Id);
                return t;
            });

            return View(nameof(Index),
                new MainPostPageModel
                {
                    Tags = checkedTags
                });
        }

        [Authorize]
        [HttpGet("{postId}")]
        public async Task<IActionResult> ShowPost([FromRoute]long postId)
        {
            return View("Post", await _postService.GetPostByIdAsync(postId));
        }

        [HttpPost]
        public async Task<ActionResult> GetPosts([FromBody] PageGetDto pageGetDto)
        {
            return PartialView("_PostCollectionPartial", await GetPostCollectionChunk(new PageSettings { PageNumber = pageGetDto.PageNumber, PageSize = pageGetDto.PostCount }, pageGetDto.SortSettings, pageGetDto.TagIds));
        }

        private async Task<IEnumerable<PostShortDto>> GetPostCollectionChunk(PageSettings? pageSettings, SortSettings? sortSettings, ICollection<long> tagids)
        {
            var posts = await _postService.GetPostPreviewsAsync(pageSettings, sortSettings, tagids);
            return posts;
        }

        [Authorize]
        public async Task<IActionResult> CreatePost()
        {
            return View(nameof(CreatePost), new PostCreateDto
                {
                    Tags = [.. (await _tagService.GetTagsForFilterAsync())]
                });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddNewPost(PostCreateDto postCreate)
        {
            await _postService.AddNewPostAsync(postCreate);

            return RedirectToAction("Index");

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
