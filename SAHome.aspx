<%@ Page Title="" Language="C#" MasterPageFile="~/AdminModule_New.master" AutoEventWireup="true"
    CodeFile="SAHome.aspx.cs" Inherits="SAHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 154px;
            height: 28px;
        }
        .style2
        {
            height: 28px;
        }
        .style3
        {
            width: 154px;
        }
    </style>
    <script type="text/javascript">
        function stopRKey(evt) {
            var evt = (evt) ? evt : ((event) ? event : null);
            var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
            if ((evt.keyCode == 8 || evt.KeyCode == 46)) { return false; }
        }
        document.onkeypress = stopRKey;
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="height: 565px; margin-bottom: 30px; vertical-align: middle;">
        <table style="height: 100%; width: 100%">
            <tr>
                <td colspan="3" />
            </tr>
            <tr>
                <td />
                <td align="center" valign="middle">
                    <table style="width: 309px; text-align: center;">
                        <tr>
                            <td class="style3">
                                <center style="width: 158px">
                                    <asp:ImageButton ID="btnJCC" runat="server" Height="69px" ImageUrl="~/img/JobController.png"
                                        OnClick="btnJobController_Click" AlternateText="Job Controller" />
                                </center>
                            </td>
                            <td>
                                <center style="width: 199px">
                                    <asp:ImageButton ID="btnJCDisplay" runat="server" Height="69px" BorderStyle="Solid"
                                        ImageAlign="Bottom" ImageUrl="~/img/display.png" ToolTip="Job Controller Displays"
                                        Width="80px" OnClick="btnJCDisplay_Click" AlternateText="Report" />
                                </center>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                <asp:HyperLink ID="HyperLink1" runat="server"  Font-Underline="False" style="text-transform:uppercase"
                                    ForeColor="#333333" NavigateUrl="~/NewJobCardCreation.aspx">Job Card Entry</asp:HyperLink>
                            </td>
                            <td align="center" class="style2">
                                <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/DisplayWorksI.aspx" style="text-transform:uppercase;text-decoration: none;" ForeColor="#333333">Service Advisor Display</asp:HyperLink>
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
</asp:Content>