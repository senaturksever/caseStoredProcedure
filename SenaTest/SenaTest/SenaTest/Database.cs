using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SenaTest
{
    public class Database
    {
        private static string ConnString = "";
        string sqlCommand = "StockStatus";

        public static string GetConnectionString()
        {
            if (ConnString == "")
            {
                ConnString = ConnectKeys();
            }
            return ConnString;
        }
        private static string ConnectKeys()
        {
            try
            {//projeye bağlı veritabanı oluşturuldu. 

                string dbConnectionString = "Data Source=DESKTOP-8E83SPI\\SQLEXPRESS;Initial Catalog=SenaTest;User ID=senaturksever;Password=Sena123.";
                return dbConnectionString;
            }
            catch (Exception)
            {

               
            }
            return "";
        }

        public static SqlParameter SetParameter(string parameterName, SqlDbType dbType, Int32 iSize, string direction, object oParamValue)
        {
            var parameter = new SqlParameter(parameterName, dbType, iSize);

            switch (direction)
            {
                case "Input":
                    parameter.Direction = ParameterDirection.Input;
                    break;
                case "Output":
                    parameter.Direction = ParameterDirection.Output;
                    break;
                case "ReturnValue":
                    parameter.Direction = ParameterDirection.ReturnValue;
                    break;
                case "InputOutput":
                    parameter.Direction = ParameterDirection.InputOutput;
                    break;
                default:
                    break;
            }

            parameter.Value = oParamValue;
            return parameter;
        }

        public static DataTable ExecuteStoredProcedure2DataTable(string procedureName, SqlParameter[] sqlParameters)
        {
            var ds = new DataSet();

            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    connection.Open(); // Bağlantıyı aç

                    SqlTransaction transaction = connection.BeginTransaction();
                    SqlCommand command = new SqlCommand(procedureName, connection, transaction)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    if (sqlParameters != null)
                    {
                        foreach (var parameter in sqlParameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }

                    var da = new SqlDataAdapter(command);
                    da.SelectCommand.CommandTimeout = 300;

                    da.Fill(ds);
                    command.Transaction.Commit();
                }
            }
            catch (Exception ex)
            {
            }

            return ds.Tables.Count != 0 ? ds.Tables[0] : new DataTable();
        }
    }
}