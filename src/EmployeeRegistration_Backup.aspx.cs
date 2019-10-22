using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EmployeeRegistration : System.Web.UI.Page
{
    private DataTable dt;
    private SqlDataAdapter sda;
    private SqlCommand cmd;
    private SqlConnection con = new SqlConnection(DataManager.ConStr);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                MultiView1.ActiveViewIndex = 0;
                fillcard();
                clear();
                fillemptype();
                fillnewcard();
                clrupdt();
                FillGrid1();
                FillGrid();
                fillemptypeupdt();
                Session["CURRENT_PAGE"] = "Employee Registration ";
            }
            catch (Exception ex)
            {
            }
        }
    }

    protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
    {
        txtEName.Text = "";
        fillcard();
        FillGrid();
    }

    private void fillcard()
    {
        SqlCommand cmd = new SqlCommand("SELECT EnrollmentNo FROM tblRFID where EmpID IS NULL AND Reserved='1' order by EnrollmentNo", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        try
        {
            da.Fill(dt);
            if (dt.Rows.Count != 0)
            {
                cmbCardNo.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cmbCardNo.Items.Add(new ListItem(dt.Rows[i]["EnrollmentNo"].ToString(), dt.Rows[i]["EnrollmentNo"].ToString()));
                }
            }
            else
            {
                cmbCardNo.Items.Clear();
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void btnAssign_Click(object sender, EventArgs e)
    {
        SqlCommand cmd1 = new SqlCommand("GetEmployeeAssigned", con);
        cmd1.CommandType = CommandType.StoredProcedure;
        cmd1.Parameters.AddWithValue("@cardno", cmbCardNo.SelectedValue);
        cmd1.Parameters.AddWithValue("@empname", txtEName.Text);
        cmd1.Parameters.AddWithValue("@emptype", cmbEType.SelectedValue);
        cmd1.Parameters.AddWithValue("@IsBodyshop", chkBodyShop.Checked.ToString());
        SqlParameter msg = cmd1.Parameters.Add("@msg", SqlDbType.VarChar, 100);
        SqlParameter flag = cmd1.Parameters.Add("@flag", SqlDbType.Int);
        msg.Direction = ParameterDirection.Output;
        msg.Value = "";
        flag.Direction = ParameterDirection.Output;
        flag.Value = 0;
        con.Open();
        cmd1.ExecuteNonQuery();
        if (Convert.ToInt16(flag.Value.ToString()) != 0)
        {
            con.Close();
            string str = "Select EmpId from tblEmployee where EmpType='" + cmbEType.SelectedValue + "' AND CardNo='" + cmbCardNo.SelectedItem + "'";

            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            try
            {
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    lblempid.Text = dt.Rows[0]["EmpId"].ToString();
                }
                string str1 = "update tblRFID set EmpID='" + lblempid.Text + "',Assigned=1 where EnrollmentNo='" + cmbCardNo.SelectedValue + "'";

                SqlCommand cmd2 = new SqlCommand(str1, con);
                con.Open();
                cmd2.ExecuteNonQuery();
                con.Close();
                lbMsg.ForeColor = Color.Green;
                lbMsg.Text = "Employee " + txtEName.Text + " has assigned card no " + cmbCardNo.SelectedValue + ", His/Her ID is " + lblempid.Text.Trim() + ".";
                txtEName.Text = "";
                chkBodyShop.Checked = false;
                fillcard();
                fillemptype();
                fillnewcard();
                FillGrid();
                FillGrid1();
                clear();
            }

            catch (Exception ex)
            {
                lbMsg.ForeColor = Color.Red;
                lbMsg.Text = ex.Message;
            }
        }
        else
        {
            lbMsg.ForeColor = Color.Red;
            lbMsg.Text = msg.Value.ToString();
        }
    }

    public void FillGrid()
    {
        SqlConnection con = new SqlConnection(DataManager.ConStr);
        SqlCommand cmd = new SqlCommand("udpGetEmployeeList", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            grdEmployeeList.DataSource = dt;
            grdEmployeeList.DataBind();
        }
        else
        {
            grdEmployeeList.DataSource = null;
        }
    }

    public void FillGrid1()
    {
        SqlConnection con = new SqlConnection(DataManager.ConStr);
        SqlCommand cmd = new SqlCommand("udpGetEmployeeList", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = null;
        }
    }

    private void clear()
    {
        cmbCardNo.SelectedIndex = 0;
        cmbEType.SelectedIndex = 0;
        txtEName.Text = "";
    }

    private void fillemptype()
    {
        SqlCommand cmd = new SqlCommand("SELECT TypeId,EmpType FROM tblEmployeeType", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        try
        {
            da.Fill(dt);
            cmbEType.DataSource = dt;
            cmbEType.DataTextField = "EmpType";
            cmbEType.DataValueField = "TypeId";
            cmbEType.DataBind();
        }
        catch (Exception ex)
        {
        }
    }

    private void fillnewcard()
    {
        SqlCommand cmd = new SqlCommand("SELECT EnrollmentNo FROM tblRFID where ISNULL(EmpID, 0) = 0  AND Reserved='1' order by EnrollmentNo", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        try
        {
            da.Fill(dt);
            ddlNewCardNo.Items.Clear();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ddlNewCardNo.Items.Add(dt.Rows[i][0].ToString());
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void btnGetDetails_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lbMsg1.Text = "";
            txtEmployeeName.Text = "";
            ddlEmployeeType.SelectedIndex = -1;
            lblSrcEmpID.Text = "";
            chkIsBodyShop.Checked = false;

            if (txtExistingTagNo.Text.Trim() == "")
            {
                lbMsg1.ForeColor = Color.Red;
                lbMsg1.Text = "Please Enter Existing Tag No.";
            }
            else
            {
                SqlConnection con = new SqlConnection(DataManager.ConStr);
                SqlDataAdapter sda = new SqlDataAdapter("GetEmployeeDetail", con);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.AddWithValue("@CardNo", txtExistingTagNo.Text.Trim());
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    txtEmployeeName.Text = dt.Rows[0]["EmpName"].ToString();
                    ddlEmployeeType.SelectedValue = dt.Rows[0]["EmpType"].ToString();
                    lblSrcEmpID.Text = dt.Rows[0]["EmpID"].ToString();
                    chkIsBodyShop.Checked = bool.Parse(dt.Rows[0]["IsBodyshop"].ToString());
                }
                else
                {
                    lbMsg1.ForeColor = Color.Red;
                    lbMsg1.Text = "No Records Found!";
                }
            }
        }
        catch (Exception ex)
        { }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            string change = "";
            if (chkChangeCardNo.Checked)
                change = "Yes";
            else
                change = "No";

            if (lblSrcEmpID.Text.Trim() == "")
                lbMsg1.Text = "Please Enter Correct Tag No.";
            else if (txtExistingTagNo.Text.Trim() == "")
                lbMsg1.Text = "Please Enter Existing Tag No.";
            else if (txtEmployeeName.Text.Trim() == "")
                lbMsg1.Text = "Please Enter Employee Name.";
            else
            {
                SqlConnection con = new SqlConnection(DataManager.ConStr);
                SqlCommand cmd = new SqlCommand("UpdateTblEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpID", lblSrcEmpID.Text.Trim());
                cmd.Parameters.AddWithValue("@OldTagNo", txtExistingTagNo.Text.Trim());
                cmd.Parameters.AddWithValue("@NewTagNo", ddlNewCardNo.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@EmpName", txtEmployeeName.Text.Trim());
                cmd.Parameters.AddWithValue("@EmpType", ddlEmployeeType.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@changeTag", change);
                cmd.Parameters.AddWithValue("@IsBodyshop", chkIsBodyShop.Checked.ToString());
                SqlParameter spm = cmd.Parameters.Add("RetVal", SqlDbType.VarChar, 30);
                spm.Direction = ParameterDirection.Output;
                spm.Value = "";
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                lbMsg1.ForeColor = Color.Green;
                lbMsg1.Text = spm.Value.ToString();
                clrupdt();
                fillnewcard();
                fillemptype();
                FillGrid();
                FillGrid1();
                chkChangeCardNo.Checked = false;
                txtEmployeeName.Text = "";
                chkIsBodyShop.Checked = false;
                ddlEmployeeType.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        { }
    }

    private void clrupdt()
    {
        txtExistingTagNo.Text = "";
        lblSrcEmpID.Text = "";
        if (ddlEmployeeType.Items.Count != 0)
            ddlEmployeeType.SelectedIndex = 0;
        if (ddlNewCardNo.Items.Count != 0)
            ddlNewCardNo.SelectedIndex = 0;
        txtEmployeeName.Text = "";
    }

    protected void btnUnAssign_Click(object sender, EventArgs e)
    {
        try
        {
            if (lblSrcEmpID.Text.Trim() == "")
                lbMsg1.Text = "Please Enter Correct Tag No.";
            else if (txtExistingTagNo.Text.Trim() == "")
                lbMsg1.Text = "Please Enter Existing Tag No.";
            else
            {
                SqlConnection con = new SqlConnection(DataManager.ConStr);
                SqlCommand cmd = new SqlCommand("UnassignTblEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpID", lblSrcEmpID.Text.Trim());
                cmd.Parameters.AddWithValue("@OldTagNo", txtExistingTagNo.Text.Trim());
                cmd.Parameters.AddWithValue("@EmpType", ddlEmployeeType.SelectedItem.Text.ToString());
                SqlParameter spm = cmd.Parameters.Add("@RetVal", SqlDbType.VarChar, 30);
                spm.Direction = ParameterDirection.Output;
                spm.Value = "";
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                lbMsg1.ForeColor = Color.Green;
                lbMsg1.Text = spm.Value.ToString();
                clrupdt();
                fillnewcard();
                fillemptype();
                FillGrid();
                FillGrid1();
                chkChangeCardNo.Checked = false;
                chkIsBodyShop.Checked = false;
                txtEmployeeName.Text = "";
                ddlEmployeeType.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        { }
    }

    protected void rdEmployeeRegistration_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdEmployeeRegistration.SelectedValue == "0")
        {
            MultiView1.ActiveViewIndex = 0;
            lbMsg1.Text = "";
        }
        else if (rdEmployeeRegistration.SelectedValue == "1")
        {
            MultiView1.ActiveViewIndex = 1;
            lbMsg.Text = "";
        }
        fillcard();
        fillemptype();
        FillGrid();
        FillGrid1();
        fillemptypeupdt();
        fillnewcard();
    }

    private void fillemptypeupdt()
    {
        SqlCommand cmd = new SqlCommand("SELECT * FROM tblEmployeeType", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        try
        {
            da.Fill(dt);
            ddlEmployeeType.DataSource = dt;
            ddlEmployeeType.DataTextField = "EmpType";
            ddlEmployeeType.DataValueField = "TypeId";
            ddlEmployeeType.DataBind();
        }
        catch (Exception ex)
        {
        }
    }

    protected void chkChangeCardNo_CheckedChanged(object sender, EventArgs e)
    {
        if (chkChangeCardNo.Checked == true)
            RequiredFieldValidator7.Enabled = true;
        else
            RequiredFieldValidator7.Enabled = false;
    }

    protected void grdEmployeeList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdEmployeeList.PageIndex = e.NewPageIndex;
        FillGrid();
    }

    protected void grdEmployeeList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager)
            e.Row.Cells[0].Visible = false;
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        FillGrid1();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager)
            e.Row.Cells[0].Visible = false;
    }
}