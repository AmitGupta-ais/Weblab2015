<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmLabWiseAttachments.aspx.cs"
    Inherits="WebLab.frmLabWiseAttachments" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Attachments</title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="../CSS/bootstrap.css" rel="stylesheet" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet" />
    <link href="../css/plugins/timeline.css" rel="stylesheet" />
    <link href="../css/sb-admin-2.css" rel="stylesheet" />
    <link href="../css/plugins/morris.css" rel="stylesheet" />
    <link href="../css/font-awesome.min.css" rel="stylesheet" type="text/css" />

    <script src="../js/jquery.js" type="text/javascript"></script>

  

    <script src="../Scripts/jquery.ui.sortable.min.js" type="text/javascript"></script>

    <script src="../js/bootstrap.min.js" type="text/javascript"></script>

    <script src="../js/metisMenu.min.js" type="text/javascript"></script>

    <script src="../js/raphael.min.js" type="text/javascript"></script>

    <script src="../js/sb-admin-2.js" type="text/javascript"></script>

    

    <style type="text/css">
        .ajax__calendar_container
        {
            z-index: 1000;
        }
        .hiddencol
  {
    display: none;
  }
        .navbar-default{
            background-color:#006699;
        }
        .navbar-default .navbar-brand
        {
            color:white;
        }
        .navbar-default .navbar-brand:focus
        {
            color:white;
        }
        .navbar-default .navbar-brand:hover
        {
            color:white;
        }
        table th{
            padding:5px;
        }
        table td{
            padding:5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
                        
    <div id="wrapper">
        <!-- Navigation -->
        <nav class="navbar navbar-default navbar-static-top" role="navigation" style="margin-bottom: 0">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="sr-only">Toggle navigation</span> <span class="icon-bar"></span><span
                        class="icon-bar"></span><span class="icon-bar"></span>
                </button>
                <a class="navbar-brand" href="#">Welcome To  [<span runat="server"
                    id="HospName"></span>]</a>
            </div>
            
        </nav>
        <div id="page-wrapper">
        <div class="row" style="padding-top:10px;">
        
        <div class="col-lg-4">
        <div class="input-group">
                            <span class="input-group-addon">Name :&nbsp</span>
                            <asp:Label ID="lbname" runat="server" CssClass="form-control"></asp:Label>
                        </div>
        </div>
        <div class="col-lg-4">
        <div class="input-group">
                            <span class="input-group-addon">Patient No. :</span>
                            <asp:Label ID="lbpatno" runat="server" CssClass="form-control"></asp:Label>
                        </div>
        
        </div>
        <div class=col-lg-4">
        <div class="input-group">
                            <span class="input-group-addon">Age/Sex :</span>
                            <asp:Label ID="lbage" runat="server" CssClass="form-control"></asp:Label>
                        </div>
        
        </div>
        <div class="col-lg-3"></div>
        
        </div>
        <div class="row">
<div class="col-lg-12"><hr /></div>        
        </div>
            <div class="row">
                <!-- /.col-lg-12 -->
                <div class="col-lg-6">
                    <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                    <asp:GridView ID="GVItemList" runat="server" 
        BorderWidth="1px" Width="100%" AllowPaging="false"
                            AutoGenerateColumns="false" 
        >
                            <Columns>
                            <asp:BoundField DataField="code" HeaderText="Code" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                           
                            
                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbname" runat="server" Text='<%# Eval("filename") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Descriptions" runat="server" Text='<%# Eval("Descr") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Download">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="dwnld" OnClick="dwnld_Click" runat="server" >Download</asp:LinkButton> 
                                                            
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                        
                                                    
                                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                </div>
            </div>
            <div class="row">
            <div class="col-lg-6">
            <div class="input-group">
                            
                            <asp:Label ID="lbnofile" runat="server" CssClass="form-control" Visible="false" BorderColor="Tomato"></asp:Label>
                        </div>
            </div>
            </div>
            <!-- /.row -->
        </div>
    </div>
    
    </form>
</body>
</html>
