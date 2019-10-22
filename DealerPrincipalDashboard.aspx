<%@ Page Title="" Language="C#" MasterPageFile="~/AdminModule.master" AutoEventWireup="true"
    CodeFile="DealerPrincipalDashboard.aspx.cs" Inherits="DealerPrincipalDashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            height: 100%;
            width: 100%;
            top: 0;
            left: 0;
            position: fixed;
            background-attachment: scroll;
            background-repeat: no-repeat;
            background-size: 100%;
        }
        .modalBackground
        {
            opacity: 0.75;
            background-color: #000;
            height: 400px;
            y-overflow: scroll;
        }
        .background
        {
            position: relative;
            top: 0px;
            left: -6px;
            height: 700px;
            width: 102%;
        }
        .style2
        {
        }
        .style3
        {
            border-right-color: "#ffffff";
            width: 75px;
        }
        .style4
        {
            color: White;
            height: 36px;
        }
        .style5
        {
            height: 28px;
        }
        .scrollstyle
        {
            scrollbar-base-color: #369;
        }
        .style9
        {
            height: 33px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel runat="server" ID="DealerUpdatePanel">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td colspan="3" valign="top">
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="DealerUpdatePanel">
                            <ProgressTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <img alt="" src="img/refresh.gif" />
                                        </td>
                                        <td style="font-weight: bold; color: #333333; font-size: large">
                                            &nbsp;&nbsp;&nbsp;Loading Please Wait ...
                                        </td>
                                    </tr>
                                </table>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
                <table border="0" cellpadding="0" cellspacing="0" style="left: 0; width: 100%;">
                    <tr>
                        <td class="style3" valign="top" align="left">
                            <div style="white-space: nowrap;">
                                <table cellspacing="0" rules="all" border="1" id="grdCaption" style="border-color: #660033;
                                    height: 420px; border-collapse: collapse;">
                                    <tr style="color: #333333; background-color: Silver; font-weight: normal;">
                                        <th scope="col" colspan="2">
                                            Date&nbsp;&nbsp;&nbsp;<br />
                                            &nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lbMonthYr" runat="server" Text=""></asp:Label>
                                            &nbsp;&nbsp;&nbsp;
                                        </th>
                                    </tr>
                                    <tr align="left" valign="middle">
                                        <td colspan="2">
                                            Total Vehicles In Flow(Nos)
                                        </td>
                                    </tr>
                                    <tr align="left" valign="middle">
                                        <td colspan="2">
                                            Vehicles Delivered(Nos)
                                        </td>
                                    </tr>
                                    <tr align="left" valign="middle">
                                        <td colspan="2">
                                            Same Day Delivery(Nos)
                                        </td>
                                    </tr>
                                    <tr align="left" valign="middle">
                                        <td colspan="2">
                                            Vehicles Pending(Nos)
                                        </td>
                                    </tr>
                                    <tr align="left" valign="middle">
                                        <td rowspan="2" align="center">
                                            Bay<br />
                                            Productivity
                                        </td>
                                        <td>
                                            Speedo(Nos)
                                        </td>
                                    </tr>
                                    <tr align="left" valign="middle">
                                        <td>
                                            Normal(Nos)
                                        </td>
                                    </tr>
                                    <tr align="left" valign="middle">
                                        <td colspan="2">
                                            Total No Of Technicians Present(Nos)
                                        </td>
                                    </tr>
                                    <tr align="left" valign="middle">
                                        <td colspan="2">
                                            Technician Utilization(%)
                                        </td>
                                    </tr>
                                    <tr align="left" valign="middle">
                                        <td colspan="2">
                                            Technician Efficiency(%)
                                        </td>
                                    </tr>
                                    <tr align="left" valign="middle">
                                        <td colspan="2">
                                            Technician Productivity(%)
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                        <td class="style2" style="width: 100%;" valign="top" align="left">
                            <div style="overflow: scroll; white-space: nowrap; width: 73%; position: fixed; vertical-align: top;
                                height: 437px; margin-top: 0px;" class="scrollstyle">
                                <asp:GridView ID="grdDPD" runat="server" BorderColor="#660033" Height="420px" OnRowDataBound="grdDPD_RowDataBound">
                                    <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <HeaderStyle BackColor="Silver" ForeColor="#333333" Font-Bold="false" />
                                </asp:GridView>
                            </div>
                            <asp:ScriptManager ID="ScriptManager2" runat="server">
                            </asp:ScriptManager>
                            <asp:Timer ID="Timer1" runat="server" Interval="3000000" OnTick="Timer1_Tick">
                            </asp:Timer>
                        </td>
                    </tr>
                </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>