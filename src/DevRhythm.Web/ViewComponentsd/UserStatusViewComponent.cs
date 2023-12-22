using DevRhythm.Core.Entities;
using DevRhythm.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DevRhythm.Web.ViewComponents
{
    public class UserStatusViewComponent(SignInManager<User> signInManager) : ViewComponent
    {
        private readonly SignInManager<User> _signInManager = signInManager;

        public IViewComponentResult Invoke()
        {
            var model = new UserStatusComponentModel
            {
                IsUserAuthenticated = _signInManager.IsSignedIn(HttpContext.User),
            };

            return View(model);
        }
    }
}
