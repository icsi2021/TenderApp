using System;
using System.Collections.Generic;
using System.Text;

namespace ICSIMemberPaymentLibrary.BL.Payment
{
    public enum PaymentProviderEnum
    {
        Unknown = 0,
        Citi = 1,
        TechProcess = 2,
        BankDraft = 3,
        ElectronicTransfer = 4,
        Challan = 5,
        BillDesk = 6,
        AxisBank = 7,
        Paypal = 100
    }
}
