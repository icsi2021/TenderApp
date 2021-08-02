using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

namespace ICSIMemberPaymentLibrary.DAL
{
    public class cConnection
    {
        #region GlobalVariables

        String sConnectionString = string.Empty;

        #endregion

        #region Constructors

        public cConnection()
        {
            //this.sConnectionString = ConfigurationSettings.AppSettings["Constr"].ToString().Trim();
            this.sConnectionString = ConfigurationManager.ConnectionStrings["tenderApp"].ToString(); //FOR TEST SERVER
            //this.sConnectionString = ConfigurationManager.ConnectionStrings["SiteSqlServer"].ToString(); //FOR PRODUCTION SERVER
        }

        public cConnection(string prm_NewConnAppKey)
        {
            this.sConnectionString = ConfigurationManager.ConnectionStrings[prm_NewConnAppKey.ToString().Trim()].ToString();
        }

        #endregion

        #region PrivateFunctions

        public SqlConnection Get_Connection()
        {
            SqlConnection _Con = new SqlConnection();
            if (_Con.State == ConnectionState.Closed)
            {
                _Con.ConnectionString = this.GetConnectionString().ToString();
                _Con.Open();
            }
            return _Con;
        }

        public string GetConnectionString()
        {
            return sConnectionString;
        }

        public void Release_Connection(SqlConnection _Con)
        {
            if (_Con.State == ConnectionState.Open)
            {
                _Con.Close();
            }
        }

        public static implicit operator DbConnection(cConnection v)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
