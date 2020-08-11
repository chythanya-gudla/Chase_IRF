<%@ Page Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="Chase_IRF.Users" %>


<asp:Content ID="UserSignUp" ContentPlaceHolderID="head" runat="server">
    <title>User Administration</title>
    <link href="css/style.css" rel="stylesheet" />
    <link href="scripts/jquery-ui.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>

    <script type="text/javascript" src="/scripts/NewIRFScript.js"></script>
    <script src="scripts/jquery-ui.js"></script>
    <script src="https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css" rel="Stylesheet" type="text/css" />
    <link href="css/datepicker.css" rel="stylesheet" />
    <link href="css/NewIRFcss.css" rel="stylesheet" />
    <style>
        .required {
            color: red;
        }

        input[type='file'] {
            color: transparent;
        }

        .user-form .field input {
            display: inline-block;
            min-height: 36px !important;
            border: 3px #ededed !important;
            border-radius: 25px;
        }

        .dropdownIRF {
            border: 5px;
            width: 25%;
            border-radius: 25px;
            height: 37px;
        }

        .btnIRF {
            height: 35px;
            vertical-align: top;
            width: 10%;
            border-radius: 25px;
        }
    </style>

    

    <form data-toggle="validator" id="frmUserAdministration" role="form" enctype="multipart/form-data" runat="server">
        <div class="newIrf-wrap">
            <div class="container">
                <div class="row">
                    <div class="col-md-12">
                        <br />
                        <h3 class="inlineBlockEle">USER ADMINISTRATION</h3>
                        <h4 class="inlineBlockEle"></h4>

                        <br />
                        <div class="form-group user-form">
                            <div class="field">

                                <label for="itemownername" class="control-label">User List:</label>
                                <asp:DropDownList ID="ddluserlist" class="dropdownIRF" OnSelectedIndexChanged="ddlUserList_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
                                <%--<select class="formField" id="ddluserlist" onselectedindexchange="ddlUserList_SelectedIndexChanged" AutoPostBack="True" runat="server" style="width:210px"></select>--%>
                                <input type="hidden" id="hdnuserid" runat="server" />

                                <label for="lblMessage" id="lblErrorMessage" style="color: red; font-weight: bold; font-size: medium; width: 350px; text-align: center" runat="server" text=""></label>
                                <label for="lblMessage" id="lblSuccessMessage" style="color: green; font-weight: bold; font-size: medium; width: 250px; text-align: center" runat="server" text=""></label>
                            </div>

                            <br />

                            <div class="container">
                                <div class="row">
                                    <div class="col-md-12">


                                        <div class="paginate_button" style="text-align: center">
                                            <table style="width: 80%">
                                                <tr>
                                                    <td style="width: 40%">
                                                        <button id="btnedituser" class="btn-primary btnIRF" style="width: 150px; border-radius: 25px" onserverclick="btnEditUser_Click" runat="server">Edit</button>
                                                    </td>
                                                    <td>
                                                        <asp:Button id="btndeleteuser" cssclass="btn-primary btnIRF" style="width: 150px; border-radius: 25px" OnClick="btnDeleteUser_Click" OnClientClick="return confirm('Are you sure you want to delete the user?')" text="Delete" type="button" runat="server"></asp:Button>
                                                    </td>
                                                    <td>
                                                        <button id="btnaddnewuser" class="btn-primary btnIRF" style="width: 150px; border-radius: 25px" onserverclick="btnAddNewUser_Click" type="button" runat="server">Add New User</button>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                                <br />

                                <div class="field">
                                    <label for="inputName" class="control-label">User Name:<span style="color: red">*</span></label>
                                    <input type="text" runat="server" class="form-control formField" id="txtUserID" required="required" />
                                </div>
                                <div class="field">
                                    <label for="inputName" class="control-label">Password:<span style="color: red">*</span></label>
                                    <input type="text" runat="server" class="form-control formField" id="txtPassword" required="required" />
                                </div>
                                <div class="field">
                                    <label for="inputName" class="control-label">First Name:<span style="color: red">*</span></label>
                                    <input type="text" runat="server" class="form-control formField" id="txtFirstName" required="required" />
                                </div>
                                <div class="field">
                                    <label for="inputName" class="control-label">Last Name:<span style="color: red">*</span></label>
                                    <input type="text" runat="server" class="form-control formField" id="txtLastName" required="required" />
                                </div>

                                <div class="field">
                                    <label for="inputName" class="control-label">Email:</label>
                                    <input type="text" runat="server" class="form-control formField" id="txtEmail" />
                                </div>
                                <div class="field">
                                    <label for="inputName" class="control-label">Phone Number:</label>
                                    <input type="text" runat="server" class="form-control formField" id="txtPhone" onkeydown="return (!((event.keyCode>=65 && event.keyCode <= 95) || event.keyCode >= 106) && event.keyCode!=32);" maxlength="10" />
                                </div>
                                <div class="field">
                                    <label for="inputName" class="control-label">Manager:</label>
                                    <input type="text" runat="server" class="form-control formField" id="txtManager" />
                                </div>
                                <div class="field">

                                    <label for="itemownername" class="control-label">Item Owner Number:</label>
                                    <select class="dropdownIRF" id="ddlownerlist" multiple="false" runat="server">
                                    </select>
                                </div>
                                <div class="field">
                                    <label for="inputName" class="control-label">Title:</label>
                                    <input type="text" runat="server" class="form-control formField" id="txtTitle" />
                                </div>

                                <div class="field">
                                    <label for="IsAdminCheckbox" class="control-label">Is Admin:</label>
                                    <input type="checkbox" runat="server" class="form-control checkbox" id="IsAdminCheckbox" />
                                </div>

                                <br /><br />
                                <div class="container">
                                    <div class="row">
                                        <div class="col-md-12">


                                            <div class="paginate_button" style="text-align: center">
                                                <table style="width: 100%">
                                                    <tr>

                                                        <td style="width: 31%">
                                                            <button id="btnSubmit" class="btn-primary btnIRF" style="width: 300px; border-radius: 25px" onserverclick="btnSubmit_Click" runat="server">Submit</button>
                                                        </td>
                                                        <td style="width: 10%; align-items: flex-end">
                                                            <button id="btnCancel" class="btn-primary btnIRF" style="border-radius: 25px; width: 25%" onclick="btnCancel_Click" onclientclick="return ConfirmOnDelete();" type="button" runat="server">Cancel</button>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <br />
                                            <br />

                                        </div>
                                    </div>
                                </div>
    </form>
</asp:Content>


