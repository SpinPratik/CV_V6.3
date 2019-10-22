<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/AdminModule_New.master" 
    CodeFile="Default.aspx.cs" EnableEventValidation="false"
    Inherits="_Default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title></title>
       <link href="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484285348/Bootstrap/bootstrap.min.css" rel="stylesheet" />
        <script type="text/javascript" src="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484286919/Bootstrap/bootstrap.min.js"></script>
    <script type="text/javascript" src="<%# ResolveUrl("~js/jquery-1.4.1.min.js") %>"></script>
    <script type="text/javascript" src="<%# ResolveUrl("~js/ScrollableGridPlugin.js") %>" ></script>
    <script language="javascript" type="text/javascript">
       
        function divexpandcollapse(divname) {
            var div = document.getElementById(divname);
            var img = document.getElementById('img' + divname);
            if (div.style.display == "none") {
                div.style.display = "inline";
                img.src = "images/minus.gif";
                //alert(divname);
            } else {
                div.style.display = "none";
                img.src = "images/plus.gif";
            }
        }
        </script>
   
    <style type="text/css">
        .mydatagrid a /** FOR THE PAGING ICONS  **/
{
	background-color: Transparent;
	padding: 5px 5px 5px 5px;
	color: #fff;
	text-decoration: none;
	
}
        
.mydatagrid th /** FOR THE PAGING ICONS  **/
{
	border:1px solid #e5e5e4 !important;
    border-bottom:2px solid #e5e5e4 !important;
	padding: 5PX;
    font-weight: 700!important;
   
    font-size: 12px !important;
    text-transform:uppercase;
	
}

.mydatagrid a:hover /** FOR THE PAGING ICONS  HOVER STYLES**/
{
	/*background-color: #000;
	color: #fff;*/
}

.pager
{
	background-color: #646464;
	font-family: Arial;
	color: White;
	height: 30px;
	text-align: left;
}
.mydatagrid tr:nth-child(even) {
   
}

.mydatagrid td
{
    border:none !important;
	padding:5px 10px;
    font-size: 13px!important;
    font-weight: 400!important;
    font-family: "Lato", sans-serif;
    border-bottom:1px solid #e5e5e4 !important;
}


 .mydatagrid a
        {
            display: block;
            /*height: 15px;*/
            width: 15px;
            
            text-align: center;
            text-decoration: none;
        }
        .mydatagrid a
        {
            background-color: unset !important;
            color: #969696;
            /*border: 1px solid #969696;*/
        }
         
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
     <script language="javascript" type="text/javascript">
         
 $(document).ready(function() 
{
     alert('#<%=gvOrders.ClientID %>');
$('#<%=gvOrders.ClientID %>').Scrollable();
}
)
    </script>

        <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div id="JCRDisplay">    

         <div id="searchMenu">
                    <table class="tblStyle" cellspacing="0">
                        <tr>
                            <td valign="top" style="white-space: nowrap; vertical-align: top; color: #333333;
                                 font-size: 12px; text-align: left;">
                            </td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:DropDownList ID="cmbCustType" CssClass="form-control" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                    OnSelectedIndexChanged="cmbCustType_SelectedIndexChanged" TabIndex="1">
                                    <asp:ListItem Value="0">Customer Type</asp:ListItem>
                                    <asp:ListItem Value="0">General</asp:ListItem>
                                    <asp:ListItem Value="1">VIP</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:DropDownList ID="cmbServiceType" CssClass="form-control" runat="server" AppendDataBoundItems="True"
                                    AutoPostBack="True" DataSourceID="srcServiceType" DataTextField="ServiceType"
                                    DataValueField="ServiceType" OnSelectedIndexChanged="cmbServiceType_SelectedIndexChanged"
                                    TabIndex="2">
                                    <asp:ListItem>Service Type</asp:ListItem>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="srcServiceType" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                                    SelectCommand="SELECT [ServiceType] FROM [tblServiceTypes]"></asp:SqlDataSource>
                            </td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:DropDownList ID="cmbVehicleModel" runat="server" AppendDataBoundItems="True"
                                    AutoPostBack="True" DataSourceID="srcVehicleModel" DataTextField="Model" DataValueField="Model"
                                    OnSelectedIndexChanged="cmbVehicleModel_SelectedIndexChanged" TabIndex="3" CssClass="form-control"
                                   >
                                    <asp:ListItem>Model</asp:ListItem>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="srcVehicleModel" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                                    SelectCommand="SELECT DISTINCT Model FROM tblVehicleModel"></asp:SqlDataSource>
                            </td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:DropDownList ID="cmbProcess" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                    DataSourceID="SqlDataSource2" DataTextField="ShowProcessName" DataValueField="ShowProcessName"
                                    OnSelectedIndexChanged="cmbProcess_SelectedIndexChanged" TabIndex="4" CssClass="form-control"  >
                                    <asp:ListItem>Process</asp:ListItem>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                                    SelectCommand="SELECT A.ShowProcessName FROM (SELECT ShowProcessName,Min(ProcessDefaultOrder) B FROM tblProcessList group BY ShowProcessName ) A order by A.B">
                                </asp:SqlDataSource>
                            </td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:DropDownList ID="cmbSA" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                    OnSelectedIndexChanged="cmbCustType_SelectedIndexChanged" TabIndex="1" Visible="True"
                                    CssClass="form-control">
                                </asp:DropDownList>
                            </td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:DropDownList ID="cmbTeamLead" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                    OnSelectedIndexChanged="cmbTeamLead_SelectedIndexChanged" TabIndex="1" Visible="True"
                                    CssClass="form-control">
                                </asp:DropDownList>
                            </td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:DropDownList ID="ddlState" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlState_SelectedIndexChanged" CssClass="form-control">
                                    <asp:ListItem Value="0">Status</asp:ListItem>
                                    <asp:ListItem Value="1">WIP</asp:ListItem>
                                    <asp:ListItem Value="2">Hold</asp:ListItem>
                                    <asp:ListItem Value="3">Idle</asp:ListItem>
                                    <asp:ListItem Value="4">Delay</asp:ListItem>
                                    <asp:ListItem Value="5">OnTime</asp:ListItem>
                                    <asp:ListItem Value="6">Vehicle Ready</asp:ListItem>
                                    <asp:ListItem Value="7">RPDT Informed</asp:ListItem>
                                    <asp:ListItem Value="8">RPDT Not Informed</asp:ListItem>
                                    <asp:ListItem Value="9">JCC Informed</asp:ListItem>
                                    <asp:ListItem Value="10">JCC Not Informed</asp:ListItem>
                                    <asp:ListItem Value="11">Customer Waiting</asp:ListItem>
                                    <asp:ListItem Value="12">JC Not Opened</asp:ListItem>
                                    <asp:ListItem Value="13">Same Day Delivery</asp:ListItem>
                                    <asp:ListItem Value="14">Previous Day Delivery</asp:ListItem>
                                    <asp:ListItem Value="15">Canceled</asp:ListItem>
                                     <asp:ListItem Value="16">White Board</asp:ListItem>
                                     <asp:ListItem Value="17">Yellow Board</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:DropDownList ID="drpOrderBy" runat="server" AutoPostBack="True" 
                                   CssClass="form-control" OnSelectedIndexChanged="drpOrderBy_SelectedIndexChanged">
                                    <asp:ListItem Value="-1">Order By </asp:ListItem>
                                    <asp:ListItem Value="0">Order By PDT</asp:ListItem>
                                    <asp:ListItem Value="1">Order By Gate In</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>&nbsp;</td>
                            <td>
                                <div>
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always">
                                        <ContentTemplate>
                                            <table style="width: 399px;">
                                                <tr>
                                                    <td class="style4">
                                                        <asp:TextBox ID="TxtDate1" runat="server" BackColor="White"  TabIndex="5" ValidationGroup="sg" placeholder="From" CssClass="form-control"></asp:TextBox>
                                                        <cc1:TextBoxWatermarkExtender ID="TxtDate1_TextBoxWatermarkExtender" runat="server"
                                                            Enabled="True" TargetControlID="TxtDate1" WatermarkText="From">
                                                        </cc1:TextBoxWatermarkExtender>
                                                        <cc1:CalendarExtender ID="TxtDate1_CalendarExtender" runat="server" Enabled="True"
                                                            TargetControlID="TxtDate1">
                                                        </cc1:CalendarExtender>
                                                    </td>    <td>&nbsp;</td>
                                                    <td class="style4">
                                                        <asp:TextBox ID="TxtDate2" runat="server" BackColor="White" TabIndex="6" placeholder="To" CssClass="form-control"></asp:TextBox>
                                                        <cc1:TextBoxWatermarkExtender ID="TxtDate2_TextBoxWatermarkExtender" runat="server"
                                                            Enabled="True" TargetControlID="TxtDate2" WatermarkText="To">
                                                        </cc1:TextBoxWatermarkExtender>
                                                        <cc1:CalendarExtender ID="TxtDate2_CalendarExtender" runat="server" Enabled="True"
                                                            TargetControlID="TxtDate2">
                                                        </cc1:CalendarExtender>
                                                    </td>    <td>&nbsp;</td>
                                                    <td class="style4">
                                                        <asp:TextBox ID="txtVehicleNumber" runat="server" TabIndex="7" ValidationGroup="sg"
                                                            CssClass="form-control" placeholder="VRN/VIN"></asp:TextBox>
                                                        <cc1:TextBoxWatermarkExtender ID="txtVehicleNumber_TextBoxWatermarkExtender" runat="server"
                                                            Enabled="True" TargetControlID="txtVehicleNumber" WatermarkText="VRN/VIN">
                                                        </cc1:TextBoxWatermarkExtender>
                                                    </td>    <td>&nbsp;</td>
                                                    <td class="style4">
                                                        <asp:TextBox ID="txtTagNo" runat="server" TabIndex="7" placeholder="VID" ValidationGroup="sg" CssClass="form-control"></asp:TextBox>
                                                        <cc1:TextBoxWatermarkExtender ID="txtTagNo_TextBoxWatermarkExtender" runat="server"
                                                            Enabled="True" TargetControlID="txtTagNo" WatermarkText="VID">
                                                        </cc1:TextBoxWatermarkExtender>
                                                    </td>    <td>&nbsp;</td>
                                                    <td class="style4">
                                                        <asp:ImageButton ID="btnSearch" runat="server" AlternateText="SEARCH" Height="20px"
                                                            ImageUrl="~/Icons/srch1.jpeg" OnClick="btnSearch_Click" ToolTip="Search" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </td>
                            <td>&nbsp;</td>
                            <td style="background-image: url('img/box.png'); background-repeat: no-repeat; background-position: center center;
                                text-align: center; vertical-align: central;padding-left:5px;" width="30">
                                <asp:Label ID="lbVCount" runat="server"  Font-Size="Small" ForeColor="#333333"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div><br />

        <table>
    <tr style="left: 0px">
                                            
        <td valign="top">
                                                <table id="UDeliveredTab" runat="server" style="text-align: right;" border="0">
                                                    <tr><td>&nbsp;</td></tr>
                                                    <tr><td>&nbsp;</td></tr>
                                                    <tr>
                                                        <td valign="top">
                                                <asp:RadioButtonList ID="rbType" runat="server" RepeatDirection="Horizontal" AutoPostBack="True"
                                                    OnSelectedIndexChanged="rbType_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True" Value="0">Today&nbsp;&nbsp;</asp:ListItem>
                                                    <asp:ListItem Value="1">Next Day&nbsp;&nbsp;</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>

                                                        <td style="white-space: nowrap;  color: #666666;">
                                                             &nbsp;&nbsp; WORKSHOP STATUS :
                                                        </td>
                                                        <td style="white-space: nowrap;  color: #666666;">
                                                            &nbsp;&nbsp;TOTAL
                                                        </td>
                                                        <td width="25px" style="border-bottom-width: thick; border-width: medium; text-align: center;">
                                                            <asp:Label ID="lbUnDelivered" runat="server" Text="" ForeColor="Black" ></asp:Label>
                                                        </td>
                                                        <td width="10px" style="white-space: nowrap; color: Red; text-align: center;">
                                                           &nbsp;&nbsp; |&nbsp;&nbsp;
                                                        </td>
                                                        <td style="white-space: nowrap;  color: #666666;border-bottom: 2px solid #666666;">
                                                            <img alt="WIP" src="https://res.cloudinary.com/deekyp5bi/image/upload/v1484205149/jcr/Status.png" width='20' height='20' />
                                                            
                                                        </td>
                                                        <td style="white-space: nowrap;  color: #666666;    border-bottom: 2px solid #666666;">
                                                            WIP&nbsp;&nbsp;
                                                        </td>
                                                        <td width="25px" style="border-bottom-width: thick; border-width: medium; text-align: center;    border-bottom: 2px solid #666666;">
                                                            <asp:Label ID="lbWIP" runat="server" Text="" ForeColor="Black" ></asp:Label>
                                                        </td>
                                                        <td width="10px" style="white-space: nowrap; color: Red; text-align: center;">
                                                            &nbsp;&nbsp; |&nbsp;&nbsp;
                                                        </td>
                                                        <td style="white-space: nowrap;  color: #666666;    border-bottom: 2px solid #99C68E;">
                                                            <img alt="Ready" src="https://res.cloudinary.com/deekyp5bi/image/upload/v1484205149/jcr/Status_Ready.png" width='20' height='20' />
                                                            
                                                        </td>
                                                        <td style="white-space: nowrap;  color: #666666;    border-bottom: 2px solid #99C68E;">
                                                            READY&nbsp;&nbsp;
                                                        </td>
                                                        <td width="25px" style=" border-bottom: 2px solid #99C68E; 
                                                            text-align: center;">
                                                            <asp:Label ID="lbReady" runat="server" Text="" ForeColor="Black" ></asp:Label>
                                                        </td>
                                                        <td width="10px" style="white-space: nowrap; color: Red; text-align: center;">
                                                            &nbsp;&nbsp; |&nbsp;&nbsp;
                                                        </td>
                                                        <td style="white-space: nowrap;  color: #666666; border-bottom: 2px solid Red;">
                                                            <img alt="Idle" src="https://res.cloudinary.com/deekyp5bi/image/upload/v1484205149/jcr/Status_IDLE.png" width='20' height='20' />
                                                             
                                                        </td>
                                                        <td style="white-space: nowrap;  color: #666666;border-bottom: 2px solid Red;">
                                                           IDLE&nbsp;&nbsp;
                                                        </td>
                                                        <td width="25px" style="border-bottom: 2px solid Red;
                                                            text-align: center;" >
                                                            <asp:Label ID="lbIdle" runat="server" Text="" ForeColor="Black" ></asp:Label>
                                                        </td>
                                                        <td width="10px" style="white-space: nowrap; color: Red; text-align: center;">
                                                            &nbsp;&nbsp; |&nbsp;&nbsp;
                                                        </td>
                                                        <td style="white-space: nowrap;  color: #666666;border-bottom: 2px solid #688CD9;">
                                                            <img alt="Hold" src="https://res.cloudinary.com/deekyp5bi/image/upload/v1484205149/jcr/Status_HOLD.png" width='20' height='20' />
                                                         
                                                        </td>
                                                        <td style="white-space: nowrap;  color: #666666;border-bottom: 2px solid #688CD9;">
                                                            HOLD&nbsp;&nbsp;
                                                        </td>
                                                        <td width="25px" style="border-bottom: 2px solid #688CD9;
                                                            text-align: center;">
                                                            <asp:Label ID="lbHold" runat="server" Text="" ForeColor="Black" ></asp:Label>
                                                        </td>
                                                        <td width="10px" style="white-space: nowrap; color: Red; text-align: center;">
                                                            &nbsp;&nbsp;|&nbsp;&nbsp;
                                                        </td>
                                                         <td style="white-space: nowrap;  ">
                                                           Total Vehicles received &nbsp;&nbsp;
                                                        </td>
                                                        <td width="25px" style="text-align: center;" >
                                                            <asp:Label ID="lblTotalReceived" runat="server" Text="" ForeColor="Black" ></asp:Label>
                                                        </td>
                                                        <td width="10px" style="white-space: nowrap; color: Red; text-align: center;">
                                                            &nbsp;&nbsp;|&nbsp;&nbsp;
                                                        </td>
                                                         <td style="white-space: nowrap;  ">
                                                           Total Vehicles delivered&nbsp;&nbsp;
                                                        </td>
                                                        <td width="25px" style="text-align: center;" >
                                                            <asp:Label ID="lblVehDel" runat="server" Text="" ForeColor="Black" ></asp:Label>
                                                        </td>
                                                        <td width="10px" style="white-space: nowrap; color: Red; text-align: center;">
                                                            &nbsp;&nbsp;|&nbsp;&nbsp;
                                                   
                                                    </tr>
                                                </table>
                                            </td>
                                        
                                        </tr>
                                    </table>

        <br />



        <asp:GridView ID="gvOrders"  CssClass="mydatagrid" Width="100%" runat="server" AutoGenerateColumns="false" 
             OnRowDataBound="gvOrders_OnRowDataBound">
            <Columns>
               <%-- <asp:TemplateField ItemStyle-Width="20px">
                    <ItemTemplate>
                        <a href="JavaScript:divexpandcollapse('div<%# Eval("RefNo") %>');">
                            <img id="imgdiv<%# Eval("RefNo") %>" width="9px" border="0" src="Images/plus.gif"
                                alt="" /></a>                        
                    </ItemTemplate>
                    <ItemStyle Width="20px" VerticalAlign="Middle"></ItemStyle>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="Order ID" Visible="false">
                <ItemTemplate>
                 <asp:Label ID="lblorderID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "RefNo") %>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>                
                <asp:BoundField DataField="#" HeaderText="SlNo" />
                
                 <asp:BoundField DataField="VID" HeaderText="VID" />     
                <asp:BoundField DataField="VRN/VIN" HeaderText="RegNo" />
                <asp:BoundField DataField="JDP CW" HeaderText="JDP-CW" />
                <asp:BoundField DataField="MODEL" HeaderText="MODEL" />
                <asp:BoundField DataField="ST" HeaderText="ST" />
                <asp:BoundField DataField="STATUS" HeaderText="STATUS" />
                  <asp:BoundField DataField="JC" HeaderText="JC" />
                  <asp:BoundField DataField="PARTS" Visible="false" HeaderText="PARTS" />
                <asp:BoundField DataField="BA" HeaderText="BA" />
                <asp:BoundField DataField="T1" HeaderText="T1" />
                <asp:BoundField DataField="T2" HeaderText="T2" />
                <asp:BoundField DataField="T3" HeaderText="T3" />
                <asp:BoundField DataField="WA" HeaderText="WA" />
                 <asp:BoundField DataField="RT" HeaderText="RT" />
                <asp:BoundField DataField="WASH" HeaderText="WASH" />
                 <asp:BoundField DataField="QC" HeaderText="QC" />
                <asp:BoundField DataField="VAS" Visible="false" HeaderText="VAS" />
                <asp:BoundField DataField="JCC" HeaderText="JCC" />
                <asp:BoundField DataField="PRG" HeaderText="PRG" />
                <asp:BoundField DataField="PDT" HeaderText="PDT" /> 
                <asp:BoundField DataField="Age" HeaderText="AGE" /> 
                  
                <asp:BoundField DataField="RMK" HeaderText="RMK" />
                 <asp:BoundField DataField="RefNo" HeaderText="RefNo" Visible="false"/>
                 <asp:BoundField DataField="ERT" HeaderText="ERT" />           
                <asp:TemplateField>
                    <ItemTemplate>
                        <tr>
                           <td colspan="100%" style="background:#F5F5F5">
                             <div id="div<%# Eval("RefNo") %>"  style="overflow:auto; display:none; position: relative; left: 15px; overflow: auto">
                              
                                   
                                     <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                                         <ContentTemplate>
                                 <table>
                                       <tr>
                                           <td><b>Service/Process Remarks</b></td>
                                       </tr>
                                       <tr>
                                                        
                                                        <td valign="top">
                                                            <asp:Label ID="lblspvc" runat="server" Text="VIN/VRN"></asp:Label>
                                                            <asp:TextBox ID="lblspvehicleno" runat="server" Text='<%# Eval("VRN/VIN") %>' CssClass="form-control" ></asp:TextBox><asp:Label
                                                                ID="lblservid" runat="server"  Font-Size="Small" ForeColor="#663300"
                                                                Visible="False"></asp:Label>
                                                        </td>
                                                        
                                                                    
                                                                    <td valign="top">
                                                                         <asp:Label ID="lbltype" runat="server" Text="Type"></asp:Label>
                                                                        <asp:DropDownList ID="drpsptype" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpsptype_SelectedIndexChanged" CssClass="form-control" 
                                                                            TabIndex="30">
                                                                            <asp:ListItem Value="0">Service</asp:ListItem>
                                                                            <asp:ListItem Value="1">Process</asp:ListItem>
                                                                            <asp:ListItem Value="2">Carry Forward</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    
                                                                    <td valign="top">
                                                                         <asp:Label ID="lblspprocess" runat="server" Text="Process" Visible="False"></asp:Label>
                                                                        <asp:DropDownList ID="drpspprocess" CssClass="form-control" runat="server" DataSourceID="SqlDataSource1"
                                                                            DataTextField="ShowProcessName" DataValueField="ProcessId" TabIndex="31" Visible="False">
                                                                        </asp:DropDownList>
                                                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                                                                            SelectCommand="SELECT ShowProcessName, ProcessId FROM tblProcessList ORDER BY ProcessDefaultOrder">
                                                                        </asp:SqlDataSource>
                                                                    </td>

                                                                    <td valign="top">
                                                                          <asp:Label ID="lblrmrktyp" runat="server" Text="Remarks"></asp:Label>
                                                                    <asp:DropDownList ID="ddlSRemarks" CssClass="form-control" runat="server" AutoPostBack="True" 
                                                                        AppendDataBoundItems="True">
                                                                    </asp:DropDownList>
                                                                        
                                                                </td>
                                                        
                                                        <td> <asp:TextBox ID="txtspremarks" placeholder="Enter Remarks" runat="server" MaxLength="100" TabIndex="32" TextMode="MultiLine"
                                                                        CssClass="form-control" Visible="false"></asp:TextBox></td>
                                                        
                                                                     <td valign="top">
                                                                         <asp:Label ID="lblServiceAction" runat="server" Text="Action"></asp:Label>
                                                        <asp:TextBox ID="txtServiceAction" runat="server" MaxLength="250" TextMode="MultiLine"
                                                            CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                                    <td valign="top">
                                                                          <asp:Label ID="lblServiceRecom" runat="server" Text="Recomendation"></asp:Label>
                                                        <asp:TextBox ID="txtRecomendation" runat="server" CssClass="form-control" MaxLength="250" TextMode="MultiLine"></asp:TextBox>
                                                    </td>
                                                                    <td valign="top" class="style8"><br />
                                                                        <asp:Button CssClass="btn btn-success" ID="btnspsave" runat="server" Text="Save" OnClick="btnspsave_Click"
                                                                            TabIndex="33" ToolTip="Save" ValidationGroup="a" />&nbsp;
                                                                    </td>
                                           <td><asp:Label runat="server" ID="lblmsg"></asp:Label></td>
                                                               
                                                    </tr>
                                       <tr>
                                           <td><b>Technician Remarks</b></td>
                                       </tr>
                                      <tr>
                                                 
                                                    <td>
                                                        <asp:Label ID="Label26" runat="server" Text="Technician"></asp:Label>
                                                        <asp:DropDownList ID="ddlTechList" runat="server" CssClass="form-control" style="width:unset" AppendDataBoundItems="True" >
                                                        </asp:DropDownList>
                                                    </td>
                                                   
                                                     <td>
                                                         <asp:Label ID="Label27" runat="server" Text="Remarks"></asp:Label>
                                                  
                                                        <asp:TextBox ID="txt_TechRemarks" runat="server" CssClass="form-control"  MaxLength="200"></asp:TextBox>
                                                    </td>
                                                    <td  style="white-space: nowrap"><br />
                                                        <asp:Button ID="btn_SaveTechRemarks" runat="server" CssClass="btn btn-success" OnClick="btn_SaveTechRemarks_Click"
                                                            Text="Save" ValidationGroup="aa1" />
                                                    </td>
                                                      <td>
                                                         <asp:Label ID="lbl_TechRefId" runat="server" Visible="False" ></asp:Label>
                                                        <asp:Label ID="lbl_Techmsg" runat="server" Visible="True" ></asp:Label>
                                                   </td>
                                                </tr>

                                   </table>
                                             </ContentTemplate>
                           </asp:UpdatePanel>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>              
            <HeaderStyle BackColor="#1591cd" ForeColor="White" Font-Names="Calibri"/>
            <RowStyle Font-Names="Calibri"/>
        </asp:GridView>                 
        </div> 
    <%--    <asp:DropDownList runat="server" ID="ddl1" AutoPostBack="true" OnSelectedIndexChanged="ddl1_SelectedIndexChanged">
            <asp:ListItem Value="1">One</asp:ListItem>
             <asp:ListItem Value="2">Two</asp:ListItem>
             <asp:ListItem Value="3">Three</asp:ListItem>
             <asp:ListItem Value="4">Four</asp:ListItem>
        </asp:DropDownList>
        
        <div ID="DynamicText" runat="server">

        </div>--%>
    
</asp:Content>
