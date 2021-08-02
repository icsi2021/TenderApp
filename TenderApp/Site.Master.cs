using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TenderApp
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            try
            {
                Session.Abandon();
                Session.Clear();
                Session["USER"] = null;
                Response.Redirect("~/Home.aspx");
            }
            catch (Exception)
            {
                
              //  throw;
            }
            

        }
    }
}
