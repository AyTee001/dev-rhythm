let currentPage = 0;
let loading = false;
let isEnd = false;
const selectedTagsInput = $("input[name='selectedTags']");
const SCROLL_THRESHOLD = 100;
const POST_COUNT_PER_PAGE = 10;
function loadMorePosts(tagIds) {
    const settings = {
        sortProperty: $('#SortProperty').val(),
        sortOrder: $('#SortOrder').val()
    };
    if (!loading && !isEnd) {
        loading = true;
        $.ajax({
            url: '/Post/GetPosts',
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({
                PageNumber: currentPage + 1,
                TagIds: tagIds,
                PostCount: POST_COUNT_PER_PAGE,
                SortSettings: settings
            }),
            success: function (data) {
                if (data) {
                    $('#post-section').append(data);
                    currentPage++;
                    loading = false;
                }
                else {
                    isEnd = true;
                }
            },
            error: function () {
                loading = false;
            },
        });
    }
}
selectedTagsInput.on('change', reloadposts);
$(window).on('scroll', function () {
    if ($(window).scrollTop() >= $(document).height() - $(window).height() - SCROLL_THRESHOLD) {
        loadMorePosts(getAllCheckedTags());
    }
});
$(function () {
    loadMorePosts(getAllCheckedTags());
});
function getPostsByTag(tagId) {
    resetPosts();
    let targetTag = $(`#tag_${tagId}`);
    selectedTagsInput.not(targetTag).prop('checked', false);
    targetTag.prop('checked', true);
    loadMorePosts([tagId]);
}
function resetPosts() {
    currentPage = 0;
    isEnd = false;
    $('#post-section').empty();
}
function getAllCheckedTags() {
    return selectedTagsInput.filter(':checked').map(function () {
        return parseInt($(this).val());
    }).get();
}
function reloadposts() {
    resetPosts();
    loadMorePosts(getAllCheckedTags());
}
$("#SortProperty, #SortOrder").on('change', reloadposts);
var SortProperty;
(function (SortProperty) {
    SortProperty[SortProperty["None"] = 0] = "None";
    SortProperty[SortProperty["CreatedAt"] = 1] = "CreatedAt";
    SortProperty[SortProperty["VoteResult"] = 2] = "VoteResult";
})(SortProperty || (SortProperty = {}));
var SortOrder;
(function (SortOrder) {
    SortOrder[SortOrder["Ascending"] = 0] = "Ascending";
    SortOrder[SortOrder["Descending"] = 1] = "Descending";
})(SortOrder || (SortOrder = {}));
//# sourceMappingURL=posts.js.map