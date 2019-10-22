<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Baydisplay_TLWise.aspx.cs" Inherits="Baydisplay_TLWise" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .commonFont
        {
            overflow: scroll;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table style="position: fixed; left: 0px; right: 0px; top: 0px; bottom: 0px; width: 100%;
        height: 100%;" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="3" valign="top">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" style="width: auto; height: 100%">
                    <ContentTemplate>
                        <asp:Timer ID="Timer1" runat="server" Interval="120000" OnTick="Timer1_Tick" 
                            Enabled="true">
                        </asp:Timer>
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <asp:Panel ID="timeLinePnl" runat="server" Height="100%" Width="100%">
                             <asp:GridView ID="timeLine" runat="server" CellPadding="4" ForeColor="#333333" OnRowDataBound="timeLine_RowDataBound"
                                AllowPaging="True" PageSize="8" Width="100%" 
                               HeaderStyle-BackColor="#00376F">
                                <RowStyle BackColor="WhiteSmoke" Font-Names="Consolas, Georgia" BorderColor="White"  
                                    BorderStyle="Solid" BorderWidth="2"  />
                                <FooterStyle Font-Names="Consolas, Georgia" BackColor="Silver" ForeColor="#333333" />
                                <PagerSettings Visible="False" />
                                <PagerStyle BackColor="Silver" ForeColor="#333333" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="WhiteSmoke" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle Font-Names="Consolas, Georgia" Font-Size="20" ForeColor="White" Wrap="False"
                                    BackColor="#00376F"/>
                                <EditRowStyle BackColor="#2461BF" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr style="background-color: Black; height: 40px">
            <td style="background-color: Black; vertical-align: top;" colspan="2" align="center">
              <%--  <marquee>--%>
                   <asp:Label ID="lbScroll" runat="server" ForeColor="White" BackColor="Black"
                  Font-Size="Large" Font-Names="Consolas, Georgia" Text="Bay Progress Display" Font-Bold="True"></asp:Label>
                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   <asp:Label ID="L2" runat="server"  ForeColor="White" BackColor="Black"
                  Font-Size="Large" Font-Names="Consolas, Georgia" Text="Work In Progress" Font-Bold="True"></asp:Label>
                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   <asp:Label ID="Label2" runat="server"  ForeColor="Green" BackColor="Black"
                  Font-Size="Large" Font-Names="Consolas, Georgia" Text="Work Completed" Font-Bold="True"></asp:Label>
                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   <asp:Label ID="Label4" runat="server"  ForeColor="CornflowerBlue" BackColor="Black"
                  Font-Size="Larger" Font-Names="Consolas, Georgia" Text="Vehicle On Hold" Font-Bold="True"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   <asp:Label ID="Label1" runat="server"  ForeColor="#A40000" BackColor="Black"
                  Font-Size="Larger" Font-Names="Consolas, Georgia" Text="Work In Progress Delay" Font-Bold="True"></asp:Label>
                 <%-- </marquee>--%>
            </td>
            <td align="center" style="width: 150px; vertical-align: top;">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lblPage" runat="server" Font-Bold="True" ForeColor="White" BackColor="Black"
                            Font-Size="Large" Font-Names="Consolas, Georgia" Text=""></asp:Label>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>