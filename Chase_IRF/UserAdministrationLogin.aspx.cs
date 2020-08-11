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
    public partial class UserAdministrationLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetExpires(DateTime.Now);

                txtPassword.Text = string.Empty;
                txtUserID.Text = string.Empty;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string visitorIP = HttpContext.Current.Request.UserHostAddress;

            string encryptionkey = "ChaseIRFKey";

            Page.Validate();
            if (Page.IsValid)
            {
                DataClass newadminlogin = new DataClass();
                string UserID = txtUserID.Text;
                //string Password = Spritz.EPIEncrypt(txtPassword.Text, encryptionkey);
                string Password = txtPassword.Text;

                try
                {
                    DataTable dt = newadminlogin.AdminLogin(UserID, Password, visitorIP);

                    string IsAdmin = dt.Rows[0].ItemArray[0].ToString();
                    if (IsAdmin == "1") //Authenticated
                    {
                        //Encrypt and Encode Parameters
                        string userid = dt.Rows[0].ItemArray[1].ToString();
                        var useridencrypted = Spritz.EPIEncrypt(userid, encryptionkey);
                        string useridencryptedencoded = System.Web.HttpUtility.UrlEncode(useridencrypted);
                        string clientid = dt.Rows[0].ItemArray[2].ToString();
                        var clientidencrypted = Spritz.EPIEncrypt(clientid, encryptionkey);
                        string clientidencryptedencoded = System.Web.HttpUtility.UrlEncode(clientidencrypted);
                        var validatedencrypted = Spritz.EPIEncrypt(DateTime.Now.ToString(), encryptionkey);
                        string validatedencryptedencoded = System.Web.HttpUtility.UrlEncode(validatedencrypted);

                        //Launch default Form

                        Response.Redirect("users.aspx?Validated=" + validatedencryptedencoded + "&ID=" + useridencryptedencoded + "&Client=" + clientidencryptedencoded);
                    }
                    else //Not Authenticated
                    {
                        lblLoginMessage.Text = "Incorrect login, or you are not an Administrator of this system.";
                        txtUserID.Text = String.Empty;
                        txtPassword.Text = String.Empty;
                    }
                }
                catch (Exception ex) // error with SQL connection
                {
                    lblLoginMessage.Text = "Cannot connect to Database, please contact administrator.";
                    txtUserID.Text = String.Empty;
                    txtPassword.Text = String.Empty;
                }

            }
        }
    }
}