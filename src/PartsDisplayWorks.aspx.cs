using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PartsDisplayWorks : System.Web.UI.Page
{
    private static string BackTo = "";
    private static DataTable DisplayDt;
    private static string EmpId = "0";
    private static int fblank = 0;
    private static int miniTabIndex = 6;
    private static string statusVal = "";
    private static int catid1;

    public static DataTable GetInOutTime(string serviceID, string ProcessName)
    {
        DataTable dt = new DataTable();
        SqlConnection con = new SqlConnection(System.Web.HttpContext.Current.Session[HttpContext.Current.Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlCommand cmd = new SqlCommand("GetProcessInOutTime", con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Serviceid", serviceID);
        cmd.Parameters.AddWithValue("@ProcessName", ProcessName);
        if (con.State != ConnectionState.Open)
            con.Open();
        cmd.ExecuteNonQuery();
        sda.Fill(dt);
        if (con.State != ConnectionState.Closed)
            con.Close();
        return dt;
    }

    protected void BindGrid()
    {
        DisplayDt = new DataTable();
        DataTable HDt = new DataTable();
        DisplayDt.Clear();
        DisplayDt = GetDisplayDate(rbType.SelectedValue, txtVehicleNumber.Text, miniTabIndex + 1);
        HDt = DisplayDt;
        if (DisplayDt.Rows.Count == 0)
        {
            grdDisplay.DataSource = null;
            grdDisplay.DataBind();
            GridView1.DataSource = null;
        }
        else
        {
            grdDisplay.GridLines = GridLines.Horizontal;
            GridView1.DataSource = HDt;
            GridView1.DataBind();
            grdDisplay.DataSource = DisplayDt;
            grdDisplay.DataBind();
        }

        fblank = 0;
        for (int fbt = 0; fbt < HDt.Rows.Count; fbt++)
        {
            if (HDt.Rows[fbt][5].ToString().Trim() != "")
            {
                fblank = 1;
            }
        }
        FillVehicleStatus();
        lbVCount.Text = DisplayDt.Rows.Count.ToString();
    }

    protected void btn_Back_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["ROLE"].ToString() == "WORK MANAGER")
        {
            Response.Redirect("JCHome.aspx");
        }
        else if (Session["ROLE"].ToString() == "DISPLAY" && Request.Url.ToString().Contains("DisplayWorks.aspx?Back=123") && Session["BACKROLE"].ToString() == "SM")
        {
            Response.Redirect("DisplayHome.aspx?Back=222");
        }
        else if (Session["ROLE"].ToString() == "DISPLAY" && Request.Url.ToString().Contains("DisplayWorks.aspx?Back=123") && Session["BACKROLE"].ToString() == "GMSERVICE")
        {
            Response.Redirect("DisplayHome.aspx?Back=333");
        }
        else if (Session["ROLE"].ToString() == "DISPLAY" && Request.Url.ToString().Contains("DisplayWorks.aspx?Back=123") && Session["BACKROLE"].ToString() == "DISPLAY")
        {
            Response.Redirect("DisplayHome.aspx");
        }
        else if (Session["ROLE"].ToString() == "CRM")
        {
            Response.Redirect("CRMHome.aspx");
        }
    }

    protected void btn_logout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();
        Response.Redirect("login.aspx");
    }

    protected void btnBACK_Click(object sender, EventArgs e)
    {
        Session["CURRENT_PAGE"] = null;
        if (Session["ROLE"].ToString() == "WORK MANAGER")
        {
            Response.Redirect("JCHome.aspx");
        }
        else if (Session["ROLE"].ToString() == "FRONT OFFICE")
        {
            Response.Redirect("FrontOfficeHome.aspx");
        }
        else if (Session["ROLE"].ToString() == "SERVICE ADVISOR")
        {
            Response.Redirect("SAHome.aspx");
        }
        else if (Session["ROLE"].ToString() == "CRM")
        {
            Response.Redirect("CRMHome.aspx");
        }
        else if (Session["ROLE"].ToString() == "DISPLAY")
        {
            Response.Redirect("DisplayHome.aspx");
        }
        else if (BackTo == "GMSERVICE")
        {
            Response.Redirect("DisplayHome.aspx?Back=333", false);
        }
        else if (BackTo == "SM")
        {
            Response.Redirect("DisplayHome.aspx?Back=222", false);
        }
    }

    protected void btnc_Click(object sender, EventArgs e)
    {
        TabContainer1.Visible = false;
    }

    protected void BtnClosePopup_Click(object sender, EventArgs e)
    {
        Timer1.Enabled = true;
    }

    protected void btnecncl_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        TabContainer1.Visible = false;
    }

    protected void btnLoad_Click(object sender, EventArgs e)
    {
        try
        {
            if (TxtDate1.Text.Trim() == "" || TxtDate2.Text.Trim() == "" || txtVehicleNumber.Text.Trim() == "" || txtTagNo.Text.Trim() == "")
            {
                lblmsg.Text = "Please enter date range Or vehicle no to search.!";
                lblmsg.Attributes.Add("style", "text-transform:none !important");
                TxtDate1.Text = "";
                TxtDate2.Text = "";
                txtVehicleNumber.Text = "";
                txtTagNo.Text = "";
            }
            else
            {
                lblmsg.Text = "";
            }
            if (txtVehicleNumber.Text.Trim() != "")
            {
                DataTable Dt = new DataTable();
                Dt = GetDisplayDate(rbType.SelectedValue, txtVehicleNumber.Text, miniTabIndex + 1);
                GridView1.DataSource = Dt;
                GridView1.DataBind();
                grdDisplay.DataSource = Dt;
                grdDisplay.DataBind();

                TxtDate1.Text = "";
                TxtDate2.Text = "";
            }
            else if (TxtDate1.Text.Trim() != "" && TxtDate2.Text.Trim() != "")
            {
                DataTable Dt = new DataTable();
                Dt = GetDisplayDate(rbType.SelectedValue, txtVehicleNumber.Text, miniTabIndex + 1);
                GridView1.DataSource = Dt;
                GridView1.DataBind();
                grdDisplay.DataSource = Dt;
                grdDisplay.DataBind();
                txtVehicleNumber.Text = "";
                txtTagNo.Text = "";
            }
        }
        catch (Exception ex)
        {
            lblLoading.Text = ex.Message;
        }
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        lblStatus.CssClass = "reset";
        lblStatus.Text = "";
        try
        {
            DataTable Dt = new DataTable();
            Dt = GetDisplayDate(rbType.SelectedValue, txtVehicleNumber.Text, miniTabIndex + 1);
            GridView1.DataSource = Dt;
            GridView1.DataBind();
            grdDisplay.DataSource = Dt;
            grdDisplay.DataBind();
        }
        catch (Exception ex)
        {
        }
    }

    protected void btnRefresh_Click1(object sender, EventArgs e)
    {
        lblStatus.CssClass = "reset";
        lblStatus.Text = "";
        TxtDate1.Text = "";
        TxtDate2.Text = "";
        txtVehicleNumber.Text = "";
        cmbProcess.SelectedIndex = -1;
        cmbVehicleModel.SelectedIndex = -1;
        cmbServiceType.SelectedIndex = -1;
        cmbCustType.SelectedIndex = -1;
        cmbTeamLead.SelectedIndex = -1;
        cmbSA.SelectedIndex = -1;
        txtTagNo.Text = "";
        lblmsg.Text = "";
        BindGrid();
    }

    protected void btnRPDTCancel_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        TabContainer1.Visible = false;
    }

    protected void btnrrcncl_Click(object sender, EventArgs e)
    {
        TabContainer1.Visible = false;
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lblmsg.Text = "";
            cmbCustType.SelectedIndex = -1;
            cmbServiceType.SelectedIndex = -1;
            cmbVehicleModel.SelectedIndex = -1;
            cmbProcess.SelectedIndex = -1;
            cmbSA.SelectedIndex = -1;
            cmbTeamLead.SelectedIndex = -1;
            ddlState.SelectedIndex = -1;
            int[] cs = { 0, 0, 0, 0 };

            if (txtVehicleNumber.Text.Trim() != "")
            {
                cs[2] = 1;
                TxtDate1.Text = "";
                TxtDate2.Text = "";
                txtTagNo.Text = "";
            }
            if (txtTagNo.Text.Trim() != "")
            {
                cs[3] = 1;
                TxtDate1.Text = "";
                TxtDate2.Text = "";
                txtVehicleNumber.Text = "";
            }
            if (TxtDate1.Text.Trim() != "")
                cs[0] = 1;
            if (TxtDate2.Text.Trim() != "")
                cs[1] = 1;
            if (cs[0] == 0 && cs[1] == 0 && (cs[2] == 1 || cs[3] == 1))
            {
                BindGrid();
                TxtDate1.Text = "";
                TxtDate2.Text = "";
                TabContainer1.Visible = false;
            }
            else if (cs[0] == 1 && cs[1] == 1 && cs[2] == 0 && cs[3] == 0)
            {
                BindGrid();
                txtVehicleNumber.Text = "";
                txtTagNo.Text = "";
                TabContainer1.Visible = false;
            }
            else if (cs[0] == 1 && cs[1] == 0)
            {
                lblmsg.Text = "Please select To Date";
                lblmsg.Attributes.Add("style", "text-transform:none !important");
                lblmsg.ForeColor = Color.Red;
            }
            else if (cs[0] == 0 && cs[1] == 1)
            {
                lblmsg.Text = "Please select From Date";
                lblmsg.Attributes.Add("style", "text-transform:none !important");
                lblmsg.ForeColor = Color.Red;
            }
            else if (cs[0] == 0 && cs[1] == 0 && cs[2] == 0 && cs[3] == 0)
            {
                lblmsg.Text = "Please select Date Range Or Vehicle No Or Tag No";
                lblmsg.Attributes.Add("style", "text-transform:none !important");
                lblmsg.ForeColor = Color.Red;
            }
        }
        catch (Exception ex)
        {
            lblLoading.Text = ex.Message;
        }
    }

    protected void cmbCustType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (cmbCustType.SelectedIndex != 0)
            {
                cmbServiceType.SelectedIndex = -1;
                cmbVehicleModel.SelectedIndex = -1;
                cmbProcess.SelectedIndex = -1;
                TxtDate1.Text = "";
                TxtDate2.Text = "";
                txtVehicleNumber.Text = "";
                txtTagNo.Text = "";
                lblmsg.Text = "";
            }
            BindGrid();
            TabContainer1.Visible = false;
        }
        catch (Exception ex)
        {
        }
    }

    protected void cmbProcess_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (cmbProcess.SelectedIndex != 0)
            {
                cmbCustType.SelectedIndex = -1;
                cmbServiceType.SelectedIndex = -1;
                cmbVehicleModel.SelectedIndex = -1;
                TxtDate1.Text = "";
                TxtDate2.Text = "";
                txtVehicleNumber.Text = "";
                txtTagNo.Text = "";
                lblmsg.Text = ""; lblmsg.CssClass = "reset";
            }
            BindGrid();
            TabContainer1.Visible = false;
        }
        catch (Exception ex)
        {
        }
    }

    protected void cmbServiceType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (cmbServiceType.SelectedIndex != 0)
            {
                cmbCustType.SelectedIndex = -1;
                cmbVehicleModel.SelectedIndex = -1;
                cmbProcess.SelectedIndex = -1;
                TxtDate1.Text = "";
                TxtDate2.Text = "";
                txtVehicleNumber.Text = "";
                txtTagNo.Text = "";
                lblmsg.Text = ""; lblmsg.CssClass = "reset";
            }
            BindGrid();
            TabContainer1.Visible = false;
        }
        catch (Exception ex)
        {
        }
    }

    protected void cmbTeamLead_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (cmbTeamLead.SelectedIndex != 0)
            {
                cmbCustType.SelectedIndex = -1;
                cmbServiceType.SelectedIndex = -1;
                cmbVehicleModel.SelectedIndex = -1;
                TxtDate1.Text = "";
                TxtDate2.Text = "";
                txtVehicleNumber.Text = "";
                txtTagNo.Text = "";
                lblmsg.Text = ""; lblmsg.CssClass = "reset";
            }
            BindGrid();
            TabContainer1.Visible = false;
        }
        catch (Exception ex)
        {
        }
    }

    protected void cmbVehicleModel_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (cmbVehicleModel.SelectedIndex != 0)
            {
                cmbCustType.SelectedIndex = -1;
                cmbServiceType.SelectedIndex = -1;
                cmbProcess.SelectedIndex = -1;
                TxtDate1.Text = "";
                TxtDate2.Text = "";
                txtVehicleNumber.Text = "";
                txtTagNo.Text = "";
                lblmsg.Text = ""; lblmsg.CssClass = "reset";
            }
            BindGrid();
            TabContainer1.Visible = false;
        }
        catch (Exception ex)
        {
        }
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlState.SelectedIndex != 0)
            {
                cmbCustType.SelectedIndex = -1;
                cmbServiceType.SelectedIndex = -1;
                cmbProcess.SelectedIndex = -1;
                TxtDate1.Text = "";
                TxtDate2.Text = "";
                txtVehicleNumber.Text = "";
                txtTagNo.Text = "";
                lblmsg.Text = "";
                lblmsg.CssClass = "reset";
            }
            statusVal = ddlState.SelectedValue.Trim();
            BindGrid();
            TabContainer1.Visible = false;
        }
        catch (Exception ex)
        {
        }
    }

    protected string[] GetCustomerDetail(String RegNo)
    {
        string sConnString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        SqlConnection oConn = new SqlConnection(sConnString);
        SqlCommand cmd = new SqlCommand("", oConn);
        cmd.CommandText = "SELECT Top 1 CustomerName,Customerphone,SlNo FROM tblMaster WHERE RegNo=@RegNo order by Slno desc";
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@RegNo", RegNo);
        SqlDataReader dr = null;
        string[] cust = { string.Empty, string.Empty, string.Empty };
        try
        {
            if (oConn.State != ConnectionState.Open)
                oConn.Open();
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                cust[0] = dr["CustomerName"].ToString();
                cust[1] = dr["Customerphone"].ToString();
                cust[2] = dr["SlNo"].ToString();
            }
            return cust;
        }
        catch
        {
            return cust;
        }
        finally
        {
            if (oConn.State != ConnectionState.Closed)
                oConn.Close();
        }
    }

    protected string getFFRemarks(GridViewRowEventArgs e)
    {
        string ffrem = "";
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlCommand cmd = new SqlCommand("GetFFRemarks", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ServiceId", e.Row.Cells[36].Text.ToString());
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0][0].ToString() == "YES")
                ffrem = dt.Rows[0][0].ToString();
            else if (dt.Rows[0][0].ToString() == "NO")
            {
                ffrem = dt.Rows[0][0].ToString() + ": " + dt.Rows[0][1].ToString();
            }
        }
        return ffrem;
    }

    protected string getPMKRemarks(GridViewRowEventArgs e)
    {
        string pmkrem = "";
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlCommand cmd = new SqlCommand("GetPMKRemarks", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ServiceId", e.Row.Cells[36].Text.ToString());
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0][0].ToString() == "100%")
                pmkrem = dt.Rows[0][0].ToString();
            else
            {
                pmkrem = dt.Rows[0][0].ToString() + ": " + dt.Rows[0][1].ToString();
            }
        }
        return pmkrem;
    }

    protected void getSA()
    {
        cmbSA.Items.Clear();
        cmbSA.Items.Add("Service Advisor");
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlCommand cmd = new SqlCommand("GetServiceAdvisorList", con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        cmd.CommandType = CommandType.StoredProcedure;
        if (con.State != ConnectionState.Open)
            con.Open();
        sda.Fill(dt);
        con.Close();
        cmbSA.DataSource = dt;

        cmbSA.DataValueField = "EmpId";
        cmbSA.DataTextField = "EmpName"; cmbSA.DataBind();
    }

    protected void getTL()
    {
        cmbTeamLead.Items.Clear();
     //   cmbTeamLead.Items.Add("TeamLead");
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlCommand cmd = new SqlCommand("GetTeamLead", con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        cmd.CommandType = CommandType.StoredProcedure;
        if (con.State != ConnectionState.Open)
            con.Open();
        sda.Fill(dt);
        con.Close();
        cmbTeamLead.DataSource = dt;
        cmbTeamLead.DataValueField = "EmpId";
        cmbTeamLead.DataTextField = "EmpName"; cmbTeamLead.DataBind();
    }

    protected void grdDisplay_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdDisplay.PageIndex = e.NewPageIndex;
            DataTable Dt = new DataTable();
            Dt = GetDisplayDate(rbType.SelectedValue, txtVehicleNumber.Text, miniTabIndex + 1);
            GridView1.DataSource = Dt;
            GridView1.DataBind();
            grdDisplay.DataSource = Dt;
            grdDisplay.DataBind();
        }
        catch (Exception ex)
        {
        }
        finally
        {
        }
    }

    protected void grdDisplay_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (grdDisplay.DataSource != null)
        {
            string status = "";
            string counter = "";
            string StatusTech = "";
            string RefNo = e.Row.Cells[21].Text.ToString();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[0].Text != "")
                {
                    e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.color='Blue';this.style.textDecoration='underline';";
                    e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='#666699';";
                    if (Session["ROLE"] == "DISPLAY")
                    {
                        TabContainer1.Visible = false;
                    }
                    else
                        e.Row.Attributes.Add("OnClick", "Javascript:__doPostBack('myClick','" + e.Row.RowIndex + "');");
                }
            }

            string[] HeaderName = new string[e.Row.Cells.Count];
            int rcount = 0;
            rcount = e.Row.Cells.Count;

            //FOR HIDING COLUMNS.
            if (e.Row.RowType != DataControlRowType.Pager)
            {
               // e.Row.Cells[7].Visible = false;
                e.Row.Cells[8].Visible = false;
                e.Row.Cells[9].Visible = false;
                e.Row.Cells[10].Visible = false;
                e.Row.Cells[11].Visible = false;
                e.Row.Cells[12].Visible = false;

                //e.Row.Cells[13].Visible = false;
                e.Row.Cells[14].Visible = false;
                e.Row.Cells[15].Visible = false;
                e.Row.Cells[16].Visible = false;
               // e.Row.Cells[19].Visible = false;
                //e.Row.Cells[18].Visible = false;
                e.Row.Cells[20].Visible = false;
                e.Row.Cells[21].Visible = false;
                e.Row.Cells[22].Visible = false;
                e.Row.Cells[23].Visible = false;
                e.Row.Cells[24].Visible = false;
                e.Row.Cells[1].Visible = false;
            }

            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text =  e.Row.Cells[0].Text.ToString() ;

               // e.Row.Cells[1].Text = "<div style='width:120px;height:42px'><table style='width:100%;Height:100%;' border='0' cellpadding='0' cellspacing='0'><tr><th style=height:50%;></td></tr><tr><th align='left'>" + e.Row.Cells[1].Text.ToString().ToUpper() + "</td></tr></table></div>";
                e.Row.Cells[2].Text =  e.Row.Cells[2].Text.ToString();
                //e.Row.Cells[3].Text = "<div style='height:42px;'><table style='width:100%;Height:100%;' border='0' cellpadding='0' cellspacing='0'><tr><th style=height:50%;></td></tr><tr><th>" + "VRN/VIN" + "</td></tr></table></div>";
                e.Row.Cells[3].Text =  e.Row.Cells[3].Text.ToString();
                e.Row.Cells[4].Text = "JDP CW";
                e.Row.Cells[5].Text = e.Row.Cells[5].Text.ToString();
                e.Row.Cells[6].Text =  "ST";
               // e.Row.Cells[6].Text = "<div style='height:42px;WIDTH:35PX;'><table style='width:100%;Height:100%;' border='0' cellpadding='0' cellspacing='0'><tr><th style=height:50%;>SERVICE</td></tr><tr><th>TYPE</td></tr></table></div>";
                e.Row.Cells[7].Text = e.Row.Cells[7].Text.ToString();
                // e.Row.Cells[6].Text = "<div style='width:25px;height:42px'><table style='width:100%;text-align:left;Height:100%;' border='0' cellpadding='0' cellspacing='0'><tr><th style=height:50%;></td></tr><tr><th>" + e.Row.Cells[6].Text.ToString() + "</td></tr></table></div>";
                e.Row.Cells[13].Text = "PARTS STATUS";
                e.Row.Cells[17].Text = "";
                e.Row.Cells[18].Text = e.Row.Cells[18].Text.ToString();
                e.Row.Cells[19].Text = e.Row.Cells[19].Text.ToString();
                e.Row.Height = 42;
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //SLNO
                e.Row.Cells[0].Text = "<div style='text-align:center;'>" + e.Row.Cells[0].Text.ToString() + "</div>";

                //BAY
               // e.Row.Cells[1].Text = "<div style=width:120px;>" + e.Row.Cells[1].Text.ToString() + "</div>";

                //TAG
                e.Row.Cells[2].Text = "<div style='text-align:LEFT;'>" + e.Row.Cells[2].Text.ToString() + "</div>";

                //REGNO
                e.Row.Cells[3].Text = "<div style=text-align:LEFT;>" + e.Row.Cells[3].Text.ToString() + "</div>";
                e.Row.Cells[3].Attributes.Add("onmouseover", "showRegNoHover(event,'" + RefNo + "')");
                e.Row.Cells[3].Attributes.Add("onmouseout", "hideTooltip(event)");

                //Type : JDP-CW
                try
                {
                    if (e.Row.Cells[4].Text.ToString() == "0-0")
                        e.Row.Cells[4].Text = "<div></div>";
                    else if (e.Row.Cells[4].Text.ToString() == "1-0")
                        e.Row.Cells[4].Text = "<div style=text-align:LEFT;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/JDP.png' Alt=''  width='16' height='16'/></div>";
                    else if (e.Row.Cells[4].Text.ToString() == "0-1")
                        e.Row.Cells[4].Text = "<div style=text-align:LEFT;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/Customer_Waiting.png' Alt=''  width='16' height='16'/></div>";
                    else if (e.Row.Cells[4].Text.ToString() == "1-1")
                        e.Row.Cells[4].Text = "<div style=text-align:LEFT;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/JDP_Customer_Waiting.png' Alt=''  width='16' height='16'/></div>";
                }
                catch (Exception ex)
                {
                }

                //MODEL
                try
                {
                    e.Row.Cells[5].ToolTip = e.Row.Cells[5].Text.ToString();//.Replace("<div style=width:60px;>", "").Replace("</div>", "");
                    if (e.Row.Cells[5].Text.Length > 10)
                        e.Row.Cells[5].Text = "<div style='text-align:right;WIDTH:100PX;'>" + e.Row.Cells[5].Text.ToString().Substring(0, 10) + "</div>";
                    else
                        e.Row.Cells[5].Text = "<div style='text-align:right;WIDTH:100PX;'>" + e.Row.Cells[5].Text.ToString() + "</div>";
                }
                catch (Exception ex)
                {
                }

                //S-T
                e.Row.Cells[6].Text = "<div style='text-align:right;'>" + e.Row.Cells[6].Text.ToString() + "</div>";
                //KMS
               
                e.Row.Cells[7].Text = "<div style=text-align:center;>" + e.Row.Cells[7].Text.ToString() + "</div>";
                
                //Parts
                string parts = "<div style=''><table style='width:100%;' border='0' cellpadding='0' cellspacing='0'><tr><td align='left' style='width: 20px;' >";

                if (e.Row.Cells[13].Text.ToString().Trim() == "0")
                {
                    parts = parts + "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484297131/images/Red.png' Alt='' width='16' height='16'/>";
                }
                else if (e.Row.Cells[13].Text.ToString().Trim() == "1")
                {
                    parts = parts + "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484297131/images/Green.png' Alt='' width='16' height='16'/>";
                }
                parts = parts + "</td><td align='left' style=''>";

                if (e.Row.Cells[14].Text.ToString().Trim() == "1")
                {
                    parts = parts + "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484297131/images/Red.png' Alt='' width='16' height='16'/>";
                }
                else if (e.Row.Cells[14].Text.ToString().Trim() == "2")
                {
                    parts = parts + "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484297131/images/Light.png' Alt='' width='16' height='16'/>";
                }
                else if (e.Row.Cells[14].Text.ToString().Trim() == "3")
                {
                    parts = parts + "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484297131/images/Green.png' Alt='' width='16' height='16'/>";
                }
                else if (e.Row.Cells[14].Text.ToString().Trim() == "4")
                {
                    parts = parts + "<img id='animg' src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484297131/images/Yellow_With_Near.png' Alt='' width='16' height='16'/>";
                }
                parts = parts + "</td></tr></table></div>";

                e.Row.Cells[13].Attributes.Add("onmouseover", "showPartsHover(event,'" + RefNo + "')");
                e.Row.Cells[13].Attributes.Add("onmouseout", "hideTooltip(event)");
                e.Row.Cells[13].Text = parts;

                //PTD Status : 0-Not Define, 1-Within Time, 2-Apporching Time, 3-Delayed
                try
                {
                    string getIFB = (e.Row.Cells[17].Text.Trim() == "&nbsp;" ? "" : e.Row.Cells[17].Text.Trim());
                    if (getIFB == "1")
                        e.Row.Cells[17].ToolTip = "On Time";
                    else if (getIFB == "0")
                        e.Row.Cells[17].ToolTip = "No PDT";
                    else if (getIFB == "2")
                        e.Row.Cells[17].ToolTip = "Approaching";
                    else
                        e.Row.Cells[17].ToolTip = "Delayed";

                    if (e.Row.Cells[17].Text.ToString() == "0")
                        e.Row.Cells[17].Text = "<div ></div>";
                    else if (e.Row.Cells[17].Text.ToString() == "1")
                        e.Row.Cells[17].Text = "<div ><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/circle_green.png' Alt=''  width='14' height='14'/></div>";
                    else if (e.Row.Cells[17].Text.ToString() == "2")
                        e.Row.Cells[17].Text = "<div ><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/circle_yellow.png' Alt=''  width='14' height='14'/></div>";
                    else if (e.Row.Cells[17].Text.ToString() == "3")
                        e.Row.Cells[17].Text = "<div ><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/circle_red.png' Alt=''  width='14' height='14'/></div>";
                }
                catch (Exception ex)
                { }

                //PDT
                try
                {
                    if (e.Row.Cells[18].Text.ToString().Contains('#'))
                    {
                        if (e.Row.Cells[18].Text.ToString().Contains('Y'))
                        {
                            e.Row.Cells[18].Text = "<table border=0 cellspacing=0 cellpadding=0 style=color:BLUE;><tr><td>" + e.Row.Cells[18].Text.Replace('#', ' ').Replace('Y', ' ').ToString().Trim() + "</td></tr></table>";
                        }
                        else
                        {
                            e.Row.Cells[18].Text = "<table border=0 cellspacing=0 cellpadding=0 style=color:RED;><tr><td>" + e.Row.Cells[18].Text.Replace('#', ' ').Replace('N', ' ').ToString().Trim() + "</td></tr></table>";
                        }
                    }
                    else
                    {
                        e.Row.Cells[18].Text = "<table border=0 cellspacing=0 cellpadding=0 style=color:GRAY;><tr><td>" + e.Row.Cells[18].Text.Replace('$', ' ').ToString().Trim() + "</td></tr></table>";
                    }
                }
                catch (Exception ex)
                { }

                //try
                //{
                //    e.Row.Cells[18].Text = "<div style=width:20px;>" + e.Row.Cells[18].Text.ToString() + "</div>";
                //}
                //catch (Exception ex)
                //{ }

                //Remarks : 0-No Remarks, 1-Remarks
                try
                {
                    if (e.Row.Cells[20].Text.Trim() == "1")
                    {
                        e.Row.Cells[20].Attributes.Add("onmouseover", "showRemarks(event,'" + RefNo + "')");
                        e.Row.Cells[20].Attributes.Add("onmouseout", "hideTooltip(event)");
                    }

                    if (e.Row.Cells[20].Text.ToString() == "0")
                        e.Row.Cells[20].Text = "<div style=width:40px;></div>";
                    else
                        e.Row.Cells[20].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/REMARKS_ENTRY.png' Alt=''  width='28' height='16'/></div>";
                }
                catch (Exception ex)
                { }
            }
        }
    }

    protected void grdDisplay_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        lbl_currTime.Text = DateTime.Now.ToString("dd MMM yyyy, HH:mm:ss");
        form1.Style.Value = "left:0px;";
        lblmsg.Text = "";
        lblmsg.CssClass = "reset";
        try
        {
            if (Session["ROLE"] == null || Session["ROLE"].ToString() != "WORK MANAGER" && Session["ROLE"].ToString() != "FRONT OFFICE" && Session["ROLE"].ToString() != "SERVICE ADVISOR" && Session["ROLE"].ToString() != "CRM" && Session["ROLE"].ToString() != "DISPLAY" && Session["ROLE"].ToString() != "GMSERVICE" && Session["ROLE"].ToString() != "SM" && Session["ROLE"].ToString() != "PARTS")
            {
                Response.Redirect("login.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("login.aspx");
        }
        srcServiceType.ConnectionString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        srcVehicleModel.ConnectionString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        SqlDataSource2.ConnectionString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        //SqlDataSource4.ConnectionString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        SqlDataSource5.ConnectionString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        try
        {
            //lbScroll0.Text = Session["DealerName"].ToString();
            if (!Page.IsPostBack)
            {
                TxtDate1.Attributes.Add("readonly", "readonly");
                TxtDate2.Attributes.Add("readonly", "readonly");
                getSA();
                getTL();
                lblRefnoParts.Text = "0";
                bindgridPartsStatus(Convert.ToInt32(lblRefnoParts.Text));
                TabContainer1.Visible = false;
                // PopupPanel.Visible = false;
                PopupPanel.Visible = true;
                ClientScriptManager script = Page.ClientScript;
                script.RegisterStartupScript(this.GetType(), "Alert", "FormWidth();", true);
                if (Page.Request.QueryString["Back"] != null)
                {
                    BackTo = Session["Role"].ToString();
                }
                else
                {
                    TabPanel1.Visible = true;
                    TabContainer1.ActiveTabIndex = 0;
                }

                //lbl_CurrentPage.Text = "Parts Display";
                lbl_LoginName.Text = Session["UserId"].ToString();
                try
                {
                    if (!Page.IsPostBack)
                    {
                    }
                    else
                    {
                        if (ScriptManager1.IsInAsyncPostBack)
                        {
                            if (Request["__EVENTTARGET"] == Timer1.ClientID)
                            {
                                BindGrid();
                                TabContainer1.Visible = false;
                                if (rbType.SelectedIndex == 0)
                                {
                                    LinkPrevVehicles("1");
                                }
                                else
                                {
                                    LinkPrevVehicles("0");
                                }
                                PnlDisplay.Update();
                            }
                            else
                            {
                                BindGrid();
                                TabContainer1.Visible = false;
                            }
                        }

                        if (!(Request.Form["_EventTarget"] != null && Request.Form["_EventTarget"] == "myClick"))
                        {
                            grdDisplay.SelectedIndex = int.Parse(Request.Form["__EVENTARGUMENT"].ToString());
                            if (grdDisplay.SelectedRow.Cells[2].Text.Replace("&nbsp;", "").Trim() != "")
                            {
                                string regno = GetRegNo(grdDisplay.SelectedRow.Cells[2].Text.Trim());
                                DataTable dt1 = GetAll(grdDisplay.SelectedRow.Cells[2].Text.Trim());
                                lblRefnoParts.Text = grdDisplay.SelectedRow.Cells[21].Text.Replace("&nbsp;", "").Trim();
                                lblStatus.Text = "";
                                lblStatus.CssClass = "reset";
                                PopupPanel.Visible = true;
                                bindgridPartsStatus(Convert.ToInt32(lblRefnoParts.Text));
                            }
                            else
                            {
                                //PopupPanel.Visible = false;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    //PopupPanel.Visible = false;
                }
            }

            try
            {
                if (ScriptManager1.IsInAsyncPostBack)
                {
                    if (Request["__EVENTTARGET"] == Timer1.ClientID)
                    {
                        BindGrid();
                        TabContainer1.Visible = false;
                        if (rbType.SelectedIndex == 0)
                        {
                            LinkPrevVehicles("1");
                        }
                        else
                        {
                            LinkPrevVehicles("0");
                        }
                        PnlDisplay.Update();
                    }
                    else
                    {
                        BindGrid();
                        TabContainer1.Visible = false;
                    }
                }
                else
                {
                    BindGrid();
                    if (rbType.SelectedIndex == 0)
                    {
                        LinkPrevVehicles("1");
                    }
                    else
                    {
                        LinkPrevVehicles("0");
                    }
                }
                if (!(Request.Form["_EventTarget"] != null && Request.Form["_EventTarget"] == "myClick"))
                {
                    BindGrid();
                    grdDisplay.SelectedIndex = int.Parse(Request.Form["__EVENTARGUMENT"].ToString());
                    if (grdDisplay.SelectedRow.Cells[2].Text.Replace("&nbsp;", "").Trim().Replace("<div style=width:26px;>", "").Replace("</div>", "") != "")
                    {
                        string regno = GetRegNo(grdDisplay.SelectedRow.Cells[2].Text.Replace("&nbsp;", "").Trim().Replace("<div style=width:26px;>", "").Replace("</div>", ""));
                        DataTable dt2 = GetAll(grdDisplay.SelectedRow.Cells[2].Text.Replace("&nbsp;", "").Trim().Replace("<div style=width:26px;>", "").Replace("</div>", ""));
                        lblRefnoParts.Text = grdDisplay.SelectedRow.Cells[21].Text.Replace("&nbsp;", "").Trim();
                        lblStatus.Text = "";
                        lblStatus.CssClass = "reset";
                        bindgridPartsStatus(Convert.ToInt32(lblRefnoParts.Text));
                        int SlNo = 0;
                        SlNo = Convert.ToInt16(grdDisplay.SelectedRow.Cells[21].Text.Replace("&nbsp;", "").Trim());
                        lblrefno.Text = grdDisplay.SelectedRow.Cells[21].Text.Replace("&nbsp;", "").Trim();
                        PopupPanel.Visible = true;
                    }
                    else
                    {
                        PopupPanel.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                if (!Page.IsPostBack)
                {
                    btnSearch_Click(null, null);
                    lblmsg.Text = "";
                    lblmsg.CssClass = "reset";
                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("login.aspx");
        }
    }

    protected void rbType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbType.SelectedIndex == 0)
        {
            LinkPrevVehicles("1");
        }
        else
        {
            LinkPrevVehicles("0");
        }

        try
        {
            txtVehicleNumber.Text = "";
            txtTagNo.Text = "";
            lblmsg.Text = "";
            lblmsg.CssClass = "reset";
            BindGrid();
            TabContainer1.Visible = false;
        }
        catch (Exception ex)
        {
            lblLoading.Text = ex.Message;
        }
    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {
        lblmsg.CssClass = "reset";
        lblStatus.CssClass = "reset";
        lblmsg.Text = "";
        Page_Load(null, null);
    }

    protected void Timer3_Tick(object sender, EventArgs e)
    {
        lblmsg.CssClass = "reset";
        lblStatus.CssClass = "reset";
        lblmsg.Text = "";
        lblmsg.ForeColor = Color.Green;
    }

    private static DataTable GetEmployeeHover(string Slno, string EmpId)
    {
        DataTable dt = new DataTable();
        SqlConnection con = new SqlConnection(System.Web.HttpContext.Current.Session[HttpContext.Current.Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlCommand cmd = new SqlCommand("GetEmployeeHover", con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Slno", Slno);
        cmd.Parameters.AddWithValue("@EmpId", EmpId);
        if (con.State != ConnectionState.Open)
            con.Open();
        cmd.ExecuteNonQuery();
        sda.Fill(dt);
        if (con.State != ConnectionState.Closed)
            con.Close();
        return dt;
    }

    private static DataTable GetProcessHover(string RefNo, string ProcessId)
    {
        DataTable dt = new DataTable();
        SqlConnection con = new SqlConnection(System.Web.HttpContext.Current.Session[HttpContext.Current.Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlCommand cmd = new SqlCommand("GetProcessHover", con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@RefNo", RefNo);
        cmd.Parameters.AddWithValue("@ProcessId", ProcessId);
        if (con.State != ConnectionState.Open)
            con.Open();
        cmd.ExecuteNonQuery();
        sda.Fill(dt);
        if (con.State != ConnectionState.Closed)
            con.Close();
        return dt;
    }

    private static DataTable GetRegNoHover(string RefNo)
    {
        DataTable dt = new DataTable();
        SqlConnection con = new SqlConnection(System.Web.HttpContext.Current.Session[HttpContext.Current.Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlCommand cmd = new SqlCommand("GetRefNoHover", con);
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

    private static DataTable GetJAHover(string RefNo)
    {
        DataTable dt = new DataTable();
        SqlConnection con = new SqlConnection(System.Web.HttpContext.Current.Session[HttpContext.Current.Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlCommand cmd = new SqlCommand("GetJAHover", con);
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

    private static DataTable GetPartsHover(string RefNo)
    {
        DataTable dt = new DataTable();
        SqlConnection con = new SqlConnection(System.Web.HttpContext.Current.Session[HttpContext.Current.Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlCommand cmd = new SqlCommand("udpHoverPartsRequisition", con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@RefId", RefNo);
        if (con.State != ConnectionState.Open)
            con.Open();
        cmd.ExecuteNonQuery();
        sda.Fill(dt);
        if (con.State != ConnectionState.Closed)
            con.Close();
        return dt;
    }

    private static DataTable GetSubProcessHover(string RefNo, string SubProcessId)
    {
        DataTable dt = new DataTable();
        SqlConnection con = new SqlConnection(System.Web.HttpContext.Current.Session[HttpContext.Current.Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlCommand cmd = new SqlCommand("GetSubProcessHover", con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@RefNo", RefNo);
        cmd.Parameters.AddWithValue("@SubProcessId", SubProcessId);
        if (con.State != ConnectionState.Open)
            con.Open();
        cmd.ExecuteNonQuery();
        sda.Fill(dt);
        if (con.State != ConnectionState.Closed)
            con.Close();
        return dt;
    }

    private int CheckPromisedTime(String RegNo)
    {
        int RefNo = DataManager.GetRefNo(RegNo,Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlCommand cmd = new SqlCommand("", con);
        cmd.CommandText = "SELECT PromisedTime FROM tblMaster WHERE slno=@RefNo";
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@RefNo", RefNo);
        DateTime pt;
        try
        {
            if (con.State != ConnectionState.Open)
                con.Open();

            pt = Convert.ToDateTime(cmd.ExecuteScalar());
            if (pt < DateTime.Now)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }
        catch
        {
            return 0;
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }

    private void FillVehicleStatus()
    {
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        try
        {
            SqlCommand cmd = new SqlCommand();
            if (Session["ROLE"].ToString() == "SERVICE ADVISOR")
            {
                cmd.CommandText = "GetCountVehicleStatusIForParts";
                cmd.Parameters.AddWithValue("@EmpId", Session["EmpId"].ToString());
                cmd.Parameters.AddWithValue("@isBodyshop", 0);
            }
            else
            {
                cmd.CommandText = "GetCountVehicleStatusIForParts";
                cmd.Parameters.AddWithValue("@EmpId", "0");
                cmd.Parameters.AddWithValue("@isBodyshop", 0);
            }
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            if (con.State != ConnectionState.Open)
                con.Open();

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count != 0)
            {
                lbUnDelivered.Text = dt.Rows[0][0].ToString().Trim();
                lbReady.Text = dt.Rows[0][1].ToString().Trim();
                lbWIP.Text = dt.Rows[0][9].ToString().Trim();
                lbIdle.Text = dt.Rows[0][2].ToString().Trim();
                lbHold.Text = dt.Rows[0][3].ToString().Trim();
            }
            else
            {
                lbUnDelivered.Text = "0";
                lbReady.Text = "0";
                lbIdle.Text = "0";
                lbHold.Text = "0";
                lbWIP.Text = "0";
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

    private DataTable GetAll(string TagNo)
    {
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        DataTable dt = new DataTable();
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select Top 1 RegNo,CustomerName,CustomerPhone,LandlineNo, Email, Slno,ServiceAdvisor from tblMaster where RFID=@TagNo And Delivered=0 order by SlNo desc";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@TagNo", TagNo);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count != 0)
            {
                return dt;
            }
            else
            {
                return dt;
            }
        }
        catch (Exception ex)
        {
            return dt;
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }

    private string getCurrentProcessInTime(GridViewRowEventArgs e)
    {
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlCommand cmd = new SqlCommand("GetCurrentProcessInTime", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@TagNo", e.Row.Cells[2].Text.ToString());
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        return dt.Rows[0]["Code"].ToString();
    }

    private DataTable GetDisplayDate(string Type, string vehiclenumber, int param)
    {
        string whichDay = string.Empty;
        if (rbType.SelectedIndex == 0)
        {
            whichDay = "1"; // Today
        }
        else
        {
            whichDay = "0"; // Next Day
        }

        string Aplus = string.Empty;
        switch (cmbCustType.SelectedValue)
        {
            case "Customer Type":
                Aplus = "";
                break;

            default:
                Aplus = cmbCustType.SelectedValue;
                break;
        }
        string ServiceTyp = string.Empty;
        if (cmbServiceType.SelectedValue == "Service Type")
        {
            ServiceTyp = "";
        }
        else
        {
            ServiceTyp = cmbServiceType.SelectedValue;
        }

        string vehicleModel = string.Empty;
        if (cmbVehicleModel.SelectedValue == "Model")
        {
            vehicleModel = "";
        }
        else
        {
            vehicleModel = cmbVehicleModel.SelectedValue;
        }

        string ProcessVal = string.Empty;
        if (cmbProcess.SelectedValue == "Process")
        {
            ProcessVal = "";
        }
        else
        {
            ProcessVal = cmbProcess.SelectedValue;
        }

        string DateFrom = string.Empty;
        if (TxtDate1.Text == "")
        {
            DateFrom = "";
        }
        else
        {
            DateFrom = TxtDate1.Text.Trim();
        }

        string DateTo = string.Empty;
        if (TxtDate2.Text == "")
        {
            DateTo = "";
        }
        else
        {
            DateTo = TxtDate2.Text.Trim();
        }

        string VehicleNo = string.Empty;
        if (txtVehicleNumber.Text.Trim() != "")
        {
            VehicleNo = txtVehicleNumber.Text.Trim();
        }
        else
        {
            VehicleNo = "";
        }

        string SAId = string.Empty;

        if (cmbSA.SelectedIndex > 0)
        {
            SAId = cmbSA.SelectedValue.ToString();
        }
        else
            SAId = "0";
        string TLId = string.Empty;
        if (cmbTeamLead.SelectedIndex > 0)
        {
            TLId = cmbTeamLead.SelectedValue.ToString();
        }
        else
            TLId = "0";

        string TagNo = string.Empty;
        if (txtTagNo.Text.Trim() == "")
            TagNo = "0";
        else
            TagNo = txtTagNo.Text.Trim();

        if (Session["ROLE"].ToString() == "SERVICE ADVISOR")
        {
            SAId = Session["EmpId"].ToString();
        }
        if (Session["ROLE"].ToString() == "CRM")
        {
            EmpId = Session["EmpId"].ToString();
        }
        
        SqlConnection oConn = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlCommand cmd = new SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "JCDisplayI_New";
        cmd.Parameters.AddWithValue("@Day", whichDay);
        cmd.Parameters.AddWithValue("@Bodyshop", 0);
        cmd.Parameters.AddWithValue("@Floor", 0);
        cmd.Parameters.AddWithValue("@CustomerType", Aplus);
        cmd.Parameters.AddWithValue("@ServiceType", ServiceTyp);
        cmd.Parameters.AddWithValue("@Model", vehicleModel);
        cmd.Parameters.AddWithValue("@Process", ProcessVal);
        cmd.Parameters.AddWithValue("@DateFrom", DateFrom);
        cmd.Parameters.AddWithValue("@DateTo", DateTo);
        cmd.Parameters.AddWithValue("@RegNo", VehicleNo);
        cmd.Parameters.AddWithValue("@Param", "");
        cmd.Parameters.AddWithValue("@SAId", SAId);
        cmd.Parameters.AddWithValue("@TLId", TLId);
        cmd.Parameters.AddWithValue("@Tagno", TagNo);
        cmd.Parameters.AddWithValue("@Status", ddlState.SelectedValue.Trim());
        cmd.Parameters.AddWithValue("@indexOn", drpOrderBy.SelectedValue.Trim());
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

    private string[] GetProcessState(string ProcessString)
    {
        try
        {
            return (ProcessString.Split('|'));
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }

    private string GetRegNo(string TagNo)
    {
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select Top 1 RegNo from tblMaster where RFID=@TagNo And Delivered=0 order by SlNo desc";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@TagNo", TagNo);
            cmd.Connection = con;
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
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

    private void getSAColors(string colorCode, int ColNo, GridViewRowEventArgs e)
    {
        switch (colorCode)
        {
            case "0":
                e.Row.Cells[ColNo].Text = "";
                break;

            case "1":
                e.Row.Cells[ColNo].Attributes.Add("Style", "font-size: large;color:#387C44;background-image: url('img/Process/clock_green.png'); background-repeat: no-repeat;background-size:24px 24px;");
                break;

            case "2":
                e.Row.Cells[ColNo].Attributes.Add("Style", "font-size: large;color:#387C44;background-image: url('img/Process/clock_red.png'); background-repeat: no-repeat;background-size:24px 24px;");
                break;
        }
    }

    private string GetServiceid(string RegNo)
    {
        SqlConnection Conn = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlDataReader dr = null;
        String ServiceID = string.Empty;
        SqlCommand Cmd = new SqlCommand("", Conn);
        try
        {
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "GetServiceIdForRegNo_H";
            Cmd.Parameters.Clear();
            Cmd.Parameters.AddWithValue("@RegNo", RegNo.Replace("<font size='4' color='red'> *</font>", ""));
            if (Conn.State != ConnectionState.Open)
            {
                Conn.Open();
            }
            dr = Cmd.ExecuteReader();
            if (dr.Read())
            {
                ServiceID = Convert.ToString(dr["ServiceId"].ToString());
            }
            else
            {
                ServiceID = string.Empty;
            }
            return ServiceID;
        }
        catch (Exception ex)
        {
            return string.Empty;
        }
        finally
        {
            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
            }
        }
    }

    private string GetServiceTypeImage(string ServiceType)
    {
        string sConnString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        SqlConnection oConn = new SqlConnection(sConnString);
        SqlCommand cmd = new SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "GetServiceTypeImage";
        cmd.Parameters.AddWithValue("@ServiceType", ServiceType);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            if (oDt.Rows.Count > 0)
            {
                return oDt.Rows[0]["Image"].ToString();
            }
            else
            {
                return string.Empty;
            }
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

    private string GetServiceTypeImg(string ServiceType)
    {
        try
        {
            string ImgUrl = string.Empty;
            ImgUrl = GetServiceTypeImage(ServiceType);
            if (ImgUrl != string.Empty)
            {
                return ("<img src=\"img/" + ImgUrl + "\" width=\"48px\" height=\"48px\" />");
            }
            else
            {
                return ServiceType;
            }
        }
        catch (Exception ex)
        {
            return "";
        }
        finally
        {
        }
    }

    private String GetTechnicianForId(string ServiceId)
    {
        DataTable dt = new DataTable();
        try
        {
            String query = "Select dbo.GetTechnicianForService(@ServiceId) As Technician";
            SqlCommand cmd = new SqlCommand(query, new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString()));
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ServiceId", ServiceId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["Technician"].ToString();
            }
            else
            {
                return string.Empty;
            }
        }
        catch (Exception ex)
        {
            return string.Empty;
        }
    }

    private int InsertRemarks(string Remarks, string ProcessId, string ServiceId, string RegNo)
    {
        SqlConnection Conn = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());

        try
        {
            if (Conn.State != ConnectionState.Open)
            {
                Conn.Open();
            }
            SqlCommand cmd = new SqlCommand("", Conn);
            cmd.CommandText = "INSERT INTO TblProcessRemarks (ServiceId, ProcessId, RegNo, DateOfRemarks, Remarks)VALUES" +
            "(@ServiceId, @ProcessId, @RegNo, GETDATE(), @Remarks)";
            cmd.Parameters.AddWithValue("@ServiceId", ServiceId);
            cmd.Parameters.AddWithValue("@ProcessId", ProcessId);
            cmd.Parameters.AddWithValue("@RegNo", RegNo.Replace("<font size='4' color='red'> *</font>", ""));
            cmd.Parameters.AddWithValue("@Remarks", Remarks);
            return (cmd.ExecuteNonQuery());
        }
        catch
        {
            return 0;
        }
        finally
        {
            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
            }
        }
    }

    private void LinkPrevVehicles(string typeLink)
    {
        if (typeLink == "1")
        {
            imgType.HRef = "DisplayWorks.aspx?typ=1";
        }
        else
        {
            imgType.HRef = "DisplayWorks.aspx?typ=0";
        }
    }

    private void OnIButtonClick(object Sender, EventArgs e)
    {
        string VehicleRegNo = string.Empty;
        string ProcessData = string.Empty;
        try
        {
            GridViewRow Gr = (GridViewRow)(Sender as Control).Parent.Parent;
            int Index = Gr.RowIndex;
            VehicleRegNo = Gr.Cells[0].Text;
            // Get Parent Cell
            TableCell Tc = (TableCell)(Sender as Control).Parent;
            if (Tc.Controls.Count == 2)
            {
                HiddenField Hf = (HiddenField)Tc.Controls[1];
                ProcessData = Hf.Value;
            }
            Timer1.Enabled = false;
        }
        catch (Exception ex)
        {
        }
    }

    private void OnRemarksButtonClick(object Sender, EventArgs e)
    {
        string Slno = string.Empty;
        string ProcessData = string.Empty;

        try
        {
            GridViewRow Gr = (GridViewRow)(Sender as Control).Parent.Parent;
            Slno = Gr.Cells[0].Text;
            Timer1.Enabled = false;
        }
        catch (Exception ex)
        {
        }
    }

    private void ShowPopup(string ProcessId, string RegNo, string DelayMessage)
    {
        string sConnString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        SqlConnection oConn = new SqlConnection(sConnString);
        SqlCommand cmd = new SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "ProcessRemarkPopup";
        cmd.Parameters.AddWithValue("@ProcessId", ProcessId);
        cmd.Parameters.AddWithValue("@RegNo", RegNo.Replace("<font size='4' color='red'> *</font>", ""));
        cmd.Parameters.AddWithValue("@DelayMessage", DelayMessage);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            if (oDt.Rows.Count == 0)
            {
                return;
            }
            string ProcessName = string.Empty;
            string ProcessTime = string.Empty;
            string Deviation = string.Empty;

            ProcessName = oDt.Rows[0]["ProcessName"].ToString();
            ProcessTime = oDt.Rows[0]["ProcessTime"].ToString();
            Deviation = oDt.Rows[0]["IdleTime"].ToString();
        }
        catch (SqlException ex)
        {
            lblErrormsg.Text = "Error,PopUp:" + ex.Message;
            lblErrormsg.ForeColor = Color.Red;
        }
        finally
        {
            oConn.Close();
        }
    }

    #region "HOVERING"

    [System.Web.Services.WebMethod(EnableSession = true)]
    public static string LoadEmployeeInOutTime(string Slno, string EmpId, string Tech)
    {
        DataTable dt = new DataTable();
        dt = GetEmployeeHover(Slno, EmpId);
        string inTime = "";
        string outTime = "";
        string EmpName = "";
        if (dt.Rows.Count > 0)
        {
            inTime = dt.Rows[0][0].ToString().Replace("#", " ");
            outTime = dt.Rows[0][1].ToString().Replace("#", " ");
            EmpName = dt.Rows[0][2].ToString();
        }

        string str = "<span><table id=InOutPnl style=width:100%;><tr style=text-align:left;color:steelblue;font-weight:bold;background-color:silver;><td colspan=2>" + Tech + ": " + EmpName + "</td></tr><tr><td style=width:50px;>In Time: </td><td style=text-align:left;>" + inTime + "</td></tr><tr><td style=width:50px;>Out Time: </td><td style=text-align:left;>" + outTime + "</td></tr></table></span>";
        return str;
    }

    [System.Web.Services.WebMethod(EnableSession = true)]
    public static string LoadInOutTime(string ServiceId, string ProcessName)
    {
        DataTable dt = new DataTable();
        dt = GetInOutTime(ServiceId, ProcessName);
        string inTime = "";
        string outTime = "";
        if (dt.Rows.Count > 0)
        {
            inTime = dt.Rows[0][0].ToString().Replace("#", " ");
            outTime = dt.Rows[0][1].ToString().Replace("#", " ");
        }

        string str = "<span><table id=InOutPnl style=width:100%;><tr><td style=width:50px;>In Time: </td><td style=text-align:left;>" + inTime + "</td></tr><tr><td style=width:50px;>Out Time: </td><td style=text-align:left;>" + outTime + "</td></tr></table></span>";
        return str;
    }

    [System.Web.Services.WebMethod(EnableSession = true)]
    public static string LoadProcessInOutTime(string RefNo, string ProcessId, string ProcessName)
    {
        DataTable dt = new DataTable();
        dt = GetProcessHover(RefNo, ProcessId);
        string inTime = "";
        string outTime = "";
        string empName = "";
        if (dt.Rows.Count > 0)
        {
            inTime = dt.Rows[0][0].ToString().Replace("#", " ");
            outTime = dt.Rows[0][1].ToString().Replace("#", " ");
            empName = dt.Rows[0][2].ToString().Trim();
        }

        string str = "<span><table id=InOutPnl style=width:100%;><tr style=text-align:left;color:steelblue;font-weight:bold;background-color:silver;><td colspan=2>" + ProcessName + " : " + empName + "</td></tr><tr><td style=width:50px;>In Time: </td><td style=text-align:left;>" + inTime + "</td></tr><tr><td style=width:50px;>Out Time: </td><td style=text-align:left;>" + outTime + "</td></tr></table></span>";
        return str;
    }

    [System.Web.Services.WebMethod(EnableSession = true)]
    public static string LoadRegNoHover(string RefNo)
    {
        DataTable dt = new DataTable();
        dt = GetRegNoHover(RefNo);

        //string tt1 = ((dt.Rows[0]["RegNo"].ToString().Trim() == "") ? "___________" : dt.Rows[0]["RegNo"].ToString());
        string tt3 = dt.Rows[0]["CustomerName"].ToString();
        string tt4 = dt.Rows[0]["CustomerPhone"].ToString();
        string tt5 = dt.Rows[0]["VehicleModel"].ToString();
        string tt6 = dt.Rows[0]["GateIn"].ToString().Replace("#", " ");
        string tt7 = dt.Rows[0]["Position"].ToString();
        string tt8 = dt.Rows[0]["JobCardNo"].ToString();
        string tt9 = dt.Rows[0]["ChassisNo"].ToString();
        string tt10 = dt.Rows[0]["KMS"].ToString();
        string tt11 = dt.Rows[0]["ServiceAdvisor"].ToString().Replace(",", "");
        string tt12 = dt.Rows[0]["EngineNo"].ToString();
        string tt13 = dt.Rows[0]["Date_Of_Sale"].ToString();
        string tt14 = dt.Rows[0]["Sold_Dealer_Id"].ToString();
        string tt15 = dt.Rows[0]["CurrentStatus"].ToString();
        string tt16 = dt.Rows[0]["GateOut"].ToString();
        string tt17 = dt.Rows[0]["KMS"].ToString();
        string str;
        //if (statusVal == "13" || statusVal == "14" || statusVal == "15")
        //    str = "<span><table style='width: 210px;' cellpadding='0' cellspacing='0'><tr bgcolor='#8db4e3'><td style='width: 100px;'>REGISTRATION NO</td><td>:</td><td class='ttipBodyVal'>" + tt1 + "</td></tr><tr><td>CUSTOMER NAME</td><td>:</td><td class='ttipBodyVal'>" + tt3 + "</td><CONTACT NO</td><tr bgcolor='#8db4e3'><td><span>CONTACT NO</span></td><td>:</td><td class='ttipBodyVal'><span>" + tt4 + "</span></td></tr><tr><td style='width: 100px;'>VEHICLE IN TIME</td><td>:</td><td class='ttipBodyVal'>" + tt6 + "</tr><tr bgcolor='#8db4e3'><td style='width: 100px;'>MODEL</td><td>:</td><td class='ttipBodyVal'>&nbsp;" + tt5 + "</tr><tr><td style='width: 100px;'>ADVISOR NAME</td><td>:</td><td class='ttipBodyVal'>" + tt11 + "</tr><tr bgcolor='#8db4e3'><td >CURRENT POSITION</td><td>:</td><td>" + tt7 + "</tr><tr><td style='width: 100px;'>CURRENT STATUS</td><td>:</td><td class='ttipBodyVal'>" + tt15 + "</tr><tr bgcolor='#8db4e3'><td >VEHICLE OUT TIME</td><td>:</td><td>" + tt17 + "</tr></table></span>";
        //else
        //    str = "<span><table style='width: 210px;' cellpadding='0' cellspacing='0'><tr bgcolor='#8db4e3'><td style='width: 100px;'>REGISTRATION NO</td><td>:</td><td class='ttipBodyVal'>" + tt1 + "</td></tr><tr><td>CUSTOMER NAME</td><td>:</td><td class='ttipBodyVal'>" + tt3 + "</td><CONTACT NO</td><tr bgcolor='#8db4e3'><td><span>CONTACT NO</span></td><td>:</td><td class='ttipBodyVal'><span>" + tt4 + "</span></td></tr><tr><td style='width: 100px;'>VEHICLE IN TIME</td><td>:</td><td class='ttipBodyVal'>" + tt6 + "</tr><tr bgcolor='#8db4e3'><td style='width: 100px;'>MODEL</td><td>:</td><td class='ttipBodyVal'>&nbsp;" + tt5 + "</tr><tr><td style='width: 100px;'>ADVISOR NAME</td><td>:</td><td class='ttipBodyVal'>" + tt11 + "</tr><tr bgcolor='#8db4e3'><td >CURRENT POSITION</td><td>:</td><td>" + tt7 + "</tr><tr><td style='width: 100px;'>CURRENT STATUS</td><td>:</td><td class='ttipBodyVal'>" + tt15 + "</tr><tr><td style='width: 100px;'>CURRENT KMS</td><td>:</td><td class='ttipBodyVal'>" + tt17 + "</tr></table></span>";
        if (tt7 == "Delivered")
            str = "<table class='mydatagrid'><tr><td ><span style='color:#a62724 !important;width:120px'>CUSTOMER NAME</span></td><td>:</td><td>" + tt3 + "</td></tr><tr><td><span style='color:#a62724 !important;width:100px'>CONTACT NO</span></td><td>:</td><td>" + tt4 + "</td></tr><tr style='height:18px'><td><span style='color:#a62724 !important;width:60px'>IN TIME</span></td><td>:</td><td>" + tt6 + "</td></tr><tr style='height:18px'><td><span style='color:#a62724 !important;width:70px'>SA NAME</span></td><td>:</td><td>" + tt11 + "</td></tr><tr style='height:18px'><td><span style='color:#a62724 !important;width:55px'>STAGE</span></td><td>:</td><td>" + tt7 + "</td></tr><tr style='height:18px'><td><span style='color:#a62724 !important;'>OUT TIME</span></td><td>:</td><td>" + tt16 + "</td></tr><tr style='height:18px'><td><span style='color:#a62724 !important;'>KMS</span></td><td>:</td><td>" + tt10 + "</td></tr></table></span>";
        else
            str = "<table class='mydatagrid'><tr><td><span style='color:#a62724 !important;width:120px'> CUSTOMER NAME</span></td><td>:</td><td>" + tt3 + "</td></tr><tr><td> <span style='color:#a62724 !important;width:100px'>CONTACT NO</span></td><td>:</td><td>" + tt4 + "</td></tr><tr style='height:18px'><td><span style='color:#a62724 !important;width:60px'>IN TIME</span></td><td>:</td><td>" + tt6 + "</td></tr><tr style='height:18px'><td><span style='color:#a62724 !important;width:70px'>SA NAME</span></td><td>:</td><td>" + tt11 + "</td></tr><tr style='height:18px'><td><span style='color:#a62724 !important;width:55px'>STAGE</span></td><td>:</td><td>" + tt7 + "</td></tr><tr style='height:18px'><td><span style='color:#a62724 !important;'>KMS</span></td><td>:</td><td>" + tt10 + "</td></tr></table></span>";
        return str;
    }

    [System.Web.Services.WebMethod(EnableSession = true)]
    public static string LoadRemarks(string RefNo)
    {
        DataTable dt = new DataTable();
        dt = GetRemarksHover(RefNo);
        string str = "";
        if (dt.Rows.Count > 0)
        {
            str = "<span><table><tr style=text-align:left;color:steelblue;font-weight:bold;background-color:silver;><th>Time</th><th>Remarks</th></tr>";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                str += "<tr><td>" + dt.Rows[i][0].ToString().Replace("#", " ") + "</td><td>" + dt.Rows[i][1].ToString() + "</td></tr>";
            }
            str += "</table></span>";
        }
        else
        {
            str = "<table style=width:100px;text-align:left;><tr><th>No Remarks</th></tr></table>";
        }
        return str;
    }

    [System.Web.Services.WebMethod(EnableSession = true)]
    public static string LoadJADetails(string RefNo)
    {
        DataTable dt = new DataTable();
        dt = GetJAHover(RefNo);
        string str = "<table cellpadding='0' cellspacing='0' border='0'><tr bgcolor='#E4E4E4'><td><strong>Bay #</strong></td><td></td><td align='left' valign='middle'><strong>Team Lead</strong></td><td align='left' valign='middle'></td><td align='left' valign='middle'><strong>In Time</strong></td><td align='left' valign='middle'></td><td align='left' valign='middle'><strong>Allotted</strong></td></tr><tr><td>" + dt.Rows[0][0].ToString() + "</td><td></td><td align='left' valign='middle'>" + dt.Rows[0][3].ToString() + "</td><td align='left' valign='middle'></td><td align='left' valign='middle'> " + dt.Rows[0][1].ToString() + "</td><td align='left' valign='middle'></td><td align='left' valign='middle'>" + dt.Rows[0][2].ToString() + "</td></tr><tr bgcolor='White'><td></td><td></td><td align='left' valign='middle'></td><td align='left' valign='middle'></td><td align='left' valign='middle'></td><td align='left' valign='middle'></td><td align='left' valign='middle'></td></tr><tr bgcolor='#E4E4E4'><td><strong>Technician</strong></td><td width='10px'></td><td align='left' valign='middle'>" + dt.Rows[0][4].ToString() + "</td><td align='left' valign='middle' width='10px'></td><td align='left' valign='middle'>" + dt.Rows[0][7].ToString() + "</td><td align='left' valign='middle' width='10px'></td><td align='left' valign='middle'>" + dt.Rows[0][10].ToString() + "</td></tr><tr><td bgcolor='#E4E4E4'><strong>Jobs</strong></td><td bgcolor='#E4E4E4'></td><td align='left' valign='middle'>" + dt.Rows[0][5].ToString() + "</td><td align='left' valign='middle'></td><td align='left' valign='middle'>" + dt.Rows[0][8].ToString() + "</td><td align='left' valign='middle'></td><td align='left' valign='middle'>" + dt.Rows[0][11].ToString() + "</td></tr><tr><td  bgcolor='#E4E4E4'><strong>Allotted</strong></td><td  bgcolor='#E4E4E4'></td><td align='left' valign='middle'>" + dt.Rows[0][6].ToString().Replace("0", "") + "</td><td align='left' valign='middle'></td><td align='left' valign='middle'> " + dt.Rows[0][9].ToString().Replace("0", "") + "</td><td align='left' valign='middle'></td><td align='left' valign='middle'>" + dt.Rows[0][12].ToString().Replace("0", "") + "</td></tr></table>";

        return str;
    }

    [System.Web.Services.WebMethod(EnableSession = true)]
    public static string LoadPartsDetails(string RefNo)
    {
        DataTable dt = new DataTable();
        dt = GetPartsHover(RefNo);
        string str = "";
        string AllotedTime="";
        if (dt.Rows.Count > 0)
        {
            //if (dt.Rows[0][1].ToString() == "N")
            //{
            //    AllotedTime = dt.Rows[0][2].ToString().Replace("#", " ");
            //}
           
            str = "<table class='mydatagrid'><tr ><td style='color:#000 !important;'><strong>PARTS NAME</strong></td><td style='width:50px;color:#000 !important;' align='left'><strong>STATUS</strong></td><td style='width:140px;color:#000 !important;' align='left'><strong>AVAILABILITY TIME</strong></td></tr>";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i % 2 == 0)
                {
                    if (dt.Rows[i]["Alloted"].ToString() == "Y")
                    {
                        AllotedTime = "";
                        str = str + "<tr style='height:20px;' bgcolor='#E4E4E4'><td style='padding-left:3px;'>" + dt.Rows[i]["PartsName"].ToString() + "</td><td style='width:50px;' align='left'>" + dt.Rows[i]["Alloted"].ToString().Replace("N", "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/No.png' Alt=''  width='16' height='16'/>").Replace("Y", "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/CN.png' Alt=''  width='16' height='16'/>") + "</td><td style='padding-left:3px;' align='left'>" + AllotedTime + "</td></tr>";
                    }
                    else
                    {
                        AllotedTime = dt.Rows[i][2].ToString().Replace("#", " ");
                        str = str + "<tr style='height:20px;' bgcolor='#E4E4E4'><td style='padding-left:3px;'>" + dt.Rows[i]["PartsName"].ToString() + "</td><td style='width:50px;' align='left'>" + dt.Rows[i]["Alloted"].ToString().Replace("N", "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/No.png' Alt=''  width='16' height='16'/>").Replace("Y", "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/CN.png' Alt=''  width='16' height='16'/>") + "</td><td style='padding-left:3px;' align='left'>" + AllotedTime + "</td></tr>";
                    }
                }
                else
                {
                    //str = str + "<tr style='height:20px;' bgcolor='#FFFFFF'><td style='padding-left:3px;'>" + dt.Rows[i]["PartsName"].ToString() + "</td><td style='width:50px;' align='left'>" + dt.Rows[i]["Alloted"].ToString().Replace("N", "<img src='images/JCR/No.png' Alt=''  width='16' height='16'/>").Replace("Y", "<img src='images/JCR/CN.png' Alt=''  width='16' height='16'/>") + "</td><td style='padding-left:3px;' align='left'>" + AllotedTime + "</td></tr>";
                    if (dt.Rows[i]["Alloted"].ToString() == "Y")
                    {
                        AllotedTime = "";
                        str = str + "<tr style='height:20px;' bgcolor='#FFFFFF'><td style='padding-left:3px;'>" + dt.Rows[i]["PartsName"].ToString() + "</td><td style='width:50px;' align='left'>" + dt.Rows[i]["Alloted"].ToString().Replace("N", "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/No.png' Alt=''  width='16' height='16'/>").Replace("Y", "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/CN.png' Alt=''  width='16' height='16'/>") + "</td><td style='padding-left:3px;' align='left'>" + AllotedTime + "</td></tr>";
                    }
                    else
                    {
                        AllotedTime = dt.Rows[i][2].ToString().Replace("#", " ");
                        str = str + "<tr style='height:20px;' bgcolor='#FFFFFF'><td style='padding-left:3px;'>" + dt.Rows[i]["PartsName"].ToString() + "</td><td style='width:50px;' align='left'>" + dt.Rows[i]["Alloted"].ToString().Replace("N", "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/No.png' Alt=''  width='16' height='16'/>").Replace("Y", "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/CN.png' Alt=''  width='16' height='16'/>") + "</td><td style='padding-left:3px;' align='left'>" + AllotedTime + "</td></tr>";
                    }
                }
            }
            str = str + "</table>";
        }
        else
        {
            str = "";
        }
        return str;
    }

    [System.Web.Services.WebMethod(EnableSession = true)]
    public static string LoadSubProcessInOutTime(string RefNo, string SubProcessId, string SubProcessName)
    {
        DataTable dt = new DataTable();
        dt = GetSubProcessHover(RefNo, SubProcessId);
        string inTime = "";
        string outTime = "";
        string empName = "";
        if (dt.Rows.Count > 0)
        {
            inTime = dt.Rows[0][0].ToString().Replace("#", " ");
            outTime = dt.Rows[0][1].ToString().Replace("#", " ");
            empName = dt.Rows[0][2].ToString().Trim();
        }

        string str = "<span><table id=InOutPnl style=width:100%;><tr style=text-align:left;color:steelblue;font-weight:bold;background-color:silver;><td colspan=2>" + SubProcessName + " : " + empName + "</td></tr><tr><td style=width:50px;>In Time: </td><td style=text-align:left;>" + inTime + "</td></tr><tr><td style=width:50px;>Out Time: </td><td style=text-align:left;>" + outTime + "</td></tr></table></span>";
        return str;
    }

    #endregion "HOVERING"

    protected void drpOrderBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (drpOrderBy.SelectedIndex != -1)
            {
                cmbCustType.SelectedIndex = -1;
                cmbServiceType.SelectedIndex = -1;
                cmbVehicleModel.SelectedIndex = -1;
                TxtDate1.Text = "";
                TxtDate2.Text = "";
                txtVehicleNumber.Text = "";
                txtTagNo.Text = "";
                lblmsg.Text = "";
                lblmsg.CssClass = "reset";
            }
            BindGrid();
            TabContainer1.Visible = false;
        }
        catch (Exception ex)
        {
        }
    }

    protected void btn_Close_Click(object sender, EventArgs e)
    {
        PopupPanel.Visible = true;
        //PopupPanel.Visible = false;
    }

    private void bindgridPartsStatus(int RefNo)
    {
        try
        {
            // string str = "select SlNo,PartsName,Alloted from tblPartsRequisition where RefNo=" + RefNo;
            string sConnString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
            SqlConnection con = new SqlConnection(sConnString);
            SqlCommand cmd = new SqlCommand("udpGetPartsRequisitionlist", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RefId", RefNo);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                btn_SaveParts.Visible = true;
                //btn_Close.Visible = true;
                GridView3.DataSource = dt;
                GridView3.DataBind();
            }
            else
            {
                btn_SaveParts.Visible = false;
                //btn_Close.Visible = true;
                GridView3.DataSource = null;
                GridView3.DataBind();
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void GridView3_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
    }

    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[0].Width = new Unit(300);
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            if (e.Row.Cells[2].Text.Length > 15)
                e.Row.Cells[2].Text = e.Row.Cells[2].Text.Substring(0, 14);
            else
                e.Row.Cells[2].Text = e.Row.Cells[2].Text;
        }
    }

    protected void btn_SaveParts_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        int ErrorMsg = 0;
        try
        {
            for (int i = 0; i < GridView3.Rows.Count; i++)
            {
                System.Web.UI.WebControls.CheckBox chk1 = (System.Web.UI.WebControls.CheckBox)GridView3.Rows[i].Cells[3].Controls[1];
                System.Web.UI.WebControls.TextBox Date = (System.Web.UI.WebControls.TextBox)GridView3.Rows[i].Cells[4].FindControl("txtPartsDate");
                //System.Web.UI.WebControls.TextBox Time = (System.Web.UI.WebControls.TextBox)GridView3.Rows[i].Cells[3].FindControl("txtPartsTime");
                System.Web.UI.WebControls.TextBox Remarks= (System.Web.UI.WebControls.TextBox)GridView3.Rows[i].Cells[1].FindControl("txtPartsNO");
                if (chk1.Checked.ToString() == "False" && Date.Text.ToString() == "")
                {
                    lblStatus.Text = "Please enter Available Date and Time.!";
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblStatus.ClientID + "').style.display='none'\",5000)</script>");
                    lblStatus.CssClass = "ErrMsg";
                    lblStatus.Attributes.Add("style", "text-transform:none !important");
                    ErrorMsg = 1;
                    break;
                }
                //else if(chk1.Checked.ToString() == "False" && Remarks.Text.ToString() == "")
                //{
                //    lblStatus.Text = "Please Enter Remarks.!";
                //    ErrorMsg = 1;
                //    break;
                //}
                //else if (Remarks.Text == "")
                //{
                //    ErrorMsg = 1;
                //    lblStatus.Text = "Please Enter Parts Number.!";
                //    lblStatus.CssClass = "ErrMsg";
                //    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblStatus.ClientID + "').style.display='none'\",5000)</script>");
                    
                //    break;
                //}
                else
                {
                    SqlCommand cmd = new SqlCommand("udpUpdatePartsRequisition", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SlNo", Convert.ToInt32(GridView3.Rows[i].Cells[0].Text.Replace("&nbsp;", "").Trim()));
                    cmd.Parameters.AddWithValue("@Alloted", chk1.Checked.ToString().Replace("True", "Y").Replace("False", "N"));
                    if (Date.Text.Trim()=="")
                        cmd.Parameters.AddWithValue("@AllotedDate",DBNull.Value);
                    else
                    {
                       // DateTime pt = Convert.ToDateTime(Date.Text.Trim() + " " + Time.Text.Trim());
                        cmd.Parameters.AddWithValue("@AllotedDate", Date.Text.ToString());

                    }
                    cmd.Parameters.AddWithValue("@PartsNo", Remarks.Text.Trim());
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            if (ErrorMsg==0)
            lblStatus.Text = "Updated Successfully..!";
            lblStatus.Attributes.Add("style", "text-transform:none !important");
            if (lblStatus.Text == "Updated Successfully..!")
            {
                lblStatus.CssClass = "ScsMsg";
                lblStatus.Attributes.Add("style", "text-transform:none !important");
                btn_SaveParts.Visible = false;
                //btn_Close.Visible = true;
                GridView3.DataSource = null;
                GridView3.DataBind();
            }
            else
            {
                lblStatus.CssClass = "ErrMsg";
                lblStatus.Attributes.Add("style", "text-transform:none !important");
            }
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblStatus.ClientID + "').style.display='none'\",5000)</script>");
            
            BindGrid();
            //bindgridPartsStatus(Convert.ToInt16(lblRefnoParts.Text.ToString()));
        }
        catch (Exception ex)
        {
            if (con.State == ConnectionState.Open)
                con.Close();
        }
    }

    protected void GridView3_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView3.PageIndex = e.NewPageIndex;
        bindgridPartsStatus(Convert.ToInt16(lblRefnoParts.Text.ToString()));
    }
    protected void chkPart_CheckedChanged(object sender, EventArgs e)
    {
        lblStatus.CssClass = "reset";
        lblStatus.Text = "";
        //int Index = GridView3.SelectedIndex;

        //GridViewRow Row = ((GridViewRow)((Control)sender).Parent.Parent);
        //string requestid = GridView3.DataKeys[Row.RowIndex].Value.ToString();
        //string cellvalue = Row.Cells[1].Text;

        CheckBox chk = (CheckBox)sender;
        GridViewRow row = (GridViewRow)chk.NamingContainer;
        int rowIndex = row.RowIndex;

        CheckBox chk1 = (CheckBox)GridView3.Rows[rowIndex].Cells[2].FindControl("chkPart");
        TextBox txtdate = (TextBox)GridView3.Rows[rowIndex].Cells[3].FindControl("txtPartsDate");
        TextBox txtTime = (TextBox)GridView3.Rows[rowIndex].Cells[3].FindControl("txtPartsTime");
        TextBox txtPartsNO = (TextBox)GridView3.Rows[rowIndex].Cells[3].FindControl("txtPartsNO");

        //if (chk1.Checked)
        //{
        //    txtdate.Enabled = false;
        //    //txtTime.Enabled = false;
        //    txtPartsNO.Enabled = false;

        //}
        //else
        //{
        //    txtdate.Enabled = true;
        //    //txtTime.Enabled = true;
        //    txtPartsNO.Enabled = true;
        //}
    }
  
  
}