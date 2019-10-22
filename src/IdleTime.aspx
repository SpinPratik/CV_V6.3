<%@ Page Language="C#" MasterPageFile="~/admin.master" AutoEventWireup="true"
    CodeFile="IdleTime.aspx.cs" Inherits="IdleTime" Title="IdleTime" %>

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
        .mydatagrid a, .mydatagrid span{
            width:unset !important;
        }
        .mydatagrid span{
            background-color:unset !important;
            color: #000;
            border: unset !important;
        }
        .mydatagrid tr:nth-child(odd){
            background-color:white !important;
        }
        .mydatagrid th{
            background-color:#f9f9f9 !important;
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <div style="height: 565px; margin-bottom: 30px; vertical-align: middle;">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                    <ProgressTemplate>
                        <asp:Image ID="Image4" runat="server" ImageUrl="~/images/ajax-loader.gif" /></ProgressTemplate>
                </asp:UpdateProgress>
              
                <div class="container">
                    <div class="row">
                        <%--<div class="col-sm-12 col-md-4">
                            <h3>Process Capacity</h3>
                             <asp:Label ID="lblCapacityMsg" runat="server" CssClass="clsValidator" ForeColor="Red"></asp:Label>
                           
                              <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1"
                            ShowHeader="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%" CssClass="mydatagrid" >
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="EmployeeType" HeaderText="EmployeeType" SortExpression="EmployeeType" />
                                <asp:TemplateField HeaderText="Capacity" SortExpression="Capacity">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" Text='<%# Bind("Capacity") %>'></asp:TextBox></EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" Text='<%# Bind("Capacity") %>'></asp:TextBox></ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle BackColor="WhiteSmoke" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="WhiteSmoke" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ProTRAC_ConnectionString %>"
                            SelectCommand="SELECT tblEmployeeCapacity.* FROM tblEmployeeCapacity"></asp:SqlDataSource>
                            <table class="mydatagrid" style="width:100%">
                            <tr>
                                <td align="right">
                                    <asp:Button CssClass="btn btn-success" ID="btnSavePCA" runat="server" OnClick="btnSavePCA_Click"
                                        Text="SAVE" />
                                     <asp:ImageButton ID="btnRefreshCapacity" runat="server" CssClass="btn btn-default" ImageUrl="~/images/refresh.png"
                                        OnClick="btnRefreshCapacity_Click" AlternateText="Refresh" />
                                </td>
                                
                            </tr>
                        </table>
                        </div>--%>

                        <div class="col-sm-12 col-md-12">
                            <h3>Process Management</h3>
                             <div class="col-md-12">
                    <asp:Label ID="lbIdleTimeMsg" runat="server" Width="100%"></asp:Label>
                </div>
                <asp:GridView ID="grvIdelTime" runat="server" AutoGenerateColumns="False" CellPadding="4"
                     ForeColor="#333333" GridLines="None" DataKeyNames="ProcessName"
                    OnRowCommand="grvIdelTime_RowCommand" CssClass="mydatagrid" Width="100%">
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <Columns>
                        <asp:BoundField DataField="ProcessName" HeaderText="Process Name" SortExpression="ProcessName" />
                      <asp:TemplateField HeaderText="Process Time" SortExpression="EscalationTime">
                            <ItemTemplate>
                                <asp:TextBox ID="txtprocessTime" CssClass="form-control" runat="server" MaxLength="4" Text='<%# Bind("ProcessTime") %>'  Enabled='<%# Eval("Status").ToString()=="1"?true:false %>' onkeypress="return isNumberKey(event)"/>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Escalation Time" SortExpression="EscalationTime">
                            <ItemTemplate>
                                <asp:TextBox ID="txtEscalationTime" CssClass="form-control" runat="server" MaxLength="4" Text='<%# Bind("EscalationTime") %>' Enabled='<%# Eval("Status").ToString()=="2"?true:false %>' onkeypress="return isNumberKey(event)"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:ButtonField ControlStyle-CssClass="btn btn-default" Text="Update" CommandName="Update_EscalationTime"  />
                    </Columns>
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="Silver" Font-Bold="True" ForeColor="#333333" />
                    <EditRowStyle BackColor="Silver" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                </asp:GridView>
               <%-- <asp:SqlDataSource ID="DataSourceI" runat="server" ConnectionString="<%$ ConnectionStrings:ProTRAC_ConnectionString %>"
                    SelectCommand="SELECT [ProcessName]+' '+[EntryType] [ProcessName], [EscalationTime] FROM [tblEscalation]">
                </asp:SqlDataSource>--%>
                        </div>


                         <%--<div class="col-sm-12 col-md-4">
                             <h3>Standard Time</h3>
                             <asp:Label ID="lblStandardTimeMsg" runat="server" CssClass="clsValidator" ForeColor="Red"></asp:Label>
                        <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" DataSourceID="DS_StandardTime"
                            ShowHeader="False" CssClass="mydatagrid" DataKeyNames="ProcessId" CellPadding="4" ForeColor="#333333"
                            GridLines="None" OnRowDataBound="GridView5_RowDataBound" PageSize="12" Width="100%" >
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="ProcessId" HeaderText="ProcessId" InsertVisible="False"
                                    ReadOnly="True" SortExpression="ProcessId" />
                                <asp:BoundField DataField="ProcessName" HeaderText="ProcessName" SortExpression="ProcessName" />
                                <asp:TemplateField HeaderText="ProcessTime" SortExpression="ProcessTime">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ProcessTime") %>' MaxLength="4"></asp:TextBox></EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="TextBox2" placeholder="IN MINS" runat="server" CssClass="form-control" Text='<%# Bind("ProcessTime", "{0:D}") %>'
                                             MaxLength="4"></asp:TextBox></ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle BackColor="WhiteSmoke" />
                            <FooterStyle BackColor="Silver" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="Silver" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="WhiteSmoke" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="WhiteSmoke" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        </asp:GridView>
                        <asp:SqlDataSource ID="DS_StandardTime" runat="server" ConnectionString="<%$ ConnectionStrings:ProTRAC_ConnectionString %>"
                            SelectCommand="SELECT ProcessId, ProcessName, ProcessTime FROM tblProcessList WHERE (ProcessName NOT IN ('GATE')) ORDER BY ProcessDefaultOrder">
                        </asp:SqlDataSource>
                              <table class="mydatagrid" style="width:100%;float:right">
                            <tr>
                                <td align="right">
                                    <asp:Button CssClass="btn btn-success" ID="btnSaveStandardTime" runat="server" ImageUrl="~/imgButton/SAVE.jpg"
                                        Text="SAVE" OnClick="btnSaveStandardTime_Click" />

                                    <asp:ImageButton ID="btnRefreshStdTime" runat="server" CssClass="btn btn-default" ImageUrl="~/images/refresh.png"
                                        OnClick="btnRefreshStdTime_Click" AlternateText="Refresh" />
                                </td>
                               
                            </tr>
                        </table>
                        </div>--%>
                         
                    </div>
                </div>
               
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>