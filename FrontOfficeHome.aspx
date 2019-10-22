<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrontOfficeHome.aspx.cs"
    Inherits="FrontOfficeHome" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Front Office Home</title>
    <link href="CSS/ProTRAC_Css.css" rel="stylesheet" type="text/css" />
    <link href="CSS/ProTRAC_Css.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function goSupport() {
            window.open('http://www.support.vtabs.in', '_blank');
        }

        function goHelp() {
            window.open('Help/index.htm');
        }
    </script>
    <%--NOTIFICATION--%>
    <link href="CSS/notify-osd.css" rel="stylesheet" type="text/css" />
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
        }
    </script>
    <script src="JS/jquery-1.3.2.js" type="text/javascript"></script>
    <script src="JS/jquery.gritter.min.js" type="text/javascript"></script>
    <link href="css/jquery.gritter.css" rel="stylesheet" type="text/css" />
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
            // alert("Success: \n"+pushstring);
            //alert(pushstring);
        }

        function OnGitterLoadFailed() {
            alert("Failure");
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
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ScriptManager>
    <%--    NOTIFICATION PANEL  --%>
    <asp:UpdatePanel ID="updTime" runat="server">
        <ContentTemplate>
            <asp:Timer ID="NotificationTimer" runat="server" Interval="30000" OnTick="NotificationTimer_Tick">
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
                                Visible="False" />
                            <asp:ImageButton ID="btn_noBack" runat="server" src="Layout/noback.png" Width="16"
                                Height="12" />
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
            <td style="height: 100%; text-align: left; vertical-align: top; font-family: Consolas, Georgia;
                font-size: 14px; font-weight: normal;">
                <table style="width: 100%; height: 100%;">
                    <tr>
                        <td>
                            <div style="height: 565px; margin-bottom: 30px; vertical-align: middle; width: 100%;">
                                <table style="height: 100%; width: 100%">
                                    <tr>
                                        <td colspan="3" />
                                    </tr>
                                    <tr>
                                        <td />
                                        <td align="center" valign="middle">
                                            <table style="width: 334px; text-align: center;" class="tblStyle">
                                                <tr>
                                                    <td class="style1">
                                                        <asp:ImageButton ID="btnFOE2" runat="server" Height="69px" ImageUrl="~/img/JobController.png"
                                                            AlternateText="Tag Assignment" OnClick="btnFOE2_Click" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="btnFOD" runat="server" Height="69px" ImageUrl="~/img/Display.png"
                                                            AlternateText="Front Office Display" OnClick="btnFOD_Click" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style1">
                                                        <asp:HyperLink ID="HyperLink9" runat="server" Font-Bold="True" Font-Underline="False"
                                                            ForeColor="#333333" NavigateUrl="~/TagAllotment.aspx">Tag Assignment</asp:HyperLink>
                                                    </td>
                                                    <td align="left">
                                                        <asp:HyperLink ID="HyperLink5" runat="server" Font-Bold="True" Font-Underline="False"
                                                            ForeColor="#333333" NavigateUrl="~/FrontOfficeDisplayStatus.aspx">Front Office
 Display</asp:HyperLink>
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
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <center>
        <p style="position: fixed; width: 100%; height: 5px; bottom: 0px; left: 0px; right: 0px;
            font-family: Consolas, Georgia; font-size: 11px;">
            <asp:Label ID="lblVersion" runat="server"></asp:Label>
        </p>
    </center>
    </form>
</body>
</html>