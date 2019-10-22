<%@ Page Title="" Language="C#" MasterPageFile="~/AdminModule.master" AutoEventWireup="true"
    CodeFile="Cashier.aspx.cs" Inherits="Cashier" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 32%;
        }

        .money
        {
            /*text-align:right;*/
        }
        .style2
        {
            width: 40%;
            height: 26px;
        }
        .style3
        {
            height: 26px;
        }
    </style>
    <script type="text/javascript">

        function stopRKey(evt) {
            var evt = (evt) ? evt : ((event) ? event : null);
            var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
            if ((evt.keyCode == 8 || evt.KeyCode == 46)) { return false; }
        }

        document.onkeypress = stopRKey;
    </script>
    <script type="text/javascript" language="javascript">

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
    <div style="height: 550px; width: 100%; margin-bottom: 30px; font-family: Consolas, Georgia;
        font-size: small;">
        <table class="fullStyle" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <asp:RadioButtonList ID="rbLsit" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbLsit_SelectedIndexChanged"
                        AutoPostBack="True" Font-Names="Consolas, Georgia">
                        <asp:ListItem Value="0">New Billing</asp:ListItem>
                        <asp:ListItem Value="1">Pending List</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="View1" runat="server">
                            <table class="fullStyle" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 40%;" valign="top">
                                        <table style="height: 241px; width: 359px; vertical-align: middle; font-family: Consolas, Georgia">
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 40%;" valign="top">
                                                    <asp:Label ID="Label3" runat="server" Text="Tag No"></asp:Label>
                                                </td>
                                                <td style="width: 40%;" valign="top">
                                                    <asp:TextBox ID="txtTagNo" runat="server" MaxLength="20" ></asp:TextBox>
                                                </td>
                                                <td align="left" valign="top">
                                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/img/search_2.png" OnClick="ImageButton2_Click1" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 40%;" valign="top">
                                                    <asp:Label ID="Label2" runat="server" Text="Vehicle No"></asp:Label>
                                                </td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="txtRegNo" runat="server" MaxLength="20"></asp:TextBox>
                                                    <asp:Label ID="lblRefNo" runat="server" Visible="False"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 40%;" valign="top">
                                                    <asp:Label ID="Label1" runat="server" Text="Bill Amount"></asp:Label>
                                                </td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="txtBillAmount" runat="server" onkeypress="return isNumberKey(event)"
                                                        MaxLength="10" CssClass="money">0.00</asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 40%;" valign="top">
                                                    <asp:CheckBox ID="chkCash" runat="server" Text="Cash" OnCheckedChanged="chkCash_CheckedChanged"
                                                        AutoPostBack="True" />
                                                </td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="txtCash" runat="server" ReadOnly="True" MaxLength="10" onkeypress="return isNumberKey(event)"
                                                        CssClass="money">0.00</asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 40%;" valign="top">
                                                    <asp:CheckBox ID="chkCard" runat="server" Text="Card" OnCheckedChanged="chkCard_CheckedChanged"
                                                        AutoPostBack="True" />
                                                </td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="txtCard" runat="server" ReadOnly="True" MaxLength="10" onkeypress="return isNumberKey(event)"
                                                        CssClass="money">0.00</asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 40%;" valign="top">
                                                    <asp:CheckBox ID="chkCredit" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox1_CheckedChanged"
                                                        Text="Credit" />
                                                </td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="txtCredit" runat="server" ReadOnly="True" MaxLength="10" onkeypress="return isNumberKey(event)"
                                                        CssClass="money">0.00</asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 40%;" valign="top">
                                                    <asp:CheckBox ID="chkHome" runat="server" Text="Home Delivery" AutoPostBack="True"
                                                        OnCheckedChanged="CheckBox2_CheckedChanged" />
                                                </td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="txtHome" runat="server" ReadOnly="True" MaxLength="10" onkeypress="return isNumberKey(event)"
                                                        CssClass="money">0.00</asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" class="style2">
                                                    <asp:CheckBox ID="chkCustPend" runat="server" Text="Customer Pending" AutoPostBack="True"
                                                        OnCheckedChanged="CheckBox3_CheckedChanged" Width="140px" />
                                                </td>
                                                <td colspan="2" class="style3">
                                                    <asp:TextBox ID="txtCustPend" runat="server" ReadOnly="True" MaxLength="10" onkeypress="return isNumberKey(event)"
                                                        CssClass="money">0.00</asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 40%;" valign="top">
                                                    <asp:Label ID="Label7" runat="server" Text="Remarks"></asp:Label>
                                                </td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="txtRemarks" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 40%;" valign="top">
                                                </td>
                                                <td align="left" width="100%">
                                                    <asp:Button CssClass="button" ID="btnSave" runat="server" Text="Save" OnClick="ImageButton2_Click" />
                                                    <asp:Button CssClass="button" ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 60%; margin-left: 40px;" valign="top">
                                        <asp:GridView ID="grdCashier" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                            CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="grdCashier_PageIndexChanging"
                                            OnRowDataBound="grdCashier_RowDataBound" OnSelectedIndexChanged="grdCashier_SelectedIndexChanged"
                                            Style="text-align: center; width: 70%">
                                            <AlternatingRowStyle BackColor="White" />
                                            <EditRowStyle BackColor="WhiteSmoke" />
                                            <Columns>
                                                <asp:CommandField ButtonType="Image" SelectImageUrl="~/images/gridSelect.png" ShowSelectButton="True" />
                                                <asp:BoundField DataField="SlNo" HeaderText="SlNo" />
                                                <asp:BoundField DataField="RFID" HeaderText="Tag No" />
                                                <asp:BoundField DataField="VehicleNo" HeaderText="Vehicle No" ItemStyle-HorizontalAlign="Left" />
                                                <asp:BoundField DataField="ServiceAdvisor" HeaderText="Service Advisor" ItemStyle-HorizontalAlign="Left" />
                                                <asp:BoundField DataField="Vehicle In" HeaderText="Gate In" ItemStyle-HorizontalAlign="Left" />
                                                <asp:BoundField DataField="Delivered" HeaderText="Delivered" ShowHeader="False" />
                                            </Columns>
                                            <FooterStyle BackColor="Silver" Font-Bold="True" ForeColor="#333333" />
                                            <HeaderStyle BackColor="Silver" Font-Bold="True" ForeColor="#333333" HorizontalAlign="Left" />
                                            <PagerStyle BackColor="Silver" ForeColor="#333333" HorizontalAlign="Center" />
                                            <RowStyle BackColor="WhiteSmoke" />
                                            <SelectedRowStyle BackColor="WhiteSmoke" Font-Bold="True" ForeColor="#333333" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                        <asp:View ID="View2" runat="server">
                            <table class="fullStyle" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 40%;" valign="top">
                                        <table style="height: 90px; width: 359px;">
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Label ID="lblMsg1" runat="server"></asp:Label>
                                                    <asp:Label ID="lblRefNo1" runat="server" Visible="False"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" class="style1">
                                                    <asp:Label ID="Label4" runat="server" Text="Tag No"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPendingTagNo" runat="server" MaxLength="20" AutoPostBack="True"
                                                        OnTextChanged="txtPendingTagNo_TextChanged"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPendingTagNo"
                                                        ErrorMessage="RequiredFieldValidator" ValidationGroup="AB">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" class="style1">
                                                    <asp:Label ID="Label5" runat="server" Text="Vehicle No"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPendingVehNo" runat="server" OnTextChanged="txtPendingVehNo_TextChanged"
                                                        AutoPostBack="True" MaxLength="20"></asp:TextBox>
                                                    <asp:Label ID="lblSlNo" runat="server" Visible="False"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style1" valign="top">
                                                    <asp:Label ID="lbHead" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbVal" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" class="style1">
                                                    <asp:Label ID="Label6" runat="server" Text="Bill Amount" Width="140px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPendingBillAmt" runat="server" BackColor="White" Enabled="False"
                                                        MaxLength="10" onkeypress="return isNumberKey(event)" CssClass="money">0.00</asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" class="style1">
                                                    <asp:CheckBox ID="chkCashPay" runat="server" AutoPostBack="True" OnCheckedChanged="chkCashPay_CheckedChanged"
                                                        Text="Cash" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCashPend" runat="server" CssClass="money" MaxLength="10" onkeypress="return isNumberKey(event)"
                                                        ReadOnly="True">0.00</asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style1" valign="top">
                                                    <asp:CheckBox ID="chkCardPay" runat="server" AutoPostBack="True" OnCheckedChanged="chkCardPay_CheckedChanged"
                                                        Text="Card" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCardPend" runat="server" CssClass="money" MaxLength="10" onkeypress="return isNumberKey(event)"
                                                        ReadOnly="True">0.00</asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style1" valign="top">
                                                    <asp:CheckBox ID="chkCreditPay" runat="server" AutoPostBack="True" OnCheckedChanged="chkCreditPay_CheckedChanged"
                                                        Text="Credit" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCreditPend" runat="server" CssClass="money" MaxLength="10" onkeypress="return isNumberKey(event)"
                                                        ReadOnly="True">0.00</asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style1" valign="top">
                                                </td>
                                                <td align="left">
                                                    <asp:Button CssClass="button" ID="btnSave1" runat="server" Text="Save" OnClick="btnSave1_Click" />
                                                    <asp:Button CssClass="button" ID="btnClear1" runat="server" OnClick="btnClear1_Click"
                                                        Text="Clear" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 60%; margin-left: 40px;" valign="top">
                                        <asp:GridView ID="GrdPendingList" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                            CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="GrdPendingList_PageIndexChanging"
                                            OnRowDataBound="GrdPendingList_RowDataBound" OnSelectedIndexChanged="GrdPendingList_SelectedIndexChanged"
                                            Style="text-align: center; width: 85%">
                                            <AlternatingRowStyle BackColor="White" />
                                            <EditRowStyle BackColor="WhiteSmoke" />
                                            <Columns>
                                                <asp:CommandField ButtonType="Image" SelectImageUrl="~/images/gridSelect.png" ShowSelectButton="True" />
                                                <asp:BoundField DataField="ServiceId" HeaderText="ServiceId" />
                                                <asp:BoundField DataField="TagNo" HeaderText="Tag No" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="VehicleNo" HeaderText="Vehicle No" ItemStyle-HorizontalAlign="Left" />
                                                <asp:BoundField DataField="HomeDelivery" HeaderText="Home Delivery" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="PendingCustomer" HeaderText="Pending Customer" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="BillAmount" HeaderText="Bill Amount" />
                                                <asp:BoundField DataField="SlNo" HeaderText="SlNo" />
                                                <asp:BoundField DataField="ServiceAdvisor" HeaderText="Service Advisor" ItemStyle-HorizontalAlign="Left" />
                                            </Columns>
                                            <FooterStyle BackColor="Silver" Font-Bold="True" ForeColor="#333333" />
                                            <HeaderStyle BackColor="Silver" Font-Bold="True" ForeColor="#333333" HorizontalAlign="Left" />
                                            <PagerStyle BackColor="Silver" ForeColor="#333333" HorizontalAlign="Center" />
                                            <RowStyle BackColor="WhiteSmoke" />
                                            <SelectedRowStyle BackColor="WhiteSmoke" Font-Bold="True" ForeColor="#333333" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>