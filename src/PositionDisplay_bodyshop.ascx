<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PositionDisplay_bodyshop.ascx.cs" Inherits="PositionDisplay_bodyshop" %>
<style type="text/css">
    .style1
    {
        height: 16px;
    }
.VehicleBG
{
   width: 99%; 
   height: 52px;
    border-left:1px solid #b3b3b3;
    border-top:3px solid #fff;
  border-bottom:3px solid;
  border-right:3.5px solid;
   white-space: nowrap;
}
 
</style>

<div style="width: 99%; height: 56px; text-align: center; vertical-align: middle;">
    <div class="VehicleBG" id="VehicleBG" runat="server">
        <table style="width: 100%; height: 100%;" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td rowspan="3" style="width: 38px; height:38px;">
                    <asp:Image ID="img_Vehicle" runat="server" Height="38px" ImageUrl="" Width="38px" />
                </td>
                <td align="left" style="white-space: nowrap; font-weight: bold; padding-left: 5px;">
                    <asp:Label ID="lbl_Slno" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="lbl_RegNo" runat="server" Text="" Font-Size="12px"></asp:Label>
                       
                &nbsp;</td>
            
                <td align="left" 
                    style="white-space: nowrap; font-weight: bold; text-align: center; width: 22px;">

                      <asp:Image ID="img_PDT" class="img_PDT" runat="server" Height="10px" Width="10px"/>

                 
                </td>
             
            </tr>
            <tr>
                <td align="left" 
                    style="white-space: nowrap; font-size: 12px; padding-left: 5px;" class="style1">
                    <asp:Label ID="lbl_Model" runat="server" Text=""></asp:Label>
                </td>
                 <td rowspan="2" align="right" valign="middle" id="Image1">
                  <%-- <td rowspan="2" align="right" valign="middle" >--%>
                     <asp:Image ID="img_LastProcess" class="img_LastProcess" runat="server" Height="20px" Width="20px"/>
                </td>
             
            </tr>
            <tr>
                <td align="left" style="white-space: nowrap; font-size: 12px; padding-left: 5px;">
                    <asp:Label ID="lbl_ServiceAdvisor" runat="server"></asp:Label>
                </td>
             
            </tr>
        
        </table>
    </div>
</div>