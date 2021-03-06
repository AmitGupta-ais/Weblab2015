﻿<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="frmCommonLogin.aspx.cs" Inherits="WebLab.frmCommonLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <%--vishant--%> 
    <meta charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title>Welcome To Online AP</title>
    <script src="Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>
    <link href="css/bootstrap.min.css" rel="stylesheet"/>
    <link href="css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet"/>
    <link href="css/sb-admin-2.css" rel="stylesheet"/>
    <link href="css/font-awesome.min.css" rel="stylesheet" type="text/css"/>
    <script type="text/javascript">
        function CheckForLogin() {
            debugger;
            var txtname = document.getElementById("txt_userName");
            var txtpass = document.getElementById("txt_pswd");
            if (txtname.value == "") {
                alert("Username Can't Be Blank");
                txtname.focus();
                return false;
            }
            else if (txtpass.value == "") {
                alert("Password Can't Be Blank");
                txtpass.focus();
                return false;
            }
            else {
                return true;
            }
        }
    </script>
</head>
<body onload="javascript:document.getElementById('txt_userName').focus();" onkeydown="javascript:if(window.event.keyCode == 13)document.getElementById('BtnLogin').click();;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Button ID="BtnLogin" runat="server" Text="SignIn" OnClientClick="return CheckForLogin();"
        Width="1px" Height="1px" Style="visibility: hidden" OnClick="BtnLogin_Click" />
    <div class="container">
        <div class="row">
            <div class="col-lg-12" style="text-align: center;background-color:#5cb85c">
                <h3 style="color:White">
                    Welcome To <span runat="server" id="HospName"></span><small style="color:Black"><span runat="server"
                        id="lblversion"></span></small>
                </h3>
            </div>
        </div>
        
        <div class="row">
            <div class="col-md-4 col-md-offset-4">
                <div class="login-panel panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title" style="color:#f0ad4e">
                            Please Login</h3>
                    </div>
                    <div class="panel-body">
                        <form role="form">
                        <fieldset>
                            <div class="form-group">
                                <asp:TextBox ID="txt_userName" runat="server" class="form-control" placeholder="Username"
                                    autofocus="true"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:TextBox ID="txt_pswd" runat="server" TextMode="Password" class="form-control"
                                    placeholder="Password"></asp:TextBox>
                            </div>
                            <!-- Change this to a button or input when using this as a form -->
                            <a href="#" class="btn btn-lg btn-success btn-block" onclick="document.getElementById('BtnLogin').click();">
                                Login</a>
                        </fieldset>
                        </form>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12" style="bottom: 0; text-align: center; vertical-align: bottom">
                <a href="http://www.acsonnet.com">
                    <img src="Images/Company.png" alt="Accurate Info Soft Pvt. Ltd." /></a>Designed
                & Developed By Accurate Info Soft Pvt. Ltd.
            </div>
        </div>
    </div>
    <script src="js/jquery.js" type ="text/jscript"></script>
    <script src="js/bootstrap.min.js" type ="text/jscript"></script>
    <script src="js/metisMenu.min.js" type ="text/jscript"></script>
    <script src="js/sb-admin-2.js" type ="text/jscript"></script>
    </form>
</body>
</html>
