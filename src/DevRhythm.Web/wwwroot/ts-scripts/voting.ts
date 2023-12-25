function doUpvote () {
    vote(true);
} 
function doDownvote() {
    vote(false);
}

function vote(isUpvote: boolean) {

    let voteData = JSON.parse($('#vote-data').val() as string) as VoteModel;

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