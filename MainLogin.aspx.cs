using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Configuration;


public partial class MainLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        string str = ConfigurationManager.ConnectionStrings["Cloud_WMS_ConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(str);
        if (conn.State != ConnectionState.Open)
        {
            conn.Open();
        }
        string Dealercode = txt_delaercode.Text;
        Session["TMLDealercode"] = txt_delaercode.Text.Trim();
        string query = "SELECT DatabaseName FROM Tbl_CurrentDealers WHERE Dealercode=@Dealercode";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = query;
        cmd.Parameters.AddWithValue("@DealerCode", Dealercode);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            dr.Read();
            Session[Session["TMLDealercode"] + "-TMLConString"] = "Data Source=52.172.205.17;Initial Catalog=" + dr.GetValue(0).ToString() + ";User ID=wmsv6;Password=admin@123;Connection TimeOut=1200000; Pooling=False;Max Pool Size=5000;Min Pool Size=1";
            conn.Close();

        }
        Response.Redirect("Display.aspx");
    }
}