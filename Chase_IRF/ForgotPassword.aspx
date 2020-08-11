<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="Chase_IRF.ForgotPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Forgot Password</title>
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
    </style>
</head>
<body>
    <form id="forgotpasswordForm" runat="server">
        <br />
        <br />
        <br />
        <div class="login-wrap">
            <div class="login-html">
                <input id="tab-1" type="radio" name="tab" class="sign-in" checked="" /><label for="tab-1" class="tab">Forgot Password</label>
                <input id="tab-2" type="radio" name="tab" class="sign-up" /><label for="tab-2" class="tab"></label>
                <div class="login-form">
                    <div class="sign-in-htm">
                        <div class="group">
                            <label for="user" class="label">Username</label>
                            <asp:TextBox ID="txtEmail" class="input" placeholder="Username" runat="server" Height="46px" />
                        </div>
                       

                        <div class="group">
                            <asp:Button class="button" ID="btnSendLink" OnClick="SendPasswordResetEmail" value="Send Email" runat="server" Text="Send Email" ToolTip="Send Email" />
                        </div>
                        <br />
                        <div class="group" align="center" style="padding-left:3%;padding-right:6%">
                            <asp:Label ID="lblMessage" runat="server" Text="" style="align-items:center"></asp:Label>
                            </div>
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

