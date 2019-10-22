<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Vehicle.ascx.cs" Inherits="Vehicle" %>
<style type="text/css">
    .style1
    {
        height: 16px;
    }
</style>
<div style="width: 99%; height: 56px; text-align: center; vertical-align: middle;">
    <div id="VehicleBG" runat="server" style="width: 99%; height: 55px; border: 1px solid #666666;
        white-space: nowrap;">
        <table style="width: 100%; height: 100%;" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td rowspan="3" style="width: 52px; height: 52px;">
                    <asp:Image ID="img_Vehicle" runat="server" Height="52px" ImageUrl="" Width="62px" />
                </td>
                <td align="left" style="white-space: nowrap; font-weight: bold; padding-left: 5px;">
                    <asp:Label ID="lbl_RegNo" runat="server" Text=""></asp:Label>
                &nbsp;</td>
                <td align="left" 
                    style="white-space: nowrap; font-weight: bold; text-align: right;">
                    PDT :<asp:Label ID="lbl_PDT" runat="server"></asp:Label>
                </td>
                <td align="left" 
                    style="white-space: nowrap; font-weight: bold; text-align: center; width: 22px;">
                    <asp:Image ID="img_PDT" runat="server" Height="12px" Width="12px" />
                </td>
                <td align="left" 
                    
                    style="white-space: nowrap; font-weight: bold; text-align: center; width: 30px; background-color: #FFFFE8;" 
                    rowspan="3">
                    <asp:Image ID="img_CWJDP" runat="server" Height="24px" Width="24px" />
                </td>
            </tr>
            <tr>
                <td align="left" 
                    style="white-space: nowrap; font-size: 12px; padding-left: 5px;" class="style1">
                    <asp:Label ID="lbl_Model" runat="server" Text=""></asp:Label>
                </td>
                <td style="font-size: 12px; color: #0066FF;
                    white-space: nowrap; text-align: right;" class="style1">
                    Gate IN Time :&nbsp;<asp:Label ID="lbl_GateInTime" runat="server"></asp:Label>
                </td>
                <td style="font-family: arial, Helvetica, sans-serif; font-size: 10px; color: #0066FF;
                    white-space: nowrap; padding-right: 10px; text-align: right;" 
                    class="style1">
                    </td>
            </tr>
            <tr>
                <td align="left" style="white-space: nowrap; font-size: 12px; padding-left: 5px;">
                    <asp:Label ID="lbl_ServiceAdvisor" runat="server" Text="Sanjay Kumar"></asp:Label>
                </td>
                <td style="font-family: arial, Helvetica, sans-serif; font-size: 10px; color: #0066FF;
                    white-space: nowrap; padding-right: 10px; text-align: right;" colspan="2">
                </td>
            </tr>
             <tr>
                <td align="left" style="white-space: nowrap; font-size: 12px; padding-left: 5px;">
                    <asp:Label ID="lbl_idleTime" runat="server"  Height="20px" Width="20px"></asp:Label>
                </td>
             
            </tr>
        </table>
    </div>
</div>