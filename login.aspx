<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>login</title>
    <link href="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484214710/Bootstrap/bootstrap.min.css" rel="stylesheet" />
    <link href="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484214710/css/Style.css" rel="stylesheet" />
    <link href="CSS/Responsive.css" rel="stylesheet" />
    <style type="text/css">
        label {
            font-size: 13px;
            font-weight: 700;
            text-transform: uppercase;
            color: #555555 !important;
            margin-bottom: 3px;
        }
        body{
            background-image:url("https://res.cloudinary.com/deekyp5bi/image/upload/v1484220590/images/login_bg.png");
        }
        .btn{
            text-transform:uppercase;
        }
    </style>

</head>
<body>
    

    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="row" style="background-color:#FF7C23 !important;padding:10px">
                <div style="float:left">
                    <img src="https://res.cloudinary.com/deekyp5bi/image/upload/v1484220590/images/wms--logo.png" style="margin-top:13px;" />
               
                </div>
                <div style="float:right">
                    <img src="https://res.cloudinary.com/deekyp5bi/image/upload/v1484219853/images/spin_logo.png"/>
                </div>
            </div>
        </div>

    <div class="container">
<div class="row" style="margin: 90px auto;float: none;">
    <div class="col-md-offset-3 col-md-6" style="">
        <h3 style="text-align:center">Login to your account</h3>
        <div style="width:100%;padding: 28px 50px;background-color:white;
    box-shadow: 0px 0px 1px 1px #d3d3d3;">
            <asp:Label ID="err_Message" style="text-align:center;" runat="server" ></asp:Label><br /><br />
             <label>Dealer Code</label>
            <asp:TextBox runat="server" ID="txt_DealerCode" style="text-transform:unset !important" class="form-control" OnTextChanged="txt_DealerCode_TextChanged"></asp:TextBox>
            <br />
            <label>Username</label>
            <asp:TextBox runat="server" ID="txt_UserName" style="text-transform:unset !important"  class="form-control"></asp:TextBox>
            <br />
            <label>Password</label>
            <asp:TextBox runat="server" ID="txt_Password" style="text-transform:unset !important" TextMode="Password" ToolTip="Enter Password" class="form-control"></asp:TextBox>
            <br />
            <asp:Button ID="btn_Login" CssClass="btn btn-success" OnClick="btn_Login_Click" runat="server" Text="sign in" />
       <asp:Button ID="btn_clear" CssClass="btn btn-group" OnClick="btn_Clear_Click" runat="server" Text="reset" /><br /><br />
 
             </div>
    </div>
</div>    
    </div>
         <%--<footer style="padding:8px;padding-bottom:3px;">
           <p class="copyright" style="font-size:1.0em;float:right;">Copyright 2008 - 2019 SPIN Technologies Pvt Ltd  |    All Rights Reserved    |  <a href="http://www.spintech.in" target="blank">Powered by www.spintech.in</a> | Version 6.30 </p>
      
         </footer>--%>
        <footer style="padding:8px;padding-bottom:3px;">
           <div style="position: relative"> <p style="position: fixed; bottom: 20px; width:100%; text-align: center"> Copyright 2008 - 2019 SPIN Technologies Pvt Ltd  |    All Rights Reserved    |  <a href="http://spintech.in/" target="_blank">Powered by www.spintech.in  </a> Version 6.30 </p><a href=" http:=" "="" www.spintech.in"="" target="blank"> </a></div><a href=" http:=" "="" www.spintech.in"="" target="blank"></a>
      <br/>
			<div style="position: relative"> <p style="position: fixed; bottom: 0; width:100%; text-align: center">WorkshopPlus is supported on the following editions of Internet Explorer – Internet Explorer 11, and Microsoft Edge  |  <a href="ReleaseNotes.aspx" target="_blank">Release Notes</a></div>
         </footer>
    </form>
</body>
</html>