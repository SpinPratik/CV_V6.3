<%@ Page Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true"
    CodeFile="ProcessDevice.aspx.cs" Inherits="ProcessDevice" Title="ProcessDevice" %>

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
       .mydatagrid a, .mydatagrid span{
             width:unset !important;
             font-weight: unset !important;
             float:left;
       }
       .mydatagrid a{
           font-weight:bold !important;
       }
        .mydatagrid td {
            white-space: nowrap;
        }
    </style>
          <script type="text/javascript" language="javascript">
        function ValidateAlpha() {
            var keyCode = window.event.keyCode;
            if ((keyCode < 65 || keyCode > 90) && (keyCode < 97 || keyCode > 123) && (keyCode < 48 || keyCode > 57) && keyCode != 32) {
                window.event.returnValue = false;
                alert("Special Characters not allowed");
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
            <asp:Label ID="lblmsg1" runat="server" Width="100%"></asp:Label>
            </div>
            <div class="col-md-12"> 
                    <asp:GridView ID="GridView3" runat="server" CssClass="mydatagrid" AutoGenerateColumns="False" DataKeyNames="SlNo"
                        Width="100%" OnRowCancelingEdit="GridView3_RowCancelingEdit" OnRowCommand="GridView3_RowCommand"
                        ShowFooter="True" Font-Bold="false" OnRowDeleting="GridView3_RowDeleting" OnRowEditing="GridView3_RowEditing"
                        OnRowUpdating="GridView3_RowUpdating" CellPadding="4" ForeColor="#333333" GridLines="None"
                        PageSize="12" >
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField></asp:TemplateField>
                            <asp:TemplateField HeaderText="Device Name">
                                <EditItemTemplate>
                                   
                                                <asp:TextBox ID="txtdevice" runat="Server" MaxLength="20" CssClass="form-control"  Text='<%# Eval("DeviceName") %>'></asp:TextBox>
                                            
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtdevice"
                                                    CssClass="clsValidator" ErrorMessage="*" ValidationGroup="ab1"></asp:RequiredFieldValidator>
                                            
                                </EditItemTemplate>
                                <FooterTemplate>
                                   
                                                <asp:TextBox ID="txtdevice" runat="Server" MaxLength="20" CssClass="form-control" ></asp:TextBox>
                                            
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="L1" runat="Server" Text='<%# Eval("DeviceName") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="IP Address">
                                <EditItemTemplate>
                                  
                                                <asp:TextBox ID="txtipaddress" runat="Server" MaxLength="15" CssClass="form-control" Text='<%# Eval("IPAddress") %>'
                                                    ValidationGroup="ab1" ></asp:TextBox>
                                            
                                                <asp:RequiredFieldValidator  ID="RequiredFieldtxtipaddress" runat="server" ControlToValidate="txtipaddress"
                                                    CssClass="clsValidator" ErrorMessage="*" ValidationGroup="ab1"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
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
                                                    CssClass="clsValidator" ErrorMessage="*" ValidationGroup="ab1"></asp:RequiredFieldValidator>
                                           
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtmachineid" runat="Server" MaxLength="4" CssClass="form-control"></asp:TextBox>
                                           
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="L3" runat="Server" Text='<%# Eval("MachineID") %>'></asp:Label></ItemTemplate>
                                <ItemStyle HorizontalAlign="left" />
                                <HeaderStyle HorizontalAlign="left" />
                            </asp:TemplateField>
                           <%-- <asp:TemplateField HeaderText="Bodyshop" ItemStyle-HorizontalAlign="left" FooterStyle-HorizontalAlign="left">
                                <EditItemTemplate>
                                    <asp:CheckBox ID="chk_isbodyshop" runat="server" Checked='<%# Eval("IsSpeedoBay").ToString()=="True"?true:false %>'
                                        Visible='<%# Eval("LocationName").ToString()=="Workshop"?true:false %>' />
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:CheckBox ID="chk_isbodyshop2" runat="server" Visible="false" />
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk_isbodyshop1" runat="server" Checked='<%# Eval("IsSpeedoBay").ToString()=="True"?true:false %>'
                                        Visible='<%# Eval("LocationName").ToString()=="Workshop"?true:false %>' Enabled='<%# Eval("LocationName").ToString()=="Workshop"?false:true %>' />
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="left"></FooterStyle>
                                <ItemStyle HorizontalAlign="left"></ItemStyle>
                            </asp:TemplateField>--%>
                           <%-- <asp:TemplateField HeaderText="Bodyshop" >
                                <FooterTemplate>
                                    <asp:CheckBox ID="chk_isbodyshop" runat="server" />
                                </FooterTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Location Name">
                                <EditItemTemplate>
                                   
                                                <asp:DropDownList ID="drplocnameEdit" runat="server" ValidationGroup="ab1" AutoPostBack="True" CssClass="form-control"
                                                    AppendDataBoundItems="true" SelectedValue='<%# Bind("LocationName") %>' OnSelectedIndexChanged="drplocnameEdit_SelectedIndexChanged" >
                                                    <asp:ListItem Value="Gate">Gate</asp:ListItem>
                                                    <asp:ListItem Value="JobSlip">JobSlip</asp:ListItem>
                                                    <asp:ListItem Value="Workshop">Workshop</asp:ListItem>
                                                    <asp:ListItem Value="Wash">Wash</asp:ListItem>
                                                    <asp:ListItem Value="Final Inspection">Final Inspection</asp:ListItem>
                                                    <asp:ListItem Value="Wheel Alignment">Wheel Alignment</asp:ListItem>
                                                     <asp:ListItem Value="VAS">VAS</asp:ListItem>
                                                </asp:DropDownList>
                                            
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="drplocnameEdit"
                                                    CssClass="clsValidator" ErrorMessage="*" ValidationGroup="ab1"></asp:RequiredFieldValidator>
                                           
                                </EditItemTemplate>
                                <FooterTemplate>
                                   
                                                <asp:DropDownList ID="drplocname" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drplocname_SelectedIndexChanged"
                                                  CssClass="form-control"  >
                                                    <asp:ListItem Value="Gate">Gate</asp:ListItem>
                                                    <asp:ListItem Value="JobSlip">JobSlip</asp:ListItem>
                                                    <asp:ListItem Value="Workshop">Workshop</asp:ListItem>
                                                    <asp:ListItem Value="Wash">Wash</asp:ListItem>
                                                    <asp:ListItem Value="Final Inspection">Final Inspection</asp:ListItem>
                                                    <asp:ListItem Value="Wheel Alignment">Wheel Alignment</asp:ListItem>
                                                     <asp:ListItem Value="VAS">VAS</asp:ListItem>
                                                </asp:DropDownList>
                                           
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="L4" runat="Server" Text='<%# Eval("LocationName") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Speedo" ItemStyle-HorizontalAlign="left" FooterStyle-HorizontalAlign="left">
                                <EditItemTemplate>
                                    <asp:CheckBox ID="chkSpeedo1" runat="server" Checked='<%# Eval("IsSpeedoBay").ToString()=="True"?true:false %>'
                                        Visible='<%# Eval("LocationName").ToString()=="Workshop"?true:false %>' />
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:CheckBox ID="chkSpeedo" runat="server" Visible="false" />
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Eval("IsSpeedoBay").ToString()=="True"?true:false %>'
                                        Visible='<%# Eval("LocationName").ToString()=="Workshop"?true:false %>' Enabled='<%# Eval("LocationName").ToString()=="Workshop"?false:true %>' />
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="left"></FooterStyle>
                                <ItemStyle HorizontalAlign="left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Process Owner">
                                <EditItemTemplate>
                                   
                                                <asp:DropDownList ID="drpEmpType" runat="server" ValidationGroup="ab1" AutoPostBack="True" CssClass="form-control"
                                                    AppendDataBoundItems="true"  SelectedValue='<%# Eval("EmployeeType").ToString()==""?"Select":Eval("EmployeeType").ToString() %>'>
                                                    <asp:ListItem Value="Select">Select</asp:ListItem>
                                                    <asp:ListItem Value="Service Advisor">Service Advisor</asp:ListItem>
                                                    <asp:ListItem Value="Final Inspector">Final Inspector</asp:ListItem>
                                                    <asp:ListItem Value="Technician">Technician</asp:ListItem>
                                                    <asp:ListItem Value="Wheel Alignment Supervisor">Wheel Alignment Supervisor</asp:ListItem>
                                                    <asp:ListItem Value="VAS Supervisor">VAS Supervisor</asp:ListItem>
                                                </asp:DropDownList>
                                    <span>&nbsp;</span>
                                        
                                </EditItemTemplate>
                                <FooterTemplate>
                                   
                                                <asp:DropDownList ID="drpEmpTypeAdd" runat="server"  CssClass="form-control">
                                                    <asp:ListItem Value="Select">Select</asp:ListItem>
                                                    <asp:ListItem Value="Service Advisor">Service Advisor</asp:ListItem>
                                                    <asp:ListItem Value="Final Inspector">Final Inspector</asp:ListItem>
                                                    <asp:ListItem Value="Technician">Technician</asp:ListItem>
                                                    <asp:ListItem Value="Wheel Alignment Supervisor">Wheel Alignment Supervisor</asp:ListItem>
                                                     <asp:ListItem Value="VAS Supervisor">VAS Supervisor</asp:ListItem>
                                                </asp:DropDownList>
                                         
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblEmpType" runat="Server" Text='<%# Eval("EmployeeType") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Device SN">
                                <EditItemTemplate>
                                    
                                                <asp:TextBox ID="txtSN" runat="Server" MaxLength="20" CssClass="form-control" Text='<%# Eval("DeviceSlNO") %>'
                                                    ValidationGroup="ab1" Width="98px"></asp:TextBox>
                                           <span>&nbsp;</span>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    
                                                <asp:TextBox ID="txtDeviceSN" runat="Server"  CssClass="form-control" onKeyPress="ValidateAlpha()" MaxLength="20" Width="100%"></asp:TextBox>
                                        
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
                                    <asp:LinkButton ID="LinkButton1" CssClass="btn" runat="server" CommandName="Add" style="background-color:#76c199;" Text="Add"></asp:LinkButton>

                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton24" runat="server" CausesValidation="False" CommandName="Edit"
                                        Text="Edit" ForeColor="#333333"></asp:LinkButton></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:Button ID="LinkButton31" CssClass="btn btn-danger" runat="server" CausesValidation="False" CommandName="Delete"
                                        Text="Delete" OnClientClick="return confirm('Are you sure you want to delete this Device Details?');"></asp:Button></ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="Silver" ForeColor="#333333" />
                        <FooterStyle BackColor="Silver" ForeColor="#333333" />
                        <HeaderStyle BackColor="Silver" ForeColor="#333333" HorizontalAlign="Left" />
                        <PagerStyle BackColor="Silver" ForeColor="#333333" HorizontalAlign="left" />
                        <RowStyle BackColor="WhiteSmoke" Font-Bold="false"/>
                        <SelectedRowStyle BackColor="WhiteSmoke" Font-Bold="false" ForeColor="#333333" />
                    </asp:GridView>
            </div>

               
    </div>
</asp:Content>