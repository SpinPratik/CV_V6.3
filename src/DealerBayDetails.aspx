<%@ Page Language="C#" MasterPageFile="~/AdminModule1.master" AutoEventWireup="true"
    CodeFile="DealerBayDetails.aspx.cs" Inherits="DealerBayDetails" Title="DealerBayDetails" %>

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
            width: 91px;
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
                    <asp:Label ID="lblDealerBayDetails" runat="server" CssClass="clsValidator" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table class="commonFont">
                        <tr>
                            <td class="style1">
                                Speedo Bays
                            </td>
                            <td class="style1">
                                <asp:TextBox ID="txtSpeedoBays" runat="server" MaxLength="20" Width="120px" Font-Names="Consolas, Georgia"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                WA Bays
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtWABays" runat="server" MaxLength="100" Width="120px" Font-Names="Consolas, Georgia"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                Wash Bays
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtWashBays" runat="server" MaxLength="100" Width="120px" Font-Names="Consolas, Georgia"
                                    Font-Size="Small"></asp:TextBox>
                            </td>
                        </tr>
                      
                        <tr>
                            <td class="style1">
                                Mechanical Bays
                            </td>
                            <td class="style1">
                                <asp:TextBox ID="txtTotalBays" runat="server" MaxLength="50" Width="120px" Font-Names="Consolas, Georgia"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                            </td>
                            <td colspan="2" align="left">
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left">
                                            <asp:Button CssClass="button" ID="btnSave" runat="server" OnClick="btnSave_Click1"
                                                Text="Save" Width="60px"></asp:Button>
                                        </td>
                                        <td align="left">
                                            <asp:Button CssClass="button" ID="btnRefreshStdTime" runat="server" OnClick="btnRefreshStdTime_Click"
                                                Text="Refresh" Width="60px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>