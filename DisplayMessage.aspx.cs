using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI;

public partial class DisplayMessage : System.Web.UI.Page
{
    private SqlCommand cmd;
    private SqlConnection con = new SqlConnection(DataManager.ConStr);
    private DataTable dt;
    private SqlDataAdapter sda;

    protected void btn_Clear_Click(object sender, EventArgs e)
    {
        lbl_msg.Text = "";
        txt_Msg.Text = "";
        txt_Msg.Focus();
    }

    protected void btn_Update_Click(object sender, EventArgs e)
    {
        try
        {
            lbl_msg.Text = "";
            SqlConnection con = new SqlConnection(DataManager.ConStr);
            if (con.State != ConnectionState.Open)
                con.Open();
            cmd = new SqlCommand("UdpInsertDisplayMsg", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DisplayStatus", txt_Msg.Text.Trim());
            cmd.ExecuteNonQuery();
            lbl_msg.ForeColor = Color.Green;
            lbl_msg.Text = "Message Successfully saved. !";
        }
        catch (Exception ex)
        {
            lbl_msg.ForeColor = Color.Red;
            lbl_msg.Text = ex.Message.ToString();
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }

    protected void btnRefreshDisplay_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lbl_msg.Text = "";
            dt = new DataTable();
            sda = new SqlDataAdapter("Select * from tbl_DisplayStatus", new SqlConnection(DataManager.ConStr));
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txt_Msg.Text = dt.Rows[0][0].ToString();
            }
            else
            {
                txt_Msg.Text = "";
            }
        }
        catch { }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    Session["CURRENT_PAGE"] = "Customer Display Message";
                }
                catch (Exception ex)
                {
                }
                lbl_msg.Text = "";
                btnRefreshDisplay_Click(null, null);
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("login.aspx");
        }
    }

    protected void btn_next_Click(object sender, EventArgs e)
    {
        Response.Redirect("WorkTime.aspx");
    }
}