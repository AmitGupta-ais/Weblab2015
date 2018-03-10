<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucTabular.ascx.cs" Inherits="WebLab.UserControls.ucTabular" %>
<!-- Modal -->
            <div id="htmlboxmodal" runat=server class="modal fade" role="dialog">
              <div class="modal-dialog">

                <!-- Modal content-->
                <div class="modal-content">
                  <div class="modal-header" id="modalheader[]">
                    <button runat="server" type="button" id="btnRefresh" class="close" data-dismiss="modal"><i class="fa fa-refresh" aria-hidden="true"></i></button>
                    <h4 class="modal-title">Tablular</h4>
                  </div>
                  <div class="modal-body">
                  <asp:Table>  
                     <asp:TableHeaderRow>
                        <asp:TableHeaderCell>Test Name</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Result</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Normal Range</asp:TableHeaderCell>
                    </asp:TableHeaderRow>
                  </asp:Table>  
                    
                  </div>
                  <%--<div class="modal-footer">
                     <button type="button" class="btn btn-info">Save</button>
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                    
                  </div>--%>
                </div>

              </div>
            </div>
