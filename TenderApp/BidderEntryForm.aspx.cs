using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessAccessLayer;
using System.Data;
using System.IO;
using ICSIMemberPaymentLibrary.BL.Master;
using ICSIMemberPaymentLibrary.BL.Payment;
using ICSIMemberPaymentLibrary.BL.Communication;
using ICSIMemberPaymentLibrary.Helper;
using System.Net.Mail;
using System.Net;
namespace TenderApp
{
    public partial class BidderEntryForm : System.Web.UI.Page
    {
        cMemberPayment oMemPay = new cMemberPayment();
        cCommunication objCommunication = new cCommunication();
        cPayment oPay = new cPayment();
        DataSet dsrequest = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTenderList();
            }

        }
        public void BindTenderList()
        {
            try
            {
                BidderBL objBidder = new BidderBL();
                DataSet ds = new DataSet();
                ds = objBidder.GetTenderList();
                ddlTenderName.DataSource = ds;
                ddlTenderName.DataTextField = "TENDERNAME";
                ddlTenderName.DataValueField = "TENDERID";
                ddlTenderName.DataBind();
                ddlTenderName.Items.Insert(0, "----Select----");
            }
            catch (Exception)
            {
                
                throw;
            }
           

        }

        protected void ddlFeeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlTenderName.SelectedItem.Value != "0" && ddlFeeType.SelectedValue!="0")
                {
                    BidderBL objBidder = new BidderBL();
                    DataSet ds = new DataSet();
                    ds = objBidder.GetTenderAmt(Convert.ToInt32(ddlTenderName.SelectedValue), ddlFeeType.SelectedValue);
                    decimal cost = Convert.ToDecimal(ds.Tables[0].Rows[0]["TENDERFEES"].ToString());
                      lblAmt.Text=cost.ToString("#,##0.00");
                   
                    lblPayAble.Text = "Payable Amount : ";
                    
                }
                
            }
            catch (Exception)
            {

                ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('Please select tender name and fee type')", true);
            }
           
        }

        protected void btnPayment_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkdisclaimer.Checked==true)
                {
                    
                if (ddlTenderName.SelectedValue != "0" && ddlFeeType.SelectedValue != "0")
                {
                    //this.GetDataReadyForMemberPaymentRequestInsert();
                    //oMemPay.UpdateMemberPaymentRequest(oMemPay.MemPayItem);
                    int feeid=0;
                    if (ddlFeeType.SelectedValue=="F")
                    {
                        feeid = 4100;
                    }
                    else
                    {
                        feeid = 553;
                    }
                    BidderBL objBidder = new BidderBL();
                    dsrequest = new DataSet();
                    dsrequest = objBidder.InsertUpdateBidderTransactionData(0, 0, 0, txtBusinessName.Text, txtAddress.Text, "", "", 0, txtGSTNo.Text,
                       txtPAN.Text, txtContactPerson.Text, Convert.ToInt64(txtMobile.Text), txtEmail.Text, ddlTenderName.SelectedValue,
                       ddlFeeType.SelectedItem.Text, Convert.ToDecimal(String.Concat(lblAmt.Text.Split(','))), feeid);
                    if (dsrequest.Tables.Count > 0)
                    {
                        string id = dsrequest.Tables[0].Rows[0][0].ToString();
                        int requestid = Convert.ToInt32(id);
                        if (requestid > 0)
                        {
                            oPay.PayItem.PaymentOwnerId = requestid;
                            oPay.PayItem.PaymentType = feeid;
                          
                            this.GetDataReadyForPaymentInsert();
                            oPay.ProcessPayment(oPay.PayItem);
                            if (oPay.PayItem.PaymentId > 0)
                            {
                                oMemPay.MemPayItem.PaymentId = oPay.PayItem.PaymentId;
                                oMemPay.MemPayItem.UpdatedTime = DateTime.Now;

                                // oMemPay.UpdateMemberPaymentRequest(oMemPay.MemPayItem);
                                objBidder.InsertUpdateBidderTransactionData(requestid, oPay.PayItem.PaymentId, 0, txtBusinessName.Text, txtAddress.Text, "", "", 0, txtGSTNo.Text,
                         txtPAN.Text, txtContactPerson.Text, Convert.ToInt64(txtMobile.Text), txtEmail.Text, ddlTenderName.SelectedValue,
                         ddlFeeType.SelectedItem.Text, Convert.ToDecimal(String.Concat(lblAmt.Text.Split(','))), feeid);
                                Session.Add("PaymentId", oPay.PayItem.PaymentId);
                                Session.Add("ReqID", requestid);
                                Session.Add("fees", Convert.ToDecimal(String.Concat(lblAmt.Text.Split(','))));

                                //_Redirect("https://www.icsi.in/StudentMemberPages/Payment/PaymentHDFC.aspx", 0);
                                //Response.Redirect("https://www.icsi.in/StudentMemberPages/Payment/PaymentHDFC.aspx");
                                //_Redirect("https://www.icsi.in/StudentMemberPages/PaymentHDFC.aspx", 0);
                                //Response.Redirect("https://www.icsi.in/StudentMemberPages/PaymentHDFC.aspx");
                                Response.Redirect("~/Payment/PaymentHDFC.aspx");
                            }

                        }
                    }
                    ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('Data insert sucessfully')", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('Please select tender name & fee type')", true);
                }
            }
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('Please checked disclaimer')", true);
                }
            }
            catch (Exception)
            {

                ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('ther are some problem')", true);
            }

        }
        protected void GetDataReadyForPaymentInsert()
        {
            //int requestid = Convert.ToInt32(dsrequest.Tables[0].Rows[0]["RequestId"]);
            oPay.PayItem.PaymentId = 0;
           // oPay.PayItem.PaymentType = 2020;
            oPay.PayItem.PaymentTypeDesc = ddlFeeType.SelectedItem.Text;
            //oPay.PayItem.PaymentOwnerId = prm_PaymentOwnerId;
            oPay.PayItem.PaymentStatus = 0;
            oPay.PayItem.SyncStatus = 0;
            oPay.PayItem.PaymentMode = 6;// for billdesk 10, for axis=2,for challan 5, for hdfc 6
            oPay.PayItem.PaymentProvider = 9;// for billdesk 6, for axis=7,for challan 5, for hdfc 9
            oPay.PayItem.PaymentAmount = Convert.ToDecimal(Convert.ToDecimal(String.Concat(lblAmt.Text.Split(','))));
            if (oPay.PayItem.PaymentType == 4100)
            {
                oPay.PayItem.PrincipalAmount = (Convert.ToDecimal(String.Concat(lblAmt.Text.Split(','))) * 100) / 118;
                //oPay.PayItem.PaymentAmount = Convert.ToDecimal(oPay.PayItem.PrincipalAmount*0.18);
                oPay.PayItem.ServiceTaxAmount = (Convert.ToDecimal(String.Concat(lblAmt.Text.Split(','))) - (oPay.PayItem.PrincipalAmount));
            }
            else
            {
                oPay.PayItem.PrincipalAmount = Convert.ToDecimal(String.Concat(lblAmt.Text.Split(',')));
                oPay.PayItem.ServiceTaxAmount = 0;
            }
           
           
            oPay.PayItem.PayerUserId = Convert.ToInt32(ddlTenderName.SelectedValue);
            oPay.PayItem.PayerUsername = txtContactPerson.Text;
            oPay.PayItem.PayerName = txtBusinessName.Text;
            oPay.PayItem.PayerMobileNumber = txtMobile.Text;
            oPay.PayItem.PayerEmail = txtEmail.Text;
            oPay.PayItem.ReceiptNumber = 0;
            oPay.PayItem.PaymentProviderRawResponse = "";
            oPay.PayItem.TransactionResponseCode1 = "";
            oPay.PayItem.TransactionResponseCode2 = "";
            oPay.PayItem.TransactionResponseCode3 = "";
            oPay.PayItem.TransactionResponseCode4 = "";
            oPay.PayItem.DraftBankName = "";
            oPay.PayItem.DraftBankBranch = "";
            oPay.PayItem.DraftBankCity = "";
            oPay.PayItem.DraftNumber = "";
            //oPay.PayItem.DraftIssueDate = System.DateTime.Now;
            oPay.PayItem.CreatedTime = System.DateTime.Now;
            oPay.PayItem.ApprovalTime = System.DateTime.Now;
            oPay.PayItem.UpdatedBy = Convert.ToInt32(ddlTenderName.SelectedValue);
            oPay.PayItem.UpdatedTime = System.DateTime.Now;
            oPay.PayItem.RejectionReason = "";
            // oPay.PayItem.feeId =4915;
            Session.Add("Mobile", txtMobile.Text);
            Session.Add("FullName", txtBusinessName.Text);
            Session.Add("Email", txtEmail.Text);
        }
        public void sendmailSMS(int otp)
        {
           
            //objCommunication.enqueueEmail(ddlTenderName.Text,txtEmail.Text,"For activation code", otp.ToString(), MessageTypeEnum.Transactional, MessageStatusEnum.Submitted);
           // SendMail(otp);
            string strUrl = "http://smsgw.sms.gov.in/failsafe/HttpLink?username=icsi.sms&pin=ak8986bj&message=" + otp + "&mnumber=" + txtMobile.Text + "&signature=ICSIHQ";
           WebRequest request = HttpWebRequest.Create(strUrl);
           // Get the response back  
           HttpWebResponse response = (HttpWebResponse)request.GetResponse();
           Stream s = (Stream)response.GetResponseStream();
           StreamReader readStream = new StreamReader(s);
           string dataString = readStream.ReadToEnd();
           response.Close();
           s.Close();
           readStream.Close(); 
            //Now Push in SMS Queue.
            //objCommunication.enqueueSMS(ddlTenderName.Text, txtMobile.Text, otp.ToString(), MessageTypeEnum.Transactional, MessageStatusEnum.Submitted);
        }
        public int GenerateRandomNo()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }
        public void SendMail(int otp)
        {
            try
            {
                SmtpClient smtp = new SmtpClient("webmail.icsi.edu", 25);
                smtp.Credentials = new System.Net.NetworkCredential("donotreply@icsi.edu", "password@123");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                MailMessage mail = new MailMessage("donotreply@icsi.edu", txtEmail.Text);
                mail.Subject = "Donotreply";
                mail.Body = otp.ToString();
                smtp.Send(mail);
           
            }
            catch (Exception)
            {
                
                //throw;
            }         
           
            

            //Or your Smtp Email ID and Password
       
           
        }


        protected void txtotp_TextChanged(object sender, EventArgs e)
        {
            try
            {
                lblOtp.Text = "";
                BidderBL objBidder1 = new BidderBL();
                //int otp = GenerateRandomNo();
                dsrequest = new DataSet();
                dsrequest = objBidder1.GetOTPDETAIL(Convert.ToInt64(txtMobile.Text), txtEmail.Text, Convert.ToInt32(txtotp.Text), 1);
                if (dsrequest.Tables.Count > 0)
                {
                    string id = dsrequest.Tables[0].Rows[0][0].ToString();
                    if (id == "1")
                    {
                        //btnPayment.Enabled = true;
                    }
                    else
                    {
                       // btnPayment.Enabled = false;
                        lblOtp.Text = "Please enter valid OTP ID";
                        //ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('Please Enter Valid OTP ID')", true);
                        txtotp.Text = "";
                    }
                }
            }
            catch (Exception)
            {
                lblOtp.Text = "Please enter valid OTP ID";

               // ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('Please Enter Valid OTP ID')", true);
            }
           
        }

        protected void btn_otp_Click(object sender, EventArgs e)
        {
            try
            {
                Label3.Text = "";
                lblMobile.Text = "";
              
                if (txtMobile.Text != "")
                {
                    if (txtEmail.Text != "")
                    {
                        if (txtMobile.Text.Length==10)
                        {
                            BidderBL objBidder = new BidderBL();
                            int otp = GenerateRandomNo();
                            dsrequest = new DataSet();
                            dsrequest = objBidder.GetOTPDETAIL(Convert.ToInt64(txtMobile.Text), txtEmail.Text, otp, 0);
                            sendmailSMS(otp);
                            ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('Otp has been send on your mobile & email')", true);
                        }
                        else
                        {
                            lblMobile.Text = "Please enter 10 digit in Mobile No.";
                        }
                        
                    }
                    else
                    {
                        Label3.Text = "Please enter Email ID";
                    }
                }
                else
                {
                    if (txtMobile.Text == "")
                    {
                        lblMobile.Text = "Please enter Mobile No";
                    }
                    if (txtEmail.Text=="")
                    {
                         Label3.Text = "Please enter Email ID";
                    }
                    if (txtEmail.Text != "")
                    {
                        Label3.Text = "";
                    }
                    if (txtMobile.Text != "")
                    {
                        lblMobile.Text = "";
                    }
                    
                }
               
            }
            catch (Exception)
            {
                //if (txtEmail.Text=="")
                //{
                //    Label3.Text = "Please Enter Email ID";
                //}
                //else
                //{
                //    Label3.Text = "";
                //}
                

                //ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('please insert email')", true);
            }
        }

        protected void ddlTenderName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BidderBL objBidder = new BidderBL();
                DataSet ds = new DataSet();
                ds = objBidder.GetTenderDateTime(Convert.ToInt32(ddlTenderName.SelectedValue));
                DataSet ds1 = new DataSet();
                ds1 = objBidder.GetTenderNumber(Convert.ToInt32(ddlTenderName.SelectedValue));
                ddlTenderNo.DataSource = ds1;
                ddlTenderNo.DataTextField = "TENDERNO";
                ddlTenderNo.DataValueField = "TENDERNO";
                ddlTenderNo.DataBind();
                ddlTenderNo.Items.Insert(0, "----Select----");
                string closdt = Convert.ToDateTime(ds.Tables[0].Rows[0]["CLOSINGDATE"]).ToString("dd/MM/yyyy HH:mm tt");
                string extdt = Convert.ToDateTime(ds.Tables[0].Rows[0]["EXTENDEDDATE"]).ToString("dd/MM/yyyy HH:mm tt");
                if (Convert.ToDateTime(ds.Tables[0].Rows[0]["CLOSINGDATE"]).ToString("dd/MM/yyyy HH:mm tt") == Convert.ToDateTime(ds.Tables[0].Rows[0]["EXTENDEDDATE"]).ToString("dd/MM/yyyy HH:mm tt"))
                {
                    Label1.Text = "Tender/RFP/EOI/RFQ Closing Date Time : " + closdt;
                }
                else
                {
                    Label1.Text = "Tender/RFP/EOI/RFQ Extended Date Time : " + extdt;
                }
            
            }
            catch (Exception)
            {
                
                //throw;
            }
           
           
            
           
           
        }

    }
}