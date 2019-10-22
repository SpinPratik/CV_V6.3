    <%@ Page Title="" Language="C#" MasterPageFile="~/AdminModule_New.master" AutoEventWireup="true" CodeFile="NewJobCardCreation.aspx.cs" Inherits="NewJobCardCreation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
 <%--   <link href="build/jquery.datetimepicker.css" rel="stylesheet" />
    <link href="CSS/Style.css" rel="stylesheet" />--%>
      <link href="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484287591/css/jquery.datetimepicker.css" rel="stylesheet" />
    <link href="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484285348/css/Style.css" rel="stylesheet" />
    <style type="text/css">
        .pnlSearch {
        }

        .style1 {
            width: 91px;
        }

        .style3 {
            width: 131px;
            white-space: nowrap;
        }

        .style4 {
            width: 96px;
        }

        .style5 {
            width: 74px;
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

       
        .mydatagrid span /** FOR THE PAGING ICONS CURRENT PAGE INDICATOR **/ {
            background-color: #c9c9c9;
            color: #000;
            padding: 5px 5px 5px 5px;
        }

        .pager {
            background-color: #646464;
            font-family: Arial;
            color: black !important;
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

        .mydatagrid span {
            background-color: #A1DCF2;
            color: #000;
            border: 1px solid #3AC0F2;
        }

        #header {
            background-color: white !important;
            padding: 2em !important;
        }

        .btn {
            text-transform: uppercase;
        }

        label {
            /*font-weight: unset;*/
            text-transform: uppercase;
        }

        span {
            text-transform: uppercase;
            font-size: 12px;
        }

        .form-control {
            width: unset;
        }

        .wrapper {
            display: inline-block;
            position: relative;
        }

        .button1 {
            /*background-color:black;*/
            color: white;
            border: 0;
            margin-left: 30px;
        }

        .button1 {
            position: absolute;
            right: 9px;
            top: 7px;
        }

        body {
            overflow-x: scroll;
        }
    </style>
    <script type="text/javascript">
        function stopRKey(evt) {
            var evt = (evt) ? evt : ((event) ? event : null);
            var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
            if ((evt.keyCode == 13) && (node.type == "text")) { return false; }
        }

        document.onkeypress = stopRKey;

        function specialChars() {
            var nbr;
            nbr = event.keyCode;
            if ((nbr >= 48 && nbr <= 57)) {
                return true;
            }
            else {
                return false;
            }
        }

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <asp:Button CssClass="btn btn-info" Style="float: right; border: 1px solid #286090; color: #286090; font-weight: 800; font-size: 12px; font-family: tahoma, arial, Verdana; background-color: white;" Text="service advisor display" ID="btn_displaylink" OnClick="btn_displaylink_Click" runat="server" />
    </div>
    <a href="TagAllotment.aspx" title="Back to FO">
        <div style="position:absolute;top:15px;left:10px;">
        <img src="img/leftarrow.png" alt="Back to FO" height="32" width="32"/>
    </div>
    </a>
    <cc1:TabContainer CssClass="MyTabStyle" ID="TabContainer1" ActiveTabIndex="0" runat="server" Width="100%"
        Style="margin-bottom: 10px" OnActiveTabChanged="TabContainer1_ActiveTabChanged"
        AutoPostBack="True">
        <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="Job Card Creation" Width="100%">
            <HeaderTemplate>
                Job Card
            </HeaderTemplate>
            <ContentTemplate>
                <table style="width: 100%;">
                    <tr>
                        <td align="center">
                            <table style="width: 100%;">
                                <tr style="font-family: Helvetica Neue, Helvetica, Arial, sans-serif; font-size: medium"
                                    align="center">
                                    <td colspan="2" style="padding: 10px; border-bottom: 6px solid #5f9eee;">
                                        <asp:Label ID="Label1" runat="server" Text=" Job Card" Style="font-weight: 700;"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" style="">
                                        <table style="vertical-align: top; width: 100%;">
                                            <tr>
                                                <td colspan="3">
                                                    <asp:Label ID="message" runat="server" Font-Bold="False" CssClass="clsValidator"
                                                        ForeColor="Black" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr class="clmDistriSmall">

                                                <td align="left" valign="top">
                                                    <asp:Label ID="Label3" runat="server" Text="VID&nbsp;&nbsp;" style="margin-left:unset;"></asp:Label>

                                                    <asp:TextBox ID="txtTagNo" runat="server" CssClass="form-control" MaxLength="5" ReadOnly="True"></asp:TextBox>

                                                </td>
                                                <td>&nbsp;</td>
                                                <td align="left" valign="top">
                                                    <asp:Label ID="Label19" runat="server" Text="VRN/VIN"></asp:Label><span style="color:red">&nbsp;*</span>
                                                    <div class="wrapper">
                                                        <asp:TextBox ID="txtRegNo" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                                        <asp:ImageButton CssClass="button1" ID="ImageButton1" runat="server" ImageUrl="https://res.cloudinary.com/deekyp5bi/image/upload/v1484285976/search_2_ll3ccm.png" OnClick="btn_Searchcd_Click"
                                                            ToolTip="Search Vehicle Details"></asp:ImageButton>
                                                    </div>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;
                                                </td>
                                            </tr>
                                            <tr>

                                                <td align="left">&nbsp;<asp:Label ID="lbl" runat="server" Text="Model&nbsp;&nbsp;"></asp:Label>
                                                    <asp:TextBox ID="txtModel" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td align="left">
                                                    <asp:Label ID="Label23" runat="server" Text="Customer Name&nbsp;&nbsp;"></asp:Label>
                                                    <asp:TextBox ID="lblCustName" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr>

                                                <td>&nbsp;
                                                </td>
                                            </tr>
                                            <tr>

                                                <td align="left">
                                                    <asp:Label ID="Label27" runat="server" Text="Customer No&nbsp;&nbsp;"></asp:Label>
                                                    <asp:TextBox ID="txtPhoneNo" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td align="left">
                                                    <asp:Label ID="Label28" runat="server" Text="Previous Visit Date&nbsp;&nbsp;" Style="white-space: nowrap;"></asp:Label>

                                                    <asp:TextBox ID="txtLastGateDate" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr>

                                                <td>&nbsp;
                                                </td>
                                            </tr>
                                            <tr>

                                                <td align="left">
                                                    <asp:Label ID="Label29" runat="server" Text="Previous Service Type" Style="white-space: nowrap;"></asp:Label>
                                                    <asp:TextBox ID="txtLastST" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td align="left">
                                                    <asp:Label ID="Label25" runat="server" Text="Service Type"></asp:Label><span style="color:red">&nbsp;*</span>

                                                    <asp:DropDownList ID="cmbWorkType" runat="server" Width="100%" CssClass="form-control">
                                                        <asp:ListItem>Service Type</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td></td>
                                            </tr>

                                            <tr class="clmDistriSmall">

                                                <td>&nbsp;
                                                </td>
                                            </tr>

                                            <tr class="clmDistriSmall">

                                                <td align="left" style="">
                                                    <asp:Label ID="Label7" runat="server" Text="Current KMS&nbsp;&nbsp;"></asp:Label><span style="color:red">*</span>

                                                    <asp:TextBox ID="txtKMS" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)" MaxLength="8"></asp:TextBox>

                                                </td>
                                                <td>&nbsp;</td>

                                                <td align="left">
                                                    <asp:Label ID="Label26" runat="server" Text="PDT"></asp:Label><span style="color:red">&nbsp;*</span>

                                                    <asp:TextBox ID="txtDate" runat="server" CssClass="datetimepicker form-control" MaxLength="10"
                                                        ToolTip="Promised Date" required="required"></asp:TextBox>
                                                    <script type="text/javascript" src="build/jquery.js"></script>
                                                    <script type="text/javascript" src="build/jquery.datetimepicker.full.js"></script>
                                                    <script type="text/javascript">
                                                        var dt = new Date();
                                                        $('.datetimepicker').datetimepicker({
                                                            dayOfWeekStart: 1,
                                                            minDate: 0,
                                                            minDateTime: dt,
                                                            scrollTime: true,
                                                            format: 'Y/m/d H:i',
                                                            step: 5,
                                                            datetimepicker: true,
                                                        });
                                                        $('.datetimepicker').datetimepicker({ step: 10 });
                                                    </script>
                                                </td>
                                                <td align="left" style="width: 80%">&nbsp;<asp:Label ID="lblSchdule" runat="server" Text="Next Schdule"></asp:Label>
&nbsp;<asp:TextBox ID="txtSchdule" runat="server" CssClass="datetimepicker form-control" MaxLength="10" required="required" ToolTip="Next Schdule Date"></asp:TextBox>
                                                </td>

                                                <td>&nbsp;</td>

                                                <td align="left">
                                                    <span style="color:red">&nbsp;*</span>

                                                    <script type="text/javascript" src="build/jquery.js"></script>
                                                    <script type="text/javascript" src="build/jquery.datetimepicker.full.js"></script>
                                                    <script type="text/javascript">
                                                        var dt = new Date();
                                                        $('.datetimepicker').datetimepicker({
                                                            dayOfWeekStart: 1,
                                                            minDate: 0,
                                                            minDateTime: dt,
                                                            scrollTime: true,
                                                            format: 'Y/m/d',
                                                            step: 5,
                                                            datetimepicker: true,
                                                        });
                                                        $('.datetimepicker').datetimepicker({ step: 10 });
                                                    </script>
                                                </td>
                                                <td align="left" style="width: 80%">&nbsp;
                                                </td>

                                            </tr>

                                            <tr>
                                                <td align="left" style="white-space: nowrap;">
                                                    <asp:CheckBox ID="Chkzipagrr" runat="server" Text="&nbsp;Zippy Aggregate&nbsp;&nbsp;" />
                                                </td>
                                                <td>&nbsp;</td>
                                                  <td align="left" style="white-space: nowrap;">
                                                      <asp:CheckBox ID="Chknonzipagrr" runat="server" Text="&nbsp;Zippy Non Aggregate&nbsp;&nbsp;" />
                                                </td>
                                                <td>&nbsp;</td>
                                                  <td align="left" style="white-space: nowrap;">
                                                      &nbsp;</td>
                                                <td>
                                                    <asp:CheckBox ID="chkCustomerWaiting" runat="server" Text="&nbsp;Wait &amp; Take&nbsp;&nbsp;" Visible="False" />
                                                </td>
                                                <td style="white-space: nowrap;">
                                                    &nbsp;</td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="white-space: nowrap;">
                                                    <asp:CheckBox ID="chkWA" runat="server" AutoPostBack="True" OnCheckedChanged="chkWA_CheckedChanged"
                                                        Text="&nbsp;Wheel Alignment&nbsp;&nbsp;"></asp:CheckBox>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td style="white-space: nowrap;">
                                                    <asp:CheckBox ID="chkWash" runat="server" AutoPostBack="True" OnCheckedChanged="chkWash_CheckedChanged"
                                                        Text="&nbsp;No Wash"></asp:CheckBox>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkJDP" runat="server" Text="&nbsp;Premium Customers" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="white-space: nowrap;">
                                                    <asp:CheckBox ID="chkWashOnly" runat="server" AutoPostBack="True" OnCheckedChanged="chkWashOnly_CheckedChanged"
                                                        Text="&nbsp;Wash Only &nbsp;&nbsp;"></asp:CheckBox>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td style="white-space: nowrap;">
                                                    <asp:CheckBox ID="ChkRT" runat="server" AutoPostBack="True" OnCheckedChanged="ChkRT_CheckedChanged"
                                                        Text="Road Test"></asp:CheckBox>
                                                </td>
                                                <td></td>

                                                <td>&nbsp;</td>
                                                <td style="white-space: nowrap;">
                                                    <asp:CheckBox ID="cbVas" runat="server" AutoPostBack="True" OnCheckedChanged="cbVas_CheckedChanged"
                                                        Text="VAS" Visible="False"></asp:CheckBox>
                                                </td>
                                                <td></td>

                                                </td>
                                                
                                            </tr>
                                            <tr>

                                                <td colspan="3" >&nbsp;<asp:Button ID="btnAdd" runat="server" CssClass="btn btn-success" OnClick="btnAdd_Click"
                                                    Text="Open JobCard" Title="" ValidationGroup="VAL" style="width:192px"></asp:Button>
                                                    &nbsp;<asp:Button
                                                        ID="btnclr" runat="server" CssClass="btn btn-info" OnClick="btnclr_Click" Text="Clear"></asp:Button>
                                                </td>
                                                <td align="left" valign="top">&#160;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>

                                    <td valign="top" style="width: 100%" aligh="lfet" >
                                        <table class="tblMain" style="font-size: small;margin-left:150px;" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td aligh="lfet" >
                                                    <table class="commonFont">
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                             <td style="width: auto;">
                                                                <asp:Label ID="errSrchTag" runat="server" CssClass="clsValidator" ForeColor="Black"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr><td>&nbsp;</td></tr>
                                                        <tr>
                                                            
                                                            <td style="width: 70px; margin:unset !important;" aligh="lfet" >
                                                                <asp:Label ID="Label17" runat="server" Text="Search VID" style="text-align: left;"></asp:Label>
                                                                <asp:TextBox ID="txtSrchTag" Style="padding-right: 30px;" CssClass="form-control" runat="server"></asp:TextBox><cc1:FilteredTextBoxExtender
                                                                    ID="txtSrchTagExtender" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtSrchTag">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </td>
                                                            <td style="width: 20px; padding-left: 5px;">
                                                                <asp:ImageButton ID="ImageButton2" CssClass="btn btn-default" Style="margin-left: -48px; margin-top: 18px; position: relative;" runat="server" AlternateText="Search" ImageUrl="https://res.cloudinary.com/deekyp5bi/image/upload/v1484285976/search_2_ll3ccm.png"
                                                                    OnClick="btnSrchTag_Click"  />
                                                            </td>
                                                            <td>
                                                                 <asp:ImageButton ID="ImageButton3" runat="server" AlternateText="Refresh" Height="16px"
                                                                    ImageUrl="https://res.cloudinary.com/deekyp5bi/image/upload/v1484284612/refresh_vahbyd.png" OnClick="btnRefresh_Click" style="margin-top: 22px; margin-left: -12px;" ></asp:ImageButton>
                                                            </td>
                                                           
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-weight: normal;">
                                                    <asp:GridView ID="grdjobcard" runat="server" CssClass="mydatagrid" AllowPaging="True" AutoGenerateColumns="False"
                                                        CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="grdjobcard_PageIndexChanging"
                                                        OnRowDataBound="grdjobcard_RowDataBound" OnSelectedIndexChanged="grdjobcard_SelectedIndexChanged"
                                                        PageSize="5" Style="text-align: center; width: 100%">
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <EditRowStyle BackColor="Silver" />
                                                        <Columns>
                                                             <asp:CommandField ButtonType="Image" SelectImageUrl="https://res.cloudinary.com/deekyp5bi/image/upload/v1484224665/jcr/gridSelect.png" ShowSelectButton="True" />
                                                            <asp:BoundField DataField="Tag No" HeaderText="VID" />
                                                            <asp:BoundField DataField="RegNO" HeaderText="VRN/VIN" />
                                                            <asp:BoundField DataField="Vehicle In" HeaderText="Vehicle In" />
                                                            <asp:BoundField DataField="Status" HeaderText="Status" />
                                                        </Columns>
                                                        <FooterStyle BackColor="Silver" Font-Bold="True" ForeColor="#333333" />
                                                        <HeaderStyle BackColor="Silver" Font-Bold="False" ForeColor="#333333" />
                                                        <PagerStyle BackColor="Silver" ForeColor="Black" HorizontalAlign="Center" />
                                                        <RowStyle BackColor="WhiteSmoke" HorizontalAlign="Left" />
                                                        <SelectedRowStyle BackColor="WhiteSmoke" Font-Bold="True" ForeColor="#333333" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="Edit VRN/VIN" >
            <HeaderTemplate>
                Edit VRN/VIN
            </HeaderTemplate>
            <ContentTemplate>
                <contenttemplate><table class="tblStyle" style="width: 500px;">
                    <tr><td><asp:Label ID="lblMessage1" runat="server" Font-Bold="False" CssClass="clsValidator"></asp:Label></td><asp:Label ID="lblenrno" runat="server" Visible="False"></asp:Label><asp:Label ID="Label6"
runat="server" Visible="False"></asp:Label></tr>
                    <tr><td>&nbsp;</td></tr>
                    <tr><td><asp:Label ID="lblevehno1" runat="server" Text="VRN/VIN" />
                        
                        <asp:DropDownList Width="100%" CssClass="form-control" AppendDataBoundItems="True"  ID="ddevehno" runat="server">
                        </asp:DropDownList></td></tr><tr><td>&nbsp;</td></tr><tr><td><asp:Label ID="lblenewvhno" runat="server" Text="New VRN/VIN" />
                             <asp:CheckBox ID="chkevhno" runat="server" AutoPostBack="True" OnCheckedChanged="chkevhno_CheckedChanged" />
                     
                                  <asp:TextBox ID="txtenewvhno" Width="100%" CssClass="form-control" runat="server" style="text-transform:uppercase" Enabled="False" MaxLength="20"/>
                           </td></tr><tr><td>&nbsp;</td></tr> 
                    <tr>
                        <td align="right">
                            <asp:Button ID="btneupd" runat="server" CssClass="btn btn-success" OnClick="btneupd_Click" Text="Update" />
                            &nbsp;<asp:Button ID="btnecncl" runat="server" CssClass="btn btn-info" OnClick="btnecncl_Click" Text="Cancel" />
                        </td>
                    </tr>
                    </table></contenttemplate>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="Edit Customer Details" >
            <HeaderTemplate>
                Edit Customer Details
            </HeaderTemplate>
            <ContentTemplate>
                <table style="width: 500px">
                    <tr>
                        <td>
                            <asp:Label ID="Label11" runat="server" Font-Bold="False" Width="100%" CssClass="clsValidator"></asp:Label><asp:Label
                                ID="Label12" runat="server" Visible="False"></asp:Label><asp:Label ID="Label13" runat="server"
                                    Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label15" runat="server" Text="VRN/VIN"></asp:Label>
                           
                            <asp:DropDownList ID="ddlVehicle" Width="100%" CssClass="form-control" AutoPostBack="True" runat="server" AppendDataBoundItems="True"
                                OnSelectedIndexChanged="ddlVehicle_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp; 
                           <%--  <asp:ImageButton ID="Button1" runat="server" Height="16px" ImageUrl="~/img/refresh.png"
                                OnClick="Refresh_Click" AlternateText="Refresh" Style=" margin-top: 23px; margin-right: -17px;"/>--%>
                                <asp:ImageButton ID="ImageButton4" runat="server" Height="16px" ImageUrl="https://res.cloudinary.com/deekyp5bi/image/upload/v1484284612/refresh_vahbyd.png"
                                OnClick="Refresh_Click" AlternateText="Refresh" Style=" margin-top: 23px; margin-right: -17px;"/>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="white-space: nowrap" >
                            <asp:Label ID="Label16" runat="server" Text="Customer Name"></asp:Label>
                            <asp:TextBox ID="txtCustName" Width="100%" CssClass="form-control" runat="server" MaxLength="25"></asp:TextBox><%--<asp:RequiredFieldValidator
                                ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtCustName"
                                ErrorMessage="* Enter Customer Name" ValidationGroup="editcust"></asp:RequiredFieldValidator>--%>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>

                        <td>
                            <asp:Label ID="Label18" runat="server" Text="Contact No"></asp:Label>
                            <asp:TextBox ID="txtPhone" Width="100%" CssClass="form-control" runat="server" MaxLength="10"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="txtPhone_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterType="Numbers" TargetControlID="txtPhone" ValidChars="0123456789">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                         <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>

                        <td>
                            <asp:Label ID="Label22" runat="server" Text="Email ID"></asp:Label>
                            <asp:TextBox ID="txtemailid" Width="100%" CssClass="form-control" runat="server" MaxLength="60"></asp:TextBox>
                        </td>
                         <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>

                        <td align="right">
                            <asp:Button CssClass="btn btn-success" ID="Button3" runat="server" OnClick="Button3_Click"
                                ValidationGroup="editcust" Text="Update" />&nbsp;<asp:Button CssClass="btn btn-info" ID="Button4"
                                    runat="server" OnClick="Button4_Click" Text="Cancel" />
                        </td>
                         <td>&nbsp;</td>
                    </tr>
                </table>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel4" runat="server" HeaderText="Tag Updation" Visible="false">
            <HeaderTemplate>
                Tag Updation
            </HeaderTemplate>
            <ContentTemplate>
                <table class="tblStyle">
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="Label4" runat="server" Font-Bold="False" CssClass="clsValidator"></asp:Label><asp:Label
                                ID="lblref" runat="server" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" class="style7">
                            <asp:Label ID="lbvehl" runat="server" Text="VRN/VIN"></asp:Label>
                        </td>
                        <td valign="top">
                            <asp:DropDownList ID="drpvehicle" runat="server" AppendDataBoundItems="True" OnSelectedIndexChanged="drpvehicle_SelectedIndexChanged"
                                ValidationGroup="a" Width="150px" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                        <td valign="top">
                            <asp:ImageButton ID="btnSearchCardUpdation" runat="server" Height="18px" ImageUrl="~/img/refresh.png"
                                OnClick="btnSearchCardUpdation_Click" AlternateText="Refresh" />
                        </td>
                    </tr>
                    <tr>
                        <td class="style7">
                            <asp:Label ID="lblcardno" runat="server" Text="VID" />
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtcardno" runat="server" ReadOnly="True" Width="146px" MaxLength="5" /><asp:RequiredFieldValidator
                                ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtcardno" ErrorMessage="*"
                                ValidationGroup="editcard" CssClass="clsValidator"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style7">
                            <asp:Label ID="lblnewcrdno" runat="server" Text="New VID" />
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtnewcrdno" runat="server" Width="147px" MaxLength="5" /><asp:RequiredFieldValidator
                                ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtnewcrdno" ErrorMessage="*"
                                ValidationGroup="editcard" CssClass="clsValidator"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td class="style5"></td>
                        <td style="text-align: right;">
                            <asp:Button CssClass="button" ID="btnupdate" runat="server" OnClick="btnupdate_Click"
                                ValidationGroup="editcard" Text="Update" />&nbsp;<asp:Button CssClass="button" ID="btncncl"
                                    runat="server" OnClick="btncncl_Click" Text="Cancel" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel7" runat="server" HeaderText="Tag Cancellation" Visible="false">
            <HeaderTemplate>
                Tag Cancellation
            </HeaderTemplate>
            <ContentTemplate>
                <table class="tblStyle">
                    <tr>
                        <td class="style1" colspan="2">
                            <asp:Label ID="lblTagCancellationMsg" runat="server" Font-Bold="False" CssClass="clsValidator"
                                ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style8">
                            <asp:Label ID="Label30" runat="server" Text="VID"></asp:Label>
                        </td>
                        <td valign="top" class="style4">
                            <asp:DropDownList ID="ddlRFID" runat="server" OnSelectedIndexChanged="drpvehiclno_SelectedIndexChanged"
                                ValidationGroup="a" Width="150px" AppendDataBoundItems="True">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" class="style8">
                            <asp:Label ID="Label31" runat="server" Text="Remarks"></asp:Label>
                        </td>
                        <td valign="top" class="style5">
                            <asp:DropDownList ID="ddlCancelationRemarks" runat="server" Width="150px" AutoPostBack="True"
                                Height="22px" OnSelectedIndexChanged="ddlCancelationRemarks_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style8"></td>
                        <td>
                            <asp:TextBox ID="txtCancelationRemark" runat="server" MaxLength="100" TextMode="MultiLine"
                                Width="150px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style8">&nbsp;&nbsp;
                        </td>
                        <td style="text-align: right;">
                            <asp:Button CssClass="button" ID="btnUpdateRFIDCancel" runat="server" OnClick="btnUpdateRFIDCancel_Click"
                                ValidationGroup="aa1" Text="Update" />&nbsp;<asp:Button CssClass="button" ID="btnCancelRFIDCancel"
                                    runat="server" OnClick="btncncel_Click" Text="Cancel" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>
</asp:Content>

