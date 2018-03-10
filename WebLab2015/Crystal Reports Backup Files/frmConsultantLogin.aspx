<%@ Page Language="C#" MasterPageFile="~/Forms/mpLogin.master" AutoEventWireup="true" Inherits="WebLabMaster.Forms_frmConsultantLogin" Title="Consultant Login" Codebehind="frmConsultantLogin.aspx.cs" %>

<%@ Register Src="../UserControls/ucConsultantLogin.ascx" TagName="ucConsultantLogin"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:ucConsultantLogin ID="UcConsultantLogin1" runat="server" />
</asp:Content>

