using System;
using System.Collections.Generic;
using System.Text;

namespace ICSIMemberPaymentLibrary.BL.Payment
{
    public class cPaymentItem
    {
        long _PaymentId;
        long _PaymentType;
        string _PaymentTypeDesc;
        long _PaymentOwnerId;
        int _PaymentStatus;
        int _SyncStatus;
        int _PaymentMode;
        int _PaymentProvider;
        decimal _PrincipalAmount;
        decimal _ServiceTaxAmount;
        decimal _PaymentAmount;
        int	_PayerUserId;
        string _PayerUsername;
        string _PayerName;
        string _PayerMobileNumber;
        string _PayerEmail;
        long? _ReceiptNumber;
        string _PaymentProviderRawResponse;
        string _TransactionResponseCode1;
        string _TransactionResponseCode2;
        string _TransactionResponseCode3;
        string _TransactionResponseCode4;
        string _DraftBankName;
        string _DraftBankBranch;
        string _DraftBankCity;
        string _DraftNumber;
        DateTime? _DraftIssueDate;
        DateTime _CreatedTime;
        DateTime? _ApprovalTime;
        int	_UpdatedBy;
        DateTime _UpdatedTime;
        string _RejectionReason;
        int _feeId;

        #region properties
        public long PaymentId { get { return _PaymentId; } set { _PaymentId = value; } }
        public long PaymentType { get { return _PaymentType; } set { _PaymentType = value; } }
        public string PaymentTypeDesc { get { return _PaymentTypeDesc; } set { _PaymentTypeDesc = value; } }
        public long PaymentOwnerId { get { return _PaymentOwnerId; } set { _PaymentOwnerId = value; } }
        public int PaymentStatus { get { return _PaymentStatus; } set { _PaymentStatus = value; } }
        public int SyncStatus { get { return _SyncStatus; } set { _SyncStatus = value; } }
        public int PaymentMode { get { return _PaymentMode; } set { _PaymentMode = value; } }
        public int PaymentProvider { get { return _PaymentProvider; } set { _PaymentProvider = value; } }
        public decimal PrincipalAmount { get { return _PrincipalAmount; } set { _PrincipalAmount = value; } }
        public decimal ServiceTaxAmount { get { return _ServiceTaxAmount; } set { _ServiceTaxAmount = value; } }
        public decimal PaymentAmount { get { return _PaymentAmount; } set { _PaymentAmount = value; } }
        public int PayerUserId { get { return _PayerUserId; } set { _PayerUserId = value; } }
        public string PayerUsername { get { return _PayerUsername; } set { _PayerUsername = value; } }
        public string PayerName { get { return _PayerName; } set { _PayerName = value; } }
        public string PayerMobileNumber { get { return _PayerMobileNumber; } set { _PayerMobileNumber = value; } }
        public string PayerEmail { get { return _PayerEmail; } set { _PayerEmail = value; } }
        public long? ReceiptNumber { get { return _ReceiptNumber; } set { _ReceiptNumber = value; } }
        public string PaymentProviderRawResponse { get { return _PaymentProviderRawResponse; } set { _PaymentProviderRawResponse = value; } }
        public string TransactionResponseCode1 { get { return _TransactionResponseCode1; } set { _TransactionResponseCode1 = value; } }
        public string TransactionResponseCode2 { get { return _TransactionResponseCode2; } set { _TransactionResponseCode2 = value; } }
        public string TransactionResponseCode3 { get { return _TransactionResponseCode3; } set { _TransactionResponseCode3 = value; } }
        public string TransactionResponseCode4 { get { return _TransactionResponseCode4; } set { _TransactionResponseCode4 = value; } }
        public string DraftBankName { get { return _DraftBankName; } set { _DraftBankName = value; } }
        public string DraftBankBranch { get { return _DraftBankBranch; } set { _DraftBankBranch = value; } }
        public string DraftBankCity { get { return _DraftBankCity; } set { _DraftBankCity = value; } }
        public string DraftNumber { get { return _DraftNumber; } set { _DraftNumber = value; } }
        public DateTime? DraftIssueDate { get { return _DraftIssueDate; } set { _DraftIssueDate = value; } }
        public DateTime CreatedTime { get { return _CreatedTime; } set { _CreatedTime = value; } }
        public DateTime? ApprovalTime { get { return _ApprovalTime; } set { _ApprovalTime = value; } }
        public int UpdatedBy { get { return _UpdatedBy; } set { _UpdatedBy = value; } }
        public DateTime UpdatedTime { get { return _UpdatedTime; } set { _UpdatedTime = value; } }
        public string RejectionReason { get { return _RejectionReason; } set { _RejectionReason = value; } }
        public int feeId { get { return _feeId; } set { _feeId = value; } }


        #endregion

        internal cPaymentItem()
        {

        }

        internal cPaymentItem(long prm_PaymentId, long prm_PaymentType, string prm_PaymentTypeDesc, long prm_PaymentOwnerId, int prm_PaymentStatus, int prm_SyncStatus,
            int prm_PaymentMode, int prm_PaymentProvider, decimal prm_PrincipalAmount, decimal prm_ServiceTaxAmount, decimal prm_PaymentAmount, int prm_PayerUserId,
            string prm_PayerUsername, string prm_PayerName, string prm_PayerMobileNumber, string prm_PayerEmail, long? prm_ReceiptNumber, string prm_PaymentProviderRawResponse,
            string prm_TransactionResponseCode1, string prm_TransactionResponseCode2, string prm_TransactionResponseCode3, string prm_TransactionResponseCode4,
            string prm_DraftBankName, string prm_DraftBankBranch, string prm_DraftBankCity, string prm_DraftNumber, DateTime? prm_DraftIssueDate, DateTime prm_CreatedTime,
            DateTime? prm_ApprovalTime, int prm_UpdatedBy, DateTime prm_UpdatedTime, string prm_RejectionReason, int prm_feeId)
        {
            PaymentId = prm_PaymentId;
            PaymentType = prm_PaymentType;
            PaymentTypeDesc = prm_PaymentTypeDesc;
            PaymentOwnerId = prm_PaymentOwnerId;
            PaymentStatus = prm_PaymentStatus;
            SyncStatus = prm_SyncStatus;
            PaymentMode = prm_PaymentMode;
            PaymentProvider = prm_PaymentProvider;
            PrincipalAmount = prm_PrincipalAmount;
            ServiceTaxAmount = prm_ServiceTaxAmount;
            PaymentAmount = prm_PaymentAmount;
            PayerUserId = prm_PayerUserId;
            PayerUsername = prm_PayerUsername;
            PayerName = prm_PayerName;
            PayerMobileNumber = prm_PayerMobileNumber;
            PayerEmail = prm_PayerEmail;
            ReceiptNumber = prm_ReceiptNumber;
            PaymentProviderRawResponse = prm_PaymentProviderRawResponse;
            TransactionResponseCode1 = prm_TransactionResponseCode1;
            TransactionResponseCode2 = prm_TransactionResponseCode2;
            TransactionResponseCode3 = prm_TransactionResponseCode3;
            TransactionResponseCode4 = prm_TransactionResponseCode4;
            DraftBankName = prm_DraftBankName;
            DraftBankBranch = prm_DraftBankBranch;
            DraftBankCity = prm_DraftBankCity;
            DraftNumber = prm_DraftNumber;
            DraftIssueDate = prm_DraftIssueDate;
            CreatedTime = prm_CreatedTime;
            ApprovalTime = prm_ApprovalTime;
            UpdatedBy = prm_UpdatedBy;
            UpdatedTime = prm_UpdatedTime;
            RejectionReason = prm_RejectionReason;
            feeId = prm_feeId;
        }

    }
}
