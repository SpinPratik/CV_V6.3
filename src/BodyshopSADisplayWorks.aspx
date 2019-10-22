<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BodyshopSADisplayWorks.aspx.cs" Inherits="BodyshopSADisplayWorks" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   
      <link href="Icons/ProTRAC Icon.ico" rel="shortcut icon" type="image/x-icon" />
    <title>Service Advisor Display</title>
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
    <%--<script src="js/jquery-1.4.4.min.js" type="text/javascript"></script>--%>
    <script src="js/notify-osd.js" type="text/javascript"></script>


    <link rel="stylesheet" type="text/css" href="CSS/normalize.css" />
    <link rel="stylesheet" type="text/css" href="CSS/demo.css" />
    <link rel="stylesheet" type="text/css" href="CSS/component.css" />

    <link type="text/css" rel="stylesheet" href="http://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.6.3/css/font-awesome.min.css" />
    <link href="Bootstrap/bootstrap.min.css" rel="stylesheet" />
    <script type="text/javascript" src="Bootstrap/bootstrap.min.js"></script>
    <script type="text/javascript">
        function FormWidth() {
            document.getElementById("lbl_width").value = screen.width;
        }
        function DisableBackButton() {
            window.history.forward()
        }
        DisableBackButton();
        window.onload = DisableBackButton;
        window.onpageshow = function (evt) {
            if (evt.persisted)
                DisableBackButton()
        }
        window.onunload = function () {
            void (0)
        }
  
        function stopRKey(evt) {
            var evt = (evt) ? evt : ((event) ? event : null);
            var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
            if ((evt.keyCode == 13) && (node.type == "text")) { return false; }
        }
        document.onkeypress = stopRKey;
   
        $(function () {
            $("[id*=grdDisplay] td").hover(function () {
                $("td", $(this).closest("tr")).addClass("hover_row");
            }, function () {
                $("td", $(this).closest("tr")).removeClass("hover_row");
            });
        });
    </script>
    <style type="text/css">
        td {
            cursor: pointer;
        }

        .hover_row {
            background-color: #A1DCF2;
        }

        a:hover {
            text-decoration: none !important;
        }

        @-moz-keyframes blink {
            0% {
                opacity: 1;
            }

            50% {
                opacity: 0;
            }

            100% {
                opacity: 1;
            }
        }

        #animg {
            -moz-animation: blink 1s;
            -moz-animation-iteration-count: infinite;
        }

        .ttipBodyVal {
            width: 100px;
            text-align: left;
        }

        .TimeLabel {
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

        .modalBackground {
            background-color: #CCCCFF;
        }

        .tableCell {
            border-right-style: solid;
            border-right-color: White;
            border-right-width: thin;
        }

        .tableCellMiddle {
            border-right-style: solid;
            border-right-color: White;
            border-right-width: thin;
            text-align: center;
        }

        .SubTableTop {
            border-top-style: solid;
            border-top-color: White;
            border-top-width: thin;
        }

        .SubTableHeader {
            border-right-style: solid;
            border-right-color: White;
            border-right-width: thin;
            border-top-style: solid;
            border-top-color: White;
            border-top-width: thin;
        }

        #PopupPnl {
            background-color: #e0ecff;
            width: 390px;
            border-width: 3px;
            border-color: Black;
            border-style: solid;
        }

        table.stats {
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

            table.stats td {
                background-color: #CCC;
                color: #000;
                padding: 4px;
                text-align: left;
                border: 1px #fff solid;
            }

                table.stats td.hed {
                    background-color: #666;
                    color: #fff;
                    padding: 4px;
                    text-align: left;
                    border-bottom: 2px #fff solid;
                    font-size: 12px;
                    font-weight: bold;
                }

        #GridDisplay td {
            border: solid 1px white;
        }

        table a {
            text-align: center;
            padding-right: 25%;
            padding-left: 25%;
            display: block;
            text-decoration: none;
            font-size: 13px;
            font-weight: bold;
            color: White;
        }

            table a:hover {
                color: Black;
            }
        /*gridview*/


        .header {
            background-color: #646464;
            font-family: Arial;
            color: White;
            border: none 0px transparent;
            height: 25px;
            text-align: center;
            font-size: 16px;
        }

        .rows {
            background-color: #fff;
            font-family: Arial;
            font-size: 14px;
            color: #000;
            min-height: 25px;
            text-align: left;
            border: none 0px transparent;
        }

            .rows:hover {
                background-color: #ff8000;
                font-family: Arial;
                color: #fff;
                text-align: left;
            }

        .selectedrow {
            background-color: #ff8000;
            font-family: Arial;
            color: #fff;
            font-weight: bold;
            text-align: left;
        }

        .mydatagrid a /** FOR THE PAGING ICONS  **/ {
            background-color: Transparent;
            padding: 5px 5px 5px 5px;
            color: #fff;
            text-decoration: none;
            font-weight: bold;
        }

        .mydatagrid th /** FOR THE PAGING ICONS  **/ {
            border: 1px solid #e5e5e4 !important;
            border-bottom: 2px solid #e5e5e4 !important;
            padding: 2PX;
            font-weight: 700 !important;
            color: #000000;
            font-size: 12px !important;
            text-transform: uppercase;
        }

        th {
            text-transform: uppercase;
        }

        .mydatagrid a:hover /** FOR THE PAGING ICONS  HOVER STYLES**/ {
            /*background-color: #000;
	color: #fff;*/
        }

        .mydatagrid span /** FOR THE PAGING ICONS CURRENT PAGE INDICATOR **/ {
            background-color: #c9c9c9;
            color: #000;
            padding: 5px 5px 5px 5px;
        }

        .pager {
            background-color: #646464;
            font-family: Arial;
            color: White;
            height: 30px;
            text-align: left;
        }

        .mydatagrid td {
            padding: 0px 10px;
            font-size: 13px !important;
            font-weight: 400 !important;
            font-family: "Lato", sans-serif;
            border-bottom: 1px solid #e5e5e4 !important;
        }

        .mydatagrid tr:nth-child(even) {
            background-color: #ffffff !important;
        }

        .mydatagrid tr:nth-child(odd) {
            background-color: #f9f9f9 !important;
        }

        .mydatagrid a, .mydatagrid span {
            display: block;
            /*height: 15px;*/
            width: 15px;
            font-weight: bold;
            text-align: center;
            text-decoration: none;
        }

        .mydatagrid a {
            background-color: #f5f5f5;
            color: #969696;
            /*border: 1px solid #969696;*/
        }

        .mydatagrid span {
            background-color: #A1DCF2;
            color: #000;
            border: 1px solid #3AC0F2;
        }
        /*tabs*/
        .MyTabStyle .ajax__tab_header {
            font-family: "Helvetica Neue", Arial, Sans-Serif;
            font-size: 14px;
            font-weight: bold;
            display: flex;
            padding: 15px 0px;
        }

        .ajax__tab_default .ajax__tab_tab {
            overflow: hidden;
            text-align: center;
            display: -moz-inline-box;
            display: inline-block;
            cursor: pointer;
            text-transform: uppercase;
        }

        .MyTabStyle .ajax__tab_header .ajax__tab_outer {
            border-color: #286090;
            border-radius: 4px;
            padding: 4px;
            border: 1px solid #286090;
            text-align: center;
            color: #286090 !important;
            float: right;
            margin-right: 4px;
            font-family: tahoma, arial, Verdana;
            font-size: 11px;
            font-weight: 800;
            padding-left: 10px;
        }

        .MyTabStyle .ajax__tab_header .ajax__tab_inner {
            border-color: #666;
            color: #286090;
            padding: 3px 10px 2px 0px;
        }

        .MyTabStyle .ajax__tab_hover .ajax__tab_outer {
            background-color: #286090;
        }

        .MyTabStyle .ajax__tab_hover .ajax__tab_inner {
            color: #fff;
        }

        .MyTabStyle .ajax__tab_active .ajax__tab_outer {
            /*border-bottom-color: #ffffff;*/
            background-color: #d7d7d7;
        }

        .MyTabStyle .ajax__tab_active .ajax__tab_inner {
            color: #000;
            border-color: #333;
        }

        .MyTabStyle .ajax__tab_body {
            /*font-family: verdana,tahoma,helvetica;*/
            font-size: 10pt;
        }

        #header {
            background-color: white !important;
            padding: 2em !important;
        }

        .btn {
            text-transform: uppercase;
        }

        label {
            text-transform: uppercase;
        }

        span {
            text-transform: uppercase;
        }
    </style>
    <script type="text/javascript">

        function goSupport() {
            window.open('http://www.support.vtabs.in', '_blank');
        }

        function goAbout() {
            window.open('http://www.spintech.in', '_blank');
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
                //LoadGitter('UserID','title','info','bgblue.gif','1','5000','gritter-close','autofade');
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
        div.htmltooltip {
            position: absolute; /*leave this and next 3 values alone*/
            z-index: 1000;
            left: -1000px;
            top: -1000px;
            background: #272727;
            border: 10px solid black;
            color: white;
            padding: 3px;
            width: 250px; /*width of tooltip*/
        }

        p.MsoNormal {
            margin-top: 0in;
            margin-right: 0in;
            margin-bottom: 10.0pt;
            margin-left: 0in;
            line-height: 115%;
            font-size: 11.0pt;
            font-family: "Calibri", "sans-serif";
        }

        .style4 {
            height: 26px;
        }

        .style11 {
            width: 120px;
        }

        .style12 {
        }

        .style13 {
            width: 268435456px;
        }

        .MyTabStyle .ajax__tab_header {
            font-family: "Helvetica Neue", Arial, Sans-Serif;
            font-size: 14px;
            font-weight: bold;
            display: flex;
            padding: 15px 0px;
        }

        .ajax__tab_default .ajax__tab_tab {
            overflow: hidden;
            text-align: center;
            display: -moz-inline-box;
            display: inline-block;
            cursor: pointer;
            text-transform: uppercase;
        }

        .MyTabStyle .ajax__tab_header .ajax__tab_outer {
            border-color: #286090;
            border-radius: 4px;
            padding: 4px;
            border: 1px solid #286090;
            text-align: center;
            color: #286090 !important;
            float: right;
            margin-right: 4px;
            font-family: tahoma, arial, Verdana;
            font-size: 11px;
            font-weight: 800;
            padding-left: 10px;
        }

        .MyTabStyle .ajax__tab_header .ajax__tab_inner {
            border-color: #666;
            color: #286090;
            padding: 3px 10px 2px 0px;
        }

        .MyTabStyle .ajax__tab_hover .ajax__tab_outer {
            background-color: #286090;
        }

        .MyTabStyle .ajax__tab_hover .ajax__tab_inner {
            color: #fff;
        }

        .MyTabStyle .ajax__tab_active .ajax__tab_outer {
            /*border-bottom-color: #ffffff;*/
            background-color: #d7d7d7;
        }

        .MyTabStyle .ajax__tab_active .ajax__tab_inner {
            color: #000;
            border-color: #333;
        }

        .MyTabStyle .ajax__tab_body {
            /*font-family: verdana,tahoma,helvetica;*/
            font-size: 10pt;
        }

        #header {
            background-color: white !important;
            padding: 2em !important;
        }

        .btn {
            text-transform: uppercase;
        }
        .vcenter {
            display: inline-block;
            vertical-align: middle;
            float: none;
        }
        .row .vcenter{
           /*border-right: 1px solid white;*/
           /*padding:10px;*/
        }
        
       
         .gn-menu-main > li:nth-last-child(2){
             border-right:unset !important;
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
                url: "DisplayWorksI.aspx/LoadInOutTime",
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
                url: "DisplayWorksI.aspx/LoadRegNoHover",
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
                url: "DisplayWorksI.aspx/LoadProcessInOutTime",
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
                url: "DisplayWorksI.aspx/LoadSubProcessInOutTime",
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
                url: "DisplayWorksI.aspx/LoadEmployeeInOutTime",
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

        function showJADHover(e, RefNo) {
            $.ajax({
                type: "POST",
                url: "DisplayWorksI.aspx/LoadJADetails",
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

        function showPartsHover(e, RefNo) {
            $.ajax({
                type: "POST",
                url: "DisplayWorksI.aspx/LoadPartsDetails",
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

        function showRemarks(e, RefNo) {
            $.ajax({
                type: "POST",
                url: "DisplayWorksI.aspx/LoadRemarks",
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
                //data: "{'ServiceId':'"+ServiceId+"','ProcessName':'"+ProcessName+"'}",
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
            }
      );
        }
    </script>
</head>
<body>
     <form id="form1" runat="server">
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
        <div id="a" class="ttipStyle" style="font-family: Arial; background-color: white; width: 155px; height: 40px; border: solid 1px gray; text-align: left;"
            align="center">
        </div>

     
        <div class="" style="height:100%;min-width:1100px;">
        
            <ul id="gn-menu" class="gn-menu-main" style="background-color: #A62724 !important;">
                 <li>
                    <a href="NewJobCardCreation.aspx">  <asp:ImageButton ImageUrl="images/left-arrow%20(1).png" ID="back" OnClick="back_Click1" runat="server" style="margin-top:15px;"></asp:ImageButton></a>
                </li>
                <li><a href="#" style="color: white; font-size: 21px;"><img src="images/wms--logo.png" style=" margin-top: 15px;"/></a></li>
                <li style="width:225px;">
                    <a href="#">
                        <asp:Label runat="server" ID="lbl_LoginName" style="color:white;text-transform:uppercase;text-decoration:none !important;"></asp:Label>               
                    </a>
                </li>
               <li style="width:225px;border-right:unset !important;"><i class="fa fa-clock-o" style="color:white"></i> &nbsp;&nbsp;<asp:Label class="" ID="lbl_currTime" Font-Bold="true" style="color: white;text-transform:uppercase !important" runat="server" ></asp:Label></li>


                 <li style="border-left: 1px solid #c6d0da;"><a style="color: white;" href="#">
                    <img src="Images/logo_spin.png" /></a></li> 
                <li>  
                  <asp:LinkButton ID="btn_logout" OnClick="btn_logout_Click" style="vertical-align: middle;color:white;" Text="logout"  runat="server"></asp:LinkButton>
                  </li>            
               
            </ul>
       
        <br />
        </div>
        <table style="width: 100%; height: 100%; font-size: 13px;"
            border="0" cellpadding="0" cellspacing="0">

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
                            <td></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblrefno" runat="server" Visible="False"></asp:Label>
                                <input id="lbl_width" type="hidden" runat="server" value="0" />
                                <div id="mainTab">
                                    <cc1:TabContainer ID="TabContainer1" CssClass="MyTabStyle" runat="server" ActiveTabIndex="0" Height="100px"
                                        Width="100%">
                                        <cc1:TabPanel ID="TabPanel14" runat="server" HeaderText="Parts">
                                            <HeaderTemplate>
                                                Parts
                                            </HeaderTemplate>
                                            <ContentTemplate>
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td colspan="3">
                                                            <asp:Label ID="lblRefnoParts" runat="server" Visible="False"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>

                                                        <td valign="top">
                                                            <asp:Label ID="Label25" runat="server" Text="Parts"></asp:Label>

                                                            <asp:TextBox CssClass="form-control" ID="txtParts" runat="server"></asp:TextBox><cc1:AutoCompleteExtender
                                                                ID="txtParts_AutoCompleteExtender" runat="server" CompletionInterval="1" FirstRowSelected="True"
                                                                DelimiterCharacters="" Enabled="True" ServiceMethod="GetCompletionList" ViewStateMode="Disabled"
                                                                CompletionListItemCssClass="AutoCompStyle4" CompletionListCssClass="AutoComStyle6"
                                                                CompletionListHighlightedItemCssClass="AutoComStyle5" MinimumPrefixLength="0"
                                                                TargetControlID="txtParts" ServicePath="">
                                                            </cc1:AutoCompleteExtender>

                                                        </td>
                                                        <td>&nbsp;</td>
                                                        <td valign="top">
                                                            <br />
                                                            <asp:Button ID="btnParts" runat="server" CssClass="btn btn-success" OnClick="btnParts_Click"
                                                                Text="Save" ValidationGroup="aa1" />&nbsp;<asp:Button ID="btnRPDTCancel0"
                                                                    runat="server" CssClass="btn btn-danger" OnClick="btnRPDTCancel_Click" TabIndex="26"
                                                                    Text="X" ToolTip="Close" />
                                                        </td>

                                                        <td valign="top">
                                                            <asp:GridView ID="grdParts" CssClass="mydatagrid" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                                ForeColor="#333333" GridLines="None" OnPageIndexChanging="grdParts_PageIndexChanging"
                                                                OnRowDeleting="grdParts_RowDeleting" PageSize="2" Width="300px" OnRowDataBound="grdParts_RowDataBound">
                                                                <AlternatingRowStyle BackColor="White" />
                                                                <Columns>
                                                                    <asp:BoundField DataField="SlNo" HeaderText="SlNo" />
                                                                    <asp:BoundField DataField="RefNo" HeaderText="Refno" />
                                                                    <asp:BoundField DataField="PartsName" HeaderText="Parts Name" />
                                                                    <asp:BoundField DataField="ReqStatus" HeaderText="Status" />
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                                                                ForeColor="#333333" Text="Delete" Visible="true"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <FooterStyle BackColor="Silver" ForeColor="#333333" />
                                                                <HeaderStyle BackColor="Silver" ForeColor="#333333" HorizontalAlign="Left" />
                                                                <PagerStyle BackColor="Silver" ForeColor="#333333" HorizontalAlign="left" />
                                                                <RowStyle BackColor="WhiteSmoke" />
                                                                <SelectedRowStyle BackColor="WhiteSmoke" Font-Bold="True" ForeColor="#333333" />
                                                            </asp:GridView>
                                                        </td>
                                                        <td>&nbsp;
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
                                                        <td colspan="2"></td>
                                                        <tr>
                                                             <td>&nbsp;&nbsp;&nbsp;
                                                            </td>
                                                            <td class="myStyle">
                                                                <asp:Label ID="lblpdtveh" runat="server" Text="VRN/VIN"></asp:Label>
                                                                <asp:TextBox ID="lblpdtvehno" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td>&nbsp;
                                                            </td>

                                                            <td class="myStyle">
                                                                <asp:Label ID="lblPDT" runat="server" Text="PDT"></asp:Label>
                                                                <asp:TextBox ID="lblpdtno" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>&nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblRevPDT" runat="server" Text="Revised PDT"></asp:Label>
                                                                <asp:TextBox ID="txtRevPDT" runat="server" AutoPostBack="True" CssClass="form-control" MaxLength="10" OnTextChanged="txtRevPDT_TextChanged"
                                                                    TabIndex="23"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="calndrrPDt" runat="server" Enabled="True" TargetControlID="txtRevPDT">
                                                                </cc1:CalendarExtender>

                                                            </td><td>&nbsp; </td>
                                                            <td>
                                                                <asp:Label runat="server" Text="&nbsp;"></asp:Label>
                                                                <asp:TextBox ID="cmbHH" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <cc1:MaskedEditExtender ID="cmbHH_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                    Enabled="True" ErrorTooltipEnabled="True" Mask="99:99" MaskType="Time" TargetControlID="cmbHH">
                                                                </cc1:MaskedEditExtender>
                                                                <cc1:MaskedEditValidator ID="mevStartTime" runat="server" ControlExtender="cmbHH_MaskedEditExtender"
                                                                    ControlToValidate="cmbHH" Display="Dynamic" EmptyValueBlurredText="Time is required "
                                                                    EmptyValueMessage="Time is required " ErrorMessage="mevStartTime" InvalidValueBlurredMessage="Invalid Time"
                                                                    InvalidValueMessage="Time is invalid" ValidationGroup="MKE"></cc1:MaskedEditValidator>
                                                            </td>
                                                            <td>&nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" Text="Reason"></asp:Label>
                                                                <asp:DropDownList ID="ddPDTRemarks" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="ddPDTRemarks_SelectedIndexChanged" CssClass="form-control">
                                                                </asp:DropDownList>
                                                                
                                                            </td><td>&nbsp; </td> <td>
                                                                <asp:Label runat="server" Text="&nbsp;"></asp:Label>
                                                                  <asp:TextBox ID="txtPDTComment" runat="server" CssClass="form-control" MaxLength="100" TextMode="MultiLine"
                                                                       ></asp:TextBox></td>
                                                         
                                                            <td>&nbsp;</td>
                                                            <td colspan="2" valign="top">
                                                                <asp:Label runat="server" Text="Customer Informed"></asp:Label>
                                                                <br />
                                                                <asp:RadioButton ID="rd_Yes" runat="server" GroupName="cinfo" Text="&nbsp;&nbsp;Yes&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" />
                                                                <asp:RadioButton ID="rd_No" runat="server" GroupName="cinfo" Text="&nbsp;&nbsp;No" />
                                                            </td>
                                                            </td><td>&nbsp;&nbsp;</td>
                                                            <td valign="bottom">
                                                                <asp:Button ID="btnPDTsave" runat="server" CssClass="btn btn-success" OnClick="btnPDTsave_Click"
                                                                    TabIndex="25" Text="Save" ToolTip="Save" ValidationGroup="a" />
                                                                <asp:Button ID="btnPDTcncl" runat="server" CssClass="btn btn-danger" OnClick="btnecncl_Click"
                                                                    TabIndex="26" Text="X" ToolTip="Close" />
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td align="left" valign="top">
                                                                <td valign="top">&nbsp;&nbsp;
                                                                </td>
                                                                <td valign="top">&nbsp;&nbsp;
                                                                </td>
                                                                <td valign="top">&nbsp;&nbsp;
                                                                </td>
                                                                
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top">&nbsp;&nbsp;
                                                            </td>
                                                            <td class="myStyle" colspan="8" valign="top">&nbsp;&nbsp;
                                                            </td>
                                                        </tr>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </cc1:TabPanel>
                                        <cc1:TabPanel ID="TabPanel12" runat="server" HeaderText="Service/Process Remarks">
                                            <HeaderTemplate>
                                                Service/Process Remarks
                                            </HeaderTemplate>
                                            <ContentTemplate>
                                                <table class="tblStyle">
                                                    <tr>
                                                        <td colspan="2"></td>
                                                        <tr>
                                                           
                                                             <td>&nbsp;&nbsp;&nbsp;</td>
                                                            <td valign="top">
                                                                <asp:Label ID="lblspvc" runat="server" Text="VRN/VIN"></asp:Label>
                                                                <asp:TextBox ID="lblspvehicleno" CssClass="form-control" runat="server" ReadOnly="true" Width="120px"></asp:TextBox>
                                                                <asp:Label ID="lblservid" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="#663300"
                                                                    Visible="False"></asp:Label>
                                                            </td>
                                                            <td>&nbsp;</td>
                                                            <td style="text-align: left;">
                                                                <table style="text-align: left;">
                                                                    <tr>
                                                                        <td valign="top"></td>
                                                                        <td valign="top">
                                                                            <asp:Label ID="lbltype" runat="server" Text="Type"></asp:Label>
                                                                            <asp:DropDownList ID="drpsptype" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpsptype_SelectedIndexChanged"
                                                                                TabIndex="30">
                                                                                <asp:ListItem Value="0">Service</asp:ListItem>
                                                                                <asp:ListItem Value="1">Process</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td>&nbsp;</td>
                                                                        <td valign="top">
                                                                             <asp:Label ID="lblspprocess" runat="server" Text="Process" Visible="False"></asp:Label>
                                                                            <asp:DropDownList ID="drpspprocess" runat="server" DataSourceID="SqlDataSource1" CssClass="form-control"
                                                                                DataTextField="ShowProcessName" DataValueField="ProcessId" TabIndex="31" Visible="False">
                                                                            </asp:DropDownList>
                                                                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ProTRAC_ConnectionString %>"
                                                                                SelectCommand="SELECT ShowProcessName, ProcessId FROM tblProcessList ORDER BY ProcessDefaultOrder"></asp:SqlDataSource>
                                                                        </td>
                                                                     

                                                                        <td>&nbsp;</td>
                                                                        <td valign="top">
                                                                            <asp:Label ID="lblrmrktyp" runat="server" Text="Remarks"></asp:Label>
                                                                            <asp:DropDownList ID="ddlSRemarks" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSRemarks_SelectedIndexChanged"
                                                                                AppendDataBoundItems="True">
                                                                            </asp:DropDownList>
                                                                        </td><td>&nbsp;</td>
                                                                           <td valign="top">
                                                                               <asp:Label ID="lbl_other" runat="server" Text="OTHER"></asp:Label>
                                                                    <asp:TextBox ID="txtspremarks" runat="server" MaxLength="100" TabIndex="32" CssClass="form-control" TextMode="MultiLine"
                                                                        ></asp:TextBox>
                                                                </td>
                                                                        <td>&nbsp;</td>
                                                                        <td valign="top">
                                                                            <asp:Label ID="lblServiceAction" runat="server" Text="Action"></asp:Label>
                                                                            <asp:TextBox ID="txtServiceAction" runat="server" MaxLength="250" TextMode="MultiLine"
                                                                                CssClass="form-control"></asp:TextBox>
                                                                        </td>
                                                            </td>
                                                            <td>&nbsp;</td>
                                                            <td valign="top">
                                                                <asp:Label ID="lblServiceRecom" runat="server" Text="Recomendation"></asp:Label>
                                                                <asp:TextBox ID="txtRecomendation" CssClass="form-control" runat="server" MaxLength="250" TextMode="MultiLine"></asp:TextBox>
                                                            </td>
                                                            <td>&nbsp;</td>
                                                            <td valign="top">
                                                                <br />
                                                                <asp:Button ID="btnspsave" runat="server" CssClass="btn btn-success" OnClick="btnspsave_Click"
                                                                    TabIndex="33" Text="Save" ToolTip="Save" ValidationGroup="a" />
                                                                <asp:Button ID="btnspcncl" runat="server" CssClass="btn btn-danger" OnClick="btnecncl_Click"
                                                                    TabIndex="34" Text="X" ToolTip="Close" />
                                                            </td>
                                                        </tr>
                                                </table>
                                                </td>
                                                    </tr>
                                            </table>
                                            <table>
                                                <tr>
                                                    <td valign="top"></td>
                                                    <td valign="top">
                                                        <table>
                                                            <tr>

                                                                
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td valign="top">&nbsp;
                                                    </td>

                                                    <td valign="top">&nbsp;
                                                    </td>

                                                    <td valign="top">&#160;&#160;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </table>
                                            </ContentTemplate>
                                        </cc1:TabPanel>

                                        <cc1:TabPanel ID="TabPanel6" runat="server" HeaderText="Vehicle Ready">
                                            <HeaderTemplate>
                                                Vehicle Ready
                                            </HeaderTemplate>
                                            <ContentTemplate>
                                                <table class="tblStyle">
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Label ID="lblenrno" runat="server" Visible="False"></asp:Label><asp:Label ID="lblslno"
                                                                runat="server" Visible="False"></asp:Label>
                                                        </td>
                                                        <tr>
                                                            <td valign="top">
                                                                <asp:Label ID="lblvehicle" runat="server" Text="VRN/VIN"></asp:Label>
                                                                <asp:TextBox ID="lblvehicleno" runat="server" CssClass="form-control"></asp:TextBox>&#160;&#160;
                                                            </td>
                                                            <td valign="top">&#160;&#160;
                                                            </td>
                                                            <td valign="top">
                                                                <table>
                                                                    <tr>
                                                                        <td valign="top">
                                                                            <br />
                                                                            <asp:Button CssClass="btn btn-success" ID="btnready" runat="server" Text="Ready" OnClick="btnready_Click"
                                                                                TabIndex="9" ToolTip="Ready" ValidationGroup="a" />
                                                                            <asp:Button CssClass="btn btn-danger" ID="btnecncl" runat="server" Text="X" OnClick="btnecncl_Click"
                                                                                ToolTip="Close" />
                                                                        </td>
                                                                        <td>&#160;&#160;
                                                                        </td>
                                                                    </tr>
                                                                </table>
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
                                                            <asp:Label ID="Label1" runat="server" Visible="False"></asp:Label><asp:Label ID="Label2"
                                                                runat="server" Visible="False"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">
                                                            <asp:Label ID="lb1" runat="server" Text="VRN/VIN"></asp:Label>
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
                                                                        <asp:Button CssClass="button" ID="ImageButton6" runat="server" OnClick="btnecncl_Click"
                                                                            Text="X" ToolTip="Close" />
                                                                    </td>
                                                                    <td>&#160;&#160;
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">&#160;&#160;
                                                        </td>
                                                        <td colspan="2" valign="top">&#160;&#160;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </cc1:TabPanel>
                                        <cc1:TabPanel ID="TabPanel13" runat="server" HeaderText="REPAIR ORDER Closing">
                                            <HeaderTemplate>
                                                repair order Closing
                                            </HeaderTemplate>
                                            <ContentTemplate>
                                                <table class="tblStyle">
                                                    <tr>
                                                        <td></td>
                                                        <td>
                                                            <asp:Label runat="server"> VRN/VIN.</asp:Label>
                                                            <asp:TextBox ID="lblJCClosing" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                                        </td>
                                                        <td>&nbsp;&nbsp;</td>
                                                        <td>
                                                            <asp:Label runat="server">ROC Informed</asp:Label>

                                                            <asp:RadioButtonList ID="RadioCustomerInformed" runat="server" RepeatDirection="Horizontal"
                                                                ToolTip="Customer Informed?" Width="100px" Font-Bold="false" style="font-weight:100 !important;">
                                                                <asp:ListItem Value="1">&nbsp;Yes</asp:ListItem>
                                                                <asp:ListItem Value="0">&nbsp;No</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="RadioCustomerInformed"
                                                                CssClass="clsValidator" Display="Dynamic" ErrorMessage="ROC informed field is Mandatory, select the proper option."
                                                                Font-Bold="True" ValidationGroup="Val"> *</asp:RequiredFieldValidator>

                                                        </td> <td>&nbsp;&nbsp;</td>
                                                        <td>
                                                            <asp:Button CssClass="btn btn-success" ID="btnCloseJCC" runat="server" OnClick="btnCloseJCC_Click"
                                                                TabIndex="25" Text="Save" ToolTip="Save" ValidationGroup="a" />
                                                            <asp:Button CssClass="btn btn-danger" ID="ImageButton12" OnClick="btnRPDTCancel_Click" runat="server"
                                                                Text="x" TabIndex="26" ToolTip="Close" Width="63px" />
                                                        </td>
                                                    </tr>
                                                    <tr>


                                                        <td>&#160;&#160;
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
                                                <table>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Label ID="Label5" runat="server" Visible="False"></asp:Label>
                                                            <asp:Label ID="Label6" runat="server" Visible="False"></asp:Label>
                                                        </td>
                                                        <tr>
                                                            <td>&nbsp;&nbsp;&nbsp;</td>
                                                            <td valign="top">
                                                                <asp:Label ID="Label7" runat="server" Text="VRN/VIN"></asp:Label>
                                                                <asp:TextBox ID="lblvovehicleno" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>&#160;&#160;
                                                            </td>
                                                            <td valign="top">&#160;&#160;
                                                            </td>
                                                            <td colspan="2" valign="top">
                                                                <asp:Label runat="server" Text="Remarks"></asp:Label>
                                                                <asp:DropDownList ID="ddVOutRemarks" CssClass="form-control" runat="server" AutoPostBack="True" AppendDataBoundItems="True"
                                                                    OnSelectedIndexChanged="ddVOutRemarks_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>&nbsp;</td>
                                                            <td valign="top">
                                                                <br />
                                                                <asp:Button CssClass="btn btn-success" ID="ImageButton4" runat="server" Text="Vehicle Out"
                                                                    OnClick="btnOut_Click" TabIndex="9" ToolTip="Vehicle Out" ValidationGroup="a" />
                                                                <asp:Button CssClass="btn btn-danger" ID="ImageButton8" runat="server" Text="X" OnClick="btnecncl_Click"
                                                                    ToolTip="Close" />
                                                            </td>
                                                        </tr>
                                                        <tr>


                                                            <td colspan="2" valign="top">
                                                                <asp:TextBox ID="txt_VORemarks" runat="server" MaxLength="100" TabIndex="32" TextMode="MultiLine"
                                                                    Width="150px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </cc1:TabPanel>
                                        <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="Update Registration No" Visible="false">
                                            <HeaderTemplate>
                                                Update Registration No
                                            </HeaderTemplate>
                                            <ContentTemplate>
                                                <table class="tblStyle" style="width: 500px;">
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Label ID="lblMessage1" runat="server" Font-Bold="False" CssClass="clsValidator"></asp:Label>
                                                        </td>
                                                        <asp:Label ID="Label3" runat="server" Visible="False"></asp:Label><asp:Label ID="Label4"
                                                            runat="server" Visible="False"></asp:Label>
                                                        <tr>
                                                            <td style="width: 120px;">
                                                                <asp:Label ID="lblevehno1" runat="server" Text="VRN/VIN"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="txtevehno" runat="server" BackColor="Silver" Width="145px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="120px">
                                                                <asp:Label ID="lblenewvhno" runat="server" Text="New VRN/VIN"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtenewvhno" runat="server" MaxLength="10"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="120px"></td>
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
                                        <cc1:TabPanel ID="TabPanel4" runat="server" HeaderText="Update Customer Details" Visible="false">
                                            <HeaderTemplate>
                                                Update Customer Details
                                            </HeaderTemplate>
                                            <ContentTemplate>
                                                <table>
                                                    <tr>
                                                        <td style="width: 100px;">
                                                            <asp:Label ID="Label15" runat="server" Text="VRN/VIN"></asp:Label>
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
                                                            <td class="style12">Mobile No
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
                                                            <td>&nbsp;&nbsp;
                                                            </td>
                                                            <td>&nbsp;&nbsp;
                                                            </td>
                                                            <td>&nbsp;&nbsp;
                                                            </td>
                                                            <td>&nbsp;&nbsp;
                                                            </td>
                                                        </tr>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </cc1:TabPanel>
                                        <cc1:TabPanel ID="TabPanel5" runat="server" HeaderText="Tag Updation" Visible="false">
                                            <HeaderTemplate>
                                                Tag Updation
                                            </HeaderTemplate>
                                            <ContentTemplate>
                                                <table class="tblStyle">
                                                    <tr>
                                                        <td width="80px">
                                                            <asp:Label ID="lbvehl" runat="server" Text="VRN/VIN"></asp:Label>
                                                        </td>
                                                        <td width="200">
                                                            <asp:Label ID="lblvehicleUPDTag" runat="server" BackColor="Silver" Width="120px"></asp:Label>
                                                        </td>
                                                        <td colspan="2">
                                                            <asp:Button CssClass="button" ID="btnupdate" runat="server" OnClick="btnupdate_Click"
                                                                ValidationGroup="editcard" Text="Update" />
                                                            <asp:Button CssClass="button" ID="btn_CloseTagUpd0" runat="server" OnClick="btnecncl_Click"
                                                                ToolTip="X" Text="X" Width="63px" />
                                                        </td>
                                                        <td>&#160;&#160;
                                                        </td>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblcardno" runat="server" Text="VID"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="txtcardno" runat="server" BackColor="Silver" Width="120px"></asp:Label>
                                                            </td>
                                                            <td width="100">&nbsp;&nbsp;
                                                            </td>
                                                            <td>&nbsp;&nbsp;
                                                            </td>
                                                            <td>&nbsp;&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblnewcrdno" runat="server" Text="New VID"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtnewcrdno" runat="server" MaxLength="5" Width="120px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtnewcrdno"
                                                                    CssClass="clsValidator" ErrorMessage="*" ValidationGroup="editcard"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>&nbsp;&nbsp;
                                                            </td>
                                                            <td>&nbsp;&nbsp;
                                                            </td>
                                                            <td>&nbsp;&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4">
                                                                <asp:Label ID="Label8" runat="server" CssClass="clsValidator" Font-Bold="False"></asp:Label>
                                                                <asp:Label ID="lblref" runat="server" Visible="False"></asp:Label>
                                                            </td>
                                                            <td>&nbsp;&nbsp;
                                                            </td>
                                                        </tr>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </cc1:TabPanel>
                                        <cc1:TabPanel ID="TabPanel7" runat="server" HeaderText="RPDT Informed To Customer" Visible="false">
                                            <HeaderTemplate>
                                                RPDT Informed
                                            </HeaderTemplate>
                                            <ContentTemplate>
                                                <table class="tblStyle">
                                                    <tr>
                                                        <td>VRN/VIN.
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblvehno" runat="server" BackColor="Silver" Width="120px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Button CssClass="button" ID="btnRPDTUpd" runat="server" OnClick="btnRPDTUpd_Click"
                                                                TabIndex="25" Text="Save" ToolTip="Save" ValidationGroup="a" />
                                                            <asp:Button CssClass="button" ID="btnRPDTCancel" runat="server" OnClick="btnRPDTCancel_Click"
                                                                TabIndex="26" Text="X" ToolTip="Close" Width="63px" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>RPDT Informed :
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdo_Yes" runat="server" Text="Yes" />
                                                        </td>
                                                        <td>&#160;&#160;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </cc1:TabPanel>
                                        <cc1:TabPanel ID="TabPanel8" runat="server" HeaderText="ROC Informed" Visible="false">
                                            <HeaderTemplate>
                                                ROC Informed
                                            </HeaderTemplate>
                                            <ContentTemplate>
                                                <table class="tblStyle">
                                                    <tr>
                                                        <td>VRN/VIN.
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblJCCvehno" runat="server" BackColor="Silver" Width="120px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Button CssClass="button" ID="btn_JCCUpdate" runat="server" OnClick="btn_JCCUpdate_Click"
                                                                TabIndex="25" Text="Save" ToolTip="Save" ValidationGroup="a" />
                                                            <asp:Button CssClass="button" ID="ImageButton7" runat="server" OnClick="btnRPDTCancel_Click"
                                                                TabIndex="26" Text="X" ToolTip="Close" Width="63px" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>ROC Informed :
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdJC_Yes" runat="server" Text="Yes" />
                                                        </td>
                                                        <td>&#160;&#160;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </cc1:TabPanel>
                                        <cc1:TabPanel ID="TabPanel9" runat="server" HeaderText="ROC Informed" Visible="false">
                                            <HeaderTemplate>
                                                Bay Confirmation
                                            </HeaderTemplate>
                                            <ContentTemplate>
                                                <table class="tblStyle">
                                                    <tr>
                                                        <td>VRN/VIN.
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lbBCRegNo" runat="server" BackColor="Silver" Width="120px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Button CssClass="button" ID="ImageButton2" runat="server" OnClick="btn_BayConfirmationUpdate_Click"
                                                                TabIndex="25" Text="Save" ToolTip="Save" ValidationGroup="a" />
                                                            <asp:Button CssClass="button" ID="ImageButton3" runat="server" OnClick="btnRPDTCancel_Click"
                                                                TabIndex="26" Text="X" ToolTip="Close" Width="63px" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Bay :
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlBayConfirmationList" runat="server" Width="100px">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>&#160;&#160;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </cc1:TabPanel>
                                        <cc1:TabPanel ID="TabPanel11" runat="server" HeaderText="ROC Informed" Visible="false">
                                            <HeaderTemplate>
                                                Bay Free
                                            </HeaderTemplate>
                                            <ContentTemplate>
                                                <table class="tblStyle">
                                                    <tr>
                                                        <td>VRN/VIN.
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lbBFRegNo" runat="server" BackColor="Silver" Width="120px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Button CssClass="button" ID="ImageButton9" runat="server" Text="Save" OnClick="btn_BayFreeUpdate_Click"
                                                                TabIndex="25" ToolTip="Save" ValidationGroup="a" />
                                                            <asp:Button CssClass="button" ID="ImageButton10" runat="server" OnClick="btnRPDTCancel_Click"
                                                                TabIndex="26" Text="X" ToolTip="Close" Width="63px" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Bay :
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lbBayFree" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td>&#160;&#160;
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
                                            <tr>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr style="left: 0px">

                                                <td valign="top">
                                                    <table id="UDeliveredTab" runat="server" style="text-align: right;" border="0">
                                                        <tr>
                                                            <td valign="top">
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:RadioButtonList ID="rbType" runat="server" ForeColor="#666666" RepeatDirection="Horizontal" AutoPostBack="True"
                                                                            OnSelectedIndexChanged="rbType_SelectedIndexChanged" Font-Bold="false">
                                                                            <asp:ListItem Selected="True" Value="0">&nbsp;Today&nbsp;&nbsp;</asp:ListItem>
                                                                            <asp:ListItem Value="1">&nbsp;NextDay&nbsp;&nbsp;</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                            <td style="white-space: nowrap; font-weight: bold; color: #666666;">WORKSHOP STATUS:&nbsp;&nbsp;
                                                            </td>
                                                            <td style="white-space: nowrap; font-weight: bold; color: #666666;">TOTAL
                                                            </td>
                                                            <td width="25px" style="border-bottom-width: thick; border-width: medium; text-align: center;">
                                                                <asp:Label ID="lbUnDelivered" runat="server" Text="" ForeColor="Black" Font-Bold="true"></asp:Label>
                                                            </td>
                                                            <td>&nbsp;&nbsp;&nbsp;</td>
                                                            <td width="10px" style="white-space: nowrap; color: Red; text-align: center;">&nbsp;&nbsp;|&nbsp;&nbsp;
                                                            </td>
                                                            <td>&nbsp;&nbsp;&nbsp;</td>
                                                            <td style="white-space: nowrap; font-weight: bold; color: #666666; border-bottom: 2px solid #666666;">
                                                                <img alt="WIP" src="images/JCR/Status.png" width='20' height='20' />
                                                            </td>
                                                            <td style="white-space: nowrap; font-weight: bold; color: #666666; border-bottom: 2px solid #666666;">WIP&nbsp;&nbsp;
                                                            </td>
                                                            <td width="25px" style="border-bottom: 2px solid #666666; text-align: center;">
                                                                <asp:Label ID="lbWIP" runat="server" Text="" ForeColor="Black" Font-Bold="true"></asp:Label>
                                                            </td>
                                                            <td>&nbsp;&nbsp;&nbsp;</td>
                                                            <td width="10px" style="white-space: nowrap; color: Red; text-align: center;">&nbsp;&nbsp; |&nbsp;&nbsp;
                                                            </td>
                                                            <td>&nbsp;&nbsp;&nbsp;</td>
                                                            <td style="white-space: nowrap; font-weight: bold; color: #666666; border-bottom: 2px solid rgba(139, 195, 74, 0.43);">
                                                                <img alt="Ready" src="images/JCR/Status_Ready.png" width='20' height='20' />
                                                            </td>
                                                            <td style="white-space: nowrap; font-weight: bold; color: #666666; border-bottom: 2px solid rgba(139, 195, 74, 0.43);">READY&nbsp;&nbsp;
                                                            </td>
                                                            <td width="25px" style="border-bottom: 2px solid rgba(139, 195, 74, 0.43); text-align: center;">
                                                                <asp:Label ID="lbReady" runat="server" Text="" ForeColor="Black" Font-Bold="true"></asp:Label>
                                                            </td>
                                                            <td>&nbsp;&nbsp;&nbsp;</td>
                                                            <td width="10px" style="white-space: nowrap; color: Red; text-align: center;">&nbsp;&nbsp;|&nbsp;&nbsp;
                                                            </td>
                                                            <td>&nbsp;&nbsp;&nbsp;</td>
                                                            <td style="white-space: nowrap; font-weight: bold; color: #666666; border-bottom: 2px solid red;">
                                                                <img alt="Idle" src="images/JCR/Status_IDLE.png" width='20' height='20' />
                                                            </td>
                                                            <td style="white-space: nowrap; font-weight: bold; color: #666666; border-bottom: 2px solid red;">IDLE&nbsp;&nbsp;
                                                            </td>
                                                            <td width="25px" style="border-bottom: 2px solid red; text-align: center;">
                                                                <asp:Label ID="lbIdle" runat="server" Text="" ForeColor="Black" Font-Bold="true"></asp:Label>
                                                            </td>
                                                            <td>&nbsp;&nbsp;&nbsp;</td>
                                                            <td width="10px" style="white-space: nowrap; color: Red; text-align: center;">&nbsp;&nbsp;|&nbsp;&nbsp;
                                                            </td>
                                                            <td>&nbsp;&nbsp;&nbsp;</td>
                                                            <td style="white-space: nowrap; font-weight: bold; color: #666666; border-bottom: 2px solid rgba(3, 83, 255, 0.34);">
                                                                <img alt="Hold" src="images/JCR/Status_HOLD.png" width='20' height='20' />
                                                            </td>
                                                            <td style="white-space: nowrap; font-weight: bold; color: #666666; border-bottom: 2px solid rgba(3, 83, 255, 0.34);">HOLD&nbsp;&nbsp;
                                                            </td>
                                                            <td width="25px" style="border-bottom: 2px solid rgba(3, 83, 255, 0.34); text-align: center;">
                                                                <asp:Label ID="lbHold" runat="server" Text="" ForeColor="Black" Font-Bold="true"></asp:Label>
                                                            </td>
                                                            <td>&nbsp;&nbsp;&nbsp;</td>
                                                            <td width="10px" style="white-space: nowrap; color: Red; text-align: center;">&nbsp;&nbsp; |&nbsp;&nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="text-align: right; color: Red;">
                                                    <asp:Label runat="server" ID="lblmsg"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
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
                                <td valign="top" style="white-space: nowrap; vertical-align: top; color: #333333; font-weight: bold; font-size: 12px; text-align: left;"></td>
                                <td>
                                    <asp:DropDownList ID="cmbCustType" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                        OnSelectedIndexChanged="cmbCustType_SelectedIndexChanged" TabIndex="1" CssClass="form-control">
                                        <asp:ListItem>Customer Type</asp:ListItem>
                                        <asp:ListItem Value="0">General</asp:ListItem>
                                        <asp:ListItem Value="1">JDP</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>&nbsp;</td>
                                <td>
                                    <asp:DropDownList ID="cmbServiceType" runat="server" AppendDataBoundItems="True"
                                        AutoPostBack="True" DataSourceID="srcServiceType" DataTextField="ServiceType"
                                        DataValueField="ServiceType" OnSelectedIndexChanged="cmbServiceType_SelectedIndexChanged"
                                        TabIndex="2" CssClass="form-control">
                                        <asp:ListItem>Service Type</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="srcServiceType" runat="server" ConnectionString="<%$ ConnectionStrings:ProTRAC_ConnectionString %>"
                                        SelectCommand="SELECT [ServiceType] FROM [tblServiceTypes]"></asp:SqlDataSource>
                                </td>
                                <td>&nbsp;</td>
                                <td>
                                    <asp:DropDownList ID="cmbVehicleModel" runat="server" AppendDataBoundItems="True"
                                        AutoPostBack="True" DataSourceID="srcVehicleModel" DataTextField="Model" DataValueField="Model"
                                        OnSelectedIndexChanged="cmbVehicleModel_SelectedIndexChanged" TabIndex="3" CssClass="form-control">
                                        <asp:ListItem>Model</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="srcVehicleModel" runat="server" ConnectionString="<%$ ConnectionStrings:ProTRAC_ConnectionString %>"
                                        SelectCommand="SELECT DISTINCT Model FROM tblVehicleModel"></asp:SqlDataSource>
                                </td>
                                <td>&nbsp;</td>
                                <td>
                                    <asp:DropDownList ID="cmbProcess" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                        DataSourceID="SqlDataSource2" DataTextField="ShowProcessName" DataValueField="ShowProcessName"
                                        OnSelectedIndexChanged="cmbProcess_SelectedIndexChanged" TabIndex="4" CssClass="form-control">
                                        <asp:ListItem>Process</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ProTRAC_ConnectionString %>"
                                        SelectCommand="SELECT A.ShowProcessName FROM (SELECT ShowProcessName,Min(ProcessDefaultOrder) B FROM tblProcessList group BY ShowProcessName ) A order by A.B"></asp:SqlDataSource>
                                </td>
                                <td>&nbsp;</td>
                                <td>
                                    <asp:DropDownList ID="cmbSA" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                        OnSelectedIndexChanged="cmbCustType_SelectedIndexChanged" TabIndex="1" Visible="False"
                                        CssClass="form-control">
                                    </asp:DropDownList>
                                </td>
                                <td>&nbsp;</td>
                                <td>
                                    <asp:DropDownList ID="cmbTeamLead" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                        OnSelectedIndexChanged="cmbTeamLead_SelectedIndexChanged" TabIndex="1" Visible="True"
                                        CssClass="form-control">
                                    </asp:DropDownList>
                                </td>
                                <td>&nbsp;</td>
                                <td>
                                    <asp:DropDownList ID="ddlState" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                        CssClass="form-control">
                                        <asp:ListItem Value="0">Status</asp:ListItem>
                                        <asp:ListItem Value="1">WIP</asp:ListItem>
                                        <asp:ListItem Value="2">Hold</asp:ListItem>
                                        <asp:ListItem Value="3">Idle</asp:ListItem>
                                        <asp:ListItem Value="4">Delay</asp:ListItem>
                                        <asp:ListItem Value="5">OnTime</asp:ListItem>
                                        <asp:ListItem Value="6">Veh Ready</asp:ListItem>
                                        <asp:ListItem Value="7">RPDT Informed</asp:ListItem>
                                        <asp:ListItem Value="8">RPDT Not Informed</asp:ListItem>
                                        <asp:ListItem Value="9">ROC Informed</asp:ListItem>
                                        <asp:ListItem Value="10">ROC Not Informed</asp:ListItem>
                                        <asp:ListItem Value="11">Customer Waiting</asp:ListItem>
                                        <asp:ListItem Value="12">JC Not Opened</asp:ListItem>
                                        <asp:ListItem Value="13">Same Day Delivery</asp:ListItem>
                                        <asp:ListItem Value="14">Previous Day Delivery</asp:ListItem>
                                        <asp:ListItem Value="15">Canceled</asp:ListItem>
                                        <asp:ListItem Value="16">White Board</asp:ListItem>
                                        <asp:ListItem Value="17">Yellow Board</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>&nbsp;</td>
                                <td>
                                    <asp:DropDownList ID="drpOrderBy" runat="server" AutoPostBack="True" CssClass="form-control"
                                        OnSelectedIndexChanged="drpOrderBy_SelectedIndexChanged">
                                        <asp:ListItem Value="-1">Order By </asp:ListItem>
                                        <asp:ListItem Value="0">Order By PDT</asp:ListItem>
                                        <asp:ListItem Value="1">Order By Gate In</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>&nbsp;</td>
                                <td>
                                    <div>
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always">
                                            <ContentTemplate>
                                                <table>
                                                    <tr>
                                                        <td class="style4">
                                                            <asp:TextBox CssClass="form-control" ID="TxtDate1" runat="server" TabIndex="5" ValidationGroup="sg" placeholder="From"></asp:TextBox>
                                                            <cc1:TextBoxWatermarkExtender ID="TxtDate1_TextBoxWatermarkExtender" runat="server"
                                                                Enabled="True" TargetControlID="TxtDate1" WatermarkText="From">
                                                            </cc1:TextBoxWatermarkExtender>
                                                            <cc1:CalendarExtender ID="TxtDate1_CalendarExtender" runat="server" Enabled="True"
                                                                TargetControlID="TxtDate1">
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                        <td class="style4">
                                                            <asp:TextBox CssClass="form-control" ID="TxtDate2" runat="server" TabIndex="6" placeholder="To"></asp:TextBox>
                                                            <cc1:TextBoxWatermarkExtender ID="TxtDate2_TextBoxWatermarkExtender" runat="server"
                                                                Enabled="True" TargetControlID="TxtDate2" WatermarkText="To">
                                                            </cc1:TextBoxWatermarkExtender>
                                                            <cc1:CalendarExtender ID="TxtDate2_CalendarExtender" runat="server" Enabled="True"
                                                                TargetControlID="TxtDate2">
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                        <td class="style4">
                                                            <asp:TextBox CssClass="form-control" ID="txtVehicleNumber" runat="server" TabIndex="7" ValidationGroup="sg" placeholder="VRN/VIN"></asp:TextBox>
                                                            <cc1:TextBoxWatermarkExtender ID="txtVehicleNumber_TextBoxWatermarkExtender" runat="server"
                                                                Enabled="True" TargetControlID="txtVehicleNumber" WatermarkText="VRN/VIN">
                                                            </cc1:TextBoxWatermarkExtender>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                        <td class="style4">
                                                            <asp:TextBox CssClass="form-control" ID="txtTagNo" runat="server" TabIndex="7" ValidationGroup="sg" placeholder="VID"></asp:TextBox>
                                                            <cc1:TextBoxWatermarkExtender ID="txtTagNo_TextBoxWatermarkExtender" runat="server"
                                                                Enabled="True" TargetControlID="txtTagNo" WatermarkText="VID">
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
                                <td style="background-image: url('img/box.png'); background-repeat: no-repeat; background-position: center center; text-align: center; vertical-align: central; padding-left: 5px;"
                                    width="30">
                                    <asp:Label ID="lbVCount" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="#333333"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <br />
                    </div>
                </td>
            </tr>
            <tr style="width: 100%; left: 0;">
                <td align="center" style="width: 100%; height: 100%; left: 0;" valign="top">
                    <asp:UpdatePanel ID="PnlDisplay" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="60000">
                            </asp:Timer>
                            <table class="GridPanel" border="0" cellpadding="0">
                                <%--<tr>
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
                            </tr>--%>
                                <tr>
                                    <td style="width: 100%">
                                        <asp:Panel ID="Panel3" runat="server" CssClass="GridPanelJCRHeader">
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
                                                <RowStyle BackColor="#EFF3FB" HorizontalAlign="left" />
                                                <EmptyDataRowStyle Height="48px" />
                                                <FooterStyle BackColor="Silver" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="left" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#CC3333" Font-Bold="True" ForeColor="WHITE" HorizontalAlign="left" />
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
                                                            No Vehicle Received
                                                        </div>
                                                    </center>
                                                </EmptyDataTemplate>
                                                <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Height="22px"
                                                    HorizontalAlign="Right" />
                                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="left" />
                                                <RowStyle HorizontalAlign="left" BackColor="#F5F5F5" ForeColor="#333333" Height="35" />
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
            <div style="width: 900px">
                <asp:ImageButton ID="btnClose" runat="server" AlternateText="Close" ImageUrl="~/Icons/Close.png"
                    Style="position: relative; left: 895px; top: 10px;" ToolTip="Close" />
                <table border="1px" style="background-color: #E6E6E6; text-align: center;" id="Table1">
                    <tr style="background-color: #507CD1; color: White; font-weight: bold; font-size: 12px">
                        <td class="legData">ICON
                        </td>
                        <td class="legData">ALLOTED
                        </td>
                        <td class="legData">WIP ONTIME
                        </td>
                        <td class="legData">WIP NEAR
                        </td>
                        <td class="legData">WIP DELAY
                        </td>
                        <td class="legData">WORK COMPLETED ONTIME
                        </td>
                        <td class="legData">WORK COMPLETED DELAY
                        </td>
                        <td class="legData">PROCESS SKIPPED
                        </td>
                        <td class="legData">PROCESS NOT REQUIRED
                        </td>
                        <td class="legData">CUSTOMER NOT INFORMED
                        </td>
                    </tr>
                    <tr>
                        <td class="legData" style="font-size: 12px;">Vehicle Inventory
                        </td>
                        <td>
                            <img alt="Alloted" src="images/JCR/SA Allot.png" class="legimg" />
                        </td>
                        <td>
                            <img alt="Delayed" src="images/JCR/SA WIP ONTIME.png" class="legimg" />
                        </td>
                        <td>
                            <img alt="WIP Near " class="legimg" src="images/JCR/SA WIP NEAR.png" />
                        </td>
                        <td>
                            <img alt="WIP Delay " class="legimg" src="images/JCR/SA WIP DELAY.png" />
                        </td>
                        <td>
                            <img alt="Work Completed On Time" class="legimg" src="images/JCR/CN.png" />
                        </td>
                        <td>
                            <img alt=" Work Completed Delay" class="legimg" src="images/JCR/CR.png" />
                        </td>
                        <td>&nbsp;<img alt="Process Skipped" class="legimg" src="images/JCR/OutSkipped.png" />
                        </td>
                        <td colspan="2"></td>
                    </tr>
                    <tr>
                        <td class="legData" style="font-size: 12px;">Work Started On Or Before Schedule
                        </td>
                        <td>
                            <img alt=" Default" src="images/JCR/JA ONTIME.png" class="legimg" />
                        </td>
                        <td colspan="9"></td>
                    </tr>
                    <tr>
                        <td class="legData" style="font-size: 12px;">Work Started After Schedule
                        </td>
                        <td>
                            <img alt=" Default" src="images/JCR/JA DELAY.png" class="legimg" />
                        </td>
                        <td colspan="9"></td>
                    </tr>
                    <tr>
                        <td class="legData" style="font-size: 12px;">Technician
                        </td>
                        <td>
                            <img alt=" Alloted" src="images/JCR/TECH ALLOT.png" class="legimg" />
                        </td>
                        <td>
                            <img alt=" WIP OnTime" src="images/JCR/TECH WIP ONTIME.png" class="legimg" />
                        </td>
                        <td>
                            <img alt=" WIP Near" src="images/JCR/TECH WIP NEAR.png" class="legimg" />
                        </td>
                        <td>
                            <img alt=" WIP Delay" src="images/JCR/TECH WIP DELAY.png" class="legimg" />
                        </td>
                        <td>
                            <img alt=" OnTime" src="images/JCR/CN.png" class="legimg" />
                        </td>
                        <td>
                            <img alt=" Delay" src="images/JCR/CR.png" class="legimg" />
                        </td>
                        <td>
                            <img alt=" Process Skipped" src="images/JCR/OutSkipped.png" class="legimg" />
                        </td>
                        <td colspan="2"></td>
                    </tr>
                    <tr>
                        <td class="legData" style="font-size: 12px;">Wheel Alignment
                        </td>
                        <td>
                            <img alt=" Alloted" src="images/JCR/WA ALLOT.png" class="legimg" />
                        </td>
                        <td>
                            <img alt=" WIP OnTime" src="images/JCR/WA WIP.png" class="legimg" />
                        </td>
                        <td>
                            <img alt=" WIP Near" src="images/JCR/WA WIP NEAR.png" class="legimg" />
                        </td>
                        <td>
                            <img alt=" WIP Delay" src="images/JCR/WA WIP DELAY.png" class="legimg" />
                        </td>
                        <td>
                            <img alt=" OnTime" src="images/JCR/CN.png" class="legimg" />
                        </td>
                        <td>
                            <img alt=" Delay" src="images/JCR/CR.png" class="legimg" />
                        </td>
                        <td>
                            <img alt=" Process Skipped" src="images/JCR/OutSkipped.png" class="legimg" />
                        </td>
                        <td colspan="2"></td>
                    </tr>
                    <tr>
                        <td class="legData" style="font-size: 12px;">Road Test
                        </td>
                        <td></td>
                        <td>
                            <img alt=" WIP Near" src="images/JCR/RT ONTIME.png" class="legimg" />
                        </td>
                        <td colspan="2"></td>
                        <td>
                            <img alt=" OnTime" src="images/JCR/CN.png" class="legimg" />
                        </td>
                        <td></td>
                        <td>
                            <img alt=" Process Skipped" src="images/JCR/OutSkipped.png" class="legimg" />
                        </td>
                        <td colspan="2"></td>
                    </tr>
                    <tr>
                        <td class="legData" style="font-size: 12px;">Wash
                        </td>
                        <td>
                            <img alt=" Alloted" src="images/JCR/WASH allot.png" class="legimg" />
                        </td>
                        <td>
                            <img alt=" WIP OnTime" src="images/JCR/WASH WIP.png" class="legimg" />
                        </td>
                        <td>
                            <img alt=" WIP Near" src="images/JCR/WASH WIP NEAR.png" class="legimg" />
                        </td>
                        <td>
                            <img alt=" WIP Delay" src="images/JCR/WASH WIP Delay.png" class="legimg" />
                        </td>
                        <td>
                            <img alt=" OnTime" src="images/JCR/CN.png" class="legimg" />
                        </td>
                        <td>
                            <img alt=" Delay" src="images/JCR/CR.png" class="legimg" />
                        </td>
                        <td>
                            <img alt=" Process Skipped" src="images/JCR/OutSkipped.png" class="legimg" />
                        </td>
                        <td colspan="2"></td>
                    </tr>
                    <tr>
                        <td class="legData" style="font-size: 12px;">QC
                        </td>
                        <td></td>
                        <td>
                            <img alt=" WIP OnTime" src="images/JCR/QC WIP.png" class="legimg" />
                        </td>
                        <td>
                            <img alt=" WIP Near" src="images/JCR/QC WIP Near.png" class="legimg" />
                        </td>
                        <td>
                            <img alt=" WIP Delay" src="images/JCR/QC WIP Delay.png" class="legimg" />
                        </td>
                        <td>
                            <img alt=" OnTime" src="images/JCR/CN.png" class="legimg" />
                        </td>
                        <td>
                            <img alt=" Delay" src="images/JCR/CR.png" class="legimg" />
                        </td>
                        <td>
                            <img alt=" Process Skipped" src="images/JCR/OutSkipped.png" class="legimg" />
                        </td>
                        <td colspan="2"></td>
                    </tr>
                    <tr>
                        <td class="legData" style="font-size: 12px;">VAS
                        </td>
                        <td>
                            <img alt="Alloted" src="images/JCR/PK4.png" class="legimg" />
                        </td>
                        <td>
                            <img alt=" WIP OnTime" src="images/JCR/PK.png" class="legimg" />
                        </td>
                        <td>
                            <img alt=" WIP Near" src="images/JCR/PK1.png" class="legimg" />
                        </td>
                        <td>
                            <img alt=" WIP Delay" src="images/JCR/PK3.png" class="legimg" />
                        </td>
                        <td>
                            <img alt=" OnTime" src="images/JCR/CN.png" class="legimg" />
                        </td>
                        <td>
                            <img alt=" Delay" src="images/JCR/CR.png" class="legimg" />
                        </td>
                        <td>
                            <img alt=" Process Skipped" src="images/JCR/OutSkipped.png" class="legimg" />
                        </td>
                        <td colspan="2"></td>
                    </tr>
                    <tr>
                        <td class="style1" style="font-size: 12px;">JCC
                        </td>
                        <td colspan="4"></td>
                        <td>
                            <img alt=" OnTime" src="images/JCR/JCC ONTIME.png" class="legimg" />
                        </td>
                        <td colspan="3"></td>
                        <td>
                            <img alt="Process Skipped" src="images/JCR/JCC SKIP.png" class="legimg" />
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" style="font-size: 12px;">PDT Status
                        </td>
                        <td colspan="2"></td>
                        <td>
                            <img alt="Approching" src="images/JCR/circle_yellow.png" />
                        </td>
                        <td>
                            <img alt="Delay" src="images/JCR/circle_red.png" />
                        </td>
                        <td>
                            <img alt="OnTime" src="images/JCR/circle_green.png" />
                        </td>
                        <td colspan="4"></td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
        <%-- <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
        PopupControlID="plPopUp" TargetControlID="btn_help" CancelControlID="btnClose">
    </cc1:ModalPopupExtender>--%>
        <div id="myToolTip" runat="server" class="htmltooltip" />
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
