using DevRhythm.App.DTOs;
using DevRhythm.App.Interfaces;
using DevRhythm.Web.DTOs;
using DevRhythm.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            var notificationDto = await _notificationService.GetNotificationsByUserIdAsync(userId);
            await _notificationService.MarkNotificationsAsReadAsync(userId);
            return View("Index", notificationDto);
        }

        public IActionResult SetUpCleaningJob([FromBody] NotificationCleaningDto notificationCleaningModel)
        {
            try
            {
                _notificationService.SetCleanAllNotificationByUserIdJob(notificationCleaningModel.UserId, notificationCleaningModel.NotificationCleaningPeriod);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }
    }
}
