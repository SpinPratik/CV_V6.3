<%@ Page Language="C#" MasterPageFile="~/AdminModule_New.master" AutoEventWireup="true"
    CodeFile="DisplayMessage.aspx.cs" Inherits="DisplayMessage" Title="DisplayMessage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .thisstyle
        {
            width: 40%;
        }
        .LeftGridPad
        {
            padding-left: 40px;
        }
        .style1
        {
            width: 66px;
        }
        .style3
        {
            width: 66px;
            height: 27px;
        }
        .style4
        {
            height: 27px;
        }
        .style5
        {
            height: 21px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <div style="padding: 10px; height: 565px; margin-bottom: 30px; vertical-align: middle;">
        <asp:UpdatePanel ID="messageUpdPnl" runat="server">
            <ContentTemplate>
                <table style="width: 50%;" class="tblStyle">
                    <tr>
                        <td colspan="2" align="left">
                            <asp:Label ID="lbl_msg" runat="server" CssClass="clsValidator" ForeColor="Red"></asp:Label>
                        </td>
                    </tr><tr>
                                    <td>
                                          <asp:Label ID="Label1" runat="server" Text="DISPLAY Message:"></asp:Label>
                                        <asp:TextBox ID="txt_Msg" runat="server" MaxLength="200" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td valign="bottom">
                                        <asp:ImageButton ID="btnRefreshDisplay" runat="server" AlternateText="Refresh" Height="20px"
                                            ImageUrl="~/images/refresh.png" OnClick="btnRefreshDisplay_Click" />
                                    </td>
                                     <td><br>
                            <asp:Button CssClass="btn btn-success" ID="btn_Update" runat="server" OnClick="btn_Update_Click"
                                Text="SAVE" />
                            <asp:Button CssClass="btn btn-danger" ID="btn_Clear" runat="server" OnClick="btn_Clear_Click"
                                Text="RESET" />
                                          <asp:Button CssClass="btn btn-warning" ID="btn_next" runat="server" OnClick="btn_next_Click"
                                Text="NEXT" />
                        </td>
                                </tr>
                           
                  
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_Clear" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnRefreshDisplay" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>