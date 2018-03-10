<%@ Page Language="C#" MasterPageFile="~/Forms/MasterPage.master" AutoEventWireup="true" Inherits="WebLabMaster.Forms_frmConsultMenuTable" Title="Consultant Page" Codebehind="frmConsultMenuTable.aspx.cs" %>

<%@ Register Src="../UserControls/ucConsultMenuTable.ascx" TagName="ucConsultMenuTable"
    TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:ucConsultMenuTable ID="UcConsultMenuTable1" runat="server" />
</asp:Content>

