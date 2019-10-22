<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewJobAllotment.aspx.cs" Inherits="NewJobAllotment" %>

<%@ Register Assembly="DayPilot" Namespace="DayPilot.Web.Ui" TagPrefix="DayPilot" %>
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<%@ Register Src="AllotmentVehicles.ascx" TagName="Vehicle" TagPrefix="uc1" %>
<%@ Register Src="Technicians.ascx" TagName="Technicians" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484214730/js/modal.js"></script>
    <script type="text/javascript" src="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484214730/js/modal.js"></script>
    <link rel="stylesheet" href="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484214730/themes/scheduler_8.css" />
    <link rel="stylesheet" href="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484214730/css/Style.css" />

    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css">
    <script src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
    <script src="https://code.jquery.com/jquery-1.8.3.min.js"></script>
    <link rel="stylesheet" href="/resources/demos/style.css">
    <link href="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484214730/Style.css" rel="stylesheet" />
    <link href="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484214730/bootstrap.min.css" rel="stylesheet" />

    <link href="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484214730/Bootstrap/bootstrap.min.css" rel="stylesheet" />
    <script src="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484214730/Bootstrap/bootstrap.min.js"></script>
     <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.6.3/css/font-awesome.min.css">
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.2/jquery.min.js"></script>
  
    <link rel="stylesheet" type="text/css" href="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484214730/css/normalize.css" />
   <%-- <link rel="stylesheet" type="text/css" href="CSS/demo.css" />--%>
    <link rel="stylesheet" type="text/css" href="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484214730/css/component.css" />
    <style>
        .btn-primary {
            color: #2e6da4;
            background-color: unset;
            border: 1px solid #2e6da4;
        }
        
        .Emp_Type_style{
            color:white;
        }
        .btn{
            text-transform:uppercase;
        }
        #right-hidden{
            background-color:#f9f9f9;
        }
    </style>


</head>
<body>
  <script type="text/javascript">
      dps.commandCallBack('refresh');
      dps1.commandCallBack('refresh');
      function dialog() {
          var modal = new DayPilot.Modal();
          modal.top = 100;
          modal.width = 1020;
          modal.height = 450;
          modal.opacity = 0;
          modal.border = "10px solid #9E9E9E";
          modal.closed = function () {
              if (this.result == "OK") {
                  dps1.commandCallBack('refresh');
                  dps.commandCallBack('refresh');
              }
              dps1.clearSelection();
              dps.clearSelection();
          };

          modal.zIndex = 100;
          return modal;
      }

     
      function timeRangeSelected(start, end, resource) {
          var d = new Date();
          var x = d.getYear() + 1900;
          var y = ("0" + (d.getMonth() + 1)).slice(-2);
          var z = ("0" + (d.getDate())).slice(-2);
          var n = ("0" + (d.getHours())).slice(-2);
          var o = ("0" + (d.getMinutes())).slice(-2);
          var p = ("0" + (d.getSeconds())).slice(-2);
          var a = x + "-" + y + "-" + z + "T" + n + ":" + o + ":" + p;

          if (start >= a) {
              var modal = dialog();
              modal.showUrl("New.aspx?start=" + start.toStringSortable() + "&end=" + end.toStringSortable() + "&r=" + resource + "&hash=<%= PageHash %>");
          }
          else {
              alert("Time is already crossed !!");
          }

      }

      function eventClick(e) {
          var d = new Date();
          var x = d.getYear() + 1900;
          var y = ("0" + (d.getMonth() + 1)).slice(-2);
          var z = ("0" + (d.getDate())).slice(-2);
          var n = ("0" + (d.getHours())).slice(-2);
          var o = ("0" + (d.getMinutes())).slice(-2);
          var p = ("0" + (d.getSeconds())).slice(-2);
          var a = x + "-" + y + "-" + z + "T" + n + ":" + o + ":" + p;

          if (e.end() >= a) {
              var modal = dialog1();
              modal.showUrl("Edit.aspx?id=" + e.value() + "&r=" + e.resource() + "&hash=<%= PageHash %>");
          }
          else {
              alert("You cannot edit this event !!");
          }
      }
      function editEvent(e) {

          var modal = new DayPilot.Modal();
          modal.top = 100;
          modal.height = 200;
          modal.opacity = 00;
          modal.border = "10px solid #d0d0d0";
          modal.closed = function () {
              if (this.result == "OK") {
                  dps1.commandCallBack('refresh');
                  dps.commandCallBack('refresh');
              }
          };
          modal.showUrl("Edit.aspx?id=" + e.value());
      }
      function dialog1() {
          var modal = new DayPilot.Modal();
          modal.top = 100;
          modal.width = 500;
          modal.opacity = 0;
          modal.border = "6px solid #D6D6D6";
          modal.closed = function () {
              if (this.result == "OK") {
                  dps1.commandCallBack('refresh');
                  dps.commandCallBack('refresh');
              }
              dps1.clearSelection();
              dps.clearSelection();
          };
          modal.height = 200;
          modal.zIndex = 100;
          return modal;
      }

      function openNav() {
          document.getElementById("mySidenav").style.width = "250px";
          document.getElementById("main_content").style.marginLeft = "250px";
      }

      function closeNav() {
          document.getElementById("mySidenav").style.width = "0";
          document.getElementById("main_content").style.marginLeft = "0";
      }

      function openNav1() {
          document.getElementById("right-hidden-div").style.width = "250px";
          //document.getElementById("main_content").style.marginRight = "250px";
      }

      function closeNav1() {
          document.getElementById("right-hidden-div").style.width = "0";
          document.getElementById("main_content").style.marginRight = "0";
      }
       
    

    </script>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
       <%-- <asp:UpdatePanel runat="server"><ContentTemplate>--%>
        
<div class="" style="min-width:1100px;">
        
            <ul id="gn-menu" class="gn-menu-main" style="background-color: #FF7C23 !important;">
                 <li>
                    <a href="JCRDisplayWorks.aspx">  <asp:ImageButton ImageUrl="images/left-arrow%20(1).png" ID="back" runat="server" style="margin-top:15px;"></asp:ImageButton></a>
                </li>
                <li><a href="#" style="color: white; font-size: 21px;"><img src="https://res.cloudinary.com/deekyp5bi/image/upload/v1484219853/images/wms--logo.png" style=" margin-top: 15px;"/></a></li>
                <li style="width:225px;">
                    <a href="#"> <img src="Images/user.png" />&nbsp;&nbsp;
                        <asp:Label runat="server" ID="lbl_LoginName" style="color:white;text-transform:uppercase;text-decoration:none !important;"></asp:Label>               
                    </a>
                </li>
               <li style="width:225px;border-right:unset !important;"><i class="fa fa-clock-o" style="color:white"></i> &nbsp;&nbsp;<asp:Label class="" ID="lbl_currTime" Font-Bold="true" style="color: white;text-transform:uppercase !important" runat="server" ></asp:Label></li>


                  <li style="border-left: 1px solid #c6d0da;"><a style="color: white;" href="#">
                    <img src="https://res.cloudinary.com/deekyp5bi/image/upload/v1484219853/images/logo_spin.png" /></a></li> 
                <li>  
                  <asp:LinkButton ID="btn_logout" OnClick="btn_logout_Click" style="vertical-align: middle;color:white;" Text="logout"  runat="server"></asp:LinkButton>
                  </li>            
               
            </ul>
        </div>


        <center>
        <p style="position: fixed; width: 100%; height: 5px; bottom: 0px; left: 0px; right: 0px;
           font-size: 11px;">
            <asp:Label ID="lblVersion" runat="server"></asp:Label>
        </p>
    </center>
       
        <br />
        <br />
       <div style="margin-right: 17px;">
             <asp:Button CssClass="btn btn-info" style="float:right;border:1px solid #286090;color:#286090;font-weight:800;font-size:12px;margin-left:3px; font-family: tahoma, arial, Verdana; background-color:white;" Text="Display" ID="btn_jcrdisplay" OnClick="btn_jcrdisplay_Click" runat="server" /> &nbsp;&nbsp;
            <asp:Button CssClass="btn btn-info" Visible="false" style="float:right;border:1px solid #286090;color:#286090;font-weight:800;font-size:12px;font-family: tahoma, arial, Verdana; background-color:white;" Text="KPI Dashboard" ID="btn_kpiDashboard" OnClick="btn_kpiDashboard_Click" runat="server" />&nbsp;&nbsp;
       </div>
        <div>
            
            
            <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%"
                Height="2px" Style=""
                AutoPostBack="True" OnActiveTabChanged="TabContainer1_ActiveTabChanged" CssClass="MyTabStyle">
            </cc1:TabContainer>
        </div>

        <div class="container-fluid" style="position: relative;">
            <div class="row">
                 <div id="right-hidden-div" onmouseover="openNav1()" onmouseout="closeNav1()" class="right-hidden">

                  
                     <center><h4 style="color: #1591cd;font-weight: bold;">VEHICLES</h4></center><hr />

                    <ul width="100%" class="list-group" id="external1" runat="server" style="direction: ltr;">
                    </ul>
                </div>

                <div id="main_content" style="margin-top: -28px;">

                    <div class=" col-md-12  scheduler_blue_wrap right" id="scheduler_blue_wrap_id">
                       <center>
                            <span class="btn btn-primary" style="border: 1px solid #286090;text-align:center;color:#286090;float: right;font-family: tahoma, arial, Verdana;font-size: 11px;font-weight: 800;" onmouseover="openNav1()" onmouseout="closeNav1()" onclick="openNav1()">Vehicles >></span>
                            <a class="btn btn_next" href="javascript:dps1.commandCallBack('next');javascript:dps.commandCallBack('next');" style="border: 1px solid #286090;text-align:center;color:#286090;float: right;margin-right: 4px;font-family: tahoma, arial, Verdana;font-size: 11px;font-weight: 800;"><span>NextDay&nbsp;&nbsp;&gt;&gt;</span></a>
                            <a class="btn btn_previous" href="javascript:dps1.commandCallBack('previous');javascript:dps.commandCallBack('previous');" style="border: 1px solid #286090;text-align:center;color:#286090;float: right;margin-right: 4px;font-family: tahoma, arial, Verdana;font-size: 11px;font-weight: 800;"><span>&lt;&lt;&nbsp;&nbsp;PreviousDay</span></a>
                         </center>
                        <br />
                        <br />

                        <div class="scheduler_blue_wrap_inner">
                            <DayPilot:DayPilotScheduler
                                ID="DayPilotScheduler1"
                                runat="server"
                                DataStartField="eventstart"
                                DataEndField="eventend"
                                DataTextField="name"
                                DataIdField="id"
                                DataResourceField="resource_id"
                                EventMoveHandling="CallBack"
                                OnEventMove="DayPilotScheduler1_EventMove"
                                Width="100%"
                                RowHeaderWidth="120"
                                CellWidth="40"
                                Scale="CellDuration"
                                CellDuration="15"
                                CellSelectColor="#ff5050"
                                DynamicEventRendering="Disabled"
                                BusinessBeginsHour="9"
                                BusinessEndsHour="21"
                                AfterRenderJavaScript="afterRender(data, isCallBack);"
                                BubbleID="DayPilotBubble1"
                                HeightSpec="Max"
                                Height="600"
                              
                                OnCommand="DayPilotScheduler1_Command"
                                TimeRangeSelectedHandling="JavaScript"
                                TimeRangeSelectedJavaScript="timeRangeSelected(start, end, resource)"
                                ClientObjectName="dps1"
                                EventMoveJavaScript="dps1.eventMoveCallBack(e, newStart, newEnd, newResource);"
                                EventClickHandling="JavaScript"
                                EventClickJavaScript="eventClick(e);"
                                EventEditHandling="CallBack"
                                ScrollLabelsVisible="false"
                                xResourceBubbleID="DayPilotBubble1"
                                DragOutAllowed="true"
                                EventResizeHandling="Notify"
                                OnEventResize="DayPilotScheduler1_EventResize"
                                OnBeforeEventRender="DayPilotScheduler1_BeforeEventRender"
                                RowMinHeight = "30"
                                Theme="scheduler_8">
                                <TimeHeaders>
                                    <DayPilot:TimeHeader GroupBy="Day" Format="D" />
                                    <DayPilot:TimeHeader GroupBy="Hour" />
                                    <DayPilot:TimeHeader GroupBy="Cell" />
                                </TimeHeaders>

                            </DayPilot:DayPilotScheduler>

                            <DayPilot:DayPilotBubble
                                ID="DayPilotBubble1"
                                runat="server"
                                OnRenderContent="DayPilotBubble1_RenderContent"
                                ClientObjectName="bubble"
                                OnRenderEventBubble="DayPilotBubble1_RenderEventBubble"
                                Width="250"
                                Corners="Rounded"
                                Position="EventTop">
                            </DayPilot:DayPilotBubble>
                        </div>
                    </div>
                </div>
            </div>
        </div>
       <%--   AutoRefreshEnabled="true"
                        AutoRefreshInterval="1000"
                        AutoRefreshMaxCount="100"--%>
        <div class="container-fluid">
            <div class="">
                <div class="scheduler_blue_wrap_inner">
                    <DayPilot:DayPilotScheduler
                        ID="DayPilotScheduler2"
                        runat="server"
                        DataStartField="eventstart"
                        DataEndField="eventend"
                        DataTextField="name"
                        DataIdField="id"
                        DataResourceField="resource_id"
                        Width="100%"
                        RowHeaderWidth="120"
                        CellWidth="40"
                        Scale="CellDuration"
                        CellDuration="15"
                        CellSelectColor="#ff5050"
                        DynamicEventRendering="Disabled"
                        BusinessBeginsHour="9"
                        BusinessEndsHour="21"
                      
                        BubbleID="DayPilotBubble1"
                        HeightSpec="Max"
                        ClientObjectName="dps"
                        Height="600"
                        OnCommand="DayPilotScheduler1_Command"
                        EventEditHandling="CallBack"
                        ScrollLabelsVisible="false"
                        xResourceBubbleID="DayPilotBubble1"
                        DragOutAllowed="true"
                        Theme="scheduler_8"
                        OnBeforeEventRender="DayPilotScheduler1_BeforeEventRender">
                        <TimeHeaders>
                            <DayPilot:TimeHeader GroupBy="Day" Format="D" />
                            <DayPilot:TimeHeader GroupBy="Hour" />
                            <DayPilot:TimeHeader GroupBy="Cell" />
                        </TimeHeaders>

                    </DayPilot:DayPilotScheduler>

                    <DayPilot:DayPilotBubble
                        ID="DayPilotBubble2"
                        runat="server"
                        OnRenderContent="DayPilotBubble1_RenderContent"
                        ClientObjectName="bubble"
                        OnRenderEventBubble="DayPilotBubble1_RenderEventBubble"
                        Width="250"
                        Corners="Rounded"
                        Position="EventTop">
                    </DayPilot:DayPilotBubble>
                </div>

            </div>
        </div>
      <%--  </ContentTemplate></asp:UpdatePanel>--%>

    </form>
</body>
</html>
