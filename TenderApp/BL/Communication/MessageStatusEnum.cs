using System;


namespace ICSIMemberPaymentLibrary.BL.Communication
{
    public enum MessageStatusEnum
    {
        Submitted = 0,
        Processing = 1,
        Dispatched = 2,
        Failed = 100
    }
}
