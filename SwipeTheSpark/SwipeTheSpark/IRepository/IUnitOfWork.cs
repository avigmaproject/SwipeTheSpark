using SwipeTheSpark.IRepository.Avigma;
using SwipeTheSpark.IRepository.Project;

namespace SwipeTheSpark.IRepository
{
    public interface IUnitOfWork
    {
        IEmailTemplate emailTemplate { get; }
        IUser_Admin_Master_Data user_Admin_Master_Data { get; }
        IUserMaster_Data userMaster_Data { get; }
        IUser_Comments_Reply_Data user_Comments_Reply_Data { get; }
        IUser_Favorite_Data user_Favorite_Data { get; }
        IUser_Followers_Data user_Followers_Data { get; }
        IUser_Like_Data user_Like_Data { get; }
        IUser_Post_Comments_Data user_Post_Comments_Data { get; }
        INotification_Data notification_Data { get; }
        IUser_Notification_Data user_Notiffication_Data { get; }
        ITwitter_Trend_Data twitter_Trend_Data { get; }
        IDebate_Master_Data debate_Master_Data { get; }
    }
}
