using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class ReportsPageForSMDashBoards : System.Web.UI.Page
{
    private string floorname = "", TLId = "", IsBodyshop = "0";
    private string selectType = "";
    private DataTable DtRpt;
    private string RegNo;
    private string DeliveryStatus = "";
    private string ServiceAdvisor = "";
    private bool AutoGen = true;
    private string Floor = "";
    private DateTime dtfrm, dtTo;
    private string TLID = "";
    private string ServiceType = "";
    private string EmpType = "";
    private string WorksManager = "";
    private string Aplus = "";
    private string VehicleStatus ="2";
    private int month = DateTime.Now.Month;
    private int year = System.DateTime.Now.Year;
    private static string Copyright;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Copyright = DataManager.getVersion();
            GenerateAutoreportTotalNoOfVehiclesInFlow();
        }
        catch (Exception ex)
        {
        }
    }

    private void GenerateAutoreportTotalNoOfVehiclesInFlow()
    {
        try
        {
            SqlConnection con = new SqlConnection(DataManager.ConStr);
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT SlNo, ReportName, Rept_Procedure, Rept_Rdlc, FileName FROM tbl_ReportMenu WHERE (Activ = 'YES') AND Autogen = 1 And SlNo=" + Session["SlNo"].ToString() + " ORDER BY Ordr", con);
            sda.Fill(dt);
            GenerateSelectedReport(dt.Rows[0]["SlNo"].ToString(), dt.Rows[0]["Rept_Rdlc"].ToString(), dt.Rows[0]["Rept_Procedure"].ToString(), dt.Rows[0]["FileName"].ToString());
        }
        catch (Exception ex)
        {
        }
    }

    private void GenerateSelectedReport(string ReportName, string rept_rdlc, string rept_proc, string file_name)
    {
        try
        {
            AutoGen = true;
            RptViewer.Reset();
            if (AutoGen == true)
            {
                dtfrm = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                dtTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
            }

            string daymnthyr = DateTime.Now.Month + "/" + DateTime.Now.Day + "/" + DateTime.Now.Year;
            RegNo = "";
            if (rept_proc.Length > 16)
            {
                switch (ReportName)
                {
                    case "4":
                        DtRpt = GetPDTADTReportDataTable(rept_proc.Substring(16));
                        FillAdditionalReport(DtRpt, rept_rdlc, rept_proc, file_name);
                        break;

                    case "7":
                        DtRpt = new DataTable();
                        DtRpt = GetEmployeeattndnc(daymnthyr, rept_proc.Substring(16));
                        FillTrendsReport(DtRpt, rept_rdlc, rept_proc, file_name, "All", daymnthyr);
                        break;

                    case "12":
                        //DtRpt = new DataTable();
                        //DtRpt = GetSAFilterReportDataTable(dtfrm, dtTo, ServiceAdvisor, rept_proc.Substring(16));
                        //FillAdditionalReport(DtRpt, rept_rdlc, rept_proc, file_name);
                         FillCarryForwardVehReport(rept_rdlc, rept_proc, file_name, ServiceAdvisor);
                        break;
                       
                    case "13":
                        DtRpt = new DataTable();
                        DtRpt = getCardNotScanned(dtfrm, dtTo, rept_proc.Substring(16));
                        FillAdditionalReport(DtRpt, rept_rdlc, rept_proc, file_name);
                        break;

                    case "20":
                        FillSameDayReport(rept_rdlc, rept_proc, file_name, ServiceAdvisor);
                        break;

                    case "22": DtRpt = new DataTable();
                        DtRpt = GetSAFilterReportDataTable(dtfrm, dtTo, ServiceAdvisor, rept_proc.Substring(16));
                        FillTrendsReport1(DtRpt, rept_rdlc, rept_proc, file_name);
                        break;

                    case "33"://Technician Time Monitoring Report 1 Tech
                        DtRpt = new DataTable();
                        DtRpt = GetTechTMR1TechTMRNEW(rept_proc.Substring(16), dtfrm.ToShortDateString());
                        FillTechTMRReportTMRNEW(DtRpt, rept_rdlc, rept_proc, file_name);
                        break;

                    case "38": // TMR NEW
                        DtRpt = new DataTable();
                        DtRpt = GetReportDataTable(dtfrm, dtTo, ServiceAdvisor, Aplus, WorksManager, RegNo, DeliveryStatus, ServiceType, Floor, TLID, selectType, rept_proc.Substring(16));
                        FillReport(DtRpt, rept_rdlc, rept_proc, file_name);
                        break;

                    case "48": DtRpt = new DataTable();
                        DtRpt = GetVehicleInOutMonthlyTrend(rept_proc.Substring(16), dtfrm.ToShortDateString(), ServiceAdvisor);
                        FillAdditional1ParaReport(DtRpt, rept_rdlc, rept_proc, file_name, ServiceAdvisor);
                        break;

                    case "49": DtRpt = new DataTable();
                        DtRpt = GetStageWiseProductivityDataTable(rept_proc.Substring(16), dtfrm.ToShortDateString(), ServiceAdvisor);
                        FillSWPReport(DtRpt, rept_rdlc, rept_proc, file_name, ServiceAdvisor);
                        break;

                    case "54"://Bay TMR
                        DtRpt = new DataTable();
                        DtRpt = GetTechTMR1TechNew(rept_proc.Substring(16), dtfrm.ToShortDateString());
                        FillTechTMRReportNew(DtRpt, rept_rdlc, rept_proc, file_name);
                        break;

                    case "55": // PROCESS & IDLE TIME ANALYSIS WRT ST
                        DtRpt = new DataTable();
                        DtRpt = GetReportDataTable(rept_proc.Substring(16));
                        FillAdditionalReport(DtRpt, rept_rdlc, rept_proc, file_name);
                        break;

                    case "57"://DELIVERY ANALYSIS REPORT SA WISE
                        DtRpt = new DataTable();
                        DtRpt = GetTechTMR1Tech(rept_proc.Substring(16), dtfrm.ToShortDateString());
                        FillTechTMRReport(DtRpt, rept_rdlc, rept_proc, file_name);
                        break;
                    case "65":
                        DtRpt = GetSAPerformanceDataTable(rept_proc.Substring(16), dtfrm.ToShortDateString(), ServiceAdvisor,VehicleStatus);
                        FillAdditional1ParaReport(DtRpt, rept_rdlc, rept_proc, file_name, ServiceAdvisor);
                        break;
                    case "69":
                        FillCardScanAdherenceReport(rept_rdlc, rept_proc, file_name, ServiceAdvisor);
                        break;
                    case "18":
                        DtRpt = DataManager.GetEmployeeReportDataTable(dtfrm, dtTo, EmpType, "", TLId, "0", "EmployeeTimeMonitoring");
                        FillETMReport(DtRpt, rept_rdlc, rept_proc, file_name);
                        break;
                }
            }
            else
            {
                return;
            }
        }
        catch (Exception ex)
        {
            RptViewer.Reset();
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
            //MyAccordion.RequireOpenedPane = false;
            //MyAccordion.SelectedIndex = -1;
            if (EmpType.ToString().Trim() == "")
            {
                EmpType = "ALL";
            }
            Microsoft.Reporting.WebForms.ReportParameter rParam1 = new Microsoft.Reporting.WebForms.ReportParameter("Datefrom", dtfrm.ToString("dd-MM-yyyy"));
            Microsoft.Reporting.WebForms.ReportParameter rParam2 = new Microsoft.Reporting.WebForms.ReportParameter("Dateto", dtTo.ToString("dd-MM-yyyy"));
            Microsoft.Reporting.WebForms.ReportParameter rParam3 = new Microsoft.Reporting.WebForms.ReportParameter("EmpType", EmpType.ToString());
            Microsoft.Reporting.WebForms.ReportParameter rParam5 = new Microsoft.Reporting.WebForms.ReportParameter("Copyright", Copyright);
            RptViewer.Reset();                                                                        // reset the report viewer
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;             // Set to Local Reporting Mode
            RptViewer.ShowExportControls = true;                                                   // Show The Export Controls
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + ReportRdlcName;
            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            ReportDS.Name = ReportName;
            ReportDS.Value = Dt;
            localRpt.DisplayName = FileName;
            localRpt.DataSources.Clear();
            localRpt.DataSources.Add(ReportDS);
            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1, rParam2, rParam3, rParam5 });
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
            //if (AutoGen == true)
            //{
            //    WriteToExcelFile(exportBytes, FileName);
            //}
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error in processing the request.";
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
            Microsoft.Reporting.WebForms.ReportParameter rParam2 = new Microsoft.Reporting.WebForms.ReportParameter("SAName", "ALL");
            Microsoft.Reporting.WebForms.ReportParameter rParam4 = new Microsoft.Reporting.WebForms.ReportParameter("TLId", "ALL");
            Microsoft.Reporting.WebForms.ReportParameter rParam6 = new Microsoft.Reporting.WebForms.ReportParameter("Copyright", Copyright);
           

            localRpt.DataSources.Clear();
            for (int flag = 0; flag < Proc.Length; flag++)
            {
                localRpt.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ProTrackDataSet_" + Proc[flag].Trim(), GetCardScanAdherence(Proc[flag].Trim(), dtfrm.ToShortDateString(), ServiceAdvisor)));
            }
            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1, rParam2, rParam4, rParam6});
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);

            //if (AutoGen == true)
            //{
            //    WriteToExcelFile(exportBytes, FileName);
            //}
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error in processing the request. " + ex.Message;
            RptViewer.Reset();
            throw (ex);
        }
    }
    public DataTable GetCardScanAdherence(string StProc, string dDate, string ServiceAdvisor)
    {
        string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(sConnString);
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
        cmd.Parameters.AddWithValue("@VehicleStatus", 2);
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
            Microsoft.Reporting.WebForms.ReportParameter rParam2 = new Microsoft.Reporting.WebForms.ReportParameter("SAName", "ALL");
            Microsoft.Reporting.WebForms.ReportParameter rParam4 = new Microsoft.Reporting.WebForms.ReportParameter("TLId", "ALL");
            Microsoft.Reporting.WebForms.ReportParameter rParam6 = new Microsoft.Reporting.WebForms.ReportParameter("Copyright", Copyright);
            localRpt.DataSources.Clear();
            for (int flag = 0; flag < Proc.Length; flag++)
            {
                localRpt.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ProTrackDataSet_" + Proc[flag].Trim(), GetSAFilterReportDataTable(dtfrm, ServiceAdvisor, Proc[flag].Trim())));
            }
            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1, rParam2, rParam4, rParam6 });
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);

            //if (AutoGen == true)
            //{
            //    WriteToExcelFile(exportBytes, FileName);
            //}
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error in processing the request. " + ex.Message;
            RptViewer.Reset();
            throw (ex);
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
            dtf = new DateTime(DtFrom.Year, DtFrom.Month, 1, 0, 0, 0);
            //dtt = new DateTime(DtFrom.Year, DtFrom.Month, DtTo.Day, 23, 59, 59);
        }
        string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(sConnString);
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = StProc;
        if (StProc == "GetCarryForwardVehx")
        {
            cmd.Parameters.AddWithValue("@Datefrom", System.DateTime.Now.ToString("MM/dd/yyyy"));
            cmd.Parameters.AddWithValue("@Dateto", System.DateTime.Now.ToString("MM/dd/yyyy"));
        }
        else
            cmd.Parameters.AddWithValue("@Date", System.DateTime.Now.ToString("MM/dd/yyyy"));
        cmd.Parameters.AddWithValue("@SAName", "");
        cmd.Parameters.AddWithValue("@FloorName", "");
        cmd.Parameters.AddWithValue("@TLId", "");
        cmd.Parameters.AddWithValue("@IsBodyshop", 2);
        cmd.Parameters.AddWithValue("@VehicleStatus", 2);
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
    public DataTable GetSAPerformanceDataTable(string StProc, string dDate, string ServiceAdvisor, string VehicleStatus)
    {
        string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(sConnString);
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
    private void FillSameDayReport(string ReportRdlcName, string ReportName, string file_name, string ServiceAdvisor)
    {
        string mimeType;
        string encoding;
        string fileNameExtension;
        Microsoft.Reporting.WebForms.Warning[] warnings;
        string[] streamids;
        byte[] exportBytes = null;
        String[] Proc = ReportName.Substring(16).Replace("ProTrackDataSet_", "").Split(',');
        try
        {
            Microsoft.Reporting.WebForms.ReportParameter rParam1 = new Microsoft.Reporting.WebForms.ReportParameter("DateFrom", dtfrm.ToString("dd-MM-yyyy"));
            Microsoft.Reporting.WebForms.ReportParameter rParam4 = new Microsoft.Reporting.WebForms.ReportParameter("TLId", "ALL");
            Microsoft.Reporting.WebForms.ReportParameter rParam2;
            if (ServiceAdvisor.ToString().Trim() == "")
            {
                rParam2 = new Microsoft.Reporting.WebForms.ReportParameter("SAName", "ALL");
            }
            else
            {
                rParam2 = new Microsoft.Reporting.WebForms.ReportParameter("SAName", ServiceAdvisor);
            }
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
            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1, rParam2, rParam4, rParam6 });
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
        }
        catch (Exception ex)
        {
            lblStstus.Text = "Error in processing the request.";
            RptViewer.Reset();
            throw (ex);
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
        string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(sConnString);
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

    public DataTable GetReportDataTable(DateTime DtFrom, DateTime DtTo, string ServiceAdvisor, string ServiceType, string WorksManager, string regNo, string Deliverystatus, string ServiceTypes, string floorname, string teamleadid, string isbodyshop, string StProc)
    {
        string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
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

    public DataTable GetTechTMR1TechNew(string StProc, string dDate)
    {
        string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(sConnString);
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Date", dtfrm.ToString("MM/dd/yyyy"));
        //cmd.Parameters.AddWithValue("@FloorName", floorname);
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

    public DataTable GetTechTMR1TechTMRNEW(string StProc, string dDate)
    {
        string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(sConnString);
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

    public DataTable GetVehicleInOutMonthlyTrend(string StProc, string dDate, string ServiceAdvisor)
    {
        string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(sConnString);
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

    public DataTable GetTechTMR1Tech(string StProc, string dDate)
    {
        string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(sConnString);
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Date", dtfrm.ToString("MM/dd/yyyy"));
        cmd.Parameters.AddWithValue("@TLid", TLId);
        cmd.Parameters.AddWithValue("@SAName", ServiceAdvisor);
        cmd.Parameters.AddWithValue("FloorName", floorname);
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

    public DataTable GetEmployeeattndnc(string daymnthyr, string StProc)
    {
        string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(sConnString);
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = StProc;
        cmd.Parameters.AddWithValue("@Date", daymnthyr);
        cmd.Parameters.AddWithValue("@EmpType", "");
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

    public DataTable GetStageWiseProductivityDataTable(string StProc, string dDate, string ServiceAdvisor)
    {
        string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(sConnString);
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

    protected DataTable getCardNotScanned(DateTime FromDate, DateTime ToDate, string StProc)
    {
        string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(sConnString);
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = StProc;
        cmd.Parameters.AddWithValue("@DateFrom", FromDate);
        cmd.Parameters.AddWithValue("@Dateto", ToDate);
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
        string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(sConnString);
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
        string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        System.Data.SqlClient.SqlConnection oConn = new System.Data.SqlClient.SqlConnection(sConnString);
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
                RptViewer.Reset();
                return;
            }
            RptViewer.Reset();
            Microsoft.Reporting.WebForms.ReportParameter rParam1 = new Microsoft.Reporting.WebForms.ReportParameter("SAName", "ALL");
            Microsoft.Reporting.WebForms.ReportParameter rParam2 = new Microsoft.Reporting.WebForms.ReportParameter("FrmDate", dtfrm.ToString("dd-MM-yyyy"));
            Microsoft.Reporting.WebForms.ReportParameter rParam3 = new Microsoft.Reporting.WebForms.ReportParameter("ToDate", dtTo.ToString("dd-MM-yyyy"));
            Microsoft.Reporting.WebForms.ReportParameter rParam4 = new Microsoft.Reporting.WebForms.ReportParameter("FloorName", "ALL");
            Microsoft.Reporting.WebForms.ReportParameter rParam6 = new Microsoft.Reporting.WebForms.ReportParameter("IsBodyshop", "ALL");
            Microsoft.Reporting.WebForms.ReportParameter rParam5 = new Microsoft.Reporting.WebForms.ReportParameter("TLId", "ALL");
            Microsoft.Reporting.WebForms.ReportParameter rParam7 = new Microsoft.Reporting.WebForms.ReportParameter("Copyright", Copyright);
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;             // Set to Local Reporting Mode
            RptViewer.ShowExportControls = true;                                                   // Show The Export Controls
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + ReportRdlcName;
            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            ReportDS.Name = ReportName;
            ReportDS.Value = Dt;
            localRpt.DisplayName = FileName;
            localRpt.DataSources.Clear();
            string month = DateTime.Now.Month.ToString();
            if (DateTime.Now.Month < 10)
            {
                month = "0" + DateTime.Now.Month;
            }
            string daymnthyr = month + "/" + "01" + "/" + DateTime.Now.Year;
            string daymnthyrTo = month + "/" + DateTime.Now.Day + "/" + DateTime.Now.Year;
            localRpt.DataSources.Add(ReportDS);
            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1, rParam2, rParam3, rParam4, rParam5, rParam6, rParam7 });
            localRpt.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ProTrackDataSet_udpSADetails", DataManager.GetSADetails(daymnthyr, daymnthyrTo, EmpType, "udpSADetails")));
            localRpt.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ProTrackDataSet_GetSDD_PDD", DataManager.GetSDDPDD(daymnthyr, daymnthyrTo, EmpType, floorname, TLId, IsBodyshop, "GetSDD_PDD")));
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
        }
        catch (Exception ex)
        {
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
                lblStstus.Text = "No data found for date specified .<br> Try different Search Criteria.";
                RptViewer.Reset();
                return;
            }
            RptViewer.Reset();                                                                        // reset the report viewer
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;             // Set to Local Reporting Mode
            RptViewer.ShowExportControls = true;                                                   // Show The Export Controls
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + ReportRdlcName;
            Microsoft.Reporting.WebForms.ReportParameter rParam1 = new Microsoft.Reporting.WebForms.ReportParameter("DateFrom", ("Date : " + dtfrm.ToString("dd-MM-yyyy")));
            Microsoft.Reporting.WebForms.ReportParameter rParam3 = new Microsoft.Reporting.WebForms.ReportParameter("Copyright", Copyright);
            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            ReportDS.Name = ReportName;
            ReportDS.Value = Dt;
            localRpt.DisplayName = FileName;
            localRpt.DataSources.Clear();
            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1, rParam3 });
            localRpt.DataSources.Add(ReportDS);
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
            ReportName = ReportName.Substring(16);
        }
        catch (Exception ex)
        {
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
                lblStstus.Text = "No data found for date specified .<br> Try different Search Criteria.";
                RptViewer.Reset();
                return;
            }
            Microsoft.Reporting.WebForms.ReportParameter rParam1 = new Microsoft.Reporting.WebForms.ReportParameter("DateFrmTxt", "From: ");
            Microsoft.Reporting.WebForms.ReportParameter rParam2 = new Microsoft.Reporting.WebForms.ReportParameter("DateFrom", dtfrm.ToString("dd-MM-yyyy"));
            Microsoft.Reporting.WebForms.ReportParameter rParam3 = new Microsoft.Reporting.WebForms.ReportParameter("DateToTxt", "To: ");
            Microsoft.Reporting.WebForms.ReportParameter rParam4 = new Microsoft.Reporting.WebForms.ReportParameter("DateTo", dtTo.ToString("dd-MM-yyyy"));
            Microsoft.Reporting.WebForms.ReportParameter rParam5 = new Microsoft.Reporting.WebForms.ReportParameter("SANameTxt", "Service Advisor:");
            Microsoft.Reporting.WebForms.ReportParameter rParam6 = new Microsoft.Reporting.WebForms.ReportParameter("SAName", ServiceAdvisor);
            Microsoft.Reporting.WebForms.ReportParameter rParam7 = new Microsoft.Reporting.WebForms.ReportParameter("WMNametxt", "Works Manager: ");
            Microsoft.Reporting.WebForms.ReportParameter rParam8 = new Microsoft.Reporting.WebForms.ReportParameter("WMName", WorksManager);
            Microsoft.Reporting.WebForms.ReportParameter rParam11 = new Microsoft.Reporting.WebForms.ReportParameter("Aplustxt", "APlus: ");
            Microsoft.Reporting.WebForms.ReportParameter rParam12 = new Microsoft.Reporting.WebForms.ReportParameter("APlus", Aplus);
            Microsoft.Reporting.WebForms.ReportParameter rParam9 = new Microsoft.Reporting.WebForms.ReportParameter("VehicleNumbertxt", "Vehicle Number: ");
            Microsoft.Reporting.WebForms.ReportParameter rParam10 = new Microsoft.Reporting.WebForms.ReportParameter("VehicleNo", RegNo);
            Microsoft.Reporting.WebForms.ReportParameter rParam13 = new Microsoft.Reporting.WebForms.ReportParameter("Deliveredtxt", "Delivery Status: ");
            Microsoft.Reporting.WebForms.ReportParameter rParam14 = new Microsoft.Reporting.WebForms.ReportParameter("Delivered", DeliveryStatus);
            Microsoft.Reporting.WebForms.ReportParameter rParam15 = new Microsoft.Reporting.WebForms.ReportParameter("FloorName", "ALL");
            Microsoft.Reporting.WebForms.ReportParameter rParam16 = new Microsoft.Reporting.WebForms.ReportParameter("TLId", "ALL");
            Microsoft.Reporting.WebForms.ReportParameter rParam17 = new Microsoft.Reporting.WebForms.ReportParameter("IsBodyshop", "ALL");
            Microsoft.Reporting.WebForms.ReportParameter rParam18 = new Microsoft.Reporting.WebForms.ReportParameter("Copyright", Copyright);
            if (RegNo == "")
            {
                rParam9.Values.Clear();
                rParam10.Values.Clear();
                if (ServiceAdvisor == "")
                {
                    rParam5.Values.Clear();
                    rParam6.Values.Clear();
                }
                if (DeliveryStatus == "ALL")
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
            }
            RptViewer.Reset();                                                                        // reset the report viewer
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;             // Set to Local Reporting Mode
            RptViewer.ShowExportControls = true;                                                   // Show The Export Controls
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + ReportRdlcName;
            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            ReportDS.Name = ReportName;
            ReportDS.Value = Dt;
            localRpt.DisplayName = FileName;
            localRpt.DataSources.Clear();
            localRpt.DataSources.Add(ReportDS);
            localRpt.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ProTrackDataSet_ProcessValues", DataManager.GetSummaryReport("ProcessValues")));
            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1, rParam2, rParam3, rParam4, rParam5, rParam6, rParam7, rParam8, rParam9, rParam10, rParam11, rParam12, rParam13, rParam14, rParam15, rParam16, rParam17, rParam18 });
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
        }
        catch (Exception ex)
        {
            RptViewer.Reset();
            throw (ex);
        }
    }

    private void FillTechTMRReportTMRNEW(DataTable Dt, string ReportRdlcName, string ReportName, string FileName)
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
                lblStstus.Text = "No data found for date specified .<br> Try different Search Criteria.";
                RptViewer.Reset();
                return;
            }
            RptViewer.Reset();                                                                        // reset the report viewer
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;             // Set to Local Reporting Mode
            RptViewer.ShowExportControls = true;                                                   // Show The Export Controls
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + ReportRdlcName;
            Microsoft.Reporting.WebForms.ReportParameter rParam1 = new Microsoft.Reporting.WebForms.ReportParameter("DateFrom", ("Date : " + dtfrm.ToString("dd-MM-yyyy")));
            Microsoft.Reporting.WebForms.ReportParameter rParam4 = new Microsoft.Reporting.WebForms.ReportParameter("TLId", "ALL");
            Microsoft.Reporting.WebForms.ReportParameter rParam6 = new Microsoft.Reporting.WebForms.ReportParameter("Copyright", Copyright);
            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            ReportDS.Name = ReportName;
            ReportDS.Value = Dt;
            localRpt.DisplayName = FileName;
            localRpt.DataSources.Clear();
            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1, rParam4, rParam6 });
            localRpt.DataSources.Add(ReportDS);
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
            ReportName = ReportName.Substring(16);
        }
        catch (Exception ex)
        {
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
                lblStstus.Text = "No data found for date specified .<br> Try different Search Criteria.";
                RptViewer.Reset();
                return;
            }

            RptViewer.Reset();
            Microsoft.Reporting.WebForms.ReportParameter rParam1 = new Microsoft.Reporting.WebForms.ReportParameter("FloorName", "ALL");
            Microsoft.Reporting.WebForms.ReportParameter rParam2 = new Microsoft.Reporting.WebForms.ReportParameter("TLId", "ALL");
            Microsoft.Reporting.WebForms.ReportParameter rParam3 = new Microsoft.Reporting.WebForms.ReportParameter("IsBodyshop", "ALL");
            Microsoft.Reporting.WebForms.ReportParameter rParam4 = new Microsoft.Reporting.WebForms.ReportParameter("Copyright", Copyright);
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;             // Set to Local Reporting Mode
            RptViewer.ShowExportControls = true;                                                   // Show The Export Controls
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + ReportRdlcName;
            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            ReportDS.Name = ReportName;
            ReportDS.Value = Dt;
            localRpt.DisplayName = FileName;
            localRpt.DataSources.Clear();
            localRpt.DataSources.Add(ReportDS);
            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1, rParam2, rParam3, rParam4 });
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
        }
        catch (Exception ex)
        {
            RptViewer.Reset();
            throw (ex);
        }
    }

    private void FillTrendsReport(System.Data.DataTable Dt, string ReportRdlcName, string ReportName, string FileName, string ss, string day)
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
                lblStstus.Text = "No data found for date specified .<br> Try different Search Criteria.";
                RptViewer.Reset();
                return;
            }
            RptViewer.Reset();
            Microsoft.Reporting.WebForms.ReportParameter rParam1 = new Microsoft.Reporting.WebForms.ReportParameter("EmpType", "ALL");
            Microsoft.Reporting.WebForms.ReportParameter rParam2 = new Microsoft.Reporting.WebForms.ReportParameter("Date", System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month));
            Microsoft.Reporting.WebForms.ReportParameter rParam3 = new Microsoft.Reporting.WebForms.ReportParameter("Copyright", Copyright);
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;             // Set to Local Reporting Mode
            RptViewer.ShowExportControls = true;                                                   // Show The Export Controls
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + ReportRdlcName;
            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            ReportDS.Name = ReportName;
            ReportDS.Value = Dt;
            localRpt.DisplayName = FileName;
            localRpt.DataSources.Clear();
            localRpt.DataSources.Add(ReportDS);
            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1, rParam2, rParam3 });
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
        }
        catch (Exception ex)
        {
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
                lblStstus.Text = "No data found for date specified .<br> Try different Search Criteria.";
                RptViewer.Reset();
                return;
            }
            Microsoft.Reporting.WebForms.ReportParameter rParam1 = new Microsoft.Reporting.WebForms.ReportParameter("StartDate", "From :" + dtfrm.ToString("dd-MM-yyyy"));
            Microsoft.Reporting.WebForms.ReportParameter rParam2 = new Microsoft.Reporting.WebForms.ReportParameter("EndDate", "To :" + dtTo.ToString("dd-MM-yyyy"));
            Microsoft.Reporting.WebForms.ReportParameter rParam6 = new Microsoft.Reporting.WebForms.ReportParameter("SAName", "ALL");
            Microsoft.Reporting.WebForms.ReportParameter rParam4 = new Microsoft.Reporting.WebForms.ReportParameter("TLId", "ALL");
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
            if (file_name == "PITAnalysis")
            {
                this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1, rParam2, rParam4, rParam6, rParam7 });
            }
            else
            {
                this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1, rParam2, rParam4, rParam6, rParam7 });
            }
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
            if (AutoGen == true)
            {
            }
        }
        catch (Exception ex)
        {
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
                lblStstus.Text = "No data found for date specified .<br> Try different Search Criteria.";
                RptViewer.Reset();
                return;
            }
            Microsoft.Reporting.WebForms.ReportParameter rParam1 = new Microsoft.Reporting.WebForms.ReportParameter("DateFrom", dtfrm.ToString("dd-MM-yyyy"));
            Microsoft.Reporting.WebForms.ReportParameter rParam4 = new Microsoft.Reporting.WebForms.ReportParameter("TLId", "ALL");
            Microsoft.Reporting.WebForms.ReportParameter rParam2;
            if (ServiceAdvisor.ToString().Trim() == "")
            {
                rParam2 = new Microsoft.Reporting.WebForms.ReportParameter("SAName", "ALL");
            }
            else
            {
                rParam2 = new Microsoft.Reporting.WebForms.ReportParameter("SAName", ServiceAdvisor);
            }
            Microsoft.Reporting.WebForms.ReportParameter rParam6 = new Microsoft.Reporting.WebForms.ReportParameter("Copyright", Copyright);
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
            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1, rParam2, rParam4, rParam6 });
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
        }
        catch (Exception ex)
        {
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
                lblStstus.Text = "No data found for date specified .<br> Try different Search Criteria.";
                RptViewer.Reset();
                return;
            }
            RptViewer.Reset();                                                                        // reset the report viewer
            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;             // Set to Local Reporting Mode
            RptViewer.ShowExportControls = true;                                                   // Show The Export Controls
            Microsoft.Reporting.WebForms.LocalReport localRpt = this.RptViewer.LocalReport;
            localRpt.ReportPath = Server.MapPath("~/") + ReportRdlcName;
            Microsoft.Reporting.WebForms.ReportParameter rParam1 = new Microsoft.Reporting.WebForms.ReportParameter("DateFrom", ("Date : " + dtfrm.ToString("dd-MM-yyyy")));
            Microsoft.Reporting.WebForms.ReportParameter rParam4 = new Microsoft.Reporting.WebForms.ReportParameter("TLId", "ALL");
            Microsoft.Reporting.WebForms.ReportParameter rParam3 = new Microsoft.Reporting.WebForms.ReportParameter("SAName", "ALL");
            Microsoft.Reporting.WebForms.ReportParameter rParam6 = new Microsoft.Reporting.WebForms.ReportParameter("Copyright", Copyright);
            Microsoft.Reporting.WebForms.ReportDataSource ReportDS = new Microsoft.Reporting.WebForms.ReportDataSource();
            ReportDS.Name = ReportName;
            ReportDS.Value = Dt;
            localRpt.DisplayName = FileName;
            localRpt.DataSources.Clear();
            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1, rParam3, rParam4, rParam6 });
            localRpt.DataSources.Add(ReportDS);
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
            ReportName = ReportName.Substring(16);
            if (AutoGen == true)
            {
            }
        }
        catch (Exception ex)
        {
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
                lblStstus.Text = "No data found for date specified .<br> Try different Search Criteria.";
                RptViewer.Reset();
                return;
            }
            Microsoft.Reporting.WebForms.ReportParameter rParam1 = new Microsoft.Reporting.WebForms.ReportParameter("dtt", dtfrm.ToString("dd-MMM-yyyy"));
            Microsoft.Reporting.WebForms.ReportParameter rParam4 = new Microsoft.Reporting.WebForms.ReportParameter("TLId", "ALL");
            Microsoft.Reporting.WebForms.ReportParameter rParam6 = new Microsoft.Reporting.WebForms.ReportParameter("Copyright", Copyright);
            Microsoft.Reporting.WebForms.ReportParameter rParam2;
            if (ServiceAdvisor.ToString().Trim() == "")
            {
                rParam2 = new Microsoft.Reporting.WebForms.ReportParameter("SAName", "ALL");
            }
            else
            {
                rParam2 = new Microsoft.Reporting.WebForms.ReportParameter("SAName", ServiceAdvisor);
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
            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam1 });
            this.RptViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { rParam2, rParam4, rParam6 });
            this.RptViewer.LocalReport.Refresh();
            exportBytes = localRpt.Render("EXCEL", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
            if (AutoGen == true)
            {
            }
        }
        catch (Exception ex)
        {
            RptViewer.Reset();
            throw (ex);
        }
    }

    protected void btn_Back_Click(object sender, EventArgs e)
    {
        //if (Session["Role"].ToString() == "CRM")
        //{
        //    Response.Redirect("CRMDashboard.aspx");
        //}
        //else if (Session["Role"].ToString() == "SM")
        //{
        //    Response.Redirect("ServiceManagerDashboard.aspx");
        //}
        //else if (Session["Role"].ToString() == "GMSERVICE")
        //{
        //    Response.Redirect("GMServicesDashboard.aspx");
        //}
        //else if (Session["Role"].ToString() == "DEALER")
        //{
        //    Response.Redirect("DealerPrincipalDashboard.aspx");
        //}
        //else if (Session["Role"].ToString() == "WORK MANAGER")
        //{
        //    Response.Redirect("JobControllerDashboard.aspx");
        //}
        //else
        //{
            Response.Redirect("KPIDashboard.aspx");
        //}
    }
}