using System;
using System.Data;
using ICSIMemberPaymentLibrary.DAL.Master;
namespace ICSIMemberPaymentLibrary.BL.Master
{
    public class cMemberPayment
    {
        cMemberPaymentItem objMemPayItem = new cMemberPaymentItem();
        cMemberPaymentQueries objMemPayQueries;

        public cMemberPaymentItem MemPayItem
        {
            get { return objMemPayItem; }
            set { objMemPayItem = value; }
        }

        public cMemberPayment()
        {
            objMemPayQueries = new cMemberPaymentQueries();
        }

        public cMemberPaymentItem GetMemberDetails(string prm_MemberNumber)
        {
            cMemberPaymentItem _MemPayItem;
            try
            {

                DataTable oDT = new DataTable();
                oDT = objMemPayQueries.get_MemberDetails(prm_MemberNumber);
                if (oDT.Rows.Count > 0)
                {

                    //MemDonItem.MembNo = oDT
                    _MemPayItem = new cMemberPaymentItem(Convert.ToInt64(oDT.Rows[0]["RequestId"]), Convert.ToInt32(oDT.Rows[0]["MemberUserId"]), oDT.Rows[0]["MemberNumber"].ToString().Trim(),
                        Convert.ToInt64(oDT.Rows[0]["PaymentId"]), Convert.ToInt32(oDT.Rows[0]["Status"]), Convert.ToInt32(oDT.Rows[0]["SyncStatus"]), Convert.ToInt32(oDT.Rows[0]["PaymentType"]),
                        Convert.ToDecimal(oDT.Rows[0]["PaymentAmount"]), oDT.Rows[0]["Remarks"].ToString().Trim(), Convert.ToDateTime(oDT.Rows[0]["CreatedTime"]),
                        Convert.ToInt32(oDT.Rows[0]["UpdatedBy"]), Convert.ToDateTime(oDT.Rows[0]["UpdatedTime"]), oDT.Rows[0]["Name"].ToString().Trim(), oDT.Rows[0]["Address"].ToString().Trim(),
                        oDT.Rows[0]["PanNumber"].ToString().Trim(), oDT.Rows[0]["Email"].ToString().Trim(), oDT.Rows[0]["MobileNumber"].ToString().Trim());
                }
                else
                {
                    _MemPayItem = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _MemPayItem;
        }

        public void UpdateMemberPaymentRequest(cMemberPaymentItem prm_MembPayItem)
        {
            prm_MembPayItem.RequestId = objMemPayQueries.UpdateMemberPaymentRequest(prm_MembPayItem);

            
        }

        public cMemberPaymentItem GetMembershipPaymentByRequestId(long prm_RequestId)
        {
            cMemberPaymentItem _MemPayItem;
            try
            {

                DataTable oDT = new DataTable();
                oDT = objMemPayQueries.GetMembershipPaymentByRequestId(prm_RequestId);


                if (oDT.Rows.Count > 0)
                {

                    
                    _MemPayItem = new cMemberPaymentItem(Convert.ToInt64(oDT.Rows[0]["RequestId"]), Convert.ToInt32(oDT.Rows[0]["MemberUserId"]), oDT.Rows[0]["MemberNumber"].ToString().Trim(),
                        Convert.ToInt64(oDT.Rows[0]["PaymentId"]), Convert.ToInt32(oDT.Rows[0]["Status"]), Convert.ToInt32(oDT.Rows[0]["SyncStatus"]), Convert.ToInt32(oDT.Rows[0]["PaymentType"]),
                        Convert.ToDecimal(oDT.Rows[0]["PaymentAmount"]), oDT.Rows[0]["Remarks"].ToString().Trim(), Convert.ToDateTime(oDT.Rows[0]["CreatedTime"]),
                        Convert.ToInt32(oDT.Rows[0]["UpdatedBy"]), Convert.ToDateTime(oDT.Rows[0]["UpdatedTime"]), oDT.Rows[0]["Name"].ToString().Trim(), oDT.Rows[0]["Address"].ToString().Trim(),
                        oDT.Rows[0]["PanNumber"].ToString().Trim(), oDT.Rows[0]["Email"].ToString().Trim(), oDT.Rows[0]["MobileNumber"].ToString().Trim()
                        );


                }
                else
                {
                    _MemPayItem = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _MemPayItem;
        }
    }

}