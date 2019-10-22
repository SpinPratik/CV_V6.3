using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DealerPrincipalDashboard : System.Web.UI.Page
{
    private string selectType = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillGrid();
            lbMonthYr.Text = DateTime.Now.ToString("MMM-yyyy");
        }
        Session["Curren_Page"] = "Dealer principal Dashboard";
        this.Title = "Dealer principal Dashboard";
    }

    protected void grdDPD_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[0].Visible = false;
        if (e.Row.RowType == DataControlRowType.Header)
        {
            for (int flag = 1; flag < e.Row.Cells.Count; flag++)
            {
                DateTime dateValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month, int.Parse(e.Row.Cells[flag].Text.Trim()));

                if (dateValue.ToString("ddd") == "Fri" || dateValue.ToString("ddd") == "Sat")
                {
                    if (e.Row.Cells[flag].Text.Length == 1)
                    {
                        e.Row.Cells[flag].Text = "&nbsp;&nbsp;&nbsp;&nbsp;0" + e.Row.Cells[flag].Text + "&nbsp;&nbsp;&nbsp;&nbsp;<br/>&nbsp;&nbsp;&nbsp;&nbsp;" + dateValue.ToString("ddd") + "&nbsp;&nbsp;&nbsp;&nbsp;";
                    }
                    else
                    {
                        e.Row.Cells[flag].Text = "&nbsp;&nbsp;&nbsp;&nbsp;" + e.Row.Cells[flag].Text + "&nbsp;&nbsp;&nbsp;&nbsp;<br/>&nbsp;&nbsp;&nbsp;&nbsp;" + dateValue.ToString("ddd") + "&nbsp;&nbsp;&nbsp;&nbsp;";
                    }
                }
                else
                {
                    if (e.Row.Cells[flag].Text.Length == 1)
                    {
                        e.Row.Cells[flag].Text = "&nbsp;&nbsp;&nbsp;0" + e.Row.Cells[flag].Text + "&nbsp;&nbsp;&nbsp;<br/>&nbsp;&nbsp;&nbsp;" + dateValue.ToString("ddd") + "&nbsp;&nbsp;&nbsp;";
                    }
                    else
                    {
                        e.Row.Cells[flag].Text = "&nbsp;&nbsp;&nbsp;" + e.Row.Cells[flag].Text + "&nbsp;&nbsp;&nbsp;<br/>&nbsp;&nbsp;&nbsp;" + dateValue.ToString("ddd") + "&nbsp;&nbsp;&nbsp;";
                    }
                }

                if (dateValue.ToString("ddd") == "Sun")
                {
                    e.Row.Cells[flag].BackColor = System.Drawing.Color.Red;
                    e.Row.Cells[flag].ForeColor = System.Drawing.Color.White;
                }
            }
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int flag = 0; flag < e.Row.Cells.Count; flag++)
            {
                e.Row.Cells[flag].Width = new Unit("100px");
                if (flag == 1)
                    e.Row.Cells[flag].ForeColor = System.Drawing.ColorTranslator.FromHtml("Green");
            }

            if ((e.Row.RowIndex >= 0 && e.Row.RowIndex <= 3) || e.Row.RowIndex == 6)
            {
                for (int flag = 0; flag < e.Row.Cells.Count; flag++)
                {
                    e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Replace(".00", "");
                }
            }
        }

        if (e.Row.RowType == DataControlRowType.Header)
            e.Row.Font.Bold = false;
    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {
        FillGrid();
    }

    protected void FillGrid()
    {
        string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        SqlConnection con = new SqlConnection(sConnString);
        try
        {
            SqlCommand cmd = new SqlCommand("GetDealerDashboard", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Bodyshop", 0);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            grdDPD.DataSource = dt;
            grdDPD.DataBind();
        }
        catch (Exception ex)
        {
            grdDPD.DataSource = null;
            grdDPD.DataBind();
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }

    protected void btnLogout_Click(object sender, ImageClickEventArgs e)
    {
        Session.Clear();
        Session.Abandon();
        Response.Redirect("login.aspx");
    }

    protected void btnBack_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("DPHome.aspx");
    }
}