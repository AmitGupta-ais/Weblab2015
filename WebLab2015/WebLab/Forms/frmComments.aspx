<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmComments.aspx.cs" Inherits="WebLab.frmLabTests" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>LabTests</title>
    <script src="../Scripts/rtf_scriptfiles/tiny_mce.js" type="text/javascript"></script>
    <style type="text/css">
        .auto-style1 {
            width: 59px;
        }
    </style>
    <script type="text/javascript">
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
        
        
    </script>
</head>
<body> 
<form runat="server"> 
   <asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine" Height="100%" Width="100%"></asp:TextBox>
 </form>
</body>
</html>
