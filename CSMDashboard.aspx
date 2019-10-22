<%@ Page Title="" Language="C#" MasterPageFile="~/AdminModule.master" AutoEventWireup="true"
    CodeFile="CSMDashboard.aspx.cs" Inherits="CSMDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function stopRKey(evt) {
            var evt = (evt) ? evt : ((event) ? event : null);
            var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
            if ((evt.keyCode == 8 || evt.KeyCode == 46)) { return false; }
        }
        document.onkeypress = stopRKey;
    </script>
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
                        <td align="left" class="style3" valign="top">
                            <div style="white-space: nowrap;">
                                <table cellspacing="0" rules="all" border="1" id="grdCaption" style="border-color: #660033;
                                    height: 420px; border-collapse: collapse;">
                                    <tr style="color: #333333; background-color: Silver; font-weight: normal;">
                                        <th scope="col" colspan="2" align="center">
                                            Date&nbsp;&nbsp;&nbsp;<br />
                                            &nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lbMonthYr" runat="server" Text=""></asp:Label>
                                            &nbsp;&nbsp;&nbsp;
                                        </th>
                                    </tr>
                                    <tr align="left" valign="middle">
                                        <td colspan="2">
                                            Vehicle Tagged by Security(Nos.)
                                        </td>
                                    </tr>
                                    <tr align="left" valign="middle">
                                        <td rowspan="2" align="center">
                                            Job Card Opened<br />
                                            In VTABS
                                        </td>
                                        <td>
                                            Nos.
                                        </td>
                                    </tr>
                                    <tr align="left" valign="middle">
                                        <td>
                                            %
                                        </td>
                                    </tr>
                                    <tr align="left" valign="middle">
                                        <td colspan="2">
                                            Vehicle Delivered(Nos.)
                                        </td>
                                    </tr>
                                    <tr align="left" valign="middle">
                                        <td rowspan="2" align="center">
                                            Job Card Closed<br />
                                            In VTABS
                                        </td>
                                        <td>
                                            Nos.
                                        </td>
                                    </tr>
                                    <tr align="left" valign="middle">
                                        <td>
                                            %
                                        </td>
                                    </tr>
                                    <tr align="left" valign="middle">
                                        <td colspan="2">
                                            Average Customer Waiting Time(Mins)
                                        </td>
                                    </tr>
                                    <tr align="left" valign="middle">
                                        <td colspan="2">
                                            Total Pending Vehicles(Nos.)
                                        </td>
                                    </tr>
                                    <tr align="left" valign="middle">
                                        <td rowspan="2" align="center">
                                            Same Day<br />
                                            Delivery
                                        </td>
                                        <td>
                                            Nos.
                                        </td>
                                    </tr>
                                    <tr align="left" valign="middle">
                                        <td>
                                            %
                                        </td>
                                    </tr>
                                    <tr align="left" valign="middle">
                                        <td rowspan="5" align="center">
                                            Monthly Card<br />
                                            Scanning<br />
                                            Adherence(%)
                                            <td>
                                                Job Slip
                                            </td>
                                    </tr>
                                    <tr align="left" valign="middle">
                                        <td>
                                            Vehicle Repair
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Wheel Alignment
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Final Inspection
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Wash
                                        </td>
                                    </tr>
                    </tr>
                    <tr align="left" valign="middle">
                        <td rowspan="2" align="center">
                            Bay Productivity<br />
                            (Nos.)
                        </td>
                        <td>
                            Normal Bay
                        </td>
                    </tr>
                    <tr align="left" valign="middle">
                        <td>
                            Speedo Bay
                        </td>
                    </tr>
                    <tr align="left" valign="middle">
                        <td colspan="2">
                            Bay Utilization (%)
                        </td>
                    </tr>
                    <tr align="left" valign="middle">
                        <td rowspan="2" align="center">
                            Technician
                            <br />
                            Productivity(%)
                        </td>
                        <td>
                            Booked
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Capacity
                        </td>
                    </tr>
                    <tr align="left" valign="middle">
                        <td colspan="2">
                            Technician Utilization (%)
                        </td>
                    </tr>
                    <tr align="left" valign="middle">
                        <td rowspan="2">
                            Attendance
                            <br />
                            (Total No.of
                            <br />
                            Technicians)
                        </td>
                        <td>
                            Nos. present
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Present %
                        </td>
                    </tr>
                </table>
                </div> </td>
                <td align="left" class="style2" style="width: 100%;" valign="top">
                    <div class="scrollstyle" style="overflow: scroll; white-space: nowrap; width: 73%;
                        position: fixed; vertical-align: top; height: 525px; margin-top: 0px;">
                        <asp:GridView ID="grdDPD" runat="server" BorderColor="#660033" Height="500px" OnRowDataBound="grdDPD_RowDataBound">
                            <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <HeaderStyle BackColor="Silver" Font-Bold="false" ForeColor="#333333" />
                        </asp:GridView>
                    </div>
                    <asp:ScriptManager ID="ScriptManager2" runat="server">
                    </asp:ScriptManager>
                    <asp:Timer ID="Timer1" runat="server" Interval="3000000" OnTick="Timer1_Tick">
                    </asp:Timer>
                </td>
                </tr>
            </table>
            </tr>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>