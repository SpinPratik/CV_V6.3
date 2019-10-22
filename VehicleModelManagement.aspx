<%@ Page Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true"
    CodeFile="VehicleModelManagement.aspx.cs" Inherits="VehicleModelManagement" Title="VehicleModelManagement" %>

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
        .mydatagrid span {
    background-color: unset !important;
    color: #000;
    border: unset !important;
    width:unset !important;
    float:left;
}
     
       .mydatagrid a, .mydatagrid span{
             width:unset !important;
               font-weight:unset !important;
    float:left;
       }
          .mydatagrid a {
            background-color: unset !important;
            font-weight:bold !important;
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
    <div class="container">
        <h3>Vehicle Model</h3>
        <div class="col-md-12">        
                    <asp:Label ID="lblmsg" runat="server" Width="100%" CssClass="clsValidator" ForeColor="Red"></asp:Label>
             </div>    
         <div class="col-md-12">
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False"
                        CellPadding="4" DataKeyNames="ID" ForeColor="#333333" GridLines="None" Height="16px" OnRowDataBound="GridView1_RowDataBound"
                        OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCancelingEdit="GridView1_RowCancelingEdit1"
                        OnRowCommand="GridView1_RowCommand" OnRowDeleting="GridView1_RowDeleting1" OnRowEditing="GridView1_RowEditing1"
                        OnRowUpdating="GridView1_RowUpdating1" ShowFooter="True" Width="100%" CssClass="mydatagrid" PageSize="7">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                           
                            <asp:TemplateField HeaderText="Model">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtModel" runat="Server" Text='<%# Eval("Model") %>' MaxLength="30" CssClass="form-control"></asp:TextBox><asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtModel" ErrorMessage="Required *"
                                        ValidationGroup="ab1" CssClass="clsValidator"></asp:RequiredFieldValidator></EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtModel1" runat="Server" Placeholder="Enter vehicle model" MaxLength="30" CssClass="form-control"></asp:TextBox><asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtModel1" placeholder="Add New Model" ErrorMessage="Required *"
                                        ValidationGroup="ab" CssClass="clsValidator"></asp:RequiredFieldValidator></FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Model") %>'></asp:Label></ItemTemplate>
                                <FooterStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Model Image">
                                <EditItemTemplate>
                                    <asp:FileUpload ID="uploader" runat="server" Height="22px" /><asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator10" runat="server" ControlToValidate="uploader" ErrorMessage="*"
                                        ValidationGroup="ab1" CssClass="clsValidator"></asp:RequiredFieldValidator></EditItemTemplate>
                                <FooterTemplate>
                                    <asp:FileUpload ID="uploader1" runat="server" /><asp:RequiredFieldValidator ID="RequiredFieldValidator11"
                                        runat="server" ControlToValidate="uploader1" ErrorMessage="Required *" ValidationGroup="ab"
                                        CssClass="clsValidator"></asp:RequiredFieldValidator></FooterTemplate>
                                <ItemTemplate>
                                    <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("ModelImageUrl", "https://res.cloudinary.com/deekyp5bi/image/upload/v1484138547/vehicles/{0}")%>'
                                        Height="50px" Width="58px" /></ItemTemplate>
                                <FooterStyle HorizontalAlign="left" />
                                <HeaderStyle HorizontalAlign="left" />
                                <ItemStyle HorizontalAlign="left" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Short Code">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtShot" runat="Server" Text='<%# Eval("ShotCode") %>' MaxLength="30" CssClass="form-control" style="text-transform:uppercase;"></asp:TextBox><asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtShot" ErrorMessage="Required *"
                                        ValidationGroup="ab1" CssClass="clsValidator" ></asp:RequiredFieldValidator></EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtShot1" runat="Server" MaxLength="4" CssClass="form-control" style="text-transform:uppercase;"></asp:TextBox><asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator19" runat="server" ControlToValidate="txtShot1" placeholder="Add New Model" ErrorMessage="Required *"
                                        ValidationGroup="ab" CssClass="clsValidator" ></asp:RequiredFieldValidator></FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblShot" runat="server" Text='<%# Bind("ShotCode") %>'></asp:Label></ItemTemplate>
                                <FooterStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <EditItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update"
                                        Text="Update" ForeColor="#333333"></asp:LinkButton>&nbsp;<asp:LinkButton ID="LinkButton21"
                                            runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" ForeColor="#333333"></asp:LinkButton>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:Button ID="LinkButton1" Width="100%" CssClass="btn btn-success" runat="server" CommandName="Add" Text="Add" ValidationGroup="ab"
                                       ></asp:Button></FooterTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit"
                                        Text="Edit" ForeColor="#333333"></asp:LinkButton></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:Button ID="LinkButton22" CssClass="btn btn-danger" runat="server" CausesValidation="False" CommandName="Delete"
                                        Text="Delete" Width="77%" OnClientClick="return confirm('Are you sure you want to delete this Vehicle Model?');"></asp:Button></ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="Silver" Height="25px" ForeColor="#333333" />
                        <FooterStyle BackColor="Silver" ForeColor="#333333" VerticalAlign="Middle" />
                        <HeaderStyle BackColor="Silver" ForeColor="#333333" />
                        <PagerStyle BackColor="Silver" ForeColor="#333333" HorizontalAlign="Center" />
                        <RowStyle BackColor="WhiteSmoke" Height="40px" />
                        <SelectedRowStyle BackColor="WhiteSmoke" Font-Bold="True" ForeColor="#333333" />
                    </asp:GridView>
            </div>

                
    </div>
</asp:Content>