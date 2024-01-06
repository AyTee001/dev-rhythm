"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
function doUpvote(guid) {
    vote(true, guid);
}
function doDownvote(guid) {
    vote(false, guid);
}
window.doUpvote = doUpvote;
window.doDownvote = doDownvote;
const votedClassName = "voted";
const downvotedClassName = "downvoted";
function vote(isUpvote, guid) {
    const voteDataSelector = `#vote-data-${guid}`;
    const upvoteBtnSelector = `#upvote-btn-${guid}`;
    const downvoteBtnSelector = `#downvote-btn-${guid}`;
    let voteData = JSON.parse($(voteDataSelector).val());
    $.ajax({
        url: "/Vote/UpdateVote",
        method: "POST",
        contentType: "application/json",
        data: JSON.stringify({
            VoteType: voteData.voteType,
            EntityId: voteData.entityId,
            UserId: voteData.userId,
            IsUpvote: isUpvote
        }),
        success: function (data) {
            if (isUpvote) {
                $(upvoteBtnSelector).toggleClass(votedClassName, !$(upvoteBtnSelector).hasClass(votedClassName));
                $(downvoteBtnSelector).toggleClass(downvotedClassName, false);
            }
            else {
                $(upvoteBtnSelector).toggleClass(votedClassName, false);
                $(downvoteBtnSelector).toggleClass(downvotedClassName, !$(downvoteBtnSelector).hasClass(downvotedClassName));
            }
        },
        error: function (error) {
            console.error("Error updating data:", error);
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