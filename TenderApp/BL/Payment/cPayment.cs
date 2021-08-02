using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using ICSIMemberPaymentLibrary.DAL.Payment;

namespace ICSIMemberPaymentLibrary.BL.Payment
{
    public class cPayment
    {
        cPaymentItem objPayItem = new cPaymentItem();
        cPaymentProviderReconciliationItem objPayRecoItem = new cPaymentProviderReconciliationItem();
        
        cPaymentQueries objPayQueries;

        public cPaymentItem PayItem
        {
            get { return objPayItem; }
            set { objPayItem = value; }
        }

        public cPaymentProviderReconciliationItem PayRecoItem
        {
            get { return objPayRecoItem; }
            set { objPayRecoItem = value; }
        }


        public cPayment()
        {
            objPayQueries = new cPaymentQueries();
        }

        public void ProcessPayment(cPaymentItem prm_PayItem)
        {
            //DataTable oDT = new DataTable();
            //oDT = objPayQueries.PaymentProcess(prm_PayItem);
            prm_PayItem.PaymentId =  objPayQueries.PaymentProcess(prm_PayItem);
            //To Do: Success of payment needs to be check here.
            //return oDT;

            //return prm_PayItem.PaymentId;
        }

        public cPaymentItem GetPaymentDetails(long prm_PaymentID)
        {
            cPaymentItem _PayItem;
            try
            {

                DataTable oDT = new DataTable();
                oDT = objPayQueries.GetPayment(prm_PaymentID);


                if (oDT.Rows.Count > 0)
                {

                    //MemDonItem.MembNo = oDT
                    _PayItem = new cPaymentItem(Convert.ToInt64(oDT.Rows[0]["PaymentId"]), Convert.ToInt64(oDT.Rows[0]["PaymentType"]), oDT.Rows[0]["PaymentTypeDesc"].ToString().Trim(),
                        Convert.ToInt64(oDT.Rows[0]["PaymentOwnerId"]), Convert.ToInt32(oDT.Rows[0]["PaymentStatus"]), Convert.ToInt32(oDT.Rows[0]["SyncStatus"]),
                        Convert.ToInt32(oDT.Rows[0]["PaymentMode"]), Convert.ToInt32(oDT.Rows[0]["PaymentProvider"]), Convert.ToDecimal(oDT.Rows[0]["PrincipalAmount"]),
                        Convert.ToDecimal(oDT.Rows[0]["ServiceTaxAmount"]), Convert.ToDecimal(oDT.Rows[0]["PaymentAmount"]), Convert.ToInt32(oDT.Rows[0]["PayerUserId"]),
                        oDT.Rows[0]["PayerUsername"].ToString().Trim(), oDT.Rows[0]["PayerName"].ToString().Trim(), oDT.Rows[0]["PayerMobileNumber"].ToString().Trim(),
                        oDT.Rows[0]["PayerEmail"].ToString().Trim(), Convert.ToInt64(oDT.Rows[0]["ReceiptNumber"]), oDT.Rows[0]["PaymentProviderRawResponse"].ToString().Trim(),
                        oDT.Rows[0]["TransactionResponseCode1"].ToString().Trim(), oDT.Rows[0]["TransactionResponseCode2"].ToString().Trim(), oDT.Rows[0]["TransactionResponseCode3"].ToString().Trim(),
                        oDT.Rows[0]["TransactionResponseCode4"].ToString().Trim(), oDT.Rows[0]["DraftBankName"].ToString().Trim(), oDT.Rows[0]["DraftBankBranch"].ToString().Trim(),
                        oDT.Rows[0]["DraftBankCity"].ToString().Trim(), oDT.Rows[0]["DraftNumber"].ToString().Trim(), (DateTime?)(oDT.Rows[0]["DraftIssueDate"]),
                        Convert.ToDateTime(oDT.Rows[0]["CreatedTime"]), (DateTime?)(oDT.Rows[0]["ApprovalTime"]), Convert.ToInt32(oDT.Rows[0]["UpdatedBy"]), Convert.ToDateTime(oDT.Rows[0]["UpdatedTime"]),
                        oDT.Rows[0]["RejectionReason"].ToString().Trim(), 0 
                        );


                }
                else
                {
                    _PayItem = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _PayItem;
        }

        public long GetNextReceiptNumber()
        {
            return this.objPayQueries.GetNextReceiptNumber();
        }

        public void ProcessReconciliation(cPaymentProviderReconciliationItem prm_PayRecoItem)
        {
            prm_PayRecoItem.PaymentId = objPayQueries.UpdatePaymentProviderReconciliationItem(prm_PayRecoItem);
            
        }

        public DataTable GetPaymentTypeList()
        {
            return objPayQueries.GetNaturalDisasterPaymentTypes();
        }
        public cPaymentItem GetPaymentDetailsByTranResCode(string prm_TranResCode)
        {
            cPaymentItem _PayItem;
            try
            {

                DataTable oDT = new DataTable();
                oDT = objPayQueries.GetPayment(prm_TranResCode);


                if (oDT.Rows.Count > 0)
                {

                    //MemDonItem.MembNo = oDT
                    _PayItem = new cPaymentItem(Convert.ToInt64(oDT.Rows[0]["PaymentId"]), Convert.ToInt64(oDT.Rows[0]["PaymentType"]), oDT.Rows[0]["PaymentTypeDesc"].ToString().Trim(),
                        Convert.ToInt64(oDT.Rows[0]["PaymentOwnerId"]), Convert.ToInt32(oDT.Rows[0]["PaymentStatus"]), Convert.ToInt32(oDT.Rows[0]["SyncStatus"]),
                        Convert.ToInt32(oDT.Rows[0]["PaymentMode"]), Convert.ToInt32(oDT.Rows[0]["PaymentProvider"]), Convert.ToDecimal(oDT.Rows[0]["PrincipalAmount"]),
                        Convert.ToDecimal(oDT.Rows[0]["ServiceTaxAmount"]), Convert.ToDecimal(oDT.Rows[0]["PaymentAmount"]), Convert.ToInt32(oDT.Rows[0]["PayerUserId"]),
                        oDT.Rows[0]["PayerUsername"].ToString().Trim(), oDT.Rows[0]["PayerName"].ToString().Trim(), oDT.Rows[0]["PayerMobileNumber"].ToString().Trim(),
                        oDT.Rows[0]["PayerEmail"].ToString().Trim(), Convert.ToInt64(oDT.Rows[0]["ReceiptNumber"]), oDT.Rows[0]["PaymentProviderRawResponse"].ToString().Trim(),
                        oDT.Rows[0]["TransactionResponseCode1"].ToString().Trim(), oDT.Rows[0]["TransactionResponseCode2"].ToString().Trim(), oDT.Rows[0]["TransactionResponseCode3"].ToString().Trim(),
                        oDT.Rows[0]["TransactionResponseCode4"].ToString().Trim(), oDT.Rows[0]["DraftBankName"].ToString().Trim(), oDT.Rows[0]["DraftBankBranch"].ToString().Trim(),
                        oDT.Rows[0]["DraftBankCity"].ToString().Trim(), oDT.Rows[0]["DraftNumber"].ToString().Trim(), (DateTime?)(oDT.Rows[0]["DraftIssueDate"]),
                        Convert.ToDateTime(oDT.Rows[0]["CreatedTime"]), (DateTime?)(oDT.Rows[0]["ApprovalTime"]), Convert.ToInt32(oDT.Rows[0]["UpdatedBy"]), Convert.ToDateTime(oDT.Rows[0]["UpdatedTime"]),
                        oDT.Rows[0]["RejectionReason"].ToString().Trim(), 0
                        );


                }
                else
                {
                    _PayItem = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _PayItem;
        }
    }
}
