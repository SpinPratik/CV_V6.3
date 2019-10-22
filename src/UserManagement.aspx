<%@ Page Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="UserManagement.aspx.cs" Inherits="UserManagement"
    Title="User Management" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="CSS/Style.css" rel="stylesheet" />
    <style type="text/css">
        .style1 {
            width: 89px;
            vertical-align: top;
        }

        .style3 {
            width: 121px;
            vertical-align: top;
        }

        .style4 {
            width: 121px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <div>
        <table class="fullStyle">
            <tr>
                <td align="left" valign="top" colspan="20">
                    <cc1:TabContainer ID="TabContainer1" CssClass="MyTabStyle" runat="server" ActiveTabIndex="0"
                        Width="100%" OnActiveTabChanged="TabContainer1_ActiveTabChanged" ForeColor="Black"
                        AutoPostBack="True">
                        <cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1">
                            <HeaderTemplate>
                                Add
                            </HeaderTemplate>
                            <ContentTemplate>
                                <div class="container-fluid">
                                    <div class="row">

                                        <div class="col-sm-12 col-md-6" style="overflow-y: scroll;">
                                              <h3>Add New User</h3>
                                            <div class="form" style="width: 100%; background: #f9f9f9 !important; padding: 25px;">
                                              
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td class="clmDistri" colspan="2">
                                                            <asp:Label ID="err_Message" runat="server" Width="100%" CssClass="clsValidator"></asp:Label>
                                                        </td>

                                                    </tr>
                                                    <tr class="commonFont">

                                                        <td>
                                                            <asp:Label ID="Label1" runat="server" Text="User Name "></asp:Label> <span style="color:red">*</span>
                                                            <asp:TextBox ID="txt_UserName" runat="server" MaxLength="15" CssClass="form-control"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr class="commonFont">

                                                        <td>
                                                            <asp:Label ID="Label2" runat="server" Text="Password "></asp:Label> <span style="color:red">*</span>
                                                            <asp:TextBox ID="txt_Password" runat="server" MaxLength="15" TextMode="Password"
                                                                CssClass="form-control"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr class="commonFont">

                                                        <td>
                                                            <asp:Label ID="Label3" runat="server" Text="Re-Enter Password "></asp:Label> <span style="color:red">*</span>
                                                            <asp:TextBox ID="txt_rePassword" runat="server" MaxLength="15" TextMode="Password"
                                                                CssClass="form-control"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr class="commonFont">

                                                        <td>
                                                            <asp:Label ID="Label6" runat="server" Text="User Type "></asp:Label> <span style="color:red">*</span>
                                                            <asp:DropDownList ID="cmb_UserType" runat="server"  CssClass="form-control">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="cmb_UserType"
                                                                ErrorMessage="*" ForeColor="Red" ValidationGroup="cu" CssClass="clsValidator"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr class="commonFont">

                                                        <td>
                                                            <asp:Label Text="EmployeeName" ID="lblemp" runat="server"></asp:Label> <span style="color:red">*</span>
                                                            <asp:DropDownList ID="cmb_EmployeeName" runat="server" DataSourceID="SqlDataSource9"
                                                                DataTextField="EmpName" DataValueField="EmpId" CssClass="form-control">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="cmb_EmployeeName"
                                                                CssClass="clsValidator" ErrorMessage="*" ForeColor="Red" ValidationGroup="cu"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    
                                                    <tr>

                                                        <td>
                                                            <asp:Button ID="btn_Create" runat="server" CssClass="btn btn-success" OnClick="btn_Create_Click"
                                                                Text="CREATE" ValidationGroup="cu"></asp:Button>
                                                            <asp:Button ID="btn_Clear" runat="server" CssClass="btn btn-info" OnClick="btn_Clear_Click"
                                                                Text="RESET"></asp:Button>
                                                        </td>

                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 col-md-6" style="overflow-y: scroll;">
                                            <h3>Existing User Details</h3>
                                            <asp:GridView ID="GridView3" runat="server" AllowPaging="True" AllowSorting="True"
                                                AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSource2"
                                                GridLines="None" OnPageIndexChanging="GridView3_PageIndexChanging"
                                                OnRowDataBound="GridView3_RowDataBound" OnSelectedIndexChanged="GridView3_SelectedIndexChanged" CssClass="mydatagrid" Width="100%">
                                                <Columns>
                                                    <asp:CommandField ButtonType="Image" SelectImageUrl="https://res.cloudinary.com/deekyp5bi/image/upload/v1484224665/jcr/gridSelect.png" ShowSelectButton="True" />
                                                    <asp:BoundField DataField="Sl No" HeaderText="Sl No" InsertVisible="False" ReadOnly="True" />
                                                    <asp:BoundField DataField="User Name" HeaderText="User Name" />
                                                    <asp:BoundField DataField="Type" HeaderText="Type" />
                                                </Columns>
                                            </asp:GridView>
                                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                                                SelectCommand="SELECT Users.SlNo AS [Sl No], Users.UserName AS [User Name], UserType.TypeDes AS Type FROM Users INNER JOIN UserType ON Users.TypeId = UserType.TypeId ORDER BY [Sl No]"
                                                ></asp:SqlDataSource>
                                            <asp:SqlDataSource ID="SqlDataSource9" runat="server" 
                                                SelectCommand="select EmpId,EmpName from tblEmployee where CardNo&lt;&gt;''"></asp:SqlDataSource>
                                        </div>


                                    </div>
                                </div>

                            </ContentTemplate>
                        </cc1:TabPanel>
                        <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2">
                            <HeaderTemplate>
                                Delete
                            </HeaderTemplate>
                            <ContentTemplate>
                                <div class="container-fluid">
                                    <div class="row">

                                        <div class="col-sm-12 col-md-6" style="overflow-y: scroll;">
                                            <h3>Delete User</h3>
                                            <div class="form" style="width: 100%; background: #f9f9f9 !important; padding: 25px;">
                                                
                                                <table style="width:100%">
                                                    <tr>
                                                                    <td>
                                                                        <asp:Label ID="lbl_DelErr" runat="server" ForeColor="Red"></asp:Label>
                                                                    </td>

                                                                </tr><tr><td>&nbsp;</td></tr>
                                                    <tr>
                                                                    <td>
                                                                          <asp:Label ID="Label12" runat="server" Text="Select User "></asp:Label>
                                                                        <asp:DropDownList ID="cmb_DelUserList" runat="server" DataSourceID="SqlDataSource6"
                                                                            DataTextField="UserName" DataValueField="SlNo" CssClass="form-control">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                
                                                    <tr><td>&nbsp;</td></tr>
                                                                <tr>
                                                                   
                                                                    <td style="float:right">
                                                                        <asp:Button CssClass="btn btn-danger" ID="btn_Delete" runat="server" OnClientClick="return confirm('Are you sure you want to delete this User?');" OnClick="btn_Delete_Click"
                                                                            Text="DELETE" />
                                                                    </td>
                                                                </tr>
                                                </table>

                                                </div></div>
                                        <div class="col-sm-12 col-md-6" style="overflow-y: scroll;">
                                            <h3>User Details</h3>
                                             <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AllowSorting="True"
                                                                            AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSource7"
                                                                            GridLines="None" OnPageIndexChanging="GridView2_PageIndexChanging" OnRowDataBound="GridView2_RowDataBound"
                                                                            OnSelectedIndexChanged="GridView2_SelectedIndexChanged" CssClass="mydatagrid" Width="100%">
                                                                            <Columns>
                                                                                <asp:CommandField ButtonType="Image" SelectImageUrl="https://res.cloudinary.com/deekyp5bi/image/upload/v1484224665/jcr/gridSelect.png" ShowSelectButton="True" />
                                                                                <asp:BoundField DataField="Sl No" HeaderText="Sl No" InsertVisible="False" ReadOnly="True" />
                                                                                <asp:BoundField DataField="User Name" HeaderText="User Name" />
                                                                                <asp:BoundField DataField="Type" HeaderText="Type" />
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                        <asp:SqlDataSource ID="SqlDataSource7" runat="server" 
                                                                            SelectCommand="SELECT Users.SlNo AS [Sl No], Users.UserName AS [User Name], UserType.TypeDes AS Type FROM Users INNER JOIN UserType ON Users.TypeId = UserType.TypeId"></asp:SqlDataSource>
                                                                        <asp:SqlDataSource ID="SqlDataSource6" runat="server" 
                                                                            SelectCommand="SELECT [SlNo], [UserName] FROM [Users]"></asp:SqlDataSource>

                                            </div>

                                    </div></div>

                                               
                            </ContentTemplate>
                        </cc1:TabPanel>
                        <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="TabPanel3">
                            <HeaderTemplate>
                                Change Profile
                            </HeaderTemplate>
                            <ContentTemplate>
                                <div class="container-fluid">
                                    <div class="row">

                                        <div class="col-sm-12 col-md-4" style="overflow-y: scroll;margin-bottom:20px;">
                                           <h3>Change Password</h3>
                                            <div class="form" style="width: 100%; background: #f9f9f9 !important; padding: 25px;">
                                               
                                                <table style="width:100%">
                                                      <tr>
                                                    <td class="clmDistri" colspan="2">
                                                        <asp:Label ID="err_CMessage" runat="server" ForeColor="Red"></asp:Label>
                                                    </td>
                                                </tr><tr><td>&nbsp;</td></tr>
                                                    <tr class="commonFont">
                                                   
                                                    <td>
                                                        <asp:Label ID="Label7" runat="server" Text="User Name "></asp:Label>
                                                        <asp:DropDownList ID="cmb_uname" runat="server" DataSourceID="SqlDataSource5" DataTextField="UserName"
                                                            DataValueField="SlNo" CssClass="form-control">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="cmb_uname"
                                                            ErrorMessage="*" ForeColor="Red" ValidationGroup="ch" CssClass="clsValidator"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr><tr><td>&nbsp;</td></tr>
                                                <tr class="commonFont">
                                                    
                                                    <td>
                                                        <asp:Label ID="Label11" runat="server" Text="New Password "></asp:Label> <span style="color:red">*</span>
                                                        <asp:TextBox ID="txt_NewPass" runat="server" ToolTip="Enter New Password. !" CssClass="form-control"
                                                            TextMode="Password" MaxLength="15"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                     <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txt_NewPass"
                                                            ErrorMessage="*" ForeColor="Red" ValidationGroup="ch" CssClass="clsValidator"></asp:RequiredFieldValidator>
                          --%>                          </td>
                                                </tr><tr><td>&nbsp;</td></tr>
                                                <tr class="style1" class="commonFont">
                                                   
                                                    <td>
                                                         <asp:Label ID="Label9" runat="server" Text="Re-Enter Password "></asp:Label> <span style="color:red">*</span>
                                                        <asp:TextBox ID="txt_RePass" runat="server" ToolTip="Re-Enter New Password. !" CssClass="form-control"
                                                            TextMode="Password" MaxLength="15"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                       
                                                     <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txt_RePass"
                                                            ErrorMessage="*" ValidationGroup="ch" CssClass="clsValidator"></asp:RequiredFieldValidator>--%>
                                                    </td>
                                                </tr><tr><td>&nbsp;</td></tr>
                                               
                                                <tr>
                                                  
                                                    <td>
                                                        <asp:Button CssClass="btn btn-success" ID="btn_Change" runat="server" ImageUrl="~/imgButton/CHANGE.jpg"
                                                            OnClick="btn_Change_Click" Text="CHANGE" ToolTip="Click here to change password. !"
                                                            ValidationGroup="ch"></asp:Button>
                                                        <asp:Button CssClass="btn btn-info" ID="btn_CPClear" runat="server" ImageUrl="~/imgButton/CLEAR.jpg"
                                                            OnClick="btn_CPClear_Click" ToolTip="Click here to clear all details. !" Text="RESET"></asp:Button>
                                                    </td>
                                                    
                                                </tr></table></div></div>

                                            <div class="col-sm-12 col-md-4" style="overflow-y: scroll;margin-bottom:20px;">
                                               <h3> Change User Type</h3>
                                                 <div class="form" style="width: 100%; background: #f9f9f9 !important; padding: 25px;">
                                                   
                                                <table style="width:100%">
                                                   <tr>
                                                    <td align="center" colspan="2">
                                                        <asp:Label ID="err_TMessage" runat="server" ForeColor="Red"></asp:Label>
                                                    </td>
                                                </tr><tr><td>&nbsp;</td></tr>
                                                <tr class="commonFont">
                                                    
                                                    <td>
                                                        <asp:Label ID="Label13" runat="server" Text="User Name "></asp:Label>
                                                        <asp:DropDownList ID="cmb_Tuname" runat="server" DataSourceID="SqlDataSource5" DataTextField="UserName"
                                                            DataValueField="SlNo" CssClass="form-control">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>&nbsp;&nbsp;
                                                    </td>
                                                </tr><tr><td>&nbsp;</td></tr>
                                                <tr class="commonFont">
                                                    
                                                    <td>
                                                        <asp:Label ID="Label10" runat="server" Text="User Type "></asp:Label>
                                                        <asp:DropDownList ID="cmb_Type" runat="server" DataSourceID="SqlDataSource3" DataTextField="TypeDes"
                                                            DataValueField="TypeId" ToolTip="Select User Type. !" CssClass="form-control">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="cmb_Type"
                                                            CssClass="clsValidator" ErrorMessage="*" ValidationGroup="t"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr><tr><td>&nbsp;</td></tr>
                                                <tr class="commonFont">
                                                    
                                                    <td>
                                                        <asp:Label ID="Label4" runat="server" Text="Employee Name "></asp:Label>
                                                        <asp:DropDownList ID="cmb_EName" runat="server" CssClass="form-control" DataSourceID="SqlDataSource10"
                                                            DataTextField="EmpName" DataValueField="EmpId">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="cmb_EName"
                                                            CssClass="clsValidator" ErrorMessage="*" ValidationGroup="t"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr><tr><td>&nbsp;</td></tr>
                                                
                                                <tr>
                                                    
                                                    <td>
                                                        <asp:Button CssClass="btn btn-success" ID="btn_TChange" runat="server" ImageUrl="~/imgButton/CHANGE.jpg"
                                                            OnClick="btn_TChange_Click" Text="CHANGE" ToolTip="Click here to change user type. !"
                                                            ValidationGroup="t"></asp:Button>
                                                        <asp:Button CssClass="btn btn-info" ID="btn_TClear" runat="server" ImageUrl="~/imgButton/CLEAR.jpg"
                                                            OnClick="btn_TClear_Click" ToolTip="Click here to clear all details. !" Text="RESET"></asp:Button>
                                                    </td>
                                                    <td>&nbsp;&nbsp;
                                                    </td>
                                                </tr>

                                                </table></div></div>
                                        <div class="col-sm-12 col-md-4" style="overflow-y: scroll;margin-bottom:20px;">
                                             <h3>Existing User Details</h3>
                                             <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                                                            AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSource8"
                                                            GridLines="None" Width="100%" CssClass="mydatagrid" OnPageIndexChanging="GridView1_PageIndexChanging"
                                                            OnRowDataBound="GridView1_RowDataBound" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                                            <Columns>
                                                                <asp:CommandField ButtonType="Image" SelectImageUrl="https://res.cloudinary.com/deekyp5bi/image/upload/v1484224665/jcr/gridSelect.png" ShowSelectButton="True" />
                                                                <asp:BoundField DataField="Sl No" HeaderText="Sl No" InsertVisible="False" ReadOnly="True" />
                                                                <asp:BoundField DataField="User Name" HeaderText="User Name" />
                                                                <asp:BoundField DataField="Type" HeaderText="Type" />
                                                                <asp:BoundField DataField="TypeId" HeaderText="TypeId" InsertVisible="False"></asp:BoundField>
                                                            </Columns>
                                                        </asp:GridView>
                                                        <asp:SqlDataSource ID="SqlDataSource8" runat="server" 
                                                            SelectCommand="SELECT Users.SlNo AS [Sl No], Users.UserName AS [User Name], UserType.TypeDes AS Type, UserType.TypeId FROM Users INNER JOIN UserType ON Users.TypeId = UserType.TypeId"></asp:SqlDataSource>
                                                        <asp:SqlDataSource ID="SqlDataSource" runat="server"></asp:SqlDataSource>
                                                        <asp:SqlDataSource ID="SqlDataSource5" runat="server" 
                                                            SelectCommand="SELECT [SlNo], [UserName] FROM [Users]"></asp:SqlDataSource>
                                                        <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                                                            SelectCommand="SELECT [TypeId], [TypeDes] FROM [UserType]"></asp:SqlDataSource>
                                                        <asp:SqlDataSource ID="SqlDataSource10" runat="server" 
                                                            SelectCommand="select EmpId,EmpName from tblEmployee where CardNo&lt;&gt;''"></asp:SqlDataSource>
                                                        
                                            </div>

                                    </div></div>

                               
                            </ContentTemplate>
                        </cc1:TabPanel>
                    </cc1:TabContainer>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
