<%@ Control Language="C#" AutoEventWireup="true"  CodeBehind="ucHtmlBox.ascx.cs"  Inherits="WebLab.UserControls.ucHtmlBox" %>
<%@ Register Assembly="AISWebCommon" Namespace="AISWebCommon.AisControls" TagPrefix="AIS" %>
        <!-- Modal -->
           
            <div id="htmlboxmodal" runat=server class="modal fade" role="dialog">
              <div class="modal-dialog" style="width:85%;height:100%">

                <!-- Modal content-->
                <div class="modal-content">
                  <div class="modal-header" id="modalheader[]">
                    <%--<button runat="server" type="button" id="btnRefresh" class="close" data-dismiss="modal" ><i class="fa fa-refresh" aria-hidden="true"></i></button>--%>
                    <h4 class="modal-title">Editing</h4>
                     Choose Template&nbsp;&nbsp;&nbsp;<asp:DropDownList runat="server" ID="templst"   onchange="return setText(this.value)"></asp:DropDownList>
                  </div>
                    <asp:HiddenField ID="hdfval" runat="server" ClientIDMode="Static" />
                  <div class="modal-body">
                  <%--<button type="button" class="close" data-dismiss="modal">&times;</button>--%>
                   <%--    <textarea name="content" style="width:100%"></textarea>--%>
                    <asp:TextBox ID="TextBox2"  runat="server" TextMode="MultiLine"  Height="500px" Width="100%" ></asp:TextBox>
                    
                  </div>
                  <%--<div class="modal-footer">
                     <button type="button" class="btn btn-info">Save</button>
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                   
                  </div>--%>
                  
                </div>

              </div>
            </div>
