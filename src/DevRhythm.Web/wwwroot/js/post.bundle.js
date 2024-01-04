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

eval("\nObject.defineProperty(exports, \"__esModule\", ({ value: true }));\nlet currentPage = 0;\nlet loading = false;\nlet isEnd = false;\nconst selectedTagsInput = $(\"input[name='selectedTags']\");\nconst SCROLL_THRESHOLD = 100;\nconst POST_COUNT_PER_PAGE = 10;\nfunction loadMorePosts(tagIds) {\n    const settings = {\n        sortProperty: $('#SortProperty').val(),\n        sortOrder: $('#SortOrder').val()\n    };\n    if (!loading && !isEnd) {\n        loading = true;\n        $.ajax({\n            url: '/Post/GetPosts',\n            method: 'POST',\n            contentType: 'application/json',\n            data: JSON.stringify({\n                PageNumber: currentPage + 1,\n                TagIds: tagIds,\n                PostCount: POST_COUNT_PER_PAGE,\n                SortSettings: settings\n            }),\n            success: function (data) {\n                if (data) {\n                    $('#post-section').append(data);\n                    currentPage++;\n                    loading = false;\n                }\n                else {\n                    isEnd = true;\n                }\n            },\n            error: function () {\n                loading = false;\n            },\n        });\n    }\n}\nselectedTagsInput.on('change', reloadposts);\n$(window).on('scroll', function () {\n    if ($(window).scrollTop() >= $(document).height() - $(window).height() - SCROLL_THRESHOLD) {\n        loadMorePosts(getAllCheckedTags());\n    }\n});\n$(function () {\n    loadMorePosts(getAllCheckedTags());\n});\nfunction getPostsByTag(tagId) {\n    resetPosts();\n    let targetTag = $(`#tag_${tagId}`);\n    selectedTagsInput.not(targetTag).prop('checked', false);\n    targetTag.prop('checked', true);\n    loadMorePosts([tagId]);\n}\nfunction resetPosts() {\n    currentPage = 0;\n    isEnd = false;\n    $('#post-section').empty();\n}\nfunction getAllCheckedTags() {\n    return selectedTagsInput.filter(':checked').map(function () {\n        return parseInt($(this).val());\n    }).get();\n}\nfunction reloadposts() {\n    resetPosts();\n    loadMorePosts(getAllCheckedTags());\n}\n$(\"#SortProperty, #SortOrder\").on('change', reloadposts);\nvar SortProperty;\n(function (SortProperty) {\n    SortProperty[SortProperty[\"None\"] = 0] = \"None\";\n    SortProperty[SortProperty[\"CreatedAt\"] = 1] = \"CreatedAt\";\n    SortProperty[SortProperty[\"VoteResult\"] = 2] = \"VoteResult\";\n})(SortProperty || (SortProperty = {}));\nvar SortOrder;\n(function (SortOrder) {\n    SortOrder[SortOrder[\"Ascending\"] = 0] = \"Ascending\";\n    SortOrder[SortOrder[\"Descending\"] = 1] = \"Descending\";\n})(SortOrder || (SortOrder = {}));\n\n\n//# sourceURL=webpack://devrhythm.web/./wwwroot/ts-scripts/Post/posts.ts?");

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