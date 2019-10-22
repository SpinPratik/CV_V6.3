<%@ Page Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true"
    CodeFile="ServiceType.aspx.cs" Inherits="ServiceType" Title="ServiceType" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
        .tblStyle1
        {
            font-family: Arial;
            font-size: small;
            vertical-align: top;
        }
         .mydatagrid span {
    background-color: unset !important;
    color: #000;
    border: unset !important;
    width:unset !important;
    float:left;
}
       
       .mydatagrid a, .mydatagrid span{
            width:unset !important;
            font-weight: unset !important;
            float:left;
       }
        .mydatagrid a {
            background-color: unset !important;
            font-weight:bold !important;
        }
        span{
            text-transform:unset !important;
        }
    </style>
    <script type="text/javascript">
        function alpha(e) {
            var k;
            document.all ? k = e.keyCode : k = e.which;
            return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || k == 32 || (k >= 48 && k <= 57));
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="updTime" runat="server">
        <ContentTemplate>
            <asp:Timer ID="NotificationTimer" runat="server" Interval="10000">
            </asp:Timer>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="container">
        <h3>Service Type</h3>
       <div class="col-md-12">
                    <asp:Label ID="lblmsg0" runat="server" CssClass="clsValidator" Width="100%" ForeColor="Red"></asp:Label><br />
           </div>    <div class="col-md-12"> 
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="ServiceID"
                        OnRowCancelingEdit="GridView2_RowCancelingEdit" OnRowCommand="GridView2_RowCommand"
                        ShowFooter="True" OnRowDeleting="GridView2_RowDeleting" OnRowEditing="GridView2_RowEditing"
                        OnRowUpdating="GridView2_RowUpdating" AllowPaging="True" OnPageIndexChanging="GridView2_PageIndexChanging"
                        CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%" CssClass="mydatagrid" PageSize="10"
                        Font-Size="10pt">
                       <%-- <AlternatingRowStyle BackColor="White" HorizontalAlign="Center"
                            VerticalAlign="Middle" />--%>
                        <Columns>
                            <asp:TemplateField>
                                <ItemStyle VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Service Type">
                                <EditItemTemplate>
                                    
                                                <asp:TextBox ID="txtServiceType" onkeypress="return alpha(event)" runat="Server" CssClass="form-control" MaxLength="49" Text='<%# Eval("ServiceType") %>'> </asp:TextBox>
                                           
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtServiceType"
                                                    CssClass="clsValidator" ErrorMessage="*" ValidationGroup="ab1"> </asp:RequiredFieldValidator>
                                           
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtServiceType" onkeypress="return alpha(event)" runat="Server" MaxLength="49" CssClass="form-control" placeholder="Add New Service Type"></asp:TextBox></FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="L1" runat="Server" Text='<%# Eval("ServiceType") %>'></asp:Label></ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Process Time">
                                <EditItemTemplate>
                                   
                                                <asp:TextBox ID="txtProcessTime" onkeypress="return alpha(event)" runat="Server" MaxLength="5" CssClass="form-control"  Text='<%# Eval("ProcessTime") %>' placeholder="Add New Process Time (In Minutes)"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender
                                                    ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtProcessTime"
                                                    ValidChars="1234567890">
                                                </cc1:FilteredTextBoxExtender>
                                          
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtProcessTime"
                                                    CssClass="clsValidator" ErrorMessage="*" ValidationGroup="ab1"></asp:RequiredFieldValidator>
                                          
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtProcessTime" onkeypress="return alpha(event)" runat="Server" MaxLength="5" CssClass="form-control" placeholder="Add New Process Time (In Minutes)"></asp:TextBox><cc1:FilteredTextBoxExtender
                                        ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtProcessTime"
                                        FilterType="Numbers" ValidChars="1234567890">
                                    </cc1:FilteredTextBoxExtender>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="L2" runat="Server" Text='<%# Eval("ProcessTime") %>'></asp:Label></ItemTemplate>
                                <ItemStyle VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Short Code">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtScode" style="text-transform:uppercase;" onkeypress="return alpha(event)" runat="server" Text='<%# Eval("ServiceType_SCode") %>' CssClass="form-control" placeholder="Add short Code"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtScode"
                                                    CssClass="clsValidator" ErrorMessage="*" ></asp:RequiredFieldValidator>
                                </EditItemTemplate>
                               
                                <FooterTemplate>
                                    <asp:TextBox ID="txtScode" runat="server" CssClass="form-control" onkeypress="return alpha(event)" placeholder="Add short Code" style="text-transform:uppercase;"></asp:TextBox>
                                    
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="L3" runat="server" Text='<%# Eval("ServiceType_SCode") %>'></asp:Label></ItemTemplate>
                                <FooterStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <EditItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update"
                                        Text="Update" ForeColor="#333333"></asp:LinkButton>&nbsp;
                                    <asp:LinkButton ID="LinkButton23" runat="server" CausesValidation="False" CommandName="Cancel"
                                        Text="Cancel" ForeColor="#333333"></asp:LinkButton>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:Button ID="LinkButton1" CssClass="btn btn-success" runat="server" CommandName="Add" Text="Add"></asp:Button>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton32" runat="server" CausesValidation="False" CommandName="Edit"
                                        Text="Edit" ForeColor="#333333"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:Button ID="LinkButton4" CssClass="btn btn-danger" runat="server" CausesValidation="False" CommandName="Delete"
                                        Text="Delete" OnClientClick="return confirm('Are you sure you want to delete this Service Type?');"></asp:Button></ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle  HorizontalAlign="left" VerticalAlign="Middle"
                            ForeColor="#333333" />
                        <EmptyDataRowStyle HorizontalAlign="left" VerticalAlign="Middle" />
                        <FooterStyle  Font-Bold="True" ForeColor="#333333" HorizontalAlign="left" />
                        <HeaderStyle Font-Bold="True" ForeColor="#333333" HorizontalAlign="left" />
                        <PagerStyle  ForeColor="#333333" HorizontalAlign="left" />
                        <RowStyle  HorizontalAlign="left" VerticalAlign="Middle" />
                        <SelectedRowStyle  Font-Bold="True" ForeColor="#333333" HorizontalAlign="left"
                            VerticalAlign="Middle" />
                    </asp:GridView>
               </div>
    </div>
</asp:Content>