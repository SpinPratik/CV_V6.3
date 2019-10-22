<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Registration.aspx.cs" Inherits="Registration" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>Registration form</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <link href="Bootstrap/bootstrap.min.css" rel="stylesheet" />
    <script src="Bootstrap/bootstrap.min.js"></script>
    <script src="Bootstrap/jquery1.12.2.min.js"></script>
    <script>
        $(":input").attr("autocomplete", "off");

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

        .btn-success {
            background-color: #0aa89e;
        }

        h4 {
            color: rgb(166, 39, 36);
        }

        .btn-success:hover {
            color: #fff;
            background-color: #2c968f;
            border-color: #398439;
        }
    </style>
</head>
<body>
    <div class="container-fluid">
        <div class="row" style="background-color: #A62724 !important; padding: 10px">
            <div style="float: left; margin-top: 12px;">
                <img src="images/wms--logo.png" />

            </div>
            <div style="float: right">
                <img src="Images/logo_spin.png" />
            </div>
        </div>
    </div>
    <br />
    <br />
    <div class="container">

        <form class="" role="form" runat="server" autocomplete="off">
            <asp:ScriptManager runat="server"></asp:ScriptManager>

            <div class="col-md-12 middle-block">
                <h4>Dealer Registration</h4>
                <div class="form-group col-md-4">
                    <label for="email">DEALER CODE</label><br />
                    <asp:TextBox ID="txt_dealer" runat="server" Width="100%" class="form-control"  onkeypress="return blockChars()" autocomplete="off"></asp:TextBox>
                </div>
                    <div class="form-group col-md-4">
                    <label for="pwd">ORGANISATION NAME</label><br />
                    <asp:TextBox ID="txt_orgName" runat="server" Width="100%" class="form-control"></asp:TextBox>
                  

                </div>
              
            
                <asp:UpdatePanel runat="server"><ContentTemplate>

                <div class="form-group col-md-4">
                    <label for="pwd">STATE</label><br />
                     <asp:DropDownList ID="drp_states" runat="server" Width="100%" class="form-control" OnSelectedIndexChanged="drp_states_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>

                </div>
                <div class="form-group col-md-4">
                    <label for="pwd">District</label><br />
                   <asp:DropDownList ID="drp_district" runat="server" Width="100%" class="form-control" ></asp:DropDownList>
                </div>
                </ContentTemplate></asp:UpdatePanel>
                  <div class="form-group col-md-4">
                    <label for="pwd">City</label><br />
                   <asp:TextBox ID="txt_city" runat="server" Width="100%" class="form-control" ></asp:TextBox>
                </div>
                <div class="form-group col-md-4">
                    <label for="pwd">AREA</label><br />
                    <asp:TextBox ID="txt_area" runat="server" Width="100%" class="form-control"></asp:TextBox>
                </div>

                <div class="form-group col-md-4">
                    <label for="pwd">PINCODE</label><br />
                    <asp:TextBox ID="txt_pincode" runat="server" Width="100%" class="form-control" MaxLength="6" onkeypress="return isNumberKey(event)"></asp:TextBox>
                </div>
                <div class="form-group col-md-4">
                    <label for="pwd">TELEPHONE NUMBER</label><br />
                    <asp:TextBox ID="txt_phoneNum" runat="server" Width="100%" class="form-control" MaxLength="13" onkeypress="return isNumberKey(event)"></asp:TextBox>
                </div>
                <div class="form-group col-md-4">
                    <label for="pwd">WEBSITE</label><br />
                    <asp:TextBox ID="txt_website" runat="server" Width="100%" class="form-control"></asp:TextBox>
                </div>
                 <div class="form-group col-md-4">
                    <label for="pwd">USER NAME</label><br />
                    <asp:TextBox ID="TextBox1" runat="server" Width="100%" class="form-control"></asp:TextBox>
                </div>
                <div class="form-group col-md-4">
                    <label for="pwd">PASSWORD</label><br />
                    <asp:TextBox ID="txt_pswd" runat="server" TextMode="Password" Width="100%" class="form-control"></asp:TextBox>
                </div>
                 <div class="form-group col-md-4">
                    <label for="pwd">CONFIRM PASSWORD</label><br />
                    <asp:TextBox ID="txt_cnfpwd" runat="server" TextMode="Password" Width="100%" class="form-control"></asp:TextBox>
                </div>

                <h4>Authorised Person</h4>
                <div class="form-group col-md-4">
                    <label for="pwd">FIRST NAME</label><br />
                    <asp:TextBox ID="txt_Fname" runat="server" Width="100%" class="form-control" MaxLength="40" onkeypress="return onlyAlphabets(event,this);"></asp:TextBox>
                </div>
                <div class="form-group col-md-4">
                    <label for="pwd">LAST NAME</label><br />
                    <asp:TextBox ID="txt_lname" runat="server" Width="100%" class="form-control" MaxLength="40" onkeypress="return onlyAlphabets(event,this);"></asp:TextBox>
                </div>
                <div class="form-group col-md-4">
                    <label for="pwd">EMAIL</label><br />
                    <asp:TextBox ID="txt_email" runat="server" Width="100%" class="form-control" MaxLength="40"></asp:TextBox>
                </div>
                <div class="form-group col-md-6">
                    <label for="pwd">ROLE</label><br />
                    <asp:TextBox ID="txt_role" runat="server" Width="100%" class="form-control" MaxLength="40" onkeypress="return onlyAlphabets(event,this);"></asp:TextBox>
                </div>
                <div class="form-group col-md-6">
                    <label for="pwd">PHONE NUMBER</label><br />
                    <asp:TextBox ID="txt_mobNum" runat="server" Width="100%" class="form-control" MaxLength="6" onkeypress="return isNumberKey(event)"></asp:TextBox>
                </div>

                

                <div class="form-group col-md-offset-10 col-md-2">
                    <br />
                    <br />

                    <asp:Button ID="btn_submit" CssClass="btn btn-success" runat="server" Text="SUBMIT" />

                    <asp:Button ID="btn_reset" Style="float: right" CssClass="btn btn-default" runat="server" Text="RESET" OnClick="btn_reset_Click" />
                    <br />
                    <br />
                </div>


            </div>
        </form>
    </div>
    <br />
    <br />
    <footer style="padding: 8px; padding-bottom: 3px;">
        <p class="copyright" style="font-size: 1.0em; float: right;">Copyright 2008 - 2016 SPIN Technologies Pvt Ltd  |    All Rights Reserved    |  <a href="http://www.spintech.in" target="blank">Powered by www.spintech.in</a> </p>

    </footer>
</body>
</html>

