using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GridView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

        if (Session["ConnectionString"].ToString()==null)
        {
            Response.Redirect("login.aspx");
        }

        }
        catch (Exception ex)
        {
            Response.Redirect("login.aspx");
        }

        //SqlDataSource1.ConnectionString = Session["ConnectionString"].ToString();
        if (!IsPostBack)
        {
            getServiceType();
            getvehicleModel();
           
        }
    }

   

    protected void getvehicleModel()
    {
        String strConnString = Session["ConnectionString"].ToString();
        SqlConnection con = new SqlConnection(strConnString);
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "GetVehicleModelforAdmin";
        cmd.Connection = con;
        try
        {
            con.Open();
            GridView1.EmptyDataText = "No Records Found";
            GridView1.DataSource = cmd.ExecuteReader();
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
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

                uploader.SaveAs(serverpath + "/" + uploader.FileName);
                string str = "udpInsertVehicleModel";
                SqlCommand cmd = new SqlCommand(str, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Model", txtModel.Text.ToString());
                cmd.Parameters.AddWithValue("@ModelImageUrl", uploader.FileName.ToString());
                cmd.ExecuteNonQuery();
                con.Close();
                //bindgrid();
                //lblmsg.ForeColor = Color.Green;
                //lblmsg.Text = "Vehicle Model Saved Successfully.!";
            }
        }
        catch (Exception ex)
        {
            //lblmsg.ForeColor = Color.Red;
            //lblmsg.Text = ex.Message;
        }
    }

    protected void getServiceType()
    {
        String strConnString = Session["ConnectionString"].ToString();
        SqlConnection con = new SqlConnection(strConnString);
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "udpGetServiceTypeForadmin";
        cmd.Connection = con;
        try
        {
            con.Open();
            GridView2.EmptyDataText = "No Records Found";
            GridView2.DataSource = cmd.ExecuteReader();
            GridView2.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
    }
}