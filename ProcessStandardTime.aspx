<%@ Page Language="C#" MasterPageFile="~/AdminModule_New.master" AutoEventWireup="true"
    CodeFile="ProcessStandardTime.aspx.cs" Inherits="ProcessStandardTime1" Title="ProcessStandardTime" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="CSS/Style.css" rel="stylesheet" />
    <style type="text/css">
        .thisstyle
        {
            width: 40%;
        }
        .LeftGridPad
        {
            padding-left: 40px;
        }
        .mydatagrid a, .mydatagrid span{
            width:unset !important;
        }
        .mydatagrid span{
            background-color:unset !important;
            color: #000;
            border: unset !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="updTime" runat="server">
        <ContentTemplate>
            <asp:Timer ID="NotificationTimer" runat="server" Interval="10000">
            </asp:Timer>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div style="padding: 10px; height: 565px; margin-bottom: 30px; vertical-align: middle;">
        <table class="tblStyle">
            <tr>
                <td>
                    <div style="width: 300px;" class="clmDistriSmall">
                        <asp:Label ID="lblStandardTimeMsg" runat="server" CssClass="clsValidator" ForeColor="Red"></asp:Label>
                        <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" DataSourceID="DS_StandardTime"
                            ShowHeader="False" CssClass="mydatagrid" DataKeyNames="ProcessId" CellPadding="4" ForeColor="#333333"
                            GridLines="None" OnRowDataBound="GridView5_RowDataBound" PageSize="12">
                            <AlternatingRowStyle BackColor="White" Width="100%" />
                            <Columns>
                                <asp:BoundField DataField="ProcessId" HeaderText="ProcessId" InsertVisible="False"
                                    ReadOnly="True" SortExpression="ProcessId" />
                                <asp:BoundField DataField="ProcessName" HeaderText="ProcessName" SortExpression="ProcessName" />
                                <asp:TemplateField HeaderText="ProcessTime" SortExpression="ProcessTime">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ProcessTime") %>' MaxLength="4"></asp:TextBox></EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("ProcessTime", "{0:D}") %>'
                                            Width="100px" MaxLength="4"></asp:TextBox>
                                        <asp:Label ID="Label2" runat="server" Text="min(s)"></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle BackColor="WhiteSmoke" />
                            <FooterStyle BackColor="Silver" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="Silver" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="WhiteSmoke" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="WhiteSmoke" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        </asp:GridView>
                        <asp:SqlDataSource ID="DS_StandardTime" runat="server" ConnectionString="<%$ ConnectionStrings:ProTRAC_ConnectionString %>"
                            SelectCommand="SELECT ProcessId, ProcessName, ProcessTime FROM tblProcessList WHERE (ProcessName NOT IN ('GATE')) ORDER BY ProcessDefaultOrder">
                        </asp:SqlDataSource>
                        <table>
                            <tr>
                                <td>
                                    <asp:Button CssClass="button" ID="btnSaveStandardTime" runat="server" ImageUrl="~/imgButton/SAVE.jpg"
                                        Text="Save" OnClick="btnSaveStandardTime_Click" />
                                </td>
                                <td valign="middle">
                                    <asp:ImageButton ID="btnRefreshStdTime" runat="server" Height="25px" ImageUrl="~/images/refresh.png"
                                        OnClick="btnRefreshStdTime_Click" AlternateText="Refresh" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>