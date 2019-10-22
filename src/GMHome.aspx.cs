using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GMHome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["Current_Page"] = "GM Home";
        this.Title = "GM Home";
       GetSMSSubcription();
    }

    protected void btnCRMDisplay_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("DisplayHome.aspx?Back=333");
    }

    protected void btnReport_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Reports.aspx?Back=333");
    }

    protected void btnCRMDB_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("KPI_New.aspx");
    }

public void GetSMSSubcription() //Added by Pratik
    {
        using (SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString()))
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT SMSCount FROM tbl_SMSCount ", con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                int Credits = Convert.ToInt32(dt.Rows[0]["SMSCount"].ToString());
                int TOTSMS = 3200;

                if (Credits > TOTSMS)
                {
                    string message = "Your SMS are over to recharge contact csco@spintech.in";

                    System.Text.StringBuilder sb = new System.Text.StringBuilder();

                    sb.Append("<script type = 'text/javascript'>");

                    sb.Append("window.onload=function(){");

                    sb.Append("alert('");

                    sb.Append(message);

                    sb.Append("')};");

                    sb.Append("</script>");

                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                }

                int diff = 0;
                diff = TOTSMS - Credits;
                if (diff <= 700)
                {
                    string message = "Your SMS are About to finish to recharge contact csco@spintech.in";

                    System.Text.StringBuilder sb = new System.Text.StringBuilder();

                    sb.Append("<script type = 'text/javascript'>");

                    sb.Append("window.onload=function(){");

                    sb.Append("alert('");

                    sb.Append(message);

                    sb.Append("')};");

                    sb.Append("</script>");

                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
        }
    }


   
}