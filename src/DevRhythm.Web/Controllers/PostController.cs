using DevRhythm.App.Interfaces;
using DevRhythm.Shared.Settings;
using DevRhythm.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DevRhythm.Web.Controllers
{
    public class PostController(IPostService postService) : Controller
    {
        private readonly IPostService _postService = postService;

        public IActionResult Index()
        {
            return View(
                new MainPostPageModel
                {
                    PostPreviewsCollection = [],
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
