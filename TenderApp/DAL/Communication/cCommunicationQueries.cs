using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;


namespace ICSIMemberPaymentLibrary.DAL.Communication
{
    internal class cCommunicationQueries
    {
        cConnection oCnn = new cConnection();

        public long AddEmailQueueItem(SqlParameter[] parameters)
        {
            int _rtnID = 0;
            DataTable oDT = new DataTable();
            SqlConnection oCon = oCnn.Get_Connection();
            SqlCommand oCmd = new SqlCommand();

            try
            {
                oCmd.Connection = oCon;
                oCmd.CommandType = CommandType.StoredProcedure;
                oCmd.CommandText = "Imbibe_Comm_AddEmailQueueItem";

                if (parameters != null)
                {
                    foreach (SqlParameter param in parameters)
                    {
                        oCmd.Parameters.Add(param);
                    }
                }
                else 
                {
                    return _rtnID;
                }

                //oCmd.Parameters.Add("@RequestId", SqlDbType.BigInt).Value = prm_MembDonItem.RequestId;
                //oCmd.Parameters.Add("@MemberUserId", SqlDbType.Int).Value = prm_MembDonItem.MemberUserId;

                SqlDataAdapter oDA = new SqlDataAdapter(oCmd);
                oDA.Fill(oDT);
            }

            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                oCnn.Release_Connection(oCon);
            }

            _rtnID = Convert.ToInt32(oDT.Rows[0][0]);

            return _rtnID;
        }


        public long AddSmsQueueItem(SqlParameter[] parameters)
        {
            int _rtnID = 0;
            DataTable oDT = new DataTable();
            SqlConnection oCon = oCnn.Get_Connection();
            SqlCommand oCmd = new SqlCommand();

            try
            {
                oCmd.Connection = oCon;
                oCmd.CommandType = CommandType.StoredProcedure;
                oCmd.CommandText = "Imbibe_Comm_AddSmsQueueItem";

                if (parameters != null)
                {
                    foreach (SqlParameter param in parameters)
                    {
                        oCmd.Parameters.Add(param);
                    }
                }
                else
                {
                    return _rtnID;
                }

                //oCmd.Parameters.Add("@RequestId", SqlDbType.BigInt).Value = prm_MembDonItem.RequestId;
                //oCmd.Parameters.Add("@MemberUserId", SqlDbType.Int).Value = prm_MembDonItem.MemberUserId;


                SqlDataAdapter oDA = new SqlDataAdapter(oCmd);
                oDA.Fill(oDT);
            }

            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                oCnn.Release_Connection(oCon);
            }

            _rtnID = Convert.ToInt32(oDT.Rows[0][0]);

            return _rtnID;
        }
    }
}
