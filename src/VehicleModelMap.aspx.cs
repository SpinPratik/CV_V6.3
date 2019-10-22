using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VehicleModelMap : System.Web.UI.Page
{
    private DataTable dt;
    private SqlDataAdapter sda;
    private SqlCommand cmd;
    private SqlConnection con = new SqlConnection(DataManager.ConStr);

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    CreateMapVeh();
                    Session["CURRENT_PAGE"] = "Vehicle Model Mapping";
                }
                catch (Exception ex)
                {
                }

                fillCustModelsNSavedModels();

                lblMapMsg.Text = "";
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("login.aspx");
        }
    }

    protected void CreateMapVeh()
    {
        MapVeh = new DataTable();
        MapVeh.Columns.Add("Existing Model");
        MapVeh.Columns.Add("Uploaded Model");
        grdVehMap.DataSource = MapVeh;
        grdVehMap.DataBind();
    }

    protected void NotificationTimer_Tick(object sender, EventArgs e)
    {
    }

    private static DataTable MapVeh = new DataTable();

    protected void btnMapModel_Click(object sender, ImageClickEventArgs e)
    {
        if (drpCustModel.SelectedItem.Text == "--Select--")
        {
            lblMapMsg.Text = "Please Select Valid Uploaded Model !";
        }
        else if (drpExistingModel.SelectedItem.Text == "--Select--")
        {
            lblMapMsg.Text = "Please Select Valid Existing Model !";
        }
        else
        {
            MapVeh.Rows.Add(drpExistingModel.SelectedItem.Text.ToString(), drpCustModel.SelectedItem.Text.ToString());
            grdVehMap.DataSource = MapVeh;
            grdVehMap.DataBind();
            fillCustModelsNSavedModels();
        }
    }

    protected void fillCustModelsNSavedModels()
    {
        SqlConnection con = new SqlConnection(DataManager.ConStr);
        SqlDataAdapter sda = new SqlDataAdapter("udpGetVehicleModelList", con);
        sda.SelectCommand.CommandType = CommandType.StoredProcedure;
        DataTable dt = new DataTable();
        sda.Fill(dt);
        drpCustModel.Items.Clear();
        drpCustModel.Items.Add("--Select--");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            drpCustModel.Items.Add(dt.Rows[i][0].ToString());
        }
        sda = new SqlDataAdapter("udpGetVehicleExistingModelList", con);
        sda.SelectCommand.CommandType = CommandType.StoredProcedure;
        dt = new DataTable();
        sda.Fill(dt);
        drpExistingModel.Items.Clear();
        drpExistingModel.Items.Add("--Select--");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (grdVehMap.Rows.Count > 0)
            {
                string found = "no";
                for (int j = 0; j < grdVehMap.Rows.Count; j++)
                {
                    if (grdVehMap.Rows[j].Cells[0].Text == dt.Rows[i][0].ToString())
                        found = "yes";
                }
                if (found == "no")
                    drpExistingModel.Items.Add(dt.Rows[i][0].ToString());
            }
            else
                drpExistingModel.Items.Add(dt.Rows[i][0].ToString());
        }
    }

    protected void grdVehMap_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int rindx = e.RowIndex;
            MapVeh.Rows.RemoveAt(rindx);
            grdVehMap.DataSource = MapVeh;
            grdVehMap.DataBind();
        }
        catch (Exception ex)
        {
        }
    }

    protected void btmMapSave_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(DataManager.ConStr);
            SqlCommand cmd = new SqlCommand();
            for (int i = 0; i < grdVehMap.Rows.Count; i++)
            {
                try
                {
                    cmd = new SqlCommand("InsertVehicleMap", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Model", grdVehMap.Rows[i].Cells[0].Text.ToString());
                    cmd.Parameters.AddWithValue("@Cust_Model_No", grdVehMap.Rows[i].Cells[1].Text.ToString());
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    if (con.State != ConnectionState.Closed)
                        con.Close();
                    lblMapMsg.Text = "Something went wrong while saving !";
                    return;
                }
            }
            lblMapMsg.ForeColor = Color.Green;
            lblMapMsg.Text = "Vehicle Mapped Succesfully !";
            MapVeh.Rows.Clear();
            grdVehMap.DataSource = MapVeh;
            grdVehMap.DataBind();
        }
        catch (Exception ex)
        {
            lblMapMsg.ForeColor = Color.Red;
            lblMapMsg.Text = ex.Message.ToString();
        }
    }

    protected void btnModelClear_Click(object sender, EventArgs e)
    {
        lblMapMsg.Text = "";
        MapVeh.Rows.Clear();
        grdVehMap.DataSource = MapVeh;
        grdVehMap.DataBind();
        fillCustModelsNSavedModels();
    }
}