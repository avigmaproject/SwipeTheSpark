using SwipeTheSpark.Repository.Lib;
using SwipeTheSpark.Models.Project;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using SwipeTheSpark.Repository.Lib.FireBase;
using SwipeTheSpark.Repository.Lib.Security;
using System.Data.SqlClient;
using SwipeTheSpark.Repository.Avigma;
using SwipeTheSpark.IRepository.Project;
using Newtonsoft.Json;
using RestSharp;

namespace SwipeTheSpark.Repository.Project
{
    public class Twitter_Trend_Data : ITwitter_Trend_Data
    {
        //MyDataSourceFactory obj = new MyDataSourceFactory();
        //Log log = new Log();
        //SecurityHelper securityHelper = new SecurityHelper();


        Log log = new Log();
        SecurityHelper securityHelper = new SecurityHelper();
        ObjectConvert obj = new ObjectConvert();
        private readonly IConfiguration _configuration;
        public string ConnectionString { get; }
        public Twitter_Trend_Data()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            _configuration = configuration;
            ConnectionString = configuration.GetConnectionString("Conn_dBcon");
        }
        //public Twitter_Trend_Data(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //    ConnectionString = _configuration.GetConnectionString("Conn_dBcon");
        //}


        public IDbConnection Connection
        {
            get { return new SqlConnection(ConnectionString); }
        }

        private List<dynamic> CreateUpdate_Twitter_Trend(Trend model)
        {
            List<dynamic> objData = new List<dynamic>();

            //string insertProcedure = "[CreateUpdate_Twitter_Trend]";

            //Dictionary<string, string> input_parameters = new Dictionary<string, string>();
            //try
            //{


            using (IDbConnection con = Connection)
            {
                if (Connection.State == ConnectionState.Closed) con.Open();

                try
                {
                    SqlCommand cmd = new SqlCommand("CreateUpdate_Trend_Master", (SqlConnection)con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@TDM_PKeyID", model.TDM_PKeyID);
                    cmd.Parameters.AddWithValue("@TDM_Name", model.name);
                    cmd.Parameters.AddWithValue("@TDM_URL", model.url);
                    cmd.Parameters.AddWithValue("@TDM_Promoted_Content", model.promoted_content);
                    cmd.Parameters.AddWithValue("@TDM_Query", model.query);
                    cmd.Parameters.AddWithValue("@TDM_Tweet_Volume", model.tweet_volume);

                    cmd.Parameters.AddWithValue("@TDM_IsActive", model.TDM_IsActive);
                    cmd.Parameters.AddWithValue("@TDM_IsDelete", model.TDM_IsDelete);
                    cmd.Parameters.AddWithValue("@Type", model.Type);
                    cmd.Parameters.AddWithValue("@UserID", model.UserID);


                    SqlParameter TDM_Pkey_Out = cmd.Parameters.AddWithValue("@TDM_Pkey_Out", 0);
                    TDM_Pkey_Out.Direction = ParameterDirection.Output;
                    SqlParameter ReturnValue = cmd.Parameters.AddWithValue("@ReturnValue", 0);
                    ReturnValue.Direction = ParameterDirection.Output;


                    cmd.ExecuteNonQuery();
                    objData.Add(TDM_Pkey_Out.Value);
                    objData.Add(ReturnValue.Value);

                    //cmd.Parameters.AddWithValue("@NT_Pkey_Out", 2 + "#bigint#" + null);
                    //    cmd.Parameters.AddWithValue("@ReturnValue", 2 + "#int#" + null);

                    //    objData = obj.SqlCRUD(insertProcedure, input_parameters);


                }
                catch (Exception ex)
                {
                    log.logErrorMessage(ex.StackTrace);
                    log.logErrorMessage(ex.Message);
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

        private DataSet Get_Twitter_Trend(Trend_DTO_Input model)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Get_Twitter_Trend", (SqlConnection)Connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TDM_PKeyID", model.TDM_PKeyID);
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

        public async Task<List<dynamic>> CreateUpdate_Twitter_TrendDetails()
        {
            List<dynamic> objData = new List<dynamic>();
            try
            {
                var RapidAPIHost = _configuration["RapidAPI-Host"];
                var RapidAPIKey = _configuration["RapidAPI-Key"];

                var options = new RestClientOptions("https://twitter154.p.rapidapi.com/")
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest("https://twitter154.p.rapidapi.com/trends/?woeid=1", Method.Get);
                request.AddHeader("X-RapidAPI-Host", RapidAPIHost);
                request.AddHeader("X-RapidAPI-Key", RapidAPIKey);
                RestResponse response = await client.ExecuteAsync(request);
                //Console.WriteLine(response.Content);
                var obj = JsonConvert.DeserializeObject<List<RootObject>>(response.Content);
                if (obj.Count > 0)
                {
                    Trend trend = new Trend();
                    for (int i = 0; i < obj[0].trends.Count; i++)
                    {
                        obj[0].trends[i].Type = 1;
                        objData.AddRange(CreateUpdate_Twitter_Trend(obj[0].trends[i]));
                    }
                }

                log.logErrorMessage(response.StatusCode.ToString());
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
            }
            return objData;
        }


        public async Task<List<dynamic>> Get_Twitter_Trend_Data(Trend_DTO_Input inputData)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            try
            {
                DataSet ds = Get_Twitter_Trend(inputData);

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables.Count; i++)
                    {
                        objDynamic.Add(obj.AsDynamicEnumerable(ds.Tables[i]));
                    }

                }
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
            }

            return objDynamic;
        }

    }
}