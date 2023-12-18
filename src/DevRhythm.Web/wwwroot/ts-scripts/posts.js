let currentPage = 0;
let loading = false;
let isEnd = false;
const selectedTagsInput = $("input[name='selectedTags']");
const SCROLL_THRESHOLD = 100;
function loadMorePosts(tagIds) {
    if (!loading && !isEnd) {
        loading = true;
        $.ajax({
            url: '/Post/GetPosts',
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({
                PageNumber: currentPage + 1,
                TagIds: tagIds,
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
selectedTagsInput.on('change', function () {
    resetPosts();
    loadMorePosts(getAllCheckedTags());
});
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
//# sourceMappingURL=posts.js.map