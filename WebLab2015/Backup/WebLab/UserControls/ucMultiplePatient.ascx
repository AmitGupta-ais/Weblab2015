<%@ Control Language="C#" AutoEventWireup="true" Inherits="WebLabMaster.UserControls_ucMultiplePatient" Codebehind="ucMultiplePatient.ascx.cs" %>
<table style="width: 100%">
    <tr>
        <td style="width: 100%; vertical-align: top; height: 389px; text-align: left;">
            <asp:GridView ID="GVPatient" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" Height="225px" Width="671px">
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <Columns>
                    <asp:HyperLinkField DataNavigateUrlFields="Code" DataNavigateUrlFormatString="~/Forms/frmTestTable.aspx?PCode={0}"
                        DataTextField="Code" HeaderText="Patient Code" NavigateUrl="~/Forms/frmTestTable.aspx"
                        Text="Show" />
                    <asp:BoundField DataField="Name" HeaderText="Patient Name" />
                </Columns>
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <EditRowStyle BackColor="#999999" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            </asp:GridView>
        </td>
        
    </tr>
    
</table>
