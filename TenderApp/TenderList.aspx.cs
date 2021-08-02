using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessAccessLayer;
using System.IO;

namespace TenderApp
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        BidderBL objBidder = new BidderBL();
        AdminBL objAdmin = new AdminBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            try
            {
                string userid = Session["USER"].ToString();
                if (string.IsNullOrEmpty(userid))
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    if (!IsPostBack)
                    {
                        BindTenderList();
                    }
                }
            }
            catch (Exception)
            {

                //throw;
            }
            
        }
        public void BindTenderList()
        {
            try
            {
                string userid = Session["USER"].ToString();
                DataSet ds = new DataSet();

                ds = objAdmin.GetTender_ForByUser(Convert.ToInt32(userid));
               // string check=ds.Tables[0].Rows[0]["TENDERNAME"].ToString();
                
                ddlTenderNo.DataSource = ds;
                ddlTenderNo.DataTextField = "TENDERNAME";
                ddlTenderNo.DataValueField = "TENDERID";  
                ddlTenderNo.DataBind();
                ddlTenderNo.Items.Insert(0, "----Select----");
            }
            catch (Exception)
            {
                
                //throw;
            }
           
           

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlTenderNo.SelectedValue!="0" && ddlFeeType.SelectedValue!="0")
                {
                    DataSet ds = new DataSet();
                    ds = objAdmin.GetParticipantData(Convert.ToInt32(ddlTenderNo.SelectedValue), Convert.ToInt32(ddlFeeType.SelectedValue));
                   
                        gdvParticipationList.DataSource = ds;
                        gdvParticipationList.DataBind();
              
                   
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('Please select Tender Name and Fee Type')", true);
                }
               
            }
            catch (Exception)
            {

                //ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('Data not found')", true);
            }
           
        }
        public override void VerifyRenderingInServerForm(Control control)
        {

        }
        private void ExportGridToExcel()
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Charset = "";
                string FileName = "Tender_Participiant_List" + DateTime.Now + ".xls";
                StringWriter strwritter = new StringWriter();
                HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
                gdvParticipationList.GridLines = GridLines.Both;
                gdvParticipationList.HeaderStyle.Font.Bold = true;
                gdvParticipationList.RenderControl(htmltextwrtter);
                Response.Write(strwritter.ToString());
                Response.End();
            }
            catch (Exception)
            {
                
                //throw;
            }
           

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ExportGridToExcel();
        } 

        //protected void ddlTenderNo_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlTenderNo.SelectedValue=="0")
        //    {
        //        ddlFeeType.Enabled = false;
        //    }
        //    else
        //    {
        //        ddlFeeType.Enabled = true;
        //    }
        //}
    }
}