<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JobAllotment1.aspx.cs" Inherits="JobAllotment" EnableViewStateMac="false" EnableSessionState="True" EnableEventValidation="false" ValidateRequest="false" ViewStateEncryptionMode="Never"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Job Allotment</title>
    <link href="CSS/ProTRAC_Css.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="js/jquery-ui-1.8.1.custom.min.js" type="text/javascript"></script>
    <script src="JS/jquery-1.3.2.js" type="text/javascript"></script>
    <script src="JS/jquery.gritter.min.js" type="text/javascript"></script>
    <script src="JS/customgitter.js" type="text/javascript"></script>
    <link href="css/cupertino/jquery-ui-1.8.1.custom.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="js/jquery-ui-1.8.1.custom.min.js" type="text/javascript"></script>
    <script src="js/jquery.gritter.min.js" type="text/javascript"></script>
    <link href="css/jquery.gritter.css" rel="stylesheet" type="text/css" />
    <link href="css/jquery.gritter2.css" rel="stylesheet" type="text/css" />
    <script src="js/customgitter.js" type="text/javascript"></script>
    <script src="js/Tooltip.js" type="text/javascript"></script>
    <link href="CSS/ProTRAC_CssJCR.css" rel="stylesheet" type="text/css" />
    <link href="CSS/notify-osd.css" rel="stylesheet" type="text/css" />
    <script src="js/Tooltip.js" type="text/javascript"></script>
     <script type="text/javascript">
         function goSupport() {
             window.open('Complain.aspx');
         }

         function goHelp() {
             window.open('Help/index.htm');
         }
         function goAbout() {
             window.open('http://www.spintech.in', '_blank');
         }
    </script>
    <style type="text/css">
        .AutoComStyle2
        {
            width: 350px;
            height: 100px;
            overflow: scroll;
            font-size: 12px;
            background-color: #EFF3FB;
        }
        .modalBackground
        {
            opacity: 0.75;
            background-color: #000;
            height: 400px;
            y-overflow: scroll;
            z-index: 1000;
        }
        .HoverMenu
        {
            z-index: 20000;
        }
        .AutoComStyle3
        {
            width: 300px;
            font-size: 12px;
            font-family: Arial;
            font-weight: bold;
            background-color: #D1DDF1;
            text-decoration: blink;
        }
        .AutoCompStyle1
        {
            width: 300px;
            font-size: 12px;
            font-family: Arial;
            background-color: #EFF3FB;
        }
        .AutoComStyle6
        {
            width: 60px;
            height: 100px;
            overflow: scroll;
            font-size: 12px;
            background-color: #EFF3FB;
        }
        .AutoComStyle5
        {
            font-size: 12px;
            font-family: Arial;
            font-weight: bold;
            background-color: #D1DDF1;
            text-decoration: blink;
        }
        .AutoCompStyle4
        {
            font-size: 12px;
            font-family: Arial;
            background-color: #EFF3FB;
        }
        .style1
        {
            height: 22px;
        }
        .style8
        {
            width: 87px;
        }
    </style>
    <script type="text/javascript">
        function CheckDateEalier(sender, args) {
            if (sender._selectedDate < new Date()) {
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }
    </script>
    <script type="text/javascript">

        function stopRKey(evt) {
            var evt = (evt) ? evt : ((event) ? event : null);
            var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
            if ((evt.keyCode == 13) && (node.type == "text")) { return false; }
        }

        document.onkeypress = stopRKey;
    </script>
    <script type="text/javascript">
        function goSupport() {
            window.open('Complain.aspx');
        }

        function goHelp() {
            window.open('Help/index.htm');
        }
        function checkDate(sender, args) {
            if (sender._selectedDate < new Date()) {
                alert("You cannot select a day earlier than today!");
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }

        var t1;

        $(document).ready(
        function () {
            t1 = new ToolTip("a", true, 40);
        });
        $(function () {
            $(".tb").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "JobAllotment.aspx/LoadJobCodes",
                        data: "{ 'JobCode': '" + request.term + "' }",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataFilter: function (data) { return data; },
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    value: item.Email
                                }
                            }))
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert(textStatus);
                        }
                    });
                },
                minLength: 2
            });
        });
        function showDetails(e, Refno, allotId) {
            $.ajax({
                type: "POST",
                url: "JobAllotment.aspx/LoadJobDetails",
                data: "{'Refno':'" + Refno + "','allotId':'" + allotId + "'}",
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

        function NotifyMe(msg) {
            $.notify_osd.create({
                'text': msg,
                'sticky': true,
                'timeout': 3,
                'dismissable': true
            });
        }

        function DismissNotify() {
            $.notify_osd.dismiss();
        }

        function endRequestHandler(sender, args) {
            showDetails();
        }

        function pageLoad() {
            if (!Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack())
                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequestHandler);
        }

        function UpdateDetails() {
            $.ajax({
                type: "POST",
                url: "Notification.aspx/UpdateDetails",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    str = result.d;
                    if (str != '') {
                        DismissNotify();
                    }
                }
            }
      );
        }
    </script>
    <style type="text/css">
        table a
        {
            text-align: center;
            padding-right: 25%;
            padding-left: 25%;
            display: block;
            text-decoration: none;
            font-family: Consolas, Georgia;
            font-size: 13px;
            font-weight: bold;
            color: White;
        }
        table a:hover
        {
            color: Black;
        }
        .style10
        {
            width: 105px;
        }
        .style11
        {
            width: 71px;
        }
        .style12
        {
            width: 58px;
        }
        .style13
        {
            width: 30px;
        }
        .style15
        {
            width: 121px;
        }
        .style16
        {
            width: 60px;
        }
        .style17
        {
            width: 130px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <%--    NOTIFICATION PANEL  --%>
    <asp:UpdatePanel ID="updTime" runat="server">
        <ContentTemplate>
            <asp:Timer ID="NotificationTimer" runat="server" Interval="3200000">
            </asp:Timer>
        </ContentTemplate>
    </asp:UpdatePanel>
    <table style="position: fixed; width: 100%; height: 100%; top: 0px; right: 0px; bottom: 0px;
        left: 0px; background-color: #F5F5F5;" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td style="background-image: url('Layout/VTABS.png'); background-repeat: no-repeat;
                background-size: 100% 100%; height: 115px; width: 100%;">
                <table style="width: 100%; height: 100%;" border="0" cellpadding="0" cellspacing="0">
                    <tr style="height: 35px">
                        <td style="width: 29%" />
                        <td style="width: 5%" align="center" valign="middle">
                            <asp:ImageButton ID="btn_Back" runat="server" src="Layout/back.png" Width="16" Height="12"
                                OnClick="btn_Back_Click" />
                            <asp:ImageButton ID="btn_noBack" runat="server" src="Layout/noback.png" Width="16"
                                Height="12" Visible="False" />
                        </td>
                        <td style="width: 35%;" valign="middle">
                            <table style="width: 100%; height: 100%;" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 25%; text-align: center; vertical-align: middle;">
                                        <asp:LinkButton ID="btn_Support" runat="server" OnClientClick="goSupport()" Text="Support" />
                                    </td>
                                    <td style="width: 25%; text-align: center; vertical-align: middle;">
                                        <asp:LinkButton ID="btn_help" runat="server" Text="Help" />
                                    </td>
                                    <td style="width: 25%; text-align: center; vertical-align: middle;">
                                        <asp:LinkButton ID="btn_About" runat="server" OnClientClick="goAbout()" Text="About" />
                                    </td>
                                    <td style="width: 25%; text-align: center; vertical-align: middle;">
                                        <asp:LinkButton ID="btn_logout" runat="server" Text="Logout" OnClick="btn_logout_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 31%" />
                    </tr>
                    <tr>
                        <td colspan="4" />
                    </tr>
                    <tr style="height: 25px; color: #333333; font-family: Consolas, Georgia; font-size: 13px;
                        font-weight: bold;">
                        <td style="padding-left: 8px" colspan="3">
                            <asp:Label ID="lbScroll0" runat="server"></asp:Label>
                        </td>
                        <td style="padding-right: 8px; text-align: right;">
                            <asp:Label ID="lbl_CurrentPage" runat="server" />|
                            <asp:Label ID="lbl_LoginName" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 100%; width: 100%; text-align: left; vertical-align: top; font-family: Consolas, Georgia;
                font-size: 14px; font-weight: normal;">
                <table>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <span style="text-align: left;">
                                        <asp:Label ID="lblClock" runat="server" Visible="False"></asp:Label>
                                    </span>
                                    <asp:Timer ID="Timer1" runat="server" Interval="30000" OnTick="Timer1_Tick">
                                    </asp:Timer>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
                <asp:Label ID="lblMessage" runat="server" Style="font-family: Consolas, Georgia;
                    font-size: small; text-decoration: blink;"></asp:Label>
                <table width="100%" class="tblStyle">
                    <tr>
                        <td valign="top" width="300px">
                            <table cellpadding="0" cellspacing="3" width="300px" border="0">
                                <tr>
                                    <td>
                                        <tr>
                                            <td style="white-space: nowrap; color: #333333; vertical-align: middle; font-family: Consolas, Georgia;"
                                                bgcolor="WhiteSmoke" class="style11">
                                                Allot Date
                                            </td>
                                            <td class="style10" align="left">
                                                <asp:TextBox ID="tbAllotDate" Text="" runat="server" Width="90px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="tbAllotDate" runat="server"
                                                    OnClientDateSelectionChanged="CheckDateEalier">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td style="white-space: nowrap; color: #333333; vertical-align: middle; font-family: Consolas, Georgia;"
                                                bgcolor="WhiteSmoke" class="style10" width="70px">
                                                Shift
                                            </td>
                                            <td valign="middle" width="110px">
                                                <asp:DropDownList ID="ddlShift" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                                    DataSourceID="DS_Shift" DataTextField="Shift" DataValueField="ShiftID" OnSelectedIndexChanged="ddlShift_SelectedIndexChanged"
                                                    Width="110px" ToolTip="Shift" Font-Names="Consolas, Georgia">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="white-space: nowrap; color: #333333; vertical-align: middle; font-family: Consolas, Georgia;
                                                padding-left: 3px" bgcolor="WhiteSmoke" class="style10" width="60px">
                                                Team Lead
                                            </td>
                                            <td style="white-space: nowrap; color: #333333; vertical-align: middle;" bgcolor="WhiteSmoke"
                                                class="style10" width="110px">
                                                <asp:DropDownList ID="ddlTeam" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                                    DataSourceID="DS_TeamLead" DataTextField="EmpName" DataValueField="EmpId" OnSelectedIndexChanged="ddlTeam_SelectedIndexChanged"
                                                    Width="110px" ToolTip="Team-Lead" Font-Names="Consolas, Georgia">
                                                    <asp:ListItem Value="0">Team Lead</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="white-space: nowrap; vertical-align: middle;" bgcolor="WhiteSmoke" class="style1"
                                                valign="middle">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="white-space: nowrap; color: #333333; vertical-align: middle; font-family: Consolas, Georgia;"
                                                bgcolor="WhiteSmoke" class="style11">
                                                Bay
                                            </td>
                                            <td valign="middle" class="style10">
                                                <asp:DropDownList ID="ddlBay" runat="server" AppendDataBoundItems="True" DataTextField="BayName"
                                                    DataValueField="BayId" Width="95px" OnSelectedIndexChanged="ddlBay_SelectedIndexChanged"
                                                    AutoPostBack="True" Font-Names="Consolas, Georgia">
                                                    <asp:ListItem Value="0">Bay</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="white-space: nowrap; color: #333333; vertical-align: middle; font-family: Consolas, Georgia;"
                                                bgcolor="WhiteSmoke" class="style10" width="70px">
                                                Employee Type
                                            </td>
                                            <td valign="middle" width="110px">
                                                <asp:DropDownList ID="ddlEmpType" runat="server" AutoPostBack="True" Width="110px"
                                                    DataSourceID="EmpTypeNew" DataTextField="EmpType" DataValueField="TypeId" Font-Names="Consolas, Georgia"
                                                    OnSelectedIndexChanged="ddlEmpType_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:SqlDataSource ID="EmpTypeNew" runat="server" ConnectionString="<%$ ConnectionStrings:ProTRAC_ConnectionString %>"
                                                    SelectCommand="SELECT [TypeId], [EmpType] FROM [tblEmployeeType] WHERE ([Head] = @Head)">
                                                    <SelectParameters>
                                                        <asp:Parameter DefaultValue="Technician" Name="Head" Type="String" />
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                                            </td>
                                            <td style="white-space: nowrap; color: #333333; vertical-align: middle; font-family: Consolas, Georgia;
                                                padding-left: 3px;" bgcolor="WhiteSmoke" class="style10" width="60px">
                                                Technician
                                            </td>
                                            <td valign="middle" width="110px">
                                                <asp:DropDownList ID="cmbTechName" runat="server" Width="110px" AppendDataBoundItems="True"
                                                    AutoPostBack="True" Font-Names="Consolas, Georgia" OnSelectedIndexChanged="cmbTechName_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="ImageButton1" runat="server" AlternateText="Go" Height="16px"
                                                    ImageUrl="~/img/search_2.png" OnClick="ImageButton1_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="white-space: nowrap; color: #333333; vertical-align: middle; font-family: Consolas, Georgia;"
                                                bgcolor="WhiteSmoke" class="style11">
                                                Job Code
                                            </td>
                                            <td class="style10" valign="middle">
                                                <asp:TextBox ID="tbJobCode" runat="server" Width="90px" Text="" OnTextChanged="tbJobCode_TextChanged"
                                                    AutoPostBack="True"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="tbJobCode_AutoCompleteExtender" runat="server" CompletionInterval="1"
                                                    FirstRowSelected="true" DelimiterCharacters="" Enabled="True" ServiceMethod="GetCompletionList"
                                                    ViewStateMode="Disabled" CompletionListItemCssClass="AutoCompStyle4" CompletionListCssClass="AutoComStyle6"
                                                    CompletionListHighlightedItemCssClass="AutoComStyle5" MinimumPrefixLength="0"
                                                    TargetControlID="tbJobCode">
                                                </cc1:AutoCompleteExtender>
                                            </td>
                                            <td style="white-space: nowrap; color: #333333; vertical-align: middle; font-family: Consolas, Georgia;"
                                                bgcolor="WhiteSmoke" class="style10" width="70px">
                                                Job Desc
                                            </td>
                                            <td style="white-space: nowrap; vertical-align: middle;" bgcolor="WhiteSmoke" class="style10"
                                                width="100%" colspan="3">
                                                <asp:TextBox ID="tbJobDesc" runat="server" Width="99%" Text="" OnTextChanged="tbJobDesc_TextChanged"
                                                    AutoPostBack="True"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" CompletionInterval="1"
                                                    CompletionListItemCssClass="AutoCompStyle1" CompletionListCssClass="AutoComStyle2"
                                                    CompletionListHighlightedItemCssClass="AutoComStyle3" Enabled="True" ServiceMethod="GetCompletionList1"
                                                    MinimumPrefixLength="1" ViewStateMode="Disabled" TargetControlID="tbJobDesc">
                                                </cc1:AutoCompleteExtender>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="white-space: nowrap; color: #333333; vertical-align: middle; font-family: Consolas, Georgia;"
                                                bgcolor="WhiteSmoke" class="style11">
                                                Job Start time
                                            </td>
                                            <td class="style10">
                                                <asp:TextBox ID="txtInTime" runat="server" Width="90px"></asp:TextBox>
                                                <cc1:MaskedEditExtender ID="txtInTime_MaskedEditExtender" runat="server" AcceptAMPM="false"
                                                    MaskType="Time" Mask="99:99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                                    OnInvalidCssClass="MaskedEditError" ErrorTooltipEnabled="true" UserTimeFormat="None"
                                                    TargetControlID="txtInTime">
                                                </cc1:MaskedEditExtender>
                                                <cc1:MaskedEditValidator ID="mevStartTime" runat="server" ControlExtender="txtInTime_MaskedEditExtender"
                                                    ControlToValidate="txtInTime" IsValidEmpty="true" EmptyValueMessage="Time is required "
                                                    InvalidValueMessage="Time is invalid" Display="Dynamic" EmptyValueBlurredText="Time is required "
                                                    InvalidValueBlurredMessage="Invalid Time" ValidationGroup="MKE" />
                                            </td>
                                            <td style="white-space: nowrap; color: #333333; vertical-align: middle; font-family: Consolas, Georgia;"
                                                bgcolor="WhiteSmoke" class="style10" width="70px">
                                                Allot Time
                                            </td>
                                            <td valign="top" align="left" style="white-space: nowrap; color: #333333; vertical-align: middle;"
                                                bgcolor="WhiteSmoke" width="110px">
                                                <asp:TextBox ID="tbAllotTime" runat="server" Width="60px"></asp:TextBox>
                                                min.
                                            </td>
                                            <td style="white-space: nowrap; color: #333333; vertical-align: middle; font-family: Consolas, Georgia;
                                                padding-left: 3px" bgcolor="WhiteSmoke">
                                                Std. Time
                                            </td>
                                            <td style="border: 1px solid #DFDFDF; white-space: nowrap; color: #333333; vertical-align: middle;"
                                                bgcolor="WhiteSmoke" align="center" width="110px">
                                                <asp:Label ID="lblStdTime" runat="server"></asp:Label>
                                            </td>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="white-space: nowrap; color: #333333; vertical-align: middle; font-family: Consolas, Georgia;"
                                        bgcolor="WhiteSmoke" class="style11">
                                        Technician Time
                                    </td>
                                    <td valign="top" class="style10" align="left" style="border: 1px solid #DFDFDF; white-space: nowrap;"
                                        height="20px">
                                        <asp:Label ID="lbl_TechnicianTime" runat="server" Font-Bold="False" ForeColor="#333333"></asp:Label>
                                    </td>
                                    <td style="white-space: nowrap; color: #333333; vertical-align: middle; font-family: Consolas, Georgia;"
                                        bgcolor="WhiteSmoke" class="style10" width="70px" height="20px">
                                        Vehicle Time
                                    </td>
                                    <td valign="middle" align="center" style="border: 1px solid #DFDFDF; white-space: nowrap;
                                        color: #333333;" bgcolor="WhiteSmoke" width="110px" height="20px">
                                        <asp:Label ID="lbl_VehicleTime" runat="server" Font-Bold="False" ForeColor="#333333"></asp:Label>
                                    </td>
                                     <td style="white-space: nowrap; color: #333333; vertical-align: middle; font-family: Consolas, Georgia;"
                                        bgcolor="WhiteSmoke" class="style10" width="70px" height="20px">
                                        Current KMS
                                    </td>
                                    <td valign="middle" align="center" style="border: 1px solid #DFDFDF; white-space: nowrap;
                                        color: #333333;" bgcolor="WhiteSmoke" width="110px" height="20px">
                                        <asp:Label ID="lblKMS" runat="server" Font-Bold="False" ForeColor="#333333"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" height="40px">
                                        <asp:Button CssClass="button" ID="btnTechAssign" runat="server" Text="Assign" OnClick="btnMechAssign_Click" />
                                        <asp:Button CssClass="button" ID="btnTechUpdate" runat="server" Text="Re-Schedule"
                                            OnClick="btnMechUpdate_Click" Visible="False" Width="85px" />
                                        <asp:Button CssClass="button" ID="btnTechCancel" runat="server" Text="UnAssign" OnClick="btnMechCancel_Click"
                                            Visible="False" />
                                        <asp:Button CssClass="button" ID="btnTechClose" runat="server" Text="Close" OnClick="btnMechClose_Click"
                                            Visible="False" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        <asp:SqlDataSource ID="DS_RegTagList" runat="server" ConnectionString="<%$ ConnectionStrings:ProTRAC_ConnectionString %>"
                                            SelectCommand="SELECT Slno, ISNULL(RegNo, N'') AS [Reg No], RFID as [Tag No], JobCardNo as [RO No], ServiceType as [ST] FROM tblMaster WHERE (Delivered = 0) AND (Cancelation = 0) AND (ISNULL(RegNo, N'') &lt;&gt; N'') AND JobCardOutTime is NULL ORDER BY GateIn">
                                        </asp:SqlDataSource>
                                        <asp:SqlDataSource ID="DS_TeamLead" runat="server" ConnectionString="<%$ ConnectionStrings:ProTRAC_ConnectionString %>"
                                            SelectCommand="SelectTeamLead" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                        <asp:SqlDataSource ID="DS_Bay" runat="server" ConnectionString="<%$ ConnectionStrings:ProTRAC_ConnectionString %>"
                                            SelectCommand="GetBays" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                        <asp:SqlDataSource ID="DS_Shift" runat="server" ConnectionString="<%$ ConnectionStrings:ProTRAC_ConnectionString %>"
                                            SelectCommand="SELECT ShiftID, Shift FROM tblShift ORDER BY Shift"></asp:SqlDataSource>
                                    </td>
                                </tr>
                        </td>
                        <tr style="height: inherit; vertical-align: bottom;">
                            <td colspan="6">
                                <table border="0px" cellpadding="0px" cellspacing="0px" style="width: 100%; vertical-align: bottom;
                                    background-color: #FFFFFF;">
                                    <tr style="width: 100%; font-family: Consolas, Georgia;" valign="middle">
                                        <td align="left" bgcolor="WhiteSmoke" style="color: #333333; width: 50px;">
                                            <asp:Label ID="Label4" runat="server" Font-Bold="False" ForeColor="#333333" Text="Date"
                                                Visible="false"></asp:Label>
                                        </td>
                                        <td align="left" bgcolor="WhiteSmoke" style="font-weight: bold; color: #333333; width: 100%;">
                                            <asp:TextBox ID="txtDate" runat="server" AutoPostBack="True" MaxLength="10" ToolTip="In Date"
                                                Width="90px" Visible="false"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" Enabled="True"
                                                FilterMode="ValidChars" TargetControlID="txtDate" ValidChars="0123456789/">
                                            </cc1:FilteredTextBoxExtender>
                                            <cc1:CalendarExtender ID="CalendarExtender9" runat="server" Enabled="True" TargetControlID="txtDate">
                                            </cc1:CalendarExtender>
                                            <asp:ImageButton ID="btntimeLineRefresh" runat="server" AlternateText="Refresh" Height="20px"
                                                ImageUrl="~/images/refresh1.png" OnClick="btntimeLineRefresh_Click" Visible="false" />
                                        </td>
                                        <td align="left" bgcolor="WhiteSmoke" style="color: #333333; width: 150px; white-space: nowrap">
                                        </td>
                                        <td align="left" style="width: 20px" bgcolor="WhiteSmoke">
                                        </td>
                                        <td>
                                            <table bgcolor="WhiteSmoke">
                                                <tr>
                                                    <td id="emp" runat="server" style="width: 200px">
                                                        <asp:DropDownList ID="ddlEmpTypes" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                                            DataSourceID="DS_EmpTypes" DataTextField="EmpType" DataValueField="EmpType" OnSelectedIndexChanged="ddlEmpTypes_SelectedIndexChanged"
                                                            Style="width: 99%" Visible="False">
                                                            <asp:ListItem Value="All">Employee Type</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:SqlDataSource ID="DS_EmpTypes" runat="server" ConnectionString="<%$ ConnectionStrings:ProTRAC_ConnectionString %>"
                                                            SelectCommand="SELECT EmpType, Head FROM tblEmployeeType WHERE Head = 'Technician'">
                                                        </asp:SqlDataSource>
                                                    </td>
                                                    <td style="width: 70px;">
                                                        <asp:DropDownList ID="ddlBayList" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                                            DataSourceID="DS_BayList" DataTextField="BayName" DataValueField="BayId" OnSelectedIndexChanged="ddlBayList_SelectedIndexChanged"
                                                            Style="width: 99%" Visible="False">
                                                            <asp:ListItem Value="All">Bay</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:SqlDataSource ID="DS_BayList" runat="server" ConnectionString="<%$ ConnectionStrings:ProTRAC_ConnectionString %>"
                                                            SelectCommand="Select * from tblBay where Active=1"></asp:SqlDataSource>
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                </table>
            </td>
            <td valign="top" style="width: 100%;">
                <table style="width: 100%; height: 10px;" bgcolor="Silver">
                    <tr>
                        <td align="left" style="white-space: nowrap;" class="style17">
                            <asp:Label ID="Label1" runat="server" Text="Pending Vehicle" ForeColor="#333333"
                                Width="122px"></asp:Label>
                        </td>
                        <td class="style15">
                            <asp:Label ID="Label2" runat="server" Text="TagNo" ForeColor="#333333"></asp:Label>
                        </td>
                        <td class="style16">
                            <asp:Label ID="Label3" runat="server" Text="RegNo" ForeColor="#333333"></asp:Label>
                        </td>
                        <td align="right" style="width: 100%">
                        </td>
                    </tr>
                    <tr style="font-family: Consolas, Georgia">
                        <td class="style17">
                            <asp:Label ID="lblPendingCount" runat="server" BackColor="Silver" Width="90px" Font-Names="Consolas, Georgia"></asp:Label>
                        </td>
                        <td align="left" class="style16">
                            <asp:TextBox ID="txtSrchTag" runat="server" MaxLength="5" Width="60px"></asp:TextBox>
                        </td>
                        <td class="style16">
                            <asp:TextBox ID="txtSrchReg" runat="server" Width="75px" MaxLength="20" Font-Size="11px"></asp:TextBox>
                        </td>
                        <td style="width: 100%">
                            <asp:ImageButton ID="btnSrchReg" runat="server" AlternateText="Go" Height="16px"
                                ImageUrl="~/img/search_2.png" OnClick="btnSrchReg_Click" />
                        </td>
                    </tr>
                </table>
                <table style="background-color: WhiteSmoke; width: 100%; padding-left: 50px;" id="vehicleInfo"
                    runat="server">
                    <tr>
                        <td class="style8" align="left">
                            <asp:Label ID="lblRegNo" runat="server" ForeColor="#333333"></asp:Label>
                            <asp:Label ID="lblRefNo" runat="server" Style="display: none;"></asp:Label>
                        </td>
                        <td align="left" class="style13" width="120px" style="white-space: nowrap">
                            <asp:Label ID="lblTagNo" runat="server" ForeColor="#333333"></asp:Label>
                        </td>
                        <td align="left" class="style12">
                            <asp:Label ID="lblST" runat="server" ForeColor="#333333"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:Label ID="lblpdt" runat="server" ForeColor="#333333"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:Label ID="lblSA" runat="server" ForeColor="#333333"></asp:Label>
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="gdvRegTagList" runat="server" CellPadding="4" ForeColor="#333333"
                    AutoGenerateColumns="False" DataKeyNames="Slno" OnRowDataBound="gdvRegTagList_RowDataBound"
                    Width="100%" AllowPaging="True" OnSelectedIndexChanged="gdvRegTagList_SelectedIndexChanged"
                    PageSize="5" OnPageIndexChanging="gdvRegTagList_PageIndexChanging" Font-Names="Consolas, Georgia"
                    GridLines="None" Font-Size="12px">
                    <RowStyle BackColor="WhiteSmoke" HorizontalAlign="Left" Height="35px" />
                    <Columns>
                        <asp:CommandField ButtonType="Image" SelectImageUrl="~/images/gridSelect1.png" ShowSelectButton="True" />
                        <asp:BoundField DataField="Slno" HeaderText="Slno" InsertVisible="False" ReadOnly="True"
                            Visible="false" SortExpression="Slno" />
                        <asp:BoundField DataField="Reg No" HeaderText="Reg #" ReadOnly="True" SortExpression="Reg No" />
                        <asp:BoundField DataField="Tag No" HeaderText="Tag #" SortExpression="Tag No" />
                        <asp:BoundField DataField="ST" HeaderText="ST" SortExpression="ST" />
                        <asp:BoundField DataField="PDT" HeaderText="PDT" SortExpression="PDT">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SA" HeaderText="SA" SortExpression="SA">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CW" HeaderText="CW" SortExpression="CW" />
                        <asp:BoundField DataField="TLAttended" HeaderText="Team Lead" />
                        <asp:BoundField DataField="KMS" HeaderText="KMS" />
                    </Columns>
                    <FooterStyle BackColor="Silver" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="Silver" ForeColor="#333333" HorizontalAlign="Center" />
                    <EmptyDataTemplate>
                        <table class="fullStyle" style="height: 120px; color: red;">
                            <tr>
                                <td align="center">
                                    No Vehicles Found !
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <SelectedRowStyle BackColor="WhiteSmoke" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="Silver" Font-Bold="True" HorizontalAlign="Left" ForeColor="#333333"
                        Height="34px" />
                    <EditRowStyle BackColor="WhiteSmoke" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ProTRAC_ConnectionString %>"
                    SelectCommand="GetFloorList" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
            </td>
        </tr>
    </table>
    <br />
    <div style="width: 1024px; overflow: auto;">
        <asp:Panel runat="server" ID="timeLinePnl" Height="200px" CssClass="commonFont">
            <asp:GridView ID="timeLine" runat="server" CellPadding="4" ForeColor="#333333" OnRowDataBound="timeLine_RowDataBound">
                <RowStyle BackColor="WhiteSmoke" CssClass="timeLinetd" Wrap="False" />
                <FooterStyle BackColor="Silver" ForeColor="#333333" />
                <PagerStyle BackColor="Silver" ForeColor="#333333" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="WhiteSmoke" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="Silver" Font-Bold="True" ForeColor="#333333" CssClass="timeLinetd"
                    Wrap="False" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
            <br />
            <asp:GridView ID="timeLine_Bay" runat="server" CellPadding="4" ForeColor="#333333"
                OnRowDataBound="timeLine_Bay_RowDataBound">
                <RowStyle BackColor="WhiteSmoke" CssClass="timeLinetd" Wrap="False" Width="100%" />
                <FooterStyle BackColor="Silver" ForeColor="#333333" />
                <PagerStyle BackColor="Silver" ForeColor="#333333" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="WhiteSmoke" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="Silver" Font-Bold="True" ForeColor="#333333" CssClass="timeLinetd"
                    Wrap="False" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
        </asp:Panel>
    </div>
    <center>
        <p style="position: fixed; width: 100%; height: 5px; bottom: 0px; left: 0px; right: 0px;
            font-family: Consolas, Georgia; font-size: 11px;">
            <asp:Label ID="lblVersion" runat="server"></asp:Label>
        </p>
    </center>
    </form>
</body>
</html>