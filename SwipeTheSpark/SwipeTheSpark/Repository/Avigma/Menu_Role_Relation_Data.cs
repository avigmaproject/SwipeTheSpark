﻿using SwipeTheSpark.Repository.Lib;
using SwipeTheSpark.Models.Avigma;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using SwipeTheSpark.Repository.Lib.Security;
using System.Data.SqlClient;

namespace SwipeTheSpark.Repository.Avigma
{
    public class Menu_Role_Relation_Data
    {
        Log log = new Log();
        SecurityHelper securityHelper = new SecurityHelper();
        private readonly IConfiguration _configuration;
        public string ConnectionString { get; }

        public Menu_Role_Relation_Data(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("Conn_dBcon");
        }

        public IDbConnection Connection
        {
            get { return new SqlConnection(ConnectionString); }
        }
        private List<dynamic> CreateUpdate_Menu_Role_Relation(Menu_Role_Relation_DTO model)
        {
            //List<dynamic> objData = new List<dynamic>();

            //string insertProcedure = "[CreateUpdate_Menu_Role_Relation]";

            //Dictionary<string, string> input_parameters = new Dictionary<string, string>();
            string msg = string.Empty;

            List<dynamic> objData = new List<dynamic>();

            using (IDbConnection con = Connection)
            {
                if (Connection.State == ConnectionState.Closed) con.Open();

                try
                {
                    SqlCommand cmd = new SqlCommand("CreateUpdate_UserMaster", (SqlConnection)con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MUR_PkeyID", model.MUR_PkeyID);
                    cmd.Parameters.AddWithValue("@MUR_MenuID", model.MUR_MenuID);
                    cmd.Parameters.AddWithValue("@MUR_Role", model.MUR_Role);
                    cmd.Parameters.AddWithValue("@MUR_IsActive", 1 + "#bit#" + model.MUR_IsActive);
                    cmd.Parameters.AddWithValue("@MUR_IsDelete", 1 + "#bit#" + model.MUR_IsDelete);

                    cmd.Parameters.AddWithValue("@Type", model.Type);
                    cmd.Parameters.AddWithValue("@UserID", model.UserID);
                    cmd.Parameters.AddWithValue("@MUR_Pkey_Out", 0).Direction = ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("@ReturnValue", 0).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    msg = "Add Success";


                }
                catch (Exception ex)
                {
                    log.logErrorMessage(ex.StackTrace);
                    log.logErrorMessage(ex.Message);
                }
                return objData;

            }

        }
        private DataSet Get_Menu_Role_Relation(Menu_Role_Relation_DTO model)
        {
            DataSet ds = new DataSet();
            try
            {
                //string selectProcedure = "[Get_Menu_Role_Relation]";
                //Dictionary<string, string> input_parameters = new Dictionary<string, string>();

                SqlCommand cmd = new SqlCommand("Get_Menu_Role_Relation", (SqlConnection)Connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MUR_PkeyID", model.MUR_PkeyID);
                cmd.Parameters.AddWithValue("@MUR_Role", model.MUR_Role);
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
        public List<dynamic> CreateUpdate_Menu_Role_Relation_DataDetails(Menu_Role_Relation_DTO model)
        {
            List<dynamic> objData = new List<dynamic>();
            try
            {
                objData = CreateUpdate_Menu_Role_Relation(model);
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
            }
            return objData;
        }
        public List<dynamic> Get_Menu_Role_RelationDetails(Menu_Role_Relation_DTO model)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            try
            {

                DataSet ds = Get_Menu_Role_Relation(model);

                var myEnumerableFeaprd = ds.Tables[0].AsEnumerable();
                List<Menu_Role_Relation_DTO> Get_details =
                   (from item in myEnumerableFeaprd
                    select new Menu_Role_Relation_DTO
                    {
                        MUR_PkeyID = item.Field<long>("MUR_PkeyID"),
                        MUR_MenuID = item.Field<long?>("MUR_MenuID"),
                        MUR_Role = item.Field<long?>("MUR_Role"),
                        MUR_IsActive = item.Field<bool?>("MUR_IsActive"),

                    }).ToList();

                objDynamic.Add(Get_details);
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