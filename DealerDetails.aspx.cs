using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI;

public partial class DealerDetails : System.Web.UI.Page
{
    private SqlCommand cmd;
    private SqlConnection con = new SqlConnection(DataManager.ConStr);
    private DataTable dt;
    private SqlDataAdapter sda;

    protected void btnClearDealer_Click(object sender, EventArgs e)
    {
        clearDealer();
    }

    protected void btnRef_Click(object sender, EventArgs e)
    {
        lblDealerMsg.Text = "";
        getDealerDetails();
    }

    protected void btnSaveDealer_Click(object sender, EventArgs e)
    {
        if (txtDCode.Text.Trim() == "")
            lblDealerMsg.Text = "Please Enter Dealer Code";
        else if (txtDName.Text.Trim() == "")
            lblDealerMsg.Text = "Please Enter Dealer Name";
        else if (txtAddr.Text.Trim() == "")
            lblDealerMsg.Text = "Please Enter Dealer Address";
        else if (txtEmail.Text.Trim() == "")
            lblDealerMsg.Text = "Please Enter Dealer Email ID";
        else if (txtDevision.Text.Trim() == "")
            lblDealerMsg.Text = "Please Enter Dealer Division";
        else
        {
            SqlConnection con = new SqlConnection(DataManager.ConStr);
            SqlCommand cmd = new SqlCommand("InsertTblDealerDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@D_Code", txtDCode.Text.Trim());
            cmd.Parameters.AddWithValue("@D_Name", txtDName.Text.Trim());
            cmd.Parameters.AddWithValue("@D_Address", txtAddr.Text.Trim());
            cmd.Parameters.AddWithValue("@D_Location", txtLocation.Text.Trim());
            cmd.Parameters.AddWithValue("@D_City", txtCity.Text.Trim());
            cmd.Parameters.AddWithValue("@D_State", txtState.Text.Trim());
            cmd.Parameters.AddWithValue("@D_ZIP", txtZip.Text.Trim());
            cmd.Parameters.AddWithValue("@D_Email", txtEmail.Text.Trim());
            cmd.Parameters.AddWithValue("@D_Division", txtDevision.Text.Trim());
            if (con.State != ConnectionState.Open)
                con.Open();
            try
            {
                cmd.ExecuteNonQuery();
                clearDealer();
                lblDealerMsg.ForeColor = Color.Green;
                lblDealerMsg.Text = "Dealer Details Saved Successfully!";
            }
            catch (Exception ex)
            {
                lblDealerMsg.ForeColor = Color.Red;
                lblDealerMsg.Text = ex.Message.ToString();
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
        }
    }

    protected void clearDealer()
    {
        lblDealerMsg.Text = "";
        txtDCode.Text = "";
        txtDName.Text = "";
        txtAddr.Text = "";
        txtLocation.Text = "";
        txtCity.Text = "";
        txtState.Text = "";
        txtZip.Text = "";
        txtEmail.Text = "";
        txtDevision.Text = "";
    }

    protected void getDealerDetails()
    {
        SqlConnection con = new SqlConnection(DataManager.ConStr);
        SqlCommand cmd = new SqlCommand("GetDealerDetails", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            txtDCode.Text = dt.Rows[0]["D_Code"].ToString();
            txtDName.Text = dt.Rows[0]["D_Name"].ToString();
            txtAddr.Text = dt.Rows[0]["D_Address"].ToString();
            txtLocation.Text = dt.Rows[0]["D_Location"].ToString();
            txtState.Text = dt.Rows[0]["D_State"].ToString();
            txtCity.Text = dt.Rows[0]["D_City"].ToString();
            txtZip.Text = dt.Rows[0]["D_Zip"].ToString();
            txtDevision.Text = dt.Rows[0]["D_Division"].ToString();
            txtEmail.Text = dt.Rows[0]["D_Email"].ToString();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    Session["CURRENT_PAGE"] = "Dealer Details";
                }
                catch (Exception ex)
                {
                }
                getDealerDetails();

                lblDealerMsg.Text = "";
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("login.aspx");
        }
    }
}