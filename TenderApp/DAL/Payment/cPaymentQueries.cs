using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;


using ICSIMemberPaymentLibrary.BL.Payment;

namespace ICSIMemberPaymentLibrary.DAL.Payment
{
    internal class cPaymentQueries
    {
        cConnection oCnn = new cConnection();

        public long PaymentProcess(cPaymentItem prm_PayItem)
        {
            DataTable oDT = new DataTable();

            SqlConnection oCon = oCnn.Get_Connection();
            SqlCommand oCmd = new SqlCommand();

            try
            {
                oCmd.Connection = oCon;
                oCmd.CommandType = CommandType.StoredProcedure;
                oCmd.CommandText = "Icsi_Pay_UpdatePayment";

                oCmd.Parameters.Add("@paymentId", SqlDbType.BigInt).Value = prm_PayItem.PaymentId;
                oCmd.Parameters.Add("@paymentType", SqlDbType.BigInt).Value = prm_PayItem.PaymentType;
                oCmd.Parameters.Add("@paymentTypeDesc", SqlDbType.NVarChar, 50).Value = prm_PayItem.PaymentTypeDesc;
                oCmd.Parameters.Add("@paymentOwnerId", SqlDbType.Int).Value = prm_PayItem.PaymentOwnerId;
                oCmd.Parameters.Add("@paymentStatus", SqlDbType.TinyInt).Value = prm_PayItem.PaymentStatus;
                oCmd.Parameters.Add("@syncStatus", SqlDbType.TinyInt).Value = prm_PayItem.SyncStatus;
                oCmd.Parameters.Add("@paymentMode", SqlDbType.TinyInt).Value = prm_PayItem.PaymentMode;
                oCmd.Parameters.Add("@paymentProvider", SqlDbType.TinyInt).Value = prm_PayItem.PaymentProvider;
                oCmd.Parameters.Add("@principalAmount", SqlDbType.Money).Value = prm_PayItem.PrincipalAmount;
                oCmd.Parameters.Add("@serviceTaxAmount", SqlDbType.Money).Value = prm_PayItem.ServiceTaxAmount;
                oCmd.Parameters.Add("@paymentAmount", SqlDbType.Money).Value = prm_PayItem.PaymentAmount;
                oCmd.Parameters.Add("@payerUserId", SqlDbType.Int).Value = prm_PayItem.PayerUserId;
                oCmd.Parameters.Add("@payerUsername", SqlDbType.NVarChar, 50).Value = prm_PayItem.PayerUsername;
                oCmd.Parameters.Add("@payerName", SqlDbType.NVarChar, 255).Value = prm_PayItem.PayerName;
                oCmd.Parameters.Add("@payerMobileNumber", SqlDbType.NVarChar, 50).Value = prm_PayItem.PayerMobileNumber;
                oCmd.Parameters.Add("@payerEmail", SqlDbType.NVarChar, 255).Value = prm_PayItem.PayerEmail;

                if (prm_PayItem.ReceiptNumber == 0)
                {
                    oCmd.Parameters.Add("@receiptNumber", SqlDbType.BigInt).Value = DBNull.Value;
                }
                else
                {
                    oCmd.Parameters.Add("@receiptNumber", SqlDbType.BigInt).Value = prm_PayItem.ReceiptNumber;
                }
                oCmd.Parameters.Add("@paymentProviderRawResponse", SqlDbType.NVarChar, -1).Value = prm_PayItem.PaymentProviderRawResponse;
                oCmd.Parameters.Add("@transactionResponseCode1", SqlDbType.NVarChar, 50).Value = prm_PayItem.TransactionResponseCode1;
                oCmd.Parameters.Add("@transactionResponseCode2", SqlDbType.NVarChar, 50).Value = prm_PayItem.TransactionResponseCode2;
                oCmd.Parameters.Add("@transactionResponseCode3", SqlDbType.NVarChar, 50).Value = prm_PayItem.TransactionResponseCode3;
                oCmd.Parameters.Add("@transactionResponseCode4", SqlDbType.NVarChar, 50).Value = prm_PayItem.TransactionResponseCode4;
                oCmd.Parameters.Add("@draftBankName", SqlDbType.NVarChar, 255).Value = prm_PayItem.DraftBankName;
                oCmd.Parameters.Add("@draftBankBranch", SqlDbType.NVarChar, 50).Value = prm_PayItem.DraftBankBranch;
                oCmd.Parameters.Add("@draftBankCity", SqlDbType.NVarChar, 50).Value = prm_PayItem.DraftBankCity;
                oCmd.Parameters.Add("@draftNumber", SqlDbType.NVarChar, 50).Value = prm_PayItem.DraftNumber;
                oCmd.Parameters.Add("@draftIssueDate", SqlDbType.DateTime).Value = DBNull.Value;
                oCmd.Parameters.Add("@createdTime", SqlDbType.DateTime).Value = prm_PayItem.CreatedTime;

                if (prm_PayItem.ApprovalTime.HasValue)
                {
                    oCmd.Parameters.Add("@approvalTime", SqlDbType.DateTime).Value = prm_PayItem.ApprovalTime;
                }
                else
                {
                    oCmd.Parameters.Add("@approvalTime", SqlDbType.DateTime).Value = DBNull.Value;
                }
                
                oCmd.Parameters.Add("@rejectionReason", SqlDbType.NVarChar, 128).Value = prm_PayItem.RejectionReason;
                oCmd.Parameters.Add("@updatedBy", SqlDbType.Int).Value = prm_PayItem.UpdatedBy;
                oCmd.Parameters.Add("@updatedTime", SqlDbType.DateTime).Value = prm_PayItem.UpdatedTime;
                oCmd.Parameters.Add("@feeId", SqlDbType.Int).Value = prm_PayItem.feeId;
                
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

            prm_PayItem.PaymentId = Convert.ToInt64(oDT.Rows[0][0]);
            
            return prm_PayItem.PaymentId;
        }

        public long GetNextReceiptNumber()
        {
            long l_RecieptNumber = 0;
            DataTable oDT = new DataTable();

            SqlConnection oCon = oCnn.Get_Connection();
            SqlCommand oCmd = new SqlCommand();

            try
            {
                oCmd.Connection = oCon;
                oCmd.CommandType = CommandType.StoredProcedure;
                //oCmd.CommandText = "Icsi_Pay_GetNextReceiptNumber";
                // Disaster Donation Reciepts sequence will be same as of Students Fees and Member Fees receipts .
                oCmd.CommandText = "Icsi_Pay_GetNextReceiptNumber"; 

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

            l_RecieptNumber = Convert.ToInt64(oDT.Rows[0][0]);
            return l_RecieptNumber; 
        }

        public DataTable GetPayment(long prm_PaymentID)
        {
            DataTable oDT = new DataTable();

            SqlConnection oCon = oCnn.Get_Connection();
            SqlCommand oCmd = new SqlCommand();

            try
            {
                oCmd.Connection = oCon;
                oCmd.CommandType = CommandType.StoredProcedure;
                oCmd.CommandText = "Icsi_Pay_GetPayment";
                oCmd.Parameters.Add("@paymentId", SqlDbType.BigInt).Value = prm_PaymentID;

                SqlDataAdapter oDA = new SqlDataAdapter(oCmd);
                oDA.Fill(oDT);

                if (oDT.Rows.Count > 0)
                {
                    if (DBNull.Value.Equals(oDT.Rows[0]["ReceiptNumber"]))
                    {
                        oDT.Rows[0]["ReceiptNumber"] = "0";
                    }

                    if (DBNull.Value.Equals(oDT.Rows[0]["DraftIssueDate"]))
                    {
                        oDT.Rows[0]["DraftIssueDate"] = DateTime.Now.ToString();
                    }

                    if (DBNull.Value.Equals(oDT.Rows[0]["ApprovalTime"]))
                    {
                        oDT.Rows[0]["ApprovalTime"] = DateTime.Now.ToString();
                    }
                }
                

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
        /// <param name="prm_TranResCode"></param>
        /// <returns></returns>
        public DataTable GetPayment(string prm_TranResCode)
        {
            DataTable oDT = new DataTable();

            SqlConnection oCon = oCnn.Get_Connection();
            SqlCommand oCmd = new SqlCommand();
            try
            {
                oCmd.Connection = oCon;
                oCmd.CommandType = CommandType.StoredProcedure;
                oCmd.CommandText = "Icsi_Pay_GetPaymentByTranRespCode";
                oCmd.Parameters.Add("@TranRespCode", SqlDbType.NVarChar,100).Value = prm_TranResCode;

                SqlDataAdapter oDA = new SqlDataAdapter(oCmd);
                oDA.Fill(oDT);

                if (oDT.Rows.Count > 0)
                {
                    if (DBNull.Value.Equals(oDT.Rows[0]["ReceiptNumber"]))
                    {
                        oDT.Rows[0]["ReceiptNumber"] = "0";
                    }

                    if (DBNull.Value.Equals(oDT.Rows[0]["DraftIssueDate"]))
                    {
                        oDT.Rows[0]["DraftIssueDate"] = DateTime.Now.ToString();
                    }

                    if (DBNull.Value.Equals(oDT.Rows[0]["ApprovalTime"]))
                    {
                        oDT.Rows[0]["ApprovalTime"] = DateTime.Now.ToString();
                    }
                }


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
        public long UpdatePaymentProviderReconciliationItem(cPaymentProviderReconciliationItem prm_PayRecoItem)
        {
            DataTable oDT = new DataTable();

            SqlConnection oCon = oCnn.Get_Connection();
            SqlCommand oCmd = new SqlCommand();

            try
            {
                oCmd.Connection = oCon;
                oCmd.CommandType = CommandType.StoredProcedure;
                oCmd.CommandText = "Icsi_Pay_UpdatePaymentProviderReconciliationItem";

                oCmd.Parameters.Add("@paymentId", SqlDbType.BigInt).Value = prm_PayRecoItem.PaymentId;
                oCmd.Parameters.Add("@paymentStatus", SqlDbType.TinyInt).Value = prm_PayRecoItem.PaymentStatus;
                oCmd.Parameters.Add("@paymentProvider", SqlDbType.TinyInt).Value = prm_PayRecoItem.PaymentProvider;
                oCmd.Parameters.Add("@paymentAmount", SqlDbType.Money).Value = prm_PayRecoItem.PaymentAmount;

                oCmd.Parameters.Add("@transactionResponseCode1", SqlDbType.NVarChar, 50).Value = prm_PayRecoItem.TransactionResponseCode1;
                oCmd.Parameters.Add("@transactionResponseCode2", SqlDbType.NVarChar, 50).Value = prm_PayRecoItem.TransactionResponseCode2;
                oCmd.Parameters.Add("@transactionResponseCode3", SqlDbType.NVarChar, 50).Value = prm_PayRecoItem.TransactionResponseCode3;
                oCmd.Parameters.Add("@transactionResponseCode4", SqlDbType.NVarChar, 50).Value = prm_PayRecoItem.TransactionResponseCode4;
                
                if (prm_PayRecoItem.PaymentProviderDate.HasValue)
                {
                    oCmd.Parameters.Add("@paymentProviderDate", SqlDbType.DateTime).Value = prm_PayRecoItem.PaymentProviderDate;
                }
                else
                {
                    oCmd.Parameters.Add("@paymentProviderDate", SqlDbType.DateTime).Value = DBNull.Value;
                }
                
                oCmd.Parameters.Add("@systemMessage", SqlDbType.NVarChar, -1).Value = prm_PayRecoItem.SystemMessage;

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

            prm_PayRecoItem.PaymentId = Convert.ToInt64(oDT.Rows[0][0]);

            return prm_PayRecoItem.PaymentId;
        }

        public DataTable GetNaturalDisasterPaymentTypes()
        {
            DataTable oDT = new DataTable();

            SqlConnection oCon = oCnn.Get_Connection();
            SqlCommand oCmd = new SqlCommand();

            try
            {
                oCmd.Connection = oCon;
                oCmd.CommandType = CommandType.StoredProcedure;
                oCmd.CommandText = "ICSI_GetNaturalDisasterPaymentType";

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
