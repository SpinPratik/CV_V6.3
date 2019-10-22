<%@ Page Title="" Language="C#" MasterPageFile="~/AdminModule1.master" AutoEventWireup="true"
    CodeFile="SyncMaster.aspx.cs" Inherits="SyncMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            vertical-align: top;
            height: 40px;
        }
        .PopupWindow
        {
            position: fixed;
            top: 0px;
            bottom: 0px;
            left: 0px;
            right: 0px;
            opacity: 0.9;
            background-color: #333333;
        }
        .style3
        {
            vertical-align: top;
            height: 40px;
            width: 105px;
        }
        .style4
        {
            width: 105px; /*font-size:medium;         	/*background-color:#2461BF;         	color:White;*/
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="height: 565px; margin-bottom: 30px; vertical-align: middle;">
        <table style="height: 100%; width: 100%">
            <tr>
                <td colspan="3" />
            </tr>
            <tr>
                <td />
                <td align="center" valign="top">
                    <table style="width: 309px; text-align: center;">
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:ImageButton ID="btn_JobCode" runat="server" Height="69px" ImageUrl="~/img/SyncMaster.png"
                                    AlternateText="Job Code" OnClick="btn_JobCode_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="btn_RateList" runat="server" Height="69px" ImageUrl="~/img/SyncMaster.png"
                                    AlternateText="Rate List" OnClick="btn_RateList_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="btn_PriceList" runat="server" Height="69px" ImageUrl="~/img/SyncMaster.png"
                                    AlternateText="Price List" OnClick="btn_PriceList_Click" />
                            </td>
                            <td rowspan="4">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                &nbsp;
                            </td>
                            <td align="center" class="style1">
                                <asp:HyperLink ID="HyperLink6" runat="server" Width="107px" ForeColor="#333333">JobCode</asp:HyperLink>
                            </td>
                            <td class="style1">
                                <asp:HyperLink ID="HyperLink9" runat="server" Font-Bold="True" Font-Underline="False"
                                    ForeColor="#333333" Width="107px">Rate List</asp:HyperLink>
                            </td>
                            <td align="center" class="style1" valign="top">
                                <asp:HyperLink ID="HyperLink13" runat="server" Font-Bold="True" Font-Underline="False"
                                    ForeColor="#333333" Width="107px">Price List</asp:HyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                &nbsp;
                            </td>
                            <td class="style1">
                                &nbsp;
                            </td>
                            <td class="style1">
                                &nbsp;
                            </td>
                            <td class="style1">
                                &nbsp;
                            </td>
                            <td class="style1">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="style1" colspan="5">
                                <br />
                            </td>
                        </tr>
                    </table>
                </td>
                <td />
            </tr>
            <tr>
                <td colspan="3" />
            </tr>
        </table>
    </div>
    <asp:Panel ID="PopupPanel" runat="server" Visible="False" CssClass="PopupWindow">
        <table style="width: 100%; height: 100%" border="0" cellpadding="0" cellspacing="0">
            <tr style="height: 33.33%">
                <td style="width: 33.33%">
                </td>
                <td style="width: 33.33%">
                </td>
                <td style="width: 33.33%">
                </td>
            </tr>
            <tr style="height: 33.33%">
                <td style="width: 33.33%">
                </td>
                <td style="width: 33.33%">
                    <div id="pnl_updateStatus" runat="server" style="width: 550px; height: 150px; background-color: #F5F5F5;
                        text-align: center; vertical-align: middle; font-family: consolas; font-size: large;
                        color: #006600; font-weight: bold;">
                        <table style="border-style: 1px; border-width: 1px; border-color: #333333; width: 100%;
                            height: 100%;" border="1" cellpadding="0" cellspacing="0">
                            <tr style="height: 25px; background-color: #0040FF;">
                                <td style="color: White; font-family: Consolas">
                                    Sync Master Details
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Label ID="lbl_Status" runat="server" Text=""></asp:Label>
                                    <br />
                                    <br />
                                    <asp:Button ID="btn_Close" runat="server" CssClass="button" OnClick="btn_Close_Click"
                                        Text="Close" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="divJobCode" runat="server" style="width: 550px; height: 150px; background-color: #F5F5F5;
                        text-align: center; vertical-align: middle; font-family: consolas; font-size: large;
                        color: #006600; font-weight: bold;">
                        <table style="border-style: 1px; border-width: 1px; border-color: #333333; width: 100%;
                            height: 100%;" border="1" cellpadding="0" cellspacing="0">
                            <tr style="height: 25px; background-color: #0040FF;">
                                <td style="color: White; font-family: Consolas">
                                    Job Code Master Details
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td colspan="7">
                                                <asp:Label ID="lblErrorJobCode" runat="server" ForeColor="#003300" 
                                                    Font-Bold="False"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label4" runat="server" Text="From Date" ForeColor="Black"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtFromDateJobCode" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" AlternateText="Select"
                                                    ImageUrl="images/calendar.gif" Width="17px" Height="18px" />
                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtFromDateJobCode"
                                                    Format="dd-MMM-yyyy" PopupButtonID="ImageButton2">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label5" runat="server" Text="To Date" ForeColor="Black"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtToDateJobCode" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="False" AlternateText="Select"
                                                    ImageUrl="images/calendar.gif" Width="17px" Height="18px" />
                                                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtToDateJobCode"
                                                    Format="dd-MMM-yyyy" PopupButtonID="ImageButton3">
                                                </cc1:CalendarExtender>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <asp:Button ID="btnFetchJobCode" runat="server" CssClass="button" OnClick="btnFetchJobCode_Click"
                                        Text="Fetch" />
                                    <asp:Button ID="btnCloseJobCard" runat="server" CssClass="button" OnClick="btnClose_Click"
                                        Text="Close" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="divAthuntication" runat="server" style="width: 450px; height: 150px; background-color: #F5F5F5;
                        text-align: center; vertical-align: middle; font-family: consolas; font-size: large;
                        color: #006600; font-weight: bold;">
                        <table style="border-style: 1px; border-width: 1px; border-color: #333333; width: 100%;
                            height: 100%;" border="1" cellpadding="0" cellspacing="0">
                            <tr style="height: 25px; background-color: #0040FF;">
                                <td style="color: White; font-family: Consolas">
                                    Login Detail
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Label ID="lblError_Login" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 25%;">
                                            </td>
                                            <td align="left" style="width: 25%;">
                                                <asp:Label ID="Label3" runat="server" Text="User Name:" ForeColor="Black" Font-Bold="false"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtUserName" runat="server" Width="169px"></asp:TextBox>
                                            </td>
                                            <td style="width: 25%;">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 25%;">
                                            </td>
                                            <td align="left" style="width: 25%;">
                                                <asp:Label ID="Label18" runat="server" Text="Password:" ForeColor="Black" Font-Bold="false"></asp:Label>
                                            </td>
                                            <td align="left" style="width: 25%;">
                                                <asp:TextBox ID="txtPassword" runat="server" Width="168px" TextMode="Password"></asp:TextBox>
                                            </td>
                                            <td style="width: 25%;">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 25%;">
                                            </td>
                                            <td align="left">
                                            </td>
                                            <td align="left">
                                                <asp:Button ID="btn_Login" runat="server" CssClass="button" Text="Login" OnClick="btn_Login_Click" />
                                                <asp:Button ID="btn_Close_Login" runat="server" CssClass="button" Text="Close" OnClick="btn_Close_Login_Click" />
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
                <td style="width: 33.33%">
                </td>
            </tr>
            <tr style="height: 33.33%">
                <td style="width: 33.33%">
                </td>
                <td style="width: 33.33%">
                </td>
                <td style="width: 33.33%">
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>