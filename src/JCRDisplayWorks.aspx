﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JCRDisplayWorks.aspx.cs"
    Inherits="JCRDisplayWorks" EnableViewStateMac="false" EnableSessionState="True"
    EnableEventValidation="false" ValidateRequest="false" ViewStateEncryptionMode="Never"
    Debug="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="Icons/ProTRAC Icon.ico" rel="shortcut icon" type="image/x-icon" />
    <title>Floor Controller</title>

<meta http-equiv="refresh" content="60">
    
    
<%--    <script src="js/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="js/jquery-ui-1.8.1.custom.min.js" type="text/javascript"></script>
    <script src="JS/jquery-1.3.2.js" type="text/javascript"></script>
    <script src="JS/jquery.gritter.min.js" type="text/javascript"></script>
    <script src="JS/customgitter.js" type="text/javascript"></script>
    <link href="css/cupertino/jquery-ui-1.8.1.custom.css" rel="stylesheet" type="text/css" />
    <link href="css/jquery.gritter.css" rel="stylesheet" type="text/css" />
    <link href="css/jquery.gritter2.css" rel="stylesheet" type="text/css" />
    <script src="js/Tooltip.js" type="text/javascript"></script>
    <link href="CSS/ProTRAC_CssJCR.css" rel="stylesheet" type="text/css" />
    <link href="CSS/notify-osd.css" rel="stylesheet" type="text/css" />
    <script src="js/notify-osd.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="CSS/normalize.css" />
    <link rel="stylesheet" type="text/css" href="CSS/demo.css" />
    <link rel="stylesheet" type="text/css" href="CSS/component.css" />
    <link href="Bootstrap/bootstrap.min.css" rel="stylesheet" />
    <link href="CSS/Style.css" rel="stylesheet" />
    <script src="Bootstrap/bootstrap.min.js"></script>--%>


        
    <script src="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484286919/js/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484286919/js/jquery-ui-1.8.1.custom.min.js" type="text/javascript"></script>
    <script src="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484286919/js/jquery-1.3.2.js" type="text/javascript"></script>
    <script src="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484286919/js/jquery.gritter.min.js" type="text/javascript"></script>
    <script src="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484286919/js/customgitter.js" type="text/javascript"></script>
    <link href="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484285348/css/cupertino/jquery-ui-1.8.1.custom.css" rel="stylesheet" type="text/css" />
    <link href="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484285348/css/jquery.gritter.css" rel="stylesheet" type="text/css" />
    <link href="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484285348/css/jquery.gritter2.css" rel="stylesheet" type="text/css" />
    <script src="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484286919/js/Tooltip.js" type="text/javascript"></script>
    <link href="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484285348/css/ProTRAC_CssJCR.css" rel="stylesheet" type="text/css" />
    <link href="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484285348/css/notify-osd.css" rel="stylesheet" type="text/css" />
    <script src="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484286919/js/notify-osd.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484285348/css/normalize.css" />
   <%-- <link rel="stylesheet" type="text/css" href="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484285348/css/demo.css" />--%>
    <link rel="stylesheet" type="text/css" href="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484285348/css/component.css" />
    <link href="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484285348/Bootstrap/bootstrap.min.css" rel="stylesheet" />
    <link href="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484285348/css/Style.css" rel="stylesheet" />
    <script src="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484286919/Bootstrap/bootstrap.min.js"></script>


  <%--   <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.6.3/css/font-awesome.min.css">--%>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <script type="text/javascript">
        function FormWidth() {
            document.getElementById("lbl_width").value = screen.width;
        }
        function DisableBackButton() {
            window.history.forward()
        }
        DisableBackButton();
        window.onload = DisableBackButton;
        window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
        window.onunload = function () { void (0) }
    </script>
    <script type="text/javascript">
        $(function () {
            $("[id*=grdDisplay] td").hover(function () {
                $("td", $(this).closest("tr")).addClass("hover_row");
            }, function () {
                $("td", $(this).closest("tr")).removeClass("hover_row");
            });
        });
    </script>
    <style type="text/css">
        td
        {
            cursor: pointer;
        }
        .hover_row
        {
            color: Black;
            background-color: #A1DCF2;
        }
  a:hover{
      text-decoration:none !important;
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
            
        }
        #GridDisplay td
        {
            border: solid 1px white;
        }
    </style>
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
            width: 140px;
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
            font-size: 13px;
            
            color: White;
        }
        table a:hover
        {
            color: Black;
        }
     
#header{
    background-color: white !important;
    padding: 2em !important;
        }
.btn{
text-transform:uppercase;
}
label{
    text-transform:uppercase;
}
span{
    text-transform:uppercase;
}
         /*gridview*/
         
        
.header
{
	background-color: #646464;
	font-family: Arial;
	color: White;
	border: none 0px transparent;
	height: 25px;
	text-align: center;
	font-size: 16px;
}

.rows
{
	background-color: #fff;
	font-family: Arial;
	font-size: 14px;
	color: #000;
	min-height: 25px;
	text-align: left;
	border: none 0px transparent;
}
.rows:hover
{
	background-color: #ff8000;
	font-family: Arial;
	color: #fff;
	text-align: left;
}
.selectedrow
{
	background-color: #ff8000;
	font-family: Arial;
	color: #fff;
	
	text-align: left;
}
.mydatagrid a /** FOR THE PAGING ICONS  **/
{
	background-color: Transparent;
	padding: 5px 5px 5px 5px;
	color: #fff;
	text-decoration: none;
	
}
.mydatagrid th /** FOR THE PAGING ICONS  **/
{
	/*border:1px solid #e5e5e4 !important;
    border-bottom:2px solid #e5e5e4 !important;*/
	padding: 2PX;
    font-weight: 700!important;
    color: #000000;
    font-size: 12px !important;
    text-transform:uppercase;
	background-color: #74b2e4;
}

.mydatagrid a:hover /** FOR THE PAGING ICONS  HOVER STYLES**/
{
	/*background-color: #000;
	color: #fff;*/
}
.mydatagrid span /** FOR THE PAGING ICONS CURRENT PAGE INDICATOR **/
{
	background-color: #c9c9c9;
	color: #000;
	padding: 5px 5px 5px 5px;
}
.pager
{
	background-color: #646464;
	font-family: Arial;
	color: White;
	height: 30px;
	text-align: left;
}

.mydatagrid td
{
	padding: 0px 10px;
    font-size: 13px!important;
    font-weight: 400!important;
    font-family: "Lato", sans-serif;
    border-bottom:1px solid #e5e5e4 !important;
}
.mydatagrid tr:nth-child(even)
    {
        background-color:#ffffff !important;
    }
.mydatagrid tr:nth-child(odd)
    {
        background-color:#f9f9f9 !important;
    }

 .mydatagrid a, .mydatagrid span
        {
            display: block;
            /*height: 15px;*/
            width: 15px;
            
            text-align: center;
            text-decoration: none;
        }
        .mydatagrid a
        {
            background-color: unset !important;
            color: #969696;
            /*border: 1px solid #969696;*/
        }
        .mydatagrid span
        {
            background-color: #A1DCF2;
            color: #000;
            border: 1px solid #3AC0F2;
        }
        .GridPanelJCRHeader{
                margin-right: -8px;
                height:50PX;
                    PADDING: 4PX;
        }

    </style>
    <script type="text/javascript">

        function goSupport() {
            window.open('https://www.support.vtabs.in', '_blank');
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
             #animg {
          animation: blink-animation 1s steps(5, start) infinite;
          -webkit-animation: blink-animation 1s steps(5, start) infinite;
        }
        @keyframes blink-animation {
          to {
            visibility: hidden;
          }
        }
        @-webkit-keyframes blink-animation {
          to {
            visibility: hidden;
          }
        }
         #Table1 td{
            padding:7px;
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
            window.open('https://www.protrac.spintech.in');
        }
        function goHelp() {
            window.open('Help/index.htm');
        }
        function goAbout() {
            window.open('www.spintech.in', '_blank');
        }
    </script>
    <script type="text/javascript">
        var t1;
        $(document).ready(
    function () {
        t1 = new ToolTip("a", true, 40);
    });

        function showDetails(e, ServiceId, ProcessName) {
            $.ajax({
                type: "POST",
                url: "JCRDisplayWorks.aspx/LoadInOutTime",
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
                url: "JCRDisplayWorks.aspx/LoadRegNoHover",
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
                url: "JCRDisplayWorks.aspx/LoadProcessInOutTime",
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
                url: "JCRDisplayWorks.aspx/LoadSubProcessInOutTime",
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
                url: "JCRDisplayWorks.aspx/LoadEmployeeInOutTime",
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
                url: "JCRDisplayWorks.aspx/LoadRemarks",
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

        function showJADHover(e, RefNo) {
            $.ajax({
                type: "POST",
                url: "JCRDisplayWorks.aspx/LoadJADetails",
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
                url: "JCRDisplayWorks.aspx/LoadPartsDetails",
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
                        DismissNotify();
                    }
                }
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
    <asp:Timer ID="Timer3" runat="server" OnTick="Timer3_Tick" Interval="1800000">
    </asp:Timer>
    <%--    NOTIFICATION PANEL  --%>
    <asp:UpdatePanel ID="updTime" runat="server">
        <ContentTemplate>
            <asp:Timer ID="NotificationTimer" runat="server" Interval="1800000" Enabled="True">
            </asp:Timer>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="a" class="ttipStyle" style="font-family: Arial; background-color: white;
        width: 155px; height: 40px; border: solid 1px gray; text-align: left;" align="center">
    </div>

                <div class="" style="height:100%;min-width:1100px;">
        
            <ul id="gn-menu" class="gn-menu-main" style="background-color: #FF7C23 !important;">
                 <li>
                    <a href="NewJobAllotment.aspx">  <asp:ImageButton ImageUrl="images/left-arrow%20(1).png" ID="back" runat="server" style="margin-top:15px;"></asp:ImageButton></a>
                </li>
                <li><a href="#" style="color: white; font-size: 21px;"><img src="https://res.cloudinary.com/deekyp5bi/image/upload/v1484297130/images/wms--logo.png" style=" margin-top: 15px;"/></a></li>
                <li style="width:225px;">
                    <a href="#"> <img src="https://res.cloudinary.com/deekyp5bi/image/upload/v1484219939/images/user.png" />&nbsp;&nbsp;
                        <asp:Label runat="server" ID="lbl_LoginName" style="color:white;text-transform:uppercase;text-decoration:none !important;"></asp:Label>               
                    </a>
                </li>
               <li style="width:225px;border-right:unset !important;"><i class="fa fa-clock-o" style="color:white"></i> &nbsp;&nbsp;<asp:Label class="" ID="lbl_currTime"  style="color: white;text-transform:uppercase !important" runat="server" ></asp:Label></li>
  <li style="border-left: 1px solid #c6d0da;"><a style="color: white;" href="#">
                    <img src="https://res.cloudinary.com/deekyp5bi/image/upload/v1484297130/images/logo_spin.png" /></a></li> 
                <li>  
                  <asp:LinkButton ID="btn_logout" OnClick="btn_logout_Click" style="vertical-align: middle;color:white;" Text="logout"  runat="server"></asp:LinkButton>
                  </li>            
               
            </ul>
       
      
        </div>
         <div style="background-color:rgba(128, 128, 128, 0.43);min-width:100%;float:right;">
            <asp:LinkButton ID="btn_help" Style="color:black;float:right;    margin-right: 49px;" runat="server"  Text="Help ?" />
       </div><br />

         <div style="margin-right: 17px;">
             <br />
             <asp:Button CssClass="btn btn-info" style="float:right;border:1px solid #286090;color:#286090;font-weight:800;font-size:12px;margin-left:3px; font-family: tahoma, arial, Verdana; background-color:white;" Text="Allotment" ID="btn_jcrAllotment" OnClick="btn_jcrAllotment_Click" runat="server" /> &nbsp;&nbsp;
            <asp:Button CssClass="btn btn-info" style="float:right;border:1px solid #286090;color:#286090;font-weight:800;font-size:12px;font-family: tahoma, arial, Verdana; background-color:white;" Visible="false" Text="KPI Dashboard" ID="btn_kpiDashboard" OnClick="btn_kpiDashboard_Click"  runat="server" />&nbsp;&nbsp;
            <asp:Button CssClass="btn btn-info" style="float:right;border:1px solid #286090;color:#286090;font-weight:800;font-size:12px;font-family: tahoma, arial, Verdana; background-color:white;" Text="Position Display" ID="Button1" OnClick="btn_position_Click"  runat="server" />&nbsp;&nbsp;
       <asp:Button CssClass="btn btn-info" style="float:right;border:1px solid #286090;color:#286090;font-weight:800;font-size:12px;margin-left:3px; font-family: tahoma, arial, Verdana; background-color:white;" Text="Reports" ID="btn_EEReport" OnClick="btn_jcrREPORTS_Click" runat="server" /> &nbsp;&nbsp; <%--added by Pratik--%>
</div>
        
    <table style="width: 100%; height: 100%; font-size: 13px;"
        border="0" cellpadding="0" cellspacing="0">
        <tr>
            <%--<td style="background-image: url('Layout/VTABS.png'); background-repeat: no-repeat;
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
                    <tr style="height: 25px; color: #333333;  font-size: 13px;
                        ">
                        <td style="padding-left: 8px" colspan="3">
                            <asp:Label ID="lbScroll0" runat="server"></asp:Label>
                        </td>
                        <td style="padding-right: 8px; text-align: right;">
                            <asp:Label ID="lbl_CurrentPage" runat="server" />|
                            <asp:Label ID="lbl_LoginName" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>--%>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td align="left">
                            <a runat="server" id="imgType" style="border: 0px"></a>
                            <asp:Label ID="lblSyncTime" runat="server"  Font-Size="Small" Visible="False"></asp:Label>
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
                                <cc1:TabContainer ID="TabContainer1" CssClass="MyTabStyle" runat="server" ActiveTabIndex="0" Height="100px"
                                    Width="100%">
                                       <cc1:TabPanel ID="TabPanel14" runat="server" HeaderText="Parts">
                                        <HeaderTemplate>
                                            Parts</HeaderTemplate>
                                        <ContentTemplate>
                                            <table>
                                                <tr>
                                                    <td colspan="3">
                                                        <asp:Label ID="lblRefnoParts" runat="server" Visible="False" ></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                 <td>&nbsp;&nbsp;</td>
                                                    <td valign="top">
                                                        <asp:Label ID="Label25" runat="server" Text="Parts"></asp:Label>
                                                        <asp:TextBox ID="txtParts" runat="server" CssClass="form-control" MaxLength="250"></asp:TextBox><cc1:AutoCompleteExtender
                                                            ID="txtParts_AutoCompleteExtender" runat="server" CompletionInterval="1" FirstRowSelected="True"
                                                            DelimiterCharacters="" Enabled="True" ServiceMethod="GetCompletionList" ViewStateMode="Disabled"
                                                            CompletionListItemCssClass="AutoCompStyle4" CompletionListCssClass="AutoComStyle6"
                                                            CompletionListHighlightedItemCssClass="AutoComStyle5" MinimumPrefixLength="0"
                                                            TargetControlID="txtParts" ServicePath="">
                                                        </cc1:AutoCompleteExtender>
                                                         <br />
                                                     <asp:Button ID="btnRPDTCancel0" style="float:right"
                                                                runat="server" CssClass="btn btn-danger" OnClick="btnRPDTCancel_Click" TabIndex="26"
                                                                Text="X" ToolTip="Close"/> &nbsp;&nbsp;&nbsp;
                                                         <asp:Button ID="btnParts" runat="server" CssClass="btn btn-success" style="float:right;margin-right:5px;" OnClick="btnParts_Click"
                                                            Text="Save" ValidationGroup="aa1"/>&nbsp;&nbsp;&nbsp;
                                                    </td><td>&nbsp;</td>
                                                    <td>
                                                        
                                                    </td>
                                                    <td >
                                                        &nbsp;&nbsp;
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
                                                                            ForeColor="#333333" Text="Delete" OnClientClick="return confirm('Are you sure you want to delete this Parts?');" Visible="true"></asp:LinkButton></ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <FooterStyle BackColor="Silver" ForeColor="#333333" />
                                                            <HeaderStyle BackColor="Silver" ForeColor="#333333" HorizontalAlign="Left" />
                                                            <PagerStyle BackColor="Silver" ForeColor="#333333" HorizontalAlign="Center" />
                                                            <RowStyle BackColor="WhiteSmoke" />
                                                            <SelectedRowStyle BackColor="WhiteSmoke"  ForeColor="#333333" />
                                                        </asp:GridView>
                                                        <br /><br />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel ID="TabPanel12" runat="server" HeaderText="Service/Process/Carry Foward Remarks">
                                        <HeaderTemplate>
                                            Service/Process/Carry Foward Remarks</HeaderTemplate>
                                        <ContentTemplate>
                                            <table class="tblStyle">
                                                <tr>
                                                    <td colspan="3">
                                                    </td>
                                                    <tr>
                                                        
                                                        <td valign="top">
                                                            <asp:Label ID="lblspvc" runat="server" Text="VIN/VRN"></asp:Label>
                                                            <asp:TextBox ID="lblspvehicleno" runat="server" CssClass="form-control" ></asp:TextBox><asp:Label
                                                                ID="lblservid" runat="server"  Font-Size="Small" ForeColor="#663300"
                                                                Visible="False"></asp:Label>
                                                        </td><td>&nbsp;</td>
                                                        
                                                                    
                                                                    <td valign="top">
                                                                         <asp:Label ID="lbltype" runat="server" Text="Type"></asp:Label>
                                                                        <asp:DropDownList ID="drpsptype" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="drpsptype_SelectedIndexChanged"
                                                                            TabIndex="30">
                                                                            <asp:ListItem Value="0">Service</asp:ListItem>
                                                                            <%--<asp:ListItem Value="1">Process</asp:ListItem>--%>
                                                                            <asp:ListItem Value="2">Carry Forward</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                       &nbsp;
                                                                    </td>
                                                                    <td valign="top">
                                                                         <asp:Label ID="lblspprocess" runat="server" Text="Process" Visible="False"></asp:Label>
                                                                        <asp:DropDownList ID="drpspprocess" CssClass="form-control" runat="server" DataSourceID="SqlDataSource1"
                                                                            DataTextField="ShowProcessName" DataValueField="ProcessId" TabIndex="31" Visible="False">
                                                                        </asp:DropDownList>
                                                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                                                                            SelectCommand="SELECT ShowProcessName, ProcessId FROM tblProcessList ORDER BY ProcessDefaultOrder">
                                                                        </asp:SqlDataSource>
                                                                    </td><td>&nbsp;</td>

                                                                    <td valign="top">
                                                                          <asp:Label ID="lblrmrktyp" runat="server" Text="Remarks"></asp:Label>
                                                                    <asp:DropDownList ID="ddlSRemarks" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSRemarks_SelectedIndexChanged"
                                                                        AppendDataBoundItems="True">
                                                                    </asp:DropDownList>
                                                                        
                                                                </td>
                                                        <td>&nbsp;</td>
                                                        <td> <asp:TextBox ID="txtspremarks" placeholder="Enter Remarks" runat="server" MaxLength="100" TabIndex="32" TextMode="MultiLine"
                                                                        CssClass="form-control" Visible="false"></asp:TextBox></td>
                                                        <td>&nbsp;</td>
                                                                     <td valign="top">
                                                                         <asp:Label ID="lblServiceAction" runat="server" Text="Action"></asp:Label>
                                                        <asp:TextBox ID="txtServiceAction" runat="server" MaxLength="250" TextMode="MultiLine"
                                                            CssClass="form-control"></asp:TextBox>
                                                    </td><td>&nbsp;</td>
                                                                    <td valign="top">
                                                                          <asp:Label ID="lblServiceRecom" runat="server" Text="Recomendation"></asp:Label>
                                                        <asp:TextBox ID="txtRecomendation" runat="server" CssClass="form-control" MaxLength="250" TextMode="MultiLine"></asp:TextBox>
                                                    </td><td>&nbsp;</td>
                                                                    <td valign="top" class="style8"><br />
                                                                        <asp:Button CssClass="btn btn-success" ID="btnspsave" runat="server" Text="Save" OnClick="btnspsave_Click"
                                                                            TabIndex="33" ToolTip="Save" ValidationGroup="a" />&nbsp;<asp:Button CssClass="btn btn-danger"
                                                                                ID="btnspcncl" runat="server" Text="X" OnClick="btnecncl_Click" TabIndex="34"
                                                                                ToolTip="Close" />
                                                                    </td>
                                                               
                                                    </tr>
                                            </table>
                                            
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                     <cc1:TabPanel ID="TabPanel15" runat="server" HeaderText="Technician Remarks">
                                        <HeaderTemplate>
                                            Technician Remarks</HeaderTemplate>
                                        <ContentTemplate>
                                            <table>
                                                <tr>
                                                    <td colspan="5">
                                                        <asp:Label ID="lbl_TechRefId" runat="server" Visible="False" ></asp:Label>
                                                        <asp:Label ID="lbl_Techmsg" runat="server" Visible="True" ></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <asp:Label ID="Label26" runat="server" Text="Technician"></asp:Label>
                                                        <asp:DropDownList ID="ddlTechList" runat="server" CssClass="form-control" style="width:unset" AppendDataBoundItems="True" >
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td valign="top">
                                                        &nbsp;&nbsp;
                                                    </td>
                                                     <td>
                                                         <asp:Label ID="Label27" runat="server" Text="Remarks"></asp:Label>
                                                  
                                                        <asp:TextBox ID="txt_TechRemarks" runat="server" CssClass="form-control"  MaxLength="200"></asp:TextBox>
                                                    </td><td>
                                                        &nbsp;&nbsp;
                                                    </td>
                                                    <td  style="white-space: nowrap"><br />
                                                        <asp:Button ID="btn_SaveTechRemarks" runat="server" CssClass="btn btn-success" OnClick="btn_SaveTechRemarks_Click"
                                                            Text="Save" ValidationGroup="aa1" />&nbsp;&nbsp;<asp:Button ID="btn_CloseTechRemarks"
                                                                runat="server" CssClass="btn btn-danger" OnClick="btnRPDTCancel_Click" TabIndex="26"
                                                                Text="X" ToolTip="Close" />
                                                    </td>
                                                    
                                                </tr>
                                                <tr>
                                                    <td style="width: 50px; white-space: nowrap;" valign="top">
                                                          </td>
                                                   
                                                    <td valign="top">
                                                        &nbsp;&nbsp;
                                                    </td>
                                                    <td valign="top">
                                                        &nbsp;&nbsp;
                                                    </td>
                                                    <td valign="top">
                                                        &nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel ID="TabPanel10" runat="server" HeaderText="Revised PDT" Visible="false">
                                        <HeaderTemplate>
                                            Revised PDT</HeaderTemplate>
                                        <ContentTemplate>
                                            <table class="tblStyle">
                                                <tr>
                                                    <td colspan="2">
                                                    </td>
                                                    <tr>
                                                        <td valign="top">
                                                            <asp:Label ID="lblpdtveh" runat="server" Text="VIN/VRN"></asp:Label>
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
                                                                TabIndex="23" Width="80px"></asp:TextBox><cc1:CalendarExtender ID="calndrrPDt" runat="server"
                                                                    Enabled="True" TargetControlID="txtRevPDT">
                                                                </cc1:CalendarExtender>
                                                            <asp:TextBox ID="cmbHH" runat="server" Width="60px"></asp:TextBox><cc1:MaskedEditExtender
                                                                ID="cmbHH_MaskedEditExtender" runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                                                CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" Mask="99:99"
                                                                MaskType="Time" TargetControlID="cmbHH">
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
                                                                    TabIndex="25" Text="Save" ToolTip="Save" ValidationGroup="a" /><asp:Button CssClass="button"
                                                                        ID="btnPDTcncl" runat="server" OnClick="btnecncl_Click" TabIndex="26" Text="X"
                                                                        ToolTip="Close" Width="63px" />
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
                                     <cc1:TabPanel ID="TabPanel13" runat="server" HeaderText="Vehicle Un Hold">
                                        <HeaderTemplate>
                                            Vehicle Un Hold</HeaderTemplate>
                                        <ContentTemplate>
                                            <table>
                                                <tr>
                                                    <td colspan="3">
                                                        <asp:Label ID="lblSlnoforHold" runat="server" Visible="False" ></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;
                                                    </td>
                                                    <td valign="middle" align="left">
                                                        <asp:Label ID="Label21" runat="server" Text="VID:" ></asp:Label>

                                                        <asp:TextBox ID="lblTagnoforHold" runat="server"  CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                       &nbsp;
                                                    </td>
                                                    <td>
                                                         <asp:Label ID="Label23" runat="server" Text="VIN/VRN:" ></asp:Label>
                                                        <asp:TextBox ID="lblVehnoforHold" runat="server"  CssClass="form-control"></asp:TextBox>
                                                    </td><td>&nbsp;</td>
                                                    <td><br />
                                                        <asp:Button CssClass="btn btn-success" ID="btnHold" runat="server" ValidationGroup="aa1" Text="Un Hold"
                                                            OnClick="btnHold_Click" />&nbsp;<asp:Button CssClass="btn btn-danger" ID="Button5" runat="server"
                                                                Text="X" ToolTip="Close" OnClick="btnecncl_Click" Width="63px" />
                                                    </td>
                                                    <td>
                                                        &nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel ID="TabPanel6" runat="server" HeaderText="Vehicle Ready">
                                        <HeaderTemplate>
                                            Vehicle Ready</HeaderTemplate>
                                        <ContentTemplate>
                                            <table style="width: 358px">
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Label ID="lblenrno" runat="server" Visible="False"></asp:Label><asp:Label ID="lblslno"
                                                            runat="server" Visible="False"></asp:Label>
                                                    </td>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td class="style6">
                                                            <asp:Label ID="lblvehicle" runat="server" Text="VIN/VRN"></asp:Label>
                                                            <asp:Label ID="lblvehicleno" runat="server" CssClass="form-control" BackColor="Silver" Width="120px"></asp:Label>&#160;&#160;
                                                        </td>  <td>&nbsp;</td>
                                                        <td>
                                                            <asp:Button CssClass="btn btn-success" ID="btnready" runat="server" Text="Ready" OnClick="btnready_Click"
                                                                TabIndex="9" ToolTip="Ready" ValidationGroup="a" />&nbsp;<asp:Button CssClass="btn btn-danger"
                                                                    ID="btnecncl" runat="server" Text="X" OnClick="btnecncl_Click" ToolTip="Close"
                                                                    />
                                                        </td>
                                                    </tr>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="Vehicle Cancelation" Visible="false">
                                        <HeaderTemplate>
                                            Vehicle Cancelation</HeaderTemplate>
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
                                                        <asp:Label ID="lb1" runat="server" Text="VIN/VRN"></asp:Label>
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
                                                                        TabIndex="9" ToolTip="Cancelation" ValidationGroup="a" /><asp:Button CssClass="button"
                                                                            ID="ImageButton6" runat="server" Text="X" OnClick="btnecncl_Click" ToolTip="Close"
                                                                            Width="63px" />
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
                                    <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="Vehicle Out" Visible="false">
                                        <HeaderTemplate>
                                            Vehicle Out</HeaderTemplate>
                                        <ContentTemplate>
                                            <table class="tblStyle">
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Label ID="Label5" runat="server" Visible="False"></asp:Label><asp:Label ID="Label6"
                                                            runat="server" Visible="False"></asp:Label>
                                                    </td>
                                                    <tr>
                                                        <td valign="top" align="left">
                                                            <asp:Label ID="Label7" runat="server" Text="VIN/VRN"></asp:Label>
                                                        </td>
                                                        <td valign="top">
                                                            <asp:Label ID="lblvovehicleno" runat="server" BackColor="Silver" Width="120px"></asp:Label>&nbsp;&nbsp;
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
                                                                        </asp:Button><asp:Button CssClass="button" ID="ImageButton8" runat="server" Text="X"
                                                                            OnClick="btnecncl_Click" ToolTip="Close" Width="63px"></asp:Button>
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
                                    <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="Update Registration No" Visible="false">
                                        <HeaderTemplate>
                                            Update Registration No</HeaderTemplate>
                                        <ContentTemplate>
                                            <table class="tblStyle" style="width: 500px;">
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Label ID="lblMessage1" runat="server" Font-Bold="False" CssClass="clsValidator"></asp:Label>
                                                    </td>
                                                    <asp:Label ID="Label3" runat="server" Visible="False"></asp:Label><asp:Label ID="Label4"
                                                        runat="server" Visible="False"></asp:Label><tr>
                                                            <td style="width: 120px;">
                                                                <asp:Label ID="lblevehno1" runat="server" Text="VIN/VRN"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="txtevehno" runat="server" BackColor="Silver" Width="125px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    <tr>
                                                        <td width="120px">
                                                            <asp:Label ID="lblenewvhno" runat="server" Text="New VIN/VRN"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtenewvhno" runat="server" MaxLength="10"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="120px">
                                                        </td>
                                                        <td>
                                                            <asp:Button CssClass="button" ID="btneupd" runat="server" Text="Update" OnClick="btneupd_Click" /><asp:Button
                                                                CssClass="button" ID="btn_CloseRegUpd" runat="server" Text="X" OnClick="btnecncl_Click"
                                                                ToolTip="Close" Width="63px" />
                                                        </td>
                                                    </tr>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel ID="TabPanel4" runat="server" HeaderText="Update Customer Details" Visible="false">
                                        <HeaderTemplate>
                                            Update Customer Details</HeaderTemplate>
                                        <ContentTemplate>
                                            <table>
                                                <tr>
                                                    <td style="width: 120px;">
                                                        <asp:Label ID="Label15" runat="server" Text="VIN/VRN"></asp:Label>
                                                    </td>
                                                    <td valign="middle">
                                                        <asp:Label ID="txtvehicle" runat="server" BackColor="Silver" Width="120px"></asp:Label>
                                                    </td>
                                                    <td valign="middle" colspan="4">
                                                        <asp:Button CssClass="button" ID="Button3" runat="server" Text="Update" OnClick="Button3_Click"
                                                            ValidationGroup="editcust" /><asp:Button CssClass="button" ID="btn_CloseCustUpd"
                                                                runat="server" Text="X" OnClick="btnecncl_Click" ToolTip="Close" Width="63px" />
                                                    </td>
                                                    <tr>
                                                        <td valign="middle" class="style11">
                                                            <asp:Label ID="Label16" runat="server" Text="Customer Name"></asp:Label>
                                                        </td>
                                                        <td valign="middle" align="left" class="style12">
                                                            <asp:TextBox ID="txtCustName" runat="server" MaxLength="25" Width="150px"></asp:TextBox><asp:RequiredFieldValidator
                                                                ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtCustName"
                                                                ErrorMessage="*" ValidationGroup="editcust"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style11">
                                                            Mobile No
                                                        </td>
                                                        <td class="style12">
                                                            <asp:TextBox ID="txtmobile" runat="server" MaxLength="10" Width="150px"></asp:TextBox><asp:RequiredFieldValidator
                                                                ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtmobile" CssClass="clsValidator"
                                                                ErrorMessage="*" ValidationGroup="editcust"></asp:RequiredFieldValidator>
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
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Label ID="Label11" runat="server" CssClass="clsValidator" Font-Bold="False"></asp:Label><asp:Label
                                                                ID="Label12" runat="server" Visible="False"></asp:Label><asp:Label ID="Label13" runat="server"
                                                                    Visible="False"></asp:Label>
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
                                    <cc1:TabPanel ID="TabPanel5" runat="server" HeaderText="Tag Updation" Visible="false">
                                        <HeaderTemplate>
                                            Tag Updation</HeaderTemplate>
                                        <ContentTemplate>
                                            <table class="tblStyle">
                                                <tr>
                                                    <td width="80px">
                                                        <asp:Label ID="lbvehl" runat="server" Text="VIN/VRN"></asp:Label>
                                                    </td>
                                                    <td width="200">
                                                        <asp:Label ID="lblvehicleUPDTag" runat="server" BackColor="Silver" Width="120px"></asp:Label>
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:Button CssClass="button" ID="btnupdate" runat="server" OnClick="btnupdate_Click"
                                                            ValidationGroup="editcard" Text="Update" /><asp:Button CssClass="button" ID="btn_CloseTagUpd0"
                                                                runat="server" Text="X" OnClick="btnecncl_Click" ToolTip="Close" Width="63px" />
                                                    </td>
                                                    <td>
                                                        &nbsp;&nbsp;
                                                    </td>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblcardno" runat="server" Text="VID"></asp:Label>
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
                                                            <asp:Label ID="lblnewcrdno" runat="server" Text="New VID"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtnewcrdno" runat="server" MaxLength="5" Width="147px"></asp:TextBox><asp:RequiredFieldValidator
                                                                ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtnewcrdno" CssClass="clsValidator"
                                                                ErrorMessage="*" ValidationGroup="editcard"></asp:RequiredFieldValidator>
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
                                                            <asp:Label ID="Label8" runat="server" CssClass="clsValidator" Font-Bold="False"></asp:Label><asp:Label
                                                                ID="lblref" runat="server" Visible="False"></asp:Label>
                                                        </td>
                                                        <td>
                                                            &nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel ID="TabPanel7" runat="server" HeaderText="RPDT Informed To Customer" Visible="false">
                                        <HeaderTemplate>
                                            RPDT Informed</HeaderTemplate>
                                        <ContentTemplate>
                                            <table class="tblStyle">
                                                <tr>
                                                    <td>
                                                        VIN/VRN.
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblvehno" runat="server" BackColor="Silver" Width="100px"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Button CssClass="button" ID="btnRPDTUpd" runat="server" OnClick="btnRPDTUpd_Click"
                                                            TabIndex="25" Text="Save" ToolTip="Save" ValidationGroup="a" Width="58px" /><asp:Button
                                                                CssClass="button" ID="btnRPDTCancel" runat="server" OnClick="btnRPDTCancel_Click"
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
                                    <cc1:TabPanel ID="TabPanel8" runat="server" HeaderText="ROC Informed" Visible="false">
                                        <HeaderTemplate>
                                            ROC Informed</HeaderTemplate>
                                        <ContentTemplate>
                                            <table class="tblStyle">
                                                <tr>
                                                    <td class="style11">
                                                        VIN/VRN.
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblJCCvehno" runat="server" BackColor="Silver" Width="80px"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Button CssClass="button" ID="btn_JCCUpdate" runat="server" Text="Save" OnClick="btn_JCCUpdate_Click"
                                                            TabIndex="25" ToolTip="Save" ValidationGroup="a" Width="58px" /><asp:Button CssClass="button"
                                                                ID="ImageButton7" runat="server" OnClick="btnRPDTCancel_Click" TabIndex="26"
                                                                Text="X" ToolTip="Close" Width="63px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style11">
                                                        ROC Informed
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
                                    <cc1:TabPanel ID="TabPanel9" runat="server" HeaderText="ROC Informed" Visible="false">
                                        <HeaderTemplate>
                                            Tag Cancelation</HeaderTemplate>
                                        <ContentTemplate>
                                            <table class="tblStyle">
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Label ID="Label14" runat="server" Visible="False"></asp:Label><asp:Label ID="Label17"
                                                            runat="server" Visible="False"></asp:Label>
                                                    </td>
                                                    <tr>
                                                        <td valign="top">
                                                            &nbsp;&nbsp;<asp:Label ID="Label18" runat="server" Text="VID"></asp:Label>
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
                                                                            ValidationGroup="aa1" Text="Cancel" /><asp:Button CssClass="button" ID="Button4"
                                                                                runat="server" Text="X" OnClick="btnecncl_Click" ToolTip="Close" Width="63px" />
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
                                    <cc1:TabPanel ID="TabPanel11" runat="server" HeaderText="ROC Informed" Visible="false">
                                        <HeaderTemplate>
                                            Service Advisor</HeaderTemplate>
                                        <ContentTemplate>
                                            <table class="tblStyle">
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Label ID="lblSlNoSA" runat="server" Visible="False"></asp:Label><asp:Label ID="Label10"
                                                            runat="server" Visible="False"></asp:Label>
                                                    </td>
                                                    <tr>
                                                        <td valign="top">
                                                            &nbsp;&nbsp;<asp:Label ID="Label19" runat="server" Text="VIN/VRN"></asp:Label>
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
                                                                            Text="Update" OnClick="btnSAUpdate_Click" /><asp:Button CssClass="button" ID="Button2"
                                                                                runat="server" Text="X" OnClick="btnecncl_Click" ToolTip="Close" Width="63px" />
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
                                                                        &nbsp;&nbsp;<asp:TextBox ID="txtSARemarks" runat="server" TextMode="MultiLine" Width="164px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
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
                                               <tr> <td style="text-align: right; color: Red;">
                                                <asp:Label runat="server" ID="lblmsg"></asp:Label>
                                            </td></tr>
                                    </table><br />
                                    <table>
                                        <tr style="left: 0px">
                                            
                                            <td valign="top">
                                                <table id="UDeliveredTab" runat="server" style="text-align: right;" border="0">
                                                    <tr><td>&nbsp;</td></tr>
                                                    <tr><td>&nbsp;</td></tr>
                                                    <tr>
                                                        <td valign="top">
                                                <asp:RadioButtonList ID="rbType" runat="server" RepeatDirection="Horizontal" AutoPostBack="True"
                                                    OnSelectedIndexChanged="rbType_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True" Value="0">Today&nbsp;&nbsp;</asp:ListItem>
                                                    <asp:ListItem Value="1">Next Day&nbsp;&nbsp;</asp:ListItem>
                                                    <asp:ListItem Value="2">&nbsp;ALL&nbsp;&nbsp;</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>

                                                        <td style="white-space: nowrap;  color: #666666;">
                                                             &nbsp;&nbsp; WORKSHOP STATUS :
                                                        </td>
                                                        <td style="white-space: nowrap;  color: #666666;">
                                                            &nbsp;&nbsp;TOTAL
                                                        </td>
                                                        <td width="25px" style="border-bottom-width: thick; border-width: medium; text-align: center;">
                                                            <asp:Label ID="lbUnDelivered" runat="server" Text="" ForeColor="Black" ></asp:Label>
                                                        </td>
                                                        <td width="10px" style="white-space: nowrap; color: Red; text-align: center;">
                                                           &nbsp;&nbsp; |&nbsp;&nbsp;
                                                        </td>
                                                        <td style="white-space: nowrap;  color: #666666;border-bottom: 2px solid #666666;">
                                                            <img alt="WIP" src="https://res.cloudinary.com/deekyp5bi/image/upload/v1484205149/jcr/Status.png" width='20' height='20' />
                                                            
                                                        </td>
                                                        <td style="white-space: nowrap;  color: #666666;    border-bottom: 2px solid #666666;">
                                                            WIP&nbsp;&nbsp;
                                                        </td>
                                                        <td width="25px" style="border-bottom-width: thick; border-width: medium; text-align: center;    border-bottom: 2px solid #666666;">
                                                            <asp:Label ID="lbWIP" runat="server" Text="" ForeColor="Black" ></asp:Label>
                                                        </td>
                                                        <td width="10px" style="white-space: nowrap; color: Red; text-align: center;">
                                                            &nbsp;&nbsp; |&nbsp;&nbsp;
                                                        </td>
                                                        <td style="white-space: nowrap;  color: #666666;    border-bottom: 2px solid #99C68E;">
                                                            <img alt="Ready" src="https://res.cloudinary.com/deekyp5bi/image/upload/v1484205149/jcr/Status_Ready.png" width='20' height='20' />
                                                            
                                                        </td>
                                                        <td style="white-space: nowrap;  color: #666666;    border-bottom: 2px solid #99C68E;">
                                                            READY&nbsp;&nbsp;
                                                        </td>
                                                        <td width="25px" style=" border-bottom: 2px solid #99C68E; 
                                                            text-align: center;">
                                                            <asp:Label ID="lbReady" runat="server" Text="" ForeColor="Black" ></asp:Label>
                                                        </td>
                                                        <td width="10px" style="white-space: nowrap; color: Red; text-align: center;">
                                                            &nbsp;&nbsp; |&nbsp;&nbsp;
                                                        </td>
                                                        <td style="white-space: nowrap;  color: #666666; border-bottom: 2px solid Red;">
                                                            <img alt="Idle" src="https://res.cloudinary.com/deekyp5bi/image/upload/v1484205149/jcr/Status_IDLE.png" width='20' height='20' />
                                                             
                                                        </td>
                                                        <td style="white-space: nowrap;  color: #666666;border-bottom: 2px solid Red;">
                                                           IDLE&nbsp;&nbsp;
                                                        </td>
                                                        <td width="25px" style="border-bottom: 2px solid Red;
                                                            text-align: center;" >
                                                            <asp:Label ID="lbIdle" runat="server" Text="" ForeColor="Black" ></asp:Label>
                                                        </td>
                                                        <td width="10px" style="white-space: nowrap; color: Red; text-align: center;">
                                                            &nbsp;&nbsp; |&nbsp;&nbsp;
                                                        </td>
                                                        <td style="white-space: nowrap;  color: #666666;border-bottom: 2px solid #688CD9;">
                                                            <img alt="Hold" src="https://res.cloudinary.com/deekyp5bi/image/upload/v1484205149/jcr/Status_HOLD.png" width='20' height='20' />
                                                         
                                                        </td>
                                                        <td style="white-space: nowrap;  color: #666666;border-bottom: 2px solid #688CD9;">
                                                            HOLD&nbsp;&nbsp;
                                                        </td>
                                                        <td width="25px" style="border-bottom: 2px solid #688CD9;
                                                            text-align: center;">
                                                            <asp:Label ID="lbHold" runat="server" Text="" ForeColor="Black" ></asp:Label>
                                                        </td>
                                                        <td width="10px" style="white-space: nowrap; color: Red; text-align: center;">
                                                            &nbsp;&nbsp;|&nbsp;&nbsp;
                                                        </td>
                                                         <td style="white-space: nowrap;  ">
                                                           Total Vehicles received &nbsp;&nbsp;
                                                        </td>
                                                        <td width="25px" style="text-align: center;" >
                                                            <asp:Label ID="lblTotalReceived" runat="server" Text="" ForeColor="Black" ></asp:Label>
                                                        </td>
                                                        <td width="10px" style="white-space: nowrap; color: Red; text-align: center;">
                                                            &nbsp;&nbsp;|&nbsp;&nbsp;
                                                        </td>
                                                         <td style="white-space: nowrap;  ">
                                                           Total Vehicles delivered&nbsp;&nbsp;
                                                        </td>
                                                        <td width="25px" style="text-align: center;" >
                                                            <asp:Label ID="lblVehDel" runat="server" Text="" ForeColor="Black" ></asp:Label>
                                                        </td>
                                                        <td width="10px" style="white-space: nowrap; color: Red; text-align: center;">
                                                            &nbsp;&nbsp;|&nbsp;&nbsp;
                                                     <%--   </td>
                                                            <td style="text-align: right; color: Red;">

                                                <asp:Label runat="server" ID="lblmsg"></asp:Label>
                                            </td>--%>
                                                    </tr>
                                                </table>
                                            </td>
                                        
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table><br />
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
                                 font-size: 12px; text-align: left;">
                            </td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:DropDownList ID="cmbCustType" CssClass="form-control" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                    OnSelectedIndexChanged="cmbCustType_SelectedIndexChanged" TabIndex="1">
                                    <asp:ListItem>Customer Type</asp:ListItem>
                                    <asp:ListItem Value="0">General</asp:ListItem>
                                    <asp:ListItem Value="1">VIP</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:DropDownList ID="cmbServiceType" CssClass="form-control" runat="server" AppendDataBoundItems="True"
                                    AutoPostBack="True" DataSourceID="srcServiceType" DataTextField="ServiceType"
                                    DataValueField="ServiceType" OnSelectedIndexChanged="cmbServiceType_SelectedIndexChanged"
                                    TabIndex="2">
                                    <asp:ListItem>Service Type</asp:ListItem>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="srcServiceType" runat="server" 
                                    SelectCommand="SELECT [ServiceType] FROM [tblServiceTypes]"></asp:SqlDataSource>
                            </td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:DropDownList ID="cmbVehicleModel" runat="server" AppendDataBoundItems="True"
                                    AutoPostBack="True" DataSourceID="srcVehicleModel" DataTextField="Model" DataValueField="Model"
                                    OnSelectedIndexChanged="cmbVehicleModel_SelectedIndexChanged" TabIndex="3" CssClass="form-control"
                                   >
                                    <asp:ListItem>Model</asp:ListItem>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="srcVehicleModel" runat="server" 
                                    SelectCommand="SELECT DISTINCT Model FROM tblVehicleModel"></asp:SqlDataSource>
                            </td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:DropDownList ID="cmbProcess" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                    DataSourceID="SqlDataSource2" DataTextField="ShowProcessName" DataValueField="ShowProcessName"
                                    OnSelectedIndexChanged="cmbProcess_SelectedIndexChanged" TabIndex="4" CssClass="form-control"  >
                                    <asp:ListItem>Process</asp:ListItem>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                                    SelectCommand="SELECT A.ShowProcessName FROM (SELECT ShowProcessName,Min(ProcessDefaultOrder) B FROM tblProcessList group BY ShowProcessName ) A order by A.B">
                                </asp:SqlDataSource>
                            </td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:DropDownList ID="cmbSA" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                    OnSelectedIndexChanged="cmbCustType_SelectedIndexChanged" TabIndex="1" Visible="True"
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
                                    OnSelectedIndexChanged="ddlState_SelectedIndexChanged" CssClass="form-control">
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
                                     <asp:ListItem Value="16">White Board</asp:ListItem>
                                     <asp:ListItem Value="17">Yellow Board</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:DropDownList ID="drpOrderBy" runat="server" AutoPostBack="True" 
                                   CssClass="form-control" OnSelectedIndexChanged="drpOrderBy_SelectedIndexChanged">
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
                                            <table style="width: 399px;">
                                                <tr>
                                                    <td class="style4">
                                                        <asp:TextBox ID="TxtDate1" runat="server" BackColor="White"  TabIndex="5" ValidationGroup="sg" placeholder="From" CssClass="form-control"></asp:TextBox>
                                                        <cc1:TextBoxWatermarkExtender ID="TxtDate1_TextBoxWatermarkExtender" runat="server"
                                                            Enabled="True" TargetControlID="TxtDate1" WatermarkText="From">
                                                        </cc1:TextBoxWatermarkExtender>
                                                        <cc1:CalendarExtender ID="TxtDate1_CalendarExtender" runat="server" Enabled="True"
                                                            TargetControlID="TxtDate1">
                                                        </cc1:CalendarExtender>
                                                    </td>    <td>&nbsp;</td>
                                                    <td class="style4">
                                                        <asp:TextBox ID="TxtDate2" runat="server" BackColor="White" TabIndex="6" placeholder="To" CssClass="form-control"></asp:TextBox>
                                                        <cc1:TextBoxWatermarkExtender ID="TxtDate2_TextBoxWatermarkExtender" runat="server"
                                                            Enabled="True" TargetControlID="TxtDate2" WatermarkText="To">
                                                        </cc1:TextBoxWatermarkExtender>
                                                        <cc1:CalendarExtender ID="TxtDate2_CalendarExtender" runat="server" Enabled="True"
                                                            TargetControlID="TxtDate2">
                                                        </cc1:CalendarExtender>
                                                    </td>    <td>&nbsp;</td>
                                                    <td class="style4">
                                                        <asp:TextBox ID="txtVehicleNumber" runat="server" TabIndex="7" ValidationGroup="sg"
                                                            CssClass="form-control" placeholder="VRN/VIN"></asp:TextBox>
                                                        <cc1:TextBoxWatermarkExtender ID="txtVehicleNumber_TextBoxWatermarkExtender" runat="server"
                                                            Enabled="True" TargetControlID="txtVehicleNumber" WatermarkText="VRN/VIN">
                                                        </cc1:TextBoxWatermarkExtender>
                                                    </td>    <td>&nbsp;</td>
                                                    <td class="style4">
                                                        <asp:TextBox ID="txtTagNo" runat="server" TabIndex="7" placeholder="VID" ValidationGroup="sg" CssClass="form-control"></asp:TextBox>
                                                        <cc1:TextBoxWatermarkExtender ID="txtTagNo_TextBoxWatermarkExtender" runat="server"
                                                            Enabled="True" TargetControlID="txtTagNo" WatermarkText="VID">
                                                        </cc1:TextBoxWatermarkExtender>
                                                    </td>    <td>&nbsp;</td>
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
                            <td>&nbsp;</td>
                            <td style="background-image: url('img/box.png'); background-repeat: no-repeat; background-position: center center;
                                text-align: center; vertical-align: central;padding-left:5px;" width="30">
                                <asp:Label ID="lbVCount" runat="server"  Font-Size="Small" ForeColor="#333333"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div><br />
            </td>
        </tr>
        <tr style="width: 100%; left: 0;">
            <td align="center" style="width: 100%; height: 100%; left: 0;" valign="top">
                <asp:UpdatePanel ID="PnlDisplay" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="10000">
                        </asp:Timer>
                        <table class="GridPanel" border="0" cellpadding="0">
                            <tr>
                                <td style="width: 100%">
                                    <asp:Panel ID="Panel3" runat="server" CssClass="GridPanelJCRHeader" style="background-color:#F9F9F9;">
                                    <%--    <table >
  <tr>
    <th style="width:29px;">SLNO</th>
    <th style="width:70px;text-align:CENTER;">BAY</th>
    <th style="width: 69px;TEXT-ALIGN: RIGHT;">VID</th>
    <th style="width: 69px;TEXT-ALIGN: RIGHT;">VRN</th>
    <th style="width: 87px;TEXT-ALIGN: RIGHT;">VIP CW</th>
    <th style="width: 78px;text-align: right;">MODEL</th>
    <th style="width: 72px;text-align: right;">ST</th>
       <th style="width: 72px;text-align: right;">STATUS</th>
    <th style="width: 72px;text-align: right;">PR.REQ</th>
    <th style="width: 60px;text-align: right;">PR.AVL</th>
    <th style="width: 21px;text-align: right;">BA</th>
    <th style="width: 41px;text-align: right;">T1</th>
    <th style="width: 62px;text-align: right;">T2</th>
    <th style="width: 60px;text-align: right;">T3</th>
    <th style="width: 59px;text-align: right;">WA</th>
    <th style="width: 64px;text-align: right;">WASH</th>
    <th style="width: 42px;text-align: right;">QC</th>
    <th style="width: 72px;text-align: right;">PDT</th>
    <th style="width: 107px;text-align: right;">AGE</th>
     <th style="width: 72px;text-align: right;">RMK</th>
      <th style="width: 72px;text-align: right;">ERT</th>
  </tr>

</table>--%>
                                        <asp:GridView ID="GridView1"  runat="server" CssClass="mydatagrid" Width="100%" CellPadding="0" ForeColor="#507CD1"
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
                                            <FooterStyle BackColor="Silver"  ForeColor="White" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#D1DDF1"  ForeColor="#333333" />
                                            <HeaderStyle BackColor="#f9f9f9"  ForeColor="Black" HorizontalAlign="Center" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <AlternatingRowStyle BackColor="White" />
                                        </asp:GridView>
                                        <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
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
                                        <asp:GridView ID="grdDisplay" runat="server" CssClass="mydatagrid" Width="100%" CellPadding="0" ForeColor="#333333"
                                            OnRowDataBound="grdDisplay_RowDataBound" OnPageIndexChanging="grdDisplay_PageIndexChanging"
                                            OnSelectedIndexChanged="grdDisplay_SelectedIndexChanged" ShowHeader="false" GridLines="Horizontal">
                                            <EmptyDataTemplate>
                                                <center>
                                                    <div class="NoData">
                                                        No Vehicle Received</div>
                                                </center>
                                            </EmptyDataTemplate>
                                          <%--  <Columns>
                                                <asp:BoundField HeaderText="Slno" DataField="#" HeaderStyle-Width="50px"/>
                                            </Columns>--%>
                                            <FooterStyle BackColor="#5D7B9D" ForeColor="White"  />
                                            <HeaderStyle BackColor="#5D7B9D"  ForeColor="White" Height="22px"
                                                HorizontalAlign="Right" />
                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle HorizontalAlign="Center" BackColor="#F5F5F5" ForeColor="#333333" Height="35" />
                                            <EditRowStyle BackColor="#999999" />
                                            <EmptyDataRowStyle Height="48px" />
                                            <SelectedRowStyle BackColor="#E2DED6"  ForeColor="#333333" />
                                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                        </asp:GridView>
                                        <asp:SqlDataSource ID="SqlDataSource5" runat="server" 
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
    <asp:Panel runat="server" ID="plPopUp" >
             <div style="width: 900px">
            <asp:ImageButton ID="btnClose" runat="server" AlternateText="Close" ImageUrl="~/Icons/Close.png"
                Style="position: relative; left: 897px; top: 10px;" ToolTip="Close" />
                <table border="1px" style="background-color: #E6E6E6; text-align: center;" id="Table1">
                    <tr style="background-color: #507CD1; color: White;  font-size: 12px">
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
                          <td class="legData">PROCESS REPEAT ONTIME
                        </td>
                        <td class="legData">PROCESS REPEAT DELAY
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
                            <img alt="Alloted" src="images/JCR/SA Allot.png" class="legimg1" style="width:16px;height:16px;" />
                        </td>
                        <td>
                            <img alt="Delayed" src="images/JCR/SA WIP ONTIME.png" class="legimg1" />
                        </td>
                        <td>
                            <img alt="WIP Near " class="legimg1" src="images/JCR/SA WIP NEAR.png" />
                        </td>
                        <td>
                            <img alt="WIP Delay " class="legimg1" src="images/JCR/SA WIP DELAY.png" />
                        </td>
                       <td>
                            <img alt="Work Completed On Time" class="legimg1" src="https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN.png" />
                        </td> 
                        <td>
                            <img alt=" Work Completed Delay" class="legimg1" src="images/JCR/CR.png" />
                        </td>
                         
                        <td>&nbsp;<img alt="Process Skipped" class="legimg1" src="images/JCR/OutSkipped.png" />
                        </td>
                        <%-- <td>
                            <img alt=" Work Completed Ontime" class="legimg1" src="https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN2.png" />
                        </td>
                          <td>
                            <img alt=" Work Completed Delay" class="legimg1" src="https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CR2.png" />
                        </td>--%>
                        <td colspan="4"></td>
                    </tr>
                    <tr>
                        <td class="legData" style="font-size: 12px;">Work Started On Or Before Schedule
                        </td>
                        <td>
                            <img alt=" Default" src="images/JCR/JA ONTIME.png" class="legimg1" />
                        </td>
                        <td colspan="10"></td>
                    </tr>
                    <tr>
                        <td class="legData" style="font-size: 12px;">Work Started After Schedule
                        </td>
                        <td>
                            <img alt=" Default" src="images/JCR/JA DELAY.png" class="legimg1" />
                        </td>
                        <td colspan="10"></td>
                    </tr>
                    <tr>
                        <td class="legData" style="font-size: 12px;">Technician
                        </td>
                        <td>
                            <img alt=" Alloted" src="images/JCR/TECH ALLOT.png" class="legimg1" />
                        </td>
                        <td>
                            <img alt=" WIP OnTime" src="images/JCR/TECH WIP ONTIME.png" class="legimg1" />
                        </td>
                        <td>
                            <img alt=" WIP Near" src="images/JCR/TECH WIP NEAR.png" class="legimg1" />
                        </td>
                        <td>
                            <img alt=" WIP Delay" src="images/JCR/TECH WIP DELAY.png" class="legimg1" style="width:16px;height:16px;"/>
                        </td>
                        <td>
                            <img alt=" OnTime" src="https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN.png" class="legimg1" />
                        </td>
                        <td>
                            <img alt=" Delay" src="images/JCR/CR.png" class="legimg1" />
                        </td>
                        <td>
                            <img alt=" Process Skipped" src="images/JCR/OutSkipped.png" class="legimg1" />
                        </td>
                       <%--  <td>
                            <img alt=" Work Completed Ontime" class="legimg1" src="https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN2.png" />
                        </td>
                          <td>
                            <img alt=" Work Completed Delay" class="legimg1" src="https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CR2.png" />
                        </td>--%>
                        <td colspan="4"></td>
                    </tr>
                    <tr>
                        <td class="legData" style="font-size: 12px;">Wheel Alignment
                        </td>
                        <td>
                            <img alt=" Alloted" src="images/JCR/WA ALLOT.png" class="legimg1" />
                        </td>
                        <td>
                            <img alt=" WIP OnTime" src="images/JCR/WA WIP.png" class="legimg1" />
                        </td>
                        <td>
                            <img alt=" WIP Near" src="images/JCR/WA WIP NEAR.png" class="legimg1" />
                        </td>
                        <td>
                            <img alt=" WIP Delay" src="images/JCR/WA WIP DELAY.png" class="legimg1" />
                        </td>
                        <td>
                            <img alt=" OnTime" src="https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN.png" class="legimg1" />
                        </td>
                        <td>
                            <img alt=" Delay" src="images/JCR/CR.png" class="legimg1" />
                        </td>
                        <td>
                            <img alt=" Process Skipped" src="images/JCR/OutSkipped.png" class="legimg1" />
                        </td>
                         <td>
                            <img alt=" Work Completed Ontime" class="legimg1" src="https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN2.png" />
                        </td>
                           <td>
                            <img alt=" Work Completed Delay" class="legimg1" src="https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CR2.png" />
                        </td>
                          <td>
                            <img alt=" Work Completed Delay" class="legimg1" src="https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/notRequired.png" />
                        </td>
                        <td ></td>
                    </tr>
                    <tr>
                        <td class="legData" style="font-size: 12px;">Road Test
                        </td>
                        <td></td>
                        <td>
                            <img alt=" WIP Near" src="images/JCR/RT ONTIME.png" class="legimg1" />
                        </td>
                        <td colspan="2"></td>
                        <td>
                            <img alt=" OnTime" src="https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN.png" class="legimg1" />
                        </td>
                        <td></td>
                        <td>
                            <img alt=" Process Skipped" src="images/JCR/OutSkipped.png" class="legimg1" />
                        </td>
                         <td>
                            <img alt=" Work Completed Ontime" class="legimg1" src="https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN2.png" />
                        </td>
                           <td>
                            <img alt=" Work Completed Delay" class="legimg1" src="https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CR2.png" />
                        </td>
                        <td colspan="2"></td>
                    </tr>
                    <tr>
                        <td class="legData" style="font-size: 12px;">Wash
                        </td>
                        <td>
                            <img alt=" Alloted" src="images/JCR/WASH allot.png" class="legimg1" />
                        </td>
                        <td>
                            <img alt=" WIP OnTime" src="images/JCR/WASH WIP.png" style="height: 16px;width: 16px;" class="legimg1" />
                        </td>
                        <td>
                            <img alt=" WIP Near" src="images/JCR/WASH WIP NEAR.png" class="legimg1" />
                        </td>
                        <td>
                            <img alt=" WIP Delay" src="images/JCR/WASH WIP Delay.png" class="legimg1" />
                        </td>
                        <td>
                            <img alt=" OnTime" src="https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN.png" class="legimg1" />
                        </td>
                        <td>
                            <img alt=" Delay" src="images/JCR/CR.png" class="legimg1" />
                        </td>
                        <td>
                            <img alt=" Process Skipped" src="images/JCR/OutSkipped.png" class="legimg1" />
                        </td>
                         <td>
                            <img alt=" Work Completed Ontime" class="legimg1" src="https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN2.png" />
                        </td>
                           <td>
                            <img alt=" Work Completed Delay" class="legimg1" src="https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CR2.png" />
                        </td>
                        <td colspan="2"></td>
                    </tr>
                    <tr>
                        <td class="legData" style="font-size: 12px;">QC
                        </td>
                        <td></td>
                        <td>
                            <img alt=" WIP OnTime" src="images/JCR/QC WIP.png" class="legimg1" />
                        </td>
                        <td>
                            <img alt=" WIP Near" src="images/JCR/QC WIP Near.png" class="legimg1" />
                        </td>
                        <td>
                            <img alt=" WIP Delay" src="images/JCR/QC WIP Delay.png" class="legimg1" />
                        </td>
                        <td>
                            <img alt=" OnTime" src="https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN.png" class="legimg1" />
                        </td>
                        <td>
                            <img alt=" Delay" src="images/JCR/CR.png" class="legimg1" />
                        </td>
                        <td>
                            <img alt=" Process Skipped" src="images/JCR/OutSkipped.png" class="legimg1" />
                        </td>
                         <td>
                            <img alt=" Work Completed Ontime" class="legimg1" src="https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN2.png" />
                        </td>
                           <td>
                            <img alt=" Work Completed Delay" class="legimg1" src="https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CR2.png" />
                        </td>
                        <td colspan="2"></td>
                    </tr>
                    
                    <tr>
                        <td class="style1" style="font-size: 12px;">JCC
                        </td>
                        <td colspan="4"></td>
                        <td>
                            <img alt=" OnTime" src="images/JCR/JCC ONTIME.png" class="legimg1" />
                        </td>
                        <td colspan="5"></td>
                        <td>
                            <img alt="Process Skipped" src="images/JCR/JCC SKIP.png" class="legimg1" />
                        </td>
                        <%-- <td>
                            <img alt=" Work Completed Delay" class="legimg1" src="https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN2.png" />
                        </td>--%>
                    </tr>
                    <tr>
                        <td class="style1" style="font-size: 12px;">PDT Status
                        </td>
                        <td colspan="3"></td>
                        <td>
                            <img alt="Approching" src="images/JCR/circle_yellow.png"  style="height: 16px;width: 16px;" />
                        </td>
                        <td>
                            <img alt="Delay" src="images/JCR/circle_red.png" style="height: 16px;width: 16px;"  />
                        </td>
                        <td>
                            <img alt="OnTime" src="images/JCR/circle_green.png" style="height: 16px;width: 16px;"  />
                        </td>
                        <td colspan="5"></td>
                    </tr>
                </table>
        </div>
        </asp:Panel>
    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
        PopupControlID="plPopUp" TargetControlID="btn_help" CancelControlID="btnClose">
    </cc1:ModalPopupExtender>
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

    <script type="text/javascript" src="js/classie.js"></script>
        <script type="text/javascript" src="js/gnmenu.js"></script>
        <script type="text/javascript">
            new gnMenu(document.getElementById('gn-menu'));
        </script>
</body>
</html>