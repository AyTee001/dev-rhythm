using DevRhythm.Shared.Enums;

namespace DevRhythm.App.Services.Helpers
{
    public static class NotificationMessageHelper
    {
        public static string GetVoteNotificationMessage(bool isUpvote, VoteType voteType)
        {
            string entityType = voteType switch
            {
                VoteType.ReplyVote => "reply",
                VoteType.PostVote => "post",
                VoteType.CommentVote => "comment",
                _ => "entity"
            };
           
            return $"Your {entityType} was " + (isUpvote ? "upvoted" : "downvoted") + " by this user";
        }
    }
}
