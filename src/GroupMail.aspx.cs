using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GroupMail : System.Web.UI.Page
{
    private DataTable dt;
    private SqlDataAdapter da;
    private SqlCommand cmd;
    private int GroupMailId;
    private string ReportId;
    private string RefId;
    private SqlConnection con = new SqlConnection(DataManager.ConStr);

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["ROLE"] == null || Session["ROLE"].ToString() != "ADMIN")
            {
                Response.Redirect("login.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("login.aspx");
        }
        getGroupMailDetails();
        if (!Page.IsPostBack)
        {
            Session["CURRENT_PAGE"] = "Group Mail";
            getReportsList();
            btn_Update.Visible = false;
            chkSelectAll.Text = "Select All";
        }
    }

    private void getGroupMailDetails()
    {
        SqlConnection con = new SqlConnection(DataManager.ConStr);
        try
        {
            cmd = new SqlCommand("", con);
            cmd.CommandText = "udpGetGroupMailDetailsforgroupmail";
            cmd.CommandType = CommandType.Text;
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                grdGroupList.DataSource = dt;
                grdGroupList.DataBind();
            }
            else
            {
                grdGroupList.DataSource = null;
            }
        }
        catch (Exception ex)
        {
        }
    }

    private void getReportsList()
    {
        GroupMailId = 0;
        SqlConnection con = new SqlConnection(DataManager.ConStr);
        try
        {
            cmd = new SqlCommand("", con);
            cmd.CommandText = "udpGetReportList";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@GroupMailId", GroupMailId);
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                chkreportsList.Items.Clear();
                ListItem li;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    li = new ListItem();
                    li.Text = dt.Rows[i][1].ToString();
                    li.Value = dt.Rows[i][0].ToString();
                    chkreportsList.Items.Add(li);
                }
            }
        }
        catch (Exception ex)
        { }
    }

    protected string GetSelectedList(CheckBoxList chl)
    {
        string str = "";
        string str2 = "";
        foreach (ListItem li in chkreportsList.Items)
        {
            if (li.Selected)
            {
                str += li.Value + ",";
            }
        }
        char[] ch1 = str.ToCharArray();
        for (int i = 0; i < ch1.Length - 1; i++)
        {
            str2 += ch1[i];
        }
        return str2;
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        try
        {
            if (txt_GroupName.Text.ToString().Trim() == "")
            {
                err_Message.Text = "Enter Group Name..!";
            }
            else if (txt_EmailIds.Text.ToString().Trim() == "")
            {
                err_Message.Text = "Enter emailids ..!";
            }
            else
            {
                ReportId = GetSelectedList(chkreportsList);

                SqlCommand cmd = new SqlCommand("udpInsertGroupMailDetails", con);
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@GroupName", txt_GroupName.Text.ToString().Trim());
                cmd.Parameters.AddWithValue("@RefReportId", ReportId);
                cmd.Parameters.AddWithValue("@EmailIds", txt_EmailIds.Text.ToString());
                cmd.Parameters.AddWithValue("@Active", chkActive.Checked.ToString());
                SqlParameter spm = cmd.Parameters.Add("@Msg", SqlDbType.VarChar, 50);
                spm.Direction = ParameterDirection.Output;
                spm.Value = "";
                SqlParameter spm1 = cmd.Parameters.Add("@flag", SqlDbType.Int);
                spm1.Direction = ParameterDirection.Output;
                spm1.Value = "";
                cmd.ExecuteNonQuery();
                con.Close();
                if (spm1.Value.ToString() == "0")
                {
                    err_Message.ForeColor = Color.Red;
                    err_Message.Text = spm.Value.ToString();
                }
                else
                {
                    err_Message.ForeColor = Color.Green;
                    err_Message.Text = spm.Value.ToString();
                    Clear();
                    getGroupMailDetails();
                    grdGroupList.SelectedIndex = -1;
                }
            }
        }
        catch (Exception ex)
        {
            err_Message.ForeColor = Color.Red;
            err_Message.Text = ex.Message;
        }
    }

    private void Clear()
    {
        txt_EmailIds.Text = "";
        txt_GroupName.Text = "";
        chkActive.Checked = false;
        getReportsList();
        getGroupMailDetails();
        btn_Save.Visible = true;
        btn_Update.Visible = false;
        foreach (ListItem li in chkreportsList.Items)
        {
            li.Selected = false;
        }
        chkSelectAll.Checked = false;
        chkSelectAll.Text = "Select All";
    }

    protected void btn_Clear_Click(object sender, EventArgs e)
    {
        txt_EmailIds.Text = "";
        txt_GroupName.Text = "";
        chkActive.Checked = false;
        getReportsList();
        getGroupMailDetails();
        err_Message.Text = "";
        btn_Save.Visible = true;
        btn_Update.Visible = false;
        foreach (ListItem li in chkreportsList.Items)
        {
            li.Selected = false;
        }
        chkSelectAll.Checked = false;
        chkSelectAll.Text = "Select All";
    }

    protected void grdGroupList_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (ListItem li in chkreportsList.Items)
        {
            li.Selected = false;
        }
        try
        {
            btn_Update.Visible = true;
            btn_Save.Visible = false;
            l1.Text = grdGroupList.SelectedRow.Cells[1].Text.Trim();
            txt_GroupName.Text = grdGroupList.SelectedRow.Cells[2].Text.Trim();
            txt_EmailIds.Text = grdGroupList.SelectedRow.Cells[9].Text.Trim();
            chkActive.Checked = bool.Parse(grdGroupList.SelectedRow.Cells[4].Text.ToString());
            RefId = grdGroupList.SelectedRow.Cells[7].Text.Trim();
            string[] str = RefId.Split(',');
            for (int i = 0; i < str.Length; i++)
            {
                for (int j = 0; j < chkreportsList.Items.Count; j++)
                {
                    if (str[i].Trim() == chkreportsList.Items[j].Value)
                    {
                        chkreportsList.Items[j].Selected = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            err_Message.Text = ex.Message;
        }
    }

    protected void grdGroupList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[4].Visible = false;
        e.Row.Cells[6].Visible = false;
        e.Row.Cells[7].Visible = false;
        e.Row.Cells[9].Visible = false;
        e.Row.Cells[1].Visible = false;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[5].Style.Value = "text-align:center;";
            if (e.Row.Cells[4].Text == "True")
            {
                e.Row.Cells[5].ForeColor = System.Drawing.Color.Green;
                e.Row.Cells[5].Text = "√";
            }
            else if (e.Row.Cells[4].Text == "False")
            {
                e.Row.Cells[5].ForeColor = System.Drawing.Color.Red;
                e.Row.Cells[5].Text = "X";
            }
            else
                e.Row.Cells[5].Text = "";
            if (e.Row.Cells[9].Text.Length > 30)
            {
                e.Row.Cells[3].Text = e.Row.Cells[9].Text.Substring(0, 30) + "...";
            }
            else
                e.Row.Cells[3].Text = e.Row.Cells[9].Text;
        }
    }

    protected void grdGroupList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdGroupList.PageIndex = e.NewPageIndex;
        getGroupMailDetails();
    }

    protected void btn_Update_Click(object sender, EventArgs e)
    {
        try
        {
            if (txt_GroupName.Text.ToString().Trim() == "")
            {
                err_Message.Text = "Enter Group Name..!";
            }
            else if (txt_EmailIds.Text.ToString().Trim() == "")
            {
                err_Message.Text = "Enter emailids ..!";
            }
            else
            {
                ReportId = GetSelectedList(chkreportsList);

                SqlCommand cmd = new SqlCommand("udpUpdateGroupMailDetails", con);
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@GroupName", txt_GroupName.Text.ToString().Trim());
                cmd.Parameters.AddWithValue("@RefReportId", ReportId);
                cmd.Parameters.AddWithValue("@EmailIds", txt_EmailIds.Text.ToString());
                cmd.Parameters.AddWithValue("@Active", chkActive.Checked.ToString());
                cmd.Parameters.AddWithValue("@GroupMailId", l1.Text.Trim());
                SqlParameter spm = cmd.Parameters.Add("@Msg", SqlDbType.VarChar, 50);
                spm.Direction = ParameterDirection.Output;
                spm.Value = "";
                SqlParameter spm1 = cmd.Parameters.Add("@flag", SqlDbType.Int);
                spm1.Direction = ParameterDirection.Output;
                spm1.Value = "";
                cmd.ExecuteNonQuery();
                con.Close();
                if (spm1.Value.ToString() == "0")
                {
                    err_Message.ForeColor = Color.Red;
                    err_Message.Text = spm.Value.ToString();
                }
                else
                {
                    err_Message.ForeColor = Color.Green;
                    err_Message.Text = spm.Value.ToString();
                    Clear();
                    getGroupMailDetails();
                    grdGroupList.SelectedIndex = -1;
                }
            }
        }
        catch (Exception ex)
        {
            err_Message.ForeColor = Color.Red;
            err_Message.Text = ex.Message;
        }
    }

    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        if (chkSelectAll.Checked && chkSelectAll.Text == "Select All")
        {
            chkSelectAll.Checked = false;
            chkSelectAll.Text = "Deselect All";
            for (int flag = 0; flag < chkreportsList.Items.Count; flag++)
            {
                chkreportsList.Items[flag].Selected = true;
            }
        }
        else if (chkSelectAll.Checked && chkSelectAll.Text == "Deselect All")
        {
            chkSelectAll.Checked = false;
            chkSelectAll.Text = "Select All";
            for (int flag = 0; flag < chkreportsList.Items.Count; flag++)
            {
                chkreportsList.Items[flag].Selected = false;
            }
        }
    }

    protected void chkreportsList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!chkSelectAll.Checked && chkSelectAll.Text == "Deselect All")
        {
            for (int j = 0; j < chkreportsList.Items.Count; j++)
            {
                if (chkreportsList.Items[j].Selected == false)
                {
                    chkSelectAll.Checked = false;
                    chkSelectAll.Text = "Select All";
                    chkreportsList.Items[j].Selected = false;

                    break;
                }
            }
        }

        int Count = 0;
        if (!chkSelectAll.Checked && chkSelectAll.Text == "Select All")
        {
            for (int k = 0; k < chkreportsList.Items.Count; k++)
            {
                if (chkreportsList.Items[k].Selected == true)
                {
                    Count = Count + 1;
                }
                else
                {
                    break;
                }
            }

            if (Count == chkreportsList.Items.Count)
            {
                chkSelectAll.Text = "Deselect All";
                chkSelectAll.Checked = false;
            }
        }
    }

    protected void grdGroupList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            GridViewRow dr = (GridViewRow)grdGroupList.Rows[e.RowIndex];
            int slno = Convert.ToInt32(dr.Cells[1].Text);
            string deletestr = "Delete from tblGroupMail where Id=" + slno;
            SqlCommand cmddel = new SqlCommand(deletestr, con);
            con.Open();
            int x = cmddel.ExecuteNonQuery();
            if (x > 0)
            {
                Clear();
                err_Message.ForeColor = Color.Green;
                err_Message.Text = "Deleted Successfully";
                con.Close();
                getGroupMailDetails();
            }
        }
        catch (Exception ex)
        {
            err_Message.ForeColor = Color.Red;
            err_Message.Text = ex.Message.ToString();
            getGroupMailDetails();
        }
    }
}