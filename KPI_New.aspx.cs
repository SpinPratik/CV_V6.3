using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class KPI_New : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session[Session["TMLDealercode"] + "-TMLConString"].ToString() == "" || Session[Session["TMLDealercode"] + "-TMLConString"].ToString() == null)
            {
                Response.Redirect("login.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("login.aspx");
        }
        FillGrid();
        if (!IsPostBack) {
           DataTable LastRun= getLastExcJobTime();
            lblLastUpdated.Text = "&nbsp;&nbsp;&nbsp;Last updated : " + Convert.ToDateTime(LastRun.Rows[0]["start_execution_date"]).ToString("dd-MM-yyyy HH:00");
       // lblLastUpdated.Text = "&nbsp;&nbsp;&nbsp;Last updated :" + DateTime.Now.ToString("dd-MM-yyyy HH:00");
        }
    }
    public DataTable getLastExcJobTime()
    {
        using (SqlConnection con= new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString()))
        {
            SqlCommand cmd = new SqlCommand("getLastExeTime", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }
    }

    protected void FillGrid()
    {
        try
        {
            string sConnString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
            SqlConnection con = new SqlConnection(sConnString);
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from tblKPIDashboard", con);
            cmd.CommandTimeout = 180;
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            GridView1.DataSource = null;
            GridView1.DataSource = ConvertColumnsAsRows(dt);
            GridView1.DataBind();
            GridView1.Rows[0].Visible = false;
            //GridView1.Rows[11].Visible = false;
            GridView1.Rows[1].Cells[0].Text = "<b>No. of Vehicles In Flow</b>";
            GridView1.Rows[2].Cells[0].Text = "<b>Job Card In  WMS #</b>";
            GridView1.Rows[3].Cells[0].Text = "<b>Job Card In  WMS %</b>";
            GridView1.Rows[4].Cells[0].Text = "<b>Job Card closed In WMS #</b>";
            GridView1.Rows[5].Cells[0].Text = "<b>Job Card closed In WMS %</b>";
            GridView1.Rows[6].Cells[0].Text = "<b>Average Customer Waiting Time(Mins)</b>";
            GridView1.Rows[7].Cells[0].Text = "<b>No.of Vehicles Delivered</b>";
            GridView1.Rows[8].Cells[0].Text = "<b>Same day Delivery #</b>";
            GridView1.Rows[9].Cells[0].Text = "<b>Same day Delivery %</b>";
            GridView1.Rows[10].Cells[0].Text = "<b>Total Carry Forward Vehicles   #</b>";
            GridView1.Rows[11].Cells[0].Text = "<b>Vehicle Alloted By JC #</b>";
            GridView1.Rows[12].Cells[0].Text = "<b>Vehicle Alloted By JC %</b>";
            GridView1.Rows[13].Cells[0].Text = "<b>Job Card	%</b>";
            GridView1.Rows[14].Cells[0].Text = "<b>Vehicle Repair	%</b>";
            GridView1.Rows[15].Cells[0].Text = "<b>Wheel Alignment	%</b>";
            GridView1.Rows[16].Cells[0].Text = "<b>Final Inspection	%</b>";
            GridView1.Rows[17].Cells[0].Text = "<b>Road Test	%</b>";
            GridView1.Rows[18].Cells[0].Text = "<b>Wash	%</b>";
            GridView1.Rows[19].Cells[0].Text = "<b>Normal Bay Performance	%</b>";
            GridView1.Rows[20].Cells[0].Text = "<b>Speedo Bay Performance	%</b>";
            GridView1.Rows[21].Cells[0].Text = "<b>Bay Productivity (Sold)	%</b>";
            GridView1.Rows[22].Cells[0].Text = "<b>Bay Productivity (Capacity)	%</b>";
            GridView1.Rows[23].Cells[0].Text = "<b>Bay Productivity (Cars/Bay)	#</b>";
            GridView1.Rows[24].Cells[0].Text = "<b>Washing Bay	#</b>";
            GridView1.Rows[25].Cells[0].Text = "<b>Wheel Alignment Bay	#</b>";
            GridView1.Rows[26].Cells[0].Text = "<b>Bay Idle Time	HH:MM</b>";
            GridView1.Rows[27].Cells[0].Text = "<b>Technician Utilization %</b>";
            GridView1.Rows[28].Cells[0].Text = "<b>Technician Efficiency %</b>";
            GridView1.Rows[29].Cells[0].Text = "<b>Technician Productivity	%</b>";
            GridView1.Rows[30].Cells[0].Text = "<b>Idle Time	HH:MM</b>";
            GridView1.Rows[31].Cells[0].Text = "<b>Technician Attendance	%</b>";
            GridView1.Rows[32].Cells[0].Text = "<b>TAT GIGO   	HH:MM</b>";
            GridView1.Rows[33].Cells[0].Text = "<b>Ready Vehicles Waiting For Delivery</b>";
            GridView1.Rows[34].Cells[0].Text = "<b>No.of BodyShop Vehicles</b>";
            GridView1.Rows[35].Cells[0].Text = "<b>No.of Cancelled Vehicles</b>";
            con.Close();
        }
        catch (Exception ex)
        {
        }
    }
    public DataTable ConvertColumnsAsRows(DataTable dt)
    {
        DataTable dtnew = new DataTable();
        //Convert all the rows to columns
        for (int i = 0; i <= dt.Rows.Count; i++)
        {
            dtnew.Columns.Add(Convert.ToString(i));
        }
        DataRow dr;
        // Convert All the Columns to Rows
        for (int j = 0; j < dt.Columns.Count; j++)
        {
            dr = dtnew.NewRow(); //Adds new row 
            dr[0] = dt.Columns[j].ToString();
            for (int k = 1; k <= dt.Rows.Count; k++)
                dr[k] = dt.Rows[k - 1][j];
            dtnew.Rows.Add(dr);
        }

        return dtnew;
      
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = "Date<br>"+DateTime.Now.ToString("MMMM - yyyy")+"";
            for (int flag = 1; flag < e.Row.Cells.Count; flag++)
            {
                DateTime dateValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month, int.Parse(e.Row.Cells[flag].Text.Trim()));
                if(flag == e.Row.Cells.Count-1)
                {
                    e.Row.Cells[flag].Text = "Avg";
                }
               else  if (dateValue.ToString("ddd") == "Fri" || dateValue.ToString("ddd") == "Sat")
                {
                    if (e.Row.Cells[flag].Text.Length == 1)
                    {
                        e.Row.Cells[flag].Text = "0" + e.Row.Cells[flag].Text + "<br/>&nbsp;&nbsp;&nbsp;&nbsp;" + dateValue.ToString("ddd");
                    }
                    else
                    {
                        e.Row.Cells[flag].Text = e.Row.Cells[flag].Text + "<br/>&nbsp;&nbsp;&nbsp;&nbsp;" + dateValue.ToString("ddd");
                    }
                }
                else
                {
                    if (e.Row.Cells[flag].Text.Length == 1)
                    {
                        e.Row.Cells[flag].Text = "0" + e.Row.Cells[flag].Text + "<br/>&nbsp;&nbsp;&nbsp;&nbsp;" + dateValue.ToString("ddd");
                    }
                    else
                    {
                        e.Row.Cells[flag].Text = e.Row.Cells[flag].Text + "<br/>&nbsp;&nbsp;&nbsp;&nbsp;" + dateValue.ToString("ddd");
                    }
                }
                if (dateValue.ToString("ddd") == "Sun")
                {
                    e.Row.Cells[flag].ForeColor = System.Drawing.Color.Red;
                }
            }
        }
        
       
    }
}