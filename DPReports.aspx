<%@ Page Title="" Language="C#" MasterPageFile="~/AdminModule.master" AutoEventWireup="true"
    CodeFile="DPReports.aspx.cs" Inherits="DPReports" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484214730/Bootstrap/bootstrap.min.css" rel="stylesheet" />
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



    <style type="text/css">
        .Menustyle {
            width: 100%;
            white-space: nowrap;
            overflow: hidden;
        }

        .MenuHead {
            text-align: center;
            vertical-align: middle;
            color: rgba(51, 51, 51, 0.83);
            /* background-color: ThreeDFace; */
            font-weight: bold;
            border-color: darkgray;
            white-space: nowrap;
            text-transform: uppercase;
            padding:10px;
        }
        .MenuHead td{
                padding: 10px;
        }
        .DateStyle {
            width: 100%;
            text-align: center;
            color: #333333;
        }

        .Empstyle {
            width: 100%;
        }

        .style3 {
            height: 30px;
        }

        .HeaderLeft {
            position: relative;
            height: 50px;
            width: 225px;
            background-image: url( 'Images/ProTRAC Logo.png' );
            background-repeat: no-repeat;
        }

        .HeaderCenter {
            font-size: 30px;
            color: #2D4A98;
        }

        .HeaderRight {
            height: 50px;
            width: 151px;
            background-image: url( 'Images/spin.png' );
            background-repeat: no-repeat;
            position: absolute;
            right: 0px;
        }

        body {
            margin: 0px;
            top: 0px;
            left: 0px;
            width: 100%;
            height: 100%;
            background-color: #e0ecff; /*D8DFEA;*/
            font-family: Arial;
        }

        .Report1 {
            position: absolute;
            height: 70%;
            width: 100%;
            overflow: scroll;
        }

        .newStyle1 {
            font-size: medium;
            font-weight: bold;
            color: #336699;
        }

        .cssAccHead {
            /*background-color: #DBDBDB;*/
            color: Black;
            padding:10px;
        }

        .cssAccContent {
            color: Black;
        }

        .style4 {
            width: 357px;
            font-family: Arial;
            font-size: small;
        }

        .style5 {
            width: 345px;
            font-size: small;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#MyAccordion").click(function () {
                if ($("#imgScroller").src != "~/imgButton/arrow-up.png") {
                    $("#imgScroller").src = "~/imgButton/arrow-up.png";

                } else {
                    $("#imgScroller").src = "~/imgButton/arrow-down.png";
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="loading" align="center">
   
    <br />
    <img src="loading.gif" alt="" /><br />
        <span style="color:#ffffff;  text-transform: capitalize;">Loading... Please Wait.</span> <br />
</div>

<a href="DPHome.aspx" title="Back to DP Dashboard">
        <div style="position:absolute;top:15px;left:10px;">
        <img src="img/leftarrow.png" alt="Alternate Text" height="32" width="32"/>
    </div>
    </a>

    <cc1:Accordion ID="MyAccordion" runat="Server" SelectedIndex="0" HeaderCssClass="cssAccHead"
        HeaderSelectedCssClass="cssAccHead" ContentCssClass="cssAccContent" AutoSize="None"
        FadeTransitions="true" TransitionDuration="250" FramesPerSecond="40" SuppressHeaderPostbacks="False"
        RequireOpenedPane="False">
        <Panes>
            <cc1:AccordionPane ID="AccordionPane1" runat="server">
                <Header>
                    <center>
                        <table style="width: 100%;">
                            <tr>
                                <td align="right">
                                    REPORT&nbsp;&nbsp;
                                </td>
                                <td>
                                    <asp:DropDownList ID="CmbRptType" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="CmbRptType_SelectedIndexChanged"
                                        AppendDataBoundItems="True" Width="300px">
                                        <asp:ListItem>Select</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                               
                                <td align="right" style="width: 30px;">
                                    <img id="imgScroller" runat="server" alt="scroller" src="imgButton/arrow-updown.png"
                                        height="25" />
                                </td>
                            </tr>
                        </table>
                    </center>
                </Header>
                <Content>
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="View1" runat="server">
                            <br /><table class="Menustyle" border="1" cellspacing="0">
                                <tr class="MenuHead">
                                    <td>Date
                                    </td>
                                    <td>Service Advisor
                                    </td>
                                    <td>Team Lead
                                    </td>
                                    <td>Employee Type
                                    </td>
                                    <td>Service Type
                                    </td>
                                    <td>Customer Type
                                    </td>
                                    <td>Delivery Status
                                    </td>
                                    <td>VRN/VIN
                                    </td>
                                    <td>Status
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table class="DateStyle" border="0" cellspacing="0">
                                            <tr>
                                                <td>&nbsp;
                                                </td>
                                                <td>DAY
                                                </td>
                                                <td>MONTH
                                                </td>
                                                <td>YEAR
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">&nbsp;&nbsp;FROM
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="cmbDayFrm" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cmbDayFrm_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="cmbMnthfrom" CssClass="form-control" runat="server" OnSelectedIndexChanged="cmbMnthfrom_SelectedIndexChanged"
                                                        AutoPostBack="True">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="cmbYearfrom" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cmbYearfrom_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr valign="top">
                                                <td align="left">&nbsp;&nbsp;TO
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="cmbDayTo" CssClass="form-control" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="cmbMnthTo" CssClass="form-control" runat="server" OnSelectedIndexChanged="cmbMnthTo_SelectedIndexChanged"
                                                        AutoPostBack="True">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="CmbYearto" CssClass="form-control" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td valign="top" style="width: 120px;">
                                        <br />
                                        <asp:DropDownList ID="cmbServiceAdvisor" CssClass="form-control" runat="server" Width="120px">
                                        </asp:DropDownList>
                                    </td>
                                    <td valign="top" style="width: 120px;">
                                        <br />
                                        <asp:DropDownList ID="cmbTeamLead" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td valign="top">
                                        <br />
                                        <asp:DropDownList ID="cmbEmpType" CssClass="form-control" runat="server" AppendDataBoundItems="True" DataSourceID="GetEmpType"
                                            DataTextField="EmpType" DataValueField="EmpType" Enabled="False" Width="120px">
                                            <asp:ListItem>ALL</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td valign="top">
                                        <br />
                                        <asp:DropDownList ID="cmbServiceType" CssClass="form-control" runat="server" AppendDataBoundItems="True"
                                            DataSourceID="srcServiceType" DataTextField="ServiceType" DataValueField="ServiceType"
                                            Width="120px">
                                            <asp:ListItem>ALL</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td valign="top">
                                        <br />
                                        <asp:DropDownList ID="CmbAplus" CssClass="form-control" runat="server" Width="120px">
                                            <asp:ListItem Value="ALL">All</asp:ListItem>
                                            <asp:ListItem Value="0">General</asp:ListItem>
                                            <asp:ListItem Value="1">VIP</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td valign="top">
                                        <br />
                                        <asp:DropDownList ID="cmbDeliveryStatus" CssClass="form-control" runat="server" Width="120px">
                                            <asp:ListItem>ALL</asp:ListItem>
                                            <asp:ListItem>DELIVERED</asp:ListItem>
                                            <asp:ListItem>UNDELIVERED</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td valign="top">
                                        <br />
                                        <asp:TextBox ID="txtRegNo" runat="server" Width="80px" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                    </td>
                                    <td valign="top">
                                        <br />
                                        <asp:DropDownList ID="drpWhiteBoard" CssClass="form-control" runat="server" Width="120px" Enabled="false">
                                            <asp:ListItem Value="ALL">ALL</asp:ListItem>
                                            <asp:ListItem Value="0">Yellow Board</asp:ListItem>
                                            <asp:ListItem Value="1">White Board</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="7">
                                        <asp:SqlDataSource ID="srcServiceType" runat="server" 
                                            SelectCommand="Select ServiceType FROM dbo.tblServiceTypes"></asp:SqlDataSource>
                                        <asp:SqlDataSource ID="GetEmpType" runat="server" 
                                            SelectCommand="GetEmployeeTypeRep" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                        <asp:View ID="View2" runat="server">
                            <center>
                                <table class="style4">
                                    <tr>
                                        <td align="center" valign="top" style="font-weight: bold">
                                            Select Month :
                                            <asp:DropDownList ID="SMonths" runat="server" AutoPostBack="True" CssClass="form-control">
                                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                <asp:ListItem Value="1">January</asp:ListItem>
                                                <asp:ListItem Value="2">February</asp:ListItem>
                                                <asp:ListItem Value="3">March</asp:ListItem>
                                                <asp:ListItem Value="4">April</asp:ListItem>
                                                <asp:ListItem Value="5">May</asp:ListItem>
                                                <asp:ListItem Value="6">June</asp:ListItem>
                                                <asp:ListItem Value="7">July</asp:ListItem>
                                                <asp:ListItem Value="8">August</asp:ListItem>
                                                <asp:ListItem Value="9">September</asp:ListItem>
                                                <asp:ListItem Value="10">October</asp:ListItem>
                                                <asp:ListItem Value="11">November</asp:ListItem>
                                                <asp:ListItem Value="12">December</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </center>
                        </asp:View>
                        <asp:View ID="View3" runat="server">
                            <center>
                                <table class="style4">
                                    <tr>
                                        <td align="center" valign="top" style="font-weight: bold;">
                                            Select Month :
                                            <asp:DropDownList ID="cmb_month" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="Current Month">Current Month</asp:ListItem>
                                                <asp:ListItem Value="1">January</asp:ListItem>
                                                <asp:ListItem Value="2">February</asp:ListItem>
                                                <asp:ListItem Value="3">March</asp:ListItem>
                                                <asp:ListItem Value="4">April</asp:ListItem>
                                                <asp:ListItem Value="5">May</asp:ListItem>
                                                <asp:ListItem Value="6">June</asp:ListItem>
                                                <asp:ListItem Value="7">July</asp:ListItem>
                                                <asp:ListItem Value="8">August</asp:ListItem>
                                                <asp:ListItem Value="9">September</asp:ListItem>
                                                <asp:ListItem Value="10">October</asp:ListItem>
                                                <asp:ListItem Value="11">November</asp:ListItem>
                                                <asp:ListItem Value="12">December</asp:ListItem>
                                            </asp:DropDownList>
                                            &nbsp;&nbsp; Year :
                                            <asp:DropDownList ID="cmbYear" runat="server" CssClass="form-control">
                                                <asp:ListItem>Current Year</asp:ListItem>
                                                <asp:ListItem>2011</asp:ListItem>
                                                <asp:ListItem>2012</asp:ListItem>
                                                <asp:ListItem>2013</asp:ListItem>
                                                <asp:ListItem>2014</asp:ListItem>
                                                <asp:ListItem>2015</asp:ListItem>
                                                 <asp:ListItem>2016</asp:ListItem>
                                                 <asp:ListItem>2017</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </center>
                        </asp:View>
                    </asp:MultiView>
                    <table width="100%" align="center">
                        <tr>
                            <td align="center" class="style3">
                                <br />
                                <asp:Button CssClass="btn btn-success" ID="btnLoad" runat="server" OnClick="btnLoad_Click"
                                    Text="GENERATE REPORT" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Label ID="lblStstus" runat="server" ForeColor="Maroon" CssClass="clsValidator"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </Content>
            </cc1:AccordionPane>
        </Panes>
    </cc1:Accordion>
    <div class="Report1">
        <rsweb:ReportViewer ID="RptViewer" runat="server" Width="72%"
            Font-Size="8pt" AsyncRendering="False" DocumentMapWidth="100%"
            Height="29px" SizeToReportContent="True">
            <LocalReport EnableExternalImages="True" EnableHyperlinks="True">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="ProTrackDataSet_GetPosition" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="GetData"
            TypeName="ProTrackDataSetTableAdapters.TimeMonitorTableAdapter"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData"
            TypeName="ProTrackDataSetTableAdapters."></asp:ObjectDataSource>
    </div>
</asp:Content>
