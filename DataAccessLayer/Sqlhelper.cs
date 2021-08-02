using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace DataAccessLayer
{
    class Sqlhelper
    {
        #region Methods
       // string str = ConfigurationManager.ConnectionStrings["tenderApp"].ToString();
        public static SqlConnection GetDataBaseConnection()
        {
            SqlConnection objConn = new SqlConnection();
            objConn.ConnectionString = ConfigurationManager.ConnectionStrings["tenderApp"].ToString();

            return objConn;
        }

       
      
        public static DataSet ExecuteDataSet(string strSpName, params SqlParameter[] parameters)
        {
            if (String.IsNullOrEmpty(strSpName))
                throw new ArgumentNullException("Please pass the Stored procedure name");

            DataSet dsList = null;
            SqlConnection objConnection = null;

            try
            {
                objConnection = GetDataBaseConnection();
                SqlCommand command = BuildSqlCommand(strSpName, objConnection, parameters);
                SqlDataAdapter objDataAdapter = new SqlDataAdapter(command);
                command.CommandTimeout = 60000;
                if (objConnection.State == ConnectionState.Closed)
                    objConnection.Open();

                dsList = new DataSet();
                objDataAdapter.Fill(dsList);

                command.Dispose();
                objDataAdapter.Dispose();
                dsList.Dispose();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (objConnection != null && objConnection.State == ConnectionState.Open)
                    objConnection.Dispose();
            }

            return dsList;
        }

        public static DataSet ExecuteDataSetWithoutParameters(string strSpName)
        {
            if (String.IsNullOrEmpty(strSpName))
                throw new ArgumentNullException("Please pass the Stored procedure name");

            DataSet dsList = null;
            SqlConnection objConnection = null;

            try
            {
                objConnection = GetDataBaseConnection();
                SqlCommand command = new SqlCommand(strSpName, objConnection);
                SqlDataAdapter objDataAdapter = new SqlDataAdapter(command);
                command.CommandTimeout = 60000;
                if (objConnection.State == ConnectionState.Closed)
                    objConnection.Open();

                dsList = new DataSet();
                objDataAdapter.Fill(dsList);

                command.Dispose();
                objDataAdapter.Dispose();
                dsList.Dispose();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (objConnection != null && objConnection.State == ConnectionState.Open)
                    objConnection.Dispose();
            }

            return dsList;
        }

     
        private static SqlCommand BuildSqlCommand(string strSpName, SqlConnection objConnection, params SqlParameter[] parameters)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = objConnection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = strSpName;

            if (parameters != null)
            {
                foreach (SqlParameter parameter in parameters)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput) && (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }

                    command.Parameters.Add(parameter);
                }
            }

            return command;
        }

        public static void ExecuteSP(string strSpName, params SqlParameter[] parameters)
        {
           // int returnCode = -1;
            // blnSuccess = false;

            if (String.IsNullOrEmpty(strSpName))
                throw new ArgumentNullException("Please pass the Stored procedure name");

            SqlConnection objConnection = null;
           // SqlParameter paramReturnValue = new SqlParameter();
            //paramReturnValue.Direction = ParameterDirection.ReturnValue;

            try
            {
                objConnection = GetDataBaseConnection();
                SqlCommand command = BuildSqlCommand(strSpName, objConnection, parameters);
                //command.Parameters.Add(paramReturnValue);

                if (objConnection.State == ConnectionState.Closed)
                    objConnection.Open();

                command.ExecuteNonQuery();

                //returnCode = Convert.ToInt32(paramReturnValue.Value);

               // blnSuccess = ((returnCode == 0) ? true : false);
            }
            finally
            {
                if (objConnection != null && objConnection.State == ConnectionState.Open)
                    objConnection.Dispose();
            }
        }


        public static void ExecuteSPReturn(string strSpName, out bool blnSuccess, params SqlParameter[] parameters)
        {
            int returnCode = -1;
            blnSuccess = false;

            if (String.IsNullOrEmpty(strSpName))
                throw new ArgumentNullException("Please pass the Stored procedure name");

            SqlConnection objConnection = null;
            SqlParameter paramReturnValue = new SqlParameter("@returnValue", SqlDbType.Int);
            paramReturnValue.Direction = ParameterDirection.ReturnValue;

            try
            {
                objConnection = GetDataBaseConnection();
                SqlCommand command = BuildSqlCommand(strSpName, objConnection, parameters);
                command.Parameters.Add(paramReturnValue);

                if (objConnection.State == ConnectionState.Closed)
                    objConnection.Open();

                command.ExecuteNonQuery();

                returnCode = Convert.ToInt32(paramReturnValue.Value);

                blnSuccess = ((returnCode == 0) ? true : false);
            }
            finally
            {
                if (objConnection != null && objConnection.State == ConnectionState.Open)
                    objConnection.Dispose();
            }
        }

        public static SqlParameter BuildSqlParameter<T>(string strParamName, T paramValue, SqlDbType type)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.SqlDbType = type;
            parameter.Value = paramValue;
            parameter.Direction = ParameterDirection.Input;
            parameter.ParameterName = strParamName;

            return parameter;
        }

        public static SqlParameter BuildOutputSqlParameter<T>(string strParamName, T paramValue, SqlDbType type)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.SqlDbType = type;
            parameter.Value = paramValue;
            parameter.Direction = ParameterDirection.Output;
            parameter.ParameterName = strParamName;

            return parameter;
        }



        #endregion
    }
}
