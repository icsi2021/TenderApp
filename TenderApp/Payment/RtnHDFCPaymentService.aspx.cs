using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;

using System.Data;
using BusinessAccessLayer;

using ICSIMemberPaymentLibrary.BL.Payment;
using ICSIMemberPaymentLibrary.BL.Master;
using ICSIMemberPaymentLibrary.BL;
using ICSIMemberPaymentLibrary.BL.PaymentGateway;
using ICSIMemberPaymentLibrary.BL.Communication;
using ICSIMemberPaymentLibrary.Helper;
using CCA.Util;

namespace ICSIMemberPaymentLibrary.Payment
{
    public partial class RtnHDFCPaymentService : PaymentPageBase
    {
        cPayment objPay = new cPayment();
        cMemberPayment objMem = new cMemberPayment();
        cCommunication objCommunication = new cCommunication();
        cCommonFunctions objCom = new cCommonFunctions();

        BidderBL objBidder = new BidderBL();
        DataSet ds = new DataSet();
        
        CCACrypto ccaCrypto = new CCACrypto();
        /// <summary>
        /// 
        /// </summary>
        public RtnHDFCPaymentService()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        protected override void doAccessDenied()
        {
            this.showMessage("Access Denied...");
        }
        /// <summary>
        /// 
        /// </summary>
        protected override void processPaymentReturn()
        {
            long num = 0;
            string status = string.Empty;

            string l_Receipt = string.Empty;
            try
            {
                string workingKey = ConfigurationManager.AppSettings["HDFCWorkingKey"].ToString(); //put in the 32bit alpha numeric key in the quotes provided here

               // string encResponse = "order_id=3003129&tracking_id=309005872524&bank_ref_no=202006390982946&order_status=Success&failure_message=&payment_mode=Credit Card&card_name=Visa&status_code=null&status_message=Signature validation failed&currency=INR&amount=236.00&billing_name=&billing_address=&billing_city=&billing_state=&billing_zip=&billing_country=&billing_tel=&billing_email=&delivery_name=&delivery_address=&delivery_city=&delivery_state=&delivery_zip=&delivery_country=&delivery_tel=&merchant_param1=test 3 march&merchant_param2=ac.chand@gmail.com&merchant_param3=9145895755&merchant_param4=&merchant_param5=&vault=N&offer_type=null&offer_code=null&discount_value=0.0&mer_amount=236.00&eci_value=null&retry=N&response_code=676&billing_notes=&trans_date=03/03/2020 12:17:34&bin_country=RUSSIAN FEDERATION";
                string encResponse = ccaCrypto.Decrypt(Request.Form["encResp"], workingKey);               
                
                NameValueCollection Params = new NameValueCollection();
                string[] segments = encResponse.Split('&');
                foreach (string seg in segments)
                {
                    string[] parts = seg.Split('=');
                    if (parts.Length > 0)
                    {
                        string Key = parts[0].Trim();
                        string Value = parts[1].Trim();
                        Params.Add(Key, Value);
                    }
                }             

                for (int i = 0; i < Params.Count; i++)
                {
                    //Response.Write(Params.Keys[i] + " = " + Params[i] + "<br>");                    
                    
                    if (Params.Keys[i]=="order_id")
                    {
                        num = Convert.ToInt64(Params[i].ToString());
                    }
                    if (Params.Keys[i] == "order_status")
                    {
                        status = Params[i].ToString();
                    }                
                }
                if (num > 0)
                {
                    objPay.PayItem = objPay.GetPaymentDetails(num);
                    objPay.PayItem.PaymentProviderRawResponse = encResponse;
                    objPay.PayItem.UpdatedTime = DateTime.Now;

                    //Added to check payment Success or not by Ravish Start
                    if (status == "Success")
                    {
                        objPay.PayItem.PaymentStatus = (int)(PaymentStatusEnum.PaymentVerifiedByIcsiProcessor);
                        objPay.PayItem.ReceiptNumber = objPay.GetNextReceiptNumber();
                        objPay.PayItem.ApprovalTime = DateTime.Now; //here approval time of the payment will be stored which will also be considered as the receipt date

                        Session.Add("ReceiptNumber", objPay.PayItem.ReceiptNumber);
                        Session.Add("PaymentAmount", Convert.ToInt64(objPay.PayItem.PaymentAmount));
                    }
                    else 
                    {
                        objPay.PayItem.PaymentStatus = (int)(PaymentStatusEnum.OnlineTransactionFailed);
                    }

                    Session.Add("PAY", objPay);

                    objPay.ProcessPayment(objPay.PayItem);

                    if (status == "Success")
                    {                        
                       // objMem.MemPayItem = objMem.GetMembershipPaymentByRequestId(objPay.PayItem.PaymentOwnerId);                        
                        //objMem.MemPayItem.Status = 10;
                        ds = objBidder.GetBidderDetailByRequestID(objPay.PayItem.PaymentOwnerId);
                        Session.Add("MEM", ds);
                        long req = objPay.PayItem.PaymentOwnerId;
                        objBidder.InsertUpdateBidderTransactionData(req, 0, 10, "", "", "", "", 0, "", "", "", 0, "", "", "", 0, 0);
                        objPay = (cPayment)(Session["PAY"]);
                        DataTable dt = ds.Tables[0];
                       // objMem.UpdateMemberPaymentRequest(objMem.MemPayItem);
                        //objPay = (cPayment)(Session["PAY"]);
                        //objMem = (cMemberPayment)(Session["MEM"]);
                        
                        //l_Receipt = this.BuildReceipt(objPay, objMemDon);

                        //Session["Receipt"] = objPay.PayItem.ReceiptNumber.ToString() + "|" + objPay.PayItem.PaymentAmount.ToString() + "|"+"72";
                        //Session.Add("Receipt", Session["ReceiptNumber"].ToString() + "|" + Session["PaymentAmount"].ToString() + "|" + "72");
                        //Session.Add("Receipt", Session["ReceiptNumber"].ToString() + "|2950|" + "72");
                        //Response.Write(Session["Receipt"].ToString());
                        //Response.Redirect("https://www.icsi.in/StudentMemberPages/Receipt.aspx");
                        
                        //l_Receipt = objCom.BuildReceipt(objPay, objMem);

                        //this.ltrmsg.Text = l_Receipt;// this.BuildReceipt(objPay, objMemDon);

                        //ToDo: For Sending Mail and SMS activate below link
                        l_Receipt = objCom.BuildReceipt(objPay, dt);

                        this.ltrmsg.Text = l_Receipt;// this.BuildReceipt(objPay, objMemDon);

                        //ToDo: For Sending Mail and SMS activate below link
                        this.sendEmailAndSms(objPay, dt, l_Receipt);
                       
                    }
                    else
                    {
                        this.ltrmsg.Text = "Error:- Payment is not successful.";
                    }

                }
                else
                {
                    this.showMessage("The payment has already been processed, please try registering again or contact ICSI HelpDesk with your Payment Id.");
                }

            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                this.showMessage(string.Concat("Error:- ", exception.Message));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        protected override void renderPaymentForm()
        {
            this.doAccessDenied();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        protected override void showMessage(string message)
        {
            this.ctlPayCodePlaceholder.Controls.Add(new LiteralControl(message));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="prm_Pay"></param>
        /// <param name="prm_MemDon"></param>
        /// <returns></returns>
        private string BuildReceipt(cPayment prm_Pay, cMemberPayment prm_Mem)
        {
           System.Text.StringBuilder SB = new System.Text.StringBuilder();

            SB.Append("<table align='center' width='800px' style='border-style:solid' cellpadding='2' cellspacing='2' >");
            SB.Append(" <tr>");
            SB.Append("     <td colspan='6'>&nbsp;</td>");
            SB.Append(" </tr>");
            SB.Append(" <tr>");
            SB.Append("     <td align='left' width='10%'>Website</td>");
            SB.Append("     <td align='left' width='20%'>WWW.ICSI.EDU</td>");
            SB.Append("     <td width='20%'>&nbsp;</td>");
            SB.Append("     <td width='20%'>&nbsp;</td>");
            SB.Append("     <td width='10%' align='left'>PAN</td>");
            SB.Append("     <td width='20%' align='left'>AAATT1103F</td>");
            SB.Append(" </tr>");
            SB.Append(" <tr>");
            //SB.Append("     <td align='left'>E-mail</td>");
            //SB.Append("     <td align='left'>csbf@icsi.edu</td>");
            SB.Append("     <td>&nbsp;</td>");
            SB.Append("     <td>&nbsp;</td>");
            SB.Append("     <td>&nbsp;</td>");
            SB.Append("     <td>&nbsp;</td>");
            SB.Append("     <td align='left'>Phone</td>");
            SB.Append("     <td align='left'>011-45341020</td>");
            SB.Append(" </tr>");
            SB.Append(" <tr>");
            SB.Append("     <td colspan='6'>&nbsp;</td>");
            SB.Append(" </tr>");
            SB.Append(" </tr>");
            SB.Append(" <tr>");
            SB.Append("     <td colspan='6'><asp:Image ImageUrl='~/Style/ICSI_Logo.jpg' runat='server' ID='Img_Logo' /></td>");
            SB.Append(" </tr>");
            //SB.Append(" <tr align='center'>");
            //SB.Append("     <td colspan='6'><font size='5'><b>THE INSTITUTE OF COMPANy SECRETARIES</b></font></td>");
            //SB.Append(" </tr>");
            //SB.Append(" <tr align='center'>");
            //SB.Append("     <td colspan='6'>(Registered under the Societies Registration Act 1860)</td>");
            //SB.Append(" </tr>");
            //SB.Append(" <tr align='center'>");
            //SB.Append("     <td colspan='6'>C/o The Institite of Company Secretaries of India</td>");
            //SB.Append(" </tr>");
            SB.Append(" <tr align='center'>");
            SB.Append("     <td colspan='6'>'ICSI HOUSE',C-36,Sector-62, Noida - 201301 </td>");
            SB.Append(" </tr>");
            SB.Append(" <tr align='center'>");
            SB.Append("     <td colspan='6'>&nbsp;</td>");
            SB.Append(" </tr>");
            SB.Append(" <tr align='center'>");
            SB.Append("     <td colspan='6'><font size ='4'><b>RECEIPT</b></font></td>");
            SB.Append(" </tr>");
            SB.Append(" <tr align='center'>");
            SB.Append("     <td colspan='6'>&nbsp;</td>");
            SB.Append(" </tr>");
            SB.Append(" <tr>");
            SB.Append("     <td width='10%' align='left'>No. :</td>");
            SB.Append("     <td width='40%' align='left'>" + prm_Pay.PayItem.ReceiptNumber + "</td>");
            SB.Append("     <td width='10%' align='right' colspan='3'>Date:</td>");
            SB.Append("     <td width='40%' align='left'>" + Convert.ToDateTime(prm_Pay.PayItem.ApprovalTime).Date.ToString("dd/M/yyyy") + "</td>");
            SB.Append(" </tr>");
            SB.Append(" <tr align='center'>");
            SB.Append("     <td colspan='6'>&nbsp;</td>");
            SB.Append(" </tr>");
            SB.Append(" <tr align='justified'>");
            //SB.Append("     <td colspan='6'>Received with thanks from Mr./Ms./M/s " + prm_MemDon.MemDonItem.Name + ", <b>Address : </b>" + prm_MemDon.MemDonItem.Address + ", <b>(PAN): </b>" + prm_MemDon.MemDonItem.PanNumber + "a sum of Rs. <b>" + prm_Pay.PayItem.PaymentAmount.ToString("0.00") + " </b>towards " + prm_Pay.PayItem.PaymentTypeDesc + ".</td>");
            SB.Append("     <td colspan='6' align='left'>Received with thanks from Mr./Ms./M/s " + prm_Mem.MemPayItem.Name + ", <b>Address : </b>" + prm_Mem.MemPayItem.Address + ", <b>(PAN): </b>" + prm_Mem.MemPayItem.PanNumber + ", a sum of Rs. <b>" + prm_Pay.PayItem.PaymentAmount.ToString("0.00") + "(Rupees " + objCom.NumToWordBD((Int64)(prm_Pay.PayItem.PaymentAmount)) + "Only ) </b>towards " + prm_Pay.PayItem.PaymentTypeDesc + ".</td>");
            SB.Append(" </tr>");
            //SB.Append(" <tr align='justified'>");
            //SB.Append("     <td colspan='6'>Address: " + prm_MemDon.MemDonItem.Address + "</td>");
            //SB.Append(" </tr>");
            //SB.Append(" <tr align='justified'>");
            //SB.Append("     <td colspan='6'>(PAN): " + prm_MemDon.MemDonItem.PanNumber + "</td>");
            //SB.Append(" </tr>");
            //SB.Append(" <tr align='justified'>");
            //SB.Append("     <td colspan='6'>a sum of Rs. " + prm_Pay.PayItem.PaymentAmount + " towards " + prm_Pay.PayItem.PaymentTypeDesc + ".</td>");
            //SB.Append(" </tr>");
            SB.Append(" <tr align='justified'>");
            SB.Append("     <td colspan='6'>&nbsp;</td>");
            SB.Append(" </tr>");
            SB.Append(" <tr align='right'>");
            SB.Append("     <td colspan='6'>For THE INSTITUTE Of Company Secretaries of INDIA</td>");
            SB.Append(" </tr>");
            SB.Append(" <tr align='justified'>");
            SB.Append("     <td colspan='6'>&nbsp;</td>");
            SB.Append(" </tr>");
            SB.Append(" <tr align='left'>");
            SB.Append("     <td colspan='6'>");
            SB.Append("     </td>");
            SB.Append(" </tr>");
            SB.Append(" <tr align='justified'>");
            SB.Append("     <td colspan='6'>&nbsp;</td>");
            SB.Append(" </tr>");
            SB.Append(" <tr align='center'>");
            SB.Append("     <td colspan='6'>This is a computer generated receipt and requires no signature.</td>");
            SB.Append(" </tr>");
            SB.Append(" <tr align='justified'>");
            SB.Append("     <td colspan='6'>&nbsp;</td>");
            SB.Append(" </tr>");
            SB.Append("</table>");

            return SB.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Num"></param>
        /// <returns></returns>
        public static string NumToWordBD(Int64 Num)
        {
            string[] Below20 = { "", "One ", "Two ", "Three ", "Four ", "Five ", "Six " , "Seven ", "Eight ", "Nine ", "Ten ",
                                   "Eleven ", "Twelve " , "Thirteen ", "Fourteen ","Fifteen ", "Sixteen " , "Seventeen ","Eighteen " , "Nineteen " };
            string[] Below100 = { "", "", "Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ", "Seventy ", "Eighty ", "Ninety " };
            string InWords = "";
            if (Num >= 1 && Num < 20)
                InWords += Below20[Num];
            if (Num >= 20 && Num <= 99)
                InWords += Below100[Num / 10] + Below20[Num % 10];
            if (Num >= 100 && Num <= 999)
                InWords += NumToWordBD(Num / 100) + " Hundred " + NumToWordBD(Num % 100);
            if (Num >= 1000 && Num <= 99999)
                InWords += NumToWordBD(Num / 1000) + " Thousand " + NumToWordBD(Num % 1000);
            if (Num >= 100000 && Num <= 9999999)
                InWords += NumToWordBD(Num / 100000) + " Lac " + NumToWordBD(Num % 100000);
            if (Num >= 10000000)
                InWords += NumToWordBD(Num / 10000000) + " Crore " + NumToWordBD(Num % 10000000);
            return InWords;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="prm_Pay"></param>
        /// <param name="prm_MemDon"></param>
        /// <param name="prm_receipt"></param>
        private void sendEmailAndSms(cPayment prm_Pay, DataTable dt, string prm_receipt)
        {
            System.Text.StringBuilder MailPart = new System.Text.StringBuilder();
            System.Text.StringBuilder SMSPart = new System.Text.StringBuilder();
            string mailSubject = "Natural Disaster Donation";

            MailPart.Append("<br />Dear <b>" + prm_Pay.PayItem.PayerName + ",<br /><br />");
            MailPart.Append("Each drop fills the ocean. The contribution received from your esteemed self is very precious to the organisation and I deeply acknowledge your magnanimity and look forward to your support in all times to come.");
            MailPart.Append("<br /><br />");
            MailPart.Append("-Proud to be a CSBF member.");
            MailPart.Append("<br /><br />");
            MailPart.Append("President");
            MailPart.Append("<br />");
            MailPart.Append("ICSI</b>");
            MailPart.Append("<br /><br /><br /><br />");
            MailPart.Append("<b>RECEIPT</b>");
            //MailPart.Append(this.BuildReceipt(prm_Pay, prm_MemDon));

            MailPart.Append(prm_receipt);

            SMSPart.Append("Dear " + prm_Pay.PayItem.PayerName + ",");
            //SMSPart.Append("<br/>");
            SMSPart.Append(Environment.NewLine);
            SMSPart.Append("Thanks for your generousness to Tender.");
            //SMSPart.Append("<br/>");
            SMSPart.Append(Environment.NewLine);
            SMSPart.Append("-Proud to be Participation.");
            //SMSPart.Append("<br/>");
            SMSPart.Append(Environment.NewLine);
            SMSPart.Append("President");
            //SMSPart.Append("<br/>");
            SMSPart.Append(Environment.NewLine);
            SMSPart.Append("ICSI");

            //Now Push in Email Queue.
            objCommunication.enqueueEmail(prm_Pay.PayItem.PayerName, prm_Pay.PayItem.PayerEmail, mailSubject, MailPart.ToString(), MessageTypeEnum.Transactional, MessageStatusEnum.Submitted);

            //Now Push in SMS Queue.
            objCommunication.enqueueSMS(prm_Pay.PayItem.PayerName, prm_Pay.PayItem.PayerEmail, SMSPart.ToString(), MessageTypeEnum.Transactional, MessageStatusEnum.Submitted);
        }        

    }
}