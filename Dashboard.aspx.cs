using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

public partial class Dashboard : System.Web.UI.Page
{
    //private SqlConnection dbaseCon = new SqlConnection(ConfigurationManager.ConnectionStrings["daypilot"].ToString());
    //private SqlConnection dbaseConNew = new SqlConnection(ConfigurationManager.ConnectionStrings["Attendance"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {


        //LabelDealerName.Text=
        //monthly data
        //DataTable MonthlyTotal1 = GetMonthlyData();
        //List<Int64> _MonthlyTotal = new List<Int64>();
        //List<Int64> _MonthlyPresent = new List<Int64>();
        //List<Int64> _MonthlyAbsent = new List<Int64>();
        //List<Int64> _MonthlyLate = new List<Int64>();
        //foreach (DataRow row in MonthlyTotal1.Rows)
        //{
        //    _MonthlyTotal.Add((Int64)row["TotalCount"]);
        //    _MonthlyPresent.Add((Int64)row["PresentCount"]);
        //    _MonthlyAbsent.Add((Int64)row["AbsentCount"]);
        //    _MonthlyLate.Add((Int64)row["LateCount"]);
        //}

        //JavaScriptSerializer jssMonthltTtl = new JavaScriptSerializer();
        //chartDataMonthlyConfirmed = jssMonthltTtl.Serialize(_MonthlyTotal);
        //chartDataMonthlyPresent = jssMonthltTtl.Serialize(_MonthlyPresent);
        //chartDataMonthlyLateArrival = jssMonthltTtl.Serialize(_MonthlyLate);
        //chartDataMonthlyAbsent = jssMonthltTtl.Serialize(_MonthlyAbsent);
        DataRow dr = getUnitData();
        PartsAmt = dr["PartsAmt"].ToString();
        labourAmt = dr["LabourAmt"].ToString();
        VasAmt = dr["VasAmt"].ToString();
        LubeAmt = dr["LubeAmt"].ToString();

        //daily data
        DataTable DailyData = GetDailyData();
        List<Int64> _DailyRevenue = new List<Int64>();
        List<Int64> _DailyTarget = new List<Int64>();
       
        foreach (DataRow row in DailyData.Rows)
        {
            _DailyRevenue.Add((Int64)row["BillAmount"]);
            _DailyTarget.Add((Int64)row["TargetAmount"]);
           
        }

        JavaScriptSerializer jssDailyData = new JavaScriptSerializer();
        TotalRevenue = jssDailyData.Serialize(_DailyRevenue);
        TargetAmt = jssDailyData.Serialize(_DailyTarget);
      

        //weekly data
        //DataTable WeeeklyData = GetWeeklyData();
        //List<Int64> _WeeklyTotal = new List<Int64>();
        //List<Int64> _WeeklyPresent = new List<Int64>();
        //List<Int64> _WeeklyAbsent = new List<Int64>();
        //List<Int64> _WeeklyLA = new List<Int64>();
        //foreach (DataRow row in WeeeklyData.Rows)
        //{
        //    _WeeklyTotal.Add((Int64)row["TotalCount"]);
        //    _WeeklyPresent.Add((Int64)row["PresentCount"]);
        //    _WeeklyAbsent.Add((Int64)row["AbsentCount"]);
        //    _WeeklyLA.Add((Int64)row["LateCount"]);
        //}

        //JavaScriptSerializer jssWeeklyData = new JavaScriptSerializer();
        //WeeklyTotal = jssWeeklyData.Serialize(_WeeklyTotal);
        //WeeklyPresent = jssWeeklyData.Serialize(_WeeklyPresent);
        //WeeklyAbsent = jssWeeklyData.Serialize(_WeeklyAbsent);
        //WeeklyLA = jssWeeklyData.Serialize(_WeeklyLA);

        //string cs = ConfigurationManager.ConnectionStrings["TA_ConnectionString"].ConnectionString;
        //using (SqlConnection con = new SqlConnection(cs))
        //{
        //    SqlCommand cmd = new SqlCommand("UdpInsertCompanyDetails", con);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@Id", id);
        //    con.Open();
        //    byte[] bytes = (byte[])cmd.ExecuteScalar();
        //    string strBase64 = Convert.ToBase64String(bytes);
        //    Image1.ImageUrl = "data:Image/png;base64," + strBase64;
        //}

        if (!IsPostBack)
        {
            //if (Image1.ImageUrl != "")
            //{
            //    Image1.ImageUrl = "Images/gravatar.jpg";
            //}
            //else {
                getImage();
            //}
            //DataRow dr = getDailyCount();
            //if (dr == null)
            //{
            //    throw new Exception("No data found");
            //}
            //  labelTotal.Text = Convert.ToInt32(dr["TotalCount"]).ToString();

            //  String v1= Convert.ToInt32(dr["TotalCount"]).ToString()
            //   String v2 = Convert.ToInt32(dr["Empleft"]);
            //  String v3 = v1 - v2;

            labelTotal.Text = "0";
            //  labelTotal.Text = Convert.ToInt32(dr["Fullcount"]).ToString();
            labelPresent.Text = "0";
            labelAbsent.Text = "0";
            labelArrival.Text = "0";
            labelleft.Text = "0";

            //DataRow dr_dc = getDeputationCount();
            //if (dr_dc == null)
            //{
                Lbl_DptIn.Text = "0";
                Lbl_Dptout.Text = "0";
            //}
            //Lbl_DptIn.Text = Convert.ToInt32(dr_dc["DeputationIn"]).ToString();
            //Lbl_Dptout.Text = Convert.ToInt32(dr_dc["DeputationOut"]).ToString();

           // DataRow login = GetLoginDetails();
            //if (login == null)
            //{
             //   LabelLogin.Text = Convert.ToDateTime(DateTime.Now).ToString("dd MMM yyyy, HH:mm tt");
            //}
            //else { 
            //LabelLogin.Text = Convert.ToDateTime(login["logdate"]).ToString("dd MMM yyyy, HH:mm tt");
            //}

            //DataRow inst_date = getInstlattionDate();
            //if (inst_date == null)
            //{
             //   Lbl_instDate.Text="365"+ "&nbsp;&nbsp;DAYS";
            //}
            //else { 
            //Lbl_instDate.Text = inst_date["days"].ToString()+ "&nbsp;&nbsp;DAYS";
            //}
            //if (Convert.ToInt32(inst_date["days"].ToString()) <= 0)
            //{
            //    Session.Clear();
            //    Response.Redirect("login.aspx");
            //}
        }

    }

    //private DataRow GetLoginDetails()
    //{
    //    SqlCommand cmd = new SqlCommand("udpGetLastloginDetails", new SqlConnection(ConfigurationManager.ConnectionStrings["Attendance_db"].ToString()));

    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.Parameters.AddWithValue("@Company_Id", Session["Company_Id"]);
    //    cmd.Parameters.AddWithValue("@Dealer_Id ", Session["Dealer_Id"]);
    //    SqlDataAdapter da1 = new SqlDataAdapter(cmd);
    //    DataTable dt = new DataTable();
    //    da1.Fill(dt);

    //    if (dt.Rows.Count > 0)
    //    {
    //        return dt.Rows[0];
    //    }
    //    return null;
    //}
    public DataRow getUnitData()
    {
        DataTable dt = new DataTable();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Attendance_db"].ToString());
        SqlCommand cmd = new SqlCommand("GetUnitsRevenue", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@StDAte", DateTime.Now);
     
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        
        sda.Fill(dt);
        if (dt.Rows.Count>0)
        {
            return dt.Rows[0];
        }
        return null;
    }


    public string chartData
    {
        get;
        set;
    }

    public string chartData1
    {
        get;
        set;
    }

    public string chartDataDailyConfirmed
    {
        get;
        set;
    }

    public string chartDataDailyRequested
    {
        get;
        set;
    }
    public string TotalRevenue
    {
        get;
        set;
    }
    public string TargetAmt
    {
        get;
        set;
    }
    public string AbsentCount
    {
        get;
        set;
    }
    public string LateCount
    {
        get;
        set;
    }
    public string highchart
    {
        get;
        set;
    }
    public string chartDataMonthlyConfirmed
    {
        get;
        set;
    }
    public string chartDataMonthlyPresent
    {
        get;
        set;
    }
    public string chartDataMonthlyLateArrival
    {
        get;
        set;
    }
    public string chartDataMonthlyAbsent
    {
        get;
        set;
    }
    public string WeeklyPresent
    {
        get;
        set;
    }
    public string WeeklyAbsent
    {
        get;
        set;
    }
    public string WeeklyTotal
    {
        get;
        set;
    }
    public string WeeklyLA
    {
        get;
        set;
    }
    public string labourAmt { get; set; }
    public string PartsAmt { get; set; }
    public string VasAmt { get; set; }
    public string LubeAmt { get; set; }


    private DataTable GetDailyData()
    {
        SqlCommand cmd = new SqlCommand("GetdailyCashierdata", new SqlConnection(ConfigurationManager.ConnectionStrings["Attendance_db"].ToString()));

        cmd.CommandType = CommandType.StoredProcedure;
        
        SqlDataAdapter da1 = new SqlDataAdapter(cmd);
        DataTable dailyData = new DataTable();
        da1.Fill(dailyData);
        return dailyData;
    }
    //private DataTable GetWeeklyData()
    //{
    //    SqlCommand cmd = new SqlCommand("udpgetWeeklyDashboarddata", new SqlConnection(ConfigurationManager.ConnectionStrings["Attendance_db"].ToString()));

    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.Parameters.AddWithValue("@Comapanyid", Session["Company_Id"]);
    //    cmd.Parameters.AddWithValue("@DealerId", Session["Dealer_Id"]);
    //    SqlDataAdapter daw = new SqlDataAdapter(cmd);
    //    DataTable WeeeklyData = new DataTable();
    //    daw.Fill(WeeeklyData);
    //    return WeeeklyData;
    //}

    //private DataTable GetMonthlyData()
    //{
      
    //        SqlCommand cmd = new SqlCommand("udpgetMonthlyDashboarddata", new SqlConnection(ConfigurationManager.ConnectionStrings["Attendance_db"].ToString()));

    //    cmd.CommandTimeout = 200;
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.Parameters.AddWithValue("@Comapanyid", Session["Company_Id"]);
    //        cmd.Parameters.AddWithValue("@DealerId", Session["Dealer_Id"]);
    //        SqlDataAdapter dam = new SqlDataAdapter(cmd);
    //        DataTable MonthlyData = new DataTable();
    //        dam.Fill(MonthlyData);
    //        return MonthlyData;
       
    //}
   


    //private DataRow getDailyCount()
    //{
    //    SqlCommand cmd = new SqlCommand("udpgetDailyDashboarddataCount", new SqlConnection(ConfigurationManager.ConnectionStrings["Attendance_db"].ToString()));

    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.Parameters.AddWithValue("@Comapanyid", Session["Company_Id"] );
    //    cmd.Parameters.AddWithValue("@DealerId", Session["Dealer_Id"]);
    //    cmd.Parameters.AddWithValue("@Date", Convert.ToDateTime(DateTime.Now));
    //    SqlDataAdapter da1 = new SqlDataAdapter(cmd);
    //    DataTable dt = new DataTable();
    //    da1.Fill(dt);

    //    if (dt.Rows.Count > 0)
    //    {
    //        return dt.Rows[0];
    //    }
    //    return null;

    //}

    //private DataRow getDeputationCount()
    //{
    //    SqlCommand cmd = new SqlCommand("udpGetDeputationCount", new SqlConnection(ConfigurationManager.ConnectionStrings["Attendance_db"].ToString()));

    //    cmd.CommandType = CommandType.StoredProcedure;
    //    //cmd.Parameters.AddWithValue("@Companyid", Session["Company_Id"]);
    //    //cmd.Parameters.AddWithValue("@DealerId", Session["Dealer_Id"]);
    //    SqlDataAdapter dc = new SqlDataAdapter(cmd);
    //    DataTable dc_dt = new DataTable();
    //    dc.Fill(dc_dt);

    //    if (dc_dt.Rows.Count > 0)
    //    {
    //        return dc_dt.Rows[0];
    //    }
    //    return null;

    //}

    //private DataRow getInstlattionDate()
    //{
    //    SqlCommand cmd = new SqlCommand("udpGetInstalationDate", new SqlConnection(ConfigurationManager.ConnectionStrings["Attendance_db"].ToString()));

    //    cmd.CommandType = CommandType.StoredProcedure;
    //    //cmd.Parameters.AddWithValue("@companyId", Session["Company_Id"]);
    //    //cmd.Parameters.AddWithValue("@DealerId", Session["Dealer_Id"]);
    //    SqlDataAdapter dc = new SqlDataAdapter(cmd);
    //    DataTable dc_dt = new DataTable();
    //    dc.Fill(dc_dt);

    //    if (dc_dt.Rows.Count > 0)
    //    {
    //        return dc_dt.Rows[0];
    //    }
    //    return null;

    //}

    protected void getImage()
    {
        //string cs = ConfigurationManager.ConnectionStrings["TA_ConnectionString"].ConnectionString;
        //using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Attendance_db"].ToString()))
        //{
        //    SqlCommand cmd = new SqlCommand("UdpGetCompanylogo", con);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    //cmd.Parameters.AddWithValue("@CompanyId", Session["Company_Id"]);
        //    //cmd.Parameters.AddWithValue("@DealerId", Session["Dealer_Id"]);
        //    SqlDataAdapter sda = new SqlDataAdapter(cmd);
        //    DataTable dt = new DataTable();
        //    sda.Fill(dt);
        //    if (dt.Rows.Count == 0)
        //    {
              //  Image1.ImageUrl = "Images/gravatar.jpg";
        //    }
        //    else
        //    {
        //        if (dt.Rows[0][0] == DBNull.Value)
        //        {
        //            Image1.ImageUrl = "Images/gravatar.jpg";
        //        } 
        //        else { 
        //        byte[] bytes = (byte[])(dt.Rows[0][0]);
        //        string strBase64 = Convert.ToBase64String(bytes);
        //        Image1.ImageUrl = "data:Image/png;base64," + strBase64;
        //           // Image1.ImageUrl = "Images/gravatar.jpg";
        //        }

        //    }

        //}

    }

}