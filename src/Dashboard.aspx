<%@ Page Title="" Language="C#" MasterPageFile="~/AdminModule_New.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  <script src="js/jquery_2.1.3.min.js"></script>
       <script src="js/Highcharts.js"></script>
<link href="Bootstrap/bootstrap_3.3.6.min.css" rel="stylesheet" />
<script src="Bootstrap/bootstrap_3.3.6.min.js"></script>
  <script type="text/javascript">
       window.history.forward();
       function noBack() { window.history.forward(); }
          </script>
      <style>
        .container > header {
            margin: 0 auto;
               padding: 7em 7em 0em 7em;
        }

        body {
            background-color: #E5E6E6;
            min-width:unset !important
        }
        
        .background-white {
            background-color: white;
            padding: 10px 10px;
            box-shadow: 0px 0px 1px #d5d5d5;
            color: black;
        }

        .second {
            border-bottom: 6px solid #76c187;
        }

        .first {
            border-bottom: 6px solid #FF6040;
        }

        .third {
            border-bottom: 6px solid #9e7da6;
        }

        .forth {
            border-bottom: 6px solid #deb63b;
        }
          .fifth {
            border-bottom: 6px solid #7A869C;
        }

        .user-profile {
            background-color: #7a869c;
            /*padding: 20px 0 15px;*/
        }

        .count-cards {
            margin-bottom: 24px;
        }

        .graph {
            margin-bottom: 24px;
        }

        .user-profile h3 {
            font-size: 16px;
            color: white;
            font-weight: 700;
            margin-top: 0;
        }

        .graph .login-time {
            padding: 12px 0 14px;
            background-color: #ffffff;
            color: black;
        }

        .chart-options {
            margin-bottom: 20px;
        }

            .chart-options span {
                padding-left: 14px;
                color: #333;
            }

        span {
            font-size: 13px !important;
            font-weight: 400 !important;
        }

        .chats-blk {
            background-color: white;
            padding: 20px 10px;
        }

        .nav-pills > li.active > a {
            background-color: unset !important;
        }

        .nav-pills > li.active > a {
            background-color: unset;
            border-bottom: 1px solid #337ab7;
            color: #337ab7;
            border-radius: 0px;
        }

            .nav-pills > li.active > a, .nav-pills > li.active > a:focus, .nav-pills > li.active > a:hover {
                color: #337ab7;
            }

        .labelcount {
            font-size: 28px !important;
            font-weight: 700 !important;
        }

        .container > header span {
            padding: 0 0 0 0;
        }

        body {
            /*overflow: hidden;*/
            overflow-x:scroll;
            overflow-y:scroll;
        }

        .sender .graph ul li {
            /*width: 49%;*/
            padding: 0 !important;
        }

        .list-inline > li {
            display: inline-block;
        }

        .graph ul li h5 {
            margin: 0 !important;
            color: #ffffff;
            padding: 12px 0;
        }

        .graph ul li p {
            margin: 0 !important;
            color: #ffffff;
            padding: 10px 0;
            line-height: 18px;
        }

        .graph ul li h5 {
            margin: 0 !important;
            color: #ffffff;
            padding: 12px 0;
        }

        h5 {
            font-size: 28px;
            margin-top: 0;
            padding-left: 0 !important;
            font-weight: 700;
        }
       
        .col-lg-1, .col-lg-10, .col-lg-11, .col-lg-12, .col-lg-2, .col-lg-3, .col-lg-4, .col-lg-5, .col-lg-6, .col-lg-7, .col-lg-8, .col-lg-9, .col-md-1, .col-md-10, .col-md-11, .col-md-12, .col-md-2, .col-md-3, .col-md-4, .col-md-5, .col-md-6, .col-md-7, .col-md-8, .col-md-9, .col-sm-1, .col-sm-10, .col-sm-11, .col-sm-12, .col-sm-2, .col-sm-3, .col-sm-4, .col-sm-5, .col-sm-6, .col-sm-7, .col-sm-8, .col-sm-9, .col-xs-1, .col-xs-10, .col-xs-11, .col-xs-12, .col-xs-2, .col-xs-3, .col-xs-4, .col-xs-5, .col-xs-6, .col-xs-7, .col-xs-8, .col-xs-9{
            position:static;
        }
        .highcharts-container{
            width:100% !important;
        }
       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid">

        <div class="row">
            <div class="col-md-12">
                <div class="row count-cards">
                    <div class="col-md-2 col-sm-6">
                        <div class="first background-white">
                            <center><asp:Label style="color:#FF6040;" CssClass="labelTotal labelcount" ID="labelTotal" runat="server" Text="100"></asp:Label>
            <asp:Label ID="Label2" runat="server" CssClass="labelText" Text="TOTAL STAFF"></asp:Label></center>
                        </div>
                    </div>
                    <div class="col-md-2 col-sm-2">
                        <div class="second background-white">
                            <center><asp:Label style="color:#76c187;" ID="labelPresent"  CssClass="labelPresent labelcount" runat="server" Text="50"></asp:Label>
            <asp:Label ID="Label4" runat="server" CssClass="labelText" Text="PRESENT STAFF"></asp:Label></center>
                        </div>
                    </div>
                    <div class="col-md-2 col-sm-2">
                        <div class="third background-white">
                            <center><asp:Label style="color:#9e7da6;" ID="labelAbsent"  CssClass="labelAbsent labelcount" runat="server" Text="50"></asp:Label>
            <asp:Label ID="Label6" runat="server" CssClass="labelText" Text="ABSENT STAFF"></asp:Label></center>
                        </div>
                    </div>
                    <div class="col-md-2 col-sm-2">
                        <div class="forth background-white">
                            <center><asp:Label style="color:#deb63b;" ID="labelArrival"  CssClass="labelArrival labelcount" runat="server" Text="25"></asp:Label>
            <asp:Label ID="Label8" runat="server" CssClass="labelText" Text="LATE ARRIVAL STAFF"></asp:Label></center>
                        </div>
                    </div>
                     <div class="col-md-2 col-sm-2">
                        <div class="fifth background-white">
                            <center><asp:Label style="color:#7A869C;" ID="labelleft"  CssClass="labelArrival labelcount" runat="server" Text="25"></asp:Label>
            <asp:Label ID="Label3" runat="server" CssClass="labelText" Text="LEFT EMPLOYEES"></asp:Label></center>
                        </div>
                    </div>

                </div>

                  </div>
            <div class="col-md-12">
                <div class="row graph">
                    <div class="col-md-4">
                        <div style="margin-bottom: 30px">
                            <div class="user-profile">

       <div id = "containerDonut" style = " height: 400px; margin: 0 auto"></div>
      <script language = "JavaScript">
         $(document).ready(function() {  
            var chart = {
               type: 'pie',
               options3d: {
                  enabled: true,
                  alpha: 45         
               }
            };
            var title = {
               text: 'Revenue Estimate'   
            };   
         
            var plotOptions = {
               pie: {
                  innerSize: 100,
                  depth: 45
               }
            };   
            var series = [{
               name: 'Amount',
               data: [
                  ['Labour', <%= labourAmt%>],
                  ['Parts', <%=PartsAmt%>],
                  ['VAS', <%=VasAmt%>],
                  ['Lube', <%= LubeAmt%>]
              
               ]
            }];     
            
            var json = {};   
            json.chart = chart; 
            json.title = title;       
          
            json.plotOptions = plotOptions; 
            json.series = series;   
            $('#containerDonut').highcharts(json);
         });
      </script>

                               <%-- <center>      
                             <h3>WELCOME</h3>
                                    <asp:Image ID="Image1"  Height="80" Width="80" runat="server"></asp:Image>
                                 <br /><br />
                         <asp:Label ID="LabelDealerName" runat="server" style="color:white;font-weight:bold;text-transform:uppercase" Font-Bold="true"></asp:Label><br />
                                <p style="font-size: 13px; font-weight: 400;">SUBSCRIPTION ENDS IN: 
                                    
                                    <asp:Label ID="Lbl_instDate" runat="server" ForeColor="White" Font-Bold="true" ></asp:Label>
                                    </p>
                         </center>--%>
                            </div>

                        <%--    <div class="login-time">
                                <center>   <p style="font-size: 13px; font-weight: 400;">LAST LOGIN: 
                   <asp:Label ID="LabelLogin" runat="server" Text="Label" style="text-transform:uppercase"></asp:Label>

                                                            </p>
                         </center>
                            </div>--%>
                        </div>
                        <div class="sender">
                            <ul class="list-inline">
                                <li style="width: 49%;">
                                    <center>  <span id="sender" style="display: none; font-size: 13px; font-weight: 400;">2</span></center>
                                    <center> <%--<h5 id="sender-id" style="background-color: #8ec65f;">2</h5>--%>
                                   <asp:Label ID="Lbl_DptIn" style="background-color: #8ec65f;font-size:30px !important;color:white!important;" runat="server" Text="Label"></asp:Label>
                                         <p style="font-size: 13px; font-weight: 400; background-color: rgb(129, 192, 77);">
                                       INFLOW <br />
                                    </p></center>
                                </li>
                                <li style="width: 49%;">
                                    <center>   <span id="sender1" style="display: none; font-size: 13px; font-weight: 400;">20</span></center>
                                    <center>  <%--<h5 id="templates" style="background-color: #1ab4ef">20</h5>--%>
                                        <asp:Label ID="Lbl_Dptout" style="background-color: #1ab4ef;font-size :30px !important;color:white!important;" Font-Size="30px" runat="server" Text="Label"></asp:Label>
                                    <p style="font-size: 13px; font-weight: 400; background-color: rgb(0, 172, 237);">
                                       OUTFLOW<br /> 
                                    </p></center>
                                </li>
                            </ul>
                        </div>
                    </div>
                    <div class="col-md-8">
                        <div class="chats-blk">
                            <div class="chart-options">
                                <span id="graphtitle" style="font-size: 13px; font-weight: 400; float: left;"></span>
                                
                                <ul class=" list-inline nav nav-pills" style="color: black;float:right">
                                    <li class="ac-graph-home active"><a data-toggle="pill" style="background-color: none;" href="#container3">Daily</a></li>
                                    <li class="ac-graph-home"><a data-toggle="pill" style="background-color: none;" href="#container1">Weekly</a></li>
                                    <li class="ac-graph-home" style="visibility:hidden" ><a data-toggle="pill" style="background-color: none;" href="#container">Monthly</a></li>
                                    
                                </ul>
                            </div>

                            <div class="tab-content" >
                                <div id="container3" class="tab-pane fade in active"></div>
                                <script>
                                    $(function () {
                                        var date = new Date();
                                        $('#container3').highcharts({
                                            chart: {
                                               
                                                zoomType: 'xy'
                                            },
                                            xAxis: {
                                                type: 'datetime',
                                                dateTimeLabelFormats: {
                                                    day: '%e %b'
                                                }
                                            },
                    
                                            yAxis: [{ // Primary yAxis
                                                min: 0,
                                                allowDecimals: false,
                                                title: {
                                                    text: 'Daily Attendance Count'
                                                }
                        
                                            }],
                                            tooltip : {
                                                headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                                                pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                                                   '<td style="padding:0"><b>{point.y}</b></td></tr>',
                                                footerFormat: '</table>',
                                                shared: true,
                                                useHTML: true
                                            },
                                            title: {
                                                text: 'Daily Attendance'
                                            },
                                            series: [{
                                                name:'Revenue',
                                                type: 'column',
                                                
                                                data: <%=TotalRevenue%>, 
                                                pointStart: Date.UTC(date.getFullYear(), date.getMonth(), 1),
                                                pointInterval: 24 * 3600 * 1000,
                                                color:'#FF6040',
                                                dataLabels: {
                                                    enabled: true,
                                                    color: '#000',
                                                    align: 'right',
                                                    format: '{point.y}', // one decimal
                                                    y: 10, // 10 pixels down from the top
                                                    style: {
                                                        fontSize: '13px',
                                                    }
                                                }
                                            }, {
                                                name:'Target',
                                                type: 'line',
                                                data: <%= TargetAmt%>,   
                                                color:'#76c187',
                                                pointStart:Date.UTC(date.getFullYear(), date.getMonth(), 1),
                                                pointInterval: 24 * 3600 * 1000, // one day
                                                dataLabels: {
                                                    enabled: true,
                                                    color: '#000',
                                                    align: 'right',
                                                    format: '{point.y}', // one decimal
                                                    y: 10, // 10 pixels down from the top
                                                    style: {
                                                        fontSize: '13px',
                                                    }
                                                }
                                            }<%--,
                    {
                        name:'Absent',
                        visible:false,
                        data: <%= AbsentCount%>,  
                        color:'#9e7da6',
                        pointStart: Date.UTC(date.getFullYear(), date.getMonth(), 1),
                        pointInterval: 24 * 3600 * 1000, // one day
                        dataLabels: {
                                            enabled: true,
                                            color: '#000',
                                            align: 'right',
                                            format: '{point.y}', // one decimal
                                            y: 10, // 10 pixels down from the top
                                            style: {
                                fontSize: '13px',
                                }
                        }
                    },
                    {
                        name:'Late Arrival',
                        visible:false,
                        color:'#deb63b',
                        data: <%= LateCount%>, 
                        pointStart: Date.UTC(date.getFullYear(), date.getMonth(), 1),
                        pointInterval: 24 * 3600 * 1000,
                        dataLabels: {
                            enabled: true,
                            color: '#000',
                            align: 'right',
                            format: '{point.y}', // one decimal
                            y: 10, // 10 pixels down from the top
                            style: {
                                fontSize: '13px',
                            }
                        }
                    },--%>
                   
                                            ]
                                        });
                                    });
                                </script>

                                <div id="container1" class="tab-pane fade " style="height: 400px; width: 796px"></div>
                                <script>
                                    $(function () {
                                        var date = new Date();
                                        var d = new Date();
                                        d.setDate(d.getDate() - 28);
                                        var curr_date = d.getDate();
                                        var curr_month = d.getMonth();
                                        var curr_year = d.getFullYear();
                                        var current =Date.UTC(curr_year,curr_month,curr_date) ;

                                        var d= Date.UTC(date.getFullYear(), date.getMonth(), date.getDay())
                                        //alert(d+"/"+current);
                                        //alert(Date.UTC(2016, 4, 1)+"  d="+d);
                                        $('#container1').highcharts({
                                            chart: {
                                                type: 'column',
                                                zoomType: 'xy'
                        
                                            },

                                            title: {
                                                text: 'Weekly Attendance'
                                            },
                                            subtitle: {
                                                text: ''
                                            },
                                            xAxis: {
                                                type: 'datetime',
                       
                                                dateTimeLabelFormats: {
                                                    week: '%b %e'
                                                }
                                            },
                                            yAxis: [{ // Primary yAxis
                                                min: 0,
                                                allowDecimals: false,
                                                //max: 100,
                                                title: {
                                                    text: 'Weekly Attendance Count'         
                                                } ,
                                                stackLabels: {
                                                    enabled: true,
                                                    style: {
                                                        fontWeight: 'bold',
                                                        color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
                                                    }
                                                }
                                            }],
                                            tooltip: {
                                                shared: true
                                            },
                  
                                            plotOptions: {

                                                line: {
                                                    dataLabels: {
                                                        enabled: true
                                                    },
                                                    enableMouseTracking: false
                                                },
                                               
                                                series: {
                                                    pointStart: current,
                                                    pointInterval: 7 * 24 *3600 * 1000
                                                },


                                            },
                                            series: [{
                                                
                                                name: 'Total',
                                                visible:false,
                                                color: '#FF6040',
                                                data: <%= WeeklyTotal %>,
                                                dataLabels: {
                                                    enabled: true,
                                                    color: '#000',
                                                    align: 'right',
                                                    format: '{point.y}',
                                                    y: 10,
                                                    style: {
                                                        fontSize: '13px',
                                                    }
                                                }

                                            },{
                                                name: 'Present',
                                                color: '#76c187',
                                                data: <%= WeeklyPresent %>,
                                                dataLabels: {
                                                    enabled: true,
                                                    color: '#000',
                                                    align: 'right',
                                                    format: '{point.y}', 
                                                    y: 10, 
                                                    style: {
                                                        fontSize: '13px',
                                                    }
                                                }

                                            },{
                                                name: 'Absent',
                                                visible:false,
                                                color: '#9e7da6',
                                                data: <%= WeeklyAbsent %>,
                                                dataLabels: {
                                                    enabled: true,
                                                    color: '#000',
                                                    align: 'right',
                                                    format: '{point.y}', // one decimal
                                                    y: 10, // 10 pixels down from the top
                                                    style: {
                                                        fontSize: '13px',
                                                    }
                                                }
                                            },
                                            {
                                                name: 'Late Arrival',
                                                visible:false,
                                                color: '#deb63b',
                                                data: <%= WeeklyLA %>,
                                                dataLabels: {
                                                    enabled: true,
                                                    color: '#000',
                                                    align: 'right',
                                                    format: '{point.y}',
                                                    y: 10, 
                                                    style: {
                                                        fontSize: '13px',
                                                    }
                                                }
                                            },

                                            ]
                                        });
                                    });
                                </script>

                                <div id="container" class="tab-pane fade" style="height: 400px; width: 796px"></div>
                                <script language="JavaScript">
                                    $(document).ready(function() {  
                                        var chart = {
                                            type: 'column'
                                        };
                                       
                                        var title = {
                                            text: 'Monthly Attendance'   
                                        };
                                        var subtitle = {
                                            text: ''  
                                        };
                                        var xAxis = {
                                            categories: ['Jan','Feb','Mar','Apr','May','Jun','Jul','Aug','Sep','Oct','Nov','Dec'],
                                            crosshair: false
                                        };
                                        var yAxis = {
                                            allowDecimals: false,
                                            min: 0,
                                            title: {
                                                text: 'Monthly Attendance Count'         
                                            }      
                                        };
                                        var tooltip = {
                                            headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                                            pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                                               '<td style="padding:0"><b>{point.y}</b></td></tr>',
                                            footerFormat: '</table>',
                                            shared: true,
                                            useHTML: true
                                        };
                                        var plotOptions = {
                                            column: {
                                                pointPadding: 0.2,
                                                borderWidth: 0
                                            },
                                            line: {
                                                dataLabels: {
                                                    enabled: true
                                                },
                                                enableMouseTracking: false
                                            }
                                        };  
                                        var credits = {
                                            enabled: false
                                        };
   
                                        var series= [{
                                            name: 'Total',
                                            visible:false,
                                            color:'#FF6040',
                                            data: <%= chartDataMonthlyConfirmed%>,
                                            dataLabels: {
                                                enabled: true,
                                                color: '#000',
                                                align: 'right',
                                                format: '{point.y}',
                                                y: 10, 
                                                style: {
                                                    fontSize: '13px',
                                                }
                                            }
                                        },
                     {
                         name: 'Present',
                         color:'#76c187',
                         data: <%= chartDataMonthlyPresent%>,
                         dataLabels: {
                             enabled: true,
                             color: '#000',
                             align: 'right',
                             format: '{point.y}',
                             y: 10, 
                             style: {
                                 fontSize: '13px',
                             }
                         }
                     },{
                         name: 'Absent',
                         visible:false,
                         color:'#9e7da6',
                         data: <%= chartDataMonthlyAbsent%>,
                         dataLabels: {
                             enabled: true,
                             color: '#000',
                             align: 'right',
                             format: '{point.y}',
                             y: 10, 
                             style: {
                                 fontSize: '13px',
                             }
                         }
                     }, 
                    
                     {
                         name: 'Late Arrival',
                         visible:false,
                         color:'#deb63b',
                         data: <%= chartDataMonthlyLateArrival%>,
                         dataLabels: {
                             enabled: true,
                             color: '#000',
                             align: 'right',
                             format: '{point.y}',
                             y: 10, 
                             style: {
                                 fontSize: '13px',
                             }
                         }
                     }, ];
      
                                        var json = {};   
                                        json.chart = chart; 
                                        json.title = title;   
                                        json.subtitle = subtitle; 
                                        json.tooltip = tooltip;
                                        json.xAxis = xAxis;
                                        json.yAxis = yAxis;  
                                        json.series = series;
                                        json.plotOptions = plotOptions;  
                                        json.credits = credits;
                                        $('#container').highcharts(json);
  
                                    });
                                </script>

                            </div>
                        </div>

                    </div>


                </div>
          </div>
        </div>
    </div>
</asp:Content>

