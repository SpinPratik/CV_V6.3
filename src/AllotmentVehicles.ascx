<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AllotmentVehicles.ascx.cs" Inherits="AllotmentVehicles" %>
<style type="text/css">
  
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
ul{
    list-style-type:none !important;
}
 
</style>


<div style="border: 1px solid rgba(128, 128, 128, 0.13); font-size: 12px; padding: 4px;
    margin-bottom: 2px; border-radius: 4px; box-shadow: 0 0 27px 0 rgba(0, 0, 0, 0.1), 0 -3px 3px 0 rgba(0, 0, 0, 0);">
    <asp:Image ID="img_Vehicle" runat="server" Height="38px" Width="38px"
        Style="margin-top: 19px;" />
    <asp:Label ID="lbl_PDT" CssClass="" runat="server"></asp:Label>
    <li runat="server" class="list-group-item1 dvehicles" id="lbl_RegNo" style="margin-top: -22px;
        cursor: move; margin-left: 43px;"></li>
    <asp:Label ID="lbl_Slno" CssClass="" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lbl_Model" CssClass="" runat="server" Text="" Style="margin-left: 42px;"></asp:Label>
<asp:Label ID="lbl_ServiceAdvisor" CssClass="" runat="server" Style=""></asp:Label>
    
    <asp:Image ID="ImageCWJDP" runat="server" Height="24px" ImageUrl="" Width="24px" style="float: right;margin-top: -29px;" />
    <asp:Image ID="ImagePDT" runat="server" Height="10px" ImageUrl="" Width="10px"  style="float: right;margin-top: -28px;
    margin-right: 21px;" />
</div>
