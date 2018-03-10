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
        function unMarkSample(labno) {
            var yestbtn = document.getElementById("yesbtn");
            yestbtn.setAttribute("onclick", "unMarkSampleNow('" + labno + "')");
            var rmktxt = document.getElementById("confinputtext");
            rmktxt.style.visibility = "visible";
            rmktxt.placeholder = "Enter Reason"
            rmktxt.style.border = "1px green solid";
            showConfMessage("Are you sure to Un-Mark Lab No. :" + labno + "?", "alert");
        }
        function unMarkSampleNow(labno) {
            var remark = document.getElementById("confinputtext");
            if (remark.value.length == 0) {
                remark.style.border = "1px red solid";
            }
            else {
                $.ajax({
                    type: "POST",
                    url: "../Forms/frmPerformTests.aspx/unMarkSample",
                    contentType: "application/json; charset=utf-8",
                    data: "{labno:" + JSON.stringify(labno) + ",reason:" + JSON.stringify(remark.value) + "}",
                    dataType: "json",
                    success: OnSuccess,
                    failure: function (response) {
                        var res = response.d;

                        showSuccessMessage(res, "error");

                    }
                });
            }

        }
        function sampleCollection(labno, pcode) {
            document.getElementById("confinputtext").style.visibility = "hidden";
            var WinSettings = "left = 20, top = 20, width = 880, height = 600, toolbar = 1, resizable = 1, scrollbars = 1"
            window.open("../Forms/frmSampleCollection.aspx?labno=" + labno + "&pcode=" + pcode, "mywindow", WinSettings);


        }
        function refreshMe(id) {

            var me = document.getElementById(id);
            me.style.border = "none";
        }
        function approveTest(labno, pcode, isheadwise) {
            document.getElementById("confinputtext").style.visibility = "hidden";
            document.getElementById("hdlabno").value = labno;
            if (isheadwise.toUpperCase() == 'Y') {
                showSuccessMessage("Your can not approve header wise tests!", "success");
                //var WinSettings = "left = 20, top = 20, width = 880, height = 600, toolbar = 1, resizable = 1, scrollbars = 1"
                //window.open("../Forms/frmApproveTest.aspx?labno=" + labno + "&pcode=" + pcode, "mywindow", WinSettings);
            }
            else {
                var yestbtn = document.getElementById("yesbtn");
                yestbtn.setAttribute("onclick", "approveNow('" + labno + "')");
                showConfMessage("Are you sure to approve Lab No. :" + labno + "?", "alert");
            }

        }
        function approveNow(labno) {
            $.ajax({
                type: "POST",
                url: "../Forms/frmPerformTests.aspx/approveTest",
                contentType: "application/json; charset=utf-8",
                data: "{labno:" + JSON.stringify(labno) + "}",
                dataType: "json",
                success: OnSuccess,
                failure: function (response) {
                    var res = response.d;

                    showSuccessMessage(res, "error");

                }
            });
        }
        function OnSuccess(response) {
            $("#confMsgModal").modal("hide");
            var res = response.d;
            showSuccessMessage(res, "success");
        }
        function showSuccessMessage(Message, type) {

            debugger;
            var msgbox = document.getElementById("msg");
            var msgtype = document.getElementById("msgtype");

            if (type == 'error') {
                msgtype.innerText = "Error";
            }
            if (type == 'success') {
                msgtype.innerText = "Message";
            }
            msgbox.innerText = Message;
            $("#msgModal").modal();

        }
        function showConfMessage(Message, type) {

            debugger;

            var msgbox = document.getElementById("confmsg");
            var msgtype = document.getElementById("confmsgtype");

            if (type == 'error') {
                msgtype.innerText = "Error";
            }
            if (type == 'success') {
                msgtype.innerText = "Message";
            }
            if (type == 'alert') {
                msgtype.innerText = "Alert";
            }
            msgbox.innerText = Message;
            $("#confMsgModal").modal();



        }
        function OpenLabTest(vallabno) {
            debugger
            var WinSettings = "left = 20, top = 20, width = 880, height = 600, toolbar = 1, resizable = 1, scrollbars = 1"
            window.open("../Forms/frmLabTests.aspx?labno=" + vallabno);
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
            $(function () {
                debugger;
                var fromDate = document.getElementById('txtFromDate').value;
                var toDate = document.getElementById('txtToDate').value;
                var Search = document.getElementById('txtSearch').value;

                var ddlDeptID = document.getElementById('ddlDept');
                var deptCode = ddlDeptID.value; //ddlDeptID.options[ddlDeptID.selectedIndex].value;
                var OPDIPDID = document.getElementById('ddlOPDIPD');
                var OPDIPD = OPDIPDID.options[OPDIPDID.selectedIndex].value;

                debugger;
                $.ajax({
                    url: 'frmPerformTests.aspx?FROM=' + fromDate + '&TO=' + toDate + '&SEARCH=' + Search + '&M=Y&deptCode=' + deptCode + '&OPDIPD=' + OPDIPD,
                    success: function (data) {
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
                            width: $(window).width() - 300,
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
                            onClickButton: function () {
                                $(this).jqGrid('columnChooser', { modal: true });
                            }
                        });
                        $("#UsersGrid").jqGrid('footerData', 'set', col_total);
                        $("#UsersGrid").jqGrid("destroyFrozenColumns")
                    .jqGrid("setColProp", "Head", { frozen: true })
                    .jqGrid("setFrozenColumns")
                    .trigger("reloadGrid", [{ current: true }]);
                    }

                });
            });
            $("#UsersGrid").jqGrid("GridUnload");
        }


        $(window).bind('resize', function () {
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
        .ajax__calendar_container {
            z-index: 1000;
        }

        .navbar-static-top {
            background-color: #006699;
        }

        .navbar-default .navbar-brand {
            color: white;
        }

            .navbar-default .navbar-brand:focus {
                color: white;
            }

            .navbar-default .navbar-brand:hover {
                color: white;
            }

        .dropdown a {
            color: white;
        }

        .dropdown a:focus,.dropdown a:hover {
            color: #006699;
        }

        .dropdown-toggle a {
            color: white !important;
        }

            .dropdown-toggle a:hover, .dropdown-toggle a:focus {
                color: #006699 !important;
            }

        .dropdown-menu, .dropdown-menu li a {
            background-color: white !important;
            color: #002D42 !important;
            border-color: white !important;
        }

            .dropdown-menu li a:hover {
                background-color: #002D42 !important;
                color: floralwhite !important;
                border-color: #002D42 !important;
            }
        /*.dropdown-toggle:hover{
             background-color: #002D42 !important;
            color: floralwhite !important;
            border-color: #002D42 !important;
        }*/
        /*.navbar-default{
            background-color:#006699;
        }
        .navbar-default .navbar-brand
        {
            color:white;
        }*/
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="wrapper">
            <!-- Navigation -->
            <nav class="navbar navbar-default navbar-static-top" role="navigation" style="margin-bottom: 0">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="sr-only">Toggle navigation</span> <span class="icon-bar"></span><span
                        class="icon-bar"></span><span class="icon-bar"></span>
                </button>
                <a class="navbar-brand" href="frmMenuTest.aspx">Welcome To  [<span runat="server"
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
                <asp:Button ID="btnlogout" runat="server" Width="1px" Height="1px" Style="visibility: hidden" OnClick="btnlogout_Click" />
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
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
        <input type="hidden" id="hdlabno" value="" />
        <!-- Modal -->
        <div class="modal fade" id="msgModal" role="dialog">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header" style="background-color: #5C9CCC;">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title" id="msgtype">Modal Header</h4>
                    </div>
                    <div class="modal-body">
                        <div id="msg">Message Content</div>
                    </div>
                    <div class="modal-footer" style="background-color: #5C9CCC;">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
        <!-- Modal -->
        <div class="modal fade" id="confMsgModal" role="dialog">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header" style="background-color: #5C9CCC;">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title" id="confmsgtype">Modal Header</h4>
                    </div>
                    <div class="modal-body">
                        <div id="confmsg">Message Content</div>
                        <br />
                        <input type="text" id="confinputtext" class="form-control" value="" placeholder="" onkeydown="refreshMe(this.id)" style="visibility: hidden" />
                    </div>
                    <div class="modal-footer" style="background-color: #5C9CCC;">
                        <button type="button" id="yesbtn" class="btn btn-danger">Yes</button>
                        <button type="button" class="btn btn-danger" data-dismiss="modal">No</button>
                    </div>
                </div>
            </div>
        </div>
        <script src="../js/bootstrap.min.js" type="text/javascript"></script>

        <script src="../js/metisMenu.min.js" type="text/javascript"></script>

        <script src="../js/raphael.min.js" type="text/javascript"></script>

        <script src="../js/sb-admin-2.js" type="text/javascript"></script>
    </form>
</body>
</html>
