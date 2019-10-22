using System;
using System.Data;
using System.Data.SqlClient;

public partial class PrintNewBill : System.Web.UI.Page
{
    private SqlConnection con = new SqlConnection(DataManager.ConStr);

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblDealer.Text = Session["CompanyName"].ToString();
            GetBillAmountDetails();
        }
        catch (Exception ex)
        {
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Cashier.aspx");
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("Cashier.aspx");
    }

    private void GetBillAmountDetails()
    {
        try
        {
            string SlNo = Session["SlNoNew"].ToString();
            SqlCommand cmd = new SqlCommand("UdpPrintDetailsForNewBill", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SlNo", SlNo);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                lblVehNo.Text = dt.Rows[0]["RegNo"].ToString();
                lblTagNo.Text = dt.Rows[0]["RFID"].ToString();
                lblGateIn.Text = dt.Rows[0]["Vehicle In"].ToString();
                lblGateOut.Text = dt.Rows[0]["Vehicle Out"].ToString();
                lblBillAmount.Text = dt.Rows[0]["BillAmount"].ToString();
                lblDate.Text = dt.Rows[0]["GatePass"].ToString();
            }
        }
        catch (Exception ex)
        {
        }
    }
}