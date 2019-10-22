using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JobCardOpen : System.Web.UI.Page
{
    private SqlConnection con = new SqlConnection(DataManager.ConStr);
    private SqlCommand cmd;
    private static string EmpId = "0";
    private int I;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
            try
            {
                if (Session["ROLE"] == null || Session["ROLE"].ToString() != "JOB SLIP")
                {
                    Response.Redirect("login.aspx");
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("login.aspx");
            }
       
        fillgrid();
        fillgridforClosing();
        if (!Page.IsPostBack)
        {
            fillgrid();
        }
        Session["CURRENT_PAGE"] = "Job Card Creation";
        this.Title = "Job Card Creation";
        try
        {
            if (!Page.IsPostBack)
            {
                FillVehicleNo(EmpId, ref ddevehno);
                FillVehicleNo(EmpId, ref drpvehicle);
                BindVehicleNo();
                fillRFID();
                fillgridforClosing();
                FillRemarksTemplate(4, ref ddlCancelationRemarks);

                if (Request.QueryString.Count > 0)
                {
                    txtTagNo.Text = Session["crdno"].ToString();
                    txtRegNo.Text = Request.QueryString[0].ToString().ToUpper();
                }
                if (!IsPostBack)
                {
                    GetWorkType();
                    txtDate.Text = DateTime.Now.Date.ToString("MM/dd/yyyy");
                    message.ForeColor = Color.Red;
                    message.Text = "";
                    lblMessage1.ForeColor = Color.Red;
                    lblMessage1.Text = "";
                    Label11.ForeColor = Color.Red;
                    Label11.Text = "";
                }
                else
                {
                    try
                    {
                        if (!(Request.Form["_EventTarget"] != null && Request.Form["_EventTarget"] == "myClick"))
                        {
                            grdjobcard.SelectedIndex = int.Parse(Request.Form["__EVENTARGUMENT"].ToString());
                            txtTagNo.Text = grdjobcard.SelectedRow.Cells[0].Text.Trim().ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        message.ForeColor = Color.Red;
                        message.Text = ex.Message.ToString();
                    }
                }
            }
        }
        catch (Exception ex) { }

    }
    private void GetWorkType()
    {
        cmbWorkType.Items.Clear();
        SqlConnection con = new SqlConnection(DataManager.ConStr);
        SqlCommand cmd = new SqlCommand("udpGetServiceType", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr = null;
        try
        {
            if (con.State != ConnectionState.Open)
                con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                cmbWorkType.Items.Add(dr["ServiceType"].ToString());
            }
            cmbWorkType.SelectedIndex = 0;
        }
        catch
        {
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }

    protected void chkevhno_CheckedChanged(object sender, EventArgs e)
    {
        if (chkevhno.Checked == true)
        {
            txtenewvhno.Enabled = true;
        }
        else
        {
            txtenewvhno.Text = "";
            txtenewvhno.Enabled = false;
        }
    }

    protected void btneupd_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddevehno.SelectedIndex != 0)
            {
                lblMessage1.ForeColor = Color.Red;
                lblMessage1.Text = "";

                if (txtenewvhno.Text.Trim() == string.Empty)
                {
                    lblMessage1.ForeColor = Color.Red;
                    lblMessage1.Text = "Empty New Registration No.";
                }
                else
                {
                    if (txtenewvhno.Text.Trim() != ddevehno.Text.Trim())
                    {
                        string str = "SELECT * FROM tblMaster Where RegNo='" + ddevehno.Text.Trim() + "' And Delivered=0 And Cancelation=0 ";
                        SqlCommand cmd = new SqlCommand(str, con);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            string str1 = "SELECT * FROM tblMaster Where RegNo='" + txtenewvhno.Text.Trim() + "' And Delivered=0 And Cancelation=0";
                            SqlCommand cmd1 = new SqlCommand(str1, con);
                            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                            DataTable dt1 = new DataTable();
                            da1.Fill(dt1);
                            if (dt1.Rows.Count == 0)
                            {
                                con.Open();
                                string str3 = "Update tblMaster set RegNo='" + txtenewvhno.Text.Trim() + "' where SlNo='" + dt.Rows[0][0].ToString() + "'";
                                SqlCommand cmd3 = new SqlCommand(str3, con);
                                cmd3.ExecuteNonQuery();
                                con.Close();
                                lblMessage1.ForeColor = Color.Green;
                                lblMessage1.Text = "Updated Successfully";
                                ddevehno.SelectedIndex = -1;
                                txtenewvhno.Text = "";
                                fillRFID();
                                FillVehicleNo(EmpId, ref ddevehno);
                                FillVehicleNo(EmpId, ref drpvehicle);
                                BindVehicleNo();
                                fillgrid();
                            }
                            else
                            {
                                lblMessage1.ForeColor = Color.Red;
                                lblMessage1.Text = "Registration No. Already In For Service ";
                            }
                        }
                        else
                        {
                            lblMessage1.ForeColor = Color.Red;
                            lblMessage1.Text = "Registration No. Not Exist Or There is no Vehicle under this SA..! ";
                        }
                    }
                    else
                    {
                        lblMessage1.ForeColor = Color.Red;
                        lblMessage1.Text = "Both Registration No. Same";
                    }
                }
            }
            else
            {
                lblMessage1.Text = "Please select vehicle number from list";
            }
        }
        catch (Exception ex)
        {
            lblMessage1.ForeColor = Color.Red;
            lblMessage1.Text = ex.Message;
        }
    }

    protected void BindVehicleNo()
    {
        SqlCommand cmdVehi = new SqlCommand("udpListVehicleDetailsforJC", con);
        cmdVehi.CommandType = CommandType.StoredProcedure;
        //cmdVehi.Parameters.AddWithValue("@EmpId", Session["EmpId"].ToString());
        SqlDataAdapter daVehi = new SqlDataAdapter(cmdVehi);
        DataTable dtVehi = new DataTable();
        daVehi.Fill(dtVehi);
        if (dtVehi.Rows.Count > 0)
        {
            ddlVehicle.Items.Clear();
            ddlVehicle.Items.Add(new ListItem("--Select--", "0"));
            ddlVehicle.DataSource = dtVehi;
            ddlVehicle.DataTextField = "RegNo";
            ddlVehicle.DataValueField = "RegNo";
            ddlVehicle.DataBind();
        }
    }

    protected void btnecncl_Click(object sender, EventArgs e)
    {
        txtenewvhno.Text = "";
        ddevehno.SelectedIndex = -1;
        chkevhno.Checked = false;
        lblMessage1.Text = "";
    }

    protected void ddlVehicle_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Label11.ForeColor = Color.Red;
            Label11.Text = "";
            //string EmpName = Session["EmpName"].ToString();
            if (ddlVehicle.SelectedIndex != 0)
            {
                string str = "SELECT * FROM tblMaster Where RegNo='" + ddlVehicle.SelectedValue.ToString() + "' And Delivered=0 And Cancelation=0";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    str = "SELECT TOP (1) CustomerName, Customerphone, Email FROM tblMaster Where Delivered=0 And Cancelation=0 And RegNo='" + ddlVehicle.SelectedValue.ToString() + "' ORDER BY GateIn DESC";
                    cmd = new SqlCommand(str, con);
                    da = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        txtCustName.Text = dt.Rows[0][0].ToString();
                        txtPhone.Text = dt.Rows[0][1].ToString();
                        txtemailid.Text = dt.Rows[0][2].ToString();
                    }
                }
                else
                {
                    Label11.ForeColor = Color.Red;
                    Label11.Text = "Vehicle Not Registered for Servicing ";
                    ddlVehicle.Focus();
                }
            }
            else
            {
                Label11.ForeColor = Color.Red;
                Label11.Text = "Please Select Vehicle No.!";
                ddlVehicle.Focus();
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void drpvehicle_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SqlCommand cmd1 = new SqlCommand("SearchVehicleRFIDSLNOforJC", con);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@VehicleNo", drpvehicle.Text.Trim());
            //cmd1.Parameters.AddWithValue("@EmpId", Session["EmpId"].ToString());
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                txtcardno.Text = dt1.Rows[0]["RFID"].ToString();
                txtnewcrdno.Text = "";
                lblref.Text = dt1.Rows[0]["Slno"].ToString();
            }
            else
            {
                txtcardno.Text = "";
                txtnewcrdno.Text = "";
                lblref.Text = "";
            }
        }
        catch (Exception ex)
        {
            message.ForeColor = Color.Red;
            message.Text = ex.Message;
        }
    }

    protected void btnSearchCardUpdation_Click(object sender, EventArgs e)
    {
        FillVehicleNo(EmpId, ref drpvehicle);
        txtcardno.Text = "";
        txtnewcrdno.Text = "";
        lblref.Text = "";
        drpvehicle.Focus();
    }

    protected void UpdateDataBind()
    {
        fillgrid();
        GetWorkType();
        fillRFID();
        FillVehicleNo(EmpId, ref ddevehno);
        FillVehicleNo(EmpId, ref drpvehicle);
        BindVehicleNo();
    }

    protected void btnupdate_Click(object sender, EventArgs e)
    {
        //string EmpName5 = Session["EmpName"].ToString();
        try
        {
            if (txtcardno.Text.Trim() != txtnewcrdno.Text.Trim() && txtcardno.Text.Trim() != "")
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                SqlCommand cmd1 = new SqlCommand("udpTagUpdationforJC", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@RefId", lblref.Text.Trim());
                cmd1.Parameters.AddWithValue("@TagNo", txtcardno.Text.Trim());
                cmd1.Parameters.AddWithValue("@NewTagNo", txtnewcrdno.Text.Trim());
                //cmd1.Parameters.AddWithValue("@EmpId", EmpId);
                SqlParameter flag = cmd1.Parameters.Add("@Flag", SqlDbType.VarChar, 75);
                flag.Direction = ParameterDirection.Output;
                flag.Value = "";
                cmd1.ExecuteNonQuery();
                Label4.ForeColor = Color.Green;
                Label4.Text = flag.Value.ToString();
                UpdateDataBind();
                txtcardno.Text = "";
                txtnewcrdno.Text = "";
            }
            else
            {
                Label4.ForeColor = Color.Red;
                Label4.Text = "Enter New Card No.";
                txtnewcrdno.Focus();
            }
        }
        catch (Exception ex)
        {
            Label4.ForeColor = Color.Red;
            Label4.Text = ex.Message;
        }
    }

    protected void btncncl_Click(object sender, EventArgs e)
    {
        txtcardno.Text = "";
        txtnewcrdno.Text = "";
        drpvehicle.SelectedIndex = -1;
        drpvehicle.Focus();
        Label4.Text = "";
    }

    private void FillRemarksTemplate(int Type, ref DropDownList ddl)
    {
        // 1-JCCRemarks ,2-PDTRemarks,3-Vehicle Cancelation ,4-Vehicle Tag Cancellation,5-Vehicle OUT,6-Service Remarks,7-Process Remarks
        try
        {
            SqlConnection con = new SqlConnection(DataManager.ConStr);
            SqlCommand cmd = new SqlCommand("GetRemarksTemplate", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RType", Type);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("--Select--", "0"));
            if (dt.Rows.Count > 0)
            {
                ddl.DataSource = dt;
                ddl.DataValueField = "SlNo";
                ddl.DataTextField = "RemarksTemplate";
                ddl.DataBind();
                txtCancelationRemark.Visible = false;
            }
            else
                txtCancelationRemark.Visible = true;
            ddl.Items.Add(new ListItem("Other", "-1"));
        }
        catch (Exception ex) { }
    }

    protected void btn_Searchcd_Click(object sender, ImageClickEventArgs e)
    {
        GetDetails();
        btnAdd.Text = "Update Job Card";
        txtRegNo.ReadOnly = true;
    }

    protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
    {
        message.Text = "";
        lblMessage1.Text = "";
        Label11.Text = "";
        Label4.Text = "";
        ErrorNote.Text = "";
    }

    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        cmbHH.Text = "";
    }

    private bool CheckFields()
    {
        if (txtTagNo.Text.Trim() != string.Empty && txtRegNo.Text.Trim() != string.Empty)//&&  txtRONumber.Text.Trim() != string.Empty && txtEstimate.Text.Trim() != string.Empty && txtKMS.Text.Trim() != string.Empty)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            try
            {
                DateTime dtt = DateTime.Parse(txtDate.Text.Trim().ToString());
            }
            catch
            {
                message.ForeColor = Color.Red;
                message.Text = "Please Select/Enter Promised Date Properly";
                return;
            }
            int flag = 0;
            int kmsout = 0;
            if (!CheckFields())
            {
                message.ForeColor = Color.Red;
                message.Text = "Please Enter The Mandatory Fields.";
            }

            else if (cmbWorkType.SelectedIndex < 1)
            {
                message.ForeColor = Color.Red;
                message.Text = "You Must Select Service Type.!";
            }
            else if (cmbHH.Text.Trim() == "")
            {
                message.ForeColor = Color.Red;
                message.Text = "You Must Enter PDT Time.!";
            }
            else if (txtTagNo.Text.Trim() != "")
            {
                if (btnAdd.Text == "Open Job Card")
                {
                    DateTime pt = Convert.ToDateTime(txtDate.Text.Trim() + " " + cmbHH.Text.Trim());
                    DateTime pt1 = Convert.ToDateTime("1/1/1900");
                    UpdateTblMasterJobSlipAdd(pt1, "", "", "", "", "", "", txtRegNo.Text.Trim().ToUpper(), txtTagNo.Text.Trim(), cmbWorkType.Text.Trim(), pt, chkCustomerWaiting.Checked ? 1 : 0, "", "", "", chkJDP.Checked ? 1 : 0, cbVas.Checked ? 1 : 0, chkWA.Checked ? 1 : 0, EmpId, chkWash.Checked ? 0 : 1, chkWashOnly.Checked ? 1 : 0);
                    if (I != 0)
                    {
                        UpdateDataBind();
                        fillgrid();
                        message.ForeColor = Color.Green;
                        message.Text = "Created Successfully.";
                        clear();
                    }
                }

                else
                {
                    DateTime pt = Convert.ToDateTime(txtDate.Text.Trim() + " " + cmbHH.Text.Trim());
                    DateTime pt1 = Convert.ToDateTime("1/1/1900");
                    UpdateTblMasterJobSlipAdd(pt1, "", "", "", "", "", "", txtRegNo.Text.Trim().ToUpper(), txtTagNo.Text.Trim(), cmbWorkType.Text.Trim(), pt, chkCustomerWaiting.Checked ? 1 : 0, "", "", "", chkJDP.Checked ? 1 : 0, cbVas.Checked ? 1 : 0, chkWA.Checked ? 1 : 0, EmpId, chkWash.Checked ? 0 : 1, chkWashOnly.Checked ? 1 : 0);
                    if (I != 0)
                    {
                        UpdateDataBind();
                        fillgrid();
                        message.ForeColor = Color.Green;
                        message.Text = "Updated Successfully.";
                        clear();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            message.ForeColor = Color.Red;
            message.Text = ex.Message;
        }
    }

    public int UpdateTblMasterJobSlipAdd(DateTime PT1, string dealername, string chassis, string engine, string email, string landline, string dealercode, string regno, string card, string ServiceType, DateTime PT, int CustomerWaiting, string Estimate, string KMS, string RONumber, int JDP, int VAS, int WA, string EmpId, int Wash, int WashOnly)
    {
        SqlConnection con = new SqlConnection();
        try
        {
            DataManager.OpenDBConnection(ref con);
            SqlCommand cmd = new SqlCommand("UpdateTblMasterJobSlipAddforJC", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@card", card);
            cmd.Parameters.AddWithValue("@JobCardInTime", DateTime.Now.ToString());
            cmd.Parameters.AddWithValue("@regno", regno.ToUpper());
            cmd.Parameters.AddWithValue("@LandlineNo", landline);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@ChassisNo", chassis);
            cmd.Parameters.AddWithValue("@EngineNo", engine);
            cmd.Parameters.AddWithValue("@DealerCode", dealercode);
            cmd.Parameters.AddWithValue("@DealerName", dealername);
            if (PT1.ToString().Trim() != "")
                cmd.Parameters.AddWithValue("@DateOfSale", PT1);
            else
                cmd.Parameters.AddWithValue("@DateOfSale", DateTime.Now.ToString());
            cmd.Parameters.AddWithValue("@servicetype", ServiceType);
            cmd.Parameters.AddWithValue("@pt", PT.ToString());
            cmd.Parameters.AddWithValue("@CustomerWaiting", CustomerWaiting);
            cmd.Parameters.AddWithValue("@KMS", KMS);
            cmd.Parameters.AddWithValue("@Estimate", Estimate);
            cmd.Parameters.AddWithValue("@RONumber", RONumber);
            cmd.Parameters.AddWithValue("@Aplus", JDP);
            cmd.Parameters.AddWithValue("@VAS", VAS);
            cmd.Parameters.AddWithValue("@WheelAlignment", WA);
            //cmd.Parameters.AddWithValue("@JCOCreatedBy", EmpId);
            cmd.Parameters.AddWithValue("@Wash", Wash);
            cmd.Parameters.AddWithValue("@WashOnly", WashOnly);
            cmd.Parameters.AddWithValue("@TeamLeadId", "");
            I = cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            message.Text = ex.Message;
        }
        finally
        {
            DataManager.CloseConnection(ref con);
        }
        return I;
    }

    protected void btnclr_Click(object sender, EventArgs e)
    {
        clear();
        message.Text = string.Empty;
    }

    protected void chkWash_CheckedChanged(object sender, EventArgs e)
    {
        if (chkWash.Checked)
        {
            if (chkWashOnly.Checked)
            {
                chkWashOnly.Checked = false;
            }
        }
    }

    protected void chkWashOnly_CheckedChanged(object sender, EventArgs e)
    {
        if (chkWashOnly.Checked)
        {
            chkWash.Checked = false;
            chkWA.Checked = false;
            cbVas.Checked = false;
        }
    }

    protected void cbVas_CheckedChanged(object sender, EventArgs e)
    {
        if (cbVas.Checked)
        {
            chkWashOnly.Checked = false;
        }
    }

    protected void chkWA_CheckedChanged(object sender, EventArgs e)
    {
        if (chkWA.Checked)
        {
            chkWashOnly.Checked = false;
        }
    }

    protected void grdjobcard_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdjobcard.PageIndex = e.NewPageIndex;
        fillgrid();
    }

    protected void grdjobcard_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[0].Text.Trim() != "")
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.textDecoration='underline';this.style.color='red'";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
                e.Row.Attributes.Add("OnClick", "Javascript:__doPostBack('myClick','" + e.Row.RowIndex + "');");
            }
            if (e.Row.Cells[4].Text.Trim() == "1")
            {
                e.Row.BackColor = Color.LightGreen;
            }
        }
    }

    protected void grdjobcard_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            clear();
            message.Text = string.Empty;
            txtTagNo.Text = grdjobcard.SelectedRow.Cells[1].Text.Trim();
            txtRegNo.Text = grdjobcard.SelectedRow.Cells[2].Text.Trim();
            txtRegNo.ReadOnly = true;
            GetDetails();
        }
        catch (Exception ex)
        {
            message.ForeColor = Color.Red;
            message.Text = ex.Message.ToString();
        }
    }

    private void GetDetails()
    {
        try
        {
            if (txtRegNo.Text.Trim() != "")
            {
                SqlConnection con = new SqlConnection(DataManager.ConStr);
                SqlCommand cmd = new SqlCommand("", con);
                cmd.CommandText = "udpSearchVehicleDetailsforJC";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@VehicleNo", txtRegNo.Text.Trim().ToUpper());
                //cmd.Parameters.AddWithValue("@EmpId", EmpId);
                SqlParameter flag = cmd.Parameters.Add("@Flag", SqlDbType.Int);
                flag.Direction = ParameterDirection.Output;
                flag.Value = 0;
                SqlDataAdapter da1 = new SqlDataAdapter(cmd);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    txtRegNo.Text = txtRegNo.Text.Trim();
                    txtTagNo.Text = dt1.Rows[0]["RFId"].ToString();
                    chkCustomerWaiting.Checked = (int)dt1.Rows[0]["CustomerWaiting"] == 1 ? true : false;
                    chkJDP.Checked = (int)dt1.Rows[0]["Aplus"] == 1 ? true : false;
                    if (String.IsNullOrEmpty(dt1.Rows[0]["VAS"].ToString()))
                    {
                        cbVas.Checked = false;
                    }
                    else
                    {
                        cbVas.Checked = (int)dt1.Rows[0]["VAS"] == 1 ? true : false;
                    }

                    if (String.IsNullOrEmpty(dt1.Rows[0]["WheelAlignment"].ToString()))
                    {
                        chkWA.Checked = false;
                    }
                    else
                    {
                        chkWA.Checked = (bool)dt1.Rows[0]["WheelAlignment"];
                    }

                    if (String.IsNullOrEmpty(dt1.Rows[0]["Wash"].ToString()) || String.IsNullOrEmpty(dt1.Rows[0]["WashOnly"].ToString()))
                    {
                        chkWash.Checked = false;
                        chkWashOnly.Checked = false;
                    }
                    else
                    {
                        if (dt1.Rows[0]["Wash"].ToString() == "False")
                        {
                            if (dt1.Rows[0]["WashOnly"].ToString() == "False")
                            {
                                chkWash.Checked = true;
                                chkWashOnly.Checked = false;
                            }
                        }
                        else
                        {
                            if (dt1.Rows[0]["WashOnly"].ToString() == "False")
                            {
                                chkWash.Checked = false;
                                chkWashOnly.Checked = false;
                            }
                            else
                            {
                                chkWash.Checked = false;
                                chkWashOnly.Checked = true;
                            }
                        }
                    }
                    txtModel.Text = dt1.Rows[0]["VehicleModel"].ToString();
                    txtLastST.Text = dt1.Rows[0]["LastServiceType"].ToString();
                    if (String.IsNullOrEmpty(dt1.Rows[0]["ServiceType"].ToString()))
                    {
                        cmbWorkType.SelectedIndex = 0;
                    }
                    else
                    {
                        cmbWorkType.Text = dt1.Rows[0]["ServiceType"].ToString();
                    }

                    lblCustName.Text = dt1.Rows[0]["CustomerName"].ToString();
                    txtPhoneNo.Text = dt1.Rows[0]["Customerphone"].ToString();
                    txtLastGateDate.Text = dt1.Rows[0]["lastVisitingdate"].ToString();
                    if (String.IsNullOrEmpty(dt1.Rows[0]["PromisedTime"].ToString()))
                    {
                        txtDate.Text = DateTime.Now.Date.ToString("MM/dd/yyyy");
                    }
                    else
                    {
                        DateTime dt2 = (DateTime)dt1.Rows[0]["PromisedTime"];
                        txtDate.Text = dt2.ToShortDateString();
                        cmbHH.Text = dt2.ToString("HH:mm");
                    }
                }
                else
                {
                    message.ForeColor = Color.Red;
                    message.Text = "No Record Found For This Vehicle No.!";
                    btnAdd.Text = "Open Job Card";
                }
            }
            else
            {
                message.ForeColor = Color.Red;
                message.Text = "Please Enter a Vehicle No.";
            }
        }
        catch (Exception ex)
        {
            message.ForeColor = Color.Red;
            message.Text = ex.Message;
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }

    private void clear()
    {
        txtRegNo.Text = string.Empty;
        txtTagNo.ReadOnly = false;
        txtTagNo.Text = string.Empty;
        txtTagNo.ReadOnly = true;
        chkCustomerWaiting.Checked = false;
        cbVas.Checked = false;
        chkJDP.Checked = false;
        chkWA.Checked = false;
        chkWash.Checked = false;
        chkWashOnly.Checked = false;
        GetWorkType();
        txtDate.Text = DateTime.Now.Date.ToString("MM/dd/yyyy");
        cmbHH.Text = "";
        btnAdd.Text = "Open Job Card";
        txtSrchTag.Text = "";
        txtRegNo.ReadOnly = false;
        txtLastGateDate.Text = "";
        txtLastST.Text = "";
        txtModel.Text = "";
        txtPhoneNo.Text = "";
        lblCustName.Text = "";
    }

    protected void btnCloseJCC_Click(object sender, EventArgs e)
    {
        try
        {
            int informed = 2;

            if (RadioCustomerInformed.SelectedIndex != -1)
            {
                informed = Convert.ToInt32(RadioCustomerInformed.SelectedValue);
            }
            SqlConnection con = new SqlConnection();
            DataManager.OpenDBConnection(ref con);
            SqlCommand cmd = new SqlCommand("udpJobCardClosingforJC", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@VehicleNo", txtRegNoJCC.Text.ToString());
            cmd.Parameters.AddWithValue("@Informed", informed.ToString());
           //cmd.Parameters.AddWithValue("@JCCInFoBy", Session["EmpName"].ToString());
            SqlParameter msg = cmd.Parameters.Add("@msg", SqlDbType.VarChar, 100);
            SqlParameter Flag = cmd.Parameters.Add("@flag", SqlDbType.VarChar, 100);
            Flag.Direction = ParameterDirection.Output;
            Flag.Value = "";
            msg.Direction = ParameterDirection.Output;
            msg.Value = 0;
            cmd.ExecuteNonQuery();
            fillgridforClosing();
            ErrorNote.ForeColor = Color.Green;
            ErrorNote.Text = "Job Card Closed Succefully.";
            txtRegNoJCC.Text = "";
            RadioCustomerInformed.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ErrorNote.ForeColor = Color.Red;
            ErrorNote.Text = ex.Message.ToString();
        }
    }

    protected void btnRefreshJCC_Click(object sender, EventArgs e)
    {
        txtRegNoJCC.Text = "";
        RadioCustomerInformed.SelectedIndex = 0;
        ErrorNote.Text = "";
    }

    protected void btnSrchTag_Click(object sender, ImageClickEventArgs e)
    {
        errSrchTag.ForeColor = Color.Red;
        if (txtSrchTag.Text.Trim() == "" || txtSrchTag.Text.Trim() == null)
        {
            errSrchTag.Text = "Enter The Tag No.!";
            txtSrchTag.Focus();
        }
        else
        {
            int i;
            for (i = 0; i < SrchDt.Rows.Count; i++)
            {
                if (SrchDt.Rows[i]["Tag No"].ToString() == txtSrchTag.Text.Trim().ToString())
                    break;
            }
            if (i == SrchDt.Rows.Count)
            {
                clear();
                errSrchTag.Text = "Wrong Tag No.!";
            }
            else
            {
                String str = txtSrchTag.Text.Trim();
                string tempSearch = txtSrchTag.Text.ToString();
                clear();
                errSrchTag.ForeColor = Color.Green;
                errSrchTag.Text = "Success.!";
                txtTagNo.Text = str;
                for (i = 0; i < SrchDt.Rows.Count; i++)
                {
                    if (SrchDt.Rows[i]["Tag No"].ToString() == tempSearch)
                    {
                        txtRegNo.Text = SrchDt.Rows[i]["RegNo"].ToString();
                    }
                }
            }
            GetDetails();
            message.Text = "";
        }
    }

    protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
    {
        fillgrid();
    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {
        fillgrid();
        fillgridforClosing();
    }

    private static DataTable SrchDt;

    private void fillgrid()
    {
        try
        {
            grdjobcard.DataSource = null;
            SqlCommand cmd = new SqlCommand("GetPendingJobCardsforJC", con);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@EmpId", EmpId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            SrchDt = dt;
            if (dt.Rows.Count != 0)
            {
                grdjobcard.DataSource = dt;
                grdjobcard.DataBind();
            }
            else
            {
                grdjobcard.DataSource = null;
                grdjobcard.DataBind();
            }
            if (grdjobcard.Rows.Count == 0)
            {
                grdjobcard.DataSource = null;
                grdjobcard.DataBind();
            }
            else
            {
                txtTagNo.ReadOnly = true;
            }
            grdjobcard.Columns[4].Visible = false;
        }
        catch (Exception ex)
        {
            message.ForeColor = Color.Red;
            message.Text = ex.Message.ToString();
        }
    }

    protected void fillRFID()
    {
        SqlConnection con = new SqlConnection(DataManager.ConStr);
        SqlCommand cmd = new SqlCommand("getTagNosforJC", con);
        cmd.CommandType = CommandType.StoredProcedure;
       // cmd.Parameters.Add("@EmpId", Session["EmpId"].ToString().Trim());
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        ddlRFID.Items.Clear();
        ddlRFID.Items.Add(new ListItem("--Select--", "0"));
        if (dt.Rows.Count != 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ddlRFID.Items.Add(new ListItem(dt.Rows[i][0].ToString(), dt.Rows[i][0].ToString()));
            }
        }
    }

    private void FillVehicleNo(string EmpId, ref DropDownList ddl)
    {
        try
        {
            SqlConnection con = new SqlConnection(DataManager.ConStr);
            SqlCommand cmd = new SqlCommand("udpListVehicleDetailsforJC", con);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@EmpId", EmpId);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("--Select--", "-1"));
            if (dt.Rows.Count > 0)
            {
                ddl.DataSource = dt;
                ddl.DataTextField = "RegNo";
                ddl.DataBind();
            }
        }
        catch (Exception ex) { }
    }

    private void fillgridforClosing()
    {
        try
        {
            grdjobcardclose.DataSource = null;
            SqlCommand cmd = new SqlCommand("GetPendingJobCardsForClosingforJC", con);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@EmpId", EmpId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            //SrchDt = dt;
            if (dt.Rows.Count != 0)
            {
                grdjobcardclose.DataSource = dt;
                grdjobcardclose.DataBind();
            }
            else
            {
                grdjobcardclose.DataSource = null;
                grdjobcardclose.DataBind();
            }
            if (grdjobcardclose.Rows.Count == 0)
            {
                grdjobcardclose.DataSource = null;
                grdjobcardclose.DataBind();
            }
            grdjobcardclose.Columns[1].Visible = false;
        }
        catch (Exception ex)
        {
            message.ForeColor = Color.Red;
            message.Text = ex.Message.ToString();
        }
    }

    protected void Refresh_Click(object sender, ImageClickEventArgs e)
    {
        BindVehicleNo();
        txtCustName.Text = "";
        txtPhone.Text = "";
        txtemailid.Text = "";
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
       // string EmpName1 = Session["EmpName"].ToString();
        try
        {
            Label11.ForeColor = Color.Red;
            Label11.Text = "";
            if (ddlVehicle.SelectedIndex == 0)
            {
                Label11.ForeColor = Color.Red;
                Label11.Text = "Please Select Vehicle No.";
                ddlVehicle.Focus();
            }

            else
            {
                string str = "SELECT Slno FROM tblMaster Where RegNo='" + ddlVehicle.SelectedValue.ToString() + "' And Delivered=0 And Cancelation=0";
                cmd = new SqlCommand(str, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    cmd = new SqlCommand("", con);
                    cmd.CommandText = "UpdateTblMasterCustomerDetails";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@CustomerName", txtCustName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Customerphone", txtPhone.Text.Trim());

                    cmd.Parameters.AddWithValue("@Email", txtemailid.Text.Trim());
                    cmd.Parameters.AddWithValue("@SlNo", dt.Rows[0]["Slno"].ToString());
                    try
                    {
                        if (con.State != ConnectionState.Open)
                            con.Open();

                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            Label11.ForeColor = Color.Green;
                            Label11.Text = "Updated Successfully";
                            UpdateDataBind();
                        }
                        else
                        {
                            Label11.ForeColor = Color.Red;
                            Label11.Text = "Update Aborted, Please Try Again.";
                        }
                    }

                    catch (Exception ex)
                    {
                        Label11.ForeColor = Color.Red;
                        Label11.Text = ex.Message;
                    }
                    finally
                    {
                        if (con.State != ConnectionState.Closed)
                            con.Close();
                    }
                    ddlVehicle.SelectedIndex = -1;
                    txtCustName.Text = "";

                    txtPhone.Text = "";
                    txtemailid.Text = "";
                }
                else
                {
                    Label11.ForeColor = Color.Red;
                    Label11.Text = "Vehicle Not Registered for Servicing ";
                    ddlVehicle.Focus();
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        ddlVehicle.SelectedIndex = -1;
        txtCustName.Text = "";
        txtPhone.Text = "";
        txtemailid.Text = "";
        Label11.Text = "";
    }

    protected void btnUpdateRFIDCancel_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlRFID.SelectedValue.ToString() == "0")
            {
                lblTagCancellationMsg.ForeColor = Color.Red;
                lblTagCancellationMsg.Text = "Please Select Tagno.";
            }
            else if (ddlCancelationRemarks.SelectedIndex == 0)
            {
                lblTagCancellationMsg.ForeColor = Color.Red;
                lblTagCancellationMsg.Text = "Please Add Remarks.";
            }
            else
            {
                string Remarks = ((ddlCancelationRemarks.SelectedValue.Trim() == "-1") ? txtCancelationRemark.Text.Trim() : ddlCancelationRemarks.SelectedItem.Text);
                lblTagCancellationMsg.Text = "";
                if (con.State != ConnectionState.Open)
                    con.Open();
                string str = "udpTagCancelation";
                SqlCommand cmd = new SqlCommand(str, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@TagNo", ddlRFID.SelectedItem.Text.Trim().ToString());
                cmd.Parameters.Add("@EmpId", EmpId);
                cmd.Parameters.Add("@Reason", Remarks);
                SqlParameter Flag = cmd.Parameters.Add("@Flag", SqlDbType.VarChar, 100);
                Flag.Direction = ParameterDirection.Output;
                Flag.Value = "";
                cmd.ExecuteNonQuery();
                lblTagCancellationMsg.ForeColor = Color.Green;
                lblTagCancellationMsg.Text = Flag.Value.ToString();
                fillRFID();
                fillgrid();
                ddlRFID.SelectedIndex = 0;
                ddlCancelationRemarks.SelectedIndex = 0;
                txtCancelationRemark.Visible = false;
            }
        }
        catch (Exception ex)
        { }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }

    protected void btncncel_Click(object sender, EventArgs e)
    {
        ddlRFID.SelectedIndex = 0;
        lblTagCancellationMsg.Text = "";
    }

    protected void drpvehiclno_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string str1 = "Select RFID from tblMaster where RegNo='" + ddlRFID.SelectedValue + "' ORDER BY Slno DESC";
            SqlCommand cmd1 = new SqlCommand(str1, con);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            if (dt1.Rows.Count != 0)
            {
                lblTagCancellationMsg.Text = dt1.Rows[0]["RFID"].ToString();
            }
        }
        catch (Exception ex)
        {
            lblTagCancellationMsg.ForeColor = Color.Red;
            lblTagCancellationMsg.Text = ex.Message.ToString();
        }
    }

    protected void ddlCancelationRemarks_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCancelationRemarks.SelectedValue == "-1")
        {
            txtCancelationRemark.Visible = true;
        }
        else
        {
            txtCancelationRemark.Visible = false;
        }
    }

    protected void grdjobcardclose_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdjobcardclose.PageIndex = e.NewPageIndex;
        fillgridforClosing();
    }

    protected void grdjobcardclose_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtRegNoJCC.Text = grdjobcardclose.SelectedRow.Cells[2].Text.Trim();
    }
}