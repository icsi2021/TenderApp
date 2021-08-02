using System;
using System.Web;
using System.Web.UI;

namespace ICSIMemberPaymentLibrary.BL.PaymentGateway
{
    public abstract class PaymentPageBase : Page
    {
        public PaymentPageBase()
        {
            base.Load += new EventHandler(this.Page_Load);
        }

        protected abstract void doAccessDenied();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Context.Request.HttpMethod == "GET")
            {
                this.renderPaymentForm();
                return;
            }
            if (this.Context.Request.HttpMethod == "POST")
            {
                this.processPaymentReturn();
                return;
            }
            this.doAccessDenied();
        }

        protected abstract void processPaymentReturn();

        protected abstract void renderPaymentForm();

        protected abstract void showMessage(string message);
    }
}
