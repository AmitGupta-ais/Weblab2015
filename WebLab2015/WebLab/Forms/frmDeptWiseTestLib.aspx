<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/menuHead.Master" AutoEventWireup="true" CodeBehind="frmDeptWiseTestLib.aspx.cs" Inherits="WebLab.Forms.frmDeptWiseTestLib" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .sort {
        cursor:pointer;
        }
    </style>
    <script src="../js/angular.min.js"></script>
    <script>
        var headr = { "Content-Type": "application/x-www-form-urlencoded" };
        var app = angular.module("appLib", []);
        app.config(function ($locationProvider) {
            $locationProvider.html5Mode({
                enabled: true,
                requireBase: false
            });
        });
        app.controller("ctrlLib", function ($scope, $http, $location) {
            $scope.sortType = 'Name'; // set the default sort type
            $scope.sortReverse = false;  // set the default sort order
            $scope.loadData = function () {
                var dept = { "dept": $scope.dept };
                $http.post("frmDeptWiseTestLib.aspx/loadData", dept, headr)
                     .then(function sucess(result) {
                         $scope.Tests = result.data.d;
                         console.log($scope.Tests[10].LabID);
                         document.getElementById('srchtest').focus();
                     },
                     function eroor(response) {

                     })
            }
            $scope.openTest = function (x) {
                var url = "frmTestDetails.aspx?apCode=" + x.Code + "&Code=" + x.LabID;
                window.open(url);
            }
            
            $scope.checksubTest = function (x) {
                var data = {
                    "apCode": x.Code,
                    "Dept": $scope.dept,
                    "Code":x.LabID
                }
                $http.post("webLabTest.asmx/Validate", data, headr)
                     .then(function sucess(result) {
                         var ret = result.data.d;
                         $scope.getSubTest(x);
                         
                     },
                     function eroor(response) {
                         //handle error
                     })
            }
            $scope.getSubTest = function (x) {
                var data = {
                    "labid": x.LabID,
                    "apServCode": x.Code,
                };
                $http.post("webLabTest.asmx/subTest", data , headr)
                .then(function success(result) {
                    $scope.subTests = result.data.d;
                    if ($scope.subTests.length == 0) {
                        $scope.subTests = null;
                        document.getElementById('subTestTable').classList.value = "ng-hide";
                        document.getElementById('testTable').classList.value = "col-lg-12 col-md-12 col-sm-12 col-xs-12";

                    }
                    else {
                        document.getElementById('subTestTable').classList.value = "col-lg-3 col-md-3 col-sm-3 col-xs-12 ng-hide";
                        document.getElementById('testTable').classList.value = "col-lg-9 col-md-9 col-sm-9 col-xs-12";

                    }
                    document.getElementById('srchSubtest').focus();
                }, function error(err) {
                    //handle error
                })
            }
        })
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row" data-ng-app="appLib" data-ng-controller="ctrlLib" style="overflow:auto">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding-bottom:2em">
            <div class="input-group">
                <span class="input-group-addon">Department</span>
                <asp:DropDownList runat="server" ID="ddlDept" data-ng-change="loadData()" data-ng-model="dept" CssClass="form-control"></asp:DropDownList>
            </div>
        </div>
       <div id="testTable" class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="max-width:100vw">
           <div class="row text-center">
                <div class="input-group " style="padding-left:15px;padding-right:15px"><span class="input-group-addon glyphicon glyphicon-search"></span><input type="text" id="srchtest" data-ng-model="srchtest" placeholder="Search for Test.." class="form-control" /></div>
            </div>
           <div>
               <table style="width:100%;margin-bottom:0px" class="table tab-content table-responsive">
                   <thead>
                       <tr class="success">
                           <th class="sort" style="width:10%" data-ng-click="sortType = 'Code'; sortReverse = !sortReverse">
                               Code
                               <span data-ng-show="sortType == 'Code' && !sortReverse" class="glyphicon glyphicon-chevron-down"></span>
                               <span data-ng-show="sortType == 'Code' && sortReverse" class="glyphicon glyphicon-chevron-up"></span> 
                           </th>
                           <th class="sort" style="width:34%" data-ng-click="sortType = 'Name'; sortReverse = !sortReverse">
                               Name
                               <span data-ng-show="sortType == 'Name' && !sortReverse" class="glyphicon glyphicon-chevron-down"></span>
                               <span data-ng-show="sortType == 'Name' && sortReverse" class="glyphicon glyphicon-chevron-up"></span>
                               </th>
                           <th class="sort" style="width:13%" data-ng-click="sortType = 'Rate'; sortReverse = !sortReverse">
                               Rate
                               <span data-ng-show="sortType == 'Rate' && !sortReverse" class="glyphicon glyphicon-chevron-down"></span>
                               <span data-ng-show="sortType == 'Rate' && sortReverse" class="glyphicon glyphicon-chevron-up"></span>
                               </th>
                           <th class="sort" style="width:13%" data-ng-click="sortType = 'LabID'; sortReverse = !sortReverse">
                               TestID
                               <span data-ng-show="sortType == 'LabID' && !sortReverse" class="glyphicon glyphicon-chevron-down"></span>
                               <span data-ng-show="sortType == 'LabID' && sortReverse" class="glyphicon glyphicon-chevron-up"></span>
                               </th>
                           <th class="sort" style="width:30%" data-ng-click="sortType = 'LabName'; sortReverse = !sortReverse">
                               LabName
                               <span data-ng-show="sortType == 'LabName' && !sortReverse" class="glyphicon glyphicon-chevron-down"></span>
                               <span data-ng-show="sortType == 'LabName' && sortReverse" class="glyphicon glyphicon-chevron-up"></span>
                               </th>
                       </tr>
                   </thead>
               </table>
           </div>
           <div style="max-height:70vh;overflow-y:scroll">
               <table class="table table-condensed table-hover table-responsive" >
                   <tbody>
                       <tr data-ng-repeat="x in Tests|orderBy:sortType:sortReverse|filter:srchtest" data-ng-click="checksubTest(x)" data-ng-dblclick="openTest(x)" data-ng-attr-class="{{x.valid && 'active' || 'warning'}}">
                           <td style="width:10%">{{x.Code}}</td>
                           <td style="width:34%">{{x.Name}}</td>
                           <td style="width:13%" data-ng-attr-class="{{x.testValidity && 'active' || 'danger'}}">{{x.Rate}}</td>
                           <td style="width:13%">{{x.LabID}}</td>
                           <td style="width:30%">{{x.LabName}}</td>
                       </tr>
                   </tbody>
                   <tfoot>
                       <tr>
                           <td></td><td></td><td></td><td></td><td></td>
                       </tr>
                   </tfoot>
               </table>
           </div>
       </div>
        <div id="subTestTable" class="col-lg-3 col-md-3 col-sm-3 col-xs-12" data-ng-show="subTests">
            <div class="row text-center">
                <div class="input-group " style="padding-left:15px;padding-right:15px"><span class="input-group-addon glyphicon glyphicon-search"></span><input type="text" id="srchSubtest" data-ng-model="srchSubtest" placeholder="Search for Test.." class="form-control" /></div>
            </div>
           <div>
               <table style="width:100%;margin-bottom:0px" class="table tab-content table-responsive">
                   <thead>
                       <tr class="success">
                           <th style="width:30%">Code</th>
                           <th style="width:35%">Name</th>
                           <th style="width:35%">Method</th>
                       </tr>
                   </thead>
               </table>
           </div>
           <div style="max-height:70vh;overflow-y:scroll">
               <table class="table table-condensed table-hover table-responsive" >
                   <tbody>
                       <tr data-ng-repeat="x in subTests|filter:srchSubtest" data-ng-dblclick="openTest(x)">
                           <td style="width:30%">{{x.LabID}}</td>
                           <td style="width:35%">{{x.Name}}</td>
                           <td style="width:35%">{{x.Method}}</td>
                       </tr>
                   </tbody>
                   <tfoot>
                       <tr>
                           <td></td><td>
                       </tr>
                   </tfoot>
               </table>
           </div>
        </div>
    </div>
</asp:Content>
