let currentPage: number = 0;
let loading: boolean = false;
let isEnd: boolean = false;
const selectedTagsInput = $("input[name='selectedTags']");
const SCROLL_THRESHOLD = 100;
const POST_COUNT_PER_PAGE = 10;

function loadMorePosts(tagIds: number[]): void {
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
            success: function (data: any) {
                if (data) {
                    $('#post-section').append(data);
                    currentPage++;
                    loading = false;
                } else {
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

function getPostsByTag(tagId: number) {
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
    return selectedTagsInput.filter(':checked').map(function (this: HTMLInputElement) {
        return parseInt($(this).val()) as number;
    }).get();
}