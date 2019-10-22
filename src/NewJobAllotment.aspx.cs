using System;
using System.Data;
using System.Text;
using ASP.App_Code.Data;
using DayPilot.Web.Ui.Events.Bubble;
using DayPilot.Web.Ui.Events.Scheduler;

using AjaxControlToolkit;
using System.Configuration;
using System.Data.SqlClient;

using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web;
using CloudinaryDotNet;

public partial class NewJobAllotment : System.Web.UI.Page
{
   // Account account = new Account(
   // "deekyp5bi",
   // "215538712761161",
   //"RYfkzjckHetqxXQ1f0yV_Pbw2SM");
    int[] TLId;
    //private SqlConnection dbaseCon = new SqlConnection(ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString());
    private DataTable table;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session[Session["TMLDealercode"] + "-TMLConString"].ToString() == null || Session[Session["TMLDealercode"] + "-TMLConString"].ToString() == "")
            {
                Response.Redirect("login.aspx");
            }
         
        }
        catch
        {
            Response.Redirect("login.aspx");
        }
        lbl_currTime.Text = DateTime.Now.ToString("dd MMM yyyy, HH:mm:ss");
       
        try
        {
            if (Session["ROLE"] == null || Session["ROLE"].ToString() != "WORK MANAGER")
            {
                Response.Redirect("login.aspx");
            }
        }
        catch (Exception ex)
        {
            //Response.Redirect("login.aspx");
        }
 
        try
        {
            createTab();
            
            if (!IsPostBack)
        {
               FillVehicleDashboard();

                lbl_LoginName.Text = Session["UserId"].ToString();
            //lbl_CurrentPage.Text = "Job Allotment";
            this.Title = "Job Allotment";
            lblVersion.Text = getVersion();
            //lbScroll0.Text = Session["DealerName"].ToString();
            DayPilotScheduler1.DataSource = DbGetEvents(DayPilotScheduler1.StartDate, DayPilotScheduler1.Days);
            DayPilotScheduler1.DataBind();
            DayPilotScheduler1.CornerHtml = String.Format("<div style='padding:20px 5px 5px 20px; font-weight: bold; font-size:16px; text-align:center'>{0}</div>", "BAYS");
            DayPilotScheduler2.DataBind();
            Session["TLID"] = TLId[TabContainer1.ActiveTabIndex].ToString();
            TabContainer1.ActiveTabIndex = 0;

            LoadResources(TLId[TabContainer1.ActiveTabIndex]);
            DayPilotScheduler1.StartDate = DateTime.Today;
            DayPilotScheduler1.Days = 1;
            LoadResources1(TLId[TabContainer1.ActiveTabIndex]);
            DayPilotScheduler1.SetScrollX(DateTime.Now);
            DayPilotScheduler2.StartDate = DateTime.Today;
            DayPilotScheduler2.Days = 1;
            DayPilotScheduler2.DataSource = DbGetEventsTechnicianwise(DayPilotScheduler2.StartDate, DayPilotScheduler2.Days);
            DayPilotScheduler2.DataBind();
            DayPilotScheduler2.CornerHtml = String.Format("<div style='padding:20px 5px 5px; font-weight: bold; font-size:16px; text-align:center'>{0}</div>", "TECHNICIANS");
            DayPilotScheduler2.SetScrollX(DateTime.Now);
        }
    }
        catch (Exception ex)
        {

        }
        
    }

    public string getVersion()
    {
        string version = "";
       // string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlDataAdapter sda = new SqlDataAdapter("select top 1 * from ProductVersion order by DTM desc", con);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        if (dt.Rows.Count > 0)
            version = dt.Rows[0][0].ToString();
        return version;
    }

    //tabs
    private void createTab()
    {
        try
        {
            SqlConnection dbaseCon = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
            SqlCommand cmd = new SqlCommand("SelectTeamLeadForAllotment", dbaseCon);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            TLId = new int[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TabPanel tbpnlDynamic = new TabPanel();
                tbpnlDynamic.HeaderText = dt.Rows[i]["EmpName"].ToString();
                tbpnlDynamic.ID = dt.Rows[i]["EmpId"].ToString();
                TLId[i] = Convert.ToInt16(dt.Rows[i]["EmpId"]);
                TabContainer1.Tabs.Add(tbpnlDynamic);
                TabContainer1.ActiveTabIndex = 0;
            }
       }
        catch (Exception ex)
        {

        }
        finally
        {

        }

    }
    protected void btn_Back_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("JCHome.aspx");
    }
    protected void btn_logout_Click(object sender, EventArgs e)
    {
        try
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("login.aspx");
        }
        catch (Exception ex)
        {
        }
    }

    public void FillVehicleDashboard()
    {
        try
        {
            external1.Controls.Clear();
            string procName = "";
            procName = "GetRegTagList_NEW";
           
            var cell = new HtmlTableCell();
            cell.VAlign = "Top";
            cell.Style.Value = "width:11.11%;";
            DataTable dtVehicle = new DataTable();
            dtVehicle = Getvehicle(procName);
            int totalVehicle = dtVehicle.Rows.Count;
            Panel pnl = new Panel();
            pnl.ID = ("pnl_" + ToString());
            Label lbl = new Label();
            for (int j = 0; j < totalVehicle; j++)
            {
                Panel pnla = new Panel();
                AllotmentVehicles vt = (AllotmentVehicles)Page.LoadControl("AllotmentVehicles.ascx");
                vt.RegNo = dtVehicle.Rows[j]["Reg No"].ToString();
                vt.Slno = dtVehicle.Rows[j]["Slno"].ToString();
                vt.ServiceAdvisor = dtVehicle.Rows[j]["SA"].ToString();
                vt.Model = dtVehicle.Rows[j]["Tag No"].ToString();
                vt.VehicleImage = DataManager.car_image(dtVehicle.Rows[j]["ModelImageUrl"].ToString());
                if (dtVehicle.Rows[j]["PDT"].ToString().Contains("#"))
                {
                    vt.VehicleColor = "Orange";
                    vt.PDT = dtVehicle.Rows[j]["PDT"].ToString().Split('#')[0];
                }
                else
                    vt.PDT = dtVehicle.Rows[j]["PDT"].ToString();
                vt.PDTStatus = DataManager.jcr_image(dtVehicle.Rows[j]["PDTStatus"].ToString());
                vt.CWJDP = DataManager.jcr_image(dtVehicle.Rows[j]["CWJDP"].ToString());
                pnla.Controls.Add(vt);
                pnl.Controls.Add(pnla);
            }
            cell.Controls.Add(lbl);
            cell.Controls.Add(pnl);
            external1.Controls.Add(cell);

        }
        catch (Exception ex)
        {
           // Console.WriteLine(ex.Message);
        }
    }

   

    private DataTable Getvehicle(string procedureName)
    {
        SqlConnection dbaseCon = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        try
        {
            if (dbaseCon.State == ConnectionState.Closed)
                dbaseCon.Open();
            SqlDataAdapter da = new SqlDataAdapter(procedureName, dbaseCon);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            if (dbaseCon.State == ConnectionState.Open)
                dbaseCon.Close();
        }
    }

    private DataTable DbGetEvents(DateTime start, int days)
    {
        SqlConnection dbaseCon = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlCommand cmd = new SqlCommand("udpGetJobAllotment", dbaseCon);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Date", start);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        return dt;
    }
    private DataTable DbGetEventsTechnicianwise(DateTime start, int days)
    {
        SqlConnection dbaseCon = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlCommand cmd = new SqlCommand("udpGetJobAllotment_Technicianwise", dbaseCon);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Date",start);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        return dt;
    }

    private void LoadResources(int TLID)
    {
        SqlConnection dbaseCon = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        DayPilotScheduler1.Resources.Clear();
        SqlCommand cmd = new SqlCommand("GetBays_TLWise", dbaseCon);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@TLID", TLID);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        foreach (DataRow r1 in dt.Rows)
        {
            string name = (string)r1["name"];
            string id = Convert.ToString(r1["id"]);
            DayPilotScheduler1.Resources.Add(name, id);
        }
    }
    private void LoadResources1(int TLID)
    {
        SqlConnection dbaseCon = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        DayPilotScheduler2.Resources.Clear();
        SqlCommand cmd2 = new SqlCommand("GetEmployees_TLWise", dbaseCon);
        cmd2.CommandType = CommandType.StoredProcedure;
        cmd2.Parameters.AddWithValue("@TLID", TLID);
        cmd2.Parameters.AddWithValue("@AllotDate", DateTime.Now.ToString());
        SqlDataAdapter da1 = new SqlDataAdapter(cmd2);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);
        foreach (DataRow r2 in dt1.Rows)
        {
            string name = (string)r2["name"];
            string id = Convert.ToString(r2["id"]);
            DayPilotScheduler2.Resources.Add(name, id);
        }
    }

    protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
    {
        try
        {
            Session["TLID"] = TLId[TabContainer1.ActiveTabIndex].ToString();
            LoadResources1(TLId[TabContainer1.ActiveTabIndex]);
            LoadResources(TLId[TabContainer1.ActiveTabIndex]);
            setDataSourceAndBind();
            FillVehicleDashboard();

        }
        catch (Exception ex)
        {
        }
    }
  
 
    private void setDataSourceAndBind()
    {
        string filter = (string)DayPilotScheduler1.ClientState["filter"];
        DayPilotScheduler1.DataSource = DbGetEvents(DayPilotScheduler1.StartDate, DayPilotScheduler1.Days);
        DayPilotScheduler1.DataBind();
        DayPilotScheduler1.Update();
     
        DayPilotScheduler2.DataSource = DbGetEventsTechnicianwise(DayPilotScheduler2.StartDate, DayPilotScheduler2.Days);
        DayPilotScheduler2.DataBind();
        DayPilotScheduler2.Update();
    }



    protected void DayPilotScheduler1_EventMove(object sender, DayPilot.Web.Ui.Events.EventMoveEventArgs e)
    {
        if (e.OldStart >= DateTime.Now && e.NewEnd >= DateTime.Now)
        {
            string id = e.Value;
            DateTime start = e.NewStart;
            DateTime end = e.NewEnd;
            string resource = e.NewResource;
            DbUpdateEvent(id, start, end);
            DayPilotScheduler1.DataSource = DbGetEvents(DayPilotScheduler1.StartDate, DayPilotScheduler1.Days);
            DayPilotScheduler1.DataBind();
            DayPilotScheduler1.UpdateWithMessage("Event moved.");
        }
        else
        {
            DayPilotScheduler1.DataSource = DbGetEvents(DayPilotScheduler1.StartDate, DayPilotScheduler1.Days);
            DayPilotScheduler1.DataBind();
            DayPilotScheduler1.UpdateWithMessage("TimeOut!!! Event cannot be moved.");
        }

    }

    private void DbUpdateEvent(string id, DateTime start, DateTime end)
    {
        using (SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString()))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE [tblJobAllotment] SET InTime = @start, OutTime = @end WHERE id = @id", con);
            cmd.Parameters.AddWithValue("id", id);
            cmd.Parameters.AddWithValue("start", start);
            cmd.Parameters.AddWithValue("end", end);
            cmd.ExecuteNonQuery();
            DayPilotScheduler1.DataBind();
            DayPilotScheduler1.Update();
        }
    }


    protected void DayPilotScheduler1_EventResize(object sender, DayPilot.Web.Ui.Events.EventResizeEventArgs e)
    {

        if ((e.OldStart < DateTime.Now && e.OldEnd < DateTime.Now) || (e.NewStart < DateTime.Now && e.NewEnd < DateTime.Now))
        {
            DayPilotScheduler1.UpdateWithMessage("TimeOut!!! Event cannot be Resized");
        }
        else if ((e.OldStart < DateTime.Now && e.OldEnd >= DateTime.Now) || (e.NewStart < DateTime.Now && e.NewEnd >= DateTime.Now))
        {
            if (e.OldStart != e.NewStart)
            {
                DayPilotScheduler1.UpdateWithMessage("TimeOut!!! Event cannot be Resized!");

            }
            else {


                using (SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString()))
                {
                    try
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("ResheduleTblJobAllotment", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@AllotID", e.Id);
                        cmd.Parameters.AddWithValue("@BayID", e.Resource);
                        cmd.Parameters.AddWithValue("@InTime", e.OldStart);
                        cmd.Parameters.AddWithValue("@OutTime", e.NewEnd);
                        cmd.Parameters.Add("@flag", SqlDbType.Int);
                        cmd.Parameters["@flag"].Direction = ParameterDirection.Output;
                        cmd.Parameters["@flag"].Value = 0;
                        if (con.State != ConnectionState.Open)
                            con.Open();
                        cmd.ExecuteNonQuery();
                        string msg = Convert.ToString(cmd.Parameters["@flag"].Value);

                        switch (msg)
                        {
                            case "0":
                                DayPilotScheduler1.UpdateWithMessage("Error in writing to database !");
                                break;

                            case "1":
                                DayPilotScheduler1.UpdateWithMessage("Allotment Rescheduled !");
                                break;

                            case "2":
                                DayPilotScheduler1.UpdateWithMessage("Technician not available !");
                                break;

                            case "3":
                                DayPilotScheduler1.UpdateWithMessage("Bay not available !");
                                break;

                            case "4":
                                DayPilotScheduler1.UpdateWithMessage("Vehicle is already assign to another Bay !");
                                break;

                            case "5":
                                DayPilotScheduler1.UpdateWithMessage("Promise Delivery Time is near to cross or already crossed !");
                                break;

                            case "7":
                                DayPilotScheduler1.UpdateWithMessage("Allotment In-Time is not Valid. !");
                                break;

                            default:
                                break;
                        }

                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }
        else
        {
            using (SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString()))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("ResheduleTblJobAllotment", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AllotID", e.Id);
                    cmd.Parameters.AddWithValue("@BayID", e.Resource);
                    if (e.OldStart != e.NewStart)
                        cmd.Parameters.AddWithValue("@InTime", e.NewStart);
                    else
                        cmd.Parameters.AddWithValue("@InTime", e.OldStart);
                    if (e.OldEnd != e.NewEnd)
                        cmd.Parameters.AddWithValue("@OutTime", e.NewEnd);
                    else
                        cmd.Parameters.AddWithValue("@OutTime", e.OldEnd);
                    cmd.Parameters.Add("@flag", SqlDbType.Int);
                    cmd.Parameters["@flag"].Direction = ParameterDirection.Output;
                    cmd.Parameters["@flag"].Value = 0;
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    cmd.ExecuteNonQuery();
                    string msg = Convert.ToString(cmd.Parameters["@flag"].Value);

                    switch (msg)
                    {
                        case "0":
                            DayPilotScheduler1.UpdateWithMessage("Error in writing to database !");
                            break;

                        case "1":
                            DayPilotScheduler1.UpdateWithMessage("Allotment Rescheduled !");
                            table.AcceptChanges();
                            break;

                        case "2":
                            DayPilotScheduler1.UpdateWithMessage("Technician not available !");
                            break;

                        case "3":
                            DayPilotScheduler1.UpdateWithMessage("Bay not available !");
                            break;

                        case "4":
                            DayPilotScheduler1.UpdateWithMessage("Vehicle is already assign to another Bay !");
                            break;

                        case "5":
                            DayPilotScheduler1.UpdateWithMessage("Promise Delivery Time is near to cross or already crossed !");
                            break;

                        case "7":
                            DayPilotScheduler1.UpdateWithMessage("Allotment In-Time is not Valid. !");
                            break;

                        default:
                            break;
                    }

                }
                catch (Exception ex)
                {

                }
            }
        }
        DayPilotScheduler1.Update();
    }

    private DataTable getAllotVehicleData(string id)
    {
        SqlConnection dbaseCon = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        try
        {

            SqlCommand cmd = new SqlCommand("UdpGetAllotmentHover", dbaseCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AllotId", id);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da1.Fill(dt);
            return dt;

        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        finally
        {

        }
    }

    protected void DayPilotBubble1_RenderEventBubble(object sender, RenderEventBubbleEventArgs e)
    {
        DataTable dt = getAllotVehicleData(e.Value);
        StringBuilder sb = new StringBuilder();
        sb.AppendFormat("VIN/VRN: {0}<br />", dt.Rows[0]["RegNo"].ToString());
        sb.AppendFormat("Emp Name: {0}<br />", dt.Rows[0]["EmpName"].ToString());
        sb.AppendFormat("In Time: {0}<br />", e.Start);
        sb.AppendFormat("Out Time: {0}<br />", e.End);
        sb.AppendFormat("Allot Time: {0}<br />", dt.Rows[0]["AllotTime"].ToString());
        sb.AppendFormat("PDT: {0}<br />", dt.Rows[0]["PromisedTime"].ToString());
        sb.AppendFormat("<br />");
        e.InnerHTML = sb.ToString();

    }
    protected void DayPilotScheduler1_BeforeEventRender(object sender, BeforeEventRenderEventArgs e)
    {
        DataTable dt = getAllotVehicleData(e.Id);
        StringBuilder sb = new StringBuilder();
        //e.CssClass = "special";
        e.BackgroundColor = "#DEE0E4";
        e.BorderColor = "5px solid #004F92";
        e.FontColor = "#004F92";
        e.Html = "<span style='font-size: 11px;font-family: tahoma, arial, Verdana;'>" + e.Html + "&nbsp;&nbsp;|&nbsp;&nbsp;" + e.Start.ToString("hh:mm tt") + "-" + e.End.ToString("hh:mm tt") + "&nbsp;&nbsp;|&nbsp;&nbsp;" + sb.AppendFormat(dt.Rows[0]["EmpName"].ToString()) + "</span>";
    }
    protected void DayPilotBubble1_RenderContent(object sender, RenderEventArgs e)
    {
        if (e is RenderResourceBubbleEventArgs)
        {
            RenderResourceBubbleEventArgs re = e as RenderResourceBubbleEventArgs;
            e.InnerHTML = "<b>Resource header details</b><br />Value: " + re.ResourceId;
        }
        else if (e is RenderCellBubbleEventArgs)
        {
            RenderCellBubbleEventArgs re = e as RenderCellBubbleEventArgs;
            e.InnerHTML = "<b>Cell details</b><br />Resource:" + re.ResourceId + "<br />From:" + re.Start + "<br />To: " + re.End;
        }
    }

    protected void DayPilotScheduler1_Command(object sender, DayPilot.Web.Ui.Events.CommandEventArgs e)
    {
        switch (e.Command)
        {
            case "navigate":
                DayPilotScheduler1.StartDate = (DateTime)e.Data["day"];
                DayPilotScheduler1.DataBind();
                DayPilotScheduler1.Update();
                break;
            case "refresh":
                setDataSourceAndBind();
                DayPilotScheduler1.DataBind();
                DayPilotScheduler1.Update();

                setDataSourceAndBind();
                DayPilotScheduler2.DataBind();
                DayPilotScheduler2.Update();
              
                break;
            case "previous":
                DayPilotScheduler1.StartDate = DayPilotScheduler1.StartDate.AddDays(-1);
                DayPilotScheduler1.DataBind();
                DayPilotScheduler1.Update();

                DayPilotScheduler2.StartDate = DayPilotScheduler2.StartDate.AddDays(-1);
                DayPilotScheduler2.DataBind();
                DayPilotScheduler2.Update();
                setDataSourceAndBind();
                break;

            case "next":
                DayPilotScheduler1.StartDate = DayPilotScheduler1.StartDate.AddDays(1);
                DayPilotScheduler1.DataBind();
                DayPilotScheduler1.Update();

                DayPilotScheduler2.StartDate = DayPilotScheduler2.StartDate.AddDays(1);
                DayPilotScheduler2.DataBind();
                DayPilotScheduler2.Update();
                setDataSourceAndBind();
                break;
        }
        setDataSourceAndBind();
        DayPilotScheduler1.DataBind();
        DayPilotScheduler1.Update();
        DayPilotScheduler2.DataBind();
        DayPilotScheduler2.Update();
    }


    //private void initData()
    //{

    //    if (Session[PageHash] == null)
    //    {
    //        Session[PageHash] = DataGeneratorScheduler.GetData();
    //    }
    //    table = (DataTable)Session[PageHash];
    //}


    protected string PageHash
    {
        get
        {
            return Hash.ForPage(this);
        }
    }

    protected void btn_jcrdisplay_Click(object sender, EventArgs e)
    {
        Response.Redirect("JCRDisplayWorks.aspx");
    }

    protected void btn_kpiDashboard_Click(object sender, EventArgs e)
    {
        Response.Redirect("KPIDashboard.aspx");
    }

    protected void back_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("JCRDisplayWorks.aspx");
    }
}
