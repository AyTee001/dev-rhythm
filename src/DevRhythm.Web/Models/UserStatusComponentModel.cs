namespace DevRhythm.Web.Models
{
    public class UserStatusComponentModel
    {
        public bool IsUserAuthenticated { get; set; }
        public int UserUnreadNotificationCount { get; set; }
        public long CurrentUserId { get; set; }
    }
}
