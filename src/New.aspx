<%@ Page Language="C#" AutoEventWireup="true" CodeFile="New.aspx.cs" Inherits="New" %>

<%@ Register Src="Technicians.ascx" TagName="Technicians" TagPrefix="uc1" %>
<%@ Register Src="AllotmentVehicles.ascx" TagName="AllotmentVehicles" TagPrefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- Latest compiled and minified CSS -->
 <link href="Bootstrap/bootstrap.min.css" rel="stylesheet" />
    <link href="Bootstrap/bootstrap-theme3.3.6.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
    <script src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
    <link rel="stylesheet" href="/resources/demos/style.css" />
    <link href="Css/jquery.datetimepicker.css" rel="stylesheet" />

    <link href="css/datetimepicker.css" rel="stylesheet" />
    <script src="Js/jquery.js"></script>
    <script src="Js/jquery.datetimepicker.full.js"></script>
    <script src="js/jquery-1.8.3.min.js"></script>
    <script type="text/ecmascript" src="css/jquery.js"></script>
   <script type="text/ecmascript" src="jquery.datetimepicker.full.js"></script>
    <link rel="stylesheet" type="text/css" href="assets/css/bootstrap.min.css" />
    <link href="Css/datetimepicker.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="assets/css/github.min.css" />
        <link rel="stylesheet" type="text/css" href="CSS/custom_ui.css" />

    <link href="CSS/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:400,500" rel="stylesheet" type="text/css" />
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet" />
     <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.6.3/css/font-awesome.min.css">

    <link href="Css/Modal.css" rel="stylesheet" />
   <script src="js/jquery-ui.min-1.8.20.js"></script>

    <title></title>
    <style type="text/css">
        textarea {
            resize: none;
        }
    </style>
    <style>
      ::-webkit-input-placeholder { /* Chrome/Opera/Safari */
  color:rgba(0,0,0,0.5) !important;
}
::-moz-placeholder { /* Firefox 19+ */
  color: pink;
}
:-ms-input-placeholder { /* IE 10+ */
  color: pink;
}
:-moz-placeholder { /* Firefox 18- */
  color: pink;
}
.text-control{
    color:#494949 !important;
    font-weight:600;
    height:28PX;
   
}
        body {
            font-family: tahoma, arial, Verdana;
            font-size: 11px;
            background-color:#ffffff;
          
        }

        .ButtonOk {
            color: white;
            background-color: #76A971;
        }

        .ButtonCancel {
            color: white;
            /*background-color:#fb8400 ;*/
        }

        .txtboxdate {
            outline: 0;
            font-family: 'Raleway',sans-serif;
            margin-top: 0px;
            border: solid 1px #dcdcdc;
            transition: box-shadow 0.3s, border 0.3s;
            background-position: bottom 50%;
        }

        .form_new {
            width: 100%;
            margin:0px;
        }

        .text-control {
            border: 1px solid #d6d6d6;
            padding: 2px 0px 2px 8px;
            margin-left: 5px;
            top: 1px;
            left: 0px;
        }
      
        .title {
            color: rgba(0, 0, 0, 0.75);   
            font-weight:bold;
        }

        .drag {
            overflow: scroll;
            height: 250px;
            direction: rtl;
        }

        .drag1 {
            overflow: scroll;
            height: 311px;
        }

        ::-webkit-scrollbar {
            width: 3px;
            height: 0.1px;
        }
        ::-webkit-scrollbar-thumb {
            background: #2F99B9;
            -webkit-box-shadow: inset 0 0 6px rgba(0,0,0,0.5);
        }

            ::-webkit-scrollbar-thumb:window-inactive {
                background-color: orange;
            }
       
        #external1 {
            direction: ltr;
        }

        .list-group {
            -webkit-box-shadow: unset !important;
            box-shadow: unset !important;
        }

        .text-control-drag {
            height: 100px;
            width: 200px;
            box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);
            border: 1px solid rgba(31, 26, 26, 0.2);
            padding: 0 0px 0px 8px;
            text-align: center;
            text-size-adjust: auto;
            font-size: large;
            background-image: url("Images/dropbox-xxl.png");
            background-position: center;
            background-repeat: no-repeat;
            background-size: 70px;
            padding-top: 20px;
        }
        /* WebKit browsers */
        ::-webkit-input-placeholder {
            color: #045FB4;
        }

        /* Mozilla Firefox 4 to 18 */
        :-moz-placeholder {
            color: #045FB4;
            opacity: 1;
        }

        /* Mozilla Firefox 19+ */
        ::-moz-placeholder {
            color: #045FB4;
            opacity: 1;
        }

        /* Internet Explorer 10+ */
        :-ms-input-placeholder {
            color: #045FB4;
        }

        .search {
            background-color: white;
            border-width: 1px;
            margin-left: -4px;
            background-color: #2F99B9;
            color: white;
        }

        .input {
            border-color: black;
            border-width: 1px;
        }

            .input:focus {
                outline: none;
            }

        .search:hover {
            cursor: pointer;
        }

        .btn {
            /*display: inline-block;
            padding: 0 8px;
            margin-bottom: 0;
            font-size: 14px;
            font-weight: 400;
            line-height: 1.42857143;
            text-align: center;
            white-space: nowrap;
            vertical-align: middle;*/
            text-transform:uppercase;
            -ms-touch-action: manipulation;
            touch-action: manipulation;
            cursor: pointer;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
            background-image: none;
            border: 1px solid transparent;
            /*border-radius: unset;*/
        }
        .btn_reset{
             display: inline-block;
            padding: 0 8px;
            margin-bottom: 0;
            font-size: 14px;
            font-weight: 400;
            line-height: 1.42857143;
            text-align: center;
            white-space: nowrap;
            vertical-align: middle;
            -ms-touch-action: manipulation;
            touch-action: manipulation;
            cursor: pointer;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
            background-image: none;
            border: 1px solid transparent;
            border-radius: unset;
            background-color:#f0ad4e;
        }
        .dropdiv1
        {
            background-image:url("images/people.png");
        }
         .dropdiv
        {
            background-image:url("images/transport%20(4).png");
            background-size: 118px;
        }
         .arrow_box {
	position: relative;
	/*background: #fff;*/
	border: 4px solid #c2e1f5;
}
.arrow_box:after, .arrow_box:before {
	top: 100%;
	left: 50%;
	border: solid transparent;
	content: " ";
	height: 0;
	width: 0;
	position: absolute;
	pointer-events: none;
}

.arrow_box:after {
	border-color: rgba(136, 183, 213, 0);
	/*border-top-color: #fff;*/
	border-width: 20px;
	margin-left: -20px;
}
.arrow_box:before {
	border-color: rgba(194, 225, 245, 0);
	border-top-color: #c2e1f5;
	border-width: 26px;
	margin-left: -26px;
}
.jc1{
    width:100px;
}
    </style>

    <script type="text/javascript">

        $(function () {
            $(".drag li").draggable({
                appendTo: "body",
                helper: "clone",
                cursor: "move",
                revert: "invalid"
            });

            initDroppable($(".dropdiv"));
            function initDroppable($elements) {
                $elements.droppable({
                    over: function (event, ui) {
                        var $this = $(this);
                    },
                    drop: function (event, ui) {
                        var $this = $(this);
                        if ($this.val() == '') {
                            $this.val(ui.draggable.text().trim())

                        }
                    }
                });
            }
        });

        $(function () {
            $(".drag1 li:first-child").draggable({
                appendTo: "body",
                helper: "clone",
                cursor: "move",
                revert: "invalid"
            });

            initDroppable($(".dropdiv1"));
            function initDroppable($elements) {
                $elements.droppable({
                    over: function (event, ui) {
                        var $this = $(this);
                    },
                    drop: function (event, ui) {
                        var $this = $(this);
                        if ($this.val() == '') {
                            $this.val(ui.draggable.text().trim())
                        }
                        else if ($this.val() != '') {
                            $this.val() == ''
                            $this.val(ui.draggable.text());
                        }
                        else {
                            $this.val() == ''
                            $this.val(ui.draggable.text());
                        }
                    }
                });
            }
        });
       


        function VehicleData() {
            document.getElementById('TextBoxVeh').value = document.getElementById('TextBox3').value;

        }

        function EmpData() {
            if (document.getElementById('TextBoxName').value == '') {
                document.getElementById('TextBoxName').value = document.getElementById('TextBox4').value.trim();
            }
            else if (document.getElementById('TextBoxName').value.trim() == document.getElementById('TextBox4').value.trim()) {
                document.getElementById('TextBoxName1').value = '';
            }
            else if (document.getElementById('TextBoxName1').value == '' && document.getElementById('TextBoxName').value.trim() != document.getElementById('TextBox4').value.trim()) {
                document.getElementById('TextBoxName1').value = document.getElementById('TextBox4').value;
            }

            else if (document.getElementById('TextBoxName1').value != null && document.getElementById('TextBoxName').value.trim() == document.getElementById('TextBox4').value.trim() && document.getElementById('TextBoxName1').value.trim() == document.getElementById('TextBox4').value.trim()) {
                document.getElementById('TextBoxName2').value = '';
            }
            else if (document.getElementById('TextBoxName').value.trim() != document.getElementById('TextBox4').value.trim() && document.getElementById('TextBoxName1').value.trim() != document.getElementById('TextBox4').value.trim()) {
                document.getElementById('TextBoxName2').value = document.getElementById('TextBox4').value;
            }

            else if (document.getElementById('TextBoxName1').value == '' || document.getElementById('TextBoxName2').value == document.getElementById('TextBox4').value || document.getElementById('TextBoxName').value == document.getElementById('TextBox4').value) {
                document.getElementById('TextBoxName1').value == '';
            }
        }
        function ClearFields() {
            document.getElementById("form1").reset();
        }

        $("#TextBox1").hide();
        $('#TextBoxjc').live('change', function () {
            $("#TextBox1").hide();
            //if ((this.value) == TextBoxjc.Items.Count)
                if ((this.value) == 4)
                {
                $('#TextBoxjc').hide();
                $("#TextBox1").show();
            }
            else {
                $("#TextBox1").hide();
            }
        });
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
             if (charCode > 31 && (charCode < 48 || charCode > 57))
                 return false;

             return true;
         }
    </script>

</head>
<body style="background-color: #f4f4f4; margin: 10px;">
    <form id="form1" runat="server" class="form_new">

        <div class="container">
            <div class="row">
                <asp:Label ID="regno" runat="server" Visible="false"></asp:Label>
                <div class="col-md-2" style="border-right: 1px solid rgba(128, 128, 128, 0.2);">
                  
                    <div class="flat" >
                     
                        <section class="flat">
                         <div class="all_heading" style="text-align:left;background-color:#E0E0E0;
                        font-weight:500;font-size:15px; width:110%;margin-left:-20px; padding:5px;
                      " > Technicians <i class="fa fa-users" aria-hidden="true" style="float:right;"></i></div>
                    
                         <br />
                 <center>  <asp:Button runat="server" ID="btn_getVehicles" CssClass=" btn btn-info" style="width:100%; background-color:#6496c8; margin-left:-30px" OnClick="btn_getVehicles_Click" Text="Show" AutoPostBack="true" />
                </center>
                       </section>
                         </div>
                    <br /> 
                          <ul class="list-group drag1" id="external" runat="server">
                             
                          </ul>
                     
                   
                       
                </div>


                <div class="col-md-7" style="float: left;">

                    <table>

                        <center> <h4 class="title">Create New Event</h4></center>
                        <center><asp:Label ID="lblMessage" runat="server" ></asp:Label></center>
                        <br />

                        <tr>
                            <td align="right">VRN/VIN&nbsp;&nbsp;</td>
                            <td>
                                <asp:TextBox ID="TextBoxVeh" CssClass="text-control" runat="server" 
                                   ></asp:TextBox></td>

                            <td align="right">BAY NAME&nbsp;&nbsp;</td>
                            <td>
                                <asp:DropDownList ID="DropDownList1" CssClass=" text-control"  runat="server">
                                </asp:DropDownList></td>
                        </tr>

                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="TextBoxName" CssClass="text-control" 
                                    placeholder="Technician 1" Width="100px" runat="server" 
                                    ></asp:TextBox></td>

                            <td>
                              <asp:Button ID="JobCode" runat="server" Text="GET JC" 
                                    style="background-color: #2F99B9;color: white;height: 28PX;width: 53px; margin-left: 5px;" 
                                    onclick="JobCode_Click" />
                            <asp:DropDownList ID="TextBoxjc" class="dropdown text-control" Width="77px" 
                                    runat="server" AutoPostBack="True" 
                                    onselectedindexchanged="TextBoxjc_SelectedIndexChanged">
                               
                                 <asp:ListItem Value="JobCode">JobCode</asp:ListItem>
                            </asp:DropDownList>
                                <asp:TextBox ID="txtJobcode1" CssClass="text-control" Width="77px" 
                                    runat="server" Visible="false" ontextchanged="txtJobcode1_TextChanged" AutoPostBack="true"></asp:TextBox>
                            </td>
                            <td>
                            <asp:TextBox ID="txtjobDesc" CssClass="text-control" placeholder="Job Description"  Width="110px" 
                                    runat="server" ></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBoxStd" CssClass="text-control" placeholder="Std.Time" Width="64px" runat="server"></asp:TextBox></td>

                            <td>
                                <asp:TextBox ID="TextBoxAllot" CssClass="text-control" placeholder="Allot Time" runat="server"  onkeypress="return isNumberKey(event)" MaxLength="5" style="margin-left: -50px;width: 68px;"></asp:TextBox></td>

                         
                            <td>
                                <asp:ImageButton ID="ImageButton3"  src="Images/black.png" Style="margin-left: 9px;" runat="server" OnClick="ButtonOK_Click " />
                            </td>

                     
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="TextBoxName1" CssClass="text-control" placeholder="Technician 2" Width="100px" runat="server"></asp:TextBox></td>

                            <td>
                                 <asp:Button ID="btnJobCode" runat="server" Text="GET JC" 
                                     style="background-color: #2F99B9;height: 28PX;color: white;width: 53px; margin-left: 5px;" onclick="btnJobCode_Click" 
                                     />
                                      <asp:DropDownList ID="drpJobcode2" class="dropdown text-control" Width="77px" 
                                    runat="server" AutoPostBack="True" onselectedindexchanged="drpJobcode2_SelectedIndexChanged" 
                                    >
                                    <asp:ListItem Value="JobCode">JobCode</asp:ListItem>
                                    </asp:DropDownList>
                                <asp:TextBox ID="TextBoxjc1" CssClass="text-control"
                                     Width="77px" runat="server" Visible="false" ontextchanged="TextBoxjc1_TextChanged" AutoPostBack="true"></asp:TextBox>

                                      </td>
                                      <td>
                                      <asp:TextBox ID="txtjobdesc1" placeholder="Job Description"  CssClass="text-control" Width="110px" 
                                    runat="server" ></asp:TextBox>
                                      </td>

                            <td>
                                <asp:TextBox ID="TextBoxStd1" CssClass="text-control" placeholder="Std.Time" Width="64px" runat="server"></asp:TextBox></td>

                            <td>
                                <asp:TextBox ID="TextBoxAllot1" CssClass="text-control" placeholder="Allot Time" runat="server"  onkeypress="return isNumberKey(event)" MaxLength="5" style="margin-left: -50px;width: 68px;"></asp:TextBox></td>

                            <td>
                                <asp:ImageButton ID="ImageButton1"  src="Images/black.png" Style="margin-left: 9px;" runat="server" OnClick="ButtonOK_Click1" /></td>

                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="TextBoxName2" CssClass="text-control" placeholder="Technician 3" Width="100px" runat="server"></asp:TextBox></td>


                            <td>
                               <asp:Button ID="btnJobCode1" runat="server" Text="GET JC" style="background-color: #2F99B9;height: 28PX;color: white;width: 53px; margin-left: 5px;" onclick="btnJobCode1_Click"  
                                     />
                                      <asp:DropDownList ID="drpJobcode3" class="dropdown text-control" Width="77px" 
                                    runat="server" AutoPostBack="True" onselectedindexchanged="drpJobcode3_SelectedIndexChanged" 
                                    >
                                    <asp:ListItem Value="JobCode">JobCode</asp:ListItem>
                                    </asp:DropDownList>
                                <asp:TextBox ID="TextBoxjc2" CssClass="text-control" Visible="false" placeholder="Job Code" 
                                    Width="77px" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                    <asp:TextBox ID="txtjobdesc2" placeholder="Job Description"  CssClass="text-control" Width="110px" 
                                    runat="server" ></asp:TextBox>
                                    </td>

                            <td>
                                <asp:TextBox ID="TextBoxStd2" CssClass="text-control" placeholder="Std.Time" Width="64px" runat="server"></asp:TextBox></td>

                            <td>
                                <asp:TextBox ID="TextBoxAllot2" CssClass="text-control" placeholder="Allot Time" runat="server"  onkeypress="return isNumberKey(event)" MaxLength="5" style="margin-left: -50px;width: 68px;"></asp:TextBox></td>

                          
                            <td>
                                <asp:ImageButton ID="ImageButton2"  src="Images/black.png" Style="margin-left: 9px;" runat="server" OnClick="ButtonOK_Click2" />

                            </td>

                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>


                    </table>
                    <table>
                        <tr>
                          <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                            <td align="right">START TIME&nbsp;&nbsp;</td>
                            <td>
                                <asp:TextBox ID="TextBoxStart1" CssClass="datetimepicker text-control" runat="server" Style="width: 121px;" AutoPostBack="true"></asp:TextBox>
                          
                       <script type="text/javascript" src="Js/jquery1.js"></script>
                       <script type="text/javascript" src="Js/jquery.datetimepicker.full.js"></script>
                        <script type="text/javascript">
                            $.datetimepicker.setLocale('en');

                            var dt = new Date();
                            $('#TextBoxStart1').datetimepicker({
                                dayOfWeekStart: 1,
                                minDate: 0,
                                minDateTime: dt,
                                scrollTime: true,
                                format: 'm/d/Y H:i',
                                step:5,
                                datetimepicker: true,
                            });
                            var readOnlyLength = $('.datetimepicker').val().length;
                            $('#TextBoxStart1').text(readOnlyLength);

                            $('.datetimepicker').on('keypress, keydown', function (event) {
                                var $TextBoxStart1 = $(this);
                                $('#TextBoxStart1').text(event.which + '-' + this.selectionStart);
                                if ((event.which != 37 && (event.which != 39))
                                            && ((this.selectionStart < readOnlyLength)
                                            || ((this.selectionStart == readOnlyLength) && (event.which == 8)))) {
                                    return false;
                                }
                            });
                        </script>
                                
                              
                            </td>
                         <%--   <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Express&nbsp;&nbsp;</td>--%>
                            <td>
                                <asp:CheckBox ID="ExpTime" runat="server" Visible="false"/></td>
                           
                        </tr>
                        <tr>

                            <td>&nbsp;</td>
                        </tr>
                    </table>
                    <center>
        <asp:TextBox ID="TextBox4" CssClass="text-control-drag dropdiv1"  TextMode="MultiLine" runat="server" onclick="EmpData();" placeholder="Drop Technician Here & Click" ReadOnly="true" style="height:100px;color:#0093E2;font-weight:bold; "></asp:TextBox>
       <asp:TextBox ID="TextBox3" CssClass="text-control-drag dropdiv"  TextMode="MultiLine" runat="server" onclick="VehicleData();" placeholder="Drop Vehicle Here & Click" ReadOnly="true" style="height:100px;color:#0093E2;font-weight:bold;"></asp:TextBox>
     </center>
                    <br />
                    <center>
                    <asp:Button ID="Save" CssClass="btn btn-success" runat="server" Text="OK" Width="87px" OnClick="Save_Click" />
                     <asp:Button ID="ButtonReset" CssClass=" btn btn-info" runat="server" Width="87px" Text="Reset" OnClick="Reset_Click" />
                    <asp:Button ID="ButtonCancel" CssClass="btn btn-danger" runat="server" Width="87px" Text="Cancel" OnClick="ButtonCancel_Click" />
         </center>
                </div>


                <div class="col-md-3" style="float: right; border-left: 1px solid rgba(128, 128, 128, 0.2);">
                    
                    <div class="all_heading" style="text-align:left;background-color:#E0E0E0;
                        font-weight:500;font-size:15px;padding:5px;
                      " > Vehicles <i class="fa fa-car" aria-hidden="true" style="float:right;padding:2px;"></i></div>
                    <br />
                   
                    <asp:RadioButtonList ID="RadioButtonList1" Style="font-weight: 200 !important;" runat="server" Height="16px" RepeatDirection="Horizontal" Width="170px" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" AutoPostBack="true">
                        
                         <asp:ListItem Value="0">&nbsp;VRN or VIN</asp:ListItem>
                        <asp:ListItem Value="1">&nbsp;VID</asp:ListItem>
                             
                    </asp:RadioButtonList>
                     
                    <section class="flat">
                    <asp:TextBox ID="txtsearch" CssClass="input text-control" placeholder="VRN/VIN or VID" OnTextChanged="Search_Button_Click" runat="server" Style="width: 141px; border: 1px solid rgba(31, 26, 26, 0.2);"></asp:TextBox>
                    <asp:Button ID="Search_Button" CssClass="button" runat="server" Text="Search" OnClick="Search_Button_Click" />
                  </section>
                      <br />
                    <br />
                    <ul class="list-group drag" id="external1" runat="server">
                    </ul>
                </div>

            </div>
        </div>




          
    </form>

    <script type="text/javascript">
        document.getElementById("TextBoxName").focus();
    </script>
    <%--<script type="text/javascript" src="assets/js/jquery.min.js"></script>
<script type="text/javascript" src="assets/js/bootstrap.min.js"></script>


<script type="text/javascript" src="assets/js/highlight.min.js"></script>--%>
    <script src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>

</body>

</html>




