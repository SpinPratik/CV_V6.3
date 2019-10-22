<%@ Page Language="C#" MasterPageFile="~/AdminModule1.master" AutoEventWireup="true"
    CodeFile="ShiftMapping.aspx.cs" Inherits="ShiftMapping" Title="ShiftMapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .thisstyle
        {
            width: 40%;
        }
        .LeftGridPad
        {
            padding-left: 40px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updTime" runat="server">
        <ContentTemplate>
            <asp:Timer ID="NotificationTimer" runat="server" Interval="10000">
            </asp:Timer>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div style="height: 565px; margin-bottom: 30px; vertical-align: middle;">
        <table class="commonFont">
            <tr>
                <td colspan="4">
                    <asp:Label ID="lbl_msg2" runat="server" CssClass="clsValidator" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    &#160;&#160;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label18" runat="server" Text="Shift :"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="dd_Shifts" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                        DataSourceID="SqlDataSource4" DataTextField="Shift" DataValueField="ShiftID"
                        OnSelectedIndexChanged="dd_Shifts_SelectedIndexChanged" Width="150px" Font-Names="Consolas, Georgia">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    &#160;&#160;
                </td>
                <td>
                    <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:ProTRAC_ConnectionString %>"
                        SelectCommand="SelectShift" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                </td>
                <td>
                    &#160;&#160;
                </td>
            </tr>
            <tr>
                <td>
                    &#160;&#160;
                </td>
                <td bgcolor="Silver">
                    <asp:Label ID="Label19" runat="server" ForeColor="#333333" Text="Employee List"></asp:Label>
                </td>
                <td bgcolor="Silver">
                    &#160;&#160;
                </td>
                <td bgcolor="Silver">
                    <asp:Label ID="Label20" runat="server" ForeColor="#333333" Text="Shift Employee"></asp:Label>
                </td>
                <td>
                    &#160;&#160;
                </td>
            </tr>
            <tr>
                <td height="200" width="100">
                    &#160;&#160;
                </td>
                <td width="300">
                    <asp:ListBox ID="list_Employee0" runat="server" DataTextField="EmpName" DataValueField="EmpId"
                        Height="200px" Width="300px" SelectionMode="Multiple" Font-Names="Consolas, Georgia">
                    </asp:ListBox>
                </td>
                <td align="center" width="50">
                    <br />
                    <asp:ImageButton ID="btn_RightMove0" runat="server" Height="20px" ImageUrl="~/imgButton/arrow-r.png"
                        OnClick="btn_RightMove0_Click" AlternateText="Right Move" /><br />
                    <asp:ImageButton ID="btn_LeftMove0" runat="server" Height="20px" ImageUrl="~/imgButton/arrow-l.png"
                        OnClick="btn_LeftMove0_Click" AlternateText="Left Move" /><br />
                </td>
                <td width="300">
                    <asp:ListBox ID="list_ShiftEmployee" runat="server" Height="200px" Width="300px"
                        SelectionMode="Multiple" Font-Names="Consolas, Georgia"></asp:ListBox>
                </td>
                <td>
                    &#160;&#160;
                </td>
            </tr>
            <tr>
                <td>
                    &#160;&#160;
                </td>
                <td align="center" bgcolor="Silver" colspan="3">
                    &#160;&#160;
                </td>
                <td>
                    &#160;&#160;
                </td>
            </tr>
            <tr>
                <td>
                    &#160;&#160;
                </td>
                <td align="left" colspan="3">
                    <asp:Label ID="Label1" ForeColor="#333333" runat="server" Text="* Press Ctrl key to make multiple selections"></asp:Label>
                </td>
                <td>
                    &#160;&#160;
                </td>
            </tr>
        </table>
    </div>
</asp:Content>