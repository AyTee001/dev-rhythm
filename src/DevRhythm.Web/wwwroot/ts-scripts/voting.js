function doUpvote(guid) {
    vote(true, guid);
}
function doDownvote(guid) {
    vote(false, guid);
}
function vote(isUpvote, guid) {
    const voteDataSelector = `#vote-data-${guid}`;
    const upvoteBtnSelector = `#upvote-btn-${guid}`;
    const downvoteBtnSelector = `#downvote-btn-${guid}`;
    let voteData = JSON.parse($(voteDataSelector).val());
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
            if (isUpvote) {
                $(upvoteBtnSelector).toggleClass('voted', !$(upvoteBtnSelector).hasClass('voted'));
                $(downvoteBtnSelector).toggleClass('downvoted', false);
            }
            else {
                $(upvoteBtnSelector).toggleClass('voted', false);
                $(downvoteBtnSelector).toggleClass('downvoted', !$(downvoteBtnSelector).hasClass('downvoted'));
            }
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
//# sourceMappingURL=voting.js.map