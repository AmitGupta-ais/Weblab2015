<%@ Page Title="Test Head Wise Library" Language="C#" MasterPageFile="~/Masters/menuHead.Master" AutoEventWireup="true" CodeBehind="frmTestHeadLib.aspx.cs" Inherits="WebLab.Forms.frmTestHeadLib" ClientIDMode="Static" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../js/angular.min.js"></script>
    <style>
        .link {
            color:#000000;
        }
        .link:hover {
            color:#428bca;
        }
        .navbar-inverse .navbar-toggle:focus, .navbar-inverse .navbar-toggle:hover {
            background-color: #ffd800;
        }
        .navbar-inverse .navbar-nav>li>a:focus, .navbar-inverse .navbar-nav>li>a {
            color: #ffd800;
            background-color: transparent;
        }
        .navbar, .navbar-inverse {
            background-color:#428bca;
        }
        .glyphicon, .glyphicon-tasks {
            background-color:#ffffff;
        }
        .navbar-toggle {
            background-color:#ffffff;
        }
        .sidelist{
            margin-top:10px;
        }
        .form-contrl {
            width:100%;
        }
        .custBorder {
            border-top-style: none;
            border-right-style: solid;
            border-bottom-style: none;
            border-left-style: solid;
        }
        .headborder {
            border-bottom-width:thin;
            border-top-width:thin;
            border-top-style: solid;
            border-right-style: none;
            border-bottom-style: solid;
            border-left-style: none;
            background:#ffd800;
        }
    </style>
    <script type="text/javascript">
        var headr = { "Content-Type": "application/x-www-form-urlencoded" };
        var app = angular.module("testHeadLib", []);
        app.config(function ($locationProvider) {
            $locationProvider.html5Mode({
                enabled: true,
                requireBase: false
            });
        });
         app.controller("ctrlTestHeadLib", function ($scope, $http, $location) {
             $scope.getTestHead = function () {
                 var dept = {"dept":$scope.dept};
                 $http.post("webLabTest.asmx/loadTestHead",dept , headr)
                     .then(function sucess(result) {
                         $scope.testHead = result.data.d;
                     },
                     function eroor(response) {

                     })
             }
             $scope.openTestHead = function (code)
             {
                 var url = "frmAddTestHead.aspx?MODE=0&CODE="+code;
                 window.open(url);
             }
             $scope.openTest = function (code,name)
             {
                 var data = { "Code": code };
                 $http.post("webLabTest.asmx/loadTestUnderHead", data, headr)
                     .then(function sucess(result) {
                         $scope.testsUnder = result.data.d;
                         var ele = document.getElementById("TestHead");
                         ele.innerHTML = name;
                         ele.focus();
                         document.getElementById("searchtest").focus();
                     },
                     function eroor(response) {

                     })
             }
             $scope.openTestDetails = function (code)
             {
                 
                 var url = "frmTestDetails.aspx?apCode=&Code=" + code.trim() + "&MODE=2";
                 window.open(url);
             }
             $scope.addNewTest = function ()
             {
                 var url = "frmAddTestHead.aspx?MODE=1";
                 window.open(url);
             }
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div data-ng-app="testHeadLib" data-ng-controller="ctrlTestHeadLib">
      <div class="navbar navbar-inverse">
          <div class="container-fluid">
              <div class="navbar-header">
                  <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#menuBar">
                        <span class="glyphicon glyphicon-tasks"></span>
                    </button>
              </div>
              <div class="collapse navbar-collapse" id="menuBar">
                    <ul class="nav navbar-nav">
                        <li>
                            <a class="link" href="#" data-ng-click="addNewTest()">Add New Test Head</a>
                        </li>
                        <li>
                            <a class="link" href="#">Delete Test Head</a>
                        </li>
                    </ul>
              </div>
          </div>

      </div>
      
      <%--<div class="row">
          <div class="btn-group col-lg-3 col-md-3 col-sm-3 col-xs-12">
              <button type="button" class="btn btn-warning">Add New Test Head</button>
              <button type="button" class="btn btn-danger">Delete Test Head</button>
          </div> 
      </div>--%>
    <div class="row" style="min-height:80vh;padding-left:20px;padding-right:20px">
        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12 sidenav custBorder" style="height:100vh">
            <div class="row" style="padding:4px 4px 0px 4px;">
                <div class="input-group">
                    <label class="input-group-addon">Department</label>
                    <asp:DropDownList runat="server" ID="ddlDept" data-ng-model="dept" CssClass="form-control" data-ng-change="getTestHead()" ></asp:DropDownList>
                </div>
            </div>
            <%--<div class="row" style="text-align:right;padding:0px 4px 4px 4px">
                <button type="button" class="btn btn-success" id="Button1" runat="server" data-ng-click="getTestHead()">
                    GO&nbsp;<span class="glyphicon glyphicon-triangle-right"></span>
                </button>
            </div>--%>
            <div class="row">
                <div class="input-group"><span class="input-group-addon glyphicon glyphicon-search"></span><input type="text" id="searchtesthead" data-ng-model="srchTeastHead" placeholder="Search for Test Head.." class="form-control" /></div>
            </div>
            <div class="row" style="padding-bottom:0em;margin-bottom:0px;overflow-y:overlay">
                <table  class="table table-condensed table-hover table-responsive" style="margin-bottom:0px">
                    <thead style="overflow-y:overlay">
                        <tr class="success">
                            <th style="width: 33%">Code</th>
                            <th>Test Name</th>
                        </tr>
                    </thead>
                </table>
            </div>
            <div class="row" style="overflow-y:overlay;max-height:70vh;">
                <table class="table table-condensed table-hover table-responsive" style="table-layout:fixed;margin-bottom:5em">
                    <tbody data-ng-repeat="x in testHead|filter:srchTeastHead">
                        <tr id="head_{{$index}}" class="active" style="cursor: pointer" data-ng-dblclick="openTestHead(x.Code)" data-ng-click="openTest(x.Code,x.TestName)">
                            <td id="{{$index}}_Code" style="width: 33%">{{x.Code}}</td>
                            <td id="{{$index}}_Name">{{x.TestName}}</td>
                        </tr>
                    </tbody>
                    <tfoot>
                        <tr style="height:50px">
                            <td>
                                &nbsp;
                            </td><td>
                                &nbsp;
                            </td>
                        </tr>
                    </tfoot>
                </table>
            </div>
        </div>
        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-12" style="overflow-x:overlay;max-width:100vw">
            <div class="row headborder">
                <span id="TestHead" class="input-group-addon headborder">&nbsp;</span>
            </div>
            <div class="row">
                <div class="input-group"><span class="input-group-addon glyphicon glyphicon-search"></span><input type="text" id="searchtest" data-ng-model="searchTest" placeholder="Search for Tests.." class="form-control" /></div>
            </div>
            <div class="row" style="padding-bottom:0em;margin-bottom:0px;overflow-y:overlay" >
                <table class="table table-condensed table-hover table-responsive" style="margin-bottom:0px">
                    <thead style="overflow-y:overlay">
                        <tr class="success" style="overflow-y:overlay">
                            <th style="max-width: 10%">SNO</th>
                            <th style="width: 10%">CODE</th>
                            <th style="width: 50%">NAME</th>
                            <th style="width: 10%">LBOUND</th>
                            <th style="width: 10%">UBOUND</th>
                            <th style="width: 10%">UNITS</th>
                        </tr>
                    </thead>
                </table>
            </div>
            <div class="row" style="overflow-y:overlay;max-height:70vh;">
                <table class="table table-condensed table-hover table-responsive" style="margin-bottom:5em">
                    <tbody data-ng-repeat="x in testsUnder |filter:searchTest|orderBy : 'Sno'" style="padding-bottom:2em">
                        <tr style="cursor:pointer" data-ng-dblclick="openTestDetails(x.testcode)" data-ng-attr-class="{{x.isMain && 'active' ||'info'}}">
                            <td style="min-width: 10%">{{x.Sno}}</td>
                            <td style="min-width: 10%">{{x.testcode}}</td>
                            <td style="min-width: 50%">{{x.testhead}}</td>
                            <td style="min-width: 10%">{{x.lowbound}}</td>
                            <td style="min-width: 10%">{{x.upbound}}</td>
                            <td style="min-width: 10%">{{x.unit}}</td>
                        </tr>
                    </tbody>
                    <tfoot>
                        <tr style="height:50px">
                            <td>
                                &nbsp;
                                
                            </td><td>
                                &nbsp;
                            </td><td>
                                &nbsp;
                            </td><td>
                                &nbsp;
                            </td><td>
                                &nbsp;
                            </td><td>
                                &nbsp;
                            </td>
                        </tr>
                    </tfoot>
                </table>
            </div>
        </div>
       
    </div>
  </div>
</asp:Content>

