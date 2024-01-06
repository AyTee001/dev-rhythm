function doUpvote (guid: string) {
    vote(true, guid);
} 
function doDownvote(guid: string) {
    vote(false, guid);
}

(window as any).doUpvote = doUpvote;
(window as any).doDownvote = doDownvote;

const votedClassName = "voted";
const downvotedClassName = "downvoted";

function vote(isUpvote: boolean, guid: string) {
    const voteDataSelector = `#vote-data-${guid}`;
    const upvoteBtnSelector = `#upvote-btn-${guid}`;
    const downvoteBtnSelector = `#downvote-btn-${guid}`;

    let voteData = JSON.parse($(voteDataSelector).val() as string) as VoteModel;

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
            } else {
                $(upvoteBtnSelector).toggleClass(votedClassName, false);
                $(downvoteBtnSelector).toggleClass(downvotedClassName, !$(downvoteBtnSelector).hasClass(downvotedClassName));
            }
        },
        error: function (error) {
            console.error("Error updating data:", error);
        }
    });

}

enum VoteType {
    PostVote = 0,
    CommentVote = 1,
    ReplyVote = 2
}

interface VoteModel {
    voteType: VoteType,
    voteResult: number,
    entityId: number,
    userId: number
}