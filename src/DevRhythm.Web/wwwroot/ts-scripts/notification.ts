function notificationReceived(notification: NotificationDto) {
    $.ajax({
        url: '/Notifications/RenderNotification',
        method: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(notification),
        success: function (data: any) {
            console.log(data)
            $('body').append(data);
            $('.toast').toast({ autohide: true, delay: 5000 });
            $('.toast').toast('show');
        },
        error: function (err: any) {
            console.log(err)
        },
    });

}


interface NotificationDto {
    receiverId: number | null,
    message: string,
    sentAt: Date,
    notificationType: NotificationType
    sender: UserNotificationDto
    postDto: PostNotificationDto
}

enum NotificationType{
    VoteNotification
}

interface UserNotificationDto {
    id: number,
    userName: string
}

interface PostNotificationDto {
    id: number,
    heading: string
}