﻿import * as signalR from "@microsoft/signalr"


const connectionNotificationIndex = new signalR.HubConnectionBuilder()
    .withUrl("/notificationHub")
    .build();


connectionNotificationIndex.on("SendNotificationAsync", (notification) => {
    appendNotification(notification);
});

connectionNotificationIndex.start().catch(() => console.log("Error"));

function appendNotification(notification) {
    $.ajax({
        url: "/Notifications/RenderNotification",
        method: "POST",
        contentType: "application/json",
        data: JSON.stringify(notification),
        success: function (data) {
            $("#notifications-container").prepend(data);

            connectionNotificationIndex.invoke("MarkNotificationAsReadByIdAsync", notification.id, notification.receiverId)
                .catch(error => console.error(error));
        },
        error: function (err) {
            console.log(err);
        },
    });
}