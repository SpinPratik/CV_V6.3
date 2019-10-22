using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class IdleTime : System.Web.UI.Page
{
    private SqlCommand cmd;
   
    private DataTable dt;
    private SqlDataAdapter sda;

    protected void grvIdelTime_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lbIdleTimeMsg.Text = "";
       // getGridDetails();

        if(e.CommandName== "Update_EscalationTime")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
            con.Open();
            SqlTransaction transaction = con.BeginTransaction();
            try
            {

                SqlCommand cmd = new SqlCommand("udpUpdateprocessmgnt", con, transaction);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProcessName", grvIdelTime.Rows[index].Cells[0].Text.ToString());
                cmd.Parameters.AddWithValue("@ProcessTime", ((TextBox)grvIdelTime.Rows[index].FindControl("txtprocessTime")).Text);
                cmd.Parameters.AddWithValue("@EscalationTime", ((TextBox)grvIdelTime.Rows[index].FindControl("txtEscalationTime")).Text);
               // con.Open();
                cmd.ExecuteNonQuery();
               // cmd.Parameters.Clear();
                transaction.Commit();
                grvIdelTime.DataBind();
                getGridDetails();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Updated Successfully')", true);
                //lbIdleTimeMsg.CssClass = "ScsMsg";
                //lbIdleTimeMsg.Attributes.Add("style", "text-transform:none");
                //lbIdleTimeMsg.Text = "Updated successfully.";
                //ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lbIdleTimeMsg.ClientID + "').style.display='none'\",5000)</script>");

            }
            catch (Exception ex)
            {
                transaction.Rollback();
              
                lbIdleTimeMsg.Text = "Error : " + ex.Message.ToString();
                lbIdleTimeMsg.CssClass = "ErrMsg";
                lbIdleTimeMsg.Attributes.Add("style", "text-transform:capitalize");
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lbIdleTimeMsg.ClientID + "').style.display='none'\",5000)</script>");
            }
            finally
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Updated Successfully')", true);
                con.Close();
            }

        }
       
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session[Session["TMLDealercode"] + "-TMLConString"] == null || Session[Session["TMLDealercode"] + "-TMLConString"].ToString() == "")
                Response.Redirect("login.aspx");

        }
        catch
        {
            Response.Redirect("login.aspx");
        }
        try
        {
            if (!Page.IsPostBack)
            {
                getGridDetails();
                try
                {
                    Session["CURRENT_PAGE"] = "Escalation Settings";
                }
                catch (Exception ex)
                {
                }
                lbIdleTimeMsg.Text = "";
                lbIdleTimeMsg.CssClass = "reset";
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("login.aspx");
        }
    }
    protected void getGridDetails()
    {
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());

        try
        { 
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("UdpGetProcessStandardandEscalationTime",con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        if(con.State!=ConnectionState.Open)
        {
            con.Open();
        }
        sda.Fill(dt);
        grvIdelTime.DataSource = dt;
        grvIdelTime.DataBind();
        }
        catch (Exception ex)
        {
           
        }
        finally { con.Close(); }
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

   
}