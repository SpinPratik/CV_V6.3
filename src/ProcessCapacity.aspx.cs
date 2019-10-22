using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ProcessCapacity : System.Web.UI.Page
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
                    Session["CURRENT_PAGE"] = "Standard Capacity";
                }
                catch (Exception ex)
                {
                }
                lblCapacityMsg.Text = "";
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("login.aspx");
        }
    }

    protected void btnSavePCA_Click(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(DataManager.ConStr);
        SqlCommand cmd = new SqlCommand();
        for (int i = 0; i < GridView4.Rows.Count; i++)
        {
            TextBox tb = (TextBox)(GridView4.Rows[i].FindControl("TextBox1"));
            if (!isNumeric(tb.Text, System.Globalization.NumberStyles.Currency))
            {
                lblCapacityMsg.ForeColor = Color.Red;
                lblCapacityMsg.Text = "Please Enter Numeric Value For Capacity";
                return;
            }
        }
        lblCapacityMsg.Text = "";
        try
        {
            for (int i = 0; i < GridView4.Rows.Count; i++)
            {
                TextBox tb = (TextBox)(GridView4.Rows[i].FindControl("TextBox1"));
                cmd = new SqlCommand("UpdateTblEmployeeCapacity", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@EmployeeType", GridView4.Rows[i].Cells[0].Text.Trim());
                cmd.Parameters.Add("@Capacity", tb.Text.Trim());
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            lblCapacityMsg.ForeColor = Color.Green;
            lblCapacityMsg.Text = "Capacity Saved";
        }
        catch (Exception ex)
        {
            lblCapacityMsg.ForeColor = Color.Red;
            lblCapacityMsg.Text = ex.Message.ToString();
        }
        finally
        {
            if (conn.State != ConnectionState.Closed)
                conn.Close();
        }
    }

    public bool isNumeric(string val, System.Globalization.NumberStyles NumberStyle)
    {
        Double result;
        return Double.TryParse(val, NumberStyle,
            System.Globalization.CultureInfo.CurrentCulture, out result);
    }

    protected void btnRefreshCapacity_Click(object sender, ImageClickEventArgs e)
    {
        GridView4.DataBind();
        lblCapacityMsg.ForeColor = Color.Red;
        lblCapacityMsg.Text = "";
    }
}