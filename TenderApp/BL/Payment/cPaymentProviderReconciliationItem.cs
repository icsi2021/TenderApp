using System;
using System.Collections.Generic;
using System.Text;

namespace ICSIMemberPaymentLibrary.BL.Payment
{
    public class cPaymentProviderReconciliationItem
    {
        decimal _PaymentAmount;
        long _PaymentId;
        PaymentProviderEnum _PaymentProvider;
        DateTime? _PaymentProviderDate;
        PaymentStatusEnum _PaymentStatus;
        string _SystemMessage;
        string _TransactionResponseCode1;
        string _TransactionResponseCode2;
        string _TransactionResponseCode3;
        string _TransactionResponseCode4;

        #region properties
        
        public decimal PaymentAmount { get {return _PaymentAmount;} set {_PaymentAmount = value;} }
        public long PaymentId { get { return _PaymentId; } set { _PaymentId = value;} }
        public PaymentProviderEnum PaymentProvider { get { return _PaymentProvider; } set { _PaymentProvider = value;} }
        public DateTime? PaymentProviderDate { get { return _PaymentProviderDate; } set { _PaymentProviderDate = value; } }
        public PaymentStatusEnum PaymentStatus { get { return _PaymentStatus; } set { _PaymentStatus = value; } }
        public string SystemMessage { get { return _SystemMessage; } set { _SystemMessage = value; } }
        public string TransactionResponseCode1 { get { return _TransactionResponseCode1; } set { _TransactionResponseCode1 = value; } }
        public string TransactionResponseCode2 { get { return _TransactionResponseCode2; } set { _TransactionResponseCode2 = value; } }
        public string TransactionResponseCode3 { get { return _TransactionResponseCode3; } set { _TransactionResponseCode3 = value; } }
        public string TransactionResponseCode4 { get { return _TransactionResponseCode4; } set { _TransactionResponseCode4 = value; } }

        #endregion


        internal cPaymentProviderReconciliationItem()
        {

        }

        internal cPaymentProviderReconciliationItem(decimal prm_PaymentAmount, long prm_PaymentId, PaymentProviderEnum prm_PaymentProvider,
            DateTime? prm_PaymentProviderDate, PaymentStatusEnum prm_PaymentStatus, string prm_SystemMessage, 
            string prm_TransactionResponseCode1, string prm_TransactionResponseCode2, string prm_TransactionResponseCode3, string prm_TransactionResponseCode4
            )
        {

            PaymentAmount = prm_PaymentAmount;
            PaymentId = prm_PaymentId;
            PaymentProvider = prm_PaymentProvider;
            PaymentProviderDate = prm_PaymentProviderDate;
            PaymentStatus = prm_PaymentStatus;
            SystemMessage = prm_SystemMessage; 
            TransactionResponseCode1 = prm_TransactionResponseCode1;
            TransactionResponseCode2 = prm_TransactionResponseCode2;
            TransactionResponseCode3 = prm_TransactionResponseCode3;
            TransactionResponseCode4 = prm_TransactionResponseCode4;
            
        }
    }
}
