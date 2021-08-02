using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using BusinessAccessLayer;
using System.Data;
using System.Collections;
namespace TenderApp
{
    public partial class ChangePassword : System.Web.UI.Page
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
               
            }
            catch (Exception)
            {

                //throw;
            }
            
        }

        protected void btn_Change_Click(object sender, EventArgs e)
        {
            try
            {
             string userid = Session["USER"].ToString();
            AdminBL objAdmin = new AdminBL();
            DataSet ds = new DataSet();
            ds = objAdmin.ChangeLoginDetails(Convert.ToInt32(userid),txtOldPass.Text,txtNewPass.Text);
            clear();
            ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('password changed')", true); 
            }
            catch (Exception)
            {

                ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('there are some error')", true); 
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            clear();
        }
        public void clear()
        {
            txtNewPass.Text = "";
            txtConfirmPass.Text = "";
            txtOldPass.Text = "";
        }
    }
}