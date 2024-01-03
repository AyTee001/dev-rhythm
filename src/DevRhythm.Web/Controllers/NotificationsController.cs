using DevRhythm.App.DTOs;
using DevRhythm.App.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevRhythm.Web.Controllers
{
    public class NotificationsController(INotificationService notificationService) : Controller
    {
        private readonly INotificationService _notificationService = notificationService;

        [HttpPost]
        public IActionResult RenderToastNotification([FromBody] NotificationDto notification)
        {
            return PartialView("_NotificationToastPartial", notification);
        }

        [HttpPost]
        public IActionResult RenderNotification([FromBody] NotificationDto notification)
        {
            return PartialView("_NotificationPartial", notification);
        }

        [HttpGet("all/{userId}")]
        public async Task<IActionResult> ShowNotifications([FromRoute]long userId)
        {
            var notifications = await _notificationService.GetNotificationsByUserIdAsync(userId);
            await _notificationService.MarkNotificationsAsReadAsync(userId);
            return View("Index", notifications);
        }
    }
}
