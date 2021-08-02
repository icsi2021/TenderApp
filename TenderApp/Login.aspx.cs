using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessAccessLayer;
using System.Data;
namespace TenderApp
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Session.Abandon();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                AdminBL objAdmin = new AdminBL();
                DataSet ds = new DataSet();
                ds = objAdmin.GetLoginDetails(txtUserName.Text, txtPWD.Text);
                if (ds.Tables.Count > 0)
                {
                    string str = ds.Tables[0].Rows[0]["USERID"].ToString();
                    if (string.IsNullOrEmpty(str))
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = "Wrong Password !!. Please enter correct password.";
                    }
                    else
                    {
                        Session["USER"] = str;
                        Response.Redirect("TenderEntryForm.aspx");
                    }

                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "User doesnot exists.";
                }
            }
            catch (Exception)
            {
                
                //throw;
            }
            

        }
    }
}