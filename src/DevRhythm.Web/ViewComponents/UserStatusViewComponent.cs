using DevRhythm.App.Interfaces;
using DevRhythm.Core.Entities;
using DevRhythm.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DevRhythm.Web.ViewComponents
{
    public class UserStatusViewComponent(SignInManager<User> signInManager, INotificationService notificationService) : ViewComponent
    {
        private readonly SignInManager<User> _signInManager = signInManager;
        private readonly INotificationService _notificationService = notificationService;

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var isUserSignedIn = _signInManager.IsSignedIn(HttpContext.User);

            if (!isUserSignedIn)
            {
                return View(new UserStatusComponentModel { IsUserAuthenticated = false });
            }

            if (!long.TryParse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out long userId))
            {
                return View(new UserStatusComponentModel { IsUserAuthenticated = true });
            }

            var model = new UserStatusComponentModel
            {
                IsUserAuthenticated = true,
                UserUnreadNotificationCount = await _notificationService.CountUnreadNotificationsForUserAsync(userId),
                CurrentUserId = userId
            };

            return View(model);
        }
    }
}
