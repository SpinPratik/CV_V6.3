using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI;

public partial class ShiftMapping : System.Web.UI.Page
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
                    Session["CURRENT_PAGE"] = "Shift Mapping";
                }

                catch (Exception ex)
                {
                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("login.aspx");
        }
    }

    protected void dd_Shifts_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillShiftMap();
    }

    protected void fillShiftMap()
    {
        list_ShiftEmployee.Items.Clear();
        list_Employee0.Items.Clear();
        SqlConnection con = new SqlConnection(DataManager.ConStr);
        DataTable dt = new DataTable();
        try
        {
            SqlCommand cmd = new SqlCommand("SelectShiftEmployee", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ShiftId", dd_Shifts.SelectedValue);
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][1].ToString() == "")
                {
                    list_Employee0.Items.Add(dt.Rows[i][0].ToString());
                }
                else
                {
                    list_ShiftEmployee.Items.Add(dt.Rows[i][0].ToString());
                }
            }
        }
        catch (Exception ex) { }
    }

    protected void btn_RightMove0_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lbl_msg2.Text = "";
            if (dd_Shifts.SelectedIndex > 0)
            {
                for (int i = 0; i < list_Employee0.Items.Count; i++)
                {
                    SqlConnection con = new SqlConnection(DataManager.ConStr);
                    SqlCommand cmd = new SqlCommand("MapEmployeeShift", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ShiftId", dd_Shifts.SelectedValue);
                    cmd.Parameters.AddWithValue("@EmpName", list_Employee0.Items[i].Selected ? list_Employee0.Items[i].Text : "");
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    cmd.ExecuteNonQuery();
                }
                fillShiftMap();
            }
            else
            {
                lbl_msg2.ForeColor = Color.Red;
                lbl_msg2.Text = "Please Select Shift !";
            }
        }
        catch (Exception ex)
        {
            lbl_msg2.ForeColor = Color.Red;
            lbl_msg2.Text = "Please Select Employee Name. !";
        }
    }

    protected void btn_LeftMove0_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lbl_msg2.Text = "";
            if (dd_Shifts.SelectedIndex > 0)
            {
                for (int i = 0; i < list_ShiftEmployee.Items.Count; i++)
                {
                    SqlConnection con = new SqlConnection(DataManager.ConStr);
                    SqlCommand cmd = new SqlCommand("UnMapEmployeeShift", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@EmpName", list_ShiftEmployee.Items[i].Selected ? list_ShiftEmployee.Items[i].Text : "");
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    cmd.ExecuteNonQuery();
                }
                fillShiftMap();
            }
            else
            {
                lbl_msg2.ForeColor = Color.Red;
                lbl_msg2.Text = "Please Select Shift !";
            }
        }
        catch (Exception ex)
        {
            lbl_msg2.ForeColor = Color.Red;
            lbl_msg2.Text = "Please Select Employee Name. !";
        }
    }
}