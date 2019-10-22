<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintNewBill1.aspx.cs" Inherits="PrintNewBill" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="//maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css">
    <style type="text/css">
        body
        {
            font-family:Arial;
            width: 330px;
            height: 150px;
        }
        
        th, td 
        {
         padding: 10px;
        }
       
        #form1
        {
          border: 1px solid black ;  
          
        }
         #printOption
        {
            background:#e0e6ef;
            padding-left:2px;
          padding-right:5px;
        }
        .div2
        {
            background:#e0e6ef;
          
        }
        .div5
        {
            border-top:dotted 1px;
            border-bottom:dotted 1px;
        }
          .div6
        {
              padding-left:2px;
          padding-right:5px;
         
        }
          .div7
        {
            background:#e0e6ef;
           
        }
         
        
    
        </style>
    <script type="text/javascript" language="javascript">
        function open_win() {

            window.open("latestCashier_n.aspx")
            window.print();
            window.close("PrintNewBill1.aspx")
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">

     <div style="text-align: right;" id="printOption" >
         <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Cashier_New.aspx">Skip</asp:HyperLink>
     <%-- <asp:Image ID="Image1" runat="server" src="img/printer.png"  style="height:12px; margin-left:180px;" />--%>
            <a href="javascript:void();" onclick="document.getElementById('printOption').style.visibility = 'hidden';open_win(); return true; ">
                Print</a>
        </div>
   <div class="div2">

     <table ID="Table1" runat="server" width="330px">
 
    <tr>
    <td>
    <asp:Label ID="Label7" runat="server" Text="Tag No:" 
                        Font-Size="Small"></asp:Label>                    
     <asp:Label ID="lblTagNo" runat="server" class="style4" 
                        Font-Size="Small"></asp:Label>
    </td>

    <td style="text-align: right;">
    <asp:Label ID="Label5" runat="server" Text="Date:"></asp:Label>
    <asp:Label ID="lblDate" runat="server" Font-Size="Small"></asp:Label>
    </td>
    </tr>
    <tr >
    <th colspan="3">
        <asp:Image ID="Image2" runat="server" ImageUrl="~/images/LOGO.jpg"/>
    <%--<asp:Label ID="lblDealer" runat="server" Font-Bold="True" Font-Size="Large" Font-Underline="False"></asp:Label>--%>
     </th>
    </tr>
    <tr>
 <th colspan="3">GATE PASS
 <%--  <asp:Label ID="Label5" runat="server" Text="GATE PASS" Font-Bold="True" Font-Size="Medium"
                        Font-Names="Consolas, Georgia" Font-Underline="True"></asp:Label>--%>
 </th> 
    </tr>

      </table>
    </div><br />


   <div class="div3">
   <table style="width: 330px">
 <tr runat="server">
    <td>
    <asp:Label ID="Label2" runat="server" Text="Vehicle No:"
                        Font-Size="Small"></asp:Label>
  
    <asp:Label ID="lblVehNo" runat="server" class="style4" 
                        Font-Size="Small"></asp:Label>
    </td>
    </tr>
    <tr runat="server">
    <td>
    <asp:Label ID="Label3" runat="server" Text="Gate IN:"
                        Font-Size="Small"></asp:Label>
    <asp:Label ID="lblGateIn" runat="server" Font-Size="Small"></asp:Label>
    </td>
    <td>
    
    </td>
    </tr>
   </table>

   </div>


   <div class="div4">

     <table style="width: 330px">
 <tr runat="server">
    <td>
    <asp:Label ID="Label4" runat="server" Text="Parts Amount:"
                        Font-Size="Small" ></asp:Label>
                        <i class="fa fa-inr" style="font-size:small"></i>
    <asp:Label ID="lblPartsAmt" runat="server" Font-Size="Small"></asp:Label>
    </td>
    </tr>
    <tr runat="server">
    <td>
    <asp:Label ID="Label9" runat="server" Text="Labour Amount:"
                        Font-Size="Small" ></asp:Label>
                        <i class="fa fa-inr" style="font-size:small"></i>
    <asp:Label ID="lblLabourAmt" runat="server"  Font-Size="Small"></asp:Label>
                        </td>
                        <td>
                            &nbsp;</td>
    </tr>

     </table>
     </div>


   <div class="div5">
   <table style="width: 330px">
    <tr ID="TableRow1" runat="server">
    <td>
    <asp:Label ID="Label8" runat="server" Text="Total Amount:" Font-Bold="true" style="margin-left:60px;"
                        Font-Size="16px"></asp:Label>
                        <i class="fa fa-inr"></i>
    <asp:Label ID="lblBillAmount" runat="server" Font-Bold="true" Font-Size="18px"></asp:Label>
     <asp:Label ID="Label12" runat="server" Text="/-" Font-Size="22px" Font-Bold="true" ></asp:Label>
    </td>
    </tr>
   </table>
   </div>


  

    <div style="text-align:right; height: 20px;" class="div6"><br /><br />
     
         <asp:Label ID="Label11" runat="server" Text="Signature" ></asp:Label>
        </div>

  <br /> <br />

    
   <div class="div7">

    <table style="width: 330px">
     <tr ID="TableRow3" runat="server">
     <th colspan="3" style="font-style:italic">Thank You & Visit Again
     </th>
  <%--  <asp:TableCell>

    <asp:Label ID="Label12" runat="server" Text="Thank You & Visit Again" Font-Size="Medium"
                        Font-Names="Arial"></asp:Label>
    </asp:TableCell>--%>
    </tr>
    </table>
    </div>
    
   
    </form>
</body>
</html>