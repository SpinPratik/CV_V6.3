<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewDisplay.aspx.cs" Inherits="NewDisplay" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

<style>
        .main_container{
            width: 700px;
            height: 250px;
            -webkit-box-shadow: -1px 2px 5px 0px rgba(0,0,0,0.75);
            -moz-box-shadow: -1px 2px 5px 0px rgba(0,0,0,0.75);
            box-shadow: -1px 2px 5px 0px rgba(0,0,0,0.75);
            padding-bottom: 3px;
            margin-top: 20px;
            float: left;
        }
        .c1{
            margin-top: 3px;
            width: 100%;
            height: 75%;

            float: left;
        }
        .c2{
            margin-top: 3px;
            width: 100%;
            height: 18%;
            float: left;
            border-top:2px solid #cccccc;
            padding-top: 10px;
        }
        .v1{
            width: 20%;
            height: 100%;
            float: left;
            text-align: left;

        }
        .v2{

            width: 15%;
            height: 100%;
            float: left;

            text-align: left;
            border-right: 2px solid #cccccc;

        }
        .v3{

            width: 15%;
            height: 100%;
            float: left;
           padding-left: 5px;
            text-align: left;

        }
        .v4{

            width: 20%;
            height: 100%;
            float: left;

            text-align: left;

        }
        .v5{

            width: 18%;
            height: 100%;
            float: left;

            text-align: left;

        }
        .v6{
            width: 10%;
            height: 100%;
            float: left;
            text-align: left;
        }
        .f1{
            width: 10%;
            height: 100%;
            float: left;
            text-align: center;

            margin-left: 3px;
        }
        .f2{
            width: 15%;
            height: 100%;
            float: left;
            text-align: center;

            margin-left: 3px;
        }
        .f3{
            width: 10%;
            height: 100%;
            float: left;
            text-align: center;

            margin-left: 3px;
        }
        .f4{
            width: 15%;
            height: 100%;
            float: left;
            text-align: center;

            margin-left: 3px;
        }
        .f5{
            width: 15%;
            height: 100%;
            float: left;
            text-align: center;

            margin-left: 3px;
        }
        .f6{
            width: 15%;
            height: 100%;
            float: left;
            text-align: center;

            margin-left: 3px;
        }
        .f7{
            width: 15%;
            height: 100%;
            float: left;
            text-align: center;

            margin-left: 3px;
        }
        .img_v1{
            width: 100%;
            height: 80%;
            float: left;
            text-align: center;
        }
        .img_v1 img{
            width: 100%;
            height: 100%;
        }
        .tag_v1{
            width: 100%;
            height: 20%;
            float: left;
            text-align: center;

        }
        .vrn{
            font-size: 12px;
            font-weight: 600;

        }
        .saname , .stype , .km ,.jdp , .cw , .intime , .pdt ,.teamlead , .age{
            font-size: 10px;
        }
        .tech_head{
            font-size: 12px;
            font-weight: 600;
        }
        .tech_data{
            font-size: 10px;
        }
        .divider{
            width: 100%;
            float: left;
            font-size: 12px;
            font-weight: 600;
            text-align: center  ;
        }
        .d1 {

            width: 50%;
            float: left;
            text-align: center;

        }
        .d2 {

            width: 50%;
            float: left;
            text-align: center;

        }
        .data{
            font-size: 10px;
        }
        .alink{
            font-size: 12px;
            text-decoration: none;
            color: #555555;

        }

        .bay{
            font-size: 12px;
            color: #555555;
            font-weight: 600;
        }
        .header{
            width: 100%;
            height: 50px;
            background-color: brown;
        }



        body{
            margin: 0;
            font-family: "HelveticaNeue-Light", "Helvetica Neue Light", "Helvetica Neue", Helvetica, Arial, "Lucida Grande", sans-serif;
        }
        .left_filter{
            width: 20%;
            height: 500px;
            background-color: #E18728;
            float: left;
        }
        .result_filter{

            width: 60%;
            height: 600px;
           overflow: scroll;
            float: left;
        }
        .right_filter{
            width: 20%;
            height: 500px;
            background-color: #E18728;
            float: right;
        }
</style>





</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div class="container">
    <div class="header"></div>
    <div class="left_filter"></div>
    <div class="result_filter">

        <div class="main_container">
            <div class="c1">
                <div class="v1">
                    <div class="img_v1"><img src="Display_image/car.jpg"></div>
                    <div class="tag_v1"><span class="bay"><br># Bay</span></div>
                </div>
                <div class="v2">
                    <span ></span><br>
                    <span class="vrn">KL07AW2023</span><br>
                    <span class="saname">Nuthan</span><br>
                    <span class="stype">FFS</span><br>
                    <span class="km">8778</span><br>
                    <span class="jdp"><img src="Display_image/star.png"></span>
                    <span class="cw" >&nbsp;&nbsp; <img src="Display_image/hourglass.png"></span><br>
                    <span class="intime">12/12/ 10:10</span><br>
                    <span class="pdt">12/12/ 20:20</span><br>
                    <span class="teamlead">Ganesh</span><br>
                    <span class="age">1</span><br>
                </div>

                <div class="v3">
                    <div class="divider"> <span class="tech_head">TECH'N</span></div>
                    <div>
                        <span class="tech_data">Rama</span><br>
                        <span class="tech_data">Krishna</span><br>
                        <span class="tech_data">DR- Govinda</span><br>
                        <span class="tech_data">EL- Ananth</span><br>
                        <span class="tech_data">AC- Achuta</span><br>
                        <span class="tech_data">WA - Sriniva</span><br>
                        <span class="tech_data">WA - Sridhara</span><br>
                        <span class="tech_data">QC - Datta</span><br>
                        <span class="tech_data">Wash</span><br>
                    </div>

                </div>
                <div class="v4">
                    <div class="divider"><span class="tech_head">PLANNED</span></div>
                    <div class="d1">
                        <span class="data">10:00</span><br>
                        <span class="data">10:00</span><br>
                        <span class="data">10:00</span><br>
                        <span class="data">10:00</span><br>
                        <span class="data">10:00</span><br>
                        <span class="data">10:00</span><br>
                        <span class="data">10:00</span><br>
                        <span class="data">10:00</span><br>
                        <span class="data">10:00</span><br>

                    </div>
                    <div class="d2">
                        <span class="data">10:00</span><br>
                        <span class="data">10:00</span><br>
                        <span class="data">10:00</span><br>
                        <span class="data">10:00</span><br>
                        <span class="data">10:00</span><br>
                        <span class="data">10:00</span><br>
                        <span class="data">10:00</span><br>
                        <span class="data">10:00</span><br>
                        <span class="data">10:00</span><br>
                    </div>
                </div>
                <div class="v5">
                    <div class="divider">
                        <span class="tech_head">ACTUAL</span>
                    </div>
                    <div class="d1">
                        <span class="data">10:00</span><br>
                        <span class="data">10:00</span><br>
                        <span class="data">10:00</span><br>
                        <span class="data">10:00</span><br>
                        <span class="data">10:00</span><br>
                        <span class="data">10:00</span><br>
                        <span class="data">10:00</span><br>
                        <span class="data">10:00</span><br>
                        <span class="data">10:00</span><br>

                    </div>
                    <div class="d2">
                        <span class="data">10:00</span><br>
                        <span class="data">10:00</span><br>
                        <span class="data">10:00</span><br>
                        <span class="data">10:00</span><br>
                        <span class="data">10:00</span><br>
                        <span class="data">10:00</span><br>
                        <span class="data">10:00</span><br>
                        <span class="data">10:00</span><br>
                        <span class="data">10:00</span><br>
                    </div>


                </div>
                <div class="v6">
                    <div class="divider">
                        <span class="tech_head">REPEAT</span>

                    </div>
                    <div class="d1">
                        <span class="data">1</span><br>
                        <span class="data">1</span><br>
                        <span class="data">0</span><br>
                        <span class="data">0</span><br>
                        <span class="data">2</span><br>
                        <span class="data">4</span><br>
                        <span class="data">0</span><br>
                        <span class="data">0</span><br>
                        <span class="data">0</span><br>

                    </div>
                </div>
            </div>
            <div class="c2">
                <div class="f1">
                     
                    <a href="" class="alink"> STATUS</a>
                </div>
                <div class="f2">
                    <a href="" class="alink">ERT</a>
                </div>
                <div class="f3">
                    <a href="" class="alink"> REMARKS</a>
                </div>
                <div class="f4">
                    <a href="" class="alink">PARTS -RQ</a>
                </div>
                <div class="f5">
                       <asp:Button ID="partsavilable" runat="server" class="alink" Text="PARTS -AVL"  OnClick="PartsAVL_Click" />
                    
                </div>
                <div class="f6">
                    <asp:Button ID="jobs" runat="server" class="alink" Text="JOBS"  OnClick="Jobs_Click" />
          
                </div>
                <div class="f7">
                   
                    <a href="" class="alink">READY NOW</a>
                </div>

            </div>

        </div>
     


    </div>
    <div class="right_filter">
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
        <br />
        <asp:TabContainer runat="server" ID="Tabs" Height="138px"  ActiveTabIndex="0" 
            Width="100%" Visible="false">
            <asp:TabPanel runat="server" ID="Panel1" HeaderText="Address">
                <ContentTemplate>
                    <asp:UpdatePanel ID="updatePanel1" runat="server">
                        <ContentTemplate>
                            <table>
                                <tr><td>First Name:</td><td><asp:TextBox ID="txtName" runat="server" /></td></tr>
                                <tr><td>Address:</td><td><asp:TextBox ID="txtAddress" runat="server" /> 
                                    <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />


                                                     </td></tr>
                            </table>
                       </ContentTemplate>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </asp:TabPanel>
            
            <asp:TabPanel runat="server" ID="Panel3" HeaderText="Email" >
                <ContentTemplate>
                    Email: <asp:TextBox ID="txtEmail" runat="server" />
                                      
                </ContentTemplate>
            </asp:TabPanel>
        
            <asp:TabPanel runat="server" ID="Panel2"  HeaderText="Login Details">
                <ContentTemplate>
                 <table>
               <tr> <td>User Name:</td><td><asp:TextBox ID="txtUser" runat="server" /></td></tr>
               <tr> <td>Password:</td><td><asp:TextBox ID="txtPass" runat="server" /></td></tr>
                </ContentTemplate>
            </asp:TabPanel>
                   
        </asp:TabContainer>

    </div>

</div>
    </div>
    </form>
</body>
</html>
