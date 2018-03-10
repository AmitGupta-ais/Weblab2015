<%@ Control Language="C#" AutoEventWireup="true" Inherits="WebLabMaster.UserControls_ucConsultantLogin" Codebehind="ucConsultantLogin.ascx.cs" %>
<%@ Register Assembly="AISWebCommon" Namespace="AISWebCommon.AisControls" TagPrefix="AIS" %>
<body>
<table>
<tr>
<td>
<table>
        <tr>
            <td style="width: 71px; vertical-align: top; text-align: left;">
                <ais:aislabel id="lblLoginId" runat="server">Login ID</ais:aislabel>
            </td>
            <td style="width: 192px; text-align: left; vertical-align: top;">
                <ais:aiswebtextbox id="txtLoginID" runat="server" width="148px"></ais:aiswebtextbox>
            </td>
           
        </tr>
        <tr>
            <td style="width: 71px; vertical-align: top; text-align: left;">
                <ais:aislabel id="lblPassword" runat="server">Password</ais:aislabel>
            </td>
            <td style="width: 192px; vertical-align: top; text-align: left;">
                <ais:aiswebtextbox id="txtPassword" TextMode="Password" runat="server" width="147px"></ais:aiswebtextbox>
            </td>
            
        </tr>
        <tr>
            <td style="width: 71px; text-align: center; vertical-align: top;">
                &nbsp;<ais:aisbutton id="btnLogin" runat="server" onclick="btnLogin_Click"  text="Login"></ais:aisbutton></td>
            <td style="width: 192px; text-align: center; vertical-align: top;">
                &nbsp;<ais:aisbutton id="btnCancel" runat="server" text="Cancel"></ais:aisbutton></td>
            
        </tr>
    </table>
    
</td>
</tr>
<tr>
<td style="vertical-align: text-top; text-align: center">
<ais:aislinkbutton id="lbtnForgetPwd" runat="server">Forget Password</ais:aislinkbutton>
</td>
</tr>
</table>
</body>