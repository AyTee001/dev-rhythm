let currentPage: number = 0;
let loading: boolean = false;
let isEnd: boolean = false;
const selectedTagsInput = $("input[name='selectedTags']");
const SCROLL_THRESHOLD = 100;
const POST_COUNT_PER_PAGE = 10;

function loadMorePosts(tagIds: number[]): void {

    const settings: SortSettings = {
        sortProperty: $("#SortProperty").val() as number,
        sortOrder: $("#SortOrder").val() as number
    }

    const keyword = $("#search-keyword").text() as string;
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
            success: function (data: any) {
                if (data) {
                    $("#post-section").append(data);
                    $(".post-preview-card").each(function () {
                        const $this: JQuery<HTMLElement> = $(this);

                        // Highlight main content
                        const postContent: string = $this.find(".post-content").html() || "";
                        const highlightedContent: string = highlightSearchQuery(keyword, postContent);
                        $this.find(".post-content").html(highlightedContent);

                        // Highlight heading
                        const heading: string = $this.find(".post-heading").html() || "";
                        const highlightedHeading: string = highlightSearchQuery(keyword, heading);
                        $this.find(".post-heading").html(highlightedHeading);

                        // Highlight tags
                        $this.find(".tag").each(function () {
                            const tagText: string = $(this).text();
                            const highlightedTag: string = highlightSearchQuery(keyword, tagText);
                            $(this).html(highlightedTag);
                        });
                    });
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

function highlightSearchQuery(query: string, html: string): string {
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
    return selectedTagsInput.filter(":checked").map(function (this: HTMLInputElement) {
        return parseInt($(this).val()) as number;
    }).get();
}

function reloadposts() {
    resetPosts();
    loadMorePosts(getAllCheckedTags());
}

$("#SortProperty, #SortOrder").on("change", reloadposts);

enum SortProperty {
    None = 0,
    CreatedAt = 1,
    VoteResult = 2
}

enum SortOrder {
    Ascending = 0,
    Descending = 1
}

interface SortSettings {
    sortProperty: SortProperty,
    sortOrder: SortOrder
}
