<%@ Page Title="" Language="C#" MasterPageFile="~/AdminModule.master" AutoEventWireup="true" CodeFile="ManualEntry.aspx.cs" Inherits="ManualEntry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style4
        {
            width: 50%;
            height: 22px;
        }
        .style5
        {
            width: 50%;
            height: 11px;
        }
        .style6
        {
            width: 50%;
            height: 12px;
        }
        .style7
        {
            width: 50%;
            height: 18px;
        }
        .style8
        {
            width: 50%;
            height: 20px;
        }
        .style9
        {
            width: 50%;
            height: 9px;
        }
        .style10
        {
            height: 22px;
        }
        .style11
        {
            height: 11px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Timer ID="Timer1" runat="server"> </asp:Timer>
    <table style="width: 305px; height: 235px;">
                    <tr>
                     <td colspan="3">
                      
                         <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        
                       
                     </td>
                     
                      
                    
                    </tr>
                    <tr>
                     <td class="style4" valign="top">
                      
                         <asp:Label ID="Label2" runat="server" Text="Process"></asp:Label>
                        </td>
                     
                      <td valign="top" class="style4">
                          <asp:DropDownList ID="drpProcess" runat="server" Width="150px" AppendDataBoundItems="True"
                              AutoPostBack="True" onselectedindexchanged="drpProcess_SelectedIndexChanged">
                          </asp:DropDownList>
                      </td>
                      <td class="style10"></td>
                    </tr>
                    <tr>
                     <td class="style5" valign="top">
                      
                         <asp:Label ID="Label3" runat="server" Text="Function"></asp:Label>
                        </td>
                     
                      <td valign="top" class="style5">
                          <asp:DropDownList ID="drpMode" runat="server" Width="150px" 
                              onselectedindexchanged="drpMode_SelectedIndexChanged" AutoPostBack="True">
                              <asp:ListItem Value="-1">--Select--</asp:ListItem>
                          </asp:DropDownList>
                      </td>
                     <td class="style11"></td>
                    </tr>
                     <tr>
                     <td class="style6" valign="top">
                      
                         <asp:Label ID="lblCardType" runat="server" Text="Card Type"></asp:Label>
                        </td>
                     
                      <td valign="top" class="style6">
                          <asp:DropDownList ID="drpCardType" runat="server" Width="150px" Enabled="false" 
                              AutoPostBack="True" onselectedindexchanged="drpCardType_SelectedIndexChanged">
                          </asp:DropDownList>
                      </td>
                     <td></td>
                    </tr>
                     <tr>
                     <td class="style7" valign="top">
                      
                         <asp:Label ID="Label5" runat="server" Text="Vehicle Card No"></asp:Label>
                        </td>
                     
                      <td valign="top" class="style7">
                          <asp:DropDownList ID="drpVCardNo" runat="server" Width="150px" Enabled="False" AppendDataBoundItems="True">
                          </asp:DropDownList>
                      </td>
                     <td></td>
                    </tr>
                     <tr>
                     <td class="style8" valign="top">
                      
                         <asp:Label ID="Label6" runat="server" Text="Employee Card No" ></asp:Label>
                        </td>
                     
                      <td valign="top" class="style8">
                          <asp:DropDownList ID="drpECardNo" runat="server" Width="150px" Enabled="False" AppendDataBoundItems="True">
                          </asp:DropDownList>
                      </td>
                    <td></td>
                    </tr>
                     <tr>
                     <td class="style9" valign="top">
                      
                         <asp:Label ID="Label7" runat="server" Text="Date"></asp:Label>
                        </td>
                           <asp:UpdatePanel runat="server">
                           <ContentTemplate>
                      <td class="style9" valign="top">
                          <asp:TextBox ID="txtDate" runat="server" Width="80px"></asp:TextBox>
                          <asp:TextBox ID="txtTime" runat="server"  Width="54px"></asp:TextBox>
                          <cc1:FilteredTextBoxExtender
                                                            ID="txtDate_FilteredTextBoxExtender" runat="server" Enabled="True" TargetControlID="txtDate"
                                                            ValidChars="0123456789/">
                                                        </cc1:FilteredTextBoxExtender>
                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtDate">
                                                    </cc1:CalendarExtender>
                        <cc1:MaskedEditExtender ID="txtTime_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                        Enabled="True" ErrorTooltipEnabled="True" Mask="99:99" MaskType="Time" TargetControlID="txtTime">
                                                    </cc1:MaskedEditExtender>
                                                    <cc1:MaskedEditValidator ID="mevStartTime" runat="server" ControlExtender="txtTime_MaskedEditExtender"
                                                        ControlToValidate="txtTime" Display="Dynamic" EmptyValueBlurredText="Time is required "
                                                        EmptyValueMessage="Time is required " ErrorMessage="mevStartTime" InvalidValueBlurredMessage="Invalid Time"
                                                        InvalidValueMessage="Time is invalid" ValidationGroup="MKE"></cc1:MaskedEditValidator>
                                                      </td><td>  
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtDate"
                                                                CssClass="clsValidator" ErrorMessage="Promosed Date is Mandatory" ValidationGroup="VAL">*</asp:RequiredFieldValidator>
                      </td>
                     </ContentTemplate>
                     </asp:UpdatePanel>
                     
                    
                    </tr>
                    <tr>
                     <td></td>
                     <td valign="top"><asp:Button ID="btnSave" runat="server" CssClass="button" 
                                                            Text="Save" Title="" ValidationGroup="VAL" 
                             Width="80px" onclick="btnSave_Click"></asp:Button><asp:Button
                                                                ID="btnclr" runat="server" 
                             CssClass="button" Text="Clear" onclick="btnclr_Click">
                                                          </asp:Button></td>
                      <td></td> 
                    
                    </tr>

  </table>
</asp:Content>

