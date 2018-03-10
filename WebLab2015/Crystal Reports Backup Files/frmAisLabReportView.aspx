<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="WebLabMaster.frmAisLabReportView" Codebehind="frmAisLabReportView.aspx.cs" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AISWebCommon" Namespace="AISWebCommon.AisControls" TagPrefix="AIS" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lab Report</title>

    <script language="javascript" type="text/javascript">
    
    function ShowMSG(msg)
    {
        alert(msg);
        window.history.back();
    }
    function openConfirmBox()
    {
    
        //document.getElementById('showWord()');
        function toggleVisibility(controlId)
        {
	            var control = document.getElementById(controlId);
	            if(control.style.visibility == "visible" || control.style.visibility == "")
	                control.style.visibility = "hidden";
	            else 
	                control.style.visibility = "visible";	 
	    }

    }
    
    </script>

    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>
                <table style="width: 100%">
                    <tr>
                        <td>
                        <asp:Label runat="server" Font-Bold="true" Font-Size="Medium" ID="lblInfo" Text="" ></asp:Label>
                            <asp:Button runat="server" ID="btnPDF" Text="Print PDF " ToolTip="PDF Form" OnClick="btnPDF_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 100%; vertical-align: top; width: 100%; text-align: left;">
                            <CR:CrystalReportViewer ID="CRV1" runat="server" Height="1054px" ReportSourceID="CrystalReportSource1"
                                Width="919px"></CR:CrystalReportViewer>
                            <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
                                <Report FileName="AisLabReport.rpt">
                                </Report>
                            </CR:CrystalReportSource>
                            &nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
