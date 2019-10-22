<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GridView.aspx.cs" Inherits="GridView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CSS/Style.css" rel="stylesheet" />
    <link href="Bootstrap/bootstrap-theme3.3.6.min.css" rel="stylesheet" />
    <link href="Bootstrap/bootstrap.min.css" rel="stylesheet" />
    <script src="Bootstrap/bootstrap.min.js"></script>
    <style type="text/css">
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="col-md-6">

            <asp:GridView ID="GridView1" runat="server" CssClass="mydatagrid" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" SortExpression="RefNo" />
                    <asp:BoundField DataField="Model" HeaderText="Model" SortExpression="EmpId" />

                    <asp:TemplateField HeaderText="MOdel Image">
                        <ItemTemplate>
                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("ModelImageUrl", "~/Images/CarImages/{0}")%>'
                                Height="30px" Width="30px" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Select">
                        <ItemTemplate>
                            <asp:DropDownList runat="server" CssClass="form-control" CausesValidation="False" CommandName="Edit">
                                <asp:ListItem>select</asp:ListItem>
                                <asp:ListItem>can Do</asp:ListItem>
                                <asp:ListItem>Can't Do</asp:ListItem>
                                <asp:ListItem>Need Help</asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField ShowHeader="False">
                        <EditItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update"
                                Text="Update" ForeColor="#333333"></asp:LinkButton>&nbsp;<asp:LinkButton ID="LinkButton21"
                                    runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" ForeColor="#333333"></asp:LinkButton>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Add" Text="Add" ValidationGroup="ab"
                                ForeColor="#333333"></asp:LinkButton>
                        </FooterTemplate>

                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit"
                                Text="Edit" ForeColor="#333333"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>

        </div>
        <div class="col-md-6">

            <asp:GridView ID="GridView2" runat="server" CssClass="mydatagrid" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="ServiceID" HeaderText="Id" />
                    <asp:BoundField DataField="ServiceType_SCode" HeaderText="Short Code" />
                    <asp:BoundField DataField="ServiceType" HeaderText="Service Type" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:DropDownList runat="server">
                                <asp:ListItem Value="-1">--Select Service Type--</asp:ListItem>
                                <asp:ListItem Value="0">Can't Do</asp:ListItem>
                                <asp:ListItem Value="1">Can Do</asp:ListItem>
                                <asp:ListItem Value="2">Need Help</asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>

        </div>
    </form>
</body>
</html>
