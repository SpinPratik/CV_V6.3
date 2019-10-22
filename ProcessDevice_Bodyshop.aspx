<%@ Page Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true"
    CodeFile="ProcessDevice_Bodyshop.aspx.cs" Inherits="ProcessDevice_Bodyshop" Title="ProcessDevice_Bodyshop" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <link href="CSS/Style.css" rel="stylesheet" />
    <style type="text/css">
       .mydatagrid span {
    background-color: unset !important;
    color: #000;
    border: unset !important;
    width:unset !important;
    float:left;
}
        .mydatagrid a {
            background-color: unset !important;
        }
        .mydatagrid span{
             width:unset !important;
    float:left;
    font-weight:unset !important;
       }
        .mydatagrid td {
            white-space: nowrap;
            font-weight:unset !important;
        }
    </style>
 <script type="text/javascript" language="javascript">
        function ValidateAlpha() {
            var keyCode = window.event.keyCode;
            if ((keyCode < 65 || keyCode > 90) && (keyCode < 97 || keyCode > 123) && keyCode != 32) {
                window.event.returnValue = false;
                alert("Special characters are not allowed");
            }
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
        <h3>Device Management</h3>
        <div class="col-md-12">                    
            <asp:Label ID="lblmsg1" runat="server"  ForeColor="Red"></asp:Label>
            </div>
        <br /><br />
            <div class="col-md-12"> 
                    <asp:GridView ID="GridView3" runat="server" CssClass="mydatagrid" AutoGenerateColumns="False" DataKeyNames="SlNo"
                        Width="100%" OnRowCancelingEdit="GridView3_RowCancelingEdit" OnRowCommand="GridView3_RowCommand"
                        ShowFooter="True" OnRowDeleting="GridView3_RowDeleting" OnRowEditing="GridView3_RowEditing"
                        OnRowUpdating="GridView3_RowUpdating" CellPadding="4" ForeColor="#333333" GridLines="None"
                        PageSize="12" Font-Bold="false" >
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField></asp:TemplateField>
                            <asp:TemplateField HeaderText="Device Name">
                                <EditItemTemplate>
                                   
                                                <asp:TextBox ID="txtdevice" runat="Server" style="text-transform:uppercase" MaxLength="20" CssClass="form-control"  Text='<%# Eval("DeviceName") %>'></asp:TextBox>
                                            
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtdevice"
                                                    CssClass="clsValidator" ErrorMessage="Enter Device Name" ValidationGroup="ab1"></asp:RequiredFieldValidator>
                                            
                                </EditItemTemplate>
                                <FooterTemplate>
                                   
                                                <asp:TextBox ID="txtdevice" runat="Server" style="text-transform:uppercase" MaxLength="20" CssClass="form-control" ></asp:TextBox>
                                            
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="L1" runat="Server" Text='<%# Eval("DeviceName") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="IP Address">
                                <EditItemTemplate>
                                  
                                                <asp:TextBox ID="txtipaddress" runat="Server" MaxLength="15" CssClass="form-control" Text='<%# Eval("IPAddress") %>'
                                                    ValidationGroup="ab1" ></asp:TextBox>
                                            
                                                <asp:RequiredFieldValidator ID="RequiredFieldtxtipaddress" runat="server" ControlToValidate="txtipaddress"
                                                    CssClass="clsValidator" ErrorMessage="Enter IP ADRESS" ValidationGroup="ab1"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                        ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtipaddress"
                                                        CssClass="clsValidator" ValidationExpression="\d{1,3}.\d{1,3}.\d{1,3}.\d{1,3}"
                                                        ValidationGroup="ab1"></asp:RegularExpressionValidator>
                                           
                                </EditItemTemplate>
                                <FooterTemplate>
                                    
                                                <asp:TextBox ID="txtipaddress" runat="Server" MaxLength="15" CssClass="form-control"></asp:TextBox>
                                            
                                                <asp:RequiredFieldValidator ID="RequiredFieldtxtipaddress" runat="server" ControlToValidate="txtipaddress"
                                                    CssClass="clsValidator"  ValidationGroup="ab11"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                        ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtipaddress"
                                                        CssClass="clsValidator" ValidationExpression="\d{1,3}.\d{1,3}.\d{1,3}.\d{1,3}"
                                                        ValidationGroup="ab11"></asp:RegularExpressionValidator>
                                           
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="L2" runat="Server" Text='<%# Eval("IPAddress") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Machine ID">
                                <EditItemTemplate>
                                  
                                                <asp:TextBox ID="txtmachineid" runat="Server" MaxLength="4" Text='<%# Eval("MachineID") %>'
                                                   CssClass="form-control" ></asp:TextBox>
                                           
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtmachineid"
                                                    CssClass="clsValidator" ErrorMessage="Enter Machine ID" ValidationGroup="ab1"></asp:RequiredFieldValidator>
                                           
                                </EditItemTemplate>
                                <FooterTemplate>
                                   
                                                <asp:TextBox ID="txtmachineid" runat="Server" MaxLength="4" CssClass="form-control"></asp:TextBox>
                                           
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="L3" runat="Server" Text='<%# Eval("MachineID") %>'></asp:Label></ItemTemplate>
                                <ItemStyle HorizontalAlign="left" />
                                <HeaderStyle HorizontalAlign="left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Location Name">
                                <EditItemTemplate>
                                   
                                                <asp:DropDownList ID="drplocnameEdit" runat="server" ValidationGroup="ab1"  CssClass="form-control"
                                                    AppendDataBoundItems="true" SelectedValue='<%# Bind("LocationName") %>' OnSelectedIndexChanged="drplocnameEdit_SelectedIndexChanged"
                                                   >
                                                    <asp:ListItem Value="Denting">Denting</asp:ListItem>
                                                    <asp:ListItem Value="Painting">Painting</asp:ListItem>
                                                   
                                                </asp:DropDownList>
                                            
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="drplocnameEdit"
                                                    CssClass="clsValidator" ErrorMessage="*" ValidationGroup="ab1"></asp:RequiredFieldValidator>
                                           
                                </EditItemTemplate>
                                <FooterTemplate>
                                   
                                                <asp:DropDownList ID="drplocname" runat="server"  OnSelectedIndexChanged="drplocname_SelectedIndexChanged"
                                                  CssClass="form-control"  >
                                                    <asp:ListItem Value="Denting">Denting</asp:ListItem>
                                                    <asp:ListItem Value="Painting">Painting</asp:ListItem>
                                                </asp:DropDownList>
                                           
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="L4" runat="Server" Text='<%# Eval("LocationName") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                           
                          
                             <asp:TemplateField HeaderText="Device SN">
                                <EditItemTemplate>
                                    
                                                <asp:TextBox ID="txtSN" runat="Server" MaxLength="20" CssClass="form-control" Text='<%# Eval("DeviceSlNO") %>'
                                                    ValidationGroup="ab1" Width="98px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtSN"
                                                    CssClass="clsValidator" ErrorMessage="Enter Device SN" ValidationGroup="ab1"></asp:RequiredFieldValidator>
                                           <span>&nbsp;</span>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    
                                                <asp:TextBox ID="txtDeviceSN" runat="Server" style="text-transform:uppercase" onKeyPress="ValidateAlpha()" CssClass="form-control" MaxLength="20" Width="100%"></asp:TextBox>
                                        
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblSN" runat="Server" Text='<%# Eval("DeviceSlNO") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="False">
                                <EditItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update"
                                        Text="Update" ValidationGroup="ab1" ForeColor="#333333"></asp:LinkButton>&nbsp;
                                    <asp:LinkButton ID="LinkButton33" runat="server" CausesValidation="False" CommandName="Cancel"
                                        Text="Cancel" ForeColor="#333333"></asp:LinkButton></EditItemTemplate>
                                <FooterTemplate>
                                    <asp:Button CssClass="btn btn-success" ID="LinkButton1" runat="server" CommandName="Add" Text="Add" ForeColor="#ffffff"></asp:Button></FooterTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton24" runat="server" CausesValidation="False" CommandName="Edit"
                                        Text="Edit" ForeColor="#333333"></asp:LinkButton></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:Button CssClass="btn btn-danger" ID="LinkButton31" runat="server" CausesValidation="False" CommandName="Delete"
                                        Text="Delete" ForeColor="#ffffff" OnClientClick="return confirm('Are you sure you want to delete this Bay?');"></asp:Button></ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="Silver" ForeColor="#333333" />
                        <FooterStyle BackColor="Silver" ForeColor="#333333" />
                        <HeaderStyle BackColor="Silver" ForeColor="#333333" HorizontalAlign="Left" />
                        <PagerStyle BackColor="Silver" ForeColor="#333333" HorizontalAlign="left" />
                        <RowStyle BackColor="WhiteSmoke" Font-Bold="false" />
                        <SelectedRowStyle BackColor="WhiteSmoke" Font-Bold="false" ForeColor="#333333" />
                    </asp:GridView>
            </div>

               
    </div>
</asp:Content>