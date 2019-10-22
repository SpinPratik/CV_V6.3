<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JCCHome.aspx.cs" Inherits="JCCHome" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Controller Home</title>
    <link href="CSS/ProTRAC_Css.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function goSupport() {
            window.open('Complain.aspx');
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
</head>
<body onload="showDetails()">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <%--    NOTIFICATION PANEL  --%>
    <asp:UpdatePanel ID="updTime" runat="server">
        <ContentTemplate>
            <asp:Timer ID="NotificationTimer" runat="server" Interval="30000">
            </asp:Timer>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="commonFont">
        <table class="fullStyle" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td>
                    <table class="fullStyle" bgcolor="#0099FF">
                        <tr>
                            <td align="left" class="AdminHeaderParts">
                                <asp:Label ID="lbScroll0" runat="server" Font-Bold="True" ForeColor="White" Font-Size="Medium"></asp:Label>
                            </td>
                            <td align="center" class="AdminHeaderParts">
                                <asp:Image ID="Image1" runat="server" Height="39px" ImageUrl="~/images/ProTracCL.PNG"
                                    Width="111px" />
                            </td>
                            <td align="right" class="AdminHeaderParts">
                                <asp:Button CssClass="button" ID="btn_Support" runat="server" OnClientClick="goSupport()"
                                    Text="Support" />
                                <asp:Button CssClass="button" ID="btn_logout" runat="server" OnClick="btn_logout_Click"
                                    Text="Logout" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table class="fullStyle" cellpadding="0" cellspacing="0">
                        <tr>
                            <td colspan="3" align="left" height="23px" valign="middle">
                                <table class="fullStyle" bgcolor="#0099FF">
                                    <tr>
                                        <td style="width: 25%;">
                                            <asp:Label ID="lbl_LoginName" runat="server" Font-Bold="True" ForeColor="#DDDDFF"></asp:Label>
                                        </td>
                                        <td style="width: 50%; text-align: center; height: 25px;">
                                            <asp:Label ID="lbl_CurrentPage" runat="server" Font-Bold="True" ForeColor="#DDDDFF"></asp:Label>
                                        </td>
                                        <td style="width: 25%;">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table class="fullStyle" style="height: 630px;">
            <tr>
                <td colspan="3" align="center" style="height: 580px; vertical-align: middle;">
                    <table class="fullStyle">
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td align="center" colspan="2" style="padding-right: 20px; height: auto;" valign="middle">
                                <table style="width: 540px; text-align: center; font-family: arial; height: 200px;">
                                    <tr>
                                        <td>
                                            <table style="width: 100%; height: 100%">
                                                <tr>
                                                    <td style="width: 17%; height: 100%">
                                                    </td>
                                                    <td style="width: 33.33%; height: 100%">
                                                        <asp:ImageButton ID="btnAppointments" runat="server" Height="69px" ImageUrl="~/img/JobController.png"
                                                            OnClick="btnAppointments_Click" AlternateText="Job Controller" />
                                                        <br />
                                                        <asp:HyperLink ID="HyperLink2" runat="server" Font-Bold="True" Font-Underline="False"
                                                            ForeColor="#333333" NavigateUrl="~/JobAllotment.aspx">JobSlip</asp:HyperLink>
                                                    </td>
                                                    <td style="width: 33.33%; height: 100%">
                                                        <br />
                                                        <asp:ImageButton ID="btnJobController" runat="server" Height="69px" ImageUrl="~/img/JobController.png"
                                                            OnClick="btnJobController_Click" AlternateText="Job Controller" />
                                                        <br />
                                                        <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="True" Font-Underline="False"
                                                            ForeColor="#333333" NavigateUrl="~/DisplayWorks.aspx">Job
                                                        Controller</asp:HyperLink>
                                                    </td>
                                                    <td style="width: 17%; height: 100%">
                                                        <br />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td align="right" style="padding-right: 20px;">
                            </td>
                            <td align="left" style="padding-left: 20px;">
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table class="fullStyle">
            <tr>
                <td align="center" bgcolor="#FFFFFF" class="AdminHeaderParts">
                    <asp:Image ID="Image2" runat="server" Height="45px" ImageUrl="~/images/Org.png" />
                </td>
                <td align="center" bgcolor="#ffffff" class="AdminHeaderParts" valign="middle">
                    <asp:Label ID="lblVersion" runat="server" Font-Bold="False" ForeColor="#000099" Font-Size="Small"></asp:Label>
                </td>
                <td align="center" bgcolor="#FFFFFF" class="AdminHeaderParts" valign="top">
                    <asp:Image ID="Image3" runat="server" ImageUrl="~/images/spin.png" Height="45px"
                        Width="150px" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>