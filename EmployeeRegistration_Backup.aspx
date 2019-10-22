<%@ Page Language="C#" MasterPageFile="~/AdminModule1.master" AutoEventWireup="true"
    CodeFile="EmployeeRegistration_Backup.aspx.cs" Inherits="EmployeeRegistration" Title="EmployeeRegistration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .thisstyle
        {
            width: 112px;
        }
        .LeftGridPad
        {
            padding-left: 40px;
        }
        .style1
        {
            width: 112px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="height: 607px; margin-bottom: 30px; vertical-align: middle;">
        <table class="fullStyle">
            <tr>
                <td>
                </td>
            </tr>
            <tr class="clmDistriSmall">
                <td valign="top">
                    <asp:RadioButtonList ID="rdEmployeeRegistration" runat="server" AutoPostBack="True"
                        OnSelectedIndexChanged="rdEmployeeRegistration_SelectedIndexChanged" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="0">Tag Assignment</asp:ListItem>
                        <asp:ListItem Value="1">Tag Updation</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="View1" runat="server">
                            <table style="width: 800px; height: auto;">
                                <tr>
                                    <td valign="top">
                                        <table style="width: 400px; height: auto;">
                                            <tr>
                                                <td colspan="2" class="tblHead1">
                                                    Tag Assignment
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Label ID="lbMsg" runat="server" CssClass="clsValidator" ForeColor="Red"></asp:Label>
                                                    <asp:Label ID="lblempid" runat="server" Visible="False" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style1">
                                                    Tag No
                                                </td>
                                                <td>
                                                    <table cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <asp:DropDownList ID="cmbCardNo" runat="server" Width="153px">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="cmbCardNo"
                                                                    CssClass="clsValidator" ErrorMessage="*" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="btnRefresh" runat="server" AlternateText="Refresh" Height="20px"
                                                                    ImageUrl="~/images/refresh.png" OnClick="btnRefresh_Click"></asp:ImageButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style1">
                                                    Employee Name
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtEName" runat="server" MaxLength="49" Width="150px"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEName" CssClass="clsValidator"
                                                        ErrorMessage="*" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style1" valign="top">
                                                    Employee Type
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="cmbEType" runat="server" Width="153px">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="cmbEType"
                                                        CssClass="clsValidator" ErrorMessage="*" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style1" valign="top">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkBodyShop" runat="server" Text="Bodyshop" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style1">
                                                </td>
                                                <td>
                                                    <asp:Button CssClass="button" ID="btnAssign" runat="server" Text="Assign" OnClick="btnAssign_Click"
                                                        ValidationGroup="a" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td valign="top">
                                        <table style="width: 400px; height: auto;">
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="grdEmployeeList" runat="server" AllowPaging="True" AllowSorting="True"
                                                        AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                                                        Width="400px" OnPageIndexChanging="grdEmployeeList_PageIndexChanging" OnRowDataBound="grdEmployeeList_RowDataBound">
                                                        <FooterStyle BackColor="#0099FF" Font-Bold="True" ForeColor="White" />
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <Columns>
                                                            <asp:BoundField DataField="EmpId" HeaderText="EmpId" />
                                                            <asp:BoundField DataField="Emp Name" HeaderText="Emp Name" />
                                                            <asp:BoundField DataField="Emp Type" HeaderText="Emp Type" />
                                                            <asp:BoundField DataField="Tag No" HeaderText="Tag No"></asp:BoundField>
                                                        </Columns>
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <HeaderStyle BackColor="Silver" Font-Bold="False" ForeColor="#333333" HorizontalAlign="Left"
                                                            BorderStyle="None" Font-Italic="False" />
                                                        <PagerStyle BackColor="Silver" ForeColor="#333333" HorizontalAlign="Center" />
                                                        <RowStyle BackColor="WhiteSmoke" />
                                                        <SelectedRowStyle BackColor="WhiteSmoke" Font-Bold="True" ForeColor="#333333" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                        <asp:View ID="View2" runat="server">
                            <table style="width: 800px; height: auto">
                                <tr>
                                    <td valign="top">
                                        <table style="width: 400px; height: auto;">
                                            <tr valign="top">
                                                <td colspan="2" class="tblHead1">
                                                    Tag Updation
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Label ID="lbMsg1" runat="server" CssClass="clsValidator" ForeColor="Red"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="thisstyle">
                                                    Existing Tag No
                                                </td>
                                                <td>
                                                    <table cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtExistingTagNo" runat="server" Width="150px"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" CssClass="clsValidator"
                                                                    ErrorMessage="*" ValidationGroup="b" ControlToValidate="txtExistingTagNo"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="btnGetDetails" runat="server" ImageUrl="~/img/search_2.png"
                                                                    OnClick="btnGetDetails_Click" AlternateText="Search" />
                                                                <asp:Label ID="lblSrcEmpID" runat="server" Visible="False"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="thisstyle">
                                                    Employee Name
                                                </td>
                                                <td>
                                                    <table cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtEmployeeName" runat="server" MaxLength="49" Width="150px"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtEmployeeName"
                                                                    CssClass="clsValidator" ErrorMessage="*" ValidationGroup="b"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="middle" class="thisstyle">
                                                    Employee Type
                                                </td>
                                                <td>
                                                    <table cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <asp:DropDownList ID="ddlEmployeeType" runat="server" Width="153px" AutoPostBack="True"
                                                                   >
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlEmployeeType"
                                                                    CssClass="clsValidator" ErrorMessage="*" ValidationGroup="b"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="thisstyle" valign="top">
                                                    New Tag No
                                                </td>
                                                <td valign="top">
                                                    <table cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <asp:DropDownList ID="ddlNewCardNo" runat="server" Width="153px">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlNewCardNo"
                                                                    CssClass="clsValidator" Enabled="False" ErrorMessage="*" ValidationGroup="b"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="chkChangeCardNo" runat="server" AutoPostBack="True" OnCheckedChanged="chkChangeCardNo_CheckedChanged"
                                                                    Text="Change" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="thisstyle" valign="top">
                                                </td>
                                                <td valign="top">
                                                    <asp:CheckBox ID="chkIsBodyShop" runat="server" Text="Bodyshop" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="thisstyle">
                                                    &#160;&#160;&#160;
                                                </td>
                                                <td>
                                                    <asp:Button CssClass="button" ID="btnUpdate" runat="server" OnClick="btnUpdate_Click"
                                                        ValidationGroup="b" Text="Update" />
                                                    <asp:Button CssClass="button" ID="btnUnAssign" runat="server" OnClick="btnUnAssign_Click"
                                                        ValidationGroup="b" Text="UnAssign" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td valign="top">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                                                        AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                                                        Width="400px" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound"
                                                        >
                                                        <FooterStyle BackColor="#0099FF" Font-Bold="True" ForeColor="White" />
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <Columns>
                                                            <asp:BoundField DataField="EmpId" HeaderText="EmpId" />
                                                            <asp:BoundField DataField="Emp Name" HeaderText="Emp Name" />
                                                            <asp:BoundField DataField="Emp Type" HeaderText="Emp Type" />
                                                            <asp:BoundField DataField="Tag No" HeaderText="Tag No"></asp:BoundField>
                                                        </Columns>
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <HeaderStyle BackColor="Silver" Font-Bold="False" ForeColor="#333333" HorizontalAlign="Left" />
                                                        <PagerStyle BackColor="Silver" ForeColor="#333333" HorizontalAlign="Center" />
                                                        <RowStyle BackColor="WhiteSmoke" />
                                                        <SelectedRowStyle BackColor="WhiteSmoke" Font-Bold="True" ForeColor="#333333" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
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