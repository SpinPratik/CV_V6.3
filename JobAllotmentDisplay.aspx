<%@ Page Title="" Language="C#" MasterPageFile="~/AdminModule.master" AutoEventWireup="true"
    CodeFile="JobAllotmentDisplay.aspx.cs" Inherits="JobAllotmentDisplay" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   <%-- <link href="CSS/ProTRAC_Css.css" rel="stylesheet" type="text/css" />
    <link href="CSS/ProTRAC_CssJCR.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="js/Tooltip.js" type="text/javascript"></script>--%>

<%--     <link href="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484286797/css/ProTRAC_Css.css" rel="stylesheet" type="text/css" />--%>
    <link href="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484286797/css/ProTRAC_CssJCR.css" rel="stylesheet" type="text/css" />
   <%-- <script src="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484286893/js/jquery-1.4.2.min.js" type="text/javascript"></script>--%>
    <script src="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484286893/js/Tooltip.js" type="text/javascript"></script>


    <style type="text/css">
        .header-row td{
            font-size:12px;
            text-transform:uppercase;
        }
        .modalBackground
        {
            background-color: Gray;
        }
        /*.style6
        {
            width: 372px;
        }
        .style8
        {
            width: 63px;
        }
        .style10
        {
            width: 129px;
        }
        .style12
        {
            width: 187px;
        }
        .style14
        {
            width: 128px;
        }
        .style15
        {
            width: 131px;
        }
        .style16
        {
            width: 48px;
        }
        .style17
        {
            width: 73px;
            white-space: nowrap;
        }
        .style19
        {
            width: 30px;
        }
        .style20
        {
            width: 132px;
        }
        .style21
        {
            width: 254px;
        }
        .style22
        {
            width: 100%;
        }
        .style23
        {
            height: 23px;
        }*/
        .form-control {
    display: block;
    width: 100%;
    height: 34px; 
    padding: 6px 12px;
    font-size: 14px;
    line-height: 1.42857143;
    color: #555;
    background-color: #fff;
    background-image: none;
    border: 1px solid #ccc;
    border-radius: 4px;
    -webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
    box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
    -webkit-transition: border-color ease-in-out .15s,-webkit-box-shadow ease-in-out .15s;
    -o-transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
    transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table style="width: 100%">
        <tr style="width: 100%">
            <td valign="top" style="background-color: #e0ecff;padding:10px;">
                <table class="header-row">
                    <tr>
                         
                         <td class="style8">
                              <asp:Label ID="Label1" runat="server" Text="DATE" ForeColor="#333333"></asp:Label>
                            <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" Width="120px" AutoPostBack="True" MaxLength="10" ToolTip="In Date"
                               ></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" Enabled="True"
                                FilterMode="ValidChars" TargetControlID="txtDate" ValidChars="0123456789/">
                            </cc1:FilteredTextBoxExtender>
                            <cc1:CalendarExtender ID="CalendarExtender9" runat="server" Enabled="True" TargetControlID="txtDate">
                            </cc1:CalendarExtender>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="RadioTypes" runat="server" RepeatDirection="Vertical"
                                 OnSelectedIndexChanged="RadioType_SelectedIndexChanged"
                                AutoPostBack="True"  ForeColor="#3333333">
                                <asp:ListItem Value="0" Selected="True">TEAM LEAD</asp:ListItem>
                                <asp:ListItem Value="1">BAY</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                               
                                    <td class="style14">
                                        <asp:Label ID="Label3" runat="server" Text="SHIFT" ForeColor="#333333" Visible="false"></asp:Label>
                                        <asp:DropDownList ID="ddlShift" runat="server" CssClass="form-control" AppendDataBoundItems="True" AutoPostBack="True"
                                            DataSourceID="DS_Shift" DataTextField="Shift" DataValueField="ShiftID" Width="120px"
                                            ToolTip="Shift" Visible="false">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="DS_Shift" runat="server" 
                                            SelectCommand="SELECT ShiftID, Shift FROM tblShift ORDER BY Shift"></asp:SqlDataSource>
                                    </td>
                                    
                                    <td class="style20">
                                          <asp:Label ID="Label4" runat="server" Text="TEAM LEAD" ForeColor="#333333" ></asp:Label>
                                <%--AppendDataBoundItems="True" DataSourceID="DS_TeamLead" DataTextField="EmpName" DataValueField="EmpId"--%>
                                        <asp:DropDownList ID="ddlTeam" runat="server" CssClass="form-control"  AutoPostBack="True"  AppendDataBoundItems="True"
                                              ToolTip="Team-Lead" >
                                            
                                        </asp:DropDownList>
                                   
                                    </td>
                                   
                                    <td class="style20">
                                        <asp:Label ID="Label2" runat="server" Text="FLOOR" ForeColor="#333333" Visible="false"></asp:Label>
                                        <asp:DropDownList ID="drpFloorName" CssClass="form-control" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            DataSourceID="SqlDataSource1" DataTextField="FloorName" DataValueField="FloorName"
                                             OnSelectedIndexChanged="drpFloorName_SelectedIndexChanged" Width="120PX" Visible="false">
                                            <asp:ListItem Value="-1">ALL</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                                            SelectCommand="GetFloorList" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                    </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
    <div style="width: 1400px; overflow: auto;">
        <table cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td>
                    <asp:Panel runat="server" ID="Panel1" CssClass="JobAllotmentGridPanelHeader">
                        <asp:GridView ID="timeLineHeader" runat="server" CellPadding="4" ForeColor="#333333"
                            OnRowDataBound="timeLine_RowDataBound" Width="100%">
                            <RowStyle BackColor="#EFF3FB" CssClass="timeLinetd" Wrap="False" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="Silver" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="Silver" Font-Bold="True" ForeColor="#333333" CssClass="timeLinetd"
                                Wrap="False" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                    </asp:Panel>
                </td>
                <td style="width: 0px; white-space: nowrap;">
                    &nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2" style="width: 100%;">
                    <asp:Panel runat="server" ID="timeLinePnl" Height="420px" class="tblStyle" ScrollBars="Auto"
                        CssClass="commonFont">
                        <asp:GridView ID="timeLine" runat="server" CellPadding="4" ForeColor="#333333" OnRowDataBound="timeLine_RowDataBound" Width="100%">
                            <RowStyle BackColor="WhiteSmoke" CssClass="timeLinetd" Wrap="False" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="Silver" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="Silver" Font-Bold="True" ForeColor="#333333" CssClass="timeLinetd"
                                Wrap="False" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>