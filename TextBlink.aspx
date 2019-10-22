<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TextBlink.aspx.cs" Inherits="TextBlink" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" >
      function blink(selector){
         $(selector).fadeOut('slow', function(){
         $(this).fadeIn('slow', function(){
        blink(this);
        });
       });
      }
 
   blink('#msg');
   </script>


</head>
<body>
    <form id="form1" runat="server">
    <div id="msg">
      Spin Technologies
    </div>
    </form>
</body>
</html>
