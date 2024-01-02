﻿using AutoMapper;
using DevRhythm.App.DTOs;
using DevRhythm.App.Interfaces;
using DevRhythm.App.Services.Base;
using DevRhythm.Core.Entities;
using DevRhythm.Infrastructure.Data;
using DevRhythm.Infrastructure.Hubs;
using DevRhythm.Infrastructure.Hubs.Interfaces;
using DevRhythm.Shared.Exceptions;
using DevRhythm.Shared.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace DevRhythm.App.Services
{
    public class NotificationService(IHubContext<NotificationHub, INotificationHub> notificationHubContext, 
        DevRhythmDbContext context, 
        IMapper mapper,
        IUserInfoProvider userInfoProvider) : BaseService(context, mapper), INotificationService
    {
        private readonly IHubContext<NotificationHub, INotificationHub> _notificationHubContext = notificationHubContext;
        private readonly IUserInfoProvider _userInfoProvider = userInfoProvider;

        public async Task AddNotificationToStorageAsync(NotificationDto notificationDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var notification = _mapper.Map<Notification>(notificationDto);

                await _context.AddAsync(notification);
                await _context.SaveChangesAsync();

                var receiversIds = notificationDto.ReceiverId is null
                    ? await _context.Users.Select(e => e.Id).ToListAsync()
                    : [(long)notificationDto.ReceiverId];

                await _context.AddRangeAsync(receiversIds.Select(x =>
                    new UserNotification
                    {
                        IsRead = false,
                        NotificationId = notification.Id,
                        ReceiverId = x
                    }
                ));

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<ICollection<NotificationDto>> GetNotificationsByUserIdAsync(long userId)
        {
            var currUserId = _userInfoProvider.Id ?? throw new NotFoundException(nameof(User), userId);

            var notifications = _context.UserNotifications
                .Where(x => x.ReceiverId == userId)
                .Include(e => e.Notification)
                    .ThenInclude(e => e!.Sender)
                .Include(e => e.Notification)
                    .ThenInclude(e => e!.Post);

            var notificationDtos = notifications.Select(n =>
                new NotificationDto
                {
                    NotificationType = n.Notification!.NotificationType,
                    Sender = _mapper.Map<UserNotificationDto>(n.Notification.Sender),
                    SentAt = n.Notification.SentAt,
                    Message = n.Notification.Message,
                    PostDto = _mapper.Map<PostNotificationDto>(n.Notification.Post),
                    ReceiverId = userId
                }
            );

            return await notificationDtos.ToListAsync();
        }

        public void SendNotification(NotificationDto notification)
        {
            if(notification.ReceiverId is not null)
            {
                _notificationHubContext.Clients.All.SendNotificationAsync(notification);
            }
        }
    }
}