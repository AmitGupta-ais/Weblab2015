<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true"  CodeBehind="frmApproveTest.aspx.cs" Inherits="WebLab.Forms.frmApproveTest" %>
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
        var loader=document.getElementById("loadimg");
        loader.style.display="block";
        var labno=document.getElementById("labnohdn");
        JsonObj=[];

        $("#Table1").find(":input[type=text]").each(function(){
             
            var cnt=this.id.split('_');
           // alert( $(this).val()+" "+$("#hdfld_"+cnt[1]).val());
            //alert($(this).val());
            item={};
            item["testcode"]=$("#hdfld_"+cnt[1]).val();//uchtml_0_TextBox2_ifr
            item["testval"]=$(this).val();
            item["labno"] = labno.value
            item["controltype"] = "textbox";
            item["comment"] = $("#uccommenthtml_" + cnt[1] + "_TextBox2_ifr").contents().find("#tinymce").html();
            JsonObj.push(item);

        });
        $("iframe").each(function(){
           
            var cnt=this.id.split('_');
           // alert( $(this).val()+" "+$("#hdfld_"+cnt[1]).val());
            var editboxid=this.id;
           // alert(editboxid+"id");
            if (cnt[0] == 'uchtml')
            {
                item={};
                item["testcode"] = $("#hdfld_" + cnt[1]).val();//uchtml_0_TextBox2_ifr
                item["controltype"] = "htmlbox";
                item["testval"] = $(this).contents().find("#tinymce").html();
                //console.log($(this).contents().find("#tinymce").html());
                item["labno"]=labno.value
                item["comment"]="";
                JsonObj.push(item);
            }

        });  
        
       //var x = document.getElementById("Table1").rows.length;
            console.log(JSON.stringify(JsonObj));
            $.ajax({
            type: "POST",
            url: "../Forms/frmLabTests.aspx/saveData",
            contentType: "application/json; charset=utf-8",
            data: "{lt:" + JSON.stringify(JsonObj) + "}",
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
           loader.style.display="none";
           showSuccessMessage("Successfully Saved","success");
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
        
          function convertHtmlToRtf(html) {
              if (!(typeof html === "string" && html)) {
                  return null;
              }

              var tmpRichText, hasHyperlinks;
              var richText = html;

              // Singleton tags
              richText = richText.replace(/<(?:hr)(?:\s+[^>]*)?\s*[\/]?>/ig, "{\\pard \\brdrb \\brdrs \\brdrw10 \\brsp20 \\par}\n{\\pard\\par}\n");
              richText = richText.replace(/<(?:br)(?:\s+[^>]*)?\s*[\/]?>/ig, "{\\pard\\par}\n");

              // Empty tags
              richText = richText.replace(/<(?:p|div|section|article)(?:\s+[^>]*)?\s*[\/]>/ig, "{\\pard\\par}\n");
              richText = richText.replace(/<(?:[^>]+)\/>/g, "");

              // Hyperlinks
              richText = richText.replace(
                  /<a(?:\s+[^>]*)?(?:\s+href=(["'])(?:javascript:void\(0?\);?|#|return false;?|void\(0?\);?|)\1)(?:\s+[^>]*)?>/ig,
                  "{{{\n");
              tmpRichText = richText;
              richText = richText.replace(
                  /<a(?:\s+[^>]*)?(?:\s+href=(["'])(.+)\1)(?:\s+[^>]*)?>/ig,
                  "{\\field{\\*\\fldinst{HYPERLINK\n \"$2\"\n}}{\\fldrslt{\\ul\\cf1\n");
              hasHyperlinks = richText !== tmpRichText;
              richText = richText.replace(/<a(?:\s+[^>]*)?>/ig, "{{{\n");
              richText = richText.replace(/<\/a(?:\s+[^>]*)?>/ig, "\n}}}");

              // Start tags
              richText = richText.replace(/<(?:b|strong)(?:\s+[^>]*)?>/ig, "{\\b\n");
              richText = richText.replace(/<(?:i|em)(?:\s+[^>]*)?>/ig, "{\\i\n");
              richText = richText.replace(/<(?:u|ins)(?:\s+[^>]*)?>/ig, "{\\ul\n");
              richText = richText.replace(/<(?:strike|del)(?:\s+[^>]*)?>/ig, "{\\strike\n");
              richText = richText.replace(/<sup(?:\s+[^>]*)?>/ig, "{\\super\n");
              richText = richText.replace(/<sub(?:\s+[^>]*)?>/ig, "{\\sub\n");
              richText = richText.replace(/<(?:p|div|section|article)(?:\s+[^>]*)?>/ig, "{\\pard\n");

              // End tags
              richText = richText.replace(/<\/(?:p|div|section|article)(?:\s+[^>]*)?>/ig, "\n\\par}\n");
              richText = richText.replace(/<\/(?:b|strong|i|em|u|ins|strike|del|sup|sub)(?:\s+[^>]*)?>/ig, "\n}");

              // Strip any other remaining HTML tags [but leave their contents]
              richText = richText.replace(/<(?:[^>]+)>/g, "");

              // Prefix and suffix the rich text with the necessary syntax
              richText =
                  "{\\rtf1\\ansi\n" + (hasHyperlinks ? "{\\colortbl\n;\n\\red0\\green0\\blue255;\n}\n" : "") + richText +
                  "\n}";
              richText=richText.replace("&nbsp;"," ");
              return richText;
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
            <img src="../Images/loader945435.GIF" style="height:200px;width:200px"/>
        </div>
        
        <div class="row" style="padding-top: 5px; padding-bottom: 5px; background-color: Gray;color:white;font-weight:bold;font-size:30px; text-align: center;top:0;">
            <div class="col-lg-12">
            <asp:HiddenField  runat="server" ID="labnohdn"  Value=""/>
              <div>Approve Test</div> 
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
                         <input type ="text" runat="server"  id="txtname" class = "form-control"  placeholder = "Name" />
                       </div>
                     <div class = "input-group">
                         <span class = "input-group-addon">Head</span>
                         <input type ="text" runat="server"  id="txthead" class = "form-control"  placeholder ="Head" />
                       </div>
                     <div class = "input-group">
                         <span class = "input-group-addon">Approve Date</span>
                         <input type ="text" runat="server"  id="txtappdate" class = "form-control"  placeholder = "Approve Date" />
                       </div>
                     <div class = "input-group">
                         <span class = "input-group-addon">Approved By</span>
                         <input type ="text" runat="server"  id="txtappby" class = "form-control"  placeholder = "Approved By" />
                       </div>
                      <div class = "input-group">
                         <span class = "input-group-addon">Perform By</span>
                         <input type ="text" runat="server"  id="txtperformby" class = "form-control"  placeholder = "Perform By" />
                       </div>
                  <div class = "text-right" style="margin-top:20px; ">
                        <button id="btnsave" type="button" class="btn btn-sm btn-info" onclick="return saveData()" onserverclick="btnOk_Click" ><i class="fa fa-floppy-o" aria-hidden="true"></i>&nbsp;&nbsp;Save</button>
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
                <div class="modal-header">
                  <button type="button" class="close" data-dismiss="modal">&times;</button>
                  <h4 class="modal-title" id="msgtype">Modal Header</h4>
                </div>
                <div class="modal-body">
                  <div id="msg">Message Content</div>
                </div>
                <div class="modal-footer">
                  <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
              </div>
            </div>
          </div>
    
    </form>
</body>
</html>
