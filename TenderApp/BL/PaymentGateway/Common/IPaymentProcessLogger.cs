using System;

namespace ICSIMemberPaymentLibrary.BL.PaymentGateway.Common
{
    public interface IPaymentProcessLogger
    {
        void logRawPaymentRequest(string rawRequestMessage);

        void logRawPaymentResponse(string rawResponseMessage);
    }
}
