<%@ Page Language="C#" MasterPageFile="~/AdminModule1.master" AutoEventWireup="true"
    CodeFile="ProcessCapacity.aspx.cs" Inherits="ProcessCapacity" Title="ProcessCapacity" %>

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
        <table class="tblStyle">
            <tr>
                <td>
                    <div style="padding: 10px; width: 300px;" class="clmDistriSmall">
                        <asp:Label ID="lblCapacityMsg" runat="server" CssClass="clsValidator" ForeColor="Red"></asp:Label>
                        <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1"
                            ShowHeader="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                            <AlternatingRowStyle BackColor="White" Width="100%" />
                            <Columns>
                                <asp:BoundField DataField="EmployeeType" HeaderText="EmployeeType" SortExpression="EmployeeType" />
                                <asp:TemplateField HeaderText="Capacity" SortExpression="Capacity">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Capacity") %>'></asp:TextBox></EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Capacity") %>'></asp:TextBox></ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle BackColor="WhiteSmoke" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="WhiteSmoke" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ProTRAC_ConnectionString %>"
                            SelectCommand="SELECT tblEmployeeCapacity.* FROM tblEmployeeCapacity"></asp:SqlDataSource>
                        <table>
                            <tr>
                                <td>
                                    <asp:Button CssClass="button" ID="btnSavePCA" runat="server" OnClick="btnSavePCA_Click"
                                        Text="Save" />
                                </td>
                                <td valign="middle">
                                    <asp:ImageButton ID="btnRefreshCapacity" runat="server" Height="24px" ImageUrl="~/images/refresh.png"
                                        OnClick="btnRefreshCapacity_Click" AlternateText="Refresh" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>