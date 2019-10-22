using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Baydisplay1 : System.Web.UI.Page
{
    private int pageIndex = 0;
    private DataTable BayColors;
    private string BackColor = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        pageIndex = timeLine.PageIndex;
        if (!Page.IsPostBack)
        {
            BackColor = GetBayBackColor();
            FillBayDisplay();
        }
    }

    private void FillBayDisplay()
    {
        BackColor = GetBayBackColor();
        try
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(DataManager.ConStr);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "BayProgressDisplay_4Hrs";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@AllotDate", System.DateTime.Now);
            cmd.Parameters.AddWithValue("@ShiftId", "1");
            cmd.Parameters.AddWithValue("@FloorName", "");
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                dt.Columns.RemoveAt(dt.Columns.Count - 1);
                timeLine.DataSource = dt;
                timeLine.DataBind();
                lblPage.Text = "Page : 1 / " + (timeLine.PageCount).ToString();
            }
            else
            {
                timeLine.DataSource = dt;
                timeLine.DataBind();
                lblPage.Text = "Page : 0 / " + (timeLine.PageCount).ToString();
            }
        }
        catch (Exception ex)
        {
        }
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

    protected string GetBayBackColor()
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
            SqlCommand cmd = new SqlCommand("udpGetBayBackColor", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(BayColors);
            return BayColors.Rows[0][0].ToString().Trim();
        }
        catch (Exception ex)
        {
            return "#FFFFFF";
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
    }

    protected string FillBayColors(int flag)
    {
        try
        {
            return ((flag % 2 == 0) ? "SkyBlue" : "DarkGray");
        }
        catch (Exception ex)
        {
            return "White";
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

    protected void timeLine_RowDataBound(object sender, GridViewRowEventArgs e)
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
            cell.HorizontalAlign = HorizontalAlign.Left;
            cell.Height = 50;
            cell.Text = "<table border=0 cellpadding=0 cellspacing=0 style=color:white;><tr><td width=100% align=left>↓BAY</td><td><table border=0 cellpadding=0 cellspacing=0 ><tr><td align=right valign=middle>Time→</td></tr></table></td></tr></table>";
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
                cell.HorizontalAlign = HorizontalAlign.Left;
                cell.Text = e.Row.Cells[i].Text.Split(':').GetValue(0).ToString();
                string str = DateTime.Now.ToString("hh");
                cell.Text = "<table border=0 height=100% width=100%><tr><td colspan=4>" + cell.Text + " HR" + "</td></tr><tr style='font-size:12px;text-align:right;'><td>15</td><td>30</td><td>45</td><td>60</td></tr></table>";
                gvr.Cells.Add(cell);  
                i = i + colsp - 1;
            }            

            timeLine.Controls[0].Controls.AddAt(0, gvr);
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
                e.Row.Cells[i].Width = Unit.Pixel(150);
            }
            e.Row.Cells[1].Attributes.Add("style", "width:15%;");
            e.Row.Cells[e.Row.Cells.Count - 2].Attributes.Add("style", "width:50px;");
            e.Row.Cells[e.Row.Cells.Count - 1].Attributes.Add("style", "width:50px;");
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
            e.Row.Cells[1].Attributes.Add("style", "width:15%; font-weight: bold; font-size: smaller; text-align: center;font-size: 25px;color:white; background-color:DimGray");
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[e.Row.Cells.Count - 3].Visible = false;
            for (int flag = 4; flag < e.Row.Cells.Count - 3; flag++)
            {
                int temp = flag - 3;
                if (temp % 12 == 0)
                    e.Row.Cells[flag].Attributes.Add("style", "border-right-color:DARKGREEN;");
                else
                    e.Row.Cells[flag].Attributes.Add("style", "border-right-color:LIGHTGRAY;");
            }
            FillBayColors();
            string BayCol = "";
            try
            {
                for (int bc = 0; bc < BayColors.Rows.Count; bc++)
                {
                    if (BayColors.Rows[bc][0].ToString().Trim() == e.Row.Cells[1].Text.ToString().Trim())
                    {
                        if (e.Row.RowIndex % 2 == 0)

                            BayCol = BayColors.Rows[bc][1].ToString().Trim();
                        else
                            BayCol = BackColor;
                    }
                }
            }
            catch (Exception ex) { }
            for (int i = e.Row.Cells.Count - 3; i >= 4; i--)
            {
                if (e.Row.Cells[i].Text.Trim() != "" && e.Row.Cells[i].Text.Trim() != "&nbsp;")
                {
                    e.Row.Cells[i].Attributes.Add("style", "background-color:" + BayCol + ";color:#000000;font-weight:bold;");
                    allots += 1;
                    if (e.Row.Cells[i].Text.Split('|')[0].Trim() == e.Row.Cells[i - 1].Text.Split('|')[0].Trim())
                    {
                        int cspan = e.Row.Cells[i].ColumnSpan < 2 ? 2 : e.Row.Cells[i].ColumnSpan + 1;
                        e.Row.Cells[i - 1].ColumnSpan = cspan;
                        e.Row.Cells[i].Visible = false;
                        allots -= 1;
                    }
                }
                e.Row.Cells[i].Font.Bold = true;
                e.Row.Cells[i].Font.Size = 20;
            }

            try
            {
                e.Row.Height = Unit.Pixel(42);
                for (int i = 4; i < e.Row.Cells.Count - 3; i++)
                {
                    if (e.Row.Cells[i].Text.Trim() != "" && e.Row.Cells[i].Text.Trim() != "&nbsp;")
                    {
                        string str2 = "";
                        string RegNo = e.Row.Cells[i].Text.ToString();
                        char[] str = RegNo.ToCharArray();
                        if (RegNo.Length <= 5)
                        {
                            for (int flag = 0; flag < str.Length - 1; flag++)
                            {
                                str2 = str2 + str[flag];
                            }
                        }
                        if (RegNo.Length > 5)
                        {
                            e.Row.Cells[i].ToolTip = " Reg No :" + e.Row.Cells[i].Text;
                        }
                        else
                        {
                            e.Row.Cells[i].ToolTip = " Reg No :" + str2;
                        }
                        if (RegNo.Length > 5)
                        {
                            e.Row.Cells[i].Text = "<div style=width:1px;><table cellpadding=0 cellspacing=0 style=width:100%;table-layout:fixed;white-space:nowrap;><tr><td align=right valign=middle style=color:black;>" + e.Row.Cells[i].Text + "</td></tr></table></div>";//<tr><td>" + EmpName + "</td></tr>
                        }
                        else
                        {
                            if (str[4].ToString() == "1")
                            {
                                e.Row.Cells[i].Text = "<div style=width:1px;><table cellpadding=0 cellspacing=0 style=width:100%;table-layout:fixed;white-space:nowrap;><tr><td align=right valign=middle style=color:White;>" + str2 + "</td></tr></table></div>";//<tr><td>" + EmpName + "</td></tr>
                            }
                            else if (str[4].ToString() == "2")
                            {
                                e.Row.Cells[i].Text = "<div style=width:1px;><table cellpadding=0 cellspacing=0 style=width:100%;table-layout:fixed;white-space:nowrap;><tr><td align=right valign=middle style=color:Green;>" + str2 + "</td></tr></table></div>";//<tr><td>" + EmpName + "</td></tr>
                            }
                            else if (str[4].ToString() == "3")
                            {
                                e.Row.Cells[i].Text = "<div style=width:1px;><table cellpadding=0 cellspacing=0 style=width:100%;table-layout:fixed;white-space:nowrap;><tr><td align=right valign=middle style=color:#A40000;>" + str2 + "</td></tr></table></div>";//<tr><td>" + EmpName + "</td></tr>
                            }
                            else if (str[4].ToString() == "4")
                            {
                                e.Row.Cells[i].Text = "<div style=width:1px;><table cellpadding=0 cellspacing=0 style=width:100%;table-layout:fixed;white-space:nowrap;><tr><td align=right valign=middle style=color:black;>" + str2 + "</td></tr></table></div>";//<tr><td>" + EmpName + "</td></tr>
                            }
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

        if (e.Row.RowIndex > 0)
        {
            if (e.Row.Cells[1].Text.Trim() == timeLine.Rows[e.Row.RowIndex - 1].Cells[1].Text.Trim())
            {
                e.Row.Cells[1].Visible = false;
                timeLine.Rows[e.Row.RowIndex - 1].Cells[1].RowSpan = 2;
                timeLine.Rows[e.Row.RowIndex - 1].Cells[1].Text = "<table border=0 cellpadding=0 cellspacing=0><tr><td width=100% align=left style=color:white;font-size:30px>" + e.Row.Cells[1].Text + "</td><td><table border=0 cellpadding=0 cellspacing=0 ><tr><td align=right valign=middle style=color:CornflowerBlue;>P</td></tr><tr><td align=right valign=middle style=color:White;F>A</td></tr></table></td></tr></table>";                
            }
        }
        e.Row.Cells[e.Row.Cells.Count - 1].Visible = false;
        e.Row.Cells[e.Row.Cells.Count - 2].Visible = false;
    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {
        try
        {
            if (timeLine.PageIndex == timeLine.PageCount - 1)
            {
                timeLine.PageIndex = 0;
                FillBayDisplay();
            }
            else
            {
                timeLine.PageIndex += 1;
                FillBayDisplay();
            }
            lblPage.Text = "Page : " + (timeLine.PageIndex + 1).ToString() + " / " + (timeLine.PageCount).ToString();
        }
        catch { }
    }
}