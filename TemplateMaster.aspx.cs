using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TemplateMaster : System.Web.UI.Page
{
    protected DataTable dtTemplate;
    private SqlConnection con = new SqlConnection();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session[Session["TMLDealercode"] + "-TMLConString"].ToString() == null)
            {
                Response.Redirect("login.aspx");
            }

        }
        catch (Exception ex)
        {
            Response.Redirect("login.aspx");
        }
        lblMsg.Visible = false;
        con.ConnectionString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();

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
        if (!Page.IsPostBack)
        {
            btn_BayUpdate.Enabled = false;
            Session["CURRENT_PAGE"] = "Remarks Template";

            BindTemplateGrid();
        }
        this.Title = "Remarks Template";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        btn_BayUpdate.Enabled = false;
        try
        {
            lblMsg.Text = "";
            if (ddlTemplateType.SelectedValue != "0" && txtTemplate.Text != "")
            {
                lblMsg.Text = "";
                int templateid = Convert.ToInt32(ddlTemplateType.SelectedValue);
                string template = txtTemplate.Text;
                SqlCommand cmd = new SqlCommand("udpInsertRemarksTemplate", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TypeId", templateid);
                cmd.Parameters.AddWithValue("@Remarks", template);
                SqlParameter spm = cmd.Parameters.Add("@Msg", SqlDbType.VarChar, 50);
                spm.Direction = ParameterDirection.Output;
                spm.Value = "";
                SqlParameter spm1 = cmd.Parameters.Add("@Flag", SqlDbType.Int);
                spm1.Direction = ParameterDirection.Output;
                spm1.Value = "";
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                if (spm1.Value.ToString() == "0")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
                    lblMsg.CssClass = "ErrMsg";
                    lblMsg.Attributes.Add("style", "text-transform:none !important");
                    lblMsg.Text = spm.Value.ToString();
                }
                else
                {
                    lblMsg.Visible = true;
                    BindTemplateGrid();
                    Clear();
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
                    lblMsg.CssClass = "ScsMsg";
                    lblMsg.Attributes.Add("style", "text-transform:none !important");
                    lblMsg.Text = spm.Value.ToString();
                }
            }
            else
            {
                lblMsg.Visible = true;
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
                lblMsg.CssClass = "ErrMsg";
                lblMsg.Text = "Please select a Template type first and enter Remarks";
                lblMsg.Attributes.Add("style", "text-transform:none !important");
                Clear();
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
            lblMsg.CssClass = "ErrMsg";
            lblMsg.Text = "Unable to save data! Try Again";
            lblMsg.Attributes.Add("style", "text-transform:none !important");
        }
        finally
        {
            if (con.State!=ConnectionState.Closed)
            {
                con.Close();
            }
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Clear();
    }

    protected void Clear()
    {
        //lblMsg.Text = "";
        ddlTemplateType.SelectedIndex = -1;
        txtTemplate.Text = "";
        btn_BayUpdate.Enabled = false;
        btnSave.Enabled = true;
    }

    protected void BindTemplateGrid()
    {
        try { 
        SqlCommand cmd = new SqlCommand("udpListRemarksTemplate", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        dtTemplate = new DataTable();
            con.Open();
        sda.Fill(dtTemplate);
        if (dtTemplate.Rows.Count > 0)
        {
            grdTemplate.DataSource = dtTemplate;
            grdTemplate.DataBind();
        }
        }
        catch (Exception ex) { }
        finally { con.Close(); }
    }

    protected void grdTemplate_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[3].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            switch (Convert.ToInt32(dtTemplate.Rows[e.Row.DataItemIndex][1]))
            {
                case 1: e.Row.Cells[1].Text = "JCC Remarks";
                    break;

                case 2: e.Row.Cells[1].Text = "PDT remarks";
                    break;

                case 3: e.Row.Cells[1].Text = "Vehicle Cancelation Remarks";
                    break;

                case 4: e.Row.Cells[1].Text = "Tag Cancellation";
                    break;

                case 5: e.Row.Cells[1].Text = "Vehicle OUT Remarks";
                    break;

                case 6: e.Row.Cells[1].Text = "Service Remarks";
                    break;

                case 7: e.Row.Cells[1].Text = "Process Remarks";
                    break;

                case 8: e.Row.Cells[1].Text = "Carry Forward";
                    break;
            }
        }
    }

    protected void grdTemplate_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            btn_BayUpdate.Enabled = true;
            btnSave.Enabled = false;
            ddlTemplateType.SelectedIndex = ddlTemplateType.Items.IndexOf((ListItem)(ddlTemplateType.Items.FindByText(grdTemplate.SelectedRow.Cells[1].Text)));
            txtTemplate.Text = grdTemplate.SelectedRow.Cells[2].Text.ToString();
            lblID.Text = grdTemplate.SelectedRow.Cells[3].Text.ToString();
        }
        catch (Exception ex)
        {
        }
    }

    protected void grdTemplate_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            GridViewRow dr = (GridViewRow)grdTemplate.Rows[e.RowIndex];
            int slno = Convert.ToInt32(dr.Cells[3].Text);
            SqlCommand cmddel = new SqlCommand("udpDeleteRemarksTemplate", con);
            cmddel.CommandType = CommandType.StoredProcedure;
            cmddel.Parameters.AddWithValue("@RefNo", slno);
            SqlParameter spm1 = cmddel.Parameters.Add("@flag", SqlDbType.Int);
            spm1.Direction = ParameterDirection.Output;
            spm1.Value = "";
            con.Open();
            cmddel.ExecuteNonQuery();
            if (spm1.Value.ToString() == "0")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
                lblMsg.CssClass = "ErrMsg";
                lblMsg.Text = "Delete unsuccessful.";
                lblMsg.Attributes.Add("style", "text-transform:none !important");
            }
            else
            {
                lblMsg.Visible = true;
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
                lblMsg.CssClass = "ScsMsg";
                lblMsg.Text = "Deleted successfully";
                lblMsg.Attributes.Add("style", "text-transform:none !important");
                Clear();
                con.Close();
                BindTemplateGrid();
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
            lblMsg.CssClass = "ErrMsg";
            lblMsg.Attributes.Add("style", "text-transform:none !important");
            lblMsg.Text = ex.Message.ToString();
            BindTemplateGrid();
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
            {
                con.Close();
            }
        }

    }

    protected void grdTemplate_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdTemplate.PageIndex = e.NewPageIndex;
        BindTemplateGrid();
    }

    protected void btn_BayUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (ddlTemplateType.SelectedValue != "0" && txtTemplate.Text != "" && lblID.Text!="")
            {
                lblMsg.Text = "";
                lblMsg.CssClass = "reset";
                int templateid = Convert.ToInt32(ddlTemplateType.SelectedValue);
                string template = txtTemplate.Text;
                SqlCommand cmd = new SqlCommand("udpUpdateRemarksTemplate", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Slno",lblID.Text.Trim());
                cmd.Parameters.AddWithValue("@TypeId", templateid);
                cmd.Parameters.AddWithValue("@Remarks", template);
                SqlParameter spm = cmd.Parameters.Add("@Msg", SqlDbType.VarChar, 50);
                spm.Direction = ParameterDirection.Output;
                spm.Value = "";
                SqlParameter spm1 = cmd.Parameters.Add("@Flag", SqlDbType.Int);
                spm1.Direction = ParameterDirection.Output;
                spm1.Value = "";
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                if (spm1.Value.ToString() == "0")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
                    lblMsg.CssClass = "ErrMsg";
                    lblMsg.Attributes.Add("style", "text-transform:none !important");
                    lblMsg.Text = spm.Value.ToString();
                }
                else
                {
                    lblMsg.Visible = true;
                    BindTemplateGrid();
                    Clear();
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
                    lblMsg.CssClass = "ScsMsg";
                    lblMsg.Attributes.Add("style", "text-transform:none !important");
                    lblMsg.Text = spm.Value.ToString();
                }
            }
            else
            {
                lblMsg.Visible = true;
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
                lblMsg.CssClass = "ErrMsg";
                lblMsg.Attributes.Add("style", "text-transform:none !important");
                lblMsg.Text = "Please select a Template type first and enter Remarks";
                Clear();
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
            lblMsg.CssClass = "ErrMsg";
            lblMsg.Attributes.Add("style", "text-transform:none !important");
            lblMsg.Text = "Unable to save data! Try Again";
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
            {
                con.Close();
            }
        }

    }
}