 <%@ Page Title="Display" Language="C#" MasterPageFile="~/AdminModule_New.master" AutoEventWireup="true"
    CodeFile="DisplayDP.aspx.cs" Inherits="DisplayDP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .AdminHeaderParts
        {
            width: 33%;
            font-size: 13;
        }

        .cap
        {
            text-align: center;
            background-color: Silver;
            color: #333333;
        }
        .style1
        {
            height: 22px;
            width: 210px;
        }
        .style2
        {
            width: 210px;
        }
    </style>
    <script type="text/javascript">

        function stopRKey(evt) {
            var evt = (evt) ? evt : ((event) ? event : null);
            var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
            if ((evt.keyCode == 8 || evt.keyCode == 46)) { return false; }
        }

        document.onkeypress = stopRKey;
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

     <a href="DPHome.aspx" title="Back to DP Dashboard">
        <div style="position:absolute;top:15px;left:10px;">
        <img src="img/leftarrow.png" alt="Alternate Text" height="32" width="32"/>
    </div>
    </a>

    <table style="height: 100%; width: 100%; font-size: 15px;"
        cellspacing="0" border="0" cellpadding="0">
        <tr>
            <td align="center" valign="middle">
                <table style="width: 600px; height: 50px;" border="0" cellpadding="1" cellspacing="10">
                    <tr>
                       <%-- <td style="height: 22px;" align="center" bgcolor="Silver">
                            Bay Progress Display
                        </td>--%>
                        <td style="height: 22px;" align="center" bgcolor="Silver">
                            Job Progress Display
                        </td>
                        <%--<td align="center" bgcolor="Silver" class="style1" style="height: 22px;">
                            Customer Display
                        </td>--%>
                       <%--<td align="center" bgcolor="Silver" class="style1" style="height: 22px;">
                            FI Display
                        </td>--%>
                    </tr>
                    <tr>
                        <%--<td align="center" height="160">
                            <asp:ImageButton ID="BtnBayDisplay" runat="server" Height="100%" Width="200px" ImageUrl="https://res.cloudinary.com/deekyp5bi/image/upload/v1498894000/JLR/BayUtilization.png"
                                ImageAlign="Middle" AlternateText="Bay Progress Display" ToolTip="Bay Progress Display"
                                OnClick="BtnBayDisplay_Click" />
                        </td>--%>
                        <td align="center" height="200">
                            <asp:ImageButton ID="BtnCRMDisplay" runat="server" Height="100%" Width="400px" ImageUrl="https://res.cloudinary.com/deekyp5bi/image/upload/v1501069866/TML/Jobprogressdisplay.png"
                                ImageAlign="Middle" AlternateText="CRM Display" ToolTip="Job Progress Display"
                                OnClick="BtnCRMDisplay_Click" />
                        </td>
                        <%--<td align="center" height="160">
                            <asp:ImageButton ID="BtnCustomerDisplay" runat="server" Height="100%" Width="200px"
                                ImageUrl="https://res.cloudinary.com/deekyp5bi/image/upload/v1501062704/JLR/CustomerDisplay_TML.png" ImageAlign="Middle" AlternateText="Customer Display"
                                ToolTip="Customer Display" OnClick="BtnCustomerDisplay_Click" />
                        </td>--%>
                        <%--<td align="center" height="160">
                            <asp:ImageButton ID="BtnFIDisplay" runat="server" Height="100%" Width="200px"
                                ImageUrl="https://res.cloudinary.com/deekyp5bi/image/upload/v1501069865/TML/Fidisplay.png" ImageAlign="Middle" AlternateText="FI Display"
                                ToolTip="FI Display" onclick="BtnFIDisplay_Click"  />
                        </td>--%>
                    </tr>
                    <tr>
                        <td colspan="3" style="height: 25px;">
                        </td>
                    </tr>
                    <tr>
                        <%--<td style="height: 22px;" align="center" bgcolor="Silver">
                            Job Allotment Display
                        </td>--%>
                        <td style="height: 22px;" align="center" bgcolor="Silver">
                            Position Display
                        </td>
                        <%--<td align="center" bgcolor="Silver" class="style1" style="height: 22px;">
                            Vehicle Status
                        </td>
                         <td align="center" bgcolor="Silver" class="style1" style="height: 22px;">
                            Wash Display
                        </td>--%>
                    </tr>
                    <tr>
                       <%-- <td align="center" height="160">
                            <asp:ImageButton ID="BtnJobAllotmentDisplay" runat="server" Height="100%" Width="200px"
                                ImageUrl="https://res.cloudinary.com/deekyp5bi/image/upload/v1498912535/JLR/allotment.png" ImageAlign="Middle" AlternateText="Job Allotment Display"
                                ToolTip="Job Allotment Display" OnClick="BtnJobAllotmentDisplay_Click" />
                        </td>--%>
                        <td align="center" height="200">
                            <asp:ImageButton ID="BtnPositionDisplay" runat="server" Height="100%" Width="400px"
                                ImageUrl="https://res.cloudinary.com/deekyp5bi/image/upload/v1501069865/TML/Positiondisplay.png" ImageAlign="Middle" AlternateText="Position Display"
                                ToolTip="Position Display" OnClick="BtnPositionDisplay_Click" />
                    <%--    </td>
                        <td align="center" height="160">
                            <asp:ImageButton ID="BtnCustomerDisplay1" runat="server" Height="100%" Width="200px"
                                ImageUrl="https://res.cloudinary.com/deekyp5bi/image/upload/v1501069865/TML/Vehiclestatus.png" ImageAlign="Middle" AlternateText="Vehicle Status"
                                ToolTip="Vehicle Status" OnClick="BtnCustomerDisplay1_Click" />
                        </td>--%>
                      <%--  <td align="center" height="160">
                            <asp:ImageButton ID="BtnWashDisplay" runat="server" Height="100%" Width="200px"
                                ImageUrl="https://res.cloudinary.com/deekyp5bi/image/upload/v1501069865/TML/Wash.png" ImageAlign="Middle" AlternateText="Wash Display"
                                ToolTip="Wash Display" onclick="BtnWashDisplay_Click"  />
                        </td>--%>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>