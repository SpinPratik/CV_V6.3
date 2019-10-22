<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true"
    CodeFile="TemplateMaster.aspx.cs" Inherits="TemplateMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
      <link href="CSS/Style.css" rel="stylesheet" />
    <style type="text/css">
       
        .mydatagrid a {
            background-color: unset !important;
        }
        .mydatagrid span {
    background-color: unset !important;
    color: #000;
    /*border: 1px solid #3AC0F2;*/
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    

    <div class="container">
        <h3>&nbsp;&nbsp;Remarks Template</h3>
         <div class="col-md-12" style="background: #f9f9f9 !important;" >
             <div class="form-group col-md-12">
                 <asp:Label ID="lblID" runat="server" Width="100%" Visible="false"></asp:Label>
                  <asp:Label ID="lblMsg" runat="server" Width="100%"></asp:Label>
             </div>
        <div class="form-group col-md-6">
                    <label for="pwd">type</label> <span style="color:red">*</span>
                    <asp:DropDownList runat="server" ID="ddlTemplateType" CssClass="form-control">
                                    <asp:ListItem Selected="True" Value="0">SELECT</asp:ListItem>
                                    <asp:ListItem Value="1">JCC Remarks</asp:ListItem>
                                    <asp:ListItem Value="2">PDT remarks</asp:ListItem>
                                    <asp:ListItem Value="3">Vehicle Cancelation Remarks</asp:ListItem>
                                    <asp:ListItem Value="4">Tag Cancellation</asp:ListItem>
                                    <asp:ListItem Value="5">Vehicle OUT Remarks</asp:ListItem>
                                    <asp:ListItem Value="6">Service Remarks</asp:ListItem>
                                    <asp:ListItem Value="7">Process Remarks</asp:ListItem>
                                    <asp:ListItem Value="8">Carry Forward</asp:ListItem>
                                </asp:DropDownList>
                   
                </div>

                <div class="form-group col-md-6">
                    <label for="pwd">Remarks</label> <span style="color:red">*</span><br />
                    <asp:TextBox ID="txtTemplate" runat="server" MaxLength="30" 
                                   CssClass="form-control" ToolTip="Remarks Template" CausesValidation="True"  placeholder="Type Remarks here (upto max 30 characters)"></asp:TextBox>
                </div>
                 <div class="form-group col-md-12">
                     <asp:Button CssClass="btn btn-success" ID="btnSave" runat="server" OnClick="btnSave_Click"
                                    Text="SAVE" />
                      <asp:Button CssClass="btn btn-primary" ID="btn_BayUpdate" runat="server" OnClick="btn_BayUpdate_Click"
                    Text="UPDATE" ValidationGroup="s"></asp:Button>
                                <asp:Button CssClass="btn btn-info" ID="btnClear" runat="server" OnClick="btnClear_Click"
                                    Text="RESET" />
                     </div>
             </div>
         <div class="col-md-12">
             <br /><h3>Existing Remarks Templates</h3>
         </div>
        <div class="col-md-12" style="background: #f9f9f9 !important;">
            <asp:GridView ForeColor="#333333" GridLines="None" ID="grdTemplate" runat="server"
                                    AutoGenerateColumns="false" Width="100%" OnRowDataBound="grdTemplate_RowDataBound"
                                    OnSelectedIndexChanged="grdTemplate_SelectedIndexChanged" OnRowDeleting="grdTemplate_RowDeleting"
                                    OnPageIndexChanging="grdTemplate_PageIndexChanging" AllowPaging="true" PageSize="10" CssClass="mydatagrid" >
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:CommandField ButtonType="Image" SelectImageUrl="https://res.cloudinary.com/deekyp5bi/image/upload/v1484224665/jcr/gridSelect.png" ShowSelectButton="True" />
                                        <asp:BoundField HeaderText="Type" DataField="Type" />
                                        <asp:BoundField HeaderText="Template Remarks" DataField="Remarks Template" />
                                         <asp:BoundField HeaderText="Slno" DataField="SlNo" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                	<asp:Button ID="LinkButton1" CssClass="btn btn-danger" runat="server" CausesValidation="False"
                                                         CommandName="Delete" Text="Delete" Visible="true" Width="50%"
                                                OnClientClick="return confirm('Are you sure you want to delete this Remarks Template?');" />
                                               <%-- <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                                    Text="Delete" Visible="true" ForeColor="#333333"></asp:LinkButton>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="Silver" ForeColor="#333333" />
                                    <HeaderStyle BackColor="Silver" ForeColor="#333333" HorizontalAlign="Left" />
                                    <PagerStyle BackColor="Silver" ForeColor="#333333" HorizontalAlign="Center" />
                                    <RowStyle BackColor="WhiteSmoke" />
                                    <SelectedRowStyle BackColor="WhiteSmoke" Font-Bold="True" ForeColor="#333333" />
                                </asp:GridView>
        </div>
    </div>
</asp:Content>