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


public partial class BodyshopPositionDisplay : System.Web.UI.Page
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
            //else
            //    Connection = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();

        }
        catch
        {
            Response.Redirect("login.aspx");
        }

       
        FillVehicleDashboard();
        FillVehicleIdleDashboard();
        lbVI.Text = (WA + FI + Wash + VHR).ToString();
        //lbIdle.Text = (WA + FI + Wash + VHR).ToString();

        lbTotal.Text = ((int.Parse(lbWGate.Text)) + (int.Parse(lbRO.Text)) + (int.Parse(lbWorkshop.Text)) + (int.Parse(lbWA.Text)) + (int.Parse(lbRT.Text)) + (int.Parse(lbFI.Text)) + (int.Parse(lbWash.Text)) + (int.Parse(lbVR.Text)) + (int.Parse(lbVI.Text)) + (int.Parse(lbVH.Text))).ToString();
        if (Page.Request.QueryString["Back"] != null)
        {
            btnBACK.Visible = true;
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
            btnBACK.Visible = true;
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



            for (int i = 0; i < 8; i++)
            {
                if (i == 0)
                {
                    procName = "GetVehiclePositionInfo_BodyShop";
                    title = "Front Office";
                    titleColor = "#006666";
                    ProcessName = "Gate";
                    DataTable dtVehicle = new DataTable();
                    dtVehicle = GetVehicles(procName, ProcessName);
                    lbWGate.Text = dtVehicle.Rows.Count.ToString();

                }
                else if (i == 1)
                {
                    procName = "GetVehiclePositionInfo_BodyShop";
                    title = "Dismantling";
                    titleColor = "#ff6b31";
                    ProcessName = "Dismantling";
                    DataTable dtVehicle1 = new DataTable();
                    dtVehicle1 = GetVehicles(procName, ProcessName);
                    lbRO.Text = dtVehicle1.Rows.Count.ToString();



                }

                else if (i == 2)
                {
                    procName = "GetVehiclePositionInfo_BodyShop";
                    title = "Denting";
                    titleColor = "#53a145";
                    ProcessName = "Denting";
                    DataTable dtVehicle2 = new DataTable();
                    dtVehicle2 = GetVehicles(procName, ProcessName);
                    lbWorkshop.Text = dtVehicle2.Rows.Count.ToString();


                }
                else if (i == 3)
                {
                    procName = "GetVehiclePositionInfo_BodyShop";
                    title = "Painting";
                    titleColor = "#275d8b";
                    ProcessName = "Painting";
                    dtVehicle = GetVehicles(procName, ProcessName);
                    lbWA.Text = dtVehicle.Rows.Count.ToString();


                }
                else if (i == 4)
                {
                    procName = "GetVehiclePositionInfo_BodyShop";
                    title = "Refitting";
                    titleColor = "#11a285";
                    ProcessName = "Refitting";
                    dtVehicle = GetVehicles(procName, ProcessName);
                    lbRT.Text = dtVehicle.Rows.Count.ToString();

                }
                else if (i == 5)
                {
                    procName = "GetVehiclePositionInfo_BodyShop";
                    title = "Final Inspection";
                    titleColor = "#6d4270";
                    ProcessName = "Final Inspection BodyShop";
                    dtVehicle = GetVehicles(procName, ProcessName);
                    lbFI.Text = dtVehicle.Rows.Count.ToString();


                }
                else if (i == 6)
                {
                    procName = "GetVehiclePositionInfo_BodyShop";
                    title = "Reinspection";
                    titleColor = "#d59010";
                    ProcessName = "Reinspection";
                    dtVehicle = GetVehicles(procName, ProcessName);
                    lbWash.Text = dtVehicle.Rows.Count.ToString();


                }
                else if (i == 7)
                {
                    procName = "GetVehiclePositionInfo_BodyShop";
                    title = "Vehicle Ready";
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


                Panel pnl = new Panel();
                pnl.ID = ("pnl_" + i.ToString());
                // pnl.Style.Value = "width:100%;height:340px;background-color:#FFFFFF;overflow:auto;cellspacing='0'; cellpadding='0';";
                pnl.CssClass = "PanelStyle1";
                for (int j = 0; j < totalVehicle; j++)
                {
                    Panel pnla = new Panel();
                    PositionDisplay_bodyshop vt = (PositionDisplay_bodyshop)Page.LoadControl("PositionDisplay_bodyshop.ascx");
                    int Length = dtVehicle.Rows[j]["RegNo"].ToString().Length;
                    if (dtVehicle.Rows[j]["RegNo"].ToString().Length > 10)
                        vt.RegNo = dtVehicle.Rows[j]["RegNo"].ToString().Substring(Length - 10, 10);
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
                    //vt.CWJDPImage = dtVehicle.Rows[j]["CWJDP"].ToString();



                    pnla.Controls.Add(vt);
                    pnla.Attributes.Add("onmouseover", "ShowLoadProcessInOutTime(event,'" + vt.Slno.ToString() + "','" + ProcessName + "')");
                    pnla.Attributes.Add("onmouseout", "hideTooltip(event)");
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

            case "Dismantling":
                //col = "#c52047";
                col = "#ff6b31";
                break;

            case "Denting":
                col = "#d59010";
                break;

            case "Painting":
                col = "#53a145";
                break;

            case "WA":
                col = "#275d8b";
                break;

            case "Refitting":
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



            for (int i = 0; i < 8; i++)
            {
                if (i == 0)
                {
                    procName = "GetVehicleIdlePositionInfo_BodyShop";
                    title = "Documents";
                    titleColor = "Gray";
                    ProcessName = "Documents";
                    dtVehicleI = GetVehicles(procName, ProcessName);
                    lbVH.Text = dtVehicleI.Rows.Count.ToString();

                }
                //else if (i == 1)
                //{
                //    procName = "GetVehicleIdlePositionInfo_BodyShop";
                //    title = "Insurance Approval";
                //    titleColor = "#53a145";
                //    ProcessName = "Insurance Approval";
                //    dtVehicleI = GetVehicles(procName, ProcessName);
                //    VHR = int.Parse(dtVehicleI.Rows.Count.ToString());
                //}
                //else if (i == 2)
                //{
                //    procName = "GetVehicleIdlePositionInfo_BodyShop";
                //    title = "Supplemantary Approval";
                //    titleColor = "#275d8b";
                //    ProcessName = "Supplemantary Approval";

                //    dtVehicleI = GetVehicles(procName, ProcessName);
                //    WA = int.Parse(dtVehicleI.Rows.Count.ToString());
                //}

                else if (i == 1)
                {
                    procName = "GetVehicleIdlePositionInfo_BodyShop";
                    title = "Dismantling";
                    titleColor = "#ff6b31";
                    ProcessName = "Dismantling";

                    dtVehicleI = GetVehicles(procName, ProcessName);
                    FI = int.Parse(dtVehicleI.Rows.Count.ToString());
                }
                else if (i == 2)
                {
                    procName = "GetVehicleIdlePositionInfo_BodyShop";
                    title = "Denting";
                    titleColor = "#53a145";
                    ProcessName = "Denting";
                    dtVehicleI = GetVehicles(procName, ProcessName);
                    Wash = int.Parse(dtVehicleI.Rows.Count.ToString());
                }
                else if (i == 3)
                {
                    procName = "GetVehicleIdlePositionInfo_BodyShop";
                    title = "Painting";
                    titleColor = "#275d8b";
                    ProcessName = "Painting";
                    dtVehicleI = GetVehicles(procName, ProcessName);
                    Wash = int.Parse(dtVehicleI.Rows.Count.ToString());
                }
                else if (i == 4)
                {
                    procName = "GetVehicleIdlePositionInfo_BodyShop";
                    title = "Refitting";
                    titleColor = "#11a285";
                    ProcessName = "Refitting";
                    dtVehicleI = GetVehicles(procName, ProcessName);
                    Wash = int.Parse(dtVehicleI.Rows.Count.ToString());
                }
                else if (i == 5)
                {
                    procName = "GetVehicleIdlePositionInfo_BodyShop";
                    title = "Final Inspection";
                    titleColor = "#6d4270";
                    ProcessName = "Final Inspection BodyShop";
                    dtVehicleI = GetVehicles(procName, ProcessName);
                    Wash = int.Parse(dtVehicleI.Rows.Count.ToString());
                }

                else if (i == 6)
                {
                    procName = "GetVehicleIdlePositionInfo_BodyShop";
                    title = "ReInspection";
                    titleColor = "#d59010";
                    ProcessName = "ReInspection";
                    dtVehicleI = GetVehicles(procName, ProcessName);
                    Wash = int.Parse(dtVehicleI.Rows.Count.ToString());
                }
                else if (i == 7)
                {
                    procName = "GetVehicleIdlePositionInfo_BodyShop";
                    title = "Vehicle Ready";
                    titleColor = "#95a112";
                    ProcessName = "Vehicle Ready";
                    dtVehicleI = GetVehicles(procName, ProcessName);
                    Wash = int.Parse(dtVehicleI.Rows.Count.ToString());
                }


                var cell = new HtmlTableCell();
                cell.VAlign = "Top";
                cell.Style.Value = "width:11.11%;";


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

                    PositionDisplay_bodyshop vt = (PositionDisplay_bodyshop)Page.LoadControl("PositionDisplay_bodyshop.ascx");
                    int Length = dtVehicle.Rows[j]["RegNo"].ToString().Length;
                    if (dtVehicle.Rows[j]["RegNo"].ToString().Length > 10)
                        vt.RegNo = dtVehicle.Rows[j]["RegNo"].ToString().Substring(Length - 10, 10);
                    else
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
                    //vt.CWJDPImage = dtVehicle.Rows[j]["CWJDP"].ToString();
                    //vt.Attributes.Add("onmouseover", "ShowLoadProcessInOutTime(event,'" + lblSlno.Text.ToString() + "','" + ProcessName + "')");
                    //vt.Attributes.Add("onmouseout", "hideTooltip(event)");
                    pnb.Controls.Add(vt);
                    if (ProcessName == "Vehicle Hold")
                    {
                        pnb.Attributes.Add("onmouseover", "ShowLoadProcessInOutTime(event,'" + lblSlno.Text.ToString() + "','" + ProcessName + "')");
                        pnb.Attributes.Add("onmouseout", "hideTooltip(event)");
                    }
                    else
                    {
                        pnb.Attributes.Add("onmouseover", "ShowLoadIdleInOutTime(event,'" + lblSlno.Text.ToString() + "')");
                        pnb.Attributes.Add("onmouseout", "hideTooltip(event)");
                    }
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
        SqlConnection con = new SqlConnection(System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["TMLDealercode"] + "-TMLConString"].ToString());

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
        SqlConnection con = new SqlConnection(System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["TMLDealercode"] + "-TMLConString"].ToString());

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
       // dtHold = GetRemarksHover(RefNo);
        string str = "";
        if (dt.Rows.Count > 0)
        {
            if (ProcessName == "Vehicle Ready")
            {
                str = "<span><table><tr style=background-color:#c3d9ff;text-align:center;><th style=width:100px;>Vehicle No</th><th style=width:100px;>Service Advisor</th><th style=width:100px;>PDT</th><th style=width:100px;>Ready Time</th></tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    str += "<tr style=text-align:center;><td>" + dt.Rows[i]["RegNo"].ToString() + "</td><td>" + dt.Rows[i]["EmpName"].ToString() + "</td><td>" + dt.Rows[i]["PDT"].ToString() + "</td><td>" + dt.Rows[i]["OutTime"].ToString().Replace("#", " ") + "</td></tr>";
                }
                str += "</table></span>";
            }
            else if (ProcessName == "Gate")
            {
                str = "<span><table><tr style=background-color:#c3d9ff;text-align:center;><th style=width:100px;>Vehicle No</th><th style=width:100px;>Service Advisor</th><th style=width:100px;>InTime</th><th style=width:100px;>PDT</th></tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    str += "<tr style=text-align:center;><td>" + dt.Rows[i]["RegNo"].ToString() + "</td><td>" + dt.Rows[i]["EmpName"].ToString() + "</td><td>" + dt.Rows[i]["InTime"].ToString().Replace("#", " ") + "</td><td>" + dt.Rows[i]["PDT"].ToString() + "</td></tr>";
                }
                str += "</table></span>";
            }
            //else if (ProcessName == "Vehicle Hold")
            //{
            //    str = "<span><table style=font-style:Consolas, Georgia;><tr style=text-align:center;border:0px;><th style=width:100px;font-size=10px;>Employee</th><th style=width:100px;>Employee Type</th><th style=width:100px;>InTime</th><th style=width:100px;>OutTime</th><th style=width:100px;>Time</th><th style=width:250px;>Remarks</th></tr>";
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        for (int j = 0; j < dtHold.Rows.Count; j++)
            //        {
            //            if (dtHold.Rows.Count > 0 && j > 0)
            //                str += "<tr style=background-color:#007fff;text-align:center;><td>" + " " + "</td><td>" + " " + "</td><td>" + "" + "</td><td>" + " " + "</td><td>" + dtHold.Rows[j][0].ToString().Replace("#", " ") + "</td><td>" + dtHold.Rows[j][1].ToString() + "</td></tr>";
            //            else
            //                str += "<tr style=background-color:#007fff;text-align:center;><td>" + dt.Rows[i]["EmpName"].ToString() + "</td><td>" + dt.Rows[i]["EmployeeType"].ToString() + "</td><td>" + dt.Rows[i]["InTime"].ToString().Replace("#", " ") + "</td><td>" + dt.Rows[i]["OutTime"].ToString() + "</td><td>" + dtHold.Rows[j][0].ToString().Replace("#", " ") + "</td><td>" + dtHold.Rows[j][1].ToString() + "</td></tr>";
            //        }
            //        if (dtHold.Rows.Count == 0)
            //        {
            //            str += "<tr style=background-color:#007fff;text-align:center;><td>" + dt.Rows[i]["EmpName"].ToString() + "</td><td>" + dt.Rows[i]["EmployeeType"].ToString() + "</td><td>" + dt.Rows[i]["InTime"].ToString().Replace("#", " ") + "</td><td>" + dt.Rows[i]["OutTime"].ToString() + "</td></tr>";
            //        }
            //    }
            //    str += "</table></span>";
            //}
            else
            {
                //string str = "<span><table class='mydatagrid' id=InOutPnl style=width:100%;><tr style=text-align:center;color:#a62724 !important;font-weight:bold;><td colspan=2><strong><center>" + ProcessName + "</center></strong></td></tr><tr><td style=width:90px;>In Time: </td><td style=text-align:center;>" + inTime + "</td></tr><tr><td style=width:90px;>Out Time: </td><td style=text-align:center;>" + outTime + "</td></tr></table></span>";
                str = "<span><table class='mydatagrid' id=InOutPnl style=width:100%;><tr style=background-color:#c3d9ff;text-align:center;><th style=width:100px;>InTime</th><th style=width:100px;>OutTime</th></tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    str += "<tr><td>" + dt.Rows[i]["InTime"].ToString().Replace("#", " ") + "</td><td>" + dt.Rows[i]["OutTime"].ToString() + "</td></tr>";
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
            str = "<span><table style='font-style:arial;border-radius:4px;'><tr style=text-align:center;><th style=width:100px;>Process</th><th style=width:150px;>Time</th></tr>";
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
                    timeInHours = hr + " H " + mint + " M ";
                else
                    timeInHours = days + "D " + hr + "H " + mint + "M";
                str += "<tr><td>" + dt.Rows[i]["ProcessName"].ToString() + "</td><td style=text-align:center;>" + timeInHours + "</td></tr>";
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
        SqlConnection con = new SqlConnection(System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["TMLDealercode"] + "-TMLConString"].ToString());
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


    protected void btn_logout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();
        Response.Redirect("login.aspx");
    }
}