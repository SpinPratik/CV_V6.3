<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomerDisplay2.aspx.cs"
    Inherits="CustomerDisplay2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script type="text/javascript">        history.go(1)</script>
    <link href="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484214710/css/Style.css" rel="stylesheet" />
    <link href="CSS/Responsive.css" rel="stylesheet" />
    <link href="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484286893/css/Protrac_Css.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .Headerdiv
        {
            height: 45px;
            width: 100%;
            background-image: url('Images/protrac2-1.png');
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
            height: 50px;
            width: 50px;
            font-family: Consolas, Georgia;
            font-size: smaller;
            font-weight: bold;
            position:absolute;
            top:15px;
            left:15px;
        }
        .HeaderCenter
        {
            width: 70%;
            height: 45px;
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
            height: 90px;
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
        th{
            text-transform:uppercase;
        }
        td{
            text-transform:uppercase;
        }
    </style>

    <title>Customer Display</title>
</head>
<body class="boddy">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table width="100%" cellpadding="0" cellspacing="0" border="0" bgcolor="Black">
        <tr>
            <td class="HeaderLeft" valign="middle">
                <table style="width: 100%; height: 100%; text-align: center;">
                    <tr>
                        <td align="center">
                            <div style="text-align: center;">
                               <%-- <asp:Image ID="Image1" runat="server" ImageUrl="~/login_img/clogo.png" />--%>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
            <%--<td> <asp:LinkButton ID="btn_logout" OnClick="btn_logout_Click"  Font-Names="Consolas, Georgia"  HeaderStyle-BackColor="#1591cd" style="vertical-align: ;color:blue;" Text="logout"  runat="server"></asp:LinkButton></td>--%>
            
            
            <td>
                <img src="img/Tata-Motors-Service.jpg" runat="server" align="left" alt="Cannot Preview" style="height:64px;"/>
            </td>
            <td class="HeaderCenter" align="center" style="color: White;">
                VEHICLE STATUS
            </td>
            <td valign="middle" align="right" style="width: 200px; padding-right:3%">
                <asp:UpdatePanel ID="UpdatePanelTimeShower" runat="server" >
                    
                    <ContentTemplate>
                        <asp:Label ID="lbTime" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="20px"
                            ForeColor="White" ></asp:Label>
                        <asp:Timer ID="TimerTimeShower" runat="server" Interval="1000" OnTick="TimerTimeShower_Tick">
                        </asp:Timer>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td >
           <%--<asp:LinkButton runat="server" type="image" class="HeaderLeft" src="img/IMG.jpg" onclick="btn_logout_Click" alt="Submit" align="left" /></asp:LinkButton>--%>
                <asp:ImageButton  ID="ImageButton1" ImageUrl="img/IMG.jpg" Height="50px" ToolTip="Logout"  AlternateText="No Image available" OnClick="btn_logout_Click" runat="server" />
            </td>
            <td class="HeaderRight" style="width: 25px">
            </td>
        </tr>
    </table>
    <div class="scrollview scroll-height" style="font-family: Verdana; font-size: small; left: 0px; right: 0px;
        width: 100%; height: 568px;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:GridView ID="grdDisplay" runat="server" CellPadding="0" Width="100%" ForeColor="#666699"
                    OnRowDataBound="grdDisplay_RowDataBound" PagerSettings-Visible="false" GridLines="None"
                    Font-Size="30px" AllowPaging="True" PageSize="8" Font-Names="Arial">
                    <PagerSettings Visible="False" />
                    <RowStyle BackColor="Gray" Height="50 px" Font-Bold="true" Font-Size="20px" HorizontalAlign="Center"
                        ForeColor="White" />
                    <FooterStyle BackColor="#666699" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#5C5C99" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="Yellow" Height="45px"
                        Font-Size="20px" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="#333333" Font-Bold="True" Font-Size="20px" ForeColor="White" />
                </asp:GridView>
                <asp:Timer ID="Timer1" runat="server" Interval="10000" OnTick="Timer1_Tick">
                </asp:Timer>
                <asp:Timer ID="Timer2" runat="server" EnableViewState="False" Interval="15000" OnTick="Timer2_Tick">
                </asp:Timer>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="grdDisplay" EventName="RowDataBound" />
                <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                <asp:AsyncPostBackTrigger ControlID="Timer2" EventName="Tick" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <div style="position: fixed; bottom: 0px; left: 0px; right: 0px;">
        <table class="fullStyle" style="height: 20px; width: 100%" cellpadding="0" cellspacing="0">
            <tr style="width: 100%">
                <td align="left" style="background-color: #000000; font-family: Arial; font-size: 20px;
                    width: 150px; color: white;">
                    <table style="width: 100%; height: 50px">
                        <tr>
                            <td align="left" width="170">
                                <img alt="" src="https://res.cloudinary.com/deekyp5bi/image/upload/v1484205149/jcr/spinx.png" style="height: 50px; width: 170px" />
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="background-color: #000000; width: 100%;">
                    <asp:Label ID="lblScroll" runat="server" Font-Bold="True" ForeColor="White" Font-Size="20px"
                        Font-Names="Arial"></asp:Label>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        SelectCommand="SELECT tblMaster.RegNo+case when TblMaster.Aplus=0 then '' else '*' End as 'Vehicle No' ,OwnerName 'Owner Name',ServiceType 'Service Type',dbo.DF3(PromisedTime) 'Promised Date', Replace((str(DatePart(Hour,PromisedTime))+':'+Case when ( Replace(str(DatePart(Minute,PromisedTime)), ' ','')='0') then '00' else str(DatePart(Minute,PromisedTime)) End),' ','') as 'Time',Remarks 'Status',Position FROM tblMaster INNER JOIN tblCustomer ON tblCustomer.RegNo = tblMaster.RegNo WHERE Position &lt;&gt; 'Delivered' ORDER BY tblMaster.SlNo">
                    </asp:SqlDataSource>
                </td>
                <td style="background-color: #000000; font-family: Consolas, Georgia; font-size: 20px;
                    width: 160px; text-align: center; color: #FFFF00;" align="right">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label ID="lblPgCount" runat="server" Font-Bold="True" Width="160px"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="grdDisplay" EventName="RowDataBound" />
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                            <asp:AsyncPostBackTrigger ControlID="Timer2" EventName="Tick" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr style="height: 10px; width: 100%">
                <td colspan="3" style="width: 100%">
                    <table id="BayNote" runat="server" border="0" width="100%">
                        <tr style="height: 10px; background-color: #333333; font-family: Arial; font-size: 15px;">
                            <td>
                                <asp:Label ID="label30" runat="server" ForeColor="White" Text="Note: " Font-Bold="True"></asp:Label>
                            </td>
                            <td style="width: 5px; white-space: nowrap;">
                                <asp:Label ID="Label2" runat="server" Text="PDT :" ForeColor="Yellow" Font-Bold="True"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Promised Delivery Time" ForeColor="White"
                                    Font-Bold="True"></asp:Label>
                            </td>
                            <td style="width: 5px; white-space: nowrap;">
                                <asp:Label ID="Label14" runat="server" Text="VI :" ForeColor="Yellow" Font-Bold="True"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Vehicle Inventory" ForeColor="White"
                                    Font-Bold="True"></asp:Label>
                            </td>
                            <td style="width: 5px; white-space: nowrap;">
                                <asp:Label ID="Label15" runat="server" Text="WS :" ForeColor="Yellow" Font-Bold="True"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text="Workshop" ForeColor="White" Font-Bold="True"></asp:Label>
                            </td>
                            <td style="width: 5px; white-space: nowrap;">
                                <asp:Label ID="Label16" runat="server" Text="WA :" ForeColor="Yellow" Font-Bold="True"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label6" runat="server" Text="Wheel Alignment" ForeColor="White" Font-Bold="True"></asp:Label>
                            </td>
                            <td style="width: 5px; white-space: nowrap;">
                                <asp:Label ID="Label17" runat="server" Text="QC :" ForeColor="Yellow" Font-Bold="True"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text="Quality Control" ForeColor="White" Font-Bold="True"></asp:Label>
                            </td>
                            <td style="width: 5px; white-space: nowrap;">
                                <asp:Label ID="Label18" runat="server" Text="WSH :" ForeColor="Yellow" Font-Bold="True"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label19" runat="server" Text="Washing" ForeColor="White" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td colspan="12">
                                <table border="0">
                                    <tr>
                                        <td align="left">
                                            <asp:Image ID="Image6" ImageUrl="https://res.cloudinary.com/deekyp5bi/image/upload/v1484218921/img/Circle_Green.png" Width="20px" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="Label10" runat="server" Text="Completed" ForeColor="White" Font-Bold="True"></asp:Label>
                                        </td>
                                        <td style="width: 15px;" />
                                        <td>
                                            <asp:Image ID="Image7" ImageUrl="https://res.cloudinary.com/deekyp5bi/image/upload/v1484218921/img/Circle_Yellow.png" Width="20px" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="Label11" runat="server" Text="In Progress" ForeColor="White" Font-Bold="True"></asp:Label>
                                        </td>
                                        <td style="width: 15px;" />
                                        <td>
                                            <asp:Image ID="Image8" ImageUrl="https://res.cloudinary.com/deekyp5bi/image/upload/v1484218921/img/Circle_Grey.png" Width="20px" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="Label12" runat="server" Text="Not Started" ForeColor="White" Font-Bold="True"></asp:Label>
                                        </td>
                                        <td style="width: 15px;" />
                                        <td>
                                            <asp:Image ID="Image9" ImageUrl="https://res.cloudinary.com/deekyp5bi/image/upload/v1484218921/img/Circle_Blue.png" Width="20px" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="Label13" runat="server" Text="Not Required" ForeColor="White" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>