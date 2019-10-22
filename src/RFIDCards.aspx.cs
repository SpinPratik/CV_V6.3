using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RFIDCards : System.Web.UI.Page
{
    private DataTable dt;
    private SqlDataAdapter sda;
    private SqlCommand cmd;
   

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session[Session["TMLDealercode"] + "-TMLConString"].ToString()==null)
            {
                Response.Redirect("login.aspx");
            }

        }
        catch (Exception ex)
        {
            Response.Redirect("login.aspx");
        }
        try
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    Session["CURRENT_PAGE"] = "RFID Card Generation";
                }
                catch (Exception ex)
                {
                }

                lblRFIDMsg.Text = "";
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("login.aspx");
        }
        FillGrid();
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        try
        {
            lblRFIDMsg.CssClass = "ErrMsg";

            lblRFIDMsg.Text = "";
            lblRFIDMsg.ForeColor = Color.Red;
            if (txtSlNoFrm.Text.Trim() == "")
            { 
                lblRFIDMsg.Text = "Please enter From Serial#.";
                lblRFIDMsg.Attributes.Add("style", "text-transform:none");
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblRFIDMsg.ClientID + "').style.display='none'\",5000)</script>");
            }
            else if (txtSlNoTo.Text.Trim() == "")
            { 
                lblRFIDMsg.Text = "Please enter To Serial#.";
                lblRFIDMsg.Attributes.Add("style", "text-transform:none");
            }
            else if (ddlCardType.SelectedValue.ToString() == "99")
            { 
                lblRFIDMsg.Text = "Please select card type.";
                lblRFIDMsg.Attributes.Add("style", "text-transform:none");
            }
            else
            {
              
                SqlCommand cmd = new SqlCommand("GenerateRFIDCards", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RangeFrm", txtSlNoFrm.Text.Trim());
                cmd.Parameters.AddWithValue("@RangeTo", txtSlNoTo.Text.Trim());
                cmd.Parameters.AddWithValue("@CardType", ddlCardType.SelectedValue.ToString());
                SqlParameter spm = cmd.Parameters.Add("@RetVal", SqlDbType.VarChar, 50);
                SqlParameter spm1 = cmd.Parameters.Add("@flagout", SqlDbType.Int);
                spm.Direction = ParameterDirection.Output;
                spm.Value = "Not Done";
                spm1.Direction = ParameterDirection.Output;
                spm1.Value = 0;

                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd.ExecuteNonQuery();
              
                if (spm1.Value.ToString() == "0")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblRFIDMsg.ClientID + "').style.display='none'\",5000)</script>");
                    lblRFIDMsg.CssClass = "ErrMsg";
                    lblRFIDMsg.Attributes.Add("style", "text-transform:capitalize");
                    lblRFIDMsg.ForeColor = Color.Red;
                    lblRFIDMsg.Text = spm.Value.ToString();
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblRFIDMsg.ClientID + "').style.display='none'\",5000)</script>");
                    lblRFIDMsg.CssClass = "ScsMsg";
                    lblRFIDMsg.Attributes.Add("style", "text-transform:capitalize");
                    lblRFIDMsg.Text = spm.Value.ToString();
                    txtSlNoFrm.Text = "";
                    txtSlNoTo.Text = "";
                    ddlCardType.SelectedIndex = 0;
                    FillGrid();
                }
            }
        }
        catch (Exception ex)
        { }
        finally
        {
            con.Close();
        }
    }

    protected void btnClearRFID_Click(object sender, EventArgs e)
    {
        lblRFIDMsg.CssClass = "Reset";
        txtSlNoFrm.Text = "";
        txtSlNoTo.Text = "";
        ddlCardType.SelectedIndex = 0;
        lblRFIDMsg.Text = "";
        FillGrid();
    }

    public string GenerateCards(string RangeFrm, string RangeTo, string CardType)
    {
        using (SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString()))
        {
            SqlCommand cmd = new SqlCommand("GenerateRFIDCards", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RangeFrm", RangeFrm);
            cmd.Parameters.AddWithValue("@RangeTo", RangeTo);
            cmd.Parameters.AddWithValue("@CardType", CardType);
            SqlParameter spm = cmd.Parameters.Add("@RetVal", SqlDbType.VarChar, 50);
            spm.Direction = ParameterDirection.Output;
            spm.Value = "Not Done";

            if (con.State != ConnectionState.Open)
                con.Open();
            cmd.ExecuteNonQuery();
            return spm.Value.ToString();
        }
    }

    public void FillGrid()
    {
        using (SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString()))
        {
        SqlCommand cmd = new SqlCommand("udpGetRFIDList", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Open();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            grdGetRFIDList.DataSource = dt;
            grdGetRFIDList.DataBind();
        }
        else
        {
            grdGetRFIDList.DataSource = null;
        }
        }
    }

    protected void grdGetRFIDList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        lblRFIDMsg.CssClass = "reset";
        lblRFIDMsg.Text = "";
        grdGetRFIDList.PageIndex = e.NewPageIndex;
        FillGrid();
    }

    protected void btn_next_Click(object sender, EventArgs e)
    {
        Response.Redirect("Bay.aspx");
    }
}