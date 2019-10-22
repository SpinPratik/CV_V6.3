using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DisplayWorks : System.Web.UI.Page
{
    private static string BackTo = "";
    
    private static string EmpId = "0";
    private static int fblank = 0;
    private static int miniTabIndex = 6;
    private static string statusVal = "";

    public static DataTable GetInOutTime(string serviceID, string ProcessName)
    {
        DataTable dt = new DataTable();
        SqlConnection con = new SqlConnection(System.Web.HttpContext.Current.Session[HttpContext.Current.Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlCommand cmd = new SqlCommand("GetProcessInOutTime", con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Serviceid", serviceID);
        cmd.Parameters.AddWithValue("@ProcessName", ProcessName);
        if (con.State != ConnectionState.Open)
            con.Open();
        cmd.ExecuteNonQuery();
        sda.Fill(dt);
        if (con.State != ConnectionState.Closed)
            con.Close();
        return dt;
    }

    protected void BindGrid()
    {
        List<TMLCRMDisplay> crmData = new List<TMLCRMDisplay>();
        List<TMLCRMDisplay> HDt = new List<TMLCRMDisplay>();
        //DisplayDt.Clear();
        crmData = GetCRMData(rbType.SelectedValue, txtVehicleNumber.Text, miniTabIndex + 1);
        HDt = crmData;
        if (crmData.Count == 0)
        {
            grdDisplay.DataSource = null;
            grdDisplay.DataBind();
            GridView1.DataSource = null;
        }
        else
        {
            grdDisplay.GridLines = GridLines.Horizontal;
            GridView1.DataSource = HDt;
            GridView1.DataBind();
            grdDisplay.DataSource = crmData;
            grdDisplay.DataBind();
        }

        fblank = 0;
        //for (int fbt = 0; fbt < HDt.Rows.Count; fbt++)
        //{
        //    if (HDt.Rows[fbt][5].ToString().Trim() != "")
        //    {
        //        fblank = 1;
        //    }
        //}
        FillVehicleStatus();
        lbVCount.Text = crmData.Count.ToString();
    }

    protected void btn_Back_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["ROLE"].ToString() == "WORK MANAGER")
        {
            Response.Redirect("JCHome.aspx");
        }
        else if (Session["ROLE"].ToString() == "DISPLAY" && Request.Url.ToString().Contains("DisplayWorks.aspx") && Session["BACKROLE"].ToString() == "SM")
        {
            Response.Redirect("DisplayHome.aspx?Back=222");
        }
        else if (Session["ROLE"].ToString() == "DISPLAY" && Request.Url.ToString().Contains("DisplayWorks.aspx?Back=123") && Session["BACKROLE"].ToString() == "GMSERVICE")
        {
            Response.Redirect("DisplayHome.aspx?Back=333");
        }
        else if (Session["ROLE"].ToString() == "DISPLAY" && Request.Url.ToString().Contains("DisplayWorks.aspx?Back=123") && Session["BACKROLE"].ToString() == "DISPLAY")
        {
            Response.Redirect("DisplayHome.aspx");
        }
        else if (Session["ROLE"].ToString() == "CRM")
        {
            Response.Redirect("CRMHome.aspx");
        }
        else if (Session["ROLE"].ToString() == "SM" && Request.Url.ToString().Contains("DisplayWorks.aspx?Back=123"))
        {
            Response.Redirect("SADisplayHome.aspx");
        }
        else if (Session["ROLE"].ToString() == "GMSERVICE" && Request.Url.ToString().Contains("DisplayWorks.aspx?Back=123") && Session["BACKROLE"].ToString() == "GMSERVICE")
        {
            Response.Redirect("DisplayHome.aspx?Back=333");
        }
    }

    protected void btn_JCCUpdate_Click(object sender, EventArgs e)
    {
        lblmsg.Attributes.Add("style", "text-transform:none !important");
        if (rdJC_Yes.Checked == true)
        {
            string sConnString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
            SqlConnection con = new SqlConnection(sConnString);
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();

                SqlCommand cmd = new SqlCommand("udpUpdateJCCInformed", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@VehicleNo", lblJCCvehno.Text.ToString().Trim());
                cmd.Parameters.AddWithValue("@UserName", Session["UserId"].ToString());

                SqlParameter msg = cmd.Parameters.Add("@Flag", SqlDbType.VarChar, 50);
                msg.Direction = ParameterDirection.Output;
                msg.Value = "";
                cmd.ExecuteNonQuery();
                lblmsg.Text = msg.Value.ToString();
                
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
            catch (Exception ex) { }
            finally
            {
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
        }
    }

    protected void btn_logout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();
        Response.Redirect("login.aspx");
    }

    //protected void btnBACK_Click(object sender, EventArgs e)
    //{
    //    Session["CURRENT_PAGE"] = null;
    //    if (Session["ROLE"].ToString() == "WORK MANAGER")
    //    {
    //        Response.Redirect("JCHome.aspx");
    //    }
    //    else if (Session["ROLE"].ToString() == "FRONT OFFICE")
    //    {
    //        Response.Redirect("FrontOfficeHome.aspx");
    //    }
    //    else if (Session["ROLE"].ToString() == "SERVICE ADVISOR")
    //    {
    //        Response.Redirect("SAHome.aspx");
    //    }
    //    else if (Session["ROLE"].ToString() == "CRM")
    //    {
    //        Response.Redirect("CRMHome.aspx");
    //    }
    //    else if (Session["ROLE"].ToString() == "DISPLAY")
    //    {
    //        Response.Redirect("DisplayHome.aspx");
    //    }
    //    else if (BackTo == "GMSERVICE")
    //    {
    //        Response.Redirect("DisplayHome.aspx?Back=333", false);
    //    }
    //    else if (BackTo == "SM")
    //    {
    //        Response.Redirect("DisplayHome.aspx?Back=222", false);
    //    }
    //}

    protected void btnc_Click(object sender, EventArgs e)
    {
        TabContainer1.Visible = false;
    }

    protected void btnCancelation_Click(object sender, EventArgs e)
    {
        lblmsg.Attributes.Add("style", "text-transform:none !important");
        try
        {
            if (lblvcvehicleno.Text.Trim() == "")
            {
                lblmsg.Text = "There Is No Vehicle Available For Cancellation.!";
                lblmsg.CssClass = "ErrMsg";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

            }
            else if (txtcncl.Text.Trim() == "")
            {
                lblmsg.Text = "Please Enter Reason For Cancellation.!";
                lblmsg.CssClass = "ErrMsg";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

            }
            else
            {
                string sConnString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
                SqlConnection con = new SqlConnection(sConnString);
                con.Open();
                string str3 = "update tblMaster set Cancelation='" + true + "',CancelationReason='" + txtcncl.Text.Trim() + "',CancelationDateTime='" + DateTime.Now.ToString() + "' where RegNo='" + lblvcvehicleno.Text.Trim().Replace("<font size='4' color='red'> *</font>", "") + "'";
                SqlCommand cmd3 = new SqlCommand(str3, con);
                cmd3.ExecuteNonQuery();
                con.Close();
                lblmsg.Text = "Vehicle Canceled Successfully";
                lblmsg.CssClass = "ScsMsg";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

                BindGrid();
                txtcncl.Text = "";
                lblvcvehicleno.Text = "";
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void BtnClosePopup_Click(object sender, EventArgs e)
    {
        //Timer1.Enabled = true;
    }

    protected void btncncl_Click(object sender, EventArgs e)
    {
        txtcardno.Text = "";
        txtnewcrdno.Text = "";
        lblvehicleUPDTag.Text = "";
    }

    protected void btnecncl_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        lblmsg.CssClass = "reset";
        TabContainer1.Visible = false;
    }

    protected void btneupd_Click(object sender, EventArgs e)
    {
        int ans = validate_vrn(txtenewvhno.Text.Trim());
        lblmsg.Attributes.Add("style", "text-transform:none !important");
        string sConnString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        SqlConnection con = new SqlConnection(sConnString);
        try
        {
            lblmsg.Text = "";
            lblmsg.CssClass = "reset";
            if (txtenewvhno.Text.Trim() == string.Empty)
            {
                lblmsg.Text = "Empty New Registration No#";
                lblmsg.CssClass = "ErrMsg";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

            }
            else if (ans == 1)
            {
                lblmsg.Text = "VRN/VIN must be lessthan 10 digit";
                lblmsg.CssClass = "ErrMsg";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
            }
            else if (ans == 2)
            {
                lblmsg.Text = "VRN/VIN invalid State Code";
                lblmsg.CssClass = "ErrMsg";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
            }
            else if (ans != 0)
            {
                lblmsg.Text = "VRN/VIN is invalid";
                lblmsg.CssClass = "ErrMsg";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
            }
            else
            {
                if (txtenewvhno.Text.Trim() != txtevehno.Text.Trim())
                {
                 
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    string str = "SELECT * FROM tblMaster Where RegNo='" + txtevehno.Text.Trim() + "' And Delivered=0 And Cancelation=0";
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
                            string str3 = "Update tblMaster set RegNo='" + txtenewvhno.Text.Trim().ToUpperInvariant() + "' where SlNo='" + dt.Rows[0][0].ToString() + "'";
                            SqlCommand cmd3 = new SqlCommand(str3, con);
                            cmd3.ExecuteNonQuery();
                            lblmsg.Text = "Updated Successfully";
                            lblmsg.CssClass = "ScsMsg";
                            SendDatatoCX10(Convert.ToInt16(dt.Rows[0][0].ToString()), txtenewvhno.Text.ToString().Trim().ToUpperInvariant());

                              ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

                            txtevehno.Text = txtenewvhno.Text;
                            txtenewvhno.Text = "";
                            BindGrid();
                          //  btnecncl_Click(null, null);
                        }
                        else
                        {
                            lblmsg.Text = "Registration No. Already In For Service";
                            lblmsg.CssClass = "ErrMsg";
                            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

                        }
                    }
                    else
                    {
                        lblmsg.Text = "Registration No. not Exist";
                        lblmsg.CssClass = "ErrMsg";
                        ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

                    }
                }
                else
                {
                    lblmsg.Text = "Both Registration No. Same";
                    lblmsg.CssClass = "ErrMsg";
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

                }
            }
        }
        catch (Exception ex)
        {
            lblmsg.CssClass = "ErrMsg";
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

            lblmsg.Text = "Plesse try again later!";
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }

    protected void btnHold_Click(object sender, EventArgs e)
    {
        lblmsg.Attributes.Add("style", "text-transform:none !important");
        lblmsg.Text = "";
        try
        {
            if (lblTagnoforHold.Text != "")
            {
                string sConnString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
                SqlConnection oConn = new SqlConnection(sConnString);
                SqlCommand cmd = new SqlCommand("udpVehicleHoldOrUnHold", oConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@VehicleHold", DateTime.Now);
                cmd.Parameters.AddWithValue("@Refno", lblSlnoforHold.Text.Trim());
                SqlParameter msg = cmd.Parameters.Add("@msg", SqlDbType.VarChar, 50);
                SqlParameter flag = cmd.Parameters.Add("@Flag", SqlDbType.Int);
                flag.Direction = ParameterDirection.Output;
                flag.Value = "";
                msg.Direction = ParameterDirection.Output;
                msg.Value = "";
                oConn.Open();
                cmd.ExecuteNonQuery();
                if (flag.Value.ToString() == "0")
                {
                    lblmsg.Text = msg.Value.ToString();
                    lblmsg.CssClass = "ErrMsg";
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

                }
                else
                {
                    lblmsg.Text = msg.Value.ToString();
                    lblmsg.CssClass = "ScsMsg";
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

                    lblSlnoforHold.Text = "";
                    lblTagnoforHold.Text = "";
                    lblVehnoforHold.Text = "";
                }

                oConn.Close();
                BindGrid();
            }
            else
            {
                lblmsg.Text = "Vehicle No# is not provided";
                lblmsg.CssClass = "ErrMsg";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message.ToString();
            lblmsg.CssClass = "ErrMsg";
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

        }
        finally
        {
        }
    }

    protected void btnLoad_Click(object sender, EventArgs e)
    {
        lblmsg.Attributes.Add("style", "text-transform:none !important");
        try
        {
            if (TxtDate1.Text.Trim() == "" || TxtDate2.Text.Trim() == "" || txtVehicleNumber.Text.Trim() == "" || txtTagNo.Text.Trim() == "")
            {
                lblmsg.Text = "Please enter date range or Vehicle No# to search";
                lblmsg.CssClass = "ErrMsg";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

                TxtDate1.Text = "";
                TxtDate2.Text = "";
                txtVehicleNumber.Text = "";
                txtTagNo.Text = "";
            }
            else
            {
                lblmsg.Text = "";
                lblmsg.CssClass = "reset";
            }
            if (txtVehicleNumber.Text.Trim() != "")
            {
                List<TMLCRMDisplay> Dt = new List<TMLCRMDisplay>();
                Dt = GetCRMData(rbType.SelectedValue, txtVehicleNumber.Text, miniTabIndex + 1);
                GridView1.DataSource = Dt;
                GridView1.DataBind();
                grdDisplay.DataSource = Dt;
                grdDisplay.DataBind();

                TxtDate1.Text = "";
                TxtDate2.Text = "";
            }
            else if (TxtDate1.Text.Trim() != "" && TxtDate2.Text.Trim() != "")
            {
                List<TMLCRMDisplay> Dt = new List<TMLCRMDisplay>();
                Dt = GetCRMData(rbType.SelectedValue, txtVehicleNumber.Text, miniTabIndex + 1);
                GridView1.DataSource = Dt;
                GridView1.DataBind();
                grdDisplay.DataSource = Dt;
                grdDisplay.DataBind();
                txtVehicleNumber.Text = "";
                txtTagNo.Text = "";
            }
        }
        catch (Exception ex)
        {
            lblLoading.Text = ex.Message;
        }
    }

    protected void btnOut_Click(object sender, EventArgs e)
    {
        lblmsg.Attributes.Add("style", "text-transform:none !important");
        try
        {
            if (lblvovehicleno.Text != "")
            {
                if (ddVOutRemarks.SelectedValue.Trim() == "-1" && txt_VORemarks.Text.Trim() == "")
                {
                    lblmsg.Text = "Please add Remarks.";
                    lblmsg.CssClass = "ErrMsg";
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

                    return;
                }
                string sConnString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
                SqlConnection oConn = new SqlConnection(sConnString);
                string str = "Select RFID,Slno from tblMaster where RegNo='" + lblvovehicleno.Text.Trim().Replace("<font size='4' color='red'> *</font>", "") + "' And Delivered=0";
                SqlCommand cmd = new SqlCommand(str, oConn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    str = "VehicleOut";
                    cmd = new SqlCommand(str, oConn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RefNo", dt.Rows[0][1].ToString());
                    cmd.Parameters.AddWithValue("@EnrollmentNo", dt.Rows[0][0].ToString());
                    if (ddVOutRemarks.SelectedItem.Text.ToString().Trim() != "Other")
                    {
                        cmd.Parameters.AddWithValue("@VOoutRemarks", ddVOutRemarks.SelectedItem.Text.ToString().Trim());
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@VOoutRemarks", txt_VORemarks.Text.ToString().Trim());
                    }
                    if (oConn.State != ConnectionState.Open)
                        oConn.Open();
                    cmd.ExecuteNonQuery();
                    if (oConn.State != ConnectionState.Closed)
                        oConn.Close();
                    lblmsg.Text = "Vehicle Out Successfully";
                    lblmsg.CssClass = "ScsMsg";
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

                    lblvovehicleno.Text = "";
                    txt_VORemarks.Text = "";
                    BindGrid();
                    btnecncl_Click(null, null);
                }
                else
                {
                    lblmsg.Attributes.Add("style", "text-transform:none !important");
                    lblmsg.Text = "Vehicle No#. not found";
                    lblmsg.CssClass = "ErrMsg";
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

                }
            }
            else
            {
                lblmsg.Text = "Vehicle No## is not provided";
                lblmsg.CssClass = "ErrMsg";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void btnPDTsave_Click(object sender, EventArgs e)
    {
        lblmsg.Attributes.Add("style", "text-transform:none !important");
        lblmsg.Text = "";
        if (cmbHH.Text=="")
        {
            lblmsg.Text = "Enter RPDT time";
            lblmsg.CssClass = "ErrMsg";
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
        }
        else { 
        try
        {
            if (lblpdtvehno.Text.Trim() != "")
            {
                string tim = txtRevPDT.Text + " " + DateTime.Parse(cmbHH.Text).ToShortTimeString();
                DateTime dat = new DateTime();
                if (DateTime.TryParse(tim, out dat))
                {
                    if (DateTime.Compare(DateTime.Parse(tim), DateTime.Now) < 0)
                    {
                        lblmsg.Text = "Date and Time must be <br> greater than or todays date";
                        lblmsg.CssClass = "ErrMsg";
                        ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

                    }
                    else
                    {
                        if (txtRevPDT.Text.Trim() != "" && cmbHH.Text.Trim() != "")
                        {
                            if (txtPDTComment.Text.Trim() != "" || ddPDTRemarks.SelectedValue.Trim() != "0")
                            {
                                if (rd_Yes.Checked != false || rd_No.Checked != false)
                                {
                                    int custInfo = 0;
                                    string username = "";
                                    if (rd_Yes.Checked == true)
                                    {
                                        custInfo = 1;
                                        username = Session["UserId"].ToString();
                                    }
                                    DateTime pt = Convert.ToDateTime(txtRevPDT.Text.Trim() + " " + cmbHH.Text.Trim());
                                    string sConnString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
                                    SqlConnection oConn = new SqlConnection(sConnString);
                                    SqlCommand cmd = new SqlCommand("UpdateTblMasterRPDT", oConn);
                                    cmd.Parameters.AddWithValue("@RevisedPromisedTime", pt);
                                    cmd.Parameters.AddWithValue("@RegNo", lblpdtvehno.Text.Trim().Replace("<font size='4' color='red'> *</font>", ""));
                                    if (ddPDTRemarks.SelectedItem.Text.ToString().Trim() == "Other")
                                    {
                                        cmd.Parameters.AddWithValue("@Comment", txtPDTComment.Text.Trim());
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@Comment", ddPDTRemarks.SelectedItem.Text.ToString().Trim());
                                    }

                                    cmd.Parameters.AddWithValue("@RPDTInformed", custInfo);
                                    cmd.Parameters.AddWithValue("@UserName", username);

                                    SqlParameter spm = new SqlParameter("@msg", SqlDbType.VarChar, 50);
                                    spm.Direction = ParameterDirection.Output;
                                    spm.Value = "";
                                    cmd.Parameters.Add(spm);
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    oConn.Open();
                                    cmd.ExecuteNonQuery();
                                    lblmsg.Text = "Revised PDT saved successfully";
                                    lblmsg.CssClass = "ScsMsg";

                                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                                        cmbHH.Text = "";
                                        rd_Yes.Checked = false;
                                        rd_Yes.Checked = false;
                                        ddPDTRemarks.SelectedIndex = 1;
                                        oConn.Close();
                                    txtRevPDT.Text = DateTime.Now.ToShortDateString();
                                    BindGrid();
                                    txtPDTComment.Text = "";
                                    rd_No.Checked = false;
                                    rd_Yes.Checked = false;
                                }
                                else
                                {
                                    lblmsg.Text = "Please select customer informed Yes or No";
                                    lblmsg.CssClass = "ErrMsg";
                                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

                                }
                            }
                            else
                            {
                                lblmsg.Text = "Please enter RPDT reason";
                                lblmsg.CssClass = "ErrMsg";
                                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

                            }
                        }
                        else
                        {
                            lblmsg.Text = "Please enter Revised PDT and Time";
                            lblmsg.CssClass = "ErrMsg";
                            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

                        }
                    }
                }
                else
                {
                    lblmsg.Text = "Invalid Date provided.";
                    lblmsg.CssClass = "ErrMsg";
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

                }
            }
            else
            {
                lblmsg.Text = "Vehicle No# not provided!";
                lblmsg.CssClass = "ErrMsg";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

            }
        }
        catch (Exception ex)
        {
        }
        }
    }

    protected void btnready_Click(object sender, EventArgs e)
    {
        lblmsg.Attributes.Add("style", "text-transform:none !important");
        lblmsg.Text = "";
        lblmsg.CssClass = "reset";
        try
        {
            if (lblvehicleno.Text != "")
            {
                string sConnString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
                SqlConnection oConn = new SqlConnection(sConnString);
                SqlCommand cmd = new SqlCommand("UpdateTblMasterVehicleReady", oConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@VehicleReady", DateTime.Now);
                cmd.Parameters.AddWithValue("@RegNo", lblvehicleno.Text.Trim().Replace("<font size='4' color='red'> *</font>", ""));
                oConn.Open();
                cmd.ExecuteNonQuery();
                lblmsg.Text = "Vehicle ready done";
                lblmsg.CssClass = "ScsMsg";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

                oConn.Close();
                BindGrid();
            }
            else
            {
                lblmsg.Text = "Vehicle No# is not provided";
                lblmsg.CssClass = "ErrMsg";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        try
        {
            List<TMLCRMDisplay> Dt = new List<TMLCRMDisplay>();
            Dt = GetCRMData(rbType.SelectedValue, txtVehicleNumber.Text, miniTabIndex + 1);
            GridView1.DataSource = Dt;
            GridView1.DataBind();
            grdDisplay.DataSource = Dt;
            grdDisplay.DataBind();
        }
        catch (Exception ex)
        {
        }
    }

    protected void btnRefresh_Click1(object sender, EventArgs e)
    {
        TxtDate1.Text = "";
        TxtDate2.Text = "";
        txtVehicleNumber.Text = "";
        cmbProcess.SelectedIndex = -1;
        cmbVehicleModel.SelectedIndex = -1;
        cmbServiceType.SelectedIndex = -1;
        cmbCustType.SelectedIndex = -1;
        cmbTeamLead.SelectedIndex = -1;
        cmbSA.SelectedIndex = -1;
        txtTagNo.Text = "";
        lblmsg.Text = "";
        lblmsg.CssClass = "reset";
        lblTagNo.Text = "";
        BindGrid();
    }

    protected void btnRPDTCancel_Click(object sender, EventArgs e)
    {
        rdo_Yes.Checked = false;
        lblmsg.Text = "";
        lblmsg.CssClass = "reset";
        TabContainer1.Visible = false;
    }

    protected void btnRPDTUpd_Click(object sender, EventArgs e)
    {
        lblmsg.Attributes.Add("style", "text-transform:none !important");
        if (rdo_Yes.Checked == true)
        {
            string sConnString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
            SqlConnection con = new SqlConnection(sConnString);
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                SqlCommand cmd = new SqlCommand("udpUpdateRPDTInformed", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@VehicleNo", lblvehno.Text.ToString().Trim());
                cmd.Parameters.AddWithValue("@UserName", Session["UserId"].ToString());

                SqlParameter msg = cmd.Parameters.Add("@flag", SqlDbType.VarChar, 50);
                msg.Direction = ParameterDirection.Output;
                msg.Value = "";
                cmd.ExecuteNonQuery();
                lblmsg.Text = msg.Value.ToString();
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
            catch (Exception ex) { }
            finally
            {
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
        }
    }

    protected void btnrrcncl_Click(object sender, EventArgs e)
    {
        TabContainer1.Visible = false;
    }

    protected void btnSAUpdate_Click(object sender, EventArgs e)
    {
        lblmsg.Attributes.Add("style", "text-transform:none !important");
        if (ddlSAList.SelectedItem.Text.ToString() != "--Select--")
        {
            SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();

                SqlCommand cmd = new SqlCommand("UdpSAMappingInCrm", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpId", ddlSAList.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@RefNo", lblSlNoSA.Text.ToString());
                cmd.Parameters.AddWithValue("@Remarks", txtSARemarks.Text.ToString());
                SqlParameter flag = cmd.Parameters.Add("@Flag", SqlDbType.Int);
                SqlParameter msg = cmd.Parameters.Add("@msg", SqlDbType.VarChar, 50);
                flag.Direction = ParameterDirection.Output;
                flag.Value = "";
                msg.Direction = ParameterDirection.Output;
                msg.Value = "";
                cmd.ExecuteNonQuery();
                if (flag.Value.ToString() == "0")
                {
                    lblmsg.CssClass = "ErrMsg";
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

                    lblmsg.Text = msg.Value.ToString();
                }
                else
                {
                    lblmsg.CssClass = "ScsMsg";
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

                    lblmsg.Text = msg.Value.ToString();
                    txtSARemarks.Text = "";
                    lblSlNoSA.Text = "";
                    getSACRM("");

                    // btnecncl_Click(null, null);
                }

                if (con.State != ConnectionState.Closed)
                    con.Close();
              
            }
            catch (Exception ex) { }
            finally
            {
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
        }
        else
        {
            
            lblmsg.Text = "Please select Service Advisor..!";
            lblmsg.CssClass = "ErrMsg";
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
            
        }
    }
    public void clear()
    {
        txtSARemarks.Text = "";
        txtenewvhno.Text = "";
        txtnewcrdno.Text = "";
        txtServiceAction.Text = "";
        txtRecomendation.Text = "";
        txtspremarks.Text = "";
        txtCancelationRemark.Text = "";
        txt_VORemarks.Text = "";

    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        lblmsg.Attributes.Add("style", "text-transform:none !important");
        try
        {
            lblmsg.Text = "";
            cmbCustType.SelectedIndex = -1;
            cmbServiceType.SelectedIndex = -1;
            cmbVehicleModel.SelectedIndex = -1;
            cmbProcess.SelectedIndex = -1;
            cmbSA.SelectedIndex = -1;
            cmbTeamLead.SelectedIndex = -1;
            ddlState.SelectedIndex = -1;
            int[] cs = { 0, 0, 0, 0 };

            if (txtVehicleNumber.Text.Trim() != "")
            {
                cs[2] = 1;
                TxtDate1.Text = "";
                TxtDate2.Text = "";
                txtTagNo.Text = "";
            }
            if (txtTagNo.Text.Trim() != "")
            {
                cs[3] = 1;
                TxtDate1.Text = "";
                TxtDate2.Text = "";
                txtVehicleNumber.Text = "";
            }
            if (TxtDate1.Text.Trim() != "")
                cs[0] = 1;
            if (TxtDate2.Text.Trim() != "")
                cs[1] = 1;
            if (cs[0] == 0 && cs[1] == 0 && (cs[2] == 1 || cs[3] == 1))
            {
                BindGrid();
                TxtDate1.Text = "";
                TxtDate2.Text = "";
                TabContainer1.Visible = false;
            }
            else if (cs[0] == 1 && cs[1] == 1 && cs[2] == 0 && cs[3] == 0)
            {
                BindGrid();
                txtVehicleNumber.Text = "";
                txtTagNo.Text = "";
                TabContainer1.Visible = false;
            }
            else if (cs[0] == 1 && cs[1] == 0)
            {
                lblmsg.Text = "Please select To Date";
                lblmsg.CssClass = "ErrMsg";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

            }
            else if (cs[0] == 0 && cs[1] == 1)
            {
                lblmsg.Text = "Please select From Date";
                lblmsg.CssClass = "ErrMsg";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

            }
            else if (cs[0] == 0 && cs[1] == 0 && cs[2] == 0 && cs[3] == 0)
            {
                lblmsg.Text = "Please select Date Range or Vehicle No# or Tag No";
                lblmsg.CssClass = "ErrMsg";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

            }
        }
        catch (Exception ex)
        {
            lblLoading.Text = ex.Message;
        }
    }

    protected void btnspsave_Click(object sender, EventArgs e)
    {
        lblmsg.Attributes.Add("style", "text-transform:none !important");
        lblmsg.Text = "";
        try
        {
            if (lblspvehicleno.Text.Trim() != "")
            {
                if (txtspremarks.Text.Trim() != "" || txtspremarks.Visible == false)
                {
                    SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
                    SqlCommand cmd3 = new SqlCommand("", con);
                    cmd3.CommandText = "Select TOP 1 SlNo From TblMaster Where Regno = '" + lblspvehicleno.Text.Trim().Replace("<font size='4' color='red'> *</font>", "").Trim() + "' AND Position <> 'Delivered' order by SlNo Desc";
                    SqlDataAdapter da = new SqlDataAdapter(cmd3);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        lblservid.Text = dt.Rows[0]["Slno"].ToString();
                    }
                    if (drpsptype.SelectedIndex == 0)
                    {
                        string sConnString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
                        SqlConnection oConn = new SqlConnection(sConnString);
                        SqlCommand cmd = new SqlCommand("InsertTblRemarks", oConn);
                        cmd.Parameters.AddWithValue("@RefNo", lblservid.Text.Trim());
                        if (txtspremarks.Visible == true)
                            cmd.Parameters.AddWithValue("@Comment", txtspremarks.Text.Trim());
                        else if (ddlSRemarks.SelectedItem.Text == "Select")
                        {
                            lblmsg.Text = "Please add Remarks.";
                            lblmsg.CssClass = "ErrMsg";
                            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

                            return;
                        }
                        else
                            cmd.Parameters.AddWithValue("@Comment", ddlSRemarks.SelectedItem.Text.Trim());
                        cmd.Parameters.AddWithValue("@DTM", DateTime.Now);
                        cmd.CommandType = CommandType.StoredProcedure;
                        oConn.Open();
                        cmd.ExecuteNonQuery();
                        lblmsg.Text = "Saved Successfully";
                        lblmsg.CssClass = "ScsMsg";
                        ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

                        oConn.Close();
                        txtspremarks.Text = "";
                        if (txtServiceAction.Text.Trim() != "" || txtRecomendation.Text.Trim() != "")
                        {
                            oConn.Open();
                            cmd = new SqlCommand("UpdateTblTimeMonitoring", oConn);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ActionTaken", txtServiceAction.Text.Trim());
                            cmd.Parameters.AddWithValue("@Recommendation", txtRecomendation.Text.Trim());
                            cmd.Parameters.AddWithValue("@Regno", lblspvehicleno.Text.Trim().Replace("<font size='4' color='red'> *</font>", ""));
                            cmd.ExecuteNonQuery();
                            lblmsg.Text = "Saved Successfully";
                            lblmsg.CssClass = "ScsMsg";
                            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

                            con.Close();
                            txtServiceAction.Text = "";
                            txtRecomendation.Text = "";
                        }
                    }
                    else if (drpsptype.SelectedIndex == 1)
                    {
                        string sConnString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
                        SqlConnection oConn = new SqlConnection(sConnString);
                        SqlCommand cmd = new SqlCommand("InsertTblProcessRemarks", oConn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ServiceId", lblservid.Text.Trim());
                        cmd.Parameters.AddWithValue("@ProcessId", drpspprocess.SelectedValue);
                        cmd.Parameters.AddWithValue("@DateOfRemarks", DateTime.Now);
                        if (txtspremarks.Visible == true)
                            cmd.Parameters.AddWithValue("@Remarks", txtspremarks.Text.Trim());
                        else if (ddlSRemarks.SelectedItem.Text == "Select")
                        {
                            lblmsg.Text = "Please add Remarks.";
                            lblmsg.CssClass = "ErrMsg";
                            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

                            return;
                        }
                        else
                            cmd.Parameters.AddWithValue("@Remarks", ddlSRemarks.SelectedItem.Text.Trim());
                        oConn.Open();
                        cmd.ExecuteNonQuery();
                        lblmsg.Text = "Saved Successfully";
                        lblmsg.CssClass = "ScsMsg";
                        ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

                        oConn.Close();
                        txtspremarks.Text = "";
                    }
                    else if (drpsptype.SelectedIndex == 2)
                    {
                        string comments = "";
                        string sConnString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
                        SqlConnection oConn = new SqlConnection(sConnString);
                        if (ddlSRemarks.SelectedItem.Text == "Select")
                        {
                            lblmsg.Text = "Please add Remarks.";
                            lblmsg.CssClass = "ErrMsg";
                            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

                        }
                        else
                        {
                            if (ddlSRemarks.SelectedIndex == ddlSRemarks.Items.Count - 1)
                            {
                                if (txtspremarks.Text.Trim() == "")
                                {
                                    lblmsg.Text = "Please add Remarks For other.";
                                    lblmsg.CssClass = "ErrMsg";
                                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

                                    return;
                                }
                                else
                                    comments = txtspremarks.Text.Trim();
                            }
                            else
                            {
                                comments = ddlSRemarks.SelectedItem.Text.Trim();
                            }

                            SqlCommand cmd = new SqlCommand("InsertTblCFRemarks", oConn);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@RefNo", lblservid.Text.Trim());
                            cmd.Parameters.AddWithValue("@Comment", comments);

                            oConn.Open();
                            cmd.ExecuteNonQuery();
                            lblmsg.Text = "Saved Successfully";
                            lblmsg.CssClass = "ScsMsg";
                            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

                            oConn.Close();
                            txtspremarks.Text = "";
                        }
                    }
                    BindGrid();
                    drpsptype.SelectedIndex = -1;
                    ddlSRemarks.SelectedIndex = -1;
                }
                else
                {
                    lblmsg.Text = "Please add Remarks!";
                    lblmsg.CssClass = "ErrMsg";
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

                }
            }
            else
            {
                lblmsg.Text = "Vehicle No# not provided!";
                lblmsg.CssClass = "ErrMsg";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void btnupdate_Click(object sender, EventArgs e)
    {
        lblmsg.Attributes.Add("style", "text-transform:none !important");
        string sConnString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        SqlConnection con = new SqlConnection(sConnString);
        if (txtnewcrdno.Text=="")
        {
            lblmsg.CssClass = "ErrMsg";
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

            lblmsg.Text = "Enter new VRN/VIN";
        }
        else { 
        try
        {
            if (txtcardno.Text.Trim() != txtnewcrdno.Text.Trim() && txtcardno.Text.Trim() != "")
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                SqlCommand cmd1 = new SqlCommand("udpTagUpdationInJCR", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@RefId", GetRefNo(lblvehicleUPDTag.Text.Trim(), Session[Session["TMLDealercode"] + "-TMLConString"].ToString()));
                cmd1.Parameters.AddWithValue("@TagNo", txtcardno.Text.Trim());
                cmd1.Parameters.AddWithValue("@NewTagNo", txtnewcrdno.Text.Trim());
                SqlParameter flag = cmd1.Parameters.Add("@Flag", SqlDbType.VarChar, 75);
                flag.Direction = ParameterDirection.Output;
                flag.Value = "";
                cmd1.ExecuteNonQuery();
                    if (flag.Value.ToString().Contains("Success"))
                    {
                        txtcardno.Text = txtnewcrdno.Text.ToString();
                        ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                        lblmsg.CssClass = "ScsMsg";
                        lblmsg.Text = flag.Value.ToString();
                    }
                    else { 
                    lblmsg.CssClass = "ErrMsg";
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                    lblmsg.Text = flag.Value.ToString();
                    }
                  
                txtnewcrdno.Text = "";
                BindGrid();
            }
            else
            {
              
                lblmsg.Text = "Enter new Tag No#";
                lblmsg.CssClass = "ErrMsg";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                   txtnewcrdno.Focus();
            }
        }
        catch (Exception ex)
        {
                lblmsg.CssClass = "ErrMsg";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

                lblmsg.Text = ex.Message;
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
        }
    }

    
    protected void btnUpdateRFIDCancel_Click(object sender, EventArgs e)
    {
        lblmsg.Attributes.Add("style", "text-transform:none !important");
        if (lblTagNo.Text.ToString() != "")
        {
            if (CmbCancelationRemarks.SelectedIndex == 0)
            {
                lblmsg.Text = "Please add Remarks.";
                lblmsg.CssClass = "ErrMsg";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

            }
            else
            {
                SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
                try
                {
                    string Remarks = ((CmbCancelationRemarks.SelectedValue.Trim() == "-1") ? txtCancelationRemark.Text.Trim() : CmbCancelationRemarks.SelectedItem.Text);

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    string str = "udpTagCancelationinCRM";
                    SqlCommand cmd = new SqlCommand(str, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TagNo", lblTagNo.Text.ToString());
                    cmd.Parameters.AddWithValue("@EmpId", EmpId);
                    cmd.Parameters.AddWithValue("@Reason", Remarks);
                    SqlParameter Flag = cmd.Parameters.Add("@Flag", SqlDbType.VarChar, 100);
                    Flag.Direction = ParameterDirection.Output;
                    Flag.Value = "";
                    cmd.ExecuteNonQuery();

                    lblmsg.Text = Flag.Value.ToString();
                    lblmsg.CssClass = "ScsMsg";
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                    if (lblmsg.Text.Contains("Successfully"))
                    {
                        SendCancelDataForCX10(lblSARegNo.Text.ToString().ToUpperInvariant(), Remarks);

                    }
                    txtCancelationRemark.Visible = false;
                    CmbCancelationRemarks.SelectedIndex = 0;
                    txtCancelationRemark.Text = "";
                }
                catch (Exception ex) { }
                finally
                {
                    if (con.State != ConnectionState.Closed)
                        con.Close();
                }
            }
        }
    }
    public void SendCancelDataForCX10(string RegNo, string Remarks)
    {
        lblmsg.Attributes.Add("style", "text-transform:none !important");
        try
        {
            string UCOde = GetDealerCode();
            var request = (HttpWebRequest)WebRequest.Create("http://v6api.cx100.in/V6/ApiCancel/CancelVehicle");

            var postData = "dealercode=" + UCOde + "&vrn=" + RegNo + "&reference=" + lblrefno.Text+ "&remark=" + Remarks;
            // var postData = "decode=001-SAIL-GRAND&vrn="+RegNo+"&part="+PartsAmount+"&labour="+labouramount+"&total="+Billamount;
            var data = System.Text.Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (System.Net.HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

        }
        catch (Exception ex)
        {

        }


    }
   

    public void SendDatatoCX10(int RefNo, string VehNo)
    {
        try
        {
            string UCOde = GetDealerCode();
            var request = (HttpWebRequest)WebRequest.Create("http://v6api.cx100.in/V6/ApiUpdateVrn/update");

            var postData = "dealercode=" + UCOde + "&vrn=" + VehNo.ToUpperInvariant() + "&reference=" + RefNo;
            // var postData = "decode=001-SAIL-GRAND&vrn="+RegNo+"&part="+PartsAmount+"&labour="+labouramount+"&total="+Billamount;
            var data = System.Text.Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (System.Net.HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

        }
        catch (Exception ex)
        {

        }


    }
    public string GetDealerCode()
    {
        try
        {
            string sConnString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
            SqlConnection con = new SqlConnection(sConnString);
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "udpGetDCodeForCX10";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count != 0)
                return dt.Rows[0][0].ToString();
            else
                return "";
        }
        catch (Exception ex)
        {
            return "";
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        lblmsg.Attributes.Add("style", "text-transform:none !important");
        string sConnString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        SqlConnection con = new SqlConnection(sConnString);
        if (txtmobile.Text!="" && txtmobile.Text.Length<10)
        {
            lblmsg.Text = "Enter valid Mobile No#.";
            lblmsg.CssClass = "ErrMsg";
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

        }
        else { 
        try
        {
           
            Label11.Text = "";
            Label11.CssClass = "reset";
            if (txtvehicle.Text.Trim() != string.Empty)
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                string str = "SELECT Slno FROM tblMaster Where RegNo='" + txtvehicle.Text.Trim() + "' And Delivered=0 And Cancelation=0";
                SqlCommand cmd = new SqlCommand(str, con);
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
                    cmd.Parameters.AddWithValue("@Customerphone", txtmobile.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", txtemailid.Text.Trim());
                    cmd.Parameters.AddWithValue("@SlNo", dt.Rows[0]["Slno"].ToString());
                    try
                    {
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            lblmsg.Text = "Customer details Updated successfully";
                            lblmsg.CssClass = "ScsMsg";
                            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

                            //lblmsg.ForeColor = Color.Green;
                        }
                        else
                        {
                            lblmsg.Text = "Update aborted, please try again.";
                            lblmsg.CssClass = "ErrMsg";
                            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

                        }
                    }
                    catch (Exception ex)
                    {
                        //lblmsg.Text = ex.Message;
                    }

                    txtCustName.Text = "";
                    txtmobile.Text = "";

                    txtemailid.Text = "";
                }
                else
                {
                    lblmsg.Text = "Vehicle No# registered for servicing";
                    lblmsg.CssClass = "ErrMsg";
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

                    txtvehicle.Focus();
                }
            }
            else
            {
                lblmsg.Text = "Enter Vehicle No#.";
                lblmsg.CssClass = "ErrMsg";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

                txtvehicle.Focus();
            }
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
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        txtvehicle.Text = "";

        txtCustName.Text = "";
        txtmobile.Text = "";
        txtemailid.Text = "";
    }

    protected void cmbCustType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (cmbCustType.SelectedIndex != 0)
            {
                cmbServiceType.SelectedIndex = -1;
                cmbVehicleModel.SelectedIndex = -1;
                cmbProcess.SelectedIndex = -1;
                TxtDate1.Text = "";
                TxtDate2.Text = "";
                txtVehicleNumber.Text = "";
                txtTagNo.Text = "";
                lblmsg.Text = "";
                lblmsg.CssClass = "reset";
            }
            BindGrid();
            TabContainer1.Visible = false;
        }
        catch (Exception ex)
        {
        }
    }

    protected void cmbProcess_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (cmbProcess.SelectedIndex != 0)
            {
                cmbCustType.SelectedIndex = -1;
                cmbServiceType.SelectedIndex = -1;
                cmbVehicleModel.SelectedIndex = -1;
                TxtDate1.Text = "";
                TxtDate2.Text = "";
                txtVehicleNumber.Text = "";
                txtTagNo.Text = "";
                lblmsg.Text = "";
                lblmsg.CssClass = "reset";
            }
            BindGrid();
            TabContainer1.Visible = false;
        }
        catch (Exception ex)
        {
        }
    }

    protected void cmbServiceType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (cmbServiceType.SelectedIndex != 0)
            {
                cmbCustType.SelectedIndex = -1;
                cmbVehicleModel.SelectedIndex = -1;
                cmbProcess.SelectedIndex = -1;
                TxtDate1.Text = "";
                TxtDate2.Text = "";
                txtVehicleNumber.Text = "";
                txtTagNo.Text = "";
                lblmsg.Text = "";
                lblmsg.CssClass = "reset";
            }
            BindGrid();
            TabContainer1.Visible = false;
        }
        catch (Exception ex)
        {
        }
    }

    protected void cmbTeamLead_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (cmbTeamLead.SelectedIndex != 0)
            {
                cmbCustType.SelectedIndex = -1;
                cmbServiceType.SelectedIndex = -1;
                cmbVehicleModel.SelectedIndex = -1;
                TxtDate1.Text = "";
                TxtDate2.Text = "";
                txtVehicleNumber.Text = "";
                txtTagNo.Text = "";
                lblmsg.Text = "";
                lblmsg.CssClass = "reset";
            }
            BindGrid();
            TabContainer1.Visible = false;
        }
        catch (Exception ex)
        {
        }
    }

    protected void cmbVehicleModel_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (cmbVehicleModel.SelectedIndex != 0)
            {
                cmbCustType.SelectedIndex = -1;
                cmbServiceType.SelectedIndex = -1;
                cmbProcess.SelectedIndex = -1;
                TxtDate1.Text = "";
                TxtDate2.Text = "";
                txtVehicleNumber.Text = "";
                txtTagNo.Text = "";
                lblmsg.Text = "";
                lblmsg.CssClass = "reset";
            }
            BindGrid();
            TabContainer1.Visible = false;
        }
        catch (Exception ex)
        {
        }
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

    protected void ddlSRemarks_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSRemarks.SelectedValue == "-1")
        {
            txtspremarks.Visible = true;
        }
        else
        {
            txtspremarks.Visible = false;
        }
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlState.SelectedIndex != 0)
            {
                cmbCustType.SelectedIndex = -1;
                cmbServiceType.SelectedIndex = -1;
                cmbProcess.SelectedIndex = -1;
                TxtDate1.Text = "";
                TxtDate2.Text = "";
                txtVehicleNumber.Text = "";
                txtTagNo.Text = "";
                lblmsg.Text = "";
                lblmsg.CssClass = "reset";
            }
            statusVal = ddlState.SelectedValue.Trim();
            BindGrid();
            TabContainer1.Visible = false;
        }
        catch (Exception ex)
        {
        }
    }

    protected void ddPDTRemarks_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddPDTRemarks.SelectedValue == "-1")
        {
            txtPDTComment.Visible = true;
        }
        else
        {
            txtPDTComment.Visible = false;
        }
    }

    protected void ddVOutRemarks_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddVOutRemarks.SelectedValue == "-1")
        {
            txt_VORemarks.Visible = true;
        }
        else
        {
            txt_VORemarks.Visible = false;
        }
    }

    protected void drpsptype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpsptype.SelectedIndex == 1)
        {
            lblspprocess.Visible = true;
            drpspprocess.Visible = true;
            lblServiceAction.Visible = false;
            lblServiceRecom.Visible = false;
            txtServiceAction.Visible = false;
            txtRecomendation.Visible = false;
            txtspremarks.Visible = false;
            FillRemarksTemplate(7, ref ddlSRemarks);
        }
        else if (drpsptype.SelectedIndex == 2)
        {
            lblspprocess.Visible = false;
            drpspprocess.Visible = false;
            lblServiceAction.Visible = false;
            lblServiceRecom.Visible = false;
            txtServiceAction.Visible = false;
            txtRecomendation.Visible = false;
            txtspremarks.Visible = false;
            FillRemarksTemplate(8, ref ddlSRemarks);
        }
        else
        {
            lblspprocess.Visible = false;
            drpspprocess.Visible = false;
            lblServiceAction.Visible = true;
            lblServiceRecom.Visible = true;
            txtServiceAction.Visible = true;
            txtRecomendation.Visible = true;
            txtspremarks.Visible = false;
            FillRemarksTemplate(6, ref ddlSRemarks);
        }
    }

    protected string[] GetCustomerDetail(String RegNo)
    {
        string sConnString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        SqlConnection oConn = new SqlConnection(sConnString);
        SqlCommand cmd = new SqlCommand("", oConn);
        cmd.CommandText = "SELECT Top 1 CustomerName,Customerphone,SlNo FROM tblMaster WHERE RegNo=@RegNo order by Slno desc";
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@RegNo", RegNo);
        SqlDataReader dr = null;
        string[] cust = { string.Empty, string.Empty, string.Empty };
        try
        {
            if (oConn.State != ConnectionState.Open)
                oConn.Open();
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                cust[0] = dr["CustomerName"].ToString();
                cust[1] = dr["Customerphone"].ToString();
                cust[2] = dr["SlNo"].ToString();
            }
            return cust;
        }
        catch
        {
            return cust;
        }
        finally
        {
            if (oConn.State != ConnectionState.Closed)
                oConn.Close();
        }
    }

    protected string getFFRemarks(GridViewRowEventArgs e)
    {
        string ffrem = "";
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlCommand cmd = new SqlCommand("GetFFRemarks", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ServiceId", e.Row.Cells[36].Text.ToString());
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0][0].ToString() == "YES")
                ffrem = dt.Rows[0][0].ToString();
            else if (dt.Rows[0][0].ToString() == "NO")
            {
                ffrem = dt.Rows[0][0].ToString() + ": " + dt.Rows[0][1].ToString();
            }
        }
        return ffrem;
    }

    protected string getPMKRemarks(GridViewRowEventArgs e)
    {
        string pmkrem = "";
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlCommand cmd = new SqlCommand("GetPMKRemarks", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ServiceId", e.Row.Cells[36].Text.ToString());
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0][0].ToString() == "100%")
                pmkrem = dt.Rows[0][0].ToString();
            else
            {
                pmkrem = dt.Rows[0][0].ToString() + ": " + dt.Rows[0][1].ToString();
            }
        }
        return pmkrem;
    }

    protected void getRemainingTabs(string SlNo, string selRow)
    {
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            cmd.CommandText = "GetRemainingTabs";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SlNo", SlNo);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                TabPanel6.Enabled = true; //false
                TabPanel10.Enabled = true;
                TabPanel1.Enabled = true;
                TabPanel11.Enabled = true;
                TabPanel13.Enabled = true;
                if (dt.Rows[0]["Hold"].ToString() == "0")
                {
                    TabPanel13.Enabled = false;
                }
                if (dt.Rows[0]["RPDTInformed"].ToString() == "1")
                {
                    TabPanel7.Enabled = false;
                }
                if (dt.Rows[0]["JCCInformed"].ToString() == "1")
                {
                    TabPanel8.Enabled = false;
                }

                if (dt.Rows[0]["VehicleReady"].ToString().Replace("&nbsp;", "").Trim() == "" || dt.Rows[0]["VehicleReady"].ToString().Trim() == "NULL")  // dt.Rows[0]["VehicleReady"].ToString().Trim() != "" || dt.Rows[0]["VehicleReady"].ToString().Trim() == "NULL"
                {
                    TabPanel6.Enabled = true; //false
                    TabPanel10.Enabled = true;
                    TabPanel1.Enabled = true;
                    TabPanel11.Enabled = true;
                    TabContainer1.ActiveTabIndex = 0;
                }
                else
                {
                    TabPanel6.Enabled = false;
                    TabPanel10.Enabled = false;
                    TabPanel1.Enabled = false;
                    TabPanel11.Enabled = false;
                    TabContainer1.ActiveTabIndex = 1;
                }
                if (dt.Rows[0][2].ToString().Replace("&nbsp;", "").Trim().ToUpper() == "FALSE" || dt.Rows[0][2].ToString().Replace("&nbsp;", "").Trim() == "" || dt.Rows[0][2].ToString().Trim() == "NULL")
                {
                    TabPanel1.Enabled = true; //false
                    TabPanel10.Enabled = true;
                    TabPanel11.Enabled = true;
                    TabContainer1.ActiveTabIndex = 0;
                    if (dt.Rows[0]["VehicleReady"].ToString().Replace("&nbsp;", "").Trim() == "" || dt.Rows[0]["VehicleReady"].ToString().Trim() == "NULL")  // dt.Rows[0]["VehicleReady"].ToString().Trim() != "" || dt.Rows[0]["VehicleReady"].ToString().Trim() == "NULL"
                    {
                        TabPanel6.Enabled = true; //false
                        TabPanel10.Enabled = true;
                        TabPanel1.Enabled = true;
                        TabPanel11.Enabled = true;
                        TabContainer1.ActiveTabIndex = 0;
                    }
                    else
                    {
                        TabPanel6.Enabled = false;
                        TabPanel10.Enabled = false;
                        TabPanel1.Enabled = false;
                        TabPanel11.Enabled = false;
                        TabContainer1.ActiveTabIndex = 1;
                    }
                }
                else
                {
                    TabPanel1.Enabled = false;
                    TabPanel10.Enabled = false;
                    TabPanel6.Enabled = false;
                    TabPanel11.Enabled = false;
                    TabContainer1.ActiveTabIndex = 1;
                }
            }
            else
            {
                TabPanel6.Enabled = false;
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

    protected void getSA()
    {
        cmbSA.Items.Clear();
        cmbSA.Items.Add("Service Advisor");
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlCommand cmd = new SqlCommand("GetServiceAdvisorList", con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        cmd.CommandType = CommandType.StoredProcedure;
        if (con.State != ConnectionState.Open)
            con.Open();
        sda.Fill(dt);
        con.Close();
        cmbSA.DataSource = dt;

        cmbSA.DataValueField = "EmpId";
        cmbSA.DataTextField = "EmpName"; cmbSA.DataBind();
    }

    protected void getSACRM(string RefNo)
    {
        ddlSAList.Items.Clear();
        ddlSAList.Items.Add("--Select--");
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlCommand cmd = new SqlCommand("GetServiceAdvisorListInCrm", con);
        cmd.Parameters.AddWithValue("@RefNo", RefNo);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        cmd.CommandType = CommandType.StoredProcedure;
        if (con.State != ConnectionState.Open)
            con.Open();
        sda.Fill(dt);
        con.Close();
        ddlSAList.DataSource = dt;

        ddlSAList.DataValueField = "EmpId";
        ddlSAList.DataTextField = "EmpName"; ddlSAList.DataBind();
    }

    protected void getTagCancelation(int SlNo)
    {
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            cmd.CommandText = "select dbo.chkStatusforCancel(@SlNo)";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@SlNo", SlNo);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0].ToString() == "0")
                {
                    TabPanel9.Enabled = false;
                }
                else if (dt.Rows[0][0].ToString() == "1")
                {
                    TabPanel9.Enabled = true;
                }
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

    protected void getTL()
    {
        cmbTeamLead.Items.Clear();
       // cmbTeamLead.Items.Add("TeamLead");
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlCommand cmd = new SqlCommand("GetTeamLead", con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        cmd.CommandType = CommandType.StoredProcedure;
        if (con.State != ConnectionState.Open)
            con.Open();
        sda.Fill(dt);
        con.Close();
        cmbTeamLead.DataSource = dt;
        cmbTeamLead.DataValueField = "EmpId";
        cmbTeamLead.DataTextField = "EmpName"; cmbTeamLead.DataBind();
    }

    protected void grdDisplay_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdDisplay.PageIndex = e.NewPageIndex;
            List<TMLCRMDisplay> Dt = new List<TMLCRMDisplay>();
            Dt = GetCRMData(rbType.SelectedValue, txtVehicleNumber.Text, miniTabIndex + 1);
            GridView1.DataSource = Dt;
            GridView1.DataBind();
            grdDisplay.DataSource = Dt;
            grdDisplay.DataBind();
        }
        catch (Exception ex)
        {
        }
        finally
        {
        }
    }

    protected void grdDisplay_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (grdDisplay.DataSource != null || GridView1.DataSource !=null)
        {
            string status = "";
            string counter = "";
            string StatusTech = "";
            string RefNo = e.Row.Cells[23].Text.ToString();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[0].Text != "")
                {
                    e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.color='Blue';this.style.textDecoration='underline';";
                    e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='#666699';";
                    if (Session["ROLE"] == "DISPLAY" || Session["ROLE"] == "SM")
                    {
                        TabContainer1.Visible = false;
                    }
                    else
                        e.Row.Attributes.Add("OnClick", "Javascript:__doPostBack('myClick','" + e.Row.RowIndex + "');");
                }
            }

            string[] HeaderName = new string[e.Row.Cells.Count];
            int rcount = 0;
            rcount = e.Row.Cells.Count;

            //FOR HIDING COLUMNS.
            if (e.Row.RowType != DataControlRowType.Pager)
            {
                e.Row.Cells[23].Visible = false;
                e.Row.Cells[24].Visible = false;
		e.Row.Cells[17].Visible = false;
            }

            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "<div style=text-align:center;>" + e.Row.Cells[0].Text.ToString() + "</div>";
                e.Row.Cells[1].Text = "<div style=text-align:center;>" + e.Row.Cells[1].Text.ToString() + "</div>";
                e.Row.Cells[2].Text = "<div style=text-align:center;>" + e.Row.Cells[2].Text.ToString() + "</div>";
                e.Row.Cells[3].Text = "<div style=text-align:center;>JDP</div>";
                e.Row.Cells[4].Text = "<div style=text-align:center;>" + e.Row.Cells[4].Text.ToString() + "</div>";
                e.Row.Cells[5].Text = "<div style=text-align:center;>" + e.Row.Cells[5].Text.ToString() + "</div>";
                e.Row.Cells[6].Text = "<div style=text-align:center;>" + e.Row.Cells[6].Text.ToString() + "</div>";
                e.Row.Cells[7].Text = "<div style=text-align:center;>" + e.Row.Cells[7].Text.ToString() + "</div>";
                e.Row.Cells[8].Text = "<div style=text-align:center;>" + e.Row.Cells[8].Text.ToString() + "</div>";
                e.Row.Cells[9].Text = "<div style=text-align:center;>" + e.Row.Cells[9].Text.ToString() + "</div>";
                e.Row.Cells[10].Text = "<div style=text-align:center;>" + e.Row.Cells[10].Text.ToString() + "</div>";
                e.Row.Cells[11].Text = "<div style=text-align:center;>" + e.Row.Cells[11].Text.ToString() + "</div>";
                e.Row.Cells[12].Text = "<div style=text-align:center;>" + e.Row.Cells[12].Text.ToString() + "</div>";
                e.Row.Cells[13].Text = "<div style=text-align:center;>" + e.Row.Cells[13].Text.ToString() + "</div>";
                e.Row.Cells[14].Text = "<div style=text-align:center;>" + e.Row.Cells[14].Text.ToString() + "</div>";
                e.Row.Cells[15].Text = "<div style=text-align:center;>" + e.Row.Cells[15].Text.ToString() + "</div>";
                e.Row.Cells[16].Text = "<div style=text-align:center;>" + e.Row.Cells[16].Text.ToString() + "</div>";
                e.Row.Cells[17].Text = "<div style=text-align:center;>" + e.Row.Cells[17].Text.ToString() + "</div>";
                e.Row.Cells[18].Text = "<div style=text-align:center;>" + e.Row.Cells[18].Text.ToString() + "</div>";
                e.Row.Cells[19].Text = "<div style=Height:25px;width:20px;></div>";
                e.Row.Cells[20].Text = "<div style=text-align:center;>" + e.Row.Cells[20].Text.ToString() + "</div>";
                e.Row.Cells[21].Text = "<div style=text-align:center;>" + e.Row.Cells[21].Text.ToString() + "</div>";
                e.Row.Cells[22].Text = "<div style=text-align:center;>" + e.Row.Cells[22].Text.ToString() + "</div>";
                e.Row.Cells[24].Text = "<div style=text-align:center;>" + e.Row.Cells[24].Text.ToString() + "</div>";
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //SLNO
                e.Row.Cells[0].Text = "<div>" + e.Row.Cells[0].Text.ToString() + "</div>";

                //TAG
                e.Row.Cells[1].Text = "<div style='width:30px;margin-left:-15px;'>" + e.Row.Cells[1].Text.ToString() + "</div>";

                //REGNO
                if (e.Row.Cells[2].Text.Length > 10)
                {
                    int lenth = e.Row.Cells[2].Text.Length;
                    string cells = e.Row.Cells[2].Text.Substring(lenth - 10, 10);
                    e.Row.Cells[2].Text = "<div style='width:40px;margin-left:-20px'>" + cells + "</div>";
                }
                else
                {
                    e.Row.Cells[2].Text = "<div style='width:40px;margin-left:-20px'>" + e.Row.Cells[2].Text.ToString() + "</div>";
                }
               // e.Row.Cells[2].Text = "<div style=width:80px;text-align:left;>" + e.Row.Cells[2].Text.ToString() + "</div>";
                //e.Row.Cells[2].Attributes.Add("onmouseover", "showRegNoHover(event,'" + RefNo + "')");
                //e.Row.Cells[2].Attributes.Add("onmouseout", "hideTooltip(event)");

                //Type : JDP-CW
                try
                {
                    if (e.Row.Cells[3].Text.ToString() == "0-0")
                        e.Row.Cells[3].Text = "<div style='width:40px;padding-left:35px;'></div>";
                    else if (e.Row.Cells[3].Text.ToString() == "1-0")
                        e.Row.Cells[3].Text = "<div style='width:40px;padding-left:35px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/JDP.png' Alt=''  width='16' height='16'/></div>";
                    else if (e.Row.Cells[3].Text.ToString() == "0-1")
                        e.Row.Cells[3].Text = "<div style='width:40px;padding-left:35px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/Customer_Waiting.png' Alt=''  width='16' height='16'/></div>";
                    else if (e.Row.Cells[3].Text.ToString() == "1-1")
                        e.Row.Cells[3].Text = "<div style='width:40px;padding-left:35px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/JDP_Customer_Waiting.png' Alt=''  width='16' height='16'/></div>";
                }
                catch (Exception ex)
                {
                }

                //MODEL
                try
                {
                    e.Row.Cells[4].ToolTip = e.Row.Cells[4].Text.ToString();//.Replace("<div style=width:60px;>", "").Replace("</div>", "");
                    if (e.Row.Cells[4].Text.Length > 10)
                    {
                        int lenth = e.Row.Cells[4].Text.Length;
                        string cells = e.Row.Cells[4].Text.Substring(lenth - 10, 10);
                        e.Row.Cells[4].Text = "<div style='width:40px;padding-left:30px;'>" + cells + "</div>";
                    }
                       // e.Row.Cells[4].Text = "<div style=width:60px;text-align:left;>" + e.Row.Cells[4].Text.ToString().Substring(0, 10) + "</div>";
                    else
                    { 
                        e.Row.Cells[4].Text = "<div style='width:40px;padding-left:30px;'>" + e.Row.Cells[4].Text.ToString() + "</div>";
                    }
                }
                catch (Exception ex)
                {
                }

                //S-T
                e.Row.Cells[5].Text = "<div style='width:40px;padding-left:35px;'>" + e.Row.Cells[5].Text.ToString() + "</div>";

                //STAUS - 000-Normal, 100-Ready , 010-Hold , 001-Idle
                try
                {
                    string[] status_str = e.Row.Cells[6].Text.ToString().Split('|');
                    if (status_str[0] == "0-0-0")
                    {
                        e.Row.Cells[6].Text = "<div style='width:40px;padding-left:45px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/Status.png' Alt=''  width='16' height='16'/></div>";
                        e.Row.Cells[6].ToolTip = "Work In Progress";
                    }
                    else if (status_str[0] == "1-0-0")
                    {
                        e.Row.Cells[6].Text = "<div style='width:40px;padding-left:45px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/Status_Ready.png' Alt='' width='16' height='16'/></div>";
                        e.Row.Cells[6].ToolTip = "Vehicle Ready [" + status_str[1].Trim() + "]";
                    }
                    else if (status_str[0] == "0-1-0")
                    {
                        e.Row.Cells[6].Text = "<div style='width:40px;padding-left:45px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/Status_HOLD.png' Alt='' width='16' height='16'/></div>";
                        e.Row.Cells[6].ToolTip = "Hold";
                    }
                    else if (status_str[0] == "0-0-1")
                    {
                        e.Row.Cells[6].Text = "<div style='width:40px;padding-left:45px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/Status_IDLE.png' Alt='' width='16' height='16'/></div>";
                        e.Row.Cells[6].ToolTip = "Idle";
                    }
                }
                catch (Exception ex)
                {
                }

                //VI //jc
                try
                {
                    string getJCP = string.Empty;
                    string[] JCPParts = { };
                    getJCP = (e.Row.Cells[7].Text.Trim() == "&nbsp;" ? "" : e.Row.Cells[7].Text.Trim());
                    JCPParts = getJCP.Split('|');
                    //e.Row.Cells[7].Attributes.Add("onmouseover", "showProcessInOut(event,'" + RefNo + "','" + JCPParts[2].Replace("*", "") + "','S.A')");
                    //e.Row.Cells[7].Attributes.Add("onmouseout", "hideTooltip(event)");

                    status = e.Row.Cells[7].Text.ToString().Substring(0, 1);
                    if (status == "0")
                    {
                        if (e.Row.Cells[7].Text.ToString().Contains('*'))
                            e.Row.Cells[7].Text = "<div style='width:40px;padding-left:50px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/SA_Allot.png' Alt=''  width='16' height='16'/></div>";
                        else
                            e.Row.Cells[7].Text = "<div style='width:40px;padding-left:50px;'></div>";
                    }
                    else if (status == "1")
                        e.Row.Cells[7].Text = "<div style='width:40px;padding-left:50px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/SA_WIP_ONTIME.png' Alt=''  width='16' height='16'/></div>";
                    else if (status == "2")
                        e.Row.Cells[7].Text = "<div style='width:40px;padding-left:50px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/SA_WIP_NEAR.png' Alt=''  width='16' height='16'/></div>";
                    else if (status == "3")
                        e.Row.Cells[7].Text = "<div style='width:40px;padding-left:50px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/SA_WIP_DELAY.png' Alt=''  width='16' height='16'/></div>";
                    else if (status == "4")
                        e.Row.Cells[7].Text = "<div style='width:40px;padding-left:50px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN.png' Alt=''  width='16' height='16'/></div>";
                    else if (status == "5")
                        e.Row.Cells[7].Text = "<div style='width:40px;padding-left:50px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CR.png' Alt=''  width='16' height='16'/></div>";
                    else if (status == "6")
                        e.Row.Cells[7].Text = "<div style='width:40px;padding-left:50px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/OutSkipped.png' Alt=''  width='16' height='16'/></div>";
                }
                catch (Exception ex)
                {
                }

                //PARTS STATUS
                if (e.Row.Cells[8].Text.ToString().Trim() == "0")
                {
                    e.Row.Cells[8].Text = "<div style='width:40px;padding-left:45px;'></div>";
                }
                else if (e.Row.Cells[8].Text.ToString().Trim() == "1")
                {
                    e.Row.Cells[8].Text = "<div style='width:40px;padding-left:45px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484297131/images/Red.png' Alt=''  width='16' height='16'/></div>";
                }
                else if (e.Row.Cells[8].Text.ToString().Trim() == "2")
                {
                    e.Row.Cells[8].Text = "<div style='width:40px;padding-left:45px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484297131/images/Light.png' Alt=''  width='16' height='16'/></div>";
                }
                else if (e.Row.Cells[8].Text.ToString().Trim() == "3")
                {
                    e.Row.Cells[8].Text = "<div style='width:40px;padding-left:45px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484297131/images/Green.png' Alt=''  width='16' height='16'/></div>";
                }
                else if (e.Row.Cells[8].Text.ToString().Trim() == "4")
                {
                    e.Row.Cells[8].Text = "<div style='width:40px;padding-left:45px;'> <img id='animg' src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484297131/images/Yellow_With_Near.png' Alt='' width='16' height='16'/></div>";
                }
                //e.Row.Cells[8].Attributes.Add("onmouseover", "showPartsHover(event,'" + RefNo + "')");
                //e.Row.Cells[8].Attributes.Add("onmouseout", "hideTooltip(event)");


                //JA
                try
                {
                    status = e.Row.Cells[9].Text.ToString().Substring(0, 1);
                    if (status == "1" || status == "2")
                    {
                        //e.Row.Cells[9].Attributes.Add("onmouseover", "showJADHover(event,'" + RefNo + "')");
                        //e.Row.Cells[9].Attributes.Add("onmouseout", "hideTooltip(event)");
                    }

                    StatusTech = e.Row.Cells[9].Text.ToString().Substring(1, 1);
                    if (status == "0")
                        e.Row.Cells[9].Text = "<div style='width:40px;padding-left:45px;'></div>";
                    else if (status == "1")
                        e.Row.Cells[9].Text = "<div style='width:40px;padding-left:45px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/JA_ONTIME.png' Alt=''  width='16' height='16'/></div>";
                    else if (status == "2")
                        e.Row.Cells[9].Text = "<div style='width:40px;padding-left:45px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/JA_DELAY.png' Alt=''  width='16' height='16'/></div>";
                }
                catch (Exception ex)
                {
                }



                //T1 - Technician1
                try
                {
                    string getTECH1 = (e.Row.Cells[10].Text.Trim() == "&nbsp;" ? "" : e.Row.Cells[10].Text.Trim());
                    int blinkflag = 0;
                    if (getTECH1.Contains('Y'))
                    {
                        blinkflag = 1;
                        getTECH1 = getTECH1.Replace("Y", "");
                    }

                    if (getTECH1.Contains("|"))
                    {
                        string[] TECH1Parts = { };
                        TECH1Parts = getTECH1.Split('|');
                        if (Convert.ToInt32(TECH1Parts[1]) > 1)
                        {
                            //e.Row.Cells[10].Attributes.Add("onmouseover", "showEmpInOut(event,'" + TECH1Parts[2].Replace("*", "").Replace("$", "").Replace("#", "") + "','" + TECH1Parts[1] + "','Tech1')");
                            //e.Row.Cells[10].Attributes.Add("onmouseout", "hideTooltip(event)");
                        }
                    }

                    status = e.Row.Cells[10].Text.ToString().Substring(0, 1);

                    if (status == "0")
                    {
                        //if (e.Row.Cells[9].Text.ToString().Contains('*'))
                        if (StatusTech == "0")
                            e.Row.Cells[10].Text = "<div style='width:40px;padding-left:45px;'></div>";
                        else
                            e.Row.Cells[10].Text = "<div style='width:40px;padding-left:45px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/TECH_ALLOT.png' Alt=''  width='16' height='16'/></div>";
                    }
                    else if (status == "1")
                    {
                        e.Row.Cells[10].Text = "<div style='width:40px;padding-left:45px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/TECH_WIP_ONTIME.png' Alt=''  width='16' height='16'/></div>";
                    }
                    else if (status == "2")
                    {
                        if (blinkflag == 1)
                            e.Row.Cells[10].Text = "<div style='width:40px;padding-left:45px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/TECH_WIP_NEAR.png' Alt='' width='16' height='16'/></div>";
                        else
                            e.Row.Cells[10].Text = "<div style='width:40px;padding-left:45px;'><img id='animg' src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/TECH_WIP_NEAR.png' Alt='' width='16' height='16'/></div>";
                    }
                    else if (status == "3")
                    {
                        e.Row.Cells[10].Text = "<div style='width:40px;padding-left:45px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/TECH_WIP_DELAY.png' Alt=''  width='16' height='16'/></div>";
                    }
                    else if (status == "4")
                    {
                        e.Row.Cells[10].Text = "<div style='width:40px;padding-left:45px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN.png' Alt=''  width='16' height='16'/></div>";
                    }
                    else if (status == "5")
                    {

                        e.Row.Cells[10].Text = "<div style='width:40px;padding-left:45px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CR.png' Alt=''  width='16' height='16'/></div>";
                    }
                    else if (status == "6")
                    {
                        e.Row.Cells[10].Text = "<div style='width:40px;padding-left:45px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/OutSkipped.png' Alt='' width='16' height='16'/></div>";
                    }
                }
                catch (Exception ex)
                { }


                //T2 - Technician2
                try
                {
                    string getTECH2 = (e.Row.Cells[11].Text.Trim() == "&nbsp;" ? "" : e.Row.Cells[11].Text.Trim());
                    int blinkflag = 0;
                    if (getTECH2.Contains('Y'))
                    {
                        blinkflag = 1;
                        getTECH2 = getTECH2.Replace("Y", "");
                    }

                    if (getTECH2.Contains("|"))
                    {
                        string[] TECH2Parts = { };
                        TECH2Parts = getTECH2.Split('|');
                        if (Convert.ToInt32(TECH2Parts[1]) > 1)
                        {
                            //e.Row.Cells[11].Attributes.Add("onmouseover", "showEmpInOut(event,'" + TECH2Parts[2].Replace("*", "").Replace("$", "").Replace("#", "") + "','" + TECH2Parts[1] + "','Tech2')");
                            //e.Row.Cells[11].Attributes.Add("onmouseout", "hideTooltip(event)");
                        }
                    }
                    status = e.Row.Cells[11].Text.ToString().Substring(0, 1);

                    if (status == "0")
                    {
                        if (Convert.ToInt16(StatusTech) > 1)
                            e.Row.Cells[11].Text = "<div style='width:40px;padding-left:45px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/TECH_ALLOT.png' Alt=''  width='16' height='16'/></div>";
                        else
                            e.Row.Cells[11].Text = "<div style='width:40px;padding-left:45px;'></div>";
                    }
                    else if (status == "1")
                    {
                        e.Row.Cells[11].Text = "<div style='width:40px;padding-left:45px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/TECH_WIP_ONTIME.png' Alt=''  width='16' height='16'/></div>";
                    }
                    else if (status == "2")
                    {
                        if (blinkflag == 1)
                            e.Row.Cells[11].Text = "<div style='width:40px;padding-left:45px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/TECH_WIP_NEAR.png' Alt='' width='16' height='16'/></div>";
                        else
                            e.Row.Cells[11].Text = "<div style='width:40px;padding-left:45px;'><img id='animg' src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/TECH_WIP_NEAR.png' Alt='' width='16' height='16'/></div>";
                    }
                    else if (status == "3")
                    {
                        e.Row.Cells[11].Text = "<div style='width:40px;padding-left:45px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/TECH_WIP_DELAY.png' Alt=''  width='16' height='16'/></div>";
                    }
                    else if (status == "4")
                    {
                        e.Row.Cells[11].Text = "<div style='width:40px;padding-left:45px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN.png' Alt=''  width='16' height='16'/></div>";
                    }
                    else if (status == "5")
                    {
                        e.Row.Cells[11].Text = "<div style='width:40px;padding-left:45px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CR.png' Alt=''  width='16' height='16'/></div>";
                    }
                    else if (status == "6")
                    {
                        e.Row.Cells[11].Text = "<div style='width:40px;padding-left:45px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/OutSkipped.png' Alt=''  width='16' height='16'/></div>";
                    }
                }
                catch (Exception ex)
                { }

                //T3 - Technician3
                try
                {
                    string getTECH3 = (e.Row.Cells[12].Text.Trim() == "&nbsp;" ? "" : e.Row.Cells[12].Text.Trim());
                    int blinkflag = 0;
                    if (getTECH3.Contains('Y'))
                    {
                        blinkflag = 1;
                        getTECH3 = getTECH3.Replace("Y", "");
                    }

                    if (getTECH3.Contains("|"))
                    {
                        string[] TECH3Parts = { };
                        TECH3Parts = getTECH3.Split('|');
                        if (Convert.ToInt32(TECH3Parts[1]) > 1)
                        {
                            //e.Row.Cells[12].Attributes.Add("onmouseover", "showEmpInOut(event,'" + TECH3Parts[2].Replace("*", "").Replace("$", "").Replace("#", "") + "','" + TECH3Parts[1] + "','Tech3')");
                            //e.Row.Cells[12].Attributes.Add("onmouseout", "hideTooltip(event)");
                        }
                    }

                    status = e.Row.Cells[12].Text.ToString().Substring(0, 1);

                    if (status == "0")
                    {
                        if (Convert.ToInt16(StatusTech) > 2)
                            e.Row.Cells[12].Text = "<div style='width:40px;padding-left:45px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/TECH_ALLOT.png' Alt=''  width='16' height='16'/></div>";
                        else
                            e.Row.Cells[12].Text = "<div style='width:40px;padding-left:45px;'></div>";
                    }
                    else if (status == "1")
                    {
                        e.Row.Cells[12].Text = "<div style='width:40px;padding-left:45px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/TECH_WIP_ONTIME.png' Alt=''  width='16' height='16'/></div>";
                    }
                    else if (status == "2")
                    {
                        if (blinkflag == 1)
                            e.Row.Cells[12].Text = "<div style='width:40px;padding-left:45px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/TECH_WIP_NEAR.png' Alt='' width='16' height='16'/></div>";
                        else
                            e.Row.Cells[12].Text = "<div style='width:40px;padding-left:45px;'><img id='animg' src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/TECH_WIP_NEAR.png' Alt='' width='16' height='16'/></div>";
                    }
                    else if (status == "3")
                    {
                        e.Row.Cells[12].Text = "<div style='width:40px;padding-left:45px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/TECH_WIP_DELAY.png' Alt=''  width='16' height='16'/></div>";
                    }
                    else if (status == "4")
                    {
                        e.Row.Cells[12].Text = "<div style='width:40px;padding-left:45px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN.png' Alt=''  width='16' height='16'/></div>";
                    }
                    else if (status == "5")
                    {
                        e.Row.Cells[12].Text = "<div style='width:40px;padding-left:45px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CR.png' Alt=''  width='16' height='16'/></div>";
                    }
                    else if (status == "6")
                    {
                        e.Row.Cells[12].Text = "<div style='width:40px;padding-left:45px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/OutSkipped.png' Alt=''  width='16' height='16'/></div>";
                    }
                }
                catch (Exception ex)
                { }



             

                //WA
                try
                {
                    string getWA = (e.Row.Cells[13].Text.Trim() == "&nbsp;" ? "" : e.Row.Cells[13].Text.Trim());
                    string[] WAParts = { };
                    if (getWA.Contains("|"))
                    {
                        WAParts = getWA.Split('|');
                    }
                    //e.Row.Cells[13].Attributes.Add("onmouseover", "showProcessInOut(event,'" + RefNo + "','" + WAParts[2].Replace("$", "").Replace("#", "").Replace("*", "") + "','W-A')");
                    //e.Row.Cells[13].Attributes.Add("onmouseout", "hideTooltip(event)");
                    if (e.Row.Cells[13].Text.ToString().Contains('*'))
                    {
                        status = e.Row.Cells[13].Text.ToString().Substring(0, 1);
                        if (status == "0")
                            if (e.Row.Cells[13].Text.ToString().Contains('$'))
                                e.Row.Cells[13].Text = "<div style='width:40px;padding-left:20px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/notRequired.png' Alt=''  width='16' height='16'/></div>";
                            else
                                e.Row.Cells[13].Text = "<div style='width:40px;padding-left:20px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/WA_allot.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "1")
                            e.Row.Cells[13].Text = "<div style='width:40px;padding-left:20px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/WA_WIP.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "2")
                            e.Row.Cells[13].Text = "<div style='width:40px;padding-left:20px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/WA_WIP_NEAR.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "3")
                            e.Row.Cells[13].Text = "<div style='width:40px;padding-left:20px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/WA_WIP_Delay.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "4")
                            e.Row.Cells[13].Text = "<div style='width:40px;padding-left:20px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "5")
                            e.Row.Cells[13].Text = "<div style='width:40px;padding-left:20px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CR.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "6")
                            e.Row.Cells[13].Text = "<div style='width:40px;padding-left:20px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/OutSkipped.png' Alt=''  width='16' height='16'/></div>";
                    }
                    else
                    {
                        status = e.Row.Cells[13].Text.ToString().Substring(0, 1);
                        if (status == "0")
                            if (e.Row.Cells[13].Text.ToString().Contains('$'))
                                e.Row.Cells[13].Text = "<div style='width:40px;padding-left:20px;'></div>";
                            else
                                e.Row.Cells[13].Text = "<div style='width:40px;padding-left:20px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/WA_ALLOT.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "1")
                            e.Row.Cells[13].Text = "<div style='width:40px;padding-left:20px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/WA_WIP.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "2")
                            e.Row.Cells[13].Text = "<div style='width:40px;padding-left:20px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/WA_WIP_NEAR.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "3")
                            e.Row.Cells[13].Text = "<div style='width:40px;padding-left:20px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/WA_WIP_DELAY.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "4")
                            e.Row.Cells[13].Text = "<div style='width:40px;padding-left:20px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "5")
                            e.Row.Cells[13].Text = "<div style='width:40px;padding-left:20px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CR.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "6")
                            e.Row.Cells[13].Text = "<div style='width:40px;padding-left:20px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/OutSkipped.png' Alt=''  width='16' height='16'/></div>";
                    }
                }
                catch (Exception ex)
                { }


                //RT
                try
                {
                    string getRT = (e.Row.Cells[14].Text.Trim() == "&nbsp;" ? "" : e.Row.Cells[14].Text.Trim());
                    string[] RTParts = { };
                    if (getRT.Contains("|"))
                    {
                        RTParts = getRT.Split('|');
                    }
                    //e.Row.Cells[14].Attributes.Add("onmouseover", "showSubProcessInOut(event,'" + RefNo + "','" + RTParts[1] + "','Road Test')");
                    //e.Row.Cells[14].Attributes.Add("onmouseout", "hideTooltip(event)");

                    status = e.Row.Cells[14].Text.ToString().Substring(0, 1);
                    counter = e.Row.Cells[14].Text.ToString().Split('|')[1];
                    if (status == "0")
                    {
                        e.Row.Cells[14].Text = "<div style='width:40px;padding-left:15px;'></div>";
                    }
                    else if (status == "1" || status == "2" || status == "3")
                    {
                        e.Row.Cells[14].Text = "<div style='width:40px;padding-left:15px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/RT_ONTIME.png' Alt=''  width='16' height='16'/></div>";
                    }
                    else if (status == "4" || status == "5")
                    {
                        if (counter == "1")
                        {
                            e.Row.Cells[14].Text = "<div style='width:40px;padding-left:15px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN.png' Alt=''  width='16' height='16'/></div>";
                        }
                        else if (counter == "2")
                        {
                            e.Row.Cells[14].Text = "<div style='width:40px;padding-left:15px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN2.png' Alt=''  width='16' height='16'/></div>";
                        }
                        else if (counter == "3")
                        {
                            e.Row.Cells[14].Text = "<div style='width:40px;padding-left:15px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN3.png' Alt=''  width='16' height='16'/></div>";
                        }                        
                    }
                    else if (status == "6")
                    {
                        e.Row.Cells[14].Text = "<div style='width:40px;padding-left:15px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/OutSkipped.png' Alt=''  width='16' height='16'/></div>";
                    }
                }
                catch (Exception ex)
                { }                


                //WSH
                try
                {
                    string getWSH = (e.Row.Cells[15].Text.Trim() == "&nbsp;" ? "" : e.Row.Cells[15].Text.Trim());
                    string[] WSHParts = { };
                    if (getWSH.Contains("|"))
                    {
                        WSHParts = getWSH.Split('|');
                    }
                    //e.Row.Cells[15].Attributes.Add("onmouseover", "showProcessInOut(event,'" + RefNo + "','" + WSHParts[2].Replace("#", "").Replace("$", "").Replace("*", "") + "','Wash')");
                    //e.Row.Cells[15].Attributes.Add("onmouseout", "hideTooltip(event)");

                    if (e.Row.Cells[15].Text.ToString().Contains('*'))
                    {
                        status = e.Row.Cells[15].Text.ToString().Substring(0, 1);
                        counter = e.Row.Cells[15].Text.ToString().Split('|')[1];
                        if (status == "0")
                        {
                            if (e.Row.Cells[15].Text.ToString().Contains('$'))
                                e.Row.Cells[15].Text = "<div style='width:40px;padding-left:15px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/notRequired.png' Alt=''  width='16' height='16'/></div>";
                            else
                                e.Row.Cells[15].Text = "<div style='width:40px;padding-left:15px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/WASH_allot.png' Alt=''  width='16' height='16'/></div>";
                        }
                        else if (status == "1")
                        {
                            e.Row.Cells[15].Text = "<div style='width:40px;padding-left:15px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/WASH_WIP.png' Alt=''  width='16' height='16'/></div>";
                        }
                        else if (status == "2")
                        {
                            e.Row.Cells[15].Text = "<div style='width:40px;padding-left:15px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/WASH_WIP_NEAR.png' Alt=''  width='16' height='16'/></div>";
                        }
                        else if (status == "3")
                        {
                            e.Row.Cells[15].Text = "<div style='width:40px;padding-left:15px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/WASH_WIP_Delay.png' Alt=''  width='16' height='16'/></div>";
                        }
                        else if (status == "4")
                        {
                            if (counter == "1")
                            {
                                e.Row.Cells[15].Text = "<div style='width:40px;padding-left:15px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN.png' Alt=''  width='16' height='18'/></div>";
                            }
                            else if (counter == "2")
                            {
                                e.Row.Cells[15].Text = "<div style='width:40px;padding-left:15px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN2.png' Alt=''  width='16' height='18'/></div>";
                            }
                            else if (counter == "3")
                            {
                                e.Row.Cells[15].Text = "<div style='width:40px;padding-left:15px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN3.png' Alt=''  width='16' height='18'/></div>";
                            }                            
                        }
                        else if (status == "5")
                        {
                            if (counter == "1")
                            {
                                e.Row.Cells[15].Text = "<div style='width:40px;padding-left:15px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CR.png' Alt=''  width='16' height='18'/></div>";
                            }
                            else if (counter == "2")
                            {
                                e.Row.Cells[15].Text = "<div style='width:40px;padding-left:15px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CR2.png' Alt=''  width='16' height='18'/></div>";
                            }
                            else if (counter == "3")
                            {
                                e.Row.Cells[15].Text = "<div style='width:40px;padding-left:15px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CR3.png' Alt=''  width='16' height='18'/></div>";
                            }                            
                        }
                        else if (status == "6")
                        {
                            e.Row.Cells[15].Text = "<div style='width:40px;padding-left:15px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/OutSkipped.png' Alt=''  width='16' height='16'/></div>";
                        }
                    }
                    else
                    {
                        status = e.Row.Cells[15].Text.ToString().Substring(0, 1);
                        counter = e.Row.Cells[15].Text.ToString().Split('|')[1];
                        if (status == "0")
                        {
                            if (e.Row.Cells[15].Text.ToString().Contains('$'))
                                e.Row.Cells[15].Text = "<div style='width:40px;padding-left:15px;'></div>";
                            else
                                e.Row.Cells[15].Text = "<div style='width:40px;padding-left:15px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/WASH.png' Alt=''  width='16' height='16'/></div>";
                        }
                        else if (status == "1")
                        {
                            e.Row.Cells[15].Text = "<div sty    le=width:30px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/WASH_WIP.png' Alt=''  width='16' height='16'/></div>";
                        }
                        else if (status == "2")
                        {
                            e.Row.Cells[15].Text = "<div style='width:40px;padding-left:15px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/WASH_WIP_NEAR.png' Alt=''  width='16' height='16'/></div>";
                        }
                        else if (status == "3")
                        {
                            e.Row.Cells[15].Text = "<div style='width:40px;padding-left:15px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/WASH_WIP_Delay.png' Alt=''  width='16' height='16'/></div>";
                        }
                        else if (status == "4")
                        {
                            if (counter == "1")
                            {
                                e.Row.Cells[15].Text = "<div style='width:40px;padding-left:15px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN.png' Alt=''  width='16' height='18'/></div>";
                            }
                            else if (counter == "2")
                            {
                                e.Row.Cells[15].Text = "<div style='width:40px;padding-left:15px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN2.png' Alt=''  width='16' height='18'/></div>";
                            }
                            else if (counter == "3")
                            {
                                e.Row.Cells[15].Text = "<div style='width:40px;padding-left:15px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN3.png' Alt=''  width='16' height='18'/></div>";
                            }
                        }
                        else if (status == "5")
                        {
                            if (counter == "1")
                            {
                                e.Row.Cells[15].Text = "<div style='width:40px;padding-left:15px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CR.png' Alt=''  width='16' height='18'/></div>";
                            }
                            else if (counter == "2")
                            {
                                e.Row.Cells[15].Text = "<div style='width:40px;padding-left:15px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CR2.png' Alt=''  width='16' height='18'/></div>";
                            }
                            else if (counter == "3")
                            {
                                e.Row.Cells[15].Text = "<div style='width:40px;padding-left:15px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CR3.png' Alt=''  width='16' height='18'/></div>";
                            }
                        }
                        else if (status == "6")
                        {
                            e.Row.Cells[15].Text = "<div style='width:40px;padding-left:15px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/OutSkipped.png' Alt=''  width='16' height='16'/></div>";
                        }
                    }
                }
                catch (Exception ex)
                { }



                //QC-FI
                try
                {
                    int repeatStatus = e.Row.Cells[16].Text.Trim().Contains("R") == true ? 1 : 0;
                    e.Row.Cells[16].Text = e.Row.Cells[16].Text.Trim().Replace("R", "");

                    string getFIN = (e.Row.Cells[16].Text.Trim() == "&nbsp;" ? "" : e.Row.Cells[16].Text.Trim());
                    string[] FINParts = { };
                    if (getFIN.Contains("|"))
                    {
                        FINParts = getFIN.Split('|');
                    }
                    //e.Row.Cells[16].Attributes.Add("onmouseover", "showProcessInOut(event,'" + RefNo + "','" + FINParts[2].Replace("#", "").Replace("$", "").Replace("*", "") + "','FI')");
                    //e.Row.Cells[16].Attributes.Add("onmouseout", "hideTooltip(event)");

                    status = e.Row.Cells[16].Text.ToString().Substring(0, 1);
                    counter = e.Row.Cells[16].Text.ToString().Split('|')[1];
                    if (repeatStatus == 0)
                    {
                        if (status == "0")
                        {
                            e.Row.Cells[16].Text = "<div style='width:40px;padding-left:15px;'></div>";
                        }
                        else if (status == "1")
                        {
                            e.Row.Cells[16].Text = "<div style='width:40px;padding-left:15px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/QC_WIP.png' Alt='' width='16' height='16'/></div>";
                        }
                        else if (status == "2")
                        {
                            e.Row.Cells[16].Text = "<div style='width:40px;padding-left:15px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/QC_WIP_Near.png' Alt='' width='16' height='16'/></div>";
                        }
                        else if (status == "3")
                        {
                            e.Row.Cells[16].Text = "<div style='width:40px;padding-left:15px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/QC_WIP_Delay.png' Alt='' width='16' height='16'/></div>";
                        }
                        else if (status == "4")
                        {
                            if (counter == "1")
                            {
                                e.Row.Cells[16].Text = "<div style='width:40px;padding-left:15px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN.png' Alt='' width='16' height='16'/></div>";
                            }
                            else if (counter == "2")
                            {
                                e.Row.Cells[16].Text = "<div style='width:40px;padding-left:15px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN2.png' Alt='' width='16' height='16'/></div>";
                            }
                            else if (counter == "3")
                            {
                                e.Row.Cells[16].Text = "<div style='width:40px;padding-left:15px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN3.png' Alt='' width='16' height='16'/></div>";
                            }
                        }
                        else if (status == "5")
                        {
                            if (counter == "1")
                            {
                                e.Row.Cells[16].Text = "<div style='width:40px;padding-left:15px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CR.png' Alt='' width='16' height='16'/></div>";
                            }
                            else if (counter == "2")
                            {
                                e.Row.Cells[16].Text = "<div style='width:40px;padding-left:15px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CR2.png' Alt='' width='16' height='16'/></div>";
                            }
                            else if (counter == "3")
                            {
                                e.Row.Cells[16].Text = "<div style='width:40px;padding-left:15px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CR3.png' Alt='' width='16' height='16'/></div>";
                            }
                        }
                        else if (status == "6")
                        {
                            e.Row.Cells[16].Text = "<div style='width:40px;padding-left:15px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/OutSkipped.png' Alt='' width='16' height='16'/></div>";
                        }
                    }
                    else
                    {
                        if (status == "0")
                        {
                            e.Row.Cells[16].Text = "<div style='width:40px;padding-left:15px;'></div>";
                        }
                        else if (status == "1")
                        {
                            e.Row.Cells[16].Text = "<div style='width:40px;padding-left:15px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/QC_WIPR.png' Alt='' width='16' height='16'/></div>";
                        }
                        else if (status == "2")
                        {
                            e.Row.Cells[16].Text = "<div style='width:40px;padding-left:15px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/QC_WIP_NearR.png' Alt='' width='16' height='16'/></div>";
                        }
                        else if (status == "3")
                        {
                            e.Row.Cells[16].Text = "<div style='width:40px;padding-left:15px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/QC_WIP_DelayR.png' Alt='' width='16' height='16'/></div>";
                        }
                        else if (status == "4")
                        {
                            if (counter == "1")
                            {
                                e.Row.Cells[16].Text = "<div style='width:40px;padding-left:15px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CNR.png' Alt='' width='16' height='16'/></div>";
                            }
                            else if (counter == "2")
                            {
                                e.Row.Cells[16].Text = "<div style='width:40px;padding-left:15px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CNR2.png' Alt='' width='16' height='16'/></div>";
                            }
                            else if (counter == "3")
                            {
                                e.Row.Cells[16].Text = "<div style='width:40px;padding-left:15px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CNR3.png' Alt='' width='16' height='16'/></div>";
                            }
                        }
                        else if (status == "5")
                        {
                            if (counter == "1")
                            {
                                e.Row.Cells[16].Text = "<div style='width:40px;padding-left:15px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CRR.png' Alt='' width='16' height='16'/></div>";
                            }
                            else if (counter == "2")
                            {
                                e.Row.Cells[16].Text = "<div style='width:40px;padding-left:15px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CRR2.png' Alt='' width='16' height='16'/></div>";
                            }
                            else if (counter == "3")
                            {
                                e.Row.Cells[16].Text = "<div style='width:40px;padding-left:15px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CRR3.png' Alt='' width='16' height='16'/></div>";
                            }
                        }
                        else if (status == "6")
                        {
                            e.Row.Cells[16].Text = "<div style='width:40px;padding-left:15px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/OutSkipped.png' Alt='' width='16' height='16'/></div>";
                        }
                    }
                }
                catch (Exception ex)
                { }  


                //VAS
                try
                {
                    string getVAS = (e.Row.Cells[17].Text.Trim() == "&nbsp;" ? "" : e.Row.Cells[17].Text.Trim());
                    string[] VASParts = { };
                    if (getVAS.Contains("|"))
                    {
                        VASParts = getVAS.Split('|');
                    }
                    //e.Row.Cells[17].Attributes.Add("onmouseover", "showProcessInOut(event,'" + RefNo + "','" + VASParts[2].Replace("#", "").Replace("$", "").Replace("*", "") + "','VAS')");
                    //e.Row.Cells[17].Attributes.Add("onmouseout", "hideTooltip(event)");

                    if (e.Row.Cells[17].Text.ToString().Contains('*'))
                    {
                        status = e.Row.Cells[17].Text.ToString().Substring(0, 1);
                        if (status == "0")
                            if (e.Row.Cells[17].Text.ToString().Contains('$'))
                                e.Row.Cells[17].Text = "<div style='width:40px;padding-left:10px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/notRequired.png' Alt=''  width='16' height='16'/></div>";
                            else
                                e.Row.Cells[17].Text = "<div style='width:40px;padding-left:10px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/PK4.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "1")
                            e.Row.Cells[17].Text = "<div style='width:40px;padding-left:10px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/PK.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "2")
                            e.Row.Cells[17].Text = "<div style='width:40px;padding-left:10px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/PK1.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "3")
                            e.Row.Cells[17].Text = "<div style='width:40px;padding-left:10px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/PK3.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "4")
                            e.Row.Cells[17].Text = "<div style='width:40px;padding-left:10px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN.png' Alt=''  width='16' height='18'/></div>";
                        else if (status == "5")
                            e.Row.Cells[17].Text = "<div style='width:40px;padding-left:10px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CR.png' Alt=''  width='16' height='18'/></div>";
                        else if (status == "6")
                            e.Row.Cells[17].Text = "<div style='width:40px;padding-left:10px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/OutSkipped.png' Alt=''  width='16' height='16'/></div>";
                    }
                    else
                    {
                        status = e.Row.Cells[17].Text.ToString().Substring(0, 1);
                        if (status == "0")
                            if (e.Row.Cells[17].Text.ToString().Contains('$'))
                                e.Row.Cells[17].Text = "<div style='width:40px;padding-left:10px;'></div>";
                            else
                                e.Row.Cells[17].Text = "<div style='width:40px;padding-left:10px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/K4.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "1")
                            e.Row.Cells[17].Text = "<div style='width:40px;padding-left:10px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/PK.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "2")
                            e.Row.Cells[17].Text = "<div style='width:40px;padding-left:10px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/PK1.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "3")
                            e.Row.Cells[17].Text = "<div style='width:40px;padding-left:10px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/PK3.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "4")
                            e.Row.Cells[17].Text = "<div style='width:40px;padding-left:10px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "5")
                            e.Row.Cells[17].Text = "<div style='width:40px;padding-left:10px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CR.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "6")
                            e.Row.Cells[17].Text = "<div style='width:40px;padding-left:10px;'><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/OutSkipped.png' Alt=''  width='16' height='16'/></div>";
                    }
                }
                catch (Exception ex)
                { }

                //JCC - 00-JCC not Closed, 10-JCC Close But not Informed , 11-JCC Close But Informed
                try
                {
                    string[] jcc_str = e.Row.Cells[18].Text.ToString().Trim().Split('|');
                    if (jcc_str[0] == "0-0")
                    {
                        e.Row.Cells[18].Text = "<div style=width:30px;></div>";
                        e.Row.Cells[18].ToolTip = "not Closed";
                    }
                    else if (jcc_str[0] == "1-0")
                    {
                        e.Row.Cells[18].Text = "<div style=width:30px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/JCC_SKIP.png' Alt=''  width='16' height='16'/></div>";
                        e.Row.Cells[18].ToolTip = "Closed but not informed [" + jcc_str[1].Trim() + "]";
                    }
                    else if (jcc_str[0] == "1-1")
                    {
                        e.Row.Cells[18].Text = "<div style=width:30px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/JCC_ONTIME.png' Alt=''  width='16' height='16'/></div>";
                        e.Row.Cells[18].ToolTip = "Closed and informed [" + jcc_str[1].Trim() + "]";
                    }
                }
                catch (Exception ex)
                { }

                //PTD Status : 0-not Define, 1-Within Time, 2-Apporching Time, 3-Delayed
                try
                {
                    string getIFB = (e.Row.Cells[19].Text.Trim() == "&nbsp;" ? "" : e.Row.Cells[19].Text.Trim());
                    if (getIFB == "1")
                        e.Row.Cells[19].ToolTip = "On Time";
                    else if (getIFB == "0")
                        e.Row.Cells[19].ToolTip = "No PDT";
                    else if (getIFB == "2")
                        e.Row.Cells[19].ToolTip = "Approaching";
                    else
                        e.Row.Cells[19].ToolTip = "Delayed";

                    if (e.Row.Cells[19].Text.ToString() == "0")
                        e.Row.Cells[19].Text = "<div style=width:20px;></div>";
                    else if (e.Row.Cells[19].Text.ToString() == "1")
                        e.Row.Cells[19].Text = "<div style=width:20px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/circle_green.png' Alt=''  width='14' height='14'/></div>";
                    else if (e.Row.Cells[19].Text.ToString() == "2")
                        e.Row.Cells[19].Text = "<div style=width:20px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/circle_yellow.png' Alt=''  width='14' height='14'/></div>";
                    else if (e.Row.Cells[19].Text.ToString() == "3")
                        e.Row.Cells[19].Text = "<div style=width:20px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/circle_red.png' Alt=''  width='14' height='14'/></div>";
                }
                catch (Exception ex)
                { }

                //PDT
                try
                {
                    if (e.Row.Cells[20].Text.ToString().Contains('#'))
                    {
                        if (e.Row.Cells[20].Text.ToString().Contains('Y'))
                        {
                            e.Row.Cells[20].Text = "<div style=width:65px;color:BLUE;>" + e.Row.Cells[20].Text.Replace('#', ' ').Replace('Y', ' ').ToString().Trim() + "</div>";
                        }
                        else
                        {
                            e.Row.Cells[20].Text = "<div style=width:65px;color:RED;>" + e.Row.Cells[20].Text.Replace('#', ' ').Replace('N', ' ').ToString().Trim() + "</div>";
                        }
                    }
                    else
                    {
                        e.Row.Cells[20].Text = "<div style=width:65px;color:GRAY;>" + e.Row.Cells[20].Text.Replace('$', ' ').ToString().Trim() + "</div>";
                    }
                }
                catch (Exception ex)
                { }

                try
                {
                    e.Row.Cells[21].Text = "<div style=width:20px;>" + e.Row.Cells[21].Text.ToString() + "</div>";
                }
                catch (Exception ex)
                { }

                //Remarks : 0-No Remarks, 1-Remarks
                try
                {
                    if (e.Row.Cells[22].Text.Trim() == "1")
                    {
                        //e.Row.Cells[22].Attributes.Add("onmouseover", "showRemarks(event,'" + RefNo + "')");
                        //e.Row.Cells[22].Attributes.Add("onmouseout", "hideTooltip(event)");
                    }

                    if (e.Row.Cells[22].Text.ToString() == "0")
                        e.Row.Cells[22].Text = "<div style=width:30px;></div>";
                    else if (e.Row.Cells[22].Text.ToString() == "1")
                        e.Row.Cells[22].Text = "<div style=width:30px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/REMARKS_ENTRY_Green.png' Alt=''  width='16' height='16'/></div>";
                    else if (e.Row.Cells[22].Text.ToString() == "2")
                        e.Row.Cells[23].Text = "<div style=width:30px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/REMARKS_ENTRY.png' Alt=''  width='16' height='16'/></div>";
                }
                catch (Exception ex)
                { }
                //EDT

                try
                {
                    //string VehReadyTime = getVehicleReadyTime(Convert.ToInt32(RefNo));
                    DateTime PDTTime = Convert.ToDateTime(getVehicleReadyTime(Convert.ToInt32(RefNo)));

                    if (e.Row.Cells[24].Text != "")
                    {
                        if (Convert.ToDateTime(e.Row.Cells[24].Text) < PDTTime)
                        {
                            if (Convert.ToDateTime(e.Row.Cells[24].Text).ToString("dd/MM") != DateTime.Now.ToString("dd/MM"))
                            {
                                e.Row.Cells[24].Text = "<div style='text-align:left;'>" + Convert.ToDateTime(e.Row.Cells[24].Text).ToString("HH:mm d/M") + "</div>";
                            }
                            else
                            {
                                e.Row.Cells[24].Text = "<div style='text-align:left;'>" + Convert.ToDateTime(e.Row.Cells[24].Text).ToString("HH:mm") + "</div>";
                            }
                        }
                        else
                        {
                            if (Convert.ToDateTime(e.Row.Cells[24].Text).ToString("dd/MM") != DateTime.Now.ToString("dd/MM"))
                            {
                                e.Row.Cells[24].Text = "<div style='color:red;text-align:left;'>" + Convert.ToDateTime(e.Row.Cells[24].Text).ToString("HH:mm d/M") + "</div>";
                            }
                            else
                            {
                                e.Row.Cells[24].Text = "<div style='color:red;text-align:left;'>" + Convert.ToDateTime(e.Row.Cells[24].Text).ToString("HH:mm") + "</div>";
                            }
                        }
                    }
                    else
                    {
                        e.Row.Cells[24].Text = "<div>" + e.Row.Cells[24].Text + "</div>";

                    }
                    //if (Convert.ToDateTime(e.Row.Cells[24].Text).ToString("dd/MM") != DateTime.Now.ToString("dd/MM"))
                    //{
                    //    if (Convert.ToDateTime(e.Row.Cells[24].Text).ToString("dd/MM") == PDTTime.ToString())
                    //    {
                    //        e.Row.Cells[24].Text = "<div>" + Convert.ToDateTime(e.Row.Cells[24].Text).ToString("HH:mm") + "</div>";
                    //    }
                    //    else
                    //    {
                    //        e.Row.Cells[24].Text = "<div>" + Convert.ToDateTime(e.Row.Cells[24].Text).ToString("HH:mm d/M") + "</div>";

                    //    }
                    //}
                    //else
                    //{
                    //    e.Row.Cells[24].Text = "<div>" + Convert.ToDateTime(e.Row.Cells[24].Text).ToString("HH:mm") + "</div>";
                    //}

                }
                catch (Exception exz)
                {

                }
                //try
                //{
                //    string VehReadyTime = getVehicleReadyTime(Convert.ToInt32(RefNo));
                //    if (VehReadyTime != "")
                //    {
                //        if (Convert.ToDateTime(e.Row.Cells[24].Text).ToString("dd/MM") == Convert.ToDateTime(VehReadyTime).ToString("dd/MM"))
                //        {
                //            if (Convert.ToDateTime(e.Row.Cells[24].Text) <= Convert.ToDateTime(VehReadyTime))
                //            {
                //                e.Row.Cells[24].Text = "<div  style=color:red>" + Convert.ToDateTime(e.Row.Cells[24].Text).ToString("HH:mm") + "</div>";
                //            }
                //            else
                //            {
                //                e.Row.Cells[24].Text = "<div  style=color:green>" + Convert.ToDateTime(e.Row.Cells[24].Text).ToString("HH:mm") + "</div>";
                //            }
                //        }
                //        else
                //        {
                //            e.Row.Cells[24].Text = "<div  style=color:green>" + Convert.ToDateTime(e.Row.Cells[24].Text).ToString("HH:mm dd/MM") + "</div>";
                //        }

                //    }

                //    else if (Convert.ToDateTime(e.Row.Cells[24].Text) < DateTime.Now)
                //    {
                //        if (Convert.ToDateTime(e.Row.Cells[24].Text).ToString("dd/MM") == DateTime.Now.ToString("dd/MM"))
                //        {
                //            e.Row.Cells[24].Text = "<div style=color:red>" + Convert.ToDateTime(e.Row.Cells[24].Text).ToString("HH:mm") + "</div>";
                //        }
                //        else
                //        {
                //            e.Row.Cells[24].Text = "<div style=color:red>" + Convert.ToDateTime(e.Row.Cells[24].Text).ToString("HH:mm dd/MM") + "</div>";
                //        }
                //    }
                //    else
                //    {
                //        if (Convert.ToDateTime(e.Row.Cells[24].Text).ToString("dd/MM") == DateTime.Now.ToString("dd/MM"))
                //        {
                //            e.Row.Cells[24].Text = "<div>" + Convert.ToDateTime(e.Row.Cells[24].Text).ToString("HH:mm") + "</div>";
                //        }
                //        else
                //        {
                //            e.Row.Cells[24].Text = "<div>" + Convert.ToDateTime(e.Row.Cells[24].Text).ToString("HH:mm dd/MM") + "</div>";
                //        }
                //    }
                //}
                //catch (Exception exz)
                //{

                //}
            }
        }
    }
    private string getVehicleReadyTime(int Refid)
    {
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlCommand cmd = new SqlCommand("select CAST(ISNULL(RevisedPromisedTime,PromisedTime) As Datetime) from tblMaster where Slno=@Refid", con);
        cmd.Parameters.AddWithValue("@Refid", Refid);
        DataTable dt = new DataTable();
        SqlDataAdapter sdt = new SqlDataAdapter(cmd);
        sdt.Fill(dt);
        return dt.Rows[0][0].ToString();
    }
    protected void grdDisplay_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        lblmsg.CssClass = "reset";
        lblvehicleno.Text = grdDisplay.SelectedRow.Cells[1].Text.Trim().Replace("<font size='4' color='red'> *</font>", "");
        lblpdtvehno.Text = grdDisplay.SelectedRow.Cells[1].Text.Trim().Replace("<font size='4' color='red'> *</font>", "");
        lblspvehicleno.Text = grdDisplay.SelectedRow.Cells[1].Text.Trim().Replace("<font size='4' color='red'> *</font>", "");
        lblvehno.Text = grdDisplay.SelectedRow.Cells[1].Text.Trim().Replace("<font size='4' color='red'> *</font>", "");
    }

    protected void hfRemarksData_ValueChanged(object sender, EventArgs e)
    {
    }
    public int validate_vrn(string vrn)
    {

        /*
         * 
         *  first 2 letters state
         *  second 2 sequance no of district
         *  third 2 digit letters
         *  four 4 digit uniq no
         * 
         * */

        string sub1 = "";
        vrn = vrn.ToUpper();
        string[] state_code = { "AN", "AP", "AR", "AS", "BR", "CG", "CH", "DD", "DL", "DN", "GA", "GJ", "HR", "HP", "JH", "JK", "KA", "KL",
                                "LD", "MH", "ML", "MN", "MP", "MZ", "NL", "OD", "PB", "PY", "RJ", "SK", "TN", "TR", "TS", "UK", "UP", "WB"};

        string sub2 = vrn[2].ToString() + vrn[3].ToString();

        int flag = 0;

        if (vrn.Length > 10)
        {
            flag = 1;
            if (vrn.Length == 17)
                flag = 0;
        }
        else if (vrn.Length > 1)
        {
            sub1 = vrn[0].ToString() + vrn[1].ToString();

            for (int i = 0; i < state_code.Length; i++)
            {
                flag = 2;
                if (sub1 == state_code[i])
                {
                    if (!Regex.IsMatch(sub2, @"^\d+$"))
                    {
                        flag = 3;
                        break;
                    }
                    flag = 0;
                    break;
                }
            }


        }
        return flag;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            
            if (Session[Session["TMLDealercode"] + "-TMLConString"] == null || Session[Session["TMLDealercode"] + "-TMLConString"].ToString() == "")
                Response.Redirect("login.aspx");

        }
        catch
        {
            Response.Redirect("login.aspx");
        }
        SqlDataSource1.ConnectionString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        srcServiceType.ConnectionString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        srcVehicleModel.ConnectionString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        SqlDataSource2.ConnectionString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        SqlDataSource4.ConnectionString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        SqlDataSource5.ConnectionString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        lbl_currTime.Text = DateTime.Now.ToString("dd MMM yyyy, HH:mm:ss");
        form1.Style.Value = "left:0px;";
        lblmsg.Text = "";
        lblmsg.CssClass = "reset";
        try
        {
            if (Session["ROLE"] == null ||  Session["ROLE"].ToString() != "SERVICE ADVISOR" && Session["ROLE"].ToString() != "CRM" && Session["ROLE"].ToString() != "DISPLAY" && Session["ROLE"].ToString() != "GMSERVICE" && Session["ROLE"].ToString() != "SM")
            {
                Response.Redirect("login.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("login.aspx");
        }
        try
        {
            //lbScroll0.Text = Session["DealerName"].ToString();
            if (!Page.IsPostBack)
            {
                TxtDate1.Attributes.Add("readonly", "readonly");
                TxtDate2.Attributes.Add("readonly", "readonly");
                txtRevPDT.Text = DateTime.Now.ToShortDateString();
                FillRemarksTemplate(6, ref ddlSRemarks);
                FillRemarksTemplate(5, ref ddVOutRemarks);
                FillRemarksTemplate(2, ref ddPDTRemarks);
                FillRemarksTemplate(4, ref CmbCancelationRemarks);
                getSA();
                getTL();
                TabContainer1.Visible = false;
                ClientScriptManager script = Page.ClientScript;
                script.RegisterStartupScript(this.GetType(), "Alert", "FormWidth();", true);
                if (Page.Request.QueryString["Back"] != null)
                {
                    BackTo = Session["Role"].ToString();
                }
              
                else if (Session["ROLE"].ToString() == "CRM")
                {
                    Session["CURRENT_PAGE"] = "CRM Display";
                    TabPanel12.Visible = true;
                    TabPanel10.Visible = true;
                    TabPanel6.Visible = false;
                    TabPanel2.Visible = true;
                    TabPanel3.Visible = true;
                    TabPanel4.Visible = true;
                    TabPanel5.Visible = true;
                    TabPanel7.Visible = false;
                    TabPanel8.Visible = false;
                    TabPanel13.Visible = false;
                   // TabContainer1.ActiveTabIndex = 0;
                    TabPanel9.Visible = true;
                    getSACRM("");
                 
                }
                else if (Session["ROLE"].ToString() == "DISPLAY" || Session["ROLE"].ToString() == "SM" || Session["ROLE"].ToString() == "GMSERVICE")
                {
                    Session["CURRENT_PAGE"] = "CRM Display";
                    TabPanel12.Visible = false;
                    TabPanel10.Visible = false;
                    TabPanel6.Visible = false;
                    TabPanel2.Visible = false;
                    TabPanel3.Visible = false;
                    TabPanel4.Visible = false;
                    TabPanel5.Visible = false;
                    TabPanel7.Visible = false;
                    TabPanel8.Visible = false;
                    TabPanel9.Visible = false;
                    TabPanel11.Visible = false;
                    TabPanel13.Visible = false;
                }
            }
                    //lbl_CurrentPage.Text = Session["CURRENT_PAGE"].ToString();
                  lbl_LoginName.Text = Session["UserId"].ToString() + "&nbsp;&nbsp;" ;
                //try
                //{
                //    if (!Page.IsPostBack)
                //    {
                //    }
                //    else
                //    {
                //        if (!(Request.Form["_EventTarget"] != null && Request.Form["_EventTarget"] == "myClick"))
                //        {
                //            grdDisplay.SelectedIndex = int.Parse(Request.Form["__EVENTARGUMENT"].ToString());
                //            if (Session["Role"].ToString() == "SM")
                //                TabContainer1.Visible = false;
                //            else if (grdDisplay.SelectedRow.Cells[1].Text.Replace("&nbsp;", "").Trim() != "")
                //            {
                //                string regno = GetRegNo(grdDisplay.SelectedRow.Cells[1].Text.Trim());
                //                DataTable dt1 = GetAll(grdDisplay.SelectedRow.Cells[1].Text.Trim());
                //                lblvehno.Text = regno;
                //                lblvehicleUPDTag.Text = regno;
                //                txtvehicle.Text = regno;
                //                lblSARegNo.Text = regno;
                //                txtCustName.Text = dt1.Rows[0]["CustomerName"].ToString();
                //                txtmobile.Text = dt1.Rows[0]["CustomerPhone"].ToString();
                //                txtemailid.Text = dt1.Rows[0]["Email"].ToString();
                //                txtevehno.Text = regno;
                //                lblJCCvehno.Text = regno;
                //                lblTagNo.Text = grdDisplay.SelectedRow.Cells[1].Text.Trim();
                //                lblSlNoSA.Text = grdDisplay.SelectedRow.Cells[23].Text.Replace("&nbsp;", "").Trim();
                //                ddPDTRemarks.SelectedIndex = -1;
                //                CmbCancelationRemarks.SelectedIndex = 0;
                //                txtPDTComment.Visible = false;
                //                lblpdtvehno.Text = regno;
                //                lblspvehicleno.Text = regno;
                //                lblvcvehicleno.Text = regno;
                //                lblvovehicleno.Text = regno;
                //                TabContainer1.Visible = true;
                //                clear();
                //            }
                //            else
                //            {
                //                TabContainer1.Visible = false;
                //            }
                //        }
                //    }
                //}
                //catch (Exception ex)
                //{
                //    TabContainer1.Visible = false;
                //}
            

            try
            {
                //if (ScriptManager1.IsInAsyncPostBack)
                //{
                //    if (Request["__EVENTTARGET"] == Timer1.ClientID)
                //    {
                //        BindGrid();
                //        TabContainer1.Visible = false;
                //        if (rbType.SelectedIndex == 0)
                //        {
                //            LinkPrevVehicles("1");
                //        }
                //        else
                //        {
                //            LinkPrevVehicles("0");
                //        }
                //        PnlDisplay.Update();
                //    }
                //    else
                //    {
                //        BindGrid();
                //        TabContainer1.Visible = false;
                //    }
                //}
                //else
                //{
                //    BindGrid();
                //    if (rbType.SelectedIndex == 0)
                //    {
                //        LinkPrevVehicles("1");
                //    }
                //    else
                //    {
                //        LinkPrevVehicles("0");
                //    }
                //}
                if (!(Request.Form["_EventTarget"] != null && Request.Form["_EventTarget"] == "myClick"))
                {
                    BindGrid();
                    grdDisplay.SelectedIndex = int.Parse(Request.Form["__EVENTARGUMENT"].ToString());
                    if (grdDisplay.SelectedRow.Cells[1].Text.Replace("&nbsp;", "").Trim().Replace("<div style='width:30px;margin-left:-15px;'>", "").Replace("</div>", "") != "")
                    {
                        string regno = GetRegNo(grdDisplay.SelectedRow.Cells[1].Text.Replace("&nbsp;", "").Trim().Replace("<div style='width:30px;margin-left:-15px;'>", "").Replace("</div>", ""));
                        DataTable dt2 = GetAll(grdDisplay.SelectedRow.Cells[1].Text.Replace("&nbsp;", "").Trim().Replace("<div style='width:30px;margin-left:-15px;'>", "").Replace("</div>", ""));
                        lblvehicleno.Text = regno;
                        lblvehno.Text = regno;
                        lblJCCvehno.Text = regno;
                        lblvehicleUPDTag.Text = regno;
                        txtvehicle.Text = regno;
                        lblSARegNo.Text = regno;
                        lblVehnoforHold.Text = regno;
                        txtCustName.Text = dt2.Rows[0]["CustomerName"].ToString();
                        txtmobile.Text = dt2.Rows[0]["CustomerPhone"].ToString();
                        txtemailid.Text = dt2.Rows[0]["Email"].ToString();
                        txtevehno.Text = regno;
                        ddPDTRemarks.SelectedIndex = -1;
                        txtPDTComment.Visible = false;
                        txtcardno.Text = grdDisplay.SelectedRow.Cells[1].Text.Replace("&nbsp;", "").Trim().Replace("<div style='width:30px;margin-left:-15px;'>", "").Replace("</div>", "");
                        lblTagNo.Text = grdDisplay.SelectedRow.Cells[1].Text.Replace("&nbsp;", "").Trim().Replace("<div style='width:30px;margin-left:-15px;'>", "").Replace("</div>", "");
                        lblTagnoforHold.Text = grdDisplay.SelectedRow.Cells[1].Text.Replace("&nbsp;", "").Trim().Replace("<div style='width:30px;margin-left:-15px;'>", "").Replace("</div>", "");
                        lblpdtvehno.Text = regno;
                        lblspvehicleno.Text = regno;
                        int SlNo = 0;
                        SlNo = Convert.ToInt32(grdDisplay.SelectedRow.Cells[23].Text.Replace("&nbsp;", "").Trim());
                        lblSlNoSA.Text = grdDisplay.SelectedRow.Cells[23].Text.Replace("&nbsp;", "").Trim();
                        lblSlnoforHold.Text = grdDisplay.SelectedRow.Cells[23].Text.Replace("&nbsp;", "").Trim();
                        getSACRM(lblSlNoSA.Text);
                        lblvcvehicleno.Text = regno;
                        lblvovehicleno.Text = regno;
                        lblrefno.Text = grdDisplay.SelectedRow.Cells[23].Text.Replace("&nbsp;", "").Trim();
                        getPDTPDC(grdDisplay.SelectedRow.Cells[23].Text.Replace("&nbsp;", "").Trim());
                        getRemainingTabs(grdDisplay.SelectedRow.Cells[23].Text.Replace("&nbsp;", "").Trim(), grdDisplay.SelectedRow.RowIndex.ToString());
                        getTagCancelation(SlNo);
                        TabContainer1.Visible = true;
                        clear();
                    }
                    else
                    {
                        TabContainer1.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                if (!Page.IsPostBack)
                {
                    btnSearch_Click(null, null);
                    lblmsg.Text = "";
                    lblmsg.CssClass = "reset";
                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("login.aspx");
        }
    }

    protected void rbType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbType.SelectedIndex == 0)
        {
            LinkPrevVehicles("1");
        }
        else
        {
            LinkPrevVehicles("0");
        }

        try
        {
            txtVehicleNumber.Text = "";
            txtTagNo.Text = "";
            lblmsg.Text = "";
            lblmsg.CssClass = "reset";
            BindGrid();
            TabContainer1.Visible = false;
        }
        catch (Exception ex)
        {
            lblLoading.Text = ex.Message;
        }
    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {
        Page_Load(null, null);
    }

    protected void Timer3_Tick(object sender, EventArgs e)
    {
        lblmsg.ForeColor = Color.Green;
    }

    protected void txtRevPDT_TextChanged(object sender, EventArgs e)
    {
        cmbHH.Text = "";
    }

    private static DataTable GetEmployeeHover(string Slno, string EmpId)
    {
        try
        {

        DataTable dt = new DataTable();
        SqlConnection con = new SqlConnection(System.Web.HttpContext.Current.Session[HttpContext.Current.Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlCommand cmd = new SqlCommand("GetEmployeeHover", con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Slno", Slno);
        cmd.Parameters.AddWithValue("@EmpId", EmpId);
        if (con.State != ConnectionState.Open)
            con.Open();
        cmd.ExecuteNonQuery();
        sda.Fill(dt);
        if (con.State != ConnectionState.Closed)
            con.Close();
        return dt;

        }
        catch (Exception ex)
        {
            return null;
        }
    }

    private static DataTable GetProcessHover(string RefNo, string ProcessId)
    {
        try
        {

        DataTable dt = new DataTable();
        SqlConnection con = new SqlConnection(System.Web.HttpContext.Current.Session[HttpContext.Current.Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlCommand cmd = new SqlCommand("GetProcessHover", con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@RefNo", RefNo);
        cmd.Parameters.AddWithValue("@ProcessId", ProcessId);
        if (con.State != ConnectionState.Open)
            con.Open();
        cmd.ExecuteNonQuery();
        sda.Fill(dt);
        if (con.State != ConnectionState.Closed)
            con.Close();
        return dt;

        }
        catch (Exception ex)
        {
            return null;
        }
    }

    private static DataTable GetRegNoHover(string RefNo)
    {
        DataTable dt = new DataTable();
        SqlConnection con = new SqlConnection(System.Web.HttpContext.Current.Session[HttpContext.Current.Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlCommand cmd = new SqlCommand("GetRefNoHover", con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@RefNo", RefNo);
        if (con.State != ConnectionState.Open)
            con.Open();
        cmd.ExecuteNonQuery();
        sda.Fill(dt);
        if (con.State != ConnectionState.Closed)
            con.Close();
        return dt;
    }

    private static DataTable GetRemarksHover(string RefNo)
    {
        DataTable dt = new DataTable();
        SqlConnection con = new SqlConnection(System.Web.HttpContext.Current.Session[HttpContext.Current.Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlCommand cmd = new SqlCommand("GetRemarksHover", con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@RefNo", RefNo);
        if (con.State != ConnectionState.Open)
            con.Open();
        cmd.ExecuteNonQuery();
        sda.Fill(dt);
        if (con.State != ConnectionState.Closed)
            con.Close();
        return dt;
    }

    private static DataTable GetJAHover(string RefNo)
    {
        DataTable dt = new DataTable();
        SqlConnection con = new SqlConnection(System.Web.HttpContext.Current.Session[HttpContext.Current.Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlCommand cmd = new SqlCommand("GetJAHover", con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@RefNo", RefNo);
        if (con.State != ConnectionState.Open)
            con.Open();
        cmd.ExecuteNonQuery();
        sda.Fill(dt);
        if (con.State != ConnectionState.Closed)
            con.Close();
        return dt;
    }

    private static DataTable GetPartsHover(string RefNo)
    {
        DataTable dt = new DataTable();
        SqlConnection con = new SqlConnection(System.Web.HttpContext.Current.Session[HttpContext.Current.Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlCommand cmd = new SqlCommand("udpHoverPartsRequisition", con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@RefId", RefNo);
        if (con.State != ConnectionState.Open)
            con.Open();
        cmd.ExecuteNonQuery();
        sda.Fill(dt);
        if (con.State != ConnectionState.Closed)
            con.Close();
        return dt;
    }

    private static DataTable GetSubProcessHover(string RefNo, string SubProcessId)
    {
        DataTable dt = new DataTable();
        SqlConnection con = new SqlConnection(System.Web.HttpContext.Current.Session[HttpContext.Current.Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlCommand cmd = new SqlCommand("GetSubProcessHover", con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@RefNo", RefNo);
        cmd.Parameters.AddWithValue("@SubProcessId", SubProcessId);
        if (con.State != ConnectionState.Open)
            con.Open();
        cmd.ExecuteNonQuery();
        sda.Fill(dt);
        if (con.State != ConnectionState.Closed)
            con.Close();
        return dt;
    }

    private int CheckPromisedTime(String RegNo)
    {
        int RefNo = GetRefNo(RegNo, Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlCommand cmd = new SqlCommand("", con);
        cmd.CommandText = "SELECT PromisedTime FROM tblMaster WHERE slno=@RefNo";
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@RefNo", RefNo);
        DateTime pt;
        try
        {
            if (con.State != ConnectionState.Open)
                con.Open();

            pt = Convert.ToDateTime(cmd.ExecuteScalar());
            if (pt < DateTime.Now)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }
        catch
        {
            return 0;
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }
    public  int GetRefNo(string RegNo, string Connectionstring)
    {
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        //OpenDBConnection(ref con);

        string cmdstr = "SELECT top (1) Slno from tblMaster where RegNo = @carnum AND Position <> 'Delivered' order by slno desc";
        SqlCommand cmd = new SqlCommand(cmdstr, con);
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@carnum", RegNo.ToUpper());
        try
        {
            int slno = 0;
            con.Open();
            slno = Convert.ToInt32(cmd.ExecuteScalar());
            return slno;
        }
        catch (Exception ex)
        {
            return 0;
        }
        finally
        {
           
            con.Close();
        }
    }

    private void FillRemarksTemplate(int Type, ref DropDownList ddl)
    {
        // 1-JCCRemarks ,2-PDTRemarks,3-Vehicle Cancelation ,4-Vehicle Tag Cancellation,5-Vehicle OUT,6-Service Remarks,7-Process Remarks
        try
        {
            SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
            SqlCommand cmd = new SqlCommand("GetRemarksTemplate", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RType", Type);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("Select", "0"));
            if (dt.Rows.Count > 0)
            {
                ddl.DataSource = dt;
                ddl.DataValueField = "SlNo";
                ddl.DataTextField = "RemarksTemplate";
                ddl.DataBind();
                txtspremarks.Visible = false;
                txt_VORemarks.Visible = false;
                txtCancelationRemark.Visible = false;
            }
            else
            {
                txtspremarks.Visible = true;
                txt_VORemarks.Visible = true;
                txtCancelationRemark.Visible = true;
            }
            ddl.Items.Add(new ListItem("Other", "-1"));
        }
        catch (Exception ex) { }
    }

    private void FillVehicleStatus()
    {
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());

        try
        {
            SqlCommand cmd = new SqlCommand();
            //if (Session["ROLE"].ToString() == "SERVICE ADVISOR")
            //{
            //    cmd.CommandText = "GetCountVehicleStatusI";
            //    cmd.Parameters.AddWithValue("@EmpId", Session["EmpId"].ToString());
            //    cmd.Parameters.AddWithValue("@isBodyshop", 0);
            //}
            //else
            //{
                cmd.CommandText = "GetCountVehicleStatusForCRm";
                cmd.Parameters.AddWithValue("@EmpId", "0");
                cmd.Parameters.AddWithValue("@isBodyshop", 0);
            //}
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            if (con.State != ConnectionState.Open)
                con.Open();

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count != 0)
            {
                lbUnDelivered.Text = dt.Rows[0][0].ToString().Trim();
                lbReady.Text = dt.Rows[0][1].ToString().Trim();
                lbWIP.Text = dt.Rows[0][9].ToString().Trim();
                lbIdle.Text = dt.Rows[0][2].ToString().Trim();
                lbHold.Text = dt.Rows[0][3].ToString().Trim();
                lblVehDel.Text = dt.Rows[0][6].ToString().Trim();
                lblTotalReceived.Text = dt.Rows[0][10].ToString().Trim();
            }
            else
            {
                lbUnDelivered.Text = "0";
                lbReady.Text = "0";
                lbIdle.Text = "0";
                lbHold.Text = "0";
                lbWIP.Text = "0";
            }
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

    private DataTable GetAll(string TagNo)
    {
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        DataTable dt = new DataTable();
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select Top 1 RegNo,CustomerName,CustomerPhone,LandlineNo, Email, Slno,ServiceAdvisor from tblMaster where RFID=@TagNo And Delivered=0 order by SlNo desc";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@TagNo", TagNo);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count != 0)
            {
                return dt;
            }
            else
            {
                return dt;
            }
        }
        catch (Exception ex)
        {
            return dt;
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }

    private string getCurrentProcessInTime(GridViewRowEventArgs e)
    {
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlCommand cmd = new SqlCommand("GetCurrentProcessInTime", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@TagNo", e.Row.Cells[2].Text.ToString());
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        return dt.Rows[0]["Code"].ToString();
    }

    //private DataTable GetDisplayDate(string Type, string vehiclenumber, int param)
    //{
    //    string whichDay = string.Empty;
    //    if (rbType.SelectedIndex == 0)
    //    {
    //        whichDay = "1"; // Today
    //    }
    //    else
    //    {
    //        whichDay = "0"; // Next Day
    //    }

    //    string Aplus = string.Empty;
    //    switch (cmbCustType.SelectedValue)
    //    {
    //        case "Customer Type":
    //            Aplus = "";
    //            break;

    //        default:
    //            Aplus = cmbCustType.SelectedValue;
    //            break;
    //    }
    //    string ServiceTyp = string.Empty;
    //    if (cmbServiceType.SelectedValue == "Service Type")
    //    {
    //        ServiceTyp = "";
    //    }
    //    else
    //    {
    //        ServiceTyp = cmbServiceType.SelectedValue;
    //    }

    //    string vehicleModel = string.Empty;
    //    if (cmbVehicleModel.SelectedValue == "Model")
    //    {
    //        vehicleModel = "";
    //    }
    //    else
    //    {
    //        vehicleModel = cmbVehicleModel.SelectedValue;
    //    }

    //    string ProcessVal = string.Empty;
    //    if (cmbProcess.SelectedValue == "Process")
    //    {
    //        ProcessVal = "";
    //    }
    //    else
    //    {
    //        ProcessVal = cmbProcess.SelectedValue;
    //    }

    //    string DateFrom = string.Empty;
    //    if (TxtDate1.Text == "")
    //    {
    //        DateFrom = "";
    //    }
    //    else
    //    {
    //        DateFrom = TxtDate1.Text.Trim();
    //    }

    //    string DateTo = string.Empty;
    //    if (TxtDate2.Text == "")
    //    {
    //        DateTo = "";
    //    }
    //    else
    //    {
    //        DateTo = TxtDate2.Text.Trim();
    //    }

    //    string VehicleNo = string.Empty;
    //    if (txtVehicleNumber.Text.Trim() != "")
    //    {
    //        VehicleNo = txtVehicleNumber.Text.Trim();
    //    }
    //    else
    //    {
    //        VehicleNo = "";
    //    }

    //    string SAId = string.Empty;

    //    if (cmbSA.SelectedIndex > 0)
    //    {
    //        SAId = cmbSA.SelectedValue.ToString();
    //    }
    //    else
    //        SAId = "0";
    //    string TLId = string.Empty;
    //    if (cmbTeamLead.SelectedIndex > 0)
    //    {
    //        TLId = cmbTeamLead.SelectedValue.ToString();
    //    }
    //    else
    //        TLId = "0";

    //    string TagNo = string.Empty;
    //    if (txtTagNo.Text.Trim() == "")
    //        TagNo = "0";
    //    else
    //        TagNo = txtTagNo.Text.Trim();

    //    if (Session["ROLE"].ToString() == "SERVICE ADVISOR")
    //    {
    //        SAId = Session["EmpId"].ToString();
    //    }
    //    if (Session["ROLE"].ToString() == "CRM")
    //    {
    //        EmpId = Session["EmpId"].ToString();
    //    }
      
    //    SqlConnection oConn = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
    //    SqlCommand cmd = new SqlCommand("", oConn);
    //    DataSet oDs = new DataSet();
    //    DataTable oDt = new DataTable();
    //    cmd.Parameters.Clear();
    //    if (oConn.State != ConnectionState.Open)
    //    {
    //        oConn.Open();
    //    }

    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.CommandText = "JCDisplayI"; // JobControllerDisplayI
    //    cmd.Parameters.AddWithValue("@Day", whichDay);
    //    cmd.Parameters.AddWithValue("@Bodyshop", 0);
    //    cmd.Parameters.AddWithValue("@Floor", 0);
    //    cmd.Parameters.AddWithValue("@CustomerType", Aplus);
    //    cmd.Parameters.AddWithValue("@ServiceType", ServiceTyp);
    //    cmd.Parameters.AddWithValue("@Model", vehicleModel);
    //    cmd.Parameters.AddWithValue("@Process", ProcessVal);
    //    cmd.Parameters.AddWithValue("@DateFrom", DateFrom);
    //    cmd.Parameters.AddWithValue("@DateTo", DateTo);
    //    cmd.Parameters.AddWithValue("@RegNo", VehicleNo);
    //    cmd.Parameters.AddWithValue("@Param", "");
    //    cmd.Parameters.AddWithValue("@SAId", SAId);
    //    cmd.Parameters.AddWithValue("@TLId", TLId);
    //    cmd.Parameters.AddWithValue("@Tagno", TagNo);
    //    cmd.Parameters.AddWithValue("@Status", ddlState.SelectedValue.Trim());
    //    cmd.Parameters.AddWithValue("@indexOn", drpOrderBy.SelectedValue.Trim());
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    try
    //    {
    //        da.Fill(oDt);
    //        return oDt;
    //    }
    //    catch (SqlException ex)
    //    {
    //        throw (ex);
    //    }
    //    finally
    //    {
    //        oConn.Close();
    //    }
    //}

    public List<TMLCRMDisplay> GetCRMData(string Type, string vehiclenumber, int param)
    {
        string whichDay = string.Empty;
        if (rbType.SelectedIndex == 0)
        {
            whichDay = "1"; // Today
        }
        else
        {
            whichDay = "0"; // Next Day
        }

        string Aplus = string.Empty;
        switch (cmbCustType.SelectedValue)
        {
            case "Customer Type":
                Aplus = "";
                break;

            default:
                Aplus = cmbCustType.SelectedValue;
                break;
        }
        string ServiceTyp = string.Empty;
        if (cmbServiceType.SelectedValue == "Service Type")
        {
            ServiceTyp = "";
        }
        else
        {
            ServiceTyp = cmbServiceType.SelectedValue;
        }

        string vehicleModel = string.Empty;
        if (cmbVehicleModel.SelectedValue == "Model")
        {
            vehicleModel = "";
        }
        else
        {
            vehicleModel = cmbVehicleModel.SelectedValue;
        }

        string ProcessVal = string.Empty;
        if (cmbProcess.SelectedValue == "Process")
        {
            ProcessVal = "";
        }
        else
        {
            ProcessVal = cmbProcess.SelectedValue;
        }

        string DateFrom = string.Empty;
        if (TxtDate1.Text == "")
        {
            DateFrom = "";
        }
        else
        {
            DateFrom = TxtDate1.Text.Trim();
        }

        string DateTo = string.Empty;
        if (TxtDate2.Text == "")
        {
            DateTo = "";
        }
        else
        {
            DateTo = TxtDate2.Text.Trim();
        }

        string VehicleNo = string.Empty;
        if (txtVehicleNumber.Text.Trim() != "")
        {
            VehicleNo = txtVehicleNumber.Text.Trim();
        }
        else
        {
            VehicleNo = "";
        }

        string SAId = string.Empty;

        if (cmbSA.SelectedIndex > 0)
        {
            SAId = cmbSA.SelectedValue.ToString();
        }
        else
            SAId = "0";
        string TLId = string.Empty;
        if (cmbTeamLead.SelectedIndex > 0)
        {
            TLId = cmbTeamLead.SelectedValue.ToString();
        }
        else
            TLId = "0";

        string TagNo = string.Empty;
        if (txtTagNo.Text.Trim() == "")
            TagNo = "0";
        else
            TagNo = txtTagNo.Text.Trim();

        //if (Session["ROLE"].ToString() == "SERVICE ADVISOR")
        //{
        //    SAId = Session["EmpId"].ToString();
        //}
        if (Session["ROLE"].ToString() == "CRM")
        {
            EmpId = Session["EmpId"].ToString();
        }

        SqlConnection oConn = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlCommand cmd = new SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        if (oConn.State != ConnectionState.Open)
        {
            oConn.Open();
        }

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "JCDisplayI"; // JobControllerDisplayI
        cmd.Parameters.AddWithValue("@Day", whichDay);
        cmd.Parameters.AddWithValue("@Bodyshop", 0);
        cmd.Parameters.AddWithValue("@Floor", 0);
        cmd.Parameters.AddWithValue("@CustomerType", Aplus);
        cmd.Parameters.AddWithValue("@ServiceType", ServiceTyp);
        cmd.Parameters.AddWithValue("@Model", vehicleModel);
        cmd.Parameters.AddWithValue("@Process", ProcessVal);
        cmd.Parameters.AddWithValue("@DateFrom", DateFrom);
        cmd.Parameters.AddWithValue("@DateTo", DateTo);
        cmd.Parameters.AddWithValue("@RegNo", VehicleNo);
        cmd.Parameters.AddWithValue("@Param", "");
        cmd.Parameters.AddWithValue("@SAId", SAId);
        cmd.Parameters.AddWithValue("@TLId", TLId);
        cmd.Parameters.AddWithValue("@Tagno", TagNo);
        cmd.Parameters.AddWithValue("@Status", ddlState.SelectedValue.Trim());
        cmd.Parameters.AddWithValue("@indexOn", drpOrderBy.SelectedValue.Trim());
        SqlDataReader dr = cmd.ExecuteReader();
        List<TMLCRMDisplay> tmlcrmdisp = new List<TMLCRMDisplay>();
        while (dr.Read())
        {
            tmlcrmdisp.Add(new TMLCRMDisplay
            {
                Slno = Convert.ToInt16(dr["#"].ToString()),
                VID = Convert.ToInt16(dr["VID"].ToString()),
                VRN = dr["VRN/VIN"].ToString(),
                VIP = dr["JDP CW"].ToString(),
                MODEL = dr["MODEL"].ToString(),
                ST = dr["ST"].ToString(),
                STATUS = dr["STATUS"].ToString(),
                JC = dr["JC"].ToString(),
                PRTS = dr["PARTS"].ToString(),
                BA = dr["BA"].ToString(),
                T1 = dr["T1"].ToString(),
                T2 = dr["T2"].ToString(),
                T3 = dr["T3"].ToString(),
                WA = dr["WA"].ToString(),
                RT = dr["RT"].ToString(),
                WASH = dr["WASH"].ToString(),
                QC = dr["QC"].ToString(),
                VAS = dr["VAS"].ToString(),
                JCC = dr["JCC"].ToString(),
                PRG = dr["PRG"].ToString(),
                PDT = dr["PDT"].ToString(),
                AGE = dr["Age"].ToString(),
                RMK = dr["RMK"].ToString(),
                REFNO = Convert.ToInt32(dr["RefNo"].ToString()),
                ERT = dr["ERT"].ToString()
            });
        }
        return tmlcrmdisp;

    }

    private void getPDTPDC(string RefNo)
    {
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlCommand cmd = new SqlCommand("GetPDTPDC", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@RefNo", RefNo);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            lblpdtno.Text = dt.Rows[0]["PDT"].ToString().Replace("#", " ");
        }
    }

    private string[] GetProcessState(string ProcessString)
    {
        try
        {
            return (ProcessString.Split('|'));
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }

    private string GetRegNo(string TagNo)
    {
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select Top 1 RegNo from tblMaster where RFID=@TagNo And Delivered=0 order by SlNo desc";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@TagNo", TagNo);
            cmd.Connection = con;
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
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
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }

    private void getSAColors(string colorCode, int ColNo, GridViewRowEventArgs e)
    {
        switch (colorCode)
        {
            case "0":
                e.Row.Cells[ColNo].Text = "";
                break;

            case "1":
                e.Row.Cells[ColNo].Attributes.Add("Style", "font-size: large;color:#387C44;background-image: url('img/Process/clock_green.png'); background-repeat: no-repeat;background-size:24px 24px;");
                break;

            case "2":
                e.Row.Cells[ColNo].Attributes.Add("Style", "font-size: large;color:#387C44;background-image: url('img/Process/clock_red.png'); background-repeat: no-repeat;background-size:24px 24px;");
                break;
        }
    }

    private string GetServiceid(string RegNo)
    {
        SqlConnection Conn = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlDataReader dr = null;
        String ServiceID = string.Empty;
        SqlCommand Cmd = new SqlCommand("", Conn);
        try
        {
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "GetServiceIdForRegNo_H";
            Cmd.Parameters.Clear();
            Cmd.Parameters.AddWithValue("@RegNo", RegNo.Replace("<font size='4' color='red'> *</font>", ""));
            if (Conn.State != ConnectionState.Open)
            {
                Conn.Open();
            }
            dr = Cmd.ExecuteReader();
            if (dr.Read())
            {
                ServiceID = Convert.ToString(dr["ServiceId"].ToString());
            }
            else
            {
                ServiceID = string.Empty;
            }
            return ServiceID;
        }
        catch (Exception ex)
        {
            return string.Empty;
        }
        finally
        {
            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
            }
        }
    }

    private string GetServiceTypeImage(string ServiceType)
    {
        string sConnString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        SqlConnection oConn = new SqlConnection(sConnString);
        SqlCommand cmd = new SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "GetServiceTypeImage";
        cmd.Parameters.AddWithValue("@ServiceType", ServiceType);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            if (oDt.Rows.Count > 0)
            {
                return oDt.Rows[0]["Image"].ToString();
            }
            else
            {
                return string.Empty;
            }
        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    private string GetServiceTypeImg(string ServiceType)
    {
        try
        {
            string ImgUrl = string.Empty;
            ImgUrl = GetServiceTypeImage(ServiceType);
            if (ImgUrl != string.Empty)
            {
                return ("<img src=\"img/" + ImgUrl + "\" width=\"48px\" height=\"48px\" />");
            }
            else
            {
                return ServiceType;
            }
        }
        catch (Exception ex)
        {
            return "";
        }
        finally
        {
        }
    }

    private String GetTechnicianForId(string ServiceId)
    {
        DataTable dt = new DataTable();
        try
        {
            String query = "Select dbo.GetTechnicianForService(@ServiceId) As Technician";
            SqlCommand cmd = new SqlCommand(query, new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString()));
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ServiceId", ServiceId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["Technician"].ToString();
            }
            else
            {
                return string.Empty;
            }
        }
        catch (Exception ex)
        {
            return string.Empty;
        }
    }

    private int InsertRemarks(string Remarks, string ProcessId, string ServiceId, string RegNo)
    {
        SqlConnection Conn = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());

        try
        {
            if (Conn.State != ConnectionState.Open)
            {
                Conn.Open();
            }
            SqlCommand cmd = new SqlCommand("", Conn);
            cmd.CommandText = "INSERT INTO TblProcessRemarks (ServiceId, ProcessId, RegNo, DateOfRemarks, Remarks)VALUES" +
            "(@ServiceId, @ProcessId, @RegNo, GETDATE(), @Remarks)";
            cmd.Parameters.AddWithValue("@ServiceId", ServiceId);
            cmd.Parameters.AddWithValue("@ProcessId", ProcessId);
            cmd.Parameters.AddWithValue("@RegNo", RegNo.Replace("<font size='4' color='red'> *</font>", ""));
            cmd.Parameters.AddWithValue("@Remarks", Remarks);
            return (cmd.ExecuteNonQuery());
        }
        catch
        {
            return 0;
        }
        finally
        {
            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
            }
        }
    }

    private void LinkPrevVehicles(string typeLink)
    {
        if (typeLink == "1")
        {
            imgType.HRef = "DisplayWorks.aspx?typ=1";
        }
        else
        {
            imgType.HRef = "DisplayWorks.aspx?typ=0";
        }
    }

    private void OnIButtonClick(object Sender, EventArgs e)
    {
        string VehicleRegNo = string.Empty;
        string ProcessData = string.Empty;
        try
        {
            // Gert Prent Gridview
            GridViewRow Gr = (GridViewRow)(Sender as Control).Parent.Parent;
            int Index = Gr.RowIndex;
            VehicleRegNo = Gr.Cells[0].Text;
            // Get Parent Cell
            TableCell Tc = (TableCell)(Sender as Control).Parent;
            if (Tc.Controls.Count == 2)
            {
                HiddenField Hf = (HiddenField)Tc.Controls[1];
                ProcessData = Hf.Value;
            }
            //Timer1.Enabled = false;
        }
        catch (Exception ex)
        {
        }
    }

    private void OnRemarksButtonClick(object Sender, EventArgs e)
    {
        string Slno = string.Empty;
        string ProcessData = string.Empty;

        try
        {
            GridViewRow Gr = (GridViewRow)(Sender as Control).Parent.Parent;
            Slno = Gr.Cells[0].Text;
            //Timer1.Enabled = false;
        }
        catch (Exception ex)
        {
        }
    }

    private void ShowPopup(string ProcessId, string RegNo, string DelayMessage)
    {
        string sConnString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        SqlConnection oConn = new SqlConnection(sConnString);
        SqlCommand cmd = new SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "ProcessRemarkPopup";
        cmd.Parameters.AddWithValue("@ProcessId", ProcessId);
        cmd.Parameters.AddWithValue("@RegNo", RegNo.Replace("<font size='4' color='red'> *</font>", ""));
        cmd.Parameters.AddWithValue("@DelayMessage", DelayMessage);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            if (oDt.Rows.Count == 0)
            {
                return;
            }
            string ProcessName = string.Empty;
            string ProcessTime = string.Empty;
            string Deviation = string.Empty;

            ProcessName = oDt.Rows[0]["ProcessName"].ToString();
            ProcessTime = oDt.Rows[0]["ProcessTime"].ToString();
            Deviation = oDt.Rows[0]["IdleTime"].ToString();
        }
        catch (SqlException ex)
        {
            lblErrormsg.Text = "Error,PopUp:" + ex.Message;
            lblErrormsg.ForeColor = Color.Red;
        }
        finally
        {
            oConn.Close();
        }
    }

    #region "HOVERING"

    //[System.Web.Services.WebMethod(EnableSession = true)]
    //public static string LoadEmployeeInOutTime(string Slno, string EmpId, string Tech)
    //{
    //    try
    //    { 
    //    DataTable dt = new DataTable();
    //    dt = GetEmployeeHover(Slno, EmpId);
    //    string inTime = "";
    //    string outTime = "";
    //    string EmpName = "";
    //    if (dt.Rows.Count > 0)
    //    {
    //        inTime = dt.Rows[0][0].ToString().Replace("#", " ");
    //        outTime = dt.Rows[0][1].ToString().Replace("#", " ");
    //        EmpName = dt.Rows[0][2].ToString();
    //            string str = "<span><table class='mydatagrid' id=InOutPnl style=width:100%;><tr><td colspan=2 style='color:#a62724 !important;'><strong>" + Tech + ": " + EmpName + "</strong></td></tr><tr><td style=width:90px;>In Time: </td><td style=text-align:center;>" + inTime + "</td></tr><tr><td style=width:90px;>Out Time: </td><td style=text-align:center;>" + outTime + "</td></tr></table></span>";
    //            return str;

    //        }
    //    else
    //    {
    //        return null;
    //    }
        
    //    }
    //    catch
    //    {
    //        return null;
    //    }
    //}

    //[System.Web.Services.WebMethod(EnableSession = true)]
    //public static string LoadInOutTime(string ServiceId, string ProcessName)
    //{
    //    DataTable dt = new DataTable();
    //    dt = GetInOutTime(ServiceId, ProcessName);
    //    string inTime = "";
    //    string outTime = "";
    //    if (dt.Rows.Count > 0)
    //    {
    //        inTime = dt.Rows[0][0].ToString().Replace("#", " ");
    //        outTime = dt.Rows[0][1].ToString().Replace("#", " ");
    //    }

    //    string str = "<span><table class='mydatagrid' id=InOutPnl style=width:100%;><tr><td style=width:90px;>In Time: </td><td style=text-align:center;>" + inTime + "</td></tr><tr><td style=width:90px;>Out Time: </td><td style=text-align:center;>" + outTime + "</td></tr></table></span>";
    //    return str;
    //}

    //[System.Web.Services.WebMethod(EnableSession = true)]
    //public static string LoadProcessInOutTime(string RefNo, string ProcessId, string ProcessName)
    //{
    //    DataTable dt = new DataTable();
    //    dt = GetProcessHover(RefNo, ProcessId);
    //    string inTime = "";
    //    string outTime = "";
    //    string empName = "";
    //    if (dt.Rows.Count > 0)
    //    {
    //        inTime = dt.Rows[0][0].ToString().Replace("#", " ");
    //        outTime = dt.Rows[0][1].ToString().Replace("#", " ");
    //        empName = dt.Rows[0][2].ToString().Trim();
    //    }
    //    else
    //    {
    //        return null;
    //    }

    //    string str = "<span><table class='mydatagrid' id=InOutPnl style=width:100%;><tr style=text-align:center;color:#a62724;font-weight:bold;><td colspan=2><strong>" + ProcessName + " : " + empName + "<strong></td></tr><tr><td style=width:90px;>In Time: </td><td style=text-align:center;>" + inTime + "</td></tr><tr><td style=width:90px;>Out Time: </td><td style=text-align:center;>" + outTime + "</td></tr></table></span>";
    //    return str;
    //}

    //[System.Web.Services.WebMethod(EnableSession = true)]
    //public static string LoadRegNoHover(string RefNo)
    //{
    //    DataTable dt = new DataTable();
    //    dt = GetRegNoHover(RefNo);

    //    string tt1 = ((dt.Rows[0]["RegNo"].ToString().Trim() == "") ? "___________" : dt.Rows[0]["RegNo"].ToString());
    //    string tt3 = dt.Rows[0]["CustomerName"].ToString();
    //    string tt4 = dt.Rows[0]["CustomerPhone"].ToString();
    //    string tt5 = dt.Rows[0]["VehicleModel"].ToString();
    //    string tt6 = dt.Rows[0]["GateIn"].ToString().Replace("#", " ");
    //    string tt7 = dt.Rows[0]["Position"].ToString();
    //    string tt8 = dt.Rows[0]["JobCardNo"].ToString();
    //    string tt9 = dt.Rows[0]["ChassisNo"].ToString();
    //    string tt10 = dt.Rows[0]["KMS"].ToString();
    //    string tt11 = dt.Rows[0]["ServiceAdvisor"].ToString().Replace(",", "");
    //    string tt12 = dt.Rows[0]["EngineNo"].ToString();
    //    string tt13 = dt.Rows[0]["Date_Of_Sale"].ToString();
    //    string tt14 = dt.Rows[0]["Sold_Dealer_Id"].ToString();
    //    string tt15 = dt.Rows[0]["CurrentStatus"].ToString();
    //    string tt16 = dt.Rows[0]["GateOut"].ToString();
    //    string tt17 = dt.Rows[0]["KMS"].ToString();
    //    string str;
    //    //if (statusVal == "13" || statusVal == "14" || statusVal == "15")
    //    // str = "<span><table style='width: 210px;' cellpadding='0' cellspacing='0'><tr bgcolor='#8db4e3'><td style='width: 100px;'>REGISTRATION NO</td><td>:</td><td class='ttipBodyVal'>" + tt1 + "</td></tr><tr><td>CUSTOMER NAME</td><td>:</td><td class='ttipBodyVal'>" + tt3 + "</td><CONTACT NO</td><tr bgcolor='#8db4e3'><td><span>CONTACT NO</span></td><td>:</td><td class='ttipBodyVal'><span>" + tt4 + "</span></td></tr><tr><td style='width: 100px;'>VEHICLE IN TIME</td><td>:</td><td class='ttipBodyVal'>" + tt6 + "</tr><tr bgcolor='#8db4e3'><td style='width: 100px;'>MODEL</td><td>:</td><td class='ttipBodyVal'>&nbsp;" + tt5 + "</tr><tr><td style='width: 100px;'>ADVISOR NAME</td><td>:</td><td class='ttipBodyVal'>" + tt11 + "</tr><tr bgcolor='#8db4e3'><td >CURRENT POSITION</td><td>:</td><td>" + tt7 + "</tr><tr><td style='width: 100px;'>CURRENT STATUS</td><td>:</td><td class='ttipBodyVal'>" + tt15 + "</tr><tr bgcolor='#8db4e3'><td >VEHICLE OUT TIME</td><td>:</td><td>" + tt16 + "</tr></table></span>";
    //    //else
    //    //    str = "<span><table style='width: 210px;' cellpadding='0' cellspacing='0'><tr bgcolor='#8db4e3'><td style='width: 100px;'>REGISTRATION NO</td><td>:</td><td class='ttipBodyVal'>" + tt1 + "</td></tr><tr><td>CUSTOMER NAME</td><td>:</td><td class='ttipBodyVal'>" + tt3 + "</td><CONTACT NO</td><tr bgcolor='#8db4e3'><td><span>CONTACT NO</span></td><td>:</td><td class='ttipBodyVal'><span>" + tt4 + "</span></td></tr><tr><td style='width: 100px;'>VEHICLE IN TIME</td><td>:</td><td class='ttipBodyVal'>" + tt6 + "</tr><tr bgcolor='#8db4e3'><td style='width: 100px;'>MODEL</td><td>:</td><td class='ttipBodyVal'>&nbsp;" + tt5 + "</tr><tr><td style='width: 100px;'>ADVISOR NAME</td><td>:</td><td class='ttipBodyVal'>" + tt11 + "</tr><tr bgcolor='#8db4e3'><td >CURRENT POSITION</td><td>:</td><td>" + tt7 + "</tr><tr><td style='width: 100px;'>CURRENT STATUS</td><td>:</td><td class='ttipBodyVal'>" + tt15 + "</tr></table></span>";

    //    if (tt7 == "Delivered")
    //        str = "<table class='mydatagrid'><tr style='height:18px'><td style='color:#a62724 !important;'><strong>CUSTOMER NAME</strong></td><td>:</td><td>" + tt3 + "</td></tr><tr style='height:18px'><td style='color:#a62724 !important;'><strong>CONTACT NO</strong></td><td>:</td><td>" + tt4 + "</td></tr><tr style='height:18px'><td style='color:#a62724 !important;'><strong>IN TIME</strong></td><td>:</td><td>" + tt6 + "</td></tr><tr style='height:18px'><td style='color:#a62724 !important;'><strong>SA NAME</strong></td><td>:</td><td>" + tt11 + "</td></tr><tr style='height:18px'><td style='color:#a62724 !important;'><strong>STAGE</strong></td><td>:</td><td>" + tt7 + "</td></tr><tr style='height:18px'><td style='color:#a62724 !important;'><strong>OUT TIME</strong></td><td>:</td><td>" + tt16 + "</td></tr><tr style='height:18px'><td style='color:#a62724 !important;'><strong>KMS</strong></td><td>:</td><td>" + tt17 + "</td></tr></table></span>";
    //    else
    //        str = "<table class='mydatagrid'><tr style='height:18px'><td style='color:#a62724 !important;'><strong>CUSTOMER NAME</strong></td><td>:</td><td>" + tt3 + "</td></tr><tr style='height:18px'><td style='color:#a62724 !important;'><strong>CONTACT NO</strong></td><td>:</td><td>" + tt4 + "</td></tr><tr style='height:18px'><td style='color:#a62724 !important;'><strong>IN TIME</strong></td><td>:</td><td>" + tt6 + "</td></tr><tr style='height:18px'><td style='color:#a62724 !important;'><strong>SA NAME</strong></td><td>:</td><td>" + tt11 + "</td></tr><tr style='height:18px'><td style='color:#a62724 !important;'><strong>STAGE</strong></td><td>:</td><td>" + tt7 + "</td></tr><tr style='height:18px'><td style='color:#a62724 !important;'><strong>KMS</strong></td><td>:</td><td>" + tt17 + "</td></tr></table></span>";

    //    return str;
    //}

    //[System.Web.Services.WebMethod(EnableSession = true)]
    //public static string LoadRemarks(string RefNo)
    //{
    //    DataTable dt = new DataTable();
    //    dt = GetRemarksHover(RefNo);
    //    string str = "";
    //    if (dt.Rows.Count > 0)
    //    {
    //        str = "<table class='mydatagrid'><tr style=color:#a62724;><th style='width:120px;color:#a62724;'>TIME</th><th style='color:#a62724;'>REMARKS</span></th></tr>";
    //        for (int i = 0; i < dt.Rows.Count; i++)
    //        {
    //            str += "<tr style='font-size:13px;text-transform:uppercase;'><td>" + dt.Rows[i][0].ToString().Replace("#", " ") + "</td><td>" + dt.Rows[i][1].ToString() + "</td></tr>";
    //        }
    //        str += "</table>";
    //    }
    //    else
    //    {
    //        str = "<table class='mydatagrid' style=text-align:center;><tr><th>No Remarks</th></tr></table>";
    //    }
    //    return str;
    //}


  //  [System.Web.Services.WebMethod(EnableSession = true)]
    //public static string LoadJADetails(string RefNo)
    //{
    //    DataTable dt = new DataTable();
    //    dt = GetJAHover(RefNo);
    //    string str = "<span><table class='mydatagrid'><tr><td style='color:#a62724 !important;'><strong>Bay #</strong></td><td></td><td align='center' valign='middle' style='color:#a62724 !important;'><strong>Team Lead</strong></td><td align='center' valign='middle'></td><td align='center' valign='middle' style='color:#a62724 !important;'><strong>In Time</strong></td><td align='center' valign='middle'></td><td align='center' valign='middle' style='color:#a62724 !important;'><strong>Allotted</strong></td></tr><tr><td>" + dt.Rows[0][0].ToString() + "</td><td></td><td align='center' valign='middle'>" + dt.Rows[0][3].ToString() + "</td><td align='center' valign='middle'></td><td align='center' valign='middle'> " + dt.Rows[0][1].ToString() + "</td><td align='center' valign='middle'></td><td align='center' valign='middle'>" + dt.Rows[0][2].ToString() + "</td></tr><tr bgcolor='White'><td></td><td></td><td align='center' valign='middle'></td><td align='center' valign='middle'></td><td align='center' valign='middle'></td><td align='center' valign='middle'></td><td align='center' valign='middle'></td></tr><tr><td style='color:#a62724 !important;'><strong>Technician</strong></td><td width='10px'></td><td align='center' valign='middle'>" + dt.Rows[0][4].ToString() + "</td><td align='center' valign='middle' width='10px'></td><td align='center' valign='middle'>" + dt.Rows[0][7].ToString() + "</td><td align='center' valign='middle' width='10px'></td><td align='center' valign='middle'>" + dt.Rows[0][10].ToString() + "</td></tr><tr><td style='color:#a62724 !important;'><strong>Jobs</strong></td><td></td><td align='center' valign='middle'>" + dt.Rows[0][5].ToString() + "</td><td align='center' valign='middle'></td><td align='center' valign='middle'>" + dt.Rows[0][8].ToString() + "</td><td align='center' valign='middle'></td><td align='center' valign='middle'>" + dt.Rows[0][11].ToString() + "</td></tr><tr><td  style='color:#a62724 !important;'><strong>Allotted</strong></td><td></td><td align='center' valign='middle'>" + dt.Rows[0][6].ToString() + "</td><td align='center' valign='middle'></td><td align='center' valign='middle'> " + dt.Rows[0][9].ToString() + "</td><td align='center' valign='middle'></td><td align='center' valign='middle'>" + dt.Rows[0][12].ToString() + "</td></tr></table>";               
    //    return str;      
    //}

    [System.Web.Services.WebMethod(EnableSession = true)]
    //public static string LoadPartsDetails(string RefNo)
    //{
    //    DataTable dt = new DataTable();
    //    dt = GetPartsHover(RefNo);
    //    string str = "";
    //    string AllotedTime = "";
    //    if (dt.Rows.Count > 0)
    //    {
    //        str = "<span><table class='mydatagrid'><tr style='color:#a62724;'><td style='padding-left:3px;'><strong>PARTS NAME</strong></td><td style='width:50px;' align='center'><strong>STATUS</strong></td><td style='width:140px;' align='center'><strong>AVAILABILITY TIME</strong></td></tr>";
    //        for (int i = 0; i < dt.Rows.Count; i++)
    //        {
    //            if (i % 2 == 0)
    //            {
    //                if (dt.Rows[i]["Alloted"].ToString() == "Y")
    //                {
    //                    AllotedTime = "";
    //                    str = str + "<tr><td style='padding-left:3px;'>" + dt.Rows[i]["PartsName"].ToString() + "</td><td style='width:50px;' align='center'>" + dt.Rows[i]["Alloted"].ToString().Replace("N", "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/No.png' Alt=''  width='16' height='16'/>").Replace("Y", "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN.png' Alt=''  width='16' height='16'/>") + "</td><td style='padding-left:3px;' align='center'>" + AllotedTime + "</td></tr>";
    //                }
    //                else
    //                {
    //                    AllotedTime = dt.Rows[i][2].ToString().Replace("#", " ");
    //                    str = str + "<tr><td style='padding-left:3px;'>" + dt.Rows[i]["PartsName"].ToString() + "</td><td style='width:50px;' align='center'>" + dt.Rows[i]["Alloted"].ToString().Replace("N", "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/No.png' Alt=''  width='16' height='16'/>").Replace("Y", "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN.png' Alt=''  width='16' height='16'/>") + "</td><td style='padding-left:3px;' align='center'>" + AllotedTime + "</td></tr>";
    //                }
    //            }
    //            else
    //            {
    //                //str = str + "<tr style='height:20px;' bgcolor='#FFFFFF'><td style='padding-left:3px;'>" + dt.Rows[i]["PartsName"].ToString() + "</td><td style='width:50px;' align='center'>" + dt.Rows[i]["Alloted"].ToString().Replace("N", "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/No.png' Alt=''  width='16' height='16'/>").Replace("Y", "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN.png' Alt=''  width='16' height='16'/>") + "</td><td style='padding-left:3px;' align='center'>" + AllotedTime + "</td></tr>";
    //                if (dt.Rows[i]["Alloted"].ToString() == "Y")
    //                {
    //                    AllotedTime = "";
    //                    str = str + "<tr style='height:20px;' bgcolor='#FFFFFF'><td style='padding-left:3px;'>" + dt.Rows[i]["PartsName"].ToString() + "</td><td style='width:50px;' align='center'>" + dt.Rows[i]["Alloted"].ToString().Replace("N", "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/No.png' Alt=''  width='16' height='16'/>").Replace("Y", "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN.png' Alt=''  width='16' height='16'/>") + "</td><td style='padding-left:3px;' align='center'>" + AllotedTime + "</td></tr>";
    //                }
    //                else
    //                {
    //                    AllotedTime = dt.Rows[i][2].ToString().Replace("#", " ");
    //                    str = str + "<tr style='height:20px;' bgcolor='#FFFFFF'><td style='padding-left:3px;'>" + dt.Rows[i]["PartsName"].ToString() + "</td><td style='width:50px;' align='center'>" + dt.Rows[i]["Alloted"].ToString().Replace("N", "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/No.png' Alt=''  width='16' height='16'/>").Replace("Y", "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN.png' Alt=''  width='16' height='16'/>") + "</td><td style='padding-left:3px;' align='center'>" + AllotedTime + "</td></tr>";
    //                }
    //            }
    //        }
    //        str = str + "</table></span>";
    //    }
    //    else
    //    {
    //        str = "";
    //    }
    //    return str;
    //}

   // [System.Web.Services.WebMethod(EnableSession = true)]
    //public static string LoadSubProcessInOutTime(string RefNo, string SubProcessId, string SubProcessName)
    //{
    //    DataTable dt = new DataTable();
    //    dt = GetSubProcessHover(RefNo, SubProcessId);
    //    string inTime = "";
    //    string outTime = "";
    //    string empName = "";
    //    if (dt.Rows.Count > 0)
    //    {
    //        inTime = dt.Rows[0][0].ToString().Replace("#", " ");
    //        outTime = dt.Rows[0][1].ToString().Replace("#", " ");
    //        empName = dt.Rows[0][2].ToString().Trim();
    //    }

    //    string str = "<span><table class='mydatagrid' id=InOutPnl style=width:100%;><tr style=font-weight:bold;><td colspan=2 style='color:#a62724 !important;'><strong>" + SubProcessName + " : " + empName + "</strong></td></tr><tr><td style=width:90px;>In Time: </td><td style=text-align:center;>" + inTime + "</td></tr><tr><td style=width:90px;>Out Time: </td><td style=text-align:center;>" + outTime + "</td></tr></table></span>";
    //    return str;
    //}

    #endregion "HOVERING"

    protected void drpOrderBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (drpOrderBy.SelectedIndex != -1)
            {
                cmbCustType.SelectedIndex = -1;
                cmbServiceType.SelectedIndex = -1;
                cmbVehicleModel.SelectedIndex = -1;
                TxtDate1.Text = "";
                TxtDate2.Text = "";
                txtVehicleNumber.Text = "";
                txtTagNo.Text = "";
                lblmsg.Text = "";
                lblmsg.CssClass = "reset";
            }
            BindGrid();
            TabContainer1.Visible = false;
        }
        catch (Exception ex)
        {
        }
    }

    protected void btn_reports_Click(object sender, EventArgs e)
    {
        Response.Redirect("Reports.aspx");
    }

    protected void btn_kpiDashboard_Click(object sender, EventArgs e)
    {
        Response.Redirect("KPIDashboard.aspx");
    }
}

public class TMLCRMDisplay
{
    public Int16 Slno { get; set; }

    public Int32 VID { get; set; }
    public string VRN { get; set; }

    public string VIP { get; set; }
    public string MODEL { get; set; }

    public string ST { get; set; }
    public string STATUS { get; set; }
    public string JC { get; set; }
    public string PRTS { get; set; }
    public string BA { get; set; }
   
    public string T1 { get; set; }
    public string T2 { get; set; }
    public string T3 { get; set; }
    public string WA { get; set; }
    public string RT { get; set; }
    public string WASH { get; set; }
    public string QC { get; set; }
    public string VAS { get; set; }
    public string JCC { get; set; }
    public string PRG { get; set; }
    public string PDT { get; set; }
    public string AGE { get; set; }
    public string RMK { get; set; }
    public Int32 REFNO { get; set; }
    public string ERT { get; set; }

}