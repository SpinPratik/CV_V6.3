<%@ Page Title="" Language="C#" MasterPageFile="~/AdminModule1.master" AutoEventWireup="true"
    CodeFile="BayTypeMaster.aspx.cs" Inherits="BayTypeMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 341px;
        }
        .style2
        {
        }
        .style4
        {
            width: 341px;
            height: 29px;
        }
        .alineMe
        {
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <%--  <asp:ScriptManager runat="server"></asp:ScriptManager>--%>
    <div style="height: 500px; margin-bottom: 30px; font-family: Consolas, Georgia; padding-top: 10px;">
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td style="padding-left: 35px">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="padding-left: 35px; white-space: nowrap;">
                    Bay Back Color :
                </td>
                <td>
                    <asp:TextBox ID="txtColor" runat="server" Width="60px" MaxLength="6"></asp:TextBox>
                    <cc1:ColorPickerExtender ID="txtColor_ColorPickerExtender" runat="server" Enabled="True"
                        TargetControlID="txtColor">
                    </cc1:ColorPickerExtender>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:Button CssClass="button" ID="btnUpdate" runat="server" OnClick="btnUpdate_Click"
                        Text="Update" CausesValidation="False" />
                </td>
                <td>
                    &nbsp;
                </td>
                <td colspan="2" style="white-space: nowrap" width="100%">
                    <asp:Label ID="lblColor" runat="server" Width="50px"></asp:Label>
                    &nbsp;
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td style="width: 10%; vertical-align: top">
                    <table style="padding-left: 30px; white-space: nowrap;">
                        <tr>
                            <td class="style2">
                                &nbsp;
                            </td>
                            <td class="style1">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="style2" colspan="2" style="background-color: #CCCCCC">
                                Bay Type Details :</td>
                        </tr>
                        <tr>
                            <td class="style2" colspan="2">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style2">
                                Bay Type :
                            </td>
                            <td class="style1">
                                <asp:TextBox ID="txtBayTypeName" runat="server" Width="150px" Style="margin-right: 2px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                Capacity :
                            </td>
                            <td class="style4">
                                <asp:TextBox ID="txtCapacity" runat="server" Width="150px"></asp:TextBox>
                                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtCapacity"
                                    Display="Dynamic" ErrorMessage="*" MaximumValue="100" MinimumValue="0" SetFocusOnError="True"
                                    Type="Integer">* Invalid</asp:RangeValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                            </td>
                            <td align="left">
                                <asp:Button CssClass="button" ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="Add" />
                                <asp:Button CssClass="button" ID="btnClear" runat="server" OnClick="btnClear_Click"
                                    CausesValidation="False" Text="Clear" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="30%" rowspan="4" valign="top">
                    <table>
                        <tr>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="grdBayCap" runat="server" ForeColor="#333333" GridLines="None"
                                    OnRowDataBound="grdBayCap_RowDataBound" OnRowEditing="grdBayCap_RowEditing" CssClass="alineMe"
                                    OnRowCancelingEdit="grdBayCap_RowCancelingEdit" DataKeyNames="BayTypeId" OnRowDeleting="grdBayCap_RowDeleting"
                                    OnRowUpdating="grdBayCap_RowUpdating" AllowPaging="True" ShowFooter="True" Width="309px">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Bay Type" HeaderStyle-HorizontalAlign="center">
                                            <EditItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txteditBayType" runat="server" Text='<%# Eval("BayTypeName") %>'></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="bayname" runat="server" Text='<%# Eval("BayTypeName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Capacity" HeaderStyle-HorizontalAlign="center">
                                            <EditItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txteditCapacity" runat="server" Text='<%# Eval("Capacity") %>' Width="87px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="capacityLbl" runat="server" Text='<%# Eval("Capacity") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False">
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Update"
                                                    Text="Update" ForeColor="#333333"></asp:LinkButton>
                                                <asp:LinkButton ID="LinkButton23" runat="server" CausesValidation="False" CommandName="Cancel"
                                                    Text="Cancel" ForeColor="#333333"></asp:LinkButton></EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnlUpdate" runat="server" CausesValidation="False" CommandName="Edit"
                                                    Text="Edit" Visible="true" ForeColor="#333333"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" CommandName="Delete"
                                                    Text="Delete" Visible="true" ForeColor="#333333"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="Silver" ForeColor="#333333" />
                                    <HeaderStyle BackColor="Silver" ForeColor="#333333" />
                                    <PagerStyle BackColor="Silver" ForeColor="#333333" HorizontalAlign="Center" />
                                    <RowStyle BackColor="WhiteSmoke" />
                                    <SelectedRowStyle BackColor="WhiteSmoke" ForeColor="#333333" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>