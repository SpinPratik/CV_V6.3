using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Text;
using System.IO;
using System.Configuration;

public partial class BodyshopFO : System.Web.UI.Page
{
    // private SqlConnection con = new SqlConnection(DataManager.ConStr);
    private SqlCommand cmd = new SqlCommand();
    private static string EmpId = "0";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["ROLE"] == null || Session["ROLE"].ToString() != "FRONT OFFICE" || Session[Session["TMLDealercode"] + "-TMLConString"].ToString() == null)
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                UpdateAll();
                FillModel();
                BindVehicleNo();
            }
            Session["CURRENT_PAGE"] = "Tag Allotment";
            this.Title = "Tag Allotment";
        }
        catch (Exception ex)
        {
            Response.Redirect("login.aspx");
        }
        try
        {
            EmpId = Session["EmpId"].ToString();
        }
        catch (Exception ex)
        {
            Response.Redirect("login.aspx");
        }
    }

    private void UpdateAll()
    {
        fillSAGrid();
        fillRFIDTags();
        fillRFIDSA();
        fillSAList();
        FillRemarksTemplate(4, ref CmbCancelationRemarks);
    }

    protected void grdSAAssign_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdSAAssign.PageIndex = e.NewPageIndex;

        fillRFIDSA();
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtRFID.Enabled = true;
        txtRFID.Text = "";
        txtRegNO.Text = "";
        cmbSAList.SelectedIndex = -1;
        btnMap.Text = "Assign";
        cmbSAList.Enabled = true;
    }

    private void FillModel()
    {
        try
        {
            SqlConnection con = new SqlConnection(DataManager.ConStr);
            SqlCommand cmd = new SqlCommand("UdpGetVehicleModel", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            drpModel.Items.Clear();
            drpModel.Items.Add(new ListItem("Model", "0"));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                    drpModel.Items.Add(new ListItem(dt.Rows[i][0].ToString(), dt.Rows[i][0].ToString()));
            }
            else
            {
                drpModel.DataSource = null;
            }
        }
        catch (Exception ex) { }
    }

    protected void grdSAAssign_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtRFID.ReadOnly = true;
            message.Text = string.Empty;
            txtRFID.Text = grdSAAssign.SelectedRow.Cells[1].Text.Trim();
            btnSave.Visible = false;
            btnCancel.Visible = false;
            btnMap.Text = "Assign";
            lblSlno.Text = grdSAAssign.SelectedRow.Cells[3].Text.Trim();
        }
        catch (Exception ex)
        {
            message.Text = ex.Message.ToString();
        }
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        fillRFIDSA();
        fillSAGrid();
        message.Text = "";
        errSrchTag.Text = "";
        lblMsg.Text = "";
        txtSrchTag.Text = "";
        txtRFID.ReadOnly = true;
        txtRFID.Text = "";
        txtRegNO.Text = "";
        cmbSAList.SelectedIndex = 0;
        chkAppointment.Checked = true;
        chkWalkIn.Checked = false;
        chk_WhiteBoard.Checked = true;
        chk_Yellow.Checked = false;
        lblMsg.Text = "";
        drpModel.SelectedIndex = -1;
        txtCustomer.Text = "";
        txtPhone1.Text = "";
        cmbSAList.Enabled = true;
        txtPreviousSA.Text = "";
        txtServiceType.Text = "";
        txtVisitingDate.Text = "";
        lblSlno.Text = "";
        btnSave.Visible = false;
        btnCancel.Visible = false;
    }
    public void SendSMS(string VehNo, string SAName, string CustomerName)
    {
        string PhoneNo = GetPhoneNo(SAName);
        string Message = "Mr." + SAName + ", A New Vehicle alloted to you. Vehicle Number : " + VehNo + " ,Customer Name : " + CustomerName;
        HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("http://login.wishbysms.com/api/sendhttp.php?authkey=78415A5xUuR7kyqi55e6b2f4&mobiles=" + PhoneNo + "&message=" + Message + " &sender=CONCRD&route=4&country=0 ");

        HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();

        System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());

        string responseString = respStreamReader.ReadToEnd();

        respStreamReader.Close();

        myResp.Close();

    }
    private string GetPhoneNo(string SAName)
    {
        string PhoneNo = "";
        try
        {
            SqlConnection con = new SqlConnection(DataManager.ConStr);
            SqlCommand cmd = new SqlCommand("", con);
            cmd.CommandText = "udpGetSAPhoneNo";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@SAName", SAName);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                PhoneNo = dt.Rows[0]["PhoneNo"].ToString();
            }
        }
        catch (Exception ex)
        {

        }
        return PhoneNo;
    }
    protected void btnSrchTag_Click(object sender, ImageClickEventArgs e)
    {
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        if (txtSrchTag.Text.Trim() == "" || txtSrchTag.Text.Trim() == null)
        {
            errSrchTag.Text = "Enter The Tag No.!";
            txtSrchTag.Focus();
        }
        else
        {
            try
            {
                string str = "udpSearchTagNoForSATagMapping";
                SqlCommand cmd = new SqlCommand(str, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RFID", txtSrchTag.Text.Trim().ToString());
                SqlParameter param1 = new SqlParameter();
                SqlParameter param2 = new SqlParameter();
                param1 = new SqlParameter("@flag", SqlDbType.Int);
                param1.Direction = ParameterDirection.Output;
                param2 = new SqlParameter("@msg", SqlDbType.NVarChar, 300);
                param2.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(param1);
                cmd.Parameters.Add(param2);
                if (con.State != ConnectionState.Open)
                    con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                switch ((int)param1.Value)
                {
                    case 0:
                        errSrchTag.Text = (string)param2.Value;
                        message.Text = "";
                        break;

                    case 1:
                        errSrchTag.Text = (string)param2.Value;
                        message.Text = "";
                        txtRegNO.Text = dt.Rows[0][1].ToString();
                        txtRFID.Text = dt.Rows[0][0].ToString();
                        cmbSAList.SelectedValue = dt.Rows[0][2].ToString();
                        drpModel.Text = dt.Rows[0]["VehicleModel"].ToString();
                        txtCustomer.Text = dt.Rows[0]["CustomerName"].ToString();
                        txtPhone1.Text = dt.Rows[0]["Customerphone"].ToString();
                        txtServiceType.Text = dt.Rows[0]["LastServiceType"].ToString();
                        txtVisitingDate.Text = dt.Rows[0]["LastVisitingDate"].ToString();
                        txtPreviousSA.Text = dt.Rows[0]["LastServiceAdvisor"].ToString();
                        if (dt.Rows[0]["IsAppointment"].ToString() == "True")
                        {
                            chkWalkIn.Checked = false;
                            chkAppointment.Checked = true;
                        }
                        else if (dt.Rows[0]["IsAppointment"].ToString() == "False")
                        {
                            chkAppointment.Checked = false;
                            chkWalkIn.Checked = true;
                        }
                        if (dt.Rows[0]["IsWhiteboard"].ToString() == "1")
                        {
                            chk_WhiteBoard.Checked = true;
                            chk_Yellow.Checked = false;
                        }
                        else if (dt.Rows[0]["IsWhiteboard"].ToString() == "0" || dt.Rows[0]["IsWhiteboard"].ToString() == " ")
                        {
                            chk_WhiteBoard.Checked = false;
                            chk_Yellow.Checked = true;
                        }
                        btnMap.Text = "Update";
                        break;

                    case 2:
                        errSrchTag.Text = (string)param2.Value;
                        message.Text = "";
                        txtRFID.Text = dt.Rows[0][0].ToString();
                        btnMap.Text = "Update";
                        break;
                }
            }
            catch (Exception ex)
            {
            }
        }
    }

    protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
    {
        lblSlno.Text = "";
        txtRFID.Text = "";
        txtRegNO.Text = "";
        drpModel.SelectedIndex = -1;
        txtCustomer.Text = "";
        txtPhone1.Text = "";
        txtServiceType.Text = "";
        txtVisitingDate.Text = "";
        txtPreviousSA.Text = "";
        cmbSAList.SelectedIndex = -1;
        chkWalkIn.Checked = false;
        ddlVehicle.SelectedIndex = -1;
        txtCustName.Text = "";
        txtPhone.Text = "";
        txtEmail.Text = "";
        message.Text = "";
        lblMsg.Text = "";
        lblTagCancellationMsg.Text = "";
        lblEditCustMsg.Text = "";
        errSrchTag.Text = "";
    }

    protected void fillRFIDTags()
    {
        try
        {
            string RFID;
            SqlConnection con = new SqlConnection(DataManager.ConStr);
            SqlCommand cmd = new SqlCommand("udpTagCancelationFOList", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            cmbRFIDTagCancle.Items.Clear();
            cmbRFIDTagCancle.Items.Add(new ListItem("--Select--", "0"));
            if (dt.Rows.Count != 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (String.IsNullOrEmpty(dt.Rows[i]["ServiceAdvisor"].ToString()))
                    {
                        RFID = dt.Rows[i][0].ToString();
                    }
                    else
                    {
                        RFID = dt.Rows[i][0].ToString() + "-" + dt.Rows[i][1].ToString();
                    }
                    cmbRFIDTagCancle.Items.Add(new ListItem(RFID, dt.Rows[i][0].ToString()));
                }
            }
        }
        catch (Exception ex)
        { }
    }

    private static DataTable SrchDt;

    protected void fillRFIDSA()
    {
        try
        {
            SqlConnection con = new SqlConnection(DataManager.ConStr);
            SqlCommand cmd = new SqlCommand("udpTagListForSAMapping", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            SrchDt = dt;
            if (dt.Rows.Count != 0)
            {
                grdSAAssign.DataSource = dt;
                grdSAAssign.DataBind();
            }
            else
            {
                grdSAAssign.DataSource = null;
                grdSAAssign.DataBind();
            }
            grdSAAssign.Columns[3].Visible = false;
        }
        catch (Exception ex) { }
        finally { }
    }

    protected void fillSAGrid()
    {
        try
        {
            SqlConnection con = new SqlConnection(DataManager.ConStr);
            SqlCommand cmd = new SqlCommand("udpSAAllotmentDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count != 0)
            {
                grdSAMapping.DataSource = dt;
                grdSAMapping.DataBind();
            }
            else
            {
                grdSAMapping.DataSource = null;
                grdSAMapping.DataBind();
            }
        }
        catch (Exception ex) { }
        finally { }
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        if (IsVehicleCard(txtRFID.Text.Trim()))
        {
            SqlCommand cmd = new SqlCommand("", con);
            cmd.CommandText = "Getprocaddcard";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@cardno", txtRFID.Text.Trim());
            SqlParameter Remarks = cmd.Parameters.Add("@Remarks", SqlDbType.VarChar, 100);
            Remarks.Direction = ParameterDirection.Output;
            Remarks.Value = "";
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
                btnCancel.Visible = false;
                btnSave.Visible = false;
                txtRFID.Text = "";
                txtRFID.ReadOnly = true;
                message.ForeColor = Color.Green;
                UpdateAll();
            }
            catch (Exception ex)
            {
                message.ForeColor = Color.Red;
                message.Text = ex.Message;
            }
            finally
            {
                message.ForeColor = Color.Green;
                message.Text = Remarks.Value.ToString();
            }
        }
        else
        {
            message.ForeColor = Color.Red;
            message.Text = "Card is NOT a Vehicle Card or NOT Registered.!";
            txtRFID.Focus();
        }
    }

    protected bool IsVehicleCard(String TagNo)
    {
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        try
        {
            SqlCommand cmd = new SqlCommand("Select EnrollmentNo from tblRFID where EnrollmentNo=@EnrollmentNo And Reserved=0", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@EnrollmentNo", TagNo);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }
        catch (Exception ex)
        {
            return false;
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }

    protected void fillSAList()
    {
        SqlConnection con = new SqlConnection(DataManager.ConStr);
        SqlCommand cmd = new SqlCommand("udpSAlIST", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        cmbSAList.Items.Clear();
        cmbSAList.Items.Add(new ListItem("--Select--", "0"));
        if (dt.Rows.Count != 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbSAList.Items.Add(new ListItem(dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString()));
            }
        }
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

    protected void btnUpdateRFIDCancel_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        try
        {
            if (cmbRFIDTagCancle.SelectedValue.ToString() == "0")
            {
                lblTagCancellationMsg.ForeColor = Color.Red;
                lblTagCancellationMsg.Text = "Please Select Tagno.";
            }
            else if (CmbCancelationRemarks.SelectedIndex == 0)
            {
                lblTagCancellationMsg.ForeColor = Color.Red;
                lblTagCancellationMsg.Text = "Please Add Remarks.";
            }
            else
            {
                string Remarks = ((CmbCancelationRemarks.SelectedValue.Trim() == "-1") ? txtCancelationRemark.Text.Trim() : CmbCancelationRemarks.SelectedItem.Text);
                lblTagCancellationMsg.Text = "";
                if (con.State != ConnectionState.Open)
                    con.Open();

                string str = "udpTagCancelation";
                SqlCommand cmd = new SqlCommand(str, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@TagNo", cmbRFIDTagCancle.SelectedValue.ToString());
                cmd.Parameters.Add("@EmpId", EmpId);
                cmd.Parameters.Add("@Reason", Remarks);
                SqlParameter Flag = cmd.Parameters.Add("@Flag", SqlDbType.VarChar, 100);
                Flag.Direction = ParameterDirection.Output;
                Flag.Value = "";
                cmd.ExecuteNonQuery();
                lblTagCancellationMsg.ForeColor = Color.Green;
                lblTagCancellationMsg.Text = Flag.Value.ToString();
                UpdateAll();
                cmbRFIDTagCancle.SelectedIndex = 0;
                txtCancelationRemark.Visible = false;
                CmbCancelationRemarks.SelectedIndex = 0;
                txtCancelationRemark.Text = "";
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
        cmbRFIDTagCancle.SelectedIndex = 0;
        lblTagCancellationMsg.Text = "";
        txtCancelationRemark.Text = "";
        cmbRFIDTagCancle.SelectedIndex = 0;
        txtCancelationRemark.Visible = false;
        FillRemarksTemplate(4, ref CmbCancelationRemarks);
    }

    protected void btnJobController_Click(object sender, EventArgs e)
    {
        Response.Redirect("JobCardCreation.aspx");
    }

    protected void btnDisplay_Click(object sender, EventArgs e)
    {
        Response.Redirect("Display.aspx");
    }

    protected void btn_logout_Click(object sender, EventArgs e)
    {
        Response.Redirect("login.aspx");
    }

    protected void btnJCDisplay_Click(object sender, EventArgs e)
    {
        Response.Redirect("DisplayWorks.aspx");
    }

    protected void btn_Back_Click(object sender, EventArgs e)
    {
        Response.Redirect("FrontOfficeHome.aspx");
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        int Appointment = 0;
        int IsWhiteBoard = 1;

        if (chkAppointment.Checked == true)
        {
            Appointment = 1;
        }
        else
        {
            Appointment = 0;
        }
        if (chk_Yellow.Checked == true)
        {
            IsWhiteBoard = 0;
        }
        else
        {
            IsWhiteBoard = 1;
        }
        if (txtRFID.Text == "")
        {
            message.ForeColor = Color.Red;
            lblMsg.Text = "Select RFID NO.";
        }
        else if (txtRegNO.Text == "")
        {
            message.ForeColor = Color.Red;
            lblMsg.Text = "Enter Vehicle RegNo.";
        }
        else if (cmbSAList.SelectedIndex == 0)
        {
            message.ForeColor = Color.Red;
            lblMsg.Text = "Select SA Name";
        }
        else if (drpModel.SelectedIndex < 1 || drpModel.SelectedValue.Trim() == "0")
        {
            message.ForeColor = Color.Red;
            message.Text = "You Must Select Model.!";
        }
        else
        {
            try
            {
                int flag = 0;
                string msg = "";
                string str = "udpSATagMapping";
                SqlCommand cmd = new SqlCommand(str, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpId", cmbSAList.SelectedValue.ToString().Trim());
                cmd.Parameters.AddWithValue("@RFID", txtRFID.Text.ToString().Trim());
                cmd.Parameters.AddWithValue("@RegNo", txtRegNO.Text.ToString().Trim());
                cmd.Parameters.AddWithValue("@VehicleModel", drpModel.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@CustomerName", txtCustomer.Text.Trim());
                cmd.Parameters.AddWithValue("@Customerphone", txtPhone1.Text.Trim());
                cmd.Parameters.AddWithValue("@Appointment", Appointment);
                cmd.Parameters.AddWithValue("@IsWhiteBoard", IsWhiteBoard);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                SqlParameter param1 = new SqlParameter();
                SqlParameter param2 = new SqlParameter();
                param1 = new SqlParameter("@flag", SqlDbType.Int);
                param1.Direction = ParameterDirection.Output;
                param2 = new SqlParameter("@msg", SqlDbType.NVarChar, 300);
                param2.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param1);
                cmd.Parameters.Add(param2);
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd.ExecuteNonQuery();
                if ((int)param1.Value == 1)
                {
                    lblMsg.ForeColor = Color.Green;
                    lblMsg.Text = (string)param2.Value;
                    try
                    {
                        // SendSMS(txtRegNO.Text.ToString(), cmbSAList.SelectedItem.Text.ToString(), txtCustomer.Text.Trim());
                        SendSMSCustomer(cmbSAList.SelectedItem.Text.ToString(), txtPhone1.Text.ToString());
                        SendSMS(txtRegNO.Text.ToString(), cmbSAList.SelectedItem.Text.ToString(), txtCustomer.Text.Trim(), drpModel.Text.Trim().ToString(), txtPhone1.Text.Trim());
                        SendNotification(cmbSAList.SelectedItem.Text.ToString(), txtRegNO.Text.ToString(), txtCustomer.Text.Trim(), drpModel.Text.Trim().ToString(), txtPhone1.Text.Trim());
                    }
                    catch (Exception ex)
                    {
                    }
                    UpdateAll();
                    txtRFID.Text = "";
                    txtRegNO.Text = "";
                    txtSrchTag.Text = "";
                    cmbSAList.SelectedIndex = 0;
                    drpModel.SelectedIndex = -1;
                    txtCustomer.Text = "";
                    txtPhone1.Text = "";
                    txtEmail.Text = "";
                    chkAppointment.Checked = true;
                    chkWalkIn.Checked = false;
                    chk_WhiteBoard.Checked = true;
                    chk_Yellow.Checked = false;
                    txtPreviousSA.Text = "";
                    txtServiceType.Text = "";
                    txtVisitingDate.Text = "";
                    lblSlno.Text = "";
                }
                else
                {
                    lblMsg.ForeColor = Color.Red;
                    lblMsg.Text = (string)param2.Value;
                    txtRegNO.Focus();
                    fillSAGrid();
                    fillSAList();
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "" + ex.Message;
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
        }
    }
    public void SendSMS(string VehNo, string SAName, string CustomerName, string VehicleModel, string CustPhone)
    {

        string PhoneNo = GetPhoneNo(SAName);
        // string Message = "வணக்கம்";
        string Message = "Mr. " + SAName + " , A New Vehicle " + VehNo + ", " + VehicleModel + " alloted to you. Please Contact your Customer " + CustomerName + " @ " + CustPhone;
        //string Message = "Mr." + SAName + ", A New Vehicle alloted to you. Vehicle Number : " + VehNo + " ,Vehicle model : " + VehicleModel + " ,Customer Name : " + CustomerName + " ,Contact No : " + CustPhone;
        HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("http://alerts.sinfini.com/api/web2sms.php?username=spinaccts&password=wReBe3r*vEq&to=" + PhoneNo + "&sender=SPINCX&message=" + Message);

        HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();

        System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());

        string responseString = respStreamReader.ReadToEnd();

        if (responseString.Contains("Message GID="))
        {
            if (Message.Length > 160)
                InsertSMSCount(2);
            else
                InsertSMSCount(1);
        }

        respStreamReader.Close();

        myResp.Close();

    }
    public void SendSMSCustomer(string SAName, string PhoneNo)
    {

        string TokenNo = GetTokenNo(SAName);
        string DealerName = GetDealerDetails();
        string Message = "Welcome to " + DealerName + ". SA " + SAName + " will attend to you soon, Your Token " + TokenNo + ", meanwhile we request you to relax at our customer lounge. It is our pleasure serving you.";
        //string Message = "Welcome to Jasper Auto. SA "+SAName+" will attend to you soon, Your Token # "+TokenNo+" , meanwhile we request you to relax at our customer lounge. It is our pleasure serving you.";
        // string Message = "Welcome to Jasper Auto. SA " + SAName + " will attend to you soon, Your Token # " + TokenNo + " , meanwhile we request you to relax at our customer lounge. It is our pleasure serving you.";
        // string Message = "Mr." + SAName + ", A New Vehicle alloted to you. Vehicle Number : " + VehNo + " ,Vehicle model : " + VehicleModel + " ,Customer Name : " + CustomerName + " ,Contact No : " + CustPhone;
        HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("http://alerts.sinfini.com/api/web2sms.php?username=spinaccts&password=wReBe3r*vEq&to=" + PhoneNo + "&sender=SPINCX&message=" + Message);

        HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();

        System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());

        string responseString = respStreamReader.ReadToEnd();

        if (responseString.Contains("Message GID="))
        {
            if (Message.Length > 160)
                InsertSMSCount(2);
            else
                InsertSMSCount(1);
        }

        respStreamReader.Close();

        myResp.Close();

    }
    private string GetTokenNo(string SAName)
    {
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        string TokenNo = "";
        try
        {
            // SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
            SqlCommand cmd = new SqlCommand("", con);
            cmd.CommandText = "GetSAAllotedToken";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@SAName", SAName);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                TokenNo = dt.Rows[0]["TodayAlloted"].ToString();
            }
        }
        catch (Exception ex)
        {

        }
        return TokenNo;
    }
    private void InsertSMSCount(int Count)
    {
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());

        try
        {
            // SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
            SqlCommand cmd = new SqlCommand("", con);
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            cmd.CommandText = "udpInsertSMSCount";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@SMSCount", Count);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception ex)
        {
            con.Close();
        }

    }
    public string GetDealerDetails()
    {
        try
        {
            string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
            SqlConnection con = new SqlConnection(sConnString);
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "udpGetDealerDetailsforSMS";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count != 0)
                return dt.Rows[0][0].ToString();
            else
                return "-Dealer Name, Place-";
        }
        catch (Exception ex)
        {
            return ">Dealer Name, Place<";
        }
    }

    public string SendNotification(string SAName, string VehNo, string CustomerName, string VehicleModel, string CustPhone)
    {
        string DeviceId = GetDeviceId(SAName);
        string Message = "Mr. " + SAName + " , A New Vehicle " + VehNo + ", " + VehicleModel + " alloted to you. Please Contact your Customer " + CustomerName + " @ " + CustPhone;
        //string GoogleAppID = "AIzaSyCCRRFRtGmRzXbijhf36q_TUT2jNpQErOc";
        //var SENDER_ID = "416757130368";
        string GoogleAppID = "AIzaSyC0ZFRA7NtjaFvicXmn2P5LeBaipfCBTrI";
        var SENDER_ID = "126955601626";
        var value = Message;
        //string DeviceID = "APA91bHhRoN7hlkG1duO8PlQgHn2E8hcfVpP7vBizLwkPg-FJFg-f9qme8xog7hBG-vyghQDmptyl0WkrzRRjJzbUjbjUkqkDCre-r7MkQv5w6bqznShvBO6WYizsBN2yhi0hiNwcf3M";
        WebRequest tRequest;
        tRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");
        tRequest.Method = "post";
        tRequest.ContentType = " application/x-www-form-urlencoded;charset=UTF-8";
        tRequest.Headers.Add(string.Format("Authorization: key={0}", GoogleAppID));

        tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));

        //tRequest.Headers.Add(string.Format("data: message={0}",value));


        string postData = "collapse_key=score_update&data.message =" + value + "&registration_id=" + DeviceId;
        //string postData = "collapse_key=score_update&time_to_live=108&delay_while_idle=1& data.message =" + value + "&data.time=" + System.DateTime.Now.ToString() + "&registration_id=" + deviceId + "";
        Console.WriteLine(postData);
        Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
        tRequest.ContentLength = byteArray.Length;

        Stream dataStream = tRequest.GetRequestStream();
        dataStream.Write(byteArray, 0, byteArray.Length);
        dataStream.Close();

        WebResponse tResponse = tRequest.GetResponse();

        dataStream = tResponse.GetResponseStream();

        StreamReader tReader = new StreamReader(dataStream);

        String sResponseFromServer = tReader.ReadToEnd();

        tReader.Close();
        dataStream.Close();
        tResponse.Close();


        return sResponseFromServer;


    }
    private string GetDeviceId(string SAName)
    {
        string DeviceId = "";
        try
        {
            SqlConnection con = new SqlConnection(DataManager.ConStr);
            SqlCommand cmd = new SqlCommand("", con);
            cmd.CommandText = "udpGetSAPhoneNo";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@SAName", SAName);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                DeviceId = dt.Rows[0]["DeviceId"].ToString();
            }
        }
        catch (Exception ex)
        {

        }
        return DeviceId;
    }
    protected void btn_Clear_Click(object sender, EventArgs e)
    {
        txtRFID.ReadOnly = true;
        txtRFID.Text = "";
        txtRegNO.Text = "";
        cmbSAList.SelectedIndex = 0;
        chkAppointment.Checked = true;
        chkWalkIn.Checked = false;
        chk_WhiteBoard.Checked = true;
        chk_Yellow.Checked = false;
        lblMsg.Text = "";
        drpModel.SelectedIndex = -1;
        txtCustomer.Text = "";
        txtPhone1.Text = "";
        cmbSAList.Enabled = true;
        txtPreviousSA.Text = "";
        txtServiceType.Text = "";
        txtVisitingDate.Text = "";
        lblSlno.Text = "";
        txtEmail.Text = "";
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        txtRFID.ReadOnly = false;
        txtRFID.Text = "";
        errSrchTag.Text = "";
        btnSave.Visible = true;
        btnCancel.Visible = true;
        txtSrchTag.Text = "";
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        btnSave.Visible = false;
        btnCancel.Visible = false;
        txtRFID.ReadOnly = true;
        lblMsg.Text = "";
        message.Text = "";
    }

    protected void ddlCancelationRemarks_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CmbCancelationRemarks.SelectedValue == "-1")
        {
            txtCancelationRemark.Visible = true;
        }
        else
        {
            txtCancelationRemark.Visible = false;
        }
    }

    protected void grdSAMapping_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdSAMapping.PageIndex = e.NewPageIndex;
        fillSAGrid();
    }

    protected void grdSAMapping_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
        }
    }

    protected void chkWalkIn_CheckedChanged(object sender, EventArgs e)
    {
        if (chkAppointment.Checked)
        {
            chkAppointment.Checked = false;
        }
        else
        {
            chkAppointment.Checked = true;
        }
    }

    protected void chkAppointment_CheckedChanged(object sender, EventArgs e)
    {
        if (chkWalkIn.Checked)
        {
            chkWalkIn.Checked = false;
        }
        else
        {
            chkWalkIn.Checked = true;
        }
    }

    protected void btnLookup_Click(object sender, EventArgs e)
    {
        txtRFID.Enabled = false;
        btnMap.Text = "Update";
        GetDetails();
    }

    private void GetDetails()
    {
        try
        {
            if (txtRegNO.Text.Trim() != "")
            {
                SqlConnection con = new SqlConnection(DataManager.ConStr);
                SqlCommand cmd = new SqlCommand("", con);
                cmd.CommandText = "udpSearchVehicleDetailsforFO";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@VehicleNo", txtRegNO.Text.Trim().ToUpper());
                SqlParameter param1 = new SqlParameter();
                SqlParameter param2 = new SqlParameter();
                param1 = new SqlParameter("@flag", SqlDbType.Int);
                param1.Direction = ParameterDirection.Output;
                param2 = new SqlParameter("@msg", SqlDbType.NVarChar, 300);
                param2.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(param1);
                cmd.Parameters.Add(param2);
                SqlDataAdapter da1 = new SqlDataAdapter(cmd);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    txtRFID.Text = dt1.Rows[0]["RFID"].ToString();
                    if (String.IsNullOrEmpty(dt1.Rows[0]["VehicleModel"].ToString()))
                    {
                        drpModel.SelectedIndex = -1;
                    }
                    else
                    {
                        drpModel.Text = dt1.Rows[0]["VehicleModel"].ToString();
                    }
                    txtCustomer.Text = dt1.Rows[0]["CustomerName"].ToString();
                    txtPhone1.Text = dt1.Rows[0]["Customerphone"].ToString();
                    if (dt1.Rows[0]["IsAppointment"].ToString() == "True")
                    {
                        chkWalkIn.Checked = true;
                        chkAppointment.Checked = false;
                    }
                    else if (dt1.Rows[0]["IsAppointment"].ToString() == "False")
                    {
                        chkAppointment.Checked = true;
                        chkWalkIn.Checked = false;
                    }
                    if ((int)param1.Value == 2)
                    {
                        if (String.IsNullOrEmpty(dt1.Rows[0]["JCOCreatedBy"].ToString()))
                        {
                            cmbSAList.SelectedIndex = -1;
                        }
                        else
                        {
                            cmbSAList.SelectedValue = dt1.Rows[0]["JCOCreatedBy"].ToString();
                        }
                        cmbSAList.Enabled = false;
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(dt1.Rows[0]["JCOCreatedBy"].ToString()))
                        {
                            cmbSAList.SelectedIndex = -1;
                        }
                        else
                        {
                            cmbSAList.SelectedValue = dt1.Rows[0]["JCOCreatedBy"].ToString();
                        }
                        cmbSAList.Enabled = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void txtRegNO_TextChanged(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        try
        {
            message.Text = "";
            if (txtRegNO.Text.Trim() != "")
            {
                SqlCommand cmd = new SqlCommand("", con);
                cmd.CommandText = "udpFetchLastVehicleDtls";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@VehicleNo", txtRegNO.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@RefId", lblSlno.Text.ToString());
                SqlDataAdapter da1 = new SqlDataAdapter(cmd);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    if (dt1.Rows[0]["Flag"].ToString() == "2")
                    {
                        message.ForeColor = Color.Red;
                        message.Text = "Vehicle Number Already In Use.";
                    }
                    else if (dt1.Rows[0]["Flag"].ToString() == "1")
                    {
                        if (String.IsNullOrEmpty(dt1.Rows[0]["VehicleModel"].ToString()))
                        {
                            drpModel.SelectedIndex = -1;
                        }
                        else
                        {
                            drpModel.Text = dt1.Rows[0]["VehicleModel"].ToString();
                        }

                        txtCustomer.Text = dt1.Rows[0]["CustomerName"].ToString();
                        txtPhone1.Text = dt1.Rows[0]["Customerphone"].ToString();
                        txtEmail.Text = dt1.Rows[0]["Email"].ToString();
                        txtServiceType.Text = dt1.Rows[0]["ServiceType"].ToString();
                        txtVisitingDate.Text = dt1.Rows[0]["GateIn"].ToString();
                        txtPreviousSA.Text = dt1.Rows[0]["ServiceAdvisor"].ToString();
                    }
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void BindVehicleNo()
    {
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlCommand cmdVehi = new SqlCommand("udpListVehicleDetailsforFO", con);
        cmdVehi.CommandType = CommandType.StoredProcedure;
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

    protected void Button3_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        try
        {
            lblEditCustMsg.ForeColor = Color.Red;
            lblEditCustMsg.Text = "";
            if (ddlVehicle.SelectedIndex == 0)
            {
                lblEditCustMsg.Text = "Please Select Vehicle No.";
                ddlVehicle.Focus();
            }
            else
            {
                cmd = new SqlCommand("", con);
                cmd.CommandText = "UpdateTblMasterCustomerDetailsforFO";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@CustomerName", txtCustName.Text.Trim());
                cmd.Parameters.AddWithValue("@Customerphone", txtPhone.Text.Trim());
                cmd.Parameters.AddWithValue("@Email", txtemailid.Text.Trim());
                cmd.Parameters.AddWithValue("@RegNo", ddlVehicle.SelectedValue.ToString());
                try
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        lblEditCustMsg.ForeColor = Color.Green;
                        lblEditCustMsg.Text = "Updated Successfully";
                    }
                    else
                    {
                        lblEditCustMsg.ForeColor = Color.Red;
                        lblEditCustMsg.Text = "Update Aborted, Please Try Again.";
                    }
                }
                catch (Exception ex)
                {
                    lblEditCustMsg.ForeColor = Color.Red;
                    lblEditCustMsg.Text = ex.Message;
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
        }
        catch (Exception ex)
        { }
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        ddlVehicle.SelectedIndex = -1;
        txtCustName.Text = "";
        txtPhone.Text = "";
        txtemailid.Text = "";
        lblEditCustMsg.Text = "";
    }

    protected void ddlVehicle_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        try
        {
            Label10.ForeColor = Color.Black;
            Label10.Text = "";
            if (ddlVehicle.SelectedIndex != -1)
            {
                string str = "udpGetCustomerDetailsforFO";
                cmd = new SqlCommand(str, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RegNo", ddlVehicle.SelectedValue.ToString());
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    txtCustName.Text = dt.Rows[0][0].ToString();

                    txtPhone.Text = dt.Rows[0][1].ToString();
                    txtemailid.Text = dt.Rows[0][2].ToString();
                }
            }
            else
            {
                lblEditCustMsg.ForeColor = Color.Black;
                lblEditCustMsg.Text = "Vehicle Not Registered for Servicing ";
                ddlVehicle.Focus();
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void Refresh_Click(object sender, ImageClickEventArgs e)
    {
        BindVehicleNo();
        txtCustName.Text = "";
        txtPhone.Text = "";
        txtemailid.Text = "";
    }

    protected void txtRegNO_TextChanged1(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        if (txtRegNO.Text.Trim() != "")
        {
            SqlCommand cmd = new SqlCommand("", con);
            cmd.CommandText = "udpFetchLastVehicleDtls";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@VehicleNo", txtRegNO.Text.Trim().ToUpper());
            cmd.Parameters.AddWithValue("@RefId", lblSlno.Text.ToString());
            SqlDataAdapter da1 = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                if (dt1.Rows[0]["Flag"].ToString() != "0")
                {
                    if (String.IsNullOrEmpty(dt1.Rows[0]["VehicleModel"].ToString()))
                    {
                        drpModel.SelectedIndex = -1;
                    }
                    else
                    {
                        drpModel.Text = dt1.Rows[0]["VehicleModel"].ToString();
                    }

                    txtCustomer.Text = dt1.Rows[0]["CustomerName"].ToString();
                    txtPhone1.Text = dt1.Rows[0]["Customerphone"].ToString();

                    txtServiceType.Text = dt1.Rows[0]["ServiceType"].ToString();
                    txtVisitingDate.Text = dt1.Rows[0]["GateIn"].ToString();
                    txtPreviousSA.Text = dt1.Rows[0]["ServiceAdvisor"].ToString();
                }
            }
        }
    }
    protected void chk_WhiteBoard_CheckedChanged(object sender, EventArgs e)
    {
        if (chk_Yellow.Checked)
        {
            chk_Yellow.Checked = false;
        }
        else
        {
            chk_Yellow.Checked = true;
        }
    }

    protected void chk_Yellow_CheckedChanged(object sender, EventArgs e)
    {
        if (chk_WhiteBoard.Checked)
        {
            chk_WhiteBoard.Checked = false;
        }
        else
        {
            chk_WhiteBoard.Checked = true;
        }
    }

    protected void btn_display_Click(object sender, EventArgs e)
    {
        Response.Redirect("FrontOfficeDisplayStatus.aspx");
    }
}