<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmTestDetails.aspx.cs" Inherits="WebLab.Forms.frmTestDetails" %>
<%@ Register Src="~/UserControls/ucCommentHtmlBox.ascx" TagPrefix="uc2" TagName="ucCommentHtmlBox" %>
<%@ Register Src="~/UserControls/angularHTML.ascx" TagPrefix="ang" TagName="angularHTML" %>
<%@ Register Src="~/UserControls/angularHTML.ascx" TagPrefix="ucA" TagName="angularHTML" %>




<!DOCTYPE html>
    <meta name="viewport" content="width=device-width, maximum-scale=1, initial-scale=1, user-scalable=0"/>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta content="IE=edge,chrome=1" http-equiv="X-UA-Compatible"/>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>TestDetails</title>

    <script src="../Scripts/rtf_scriptfiles/tiny_mce_src.js"></script>
    <link href="../simeditor/stylesheets/font-awesome.css" rel="stylesheet" />
    <link href="../simeditor/stylesheets/simditor.css" rel="stylesheet" />
    <script type="text/javascript" src="../js/angular.min.js"></script>
    <script type="text/javascript" src="../Bootstrap/jquery.min.js"></script>
    <script type="text/javascript" src="../Bootstrap/bootstrap.min.js"></script>
    <link rel="stylesheet" href="../Bootstrap/bootstrap.min.css" />
    <script src="../simeditor/javascripts/simditor/simditor-all.js"></script>
    <script src="../simeditor/javascripts/angular-editor.js"></script>

    <script src="../TextAngular/dist/textAngular-rangy.min.js"></script>
    <script src="../TextAngular/dist/textAngular-sanitize.min.js"></script>
    <link href="../TextAngular/dist/textAngular.css" rel="stylesheet" />
    <script src="../TextAngular/dist/textAngular.min.js"></script>

    <style>
    @media screen and (min-width: 768px) {
        .modal-dialog {
          width: 30%; /* New width for default modal */
          height:auto;
        }
        .modal-dialog.big {
          width: 70%; /* New width for default modal */
          height:auto;
        }
        .modal-sm {
          width: 350px; /* New width for small modal */
        }
    }
    @media screen and (min-width: 992px) {
        .modal-lg {
          width: 100%; /* New width for large modal */
          height:auto;
        }
    }
        .adv {
            width:90%;
            position:center;
            vertical-align:middle;
        }
        .btnDone {
        }
        .row {
            padding-bottom:1em;
        }
    </style>
    <script>
        
        var headr = { "Content-Type": "application/x-www-form-urlencoded" };
        var app = angular.module("appTestDetail", ['textAngular']);
        app.config(function ($locationProvider) {
            $locationProvider.html5Mode({
                enabled: true,
                requireBase: false
            });
        });
        app.controller("ctrlTestDetail", function ($scope, $http, $location, textAngularManager) {

            /////Initializing data objects
            $scope.TestDetail = {};
            $scope.version = textAngularManager.getVersion();
            $scope.versionNumber = $scope.version.substring(1);
            $scope.disabled = false;    

            //repeat
            $scope.number = 5;//predefined

            $scope.getNumber = function (num) {
                return new Array(num);
            }
            
            //
            $scope.TestDetail = {days:""};
            $scope.testType = [
                "Normal" ,
                "Sub Heading" ,
                "Paragraph" ,
                "Table",
                "Date" ,
                "Time" ,
                "Long Result" 
            ];
            $scope.seldays = {};
            $scope.apGender = [
                { text: "All", value: "0" },
                { text: "Male", value: "1" },
                { text: "Female", value: "2" }
            ];
            
            /////reding querystring            
            $scope.TestDetail.apCode = $location.search().apCode;/////code id the object
            $scope.TestDetail.Code = $location.search().Code;////Code from Query string
            var open = $location.search().MODE;
            var data2send = $scope.TestDetail;         
            
            $scope.loadData = function () {
                $http.post("webLabTest.asmx/loadData", data2send, headr)
                .then(function success(result) {
                    $scope.testData = result.data.d;
                    var gender = $scope.testData.testApplyTo;
                    var rday = $scope.testData.rday;
                    console.log($scope.testData);

                    $scope.getDays(rday);

                    if (gender != null)
                    {
                        if (gender == 1)
                        {
                            $scope.forMale = true;
                        }
                        else if (gender == 2)
                        {
                            $scope.forFemale = true;
                        }
                        else if(gender == 0)
                        {
                            $scope.forAll = true;
                        }
                    }
                },
                function error(error) {
                    console.log(error)
                });
            };
            $scope.loadData();
            $scope.submit = function ()
            {
                obj = $scope.testData;
                var Data = '{"obj":' + JSON.stringify(obj) + '}';
               
                $http.post("webLabTest.asmx/saveData", Data, headr)
                    .then(function success(result) {
                        console.log(result.data);
                        if (result.data.d == true)
                        {
                            alert("Data Saved Sucessfully");
                            window.close();
                        }
                },
                function errror(error) {
                });
            }
            $scope.selDayList = function () {
                var rday = "";
                var day = "";
                if ($scope.seldays.all == true) {
                    $scope.seldays.sun = true;
                    $scope.seldays.mon = true;
                    $scope.seldays.wed = true;
                    $scope.seldays.tue = true;
                    $scope.seldays.thur = true;
                    $scope.seldays.fri = true;
                    $scope.seldays.sat = true;
                    
                }
                if ($scope.seldays.sun == true) {
                    day += "SUNDAY,";
                    rday += "1";
                }
                else {
                    rday += "0";
                }
                if ($scope.seldays.mon == true) {
                    day += "MONDAY,";
                    rday += "1"
                }
                else {
                    rday += "0";
                }
                if ($scope.seldays.tue == true) {
                    day += "TUESDAY,";
                    rday += "1";
                }
                else {
                    rday += "0";
                }
                if ($scope.seldays.wed == true) {
                    day += "WEDNESDAY,";
                    rday += "1";
                }
                else {
                    rday += "0";
                }
                if ($scope.seldays.thur == true) {
                    day += "THURSDAY,";
                    rday += "1";
                }
                else {
                    rday += "0";
                }
                if ($scope.seldays.fri == true) {
                    day += "FRIDAY,";
                    rday += "1";
                }
                else {
                    rday += "0";
                }
                if ($scope.seldays.sat == true) {
                    day += "SATURDAY,";
                    rday += "1";
                }
                else {
                    rday += "0";
                }
                $scope.testData.rday = rday;
                $scope.days = day;
            };
            $scope.getDays = function (rday) {
                if (rday.length > 0)
                {
                    var i = 7;
                    var day = rday.split("");
                    for (i = 0; i < 7; i++)
                    {
                        if (day[i] == '1')
                        {
                            if (i == 0){
                                $scope.seldays.mon = true;
                            }
                            if (i == 1) {
                                $scope.seldays.tue = true;
                            }
                            if (i == 2) {
                                $scope.seldays.wed = true;
                            }
                            if (i == 3) {
                                $scope.seldays.thur = true;
                            }
                            if (i == 4) {
                                $scope.seldays.fri = true;
                            }
                            if (i == 5) {
                                $scope.seldays.sat = true;
                            }
                            if (i == 6) {
                                $scope.seldays.sun = true;
                            }
                        }
                    }
                }
                $scope.defineTab = function () {
                    
                    if ($scope.testData.testtyp == "Table") {
                        var btn = document.getElementById("btnDefTab");
                        console.log(btn);
                        var dt = document.createAttribute ("data-toggle");
                        dt.value = "modal";
                        btn.attributes.setNamedItem(dt);
                        var trgt = document.createAttribute ("data-target");
                        trgt.value = "#tableModal";
                        btn.attributes.setNamedItem(trgt);
                    }
                    else {
                        alert("First Select Type of Test as 'TABLE'");
                        var ddl = document.getElementById("ddltypTest");
                        ddl.focus();
                    }
                }
                $scope.addOpt = function ()
                {
                    alert();
                }
                $scope.newOpt = function () {
                    debugger;
                    console.log('aasasdad');
                    var sn = $scope.testData.options;
                    var k = sn.length;
                    console.log(parseInt(k));
                    var i = 1;
                    //sn[k].sno
                    if (k == 0) {
                        i = 1;
                    }
                    else
                    {
                        i = parseInt(sn[k-1].sno) + 1;
                    }
                    var opt = { "sno": i, "opt": "", "appFor": "" };
                    $scope.testData.options.push(opt);
                    console.log($scope.testData.options);
                }
                $scope.selDayList();
                
            };
        });
        
    </script>
</head>
<body>
    <form id="form1" runat="server" data-ng-app="appTestDetail" data-ng-controller="ctrlTestDetail" class="container-fluid">
    <div  class="panel panel-primary">
        <div class="panel-heading text-center text-capitalize"><label>Test Details</label></div>
        <div class="panel-body">
            <div class="row">
                <div class="col-lg-2 col-sm-2 col-md-2 col-xs12 text-center">
                    <div class="btn-group text-center">
                         <button type="button" class="btn btn-success" data-ng-click="submit()"  >Save</button>
                         <button type="button" class="btn btn-danger" onclick="javascript:window.close();"  >Cancel</button>
                     </div>
                </div>
                <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12 text-center">
                    <div class="btn-group text-center">
                        <button type="button" class="btn btn-info" data-toggle="modal" data-target="#mdInterpretation"  >Interpretation</button>
                        <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#mdPrecaution" >Precaution</button>
                        <button type="button" id="btnDefTab" class="btn btn-info" data-ng-click="defineTab()"  >Define Table</button>
                        <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#modalOptions" data-ng-click=""  >Options</button>
                        <button type="button" class="btn btn-info" data-ng-click=""  >ImportImage</button>
                        <button type="button" class="btn btn-primary" data-ng-click=""  >KitDetails</button>
                        <button type="button" class="btn btn-info" data-ng-click=""  >InventoryItems</button>
                        <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#detFormModal"  data-ng-click=""  >Define Detail Formats</button>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                    <div class="input-group">
                        <label class="input-group-addon">AP Service Code</label>
                        <asp:TextBox runat="server" ReadOnly="true" ID="txtApServCode" CssClass="form-control" data-ng-model="testData.apservcode"></asp:TextBox>
                    </div>
                    
                </div>
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                    <div class="input-group" data-ng-show="testData.servname">
                        <label class="input-group-addon">Service Name</label>
                        <asp:TextBox runat="server" ReadOnly="true" ID="txtServName" CssClass="form-control" data-ng-model="testData.servname"></asp:TextBox>
                    </div>
                    <div>
                        <button type="button" id="mapservice" data-toggle="modal" data-target="#mapSrvModal" class="btn btn-danger" data-ng-show="!testData.servname">Map Service</button>
                    </div>
                </div>
                <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12">
                    <div class="input-group">
                        <label class="input-group-addon">Rate</label>
                        <asp:TextBox runat="server" ReadOnly="true" ID="txtServRate" CssClass="form-control" data-ng-model="testData.rate"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                    <div class="input-group">
                        <label class="input-group-addon">Group Under</label>
                        <asp:TextBox runat="server" ReadOnly="true" ID="txtGrpUnder" CssClass="form-control" data-ng-model="testData.underGroup"></asp:TextBox>
                    </div>
                </div>
            </div> 
            <div class="row">
                <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12">
                    <div class="input-group">
                        <label class="input-group-addon">Test Head</label>
                        <asp:TextBox runat="server" ReadOnly="true" ID="txtTestHead" CssClass="form-control" data-ng-model="testData.testhead"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                    <div class="input-group">
                        <label class="input-group-addon">Print Name</label>
                        <asp:TextBox runat="server" ID="txtPrintName" CssClass="form-control" data-ng-model="testData.printname"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12">
                    <div class="input-group">
                        <label class="input-group-addon">Test ID</label>
                        <asp:TextBox runat="server" ReadOnly="true" ID="txtTestID" CssClass="form-control" data-ng-model="testData.testid"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12">
                    <div class="input-group">
                        <label class="input-group-addon">Type of Test</label>
                        <select id="ddltypTest" class="form-control" data-ng-model="testData.testtyp" data-ng-options="x for x in testType" ></select>
                        
                    </div>
                </div>
                <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12">
                    <div class="input-group">
                        <label class="input-group-addon">Units</label>
                        <asp:TextBox runat="server" ID="txtUnits" CssClass="form-control" data-ng-model="testData.unit"></asp:TextBox>
                    </div>
                </div>
            </div>        
            <div class="row">
                <div class="col-lg-12 col-xs-12 col-sm-12 col-md-12">
                        <table class="table-bordered">
                            <tr>
                                <td>Define Bounds
                                    <div class="row">
                                        <div class="col-lg-5 col-md-5 col-sm-5 col-xs-12">
                                            <div class="input-group">
                                                <label class="input-group-addon">
                                                    Lower Bound
                                                </label>
                                                <asp:TextBox runat="server" ID="txtLowBound" CssClass="form-control"  data-ng-model="testData.lowbound"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-5 col-md-5 col-sm-5 col-xs-12">
                                            <div class="input-group">
                                                <label class="input-group-addon">
                                                    Upper Bound
                                                </label>
                                                <asp:TextBox runat="server" ID="txtUpprBound" CssClass="form-control"  data-ng-model="testData.upbound"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12">
                                            <button type="button" id="btnAdvance" class="btn btn-warning" data-toggle="modal" data-target="#advModal">
                                                Advance
                                            </button>
                                        </div>
                                    </div>
                                </td>
                                <td>Range
                                    <div class="row">
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                            <div class="input-group">
                                                <label class="input-group-addon">
                                                    From
                                                </label>
                                                <asp:TextBox runat="server" ID="txtRngFrom" CssClass="form-control" data-ng-model="testData.lowValue"></asp:TextBox>
                                            </div>
                                        </div>
                                         <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                            <div class="input-group">
                                                <label class="input-group-addon">
                                                    To
                                                </label>
                                                <asp:TextBox runat="server" ID="txtRngTo" CssClass="form-control" data-ng-model="testData.highValue"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
        </div>
            <div class="row" style="padding-bottom:0em">
                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                    <div class="input-group">
                        <span class="input-group-addon">Formula</span>
                        <input type="text" class="form-control" data-ng-model="testData.formula"/>
                    </div>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                    <div class="input-group">
                        <span class="input-group-addon">Default value</span>
                        <input type="text" class="form-control" data-ng-model="testData.defaultvalue"/>
                    </div>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                    <div class="input-group">
                        <span class="input-group-addon">Condition</span>
                        <input type="text" class="form-control" data-ng-model="testData.condition"/>
                    </div>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                    <table class="table table-bordered">
                        <tr><td>
                            <input type="radio" name="apclGender" data-ng-checked="forAll" data-ng-model="testData.testApplyTo" value="0"/>All&nbsp;&nbsp;
                            <input type="radio" name="apclGender" data-ng-checked="forMale" data-ng-model="testData.testApplyTo" value="1"/>Male&nbsp;
                            <input type="radio" name="apclGender" data-ng-checked="forFemale" data-ng-model="testData.testApplyTo" value="2"/>Female
                        </td></tr>
                    </table>
                </div>
                
            </div>
            <div class="row">
                <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12">
                    <div class="input-group">
                        <label class="input-group-addon">Sample Qty</label>
                        <input type="text" class="form-control" data-ng-model="testData.SampleQty"/>
                        <label class="input-group-addon">ML</label>
                    </div>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                    <div class="input-group">
                        <label class="input-group-addon">Default Kit Used</label>
                        <input type="text" class="form-control" data-ng-model="testData.defKit"/>
                    </div>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                    <div class="input-group">
                        <label class="input-group-addon">Short Name</label>
                        <input type="text" class="form-control" data-ng-model="testData.shortName"/>
                    </div>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                    <div class="input-group">
                        <label class="input-group-addon">Search Parameter</label>
                        <input type="text" class="form-control" data-ng-model="testData.srchParam"/>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">                    
                    
                    <div class="input-group">
                    <label class="input-group-addon">Report Coll.</label>    
                    <label class="input-group-addon">Days</label>                        
                    <input type="text" class="form-control" data-ng-model="testData.time"/>                        
                    <label class="input-group-addon">Time</label>                        
                    <input type="text" class="form-control" data-ng-model="testData.hours"/>                       
                    <label class="input-group-addon">Hours</label>
                    </div>                                                       
                </div>
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                <div class="input-group">
                    <label class="btn btn-default input-group-addon" id="btnseldays" data-toggle="modal" data-target="#dayModal">Select Days</label>
                    <asp:TextBox runat="server" ID="txtDays" CssClass="form-control" data-ng-model="days" data-toggle="modal" data-target="#dayModal" ReadOnly="true"></asp:TextBox>
                        <%--<label class="input-group-addon">Select Days</label>--%>                    
                        <%--<asp:DropDownList runat="server" ID="ddlDays" CssClass="form-control" data-ng-model="selDay" data-ng-options="x.day for x in testDays" data-ng-change="sh(selDay.value)"></asp:DropDownList>--%>                        
                    </div>
                </div>
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                    <div class="input-group">
                        <span class="input-group-addon">Remarks</span>
                        <input type="text" class="form-control" data-ng-model="testData.remark"/>
                    </div>
                </div>
            </div>
        <div class="row">
                <div class="col-lg-2 col-md-2 col-sm-2 col-xs-6">       
                    <div class="input-group">
                        <span class="input-group-addon">PreSample</span>
                        <input type="text" class="form-control" data-ng-model="testData.presample"/>
                    </div>                                                     
                </div>
                <div class="col-lg-2 col-md-2 col-sm-2 col-xs-6"> 
                    <div class="input-group">
                        <span class="input-group-addon">Method</span>
                        <input type="text" class="form-control" data-ng-model="testData.method"/>   
                    </div>                                                            
                </div>
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                <div class="input-group">
                        <span class="input-group-addon">Comments</span>
                        <input type="text" class="form-control" data-ng-model="testData.comments"/>
                    </div>
                </div>
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                    <div class="input-group">
                        <span class="input-group-addon">Print Comments</span>
                        <input type="text" class="form-control" data-ng-model="testData.prntcomment"/>
                    </div>
                </div>
            </div>
            <div style="padding-bottom:0em" class="row table table-bordered" >
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                    <div class="row" >
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                        <input type="checkbox" data-ng-checked="testData.isNABL" data-ng-model="testData.isNABL" data-ng-true-value="1" data-ng-false-value="0"/>
                        <span >Is Under NABL</span>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                        <input type="checkbox" data-ng-checked="testData.printKitWTest" data-ng-model="testData.printKitWTest" data-ng-true-value="1" data-ng-false-value="0"/>
                        <span >Print Kit With Test Name</span>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                        <input type="checkbox" data-ng-checked="testData.AbnormalWith" data-ng-model="testData.AbnormalWith" data-ng-true-value="1" data-ng-false-value="0"/>
                        <span >Print Abnormal With</span>
                        <input class="form-control" data-ng-model="testData.abnormalValue" />
                    </div>
                     <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                         <input type="checkbox" data-ng-checked="testData.preSample" data-ng-model="testData.preSample" data-ng-true-value="1" data-ng-false-value="0" />
                         <span >Print PreSample With Test</span>
                     </div>
                </div>
                 </div>
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                    <div class="row" >
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                        <input type="checkbox" data-ng-checked="testData.isword" data-ng-model="testData.isword" data-ng-true-value="1" data-ng-false-value="0"/>
                        <span >Use Word Editor</span>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                        <input type="checkbox" data-ng-checked="testData.hideinPrint" data-ng-model="testData.hideinPrint" data-ng-true-value="1" data-ng-false-value="0"/>
                        <span >Hide Name in Printout</span>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                        <input type="checkbox" data-ng-checked="testData.hideinWS" data-ng-model="testData.hideinWS" data-ng-true-value="1" data-ng-false-value="0"/>
                        <span >Hide Test in Workshhet</span>
                    </div>
                     <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                         <input type="checkbox" data-ng-checked="testData.printMname" data-ng-model="testData.printMname" data-ng-true-value="1" data-ng-false-value="0"/>
                         <span >Print Machine Name with Test</span>
                     </div>
                </div>
                
                </div>
            </div>
            <div class="row">
                <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                    
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-center">
                    
                </div>
            </div>
            <div class="row">
                 <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-center">
                     
                 </div>
            </div>
        
    </div>
        <%--<uc2:ucCommentHtmlBox runat="server" ID="mdPrecaution" controlval="{{testData.precaution}}" />
        <uc2:ucCommentHtmlBox runat="server" ID="mdInterpretation" controlval="{{testData.interpretation}}" />        
        
        <uc2:ucCommentHtmlBox runat="server" ID="ucDetailBox1" controlval="{{testData.detailFrmt1}}" />
        <uc2:ucCommentHtmlBox runat="server" ID="ucDetailBox2" controlval="{{testData.detailFrmt2}}" />
        <uc2:ucCommentHtmlBox runat="server" ID="ucDetailBox3" controlval="{{testData.detailFrmt3}}" />
        <uc2:ucCommentHtmlBox runat="server" ID="ucDetailBox4" controlval="{{testData.detailFrmt4}}" />
        <uc2:ucCommentHtmlBox runat="server" ID="ucDetailBox5" controlval="{{testData.detailFrmt5}}" />
        <uc2:ucCommentHtmlBox runat="server" ID="ucDetailBox6" controlval="{{testData.detailFrmt6}}" />
        <uc2:ucCommentHtmlBox runat="server" ID="ucDetailBox7" controlval="{{testData.detailFrmt7}}" />
        <uc2:ucCommentHtmlBox runat="server" ID="ucDetailBox8" controlval="{{testData.detailFrmt8}}" />
        <uc2:ucCommentHtmlBox runat="server" ID="ucDetailBox9" controlval="{{testData.detailFrmt9}}" />
        <uc2:ucCommentHtmlBox runat="server" ID="ucDetailBox10" controlval="{{testData.detailFrmt10}}" />--%>

        <div id="mdPrecaution" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <div class="modal-header">
                            <h4 class="modal-title">PRECATION</h4>
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div>
                                <div text-angular="text-angular" data-ng-model="testData.precaution" ta-disabled='disabled'></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="mdInterpretation" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <div class="modal-header">
                            <h4 class="modal-title">INTERPRETAION</h4>
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div>
                                <div text-angular="text-angular" data-ng-model="testData.interpretation"  ta-disabled='disabled'></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="ucDetailBox1" class="modal fade" role="dialog">
            <div class="modal-dialog big">
                <div class="modal-content">
                    <div class="modal-header">
                        <div class="modal-header">
                            <h4 class="modal-title">DetailFormat{{testData.detailName1}}</h4>
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div>
                                <div text-angular="text-angular"  data-ng-model="testData.detailFrmt1"  ta-disabled='disabled'></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="ucDetailBox2" class="modal fade" role="dialog">
            <div class="modal-dialog big">
                <div class="modal-content">
                    <div class="modal-header">
                        <div class="modal-header">
                            <h4 class="modal-title">DetailFormat{{testData.detailName2}}</h4>
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div>
                                <div text-angular="text-angular"  data-ng-model="testData.detailFrmt2"  ta-disabled='disabled'></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="ucDetailBox3" class="modal fade" role="dialog">
            <div class="modal-dialog big">
                <div class="modal-content">
                    <div class="modal-header">
                        <div class="modal-header">
                            <h4 class="modal-title">DetailFormat{{testData.detailName3}}</h4>
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div>
                                <div text-angular="text-angular"  data-ng-model="testData.detailFrmt3"  ta-disabled='disabled'></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="ucDetailBox4" class="modal fade" role="dialog">
            <div class="modal-dialog big">
                <div class="modal-content">
                    <div class="modal-header">
                        <div class="modal-header">
                            <h4 class="modal-title">DetailFormat{{testData.detailName4}}</h4>
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div>
                                <div text-angular="text-angular"  data-ng-model="testData.detailFrmt4"  ta-disabled='disabled'></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="ucDetailBox5" class="modal fade" role="dialog">
            <div class="modal-dialog big">
                <div class="modal-content">
                    <div class="modal-header">
                        <div class="modal-header">
                            <h4 class="modal-title">DetailFormat{{testData.detailName5}}</h4>
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div>
                                <div text-angular="text-angular"  data-ng-model="testData.detailFrmt5"  ta-disabled='disabled'></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="ucDetailBox6" class="modal fade" role="dialog">
            <div class="modal-dialog big">
                <div class="modal-content">
                    <div class="modal-header">
                        <div class="modal-header">
                            <h4 class="modal-title">DetailFormat{{testData.detailName6}}</h4>
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div>
                                <div text-angular="text-angular"  data-ng-model="testData.detailFrmt6"  ta-disabled='disabled'></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="ucDetailBox7" class="modal fade" role="dialog">
            <div class="modal-dialog big">
                <div class="modal-content">
                    <div class="modal-header">
                        <div class="modal-header">
                            <h4 class="modal-title">DetailFormat{{testData.detailName7}}</h4>
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div>
                                <div text-angular="text-angular"  data-ng-model="testData.detailFrmt7"  ta-disabled='disabled'></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="ucDetailBox8" class="modal fade" role="dialog">
            <div class="modal-dialog big">
                <div class="modal-content">
                    <div class="modal-header">
                        <div class="modal-header">
                            <h4 class="modal-title">DetailFormat{{testData.detailName8}}</h4>
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div>
                                <div text-angular="text-angular"  data-ng-model="testData.detailFrmt8"  ta-disabled='disabled'></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
            <div id="ucDetailBox9" class="modal fade" role="dialog">
            <div class="modal-dialog big">
                <div class="modal-content">
                    <div class="modal-header">
                        <div class="modal-header">
                            <h4 class="modal-title">DetailFormat{{testData.detailName9}}</h4>
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div>
                                <div text-angular="text-angular"  data-ng-model="testData.detailFrmt9"  ta-disabled='disabled'></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="ucDetailBox10" class="modal fade" role="dialog">
            <div class="modal-dialog big">
                <div class="modal-content">
                    <div class="modal-header">
                        <div class="modal-header">
                            <h4 class="modal-title">DetailFormat{{testData.detailName10}}</h4>
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div>
                                <div text-angular="text-angular"  data-ng-model="testData.detailFrmt10"  ta-disabled='disabled'></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
   </div>
        

        <!-- Modal For days -->
<div id="dayModal" class="modal fade" role="dialog">
  <div class="modal-dialog">

    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header">
        <%--<button type="button" class="close" data-dismiss="modal">&times;</button>--%>
        <h4 class="modal-title">Select Days</h4>
      </div>
      <div class="modal-body">
        <div class="row">
            <%--<div class="form-group">
                <label class="">All</label>
                <input type="checkbox" class="" data-ng-model="seldays.all" />
                
            </div>--%>
            <div class="col-lg-3"></div>
            <div class="col-lg-6">
                <div class="form-group">
                    <input type="checkbox" class="" data-ng-model="seldays.mon" data-ng-checked="seldays.all" />
                    <label class="">Monday</label>
                </div>
                <div class="form-group">
                    <input type="checkbox" class="" data-ng-model="seldays.tue" data-ng-checked="seldays.all" />
                    <label class="">Tuesday</label>
                </div>
                <div class="form-group">
                    <input type="checkbox" class="" data-ng-model="seldays.wed" data-ng-checked="seldays.all" />
                    <label class="">Wednesday</label>

                </div>
                <div class="form-group">
                    <input type="checkbox" class="" data-ng-model="seldays.thur" data-ng-checked="seldays.all" />
                    <label class="">Thursday</label>

                </div>
                <div class="form-group">
                    <input type="checkbox" class="" data-ng-model="seldays.fri" data-ng-checked="seldays.all" />
                    <label class="">Friday</label>

                </div>
                <div class="form-group">
                    <input type="checkbox" class="" data-ng-model="seldays.sat" data-ng-checked="seldays.all" />
                    <label class="">Saturday</label>

                </div>
                <div class="form-group">
                    <input type="checkbox" class="" data-ng-model="seldays.sun" data-ng-checked="seldays.all" />
                    <label class="">Sunday</label>

                </div>
            </div>
            <div class="col-lg-3"></div>
        </div>
      </div>
      <div class="modal-footer text-center">
          <button type="button" class="btn btn-info" data-dismiss="modal" data-ng-click="selDayList()">Done</button>
      </div>
    </div>
  </div>
</div>

        <!--Define Details Format-->
        <div class="modal fade" id="detFormModal" role="dialog">
            <div class="modal-dialog">
                <!--MODAL--->
                <div class="modal-content">
                    <div class="modal-header bg-warning">
                        <h4 class="modal-title">DEFINE DETAIL FORMATS</h4>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                    </div>
                    <div class="modal-body">
                        <table class="table table-bordered table-condensed">
                            <tbody>
                                <tr>
                                    <td>
                                        <div class="row">
                                            <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10">
                                                <asp:TextBox runat="server" ID="txtDetFrmt1" data-ng-model="testData.detailName1" CssClass="form-control">{{testData.detailName1}}</asp:TextBox>
                                            </div>
                                            <div class="col-lg-2 col-md-2 col-sm-2 colxs-2" style="vertical-align:middle;height:100%" data-ng-if="testData.detailName1">
                                            <button type="button"  class="btn btn-warning" data-toggle="modal" data-target="#ucDetailBox1" ><span class="glyphicon glyphicon-edit"></span></button>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="row">
                                            <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10">
                                                <asp:TextBox runat="server" ID="txtDetFrmt2" CssClass="form-control" data-ng-model="testData.detailName2"></asp:TextBox>
                                            </div>
                                            <div class="col-lg-2 col-md-2 col-sm-2 colxs-2" style="vertical-align:middle;height:100%" data-ng-if="testData.detailName2">
                                                <button type="button"  class="btn btn-warning" data-toggle="modal" data-target="#ucDetailBox2" ><span class="glyphicon glyphicon-edit"></span></button>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="row">
                                            <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10">
                                                <asp:TextBox runat="server" ID="txtDetFrmt3" CssClass="form-control" data-ng-model="testData.detailName3"></asp:TextBox>
                                            </div>
                                            <div class="col-lg-2 col-md-2 col-sm-2 colxs-2" style="vertical-align:middle;height:100%" data-ng-if="testData.detailName3">
                                                <button type="button"  class="btn btn-warning" data-toggle="modal" data-target="#ucDetailBox3" ><span class="glyphicon glyphicon-edit"></span></button>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="row">
                                            <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10">
                                                <asp:TextBox runat="server" ID="txtDetFrmt4" CssClass="form-control" data-ng-model="testData.detailName4"></asp:TextBox>
                                            </div>
                                            <div class="col-lg-2 col-md-2 col-sm-2 colxs-2" style="vertical-align:middle;height:100%" data-ng-if="testData.detailName4">
                                                <button type="button"  class="btn btn-warning" data-toggle="modal" data-target="#ucDetailBox4" ><span class="glyphicon glyphicon-edit"></span></button>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="row">
                                            <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10">
                                                <asp:TextBox runat="server" ID="txtDetFrmt5" CssClass="form-control" data-ng-model="testData.detailName5"></asp:TextBox>
                                            </div>
                                            <div class="col-lg-2 col-md-2 col-sm-2 colxs-2" style="vertical-align:middle;height:100%" data-ng-if="testData.detailName5">
                                                <button type="button"  class="btn btn-warning" data-toggle="modal" data-target="#ucDetailBox5" ><span class="glyphicon glyphicon-edit"></span></button>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="row">
                                            <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10">
                                                <asp:TextBox runat="server" ID="txtDetFrmt6" CssClass="form-control" data-ng-model="testData.detailName6"></asp:TextBox>
                                            </div>
                                            <div class="col-lg-2 col-md-2 col-sm-2 colxs-2" style="vertical-align:middle;height:100%" data-ng-if="testData.detailName6">
                                                <button type="button"  class="btn btn-warning" data-toggle="modal" data-target="#ucDetailBox6" ><span class="glyphicon glyphicon-edit"></span></button>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="row">
                                            <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10">
                                                <asp:TextBox runat="server" ID="txtDetFrmt7" CssClass="form-control" data-ng-model="testData.detailName7"></asp:TextBox>
                                            </div>
                                            <div class="col-lg-2 col-md-2 col-sm-2 colxs-2" style="vertical-align:middle;height:100%" data-ng-if="testData.detailName7">
                                                <button type="button"  class="btn btn-warning" data-toggle="modal" data-target="#ucDetailBox7" ><span class="glyphicon glyphicon-edit"></span></button>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="row">
                                            <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10">
                                                <asp:TextBox runat="server" ID="txtDetFrmt8" CssClass="form-control" data-ng-model="testData.detailName8"></asp:TextBox>
                                            </div>
                                            <div class="col-lg-2 col-md-2 col-sm-2 colxs-2" style="vertical-align:middle;height:100%" data-ng-if="testData.detailName8">
                                                <button type="button"  class="btn btn-warning" data-toggle="modal" data-target="#ucDetailBox8" ><span class="glyphicon glyphicon-edit"></span></button>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="row">
                                            <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10">
                                                <asp:TextBox runat="server" ID="txtDetFrmt9" CssClass="form-control" data-ng-model="testData.detailName9"></asp:TextBox>
                                            </div>
                                            <div class="col-lg-2 col-md-2 col-sm-2 colxs-2" style="vertical-align:middle;height:100%" data-ng-if="testData.detailName9">
                                                <button type="button"  class="btn btn-warning" data-toggle="modal" data-target="#ucDetailBox9" ><span class="glyphicon glyphicon-edit"></span></button>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="row">
                                            <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10">
                                                <asp:TextBox runat="server" ID="txtDetFrmt10" CssClass="form-control" data-ng-model="testData.detailName10"></asp:TextBox>
                                            </div>
                                            <div class="col-lg-2 col-md-2 col-sm-2 colxs-2" style="vertical-align:middle;height:100%" data-ng-if="testData.detailName10">
                                                <button type="button"  class="btn btn-warning" data-toggle="modal" data-target="#ucDetailBox10" ><span class="glyphicon glyphicon-edit"></span></button>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>

                    </div>
                </div>
            </div>
        </div>
        <!--modalOptions-->
        <div id="modalOptions" class="modal fade in" role="dialog" padding-left: 17px;">
              <div class="modal-dialog" >

                <!-- Modal content-->
                <div class="modal-content">
                  <div class="modal-header" >
                    
                    <h4 class="modal-title">Options</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                  </div>
                  <div class="modal-body">
                        <table class="table table-bordered table-condensed">
                            <tbody>
                                <tr >
                                    <td>
                                        <div class="row" data-ng-repeat="x in testData.options">
                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                                            <asp:TextBox runat="server" ID="TextBox16" data-ng-model="x.opt" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-lg-4 col-md-4 col-sm-4 colxs-4" style="vertical-align:middle;height:100%">
                                            <asp:TextBox runat="server" ID="TextBox17" data-ng-model="x.appFor" CssClass="form-control"></asp:TextBox>                                            
                                            </div>
                                        </div>
                                        <div class="row"> 
                                            <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12">
                                                <button type="button" class="btn btn-info" data-ng-click="newOpt()"><span class="glyphicon glyphicon-plus-sign"></span></button>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                            </table>                  
                                     
                  </div>
                  
                </div>

              </div>
            </div>



        

        

        <!--Modal for Define Table-->
        <div class="modal fade" id="tableModal" role="dialog">
            <div class="modal-dialog big">

                <!--modal Content-->
                <div class="modal-content">
                    <div class="modal-header bg-info">
                        <h4 class="modal-title">DEFINE TABLE</h4>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                    </div>
                    <div class="modal-body">
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <div class="row" style="padding-bottom: 0px;">
                                        <%--<div class="col-lg-1 col-md-1 col-sm-1 col-xs-1"></div>--%>
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <div class="row" style="padding-bottom: 0px;">
                                                <div class="col-lg-5 col-md-5 col-sm-5 col-xs-5">
                                                    
                                                </div>
                                                <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                                                    <asp:TextBox runat="server" ID="TextBox1" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                                                    <asp:TextBox runat="server" ID="TextBox2" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                                                    <asp:TextBox runat="server" ID="TextBox3" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                                                    <asp:TextBox runat="server" ID="TextBox4" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                                                    <asp:TextBox runat="server" ID="TextBox5" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                                                    <asp:TextBox runat="server" ID="TextBox6" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                                                    <asp:TextBox runat="server" ID="TextBox7" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <%--<div class="col-lg-1 col-md-1 col-sm-1 col-xs-1"></div>--%>
                                    </div>
                                </div>
                            </div>
                            <div class="row" data-ng-repeat="i in getNumber(11) track by $index">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <div class="row" style="padding-bottom: 0px;">
                                        <%--<div class="col-lg-1 col-md-1 col-sm-1 col-xs-1"></div>--%>
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <div class="row" style="padding-bottom: 0px;">
                                                <div class="col-lg-5 col-md-5 col-sm-5 col-xs-5">
                                                    <asp:TextBox runat="server" ID="TextBox15" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                                                    <asp:TextBox runat="server" ID="TextBox8" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                                                    <asp:TextBox runat="server" ID="TextBox9" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                                                    <asp:TextBox runat="server" ID="TextBox10" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                                                    <asp:TextBox runat="server" ID="TextBox11" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                                                    <asp:TextBox runat="server" ID="TextBox12" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                                                    <asp:TextBox runat="server" ID="TextBox13" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                                                    <asp:TextBox runat="server" ID="TextBox14" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <%--<div class="col-lg-1 col-md-1 col-sm-1 col-xs-1"></div>--%>
                                    </div>
                                </div>
                            </div>
                        <input type="button" class="btn btn-warning" value="Comments"/>
                            <div class="row">
                                <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12">
                                    Check 1<input type="checkbox" />
                                </div>
                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-12">
                                    <button class="btn btn-success" data-dismiss="modal" >Done</button>
                                </div>                                
                                <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>



        <!-- Modal FOR Advance Button -->
  <div class="modal fade" id="advModal" role="dialog">
    <div class="modal-lg">
    
      <!-- Modal content-->
      <div class="modal-content">
        <div class="modal-header bg-warning">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title">ADVANCE</h4>
        </div>
        <div class="modal-body">
          <div class="container-fluid">
              <div class="row">
                  <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                      <div class="row">
                          <table class="table table-bordered">
                              <thead>
                                  <tr>
                                      <td>
                                          Sex
                                      </td>
                                      <td>
                                          Age (Yrs)
                                      </td>
                                      <td>
                                          Lower Bound
                                      </td>
                                      <td>
                                          Upper Bound
                                      </td>
                                  </tr>
                              </thead>
                              <tbody <%--data-ng-repeat="i in getNumber(5) track by $index"--%>>
                                  <tr>
                                    <td>
                                      Male
                                    </td>
                                      <td rowspan="2">
                                          <div class="input-group">
                                              <label class="input-group-addon"> &lt;=</label>
                                               <input type="text" class="form-control adv" data-ng-model="testData.YRSC.AGE"/>

                                          </div>
                                      </td>
                                      <td>
                                          <input type="text" class="form-control adv" data-ng-model="testData.YRSC.MALE.LOWERBOUND"/>
                                      </td>
                                      <td>
                                          <input type="text" class="form-control adv" data-ng-model="testData.YRSC.MALE.UPPERBOUND"/>
                                      </td>
                                  </tr>
                                  <tr>
                                      <td>
                                          Female
                                      </td>
                                      <td>
                                          <input type="text" class="form-control adv" data-ng-model="testData.YRSC.FEMALE.LOWERBOUND"/>
                                      </td>
                                      <td>
                                          <input type="text" class="form-control adv" data-ng-model="testData.YRSC.FEMALE.UPPERBOUND"/>
                                      </td>
                                  </tr>
                                  <tr>
                                    <td>
                                      Male
                                    </td>
                                      <td rowspan="2">
                                          <div class="input-group">
                                              <label class="input-group-addon"> &lt;=</label>
                                               <input type="text" class="form-control adv" data-ng-model="testData.YRSA.AGE"/>

                                          </div>
                                      </td>
                                      <td>
                                          <input type="text" class="form-control adv" data-ng-model="testData.YRSA.MALE.LOWERBOUND"/>
                                      </td>
                                      <td>
                                          <input type="text" class="form-control adv" data-ng-model="testData.YRSA.MALE.UPPERBOUND"/>
                                      </td>
                                  </tr>
                                  <tr>
                                      <td>
                                          Female
                                      </td>
                                      <td>
                                          <input type="text" class="form-control adv" data-ng-model="testData.YRSA.FEMALE.LOWERBOUND"/>
                                      </td>
                                      <td>
                                          <input type="text" class="form-control adv" data-ng-model="testData.YRSA.FEMALE.UPPERBOUND"/>
                                      </td>
                                  </tr>
                                  <tr>
                                    <td>
                                      Male
                                    </td>
                                      <td rowspan="2">
                                          <div class="input-group">
                                              <label class="input-group-addon"> &lt;=</label>
                                               <input type="text" class="form-control adv" data-ng-model="testData.YRSO.AGE"/>

                                          </div>
                                      </td>
                                      <td>
                                          <input type="text" class="form-control adv" data-ng-model="testData.YRSO.MALE.LOWERBOUND"/>
                                      </td>
                                      <td>
                                          <input type="text" class="form-control adv" data-ng-model="testData.YRSO.MALE.UPPERBOUND"/>
                                      </td>
                                  </tr>
                                  <tr>
                                      <td>
                                          Female
                                      </td>
                                      <td>
                                          <input type="text" class="form-control adv" data-ng-model="testData.YRSO.FEMALE.LOWERBOUND"/>
                                      </td>
                                      <td>
                                          <input type="text" class="form-control adv" data-ng-model="testData.YRSO.FEMALE.UPPERBOUND"/>
                                      </td>
                                  </tr>
                                  <tr>
                                    <td>
                                      Male
                                    </td>
                                      <td rowspan="2">
                                          <div class="input-group">
                                              <label class="input-group-addon"> &lt;=</label>
                                               <input type="text" class="form-control adv" data-ng-model="testData.YRSE.AGE"/>

                                          </div>
                                      </td>
                                      <td>
                                          <input type="text" class="form-control adv" data-ng-model="testData.YRSE.MALE.LOWERBOUND"/>
                                      </td>
                                      <td>
                                          <input type="text" class="form-control adv" data-ng-model="testData.YRSE.MALE.UPPERBOUND"/>
                                      </td>
                                  </tr>
                                  <tr>
                                      <td>
                                          Female
                                      </td>
                                      <td>
                                          <input type="text" class="form-control adv" data-ng-model="testData.YRSE.FEMALE.LOWERBOUND"/>
                                      </td>
                                      <td>
                                          <input type="text" class="form-control adv" data-ng-model="testData.YRSE.FEMALE.UPPERBOUND"/>
                                      </td>
                                  </tr>
                                  <tr>
                                    <td>
                                      Male
                                    </td>
                                      <td rowspan="2">
                                          <div class="input-group">
                                              <label class="input-group-addon"> &lt;=</label>
                                               <input type="text" class="form-control adv" data-ng-model="testData.YRSF.AGE"/>

                                          </div>
                                      </td>
                                      <td>
                                          <input type="text" class="form-control adv" data-ng-model="testData.YRSF.MALE.LOWERBOUND"/>
                                      </td>
                                      <td>
                                          <input type="text" class="form-control adv" data-ng-model="testData.YRSF.MALE.UPPERBOUND"/>
                                      </td>
                                  </tr>
                                  <tr>
                                      <td>
                                          Female
                                      </td>
                                      <td>
                                          <input type="text" class="form-control adv" data-ng-model="testData.YRSF.FEMALE.LOWERBOUND"/>
                                      </td>
                                      <td>
                                          <input type="text" class="form-control adv" data-ng-model="testData.YRSF.FEMALE.UPPERBOUND"/>
                                      </td>
                                  </tr>
                              </tbody>
                          </table>
                      </div>
                  </div>
                  <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                      <div class="row">
                          <table class="table table-bordered">
                              <thead>
                                  <tr>
                                      <td>
                                          Sex
                                      </td>
                                      <td>
                                          Age(Days)
                                      </td>
                                      <td>
                                          Lower Bound
                                      </td>
                                      <td>
                                          Upper Bound
                                      </td>
                                  </tr>
                              </thead>
                              <tbody <%--data-ng-repeat="i in getNumber(2) track by $index"--%>>
                                  <tr>
                                    <td>
                                      Male
                                    </td>
                                      <td rowspan="2">
                                         <div class="input-group">
                                              <label class="input-group-addon"> &lt;=</label>
                                               <input type="text" class="form-control adv" data-ng-model="testData.DAY1.AGE"/>

                                          </div>
                                      </td>
                                      <td>
                                          <input type="text" class="form-control adv"  data-ng-model="testData.DAY1.MALE.LOWERBOUND"/>
                                      </td>
                                      <td>
                                          <input type="text" class="form-control adv"  data-ng-model="testData.DAY1.MALE.UPPERBOUND"/>
                                      </td>
                                  </tr>
                                  <tr>
                                      <td>
                                          Female
                                      </td>
                                      <td>
                                          <input type="text" class="form-control adv"  data-ng-model="testData.DAY1.FEMALE.LOWERBOUND"/>
                                      </td>
                                      <td>
                                          <input type="text" class="form-control adv"  data-ng-model="testData.DAY1.FEMALE.UPPERBOUND"/>
                                      </td>
                                  </tr>
                                  <tr>
                                    <td>
                                      Male
                                    </td>
                                      <td rowspan="2">
                                         <div class="input-group">
                                              <label class="input-group-addon"> &lt;=</label>
                                               <input type="text" class="form-control adv" data-ng-model="testData.DAY2.AGE"/>

                                          </div>
                                      </td>
                                      <td>
                                          <input type="text" class="form-control adv"  data-ng-model="testData.DAY2.MALE.LOWERBOUND"/>
                                      </td>
                                      <td>
                                          <input type="text" class="form-control adv"  data-ng-model="testData.DAY2.MALE.UPPERBOUND"/>
                                      </td>
                                  </tr>
                                  <tr>
                                      <td>
                                          Female
                                      </td>
                                      <td>
                                          <input type="text" class="form-control adv"  data-ng-model="testData.DAY2.FEMALE.LOWERBOUND"/>
                                      </td>
                                      <td>
                                          <input type="text" class="form-control adv"  data-ng-model="testData.DAY2.FEMALE.UPPERBOUND"/>
                                      </td>
                                  </tr>
                              </tbody>
                          </table>
                           <table class="table table-bordered">
                              <thead>
                                  <tr>
                                      <td>
                                          Sex
                                      </td>
                                      <td>
                                          Age(Mons)
                                      </td>
                                      <td>
                                          Lower Bound
                                      </td>
                                      <td>
                                          Upper Bound
                                      </td>
                                  </tr>
                              </thead>
                              <tbody <%-- data-ng-repeat="i in getNumber(2) track by $index"--%>>
                                  <tr>
                                    <td>
                                      Male
                                    </td>
                                      <td rowspan="2">
                                         <div class="input-group">
                                              <label class="input-group-addon"> &lt;=</label>
                                               <input type="text" class="form-control adv" data-ng-model="testData.MON1.AGE"/>

                                          </div>
                                      </td>
                                      <td>
                                          <input type="text" class="form-control adv"  data-ng-model="testData.MON1.MALE.LOWERBOUND"/>
                                      </td>
                                      <td>
                                          <input type="text" class="form-control adv"  data-ng-model="testData.MON1.MALE.UPPERBOUND"/>
                                      </td>
                                  </tr>
                                  <tr>
                                      <td>
                                          Female
                                      </td>
                                      <td>
                                          <input type="text" class="form-control adv"  data-ng-model="testData.MON1.FEMALE.LOWERBOUND"/>
                                      </td>
                                      <td>
                                          <input type="text" class="form-control adv"  data-ng-model="testData.MON1.FEMALE.UPPERBOUND"/>
                                      </td>
                                  </tr>
                                  <tr>
                                    <td>
                                      Male
                                    </td>
                                      <td rowspan="2">
                                         <div class="input-group">
                                              <label class="input-group-addon"> &lt;=</label>
                                               <input type="text" class="form-control adv" data-ng-model="testData.MON2.AGE"/>

                                          </div>
                                      </td>
                                      <td>
                                          <input type="text" class="form-control adv"  data-ng-model="testData.MON2.MALE.LOWERBOUND"/>
                                      </td>
                                      <td>
                                          <input type="text" class="form-control adv"  data-ng-model="testData.MON2.MALE.UPPERBOUND"/>
                                      </td>
                                  </tr>
                                  <tr>
                                      <td>
                                          Female
                                      </td>
                                      <td>
                                          <input type="text" class="form-control adv"  data-ng-model="testData.MON2.FEMALE.LOWERBOUND"/>
                                      </td>
                                      <td>
                                          <input type="text" class="form-control adv"  data-ng-model="testData.MON2.FEMALE.UPPERBOUND"/>
                                      </td>
                                  </tr>
                              </tbody>
                          </table>
                      </div>
                  </div>
              </div>
          </div>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-success btnDone" data-dismiss="modal">Done</button>
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>          
        </div>
      </div>
      
    </div>
  </div>
    </form>
</body>
</html>
