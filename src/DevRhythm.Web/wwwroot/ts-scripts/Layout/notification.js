"use strict";
var __createBinding = (this && this.__createBinding) || (Object.create ? (function(o, m, k, k2) {
    if (k2 === undefined) k2 = k;
    var desc = Object.getOwnPropertyDescriptor(m, k);
    if (!desc || ("get" in desc ? !m.__esModule : desc.writable || desc.configurable)) {
      desc = { enumerable: true, get: function() { return m[k]; } };
    }
    Object.defineProperty(o, k2, desc);
}) : (function(o, m, k, k2) {
    if (k2 === undefined) k2 = k;
    o[k2] = m[k];
}));
var __setModuleDefault = (this && this.__setModuleDefault) || (Object.create ? (function(o, v) {
    Object.defineProperty(o, "default", { enumerable: true, value: v });
}) : function(o, v) {
    o["default"] = v;
});
var __importStar = (this && this.__importStar) || function (mod) {
    if (mod && mod.__esModule) return mod;
    var result = {};
    if (mod != null) for (var k in mod) if (k !== "default" && Object.prototype.hasOwnProperty.call(mod, k)) __createBinding(result, mod, k);
    __setModuleDefault(result, mod);
    return result;
};
Object.defineProperty(exports, "__esModule", { value: true });
const signalR = __importStar(require("@microsoft/signalr"));
$(document).ready(function () {
    let $notificationBadge = $("#notificationBadge");
    let currentCount = parseInt($notificationBadge.text(), 10) || 0;
    if (currentCount === 0) {
        $notificationBadge.hide();
    }
    else {
        $notificationBadge.show();
    }
});
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/notificationHub")
    .build();
connection.on("SendNotificationAsync", (notification) => {
    notificationReceived(notification);
});
connection.start().catch(() => console.log("Error"));
function notificationReceived(notification) {
    $.ajax({
        url: "/Notifications/RenderToastNotification",
        method: "POST",
        contentType: "application/json",
        data: JSON.stringify(notification),
        success: function (data) {
            incrementBadgeNotificationCount();
            console.log(data);
            var $toast = $(data).appendTo(".toast-container");
            var closeButton = $toast.find(".close");
            closeButton.on("click", function () {
                $toast.remove();
            });
            $toast.toast({
                autohide: true,
                delay: 5000
            }).toast("show").on("hidden.bs.toast", function () {
                $(this).remove();
            });
        },
        error: function (err) {
            console.log(err);
        },
    });
}
function incrementBadgeNotificationCount() {
    const $badge = $("#notificationBadge");
    let currentCount = parseInt($badge.text(), 10) || 0;
    currentCount++;
    $badge.text(currentCount);
    $badge.show();
}
$("#notificationCleaningPeriod").on('change', function () {
    console.log($("#notificationCleaningPeriod").val());
    $.ajax({
        url: "/Notifications/SetUpCleaningJob",
        method: "POST",
        contentType: "application/json",
        data: JSON.stringify({
            userId: $("#userIdNotificationCleaningPreference").val(),
            notificationCleaningPeriod: $("#notificationCleaningPeriod").val()
        }),
        error: function (err) {
            console.log(err);
        },
    });
});
//# sourceMappingURL=notification.js.map