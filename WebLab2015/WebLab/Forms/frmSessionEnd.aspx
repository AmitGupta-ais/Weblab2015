<%@ Page Language="C#" AutoEventWireup="true" Inherits="WebLabMaster.Forms_frmSessionEnd" Codebehind="frmSessionEnd.aspx.cs" %>

<%@ Register Assembly="AISWebCommon" Namespace="AISWebCommon.AisControls" TagPrefix="AIS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Session End</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr style="width:100%; height:100%; text-align:left; vertical-align:top">
                <td style="width:100%; height:100%; text-align:left; vertical-align:top">
                    <asp:Label ID="lblMsg" runat="server">Sorry! Your session has been ended.</asp:Label>
                </td>
             </tr>
            
        </table>
    
    </div>
    </form>
</body>
</html>
