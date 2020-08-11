using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using SpritzDotNet;
using System.Data;

namespace Chase_IRF
{
    public partial class PasswordResetPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetExpires(DateTime.Now);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetExpires(DateTime.Now);
                string encryptionkey = "ChaseIRFKey";
                string Validatedencrypted = Request.QueryString["validated"];
                var Validated = Spritz.EPIDecrypt(Validatedencrypted, encryptionkey);
                DateTime validatedtime = Convert.ToDateTime(Validated);
                string idencrypted = Request.QueryString["username"];
                var id = Spritz.EPIDecrypt(idencrypted, encryptionkey);
                string username = Request.Params["username"];

                if (validatedtime >= DateTime.Now.AddMinutes(-60))
                    {
                    userid.Text = id;
                    userid.Enabled = false;
                    newpassword.Text = string.Empty;
                    newpassword2.Text = string.Empty;
                }
                else
                {
                    Server.Transfer("Timeout.aspx");
                }

            }
        }
        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            string visitorIP = HttpContext.Current.Request.UserHostAddress;
            Page.Validate();
            if (Page.IsValid)
            {
                int trycatchresult = 2;
                int redirect = 0;
                try
                {
                    int changed;
                    string encryptionkey = "ChaseIRFKey";
                    string idencrypted = Request.QueryString["username"];
                    var id = Spritz.EPIDecrypt(idencrypted, encryptionkey);
                   // string username = Request.Params["username"];
                    userid.Text = id;
                    string newpass = newpassword.Text;
                    string newpass2 = newpassword2.Text;
                    if (id != null  && newpass != newpass2)
                    {
                        lblChangePasswordMessage.Text = "Update Failed,Please ensure confirmation password matches new password.";
                    }
                    else if (id != null  && newpass == newpass2)
                    {
                        DataClass mydac = new DataClass();
                        changed = mydac.ResetUserPassword(id, newpass);
                        if (changed == 1)
                        {
                            //lblChangePasswordMessage.Text = "Your new Password Updated";
                            //Response.Write("<script language=javascript>alert('Your new Password Updated Succesfully.')</script>");
                            //Response.RedirectLocation = "UserLogin.aspx";
                            redirect = 1;
                        }
                        else
                        {
                            lblChangePasswordMessage.Text = "The username you entered is incorrect.";
                        }
                    }
                    else
                    {
                        lblChangePasswordMessage.Text = "Update Failed, Ensure you entered correct login Information";
                    }
                }
                catch
                {
                    trycatchresult = 1;
                }
                if (redirect == 1)
                {
                    //lblChangePasswordMessage.Text = "Your new Password Updated";
                    Response.Write("<script language=javascript>alert('Your new Password Updated Succesfully.')</script>");
                    Server.Transfer("UserLogin.aspx");
                }
            }
        }
    }
}