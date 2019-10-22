<%@ Page Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true"
    CodeFile="Bay.aspx.cs" Inherits="Bay" Title="Bay" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="js/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="js/jquery-ui-1.8.1.custom.min.js" type="text/javascript"></script>
    <script src="JS/jquery-1.3.2.js" type="text/javascript"></script>
    <script src="JS/jquery.gritter.min.js" type="text/javascript"></script>
    <script src="js/customgitter.js" type="text/javascript"></script>
    <script src="js/Tooltip.js" type="text/javascript"></script>
    <script src="js/notify-osd.js" type="text/javascript"></script>
    <link href="CSS/notify-osd.css" rel="stylesheet" type="text/css" />
    <link href="css/cupertino/jquery-ui-1.8.1.custom.css" rel="stylesheet" type="text/css" />
    <link href="css/jquery.gritter.css" rel="stylesheet" type="text/css" />
    <link href="CSS/Style.css" rel="stylesheet" />


    
    <script type="text/javascript">
        function LoadGitter(empid, serviceid, slno, title, info, imageinfo, sticktype, sticktime, classname, StickNoteType) {
            try {
                switch (StickNoteType) {
                    case 'autofade':
                        var unique_id = $.gritter.add({
                            title: title,
                            text: info,
                            image: imageinfo,
                            sticky: false,
                            time: sticktime,
                            class_name: classname
                        });
                        break;
                    case 'sticky': var unique_id = $.gritter.add({
                        title: title,
                        text: info,
                        image: imageinfo,
                        sticky: true,
                        time: sticktime,
                        class_name: classname
                    });

                        break;
                    case 'noimage':
                        $.gritter.add({
                            title: title,
                            text: info,
                            sticky: true,
                            time: sticktime
                        });
                        break;
                    case 'callback':
                        $.gritter.add({
                            title: title,
                            text: info,
                            sticky: sticktype,
                            before_open: function () {
                            },
                            after_open: function (e) {
                            },
                            before_close: function (e) {
                            },
                            after_close: function () {

                                PageMethods.GetReply(empid, slno, title, info, OnGitterLoadSuccess, OnGitterLoadFailed);
                            }
                        });
                        break;
                }

            } catch (Msg) {

            }
        }

        function OnGitterLoadSuccess(pushstring) {

            // alert("Success: \n"+pushstring);
            //alert(pushstring);
        }

        function OnGitterLoadFailed() {
            alert("Failure");
        }

        function displayGitter() {
            try {
                var unique_id = $.gritter.add({
                    title: 'title',
                    text: 'info',
                    image: 'imageinfo',
                    sticky: false,
                    time: 5000,
                    class_name: 'gritter-close'
                });

                $.gritter.add({
                    title: 'title',
                    text: 'info',
                    sticky: true,
                    time: 500
                });
                alert('Done');
            }
            catch (e) {
                alert(e);
            };
        };
    </script>
    <style type="text/css">
        .thisstyle {
            width: 40%;
        }

        .LeftGridPad {
            padding-left: 40px;
        }

        .style1 {
            background-color: Silver;
            font-family: Consolas, Georgia;
            font-size: medium;
            color: #333333;
            text-align: center;
            vertical-align: top;
            height: 27px;
        }

        .style2 {
            width: 101px;
        }

        .style3 {
            width: 43%;
        }
        .mydatagrid a {
             background-color: unset !important; 
        }
        .mydatagrid tr:nth-child(odd){
            background-color:unset !important;
        }
        select option[value="607D8B"]{
    background: #607D8B;
    color:#000;
}

select option[value="f48024"]{
    background-color:#f48024;
      color:#000;
}
select option[value="009688"]{
    background: #009688;
      color:#000;
}
select option[value="BA68C8"]{
    background:#BA68C8 ;
      color:#000;
}
select option[value="3F51B5"]{
    background: #3F51B5;
      color:#000;
}
select option[value="D32F2F"]{
    background:#D32F2F;
      color:#000;
}
select option[value="067AB4"]{
    background:#067AB4 ;
      color:#000;
}
select option[value="005238"]{
    background:#005238 ;
      color:#000;
}

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
     <asp:DropDownList ID="dd_FloorName" runat="server" Width="155px" Visible="false">
                                </asp:DropDownList>
   <%--   <asp:CheckBox ID="chk_NewBayType" runat="server" AutoPostBack="True" OnCheckedChanged="chk_NewBayType_CheckedChanged"
                                    Text="New" Visible="False"></asp:CheckBox>--%>
    <div class="container">
           <h3> Bay Details</h3>
        <div class="form-group col-md-12" style="background: #f9f9f9 !important; padding: 25px;">
            <div class="col-md-12">
                <asp:Label ID="lbl_msg0" runat="server" CssClass="clsValidator" ForeColor="Red" Width="100%"></asp:Label>
              
                <asp:Label ID="lblBayId" runat="server" Visible="False"></asp:Label>
            </div>
            <div class="col-md-12">
                &nbsp;&nbsp;</div>
            <div class="form-group col-md-3">
                <label for="pwd">bay name</label> <span style="color:red">*</span><br />
                <asp:TextBox ID="txt_BayName" runat="server" MaxLength="20" class="form-control"></asp:TextBox><%--<asp:RequiredFieldValidator
                    ID="RequiredFieldValidator14" runat="server" ControlToValidate="txt_BayName"
                    ErrorMessage="*" ValidationGroup="c"></asp:RequiredFieldValidator>--%>
            </div>

            <div class="form-group col-md-3">
                <label for="pwd">bay type</label> <span style="color:red">*</span><br />
                <asp:DropDownList ID="dd_BayType" runat="server" DataSourceID="SqlDataSource2" DataTextField="BayTypeName"
                    DataValueField="BayTypeId" class="form-control">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                    SelectCommand="GetBayType" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                <asp:TextBox ID="txt_NewBayType" runat="server" Visible="False" class="form-control"></asp:TextBox>
            </div>
            <div class="form-group col-md-2">
                <label for="pwd">color</label> <span style="color:red">*</span><br />
                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control">                      
             <asp:ListItem  Value="607D8B"></asp:ListItem>
              <asp:ListItem  Value="f48024"></asp:ListItem>
              <asp:ListItem  Value="009688"></asp:ListItem>
              <asp:ListItem  Value="BA68C8"></asp:ListItem>
              <asp:ListItem  Value="3F51B5"></asp:ListItem>
              <asp:ListItem  Value="D32F2F"></asp:ListItem>
              <asp:ListItem  Value="067AB4"></asp:ListItem>
              <asp:ListItem  Value="005238"></asp:ListItem>       
        </asp:DropDownList>
               <%-- <asp:TextBox ID="txtColor" runat="server" MaxLength="6" CssClass="form-control"></asp:TextBox>
                <cc1:ColorPickerExtender ID="txtColor_ColorPickerExtender" runat="server" Enabled="True"
                    TargetControlID="txtColor">
                </cc1:ColorPickerExtender>

                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtColor"
                    Display="Dynamic" ErrorMessage="wrong color code" ValidationExpression="([A-Fa-f0-9]{6})">* wrong color code</asp:RegularExpressionValidator>
 --%>           </div>
            <div class="form-group col-md-2">
                <label for="pwd">team lead</label><br />
                <asp:DropDownList ID="drp_tlid" runat="server" Width="100%" class="form-control"></asp:DropDownList>
            </div>

            <div class="form-group col-md-2">

                <asp:CheckBox ID="chk_toolkit" runat="server" Width="100%" Text="TOOLKIT"></asp:CheckBox>
                <asp:CheckBox ID="chk_lift" runat="server" Width="100%" Text="LIFT"></asp:CheckBox>
            </div>

            <div class="form-group col-md-12">
                <asp:Button CssClass="btn btn-success" ID="btn_Create" runat="server" OnClick="btn_Create_Click"
                    ValidationGroup="s" Text="SAVE"></asp:Button>
                <asp:Button CssClass="btn btn-primary" ID="btn_BayUpdate" runat="server" OnClick="btn_BayUpdate_Click"
                    Text="UPDATE" ValidationGroup="s"></asp:Button>
                <asp:Button CssClass="btn btn-info" ID="btn_Clear0" runat="server" OnClick="btn_Clear0_Click"
                    Text="RESET" CausesValidation="False"></asp:Button>
                 <asp:Button CssClass="btn btn-warning" ID="btn_next" runat="server" 
                    Text="NEXT" OnClick="btn_next_Click"></asp:Button>
            </div>

        </div>



        <h3>Existing Bay Details</h3>
        <div class="form-group col-md-12" style="background: #f9f9f9 !important;">

            <asp:GridView ID="GridView6" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                Font-Size="10pt" ForeColor="#333333" GridLines="None" OnPageIndexChanging="GridView6_PageIndexChanging"
                OnRowDeleting="GridView6_RowDeleting" CssClass="mydatagrid" OnRowDataBound="GridView6_RowDataBound"
                OnSelectedIndexChanged="GridView6_SelectedIndexChanged" Width="100%">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:CommandField ButtonType="Image" SelectImageUrl="https://res.cloudinary.com/deekyp5bi/image/upload/v1484224665/jcr/gridSelect.png" ShowSelectButton="True" />
                    <asp:BoundField DataField="BayId" HeaderText="Bay ID" />
                    <asp:BoundField DataField="BayName" HeaderText="BayName" />
                    <asp:BoundField DataField="Date" HeaderText="Date" />
                    <asp:BoundField DataField="BayTypeId" HeaderText="BayTypeId" />
                    <asp:BoundField DataField="FloorName" HeaderText="FloorName" />
                    <asp:BoundField DataField="Color" />
                    <asp:BoundField HeaderText="Color" />
                    
                    <asp:TemplateField HeaderText="Delete">
                     
	<ItemTemplate>
		<asp:Button ID="deleteButton" CssClass="btn btn-danger" runat="server" CommandName="Delete" Text="Delete"
OnClientClick="return confirm('Are you sure you want to delete this Bay?');" />
	</ItemTemplate>
</asp:TemplateField> 
                   <%-- <asp:CommandField ShowDeleteButton="True" />--%>
                    <asp:BoundField DataField="Active" />
                    <asp:BoundField HeaderText="TeamLead"  DataField="TeamLeadId"/>
                    <asp:BoundField HeaderText="ToolKit"  DataField="ToolKit"/>
                    <asp:BoundField HeaderText="LIFT"  DataField="LIFT"/>
                </Columns>
                <EditRowStyle BackColor="Silver" ForeColor="#333333" />
                <FooterStyle BackColor="Silver" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="Silver" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="Silver" ForeColor="#333333" HorizontalAlign="Center" />
                <RowStyle BackColor="WhiteSmoke" />
                <SelectedRowStyle BackColor="WhiteSmoke" Font-Bold="True" ForeColor="#333333" />
            </asp:GridView>
        </div>
    </div>

    
</asp:Content>
