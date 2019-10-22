using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Globalization;

public partial class Cashier_New : System.Web.UI.Page
{
  // private SqlConnection con = new SqlConnection(DataManager.ConStr);
    private string userId = "0";
    //private decimal BillAmount;
    //private decimal PartsAmount;
    //private decimal LabourAmount;
    //private decimal VasAmount;
    //private decimal LubeAmount;

    protected void Page_Load(object sender, EventArgs e)
    {
        //calndrrPDt1.StartDate = DateTime.Now;
        //calndrrPDt.StartDate = DateTime.Now;

        //if (Credit.Checked == true)
        //{
        //    CrAmtCell.Visible = true;
        //    CrDateCell.Visible = true;
        //    Credit.Checked = true;
        //    InAmtCell.Visible = false;
        //    InDateCell.Visible = false;
        //}
      

        //  if (Insurance.Checked == true)
        //{
        //    InAmtCell.Visible = true;
        //    InDateCell.Visible = true;
        //    Insurance.Checked = true;
        //    CrAmtCell.Visible = false;
        //    CrDateCell.Visible = false;
        //}

        //if (CashPaid.Checked == true || BodyShop.Checked == true || AMC.Checked == true || FreeService.Checked == true || CheckDD.Checked == true || Warranty.Checked == true)
        //{
        //    InAmtCell.Visible = false;
        //    InDateCell.Visible = false;
        //    Insurance.Checked = false;
        //    Credit.Checked = false;
        //    CrAmtCell.Visible = false;
        //    CrDateCell.Visible = false;
        //}



        if (!Page.IsPostBack)
        {
          
               

            try
            {
                lblRefNo.Text = "0";
                if (Session["ROLE"] == null || Session["ROLE"].ToString() != "CASHIER")
                {
                    Response.Redirect("login.aspx");
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("login.aspx");
            }
            txtRemarks.Attributes.Add("maxlength", txtRemarks.MaxLength.ToString());
            //MultiView1.ActiveViewIndex = 0;


            if ((!string.IsNullOrEmpty(txtPartsAmount.Text)) || (!string.IsNullOrEmpty(txtLabourAmount.Text)))
            {
                txtBillAmount.Text = (Convert.ToInt32(txtPartsAmount.Text) + Convert.ToInt32(txtLabourAmount.Text)).ToString();
            }
            fillgrid();
            GetSMSCountlabel();
        }
        
        
        ////fillPendinggrid();
        Session["Current_Page"] = "Bill Amount Details";
        this.Title = "Bill Amount Details";
        //string[] str = GetDealerDetails(Session[Session["TMLDealercode"] + "-TMLConString"].ToString()).Split(',');
        //Session["CompanyName"] = str[0];
    }
    


    public  string GetDealerDetails(string Connection)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString()))
            {
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "udpGetDealerDetails";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                con.Open();
                da.Fill(dt);
                if (dt.Rows.Count != 0)
                    return dt.Rows[0][0].ToString();
                else
                    return "-Dealer Name, Place-";
            }
        }
        catch (Exception ex)
        {
            return ">Dealer Name, Place<";
        }
    }
    private void fillgrid()
    {
        try
        {
            grdCashier.DataSource = null;
            using (SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("GetJobCards", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                da.Fill(dt);

                if (dt.Rows.Count != 0)
                {
                    grdCashier.DataSource = dt;
                    grdCashier.DataBind();
                }
                else
                {
                    grdCashier.DataSource = null;
                    grdCashier.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void txtPartsAmount_TextChanged(object sender, EventArgs e)
    {
        lblMsg.Attributes.Add("style", "text-transform:none !important");
        txtLabourAmount.Focus();
        lblMsg.CssClass = "reset";
        lblMsg.Text = "";
        if (string.IsNullOrEmpty(txtLabourAmount.Text))
        {
            txtBillAmount.Text = (Convert.ToDecimal(txtPartsAmount.Text)).ToString();
        }
        else if ((string.IsNullOrEmpty(txtPartsAmount.Text)) && (string.IsNullOrEmpty(txtLabourAmount.Text)))
        {
            txtBillAmount.Text = (Convert.ToInt32(txtPartsAmount.Text) + Convert.ToInt32(txtLabourAmount.Text)).ToString();
        }
    }
    protected void txtLabourAmount_TextChanged(object sender, EventArgs e)
    {
        txt_VasAmt.Focus();
        lblMsg.Attributes.Add("style", "text-transform:none !important");
        lblMsg.CssClass = "reset";
        lblMsg.Text = "";
        if (string.IsNullOrEmpty(txtPartsAmount.Text))
        {
            txtBillAmount.Text = (Convert.ToDouble(txtLabourAmount.Text)).ToString();
        }
        else if ((!string.IsNullOrEmpty(txtPartsAmount.Text)) && (!string.IsNullOrEmpty(txtLabourAmount.Text)))
        {
            txtBillAmount.Text = (Convert.ToDouble(txtPartsAmount.Text) + Convert.ToDouble(txtLabourAmount.Text)).ToString();
        }
    }

    //protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
    //{
    //    DateTime pastday = e.Day.Date;
    //    DateTime date = DateTime.Now;
    //    int year = date.Year;
    //    int month = date.Month;
    //    int day = date.Day;
    //    DateTime today = new DateTime(year, month, day);
    //    if (pastday.CompareTo(today) < 0)
    //    {
    //        e.Cell.BackColor = System.Drawing.Color.Gray;
    //        e.Day.IsSelectable = false;
    //    }
    //}

    //protected void Calendar2_SelectionChanged(System.Object sender, System.EventArgs e)
    //{
    //    TextBox1.Text = Convert.ToDateTime(Calendar2.SelectedDate, CultureInfo.GetCultureInfo("en-US")).ToString("MM/dd/yyyy");
    //}

    protected void txtBillAmount_TextChanged(object sender, EventArgs e)
    {
        lblMsg.CssClass = "reset";
        lblMsg.Text = "";
        if ((!string.IsNullOrEmpty(txtPartsAmount.Text)) && (!string.IsNullOrEmpty(txtLabourAmount.Text)))
        {
            txtBillAmount.Text = (Convert.ToDouble(txtPartsAmount.Text) + Convert.ToDouble(txtLabourAmount.Text)).ToString();
        }
    }


    protected void btn_logout_Click(object sender, EventArgs e)
    {
        Response.Redirect("login.aspx");
    }
    protected void btn_Support_Click(object sender, EventArgs e)
    {
        Response.Redirect("Complain.aspx");
    }



    protected void grdCashier_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        lblMsg.CssClass = "reset";
        lblMsg.Text = "";
        grdCashier.PageIndex = e.NewPageIndex;
        fillgrid();
    }



    protected void grdCashier_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            e.Row.Cells[1].Visible = false;
        }
            //e.Row.Cells[1].Visible = false;
            //e.Row.Cells[6].Visible = false;
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //e.Row.Cells[1].Visible = false;
            //    if (e.Row.Cells[4].Text.Trim() == "True" || e.Row.Cells[4].Text.Trim() == "1")
            //    {
            //        e.Row.Attributes.Add("Style", "background-color:#9CC7F2;color:White;");
            //    }
            //}
            //e.Row.Cells[6].Visible = false;
            //e.Row.Cells[5].Visible = false;
        }



    protected void grdCashier_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtPartsAmount.Focus();
        txtPartsAmount.Text = "";
        txtLabourAmount.Text = "";
        txtBillAmount.Text = "";
        txtRemarks.Text = "";
        txt_VasAmt.Text = "";
        rdPaytype.SelectedValue = "0";
        try
        {
            txtTagNo.Enabled = false;
            if (grdCashier.SelectedRow.Cells[3].Text.ToString() == "&nbsp;") { 
                txtTagNo.Text = "";
            }
            else {
            txtRegNo.Text = grdCashier.SelectedRow.Cells[3].Text.Trim();
            }

            txtTagNo.Text = grdCashier.SelectedRow.Cells[2].Text.Trim();
            lblRefNo.Text = grdCashier.SelectedRow.Cells[1].Text.Trim();
            Session["SlNoNew"] = lblRefNo.Text;
            lblMsg.Text = "";
            lblMsg.CssClass = "reset";
        }
        catch (Exception ex)
        {
        }
    }
    public DataTable GetDealerDetails()
    {
        try
        {
            //string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
            using (SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString()))
            {
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "udpGetDealerDetailsforSMS";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                con.Open();
                da.Fill(dt);
                if (dt.Rows.Count != 0)
                    return dt;
                else
                    return null;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }



    protected void btnSave_Click1(object sender, EventArgs e)
    {
        String category = string.Empty;
        try
        {
            //userId = Session["EmpId"].ToString();
            if (txtRegNo.Text.ToString() == "" && txtTagNo.Text.ToString() == "")
            {
                lblMsg.Attributes.Add("style", "text-transform:none !important");
                lblMsg.Text = "Please enter TagNo and VehicleNo..!";
                lblMsg.CssClass = "ErrMsg";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
                
            }
            //else if (txtPartsAmount.Text.ToString() == "" || txtPartsAmount.Text.ToString().Trim() == "0")
            //{
            //    lblMsg.Text = "Please Enter Parts Amount..!";
            //    lblMsg.CssClass = "ErrMsg";
            //    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");

            //}
            else if (txtLabourAmount.Text.ToString() == "")
            {
                lblMsg.Attributes.Add("style", "text-transform:none !important");
                lblMsg.Text = "Please enter Labour Amount..!";
                lblMsg.CssClass = "ErrMsg";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");


            }
            else if (rdPaytype.SelectedItem.Text.Contains("Credit") && (txt_creditAmt.Text=="" || Credit_date.Text==""))
            {
                lblMsg.Attributes.Add("style", "text-transform:none !important");
                lblMsg.Text = "Please enter Credit Amount and Credit Date..!";
                lblMsg.CssClass = "ErrMsg";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");

            }

            else if (rdSrcType.SelectedItem.Text.Contains("Insurance") && (txtInsAmt.Text == "" || txtInsDate.Text == ""))
            {
                lblMsg.Attributes.Add("style", "text-transform:none !important");
                lblMsg.Text = "Please enter Insurance Amount and Insurance Date..!";
                lblMsg.CssClass = "ErrMsg";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");

            }
            else if (rdPaytype.SelectedItem.Text.Contains("Cheque") && txtRefNO.Text=="")
            {
                lblMsg.Attributes.Add("style", "text-transform:none !important");
                lblMsg.Text = "Please enter Cheque reference Number..!";
                lblMsg.CssClass = "ErrMsg";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");

            }
            else
            {
                
                //BillAmount = Convert.ToDecimal(txtBillAmount.Text.Trim());
                //PartsAmount = Convert.ToDecimal(txtPartsAmount.Text.Trim());
                //VasAmount = Convert.ToDecimal(txt_VasAmt.Text.Trim());
                //LubeAmount = Convert.ToDecimal(txt_LubeAmt.Text.Trim());
                //if (txtLabourAmount.Text.ToString() == "")
                //{
                //    LabourAmount = 0;
                //}
                //else {
                //LabourAmount = Convert.ToDecimal(txtLabourAmount.Text.Trim());
                //}


                //if (GetTotal(BillAmount, PartsAmount, LabourAmount, VasAmount,LubeAmount))
                //{
                //    //lblMsg.ForeColor = Color.Red;
                //    //lblMsg.Text = "Specified Total Amount Should Be Match with Bill Amount..! ";
                //    //txtBillAmount.Focus();
                //}

                //else
                //{
                    using (SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString()))
                    {
                        SqlCommand cmd1 = new SqlCommand("", con);
                        if (txtTagNo.Text.ToString() != "")
                        {
                            cmd1.CommandText = "UdpGetSlNoByTagRegNo";
                            cmd1.CommandType = CommandType.StoredProcedure;
                            cmd1.Parameters.AddWithValue("@RFID", txtTagNo.Text.ToString());
                        cmd1.Parameters.AddWithValue("@RegNo", txtRegNo.Text.ToString());
                    }
                        else
                        {
                            cmd1.CommandText = "UdpGetSlNoByRegNo";
                            cmd1.CommandType = CommandType.StoredProcedure;
                            cmd1.Parameters.AddWithValue("@RegNo", txtRegNo.Text.ToString());
                        }
                        SqlDataAdapter da = new SqlDataAdapter(cmd1);
                        DataTable dt = new DataTable();
                        con.Open();
                        da.Fill(dt);

                        if (dt.Rows.Count == 0)
                        {
                            lblMsg.Attributes.Add("style", "text-transform:none !important");
                            lblMsg.Text = "There is no vehicle with this NO# or already done..!";
                            lblMsg.CssClass = "ErrMsg";
                            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");

                        }
                        else
                        {
                            Session["SlNoNew"] = dt.Rows[0]["SlNo"].ToString();
                            SqlCommand cmd = new SqlCommand("UdpInsertCashDetails", con);
                            if (con.State != ConnectionState.Open)
                            {
                                con.Open();
                            }

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@RFID", txtTagNo.Text.ToString());
                            cmd.Parameters.AddWithValue("@RegNo ", txtRegNo.Text.ToString());
                            cmd.Parameters.AddWithValue("@PartsAmount", txtPartsAmount.Text.Trim().ToString());
                            if (txtLabourAmount.Text.ToString() == "")
                            {
                                cmd.Parameters.AddWithValue("@LabourAmount", 0);
                            }
                            else {
                                cmd.Parameters.AddWithValue("@LabourAmount", txtLabourAmount.Text.Trim().ToString());
                            }
                            if (txt_VasAmt.Text.ToString() == "")
                            {
                                cmd.Parameters.AddWithValue("@VasAmt", 0);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@VasAmt", txt_VasAmt.Text.Trim().ToString());
                            }
                            cmd.Parameters.AddWithValue("@Paytype", rdPaytype.SelectedValue);
                        cmd.Parameters.AddWithValue("@SourceType", rdSrcType.SelectedValue);
                        if (rdPaytype.SelectedValue=="1")
                            {
                                cmd.Parameters.AddWithValue("@CreditAmt", Convert.ToInt32(txt_creditAmt.Text.Trim().ToString()));
                                cmd.Parameters.AddWithValue("@CreditOrInsDate", Convert.ToDateTime(Credit_date.Text.Trim().ToString()));

                            }
                          
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreditAmt", DBNull.Value);
                                cmd.Parameters.AddWithValue("@CreditOrInsDate", DBNull.Value);

                        }

                         if (rdSrcType.SelectedValue == "2")
                        {
                            cmd.Parameters.AddWithValue("@InsAmt", Convert.ToInt32(txtInsAmt.Text.Trim().ToString()));
                            cmd.Parameters.AddWithValue("@InsDate", Convert.ToDateTime(txtInsDate.Text.Trim().ToString()));
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@InsAmt", DBNull.Value);
                            cmd.Parameters.AddWithValue("@InsDate", DBNull.Value);

                        }
                        if (txt_LubeAmt.Text.ToString() == "")
                            {
                                cmd.Parameters.AddWithValue("@LubeAmt", 0);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@LubeAmt", txt_LubeAmt.Text.Trim().ToString());
                            }
                            //cmd.Parameters.AddWithValue("@LubeAmt", txt_LubeAmt.Text.Trim().ToString());
                            cmd.Parameters.AddWithValue("@BillAmount", txtBillAmount.Text.Trim().ToString());
                            cmd.Parameters.AddWithValue("@BillDate", System.DateTime.Now);
                            cmd.Parameters.AddWithValue("@UserId", Session["EmpId"].ToString());
                            if (txtRemarks.Text.ToString() == "")
                                cmd.Parameters.AddWithValue("@Remarks", DBNull.Value);
                            else
                                cmd.Parameters.AddWithValue("@Remarks", txtRemarks.Text.Trim().ToString());
                        if (txtRefNO.Text=="")
                        {
                            cmd.Parameters.AddWithValue("@ChequeRefNo", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@ChequeRefNo", txtRefNO.Text.Trim().ToString());
                        }
                       
                        SqlParameter Flag = cmd.Parameters.Add("@Msg", SqlDbType.VarChar, 100);
                            Flag.Direction = ParameterDirection.Output;
                            Flag.Value = "";
                            cmd.ExecuteNonQuery();
                            con.Close();
                            lblMsg.CssClass = "ScsMsg";
                            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
                            lblMsg.Attributes.Add("style", "text-transform:none !important");
                            lblMsg.Text = Flag.Value.ToString();
                            //Clear();
                            fillgrid();
                            if (lblMsg.Text == "Saved Successfully")
                            {
                                SendDataForCX10(Convert.ToInt32(dt.Rows[0]["SlNo"].ToString()), txtRegNo.Text.ToString().ToUpperInvariant(), txtPartsAmount.Text.Trim(), txtLabourAmount.Text.Trim(), txtBillAmount.Text.Trim(), txt_VasAmt.Text.Trim(),txt_LubeAmt.Text.Trim());
                                Clear();
                                int SMSCount = Convert.ToInt32(GetSMSCount());
                                int SMSLimitCount = Convert.ToInt32(GetSMSLimitCount());
                                if (SMSCount < SMSLimitCount)
                                {
                                    SendSMSCustomer(dt.Rows[0]["SlNo"].ToString());
                                    //SendSMSCustomercamp(dt.Rows[0]["SlNo"].ToString());
                                    GetSMSCountlabel();
                                }
                                // Response.Redirect("PrintNewBill1.aspx", false);
                                GetSMSCountlabel();

                            }
                        }
                    }
                
            }
        }
        catch (Exception ex)
        {
            //lblMsg.ForeColor = Color.Red;
            //lblMsg.Text = ex.Message.ToString();
        }
        finally
        {
            //if (con.State != ConnectionState.Closed)
            //    con.Close();
        }
    }

    public void SendSMSCountForCX10(string DealerCode, int SMSCount)
    {

        try
        {

            var request = (HttpWebRequest)WebRequest.Create("http://v6api.cx100.in/api/Cx10_api/sms");

            var postData = "decode=" + DealerCode + "&count=" + SMSCount;
            // var postData = "decode=001-SAIL-GRAND&count="+SMSCount;
            var data = System.Text.Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

        }
        catch (Exception ex)
        {

        }

    }
    public void SendDataForCX10(int Slno, string RegNo, string PartsAmount, string labouramount, string Billamount, string VasAmt, string LubeAmt)
    {
        try
        {
            string UCOde = GetDealerCode();
            var request = (HttpWebRequest)WebRequest.Create("http://v6api.cx100.in/V6/ApiCash/updateCashDetailsTML");
            string Insdate = "";
            string ChkDate = "";
            if(txtInsDate.Text.Trim()!="" || txtInsDate.Text.Trim() != null)
            {
                Insdate = txtInsDate.Text.Trim();
            }
            else
            {
                Insdate = "2000-01-01";
            }
         
            if (Credit_date.Text.Trim() != "" || Credit_date.Text.Trim() != null)
            {
                ChkDate = Credit_date.Text.Trim();
            }
            else
            {
                ChkDate = "2000-01-01";
            }
            var postData = "reference=" + Slno+ "&dealercode=" + UCOde + "&vrn=" + RegNo + "&parts=" + PartsAmount + "&labour=" + labouramount+ "&vas=" + VasAmt + "&total=" + Billamount + "&lube=" + LubeAmt + "&modeofpay="+ rdPaytype.SelectedValue + "&srcofpay="+ rdSrcType.SelectedValue +"&creditamt="+ txt_creditAmt.Text+"&creditdate="+ ChkDate + "&insamt=" + txtInsAmt.Text.Trim()+"&insdate="+ Insdate + "&check_ref_no=" + txtRefNO.Text;
            // var postData = "decode=001-SAIL-GRAND&vrn="+RegNo+"&part="+PartsAmount+"&labour="+labouramount+"&total="+Billamount;
            var data = System.Text.Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

        }
        catch (Exception ex)
        {

        }


    }
    

    //public void SendSMSCustomer(string Slno)
    //{

    //    string CustPhone = GetPhoneNo(Slno);
    //    DataTable dt = GetDealerDetails();
    //   // string Message = "எங்களது அதிர்ஷ்ட் ஆட்டோ நிறுவனத்திற்கு வருகை தந்தமைக்கு மிக்க நன்றி. வணக்கம்";
    //    string Message = "Thank u for choosing our service! we will appreciate to serve you in feature!" + dt.Rows[0][0].ToString() + "," + dt.Rows[0][1].ToString() + "," + dt.Rows[0][2].ToString();
    //    //string Message = "அதிர்ஷ்ட் ஆட்டோ தங்களை அன்புடன் வரவேற்கிறது. எங்களது சர்வீஸ் அட்வைஸர் திரு " + SANAmeTamil + " விரைவில் உங்களை தொடர்பு கொள்வார். நன்றி Ph No:" + SAPhone;
    //    // string Message = "வணக்கம்";
    //    // string Message = "Welcome to Jasper Auto. SA " + SAName + " will attend to you soon, Your Token " +TokenNo+ ", meanwhile we request you to relax at our customer lounge. It is our pleasure serving you.";
    //    // string Message = "Welcome to Jasper Auto. SA "+SAName+" will attend to you soon, Your Token # "+TokenNo+" , meanwhile we request you to relax at our customer lounge. It is our pleasure serving you.";
    //    // string Message = "Welcome to Jasper Auto. SA " + SAName + " will attend to you soon, Your Token # " + TokenNo + " , meanwhile we request you to relax at our customer lounge. It is our pleasure serving you.";
    //    // string Message = "Mr." + SAName + ", A New Vehicle alloted to you. Vehicle Number : " + Veh No + " ,Vehicle model : " + VehicleModel + " ,Customer Name : " + CustomerName + " ,Contact No : " + CustPhone;
    //    HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("http://alerts.sinfini.com/api/web2sms.php?username=spinaccts&password=wReBe3r*vEq&to=" + CustPhone + "&sender=SPINCX&message=" + Message + "&unicode=1");

    //    HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();

    //    System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());

    //    string responseString = respStreamReader.ReadToEnd();
    //    if (responseString.Contains("Message GID="))
    //    {
    //        if (Message.Length>160)
    //        InsertSMSCount(2);
    //        else
    //            InsertSMSCount(1);
    //    }

    //    respStreamReader.Close();

    //    myResp.Close();

    //}

    public void SendSMSCustomer(string Slno)
    {

        string CustPhone = GetPhoneNo(Slno);
        DataTable dt = GetDealerDetails();

        string Message = "Thank u for choosing our service! we will appreciate to serve you in future!" + dt.Rows[0][0].ToString() + "," + dt.Rows[0][1].ToString() + "," + dt.Rows[0][2].ToString();

        HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("http://alerts.sinfini.com/api/web2sms.php?username=spinaccts&password=wReBe3r*vEq&to=" + CustPhone + "&sender=SPINCX&message=" + Message + "&unicode=1");

        HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();

        System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());

        string responseString = respStreamReader.ReadToEnd();
        if (responseString.Contains("Message GID="))
        {
            //var a = 0;
            //if (Message.Length != 0)
            //{
            //    a = Message.Length % 160;

            //    InsertSMSCount(Convert.ToInt16(Math.Round(a, 0)));
            //    SendSMSCountForCX10(GetDealerCode(), a);
            //}
            //else
            //{
            //    InsertSMSCount(a);
            //    SendSMSCountForCX10(GetDealerCode(), a);
            //}
            if (Message.Length > 145)
            {

                try
                {
                    SendSMSCountForCX10(GetDealerCode(), 2);
                }
                catch (Exception ex)
                { }
            }
            else
            {
                InsertSMSCount(1);
                try
                {
                    SendSMSCountForCX10(GetDealerCode(), 1);
                }
                catch (Exception ex)
                { }
            }

        }

        respStreamReader.Close();

        myResp.Close();

    }

    
    public string GetDealerCode()
    {
        try
        {
            
            using (SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString()))
            {
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "udpGetDCodeForCX10";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                con.Open();
                da.Fill(dt);
                if (dt.Rows.Count != 0)
                    return dt.Rows[0][0].ToString();
                else
                    return "";
            }
        }
        catch (Exception ex)
        {
            return "";
        }
    }
    private string GetPhoneNo(string Slno)
    {
        using (SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString()))
        {
            string PhoneNo = "";

            SqlCommand cmd = new SqlCommand("", con);
            cmd.CommandText = "udpGetCustomerPhoneNo";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@RefId", Slno);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                PhoneNo = dt.Rows[0]["Customerphone"].ToString();
            }

            return PhoneNo;
        }
    }
    private void InsertSMSCount(int Count)
    {
        using (SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString()))
        {
            SqlCommand cmd = new SqlCommand("", con);
            cmd.CommandText = "udpInsertSMSCount";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@SMSCount", Count);
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
    private string GetSMSLimitCount()
    {
        string SMSLimitCount = "";

        using (SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString()))
        {
            SqlCommand cmd = new SqlCommand("", con);
            cmd.CommandText = "udpGetSMSLimitCount";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                SMSLimitCount = dt.Rows[0]["SMSLimitCount"].ToString();
            }
        }
        return SMSLimitCount;
    }
    private string GetSMSCount()
    {
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        string SMSCount = "";
        try
        {
            // SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
            SqlCommand cmd = new SqlCommand("", con);
            cmd.CommandText = "udpGetSMSCount";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                SMSCount = dt.Rows[0]["SMSCount"].ToString();
            }
            else
            {
                SMSCount = "0";
            }
        }
        catch (Exception ex)
        {

        }
        finally
        {
            if (con.State != ConnectionState.Closed)
            {
                con.Close();
            }
        }

        return SMSCount;
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        rdPaytype.SelectedValue = "0";

        Clear();
        lblMsg.Text = "";
        lblMsg.CssClass = "reset";
        fillgrid();
        txtRegNo.Focus();
    }

    private void Clear()
    {
        InAmtCell.Visible = false;
        InDateCell.Visible = false;
        CrAmtCell.Visible = false;
        CrDateCell.Visible = false;
        txtRegNo.Text = "";
        txtTagNo.Text = "";
        txtTagNo.Enabled = true;
        txtBillAmount.Text = "";
        txtPartsAmount.Text = "";
        txtLabourAmount.Text = "";
        txtRemarks.Text = "";
        lblRefNo.Text = "0";
        txt_VasAmt.Text = "";
        txt_LubeAmt.Text = "";
        txt_creditAmt.Text = "";
        Credit_date.Text = "";
        txtInsAmt.Text = "";
        txtInsDate.Text = "";
    }

    private bool GetTotal(Decimal BillAmount, Decimal PartsAmount, Decimal LabourAmount,Decimal VasAmt,Decimal LubeAmt)
    {
        Decimal Total = PartsAmount + LabourAmount+VasAmt+ LubeAmt;
        if (BillAmount == Total)
        {
            return false;
        }
        else
        {
            return true;
        }
    }


    private void GetSMSCountlabel()
    {
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        string SMSCount = "";
        try
        {
            // SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
            SqlCommand cmd = new SqlCommand("", con);
            cmd.CommandText = "udpGetSMSCount";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                // lblMaxCount.Text = "30000";
                lblMaxCount.Text = Convert.ToInt32(GetSMSLimitCount()).ToString();
                lblConsumedCount.Text = dt.Rows[0]["SMSCount"].ToString();
                lblRemained.Text = (Convert.ToInt32(lblMaxCount.Text) - Convert.ToInt32(lblConsumedCount.Text)).ToString();
            }
        }
        catch (Exception ex)
        {

        }
        finally
        {
            if (con.State != ConnectionState.Closed)
            {
                con.Close();
            }
        }


    }


    protected void ImageButton2_Click1(object sender, ImageClickEventArgs e)
    {
        lblMsg.Text = "";
        lblMsg.CssClass = "reset";
        if (txtTagNo.Text.ToString() == "")
        {
            lblMsg.Text = "Please enter Tag No..!";
            lblMsg.CssClass = "ErrMsg";
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");

        }
        else
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString()))
                {
                    SqlCommand cmd1 = new SqlCommand("GetRegnoinCashier", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@RFID", txtTagNo.Text.ToString());
                    SqlDataAdapter sda = new SqlDataAdapter(cmd1);
                    DataTable dt = new DataTable();
                    con.Open();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        txtRegNo.Text = dt.Rows[0]["RegNo"].ToString();
                    }
                    else
                    {

                        lblMsg.Text = "There is no vehicle with this TagNo or already done..!";
                        lblMsg.CssClass = "ErrMsg";
                        ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");

                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }

    protected void txtRemarks_TextChanged1(object sender, EventArgs e)
    {
        lblMsg.CssClass = "reset";
        lblMsg.Text = "";
        if ((!string.IsNullOrEmpty(txtPartsAmount.Text)) || (!string.IsNullOrEmpty(txtLabourAmount.Text)))
        {
            txtBillAmount.Text = (Convert.ToDouble(txtPartsAmount.Text) + Convert.ToDouble(txtLabourAmount.Text)).ToString();
        }

    }






    //    protected void grdCashier_RowCommand(object sender, GridViewCommandEventArgs e)
    //    {

    //        if (e.CommandName=="Cancel")
    //        {
    //            if (CmbCancelationRemarks.SelectedIndex == 0)
    //            {
    //                lblmsg.Text = "Please Add Remarks.";
    //                lblmsg.CssClass = "ErrMsg";
    //                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

    //            }
    //            else
    //            {
    //                SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
    //                try
    //                {
    //                    string Remarks = ((CmbCancelationRemarks.SelectedValue.Trim() == "-1") ? txtCancelationRemark.Text.Trim() : CmbCancelationRemarks.SelectedItem.Text);

    //                    if (con.State != ConnectionState.Open)
    //                        con.Open();

    //                    string str = "udpTagCancelationinCRM";
    //                    SqlCommand cmd = new SqlCommand(str, con);
    //                    cmd.CommandType = CommandType.StoredProcedure;
    //                    cmd.Parameters.AddWithValue("@TagNo", lblTagNo.Text.ToString());
    //                    cmd.Parameters.AddWithValue("@EmpId", EmpId);
    //                    cmd.Parameters.AddWithValue("@Reason", Remarks);
    //                    SqlParameter Flag = cmd.Parameters.Add("@Flag", SqlDbType.VarChar, 100);
    //                    Flag.Direction = ParameterDirection.Output;
    //                    Flag.Value = "";
    //                    cmd.ExecuteNonQuery();

    //                    lblmsg.Text = Flag.Value.ToString();
    //                    lblmsg.CssClass = "ScsMsg";
    //                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
    //                    if (lblmsg.Text.Contains("Successfully"))
    //                    {
    //                        SendDataForCX10(lblSARegNo.Text.ToString().ToUpperInvariant(), "0", "0", "0");

    //                    }
    //                    txtCancelationRemark.Visible = false;
    //                    CmbCancelationRemarks.SelectedIndex = 0;
    //                    txtCancelationRemark.Text = "";
    //                }
    //                catch (Exception ex) { }
    //                finally
    //                {
    //                    if (con.State != ConnectionState.Closed)
    //                        con.Close();
    //                }

    //        }
    //    }
    //}

    protected void txt_VasAmt_TextChanged(object sender, EventArgs e)
    {
        
        if (txt_VasAmt.Text =="" )
        {
            txtBillAmount.Text = (Convert.ToDouble(txtBillAmount.Text)).ToString();
        }
        else if ((!string.IsNullOrEmpty(txtPartsAmount.Text)) && (!string.IsNullOrEmpty(txtLabourAmount.Text) && (!string.IsNullOrEmpty(txt_VasAmt.Text))))
        {
            txtBillAmount.Text = (Convert.ToDouble(txtPartsAmount.Text) + Convert.ToDouble(txtLabourAmount.Text)+ Convert.ToDouble(txt_VasAmt.Text)).ToString();
        }
        txt_LubeAmt.Focus();
    }

    protected void txt_LubeAmt_TextChanged(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txt_LubeAmt.Text))
        {
            txtBillAmount.Text = (Convert.ToDouble(txtLabourAmount.Text)).ToString();
        }
        else if ((!string.IsNullOrEmpty(txtPartsAmount.Text)) && (!string.IsNullOrEmpty(txtLabourAmount.Text) && (!string.IsNullOrEmpty(txtLabourAmount.Text))))
        {
            txtBillAmount.Text = (Convert.ToDouble(txtPartsAmount.Text) + Convert.ToDouble(txtLabourAmount.Text) + Convert.ToDouble(txt_VasAmt.Text) + Convert.ToDouble(txt_LubeAmt.Text)).ToString();
        }
    }


    protected void rdPaytype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdPaytype.SelectedItem.Text.Contains("Credit"))
        {
           

            foreach (ListItem lstItm in rdSrcType.Items)
            {
                if (lstItm.Text.Contains("Insurance") || lstItm.Text.Contains("Warranty"))
                {
                    lstItm.Selected = false;
                    lstItm.Enabled = false;
                    lstItm.Attributes.Add("style", "color:#999;");
                    //break;
                }
                rdSrcType.Items[0].Selected = true;
            }
            CrAmtCell.Visible = true;
            CrDateCell.Visible = true;
            InAmtCell.Visible = false;
            InDateCell.Visible = false;
            ChqRefCell.Visible = false;
            EmptyCell.Visible = true;
        }
      
        else if (rdPaytype.SelectedItem.Text.Contains("Cheque / DD"))
        {
            EmptyCell.Visible = false;
            ChqRefCell.Visible = true;
            InAmtCell.Visible = false;
            InDateCell.Visible = false;
            CrAmtCell.Visible = false;
            CrDateCell.Visible = false;
        }
        else
        {
            foreach (ListItem lstItm in rdSrcType.Items)
            {
                lstItm.Enabled = true;
            }
            InAmtCell.Visible = false;
            InDateCell.Visible = false;
            CrAmtCell.Visible = false;
            CrDateCell.Visible = false;

        }
    }

   

    protected void rdSrcType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdSrcType.SelectedItem.Text.Contains("Insurance"))
        {
            InAmtCell.Visible = true;
            InDateCell.Visible = true;
            CrAmtCell.Visible = false;
            CrDateCell.Visible = false;
            ChqRefCell.Visible = false;
            EmptyCell.Visible = true;
            lblMsg.Text = "";
            lblMsg.CssClass = "reset";
        }
        else
        {
            InAmtCell.Visible = false;
            InDateCell.Visible = false;
        }
    }
}