  <%@ Page Title="" Language="C#" MasterPageFile="~/AdminModule_New.master" AutoEventWireup="true"
    CodeFile="GMHome.aspx.cs" Inherits="GMHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function stopRKey(evt) {
            var evt = (evt) ? evt : ((event) ? event : null);
            var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
            if ((evt.keyCode == 8 || evt.keyCode == 46)) { return false; }
        }
        document.onkeypress = stopRKey;
    </script>

      <style>
          .background-white {
            background-color: white;
            /*padding: 10px 10px;*/
            box-shadow: 0px 0px 1px #d5d5d5;
            color: black;
            box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);
        }

          body{
              background-color:#232323 ;
          }

        .second {
            border-bottom: 6px solid #76c187;
        }

        .first {
            border-bottom: 6px solid #5f9eee;
        }

        .third {
            border-bottom: 6px solid orangered;
        }

        .forth {
            border-bottom: 6px solid #9e7da6;
        }
        div a{
            text-transform:uppercase;
            font-weight:500;
        }
        .fusion-fullwidth{
            border-color: #eae9e9;
    border-bottom-width: 0px;
    border-top-width: 0px;
    border-bottom-style: solid;
    border-top-style: solid;
    padding-bottom: 120px;
    padding-top: 155px;
    padding-left: 20px;
    padding-right: 20px;
    padding-left: 20px !important;
    padding-right: 20px !important;
    background-color: #ffffff;
    background-position: center center;
    background-repeat: repeat;
        background-image: url(images/background_home.jpg);
        }
        .background-image{
                border-width: 0px;
    height: 230px;
    width: 100%;
    border-bottom: 1px solid rgba(0, 0, 0, 0.1);
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="height: 565px; margin-bottom: 30px; vertical-align: middle;">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="fusion-fullwidth">
        <div class="container">
          <div class="row count-cards">
                   
                    <div class="col-md-4 col-sm-6">
                        <div class="second background-white">
                                 <div style="margin: 20px;">
                                     <asp:HyperLink ID="HyperLink6" NavigateUrl="DisplayGM.aspx" runat="server" Text="display">
          <center> 
              
              <div>      
                                       <img src="images/DisplayCard/display.png" alt="Reports" height="128px" width="128px"/>
                                        </div>
                                    <h3>Display</h3>

              

          </center>
                                         </asp:HyperLink></div>
                        </div>
                    </div>
                    <div class="col-md-4 col-sm-6">
                        <div class="third background-white">
                            <div style="margin: 20px;">  
                                <asp:HyperLink ID="HyperLink5" NavigateUrl="GMReports.aspx" runat="server" Text="reports">
                                <center>
                                    <div>      
                                       <img src="images/DisplayCard/reports.png" alt="Reports" height="128px" width="128px"/>
                                        </div>
                                    <h3>Reports</h3>
                                </center>
                                </asp:HyperLink>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4 col-sm-6">
                        <div class="forth background-white">
                            <div style="margin: 20px;"> 
                                <asp:HyperLink ID="HyperLink4" Target="_blank" NavigateUrl="https://insight.vtabs.in" runat="server">
                                   <center>
                                        <div>      
                                       <img src="images/DisplayCard/smartdashboard.png" alt="Insight" height="128px" width="128px"/>
                                        </div>
                                   <h3>Insight</h3>
                               </center>
                                    </asp:HyperLink> 
                                </div>
                        </div>
                    </div>

                </div></div>
        </div>
        
    </div>

  
</asp:Content>

<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="height: 565px; margin-bottom: 30px; vertical-align: middle;">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table style="height: 100%; width: 100%">
            <tr>
                <td colspan="3" />
            </tr>
            <tr>
                <td />
                <td align="center" valign="middle">
                    <table style="width: 334px; text-align: center;" class="tblStyle">
                        <tr>
                            <td class="style1">
                                <asp:ImageButton ID="btndisplay" runat="server" Height="69px" ImageUrl="~/img/display.png"
                                    OnClick="btnCRMDisplay_Click" AlternateText="CRM Display" />
                            </td>
                            <td>
                                <asp:ImageButton ID="btnReport" runat="server" Height="69px" ImageUrl="~/images/reports.png"
                                    OnClick="btnReport_Click" AlternateText="Report" />
                            </td>
                            <td>
                                <asp:ImageButton ID="btnCRMDB" runat="server" Height="69px" ImageUrl="~/images/crmdb.png"
                                    OnClick="btnCRMDB_Click" AlternateText="KPI Dashboard" />
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                <asp:HyperLink ID="HyperLink2" runat="server" Font-Underline="False" Font-Names="Consolas, Georgia"
                                    ForeColor="#333333" NavigateUrl="~/DisplayHome.aspx?Back=222">Display</asp:HyperLink>
                            </td>
                            <td align="left">
                                <asp:HyperLink ID="HyperLink3" runat="server" Font-Underline="False" Font-Names="Consolas, Georgia"
                                    ForeColor="#333333" NavigateUrl="~/Reports.aspx?Back=222">Reports</asp:HyperLink>
                            </td>
                            <td>
                                <asp:HyperLink ID="HyperLink1" runat="server" Font-Underline="False" Font-Names="Consolas, Georgia"
                                    ForeColor="#333333" NavigateUrl="~/KPIDashboard.aspx">KPI<br /> Dashboard</asp:HyperLink>
                            </td>
                        </tr>
                    </table>
                </td>
                <td />
            </tr>
            <tr>
                <td colspan="3" />
            </tr>
        </table>
    </div>
</asp:Content>--%>



