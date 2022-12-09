using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using SpritzDotNet;
using System.Net;

namespace Chase_IRF
{
    public partial class UserLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetExpires(DateTime.Now);

                userid.Text = string.Empty;
                password.Text = string.Empty;
            }
        }

        protected void btnUserSignIn_Click(object sender, EventArgs e)
        {
            string visitorIP = HttpContext.Current.Request.UserHostAddress;

            string encryptionkey = "ChaseIRFKey";
            System.Diagnostics.Debug.WriteLine("User ID: "+ userid.Text);
            System.Diagnostics.Debug.WriteLine("Password: " + password.Text);
            System.Diagnostics.Debug.WriteLine("visitorIP: " + visitorIP);

            Page.Validate();
            if (Page.IsValid)
            {
                DataClass newlogin = new DataClass();
                string UserID = userid.Text;
                //string Password = Spritz.EPIEncrypt(password.Text, encryptionkey);
                string Password = password.Text;

                try
                {
                    DataTable dt = newlogin.Login(UserID, Password, visitorIP);

                    string defaultform = dt.Rows[0].ItemArray[1].ToString();
                    if (defaultform != "") //Authenticated
                    {
                        //Encrypt and Encode Parameters
                        string id = dt.Rows[0].ItemArray[0].ToString();

                        string isAdmin = dt.Rows[0].ItemArray[2].ToString();
                        Session["isAdmin"] = isAdmin;
                        Session["userid"] = id;
                        Session["UpdatedUser"] = userid.Text;
                        var idencrypted = Spritz.EPIEncrypt(id, encryptionkey);
                        string idencryptedencoded = System.Web.HttpUtility.UrlEncode(idencrypted);
                        var validatedencrypted = Spritz.EPIEncrypt(DateTime.Now.ToString(), encryptionkey);
                        string validatedencryptedencoded = System.Web.HttpUtility.UrlEncode(validatedencrypted);
                        //Launch default Form
                        Response.Redirect(defaultform + "?Validated=" + validatedencryptedencoded + "&ID=" + idencryptedencoded);
                    }
                    else //Not Authenticated
                    {
                        lblLoginMessage.Text = "Incorrect Login, please try again.";
                        userid.Text = string.Empty;
                        password.Text = string.Empty;
                    }
                }
                catch (Exception ex) // error with SQL connection
                {
                    lblLoginMessage.Text = "Cannot connect to Database, please contact administrator.";
                    userid.Text = String.Empty;
                    password.Text = String.Empty;
                }
            }
        }
    }
}