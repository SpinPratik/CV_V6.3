<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="WashDisplay.aspx.cs"
    Inherits="WashDisplay" %>

<%@ Register Src="Vehicle.ascx" TagName="Vehicle" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="Icons/ProTRAC Icon.ico" rel="shortcut icon" type="image/x-icon" />
<%--    <link href="css/Stylesheet.css" rel="stylesheet" type="text/css" />--%>
  <%--  <script src="JS/jquery-1.3.2.js" type="text/javascript"></script>
    <script src="JS/jquery.gritter.min.js" type="text/javascript"></script>
    <script src="JS/customgitter.js" type="text/javascript"></script>
    <link href="css/cupertino/jquery-ui-1.8.1.custom.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="js/jquery-ui-1.8.1.custom.min.js" type="text/javascript"></script>
    <script src="js/jquery.gritter.min.js" type="text/javascript"></script>
    <link href="css/jquery.gritter.css" rel="stylesheet" type="text/css" />
    <link href="css/jquery.gritter2.css" rel="stylesheet" type="text/css" />
    <script src="js/customgitter.js" type="text/javascript"></script>--%>
    <%--<link href="CSS/ProTRAC_Css.css" rel="stylesheet" type="text/css" />

      <link rel="stylesheet" type="text/css" href="CSS/normalize.css" />
 <%--   <link rel="stylesheet" type="text/css" href="CSS/demo.css" />--%>
 <%--   <link rel="stylesheet" type="text/css" href="CSS/component.css" />--%>--%>

      <link href="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484214710/css/ProTRAC_Css.css" rel="stylesheet" type="text/css" />
        <link rel="stylesheet" type="text/css" href="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484285134/css/component.css" />

      <link href="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484285057/Bootstrap/bootstrap.min.css" rel="stylesheet" />
  <script type="text/javascript" src="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484285057/Bootstrap/bootstrap.min.js"></script>




     <link type="text/css" rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.6.3/css/font-awesome.min.css"/>
  <%--  <link href="Bootstrap/bootstrap.min.css" rel="stylesheet" />
  <script type="text/javascript" src="Bootstrap/bootstrap.min.js"></script>--%>
    <script type="text/javascript">
        function goSupport() {
            window.open('http://www.support.vtabs.in', '_blank');
        }


        function goHelp() {
            window.open('Help/index.htm');
        }
        function goAbout() {
            window.open('http://www.spintech.in', '_blank');
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
    <script>        history.go(1)</script>
    <%--NOTIFICATION--%>
   <%-- <link href="CSS/notify-osd.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery-1.4.4.min.js" type="text/javascript"></script>
    <script src="js/notify-osd.js" type="text/javascript"></script>
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
        }--%>
    </script>
    <style type="text/css">
        table a
        {
            text-align: center;
            padding-right: 25%;
            padding-left: 25%;
            display: block;
            text-decoration: none;
            font-size: 13px;
            font-weight: bold;
            color: White;
        }
        table a:hover
        {
            color: Black;
        }
        a:hover{
            text-decoration:none !important;
        }
    </style>
    <title>Wash Display</title>
</head>
<body onload="showDetails()">
    <form id="form1" runat="server">
    <table style="position: fixed; width: 100%; height: 100%; top: 0px; right: 0px; bottom: 0px;
        left: 0px; background-color: #F5F5F5;" border="0" cellpadding="0" cellspacing="0">
        <%--<tr>
            <td style="background-image: url('Layout/VTABS.png'); background-repeat: no-repeat;
                background-size: 100% 100%; height: 115px; width: 100%;">
                <table style="width: 100%; height: 100%;" border="0" cellpadding="0" cellspacing="0">
                    <tr style="height: 35px">
                        <td style="width: 29%" />
                        <td style="width: 5%" align="center" valign="middle">
                           <asp:ImageButton ID="btn_Back" runat="server" src="Layout/back.png" Width="16" Height="12"
                                OnClick="btn_Back_Click" />
                            <asp:ImageButton ID="btn_noBack" runat="server" src="Layout/noback.png" Width="16"
                                Height="12" Visible="false"/>
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
                    <tr style="height: 25px; color: #333333; font-size: 13px;
                        font-weight: bold;">
                        <td style="padding-left: 8px" colspan="3">
                            <asp:Label ID="lbScroll0" runat="server"></asp:Label>
                        </td>
                        <td style="padding-right: 8px; text-align: right;">
                            <asp:Label ID="lbl_CurrentPage" runat="server" Text="WASH DISPLAY "/>|
                            <asp:Label ID="lbl_LoginName" runat="server" Text="Display"/>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>--%>
        <tr>
            <td>
               <div class="" style="height:100%;min-width:1100px;">
        
            <ul id="gn-menu" class="gn-menu-main" style="background-color: #1591cd !important;">
                 <%--<li>
                    <a href="DisplayHome.aspx">  <asp:ImageButton ImageUrl="images/left-arrow%20(1).png" OnClick="back_Click" ID="back" runat="server" style="margin-top:15px;"></asp:ImageButton></a>
                </li>--%>
                <li><a href="#" style="color: white; font-size: 21px;"><img src="https://res.cloudinary.com/deekyp5bi/image/upload/v1484219853/images/wms--logo.png" style=" margin-top: 15px;"/></a></li>
                <li style="width:225px;">
                    <a href="#"> <img src="Images/user.png" />&nbsp;&nbsp;
                        <asp:Label runat="server" ID="lbl_LoginName" style="color:white;text-transform:uppercase;text-decoration:none !important;"></asp:Label>               
                    </a>
                </li>
               <li style="width:225px;border-right:unset !important;"><i class="fa fa-clock-o" style="color:white"></i> &nbsp;&nbsp;<asp:Label class="" ID="lbl_currTime" Font-Bold="true" style="color: white;text-transform:uppercase !important" runat="server" ></asp:Label></li>
  <li style="border-left: 1px solid #c6d0da;"><a style="color: white;" href="#">
                    <img src="https://res.cloudinary.com/deekyp5bi/image/upload/v1484219853/images/spin_logo.png" /></a></li> 
                <li>  
                  <asp:LinkButton ID="btn_logout" OnClick="btn_logout_Click" style="vertical-align: middle;color:white;" Text="logout"  runat="server"></asp:LinkButton>
                  </li>            
               
            </ul>
       
        <br />
        </div>
            </td>
        </tr>
        <tr>
            <td style="height: 100%; text-align: left; vertical-align: top;
                font-size: 14px; font-weight: normal;">
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <asp:Timer ID="Timer1" runat="server" Interval="10000" OnTick="Timer1_Tick">
                </asp:Timer>
                <table style="width: 100%;" cellspacing="0" cellpadding="0" border="0">
                    <tr id="pnl_Display" runat="server">
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <center>
        <p style="position: fixed; width: 100%; height: 5px; bottom: 0px; left: 0px; right: 0px;
           font-size: 11px;">
            <asp:Label ID="lblVersion" runat="server"></asp:Label>
        </p>
    </center>
    </form>
</body>
</html>