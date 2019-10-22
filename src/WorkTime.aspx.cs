using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WorkTime : System.Web.UI.Page
{
    private DataTable dt;
    private SqlDataAdapter sda;
    private SqlCommand cmd;
    private SqlConnection con = new SqlConnection(DataManager.ConStr);

    protected void Page_Load(object sender, EventArgs e)
    {
       
        try
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    Session["CURRENT_PAGE"] = "Work Time";
                }
                catch (Exception ex)
                {
                }
                fillbreakout();
                fillbreakin();
                fillouttime();
                FillInTime();
                getShifts();
                FillGrid();
                SetLastSetTime();
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("login.aspx");
        }
    }

    public void FillGrid()
    {
        SqlConnection con = new SqlConnection(DataManager.ConStr);
        SqlCommand cmd = new SqlCommand("udpGetShiftDetails", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
    }

    protected void chkShift_CheckedChanged(object sender, EventArgs e)
    {
        lblmessage.Text = "";
        if (chkShift.Checked)
        {
            ddlShift.Visible = false;
            txtShift.Visible = true;
        }
        else
        {
            ddlShift.Visible = true;
            txtShift.Visible = false;
        }
        getShifts();
    }

    protected void getShifts()
    {
        SqlConnection con = new SqlConnection(DataManager.ConStr);
        SqlCommand cmd = new SqlCommand("udpGetShiftList", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter sda = new SqlDataAdapter(cmd);

        DataTable dt = new DataTable();
        sda.Fill(dt);
        ddlShift.Items.Clear();
        ddlShift.Items.Add("--Select--");
        ddlShift.DataSource = dt;
        ddlShift.DataTextField = "Shift";
        ddlShift.DataValueField = "ShiftID";
        ddlShift.DataBind();
    }

    private void FillInTime()
    {
        try
        {
            int i = 0;
            int j = 0;
            ddlintime.Items.Clear();

            for (i = 7; i <= 22; i++)
            {
                for (j = 0; j < 60; j += 10)
                {
                    ddlintime.Items.Add(i.ToString() + ":" + ((j == 0) ? "00" : j.ToString()));
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    private void fillouttime()
    {
        try
        {
            int i = 0;
            int j = 0;
            ddlouttime.Items.Clear();

            for (i = 7; i <= 22; i++)
            {
                for (j = 0; j < 60; j += 10)
                {
                    ddlouttime.Items.Add(i.ToString() + ":" + ((j == 0) ? "00" : j.ToString()));
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    private void fillbreakin()
    {
        try
        {
            int i = 0;
            int j = 0;
            ddlbrkin.Items.Clear();

            for (i = 7; i <= 22; i++)
            {
                for (j = 0; j < 60; j += 10)
                {
                    ddlbrkin.Items.Add(i.ToString() + ":" + ((j == 0) ? "00" : j.ToString()));
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    private void fillbreakout()
    {
        try
        {
            int i = 0;
            int j = 0;
            ddlbrkout.Items.Clear();

            for (i = 7; i <= 22; i++)
            {
                for (j = 0; j < 60; j += 10)
                {
                    ddlbrkout.Items.Add(i.ToString() + ":" + ((j == 0) ? "00" : j.ToString()));
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        if ((Convert.ToDateTime(ddlintime.SelectedValue) < Convert.ToDateTime(ddlbrkin.SelectedValue)) && (Convert.ToDateTime(ddlbrkin.SelectedValue) < Convert.ToDateTime(ddlbrkout.SelectedValue)) && (Convert.ToDateTime(ddlbrkout.SelectedValue) < Convert.ToDateTime(ddlouttime.SelectedValue)))
        {
            if (chkShift.Checked == false)
            {
                if (ddlShift.SelectedItem.Text.ToLower() == "--select--")
                {
                    lblmessage.Text = "Please Select Shift";
                }
                else
                {
                    string str = "InsertTblShiftWorkTime";
                    SqlCommand cmd = new SqlCommand(str, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@InTime", ddlintime.Text);
                    cmd.Parameters.AddWithValue("@BreakIn", ddlbrkin.Text);
                    cmd.Parameters.AddWithValue("@BreakOut", ddlbrkout.Text);
                    cmd.Parameters.AddWithValue("@OutTime", ddlouttime.Text);
                    cmd.Parameters.AddWithValue("@ShiftID", ddlShift.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Shift", "");
                    cmd.Parameters.AddWithValue("@saveShift", "N");
                    SqlParameter sp = cmd.Parameters.Add("@exist", SqlDbType.VarChar, 5);
                    sp.Direction = ParameterDirection.Output;
                    sp.Value = "N";
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    lblmessage.Text = "Saved Successfully";
                    FillGrid();
                }
            }
            else
            {
                if (txtShift.Text.Trim() != "")
                {
                    string str = "InsertTblShiftWorkTime";
                    SqlCommand cmd = new SqlCommand(str, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@InTime", ddlintime.Text);
                    cmd.Parameters.AddWithValue("@BreakIn", ddlbrkin.Text);
                    cmd.Parameters.AddWithValue("@BreakOut", ddlbrkout.Text);
                    cmd.Parameters.AddWithValue("@OutTime", ddlouttime.Text);
                    cmd.Parameters.AddWithValue("@ShiftID", "0");
                    cmd.Parameters.AddWithValue("@Shift", txtShift.Text.Trim());
                    cmd.Parameters.AddWithValue("@saveShift", "Y");
                    SqlParameter sp = cmd.Parameters.Add("@exist", SqlDbType.VarChar, 5);
                    sp.Direction = ParameterDirection.Output;
                    sp.Value = "N";
                    con.Open();
                    cmd.ExecuteNonQuery();
                    if (sp.Value.ToString() == "YES")
                        lblmessage.Text = "Shift Already Exist! New Shift Not Generated.";
                    else
                        lblmessage.Text = "Saved Successfully";
                    con.Close();
                    FillGrid();
                }
                else
                {
                    lblmessage.Text = "Please Enter Shift!";
                }
            }
        }
        else
        {
            lblmessage.Text = "Time is not in the correct order";
        }
    }

    protected void btnrefrsh_Click(object sender, EventArgs e)
    {
        lblmessage.Text = "";
        txtShift.Text = "";
        chkShift.Checked = false;
        txtShift.Visible = false;
        ddlShift.Visible = true;
        SetLastSetTime();
        getShifts();
        FillGrid();
    }

    protected void SetLastSetTime()
    {
        String query = "udpGetWorkTimeforAdmin";
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        cmd.CommandType = CommandType.StoredProcedure;
        DataTable dt = new DataTable();
        da.Fill(dt);

        if (dt.Rows.Count != 0)
        {
            ddlintime.Text = dt.Rows[0][0].ToString();
            ddlbrkin.Text = dt.Rows[0][1].ToString();
            ddlbrkout.Text = dt.Rows[0][2].ToString();
            ddlouttime.Text = dt.Rows[0][3].ToString();
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        FillGrid();
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlintime.Text = GridView1.SelectedRow.Cells[2].Text.Trim();
            ddlbrkin.Text = GridView1.SelectedRow.Cells[3].Text.Trim();
            ddlbrkout.Text = GridView1.SelectedRow.Cells[4].Text.Trim();
            ddlouttime.Text = GridView1.SelectedRow.Cells[5].Text.Trim();
            ddlShift.SelectedValue = GridView1.SelectedRow.Cells[1].Text.Trim();
        }
        catch
        {
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager)
            e.Row.Cells[1].Visible = false;
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            GridViewRow dr = (GridViewRow)GridView1.Rows[e.RowIndex];
            int slno = Convert.ToInt32(dr.Cells[1].Text);
            SqlCommand cmd = new SqlCommand("UdpDeleteShiftDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ShiftId", slno);
            con.Open();
            int x = cmd.ExecuteNonQuery();
            if (x > 0)
            {
                lblmessage.Text = "Deleted Successfully";
                con.Close();
                FillGrid();

            }
        }
        catch (Exception ex)
        {

            lblmessage.Text = ex.Message.ToString();
            FillGrid();
        }
    }

    protected void btn_next_Click(object sender, EventArgs e)
    {
        Response.Redirect("RFIDCards.aspx");
    }
}