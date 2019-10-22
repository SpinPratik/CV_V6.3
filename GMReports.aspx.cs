using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GMReports : System.Web.UI.Page
{
    private string daymnthyrTo;
    private string daymnthyr;
    private System.Data.DataTable DtRpt;
    private string ServiceAdvisor;
    private string WorksManager;
    private string IsBodyshop = "2", TLId, VehicleStatus;
    private string floorname = "";
    private string RegNo;
    private DateTime dtfrm;
    private DateTime dtTo;
    private string Aplus;
    private string DeliveryStatus;
    private bool AutoGen;
    private int year;
    private int month = 0;
    private int monthval = 0;
    private string EmpType;
    private string strMonth, strMonthTo;
    private string strYear, strYearTo;
    private string strDay, strDayTo;
    private static string rept_proc;
    private static string rept_rdlc;
    private static string file_name;
    private static string backto = "";
    private static string Copyright;
    private static string DealerName;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session[Session["TMLDealercode"] + "-TMLConString"].ToString() == "" || Session[Session["TMLDealercode"] + "-TMLConString"].ToString() == null)
            {
                Response.Redirect("login.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("login.aspx");
        }

        srcServiceType.ConnectionString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        GetEmpType.ConnectionString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        try
        {
            Session["CURRENT_PAGE"] = "Reports";
            this.Title = "Reports";

            if (!Page.IsPostBack)
            {
                Copyright = getVersion();
                GetReportList();
                FillYearCombo(ref cmbYearfrom);
                FillMonthCombo(ref cmbMnthfrom, Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
                FillDayCombo(ref cmbYearfrom, ref cmbMnthfrom, ref cmbDayFrm);
                FillYearCombo(ref CmbYearto);
                FillMonthCombo(ref cmbMnthTo, Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
                FillDayCombo(ref CmbYearto, ref cmbMnthTo, ref cmbDayTo);
                FillServiceAdvisor(ref cmbServiceAdvisor, Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
                FillTeamLead(ref cmbTeamLead, Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
                MultiView1.ActiveViewIndex = 0;
                MyAccordion.FadeTransitions = true;
                MyAccordion.RequireOpenedPane = true;
                MyAccordion.SelectedIndex = 0;
            }
            lblStstus.Text = "";
            DealerName = GetDealerDetails(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
            if (!Page.IsPostBack)
            {
                MyAccordion.SelectedIndex = 0;
                MyAccordion.RequireOpenedPane = true;

                if (Request.QueryString["autogen"] != null)
                {
                    AutoGen = true;
                    rept_proc = "";
                    rept_rdlc = "";
                    GenerateAutoreport();
                }
                else
                {
                    AutoGen = false;
                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("login.aspx");
        }
    }
    public string GetDealerDetails(string Connection)
    {
        try
        {
            SqlConnection con = new SqlConnection(Connection);
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "udpGetDealerDetails";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count != 0)
                return dt.Rows[0][0].ToString();
            else
                return "-Dealer Name, Place-";
        }
        catch (Exception ex)
        {
            return ">Dealer Name, Place<";
        }
    }
    public void FillTeamLead(ref DropDownList cmbTeamLeadFill, string sConnStrings)
    {
        cmbTeamLeadFill.Items.Clear();

        SqlConnection oConn = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());

        try
        {
            if (oConn.State != ConnectionState.Open)
            {
                oConn.Open();
            }
            DataSet oDs = new DataSet();

            SqlCommand cmd = new SqlCommand("", oConn);
            cmd.CommandText = "GetTeamLead";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter ODa = new SqlDataAdapter(cmd);
            ODa.Fill(oDs);

            ListItem lstItem;
            lstItem = new ListItem();
            lstItem.Text = "ALL";
            lstItem.Value = "-1";
            cmbTeamLeadFill.Items.Add(lstItem);
            for (int i = 0; i < oDs.Tables[0].Rows.Count; i++)
            {
                lstItem = new ListItem();
                lstItem.Text = oDs.Tables[0].Rows[i]["EmpName"].ToString();
                lstItem.Value = oDs.Tables[0].Rows[i]["EmpId"].ToString();
                cmbTeamLeadFill.Items.Add(lstItem);
            }
        }
        catch (Exception ex)
        {
        }
        finally
        {
            oConn.Close();
        }
    }

    public void FillServiceAdvisor(ref DropDownList cmbToFill, string sConnString)
    {
        cmbToFill.Items.Clear();

        SqlConnection oConn = new SqlConnection(sConnString);

        try
        {
            if (oConn.State != ConnectionState.Open)
            {
                oConn.Open();
            }
            DataSet oDs = new DataSet();

            SqlCommand cmd = new SqlCommand("", oConn);
            cmd.CommandText = "GetServiceAdvisorList";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter ODa = new SqlDataAdapter(cmd);
            ODa.Fill(oDs);

            ListItem lstItem;
            lstItem = new ListItem();
            lstItem.Text = "ALL";
            lstItem.Value = "-1";
            cmbToFill.Items.Add(lstItem);
            for (int i = 0; i < oDs.Tables[0].Rows.Count; i++)
            {
                lstItem = new ListItem();
                lstItem.Text = oDs.Tables[0].Rows[i]["EmpName"].ToString();
                lstItem.Value = oDs.Tables[0].Rows[i]["EmpName"].ToString();
                cmbToFill.Items.Add(lstItem);
            }
        }
        catch (Exception ex)
        {
        }
        finally
        {
            oConn.Close();
        }
    }

    public string getVersion()
    {
        string version = "";
        string sConnString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        SqlConnection con = new SqlConnection(sConnString);
        SqlDataAdapter sda = new SqlDataAdapter("select top 1 * from ProductVersion order by DTM desc", con);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        if (dt.Rows.Count > 0)
            version = dt.Rows[0][0].ToString();
        return version;
    }

    public void FillYearCombo(ref DropDownList cmbYear)
    {
        cmbYear.Items.Clear();
        ListItem lstItem;
        for (int i = 2009; i < DateTime.Now.Year + 1; i++)
        {
            lstItem = new ListItem();
            lstItem.Text = i.ToString();
            lstItem.Value = i.ToString();
            cmbYear.Items.Add(lstItem);
        }
        cmbYear.SelectedValue = DateTime.Now.Year.ToString();
    }
    public void FillMonthCombo(ref DropDownList cmbMonth, string conn)
    {
        cmbMonth.Items.Clear();
        ListItem lstItem;
        for (int i = 1; i < 13; i++)
        {
            lstItem = new ListItem();
            lstItem.Text = i.ToString();
            lstItem.Value = i.ToString();
            cmbMonth.Items.Add(lstItem);
        }
        cmbMonth.SelectedValue = DateTime.Now.Month.ToString();
    }

    public void FillDayCombo(ref DropDownList cmbYear, ref DropDownList cmbMonth, ref DropDownList cmbDay)
    {
        try
        {
            cmbDay.Items.Clear();
            int NoDays;
            int YearSelected;
            int MonthSelected;
            YearSelected = Convert.ToInt16(cmbYear.SelectedValue);
            MonthSelected = Convert.ToInt16(cmbMonth.SelectedValue);
            NoDays = System.DateTime.DaysInMonth(YearSelected, MonthSelected);
            ListItem lstItem;
            for (int i = 1; i < NoDays + 1; i++)
            {
                lstItem = new ListItem();
                lstItem.Text = i.ToString();
                lstItem.Value = i.ToString();
                cmbDay.Items.Add(lstItem);
            }
            if (cmbMonth.SelectedValue == DateTime.Now.Month.ToString())
            {
                cmbDay.SelectedValue = DateTime.Now.Day.ToString();
            }
        }
        catch (Exception ex)
        {
        }
    }

    private void GenerateAutoreport()
    {
        try
        {
            try
            {
                if (Directory.Exists(@"" + ConfigurationManager.AppSettings["MailDirectory"].ToString()))
                {
                    String[] dir = Directory.GetDirectories(@"" + ConfigurationManager.AppSettings["MailDirectory"].ToString());
                    foreach (String strPath in dir)
                    {
                        try
                        {
                            Directory.Delete(strPath);
                        }
                        catch { }
                    }
                }
                else
                {
                    Directory.CreateDirectory(@"" + ConfigurationManager.AppSettings["MailDirectory"].ToString());
                }
            }
            catch (Exception ex)
            {
            }
            String path = @"" + ConfigurationManager.AppSettings["MailDirectory"].ToString() + @"\" + System.DateTime.Now.ToString("dd_MM_yyyy");
            if (Directory.Exists(path))
            {
            }
            else
            {
                System.IO.DirectoryInfo dir = Directory.CreateDirectory(path);
            }

            // Set the path for saving these xls & csv files
            SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT SlNo, ReportName, Rept_Procedure, Rept_Rdlc, FileName FROM tbl_ReportMenu WHERE (Activ = 'YES') AND Autogen = 1 ORDER BY Ordr", con);
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ArrayList fileList = getFileList();
                    if (fileList.Contains(dt.Rows[i]["FileName"]))
                    {
                        GenerateSelectedReport(dt.Rows[i]["SlNo"].ToString(), dt.Rows[i]["Rept_Rdlc"].ToString(), dt.Rows[i]["Rept_Procedure"].ToString(), dt.Rows[i]["FileName"].ToString());
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error 1 :" + ex.Message;
        }
        finally
        {
        }
    }

    protected void GetReportList()
    {
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("UdpGetGMReportDetails", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        sda.Fill(dt);
        CmbRptType.DataSource = dt;
        CmbRptType.DataTextField = "ReportName";
        CmbRptType.DataValueField = "SlNo";
        CmbRptType.DataBind();
    }

    public DataTable GetFrontOfficeReportDataTable(DateTime DtFrom, DateTime DtTo, string ServiceAdvisor, string StProc)
    {

        SqlConnection oConn = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlCommand cmd = new SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = StProc;

        cmd.Parameters.AddWithValue("@SAName", ServiceAdvisor);
        cmd.Parameters.AddWithValue("@dateFrom", DtFrom);
        cmd.Parameters.AddWithValue("@ToDate", DtTo);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }


    private void GenerateSelectedReport(string ReportName, string rept_rdlc, string rept_proc, string file_name)
    {
        try
        {
            RptViewer.Reset();
            if (AutoGen == true)
            {
                dtfrm = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                dtTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
            }
            else
            {
                dtfrm = new DateTime(Convert.ToInt16(cmbYearfrom.SelectedValue), Convert.ToInt16(cmbMnthfrom.SelectedValue), Convert.ToInt16(cmbDayFrm.SelectedValue), 0, 0, 0);
                dtTo = new DateTime(Convert.ToInt16(CmbYearto.SelectedValue), Convert.ToInt16(cmbMnthTo.SelectedValue), Convert.ToInt16(cmbDayTo.SelectedValue), 23, 59, 00);
            }

            DeliveryStatus = "";
            if (cmbServiceAdvisor.SelectedValue == "-1")
            {
                ServiceAdvisor = "";
            }
            else
            {
                ServiceAdvisor = cmbServiceAdvisor.SelectedValue;
            }
            string ServiceType = string.Empty;
            if (cmbServiceType.SelectedValue == "ALL")
            {
                ServiceType = "";
            }
            else
            {
                ServiceType = cmbServiceType.SelectedValue;
            }

            if (cmbEmpType.SelectedValue == "ALL")
            {
                EmpType = "";
            }
            else
            {
                EmpType = cmbEmpType.SelectedValue;
            }
            //string VehicleStatus = string.Empty;
            if (drpWhiteBoard.SelectedValue == "ALL")
            {
                VehicleStatus = "2";
            }
            else
            {
                VehicleStatus = drpWhiteBoard.SelectedValue;
            }

            WorksManager = "";
            if (cmbTeamLead.SelectedIndex != 0)
            {
                TLId = cmbTeamLead.SelectedValue;
            }
            else
            {
                TLId = "";
            }

            switch (CmbAplus.SelectedValue)
            {
                case "ALL":
                    Aplus = "";
                    break;

                default:
                    Aplus = CmbAplus.SelectedValue;
                    break;
            }
            switch (cmbDeliveryStatus.SelectedValue)
            {
                case "UNDELIVERED":
                    DeliveryStatus = "0";
                    break;

                case "DELIVERED":
                    DeliveryStatus = "1";
                    break;

                case "ALL":
                    DeliveryStatus = "";
                    break;
            }

            if (cmbMnthfrom.SelectedValue.Length == 1)
            {
                strMonth = "0" + cmbMnthfrom.SelectedValue;
            }
            else
            {
                strMonth = cmbMnthfrom.SelectedValue;
            }
            if (cmbMnthTo.SelectedValue.Length == 1)
            {
                strMonthTo = "0" + cmbMnthTo.SelectedValue;
            }
            else
            {
                strMonthTo = cmbMnthTo.SelectedValue;
            }
            if (cmbDayFrm.SelectedValue.Length == 1)
            {
                strDay = "0" + cmbDayFrm.SelectedValue;
            }
            else
            {
                strDay = cmbDayFrm.SelectedValue;
            }

            if (cmbDayTo.SelectedValue.Length == 1)
            {
                strDayTo = "0" + cmbDayTo.SelectedValue;
            }
            else
            {
                strDayTo = cmbDayTo.SelectedValue;
            }
            string mnthyr = cmbYearfrom.SelectedValue + strMonth;

            daymnthyr = strMonth + "/" + strDay + "/" + cmbYearfrom.SelectedValue;
            daymnthyrTo = strMonthTo + "/" + strDayTo + "/" + CmbYearto.SelectedValue;

            RegNo = txtRegNo.Text;
            if (rept_proc.Length > 16)
            {
                switch (ReportName)
                {
                    case "1":
                        FillReport(DtRpt, rept_rdlc, rept_proc, file_name);
                        break;

                    case "2":
                        DtRpt = GetPDTADTReportDataTable(rept_proc.Substring(16));
                        FillAdditionalReport(DtRpt, rept_rdlc, rept_proc, file_name);
                        break;

                    case "3":
                        DtRpt = GetPDTADTReportDataTable(rept_proc.Substring(16));
                        FillAdditionalReport(DtRpt, rept_rdlc, rept_proc, file_name);
                        break;

                    case "4":
                        DtRpt = GetPDTADTReportDataTable(rept_proc.Substring(16));
                        FillAdditionalReport(DtRpt, rept_rdlc, rept_proc, file_name);
                        break;

                    case "5":
                        FillInstantFeedBack(rept_rdlc, rept_proc, file_name);
                        break;

                    case "6":
                        FillDailyTechnicianPerformance(rept_rdlc, rept_proc, file_name);
                        break;

                    case "7":
                        DtRpt = GetEmployeeattndnc(daymnthyr, rept_proc.Substring(16));
                        FillTrendsReport(DtRpt, rept_rdlc, rept_proc, file_name, EmpType, daymnthyr);
                        break;

                    case "8":
                        DtRpt = Getjobtrend(dtfrm, dtTo, rept_proc.Substring(16));
                        FillAdditionalReport(DtRpt, rept_rdlc, rept_proc, file_name);
                        break;

                    case "9":
                        DtRpt = Getjobtrend(dtfrm, dtTo, rept_proc.Substring(16));
                        FillAdditionalReport(DtRpt, rept_rdlc, rept_proc, file_name);
                        break;

                    case "10":
                        DtRpt = GetSAFilterReportDataTable(rept_proc.Substring(16));
                        FillTrendsReport(DtRpt, rept_rdlc, rept_proc, file_name);
                        break;

                    case "11":
                        DtRpt = GetReportDataTable(rept_proc.Substring(16));
                        FillTrendsReport(DtRpt, rept_rdlc, rept_proc, file_name);
                        break;

                    case "12":
                        //DtRpt = GetSAFilterReportDataTable(dtfrm, dtTo, ServiceAdvisor, rept_proc.Substring(16));
                        //FillAdditionalReport(DtRpt, rept_rdlc, rept_proc, file_name);
                        FillCarryForwardVehReport(rept_rdlc, rept_proc, file_name, ServiceAdvisor);
                        break;

                    case "13":
                        DtRpt = getCardNotScanned(dtfrm, dtTo, rept_proc.Substring(16));
                        FillAdditionalReport(DtRpt, rept_rdlc, rept_proc, file_name);
                        break;

                    case "14":
                        DtRpt = GetReportDataTable(rept_proc.Substring(16));
                        FillTrendsReport(DtRpt, rept_rdlc, rept_proc, file_name);
                        break;

                    case "15":
                        DtRpt = GetReportDataTable(rept_proc.Substring(16));
                        FillTrendsReport(DtRpt, rept_rdlc, rept_proc, file_name);
                        break;

                    case "16":
                        DtRpt = GetReportDataTable(rept_proc.Substring(16));
                        FillTrendsReport(DtRpt, rept_rdlc, rept_proc, file_name);
                        break;

                    case "17":
                        DtRpt = GetReportDataTable(rept_proc.Substring(16));
                        FillTrendsReport(DtRpt, rept_rdlc, rept_proc, file_name);
                        break;

                    case "18":
                        DtRpt = GetEmployeeReportDataTable_EMPTMR(dtfrm, dtTo, EmpType, "", TLId, "0", "EmployeeTimeMonitoring", Convert.ToInt16(VehicleStatus), Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
                        FillETMReport(DtRpt, rept_rdlc, rept_proc, file_name);
                        break;

                    case "19":
                        DtRpt = GetReportDataTable(rept_proc.Substring(16));
                        FillTrendsReport(DtRpt, rept_rdlc, rept_proc, file_name);
                        break;

                    case "20":
                        FillSameDayReport(rept_rdlc, rept_proc, file_name, ServiceAdvisor);
                        break;

                    case "21":
                        DtRpt = GetSAFilterReportDataTable(dtfrm, dtTo, ServiceAdvisor, rept_proc.Substring(16));
                        FillTrendsReport(DtRpt, rept_rdlc, rept_proc, file_name);
                        break;

                    case "22":
                        DtRpt = GetSAFilterReportDataTable(dtfrm, dtTo, ServiceAdvisor, rept_proc.Substring(16));
                        FillTrendsReport1(DtRpt, rept_rdlc, rept_proc, file_name);
                        break;

                    case "23":
                        DtRpt = GetTechnicianReport(month, year);
                        FillTrendsReport(DtRpt, rept_rdlc, rept_proc, file_name);
                        break;

                    case "24":
                        DtRpt = GetReportDataTable(dtfrm, dtTo, RegNo, ServiceType, rept_proc.Substring(16));
                        FillAdditionalReport(DtRpt, rept_rdlc, rept_proc, file_name);
                        break;

                    case "25":
                        DtRpt = GetReportDataTableAFD(dtfrm, dtTo, RegNo, ServiceType, ServiceAdvisor, rept_proc.Substring(16));
                        FillAdditionalReportAFD(DtRpt, rept_rdlc, rept_proc, file_name);
                        break;

                    case "26":
                        FillReport(DtRpt, rept_rdlc, rept_proc, file_name);
                        break;

                    case "27"://Front Office Report
                        DtRpt = GetFrontOfficeReportDataTable(dtfrm, dtTo, ServiceAdvisor, rept_proc.Substring(16));
                        FillFrontOfficeReport(DtRpt, dtfrm, dtTo, ServiceAdvisor, rept_rdlc, rept_proc, file_name);
                        break;

                    case "28"://Get Technician Avg Daily Report
                        DtRpt = GetTechAvgDailyTrack(rept_proc.Substring(16));
                        FillTechnicalAVGDailyTrackReport(DtRpt, rept_rdlc, rept_proc, file_name);
                        break;

                    case "29"://Quick Repair
                        DtRpt = GetExpressServiceAndPartsTMRAndRoadTestTMR(rept_proc.Substring(16), dtfrm, dtTo);
                        FillExpressServiceAndPartsTMRAndRoadTestTMRReport(DtRpt, rept_rdlc, rept_proc, file_name);
                        break;

                    case "30"://Parts Time Monitoring Report
                        DtRpt = GetExpressServiceAndPartsTMRAndRoadTestTMR(rept_proc.Substring(16), dtfrm, dtTo);
                        FillExpressServiceAndPartsTMRAndRoadTestTMRReport(DtRpt, rept_rdlc, rept_proc, file_name);
                        break;

                    case "31"://Road Test Time Monitoring Report
                        DtRpt = GetExpressServiceAndPartsTMRAndRoadTestTMR(rept_proc.Substring(16), dtfrm, dtTo);
                        FillExpressServiceAndPartsTMRAndRoadTestTMRReport(DtRpt, rept_rdlc, rept_proc, file_name);
                        break;

                    case "32"://Machanic Productivity Summary Report
                        DtRpt = GetMachanicProductivity(rept_proc.Substring(16), month.ToString(), year.ToString());
                        FillMachanicProductivityReport(DtRpt, rept_rdlc, rept_proc, file_name);
                        break;

                    case "33"://Technician Time Monitoring Report 1 Tech
                        DtRpt = GetTechTMR1Tech(rept_proc.Substring(16), dtfrm.ToShortDateString());
                        FillTechTMRReport(DtRpt, rept_rdlc, rept_proc, file_name);
                        break;

                    case "34"://Planned vs Actual Report
                        DtRpt = GetPlannedVsActual(rept_proc.Substring(16), dtfrm);
                        FillVehicleTMR(DtRpt, rept_rdlc, rept_proc, file_name);
                        break;

                    case "35"://Time Monitoring Report Comprehensive
                        FillReport(DtRpt, rept_rdlc, rept_proc, file_name);
                        break;

                    case "36"://Daily Attendance Report
                        DtRpt = GetMonthlyAttendance("", cmbMnthfrom.SelectedItem.Text.Trim(), cmbYearfrom.SelectedItem.Text.Trim(), rept_proc.Substring(16));
                        FillAttendanceReport(DtRpt, rept_rdlc, rept_proc, file_name, "");
                        break;

                    case "37": //VEHICLE IN OUT FLOW TREND
                        DtRpt = GetVehicleInOut(rept_proc.Substring(16), dtfrm.ToShortDateString());
                        FillTechTMRReport(DtRpt, rept_rdlc, rept_proc, file_name);
                        break;

                    case "38": // TMR NEW
                        DtRpt = GetReportDataTable1(dtfrm, dtTo, ServiceAdvisor, Aplus, WorksManager, RegNo, DeliveryStatus, ServiceType, "", TLId, "0", rept_proc.Substring(16), Convert.ToInt16(VehicleStatus));
                        FillReport_TMR(DtRpt, rept_rdlc, rept_proc, file_name);
                        break;

                    case "39": // ADT REPORT New
                        FillADTReport(rept_proc, rept_rdlc, file_name);
                        break;

                    case "40": // WORKSHOP OVERVIEW
                        FillWorkshopAnalysisReport(rept_proc, rept_rdlc, file_name);
                        break;

                    case "41": // Quick Repair TREND
                        DtRpt = GetReportDataTable(rept_proc.Substring(16));
                        FillTrendsReport(DtRpt, rept_rdlc, rept_proc, file_name);
                        break;

                    case "42": // TURN AROUND TIME TREND
                        DtRpt = GetReportDataTable(rept_proc.Substring(16));
                        FillAdditionalReport(DtRpt, rept_rdlc, rept_proc, file_name);
                        break;

                    case "43": // SERVICE TYPE TREND
                        DtRpt = GetReportDataTable(rept_proc.Substring(16));
                        FillAdditionalReport(DtRpt, rept_rdlc, rept_proc, file_name);
                        break;

                    case "44"://Technician Time Monitoring Report 2 Tech
                        DtRpt = GetTechTMR2Tech(rept_proc.Substring(16), dtfrm.ToShortDateString());
                        FillTechTMRReport(DtRpt, rept_rdlc, rept_proc, file_name);
                        break;

                    case "45":
                        DtRpt = GetReportDataTable(dtfrm, dtTo, ServiceAdvisor, Aplus, WorksManager, RegNo, DeliveryStatus, ServiceType, "", TLId, "0", rept_proc.Substring(16));
                        FillReport(DtRpt, rept_rdlc, rept_proc, file_name);
                        break;

                    case "46":
                        DtRpt = GetReportDataTable(rept_proc.Substring(16));
                        FillAdditionalReport(DtRpt, rept_rdlc, rept_proc, file_name);
                        break;

                    case "47":
                        DtRpt = GetReportDataTable(rept_proc.Substring(16));
                        FillAdditionalReport(DtRpt, rept_rdlc, rept_proc, file_name);
                        break;

                    case "48":
                        DtRpt = GetVehicleInOutMonthlyTrend(rept_proc.Substring(16), dtfrm.ToShortDateString(), ServiceAdvisor);
                        FillAdditional1ParaReport(DtRpt, rept_rdlc, rept_proc, file_name, ServiceAdvisor);
                        break;

                    case "49":
                        DtRpt = GetStageWiseProductivityDataTable(rept_proc.Substring(16), dtfrm.ToShortDateString(), ServiceAdvisor);
                        FillSWPReport(DtRpt, rept_rdlc, rept_proc, file_name, ServiceAdvisor);
                        break;

                    case "50": // DELAY ANALYSIS REPORT
                        if (AutoGen == true)
                        {
                            dtfrm = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
                        }
                        DtRpt = GetDelayAnalysisReportDataTable(rept_proc.Substring(16), dtfrm.ToShortDateString(), dtTo.ToShortDateString(), ServiceAdvisor, ServiceType);
                        FillDelayAnalysisReport(DtRpt, dtfrm, dtTo, ServiceAdvisor, ServiceType, rept_rdlc, rept_proc, file_name);
                        break;

                    case "51": // DELIVERY ANALYSIS REPORT
                        DtRpt = GetDeliveryAnalysisReportDataTable(rept_proc.Substring(16), dtfrm.ToShortDateString(), dtTo.ToShortDateString(), ServiceAdvisor);
                        FillDeliveryAnalysisReport(DtRpt, dtfrm, dtTo, ServiceAdvisor, rept_rdlc, rept_proc, file_name);
                        break;

                    case "52": // VEHICLE AGING RATIO
                        DtRpt = GetReportDataTable2(rept_proc.Substring(16));
                        FillTrendsReport(DtRpt, rept_rdlc, rept_proc, file_name);
                        break;

                    case "53":
                        if (AutoGen == true)
                        {
                            dtfrm = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
                        }
                        DtRpt = GetDeliveryAnalysisReportDataTable(rept_proc.Substring(16), dtfrm.ToShortDateString(), dtTo.ToShortDateString(), ServiceAdvisor);
                        FillDeliveryAnalysisReport(DtRpt, dtfrm, dtTo, ServiceAdvisor, rept_rdlc, rept_proc, file_name);
                        break;

                    case "54"://Bay TMR
                        DtRpt = GetTechTMR1TechNew(rept_proc.Substring(16), dtfrm.ToShortDateString());
                        FillTechTMRReportNew(DtRpt, rept_rdlc, rept_proc, file_name);
                        break;

                    case "55": // PROCESS & IDLE TIME ANALYSIS WRT ST
                        DtRpt = GetReportDataTable(rept_proc.Substring(16));
                        FillAdditionalReport(DtRpt, rept_rdlc, rept_proc, file_name);
                        break;

                    case "56": // SUMMARY REPORT
                        FillSummaryReport(rept_proc, rept_rdlc, file_name);
                        break;

                    case "57"://DELIVERY ANALYSIS REPORT SA WISE
                        DtRpt = GetDeliveryAnalysisReportSAWise(rept_proc.Substring(16), dtfrm.ToShortDateString());
                        FillSWPReport(DtRpt, rept_rdlc, rept_proc, file_name, ServiceAdvisor);
                        break;

                    case "58"://DELIVERY ANALYSIS REPORT New
                        FillDeliveryAnalysisxReport(rept_proc, rept_rdlc, file_name, ServiceAdvisor);
                        break;

                    case "59"://CRM Dashboard
                        FillDashboardReport(rept_proc, rept_rdlc, file_name);
                        break;

                    case "60"://GM SERVICES Dashboard
                        FillDashboardReport(rept_proc, rept_rdlc, file_name);
                        break;

                    case "61"://DEALER PRINCIPLE Dashboard
                        FillDashboardReport(rept_proc, rept_rdlc, file_name);
                        break;

                    case "62"://Service Manager Dashboard
                        FillDashboardReport(rept_proc, rept_rdlc, file_name);
                        break;

                    case "63"://JOB CONTROLLER Dashboard
                        FillDashboardReport(rept_proc, rept_rdlc, file_name);
                        break;

                    case "64"://Pending Vehicle Details
                        DtRpt = GetSAFilterReportDataTable(rept_proc.Substring(16));
                        FillTrendsReport(DtRpt, rept_rdlc, rept_proc, file_name);
                        break;

                    case "65":
                        DtRpt = GetSAPerformanceDataTable(rept_proc.Substring(16), dtfrm.ToShortDateString(), ServiceAdvisor);
                        FillAdditional1ParaReport(DtRpt, rept_rdlc, rept_proc, file_name, ServiceAdvisor);
                        break;

                    case "66":
                        DtRpt = GetVehicelRemarksDataTable(rept_proc.Substring(16));
                        FillRemarksReport(DtRpt, rept_rdlc, rept_proc, file_name);
                        break;

                    case "67":
                        FillBillingReport(rept_proc, rept_rdlc, file_name);
                        break;

                    case "68":
                        DtRpt = GetTechTMR1Tech(rept_proc.Substring(16), dtfrm.ToShortDateString());
                        FillTechTMRReport(DtRpt, rept_rdlc, rept_proc, file_name);
                        break;

                    case "69":
                        FillCardScanAdherenceReport(rept_rdlc, rept_proc, file_name, ServiceAdvisor);
                        break;

                    case "70"://Employee Dashboard
                        DtRpt = FillEmployeeDashboard(rept_proc.Substring(16), dtTo.ToShortDateString(), "0");
                        FillEmployeeDashboardReport(DtRpt, rept_rdlc, rept_proc, file_name);
                        break;

                    case "71"://Bay Dashboard
                        DtRpt = FillEmployeeDashboard(rept_proc.Substring(16), dtfrm.ToShortDateString(), "0");
                        FillEmployeeDashboardReport(DtRpt, rept_rdlc, rept_proc, file_name);
                        break;

                    case "72"://Common Dashboard
                        FillCommonDashboardReport(rept_proc, rept_rdlc, file_name);
                        break;

                    case "73":
                        FillCommonDashboardReport1(rept_proc, rept_rdlc, file_name);
                        break;

                    //DtRpt = FillEmployeeDashboard(rept_proc.Substring(16), dtfrm.ToShortDateString(), IsBodyshop);
                    //FillCSMDashboardReport(DtRpt, rept_rdlc, rept_proc, file_name);
                    //break;

                    case "74"://KPI DASHBOARD
                        FillCommonDashboardReport(rept_proc, rept_rdlc, file_name);
                        break;
                    case "75":
                        FillCommonDashboardReport1(rept_proc, rept_rdlc, file_name);
                        break;
                    case "76":
                        DtRpt = GetPartsData(rept_proc.Substring(16), dtfrm.ToShortDateString());
                        FillPartsReport(DtRpt, rept_rdlc, rept_proc, file_name, ServiceAdvisor);
                        break;
                    case "77":
                        DtRpt = GetRptVehicleData(rept_proc.Substring(16), dtfrm.ToShortDateString());
                        FillVehicleTOTReport(DtRpt, rept_rdlc, rept_proc, file_name, ServiceAdvisor);
                        break;
                    case "78":
                        DtRpt = FillCustomerDetails(rept_proc.Substring(16), dtfrm.ToShortDateString(), dtTo.ToShortDateString());
                        FillCustomerDetailsReport(DtRpt, rept_rdlc, rept_proc, file_name);
                        break;
                    case "79":
                        DtRpt = FillCustomerDetails(rept_proc.Substring(16), dtfrm.ToShortDateString(), dtTo.ToShortDateString());
                        FillCustomerDetailsReport(DtRpt, rept_rdlc, rept_proc, file_name);
                        break;
                }
            }
            else
            {
                lblStstus.Text = "Please Re-select Report Name.";
                CmbRptType.SelectedIndex = 0;
                return;
            }
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error in processing the request. " + ex.Message;
            RptViewer.Reset();
        }
    }
    public DataTable GetReportDataTable(DateTime DtFrom, DateTime DtTo, string ServiceAdvisor, string ServiceType, string WorksManager, string regNo, string Deliverystatus, string ServiceTypes, string floorname, string teamleadid, string isbodyshop, string StProc)
    {
        string sConnString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        SqlConnection oConn = new SqlConnection(sConnString);
        SqlCommand cmd = new SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = StProc;
        cmd.Parameters.AddWithValue("@regno", regNo);
        cmd.Parameters.AddWithValue("@SAName", ServiceAdvisor);
        cmd.Parameters.AddWithValue("@WorksManager", WorksManager);
        cmd.Parameters.AddWithValue("@Aplus", ServiceType);
        cmd.Parameters.AddWithValue("@Datefrom", DtFrom);
        cmd.Parameters.AddWithValue("@Dateto", DtTo);
        cmd.Parameters.AddWithValue("@DeliveryStatus", Deliverystatus);
        cmd.Parameters.AddWithValue("@ServiceType", ServiceTypes);
        cmd.Parameters.AddWithValue("@FloorName", floorname);
        cmd.Parameters.AddWithValue("@TLId", teamleadid);
        cmd.Parameters.AddWithValue("@IsBodyshop", isbodyshop);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    public DataTable GetReportDataTable1(DateTime DtFrom, DateTime DtTo, string ServiceAdvisor, string ServiceType, string WorksManager, string regNo, string Deliverystatus, string ServiceTypes, string floorname, string teamleadid, string isbodyshop, string StProc, int Status)
    {

        SqlConnection oConn = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlCommand cmd = new SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = StProc;
        cmd.Parameters.AddWithValue("@regno", regNo);
        cmd.Parameters.AddWithValue("@SAName", ServiceAdvisor);
        cmd.Parameters.AddWithValue("@WorksManager", WorksManager);
        cmd.Parameters.AddWithValue("@Aplus", ServiceType);
        cmd.Parameters.AddWithValue("@Datefrom", DtFrom);
        cmd.Parameters.AddWithValue("@Dateto", DtTo);
        cmd.Parameters.AddWithValue("@DeliveryStatus", Deliverystatus);
        cmd.Parameters.AddWithValue("@ServiceType", ServiceTypes);
        cmd.Parameters.AddWithValue("@FloorName", floorname);
        cmd.Parameters.AddWithValue("@TLId", teamleadid);
        cmd.Parameters.AddWithValue("@IsBodyshop", isbodyshop);
        cmd.Parameters.AddWithValue("@IsWhiteBoard", Status);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }
    public DataTable GetEmployeeReportDataTable_EMPTMR(DateTime DtFrom, DateTime DtTo, string emptype, string floorname, string TLId, string IsBodyshop, string StProc, int VehicleStatus, string ConnectionString)
    {

        SqlConnection oConn = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlCommand cmd = new SqlCommand("", oConn);
        if (oConn.State != ConnectionState.Open)
            oConn.Open();
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = StProc;
        cmd.Parameters.AddWithValue("@Datefrom", DtFrom);
        //cmd.Parameters.AddWithValue("@Dateto", DtTo);
        cmd.Parameters.AddWithValue("@EmployeeType", emptype);
        cmd.Parameters.AddWithValue("@IsBodyshop", IsBodyshop);
        cmd.Parameters.AddWithValue("@VehicleStatus", VehicleStatus);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    private void FillCustomerDetailsReport(DataTable Dt, string ReportRdlcName, string ReportName, string FileName)
    {
        string mimeType;
        string encoding;
        string fileNameExtension;
        Microsoft.Reporting.WebForms.Warning[] warnings;
        string[] streamids;
        byte[] exportBytes = null;
        try
        {
            if (Dt.Rows.Count < 1)
            {
                lblStstus.Text = "Your Search Criteria did not match any documents.<br>Try different Search Criteria.";
                RptViewer.Reset();
                return;
            }
            MyAccordion.RequireOpenedPane = false;
            MyAccordion.SelectedIndex = -1;
            RptViewer.Reset();                                                                        // reset the report viewer
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;             // Set to Local Reporting Mode
            RptViewer.ShowExportControls = true;                                                   // Show The Export Controls
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + ReportRdlcName;
            Microsoft.Reporting.WebForms.ReportParameter rParam1 = new Microsoft.Reporting.WebForms.ReportParameter("FromDate", (dtfrm.ToString("dd-MM-yyyy")));
            Microsoft.Reporting.WebForms.ReportParameter rParam2 = new Microsoft.Reporting.WebForms.ReportParameter("ToDate", (dtTo.ToString("dd-MM-yyyy")));
            Microsoft.Reporting.WebForms.ReportParameter rParam6 = new Microsoft.Reporting.WebForms.ReportParameter("Copyright", Copyright);
            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            ReportDS.Name = ReportName;
            ReportDS.Value = Dt;
            localRpt.DisplayName = file_name;
            localRpt.DataSources.Clear();
            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1, rParam2, rParam6 });
            //this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam6 });
            localRpt.DataSources.Add(ReportDS);
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
            ReportName = ReportName.Substring(16);
            if (AutoGen == true)
            {
                WriteToExcelFile(exportBytes, FileName);
            }
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Please Try again later";
            RptViewer.Reset();
            throw (ex);
        }
    }

    public DataTable FillCustomerDetails(string StProc, string FromDate, string ToDate)
    {


        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        //System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(sConnString);
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@FromDate", FromDate);
        cmd.Parameters.AddWithValue("@ToDate", ToDate);

        cmd.CommandText = StProc;
        System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    private void FillBillingReport(string rept_proc, string ReportRdlcName, string ReportName)
    {
        string mimeType;
        string encoding;
        string fileNameExtension;
        Microsoft.Reporting.WebForms.Warning[] warnings;
        string[] streamids;
        byte[] exportBytes = null;
        String[] Proc = rept_proc.Substring(16).Replace("ProTrackDataSet_", "").Split(',');
        try
        {
            RptViewer.Reset();                                                                        // reset the report viewer
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;             // Set to Local Reporting Mode
            RptViewer.ShowExportControls = true;                                                   // Show The Export Controls
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + ReportRdlcName;
            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            localRpt.DataSources.Clear();
            for (int flag = 0; flag < Proc.Length; flag++)
            {
                localRpt.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ProTrackDataSet_" + Proc[flag].Trim(), GetVehicelRemarksDataTable(Proc[flag].Trim())));
            }
            Microsoft.Reporting.WebForms.ReportParameter rParam1 = new Microsoft.Reporting.WebForms.ReportParameter("Date", dtfrm.ToString("dd-MM-yyyy"));
            Microsoft.Reporting.WebForms.ReportParameter rParam2 = new Microsoft.Reporting.WebForms.ReportParameter("Copyright", Copyright);
            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1, rParam2 });
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
            if (AutoGen == true)
            {
                WriteToExcelFile(exportBytes, ReportName);
            }
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error in processing the request.";
            RptViewer.Reset();
            throw (ex);
        }
    }

    private void FillRemarksReport(System.Data.DataTable Dt, string ReportRdlcName, string ReportName, string FileName)
    {
        string mimeType;
        string encoding;
        string fileNameExtension;
        Microsoft.Reporting.WebForms.Warning[] warnings;
        string[] streamids;
        byte[] exportBytes = null;
        try
        {
            if (Dt.Rows.Count < 1)
            {
                lblStstus.Text = "Your Search Criteria did not match any documents.<br>Try different Search Criteria.";
                RptViewer.Reset();
                return;
            }
            MyAccordion.RequireOpenedPane = false;
            MyAccordion.SelectedIndex = -1;
            RptViewer.Reset();
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;             // Set to Local Reporting Mode
            RptViewer.ShowExportControls = true;                                                   // Show The Export Controls
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + ReportRdlcName;
            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            ReportDS.Name = ReportName;
            ReportDS.Value = Dt;
            localRpt.DisplayName = file_name;
            localRpt.DataSources.Clear();
            Microsoft.Reporting.WebForms.ReportParameter rParam1 = new Microsoft.Reporting.WebForms.ReportParameter("Date", dtfrm.ToString("dd-MM-yyyy"));
            Microsoft.Reporting.WebForms.ReportParameter rParam2 = new Microsoft.Reporting.WebForms.ReportParameter("Copyright", Copyright);
            Microsoft.Reporting.WebForms.ReportParameter rParam3 = new Microsoft.Reporting.WebForms.ReportParameter("VehicleStatus", drpWhiteBoard.Items[drpWhiteBoard.SelectedIndex].Text);
            localRpt.DataSources.Add(ReportDS);
            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1, rParam2, rParam3 });
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
            if (AutoGen == true)
            {
                WriteToExcelFile(exportBytes, FileName);
            }
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error in processing the request.";
            RptViewer.Reset();
            throw (ex);
        }
    }

    private void FillDashboardReport(string rept_proc, string ReportRdlcName, string ReportName)
    {
        string mimeType;
        string encoding;
        string fileNameExtension;
        Microsoft.Reporting.WebForms.Warning[] warnings;
        string[] streamids;
        byte[] exportBytes = null;
        String[] Proc = rept_proc.Substring(16).Replace("ProTrackDataSet_", "").Split(',');
        try
        {
            RptViewer.Reset();
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local; // Set to Local Reporting Mode
            RptViewer.ShowExportControls = true;
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + ReportRdlcName;
            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            localRpt.DataSources.Clear();
            for (int flag = 0; flag < Proc.Length; flag++)
            {
                localRpt.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ProTrackDataSet_" + Proc[flag].Trim(), GetDashboardDataTable(Proc[flag].Trim())));
            }
            DataTable info1 = getInfo("0");
            Microsoft.Reporting.WebForms.ReportParameter rParam1 = new Microsoft.Reporting.WebForms.ReportParameter("DealerName", info1.Rows[0][0].ToString());
            Microsoft.Reporting.WebForms.ReportParameter rParam2 = new Microsoft.Reporting.WebForms.ReportParameter("DealerCity", info1.Rows[0][1].ToString());
            Microsoft.Reporting.WebForms.ReportParameter rParam5 = new Microsoft.Reporting.WebForms.ReportParameter("SpeedoBay", info1.Rows[0]["SpeedoBay"].ToString());
            Microsoft.Reporting.WebForms.ReportParameter rParam6 = new Microsoft.Reporting.WebForms.ReportParameter("TotalEmpService", info1.Rows[0]["TotalEmp"].ToString());
            Microsoft.Reporting.WebForms.ReportParameter rParam7 = new Microsoft.Reporting.WebForms.ReportParameter("TotalBay", info1.Rows[0]["TotalBay"].ToString());
            Microsoft.Reporting.WebForms.ReportParameter rParam8 = new Microsoft.Reporting.WebForms.ReportParameter("TotalSAService", info1.Rows[0]["TotalSA"].ToString());
            Microsoft.Reporting.WebForms.ReportParameter rParamCopyright = new Microsoft.Reporting.WebForms.ReportParameter("Copyright", Copyright);
            DataTable info2 = getInfo("1");
            Microsoft.Reporting.WebForms.ReportParameter rParam9 = new Microsoft.Reporting.WebForms.ReportParameter("TotalSABodyshop", info2.Rows[0]["TotalSA"].ToString());
            Microsoft.Reporting.WebForms.ReportParameter rParam4 = new Microsoft.Reporting.WebForms.ReportParameter("TotalEmpBodyshop", info2.Rows[0]["TotalEmp"].ToString());
            Microsoft.Reporting.WebForms.ReportParameter rParam3 = new Microsoft.Reporting.WebForms.ReportParameter("BodyshopBay", info2.Rows[0]["BodyshopBay"].ToString());
            if (ReportName == "CRMDashboard")
            {
                this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1, rParam2, rParam8, rParam9, rParamCopyright });
            }
            else if (ReportName == "GMServiceDashboard")
            {

                SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
                SqlCommand cmd = new SqlCommand("select WashBays from DealerBayDetails ", con);
                DataTable tb1 = new DataTable();
                con.Open();
                string washingBays = Convert.ToString((int)cmd.ExecuteScalar());
                con.Close();
                Microsoft.Reporting.WebForms.ReportParameter rParam10 = new Microsoft.Reporting.WebForms.ReportParameter("WashingBay", washingBays);
                this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1, rParam2, rParam3, rParam4, rParam5, rParam6, rParam7, rParam10, rParamCopyright });
            }
            else
            {
                this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1, rParam2, rParam3, rParam4, rParam5, rParam6, rParam7, rParamCopyright });
            }
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
            if (AutoGen == true)
            {
                WriteToExcelFile(exportBytes, ReportName);
            }
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error in processing the request.";
            RptViewer.Reset();
            throw (ex);
        }
    }
    public static DataTable getInfo(string type)
    {
        try
        {
            string sConnString = System.Web.HttpContext.Current.Session[HttpContext.Current.Session["TMLDealercode"] + "-TMLConString"].ToString();
            SqlConnection con = new SqlConnection(sConnString);
            SqlCommand cmd = new SqlCommand("udpGetheaderDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Bodyshop", Convert.ToString(type));
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return new DataTable();
        }
    }
    private void FillCommonDashboardReport(string rept_proc, string ReportRdlcName, string ReportName)
    {
        string mimeType;
        string encoding;
        string fileNameExtension;
        Microsoft.Reporting.WebForms.Warning[] warnings;
        string[] streamids;
        byte[] exportBytes = null;
        String[] Proc = rept_proc.Substring(16).Replace("ProTrackDataSet_", "").Split(',');
        try
        {
            RptViewer.Reset();
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local; // Set to Local Reporting Mode
            RptViewer.ShowExportControls = true;
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + ReportRdlcName;
            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            localRpt.DataSources.Clear();
            for (int flag = 0; flag < Proc.Length; flag++)
            {
                localRpt.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ProTrackDataSet_" + Proc[flag].Trim(), GetDashboard(Proc[flag].Trim())));
            }
            Microsoft.Reporting.WebForms.ReportParameter rParam1 = new Microsoft.Reporting.WebForms.ReportParameter("DateFrom", dtfrm.ToString());
            Microsoft.Reporting.WebForms.ReportParameter rParam2 = new Microsoft.Reporting.WebForms.ReportParameter("Copyright", Copyright);
            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1, rParam2 });
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
            if (AutoGen == true)
            {
                WriteToExcelFile(exportBytes, ReportName);
            }
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error in processing the request.";
            RptViewer.Reset();
            throw (ex);
        }
    }
    private void FillCommonDashboardReport1(string rept_proc, string ReportRdlcName, string ReportName)
    {
        string mimeType;
        string encoding;
        string fileNameExtension;
        Microsoft.Reporting.WebForms.Warning[] warnings;
        string[] streamids;
        byte[] exportBytes = null;
        String[] Proc = rept_proc.Substring(16).Replace("ProTrackDataSet_", "").Split(',');
        try
        {
            RptViewer.Reset();
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local; // Set to Local Reporting Mode
            RptViewer.ShowExportControls = true;
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + ReportRdlcName;
            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            localRpt.DataSources.Clear();
            for (int flag = 0; flag < Proc.Length; flag++)
            {
                localRpt.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ProTrackDataSet_" + Proc[flag].Trim(), GetDashboard(Proc[flag].Trim())));
            }
            Microsoft.Reporting.WebForms.ReportParameter rParam1 = new Microsoft.Reporting.WebForms.ReportParameter("DateFrom", dtfrm.ToString("MM/dd/yyyy"));
            Microsoft.Reporting.WebForms.ReportParameter rParam2 = new Microsoft.Reporting.WebForms.ReportParameter("Copyright", Copyright);
            Microsoft.Reporting.WebForms.ReportParameter rParam3 = new Microsoft.Reporting.WebForms.ReportParameter("DealerDetails", DealerName);
            Microsoft.Reporting.WebForms.ReportParameter rParam4;
            int MaxDate;
            if (int.Parse(cmbDayTo.SelectedValue.ToString().Trim()) >= 10)
                rParam4 = new Microsoft.Reporting.WebForms.ReportParameter("MaxDate", (int.Parse(cmbDayTo.SelectedValue.ToString().Trim()) + 1).ToString());
            else
            {
                MaxDate = int.Parse(cmbDayTo.SelectedValue.Trim()) + 1;
                rParam4 = new Microsoft.Reporting.WebForms.ReportParameter("MaxDate", ("0" + MaxDate.ToString()));
            }

            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1, rParam2, rParam3, rParam4 });
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
            if (AutoGen == true)
            {
                WriteToExcelFile(exportBytes, ReportName);
            }
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error in processing the request.";
            RptViewer.Reset();
            throw (ex);
        }
    }

    private void FillTrendsReport(System.Data.DataTable Dt, string ReportRdlcName, string ReportName, string FileName)
    {
        string mimeType;
        string encoding;
        string fileNameExtension;
        Microsoft.Reporting.WebForms.Warning[] warnings;
        string[] streamids;
        byte[] exportBytes = null;
        try
        {
            if (Dt.Rows.Count < 1)
            {
                lblStstus.Text = "Your Search Criteria did not match any documents.<br>Try different Search Criteria.";
                RptViewer.Reset();
                return;
            }
            MyAccordion.RequireOpenedPane = false;
            MyAccordion.SelectedIndex = -1;
            RptViewer.Reset();
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;             // Set to Local Reporting Mode
            RptViewer.ShowExportControls = true;                                                   // Show The Export Controls
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + ReportRdlcName;
            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            ReportDS.Name = ReportName;
            ReportDS.Value = Dt;
            localRpt.DisplayName = file_name;
            localRpt.DataSources.Clear();
            Microsoft.Reporting.WebForms.ReportParameter rParam1 = new Microsoft.Reporting.WebForms.ReportParameter("DateFrm", daymnthyr);
            Microsoft.Reporting.WebForms.ReportParameter rParam2 = new Microsoft.Reporting.WebForms.ReportParameter("SAName", cmbServiceAdvisor.Items[cmbServiceAdvisor.SelectedIndex].Text);
            Microsoft.Reporting.WebForms.ReportParameter rParam3 = new Microsoft.Reporting.WebForms.ReportParameter("TLId", cmbTeamLead.Items[cmbTeamLead.SelectedIndex].Text);
            Microsoft.Reporting.WebForms.ReportParameter rParam6 = new Microsoft.Reporting.WebForms.ReportParameter("Copyright", Copyright);
            localRpt.DataSources.Add(ReportDS);
            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1, rParam2, rParam3, rParam6 });
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
            if (AutoGen == true)
            {
                WriteToExcelFile(exportBytes, FileName);
            }
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error in processing the request.";
            RptViewer.Reset();
            throw (ex);
        }
    }

    private void FillSWPReport(System.Data.DataTable Dt, string ReportRdlcName, string ReportName, string file_name, string ServiceAdvisor)
    {
        string mimeType;
        string encoding;
        string fileNameExtension;
        Microsoft.Reporting.WebForms.Warning[] warnings;
        string[] streamids;
        byte[] exportBytes = null;
        try
        {
            if (Dt.Rows.Count < 1)
            {
                lblStstus.Text = "Your Search Criteria did not match any documents.<br>Try different Search Criteria.";
                RptViewer.Reset();
                return;
            }
            MyAccordion.RequireOpenedPane = false;
            MyAccordion.SelectedIndex = -1;
            Microsoft.Reporting.WebForms.ReportParameter rParam1 = new Microsoft.Reporting.WebForms.ReportParameter("DateFrom", dtfrm.ToString("dd-MM-yyyy"));
            Microsoft.Reporting.WebForms.ReportParameter rParam2;
            if (ServiceAdvisor.ToString().Trim() == "")
            {
                rParam2 = new Microsoft.Reporting.WebForms.ReportParameter("SAName", "ALL");
            }
            else
            {
                rParam2 = new Microsoft.Reporting.WebForms.ReportParameter("SAName", ServiceAdvisor);
            }
            Microsoft.Reporting.WebForms.ReportParameter rParam3 = new Microsoft.Reporting.WebForms.ReportParameter("TLId", cmbTeamLead.Items[cmbTeamLead.SelectedIndex].Text);
            Microsoft.Reporting.WebForms.ReportParameter rParam6 = new Microsoft.Reporting.WebForms.ReportParameter("Copyright", Copyright);
            Microsoft.Reporting.WebForms.ReportParameter rParam7 = new Microsoft.Reporting.WebForms.ReportParameter("VehicleStatus", drpWhiteBoard.Items[drpWhiteBoard.SelectedIndex].Text);
            RptViewer.Reset();                                                                        // reset the report viewer
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;             // Set to Local Reporting Mode
            RptViewer.ShowExportControls = true;                                                   // Show The Export Controls
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + ReportRdlcName;
            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            ReportDS.Name = ReportName;
            ReportDS.Value = Dt;
            localRpt.DisplayName = file_name;
            localRpt.DataSources.Clear();
            localRpt.DataSources.Add(ReportDS);
            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1, rParam2, rParam3, rParam6, rParam7 });
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
            if (AutoGen == true)
            {
                WriteToExcelFile(exportBytes, file_name);
            }
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error in processing the request.";
            RptViewer.Reset();
            throw (ex);
        }
    }
    private void FillPartsReport(System.Data.DataTable Dt, string ReportRdlcName, string ReportName, string file_name, string ServiceAdvisor)
    {
        string mimeType;
        string encoding;
        string fileNameExtension;
        Microsoft.Reporting.WebForms.Warning[] warnings;
        string[] streamids;
        byte[] exportBytes = null;
        try
        {
            if (Dt.Rows.Count < 1)
            {
                lblStstus.Text = "Your Search Criteria did not match any documents.<br>Try different Search Criteria.";
                RptViewer.Reset();
                return;
            }
            MyAccordion.RequireOpenedPane = false;
            MyAccordion.SelectedIndex = -1;
            Microsoft.Reporting.WebForms.ReportParameter rParam1 = new Microsoft.Reporting.WebForms.ReportParameter("DateFrom", dtfrm.ToString("dd-MM-yyyy"));
            //Microsoft.Reporting.WebForms.ReportParameter rParam2;
            //if (ServiceAdvisor.ToString().Trim() == "")
            //{
            //    rParam2 = new Microsoft.Reporting.WebForms.ReportParameter("SAName", "ALL");
            //}
            //else
            //{
            //    rParam2 = new Microsoft.Reporting.WebForms.ReportParameter("SAName", ServiceAdvisor);
            //}
            //Microsoft.Reporting.WebForms.ReportParameter rParam3 = new Microsoft.Reporting.WebForms.ReportParameter("TLId", cmbTeamLead.Items[cmbTeamLead.SelectedIndex].Text);
            Microsoft.Reporting.WebForms.ReportParameter rParam6 = new Microsoft.Reporting.WebForms.ReportParameter("Copyright", Copyright);
            // Microsoft.Reporting.WebForms.ReportParameter rParam7 = new Microsoft.Reporting.WebForms.ReportParameter("VehicleStatus", drpWhiteBoard.Items[drpWhiteBoard.SelectedIndex].Text);
            RptViewer.Reset();                                                                        // reset the report viewer
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;             // Set to Local Reporting Mode
            RptViewer.ShowExportControls = true;                                                   // Show The Export Controls
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + ReportRdlcName;
            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            ReportDS.Name = ReportName;
            ReportDS.Value = Dt;
            localRpt.DisplayName = file_name;
            localRpt.DataSources.Clear();
            localRpt.DataSources.Add(ReportDS);
            // this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1, rParam2, rParam3, rParam6, rParam7 });
            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1, rParam6 });
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
            if (AutoGen == true)
            {
                WriteToExcelFile(exportBytes, file_name);
            }
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error in processing the request.";
            RptViewer.Reset();
            throw (ex);
        }
    }
    private void FillVehicleTOTReport(System.Data.DataTable Dt, string ReportRdlcName, string ReportName, string file_name, string ServiceAdvisor)
    {
        string mimeType;
        string encoding;
        string fileNameExtension;
        Microsoft.Reporting.WebForms.Warning[] warnings;
        string[] streamids;
        byte[] exportBytes = null;
        try
        {
            if (Dt.Rows.Count < 1)
            {
                lblStstus.Text = "Your Search Criteria did not match any documents.<br>Try different Search Criteria.";
                RptViewer.Reset();
                return;
            }
            MyAccordion.RequireOpenedPane = false;
            MyAccordion.SelectedIndex = -1;
            Microsoft.Reporting.WebForms.ReportParameter rParam1 = new Microsoft.Reporting.WebForms.ReportParameter("DateFrom", dtfrm.ToString("dd-MM-yyyy"));
            //Microsoft.Reporting.WebForms.ReportParameter rParam2;
            //if (ServiceAdvisor.ToString().Trim() == "")
            //{
            //    rParam2 = new Microsoft.Reporting.WebForms.ReportParameter("SAName", "ALL");
            //}
            //else
            //{
            //    rParam2 = new Microsoft.Reporting.WebForms.ReportParameter("SAName", ServiceAdvisor);
            //}
            //Microsoft.Reporting.WebForms.ReportParameter rParam3 = new Microsoft.Reporting.WebForms.ReportParameter("TLId", cmbTeamLead.Items[cmbTeamLead.SelectedIndex].Text);
            Microsoft.Reporting.WebForms.ReportParameter rParam6 = new Microsoft.Reporting.WebForms.ReportParameter("Copyright", Copyright);
            // Microsoft.Reporting.WebForms.ReportParameter rParam7 = new Microsoft.Reporting.WebForms.ReportParameter("VehicleStatus", drpWhiteBoard.Items[drpWhiteBoard.SelectedIndex].Text);
            RptViewer.Reset();                                                                        // reset the report viewer
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;             // Set to Local Reporting Mode
            RptViewer.ShowExportControls = true;                                                   // Show The Export Controls
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + ReportRdlcName;
            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            ReportDS.Name = ReportName;
            ReportDS.Value = Dt;
            localRpt.DisplayName = file_name;
            localRpt.DataSources.Clear();
            localRpt.DataSources.Add(ReportDS);
            // this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1, rParam2, rParam3, rParam6, rParam7 });
            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1, rParam6 });
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
            if (AutoGen == true)
            {
                WriteToExcelFile(exportBytes, file_name);
            }
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error in processing the request.";
            RptViewer.Reset();
            throw (ex);
        }
    }
    private void FillSameDayReport(string ReportRdlcName, string rept_proc, string file_name, string ServiceAdvisor)
    {
        string mimeType;
        string encoding;
        string fileNameExtension;
        Microsoft.Reporting.WebForms.Warning[] warnings;
        string[] streamids;
        byte[] exportBytes = null;
        String[] Proc = rept_proc.Substring(16).Replace("ProTrackDataSet_", "").Split(',');
        try
        {
            MyAccordion.RequireOpenedPane = false;
            MyAccordion.SelectedIndex = -1;
            Microsoft.Reporting.WebForms.ReportParameter rParam1 = new Microsoft.Reporting.WebForms.ReportParameter("DateFrom", dtfrm.ToString("dd-MM-yyyy"));
            Microsoft.Reporting.WebForms.ReportParameter rParam2;
            if (ServiceAdvisor.ToString().Trim() == "")
            {
                rParam2 = new Microsoft.Reporting.WebForms.ReportParameter("SAName", "ALL");
            }
            else
            {
                rParam2 = new Microsoft.Reporting.WebForms.ReportParameter("SAName", ServiceAdvisor);
            }
            Microsoft.Reporting.WebForms.ReportParameter rParam3 = new Microsoft.Reporting.WebForms.ReportParameter("TLId", cmbTeamLead.Items[cmbTeamLead.SelectedIndex].Text);
            Microsoft.Reporting.WebForms.ReportParameter rParam6 = new Microsoft.Reporting.WebForms.ReportParameter("Copyright", Copyright);
            RptViewer.Reset();                                                                        // reset the report viewer
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;             // Set to Local Reporting Mode
            RptViewer.ShowExportControls = true;                                                   // Show The Export Controls
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + ReportRdlcName;
            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            localRpt.DataSources.Clear();
            for (int flag = 0; flag < Proc.Length; flag++)
            {
                localRpt.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ProTrackDataSet_" + Proc[flag].Trim(), GetStageWiseProductivityDataTable(Proc[flag].Trim(), dtfrm.ToShortDateString(), ServiceAdvisor)));
            }
            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1, rParam2, rParam3, rParam6 });
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
            if (AutoGen == true)
            {
                WriteToExcelFile(exportBytes, file_name);
            }
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error in processing the request.";
            RptViewer.Reset();
            throw (ex);
        }
    }

    protected DataTable GetVehicelRemarksDataTable(string StProc)
    {

        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Date", dtfrm.ToString("MM/dd/yyyy"));
        cmd.Parameters.AddWithValue("@VehicleStatus", VehicleStatus);
        cmd.CommandText = StProc;
        System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    public DataTable GetStageWiseProductivityDataTable(string StProc, string dDate, string ServiceAdvisor)
    {

        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Date", dtfrm.ToString("MM/dd/yyyy"));
        cmd.Parameters.AddWithValue("@SAName", ServiceAdvisor);
        cmd.Parameters.AddWithValue("@FloorName", floorname);
        cmd.Parameters.AddWithValue("@TLid", TLId);
        cmd.Parameters.AddWithValue("@IsBodyshop", IsBodyshop);
        cmd.Parameters.AddWithValue("@VehicleStatus", VehicleStatus);
        cmd.CommandText = StProc;
        System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    public DataTable GetCardScanAdherence(string StProc, string dDate, string ServiceAdvisor)
    {

        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@DateFrom", dtfrm.ToString("MM/dd/yyyy"));
        cmd.Parameters.AddWithValue("@SAName", ServiceAdvisor);
        cmd.Parameters.AddWithValue("@TLId", TLId);
        cmd.Parameters.AddWithValue("@FloorName", "");
        cmd.Parameters.AddWithValue("@IsBodyshop", 0);
        cmd.Parameters.AddWithValue("@VehicleStatus", VehicleStatus);
        cmd.CommandText = StProc;
        System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    public DataTable GetDelayAnalysisReportDataTable(string StProc, string DtFrom, string DtTo, string ServiceAdvisor, string ServiceType)
    {

        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Datefrom", DtFrom);
        cmd.Parameters.AddWithValue("@Dateto", DtTo);
        cmd.Parameters.AddWithValue("@SAName", ServiceAdvisor);
        cmd.Parameters.AddWithValue("@ServiceType", ServiceType);
        cmd.CommandText = StProc;
        System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    public DataTable GetDeliveryAnalysisReportDataTable(string StProc, string Date, string ServiceAdvisor)
    {

        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Date", Date);
        cmd.Parameters.AddWithValue("@SAName", ServiceAdvisor);
        cmd.CommandText = StProc;
        System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    public DataTable GetDeliveryAnalysisReportDataTable(string StProc, string Datefrom, string Dateto, string ServiceAdvisor)
    {

        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Datefrom", Datefrom);
        cmd.Parameters.AddWithValue("@Dateto", Dateto);
        cmd.Parameters.AddWithValue("@EmpName", ServiceAdvisor);
        cmd.CommandText = StProc;
        System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    public DataTable GetMonthlyAttendance(string EmpName, string Mnth, string Yr, string StProc)
    {

        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = StProc;                               //@EmpName varchar(50),@Mon int,@Yr int
        cmd.Parameters.AddWithValue("@EmpName", EmpName);
        cmd.Parameters.AddWithValue("@Mon", Mnth);
        cmd.Parameters.AddWithValue("@Yr", Yr);
        System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    private void FillAttendanceReport(System.Data.DataTable Dt, string ReportRdlcName, string ReportName, string file_name, string EmpName)
    {
        string mimeType;
        string encoding;
        string fileNameExtension;
        Microsoft.Reporting.WebForms.Warning[] warnings;
        string[] streamids;
        byte[] exportBytes = null;
        try
        {
            if (Dt.Rows.Count < 1)
            {
                lblStstus.Text = "Your Search Criteria did not match any documents.<br>Try different Search Criteria.";
                RptViewer.Reset();
                return;
            }
            MyAccordion.RequireOpenedPane = false;
            MyAccordion.SelectedIndex = -1;
            Microsoft.Reporting.WebForms.ReportParameter rParam1 = new Microsoft.Reporting.WebForms.ReportParameter("Copyright", Copyright);
            RptViewer.Reset();                                                                        // reset the report viewer
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;             // Set to Local Reporting Mode
            RptViewer.ShowExportControls = true;                                                   // Show The Export Controls
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + ReportRdlcName;
            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            ReportDS.Name = ReportName;
            ReportDS.Value = Dt;
            localRpt.DisplayName = file_name;
            localRpt.DataSources.Clear();
            localRpt.DataSources.Add(ReportDS);
            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1 });
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
            if (AutoGen == true)
            {
                WriteToExcelFile(exportBytes, file_name);
            }
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error in processing the request.";
            RptViewer.Reset();
            throw (ex);
        }
    }

    private void FillEmpReport(System.Data.DataTable Dt, string ReportRdlcName, string ReportName)
    {
        byte[] exportBytes = null;
        try
        {
            if (Dt.Rows.Count < 1)
            {
                lblStstus.Text = "Your Search Criteria did not match any documents.<br>Try different Search Criteria.";
                RptViewer.Reset();
                return;
            }
            MyAccordion.RequireOpenedPane = false;
            MyAccordion.SelectedIndex = -1;
            Microsoft.Reporting.WebForms.ReportParameter rParam1 = new Microsoft.Reporting.WebForms.ReportParameter("DateFrom", dtfrm.ToString("dd-MM-yyyy"));
            Microsoft.Reporting.WebForms.ReportParameter rParam2 = new Microsoft.Reporting.WebForms.ReportParameter("DateTo", dtTo.ToString("dd-MM-yyyy"));
            if (EmpType.ToString().Trim() == "")
            {
                EmpType = "ALL";
            }
            Microsoft.Reporting.WebForms.ReportParameter rParam3 = new Microsoft.Reporting.WebForms.ReportParameter("EmpTypetxt", EmpType.ToString());
            RptViewer.Reset();
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
            RptViewer.ShowExportControls = true;
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + ReportRdlcName;
            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            ReportDS.Name = ReportName;
            ReportDS.Value = Dt;
            localRpt.DisplayName = file_name;
            localRpt.DataSources.Clear();
            localRpt.DataSources.Add(ReportDS);
            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1, rParam2, rParam3 });
            this.RptViewer.LocalReport.Refresh();
            if (AutoGen == true)
            {
                WriteToExcelFile(exportBytes, file_name);
            }
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error in processing the request. " + ex.Message;
            RptViewer.Reset();
            throw (ex);
        }
    }

    public DataTable GetInstantFeedBack()
    {
        string sConnString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        SqlConnection oConn = new SqlConnection(sConnString);
        SqlCommand cmd = new SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "GetVotingPer";

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }
    public DataTable GetInstantFeedBackCustomersDetails()
    {
        string sConnString = System.Web.HttpContext.Current.Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        SqlConnection oConn = new SqlConnection(sConnString);
        SqlCommand cmd = new SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "GetUnSatisfiedList";

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }
    private void FillInstantFeedBack(string rept_rdlc, string rept_proc, string FileName)
    {
        string mimeType;
        string encoding;
        string fileNameExtension;
        Microsoft.Reporting.WebForms.Warning[] warnings;
        string[] streamids;
        byte[] exportBytes = null;
        System.Data.DataTable Dt = new System.Data.DataTable();
        Dt = GetInstantFeedBack();
        System.Data.DataTable Dt1 = new System.Data.DataTable();
        Dt1 = GetInstantFeedBackCustomersDetails();
        if (Dt.Rows.Count <= 0)
        {
            lblStstus.Text = "No data found for date specified .<br> Try different Search Criteria.";
            RptViewer.Reset();
            return;
        }
        MyAccordion.RequireOpenedPane = false;
        MyAccordion.SelectedIndex = -1;
        Microsoft.Reporting.WebForms.ReportParameter rParam1 = new Microsoft.Reporting.WebForms.ReportParameter("Copyright", Copyright);
        try
        {
            string[] rpt_procs = { "", "", "" };
            rpt_procs = rept_proc.Split(',');
            RptViewer.Reset();                                                                        // reset the report viewer
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;             // Set to Local Reporting Mode
            RptViewer.ShowExportControls = true;                                                   // Show The Export Controls
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + rept_rdlc;
            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            localRpt.DataSources.Clear();
            localRpt.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource(rpt_procs[0].ToString(), Dt));
            if (Dt1.Rows.Count != 0)
            {
                localRpt.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource(rpt_procs[1].ToString(), Dt1));
            }
            else
            {
                localRpt.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource(rpt_procs[1].ToString(), Dt1));
            }

            this.RptViewer.LocalReport.EnableExternalImages = true;
            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1 });
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
            if (AutoGen == true)
            {
                WriteToExcelFile(exportBytes, FileName);
            }
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error in processing the request.";
            throw (ex);
        }
    }

    private void FillFrontOfficeReport(System.Data.DataTable Dt, DateTime DtFrom, DateTime DtTo, string ServiceAdvisor, string ReportRdlcName, string StProc, string FileName)
    {
        string mimeType;
        string encoding;
        string fileNameExtension;
        Microsoft.Reporting.WebForms.Warning[] warnings;
        string[] streamids;
        byte[] exportBytes = null;
        try
        {
            if (Dt.Rows.Count < 1)
            {
                lblStstus.Text = "Your Search Criteria did not match any documents.<br>Try different Search Criteria.";
                RptViewer.Reset();
                return;
            }
            MyAccordion.RequireOpenedPane = false;
            MyAccordion.SelectedIndex = -1;
            Microsoft.Reporting.WebForms.ReportParameter rParam1 = new Microsoft.Reporting.WebForms.ReportParameter("SAName", ServiceAdvisor.ToString());
            Microsoft.Reporting.WebForms.ReportParameter rParam2 = new Microsoft.Reporting.WebForms.ReportParameter("StartDate", DtFrom.ToString("dd-MM-yyyy"));
            Microsoft.Reporting.WebForms.ReportParameter rParam3 = new Microsoft.Reporting.WebForms.ReportParameter("EndDate", DtTo.ToString("dd-MM-yyyy"));
            Microsoft.Reporting.WebForms.ReportParameter rParam4 = new Microsoft.Reporting.WebForms.ReportParameter("Copyright", Copyright);
            RptViewer.Reset();                                                                        // reset the report viewer
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;             // Set to Local Reporting Mode
            RptViewer.ShowExportControls = true;                                                   // Show The Export Controls
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + ReportRdlcName;
            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            ReportDS.Name = StProc;
            ReportDS.Value = Dt;
            localRpt.DisplayName = StProc;
            localRpt.DataSources.Clear();
            localRpt.DataSources.Add(ReportDS);
            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1, rParam2, rParam3, rParam4 });
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
            if (AutoGen == true)
            {
                WriteToExcelFile(exportBytes, FileName);
            }
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error in processing the request. " + ex.Message;
            RptViewer.Reset();
            throw (ex);
        }
    }

    private void FillDelayAnalysisReport(System.Data.DataTable Dt, DateTime DtFrom, DateTime DtTo, string ServiceAdvisor, string ServiceType, string ReportRdlcName, string StProc, string FileName)
    {
        string mimeType;
        string encoding;
        string fileNameExtension;
        Microsoft.Reporting.WebForms.Warning[] warnings;
        string[] streamids;
        byte[] exportBytes = null;
        try
        {
            if (Dt.Rows.Count < 1)
            {
                lblStstus.Text = "Your Search Criteria did not match any documents.<br>Try different Search Criteria.";
                RptViewer.Reset();
                return;
            }
            MyAccordion.RequireOpenedPane = false;
            MyAccordion.SelectedIndex = -1;
            Microsoft.Reporting.WebForms.ReportParameter rParam1 = new Microsoft.Reporting.WebForms.ReportParameter("SAName", ServiceAdvisor.ToString());
            Microsoft.Reporting.WebForms.ReportParameter rParam2 = new Microsoft.Reporting.WebForms.ReportParameter("StartDate", DtFrom.ToString("dd-MM-yyyy"));
            Microsoft.Reporting.WebForms.ReportParameter rParam3 = new Microsoft.Reporting.WebForms.ReportParameter("EndDate", DtTo.ToString("dd-MM-yyyy"));
            Microsoft.Reporting.WebForms.ReportParameter rParam4 = new Microsoft.Reporting.WebForms.ReportParameter("ServiceType", ServiceType.ToString());
            Microsoft.Reporting.WebForms.ReportParameter rParam5 = new Microsoft.Reporting.WebForms.ReportParameter("Copyright", Copyright);
            RptViewer.Reset();                                                                        // reset the report viewer
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;             // Set to Local Reporting Mode
            RptViewer.ShowExportControls = true;                                                   // Show The Export Controls
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + ReportRdlcName;
            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            ReportDS.Name = StProc;
            ReportDS.Value = Dt;
            localRpt.DisplayName = StProc;
            localRpt.DataSources.Clear();
            localRpt.DataSources.Add(ReportDS);
            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1, rParam2, rParam3, rParam4, rParam5 });
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
            if (AutoGen == true)
            {
                WriteToExcelFile(exportBytes, FileName);
            }
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error in processing the request. " + ex.Message;
            RptViewer.Reset();
            throw (ex);
        }
    }

    private void FillDeliveryAnalysisReport(System.Data.DataTable Dt, DateTime DtFrom, string ServiceAdvisor, string ReportRdlcName, string StProc, string FileName)
    {
        string mimeType;
        string encoding;
        string fileNameExtension;
        Microsoft.Reporting.WebForms.Warning[] warnings;
        string[] streamids;
        byte[] exportBytes = null;
        try
        {
            if (Dt.Rows.Count < 1)
            {
                lblStstus.Text = "Your Search Criteria did not match any documents.<br>Try different Search Criteria.";
                RptViewer.Reset();
                return;
            }
            MyAccordion.RequireOpenedPane = false;
            MyAccordion.SelectedIndex = -1;
            Microsoft.Reporting.WebForms.ReportParameter rParam1 = new Microsoft.Reporting.WebForms.ReportParameter("SAName", ServiceAdvisor.ToString());
            Microsoft.Reporting.WebForms.ReportParameter rParam2 = new Microsoft.Reporting.WebForms.ReportParameter("StartDate", DtFrom.ToString("dd-MM-yyyy"));
            RptViewer.Reset();                                                                        // reset the report viewer
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;             // Set to Local Reporting Mode
            RptViewer.ShowExportControls = true;                                                   // Show The Export Controls
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + ReportRdlcName;
            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            ReportDS.Name = StProc;
            ReportDS.Value = Dt;
            localRpt.DisplayName = StProc;
            localRpt.DataSources.Clear();
            localRpt.DataSources.Add(ReportDS);
            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1, rParam2 });
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
            if (AutoGen == true)
            {
                WriteToExcelFile(exportBytes, FileName);
            }
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error in processing the request. " + ex.Message;
            RptViewer.Reset();
            throw (ex);
        }
    }

    private void FillDeliveryAnalysisReport(System.Data.DataTable Dt, DateTime DtFrom, DateTime DtTo, string ServiceAdvisor, string ReportRdlcName, string StProc, string FileName)
    {
        string mimeType;
        string encoding;
        string fileNameExtension;
        Microsoft.Reporting.WebForms.Warning[] warnings;
        string[] streamids;
        byte[] exportBytes = null;
        try
        {
            RptViewer.Reset();
            if (Dt.Rows.Count < 1)
            {
                lblStstus.Text = "Your Search Criteria did not match any documents.<br>Try different Search Criteria.";
                RptViewer.Reset();
                return;
            }
            MyAccordion.RequireOpenedPane = false;
            MyAccordion.SelectedIndex = -1;
            Microsoft.Reporting.WebForms.ReportParameter rParam1 = new Microsoft.Reporting.WebForms.ReportParameter("SAName", (ServiceAdvisor.ToString().Trim() == "" ? "All" : ServiceAdvisor.ToString().Trim()));
            Microsoft.Reporting.WebForms.ReportParameter rParam2 = new Microsoft.Reporting.WebForms.ReportParameter("StartDate", DtFrom.ToString("dd-MM-yyyy"));
            Microsoft.Reporting.WebForms.ReportParameter rParam3 = new Microsoft.Reporting.WebForms.ReportParameter("EndDate", DtTo.ToString("dd-MM-yyyy"));
            Microsoft.Reporting.WebForms.ReportParameter rParam4 = new Microsoft.Reporting.WebForms.ReportParameter("Copyright", Copyright);
            RptViewer.Reset();                                                                        // reset the report viewer
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;             // Set to Local Reporting Mode
            RptViewer.ShowExportControls = true;                                                   // Show The Export Controls
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + ReportRdlcName;
            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            ReportDS.Name = StProc;
            ReportDS.Value = Dt;
            localRpt.DisplayName = file_name;
            localRpt.DataSources.Clear();
            localRpt.DataSources.Add(ReportDS);
            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1, rParam2, rParam3, rParam4 });
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
            if (AutoGen == true)
            {
                WriteToExcelFile(exportBytes, FileName);
            }
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error in processing the request. " + ex.Message;
            RptViewer.Reset();
            throw (ex);
        }
    }

    private void FillVehicleAgeingRatioReport(System.Data.DataTable Dt, string ServiceAdvisor, string ReportRdlcName, string StProc, string FileName)
    {
        string mimeType;
        string encoding;
        string fileNameExtension;
        Microsoft.Reporting.WebForms.Warning[] warnings;
        string[] streamids;
        byte[] exportBytes = null;
        try
        {
            if (Dt.Rows.Count < 1)
            {
                lblStstus.Text = "Your Search Criteria did not match any documents.<br>Try different Search Criteria.";
                RptViewer.Reset();
                return;
            }
            MyAccordion.RequireOpenedPane = false;
            MyAccordion.SelectedIndex = -1;
            Microsoft.Reporting.WebForms.ReportParameter rParam1 = new Microsoft.Reporting.WebForms.ReportParameter("ServiceAdvisor", ServiceAdvisor);
            RptViewer.Reset();                                                                        // reset the report viewer
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;             // Set to Local Reporting Mode
            RptViewer.ShowExportControls = true;                                                   // Show The Export Controls
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + ReportRdlcName;
            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            ReportDS.Name = StProc;
            ReportDS.Value = Dt;
            localRpt.DisplayName = StProc;
            localRpt.DataSources.Clear();
            localRpt.DataSources.Add(ReportDS);
            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1 });
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
            if (AutoGen == true)
            {
                WriteToExcelFile(exportBytes, FileName);
            }
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error in processing the request. " + ex.Message;
            RptViewer.Reset();
            throw (ex);
        }
    }

    private void FillFFReport(string rept_Proc, string rept_Rdlc, string FileName)
    {
        string mimeType;
        string encoding;
        string fileNameExtension;
        Microsoft.Reporting.WebForms.Warning[] warnings;
        string[] streamids;
        byte[] exportBytes = null;
        System.Data.DataTable Dt = new System.Data.DataTable();
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlCommand cmd = new SqlCommand("", con);
        cmd.CommandText = rept_Proc.Split(',').GetValue(0).ToString().Substring(16);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        sda.Fill(Dt);
        System.Data.DataTable Dt1 = new System.Data.DataTable();
        cmd = new SqlCommand("", con);
        cmd.CommandText = rept_Proc.Split(',').GetValue(1).ToString().Substring(16);
        cmd.CommandType = CommandType.StoredProcedure;
        sda = new SqlDataAdapter(cmd);
        sda.Fill(Dt1);
        if (Dt.Rows.Count <= 0)
        {
            lblStstus.Text = "No data found for date specified .<br> Try different Search Criteria.";
            RptViewer.Reset();
            return;
        }
        MyAccordion.RequireOpenedPane = false;
        MyAccordion.SelectedIndex = -1;
        try
        {
            string[] rpt_procs = { "", "" };
            rpt_procs = rept_Proc.Split(',');
            RptViewer.Reset();                                                                        // reset the report viewer
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;             // Set to Local Reporting Mode
            RptViewer.ShowExportControls = true;                                                   // Show The Export Controls
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + rept_rdlc;
            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            localRpt.DataSources.Clear();
            localRpt.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource(rpt_procs[0].ToString(), Dt));
            localRpt.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource(rpt_procs[1].ToString(), Dt1));
            this.RptViewer.LocalReport.EnableExternalImages = true;
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
            if (AutoGen == true)
            {
                WriteToExcelFile(exportBytes, FileName);
            }
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error in processing the request.";
            throw (ex);
        }
    }

    public DataTable GetDailyTechnicianPerformance()
    {
        string sConnString = System.Web.HttpContext.Current.Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        SqlConnection oConn = new SqlConnection(sConnString);
        SqlCommand cmd = new SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "GetTechnicianPerformance";

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    private void FillDailyTechnicianPerformance(string rept_rdlc, string rept_proc, string FileName)
    {
        string mimeType;
        string encoding;
        string fileNameExtension;
        Microsoft.Reporting.WebForms.Warning[] warnings;
        string[] streamids;
        byte[] exportBytes = null;
        System.Data.DataTable Dt = new System.Data.DataTable();
        Dt = GetDailyTechnicianPerformance();
        if (Dt.Rows.Count <= 0)
        {
            lblStstus.Text = "No data found for date specified .<br> Try different Search Criteria.";
            RptViewer.Reset();
            return;
        }
        MyAccordion.RequireOpenedPane = false;
        MyAccordion.SelectedIndex = -1;
        Microsoft.Reporting.WebForms.ReportParameter rParam1 = new Microsoft.Reporting.WebForms.ReportParameter("Copyright", Copyright);
        try
        {
            RptViewer.Reset();                                                                        // reset the report viewer
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;             // Set to Local Reporting Mode
            RptViewer.ShowExportControls = true;                                                   // Show The Export Controls
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + rept_rdlc;
            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            localRpt.DataSources.Clear();
            localRpt.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource(rept_proc, Dt));
            this.RptViewer.LocalReport.EnableExternalImages = true;
            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1 });
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
            if (AutoGen == true)
            {
                WriteToExcelFile(exportBytes, FileName);
            }
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error in processing the request.";
            RptViewer.Reset();
            throw (ex);
        }
    }

    public DataTable GetCarReadyReportDataTable(DateTime DtFrom, DateTime DtTo, string RegNo, string ServiceType, string StProc)
    {
        string sConnString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(sConnString);
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = StProc;
        cmd.Parameters.AddWithValue("@StartDate", DtFrom);
        cmd.Parameters.AddWithValue("@EndDate", DtTo);
        cmd.Parameters.AddWithValue("@RegNo", RegNo);
        cmd.Parameters.AddWithValue("@stdate", ServiceType);
        System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }



    private void FillSDD_PDD(string ReportRdlcName, string ReportName)
    {
        string mimeType;
        string encoding;
        string fileNameExtension;
        Microsoft.Reporting.WebForms.Warning[] warnings;
        string[] streamids;
        byte[] exportBytes = null;
        try
        {
            RptViewer.Reset();                                                                        // reset the report viewer
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;             // Set to Local Reporting Mode
            RptViewer.ShowExportControls = true;                                                   // Show The Export Controls
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + ReportRdlcName;
            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            localRpt.DataSources.Clear();
            localRpt.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ProTrackDataSet_GetSDD_PDD", GetSummaryReport("GetSDD_PDD")));
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
            if (AutoGen == true)
            {
                WriteToExcelFile(exportBytes, "SDD_PDD_TREND");
            }
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error in processing the request.";
            RptViewer.Reset();
            throw (ex);
        }
    }

    private void FillSDD_PDD_CF_Ratio(string ReportRdlcName, string ReportName)
    {
        string mimeType;
        string encoding;
        string fileNameExtension;
        Microsoft.Reporting.WebForms.Warning[] warnings;
        string[] streamids;
        byte[] exportBytes = null;
        try
        {
            RptViewer.Reset();                                                                        // reset the report viewer
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;             // Set to Local Reporting Mode
            RptViewer.ShowExportControls = true;                                                   // Show The Export Controls
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + ReportRdlcName;
            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            localRpt.DataSources.Clear();
            localRpt.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ProTrackDataSet_GetSDD_PDD_CF_Ratio", GetSummaryReport("GetSDD_PDD_CF_Ratio")));
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
            if (AutoGen == true)
            {
                WriteToExcelFile(exportBytes, "SDD_PDD_CF_RATIO");
            }
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error in processing the request.";
            RptViewer.Reset();
            throw (ex);
        }
    }

    private void FillCFReport(string ReportRdlcName, string ReportName)
    {
        string mimeType;
        string encoding;
        string fileNameExtension;
        Microsoft.Reporting.WebForms.Warning[] warnings;
        string[] streamids;
        byte[] exportBytes = null;
        try
        {
            RptViewer.Reset();                                                                        // reset the report viewer
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;             // Set to Local Reporting Mode
            RptViewer.ShowExportControls = true;                                                   // Show The Export Controls
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + ReportRdlcName;
            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            localRpt.DataSources.Clear();
            localRpt.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ProTrackDataSet_GetCF", GetSummaryReport("GetCF")));
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
            if (AutoGen == true)
            {
                WriteToExcelFile(exportBytes, "CARRY_FORWARD");
            }
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error in processing the request.";
            RptViewer.Reset();
            throw (ex);
        }
    }

    private void FillAgingTrend(string ReportRdlcName, string ReportName)
    {
        string mimeType;
        string encoding;
        string fileNameExtension;
        Microsoft.Reporting.WebForms.Warning[] warnings;
        string[] streamids;
        byte[] exportBytes = null;
        try
        {
            RptViewer.Reset();                                                                        // reset the report viewer
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;             // Set to Local Reporting Mode
            RptViewer.ShowExportControls = true;                                                   // Show The Export Controls
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + ReportRdlcName;
            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            localRpt.DataSources.Clear();
            localRpt.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ProTrackDataSet_GetAgingAnalysis", GetSummaryReport("GetAgingAnalysis")));
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
            if (AutoGen == true)
            {
                WriteToExcelFile(exportBytes, "AGING_TREND");
            }
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error in processing the request.";
            RptViewer.Reset();
            throw (ex);
        }
    }

    private void FillSERVICE_ADVISOR_PRODUCTIVITY(string ReportRdlcName, string ReportName)
    {
        string mimeType;
        string encoding;
        string fileNameExtension;
        Microsoft.Reporting.WebForms.Warning[] warnings;
        string[] streamids;
        byte[] exportBytes = null;
        try
        {
            RptViewer.Reset();                                                                        // reset the report viewer
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;             // Set to Local Reporting Mode
            RptViewer.ShowExportControls = true;                                                   // Show The Export Controls
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + ReportRdlcName;
            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            localRpt.DataSources.Clear();
            localRpt.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ProTrackDataSet_SAAnalysis", GetSummaryReport("SAAnalysis")));
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
            if (AutoGen == true)
            {
                WriteToExcelFile(exportBytes, "SERVICE_ADVISOR_PRODUCTIVITY");
            }
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error in processing the request.";
            RptViewer.Reset();
            throw (ex);
        }
    }

    private void FillTechnician_Report(string ReportRdlcName, string ReportName)
    {
        string mimeType;
        string encoding;
        string fileNameExtension;
        Microsoft.Reporting.WebForms.Warning[] warnings;
        string[] streamids;
        byte[] exportBytes = null;
        try
        {
            RptViewer.Reset();                                                                        // reset the report viewer
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;             // Set to Local Reporting Mode
            RptViewer.ShowExportControls = true;                                                   // Show The Export Controls
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + ReportRdlcName;
            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            localRpt.DataSources.Clear();
            localRpt.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ProTrackDataSet_GetTechReport", GetTechnicianReport()));
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
            if (AutoGen == true)
            {
                WriteToExcelFile(exportBytes, "TechnicianReport");
            }
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error in processing the request.";
            RptViewer.Reset();
            throw (ex);
        }
    }

    public DataTable GetTechnicianReport()
    {
        string sConnString = System.Web.HttpContext.Current.Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        SqlConnection oConn = new SqlConnection(sConnString);
        SqlCommand cmd = new SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "GetTechReport";
        cmd.Parameters.Clear();

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }
    public DataTable GetCustomerDetails()
    {
        string sConnString = System.Web.HttpContext.Current.Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        SqlConnection oConn = new SqlConnection(sConnString);
        SqlCommand cmd = new SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "GetCustomerDetails";
        cmd.Parameters.Clear();

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }
    private void FillSERVICE_CUSTOMER_DETAILS(string ReportRdlcName, string ReportName)
    {
        string mimeType;
        string encoding;
        string fileNameExtension;
        Microsoft.Reporting.WebForms.Warning[] warnings;
        string[] streamids;
        byte[] exportBytes = null;
        try
        {
            RptViewer.Reset();                                                                        // reset the report viewer
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;             // Set to Local Reporting Mode
            RptViewer.ShowExportControls = true;                                                   // Show The Export Controls
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + ReportRdlcName;
            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            localRpt.DataSources.Clear();
            localRpt.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ProTrackDataSet_GetCustomerDetails", GetCustomerDetails()));
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
            if (AutoGen == true)
            {
                WriteToExcelFile(exportBytes, "CUSTOMER_DETAILS");
            }
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error in processing the request.";
            RptViewer.Reset();
            throw (ex);
        }
    }

    private void FillSummaryReport(string rept_proc, string ReportRdlcName, string ReportName)
    {
        string mimeType;
        string encoding;
        string fileNameExtension;
        Microsoft.Reporting.WebForms.Warning[] warnings;
        string[] streamids;
        byte[] exportBytes = null;
        String[] Proc = rept_proc.Substring(16).Replace("ProTrackDataSet_", "").Split(',');
        try
        {
            RptViewer.Reset();                                                                        // reset the report viewer
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;             // Set to Local Reporting Mode
            RptViewer.ShowExportControls = true;                                                   // Show The Export Controls
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + ReportRdlcName;
            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            localRpt.DataSources.Clear();
            for (int flag = 0; flag < Proc.Length; flag++)
            {
                localRpt.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ProTrackDataSet_" + Proc[flag].Trim(), GetReportDataTable(Proc[flag].Trim())));
            }
            Microsoft.Reporting.WebForms.ReportParameter rParam1 = new Microsoft.Reporting.WebForms.ReportParameter("StartDate", "01-" + dtfrm.ToString("MM-yyyy"));
            Microsoft.Reporting.WebForms.ReportParameter rParam2 = new Microsoft.Reporting.WebForms.ReportParameter("EndDate", dtTo.ToString("dd-MM-yyyy"));
            Microsoft.Reporting.WebForms.ReportParameter rParam6 = new Microsoft.Reporting.WebForms.ReportParameter("SAName", cmbServiceAdvisor.Items[cmbServiceAdvisor.SelectedIndex].Text);
            Microsoft.Reporting.WebForms.ReportParameter rParam4 = new Microsoft.Reporting.WebForms.ReportParameter("TLId", cmbTeamLead.Items[cmbTeamLead.SelectedIndex].Text);
            Microsoft.Reporting.WebForms.ReportParameter rParam7 = new Microsoft.Reporting.WebForms.ReportParameter("Copyright", Copyright);
            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1, rParam2, rParam4, rParam6, rParam7 });
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
            if (AutoGen == true)
            {
                WriteToExcelFile(exportBytes, ReportName);
            }
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error in processing the request.";
            RptViewer.Reset();
            throw (ex);
        }
    }

    private void FillDeliveryAnalysisxReport(string rept_proc, string ReportRdlcName, string ReportName, string ServiceAdvisor)
    {
        string mimeType;
        string encoding;
        string fileNameExtension;
        Microsoft.Reporting.WebForms.Warning[] warnings;
        string[] streamids;
        byte[] exportBytes = null;
        String[] Proc = rept_proc.Substring(16).Replace("ProTrackDataSet_", "").Split(',');
        try
        {
            RptViewer.Reset();                                                                        // reset the report viewer
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;             // Set to Local Reporting Mode
            RptViewer.ShowExportControls = true;                                                   // Show The Export Controls
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + ReportRdlcName;
            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            localRpt.DataSources.Clear();
            for (int flag = 0; flag < Proc.Length; flag++)
            {
                localRpt.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ProTrackDataSet_" + Proc[flag].Trim(), GetSAFilterReportDataTable(dtfrm, dtTo, ServiceAdvisor, Proc[flag].Trim())));
            }
            Microsoft.Reporting.WebForms.ReportParameter rParam1 = new Microsoft.Reporting.WebForms.ReportParameter("StartDate", "01-" + dtfrm.ToString("MM-yyyy"));
            Microsoft.Reporting.WebForms.ReportParameter rParam2 = new Microsoft.Reporting.WebForms.ReportParameter("EndDate", dtTo.ToString("dd-MM-yyyy"));
            Microsoft.Reporting.WebForms.ReportParameter rParam3 = new Microsoft.Reporting.WebForms.ReportParameter("Copyright", Copyright);
            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1, rParam2, rParam3 });
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
            if (AutoGen == true)
            {
                WriteToExcelFile(exportBytes, ReportName);
            }
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error in processing the request.";
            RptViewer.Reset();
            throw (ex);
        }
    }

    private void FillADTReport(string rept_proc, string ReportRdlcName, string ReportName)
    {
        string mimeType;
        string encoding;
        string fileNameExtension;
        Microsoft.Reporting.WebForms.Warning[] warnings;
        string[] streamids;
        byte[] exportBytes = null;
        String[] Proc = rept_proc.Substring(16).Replace("ProTrackDataSet_", "").Split(',');
        try
        {
            RptViewer.Reset();
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
            RptViewer.ShowExportControls = true;
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + ReportRdlcName;
            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            localRpt.DataSources.Clear();
            for (int flag = 0; flag < Proc.Length; flag++)
            {
                localRpt.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ProTrackDataSet_" + Proc[flag].Trim(), GetReportDataTable(Proc[flag].Trim())));
            }
            Microsoft.Reporting.WebForms.ReportParameter rParam1 = new Microsoft.Reporting.WebForms.ReportParameter("StartDate", "01-" + dtfrm.ToString("MM-yyyy"));
            Microsoft.Reporting.WebForms.ReportParameter rParam2 = new Microsoft.Reporting.WebForms.ReportParameter("EndDate", dtTo.ToString("dd-MM-yyyy"));
            Microsoft.Reporting.WebForms.ReportParameter rParam3 = new Microsoft.Reporting.WebForms.ReportParameter("Copyright", Copyright);
            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1, rParam2, rParam3 });
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
            if (AutoGen == true)
            {
                WriteToExcelFile(exportBytes, ReportName);
            }
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error in processing the request.";
            RptViewer.Reset();
            throw (ex);
        }
    }

    private void FillExpressServiceAndPartsTMRAndRoadTestTMRReport(DataTable Dt, string ReportRdlcName, string ReportName, string FileName)
    {
        string mimeType;
        string encoding;
        string fileNameExtension;
        Microsoft.Reporting.WebForms.Warning[] warnings;
        string[] streamids;
        byte[] exportBytes = null;
        try
        {
            if (Dt.Rows.Count < 1)
            {
                lblStstus.Text = "Your Search Criteria did not match any documents.<br>Try different Search Criteria.";
                RptViewer.Reset();
                return;
            }
            MyAccordion.RequireOpenedPane = false;
            MyAccordion.SelectedIndex = -1;
            Microsoft.Reporting.WebForms.ReportParameter rParam1 = new Microsoft.Reporting.WebForms.ReportParameter("DateFrmTxt", "From: ");
            Microsoft.Reporting.WebForms.ReportParameter rParam2 = new Microsoft.Reporting.WebForms.ReportParameter("DateFrom", dtfrm.ToString("dd-MM-yyyy"));
            Microsoft.Reporting.WebForms.ReportParameter rParam3 = new Microsoft.Reporting.WebForms.ReportParameter("DateToTxt", "To: ");
            Microsoft.Reporting.WebForms.ReportParameter rParam4 = new Microsoft.Reporting.WebForms.ReportParameter("DateTo", dtTo.ToString("dd-MM-yyyy"));
            Microsoft.Reporting.WebForms.ReportParameter rParam5 = new Microsoft.Reporting.WebForms.ReportParameter("Copyright", Copyright);
            RptViewer.Reset();                                                                        // reset the report viewer
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;             // Set to Local Reporting Mode
            RptViewer.ShowExportControls = true;                                                   // Show The Export Controls
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + ReportRdlcName;
            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            ReportDS.Name = ReportName;
            ReportDS.Value = Dt;
            localRpt.DisplayName = file_name;
            localRpt.DataSources.Clear();
            localRpt.DataSources.Add(ReportDS);
            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1, rParam2, rParam3, rParam4, rParam5 });
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
            if (AutoGen == true)
            {
                WriteToExcelFile(exportBytes, FileName);
            }
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error in processing the request. " + ex.Message;
            RptViewer.Reset();
            throw (ex);
        }
    }

    private void FillMachanicProductivityReport(DataTable Dt, string ReportRdlcName, string ReportName, string FileName)
    {
        string mimeType;
        string encoding;
        string fileNameExtension;
        Microsoft.Reporting.WebForms.Warning[] warnings;
        string[] streamids;
        byte[] exportBytes = null;
        try
        {
            if (Dt.Rows.Count < 1)
            {
                lblStstus.Text = "Your Search Criteria did not match any documents.<br>Try different Search Criteria.";
                RptViewer.Reset();
                return;
            }
            MyAccordion.RequireOpenedPane = false;
            MyAccordion.SelectedIndex = -1;
            string mnt = "";
            if (cmb_month.SelectedValue == "Current Month")
                mnt = DateTime.Now.Month.ToString();
            else
                mnt = cmb_month.Text.ToString();
            Microsoft.Reporting.WebForms.ReportParameter rParam1 = new Microsoft.Reporting.WebForms.ReportParameter("MonthTxt", "Month: ");
            Microsoft.Reporting.WebForms.ReportParameter rParam2 = new Microsoft.Reporting.WebForms.ReportParameter("Month", mnt);
            Microsoft.Reporting.WebForms.ReportParameter rParam3 = new Microsoft.Reporting.WebForms.ReportParameter("YearTxt", "Year: ");
            Microsoft.Reporting.WebForms.ReportParameter rParam5 = new Microsoft.Reporting.WebForms.ReportParameter("Copyright", Copyright);
            string yer = "";
            if (cmbYear.SelectedValue == "Current Year")
                yer = DateTime.Now.Year.ToString();
            else
                yer = cmbYear.Text.ToString();
            Microsoft.Reporting.WebForms.ReportParameter rParam4 = new Microsoft.Reporting.WebForms.ReportParameter("Year", yer);
            RptViewer.Reset();
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
            RptViewer.ShowExportControls = true;
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + ReportRdlcName;
            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            ReportDS.Name = ReportName;
            ReportDS.Value = Dt;
            localRpt.DisplayName = file_name;
            localRpt.DataSources.Clear();
            localRpt.DataSources.Add(ReportDS);
            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1, rParam2, rParam3, rParam4, rParam5 });
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
            ReportName = ReportName.Substring(16);
            if (AutoGen == true)
            {
                WriteToExcelFile(exportBytes, FileName);
            }
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error in processing the request. " + ex.Message;
            RptViewer.Reset();
            throw (ex);
        }
    }

    private void FillTechTMRReportNew(DataTable Dt, string ReportRdlcName, string ReportName, string FileName)
    {
        string mimeType;
        string encoding;
        string fileNameExtension;
        Microsoft.Reporting.WebForms.Warning[] warnings;
        string[] streamids;
        byte[] exportBytes = null;
        try
        {
            if (Dt.Rows.Count < 1)
            {
                lblStstus.Text = "Your Search Criteria did not match any documents.<br>Try different Search Criteria.";
                RptViewer.Reset();
                return;
            }
            MyAccordion.RequireOpenedPane = false;
            MyAccordion.SelectedIndex = -1;
            RptViewer.Reset();                                                                        // reset the report viewer
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;             // Set to Local Reporting Mode
            RptViewer.ShowExportControls = true;                                                   // Show The Export Controls
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + ReportRdlcName;
            Microsoft.Reporting.WebForms.ReportParameter rParam1 = new Microsoft.Reporting.WebForms.ReportParameter("DateFrom", ("Date : " + dtfrm.ToString("dd-MM-yyyy")));
            Microsoft.Reporting.WebForms.ReportParameter rParam3 = new Microsoft.Reporting.WebForms.ReportParameter("Copyright", Copyright);
            Microsoft.Reporting.WebForms.ReportParameter rParam4 = new Microsoft.Reporting.WebForms.ReportParameter("TLId", cmbTeamLead.Items[cmbTeamLead.SelectedIndex].Text);
            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            ReportDS.Name = ReportName;
            ReportDS.Value = Dt;
            localRpt.DisplayName = file_name;
            localRpt.DataSources.Clear();
            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1, rParam3, rParam4 });
            localRpt.DataSources.Add(ReportDS);
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
            ReportName = ReportName.Substring(16);
            if (AutoGen == true)
            {
                WriteToExcelFile(exportBytes, FileName);
            }
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error in processing the request. " + ex.Message;
            RptViewer.Reset();
            throw (ex);
        }
    }

    private void FillVehicleTMR(DataTable Dt, string ReportRdlcName, string ReportName, string FileName)
    {
        string mimeType;
        string encoding;
        string fileNameExtension;
        Microsoft.Reporting.WebForms.Warning[] warnings;
        string[] streamids;
        byte[] exportBytes = null;
        try
        {
            if (Dt.Rows.Count < 1)
            {
                lblStstus.Text = "Your Search Criteria did not match any documents.<br>Try different Search Criteria.";
                RptViewer.Reset();
                return;
            }
            MyAccordion.RequireOpenedPane = false;
            MyAccordion.SelectedIndex = -1;
            RptViewer.Reset();                                                                        // reset the report viewer
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;             // Set to Local Reporting Mode
            RptViewer.ShowExportControls = true;                                                   // Show The Export Controls
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + ReportRdlcName;
            Microsoft.Reporting.WebForms.ReportParameter rParam1 = new Microsoft.Reporting.WebForms.ReportParameter("DateFrom", ("Date : " + dtfrm.ToString("dd-MM-yyyy")));
            Microsoft.Reporting.WebForms.ReportParameter rParam3 = new Microsoft.Reporting.WebForms.ReportParameter("SAName", cmbServiceAdvisor.Items[cmbServiceAdvisor.SelectedIndex].Text);
            Microsoft.Reporting.WebForms.ReportParameter rParam4 = new Microsoft.Reporting.WebForms.ReportParameter("TLId", cmbTeamLead.Items[cmbTeamLead.SelectedIndex].Text);
            Microsoft.Reporting.WebForms.ReportParameter rParam6 = new Microsoft.Reporting.WebForms.ReportParameter("Copyright", Copyright);
            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            ReportDS.Name = ReportName;
            ReportDS.Value = Dt;
            localRpt.DisplayName = file_name;
            localRpt.DataSources.Clear();
            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1, rParam3, rParam4, rParam6 });
            localRpt.DataSources.Add(ReportDS);
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
            ReportName = ReportName.Substring(16);
            if (AutoGen == true)
            {
                WriteToExcelFile(exportBytes, FileName);
            }
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error in processing the request. " + ex.Message;
            RptViewer.Reset();
            throw (ex);
        }
    }

    private void FillCardScanAdherence(DataTable Dt, string ReportRdlcName, string ReportName, string FileName)
    {
        string mimeType;
        string encoding;
        string fileNameExtension;
        Microsoft.Reporting.WebForms.Warning[] warnings;
        string[] streamids;
        byte[] exportBytes = null;
        try
        {
            if (Dt.Rows.Count < 1)
            {
                lblStstus.Text = "Your Search Criteria did not match any documents.<br>Try different Search Criteria.";
                RptViewer.Reset();
                return;
            }
            MyAccordion.RequireOpenedPane = false;
            MyAccordion.SelectedIndex = -1;
            RptViewer.Reset();                                                                        // reset the report viewer
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;             // Set to Local Reporting Mode
            RptViewer.ShowExportControls = true;                                                   // Show The Export Controls
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + ReportRdlcName;
            Microsoft.Reporting.WebForms.ReportParameter rParam1 = new Microsoft.Reporting.WebForms.ReportParameter("DateFrom", dtfrm.ToString("dd-MM-yyyy"));
            Microsoft.Reporting.WebForms.ReportParameter rParam2 = new Microsoft.Reporting.WebForms.ReportParameter("SAName", cmbServiceAdvisor.Items[cmbServiceAdvisor.SelectedIndex].Text);
            Microsoft.Reporting.WebForms.ReportParameter rParam4 = new Microsoft.Reporting.WebForms.ReportParameter("TLId", cmbTeamLead.Items[cmbTeamLead.SelectedIndex].Text);
            Microsoft.Reporting.WebForms.ReportParameter rParam6 = new Microsoft.Reporting.WebForms.ReportParameter("Copyright", Copyright);
            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            ReportDS.Name = ReportName;
            ReportDS.Value = Dt;
            localRpt.DisplayName = file_name;
            localRpt.DataSources.Clear();
            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1, rParam2, rParam4, rParam6 });
            localRpt.DataSources.Add(ReportDS);
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
            ReportName = ReportName.Substring(16);
            if (AutoGen == true)
            {
                WriteToExcelFile(exportBytes, FileName);
            }
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error in processing the request. " + ex.Message;
            RptViewer.Reset();
            throw (ex);
        }
    }

    private void FillCardScanAdherenceReport(string ReportRdlcName, string rept_proc, string FileName, string ServiceAdvisor)
    {
        string mimeType;
        string encoding;
        string fileNameExtension;
        Microsoft.Reporting.WebForms.Warning[] warnings;
        string[] streamids;
        byte[] exportBytes = null;
        String[] Proc = rept_proc.Substring(16).Replace("ProTrackDataSet_", "").Split(',');
        try
        {
            RptViewer.Reset();                                                                        // reset the report viewer
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;             // Set to Local Reporting Mode
            RptViewer.ShowExportControls = true;                                                   // Show The Export Controls
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + ReportRdlcName;
            Microsoft.Reporting.WebForms.ReportParameter rParam1 = new Microsoft.Reporting.WebForms.ReportParameter("DateFrom", dtfrm.ToString("dd-MM-yyyy"));
            Microsoft.Reporting.WebForms.ReportParameter rParam2 = new Microsoft.Reporting.WebForms.ReportParameter("SAName", cmbServiceAdvisor.Items[cmbServiceAdvisor.SelectedIndex].Text);
            Microsoft.Reporting.WebForms.ReportParameter rParam4 = new Microsoft.Reporting.WebForms.ReportParameter("TLId", cmbTeamLead.Items[cmbTeamLead.SelectedIndex].Text);
            Microsoft.Reporting.WebForms.ReportParameter rParam6 = new Microsoft.Reporting.WebForms.ReportParameter("Copyright", Copyright);
            Microsoft.Reporting.WebForms.ReportParameter rParam7 = new Microsoft.Reporting.WebForms.ReportParameter("VehicleStatus", drpWhiteBoard.Items[drpWhiteBoard.SelectedIndex].Text);
            localRpt.DataSources.Clear();
            for (int flag = 0; flag < Proc.Length; flag++)
            {
                localRpt.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ProTrackDataSet_" + Proc[flag].Trim(), GetCardScanAdherence(Proc[flag].Trim(), dtfrm.ToShortDateString(), ServiceAdvisor)));
            }
            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1, rParam2, rParam4, rParam6, rParam7 });
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);

            if (AutoGen == true)
            {
                WriteToExcelFile(exportBytes, FileName);
            }
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error in processing the request. " + ex.Message;
            RptViewer.Reset();
            throw (ex);
        }
    }
    private void FillCarryForwardVehReport(string ReportRdlcName, string rept_proc, string FileName, string ServiceAdvisor)
    {
        string mimeType;
        string encoding;
        string fileNameExtension;
        Microsoft.Reporting.WebForms.Warning[] warnings;
        string[] streamids;
        byte[] exportBytes = null;
        String[] Proc = rept_proc.Substring(16).Replace("ProTrackDataSet_", "").Split(',');
        try
        {
            RptViewer.Reset();                                                                        // reset the report viewer
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;             // Set to Local Reporting Mode
            RptViewer.ShowExportControls = true;                                                   // Show The Export Controls
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + ReportRdlcName;
            Microsoft.Reporting.WebForms.ReportParameter rParam1 = new Microsoft.Reporting.WebForms.ReportParameter("StartDate", dtfrm.ToString("dd-MM-yyyy"));
            Microsoft.Reporting.WebForms.ReportParameter rParam2 = new Microsoft.Reporting.WebForms.ReportParameter("SAName", cmbServiceAdvisor.Items[cmbServiceAdvisor.SelectedIndex].Text);
            Microsoft.Reporting.WebForms.ReportParameter rParam4 = new Microsoft.Reporting.WebForms.ReportParameter("TLId", cmbTeamLead.Items[cmbTeamLead.SelectedIndex].Text);
            Microsoft.Reporting.WebForms.ReportParameter rParam6 = new Microsoft.Reporting.WebForms.ReportParameter("Copyright", Copyright);
            Microsoft.Reporting.WebForms.ReportParameter rParam7 = new Microsoft.Reporting.WebForms.ReportParameter("VehicleStatus", drpWhiteBoard.Items[drpWhiteBoard.SelectedIndex].Text);
            localRpt.DataSources.Clear();
            for (int flag = 0; flag < Proc.Length; flag++)
            {
                localRpt.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ProTrackDataSet_" + Proc[flag].Trim(), GetSAFilterReportDataTable(dtfrm, ServiceAdvisor, Proc[flag].Trim())));
            }
            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1, rParam2, rParam4, rParam6, rParam7 });
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);

            if (AutoGen == true)
            {
                WriteToExcelFile(exportBytes, FileName);
            }
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error in processing the request. " + ex.Message;
            RptViewer.Reset();
            throw (ex);
        }
    }
    private void FillTechTMRReport(DataTable Dt, string ReportRdlcName, string ReportName, string FileName)
    {
        string mimeType;
        string encoding;
        string fileNameExtension;
        Microsoft.Reporting.WebForms.Warning[] warnings;
        string[] streamids;
        byte[] exportBytes = null;
        try
        {
            if (Dt.Rows.Count < 1)
            {
                lblStstus.Text = "Your Search Criteria did not match any documents.<br>Try different Search Criteria.";
                RptViewer.Reset();
                return;
            }
            MyAccordion.RequireOpenedPane = false;
            MyAccordion.SelectedIndex = -1;
            RptViewer.Reset();                                                                        // reset the report viewer
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;             // Set to Local Reporting Mode
            RptViewer.ShowExportControls = true;                                                   // Show The Export Controls
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + ReportRdlcName;
            Microsoft.Reporting.WebForms.ReportParameter rParam1 = new Microsoft.Reporting.WebForms.ReportParameter("DateFrom", ("Date : " + dtfrm.ToString("dd-MM-yyyy")));
            Microsoft.Reporting.WebForms.ReportParameter rParam4 = new Microsoft.Reporting.WebForms.ReportParameter("TLId", cmbTeamLead.Items[cmbTeamLead.SelectedIndex].Text);
            Microsoft.Reporting.WebForms.ReportParameter rParam6 = new Microsoft.Reporting.WebForms.ReportParameter("Copyright", Copyright);
            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            ReportDS.Name = ReportName;
            ReportDS.Value = Dt;
            localRpt.DisplayName = file_name;
            localRpt.DataSources.Clear();
            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1, rParam4, rParam6 });
            localRpt.DataSources.Add(ReportDS);
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
            ReportName = ReportName.Substring(16);
            if (AutoGen == true)
            {
                WriteToExcelFile(exportBytes, FileName);
            }
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error in processing the request. " + ex.Message;
            RptViewer.Reset();
            throw (ex);
        }
    }

    private void FillEmployeeDashboardReport(DataTable Dt, string ReportRdlcName, string ReportName, string FileName)
    {
        string mimeType;
        string encoding;
        string fileNameExtension;
        Microsoft.Reporting.WebForms.Warning[] warnings;
        string[] streamids;
        byte[] exportBytes = null;
        try
        {
            if (Dt.Rows.Count < 1)
            {
                lblStstus.Text = "Your Search Criteria did not match any documents.<br>Try different Search Criteria.";
                RptViewer.Reset();
                return;
            }
            MyAccordion.RequireOpenedPane = false;
            MyAccordion.SelectedIndex = -1;
            RptViewer.Reset();                                                                        // reset the report viewer
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;             // Set to Local Reporting Mode
            RptViewer.ShowExportControls = true;                                                   // Show The Export Controls
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + ReportRdlcName;
            Microsoft.Reporting.WebForms.ReportParameter rParam1 = new Microsoft.Reporting.WebForms.ReportParameter("DateFrom", (dtfrm.ToString()));
            Microsoft.Reporting.WebForms.ReportParameter rParam6 = new Microsoft.Reporting.WebForms.ReportParameter("Copyright", Copyright);
            Microsoft.Reporting.WebForms.ReportParameter rParam4;
            int MaxDate;
            if (int.Parse(cmbDayTo.SelectedValue.ToString().Trim()) >= 10)
                rParam4 = new Microsoft.Reporting.WebForms.ReportParameter("MaxDate", (int.Parse(cmbDayTo.SelectedValue.ToString().Trim()) + 1).ToString());
            else
            {
                MaxDate = int.Parse(cmbDayTo.SelectedValue.Trim()) + 1;
                rParam4 = new Microsoft.Reporting.WebForms.ReportParameter("MaxDate", ("0" + MaxDate.ToString()));
            }
            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            ReportDS.Name = ReportName;
            ReportDS.Value = Dt;
            localRpt.DisplayName = file_name;
            localRpt.DataSources.Clear();
            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1, rParam6, rParam4 });
            localRpt.DataSources.Add(ReportDS);
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
            ReportName = ReportName.Substring(16);
            if (AutoGen == true)
            {
                WriteToExcelFile(exportBytes, FileName);
            }
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error in processing the request. " + ex.Message;
            RptViewer.Reset();
            throw (ex);
        }
    }

    private void FillCSMDashboardReport(DataTable Dt, string ReportRdlcName, string ReportName, string FileName)
    {
        string mimeType;
        string encoding;
        string fileNameExtension;
        Microsoft.Reporting.WebForms.Warning[] warnings;
        string[] streamids;
        byte[] exportBytes = null;
        try
        {
            if (Dt.Rows.Count < 1)
            {
                lblStstus.Text = "Your Search Criteria did not match any documents.<br>Try different Search Criteria.";
                RptViewer.Reset();
                return;
            }
            MyAccordion.RequireOpenedPane = false;
            MyAccordion.SelectedIndex = -1;
            RptViewer.Reset();                                                                        // reset the report viewer
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;             // Set to Local Reporting Mode
            RptViewer.ShowExportControls = true;                                                   // Show The Export Controls
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + ReportRdlcName;
            Microsoft.Reporting.WebForms.ReportParameter rParam1 = new Microsoft.Reporting.WebForms.ReportParameter("DateFrom", (dtTo.ToString()));
            Microsoft.Reporting.WebForms.ReportParameter rParam2 = new Microsoft.Reporting.WebForms.ReportParameter("Copyright", Copyright);
            Microsoft.Reporting.WebForms.ReportParameter rParam3 = new Microsoft.Reporting.WebForms.ReportParameter("DealerDetails", DealerName);
            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            ReportDS.Name = ReportName;
            ReportDS.Value = Dt;
            localRpt.DisplayName = file_name;
            localRpt.DataSources.Clear();
            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1, rParam2, rParam3 });
            localRpt.DataSources.Add(ReportDS);
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
            ReportName = ReportName.Substring(16);
            if (AutoGen == true)
            {
                WriteToExcelFile(exportBytes, FileName);
            }
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error in processing the request. " + ex.Message;
            RptViewer.Reset();
            throw (ex);
        }
    }

    private void FillCardScanReport(DataTable Dt, string ReportRdlcName, string ReportName, string FileName, string ServiceAdvisor)
    {
        string mimeType;
        string encoding;
        string fileNameExtension;
        Microsoft.Reporting.WebForms.Warning[] warnings;
        string[] streamids;
        byte[] exportBytes = null;
        try
        {
            if (Dt.Rows.Count < 1)
            {
                lblStstus.Text = "Your Search Criteria did not match any documents.<br>Try different Search Criteria.";
                RptViewer.Reset();
                return;
            }
            MyAccordion.RequireOpenedPane = false;
            MyAccordion.SelectedIndex = -1;
            RptViewer.Reset();                                                                        // reset the report viewer
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;             // Set to Local Reporting Mode
            RptViewer.ShowExportControls = true;                                                   // Show The Export Controls
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + ReportRdlcName;
            Microsoft.Reporting.WebForms.ReportParameter rParam1 = new Microsoft.Reporting.WebForms.ReportParameter("DateFrom", dtfrm.ToString("dd-MM-yyyy"));
            Microsoft.Reporting.WebForms.ReportParameter rParam3 = new Microsoft.Reporting.WebForms.ReportParameter("SAName", cmbServiceAdvisor.Items[cmbServiceAdvisor.SelectedIndex].Text);
            Microsoft.Reporting.WebForms.ReportParameter rParam4 = new Microsoft.Reporting.WebForms.ReportParameter("TLId", cmbTeamLead.Items[cmbTeamLead.SelectedIndex].Text);
            Microsoft.Reporting.WebForms.ReportParameter rParam6 = new Microsoft.Reporting.WebForms.ReportParameter("Copyright", Copyright);
            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            ReportDS.Name = ReportName;
            ReportDS.Value = Dt;
            localRpt.DisplayName = file_name;
            localRpt.DataSources.Clear();
            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1, rParam3, rParam4, rParam6 });
            localRpt.DataSources.Add(ReportDS);
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
            ReportName = ReportName.Substring(16);
            if (AutoGen == true)
            {
                WriteToExcelFile(exportBytes, FileName);
            }
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error in processing the request. " + ex.Message;
            RptViewer.Reset();
            throw (ex);
        }
    }

    public DataTable GetRepaircosttrend(string ServiceType, string StProc)
    {
        // string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = StProc;
        cmd.Parameters.AddWithValue("@Stype", ServiceType);
        System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    public DataTable GetEmployeeattndnc(string daymnthyr, string StProc)
    {
        //string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = StProc;
        cmd.Parameters.AddWithValue("@Date", daymnthyr);
        if (cmbEmpType.SelectedValue.Trim().ToUpper() == "ALL")
            cmd.Parameters.AddWithValue("@EmpType", "");
        else
            cmd.Parameters.AddWithValue("@EmpType", cmbEmpType.SelectedValue);
        cmd.Parameters.AddWithValue("@IsBodyshop", IsBodyshop);
        System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    static public DataTable GetTechnicianReport(int month, int year)
    {
        // string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        SqlConnection oConn = new SqlConnection(System.Web.HttpContext.Current.Session[HttpContext.Current.Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlCommand cmd = new SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "GetTechReport";
        cmd.Parameters.AddWithValue("@mmonth", month);
        cmd.Parameters.AddWithValue("@myear", year);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    public DataTable GetPDTADTReportDataTable(string StProc)
    {
        DateTime dtf;
        DateTime dtt;
        dtf = dtfrm;
        dtt = dtTo;
        if (AutoGen == true)
        {
            dtf = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
            dtt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
        }
        // string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Datefrom", dtf.ToString("MM/dd/yyyy"));
        cmd.Parameters.AddWithValue("@Dateto", dtt.ToString("MM/dd/yyyy"));
        cmd.Parameters.AddWithValue("@SAName", ServiceAdvisor);
        cmd.Parameters.AddWithValue("@FloorName", floorname);
        cmd.Parameters.AddWithValue("@TLid", TLId);
        cmd.Parameters.AddWithValue("@IsBodyshop", IsBodyshop);
        cmd.CommandText = StProc;
        System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    public DataTable GetReportDataTable(string StProc)
    {
        DateTime dtf;
        DateTime dtt;
        dtf = dtfrm;
        dtt = dtTo;
        if (AutoGen == true)
        {
            dtf = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
            dtt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
        }
        //string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Datefrom", dtf.ToString("MM/dd/yyyy"));
        cmd.Parameters.AddWithValue("@Dateto", dtt.ToString("MM/dd/yyyy"));
        cmd.Parameters.AddWithValue("@SAName", ServiceAdvisor);
        cmd.Parameters.AddWithValue("@FloorName", floorname);
        cmd.Parameters.AddWithValue("@TLid", TLId);
        cmd.Parameters.AddWithValue("@IsBodyshop", IsBodyshop);
        cmd.CommandText = StProc;
        System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    public DataTable GetSAFilterReportDataTable(string StProc)
    {
        //string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Date", daymnthyr);
        cmd.Parameters.AddWithValue("@SAName", ServiceAdvisor);
        cmd.Parameters.AddWithValue("@TLId", TLId);
        cmd.Parameters.AddWithValue("@floorName", floorname);
        cmd.Parameters.AddWithValue("@IsBodyshop", IsBodyshop);
        cmd.CommandText = StProc;
        System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    public DataTable GetCarryForwardReportDataTable(string StProc)
    {
        //string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Datefrom", dtfrm.ToString("MM/dd/yyyy"));
        cmd.Parameters.AddWithValue("@Dateto", dtfrm.ToString("MM/dd/yyyy"));
        cmd.CommandText = StProc;
        System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    public DataTable GetReportDataTable1(string StProc)
    {
        //string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = StProc;
        System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    public DataTable Getjobtrend(DateTime DtFrom, DateTime DtTo, string StProc)
    {
        // string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = StProc;
        cmd.Parameters.AddWithValue("@stdate", DtFrom);
        cmd.Parameters.AddWithValue("@enddate", DtTo);
        System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    public DataTable GetTechAvgDailyTrack(string StProc)
    {
        DateTime dtf;
        DateTime dtt;
        dtf = dtfrm;
        dtt = dtTo;
        if (AutoGen == true)
        {
            dtf = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
            dtt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
        }
        // string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@stDate", dtf.ToString("MM/dd/yyyy"));
        cmd.Parameters.AddWithValue("@endDate", dtt.ToString("MM/dd/yyyy"));
        cmd.CommandText = StProc;
        System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    public DataTable GetReportDataTable(DateTime DtFrom, DateTime DtTo, string StProc)
    {
        DateTime dtf;
        DateTime dtt;
        dtf = DtFrom;
        dtt = DtTo;
        if (AutoGen == true)
        {
            dtf = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
            dtt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
        }
        //string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = StProc;
        cmd.Parameters.AddWithValue("@StartDate", dtf);
        cmd.Parameters.AddWithValue("@EndDate", dtt);
        System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    public DataTable GetSAFilterReportDataTable(DateTime DtFrom, DateTime DtTo, string ServiceAdvisor, string StProc)
    {
        DateTime dtf;
        DateTime dtt;
        dtf = DtFrom;
        dtt = DtTo;
        if (AutoGen == true)
        {
            dtf = new DateTime(DtFrom.Year, DtFrom.Month, 1, 0, 0, 0);
            dtt = new DateTime(DtFrom.Year, DtFrom.Month, DtTo.Day, 23, 59, 59);
        }
        //string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = StProc;
        cmd.Parameters.AddWithValue("@Datefrom", dtf.ToString("MM/dd/yyyy"));
        cmd.Parameters.AddWithValue("@Dateto", dtt.ToString("MM/dd/yyyy"));
        cmd.Parameters.AddWithValue("@SAName", ServiceAdvisor);
        cmd.Parameters.AddWithValue("@FloorName", floorname);
        cmd.Parameters.AddWithValue("@TLId", TLId);
        cmd.Parameters.AddWithValue("@IsBodyshop", IsBodyshop);
        System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }
    public DataTable GetSAFilterReportDataTable(DateTime DtFrom, string ServiceAdvisor, string StProc)
    {
        DateTime dtf;
        DateTime dtt;
        dtf = DtFrom;
        //dtt = DtTo;
        if (AutoGen == true)
        {
            dtf = new DateTime(DtFrom.Year, DtFrom.Month, DtFrom.Day, 0, 0, 0);
            //dtt = new DateTime(DtFrom.Year, DtFrom.Month, DtTo.Day, 23, 59, 59);
        }
        // string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = StProc;
        if (StProc == "GetCarryForwardVehx")
        {
            cmd.Parameters.AddWithValue("@Datefrom", dtf.ToString("MM/dd/yyyy"));
            cmd.Parameters.AddWithValue("@Dateto", dtf.ToString("MM/dd/yyyy"));
        }
        else
            cmd.Parameters.AddWithValue("@Date", dtf.ToString("MM/dd/yyyy"));
        cmd.Parameters.AddWithValue("@SAName", ServiceAdvisor);
        cmd.Parameters.AddWithValue("@FloorName", floorname);
        cmd.Parameters.AddWithValue("@TLId", TLId);
        cmd.Parameters.AddWithValue("@IsBodyshop", IsBodyshop);
        cmd.Parameters.AddWithValue("@VehicleStatus", VehicleStatus);
        System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }
    public DataTable GetReportDataTable2(string StProc)
    {
        //string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = StProc;
        cmd.Parameters.AddWithValue("@Date", DateTime.Now.ToShortDateString());
        System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    public DataTable GetSAFilterReportDataTable(string Date, string ServiceAdvisor, string StProc)
    {
        // string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = StProc;
        cmd.Parameters.AddWithValue("@Date", Date);
        cmd.Parameters.AddWithValue("@EmpName", ServiceAdvisor);
        System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    public DataTable GetReportDataTable(DateTime DtFrom, DateTime DtTo, string RegNo, string serviceType, string StProc)
    {
        // string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = StProc;
        cmd.Parameters.AddWithValue("@StartDate", DtFrom);
        cmd.Parameters.AddWithValue("@EndDate", DtTo);
        cmd.Parameters.AddWithValue("@RegNo", RegNo);
        cmd.Parameters.AddWithValue("@ServiceType", serviceType);
        System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    public DataTable GetReportDataTableAFD(DateTime DtFrom, DateTime DtTo, string RegNo, string serviceType, string ServiceAdvisor, string StProc)
    {
        //string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = StProc;
        cmd.Parameters.AddWithValue("@StartDate", DtFrom);
        cmd.Parameters.AddWithValue("@EndDate", DtTo);
        cmd.Parameters.AddWithValue("@ServiceType", serviceType);
        cmd.Parameters.AddWithValue("@SAName", ServiceAdvisor);
        cmd.Parameters.AddWithValue("@FloorName", floorname);
        cmd.Parameters.AddWithValue("@TLid", TLId);
        cmd.Parameters.AddWithValue("@IsBodyshop", IsBodyshop);
        System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    public DataTable GetReportDataTable(DateTime DtFrom, DateTime DtTo, string RegNo, string serviceType, string ServiceAdvisor, string StProc)
    {
        //string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = StProc;
        cmd.Parameters.AddWithValue("@StartDate", DtFrom);
        cmd.Parameters.AddWithValue("@EndDate", DtTo);
        cmd.Parameters.AddWithValue("@RegNo", RegNo);
        cmd.Parameters.AddWithValue("@ServiceType", serviceType);
        cmd.Parameters.AddWithValue("@SAName", ServiceAdvisor);
        cmd.Parameters.AddWithValue("@FloorName", floorname);
        cmd.Parameters.AddWithValue("@TLid", TLId);
        cmd.Parameters.AddWithValue("@IsBodyshop", IsBodyshop);
        System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    protected DataTable getCardNotScanned(DateTime FromDate, DateTime ToDate, string StProc)
    {
        //string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = StProc;
        cmd.Parameters.AddWithValue("@DateFrom", FromDate);
        cmd.Parameters.AddWithValue("@Dateto", ToDate);
        cmd.Parameters.AddWithValue("@SAName", ServiceAdvisor);
        cmd.Parameters.AddWithValue("@FloorName", "");
        cmd.Parameters.AddWithValue("@TLid", TLId);
        cmd.Parameters.AddWithValue("@IsBodyshop", 0);
        System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    public DataTable GetVehicleAgingReportDataTable(DateTime DtFrom, DateTime DtTo, string RegNo, string ServiceType, string StProc)
    {
        //string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = StProc;
        cmd.Parameters.AddWithValue("@StartDate", DtFrom);
        cmd.Parameters.AddWithValue("@EndDate", DtTo);
        cmd.Parameters.AddWithValue("@RegNo", RegNo);
        cmd.Parameters.AddWithValue("@ServiceType", ServiceType);
        System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    public DataTable Getturnaroundtime(DateTime DtFrom, DateTime DtTo, string ServiceType, string StProc)
    {
        //string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = StProc;
        cmd.Parameters.AddWithValue("@stdate", DtFrom);
        cmd.Parameters.AddWithValue("@enddate", DtTo);
        cmd.Parameters.AddWithValue("@SType", ServiceType);
        System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    public DataTable GetExpressServiceAndPartsTMRAndRoadTestTMR(string StProc, DateTime dFrom, DateTime dTo)
    {
        // string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@dateFrom", dFrom.ToString());
        cmd.Parameters.AddWithValue("@DateTo", dTo.ToString());
        cmd.CommandText = StProc;
        System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    public DataTable GetMachanicProductivity(string StProc, string Mon, string Yr)
    {
        // string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Month", Mon);
        cmd.Parameters.AddWithValue("@Yr", Yr);
        cmd.CommandText = StProc;
        System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    public DataTable GetVehicleInOutMonthlyTrend(string StProc, string dDate, string ServiceAdvisor)
    {
        // string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Date", dtfrm.ToString("MM/dd/yyyy"));
        cmd.Parameters.AddWithValue("@SAName", ServiceAdvisor);
        cmd.Parameters.AddWithValue("@FloorName", floorname);
        cmd.Parameters.AddWithValue("@TLid", TLId);
        cmd.Parameters.AddWithValue("@IsBodyshop", IsBodyshop);
        cmd.CommandText = StProc;
        System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    public DataTable GetSAPerformanceDataTable(string StProc, string dDate, string ServiceAdvisor)
    {
        // string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Date", dtfrm.ToString("MM/dd/yyyy"));
        cmd.Parameters.AddWithValue("@SAName", ServiceAdvisor);
        cmd.Parameters.AddWithValue("@IsBodyshop", IsBodyshop);
        cmd.Parameters.AddWithValue("@FloorName", floorname);
        cmd.Parameters.AddWithValue("@TLId", TLId);
        cmd.Parameters.AddWithValue("@VehicleStatus", VehicleStatus);
        cmd.CommandText = StProc;
        System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    public DataTable GetTechTMR1TechNew(string StProc, string dDate)
    {
        // string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Date", dtfrm.ToString("MM/dd/yyyy"));
        cmd.Parameters.AddWithValue("@TLID", TLId);
        cmd.CommandText = StProc;
        System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    public DataTable GetDeliveryAnalysisReportSAWise(string StProc, string dDate)
    {
        //string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Date", dtfrm.ToString("MM/dd/yyyy"));
        cmd.Parameters.AddWithValue("@SAName", ServiceAdvisor);
        cmd.Parameters.AddWithValue("@FloorName", floorname);
        cmd.Parameters.AddWithValue("@TLid", TLId);
        cmd.Parameters.AddWithValue("@IsBodyshop", IsBodyshop);
        cmd.Parameters.AddWithValue("@VehicleStatus", VehicleStatus);
        cmd.CommandText = StProc;
        System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }
    public DataTable GetPartsData(string StProc, string dDate)
    {
        //string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@RptDate", dtfrm.ToString("MM/dd/yyyy"));

        cmd.CommandText = StProc;
        System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }
    public DataTable GetRptVehicleData(string StProc, string dDate)
    {
        //string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@RptDate", dtfrm.ToString("MM/dd/yyyy"));

        cmd.CommandText = StProc;
        System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }
    public DataTable FillEmployeeDashboard(string StProc, string dDate, string IsBodyShop)
    {
        //string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@RptDate", dtTo.ToString("MM/dd/yyyy"));
        cmd.Parameters.AddWithValue("@Bodyshop", 0);
        cmd.CommandText = StProc;
        System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    public DataTable GetTechTMR1Tech(string StProc, string dDate)
    {
        //string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Date", dtfrm.ToString("MM/dd/yyyy"));
        cmd.Parameters.AddWithValue("@TLid", TLId);
        cmd.Parameters.AddWithValue("@IsBodyshop", IsBodyshop);
        cmd.CommandText = StProc;
        System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    public DataTable GetTechTMR2Tech(string StProc, string dDate)
    {
        // string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Date", dtfrm.ToString("MM/dd/yyyy"));
        cmd.Parameters.AddWithValue("@Type", "1");
        cmd.CommandText = StProc;
        System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    public DataTable GetVehicleInOut(string StProc, string dDate)
    {
        //string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Date", dtfrm.ToString("MM/dd/yyyy"));
        cmd.CommandText = StProc;
        System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    public DataTable GetPlannedVsActual(string StProc, DateTime dDate)
    {
        //string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Date", dtfrm.ToString("MM/dd/yyyy"));
        cmd.Parameters.AddWithValue("@FloorName", "");
        cmd.Parameters.AddWithValue("@SAName", ServiceAdvisor);
        cmd.Parameters.AddWithValue("@TLid", TLId);
        cmd.Parameters.AddWithValue("@IsBodyshop", 0);
        cmd.CommandText = StProc;
        System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    protected void btnLoad_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime d1 = new DateTime();
            DateTime d2 = new DateTime();
            d1 = DateTime.Parse(cmbMnthfrom.SelectedItem.Text.Trim() + "/" + cmbDayFrm.SelectedItem.Text.Trim() + "/" + cmbYearfrom.SelectedItem.Text.Trim());
            d2 = DateTime.Parse(cmbMnthTo.SelectedItem.Text.Trim() + "/" + cmbDayTo.SelectedItem.Text.Trim() + "/" + CmbYearto.SelectedItem.Text.Trim());
            if (DateTime.Compare(d1, d2) <= 0)
            {
                if (CmbRptType.SelectedValue.ToUpper() != "SELECT")
                {
                    GenerateSelectedReport(CmbRptType.SelectedValue, rept_rdlc, rept_proc, file_name);
                }
            }
            else
                lblStstus.Text = "From Date Should Be Less Than Or Equal To Date.!";
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Please Refresh Page To Continue.";//"Error in processing the request. " + ex.Message;
        }
    }

    protected void cmbMnthfrom_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            RptViewer.Reset();
            FillDayCombo(ref cmbYearfrom, ref cmbMnthfrom, ref cmbDayFrm);
            cmbMnthTo.SelectedValue = cmbMnthfrom.SelectedValue;
            CmbYearto.SelectedValue = cmbYearfrom.SelectedValue;
            FillDayCombo(ref CmbYearto, ref cmbMnthTo, ref cmbDayTo);
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Please Select Report Type!";
            CmbRptType.SelectedIndex = -1;
        }
    }

    protected void cmbMnthTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            RptViewer.Reset();
            FillDayCombo(ref CmbYearto, ref cmbMnthTo, ref cmbDayTo);
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }

    private void FillWorkshopAnalysisReport(string rept_Proc, string rept_Rdlc, string FileName)
    {
        string mimeType;
        string encoding;
        string fileNameExtension;
        Microsoft.Reporting.WebForms.Warning[] warnings;
        string[] streamids;
        byte[] exportBytes = null;
        System.Data.DataTable Dt = new System.Data.DataTable();
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlCommand cmd = new SqlCommand("", con);
        cmd.CommandText = rept_Proc.Split(',').GetValue(0).ToString().Substring(16);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        sda.Fill(Dt);

        // Next DataSet
        System.Data.DataTable Dt1 = new System.Data.DataTable();
        cmd = new SqlCommand("", con);
        cmd.CommandText = rept_Proc.Split(',').GetValue(1).ToString().Substring(16);
        cmd.CommandType = CommandType.StoredProcedure;
        sda = new SqlDataAdapter(cmd);
        sda.Fill(Dt1);

        // Next DataSet
        System.Data.DataTable Dt2 = new System.Data.DataTable();
        cmd = new SqlCommand("", con);
        cmd.CommandText = rept_Proc.Split(',').GetValue(2).ToString().Substring(16);
        cmd.CommandType = CommandType.StoredProcedure;
        sda = new SqlDataAdapter(cmd);
        sda.Fill(Dt2);

        if (Dt.Rows.Count <= 0)
        {
            lblStstus.Text = "No data found for date specified .<br> Try different Search Criteria.";
            RptViewer.Reset();
            return;
        }
        MyAccordion.RequireOpenedPane = false;
        MyAccordion.SelectedIndex = -1;
        Microsoft.Reporting.WebForms.ReportParameter rParam1 = new Microsoft.Reporting.WebForms.ReportParameter("Copyright", Copyright);

        try
        {
            string[] rpt_procs = { "", "", "" };
            rpt_procs = rept_Proc.Split(',');
            RptViewer.Reset();
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local; // Set to Local Reporting Mode
            RptViewer.ShowExportControls = true;
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + rept_Rdlc;
            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            localRpt.DataSources.Clear();
            localRpt.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource(rpt_procs[0].ToString(), Dt));
            localRpt.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource(rpt_procs[1].ToString(), Dt1));
            localRpt.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource(rpt_procs[2].ToString(), Dt2));
            this.RptViewer.LocalReport.EnableExternalImages = true;
            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1 });
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
            if (AutoGen == true)
            {
                WriteToExcelFile(exportBytes, FileName);
            }
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error in processing the request.";
            throw (ex);
        }
    }

    private void FillRepeatJobTrendReport(DataTable Dt, string ReportRdlcName, string ReportName)
    {
        string mimeType;
        string encoding;
        string fileNameExtension;
        Microsoft.Reporting.WebForms.Warning[] warnings;
        string[] streamids;
        byte[] exportBytes = null;
        try
        {
            if (Dt.Rows.Count < 1)
            {
                lblStstus.Text = "Your Search Criteria did not match any documents.<br>Try different Search Criteria.";
                RptViewer.Reset();
                return;
            }
            Microsoft.Reporting.WebForms.ReportParameter rParam1 = new Microsoft.Reporting.WebForms.ReportParameter("FromDate", dtfrm.ToString("dd-MM-yyyy"));
            Microsoft.Reporting.WebForms.ReportParameter rParam2 = new Microsoft.Reporting.WebForms.ReportParameter("ToDate", dtTo.ToString("dd-MM-yyyy"));
            RptViewer.Reset();                                                                        // reset the report viewer
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;             // Set to Local Reporting Mode
            RptViewer.ShowExportControls = true;                                                   // Show The Export Controls
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + ReportRdlcName;
            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            ReportDS.Name = ReportName;
            ReportDS.Value = Dt;
            localRpt.DisplayName = file_name;
            localRpt.DataSources.Clear();
            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1, rParam2 });
            localRpt.DataSources.Add(ReportDS);
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
            if (AutoGen == true)
            {
                WriteToExcelFile(exportBytes, ReportName);
            }
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error in processing the request.";
            RptViewer.Reset();
            throw (ex);
        }
    }

    private void FillAdditional1ParaReport(System.Data.DataTable Dt, string ReportRdlcName, string ReportName, string file_name, string ServiceAdvisor)
    {
        string mimeType;
        string encoding;
        string fileNameExtension;
        Microsoft.Reporting.WebForms.Warning[] warnings;
        string[] streamids;
        byte[] exportBytes = null;
        try
        {
            if (Dt.Rows.Count < 1)
            {
                lblStstus.Text = "Your Search Criteria did not match any documents.<br>Try different Search Criteria.";
                RptViewer.Reset();
                return;
            }
            MyAccordion.RequireOpenedPane = false;
            MyAccordion.SelectedIndex = -1;
            Microsoft.Reporting.WebForms.ReportParameter rParam1 = new Microsoft.Reporting.WebForms.ReportParameter("dtt", dtfrm.ToString("dd-MMM-yyyy"));
            Microsoft.Reporting.WebForms.ReportParameter rParam2;
            rParam2 = new Microsoft.Reporting.WebForms.ReportParameter("SAName", cmbServiceAdvisor.Items[cmbServiceAdvisor.SelectedIndex].Text);
            Microsoft.Reporting.WebForms.ReportParameter rParam4 = new Microsoft.Reporting.WebForms.ReportParameter("TLId", cmbTeamLead.Items[cmbTeamLead.SelectedIndex].Text);
            Microsoft.Reporting.WebForms.ReportParameter rParam6 = new Microsoft.Reporting.WebForms.ReportParameter("Copyright", Copyright);
            Microsoft.Reporting.WebForms.ReportParameter rParam7 = new Microsoft.Reporting.WebForms.ReportParameter("VehicleStatus", drpWhiteBoard.Items[drpWhiteBoard.SelectedIndex].Text);
            RptViewer.Reset();                                                                        // reset the report viewer
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;             // Set to Local Reporting Mode
            RptViewer.ShowExportControls = true;                                                   // Show The Export Controls
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + ReportRdlcName;
            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            ReportDS.Name = ReportName;
            ReportDS.Value = Dt;
            localRpt.DisplayName = file_name;
            localRpt.DataSources.Clear();
            localRpt.DataSources.Add(ReportDS);
            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1, rParam2, rParam4, rParam6, rParam7 });
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
            if (AutoGen == true)
            {
                WriteToExcelFile(exportBytes, file_name);
            }
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error in processing the request.";
            RptViewer.Reset();
            throw (ex);
        }
    }

    private void FillAdditionalReportAFD(System.Data.DataTable Dt, string ReportRdlcName, string ReportName, string file_name)
    {
        string mimeType;
        string encoding;
        string fileNameExtension;
        Microsoft.Reporting.WebForms.Warning[] warnings;
        string[] streamids;
        byte[] exportBytes = null;
        try
        {
            if (Dt.Rows.Count < 1)
            {
                lblStstus.Text = "Your Search Criteria did not match any documents.<br>Try different Search Criteria.";
                RptViewer.Reset();
                return;
            }
            MyAccordion.RequireOpenedPane = false;
            MyAccordion.SelectedIndex = -1;
            Microsoft.Reporting.WebForms.ReportParameter rParam1 = new Microsoft.Reporting.WebForms.ReportParameter("StartDate", "From :" + dtfrm.ToString("dd-MM-yyyy"));
            Microsoft.Reporting.WebForms.ReportParameter rParam2 = new Microsoft.Reporting.WebForms.ReportParameter("EndDate", "To :" + dtTo.ToString("dd-MM-yyyy"));
            Microsoft.Reporting.WebForms.ReportParameter rParam4 = new Microsoft.Reporting.WebForms.ReportParameter("TLId", cmbTeamLead.Items[cmbTeamLead.SelectedIndex].Text);
            Microsoft.Reporting.WebForms.ReportParameter rParam6 = new Microsoft.Reporting.WebForms.ReportParameter("SAName", cmbServiceAdvisor.Items[cmbServiceAdvisor.SelectedIndex].Text);
            Microsoft.Reporting.WebForms.ReportParameter rParam7 = new Microsoft.Reporting.WebForms.ReportParameter("ServiceType", cmbServiceType.Items[cmbServiceType.SelectedIndex].Text);
            Microsoft.Reporting.WebForms.ReportParameter rParam8 = new Microsoft.Reporting.WebForms.ReportParameter("Copyright", Copyright);
            RptViewer.Reset();                                                                        // reset the report viewer
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;             // Set to Local Reporting Mode
            RptViewer.ShowExportControls = true;                                                   // Show The Export Controls
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + ReportRdlcName;
            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            ReportDS.Name = ReportName;
            ReportDS.Value = Dt;
            localRpt.DisplayName = file_name;
            localRpt.DataSources.Clear();
            localRpt.DataSources.Add(ReportDS);
            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1, rParam2, rParam4, rParam6, rParam8 });
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
            if (AutoGen == true)
            {
                WriteToExcelFile(exportBytes, file_name);
            }
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error in processing the request.";
            RptViewer.Reset();
            throw (ex);
        }
    }

    private void FillAdditionalReport(System.Data.DataTable Dt, string ReportRdlcName, string ReportName, string file_name)
    {
        string mimeType;
        string encoding;
        string fileNameExtension;
        Microsoft.Reporting.WebForms.Warning[] warnings;
        string[] streamids;
        byte[] exportBytes = null;
        try
        {
            if (Dt.Rows.Count < 1)
            {
                lblStstus.Text = "Your Search Criteria did not match any documents.<br>Try different Search Criteria.";
                RptViewer.Reset();
                return;
            }
            MyAccordion.RequireOpenedPane = false;
            MyAccordion.SelectedIndex = -1;
            Microsoft.Reporting.WebForms.ReportParameter rParam1 = new Microsoft.Reporting.WebForms.ReportParameter("StartDate", "From :" + dtfrm.ToString("dd-MM-yyyy"));
            Microsoft.Reporting.WebForms.ReportParameter rParam2 = new Microsoft.Reporting.WebForms.ReportParameter("EndDate", "To :" + dtTo.ToString("dd-MM-yyyy"));
            Microsoft.Reporting.WebForms.ReportParameter rParam4 = new Microsoft.Reporting.WebForms.ReportParameter("TLId", cmbTeamLead.Items[cmbTeamLead.SelectedIndex].Text);
            Microsoft.Reporting.WebForms.ReportParameter rParam6 = new Microsoft.Reporting.WebForms.ReportParameter("SAName", cmbServiceAdvisor.Items[cmbServiceAdvisor.SelectedIndex].Text);
            Microsoft.Reporting.WebForms.ReportParameter rParam7 = new Microsoft.Reporting.WebForms.ReportParameter("Copyright", Copyright);
            RptViewer.Reset();                                                                        // reset the report viewer
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;             // Set to Local Reporting Mode
            RptViewer.ShowExportControls = true;                                                   // Show The Export Controls
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + ReportRdlcName;
            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            ReportDS.Name = ReportName;
            ReportDS.Value = Dt;
            localRpt.DisplayName = file_name;
            localRpt.DataSources.Clear();
            localRpt.DataSources.Add(ReportDS);
            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1, rParam2, rParam4, rParam6, rParam7 });
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
            if (AutoGen == true)
            {
                WriteToExcelFile(exportBytes, file_name);
            }
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error in processing the request.";
            RptViewer.Reset();
            throw (ex);
        }
    }

    private void FillTechnicalAVGDailyTrackReport(System.Data.DataTable Dt, string ReportRdlcName, string ReportName, string FileName)
    {
        string mimeType;
        string encoding;
        string fileNameExtension;
        Microsoft.Reporting.WebForms.Warning[] warnings;
        string[] streamids;
        byte[] exportBytes = null;
        try
        {
            if (Dt.Rows.Count < 1)
            {
                lblStstus.Text = "Your Search Criteria did not match any documents.<br>Try different Search Criteria.";
                RptViewer.Reset();
                return;
            }
            MyAccordion.RequireOpenedPane = false;
            MyAccordion.SelectedIndex = -1;
            Microsoft.Reporting.WebForms.ReportParameter rParam1 = new Microsoft.Reporting.WebForms.ReportParameter("Copyright", Copyright);
            RptViewer.Reset();
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
            RptViewer.ShowExportControls = true;
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + ReportRdlcName;
            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            ReportDS.Name = ReportName;
            ReportDS.Value = Dt;
            localRpt.DisplayName = file_name;
            localRpt.DataSources.Clear();
            localRpt.DataSources.Add(ReportDS);
            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1 });
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
            if (AutoGen == true)
            {
                WriteToExcelFile(exportBytes, FileName);
            }
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error in processing the request.";
            RptViewer.Reset();
            throw (ex);
        }
    }

    private void FillETMReport(System.Data.DataTable Dt, string ReportRdlcName, string ReportName, string FileName)
    {
        string mimeType;
        string encoding;
        string fileNameExtension;
        Microsoft.Reporting.WebForms.Warning[] warnings;
        string[] streamids;
        byte[] exportBytes = null;
        try
        {
            if (Dt.Rows.Count < 1)
            {
                lblStstus.Text = "Your Search Criteria did not match any documents.<br>Try different Search Criteria.";
                RptViewer.Reset();
                return;
            }
            MyAccordion.RequireOpenedPane = false;
            MyAccordion.SelectedIndex = -1;
            if (EmpType.ToString().Trim() == "")
            {
                EmpType = "ALL";
            }
            Microsoft.Reporting.WebForms.ReportParameter rParam1 = new Microsoft.Reporting.WebForms.ReportParameter("Datefrom", dtfrm.ToString("dd-MM-yyyy"));
            Microsoft.Reporting.WebForms.ReportParameter rParam2 = new Microsoft.Reporting.WebForms.ReportParameter("Dateto", dtTo.ToString("dd-MM-yyyy"));
            Microsoft.Reporting.WebForms.ReportParameter rParam3 = new Microsoft.Reporting.WebForms.ReportParameter("EmpType", EmpType.ToString());
            Microsoft.Reporting.WebForms.ReportParameter rParam5 = new Microsoft.Reporting.WebForms.ReportParameter("Copyright", Copyright);
            Microsoft.Reporting.WebForms.ReportParameter rParam6 = new Microsoft.Reporting.WebForms.ReportParameter("VehicleStatus", drpWhiteBoard.Items[drpWhiteBoard.SelectedIndex].Text);
            RptViewer.Reset();                                                                        // reset the report viewer
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;             // Set to Local Reporting Mode
            RptViewer.ShowExportControls = true;                                                   // Show The Export Controls
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + ReportRdlcName;
            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            ReportDS.Name = ReportName;
            ReportDS.Value = Dt;
            localRpt.DisplayName = file_name;
            localRpt.DataSources.Clear();
            localRpt.DataSources.Add(ReportDS);
            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1, rParam2, rParam3, rParam5, rParam6 });
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
            if (AutoGen == true)
            {
                WriteToExcelFile(exportBytes, FileName);
            }
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error in processing the request.";
            RptViewer.Reset();
            throw (ex);
        }
    }

    private void FillTrendsReport1(System.Data.DataTable Dt, string ReportRdlcName, string ReportName, string FileName)
    {
        string mimeType;
        string encoding;
        string fileNameExtension;
        Microsoft.Reporting.WebForms.Warning[] warnings;
        string[] streamids;
        byte[] exportBytes = null;
        try
        {
            if (Dt.Rows.Count < 1)
            {
                lblStstus.Text = "Your Search Criteria did not match any documents.<br>Try different Search Criteria.";
                RptViewer.Reset();
                return;
            }
            MyAccordion.RequireOpenedPane = false;
            MyAccordion.SelectedIndex = -1;
            RptViewer.Reset();
            Microsoft.Reporting.WebForms.ReportParameter rParam1 = new Microsoft.Reporting.WebForms.ReportParameter("SAName", cmbServiceAdvisor.Items[cmbServiceAdvisor.SelectedIndex].Text.ToString());
            Microsoft.Reporting.WebForms.ReportParameter rParam2 = new Microsoft.Reporting.WebForms.ReportParameter("FrmDate", dtfrm.ToString("dd-MM-yyyy"));
            Microsoft.Reporting.WebForms.ReportParameter rParam3 = new Microsoft.Reporting.WebForms.ReportParameter("ToDate", dtTo.ToString("dd-MM-yyyy"));
            Microsoft.Reporting.WebForms.ReportParameter rParam5 = new Microsoft.Reporting.WebForms.ReportParameter("TLId", cmbTeamLead.Items[cmbTeamLead.SelectedIndex].Text);
            Microsoft.Reporting.WebForms.ReportParameter rParam7 = new Microsoft.Reporting.WebForms.ReportParameter("Copyright", Copyright);
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;             // Set to Local Reporting Mode
            RptViewer.ShowExportControls = true;                                                   // Show The Export Controls
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + ReportRdlcName;
            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            ReportDS.Name = ReportName;
            ReportDS.Value = Dt;
            localRpt.DisplayName = file_name;
            localRpt.DataSources.Clear();
            localRpt.DataSources.Add(ReportDS);
            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1, rParam2, rParam3, rParam5, rParam7 });
            localRpt.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ProTrackDataSet_udpSADetails", GetSADetails(daymnthyr, daymnthyrTo, EmpType, "udpSADetails")));
            localRpt.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ProTrackDataSet_GetSDD_PDD", GetSDDPDD(daymnthyr, daymnthyrTo, EmpType, floorname, TLId, IsBodyshop, "GetSDD_PDD")));
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
            if (AutoGen == true)
            {
                WriteToExcelFile(exportBytes, FileName);
            }
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error in processing the request.";
            RptViewer.Reset();
            throw (ex);
        }
    }

    public DataTable GetSDDPDD(string frmdate, string todate, string SAName, string floorname, string TLId, string IsBodyshop, string ProcedureName)
    {
        string sConnString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        SqlConnection oConn = new SqlConnection(sConnString);
        SqlCommand cmd = new SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt2 = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = ProcedureName;

        cmd.Parameters.AddWithValue("@Datefrom", frmdate);
        cmd.Parameters.AddWithValue("@Dateto", todate);
        cmd.Parameters.AddWithValue("@SAName", SAName);
        cmd.Parameters.AddWithValue("@FloorName", floorname);
        cmd.Parameters.AddWithValue("@TLId", TLId);
        cmd.Parameters.AddWithValue("@IsBodyshop", IsBodyshop);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt2);
            return oDt2;
        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    public DataTable GetSADetails(string frmdate, string todate, string SAName, string ProcedureName)
    {
        string sConnString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        SqlConnection oConn = new SqlConnection(sConnString);
        SqlCommand cmd = new SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt1 = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = ProcedureName;

        cmd.Parameters.AddWithValue("@Datefrom", frmdate);
        cmd.Parameters.AddWithValue("@Dateto", todate);
        cmd.Parameters.AddWithValue("@EmpName", SAName);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt1);
            return oDt1;
        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }
    public DataTable GetDashboardDataTable(string StProc)
    {
        DateTime dtf;
        DateTime dtt;
        dtf = dtfrm;
        dtt = dtTo;
        if (AutoGen == true)
        {
            dtf = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
            dtt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
        }
        // string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = StProc;
        System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    public DataTable GetDashboard(string StProc)
    {
        DateTime dtf;
        DateTime dtt;
        dtf = dtfrm;
        dtt = dtTo;
        if (AutoGen == true)
        {
            dtf = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
            dtt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
        }
        //string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("", oConn);
        cmd.CommandTimeout = 180;
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Bodyshop", "0");
        cmd.Parameters.AddWithValue("@RptDate", dtTo.ToString("MM/dd/yyyy"));
        cmd.CommandText = StProc;
        System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    private void FillTrendsReport(System.Data.DataTable Dt, string ReportRdlcName, string ReportName, string FileName, string emptype, string date)
    {
        string mimeType;
        string encoding;
        string fileNameExtension;
        Microsoft.Reporting.WebForms.Warning[] warnings;
        string[] streamids;
        byte[] exportBytes = null;
        try
        {
            if (Dt.Rows.Count < 1)
            {
                lblStstus.Text = "Your Search Criteria did not match any documents.<br>Try different Search Criteria.";
                RptViewer.Reset();
                return;
            }
            MyAccordion.RequireOpenedPane = false;
            MyAccordion.SelectedIndex = -1;
            RptViewer.Reset();
            Microsoft.Reporting.WebForms.ReportParameter rParam1 = new Microsoft.Reporting.WebForms.ReportParameter("EmpType", (EmpType == "" ? "All" : EmpType));
            Microsoft.Reporting.WebForms.ReportParameter rParam2 = new Microsoft.Reporting.WebForms.ReportParameter("Date", (System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(strMonth))) + "-" + cmbYearfrom.SelectedValue);
            Microsoft.Reporting.WebForms.ReportParameter rParam4 = new Microsoft.Reporting.WebForms.ReportParameter("Copyright", Copyright);
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
            RptViewer.ShowExportControls = true;
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + ReportRdlcName;
            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            ReportDS.Name = ReportName;
            ReportDS.Value = Dt;
            localRpt.DisplayName = file_name;
            localRpt.DataSources.Clear();
            localRpt.DataSources.Add(ReportDS);
            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1, rParam2, rParam4 });
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
            if (AutoGen == true)
            {
                WriteToExcelFile(exportBytes, FileName);
            }
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error in processing the request.";
            RptViewer.Reset();
            throw (ex);
        }
    }

    private void FillReport(System.Data.DataTable Dt, string ReportRdlcName, string ReportName, string FileName)
    {
        string mimeType;
        string encoding;
        string fileNameExtension;
        Microsoft.Reporting.WebForms.Warning[] warnings;
        string[] streamids;
        byte[] exportBytes = null;
        try
        {
            if (Dt.Rows.Count < 1)
            {
                lblStstus.Text = "Your Search Criteria did not match any documents.<br>Try different Search Criteria.";
                RptViewer.Reset();
                return;
            }
            MyAccordion.RequireOpenedPane = false;
            MyAccordion.SelectedIndex = -1;
            Microsoft.Reporting.WebForms.ReportParameter rParam1 = new Microsoft.Reporting.WebForms.ReportParameter("DateFrmTxt", "From: ");
            Microsoft.Reporting.WebForms.ReportParameter rParam2 = new Microsoft.Reporting.WebForms.ReportParameter("DateFrom", dtfrm.ToString("dd-MM-yyyy"));
            Microsoft.Reporting.WebForms.ReportParameter rParam3 = new Microsoft.Reporting.WebForms.ReportParameter("DateToTxt", "To: ");
            Microsoft.Reporting.WebForms.ReportParameter rParam4 = new Microsoft.Reporting.WebForms.ReportParameter("DateTo", dtTo.ToString("dd-MM-yyyy"));
            Microsoft.Reporting.WebForms.ReportParameter rParam5 = new Microsoft.Reporting.WebForms.ReportParameter("SANameTxt", "Service Advisor:");
            Microsoft.Reporting.WebForms.ReportParameter rParam6 = new Microsoft.Reporting.WebForms.ReportParameter("SAName", cmbServiceAdvisor.Items[cmbServiceAdvisor.SelectedIndex].Text);
            Microsoft.Reporting.WebForms.ReportParameter rParam7 = new Microsoft.Reporting.WebForms.ReportParameter("WMNametxt", "Works Manager: ");
            Microsoft.Reporting.WebForms.ReportParameter rParam8 = new Microsoft.Reporting.WebForms.ReportParameter("WMName", WorksManager);
            Microsoft.Reporting.WebForms.ReportParameter rParam11 = new Microsoft.Reporting.WebForms.ReportParameter("Aplustxt", "APlus: ");
            Microsoft.Reporting.WebForms.ReportParameter rParam12 = new Microsoft.Reporting.WebForms.ReportParameter("APlus", CmbAplus.SelectedValue);
            Microsoft.Reporting.WebForms.ReportParameter rParam9 = new Microsoft.Reporting.WebForms.ReportParameter("VehicleNumbertxt", "Vehicle Number: ");
            Microsoft.Reporting.WebForms.ReportParameter rParam10 = new Microsoft.Reporting.WebForms.ReportParameter("VehicleNo", RegNo);
            Microsoft.Reporting.WebForms.ReportParameter rParam13 = new Microsoft.Reporting.WebForms.ReportParameter("Deliveredtxt", "Delivery Status: ");
            Microsoft.Reporting.WebForms.ReportParameter rParam14 = new Microsoft.Reporting.WebForms.ReportParameter("Delivered", cmbDeliveryStatus.SelectedValue);
            Microsoft.Reporting.WebForms.ReportParameter rParam16 = new Microsoft.Reporting.WebForms.ReportParameter("TLId", cmbTeamLead.Items[cmbTeamLead.SelectedIndex].Text);
            Microsoft.Reporting.WebForms.ReportParameter rParam18 = new Microsoft.Reporting.WebForms.ReportParameter("Copyright", Copyright);
            if (RegNo == "")
            {
                rParam9.Values.Clear();
                rParam10.Values.Clear();
                if (ServiceAdvisor == "")
                {
                    rParam5.Values.Clear();
                }
                if (cmbDeliveryStatus.SelectedValue == "ALL")
                {
                    rParam13.Values.Clear();
                    rParam14.Values.Clear();
                }
                switch (Aplus)
                {
                    case "0":
                        break;

                    case "1":
                        break;

                    case "":
                        rParam11.Values.Clear();
                        rParam12.Values.Clear();
                        break;
                }
            }
            else
            {
                rParam1.Values.Clear();
                rParam2.Values.Clear();
                rParam3.Values.Clear();
                rParam4.Values.Clear();
                rParam7.Values.Clear();
                rParam8.Values.Clear();
                rParam5.Values.Clear();
                rParam6.Values.Clear();
                rParam11.Values.Clear();
                rParam12.Values.Clear();
                rParam13.Values.Clear();
                rParam14.Values.Clear();
                rParam16.Values.Clear();
            }
            RptViewer.Reset();                                                                        // reset the report viewer
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;             // Set to Local Reporting Mode
            RptViewer.ShowExportControls = true;                                                   // Show The Export Controls
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + ReportRdlcName;

            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            ReportDS.Name = ReportName;
            ReportDS.Value = Dt;
            localRpt.DisplayName = file_name;
            localRpt.DataSources.Clear();
            localRpt.DataSources.Add(ReportDS);
            localRpt.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ProTrackDataSet_ProcessValues", GetSummaryReport("ProcessValues")));
            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1, rParam2, rParam3, rParam4, rParam5, rParam6, rParam7, rParam8, rParam9, rParam10, rParam11, rParam12, rParam13, rParam14, rParam16, rParam18 });
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
            if (AutoGen == true)
            {
                WriteToExcelFile(exportBytes, FileName);
            }
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error in processing the request. " + ex.Message;
            RptViewer.Reset();
            throw (ex);
        }
    }

    public DataTable GetSummaryReport(string ProcedureName)
    {
        string sConnString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        SqlConnection oConn = new SqlConnection(sConnString);
        SqlCommand cmd = new SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = ProcedureName;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }
    private void FillReport_TMR(System.Data.DataTable Dt, string ReportRdlcName, string ReportName, string FileName)
    {
        string mimeType;
        string encoding;
        string fileNameExtension;
        Microsoft.Reporting.WebForms.Warning[] warnings;
        string[] streamids;
        byte[] exportBytes = null;
        try
        {
            if (Dt.Rows.Count < 1)
            {
                lblStstus.Text = "Your Search Criteria did not match any documents.<br>Try different Search Criteria.";
                RptViewer.Reset();
                return;
            }
            MyAccordion.RequireOpenedPane = false;
            MyAccordion.SelectedIndex = -1;
            Microsoft.Reporting.WebForms.ReportParameter rParam1 = new Microsoft.Reporting.WebForms.ReportParameter("DateFrmTxt", "From: ");
            Microsoft.Reporting.WebForms.ReportParameter rParam2 = new Microsoft.Reporting.WebForms.ReportParameter("DateFrom", dtfrm.ToString("dd-MM-yyyy"));
            Microsoft.Reporting.WebForms.ReportParameter rParam3 = new Microsoft.Reporting.WebForms.ReportParameter("DateToTxt", "To: ");
            Microsoft.Reporting.WebForms.ReportParameter rParam4 = new Microsoft.Reporting.WebForms.ReportParameter("DateTo", dtTo.ToString("dd-MM-yyyy"));
            Microsoft.Reporting.WebForms.ReportParameter rParam5 = new Microsoft.Reporting.WebForms.ReportParameter("SANameTxt", "Service Advisor:");
            Microsoft.Reporting.WebForms.ReportParameter rParam6 = new Microsoft.Reporting.WebForms.ReportParameter("SAName", cmbServiceAdvisor.Items[cmbServiceAdvisor.SelectedIndex].Text);
            Microsoft.Reporting.WebForms.ReportParameter rParam7 = new Microsoft.Reporting.WebForms.ReportParameter("WMNametxt", "Works Manager: ");
            Microsoft.Reporting.WebForms.ReportParameter rParam8 = new Microsoft.Reporting.WebForms.ReportParameter("WMName", WorksManager);
            Microsoft.Reporting.WebForms.ReportParameter rParam11 = new Microsoft.Reporting.WebForms.ReportParameter("Aplustxt", "APlus: ");
            Microsoft.Reporting.WebForms.ReportParameter rParam12 = new Microsoft.Reporting.WebForms.ReportParameter("APlus", CmbAplus.SelectedValue);
            Microsoft.Reporting.WebForms.ReportParameter rParam9 = new Microsoft.Reporting.WebForms.ReportParameter("VehicleNumbertxt", "Vehicle Number: ");
            Microsoft.Reporting.WebForms.ReportParameter rParam10 = new Microsoft.Reporting.WebForms.ReportParameter("VehicleNo", RegNo);
            Microsoft.Reporting.WebForms.ReportParameter rParam13 = new Microsoft.Reporting.WebForms.ReportParameter("Deliveredtxt", "Delivery Status: ");
            Microsoft.Reporting.WebForms.ReportParameter rParam14 = new Microsoft.Reporting.WebForms.ReportParameter("Delivered", cmbDeliveryStatus.SelectedValue);
            Microsoft.Reporting.WebForms.ReportParameter rParam16 = new Microsoft.Reporting.WebForms.ReportParameter("TLId", cmbTeamLead.Items[cmbTeamLead.SelectedIndex].Text);
            Microsoft.Reporting.WebForms.ReportParameter rParam18 = new Microsoft.Reporting.WebForms.ReportParameter("Copyright", Copyright);
            Microsoft.Reporting.WebForms.ReportParameter rparam19 = new Microsoft.Reporting.WebForms.ReportParameter("VehicleStatus", drpWhiteBoard.Items[drpWhiteBoard.SelectedIndex].Text);
            if (RegNo == "")
            {
                rParam9.Values.Clear();
                rParam10.Values.Clear();
                if (ServiceAdvisor == "")
                {
                    rParam5.Values.Clear();
                }
                if (cmbDeliveryStatus.SelectedValue == "ALL")
                {
                    rParam13.Values.Clear();
                    rParam14.Values.Clear();
                }
                switch (Aplus)
                {
                    case "0":
                        break;

                    case "1":
                        break;

                    case "":
                        rParam11.Values.Clear();
                        rParam12.Values.Clear();
                        break;
                }
            }
            else
            {
                rParam1.Values.Clear();
                rParam2.Values.Clear();
                rParam3.Values.Clear();
                rParam4.Values.Clear();
                rParam7.Values.Clear();
                rParam8.Values.Clear();
                rParam5.Values.Clear();
                rParam6.Values.Clear();
                rParam11.Values.Clear();
                rParam12.Values.Clear();
                rParam13.Values.Clear();
                rParam14.Values.Clear();
                rParam16.Values.Clear();
            }
            RptViewer.Reset();                                                                        // reset the report viewer
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;             // Set to Local Reporting Mode
            RptViewer.ShowExportControls = true;                                                   // Show The Export Controls
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + ReportRdlcName;

            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            ReportDS.Name = ReportName;
            ReportDS.Value = Dt;
            localRpt.DisplayName = file_name;
            localRpt.DataSources.Clear();
            localRpt.DataSources.Add(ReportDS);
            localRpt.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ProTrackDataSet_ProcessValues", GetSummaryReport("ProcessValues")));
            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1, rParam2, rParam3, rParam4, rParam5, rParam6, rParam7, rParam8, rParam9, rParam10, rParam11, rParam12, rParam13, rParam14, rParam16, rParam18, rparam19 });
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
            if (AutoGen == true)
            {
                WriteToExcelFile(exportBytes, FileName);
            }
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error in processing the request. " + ex.Message;
            RptViewer.Reset();
            throw (ex);
        }
    }
    protected void getSelectedReportDetails()
    {
        try
        {
            if (CmbRptType.SelectedIndex > 0)
            {
                SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
                SqlDataAdapter sda = new SqlDataAdapter("SELECT Rept_Procedure, Rept_Rdlc, Autogen, FileName FROM tbl_ReportMenu WHERE (SlNo = " + CmbRptType.SelectedValue.Trim() + ")", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    rept_proc = dt.Rows[0]["Rept_Procedure"].ToString();
                    rept_rdlc = dt.Rows[0]["Rept_Rdlc"].ToString();
                    file_name = dt.Rows[0]["FileName"].ToString();
                    if (file_name.Contains("_"))
                    {
                        file_name = file_name.Replace("_", " ");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Please Re-select Report Name.";
            CmbRptType.SelectedIndex = 0;
        }
    }

    protected void CmbRptType_SelectedIndexChanged(object sender, EventArgs e)
    {
        MultiView1.Visible = true;
        RptViewer.Reset();
        getSelectedReportDetails();
        cmbMnthTo.SelectedIndex = cmbMnthfrom.SelectedIndex;
        cmbDayTo.SelectedIndex = cmbDayFrm.SelectedIndex;
        cmbYear.SelectedIndex = cmbYear.SelectedIndex;
        cmbEmpType.Enabled = false;
        MyAccordion.SelectedIndex = 0;
        MyAccordion.RequireOpenedPane = true;
        drpWhiteBoard.Enabled = false;
        drpWhiteBoard.SelectedValue = "ALL";
        cmbTeamLead.SelectedIndex = -1;
        if (CmbRptType.SelectedIndex > 0)
        {
            if (Int32.Parse(CmbRptType.SelectedValue.ToString()) == 24)
            {
                MultiView1.ActiveViewIndex = 0;
                cmbYearfrom.Enabled = true;
                cmbMnthfrom.Enabled = true;
                cmbDayFrm.Enabled = true;
                CmbYearto.Enabled = true;
                cmbDayTo.Enabled = true;
                cmbMnthTo.Enabled = true;
                cmbServiceType.Enabled = true;
                cmbEmpType.Enabled = false;
                txtRegNo.Enabled = true;
                cmbTeamLead.Enabled = true;
                cmbServiceAdvisor.Enabled = false;
                CmbAplus.Enabled = false;
                cmbDeliveryStatus.Enabled = false;
            }
            else if (Int32.Parse(CmbRptType.SelectedValue.ToString()) == 76)
            {
                MultiView1.ActiveViewIndex = 0;
                cmbYearfrom.Enabled = true;
                cmbMnthfrom.Enabled = true;
                cmbDayFrm.Enabled = true;
                CmbYearto.Enabled = false;
                cmbDayTo.Enabled = false;
                cmbMnthTo.Enabled = false;
                cmbServiceType.Enabled = false;
                cmbEmpType.Enabled = false;
                txtRegNo.Enabled = false;
                cmbTeamLead.Enabled = false;
                cmbServiceAdvisor.Enabled = false;
                CmbAplus.Enabled = false;
                cmbDeliveryStatus.Enabled = false;
            }

            else if (Int32.Parse(CmbRptType.SelectedValue.ToString()) == 25)
            {
                MultiView1.ActiveViewIndex = 0;
                cmbYearfrom.Enabled = true;
                cmbMnthfrom.Enabled = true;
                cmbDayFrm.Enabled = true;
                CmbYearto.Enabled = true;
                cmbDayTo.Enabled = true;
                cmbMnthTo.Enabled = true;
                cmbServiceType.Enabled = true;
                cmbEmpType.Enabled = false;
                txtRegNo.Enabled = true;
                cmbTeamLead.Enabled = true;
                cmbServiceAdvisor.Enabled = true;
                CmbAplus.Enabled = false;
                cmbDeliveryStatus.Enabled = false;
            }
            else if (Int32.Parse(CmbRptType.SelectedValue.ToString()) == 65)
            {
                MultiView1.ActiveViewIndex = 0;
                cmbYearfrom.Enabled = true;
                cmbMnthfrom.Enabled = true;
                cmbDayFrm.Enabled = true;
                CmbYearto.Enabled = false;
                cmbDayTo.Enabled = false;
                cmbMnthTo.Enabled = false;
                cmbServiceType.Enabled = false;
                cmbEmpType.Enabled = false;
                txtRegNo.Enabled = false;
                cmbTeamLead.Enabled = true;
                cmbServiceAdvisor.Enabled = false;
                CmbAplus.Enabled = false;
                cmbDeliveryStatus.Enabled = false;
            }
            else if (Int32.Parse(CmbRptType.SelectedValue.ToString()) == 64)
            {
                MultiView1.ActiveViewIndex = 0;
                cmbYearfrom.Enabled = true;
                cmbMnthfrom.Enabled = true;
                cmbDayFrm.Enabled = true;
                CmbYearto.Enabled = false;
                cmbDayTo.Enabled = false;
                cmbMnthTo.Enabled = false;
                cmbServiceType.Enabled = false;
                txtRegNo.Enabled = false;
                cmbServiceAdvisor.Enabled = true;
                CmbAplus.Enabled = false;
                cmbTeamLead.Enabled = true;
                cmbDeliveryStatus.Enabled = false;
            }
            else if (Int32.Parse(CmbRptType.SelectedValue.ToString()) > 7 && Int32.Parse(CmbRptType.SelectedValue.ToString()) <= 9)
            {
                MultiView1.ActiveViewIndex = 0;
                cmbYearfrom.Enabled = true;
                cmbMnthfrom.Enabled = true;
                cmbDayFrm.Enabled = true;
                CmbYearto.Enabled = true;
                cmbDayTo.Enabled = true;
                cmbMnthTo.Enabled = true;
                cmbServiceType.Enabled = false;
                txtRegNo.Enabled = false;
                cmbTeamLead.Enabled = true;
                cmbServiceAdvisor.Enabled = false;
                CmbAplus.Enabled = false;
                cmbDeliveryStatus.Enabled = false;
            }
            else if (Int32.Parse(CmbRptType.SelectedValue.ToString()) == 13)
            {
                MultiView1.ActiveViewIndex = 0;
                cmbYearfrom.Enabled = true;
                cmbMnthfrom.Enabled = true;
                cmbDayFrm.Enabled = true;
                CmbYearto.Enabled = true;
                cmbDayTo.Enabled = true;
                cmbMnthTo.Enabled = true;
                cmbServiceType.Enabled = false;
                txtRegNo.Enabled = false;
                cmbTeamLead.Enabled = true;
                cmbServiceAdvisor.Enabled = true;
                CmbAplus.Enabled = false;
                cmbDeliveryStatus.Enabled = false;
            }
            else if (Int32.Parse(CmbRptType.SelectedValue.ToString()) == 14 || Int32.Parse(CmbRptType.SelectedValue.ToString()) == 15)
            {
                MultiView1.ActiveViewIndex = 0;
                cmbYearfrom.Enabled = true;
                cmbMnthfrom.Enabled = true;
                cmbDayFrm.Enabled = true;
                CmbYearto.Enabled = true;
                cmbDayTo.Enabled = true;
                cmbMnthTo.Enabled = true;
                cmbServiceType.Enabled = false;
                txtRegNo.Enabled = false;
                cmbTeamLead.Enabled = true;
                cmbServiceAdvisor.Enabled = false;
                CmbAplus.Enabled = false;
                cmbDeliveryStatus.Enabled = false;
            }
            else if (Int32.Parse(CmbRptType.SelectedValue.ToString()) == 18 || Int32.Parse(CmbRptType.SelectedValue.ToString()) == 19)
            {
                MultiView1.ActiveViewIndex = 0;
                cmbYearfrom.Enabled = true;
                cmbMnthfrom.Enabled = true;
                cmbDayFrm.Enabled = true;
                CmbYearto.Enabled = true;
                cmbDayTo.Enabled = true;
                cmbMnthTo.Enabled = true;
                cmbServiceType.Enabled = false;
                txtRegNo.Enabled = false;
                cmbServiceAdvisor.Enabled = false;
                CmbAplus.Enabled = false;
                cmbTeamLead.Enabled = false;
                cmbDeliveryStatus.Enabled = false;
            }
            else if (Int32.Parse(CmbRptType.SelectedValue.ToString()) == 7)
            {
                MultiView1.ActiveViewIndex = 0;
                cmbYearfrom.Enabled = true;
                cmbMnthfrom.Enabled = true;
                cmbDayFrm.Enabled = false;
                CmbYearto.Enabled = false;
                cmbDayTo.Enabled = false;
                cmbMnthTo.Enabled = false;
                cmbServiceAdvisor.Enabled = false;
                CmbAplus.Enabled = false;
                cmbServiceType.Enabled = false;
                cmbEmpType.Enabled = true;
                cmbTeamLead.Enabled = false;
                cmbDeliveryStatus.Enabled = false;
                txtRegNo.Enabled = false;
            }
            else if (Int32.Parse(CmbRptType.SelectedValue.ToString()) == 36) //FROM MONTH YEAR
            {
                MultiView1.ActiveViewIndex = 0;
                cmbYearfrom.Enabled = true;
                cmbMnthfrom.Enabled = true;
                cmbDayFrm.Enabled = false;
                CmbYearto.Enabled = false;
                cmbDayTo.Enabled = false;
                cmbMnthTo.Enabled = false;
                cmbTeamLead.Enabled = true;
                cmbServiceAdvisor.Enabled = false;
                CmbAplus.Enabled = false;
                cmbServiceType.Enabled = false;
                cmbDeliveryStatus.Enabled = false;
                txtRegNo.Enabled = false;
            }
            else if (Int32.Parse(CmbRptType.SelectedValue.ToString()) == 26) // SERVICE TYPE
            {
                MultiView1.ActiveViewIndex = 0;
                cmbYearfrom.Enabled = false;
                cmbMnthfrom.Enabled = false;
                cmbDayFrm.Enabled = false;
                CmbYearto.Enabled = false;
                cmbDayTo.Enabled = false;
                cmbMnthTo.Enabled = false;
                cmbTeamLead.Enabled = true;
                cmbServiceType.Enabled = true;
                cmbServiceAdvisor.Enabled = false;
                CmbAplus.Enabled = false;
                cmbDeliveryStatus.Enabled = false;
                txtRegNo.Enabled = false;
            }
            else if (Int32.Parse(CmbRptType.SelectedValue.ToString()) == 1 || Int32.Parse(CmbRptType.SelectedValue.ToString()) == 26 || Int32.Parse(CmbRptType.SelectedValue.ToString()) == 35)
            {
                MultiView1.ActiveViewIndex = 0;
                cmbYearfrom.Enabled = true;
                cmbMnthfrom.Enabled = true;
                cmbDayFrm.Enabled = true;
                CmbYearto.Enabled = true;
                cmbDayTo.Enabled = true;
                cmbMnthTo.Enabled = true;
                cmbTeamLead.Enabled = true;
                cmbServiceType.Enabled = true;
                cmbServiceAdvisor.Enabled = true;
                CmbAplus.Enabled = true;
                cmbDeliveryStatus.Enabled = true;
                txtRegNo.Enabled = true;
            }

            else if (Int32.Parse(CmbRptType.SelectedValue.ToString()) == 38)
            {
                MultiView1.ActiveViewIndex = 0;
                cmbYearfrom.Enabled = true;
                cmbMnthfrom.Enabled = true;
                cmbDayFrm.Enabled = true;
                CmbYearto.Enabled = true;
                cmbDayTo.Enabled = true;
                cmbMnthTo.Enabled = true;
                cmbTeamLead.Enabled = true;
                cmbServiceType.Enabled = true;
                cmbServiceAdvisor.Enabled = true;
                CmbAplus.Enabled = true;
                cmbDeliveryStatus.Enabled = true;
                txtRegNo.Enabled = true;
                drpWhiteBoard.Enabled = true;
            }

            else if (Int32.Parse(CmbRptType.SelectedValue.ToString()) != 1) //ALL FALSE
            {
                cmbYearfrom.Enabled = false;
                cmbMnthfrom.Enabled = false;
                cmbDayFrm.Enabled = false;
                CmbYearto.Enabled = false;
                cmbDayTo.Enabled = false;
                cmbMnthTo.Enabled = false;
                cmbServiceAdvisor.Enabled = false;
                CmbAplus.Enabled = false;
                cmbServiceType.Enabled = false;
                cmbTeamLead.Enabled = true;
                cmbDeliveryStatus.Enabled = false;
                txtRegNo.Enabled = false;
                MultiView1.ActiveViewIndex = -1;
                drpWhiteBoard.Enabled = false;
            }
            else
            {
                MultiView1.ActiveViewIndex = 0;
                cmbYearfrom.Enabled = true;
                cmbMnthfrom.Enabled = true;
                cmbDayFrm.Enabled = true;
                CmbYearto.Enabled = true;
                cmbDayTo.Enabled = true;
                cmbMnthTo.Enabled = true;
                cmbServiceAdvisor.Enabled = true;
                CmbAplus.Enabled = true;
                cmbServiceType.Enabled = true;
                cmbTeamLead.Enabled = true;
                cmbDeliveryStatus.Enabled = true;
                txtRegNo.Enabled = true;
                drpWhiteBoard.Enabled = false;
            }
            if (Int32.Parse(CmbRptType.SelectedValue.ToString()) == 77)
            {
                MultiView1.ActiveViewIndex = 0;
                cmbYearfrom.Enabled = true;
                cmbMnthfrom.Enabled = true;
                cmbDayFrm.Enabled = true;
                CmbYearto.Enabled = false;
                cmbDayTo.Enabled = false;
                cmbMnthTo.Enabled = false;
                cmbTeamLead.Enabled = false;
                cmbServiceType.Enabled = false;
                cmbServiceAdvisor.Enabled = false;
                CmbAplus.Enabled = false;
                cmbDeliveryStatus.Enabled = false;
                txtRegNo.Enabled = false;
                drpWhiteBoard.Enabled = false;
            }

            if (Int32.Parse(CmbRptType.SelectedValue.ToString()) == 23)
            {
                MultiView1.ActiveViewIndex = 2;
            }
            if (Int32.Parse(CmbRptType.SelectedValue.ToString()) == 27) //FROM AND TO DATE WITH SERVICE ADVISOR
            {
                MultiView1.ActiveViewIndex = 0;
                cmbYearfrom.Enabled = true;
                cmbMnthfrom.Enabled = true;
                cmbDayFrm.Enabled = true;
                CmbYearto.Enabled = true;
                cmbDayTo.Enabled = true;
                cmbMnthTo.Enabled = true;
                cmbTeamLead.Enabled = true;
                cmbServiceAdvisor.Enabled = true;
                CmbAplus.Enabled = false;
                cmbServiceType.Enabled = false;
                cmbDeliveryStatus.Enabled = false;
                txtRegNo.Enabled = false;
            }
            if (Int32.Parse(CmbRptType.SelectedValue.ToString()) == 18) // ALL TRUE
            {
                MultiView1.ActiveViewIndex = 0;
                cmbYearfrom.Enabled = true;
                cmbMnthfrom.Enabled = true;
                cmbDayFrm.Enabled = true;
                CmbYearto.Enabled = false;
                cmbDayTo.Enabled = false;
                cmbMnthTo.Enabled = false;
                cmbTeamLead.Enabled = true;
                cmbEmpType.Enabled = true;
                cmbServiceAdvisor.Enabled = false;
                CmbAplus.Enabled = false;
                cmbServiceType.Enabled = false;
                cmbDeliveryStatus.Enabled = false;
                txtRegNo.Enabled = false;
                drpWhiteBoard.Enabled = true;
            }

            if ((Int32.Parse(CmbRptType.SelectedValue.ToString()) >= 29 && Int32.Parse(CmbRptType.SelectedValue.ToString()) <= 31)) // FROM AND TO DATE
            {
                MultiView1.ActiveViewIndex = 0;
                cmbYearfrom.Enabled = true;
                cmbMnthfrom.Enabled = true;
                cmbDayFrm.Enabled = true;
                CmbYearto.Enabled = true;
                cmbDayTo.Enabled = true;
                cmbMnthTo.Enabled = true;
                CmbAplus.Enabled = false;
                txtRegNo.Enabled = false;
                cmbDeliveryStatus.Enabled = false;
                cmbServiceType.Enabled = false;
                cmbEmpType.Enabled = false;
                cmbTeamLead.Enabled = true;
                cmbServiceAdvisor.Enabled = false;
            }
            else if (Int32.Parse(CmbRptType.SelectedValue.ToString()) == 32)
            {
                MultiView1.ActiveViewIndex = 2;
                return;
            }
            else if (Int32.Parse(CmbRptType.SelectedValue.ToString()) == 38 || Int32.Parse(CmbRptType.SelectedValue.ToString()) == 45)
            {
                MultiView1.ActiveViewIndex = 0;
                cmbYearfrom.Enabled = true;
                cmbMnthfrom.Enabled = true;
                cmbDayFrm.Enabled = true;
                CmbYearto.Enabled = false;
                cmbDayTo.Enabled = false;
                cmbMnthTo.Enabled = false;
                cmbTeamLead.Enabled = true;
                cmbServiceType.Enabled = false;
                cmbServiceAdvisor.Enabled = true;
                CmbAplus.Enabled = true;
                cmbDeliveryStatus.Enabled = true;
                txtRegNo.Enabled = true;
            }
            else if (Int32.Parse(CmbRptType.SelectedValue.ToString()) == 54) // FROM DATE
            {
                MultiView1.ActiveViewIndex = 0;
                cmbYearfrom.Enabled = true;
                cmbMnthfrom.Enabled = true;
                cmbDayFrm.Enabled = true;
                CmbYearto.Enabled = false;
                cmbDayTo.Enabled = false;
                cmbMnthTo.Enabled = false;
                cmbServiceType.Enabled = false;
                cmbServiceAdvisor.Enabled = false;
                CmbAplus.Enabled = false;
                cmbTeamLead.Enabled = true;
                cmbDeliveryStatus.Enabled = false;
                txtRegNo.Enabled = false;

                return;
            }
            else if (Int32.Parse(CmbRptType.SelectedValue.ToString()) == 57) // FROM DATE
            {
                MultiView1.ActiveViewIndex = 0;
                cmbYearfrom.Enabled = true;
                cmbMnthfrom.Enabled = true;
                cmbDayFrm.Enabled = true;
                CmbYearto.Enabled = false;
                cmbDayTo.Enabled = false;
                cmbMnthTo.Enabled = false;
                cmbTeamLead.Enabled = true;
                cmbServiceType.Enabled = false;
                cmbServiceAdvisor.Enabled = true;
                CmbAplus.Enabled = false;
                cmbDeliveryStatus.Enabled = false;
                txtRegNo.Enabled = false;
                drpWhiteBoard.Enabled = true;
                return;
            }
            else if (Int32.Parse(CmbRptType.SelectedValue.ToString()) == 33 || Int32.Parse(CmbRptType.SelectedValue.ToString()) == 68) // FROM DATE
            {
                MultiView1.ActiveViewIndex = 0;
                cmbYearfrom.Enabled = true;
                cmbMnthfrom.Enabled = true;
                cmbDayFrm.Enabled = true;
                CmbYearto.Enabled = false;
                cmbDayTo.Enabled = false;
                cmbMnthTo.Enabled = false;
                cmbServiceType.Enabled = false;
                cmbServiceAdvisor.Enabled = false;
                CmbAplus.Enabled = false;
                cmbDeliveryStatus.Enabled = false;
                txtRegNo.Enabled = false;
                cmbTeamLead.Enabled = true;
                return;
            }
            else if (Int32.Parse(CmbRptType.SelectedValue.ToString()) == 34) // FROM DATE
            {
                MultiView1.ActiveViewIndex = 0;
                cmbYearfrom.Enabled = true;
                cmbMnthfrom.Enabled = true;
                cmbDayFrm.Enabled = true;
                CmbYearto.Enabled = false;
                cmbDayTo.Enabled = false;
                cmbMnthTo.Enabled = false;
                cmbTeamLead.Enabled = true;
                cmbServiceType.Enabled = false;
                cmbServiceAdvisor.Enabled = true;
                CmbAplus.Enabled = false;
                cmbDeliveryStatus.Enabled = false;
                txtRegNo.Enabled = false;
                return;
            }
            else if (Int32.Parse(CmbRptType.SelectedValue.ToString()) == 37 || Int32.Parse(CmbRptType.SelectedValue.ToString()) == 44 || Int32.Parse(CmbRptType.SelectedValue.ToString()) == 45) // FROM DATE
            {
                MultiView1.ActiveViewIndex = 0;
                cmbYearfrom.Enabled = true;
                cmbMnthfrom.Enabled = true;
                cmbDayFrm.Enabled = true;
                CmbYearto.Enabled = false;
                cmbDayTo.Enabled = false;
                cmbMnthTo.Enabled = false;
                cmbTeamLead.Enabled = true;
                cmbServiceType.Enabled = false;
                cmbServiceAdvisor.Enabled = false;
                CmbAplus.Enabled = false;
                cmbDeliveryStatus.Enabled = false;
                txtRegNo.Enabled = false;
                return;
            }
            else if (Int32.Parse(CmbRptType.SelectedValue.ToString()) == 48)
            {
                MultiView1.ActiveViewIndex = 0;
                cmbYearfrom.Enabled = true;
                cmbMnthfrom.Enabled = true;
                cmbDayFrm.Enabled = true;
                CmbYearto.Enabled = false;
                cmbDayTo.Enabled = false;
                cmbMnthTo.Enabled = false;
                cmbTeamLead.Enabled = true;
                cmbServiceType.Enabled = false;
                cmbServiceAdvisor.Enabled = true;
                CmbAplus.Enabled = false;
                cmbDeliveryStatus.Enabled = false;
                txtRegNo.Enabled = false;
                return;
            }
            else if (Int32.Parse(CmbRptType.SelectedValue.ToString()) == 49)
            {
                MultiView1.ActiveViewIndex = 0;
                cmbYearfrom.Enabled = true;
                cmbMnthfrom.Enabled = true;
                cmbDayFrm.Enabled = true;
                CmbYearto.Enabled = false;
                cmbDayTo.Enabled = false;
                cmbMnthTo.Enabled = false;
                cmbTeamLead.Enabled = true;
                cmbServiceType.Enabled = false;
                cmbServiceAdvisor.Enabled = true;
                CmbAplus.Enabled = false;
                cmbDeliveryStatus.Enabled = false;
                txtRegNo.Enabled = false;
                drpWhiteBoard.Enabled = true;
                return;
            }
            else if (Int32.Parse(CmbRptType.SelectedValue.ToString()) == 65)
            {
                MultiView1.ActiveViewIndex = 0;
                cmbYearfrom.Enabled = true;
                cmbMnthfrom.Enabled = true;
                cmbDayFrm.Enabled = true;
                CmbYearto.Enabled = false;
                cmbDayTo.Enabled = false;
                cmbMnthTo.Enabled = false;
                cmbTeamLead.Enabled = true;
                cmbServiceType.Enabled = false;
                cmbServiceAdvisor.Enabled = true;
                CmbAplus.Enabled = false;
                cmbDeliveryStatus.Enabled = false;
                txtRegNo.Enabled = false;
                drpWhiteBoard.Enabled = true;
                return;
            }
            else if (Int32.Parse(CmbRptType.SelectedValue.ToString()) == 50)
            {
                MultiView1.ActiveViewIndex = 0;
                cmbYearfrom.Enabled = true;
                cmbMnthfrom.Enabled = true;
                cmbDayFrm.Enabled = true;
                CmbYearto.Enabled = false;
                cmbDayTo.Enabled = true;
                cmbMnthTo.Enabled = false;
                cmbTeamLead.Enabled = true;
                cmbServiceType.Enabled = true;
                cmbServiceAdvisor.Enabled = true;
                CmbAplus.Enabled = false;
                cmbDeliveryStatus.Enabled = false;
                txtRegNo.Enabled = false;
                return;
            }
            else if (Int32.Parse(CmbRptType.SelectedValue.ToString()) == 51 || Int32.Parse(CmbRptType.SelectedValue.ToString()) == 53 || Int32.Parse(CmbRptType.SelectedValue.ToString()) == 58)
            {
                MultiView1.ActiveViewIndex = 0;
                cmbYearfrom.Enabled = true;
                cmbMnthfrom.Enabled = true;
                cmbDayFrm.Enabled = true;
                CmbYearto.Enabled = false;
                cmbDayTo.Enabled = true;
                cmbMnthTo.Enabled = false;
                cmbTeamLead.Enabled = true;
                cmbServiceType.Enabled = false;
                cmbServiceAdvisor.Enabled = true;
                CmbAplus.Enabled = false;
                cmbDeliveryStatus.Enabled = false;
                txtRegNo.Enabled = false;
                return;
            }
            else if (Int32.Parse(CmbRptType.SelectedValue.ToString()) == 52)
            {
                MultiView1.ActiveViewIndex = 0;
                cmbYearfrom.Enabled = false;
                cmbMnthfrom.Enabled = false;
                cmbDayFrm.Enabled = false;
                CmbYearto.Enabled = false;
                cmbDayTo.Enabled = false;
                cmbMnthTo.Enabled = false;
                cmbTeamLead.Enabled = true;
                cmbServiceType.Enabled = false;
                cmbServiceAdvisor.Enabled = false;
                CmbAplus.Enabled = false;
                cmbDeliveryStatus.Enabled = false;
                txtRegNo.Enabled = false;
                return;
            }
            else if (Int32.Parse(CmbRptType.SelectedValue.ToString()) == 55)
            {
                MultiView1.ActiveViewIndex = 0;
                cmbDayFrm.SelectedValue = "1";
                cmbYearfrom.Enabled = true;
                cmbMnthfrom.Enabled = true;
                cmbDayFrm.Enabled = false;
                CmbYearto.Enabled = false;
                cmbDayTo.Enabled = true;
                cmbMnthTo.Enabled = false;
                cmbTeamLead.Enabled = true;
                cmbServiceType.Enabled = false;
                cmbServiceAdvisor.Enabled = true;
                CmbAplus.Enabled = false;
                cmbDeliveryStatus.Enabled = false;
                txtRegNo.Enabled = false;

                return;
            }
            else if (Int32.Parse(CmbRptType.SelectedValue.ToString()) == 47)
            {
                MultiView1.ActiveViewIndex = 0;
                cmbDayFrm.SelectedValue = "1";
                cmbYearfrom.Enabled = true;
                cmbMnthfrom.Enabled = true;
                cmbDayFrm.Enabled = false;
                CmbYearto.Enabled = false;
                cmbDayTo.Enabled = true;
                cmbMnthTo.Enabled = false;
                cmbTeamLead.Enabled = false;
                cmbServiceType.Enabled = false;
                cmbServiceAdvisor.Enabled = false;
                CmbAplus.Enabled = false;
                cmbDeliveryStatus.Enabled = false;
                txtRegNo.Enabled = false;
                return;
            }
            else if (Int32.Parse(CmbRptType.SelectedValue.ToString()) == 66)
            {
                MultiView1.ActiveViewIndex = 0;
                cmbDayFrm.SelectedValue = "1";
                cmbYearfrom.Enabled = true;
                cmbMnthfrom.Enabled = true;
                cmbDayFrm.Enabled = true;
                CmbYearto.Enabled = false;
                cmbDayTo.Enabled = false;
                cmbMnthTo.Enabled = false;
                cmbTeamLead.Enabled = false;
                cmbServiceType.Enabled = false;
                cmbServiceAdvisor.Enabled = false;
                CmbAplus.Enabled = false;
                cmbDeliveryStatus.Enabled = false;
                txtRegNo.Enabled = false;
                drpWhiteBoard.Enabled = true;
                return;
            }
            else if (Int32.Parse(CmbRptType.SelectedValue.ToString()) == 67)
            {
                MultiView1.ActiveViewIndex = 0;
                cmbDayFrm.SelectedValue = "1";
                cmbYearfrom.Enabled = true;
                cmbMnthfrom.Enabled = true;
                cmbDayFrm.Enabled = true;
                CmbYearto.Enabled = false;
                cmbDayTo.Enabled = false;
                cmbMnthTo.Enabled = false;
                cmbTeamLead.Enabled = false;
                cmbServiceType.Enabled = false;
                cmbServiceAdvisor.Enabled = false;
                CmbAplus.Enabled = false;
                cmbDeliveryStatus.Enabled = false;
                txtRegNo.Enabled = false;
                return;
            }
            else if (Int32.Parse(CmbRptType.SelectedValue.ToString()) == 43)
            {
                MultiView1.ActiveViewIndex = 0;
                cmbDayFrm.SelectedValue = "1";
                cmbYearfrom.Enabled = true;
                cmbMnthfrom.Enabled = true;
                cmbDayFrm.Enabled = false;
                CmbYearto.Enabled = false;
                cmbDayTo.Enabled = true;
                cmbMnthTo.Enabled = false;
                cmbTeamLead.Enabled = true;
                cmbServiceType.Enabled = false;
                cmbServiceAdvisor.Enabled = true;
                CmbAplus.Enabled = false;
                cmbDeliveryStatus.Enabled = false;
                txtRegNo.Enabled = false;
                return;
            }
            else if (Int32.Parse(CmbRptType.SelectedValue.ToString()) == 3 || Int32.Parse(CmbRptType.SelectedValue.ToString()) == 4 || Int32.Parse(CmbRptType.SelectedValue.ToString()) == 56)
            {
                MultiView1.ActiveViewIndex = 0;
                cmbDayFrm.SelectedValue = "1";
                cmbYearfrom.Enabled = true;
                cmbMnthfrom.Enabled = true;
                cmbDayFrm.Enabled = false;
                CmbYearto.Enabled = false;
                cmbDayTo.Enabled = true;
                cmbMnthTo.Enabled = false;
                cmbTeamLead.Enabled = true;
                cmbServiceType.Enabled = false;
                cmbServiceAdvisor.Enabled = true;
                CmbAplus.Enabled = false;
                cmbDeliveryStatus.Enabled = false;
                txtRegNo.Enabled = false;
                return;
            }

            else if (Int32.Parse(CmbRptType.SelectedValue.ToString()) == 2 || (Int32.Parse(CmbRptType.SelectedValue.ToString()) == 39 || Int32.Parse(CmbRptType.SelectedValue.ToString()) >= 41 && Int32.Parse(CmbRptType.SelectedValue.ToString()) == 42) || Int32.Parse(CmbRptType.SelectedValue.ToString()) == 36 || Int32.Parse(CmbRptType.SelectedValue.ToString()) == 28 || Int32.Parse(CmbRptType.SelectedValue.ToString()) == 46 || Int32.Parse(CmbRptType.SelectedValue.ToString()) == 47)
            {
                MultiView1.ActiveViewIndex = 0;
                cmbDayFrm.SelectedValue = "1";
                cmbYearfrom.Enabled = true;
                cmbMnthfrom.Enabled = true;
                cmbDayFrm.Enabled = false;
                CmbYearto.Enabled = false;
                cmbDayTo.Enabled = true;
                cmbMnthTo.Enabled = false;
                cmbTeamLead.Enabled = true;
                cmbServiceType.Enabled = false;
                cmbServiceAdvisor.Enabled = false;
                CmbAplus.Enabled = false;
                cmbDeliveryStatus.Enabled = false;
                txtRegNo.Enabled = false;

                return;
            }
            else if (Int32.Parse(CmbRptType.SelectedValue.ToString()) == 20)
            {
                MultiView1.ActiveViewIndex = 0;
                cmbYearfrom.Enabled = true;
                cmbMnthfrom.Enabled = true;
                cmbDayFrm.Enabled = true;
                CmbYearto.Enabled = false;
                cmbDayTo.Enabled = false;
                cmbMnthTo.Enabled = false;
                cmbTeamLead.Enabled = true;
                cmbServiceType.Enabled = false;
                cmbServiceAdvisor.Enabled = true;
                CmbAplus.Enabled = false;
                cmbDeliveryStatus.Enabled = false;
                txtRegNo.Enabled = false;
                return;
            }
            else if (Int32.Parse(CmbRptType.SelectedValue.ToString()) == 69)
            {
                MultiView1.ActiveViewIndex = 0;
                cmbYearfrom.Enabled = true;
                cmbMnthfrom.Enabled = true;
                cmbDayFrm.Enabled = true;
                CmbYearto.Enabled = false;
                cmbDayTo.Enabled = false;
                cmbMnthTo.Enabled = false;
                cmbTeamLead.Enabled = true;
                cmbServiceType.Enabled = false;
                cmbServiceAdvisor.Enabled = true;
                CmbAplus.Enabled = false;
                cmbDeliveryStatus.Enabled = false;
                txtRegNo.Enabled = false;
                drpWhiteBoard.Enabled = true;
                return;
            }

            else if ((Int32.Parse(CmbRptType.SelectedValue.ToString()) == 22))
            {
                MultiView1.ActiveViewIndex = 0;
                cmbYearfrom.Enabled = true;
                cmbMnthfrom.Enabled = true;
                cmbDayFrm.Enabled = false;
                CmbYearto.Enabled = false;
                cmbDayTo.Enabled = true;
                cmbDayFrm.SelectedIndex = 0;
                cmbMnthTo.Enabled = false;
                cmbTeamLead.Enabled = true;
                cmbServiceType.Enabled = false;
                cmbServiceAdvisor.Enabled = true;
                CmbAplus.Enabled = false;
                cmbDeliveryStatus.Enabled = false;
                txtRegNo.Enabled = false;
                return;
            }
            else if ((Int32.Parse(CmbRptType.SelectedValue.ToString()) >= 41 && Int32.Parse(CmbRptType.SelectedValue.ToString()) < 42))
            {
                MultiView1.ActiveViewIndex = 0;
                cmbYearfrom.Enabled = true;
                cmbMnthfrom.Enabled = true;
                cmbDayFrm.Enabled = true;
                CmbYearto.Enabled = false;
                cmbDayTo.Enabled = true;
                cmbMnthTo.Enabled = false;
                cmbTeamLead.Enabled = true;
                cmbServiceType.Enabled = false;
                cmbServiceAdvisor.Enabled = true;
                CmbAplus.Enabled = false;
                cmbDeliveryStatus.Enabled = false;
                txtRegNo.Enabled = false;
                return;
            }
            else if (Int32.Parse(CmbRptType.SelectedValue.ToString()) == 12)
            {
                MultiView1.ActiveViewIndex = 0;
                cmbYearfrom.Enabled = true;
                cmbMnthfrom.Enabled = true;
                cmbDayFrm.Enabled = true;
                CmbYearto.Enabled = false;
                cmbDayTo.Enabled = false;
                cmbMnthTo.Enabled = false;
                cmbTeamLead.Enabled = true;
                cmbServiceType.Enabled = false;
                cmbServiceAdvisor.Enabled = true;
                CmbAplus.Enabled = false;
                cmbDeliveryStatus.Enabled = false;
                txtRegNo.Enabled = false;
                drpWhiteBoard.Enabled = true;
                return;
            }
            else if ((Int32.Parse(CmbRptType.SelectedValue.ToString()) == 70 || Int32.Parse(CmbRptType.SelectedValue.ToString()) == 71 || Int32.Parse(CmbRptType.SelectedValue.ToString()) == 72 || Int32.Parse(CmbRptType.SelectedValue.ToString()) == 73 || Int32.Parse(CmbRptType.SelectedValue.ToString()) == 74 || Int32.Parse(CmbRptType.SelectedValue.ToString()) == 75))
            {
                MultiView1.ActiveViewIndex = 0;
                cmbYearfrom.Enabled = true;
                cmbMnthfrom.Enabled = true;
                cmbDayFrm.Enabled = false;
                CmbYearto.Enabled = false;
                cmbDayTo.Enabled = true;
                cmbDayFrm.SelectedIndex = 0;
                cmbMnthTo.Enabled = false;
                cmbTeamLead.Enabled = false;
                cmbServiceType.Enabled = false;
                cmbServiceAdvisor.Enabled = false;
                CmbAplus.Enabled = false;
                cmbDeliveryStatus.Enabled = false;
                txtRegNo.Enabled = false;
                return;
            }

            else if (Int32.Parse(CmbRptType.SelectedValue.ToString()) == 78)
            {
                MultiView1.ActiveViewIndex = 0;
                cmbYearfrom.Enabled = false;
                cmbMnthfrom.Enabled = true;
                cmbDayFrm.Enabled = true;
                CmbYearto.Enabled = false;
                cmbDayTo.Enabled = true;
                cmbDayFrm.SelectedIndex = 0;
                cmbMnthTo.Enabled = false;
                cmbTeamLead.Enabled = false;
                cmbServiceType.Enabled = false;
                cmbServiceAdvisor.Enabled = false;
                CmbAplus.Enabled = false;
                cmbDeliveryStatus.Enabled = false;
                txtRegNo.Enabled = false;
                return;
            }

            else if (Int32.Parse(CmbRptType.SelectedValue.ToString()) == 79)
            {
                MultiView1.ActiveViewIndex = 0;
                cmbYearfrom.Enabled = false;
                cmbMnthfrom.Enabled = true;
                cmbDayFrm.Enabled = true;
                CmbYearto.Enabled = false;
                cmbDayTo.Enabled = true;
                cmbDayFrm.SelectedIndex = 0;
                cmbMnthTo.Enabled = false;
                cmbTeamLead.Enabled = false;
                cmbServiceType.Enabled = false;
                cmbServiceAdvisor.Enabled = false;
                CmbAplus.Enabled = false;
                cmbDeliveryStatus.Enabled = false;
                txtRegNo.Enabled = false;
                return;
            }
        }
    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {
        lblStstus.Text = "";
    }

    private string getGroupName()
    {
        // string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("udpGetGroupName", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@GroupId", Request.QueryString[0].ToString());
        oConn.Open();
        string GroupName = (string)cmd.ExecuteScalar();
        oConn.Close();
        return GroupName;
    }

    private ArrayList getFileList()
    {
        // string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("udpGetReportList", oConn);
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@GroupMailId", Request.QueryString[0].ToString());
        oConn.Open();
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        sda.Fill(oDt);
        ArrayList filelist = new ArrayList();
        for (int i = 0; i < oDt.Rows.Count; i++)
        {
            filelist.Add((string)oDt.Rows[i][2]);
        }
        return filelist;
    }

    private void WriteToExcelFile(byte[] ExcelBytes, string ReportName)
    {
        String path = @"" + ConfigurationManager.AppSettings["MailDirectory"].ToString() + @"\" + System.DateTime.Now.ToString("dd_MM_yyyy");
        string CurrentDirectory = Directory.GetCurrentDirectory();
        Directory.SetCurrentDirectory(path);
        if (Directory.Exists(path))
        {
            string groupname = getGroupName();
            if (!Directory.Exists(@"" + path + "\\" + groupname))
            {
                Directory.CreateDirectory(@"" + path + "\\" + groupname);
                Directory.SetCurrentDirectory(@"" + path + "\\" + groupname);
            }
            else
            {
                Directory.SetCurrentDirectory(@"" + path + "\\" + groupname);
            }
        }
        else
        {
            Directory.CreateDirectory(path);
        }
        ReportName = ReportName.Replace("ProTrackDataSet_", "");
        string fileName = ReportName + ".Xls";
        FileStream fileStream = null;
        try
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            fileStream = new FileStream(fileName, FileMode.Create);
            // Write the data to the file, byte by byte.
            for (int i = 0; i < ExcelBytes.Length; i++)
            {
                fileStream.WriteByte(ExcelBytes[i]);
            }
            // Set the stream position to the beginning of the file.
            fileStream.Seek(0, SeekOrigin.Begin);
            // Read and verify the data.
            for (int i = 0; i < ExcelBytes.Length; i++)
            {
                if (ExcelBytes[i] != fileStream.ReadByte())
                {
                    return;
                }
            }
        }
        catch (Exception ex)
        {
        }
        finally
        {
            if (fileStream != null)
            {
                fileStream.Close();
            }
            Directory.SetCurrentDirectory(CurrentDirectory);
            try
            {
                System.Diagnostics.Process[] processlist = System.Diagnostics.Process.GetProcesses();

                foreach (System.Diagnostics.Process theprocess in processlist)
                {
                    if (theprocess.ProcessName == "EXCEL")
                    {
                        // Console.WriteLine(theprocess.Id.ToString() + " " + theprocess.MainModule.FileName + " " + theprocess.MainWindowTitle + " " + theprocess.ProcessName);
                        theprocess.Kill();
                        theprocess.Dispose();
                    }
                }
            }
            catch { }
        }
    }

    protected void btn_logout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();
        Response.Redirect("login.aspx");
    }

    protected void btn_Back_Click(object sender, EventArgs e)
    {
        Session["CURRENT_PAGE"] = null;
        Response.Redirect("AHome.aspx");
    }

    protected void cmbYearfrom_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            cmbMnthTo.SelectedValue = cmbMnthfrom.SelectedValue;
            CmbYearto.SelectedValue = cmbYearfrom.SelectedValue;
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Please Select Report Type!";
            CmbRptType.SelectedIndex = -1;
        }
    }

    protected void cmbDayFrm_SelectedIndexChanged(object sender, EventArgs e)
    {
        cmbDayTo.SelectedIndex = cmbDayFrm.SelectedIndex;
        cmbMnthTo.SelectedIndex = cmbMnthfrom.SelectedIndex;
        CmbYearto.SelectedIndex = cmbYearfrom.SelectedIndex;
    }
}