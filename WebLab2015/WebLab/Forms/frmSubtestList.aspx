<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Menu.Master" AutoEventWireup="true" CodeBehind="frmSubtestList.aspx.cs" Inherits="WebLab.Forms.frmSubtestList" ClientIDMode="Static" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
         <script src="../js/metisMenu.min.js" type="text/javascript"></script>

    <script src="../js/raphael.min.js" type="text/javascript"></script>

    <script src="../js/sb-admin-2.js" type="text/javascript"></script>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <link href="../css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet" />
    <link href="../css/plugins/timeline.css" rel="stylesheet" />
    <link href="../css/sb-admin-2.css" rel="stylesheet" />
    <script src="../jquery-ui-1.12.1.custom/external/jquery/jquery.js"></script>
    <link href="../jquery-ui-1.12.1.custom/jquery-ui.css" rel="stylesheet" />
    <script src="../jquery-ui-1.12.1.custom/jquery-ui.js"></script>
    <link href="../jquery-ui-1.12.1.custom/jquery-ui.min.css" rel="stylesheet" />
    <script src="../jquery-ui-1.12.1.custom/jquery-ui.min.js"></script>
    <link href="../jquery-ui-1.12.1.custom/jquery-ui.structure.css" rel="stylesheet" />
    <link href="../jquery-ui-1.12.1.custom/jquery-ui.structure.min.css" rel="stylesheet" />
    <link href="../jquery-ui-1.12.1.custom/jquery-ui.theme.css" rel="stylesheet" />
    <link href="../jquery-ui-1.12.1.custom/jquery-ui.theme.min.css" rel="stylesheet" />
    <script src="../jquery-ui-1.12.1.custom/external/jquery/jquery.js"></script> 
      <link href="../css/plugins/morris.css" rel="stylesheet" />
    <link href="../Scripts/jquery-ui.css" rel="stylesheet" />
    <script src="../Scripts/jquery.jqGrid.min.js"></script>
    <script src="../Scripts/jquery.jqGrid.src.js"></script>
    <link href="../font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet" />
<link href="../Styles/UI.1.8.11.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../Styles/ui.jqgrid.css" />
    <script src="../Scripts/grid.setcolumns.js" type="text/javascript"></script>
    <script src="../Scripts/grid.locale-en.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.jqGrid.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui-1.9.2.custom.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui-1.9.2.custom.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.sortable.min.js" type="text/javascript"></script>
    <style>
        .sidelist{
            margin-top:10px;
        }
        .form-contrl {
            width:100%;
        }
    </style>
    <script>
        $(window).bind('resize', function () {
            debugger;
            if ($(window).width() >= 800) {
                $("#UsersGrid").setGridWidth($(window).width() - 300);
            }
            if ($(window).width() < 800) {
                $("#UsersGrid").setGridWidth($(window).width());
            }
        }).trigger('resize');

        function refreshMe(id) {

            var me = document.getElementById(id);
            me.style.border = "none";
        }

        function OpenTest(apCode,Dept,Code,i) {
            debugger
            var WinSettings = "left = 20, top = 20, width = 880, height = 600, toolbar = 1, resizable = 1, scrollbars = 1"
            if(i==2)
            {
                window.open("../Forms/frmTestDetails.aspx?apCode=" + apCode+"&Dept="+Dept+"&Code="+Code+"&MODE=2");
            }
            if (i == 1 ) {
                window.open("../Forms/frmTestDetails.aspx?apCode=" + apCode + "&Dept=" + Dept + "&Code=" + Code+"&MODE=1");
            }


        }

        function openSubTest(Code){
            $.ajax({
                type:'POST',
                url: 'frmDeptWiseLib.aspx/openSubTest',
                data:JSON.stringify(Code),
                success: function (data) {
                }                
            })
        }
        
        $(document).ready(function () {
            document.getElementById("Button1").click();
        });
        function getParameterByName(name, url) {
            if (!url) url = window.location.href;
            name = name.replace(/[\[\]]/g, "\\$&");
            var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
                results = regex.exec(url);
            if (!results) return null;
            if (!results[2]) return '';
            return decodeURIComponent(results[2].replace(/\+/g, " "));
        }

        function refresh() {
            debugger;
            $(function () {
                debugger;
                var LabID = document.getElementById('txtCode').value;
                var apCode = document.getElementById('hdnapCode').value;
                var apcode = getParameterByName('apCode');

                debugger;
                $.ajax({
                    url: 'frmSubtestList.aspx?LabID=' + LabID + "&apCode=" + apcode + "&SR=true",
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
                            url: 'frmSubtestList.aspx?LabID=' + LabID + "&apCode=" + apcode + "&SR=true",
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
                            caption: 'Department Wise test Library',
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
                            buttonicon: "glyphicon glyphicon-th",
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
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="row">
        <div class="col-lg-12 sidelist">
                <div class="input-group"><label class="input-group-addon">LabID</label>
                <asp:TextBox runat="server" ID="txtCode" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
        </div>
    <asp:HiddenField runat="server" ID="hdnapCode" />
        <div class="row">
            <div class="col-lg-12">
                <button type="button" class="btn btn-success" id="Button1" runat="server" onclick="refresh();">
                                    <span class="glyphicon glyphicon-refresh"></span>Refresh
                                </button>
            </div>
            </div> 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
         <div id="">
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
</asp:Content>
