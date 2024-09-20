using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace EmployeeApplication
{
    public class SqlData
    {
        public static DataSet GetFillDataSet(string connString, string procName, params SqlParameter[] paramters)
        {
            try
            {
                DataSet ds = new DataSet();

                using (var sqlConnection = new SqlConnection(connString))
                {
                    using (var command = sqlConnection.CreateCommand())
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = procName;
                        if (paramters != null)
                        {
                            command.Parameters.AddRange(paramters);
                        }
                        //sqlConnection.Open();
                        using (var da = new SqlDataAdapter(command))
                        {
                            // command.CommandType = CommandType.StoredProcedure;
                            da.Fill(ds);
                        }
                    }
                }
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public int ExecuteNonQuery(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            using (SqlCommand cmdExecute = new SqlCommand())
            {
                PrepareCommand(cmdExecute, connection, transaction, commandType, commandText, commandParameters);
                cmdExecute.CommandTimeout = 0;
                int returnVal = cmdExecute.ExecuteNonQuery();
                return returnVal;
            }
        }
        private void PrepareCommand(SqlCommand command, SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters)
        {
            command.Connection = connection;
            command.CommandText = commandText;
            command.Transaction = transaction;
            command.CommandType = commandType;
            if (commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }
            return;
        }

        private void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
        {
            foreach (SqlParameter param in commandParameters)
            {
                if (param.Value == null)
                {
                    param.Value = DBNull.Value;
                }
                else if (param.Value.ToString() == "1/1/1999 12:00:00 AM")
                {
                    param.Value = DBNull.Value;
                }
                else if (param.Value.ToString() == DateTime.MinValue.ToString())
                {
                    param.Value = DBNull.Value;
                }
                else if (param.Value.ToString() == "01/01/1999 00:00:00")
                {
                    param.Value = DBNull.Value;
                }
                command.Parameters.Add(param);
            }
        }
    }
}