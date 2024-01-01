using DevRhythm.Infrastructure.Hubs.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace DevRhythm.Infrastructure.Hubs
{
    public sealed class NotificationHub : Hub<INotificationHub>
    {
        public override async Task OnConnectedAsync()
        {
            var userId = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if(!string.IsNullOrEmpty(userId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, userId!);
            }
        }
    }
}
