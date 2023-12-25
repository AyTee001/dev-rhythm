function doUpvote() {
    vote(true);
}
function doDownvote() {
    vote(false);
}
function vote(isUpvote) {
    let voteData = JSON.parse($('#vote-data').val());
    $.ajax({
        url: '/Vote/UpdateVote',
        method: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({
            VoteType: voteData.voteType,
            EntityId: voteData.entityId,
            UserId: voteData.userId,
            IsUpvote: isUpvote
        }),
        success: function (data) {
            console.log("success");
        },
        error: function (error) {
            console.error('Error updating data:', error);
        }
    });
}
var VoteType;
(function (VoteType) {
    VoteType[VoteType["PostVote"] = 0] = "PostVote";
    VoteType[VoteType["CommentVote"] = 1] = "CommentVote";
    VoteType[VoteType["ReplyVote"] = 2] = "ReplyVote";
})(VoteType || (VoteType = {}));
