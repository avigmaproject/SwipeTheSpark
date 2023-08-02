using System.Data;
using System.Data.SqlClient;
using SwipeTheSpark.Data;
using SwipeTheSpark.IRepository;
using SwipeTheSpark.Models;
using Microsoft.Extensions.Options;
using SwipeTheSpark.IRepository.Avigma;
using SwipeTheSpark.IRepository.Project;
using SwipeTheSpark.Repository.Project;

namespace SwipeTheSpark.Repository.Avigma
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IConfiguration _configuration;

        public UnitOfWork(IConfiguration configuration)
        {
            _configuration = configuration;

        }
        public IEmailTemplate emailTemplate => new EmailTemplate(_configuration);
        public IUser_Admin_Master_Data user_Admin_Master_Data => new User_Admin_Master_Data(_configuration);
        public IUserMaster_Data userMaster_Data => new UserMaster_Data();

        public IUser_Comments_Reply_Data user_Comments_Reply_Data => new User_Comments_Reply_Data(_configuration);

        public IUser_Favorite_Data user_Favorite_Data => new User_Favorite_Data(_configuration);

        public IUser_Followers_Data user_Followers_Data => new User_Followers_Data(_configuration);

        public IUser_Like_Data user_Like_Data => new User_Like_Data(_configuration);

        public IUser_Post_Comments_Data user_Post_Comments_Data => new User_Post_Comments_Data(_configuration);

        public INotification_Data notification_Data => new Notification_Data(_configuration);
        public IUser_Notification_Data user_Notiffication_Data => new User_Notification_Data(_configuration);
        public ITwitter_Trend_Data twitter_Trend_Data => new Twitter_Trend_Data();
        public IDebate_Master_Data debate_Master_Data => new Debate_Master_Data(_configuration);
    }
}
