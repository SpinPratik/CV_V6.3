using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI;

public partial class DealerBayDetails : System.Web.UI.Page
{
    private string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
    private SqlConnection oConn;
    private DateTime dt1;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    Session["CURRENT_PAGE"] = "Dealer Bay Details";
                }
                catch (Exception ex)
                {
                }
                FillDealerBayDetails();
                lblDealerBayDetails.Text = "";
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("login.aspx");
        }
    }

    private void FillDealerBayDetails()
    {
        try
        {
            oConn = new SqlConnection(sConnString);
            SqlCommand cmd = new SqlCommand("getDealerBayDetails", oConn);
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable oDt = new DataTable();
            cmd.Parameters.Clear();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(oDt);
            if (oDt.Rows.Count > 0)
            {
                txtSpeedoBays.Text = oDt.Rows[0]["SpeedoBays"].ToString();
                txtWABays.Text = oDt.Rows[0]["WABays"].ToString();
                txtWashBays.Text = oDt.Rows[0]["WashBays"].ToString();
                //txtBodyShopBays.Text = oDt.Rows[0]["BodyshopBays"].ToString();
                txtTotalBays.Text = oDt.Rows[0]["TotalBays"].ToString();
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void btnSave_Click1(object sender, EventArgs e)
    {
        try
        {
            lblDealerBayDetails.Text = "";

            oConn = new SqlConnection(sConnString);
            oConn.Open();
            SqlCommand cmd = new SqlCommand("UpdateDealerBayDetails", oConn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SpeedoBays", txtSpeedoBays.Text);
            cmd.Parameters.AddWithValue("@WABays", txtWABays.Text);
            cmd.Parameters.AddWithValue("@WashBays", txtWashBays.Text);
            cmd.Parameters.AddWithValue("@TotalBays", txtTotalBays.Text);
            //cmd.Parameters.AddWithValue("@BodyshopBays", txtBodyShopBays.Text);
            cmd.Parameters.AddWithValue("@DOM", DateTime.Now);
            cmd.ExecuteNonQuery();
            oConn.Close();
            lblDealerBayDetails.ForeColor = Color.Green;
            lblDealerBayDetails.Text = "Dealer Bay Details Updated Successfully..!";
            FillDealerBayDetails();
        }
        catch (Exception ex)
        {
            lblDealerBayDetails.ForeColor = Color.Red;
            lblDealerBayDetails.Text = ex.Message.ToString();
        }
    }

    protected void btnRefreshStdTime_Click(object sender, EventArgs e)
    {
        FillDealerBayDetails();
        lblDealerBayDetails.Text = "";
    }
}