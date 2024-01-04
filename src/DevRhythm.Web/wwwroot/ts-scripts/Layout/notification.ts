import * as signalR from "@microsoft/signalr"
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

function notificationReceived(notification: any) {
    $.ajax({
        url: '/Notifications/RenderToastNotification',
        method: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(notification),
        success: function (data) {
            incrementBadgeNotificationCount();
            console.log(data);
            var $toast = $(data).appendTo(".toast-container");

            var closeButton = $toast.find('.close');
            closeButton.on('click', function () {
                $toast.remove();
            });

            $toast.toast({
                autohide: true,
                delay: 5000
            }).toast('show').on('hidden.bs.toast', function () {
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

var NotificationType;
(function (NotificationType) {
    NotificationType[NotificationType["VoteNotification"] = 0] = "VoteNotification";
})(NotificationType || (NotificationType = {}));
