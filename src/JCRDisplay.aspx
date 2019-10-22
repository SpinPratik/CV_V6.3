<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JCRDisplay.aspx.cs" Inherits="JCRDisplay" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="Icons/ProTRAC Icon.ico" rel="shortcut icon" type="image/x-icon" />
    <title>Job Controller</title>
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
    <script src="js/notify-osd.js" type="text/javascript"></script>
    <script type="text/javascript">
        function FormWidth() {
            document.getElementById("lbl_width").value = screen.width;
        }
    </script>
    <style type="text/css">
        .ttipBodyVal
        {
            width: 100px;
            text-align: left;
        }
        .TimeLabel
        {
            width: 100px;
            height: 20px;
            position: fixed;
            right: 55px;
            top: 60px;
            z-index: 20000px;
            background-color: Transparent;
            font-family: Arial;
            font-size: small;
            font-weight: bold;
            color: #FFFFFF;
        }
        .modalBackground
        {
            background-color: #CCCCFF;
        }
        .tableCell
        {
            border-right-style: solid;
            border-right-color: White;
            border-right-width: thin;
        }
        .tableCellMiddle
        {
            border-right-style: solid;
            border-right-color: White;
            border-right-width: thin;
            text-align: center;
        }
        .SubTableTop
        {
            border-top-style: solid;
            border-top-color: White;
            border-top-width: thin;
        }
        .SubTableHeader
        {
            border-right-style: solid;
            border-right-color: White;
            border-right-width: thin;
            border-top-style: solid;
            border-top-color: White;
            border-top-width: thin;
        }
        #PopupPnl
        {
            background-color: #e0ecff;
            width: 390px;
            border-width: 3px;
            border-color: Black;
            border-style: solid;
        }
        table.stats
        {
            text-align: center;
            font-family: Verdana, Geneva, Arial, Helvetica, sans-serif;
            font-weight: normal;
            font-size: 11px;
            color: #fff;
            width: 280px;
            background-color: #666;
            border: 0px;
            border-collapse: collapse;
            border-spacing: 0px;
        }
        table.stats td
        {
            background-color: #CCC;
            color: #000;
            padding: 4px;
            text-align: left;
            border: 1px #fff solid;
        }
        table.stats td.hed
        {
            background-color: #666;
            color: #fff;
            padding: 4px;
            text-align: left;
            border-bottom: 2px #fff solid;
            font-size: 12px;
            font-weight: bold;
        }
        #GridDisplay td
        {
            border: solid 1px white;
        }
    </style>
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
    </style>
    <script type="text/javascript">

        function goSupport() {
            window.open('Complain.aspx');
        }

        function LoadGitter(userId, title, info, imageinfo, sticktype, sticktime, classname, StickNoteType) {
            try {
                switch (StickNoteType) {
                    case 'autofade':
                        var unique_id = $.gritter.add({
                            title: title,
                            text: info,
                            image: imageinfo,
                            sticky: false,
                            time: sticktime,
                            class_name: classname
                        });
                        break;
                    case 'sticky': var unique_id = $.gritter.add({
                        title: title,
                        text: info,
                        image: imageinfo,
                        sticky: true,
                        time: sticktime,
                        class_name: classname
                    });

                        break;
                    case 'noimage':
                        $.gritter.add({
                            title: title,
                            text: info,
                            sticky: true,
                            time: sticktime
                        });
                        break;
                    case 'callback':
                        $.gritter.add({
                            title: title,
                            text: info,
                            sticky: sticktype,
                            before_open: function () {
                            },
                            after_open: function (e) {
                            },
                            before_close: function (e) {
                            },
                            after_close: function () {
                                PageMethods.GetReply(userId, title, info, OnGitterLoadSuccess, OnGitterLoadFailed);
                            }
                        });
                        break;
                }

            } catch (Msg) {

            }

        }

        function OnGitterLoadSuccess(pushstring) {
            //    alert("Success:"+pushstring);
            //alert(pushstring);
        }

        function OnGitterLoadFailed() {
            //alert("Failure");
        }

        function displayGitter() {
            try {
                var unique_id = $.gritter.add({
                    title: 'title',
                    text: 'info',
                    image: 'imageinfo',
                    sticky: false,
                    time: 5000,
                    class_name: 'gritter-close'
                });

                $.gritter.add({
                    title: 'title',
                    text: 'info',
                    sticky: true,
                    time: 500
                });
                alert('Done');
            }
            catch (e) {
                alert(e);
            };
        };
    </script>
    <style type="text/css">
        div.htmltooltip
        {
            position: absolute;
            z-index: 1000;
            left: -1000px;
            top: -1000px;
            background: #272727;
            border: 10px solid black;
            color: white;
            padding: 3px;
            width: 250px;
        }
        p.MsoNormal
        {
            margin-top: 0in;
            margin-right: 0in;
            margin-bottom: 10.0pt;
            margin-left: 0in;
            line-height: 115%;
            font-size: 11.0pt;
            font-family: "Calibri" , "sans-serif";
        }
        .style2
        {
            width: 18px;
        }
        .style3
        {
            width: 55px;
        }
        .style4
        {
            height: 26px;
        }
        .style6
        {
            width: 124px;
        }
        .style7
        {
            width: 484px;
        }
        .style8
        {
            width: 98px;
        }
        .style10
        {
            width: 70px;
        }
        .button
        {
        }
        .style11
        {
            width: 120px;
        }
    </style>
    <script type="text/javascript">
        function WriteRemarks(MyId) {
            genericPopup(300, 150, 'Remarks', MyId);
        }

        function genericPopup(w, h, title, MyId) {
            var left = (screen.width / 2) - (w / 2);
            var top = (screen.height / 2) - (h / 2);
            var targetWin = window.open("DisplayPopup.aspx?Id=" + MyId, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
            targetWin.style.backgroundColor = "black";
        }

        function GetKeyPress() {
            /*alert("you Pressed " & event.KeyCode);*/
        }
        function GetSupport() {
            window.open('http://www.protrac.spintech.in');
        }
        function goHelp() {
            window.open('Help/index.htm');
        }
        function goAbout() {
            window.open('http://www.spintech.in', '_blank');
        }
    </script>
    <script language="javascript" type="text/javascript">
        var t1;

        $(document).ready(
    function () {
        t1 = new ToolTip("a", true, 40);
    });

        function showDetails(e, ServiceId, ProcessName) {
            $.ajax({
                type: "POST",
                url: "DisplayWorks.aspx/LoadInOutTime",
                data: "{'ServiceId':'" + ServiceId + "','ProcessName':'" + ProcessName + "'}",
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

        function showRegNoHover(e, RefNo) {

            $.ajax({
                type: "POST",
                url: "DisplayWorks.aspx/LoadRegNoHover",
                data: "{'RefNo':'" + RefNo + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    str = result.d;
                    t1.Show(e, str);
                },
                error: function (result) {
                    //                    alert('error');
                    t1.Show(e, result.status + ' ' + result.statusText);
                }
            }
      );
        }

        function showProcessInOut(e, RefNo, ProcessId, ProcessName) {
            $.ajax({
                type: "POST",
                url: "DisplayWorks.aspx/LoadProcessInOutTime",
                data: "{'RefNo':'" + RefNo + "','ProcessId':'" + ProcessId + "','ProcessName':'" + ProcessName + "'}",
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

        function showSubProcessInOut(e, RefNo, SubProcessId, SubProcessName) {
            $.ajax({
                type: "POST",
                url: "DisplayWorks.aspx/LoadSubProcessInOutTime",
                data: "{'RefNo':'" + RefNo + "','SubProcessId':'" + SubProcessId + "','SubProcessName':'" + SubProcessName + "'}",
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

        function showEmpInOut(e, Slno, EmpId, Tech) {
            $.ajax({
                type: "POST",
                url: "DisplayWorks.aspx/LoadEmployeeInOutTime",
                data: "{'Slno':'" + Slno + "','EmpId':'" + EmpId + "','Tech':'" + Tech + "'}",
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

        function showRemarks(e, RefNo) {
            $.ajax({
                type: "POST",
                url: "DisplayWorks.aspx/LoadRemarks",
                data: "{'RefNo':'" + RefNo + "'}",
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
    <%--NOTIFICATION--%>
    <script type="text/javascript">

        function NotifyMe(msg) {
            $.notify_osd.create({
                'text': msg,
                //'icon'        : ico,     // icon path, 48x48
                'sticky': true,                 // if true, timeout is ignored
                'timeout': 3,                     // disappears after 6 seconds
                'dismissable': true                   // can be dismissed manually
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

        function showDetails() {
            $.ajax({
                type: "POST",
                url: "Notification.aspx/NotificationDetails",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    str = result.d;
                    if (str != '')
                        NotifyMe(str);
                    else
                        DismissNotify();
                }
                //              error:function(result)
                //              {
                //                NotifyMe(result.status + ' ' + result.statusText);
                //              }
            }
      );
        }

        function UpdateDetails() {
            $.ajax({
                type: "POST",
                url: "Notification.aspx/UpdateDetails",
                //data: "{'ServiceId':'"+ServiceId+"','ProcessName':'"+ProcessName+"'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    str = result.d;
                    if (str != '') {
                        //NotifyMe('Noticed!');
                        DismissNotify();
                    }
                }
                //                      error:function(result)
                //                      {
                //                      NotifyMe(result.status + ' ' + result.statusText);
                //                      }
            }
      );
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
</head>
<body onload="showDetails()">
    <form id="form1" runat="server" style="left: 0px">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Timer ID="Timer3" runat="server" OnTick="Timer3_Tick">
    </asp:Timer>
    <%--    NOTIFICATION PANEL  --%>
    <asp:UpdatePanel ID="updTime" runat="server">
        <ContentTemplate>
            <asp:Timer ID="NotificationTimer" runat="server" Interval="10000" Enabled="True">
            </asp:Timer>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="a" class="ttipStyle" style="font-family: Arial; background-color: white;
        width: 155px; height: 40px; border: solid 1px gray; text-align: left;" align="center">
    </div>
    <table style="width: 100%; height: 100%; font-family: Consolas,Georgia; font-size: 13px;"
        border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td style="background-image: url('Layout/VTABS.png'); background-repeat: no-repeat;
                background-size: 100% 100%; height: 115px; width: 100%;">
                <table style="width: 100%; height: 100%;" border="0" cellpadding="0" cellspacing="0">
                    <tr style="height: 35px">
                        <td style="width: 29%" />
                        <td style="width: 5%" align="center" valign="middle">
                            <asp:ImageButton ID="btn_Back" runat="server" src="Layout/back.png" Width="16" Height="12"
                                OnClick="btn_Back_Click" />
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
                                    <%--  <td style="width: 20%; text-align: center; vertical-align: middle;">
                                        <asp:LinkButton ID="btnHelp" runat="server" Text="Info" Visible="false" />
                                    </td>--%>
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
                            <asp:Label ID="lbl_CurrentPage" runat="server" /><%--|
                            <asp:Label ID="lbl_LoginName" runat="server" />--%>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td align="left">
                            <a runat="server" id="imgType" style="border: 0px"></a>
                            <asp:Label ID="lblSyncTime" runat="server" Font-Bold="True" Font-Size="Small" Visible="False"></asp:Label>
                            <asp:Label ID="lblLoading" runat="server" Text=""></asp:Label>
                            <asp:Label ID="lblErrormsg" runat="server" Visible="False"></asp:Label>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblrefno" runat="server" Visible="False"></asp:Label>
                            <input id="lbl_width" type="hidden" runat="server" value="0" />
                            <div id="mainTab">
                                <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Height="100px"
                                    Width="100%">
                                    <cc1:TabPanel ID="TabPanel12" runat="server" HeaderText="Service/Process/Carry Foward Remarks">
                                        <HeaderTemplate>
                                            Service/Process/Carry Foward Remarks
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table class="tblStyle">
                                                <tr>
                                                    <td colspan="3">
                                                    </td>
                                                    <tr>
                                                        <td valign="top">
                                                            <asp:Label ID="lblspvc" runat="server" Text="Vehicle No"></asp:Label>
                                                        </td>
                                                        <td valign="top">
                                                            <asp:Label ID="lblspvehicleno" runat="server" BackColor="Silver" Width="120px"></asp:Label>
                                                            <asp:Label ID="lblservid" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="#663300"
                                                                Visible="False"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left;" class="style7">
                                                            <table style="text-align: left;">
                                                                <tr>
                                                                    <td valign="top">
                                                                        <asp:Label ID="lbltype" runat="server" Text="Type"></asp:Label>
                                                                    </td>
                                                                    <td valign="top">
                                                                        <asp:DropDownList ID="drpsptype" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpsptype_SelectedIndexChanged"
                                                                            TabIndex="30">
                                                                            <asp:ListItem Value="0">Service</asp:ListItem>
                                                                            <asp:ListItem Value="1">Process</asp:ListItem>
                                                                            <asp:ListItem Value="2">Carry Forward</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td valign="top">
                                                                        <asp:Label ID="lblspprocess" runat="server" Text="Process" Visible="False"></asp:Label>
                                                                    </td>
                                                                    <td valign="top">
                                                                        <asp:DropDownList ID="drpspprocess" runat="server" DataSourceID="SqlDataSource1"
                                                                            DataTextField="ShowProcessName" DataValueField="ProcessId" TabIndex="31" Visible="False">
                                                                        </asp:DropDownList>
                                                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ProTRAC_ConnectionString %>"
                                                                            SelectCommand="SELECT ShowProcessName, ProcessId FROM tblProcessList ORDER BY ProcessDefaultOrder">
                                                                        </asp:SqlDataSource>
                                                                    </td>
                                                                    <td valign="top" class="style8">
                                                                        <asp:Button CssClass="button" ID="btnspsave" runat="server" Text="Save" OnClick="btnspsave_Click"
                                                                            TabIndex="33" ToolTip="Save" ValidationGroup="a" />
                                                                        <asp:Button CssClass="button" ID="btnspcncl" runat="server" Text="X" OnClick="btnecncl_Click"
                                                                            TabIndex="34" ToolTip="Close" Width="53px" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                            </table>
                                            <table>
                                                <tr>
                                                    <td valign="top">
                                                        <asp:Label ID="lblrmrktyp" runat="server" Text="Remarks"></asp:Label>
                                                    </td>
                                                    <td valign="top">
                                                        <table>
                                                            <tr>
                                                                <td valign="top">
                                                                    <asp:DropDownList ID="ddlSRemarks" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSRemarks_SelectedIndexChanged"
                                                                        AppendDataBoundItems="True">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td valign="top">
                                                                    <asp:TextBox ID="txtspremarks" runat="server" MaxLength="100" TabIndex="32" TextMode="MultiLine"
                                                                        Width="150px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td valign="top" width="45">
                                                        <asp:Label ID="lblServiceAction" runat="server" Text="Action"></asp:Label>
                                                    </td>
                                                    <td valign="top">
                                                        <asp:TextBox ID="txtServiceAction" runat="server" MaxLength="250" TextMode="MultiLine"
                                                            Width="150px"></asp:TextBox>
                                                    </td>
                                                    <td valign="top" width="95">
                                                        <asp:Label ID="lblServiceRecom" runat="server" Text="Recomendation"></asp:Label>
                                                    </td>
                                                    <td valign="top">
                                                        <asp:TextBox ID="txtRecomendation" runat="server" MaxLength="250" TextMode="MultiLine"
                                                            Width="150px"></asp:TextBox>
                                                    </td>
                                                    <td valign="top">
                                                        &nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel ID="TabPanel10" runat="server" HeaderText="Revised PDT">
                                        <HeaderTemplate>
                                            Revised PDT
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table class="tblStyle">
                                                <tr>
                                                    <td colspan="2">
                                                    </td>
                                                    <tr>
                                                        <td valign="top">
                                                            <asp:Label ID="lblpdtveh" runat="server" Text="Vehicle No"></asp:Label>
                                                        </td>
                                                        <td class="myStyle" valign="top">
                                                            <asp:Label ID="lblpdtvehno" runat="server" BackColor="Silver" Width="80px"></asp:Label>
                                                        </td>
                                                        <td valign="top">
                                                            <asp:Label ID="lblPDT" runat="server" Text="PDT"></asp:Label>
                                                        </td>
                                                        <td class="myStyle" valign="top">
                                                            <asp:Label ID="lblpdtno" runat="server" BackColor="Silver" Width="80px"></asp:Label>
                                                        </td>
                                                        <td valign="top">
                                                            <asp:Label ID="lblRevPDT" runat="server" Text="Revised PDT"></asp:Label>
                                                        </td>
                                                        <td valign="top">
                                                            <asp:TextBox ID="txtRevPDT" runat="server" AutoPostBack="True" MaxLength="10" OnTextChanged="txtRevPDT_TextChanged"
                                                                TabIndex="23" Width="80px"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="calndrrPDt" runat="server" Enabled="True" TargetControlID="txtRevPDT">
                                                            </cc1:CalendarExtender>
                                                            <%--<asp:DropDownList ID="drprpdttime" runat="server" TabIndex="24">
                                                            </asp:DropDownList>--%>
                                                            <asp:TextBox ID="cmbHH" runat="server" Width="60px"></asp:TextBox>
                                                            <cc1:MaskedEditExtender ID="cmbHH_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                Enabled="True" ErrorTooltipEnabled="True" Mask="99:99" MaskType="Time" TargetControlID="cmbHH">
                                                            </cc1:MaskedEditExtender>
                                                            <cc1:MaskedEditValidator ID="mevStartTime" runat="server" ControlExtender="cmbHH_MaskedEditExtender"
                                                                ControlToValidate="cmbHH" Display="Dynamic" EmptyValueBlurredText="Time is required "
                                                                EmptyValueMessage="Time is required " ErrorMessage="mevStartTime" InvalidValueBlurredMessage="Invalid Time"
                                                                InvalidValueMessage="Time is invalid" ValidationGroup="MKE"></cc1:MaskedEditValidator><td
                                                                    align="left">
                                                                </td>
                                                            <td align="left" valign="top">
                                                                Reason
                                                            </td>
                                                            <td valign="top">
                                                                <asp:DropDownList ID="ddPDTRemarks" runat="server" AutoPostBack="True" AppendDataBoundItems="True"
                                                                    OnSelectedIndexChanged="ddPDTRemarks_SelectedIndexChanged" Width="150px">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td valign="top">
                                                                <asp:Button CssClass="button" ID="btnPDTsave" runat="server" OnClick="btnPDTsave_Click"
                                                                    TabIndex="25" Text="Save" ToolTip="Save" ValidationGroup="a" />
                                                                <asp:Button CssClass="button" ID="btnPDTcncl" runat="server" OnClick="btnecncl_Click"
                                                                    TabIndex="26" Text="X" ToolTip="Close" Width="63px" />
                                                            </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" valign="top">
                                                            Customer Informed :
                                                            <asp:RadioButton ID="rd_Yes" runat="server" GroupName="cinfo" Text="Yes" /><asp:RadioButton
                                                                ID="rd_No" runat="server" GroupName="cinfo" Text="No" />
                                                        </td>
                                                        <td align="left" valign="top">
                                                            <td valign="top">
                                                                &nbsp;&nbsp;
                                                            </td>
                                                            <td valign="top">
                                                                &nbsp;&nbsp;
                                                            </td>
                                                            <td valign="top">
                                                                &nbsp;&nbsp;
                                                            </td>
                                                            <td colspan="2" style="text-align: right;" valign="top">
                                                                <asp:TextBox ID="txtPDTComment" runat="server" MaxLength="100" TextMode="MultiLine"
                                                                    Width="150px"></asp:TextBox>
                                                            </td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">
                                                            &nbsp;&nbsp;
                                                        </td>
                                                        <td class="myStyle" colspan="8" valign="top">
                                                            &nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel ID="TabPanel6" runat="server" HeaderText="Vehicle Ready">
                                        <HeaderTemplate>
                                            Vehicle Ready
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table style="width: 358px">
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Label ID="lblenrno" runat="server" Visible="False"></asp:Label>
                                                        <asp:Label ID="lblslno" runat="server" Visible="False"></asp:Label>
                                                    </td>
                                                    <tr>
                                                        <td valign="top" class="style10">
                                                            <asp:Label ID="lblvehicle" runat="server" Text="Vehicle No"></asp:Label>
                                                        </td>
                                                        <td valign="top" class="style6">
                                                            <asp:Label ID="lblvehicleno" runat="server" BackColor="Silver" Width="120px"></asp:Label>&#160;&#160;
                                                        </td>
                                                        <td valign="top" align="left">
                                                            <asp:Button CssClass="button" ID="btnready" runat="server" Text="Ready" OnClick="btnready_Click"
                                                                TabIndex="9" ToolTip="Ready" ValidationGroup="a" />
                                                            <asp:Button CssClass="button" ID="btnecncl" runat="server" Text="X" OnClick="btnecncl_Click"
                                                                ToolTip="Close" Width="63px" />
                                                        </td>
                                                    </tr>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="Vehicle Cancelation" Visible="false">
                                        <HeaderTemplate>
                                            Vehicle Cancelation
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table class="tblStyle">
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Label ID="Label1" runat="server" Visible="False"></asp:Label>
                                                        <asp:Label ID="Label2" runat="server" Visible="False"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top">
                                                        <asp:Label ID="lb1" runat="server" Text="Vehicle No"></asp:Label>
                                                    </td>
                                                    <td valign="top">
                                                        <asp:Label ID="lblvcvehicleno" runat="server" BackColor="Silver" Width="120px"></asp:Label>&#160;&#160;
                                                    </td>
                                                    <td valign="top">
                                                        <asp:TextBox ID="txtcncl" runat="server" Height="50px" TextMode="MultiLine" Width="200px"></asp:TextBox>
                                                    </td>
                                                    <td valign="top">
                                                        <table>
                                                            <tr>
                                                                <td valign="top">
                                                                    <asp:Button CssClass="button" ID="ImageButton5" runat="server" Text="Cancel" OnClick="btnCancelation_Click"
                                                                        TabIndex="9" ToolTip="Cancelation" ValidationGroup="a" />
                                                                    <asp:Button CssClass="button" ID="ImageButton6" runat="server" Text="X" OnClick="btnecncl_Click"
                                                                        ToolTip="Close" Width="63px" />
                                                                </td>
                                                                <td>
                                                                    &nbsp;&nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top">
                                                        &nbsp;&nbsp;
                                                    </td>
                                                    <td colspan="2" valign="top">
                                                        &nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="Vehicle Out">
                                        <HeaderTemplate>
                                            Vehicle Out
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table class="tblStyle">
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Label ID="Label5" runat="server" Visible="False"></asp:Label>
                                                        <asp:Label ID="Label6" runat="server" Visible="False"></asp:Label>
                                                    </td>
                                                    <tr>
                                                        <td valign="top" align="left">
                                                            <asp:Label ID="Label7" runat="server" Text="Vehicle No"></asp:Label>
                                                        </td>
                                                        <td valign="top">
                                                            <asp:Label ID="lblvovehicleno" runat="server" BackColor="Silver" Width="120px"></asp:Label>
                                                            &#160;&#160;
                                                        </td>
                                                        <td colspan="2" valign="top">
                                                            &nbsp;&nbsp;
                                                        </td>
                                                        <td valign="top">
                                                            <table>
                                                                <tr>
                                                                    <td valign="top">
                                                                        <asp:Button CssClass="button" ID="ImageButton4" runat="server" Text="Vehicle Out"
                                                                            OnClick="btnOut_Click" TabIndex="9" ToolTip="Vehicle Out" ValidationGroup="a">
                                                                        </asp:Button>
                                                                        <asp:Button CssClass="button" ID="ImageButton8" runat="server" Text="X" OnClick="btnecncl_Click"
                                                                            ToolTip="Close" Width="63px"></asp:Button>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" align="left">
                                                            Remarks
                                                        </td>
                                                        <td colspan="2" valign="top">
                                                            <asp:DropDownList ID="ddVOutRemarks" runat="server" AutoPostBack="True" AppendDataBoundItems="True"
                                                                OnSelectedIndexChanged="ddVOutRemarks_SelectedIndexChanged" Width="150px">
                                                            </asp:DropDownList>
                                                            <td valign="top">
                                                                <asp:TextBox ID="txt_VORemarks" runat="server" MaxLength="100" TabIndex="32" TextMode="MultiLine"
                                                                    Width="150px"></asp:TextBox>
                                                            </td>
                                                        </td>
                                                    </tr>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="Update Registration No">
                                        <HeaderTemplate>
                                            Update Registration No
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table class="tblStyle" style="width: 500px;">
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Label ID="lblMessage1" runat="server" Font-Bold="False" CssClass="clsValidator"></asp:Label>
                                                    </td>
                                                    <asp:Label ID="Label3" runat="server" Visible="False"></asp:Label>
                                                    <asp:Label ID="Label4" runat="server" Visible="False"></asp:Label>
                                                    <tr>
                                                        <td style="width: 120px;">
                                                            <asp:Label ID="lblevehno1" runat="server" Text="Vehicle No"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txtevehno" runat="server" BackColor="Silver" Width="125px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="120px">
                                                            <asp:Label ID="lblenewvhno" runat="server" Text="New Vehicle No"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtenewvhno" runat="server" MaxLength="10"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="120px">
                                                        </td>
                                                        <td>
                                                            <asp:Button CssClass="button" ID="btneupd" runat="server" Text="Update" OnClick="btneupd_Click" />
                                                            <asp:Button CssClass="button" ID="btn_CloseRegUpd" runat="server" Text="X" OnClick="btnecncl_Click"
                                                                ToolTip="Close" Width="63px" />
                                                        </td>
                                                    </tr>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel ID="TabPanel4" runat="server" HeaderText="Update Customer Details">
                                        <HeaderTemplate>
                                            Update Customer Details
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table>
                                                <tr>
                                                    <td style="width: 120px;">
                                                        <asp:Label ID="Label15" runat="server" Text="Vehicle No"></asp:Label>
                                                    </td>
                                                    <td valign="middle">
                                                        <asp:Label ID="txtvehicle" runat="server" BackColor="Silver" Width="120px"></asp:Label>
                                                    </td>
                                                    <td valign="middle" colspan="4">
                                                        <asp:Button CssClass="button" ID="Button3" runat="server" Text="Update" OnClick="Button3_Click"
                                                            ValidationGroup="editcust" />
                                                        <asp:Button CssClass="button" ID="btn_CloseCustUpd" runat="server" Text="X" OnClick="btnecncl_Click"
                                                            ToolTip="Close" Width="63px" />
                                                    </td>
                                                    <tr>
                                                        <td valign="middle" class="style11">
                                                            <asp:Label ID="Label16" runat="server" Text="Customer Name"></asp:Label>
                                                        </td>
                                                        <td valign="middle" align="left" class="style12">
                                                            <asp:TextBox ID="txtCustName" runat="server" MaxLength="25" Width="150px"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtCustName"
                                                                ErrorMessage="*" ValidationGroup="editcust"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style11">
                                                            Mobile No
                                                        </td>
                                                        <td class="style12">
                                                            <asp:TextBox ID="txtmobile" runat="server" MaxLength="10" Width="150px"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtmobile"
                                                                CssClass="clsValidator" ErrorMessage="*" ValidationGroup="editcust"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 60px">
                                                            <asp:Label ID="Label22" runat="server" Text="Email ID"></asp:Label>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtemailid" runat="server" MaxLength="60" Width="150px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <%-- <tr>
                                        <td>
                                            &nbsp;&nbsp;
                                        </td>
                                        <td colspan="5">
                                            &nbsp;&nbsp;
                                        </td>
                                    </tr>--%>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Label ID="Label11" runat="server" CssClass="clsValidator" Font-Bold="False"></asp:Label>
                                                            <asp:Label ID="Label12" runat="server" Visible="False"></asp:Label>
                                                            <asp:Label ID="Label13" runat="server" Visible="False"></asp:Label>
                                                        </td>
                                                        <td>
                                                            &nbsp;&nbsp;
                                                        </td>
                                                        <td>
                                                            &nbsp;&nbsp;
                                                        </td>
                                                        <td>
                                                            &nbsp;&nbsp;
                                                        </td>
                                                        <td>
                                                            &nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel ID="TabPanel5" runat="server" HeaderText="Tag Updation">
                                        <HeaderTemplate>
                                            Tag Updation
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table class="tblStyle">
                                                <tr>
                                                    <td width="80px">
                                                        <asp:Label ID="lbvehl" runat="server" Text="Vehicle No"></asp:Label>
                                                    </td>
                                                    <td width="200">
                                                        <asp:Label ID="lblvehicleUPDTag" runat="server" BackColor="Silver" Width="120px"></asp:Label>
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:Button CssClass="button" ID="btnupdate" runat="server" OnClick="btnupdate_Click"
                                                            ValidationGroup="editcard" Text="Update" />
                                                        <asp:Button CssClass="button" ID="btn_CloseTagUpd0" runat="server" Text="X" OnClick="btnecncl_Click"
                                                            ToolTip="Close" Width="63px" />
                                                    </td>
                                                    <td>
                                                        &nbsp;&nbsp;
                                                    </td>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblcardno" runat="server" Text="Tag No"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txtcardno" runat="server" BackColor="Silver" Width="120px"></asp:Label>
                                                        </td>
                                                        <td width="100">
                                                            &nbsp;&nbsp;
                                                        </td>
                                                        <td>
                                                            &nbsp;&nbsp;
                                                        </td>
                                                        <td>
                                                            &nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblnewcrdno" runat="server" Text="New Tag No"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtnewcrdno" runat="server" MaxLength="5" Width="147px"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtnewcrdno"
                                                                CssClass="clsValidator" ErrorMessage="*" ValidationGroup="editcard"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                            &nbsp;&nbsp;
                                                        </td>
                                                        <td>
                                                            &nbsp;&nbsp;
                                                        </td>
                                                        <td>
                                                            &nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <asp:Label ID="Label8" runat="server" CssClass="clsValidator" Font-Bold="False"></asp:Label>
                                                            <asp:Label ID="lblref" runat="server" Visible="False"></asp:Label>
                                                        </td>
                                                        <td>
                                                            &nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel ID="TabPanel7" runat="server" HeaderText="RPDT Informed To Customer">
                                        <HeaderTemplate>
                                            RPDT Informed
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table class="tblStyle">
                                                <tr>
                                                    <td>
                                                        Vehicle No.
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblvehno" runat="server" BackColor="Silver" Width="100px"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Button CssClass="button" ID="btnRPDTUpd" runat="server" OnClick="btnRPDTUpd_Click"
                                                            TabIndex="25" Text="Save" ToolTip="Save" ValidationGroup="a" Width="58px" />
                                                        <asp:Button CssClass="button" ID="btnRPDTCancel" runat="server" OnClick="btnRPDTCancel_Click"
                                                            TabIndex="26" Text="X" ToolTip="Close" Width="63px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        RPDT Informed
                                                    </td>
                                                    <td>
                                                        <asp:RadioButton ID="rdo_Yes" runat="server" Text="Yes" />
                                                    </td>
                                                    <td>
                                                        &nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel ID="TabPanel8" runat="server" HeaderText="JCC Informed">
                                        <HeaderTemplate>
                                            JCC Informed
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table class="tblStyle">
                                                <tr>
                                                    <td class="style11">
                                                        Vehicle No.
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblJCCvehno" runat="server" BackColor="Silver" Width="80px"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Button CssClass="button" ID="btn_JCCUpdate" runat="server" Text="Save" OnClick="btn_JCCUpdate_Click"
                                                            TabIndex="25" ToolTip="Save" ValidationGroup="a" Width="58px" />
                                                        <asp:Button CssClass="button" ID="ImageButton7" runat="server" OnClick="btnRPDTCancel_Click"
                                                            TabIndex="26" Text="X" ToolTip="Close" Width="63px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style11">
                                                        JCC Informed
                                                    </td>
                                                    <td>
                                                        <asp:RadioButton ID="rdJC_Yes" runat="server" Text="Yes" />
                                                    </td>
                                                    <td>
                                                        &nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel ID="TabPanel9" runat="server" HeaderText="JCC Informed">
                                        <HeaderTemplate>
                                            Tag Cancelation
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table class="tblStyle">
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Label ID="Label14" runat="server" Visible="False"></asp:Label>
                                                        <asp:Label ID="Label17" runat="server" Visible="False"></asp:Label>
                                                    </td>
                                                    <tr>
                                                        <td valign="top">
                                                            &nbsp;
                                                            <asp:Label ID="Label18" runat="server" Text="Tag No"></asp:Label>
                                                        </td>
                                                        <td valign="top">
                                                            <asp:Label ID="lblTagNo" runat="server" BackColor="Silver" Width="120px"></asp:Label>&#160;&#160;
                                                        </td>
                                                        <td colspan="2" valign="top">
                                                            &nbsp;&nbsp;
                                                        </td>
                                                        <td valign="top">
                                                            <table>
                                                                <tr>
                                                                    <td valign="top">
                                                                        <asp:Button CssClass="button" ID="btnUpdateRFIDCancel" runat="server" OnClick="btnUpdateRFIDCancel_Click"
                                                                            ValidationGroup="aa1" Text="Cancel" />
                                                                        <asp:Button CssClass="button" ID="Button4" runat="server" Text="X" OnClick="btnecncl_Click"
                                                                            ToolTip="Close" Width="63px" />
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;&nbsp;
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">
                                                            Remarks
                                                        </td>
                                                        <td colspan="2" valign="top">
                                                            <asp:DropDownList ID="CmbCancelationRemarks" runat="server" Width="150px" AppendDataBoundItems="true"
                                                                AutoPostBack="True" Height="22px" OnSelectedIndexChanged="ddlCancelationRemarks_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td colspan="2" valign="top">
                                                            <asp:TextBox ID="txtCancelationRemark" runat="server" MaxLength="100" TextMode="MultiLine"
                                                                Width="150px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel ID="TabPanel11" runat="server" HeaderText="JCC Informed">
                                        <HeaderTemplate>
                                            Service Advisor
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table class="tblStyle">
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Label ID="lblSlNoSA" runat="server" Visible="False"></asp:Label>
                                                        <asp:Label ID="Label10" runat="server" Visible="False"></asp:Label>
                                                    </td>
                                                    <tr>
                                                        <td valign="top">
                                                            &nbsp;
                                                            <asp:Label ID="Label19" runat="server" Text="Vehicle No"></asp:Label>
                                                        </td>
                                                        <td valign="top">
                                                            <asp:Label ID="lblSARegNo" runat="server" BackColor="Silver" Width="120px"></asp:Label>&#160;&#160;
                                                        </td>
                                                        <td valign="top" class="style2">
                                                            &nbsp;&nbsp;
                                                        </td>
                                                        <td valign="top">
                                                            <table>
                                                                <tr>
                                                                    <td class="style3">
                                                                        &nbsp;&nbsp;
                                                                    </td>
                                                                    <td valign="top">
                                                                        <asp:Button CssClass="button" ID="btnSAUpdate" runat="server" ValidationGroup="aa1"
                                                                            Text="Update" OnClick="btnSAUpdate_Click" />
                                                                        <asp:Button CssClass="button" ID="Button2" runat="server" Text="X" OnClick="btnecncl_Click"
                                                                            ToolTip="Close" Width="63px" />
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;&nbsp;
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">
                                                            Service Advisor
                                                        </td>
                                                        <td colspan="2" valign="top">
                                                            <asp:DropDownList ID="ddlSAList" runat="server" Width="150px" AppendDataBoundItems="True"
                                                                Height="22px">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td valign="top">
                                                            <table>
                                                                <tr>
                                                                    <td valign="top">
                                                                        <asp:Label ID="Label20" runat="server" Text="Remarks"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                        <asp:TextBox ID="txtSARemarks" runat="server" TextMode="MultiLine" Width="164px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel ID="TabPanel13" runat="server" HeaderText="Vehicle Un Hold">
                                        <HeaderTemplate>
                                            Vehicle Un Hold
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table>
                                                <tr>
                                                    <td colspan="3">
                                                        <asp:Label ID="lblSlnoforHold" runat="server" Visible="False" Font-Names="Consolas,Georgia"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="middle" style="width: 50px">
                                                        <asp:Label ID="Label21" runat="server" Text="Tag No:" Font-Names="Consolas,Georgia"></asp:Label>
                                                    </td>
                                                    <td valign="middle" align="left">
                                                        <asp:Label ID="lblTagnoforHold" runat="server" BackColor="Silver" Width="70px" Font-Names="Consolas,Georgia"></asp:Label>
                                                    </td>
                                                    <td valign="middle" style="width: 80px;">
                                                        <asp:Label ID="Label23" runat="server" Text="Vehicle No:" Font-Names="Consolas,Georgia"></asp:Label>
                                                    </td>
                                                    <td valign="middle" align="left">
                                                        <asp:Label ID="lblVehnoforHold" runat="server" BackColor="Silver" Width="140px" Font-Names="Consolas,Georgia"></asp:Label>
                                                    </td>
                                                    <td valign="top">
                                                        <asp:Button CssClass="button" ID="btnHold" runat="server" ValidationGroup="aa1" Text="Un Hold"
                                                            OnClick="btnHold_Click" />
                                                        <asp:Button CssClass="button" ID="Button5" runat="server" Text="X" ToolTip="Close"
                                                            OnClick="btnecncl_Click" Width="63px" />
                                                    </td>
                                                    <td>
                                                        &nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                </cc1:TabContainer>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div id="miniTab" style="width: auto;">
                                <div style="text-align: center;">
                                    <table>
                                        <tr style="left: 0px">
                                            <td valign="top">
                                                <asp:RadioButtonList ID="rbType" runat="server" RepeatDirection="Horizontal" AutoPostBack="True"
                                                    OnSelectedIndexChanged="rbType_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True" Value="0">Today</asp:ListItem>
                                                    <asp:ListItem Value="1">Next Day</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td valign="top">
                                                <table id="UDeliveredTab" runat="server" style="text-align: right;" border="0">
                                                    <tr>
                                                        <td style="white-space: nowrap; font-weight: bold; color: #666666;">
                                                            Workshop Status :
                                                        </td>
                                                        <td style="white-space: nowrap; font-weight: bold; color: #666666;">
                                                            Total
                                                        </td>
                                                        <td width="25px" style="border-bottom-width: thick; border-width: medium; text-align: center;">
                                                            <asp:Label ID="lbUnDelivered" runat="server" Text="" ForeColor="Black" Font-Bold="true"></asp:Label>
                                                        </td>
                                                        <td width="10px" style="white-space: nowrap; color: Red; text-align: center;">
                                                            |
                                                        </td>
                                                        <td style="white-space: nowrap; font-weight: bold; color: #666666;">
                                                            <img alt="WIP" src="images/JCR/Status.png" width='20' height='20' />
                                                        </td>
                                                        <td style="white-space: nowrap; font-weight: bold; color: #666666;">
                                                            WIP
                                                        </td>
                                                        <td width="25px" style="border-bottom-width: thick; border-width: medium; text-align: center;">
                                                            <asp:Label ID="lbWIP" runat="server" Text="" ForeColor="Black" Font-Bold="true"></asp:Label>
                                                        </td>
                                                        <td width="10px" style="white-space: nowrap; color: Red; text-align: center;">
                                                            |
                                                        </td>
                                                        <td style="white-space: nowrap; font-weight: bold; color: #666666;">
                                                            <img alt="Ready" src="images/JCR/Status_Ready.png" width='20' height='20' />
                                                        </td>
                                                        <td style="white-space: nowrap; font-weight: bold; color: #666666;">
                                                            Ready
                                                        </td>
                                                        <td width="25px" style="border-bottom-width: thick; background-color: #99C68E; border-width: medium;
                                                            text-align: center;">
                                                            <asp:Label ID="lbReady" runat="server" Text="" ForeColor="Black" Font-Bold="true"></asp:Label>
                                                        </td>
                                                        <td width="10px" style="white-space: nowrap; color: Red; text-align: center;">
                                                            |
                                                        </td>
                                                        <td style="white-space: nowrap; font-weight: bold; color: #666666;">
                                                            <img alt="Idle" src="images/JCR/Status_IDLE.png" width='20' height='20' />
                                                        </td>
                                                        <td style="white-space: nowrap; font-weight: bold; color: #666666;">
                                                            Idle
                                                        </td>
                                                        <td width="25px" style="border-bottom-width: thick; background-color: Red; border-width: medium;
                                                            text-align: center;" bgcolor="#D72B00">
                                                            <asp:Label ID="lbIdle" runat="server" Text="" ForeColor="Black" Font-Bold="true"></asp:Label>
                                                        </td>
                                                        <td width="10px" style="white-space: nowrap; color: Red; text-align: center;">
                                                            |
                                                        </td>
                                                        <td style="white-space: nowrap; font-weight: bold; color: #666666;">
                                                            <img alt="Hold" src="images/JCR/Status_HOLD.png" width='20' height='20' />
                                                        </td>
                                                        <td style="white-space: nowrap; font-weight: bold; color: #666666;">
                                                            Hold
                                                        </td>
                                                        <td width="25px" style="border-bottom-width: thick; background-color: #688CD9; border-width: medium;
                                                            text-align: center;" bgcolor="#688CD9">
                                                            <asp:Label ID="lbHold" runat="server" Text="" ForeColor="Black" Font-Bold="true"></asp:Label>
                                                        </td>
                                                        <td width="10px" style="white-space: nowrap; color: Red; text-align: center;">
                                                            |
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="text-align: right; color: Red;">
                                                <asp:Label runat="server" ID="lblmsg"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="PnlDisplay">
                    <ProgressTemplate>
                        <img alt="" src="img/waitSpin.gif" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <div id="searchMenu">
                    <table class="tblStyle" cellspacing="0">
                        <tr>
                            <td valign="top" style="white-space: nowrap; vertical-align: top; color: #333333;
                                font-weight: bold; font-size: 12px; text-align: left;">
                            </td>
                            <td>
                                <asp:DropDownList ID="cmbCustType" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                    OnSelectedIndexChanged="cmbCustType_SelectedIndexChanged" TabIndex="1" Width="100px"
                                    Font-Names="Consolas, Georgia">
                                    <asp:ListItem>Customer Type</asp:ListItem>
                                    <asp:ListItem Value="0">General</asp:ListItem>
                                    <asp:ListItem Value="1">JDP</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="cmbServiceType" runat="server" AppendDataBoundItems="True"
                                    AutoPostBack="True" DataSourceID="srcServiceType" DataTextField="ServiceType"
                                    DataValueField="ServiceType" OnSelectedIndexChanged="cmbServiceType_SelectedIndexChanged"
                                    TabIndex="2" Width="100px" Font-Names="Consolas, Georgia">
                                    <asp:ListItem>Service Type</asp:ListItem>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="srcServiceType" runat="server" ConnectionString="<%$ ConnectionStrings:ProTRAC_ConnectionString %>"
                                    SelectCommand="SELECT [ServiceType] FROM [tblServiceTypes]"></asp:SqlDataSource>
                            </td>
                            <td>
                                <asp:DropDownList ID="cmbVehicleModel" runat="server" AppendDataBoundItems="True"
                                    AutoPostBack="True" DataSourceID="srcVehicleModel" DataTextField="Model" DataValueField="Model"
                                    OnSelectedIndexChanged="cmbVehicleModel_SelectedIndexChanged" TabIndex="3" Width="90px"
                                    Font-Names="Consolas, Georgia">
                                    <asp:ListItem>Model</asp:ListItem>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="srcVehicleModel" runat="server" ConnectionString="<%$ ConnectionStrings:ProTRAC_ConnectionString %>"
                                    SelectCommand="SELECT DISTINCT Model FROM tblVehicleModel"></asp:SqlDataSource>
                            </td>
                            <td>
                                <asp:DropDownList ID="cmbProcess" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                    DataSourceID="SqlDataSource2" DataTextField="ShowProcessName" DataValueField="ShowProcessName"
                                    OnSelectedIndexChanged="cmbProcess_SelectedIndexChanged" TabIndex="4" Width="90px"
                                    Font-Names="Consolas, Georgia">
                                    <asp:ListItem>Workshop</asp:ListItem>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ProTRAC_ConnectionString %>"
                                    SelectCommand="SELECT A.ShowProcessName FROM (SELECT ShowProcessName,Min(ProcessDefaultOrder) B FROM tblProcessList group BY ShowProcessName ) A order by A.B">
                                </asp:SqlDataSource>
                            </td>
                            <td>
                                <asp:DropDownList ID="cmbSA" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                    OnSelectedIndexChanged="cmbCustType_SelectedIndexChanged" TabIndex="1" Visible="True"
                                    Width="70px" Font-Names="Consolas, Georgia">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="cmbTeamLead" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                    OnSelectedIndexChanged="cmbTeamLead_SelectedIndexChanged" TabIndex="1" Visible="True"
                                    Width="70px" Font-Names="Consolas, Georgia">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlState" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                    Width="70px" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" Font-Names="Consolas, Georgia">
                                    <asp:ListItem Value="0">Status</asp:ListItem>
                                    <asp:ListItem Value="1">WIP</asp:ListItem>
                                    <asp:ListItem Value="2">Hold</asp:ListItem>
                                    <asp:ListItem Value="3">Idle</asp:ListItem>
                                    <asp:ListItem Value="4">Delay</asp:ListItem>
                                    <asp:ListItem Value="5">OnTime</asp:ListItem>
                                    <asp:ListItem Value="6">Vehicle Ready</asp:ListItem>
                                    <asp:ListItem Value="7">RPDT Informed</asp:ListItem>
                                    <asp:ListItem Value="8">RPDT Not Informed</asp:ListItem>
                                    <asp:ListItem Value="9">JCC Informed</asp:ListItem>
                                    <asp:ListItem Value="10">JCC Not Informed</asp:ListItem>
                                    <asp:ListItem Value="11">Customer Waiting</asp:ListItem>
                                    <asp:ListItem Value="12">JC Not Opened</asp:ListItem>
                                    <asp:ListItem Value="13">Same Day Delivery</asp:ListItem>
                                    <asp:ListItem Value="14">Previous Day Delivery</asp:ListItem>
                                    <asp:ListItem Value="15">Canceled</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <div style="border: solid 1px black;">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always">
                                        <ContentTemplate>
                                            <table>
                                                <tr>
                                                    <td class="style4">
                                                        <asp:TextBox ID="TxtDate1" runat="server" TabIndex="5" ValidationGroup="sg" Width="60px"></asp:TextBox>
                                                        <cc1:TextBoxWatermarkExtender ID="TxtDate1_TextBoxWatermarkExtender" runat="server"
                                                            Enabled="True" TargetControlID="TxtDate1" WatermarkText="From">
                                                        </cc1:TextBoxWatermarkExtender>
                                                        <cc1:CalendarExtender ID="TxtDate1_CalendarExtender" runat="server" Enabled="True"
                                                            TargetControlID="TxtDate1">
                                                        </cc1:CalendarExtender>
                                                    </td>
                                                    <td class="style4">
                                                        <asp:TextBox ID="TxtDate2" runat="server" TabIndex="6" Width="60px"></asp:TextBox>
                                                        <cc1:TextBoxWatermarkExtender ID="TxtDate2_TextBoxWatermarkExtender" runat="server"
                                                            Enabled="True" TargetControlID="TxtDate2" WatermarkText="To">
                                                        </cc1:TextBoxWatermarkExtender>
                                                        <cc1:CalendarExtender ID="TxtDate2_CalendarExtender" runat="server" Enabled="True"
                                                            TargetControlID="TxtDate2">
                                                        </cc1:CalendarExtender>
                                                    </td>
                                                    <td class="style4">
                                                        <asp:TextBox ID="txtVehicleNumber" runat="server" TabIndex="7" ValidationGroup="sg"
                                                            Width="60px"></asp:TextBox>
                                                        <cc1:TextBoxWatermarkExtender ID="txtVehicleNumber_TextBoxWatermarkExtender" runat="server"
                                                            Enabled="True" TargetControlID="txtVehicleNumber" WatermarkText="Reg No">
                                                        </cc1:TextBoxWatermarkExtender>
                                                    </td>
                                                    <td class="style4">
                                                        <asp:TextBox ID="txtTagNo" runat="server" TabIndex="7" ValidationGroup="sg" Width="60px"></asp:TextBox>
                                                        <cc1:TextBoxWatermarkExtender ID="txtTagNo_TextBoxWatermarkExtender" runat="server"
                                                            Enabled="True" TargetControlID="txtTagNo" WatermarkText="Tag No">
                                                        </cc1:TextBoxWatermarkExtender>
                                                    </td>
                                                    <td class="style4">
                                                        <asp:ImageButton ID="btnSearch" runat="server" AlternateText="SEARCH" Height="20px"
                                                            ImageUrl="~/Icons/srch1.jpeg" OnClick="btnSearch_Click" ToolTip="Search" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </td>
                            <td style="background-image: url('img/box.png'); background-repeat: no-repeat; background-position: center center;
                                text-align: center; vertical-align: central;padding-left:5px;" width="30">
                                <asp:Label ID="lbVCount" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="#333333"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr style="width: 100%; left: 0;">
            <td align="center" style="width: 100%; height: 100%; left: 0;" valign="top">
                <asp:UpdatePanel ID="PnlDisplay" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="120000">
                        </asp:Timer>
                        <table class="GridPanel" border="0" cellpadding="0">
                            <tr>
                                <td style="width: 100%">
                                    <asp:Panel ID="Panel3" runat="server" CssClass="GridPanelHeader">
                                        <asp:GridView ID="GridView1" runat="server" Width="100%" CellPadding="0" ForeColor="#507CD1"
                                            OnRowDataBound="grdDisplay_RowDataBound" OnPageIndexChanging="grdDisplay_PageIndexChanging"
                                            OnSelectedIndexChanged="grdDisplay_SelectedIndexChanged" ShowHeader="True" Height="45px"
                                            GridLines="None">
                                            <EmptyDataTemplate>
                                                <center>
                                                    <div class="NoData">
                                                    </div>
                                                </center>
                                            </EmptyDataTemplate>
                                            <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" />
                                            <EmptyDataRowStyle Height="48px" />
                                            <FooterStyle BackColor="Silver" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <HeaderStyle BackColor="#CC3333" Font-Bold="True" ForeColor="WHITE" HorizontalAlign="Center" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <AlternatingRowStyle BackColor="White" />
                                        </asp:GridView>
                                        <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:ProTRAC_ConnectionString%>"
                                            SelectCommand="DisplayWorks" SelectCommandType="StoredProcedure">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="rbType" DefaultValue="0" Name="DayToPrev" PropertyName="SelectedValue"
                                                    Type="Int32" />
                                                <asp:ControlParameter ControlID="txtVehicleNumber" DefaultValue="" Name="VehicleNumber"
                                                    PropertyName="Text" Type="String" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </asp:Panel>
                                </td>
                                <td>
                                    <asp:Label ID="Label9" runat="server" Text="" Height="25" Width="14"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Panel ID="Panel51" runat="server" CssClass="GridPanel" ScrollBars="Vertical">
                                        <asp:GridView ID="grdDisplay" runat="server" Width="100%" CellPadding="0" ForeColor="#333333"
                                            OnRowDataBound="grdDisplay_RowDataBound" OnPageIndexChanging="grdDisplay_PageIndexChanging"
                                            OnSelectedIndexChanged="grdDisplay_SelectedIndexChanged" ShowHeader="False" GridLines="Horizontal">
                                            <EmptyDataTemplate>
                                                <center>
                                                    <div class="NoData">
                                                        No Vehicle Received</div>
                                                </center>
                                            </EmptyDataTemplate>
                                            <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Height="22px"
                                                HorizontalAlign="Right" />
                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle HorizontalAlign="Center" BackColor="#F5F5F5" ForeColor="#333333" Height="35" />
                                            <EditRowStyle BackColor="#999999" />
                                            <EmptyDataRowStyle Height="48px" />
                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                        </asp:GridView>
                                        <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:ProTRAC_ConnectionString%>"
                                            SelectCommand="DisplayWorks" SelectCommandType="StoredProcedure">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="rbType" DefaultValue="0" Name="DayToPrev" PropertyName="SelectedValue"
                                                    Type="Int32" />
                                                <asp:ControlParameter ControlID="txtVehicleNumber" DefaultValue="" Name="VehicleNumber"
                                                    PropertyName="Text" Type="String" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                        <div id="GrdHead" runat="server">
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        <asp:AsyncPostBackTrigger ControlID="grdDisplay" EventName="RowDataBound" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <asp:Panel runat="server" ID="plPopUp" Style="display: none;">
        <div>
            <asp:ImageButton ID="btnClose" runat="server" AlternateText="Close" ImageUrl="~/Icons/Close.png"
                Style="position: absolute; right: -20px; top: -20px;" ToolTip="Close" />
            <table border="1px" style="background-color: #EFF3FB; text-align: center;" id="Table1"
                class="tblStyle">
                <tr style="background-color: #507CD1; color: White; font-weight: bold">
                    <td class="legData">
                        Image
                    </td>
                    <td class="legData">
                        Description
                    </td>
                    <td class="legData">
                        Image
                    </td>
                    <td class="legData">
                        Description
                    </td>
                </tr>
                <tr>
                    <td>
                        <img alt="Work In Progress" src="img/Process/PK.png" class="legimg" />
                    </td>
                    <td class="legData">
                        Work In Progress
                    </td>
                    <td>
                        <img alt="Work In Progress 5 mins remaining" src="img/Process/PK1.png" class="legimg" />
                    </td>
                    <td class="legData">
                        Work In Progress 5 mins remaining
                    </td>
                </tr>
                <tr>
                    <td>
                        <img alt="Work In Progress But Delayed" src="img/Process/PK3.png" class="legimg" />
                    </td>
                    <td class="legData">
                        Work In Progress But Delayed
                    </td>
                    <td>
                        <img alt="Rework (Work In Progress)" class="legimg" src="img/Process/PK4.png" />
                    </td>
                    <td class="legData">
                        Rework (Work In Progress)
                    </td>
                </tr>
                <tr>
                    <td>
                        <img alt="Rework (Work In Progress 5 mins remaining)" class="legimg" src="img/Process/PK5.png" />
                    </td>
                    <td class="legData">
                        Rework (Work In Progress 5 mins remaining)
                    </td>
                    <td>
                        <img alt="Rework (Work In Progress But Delayed)" class="legimg" src="img/Process/PK6.png" />
                    </td>
                    <td class="legData">
                        Rework (Work In Progress But Delayed)
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;<img alt="Work Complete In Time" class="legimg" src="img/Process/CN.png" />
                    </td>
                    <td class="legData">
                        Work Complete In Time
                    </td>
                    <td>
                        &nbsp;<img alt="Work Complete But Delayed" class="legimg" src="img/Process/CR.png" />
                    </td>
                    <td class="legData">
                        Work Complete But Delayed
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;<img alt="Rework (FI Break In On Time)" class="legimg" src="img/Process/CNR.png" />
                    </td>
                    <td class="legData">
                        Rework (FI Break In On Time)
                    </td>
                    <td>
                        &nbsp;<img alt="Rework (FI Break In Delay)" class="legimg" src="img/Process/CRR.png" />
                    </td>
                    <td class="legData">
                        Rework (FI Break In Delay)
                    </td>
                </tr>
                <tr>
                    <td>
                        <img alt="Process Skipped" src="img/Process/process_skiped.png" class="legimg" />
                    </td>
                    <td class="legData">
                        Process Skipped
                    </td>
                    <td>
                        <img alt="Process Out Skipped" src="img/Process/PS3.png" class="legimg" />
                    </td>
                    <td class="legData">
                        Process Out Skipped
                    </td>
                </tr>
                <tr>
                    <td>
                        <img alt="SA On Time" src="img/Process/clock_green.png" class="legimg" />
                    </td>
                    <td class="legData">
                        Scheduled Allotment On Time
                    </td>
                    <td>
                        <img alt="SA Delayed" src="img/Process/clock_red.png" class="legimg" />
                    </td>
                    <td class="legData">
                        Scheduled Allotment Delayed
                    </td>
                </tr>
                <tr>
                    <td width="32px" height="32px">
                        <img alt="In Progress" src="img/Process/circle_white.png" width="14px" height="14px" />
                    </td>
                    <td class="legData">
                        In Progress
                    </td>
                    <td width="32px" height="32px">
                        <img alt="Approaching" src="img/Process/circle_yellow.gif" width="14px" height="14px" />
                    </td>
                    <td class="legData">
                        Approaching
                    </td>
                </tr>
                <tr>
                    <td width="32px" height="32px">
                        <img alt="On Time" src="img/Process/circle_green.gif" width="14px" height="14px" />&nbsp;
                    </td>
                    <td class="legData">
                        On Time
                    </td>
                    <td width="32px" height="32px">
                        <img alt="Delayed" src="img/Process/circle_red.gif" width="14px" height="14px" />
                    </td>
                    <td class="legData">
                        Delayed
                    </td>
                </tr>
                <tr>
                    <td>
                        <img alt="Satisfied" class="legimg" src="Icons/Remarks_Added.png" />
                    </td>
                    <td class="legData">
                        Remarks Added
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <table style="text-align: left; vertical-align: middle; width: 100%; white-space: nowrap;">
                            <tr>
                                <td colspan="4" style="font-weight: bold;">
                                    Abbreviation :
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>S-T</b> : Service Type
                                </td>
                                <td>
                                    <b>PDT</b> : Promised Delivery Time
                                </td>
                                <td>
                                    <b>C.A</b> : Customer Acknowledgement
                                </td>
                                <td>
                                    <b>J.A</b> : Job Allotment Time
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>W-A</b> : Wheel Alignment
                                </td>
                                <td>
                                    <b>FI</b> : Final Inspection
                                </td>
                                <td>
                                    <b>RTest</b> : Road Test
                                </td>
                                <td>
                                    <b>RMK</b> : Remarks
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>PRG</b> : Progress
                                </td>
                                <td>
                                    <b>WIP</b> : Work In Progress
                                </td>
                                <td>
                                    <b>SDD</b> : Same Day Delivery
                                </td>
                                <td>
                                    <b>PDD</b> : Previous Day Delivery
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <%-- <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
        PopupControlID="plPopUp" TargetControlID="btnHelp" CancelControlID="btnClose">
    </cc1:ModalPopupExtender>--%>
    <%-- <asp:Panel runat="server" ID="Panel2" Style="display: none;">
        <div>
            <asp:Button CssClass="button" ID="ImageButton1" runat="server" AlternateText="Close"
                ImageUrl="~/Icons/Close.png" Style="position: absolute; right: -20px; top: -20px;" />
        </div>
    </asp:Panel>
    <cc1:ModalPopupExtender ID="ModalPopupExtender3" runat="server" BackgroundCssClass="modalBackground"
        PopupControlID="Panel1" TargetControlID="btnRemarksClose" CancelControlID="btnRemarksClose">
    </cc1:ModalPopupExtender>
    <asp:Panel runat="server" ID="Panel1">
        <div align="right">
            <asp:Button ID="btnRemarksClose" runat="server" Text="Close" />
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:GridView ID="GridViewRemarksDetails" runat="server" CellPadding="4" ForeColor="#333333"
                    GridLines="None">
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#999999" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>--%>
    <div id="myToolTip" runat="server" class="htmltooltip" />
    <%--     <center>
            <p style="position: fixed; width: 100%; height: 5px; bottom: 0px; left: 0px; right: 0px;
                font-family: Consolas, Georgia; font-size: 11px;">
                <asp:Label ID="lblVersion" runat="server"></asp:Label>
            </p>
        </center>--%>
    </form>
    <script type="text/javascript">
        var xPos, yPos;
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_beginRequest(BeginRequestHandler);
        prm.add_endRequest(EndRequestHandler);
        function BeginRequestHandler(sender, args) {
            xPos = $get('scrollDiv').scrollLeft;
            yPos = $get('scrollDiv').scrollTop;
        }
        function EndRequestHandler(sender, args) {
            $get('scrollDiv').scrollLeft = xPos;
            $get('scrollDiv').scrollTop = yPos;
        }
    </script>
</body>
</html>