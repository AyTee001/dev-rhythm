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
            var model = new UserStatusComponentModel
            {
                IsUserAuthenticated = isUserSignedIn,
                UserUnreadNotificationCount = isUserSignedIn ?  await _notificationService.CountUnreadNotificationsForUserAsync(long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!)) : 0
            };

            return View(model);
        }
    }
}
