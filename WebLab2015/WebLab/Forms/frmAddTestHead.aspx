<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmAddTestHead.aspx.cs" Inherits="WebLab.Forms.frmAddTestHead" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, maximum-scale=1, initial-scale=1, user-scalable=0"/>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta content="IE=edge,chrome=1" http-equiv="X-UA-Compatible"/>
    <script type="text/javascript" src="../js/angular.min.js"></script>
    <script type="text/javascript" src="../Bootstrap/jquery.min.js"></script>
    <script type="text/javascript" src="../Bootstrap/bootstrap.min.js"></script>
    <link rel="stylesheet" href="../Bootstrap/bootstrap.min.css" />
    <title>Add/Update Test Head</title>

    <script>
        var header = { "Content-Type": "application/x-www-form-urlencoded" };
        var app = angular.module("appAddUpdt", []);
        app.config(function ($locationProvider) {
            $locationProvider.html5Mode({
                enabled: true,
                requireBase: false
            });
        });
        app.controller("ctrlAddUpdt", function ($scope, $http, $location) {
            $scope.Code = $location.search().CODE;
            $scope.Mode = $location.search().MODE;
            if ($scope.Mode == 0)//EDIT MODE
            {
                var data2send = {"Code":$scope.Code};
                $http.post("webLabTest.asmx/editTest", data2send, header)
                .then(function suncess(res) {
                    $scope.headData = res.data.d;
                }, function error(err) {
                    //Handle err
                })
            }
            else if ($scope.Mode == 1)//NEW MODE
            {

            }
        });
    </script>
</head>
<body>
    <form id="form1" runat="server" data-ng-app="appAddUpdt" data-ng-controller="ctrlAddUpdt">
    <div class="container-fluid">
        <div class="panel panel-primary">
            <div class="panel-heading text-center">Add/Update Test Head</div>
            <div class="panel-body">
                <div class="list-group-item">
                    <div class="row text-center">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <div class="btn-group">
                                <button type="button" class="btn btn-success">Save</button>
                                <button type="button" class="btn btn-warning">Cancel</button>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="list-group-item">
                    <div class="row">
                        <div class="col-lg-2 col-sm-2col-md-2 col-xs-12">
                            <div class="input-group">
                                <span class="input-group-addon">Code</span>
                                <input type="text" class="form-control"  data-ng-model="headData.Code"/>
                            </div>
                        </div>
                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                            <div class="input-group">
                                <span class="input-group-addon">Name</span>
                                <input type="text" class="form-control" data-ng-model="headData.TestName"/>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-6">
                            <div class="input-group">
                                <span class="input-group-addon">No. of lines</span>
                                <input type="text" class="form-control" data-ng-model="headData.numLINES"/>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-6">
                            <div class="input-group ">
                                <span class="input-group-addon">Sl. No.</span>
                                <input type="text" class="form-control" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                            <H4>Remarks</H4>
                            <div class="form-group">
                                <textarea class="form-control" rows="4"></textarea>
                            </div>
                        </div>
                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                            <H4>Signatory</H4>
                            <div class="form-group" style="">
                                <div class="form-control" style="height:96px">
                                    {{SIGNATORY}}
                                </div>
                            </div>
                            <%--<div class="form-group">
                                <textarea class="form-control" rows="4"></textarea>
                            </div>--%>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                            <div class="input-group">
                                <span class="input-group-addon">Condition</span>
                                <input type="text" class="form-control"/>
                            </div>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                            <div class="input-group">
                                <span class="input-group-addon">Pre Comments</span>
                                <input type="text" class="form-control"/>
                            </div>
                        </div><div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                            <div class="input-group">
                                <span class="input-group-addon">Estimate Perform Time</span>
                                <input type="text" class="form-control"/>
                                <span class="input-group-addon">In Minutes</span>
                            </div>
                        </div>
                    </div>
                    <div class="row list-group-item">
                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12">
                            <input type="checkbox"/>                            
                            <span>Units</span>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12">
                            <input type="checkbox"/>
                            <span>Reference Range</span>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12">
                            <input type="checkbox"/>
                            <span>Test Name Result</span>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12">
                            <input type="checkbox"/>
                            <span>Head Name</span>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12">
                            <input type="checkbox"/>
                            <span>Culture</span>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12">
                            <input type="checkbox"/>
                            <span>Method</span>
                        </div>
                    </div>
                    <div class="row list-group-item">
                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12">
                            <input type="checkbox"/>
                            <span>Required Approval</span>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12">
                            <input type="checkbox"/>
                            <span>Required Booked Test</span>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12">
                            <input type="checkbox"/>
                            <span>Text in Box</span>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12">
                            <input type="checkbox"/>
                            <span>Required Prev Result</span>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                            <input type="checkbox"/>
                            <span>Required LowHigh in Seprate Column</span>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel-footer"></div>
        </div>
    </div>
    </form>
</body>
</html>
