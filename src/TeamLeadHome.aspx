<%@ Page Title="" Language="C#" MasterPageFile="~/AdminModule.master" AutoEventWireup="true" CodeFile="TeamLeadHome.aspx.cs" Inherits="TeamLeadHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">

        function stopRKey(evt) {
            var evt = (evt) ? evt : ((event) ? event : null);
            var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
            if ((evt.keyCode == 8 || evt.KeyCode == 46)) { return false; }
        }

        document.onkeypress = stopRKey;
    </script>
    <style type="text/css">
        .style1
        {
            width: 163px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <div style="height: 565px; margin-bottom: 30px; vertical-align: middle;">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
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
                                <asp:ImageButton ID="btnDisplay" runat="server" Height="59px" ImageUrl="~/MenuImages/Bay Progress.png"
                                    OnClick="btnDisplay_Click" AlternateText="BayDisplay" />
                            </td>
                            <td>
                                <asp:ImageButton ID="btnAllotment" runat="server" Height="69px" ImageUrl="~/img/appointment.png"
                                    OnClick="btnAllotment_Click" AlternateText="JobAllotment" Width="78px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                <asp:HyperLink ID="HyperLink3" runat="server" Font-Bold="True" Font-Underline="False"
                                    ForeColor="#333333" NavigateUrl="~/Baydisplay_TLWise.aspx">Bay Display</asp:HyperLink>
                            </td>
                            <td align="center">
                                <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="True" Font-Underline="False"
                                    ForeColor="#333333" NavigateUrl="~/JobAllotment.aspx" Width="116px">Job Allotment</asp:HyperLink>
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

