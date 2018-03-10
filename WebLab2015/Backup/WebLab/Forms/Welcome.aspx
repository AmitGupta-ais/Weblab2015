<%@ Page Language="C#" AutoEventWireup="true" Inherits="WebLabMaster.Forms_Welcome" Codebehind="Welcome.aspx.cs" %>

<%@ Register Assembly="AISWebCommon" Namespace="AISWebCommon.AisControls" TagPrefix="AIS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Welcome</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td style="vertical-align: top; width: 242px; text-align: center">
                    <AIS:AisWebTextBox ID="txtDatabase" runat="server"></AIS:AisWebTextBox>
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top; width: 242px; text-align: center">
                    <AIS:AisButton ID="btnSubmit" Text="Submit" runat="server" OnClick="btnSubmit_Click" />
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top; width: 242px; text-align: center">
                    <AIS:AisLabel ID="lblDB" Text="Database" runat="server"></AIS:AisLabel>
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top; width: 242px; text-align: center">
                    <AIS:AisLabel ID="lblSecDB" Text="SecondDatabase" runat="server"></AIS:AisLabel>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
