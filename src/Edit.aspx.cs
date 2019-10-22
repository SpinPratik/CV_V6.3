using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Edit : Page
{
    private DataTable table;
  //  private SqlConnection dbaseCon = new SqlConnection(ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session[Session["TMLDealercode"] + "-TMLConString"].ToString() == null || Session[Session["TMLDealercode"] + "-TMLConString"].ToString() == "")
                Response.Redirect("login.aspx");

        }
        catch
        {
            Response.Redirect("login.aspx");
        }
        TextBoxVeh.Attributes.Add("readonly", "readonly");
        TextBoxStart.Attributes.Add("readonly", "readonly");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        if (!IsPostBack)
        {
            DataRow dr = DbGetEvents(Request.QueryString["id"]);
            
            if (dr == null)
            {
                throw new Exception("The event was not found");
            }
           
            TextBoxAllot.Text = Convert.ToInt16(dr["AllotTime"]).ToString();
            TextBoxVeh.Text = dr["RegNo"].ToString();
            TextBoxName.Text= dr["EmpName"].ToString();
            TextBoxName.Enabled = false;

            if (dr["ITStatus"].ToString()=="0")
            {
                TextBoxStart.Text = dr["InTime"].ToString();
                TextBoxStart.Enabled = false;
                btn_Delete.Enabled = true;
            }
            else
            {
                TextBoxStart.Text = dr["InTime"].ToString();
                TextBoxStart.Enabled = true;
                btn_Delete.Enabled = true;
            }

             using (SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("GetBays_TLWise"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TLID", Session["TLID"].ToString());
                    cmd.Connection = con;
                    con.Open();
                    DropDownList1.DataSource = cmd.ExecuteReader();
                    DropDownList1.DataTextField = "Name";
                    DropDownList1.DataValueField = "Id";
                    DropDownList1.DataBind();
                }
            }
            DropDownList1.Items.Insert(0, new ListItem("--Select Bay--", "0"));
            DropDownList1.SelectedValue = Request.QueryString["r"];
        }
      
    }
     private void initData()
    {
        string id = Request.QueryString["hash"];

        if (Session[id] == null)
        {
            Session[id] = DataGeneratorScheduler.GetData();
        }
        table = (DataTable)Session[id];
    }
  
    protected void ButtonOK_Click(object sender, EventArgs e)
    {
        if (TextBoxName.Text.ToString().Trim() == "")
        {
            //lblMsg.Text = "Please Enter BillAmount..!";
            //TextBoxName.Focus();
        }
        else
        {
            DateTime start = Convert.ToDateTime(TextBoxStart.Text);
            //  DateTime end = Convert.ToDateTime(TextBoxEnd.Text);
            string name = TextBoxName.Text;
            string id = Request.QueryString["id"];
            dbUpdateEvent(id, start, name);
            //Modal.Close(this, "OK");
        }
    }


    private DataRow DbGetEvents(string id)
    {
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlCommand cm = new SqlCommand("UdpGetAllotmentHover", con);
        cm.CommandType = CommandType.StoredProcedure;
        cm.Parameters.AddWithValue("@AllotId", id);
        SqlDataAdapter da = new SqlDataAdapter(cm);
       
        DataTable dt = new DataTable();
        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            return dt.Rows[0];
        }
        return null;
    }

    private void dbUpdateEvent(string id, DateTime start, string name)
    {
        bool check = false;
        using (SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString()))
        { 
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UpdateTblJobAllotment_New", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AllotID", id);
                cmd.Parameters.AddWithValue("@InTime", start);
                cmd.Parameters.AddWithValue("@RegNo", TextBoxVeh.Text.ToString());
                cmd.Parameters.AddWithValue("@BayID", DropDownList1.SelectedValue);
                cmd.Parameters.AddWithValue("@AllotTime", TextBoxAllot.Text.ToString());
                //cmd.Parameters.AddWithValue("@Express", ExpTime.Checked);
                cmd.Parameters.AddWithValue("@Empname", name);

                cmd.Parameters.Add("@flag", SqlDbType.Int);
                cmd.Parameters["@flag"].Direction = ParameterDirection.Output;
                cmd.Parameters["@flag"].Value = 0;
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd.ExecuteNonQuery();
                string msg = Convert.ToString(cmd.Parameters["@flag"].Value);

                switch (msg)
                {
                    case "0":
                        lblMessage.ForeColor = Color.Red;
                        lblMessage.Text = "Error in writing to database !";
                        check = false;
                        break;

                    case "1":
                        check = true;
                        Modal.Close(this, "OK");
                        break;

                    case "2":
                        lblMessage.ForeColor = Color.Red;
                        lblMessage.Text = "Technician not available !";
                        check = false;
                        break;

                    case "3":
                        lblMessage.ForeColor = Color.Red;
                        lblMessage.Text = "Bay not available !";
                        check = false;
                        break;

                    case "4":
                        lblMessage.ForeColor = Color.Red;
                        lblMessage.Text = "Vehicle is already assign to another Bay !";
                        check = false;
                        break;

                    case "5":
                        lblMessage.ForeColor = Color.Red;
                        lblMessage.Text = "Promise Delivery Time is near to cross or already crossed !";
                        check = false;
                        break;

                    case "7":
                        lblMessage.ForeColor = Color.Red;
                        lblMessage.Text = "Allotment In-Time is not Valid. !";
                        check = false;
                        break;

                    default:
                        check = false;
                        break;
                }
            }

            catch (Exception ex)
            {
                check = false;
            }
            finally
            {
                //if (con.State != ConnectionState.Closed)
                //    con.Close();
            }
        }   
   }
    protected void DropDownList1_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        TextBoxName.Text = DropDownList1.SelectedValue;
    }

    private void dbDeleteEvent(string id)
    {
        using (SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString()))
        {
            try { 
            con.Open();
            SqlCommand cmd = new SqlCommand("DeleteTblJobAllotment", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AllotID", id);
            cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {

            }
        }
    }
    
    protected void LinkButtonDelete_Click(object sender, EventArgs e)
    {
        string id = Request.QueryString["id"];
        dbDeleteEvent(id);
        Modal.Close(this, "OK");
    }
    private void dbCancelEvent(string id)
    {


    }

    protected void ButtonCancel_Click(object sender, EventArgs e)
    {
        Modal.Close(this);
    }
    protected void Cancel_Click(object sender, EventArgs e)
    {
        string id = Request.QueryString["id"];
        dbCancelEvent(id);
        Modal.Close(this, "OK");
    }

    
}
