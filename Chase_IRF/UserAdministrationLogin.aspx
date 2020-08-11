<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserAdministrationLogin.aspx.cs" Inherits="Chase_IRF.UserAdministrationLogin" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Login</title>
    <link rel='stylesheet prefetch' href='https://fonts.googleapis.com/css?family=Open+Sans:600' />
    <link rel="stylesheet" href="css/style.css" />
    <script src="scripts/vendor/jquery-1.9.1.min.js"></script>
    <script src="scripts/main.js"></script>
</head>
<body>
    <form id="formLogin" runat="server">
        <div class="login-wrap">
            <div class="login-html">
                <input id="tab-1" type="radio" name="tab" class="sign-in" checked="" /><label for="tab-1" class="tab">Admin Sign In</label>
                <input id="tab-2" type="radio" name="tab" class="sign-up" /><label for="tab-2" class="tab"></label>
                <div class="login-form">
                    <div class="sign-in-htm">
                        <div class="group">
                            <label for="user" class="label">Username</label>
                            <asp:TextBox ID="txtUserID" class="input" required="" placeholder="User Name" runat="server" />
                            <%--<input id="userid" type="text" class="input" required="" placeholder="User Name"/> --%>
                        </div>
                        <div class="group">
                            <label for="pass" class="label">Password</label>
                            <%--<input id="password" type="password" class="input" data-type="password" required="" placeholder="Password"/> --%>
                            <asp:TextBox ID="txtPassword" class="input" required="" TextMode="Password" AutComplete="off" placeholder="Password" runat="server" />
                        </div>
                        <%--<div class="group">
					<input id="check" type="checkbox" class="check" checked>
					<label for="check"><span class="icon"></span> Keep me Signed in</label>
				    </div>--%>
                        <div class="group">
                            <%-- <input type="submit" class="button" value="Sign In" onClick="userSignIn()"/>--%>
                            <asp:Button class="button" ID="btnLogin" OnClick="btnLogin_Click" value="Sign In" runat="server" Text="Sign In" ToolTip="Sign In" />
                        </div>
                        <div class="hr"></div>
                        <div class="foot-lnk">
                            <a href="#forgot">Forgot Password?</a>
                        </div>
                        <div>
                            <asp:Label ID="lblLoginMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                        </div>
                    </div>

                </div>
            </div>
        </div>

    </form>
</body>
</html>
