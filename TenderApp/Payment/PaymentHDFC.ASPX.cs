using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using ICSIMemberPaymentLibrary.BL.Payment;
using RazorpaySampleApp.BL.Mlib;
using CCA.Util;

namespace PwC.ICSI.UI.StudentMemberPages.Payment
{
    public partial class PaymentHDFC : System.Web.UI.Page
    {
        public string str = "";
        public string ReqID = "";
        public string PaymentId = "";
        //public decimal fees;
        public string PayIdAndTranId = "";
        //public string orderId="";
        cPayment objPay = new cPayment();

        CCACrypto ccaCrypto = new CCACrypto();
        string workingKey = "";//D8A1A5084B95AC9DE0FAE4031D106E6F"; //put in the 32bit alpha numeric key in the quotes provided here.	
        string ccaRequest = "";
        public string strEncRequest = "";
        public string strAccessCode = "";//AVVA90HB50BB35AVBB";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["PaymentId"] != null)
                {
                    PayIdAndTranId = "Your Payment Id is: " + Session["PaymentId"].ToString().Trim() + " and Request Id is : " + Session["ReqID"].ToString().Trim() + ". Please note the same for future use.";
                    string l_Tid = string.Empty;
                    DateTime CreatedTime = System.DateTime.Now;
                    l_Tid = CreatedTime.Year.ToString() + CreatedTime.Month.ToString("00") + CreatedTime.Day.ToString("00") +CreatedTime.Hour.ToString("00") +CreatedTime.Minute.ToString("00") +CreatedTime.Second.ToString("00");
                    string str = string.Empty;
                    //paygtaeurl += ConfigurationManager.AppSettings["HDFCMerchantId"].ToString() + "|" + Session["PaymentId"].ToString() + "|NA|" + Session["fees"].ToString() + "|NA|NA|NA|INR|DIRECT|R|" + ConfigurationManager.AppSettings["HDFCMerchantSecureId"].ToString() + "|NA|NA|F|";
                    //paygtaeurl += Session["ReqID"].ToString() + "|" + Session["Mobile"].ToString() + "|" + Session["Email"].ToString() + "|NA|NA|NA|NA|" + ConfigurationManager.AppSettings["HDFCReturnURL"].ToString();

                    string HDFCpaygateurlBeforeEncrpt = string.Empty;
                    string HDFCpaygateurlAfterEncrpt = string.Empty;
                    HDFCpaygateurlBeforeEncrpt += "";
                    //HDFCpaygateurlBeforeEncrpt += "tid=1581936477404";
                    HDFCpaygateurlBeforeEncrpt += "merchant_id=" + ConfigurationManager.AppSettings["HDFCMerchantId"].ToString();
                    HDFCpaygateurlBeforeEncrpt += "&order_id=" + Session["PaymentId"].ToString() + "&amount=" + Session["fees"].ToString() + "&currency=INR";
                    HDFCpaygateurlBeforeEncrpt += "&redirect_url=" + ConfigurationManager.AppSettings["HDFCReturnURL"].ToString();
                    HDFCpaygateurlBeforeEncrpt += "&cancel_url=" + ConfigurationManager.AppSettings["HDFCancelURL"].ToString();
                    HDFCpaygateurlBeforeEncrpt += "&merchant_param1=" + Session["FullName"].ToString();
                    HDFCpaygateurlBeforeEncrpt += "&merchant_param2=" + Session["Email"].ToString();
                    HDFCpaygateurlBeforeEncrpt += "&merchant_param3=" + Session["Mobile"].ToString();
                    HDFCpaygateurlBeforeEncrpt += "&merchant_param4=" + Session["PaymentId"].ToString();
                    HDFCpaygateurlBeforeEncrpt += "&tid=" + l_Tid.ToString();
                    //HDFCpaygateurlBeforeEncrpt += "&merchant_param4= NA" ;
                    //HDFCpaygateurlBeforeEncrpt += "&merchant_param5= NA" ;


                    ReqID = "Ref No:- ICSIPMT" + Session["PaymentId"];
                    long paymentid = (long)Session["PaymentId"];
                    objPay.PayItem = objPay.GetPaymentDetails(paymentid);
                    objPay.PayItem.PaymentProviderRawResponse = HDFCpaygateurlBeforeEncrpt;
                    objPay.ProcessPayment(objPay.PayItem);


                    //ReqID = "Ref No:- " + Session["PaymentId"];
                    //str = HDFCpaygateurlAfterEncrpt;


                    //workingKey = "D8A1A5084B95AC9DE0FAE4031D106E6F";
                    workingKey = ConfigurationManager.AppSettings["HDFCWorkingKey"].ToString();
                    strAccessCode = ConfigurationManager.AppSettings["HDFCMerchantSecureId"].ToString(); //"AVVA90HB50BB35AVBB"; // ConfigurationManager.AppSettings["HDFCMerchantSecureId"].ToString();

                    strEncRequest = ccaCrypto.Encrypt(HDFCpaygateurlBeforeEncrpt, workingKey);
                    //str = strEncRequest;
                    //Response.Redirect(HDFCpaygateurlAfterEncrpt);

                    //Response.Write("workingKey::" + workingKey);
                    //Response.Write("\nstrAccessCode::" + strAccessCode);
                    //Response.Write("\nstrEncRequest::" + strEncRequest);
                    //Response.Write("\nHDFCpaygateurlBeforeEncrpt ::" + HDFCpaygateurlBeforeEncrpt);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}