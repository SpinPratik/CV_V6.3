<%@ Page Title="" Language="C#" MasterPageFile="~/AdminModule.master" AutoEventWireup="true"
    CodeFile="Complain.aspx.cs" Inherits="Complain" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table style="width: 100%;" class="tblStyle">
        <tr>
            <td colspan="7">
                <div align="center" style="height: 580px;">
                    <div align="right" class="commonFont" style="padding-right: 10px;">
                        <img src="images/mail.png" alt="Email : " /><a href="mailto:support@spintech.in">support@spintech.in</a>
                        &nbsp;<img src="images/phone.png" alt="Phone : " />&nbsp;91-80-4080-5000
                    </div>
                    <table id="Table1" class="tblStyle" style="border: 1px solid #666666; width: 800px;
                        text-align: left;" runat="server">
                        <tr>
                            <td class="tblHeadsCol">
                                &nbsp;
                            </td>
                            <td class="tblDatasCol" colspan="2">
                                &nbsp;
                            </td>
                            <td class="tblHeadsCol">
                                &nbsp;
                            </td>
                            <td class="tblDatasCol" colspan="2">
                                <asp:Label ID="lbl_date" runat="server" Style="display: none;"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="tblHeadsCol">
                                CSR No
                            </td>
                            <td class="tblDatasCol" colspan="2">
                                <asp:Label ID="lbl_csrno" runat="server"></asp:Label>
                            </td>
                            <td class="tblHeadsCol">
                                Dealer Code
                            </td>
                            <td class="tblDatasCol" colspan="2">
                                <asp:Label ID="lblDealerCode" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="tblHeadsCol">
                                Dealer Name
                            </td>
                            <td class="tblDatasCol" colspan="2">
                                <asp:Label ID="lblDealer" runat="server"></asp:Label>
                            </td>
                            <td class="tblHeadsCol">
                                Location
                            </td>
                            <td class="tblDatasCol" colspan="2">
                                <asp:Label ID="lblLocation" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="tblHeadsCol">
                                Address
                            </td>
                            <td class="tblDatasCol" colspan="2">
                                <asp:Label ID="lblAddress" runat="server"></asp:Label>
                            </td>
                            <td class="tblHeadsCol">
                                City
                            </td>
                            <td class="tblDatasCol" colspan="2">
                                <asp:Label ID="lblCity" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="tblHeadsCol">
                                &nbsp;
                            </td>
                            <td class="tblDatasColSmall">
                                &nbsp;
                            </td>
                            <td class="tblHeadsCol">
                                &nbsp;
                            </td>
                            <td class="tblDatasColSmall">
                                &nbsp;
                            </td>
                            <td class="tblHeadsCol">
                                &nbsp;
                            </td>
                            <td class="tblDatasColSmall">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <table class="tblStyle" style="width: 800px; text-align: left; border-right-style: solid;
                        border-bottom-style: solid; border-left-style: solid; border-right-width: 1px;
                        border-bottom-width: 1px; border-left-width: 1px; border-right-color: #666666;
                        border-bottom-color: #666666; border-left-color: #666666;">
                        <tr>
                            <td class="tblHeadsCol">
                                &nbsp;
                            </td>
                            <td class="tblDatasCol">
                                &nbsp;
                                <asp:Label ID="lbl_msg" runat="server" CssClass="clsValidator"></asp:Label>
                            </td>
                            <td class="tblHeadsCol">
                                &nbsp;
                            </td>
                            <td class="tblDatasCol">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="tblHeadsCol">
                                System Down
                            </td>
                            <td class="tblDatasCol">
                                <asp:RadioButton ID="rbox_yes" runat="server" GroupName="s" Text="Yes" OnCheckedChanged="rbox_yes_CheckedChanged" />
                                <asp:RadioButton ID="rbox_no" runat="server" GroupName="s" Text="No" OnCheckedChanged="rbox_no_CheckedChanged" />
                            </td>
                            <td class="tblHeadsCol">
                                &nbsp;
                            </td>
                            <td class="tblDatasCol">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="tblHeadsCol">
                                &nbsp;
                            </td>
                            <td class="tblDatasCol">
                                <table>
                                    <tr>
                                        <td colspan="2">
                                            Hardware :
                                        </td>
                                    </tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:CheckBoxList ID="chkHardware" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem>RF Reader</asp:ListItem>
                                            <asp:ListItem>RF Card</asp:ListItem>
                                            <asp:ListItem>Thin Client</asp:ListItem>
                                            <asp:ListItem>Power Adapter</asp:ListItem>
                                        </asp:CheckBoxList>
                                    </td>
                                    <tr>
                                        <td colspan="2">
                                            Software :
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:CheckBoxList ID="chkSoftware" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem>Functionality</asp:ListItem>
                                                <asp:ListItem>Report</asp:ListItem>
                                                <asp:ListItem>Display</asp:ListItem>
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="tblHeadsCol">
                                &nbsp;
                            </td>
                            <td class="tblDatasCol">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="tblHeadsCol">
                                Call Reported By
                            </td>
                            <td class="tblDatasCol" colspan="3">
                                <asp:TextBox ID="txt_callreportedby" runat="server" Width="217px" MaxLength="25"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_callreportedby"
                                    ErrorMessage="Enter Call Reporter Name. !" ValidationGroup="cs" CssClass="clsValidator"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="tblHeadsCol">
                                File Attachment
                            </td>
                            <td class="tblDatasCol">
                                <asp:AsyncFileUpload ID="uf1" runat="server" CompleteBackColor="Green" PersistFile="True"
                                    UploadingBackColor="#99CCFF" Width="210px" />
                                <asp:Label ID="lbl_size" runat="server" Style="display: none;"></asp:Label>
                            </td>
                            <td class="tblHeadsCol">
                                &nbsp;
                            </td>
                            <td class="tblDatasCol">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="tblHeadsCol" valign="top">
                                Problem Reported
                            </td>
                            <td class="tblDatasCol">
                                <asp:TextBox ID="txt_problem" runat="server" Height="120px" TextMode="MultiLine"
                                    Width="100%" MaxLength="500"></asp:TextBox>
                            </td>
                            <td class="tblHeadsCol">
                                &nbsp;
                            </td>
                            <td class="tblDatasCol">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="tblHeadsCol">
                                &nbsp;
                            </td>
                            <td class="tblDatasCol">
                                <asp:Button CssClass="button" ID="btn_send" runat="server" OnClick="btn_send_Click"
                                    Text="Send" ValidationGroup="cs" />
                                <asp:Button CssClass="button" ID="btn_New" runat="server" Text="Clear" OnClick="btn_New_Click" />
                                <asp:Button CssClass="button" ID="btn_clear" runat="server" OnClick="btn_clear_Click"
                                    Text="Clear" />
                            </td>
                            <td class="tblHeadsCol">
                                &nbsp;
                            </td>
                            <td class="tblDatasCol">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="tblHeadsCol">
                                &nbsp;
                            </td>
                            <td class="tblDatasCol">
                                &nbsp;
                            </td>
                            <td class="tblHeadsCol">
                                &nbsp;
                            </td>
                            <td class="tblDatasCol">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>