﻿using SwipeTheSpark.Repository.Lib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using SwipeTheSpark.Repository.Lib.Security;
using SwipeTheSpark.IRepository.Avigma;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data.SqlClient;
using SwipeTheSpark.Models.Avigma;
using SwipeTheSpark.Data;
using System.Security.Claims;
using SwipeTheSpark.Models;
using Newtonsoft.Json.Linq;

namespace SwipeTheSpark.Repository.Avigma
{
    public class UserMaster_Data : IUserMaster_Data
    {
        Log log = new Log();
        SecurityHelper securityHelper = new SecurityHelper();
        ObjectConvert obj = new ObjectConvert();

        private readonly IConfiguration _configuration;
        public string ConnectionString { get; }
        public UserMaster_Data()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            ConnectionString = configuration.GetConnectionString("Conn_dBcon");
        }
        public UserMaster_Data(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("Conn_dBcon");
        }


        public IDbConnection Connection
        {
            get { return new SqlConnection(ConnectionString); }
        }


        private List<dynamic> AddUpdateUserMaster_Data(UserMaster_DTO model)
        {
            string msg = string.Empty;

            List<dynamic> objData = new List<dynamic>();


            //            string procedureName = "CreateUpdate_UserMaster";
            //            SqlParameter[] parameters = new SqlParameter[]
            //            {
            //new SqlParameter("@User_PkeyID", model.User_PkeyID),
            //new SqlParameter("@User_Name", model.User_Name),
            //new SqlParameter("@User_Email", model.User_Email),
            //new SqlParameter("@User_Password", model.User_Password),
            //new SqlParameter("@User_Phone", model.User_Phone),
            //new SqlParameter("@User_Address", model.User_Address),
            //new SqlParameter("@User_City", model.User_City),
            //new SqlParameter("@User_Country", model.User_Country),
            //new SqlParameter("@User_Zip", model.User_Zip),
            //new SqlParameter("@User_DOB", model.User_DOB),
            //new SqlParameter("@User_Gender", model.User_Gender),
            //new SqlParameter("@User_Type", model.User_Type),
            //new SqlParameter("@User_Image_Path", model.User_Image_Path),
            //new SqlParameter("@User_MacID", model.User_MacID),
            //new SqlParameter("@User_IsVerified", model.User_IsVerified),
            //new SqlParameter("@User_IsActive", model.User_IsActive),
            //new SqlParameter("@User_IsDelete", model.User_IsDelete),
            //new SqlParameter("@User_latitude", model.User_latitude),
            //new SqlParameter("@User_longitude", model.User_longitude),
            //new SqlParameter("@User_PkeyID_Master", model.User_PkeyID_Master),
            //new SqlParameter("@User_OTP", model.User_OTP),
            //new SqlParameter("@User_IsActive_Prof", model.User_IsActive_Prof),
            //new SqlParameter("@User_Token_val", model.User_Token_val),
            //new SqlParameter("@User_Login_Type", model.User_Login_Type),
            //new SqlParameter("@User_Image_Base", model.User_Image_Base),
            //new SqlParameter("@User_LastName", model.User_LastName),
            //new SqlParameter("@User_Company", model.User_Company),
            //new SqlParameter("@User_Language", model.User_Language),
            //new SqlParameter("@Type", model.Type),
            //new SqlParameter("@UserID", model.UserID),

            //new SqlParameter("@User_Occupation", model.User_Occupation),
            //new SqlParameter("@User_Store_URL", model.User_Store_URL),
            //new SqlParameter("@User_FB_URL", model.User_FB_URL),
            //new SqlParameter("@User_Insta_URL", model.User_Insta_URL),
            //new SqlParameter("@User_YouTube_URL", model.User_YouTube_URL),

            //new SqlParameter("@User_FB_ID", model.User_FB_ID),
            //new SqlParameter("@User_Gmail_ID", model.User_Gmail_ID),
            //new SqlParameter("@User_iOS_ID", model.User_iOS_ID),
            //new SqlParameter("@User_Bio", model.User_Bio),
            //new SqlParameter("@User_Image_Name", model.User_Image_Name),
            //new SqlParameter("@User_Firebase_UID", model.User_Firebase_UID),
            //        };
            //            Int64 OutPutID;
            //            int outPut;
            //            objData = DatabaseHelper.ExecuteStoredProcedure(procedureName, parameters,out OutPutID,out outPut);


            using (IDbConnection con = Connection)
            {
                if (Connection.State == ConnectionState.Closed) con.Open();

                try
                {
                    SqlCommand cmd = new SqlCommand("CreateUpdate_UserMaster", (SqlConnection)con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@User_PkeyID", model.User_PkeyID);
                    cmd.Parameters.AddWithValue("@User_Name", model.User_Name);
                    cmd.Parameters.AddWithValue("@User_Email", model.User_Email);
                    cmd.Parameters.AddWithValue("@User_Password", model.User_Password);
                    cmd.Parameters.AddWithValue("@User_Phone", model.User_Phone);
                    cmd.Parameters.AddWithValue("@User_Address", model.User_Address);
                    cmd.Parameters.AddWithValue("@User_City", model.User_City);
                    cmd.Parameters.AddWithValue("@User_Country", model.User_Country);
                    cmd.Parameters.AddWithValue("@User_Zip", model.User_Zip);
                    cmd.Parameters.AddWithValue("@User_DOB", model.User_DOB);
                    cmd.Parameters.AddWithValue("@User_Gender", model.User_Gender);
                    cmd.Parameters.AddWithValue("@User_Type", model.User_Type);
                    cmd.Parameters.AddWithValue("@User_Image_Path", model.User_Image_Path);
                    cmd.Parameters.AddWithValue("@User_MacID", model.User_MacID);
                    cmd.Parameters.AddWithValue("@User_IsVerified", model.User_IsVerified);
                    cmd.Parameters.AddWithValue("@User_IsActive", model.User_IsActive);
                    cmd.Parameters.AddWithValue("@User_IsDelete", model.User_IsDelete);
                    cmd.Parameters.AddWithValue("@User_latitude", model.User_latitude);
                    cmd.Parameters.AddWithValue("@User_longitude", model.User_longitude);
                    cmd.Parameters.AddWithValue("@User_PkeyID_Master", model.User_PkeyID_Master);
                    cmd.Parameters.AddWithValue("@User_OTP", model.User_OTP);
                    cmd.Parameters.AddWithValue("@User_IsActive_Prof", model.User_IsActive_Prof);
                    cmd.Parameters.AddWithValue("@User_Token_val", model.User_Token_val);
                    cmd.Parameters.AddWithValue("@User_Login_Type", model.User_Login_Type);
                    cmd.Parameters.AddWithValue("@User_Image_Base", model.User_Image_Base);
                    cmd.Parameters.AddWithValue("@User_LastName", model.User_LastName);
                    cmd.Parameters.AddWithValue("@User_Company", model.User_Company);
                    cmd.Parameters.AddWithValue("@User_Language", model.User_Language);
                    //cmd.Parameters.AddWithValue("@User_IPAddress", model.User_IPAddress);
                    cmd.Parameters.AddWithValue("@Type", model.Type);
                    cmd.Parameters.AddWithValue("@UserID", model.UserID);

                    cmd.Parameters.AddWithValue("@User_Occupation", model.User_Occupation);
                    cmd.Parameters.AddWithValue("@User_Store_URL", model.User_Store_URL);
                    cmd.Parameters.AddWithValue("@User_FB_URL", model.User_FB_URL);
                    cmd.Parameters.AddWithValue("@User_Insta_URL", model.User_Insta_URL);
                    cmd.Parameters.AddWithValue("@User_YouTube_URL", model.User_YouTube_URL);

                    cmd.Parameters.AddWithValue("@User_FB_ID", model.User_FB_ID);
                    cmd.Parameters.AddWithValue("@User_Gmail_ID", model.User_Gmail_ID);
                    cmd.Parameters.AddWithValue("@User_iOS_ID", model.User_iOS_ID);
                    cmd.Parameters.AddWithValue("@User_Bio", model.User_Bio);
                    cmd.Parameters.AddWithValue("@User_Image_Name", model.User_Image_Name);
                    cmd.Parameters.AddWithValue("@User_Firebase_UID", model.User_Firebase_UID);


                    SqlParameter User_PkeyID_Out = cmd.Parameters.AddWithValue("@User_PkeyID_Out", 0);
                    User_PkeyID_Out.Direction = ParameterDirection.Output;
                    SqlParameter ReturnValue = cmd.Parameters.AddWithValue("@ReturnValue", 0);
                    ReturnValue.Direction = ParameterDirection.Output;


                    cmd.ExecuteNonQuery();

                    if (Int32.Parse(User_PkeyID_Out.Value.ToString()) == 0)
                    {
                        objData.Add("Invalid Email !!");
                        objData.Add(0);
                    }
                    else
                    {
                        objData.Add(User_PkeyID_Out.Value);
                        objData.Add(ReturnValue.Value);
                    }
                }
                catch (Exception ex)
                {
                    log.logErrorMessage(ex.StackTrace);
                    log.logErrorMessage(ex.Message);
                    msg = ex.Message;
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
            return objData;
        }

        public List<dynamic> AddUserMaster_Data(UserMaster_DTO model)
        {
            ImageGenerator imageGenerator = new ImageGenerator();
            string imgPath = string.Empty;
            List<dynamic> objData = new List<dynamic>();
            try
            {
                if (model.Type == 6)
                {
                    if (!string.IsNullOrEmpty(model.User_Image_Base))
                    {
                        imgPath = imageGenerator.Base64ToImage(model.User_Image_Base);
                        model.User_Image_Path = imgPath;
                        model.User_Image_Base = string.Empty;
                    }
                }
                objData.Add(AddUpdateUserMaster_Data(model));
                if (model.Type == 6)
                {
                    objData.Add(imgPath);

                }
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);
            }
            return objData;
        }


        private List<dynamic> DeleteUserMaster(UserMaster_DTO model)
        {
            string msg = string.Empty;
            List<dynamic> objData = new List<dynamic>();

            using (IDbConnection con = Connection)
            {
                if (Connection.State == ConnectionState.Closed) con.Open();

                //string insertProcedure = "[Delete_User_Master]";

                //Dictionary<string, string> input_parameters = new Dictionary<string, string>();
                try
                {
                    SqlCommand cmd = new SqlCommand("Delete_User_Master", (SqlConnection)con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@User_PkeyID", model.User_PkeyID);
                    cmd.Parameters.AddWithValue("@Type", model.Type);
                    cmd.Parameters.AddWithValue("@UserID", model.UserID);

                    SqlParameter User_Pkey_Out = cmd.Parameters.AddWithValue("@User_Pkey_Out", 0);
                    User_Pkey_Out.Direction = ParameterDirection.Output;
                    SqlParameter ReturnValue = cmd.Parameters.AddWithValue("@ReturnValue", 0);
                    ReturnValue.Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    //msg = "Delete Success";
                    objData.Add(User_Pkey_Out.Value);
                    objData.Add(ReturnValue.Value);


                }
                catch (Exception ex)
                {
                    log.logErrorMessage(ex.StackTrace);
                    log.logErrorMessage(ex.Message);
                }
                return objData;

            }
        }

        public List<dynamic> DeleteUserMasterDetails(UserMaster_DTO model)
        {
            List<dynamic> objData = new List<dynamic>();
            try
            {
                objData = DeleteUserMaster(model);
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);
            }
            return objData;

        }


        public List<dynamic> ChangePassword(UserMaster_ChangePassword model)
        {
            List<dynamic> objData = new List<dynamic>();
            try
            {
                long User_PkeyID = 0;
                if (!string.IsNullOrEmpty(model.User_PkeyID))
                {
                    User_PkeyID = Convert.ToInt64(securityHelper.Decode(model.User_PkeyID));
                }
                UserMaster_DTO userMaster_DTO = new UserMaster_DTO();
                userMaster_DTO.Type = model.Type;
                userMaster_DTO.User_PkeyID = User_PkeyID;
                userMaster_DTO.User_Password = model.User_Password;

                objData = AddUpdateUserMaster_Data(userMaster_DTO);
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);
            }
            return objData;
        }

        public List<dynamic> ChangePasswordByEmail(UserMaster_ChangePassword model)
        {
            List<dynamic> objData = new List<dynamic>();
            try
            {



                switch (model.User_Type)
                {
                    case 1:
                        {
                            UserMaster_DTO userMaster_DTO = new UserMaster_DTO();
                            userMaster_DTO.Type = model.Type;
                            userMaster_DTO.User_Email = model.User_Email;
                            userMaster_DTO.User_Password = model.User_Password;
                            objData = AddUpdateUserMaster_Data(userMaster_DTO);
                            break;
                        }
                    case 2:
                        {
                            break;
                        }
                }

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);
            }
            return objData;
        }



        public List<dynamic> VerifyUserByEmail(UserMaster_DTO userMaster_DTO)
        {
            List<dynamic> objData = new List<dynamic>();
            try
            {
                userMaster_DTO.Type = 10;
                objData = AddUpdateUserMaster_Data(userMaster_DTO);

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);
            }
            return objData;
        }

        // Below are Done
        private DataSet Get_UserMaster(UserMaster_DTO_Input userMaster)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Get_UserMaster", (SqlConnection)Connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@User_PkeyID", userMaster.User_PkeyID);
                cmd.Parameters.AddWithValue("@User_PkeyID_Master", userMaster.User_PkeyID_Master);
                cmd.Parameters.AddWithValue("@Type", userMaster.Type);

                //cmd.Parameters.AddWithValue("@WhereClause", userMaster.WhereClause);
                //cmd.Parameters.AddWithValue("@PageNumber", userMaster.PageNumber);
                //cmd.Parameters.AddWithValue("@NoofRows", userMaster.NoofRows);
                //cmd.Parameters.AddWithValue("@Orderby", userMaster.Orderby);
                cmd.Parameters.AddWithValue("@UserID", userMaster.UserID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                throw;
            }
            return ds;

        }


        private DataSet Get_UserMasterDetail(UserMaster_DTO_Input userMaster)
        {
            DataSet ds = new DataSet();
            try
            {
                //SqlCommand cmd = new SqlCommand("Get_HomeData", (SqlConnection)Connection);
                SqlCommand cmd = new SqlCommand("Get_User_Home", (SqlConnection)Connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@User_PkeyID", userMaster.User_PkeyID);
                cmd.Parameters.AddWithValue("@Type", userMaster.Type);

                cmd.Parameters.AddWithValue("@WhereClause", userMaster.WhereClause);
                cmd.Parameters.AddWithValue("@PageNumber", userMaster.PageNumber);
                cmd.Parameters.AddWithValue("@NoofRows", userMaster.NoofRows);
                cmd.Parameters.AddWithValue("@Orderby", userMaster.Orderby);
                cmd.Parameters.AddWithValue("@UserID", userMaster.UserID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                throw;
            }
            return ds;

        }

        public List<UserMaster_DTO> Get_UserMasterDetailsDTO(UserMaster_DTO model)
        {
            //List<dynamic> objDynamic = new List<dynamic>();
            List<UserMaster_DTO> UserMaster = new List<UserMaster_DTO>();
            UserMaster_DTO_Input input = new UserMaster_DTO_Input();
            input.User_PkeyID = model.User_PkeyID;
            input.UserID = model.UserID;
            input.Type = 2;
            input.User_PkeyID_Master = 0;

            try
            {


                DataSet ds = Get_UserMaster(input);

                //if (ds.Tables.Count > 0)
                //{
                //    for (int i = 0; i < ds.Tables.Count; i++)
                //    {
                //        objDynamic.Add(obj.AsDynamicEnumerable(ds.Tables[i]));
                //    }

                //}

                var myEnumerableFeaprd = ds.Tables[0].AsEnumerable();
                UserMaster =
                (from item in myEnumerableFeaprd
                 select new UserMaster_DTO
                 {
                     User_PkeyID = item.Field<long>("User_PkeyID"),
                     //User_PkeyID_Master = item.Field<long?>("User_PkeyID_Master"),
                     User_Name = item.Field<string>("User_Name"),
                     User_Email = item.Field<string>("User_Email"),
                     //User_Password = item.Field<string>("User_Password"),
                     //User_Phone = item.Field<string>("User_Phone"),
                     //User_Address = item.Field<string>("User_Address"),
                     //User_City = item.Field<string>("User_City"),
                     //User_Country = item.Field<string>("User_Country"),
                     //User_Zip = item.Field<string>("User_Zip"),
                     //User_DOB = item.Field<DateTime?>("User_DOB"),
                     User_Gender = item.Field<int?>("User_Gender"),
                     //User_Type = item.Field<int?>("User_Type"),
                     //User_Image_Path = item.Field<string>("User_Image_Path"),
                     //User_MacID = item.Field<string>("User_MacID"),
                     //User_IsVerified = item.Field<bool?>("User_IsVerified"),
                     User_IsActive = item.Field<bool?>("User_IsActive"),
                     //User_latitude = item.Field<string>("User_latitude"),
                     //User_longitude = item.Field<string>("User_longitude"),
                     //User_Token_val = item.Field<string>("User_Token_val"),
                     //User_Login_Type = item.Field<int?>("User_Login_Type"),
                     //User_Image_Base = item.Field<string>("User_Image_Base"),
                     //User_Company = item.Field<string>("User_Company"),
                     //User_LastName = item.Field<string>("User_LastName"),
                     //User_Store_URL = item.Field<string>("User_Store_URL"),


                 }).ToList();


            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);
            }
            return UserMaster;
        }


        public List<dynamic> Get_UserMasterDetails(UserMaster_DTO_Input model)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            try
            {

                DataSet ds = Get_UserMaster(model);

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables.Count; i++)
                    {
                        objDynamic.Add(obj.AsDynamicEnumerable(ds.Tables[i]));
                    }

                }

                //var myEnumerableFeaprd = ds.Tables[0].AsEnumerable();
                //List<UserMaster_DTO> UserMaster =
                //   (from item in myEnumerableFeaprd
                //    select new UserMaster_DTO
                //    {
                //        User_PkeyID = item.Field<long>("User_PkeyID"),
                //        User_PkeyID_Master = item.Field<long?>("User_PkeyID_Master"),
                //        User_Name = item.Field<string>("User_Name"),
                //        User_Email = item.Field<string>("User_Email"),
                //        User_Password = item.Field<string>("User_Password"),
                //        User_Phone = item.Field<string>("User_Phone"),
                //        User_Address = item.Field<string>("User_Address"),
                //        User_City = item.Field<string>("User_City"),
                //        User_Country = item.Field<string>("User_Country"),
                //        User_Zip = item.Field<string>("User_Zip"),
                //        User_DOB = item.Field<DateTime?>("User_DOB"),
                //        User_Gender = item.Field<int?>("User_Gender"),
                //        User_Type = item.Field<int?>("User_Type"),
                //        User_Image_Path = item.Field<string>("User_Image_Path"),
                //        User_MacID = item.Field<string>("User_MacID"),
                //        User_IsVerified = item.Field<bool?>("User_IsVerified"),
                //        User_IsActive = item.Field<bool?>("User_IsActive"),
                //        User_latitude = item.Field<string>("User_latitude"),
                //        User_longitude = item.Field<string>("User_longitude"),
                //        User_Token_val = item.Field<string>("User_Token_val"),
                //        User_Login_Type = item.Field<int?>("User_Login_Type"),
                //        User_Image_Base = item.Field<string>("User_Image_Base"),
                //        //User_IsActive_Prof = item.Field<Boolean?>("User_IsActive_Prof"),
                //        User_Company = item.Field<string>("User_Company"),
                //        User_LastName = item.Field<string>("User_LastName"),



                //    }).ToList();

                //objDynamic.Add(UserMaster);
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);
            }

            return objDynamic;
        }

        public DataSet Get_UserMasterLogin(RootUserLogin_input model)
        {
            DataSet ds = new DataSet();
            try
            {
                //string selectProcedure = "[Get_UserMasterLogin]";
                //Dictionary<string, string> input_parameters = new Dictionary<string, string>();
                //List<dynamic> objdynamic = new List<dynamic>();
                //List<dynamic> objdynamicret = new List<dynamic>();
                SqlCommand cmd = new SqlCommand("Get_UserMasterLogin", (SqlConnection)Connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@User_Name", model.User_Name);
                cmd.Parameters.AddWithValue("@User_Email", model.User_Email);
                cmd.Parameters.AddWithValue("@User_Password", model.User_Password);
                cmd.Parameters.AddWithValue("@User_Phone", model.User_Phone);
                cmd.Parameters.AddWithValue("@User_Address", model.User_Address);
                cmd.Parameters.AddWithValue("@User_City", model.User_City);
                cmd.Parameters.AddWithValue("@User_Country", model.User_Country);
                cmd.Parameters.AddWithValue("@User_Zip", model.User_Zip);
                cmd.Parameters.AddWithValue("@User_DOB", model.User_DOB);
                cmd.Parameters.AddWithValue("@User_Type", model.User_Type);
                cmd.Parameters.AddWithValue("@User_Image_Path", model.User_Image_Path);
                cmd.Parameters.AddWithValue("@User_MacID", model.User_MacID);
                cmd.Parameters.AddWithValue("@User_IsVerified", model.User_IsVerified);
                cmd.Parameters.AddWithValue("@User_IsActive", model.User_IsActive);
                cmd.Parameters.AddWithValue("@User_IsDelete", model.User_IsDelete);
                cmd.Parameters.AddWithValue("@UserID", model.UserID);
                cmd.Parameters.AddWithValue("@User_OTP", model.User_OTP);
                cmd.Parameters.AddWithValue("@User_latitude", model.User_latitude);
                cmd.Parameters.AddWithValue("@User_longitude", model.User_longitude);
                cmd.Parameters.AddWithValue("@User_PkeyID_Master", model.User_PkeyID_Master);
                cmd.Parameters.AddWithValue("@User_Login_Type", model.User_Login_Type);
                cmd.Parameters.AddWithValue("@User_Gender", model.User_Gender);
                cmd.Parameters.AddWithValue("@User_Image_Base", model.User_Image_Base);
                cmd.Parameters.AddWithValue("@User_LastName", model.User_LastName);
                cmd.Parameters.AddWithValue("@User_Company", model.User_Company);
                cmd.Parameters.AddWithValue("@User_Language", model.User_Language);
                cmd.Parameters.AddWithValue("@User_Occupation", model.User_Occupation);
                cmd.Parameters.AddWithValue("@User_Token_val", model.User_Token_val);
                cmd.Parameters.AddWithValue("@User_IPAddress", model.User_IPAddress);
                cmd.Parameters.AddWithValue("@User_IsLogin", model.User_IsLogin);
                cmd.Parameters.AddWithValue("@User_Login_Token", model.User_Login_Token);


                cmd.Parameters.AddWithValue("@User_FB_ID", model.User_FB_ID);
                cmd.Parameters.AddWithValue("@User_Gmail_ID", model.User_Gmail_ID);
                cmd.Parameters.AddWithValue("@User_iOS_ID", model.User_iOS_ID);

                cmd.Parameters.AddWithValue("@User_Store_URL", model.User_Store_URL);
                cmd.Parameters.AddWithValue("@User_FB_URL", model.User_FB_URL);
                cmd.Parameters.AddWithValue("@User_Insta_URL", model.User_Insta_URL);
                cmd.Parameters.AddWithValue("@User_YouTube_URL", model.User_YouTube_URL);
                cmd.Parameters.AddWithValue("@User_Bio", model.User_Bio);
                cmd.Parameters.AddWithValue("@User_Image_Name", model.User_Image_Name);
                cmd.Parameters.AddWithValue("@User_Firebase_UID", model.User_Firebase_UID);

                cmd.Parameters.AddWithValue("@Type", model.Type);


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);

            }

            return ds;
        }

        // Below for UserDetail after login

        public List<dynamic> Get_LoginUserDetails(UserMaster_DTO_Input model)
        {
            string wherecondition = string.Empty;

            List<dynamic> objDynamic = new List<dynamic>();
            try
            {
                //if (!string.IsNullOrEmpty(model.Search_Data))
                //{
                //    //wherecondition = " And WI_LoginName =    '" + Data.WI_LoginName + "'";
                //    wherecondition = wherecondition + " And Search Data LIKE '%" + model.Search_Data + "%'";
                //}
                //model.WhereClause = wherecondition;

                DataSet ds = Get_UserMasterDetail(model);

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables.Count; i++)
                    {
                        objDynamic.Add(obj.AsDynamicEnumerable(ds.Tables[i]));
                    }

                }

                //var myEnumerableFeaprd = ds.Tables[0].AsEnumerable();
                //List<UserMaster_DTO> UserMaster =
                //   (from item in myEnumerableFeaprd
                //    select new UserMaster_DTO
                //    {
                //        User_PkeyID = item.Field<long>("User_PkeyID"),
                //        User_Name = item.Field<string>("User_Name"),

                //    }).ToList();
                //objDynamic.Add(UserMaster);

                return objDynamic;
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);
            }

            return objDynamic;
        }

    }
}