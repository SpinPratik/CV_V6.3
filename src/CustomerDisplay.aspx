<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomerDisplay.aspx.cs"
    Inherits="CustomerDisplay" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <%--<script>        history.go(1)</script>--%>
    <link href="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484286893/CSS/ProTRAC_Css.css" rel="stylesheet" type="text/css" />
  <%--  <link href="CSS/bootstrap.min2.css" rel="stylesheet" />
    <link href="CSS/custom.css" rel="stylesheet" />
  --%>
    <style type="text/css">
        .Headerdiv
        {
            height: 70px;
            width: 100%;
            background-image: url('Images/protrac2-1.png' );
            background-repeat: no-repeat;
            background-position: center;
            text-align: center;
        }
        .boddy
        {
            margin: 0;
            padding: 0;
            height: 100%;
            background-color: #333333;
        }
        
        .HeaderLeft
        {
            height: 75px;
            width: 250px;
            font-family: Arial;
            font-size: smaller;
            font-weight: bold;
            text-align: center;
        }
        .HeaderCenter
        {
            width: 50%;
            height: 75px;
            font-size: 35px;
            color: #4682B4;
            font-family: Arial;
            font-weight: bold;
            text-align: center;
        }
        .HeaderRight
        {
            padding-right: 5px;
            font-family: Arial;
            font-size: 30px;
            font-weight: bold;
            height: 75px;
            width: 25%;
            text-align: center;
        }
        #contentBox
        {
            height: 600px;
            width: 100%;
        }
        .style1
        {
            height: 17px;
        }
        .back
        {
            top: 10px;
            right: 10px;
            z-index: -1;
            text-align: right;
            vertical-align: top;
        }
        th{
            text-transform:uppercase;
                /*padding-left: 32px;*/
        }
        td{
               padding-left: 20px;
            text-transform:uppercase;
        }
        tr:first-child > td:first-child{

        }
        body{
            font-family:Arial !important;
        }
    </style>
    <title>Customer Display</title>
</head>
<body style="background-color: Black;" onload="startTime()">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table width="100%" cellpadding="0" cellspacing="0" border="0" bgcolor="Black" style="right: 0px;
        left: 0px; top: 0px;">
        <tr>
            <td class="HeaderLeft" valign="middle">
                <table style="width: 100%; height: 100%; text-align: center;">
                    <tr>
                        <td align="center">
                            <asp:Image ID="Image2" runat="server" ImageUrl="images/TML%20Logo.png" Width="200px" Height="60px"/>
                        </td>
                    </tr>
                </table>
            </td>
            <td class="HeaderCenter" align="center" style="color: White;">
                VEHICLE STATUS
            </td>
            <td class="HeaderRight">
                <div style="text-align: center;">
                <%--    <asp:UpdatePanel ID="UpdatePanelTimeShower" runat="server">
                        <ContentTemplate>--%>
                            <asp:Label ID="lbTime" runat="server" Font-Bold="True" 
                                Font-Size="20px" ForeColor="white"></asp:Label>
                            <%--<asp:Timer ID="TimerTimeShower" runat="server" Interval="1000" OnTick="TimerTimeShower_Tick">
                            </asp:Timer>--%>
                        <%--</ContentTemplate>
                    </asp:UpdatePanel>--%>
                </div>
            </td>
        </tr>
        <tr><td>&nbsp;</td></tr>
    </table>
    <div style=" font-size: small; position: fixed; left: 0px;
        right: 0px; width: 100%; background-color: WhiteSmoke" >
      <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>--%>
                <asp:GridView ID="grdDisplay" runat="server" CellPadding="0" Width="100%" ForeColor="#666699"
                    OnRowDataBound="grdDisplay_RowDataBound" CssClass="flipper" PagerSettings-Visible="false" GridLines="Horizontal"
                    Font-Size="24px" AllowPaging="True" PageSize="8" >
                    
                    <PagerSettings Visible="False" />
                    
                    <RowStyle  BackColor="Black" Height="60 px" Font-Bold="true" Font-Size="20px" ForeColor="White" />
                    <FooterStyle BackColor="#666699" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#5C5C99" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" Height="50px" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="Black" Font-Bold="True" Font-Size="20px" ForeColor="White" />
                </asp:GridView>
                <%-- </div>--%>
                <asp:Timer ID="Timer1" Enabled="true"  runat="server" Interval="30000" OnTick="Timer1_Tick">
                </asp:Timer>
                <asp:Timer ID="Timer2" Enabled="true" runat="server" EnableViewState="False" Interval="30000" OnTick="Timer2_Tick">
                </asp:Timer>

                   <script  type="text/javascript" src="https://code.jquery.com/jquery-1.10.1.min.js"></script>
  <%--  <script  type="text/javascript" src="js/jquery.splitflap.js"></script>
    <script type="text/javascript">
    (function ($) {
        $(document).ready(function () {
            $('.do-splitflap')
                    .splitFlap();

            $('.click-splitflap')
                    .splitFlap({
                        textInit:   'Click me ',
                        autoplay:   false,
                        onComplete: function () {
                            console.log('Done !');
                        }
                    })
                    .click(function () {
                        $(this).splitFlap('splitflap').animate();
                    });

            $('.empty-splitflap')
                    .splitFlap({
                        text: 'This is JS'
                    });

            var ratio = 0.4;
            $('.resized-splitflap')
                    .splitFlap({
                        charWidth:  50 * ratio,
                        charHeight: 100 * ratio,
                        imageSize:  (2500 * ratio) + 'px ' + (100 * ratio) + 'px'
                        //half size of original
//                        charWidth:  25 * ratio,
//                        charHeight: 50 * ratio,
//                        imageSize:  (1250 * ratio) + 'px ' + (50 * ratio) + 'px'

                    });
        });
    })(jQuery);


</script>--%>

        <script type="text/javascript">
            function startTime() {
                var monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun",
  "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"
                ];

        var today = new Date();
        var Month = today.getMonth();
        var h = today.getHours();
        var m = today.getMinutes();
        var s = today.getSeconds();
        m = checkTime(m);
        s = checkTime(s);
        document.getElementById('lbTime').innerHTML =
         monthNames[today.getMonth()]+" "+ today.getDate() + " " + h + ":" + m + ":" + s;
        var t = setTimeout(startTime, 500);
    }
    function checkTime(i) {
        if (i < 10) {i = "0" + i};  // add zero in front of numbers < 10
        return i;
    }
</script>
           <%-- </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="grdDisplay" EventName="RowDataBound" />--%>
              <%--  <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                <asp:AsyncPostBackTrigger ControlID="Timer2" EventName="Tick" />--%>
           <%-- </Triggers>
        </asp:UpdatePanel>--%>
    </div>
    <div style="position: fixed; bottom: 0px; left: 0px; right: 0px;">
        <table class="fullStyle" style="height: 100%;" cellpadding="0" cellspacing="0">
            <tr>
                <td style="background-color: #000000;  font-size: 20px;
                    width: 150px; color: white;">
                    <table style="width: 100%; height: 30px">
                        <tr>
                            <td align="center">
                                <img alt="" src="images/spinx.png" style="height: 50px; width: 190px" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="background-color: #000000;">
                    <asp:Label ID="lblScroll" runat="server" Font-Bold="True" ForeColor="White" Font-Size="20px"
                        ></asp:Label>
                <%--    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        SelectCommand="SELECT tblMaster.RegNo+case when TblMaster.Aplus=0 then '' else '*' End as 'Vehicle No' ,OwnerName 'Owner Name',ServiceType 'Service Type',dbo.DF3(PromisedTime) 'Promised Date', Replace((str(DatePart(Hour,PromisedTime))+':'+Case when ( Replace(str(DatePart(Minute,PromisedTime)), ' ','')='0') then '00' else str(DatePart(Minute,PromisedTime)) End),' ','') as 'Time',Remarks 'Status',Position FROM tblMaster INNER JOIN tblCustomer ON tblCustomer.RegNo = tblMaster.RegNo WHERE Position &lt;&gt; 'Delivered' ORDER BY tblMaster.SlNo">
                    </asp:SqlDataSource>--%>
                </td>
                <td style="background-color: #000000; font-family: Consolas, Georgia; font-size: 20px;
                    width: 190px; text-align: center; color: #FFFFFF;">
                   <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>--%>
                            <asp:Label ID="lblPgCount" runat="server" Font-Bold="True"></asp:Label>
                      <%--  </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="grdDisplay" EventName="RowDataBound" />
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                            <asp:AsyncPostBackTrigger ControlID="Timer2" EventName="Tick" />
                        </Triggers>
                    </asp:UpdatePanel>--%>
                </td>
            </tr>
        </table>
    </div>
        
    </form>
   

</body>
</html>
