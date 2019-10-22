<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintNewBill.aspx.cs" Inherits="PrintNewBill" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body
        {
            width: 330px;
            height: 150px;
        }
        .style1
        {
            white-space: nowrap;
            width: 92%;
        }
        .style3
        {
            width: 629px;
            height: 25px;
        }
        .style4
        {
            width: 25%;
            height: 23px;
        }

        .style5
        {
            height: 30px;
            width: 24%;
        }
        .style6
        {
            height: 30px;
            width: 18%;
        }

        .style7
        {
            height: 30px;
            width: 29%;
        }
    </style>
    <script type="text/javascript" language="javascript">
        function open_win() {

            window.open("Cashier.aspx")
            window.print();
            window.close("PrintNewBill.aspx")
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div style="text-align: right;" id="printOption">
            <a href="javascript:void();" onclick="document.getElementById('printOption').style.visibility = 'hidden';open_win(); return true; ">
                Print</a>
        </div>
        <table align="center" class="style1" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="4" align="center">
                    <asp:Label ID="lblDealer" runat="server" Font-Bold="True" Font-Size="Large" Font-Underline="False"
                        Font-Names="Consolas, Georgia"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4" valign="top" align="center">
                </td>
            </tr>
            <tr>
                <td colspan="4">
                </td>
            </tr>
            <tr>
                <td colspan="4">
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center" class="style3">
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    <asp:Label ID="Label5" runat="server" Text="GATE PASS" Font-Bold="True" Font-Size="Medium"
                        Font-Names="Consolas, Georgia" Font-Underline="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                </td>
            </tr>
            <tr>
                <td colspan="2" style="height: 30px;">
                </td>
                <td colspan="2" style="width: 50%; height: 30px;" align="right">
                    <asp:Label ID="lblDate" runat="server" Font-Names="Consolas, Georgia" Font-Size="Small"></asp:Label>&nbsp;
                </td>
            </tr>
            <tr>
                <td class="style5">
                    <asp:Label ID="Label2" runat="server" Text="Vehicle No:" Font-Names="Consolas, Georgia"
                        Font-Size="Small"></asp:Label>
                </td>
                <td class="style7">
                    <asp:Label ID="lblVehNo" runat="server" class="style4" Font-Names="Consolas, Georgia"
                        Font-Size="Small"></asp:Label>
                </td>
                <td class="style6">
                    <asp:Label ID="Label7" runat="server" Text="Tag No:" class="style4" Font-Names="Consolas, Georgia"
                        Font-Size="Small"></asp:Label>
                </td>
                <td style="width: 25%; height: 30px;">
                    <asp:Label ID="lblTagNo" runat="server" class="style4" Font-Names="Consolas, Georgia"
                        Font-Size="Small"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style5">
                    <asp:Label ID="Label3" runat="server" Text="Gate IN:" Font-Names="Consolas, Georgia"
                        Font-Size="Small"></asp:Label>
                </td>
                <td class="style7">
                    <asp:Label ID="lblGateIn" runat="server" Font-Names="Consolas, Georgia" Font-Size="Small"></asp:Label>
                </td>
                <td class="style6">
                    <asp:Label ID="Label1" runat="server" Text="Gate OUT:" Font-Names="Consolas, Georgia"
                        Font-Size="Small"></asp:Label>
                </td>
                <td style="width: 25%; height: 30px;">
                    <asp:Label ID="lblGateOut" runat="server" Font-Names="Consolas, Georgia" Font-Size="Small"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style5">
                    <asp:Label ID="Label8" runat="server" Text="Bill Amount:" Font-Names="Consolas, Georgia"
                        Font-Size="Small"></asp:Label>
                </td>
                <td class="style7">
                    <asp:Label ID="lblBillAmount" runat="server" Font-Names="Consolas, Georgia" Font-Size="Small"></asp:Label>
                </td>
                <td class="style6">
                </td>
                <td style="width: 25%; height: 30px;">
                </td>
            </tr>
            <tr>
                <td colspan="4">
                </td>
            </tr>
            <tr>
                <td colspan="4">
                </td>
            </tr>
            <tr>
                <td colspan="2" style="height: 25px;">
                </td>
                <td colspan="2" style="width: 50%; height: 25px;">
                </td>
            </tr>
            <tr>
                <td colspan="2" style="height: 25px;">
                </td>
                <td colspan="2" style="width: 50%; height: 25px;" align="center">
                    <asp:Label ID="Label6" runat="server" Text="Signature" Font-Names="Arial"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                </td>
            </tr>
            <tr>
                <td colspan="4">
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center" class="style3">
                    <asp:Label ID="Label4" runat="server" Text="Thank You & Visit Again" Font-Size="Medium"
                        Font-Names="Arial"></asp:Label>
                </td>
            </tr>
        </table>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
    </div>
    </form>
</body>
</html>