<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true"  CodeBehind="frmSampleCollection.aspx.cs" Inherits="WebLab.Forms.frmSampleCollection" %>
<%@ Register src="../UserControls/ucHtmlBox.ascx" TagName="htb" TagPrefix="htb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lab Tests</title>
    <script src="../Scripts/rtf_scriptfiles/tiny_mce.js" type="text/javascript"></script>
    <link href="../CSS/bootstrap.min337.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery.min320.js" type="text/javascript"></script>
    <script src="../js/bootstrap.min337.js" type="text/javascript"></script>
     <link href="../styles/prism.css" rel="stylesheet" type="text/css" />
    <link href="../styles/chosen.css" rel="stylesheet" type="text/css" />
    <script src="../scripts/prism.js" type="text/javascript"></script>
    <script src="../scripts/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
      function CloseWindow()
      {
       window.close();
      }
      
      $('#btnsave').click(function(e) {
        debugger;
        e.preventDefault();
        });
     function showCommentModal(modalid,id,curval,rowval,okid)
     {
     debugger;
//       var modaldisp=document.getElementById(modalid);
//       modaldisp.style.display="block";
//       modaldisp.classList.add("in");
       $("#"+modalid).modal();
       var closebtn=document.getElementById(id);
       closebtn.style.display="block";
       var okbtn=document.getElementById(okid);
       okbtn.style.display="block";
     }   
     
     function saveData()
     {
         debugger;
         //string labno, string pcode,string user,string conscode,string urgnorm
        var loader=document.getElementById("loadimg");
        loader.style.display="block";
        var ischecked = 0;
        var chkurgval=document.getElementById("rdurgent");
        var chknormval = document.getElementById("rdnormal");
        
        if (chkurgval.checked == true)
        {
            ischecked = 1;
         
        }

            $.ajax({
            type: "POST",
            url: "../Forms/frmSampleCollection.aspx/markSampleCollected",
            contentType: "application/json; charset=utf-8",
            data: "{labno:" + JSON.stringify($("#txtlabno").val()) + ",pcode: " + JSON.stringify($("#pcodehn").val()) + ",user:" + JSON.stringify($("#txtappby").val()) + ",conscode:" + JSON.stringify($("#lstConsultant").val()) + ",urgnorm:" + JSON.stringify(ischecked) + "}",
            dataType: "json",
            success: OnSuccess,
            failure: function(response) {
               var res=response.d;
               var loader=document.getElementById("loadimg");
               loader.style.display="none";
               showSuccessMessage(res,"error");
               
            }
        });
       return true;
     }
     
     function OnSuccess(response) {
           var loader=document.getElementById("loadimg");
           loader.style.display = "none";
           var res = response.d;
           showSuccessMessage(res, "success");
      }
      function getPreviousValue(testcode)
      {
        $.ajax({
            type: "POST",
            url: "../Forms/frmLabTests.aspx/getPreviousValueOfTest",
            contentType: "application/json; charset=utf-8",
            data: "{tescode:" + JSON.stringify(testcode) + "}",
            dataType: "json",
            success: OnSuccess1,
            failure: function(response) {
                console.log(response.d);
            }
        });
      
      }
      function OnSuccess1(response) {
            var pval=response.d;
            for(var i=0;i<pval.length;i++)
            {
               // alert(pval[i]["testval"]);
            }
            console.log(response.d);
      }
    
         tinyMCE.init({
            // General options
            mode: "textareas",
            theme: "advanced",
            plugins: "pagebreak,style,layer,table,save,advhr,advimage,advlink,emotions,iespell,inlinepopups",
           
        });
        function SendpatientCode(Appcode) {
            var MyArgs = new Array(Appcode);
            window.opener.SetPatient(MyArgs);
            window.close();
        }
        
        function closeModal(modalId,id,closebtnid)
        {
          debugger;
//          var mid=document.getElementById(modalId);
//          mid.style.display="none";
//          mid.classList.remove("in");
            $("#"+modalId).modal('hide');
          
          var closebtn=document.getElementById(closebtnid);
          closebtn.style.display="none";
          var okbtn=document.getElementById(id);
          okbtn.style.display="none";
        }
        function closeContextMenu(id)
        {
        debugger;
          var closeMenu=document.getElementById(id);
          closeMenu.style.display="none";
        }
        function openContextMenu(cnrl,rowcount)
        {
        debugger;         
            var element = document.getElementById(cnrl.id);
            var position = element.getBoundingClientRect();
            var x = position.left;
            var y = position.top-125;
            var eleold=document.getElementById("cmenu"+rowcount);  
            if(eleold==null)
            {
              iscreate=1;
            }
            else
            {
              iscreate=2;
            }
           if(iscreate==1)
           {
             var ele=document.createElement("div");
            var container=document.getElementById("uspanel");
            var iscreate=0;
            
            var child2=document.createElement("label");
            child2.setAttribute("style","color:white;width:100%;background-color:#18568D");
            child2.setAttribute("class","text-center");
           // child1.setAttribute("onclick","closeContextMenu('cmenu"+rowcount+"')");
            var i=0;
            var rownum=rowcount+2;
            var columnValue = document.getElementById("Table1").rows[rownum].cells[i].innerText;
           
            child2.innerText=columnValue;
            ele.appendChild(child2);
            
           // function showCommentModal(modalid,id,curval,rowval,okid)
            
            var child3=document.createElement("button");
            child3.setAttribute("type","button");
            child3.setAttribute("id","contmenuitem");
            child3.setAttribute("onclick","showCommentModal('uccommenthtml_"+rowcount+"_htmlboxmodal','uccommenthtml_"+rowcount+"_ok_"+rowcount+"','',"+rowcount+",'uccommenthtml_"+rowcount+"_close_"+rowcount+"')");
            subchild=document.createElement("i");
            subchild.setAttribute("class","fa fa-comment-o");
            subchild.setAttribute("aria-hidden","true");
            subchild.setAttribute("style","margin:0");
            child3.appendChild(subchild);
            subchild=document.createElement("label");
            subchild.innerText="  Comment";
            child3.appendChild(subchild);
            ele.appendChild(child3);
            
            var child4=document.createElement("button");
            child4.setAttribute("type","button");
            child4.setAttribute("id","contmenuitem");
            child4.setAttribute("onclick","markTest('cmenu"+rowcount+"')");
            subchild=document.createElement("i");
            subchild.setAttribute("class","fa fa-check");
            subchild.setAttribute("aria-hidden","true");
            subchild.setAttribute("style","margin:0");
            subchild.setAttribute("style","cursor:pointer");
            child4.appendChild(subchild);
            subchild=document.createElement("label");
            subchild.innerText="  Test Mark is normal";
            child4.appendChild(subchild);
            ele.appendChild(child4);
            
            
            var child1=document.createElement("button");
            child1.setAttribute("type","button");
            child1.setAttribute("id","contmenuitem");
            child1.setAttribute("style","background-color:#C90000;")
            child1.setAttribute("onclick","closeContextMenu('cmenu"+rowcount+"')");
            var subchild=document.createElement("i");
            subchild.setAttribute("class","fa fa-window-close-o");
            subchild.setAttribute("aria-hidden","true");
            subchild.setAttribute("style","cursor:pointer;");
            child1.appendChild(subchild);
            subchild=document.createElement("span");
            subchild.innerText="  Close";
            child1.appendChild(subchild);
            ele.appendChild(child1);
            
           
            
        
            
            
            ele.id="cmenu"+rowcount;
            ele.setAttribute("style","position:fixed;width:150px;height:150px;display:block;background-color:#2482D4;color:white;top:"+y+"px;left:"+x+"px");
            //ele.innerText="ContextMenu" ;
            ele.setAttribute("class","");
            ele.classList.add("boxshadow");
            
            container.appendChild(ele);
           
           }
           else
           {
               eleold.style.display="block";
           }
                
            
         
            
           //  console.log(x+' '+y);
            // alert(cnrl.id+' '+rowcount);
        }
        

          
          function showSuccessMessage(Message,type)
          {
              
              debugger;
              var msgbox=document.getElementById("msg");
              var msgtype=document.getElementById("msgtype");
              
              if(type=='error')
              {
                msgtype.innerText="Error";
              }
              if(type=='success')
              {
                msgtype.innerText="Message";
              }
              msg.innerText=Message;
              $("#msgModal").modal();
          
          }
        
        
    </script>
    

    <style>
        .login {
  background: #eceeee;
  border: 1px solid #42464b;
  border-radius: 6px;
  height: 300px;
  margin: 20px auto 0;
  width: 80%;
  padding:20px;
}
          .loader
      {
      	height:100%;
      	z-index:1;
      	width:100%;
      	position:fixed;
      	top:0;
      	background-color:White;
      	opacity:0.5;
      	
      	
      	}

/*.login h1 {
  background-image: linear-gradient(top, #f1f3f3, #d4dae0);
  border-bottom: 1px solid #a6abaf;
  border-radius: 6px 6px 0 0;
  box-sizing: border-box;
  color: #727678;
  display: block;
  height: 43px;
  font: 600 14px/1 'Open Sans', sans-serif;
  padding-top: 14px;
  margin: 0;
  text-align: center;
  text-shadow: 0 -1px 0 rgba(0,0,0,0.2), 0 1px 0 #fff;
}
input[type="password"], input[type="text"] {
  background: url('http://i.minus.com/ibhqW9Buanohx2.png') center left no-repeat, linear-gradient(top, #d6d7d7, #dee0e0);
  border: 1px solid #a1a3a3;
  border-radius: 4px;
  box-shadow: 0 1px #fff;
  box-sizing: border-box;
  color: #696969;
  height: 39px;
  margin: 31px 0 0 29px;
  padding-left: 37px;
  transition: box-shadow 0.3s;
  width: 240px;
}
input[type="password"]:focus, input[type="text"]:focus {
  box-shadow: 0 0 4px 1px rgba(55, 166, 155, 0.3);
  outline: 0;
}
.show-password {
  display: block;
  height: 16px;
  margin: 26px 0 0 28px;
  width: 87px;
}
input[type="checkbox"] {
  cursor: pointer;
  height: 16px;
  opacity: 0;
  position: relative;
  width: 64px;
}
input[type="checkbox"]:checked {
  left: 29px;
  width: 58px;
}
.toggle {
  background: url(http://i.minus.com/ibitS19pe8PVX6.png) no-repeat;
  display: block;
  height: 16px;
  margin-top: -20px;
  width: 87px;
  z-index: -1;
}
input[type="checkbox"]:checked + .toggle { background-position: 0 -16px }
.forgot {
  color: #7f7f7f;
  display: inline-block;
  float: right;
  font: 12px/1 sans-serif;
  left: -19px;
  position: relative;
  text-decoration: none;
  top: 5px;
  transition: color .4s;
}
.forgot:hover { color: #3b3b3b }
input[type="submit"] {
  width:240px;
  height:35px;
  display:block;
  font-family:Arial, "Helvetica", sans-serif;
  font-size:16px;
  font-weight:bold;
  color:#fff;
  text-decoration:none;
  text-transform:uppercase;
  text-align:center;
  text-shadow:1px 1px 0px #37a69b;
  padding-top:6px;
  margin: 29px 0 0 29px;
  position:relative;
  cursor:pointer;
  border: none;  
  background-color: #37a69b;
  background-image: linear-gradient(top,#3db0a6,#3111);
  border-top-left-radius: 5px;
  border-top-right-radius: 5px;
  border-bottom-right-radius: 5px;
  border-bottom-left-radius:5px;
  box-shadow: inset 0px 1px 0px #2ab7ec, 0px 5px 0px 0px #497a78, 0px 10px 5px #999;
}




input[type="submit"]:active {
  top:3px;
  box-shadow: inset 0px 1px 0px #2ab7ec, 0px 2px 0px 0px #31524d, 0px 5px 3px #999;
}
      .loader
      {
      	height:100%;
      	z-index:1;
      	width:100%;
      	position:fixed;
      	top:0;
      	background-color:White;
      	opacity:0.5;
      	
      	
      	}*/  	
      
    </style>
    
    <%--margin: 4px 0;
        box-sizing: border-box;
        border: 3px solid #ccc;
        -webkit-transition: 0.5s;
        transition: 0.5s;
        outline: none;--%>
</head>
<body>
    <form id="form1" runat="server">
 <%--   <input list="browsers">

<datalist id="browsers">
  <option value="Internet Explorer">
  <option value="Firefox">
  <option value="Google Chrome">
  <option value="Opera">
  <option value="Safari">
</datalist>--%>

    <div class="container-fluid" id="mainContainer">
        <div class="loader text-center"  id="loadimg" style="display:none">
            <div style="font-size:20px;font-weight:bold;color:Blue;z-index:1;">Saving! Please Wait........</div>
            <img src="../Images/loader945435.GIF" style="height:60px;width:60px;z-index:1;"/>
        </div>
        
        <div class="row" style="padding-top: 5px; padding-bottom: 5px; background-color: Gray;color:white;font-weight:bold;font-size:30px; text-align: center;top:0;">
            <div class="col-lg-12">
            <asp:HiddenField  runat="server" ID="pcodehn"  Value=""/>
              <div>Sample Collection</div> 
              <%--<asp:Button ID="btnOk" runat="server" Text="Button" onclick="btnOk_Click" />--%> </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
              <div class="login">
                      <div class = "input-group">
                         <span class = "input-group-addon">Lab No.</span>
                         <input type ="text" runat="server" id="txtlabno" class = "form-control"  placeholder = "Lab No." readonly="readonly"/>
                      </div>
                         <div class = "input-group">
                         <span class = "input-group-addon">Name</span>
                         <input type ="text" runat="server"  id="txtname" class = "form-control"  placeholder = "Name" readonly="readonly"/>
                       </div>
                   
                     <div class = "input-group">
                         <span class = "input-group-addon">Date</span>
                         <input type ="text" runat="server"  id="txtappdate" class = "form-control"  placeholder = "Approve Date" readonly="readonly"/>
                       </div>
                     <div class = "input-group">
                         <span class = "input-group-addon">User</span>
                         <input type ="text" runat="server"  id="txtappby" class = "form-control"  placeholder = "Approved By" readonly="readonly"/>
                       </div>
                      <div class = "input-group">
                         <span class = "input-group-addon">Collected By Consultant</span>
                         <asp:ListBox ID="lstConsultant"  ClientIDMode="Static" runat ="server" data-placeholder="Choose Consultant(s)" class="chosen-select" SelectionMode="Single" Style="width: 100%" onchange="getSelectedConsultant(this.value)"></asp:ListBox>
                         <script type="text/javascript">
                            var config = {
                                '.chosen-select': {},
                                '.chosen-select-deselect': { allow_single_deselect: true },
                                '.chosen-select-no-single': { disable_search_threshold: 10 },
                                '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
                                '.chosen-select-width': { width: "95%" }
                            }
                            for (var selector in config) {
                                $(selector).chosen(config[selector]);
                            }
                        </script> 
                      </div>
                  <div class = "input-group">
                    
                            <asp:RadioButton runat="server" ID="rdnormal" GroupName="casetype" Text="Normal" Checked="true"/>
                            &nbsp;&nbsp;&nbsp;<asp:RadioButton runat="server" ID="rdurgent" Selected="True" GroupName="casetype" Text="Urgent" />
   
                   </div>
                  <div class = "text-right" style="margin-top:20px; ">
                        <button id="btnsave" type="button" class="btn btn-sm btn-info" onclick="return saveData()"><i class="fa fa-floppy-o" aria-hidden="true"></i>&nbsp;&nbsp;Save</button>
                        <button id="Button1"  class="btn btn-sm btn-danger" onclick="CloseWindow()"><i class="fa fa-times" aria-hidden="true" ></i>&nbsp;&nbsp;Close</button> 
                  </div>
                 </div>
                <div class="shadow"></div>
            </div>
        </div>
        
       
    </div>
      <!-- Modal -->
          <div class="modal fade" id="msgModal" role="dialog">
            <div class="modal-dialog modal-lg">
              <div class="modal-content">
                <div class="modal-header" style="background-color:#5C9CCC;">
                  <button type="button" class="close" data-dismiss="modal">&times;</button>
                  <h4 class="modal-title" id="msgtype">Modal Header</h4>
                </div>
                <div class="modal-body">
                  <div id="msg">Message Content</div>
                </div>
                <div class="modal-footer" style="background-color:#5C9CCC;">
                  <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
              </div>
            </div>
          </div>
    
    </form>
</body>
</html>
