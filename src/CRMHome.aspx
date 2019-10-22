<%@ Page Title="" Language="C#" MasterPageFile="~/AdminModule_New.master" AutoEventWireup="true"
    CodeFile="CRMHome.aspx.cs" Inherits="CRMHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function stopRKey(evt) {
            var evt = (evt) ? evt : ((event) ? event : null);
            var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
            if ((evt.keyCode == 8 || evt.KeyCode == 46)) { return false; }
        }

        document.onkeypress = stopRKey;
    </script>
     <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript">
    function ShowProgress() {
        setTimeout(function () {
            var modal = $('<div />');
            modal.addClass("modal");
            $('body').append(modal);
            var loading = $(".loading");
            loading.show();
            var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
            var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
            loading.css({ top: top, left: left });
        }, 200);
    }
    $('form').live("submit", function () {
        ShowProgress();
    });
</script>
    <style>
          .background-white {
            background-color: white;
            /*padding: 10px 10px;*/
            box-shadow: 0px 0px 1px #d5d5d5;
            color: black;
            box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);
        }

        .second {
            border-bottom: 6px solid #76c187;
        }

        .first {
            border-bottom: 6px solid #5f9eee;
        }

        .third {
            border-bottom: 6px solid orangered;
        }

        .forth {
            border-bottom: 6px solid #9e7da6;
        }
        div a{
            text-transform:uppercase;
            font-weight:500;
        }
        .fusion-fullwidth{
            border-color: #eae9e9;
            border-bottom-width: 0px;
            border-top-width: 0px;
            border-bottom-style: solid;
            border-top-style: solid;
            padding-bottom: 120px;
            padding-top: 155px;
            padding-left: 20px;
            padding-right: 20px;
            padding-left: 20px !important;
            padding-right: 20px !important;
            background-color: #ffffff;
            background-position: center center;
            background-repeat: repeat;
            background-image: url(images/background_home.jpg);
        }
        .background-image{
                border-width: 0px;
    height: 230px;
    width: 100%;
    border-bottom: 1px solid rgba(0, 0, 0, 0.1);
        }
    </style>
    <style type="text/css">
    .modal
    {
        position: fixed;
        top: 0;
        left: 0;
        background-color: black;
        z-index: 99;
        opacity: 0.8;
        filter: alpha(opacity=80);
        -moz-opacity: 0.8;
        min-height: 100%;
        width: 100%;
    }
    .loading
    {
        font-family: Arial;
        font-size: 10pt;
      /* border: 5px solid #67CFF5;*/
        width: 100%;
        height: 100%;
        display: none;
        position: fixed;

        background-color: #808080;
        opacity:0.8;
        z-index: 999;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="height: 565px; margin-bottom: 30px; vertical-align: middle;">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="loading" align="center">
   
    <br />
    <img src="loading.gif" alt="" /><br />
        <span style="color:#ffffff;  text-transform: capitalize;">Loading... Please Wait.</span> <br />
</div>
        <div class="fusion-fullwidth">
        <div class="container">
          <div class="row count-cards">
                   
                    <div class="col-md-6 col-sm-6">
                        <div class="second background-white">
                              <%--  <asp:ImageButton src="images/display-bg.jpg" runat="server" style="border-bottom: 1px solid rgba(0, 0, 0, 0.15);" CssClass="background-image" ID="img_display" OnClick="btnCRMDisplay_Click" /><br />
                              --%>  
                            <div style="margin: 20px;">
          <center> <asp:HyperLink ID="HyperLink6" NavigateUrl="DisplayWorks.aspx" runat="server" onclick="ShowProgress()" Text="display"></asp:HyperLink></center></div>
                        </div>
                    </div>
                    <div class="col-md-6 col-sm-6">
                        <div class="third background-white">
                         <%--   <asp:ImageButton src="images/RTEPORTS_NEW.jpg" runat="server" CssClass="background-image" style="border-bottom: 1px solid rgba(0, 0, 0, 0.15);" OnClick="btnReport_Click" /><br />
                       --%>
                             <div style="margin: 20px;">    <center>
          <asp:HyperLink ID="HyperLink5" NavigateUrl="CrmReports.aspx" runat="server" onclick="ShowProgress()" Text="reports"></asp:HyperLink></center></div>
                        </div>
                    </div>
                    <div class="col-md-4 col-sm-6">
                        <%--<div class="forth background-white">--%>
                           <%--  <asp:ImageButton src="images/kpi_background.png" CssClass="background-image" style="border-bottom: 1px solid rgba(0, 0, 0, 0.15);" runat="server" OnClick="btnCRMDB_Click" /><br />
                           
                            --%>
                           <%-- <div style="margin: 20px;"> <center>
                                  <asp:HyperLink ID="HyperLink4" NavigateUrl="~/KPI_New.aspx" onclick="ShowProgress()" Text="kpi dashboard" runat="server"></asp:HyperLink> </center></div>
                        </div>
                    </div>--%>

                </div></div>
        </div>

      <%--  <table style="height: 100%; width: 100%">
            <tr>
                <td colspan="3" />
            </tr>
            <tr>
                <td />
                <td align="center" valign="middle">
                    <table style="width: 334px; text-align: center;" class="tblStyle">
                        <tr>
                            <td class="style1">
                                <asp:ImageButton ID="btndisplay" runat="server" Height="69px" ImageUrl="~/img/display.png"
                                    OnClick="btnCRMDisplay_Click" AlternateText="CRM Display" />
                            </td>
                            <td>
                                <asp:ImageButton ID="btnReport" runat="server" Height="69px" ImageUrl="~/images/reports.png"
                                    OnClick="btnReport_Click" AlternateText="Report" />
                            </td>
                            <td>
                                <asp:ImageButton ID="btnCRMDB" runat="server" Height="69px" ImageUrl="~/images/crmdb.png"
                                    OnClick="btnCRMDB_Click" AlternateText="KPI Dashboard" />
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                <asp:HyperLink ID="HyperLink2" runat="server" Font-Underline="False" Font-Names="Consolas, Georgia"
                                    ForeColor="#333333" NavigateUrl="~/DisplayWorksI.aspx">CRM Display</asp:HyperLink>
                            </td>
                            <td align="left">
                                <asp:HyperLink ID="HyperLink3" runat="server" Font-Underline="False" Font-Names="Consolas, Georgia"
                                    ForeColor="#333333" NavigateUrl="~/Reports.aspx">Reports</asp:HyperLink>
                            </td>
                            <td>
                                <asp:HyperLink ID="HyperLink1" runat="server" Font-Underline="False" Font-Names="Consolas, Georgia"
                                    ForeColor="#333333" NavigateUrl="~/KPIDashboard.aspx">KPI Dashboard</asp:HyperLink>
                            </td>
                        </tr>
                    </table>
                </td>
                <td />
            </tr>
            <tr>
                <td colspan="3" />
            </tr>
        </table>--%>
    </div>
</asp:Content>