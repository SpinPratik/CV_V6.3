<%@ Page Title="" Language="C#" MasterPageFile="~/admin.master" AutoEventWireup="true" CodeFile="AdminRegistration.aspx.cs" Inherits="AdminRegistration" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="build/jquery.datetimepicker.css" rel="stylesheet" />
    <link href="CSS/Style.css" rel="stylesheet" />
      <script>
          $(":input").attr("autocomplete", "off");

          function onlyAlphabets(e, t) {
              try {
                  if (window.event) {
                      var charCode = window.event.keyCode;
                  }
                  else if (e) {
                      var charCode = e.which;
                  }
                  else { return true; }
                  if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || (charCode == 08) || (charCode == 32))
                      return true;
                  else
                      return false;
              }
              catch (err) {
                  alert(err.Description);
              }
          }

          function isNumberKey(evt) {
              var charCode = (evt.which) ? evt.which : event.keyCode
              if (charCode > 31 && (charCode < 48 || charCode > 57))
                  return false;

              return true;
          }

          function blockChars() {
              try {
                  if (window.event) {
                      var charCode1 = window.event.keyCode;
                  }
                  else if (e) {
                      var charCode1 = e.which;
                  }
                  else { return true; }
                  if ((charCode1 > 32 && charCode1 < 47))
                      return false;
                  else
                      return true;
              }
              catch (er) {
                  alert(er.Description);
              }
          }

    </script>
    <style>
        label {
            font-size: 13px;
            font-weight: 700;
            text-transform: uppercase;
            color: rgba(85, 85, 85, 0.9) !important;
            margin-bottom: 3px;
        }

        .middle-block {
            padding: 15px 0px 15px 34px;
            background-color: #f3f3f3;
        }

        /*.btn-success {
            background-color: #0aa89e;
        }*/

        h4 {
            /*color: rgb(166, 39, 36);*/
        }

        /*.btn-success:hover {
            color: #fff;
            background-color: #2c968f;
            border-color: #398439;
        }*/
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
     <div class="container" >
           <h3>Dealer Details</h3>
        <div class="" role="form" runat="server" autocomplete="off">
            <div class="col-md-12 middle-block">
              
                <div class="form-group col-md-12">
                    <asp:Label ID="lblMsg" runat="server" Width="100%"></asp:Label>
                      <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
      ControlToValidate="txt_phoneNum" ErrorMessage="Invalid Mobile Number" CssClass="text-capitalize"
    ValidationExpression="[0-9]{10}" ></asp:RegularExpressionValidator>
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
      ControlToValidate="txt_mobNum" ErrorMessage="Invalid Mobile Number" CssClass="text-capitalize" 
    ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>
                   <%-- <asp:CompareValidator id="CompareTimes" runat="server" ControlToCompare="txt_shiftStart" ControlToValidate="txt_shiftEnd"
							ErrorMessage="The End Time must be later than the Start Time." Type="Date" Operator="GreaterThan" />--%>
                    </div>
                <div class="form-group col-md-3">
                    <label for="email">DEALER CODE</label><br />
                    <asp:TextBox ID="txt_dealer" runat="server" Width="100%" class="form-control" autocomplete="off" MaxLength="20"  ></asp:TextBox>
                
                </div>
               <%-- <div class="form-group col-md-3">
                    <label for="pwd">PASSWORD</label><br />
                    <asp:TextBox ID="txt_pswd" runat="server" TextMode="Password" Width="100%" class="form-control" MaxLength="20"></asp:TextBox>
                </div>--%>
                <div class="form-group col-md-3">
                    <label for="pwd">ORGANISATION NAME</label><br />
                    <asp:TextBox ID="txt_orgName" runat="server" Width="100%" class="form-control" MaxLength="50"></asp:TextBox>
                    

                </div>
                <asp:UpdatePanel runat="server"><ContentTemplate>
                <div class="form-group col-md-3">
                    <label for="pwd">STATE</label><br />
                    <asp:DropDownList ID="drp_states" runat="server" Width="100%" class="form-control" OnSelectedIndexChanged="drp_states_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>

                </div>
                <div class="form-group col-md-3">
                    <label for="pwd">district</label><br />
                    <asp:DropDownList ID="drp_district" runat="server" Width="100%" class="form-control" ></asp:DropDownList>
                </div>

                   
                    </ContentTemplate></asp:UpdatePanel>
                  <div class="form-group col-md-3">
                    <label for="pwd">city</label><br />
                    <asp:TextBox ID="txt_city" runat="server" Width="100%" class="form-control" MaxLength="50"></asp:TextBox>
                </div>
                <div class="form-group col-md-3">
                    <label for="pwd">AREA</label><br />
                    <asp:TextBox ID="txt_area" runat="server" Width="100%" MaxLength="50" class="form-control"></asp:TextBox>
                </div>

                <div class="form-group col-md-3">
                    <label for="pwd">PINCODE</label><br />
                    <asp:TextBox ID="txt_pincode" runat="server" Width="100%" MaxLength="6" onkeypress="return isNumberKey(event)" class="form-control"></asp:TextBox>
                </div>
                <div class="form-group col-md-3">
                    <label for="pwd">MOBILE NUMBER</label><br />
                    <asp:TextBox ID="txt_phoneNum" runat="server" Width="100%" MaxLength="13" class="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                </div>
                <div class="form-group col-md-12">
                    <label for="pwd">WEBSITE</label><br />
                    <asp:TextBox ID="txt_website" runat="server" Width="100%" MaxLength="50" class="form-control"></asp:TextBox><br />
                   
                </div>
              

                <h4>Authorised Person</h4>
                <div class="form-group col-md-4">
                    <label for="pwd">FIRST NAME</label><span style="color:red">&nbsp;*</span><br />
                    <asp:TextBox ID="txt_Fname" runat="server" Width="100%" class="form-control" MaxLength="40" onkeypress="return onlyAlphabets(event,this);"></asp:TextBox>
                </div>
                <div class="form-group col-md-4">
                    <label for="pwd">LAST NAME</label><br />
                    <asp:TextBox ID="txt_lname" runat="server" Width="100%" class="form-control" MaxLength="40" onkeypress="return onlyAlphabets(event,this);"></asp:TextBox>
                </div>
                <div class="form-group col-md-4">
                    <label for="pwd">EMAIL</label><span style="color:red">&nbsp;*</span><br />
                    <asp:TextBox ID="txt_email" runat="server" Width="100%" class="form-control" MaxLength="40"></asp:TextBox>
                </div>
                <div class="form-group col-md-6">
                    <label for="pwd">ROLE</label><br />
                    <asp:TextBox ID="txt_role" runat="server" Width="100%" class="form-control" MaxLength="40" onkeypress="return onlyAlphabets(event,this);"></asp:TextBox>
                </div>
                <div class="form-group col-md-6">
                    <label for="pwd">mobile NUMBER</label><span style="color:red">&nbsp;*</span><br />
                    <asp:TextBox ID="txt_mobNum" runat="server" Width="100%" class="form-control"  onkeypress="return isNumberKey(event)"></asp:TextBox><br />
                    
                </div>
                  <h4>Work Time</h4>
                 <div class="form-group col-md-3">
                    <label for="pwd">shift start time</label><br />
                    <asp:TextBox ID="txt_shiftStart" runat="server" Width="100%" class="form-control datetimepicker readonly" onkeypress="return isNumberKey(event)"></asp:TextBox>
                </div>
                
                <div class="form-group col-md-3">
                    <label for="pwd">break start time</label><br />
                    <asp:TextBox ID="txt_brkStart" runat="server" Width="100%" class="form-control datetimepicker readonly" onkeypress="return isNumberKey(event)"></asp:TextBox>
                </div>
                <div class="form-group col-md-3">
                    <label for="pwd">break end time</label><br />
                    <asp:TextBox ID="txt_brkEnd" runat="server" Width="100%" class="form-control datetimepicker readonly" onkeypress="return isNumberKey(event)"></asp:TextBox>
                  </div>
                <div class="form-group col-md-3">
                    <label for="pwd">shift end time</label><br />
                    <asp:TextBox ID="txt_shiftEnd" runat="server" Width="100%" class="form-control datetimepicker readonly" onkeypress="return isNumberKey(event)"></asp:TextBox> <br /> 
                </div>
                <script src="build/jquery.js"></script>
                        <script src="build/jquery.datetimepicker.full.js"></script>
                        <script>
                            $.datetimepicker.setLocale('en');
                            $('.datetimepicker').datetimepicker({
                                datepicker: false,
                                format: 'H:i'
                            });
                            $('.datetimepicker').datetimepicker({ step: 10 });

                        </script>
                    
                    <script>
                            var readOnlyLength = $('.readonly').val().length;
                            $('#txt_shiftStart').text(readOnlyLength);
                            $('#txt_shiftEnd').text(readOnlyLength);
                            $('#txt_brkStart').text(readOnlyLength);
                            $('#txt_brkEnd').text(readOnlyLength);

                            $('.readonly').on('keypress, keydown', function (event) {
                                var $TextBoxStart1 = $(this);
                                $('#txt_brkEnd').text(event.which + '-' + this.selectionStart);
                                if ((event.which != 37 && (event.which != 39))
                                            && ((this.selectionStart < readOnlyLength)
                                            || ((this.selectionStart == readOnlyLength) && (event.which == 8)))) {
                                    return false;
                                }
                            });
                        </script>
                 <h4>Customer Display Message</h4>
                 <div class="form-group col-sm-12 col-md-12">
                    <label for="pwd">Message</label><br />
                    <asp:TextBox ID="txt_dsplyMsg" runat="server" Width="100%" MaxLength="250" class="form-control"></asp:TextBox>
                </div>
                <div class="form-group col-md-offset-8 col-md-4">
                    <br />
                    <br />

                    <asp:Button ID="btn_submit" CssClass="btn btn-success" runat="server" Text="SUBMIT" OnClick="btn_submit_Click"/>

                    <asp:Button ID="btn_reset" CssClass="btn btn-info" runat="server" Text="RESET" OnClick="btn_reset_Click" />
                    <asp:Button ID="btn_next" CssClass="btn btn-warning" runat="server" Text="NEXT" OnClick="btn_next_Click" />
                   
                    <br />
                    <br />
                </div>


            </div>
        </div>
    </div>
    
    
</asp:Content>

