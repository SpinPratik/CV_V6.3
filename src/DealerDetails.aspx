<%@ Page Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true"
    CodeFile="DealerDetails.aspx.cs" Inherits="DealerDetails" Title="DealerDetails" %>

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
            width: 89px;
        }
        .style2
        {
            width: 10px;
        }
        .style3
        {
            width: 97px;
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
        <table class="commonFont">
            <tr>
                <td colspan="4">
                    <asp:Label ID="lblDealerMsg" runat="server" CssClass="clsValidator" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style3">
                    Dealer Code<asp:Label ID="Label6" runat="server" ForeColor="Red" Text="*"></asp:Label>
                </td>
                <td class="style1">
                    <asp:TextBox ID="txtDCode" runat="server" MaxLength="20" Width="100px" Font-Names="Consolas, Georgia"></asp:TextBox>
                </td>
                <td colspan="2">
                </td>
            </tr>
            <tr>
                <td class="style3">
                    Name<asp:Label ID="Label7" runat="server" ForeColor="Red" Text="*"></asp:Label>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="txtDName" runat="server" MaxLength="100" Width="230px" Font-Names="Consolas, Georgia"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td class="style3" valign="top">
                    Address<asp:Label ID="Label8" runat="server" ForeColor="Red" Text="*"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtAddr" runat="server" MaxLength="100" TextMode="MultiLine" Width="230px"
                        Font-Names="Consolas, Georgia" Font-Size="Small"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style3">
                    Location
                </td>
                <td class="style1">
                    <asp:TextBox ID="txtLocation" runat="server" MaxLength="50" Font-Names="Consolas, Georgia"></asp:TextBox>
                </td>
                <td class="style2">
                    City
                </td>
                <td>
                    <asp:TextBox ID="txtCity" runat="server" MaxLength="50" Font-Names="Consolas, Georgia"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style3">
                    State
                </td>
                <td class="style1">
                    <asp:TextBox ID="txtState" runat="server" MaxLength="50" Font-Names="Consolas, Georgia"></asp:TextBox>
                </td>
                <td class="style2">
                    Zip
                </td>
                <td style="margin-left: 40px">
                    <asp:TextBox ID="txtZip" runat="server" MaxLength="6" Font-Names="Consolas, Georgia"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style3">
                    Division<asp:Label ID="label40" runat="server" ForeColor="Red" Text="*"></asp:Label>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="txtDevision" runat="server" Font-Names="Consolas, Georgia" MaxLength="50"
                        Width="230px"></asp:TextBox>
                </td>
                <td style="margin-left: 40px">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style3">
                    Email<asp:Label ID="Label9" runat="server" ForeColor="Red" Text="*"></asp:Label>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="txtEmail" runat="server" Font-Names="Consolas, Georgia" MaxLength="50"
                        Width="230px"></asp:TextBox>
                </td>
                <td style="margin-left: 40px">
                    &#160;&#160;
                </td>
            </tr>
            <tr>
                <td class="style3">
                </td>
                <td colspan="2">
                    <table>
                        <tr>
                            <td>
                                <asp:Button CssClass="button" ID="btnSaveDealer" runat="server" OnClick="btnSaveDealer_Click"
                                    Text="Save"></asp:Button>
                            </td>
                            <td>
                                <asp:Button CssClass="button" ID="btnClearDealer" runat="server" OnClick="btnClearDealer_Click"
                                    Text="Clear"></asp:Button>
                            </td>
                            <td>
                                <asp:Button CssClass="button" ID="btnRef" runat="server" OnClick="btnRef_Click" Text="Refresh" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <br />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>