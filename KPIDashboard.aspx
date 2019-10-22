<%@ Page Title="" Language="C#" MasterPageFile="~/AdminModule.master" AutoEventWireup="true" CodeFile="KPIDashboard.aspx.cs" Inherits="KPIDashboard" %>
<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
            height: 100%;
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <asp:UpdatePanel runat="server" ID="DealerUpdatePanel" style="position:fixed">
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
                                        <th scope="col" colspan="3" align="right">
                                            &nbsp;&nbsp;Date&nbsp;&nbsp;&nbsp;<br />
                                            &nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lbMonthYr" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;
                                        </th>
                                    </tr>
                                    <tr align="right" valign="middle">
                                        <td colspan="3">
                                            No. of Vehicles In Flow
                                        </td>
                                    </tr>
                                    <tr align="right" valign="middle">
                                        <td rowspan="2" align="right">
                                          Job Card <br />In  WMS
                                        </td>
                                        <td style="border-right-style:none"></td>
                                        <td style="border-left-style:none">#</td>
                                    </tr>
                                    <tr align="right" valign="middle">
                                        <td style="border-right-style:none"></td>
                                        <td style="border-left-style:none">
                                           %
                                        </td>
                                    </tr>
                                    <tr align="right" valign="middle">
                                        <td rowspan="2" align="right">
                                           Job Card closed <br />In  WMS
                                        </td>
                                        <td style="border-right-style:none"></td>
                                        <td style="border-left-style:none">#</td>
                                    </tr>
                                     <tr align="right" valign="middle">
                                        <td style="border-right-style:none"></td>
                                        <td style="border-left-style:none">
                                           %
                                        </td>
                                    </tr>
                                    <tr align="right" valign="middle">
                                        <td colspan="3">
                                            Average Customer Waiting Time(Mins)
                                        </td>
                                    </tr>
                                    <tr align="right" valign="middle">
                                        <td colspan="3">
                                           No.of Vehicles Delivered.
                                        </td>
                                    </tr>
                                    <tr align="right" valign="middle">
                                        <td rowspan="2" align="right">
                                           Same day <br />Delivery
                                        </td>
                                        <td style="border-right-style:none"></td>
                                        <td style="border-left-style:none">#</td>
                                    </tr>
                                     <tr align="right" valign="middle">
                                        <td style="border-right-style:none"></td>
                                        <td style="border-left-style:none">%</td>
                                    </tr>
                                    <tr align="right" valign="middle">
                                        <td colspan="3">
                                            Total Carry Forward Vehicles&nbsp;&nbsp; #
                                        </td>
                                    </tr>
                                   <%-- <tr align="right" valign="middle">
                                        <td colspan="3" >
                                            Total Carry Forward Vehicles-JCNO &nbsp;&nbsp; #	
                                        </td>
                                    </tr>--%>
                                    <tr align="right" valign="middle">
                                        <td rowspan="2" align="right">
                                           Vehicle Alloted<br /> By JC
                                        </td>
                                        <td style="border-right-style:none"></td>
                                        <td style="border-left-style:none">#</td>
                                    </tr>
                                    <tr align="right" valign="middle">
                                       <td style="border-right-style:none"></td>
                                        <td style="border-left-style:none">%</td>

                                    </tr>
                                    <tr  valign="middle">
                                        <td rowspan="6" align="right">
                                           Stage Wise <br />Card Scan <br />Adherence
                                        </td>
                                        <td style="border-right-style:none;" align="left">Job Card</td>
                                         <td style="border-left-style:none" align="right">%</td>
                                    </tr>
                                    <tr  valign="middle">
                                        <td style="border-right-style:none" align="left">Vehicle Repair</td>
                                         <td style="border-left-style:none" align="right">%</td>
                                    </tr>
                                    <tr  valign="middle">
                                        <td style="border-right-style:none" align="left">Wheel Alignment</td>
                                        <td style="border-left-style:none" align="right">%</td>
                                    </tr>
                                     <tr  valign="middle">
                                        <td style="border-right-style:none" align="left">Final Inspection</td>
                                        <td style="border-left-style:none" align="right">%</td>
                                    </tr>
                                    <tr valign="middle">
                                        <td style="border-right-style:none" align="left">Road Test</td>
                                       <td style="border-left-style:none" align="right">%</td>
                                    </tr>
                                    <tr valign="middle">
                                        <td style="border-right-style:none" align="left">Wash</td>
                                        <td style="border-left-style:none" align="right">%</td>
                                    </tr>
                                    <%-- <tr valign="middle">
                                        <td style="border-right-style:none" align="left">VAS</td>
                                        <td style="border-left-style:none" align="right">%</td>
                                    </tr>--%>
                                    
                                     <tr valign="middle">
                                        <td rowspan="8" align="right">
                                            Bay <br />
                                        </td>
                                        <td style="border-right-style:none" align="left">Normal Bay Performance</td>
                                        <td style="border-left-style:none" align="right">%</td>
                                    </tr>
                                     <tr valign="middle">
                                        <td style="border-right-style:none" align="left">Speedo Bay Performance</td>
                                        <td style="border-left-style:none" align="right">
                                           %
                                        </td>
                                    </tr>
                                     <tr valign="middle">
                                        <td style="border-right-style:none" align="left">Productivity (Sold)</td>
                                        <td style="border-left-style:none" align="right">
                                            %
                                        </td>
                                    </tr>
                                     <tr valign="middle">
                                        <td style="border-right-style:none" align="left"> Productivity (Capacity)
</td>
                                        <td style="border-left-style:none" align="right">
                                           %
                                        </td>
                                    </tr>
                                     <tr valign="middle">
                                        <td style="border-right-style:none" align="left">Productivity (Cars/Bay) </td>
                                        <td style="border-left-style:none" align="right">
                                            #
                                        </td>
                                    </tr>
                                    <tr valign="middle">
                                        <td style="border-right-style:none" align="left">Washing </td>
                                        <td style="border-left-style:none" align="right">
                                            #
                                        </td>
                                    </tr>
                                    <tr valign="middle">
                                        <td style="border-right-style:none" align="left">Wheel Alignment </td>
                                        <td style="border-left-style:none" align="right">
                                            #
                                        </td>
                                    </tr>
                                     <tr valign="middle">
                                        <td style="border-right-style:none" align="left">Idle Time</td>
                                        <td style="border-left-style:none" align="right">
                                            HH:MM
                                        </td>
                                    </tr>
                                     <tr  valign="middle">
                                        <td rowspan="5" align="right">
                                           Technician<br/>
                                        </td>
                                        <td style="border-right-style:none" align="left">Performance </td>
                                        <td style="border-left-style:none" align="right">%</td>
                                    </tr>
                                     <tr valign="middle">
                                        <td style="border-right-style:none" align="left">Efficiency (Sold) </td>
                                        <td style="border-left-style:none" align="right">%</td>
                                    </tr>
                                     <tr valign="middle">
                                       <td style="border-right-style:none" align="left">Productivity </td>
                                        <td style="border-left-style:none" align="right">%</td>
                                      </tr>
                                      <tr valign="middle">
                                        <td style="border-right-style:none" align="left">Idle Time</td>
                                        <td style="border-left-style:none" align="right">HH:MM</td>
                                    </tr>
                                      
                                    <tr valign="middle"><td style="border-right-style:none" align="left">Technician Attendance</td><td style="border-left-style:none" align="right">%</td></tr>
                                    <tr valign="middle"><td  style="border-right-style:none" colspan="2" align="right">TAT GIGO &nbsp;&nbsp;</td><td style="border-left-style:none" align="right">HH:MM</td></tr>
                                    <tr><td align="right" colspan="3">Ready Vehicles Waiting For Delivery</td></tr>
                                     <tr><td align="right" colspan="3">No.of BodyShop Vehicles</td></tr>
                                     <tr><td align="right" colspan="3">No.of Cancelled Vehicles</td></tr>
                                   <%-- </tr>--%>

                                </table>
                            </div>
                        </td>
                        <td class="style2" style="width: 100%;" valign="top" align="left">
                            <div style="overflow: auto; white-space: nowrap; width: 67%; position: fixed; vertical-align: top;
                                height: 100%; margin-top: 0px;" class="">
                                <asp:GridView ID="grdDPD" runat="server" BorderColor="#660033" Height="600px" OnRowDataBound="grdDPD_RowDataBound">
                                    <RowStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="true" ForeColor="Black"/>
                                    <HeaderStyle BackColor="Silver" ForeColor="#333333" Font-Bold="false"  HorizontalAlign="Right"/>
                                </asp:GridView>
                                <asp:Timer ID="Timer2" runat="server" Interval="300000000" OnTick="Timer1_Tick">
                                </asp:Timer>
                            </div>
                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
                            <asp:Timer ID="Timer1" runat="server" Interval="300000000"  OnTick="Timer1_Tick">
                            </asp:Timer>
                        </td>
                         <td valign="top" class="style3" style="overflow: auto; white-space: nowrap; width: 4%&gt;
                            &lt; div" style="white-space: nowrap;">
                            <table cellspacing="0" rules="all" border="1" style="border-color: #660033; height: 635px;
                                width: 40px; border-collapse: collapse;">
                                <tr style="color: #333333; background-color: Silver; font-weight: normal;">
                                    <th style="height: 29px">
                                        <br />
                                    </th>
                                </tr>
                                <tr>
                                    <td style="height: 15px">
                                       
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 15px">
                                       <asp:ImageButton ID="ImageButton4" runat="server" ToolTip="SA Performance"
                                            Height="15px" Width="32px" ImageUrl="~/images/ReportforDashBoard.png" AlternateText="excel"
                                            OnClick="GenerateAutoSAPerformance" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 15px" valign="top">
                                       <%-- <asp:ImageButton ID="btnxcel" runat="server" ToolTip="Same Day Delivery" Height="20px"
                                            Width="32px" ImageUrl="~/images/ReportforDashBoard.png" AlternateText="excel"
                                            OnClick="GenerateAutoreportSameDay" />--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 15px">
                                       <%-- <asp:ImageButton ID="ImageButton1" runat="server" ToolTip="Carry Forward Vehicles"
                                            Height="20px" Width="32px" ImageUrl="~/images/ReportforDashBoard.png" AlternateText="excel"
                                            OnClick="GenerateAutoreportPendingVehicle" />--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 15px">
                                       <%-- <asp:ImageButton ID="ImageButton5" runat="server" ToolTip="Total Idle Time" Height="20px"
                                            Width="32px" ImageUrl="~/images/ReportforDashBoard.png" AlternateText="excel"
                                            OnClick="GenerateAutoreportIdleTime" />--%>
                                    </td>
                                </tr>
                                  <tr>
                                    <td style="height: 15px">
                                       <%-- <asp:ImageButton ID="ImageButton5" runat="server" ToolTip="Total Idle Time" Height="20px"
                                            Width="32px" ImageUrl="~/images/ReportforDashBoard.png" AlternateText="excel"
                                            OnClick="GenerateAutoreportIdleTime" />--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 45px">
                                        <asp:ImageButton ID="ImageButton6" runat="server" ToolTip="SA Wise Delivery" Height="15px"
                                            Width="32px" ImageUrl="~/images/ReportforDashBoard.png" AlternateText="excel"
                                            OnClick="GenerateAutoreportSAWiseDelivery" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 15px">
                                        <asp:ImageButton ID="ImageButton3" runat="server" ToolTip="Carry Forward Vehicles" Height="18px"
                                            Width="32px" ImageUrl="~/images/ReportforDashBoard.png" AlternateText="excel"
                                            OnClick="GenerateAutoreportCarryForwardVehicles" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 15px">
                                    </td>
                                </tr>
                                 <tr>
                                    <td style="height: 15px">
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td style="height: 100px">
                                       <asp:ImageButton ID="ImageButton1" runat="server" ToolTip="Card Scan Adherence" Height="18px"
                                            Width="32px" ImageUrl="~/images/ReportforDashBoard.png" AlternateText="excel"
                                            OnClick="GenerateAutoreportCardScanAdherence" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 15px">
                                    </td>
                                </tr>
                                 <tr>
                                    <td style="height: 15px">
                                    </td>
                                </tr>
                                   <tr>
                                    <td style="height: 70px">
                                        <asp:ImageButton ID="ImageButton2" runat="server" ToolTip="Bay TMR" Height="18px"
                                            Width="32px" ImageUrl="~/images/ReportforDashBoard.png" AlternateText="excel"
                                            OnClick="GenerateAutoreportBayTMR" />
                                    </td>
                                </tr>
                                 <tr>
                                    <td style="height: 15px">
                                        <asp:ImageButton ID="ImageButton5" runat="server" ToolTip="Employee Engagement Report" Height="18px"
                                            Width="32px" ImageUrl="~/images/ReportforDashBoard.png" AlternateText="excel"
                                            OnClick="GenerateAutoreportEmployeeEngagement" />
                                    </td>
                                </tr>
                                 <tr>
                                 <td style="height: 10px"></td>
                                 </tr>
                                   <tr>
                                 <td style="height: 10px"></td>
                                 </tr>
                                   <tr>
                                 <td style="height: 10px"></td>
                                 </tr>
                                 <tr>
                                    <td style="height: 15px">
                                        <asp:ImageButton ID="ImageButton7" runat="server" ToolTip="Employee Attrndance" Height="18px"
                                            Width="32px" ImageUrl="~/images/ReportforDashBoard.png" AlternateText="excel"
                                            OnClick="GenerateAutoreportEmployeeAttendance" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 15px">
                                       <%-- <asp:ImageButton ID="ImageButton2" runat="server" ToolTip="Technician Utilization/Efficency/Productivity"
                                            Height="20px" Width="32px" ImageUrl="~/images/ReportforDashBoard.png" AlternateText="excel"
                                            OnClick="GenerateAutoreportTechnicianUtilization" />--%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>
                            <rsweb:ReportViewer ID="RptViewer" runat="server">
                            </rsweb:ReportViewer>
                        </td>
                    </tr>
                </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

