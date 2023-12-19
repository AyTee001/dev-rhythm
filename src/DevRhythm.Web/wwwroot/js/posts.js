let currentPage = 0;
let loading = false;
let isEnd = false;
const selectedTagsInput = $("input[name='selectedTags']");
const SCROLL_THRESHOLD = 100;
const POST_COUNT_PER_PAGE = 10;
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
                PostCount: POST_COUNT_PER_PAGE
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
