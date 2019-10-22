<%@ Page Title="" Language="C#" MasterPageFile="~/AdminModule.master" AutoEventWireup="true" CodeFile="JobCardOpen.aspx.cs" Inherits="JobCardOpen" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <style type="text/css">
        .pnlSearch
        {
        }
        .style1
        {
            width: 91px;
        }

        .style3
        {
            width: 131px;
            white-space: nowrap;
        }

        .style4
        {
            width: 96px;
        }

        .style5
        {
            width: 74px;
        }
    </style>
    <script type="text/javascript">
        function stopRKey(evt) {
            var evt = (evt) ? evt : ((event) ? event : null);
            var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
            if ((evt.keyCode == 13) && (node.type == "text")) { return false; }
        }

        document.onkeypress = stopRKey;
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:TabContainer ID="TabContainer1" ActiveTabIndex="0" runat="server" Width="100%"
        Height="650px" Style="margin-bottom: 10px" OnActiveTabChanged="TabContainer1_ActiveTabChanged"
        AutoPostBack="True">
        <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="Job Card Creation" Width="100%">
            <HeaderTemplate>
                Shop Floor Instuction</HeaderTemplate>
            <ContentTemplate>
                <table style="width: 100%;">
                    <tr>
                        <td align="center">
                            <table style="width: 100%;">
                                <tr style="background-color: Silver; font-family: Consolas, Georgia; font-size: medium"
                                    align="center">
                                    <td colspan="3">
                                        <asp:Label ID="Label1" runat="server" Text="Shop Floor Instuction"></asp:Label>
                                        <asp:Timer ID="Timer1" runat="server" Interval="30000" OnTick="Timer1_Tick">
                                        </asp:Timer>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" style="width: 350px">
                                        <table style="vertical-align: top; width: 100%;">
                                            <tr>
                                                <td colspan="3">
                                                    <asp:Label ID="message" runat="server" Font-Bold="False" CssClass="clsValidator"
                                                        ForeColor="Black" />
                                                </td>
                                            </tr>
                                            <tr class="clmDistriSmall">
                                                <td align="left" valign="top" style="width: 50%; white-space: nowrap;">
                                                    <asp:Label ID="Label3" runat="server" Text="Tag No"></asp:Label><asp:Label ID="Label36"
                                                        runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                </td>
                                                <td align="left" valign="top" style="width: 40%">
                                                    <asp:TextBox ID="txtTagNo" runat="server" MaxLength="5" ReadOnly="True" Width="100px"></asp:TextBox><td>
                                                    </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="width: 50%; white-space: nowrap;" valign="top">
                                                    <asp:Label ID="Label19" runat="server" Text="Vehicle No"></asp:Label><asp:Label ID="Label20"
                                                        runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                </td>
                                                <td align="left" valign="top" style="width: 50%">
                                                    <asp:TextBox ID="txtRegNo" runat="server" MaxLength="20" Width="100px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="btn_Searchcd" runat="server" ImageUrl="~/img/search_2.png" OnClick="btn_Searchcd_Click"
                                                        Text="Search Vehicle Details" ToolTip="Search Vehicle Details"></asp:ImageButton>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="width: 50%; white-space: nowrap;" valign="top">
                                                    &nbsp;<asp:Label ID="lbl" runat="server" Text="Model"></asp:Label>
                                                </td>
                                                <td align="left" style="width: 50%">
                                                    <asp:TextBox ID="txtModel" runat="server" Width="100px" ReadOnly="True" BackColor="Silver"></asp:TextBox>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="width: 50%; white-space: nowrap;" valign="top">
                                                    <asp:Label ID="Label23" runat="server" Text="Customer Name"></asp:Label>
                                                </td>
                                                <td align="left" style="width: 50%">
                                                    <asp:TextBox ID="lblCustName" runat="server" Width="100px" ReadOnly="True" BackColor="Silver"></asp:TextBox>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="width: 50%; white-space: nowrap;" valign="top">
                                                    <asp:Label ID="Label27" runat="server" Text="Customer No"></asp:Label>
                                                </td>
                                                <td align="left" style="width: 50%">
                                                    <asp:TextBox ID="txtPhoneNo" runat="server" Width="100px" ReadOnly="True" BackColor="Silver"></asp:TextBox>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="width: 50%; white-space: nowrap;" valign="top">
                                                    <asp:Label ID="Label28" runat="server" Text="Previous Visit Date"></asp:Label>
                                                </td>
                                                <td align="left" style="width: 50%">
                                                    <asp:TextBox ID="txtLastGateDate" runat="server" Width="100px" ReadOnly="True" BackColor="Silver"></asp:TextBox>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="width: 50%; white-space: nowrap;" valign="top">
                                                    <asp:Label ID="Label29" runat="server" Text="Previous Service Type"></asp:Label>
                                                </td>
                                                <td align="left" style="width: 50%">
                                                    <asp:TextBox ID="txtLastST" runat="server" Width="100px" ReadOnly="True" BackColor="Silver"></asp:TextBox>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td valign="top" style="width: 300px">
                                        <table style="vertical-align: top; width: 100%;">
                                            <tr class="clmDistriSmall">
                                                <td align="left" valign="top" style="width: 20%; white-space: nowrap;">
                                                    <asp:Label ID="Label25" runat="server" Text="Service Type"></asp:Label><asp:Label
                                                        ID="Label49" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                </td>
                                                <td align="left" style="width: 80%">
                                                    <asp:DropDownList ID="cmbWorkType" runat="server" Width="190px">
                                                        <asp:ListItem>Service Type</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="left" style="width: 80%">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" valign="top" style="width: 20%; white-space: nowrap;">
                                                    <asp:Label ID="Label26" runat="server" Text="PDT"></asp:Label><asp:Label ID="Label50"
                                                        runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                </td>
                                                <td align="left" style="width: 80%">
                                                    <asp:TextBox ID="txtDate" runat="server" AutoPostBack="True" MaxLength="10" OnTextChanged="txtDate_TextChanged"
                                                        ToolTip="Promised Date" Width="115px"></asp:TextBox><cc1:FilteredTextBoxExtender
                                                            ID="txtDate_FilteredTextBoxExtender" runat="server" Enabled="True" TargetControlID="txtDate"
                                                            ValidChars="0123456789/">
                                                        </cc1:FilteredTextBoxExtender>
                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtDate">
                                                    </cc1:CalendarExtender>
                                                    <asp:TextBox ID="cmbHH" runat="server" Width="60px"></asp:TextBox>
                                                    <cc1:MaskedEditExtender ID="cmbHH_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                        Enabled="True" ErrorTooltipEnabled="True" Mask="99:99" MaskType="Time" TargetControlID="cmbHH">
                                                    </cc1:MaskedEditExtender>
                                                    <cc1:MaskedEditValidator ID="mevStartTime" runat="server" ControlExtender="cmbHH_MaskedEditExtender"
                                                        ControlToValidate="cmbHH" Display="Dynamic" EmptyValueBlurredText="Time is required "
                                                        EmptyValueMessage="Time is required " ErrorMessage="mevStartTime" InvalidValueBlurredMessage="Invalid Time"
                                                        InvalidValueMessage="Time is invalid" ValidationGroup="MKE"></cc1:MaskedEditValidator><td
                                                            align="left" style="width: 80%">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtDate"
                                                                CssClass="clsValidator" ErrorMessage="Promosed Date is Mandatory" ValidationGroup="VAL">*</asp:RequiredFieldValidator>
                                                        </td>
                                            </tr>
                                            <tr>
                                                <td align="left" valign="top" style="width: 20%; white-space: nowrap;">
                                                </td>
                                                <td align="left" style="width: 80%; white-space: nowrap;" valign="top">
                                                    <table>
                                                        <tr>
                                                            <td style="white-space: nowrap;">
                                                                <asp:CheckBox ID="chkCustomerWaiting" runat="server" Text="Wait & Take"></asp:CheckBox>
                                                            </td>
                                                            <td style="white-space: nowrap;">
                                                                <asp:CheckBox ID="chkJDP" runat="server" Text="JDP"></asp:CheckBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="white-space: nowrap;">
                                                                <asp:CheckBox ID="chkWA" runat="server" AutoPostBack="True" OnCheckedChanged="chkWA_CheckedChanged"
                                                                    Text="Wheel Alignment"></asp:CheckBox>
                                                            </td>
                                                            <td style="white-space: nowrap;">
                                                                <asp:CheckBox ID="chkWash" runat="server" AutoPostBack="True" OnCheckedChanged="chkWash_CheckedChanged"
                                                                    Text="No Wash"></asp:CheckBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="white-space: nowrap;">
                                                                <asp:CheckBox ID="chkWashOnly" runat="server" AutoPostBack="True" OnCheckedChanged="chkWashOnly_CheckedChanged"
                                                                    Text="Wash Only "></asp:CheckBox>
                                                            </td>
                                                            <td style="white-space: nowrap;">
                                                                <asp:CheckBox ID="cbVas" runat="server" AutoPostBack="True" OnCheckedChanged="cbVas_CheckedChanged"
                                                                    Text="VAS"></asp:CheckBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td align="left" valign="top" style="width: 80%; white-space: nowrap;">
                                                    &nbsp;
                                                </td>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td align="left" valign="top">
                                                        &nbsp;<asp:Button ID="btnAdd" runat="server" CssClass="button" OnClick="btnAdd_Click"
                                                            Text="Open Job Card" Title="" ValidationGroup="VAL" Width="110px"></asp:Button><asp:Button
                                                                ID="btnclr" runat="server" CssClass="button" OnClick="btnclr_Click" Text="Clear">
                                                            </asp:Button>
                                                    </td>
                                                    <td align="left" valign="top">
                                                        &#160;
                                                    </td>
                                                </tr>
                                        </table>
                                    </td>
                                    <td valign="top" style="width: 100%">
                                        <table class="tblMain" style="font-size: small;" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <table class="commonFont">
                                                        <tr>
                                                            <td style="width: 90px;">
                                                                &nbsp;<asp:Label ID="Label17" runat="server" Text="Search Tag No"></asp:Label>
                                                            </td>
                                                            <td style="width: 70px;">
                                                                <asp:TextBox ID="txtSrchTag" runat="server" Width="100%"></asp:TextBox><cc1:FilteredTextBoxExtender
                                                                    ID="txtSrchTagExtender" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtSrchTag">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </td>
                                                            <td style="width: 20px; padding-left: 5px;">
                                                                <asp:ImageButton ID="btnSrchTag" runat="server" AlternateText="Search" ImageUrl="~/img/search_2.png"
                                                                    OnClick="btnSrchTag_Click" Width="16px" />
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="btnRefresh" runat="server" AlternateText="Refresh" Height="16px"
                                                                    ImageUrl="~/img/refresh.png" OnClick="btnRefresh_Click" Width="16px"></asp:ImageButton>
                                                            </td>
                                                            <td style="width: auto;">
                                                                <asp:Label ID="errSrchTag" runat="server" CssClass="clsValidator" ForeColor="Black"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-weight: normal;">
                                                    <asp:GridView ID="grdjobcard" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                        CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="grdjobcard_PageIndexChanging"
                                                        OnRowDataBound="grdjobcard_RowDataBound" OnSelectedIndexChanged="grdjobcard_SelectedIndexChanged"
                                                        PageSize="5" Style="text-align: center; width: 100%">
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <EditRowStyle BackColor="Silver" />
                                                        <Columns>
                                                            <asp:CommandField ButtonType="Image" SelectImageUrl="~/images/gridSelect.png" ShowSelectButton="True" />
                                                            <asp:BoundField DataField="Tag No" HeaderText="Tag No" />
                                                            <asp:BoundField DataField="RegNO" HeaderText="Vehicle No" />
                                                            <asp:BoundField DataField="Vehicle In" HeaderText="Vehicle In" />
                                                            <asp:BoundField DataField="Status" HeaderText="Status" />
                                                        </Columns>
                                                        <FooterStyle BackColor="Silver" Font-Bold="True" ForeColor="#333333" />
                                                        <HeaderStyle BackColor="Silver" Font-Bold="False" ForeColor="#333333" />
                                                        <PagerStyle BackColor="Silver" ForeColor="White" HorizontalAlign="Center" />
                                                        <RowStyle BackColor="WhiteSmoke" />
                                                        <SelectedRowStyle BackColor="WhiteSmoke" Font-Bold="True" ForeColor="#333333" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <table style="width: 100%;">
                                <tr style="background-color: Silver; font-family: Consolas, Georgia; font-size: medium"
                                    align="center">
                                    <td colspan="3">
                                        <asp:Label ID="Label2" runat="server" Text="Job Card Closing"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" style="width: 300px">
                                        <table style="font-size: small; width: 100%;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="ErrorNote" runat="server" CssClass="clsValidator" Font-Bold="False"></asp:Label><asp:ValidationSummary
                                                        ID="ValidationSummary2" runat="server" CssClass="clsValidator" Font-Names="Arial"
                                                        ValidationGroup="Val" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table cellpadding="0" cellspacing="0" class="fullStyle">
                                                        <tr>
                                                            <td class="style3">
                                                                <asp:Label ID="Label34" runat="server" Text="Vehicle No"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtRegNoJCC" runat="server" MaxLength="10" Width="160px"></asp:TextBox><asp:Label
                                                                    ID="lblSelSlNo" runat="server" Visible="False"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="txtRegNoJCC"
                                                                    CssClass="clsValidator" ErrorMessage="Registration No is Mandatory, Select a Record From List."
                                                                    Font-Bold="True" ValidationGroup="Val"> *</asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 10px">
                                                            <td valign="middle" class="style3">
                                                                <asp:Label ID="Label5" runat="server" Text="JCC Informed"></asp:Label>
                                                            </td>
                                                            <td valign="middle" align="left" style="height: 10px">
                                                                <asp:RadioButtonList ID="RadioCustomerInformed" runat="server" RepeatDirection="Horizontal"
                                                                    ToolTip="JCC  Informed?" Width="100px">
                                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                            <td style="height: 10px" valign="middle">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="RadioCustomerInformed"
                                                                    CssClass="clsValidator" Display="Dynamic" ErrorMessage="JCC informed field is Mandatory, select the proper option."
                                                                    Font-Bold="True" ValidationGroup="Val"> *</asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style1">
                                                                &nbsp;
                                                            </td>
                                                            <td align="left" valign="top">
                                                                <asp:Button ID="btnCloseJCC" runat="server" CssClass="button" OnClick="btnCloseJCC_Click"
                                                                    Text="Close Job Card" ValidationGroup="Val" Width="105px"></asp:Button><asp:Button
                                                                        ID="btnRefreshJCC" runat="server" CssClass="button" OnClick="btnRefreshJCC_Click"
                                                                        Text="Clear" Width="50px"></asp:Button>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td valign="top" style="width: 400px">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="grdjobcardclose" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                        CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="grdjobcardclose_PageIndexChanging"
                                                         OnSelectedIndexChanged="grdjobcardclose_SelectedIndexChanged"
                                                        PageSize="5" Style="text-align: center;" Width="100%">
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <Columns>
                                                            <asp:CommandField ButtonType="Image" SelectImageUrl="~/images/gridSelect.png" ShowSelectButton="True" />
                                                            <asp:BoundField DataField="Tag No" HeaderText="Tag No" />
                                                            <asp:BoundField DataField="RegNO" HeaderText="Vehicle No" />
                                                            <asp:BoundField DataField="Vehicle Ready" HeaderText="Vehicle Ready" />
                                                        </Columns>
                                                        <EditRowStyle BackColor="WhiteSmoke" />
                                                        <FooterStyle BackColor="Silver" Font-Bold="True" ForeColor="#333333" />
                                                        <HeaderStyle BackColor="Silver" Font-Bold="False" ForeColor="#333333" Height="10px" />
                                                        <PagerStyle BackColor="Silver" ForeColor="White" HorizontalAlign="Center" />
                                                        <RowStyle BackColor="WhiteSmoke" />
                                                        <SelectedRowStyle BackColor="WhiteSmoke" Font-Bold="True" ForeColor="#333333" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td valign="top" style="width: 700px">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="Edit Registration No">
            <HeaderTemplate>
                Edit Registration No</HeaderTemplate>
            <ContentTemplate>
                <contenttemplate><table class="tblStyle" style="width: 500px;"><tr><td colspan="2"><asp:Label ID="lblMessage1" runat="server" Font-Bold="False" CssClass="clsValidator"></asp:Label></td><asp:Label ID="lblenrno" runat="server" Visible="False"></asp:Label><asp:Label ID="Label6"
runat="server" Visible="False"></asp:Label></tr><tr><td style="width: 110px;"><asp:Label ID="lblevehno1" runat="server" Text="Vehicle No" /></td><td><asp:DropDownList AppendDataBoundItems="True"  ID="ddevehno" runat="server"
                                            Width="160px"></asp:DropDownList></td></tr><tr><td width="110px"><asp:Label ID="lblenewvhno" runat="server" Text="New Vehicle No" /></td><td><asp:TextBox ID="txtenewvhno" runat="server" Enabled="False" MaxLength="15"
Width="155px" /><asp:CheckBox
ID="chkevhno" runat="server" AutoPostBack="True" OnCheckedChanged="chkevhno_CheckedChanged" /></td></tr><tr><td width="120px"></td>&nbsp;<td>
<asp:Button CssClass="button" ID="btneupd" runat="server" OnClick="btneupd_Click"
 Text="Update" />&nbsp;<asp:Button CssClass="button" ID="btnecncl" runat="server"
OnClick="btnecncl_Click"  Text="Cancel" /></td></tr></table></contenttemplate>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="Edit Customer Details">
            <HeaderTemplate>
                Edit Customer Details</HeaderTemplate>
            <ContentTemplate>
                <table style="width: 294px;">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="Label11" runat="server" Font-Bold="False" CssClass="clsValidator"></asp:Label><asp:Label
                                ID="Label12" runat="server" Visible="False"></asp:Label><asp:Label ID="Label13" runat="server"
                                    Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style4">
                            <asp:Label ID="Label15" runat="server" Text="Vehicle No" Font-Names="Consolas,Georgia"></asp:Label>
                        </td>
                        <td valign="middle" style="white-space: nowrap">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="ddlVehicle" AutoPostBack="True" runat="server" AppendDataBoundItems="True"
                                            Width="145px" OnSelectedIndexChanged="ddlVehicle_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="padding-left: 2px;">
                                        <asp:ImageButton ID="Button1" runat="server" Height="18px" ImageUrl="~/img/refresh.png"
                                            OnClick="Refresh_Click" AlternateText="Refresh" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="style4" style="white-space: nowrap">
                            <asp:Label ID="Label16" runat="server" Text="Customer Name" Font-Names="Consolas,Georgia"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCustName" runat="server" MaxLength="25"></asp:TextBox><asp:RequiredFieldValidator
                                ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtCustName"
                                ErrorMessage="*" ValidationGroup="editcust"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style4" style="white-space: nowrap">
                            <asp:Label ID="Label18" runat="server" Text="Contact No" Font-Names="Consolas,Georgia"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPhone" runat="server" MaxLength="10"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="txtPhone_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterType="Numbers" TargetControlID="txtPhone" ValidChars="0123456789">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="style4" style="white-space: nowrap">
                            <asp:Label ID="Label22" runat="server" Text="Email ID" Font-Names="Consolas,Georgia"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtemailid" runat="server" MaxLength="60"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style4">
                            &nbsp;&nbsp;
                        </td>
                        <td>
                            <asp:Button CssClass="button" ID="Button3" runat="server" OnClick="Button3_Click"
                                ValidationGroup="editcust" Text="Update" />&nbsp;<asp:Button CssClass="button" ID="Button4"
                                    runat="server" OnClick="Button4_Click" Text="Cancel" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel4" runat="server" HeaderText="Tag Updation">
            <HeaderTemplate>
                Tag Updation</HeaderTemplate>
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
                            <asp:Label ID="lbvehl" runat="server" Text="Vehicle No"></asp:Label>
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
                            <asp:Label ID="lblcardno" runat="server" Text="Tag No" />
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtcardno" runat="server" ReadOnly="True" Width="146px" MaxLength="5" /><asp:RequiredFieldValidator
                                ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtcardno" ErrorMessage="*"
                                ValidationGroup="editcard" CssClass="clsValidator"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style7">
                            <asp:Label ID="lblnewcrdno" runat="server" Text="New Tag No" />
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
                        <td class="style5">
                        </td>
                        <td style="text-align: right;">
                            <asp:Button CssClass="button" ID="btnupdate" runat="server" OnClick="btnupdate_Click"
                                ValidationGroup="editcard" Text="Update" />&nbsp;<asp:Button CssClass="button" ID="btncncl"
                                    runat="server" OnClick="btncncl_Click" Text="Cancel" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel7" runat="server" HeaderText="Tag Cancellation">
            <HeaderTemplate>
                Tag Cancellation</HeaderTemplate>
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
                            <asp:Label ID="Label30" runat="server" Text="Tag No"></asp:Label>
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
                        <td class="style8">
                        </td>
                        <td>
                            <asp:TextBox ID="txtCancelationRemark" runat="server" MaxLength="100" TextMode="MultiLine"
                                Width="150px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style8">
                            &nbsp;&nbsp;
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

