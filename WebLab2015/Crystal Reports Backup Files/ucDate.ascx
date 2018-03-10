
<%@ Control Language="C#" AutoEventWireup="true" Inherits="WebLabMaster.UserControls_ucDate" Codebehind="ucDate.ascx.cs" %>
<%@ Register Assembly="AISWebCommon" Namespace="AISWebCommon.AisControls" TagPrefix="AIS" %>

<table style="width: 145px">
    <tr>
        <td style="width: 9px; height: 24px;">
            <ais:aiswebcombobox id="ddlDay" runat="server" width="52px"></ais:aiswebcombobox>
        </td>
        <td style="width: 70px; height: 24px;">
            <ais:aiswebcombobox id="ddlMonth" runat="server" width="88px"></ais:aiswebcombobox>
        </td>
        <td style="width: 46px; height: 24px;">
            <ais:aiswebcombobox id="ddlYear" runat="server"></ais:aiswebcombobox>
        </td>
    </tr>
       
</table>
