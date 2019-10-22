<%@ Page Title="" Language="C#" MasterPageFile="~/AdminModule_New.master" AutoEventWireup="true"
    CodeFile="TagAllotment.aspx.cs" Inherits="TagAllotment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--  <link href="Bootstrap/bootstrap.min.css" rel="stylesheet" />
   <link href="CSS/Style.css" rel="stylesheet" />--%>
   <link href="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484285057/Bootstrap/bootstrap.min.css" rel="stylesheet" />
   <link href="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484285348/css/Style.css" rel="stylesheet" />
    <style type="text/css">
        .style1 {
            width: 330px;
        }

        .style3 {
            width: 332px;
        }

        .style9 {
            width: 172px;
        }

        .tblStyle1 {
            font-family: "Helvetica Neue", Helvetica, Arial, sans-serif;
            font-size: small;
            vertical-align: top;
            width: 334px;
        }

        .style20 {
            width: 83px;
        }

        .style23 {
            width: 219px;
            height: 22px;
        }

        .style25 {
            width: 194px;
            height: 22px;
            white-space: nowrap;
        }

        .style26 {
            width: 197px;
            white-space: nowrap;
        }

        .style27 {
            width: 197px;
            height: 22px;
            white-space: nowrap;
        }

        .style29 {
            width: 149px;
        }

        .style30 {
            width: 33%;
            white-space: nowrap;
        }

        .style31 {
            width: 80px;
            height: 22px;
            white-space: nowrap;
        }

        .style32 {
            width: 219px;
        }

        .style33 {
            width: 219px;
            height: 22px;
            white-space: nowrap;
        }
        /*gridview*/


        .header {
            background-color: #646464;
            font-family: Arial;
            color: White;
            border: none 0px transparent;
            height: 25px;
            text-align: center;
            font-size: 16px;
        }

        .rows {
            background-color: #fff;
            font-family: Arial;
            font-size: 14px;
            color: #000;
            min-height: 25px;
            text-align: left;
            border: none 0px transparent;
        }

            .rows:hover {
                background-color: #ff8000;
                font-family: Arial;
                color: #fff;
                text-align: left;
            }

        .selectedrow {
            background-color: #ff8000;
            font-family: Arial;
            color: #fff;
            font-weight: bold;
            text-align: left;
        }

        .mydatagrid a /** FOR THE PAGING ICONS  **/ {
            background-color: Transparent;
            padding: 5px 5px 5px 5px;
            color: #fff;
            text-decoration: none;
            font-weight: bold;
        }

        .mydatagrid th /** FOR THE PAGING ICONS  **/ {
            border: 1px solid #e5e5e4 !important;
            border-bottom: 2px solid #e5e5e4 !important;
            padding: 12PX;
            font-weight: 700 !important;
            color: #000000;
            font-size: 12px !important;
            text-transform: uppercase;
        }

        .mydatagrid a:hover /** FOR THE PAGING ICONS  HOVER STYLES**/ {
            /*background-color: #000;
	color: #fff;*/
        }

        /*.mydatagrid span {
            background-color: #c9c9c9;
            color: #000;
            padding: 5px 5px 5px 5px;
        }*/

        .pager {
            background-color: #646464;
            font-family: Arial;
            color: White;
            height: 30px;
            text-align: left;
        }

        .mydatagrid td {
            padding: 5px 10px;
            font-size: 13px !important;
            font-weight: 400 !important;
            font-family: "Lato", sans-serif;
            border-bottom: 1px solid #e5e5e4 !important;
        }

        .mydatagrid tr:nth-child(even) {
            background-color: #ffffff !important;
        }

        .mydatagrid tr:nth-child(odd) {
            background-color: #f9f9f9 !important;
        }

        .mydatagrid a, .mydatagrid span {
            display: block;
            /*height: 15px;*/
            width: 15px;
            font-weight: bold;
            text-align: center;
            text-decoration: none;
        }

        .mydatagrid a {
            background-color: #f5f5f5;
            color: #969696;
            /*border: 1px solid #969696;*/
        }

        /*.mydatagrid span {
            background-color: #A1DCF2;
            color: #000;
            border: 1px solid #3AC0F2;
        }*/
       
        #header {
            background-color: white !important;
            padding: 2em !important;
        }

        .btn {
            text-transform: uppercase;
        }

        label {
            font-weight: unset;
        }

        span {
            text-transform: uppercase;
            font-size: 12px;
        }

       

       
    </style>
     
<script type="text/javascript" language="javascript">
        function ValidateAlpha() {
            var keyCode = window.event.keyCode;
            if ((keyCode < 65 || keyCode > 90) && (keyCode < 97 || keyCode > 123) && (keyCode < 48 || keyCode > 57) && keyCode != 32) {
                window.event.returnValue = false;
                alert("Please Enter only AlphaNumeric Values");
            }
        }
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <%--<div style="margin-right: 22px;">
        <asp:Button CssClass="btn btn-info" Style="float: right; border: 1px solid #286090; color: #286090; font-weight: 800; font-size: 12px; font-family: tahoma, arial, Verdana; background-color: white;" Text="SA TAG MAPPING" ID="btn_SATagMapping" OnClick="btn_SATAGMapping_Click" runat="server" />
    </div>--%>
     <div>
    </div>
  
    <div style="margin-right: 17px;">
<%--        <asp:Button CssClass="btn btn-info" Style="float: right; border: 1px solid #286090; color: #286090; font-weight: 800; font-size: 12px; font-family: tahoma, arial, Verdana; background-color: white;" Text="DISPLAY" ID="btn_display" OnClick="btn_display_Click" runat="server" />--%>
        <asp:Button CssClass="btn btn-info" Style="float: right; border: 1px solid #286090; color: #286090; font-weight: 800; font-size: 12px; font-family: tahoma, arial, Verdana; background-color: white;" Text="service advisor display" ID="btn_displaylink" OnClick="btn_displaylink_Click" runat="server" />
    </div>
    </div>
    </div>
    <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Style="margin-bottom: 10px"
        Width="100%" OnActiveTabChanged="TabContainer1_ActiveTabChanged" CssClass="MyTabStyle" Height="580px"
        AutoPostBack="True">
        <cc1:TabPanel runat="server" hedertext="TabPanel2" ID="TabPanel2">
            <HeaderTemplate>
                customer details
            </HeaderTemplate>
            <ContentTemplate>
                <table style="height: 370px; width: 100%;">
                    <tr style="background-color: white; font-family: Helvetica Neue, Helvetica, Arial, sans-serif">
                        <td align="center" style="border-bottom: 6px solid #5f9eee; padding: 10px; color: black;" valign="top" class="style30">
                            <asp:Label ID="Label10" runat="server" Style="font-size: small; text-transform: uppercase; font-weight: 700;"
                                Text="Customer Details"></asp:Label>
                        </td>
                        <td align="center" style="border-bottom: 6px solid #76c187; padding: 10px; color: black;" valign="top" style="width: 25%; white-space: nowrap;">
                            <asp:Label ID="Label21" runat="server" Style="font-size: small; text-transform: uppercase; font-weight: 700;"
                                Text="Customer Arrival List"></asp:Label>
                        </td>
                        <td align="center" valign="top" style="width: 44%; border-bottom: 6px solid #9e7da6; padding: 10px; color: black; white-space: nowrap;">
                            <%--<asp:Label ID="Label1" runat="server" Style="font-size: small; text-transform: uppercase; font-weight: 700;"
                                Text="Alloted Vehicles List"></asp:Label>--%>
                        </td>
                    </tr>
                    <tr style="font-family: Helvetica Neue, Helvetica, Arial, sans-serif; color: #333333">
                        <td align="left" style="vertical-align: top;" class="style30">
                            <table width="100%;">
                                <tr>
                                    <td class="style2" colspan="2">
                                        <asp:Label ID="message" runat="server" Width="100%" ></asp:Label><asp:Label ID="lblSlno"
                                            runat="server" Visible="False"></asp:Label><asp:Label ID="errSrchTag" runat="server"
                                                CssClass="clsValidator"></asp:Label>
                                        <asp:Label ID="lblMsg" runat="server" Font-Bold="False" CssClass="clsValidator" ForeColor="Black"></asp:Label>

                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>

                                <tr>

                                    <td valign="top" class="style32">
                                        <asp:Label ID="Label5" runat="server" Text="VID"></asp:Label><span style="color:red">&nbsp;*</span>
                                        <asp:TextBox ID="txtRFID" ReadOnly="True" CssClass="form-control" runat="server" Style="margin-left: 0px"
                                            Width="180px"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender
                                            ID="FilteredTextBoxExtender7" runat="server" Enabled="True" FilterType="Numbers"
                                            TargetControlID="txtRFID" ValidChars="0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                        <asp:Button CssClass="btn btn-success" ID="btnSave" runat="server" OnClick="btnsave_Click" Style="padding: 2px;width: 38%;    margin-top: 5px;"
                                            Text="Save" Visible="False" />&nbsp;&nbsp;&nbsp;<asp:Button CssClass="btn btn-info" ID="btnCancel" Style="padding: 2px;width: 38%;    margin-top: 5px;" runat="server"
                                                OnClick="btnCancel_Click" Text="Cancel" Visible="False" />
                                    </td>
                                    <td class="style26">
                                        <asp:Label ID="Label22" runat="server" Text=" VRN/VIN"></asp:Label><span style="color:red">&nbsp;*</span>
                                       
                                      <asp:TextBox ID="txtRegNO" CssClass="form-control" ToolTip="Enter Pattern like KA51AB4242" placeholder="2Letters & 4Digits" onKeyPress="ValidateAlpha()" runat="server"  MaxLength="10" style="text-transform:uppercase" Width="180px" AutoPostBack="True" OnTextChanged="txtRegNO_TextChanged"></asp:TextBox>
                                        <br />
                                        <asp:Button CssClass="button" ID="Button2" runat="server"
                                            Text="Save" Visible="False" />
                                    </td>
                                </tr>

                                <tr>
                                    <td align="left" class="style32">
                                        <asp:Label ID="Label6" runat="server" Text="Model"></asp:Label><span style="color:red">&nbsp;*</span>
                                        <asp:DropDownList ID="drpModel" CssClass="form-control" runat="server" Width="182px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblcustomer0" runat="server" Text="Name"></asp:Label><span style="color:red">&nbsp;*</span>
                                        <asp:TextBox ID="txtCustomer" CssClass="form-control" runat="server" MaxLength="25" Width="180px"></asp:TextBox>

                                    </td>

                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>

                                <tr>

                                    <td align="left" class="style32">
                                        <asp:Label ID="lblcustphone" runat="server" Text="Contact No"></asp:Label><span style="color:red">&nbsp;*</span>

                                        <asp:TextBox ID="txtPhone1" CssClass="form-control" runat="server" MaxLength="10" Width="180px"></asp:TextBox><cc1:FilteredTextBoxExtender
                                            ID="txtPhone1_FilteredTextBoxExtender" runat="server" Enabled="True" FilterType="Numbers"
                                            TargetControlID="txtPhone1" ValidChars="0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label11" runat="server" Text="Email"></asp:Label>
                                        <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" Width="180px"></asp:TextBox>
                                    </td>

                                </tr>

                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label7" runat="server" Text="Previous Service Type"></asp:Label>&nbsp;
                                       <asp:TextBox ID="txtServiceType" CssClass="form-control" runat="server" Width="180px" ReadOnly="True"></asp:TextBox>

                                    </td>
                                    <td align="left" class="style33" valign="top">
                                        <asp:Label ID="Label8" runat="server" Text=" Previous Visit Date"></asp:Label>&nbsp;

                                        <asp:TextBox ID="txtVisitingDate" CssClass="form-control" runat="server" MaxLength="12" Width="180px" ReadOnly="True"></asp:TextBox>
                                    </td>

                                </tr>

                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label9" runat="server" Text=" Previous Service Advisor"></asp:Label>&nbsp;
                                        <asp:TextBox ID="txtPreviousSA" CssClass="form-control" runat="server" Width="180px" MaxLength="30" ReadOnly="True"></asp:TextBox>

                                    </td>

                                    <td valign="top" class="style23">
                                        <%--<asp:CheckBox ID="chkbodyshop" runat="server" Font-Bold="False" Text="&nbsp;&nbsp;<img src='images/bodyshop.png' />&nbsp;&nbsp;BODYSHOP" ToolTip="Appointment" AutoPostBack="True" OnCheckedChanged="chkbodyshop_CheckedChanged" />--%>
                                        <asp:CheckBox ID="chkbodyshop" runat="server" Font-Bold="False" Text="&nbsp;&nbsp;<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484286184/bodyshop_a81shh.png' />&nbsp;&nbsp;BODYSHOP" ToolTip="Appointment" AutoPostBack="True" OnCheckedChanged="chkbodyshop_CheckedChanged" />
                                    </td>

                                </tr>
                                <tr>
                                    <td style="text-align: left;" class="style32" valign="middle">
                                        <br />

                                        <asp:CheckBox ID="chkAppointment" runat="server" Font-Bold="False" Text="&nbsp;&nbsp;<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484284892/time-planning_wqengj.png' />&nbsp;&nbsp;Appointment" ToolTip="Appointment" />
                                        <asp:CheckBox ID="chkWalkIn" runat="server" Visible="False" Text="Walk In" ToolTip="Walk In" OnCheckedChanged="chkWalkIn_CheckedChanged"
                                            AutoPostBack="True" />
                                    </td>
                                    <td style="text-align: left;" class="style32" valign="middle">
                                      
                                         <asp:CheckBox ID="chk_WhiteBoard" runat="server" Visible="False" Text="White Board" ToolTip="White Board"
                                             Checked="True" AutoPostBack="True"
                                             OnCheckedChanged="chk_WhiteBoard_CheckedChanged" />
                                        <%--<asp:Label ID="Label14" runat="server" Text="Service Advisor"></asp:Label><span style="color:red">&nbsp;*</span>
                                        <asp:DropDownList ID="cmbSAList" CssClass="form-control" runat="server" Width="182px">
                                        </asp:DropDownList>--%>

                                      
                                    </td>
                                </tr>
                                 <tr>
                                    <td>   <asp:CheckBox ID="chk_Yellow" runat="server" Font-Bold="False" Text="&nbsp;&nbsp;<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484285049/frontal-taxi-cab_1_lv26yz.png' style='WIDTH: 13PX; HEIGHT: 13PX;' />&nbsp;&nbsp;TAXI" ToolTip="TAXI" />
                                   </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td style="text-align: left;" class="style32">
                                        <asp:Button ID="btnMap" runat="server" ImageAlign="Middle" OnClick="Button2_Click"
                                            Text="Next" ToolTip="Next" Width="42%" class="btn btn-success" />&nbsp;&nbsp;<asp:Button ID="btn_Clear" Width="42%" runat="server"
                                                ImageAlign="Middle" OnClick="btn_Clear_Click" class="btn btn-info" Text="Clear" ToolTip="Clear" />
                                    </td>
                                </tr>

                            </table>
                        </td>
                        <td valign="top" class="style3" rowspan="6" style="padding: 10px;">
                            <table class="tblMain" style="font-size: small; margin-left: 0px; width: 100%">
                                <tr>
                                    <td>
                                        <table class="commonFont">
                                            <tr>
                                                <td>&nbsp;&nbsp;</td>
                                            </tr>
                                            <tr>

                                                <td>
                                                    <asp:Label ID="Label17" runat="server" Text="Search VID"></asp:Label>
                                                    <asp:TextBox ID="txtSrchTag" CssClass="form-control" Style="padding-right: 30px" MaxLength="20" runat="server" Width="100%"></asp:TextBox><cc1:FilteredTextBoxExtender
                                                        ID="txtSrchTagExtender" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtSrchTag">
                                                    </cc1:FilteredTextBoxExtender>
                                                </td>

                                                <td style="width: 20px; padding-left: 5px;">
                                                    <%--<asp:ImageButton ID="btnSrchTag" Style="margin-left: -28px; margin-top: 22px;" runat="server" AlternateText="Search" ImageUrl="~/img/search_2.png"
                                                        OnClick="btnSrchTag_Click" Width="16px" />--%>
                                                    <asp:ImageButton ID="ImageButton2" Style="margin-left: -28px; margin-top: 22px;" runat="server" AlternateText="Search" ImageUrl="https://res.cloudinary.com/deekyp5bi/image/upload/v1484285976/search_2_ll3ccm.png"
                                                        OnClick="btnSrchTag_Click" Width="16px" />
                                                </td>
                                                <td style="width: auto;"></td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;&nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>

                                        <asp:GridView ID="grdSAAssign" CssClass="mydatagrid" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                            CellPadding="4" ForeColor="#333333" Font-Bold="False" GridLines="None" OnPageIndexChanging="grdSAAssign_PageIndexChanging"
                                            OnSelectedIndexChanged="grdSAAssign_SelectedIndexChanged" Style="text-align: left; width: 100%">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:CommandField ButtonType="Image" SelectImageUrl="https://res.cloudinary.com/deekyp5bi/image/upload/v1484224665/jcr/gridSelect.png" ShowSelectButton="True" />
                                                <asp:BoundField DataField="RFId" HeaderText="VID" />
                                                <asp:BoundField DataField="GateIn" HeaderText="Arrival Time" />
                                                <asp:BoundField DataField="SlNo" HeaderText="SlNo" />
                                            </Columns>
                                            <EditRowStyle BackColor="Silver" />
                                            <EmptyDataTemplate>
                                                <center>
                                                    <div class="NoData">
                                                        No Pending List
                                                    </div>
                                                </center>
                                            </EmptyDataTemplate>
                                            <PagerStyle HorizontalAlign="Left" />
                                            <SelectedRowStyle Font-Bold="True" ForeColor="#1591cd" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <br />
                                        <asp:Button CssClass="btn btn-success" ID="btnAddNew" runat="server" Text="Add New" style="width: 100px;" OnClick="btnAddNew_Click" />
                                        &nbsp;
                                            <asp:Button CssClass="btn btn-info" ID="btnRefresh" runat="server" AlternateText="Refresh" Text="Refresh"
                                                OnClick="btnRefresh_Click" /><br />

                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="style9" valign="top" style="padding: 10px;">
                            <br />
                            <%--<asp:GridView ID="grdSAMapping" CssClass="mydatagrid" runat="server" Style="text-align: left;" CellPadding="3"
                                ForeColor="#333333" Font-Bold="False" GridLines="None" OnPageIndexChanging="grdSAMapping_PageIndexChanging"
                                OnRowDataBound="grdSAMapping_RowDataBound" PageSize="15" Font-Size="13px" Width="100%">
                                <AlternatingRowStyle BackColor="White" />
                                <EditRowStyle BackColor="WhiteSmoke" />
                                <FooterStyle BackColor="Silver" ForeColor="#333333" />
                                <HeaderStyle BackColor="Silver" ForeColor="#333333" HorizontalAlign="Left" Font-Bold="False" />
                                <PagerStyle BackColor="Silver" ForeColor="#333333" HorizontalAlign="Left" />
                                <RowStyle BackColor="WhiteSmoke" HorizontalAlign="Left" />
                            </asp:GridView>--%>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="Edit Customer Details">
            <HeaderTemplate>
                Edit Customer Details
            </HeaderTemplate>
            <ContentTemplate>
                <table class="tblStyle" style="width: 100%;">
                    <tr>
                        <td colspan="2" style="width: 100%;" >
                            <asp:Label ID="lblEditCustMsg" runat="server" Font-Bold="False" style="width: 100%;" CssClass="clsValidator"></asp:Label>
                        </td>
                    </tr>
                    <tr>

                        <td valign="middle" class="style20">
                            <table cellpadding="0" cellspacing="0" border="0" style="width: 100%">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label15" runat="server" Text="VRN/VIN"></asp:Label>
                                        <asp:DropDownList ID="ddlVehicle" CssClass="form-control" AutoPostBack="True" runat="server" AppendDataBoundItems="True"
                                            OnSelectedIndexChanged="ddlVehicle_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 50px;">
                                        <asp:ImageButton ID="Button1" runat="server"  CssClass="btn btn-default" Style="margin-top: 25px;padding:2px;" ImageUrl="https://res.cloudinary.com/deekyp5bi/image/upload/v1484284612/refresh_vahbyd.png"
                                            OnClick="Refresh_Click" AlternateText="Refresh" />
                                    </td>

                                </tr>
                            </table>
                        </td>

                        <td>
                            <br />
                            <asp:Label ID="Label16" runat="server" Text="Customer Name"></asp:Label>
                            <asp:TextBox ID="txtCustName" runat="server" CssClass="form-control" MaxLength="25"></asp:TextBox>
                          <%--  <asp:RequiredFieldValidator
                                ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtCustName"
                                ErrorMessage="*" ValidationGroup="editcust"></asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>

                    <tr>

                        <td>
                            <asp:Label ID="Label18" runat="server" Text="Contact No"></asp:Label>
                            <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" Style="width: 98%;" MaxLength="10"></asp:TextBox><cc1:FilteredTextBoxExtender
                                ID="FilteredTextBoxExtender1" runat="server" Enabled="True" FilterType="Numbers"
                                TargetControlID="txtPhone" ValidChars="0123456789">
                            </cc1:FilteredTextBoxExtender>
                        </td>

                        <td class="style20">
                            <asp:Label ID="Label4" runat="server" Text="Email ID"></asp:Label>
                            <asp:TextBox ID="txtemailid" CssClass="form-control" runat="server" MaxLength="60"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>


                    <tr>
                        <td>&nbsp;</td>
                        <td style="float: right;">
                            <asp:Button CssClass="btn btn-success" ID="Button3" runat="server" OnClick="Button3_Click"
                                ValidationGroup="editcust" Text="Update" />&nbsp;<asp:Button CssClass="btn btn-info" ID="Button4"
                                    runat="server" OnClick="Button4_Click" Text="Cancel" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1" Visible="false">
            <HeaderTemplate>
                Tag Cancelation
            </HeaderTemplate>
            <ContentTemplate>
                <table class="fullStyle" style="height: 630px;">
                    <tr>
                        <td align="left" style="height: 580px; vertical-align: top;">
                            <table class="tblStyle1">
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="lblTagCancellationMsg" ForeColor="Red" runat="server" Font-Bold="False"
                                            CssClass="clsValidator"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text="VID"></asp:Label>
                                    </td>
                                    <td valign="top">
                                        <asp:DropDownList ID="cmbRFIDTagCancle" runat="server" Width="150px" AppendDataBoundItems="True">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <asp:Label ID="Label3" runat="server" Text="Remarks"></asp:Label>
                                    </td>
                                    <td valign="top">
                                        <asp:DropDownList ID="CmbCancelationRemarks" runat="server" Width="150px" AppendDataBoundItems="True"
                                            AutoPostBack="True" Height="22px" OnSelectedIndexChanged="ddlCancelationRemarks_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:TextBox ID="txtCancelationRemark" runat="server" MaxLength="100" TextMode="MultiLine"
                                            Width="150px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style2">&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <asp:Button CssClass="button" ID="ImageButton1" runat="server" OnClick="btnUpdateRFIDCancel_Click"
                                            ValidationGroup="aa1" Text="Update" /><asp:Button CssClass="button" ID="ImageButton3"
                                                runat="server" OnClick="btncncel_Click" Text="Cancel" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>
</asp:Content>
