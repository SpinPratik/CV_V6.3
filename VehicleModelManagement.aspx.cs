using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VehicleModelManagement : System.Web.UI.Page
{
    private DataTable dt;
    private SqlDataAdapter sda;
    private SqlCommand cmd;
    //private SqlConnection con = new SqlConnection(DataManager.ConStr);

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["ConnectionString"] == null || Session["ConnectionString"].ToString() == "")
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

                try
                {
                    Session["CURRENT_PAGE"] = "Vehicle Model Management";
                }
                catch (Exception ex)
                {
                }

                lblmsg.Text = "";
                lblmsg.CssClass = "reset";
                bindgrid();
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("login.aspx");
        }
    }

    protected void GridView1_RowUpdating1(object sender, GridViewUpdateEventArgs e)
    {
      

        SqlConnection con = new SqlConnection(Session["ConnectionString"].ToString());
        try
        {
          
            if (((FileUpload)GridView1.Rows[e.RowIndex].Cells[1].Controls[1]).FileName=="")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                
                lblmsg.CssClass = "ErrMsg";
                lblmsg.Text = "Please select a file";
            }
            else
            { 
            int catid = int.Parse(GridView1.DataKeys[e.RowIndex].Value.ToString());
            string txtModel = ((TextBox)GridView1.Rows[e.RowIndex].Cells[0].Controls[1]).Text;
            FileUpload uploader = ((FileUpload)GridView1.Rows[e.RowIndex].Cells[1].Controls[1]);
                string txtShotCode1 = ((TextBox)GridView1.Rows[e.RowIndex].Cells[2].Controls[1]).Text;
                string serverpath = Server.MapPath("Images/CarImages");
            uploader.SaveAs(serverpath + "/" + uploader.FileName);
            con.Open();
            string str = "udpUpdateVehicleModel";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", catid);
            cmd.Parameters.AddWithValue("@Model", txtModel);
            cmd.Parameters.AddWithValue("@ModelImageUrl", uploader.FileName.ToString());
                cmd.Parameters.AddWithValue("@ShotCode", txtShotCode1.ToUpperInvariant());
            cmd.ExecuteNonQuery();
            con.Close();
            GridView1.EditIndex = -1;
            bindgrid();
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                lblmsg.CssClass = "ScsMsg";
                lblmsg.Text = "Vehicle Model Updated Successfully.!";
            }
        }
        catch (Exception ex)
        {
            lblmsg.ForeColor = Color.Red;
            lblmsg.Text = ex.Message;
        }
    }

    protected void GridView1_RowEditing1(object sender, GridViewEditEventArgs e)
    {
        lblmsg.CssClass = "reset";
        lblmsg.Text = "";
        GridView1.EditIndex = e.NewEditIndex;
        bindgrid();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        SqlConnection con = new SqlConnection(Session["ConnectionString"].ToString());
        try
        {
            if (e.CommandName.Equals("Add"))
            {
                con.Open();
                TextBox txtModel = (TextBox)GridView1.FooterRow.FindControl("txtModel1");
                FileUpload uploader = ((FileUpload)GridView1.FooterRow.FindControl("uploader1"));
                string serverpath = Server.MapPath("Images/CarImages");
                TextBox txtShot = (TextBox)GridView1.FooterRow.FindControl("txtShot1");
                uploader.SaveAs(serverpath + "/" + uploader.FileName);
                string str = "udpInsertVehicleModel";
                SqlCommand cmd = new SqlCommand(str, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Model", txtModel.Text.ToString());
                cmd.Parameters.AddWithValue("@ModelImageUrl", uploader.FileName.ToString());
                cmd.Parameters.AddWithValue("@ShotCode", txtShot.Text.ToUpperInvariant().ToString());
                cmd.ExecuteNonQuery();
                con.Close();
                bindgrid();
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                lblmsg.CssClass = "ScsMsg";
                lblmsg.Text = "Vehicle Model Saved Successfully.!";
                if (lblmsg.Text.Contains("Saved Successfully.!"))
                {
                    SqlCommand cmd1 = new SqlCommand("getVehModelBYEmpId", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@Model", txtModel.Text.ToString());
                    cmd1.Parameters.AddWithValue("@modelStCode", txtShot.Text.ToUpperInvariant().ToString());
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                        cmd1.ExecuteNonQuery();
                        bindgrid();
                    }

                }
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
            lblmsg.CssClass = "ErrMsg";
            lblmsg.Text ="Please try again!";
        }
    }

    protected void GridView1_RowCancelingEdit1(object sender, GridViewCancelEditEventArgs e)
    {
        lblmsg.Text = "";
        lblmsg.CssClass = "reset";
        GridView1.EditIndex = -1;
        bindgrid();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        lblmsg.Text = "";
        lblmsg.CssClass = "reset";
        GridView1.PageIndex = e.NewPageIndex;
        bindgrid();
    }

    private void bindgrid()
    {
        SqlConnection con = new SqlConnection(Session["ConnectionString"].ToString());
        string str = "GetVehicleModelforAdmin";
        SqlCommand cmd = new SqlCommand(str, con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(str, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = this.Get_EmptyDataTable();
            GridView1.DataBind();
            GridView1.Rows[0].Visible = false;
        }
    }

    public DataTable Get_EmptyDataTable()
    {
        DataTable dtEmpty = new DataTable();
        //Here ensure that you have added all the column available in your gridview
        dtEmpty.Columns.Add("ID", typeof(string));
        dtEmpty.Columns.Add("Model", typeof(string));
        dtEmpty.Columns.Add("ModelImageUrl", typeof(string));
        dtEmpty.Columns.Add("ShotCode", typeof(string));
        DataRow datatRow = dtEmpty.NewRow();

        //Inserting a new row,datatable .newrow creates a blank row
        dtEmpty.Rows.Add(datatRow);//adding row to the datatable
        return dtEmpty;
    }

    protected void GridView1_RowDeleting1(object sender, GridViewDeleteEventArgs e)
    {
        SqlConnection con = new SqlConnection(Session["ConnectionString"].ToString());
        try
        {
            con.Open();
            int i = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
            SqlCommand cmd = new SqlCommand("udpDeleteVehicleModel", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", i);
            cmd.ExecuteNonQuery();
            bindgrid();
            con.Close();

            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
            lblmsg.CssClass = "ScsMsg";
            lblmsg.Text = "Vehicle Model Deleted Successfully.!";
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
            lblmsg.CssClass = "ErrMsg";
            lblmsg.Text = "Please try again!";
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      
        
    }
}