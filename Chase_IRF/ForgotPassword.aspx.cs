using System;
using System.Web;
using SpritzDotNet;
using System.Data;

namespace Chase_IRF
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetExpires(DateTime.Now);

                txtEmail.Text = string.Empty;
                //password.Text = string.Empty;
            }
            else
            {
                

                //Server.Transfer("Unauthorized.aspx");
            }

        }

        protected void SendPasswordResetEmail(object sender, EventArgs e)
        {
            string encryptionkey = "ChaseIRFKey";

            string userid = txtEmail.Text;
            int clientid = 1;
            Page.Validate();
            if (Page.IsValid)
            {
                try
                {
                    DataClass emailLinkDetails = new DataClass();
                    DataTable userDetailsDataTable = emailLinkDetails.GetUserDetailsbyUserID(clientid, userid);
                    if (userDetailsDataTable.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(userDetailsDataTable.Rows[0]["RtnCode"]) > 0)
                        {
                            string ToAddress = userDetailsDataTable.Rows[0]["Email"].ToString().Trim();
                            string userName = userDetailsDataTable.Rows[0]["UserID"].ToString().Trim();


                            //Encrypt and Encode Parameters
                            var idencrypted = Spritz.EPIEncrypt(userName, encryptionkey);
                            string idencryptedencoded = System.Web.HttpUtility.UrlEncode(idencrypted);
                            var validatedencrypted = Spritz.EPIEncrypt(DateTime.Now.ToString(), encryptionkey);
                            string validatedencryptedencoded = System.Web.HttpUtility.UrlEncode(validatedencrypted);
                            // string resetUrl = string.Format("{0}://{1}/{2}?username={3}?validated={4}", Request.Url.Scheme, Request.Url.Authority, "PasswordResetPage.aspx", idencryptedencoded, validatedencryptedencoded);
                            string resetUrl = Request.Url.Scheme + "://" + Request.Url.Authority + "/" + "PasswordResetPage.aspx" + "?username=" + idencryptedencoded + "&validated=" + validatedencryptedencoded;

                            string EmailBody = "Greetings: <br/><br>You recently requested a reset to your password for the Chase IRF Portal. Use the link below to reset your password. This password reset link is only valid for the next 60 minutes.<br><br><a href = " + resetUrl + "> Please click here to reset your password.</a> <br/><br/>If you did not request a password reset, please ignore this email. If you have any questions, contact support at <a href='mailto:ChaseAMTeam@epiinc.com'>ChaseAMTeam@epiinc.com</a> <br><br>Thank you,<br>Chase IRF Support Team";
                            EmailClass EC = new EmailClass();
                            EC.SendEmail(EmailBody, ToAddress, "", "", "Chase IRF Portal Password Reset", true);

                            lblMessage.Text = "A password reset link has been sent to the registered email address";
                            lblMessage.ForeColor = System.Drawing.Color.White;
                            lblMessage.Font.Size = 11;
                            // lblMessage.BackColor = System.Drawing.Color.White;
                        }
                        else
                        {
                            lblMessage.Text = "Invalid Username. Please type correct Username";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                            lblMessage.Font.Size = 12;
                            // lblMessage.BackColor = System.Drawing.Color.White;
                        }

                    }

                }
                catch
                {

                }
            }
        }
    }
}