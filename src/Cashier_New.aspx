<%@ Page Title="" Language="C#" MasterPageFile="~/AdminModule_New.master" AutoEventWireup="true" CodeFile="Cashier_New.aspx.cs" Inherits="Cashier_New" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="//maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css">
    <link href="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484285348/css/Style.css" rel="stylesheet" />
 <%--   <link href="Bootstrap/bootstrap.min.css" rel="stylesheet" />--%>
 <style type="text/css">


.wrapper { 
            /*display: inline-block;*/
            position: relative;
        }
 .button1 {
            /*background-color:black;*/
            color: white;
            border: 0;
            margin-left: 30px;
        }

        .button1 {
            position: absolute;
            right: 9px;
            top: 7px;
        }
       .first{
            background-color: white;
            padding: 10px 10px;
            box-shadow: 0px 0px 1px #d5d5d5;
            color: black;
           border-bottom: 6px solid #5f9eee;
       }
       .second{
            background-color: white;
            padding: 10px 10px;
            box-shadow: 0px 0px 1px #d5d5d5;
            color: black;
            border-bottom: 6px solid #deb63b;
       }
        .third {
             background-color: white;
            padding: 10px 10px;
            box-shadow: 0px 0px 1px #d5d5d5;
            color: black;
            border-bottom: 6px solid #9e7da6;
        }
        .form{
            background: #f9f9f9 !important;
        }
        body{
            background: #e5e6e6 !important;
        }
        .fixed-width label{
            width: 120px !important;
        }
        .imgsync{
            height:24px;
            width:24px;
        }
    </style>

     <script type="text/javascript">
         function sum() {
             var txtFirstNumberValue = document.getElementById('txtLabourAmount').value;
             var txtSecondNumberValue = document.getElementById('txtLabourAmount').value;
             var VasAmt = document.getElementById('txt_VasAmt').value;
             var result = parseInt(txtFirstNumberValue) + parseInt(txtSecondNumberValue) + parseInt(VasAmt);
             if (!isNaN(result)) {
                 document.getElementById('txtBillAmount').value = result;
             }
         }

    </script> 

    <script type="text/javascript">

        function stopRKey(evt1) {
            var evt1 = (evt1) ? evt1 : ((event) ? event : null);
            var node = (evt1.target) ? evt1.target : ((evt1.srcElement) ? evt1.srcElement : null);
            if ((evt1.keyCode == 8 || evt1.KeyCode == 46)) { return false; }
        }

        document.onkeypress = stopRKey;
    </script>
    <script type="text/javascript" language="javascript">

        function specialChars() {
            var nbr;
            nbr = event.keyCode;
            if ((nbr >= 48 && nbr <= 57)) {
                return true;
            }
            else {
                return false;
            }
        }

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 46 || charCode > 57))
                return false;

            return true;
        }
    </script>
   <%-- <script type="text/javascript" language="javascript">
    $(function () {
    $('#btnformat').on('click', function () {
        var x = $('txtPartsAmount').val();
        $('txtPartsAmount').val(addCommas(x));
    });
});
 
function addCommas(x) {
    var parts = x.toString().split(".");
    parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    return parts.join(".");
}

 </script>--%>


<%-- <script language="javascript" type="text/javascript">
     // <!CDATA[
    // function AddComma(txt) { if (txt.value.length % 4 == 3) { txt.value += ","; } }
     // ]]>


     function AddComma(txt) {
         txt.value = txt.value.replace(",", "").replace(/(\d+)(\d{3})/, "$1,$2");
     }
   </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <div class="container-fluid" style="margin-left: auto; margin-right: auto;">
        <div class="row">
            <%--<asp:UpdatePanel runat="server"><ContentTemplate>--%>
            <div class="col-sm-12 col-md-6" style="margin-bottom:50px;">
                
                        <h3>Cashier Details</h3>
        <div class=" form" id="corner">

           <center>  <asp:Label ID="Label8"  runat="server"></asp:Label></center>
            <center>   
               
                <asp:Table ID="Table1" Width="90%" runat="server" CellPadding="4" CellSpacing="5">
               
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="left" ColumnSpan="2">
        <asp:Label ID="lblMsg" runat="server" Width="100%" ></asp:Label>
                    <asp:Label ID="lblRefNo" runat="server" Visible="False"></asp:Label>
                </asp:TableCell>
               
            </asp:TableRow>
             
                   
            <asp:TableRow >
                    <asp:TableCell ><label for="Leave_Type_Name">VID&nbsp;&nbsp;</label><label style="color:red !important">*</label>
                    <div class="wrapper">
                          <asp:TextBox ID="txtTagNo" placeholder="EXAMPLE - VID" MaxLength="20" onkeypress="return isNumberKey(event)" CssClass="form-control" runat="server"></asp:TextBox>
                         <asp:ImageButton ID="ImageButton2" CssClass="button1" runat="server" ImageUrl="https://res.cloudinary.com/deekyp5bi/image/upload/v1484543292/img/search_2.png" OnClick="ImageButton2_Click1" />
  
                   </div> 
                        <br/>                      
                    </asp:TableCell>
                
                <asp:TableCell > &nbsp; </asp:TableCell>
               
                    <asp:TableCell>
                      <label for="Short_Name">VRN/VIN&nbsp;&nbsp;</label><label style="color:red !important">*</label>
                     <asp:TextBox ID="txtRegNo" placeholder="EXAMPLE - KA01EW12" MaxLength="10" Enabled="false" CssClass="form-control" style="text-transform:uppercase !important" runat="server"></asp:TextBox>
                <br/>
                    </asp:TableCell>
                
            </asp:TableRow>
            
            
                    <asp:TableRow>
              
                <asp:TableCell>
                    <asp:UpdatePanel runat="server"><ContentTemplate>
                    <label for="Yearly_Limit">Parts Amount&nbsp;&nbsp;</label><label style="color:red !important">*</label>
                    <asp:TextBox ID="txtPartsAmount" placeholder="EXAMPLE - 500 (INR)" onkeypress="return isNumberKey(event)"
                     MaxLength="6"  onkeyup="sum();" ontextchanged="txtPartsAmount_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server" ></asp:TextBox>
       </ContentTemplate></asp:UpdatePanel><br/>
                </asp:TableCell>
                        <asp:TableCell > &nbsp; </asp:TableCell>
                <asp:TableCell>   
                     <asp:UpdatePanel runat="server"><ContentTemplate>
                      <label for="Carry_Forward _Limit">LABOUR AMOUNT&nbsp;&nbsp;</label><label style="color:red !important">*</label>           
        <asp:TextBox ID="txtLabourAmount" CssClass="form-control" placeholder="EXAMPLE - 500 (INR)" runat="server"  MaxLength="6" onkeyup="sum();"
                      AutoPostBack="True" ontextchanged="txtLabourAmount_TextChanged" onkeypress="return isNumberKey(event)" ></asp:TextBox>
                           </ContentTemplate></asp:UpdatePanel>      
                <br/></asp:TableCell>
                
            </asp:TableRow>
               

            <asp:TableRow>
                <asp:TableCell>   
                     <asp:UpdatePanel runat="server"><ContentTemplate>
                      <label for="Carry_Forward _Limit">VAS AMOUNT&nbsp;&nbsp;</label>        
   <asp:TextBox ID="txt_VasAmt" CssClass="form-control" placeholder="EXAMPLE - 500 (INR)" runat="server"  MaxLength="6" onkeyup="sum();"
                      AutoPostBack="True" ontextchanged="txt_VasAmt_TextChanged" onkeypress="return isNumberKey(event)" ></asp:TextBox>
                           </ContentTemplate></asp:UpdatePanel>      
                <br/></asp:TableCell>
                <asp:TableCell > &nbsp; </asp:TableCell>
                <asp:TableCell>   
                     <asp:UpdatePanel runat="server"><ContentTemplate>
                      <label for="Carry_Forward _Limit">Lube AMOUNT&nbsp;&nbsp;</label>        
   <asp:TextBox ID="txt_LubeAmt" CssClass="form-control" placeholder="EXAMPLE - 500 (INR)" runat="server"  MaxLength="6" onkeyup="sum();"
                      AutoPostBack="True" ontextchanged="txt_LubeAmt_TextChanged" onkeypress="return isNumberKey(event)" ></asp:TextBox>
                           </ContentTemplate></asp:UpdatePanel>      
                <br/></asp:TableCell>
            </asp:TableRow>
               

            <asp:TableRow>
                <asp:TableCell ColumnSpan="3">
                     <asp:UpdatePanel runat="server"><ContentTemplate>
                         <label for="Applicable_To">Bill Amount&nbsp;&nbsp;</label>
        <asp:TextBox ID="txtBillAmount" CssClass="form-control" runat="server" AutoPostBack="true" ReadOnly="true"></asp:TextBox>
                               </ContentTemplate></asp:UpdatePanel>             
                <br/></asp:TableCell>
            </asp:TableRow>
               
           
             


            <asp:TableRow>
            <asp:TableCell  HorizontalAlign="Left">
             <label for ="Mode of Payment"  >Mode of Payment</label><br />
                <asp:RadioButtonList id="rdPaytype" OnSelectedIndexChanged="rdPaytype_SelectedIndexChanged" CssClass="fixed-width" runat="server" RepeatLayout="Flow" RepeatDirection="Vertical"  AutoPostBack="true" >  
                        <asp:ListItem Text=" Cash Paid " Value="0" Selected="true"></asp:ListItem>
                        <asp:ListItem Text=" Credit " Value="1"></asp:ListItem>
                        <asp:ListItem Text=" Free Service " Value="2"></asp:ListItem>
                        <asp:ListItem Text=" CHEQUE / DD " Value="3"></asp:ListItem>
               </asp:RadioButtonList>

            </asp:TableCell>
                <asp:TableCell>&nbsp;</asp:TableCell>
               <asp:TableCell>
                   <label for ="Source of Payment"  >Source of Payment</label><br />
                    <asp:RadioButtonList id="rdSrcType" OnSelectedIndexChanged="rdSrcType_SelectedIndexChanged" CssClass="fixed-width" runat="server" RepeatLayout="Flow" RepeatDirection="Vertical"  AutoPostBack="true" >  
                        <asp:ListItem Text=" Mechanical " Value="4" Selected="True"></asp:ListItem> 
                        <asp:ListItem Text=" AMC " Value="0" ></asp:ListItem>
                         <asp:ListItem Text=" Body Shop " Value="1"></asp:ListItem>
                         <asp:ListItem Text=" Insurance " Value="2"></asp:ListItem>
                          <asp:ListItem Text=" Warranty " Value="3"></asp:ListItem>
                         

                        </asp:RadioButtonList>
               </asp:TableCell>
            </asp:TableRow>
           <asp:TableRow runat="server">
                <asp:TableCell ID="CrAmtCell" visible="false" runat="server">   <br/>
                 
                      <label for="Carry_Forward _Limit">Credit Amount&nbsp;&nbsp;</label><label style="color:red !important">*</label>           
                       <asp:TextBox ID="txt_creditAmt" CssClass="form-control" placeholder="EXAMPLE - 500 (INR)" runat="server"  MaxLength="6" onkeyup="sum();"
                      AutoPostBack="True" ontextchanged="txt_VasAmt_TextChanged" onkeypress="return isNumberKey(event)"  ></asp:TextBox>
                      
                <br/></asp:TableCell>
               <asp:TableCell ID="InAmtCell" visible="false" runat="server">   <br/>
               
                      <label for="Carry_Forward _Limit">Insurance Amount&nbsp;&nbsp;</label><label style="color:red !important">*</label>           
                       <asp:TextBox ID="txtInsAmt" CssClass="form-control" placeholder="EXAMPLE - 500 (INR)" runat="server"  MaxLength="6" onkeyup="sum();"
                       onkeypress="return isNumberKey(event)"  ></asp:TextBox>
                     
                <br/></asp:TableCell>
                <asp:TableCell runat="server" ID="EmptyCell"> &nbsp; </asp:TableCell>
                <asp:TableCell ID="CrDateCell" visible="false" runat="server">   <br/>
              <label for="Carry_Forward _Limit">Select Date&nbsp;&nbsp;</label><label style="color:red !important">*</label>           
                       
                     <asp:TextBox ID="Credit_date" CssClass="disable_past_dates form-control" runat="server"  MaxLength="6" 
                       onkeypress="return isNumberKey(event)"></asp:TextBox>
                          
                       <cc1:CalendarExtender ID="calndrrPDt" runat="server" Enabled="True"  TargetControlID="Credit_date">
                                                            </cc1:CalendarExtender>
                <br/></asp:TableCell>

               <asp:TableCell ID="InDateCell" visible="false" runat="server">   <br/>
            

                      <label>Select Date&nbsp;&nbsp;</label><label style="color:red !important">*</label> 
                             
                       <asp:TextBox ID="txtInsDate" CssClass="disable_past_dates form-control"  runat="server"  MaxLength="6" 
                        onkeypress="return isNumberKey(event)" ></asp:TextBox>
                      <cc1:CalendarExtender ID="CalendarExtender1" runat="server"  Enabled="True" TargetControlID="txtInsDate">
                                                               </cc1:CalendarExtender>
                 
                       
                <br/></asp:TableCell>

               <asp:TableCell ID="ChqRefCell" ColumnSpan="3" Visible="false" runat="server">
                      <label for="Consider_As">Cheque Ref. No&nbsp;&nbsp;</label>
                         <asp:TextBox ID="txtRefNO"  MaxLength="50" CssClass="form-control" runat="server"  ></asp:TextBox>
               <br />
                    </asp:TableCell>

            </asp:TableRow>

<asp:TableRow>
    
                   <asp:TableCell ColumnSpan="3">
                      <label for="Consider_As">Remarks&nbsp;&nbsp;</label>
                         <asp:TextBox ID="txtRemarks" placeholder="Type your remarks here (upto max 250 characters)" MaxLength="250" CssClass="form-control" runat="server"  TextMode="MultiLine" ></asp:TextBox>
                </asp:TableCell>
</asp:TableRow>

           <asp:TableFooterRow HorizontalAlign="Right"> 
                <asp:TableCell ColumnSpan="3">
                    <br />
                    <asp:Button class="btn btn-success" ID="btn_Save" runat="server" Text="SAVE" OnClick="btnSave_Click1"/>

                     &nbsp;&nbsp;<asp:Button Class="btn btn-primary" ID="btn_Clear" runat="server" Text="RESET" OnClick="btnClear_Click"/>
                      </asp:TableCell>
                <asp:TableCell>&nbsp;</asp:TableCell>
            </asp:TableFooterRow>
               
        </asp:Table>         </center>
                <link href="build/CSS.css" rel="stylesheet" />
                  <script src="build/Extension.min.js"></script>
        </div>

                <br />
                    <table class="tbl_count" style="width:100%">
                                        <tr>
                                             <td style="text-align:center;" class="first">
                                                 <asp:Label ID="lblMaxCount" runat="server" Font-Size="28px" style="color:#5f9eee !important;"></asp:Label>  <br />
                                                 <asp:Label ID="lbl" runat="server" Text="SMS Credit"></asp:Label>
                                                 
                                                 </td><td>&nbsp;&nbsp;&nbsp;</td>
                                             <td style="text-align:center;" class="second">
                                                 <asp:Label ID="lblConsumedCount" runat="server" Font-Size="28px" style="color:#deb63b !important;" ></asp:Label><br />
                                                 <asp:Label ID="Label9" runat="server" Text="Consumed"></asp:Label>
                                                 
                                             </td><td>&nbsp;&nbsp;&nbsp;</td>
                                             <td style="text-align:center;" class="third">
                                                 <asp:Label ID="lblRemained" runat="server" Font-Size="28px" style="color:#9e7da6 !important;"></asp:Label><br />
                                                 <asp:Label ID="Label6" runat="server" Text="Remaining" ></asp:Label>
                                                 
                                             </td>
                                          
                                            </tr>
                                        </table>         
                </div>
              <%--  </ContentTemplate></asp:UpdatePanel>--%>
            <div class="col-sm-12 col-md-5">
               
        <div style="margin-left:50px; width:100%;  float: left; background-color:white; ">
           
            <asp:GridView ID="grdCashier"  runat="server" AllowPaging="True" AutoGenerateColumns="False" 
                                            CellPadding="4" OnPageIndexChanging="grdCashier_PageIndexChanging"  PageSize="19" 
                                           OnRowDataBound="grdCashier_RowDataBound" OnSelectedIndexChanged="grdCashier_SelectedIndexChanged" CssClass="mydatagrid" PagerStyle-CssClass="pager"
            HeaderStyle-CssClass="header" GridLines="None" Width="100%" >
                                            <%--<AlternatingRowStyle BackColor="White" />
                                            <EditRowStyle BackColor="WhiteSmoke" />--%>
                                            <Columns >
                                                <asp:CommandField ButtonType="Image" ControlStyle-CssClass="imgsync" SelectImageUrl="https://cdn2.iconfinder.com/data/icons/banking-finance-colored-vol-04/48/rupee_currency_price_tag_sale_trade_shopping-2-512.png" ShowSelectButton="True" />
                                                <asp:BoundField DataField="SlNo" HeaderText="SlNo" ItemStyle-HorizontalAlign="Left">
<ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="RFID" HeaderText="VID" ItemStyle-HorizontalAlign="Left" >
<ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="VehicleNo" HeaderText="VRN/VIN" ItemStyle-HorizontalAlign="Left" >
<ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ServiceAdvisor" HeaderText="Service Advisor" ItemStyle-HorizontalAlign="Left" >
<ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:BoundField>
                                              <asp:BoundField DataField="Vehicle In" HeaderText="Vehicle In"  ItemStyle-HorizontalAlign="Left" >
                                              
<ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:BoundField>
                                                    <asp:BoundField DataField="Vehicle Out" HeaderText="Vehicle Out"  ItemStyle-HorizontalAlign="Left" >
                                              
<ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:BoundField>
                                              <%--  <asp:ButtonField ButtonType="Button" CommandName="Cancel" HeaderText="Cancel Vehicle" ShowHeader="True" Text="Cancel" ControlStyle-CssClass="btn btn-danger"/>
                                           --%>   
                                            </Columns>
                                           <%-- <FooterStyle BackColor="#008080"  ForeColor="#008080" />
                                            <HeaderStyle BackColor="Silver"  ForeColor="#333333" HorizontalAlign="Left" />
                                            <PagerStyle BackColor="Silver" ForeColor="#333333" HorizontalAlign="Center" />
                                            <RowStyle BackColor="WhiteSmoke" />
                                            <SelectedRowStyle BackColor="WhiteSmoke"  ForeColor="#333333" />--%>
                                            <HeaderStyle  HorizontalAlign="Left" />
                                            <PagerStyle  />
                                            <RowStyle HorizontalAlign="Left"/>
                                        </asp:GridView>

           
        </div>
                </div>
</div>
    </div>




</asp:Content>

