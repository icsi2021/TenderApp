using System;
using System.Collections.Generic;
using System.Text;

namespace ICSIMemberPaymentLibrary.BL.Payment
{
    public enum PaymentStatusEnum
    {
        PaymentCreated = 0,
        PaymentSubmittedForApproval = 1,
        PaymentVerifiedByProviderProcessor = 10,
        PaymentVerifiedByIcsiProcessor = 11,
        PaymentReconciled = 12,
        Refunded = 50,
        OnlineTransactionFailed = 100,
        OfflinePaymentRejected = 101,
        ErrorOccured = 102
    }
}
