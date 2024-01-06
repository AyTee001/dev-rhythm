using System.ComponentModel.DataAnnotations;

namespace DevRhythm.Shared.Enums
{
    public enum NotificationCleaningPeriod
    {
        [Display(Name = "No_clean")]
        None,
        [Display(Name = "Daily_clean")]
        Daily,
        [Display(Name = "Weekly_clean")]
        Weekly,
        [Display(Name = "Monthly_clean")]
        Monthly
    }
}
