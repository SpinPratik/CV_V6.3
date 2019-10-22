<%@ Page Language="C#" MasterPageFile="~/AdminModule_New.master" AutoEventWireup="true"
    CodeFile="WorkTime.aspx.cs" Inherits="WorkTime" Title="WorkTime" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="CSS/Style.css" rel="stylesheet" />
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
            width: 68px;
        }
        .mydatagrid a, .mydatagrid span {
            width: unset !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    
    <asp:UpdatePanel ID="updTime" runat="server">
        <ContentTemplate>
            <asp:Timer ID="NotificationTimer" runat="server" Interval="10000">
            </asp:Timer>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="updShift" runat="server">
        <ContentTemplate>

            <div class="container">
        <div class="row">
          
            <div class="col-sm-12 col-md-6" style="overflow-y: scroll;">
                <div class="form" style="width:100%;background: #f9f9f9 !important;padding: 25px;">
               <table style="width:100%;">
                   <tr>
                                    <td align="left" colspan="2">
                                        <asp:Label ID="lblmessage" runat="server" CssClass="clsValidator"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                      
                                </tr>
                                <tr>
                                          <td>
                                                    <label>Shift&nbsp;&nbsp;&nbsp;</label>
                                                 <asp:CheckBox ID="chkShift" runat="server" AutoPostBack="True" Text="&nbsp;New" OnCheckedChanged="chkShift_CheckedChanged" />
                                              
                                                    <asp:DropDownList ID="ddlShift" runat="server" AppendDataBoundItems="True" CssClass="form-control">
                                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:TextBox ID="txtShift" runat="server" MaxLength="2" Visible="False" CssClass="form-control" ></asp:TextBox>
                                                </td>
                                               
                                             
                                </tr>
                                  <tr><td>&nbsp;</td></tr>
                                <tr>
                                   
                                    <td>
                                        <label> In Time</label>
                                        <asp:DropDownList ID="ddlintime" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                  <tr><td>&nbsp;</td></tr>
                                <tr>
                                    
                                    <td>
                                        <label> Break In</label>
                                        <asp:DropDownList ID="ddlbrkin" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                  <tr><td>&nbsp;</td></tr>
                                <tr>
                                   
                                    <td>
                                        <label> Break Out</label>
                                        <asp:DropDownList ID="ddlbrkout" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                  <tr><td>&nbsp;</td></tr>
                                <tr>
                                    <td>
                                        <label>Out Time</label>
                                        <asp:DropDownList ID="ddlouttime" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr><td>&nbsp;</td></tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnsave" runat="server" CssClass="btn btn-success" OnClick="btnsave_Click"
                                            Text="SAVE" />
                                        <asp:Button ID="btnrefrsh" runat="server" CssClass="btn btn-default" OnClick="btnrefrsh_Click"
                                            Text="REFRESH" />
                                        <asp:Button ID="btn_next" runat="server" CssClass="btn btn-warning" OnClick="btn_next_Click"
                                            Text="NEXT" />
                                    </td>
                                </tr>
                           
               </table>
                 </div>
            </div>

            <div class="col-sm-12 col-md-6">
                 <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                                            AutoGenerateColumns="False" CellPadding="4" GridLines="None" CssClass="mydatagrid"
                                            OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound"
                                            OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDeleting="GridView1_RowDeleting">
                                            <Columns>
                                                <asp:CommandField ButtonType="Image" SelectImageUrl="~/images/gridSelect.png" ShowSelectButton="True" />
                                                <asp:BoundField DataField="ShiftId" HeaderText="Shift Id" ItemStyle-HorizontalAlign="Center">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="InTime" HeaderText="In Time" ItemStyle-HorizontalAlign="Center">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="BreakIn" HeaderText="Break In" ItemStyle-HorizontalAlign="Center">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="BreakOut" HeaderText="Break Out" ItemStyle-HorizontalAlign="Center">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="OutTime" HeaderText="Out Time" ItemStyle-HorizontalAlign="Center">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Shift" HeaderText="Shift" ItemStyle-HorizontalAlign="Center">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                  <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                                    Text="Delete" ForeColor="#333333"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle />
                                            <AlternatingRowStyle />
                                            <EditRowStyle />
                                            <HeaderStyle/>
                                            <PagerStyle/>
                                            <RowStyle />
                                            <SelectedRowStyle BackColor="WhiteSmoke" Font-Bold="True" ForeColor="#333333" />
                                        </asp:GridView>
            </div>
        </div>


    </div>

            
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnsave" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnrefrsh" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="chkShift" EventName="CheckedChanged" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>