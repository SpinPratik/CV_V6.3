<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportsPageForSMDashBoards.aspx.cs"
    Inherits="ReportsPageForSMDashBoards" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reports Page</title>
    <link href="CSS/ProTRAC_Css.css" rel="stylesheet" type="text/css" />
</head>
<body style="background-color: #F5F5F5;">
    <form id="form1" runat="server">
    <table width="100%" align="center">
        <tr>
            <td align="right" valign="top">
                <asp:Button CssClass="button" ID="btn_Back" runat="server" Text="Back" Style="margin-left: 0px"
                    OnClick="btn_Back_Click" />
            </td>
        </tr>
    </table>
    <div style="height: 593px">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table width="100%" align="center">
            <tr>
                <td align="center">
                    <asp:Label ID="lblStstus" runat="server" ForeColor="Maroon" CssClass="clsValidator"></asp:Label>
                </td>
            </tr>
        </table>
         
        <rsweb:ReportViewer ID="RptViewer" runat="server" Width="100%" Font-Names="Verdana"
            Font-Size="8pt" ProcessingMode="Remote" BackColor="White" AsyncRendering="False"
            DocumentMapWidth="100%" Height="29px" SizeToReportContent="True">
        </rsweb:ReportViewer>
    </div>
    </form>
</body>
</html>