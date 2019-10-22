<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReleaseNotes.aspx.cs" Inherits="ReleaseNotes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Release Notes</title>
    <link href="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484214710/Bootstrap/bootstrap.min.css" rel="stylesheet" />
    <link href="https://res.cloudinary.com/deekyp5bi/raw/upload/v1484214710/css/Style.css" rel="stylesheet" />
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
            <div class="row" style="background-color:#1591cd !important;padding:10px">
                <div style="float:left">
                    <img src="https://res.cloudinary.com/deekyp5bi/image/upload/v1484220590/images/wms--logo.png" style="margin-top:13px;" />
               
                </div>
                <div style="float:right">
                    <img src="https://res.cloudinary.com/deekyp5bi/image/upload/v1484219853/images/spin_logo.png"/>
                </div>
            </div>
        <div>

            <div class="repository-content">

    
  <div class="subnav" data-pjax="">
    <div class="subnav-links float-left" role="navigation">
  

</div>


    <div class="float-right">
    </div>
  </div>

  <div class="release-timeline">






            <pre>
<b>Release Notes</b>
V6 Plus
 
Release Features
Feature #1
Body shop Integration in V6Plus
Feature #2
Speedo or Rapid bay configuration from devices
Feature #3
Employee TCM – shows the capacity of the employees based on service types & vehicle models
Feature #4
Job allotment – A highly compatible allotment with the trending scheduler. 
Feature #5
Parts module – Parts required for the individual vehicles & it availability time can be added
Feature #6
Reports – Modified few reports formulas
Feature #7
SMS – Welcome & Thank you SMS are sent to customer phone, SMS of Assigned vehicle details sent to particular Service advisor phone
Feature #8
DP Delight integration – Web application used by the DP to view the vehicle details, customer experience in the dashboard and SMS regarding vehicle details & cashier details is sent to given DP’s phone number.
  
Enhancements
Enhancement #1
Search option in employee details
Enhancement #2
Change VHR to WS
Enhancement #3
PDI / Unregistered Vehicle - Chassis Number to be mentioned in place of Reg. No.
Enhancement #4
CRE / SA – Address, Pan No, Tel No and Email ID are made mandatory At the Time of JC Entry
Enhancement #5
Service type is Mandatory while opening jobcard
Enhancement #6
Added estimated parts amount & labor amount in SA module
Enhancement #7
Registrations numbers are completely visible in position display.
Enhancement #8
Added total received/deliveries for the day in display 
Enhancement #9
Employee ID’s 6 digits are mandatory as per requirement
Enhancement #10
Pop up image (blue color) when technician’s allotted time is going to exceed 
Enhancement #11
Multiple road test & wash details are captured & shown in the front end
Enhancement #12
PDT is visible in JCR
Enhancement #13
JCR can extend or reduce the time allotted for technician

Enhancement #14
Separate remarks options are given to SA, JCR, and GM for particular technicians & vehicles
Enhancement #15
Cancelled vehicles are shown in KPI report & dashboard
Enhancement #16
SMS limits & number of messages used count details are shown in cashier login











 
Fixes
[Use this section for fixes to your product. This section should be used to describe fixes to currently existing features. In a company where defect or bug tracking is being used, you might also use conditional text during internal review to show which defect the fix is mapped to. The conditional text is turned off for external use.
Depending on your product, use the same structure to group your fixes. For example, if you have tabs in your product, use the name of that tab as a section header, then detail the fixes in that tab in that section.]
Fix #1
[Provide as much description of the fix as needed to convince the customer that the issue has been fixed.]



 
Known Issues and Problems
[This section covers issues and problems that are known about, but have a workaround. Often, this is where you include information about issues with operating systems, firewall configuration, or anti-virus programs.]
Issue #1
[Include a description of the issue.]
Workaround 
[Describe the workaround and any steps needed.]
[Example:]
[Product] requires Adobe Reader 8 or later to open the PDF files included in the software. Go to http://get.adobe.com/reader/ to get the latest version of Adobe Reader. 

            </pre>
        </div>
        </div>
    </form>
</body>
</html>
