using System;

using System.Web.UI;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Drawing;

public partial class New : Page
{
    private DataTable table;
    private static string stdTime = "";
 
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session[Session["TMLDealercode"] + "-TMLConString"].ToString() == null || Session[Session["TMLDealercode"] + "-TMLConString"].ToString() == "")
            {
               Modal.Close(this);
               // Response.Redirect("login.aspx");
            }
             

        }
        catch
        {
           // Response.Redirect("login.aspx");
        }
        try
        {
            if (Session["TLID"].ToString() == null)
            {
                Modal.Close(this);
                Response.Redirect("NewJobAllotment.aspx");
            }
            else
            {
              //  FillTechnicianDashboard(Convert.ToInt16(Session["TLID"].ToString()));
            }
            TextBoxVeh.Attributes.Add("readonly", "readonly");
            TextBoxName.Attributes.Add("readonly", "readonly");
            TextBoxName1.Attributes.Add("readonly", "readonly");
            TextBoxName2.Attributes.Add("readonly", "readonly");
            TextBoxStd.Attributes.Add("readonly", "readonly");
            TextBoxStd1.Attributes.Add("readonly", "readonly");
            TextBoxStd2.Attributes.Add("readonly", "readonly");

            if (TextBoxjc.Visible == true)
            {
                txtjobDesc.Attributes.Add("readonly", "readonly");
            }
            else if (TextBoxjc.Visible == false)
            {
                txtjobDesc.Attributes.Remove("readonly");
            }

            if (drpJobcode2.Visible == true)
            {
                txtjobdesc1.Attributes.Add("readonly", "readonly");
            }
            else
            {
                txtjobdesc1.Attributes.Remove("readonly");
            }

            if (drpJobcode3.Visible == true)
            {
                txtjobdesc2.Attributes.Add("readonly", "readonly");
            }
            else
            {
                txtjobdesc2.Attributes.Remove("readonly");
            }


           FillVehicleDashboard("GetRegTagList_NEW");

            if (!IsPostBack)
            {
                bool time = Request.QueryString["time"] == "yes";
                RadioButtonList1.SelectedIndex = 0;
                if (time)
                {
                    TextBoxStart1.Text = Convert.ToDateTime(Request.QueryString["start"]).ToString("M/d/yyyy HH:mm");
                }
                else
                {
                    TextBoxStart1.Text = Convert.ToDateTime(Request.QueryString["start"]).ToString("M/d/yyyy HH:mm");
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
        catch (Exception ex)
        {
            Response.Redirect("NewJobAllotment.aspx");
        }
        }


    //TECHNICIAN DETAILS
    public void FillTechnicianDashboard(int TLId)
    {
        try
        {
            external.Controls.Clear();
            string procName = "";
            procName = "UdpGetAllotmentEmp";
            DataTable dtTech1 = new DataTable();
            dtTech1 = GetTechnician(procName, TLId);
            var cell1 = new HtmlTableCell();
            cell1.VAlign = "Top";
            cell1.Style.Value = "width:11.11%;";
            DataTable Technician = new DataTable();

            Technician = GetTechnician(procName, TLId);
            int totalVehicle1 = Technician.Rows.Count;
            Panel pnl1 = new Panel();
            pnl1.ID = ("pnl1_" + ToString());
            Label lbl1 = new Label();
            for (int j = 0; j < totalVehicle1; j++)
            {
                Panel pnla1 = new Panel();
                Technicians vt1 = (Technicians)Page.LoadControl("Technicians.ascx");
                vt1.Tech = Technician.Rows[j]["EmpName"].ToString();
                vt1.Employe_Type = Technician.Rows[j]["EmpType"].ToString();
                vt1.ID = Technician.Rows[j]["EmpId"].ToString();
                vt1.Employe_Status = Technician.Rows[j]["status"].ToString();
                pnla1.Controls.Add(vt1);
                pnl1.Controls.Add(pnla1);
            }

            cell1.Controls.Add(lbl1);
            cell1.Controls.Add(pnl1);
            external.Controls.Add(cell1);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private DataTable GetTechnician(string procedureName, int TLID)
    {
        SqlConnection dbaseCon = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        try
        {

            SqlCommand cmd = new SqlCommand(procedureName, dbaseCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TLId", TLID);
            cmd.Parameters.AddWithValue("@AllotDate", TextBoxStart1.Text.ToString());
            cmd.Parameters.AddWithValue("@RegNo", TextBoxVeh.Text);

            SqlDataAdapter da1 = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da1.Fill(dt);
            return dt;

        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        finally
        {

        }
    }

    //VEHICLE DETAILS

    public void FillVehicleDashboard(string procName)
    {
        try
        {
            external1.Controls.Clear();
            String Error = "";
            var cell = new HtmlTableCell();
            cell.VAlign = "Top";
            cell.Style.Value = "width:11.11%;";
            DataTable dtVehicle = new DataTable();
            if (string.Equals(procName, "GetRegTagList_NEW", StringComparison.OrdinalIgnoreCase))
            {
                dtVehicle = Getvehicle(procName);
            }
            else if (string.Equals(procName, "CheckAllotmentByRegNo_New", StringComparison.OrdinalIgnoreCase))
            {
                dtVehicle = GetVehicleByRegNO(procName, txtsearch.Text);
                Error = "RegNo";
            }

            else if (string.Equals(procName, "CheckAllotmentByTagNo_New", StringComparison.OrdinalIgnoreCase))
            {
                dtVehicle = GetVehicleByTagNO(procName, txtsearch.Text);
                Error = "TagNo";
            }
            if (dtVehicle.Rows.Count == 0 && Error!="")
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = "Wrong " + Error + ".!";
            }
            else
            {
                lblMessage.Text = "";
                int totalVehicle = dtVehicle.Rows.Count;
                Panel pnl = new Panel();
                pnl.ID = ("pnl_" + ToString());
                Label lbl = new Label();

                for (int j = 0; j < totalVehicle; j++)
                {
                    Panel pnla = new Panel();
                    AllotmentVehicles vt = (AllotmentVehicles)Page.LoadControl("AllotmentVehicles.ascx");
                    regno.Text= dtVehicle.Rows[j]["Reg No"].ToString();
                    vt.RegNo = dtVehicle.Rows[j]["Reg No"].ToString();
                    vt.Slno = dtVehicle.Rows[j]["Slno"].ToString();
                    vt.ServiceAdvisor = dtVehicle.Rows[j]["SA"].ToString();
                    vt.Model = dtVehicle.Rows[j]["Tag No"].ToString();
                    vt.VehicleImage =DataManager.car_image(dtVehicle.Rows[j]["ModelImageUrl"].ToString());
                    if (dtVehicle.Rows[j]["PDT"].ToString().Contains("#"))
                    {
                        vt.VehicleColor = "Orange";
                        vt.PDT = dtVehicle.Rows[j]["PDT"].ToString().Split('#')[0];
                    }
                    else
                    vt.PDT = dtVehicle.Rows[j]["PDT"].ToString();
                    vt.PDTStatus = DataManager.jcr_image(dtVehicle.Rows[j]["PDTStatus"].ToString());
                    vt.CWJDP = DataManager.jcr_image(dtVehicle.Rows[j]["CWJDP"].ToString());
                    pnla.Controls.Add(vt);
                    pnl.Controls.Add(pnla);
                }

                cell.Controls.Add(lbl);
                cell.Controls.Add(pnl);
                external1.Controls.Add(cell);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    protected void Search_Button_Click(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedIndex == 0)
        {
            if (txtsearch.Text == "")
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = "Please enter RegNo.";
            }
            else 
            FillVehicleDashboard("CheckAllotmentByRegNo_New");
        }
        else {
            if (txtsearch.Text == "")
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = "Please enter TagNo.";
            }
            else
                FillVehicleDashboard("CheckAllotmentByTagNo_New");
        }
        //if (external1.ToString() == "")
        //{
        //    lblMessage.Text = "Vehicle not found";
        //}
    }
    private DataTable GetVehicleByRegNO(string procedurename,string regno)
    {
        SqlConnection dbaseCon = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        try
        {
            
            SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
            SqlCommand cmd = new SqlCommand("CheckAllotmentByRegNo_New", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RegNo", txtsearch.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            if (dbaseCon.State == ConnectionState.Open)
                dbaseCon.Close();
        }
    }
    private DataTable GetVehicleByTagNO(string procedurename, string regno)
    {
        SqlConnection dbaseCon = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        try
        {
           
            SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
            SqlCommand cmd = new SqlCommand("CheckAllotmentByTagNo_New", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TagNo", txtsearch.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            if (dbaseCon.State == ConnectionState.Open)
                dbaseCon.Close();
        }
    }

    private DataTable Getvehicle(string procedureName)
    {
        SqlConnection dbaseCon = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        try
        {
            if (dbaseCon.State == ConnectionState.Closed)
                dbaseCon.Open();
            SqlDataAdapter da = new SqlDataAdapter(procedureName, dbaseCon);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            if (dbaseCon.State == ConnectionState.Open)
                dbaseCon.Close();
        }
    }

  
    protected void ButtonOK_Click(object sender, EventArgs e)
    {
        string jobcode = "";
        if (txtJobcode1.Visible == false)
        {
            jobcode = TextBoxjc.Text.ToString();
        }
        else
        {
            jobcode = txtJobcode1.Text.ToString();
        }
        DateTime start = Convert.ToDateTime(TextBoxStart1.Text);
        InsertEvents(TextBoxName.Text.ToString(), start, TextBoxVeh.Text.ToString(), Convert.ToInt16(DropDownList1.SelectedValue), Convert.ToInt16(Session["TLID"].ToString()), "", jobcode, TextBoxStd.Text.ToString(), TextBoxAllot.Text.ToString());
        //InsertEvents(TextBoxName.Text.ToString(), start, TextBoxVeh.Text.ToString(), Convert.ToInt16(DropDownList1.SelectedValue), Convert.ToInt16(Session["TLID"].ToString()),"", TextBoxjc.Text.ToString(), TextBoxStd.Text.ToString(), TextBoxAllot.Text.ToString(), Convert.ToInt16(ExpTime.Checked));
       // DbGetEvents(DateTime.Now);
    }
    protected void ButtonOK_Click1(object sender, EventArgs e)
    {
        string jobcode = "";
        if (TextBoxjc1.Visible == false)
        {
            jobcode = drpJobcode2.Text.ToString();
        }
        else
        {
            jobcode = TextBoxjc1.Text.ToString();
        }
        DateTime start = Convert.ToDateTime(TextBoxStart1.Text);
        InsertEvents(TextBoxName1.Text.ToString(), start, TextBoxVeh.Text.ToString(), Convert.ToInt16(DropDownList1.SelectedValue), Convert.ToInt16(Session["TLID"].ToString()), "", jobcode, TextBoxStd1.Text.ToString(), TextBoxAllot1.Text.ToString());
        //InsertEvents(TextBoxName1.Text.ToString(), start, TextBoxVeh.Text.ToString(), Convert.ToInt16(DropDownList1.SelectedValue), Convert.ToInt16(Session["TLID"].ToString()), "", TextBoxjc1.Text.ToString(), TextBoxStd1.Text.ToString(), TextBoxAllot1.Text.ToString(), Convert.ToInt16(ExpTime.Checked));
        DbGetEvents(DateTime.Now);
    }
    protected void ButtonOK_Click2(object sender, EventArgs e)
    {
        string jobcode = "";
        if (TextBoxjc2.Visible == false)
        {
            jobcode = drpJobcode3.Text.ToString();
        }
        else
        {
            jobcode = TextBoxjc2.Text.ToString();
        }
        DateTime start = Convert.ToDateTime(TextBoxStart1.Text);
        InsertEvents(TextBoxName2.Text.ToString(), start, TextBoxVeh.Text.ToString(), Convert.ToInt16(DropDownList1.SelectedValue), Convert.ToInt16(Session["TLID"].ToString()), "", jobcode, TextBoxStd2.Text.ToString(), TextBoxAllot2.Text.ToString());
        DbGetEvents(DateTime.Now);
    }
     protected void InsertEvents(string EmpName,DateTime start,string RegNo,int BayId,int TlId,string JobDesc,string JobId,string stdTime,string AllotTime)
    {
        //initData();
        int tlid = Convert.ToInt16(Session["TLID"].ToString());
        bool check = false;
        int AllotedTime = 0;
        lblMessage.ForeColor = Color.Red;
        if (RegNo == "")
        {
            lblMessage.Text = "Please Drag Vehicle from the list  !";
        }
        else if (BayId == 0)
        {
            lblMessage.Text = "Please select bay !";
        }
        else if (EmpName == "")
        {
           lblMessage.Text = "Please Drag Employee from the list  !";
        }
       
       
        else if (AllotTime == "")
        {
            lblMessage.Text = "Please enter allot time !";
        }
        else if (AllotTime == "0")
        {
            lblMessage.Text = "Please enter valid allot time !";
        }
        else
        {
            try
            {
                int chk = int.Parse(AllotTime);
                chk = chk % 10;
                if (chk != 0)
                {
                    lblMessage.Text = "Please enter allot time multiple of 10 !";
                   
                    return;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Please enter allot time multiple of 10 !";
                  return;
            }

            using (SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString()))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("InsertTblJobAllotment_New", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Empname", EmpName);
                    cmd.Parameters.AddWithValue("@InTime", start);
                    cmd.Parameters.AddWithValue("@RegNo", RegNo);
                    cmd.Parameters.AddWithValue("@BayID", BayId);
                    cmd.Parameters.AddWithValue("@TeamLeadID", TlId);
                    cmd.Parameters.AddWithValue("@JobDesc", "");
                    cmd.Parameters.AddWithValue("@JobId ", JobId);
                    cmd.Parameters.AddWithValue("@StdTime", stdTime);
                    cmd.Parameters.AddWithValue("@AllotTime", AllotTime);
                   // cmd.Parameters.AddWithValue("@Express", express);
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
                            lblMessage.ForeColor = Color.Green;
                            lblMessage.Text = "Assigned Successfully";
                            check = true;
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
                        case "6":
                            lblMessage.ForeColor = Color.Red;
                            lblMessage.Text = "Enter correct Details";
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
                   
                }
            }
        }
    }

    protected void ButtonCancel_Click(object sender, EventArgs e)
    {
        Modal.Close(this);
    }
    private void initData()
    {
        string id = Request.QueryString["hash"];

        if (Session[id] == null)
        {
            Session[id] = DataGeneratorScheduler.GetData();
            Response.Redirect("login.aspx");
        }
        table = (DataTable)Session[id];
    }

    protected void TextBoxStart1_TextChanged(object sender, EventArgs e)
    {
        FillTechnicianDashboard(Convert.ToInt16(Session["TLID"].ToString()));
    }

    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtsearch.Text = "";
        lblMessage.Text = "";

    }

    protected void Save_Click(object sender, EventArgs e)
    {
        Modal.Close(this, "OK");
    }
    private DataTable DbGetEvents(DateTime start)
    {
        SqlConnection dbaseCon = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlCommand cmd = new SqlCommand("udpGetJobAllotment", dbaseCon);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Date", start);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        return dt;
    }
    protected void GetJobDesc(string JobCode,string VehicleNo,TextBox txtStdtime,TextBox txtJobDesc,TextBox txtAllottime)
    {
        SqlConnection dbaseCon = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        try
        {
            string ModelName1 = GetModelName(VehicleNo);
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("getJobDetailsfromJobCode", dbaseCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@JobCode ", JobCode);
            cmd.Parameters.AddWithValue("@model", ModelName1.Trim());
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            int HrhsInMinute = 0;
            if (dt.Rows.Count > 0)
            {
                string StandardAllottime = dt.Rows[0][0].ToString();
                txtJobDesc.Text = dt.Rows[0][1].ToString();
                if (StandardAllottime.Contains("."))
                {
                    string hrs = StandardAllottime.Split('.').GetValue(0).ToString();
                    if (hrs == "")
                        HrhsInMinute = ((Convert.ToInt32(StandardAllottime.Split('.').GetValue(1))) * 6);
                    else
                        HrhsInMinute = ((Convert.ToInt32(hrs)) * 60) + ((Convert.ToInt32(StandardAllottime.Split('.').GetValue(1))) * 6);
                    standartTime = HrhsInMinute;
                }
                else
                {
                    HrhsInMinute =
                    (Convert.ToInt32(StandardAllottime) * 60);
                    standartTime = HrhsInMinute;
                }
                txtAllottime.Text = HrhsInMinute.ToString();
                txtStdtime.Text = HrhsInMinute.ToString();

                stdTime = HrhsInMinute.ToString();
            }
            else
            {
                TextBoxAllot.Text = "";
                stdTime = "0";
            }
        }
        catch (Exception ex)
        {
        }
    }
  
    protected void GetJobCodes(DropDownList ddl,string VehicleNo,string EmpName)
    {
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        try
        {
            ddl.Items.Clear();
            ddl.Items.Add("JobCode");
            SqlCommand cmd = new SqlCommand("GetVehicleJobCodeList", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Regno", VehicleNo);
            cmd.Parameters.AddWithValue("@EmpName", EmpName);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count != 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ddl.Items.Add(new ListItem(dt.Rows[i][0].ToString(), dt.Rows[i][0].ToString()));
                }
            }

            ddl.Items.Add("Other");
           
        }
        catch (Exception ex)
        {
           
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }
    
   
    private int standartTime;
    protected void TextBoxjc_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (TextBoxjc.SelectedItem.Text == "Other")
        {
            txtJobcode1.Visible = true;
            TextBoxjc.Visible = false;
        }
        else
        {
            if (TextBoxjc.SelectedIndex != 0)
            {
                GetJobDesc(TextBoxjc.Text.ToString(), TextBoxVeh.Text.Trim(), TextBoxStd, txtjobDesc, TextBoxAllot);
            }
        }
    }
    protected string GetModelName(string Refno)
    {
        SqlConnection dbaseCon = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select Top 1 VehicleModel from tblMaster where regno='" + Refno + "' Order by GateIn Desc";
            cmd.Connection = dbaseCon;
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            if (dbaseCon.State != ConnectionState.Open)
                dbaseCon.Open();
            da.Fill(dt);
            if (dt.Rows.Count != 0)
            {
                return dt.Rows[0][0].ToString().Trim();
            }
            else
            {
                return "";
            }
        }
        catch (Exception ex)
        {
            return "";
        }
        finally
        {
            if (dbaseCon.State != ConnectionState.Closed)
                dbaseCon.Close();
        }
    }
    protected void JobCode_Click(object sender, EventArgs e)
    {
        GetJobCodes(TextBoxjc, TextBoxVeh.Text.ToString(), TextBoxName.Text.ToString());
    }

    protected void txtJobcode1_TextChanged(object sender, EventArgs e)
    {
        if(txtJobcode1.Text!="")
        {
        GetJobDesc(txtJobcode1.Text.ToString(),TextBoxVeh.Text.Trim(),TextBoxStd,txtjobDesc,TextBoxAllot);
        }
    }
    protected void btnJobCode_Click(object sender, EventArgs e)
    {
        GetJobCodes(drpJobcode2, TextBoxVeh.Text.ToString(), TextBoxName1.Text.ToString());
    }
    protected void drpJobcode2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpJobcode2.SelectedItem.Text == "Other")
        {
            drpJobcode2.Visible = false;
            TextBoxjc1.Visible = true;

        }
        else
        {
            if (drpJobcode2.SelectedIndex != 0)
            {
                GetJobDesc(drpJobcode2.Text.ToString(), TextBoxVeh.Text.Trim(), TextBoxStd1, txtjobdesc1, TextBoxAllot1);
            }
        }
    }
    protected void TextBoxjc1_TextChanged(object sender, EventArgs e)
    {
        if (txtJobcode1.Text != "")
        {
            GetJobDesc(txtJobcode1.Text.ToString(), TextBoxVeh.Text.Trim(), TextBoxStd1, txtjobdesc1, TextBoxAllot1);
        }
    }
    protected void btnJobCode1_Click(object sender, EventArgs e)
    {
        GetJobCodes(drpJobcode3, TextBoxVeh.Text.ToString(), TextBoxName2.Text.ToString());
    }
    protected void drpJobcode3_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpJobcode3.SelectedItem.Text == "Other")
        {
            TextBoxjc2.Visible = true;
            drpJobcode3.Visible = false;

        }
        else
        {
            if (drpJobcode3.SelectedIndex != 0)
            {
                GetJobDesc(drpJobcode3.Text.ToString(), TextBoxVeh.Text.Trim(), TextBoxStd2, txtjobdesc2, TextBoxAllot2);
            }
        }
    }

    protected void Reset_Click(object sender, EventArgs e)
    {
        TextBoxVeh.Text = string.Empty;
        TextBoxName.Text = string.Empty;
        TextBoxName1.Text = string.Empty;
        TextBoxName2.Text = string.Empty;
        TextBoxjc1.Text = string.Empty;
        TextBoxjc2.Text = string.Empty;
        TextBoxStd.Text = string.Empty;
        TextBoxStd1.Text = string.Empty;
        TextBoxStd2.Text = string.Empty;
        TextBoxAllot.Text = string.Empty;
        TextBoxAllot1.Text = string.Empty;
        TextBoxAllot2.Text = string.Empty;
        TextBox4.Text = string.Empty;
        TextBox3.Text = string.Empty;
        lblMessage.Text = string.Empty;
    }

    protected void btn_getVehicles_Click(object sender, EventArgs e)
    {
       
        if (TextBoxVeh.Text=="")
        {
            lblMessage.Text = "Please Select vehicle Number";
        }
        else
        {
            FillTechnicianDashboard(Convert.ToInt16(Session["TLID"].ToString()));

            try { 
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlCommand cmd = new SqlCommand("UdpGetAllotmentEmp", con);
        cmd.CommandType = CommandType.StoredProcedure;
        
       
        con.Open();
        cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {

        }

        }
    }
}
