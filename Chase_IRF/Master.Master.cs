using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SpritzDotNet;

namespace Chase_IRF
{
    public partial class Master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            DataClass rep1 = new DataClass();
            string encryptionkey = "ChaseIRFKey";
            string idencrypted = Request.QueryString["ID"];
            if(idencrypted!=null)
            { 
            Session["idencryptedencoded"] = idencrypted;
                //Session.Timeout = 1;
            }
            if (string.IsNullOrEmpty(idencrypted))
            {
                idencrypted = Convert.ToString(Session["idencryptedencoded"]);
            }
            var iddecrypted = Spritz.EPIDecrypt(idencrypted, encryptionkey);
            
           //int id = Convert.ToInt32(iddecrypted);
           int id = 0;
            if (string.IsNullOrEmpty(iddecrypted))
            {
                id = 0;
                Response.Redirect("UserLogin.aspx");
            }
            else
            {
                id = int.Parse(iddecrypted);
            }

            // string status = "ALL";
            DataTable dtrep = rep1.GetUserDetails(id);
            string fname = dtrep.Rows[0].ItemArray[2].ToString();
            string lname = dtrep.Rows[0].ItemArray[3].ToString();
            string fullname = fname + ' ' + lname;
            Session["fullname"] = fullname;
            Session["UserName"] = dtrep.Rows[0].ItemArray[0].ToString();
            Session["EmailId"] = dtrep.Rows[0].ItemArray[4].ToString();//   NewIRFPage.Visible = true;
            loggedInUser.Text = Convert.ToString(Session["fullname"]);

            string isAdmin = dtrep.Rows[0].ItemArray[9].ToString();
            //if (IsAdmin != "")
            //{
            //    usersAdminTab.InnerHtml = "";
            //}


        }
        protected void btnLogout_Click(object sender, EventArgs e)
        {

            Response.Redirect("UserLogin.aspx");
        }

        //protected void HelpPageTabClick(object sender, EventArgs e)
        //{
        //    Response.Redirect(@"\uploads\IRF_HELP_Page.pdf");
        //    string url = "" + @"\uploads\IRF_HELP_Page.pdf";
        //    Response.Write("<script>window.open (url,'_blank');</script>");
        //}

    }
}
