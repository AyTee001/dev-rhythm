﻿const connectionNotificationIndex = new signalR.HubConnectionBuilder()
    .withUrl("/notificationHub")
    .build();


connectionNotificationIndex.on("SendNotificationAsync", (notification) => {
    appendNotification(notification);
});

connectionNotificationIndex.start().catch(() => console.log("Error"));

function appendNotification(notification) {
    $.ajax({
        url: '/Notifications/RenderNotification',
        method: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(notification),
        success: function (data) {
            $("#notifications-main").prepend(data);
        },
        error: function (err) {
            console.log(err);
        },
    });
}