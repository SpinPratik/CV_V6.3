using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Configuration;

public partial class Registration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txt_dealer.Attributes.Add("autocomplete", "off");
        if (!IsPostBack)
        {
            selectCity("0");
            FillStates("UdpGetStates", drp_states);

        }
    }

    protected void btn_reset_Click(object sender, EventArgs e)
    {
        txt_area.Text = "";
        //txt_city.Text = "";
        txt_dealer.Text = "";
        txt_email.Text = "";
        txt_Fname.Text = "";
        txt_lname.Text = "";
        txt_mobNum.Text = "";
        txt_orgName.Text = "";
        txt_phoneNum.Text = "";
        txt_pincode.Text = "";
        //txt_plan.Text = "";
        txt_pswd.Text = "";
        txt_role.Text = "";
        //txt_state.Text = "";
        txt_website.Text = "";
    }

    public void FillStates(string procedureName, DropDownList fill_comboBox)
    {
        string constr = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(constr);
        try
        {
            DataTable sdt = new DataTable();
            SqlCommand cmd = new SqlCommand(procedureName, con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter ssda = new SqlDataAdapter(cmd);
            ssda.Fill(sdt);
            fill_comboBox.Items.Clear();
            ListItem lstItem;

            for (int i = 0; i < sdt.Rows.Count; i++)
            {
                lstItem = new ListItem();
                lstItem.Text = sdt.Rows[i]["StateName"].ToString();
                lstItem.Value = sdt.Rows[i]["StateId"].ToString();
                fill_comboBox.Items.Add(lstItem);
            }

        }
        catch (Exception ex) { }
    }

    protected void drp_states_SelectedIndexChanged(object sender, EventArgs e)
    {
        selectCity(drp_states.SelectedValue.ToString());
    }

    protected void selectCity(string stateId)
    {
        string constr = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ConnectionString;

        SqlConnection con = new SqlConnection(constr);
        {
            using (SqlCommand cmd = new SqlCommand("UdpGetDistricts", con))
            {
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StateId", stateId);
                cmd.Connection = con;

                drp_district.DataSource = cmd.ExecuteReader();
                drp_district.DataTextField = "DistrictName";
                drp_district.DataValueField = "DistrictId";
                drp_district.DataBind();
                con.Close();
            }
        }

    }
}