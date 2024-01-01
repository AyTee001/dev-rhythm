function doUpvote (guid: string) {
    vote(true, guid);
} 
function doDownvote(guid: string) {
    vote(false, guid);
}

function vote(isUpvote: boolean, guid: string) {
    const voteDataSelector = `#vote-data-${guid}`;
    const upvoteBtnSelector = `#upvote-btn-${guid}`;
    const downvoteBtnSelector = `#downvote-btn-${guid}`;

    let voteData = JSON.parse($(voteDataSelector).val() as string) as VoteModel;

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
            } else {
                $(upvoteBtnSelector).toggleClass('voted', false);
                $(downvoteBtnSelector).toggleClass('downvoted', !$(downvoteBtnSelector).hasClass('downvoted'));
            }
        },
        error: function (error) {
            console.error('Error updating data:', error);
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