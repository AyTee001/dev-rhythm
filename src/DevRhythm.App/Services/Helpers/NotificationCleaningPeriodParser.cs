using DevRhythm.Shared.Enums;

namespace DevRhythm.App.Services.Helpers
{
    public static class NotificationCleaningPeriodParser
    {
        public static string ParsePeriodToCronExpression(NotificationCleaningPeriod notificationCleaningPeriod)
        {
            return notificationCleaningPeriod switch
            {
                NotificationCleaningPeriod.Daily => "0 0 * * *",
                NotificationCleaningPeriod.Weekly => "0 0 * * 0",
                NotificationCleaningPeriod.Monthly => "0 0 1 * *",
                _ => throw new ArgumentException("No such time period")
            };
        }
    }
}
