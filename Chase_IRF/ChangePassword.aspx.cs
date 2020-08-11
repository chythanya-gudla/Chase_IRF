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
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetExpires(DateTime.Now);
                userid.Text = string.Empty;
                oldpassword.Text = string.Empty;
                newpassword.Text = string.Empty;
                newpassword2.Text = string.Empty;
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
                    string loginemailid = userid.Text;
                   // userid.Text= loginemailid;
                   // Session["userid"] = loginemailid;

                    string oldpass = oldpassword.Text;
                    string newpass = newpassword.Text;
                    string newpass2 = newpassword2.Text;


                    if (loginemailid == null)
                    {
                        lblChangePasswordMessage.Text = "User Name cannot be Empty";
                    }

                    else if (oldpass == null)
                    {
                        lblChangePasswordMessage.Text = "Password cannot be Empty";
                    }

                    else if (loginemailid != null && oldpass != null && newpass != newpass2)
                    {
                        lblChangePasswordMessage.Text = "Update Failed,Please ensure confirmation password matches new password.";
                    }


                    else if (loginemailid != null && oldpass != null && newpass == newpass2)

                    {
                        DataClass mydac = new DataClass();
                        changed = mydac.UpdUserPassword(loginemailid, oldpass, newpass);
                        if (changed == 1)
                        {
                            //lblChangePasswordMessage.Text = "Your new Password Updated";
                            //Response.Write("<script language=javascript>alert('Your new Password Updated Succesfully.')</script>");
                            //Response.RedirectLocation = "UserLogin.aspx";
                            redirect = 1;
                            
                        }
                        else
                        {
                            lblChangePasswordMessage.Text = "The old password you entered is incorrect.";
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
    
