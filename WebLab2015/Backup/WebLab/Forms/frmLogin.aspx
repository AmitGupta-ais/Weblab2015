<%@ Page Language="C#" MasterPageFile="~/Forms/mpLogin.master" AutoEventWireup="true" Inherits="WebLabMaster.Forms_frmLogin" Title="Login Page" Codebehind="frmLogin.aspx.cs" %>

<%@ Register Src="../UserControls/ucLogin.ascx" TagName="ucLogin" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script language="javascript" type="text/javascript">
   

</script>
    <table>
    <tr>
        <td>
            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td>
            <uc1:ucLogin ID="UcLogin1" runat="server" />    
        </td>
    </tr>
    
</table>
    
</asp:Content>

