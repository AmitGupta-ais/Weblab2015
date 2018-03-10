<%@ Page Language="C#" MasterPageFile="~/Forms/MasterPage.master" AutoEventWireup="true" Inherits="WebLabMaster.Forms_frmTestTable" Title="Test Table" Codebehind="frmTestTable.aspx.cs" %>

<%@ Register Assembly="AISWebCommon" Namespace="AISWebCommon.AisControls" TagPrefix="AIS" %>
<%@ Register Src="../UserControls/ucTestTables.ascx" TagName="ucTestTables" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        a{
            text-decoration:none;
        }
        a:focus{
            text-decoration:none;
            color:seagreen;
        }
        a:hover{
            text-decoration:none;
            color:seagreen;
        }
    </style>
    <script type="text/javascript">
        function toggleVisibility(controlId)
	    {
	        var control = document.getElementById(controlId);
	        if(control.style.visibility == "visible" || control.style.visibility == "")
	        {
	            control.style.visibility = "hidden";
	        }
	        else 
	        {
	            control.style.visibility = "visible";
	        }
	 
        }
        

    </script>

    <uc1:ucTestTables ID="UcTestTables1" runat="server" />
    
    <asp:Panel ID="panelWord" Visible="false" runat="server" BorderColor="Gold">
        <table style="background-color: #00CC99">
            <tr>
                <th>
                    Test Report Summary
                </th>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
