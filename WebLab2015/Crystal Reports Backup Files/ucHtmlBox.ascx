<%@ Control Language="C#" AutoEventWireup="true"  CodeBehind="ucHtmlBox.ascx.cs"  Inherits="WebLab.UserControls.ucHtmlBox" %>
<%@ Register Assembly="AISWebCommon" Namespace="AISWebCommon.AisControls" TagPrefix="AIS" %>
        <!-- Modal -->
            <div id="htmlboxmodal" runat=server class="modal fade" role="dialog">
              <div class="modal-dialog">

                <!-- Modal content-->
                <div class="modal-content">
                  <div class="modal-header" id="modalheader[]">
                    <button runat="server" type="button" id="btnRefresh" class="close" data-dismiss="modal" ><i class="fa fa-refresh" aria-hidden="true"></i></button>
                    <h4 class="modal-title">Editing</h4>
                  </div>
                  <div class="modal-body">
                  <%--<button type="button" class="close" data-dismiss="modal">&times;</button>--%>
                    <asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine"  Height="100%" Width="100%" ></asp:TextBox>
                    
                  </div>
                  <%--<div class="modal-footer">
                     <button type="button" class="btn btn-info">Save</button>
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                    
                  </div>--%>
                </div>

              </div>
            </div>
