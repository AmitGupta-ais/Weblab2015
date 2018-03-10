<head>
    <title></title>

    <script language="javascript" type="text/javascript">
        function backtobase(msg) {
            debugger;
            alert(msg);
        }

        function CheckOnClick() {
            sUser = document.getElementById('txtLoginID')
            sPassword = document.getElementById('txtPassword')
            if (sUser == ' ') {
                alert("LoginID cannot be blank.");
                sUser.focus();
                return false;
            }
            if (sPassword == ' ') {
                alert("Password cannot be blank.");
                sPassword.focus();
                return false;
            }


            //window.location = url.value;

            return true;
            // if(txt.length==0)
            // {
            // WebLabMaster.UserControls_ucLogin.txtLoginId.Focus();
            // }
        }
        function window_onload() {

        }

    </script>

</head>
<%@ Control Language="C#" AutoEventWireup="true" Inherits="WebLabMaster.UserControls_ucLogin"
    CodeBehind="ucLogin.ascx.cs" %>
<%@ Register Assembly="AISWebCommon" Namespace="AISWebCommon.AisControls" TagPrefix="AIS" %>
<body>
    <p>
        &nbsp;</p>
    <asp:Panel ID="Panel1" runat="server" Height="161px" Width="331px" Style="text-align: left">
        <table>
            <tr>
                <td style="width: 71px; vertical-align: top; text-align: left;">
                    <asp:Label ID="lblLoginId" runat="server">Login ID</asp:Label>
                    <asp:HiddenField ID="url" runat="server" Value="1" />
                </td>
                <td style="width: 192px; text-align: left; vertical-align: top;">
                    <AIS:AisWebTextBox ID="txtLoginID" runat="server" Width="148px"></AIS:AisWebTextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 71px; vertical-align: top; text-align: left;">
                    <asp:Label ID="lblPassword" runat="server">Password</asp:Label>
                </td>
                <td style="width: 192px; vertical-align: top; text-align: left;">
                    <AIS:AisWebTextBox ID="txtPassword" TextMode="Password" runat="server" Width="147px"></AIS:AisWebTextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 71px; text-align: center; vertical-align: top;">
                    &nbsp;<asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click" Text="Login">
                    </asp:Button>
                </td>
                <td style="width: 192px; text-align: center; vertical-align: top;">
                    &nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel"></asp:Button>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblMsg" runat="server" ForeColor="#FF33CC"></asp:Label>
                    <br />
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                    &nbsp;&nbsp;
                    <AIS:AisLinkButton ID="lbtnForgetPwd" runat="server">Forget Password</AIS:AisLinkButton>
                    <asp:RequiredFieldValidator ID="RFVUser" runat="server" ControlToValidate="txtLoginID"
                        ErrorMessage="error" ValidationGroup="validate">Fill Username</asp:RequiredFieldValidator><br />
                    <strong>Login ID </strong>is your Patient ID or E-mail address<br />
                    <strong>Password</strong> is First four alphabets of your name<br />
                    For Eg
                    <br />
                    <em><span style="font-family: Times">Eg1: If &nbsp;your name as on receipt is Pankaj
                        then password will be PANK<br />
                    </span></em><em><span style="font-family: Times">Eg2: If &nbsp;your name as on receipt
                        is S.S. Sharma then password will be SSSH </span></em>
                </td>
            </tr>
        </table>
    </asp:Panel>
</body>
