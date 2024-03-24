"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
let currentPage = 0;
let loading = false;
let isEnd = false;
const selectedTagsInput = $("input[name='selectedTags']");
const SCROLL_THRESHOLD = 100;
const POST_COUNT_PER_PAGE = 10;
function loadMorePosts(tagIds) {
    const settings = {
        sortProperty: $("#SortProperty").val(),
        sortOrder: $("#SortOrder").val()
    };
    const keyword = $("#search-keyword").text();
    if (!loading && !isEnd) {
        loading = true;
        $.ajax({
            url: "/Post/GetPosts",
            method: "POST",
            contentType: "application/json",
            data: JSON.stringify({
                PageNumber: currentPage + 1,
                TagIds: tagIds,
                PostCount: POST_COUNT_PER_PAGE,
                SortSettings: settings,
                Keyword: keyword ? keyword : ""
            }),
            success: function (data) {
                if (data) {
                    $("#post-section").append(data);
                    $(".post-preview-card").each(function () {
                        const $this = $(this);
                        // Highlight main content
                        const postContent = $this.find(".post-content").html() || "";
                        const highlightedContent = highlightSearchQuery(keyword, postContent);
                        $this.find(".post-content").html(highlightedContent);
                        // Highlight heading
                        const heading = $this.find(".post-heading").html() || "";
                        const highlightedHeading = highlightSearchQuery(keyword, heading);
                        $this.find(".post-heading").html(highlightedHeading);
                        // Highlight tags
                        $this.find(".tag").each(function () {
                            const tagText = $(this).text();
                            const highlightedTag = highlightSearchQuery(keyword, tagText);
                            $(this).html(highlightedTag);
                        });
                    });
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
function highlightSearchQuery(query, html) {
    // Create a regular expression to match the search query
    const regex = new RegExp(query, 'gi');
    // Replace occurrences of the query with the highlighted version
    const highlightedHtml = html.replace(regex, `<span class="highlight">$&</span>`);
    console.log(highlightedHtml);
    return highlightedHtml;
}
selectedTagsInput.on("change", reloadposts);
$(window).on("scroll", function () {
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
    $("#post-section").empty();
}
function getAllCheckedTags() {
    return selectedTagsInput.filter(":checked").map(function () {
        return parseInt($(this).val());
    }).get();
}
function reloadposts() {
    resetPosts();
    loadMorePosts(getAllCheckedTags());
}
$("#SortProperty, #SortOrder").on("change", reloadposts);
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