using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using ICSIMemberPaymentLibrary.BL.Master;

namespace ICSIMemberPaymentLibrary.DAL.Master
{
    public class cMemberPaymentQueries
    {
        cConnection oCnn = new cConnection();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="prm_MemberNumber"></param>
        /// <returns></returns>
        public DataTable get_MemberDetails(string prm_MemberNumber)
        {
            DataTable oDT = new DataTable();
            SqlConnection oCon = oCnn.Get_Connection();
            SqlCommand oCmd = new SqlCommand();
            

            try
            {
                oCmd.Connection = oCon;
                oCmd.CommandType = CommandType.StoredProcedure;
                oCmd.CommandText = "ICSI_GetMemberByMembershipNumber";
                oCmd.Parameters.Add("@membershipNumber", SqlDbType.NVarChar, 255).Value = prm_MemberNumber;

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

            return oDT;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="prm_MembDonItem"></param>
        /// <returns></returns>
        public long UpdateMemberPaymentRequest(cMemberPaymentItem prm_MembPayItem)
        {
            DataTable oDT = new DataTable();
            SqlConnection oCon = oCnn.Get_Connection();
            SqlCommand oCmd = new SqlCommand();


            try
            {
                oCmd.Connection = oCon;
                oCmd.CommandType = CommandType.StoredProcedure;
                oCmd.CommandText = "Icsi_Update_MemberPaymentRequest";

                oCmd.Parameters.Add("@RequestId", SqlDbType.BigInt).Value = prm_MembPayItem.RequestId;
                oCmd.Parameters.Add("@MemberUserId", SqlDbType.Int).Value = prm_MembPayItem.MemberUserId;
                oCmd.Parameters.Add("@MemberNumber", SqlDbType.NVarChar, 20).Value = prm_MembPayItem.MemberNumber;
                oCmd.Parameters.Add("@PaymentId", SqlDbType.BigInt).Value = prm_MembPayItem.PaymentId;
                oCmd.Parameters.Add("@Status", SqlDbType.Int).Value = prm_MembPayItem.Status;
                oCmd.Parameters.Add("@SyncStatus", SqlDbType.Int).Value = prm_MembPayItem.SyncStatus;
                oCmd.Parameters.Add("@PaymentType", SqlDbType.Int).Value = prm_MembPayItem.PaymentType;
                oCmd.Parameters.Add("@PaymentAmount", SqlDbType.Decimal).Value = prm_MembPayItem.PaymentAmount;               
                oCmd.Parameters.Add("@Remarks", SqlDbType.NVarChar, -1).Value = prm_MembPayItem.Remarks;
                oCmd.Parameters.Add("@CreatedTime", SqlDbType.DateTime).Value = prm_MembPayItem.CreatedTime;
                oCmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = prm_MembPayItem.UpdatedBy;
                oCmd.Parameters.Add("@UpdatedTime", SqlDbType.DateTime).Value = prm_MembPayItem.UpdatedTime;               
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

            prm_MembPayItem.RequestId = Convert.ToInt64(oDT.Rows[0][0]);

            return prm_MembPayItem.RequestId;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="prm_RequestId"></param>
        /// <returns></returns>
        public DataTable GetMembershipPaymentByRequestId(long prm_RequestId)
        {
            DataTable oDT = new DataTable();
            SqlConnection oCon = oCnn.Get_Connection();
            SqlCommand oCmd = new SqlCommand();


            try
            {
                oCmd.Connection = oCon;
                oCmd.CommandType = CommandType.StoredProcedure;
                oCmd.CommandText = "Icsi_GetMemberPaymentRequestByID";

                oCmd.Parameters.Add("@RequestId", SqlDbType.BigInt).Value = prm_RequestId;
                
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

            return oDT;
        }
    }
}
