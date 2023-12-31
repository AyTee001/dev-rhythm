﻿using AutoMapper;
using DevRhythm.App.DTOs;
using DevRhythm.App.Interfaces;
using DevRhythm.App.Services.Base;
using DevRhythm.App.Services.Helpers;
using DevRhythm.Core.Entities;
using DevRhythm.Infrastructure.Data;
using DevRhythm.Infrastructure.Hubs;
using DevRhythm.Infrastructure.Hubs.Interfaces;
using DevRhythm.Core.Enums;
using DevRhythm.Shared.Exceptions;
using Hangfire;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace DevRhythm.App.Services
{
    public class NotificationService(IHubContext<NotificationHub, INotificationHub> notificationHubContext, 
        DevRhythmDbContext context, 
        IMapper mapper) : BaseService(context, mapper), INotificationService
    {
        private readonly IHubContext<NotificationHub, INotificationHub> _notificationHubContext = notificationHubContext;

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

                notificationDto.Id = notification.Id;

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<int> CountUnreadNotificationsForUserAsync(long userId)
        {
            return await _context.UserNotifications.CountAsync(x => x.ReceiverId == userId && x.IsRead == false);
        }

        public async Task<FullNotificationUserDataDto> GetNotificationsByUserIdAsync(long userId)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == userId) ?? throw new NotFoundException(nameof(User), userId);

            var notifications = _context.UserNotifications
                .Where(x => x.ReceiverId == userId)
                .Include(e => e.Notification)
                    .ThenInclude(e => e!.Sender)
                .Include(e => e.Notification)
                    .ThenInclude(e => e!.Post);

            var notificationDtos = notifications.OrderByDescending(n => n.Notification!.SentAt).ThenByDescending(n => n.IsRead).Select(n =>
                new NotificationDto
                {
                    Id = n.Notification!.Id,
                    NotificationType = n.Notification!.NotificationType,
                    Sender = _mapper.Map<UserNotificationDto>(n.Notification.Sender),
                    SentAt = n.Notification.SentAt,
                    Message = n.Notification.Message,
                    PostDto =  _mapper.Map<PostNotificationDto>(n.Notification.Post),
                    ReceiverId = userId,
                    IsRead = n.IsRead
                }
            );

            return new FullNotificationUserDataDto {
                UserId = userId,
                Notifications = await notificationDtos.ToListAsync(),
                NotificationCleaningPeriod = user.NotificationCleaningPeriod
            };
        }

        public async Task MarkNotificationsAsReadAsync(long userId)
        {
            await _context.UserNotifications.Where(n => n.ReceiverId == userId).ForEachAsync(x => x.IsRead = true);
            await _context.SaveChangesAsync();
        }

        public async Task MarkNotificationAsReadByIdAsync(long notificationId, long userId)
        {
            await _context.UserNotifications.Where(n => n.ReceiverId == userId && n.NotificationId == notificationId).ForEachAsync(x => x.IsRead = true);
            await _context.SaveChangesAsync();
        }

        public void SendNotification(NotificationDto notification)
        {
            if(notification.ReceiverId is not null)
            {
                _notificationHubContext.Clients.Group(notification.ReceiverId.ToString()!).SendNotificationAsync(notification);
            }
            else
            {
                _notificationHubContext.Clients.All.SendNotificationAsync(notification);
            }
        }

        public void SetCleanAllNotificationByUserIdJob(long userId, NotificationCleaningPeriod notificationCleaningPeriod)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == userId) ?? throw new NotFoundException(nameof(User), userId);

            string jobName = $"{userId}{JobNames.NotificationCleaningJob}";
            if (notificationCleaningPeriod == NotificationCleaningPeriod.None)
            {
                RecurringJob.RemoveIfExists(jobName);
                return;
            }

            RecurringJob.AddOrUpdate(jobName, 
                () => RemoveNotificationsByUserIdAsync(userId), 
                NotificationCleaningPeriodParser.ParsePeriodToCronExpression(notificationCleaningPeriod));

            user.NotificationCleaningPeriod = notificationCleaningPeriod;
            _context.SaveChanges();
        }

        public async Task RemoveNotificationsByUserIdAsync(long userId)
        {
            _context.RemoveRange(_context.UserNotifications.Where(x => x.ReceiverId == userId && x.IsRead == true));
            await _context.SaveChangesAsync();
        }
    }
}
