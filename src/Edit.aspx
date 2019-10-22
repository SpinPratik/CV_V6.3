<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Edit.aspx.cs" Inherits="Edit" %>

<%@ Register Src="Technicians.ascx" TagName="Technicians" TagPrefix="uc1" %>
<%@ Register Src="Vehicle.ascx" TagName="Vehicle" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>New event</title>
    <link href='../Media/empty.css' type="text/css" rel="stylesheet" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css" integrity="sha384-1q8mTJOASx8j1Au+a5WDVnPi2lkFfwwEAa8hDDdjZlpLegxhjVME1fgjWPGmkzs7" crossorigin="anonymous">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap-theme.min.css" integrity="sha384-fLW2N01lMqjakBkx3l/M9EahuwpSfeNvV63J5ezn3uZzapT0u7EYsXMjQV+0En5r" crossorigin="anonymous">
    <script type="text/javascript" src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js" integrity="sha384-0mSbJDEHialfmuBBQP6A4Qrprq5OVfW37PRR3j5ELqxss1yVqOtnepnHVP9aJ7xS" crossorigin="anonymous"></script>
    <script type="text/javascript" src="Js/modal.js"></script>
    <script src="https://code.jquery.com/ui/1.8.20/jquery-ui.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>

    <link href="Css/Modal.css" rel="stylesheet" />
    <style type="text/css">
        body {
            font-family: tahoma, arial, Verdana;
            font-size: 11px;
            background-color: #F4F4F4;
        }

        .ButtonOk {
            color: white;
            background-color: #2f99b9;
        }

        .ButtonCancel {
            color: white;
            background-color: #fb8400;
        }

        .text-control {
            border: 1px solid rgba(31, 26, 26, 0.2);
            padding: 0 0px 0px 8px;
            width: 143px;
        }

        .title {
            color: rgba(31, 26, 26, 0.6);
        }

        .drag {
            overflow: scroll;
            height: 380px;
            direction: rtl;
        }

        .drag1 {
            overflow: scroll;
            height: 380px;
        }

        ::-webkit-scrollbar {
            width: 3px;
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

        .btn {
            border: 1px solid #52843a;
            height: 22px !important;
            color: white;
            cursor: pointer;
            font-size: 11px;
            border-radius: unset;
            padding-bottom: 19px;
        }
        #btn_Delete{
            color:white;
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
            $(".drag1 li").draggable({
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
                document.getElementById('TextBoxName').value = document.getElementById('TextBox4').value;
            }
            else if (document.getElementById('TextBoxName').value.trim() == document.getElementById('TextBox4').value.trim()) {
                document.getElementById('TextBoxName1').value = '';
            }
            else if (document.getElementById('TextBoxName').value.trim() != document.getElementById('TextBox4').value.trim()) {
                document.getElementById('TextBoxName1').value = document.getElementById('TextBox4').value;
            }
            else if (document.getElementById('TextBoxName').value.trim() == document.getElementById('TextBox4').value.trim() || document.getElementById('TextBoxName1').value.trim() == document.getElementById('TextBox4').value.trim()) {
                document.getElementById('TextBoxName2').value = '';
            }
            else if (document.getElementById('TextBoxName').value.trim() != document.getElementById('TextBox4').value.trim() || document.getElementById('TextBoxName1').value.trim() != document.getElementById('TextBox4').value.trim()) {
                document.getElementById('TextBoxName2').value = document.getElementById('TextBox4').value;
            }

        }
        function ClearFields() {
            document.getElementById("form1").reset();
        }
    </script>
</head>
<body style="background-color: #f4f4f4; margin: 10px;">
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="row">
                <%-- <div class="col-sm-3" style="border-right:1px solid rgba(128, 128, 128, 0.2);">
                   <h3 class="title">Technicians</h3><br />
            <ul class="list-group drag1" id="external" runat="server">
            </ul>
                 </div>--%>
                <div class=" col-md-offset-1 col-md-12">
                    <center>
                        <h4>Edit Allotment</h4>
                    </center>
                    <center>
                        <asp:Label ID="lblMessage" runat="server"></asp:Label></center>
                    <br />
                    <table>
                        <tr>
                            <td align="right">Start Time</td>
                            <td>
                                <asp:TextBox ID="TextBoxStart" CssClass="text-control" runat="server"></asp:TextBox></td>
                            <%--<td>&nbsp;&nbsp;</td>--%>
                            <td align="right">&nbsp;&nbsp;&nbsp;&nbsp;Technician</td>
                            <td>
                                <asp:TextBox ID="TextBoxName" CssClass="text-control" runat="server"></asp:TextBox></td>
                        </tr>

                        <tr>
                            <td>&nbsp;&nbsp;</td>
                        </tr>
                        <tr>
                            <td align="right">Bay</td>
                            <td>
                                <asp:DropDownList CssClass="text-control" Enabled="false" ID="DropDownList1" runat="server">
                                </asp:DropDownList></td>
                            <%-- <td>&nbsp;&nbsp;</td>--%>
                            <td align="right">Vehicle #</td>
                            <td>
                                <asp:TextBox ID="TextBoxVeh" CssClass="text-control" runat="server"></asp:TextBox></td>
                        </tr>

                        <tr>
                            <td>&nbsp;&nbsp;</td>
                        </tr>
                        <tr>
                            <td>Allot Time</td>
                            <td>
                                <asp:TextBox ID="TextBoxAllot" CssClass="text-control" runat="server"></asp:TextBox></td>
                           <%-- <td>Express &nbsp;&nbsp;
                                <asp:CheckBox ID="ExpTime" runat="server" /></td>--%>
                        </tr>


                        <tr>
                            <td>&nbsp;</td>
                        </tr>

                    </table>
                    <center>
                        <asp:Button ID="ButtonOK" CssClass="btn ButtonOk" runat="server" OnClick="ButtonOK_Click" BackColor="#76A971" Text="ReSchedule" />
                        <%--<input type="button" id="Button1" class="btn" onclick="ClearFields()" style="background-color: #eb9316;" value="Reset" />--%>
                        <asp:Button ID="ButtonCancel" CssClass="btn ButtonCancel" runat="server" Style="border-color: #245580; background-color: #337ab7;" Text="Cancel" OnClick="ButtonCancel_Click" />
                        <asp:Button ID="btn_Delete" CssClass="btn" runat="server" Text="Delete" Style="border-color: darkred; background-color: darkred" OnClick="LinkButtonDelete_Click" />
                        <%--     <asp:ImageButton ID="btn_Delete" src="Images/tool%20(1).png" runat="server"  style="border-color: darkred;" OnClick="LinkButtonDelete_Click"/>--%>
                    </center>
                </div>
            </div>
        </div>

    </form>
    <script type="text/javascript">
        document.getElementById("TextBoxName").focus();
    </script>

</body>
</html>
