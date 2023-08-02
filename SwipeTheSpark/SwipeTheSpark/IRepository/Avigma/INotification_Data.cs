using SwipeTheSpark.Models.Avigma;

namespace SwipeTheSpark.IRepository
{
    public interface INotification_Data
    {
        Task<ResponseModel> SendNotification(NotificationMasterDTO notificationModel);
        Task<string> SendNotificationToken(NotificationMasterTokenDTO notification);
    }
}
