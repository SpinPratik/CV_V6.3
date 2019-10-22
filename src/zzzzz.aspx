<%@ Page Language="C#" AutoEventWireup="true" CodeFile="zzzzz.aspx.cs" Inherits="zzzzz" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <script type="text/javascript">
          function setdata() {
              document.getElementById("TextBox1").value = "Test Data";
              __doPostBack(document.getElementById('TextBox1').name, '')
          }
      </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <asp:TextBox ID="TextBox1" runat="server" AutoPostBack="True"
            ontextchanged="TextBox1_TextChanged" onkeypress="javascript:return false;"></asp:TextBox>
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        <asp:Button ID="Button1"
            runat="server" Text="Button" onclick="Button1_Click" />

    </div>
    </form>
</body>
</html>
