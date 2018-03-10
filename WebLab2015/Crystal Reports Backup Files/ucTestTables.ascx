<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="WebLabMaster.UserControls_ucTestReports" Codebehind="ucTestTables.ascx.cs" %>
<%@ Register Assembly="AISWebCommon" Namespace="AISWebCommon.AisControls" TagPrefix="AIS" %>
<link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
    rel="stylesheet" type="text/css" />
<link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
    rel="stylesheet" type="text/css" />
<table style="width: 100%; height: 100%">
 
    <tr>
        <td style="width: 100%; height: 94%; vertical-align: top; text-align: left;">
            <asp:GridView ID="GV1" runat="server" AllowSorting="True" BorderStyle="None"
                BorderWidth="1px" CellPadding="3" Width="100%" AutoGenerateColumns="False" 
                OnPageIndexChanging="GV1_PageIndexChanging">
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                <Columns>
                    <asp:HyperLinkField DataTextField="LabNo" NavigateUrl="~/Forms/frmTestTable.aspx"
                        Text="Show" DataNavigateUrlFields="LabNo,TestHeadCode,IsApp,IsOPDCreditBal" DataNavigateUrlFormatString="~/Forms/frmTestTable.aspx?TestHeadCode={1}&LabNo={0}&IsApp={2}&IsOPDCreditBal={3}"
                        HeaderText="Lab Number" />
                    <asp:BoundField DataField="TestDate" HeaderText="Test Date" ReadOnly="True" />
                    <asp:BoundField DataField="TestName" HeaderText="Test Name" ReadOnly="True" />
                    <asp:HyperLinkField Text="View Attachments"  NavigateUrl="~/Forms/frmLabWiseAttachments.aspx" DataNavigateUrlFields="LabNo"
                    DataNavigateUrlFormatString="~/Forms/frmLabWiseAttachments.aspx?LabNo={0}" Target="_blank" HeaderText="Attachments" />
                
                </Columns>
            </asp:GridView>
            
            <asp:GridView ID="GV2" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                BorderWidth="1px" Caption="Test Report Menu" CellPadding="4" ForeColor="#333333"
                GridLines="None" Width="100%" OnPageIndexChanging="GV2_PageIndexChanging">
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <Columns>
                    <asp:HyperLinkField DataNavigateUrlFields="TestGroup" DataNavigateUrlFormatString="~/Forms/frmTestwiseHistoryDetail.aspx?TestGroup={0}&IsOPDCreditBal={3}"
                        DataTextField="TestGroup" HeaderText="Test Group" NavigateUrl="~/Forms/frmTestwiseHistoryDetail.aspx"
                        Text="Test Group" />
                    <asp:BoundField DataField="TestName" HeaderText="Test Name" />
                    <asp:BoundField DataField="TestHeadName" HeaderText="Test Head Name" />
                    <asp:BoundField DataField="TestCount" HeaderText="Total" />
                    </Columns>
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <EditRowStyle BackColor="#999999" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            </asp:GridView>
            <asp:Label ID="AisLabel1" runat="server" Visible="True">* Marked reports are under process, Not available now.</asp:Label>
            
            <asp:Label ID="lblInfo" runat="server" Visible="False">There is no data.</asp:Label>
            &nbsp;
                        <AIS:AisLinkButton ID="lbtnTestwise" runat="server" 
                OnClick="lbtnTestwise_Click" Visible="False">Testwise History Report</AIS:AisLinkButton>
                        <AIS:AisLinkButton ID="lbtnDatewise" runat="server" 
                OnClick="lbtnDatewise_Click" Visible="False">Datewise Report</AIS:AisLinkButton>
        </td>
    </tr>
    
</table>
