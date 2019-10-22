<%@ Page Language="C#" MasterPageFile="~/AdminModule1.master" AutoEventWireup="true"
    CodeFile="GroupMapping.aspx.cs" Inherits="GroupMapping" Title="GroupMapping" %>

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
        .style1
        {
            height: 20px;
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
    <asp:UpdatePanel ID="updShift" runat="server">
        <ContentTemplate>
            <div style="height: 565px; margin-bottom: 30px; vertical-align: middle;">
                <table class="commonFont">
                    <tr>
                        <td colspan="4" class="style1">
                            &nbsp;<asp:Label ID="lbl_msg1" runat="server" CssClass="clsValidator" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="style1">
                            &nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label15" runat="server" Text="Team Lead :"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="dd_TeamLead" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                DataSourceID="SqlDataSource3" DataTextField="EmpName" DataValueField="EmpId"
                                OnSelectedIndexChanged="dd_TeamLead_SelectedIndexChanged" Width="150px" Font-Names="Consolas, Georgia">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;&nbsp;
                        </td>
                        <td>
                            <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:ProTRAC_ConnectionString %>"
                                SelectCommand="SelectTeamLead" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                        </td>
                        <td>
                            &#160;&#160;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;&nbsp;
                        </td>
                        <td bgcolor="Silver">
                            <asp:Label ID="Label16" runat="server" ForeColor="#333333" Text="Employee List"></asp:Label>
                        </td>
                        <td bgcolor="Silver">
                            &nbsp;&nbsp;
                        </td>
                        <td bgcolor="Silver">
                            <asp:Label ID="Label17" runat="server" ForeColor="#333333" Text="Employee Group"></asp:Label>
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
                            <asp:ListBox ID="list_Employee" runat="server" DataTextField="EmpName" DataValueField="EmpId"
                                Height="200px" Width="300px" SelectionMode="Multiple" Font-Names="Consolas, Georgia">
                            </asp:ListBox>
                        </td>
                        <td align="center" width="50">
                            <br />
                            <asp:ImageButton ID="btn_RightMove" runat="server" Height="20px" ImageUrl="~/imgButton/arrow-r.png"
                                OnClick="btn_RightMove_Click" AlternateText="Right Move" /><br />
                            <asp:ImageButton ID="btn_LeftMove" runat="server" Height="20px" ImageUrl="~/imgButton/arrow-l.png"
                                OnClick="btn_LeftMove_Click" AlternateText="Left Move" /><br />
                        </td>
                        <td width="300">
                            <asp:ListBox ID="list_Group" runat="server" Height="200px" Width="300px" SelectionMode="Multiple"
                                Font-Names="Consolas, Georgia"></asp:ListBox>
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
                            &nbsp;&nbsp;
                        </td>
                        <td>
                            &nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &#160;&#160;
                        </td>
                        <td align="left" colspan="3">
                            <asp:Label ID="Label1" ForeColor="#333333" runat="server" Text="* Press Ctrl key to make multiple selections"></asp:Label>
                            &#160;&#160;
                        </td>
                        <td>
                            &#160;&#160;
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>