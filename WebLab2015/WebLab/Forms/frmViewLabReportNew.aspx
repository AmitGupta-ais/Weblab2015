<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmViewLabReportNew.aspx.cs" Inherits="WebLab.Forms.frmViewLabReportNew" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>lab Report</title>
    <link href="../Bootstrap/bootstrap.min.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.9.0.min.js"></script>
    <script src="../Bootstrap/bootstrap.min.js"></script>
    <style>
        .headcol{
            font-weight:bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
        <h3 class="text-center">Lab Reports</h3>
        <asp:Table ID="table1" CssClass="table table-bordered table-responsive" runat="server">

        </asp:Table>
    </div>
    </form>
</body>
</html>
