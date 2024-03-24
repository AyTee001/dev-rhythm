/*
 * ATTENTION: The "eval" devtool has been used (maybe by default in mode: "development").
 * This devtool is neither made for production nor for readable output files.
 * It uses "eval()" calls to create a separate source file in the browser devtools.
 * If you are trying to read the output file, select a different devtool (https://webpack.js.org/configuration/devtool/)
 * or disable the default devtool with "devtool: false".
 * If you are looking for production-ready output files, see mode: "production" (https://webpack.js.org/configuration/mode/).
 */
/******/ (() => { // webpackBootstrap
/******/ 	"use strict";
/******/ 	var __webpack_modules__ = ({

/***/ "./wwwroot/ts-scripts/Post/posts.ts":
/*!******************************************!*\
  !*** ./wwwroot/ts-scripts/Post/posts.ts ***!
  \******************************************/
/***/ ((__unused_webpack_module, exports) => {

eval("\nObject.defineProperty(exports, \"__esModule\", ({ value: true }));\nlet currentPage = 0;\nlet loading = false;\nlet isEnd = false;\nconst selectedTagsInput = $(\"input[name='selectedTags']\");\nconst SCROLL_THRESHOLD = 100;\nconst POST_COUNT_PER_PAGE = 10;\nfunction loadMorePosts(tagIds) {\n    const settings = {\n        sortProperty: $(\"#SortProperty\").val(),\n        sortOrder: $(\"#SortOrder\").val()\n    };\n    const keyword = $(\"#search-keyword\").text();\n    if (!loading && !isEnd) {\n        loading = true;\n        $.ajax({\n            url: \"/Post/GetPosts\",\n            method: \"POST\",\n            contentType: \"application/json\",\n            data: JSON.stringify({\n                PageNumber: currentPage + 1,\n                TagIds: tagIds,\n                PostCount: POST_COUNT_PER_PAGE,\n                SortSettings: settings,\n                Keyword: keyword ? keyword : \"\"\n            }),\n            success: function (data) {\n                if (data) {\n                    $(\"#post-section\").append(data);\n                    $(\".post-preview-card\").each(function () {\n                        const $this = $(this);\n                        // Highlight main content\n                        const postContent = $this.find(\".post-content\").html() || \"\";\n                        const highlightedContent = highlightSearchQuery(keyword, postContent);\n                        $this.find(\".post-content\").html(highlightedContent);\n                        // Highlight heading\n                        const heading = $this.find(\".post-heading\").html() || \"\";\n                        const highlightedHeading = highlightSearchQuery(keyword, heading);\n                        $this.find(\".post-heading\").html(highlightedHeading);\n                        // Highlight tags\n                        $this.find(\".tag\").each(function () {\n                            const tagText = $(this).text();\n                            const highlightedTag = highlightSearchQuery(keyword, tagText);\n                            $(this).html(highlightedTag);\n                        });\n                    });\n                    currentPage++;\n                    loading = false;\n                }\n                else {\n                    isEnd = true;\n                }\n            },\n            error: function () {\n                loading = false;\n            },\n        });\n    }\n}\nfunction highlightSearchQuery(query, html) {\n    // Create a regular expression to match the search query\n    const regex = new RegExp(query, 'gi');\n    // Replace occurrences of the query with the highlighted version\n    const highlightedHtml = html.replace(regex, `<span class=\"highlight\">$&</span>`);\n    console.log(highlightedHtml);\n    return highlightedHtml;\n}\nselectedTagsInput.on(\"change\", reloadposts);\n$(window).on(\"scroll\", function () {\n    if ($(window).scrollTop() >= $(document).height() - $(window).height() - SCROLL_THRESHOLD) {\n        loadMorePosts(getAllCheckedTags());\n    }\n});\n$(function () {\n    loadMorePosts(getAllCheckedTags());\n});\nfunction resetPosts() {\n    currentPage = 0;\n    isEnd = false;\n    $(\"#post-section\").empty();\n}\nfunction getAllCheckedTags() {\n    return selectedTagsInput.filter(\":checked\").map(function () {\n        return parseInt($(this).val());\n    }).get();\n}\nfunction reloadposts() {\n    resetPosts();\n    loadMorePosts(getAllCheckedTags());\n}\n$(\"#SortProperty, #SortOrder\").on(\"change\", reloadposts);\nvar SortProperty;\n(function (SortProperty) {\n    SortProperty[SortProperty[\"None\"] = 0] = \"None\";\n    SortProperty[SortProperty[\"CreatedAt\"] = 1] = \"CreatedAt\";\n    SortProperty[SortProperty[\"VoteResult\"] = 2] = \"VoteResult\";\n})(SortProperty || (SortProperty = {}));\nvar SortOrder;\n(function (SortOrder) {\n    SortOrder[SortOrder[\"Ascending\"] = 0] = \"Ascending\";\n    SortOrder[SortOrder[\"Descending\"] = 1] = \"Descending\";\n})(SortOrder || (SortOrder = {}));\n\n\n//# sourceURL=webpack://devrhythm.web/./wwwroot/ts-scripts/Post/posts.ts?");

/***/ })

/******/ 	});
/************************************************************************/
/******/ 	
/******/ 	// startup
/******/ 	// Load entry module and return exports
/******/ 	// This entry module can't be inlined because the eval devtool is used.
/******/ 	var __webpack_exports__ = {};
/******/ 	__webpack_modules__["./wwwroot/ts-scripts/Post/posts.ts"](0, __webpack_exports__);
/******/ 	
/******/ })()
;