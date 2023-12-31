﻿using DevRhythm.App.Interfaces;
using DevRhythm.Infrastructure.Hubs.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace DevRhythm.Infrastructure.Hubs
{
    public sealed class NotificationHub(INotificationService notificationService) : Hub<INotificationHub>
    {
        private readonly INotificationService _notificationService = notificationService;
        public override async Task OnConnectedAsync()
        {
            var userId = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if(!string.IsNullOrEmpty(userId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, userId!);
            }
        }

        public async Task MarkNotificationAsReadByIdAsync(long notificationId, long userId)
        {
            await _notificationService.MarkNotificationAsReadByIdAsync(notificationId, userId);
        }
    }
}
