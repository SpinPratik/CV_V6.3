<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeFile="Employee.aspx.cs" Inherits="Employee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="CSS/Style.css" rel="stylesheet" />
    <style>
        .space {
            /*margin-left:5px;
            margin-right:5px;*/
        }

        .mydatagrid th {
            padding: 6px;
        }
      
        .ScsMsg {
            background-color: #5cb85c;
            text-align: left !important;
            border-radius: 2px;
            margin-bottom: 10px;
            color: white !important;
            margin-top: 10px;
            padding-left: 12px;
            -webkit-transition: 2s; /* For Safari 3.1 to 6.0 */
            transition: 2s;
            padding-right: 10px !important;
            padding-left: 10px !important;
            padding-top: 5px !important;
        }

        .ErrMsg {
            text-align: left !important;
            background-color: rgba(248, 0, 0, 0.63);
            color: white !important;
            border-radius: 2px;
            margin-bottom: 10px;
            margin-top: 10px;
            -webkit-transition: 2s; /* For Safari 3.1 to 6.0 */
            transition: 2s;
            padding-right: 10px !important;
            padding-left: 10px !important;
            padding-top: 5px !important;
        }

        .reset {
            background-color: transparent;
            -webkit-transition: 2s; /* For Safari 3.1 to 6.0 */
            transition: 2s;
        }

        /*checkbox list*/
        	.checkboxlist input {
        font: inherit;
        font-size: 0.875em; /* 14px / 16px */
        color: #494949;
        float:left;
        margin-top:2px;
        margin-bottom:18px;
	}
	.checkboxlist label {
        font: inherit;
        font-size: 0.875em; /* 14px / 16px */
        color: #494949;
        position:relative;
        margin-top:2px;
        display:block;
	}
  /*.middle-block{
      width:100%;
      overflow-x:scroll;
   }*/
        .thisstyle {
            width: 40%;
        }

        .LeftGridPad {
            padding-left: 40px;
        }

        .mydatagrid span {
            background-color: unset !important;
            color: #000;
            border: unset !important;
            width: unset !important;
            float: left;
        }

        .mydatagrid a {
            background-color: unset !important;
        }

        .mydatagrid a, .mydatagrid span {
            width: unset !important;
            float: left;
        }
          .search{
                        float:left;
                        background-color:rgba(0,0,0,0.1);
                        width:100%;
                        height:50px;
                        padding:5px;
                       margin-bottom:4px;
                    }
          .t1{
                        width:12%;
                        float:left;
                           padding:5px;
                       
                    }
                    .t2{
                        width:12%;
                            float:left;
                        padding:5px;
                    }
                    .t3{
                        width:12%;
                            float:left;
                        padding:5px;
                    }
                    .t4{
                        width:12%;
                            float:left;
                        padding:5px;
                    }
                    .t5{
                        width:12%;
                            float:left;
                        padding:5px;
                    }
                    .t6{
                        /*width:12%;*/
                            float:left;
                   padding:5px;
                    }
                    .t7{
                        /*width:9%;*/
                           float: left;
                   padding:5px;
                    }
                    .t8 {
                        /*width: 9%;*/
                        float: left;
                        padding: 5px;
                    }
                   .t9{
                        /*width:9%;*/
                            float:left;
                   padding:5px;
                   
                    }
                  
    </style>
  
    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
    </script>

<script type="text/javascript" language="javascript">
        function ValidateAlpha() {
            var keyCode = window.event.keyCode;
            if ((keyCode < 65 || keyCode > 90) && (keyCode < 97 || keyCode > 123) && keyCode != 32) {
                window.event.returnValue = false;
                alert("Special characters and numeric values are not allowed");
            }
        }
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>


    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
            <div class="container-fluid">
                <div class="col-md-12">
                    <asp:Label ID="lblmsg1" runat="server" Width="50%"></asp:Label><br />
                </div>
              <%--  <div class="col-md-offset-5 col-md-6 middle-block">
                    <asp:Button runat="server" ID="btn_add" Text="ADD NEW" CssClass="btn btn-success" OnClick="btn_add_Click" Width="100px" />
                    <asp:Button runat="server" ID="modify" Text="MODIFY" CssClass="btn btn-default" OnClick="modify_Click" Width="100px"/>
                    <asp:Button runat="server" ID="btn_delete" Text="UNASSIGN" Width="100px" OnClientClick="return confirm('Are you sure you want to unassign this Employee?');" CssClass="btn btn-danger" OnClick="btn_delete_Click" />
               <br /> </div>
                <br />
                <br />
                <br />--%>

                    <div class="search">

                    <div class="t1"> <asp:TextBox ID="txtEmpname" onKeyPress="ValidateAlpha()" ToolTip="Enter Name" CssClass="form-control" placeholder="Emp Name" runat="server" ></asp:TextBox></div>
                     <div class="t2"><asp:DropDownList ID="drp_Emptype1"  runat="server"  CssClass="form-control" ></asp:DropDownList></div>
                    <%-- <div class="t3"> <asp:DropDownList ID="drp_level1" Visible="false" runat="server"  CssClass="form-control" >
                          <asp:ListItem Value="0">Select Level</asp:ListItem>
                                               <asp:ListItem Value="1">L0</asp:ListItem>
                                              <asp:ListItem Value="2">L0M</asp:ListItem>
                                              <asp:ListItem Value="3">L1</asp:ListItem>
                          <asp:ListItem Value="3">ETEK</asp:ListItem>
                                      </asp:DropDownList></div>--%>
                     <div class="t4"> <asp:TextBox ID="txt_empcode1" CssClass="form-control"  placeholder="Emp Code" runat="server"></asp:TextBox></div>
                     <div class="t5"> <asp:TextBox ID="TextBox5" CssClass="form-control" placeholder="Emp Id" runat="server"></asp:TextBox> </div>
                     <div class="t6"><asp:Button ID="btn_search" runat="server" CssClass="btn btn-info" OnClick="btn_search_Click"  Text="Search" /></div>
                    <div class="t7"> <asp:Button ID="btn_reset1" runat="server" CssClass="btn btn-warning" OnClick="btn_reset1_Click"  Text="Reset" /></div>
                     <div class="t7">    <asp:Button runat="server" ID="btn_add" Text="Add New" CssClass="btn btn-success" OnClick="btn_add_Click"  /></div>
                
                      <div class="t8"> <asp:Button runat="server" ID="modify" Text="Modify" CssClass="btn btn-default" OnClick="modify_Click" /></div>
                     <div class="t9"> <asp:Button runat="server" ID="btn_delete" Text="Unassign" Width="100px" OnClientClick= "return confirm('Are you sure you want to unassign this Employee?');" CssClass="btn btn-danger" OnClick="btn_delete_Click" /> </div>
                 
                </div>
                <div class="col-md-12 middle-block">
                    <asp:GridView ID="grd_emp" runat="server" AllowPaging="True" AutoGenerateColumns="true"
                        Font-Size="10pt" ForeColor="#333333" GridLines="None"
                        CssClass="mydatagrid" OnRowDataBound="grd_emp_RowDataBound"
                        OnSelectedIndexChanged="grd_emp_SelectedIndexChanged" Width="100%" PageSize="10" OnPageIndexChanging="grd_emp_PageIndexChanging1">
                        <Columns>
                            <asp:CommandField ButtonType="Image" SelectImageUrl="https://res.cloudinary.com/deekyp5bi/image/upload/v1484224665/jcr/gridSelect.png" ShowSelectButton="True" />
                        </Columns>
                        <SelectedRowStyle ForeColor="#337ab7" />
                    </asp:GridView>
                </div>
            </div>
        </asp:View>
        <asp:View ID="View2" runat="server">
            <div class="container">
                <h3>Employee Details</h3>
                <div class="col-md-12 middle-block" style="background: #f9f9f9 !important; padding: 25px;">
                    <div class="form-group col-md-12">
                        <asp:Label ID="lblmsg" Width="100%" runat="server"></asp:Label>
                        <asp:Label ID="lblempid" runat="server" Visible="False" />
                         <asp:Label ID="lblsrvcid" runat="server" Visible="False" />
                       <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                            ControlToValidate="txt_mobNum" ErrorMessage="Invalid Mobile Number"
                            ValidationExpression="[0-9]{10}" Visible="true" style="visibility: visible !important;"></asp:RegularExpressionValidator>--%>
                    </div>
                    <div class="col-md-12">
                        <div class="form-group col-md-4">
                            <label for="email">Employee Name</label> <span style="color:red">*</span><br />
                            <asp:TextBox ID="txt_empname" runat="server" Width="100%" class="form-control" MaxLength="30" onKeyPress="ValidateAlpha()" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="form-group col-md-4">
                            <asp:UpdatePanel runat="server"><ContentTemplate>
                            <label>EMPLOYEE IDENTIFICATION #</label> <span style="color:red">*</span>
                              <asp:CheckBox runat="server" ID="chk_EmpId" Text="" OnCheckedChanged="chk_EmpId_CheckedChanged" AutoPostBack="true" />
                            <div class="form-inline">
                                <asp:DropDownList ID="cmbCardNo" runat="server" class="form-control" Width="100%">
                                </asp:DropDownList>
                            </div>
                                 </ContentTemplate></asp:UpdatePanel>
                        </div>
                        <asp:Panel class="form-group col-md-2" runat="server" ID="existing_div">
                            <label>Existing TagNO</label>
                            <asp:TextBox ID="txt_existTagno" runat="server" class="form-control" Enabled="false" onkeypress="return blockChars()" autocomplete="off"></asp:TextBox>
                        </asp:Panel>
                        <asp:Panel class="form-group col-md-2" runat="server" ID="pnl_empCode">
                            <label for="pwd">Employee code</label> <span style="color:red">*</span><br />
                            <asp:TextBox ID="txt_EmpCode" runat="server" Width="100%" MaxLength="15" class="form-control"></asp:TextBox>

                        </asp:Panel>
                    </div>
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>

                            <div class="col-md-12">
                                <div class="form-group col-md-4">
                                    <label for="pwd">Employee type</label><br />
                                    <asp:DropDownList ID="drp_emptype" runat="server" Width="100%" class="form-control"  OnSelectedIndexChanged="drp_emptype_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>

                                </div>
                                <div class="form-group col-md-4">
                                    <label for="pwd">MOBILE NUMBER</label><br />
                                    <asp:TextBox ID="txt_mobNum" runat="server" Width="100%" class="form-control" MaxLength="10" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                </div>


                                <div class="form-group col-md-4">
                                    <label for="pwd">CRM USER ID</label><br />
                                    <asp:TextBox ID="txt_crmid"  runat="server" Width="100%" MaxLength="15" class="form-control"></asp:TextBox>
                                </div>


                                <asp:Panel ID="panel1" runat="server">
                                 <%--   <div class="form-group col-md-12">
                                        <label for="pwd">MODEL</label><br />
                                        <asp:CheckBoxList runat="server" ID="chk_model" RepeatDirection="Horizontal" RepeatColumns="6" Style="width: 100%" CssClass="checkboxlist">
                                        </asp:CheckBoxList>
                                    </div>--%>
                                   <%-- <div class="form-group col-md-12">
                                        <label for="pwd">SERVICE TYPE</label><br />
                                        <asp:CheckBoxList runat="server" ID="chk_srvcType" CssClass="checkboxlist" RepeatDirection="Horizontal" Style="width: 100%">
                                        </asp:CheckBoxList>
                                    </div>--%>
                                    <div class="form-group col-md-6">
                                        <label for="pwd">TRAINING</label><br />
                                        <asp:DropDownList ID="txt_training" runat="server" Width="100%"  class="form-control">
                                            <asp:ListItem Value="1">L1</asp:ListItem>
                                             <asp:ListItem Value="2">L2</asp:ListItem>
                                             <asp:ListItem Value="3">L3</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                  <%--   <div class="form-group col-md-4">
                                        <label for="pwd">LEVEL</label><br />
                                        <asp:DropDownList ID="txt_lvl" runat="server" Width="100%"  class="form-control">
                                            <asp:ListItem Value="1">L1</asp:ListItem>
                                              <asp:ListItem Value="2">L1</asp:ListItem>
                                              <asp:ListItem Value="3">L1</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>--%>
                                    <div class="form-group col-md-6">
                                        <label for="pwd">TEAM LEAD</label><br />
                                        <asp:DropDownList ID="drp_tlid" runat="server" Width="100%" class="form-control"></asp:DropDownList><br />

                                    </div>
                                      <div class="form-group col-md-6">

            <asp:GridView ID="GridView1" runat="server" CssClass="mydatagrid" PageSize="5" PageIndex="1" AutoGenerateColumns="False"
                OnSelectedIndexChanged="GridView1_SelectedIndexChanged" GridLines="None" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound"  
                 >
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" SortExpression="RefNo" />
                    <asp:BoundField DataField="Model" HeaderText="Model" SortExpression="EmpId" />

                    <asp:TemplateField HeaderText="MOdel Image">
                        <ItemTemplate>
                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("ModelImageUrl", "https://res.cloudinary.com/deekyp5bi/image/upload/v1484138547/vehicles/{0}")%>'
                                Height="30px" Width="30px" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Select">
                        <ItemTemplate>
                            <asp:DropDownList runat="server" ID="ddl_AddModel" CssClass="form-control" CausesValidation="False" CommandName="Edit" SelectedValue='<%# Bind("Status") %>'>
                                   <asp:ListItem Value="-1" Text="--Select Vehicle Model--"></asp:ListItem>
                                <asp:ListItem Value="0" Text="Can't Do"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Can Do"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Need Help"></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>

                 

                </Columns>
            </asp:GridView>

        </div>

                                    <div class="form-group col-md-6">

            <asp:GridView ID="GridView2" runat="server" GridLines="None" CssClass="mydatagrid" PageSize="7" OnRowDataBound="GridView2_RowDataBound"
                OnSelectedIndexChanged="GridView2_SelectedIndexChanged" AutoGenerateColumns="False" OnPageIndexChanging="GridView2_PageIndexChanging">
                <Columns>
                    <asp:BoundField DataField="ServiceID" HeaderText="Id" />
                    <asp:BoundField DataField="ServiceType_SCode" HeaderText="Short Code" />
                    <asp:BoundField DataField="ServiceType" HeaderText="Service Type" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:DropDownList runat="server" ID="ddl_AddService"  CssClass="form-control" CausesValidation="False" CommandName="Edit" SelectedValue='<%# Bind("Status") %>'>
                                <asp:ListItem Value="-1" Text="--Select Service Type--"></asp:ListItem>
                                <asp:ListItem Value="0" Text="Can't Do"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Can Do"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Need Help"></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>

        </div>
                                </asp:Panel>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <div class="form-group col-md-offset-7 col-md-5">

                        <asp:Button ID="btn_submit" CssClass="btn btn-success" runat="server" Text="SUBMIT" OnClick="btn_submit_Click" />
                        <asp:Button ID="btn_reset" CssClass="btn btn-info" runat="server" Text="RESET" OnClick="btn_reset_Click" />

                        <asp:Button ID="btn_update" CssClass="btn btn-primary" runat="server" Text="UPDATE" OnClick="btn_update_Click" />
                        <%-- <asp:Button ID="btn_delete" CssClass="btn btn-danger" runat="server" Text="DELETE" />--%>
                        <asp:Button ID="btn_close" CssClass="btn btn-warning" runat="server" Text="CLOSE" OnClick="btn_close_Click" />
                        <br />
                        <br />
                    </div>


                </div>
               </div>
                
                 

            
        </asp:View>
    </asp:MultiView>



</asp:Content>

