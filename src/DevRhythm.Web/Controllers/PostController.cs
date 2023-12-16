using DevRhythm.App.DTOs;
using DevRhythm.App.Interfaces;
using DevRhythm.Shared.Entities;
using DevRhythm.Shared.Settings;
using DevRhythm.Web.DTOs;
using DevRhythm.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DevRhythm.Web.Controllers
{
    public class PostController(IPostService postService) : Controller
    {
        private readonly IPostService _postService = postService;
        private const int _itemsOnPageCount = 10;

        public IActionResult Index()
        {
            return View(
                new MainPostPageModel
                {
                    SortSettings = null,
                });
        }
         
        [HttpPost]
        public async Task<ActionResult> GetPosts([FromBody] PageGetDto pageGetDto)
        {
            return PartialView("_PostCollectionPartial", await GetPostCollectionChunk(new PageSettings { PageNumber = pageGetDto.PageNumber, PageSize = _itemsOnPageCount }, null));
        }

        private async Task<ICollection<PostShortDto>> GetPostCollectionChunk(PageSettings? pageSettings, SortSettings? sortSettings)
        {
            var posts = await _postService.GetPostPreviewsAsync(pageSettings, sortSettings, []);
            return posts;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
