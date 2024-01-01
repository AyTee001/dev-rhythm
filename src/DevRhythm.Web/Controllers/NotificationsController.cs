using DevRhythm.App.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DevRhythm.Web.Controllers
{
    public class NotificationsController : Controller
    {
        [HttpPost]
        public IActionResult RenderNotification([FromBody] NotificationDto notification)
        {
            return PartialView("_NotificationPartial", notification);
        }
    }
}
