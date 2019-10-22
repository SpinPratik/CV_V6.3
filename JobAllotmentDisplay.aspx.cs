using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JobAllotmentDisplay : System.Web.UI.Page
{
    private int flag = 0;
    private static DateTime StTime;
    private static DateTime EdTime;
    private static string allotId = "";
    private DataTable BayColors;
    //private SqlConnection con = new SqlConnection(Session["ConnectionString"].ToString());
    private static int rowSelected = 0;
    private static string backto = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["ConnectionString"].ToString() == null || Session["ConnectionString"].ToString() == "")
                Response.Redirect("login.aspx");

        }
        catch
        {
            Response.Redirect("login.aspx");
        }
        //this.Title = "Job Allotment Display";
        DS_Shift.ConnectionString = Session["ConnectionString"].ToString();
        SqlDataSource1.ConnectionString = Session["ConnectionString"].ToString();
        try
        {
            if (!Page.IsPostBack)
            {
                GetTeamLead();
                txtDate.Text = DateTime.Now.ToShortDateString();

                Session["CURRENT_PAGE"] = "Job Allotment Display";
            }
            
            RefreshGrid(RadioTypes.SelectedValue.ToString());
            timeLine.Style.Add("table-layout", "fixed");
            timeLine.Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
            if (Request.QueryString.Count >= 1)
            {
                string id;
                try
                {
                    id = Request.QueryString["id"].ToString();
                    if (Request.QueryString["typ"].ToString() == "Delete")
                    {
                        Response.Write("<script>confirm('Do you want to delete')</script>");
                        DeleteAppointment(id);
                        Response.Write(id);
                        Response.Redirect("JobAllotment.aspx");
                    }
                }
                catch
                {
                    Response.Write("<script>windows.alert('Cannot Delete')</script>");
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void FillBayColors()
    {
        SqlConnection con = new SqlConnection(Session["ConnectionString"].ToString());
        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            BayColors = new DataTable();
            BayColors.Clear();
            SqlCommand cmd = new SqlCommand("BayColors", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(BayColors);
        }
        catch (Exception ex) { }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
    }

    protected void RadioType_SelectedIndexChanged(object sender, EventArgs e)
    {
        RefreshGrid(RadioTypes.SelectedValue.ToString());
    }

    private static DataTable GetEmp(string EmpId)
    {
        DataTable dt = new DataTable();
        SqlConnection con = new SqlConnection(System.Web.HttpContext.Current.Session["ConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "udpGetEmpDetails";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@EmpId", EmpId);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        sda.Fill(dt);
        return dt;
    }

    protected DataTable GetDetails(string Refid)
    {
        SqlConnection con = new SqlConnection(System.Web.HttpContext.Current.Session["ConnectionString"].ToString());
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "udpGetAllotDetails";
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SlNo", Refid);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dtProcess = new DataTable();
            sda.Fill(dtProcess);
            return dtProcess;
        }
        catch (Exception ex)
        {
            return null;
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }
    protected void GetTeamLead()
    {
        ddlTeam.Items.Clear();
        SqlConnection con = new SqlConnection(Session["ConnectionString"].ToString());
        SqlCommand cmd1 = new SqlCommand("SelectTeamLead", con);
        cmd1.CommandType = CommandType.Text;
        SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        if (con.State != ConnectionState.Open)
            con.Open();
        sda1.Fill(dt1);
        con.Close();
       // ddlTeam.Items.Add(new ListItem("TeamLead"));
        if (dt1.Rows.Count > 0)
        {

            ddlTeam.DataSource = dt1;
            ddlTeam.DataTextField = "EmpName";
            ddlTeam.DataValueField = "EmpId";
            ddlTeam.DataBind();
        }
    }

    protected DataTable GetProcessStatus(string Refid)
    {
        SqlConnection con = new SqlConnection(System.Web.HttpContext.Current.Session["ConnectionString"].ToString());
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "udpGetProcessStatus";
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Refid", Refid);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dtProcess = new DataTable();
            if (con.State != ConnectionState.Open)
                con.Open();
            sda.Fill(dtProcess);
            return dtProcess;
        }
        catch (Exception ex)
        {
            return null;
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }

    protected DataTable GetAllotedTime(string AllotNo)
    {
        SqlConnection con = new SqlConnection(System.Web.HttpContext.Current.Session["ConnectionString"].ToString());
        try
        {
            SqlDataAdapter sda = new SqlDataAdapter("GetAllotmentTime", con);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.AddWithValue("@AllotId", allotId);
            DataTable dt = new DataTable();
            if (con.State != ConnectionState.Open)
                con.Open();
            sda.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }

    private static DataTable SrchDt;

    protected void getDets(int selrow)
    {
        try
        {
            timeGrid.Rows[selrow][0].ToString();
        }
        catch (Exception ex)
        {
        }
    }

    protected void getWorkTime(string shiftID)
    {
        SqlConnection con = new SqlConnection(Session["ConnectionString"].ToString());
        try
        {
            SqlDataAdapter sda = new SqlDataAdapter("udpGetWorkTime", con);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.AddWithValue("@ShiftID", shiftID);

            DataTable dt = new DataTable();
            if (con.State != ConnectionState.Open)
                con.Open();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                StTime = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + dt.Rows[0]["InTime"].ToString());
                EdTime = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + dt.Rows[0]["OutTime"].ToString());
            }
            RefreshGrid(RadioTypes.SelectedValue.ToString());
        }
        catch (Exception ex)
        {
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }

    private bool CheckTime(DateTime InTime, DateTime OutTime, String EmpId, string InsUpd)
    {
        SqlConnection con = new SqlConnection(Session["ConnectionString"].ToString());
        try
        {
            SqlCommand cmd = new SqlCommand("", con);
            Object o;
            cmd.CommandText = "CheckEmployeeAvailable";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@InTime", InTime);
            cmd.Parameters.AddWithValue("@OutTime", OutTime);
            cmd.Parameters.AddWithValue("@EmpID", EmpId);
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                if (InsUpd == "INS")
                {
                    o = cmd.ExecuteScalar();
                    if (o == null)
                        return true;
                    else
                        return false;
                }
                else if (InsUpd == "UPD")
                {
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count == 1)
                    {
                        if (dt.Rows[0][0].ToString().Trim() == allotId.ToString())
                            return true;
                        else
                            return false;
                    }
                    else
                        return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    private bool CheckVehicleOnOtherBay(DateTime InTime, DateTime OutTime, String BayId, String RefNo)
    {
        SqlConnection con = new SqlConnection(Session["ConnectionString"].ToString());
        try
        {
            SqlCommand cmd = new SqlCommand("", con);
            Object o;
            cmd.CommandText = "GetVehicleBay";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@InTime", InTime);
            cmd.Parameters.AddWithValue("@OutTime", OutTime);
            cmd.Parameters.AddWithValue("@BayID", BayId);
            cmd.Parameters.AddWithValue("@SlNo", RefNo);
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                o = cmd.ExecuteScalar();
                if (o == null)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    private bool CheckBay(DateTime InTime, DateTime OutTime, String BayId, String RefNo)
    {
        SqlConnection con = new SqlConnection(Session["ConnectionString"].ToString());
        try
        {
            SqlCommand cmd = new SqlCommand("", con);
            Object o;
            cmd.CommandText = "CheckBayAvailable";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@InTime", InTime);
            cmd.Parameters.AddWithValue("@OutTime", OutTime);
            cmd.Parameters.AddWithValue("@BayID", BayId);
            cmd.Parameters.AddWithValue("@RefNo", RefNo);
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                o = cmd.ExecuteScalar();
                if (o == null)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    private void InsertAppointment(String EmpId, DateTime InTime, DateTime OutTime, String RefNo, DateTime DT)
    {
        SqlConnection con = new SqlConnection(Session["ConnectionString"].ToString());
        try
        {
            SqlCommand cmd = new SqlCommand("", con);
            cmd.CommandText = "udpInsertAppointment";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@EmpId", EmpId);
            cmd.Parameters.AddWithValue("@InTime", InTime);
            cmd.Parameters.AddWithValue("@OutTime", OutTime);
            cmd.Parameters.AddWithValue("@RefNo", RefNo);
            cmd.Parameters.AddWithValue("@DT", DT);
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
        }
        catch (Exception ex)
        {
        }
    }

    private DataTable timeGrid = new DataTable();
    private DataTable timeGridBay = new DataTable();

    private void RefreshGrid(string TeamLeadBay)
    {
        SqlConnection con = new SqlConnection(Session["ConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter sda = new SqlDataAdapter();
        timeGrid = new DataTable();
        if (TeamLeadBay == "0")
        {
            try
            {
                timeLine.DataSource = null;
                timeGrid.Clear();
                string selType;
                selType = "";
                cmd = new SqlCommand("GetJobAllotmentTime_Display", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AllotDate", txtDate.Text.Trim());
                cmd.Parameters.AddWithValue("@EmpType", selType);
                if (ddlShift.SelectedValue.ToString()=="")
                    cmd.Parameters.AddWithValue("@ShiftId", 1);
                else
                 cmd.Parameters.AddWithValue("@ShiftId", ddlShift.SelectedValue.ToString());
                if (ddlTeam.SelectedValue.ToString()=="TeamLead")
                    cmd.Parameters.AddWithValue("@TeamLeadId", "");
                else
                cmd.Parameters.AddWithValue("@TeamLeadId", ddlTeam.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@FloorName", "All");
                sda = new SqlDataAdapter(cmd);
                sda.Fill(timeGrid);
                int k = timeGrid.Rows.Count;
                timeLine.DataSource = timeGrid;
                timeLineHeader.DataSource = timeGrid;
                timeLineHeader.DataBind();
                timeLine.DataBind();
            }
            catch (Exception ex)
            {
                timeLineHeader.DataSource = null;
                timeLineHeader.DataBind();
                timeLine.DataSource = null;
                timeLine.DataBind();
            }
        }
        else if (TeamLeadBay == "1")
        {
            try
            {
                timeLine.DataSource = null;
                timeGridBay.Clear();
                string selType;
                selType = "";
                cmd = new SqlCommand("GetJobAllotmentTimeBay_Display", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AllotDate", txtDate.Text.Trim());
                cmd.Parameters.AddWithValue("@BayId", selType);
                cmd.Parameters.AddWithValue("@ShiftId", 1);
                cmd.Parameters.AddWithValue("@FloorName", "ALL");
                //cmd.Parameters.AddWithValue("@TLId", ddlTeam.SelectedValue.ToString());
                if (ddlTeam.SelectedValue.ToString() == "TeamLead")
                    cmd.Parameters.AddWithValue("@TLId", "");
                else
                    cmd.Parameters.AddWithValue("@TLId", ddlTeam.SelectedValue.ToString());
                sda = new SqlDataAdapter(cmd);
                sda.Fill(timeGridBay);
                timeLine.DataSource = timeGridBay;
                timeLineHeader.DataSource = timeGridBay;
                timeLineHeader.DataBind();
                timeLine.DataBind();
            }
            catch (Exception ex)
            {
                timeLineHeader.DataSource = null;
                timeLineHeader.DataBind();
                timeLine.DataSource = null;
                timeLine.DataBind();
            }
        }
    }

    protected void TxtDate_TextChanged(object sender, EventArgs e)
    {
        RefreshGrid(RadioTypes.SelectedValue.ToString());
    }

    private void DeleteAppointment(String ID)
    {
        SqlConnection con = new SqlConnection(Session["ConnectionString"].ToString());
        try
        {
            SqlCommand cmd = new SqlCommand("", con);
            cmd.CommandText = "DeleteTblJobAllotment";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@AllotID", ID);
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                    con.Close();
                allotId = "0";
            }
            RefreshGrid(RadioTypes.SelectedValue.ToString());
        }
        catch (Exception ex)
        {
        }
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

    public static DataTable GetvehDetailsForBayHover(int RefNo)
    {
        SqlConnection con = new SqlConnection(System.Web.HttpContext.Current.Session["ConnectionString"].ToString());
        string cmdstr = "udpGetVehDetailsForHovering";
        SqlCommand cmd = new SqlCommand(cmdstr, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@SlNo", RefNo);
        DataTable dt = new DataTable();
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        sda.Fill(dt);
        return dt;
    }

    protected void btnBACK_Click(object sender, EventArgs e)
    {
        try
        {
            if (backto == "GMSERVICE")
            {
                Response.Redirect("DisplayHome.aspx?Back=333", false);
            }
            else if (backto == "SM")
            {
                Response.Redirect("DisplayHome.aspx?Back=222", false);
            }
            else if (backto == "DISPLAY")
            {
                Response.Redirect("DisplayHome.aspx", false);
            }
            else
            {
                Session["CURRENT_PAGE"] = null;
                Response.Redirect("JCHome.aspx");
            }
        }
        catch (Exception ex)
        {
        }
    }

    public static string GetpromisedTime(int RefNo)
    {
        string Promisedtime = string.Empty;
        SqlConnection con = new SqlConnection(System.Web.HttpContext.Current.Session["ConnectionString"].ToString());
        string cmdstr = "UdpGetPDTForHovering";
        SqlCommand cmd = new SqlCommand(cmdstr, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@SlNo", RefNo);
        DataTable dt = new DataTable();
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        try
        {
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Promisedtime = dt.Rows[0][0].ToString();
            }
            return Promisedtime;
        }
        catch (Exception ex)
        {
            return string.Empty;
        }
    }

    protected void timeLine_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (RadioTypes.SelectedValue.ToString() == "0")
        {
            if (e.Row.RowIndex > -1)
            {
                e.Row.Height = 20;
            }
            int allots = 0;
            e.Row.Cells[1].Width = new Unit(30);
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[4].Visible = false;
                e.Row.Cells[e.Row.Cells.Count - 1].Visible = false;
                e.Row.Cells[e.Row.Cells.Count - 5].Visible = false;
                GridViewRow gvr = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
                TableCell cell = new TableCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.Text = "EMPLOYEE";
                gvr.Cells.Add(cell);
                for (int i = 5; i < e.Row.Cells.Count - 5; i++)
                {
                    int colsp = 0;
                    for (int j = 5; j < e.Row.Cells.Count - 5; j++)
                    {
                        if (e.Row.Cells[i].Text.Split(':').GetValue(0).ToString() == e.Row.Cells[j].Text.Split(':').GetValue(0).ToString())
                        {
                            e.Row.Cells[e.Row.Cells.Count - 4].Attributes.Add("Style", "width:5px;");
                            colsp++;
                        }
                    }
                    cell = new TableCell();
                    cell.ColumnSpan = colsp;
                    cell.HorizontalAlign = HorizontalAlign.Center;
                    cell.Text = e.Row.Cells[i].Text.Split(':').GetValue(0).ToString();
                    string str = DateTime.Now.ToString("hh");
                    if (Int32.Parse(cell.Text) == Int32.Parse(str))
                    {
                        cell.BackColor = System.Drawing.Color.DarkOrange;
                    }
                    gvr.Cells.Add(cell);
                    i = i + colsp - 1;
                }

                cell = new TableCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.Text = "JOBS";
                cell.Font.Size = FontUnit.Point(7);
                cell.ToolTip = "No of Jobs";
                gvr.Cells.Add(cell);
                cell = new TableCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.Text = "CARS";
                cell.Font.Size = FontUnit.Point(7);
                cell.ToolTip = "No of Cars";
                gvr.Cells.Add(cell);
                cell = new TableCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.Text = "TTA(m)";
                cell.Font.Size = FontUnit.Point(7);
                cell.ToolTip = "Total Time Alloted (min.)";
                gvr.Cells.Add(cell);
                timeLineHeader.Controls[0].Controls.AddAt(0, gvr);
                DateTime dt = DateTime.Parse(txtDate.Text);
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    if (i > 4 && i < e.Row.Cells.Count - 5)
                    {
                        string str = e.Row.Cells[i].Text.Substring(0, 6) + " " + e.Row.Cells[i].Text.Substring(6);
                        DateTime datt = DateTime.Parse(dt.Date.ToShortDateString() + " " + str);
                        if (DateTime.Compare(DateTime.Parse(datt.ToString("HH:mm")), DateTime.Parse(DateTime.Now.ToString("HH:mm"))) <= 0 && DateTime.Parse(dt.Date.ToShortDateString()) == DateTime.Parse(DateTime.Now.ToShortDateString()))
                        {
                            e.Row.Cells[i].BackColor = System.Drawing.Color.DarkOrange;
                        }
                        else if (DateTime.Compare(DateTime.Parse(DateTime.Now.ToShortDateString()), DateTime.Parse(txtDate.Text.ToString())) > 0)
                        {
                            e.Row.Cells[i].BackColor = System.Drawing.Color.DarkOrange;
                        }
                    }
                    e.Row.Cells[i].Text = "";
                }
                e.Row.Cells[e.Row.Cells.Count - 4].Attributes.Add("style", "width:20px;");
                e.Row.Cells[e.Row.Cells.Count - 3].Attributes.Add("style", "width:20px;");
                e.Row.Cells[e.Row.Cells.Count - 2].Attributes.Add("style", "width:20px;");
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[0].Text != "")
                {
                    e.Row.Cells[1].ToolTip = e.Row.Cells[1].Text.Split('#')[1].Trim();
                    e.Row.Cells[1].Text = e.Row.Cells[1].Text.Split('#')[0].Trim();
                    e.Row.Cells[1].Width = new Unit(85);
                    e.Row.Cells[1].Font.Size = new FontUnit(8);
                    e.Row.Cells[1].Wrap = true;
                    for (int i = 4; i < e.Row.Cells.Count - 4; i++)
                    {
                        if (e.Row.Cells[e.Row.Cells.Count - 1].Text == "1")
                        {
                            if (DateTime.Compare(DateTime.Parse(txtDate.Text.Trim()), DateTime.Now) <= 0)
                                e.Row.Cells[i].Attributes.Add("OnClick", "Javascript:__doPostBack('myClick','" + (e.Row.RowIndex.ToString() + "," + i.ToString()) + "');");
                            e.Row.Cells[e.Row.Cells.Count - 4].Attributes.Add("Style", "width:60px;text-align:center;");
                            e.Row.Cells[e.Row.Cells.Count - 3].Attributes.Add("Style", "width:60px;text-align:center;");
                            e.Row.Cells[e.Row.Cells.Count - 2].Attributes.Add("Style", "width:50px;text-align:center;");
                        }
                        else
                        {
                            e.Row.Cells[i].Attributes.Add("OnClick", "Javascript:__doPostBack('myClick','" + (e.Row.RowIndex.ToString() + "," + i.ToString()) + "');");
                            e.Row.Cells[1].Attributes.Add("Style", "color:BROWN;border-color:#666699;");
                            e.Row.Cells[e.Row.Cells.Count - 4].Attributes.Add("Style", "width:60px;text-align:center;color:BROWN;border-color:#666699;");
                            e.Row.Cells[e.Row.Cells.Count - 3].Attributes.Add("Style", "width:60px;text-align:center;color:BROWN;border-color:#666699;");
                            e.Row.Cells[e.Row.Cells.Count - 2].Attributes.Add("Style", "width:60px;text-align:center;color:BROWN;border-color:#666699;");
                        }
                    }
                }
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[4].Visible = false;
                e.Row.Cells[e.Row.Cells.Count - 1].Visible = false;
                e.Row.Cells[e.Row.Cells.Count - 5].Visible = false;
                for (int i = e.Row.Cells.Count - 5; i >= 5; i--)
                {
                    if (e.Row.Cells[i].Text.Trim() != "" && e.Row.Cells[i].Text.Trim() != "&nbsp;")
                    {
                        allots += 1;
                        if (e.Row.Cells[i].Text == e.Row.Cells[i - 1].Text)
                        {
                            int cspan = e.Row.Cells[i].ColumnSpan < 2 ? 2 : e.Row.Cells[i].ColumnSpan + 1;
                            e.Row.Cells[i - 1].ColumnSpan = cspan;
                            e.Row.Cells[i - 1].BackColor = Color.DarkGray;
                            e.Row.Cells[i - 1].ForeColor = Color.Black;
                            e.Row.Cells[i].Visible = false;
                            allots -= 1;
                        }
                    }
                }
                try
                {
                    string flag = "0";
                    for (int i = 5; i < e.Row.Cells.Count - 4; i++)
                    {
                        string BayId = "";
                        if (i == 5)
                            flag = "0";
                        if (e.Row.Cells[i].Text.Trim() != "" && e.Row.Cells[i].Text.Trim() != "&nbsp;")
                        {
                            int shortReg = Convert.ToInt32(e.Row.Cells[i].Text.Split('|').GetValue(0).ToString());
                            string FullRegNo = (string)DataManager.GetRegNo(shortReg);
                            string JobCode = e.Row.Cells[i].Text.Split('|').GetValue(6).ToString();
                            string JobDesc = e.Row.Cells[i].Text.Split('|').GetValue(7).ToString();
                            string InTime = e.Row.Cells[i].Text.Split('|').GetValue(10).ToString();
                            string AllotTime = e.Row.Cells[i].Text.Split('|').GetValue(11).ToString();
                            string OutTime = DateTime.Parse(InTime).AddMinutes(int.Parse(AllotTime)).ToString("HH:mm");
                            string PromisedTime = GetpromisedTime(shortReg);
                            BayId = e.Row.Cells[i].Text.Split('|').GetValue(3).ToString();
                            DataTable dt = new DataTable();
                            dt = GetBay(BayId);
                            string Bay = "";
                            if (dt.Rows.Count > 0)
                            {
                                Bay = dt.Rows[0]["BayName"].ToString().Replace("#", " ");
                            }
                            Color color = (e.Row.Cells[i].Text.Split('|').GetValue(14).ToString() == "0") ? Color.DarkGray : Color.Gray;
                            flag = "1";
                            e.Row.Cells[i].Text = "<div style=width:1px;font-size:smaller;>" + Bay + "</div>";//
                            e.Row.Cells[i].BackColor = color;
                            e.Row.Cells[i].ToolTip = " Reg No:" + FullRegNo + " \r\n " + "Bay Name :" + Bay + " \r\n " + "Job Code :" + JobCode + " \r\n " + "Job Desc :" + JobDesc + " \r\n " + "In-Time :" + InTime + " \r\n " + "Out-Time :" + OutTime + "\r\n " + "Promised Time :" + PromisedTime;
                        }
                        else
                        {
                            e.Row.Cells[i].ToolTip = "";
                            e.Row.Cells[i].Text = "";
                        }
                    }
                    if (flag != "1")
                    {
                        e.Row.Visible = false;
                        flag = "0";
                    }
                    e.Row.Visible = true;
                }
                catch (Exception ex)
                {
                }
            }
        }
        else if (RadioTypes.SelectedValue.ToString() == "1")
        {
            int allots = 0;
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[e.Row.Cells.Count - 3].Visible = false;
                GridViewRow gvr = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
                TableCell cell = new TableCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.Text = "<div style=width:44px>BAY</div>";
                gvr.Cells.Add(cell);
                for (int i = 4; i < e.Row.Cells.Count - 3; i++)
                {
                    int colsp = 0;
                    for (int j = 4; j < e.Row.Cells.Count - 3; j++)
                    {
                        if (e.Row.Cells[i].Text.Split(':').GetValue(0).ToString() == e.Row.Cells[j].Text.Split(':').GetValue(0).ToString())
                        {
                            colsp++;
                        }
                    }
                    cell = new TableCell();
                    cell.ColumnSpan = colsp;
                    cell.HorizontalAlign = HorizontalAlign.Center;
                    cell.Text = e.Row.Cells[i].Text.Split(':').GetValue(0).ToString();
                    string str = DateTime.Now.ToString("hh");
                    if (Int32.Parse(cell.Text) == Int32.Parse(str))
                    {
                        cell.BackColor = System.Drawing.Color.DarkOrange;
                    }
                    gvr.Cells.Add(cell);
                    i = i + colsp - 1;
                }
                cell = new TableCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.Text = "<div style=width:35px>CARS</div>";
                cell.Font.Size = FontUnit.Point(7);
                cell.ToolTip = "No of Cars";
                gvr.Cells.Add(cell);
                cell = new TableCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.Text = "<div style=width:35px>TTA(m)</div>";
                cell.Font.Size = FontUnit.Point(7);
                cell.ToolTip = "Total Time Alloted (min.)";
                gvr.Cells.Add(cell);
                timeLineHeader.Controls[0].Controls.AddAt(0, gvr);
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    if (i > 3 && i < e.Row.Cells.Count - 3)
                    {
                        string str = e.Row.Cells[i].Text.Substring(0, 6) + " " + e.Row.Cells[i].Text.Substring(6);
                        DateTime datt = DateTime.Parse(str);
                        if (DateTime.Compare(DateTime.Parse(datt.ToString("HH:mm")), DateTime.Parse(DateTime.Now.ToString("HH:mm"))) <= 0)
                        {
                            e.Row.Cells[i].BackColor = System.Drawing.Color.DarkOrange;
                        }
                    }
                    e.Row.Cells[i].Text = "";
                }
                e.Row.Cells[1].Attributes.Add("style", "width:44px;");
                e.Row.Cells[e.Row.Cells.Count - 2].Attributes.Add("style", "width:35px;");
                e.Row.Cells[e.Row.Cells.Count - 1].Attributes.Add("style", "width:35px;");
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[1].Text.Contains('#') == true)
                {
                    e.Row.Cells[1].BackColor = System.Drawing.Color.Khaki;
                    e.Row.Cells[1].ForeColor = System.Drawing.Color.BlueViolet;
                    e.Row.Cells[1].Text = e.Row.Cells[1].Text.Replace('#', ' ').Trim();
                    e.Row.Cells[1].ToolTip = e.Row.Cells[1].Text + " : Inactive Bay";
                }
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[e.Row.Cells.Count - 3].Visible = false;
                FillBayColors();
                string BayCol = "";
                try
                {
                    for (int bc = 0; bc < BayColors.Rows.Count; bc++)
                    {
                        if (BayColors.Rows[bc][0].ToString().Trim() == e.Row.Cells[1].Text.ToString().Trim())
                        {
                            BayCol = BayColors.Rows[bc][1].ToString().Trim();
                        }
                    }
                }
                catch (Exception ex) { }
                for (int i = e.Row.Cells.Count - 3; i >= 4; i--)
                {
                    if (e.Row.Cells[i].Text.Trim() != "" && e.Row.Cells[i].Text.Trim() != "&nbsp;")
                    {
                        e.Row.Cells[i].Attributes.Add("style", "background-color:" + BayCol + ";color:#ffffff;font-weight:bold;");
                        allots += 1;
                        if (e.Row.Cells[i].Text.Split('|')[0].Trim() == e.Row.Cells[i - 1].Text.Split('|')[0].Trim())
                        {
                            int cspan = e.Row.Cells[i].ColumnSpan < 2 ? 2 : e.Row.Cells[i].ColumnSpan + 1;
                            e.Row.Cells[i - 1].ColumnSpan = cspan;
                            e.Row.Cells[i].Visible = false;
                            allots -= 1;
                        }
                    }
                }
                try
                {
                    e.Row.Height = Unit.Pixel(35);
                    for (int i = 4; i < e.Row.Cells.Count - 3; i++)
                    {
                        if (e.Row.Cells[i].Text.Trim() != "" && e.Row.Cells[i].Text.Trim() != "&nbsp;")
                        {
                            string RegNo = e.Row.Cells[i].Text.Split('|').GetValue(1).ToString();
                            string EmpId = e.Row.Cells[i].Text.Split('|').GetValue(4).ToString();
                            DataTable Empdt = new DataTable();
                            Empdt = GetEmp(EmpId);
                            int shortReg = Convert.ToInt32(e.Row.Cells[i].Text.Split('|').GetValue(0).ToString());
                            string EmpName = "";
                            if (Empdt.Rows.Count > 0)
                                EmpName = Empdt.Rows[0][0].ToString();
                            DataTable Details = new DataTable();
                            Details = GetvehDetailsForBayHover(shortReg);
                            string FullRegNo = "";
                            string PDT = "";
                            string VehicleModel = "";
                            string ServiceType = "";
                            if (Details.Rows.Count > 0)
                            {
                                FullRegNo = Details.Rows[0]["RegNo"].ToString();
                                PDT = Details.Rows[0]["PromisedTime"].ToString();
                                VehicleModel = Details.Rows[0]["VehicleModel"].ToString();
                                ServiceType = Details.Rows[0]["ServiceType"].ToString();
                            }
                            e.Row.Cells[i].ToolTip = " Emp Name :" + EmpName + " \r\n" + " Reg No :" + FullRegNo + "\r\n" + " Promised Time :" + PDT + "\r\n" + " Vehicle Model :" + VehicleModel + "\r\n" + " ServiceType :" + ServiceType;
                            e.Row.Cells[i].Text = "<div style=width:1px;font-size:smaller;><table cellpadding=0 cellspacing=0 style=width:100%;table-layout:fixed;white-space:nowrap;><tr><td>" + RegNo.Substring(RegNo.Length - 4) + "</td></tr></table></div>";//<tr><td>" + EmpName + "</td></tr>
                        }
                        else
                        {
                            e.Row.Cells[i].ToolTip = "";
                            e.Row.Cells[i].Text = "";
                        }
                    }
                    e.Row.Cells[e.Row.Cells.Count - 2].Attributes.Add("style", "text-align:center;");
                    e.Row.Cells[e.Row.Cells.Count - 1].Attributes.Add("style", "text-align:center;");
                }
                catch (Exception ex)
                {
                }
            }
        }
    }

    private static int spans = 0;

    public static void MergeCols(GridView gridView)
    {
        try
        {
            int allots = 0;
            for (int row = 0; row < gridView.Rows.Count; row++)
            {
                for (int col = spans + 4 - 1; col >= 4; col--)
                {
                    string curCell = gridView.Rows[row].Cells[col].Text.Trim();
                    string prevCell = gridView.Rows[row].Cells[col - 1].Text.Trim();
                    if (curCell == prevCell)
                    {
                        if (curCell.Trim() != "&nbsp;")
                        {
                            gridView.Rows[row].Cells[col - 1].ColumnSpan = gridView.Rows[row].Cells[col].ColumnSpan < 2 ? 2 : gridView.Rows[row].Cells[col].ColumnSpan + 1;
                            if (allots % 2 == 0)
                                gridView.Rows[row].Cells[col - 1].Attributes.Add("Style", "text-align:center;background-image: url('images/bgapp.png'); height:16px;width:16px;");
                            else
                                gridView.Rows[row].Cells[col - 1].Attributes.Add("Style", "text-align:center;background-image: url('images/bgappalt.png'); height:16px;width:16px;");
                            gridView.Rows[row].Cells[col].Visible = false;
                            allots += 1;
                        }
                    }
                    if (curCell.Trim() != "&nbsp;")
                    {
                        if (allots % 2 == 0)
                            gridView.Rows[row].Cells[col].Attributes.Add("Style", "text-align:center;background-image: url('images/bgapp.png'); height:16px;width:16px;");
                        else
                            gridView.Rows[row].Cells[col].Attributes.Add("Style", "text-align:center;background-image: url('images/bgappalt.png'); height:16px;width:16px;");
                        allots += 1;
                    }
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void gdvRegTagList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[1].Visible = false;

                e.Row.Cells[8].Visible = false;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Width = new Unit(20);
                e.Row.Cells[2].Width = new Unit(75);
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[8].Visible = false;
                e.Row.Cells[2].Attributes.Add("Style", "text-align:center");

                e.Row.Cells[2].ToolTip = e.Row.Cells[2].Text.Replace("*", "").Replace("#", "");

                if (e.Row.Cells[2].Text.Contains("*") == true)
                {
                    e.Row.Cells[2].Text = e.Row.Cells[2].Text.Replace("*", "<font size='5' color='red'>*</font>");
                }

                if (e.Row.Cells[2].Text.Contains("#") == true)
                {
                    e.Row.Cells[2].Attributes.Add("style", "background-image:url('images/CustomerWaiting.png'); background-repeat: no-repeat;background-size:17px 17px;background-position:left top; width:10px;text-align:center;");
                    e.Row.Cells[2].Text = e.Row.Cells[2].Text.Replace("#", "");
                }

                if (e.Row.Cells[6].Text.Contains("#") == true)
                {
                    e.Row.Cells[6].ForeColor = Color.Orange;
                    e.Row.Cells[6].Font.Bold = true;
                    e.Row.Cells[6].Text = e.Row.Cells[6].Text.Replace("#", "");
                }

                e.Row.Cells[3].Attributes.Add("Style", "text-align:center");
                e.Row.Cells[4].Attributes.Add("Style", "text-align:center");
                e.Row.Cells[5].Attributes.Add("Style", "text-align:center");

                if (e.Row.Cells[5].Text.Trim() == "Y")
                {
                    e.Row.Cells[5].Text = "Y";
                }
                else
                    e.Row.Cells[5].Style.Value = "text-align:center";
            }
        }
        catch (Exception ex)
        {
        }
    }

    private string GetRegNoByRefId(string RefId)
    {
        SqlConnection con = new SqlConnection(Session["ConnectionString"].ToString());
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "udpGetRegNoByRefId";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RefId", RefId);
            cmd.Connection = con;
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            if (con.State != ConnectionState.Open)
                con.Open();
            da.Fill(dt);
            if (dt.Rows.Count != 0)
            {
                return dt.Rows[0][0].ToString().Trim();
            }
            else
            {
                return "";
            }
        }
        catch (Exception ex)
        {
            return "";
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }

    private string GetRegNo(string TagNo)
    {
        SqlConnection con = new SqlConnection(Session["ConnectionString"].ToString());
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "udpGetRegNo";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TagNo", TagNo);
            cmd.Connection = con;
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            if (con.State != ConnectionState.Open)
                con.Open();
            da.Fill(dt);
            if (dt.Rows.Count != 0)
            {
                return dt.Rows[0][0].ToString().Trim();
            }
            else
            {
                return "";
            }
        }
        catch (Exception ex)
        {
            return "";
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }

    private string GetRefId(string TagNo)
    {
        SqlConnection con = new SqlConnection(Session["ConnectionString"].ToString());
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "udpGetRefId";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TagNo", TagNo);
            cmd.Connection = con;
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            if (con.State != ConnectionState.Open)
                con.Open();
            da.Fill(dt);
            if (dt.Rows.Count != 0)
            {
                return dt.Rows[0][0].ToString().Trim();
            }
            else
            {
                return "";
            }
        }
        catch (Exception ex)
        {
            return "";
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }

    protected void GetTechnicianName(DropDownList ddl, string EmpType, string TeamLeadId, string ShiftID, bool RoadTest)
    {
        SqlConnection con = new SqlConnection(Session["ConnectionString"].ToString());
        try
        {
            ddl.Items.Clear();
           // ddl.Items.Add("--Select--");
            SqlCommand cmd = new SqlCommand("GetEmployeeList", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Type", EmpType);
            cmd.Parameters.AddWithValue("@TLId", TeamLeadId);
            cmd.Parameters.AddWithValue("@ShiftId", ShiftID);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            if (con.State != ConnectionState.Open)
                con.Open();
            sda.Fill(dt);
            ddl.DataSource = dt;
            ddl.DataTextField = "EmpName";
            ddl.DataValueField = "EmpId";
            ddl.DataBind();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }

    protected bool CheckWithPDT(string Refno, DateTime inTime, DateTime outTime)
    {
        SqlConnection con = new SqlConnection(Session["ConnectionString"].ToString());
        try
        {
            SqlCommand cmd = new SqlCommand("CheckAllotTimeWithPDT", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@inTime", inTime.ToString());
            cmd.Parameters.AddWithValue("@outTime", outTime.ToString());
            cmd.Parameters.AddWithValue("@RefNo", Refno.Trim());
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            if (con.State != ConnectionState.Open)
                con.Open();
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Close();
            if (dt.Rows.Count == 0)
                return true;
            else
                return false;
        }
        catch (Exception ex)
        {
            return false;
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }

    protected bool FixAllotment(DropDownList ddl, DateTime inTime, DateTime outTime, string RefNo, string EmpType, string BayID, string TeamLeadId)
    {
        SqlConnection con = new SqlConnection(Session["ConnectionString"].ToString());
        try
        {
            SqlCommand cmd = new SqlCommand("InsertTblJobAllotment", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmpId", ddl.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@InTime", inTime.ToString());
            cmd.Parameters.AddWithValue("@OutTime", outTime.ToString());
            cmd.Parameters.AddWithValue("@RefNo", RefNo.Trim());
            cmd.Parameters.AddWithValue("@EmpType", EmpType.Trim());
            cmd.Parameters.AddWithValue("@BayID", BayID.Trim());
            cmd.Parameters.AddWithValue("@EmpT", EmpType);
            cmd.Parameters.AddWithValue("@TeamLeadID", TeamLeadId);
            cmd.Parameters.AddWithValue("@ShiftId", ddlShift.SelectedValue);
            if (con.State != ConnectionState.Open)
                con.Open();
            cmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }

    protected void ddlEmpTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        RefreshGrid(RadioTypes.SelectedValue.ToString());
    }

    [System.Web.Services.WebMethod(EnableSession = true)]
    public static string LoadBayDetails(string BayId, string RegNo)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = GetBay(BayId);
            string Bay = "";
            if (dt.Rows.Count > 0)
            {
                Bay = dt.Rows[0]["BayName"].ToString().Replace("#", " ");
            }

            string str = "<span><table id=InOutPnl style=width:100%;><tr><td style=width:50px;>Reg No: </td><td style=text-align:center;>" + RegNo + "</td></tr><tr><td style=width:50px;>Bay: </td><td style=text-align:center;>" + Bay + "</td></tr></table></span>";
            return str;
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    private static DataTable GetBay(string BayId)
    {
        SqlConnection con1 = new SqlConnection(System.Web.HttpContext.Current.Session["ConnectionString"].ToString());
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter("udpGetBay", con1);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.AddWithValue("@BayId", BayId);
            if (con1.State != ConnectionState.Open)
                con1.Open();
            sda.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
        finally
        {
            if (con1.State != ConnectionState.Closed)
                con1.Close();
        }
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("JobAllotment.aspx");
        }
        catch (Exception ex) { }
    }

    protected void BindTechnicianDateTime(string AllotDate, string RegNo, string EmpId, ref DropDownList ddlIn)
    {
        SqlConnection con = new SqlConnection(Session["ConnectionString"].ToString());
        try
        {
            if (EmpId.ToString().Trim() == "")
            {
                ddlIn.Items.Clear();
                return;
            }
            ddlIn.Items.Clear();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetAvailableTechTimeList";
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@RegNo", RegNo);
            cmd.Parameters.AddWithValue("@EmpId", EmpId.Replace("--Select--", "0"));
            cmd.Parameters.AddWithValue("@AllotDate", DateTime.Parse(AllotDate));
            cmd.Parameters.AddWithValue("@AllotId ", ((allotId.Trim() == "") ? "0" : allotId));
            if (con.State != ConnectionState.Open)
                con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count != 0)
            {
                for (int flag = 0; flag < dt.Rows.Count; flag++)
                {
                    ddlIn.Items.Add(dt.Rows[flag][0].ToString());
                }
            }
        }
        catch (Exception ex)
        {
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }

    protected void drpFloorName_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlTeam.SelectedIndex = -1;
        ddlShift.SelectedIndex = -1;
        RefreshGrid(RadioTypes.SelectedValue.ToString());
    }

    protected void BindTechnicianOutDateTime(string AllotDate, string RegNo, string EmpId, ref DropDownList ddlOut)
    {
        SqlConnection con = new SqlConnection(Session["ConnectionString"].ToString());
        try
        {
            if (EmpId.ToString().Trim() == "")
            {
                ddlOut.Items.Clear();
                return;
            }
            ddlOut.Items.Clear();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetAvailableTechTimeListOut";
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@RegNo", RegNo);
            cmd.Parameters.AddWithValue("@EmpId", EmpId.Replace("--Select--", "0"));
            cmd.Parameters.AddWithValue("@AllotDate", DateTime.Parse(AllotDate));
            cmd.Parameters.AddWithValue("@AllotId ", ((allotId.Trim() == "") ? "0" : allotId));
            if (con.State != ConnectionState.Open)
                con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count != 0)
            {
                for (int flag = 0; flag < dt.Rows.Count; flag++)
                {
                    if (flag != 0)
                        ddlOut.Items.Add(dt.Rows[flag][0].ToString());
                }
            }
        }
        catch (Exception ex)
        {
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }

    protected void btntimeLineRefresh_Click(object sender, EventArgs e)
    {
        try
        {
            RefreshGrid(RadioTypes.SelectedValue.ToString());
        }
        catch (Exception ex) { }
    }

    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        try
        {
            RefreshGrid(RadioTypes.SelectedValue.ToString());
        }
        catch (Exception ex) { }
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            RefreshGrid(RadioTypes.SelectedValue.ToString());
        }
        catch (Exception ex) { }
    }
}