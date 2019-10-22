<%@ Page Title="" Language="C#" MasterPageFile="~/admin.master" AutoEventWireup="true" CodeFile="ShiftDetails.aspx.cs" Inherits="ShiftDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
       <link href="build/jquery.datetimepicker.css" rel="stylesheet" />
    <link href="CSS/Style.css" rel="stylesheet" />
</asp:Content>




<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
<Center><h4>Work Time</h4></Center><br/>
    <div class="row">
        <div class="col-md-1"></div>
        <div class="form-group col-md-2">
             <label for="pwd">Shift ID</label><br />
                        <asp:TextBox ID="txt_ShiftID" CssClass="form-control" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox>
                    </div>
        <div class="form-group col-md-2">
             <label for="pwd">Shift Name</label><br />
                        <asp:TextBox ID="txt_ShiftName" CssClass="form-control" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox>
                    </div>
        <div class="form-group col-md-2">
                        <label for="pwd">shift start time</label><br />
                        <asp:TextBox ID="txt_shiftStart" runat="server" Width="80%" class="form-control datetimepicker readonly" onkeypress="return isNumberKey(event)"></asp:TextBox>
                    </div>
        <div class="form-group col-md-2">
                        <label for="pwd">shift end time</label><br />
                        <asp:TextBox ID="txt_shiftEnd" runat="server" Width="80%" class="form-control datetimepicker readonly" onkeypress="return isNumberKey(event)"></asp:TextBox> <br /> 
                    </div>
        <div class="form-group col-md-2">
               <br />
             <asp:Button CssClass="btn btn-success" ID="btn_Submit" runat="server" Text="Button" />
        </div>
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
   
</asp:Content>