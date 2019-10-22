<%@ Page Title="" Language="C#" MasterPageFile="~/AdminModule.master" AutoEventWireup="true"
    CodeFile="JCHome.aspx.cs" Inherits="JCHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
                    <table style="width: 334px; text-align: center;">
                        <tr>
                            <td class="style1">
                                <asp:ImageButton ID="btnJobController" runat="server" Height="69px" ImageUrl="~/img/JobController.png"
                                    OnClick="btnJobController_Click" AlternateText="Job Controller" />
                            </td>
                            <td>
                                <asp:ImageButton ID="btnAppointments" runat="server" Height="69px" ImageUrl="~/img/appointment.png"
                                    OnClick="btnAppointments_Click" AlternateText="Appointment" />
                            </td>
                            <td>
                                <asp:ImageButton ID="btnCRMDB" runat="server" Height="69px" ImageUrl="~/images/crmdb.png"
                                    OnClick="btnCRMDB_Click" AlternateText="KPI Dashboard" />
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="True" Font-Underline="False"
                                    ForeColor="#333333" NavigateUrl="~/DisplayWorks.aspx">Job
                                                        Controller</asp:HyperLink>
                            </td>
                            <td align="left">
                                <asp:HyperLink ID="HyperLink2" runat="server" Font-Bold="True" Font-Underline="False"
                                    ForeColor="#333333" NavigateUrl="~/NewJobAllotment.aspx">Job
                                                        Allotment</asp:HyperLink>
                            </td>
                            <td>
                                <asp:HyperLink ID="HyperLink3" runat="server" Font-Bold="True" Font-Underline="False"
                                    ForeColor="#333333" NavigateUrl="~/KPIDashboard.aspx">KPI<br /> Dashboard</asp:HyperLink>
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