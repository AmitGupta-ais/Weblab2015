<%@ Page Language="C#" AutoEventWireup="true" Inherits="Forms_frmMultiPatient" Codebehind="frmMultiPatient.aspx.cs" %>

<%@ Register Src="../UserControls/ucMultiplePatient.ascx" TagName="ucMultiplePatient"
    TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Patient List</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%">
            <tr style="width: 100%">
                <td style="width: 100%; vertical-align: top; height: 485px; text-align: left;">
                    <uc1:ucMultiplePatient ID="UcMultiplePatient1" runat="server" />
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
