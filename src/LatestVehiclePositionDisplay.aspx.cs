using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web;

public partial class LatestVehiclePositionDisplay : System.Web.UI.Page
{
    private SqlConnection dbaseCon = new SqlConnection();
    public DataTable dtVehicle = new DataTable();
    public DataTable dtVehicleI = new DataTable();
    private int VHR = 0;
    private int WA = 0;
    private int RT = 0;
    private int FI = 0;
    private int Parking = 0;
    private int Wash = 0;
    private static string BackTo = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (Session[Session["TMLDealercode"] + "-TMLConString"].ToString() == null || Session[Session["TMLDealercode"] + "-TMLConString"].ToString() == "")
                Response.Redirect("login.aspx");
        }
        catch
        {
            Response.Redirect("login.aspx");
        }
        lbl_vehiclesDeliveredToday.Text = getDailyVehiclesDelCount().ToString();

        FillVehicleDashboard();
        FillVehicleIdleDashboard();
        lbVI.Text = (WA + FI + Wash + VHR).ToString();

        lbTotal.Text = ((int.Parse(lbWGate.Text)) + (int.Parse(lbRO.Text)) + (int.Parse(lbWorkshop.Text)) + (int.Parse(lbSpeedo.Text)) + + (int.Parse(lbWA.Text)) + (int.Parse(lbRT.Text)) + (int.Parse(lbFI.Text)) + (int.Parse(lbWash.Text)) + (int.Parse(lbVR.Text)) + (int.Parse(lbVI.Text)) + (int.Parse(lbVH.Text))).ToString();
        if (Page.Request.QueryString["Back"] != null)
        {
            //btnBACK.Visible = true;
            try
            {
                BackTo = Session["Role"].ToString();
            }
            catch (Exception ex)
            {
                Response.Redirect("login.aspx");
            }
        }
        else
        {
            //btnBACK.Visible = true;
        }
    }
    public void FillVehicleDashboard()
    {
        try
        {
            pnl_Display.Controls.Clear();
            string procName = "";
            string title = "";
            string titleColor = "";
            string ProcessName = "";
            string VehicleCount = "";



            for (int i = 0; i < 9; i++)
            {
                if (i == 0)
                {
                    procName = "GetVehiclePositionInfo_New";
                    title = "Gate";
                    titleColor = "#006666";
                    ProcessName = "Gate";
                    //lbWGate.Text = VehicleCount;
                    DataTable dtVehicle = new DataTable();
                    dtVehicle = GetVehicles(procName, ProcessName);
                    lbWGate.Text = dtVehicle.Rows.Count.ToString();

                }
                else if (i == 1)
                {
                    procName = "GetVehiclePositionInfo_New";
                    title = "Job Card";
                    //titleColor = "#c52047";
                    titleColor = "#ff6b31";
                    ProcessName = "JobSlip";
                    DataTable dtVehicle1 = new DataTable();
                    dtVehicle1 = GetVehicles(procName, ProcessName);
                    lbRO.Text = dtVehicle1.Rows.Count.ToString();
                }

                else if (i == 2)
                {
                    procName = "GetVehiclePositionInfo_New";
                    title = "Workshop";
                    titleColor = "#53a145";
                    ProcessName = "WorkShop";
                    DataTable dtVehicle2 = new DataTable();
                    dtVehicle2 = GetVehicles(procName, ProcessName);
                    lbWorkshop.Text = dtVehicle2.Rows.Count.ToString();
                }
                else if (i == 3)
                {
                    procName = "GetVehiclePositionInfo_New";
                    title = "Speedo";
                    titleColor = "#c16978";
                    ProcessName = "Speedo";
                    DataTable dtVehicle2 = new DataTable();
                    dtVehicle2 = GetVehicles(procName, ProcessName);
                    lbSpeedo.Text = dtVehicle2.Rows.Count.ToString();


                }
                else if (i == 4)
                {
                    procName = "GetVehiclePositionInfo_New";
                    title = "Wheel Alignment";
                    titleColor = "#275d8b";
                    ProcessName = "Wheel Alignment";
                    dtVehicle = GetVehicles(procName, ProcessName);
                    lbWA.Text = dtVehicle.Rows.Count.ToString();


                }
                else if (i == 5)
                {
                    procName = "GetVehiclePositionInfo_New";
                    title = "Road Test";
                    titleColor = "#11a285";
                    ProcessName = "RT";
                    dtVehicle = GetVehicles(procName, ProcessName);
                    lbRT.Text = dtVehicle.Rows.Count.ToString();

                }
                else if (i == 6)
                {
                    procName = "GetVehiclePositionInfo_New";
                    title = " Quality";
                    titleColor = "#6d4270";
                    ProcessName = "Final Inspection";
                    dtVehicle = GetVehicles(procName, ProcessName);
                    lbFI.Text = dtVehicle.Rows.Count.ToString();


                }
                else if (i == 7)
                {
                    procName = "GetVehiclePositionInfo_New";
                    title = "Wash";
                    titleColor = "#d59010";
                    ProcessName = "Wash";
                    dtVehicle = GetVehicles(procName, ProcessName);
                    lbWash.Text = dtVehicle.Rows.Count.ToString();


                }
                else if (i == 8)
                {
                    procName = "GetVehiclePositionInfo_New";
                    title = "Ready";
                    titleColor = "#95a112";
                    ProcessName = "Vehicle Ready";
                    dtVehicle = GetVehicles(procName, ProcessName);
                    lbVR.Text = dtVehicle.Rows.Count.ToString();


                }
                var cell = new HtmlTableCell();
                cell.VAlign = "Top";
                cell.Style.Value = "width:11.11%;";

                //var Row = new HtmlTableRow();


                //  DataTable dtVehicle = new DataTable();
                dtVehicle = GetVehicles(procName, ProcessName);
                int totalVehicle = dtVehicle.Rows.Count;

                DataTable dt = new DataTable();
              
                
                Panel pnl = new Panel();
                pnl.ID = ("pnl_" + i.ToString());
                // pnl.Style.Value = "width:100%;height:340px;background-color:#FFFFFF;overflow:auto;cellspacing='0'; cellpadding='0';";
                pnl.CssClass = "PanelStyle1";
                for (int j = 0; j < totalVehicle; j++)
                {
                    dt = GetInOutTime(dtVehicle.Rows[j]["Slno"].ToString(), ProcessName);
                    Panel pnla = new Panel();
                    PositionDisplay vt = (PositionDisplay)Page.LoadControl("PositionDisplay.ascx");
                    int Vehlenth = dtVehicle.Rows[j]["RegNo"].ToString().Length;
                    if (dtVehicle.Rows[j]["RegNo"].ToString().Length > 8)
                        vt.RegNo = dtVehicle.Rows[j]["RegNo"].ToString().Substring(2, Vehlenth - 2);
                    else
                        vt.RegNo = dtVehicle.Rows[j]["RegNo"].ToString();
                    vt.Model = dtVehicle.Rows[j]["VehicleModel"].ToString();
                    vt.VehicleImage = DataManager.car_image(dtVehicle.Rows[j]["ModelImageUrl"].ToString());
                    vt.Slno = dtVehicle.Rows[j]["Slno"].ToString();
                    //if (dtVehicle.Rows[j]["Position"].ToString() != "Gate")
                    vt.LastProcess = DataManager.jcr_image(dtVehicle.Rows[j]["PositionUrl"].ToString());
                    // lblSlno.Text = dtVehicle.Rows[j]["Slno"].ToString();
                    vt.VehicleColor = GetPositionColor(dtVehicle.Rows[j]["Position"].ToString().Trim());

                    //vt.PDTCheck = true;
                    //vt.PDT = dtVehicle.Rows[j]["PDT"].ToString();
                    vt.PDTImage = DataManager.jcr_image(dtVehicle.Rows[j]["PDTStatus"].ToString());
                    if (dtVehicle.Rows[j]["ServiceAdvisor"].ToString().Length > 10)
                        vt.ServiceAdvisor = dtVehicle.Rows[j]["ServiceAdvisor"].ToString().Substring(0, 10);
                    else
                        vt.ServiceAdvisor = dtVehicle.Rows[j]["ServiceAdvisor"].ToString();
                    if (ProcessName.Contains("Ready")) { 
                    vt.idletime = "Ready "+ dt.Rows[0]["Intime"].ToString().Replace("#", " ");
                    }
                    else
                    {
                        vt.idletime = "Intime " + dt.Rows[0]["Intime"].ToString().Replace("#", " ");
                    }
                    //vt.CWJDPImage = dtVehicle.Rows[j]["CWJDP"].ToString();



                    pnla.Controls.Add(vt);
                    //pnla.Attributes.Add("onmouseover", "ShowLoadProcessInOutTime(event,'" + vt.Slno.ToString() + "','" + ProcessName + "')");
                    //pnla.Attributes.Add("onmouseout", "hideTooltip(event)");
                    pnl.Controls.Add(pnla);
                    //cell.Controls.Add(pnl);
                    //Row.Controls.Add(cell);
                }




                Label lbl = new Label();
                lbl.Style.Value = "width:99%;background-color:" + titleColor + ";vertical-align:middle;color:#FFFFFF; font-weight:bold;padding-left:5px;font-size:18px;";
                lbl.Height = new Unit(64);
                lbl.Width = new Unit(13);
                //lbl.Text = "<table style='width: 99%; height: 100%;' border='0' cellspacing='0' cellpadding='0'><tr><td style='white-space:nowrap;'>" + title + "</td><td style='white-space:nowrap;text-align:right;padding-right:15px;border=1;'> " + totalVehicle.ToString() + "</td></tr></table>";
                lbl.Text = "<table style='width: 99%; height: 100%;' border='0' cellspacing='0' cellpadding='0'><tr><td style='white-space:nowrap;text-align:center;border=1;font-family:Roboto, sans-serif; font-size:35px;'> " + totalVehicle.ToString() + "</td></tr><tr><td style='white-space:nowrap;text-align:center;font-family:Roboto, sans-serif; font-size:15px;'>" + title + "</td></tr></table>";
                cell.Controls.Add(lbl);
                cell.Controls.Add(pnl);



                //DataTable dt = new DataTable();
                //dt = GetVehicles(procName, ProcessName);
                //int totalVehicle1 = dt.Rows.Count;
                //for (int flag = 0; flag < totalVehicle1; flag++)
                //{

                //    cell.Attributes.Add("onmouseover", "ShowLoadProcessInOutTime(event,'" + dt.Rows[flag]["Slno"].ToString() + "','" + ProcessName + "')");
                //    cell.Attributes.Add("onmouseout", "hideTooltip(event)");
                //    pnl_Display.Cells.Add(cell);
                //}
                pnl_Display.Controls.Add(cell);
                // pnl_Display.Cells.Add(cell);

                // tbl_Hover.Cells.Add(pnl_Display);

                //pnl_Display.Attributes.Add("onmouseover", "ShowLoadProcessInOutTime(event,'" + lblSlno.Text.ToString() + "','" + ProcessName + "')");
                //pnl_Display.Attributes.Add("onmouseout", "hideTooltip(event)");


                // pnl_Display.Attributes.Add("onmouseover", "ShowLoadProcessInOutTime(event,'" + lblSlno.Text + "','Gate')");


            }
        }
        catch (Exception ex)
        {

        }

    }
    public string GetPositionColor(string Position)
    {
        string col = "";
        switch (Position)
        {
            case "Gate":
                col = "#006666";
                break;

            case "JobSlip":
                //col = "#c52047";
                col = "#ff6b31";
                break;

            case "Wash":
                col = "#d59010";
                break;

            case "Workshop":
                col = "#53a145";
                break;

            case "WA":
                col = "#275d8b";
                break;

            case "Wheel Alignment":
                col = "#275d8b";
                break;

            case "VAS":
                col = "#B8B872";
                break;

            case "Final Inspection":
                col = "#6d4270";
                break;

            case "RT":
                col = "#11a285";
                break;

            case "Road Test":
                col = "#11a285";
                break;

            case "Vehicle Ready":
                col = "#95a112";
                break;

            case "Vehicle Idle":
                col = "#FFFA73";
                break;
        }
        return col;
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        Page_Load(null, null);
    }
    private DataTable GetVehicles(string procedureName, string ProcessName)
    {
        // string dbaseCon = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        try
        {
            SqlConnection dbaseCon = new SqlConnection(System.Web.HttpContext.Current.Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(procedureName, dbaseCon);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Position", ProcessName);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        //finally
        //{
        //    if (dbaseCon.State == ConnectionState.Open)
        //        dbaseCon.Close();
        //}
    }
    public void FillVehicleIdleDashboard()
    {

        try
        {
            pnl_Idle_Display.Controls.Clear();

            string procName = "";
            string title = "";
            string titleColor = "";
            string ProcessName = "";



            for (int i = 0; i < 5; i++)
            {
                if (i == 0)
                {
                    procName = "GetVehiclePositionInfo_New";
                    title = " Vehicle Hold";
                    titleColor = "Gray";
                    ProcessName = "Vehicle Hold";
                    dtVehicleI = GetVehicles(procName, ProcessName);
                    lbVH.Text = dtVehicleI.Rows.Count.ToString();

                }
                else if (i == 1)
                {
                    procName = "GetVehicleIdlePositionInfo_New";
                    title = "Workshop";
                    titleColor = "#53a145";
                    ProcessName = "Workshop";
                    dtVehicleI = GetVehicles(procName, ProcessName);
                    VHR = int.Parse(dtVehicleI.Rows.Count.ToString());
                }
                else if (i == 2)
                {
                    procName = "GetVehicleIdlePositionInfo_New";
                    title = "Wheel Alignment";
                    titleColor = "#275d8b";
                    ProcessName = "Wheel Alignment";

                    dtVehicleI = GetVehicles(procName, ProcessName);
                    WA = int.Parse(dtVehicleI.Rows.Count.ToString());
                }

                else if (i == 3)
                {
                    procName = "GetVehicleIdlePositionInfo_New";
                    title = "Quality";
                    titleColor = "#6d4270";
                    ProcessName = "Final Inspection";

                    dtVehicleI = GetVehicles(procName, ProcessName);
                    FI = int.Parse(dtVehicleI.Rows.Count.ToString());
                }
                else if (i == 4)
                {
                    procName = "GetVehicleIdlePositionInfo_New";
                    title = "Wash";
                    titleColor = "#d59010";
                    ProcessName = "Wash";
                    dtVehicleI = GetVehicles(procName, ProcessName);
                    Wash = int.Parse(dtVehicleI.Rows.Count.ToString());
                }




                var cell = new HtmlTableCell();
                cell.VAlign = "Top";
                cell.Style.Value = "width:20%;";


                DataTable dtVehicle = new DataTable();
                dtVehicle = GetVehicles(procName, ProcessName);
                int totalVehicle = dtVehicle.Rows.Count;

                Panel pnl1 = new Panel();
                pnl1.ID = ("pnl1_" + i.ToString());
                //pnl1.Style.Value = "width:100%;height:220px;background-color:#FFFFFF;overflow:auto;cellspacing='0'; cellpadding='0';";
                pnl1.CssClass = "PanelStyle";
                for (int j = 0; j < totalVehicle; j++)
                {

                    Panel pnb = new Panel();

                    PositionDisplay vt = (PositionDisplay)Page.LoadControl("PositionDisplay.ascx");
                    vt.RegNo = dtVehicle.Rows[j]["RegNo"].ToString();
                    vt.Model = dtVehicle.Rows[j]["VehicleModel"].ToString();
                    vt.VehicleImage = DataManager.car_image(dtVehicle.Rows[j]["ModelImageUrl"].ToString());
                    vt.VehicleColor = GetPositionColor(dtVehicle.Rows[j]["Position"].ToString().Trim());
                    lblSlno.Text = dtVehicle.Rows[j]["Slno"].ToString();
                    //vt.PDT = dtVehicle.Rows[j]["PDT"].ToString();
                    vt.PDTImage = DataManager.jcr_image(dtVehicle.Rows[j]["PDTStatus"].ToString());
                    if (ProcessName != "Gate")
                        vt.LastProcess = DataManager.jcr_image(dtVehicle.Rows[j]["PositionUrl"].ToString());
                    //vt.GateInTime = dtVehicle.Rows[j]["PDT"].ToString();
                    if (dtVehicle.Rows[j]["ServiceAdvisor"].ToString().Length > 12)
                        vt.ServiceAdvisor = dtVehicle.Rows[j]["ServiceAdvisor"].ToString().Substring(0, 12);
                    else
                        vt.ServiceAdvisor = dtVehicle.Rows[j]["ServiceAdvisor"].ToString();
                    vt.idletime = LoadIdleInOutTime(dtVehicle.Rows[j]["Slno"].ToString());
                    //vt.CWJDPImage = dtVehicle.Rows[j]["CWJDP"].ToString();
                    //vt.Attributes.Add("onmouseover", "ShowLoadProcessInOutTime(event,'" + lblSlno.Text.ToString() + "','" + ProcessName + "')");
                    //vt.Attributes.Add("onmouseout", "hideTooltip(event)");
                    pnb.Controls.Add(vt);

                    /*CHANGES MADE TO HOVER*/

                    //if (ProcessName == "Vehicle Hold")
                    //{
                    //    pnb.Attributes.Add("onmouseover", "ShowLoadProcessInOutTime(event,'" + lblSlno.Text.ToString() + "','" + ProcessName + "')");
                    //    pnb.Attributes.Add("onmouseout", "hideTooltip(event)");
                    //}
                    //else
                    //{
                    //    pnb.Attributes.Add("onmouseover", "ShowLoadIdleInOutTime(event,'" + lblSlno.Text.ToString() + "')");
                    //    pnb.Attributes.Add("onmouseout", "hideTooltip(event)");
                    //}


                    /*CHANGES MADE TO HOVER*/
                    pnl1.Controls.Add(pnb);

                }


                Label lbl = new Label();
                //if (ProcessName == "BodyShop")
                //    lbl.Style.Value = "width:99%;background-color:" + titleColor + ";vertical-align:middle;color:#000000; font-weight:bold;padding-left:5px;font-size:18px;";
                //else
                lbl.Style.Value = "width:99%;background-color:" + titleColor + ";vertical-align:middle;color:#FFFFFF; font-weight:bold;padding-left:5px;font-size:18px;";
                lbl.Height = new Unit(24);
                lbl.Width = new Unit(13);
                lbl.Text = "<table style='width: 99%; height: 100%;' border='0' cellspacing='0' cellpadding='0'><tr><td style='white-space:nowrap;font-family:Roboto, sans-serif;font-size:15px;'>" + title + "</td><td style='white-space:nowrap;text-align:right;padding-right:15px;font-family:Roboto, sans-serif;font-size:15px;'>" + totalVehicle.ToString() + "</td></tr></table>";
                cell.Controls.Add(lbl);


                cell.Controls.Add(pnl1);

                pnl_Idle_Display.Cells.Add(cell);


            }
        }
        catch (Exception ex)
        {

        }

    }
    public static DataTable GetInOutTime(string RefNo, string ProcessName)
    {
        DataTable dt = new DataTable();
        SqlConnection con = new SqlConnection(System.Web.HttpContext.Current.Session[HttpContext.Current.Session["TMLDealercode"] + "-TMLConString"].ToString());

        SqlCommand cmd = new SqlCommand("GetPositionProcessHover", con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@RefNo", RefNo);
        cmd.Parameters.AddWithValue("@Position", ProcessName);
        if (con.State != ConnectionState.Open)
            con.Open();
        sda.Fill(dt);
        if (con.State != ConnectionState.Closed)
            con.Close();
        return dt;
    }
    private static DataTable GetRemarksHover(string RefNo)
    {
        DataTable dt = new DataTable();
        SqlConnection con = new SqlConnection(System.Web.HttpContext.Current.Session[HttpContext.Current.Session["TMLDealercode"] + "-TMLConString"].ToString());

        SqlCommand cmd = new SqlCommand("GetRemarksHover", con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@RefNo", RefNo);
        if (con.State != ConnectionState.Open)
            con.Open();
        cmd.ExecuteNonQuery();
        sda.Fill(dt);
        if (con.State != ConnectionState.Closed)
            con.Close();
        return dt;
    }
    [System.Web.Services.WebMethod(EnableSession = true)]
    public static string LoadProcessInOutTime(string RefNo, string ProcessName)
    {
        DataTable dt = new DataTable();
        DataTable dtHold = new DataTable();
        dt = GetInOutTime(RefNo, ProcessName);
        dtHold = GetRemarksHover(RefNo);
        string str = "";
        if (dt.Rows.Count > 0)
        {
            if (ProcessName == "Vehicle Ready")
            {
                str = "<span><table style=font-style:Consolas, Georgia;><tr style=background-color:#c3d9ff;text-align:center;><th style=width:100px;>Vehicle No</th><th style=width:100px;>Service Advisor</th><th style=width:100px;>PDT</th><th style=width:100px;>Ready Time</th></tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    str += "<tr style=text-align:center;><td>" + dt.Rows[i]["RegNo"].ToString() + "</td><td>" + dt.Rows[i]["EmpName"].ToString() + "</td><td>" + dt.Rows[i]["PDT"].ToString() + "</td><td>" + dt.Rows[i]["OutTime"].ToString().Replace("#", " ") + "</td></tr>";
                }
                str += "</table></span>";
            }
            else if (ProcessName == "Gate")
            {
                str = "<span><table style=font-style:Consolas, Georgia;><tr style=background-color:#c3d9ff;text-align:center;><th style=width:100px;>Vehicle No</th><th style=width:100px;>Service Advisor</th><th style=width:100px;>InTime</th><th style=width:100px;>PDT</th></tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    str += "<tr style=text-align:center;><td>" + dt.Rows[i]["RegNo"].ToString() + "</td><td>" + dt.Rows[i]["EmpName"].ToString() + "</td><td>" + dt.Rows[i]["InTime"].ToString().Replace("#", " ") + "</td><td>" + dt.Rows[i]["PDT"].ToString() + "</td></tr>";
                }
                str += "</table></span>";
            }
            else if (ProcessName == "Vehicle Hold")
            {
                str = "<span><table style=font-style:Consolas, Georgia;><tr style=text-align:center;border:0px;><th style=width:100px;font-size=10px;>Employee</th><th style=width:100px;>Employee Type</th><th style=width:100px;>InTime</th><th style=width:100px;>OutTime</th><th style=width:100px;>Time</th><th style=width:250px;>Remarks</th></tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dtHold.Rows.Count; j++)
                    {
                        if (dtHold.Rows.Count > 0 && j > 0)
                            str += "<tr style=background-color:#007fff;text-align:center;><td>" + " " + "</td><td>" + " " + "</td><td>" + "" + "</td><td>" + " " + "</td><td>" + dtHold.Rows[j][0].ToString().Replace("#", " ") + "</td><td>" + dtHold.Rows[j][1].ToString() + "</td></tr>";
                        else
                            str += "<tr style=background-color:#007fff;text-align:center;><td>" + dt.Rows[i]["EmpName"].ToString() + "</td><td>" + dt.Rows[i]["EmployeeType"].ToString() + "</td><td>" + dt.Rows[i]["InTime"].ToString().Replace("#", " ") + "</td><td>" + dt.Rows[i]["OutTime"].ToString() + "</td><td>" + dtHold.Rows[j][0].ToString().Replace("#", " ") + "</td><td>" + dtHold.Rows[j][1].ToString() + "</td></tr>";
                    }
                    if (dtHold.Rows.Count == 0)
                    {
                        str += "<tr style=background-color:#007fff;text-align:center;><td>" + dt.Rows[i]["EmpName"].ToString() + "</td><td>" + dt.Rows[i]["EmployeeType"].ToString() + "</td><td>" + dt.Rows[i]["InTime"].ToString().Replace("#", " ") + "</td><td>" + dt.Rows[i]["OutTime"].ToString() + "</td></tr>";
                    }
                }
                str += "</table></span>";
            }
            else
            {
                str = "<span><table style=font-style:Consolas, Georgia; font-size: 13px;><tr style=background-color:#c3d9ff;text-align:center;><th style=width:100px;>Employee</th><th style=width:100px;> Type</th><th style=width:100px;>InTime</th><th style=width:100px;>OutTime</th></tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    str += "<tr style=text-align:center;font-style:Consolas, Georgia;font-size: 13px;><td>" + dt.Rows[i]["EmpName"].ToString() + "</td><td>" + dt.Rows[i]["EmployeeType"].ToString() + "</td><td>" + dt.Rows[i]["InTime"].ToString().Replace("#", " ") + "</td><td>" + dt.Rows[i]["OutTime"].ToString() + "</td></tr>";
                }
                str += "</table></span>";
            }
        }
        else
        {
            str = "<table style=width:100px;text-align:center;><tr><th>No Details</th></tr></table>";
        }
        return str;
    }
    protected void btnBACK_Click(object sender, EventArgs e)
    {
        if (BackTo == "GMSERVICE")
        {
            Response.Redirect("DisplayHome.aspx?Back=333", false);
        }
        else if (BackTo == "SM")
        {
            Response.Redirect("DisplayHome.aspx?Back=222", false);
        }
        else if (BackTo == "DISPLAY")
        {
            Response.Redirect("DisplayHome.aspx", false);
        }
    }
    [System.Web.Services.WebMethod(EnableSession = true)]
    public static string LoadIdleInOutTime(string RefNo)
    {
        Int64 days = 0;
        string timeInHours = "";
        DataTable dt = new DataTable();
        dt = GetInOutTime(RefNo);
        string str = "";
        if (dt.Rows.Count > 0)
        {
            str = "<span><table style='font-style:arial;border-radius:4px;'><tr style=text-align:center;></tr>";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Int64 temp = Convert.ToInt64(dt.Rows[i]["IdleFrom"].ToString());
                Int64 hr = (temp) / 60;
                if (hr > 24)
                {
                    days = (hr) / 24;
                    hr = (hr) % 24;
                }
                Int64 mint = temp % 60;
                if (days == 0)
                    timeInHours = hr + "H " + mint + "M ";
                else
                    timeInHours = days + "D " + hr + "H " + mint + "M";
                str += dt.Rows[i]["ProcessName"].ToString() + "&nbsp;" + timeInHours;
            }
            str += "</table></span>";
        }
        else
        {
            str = "<table style=width:100px;text-align:center;border-radius:4px;><tr><th>No Details</th></tr></table>";
        }
        return str;
    }
    public static DataTable GetInOutTime(string RefNo)
    {
        SqlConnection con = new SqlConnection(System.Web.HttpContext.Current.Session[HttpContext.Current.Session["TMLDealercode"] + "-TMLConString"].ToString());
        DataTable dt = new DataTable();

        SqlCommand cmd = new SqlCommand("GetPositionIdleHover", con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@RefNo", RefNo);
        if (con.State != ConnectionState.Open)
            con.Open();
        sda.Fill(dt);
        if (con.State != ConnectionState.Closed)
            con.Close();
        return dt;
    }
    protected void btnBACK_Click1(object sender, ImageClickEventArgs e)
    {
        if (BackTo == "GMSERVICE")
        {
            Response.Redirect("DisplayHome.aspx?Back=333", false);
        }
        else if (BackTo == "SM")
        {
            Response.Redirect("DisplayHome.aspx?Back=222", false);
        }
        else if (BackTo == "DISPLAY")
        {
            Response.Redirect("DisplayHome.aspx", false);
        }
    }

    public string getDailyVehiclesDelCount()
    {
        try { 
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlCommand cmd = new SqlCommand("getDailyDeliveryCount", con);
        cmd.CommandType = CommandType.StoredProcedure;
        DataTable dt = new DataTable();
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        sda.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            return dt.Rows[0]["VehiclesDeliveredToday"].ToString();
        }
        return null;
        }
        catch(Exception ex)
        {
            return null;
        }
    }
}