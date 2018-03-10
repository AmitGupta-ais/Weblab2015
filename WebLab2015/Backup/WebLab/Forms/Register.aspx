<%@ Page Language="C#" AutoEventWireup="true" Inherits="WebLabMaster.Forms_Register" Codebehind="Register.aspx.cs" %>

<%@ Register Assembly="AISWebCommon" Namespace="AISWebCommon.AisControls" TagPrefix="AIS" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Register</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            
            <tr>
                <td style="width: 488px; vertical-align: top; height: 75px; vertical-align:top; text-align:left">
                    <table style="width:100%; height:100%">
                        <tr>
                            <td  style="width:40%;vertical-align:top; text-align:left">
                                <AIS:AisLabel ID="lblName" runat="server">Name</AIS:AisLabel>
                            </td>
                            <td style="width: 60%; height: 23px;vertical-align:top; text-align:left">
                                <AIS:AisWebTextBox ID="txtName" runat="server"></AIS:AisWebTextBox>
                            </td>
                            
                        </tr>
                        
                    </table>
                </td>
                
            </tr>
            <tr>
                <td style="width: 488px; height: 26px; vertical-align:top; text-align:center">
                    <AIS:AisButton ID="btnCreate" Text="Create" runat="server" OnClick="btnCreate_Click"/>
                </td>
                
            </tr>
            <tr>
                <td style="width: 488px; height: 26px; vertical-align:top; text-align:center">
                    <AIS:AisLabel ID="lblMsg" runat="server" Visible="false">Message</AIS:AisLabel>
                </td>
                
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
