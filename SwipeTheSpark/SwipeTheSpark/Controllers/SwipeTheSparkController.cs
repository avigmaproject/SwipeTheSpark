using SwipeTheSpark.IRepository;
using SwipeTheSpark.Models.Avigma;
using SwipeTheSpark.Models.Project;
using SwipeTheSpark.Repository.Avigma;
using SwipeTheSpark.Repository.Lib;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Web.Helpers;
using static System.Net.Mime.MediaTypeNames;
using System.Text.Json;
using SwipeTheSpark.Repository.Project;
using RestSharp;

namespace SwipeTheSpark.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class SwipeTheSparkController : BaseController
    {
        Log log = new Log();
        private readonly IUnitOfWork _uof;
        private readonly IConfiguration _configuration;
        public SwipeTheSparkController(IUnitOfWork uof, IConfiguration configuration)
        {
            _uof = uof;
            _configuration = configuration;
        }

        [HttpPost]
        [AllowAnonymous]
        [Authorize]
        [Route("Log")]
        public IActionResult Index()
        {
            log.logDebugMessage("Log Message from Debug Method");
            log.logErrorMessage("Log Message from Error Method");
            log.logInfoMessage("Log Message from Info Method");

            return Ok();

       }


        [Route("GetHomeData")]
        [HttpPost]
        [Authorize]
        [AllowAnonymous]
        public async Task<dynamic> HomeData(UserMaster_DTO_Input InputData)
        {

            //UserMaster_DTO userMaster = new UserMaster_DTO();
            InputData.User_PkeyID = LoggedInUserId;
            InputData.UserID = LoggedInUserId;
            InputData.Type = InputData.Type;

            var result = _uof.userMaster_Data.Get_LoginUserDetails(InputData);
            return result;
        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("CreateUpdateUserMasterData")]
        public async Task<List<dynamic>> AddUserMasterData(UserMaster_DTO Data)
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                //Data.User_PkeyID = LoggedInUserId;
                Data.UserID = LoggedInUserId;
                var Output = await Task.Run(() => _uof.userMaster_Data.AddUserMaster_Data(Data));

                return Output;
           }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }

        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("GetUserMasterData")]
        public async Task<List<dynamic>> GetUserMasterData(UserMaster_DTO_Input InputData)
        {

            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                InputData.UserID = LoggedInUserId;

                var result = await Task.Run(() => _uof.userMaster_Data.Get_UserMasterDetails(InputData));
                objdynamicobj.Add(result);

                return objdynamicobj;

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }

        }



        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("CreateUpdateUserCommentsReplyData")]
        public async Task<List<dynamic>> AddUserCommentsReplyData(User_Comments_Reply_DTO Data)
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                Data.UserID = LoggedInUserId;
                var Output = await Task.Run(() => _uof.user_Comments_Reply_Data.AddUpdateUser_Comments_Reply_Data(Data));

                return Output;

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }

        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("GetUserCommentsReplyData")]
        public async Task<List<dynamic>> GetUserCommentsReplyData(User_Comments_Reply_DTO_Input InputData)
        {

            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                InputData.UserID = LoggedInUserId;

                var output = await Task.Run(() => _uof.user_Comments_Reply_Data.Get_User_Comments_ReplyDetailsDTO(InputData));
                objdynamicobj.Add(output);

                return objdynamicobj;

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }

        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("CreateUpdateUserFavoriteData")]
        public async Task<List<dynamic>> AddUserFavoriteData(User_Favorite_DTO Data)
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                Data.UserID = LoggedInUserId;
                var Output = await Task.Run(() => _uof.user_Favorite_Data.AddUpdateUser_Favorite_Data(Data));

                return Output;

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }

        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("GetUserFavoriteData")]
        public async Task<List<dynamic>> GetUserFavoriteData(User_Favorite_DTO_Input InputData)
        {

            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                InputData.UserID = LoggedInUserId;

                var output = await Task.Run(() => _uof.user_Favorite_Data.Get_User_FavoriteDetailsDTO(InputData));
                objdynamicobj.Add(output);

                return objdynamicobj;

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }

        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("CreateUpdateUserFollowersData")]
        public async Task<List<dynamic>> AddUserFollowersData(User_Followers_DTO Data)
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                Data.UserID = LoggedInUserId;
                var Output = await Task.Run(() => _uof.user_Followers_Data.AddUpdateUser_Followers_Data(Data));

                return Output;

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }

        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("GetUserFollowersData")]
        public async Task<List<dynamic>> GetUserFollowersData(User_Followers_DTO_Input InputData)
        {

            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                InputData.UserID = LoggedInUserId;

                var output = await Task.Run(() => _uof.user_Followers_Data.Get_User_FollowersDetailsDTO(InputData));
                objdynamicobj.Add(output);

                return objdynamicobj;

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }

        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("CreateUpdateDebateMaster")]
        public async Task<List<dynamic>> CreateUpdateDebateMasterData(Debate_Master_DTO Data)
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                Data.UserID = LoggedInUserId;
                var Output = await Task.Run(() => _uof.debate_Master_Data.AddUpdate_Debate_Master_Data(Data));

                return Output;

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }

        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("GetDebateMasterData")]
        public async Task<List<dynamic>> GetDebateMasterData(Debate_Master_DTO_Input InputData)
        {

            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                InputData.UserID = LoggedInUserId;

                var output = await Task.Run(() => _uof.debate_Master_Data.Get_Debate_Master_DetailsDTO(InputData));
                objdynamicobj.Add(output);

                return objdynamicobj;

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }

        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("CreateUpdateUserLikeData")]
        public async Task<List<dynamic>> AddUserLikeData(User_Like_DTO Data)
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                Data.UL_User_PkeyID = LoggedInUserId;
                Data.UserID = LoggedInUserId;
                var Output = await Task.Run(() => _uof.user_Like_Data.AddUpdateUser_Like_Data(Data));

                return Output;

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }

        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("GetUserLikeData")]
        public async Task<List<dynamic>> GetUserLikeData(User_Like_DTO_Input InputData)
        {

            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                InputData.UserID = LoggedInUserId;

                var output = await Task.Run(() => _uof.user_Like_Data.Get_User_LikeDetailsDTO(InputData));
                objdynamicobj.Add(output);

                return objdynamicobj;

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }

        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("CreateUpdateUserPostCommentsData")]
        public async Task<List<dynamic>> AddUserPostCommentsData(User_Post_Comments_DTO Data)
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                Data.UserID = LoggedInUserId;
                var Output = await Task.Run(() => _uof.user_Post_Comments_Data.AddUpdateUser_Post_Comments_Data(Data));

                return Output;

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }

        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("GetUserPostCommentsData")]
        public async Task<List<dynamic>> GetUserPostCommentsData(User_Post_Comments_DTO_Input InputData)
        {

            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                InputData.UserID = LoggedInUserId;

                var output = await Task.Run(() => _uof.user_Post_Comments_Data.Get_User_Post_CommentsDetailsDTO(InputData));
                objdynamicobj.Add(output);

                return objdynamicobj;

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }

        }


        [HttpPost]
        [Route("ForGotPassword")]
        public async Task<List<dynamic>> ForGotPassword(UserLogin user_Child_DTO)
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                GetSetUser getSetUser = new GetSetUser(_configuration);
                //System.IO.Stream body = System.Web.HttpContext.Current.Request.InputStream;
                //System.Text.Encoding encoding = System.Web.HttpContext.Current.Request.ContentEncoding;
                //System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
                //string s = reader.ReadToEnd();
                //log.logDebugMessage("----------ForGotPassword Start--------------");
                //log.logDebugMessage(s);
                //log.logDebugMessage("----------ForGotPassword End--------------");
                //var user_Child_DTO = JsonConvert.DeserializeObject<UserLogin>(s);


                var output = await Task.Run(() => getSetUser.GetForGetPassword(user_Child_DTO));

                return output;

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }
        }



        [HttpPost]
        [Route("GetUserViryficationDetails")]
        public async Task<List<dynamic>> GetUserViryficationDetails(UserVerificationMaster_DTO Data)
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                UserVerificationMaster_Data userVerificationMaster_Data = new UserVerificationMaster_Data(_configuration);
                //System.IO.Stream body = System.Web.HttpContext.Current.Request.InputStream;
                //System.Text.Encoding encoding = System.Web.HttpContext.Current.Request.ContentEncoding;
                //System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
                //string s = reader.ReadToEnd();
                //log.logDebugMessage("----------GetUserViryficationDetails Start--------------");
                //log.logDebugMessage(s);
                //log.logDebugMessage("----------GetUserViryficationDetails End--------------");
                ////log.logDebugMessage(s);
                //var Data = JsonConvert.DeserializeObject<UserVerificationMaster_DTO>(s);
                //Data.Type = 1;

                var output = await Task.Run(() => userVerificationMaster_Data.Check_User(Data));
                return output;

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }

        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("sendNotification")]
        public async Task<IActionResult> SendNotification(NotificationMasterDTO notificationModel)
        {
            var output = await Task.Run(() => _uof.notification_Data.SendNotification(notificationModel));
            return Ok(output);
        }


        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("sendNotifications")]
        public async Task<string> SendNotification(NotificationMasterTokenDTO notification)
        {
            var output = await Task.Run(() => _uof.notification_Data.SendNotificationToken(notification));

            return output;
        }




        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("CreateUpdateUserNotification")]
        public async Task<List<dynamic>> AddNotificationData(User_Notification_DTO Data)
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                Data.UserID = LoggedInUserId;
                var Output = await Task.Run(() => _uof.user_Notiffication_Data.CreateUpdate_User_NotificationDetails(Data));

                return Output;

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }

        }


        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("GetUserNotification")]
        public async Task<List<dynamic>> GetUserNotification(User_Notification_DTO InputData)
        {

            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                InputData.UserID = LoggedInUserId;

                var output = await Task.Run(() => _uof.user_Notiffication_Data.Get_User_NotificationDetails(InputData));
                objdynamicobj.Add(output);

                return objdynamicobj;

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }

        }
        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("CreateUpdate_TwitterTrend")]
        public async Task<List<dynamic>> CreateUpdate_TwitterTrend()
        {
            List<dynamic> objdynamic = new List<dynamic>();


            var output = await Task.Run(() => _uof.twitter_Trend_Data.CreateUpdate_Twitter_TrendDetails());
            objdynamic.Add(output);

            return objdynamic;
        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("Get_Twitter_Trend")]
        public async Task<List<dynamic>> Get_Twitter_Trend(Trend_DTO_Input InputData)
        {

            List<dynamic> objdynamic = new List<dynamic>();

            try
            {
                InputData.UserID = LoggedInUserId;

                var output = await Task.Run(() => _uof.twitter_Trend_Data.Get_Twitter_Trend_Data(InputData));
                objdynamic.Add(output);

                return objdynamic;

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                return objdynamic;
            }

        }
    }

}
