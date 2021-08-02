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
namespace TenderApp
{
    public partial class WebForm1 : System.Web.UI.Page
    {
       // MemberBL Member = new MemberBL();
        cMemberPayment oMemPay = new cMemberPayment();
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
            BidderBL objBidder = new BidderBL();
            DataSet ds = new DataSet();
            ds = objBidder.GetTenderList();
            ddlTenderName.DataSource = ds;
            ddlTenderName.DataTextField = "TENDERNAME";
            ddlTenderName.DataValueField = "TENDERID";
            ddlTenderName.DataBind();
            ddlTenderName.Items.Insert(0, "----Select----");
            
        }

        protected void ddlFeeType_SelectedIndexChanged(object sender, EventArgs e)
        {
             BidderBL objBidder = new BidderBL();
            DataSet ds = new DataSet();
            ds = objBidder.GetTenderAmt(Convert.ToInt32(ddlTenderName.SelectedValue), ddlFeeType.SelectedValue);
            lblAmt.Text = ds.Tables[0].Rows[0]["TENDERFEES"].ToString();
        }

        protected void btnPayment_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlTenderName.SelectedValue!="0" && ddlFeeType.SelectedValue!="0")
                {
                    //this.GetDataReadyForMemberPaymentRequestInsert();
                    //oMemPay.UpdateMemberPaymentRequest(oMemPay.MemPayItem);
                    BidderBL objBidder = new BidderBL();
                     dsrequest = new DataSet();
                     dsrequest = objBidder.InsertUpdateBidderTransactionData(0, 0, 0, txtBusinessName.Text, txtAddress.Text, "", "",0, txtGSTNo.Text,
                        txtPAN.Text, txtContactPerson.Text,Convert.ToInt64(txtMobile.Text), txtEmail.Text, ddlTenderName.SelectedValue,
                        ddlFeeType.SelectedItem.Text, Convert.ToDecimal(lblAmt.Text));
                     if (dsrequest.Tables.Count > 0)
                   {
                         string id= dsrequest.Tables[0].Rows[0][0].ToString();
                         int requestid = Convert.ToInt32(id);
                       if (requestid > 0)
                       {
                           oPay.PayItem.PaymentOwnerId = requestid;
                           this.GetDataReadyForPaymentInsert();
                           oPay.ProcessPayment(oPay.PayItem);
                           if (oPay.PayItem.PaymentId > 0)
                           {
                               oMemPay.MemPayItem.PaymentId = oPay.PayItem.PaymentId;
                               oMemPay.MemPayItem.UpdatedTime = DateTime.Now;

                              // oMemPay.UpdateMemberPaymentRequest(oMemPay.MemPayItem);
                               objBidder.InsertUpdateBidderTransactionData(requestid, oPay.PayItem.PaymentId, 0, txtBusinessName.Text, txtAddress.Text, "", "", 0, txtGSTNo.Text,
                        txtPAN.Text, txtContactPerson.Text, Convert.ToInt64(txtMobile.Text), txtEmail.Text, ddlTenderName.SelectedValue,
                        ddlFeeType.SelectedItem.Text, Convert.ToDecimal(lblAmt.Text));
                               Session.Add("PaymentId", oPay.PayItem.PaymentId);
                               Session.Add("ReqID", requestid);
                               Session.Add("fees", Convert.ToDecimal(lblAmt.Text));

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
            catch (Exception)
            {

                ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('ther are some problem')", true);
            }
        
        }
        protected void GetDataReadyForPaymentInsert()
        {
            //int requestid = Convert.ToInt32(dsrequest.Tables[0].Rows[0]["RequestId"]);
            oPay.PayItem.PaymentId = 0;
            oPay.PayItem.PaymentType =2020;
            oPay.PayItem.PaymentTypeDesc =ddlFeeType.SelectedItem.Text;
            //oPay.PayItem.PaymentOwnerId = prm_PaymentOwnerId;
            oPay.PayItem.PaymentStatus = 0;
            oPay.PayItem.SyncStatus = 0;
            oPay.PayItem.PaymentMode = 6;// for billdesk 10, for axis=2,for challan 5, for hdfc 6
            oPay.PayItem.PaymentProvider = 9;// for billdesk 6, for axis=7,for challan 5, for hdfc 9
            oPay.PayItem.PrincipalAmount = Convert.ToDecimal(lblAmt.Text.Trim());
            oPay.PayItem.ServiceTaxAmount = 0;
            oPay.PayItem.PaymentAmount = Convert.ToDecimal(lblAmt.Text.Trim());
            oPay.PayItem.PayerUserId = Convert.ToInt32(ddlTenderName.SelectedValue);
            oPay.PayItem.PayerUsername =txtContactPerson.Text;
            oPay.PayItem.PayerName =txtBusinessName.Text;
            oPay.PayItem.PayerMobileNumber =txtMobile.Text;
            oPay.PayItem.PayerEmail =txtEmail.Text;
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
            Session.Add("Mobile",txtMobile.Text);
            Session.Add("FullName",txtBusinessName.Text);
            Session.Add("Email", txtEmail.Text);
        }
        
    }
}