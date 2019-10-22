<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PositionDisplay.aspx.cs"
    Inherits="PositionDisplay" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Position Display</title>
    <link href="CSS/ProTRAC_Css.css" rel="stylesheet" type="text/css" />
    <link href="CSS/Stylesheet.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="js/Tooltip.js" type="text/javascript"></script>
    <style type="text/css">
        #RegnoLabel
        {
            white-space: nowrap;
        }
        #DisplayStatus
        {
            font-family: Arial;
            font-weight: bold;
            font-size: x-large;
        }
        .HeaderLeft
        {
            height: 50px;
            width: 33%;
            font-family: Arial;
            font-size: smaller;
            font-weight: bold;
            text-align: center;
        }
        .HeaderCenter
        {
            width: 34%;
            height: 50px;
            font-size: 30px;
            color: #4682B4;
            font-family: Arial;
            font-weight: bold;
            text-align: center;
        }
        .HeaderRight
        {
            height: 50px;
            width: 33%;
            text-align: center;
        }
        #backgroundPopup
        {
            display: none;
            position: fixed;
            _position: absolute;
            height: 100%;
            width: 100%;
            top: 0;
            left: 0;
            background: #000000;
            border: 1px solid #cecece;
            z-index: 1;
        }
        #popupContact
        {
            display: none;
            position: fixed;
            _position: absolute;
            height: 384px;
            width: 408px;
            background: #FFFFFF;
            border: 2px solid #cecece;
            z-index: 2;
            padding: 12px;
            font-size: 13px;
        }
        #popupContactClose
        {
            font-size: 14px;
            line-height: 14px;
            right: 6px;
            top: 4px;
            position: absolute;
            color: #6fa5fd;
            font-weight: 700;
            display: block;
        }
        #RegnoLabel0
        {
            white-space: nowrap;
        }
        .modalBackground
        {
            opacity: 0.75;
            background-color: #000;
            height: 400px;
            y-overflow: scroll;
        }
        #PopupPnl
        {
            background-color: #e0ecff;
            width: 390px;
            border-width: 3px;
            border-color: Black;
            border-style: solid;
        }
        .style1
        {
            width: 25%;
            height: 109px;
        }
    </style>
    <script language="javascript" type="text/javascript">
        var t1;

        $(document).ready(
    function () {
        t1 = new ToolTip("a", true, 40);
    });

        function ShowLoadProcessInOutTime(e, RegNo, ProcessName) {
            $.ajax({
                type: "POST",
                url: "PositionDisplay.aspx/LoadProcessInOutTime",
                data: "{'RefNo':'" + RegNo + "','ProcessName':'" + ProcessName + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    str = result.d;
                    t1.Show(e, str);
                },
                error: function (result) {
                    t1.Show(e, result.status + ' ' + result.statusText);
                }
            }
      );

        }

        function ShowWorkshopLoadProcessInOutTime(e, RegNo, ProcessName) {
            $.ajax({
                type: "POST",
                url: "PositionDisplay.aspx/LoadWorkshopProcessInOutTime",
                data: "{'RefNo':'" + RegNo + "','ProcessName':'" + ProcessName + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    str = result.d;
                    t1.Show(e, str);
                },
                error: function (result) {
                    t1.Show(e, result.status + ' ' + result.statusText);
                }
            }
      );

        }

        function ShowLoadIdleInOutTime(e, RegNo) {
            $.ajax({
                type: "POST",
                url: "PositionDisplay.aspx/LoadIdleInOutTime",
                data: "{'RefNo':'" + RegNo + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    str = result.d;
                    t1.Show(e, str);
                },
                error: function (result) {
                    t1.Show(e, result.status + ' ' + result.statusText);
                }
            }
      );
        }

        function hideTooltip(e) {
            if (t1) t1.Hide(e);
        }

        Event.observe(window, 'load', init, false);
    </script>
    <script type="text/javascript">
        function goSupport() {
            window.open('Complain.aspx');
        }

        function goHelp() {
            window.open('Help/index.htm');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="a" class="ttipStyle" style="font-family: Arial; background-color: white;
        width: 155px; height: 40px; border: solid 1px gray; text-align: left;" align="center">
    </div>
    <asp:UpdatePanel ID="upUpdateGrid" runat="server">
        <ContentTemplate>
            <table class="fullStyle" cellpadding="0" cellspacing="0">
                <tr class="smallFont">
                    <td>
                        <table class="fullStyle" bgcolor="Silver">
                            <tr>
                                <td style="width: 25%; padding-right: 10px;" align="left">
                                    <asp:Label ID="lblSyncTime" runat="server" Font-Size="Medium" ForeColor="#333333"></asp:Label>
                                </td>
                                <td style="width: 50%; text-align: center; height: 25px;">
                                    <asp:Label ID="lbl_CurrentPage" runat="server" ForeColor="#333333" Text="Position Display"></asp:Label>
                                </td>
                                <td style="width: 25%;" align="right">
                                    <asp:Button CssClass="button" ID="btnBACK" Visible="false" runat="server" Text="BACK"
                                        OnClick="btnBACK_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="tmrGrid" EventName="Tick" />
        </Triggers>
    </asp:UpdatePanel>
    <div style="text-align: center;">
        <asp:Timer ID="tmrGrid" runat="server" OnTick="tmrGrid_Tick" Interval="5000">
        </asp:Timer>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="border-color: #FFFFFF; background-color: Silver; width: 100%; color: #333333;
                text-align: center;" border="0" cellspacing="0" cellpadding="0">
                <tr style="font-size: 12px; height: 30px;">
                    <td>
                        <asp:Label ID="Label12" runat="server" Width="100px" Text="Waiting (Gate)"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label13" runat="server" Width="100px" Text="Jobslip"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label14" runat="server" Width="100px" Text="Wash"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label15" runat="server" Width="100px" Text="Workshop"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label16" runat="server" Width="100px" Text="WA / VAS"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label17" runat="server" Width="100px" Text="Final Inspection"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label18" runat="server" Width="100px" Text="Road Test"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label19" runat="server" Width="100px" Text="Vehicle Ready"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label20" runat="server" Width="100px" Text="Vehicle Idle"></asp:Label>
                    </td>
                    <td width="100%">
                        <asp:Label ID="Label21" runat="server" Width="100%" Text="Total"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label Width="100px" ID="lbWGate" runat="server" Text="0"></asp:Label>
                    </td>
                    <td>
                        <asp:Label Width="100px" ID="lbRO" runat="server" Text="0"></asp:Label>
                    </td>
                    <td>
                        <asp:Label Width="100px" ID="lbWash" runat="server" Text="0"></asp:Label>
                    </td>
                    <td>
                        <asp:Label Width="100px" ID="lbWorkshop" runat="server" Text="0"></asp:Label>
                    </td>
                    <td>
                        <asp:Label Width="100px" ID="lbWA" runat="server" Text="0"></asp:Label>
                    </td>
                    <td>
                        <asp:Label Width="100px" ID="lbFI" runat="server" Text="0"></asp:Label>
                    </td>
                    <td>
                        <asp:Label Width="100px" ID="lbRT" runat="server" Text="0"></asp:Label>
                    </td>
                    <td>
                        <asp:Label Width="100px" ID="lbVR" runat="server" Text="0"></asp:Label>
                    </td>
                    <td>
                        <asp:Label Width="100px" ID="lbVI" runat="server" Text="0"></asp:Label>
                    </td>
                    <td width="100%">
                        <asp:Label Width="100%" ID="lbTotal" runat="server" Text="0"></asp:Label>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="tmrGrid" EventName="Tick" />
        </Triggers>
    </asp:UpdatePanel>
    <div>
        <center>
            <table class="fullStyle">
                <tr valign="top" align="center">
                    <td valign="top" align="center" class="style1">
                        <div id="Waiting" runat="server">
                            <div class="panel1Red">
                                <asp:UpdatePanel ID="updWaitPnl1" runat="server">
                                    <ContentTemplate>
                                        <table class="fullStyle">
                                            <tr>
                                                <td>
                                                    Waiting (Gate)
                                                </td>
                                                <td style="padding-right: 5px; width: 18px; height: 18px;">
                                                    <div class="countStyle">
                                                        <asp:Label ID="lblGateCount" runat="server">0</asp:Label></div>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="tmrGrid" EventName="Tick" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                            <div class="divblock" style="width: 100%;">
                                <asp:UpdatePanel ID="updWaitPnl2" runat="server">
                                    <ContentTemplate>
                                        <asp:ListView ID="lstWaiting" runat="server" DataSourceID="srcWaiting" GroupItemCount="3"
                                            OnItemDataBound="lstWaiting_ItemDataBound">
                                            <EmptyItemTemplate>
                                            </EmptyItemTemplate>
                                            <ItemTemplate>
                                                <td id="Td2" runat="server" style="background-color: White; color: white; font-size: small;
                                                    width: 76px; height: 91px;" valign="top">
                                                    <div id="BackDiv" runat="server">
                                                        <center>
                                                            <div style="height: 60px; width: 100%">
                                                                <asp:Image ID="ModelImg" runat="server" Width="100%" Height="60px" />
                                                                <asp:HiddenField ID="hfModel" runat="server" Value='<%# Eval("VehicleModel") %>' />
                                                                <asp:Label ID="lblRefNo" runat="server" Text='<%# Eval("Slno") %>' ForeColor="#000066"
                                                                    Style="display: none;" />
                                                                <asp:Label ID="lblDev" runat="server" Text='<%# Eval("Deviation") %>' ForeColor="#000066"
                                                                    Style="display: none;" />
                                                                <asp:Label ID="lblPosition" runat="server" Text='<%# Eval("Position") %>' ForeColor="#000066"
                                                                    Style="display: none;" />
                                                            </div>
                                                            <div style="height: 52px; width: 100%">
                                                                <asp:Label ID="RegnoLabel" runat="server" Text='<%# Eval("Regno") %>' ForeColor="#000066" />
                                                                <br />
                                                                <asp:Label ID="InTimeLabel" runat="server" Text='<%# Eval("InTime") %>' ForeColor="Black" />
                                                            </div>
                                                        </center>
                                                    </div>
                                                </td>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <table id="Table1" runat="server" style="color: Black; height: 100%; font-weight: bold;
                                                    text-align: center; width: 100%; border-collapse: collapse; border-color: #999999;
                                                    border-style: none; border-width: 1px;">
                                                    <tr>
                                                        <td>
                                                            No Vehicles
                                                        </td>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
                                            <LayoutTemplate>
                                                <table id="Table2" runat="server">
                                                    <tr id="Tr1" runat="server" style="width: 290px;">
                                                        <td id="Td3" runat="server">
                                                            <table id="groupPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF;
                                                                border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;
                                                                font-family: Verdana, Arial, Helvetica, sans-serif;">
                                                                <tr id="groupPlaceholder" runat="server">
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </LayoutTemplate>
                                            <GroupTemplate>
                                                <tr id="itemPlaceholderContainer" runat="server">
                                                    <td id="itemPlaceholder" runat="server">
                                                    </td>
                                                </tr>
                                            </GroupTemplate>
                                        </asp:ListView>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="tmrGrid" EventName="Tick" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <cc1:ModalPopupExtender ID="lstWaiting_ModalPopupExtender" runat="server" DynamicServicePath=""
                                    BackgroundCssClass="modalBackground" Enabled="True" TargetControlID="updWaitPnl2"
                                    PopupControlID="PopPnl1" CancelControlID="btnPnlClose1">
                                </cc1:ModalPopupExtender>
                                <asp:HiddenField ID="hfWaiting" runat="server" Value="Gate" />
                                <asp:SqlDataSource ID="srcWaiting" runat="server" ConnectionString="<%$ ConnectionStrings:ProTRAC_ConnectionString %>"
                                    SelectCommand="GetVehiclePositionInfo" SelectCommandType="StoredProcedure">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="hfWaiting" DefaultValue="Gate" Name="Position" PropertyName="Value"
                                            Type="String" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </div>
                        </div>
                    </td>
                    <td valign="top" align="center" class="style1">
                        <div id="Jobslip" runat="server">
                            <div class="panel1Blue">
                                <asp:UpdatePanel ID="updJobSlipPnl1" runat="server">
                                    <ContentTemplate>
                                        <table class="fullStyle">
                                            <tr>
                                                <td>
                                                    Jobslip
                                                </td>
                                                <td style="padding-right: 5px; width: 18px; height: 18px;">
                                                    <div class="countStyle">
                                                        <asp:Label ID="lblJobSlipCount" runat="server">0</asp:Label></div>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="tmrGrid" EventName="Tick" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                            <div class="divblock" style="width: 100%;">
                                <asp:UpdatePanel ID="updJobSlipPnl2" runat="server">
                                    <ContentTemplate>
                                        <asp:ListView ID="lstJobSlip" runat="server" DataSourceID="srcJobSlip" GroupItemCount="3"
                                            OnItemDataBound="lstJobSlip_ItemDataBound">
                                            <EmptyItemTemplate>
                                            </EmptyItemTemplate>
                                            <ItemTemplate>
                                                <td id="Td2" runat="server" style="background-color: White; color: white; font-size: small;
                                                    width: 76px; height: 91px;" valign="top">
                                                    <div id="BackDiv" runat="server">
                                                        <center>
                                                            <div style="height: 60px; width: 100%">
                                                                <asp:Image ID="ModelImg" runat="server" Style="height: 60px; width: 100%;" />
                                                                <asp:HiddenField ID="hfModel" runat="server" Value='<%# Eval("VehicleModel") %>' />
                                                                <asp:Label ID="lblRefNo" runat="server" Text='<%# Eval("Slno") %>' ForeColor="#000066"
                                                                    Style="display: none;" />
                                                                <asp:Label ID="lblDev" runat="server" Text='<%# Eval("Deviation") %>' ForeColor="#000066"
                                                                    Style="display: none;" />
                                                                <asp:Label ID="lblPosition" runat="server" Text='<%# Eval("Position") %>' ForeColor="#000066"
                                                                    Style="display: none;" />
                                                            </div>
                                                            <div style="height: 52px; width: 100%">
                                                                <asp:Label ID="RegnoLabel" runat="server" Text='<%# Eval("Regno") %>' ForeColor="#000066" />
                                                                <br />
                                                                <asp:Label ID="InTimeLabel" runat="server" Text='<%# Eval("InTime") %>' ForeColor="Black" />
                                                            </div>
                                                        </center>
                                                    </div>
                                                </td>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <table id="Table1" runat="server" style="color: Black; height: 100%; font-weight: bold;
                                                    text-align: center; width: 100%; border-collapse: collapse; border-color: #999999;
                                                    border-style: none; border-width: 1px;">
                                                    <tr>
                                                        <td>
                                                            No Vehicles
                                                        </td>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
                                            <LayoutTemplate>
                                                <table id="Table2" runat="server">
                                                    <tr id="Tr1" runat="server" style="width: 290px;">
                                                        <td id="Td3" runat="server">
                                                            <table id="groupPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF;
                                                                border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;
                                                                font-family: Verdana, Arial, Helvetica, sans-serif;">
                                                                <tr id="groupPlaceholder" runat="server">
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </LayoutTemplate>
                                            <GroupTemplate>
                                                <tr id="itemPlaceholderContainer" runat="server">
                                                    <td id="itemPlaceholder" runat="server">
                                                    </td>
                                                </tr>
                                            </GroupTemplate>
                                        </asp:ListView>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="tmrGrid" EventName="Tick" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <cc1:ModalPopupExtender ID="lstJobSlip_ModalPopupExtender" runat="server" DynamicServicePath=""
                                    BackgroundCssClass="modalBackground" Enabled="True" TargetControlID="updJobSlipPnl2"
                                    PopupControlID="PopPnl2" CancelControlID="btnPnlClose2">
                                </cc1:ModalPopupExtender>
                                <asp:HiddenField ID="hfJobSlip" runat="server" Value="jobslip" />
                                <asp:SqlDataSource ID="srcJobSlip" runat="server" ConnectionString="<%$ ConnectionStrings:ProTRAC_ConnectionString %>"
                                    SelectCommand="GetVehiclePositionInfo" SelectCommandType="StoredProcedure">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="hfJobSlip" DefaultValue="JobSlip" Name="Position"
                                            PropertyName="Value" Type="String" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </div>
                        </div>
                    </td>
                    <td valign="top" align="center" class="style1">
                        <div id="Div3" runat="server">
                            <div class="panel1Yellow">
                                <asp:UpdatePanel ID="updWashPnl1" runat="server">
                                    <ContentTemplate>
                                        <table class="fullStyle">
                                            <tr>
                                                <td>
                                                    Wash
                                                </td>
                                                <td style="padding-right: 5px; width: 18px; height: 18px;">
                                                    <div class="countStyle">
                                                        <asp:Label ID="lblWashCount" runat="server">0</asp:Label>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="tmrGrid" EventName="Tick" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                            <div class="divblock" style="width: 100%;">
                                <asp:UpdatePanel ID="updWashPnl2" runat="server">
                                    <ContentTemplate>
                                        <asp:ListView ID="lstWash" runat="server" DataSourceID="srcWash" GroupItemCount="3"
                                            OnItemDataBound="lstWash_ItemDataBound">
                                            <EmptyItemTemplate>
                                            </EmptyItemTemplate>
                                            <ItemTemplate>
                                                <td id="Td5" runat="server" style="background-color: White; color: white; font-size: small;
                                                    width: 76px; height: 91px;" valign="top">
                                                    <div id="BackDiv" runat="server">
                                                        <center>
                                                            <div style="height: 60px; width: 100%">
                                                                <asp:Image ID="ModelImg" runat="server" Style="height: 60px; width: 100%;" />
                                                                <asp:HiddenField ID="hfModel" runat="server" Value='<%# Eval("VehicleModel") %>' />
                                                                <asp:Label ID="lblRefNo" runat="server" Text='<%# Eval("Slno") %>' ForeColor="#000066"
                                                                    Style="display: none;" />
                                                                <asp:Label ID="lblDev" runat="server" Text='<%# Eval("Deviation") %>' ForeColor="#000066"
                                                                    Style="display: none;" />
                                                                <asp:Label ID="lblPosition" runat="server" Text='<%# Eval("Position") %>' ForeColor="#000066"
                                                                    Style="display: none;" />
                                                            </div>
                                                            <div style="height: 52px; width: 100%">
                                                                <asp:Label ID="RegnoLabel" runat="server" Text='<%# Eval("Regno") %>' ForeColor="#000066" />
                                                                <br />
                                                                <asp:Label ID="InTimeLabel" runat="server" Text='<%# Eval("InTime") %>' ForeColor="Black" />
                                                            </div>
                                                        </center>
                                                    </div>
                                                </td>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <table id="Table3" runat="server" style="color: Black; height: 100%; font-weight: bold;
                                                    text-align: center; width: 100%; border-collapse: collapse; border-color: #999999;
                                                    border-style: none; border-width: 1px;">
                                                    <tr>
                                                        <td>
                                                            No Vehicles
                                                        </td>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
                                            <LayoutTemplate>
                                                <table id="Table4" runat="server">
                                                    <tr id="Tr2" runat="server" style="width: 290px;">
                                                        <td id="Td6" runat="server">
                                                            <table id="groupPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF;
                                                                border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;
                                                                font-family: Verdana, Arial, Helvetica, sans-serif;">
                                                                <tr id="groupPlaceholder" runat="server">
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </LayoutTemplate>
                                            <GroupTemplate>
                                                <tr id="itemPlaceholderContainer" runat="server">
                                                    <td id="itemPlaceholder" runat="server">
                                                    </td>
                                                </tr>
                                            </GroupTemplate>
                                        </asp:ListView>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="tmrGrid" EventName="Tick" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <cc1:ModalPopupExtender ID="lstWash_ModalPopupExtender" runat="server" DynamicServicePath=""
                                    BackgroundCssClass="modalBackground" Enabled="True" TargetControlID="updWashPnl2"
                                    PopupControlID="PopPnl3" CancelControlID="btnPnlClose3">
                                </cc1:ModalPopupExtender>
                                <asp:HiddenField ID="hfWash" runat="server" Value="wash" />
                                <asp:SqlDataSource ID="srcWash" runat="server" ConnectionString="<%$ ConnectionStrings:ProTRAC_ConnectionString %>"
                                    SelectCommand="GetVehiclePositionInfo" SelectCommandType="StoredProcedure">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="hfWash" DefaultValue="Wash" Name="Position" PropertyName="Value"
                                            Type="String" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </div>
                        </div>
                    </td>
                    <td align="center" valign="top" class="style1">
                        <div id="Div2" runat="server">
                            <div class="panel1Orange">
                                <asp:UpdatePanel ID="updWorkshopPnl1" runat="server">
                                    <ContentTemplate>
                                        <table class="fullStyle">
                                            <tr>
                                                <td>
                                                    Workshop
                                                </td>
                                                <td style="padding-right: 5px; width: 18px; height: 18px;">
                                                    <div class="countStyle">
                                                        <asp:Label ID="lblWorkshopCount" runat="server">0</asp:Label>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="tmrGrid" EventName="Tick" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                            <div class="divblock" style="width: 100%;">
                                <asp:UpdatePanel ID="updWorkshopPnl2" runat="server">
                                    <ContentTemplate>
                                        <asp:ListView ID="lstFloor" runat="server" DataSourceID="srcFloor" GroupItemCount="3"
                                            OnItemDataBound="lstFloor_ItemDataBound">
                                            <EmptyItemTemplate>
                                            </EmptyItemTemplate>
                                            <ItemTemplate>
                                                <td id="Td5" runat="server" style="background-color: White; color: white; font-size: small;
                                                    width: 76px; height: 91px;" valign="top">
                                                    <div id="BackDiv" runat="server">
                                                        <center>
                                                            <div style="height: 60px; width: 100%">
                                                                <asp:Image ID="ModelImg" runat="server" Style="height: 60px; width: 100%;" />
                                                                <asp:HiddenField ID="hfModel" runat="server" Value='<%# Eval("VehicleModel") %>' />
                                                                <asp:Label ID="lblRefNo" runat="server" Text='<%# Eval("Slno") %>' ForeColor="#000066"
                                                                    Style="display: none;" />
                                                                <asp:Label ID="lblDev" runat="server" Text='<%# Eval("Deviation") %>' ForeColor="#000066"
                                                                    Style="display: none;" />
                                                                <asp:Label ID="lblPosition" runat="server" Text='<%# Eval("Position") %>' ForeColor="#000066"
                                                                    Style="display: none;" />
                                                            </div>
                                                            <div style="height: 52px; width: 100%">
                                                                <asp:Label ID="RegnoLabel" runat="server" Text='<%# Eval("Regno") %>' ForeColor="#000066" />
                                                                <br />
                                                                <asp:Label ID="InTimeLabel" runat="server" Text='<%# Eval("InTime") %>' ForeColor="Black" />
                                                            </div>
                                                        </center>
                                                    </div>
                                                </td>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <table id="Table3" runat="server" style="color: Black; height: 100%; font-weight: bold;
                                                    text-align: center; width: 100%; border-collapse: collapse; border-color: #999999;
                                                    border-style: none; border-width: 1px;">
                                                    <tr>
                                                        <td>
                                                            No Vehicles
                                                        </td>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
                                            <LayoutTemplate>
                                                <table id="Table4" runat="server">
                                                    <tr id="Tr2" runat="server" style="width: 290px;">
                                                        <td id="Td6" runat="server">
                                                            <table id="groupPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF;
                                                                border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;
                                                                font-family: Verdana, Arial, Helvetica, sans-serif;">
                                                                <tr id="groupPlaceholder" runat="server">
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </LayoutTemplate>
                                            <GroupTemplate>
                                                <tr id="itemPlaceholderContainer" runat="server">
                                                    <td id="itemPlaceholder" runat="server">
                                                    </td>
                                                </tr>
                                            </GroupTemplate>
                                        </asp:ListView>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="tmrGrid" EventName="Tick" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <cc1:ModalPopupExtender ID="lstFloor_ModalPopupExtender" runat="server" DynamicServicePath=""
                                    BackgroundCssClass="modalBackground" Enabled="True" TargetControlID="updWorkshopPnl2"
                                    PopupControlID="PopPnl4" CancelControlID="btnPnlClose4">
                                </cc1:ModalPopupExtender>
                                <asp:HiddenField ID="hfFloor" runat="server" Value="WorkShop" />
                                <asp:SqlDataSource ID="srcFloor" runat="server" ConnectionString="<%$ ConnectionStrings:ProTRAC_ConnectionString %>"
                                    SelectCommand="GetVehiclePositionInfo" SelectCommandType="StoredProcedure">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="hfFloor" DefaultValue="WorkShop" Name="Position"
                                            PropertyName="Value" Type="String" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td valign="top" align="center" style="width: 25%;">
                        <div id="Div5" runat="server">
                            <div class="panel1Violet">
                                <asp:UpdatePanel ID="updWAPnl1" runat="server">
                                    <ContentTemplate>
                                        <table class="fullStyle">
                                            <tr>
                                                <td>
                                                    Wheel Alignment / VAS
                                                </td>
                                                <td style="padding-right: 5px; width: 18px; height: 18px;">
                                                    <div class="countStyle">
                                                        <asp:Label ID="lblWACount" runat="server">0</asp:Label>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="tmrGrid" EventName="Tick" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                            <div class="divblock" style="width: 100%;">
                                <asp:UpdatePanel ID="updWAPnl2" runat="server">
                                    <ContentTemplate>
                                        <asp:ListView ID="lstWA" runat="server" DataSourceID="srcWA" GroupItemCount="3" OnItemDataBound="lstWA_ItemDataBound">
                                            <EmptyItemTemplate>
                                            </EmptyItemTemplate>
                                            <ItemTemplate>
                                                <td id="Td5" runat="server" style="background-color: #c3d9ff; color: white; font-size: small;
                                                    width: 76px; height: 91px; background-repeat: no-repeat;" valign="top">
                                                    <div id="BackDiv" runat="server">
                                                        <center>
                                                            <div style="height: 60px; width: 100%">
                                                                <asp:Image ID="ModelImg" runat="server" Style="height: 60px; width: 100%;" />
                                                                <asp:HiddenField ID="hfModel" runat="server" Value='<%# Eval("VehicleModel") %>' />
                                                                <asp:Label ID="lblRefNo" runat="server" Text='<%# Eval("Slno") %>' ForeColor="#000066"
                                                                    Style="display: none;" />
                                                                <asp:Label ID="lblDev" runat="server" Text='<%# Eval("Deviation") %>' ForeColor="#000066"
                                                                    Style="display: none;" />
                                                                <asp:Label ID="lblPosition" runat="server" Text='<%# Eval("Position") %>' ForeColor="#000066"
                                                                    Style="display: none;" />
                                                            </div>
                                                            <div style="height: 52px; width: 100%">
                                                                <asp:Label ID="RegnoLabel" runat="server" Text='<%# Eval("Regno") %>' ForeColor="#000066" />
                                                                <br />
                                                                <asp:Label ID="InTimeLabel" runat="server" Text='<%# Eval("InTime") %>' ForeColor="Black" />
                                                            </div>
                                                        </center>
                                                    </div>
                                                </td>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <table id="Table3" runat="server" style="color: Black; height: 100%; font-weight: bold;
                                                    text-align: center; width: 100%; border-collapse: collapse; border-color: #999999;
                                                    border-style: none; border-width: 1px;">
                                                    <tr>
                                                        <td>
                                                            No Vehicles
                                                        </td>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
                                            <LayoutTemplate>
                                                <table id="Table4" runat="server">
                                                    <tr id="Tr2" runat="server" style="width: 290px;">
                                                        <td id="Td6" runat="server">
                                                            <table id="groupPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF;
                                                                border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;
                                                                font-family: Verdana, Arial, Helvetica, sans-serif;">
                                                                <tr id="groupPlaceholder" runat="server">
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </LayoutTemplate>
                                            <GroupTemplate>
                                                <tr id="itemPlaceholderContainer" runat="server">
                                                    <td id="itemPlaceholder" runat="server">
                                                    </td>
                                                </tr>
                                            </GroupTemplate>
                                        </asp:ListView>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="tmrGrid" EventName="Tick" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <cc1:ModalPopupExtender ID="lstWA_ModalPopupExtender" runat="server" DynamicServicePath=""
                                    BackgroundCssClass="modalBackground" Enabled="True" TargetControlID="updWAPnl2"
                                    PopupControlID="PopPnl5" CancelControlID="btnPnlClose5">
                                </cc1:ModalPopupExtender>
                                <asp:HiddenField ID="hfWA" runat="server" Value="Wheel Alignment" />
                                <asp:SqlDataSource ID="srcWA" runat="server" ConnectionString="<%$ ConnectionStrings:ProTRAC_ConnectionString %>"
                                    SelectCommand="GetVehiclePositionInfo" SelectCommandType="StoredProcedure">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="hfWA" DefaultValue="Wheel Alignment" Name="Position"
                                            PropertyName="Value" Type="String" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </div>
                        </div>
                    </td>
                    <td valign="top" align="center" style="width: 25%;">
                        <div id="Div4" runat="server">
                            <div class="panel1White">
                                <asp:UpdatePanel ID="updFIPnl1" runat="server">
                                    <ContentTemplate>
                                        <table class="fullStyle">
                                            <tr>
                                                <td>
                                                    Final Inspection
                                                </td>
                                                <td style="padding-right: 5px; width: 18px; height: 18px;">
                                                    <div class="countStyle">
                                                        <asp:Label ID="lblFICount" runat="server">0</asp:Label>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="tmrGrid" EventName="Tick" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                            <div class="divblock" style="width: 100%;">
                                <asp:UpdatePanel ID="updFIPnl2" runat="server">
                                    <ContentTemplate>
                                        <asp:ListView ID="lstQC" runat="server" DataSourceID="srcQC" GroupItemCount="3" OnItemDataBound="lstQC_ItemDataBound">
                                            <EmptyItemTemplate>
                                            </EmptyItemTemplate>
                                            <ItemTemplate>
                                                <td id="Td5" runat="server" style="background-color: #c3d9ff; color: white; font-size: small;
                                                    width: 76px; height: 91px; background-repeat: no-repeat;" valign="top">
                                                    <div id="BackDiv" runat="server">
                                                        <center>
                                                            <div style="height: 60px; width: 100%">
                                                                <asp:Image ID="ModelImg" runat="server" Style="height: 60px; width: 100%;" />
                                                                <asp:HiddenField ID="hfModel" runat="server" Value='<%# Eval("VehicleModel") %>' />
                                                                <asp:Label ID="lblRefNo" runat="server" Text='<%# Eval("Slno") %>' ForeColor="#000066"
                                                                    Style="display: none;" />
                                                                <asp:Label ID="lblDev" runat="server" Text='<%# Eval("Deviation") %>' ForeColor="#000066"
                                                                    Style="display: none;" />
                                                                <asp:Label ID="lblPosition" runat="server" Text='<%# Eval("Position") %>' ForeColor="#000066"
                                                                    Style="display: none;" />
                                                            </div>
                                                            <div style="height: 52px; width: 100%">
                                                                <asp:Label ID="RegnoLabel" runat="server" Text='<%# Eval("Regno") %>' ForeColor="#000066" />
                                                                <br />
                                                                <asp:Label ID="InTimeLabel" runat="server" Text='<%# Eval("InTime") %>' ForeColor="Black" />
                                                            </div>
                                                        </center>
                                                    </div>
                                                </td>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <table id="Table3" runat="server" style="color: Black; height: 100%; font-weight: bold;
                                                    text-align: center; width: 100%; border-collapse: collapse; border-color: #999999;
                                                    border-style: none; border-width: 1px;">
                                                    <tr>
                                                        <td>
                                                            No Vehicles
                                                        </td>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
                                            <LayoutTemplate>
                                                <table id="Table4" runat="server">
                                                    <tr id="Tr2" runat="server" style="width: 290px;">
                                                        <td id="Td6" runat="server">
                                                            <table id="groupPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF;
                                                                border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;
                                                                font-family: Verdana, Arial, Helvetica, sans-serif;">
                                                                <tr id="groupPlaceholder" runat="server">
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </LayoutTemplate>
                                            <GroupTemplate>
                                                <tr id="itemPlaceholderContainer" runat="server">
                                                    <td id="itemPlaceholder" runat="server">
                                                    </td>
                                                </tr>
                                            </GroupTemplate>
                                        </asp:ListView>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="tmrGrid" EventName="Tick" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <cc1:ModalPopupExtender ID="lstQC_ModalPopupExtender" runat="server" DynamicServicePath=""
                                    BackgroundCssClass="modalBackground" Enabled="True" TargetControlID="updFIPnl2"
                                    PopupControlID="PopPnl7" CancelControlID="btnPnlClose7">
                                </cc1:ModalPopupExtender>
                                <asp:HiddenField ID="hfQC" runat="server" Value="Final Inspection" />
                                <asp:SqlDataSource ID="srcQC" runat="server" ConnectionString="<%$ ConnectionStrings:ProTRAC_ConnectionString %>"
                                    SelectCommand="GetVehiclePositionInfo" SelectCommandType="StoredProcedure">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="hfQC" DefaultValue="Final Inspection" Name="Position"
                                            PropertyName="Value" Type="String" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </div>
                        </div>
                    </td>
                    <td align="center" style="width: 25%;" valign="top">
                        <div id="Div7" runat="server">
                            <div class="panel1Teal">
                                <asp:UpdatePanel ID="updRTPnl1" runat="server">
                                    <ContentTemplate>
                                        <table class="fullStyle">
                                            <tr>
                                                <td>
                                                    Road Test
                                                </td>
                                                <td style="padding-right: 5px; width: 18px; height: 18px;">
                                                    <div class="countStyle">
                                                        <asp:Label ID="lblRTCount" runat="server">0</asp:Label>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="tmrGrid" EventName="Tick" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                            <div class="divblock" style="width: 100%;">
                                <asp:UpdatePanel ID="updRTPnl2" runat="server">
                                    <ContentTemplate>
                                        <asp:ListView ID="lstRT" runat="server" DataSourceID="srcRT" GroupItemCount="3" OnItemDataBound="lstRT_ItemDataBound">
                                            <EmptyItemTemplate>
                                            </EmptyItemTemplate>
                                            <ItemTemplate>
                                                <td id="Td5" runat="server" style="background-color: #c3d9ff; color: white; font-size: small;
                                                    width: 76px; height: 91px; background-repeat: no-repeat;" valign="top">
                                                    <div id="BackDiv" runat="server">
                                                        <center>
                                                            <div style="height: 60px; width: 100%">
                                                                <asp:Image ID="ModelImg" runat="server" Style="height: 60px; width: 100%;" />
                                                                <asp:HiddenField ID="hfModel" runat="server" Value='<%# Eval("VehicleModel") %>' />
                                                                <asp:Label ID="lblRefNo" runat="server" Text='<%# Eval("Slno") %>' ForeColor="#000066"
                                                                    Style="display: none;" />
                                                                <asp:Label ID="lblDev" runat="server" Text='<%# Eval("Deviation") %>' ForeColor="#000066"
                                                                    Style="display: none;" />
                                                                <asp:Label ID="lblPosition" runat="server" Text='<%# Eval("Position") %>' ForeColor="#000066"
                                                                    Style="display: none;" />
                                                            </div>
                                                            <div style="height: 52px; width: 100%">
                                                                <asp:Label ID="RegnoLabel" runat="server" Text='<%# Eval("Regno") %>' ForeColor="#000066" />
                                                                <br />
                                                                <asp:Label ID="InTimeLabel" runat="server" Text='<%# Eval("InTime") %>' ForeColor="Black" />
                                                            </div>
                                                        </center>
                                                    </div>
                                                </td>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <table id="Table3" runat="server" style="color: Black; height: 100%; font-weight: bold;
                                                    text-align: center; width: 100%; border-collapse: collapse; border-color: #999999;
                                                    border-style: none; border-width: 1px;">
                                                    <tr>
                                                        <td>
                                                            No Vehicles
                                                        </td>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
                                            <LayoutTemplate>
                                                <table id="Table4" runat="server">
                                                    <tr id="Tr2" runat="server" style="width: 290px;">
                                                        <td id="Td6" runat="server">
                                                            <table id="groupPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF;
                                                                border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;
                                                                font-family: Verdana, Arial, Helvetica, sans-serif;">
                                                                <tr id="groupPlaceholder" runat="server">
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </LayoutTemplate>
                                            <GroupTemplate>
                                                <tr id="itemPlaceholderContainer" runat="server">
                                                    <td id="itemPlaceholder" runat="server">
                                                    </td>
                                                </tr>
                                            </GroupTemplate>
                                        </asp:ListView>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="tmrGrid" EventName="Tick" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <cc1:ModalPopupExtender ID="lstRT_ModalPopupExtender" runat="server" DynamicServicePath=""
                                    BackgroundCssClass="modalBackground" Enabled="True" TargetControlID="updRTPnl2"
                                    PopupControlID="PopPnl8" CancelControlID="btnPnlClose8">
                                </cc1:ModalPopupExtender>
                                <asp:HiddenField ID="hfRT" runat="server" Value="RT" />
                                <asp:SqlDataSource ID="srcRT" runat="server" ConnectionString="<%$ ConnectionStrings:ProTRAC_ConnectionString %>"
                                    SelectCommand="GetVehiclePositionInfo" SelectCommandType="StoredProcedure">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="hfRT" DefaultValue="RT" Name="Position" PropertyName="Value"
                                            Type="String" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </div>
                        </div>
                    </td>
                    <td valign="top" align="center" style="width: 25%;">
                        <div id="Div1" runat="server">
                            <div class="panel1Green">
                                <asp:UpdatePanel ID="updVRPnl1" runat="server">
                                    <ContentTemplate>
                                        <table class="fullStyle">
                                            <tr>
                                                <td>
                                                    Vehicle Ready
                                                </td>
                                                <td style="padding-right: 5px; width: 18px; height: 18px;">
                                                    <div class="countStyle">
                                                        <asp:Label ID="lblVRCount" runat="server">0</asp:Label>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="tmrGrid" EventName="Tick" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                            <div class="divblock" style="width: 100%;">
                                <asp:UpdatePanel ID="updVRPnl2" runat="server">
                                    <ContentTemplate>
                                        <asp:ListView ID="lstCarReady" runat="server" DataSourceID="srcCarReady" GroupItemCount="3"
                                            OnItemDataBound="lstCarReady_ItemDataBound">
                                            <EmptyItemTemplate>
                                            </EmptyItemTemplate>
                                            <ItemTemplate>
                                                <td id="Td5" runat="server" style="background-color: #c3d9ff; color: white; font-size: small;
                                                    width: 76px; height: 91px; background-repeat: no-repeat;" valign="top">
                                                    <div id="BackDiv" runat="server">
                                                        <center>
                                                            <div style="height: 60px; width: 100%">
                                                                <asp:Image ID="ModelImg" runat="server" Style="height: 60px; width: 100%;" />
                                                                <asp:HiddenField ID="hfModel" runat="server" Value='<%# Eval("VehicleModel") %>' />
                                                                <asp:Label ID="lblRefNo" runat="server" Text='<%# Eval("Slno") %>' ForeColor="#000066"
                                                                    Style="display: none;" />
                                                                <asp:Label ID="lblDev" runat="server" Text='<%# Eval("Deviation") %>' ForeColor="#000066"
                                                                    Style="display: none;" />
                                                                <asp:Label ID="lblPosition" runat="server" Text='<%# Eval("Position") %>' ForeColor="#000066"
                                                                    Style="display: none;" />
                                                            </div>
                                                            <div style="height: 52px; width: 100%">
                                                                <asp:Label ID="RegnoLabel" runat="server" Text='<%# Eval("Regno") %>' ForeColor="#000066" />
                                                                <br />
                                                                <asp:Label ID="InTimeLabel" runat="server" Text='<%# Eval("InTime") %>' ForeColor="Black" />
                                                            </div>
                                                        </center>
                                                    </div>
                                                </td>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <table id="Table3" runat="server" style="color: Black; height: 100%; font-weight: bold;
                                                    text-align: center; width: 100%; border-collapse: collapse; border-color: #999999;
                                                    border-style: none; border-width: 1px;">
                                                    <tr>
                                                        <td>
                                                            No Vehicles
                                                        </td>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
                                            <LayoutTemplate>
                                                <table id="Table4" runat="server">
                                                    <tr id="Tr2" runat="server" style="width: 290px;">
                                                        <td id="Td6" runat="server">
                                                            <table id="groupPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF;
                                                                border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;
                                                                font-family: Verdana, Arial, Helvetica, sans-serif;">
                                                                <tr id="groupPlaceholder" runat="server">
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </LayoutTemplate>
                                            <GroupTemplate>
                                                <tr id="itemPlaceholderContainer" runat="server">
                                                    <td id="itemPlaceholder" runat="server">
                                                    </td>
                                                </tr>
                                            </GroupTemplate>
                                        </asp:ListView>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="tmrGrid" EventName="Tick" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <cc1:ModalPopupExtender ID="lstCarReady_ModalPopupExtender" runat="server" DynamicServicePath=""
                                    BackgroundCssClass="modalBackground" Enabled="True" TargetControlID="updVRPnl2"
                                    PopupControlID="PopPnl9" CancelControlID="btnPnlClose9">
                                </cc1:ModalPopupExtender>
                                <asp:HiddenField ID="hfCarReady" runat="server" Value="Vehicle Ready" />
                                <asp:SqlDataSource ID="srcCarReady" runat="server" ConnectionString="<%$ ConnectionStrings:ProTRAC_ConnectionString %>"
                                    SelectCommand="GetVehiclePositionInfo" SelectCommandType="StoredProcedure">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="hfCarReady" DefaultValue="Vehicle Ready" Name="Position"
                                            PropertyName="Value" Type="String" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:HiddenField ID="hfIdle" runat="server" Value="Vehicle Idle" />
                        <asp:SqlDataSource ID="srcIdle" runat="server" ConnectionString="<%$ ConnectionStrings:ProTRAC_ConnectionString %>"
                            SelectCommand="GetVehiclePositionInfo" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="hfIdle" DefaultValue="Vehicle Idle" Name="Position"
                                    PropertyName="Value" Type="String" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                        <div class="panel1Brown">
                            <asp:UpdatePanel ID="updVIPnl1" runat="server">
                                <ContentTemplate>
                                    <table class="fullStyle">
                                        <tr>
                                            <td>
                                                Vehicle Idle
                                            </td>
                                            <td style="padding-right: 5px; width: 18px; height: 18px;">
                                                <div class="countStyle">
                                                    <asp:Label ID="lblVIdleCount" runat="server">0</asp:Label>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="tmrGrid" EventName="Tick" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td valign="top" align="center" style="width: 25%;">
                        <div id="Div6" runat="server">
                            <div class="panel1Orange">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <table class="fullStyle">
                                            <tr>
                                                <td>
                                                    Waiting For Workshop
                                                </td>
                                                <td style="padding-right: 5px; width: 18px; height: 18px;">
                                                    <div class="countStyle">
                                                        <asp:Label ID="lbIdle1" runat="server">0</asp:Label>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="tmrGrid" EventName="Tick" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                            <div class="divblock" style="width: 100%; empty-cells: hide;">
                                <asp:UpdatePanel ID="upIdle1" runat="server">
                                    <ContentTemplate>
                                        <asp:ListView ID="lstIdle1" runat="server" DataSourceID="srcIdle1" GroupItemCount="3"
                                            OnItemDataBound="lstIdle_ItemDataBound">
                                            <EmptyItemTemplate>
                                            </EmptyItemTemplate>
                                            <ItemTemplate>
                                                <td id="Td5" runat="server" style="background-color: #c3d9ff; color: white; font-size: small;
                                                    width: 76px; height: 91px; background-repeat: no-repeat;" valign="top">
                                                    <div id="BackDiv" runat="server">
                                                        <center>
                                                            <div style="height: 60px; width: 100%">
                                                                <asp:Image ID="ModelImg" runat="server" Style="height: 60px; width: 100%;" />
                                                                <asp:HiddenField ID="hfModel" runat="server" Value='<%# Eval("VehicleModel") %>' />
                                                                <asp:Label ID="lblRefNo" runat="server" Text='<%# Eval("Slno") %>' ForeColor="#000066"
                                                                    Style="display: none;" />
                                                                <asp:Label ID="lblDev" runat="server" Text='<%# Eval("Deviation") %>' ForeColor="#000066"
                                                                    Style="display: none;" />
                                                                <asp:Label ID="lblPosition" runat="server" Text='<%# Eval("Position") %>' ForeColor="#000066"
                                                                    Style="display: none;" />
                                                            </div>
                                                            <div style="height: 52px; width: 100%">
                                                                <asp:Label ID="RegnoLabel" runat="server" Text='<%# Eval("Regno") %>' ForeColor="#000066" />
                                                                <br />
                                                                <asp:Label ID="InTimeLabel" runat="server" Text='<%# Eval("InTime") %>' ForeColor="Black" />
                                                            </div>
                                                        </center>
                                                    </div>
                                                </td>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <table id="Table3" runat="server" style="color: Black; height: 100%; font-weight: bold;
                                                    text-align: center; width: 100%; border-collapse: collapse; border-color: #999999;
                                                    border-style: none; border-width: 1px;">
                                                    <tr>
                                                        <td>
                                                            No Vehicles
                                                        </td>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
                                            <LayoutTemplate>
                                                <table id="Table4" runat="server">
                                                    <tr id="Tr2" runat="server" style="width: 290px;">
                                                        <td id="Td6" runat="server">
                                                            <table id="groupPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF;
                                                                border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;
                                                                font-family: Verdana, Arial, Helvetica, sans-serif;">
                                                                <tr id="groupPlaceholder" runat="server">
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </LayoutTemplate>
                                            <GroupTemplate>
                                                <tr id="itemPlaceholderContainer" runat="server">
                                                    <td id="itemPlaceholder" runat="server">
                                                    </td>
                                                </tr>
                                            </GroupTemplate>
                                        </asp:ListView>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="tmrGrid" EventName="Tick" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" DynamicServicePath=""
                                    BackgroundCssClass="modalBackground" Enabled="True" TargetControlID="upIdle1"
                                    PopupControlID="PopPnl10" CancelControlID="btnPnlClose10">
                                </cc1:ModalPopupExtender>
                            </div>
                        </div>
                        <asp:HiddenField ID="hfIdle1" runat="server" Value="Workshop" />
                        <asp:SqlDataSource ID="srcIdle1" runat="server" ConnectionString="<%$ ConnectionStrings:ProTRAC_ConnectionString %>"
                            SelectCommand="GetVehicleIdlePositionInfo" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="hfIdle1" DefaultValue="Workshop" Name="Position"
                                    PropertyName="Value" Type="String" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </td>
                    <td valign="top" align="center" style="width: 25%;">
                        <div id="Div8" runat="server">
                            <div class="panel1Violet">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <table class="fullStyle">
                                            <tr>
                                                <td>
                                                    Waiting For WA / VAS
                                                </td>
                                                <td style="padding-right: 5px; width: 18px; height: 18px;">
                                                    <div class="countStyle">
                                                        <asp:Label ID="lbIdle2" runat="server">0</asp:Label>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="tmrGrid" EventName="Tick" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                            <div class="divblock" style="width: 100%;">
                                <asp:UpdatePanel ID="upIdle2" runat="server">
                                    <ContentTemplate>
                                        <asp:ListView ID="lstIdle2" runat="server" DataSourceID="srcIdle2" GroupItemCount="3"
                                            OnItemDataBound="lstIdle_ItemDataBound">
                                            <EmptyItemTemplate>
                                            </EmptyItemTemplate>
                                            <ItemTemplate>
                                                <td id="Td5" runat="server" style="background-color: #c3d9ff; color: white; font-size: small;
                                                    width: 76px; height: 91px; background-repeat: no-repeat;" valign="top">
                                                    <div id="BackDiv" runat="server">
                                                        <center>
                                                            <div style="height: 60px; width: 100%">
                                                                <asp:Image ID="ModelImg" runat="server" Style="height: 60px; width: 100%;" />
                                                                <asp:HiddenField ID="hfModel" runat="server" Value='<%# Eval("VehicleModel") %>' />
                                                                <asp:Label ID="lblRefNo" runat="server" Text='<%# Eval("Slno") %>' ForeColor="#000066"
                                                                    Style="display: none;" />
                                                                <asp:Label ID="lblDev" runat="server" Text='<%# Eval("Deviation") %>' ForeColor="#000066"
                                                                    Style="display: none;" />
                                                                <asp:Label ID="lblPosition" runat="server" Text='<%# Eval("Position") %>' ForeColor="#000066"
                                                                    Style="display: none;" />
                                                            </div>
                                                            <div style="height: 52px; width: 100%">
                                                                <asp:Label ID="RegnoLabel" runat="server" Text='<%# Eval("Regno") %>' ForeColor="#000066" />
                                                                <br />
                                                                <asp:Label ID="InTimeLabel" runat="server" Text='<%# Eval("InTime") %>' ForeColor="Black" />
                                                            </div>
                                                        </center>
                                                    </div>
                                                </td>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <table id="Table3" runat="server" style="color: Black; height: 100%; font-weight: bold;
                                                    text-align: center; width: 100%; border-collapse: collapse; border-color: #999999;
                                                    border-style: none; border-width: 1px;">
                                                    <tr>
                                                        <td>
                                                            No Vehicles
                                                        </td>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
                                            <LayoutTemplate>
                                                <table id="Table4" runat="server">
                                                    <tr id="Tr2" runat="server" style="width: 290px;">
                                                        <td id="Td6" runat="server">
                                                            <table id="groupPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF;
                                                                border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;
                                                                font-family: Verdana, Arial, Helvetica, sans-serif;">
                                                                <tr id="groupPlaceholder" runat="server">
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </LayoutTemplate>
                                            <GroupTemplate>
                                                <tr id="itemPlaceholderContainer" runat="server">
                                                    <td id="itemPlaceholder" runat="server">
                                                    </td>
                                                </tr>
                                            </GroupTemplate>
                                        </asp:ListView>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="tmrGrid" EventName="Tick" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" DynamicServicePath=""
                                    BackgroundCssClass="modalBackground" Enabled="True" TargetControlID="upIdle2"
                                    PopupControlID="PopPnl11" CancelControlID="btnPnlClose11">
                                </cc1:ModalPopupExtender>
                            </div>
                        </div>
                        <asp:HiddenField ID="hfIdle2" runat="server" Value="Wheel Alignment" />
                        <asp:SqlDataSource ID="srcIdle2" runat="server" ConnectionString="<%$ ConnectionStrings:ProTRAC_ConnectionString %>"
                            SelectCommand="GetVehicleIdlePositionInfo" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="hfIdle2" DefaultValue="Wheel Alignment" Name="Position"
                                    PropertyName="Value" Type="String" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </td>
                    <td align="center" style="width: 25%;" valign="top">
                        <div id="Div9" runat="server">
                            <div class="panel1White">
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>
                                        <table class="fullStyle">
                                            <tr>
                                                <td>
                                                    Waiting For FI
                                                </td>
                                                <td style="padding-right: 5px; width: 18px; height: 18px;">
                                                    <div class="countStyle">
                                                        <asp:Label ID="lbIdle3" runat="server">0</asp:Label>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="tmrGrid" EventName="Tick" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                            <div class="divblock" style="width: 100%;">
                                <asp:UpdatePanel ID="upIdle3" runat="server">
                                    <ContentTemplate>
                                        <asp:ListView ID="lstIdle3" runat="server" DataSourceID="srcIdle3" GroupItemCount="3"
                                            OnItemDataBound="lstIdle_ItemDataBound">
                                            <EmptyItemTemplate>
                                            </EmptyItemTemplate>
                                            <ItemTemplate>
                                                <td id="Td5" runat="server" style="background-color: #c3d9ff; color: white; font-size: small;
                                                    width: 76px; height: 91px; background-repeat: no-repeat;" valign="top">
                                                    <div id="BackDiv" runat="server">
                                                        <center>
                                                            <div style="height: 60px; width: 100%">
                                                                <asp:Image ID="ModelImg" runat="server" Style="height: 60px; width: 100%;" />
                                                                <asp:HiddenField ID="hfModel" runat="server" Value='<%# Eval("VehicleModel") %>' />
                                                                <asp:Label ID="lblRefNo" runat="server" Text='<%# Eval("Slno") %>' ForeColor="#000066"
                                                                    Style="display: none;" />
                                                                <asp:Label ID="lblDev" runat="server" Text='<%# Eval("Deviation") %>' ForeColor="#000066"
                                                                    Style="display: none;" />
                                                                <asp:Label ID="lblPosition" runat="server" Text='<%# Eval("Position") %>' ForeColor="#000066"
                                                                    Style="display: none;" />
                                                            </div>
                                                            <div style="height: 52px; width: 100%">
                                                                <asp:Label ID="RegnoLabel" runat="server" Text='<%# Eval("Regno") %>' ForeColor="#000066" />
                                                                <br />
                                                                <asp:Label ID="InTimeLabel" runat="server" Text='<%# Eval("InTime") %>' ForeColor="Black" />
                                                            </div>
                                                        </center>
                                                    </div>
                                                </td>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <table id="Table3" runat="server" style="color: Black; height: 100%; font-weight: bold;
                                                    text-align: center; width: 100%; border-collapse: collapse; border-color: #999999;
                                                    border-style: none; border-width: 1px;">
                                                    <tr>
                                                        <td>
                                                            No Vehicles
                                                        </td>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
                                            <LayoutTemplate>
                                                <table id="Table4" runat="server">
                                                    <tr id="Tr2" runat="server" style="width: 290px;">
                                                        <td id="Td6" runat="server">
                                                            <table id="groupPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF;
                                                                border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;
                                                                font-family: Verdana, Arial, Helvetica, sans-serif;">
                                                                <tr id="groupPlaceholder" runat="server">
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </LayoutTemplate>
                                            <GroupTemplate>
                                                <tr id="itemPlaceholderContainer" runat="server">
                                                    <td id="itemPlaceholder" runat="server">
                                                    </td>
                                                </tr>
                                            </GroupTemplate>
                                        </asp:ListView>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="tmrGrid" EventName="Tick" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <cc1:ModalPopupExtender ID="ModalPopupExtender3" runat="server" DynamicServicePath=""
                                    BackgroundCssClass="modalBackground" Enabled="True" TargetControlID="upIdle3"
                                    PopupControlID="PopPnl12" CancelControlID="btnPnlClose12">
                                </cc1:ModalPopupExtender>
                            </div>
                        </div>
                        <asp:HiddenField ID="hfIdle3" runat="server" Value="Final Inspection" />
                        <asp:SqlDataSource ID="srcIdle3" runat="server" ConnectionString="<%$ ConnectionStrings:ProTRAC_ConnectionString %>"
                            SelectCommand="GetVehicleIdlePositionInfo" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="hfIdle3" DefaultValue="Final Inspection" Name="Position"
                                    PropertyName="Value" Type="String" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </td>
                    <td valign="top" align="center" style="width: 25%;">
                        <div id="Div10" runat="server">
                            <div class="panel1Yellow">
                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                    <ContentTemplate>
                                        <table class="fullStyle">
                                            <tr>
                                                <td>
                                                    Waiting For Wash
                                                </td>
                                                <td style="padding-right: 5px; width: 18px; height: 18px;">
                                                    <div class="countStyle">
                                                        <asp:Label ID="lbIdle4" runat="server">0</asp:Label>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="tmrGrid" EventName="Tick" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                            <div class="divblock" style="width: 100%;">
                                <asp:UpdatePanel ID="upIdle4" runat="server">
                                    <ContentTemplate>
                                        <asp:ListView ID="lstIdle4" runat="server" DataSourceID="srcIdle4" GroupItemCount="3"
                                            OnItemDataBound="lstIdle_ItemDataBound">
                                            <EmptyItemTemplate>
                                            </EmptyItemTemplate>
                                            <ItemTemplate>
                                                <td id="Td5" runat="server" style="background-color: #c3d9ff; color: white; font-size: small;
                                                    width: 76px; height: 91px; background-repeat: no-repeat;" valign="top">
                                                    <div id="BackDiv" runat="server">
                                                        <center>
                                                            <div style="height: 60px; width: 100%">
                                                                <asp:Image ID="ModelImg" runat="server" Style="height: 60px; width: 100%;" />
                                                                <asp:HiddenField ID="hfModel" runat="server" Value='<%# Eval("VehicleModel") %>' />
                                                                <asp:Label ID="lblRefNo" runat="server" Text='<%# Eval("Slno") %>' ForeColor="#000066"
                                                                    Style="display: none;" />
                                                                <asp:Label ID="lblDev" runat="server" Text='<%# Eval("Deviation") %>' ForeColor="#000066"
                                                                    Style="display: none;" />
                                                                <asp:Label ID="lblPosition" runat="server" Text='<%# Eval("Position") %>' ForeColor="#000066"
                                                                    Style="display: none;" />
                                                            </div>
                                                            <div style="height: 52px; width: 100%">
                                                                <asp:Label ID="RegnoLabel" runat="server" Text='<%# Eval("Regno") %>' ForeColor="#000066" />
                                                                <br />
                                                                <asp:Label ID="InTimeLabel" runat="server" Text='<%# Eval("InTime") %>' ForeColor="Black" />
                                                            </div>
                                                        </center>
                                                    </div>
                                                </td>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <table id="Table3" runat="server" style="color: Black; height: 100%; font-weight: bold;
                                                    text-align: center; width: 100%; border-collapse: collapse; border-color: #999999;
                                                    border-style: none; border-width: 1px;">
                                                    <tr>
                                                        <td>
                                                            No Vehicles
                                                        </td>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
                                            <LayoutTemplate>
                                                <table id="Table4" runat="server">
                                                    <tr id="Tr2" runat="server" style="width: 290px;">
                                                        <td id="Td6" runat="server">
                                                            <table id="groupPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF;
                                                                border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;
                                                                font-family: Verdana, Arial, Helvetica, sans-serif;">
                                                                <tr id="groupPlaceholder" runat="server">
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </LayoutTemplate>
                                            <GroupTemplate>
                                                <tr id="itemPlaceholderContainer" runat="server">
                                                    <td id="itemPlaceholder" runat="server">
                                                    </td>
                                                </tr>
                                            </GroupTemplate>
                                        </asp:ListView>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="tmrGrid" EventName="Tick" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <cc1:ModalPopupExtender ID="ModalPopupExtender4" runat="server" DynamicServicePath=""
                                    BackgroundCssClass="modalBackground" Enabled="True" TargetControlID="upIdle4"
                                    PopupControlID="PopPnl13" CancelControlID="btnPnlClose13">
                                </cc1:ModalPopupExtender>
                            </div>
                        </div>
                        <asp:HiddenField ID="hfIdle4" runat="server" Value="Wash" />
                        <asp:SqlDataSource ID="srcIdle4" runat="server" ConnectionString="<%$ ConnectionStrings:ProTRAC_ConnectionString %>"
                            SelectCommand="GetVehicleIdlePositionInfo" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="hfIdle4" DefaultValue="Wash" Name="Position" PropertyName="Value"
                                    Type="String" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </td>
                </tr>
            </table>
        </center>
    </div>
    <div>
        <asp:UpdatePanel ID="upnl" runat="server">
            <ContentTemplate>
                <asp:Panel ID="PopPnl1" runat="server" Style="height: 600px; width: 800px; background-color: #eeeeff;
                    border-radius: 1em; box-shadow: 2px 2px 2px #888; display: none; overflow: scroll;">
                    <div class="panel1Red">
                        <table class="fullStyle">
                            <tr>
                                <td style="text-align: center; font-weight: bold;">
                                    <asp:Label ID="lblStage" runat="server">Waiting</asp:Label>
                                </td>
                                <td align="right" style="width: 20px; padding-right: 3px;">
                                    <asp:ImageButton ID="btnPnlClose1" runat="server" ImageUrl="~/images/Gitter/fileclose.png"
                                        AlternateText="Close File" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <table class="fullStyle">
                        <tr valign="middle">
                            <td>
                                <asp:Panel ID="SelPnl1" runat="server" ScrollBars="Auto" Height="100%" HorizontalAlign="Center">
                                    <asp:ListView ID="ListView1" runat="server" DataSourceID="srcWaiting" GroupItemCount="10"
                                        OnItemDataBound="lstWaiting_ItemDataBound">
                                        <EmptyItemTemplate>
                                        </EmptyItemTemplate>
                                        <ItemTemplate>
                                            <td id="Td2" runat="server" style="background-color: White; color: white; font-size: small;
                                                width: 76px; height: 91px;" valign="top">
                                                <div id="BackDiv" runat="server">
                                                    <center>
                                                        <div style="height: 60px; width: 100%">
                                                            <asp:Image ID="ModelImg" runat="server" Style="height: 60px; width: 100%;" />
                                                            <asp:HiddenField ID="hfModel" runat="server" Value='<%# Eval("VehicleModel") %>' />
                                                            <asp:Label ID="lblRefNo" runat="server" Text='<%# Eval("Slno") %>' ForeColor="#000066"
                                                                Style="display: none;" />
                                                            <asp:Label ID="lblDev" runat="server" Text='<%# Eval("Deviation") %>' ForeColor="#000066"
                                                                Style="display: none;" />
                                                            <asp:Label ID="lblPosition" runat="server" Text='<%# Eval("Position") %>' ForeColor="#000066"
                                                                Style="display: none;" />
                                                        </div>
                                                        <div style="height: 52px; width: 100%">
                                                            <asp:Label ID="RegnoLabel" runat="server" Text='<%# Eval("Regno") %>' ForeColor="#000066" />
                                                            <br />
                                                            <asp:Label ID="InTimeLabel" runat="server" Text='<%# Eval("InTime") %>' ForeColor="Black" />
                                                        </div>
                                                    </center>
                                                </div>
                                            </td>
                                        </ItemTemplate>
                                        <EmptyDataTemplate>
                                            <table id="Table1" runat="server" style="color: Black; height: 100%; font-weight: bold;
                                                text-align: center; width: 100%; border-collapse: collapse; border-color: #999999;
                                                border-style: none; border-width: 1px;">
                                                <tr>
                                                    <td>
                                                        No Vehicles
                                                    </td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <table id="Table2" runat="server">
                                                <tr id="Tr1" runat="server" style="width: 290px;">
                                                    <td id="Td3" runat="server">
                                                        <table id="groupPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF;
                                                            border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;
                                                            font-family: Verdana, Arial, Helvetica, sans-serif;">
                                                            <tr id="groupPlaceholder" runat="server">
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </LayoutTemplate>
                                        <GroupTemplate>
                                            <tr id="itemPlaceholderContainer" runat="server">
                                                <td id="itemPlaceholder" runat="server">
                                                </td>
                                            </tr>
                                        </GroupTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="PopPnl2" runat="server" Style="height: 600px; width: 800px; background-color: #eeeeff;
                    border-radius: 1em; box-shadow: 2px 2px 2px #888; display: none; overflow: scroll;">
                    <div class="panel1Blue">
                        <table class="fullStyle">
                            <tr>
                                <td style="text-align: center; font-weight: bold;">
                                    <asp:Label ID="Label1" runat="server">Jobslip</asp:Label>
                                </td>
                                <td align="right" style="width: 20px; padding-right: 3px;">
                                    <asp:ImageButton ID="btnPnlClose2" runat="server" ImageUrl="~/images/Gitter/fileclose.png"
                                        AlternateText="Close File" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <table class="fullStyle">
                        <tr valign="middle">
                            <td>
                                <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto" Height="100%" HorizontalAlign="Center">
                                    <asp:ListView ID="ListView2" runat="server" DataSourceID="srcJobSlip" GroupItemCount="10"
                                        OnItemDataBound="lstJobSlip_ItemDataBound">
                                        <EmptyItemTemplate>
                                        </EmptyItemTemplate>
                                        <ItemTemplate>
                                            <td id="Td2" runat="server" style="background-color: White; color: white; font-size: small;
                                                width: 76px; height: 91px;" valign="top">
                                                <div id="BackDiv" runat="server">
                                                    <center>
                                                        <div style="height: 60px; width: 100%">
                                                            <asp:Image ID="ModelImg" runat="server" Style="height: 60px; width: 100%;" />
                                                            <asp:HiddenField ID="hfModel" runat="server" Value='<%# Eval("VehicleModel") %>' />
                                                            <asp:Label ID="lblRefNo" runat="server" Text='<%# Eval("Slno") %>' ForeColor="#000066"
                                                                Style="display: none;" />
                                                            <asp:Label ID="lblDev" runat="server" Text='<%# Eval("Deviation") %>' ForeColor="#000066"
                                                                Style="display: none;" />
                                                            <asp:Label ID="lblPosition" runat="server" Text='<%# Eval("Position") %>' ForeColor="#000066"
                                                                Style="display: none;" />
                                                        </div>
                                                        <div style="height: 52px; width: 100%">
                                                            <asp:Label ID="RegnoLabel" runat="server" Text='<%# Eval("Regno") %>' ForeColor="#000066" />
                                                            <br />
                                                            <asp:Label ID="InTimeLabel" runat="server" Text='<%# Eval("InTime") %>' ForeColor="Black" />
                                                        </div>
                                                    </center>
                                                </div>
                                            </td>
                                        </ItemTemplate>
                                        <EmptyDataTemplate>
                                            <table id="Table1" runat="server" style="color: Black; height: 100%; font-weight: bold;
                                                text-align: center; width: 100%; border-collapse: collapse; border-color: #999999;
                                                border-style: none; border-width: 1px;">
                                                <tr>
                                                    <td>
                                                        No Vehicles
                                                    </td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <table id="Table2" runat="server">
                                                <tr id="Tr1" runat="server" style="width: 290px;">
                                                    <td id="Td3" runat="server">
                                                        <table id="groupPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF;
                                                            border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;
                                                            font-family: Verdana, Arial, Helvetica, sans-serif;">
                                                            <tr id="groupPlaceholder" runat="server">
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </LayoutTemplate>
                                        <GroupTemplate>
                                            <tr id="itemPlaceholderContainer" runat="server">
                                                <td id="itemPlaceholder" runat="server">
                                                </td>
                                            </tr>
                                        </GroupTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="PopPnl3" runat="server" Style="height: 600px; width: 800px; background-color: #eeeeff;
                    border-radius: 1em; box-shadow: 2px 2px 2px #888; display: none; overflow: scroll;">
                    <div class="panel1Yellow">
                        <table class="fullStyle">
                            <tr>
                                <td style="text-align: center; font-weight: bold;">
                                    <asp:Label ID="Label2" runat="server">Wash</asp:Label>
                                </td>
                                <td align="right" style="width: 20px; padding-right: 3px;">
                                    <asp:ImageButton ID="btnPnlClose3" runat="server" ImageUrl="~/images/Gitter/fileclose.png"
                                        AlternateText="Close File" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <table class="fullStyle">
                        <tr valign="middle">
                            <td>
                                <asp:Panel ID="Panel3" runat="server" ScrollBars="Auto" Height="100%" HorizontalAlign="Center">
                                    <asp:ListView ID="ListView3" runat="server" DataSourceID="srcWash" GroupItemCount="10"
                                        OnItemDataBound="lstWash_ItemDataBound">
                                        <EmptyItemTemplate>
                                        </EmptyItemTemplate>
                                        <ItemTemplate>
                                            <td id="Td2" runat="server" style="background-color: White; color: white; font-size: small;
                                                width: 76px; height: 91px;" valign="top">
                                                <div id="BackDiv" runat="server">
                                                    <center>
                                                        <div style="height: 60px; width: 100%">
                                                            <asp:Image ID="ModelImg" runat="server" Style="height: 60px; width: 100%;" />
                                                            <asp:HiddenField ID="hfModel" runat="server" Value='<%# Eval("VehicleModel") %>' />
                                                            <asp:Label ID="lblRefNo" runat="server" Text='<%# Eval("Slno") %>' ForeColor="#000066"
                                                                Style="display: none;" />
                                                            <asp:Label ID="lblDev" runat="server" Text='<%# Eval("Deviation") %>' ForeColor="#000066"
                                                                Style="display: none;" />
                                                            <asp:Label ID="lblPosition" runat="server" Text='<%# Eval("Position") %>' ForeColor="#000066"
                                                                Style="display: none;" />
                                                        </div>
                                                        <div style="height: 52px; width: 100%">
                                                            <asp:Label ID="RegnoLabel" runat="server" Text='<%# Eval("Regno") %>' ForeColor="#000066" />
                                                            <br />
                                                            <asp:Label ID="InTimeLabel" runat="server" Text='<%# Eval("InTime") %>' ForeColor="Black" />
                                                        </div>
                                                    </center>
                                                </div>
                                            </td>
                                        </ItemTemplate>
                                        <EmptyDataTemplate>
                                            <table id="Table1" runat="server" style="color: Black; height: 100%; font-weight: bold;
                                                text-align: center; width: 100%; border-collapse: collapse; border-color: #999999;
                                                border-style: none; border-width: 1px;">
                                                <tr>
                                                    <td>
                                                        No Vehicles
                                                    </td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <table id="Table2" runat="server">
                                                <tr id="Tr1" runat="server" style="width: 290px;">
                                                    <td id="Td3" runat="server">
                                                        <table id="groupPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF;
                                                            border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;
                                                            font-family: Verdana, Arial, Helvetica, sans-serif;">
                                                            <tr id="groupPlaceholder" runat="server">
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </LayoutTemplate>
                                        <GroupTemplate>
                                            <tr id="itemPlaceholderContainer" runat="server">
                                                <td id="itemPlaceholder" runat="server">
                                                </td>
                                            </tr>
                                        </GroupTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="PopPnl4" runat="server" Style="height: 600px; width: 800px; background-color: #eeeeff;
                    border-radius: 1em; box-shadow: 2px 2px 2px #888; display: none; overflow: scroll;">
                    <div class="panel1Orange">
                        <table class="fullStyle">
                            <tr>
                                <td style="text-align: center; font-weight: bold;">
                                    <asp:Label ID="Label3" runat="server">Workshop</asp:Label>
                                </td>
                                <td align="right" style="width: 20px; padding-right: 3px;">
                                    <asp:ImageButton ID="btnPnlClose4" runat="server" ImageUrl="~/images/Gitter/fileclose.png"
                                        AlternateText="Close File" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <table class="fullStyle">
                        <tr valign="middle">
                            <td>
                                <asp:Panel ID="Panel4" runat="server" ScrollBars="Auto" Height="100%" HorizontalAlign="Center">
                                    <asp:ListView ID="ListView4" runat="server" DataSourceID="srcFloor" GroupItemCount="10"
                                        OnItemDataBound="lstFloor_ItemDataBound">
                                        <EmptyItemTemplate>
                                        </EmptyItemTemplate>
                                        <ItemTemplate>
                                            <td id="Td2" runat="server" style="background-color: White; color: white; font-size: small;
                                                width: 76px; height: 91px;" valign="top">
                                                <div id="BackDiv" runat="server">
                                                    <center>
                                                        <div style="height: 60px; width: 100%">
                                                            <asp:Image ID="ModelImg" runat="server" Style="height: 60px; width: 100%;" />
                                                            <asp:HiddenField ID="hfModel" runat="server" Value='<%# Eval("VehicleModel") %>' />
                                                            <asp:Label ID="lblRefNo" runat="server" Text='<%# Eval("Slno") %>' ForeColor="#000066"
                                                                Style="display: none;" />
                                                            <asp:Label ID="lblDev" runat="server" Text='<%# Eval("Deviation") %>' ForeColor="#000066"
                                                                Style="display: none;" />
                                                            <asp:Label ID="lblPosition" runat="server" Text='<%# Eval("Position") %>' ForeColor="#000066"
                                                                Style="display: none;" />
                                                        </div>
                                                        <div style="height: 52px; width: 100%">
                                                            <asp:Label ID="RegnoLabel" runat="server" Text='<%# Eval("Regno") %>' ForeColor="#000066" />
                                                            <br />
                                                            <asp:Label ID="InTimeLabel" runat="server" Text='<%# Eval("InTime") %>' ForeColor="Black" />
                                                        </div>
                                                    </center>
                                                </div>
                                            </td>
                                        </ItemTemplate>
                                        <EmptyDataTemplate>
                                            <table id="Table1" runat="server" style="color: Black; height: 100%; font-weight: bold;
                                                text-align: center; width: 100%; border-collapse: collapse; border-color: #999999;
                                                border-style: none; border-width: 1px;">
                                                <tr>
                                                    <td>
                                                        No Vehicles
                                                    </td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <table id="Table2" runat="server">
                                                <tr id="Tr1" runat="server" style="width: 290px;">
                                                    <td id="Td3" runat="server">
                                                        <table id="groupPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF;
                                                            border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;
                                                            font-family: Verdana, Arial, Helvetica, sans-serif;">
                                                            <tr id="groupPlaceholder" runat="server">
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </LayoutTemplate>
                                        <GroupTemplate>
                                            <tr id="itemPlaceholderContainer" runat="server">
                                                <td id="itemPlaceholder" runat="server">
                                                </td>
                                            </tr>
                                        </GroupTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="PopPnl5" runat="server" Style="height: 600px; width: 800px; background-color: #eeeeff;
                    border-radius: 1em; box-shadow: 2px 2px 2px #888; display: none; overflow: scroll;">
                    <div class="panel1Violet">
                        <table class="fullStyle">
                            <tr>
                                <td style="text-align: center; font-weight: bold;">
                                    <asp:Label ID="Label4" runat="server">Wheel Alignment / VAS</asp:Label>
                                </td>
                                <td align="right" style="width: 20px; padding-right: 3px;">
                                    <asp:ImageButton ID="btnPnlClose5" runat="server" ImageUrl="~/images/Gitter/fileclose.png"
                                        AlternateText="Close File" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <table class="fullStyle">
                        <tr valign="middle">
                            <td>
                                <asp:Panel ID="Panel5" runat="server" ScrollBars="Auto" Height="100%" HorizontalAlign="Center">
                                    <asp:ListView ID="ListView5" runat="server" DataSourceID="srcWA" GroupItemCount="10"
                                        OnItemDataBound="lstWA_ItemDataBound">
                                        <EmptyItemTemplate>
                                        </EmptyItemTemplate>
                                        <ItemTemplate>
                                            <td id="Td2" runat="server" style="background-color: White; color: white; font-size: small;
                                                width: 76px; height: 91px;" valign="top">
                                                <div id="BackDiv" runat="server">
                                                    <center>
                                                        <div style="height: 60px; width: 100%">
                                                            <asp:Image ID="ModelImg" runat="server" Style="height: 60px; width: 100%;" />
                                                            <asp:HiddenField ID="hfModel" runat="server" Value='<%# Eval("VehicleModel") %>' />
                                                            <asp:Label ID="lblRefNo" runat="server" Text='<%# Eval("Slno") %>' ForeColor="#000066"
                                                                Style="display: none;" />
                                                            <asp:Label ID="lblDev" runat="server" Text='<%# Eval("Deviation") %>' ForeColor="#000066"
                                                                Style="display: none;" />
                                                            <asp:Label ID="lblPosition" runat="server" Text='<%# Eval("Position") %>' ForeColor="#000066"
                                                                Style="display: none;" />
                                                        </div>
                                                        <div style="height: 52px; width: 100%">
                                                            <asp:Label ID="RegnoLabel" runat="server" Text='<%# Eval("Regno") %>' ForeColor="#000066" />
                                                            <br />
                                                            <asp:Label ID="InTimeLabel" runat="server" Text='<%# Eval("InTime") %>' ForeColor="Black" />
                                                        </div>
                                                    </center>
                                                </div>
                                            </td>
                                        </ItemTemplate>
                                        <EmptyDataTemplate>
                                            <table id="Table1" runat="server" style="color: Black; height: 100%; font-weight: bold;
                                                text-align: center; width: 100%; border-collapse: collapse; border-color: #999999;
                                                border-style: none; border-width: 1px;">
                                                <tr>
                                                    <td>
                                                        No Vehicles
                                                    </td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <table id="Table2" runat="server">
                                                <tr id="Tr1" runat="server" style="width: 290px;">
                                                    <td id="Td3" runat="server">
                                                        <table id="groupPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF;
                                                            border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;
                                                            font-family: Verdana, Arial, Helvetica, sans-serif;">
                                                            <tr id="groupPlaceholder" runat="server">
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </LayoutTemplate>
                                        <GroupTemplate>
                                            <tr id="itemPlaceholderContainer" runat="server">
                                                <td id="itemPlaceholder" runat="server">
                                                </td>
                                            </tr>
                                        </GroupTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="PopPnl7" runat="server" Style="height: 600px; width: 800px; background-color: #eeeeff;
                    border-radius: 1em; box-shadow: 2px 2px 2px #888; display: none; overflow: scroll;">
                    <div class="panel1White">
                        <table class="fullStyle">
                            <tr>
                                <td style="text-align: center; font-weight: bold;">
                                    <asp:Label ID="Label6" runat="server">Final Inspection</asp:Label>
                                </td>
                                <td align="right" style="width: 20px; padding-right: 3px;">
                                    <asp:ImageButton ID="btnPnlClose7" runat="server" ImageUrl="~/images/Gitter/fileclose.png"
                                        AlternateText="Close File" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <table class="fullStyle">
                        <tr valign="middle">
                            <td>
                                <asp:Panel ID="Panel7" runat="server" ScrollBars="Auto" Height="100%" HorizontalAlign="Center">
                                    <asp:ListView ID="ListView7" runat="server" DataSourceID="srcQC" GroupItemCount="10"
                                        OnItemDataBound="lstQC_ItemDataBound">
                                        <EmptyItemTemplate>
                                        </EmptyItemTemplate>
                                        <ItemTemplate>
                                            <td id="Td2" runat="server" style="background-color: White; color: white; font-size: small;
                                                width: 76px; height: 91px;" valign="top">
                                                <div id="BackDiv" runat="server">
                                                    <center>
                                                        <div style="height: 60px; width: 100%">
                                                            <asp:Image ID="ModelImg" runat="server" Style="height: 60px; width: 100%;" />
                                                            <asp:HiddenField ID="hfModel" runat="server" Value='<%# Eval("VehicleModel") %>' />
                                                            <asp:Label ID="lblRefNo" runat="server" Text='<%# Eval("Slno") %>' ForeColor="#000066"
                                                                Style="display: none;" />
                                                            <asp:Label ID="lblDev" runat="server" Text='<%# Eval("Deviation") %>' ForeColor="#000066"
                                                                Style="display: none;" />
                                                            <asp:Label ID="lblPosition" runat="server" Text='<%# Eval("Position") %>' ForeColor="#000066"
                                                                Style="display: none;" />
                                                        </div>
                                                        <div style="height: 52px; width: 100%">
                                                            <asp:Label ID="RegnoLabel" runat="server" Text='<%# Eval("Regno") %>' ForeColor="#000066" />
                                                            <br />
                                                            <asp:Label ID="InTimeLabel" runat="server" Text='<%# Eval("InTime") %>' ForeColor="Black" />
                                                        </div>
                                                    </center>
                                                </div>
                                            </td>
                                        </ItemTemplate>
                                        <EmptyDataTemplate>
                                            <table id="Table1" runat="server" style="color: Black; height: 100%; font-weight: bold;
                                                text-align: center; width: 100%; border-collapse: collapse; border-color: #999999;
                                                border-style: none; border-width: 1px;">
                                                <tr>
                                                    <td>
                                                        No Vehicles
                                                    </td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <table id="Table2" runat="server">
                                                <tr id="Tr1" runat="server" style="width: 290px;">
                                                    <td id="Td3" runat="server">
                                                        <table id="groupPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF;
                                                            border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;
                                                            font-family: Verdana, Arial, Helvetica, sans-serif;">
                                                            <tr id="groupPlaceholder" runat="server">
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </LayoutTemplate>
                                        <GroupTemplate>
                                            <tr id="itemPlaceholderContainer" runat="server">
                                                <td id="itemPlaceholder" runat="server">
                                                </td>
                                            </tr>
                                        </GroupTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="PopPnl8" runat="server" Style="height: 600px; width: 800px; background-color: #eeeeff;
                    border-radius: 1em; box-shadow: 2px 2px 2px #888; display: none; overflow: scroll;">
                    <div class="panel1Teal">
                        <table class="fullStyle">
                            <tr>
                                <td style="text-align: center; font-weight: bold;">
                                    <asp:Label ID="Label7" runat="server">Road Test</asp:Label>
                                </td>
                                <td align="right" style="width: 20px; padding-right: 3px;">
                                    <asp:ImageButton ID="btnPnlClose8" runat="server" ImageUrl="~/images/Gitter/fileclose.png"
                                        AlternateText="Close File" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <table class="fullStyle">
                        <tr valign="middle">
                            <td>
                                <asp:Panel ID="Panel8" runat="server" ScrollBars="Auto" Height="100%" HorizontalAlign="Center">
                                    <asp:ListView ID="ListView8" runat="server" DataSourceID="srcRT" GroupItemCount="10"
                                        OnItemDataBound="lstRT_ItemDataBound">
                                        <EmptyItemTemplate>
                                        </EmptyItemTemplate>
                                        <ItemTemplate>
                                            <td id="Td2" runat="server" style="background-color: White; color: white; font-size: small;
                                                width: 76px; height: 91px;" valign="top">
                                                <div id="BackDiv" runat="server">
                                                    <center>
                                                        <div style="height: 60px; width: 100%">
                                                            <asp:Image ID="ModelImg" runat="server" Style="height: 60px; width: 100%;" />
                                                            <asp:HiddenField ID="hfModel" runat="server" Value='<%# Eval("VehicleModel") %>' />
                                                            <asp:Label ID="lblRefNo" runat="server" Text='<%# Eval("Slno") %>' ForeColor="#000066"
                                                                Style="display: none;" />
                                                            <asp:Label ID="lblDev" runat="server" Text='<%# Eval("Deviation") %>' ForeColor="#000066"
                                                                Style="display: none;" />
                                                            <asp:Label ID="lblPosition" runat="server" Text='<%# Eval("Position") %>' ForeColor="#000066"
                                                                Style="display: none;" />
                                                        </div>
                                                        <div style="height: 52px; width: 100%">
                                                            <asp:Label ID="RegnoLabel" runat="server" Text='<%# Eval("Regno") %>' ForeColor="#000066" />
                                                            <br />
                                                            <asp:Label ID="InTimeLabel" runat="server" Text='<%# Eval("InTime") %>' ForeColor="Black" />
                                                        </div>
                                                    </center>
                                                </div>
                                            </td>
                                        </ItemTemplate>
                                        <EmptyDataTemplate>
                                            <table id="Table1" runat="server" style="color: Black; height: 100%; font-weight: bold;
                                                text-align: center; width: 100%; border-collapse: collapse; border-color: #999999;
                                                border-style: none; border-width: 1px;">
                                                <tr>
                                                    <td>
                                                        No Vehicles
                                                    </td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <table id="Table2" runat="server">
                                                <tr id="Tr1" runat="server" style="width: 290px;">
                                                    <td id="Td3" runat="server">
                                                        <table id="groupPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF;
                                                            border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;
                                                            font-family: Verdana, Arial, Helvetica, sans-serif;">
                                                            <tr id="groupPlaceholder" runat="server">
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </LayoutTemplate>
                                        <GroupTemplate>
                                            <tr id="itemPlaceholderContainer" runat="server">
                                                <td id="itemPlaceholder" runat="server">
                                                </td>
                                            </tr>
                                        </GroupTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="PopPnl9" runat="server" Style="height: 600px; width: 800px; background-color: #eeeeff;
                    border-radius: 1em; box-shadow: 2px 2px 2px #888; display: none; overflow: scroll;">
                    <div class="panel1Green">
                        <table class="fullStyle">
                            <tr>
                                <td style="text-align: center; font-weight: bold;">
                                    <asp:Label ID="Label8" runat="server">Vehicle Ready</asp:Label>
                                </td>
                                <td align="right" style="width: 20px; padding-right: 3px;">
                                    <asp:ImageButton ID="btnPnlClose9" runat="server" ImageUrl="~/images/Gitter/fileclose.png"
                                        AlternateText="Close File" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <table class="fullStyle">
                        <tr valign="middle">
                            <td>
                                <asp:Panel ID="Panel10" runat="server" ScrollBars="Auto" Height="100%" HorizontalAlign="Center">
                                    <asp:ListView ID="ListView9" runat="server" DataSourceID="srcCarReady" GroupItemCount="10"
                                        OnItemDataBound="lstCarReady_ItemDataBound">
                                        <EmptyItemTemplate>
                                        </EmptyItemTemplate>
                                        <ItemTemplate>
                                            <td id="Td2" runat="server" style="background-color: White; color: white; font-size: small;
                                                width: 76px; height: 91px;" valign="top">
                                                <div id="BackDiv" runat="server">
                                                    <center>
                                                        <div style="height: 60px; width: 100%">
                                                            <asp:Image ID="ModelImg" runat="server" Style="height: 60px; width: 100%;" />
                                                            <asp:HiddenField ID="hfModel" runat="server" Value='<%# Eval("VehicleModel") %>' />
                                                            <asp:Label ID="lblRefNo" runat="server" Text='<%# Eval("Slno") %>' ForeColor="#000066"
                                                                Style="display: none;" />
                                                            <asp:Label ID="lblDev" runat="server" Text='<%# Eval("Deviation") %>' ForeColor="#000066"
                                                                Style="display: none;" />
                                                            <asp:Label ID="lblPosition" runat="server" Text='<%# Eval("Position") %>' ForeColor="#000066"
                                                                Style="display: none;" />
                                                        </div>
                                                        <div style="height: 52px; width: 100%">
                                                            <asp:Label ID="RegnoLabel" runat="server" Text='<%# Eval("Regno") %>' ForeColor="#000066" />
                                                            <br />
                                                            <asp:Label ID="InTimeLabel" runat="server" Text='<%# Eval("InTime") %>' ForeColor="Black" />
                                                        </div>
                                                    </center>
                                                </div>
                                            </td>
                                        </ItemTemplate>
                                        <EmptyDataTemplate>
                                            <table id="Table1" runat="server" style="color: Black; height: 100%; font-weight: bold;
                                                text-align: center; width: 100%; border-collapse: collapse; border-color: #999999;
                                                border-style: none; border-width: 1px;">
                                                <tr>
                                                    <td>
                                                        No Vehicles
                                                    </td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <table id="Table2" runat="server">
                                                <tr id="Tr1" runat="server" style="width: 290px;">
                                                    <td id="Td3" runat="server">
                                                        <table id="groupPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF;
                                                            border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;
                                                            font-family: Verdana, Arial, Helvetica, sans-serif;">
                                                            <tr id="groupPlaceholder" runat="server">
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </LayoutTemplate>
                                        <GroupTemplate>
                                            <tr id="itemPlaceholderContainer" runat="server">
                                                <td id="itemPlaceholder" runat="server">
                                                </td>
                                            </tr>
                                        </GroupTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="PopPnl10" runat="server" Style="height: 600px; width: 800px; background-color: #eeeeff;
                    border-radius: 1em; box-shadow: 2px 2px 2px #888; display: none; overflow: scroll;">
                    <div class="panel1Brown">
                        <table class="fullStyle">
                            <tr>
                                <td style="text-align: center; font-weight: bold;">
                                    <asp:Label ID="Label5" runat="server">Idle Time (Waiting For Workshop)</asp:Label>
                                </td>
                                <td align="right" style="width: 20px; padding-right: 3px;">
                                    <asp:ImageButton ID="btnPnlClose10" runat="server" ImageUrl="~/images/Gitter/fileclose.png"
                                        AlternateText="Close File" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <table class="fullStyle">
                        <tr valign="middle">
                            <td>
                                <asp:Panel ID="Panel6" runat="server" ScrollBars="Auto" Height="100%" HorizontalAlign="Center">
                                    <asp:ListView ID="ListView10" runat="server" DataSourceID="srcIdle1" GroupItemCount="10"
                                        OnItemDataBound="lstIdle1_ItemDataBound">
                                        <EmptyItemTemplate>
                                        </EmptyItemTemplate>
                                        <ItemTemplate>
                                            <td id="Td5" runat="server" style="background-color: #c3d9ff; color: white; font-size: small;
                                                width: 76px; height: 91px; background-repeat: no-repeat;" valign="top">
                                                <div id="BackDiv" runat="server">
                                                    <center>
                                                        <div style="height: 60px; width: 100%">
                                                            <asp:Image ID="ModelImg" runat="server" Style="height: 60px; width: 100%;" />
                                                            <asp:HiddenField ID="hfModel1" runat="server" Value='<%# Eval("VehicleModel") %>' />
                                                            <asp:Label ID="lblRefNo" runat="server" Text='<%# Eval("Slno") %>' ForeColor="#000066"
                                                                Style="display: none;" />
                                                            <asp:Label ID="lblDev" runat="server" Text='<%# Eval("Deviation") %>' ForeColor="#000066"
                                                                Style="display: none;" />
                                                            <asp:Label ID="lblPosition" runat="server" Text='<%# Eval("Position") %>' ForeColor="#000066"
                                                                Style="display: none;" />
                                                        </div>
                                                        <div style="height: 52px; width: 100%">
                                                            <asp:Label ID="RegnoLabel" runat="server" Text='<%# Eval("Regno") %>' ForeColor="#000066" />
                                                            <br />
                                                            <asp:Label ID="InTimeLabel" runat="server" Text='<%# Eval("InTime") %>' ForeColor="Black" />
                                                        </div>
                                                    </center>
                                                </div>
                                            </td>
                                        </ItemTemplate>
                                        <EmptyDataTemplate>
                                            <table id="Table3" runat="server" style="color: Black; height: 100%; font-weight: bold;
                                                text-align: center; width: 100%; border-collapse: collapse; border-color: #999999;
                                                border-style: none; border-width: 1px;">
                                                <tr>
                                                    <td>
                                                        No Vehicles
                                                    </td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <table id="Table4" runat="server">
                                                <tr id="Tr2" runat="server" style="width: 290px;">
                                                    <td id="Td6" runat="server">
                                                        <table id="groupPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF;
                                                            border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;
                                                            font-family: Verdana, Arial, Helvetica, sans-serif;">
                                                            <tr id="groupPlaceholder" runat="server">
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </LayoutTemplate>
                                        <GroupTemplate>
                                            <tr id="itemPlaceholderContainer" runat="server">
                                                <td id="itemPlaceholder" runat="server">
                                                </td>
                                            </tr>
                                        </GroupTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="PopPnl11" runat="server" Style="height: 600px; width: 800px; background-color: #eeeeff;
                    border-radius: 1em; box-shadow: 2px 2px 2px #888; display: none; overflow: scroll;">
                    <div class="panel1Brown">
                        <table class="fullStyle">
                            <tr>
                                <td style="text-align: center; font-weight: bold;">
                                    <asp:Label ID="Label9" runat="server">Idle Time (Waiting For Wheel Alignment / VAS)</asp:Label>
                                </td>
                                <td align="right" style="width: 20px; padding-right: 3px;">
                                    <asp:ImageButton ID="btnPnlClose11" runat="server" ImageUrl="~/images/Gitter/fileclose.png"
                                        AlternateText="Close File" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <table class="fullStyle">
                        <tr valign="middle">
                            <td>
                                <asp:Panel ID="Panel9" runat="server" ScrollBars="Auto" Height="100%" HorizontalAlign="Center">
                                    <asp:ListView ID="ListView6" runat="server" DataSourceID="srcIdle2" GroupItemCount="10"
                                        OnItemDataBound="lstIdle2_ItemDataBound">
                                        <EmptyItemTemplate>
                                        </EmptyItemTemplate>
                                        <ItemTemplate>
                                            <td id="Td5" runat="server" style="background-color: #c3d9ff; color: white; font-size: small;
                                                width: 76px; height: 91px; background-repeat: no-repeat;" valign="top">
                                                <div id="BackDiv" runat="server">
                                                    <center>
                                                        <div style="height: 60px; width: 100%">
                                                            <asp:Image ID="ModelImg" runat="server" Style="height: 60px; width: 100%;" />
                                                            <asp:HiddenField ID="hfModel1" runat="server" Value='<%# Eval("VehicleModel") %>' />
                                                            <asp:Label ID="lblRefNo" runat="server" Text='<%# Eval("Slno") %>' ForeColor="#000066"
                                                                Style="display: none;" />
                                                            <asp:Label ID="lblDev" runat="server" Text='<%# Eval("Deviation") %>' ForeColor="#000066"
                                                                Style="display: none;" />
                                                            <asp:Label ID="lblPosition" runat="server" Text='<%# Eval("Position") %>' ForeColor="#000066"
                                                                Style="display: none;" />
                                                        </div>
                                                        <div style="height: 52px; width: 100%">
                                                            <asp:Label ID="RegnoLabel" runat="server" Text='<%# Eval("Regno") %>' ForeColor="#000066" />
                                                            <br />
                                                            <asp:Label ID="InTimeLabel" runat="server" Text='<%# Eval("InTime") %>' ForeColor="Black" />
                                                        </div>
                                                    </center>
                                                </div>
                                            </td>
                                        </ItemTemplate>
                                        <EmptyDataTemplate>
                                            <table id="Table3" runat="server" style="color: Black; height: 100%; font-weight: bold;
                                                text-align: center; width: 100%; border-collapse: collapse; border-color: #999999;
                                                border-style: none; border-width: 1px;">
                                                <tr>
                                                    <td>
                                                        No Vehicles
                                                    </td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <table id="Table4" runat="server">
                                                <tr id="Tr2" runat="server" style="width: 290px;">
                                                    <td id="Td6" runat="server">
                                                        <table id="groupPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF;
                                                            border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;
                                                            font-family: Verdana, Arial, Helvetica, sans-serif;">
                                                            <tr id="groupPlaceholder" runat="server">
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </LayoutTemplate>
                                        <GroupTemplate>
                                            <tr id="itemPlaceholderContainer" runat="server">
                                                <td id="itemPlaceholder" runat="server">
                                                </td>
                                            </tr>
                                        </GroupTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="PopPnl12" runat="server" Style="height: 600px; width: 800px; background-color: #eeeeff;
                    border-radius: 1em; box-shadow: 2px 2px 2px #888; display: none; overflow: scroll;">
                    <div class="panel1Brown">
                        <table class="fullStyle">
                            <tr>
                                <td style="text-align: center; font-weight: bold;">
                                    <asp:Label ID="Label10" runat="server">Idle Time (Waiting For Final Inspection)</asp:Label>
                                </td>
                                <td align="right" style="width: 20px; padding-right: 3px;">
                                    <asp:ImageButton ID="btnPnlClose12" runat="server" ImageUrl="~/images/Gitter/fileclose.png"
                                        AlternateText="Close File" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <table class="fullStyle">
                        <tr valign="middle">
                            <td>
                                <asp:Panel ID="Panel11" runat="server" ScrollBars="Auto" Height="100%" HorizontalAlign="Center">
                                    <asp:ListView ID="ListView11" runat="server" DataSourceID="srcIdle3" GroupItemCount="10"
                                        OnItemDataBound="lstIdle3_ItemDataBound">
                                        <EmptyItemTemplate>
                                        </EmptyItemTemplate>
                                        <ItemTemplate>
                                            <td id="Td5" runat="server" style="background-color: #c3d9ff; color: white; font-size: small;
                                                width: 76px; height: 91px; background-repeat: no-repeat;" valign="top">
                                                <div id="BackDiv" runat="server">
                                                    <center>
                                                        <div style="height: 60px; width: 100%">
                                                            <asp:Image ID="ModelImg" runat="server" Style="height: 60px; width: 100%;" />
                                                            <asp:HiddenField ID="hfModel1" runat="server" Value='<%# Eval("VehicleModel") %>' />
                                                            <asp:Label ID="lblRefNo" runat="server" Text='<%# Eval("Slno") %>' ForeColor="#000066"
                                                                Style="display: none;" />
                                                            <asp:Label ID="lblDev" runat="server" Text='<%# Eval("Deviation") %>' ForeColor="#000066"
                                                                Style="display: none;" />
                                                            <asp:Label ID="lblPosition" runat="server" Text='<%# Eval("Position") %>' ForeColor="#000066"
                                                                Style="display: none;" />
                                                        </div>
                                                        <div style="height: 52px; width: 100%">
                                                            <asp:Label ID="RegnoLabel" runat="server" Text='<%# Eval("Regno") %>' ForeColor="#000066" />
                                                            <br />
                                                            <asp:Label ID="InTimeLabel" runat="server" Text='<%# Eval("InTime") %>' ForeColor="Black" />
                                                        </div>
                                                    </center>
                                                </div>
                                            </td>
                                        </ItemTemplate>
                                        <EmptyDataTemplate>
                                            <table id="Table3" runat="server" style="color: Black; height: 100%; font-weight: bold;
                                                text-align: center; width: 100%; border-collapse: collapse; border-color: #999999;
                                                border-style: none; border-width: 1px;">
                                                <tr>
                                                    <td>
                                                        No Vehicles
                                                    </td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <table id="Table4" runat="server">
                                                <tr id="Tr2" runat="server" style="width: 290px;">
                                                    <td id="Td6" runat="server">
                                                        <table id="groupPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF;
                                                            border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;
                                                            font-family: Verdana, Arial, Helvetica, sans-serif;">
                                                            <tr id="groupPlaceholder" runat="server">
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </LayoutTemplate>
                                        <GroupTemplate>
                                            <tr id="itemPlaceholderContainer" runat="server">
                                                <td id="itemPlaceholder" runat="server">
                                                </td>
                                            </tr>
                                        </GroupTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="PopPnl13" runat="server" Style="height: 600px; width: 800px; background-color: #eeeeff;
                    border-radius: 1em; box-shadow: 2px 2px 2px #888; display: none; overflow: scroll;">
                    <div class="panel1Brown">
                        <table class="fullStyle">
                            <tr>
                                <td style="text-align: center; font-weight: bold;">
                                    <asp:Label ID="Label11" runat="server">Idle Time (Waiting For Wash)</asp:Label>
                                </td>
                                <td align="right" style="width: 20px; padding-right: 3px;">
                                    <asp:ImageButton ID="btnPnlClose13" runat="server" ImageUrl="~/images/Gitter/fileclose.png"
                                        AlternateText="Close File" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <table class="fullStyle">
                        <tr valign="middle">
                            <td>
                                <asp:Panel ID="Panel12" runat="server" ScrollBars="Auto" Height="100%" HorizontalAlign="Center">
                                    <asp:ListView ID="ListView12" runat="server" DataSourceID="srcIdle4" GroupItemCount="10"
                                        OnItemDataBound="lstIdle4_ItemDataBound">
                                        <EmptyItemTemplate>
                                        </EmptyItemTemplate>
                                        <ItemTemplate>
                                            <td id="Td5" runat="server" style="background-color: #c3d9ff; color: white; font-size: small;
                                                width: 76px; background-repeat: no-repeat;" valign="top">
                                                <div id="BackDiv" runat="server">
                                                    <center>
                                                        <div style="height: 60px; width: 100%">
                                                            <asp:Image ID="ModelImg" runat="server" Style="height: 60px; width: 100%;" />
                                                            <asp:HiddenField ID="hfModel1" runat="server" Value='<%# Eval("VehicleModel") %>' />
                                                            <asp:Label ID="lblRefNo" runat="server" Text='<%# Eval("Slno") %>' ForeColor="#000066"
                                                                Style="display: none;" />
                                                            <asp:Label ID="lblDev" runat="server" Text='<%# Eval("Deviation") %>' ForeColor="#000066"
                                                                Style="display: none;" />
                                                            <asp:Label ID="lblPosition" runat="server" Text='<%# Eval("Position") %>' ForeColor="#000066"
                                                                Style="display: none;" />
                                                        </div>
                                                        <div style="height: 52px; width: 100%">
                                                            <asp:Label ID="RegnoLabel" runat="server" Text='<%# Eval("Regno") %>' ForeColor="#000066" />
                                                            <br />
                                                            <asp:Label ID="InTimeLabel" runat="server" Text='<%# Eval("InTime") %>' ForeColor="Black" />
                                                        </div>
                                                    </center>
                                                </div>
                                            </td>
                                        </ItemTemplate>
                                        <EmptyDataTemplate>
                                            <table id="Table3" runat="server" style="color: Black; height: 100%; font-weight: bold;
                                                text-align: center; width: 100%; border-collapse: collapse; border-color: #999999;
                                                border-style: none; border-width: 1px;">
                                                <tr>
                                                    <td>
                                                        No Vehicles
                                                    </td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <table id="Table4" runat="server">
                                                <tr id="Tr2" runat="server" style="width: 290px;">
                                                    <td id="Td6" runat="server">
                                                        <table id="groupPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF;
                                                            border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;
                                                            font-family: Verdana, Arial, Helvetica, sans-serif;">
                                                            <tr id="groupPlaceholder" runat="server">
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </LayoutTemplate>
                                        <GroupTemplate>
                                            <tr id="itemPlaceholderContainer" runat="server">
                                                <td id="itemPlaceholder" runat="server">
                                                </td>
                                            </tr>
                                        </GroupTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="tmrGrid" EventName="Tick" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>