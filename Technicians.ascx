<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Technicians.ascx.cs" Inherits="Technicians" %>
<style>
    .technician{
       box-shadow: 0 -9px 8px 0 rgba(0, 0, 0, 0.1), 0 -3px 3px 0 rgba(0, 0, 0, 0);
    border: 1px solid rgba(128, 128, 128, 0.11);
    border-radius: 4px;
    margin-bottom: 2px;
    padding: 5px;
    font-size: 12px;
    }
</style>

<div class="technician">
   <li runat="server" ID="lbl_Name" class="list-group-item1" style=" margin-left: 41px;cursor: move;">
         </li>      
        
  <asp:Image ID="Image1"  Height="28px" Width="38px" runat="server" style="margin-top: -15px;opacity:0.5;" />
  <asp:Label   ID="Emp_Type" runat="server" style="" ></asp:Label>   
     <asp:Label ID="lbl_status" Visible="false" runat="server"></asp:Label> 
<br /></div>





