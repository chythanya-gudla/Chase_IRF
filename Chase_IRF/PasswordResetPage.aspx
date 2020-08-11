﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PasswordResetPage.aspx.cs" Inherits="Chase_IRF.PasswordResetPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Password Reset</title>
    <link rel='stylesheet prefetch' href='https://fonts.googleapis.com/css?family=Open+Sans:600' />
    <link href="css/style.css" rel="stylesheet" />
    <script src="scripts/vendor/jquery-1.9.1.min.js"></script>
    <script src="scripts/main.js"></script>
    <style>
        .login-wrap {
            width: 100%;
            margin: auto;
            max-width: 525px;
            min-height: 550px;
            position: relative;
            background: url(images/Chase.png) no-repeat center;
            box-shadow: 0 12px 15px 0 rgba(0,0,0,.24),0 17px 50px 0 rgba(0,0,0,.19);
        }
                #userid{
                border: none !important;
    padding: 15px 20px !important;
    border-radius: 25px !important;
    background: #c8d2e2 !important;
        width: 100% !important;

        }
    </style>
</head>
<body>
    <form id="passwordResetForm" runat="server">
        <br />
        <br />
        <br />
        <div class="login-wrap">
            <div class="login-html">
                <input id="tab-1" type="radio" name="tab" class="sign-in" checked="" /><label for="tab-1" class="tab">Reset Password</label>
                <input id="tab-2" type="radio" name="tab" class="sign-up" /><label for="tab-2" class="tab"></label>
                <div class="login-form">
                    <div class="sign-in-htm">
                         <div class="group">
                            <label for="user" class="label">Username</label>
                            <asp:TextBox ID="userid" class="input" placeholder="User Name" runat="server" Height="46px" />
                        </div>
                        <div class="group">
                            <label for="pass" class="label">New Password</label>
                            <asp:TextBox ID="newpassword" class="input" required="" TextMode="Password" placeholder="Password" runat="server" />
                        </div>
                                                <div class="group">
                            <label for="pass" class="label">Confirm New Password</label>
                            <asp:TextBox ID="newpassword2" class="input" required="" TextMode="Password" placeholder="Password" runat="server" />
                        </div>

                        <div class="group">
                            <asp:Button class="button" ID="btnChangePassword" OnClick="btnChangePassword_Click" value="Update Password" runat="server" Text="Update Password" ToolTip="Update Password" />
                        </div>
                        
                            <asp:Label ID="lblChangePasswordMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                        </div>
                    </div>

                </div>
            </div>
        
        <br />
        <br />
        <br />
    </form>
</body>
</html>
