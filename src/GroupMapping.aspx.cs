using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI;

public partial class GroupMapping : System.Web.UI.Page
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
                    Session["CURRENT_PAGE"] = "Group Mapping";
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

    protected void dd_TeamLead_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGroupMap();
    }

    protected void fillGroupMap()
    {
        list_Group.Items.Clear();
        list_Employee.Items.Clear();
        SqlConnection con = new SqlConnection(DataManager.ConStr);
        DataTable dt = new DataTable();
        try
        {
            SqlCommand cmd = new SqlCommand("SelectEmployee", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmpId", dd_TeamLead.SelectedValue);
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
                    list_Employee.Items.Add(dt.Rows[i][0].ToString());
                }
                else
                {
                    list_Group.Items.Add(dt.Rows[i][0].ToString());
                }
            }
        }
        catch (Exception ex) { }
    }

    protected void btn_RightMove_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lbl_msg1.Text = "";
            if (dd_TeamLead.SelectedIndex > 0)
            {
                for (int i = 0; i < list_Employee.Items.Count; i++)
                {
                    SqlConnection con = new SqlConnection(DataManager.ConStr);
                    SqlCommand cmd = new SqlCommand("MapTeamGroup", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmpId", dd_TeamLead.SelectedValue);
                    cmd.Parameters.AddWithValue("@EmpName", list_Employee.Items[i].Selected ? list_Employee.Items[i].Text : "");
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    cmd.ExecuteNonQuery();
                }
                fillGroupMap();
            }
            else
            {
                lbl_msg1.ForeColor = Color.Red;
                lbl_msg1.Text = "Please Select Team Lead !";
            }
        }
        catch (Exception ex)
        {
            lbl_msg1.ForeColor = Color.Red;
            lbl_msg1.Text = "Please Select Employee Name. !";
        }
    }

    protected void btn_LeftMove_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lbl_msg1.Text = "";
            if (dd_TeamLead.SelectedIndex > 0)
            {
                for (int i = 0; i < list_Group.Items.Count; i++)
                {
                    SqlConnection con = new SqlConnection(DataManager.ConStr);
                    SqlCommand cmd = new SqlCommand("UnMapTeamGroup", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmpName", list_Group.Items[i].Selected ? list_Group.Items[i].Text : "");
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    cmd.ExecuteNonQuery();
                }
                fillGroupMap();
            }
            else
            {
                lbl_msg1.ForeColor = Color.Red;
                lbl_msg1.Text = "Please Select Team Lead. !";
            }
        }
        catch (Exception ex)
        {
            lbl_msg1.ForeColor = Color.Red;
            lbl_msg1.Text = "Please Select Employee Name. !";
        }
    }
}