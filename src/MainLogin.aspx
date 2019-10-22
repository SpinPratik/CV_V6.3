<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MainLogin.aspx.cs" Inherits="MainLogin" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="txt_delaercode" runat="server"></asp:TextBox>
        <asp:Button ID="btn_submit" OnClick="btn_submit_Click" runat="server" Text="Button" />
    </div>
    </form>
</body>
</html>
