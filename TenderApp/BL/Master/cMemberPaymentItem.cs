using System;
using System.Collections.Generic;
using System.Web;

namespace ICSIMemberPaymentLibrary.BL.Master
{
    public class cMemberPaymentItem
    {
        long _RequestId;
        int _MemberUserId;
        string _MemberNumber;
        long _PaymentId;
        int _Status;
        int _SyncStatus;
        int _PaymentType;
        decimal _PaymentAmount;
        string _Remarks;
        DateTime _CreatedTime;
        int _UpdatedBy;
        DateTime _UpdatedTime;
        string _Name;
        string _Address;
        string _PanNumber;
        string _Email;
        string _MobileNumber;


        #region Properties
        public long RequestId { get { return _RequestId; } set { _RequestId = value; } }
        public int MemberUserId { get { return _MemberUserId; } set { _MemberUserId = value; } }
        public string MemberNumber { get { return _MemberNumber; } set { _MemberNumber = value; } }
        public long PaymentId { get { return _PaymentId; } set { _PaymentId = value; } }
        public int Status { get { return _Status; } set { _Status = value; } }
        public int SyncStatus { get { return _SyncStatus; } set { _SyncStatus = value; } }
        public int PaymentType { get { return _PaymentType; } set { _PaymentType = value; } }
        public decimal PaymentAmount { get { return _PaymentAmount; } set { _PaymentAmount = value; } }
        public string Remarks { get { return _Remarks; } set { _Remarks = value; } }
        public string Email { get { return _Email; } set { _Email = value; } }
        public string MobileNumber { get { return _MobileNumber; } set { _MobileNumber = value; } }
        public string Name { get { return _Name; } set { _Name = value; } }
        public string Address { get { return _Address; } set { _Address = value; } }
        public string PanNumber { get { return _PanNumber; } set { _PanNumber = value; } }
        public DateTime CreatedTime { get { return _CreatedTime; } set { _CreatedTime = value; } }
        public int UpdatedBy { get { return _UpdatedBy; } set { _UpdatedBy = value; } }
        public DateTime UpdatedTime { get { return _UpdatedTime; } set { _UpdatedTime = value; } }

        #endregion

        internal cMemberPaymentItem()
        {

        }

        internal cMemberPaymentItem(long prm_RequestId, int prm_MemberUserId, string prm_MemberNumber, long prm_PaymentId, int prm_Status, int prm_SyncStatus,
            int prm_PaymentType, decimal prm_PaymentAmount, string prm_Remarks,
            DateTime prm_CreatedTime, int prm_UpdatedBy, DateTime prm_UpdatedTime, string prm_Name, string prm_Address, string prm_PanNumber, string prm_Email, string prm_MobileNumber)
        {
            RequestId = prm_RequestId;
            MemberUserId = prm_MemberUserId;
            MemberNumber = prm_MemberNumber;
            PaymentId = prm_PaymentId;
            Status = prm_Status;
            SyncStatus = prm_SyncStatus;
            PaymentType = prm_PaymentType;
            PaymentAmount = prm_PaymentAmount;           
            CreatedTime = prm_CreatedTime;
            UpdatedBy = prm_UpdatedBy;
            UpdatedTime = prm_UpdatedTime;
            Remarks = prm_Remarks;
            Name = prm_Name;
            Address = prm_Address;
            Email = prm_Email;
            MobileNumber = prm_MobileNumber;
            PanNumber = prm_PanNumber;
        }
    }
}