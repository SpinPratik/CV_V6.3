<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LatestVehiclePositionDisplay.aspx.cs" Inherits="LatestVehiclePositionDisplay" %>
<%@ Register Src="PositionDisplay.ascx" TagName="PositionDisplay" TagPrefix="uc1" %>
<%@ Register Src="VehicleIdle.ascx" TagName="VehicleIdle" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
     <link href="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484214710/css/ProTRAC_Css.css" rel="stylesheet" type="text/css" />
    <link href="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484214730/css/Stylesheet.css" rel="stylesheet" type="text/css" />


<%--     <link href="CSS/ProTRAC_Css.css" rel="stylesheet" type="text/css" />
    <link href="CSS/Stylesheet.css" rel="stylesheet" type="text/css" />--%>
    <%--<script src="js/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="js/Tooltip.js" type="text/javascript"></script>--%>
   <%-- <link href="CSS/Tablehover.css" rel="stylesheet" type="text/css" />--%>
     <link href='https://fonts.googleapis.com/css?family=Roboto' rel='stylesheet' type='text/css'/>
    <style type="text/css">
         ::-webkit-scrollbar{ width:10px; background:transparent;height: 10px; } 
     ::-webkit-scrollbar-thumb{ background:transparent; border-radius:2px; } 
     ::-webkit-scrollbar-thumb:hover{ background-color:#BF4649; border:1px solid #333333; } 
     ::-webkit-scrollbar-thumb:active{ background-color:#A6393D; border:1px solid #333333; } 
     ::-webkit-scrollbar-track{ border:1px gray solid; border-radius:2px; -webkit-box-shadow:0 0 6px gray inset; 
       } 
       
         .PanelStyle1
     {
          overflow-x:hidden;
          width:100%;
          height:340px;
          background-color:#FFFFFF;
         }
    
     .PanelStyle
     {
          overflow-x:hidden;
          
          width:100%;
          height:220px;
          background-color:#FFFFFF;
         }
.topbtn
{
    color:#fff;
    margin-left:30px;
    font-family: Roboto, sans-serif;
}
    
    .topbtn1
{
    color:#fff;
    margin-right:50px;
    font-family: Roboto, sans-serif;
}
    #btnBACK  {
        margin-left:7px;
        -webkit-transform: scale(1);
	transform: scale(1);
	-webkit-transition: .3s ease-in-out;
	transition: .3s ease-in-out;
    }
    #btnBACK:hover  {
	-webkit-transform: scale(1.3);
	transform: scale(1.3);
	
	
}
    </style>
 <script language="javascript" type="text/javascript">
    // var t1;

    // $(document).ready(
    //function () {
    //    t1 = new ToolTip("a", true, 40);
    //});

     //function ShowLoadProcessInOutTime(e, RegNo, ProcessName) {
     //    $.ajax({
     //        type: "POST",
     //        url: "LatestVehiclePositionDisplay.aspx/LoadProcessInOutTime",
     //        data: "{'RefNo':'" + RegNo + "','ProcessName':'" + ProcessName + "'}",
     //        contentType: "application/json; charset=utf-8",
     //        dataType: "json",
     //        success: function (result) {
     //            str = result.d;
     //            t1.Show(e, str);
     //        },
     //        error: function (result) {
     //            t1.Show(e, result.status + ' ' + result.statusText);
     //        }
     //    }
     // );

     //}

     //function ShowWorkshopLoadProcessInOutTime(e, RegNo, ProcessName) {
     //    $.ajax({
     //        type: "POST",
     //        url: "LatestVehiclePositionDisplay.aspx/LoadWorkshopProcessInOutTime",
     //        data: "{'RefNo':'" + RegNo + "','ProcessName':'" + ProcessName + "'}",
     //        contentType: "application/json; charset=utf-8",
     //        dataType: "json",
     //        success: function (result) {
     //            str = result.d;
     //            t1.Show(e, str);
     //        },
     //        error: function (result) {
     //            t1.Show(e, result.status + ' ' + result.statusText);
     //        }
     //    }
     // );

     //}

     //function ShowLoadIdleInOutTime(e, RegNo) {
     //    $.ajax({
     //        type: "POST",
     //        url: "LatestVehiclePositionDisplay.aspx/LoadIdleInOutTime",
     //        data: "{'RefNo':'" + RegNo + "'}",
     //        contentType: "application/json; charset=utf-8",
     //        dataType: "json",
     //        success: function (result) {
     //            str = result.d;
     //            t1.Show(e, str);
     //        },
     //        error: function (result) {
     //            t1.Show(e, result.status + ' ' + result.statusText);
     //        }
     //    }
     // );
     //}

     //function hideTooltip(e) {
     //    if (t1) t1.Hide(e);
     //}

     //Event.observe(window, 'load', init, false);
    </script>
   
    
    
  
      
</head>
<body>
   <form id="form1" runat="server">
   
<%--     <div id="a" class="CSSTableGenerator" style="font-family: Arial; background-color: white;
        width: 155px; height: 40px; border: solid 1px gray; border-radius: 6px; text-align: left;" align="center">
    </div>--%>
    <div>
       <table class="fullStyle" cellpadding="0" cellspacing="0" border="0">
                <tr class="smallFont">
                    <td>
                        <table class="fullStyle" bgcolor="#FF7C23" border="0">
                            <tr>
                                <td style="width: 0%; padding-right: 10px;" align="left">
                                    <asp:Label ID="lblSyncTime" runat="server" Font-Size="Medium" ForeColor="#333333"></asp:Label>
                               <asp:Label ID="lblSlno" runat="server"  Visible="false"></asp:Label>
                                    <%--<asp:ImageButton ID="btnBACK" runat="server" ImageUrl="~/Icons/back_btn4.png" 
                                        Width="20px" onclick="btnBACK_Click"/>--%>
                                </td>

                                <td style="width: 17%; text-align: center; height: 30px;">
                                    
                                    <div style="border-radius:5px;font-family: Roboto, sans-serif;">
                                        <span  style="color:White;">No. of vehicles delivered</span>
                                </div></td>
                                <td><b><asp:Label runat="server" ID="lbl_vehiclesDeliveredToday" Style="color:white">10</asp:Label></b></td>
                                <td style="width: 50%; text-align: center; height: 30px;">
                                    
                                    <div style="border-radius:5px;font-family: Roboto, sans-serif;"><asp:Label ID="lbl_CurrentPage" runat="server" ForeColor="White" Text="Position Display" Font-Bold="true" Font-Size="20px" ></asp:Label>
                                </td>
                                <td style="width: 25%;" align="right">

                                <div class="topbtn1">
                               <%-- Idle:
                                <asp:Label ID="lbIdle" Text="" runat="server"   Font-Bold="true" style="margin-right:10px;color:#fff; font-family: Roboto, sans-serif;"></asp:Label>--%>
                                Total:
                                <asp:Label ID="lbTotal" runat="server" Text=""  Font-Bold="true" style="color:#fff;"></asp:Label>
                                </div>
                                
                                    
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
    <%--<table style="border-color: #FFFFFF; background-color:#007fff; width: 100%; color: #333333; border-color:White;
                text-align: center;"  cellspacing="0" cellpadding="0">
                <tr style="font-size: 13px; height: 30px;">
                    <td>
                       
                        <asp:Label ID="Label12" runat="server" Width="100px" Text="Waiting (Gate)" Font-Bold="true" ForeColor="White" ></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label13" runat="server" Width="100px" Text="Job Card" Font-Bold="true" ForeColor="White"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label4" runat="server" Width="100px" Text="Estimation" Font-Bold="true" ForeColor="White"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label14" runat="server" Width="100px" Text="Workshop" Font-Bold="true" ForeColor="White"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label16" runat="server" Width="150px" Text="Wheel Alighment" Font-Bold="true" ForeColor="White"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label17" runat="server" Width="100px" Text="Road Test" Font-Bold="true" ForeColor="White"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label18" runat="server" Width="85px" Text="Quality" Font-Bold="true" ForeColor="White"></asp:Label>
                    </td>
                     <td>
                        <asp:Label ID="Label1" runat="server" Width="85px" Text="Wash" Font-Bold="true" ForeColor="White"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label19" runat="server" Width="85px" Text="Ready" Font-Bold ="true" ForeColor="White"></asp:Label>
                    </td>
                     <td>
                        <asp:Label ID="Label2" runat="server" Width="100px" Text="Vehicle Hold" Font-Bold ="true" ForeColor="White"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label20" runat="server" Width="100px" Text="Vehicle Idle" Font-Bold="true" ForeColor="White"></asp:Label>
                    </td>
                     <td>
                        <asp:Label ID="Label3" runat="server" Width="100px" Text="Body Shop" Font-Bold="true" ForeColor="White"></asp:Label>
                    </td>
                    <td width="100%">
                        <asp:Label ID="Label21" runat="server" Width="100%" Text="Total" Font-Bold="true" ForeColor="White"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label Width="100px" ID="lbWGate" runat="server" Text="0" Font-Bold="true" ForeColor="White"></asp:Label>
                    </td>
                    <td>
                        <asp:Label Width="100px" ID="lbRO" runat="server" Text="0" Font-Bold="true" ForeColor="White"></asp:Label>
                    </td>
                     <td>
                        <asp:Label Width="100px" ID="lbEstimation" runat="server" Text="0" Font-Bold="true" ForeColor="White"></asp:Label>
                    </td>
                    <td>
                        <asp:Label Width="100px" ID="lbWorkshop" runat="server" Text="0" Font-Bold="true" ForeColor="White"></asp:Label>
                       
                    </td>
                    <td>
                        <asp:Label Width="100px" ID="lbWA" runat="server" Text="0" Font-Bold="true" ForeColor="White"></asp:Label>
                    </td>
                    <td>
                         <asp:Label Width="100px" ID="lbRT" runat="server" Text="0" Font-Bold="true" ForeColor="White"></asp:Label>
                    </td>
                    <td>
                        <asp:Label Width="85px" ID="lbFI" runat="server" Text="0" Font-Bold="true" ForeColor="White"></asp:Label>
                    </td>
                    <td>
                        <asp:Label Width="85px" ID="lbWash" runat="server" Text="0" Font-Bold="true" ForeColor="White"></asp:Label>
                    </td>
                    <td>
                        <asp:Label Width="85px" ID="lbVR" runat="server" Text="0" Font-Bold="true" ForeColor="White"></asp:Label>
                    </td>
                     <td>
                        <asp:Label Width="100px" ID="lbVH" runat="server" Text="0" Font-Bold="true" ForeColor="White"></asp:Label>
                    </td>
                    <td>
                       <asp:Label Width="100px" ID="lbVI" runat="server" Text="0" Font-Bold="true" ForeColor="White"></asp:Label>
                    </td>
                     <td>
                       <asp:Label Width="100px" ID="lblBodyShop" runat="server" Text="0" Font-Bold="true" ForeColor="White"></asp:Label>
                    </td>
                    <td width="100%">
                        <asp:Label Width="100%" ID="lbTotal" runat="server" Text="0" Font-Bold="true" ForeColor="White"></asp:Label>
                    </td>
                </tr>
            </table>--%>
  
    <table width="100%">
     <tr>
            <td style="height: 50%; text-align: left; vertical-align: top; font-family: Consolas, Georgia;
                font-size: 14px; font-weight: normal;">
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <asp:Timer ID="Timer1" runat="server" Interval="600000" ontick="Timer1_Tick">
                </asp:Timer>
                              
                <table style="width: 100%;" cellspacing="0" cellpadding="0" border="0">
                    <tr><td>
                        <asp:Label Width="100px" ID="lbWGate" runat="server" Text="0" Font-Bold="true" ForeColor="White" Visible="false"></asp:Label>
                         <asp:Label Width="100px" ID="lbRO" runat="server" Text="0" Font-Bold="true" ForeColor="White" Visible="false"></asp:Label> 
                        
                            <asp:Label Width="100px" ID="lbWorkshop" runat="server" Text="0" Font-Bold="true" ForeColor="White" Visible="false"></asp:Label>
                            <asp:Label Width="100px" ID="lbSpeedo" runat="server" Text="0" Font-Bold="true" ForeColor="White" Visible="false"></asp:Label>   
                        
                        <asp:Label Width="100px" ID="lbWA" runat="server" Text="0" Font-Bold="true" ForeColor="White" Visible="false"></asp:Label>
                             <asp:Label Width="100px" ID="lbRT" runat="server" Text="0" Font-Bold="true" ForeColor="White" Visible="false"></asp:Label>
                              <asp:Label Width="85px" ID="lbFI" runat="server" Text="0" Font-Bold="true" ForeColor="White" Visible="false"></asp:Label>
                                <asp:Label Width="85px" ID="lbWash" runat="server" Text="0" Font-Bold="true" ForeColor="White" Visible="false"></asp:Label>
                                  <asp:Label Width="85px" ID="lbVR" runat="server" Text="0" Font-Bold="true" ForeColor="White" Visible="false"></asp:Label>
                                   <asp:Label Width="100px" ID="lbVH" runat="server" Text="0" Font-Bold="true" ForeColor="White" Visible="false"></asp:Label>
                                    <asp:Label Width="100px" ID="Label1" runat="server" Text="0" Font-Bold="true" ForeColor="White" Visible="false"></asp:Label>
                                    
                          
                    </td></tr>
                    <tr id="pnl_Display" runat="server">
                                       
                    </tr>
                </table>
            </td>
        </tr>
        </table>
         <table width="100%">
        <tr>
            <td style="height: 5%; text-align: left; vertical-align: top; font-family: Consolas, Georgia;
                font-size: 14px; font-weight: normal;">
               <table style="width: 100%; background-color: #1591cd;" cellspacing="0" cellpadding="0" border="0" class="panel1Blue" >
                   
                    <tr id="Tr1" runat="server" >
                  <%--  <td align="left" style="width:16.7%">
                       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Vehicle Hold 
                    </td>--%>
                     <td align="left"><div style="width:1000px;margin-left:170px; border-top-right-radius:3px;border-top-left-radius:3px; text-align:center;">
                       Idle(Waiting For Next Process)
                        <asp:Label Width="100px" ID="lbVI" runat="server" Text="0" Font-Bold="true" ForeColor="White"></asp:Label></div>
                    </td>
                   <%-- <td align="center" style="width:16.7%">
                       Body Shop
                    </td>--%>
                    </tr>
                </table>
            </td>
        </tr>
        </table>
         <table width="100%">
     <tr>
            <td style="height: 25%; text-align: left; vertical-align: top; font-family: Roboto, sans-serif;
                font-size: 12px; font-weight: normal;">
               <table style="width: 100%;" cellspacing="0" cellpadding="0" border="0">
                   
                    <tr id="pnl_Idle_Display" runat="server">
                        
                    </tr>
                </table>
            </td>
        </tr>
        </table>
    </div>
    
    </form>
</body>
</html>
