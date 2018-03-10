<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true"  CodeBehind="frmLabTests.aspx.cs" Inherits="WebLab.Forms.frmLabTests" %>
<%@ Register src="../UserControls/ucHtmlBox.ascx" TagName="htb" TagPrefix="htb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lab Tests</title>
    <script src="../Scripts/rtf_scriptfiles/tiny_mce.js"></script>
    <script src="../Scripts/rtf_scriptfiles/tiny_mce_src.js"></script>
    <script src="../Scripts/rtf_scriptfiles/tiny_mce.js"></script>
    <link href="../CSS/bootstrap.min337.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery.min320.js" type="text/javascript"></script>

    <script src="../js/bootstrap.min337.js" type="text/javascript"></script>


    <script type="text/javascript">
        function closePrevbox()
        {
            var prebox = document.getElementById("prevalbox");
            prebox.style.display = "none";
        }
        function openPrevbox() {
            var prebox = document.getElementById("prevalbox");
            prebox.style.display = "block";
        }
        function checkTestNormalRamge(id,val,rownum)
        {
            debugger;
            var r = rownum + 1;
           /// alert(r);
           // var rownum = row + 1;
           // alert("Checking normal value" + id + " " + val + " " + row);
            var maxcolumnValue = document.getElementById("Table1").rows[r].cells[4].innerText;
            var mincolumnValue = document.getElementById("Table1").rows[r].cells[3].innerText;
            var arrminval;
            arrminval = mincolumnValue.split('-');
            if(arrminval.length>1)
            {
                mincolumnValue=Number(-arrminval[1])
            }
           // alert("max : " + maxcolumnValue + "min : " + mincolumnValue);
            var ctrlid = document.getElementById(id);
          
            if (Number(val) <= Number(maxcolumnValue) && Number(val) >= Number(mincolumnValue)) {
             
                ctrlid.style.color = "green";
            }
            else if (Number(val) <= Number(maxcolumnValue) && mincolumnValue == "<")
            {
                ctrlid.style.color = "green";
            }
            else if (Number(val) >= Number(mincolumnValue) && maxcolumnValue == ">") {
                ctrlid.style.color = "green";
            }
            else {
                
                if (maxcolumnValue=="" || mincolumnValue=="")
                {
                    ctrlid.style.color = "black";
                }
                else {
                    ctrlid.style.color = "red";
                }
            }

        }

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
                debugger;
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
                    debugger;
                    console.log(item["comment"]);
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
               // console.log(JSON.stringify(JsonObj));
                $.ajax({
                    type: "POST",
                    url: "../Forms/frmLabTests.aspx/saveData",
                    contentType: "application/json; charset=utf-8",
                    data: "{lt:" + JSON.stringify(JsonObj) + "}",
                    dataType: "json",
                    success: OnSuccess,
                    failure: function (response) {
                        alert(response);
                        var res = response.d;
                        
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
                var pval = response.d;
                if (pval.length > 0) {
                    openPrevbox();
                    var boxcontent = document.getElementById("bodycontent");
                    while (boxcontent.hasChildNodes()) {
                        boxcontent.removeChild(boxcontent.lastChild);
                    }
                    for (var i = 0; i < pval.length; i++) {
                        //<div id="bodycontent">
                        //<div style=>12</div>
                        var divele = document.createElement("div");
                        divele.setAttribute("style", "cursor:pointer;width:100%;border-bottom:2px brown solid;");
                        divele.innerText = pval[i]["testval"];
                        boxcontent.appendChild(divele);
                    }
                    console.log(response.d);
                }
            }
    
            // tinyMCE.init({
            //    // General options
            //    mode: "textareas",
            //    theme: "advanced",
            //    plugins: "pagebreak,style,layer,table,save,advhr,advimage,advlink,emotions,iespell,inlinepopups",
            //    font_formats: 'Arial=arial,helvetica,sans-serif;Courier New=courier new,courier,monospace;AkrutiKndPadmini=Akpdmi-n'

           
            //});

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
                    var rownum=rowcount+1;
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
            function setText(val1) {

                $("iframe").each(function () {

                    $(this).contents().find("#tinymce").html(val1);

                });


            }
        
        
    </script>
    <%--<script type="text/javascript" src="http://archive.tinymce.com//js/tinymce_3_x/jscripts/tiny_mce/tiny_mce.js"></script>--%>
    <script type="text/javascript">
tinyMCE.init({
        // General options
        mode : "textareas",
        theme : "advanced",
        plugins : "autolink,lists,spellchecker,pagebreak,style,layer,table,save,advhr,advimage,advlink,emotions,iespell,inlinepopups,insertdatetime,preview,media,searchreplace,print,contextmenu,paste,directionality,fullscreen,noneditable,visualchars,nonbreaking,xhtmlxtras,template",

        // Theme options
        theme_advanced_buttons1 : "save,newdocument,|,bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,|,styleselect,formatselect,fontselect,fontsizeselect",
        theme_advanced_buttons2 : "cut,copy,paste,pastetext,pasteword,|,search,replace,|,bullist,numlist,|,outdent,indent,blockquote,|,undo,redo,|,link,unlink,anchor,image,cleanup,help,code,|,insertdate,inserttime,preview,|,forecolor,backcolor",
        theme_advanced_buttons3 : "tablecontrols,|,hr,removeformat,visualaid,|,sub,sup,|,charmap,emotions,iespell,media,advhr,|,print,|,ltr,rtl,|,fullscreen",
        theme_advanced_buttons4 : "insertlayer,moveforward,movebackward,absolute,|,styleprops,spellchecker,|,cite,abbr,acronym,del,ins,attribs,|,visualchars,nonbreaking,template,blockquote,pagebreak,|,insertfile,insertimage",
        theme_advanced_toolbar_location : "top",
        theme_advanced_toolbar_align : "left",
        theme_advanced_statusbar_location : "bottom",
        theme_advanced_resizing : true,

        // Skin options
        skin : "o2k7",
        skin_variant : "silver",

        // Example content CSS (should be your site CSS)
        content_css : "css/example.css",

        // Drop lists for link/image/media/template dialogs
        template_external_list_url : "js/template_list.js",
        external_link_list_url : "js/link_list.js",
        external_image_list_url : "js/image_list.js",
        media_external_list_url : "js/media_list.js",

        // Replace values for the template plugin
        template_replace_values : {
                username : "Some User",
                staffid : "991234"
        }
});

</script>


    <style>
        .boxshadow
        {
        	-webkit-box-shadow: -4px 4px 5px -1px rgba(0,0,0,0.75);
            -moz-box-shadow: -4px 4px 5px -1px rgba(0,0,0,0.75);
             box-shadow: -4px 4px 5px -1px rgba(0,0,0,0.75);
        	
        	}
          .auto-style1 {
            width: 59px;
        }
        
        .table-width
        {
            width: 100%;
        }
        table
        {
            margin: 0;
            background-color: white;
        }
       th
        {
            background-color: Gray;
            color: White;
            
        }
        td
        {
            padding: 5px;
        }
        caption
        {
            text-align: center;
            font-size: 20px;
            font-weight: bold;
        }
        input[type=text]
        {
            width: 100%;
        }
        a
        {
            background: #31a5d3;
            height: 40px;
            color: #fff;
            padding: 8px 15px;
            text-transform: uppercase;
            font-weight: 200;
            letter-spacing: 2px;
            appearance: none;
            border: 2px white solid;
            -webkit-appearance: none;
            -moz-appearance: none;
            -o-appearance: none;
            -ms-appearance: none;
            -webkit-transition: all ease .3s;
            -moz-transition: all ease .3s;
            -o-transition: all ease .3s;
            -ms-transition: all ease .3s;
            transition: all ease .3s;
            border-radius: 0px;
        }
        a:hover
        {
            background: #2482D4;
            color: #fff;
            padding: 8px 15px;
            text-decoration: none;
            text-transform: uppercase;
            font-weight: 200;
            border: 2px white solid;
            letter-spacing: 2px;
            appearance: none;
            cursor: pointer;
            -webkit-appearance: none;
            -moz-appearance: none;
            -o-appearance: none;
            -ms-appearance: none;
            -webkit-transition: all ease .3s;
            -moz-transition: all ease .3s;
            -o-transition: all ease .3s;
            -ms-transition: all ease .3s;
            transition: all ease .3s;
            border-radius: 0px;
        }
        #suggestionbox
        {
        	background-color:Navy;
        	position:fixed;
        	height:300px;
        	width:150px;
        	right:0;
        	top: 20%;
        	color: White;
        	
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
      	#contmenuitem
      	{
      		width:100%;border:0;height:20px;font-size:12px;font-weight:normal;background-color:#18568D;padding:2px;float:left;margin-bottom:2px;border-radius:20px;
      		
      		}
        #contmenuitem:hover
      	{
      		width:100%;border:0;height:20px;font-size:12px;font-weight:normal;background-color:#737373;padding:2px;float:left;cursor:pointer;margin-bottom:2px;border-radius:20px;
         }
        #prevalbox
        {
            background-color : black;
            color: white;
            position:fixed;
            width : 150px;
            right:0;
            top:30%;
            display:none;
        }
        #prevalbox>#head{
            padding: 5px;
            background-color:brown;
        }
        #prevalbox>#bodycontent{
           height :150px;
           overflow-y :auto;
        }

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

    <div class="container-fluid">  
           <div class="loader text-center"  id="loadimg" style="display:none">
            <div style="font-size:20px;font-weight:bold;color:Blue">Saving! Please Wait........</div>
            <img src="../Images/loader945435.GIF" style="height:200px;width:200px"/>
        </div>    
        <div class="row" style="padding-top: 5px; padding-bottom: 5px; background-color: Gray;text-align: right;top:0;">
            <div class="col-lg-12">
            <asp:HiddenField  runat="server" ID="labnohdn"  Value=""/>
            <button id="btnsave" type="button" class="btn btn-sm btn-info" onclick="return saveData()" onserverclick="btnOk_Click" ><i class="fa fa-floppy-o" aria-hidden="true"></i>&nbsp;&nbsp;Save</button>
            <button id="Button1"  class="btn btn-sm btn-danger" onclick="CloseWindow()"><i class="fa fa-times" aria-hidden="true" ></i>&nbsp;&nbsp;Close</button>   
              <%--<asp:Button ID="btnOk" runat="server" Text="Button" onclick="btnOk_Click" />--%> </div>
        </div>
        
        <asp:Table ID="Table1" runat="server" Caption="Lab Tests" CssClass="table-responsive table-hover table-width fa-border">
            <asp:TableHeaderRow>
                <asp:TableHeaderCell>Test Name</asp:TableHeaderCell>
                <asp:TableHeaderCell>Result</asp:TableHeaderCell>
                <asp:TableHeaderCell ColumnSpan="3">Normal Range</asp:TableHeaderCell>
            </asp:TableHeaderRow>
        </asp:Table>  
        <asp:Panel runat="server" ID="uspanel">
        </asp:Panel>
       <div id="prevalbox">
           <div id="head">
               Prevous Values
               <i class="fa fa-remove" style="float:right;cursor:pointer" onclick="closePrevbox()"></i>
           </div>
           <div id="bodycontent">
              
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
     </div>
    </form>
</body>
</html>
