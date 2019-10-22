using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JobAllotment : System.Web.UI.Page
{
    private static DateTime StTime;
    private static DateTime EdTime;
    private static string allotId = "";
    private DataTable BayColors;
    private SqlConnection con = new SqlConnection(DataManager.ConStr);
    private static int rowSelected = 0;
    private static string ModelName = "";
    private static string stdTime = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["ROLE"] == null || Session["ROLE"].ToString() != "WORK MANAGER")
            {
                Response.Redirect("login.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("login.aspx");
        }
        try
        {
            if (!Page.IsPostBack)
            {
                txtDate.Text = DateTime.Now.ToShortDateString();
                tbAllotDate.Text = DateTime.Now.ToShortDateString();
                lbl_LoginName.Text = "Welcome, " + Session["UserId"].ToString();
                lbl_CurrentPage.Text = "Job Allotment";
                this.Title = "Job Allotment";
                lblVersion.Text = DataManager.getVersion();
                lbScroll0.Text = Session["DealerName"].ToString();
                getRegTag();
                getAllBays();
                timeLine.Visible = false;
                timeLine_Bay.Visible = false;
            }
            RefreshGrid();
            RefreshGridforBay();
        }
        catch (Exception ex)
        {
        }
        if (IsPostBack && Request.Form["__EVENTTARGET"] == "myClick")
        {
            try
            {
                if (Request.Form["__EVENTARGUMENT"] != null && Request.Form["__EVENTARGUMENT"].ToString().Trim() != string.Empty)
                {
                    RefreshGrid();
                    RefreshGridforBay();
                    timeLine.Style.Add("table-layout", "fixed");
                    timeLine.Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
                    timeLine_Bay.Style.Add("table-layout", "fixed");
                    timeLine_Bay.Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
                    lblMessage.Text = "";
                    int selrow = Convert.ToInt32(Request.Form["__EVENTARGUMENT"].Split(',').GetValue(0).ToString());
                    int selcol = Convert.ToInt32(Request.Form["__EVENTARGUMENT"].Split(',').GetValue(1).ToString());
                    rowSelected = selrow;
                    string selVehicle = timeGrid.Rows[selrow][selcol].ToString();
                    try
                    {
                        if (selVehicle.Split('|').GetValue(12).ToString() == "0")
                        {
                            lblMessage.ForeColor = Color.Red;
                            lblMessage.Text = "Out-Time already crossed !";
                            ClearAll();
                            tbAllotDate.Text = System.DateTime.Now.ToShortDateString();
                            return;
                        }
                        lblRefNo.Text = selVehicle.Split('|').GetValue(0).ToString();
                        allotId = selVehicle.Split('|').GetValue(1).ToString();
                        ddlTeam.SelectedValue = selVehicle.Split('|').GetValue(2).ToString();
                        ddlBay.SelectedValue = selVehicle.Split('|').GetValue(3).ToString();
                        ddlEmpType.SelectedValue = selVehicle.Split('|').GetValue(4).ToString();
                        ddlEmpType_SelectedIndexChanged(null, null);
                        cmbTechName.SelectedValue = selVehicle.Split('|').GetValue(5).ToString();
                        tbJobCode.Text = selVehicle.Split('|').GetValue(6).ToString();
                        tbJobDesc.Text = selVehicle.Split('|').GetValue(7).ToString();
                        lblStdTime.Text = selVehicle.Split('|').GetValue(8).ToString();
                        tbAllotDate.Text = selVehicle.Split('|').GetValue(9).ToString();
                        txtInTime.Text = selVehicle.Split('|').GetValue(10).ToString();
                        tbAllotTime.Text = selVehicle.Split('|').GetValue(11).ToString();
                        ddlEmpType.Enabled = false;
                        cmbTechName.Enabled = false;
                        if (selVehicle.Split('|').GetValue(13).ToString() == "0")
                        {
                            tbAllotDate.Enabled = false;
                            txtInTime.Enabled = false;
                            btnTechCancel.Visible = false;
                        }
                        else
                        {
                            tbAllotDate.Enabled = true;
                            txtInTime.Enabled = true;
                            btnTechCancel.Visible = true;
                        }

                        tbJobCode.Enabled = false;
                        tbJobDesc.Enabled = false;
                        btnTechAssign.Visible = false;
                        btnTechUpdate.Visible = true;
                        btnTechClose.Visible = true;
                    }
                    catch (Exception ex)
                    {
                        lblRefNo.Text = "0";
                        ddlBay.SelectedIndex = 0;
                        ClearAll();
                        tbAllotDate.Text = System.DateTime.Now.ToShortDateString();
                    }
                    DataTable dtDetails = new DataTable();
                    dtDetails = GetDetails(lblRefNo.Text);
                    if (dtDetails.Rows.Count != 0)
                    {
                        lblRegNo.Text = dtDetails.Rows[0][0].ToString();
                        lblSA.Text = dtDetails.Rows[0][2].ToString();
                        lblTagNo.Text = dtDetails.Rows[0][1].ToString();
                        lblpdt.Text = dtDetails.Rows[0][3].ToString();
                        lblST.Text = dtDetails.Rows[0][5].ToString();
                    }
                    else
                    {
                        lblRegNo.Text = "";
                        lblSA.Text = "";
                        lblTagNo.Text = "";
                        lblpdt.Text = "";
                        lblST.Text = "";
                    }
                    Fill_TotalAllotedTimeByTechnician(cmbTechName.SelectedValue, tbAllotDate.Text);
                    Fill_TotalAllotedTimeByVehicle(lblRefNo.Text, tbAllotDate.Text);
                    Request.Form.Clear();
                }
            }
            catch (Exception ex)
            {
            }
        }
    }

    protected DataTable GetDetails(string Refid)
    {
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

    private DataTable timeGrid = new DataTable();
    private DataTable timeGridBay = new DataTable();

    private void RefreshGrid()
    {
        SqlConnection con = new SqlConnection(DataManager.ConStr);
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter sda = new SqlDataAdapter();
        timeGrid = new DataTable();
        try
        {
            timeLine.DataSource = null;
            timeGrid.Clear();
            cmd = new SqlCommand("GetJobAllotmentTimeforTechnician", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AllotDate", tbAllotDate.Text.Trim());
            cmd.Parameters.AddWithValue("@EmpType", ddlEmpType.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@ShiftId", ddlShift.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@TechnicianId", cmbTechName.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@FloorName", "ALL");
            sda = new SqlDataAdapter(cmd);
            sda.Fill(timeGrid);
            int k = timeGrid.Rows.Count;
            if (k == 0)
            {
                timeLine.DataSource = null;
                timeLine.Visible = false;
                timeLine.DataBind();
            }
            else
            {
                timeLine.Visible = true;
                timeLine.DataSource = timeGrid;
                timeLine.DataBind();
            }
        }
        catch (Exception ex)
        {
            timeLine.Visible = false;
            timeLine.DataSource = null;
            timeLine.DataBind();
        }
    }

    private void RefreshGridforBay()
    {
        SqlConnection con = new SqlConnection(DataManager.ConStr);
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter sda = new SqlDataAdapter();
        timeGridBay = new DataTable();
        try
        {
            timeLine_Bay.DataSource = null;
            timeGridBay.Clear();
            cmd = new SqlCommand("GetJobAllotmentTimeBay", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AllotDate", tbAllotDate.Text.Trim());
            cmd.Parameters.AddWithValue("@BayId", ddlBay.SelectedValue.Trim());
            cmd.Parameters.AddWithValue("@ShiftId", ddlShift.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@FloorName", "ALL");
            sda = new SqlDataAdapter(cmd);
            sda.Fill(timeGridBay);
            if (timeGridBay.Rows.Count == 0)
            {
                timeLine_Bay.Visible = false;
                timeLine_Bay.DataSource = null;
                timeLine_Bay.DataBind();
            }
            else
            {
                timeLine_Bay.Visible = true;
                timeLine_Bay.DataSource = timeGridBay;
                timeLine_Bay.DataBind();
            }
        }
        catch (Exception ex)
        {
            timeLine_Bay.DataSource = null;
            timeLine_Bay.DataBind();
        }
    }

    private void ClearAllOnce()
    {
        try
        {
            tbJobCode.Enabled = false;
            tbJobDesc.Enabled = false;
            ddlEmpType.SelectedIndex = 0;
            tbJobCode.Text = "";
            tbJobDesc.Text = "";
            lblStdTime.Text = "";
            txtInTime.Text = "";
            tbAllotTime.Text = "";
            if (btnTechAssign.Visible == false)
            {
                lblRegNo.Text = "";
                lblSA.Text = "";
                lblTagNo.Text = "";
                lblpdt.Text = "";
                lblST.Text = "";
                lbl_TechnicianTime.Text = "";
                lbl_VehicleTime.Text = "";
            }
            btnTechAssign.Visible = true;
            btnTechUpdate.Visible = false;
            btnTechCancel.Visible = false;
            btnTechClose.Visible = false;
            tbAllotDate.ReadOnly = false;
            ddlEmpType.Enabled = true;
            txtInTime.Enabled = true;
            tbJobCode.Enabled = true;
            tbJobDesc.Enabled = true;
        }
        catch (Exception ex)
        {
        }
    }
    private void ClearAll()
    {
        try
        {
            tbJobCode.Enabled = false;
            tbJobDesc.Enabled = false;
            cmbTechName.Items.Clear();
            cmbTechName.Items.Add("--Select--");
            cmbTechName.SelectedIndex = -1;
            cmbTechName.Enabled = true;
            cmbTechName.SelectedIndex = 0;
            ddlEmpType.SelectedIndex = 0;
            tbJobCode.Text = "";
            tbJobDesc.Text = "";
            lblStdTime.Text = "";
            txtInTime.Text = "";
            tbAllotTime.Text = "";
            lblKMS.Text = "";
            if (btnTechAssign.Visible == false)
            {
                lblRegNo.Text = "";
                lblSA.Text = "";
                lblTagNo.Text = "";
                lblpdt.Text = "";
                lblST.Text = "";
                lbl_TechnicianTime.Text = "";
                lbl_VehicleTime.Text = "";
            }
            btnTechAssign.Visible = true;
            btnTechUpdate.Visible = false;
            btnTechCancel.Visible = false;
            btnTechClose.Visible = false;
            tbAllotDate.ReadOnly = false;
            ddlEmpType.Enabled = true;
            txtInTime.Enabled = true;
            tbJobCode.Enabled = true;
            tbJobDesc.Enabled = true;
        }
        catch (Exception ex)
        {
        }
    }

    protected DataTable GetAllotedTime(string AllotNo)
    {
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

    protected void getRegTag()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("GetRegTagList", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            if (con.State != ConnectionState.Open)
                con.Open();
            sda.Fill(dt);
            lblPendingCount.Text = dt.Rows.Count.ToString();
            SrchDt = dt;
            con.Close();
            gdvRegTagList.DataSource = dt;
            gdvRegTagList.DataBind();
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

    protected void getAllBays()
    {
        SqlCommand cmd1 = new SqlCommand("GetBays", con);
        cmd1.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        if (con.State != ConnectionState.Open)
            con.Open();
        sda1.Fill(dt1);
        con.Close();
        if (dt1.Rows.Count > 0)
        {
            ddlBay.Items.Clear();
            ddlBay.Items.Add(new ListItem("Bay"));
            ddlBay.DataSource = dt1;
            ddlBay.DataTextField = "BayName";
            ddlBay.DataValueField = "BayId";
            ddlBay.DataBind();
        }
    }

    private void hideAllExtra()
    {
        try
        {
            txtSrchReg.Text = "";
            txtSrchTag.Text = "";
            cmbTechName.Enabled = true;
            ddlBay.Enabled = true;
            lblpdt.Text = "";
            lblSA.Text = "";
            lblST.Text = "";
            lblRegNo.Text = "";
            lblTagNo.Text = "";
            lblRefNo.Text = "";
        }
        catch (Exception ex)
        {
        }
    }

    protected void getWorkTime(string shiftID)
    {
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

    protected void tmrClock_Tick(object sender, EventArgs e)
    {
    }

    private void DeleteAppointment(String ID)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("", con);
            cmd.CommandText = "DeleteTblJobAllotment";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@AllotID", ID);
            if (con.State != ConnectionState.Open)
                con.Open();
            cmd.ExecuteNonQuery();
            hideAllExtra();
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

    protected void btnBACK_Click(object sender, EventArgs e)
    {
        try
        {
            Session["CURRENT_PAGE"] = null;
            Response.Redirect("JCHome.aspx");
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
                e.Row.Cells[7].Visible = false;
                e.Row.Cells[8].Visible = false;
                e.Row.Cells[9].Visible = false;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Width = new Unit(20);
                e.Row.Cells[2].Width = new Unit(60);
                e.Row.Cells[5].Width = new Unit(80);
                e.Row.Cells[1].Width = new Unit(110);
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[7].Visible = false;
                e.Row.Cells[8].Visible = false;
                e.Row.Cells[9].Visible = false;
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
                if (e.Row.Cells[5].Text.Contains("#") == true)
                {
                    e.Row.Cells[5].ForeColor = Color.Orange;
                    e.Row.Cells[5].Font.Bold = true;
                    e.Row.Cells[5].Text = e.Row.Cells[5].Text.Replace("#", "");
                }
                e.Row.Cells[3].Attributes.Add("Style", "text-align:left");
                e.Row.Cells[4].Attributes.Add("Style", "text-align:left");
                e.Row.Cells[5].Attributes.Add("Style", "text-align:left");
                e.Row.Cells[6].Attributes.Add("Style", "text-align:left");
            }
        }
        catch (Exception ex)
        {
        }
    }

    private string GetRegNo(string TagNo)
    {
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

    protected void gdvRegTagList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ClearAll();
            if (gdvRegTagList.SelectedRow.Cells[6].ForeColor != Color.Orange)
            {
                lblMessage.Text = "";
                hideAllExtra();
                lblTagNo.Text = gdvRegTagList.SelectedRow.Cells[3].Text.Trim();
                string tagno = gdvRegTagList.SelectedRow.Cells[3].Text.Trim();
                lblRefNo.Text = GetRefId(tagno);
                ModelName = GetModelName(lblRefNo.Text);
                lblRegNo.Text = GetRegNo(tagno);
                getAllBays();
                lblSA.Text = gdvRegTagList.SelectedRow.Cells[6].Text.Trim();
                lblpdt.Text = gdvRegTagList.SelectedRow.Cells[5].Text.Trim();
                lblST.Text = gdvRegTagList.SelectedRow.Cells[4].Text.Trim();
                lblKMS.Text = gdvRegTagList.SelectedRow.Cells[9].Text.Trim();
                ddlTeam.SelectedValue = gdvRegTagList.SelectedRow.Cells[8].Text.Trim();
                Fill_TotalAllotedTimeByVehicle(lblRefNo.Text, tbAllotDate.Text);
            }
            else
            {
                lblRegNo.Text = "";
                lblRefNo.Text = "";
                lblTagNo.Text = "";
                lblST.Text = "";
                lblSA.Text = "";
                lblpdt.Text = "";
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = "PDT Time Exceeded.";
                lblRegNo.Text = "";
            }
        }
        catch (Exception ex) { }
    }

    protected string GetModelName(string Refno)
    {
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select VehicleModel from tblMaster where Slno=" + Refno;
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

    protected void gdvRegTagList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gdvRegTagList.PageIndex = e.NewPageIndex;
            getRegTag();
            gdvRegTagList.SelectedIndex = -1;
        }
        catch (Exception ex) { }
    }

    protected void GetTechnicianName(DropDownList ddl, string EmpType, string TeamLeadId, string ShiftID, bool RoadTest, string Allotdate)
    {
        try
        {
            ddl.Items.Clear();
            ddl.Items.Add("--Select--");
            SqlCommand cmd = new SqlCommand("GetEmployeeList", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Type", EmpType);
            cmd.Parameters.AddWithValue("@TLId", TeamLeadId);
            cmd.Parameters.AddWithValue("@ShiftId", ShiftID);
            cmd.Parameters.AddWithValue("@AllotDate", Allotdate);
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

    protected void ddlShift_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearAll();
        try
        {
            lblMessage.Text = "";
            if (ddlShift.SelectedIndex > -1)
            {
                if (ddlTeam.SelectedIndex > 0)
                {
                    GetTechnicianName(cmbTechName, "Mechanic", ddlTeam.SelectedValue.ToString(), ddlShift.SelectedValue.ToString(), false, tbAllotDate.Text);
                }
                getWorkTime(ddlShift.SelectedValue.ToString());
            }
            else
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = "Please Select Shift!";
            }
            timeLine.Visible = false;
            timeLine_Bay.Visible = false;
        }
        catch (Exception ex) { }
    }

    protected void ddlTeam_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearAll();
        try
        {
            lblMessage.Text = "";
            if (ddlTeam.SelectedIndex > 0)
            {
                GetTechnicianName(cmbTechName, "Mechanic", ddlTeam.SelectedValue.ToString(), ddlShift.SelectedValue.ToString(), false, tbAllotDate.Text);
                timeLine.Visible = false;
                timeLine_Bay.Visible = false;
            }
            else
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = "Please Select Team Lead!";
            }
        }
        catch (Exception ex) { }
    }

    private int standartTime;

    protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("NewJobAllotment.aspx", false);
        }
        catch (Exception ex) { }
    }

    protected void btnSrchReg_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lblRefNo.Text = "";
            lblTagNo.Text = "";
            lblSA.Text = "";
            lblST.Text = "";
            lblpdt.Text = "";
            lblMessage.Text = "";
            ClearAll();
            DataTable dtTime = new DataTable();
            getRegTag();
            dtTime = GetAllotedTime(allotId);
            if ((txtSrchReg.Text.Trim() == "" || txtSrchReg.Text.Trim() == null) && (txtSrchTag.Text == "" || txtSrchTag.Text.Trim() == null))
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = "Enter The Reg No. or Tag no!";
                txtSrchReg.Focus();
                lblRegNo.Text = "";
            }
            else
            {
                if (txtSrchReg.Text != "" && txtSrchTag.Text == "")
                {
                    DataTable dt = new DataTable();
                    int i = 0;
                    SqlCommand cmd = new SqlCommand("CheckAllotmentByRegNo", con);
                    SqlDataAdapter ad = new SqlDataAdapter(cmd);
                    ad.SelectCommand.CommandType = CommandType.StoredProcedure;
                    ad.SelectCommand.Parameters.AddWithValue("@RegNo", txtSrchReg.Text.ToString().Trim());
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    ad.Fill(dt);
                    if (i == dt.Rows.Count)
                    {
                        lblMessage.ForeColor = Color.Red;
                        lblMessage.Text = "Wrong Registration No.!";
                        lblRegNo.Text = "";
                    }
                    else
                    {
                        if (dt.Rows[i][6].ToString().Contains("#") == true)
                        {
                            lblRegNo.Text = "";
                            lblRefNo.Text = "";
                            lblTagNo.Text = "";
                            lblST.Text = "";
                            lblSA.Text = "";
                            lblpdt.Text = "";
                            lblMessage.ForeColor = Color.Red;
                            lblMessage.Text = "PDT Time Exceeded.";
                            lblRegNo.Text = "";
                        }
                        else
                        {
                            lblRegNo.Text = dt.Rows[i][1].ToString(); ;
                            lblRefNo.Text = dt.Rows[i][0].ToString();
                            lblTagNo.Text = dt.Rows[i][2].ToString();
                            lblST.Text = dt.Rows[i][3].ToString();
                            lblSA.Text = dt.Rows[i][4].ToString();
                            lblpdt.Text = dt.Rows[i][6].ToString();
                            int refno = Convert.ToInt32(dt.Rows[i][0].ToString());
                            ModelName = GetModelName(lblRefNo.Text);
                        }
                    }
                }
                else if (txtSrchTag.Text != "" && txtSrchReg.Text == "")
                {
                    DataTable dt = new DataTable();
                    int i = 0;
                    SqlCommand cmd = new SqlCommand("CheckAllotmentByTagNo", con);
                    SqlDataAdapter ad = new SqlDataAdapter(cmd);
                    ad.SelectCommand.CommandType = CommandType.StoredProcedure;
                    ad.SelectCommand.Parameters.AddWithValue("@TagNo", txtSrchTag.Text.ToString().Trim());
                    ad.Fill(dt);
                    if (i == dt.Rows.Count)
                    {
                        lblMessage.ForeColor = Color.Red;
                        lblMessage.Text = "Wrong Tag No.!";
                        lblRegNo.Text = "";
                    }
                    else
                    {
                        if (dt.Rows[i][6].ToString().Contains("#") == true)
                        {
                            lblRegNo.Text = "";
                            lblRefNo.Text = "";
                            lblTagNo.Text = "";
                            lblST.Text = "";
                            lblSA.Text = "";
                            lblpdt.Text = "";
                            lblMessage.ForeColor = Color.Red;
                            lblMessage.Text = "PDT Time Exceeded.";
                            lblRegNo.Text = "";
                        }
                        else
                        {
                            lblRegNo.Text = dt.Rows[i][1].ToString(); ;
                            lblRefNo.Text = dt.Rows[i][0].ToString();
                            lblTagNo.Text = dt.Rows[i][2].ToString();
                            lblSA.Text = dt.Rows[i][4].ToString();
                            lblST.Text = dt.Rows[i][3].ToString();
                            lblpdt.Text = dt.Rows[i][6].ToString();
                            ModelName = GetModelName(lblRefNo.Text);
                        }
                    }
                }
                else
                {
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Text = "Please select either one of the search options!";
                }
            }
        }
        catch (Exception ex)
        {
        }
        finally
        {
            Fill_TotalAllotedTimeByVehicle(lblRefNo.Text, tbAllotDate.Text);
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }

    protected void btnAddNewJob_Click(object sender, EventArgs e)
    {
    }

    protected void GetBayOnSearch(string Refno)
    {
        SqlConnection con1 = new SqlConnection(DataManager.ConStr);
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter("GetBayOnVehicleSearch", con1);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.AddWithValue("@Refno", Refno);
            if (con1.State != ConnectionState.Open)
                con1.Open();
            sda.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                ddlBay.SelectedIndex = ddlBay.Items.IndexOf(ddlBay.Items.FindByValue(dt.Rows[0][4].ToString()));
                ddlTeam.SelectedIndex = ddlTeam.Items.IndexOf(ddlTeam.Items.FindByValue(dt.Rows[0][3].ToString()));
            }
        }
        catch (Exception ex)
        { }
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public static string[] GetCompletionList(string prefixText, int count)
    {
        SqlCommand cmd = new SqlCommand();
        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString());
        cmd.Connection = con1;
        cmd.CommandText = "getJobCodeList";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@prefixText", prefixText);
        cmd.Parameters.AddWithValue("@ModelName", ModelName.ToString().ToUpper());
        if (con1.State != ConnectionState.Open)
            con1.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        string[] results = new string[dt.Rows.Count];
        for (int index = 0; index < dt.Rows.Count; index++)
        {
            results[index] = dt.Rows[index][0].ToString();
        }

        return results;
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public static string[] GetCompletionList1(string prefixText, int count)
    {
        SqlCommand cmd = new SqlCommand();
        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString());
        cmd.Connection = con1;
        cmd.CommandText = "getJobDescList";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@prefixText", prefixText);
        cmd.Parameters.AddWithValue("@ModelName", ModelName.ToString().ToUpper());
        if (con1.State != ConnectionState.Open)
            con1.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        string[] results = new string[dt.Rows.Count];
        for (int index = 0; index < dt.Rows.Count; index++)
        {
            results[index] = dt.Rows[index][1].ToString();
        }

        return results;
    }

    protected void tbJobCode_TextChanged(object sender, EventArgs e)
    {
        SqlConnection con1 = new SqlConnection(DataManager.ConStr);
        string jobcodestring = "";
        jobcodestring = tbJobCode.Text;
        try
        {
            string ModelName1 = GetModelName(lblRefNo.Text);
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("getJobDetailsfromJobCode", con1);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@JobCode ", tbJobCode.Text.Trim());
            cmd.Parameters.AddWithValue("@model", ModelName1.Trim());
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            int HrhsInMinute = 0;
            if (dt.Rows.Count > 0)
            {
                string StandardAllottime = dt.Rows[0][0].ToString();
                DateTime allotdatetime = Convert.ToDateTime(tbAllotDate.Text);
                if (StandardAllottime.Contains('.'))
                {
                    string hrs = StandardAllottime.Split('.').GetValue(0).ToString();
                    HrhsInMinute = ((Convert.ToInt32(hrs)) * 60) + ((Convert.ToInt32(StandardAllottime.Split('.').GetValue(1))) * 6);
                    standartTime = HrhsInMinute;
                }
                else
                {
                    HrhsInMinute =
                    (Convert.ToInt32(StandardAllottime) * 60);
                    standartTime = HrhsInMinute;
                }
                tbJobDesc.Enabled = false;
                tbJobDesc.Text = dt.Rows[0][1].ToString();
                tbJobDesc.Enabled = true;
                tbAllotTime.Text = HrhsInMinute.ToString();
                lblStdTime.Text = HrhsInMinute.ToString();
                stdTime = HrhsInMinute.ToString();
            }
            else
            {
                tbAllotTime.Text = "";
                stdTime = "0";
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void tbJobDesc_TextChanged(object sender, EventArgs e)
    {
        SqlConnection con1 = new SqlConnection(DataManager.ConStr);
        string jobcodestring = "";
        jobcodestring = tbJobDesc.Text;
        try
        {
            string ModelName1 = GetModelName(lblRefNo.Text);
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con1;
            cmd.CommandText = "getJobDetailsfromJobDesc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@JobDesc", tbJobDesc.Text.Trim());
            cmd.Parameters.AddWithValue("@model", ModelName1.Trim());
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            int HrhsInMinute = 0;
            if (dt.Rows.Count > 0)
            {
                string StandardAllottime = dt.Rows[0][0].ToString();
                DateTime allotdatetime = Convert.ToDateTime(tbAllotDate.Text);
                if (StandardAllottime.Contains('.'))
                {
                    string hrs = StandardAllottime.Split('.').GetValue(0).ToString();
                    HrhsInMinute = ((Convert.ToInt32(hrs)) * 60) + ((Convert.ToInt32(StandardAllottime.Split('.').GetValue(1))) * 6);
                    standartTime = HrhsInMinute;
                }
                else
                {
                    HrhsInMinute =
                    (Convert.ToInt32(StandardAllottime) * 60);
                    standartTime = HrhsInMinute;
                }
                tbJobCode.Enabled = false;
                tbJobCode.Text = dt.Rows[0][1].ToString();
                tbJobCode.Enabled = true;
                tbAllotTime.Text = HrhsInMinute.ToString();
                lblStdTime.Text = HrhsInMinute.ToString();
            }
            else
            {
                tbAllotTime.Text = "";
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void drpFloorName_SelectedIndexChanged(object sender, EventArgs e)
    {
        getRegTag();
        ClearAll();
        ddlTeam.SelectedIndex = -1;
        ddlShift.SelectedIndex = -1;
        ddlBay.SelectedIndex = -1;
        lblRegNo.Text = "";
        lblSA.Text = "";
        lblST.Text = "";
        lblTagNo.Text = "";
        lblpdt.Text = "";

        SqlConnection con1 = new SqlConnection(DataManager.ConStr);
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter("udpListFloorBay", con1);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (con1.State != ConnectionState.Open)
                con1.Open();
            sda.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                ddlBay.DataSource = null;
                ddlBay.Items.Clear();
                ddlBay.Items.Add(new ListItem("Bay"));
                ddlBay.DataSource = dt;
                ddlBay.DataTextField = "BayName";
                ddlBay.DataValueField = "BayId";
                ddlBay.DataBind();
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void ddlBay_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearAll();
        GetTechnicianName(cmbTechName, ddlEmpType.SelectedItem.Text.Trim(), ddlTeam.SelectedValue.ToString(), ddlShift.SelectedValue.ToString(), false, tbAllotDate.Text);
        timeLine.Visible = false;
        timeLine_Bay.Visible = false;
    }

    protected void btnMechAssign_Click(object sender, EventArgs e)
    {
        lblMessage.ForeColor = Color.Red;
        if (lblRegNo.Text.ToString().Trim() == "")
        {
            lblMessage.Text = "Please select Regno !";
        }
        else if (ddlShift.SelectedIndex == -1)
        {
            lblMessage.Text = "Please select shift !";
            ddlShift.Focus();
        }
        else if (ddlTeam.SelectedIndex == 0)
        {
            lblMessage.Text = "Please select team lead !";
            ddlTeam.Focus();
        }
        else if (ddlBay.SelectedIndex == 0)
        {
            lblMessage.Text = "Please select bay !";
            ddlBay.Focus();
        }
        else if (cmbTechName.SelectedIndex == 0)
        {
            lblMessage.Text = "Please select technician !";
            cmbTechName.Focus();
        }
        else if (txtInTime.Text.ToString().Trim() == "")
        {
            lblMessage.Text = "Please enter in-time !";
            txtInTime.Focus();
        }
        else if (tbAllotTime.Text.ToString().Trim() == "")
        {
            lblMessage.Text = "Please enter allot time !";
            tbAllotTime.Focus();
        }
        else if (tbAllotTime.Text.ToString().Trim() == "0")
        {
            lblMessage.Text = "Please enter valid allot time !";
            tbAllotTime.Focus();
        }
        else
        {
            try
            {
                int chk = int.Parse(txtInTime.Text.Substring(3).Trim());
                chk = chk % 5;
                if (chk != 0)
                {
                    lblMessage.Text = "Please enter InTime multiple of 5 minute !";
                    txtInTime.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Please enter InTime multiple of 5 minute !";
                txtInTime.Focus();
                return;
            }

            try
            {
                int chk = int.Parse(tbAllotTime.Text.ToString().Trim());
                chk = chk % 10;
                if (chk != 0)
                {
                    lblMessage.Text = "Please enter allot time multiple of 10 !";
                    tbAllotTime.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Please enter allot time multiple of 10 !";
                tbAllotTime.Focus();
                return;
            }

            bool check = false;
            try
            {
                DateTime Intime = new DateTime();
                Intime = Convert.ToDateTime(tbAllotDate.Text.Trim() + " " + txtInTime.Text.Trim());
                SqlCommand cmd = new SqlCommand("InsertTblJobAllotment", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpID", cmbTechName.SelectedValue);
                cmd.Parameters.AddWithValue("@InTime", Intime);
                cmd.Parameters.AddWithValue("@RefNo", lblRefNo.Text);
                cmd.Parameters.AddWithValue("@EmpType", ddlEmpType.SelectedValue);
                cmd.Parameters.AddWithValue("@BayID", ddlBay.SelectedValue);
                cmd.Parameters.AddWithValue("@TeamLeadID", ddlTeam.SelectedValue);
                cmd.Parameters.AddWithValue("@ShiftId", ddlShift.Text);
                cmd.Parameters.AddWithValue("@JobId", tbJobCode.Text);
                cmd.Parameters.AddWithValue("@JobDesc", tbJobDesc.Text);
                cmd.Parameters.AddWithValue("@StdTime", lblStdTime.Text);
                cmd.Parameters.AddWithValue("@AllotTime", tbAllotTime.Text);
                cmd.Parameters.Add("@flag", SqlDbType.Int);
                cmd.Parameters["@flag"].Direction = ParameterDirection.Output;
                cmd.Parameters["@flag"].Value = 0;
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd.ExecuteNonQuery();
                string msg = Convert.ToString(cmd.Parameters["@flag"].Value);

                switch (msg)
                {
                    case "0": lblMessage.ForeColor = Color.Red;
                        lblMessage.Text = "Error in writing to database !";
                        check = false;
                        break;

                    case "1": check = true;
                        break;

                    case "2": lblMessage.ForeColor = Color.Red;
                        lblMessage.Text = "Technician not available !";
                        check = false;
                        break;

                    case "3": lblMessage.ForeColor = Color.Red;
                        lblMessage.Text = "Bay not available !";
                        check = false;
                        break;

                    case "4": lblMessage.ForeColor = Color.Red;
                        lblMessage.Text = "Vehicle is already assign to another Bay !";
                        check = false;
                        break;

                    case "5": lblMessage.ForeColor = Color.Red;
                        lblMessage.Text = "Promise Delivery Time is near to cross or already crossed !";
                        check = false;
                        break;

                    case "7": lblMessage.ForeColor = Color.Red;
                        lblMessage.Text = "Allotment In-Time is not Valid. !";
                        check = false;
                        break;

                    default:
                        check = false;
                        break;
                }
            }
            catch (Exception ex)
            {
                check = false;
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }

            if (check == true)
            {
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = "Assigned To Vehicle No: " + lblRegNo.Text;
                ClearAllOnce();
                tbAllotDate.Text = System.DateTime.Now.ToShortDateString();
                RefreshGrid();
                RefreshGridforBay();
                GetTechnicianName(cmbTechName, ddlEmpType.SelectedItem.Text.Trim(), ddlTeam.SelectedValue.ToString(), ddlShift.SelectedValue.ToString(), false, tbAllotDate.Text);
            }
        }
    }

    protected void btnMechUpdate_Click(object sender, EventArgs e)
    {
        bool check = false;
        if (tbAllotTime.Text.ToString().Trim() == "")
        {
            lblMessage.ForeColor = Color.Red;
            lblMessage.Text = "Please enter allot time !";
            tbAllotTime.Focus();
        }
        else
        {
            try
            {
                int chk = int.Parse(txtInTime.Text.Substring(3).Trim());
                chk = chk % 5;
                if (chk != 0)
                {
                    lblMessage.Text = "Please enter InTime multiple of 5 minute !";
                    txtInTime.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Please enter InTime multiple of 5 minute !";
                txtInTime.Focus();
                return;
            }

            try
            {
                int chk = int.Parse(tbAllotTime.Text.ToString().Trim());
                chk = chk % 10;
                if (chk != 0)
                {
                    lblMessage.Text = "Please enter allot time multiple of 10 !";
                    tbAllotTime.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Please enter allot time multiple of 10 !";
                tbAllotTime.Focus();
                return;
            }

            try
            {
                DateTime Intime = new DateTime();
                Intime = Convert.ToDateTime(tbAllotDate.Text.Trim() + " " + txtInTime.Text.Trim());
                SqlCommand cmd = new SqlCommand("", con);
                cmd.CommandText = "UpdateTblJobAllotment";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@AllotId", allotId);
                cmd.Parameters.AddWithValue("@EmpID", cmbTechName.SelectedValue);
                cmd.Parameters.AddWithValue("@InTime", Intime);
                cmd.Parameters.AddWithValue("@RefNo", lblRefNo.Text.ToString().Trim());
                cmd.Parameters.AddWithValue("@BayID", ddlBay.SelectedValue);
                cmd.Parameters.AddWithValue("@ShiftId", ddlShift.SelectedValue);
                cmd.Parameters.AddWithValue("@AllotTime", tbAllotTime.Text);
                cmd.Parameters.Add("@flag", SqlDbType.Int);
                cmd.Parameters["@flag"].Direction = ParameterDirection.Output;
                cmd.Parameters["@flag"].Value = 0;
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd.ExecuteNonQuery();
                string msg = Convert.ToString(cmd.Parameters["@flag"].Value);

                switch (msg)
                {
                    case "0": lblMessage.ForeColor = Color.Red;
                        lblMessage.Text = "Error in writing to database !";
                        check = false;
                        break;

                    case "1": check = true;
                        break;

                    case "2": lblMessage.ForeColor = Color.Red;
                        lblMessage.Text = "Technician not available !";
                        check = false;
                        break;

                    case "3": lblMessage.ForeColor = Color.Red;
                        lblMessage.Text = "Bay not available !";
                        check = false;
                        break;

                    case "4": lblMessage.ForeColor = Color.Red;
                        lblMessage.Text = "Vehicle is already assign to another Bay !";
                        check = false;
                        break;

                    case "5": lblMessage.ForeColor = Color.Red;
                        lblMessage.Text = "Promise Delivery Time is near to cross or already crossed !";
                        check = false;
                        break;

                    case "6": lblMessage.ForeColor = Color.Red;
                        lblMessage.Text = "Out Time Crossed Current Time !";
                        check = false;
                        break;

                    case "7": lblMessage.ForeColor = Color.Red;
                        lblMessage.Text = "In-Time Less Than Current Time !";
                        check = false;
                        break;

                    default:
                        check = false;
                        break;
                }

                if (check == true)
                {
                    RefreshGrid();
                    RefreshGridforBay();
                    lblMessage.ForeColor = Color.Green;
                    lblMessage.Text = "Re-Schedule Done For Vehicle No: " + lblRegNo.Text;
                    lblMessage.ForeColor = Color.Green;
                    ClearAll();
                    tbAllotDate.Enabled = true;
                    tbAllotDate.Text = System.DateTime.Now.ToShortDateString();
                    GetTechnicianName(cmbTechName, ddlEmpType.SelectedItem.Text.Trim(), ddlTeam.SelectedValue.ToString(), ddlShift.SelectedValue.ToString(), false, tbAllotDate.Text);
                }
                allotId = "0";
            }
            catch (Exception ex)
            {
                ClearAll();
                tbAllotDate.ReadOnly = true;
                tbAllotDate.Text = System.DateTime.Now.ToShortDateString();
            }
            finally
            {
                GetTechnicianName(cmbTechName, ddlEmpType.SelectedItem.Text.Trim(), ddlTeam.SelectedValue.ToString(), ddlShift.SelectedValue.ToString(), false, tbAllotDate.Text);
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
        }
    }

    protected void btnMechCancel_Click(object sender, EventArgs e)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("", con);
            cmd.CommandText = "DeleteTblJobAllotment";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@AllotID", allotId);
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd.ExecuteNonQuery();
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = "Un-Assigned To Vehicle No: " + lblRegNo.Text;
                lblMessage.ForeColor = Color.Green;
                allotId = "0";
                ClearAll();
                tbAllotDate.Text = System.DateTime.Now.ToShortDateString();
                GetTechnicianName(cmbTechName, ddlEmpType.SelectedItem.Text.Trim(), ddlTeam.SelectedValue.ToString(), ddlShift.SelectedValue.ToString(), false, tbAllotDate.Text);
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
            RefreshGrid();
            RefreshGridforBay();
            hideAllExtra();
        }
        catch (Exception ex)
        {
        }
    }

    protected void btnMechClose_Click(object sender, EventArgs e)
    {
        hideAllExtra();
        ClearAll();
        tbAllotDate.Text = System.DateTime.Now.ToShortDateString();
        lblMessage.Text = "";
        GetTechnicianName(cmbTechName, ddlEmpType.SelectedItem.Text.Trim(), ddlTeam.SelectedValue.ToString(), ddlShift.SelectedValue.ToString(), false, tbAllotDate.Text);
    }

    protected void ddlEmpTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void ddlBayList_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void btntimeLineRefresh_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        RefreshGrid();
        RefreshGridforBay();
    }

    protected void FillBayColors()
    {
        SqlConnection con = new SqlConnection(DataManager.ConStr);
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

    private static DataTable GetEmp(string EmpId)
    {
        DataTable dt = new DataTable();
        SqlConnection con = new SqlConnection(DataManager.ConStr);
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "udpGetEmpDetails";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@EmpId", EmpId);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        sda.Fill(dt);
        return dt;
    }

    public string GetTagNo(int RefNo)
    {
        SqlConnection con = new SqlConnection(DataManager.ConStr);
        con.Open();
        string cmdstr = "SELECT top (1) RFID from tblMaster WHERE slno =" + RefNo.ToString();// @RefNo";
        SqlCommand cmd = new SqlCommand(cmdstr, con);
        cmd.Parameters.Clear();
        try
        {
            string RegNo = string.Empty;
            RegNo = cmd.ExecuteScalar().ToString();
            return RegNo;
        }
        catch (Exception ex)
        {
            return string.Empty;
        }
        finally
        {
            con.Close();
        }
    }

    protected void timeLine_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            e.Row.Height = 35;
        }
        int allots = 0;
        e.Row.Cells[1].Width = new Unit(20);
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
            cell.Text = "<table style=width:100px;><tr><td>Employee</td></tr></table>";
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
            timeLine.Controls[0].Controls.AddAt(0, gvr);
            DateTime dt = DateTime.Parse(tbAllotDate.Text);
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
                    else if (DateTime.Compare(DateTime.Parse(DateTime.Now.ToShortDateString()), DateTime.Parse(tbAllotDate.Text.ToString())) > 0)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.DarkOrange;
                    }
                }
                e.Row.Cells[i].Text = "";
            }
            e.Row.Cells[e.Row.Cells.Count - 4].Attributes.Add("style", "width:98px;");
            e.Row.Cells[e.Row.Cells.Count - 3].Attributes.Add("style", "width:98px;");
            e.Row.Cells[e.Row.Cells.Count - 2].Attributes.Add("style", "width:98px;");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[0].Text != "")
            {
                e.Row.Cells[1].ToolTip = e.Row.Cells[1].Text.Split('#')[1].Trim();
                e.Row.Cells[1].Text = e.Row.Cells[1].Text.Split('#')[0].Trim();
                e.Row.Cells[1].Width = new Unit(105);
                e.Row.Cells[1].Font.Size = new FontUnit(8);
                e.Row.Cells[1].Wrap = true;
                for (int i = 4; i < e.Row.Cells.Count - 4; i++)
                {
                    if (e.Row.Cells[e.Row.Cells.Count - 1].Text == "1")
                    {
                        if (DateTime.Compare(DateTime.Parse(txtDate.Text.Trim()), DateTime.Now) <= 0)
                            e.Row.Cells[i].Attributes.Add("OnClick", "Javascript:__doPostBack('myClick','" + (e.Row.RowIndex.ToString() + "," + i.ToString()) + "');");
                        e.Row.Cells[e.Row.Cells.Count - 4].Attributes.Add("Style", "text-align:center;");
                        e.Row.Cells[e.Row.Cells.Count - 3].Attributes.Add("Style", "text-align:center;");
                        e.Row.Cells[e.Row.Cells.Count - 2].Attributes.Add("Style", "text-align:center;");
                    }
                    else
                    {
                        e.Row.Cells[i].Attributes.Add("OnClick", "Javascript:__doPostBack('myClick','" + (e.Row.RowIndex.ToString() + "," + i.ToString()) + "');");
                        e.Row.Cells[1].Attributes.Add("Style", "color:BROWN;border-color:#666699;");
                        e.Row.Cells[e.Row.Cells.Count - 4].Attributes.Add("Style", "text-align:center;color:BROWN;border-color:#666699;");
                        e.Row.Cells[e.Row.Cells.Count - 3].Attributes.Add("Style", "text-align:center;color:BROWN;border-color:#666699;");
                        e.Row.Cells[e.Row.Cells.Count - 2].Attributes.Add("Style", "text-align:center;color:BROWN;border-color:#666699;");
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
            string BayName = "";
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
                        string TagNo = (string)GetTagNo(shortReg);
                        string JobCode = e.Row.Cells[i].Text.Split('|').GetValue(6).ToString();
                        string JobDesc = e.Row.Cells[i].Text.Split('|').GetValue(7).ToString();
                        string InTime = e.Row.Cells[i].Text.Split('|').GetValue(10).ToString();
                        string AllotTime = e.Row.Cells[i].Text.Split('|').GetValue(11).ToString();
                        string OutTime = DateTime.Parse(InTime).AddMinutes(int.Parse(AllotTime)).ToString("HH:mm");
                        string PromisedTime = GetpromisedTime(shortReg);
                        BayId = e.Row.Cells[i].Text.Split('|').GetValue(4).ToString();
                        if (ddlBayList.SelectedValue.Trim().ToUpper() == "ALL" || BayId == ddlBayList.SelectedValue.Trim())
                        {
                            BayName = ddlBay.Items.FindByValue(e.Row.Cells[i].Text.Split('|').GetValue(3).ToString()).Text;
                            Color color = (e.Row.Cells[i].Text.Split('|').GetValue(14).ToString() == "0") ? Color.DarkGray : Color.Gray;
                            flag = "1";
                            e.Row.Cells[i].Text = "<div style=width:1px;font-size:smaller;>" + BayName + "</div>";//
                            e.Row.Cells[i].BackColor = color;
                            e.Row.Cells[i].ToolTip = " Reg No:" + FullRegNo + " \r\n " + "Bay Name :" + BayName + " \r\n " + "Job Code :" + JobCode + " \r\n " + "Job Desc :" + JobDesc + " \r\n " + "In-Time :" + InTime + " \r\n " + "Out-Time :" + OutTime + "\r\n " + "Promised Time :" + PromisedTime;
                        }
                        else
                        {
                            flag = "0";
                            e.Row.Cells[i].Text = "";
                            e.Row.Cells[i].ToolTip = "";
                        }
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

    protected void ddlEmpType_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetTechnicianName(cmbTechName, ddlEmpType.SelectedItem.Text.Trim(), ddlTeam.SelectedValue.ToString(), ddlShift.SelectedValue.ToString(), false, tbAllotDate.Text);
        timeLine.Visible = false;
        timeLine_Bay.Visible = false;
    }

     private void Fill_TotalAllotedTimeByVehicle(string Slno, String date)
    {
        DataTable dt = new DataTable();
        SqlConnection con = new SqlConnection(DataManager.ConStr);
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "udpGetVehicleAllotTime";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@SlNo", Slno);
        cmd.Parameters.AddWithValue("@Date", date);
        cmd.Parameters.AddWithValue("@ShiftId", ddlShift.SelectedValue);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        sda.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            lbl_VehicleTime.Text = dt.Rows[0][0].ToString();
        }
        else
        {
            lbl_VehicleTime.Text = "0";
        }
    }

    private void Fill_TotalAllotedTimeByTechnician(string TechID, String date)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(DataManager.ConStr);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "udpGetTechnicianAllotTime";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@EmpId", TechID);
            cmd.Parameters.AddWithValue("@Date", date);
            cmd.Parameters.AddWithValue("@ShiftId", ddlShift.SelectedValue);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                lbl_TechnicianTime.Text = dt.Rows[0][0].ToString();
            }
            else
            {
                lbl_TechnicianTime.Text = "0";
            }
        }
        catch (Exception ex)
        {
            lbl_TechnicianTime.Text = "0";
        }
    }

    protected void cmbTechName_SelectedIndexChanged(object sender, EventArgs e)
    {
        timeLine.Visible = false;
        timeLine_Bay.Visible = false;
    }

    public static DataTable GetvehDetailsForBayHover(int RefNo)
    {
        SqlConnection con = new SqlConnection(DataManager.ConStr);
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

    public static string GetpromisedTime(int RefNo)
    {
        string Promisedtime = string.Empty;
        SqlConnection con = new SqlConnection(DataManager.ConStr);
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
        finally
        {
        }
    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {
        ImageButton1_Click(null, null);
    }

    protected void timeLine_Bay_RowDataBound(object sender, GridViewRowEventArgs e)
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
            cell.Text = "<table style=width:93px;><tr><td>BAY</td></tr></table>";
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
            timeLine_Bay.Controls[0].Controls.AddAt(0, gvr);
            DateTime dt = DateTime.Parse(tbAllotDate.Text);
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                if (i > 3 && i < e.Row.Cells.Count - 3)
                {
                    string str = e.Row.Cells[i].Text.Substring(0, 6) + " " + e.Row.Cells[i].Text.Substring(6);
                    DateTime datt = DateTime.Parse(dt.Date.ToShortDateString() + " " + str);
                    if (DateTime.Compare(DateTime.Parse(datt.ToString("HH:mm")), DateTime.Parse(DateTime.Now.ToString("HH:mm"))) <= 0 && DateTime.Parse(dt.Date.ToShortDateString()) == DateTime.Parse(DateTime.Now.ToShortDateString()))
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.DarkOrange;
                    }
                    else if (DateTime.Compare(DateTime.Parse(DateTime.Now.ToShortDateString()), DateTime.Parse(tbAllotDate.Text.ToString())) > 0)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.DarkOrange;
                    }
                }
                e.Row.Cells[i].Text = "";
            }
            e.Row.Cells[1].Attributes.Add("style", "width:55px;");
            e.Row.Cells[e.Row.Cells.Count - 2].Attributes.Add("style", "width:98px;");
            e.Row.Cells[e.Row.Cells.Count - 1].Attributes.Add("style", "width:98px;");
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
                        string TagNo = (string)GetTagNo(shortReg);
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
                        if (txtSrchReg.Text.ToString() != "" && txtSrchTag.Text.ToString() == "")
                        {
                            if (txtSrchReg.Text.ToString() == FullRegNo.Substring(FullRegNo.Length - 4))
                            {
                                e.Row.Cells[i].ToolTip = " Emp Name :" + EmpName + " \r\n" + " Reg No :" + FullRegNo + "\r\n" + " Promised Time :" + PDT + "\r\n" + " Vehicle Model :" + VehicleModel + "\r\n" + " ServiceType :" + ServiceType;
                                e.Row.Cells[i].Text = "<div style=width:1px;font-size:Medium;font-weight:Bold;><table cellpadding=0 cellspacing=0 style=width:100%;table-layout:fixed;white-space:nowrap;><tr><td>" + RegNo.Substring(RegNo.Length - 4) + "</td></tr></table></div>";//<tr><td>" + EmpName + "</td></tr>
                            }
                            else
                            {
                                e.Row.Cells[i].ToolTip = " Emp Name :" + EmpName + " \r\n" + " Reg No :" + FullRegNo + "\r\n" + " Promised Time :" + PDT + "\r\n" + " Vehicle Model :" + VehicleModel + "\r\n" + " ServiceType :" + ServiceType;
                                e.Row.Cells[i].Text = "<div style=width:1px;font-size:smaller;><table cellpadding=0 cellspacing=0 style=width:100%;table-layout:fixed;white-space:nowrap;><tr><td>" + RegNo.Substring(RegNo.Length - 4) + "</td></tr></table></div>";//<tr><td>" + EmpName + "</td></tr>
                            }
                        }
                        else if (txtSrchReg.Text.ToString() == "" && txtSrchTag.Text.ToString() != "")
                        {
                            if (txtSrchTag.Text.ToString() == TagNo)
                            {
                                e.Row.Cells[i].ToolTip = " Emp Name :" + EmpName + " \r\n" + " Reg No :" + FullRegNo + "\r\n" + " Promised Time :" + PDT + "\r\n" + " Vehicle Model :" + VehicleModel + "\r\n" + " ServiceType :" + ServiceType;
                                e.Row.Cells[i].Text = "<div style=width:1px;font-size:Medium;font-weight:Bold;><table cellpadding=0 cellspacing=0 style=width:100%;table-layout:fixed;white-space:nowrap;><tr><td>" + RegNo.Substring(RegNo.Length - 4) + "</td></tr></table></div>";//<tr><td>" + EmpName + "</td></tr>
                            }
                            else
                            {
                                e.Row.Cells[i].ToolTip = " Emp Name :" + EmpName + " \r\n" + " Reg No :" + FullRegNo + "\r\n" + " Promised Time :" + PDT + "\r\n" + " Vehicle Model :" + VehicleModel + "\r\n" + " ServiceType :" + ServiceType;
                                e.Row.Cells[i].Text = "<div style=width:1px;font-size:smaller;><table cellpadding=0 cellspacing=0 style=width:100%;table-layout:fixed;white-space:nowrap;><tr><td>" + RegNo.Substring(RegNo.Length - 4) + "</td></tr></table></div>";//<tr><td>" + EmpName + "</td></tr>
                            }
                        }
                        else
                        {
                            e.Row.Cells[i].ToolTip = " Emp Name :" + EmpName + " \r\n" + " Reg No :" + FullRegNo + "\r\n" + " Promised Time :" + PDT + "\r\n" + " Vehicle Model :" + VehicleModel + "\r\n" + " ServiceType :" + ServiceType;
                            e.Row.Cells[i].Text = "<div style=width:1px;font-size:smaller;><table cellpadding=0 cellspacing=0 style=width:100%;table-layout:fixed;white-space:nowrap;><tr><td>" + RegNo.Substring(RegNo.Length - 4) + "</td></tr></table></div>";//<tr><td>" + EmpName + "</td></tr>
                        }
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

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        getRegTag();
        RefreshGrid();
        RefreshGridforBay();
    }

    protected void btn_Back_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("JCHome.aspx");
    }
}