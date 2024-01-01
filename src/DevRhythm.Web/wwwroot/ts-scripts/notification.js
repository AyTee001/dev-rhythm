function notificationReceived(notification) {
    $.ajax({
        url: '/Notifications/RenderNotification',
        method: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(notification),
        success: function (data) {
            console.log(data);
            $('body').append(data);
            $('.toast').toast({ autohide: true, delay: 5000 });
            $('.toast').toast('show');
        },
        error: function (err) {
            console.log(err);
        },
    });
}
var NotificationType;
(function (NotificationType) {
    NotificationType[NotificationType["VoteNotification"] = 0] = "VoteNotification";
})(NotificationType || (NotificationType = {}));
//# sourceMappingURL=notification.js.map