<%@ Page Language="C#" MasterPageFile="~/AdminModule1.master" AutoEventWireup="true"
    CodeFile="VehicleModelMap.aspx.cs" Inherits="VehicleModelMap" Title="VehicleModelMap" %>

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
            width: 104px;
        }
        .style2
        {
            width: 287px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updTime" runat="server">
        <ContentTemplate>
            <asp:Timer ID="NotificationTimer" runat="server" Interval="10000" OnTick="NotificationTimer_Tick">
            </asp:Timer>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div style="padding: 10px; height: 565px; margin-bottom: 30px; vertical-align: middle;">
        <asp:UpdatePanel ID="modelPnl" runat="server">
            <ContentTemplate>
                <table style="width: 600px;" class="commonFont">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblMapMsg" runat="server" CssClass="clsValidator" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" class="style2">
                            <table class="fullStyle">
                                <tr>
                                    <td class="style1" align="left">
                                        Uploaded Model :
                                    </td>
                                    <td style="width: 150px;">
                                        <asp:DropDownList ID="drpCustModel" runat="server" Width="150px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style1" align="left">
                                        Existing Model :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drpExistingModel" runat="server" Width="150px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style1">
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btnMapModel" runat="server" ImageUrl="~/imgButton/arrow-r.png"
                                            Height="20px" OnClick="btnMapModel_Click" AlternateText="Right Move" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table class="fullStyle">
                                <tr>
                                    <td valign="top">
                                        <asp:Panel ID="PanelGridVehMap" runat="server" Height="250px" ScrollBars="Auto" BorderStyle="Groove"
                                            BorderWidth="1px">
                                            <asp:GridView ID="grdVehMap" runat="server" CellPadding="4" ForeColor="#666699" GridLines="None"
                                                Width="100%" AutoGenerateColumns="False" OnRowDeleting="grdVehMap_RowDeleting">
                                                <Columns>
                                                    <asp:BoundField DataField="Existing Model" HeaderText="Existing Model" />
                                                    <asp:BoundField DataField="Uploaded Model" HeaderText="Uploaded Model" />
                                                    <asp:CommandField DeleteImageUrl="~/img/ND2.png" DeleteText="Remove" ShowDeleteButton="True" />
                                                </Columns>
                                                <FooterStyle BackColor="Silver" Font-Bold="True" ForeColor="White" />
                                                <RowStyle BackColor="WhiteSmoke" />
                                                <PagerStyle BackColor="Silver" ForeColor="White" HorizontalAlign="Center" />
                                                <EmptyDataTemplate>
                                                    <table class="fullStyle" style="height: 230px;">
                                                        <tr>
                                                            <td align="center" height="150" style="color: red;">
                                                                SELECT MODELS TO MAP !
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </EmptyDataTemplate>
                                                <SelectedRowStyle BackColor="WhiteSmoke" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle BackColor="Silver" Font-Bold="True" ForeColor="#333333" />
                                                <EditRowStyle BackColor="WhiteSmoke" />
                                                <AlternatingRowStyle BackColor="White" />
                                            </asp:GridView>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button CssClass="button" ID="btmMapSave" runat="server" OnClick="btmMapSave_Click"
                                            Text="Save" />
                                        <asp:Button CssClass="button" ID="btnModelClear" runat="server" OnClick="btnModelClear_Click"
                                            Text="Clear" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btmMapSave" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnModelClear" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnMapModel" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>