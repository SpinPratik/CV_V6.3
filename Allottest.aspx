<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Allottest.aspx.cs" Inherits="Allottest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="timeLine" runat="server" CellPadding="4" ForeColor="#333333" OnRowDataBound="timeLine_RowDataBound">
            <RowStyle BackColor="WhiteSmoke" CssClass="timeLinetd" Wrap="False" />
            <FooterStyle BackColor="Silver" ForeColor="#333333" />
            <PagerStyle BackColor="Silver" ForeColor="#333333" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="WhiteSmoke" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="Silver" Font-Bold="True" ForeColor="#333333" CssClass="timeLinetd"
                Wrap="False" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>