using SwipeTheSpark.Models.Avigma;
using CorePush.Google;
using Microsoft.Extensions.Options;
using static SwipeTheSpark.Models.Avigma.GoogleNotification;
using System.Net.Http.Headers;
using SwipeTheSpark.IRepository;
using CorePush.Apple;
using SwipeTheSpark.Repository.Lib;
using Newtonsoft.Json;
using System.Net;
using System.Data;
using System.Data.SqlClient;
using SwipeTheSpark.Models.Project;
using SwipeTheSpark.Repository.Lib.FireBase;
using SwipeTheSpark.Repository.Project;

namespace SwipeTheSpark.Repository.Avigma
{
    public class Notification_Data : INotification_Data
    {
        Log log = new Log();
        private readonly IConfiguration _configuration;
        public string ConnectionString { get; }
        public Notification_Data()
        {
        }
        public Notification_Data(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("Conn_dBcon");
        }


        public IDbConnection Connection
        {
            get { return new SqlConnection(ConnectionString); }
        }

        public async Task<ResponseModel> SendNotification(NotificationMasterDTO notificationModel)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                if (notificationModel.IsAndroiodDevice)
                {
                    /* FCM Sender (Android Device) */
                    FcmSettings settings = new FcmSettings()
                    {
                        SenderId = _configuration["SenderId"],
                        ServerKey = _configuration["ServerKey"]
                    };
                    HttpClient httpClient = new HttpClient();

                    string authorizationKey = string.Format("keyy={0}", settings.ServerKey);
                    string deviceToken = notificationModel.DeviceId;

                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorizationKey);
                    httpClient.DefaultRequestHeaders.Accept
                            .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    DataPayload dataPayload = new DataPayload();
                    dataPayload.Title = notificationModel.Title;
                    dataPayload.Body = notificationModel.Body;

                    GoogleNotification notification = new GoogleNotification();
                    notification.Data = dataPayload;
                    notification.Notification = dataPayload;

                    var fcm = new FcmSender(settings, httpClient);
                    var fcmSendResponse = await fcm.SendAsync(deviceToken, notification);

                    if (fcmSendResponse.IsSuccess())
                    {
                        response.IsSuccess = true;
                        response.Message = "Notification sent successfully";
                        return response;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = fcmSendResponse.Results[0].Error;
                        return response;
                    }
                }
                else
                {
                    /* Code here for APN Sender (iOS Device) */
                    //var apn = new ApnSender(apnSettings, httpClient);
                    //await apn.SendAsync(notification, deviceToken);
                }
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Something went wrong";
                return response;

                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);

            }
        }

        public string Send_Notification(User_Notification_DTO user_Notification_DTO)
        {
            var result = "-1";
            string message = string.Empty, msgtitle = string.Empty, UserToken = string.Empty, Username = string.Empty, User_Name_Post = string.Empty;
            Int64? User_PkeyID = 0, UP_UserID = 0, UPC_User_PkeyID = 0;
            NotificationGetData notificationGet = new NotificationGetData(_configuration);
            User_Notification_Data user_Notification_Data = new User_Notification_Data(_configuration);
            //User_Post_DTO user_Post_DTO = new User_Post_DTO();
            User_Post_Notification_DTO user_Post_DTO = new User_Post_Notification_DTO();
            try
            {
                #region comment
                //if (user_Notification_DTO.NT_UP_PKeyID != null)
                //{
                //    user_Post_DTO.UP_PKeyID = Convert.ToInt64(user_Notification_DTO.NT_UP_PKeyID);
                //    user_Post_DTO.Type = 1;
                //    user_Post_DTO.UL_PKeyID = user_Notification_DTO.NT_UL_PKeyID;
                //}

                //else if (user_Notification_DTO.NT_US_PKeyID != null)
                //{
                //    user_Post_DTO.US_PKeyID = Convert.ToInt64(user_Notification_DTO.NT_US_PKeyID);
                //    user_Post_DTO.Type = 2;
                //    user_Post_DTO.UPC_PkeyID = user_Notification_DTO.NT_UPC_PkeyID;
                //}
                //else if (user_Notification_DTO.NT_SP_PKeyID != null)
                //{
                //    user_Post_DTO.SUB_PKeyID = Convert.ToInt64(user_Notification_DTO.NT_SP_PKeyID);
                //    user_Post_DTO.Type =3;
                //}
                //else if (user_Notification_DTO.NT_FLL_PKeyID != null)
                //{
                //    user_Post_DTO.FLL_PKeyID = Convert.ToInt64(user_Notification_DTO.NT_FLL_PKeyID);
                //    user_Post_DTO.Type = 4;
                //}
                #endregion

                switch (user_Notification_DTO.NT_C_L)
                {
                    case 1:
                        {

                            user_Post_DTO.UPC_PkeyID = user_Notification_DTO.NT_UPC_PkeyID;
                            user_Post_DTO.UP_PKeyID = user_Notification_DTO.NT_UP_PKeyID;
                            user_Post_DTO.Type = 1;
                            user_Post_DTO.UserID = user_Notification_DTO.UserID;
                            break;
                        }
                    case 2:
                        {
                            user_Post_DTO.UP_PKeyID = user_Notification_DTO.NT_UP_PKeyID;
                            user_Post_DTO.UL_PKeyID = user_Notification_DTO.NT_UL_PKeyID;
                            user_Post_DTO.Type = 1;
                            user_Post_DTO.UserID = user_Notification_DTO.UserID;
                            break;

                        }
                    case 3:
                        {

                            user_Post_DTO.UPC_PkeyID = user_Notification_DTO.NT_UPC_PkeyID;
                            user_Post_DTO.UPR_PkeyID = user_Notification_DTO.NT_UPR_PkeyID;
                            user_Post_DTO.Type = 2;
                            user_Post_DTO.UserID = user_Notification_DTO.UserID;
                            break;
                        }
                        //case 4:
                        //    {
                        //        user_Post_DTO.FLL_PKeyID = Convert.ToInt64(user_Notification_DTO.NT_FLL_PKeyID);
                        //        user_Post_DTO.Type = 4;
                        //        break;
                        //    }
                        //case 5:
                        //    {
                        //        user_Post_DTO.User_Creator_PkeyID = Convert.ToInt64(user_Notification_DTO.NT_Creator_PKeyID);
                        //        user_Post_DTO.Type = 4;
                        //        break;
                        //    }
                        //case 7:
                        //    {
                        //        user_Post_DTO.SUB_PKeyID = Convert.ToInt64(user_Notification_DTO.NT_SP_PKeyID);
                        //        user_Post_DTO.Type = 7;
                        //        break;
                        //    }
                        //case 9:
                        //    {
                        //        user_Post_DTO.TP_PKeyID = Convert.ToInt64(user_Notification_DTO.NT_TP_PKeyID);
                        //        user_Post_DTO.Type = 9;
                        //        break;
                        //    }
                        //case 10:
                        //    {
                        //        user_Post_DTO.UCD_User_PkeyID = Convert.ToInt64(user_Notification_DTO.NT_UCD_User_PkeyID);
                        //        user_Post_DTO.Type = 10;
                        //        break;
                        //    }
                        //case 11:
                        //    {
                        //        user_Post_DTO.UPG_PkeyID = Convert.ToInt64(user_Notification_DTO.NT_UPG_PkeyID);
                        //        user_Post_DTO.Type = 11;
                        //        break;
                        //    }
                        //case 12:
                        //    {

                        //        user_Post_DTO.UPG_PkeyID = Convert.ToInt64(user_Notification_DTO.NT_UPG_PkeyID);
                        //        user_Post_DTO.Type = 12;
                        //        break;
                        //    }
                }

                user_Notification_DTO.NT_IsActive = true;
                //DataSet ds = Get_UserDetailsByPost_Story(user_Post_DTO);
                DataSet ds = Get_User_Notification_DetailsByPost_Story(user_Post_DTO);

                if (ds.Tables.Count > 0)
                {

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        UserToken = ds.Tables[0].Rows[i]["User_Token_val"].ToString();
                        Username = ds.Tables[0].Rows[i]["User_Name"].ToString();
                        if (!string.IsNullOrWhiteSpace(ds.Tables[0].Rows[i]["User_PkeyID"].ToString()))
                        {
                            User_PkeyID = Convert.ToInt64(ds.Tables[0].Rows[i]["User_PkeyID"].ToString());

                        }

                        if (!string.IsNullOrWhiteSpace(UserToken))
                        {
                            switch (user_Notification_DTO.NT_C_L)
                            {
                                case 1:
                                    {
                                        if (!string.IsNullOrWhiteSpace(ds.Tables[0].Rows[i]["UP_UserID"].ToString()))
                                        {
                                            UP_UserID = Convert.ToInt64(ds.Tables[0].Rows[i]["UP_UserID"].ToString());
                                            user_Notification_DTO.NT_UserID = UP_UserID;
                                        }
                                        User_Name_Post = ds.Tables[0].Rows[i]["User_Name_Post"].ToString();
                                        //message = "Notification Received for Comments "+ User_Name_Post;
                                        //msgtitle = "New Notification Received for Comments  " + User_Name_Post;
                                        message = User_Name_Post + " commented on your post";
                                        msgtitle = User_Name_Post + " commented on your post";
                                        user_Notification_DTO.NT_Description = message;
                                        user_Notification_DTO.NT_Name = msgtitle;
                                        user_Notification_DTO.Type = 1;
                                        break;
                                    }
                                case 2:
                                    {
                                        if (!string.IsNullOrWhiteSpace(ds.Tables[0].Rows[i]["UP_UserID"].ToString()))
                                        {
                                            UP_UserID = Convert.ToInt64(ds.Tables[0].Rows[i]["UP_UserID"].ToString());
                                            user_Notification_DTO.NT_UserID = UP_UserID;
                                        }
                                        User_Name_Post = ds.Tables[0].Rows[i]["User_Name_Post"].ToString();
                                        //message = "Notification Received for Like  "+ User_Name_Post;
                                        //msgtitle = "New Notification Received for Like " + User_Name_Post ;

                                        message = User_Name_Post + " liked your post";
                                        msgtitle = User_Name_Post + " liked your post";
                                        user_Notification_DTO.NT_Description = message;
                                        user_Notification_DTO.NT_Name = msgtitle;
                                        user_Notification_DTO.Type = 1;
                                        break;
                                    }
                                case 3:
                                    {
                                        if (!string.IsNullOrWhiteSpace(ds.Tables[0].Rows[i]["UPC_User_PkeyID"].ToString()))
                                        {
                                            UPC_User_PkeyID = Convert.ToInt64(ds.Tables[0].Rows[i]["UPC_User_PkeyID"].ToString());
                                            user_Notification_DTO.NT_UserID = UPC_User_PkeyID;
                                        }
                                        User_Name_Post = ds.Tables[0].Rows[i]["User_Name_Post"].ToString();
                                        //message = "Notification Received for Comments "+ User_Name_Post;
                                        //msgtitle = "New Notification Received for Comments  " + User_Name_Post;
                                        message = User_Name_Post + " Review on your Comment";
                                        msgtitle = User_Name_Post + " Review on your Comment";
                                        user_Notification_DTO.NT_Description = message;
                                        user_Notification_DTO.NT_Name = msgtitle;
                                        user_Notification_DTO.Type = 1;
                                        break;

                                    }
                                case 8:
                                case 4:
                                    {
                                        user_Notification_DTO.NT_UserID = User_PkeyID;



                                        if (user_Notification_DTO.NT_C_L == 4)
                                        {
                                            message = "Notification Received Follower Request  from " + Username;
                                            msgtitle = "Notification Received Follower Request from " + Username;
                                        }
                                        else if (user_Notification_DTO.NT_C_L == 8)
                                        {
                                            message = Username + "  subscribed to your profile";
                                            msgtitle = Username + "  subscribed to your profile";
                                        }
                                        user_Notification_DTO.NT_Description = message;
                                        user_Notification_DTO.NT_Name = msgtitle;
                                        user_Notification_DTO.Type = 1;
                                        break;
                                    }
                                case 5:
                                    {
                                        user_Notification_DTO.NT_UserID = User_PkeyID;
                                        if (user_Notification_DTO.User_Creator_IsVerfied == true)
                                        {
                                            message = "Creator Request Approved from Admin";
                                            msgtitle = "Creator Request Approved from Admin";
                                        }
                                        else
                                        {
                                            message = "Creator Request Rejected from Admin";
                                            msgtitle = "Creator Request Rejected from Admin";
                                        }

                                        user_Notification_DTO.NT_Description = message;
                                        user_Notification_DTO.NT_Name = msgtitle;
                                        user_Notification_DTO.Type = 1;
                                        break;
                                    }
                                case 6: // Live Streaming 
                                    {
                                        break;
                                    }
                                case 7: // Sponsor Accept Reject 
                                    {
                                        //message = "Notification Received for Sponsor ";
                                        //msgtitle = "New Notification Received Sponsor";
                                        user_Notification_DTO.NT_UserID = User_PkeyID;
                                        string Name = string.Empty, Description = string.Empty, Amount = string.Empty, From_otherUsername = string.Empty, Accept_Reject = string.Empty;
                                        Name = ds.Tables[0].Rows[i]["SP_Name"].ToString();
                                        Description = ds.Tables[0].Rows[i]["SP_Description"].ToString();
                                        From_otherUsername = ds.Tables[0].Rows[i]["From_otherUsername"].ToString();
                                        string User_MotiID = ds.Tables[0].Rows[i]["User_MotiID"].ToString();
                                        Amount = ds.Tables[0].Rows[i]["SP_Amount"].ToString();
                                        if (ds.Tables[0].Rows[i]["SP_IsAccept_Reject"].ToString() == "1")
                                        {
                                            Accept_Reject = "  accepted ";
                                        }
                                        else if (ds.Tables[0].Rows[i]["SP_IsAccept_Reject"].ToString() == "2")
                                        {
                                            Accept_Reject = "  declined ";
                                        }
                                        //  message = " Sponsor Request   " + Accept_Reject + " " + From_otherUsername + " " + Name + " " + Description + "For " + Amount;
                                        //    msgtitle = "New Notification Received  Sponsor Request " + Accept_Reject + " " + From_otherUsername + "For " + Name + " " + Amount;

                                        message = User_MotiID + " " + Accept_Reject + "  your sponsorship request";
                                        msgtitle = User_MotiID + " " + Accept_Reject + " your sponsorship request";

                                        user_Notification_DTO.NT_Description = message;
                                        user_Notification_DTO.NT_Name = msgtitle;
                                        user_Notification_DTO.Type = 1;
                                        break;
                                    }
                                case 9: //  Tips
                                    {
                                        user_Notification_DTO.NT_UserID = User_PkeyID;
                                        string UserTipName = ds.Tables[0].Rows[i]["User_TipName"].ToString();
                                        message = UserTipName + " sent you a tip";
                                        msgtitle = UserTipName + " sent you a tip";

                                        user_Notification_DTO.NT_Description = message;
                                        user_Notification_DTO.NT_Name = msgtitle;
                                        user_Notification_DTO.Type = 1;
                                        break;
                                    }
                                case 10: //  purchase on demand
                                    {
                                        user_Notification_DTO.NT_UserID = User_PkeyID;
                                        string userPur = ds.Tables[0].Rows[i]["user_Pur"].ToString();
                                        string Title = ds.Tables[0].Rows[i]["UOD_Title"].ToString();
                                        message = userPur + " purchased On-Demand content " + Title;
                                        msgtitle = userPur + " purchased On-Demand content " + Title;

                                        user_Notification_DTO.NT_Description = message;
                                        user_Notification_DTO.NT_Name = msgtitle;
                                        user_Notification_DTO.Type = 1;
                                        break;
                                    }
                                case 11: //  Subscribe 
                                    {
                                        user_Notification_DTO.NT_UserID = User_PkeyID;
                                        string User_MotiID = ds.Tables[0].Rows[i]["User_MotiID"].ToString();

                                        message = User_MotiID + " subscribed to your profile";
                                        msgtitle = User_MotiID + " subscribed to your profile";

                                        user_Notification_DTO.NT_Description = message;
                                        user_Notification_DTO.NT_Name = msgtitle;
                                        user_Notification_DTO.Type = 1;
                                        break;
                                    }
                                case 12: //   Sponspor
                                    {

                                        user_Notification_DTO.NT_UserID = User_PkeyID;
                                        string User_MotiID = ds.Tables[0].Rows[i]["User_MotiID"].ToString();
                                        message = User_MotiID + " has paid the sponsorship request";
                                        msgtitle = User_MotiID + " has paid the sponsorship request";
                                        user_Notification_DTO.Type = 1;
                                        user_Notification_DTO.NT_Description = message;
                                        user_Notification_DTO.NT_Name = msgtitle;
                                        break;
                                    }
                            }
                            notificationGet.SendNotification(UserToken, message, msgtitle, "1");


                            user_Notification_Data.CreateUpdate_User_NotificationDetails(user_Notification_DTO);

                        }
                        else
                        {
                            log.logInfoMessage("No UserToken Found " + User_PkeyID);
                            user_Notification_DTO.Type = 1;
                            user_Notification_DTO.NT_UserID = User_PkeyID;
                            user_Notification_Data.CreateUpdate_User_NotificationDetails(user_Notification_DTO);
                        }

                    }
                }
                else
                {
                    log.logErrorMessage("No Taable Data Found in Notification");
                }

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
            }

            return result;


        }

        public DataSet Get_UserDetailsByPost_Story(User_Post_DTO model)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Get_UserDetailsByPost_Story", (SqlConnection)Connection);
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("@UP_PKeyID", model.UP_PKeyID);
                cmd.Parameters.AddWithValue("@US_PKeyID", model.US_PKeyID);
                cmd.Parameters.AddWithValue("@UL_PKeyID", model.UL_PKeyID);
                cmd.Parameters.AddWithValue("@UPC_PkeyID", model.UPC_PkeyID);
                //cmd.Parameters.AddWithValue("@User_Creator_PkeyID", model.User_Creator_PkeyID);
                cmd.Parameters.AddWithValue("@SUB_PKeyID", model.SUB_PKeyID);
                cmd.Parameters.AddWithValue("@FLL_PKeyID", model.FLL_PKeyID);
                //cmd.Parameters.AddWithValue("@TP_PKeyID", model.TP_PKeyID);
                //cmd.Parameters.AddWithValue("@UCD_User_PkeyID", model.UCD_User_PkeyID);
                cmd.Parameters.AddWithValue("@UPG_PkeyID", model.UPG_PkeyID);
                cmd.Parameters.AddWithValue("@UserID", model.UP_UserID);
                cmd.Parameters.AddWithValue("@WhereClause", model.WhereClause);
                cmd.Parameters.AddWithValue("@PageNumber", model.PageNumber);
                cmd.Parameters.AddWithValue("@NoofRows", model.NoofRows);
                cmd.Parameters.AddWithValue("@Orderby", model.Orderby);
                cmd.Parameters.AddWithValue("@Type", model.Type);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }

            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
            }



            return ds;
        }
        public async Task<string> SendNotificationToken(NotificationMasterTokenDTO notification)
        {
            var result = "-1";
            try
            {

                WebRequest httpWebRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                httpWebRequest.Method = "post";

                string serverKey = _configuration["ServerKey"];
                string senderId = _configuration["SenderId"];

                httpWebRequest.Headers.Add(string.Format("Authorization: key={0}", serverKey));
                httpWebRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                httpWebRequest.ContentType = "application/json";
                var payload = new
                {
                    to = notification.userToken,

                    priority = "high",
                    content_available = true,
                    notification = new
                    {
                        body = notification.message,
                        title = notification.msgtitle,
                        badge = 1,

                    },
                    data = new
                    {
                        key1 = notification.data,

                    }

                };

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(payload);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
                log.logInfoMessage("Notifcation Status---------->" + notification.userToken);
                log.logInfoMessage(result);

            }
            catch (Exception ex)
            {

                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);
            }
            return result;

        }

        public DataSet Get_User_Notification_DetailsByPost_Story(User_Post_Notification_DTO model)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Get_Notification_Data", (SqlConnection)Connection);
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("@UP_PKeyID", model.UP_PKeyID);
                cmd.Parameters.AddWithValue("@UL_PKeyID", model.UL_PKeyID);
                cmd.Parameters.AddWithValue("@UPC_PKeyID", model.UPC_PkeyID);
                cmd.Parameters.AddWithValue("@UserID", model.UserID);
                cmd.Parameters.AddWithValue("@WhereClause", model.WhereClause);
                cmd.Parameters.AddWithValue("@PageNumber", model.PageNumber);
                cmd.Parameters.AddWithValue("@NoofRows", model.NoofRows);
                cmd.Parameters.AddWithValue("@Orderby", model.Orderby);
                cmd.Parameters.AddWithValue("@Type", model.Type);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }

            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
            }



            return ds;
        }

    }
}
