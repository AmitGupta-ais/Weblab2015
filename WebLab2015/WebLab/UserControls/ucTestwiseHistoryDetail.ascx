<%@ Control Language="C#" AutoEventWireup="true" Inherits="WebLabMaster.UserControls_ucQryDetailTable" Codebehind="ucTestwiseHistoryDetail.ascx.cs" %>
<%@ Register Assembly="AISWebCommon" Namespace="AISWebCommon.AisControls" TagPrefix="AIS" %>
<table style="width: 100%; height: 100%">
    <tr style="height: 10%">
        <td style="width: 100%; height: 1px">
            <table style="width: 403px">
                <tr>
                    
                    <td style="width: 1%; height: 9px; vertical-align: top; text-align: left;"> 
                        <asp:Button ID="btnWord" runat="server" OnClick="btnWord_Click" Text=" W " ToolTip="Word file" /></td>
                    <td style="width: 1%; height: 9px; vertical-align: top; text-align: left"> 
                        <asp:Button ID="btnExcel" runat="server" OnClick="btnExcel_Click" Text=" X " ToolTip="Excel file" /></td>
                     <td style="height: 9px; width: 6%; vertical-align: top; text-align: left"> 
                         <asp:Button ID="btnPDF" runat="server" OnClick="btnPDF_Click" Text=" P " ToolTip="PDF file" /></td>
                         <td style="width: 35px; height: 17px; vertical-align: top; text-align: left">
                             <asp:Label ID="lblPage1" runat="server">Page</asp:Label></td>
                        <td style="width: 18px; height: 17px; vertical-align: top; text-align: left"> 
                            <asp:Button ID="btnFirst1" runat="server" OnClick="btnFirst_Click" Text="<< "
                                ToolTip="First page" Enabled="False" /></td>
                        <td style="width: 17px; height: 17px; vertical-align: top; text-align: left"> 
                            <asp:Button ID="btnPrev1" runat="server" OnClick="btnPre_Click" Text=" < " ToolTip="Previous page" Enabled="False" /></td>
                        <td style="width: 18px; height: 17px; vertical-align: top; text-align: left;"> 
                            <asp:Label ID="lblPNo1" runat="server">0</asp:Label></td>
                        <td style="height: 17px; width: 5px; vertical-align: top; text-align: left"> 
                            <asp:Label ID="lblOf1" runat="server">of</asp:Label></td>
                        <td style="width: 25px; vertical-align: top; text-align: left"> 
                            <asp:Label ID="lblTotalPage1" runat="server">0</asp:Label></td>
                        <td style="width: 26px; vertical-align: top; text-align: left"> 
                            <asp:Button ID="btnNext1" runat="server" OnClick="btnNext_Click" Text=" > " ToolTip="Next oage" Enabled="False" /></td>
                        <td style="width: 31px; vertical-align: top; text-align: left"> 
                            <asp:Button ID="btnLast1" runat="server" OnClick="btnLast_Click" Text=" >>" ToolTip="Last page" Enabled="False" /></td>
                            <td style="width: 38px; vertical-align: top; text-align: left"> 
                                <asp:Label ID="lblStart1" runat="server" Visible="False">Start</asp:Label></td>
                            <td style="width: 26px; vertical-align: top; text-align: left"> 
                                <asp:Label ID="lblEnd1" runat="server" Visible="False">End</asp:Label></td>
                        <td style="width: 16px; vertical-align: top; text-align: left">   
                            <asp:Label ID="AisLabel2" runat="server" Visible="False">0</asp:Label></td>
                </tr>
                
            </table>
        
        </td>
    </tr>
    <tr style="height: 80%">
        <td style="width: 4638px; height: 406px; vertical-align: top; text-align: left;" align="left" valign="top">
            &nbsp;
            <asp:Panel ID="Panel1" runat="server" Height="100%" HorizontalAlign="Left" ScrollBars="Auto"
                Width="100%" Wrap="False" BorderStyle="Double">
            <asp:Literal ID="Literal1" runat="server"></asp:Literal></asp:Panel>
        </td>
    </tr>
    <tr style="height: 10%">
        <td style="width: 4638px; height: 6px;">
            <table style="width: 401px; vertical-align: top; text-align: left;">
                <tr>
                    <td style="width: 35px; vertical-align: top; height: 17px; text-align: left;">
                        <asp:Label ID="lblPage" runat="server">Page</asp:Label></td>
                        <td style="width: 18px; height: 17px; vertical-align: top; text-align: left"> 
                            <asp:Button ID="btnFirst" runat="server" OnClick="btnFirst_Click" Text="<< " ToolTip="First Page" Enabled="False" /></td>
                        <td style="width: 17px; height: 17px; vertical-align: top; text-align: left"> 
                            <asp:Button ID="btnPre" runat="server" OnClick="btnPre_Click" Text=" < " ToolTip="Previous page" Enabled="False" /></td>
                        <td style="width: 18px; height: 17px;vertical-align: top; text-align: left"> 
                            <asp:Label ID="lblPNo" runat="server">0</asp:Label></td>
                        <td style="height: 17px; width: 5px;vertical-align: top; text-align: left"> 
                            <asp:Label ID="lblOf" runat="server">of</asp:Label></td>
                        <td style="width: 25px; vertical-align: top; text-align: left"> 
                            <asp:Label ID="lblTotalPage" runat="server">0</asp:Label></td>
                        <td style="width: 26px; vertical-align: top; text-align: left"> 
                            <asp:Button ID="btnNext" runat="server" OnClick="btnNext_Click" Text=" > " ToolTip="Next page" Enabled="False" /></td>
                        <td style="vertical-align: top; text-align: left; width: 31px;"> 
                            <asp:Button ID="btnLast" runat="server" OnClick="btnLast_Click" Text=" >>" ToolTip="Last page" Enabled="False" /></td>
                            <td style="width: 38px; vertical-align: top; text-align: left"> 
                                <asp:Label ID="lblStart" runat="server" Visible="False">Start</asp:Label></td>
                            <td style="width: 26px; vertical-align: top; text-align: left"> 
                                <asp:Label ID="lblEnd" runat="server" Visible="False">End</asp:Label></td>
                        <td style="width: 16px; vertical-align: top; text-align: left">   
                            <asp:Label ID="lblCountofData" runat="server" Visible="False">0</asp:Label></td>
              
                </tr>
            </table>
        
        </td>
    </tr>
</table>
