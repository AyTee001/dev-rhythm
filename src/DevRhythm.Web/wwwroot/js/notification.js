var toastElList = [].slice.call(document.querySelectorAll('.toast'))
var toastList = toastElList.map(function (toastEl) {
    return new bootstrap.Toast(toastEl, option)
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
        url: '/Notifications/RenderNotification',
        method: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(notification),
        success: function (data) {
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
var NotificationType;
(function (NotificationType) {
    NotificationType[NotificationType["VoteNotification"] = 0] = "VoteNotification";
})(NotificationType || (NotificationType = {}));
