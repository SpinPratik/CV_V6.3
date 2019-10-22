<%@ Page Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true"
    CodeFile="RFIDCards.aspx.cs" Inherits="RFIDCards" Title="RFIDCards" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="CSS/Style.css" rel="stylesheet" />
    <style type="text/css">
       
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
         <h3>RFID Management</h3>
        <div >
        <div class="col-md-12" style="width:100%;background: #f9f9f9 !important;padding: 25px;" >
            <div class="form-group col-md-12">
              <asp:Label ID="lblRFIDMsg" runat="server" CssClass="clsValidator" ForeColor="Red" Width="100%"></asp:Label>
                </div>
              <div class="form-group col-md-4">
                    <label for="pwd">From Serial#</label> <span style="color:red">*</span> <br />
                    <asp:TextBox ID="txtSlNoFrm" runat="server" CssClass="form-control"></asp:TextBox><cc1:FilteredTextBoxExtender
                                    ID="txtSlNoFrm_FilteredTextBoxExtender" runat="server" Enabled="True" TargetControlID="txtSlNoFrm"
                                    FilterType="Numbers">
                                </cc1:FilteredTextBoxExtender>  </div>
                <div class="form-group col-md-4">
                    <label for="pwd">To Serial#</label> <span style="color:red">*</span><br />
                     <asp:TextBox ID="txtSlNoTo" runat="server" CssClass="form-control"></asp:TextBox><cc1:FilteredTextBoxExtender
                                    ID="txtSlNoTo_FilteredTextBoxExtender" runat="server" Enabled="True" TargetControlID="txtSlNoTo"
                                    FilterType="Numbers">
                                </cc1:FilteredTextBoxExtender>
                </div>
                <div class="form-group col-md-4">
                    <label for="pwd">CardType</label> <span style="color:red">*</span><br />
                    <asp:DropDownList ID="ddlCardType" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="99">--Select--</asp:ListItem>
                                    <asp:ListItem Value="0">Vehicle</asp:ListItem>
                                    <asp:ListItem Value="1">Employee</asp:ListItem>
                                  <%--  <asp:ListItem Value="2">Admin</asp:ListItem>--%>
                                </asp:DropDownList>
                 
                </div>
            <div class="col-md-offset-8 col-md-4">
                 <asp:Button CssClass="btn btn-success" ID="btnGenerate" runat="server" Text="GENERATE" Width="100px" OnClick="btnGenerate_Click" />
                                     
                                <asp:Button CssClass="btn btn-info" ID="btnClearRFID" runat="server" OnClick="btnClearRFID_Click"
                                                Text="RESET" />
                <asp:Button CssClass="btn btn-warning" ID="btn_next" runat="server" OnClick="btn_next_Click"
                                                Text="NEXT" />
            </div>
        </div>
            </div>
        <br />
        <div class="row">
          
            <div class="col-sm-12 col-md-12">
                 <h3>RFID List</h3>
                <asp:GridView ID="grdGetRFIDList" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" CellPadding="4" CssClass="mydatagrid" GridLines="None" Width="100%"
                    OnPageIndexChanging="grdGetRFIDList_PageIndexChanging">
                    <FooterStyle Font-Bold="True" />
                    <AlternatingRowStyle />
                    <Columns>
                        <asp:BoundField HeaderText="Start VID" DataField="Start Tag No">
                            <ItemStyle></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="End VID" DataField="End Tag No">
                            <ItemStyle></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Tag Type" DataField="Tag Type">
                            <ItemStyle></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="DOC" DataField="DOC">
                            <ItemStyle></ItemStyle>
                        </asp:BoundField>
                    </Columns>
                    <EditRowStyle />
                    <HeaderStyle />
                    <PagerStyle />
                    <RowStyle />
                    <SelectedRowStyle />
                </asp:GridView>
            </div>
        </div>


    </div>


    
</asp:Content>