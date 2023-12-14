using DevRhythm.App.Interfaces;
using DevRhythm.Shared.Entities;
using DevRhythm.Shared.Settings;
using DevRhythm.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DevRhythm.Web.Controllers
{
    public class PostController(IPostService postService) : Controller
    {
        private readonly IPostService _postService = postService;

        public async Task<IActionResult> Index()
        {
            return View(
                new MainPostPageModel
                {
                    PostPreviewsCollection = await _postService.GetPostPreviewsAsync(new PageSettings() { PageNumber = 1, PageSize = 10 }, null, []),
                    SortSettings = new SortSettings()
                });
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
