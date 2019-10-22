using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ServiceType : System.Web.UI.Page
{
    private DataTable dt;
    private SqlDataAdapter sda;
    private SqlCommand cmd;
    //private SqlConnection con = new SqlConnection(DataManager.ConStr);

    protected void Page_Load(object sender, EventArgs e)
    {
        lblmsg0.Text = "";
        lblmsg0.CssClass = "reset";
        try
        {
            if (Session["ConnectionString"].ToString() == null)
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
                    Session["CURRENT_PAGE"] = "Service Type";
                }
                catch (Exception ex)
                {
                }
                bindgrids();
                lblmsg0.Text = "";
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("login.aspx");
        }
    }

    private void bindgrids()
    {
        SqlConnection con = new SqlConnection(Session["ConnectionString"].ToString());
        string str = "select ServiceID,ServiceType,ProcessTime,ServiceType_SCode from tblServiceTypes";
        
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GridView2.DataSource = dt;
            GridView2.DataBind();
        }
        else
        {
            GridView2.DataSource = this.Get_EmptyDataTable();
            GridView2.DataBind();
            GridView2.Rows[0].Visible = false;
        }
    }

    public DataTable Get_EmptyDataTable()
    {
        DataTable dtEmpty = new DataTable();
        //Here ensure that you have added all the column available in your gridview
        dtEmpty.Columns.Add("ServiceID", typeof(string));
        dtEmpty.Columns.Add("ServiceType", typeof(string));
        dtEmpty.Columns.Add("ProcessTime", typeof(string));
        dtEmpty.Columns.Add("ServiceType_SCode", typeof(string));
        DataRow datatRow = dtEmpty.NewRow();

        //Inserting a new row,datatable .newrow creates a blank row
        dtEmpty.Rows.Add(datatRow);//adding row to the datatable
        return dtEmpty;
    }

    protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
    {
        lblmsg0.Text = "";
        lblmsg0.CssClass = "reset";
        GridView2.EditIndex = e.NewEditIndex;
        bindgrids();
    }

    protected void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        SqlConnection con = new SqlConnection(Session["ConnectionString"].ToString());
        try
        {
            int catid = int.Parse(GridView2.DataKeys[e.RowIndex].Value.ToString());
            string txtServiceType = ((TextBox)GridView2.Rows[e.RowIndex].Cells[1].FindControl("txtServiceType")).Text;
            string txtProcessTime = ((TextBox)GridView2.Rows[e.RowIndex].Cells[2].FindControl("txtProcessTime")).Text;
            string txtsScode = ((TextBox)GridView2.Rows[e.RowIndex].Cells[3].FindControl("txtSCode")).Text.ToUpperInvariant();
            con.Open();
            string str = "update tblServiceTypes set ServiceType='" + txtServiceType + "',ProcessTime='" + txtProcessTime + "',ServiceType_SCode='" + txtsScode.ToString() + "' where ServiceID='" + catid + "'";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.ExecuteNonQuery();
            con.Close();
            lblmsg0.Text = "Service Successfully Updated. !";
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg0.ClientID + "').style.display='none'\",5000)</script>");
            lblmsg0.CssClass = "ScsMsg";
            GridView2.EditIndex = -1;
            bindgrids();
        }
        catch (Exception ex)
        {
        }
    }

    //getstTypeBYEmpId

    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SqlConnection con = new SqlConnection(Session["ConnectionString"].ToString());
        con.Open();
        int i = Convert.ToInt32(GridView2.DataKeys[e.RowIndex].Value.ToString());

        string Query = "Delete tblServiceTypes where ServiceID= '" + i + "'";
        SqlCommand cmd = new SqlCommand(Query, con);
        cmd.ExecuteNonQuery();
        bindgrids();
        lblmsg0.CssClass = "ScsMsg";
        lblmsg0.Text = "Service Type Deleted Successfully";
        ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg0.ClientID + "').style.display='none'\",5000)</script>");

        con.Close();
    }

    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        SqlConnection con = new SqlConnection(Session["ConnectionString"].ToString());
        try
        {
            if (e.CommandName.Equals("Add"))
            {
                TextBox txtServiceType = (TextBox)GridView2.FooterRow.FindControl("txtServiceType");
                TextBox txtProcessTime = (TextBox)GridView2.FooterRow.FindControl("txtProcessTime");
                TextBox txtScode = (TextBox)GridView2.FooterRow.FindControl("txtScode");
                if (txtServiceType.Text.Trim() != "" && txtProcessTime.Text.Trim() != "" && txtScode.Text.Trim() != "")
                {
                    string str = "insert into tblServiceTypes(ServiceType,ProcessTime,ServiceType_SCode) values('" + txtServiceType.Text + "','" + txtProcessTime.Text + "','" + txtScode.Text.ToUpperInvariant() + "')";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(str, con);
                    cmd.ExecuteNonQuery();
                    lblmsg0.CssClass = "ScsMsg";
                    lblmsg0.Text = "Service Successfully Saved. !";
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg0.ClientID + "').style.display='none'\",5000)</script>");

                    con.Close();
                    bindgrids();
                }
                else
                {
                    lblmsg0.CssClass = "ErrMsg";
                    lblmsg0.Text = "You Must Enter Service Type, ProcessTime And Short Code.!";
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg0.ClientID + "').style.display='none'\",5000)</script>");
                
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void GridView2_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        lblmsg0.Text = "";
        lblmsg0.CssClass = "reset";
        GridView2.EditIndex = -1;
        bindgrids();
    }

    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        bindgrids();
    }
}