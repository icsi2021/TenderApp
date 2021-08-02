using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessAccessLayer;
using System.Data;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;  
namespace TenderApp
{
    public partial class WebForm2 : System.Web.UI.Page
    {
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

                        BindDepartment();
                        GetTendersList();
                    }
                }
           
            }
            catch (Exception)
            {
                
               // throw;
            }
               
           
          
           
        }
       

        protected void btnSave_Update_Click(object sender, EventArgs e)
        {
            AdminBL objAdmin = new AdminBL();
            txtExtendDateTime.Enabled = false;
            
            try
            {
                string userid = Session["USER"].ToString();
                string pdate = txtPublishDateTime.Text;
                string cdate = txtCloseDateTime.Text;
                lblMessage.Text = "";

                //DateTime startDate = Convert.ToDateTime(txtPublishDateTime.Text.Trim());
                //DateTime endDate = Convert.ToDateTime(txtCloseDateTime.Text.Trim());

                   DateTime pudate = DateTime.ParseExact(pdate, "dd'/'MM'/'yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                //     //pudate = Convert.ToDateTime(pdate).ToString("dd/MM/yyyy HH:mm:ss");
                    DateTime closedate = DateTime.ParseExact(cdate, "dd'/'MM'/'yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                    if (pudate < closedate)
                {
                    // Doyour stuff.
                }
                else
                {

                    txtCloseDateTime.Text = string.Empty;
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "", "alert('Purchase date should be less than End date')", true);
                }

                //if (string.IsNullOrEmpty(lblTenderid.Text))
                //{
                //    DateTime pudate = DateTime.ParseExact(pdate, "dd'/'MM'/'yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                //     //pudate = Convert.ToDateTime(pdate).ToString("dd/MM/yyyy HH:mm:ss");
                //    DateTime closedate = DateTime.ParseExact(cdate, "dd'/'MM'/'yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                //    //string[] dst = cdate.Split(' ');
                    
                //    //closedate =Convert.ToDateTime(dst[0]);


                   


                //    int DD = pudate.Day, MM = pudate.Month, YY = pudate.Year;
                //    int DD1 = closedate.Day, MM1 = closedate.Month, YY1 = closedate.Year;
                //    if (ddlDept.SelectedValue != "0")
                //    {//DateTime pudate,closedate;
                     
                //        if (MM== MM1 && YY== YY1 && YY >=2020)
                //        {
                //            if (DD <= DD1)
                //            {
                //                if (txtExtendDateTime.Text=="")
                //                {
                //                    objAdmin.InsertUpdateTenderDetail(0, txtName.Text, txtTenderNo.Text, pudate, closedate,
                //                   closedate, ddlDept.SelectedItem.Value.ToString(), Convert.ToDecimal(String.Concat(txtCost.Text.Split(','))), Convert.ToDecimal(String.Concat(txtEMD.Text.Split(','))), Convert.ToInt32(userid));
                //                    //ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('Data insert sucessfully')", true);
                //                    lblMessage.Text = "Record inserted sucessfully";
                //                    clear();
                //                    GetTendersList();
                //                }
                //                else
                //                {
                //                   DateTime ext= DateTime.ParseExact(txtExtendDateTime.Text, "dd'/'MM'/'yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                //                    objAdmin.InsertUpdateTenderDetail(0, txtName.Text, txtTenderNo.Text, pudate, closedate,
                //                                                      ext, ddlDept.SelectedItem.Value.ToString(), Convert.ToDecimal(String.Concat(txtCost.Text.Split(','))), Convert.ToDecimal(String.Concat(txtEMD.Text.Split(','))), Convert.ToInt32(userid));
                //                    //ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('Data insert sucessfully')", true);
                //                    lblMessage.Text = "Record inserted sucessfully";
                //                    clear();
                //                    GetTendersList();
                //                }
                              
                //            }
                //            else
                //            {
                //                ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('Please check date')", true);
                //            }
                //        }
                //        else
                //        {
                //            if (MM <= MM1 && YY <= YY1 && YY >= 2020)
                //            {
                //                if (txtExtendDateTime.Text=="")
                //                {
                //                      objAdmin.InsertUpdateTenderDetail(0, txtName.Text, txtTenderNo.Text, pudate, closedate,
                //                     closedate, ddlDept.SelectedItem.Value.ToString(), Convert.ToDecimal(String.Concat(txtCost.Text.Split(','))), Convert.ToDecimal(String.Concat(txtEMD.Text.Split(','))), Convert.ToInt32(userid));
                //                lblMessage.Text = "Record inserted sucessfully";
                //                    clear();
                //                    GetTendersList();
                //                }
                //                else
                //                {
                //                    DateTime ext = DateTime.ParseExact(txtExtendDateTime.Text, "dd'/'MM'/'yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                //                    objAdmin.InsertUpdateTenderDetail(0, txtName.Text, txtTenderNo.Text, pudate, closedate,
                //                  ext, ddlDept.SelectedItem.Value.ToString(), Convert.ToDecimal(String.Concat(txtCost.Text.Split(','))), Convert.ToDecimal(String.Concat(txtEMD.Text.Split(','))), Convert.ToInt32(userid));
                //                    lblMessage.Text = "Record inserted sucessfully";
                //                    clear();
                //                    GetTendersList();
                //                }
                             
                               
                //            }
                //            else
                //            {
                //                ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('Please check date')", true);
                //            }
                //        }
                        
                //    }
                //    else
                //    {
                //        ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('Please select department')", true);
                //    }
                //}
                //else
                //{
                //    DateTime pudate = DateTime.ParseExact(pdate, "dd'/'MM'/'yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                //    DateTime closedate = DateTime.ParseExact(txtCloseDateTime.Text, "dd'/'MM'/'yyyy HH:mm:ss", CultureInfo.InvariantCulture);// Convert.ToDateTime(txtCloseDateTime.Text);
                //    int DD1 = closedate.Day, MM1 = closedate.Month, YY1 = closedate.Year;
                //    DateTime extdate;
                //    if (txtExtendDateTime.Text!="")
                //    {
                //        extdate = DateTime.ParseExact(txtExtendDateTime.Text, "dd'/'MM'/'yyyy HH:mm:ss", CultureInfo.InvariantCulture);  //Convert.ToDateTime(txtExtendDateTime.Text);
                //    }
                //    else
                //    {
                //         extdate = DateTime.ParseExact(txtCloseDateTime.Text, "dd'/'MM'/'yyyy HH:mm:ss", CultureInfo.InvariantCulture);  //Convert.ToDateTime(txtExtendDateTime.Text);
                //    }
                   
                //    int DD2 = closedate.Day, MM2 = closedate.Month, YY2 = closedate.Year;
                //    if (string.IsNullOrEmpty(txtExtendDateTime.Text))
                //    {
                //        if (ddlDept.SelectedValue != "0")
                //        {
                //            if (MM1 == MM2 && YY1 == YY2 && YY1 >= 2020)
                //            {
                //                if (DD1 <= DD2)
                //                {
                //                    objAdmin.InsertUpdateTenderDetail(Convert.ToInt32(lblTenderid.Text), txtName.Text, txtTenderNo.Text, pudate, closedate,
                //                     closedate, ddlDept.SelectedItem.Value.ToString(), Convert.ToDecimal(String.Concat(txtCost.Text.Split(','))), Convert.ToDecimal(String.Concat(txtEMD.Text.Split(','))), Convert.ToInt32(userid));
                //                    lblMessage.Text = "Record updated sucessfully";
                //                    lblTenderid.Text = "";
                //                    clear();
                //                    GetTendersList();
                //                }
                //                else
                //                {
                                                              
                //                     ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('Please check date')", true);
                           
                //                }
                //            }
                //            else
                //            {
                //                if (MM1 <= MM2 && YY1 <= YY2 && YY1 >= 2020)
                //                {
                //                    objAdmin.InsertUpdateTenderDetail(Convert.ToInt32(lblTenderid.Text), txtName.Text, txtTenderNo.Text,pudate, closedate,
                //                     closedate, ddlDept.SelectedItem.Value.ToString(), Convert.ToDecimal(String.Concat(txtCost.Text.Split(','))), Convert.ToDecimal(String.Concat(txtEMD.Text.Split(','))), Convert.ToInt32(userid));
                //                    lblMessage.Text = "Record updated sucessfully";
                //                    lblTenderid.Text = "";
                //                    GetTendersList();
                //                    clear();
                //                }
                //                else
                //                {

                //                    ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('Please check date')", true);

                //                }
                //            }
                           
                //        }
                //        else
                //        {
                //            ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('Please select department')", true);
                //        }

                //    }
                //    else
                //    {
                //        if (ddlDept.SelectedValue != "0")
                //        {
                //            if (MM1 == MM2 && YY1 == YY2 && YY1 >= 2020)
                //            {
                //                if (DD1 <= DD2)
                //                {
                //                    objAdmin.InsertUpdateTenderDetail(Convert.ToInt32(lblTenderid.Text), txtName.Text, txtTenderNo.Text, pudate, closedate,
                //                extdate, ddlDept.SelectedItem.Value.ToString(), Convert.ToDecimal(String.Concat(txtCost.Text.Split(','))), Convert.ToDecimal(String.Concat(txtEMD.Text.Split(','))), Convert.ToInt32(userid));
                //                    lblMessage.Text = "Record updated sucessfully";
                //                    lblTenderid.Text = "";
                //                    clear();
                //                    GetTendersList();
                //                }
                //                else
                //                {
                //                    ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('Please select department')", true);
                //                }
                //            }
                //            else
                //            {
                //                if (MM1 <= MM2 && YY1 <= YY2 && YY1 >= 2020)
                //                {
                //                    objAdmin.InsertUpdateTenderDetail(Convert.ToInt32(lblTenderid.Text), txtName.Text, txtTenderNo.Text, pudate, closedate,
                //                      extdate, ddlDept.SelectedItem.Value.ToString(), Convert.ToDecimal(String.Concat(txtCost.Text.Split(','))), Convert.ToDecimal(String.Concat(txtEMD.Text.Split(','))), Convert.ToInt32(userid));
                //                    lblMessage.Text = "Record updated sucessfully";
                //                    lblTenderid.Text = "";
                //                    clear();
                //                    GetTendersList();
                //                }
                //                else
                //                {

                //                    ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('Please check date')", true);

                //                }
                //            }
                //        }
                //        else
                //        {
                //            ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('Please select department')", true);
                //        }
                //    }

                //}
            }
            catch (Exception ex)
            {
                
                //throw;
            }
            //DateTime dr;
           
           
        }
       
        public void GetTenderDetail_ById(int TenderID)
        {
            try
            {
                // lblMessage.Text = "";
                AdminBL objAdmin = new AdminBL();
                DataSet ds = new DataSet();
                ds = objAdmin.GetTenderDataByID(TenderID);
                txtName.Text = ds.Tables[0].Rows[0]["TENDERNAME"].ToString();
                txtTenderNo.Text = ds.Tables[0].Rows[0]["TENDERNO"].ToString();
                txtPublishDateTime.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["PUBLISHINGDATE"]).ToString("dd/MM/yyyy HH:mm:ss");
                // DateTime.ParseExact(txtCloseDateTime.Text, "dd'/'MM'/'yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                if (Convert.ToDateTime(ds.Tables[0].Rows[0]["CLOSINGDATE"]).ToString("dd/MM/yyyy HH:mm:ss") == Convert.ToDateTime(ds.Tables[0].Rows[0]["EXTENDEDDATE"]).ToString("dd/MM/yyyy HH:mm:ss"))
                {
                    txtCloseDateTime.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["CLOSINGDATE"]).ToString("dd/MM/yyyy HH:mm:ss");
                }
                else
                {
                    txtCloseDateTime.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["CLOSINGDATE"]).ToString("dd/MM/yyyy HH:mm:ss");
                    txtExtendDateTime.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["EXTENDEDDATE"]).ToString("dd/MM/yyyy HH:mm:ss");
                }

                lblTenderid.Text = ds.Tables[0].Rows[0]["TENDERID"].ToString();
                ddlDept.Text = ds.Tables[0].Rows[0]["PUBLISHINGDEPT"].ToString();
                decimal cost = Convert.ToDecimal(ds.Tables[0].Rows[0]["TENDERFEES"]);//
                txtCost.Text = cost.ToString("#,##0.00");
                decimal emd = Convert.ToDecimal(ds.Tables[0].Rows[0]["EMDAMT"]);
                txtEMD.Text = emd.ToString("#,##0.00");
            }
            catch (Exception)
            {
                
                //throw;
            }
          
            
            
            
        }

        public void BindDepartment()
        {
            try
            {
                AdminBL objAdmin = new AdminBL();
                DataSet ds = new DataSet();
                ds = objAdmin.GetDeptMentList();
                ddlDept.DataSource = ds;
                ddlDept.DataTextField = "DEPARTMENT";
                ddlDept.DataValueField = "DEPARTMENT1";
                ddlDept.DataBind();
                ddlDept.Items.Insert(0, "----Select----");
            }
            catch (Exception)
            {
                
              //  throw;
            }
           
            // ddlDept.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        public void GetTendersList()
        {
            try
            {
                string userid = Session["USER"].ToString();
                AdminBL objAdmin = new AdminBL();
                DataSet ds = new DataSet();
                ds = objAdmin.GetTenderDataListView_ForDPT(Convert.ToInt32(userid));
                grdv_tenderlistview.DataSource = ds;
                grdv_tenderlistview.DataBind();
            }
            catch (Exception)
            {
                
               // throw;
            }
           
            // ddlDept.Items.Insert(0, new ListItem("--Select--", "0"));
        }
       
        protected void grdv_tenderlistview_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string tenderid = (grdv_tenderlistview.SelectedRow.FindControl("lblTENDERID") as Label).Text;
                GetTenderDetail_ById(Convert.ToInt32(tenderid));
                txtExtendDateTime.Enabled = true;
            }
            catch (Exception)
            {
                
                //throw;
            }
               
           
        }

        protected void grdv_tenderlistview_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdv_tenderlistview.PageIndex = e.NewPageIndex;
                GetTendersList();
            }
            catch (Exception)
            {
                
                //throw;
            }
          
        }
  
   public string ParseDateOnChange(string dtDate) {
    var NewDate = new DateTime();
    var resultDate ="";
     if (dtDate =="") 
     {
         return null;
     }
     if (dtDate.Length >= 1)
     {

         NewDate = DateTime.ParseExact(dtDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);// Convert.ToDateTime(dtDate);
         int DD1 = NewDate.Day, MM1 = NewDate.Month, YY1 = NewDate.Year;
         string DD, MM, YY;
         DD = DD1.ToString();
         MM = MM1.ToString();
         YY = YY1.ToString();


         dtDate = dtDate.Replace("/ /g", "/");
         dtDate = dtDate.Replace("/[.]/g", "/");
         dtDate = dtDate.Replace("/-/g", "/");
         var arDate = dtDate.Split('/');
         if (arDate.Length >= 1)
         {
           
                     YY = arDate[2].ToString();
                     if (YY == "1")
                         YY = "0" + YY;
                     if (YY == "2")
                         YY = "20" + YY;
                     if (Convert.ToInt32(YY) > 2100 || Convert.ToInt32(YY) < 1900)
                     {
                         YY = "Error";
                     }
                
                     if (arDate[1].ToString() != "")
                     {
                         if (MM.Length>2)
                         {
                             MM = "Error";
                         }
                     }
                     if (Convert.ToInt32(MM) > 12)
                     {
                         MM = "Error";
                     }

               
                     if (arDate[0].ToString() != "")
                         DD = arDate[0].ToString();
                     if (Convert.ToInt32(DD) > 31)
                     {
                         DD = "Error";
                     }
                     if (DD== "Error"||MM== "Error"||YY == "Error")
                     {
                         resultDate = "Error";
                     }
                   
             }
            
        
         resultDate = DD + "/" + MM + "/" + YY;
     }
    
        return resultDate;
   
}
     public string ParseTimeSecond(string dtTime) {
    var NewDate = new DateTime();

    NewDate = DateTime.ParseExact(dtTime, "HH:mm:ss", CultureInfo.InvariantCulture);// Convert.ToDateTime(dtDate);
        if (dtTime != "")
        {

            var HH = NewDate.Hour.ToString();
            var MM = NewDate.Minute.ToString();
            var SS = NewDate.Second.ToString();
            if (HH.Length == 1)
            {
                HH = "0" + HH;
            }
            if (MM.Length == 1)
            {
                MM = "0" + MM;
            }
            if (SS.Length == 1)
            {
                SS = "0" + SS;
            }
            dtTime = HH + ":" + MM + ":" + SS;
        }
        dtTime = dtTime.Replace("/[.]/g", ":");
        var saTime = dtTime.Split(':');
        string hh = "00", mm = "00", ss = "00";

        if (saTime.Length >= 1)
        {


            if (saTime[2] != "")
            {
                ss = saTime[2];
                ss = ss.ToString();
            }
            if (ss.ToString().Length <2)
            {
                ss = "0" + ss.ToString();
            }

            if (Convert.ToInt32(ss) > 59 || ss == "")
            {
                hh = "Error";
            }

            if (saTime[1] != "")
            {
                mm = saTime[1];
                mm = mm.ToString();
            }
            if (mm.ToString().Length<2)
            {
                mm = "0" + mm.ToString();
            }
            if (Convert.ToInt32(mm) > 59 || mm == "")
            {
                hh = "Error";
            }

            hh = saTime[0];
            if (hh.Length <2)
                hh = "0" + hh.ToString();
            if (Convert.ToInt32(hh) > 23 || Convert.ToInt32(hh) < 0)
            {
                hh = "Error";
            }

            if (hh == "Error" || mm == "Error" || ss == "Error")
            {
                return "Error";
            }
            //hh = "Error";
            //break;

        }

        return hh + ":" + mm + ":" + ss;
    
    
}

     protected void Button1_Click(object sender, EventArgs e)
     {
         try
         {
             ExportGridToExcel();
         }
         catch (Exception)
         {
             
            // throw;
         }
         
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
             grdv_tenderlistview.GridLines = GridLines.Both;
             grdv_tenderlistview.HeaderStyle.Font.Bold = true;
             grdv_tenderlistview.RenderControl(htmltextwrtter);
             Response.Write(strwritter.ToString());
             Response.End();
         }
         catch (Exception)
         {
             
            // throw;
         }
          
     }

     public override void VerifyRenderingInServerForm(Control control)
     {
         // controller   
     }  

     public void clear()
     { 
      txtName.Text="";
         txtTenderNo.Text="";
         txtPublishDateTime.Text="";
         txtCloseDateTime.Text="";
         txtCloseDateTime.Text="";
         txtCost.Text="";
         txtEMD.Text="";
         txtExtendDateTime.Text = "";
         ddlDept.ClearSelection();
         //GetTendersList();
     }

     protected void btnCancel_Click(object sender, EventArgs e)
     {
         txtExtendDateTime.Enabled = false;
         clear();
         lblMessage.Text = "";
         lblTenderid.Text = "";
     }

     protected void txtTenderNo_TextChanged(object sender, EventArgs e)
     {
         try
         {
             AdminBL objAdmin = new AdminBL();
             DataSet ds = new DataSet();
             ds = objAdmin.GetTenderName(txtTenderNo.Text);
             if (Convert.ToInt32(ds.Tables[0].Rows[0]["tenderno"]) != 0)
             {
                 ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('This tender no is exist, please enter another tender no')", true);
                 txtTenderNo.Text = "";
             }
         }
         catch (Exception)
         {
             
            // throw;
         }
       
        

     }

     //protected void txtCost_TextChanged(object sender, EventArgs e)
     //{
     //    decimal cost = Convert.ToDecimal(txtCost.Text);
     //    txtCost.Text=cost.ToString("#,##0.00");
     //}

       
       
    }
}