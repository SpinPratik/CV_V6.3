<%@ Page Title="" Language="C#" MasterPageFile="~/admin.master" AutoEventWireup="true" CodeFile="reportSample.aspx.cs" Inherits="reportSample" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:TextBox ID="txtFrom" runat="server" Text="08/01/2016"></asp:TextBox>
    <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Show" />
    <br />
    <br />
    <br />
    <rsweb:ReportViewer ID="RptViewer" runat="server" Width="90%">
    </rsweb:ReportViewer>
</asp:Content>

