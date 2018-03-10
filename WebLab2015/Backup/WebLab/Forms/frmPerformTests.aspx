<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmPerformTests.aspx.cs"
    Inherits="WebLab.Forms.frmPerformTests" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Perform Test</title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet" />
    <link href="../css/plugins/timeline.css" rel="stylesheet" />
    <link href="../css/sb-admin-2.css" rel="stylesheet" />
    <link href="../css/plugins/morris.css" rel="stylesheet" />
    <link href="../css/font-awesome.min.css" rel="stylesheet" type="text/css" />
 <script src="../js/jquery.js" type="text/javascript"></script>
 
<link href="../Styles/UI.1.8.11.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../Styles/ui.jqgrid.css" />
    <link href="../Styles/ui.multiselect.css" rel="stylesheet" type="text/css" />

 <script src="../Scripts/ui.multiselect.js" type="text/javascript"></script>

    <script src="../Scripts/grid.setcolumns.js" type="text/javascript"></script>

    <script src="../Scripts/grid.locale-en.js" type="text/javascript"></script>

    <script src="../Scripts/jquery.jqGrid.min.js" type="text/javascript"></script>

    <script src="../Scripts/jquery-ui-1.9.2.custom.js" type="text/javascript"></script>

    <script src="../Scripts/jquery-ui-1.9.2.custom.min.js" type="text/javascript"></script>

    <script src="../Scripts/jquery.ui.sortable.min.js" type="text/javascript"></script>

  
    
    

    <script type="text/javascript">
        function OpenLabTest(vallabno)
        {
           debugger
            var WinSettings = "left = 20, top = 20, width = 880, height = 600, toolbar = 1, resizable = 1, scrollbars = 1"
            window.open("../Forms/frmLabTests.aspx?labno="+vallabno, "mywindow", WinSettings);
        }
        
        
        function PrintBill(LabNo, IsApproved, isPerform) {
            debugger;
            if (isPerform == 0) {
                alert('No Tests has been performed in this Lab' + LabNo);
                return;
            }
            var Iapprove = document.getElementById('hdnIapprove').value;
            if (Iapprove == 1) {
                if (IsApproved == 0) {
                    alert('You are not authorized to print Un-Approved Report');
                    return;
                }
            }
            window.open("frmTestTable.aspx?FromPerformTest=Y&LabNo=" + LabNo);
        }

        function Showattachment(LabNo) {
            debugger;
            window.open("frmLabWiseAttachments.aspx?LabNo=" + LabNo);
        }

        var col_names = [];
        var col_model = [];
        var col_total = [];
        var colname_Fortotal = [];

        function refresh() {
            debugger;
            $(function() {
                debugger;
                var fromDate = document.getElementById('txtFromDate').value;
                var toDate = document.getElementById('txtToDate').value;
                var Search = document.getElementById('txtSearch').value;

                var ddlDeptID = document.getElementById('ddlDept');
                var deptCode = ddlDeptID.value; //ddlDeptID.options[ddlDeptID.selectedIndex].value;
                var OPDIPDID = document.getElementById('ddlOPDIPD');
                var OPDIPD = OPDIPDID.options[OPDIPDID.selectedIndex].value;

                debugger;
                $.ajax({ url: 'frmPerformTests.aspx?FROM=' + fromDate + '&TO=' + toDate + '&SEARCH=' + Search + '&M=Y&deptCode=' + deptCode + '&OPDIPD=' + OPDIPD,
                    success: function(data) {
                        debugger;
                        col_names = data.rowsHead;
                        col_model = data.rowsM;
                        col_total = data.userdata;
                        colname_Fortotal = data.col_Fortotal;
                        $("#UsersGrid").jqGrid({
                            jsonReader: {
                                repeatitems: false,
                                root: "rows"
                            },
                            url: 'frmPerformTests.aspx?FROM=' + fromDate + '&TO=' + toDate + '&SEARCH=' + Search + '&M=Y&deptCode=' + deptCode + '&OPDIPD=' + OPDIPD,
                            datatype: 'json',
                            cache: true,
                            loadonce: true,
                            width: $(window).width()-300,
                            //                        sortable: true,
                            colNames: col_names,
                            colModel: col_model,
                            rowNum: 30,
                            rowList: [10, 20, 30],
                            pager: '#UsersGridPager',
                            viewrecords: true,
                            shrinkToFit: false,
                            search: true,
                            scroll: false,
                            emptyrecords: "No records to view",
                            //autowidth : true,
                            //rownumbers: true,
                            caption: 'Perform Tests',
                            footerrow: true,
                            ignoreCase: true,
                            altRows: true

                        })
                        ////for double click
                        ///////For Navigation Layer
                        $("#UsersGrid").jqGrid('navGrid', '#UsersGridPager', { del: false, edit: false, add: false }, { reloadAfterSubmit: false },
                            { url: "yourEditUrl" }, { url: "yourAddUrl" }, { url: "yourDelUrl" });

                        /////////// For Resize 
                        var height = $(window).height();
                        $('.ui-jqgrid-bdiv').height(height - 160);

                        //////// For Column Chooser
                        $("#UsersGrid").jqGrid('navButtonAdd', '#UsersGridPager', {
                            caption: "",
                            buttonicon: "ui-icon-calculator",
                            title: "Choose columns",
                            onClickButton: function() {
                                $(this).jqGrid('columnChooser', { modal: true });
                            }
                        });
                        $("#UsersGrid").jqGrid('footerData', 'set', col_total);
                        $("#UsersGrid").jqGrid("destroyFrozenColumns")
                    .jqGrid("setColProp", "Head", { frozen: true })
                    .jqGrid("setFrozenColumns")
                    .trigger("reloadGrid", [{ current: true}]);
                    }

                });
            });
            $("#UsersGrid").jqGrid("GridUnload");
        }
        $(window).bind('resize', function() {
            debugger;
            if ($(window).width() >= 800) {
                $("#UsersGrid").setGridWidth($(window).width() - 300);
            }
            if ($(window).width() < 800) {
                $("#UsersGrid").setGridWidth($(window).width());
            }
        }).trigger('resize');

        function logout() {
            debugger;
            var btn = document.getElementById("btnlogout");
            btn.click();
        }

    </script>

    <style type="text/css">
        .ajax__calendar_container
        {
            z-index: 1000;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Button ID="btnlogout" runat="server" Width="1px" Height="1px" Style="visibility: hidden" OnClick="btnlogout_Click" />
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
            <!-- /.navbar-header -->
            <ul class="nav navbar-top-links navbar-right">
                <li class="dropdown"><a class="dropdown-toggle" data-toggle="dropdown" href="#"><span runat="server"
                    id="UserName"></span>&nbsp;<i
                    class="glyphicon glyphicon-user"></i><i class="glyphicon glyphicon-collapse-down"></i></a>
                    <ul class="dropdown-menu">
                        <li><a href="#" onclick="logout()"><i class="glyphicon glyphicon-log-out"></i>Logout</a> </li>
                    </ul>
                    <!-- /.dropdown-user -->
                </li>
                <!-- /.dropdown -->
            </ul>
            <!-- /.navbar-top-links -->
            <div class="navbar-default sidebar" role="navigation"><asp:HiddenField ID="hdnIapprove" runat="server"></asp:HiddenField>
                <div class="sidebar-nav navbar-collapse">
                    <ul class="nav" id="side-menu">
                        <li class="sidebar-search">
                            <div class="input-group custom-search-form" style="max-height: 50px; overflow: auto">
                            <div class="row">
                            <div class="col-lg-12">
                             <div class="input-group" >
                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" placeholder="From Date"></asp:TextBox>
                                <span class="input-group-btn">
                                    <button class="btn btn-default" id="calfrom">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </button>
                                </span>
                                <cc1:CalendarExtender ID="CE1" runat="server" Format="dd/MMM/yyyy" PopupButtonID="calfrom" TargetControlID="txtFromDate" >
                                </cc1:CalendarExtender>
                            </div>
                            </div>
                            </div>
                            <div class="row">
                            <div class="col-lg-12"><div class="input-group">
                                <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" placeholder="To Date"></asp:TextBox>
                                <span class="input-group-btn">
                                    <button class="btn btn-default" id="calto">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </button>
                                </span>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MMM/yyyy"
                                    PopupButtonID="calto" TargetControlID="txtToDate">
                                </cc1:CalendarExtender>
                            </div></div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    Department
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:DropDownList ID="ddlDept" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:DropDownList ID="ddlOPDIPD" runat="server" CssClass="form-control">
                                        <asp:ListItem Value ="Both" Text ="Both" Selected ="True"></asp:ListItem>
                                        <asp:ListItem Value ="OPD" Text ="OPD"></asp:ListItem>
                                        <asp:ListItem Value ="IPD" Text ="IPD"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                            <div class="col-lg-12">
                            <div class="input-group">
                                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search With Patno Or Name"></asp:TextBox>
                                <span class="input-group-addon"><span class="glyphicon glyphicon-search"></span>
                                </span>
                            </div>
                            </div>
                            </div>
                            <div class="row">
                            <div class="col-lg-12">
                             <div >
                                <button type="button" class="btn btn-success" id="btnShow" runat="server" onclick="refresh();">
                                    <span class="glyphicon glyphicon-refresh"></span>Refresh
                                </button>
                            </div>
                            </div>
                            </div>
                            </div>
                            </li>
                            </ul>
                </div>
                <!-- /.sidebar-collapse -->
            </div>
            <!-- /.navbar-static-side -->
        </nav>
        <div id="page-wrapper">
            <div class="row">
                <!-- /.col-lg-12 -->
                <div class="col-lg-12">
                    <table id="UsersGrid">
                    </table>
                    <div id="UsersGridPager">
                    </div>
                </div>
            </div>
            <!-- /.row -->
        </div>
    </div>
   <script src="../js/bootstrap.min.js" type="text/javascript"></script>

    <script src="../js/metisMenu.min.js" type="text/javascript"></script>

    <script src="../js/raphael.min.js" type="text/javascript"></script>

    <script src="../js/sb-admin-2.js" type="text/javascript"></script>
    </form>
</body>
</html>
