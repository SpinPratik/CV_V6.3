<%@ Page Language="C#" MasterPageFile="~/AdminModule1.master" AutoEventWireup="true"
    CodeFile="GroupMail.aspx.cs" Inherits="GroupMail" Title="Group Mail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 97px;
            vertical-align: top;
        }
        .style2
        {
            width: 149px;
        }
        .style3
        {
            width: 97px;
            vertical-align: top;
            height: 54px;
        }
        .style4
        {
            height: 54px;
            width: 12px;
        }
        .style5
        {
            width: 12px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="padding: 10px; height: 595px; width: 100%;">
        <table style="width: 100%" class="tblStyle">
            <tr>
                <td valign="top">
                    <table style="width: 400px;">
                        <tr class="commonFont">
                            <td align="right" class="style1">
                                <asp:Label ID="Label1" runat="server" Text="Group Name"></asp:Label>
                                <asp:Label ID="Label36" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="style2">
                                <asp:TextBox ID="txt_GroupName" runat="server" Width="172px"></asp:TextBox>
                            </td>
                            <td class="style5">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_GroupName"
                                    CssClass="clsValidator" ErrorMessage="*" ValidationGroup="cu"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr class="commonFont">
                            <td align="right" class="style3">
                                <asp:Label ID="Label3" runat="server" Text="Reports"></asp:Label>
                                <asp:Label ID="Label5" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="True" OnCheckedChanged="chkSelectAll_CheckedChanged"
                                    Text="Select All" />
                                <div style="overflow: scroll; height: 300px; width: 250px;">
                                    <asp:UpdatePanel ID="reportList" runat="server">
                                        <ContentTemplate>
                                            <asp:CheckBoxList ID="chkreportsList" runat="server" BorderColor="White" BorderWidth="1pt"
                                                BorderStyle="Solid" AutoPostBack="True" OnSelectedIndexChanged="chkreportsList_SelectedIndexChanged">
                                            </asp:CheckBoxList>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="chkreportsList" EventName="selectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </td>
                            <td class="style4">
                            </td>
                        </tr>
                        <tr class="commonFont">
                            <td align="right" class="style1">
                                <asp:Label ID="Label6" runat="server" Text="Email Ids"></asp:Label>
                                <asp:Label ID="Label7" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                <br />
                                (, Seperated)
                            </td>
                            <td class="style2">
                                <asp:TextBox ID="txt_EmailIds" runat="server" Width="172px"></asp:TextBox>
                            </td>
                            <td class="style5">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_EmailIds"
                                    ErrorMessage="*" ValidationGroup="cu" CssClass="clsValidator"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr class="commonFont">
                            <td align="right" class="style1">
                            </td>
                            <td class="style2">
                                <asp:CheckBox ID="chkActive" runat="server" Text="Active" />
                            </td>
                            <td class="style5">
                            </td>
                        </tr>
                        <tr>
                            <td align="center" class="clmDistri" colspan="3">
                                <asp:Label ID="err_Message" runat="server" CssClass="clsValidator" ForeColor="Black"></asp:Label>
                                <asp:Label ID="l1" runat="server" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                            </td>
                            <td class="style2">
                                <asp:Button CssClass="button" ID="btn_Save" runat="server" ImageUrl="~/imgButton/SAVE.jpg"
                                    Text="Save" OnClick="btn_Save_Click"></asp:Button>
                                <asp:Button CssClass="button" runat="server" ImageUrl="~/imgButton/UPDATE.jpg" Text="Update"
                                    ID="btn_Update" OnClick="btn_Update_Click"></asp:Button>
                                <asp:Button CssClass="button" ID="btn_Clear" runat="server" ImageUrl="~/imgButton/CLEAR.jpg"
                                    OnClick="btn_Clear_Click" Text="Clear"></asp:Button>
                            </td>
                            <td class="style5">
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="top">
                    <table style="width: 450;">
                        <tr>
                            <td class="tblHead1" style="font-size: medium;">
                                Existing Group List
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <asp:GridView ID="grdGroupList" runat="server" AllowPaging="True" AllowSorting="True"
                                    AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                                    OnSelectedIndexChanged="grdGroupList_SelectedIndexChanged" OnPageIndexChanging="grdGroupList_PageIndexChanging"
                                    OnRowDataBound="grdGroupList_RowDataBound" OnRowDeleting="grdGroupList_RowDeleting"
                                    Width="100px">
                                    <RowStyle BackColor="WhiteSmoke" />
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:CommandField ButtonType="Image" SelectImageUrl="~/images/gridSelect.png" ShowSelectButton="True" />
                                        <asp:BoundField DataField="Id" HeaderText="Id" />
                                        <asp:BoundField DataField="GroupName" HeaderText="Group Name" ItemStyle-VerticalAlign="Middle"
                                            HeaderStyle-Wrap="false">
                                            <ItemStyle VerticalAlign="Middle" Wrap="false"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Email Ids" ItemStyle-VerticalAlign="Middle">
                                            <ItemStyle VerticalAlign="Middle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Active" ItemStyle-VerticalAlign="Middle">
                                            <ItemStyle VerticalAlign="Middle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Active">
                                            <ItemStyle VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DOC" HeaderText="DOC" />
                                        <asp:BoundField DataField="RefReportId" HeaderText="RefReportId " ItemStyle-VerticalAlign="Middle">
                                            <ItemStyle VerticalAlign="Middle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                                    Text="Delete" ForeColor="#333333"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="EmailIds" />
                                    </Columns>
                                    <EditRowStyle BackColor="WhiteSmoke" />
                                    <FooterStyle BackColor="Silver" ForeColor="#333333" />
                                    <HeaderStyle BackColor="Silver" ForeColor="#333333" HorizontalAlign="Left" />
                                    <PagerStyle BackColor="Silver" ForeColor="#333333" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="WhiteSmoke" Font-Bold="True" ForeColor="#333333" />
                                </asp:GridView>
                                <br />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>