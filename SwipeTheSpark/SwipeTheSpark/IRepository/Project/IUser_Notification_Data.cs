using SwipeTheSpark.Models.Project;

namespace SwipeTheSpark.IRepository.Project
{
    public interface IUser_Notification_Data
    {
        public List<dynamic> CreateUpdate_User_NotificationDetails(User_Notification_DTO model);
        public int SendFireBaseNotification(User_Notification_DTO model);
        public List<dynamic> Get_User_NotificationDetails(User_Notification_DTO model);
    }
}