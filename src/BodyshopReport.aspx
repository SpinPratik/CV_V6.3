<%@ Page Title="" Language="C#" MasterPageFile="~/AdminModule_New.master" AutoEventWireup="true" CodeFile="BodyshopReport.aspx.cs" Inherits="BodyshopReport" %>
 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>  
  
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
   
    <div class="col-md-12">
      <center>  <table>
            <tr>
             <td align="right">
                                   <p style="font-size: 20pt;text-align:center"> REPORT&nbsp;&nbsp;     </p>
                                </td></tr>
        </table></center>
        <hr />
        <table>
           
            <tr>
                <td><asp:Label ID="lblmsg" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <label>&nbsp;From Date:</label>
                     <asp:TextBox ID="txtFrom" Placeholder="From Date" runat="server" CssClass="form-control"></asp:TextBox>
      <cc1:CalendarExtender ID="Calendar1" PopupButtonID="imgPopup" runat="server" TargetControlID="txtFrom" Format="yyyy-MM-dd"> </cc1:CalendarExtender>  
  
                </td><td>&nbsp;</td>
                <td>
                     <label>To Date:</label>
<asp:TextBox ID="TextTo" runat="server" Placeholder="To Date"  CssClass="form-control"></asp:TextBox>
     <cc1:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopup" runat="server" TargetControlID="TextTo" Format="yyyy-MM-dd"> </cc1:CalendarExtender>  
  
                </td><td>&nbsp;</td>
                <td>
                    <br />
                    <asp:Button ID="btnShow" CssClass="btn btn-success" runat="server" OnClick="btnShow_Click" Text="View Report" />
                </td>
            </tr>
        </table> <hr />
               
   
        </div>
   
    <div class="col-md-12">
    <rsweb:ReportViewer ID="RptViewer" runat="server" Width="100%" >
    </rsweb:ReportViewer>
     </div>
</asp:Content>

