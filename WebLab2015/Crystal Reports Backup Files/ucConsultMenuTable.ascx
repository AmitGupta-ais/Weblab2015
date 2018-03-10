<%@ Control Language="C#" AutoEventWireup="true" Inherits="WebLabMaster.UserControls_ucConsultMenuTable" Codebehind="ucConsultMenuTable.ascx.cs" %>
<%@ Register Src="ucDate.ascx" TagName="ucDate" TagPrefix="uc1" %>
<%@ Register Assembly="AISWebCommon" Namespace="AISWebCommon.AisControls" TagPrefix="AIS" %>

<body>
<table style="width: 100%; height: 100%">
<tr>
<td style=" width: 100%; height: 10%; vertical-align: top; text-align: left;">
<table style="width: 100%; height: 100%">
<tr style="width: 100%; height: 50%">
<td style="vertical-align: top; text-align: left; width: 20%;">
<asp:CheckBox runat="server" ID="cbAll" Text="All" Checked="false"/>
</td>
<td style="vertical-align: top; text-align: left; width: 15%;">
<asp:Label runat="server" ID="lblFromDT">From Date </asp:Label>
</td>
<td style="vertical-align: top; text-align: left; width: 25%;">  
    <uc1:ucDate ID="UcFromDate" runat="server" />

</td>
<td style="vertical-align: top; text-align: left; width: 15%;">
<asp:Label runat="server" ID="lblToDT">To Date</asp:Label>
</td>

<td style="vertical-align: top; text-align: left; width: 25%;">
    <uc1:ucDate ID="UcToDate" runat="server" />
</td>
</tr>
<tr style="width: 100%; height: 50%">
<td style="width: 20%; height: 100%; vertical-align: top; text-align: left;">
    </td>
<td style="width: 160806%; height: 100%; vertical-align: top; text-align: left;">
    <AIS:AisLinkButton ID="lbtnDatewise" runat="server" OnClick="lbtnDatewise_Click">Datewise</AIS:AisLinkButton></td>

<td style="vertical-align: top; text-align: left; width: 13%;">

</td>
<td style="vertical-align: top; text-align: left; width: 10%;">
    <AIS:AisLinkButton ID="lbtnPatientwise" runat="server" OnClick="lbtnPatientwise_Click">Patientwise</AIS:AisLinkButton></td>
<td style="vertical-align: top; text-align: left; width: 19%;">  
   

</td>
</tr>
</table>
</td>
</tr>
    <tr>
       <td style="width: 100%; height: 94%; vertical-align: top; text-align: left;">
           &nbsp;<asp:GridView ID="GV1" runat="server" AllowSorting="True" BorderStyle="None" BorderWidth="1px"
               CellPadding="3" Width="100%" AutoGenerateColumns="False" Caption="Patient List" OnPageIndexChanging="GV1_PageIndexChanging">
               <FooterStyle BackColor="White" ForeColor="#000066" />
               <RowStyle ForeColor="#000066" />
               <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
               <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
               <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
               <Columns>
                   <asp:HyperLinkField DataTextField="PCode" NavigateUrl="~/Forms/frmTestTable.aspx"
                       Text="Show" DataNavigateUrlFields="PCode" DataNavigateUrlFormatString="~/Forms/frmTestTable.aspx?PCode={0}" HeaderText="Patient Code" />
                   <asp:BoundField DataField="PatName" HeaderText="Patient Name" ReadOnly="True" />
                       
               </Columns>
           </asp:GridView>
           <asp:GridView ID="GV2" runat="server" AllowSorting="True" AutoGenerateColumns="False"
               BorderWidth="1px" Caption="Datewise Lab Test List" CellPadding="4" ForeColor="#333333"
               GridLines="None" Width="100%" OnPageIndexChanging="GV2_PageIndexChanging">
               <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
               <Columns>
                   <asp:HyperLinkField DataNavigateUrlFields="LabNo" DataNavigateUrlFormatString="~/Forms/frmAisLabReportView.aspx?LabNo={0}"
                       DataTextField="LabNo" HeaderText="Lab No." NavigateUrl="~/Forms/frmAisLabReportView.aspx"
                       Text="Lab No." />
                   <asp:BoundField DataField="TDate" HeaderText="Date" />
                   <asp:BoundField DataField="PatName" HeaderText="Patient Name" />
               </Columns>
               <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
               <EditRowStyle BackColor="#999999" />
               <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
               <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
               <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
               <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
           </asp:GridView>
           <asp:Label ID="lblInfo" runat="server" Visible="False">There is no data.</asp:Label></td>
     </tr>
    
</table>
</body>