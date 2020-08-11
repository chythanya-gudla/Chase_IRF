using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using SpritzDotNet;

namespace Chase_IRF
{
    public partial class Users : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Set Cache History
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetExpires(DateTime.Now);

               //Check Validation - Decrypt and Decode Parameters
                string encryptionkey = "ChaseIRFKey";
                string Validatedencrypted = Request.QueryString["Validated"];
                var Validated = Spritz.EPIDecrypt(Validatedencrypted, encryptionkey);
               //DateTime validatedtime = Convert.ToDateTime(Validated);
                string useridencrypted = Request.QueryString["ID"];
                var userid = Spritz.EPIDecrypt(useridencrypted, encryptionkey);
                string clientidencrypted = Request.QueryString["Client"];
                var clientid = Spritz.EPIDecrypt(clientidencrypted, encryptionkey);


                //if (validatedtime >= DateTime.Now.AddMinutes(-1))
                // {
                FillClient(1);
                FillUserDDL();
                FillOwnerDDL();
                //Buttons
                btnSubmit.Disabled = true;
                btnCancel.Disabled = true;
                btnaddnewuser.Disabled = false;
                btnedituser.Disabled = true;
                btndeleteuser.Enabled= false;
                //Input Fields
                txtUserID.Disabled = true;
                txtPassword.Disabled = true;
                txtFirstName.Disabled = true;
                txtLastName.Disabled = true;
                txtEmail.Disabled = true;
                txtPhone.Disabled = true;
                txtManager.Disabled = true;
                txtTitle.Disabled = true;
                IsAdminCheckbox.Disabled = true;
                ddlownerlist.Disabled = true;
                //}
                //else
                //{
                //    Server.Transfer("Unauthorized.aspx");
                //}
            }
        }

        private void FillClient(int clientidint)
        {
            DataClass getclient = new DataClass();
            string client = getclient.GetClient(clientidint);
        }

        private void FillUserDDL()
        {
            DataClass rep = new DataClass();
            int id = 1;
            string status = "ALL";
            DataTable dtrep = rep.GetUsers(id, status);
            ddluserlist.DataSource = dtrep;
            ddluserlist.DataTextField = "UsrNam";
            ddluserlist.DataValueField = "ID";
            ddluserlist.DataBind();
            ddluserlist.Items.Insert(0, new ListItem("- Select a User", "0"));
        }

        private void FillOwnerDDL()
        {
            DataClass own = new DataClass();
            string encryptionkey = "ChaseIRFKey";
            string idencrypted = Request.QueryString["ID"];
            var iddecrypted = Spritz.EPIDecrypt(idencrypted, encryptionkey);
            int id = Convert.ToInt32(iddecrypted);
            DataTable dtown = own.GetItemOwners();
            ddlownerlist.DataSource = dtown;
            ddlownerlist.DataTextField = "OwnerName";
            ddlownerlist.DataValueField = "OPCustomer";
            ddlownerlist.DataBind();

            ddlownerlist.Items.Insert(0, new ListItem("- Select an Owner", "0"));
    
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
    {
            lblErrorMessage.InnerHtml = "";
            lblSuccessMessage.InnerHtml = "";
            string userId = hdnuserid.Value;
            string userID = txtUserID.Value;
            string password = txtPassword.Value;
            string firstname = txtFirstName.Value;
            string lastname = txtLastName.Value;
            if ((txtUserID.Value != "" || userId != "") && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(firstname) && !string.IsNullOrEmpty(lastname))
            {
                int clientIDint = 0;
                    clientIDint = 1;
                
                //string password = Spritz.EPIEncrypt(txtPassword.Value, encryptionkey);
                
                string email = txtEmail.Value;
                string Phone = "0";
                if (!string.IsNullOrEmpty(txtPhone.Value))
                {
                    Phone = txtPhone.Value;
                }
                string manager = txtManager.Value;
                string title = txtTitle.Value;
                bool IsActive = true;
                bool IsAdmin = false;
                if(IsAdminCheckbox.Checked == true)
                    {
                        IsAdmin = true;
                    }
                else
                {
                     IsAdmin = false;
                }
                int ItemOwnerNumber = Convert.ToInt32(ddlownerlist.Value);
                string altuserid = "";// AltUserId.Value;
                string upduserid = "";// upduserid.Value;
                string comments = "";// comments.Value;

                DataClass newuser = new DataClass();
                int InsertUser = newuser.NewUser(clientIDint,
                    userID,
                    password,
                    firstname,
                    lastname,
                    email,
                    Phone,
                    manager,
                    title,
                    altuserid,
                    IsActive,
                    IsAdmin,
                    ItemOwnerNumber,
                    upduserid,
                    comments);

                if (InsertUser > 0)
                {
                    if (userId != "" && userID != "")
                    {
                        lblSuccessMessage.InnerHtml = "User Updated Successfully";
                    }
                    else
                    {
                        lblSuccessMessage.InnerHtml = "User Added Successfully";
                    }
                    ddluserlist.Enabled = true;
                    txtUserID.Disabled = false;
                    txtPassword.Disabled = false;
                    txtFirstName.Disabled = false;
                    txtLastName.Disabled = false;
                    txtEmail.Disabled = false;
                    txtPhone.Disabled = false;
                    txtManager.Disabled = false;
                    txtTitle.Disabled = false;
                    txtUserID.Value = "";
                    txtFirstName.Value = "";
                    txtLastName.Value = "";
                    txtPassword.Value = "";
                    txtEmail.Value = "";
                    txtPhone.Value = "";
                    txtManager.Value = "";
                    ddlownerlist.Value = "";
                    txtTitle.Value = "";
                    FillUserDDL();
                    FillOwnerDDL();
                }
                else
                {
                    lblErrorMessage.InnerHtml = "There was an error in the database, or the user already exists";
                }
            }
            else
            {
               
                lblErrorMessage.InnerHtml = "Please fill all the required fields";
            }
        }

        protected void ddlUserList_SelectedIndexChanged(object sender, EventArgs e)
        {
          //  string encryptionkey = "ChaseIRFKey";
            lblErrorMessage.InnerHtml = "";
            lblSuccessMessage.InnerHtml = "";
            int id = Convert.ToInt32(ddluserlist.SelectedValue);
            DataClass getuserdetails = new DataClass();
            DataTable dt = getuserdetails.GetUserDetails(id);
            ddluserlist.EnableViewState = true;
            btnSubmit.Disabled = true;
            btnCancel.Disabled = false;
            btnedituser.Disabled = false;
            btndeleteuser.Enabled= true;
            btnaddnewuser.Disabled = false;
            txtUserID.Disabled = true;
            txtPassword.Disabled = true;
            txtFirstName.Disabled = true;
            txtLastName.Disabled = true;
            txtEmail.Disabled = true;
            txtPhone.Disabled = true;
            txtManager.Disabled = true;
            txtTitle.Disabled = true;
            ddlownerlist.Disabled = true;
            IsAdminCheckbox.Disabled = true;
            hdnuserid.Value = Convert.ToString(id);
            txtUserID.Value = dt.Rows[0].ItemArray[0].ToString();
            //txtPassword.Value = Spritz.EPIDecrypt(dt.Rows[0].ItemArray[1].ToString(), encryptionkey);
            txtPassword.Value = dt.Rows[0].ItemArray[1].ToString();
            txtFirstName.Value = dt.Rows[0].ItemArray[2].ToString();
            txtLastName.Value = dt.Rows[0].ItemArray[3].ToString();
            txtEmail.Value = dt.Rows[0].ItemArray[4].ToString();
            txtPhone.Value= dt.Rows[0].ItemArray[5].ToString();
            txtManager.Value = dt.Rows[0].ItemArray[10].ToString();
            txtTitle.Value = dt.Rows[0].ItemArray[11].ToString();
            ddlownerlist.Value= dt.Rows[0].ItemArray[13].ToString();

           
        }

        protected void btnAddNewUser_Click(object sender, EventArgs e)
        {
            lblErrorMessage.InnerHtml = "";
            lblSuccessMessage.InnerHtml = "";
            ddluserlist.SelectedIndex = 0;
            ddluserlist.Items.Clear();
            ddluserlist.Enabled = false;
            ddlownerlist.SelectedIndex = 0;
            ddlownerlist.Items.Clear();
            ddlownerlist.Disabled = false;
            txtUserID.Value = "";
            txtFirstName.Value = "";
            txtLastName.Value = "";
            txtPassword.Value = "";
            txtEmail.Value = "";
            txtPhone.Value = "";
            txtManager.Value = "";
            txtTitle.Value = "";
            txtUserID.Disabled = false;
            txtPassword.Disabled = false;
            txtFirstName.Disabled = false;
            txtLastName.Disabled = false;
            txtEmail.Disabled = false;
            txtPhone.Disabled = false;
            txtManager.Disabled = false;
            txtTitle.Disabled = false;
            IsAdminCheckbox.Disabled = false;
            FillOwnerDDL();
            btnedituser.Disabled = true;
            btndeleteuser.Enabled= false;
            btnSubmit.Disabled = false;
            btnCancel.Disabled = false;
            
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            lblSuccessMessage.InnerHtml = "";
            lblErrorMessage.InnerHtml = "";

            if (ddluserlist.SelectedIndex > 0)
            {
                ddluserlist.Enabled = true;
                txtPassword.Disabled = true;
                txtFirstName.Disabled = true;
                txtLastName.Disabled = true;
                txtEmail.Disabled = true;
                txtPhone.Disabled = true;
                txtManager.Disabled = true;
                txtTitle.Disabled = true;
                ddlownerlist.Disabled = true;
                IsAdminCheckbox.Disabled = true;
                btnSubmit.Disabled = true;
                btndeleteuser.Enabled= true;
                int id = Convert.ToInt32(ddluserlist.SelectedValue);
                DataClass getuserdetails = new DataClass();
                DataTable dt = getuserdetails.GetUserDetails(id);
                txtUserID.Value = dt.Rows[0].ItemArray[0].ToString();
                //txtPassword.Value = Spritz.EPIDecrypt(dt.Rows[0].ItemArray[1].ToString(), encryptionkey);
                txtPassword.Value = dt.Rows[0].ItemArray[1].ToString();
                txtFirstName.Value = dt.Rows[0].ItemArray[2].ToString();
                txtLastName.Value = dt.Rows[0].ItemArray[3].ToString();
                txtEmail.Value = dt.Rows[0].ItemArray[4].ToString();
                txtPhone.Value = dt.Rows[0].ItemArray[5].ToString();
                txtManager.Value = dt.Rows[0].ItemArray[10].ToString();
                txtTitle.Value = dt.Rows[0].ItemArray[11].ToString();
                ddlownerlist.Value = dt.Rows[0].ItemArray[13].ToString();
            }
            
            else
            { 
                ClearInputs(Page.Controls);
            FillUserDDL();
            FillOwnerDDL();
            ddluserlist.Enabled = true;
            ddlownerlist.Disabled = true;
            btnSubmit.Disabled = true;
            btnCancel.Disabled = false;
            btnedituser.Disabled = false;
            btndeleteuser.Enabled= true;
            btnaddnewuser.Disabled = false;
            txtUserID.Disabled = true;
            txtPassword.Disabled = true;
            txtFirstName.Disabled = true;
            txtLastName.Disabled = true;
            txtEmail.Disabled = true;
            txtPhone.Disabled = true;
            txtManager.Disabled = true;
            txtTitle.Disabled = true;
            IsAdminCheckbox.Disabled = true;
            ddluserlist.SelectedIndex = 0;
            ddlownerlist.SelectedIndex = 0;
            txtUserID.Value = "";
            txtPassword.Value = "";
            txtFirstName.Value = "";
            txtLastName.Value = "";
            txtEmail.Value = "";
            txtPhone.Value = "";
            txtManager.Value = "";
            txtTitle.Value = "";
            IsAdminCheckbox.Checked = false;

            }
        }


        protected void btnEditUser_Click(object sender, EventArgs e)
        {
            lblErrorMessage.InnerHtml = " ";
            lblSuccessMessage.InnerHtml = "";
            if (ddluserlist.SelectedIndex > 0)
            {
                ddluserlist.Enabled = true;
                txtUserID.Disabled = true;
                txtPassword.Disabled = false;
                txtFirstName.Disabled = false;
                txtLastName.Disabled = false;
                txtEmail.Disabled = false;
                txtPhone.Disabled = false;
                txtManager.Disabled = false;
                txtTitle.Disabled = false;
                ddlownerlist.Disabled = false;
                btnSubmit.Disabled = false;
                IsAdminCheckbox.Disabled = false;
                btndeleteuser.Enabled= true;
                btnaddnewuser.Disabled = false;
                btnCancel.Disabled = false;
            }
            else 
            {
                lblErrorMessage.InnerHtml = "Please Select the User to Edit ";
                FillOwnerDDL();
                FillUserDDL();
                ddluserlist.Enabled = true;
                txtPassword.Disabled = true;
                txtFirstName.Disabled = true;
                txtLastName.Disabled = true;
                txtEmail.Disabled = true;
                txtPhone.Disabled = true;
                txtManager.Disabled = true;
                txtTitle.Disabled = true;
                ddlownerlist.Disabled = true;
                IsAdminCheckbox.Disabled = true;
                btnSubmit.Disabled = true;
                btndeleteuser.Enabled= true;
                btnaddnewuser.Disabled = false;
                btnCancel.Disabled = false;
            }
           
            
        }
        protected void btnDeleteUser_Click(object sender, EventArgs e)
        {
            lblErrorMessage.InnerHtml = " ";
            lblSuccessMessage.InnerHtml = "";

            //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),"err_msg","alert('Are you sure you want to delete the user?');", true);
            if (ddluserlist.SelectedIndex > 0)
            {
                
                int id = Convert.ToInt32(ddluserlist.SelectedValue);
                DataClass deleteuser = new DataClass();
                string userid = txtUserID.Value;
                string comments = "";
                int deleteuserresult = deleteuser.DeleteUser(id, userid, comments);
                if (deleteuserresult == 1)
                {
                    lblSuccessMessage.InnerHtml = "User Deleted Successfully";
                   
                }
                else
                {
                    lblErrorMessage.InnerHtml = "There was an error in the database, or the user has already been deleted";
                }
                ddluserlist.Enabled = true;
                txtUserID.Disabled = true;
                txtPassword.Disabled = true;
                txtFirstName.Disabled = true;
                txtLastName.Disabled = true;
                txtEmail.Disabled = true;
                txtPhone.Disabled = true;
                txtManager.Disabled = true;
                txtTitle.Disabled = true;
                ddlownerlist.Disabled = true;
                IsAdminCheckbox.Disabled = true;
                btnSubmit.Disabled = true;
                btnedituser.Disabled = false;
                btnaddnewuser.Disabled = false;
                btndeleteuser.Enabled= true;
                btnCancel.Disabled = false;

            }
            else
            {
                lblErrorMessage.InnerHtml = "Please select a user to delete.";
            }
            txtUserID.Value = "";
            txtFirstName.Value = "";
            txtLastName.Value = "";
            txtPassword.Value = "";
            txtEmail.Value = "";
            txtPhone.Value = "";
            txtManager.Value = "";
            ddluserlist.Enabled = true;
            ddlownerlist.Value = "";
            txtTitle.Value = "";
            IsAdminCheckbox.Checked = false;
            FillUserDDL();
            FillOwnerDDL();
            btnSubmit.Disabled = true;
            btnedituser.Disabled = false;
            btnaddnewuser.Disabled = false;
            btndeleteuser.Enabled= true;
            btnCancel.Disabled = false;

        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            ClearInputs(Page.Controls);
            Response.Redirect("UserLogin.aspx");
        }

        private void ClearInputs(ControlCollection ctrls)
        {
            foreach (Control ctrl in ctrls)
            {
                if (ctrl is TextBox)
                    ((TextBox)ctrl).Text = string.Empty;
                else if (ctrl is DropDownList)
                    ((DropDownList)ctrl).ClearSelection();

                ClearInputs(ctrl.Controls);
                btnSubmit.EnableViewState = false;
            }
        }

    }
}
