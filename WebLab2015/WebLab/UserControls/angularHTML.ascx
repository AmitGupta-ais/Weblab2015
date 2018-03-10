<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="angularHTML.ascx.cs" Inherits="WebLab.angularHTML" %>
<%@ Register Assembly="AISWebCommon" Namespace="AISWebCommon.AisControls" TagPrefix="AIS" %>

        <div id="mapSrvModal" runat="server" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <div class="modal-header">
                            <h4 class="modal-title"><asp:Label runat="server" ID="txtHead"></asp:Label></h4>
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>
                    </div>
                    <asp:TextBox runat="server" ID="txtHtml" data-ng-modal="_HTML"></asp:TextBox>
                    <div class="modal-body">
                        <div class="row">
                            <div>
                                <div text-angular="text-angular" name="testData.interpretation" data-ng-model="_HTML"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>