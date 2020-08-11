<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserLogin.aspx.cs" Inherits="Chase_IRF.UserLogin" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>User Sign In</title>
    
    <link href="css/style.css" rel="stylesheet" />
    <script src="scripts/vendor/jquery-1.9.1.min.js"></script>
    <script src="scripts/main.js"></script>
    <style>
        .login-wrap {
            width: 100%;
            margin: auto;
            max-width: 525px;
            min-height: 500px;
            position: relative;
            background: url(images/Chase.png) no-repeat center;
            box-shadow: 0 12px 15px 0 rgba(0,0,0,.24),0 17px 50px 0 rgba(0,0,0,.19);
        }
    </style>
</head>
<body>
    <form id="loginForm" runat="server">
        <br />
        <br />
        <br />
        <div class="login-wrap">
            <div class="login-html">
                <input id="tab-1" type="radio" name="tab" class="sign-in" checked="" /><label for="tab-1" class="tab">Sign In</label>
                <input id="tab-2" type="radio" name="tab" class="sign-up" /><label for="tab-2" class="tab"></label>
                <div class="login-form">
                    <div class="sign-in-htm">
                        <div class="group">
                            <label for="user" class="label">Username</label>
                            <asp:TextBox ID="userid" class="input" required="" placeholder="User Name" runat="server" Height="46px" />
                            <%--<input id="userid" type="text" class="input" required="" placeholder="User Name"/> --%>
                        </div>
                        <div class="group">
                            <label for="pass" class="label">Password</label>
                            <%--<input id="password" type="password" class="input" data-type="password" required="" placeholder="Password"/> --%>
                            <asp:TextBox ID="password" class="input" required="" TextMode="Password" AutoComplete="off" placeholder="Password" runat="server" />
                        </div>
                        <%--<div class="group">
					<input id="check" type="checkbox" class="check" checked>
					<label for="check"><span class="icon"></span> Keep me Signed in</label>
				    </div>--%>
                        <div class="group">
                            <%-- <input type="submit" class="button" value="Sign In" onClick="userSignIn()"/>--%>
                            <asp:Button class="button" ID="btnUserSignIn" OnClick="btnUserSignIn_Click" value="Sign In" runat="server" Text="Sign In" ToolTip="Sign In" />
                        </div>
                        <center><a href="ChangePassword.aspx"><i>Change Password</i></a></center>
                        <br />
                        <br />
                        <center><a href="ForgotPassword.aspx"><i>Forgot Password</i></a></center>
                        <br />
                        <br />
                        <center><asp:Label ID="lblLoginMessage" runat="server" Text="" ForeColor="Red"></asp:Label></center>
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
