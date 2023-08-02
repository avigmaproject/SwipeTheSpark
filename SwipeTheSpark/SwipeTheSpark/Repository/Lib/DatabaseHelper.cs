using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace SwipeTheSpark.Repository.Lib
{
    public class DatabaseHelper
    {

        //private static string connectionString = "your_connection_string_here";

        private static string connectionString;

        static DatabaseHelper()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            connectionString = configuration.GetConnectionString("Conn_dBcon");
        }


        public static List<dynamic> ExecuteStoredProcedure(string procedureName, SqlParameter[] parameters, out Int64 OutPutID, out int outPut)
        {
            List<dynamic> objData = new List<dynamic>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(procedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(parameters);

                    //SqlParameter OutPutID1 = new SqlParameter("@OutputParameter", System.Data.SqlDbType.Int);
                    //OutPutID1.Direction = System.Data.ParameterDirection.Output;
                    //command.Parameters.Add(OutPutID1);

                    SqlParameter newq = command.Parameters.AddWithValue("@User_PkeyID_Out", 0);
                    newq.Direction = ParameterDirection.Output;
                    SqlParameter ReturnValue = command.Parameters.AddWithValue("@ReturnValue", 0);
                    ReturnValue.Direction = ParameterDirection.Output;
                    command.ExecuteNonQuery();
                    OutPutID = Convert.ToInt64(newq.Value);
                    outPut = Convert.ToInt32(ReturnValue.Value);

                    objData.Add(newq.Value);
                    objData.Add(ReturnValue.Value);
                }
                return objData;
            }
        }
    }
}
