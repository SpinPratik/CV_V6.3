<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrontOfficeDisplayStatus.aspx.cs"
    Inherits="FrontOfficeDisplayStatus" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Front Office Display Status</title>
     <link rel="stylesheet" type="text/css" href="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484291841/normalize_tmnnwa.css" />
    <link rel="stylesheet" type="text/css" href="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484287754/demo_azo6gm.css" />
    <link rel="stylesheet" type="text/css" href="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484287756/component_d9rckt.css" />
      <link type="text/css" rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.6.3/css/font-awesome.min.css"/>
    <link href="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484285057/Bootstrap/bootstrap.min.css" rel="stylesheet" />
  <script type="text/javascript" src="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484292290/js/bootstrap.min.js"></script>
    <link href="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484214710/css/ProTRAC_Css.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .Headerdiv
        {
            height: 70px;
            width: 100%;
            background-image: url(  'Images/protrac2-1.png' );
            background-repeat: no-repeat;
            background-position: center;
            text-align: center;
        }
        .boddy
        {
            margin: 0;
            padding: 0;
            height: 100%;
            background-color: White;
        }
        .HeaderLeft
        {
            width: 33%;
            font-family: Arial;
            font-size: smaller;
            font-weight: bold;
            text-align: center;
        }
        .HeaderCenter
        {
            width: 34%;
            font-size: 20px;
            color: #333333;
            font-family: Consolas, Georgia;
            font-weight: bold;
            text-align: center;
        }
        .HeaderRight
        {
            width: 33%;
            text-align: center;
        }
        #contentBox
        {
            height: 100%;
            width: 100%;
        }
        .style1
        {
            width: 172px;
        }
        .style2
        {
            width: 26px;
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
	font-weight: bold;
	text-align: left;
}
.mydatagrid a /** FOR THE PAGING ICONS  **/
{
	background-color: Transparent;
	padding: 5px 5px 5px 5px;
	color: #fff;
	text-decoration: none;
	font-weight: bold;
}
.mydatagrid th /** FOR THE PAGING ICONS  **/
{
	border:1px solid #e5e5e4 !important;
    border-bottom:2px solid #e5e5e4 !important;
	padding: 12PX;
    font-weight: 700!important;
    color: #000;
    font-size: 14px !important;
    /*text-align:left;*/
    text-transform:uppercase;
    /*background-color:#CC3333 !important*/
	
}
.mydatagrid td{
    text-align:left;
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
	padding: 5px 10px;
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
            font-weight: bold;
            text-align: center;
            text-decoration: none;
        }
        .mydatagrid a
        {
            background-color: #f5f5f5;
            color: #969696;
            /*border: 1px solid #969696;*/
        }
        .mydatagrid span
        {
            background-color: #A1DCF2;
            color: #000;
            border: 1px solid #3AC0F2;
        }
        a:hover{
            text-decoration:none !important;
        }
    </style>
    <script type="text/javascript">
        function stopRKey(evt) {
            var evt = (evt) ? evt : ((event) ? event : null);
            var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
            if ((evt.keyCode == 13) && (node.type == "text")) { return false; }
        }

        document.onkeypress = stopRKey;

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
       <div class="" style="height:100%;min-width:1100px;">
        
            <ul id="gn-menu" class="gn-menu-main" style="background-color: #1591cd !important;">
                 <li>
                    <a href="TagAllotment.aspx">  <asp:ImageButton ImageUrl="images/left-arrow%20(1).png" ID="back" OnClick="back_Click" runat="server" style="margin-top:15px;"></asp:ImageButton></a>
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

    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            SelectCommand="GetSADisplay" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:ControlParameter ControlID="ddlSA" DefaultValue="ALL" Name="SAName" PropertyName="SelectedValue"
                    Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        <h4 style="text-align:center">FRONT OFFICE DISPLAY</h4>
      <%--  <table width="100%" cellpadding="0" cellspacing="0" border="0" style="height: 10px">
            <tr>
                <td class="HeaderLeft" align="center">
                </td>
                <td class="HeaderCenter" valign="top" align="center">
                    Front Office Display
                </td>
                <td align="right" valign="top" style="right: 10px;">
                    <asp:Button CssClass="button" ID="ImageButton1" runat="server" OnClick="ImageButton1_Click"
                        Text="Back" />
                </td>
            </tr>
        </table>--%>
      
        <table width="100%">
            <tr class="clmDistriSmall">
                <td>&nbsp;</td>
                <td>
                     <asp:Label ID="lblSA" runat="server" Text="Service Advisor"
                        ></asp:Label>
                    <asp:DropDownList ID="ddlSA" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlSA_SelectedIndexChanged"
                        AutoPostBack="True" AppendDataBoundItems="True">
                        <asp:ListItem Value="ALL">ALL</asp:ListItem>
                    </asp:DropDownList>
                    
                    </td>
                    <td>
                        &nbsp;
                    </td>
                  
                    <td>
                        <asp:Label ID="lblVehicleNo" runat="server" Text="VRN/VIN" 
                           ></asp:Label>
                        <asp:TextBox ID="txtVehicleNo" CssClass="form-control" runat="server"></asp:TextBox>
                    </td>
                   
                    <td>&nbsp; </td>
                    <td>
                        <asp:Label ID="lblTagNo" runat="server" Text="VID" ></asp:Label>
                        <asp:TextBox ID="txtTagNo" CssClass="form-control" onkeypress="return isNumberKey(event)" MaxLength="10" runat="server"></asp:TextBox>
                    </td>
                     <td>&nbsp;</td>
                    <td style="width: 115px;">
                        <br />&nbsp;
                        <asp:CheckBox ID="chkBodyShop" runat="server" Text="&nbsp;&nbsp;BodyShop" AutoPostBack="True" OnCheckedChanged="chkBodyShop_CheckedChanged" /></td>
               
               
                <td><br />
                   <%-- <asp:ImageButton ID="btnSearch" runat="server" AlternateText="SEARCH" Height="30px"
                        ImageUrl="~/Icons/srch1.jpeg" OnClick="btnSearch_Click" ToolTip="Search" CssClass="btn btn-default" />
                     <asp:ImageButton ID="btntimeLineRefresh" runat="server" CssClass="btn btn-default" Height="27px" ImageUrl="~/images/refresh1.png"
                        OnClick="btnRefresh_Click" AlternateText="Refresh" ToolTip="Refresh" />--%>
                       <asp:ImageButton ID="ImageButton1" runat="server" AlternateText="SEARCH" Height="30px"
                        ImageUrl="https://res.cloudinary.com/deekyp5bi/image/upload/v1484285976/search_2_ll3ccm.png" OnClick="btnSearch_Click" ToolTip="Search" CssClass="btn btn-default" />
                     <asp:ImageButton ID="ImageButton2" runat="server" CssClass="btn btn-default" Height="27px" ImageUrl="https://res.cloudinary.com/deekyp5bi/image/upload/v1484284612/refresh_vahbyd.png"
                        OnClick="btnRefresh_Click" AlternateText="Refresh" ToolTip="Refresh" />
                </td>
               
                <td valign="top" style="width: 50px; text-align: left; padding-left: 5px; padding-right: 0px;
                    white-space: nowrap;">
                    &nbsp;&nbsp;
                </td>
                <td>               
                     <asp:UpdateProgress ID="UpdProg" runat="server">
                    <ProgressTemplate>
                        <img src="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484295279/css/ajax-loader.gif" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
               </td>

            </tr>
            <tr><td>&nbsp;</td></tr>
        </table>
        <asp:UpdatePanel ID="updPanelGrd" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td valign="top">
                            <div id="contentBox" style="font-family: Consolas, Georgia">
                                <asp:Timer ID="Timer1" runat="server" Interval="150000" OnTick="Timer1_Tick">
                                </asp:Timer>
                                <asp:Timer ID="Timer2" runat="server" Interval="150000" OnTick="Timer2_Tick">
                                </asp:Timer>
                                <asp:Label ID="lblSyncTime" runat="server" Font-Bold="True" Font-Size="Larger" Visible="False"></asp:Label>
                                <asp:GridView ID="gvDisplayStatus" CssClass="mydatagrid" runat="server" Width="100%" CellPadding="4" ForeColor="#333333"
                                    GridLines="None" AllowPaging="True" PageSize="8" OnPageIndexChanging="gvDisplayStatus_PageIndexChanging"
                                    OnRowDataBound="gvDisplayStatus_RowDataBound1" Font-Size="Large" Font-Names="Consolas, Georgia">
                                    <RowStyle BackColor="WhiteSmoke" Height="60px" ForeColor="#333333" Font-Bold="true"
                                        Font-Size="Medium" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <FooterStyle BackColor="Silver" Font-Bold="True" ForeColor="#333333" HorizontalAlign="Center"
                                        VerticalAlign="Middle" />
                                    <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <PagerStyle BackColor="Silver" ForeColor="#333333" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="WhiteSmoke" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="Silver" Font-Bold="True" ForeColor="#333333" Height="50px" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <AlternatingRowStyle BackColor="WhiteSmoke" ForeColor="#333333" Font-Size="Medium"
                                        HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlSA" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>