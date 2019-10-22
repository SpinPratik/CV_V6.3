<%@ Page Language="C#" AutoEventWireup="true" CodeFile="List.aspx.cs" Inherits="List" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ListView ID="ListView1" runat="server" DataSourceID="SqlDataSource1"></asp:ListView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ProTRAC_ConnectionString %>" SelectCommand="SELECT [id], [partsname] FROM [tbl_identification]"></asp:SqlDataSource>
        <asp:ListView ID="ListView2" runat="server"></asp:ListView>
    </div>
    </form>
</body>
</html>
