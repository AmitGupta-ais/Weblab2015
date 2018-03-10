<%@ Page Language="C#" MasterPageFile="~/Forms/MasterPage.master" AutoEventWireup="true" Inherits="WebLabMaster.Forms_frmTestwiseHistoryDetail" Title="Test History Detail" Codebehind="frmTestwiseHistoryDetail.aspx.cs" %>

<%@ Register Src="../UserControls/ucTestwiseHistoryDetail.ascx" TagName="ucTestwiseHistoryDetail"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:ucTestwiseHistoryDetail id="UcTestwiseHistoryDetail1" runat="server" >
    </uc1:ucTestwiseHistoryDetail>
</asp:Content>

