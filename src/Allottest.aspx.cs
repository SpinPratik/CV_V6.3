using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class Allottest : System.Web.UI.Page
{
    private SqlConnection con = new SqlConnection(DataManager.ConStr);

    protected void Page_Load(object sender, EventArgs e)
    {
        RefreshGrid();
    }

    protected void timeLine_RowDataBound(object sender, GridViewRowEventArgs e)
    {
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
                gvr.Cells.Add(cell);
                i = i + colsp - 1;
            }
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                if (i > 4 && i < e.Row.Cells.Count - 5)
                {
                    string str = e.Row.Cells[i].Text.Substring(0, 6) + " " + e.Row.Cells[i].Text.Substring(6);
                    //DateTime datt = DateTime.Parse(dt.Date.ToShortDateString() + " " + str);
                    DateTime datt = System.DateTime.Now;
                    if (DateTime.Compare(DateTime.Parse(datt.ToString("HH:mm")), DateTime.Parse(DateTime.Now.ToString("HH:mm"))) <= 0)
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
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[e.Row.Cells.Count - 1].Visible = false;
            e.Row.Cells[e.Row.Cells.Count - 5].Visible = false;

            e.Row.Cells[1].ToolTip = e.Row.Cells[1].Text.Split('#')[1].Trim();
            e.Row.Cells[1].Text = e.Row.Cells[1].Text.Split('#')[0].Trim();
            e.Row.Cells[1].Width = new Unit(85);
            e.Row.Cells[1].Font.Size = new FontUnit(8);
            e.Row.Cells[1].Wrap = true;

            for (int i = 5; i < e.Row.Cells.Count - 4; i++)
            {
                if (e.Row.Cells[i].Text.Trim() != "" && e.Row.Cells[i].Text.Trim() != "&nbsp;")
                {
                    e.Row.Cells[i].Text = "<div style=width:20px;align=center;><img src='images/JCR/circle_green Small.png' Alt=''  width='10' height='10'/></div>";
                }
                else
                {
                    e.Row.Cells[i].Text = "<div style=width:20px;><img src='images/JCR/circle_white_Small.png' Alt=''  width='10' height='10'/></div>";
                }
            }
        }
    }

    private void RefreshGrid()
    {
        SqlConnection con = new SqlConnection(DataManager.ConStr);
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable timeGrid = new DataTable();
        try
        {
            timeLine.DataSource = null;
            timeGrid.Clear();
            cmd = new SqlCommand("GetJobAllotmentTime", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AllotDate", "2014-12-11");
            cmd.Parameters.AddWithValue("@EmpType", "");
            cmd.Parameters.AddWithValue("@ShiftId", "1");
            //cmd.Parameters.AddWithValue("@TechnicianId", "0");
            cmd.Parameters.AddWithValue("@FloorName", "");
            cmd.Parameters.AddWithValue("@TeamLeadId", "");
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
}