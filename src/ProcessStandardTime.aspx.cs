using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ProcessStandardTime1 : System.Web.UI.Page
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
                    Session["CURRENT_PAGE"] = "Process Standard Time";
                }
                catch (Exception ex)
                {
                }

                lblStandardTimeMsg.Text = "";
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("login.aspx");
        }
    }

    protected void GridView5_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            e.Row.Cells[0].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Text = "Process";
            e.Row.Cells[2].Text = "Standard Time";
        }
    }

    public bool isNumeric(string val, System.Globalization.NumberStyles NumberStyle)
    {
        Double result;
        return Double.TryParse(val, NumberStyle, System.Globalization.CultureInfo.CurrentCulture, out result);
    }

    protected void btnSaveStandardTime_Click(object sender, EventArgs e)
    {
        //SqlConnection conn = new SqlConnection(DataManager.ConStr);
        //SqlCommand cmd = new SqlCommand();
        for (int i = 0; i < GridView5.Rows.Count; i++)
        {
            TextBox tb = (TextBox)(GridView5.Rows[i].FindControl("TextBox2"));
            if (!isNumeric(tb.Text, System.Globalization.NumberStyles.Integer))
            {
                lblStandardTimeMsg.Text = "Please Enter Numeric Value For Capacity";
                return;
            }
        }
        // lblStandardTimeMsg.Text = "";
        SqlConnection con = new SqlConnection(Session["ConnectionString"].ToString());
        try
        {
            for (int i = 0; i < GridView5.Rows.Count; i++)
            {
                TextBox tb = (TextBox)(GridView5.Rows[i].FindControl("TextBox2"));
                SqlCommand cmd = new SqlCommand("UpdateTblProcessListProcessTime", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProcessTime", tb.Text.Trim());
                cmd.Parameters.AddWithValue("@ProcessId", GridView5.Rows[i].Cells[0].Text.Trim());
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            lblStandardTimeMsg.ForeColor = Color.Green;
            lblStandardTimeMsg.Text = "Standard Time Saved";
        }
        catch (Exception ex)
        {
            lblStandardTimeMsg.ForeColor = Color.Red;
            lblStandardTimeMsg.Text = ex.Message.ToString();
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }

    protected void btnRefreshStdTime_Click(object sender, ImageClickEventArgs e)
    {
        GridView5.DataBind();
        lblStandardTimeMsg.Text = "";
    }
}