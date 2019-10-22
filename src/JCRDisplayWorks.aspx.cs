using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using System.Windows.Forms;

public partial class JCRDisplayWorks : System.Web.UI.Page
{
    private static string BackTo = "";
    private static DataTable DisplayDt;
    private static string EmpId = "0";
    private static int fblank = 0;
    private static int miniTabIndex = 6;
    private static string statusVal = "";
    private static string RegNo = "";
    private static int LostCusCount = 0;

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
        //DisplayDt = new DataTable();
        //DataTable HDt = new DataTable();
        //DisplayDt.Clear();
        //HDt.Clear();
        //DisplayDt = GetDisplayDate(rbType.SelectedValue, txtVehicleNumber.Text, miniTabIndex + 1);
        List<Display> Disp = GetDisplayDate(rbType.SelectedValue, txtVehicleNumber.Text, miniTabIndex + 1,true);
       // HDt = DisplayDt;
        if (Disp.Count == 0)
        {
            grdDisplay.DataSource = null;
            grdDisplay.DataBind();
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
        else
        {
            grdDisplay.GridLines = GridLines.Horizontal;
            GridView1.DataSource = Disp;
            GridView1.DataBind();
            grdDisplay.DataSource = Disp;
            grdDisplay.DataBind();
        }

        //fblank = 0;
        //for (int fbt = 0; fbt < Disp.Count; fbt++)
        //{
        //    if (Disp.Rows[fbt][5].ToString().Trim() != "")
        //    {
        //        fblank = 1;
        //    }
        //}
        FillVehicleStatus();
        lbVCount.Text = Disp.Count.ToString();
    }

    protected void BindPartsGrid(int Refno)
    {
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlCommand cmd = new SqlCommand("udpListPartsRequisition", con);
        cmd.Parameters.AddWithValue("@RefId", Refno);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dtParts = new DataTable();
        sda.Fill(dtParts);
        if (dtParts.Rows.Count > 0)
        {
            grdParts.DataSource = dtParts;
            grdParts.DataBind();
        }
        else
        {
            grdParts.DataSource = null;
            grdParts.DataBind();
        }
    }

    protected void LostCust() // //Added by Pratik for Lost Customer identification
    {
       
            
            string sConnString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
            SqlConnection con = new SqlConnection(sConnString);
            SqlDataAdapter sda = new SqlDataAdapter("select RegistrationNo from tbl_UploadLostCustomers", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
       
        if (dt.Rows.Count > 0)
        {
            LostCusCount = dt.Rows.Count;
        }
    }


    protected void BindTechnicianList(int Refno, string Tech1, string Tech2, string Tech3)
    {
        try
        {
            // lbl_Techmsg.Text = "";
            //if (Tech1.Contains("#") || Tech2.Contains("#") || Tech3.Contains("#"))
            //{
            //    Tech1 = Tech1.Replace("#","");
            //    Tech2 = Tech2.Replace("#","");
            //    Tech3 = Tech3.Replace("#", "");
            //}
            //else 
        if (Tech1.Contains("$") || Tech2.Contains("$") || Tech3.Contains("$"))
            {
                Tech1 = Tech1.Replace("$", "");
                Tech2 = Tech2.Replace("$", "");
                Tech3 = Tech3.Replace("$", "");
            }
           
            lbl_TechRefId.Text = Refno.ToString();           
            SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
            SqlCommand cmd = new SqlCommand("udpListEmployee", con);
            cmd.Parameters.AddWithValue("@RefNo", Refno);
            cmd.Parameters.AddWithValue("@Tech1", Tech1);
            cmd.Parameters.AddWithValue("@Tech2", Tech2);
            cmd.Parameters.AddWithValue("@Tech3", Tech3);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            cmd.CommandType = CommandType.StoredProcedure;
            if (con.State == ConnectionState.Closed)
                con.Open();
            sda.Fill(dt);
            con.Close();
            ddlTechList.Items.Clear();
            ddlTechList.DataSource = dt;
            ddlTechList.DataValueField = "WorkId";
            ddlTechList.DataTextField = "EmpName";
            ddlTechList.DataBind();
        }
        catch (Exception ex) { }
    }


    protected void btn_Back_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["ROLE"].ToString() == "WORK MANAGER")
        {
            Response.Redirect("JCHome.aspx");
        }
        else if (Session["ROLE"].ToString() == "DISPLAY" && Request.Url.ToString().Contains("DisplayWorks.aspx?Back=123") && Session["BACKROLE"].ToString() == "SM")
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
    }

    protected void btn_JCCUpdate_Click(object sender, EventArgs e)
    {
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
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                lblmsg.CssClass = "ScsMsg";
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

    protected void btnBACK_Click(object sender, EventArgs e)
    {
        Session["CURRENT_PAGE"] = null;
        if (Session["ROLE"].ToString() == "WORK MANAGER")
        {
            Response.Redirect("JCHome.aspx");
        }
        else if (Session["ROLE"].ToString() == "FRONT OFFICE")
        {
            Response.Redirect("FrontOfficeHome.aspx");
        }
        else if (Session["ROLE"].ToString() == "SERVICE ADVISOR")
        {
            Response.Redirect("SAHome.aspx");
        }
        else if (Session["ROLE"].ToString() == "CRM")
        {
            Response.Redirect("CRMHome.aspx");
        }
        else if (Session["ROLE"].ToString() == "DISPLAY")
        {
            Response.Redirect("DisplayHome.aspx");
        }
        else if (BackTo == "GMSERVICE")
        {
            Response.Redirect("DisplayHome.aspx?Back=333", false);
        }
        else if (BackTo == "SM")
        {
            Response.Redirect("DisplayHome.aspx?Back=222", false);
        }
    }

    protected void btnc_Click(object sender, EventArgs e)
    {
        TabContainer1.Visible = false;
    }

    protected void btnCancelation_Click(object sender, EventArgs e)
    {
        try
        {
            if (lblvcvehicleno.Text.Trim() == "")
            {
                lblmsg.Text = "There Is No Vehicle Available For Cancellation.!";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                lblmsg.CssClass = "ErrMsg";
            }
            else if (txtcncl.Text.Trim() == "")
            {
                lblmsg.Text = "Please Enter Reason For Cancellation.!";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                lblmsg.CssClass = "ErrMsg";
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
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                lblmsg.CssClass = "ScsMsg";
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
        Timer1.Enabled = true;
    }

    protected void btncncl_Click(object sender, EventArgs e)
    {
        txtcardno.Text = "";
        txtnewcrdno.Text = "";
        lblvehicleUPDTag.Text = "";
    }

    protected void btnecncl_Click(object sender, EventArgs e)
    {
        lblmsg.CssClass = "reset";
        lblmsg.Text = "";
        TabContainer1.Visible = false;
    }

    protected void btneupd_Click(object sender, EventArgs e)
    {
        string sConnString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        SqlConnection con = new SqlConnection(sConnString);
        try
        {
            lblmsg.Text = "";
            lblmsg.CssClass = "reset";
            if (txtenewvhno.Text.Trim() == string.Empty)
            {
                lblmsg.Text = "Empty New Registration No.";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                lblmsg.CssClass = "ErrMsg";
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
                            string str3 = "Update tblMaster set RegNo='" + txtenewvhno.Text.Trim() + "' where SlNo='" + dt.Rows[0][0].ToString() + "'";
                            SqlCommand cmd3 = new SqlCommand(str3, con);
                            cmd3.ExecuteNonQuery();
                            lblmsg.Text = "Updated Successfully";
                            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                            lblmsg.CssClass = "ScsMsg";
                            txtevehno.Text = txtenewvhno.Text;
                            txtenewvhno.Text = "";
                            BindGrid();
                            btnecncl_Click(null, null);
                        }
                        else
                        {
                            lblmsg.Text = "Registration No. Already In For Service";
                            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                            lblmsg.CssClass = "ErrMsg";
                        }
                    }
                    else
                    {
                        lblmsg.Text = "Registration No. Not Exist";
                        ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                        lblmsg.CssClass = "ErrMsg";
                    }
                }
                else
                {
                    lblmsg.Text = "Both Registration No. Same";
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                    lblmsg.CssClass = "ErrMsg";
                }
            }
        }
        catch (Exception ex)
        {
           // lblmsg.Text = ex.Message;
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }

    protected void btnHold_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        lblmsg.CssClass = "ScsMsg";
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
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                    lblmsg.CssClass = "ErrMsg";
                }
                else
                {
                    lblmsg.Text = msg.Value.ToString();
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                    lblmsg.CssClass = "ScsMsg";
                    lblSlnoforHold.Text = "";
                    lblTagnoforHold.Text = "";
                    lblVehnoforHold.Text = "";
                }

                oConn.Close();
                BindGrid();
            }
            else
            {
                lblmsg.Text = "VIN/VRN is not provided";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                lblmsg.CssClass = "ErrMsg";
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message.ToString();
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
            lblmsg.CssClass = "ErrMsg";
        }
        finally
        {
        }
    }

   

    protected void btnOut_Click(object sender, EventArgs e)
    {
        try
        {
            if (lblvovehicleno.Text != "")
            {
                if (ddVOutRemarks.SelectedValue.Trim() == "-1" && txt_VORemarks.Text.Trim() == "")
                {
                    lblmsg.Text = "Please Add Remarks.";
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                    lblmsg.CssClass = "ErrMsg";
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
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                    lblmsg.CssClass = "ScsMsg";
                    lblvovehicleno.Text = "";
                    txt_VORemarks.Text = "";
                    BindGrid();
                    btnecncl_Click(null, null);
                }
                else
                {
                    lblmsg.Text = "VIN/VRN. Not Found";
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                    lblmsg.CssClass = "ErrMsg";
                }
            }
            else
            {
                lblmsg.Text = "VIN/VRN is not provided";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                lblmsg.CssClass = "ErrMsg";
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void btnPDTsave_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        lblmsg.CssClass = "reset";
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
                        ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                        lblmsg.CssClass = "ErrMsg";
                    }
                    else
                    {
                        if (txtRevPDT.Text.Trim() != "" && cmbHH.Text.Trim() != "")
                        {
                            if (txtPDTComment.Text.Trim() != "" || ddPDTRemarks.SelectedValue.Trim() != "-1")
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
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    oConn.Open();
                                    cmd.ExecuteNonQuery();
                                    lblmsg.Text = "Revised PDT Saved Successfully";
                                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                                    lblmsg.CssClass = "ScsMsg";
                                    oConn.Close();
                                    txtRevPDT.Text = DateTime.Now.ToShortDateString();
                                    BindGrid();
                                    txtPDTComment.Text = "";
                                    rd_No.Checked = false;
                                    rd_Yes.Checked = false;
                                }
                                else
                                {
                                    lblmsg.Text = "Please Select Customer Informed Yes or No";
                                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                                    lblmsg.CssClass = "ErrMsg";
                                }
                            }
                            else
                            {
                                lblmsg.Text = "Please Enter RPDT Reason";
                                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                                lblmsg.CssClass = "ErrMsg";
                            }
                        }
                        else
                        {
                            lblmsg.Text = "Please Enter Revised PDT and Time";
                            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                            lblmsg.CssClass = "ErrMsg";
                        }
                    }
                }
                else
                {
                    lblmsg.Text = "Invalid Date Provided.";
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                    lblmsg.CssClass = "ErrMsg";
                }
            }
            else
            {
                lblmsg.Text = "VIN/VRN Not Provided!";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                lblmsg.CssClass = "ErrMsg";
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void btnready_Click(object sender, EventArgs e)
    {
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
                lblmsg.Text = "Vehicle Ready Done";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                lblmsg.CssClass = "ScsMsg";
                oConn.Close();
                BindGrid();
            }
            else
            {
                lblmsg.Text = "VIN/VRN is not provided";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                lblmsg.CssClass = "ErrMsg";
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
        
            List<Display> Disp = GetDisplayDate(rbType.SelectedValue, txtVehicleNumber.Text, miniTabIndex + 1, true);

            GridView1.DataSource = Disp;
            GridView1.DataBind();
            grdDisplay.DataSource = Disp;
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
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                lblmsg.CssClass = "ScsMsg";
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
        if (ddlSAList.SelectedItem.Text.ToString() != "Select")
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
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                    lblmsg.CssClass = "ErrMsg";
                    lblmsg.Text = msg.Value.ToString();
                }
                else
                {
                    lblmsg.Text = msg.Value.ToString();
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                    lblmsg.CssClass = "ScsMsg";

                    btnecncl_Click(null, null);
                }

                if (con.State != ConnectionState.Closed)
                    con.Close();
                txtSARemarks.Text = "";
                lblSlNoSA.Text = "";
                getSACRM("");
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
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
            lblmsg.CssClass = "ErrMsg";
            lblmsg.Text = "Please Select Service Advisor..!";
        }
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lblmsg.Text = "";
            lblmsg.CssClass = "reset";
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
                lblmsg.Text = "Please Select To Date";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                lblmsg.CssClass = "ErrMsg";
            }
            else if (cs[0] == 0 && cs[1] == 1)
            {
                lblmsg.Text = "Please Select From Date";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                lblmsg.CssClass = "ErrMsg";
            }
            else if (cs[0] == 0 && cs[1] == 0 && cs[2] == 0 && cs[3] == 0)
            {
                lblmsg.Text = "Please Select Date Range Or VIN/VRN Or Tag No";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                lblmsg.CssClass = "ErrMsg";
            }
        }
        catch (Exception ex)
        {
            lblLoading.Text = ex.Message;
        }
    }

    protected void btnspsave_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        lblmsg.CssClass = "reset";
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
                            lblmsg.Text = "Please Add Remarks.";
                            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                            lblmsg.CssClass = "ErrMsg";
                            return;
                        }
                        else
                            cmd.Parameters.AddWithValue("@Comment", ddlSRemarks.SelectedItem.Text.Trim());
                        cmd.Parameters.AddWithValue("@DTM", DateTime.Now);
                        cmd.CommandType = CommandType.StoredProcedure;
                        oConn.Open();
                        cmd.ExecuteNonQuery();
                        lblmsg.Text = "Saved Successfully";
                        ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                        lblmsg.CssClass = "ScsMsg";
                        txtServiceAction.Text = String.Empty;
                        txtRecomendation.Text = String.Empty;
                        oConn.Close();
                        txtspremarks.Text = "";
                        drpsptype.SelectedIndex = 0;
                        FillRemarksTemplate(6, ref ddlSRemarks);
                        drpspprocess.SelectedIndex = 0;

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
                            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                            lblmsg.CssClass = "ScsMsg";
                            txtServiceAction.Text = String.Empty;
                            txtRecomendation.Text = String.Empty;
                            con.Close();
                            
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
                            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                            lblmsg.CssClass = "ErrMsg";
                           
                            return;
                        }
                        else
                            cmd.Parameters.AddWithValue("@Remarks", ddlSRemarks.SelectedItem.Text.Trim());
                        oConn.Open();
                        cmd.ExecuteNonQuery();
                        lblmsg.Text = "Saved Successfully";
                        ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                        lblmsg.CssClass = "ScsMsg";
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
                            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                            lblmsg.CssClass = "ErrMsg";
                            lblmsg.Text = "Please Add Remarks.";
                        }
                        else
                        {
                            if (ddlSRemarks.SelectedIndex == ddlSRemarks.Items.Count - 1)
                            {
                                if (txtspremarks.Text.Trim() == "")
                                {
                                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                                    lblmsg.CssClass = "ErrMsg";
                                    lblmsg.Text = "Please Add Remarks For Other.";
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
                            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                            lblmsg.CssClass = "ScsMsg";
                            oConn.Close();
                            txtspremarks.Text = "";
                            ddlSRemarks.SelectedIndex = 0;
                            
                            drpsptype.SelectedIndex = 0;
                            FillRemarksTemplate(6, ref ddlSRemarks);
                            ddlSRemarks.Visible = true;
                            drpspprocess.Visible = false;
                            drpspprocess.SelectedIndex = 0;


                        }
                    }
                    BindGrid();
                    lblspprocess.Visible = false;
                    drpspprocess.Visible = false;
                    lblServiceAction.Visible = true;
                    lblServiceRecom.Visible = true;
                    txtServiceAction.Visible = true;
                    txtRecomendation.Visible = true;
                    txtspremarks.Visible = false;
                    FillRemarksTemplate(6, ref ddlSRemarks);

                    // drpspprocess.SelectedIndex = 0;
                    //drpsptype.SelectedIndex = -1;
                    //ddlSRemarks.SelectedIndex = -1;
                    //drpspprocess.SelectedIndex = -1;
                }
                else
                {
                    lblmsg.Text = "Please Add Remarks!";
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                    lblmsg.CssClass = "ErrMsg";
                }
            }
            else
            {
                lblmsg.Text = "VIN/VRN Not Provided!";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                lblmsg.CssClass = "ErrMsg";
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string sConnString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        SqlConnection con = new SqlConnection(sConnString);
        try
        {
            if (txtcardno.Text.Trim() != txtnewcrdno.Text.Trim() && txtcardno.Text.Trim() != "")
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                SqlCommand cmd1 = new SqlCommand("udpTagUpdationInJCR", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@RefId", DataManager.GetRefNo(lblvehicleUPDTag.Text.Trim(), Session[Session["TMLDealercode"] + "-TMLConString"].ToString()));
                cmd1.Parameters.AddWithValue("@TagNo", txtcardno.Text.Trim());
                cmd1.Parameters.AddWithValue("@NewTagNo", txtnewcrdno.Text.Trim());
                SqlParameter flag = cmd1.Parameters.Add("@Flag", SqlDbType.VarChar, 75);
                flag.Direction = ParameterDirection.Output;
                flag.Value = "";
                cmd1.ExecuteNonQuery();
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                lblmsg.CssClass = "ErrMsg";
                lblmsg.Text = flag.Value.ToString();
                txtcardno.Text = txtnewcrdno.Text.ToString();
                txtnewcrdno.Text = "";
                BindGrid();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                lblmsg.CssClass = "ErrMsg";
                lblmsg.Text = "Enter New Card No.";
                txtnewcrdno.Focus();
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
            lblmsg.CssClass = "ErrMsg";
            lblmsg.Text = "Please try again!";
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }

    protected void btnUpdateRFIDCancel_Click(object sender, EventArgs e)
    {
        if (lblTagNo.Text.ToString() != "")
        {
            if (CmbCancelationRemarks.SelectedIndex == 0)
            {
                lblmsg.Text = "Please Add Remarks.";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                lblmsg.CssClass = "ErrMsg";
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
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                    lblmsg.CssClass = "ScsMsg";
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

    protected void Button3_Click(object sender, EventArgs e)
    {
        string sConnString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        SqlConnection con = new SqlConnection(sConnString);
        try
        {
            Label11.ForeColor = Color.Red;
            Label11.Text = "";
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
                            lblmsg.Text = "Updated Successfully";
                            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                            lblmsg.CssClass = "ScsMsg";
                        }
                        else
                        {
                            lblmsg.Text = "Update Aborted, Please Try Again.";
                            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                            lblmsg.CssClass = "ErrMsg";
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
                    lblmsg.Text = "VIN/VRNt Registered for Servicing";
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                    lblmsg.CssClass = "ErrMsg";
                    txtvehicle.Focus();
                }
            }
            else
            {
                lblmsg.Text = "Enter VIN/VRN.";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                lblmsg.CssClass = "ErrMsg";
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
                }
                else
                {
                    TabPanel6.Enabled = false;
                    TabPanel10.Enabled = false;
                    TabPanel1.Enabled = false;
                    TabPanel11.Enabled = false;
                }
                if (dt.Rows[0][2].ToString().Replace("&nbsp;", "").Trim().ToUpper() == "FALSE" || dt.Rows[0][2].ToString().Replace("&nbsp;", "").Trim() == "" || dt.Rows[0][2].ToString().Trim() == "NULL")
                {
                    TabPanel1.Enabled = true; //false
                    TabPanel10.Enabled = true;
                    TabPanel11.Enabled = true;
                    if (dt.Rows[0]["VehicleReady"].ToString().Replace("&nbsp;", "").Trim() == "" || dt.Rows[0]["VehicleReady"].ToString().Trim() == "NULL")  // dt.Rows[0]["VehicleReady"].ToString().Trim() != "" || dt.Rows[0]["VehicleReady"].ToString().Trim() == "NULL"
                    {
                        TabPanel6.Enabled = true; //false
                        TabPanel10.Enabled = true;
                        TabPanel1.Enabled = true;
                        TabPanel11.Enabled = true;
                    }
                    else
                    {
                        TabPanel6.Enabled = false;
                        TabPanel10.Enabled = false;
                        TabPanel1.Enabled = false;
                        TabPanel11.Enabled = false;
                    }
                }
                else
                {
                    TabPanel1.Enabled = false;
                    TabPanel10.Enabled = false;
                    TabPanel6.Enabled = false;
                    TabPanel11.Enabled = false;
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
          
            List<Display> Disp = GetDisplayDate(rbType.SelectedValue, txtVehicleNumber.Text, miniTabIndex + 1, true);

            GridView1.DataSource = Disp;
            GridView1.DataBind();
            grdDisplay.DataSource = Disp;
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
        if (grdDisplay.DataSource != null || GridView1.DataSource != null)
        {
            string status = "";
            string counter = "";
            string StatusTech = "";
            string RefNo = e.Row.Cells[21].Text.ToString();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[0].Text != "")
                {
                    e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.color='Blue';this.style.textDecoration='underline';";
                    e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='#666699';";
                    if (Session["ROLE"] == "DISPLAY")
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
            ///// Declaration for Lost Customer and calling the function///////
          
             
            ///////   End  ///////////////////////////
            //FOR HIDING COLUMNS.
            if (e.Row.RowType != DataControlRowType.Pager)
            {
                e.Row.Cells[9].Visible = false;
               // e.Row.Cells[19].Visible = false;
              //  e.Row.Cells[20].Visible = false;
                e.Row.Cells[21].Visible = false;
                e.Row.Cells[22].Visible = false;
                e.Row.Cells[23].Visible = false;
                e.Row.Cells[24].Visible = false;
                e.Row.Cells[8].Visible = false;
                e.Row.Cells[9].Visible = false;
                e.Row.Cells[25].Visible = false;
            }

            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[1].Text = "<div style='height:42px'><table style='width:100%;Height:100%;' border='0' cellpadding='0' cellspacing='0'><tr><td align='left' style='padding-left: 16px;'>" + e.Row.Cells[1].Text.ToString().ToUpper() + "</td></tr></table></div>";
                e.Row.Cells[2].Text = "<div style='height:42px'><table style='width:100%;Height:100%;' border='0' cellpadding='0' cellspacing='0'><tr><td align='right'>" + e.Row.Cells[2].Text.ToString() + "</td></tr></table></div>";
                e.Row.Cells[3].Text = "<div style='height:42px'><table style='width:100%;text-align:left;Height:100%;' border='0' cellpadding='0' cellspacing='0'><tr><td align='center'>VRN</td></tr></table></div>";
                //e.Row.Cells[4].Text = "<div style='height:42px'>" + e.Row.Cells[4].Text.ToString() + "</div>";
                //e.Row.Cells[4].Text = "<div style='text-align:center;height:42px'><table style='width:100%;Height:100%;' border='0' cellpadding='0' cellspacing='0'><tr><td style='height:50%;padding-left: 17px;'>VIP</td></tr><tr><td style='    padding-left: 17px;'> CW</td></tr></table></div>";
                e.Row.Cells[4].Text = "<div style='height:42px'><table style='width:100%;text-align:left;Height:100%;' border='0' cellpadding='0' cellspacing='0'><tr><td align='left' style='    padding-left: 10px;'>JDP/CW</td></tr></table></div>";

                e.Row.Cells[5].Text = "<div style='height:42px'><table style='width:100%;text-align:left;Height:100%;' border='0' cellpadding='0' cellspacing='0'><tr><td align='left' style='    padding-left: 10px;'>" + e.Row.Cells[5].Text.ToString() + "</td></tr></table></div>";
                e.Row.Cells[6].Text = "<div style='height:42px'><table style='width:100%;text-align:left;Height:100%;' border='0' cellpadding='0' cellspacing='0'><tr><td align='right'>" + e.Row.Cells[6].Text.ToString() + "</td></tr></table></div>";
                e.Row.Cells[7].Text = "<div style='height:42px'><table style='width:100%;Height:100%;' border='0' cellpadding='0' cellspacing='0'><tr><td align='right'>" + e.Row.Cells[7].Text.ToString() + "</td></tr></table></div>";
                //e.Row.Cells[8].Text = "<div style='height:42px'><table style='width:100%;Height:100%;' border='0' cellpadding='0' cellspacing='0'><tr><td style='height:50%;text-align: center;' colspan='2'>PARTS STATUS</td></tr><tr><td align='center'>REQ.</td><td align='left'>AVL.</td></tr></table></div>";
                e.Row.Cells[10].Text = "<div style='height:42px'><table style='width:100%;Height:100%;' border='0' cellpadding='0' cellspacing='0'><tr><td align='left' style='padding-left: 17px;'>" + e.Row.Cells[10].Text.ToString() + "</td></tr></table></div>";
                e.Row.Cells[11].Text = "<div style='height:42px'><table style='width:100%;Height:100%;' border='0' cellpadding='0' cellspacing='0'><tr><td align='left' style='padding-left: 15px;'>" + e.Row.Cells[11].Text.ToString() + "</td></tr></table></div>";
                e.Row.Cells[12].Text = "<div style='height:42px'><table style='width:100%;Height:100%;' border='0' cellpadding='0' cellspacing='0'><tr><td align='left' style='padding-left: 14px;'>" + e.Row.Cells[12].Text.ToString() + "</td></tr></table></div>";
                e.Row.Cells[13].Text = "<div style='height:42px'><table style='width:100%;Height:100%;' border='0' cellpadding='0' cellspacing='0'><tr><td align='center'>" + e.Row.Cells[13].Text.ToString() + "</td></tr></table></div>";
                e.Row.Cells[14].Text = "<div style='height:42px'><table style='width:100%;Height:100%;' border='0' cellpadding='0' cellspacing='0'><tr><td align='center'>" + e.Row.Cells[14].Text.ToString() + "</td></tr></table></div>";
                e.Row.Cells[15].Text = "<div style='height:42px'><table style='width:100%;Height:100%;' border='0' cellpadding='0' cellspacing='0'><tr><td align='right'>" + e.Row.Cells[15].Text.ToString() + "</td></tr></table></div>";
                e.Row.Cells[16].Text = "<div style='height:42px'><table style='width:100%;Height:100%;' border='0' cellpadding='0' cellspacing='0'><tr><td align='center'>" + e.Row.Cells[16].Text.ToString() + "</td></tr></table></div>";
                e.Row.Cells[17].Text = "<div style='height:42px'><table style='width:100%;Height:100%;' border='0' cellpadding='0' cellspacing='0'><tr><td align='center'></td></tr></table></div>";
                e.Row.Cells[18].Text = "<div style='height:42px'><table style='width:100%;Height:100%;' border='0' cellpadding='0' cellspacing='0'><tr><td align='left'>" + e.Row.Cells[18].Text.ToString() + "</td></tr></table></div>";
                // e.Row.Cells[4].Text = "<div style='width:30px;height:42px'><table style='width:100%;Height:100%;' border='0' cellpadding='0' cellspacing='0'><tr><td style=height:50%;>PROMISED</td></tr><tr><td>TIME</td></tr></table></div>";

                e.Row.Cells[19].Text = "<div style='height:42px'><table style='width:100%;Height:100%;' border='0'><tr><td style='text-align: right;'>" + e.Row.Cells[19].Text.ToString() + "</td></tr></table></div>";
                e.Row.Cells[20].Text = "<div style='height:42px'><table style='width:100%;Height:100%;' border='0'><tr><td style='text-align: right;'>" + e.Row.Cells[20].Text.ToString() + "</td></tr></table></div>";
                e.Row.Cells[25].Text = "<div style='height:42px'><table style='width:100%;Height:100%;' border='0'><tr><td style='text-align: center;'>" + e.Row.Cells[25].Text.ToString() + "</td></tr></table></div>";

                e.Row.Height = 42;
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //SLNO
                e.Row.Cells[0].Text = "<div style=width:29px;>" + e.Row.Cells[0].Text.ToString() + "</div>";

                //BAY
                e.Row.Cells[1].Text = "<div style=width:70px;text-align:left;>" + e.Row.Cells[1].Text.ToString() + "</div>";

                //TAG
                e.Row.Cells[2].Text = "<div style=width:26px;>" + e.Row.Cells[2].Text.ToString() + "</div>";

                //REGNO
                if (e.Row.Cells[3].Text.Length > 10)
                {
                    int lenth = e.Row.Cells[3].Text.Length;
                    string cells = e.Row.Cells[3].Text.Substring(lenth - 10, 10);
                    e.Row.Cells[3].Text = "<div style=width:80px;text-align:left;>" + cells + "</div>";
                }
                
                else if (LostCusCount>0) //Added by Pratik for Lost Customer identification
                {
                    string sConnString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
                    SqlConnection con = new SqlConnection(sConnString);
                    SqlDataAdapter sda = new SqlDataAdapter("select RegistrationNo from tbl_UploadLostCustomers", con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    LostCusCount = dt.Rows.Count;
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            RegNo = dt.Rows[i][0].ToString();




                            if (RegNo == e.Row.Cells[3].Text.ToString())
                            {
                                e.Row.Cells[3].Text = "<div style=width:80px;text-align:left;>" + e.Row.Cells[3].Text.ToString() + "</div>";
                                e.Row.Cells[3].ForeColor = Color.Red;
                            }
                        }
                    }
                   
                }
                else
                {
                    e.Row.Cells[3].Text = "<div style=width:80px;text-align:left;>" + e.Row.Cells[3].Text.ToString() + "</div>";
                }
               // e.Row.Cells[3].Text = "<div style=width:100px;text-align:left;>" + e.Row.Cells[3].Text.ToString() + "</div>";
                e.Row.Cells[3].Attributes.Add("onmouseover", "showRegNoHover(event,'" + RefNo + "')");
                e.Row.Cells[3].Attributes.Add("onmouseout", "hideTooltip(event)");
                

                //Type : JDP-CW
                try
                {
                    if (e.Row.Cells[4].Text.ToString() == "0-0")
                        e.Row.Cells[4].Text = "<div style=width:30px;></div>";
                    else if (e.Row.Cells[4].Text.ToString() == "1-0")
                        e.Row.Cells[4].Text = "<div style=width:30px; ><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/JDP.png' Alt=''  width='16' height='16'/></div>";
                    else if (e.Row.Cells[4].Text.ToString() == "0-1")
                        e.Row.Cells[4].Text = "<div style=width:30px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/Customer_Waiting.png' Alt=''  width='16' height='16'/></div>";
                    else if (e.Row.Cells[4].Text.ToString() == "1-1")
                        e.Row.Cells[4].Text = "<div style=width:30px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/JDP_Customer_Waiting.png' Alt=''  width='16' height='16'/></div>";
                }
                catch (Exception ex)
                {
                }

                //MODEL
                try
                {
                    e.Row.Cells[5].ToolTip = e.Row.Cells[5].Text.ToString();//.Replace("<div style=width:60px;>", "").Replace("</div>", "");
                    if (e.Row.Cells[5].Text.Length > 10)
                        e.Row.Cells[5].Text = "<div style=width:75px;text-align:left;>" + e.Row.Cells[5].Text.ToString().Substring(0, 10) + "</div>";
                    else
                        e.Row.Cells[5].Text = "<div style=width:75px;text-align:left;>" + e.Row.Cells[5].Text.ToString() + "</div>";
                }
                catch (Exception ex)
                {
                }

                //S-T
                e.Row.Cells[6].Text = "<div style=width:25px;text-align:left;>" + e.Row.Cells[6].Text.ToString() + "</div>";

                //STAUS - 000-Normal, 100-Ready , 010-Hold , 001-Idle
                try
                {
                    string[] status_str = e.Row.Cells[7].Text.ToString().Split('|');
                    if (status_str[0] == "0-0-0")
                    {

                        e.Row.Cells[7].Text = "<div style=width:45px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205149/jcr/Status.png' Alt=''  width='16' height='16'/></div>";
                        e.Row.Cells[7].ToolTip = "Work In Progress";
                    }
                    else if (status_str[0] == "1-0-0")
                    {


                        e.Row.Cells[7].Text = "<div style=width:45px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205149/jcr/Status_Ready.png' Alt='' width='16' height='16'/></div>";
                        e.Row.Cells[7].ToolTip = "Vehicle Ready [" + status_str[1].Trim() + "]";
                    }
                    else if (status_str[0] == "0-1-0")
                    {


                        e.Row.Cells[7].Text = "<div style=width:45px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205149/jcr/Status_HOLD.png' Alt='' width='16' height='16'/></div>";
                        e.Row.Cells[7].ToolTip = "Hold";
                    }
                    else if (status_str[0] == "0-0-1")
                    {
                   
                        e.Row.Cells[7].Text = "<div style=width:45px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205149/jcr/Status_IDLE.png' Alt='' width='16' height='16'/></div>";
                        e.Row.Cells[7].ToolTip = "Idle";
                    }
                }
                catch (Exception ex)
                {
                }

                //Parts
                //string parts = "<div style='width:90px;'><table style='width:100%;' border='0' cellpadding='0' cellspacing='0'><tr><td align='left'>";

                //if (e.Row.Cells[8].Text.ToString().Trim() == "0")
                //{
                //    parts = parts + "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484297131/images/Red.png' Alt='' width='16' height='16'/>";
                //}
                //else if (e.Row.Cells[8].Text.ToString().Trim() == "1")
                //{
                //    parts = parts + "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484297131/images/Green.png' Alt='' width='16' height='16'/>";
                //}
                //parts = parts + "</td><td align='left' >";

                //if (e.Row.Cells[9].Text.ToString().Trim() == "1")
                //{
                //    parts = parts + "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484297131/images/Red.png' Alt='' width='16' height='16'/>";
                //}
                //else if (e.Row.Cells[9].Text.ToString().Trim() == "2")
                //{
                //    parts = parts + "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484297131/images/Light.png' Alt='' width='16' height='16'/>";
                //}
                //else if (e.Row.Cells[9].Text.ToString().Trim() == "3")
                //{
                //    parts = parts + "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484297131/images/Green.png' Alt='' width='16' height='16'/>";
                //}
                //else if (e.Row.Cells[9].Text.ToString().Trim() == "4")
                //{
                //    parts = parts + "<img id='animg' src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484297131/images/Yellow_With_Near.png' Alt='' width='16' height='16'/>";
                //}
                //parts = parts + "</td></tr></table></div>";

                //e.Row.Cells[8].Attributes.Add("onmouseover", "showPartsHover(event,'" + RefNo + "')");
                //e.Row.Cells[8].Attributes.Add("onmouseout", "hideTooltip(event)");
                //e.Row.Cells[8].Text = parts;


                //BA
                try
                {
                    status = e.Row.Cells[10].Text.ToString().Substring(0, 1);
                    StatusTech = e.Row.Cells[10].Text.ToString().Substring(1, 1);
                    if (status == "1" || status == "2")
                    {
                        e.Row.Cells[10].Attributes.Add("onmouseover", "showJADHover(event,'" + RefNo + "')");
                        e.Row.Cells[10].Attributes.Add("onmouseout", "hideTooltip(event)");
                    }
                    //if (e.Row.Cells[10].Text.ToString() == "0" || e.Row.Cells[10].Text.ToString() != "")
                    //{
                    //    StatusTech = "0";
                    //}
                    //else {
                    //    StatusTech = e.Row.Cells[10].Text.ToString().Substring(1, 1);
                    //}
                    if (status == "0")
                        e.Row.Cells[10].Text = "";
                    else if (status == "1")
                        e.Row.Cells[10].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/JA_ONTIME.png' Alt=''  width='16' height='16'/>";
                    else if (status == "2")
                        e.Row.Cells[10].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/JA_DELAY.png' Alt=''  width='16' height='16'/>";
                }
                catch (Exception ex)
                {
                }

                //T1 - Technician1
                try
                {                    
                    string getTECH1 = (e.Row.Cells[11].Text.Trim() == "&nbsp;" ? "" : e.Row.Cells[11].Text.Trim());
                    int blinkflag = 0;
                    if(getTECH1.Contains('Y'))
                    {
                        blinkflag = 1;
                        getTECH1 = getTECH1.Replace("Y", "");
                    }

                    e.Row.Cells[22].Text = getTECH1;
                    if (getTECH1.Contains("|"))
                    {
                        string[] TECH1Parts = { };
                        TECH1Parts = getTECH1.Split('|');
                        if (Convert.ToInt32(TECH1Parts[1]) > 1)
                        {
                            e.Row.Cells[11].Attributes.Add("onmouseover", "showEmpInOut(event,'" + TECH1Parts[2].Replace("*", "").Replace("$", "").Replace("#", "") + "','" + TECH1Parts[1] + "','Tech1')");
                            e.Row.Cells[11].Attributes.Add("onmouseout", "hideTooltip(event)");
                        }
                    }

                    status = e.Row.Cells[11].Text.ToString().Substring(0, 1);

                    if (status == "0")
                    {
                        if (StatusTech == "0")
                            e.Row.Cells[11].Text = "<div style=width:40px;></div>";
                        else
                            e.Row.Cells[11].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/TECH_ALLOT.png' Alt=''  width='16' height='16'/></div>";
                    }
                    else if (status == "1")
                    {
                        e.Row.Cells[11].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/TECH_WIP_ONTIME.png' Alt=''  width='16' height='16'/></div>";
                    }
                    else if (status == "2")
                    {
                        if(blinkflag == 1)
                            e.Row.Cells[11].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/TECH_WIP_NEAR.png' Alt='' width='16' height='16'/></div>";
                        else
                            e.Row.Cells[11].Text = "<div style=width:40px;><img id='animg' src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/TECH_WIP_NEAR.png' Alt='' width='16' height='16'/></div>";
                    }
                    else if (status == "3")
                    {
                        e.Row.Cells[11].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/TECH_WIP_DELAY.png' Alt=''  width='16' height='16'/></div>";
                    }
                    else if (status == "4")
                    {
                        e.Row.Cells[11].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/CN.png' Alt=''  width='16' height='16'/></div>";
                    }
                    else if (status == "5")
                    {
                        e.Row.Cells[11].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/CR.png' Alt=''  width='16' height='16'/></div>";


                    }
                    else if (status == "6")
                    {
                        e.Row.Cells[11].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/OutSkipped.png' Alt='' width='16' height='16'/></div>";
                    }
                    //else if (status == "7")
                    //{
                    //    e.Row.Cells[9].Text = "<div style=width:40px;><img src='images/JCR/Hold.png' Alt='' width='24' height='24'/></div>";
                    //}
                }
                catch (Exception ex)
                { }

                //T2 - Technician2
                try
                {
                    string getTECH2 = (e.Row.Cells[10].Text.Trim() == "&nbsp;" ? "" : e.Row.Cells[12].Text.Trim());

                    int blinkflag = 0;
                    if (getTECH2.Contains('Y'))
                    {
                        blinkflag = 1;
                        getTECH2 = getTECH2.Replace("Y", "");
                    }

                    e.Row.Cells[23].Text = getTECH2;
                    if (getTECH2.Contains("|"))
                    {
                        string[] TECH2Parts = { };
                        TECH2Parts = getTECH2.Split('|');
                        if (Convert.ToInt32(TECH2Parts[1]) > 1)
                        {
                            e.Row.Cells[12].Attributes.Add("onmouseover", "showEmpInOut(event,'" + TECH2Parts[2].Replace("*", "").Replace("$", "").Replace("#", "") + "','" + TECH2Parts[1] + "','Tech2')");
                            e.Row.Cells[12].Attributes.Add("onmouseout", "hideTooltip(event)");
                        }
                    }
                    status = e.Row.Cells[12].Text.ToString().Substring(0, 1);

                    if (status == "0")
                    {
                        if (Convert.ToInt16(StatusTech) > 1)
                            e.Row.Cells[12].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/TECH_ALLOT.png' Alt=''  width='16' height='16'/></div>";
                        else
                            e.Row.Cells[12].Text = "<div style=width:40px;></div>";
                    }
                    else if (status == "1")
                    {
                        e.Row.Cells[12].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/TECH_WIP_ONTIME.png' Alt=''  width='16' height='16'/></div>";
                    }
                    else if (status == "2")
                    {
                        if (blinkflag == 1)
                            e.Row.Cells[12].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/TECH_WIP_NEAR.png' Alt='' width='16' height='16'/></div>";
                        else
                            e.Row.Cells[12].Text = "<div style=width:40px;><img id='animg' src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/TECH_WIP_NEAR.png' Alt='' width='16' height='16'/></div>";
                    }
                    else if (status == "3")
                    {
                        e.Row.Cells[12].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/TECH_WIP_DELAY.png' Alt=''  width='16' height='16'/></div>";
                    }
                    else if (status == "4")
                    {

                        e.Row.Cells[12].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/CN.png' Alt=''  width='16' height='16'/></div>";

                    }
                    else if (status == "5")
                    {
                        e.Row.Cells[12].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/CR.png' Alt=''  width='16' height='16'/></div>";


                    }
                    else if (status == "6")
                    {
                        e.Row.Cells[12].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/OutSkipped.png' Alt=''  width='16' height='16'/></div>";
                    }
                    //else if (status == "7")
                    //{
                    //    e.Row.Cells[10].Text = "<div style=width:40px;><img src='images/JCR/Hold.png' Alt='' width='24' height='24'/></div>";
                    //}
                }
                catch (Exception ex)
                { }


                //T3 - Technician3
                try
                {
                    string getTECH3 = (e.Row.Cells[13].Text.Trim() == "&nbsp;" ? "" : e.Row.Cells[13].Text.Trim());
                    int blinkflag = 0;
                    if (getTECH3.Contains("Y"))
                    {
                        blinkflag = 1;
                        getTECH3 = getTECH3.Replace("Y", "");
                    }
                    
                    e.Row.Cells[24].Text = getTECH3;
                    if (getTECH3.Contains("|"))
                    {
                        string[] TECH3Parts = { };
                        TECH3Parts = getTECH3.Split('|');
                        if (Convert.ToInt32(TECH3Parts[1]) > 1)
                        {
                            e.Row.Cells[13].Attributes.Add("onmouseover", "showEmpInOut(event,'" + TECH3Parts[2].Replace("*", "").Replace("$", "").Replace("#", "") + "','" + TECH3Parts[1] + "','Tech3')");
                            e.Row.Cells[13].Attributes.Add("onmouseout", "hideTooltip(event)");
                        }
                    }

                    status = e.Row.Cells[13].Text.ToString().Substring(0, 1);

                    if (status == "0")
                    {
                        if (Convert.ToInt16(StatusTech) > 2)
                            e.Row.Cells[13].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/TECH_ALLOT.png' Alt=''  width='16' height='16'/></div>";
                        else
                            e.Row.Cells[13].Text = "<div style=width:40px;></div>";
                    }
                    else if (status == "1")
                    {
                        e.Row.Cells[13].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/TECH_WIP_ONTIME.png' Alt=''  width='16' height='16'/></div>";
                    }
                    else if (status == "2")
                    {
                        if (blinkflag == 1)
                            e.Row.Cells[13].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/TECH_WIP_NEAR.png' Alt='' width='16' height='16'/></div>";
                        else
                            e.Row.Cells[13].Text = "<div style=width:40px;><img id='animg' src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/TECH_WIP_NEAR.png' Alt='' width='16' height='16'/></div>";                        
                    }
                    else if (status == "3")
                    {
                        e.Row.Cells[13].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/TECH_WIP_DELAY.png' Alt=''  width='16' height='16'/></div>";
                    }
                    else if (status == "4")
                    {
                        e.Row.Cells[13].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/CN.png' Alt=''  width='16' height='16'/></div>";
                    }
                    else if (status == "5")
                    {
                        e.Row.Cells[13].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/CR.png' Alt=''  width='16' height='16'/></div>";


                    }
                    else if (status == "6")
                    {
                        e.Row.Cells[13].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/OutSkipped.png' Alt=''  width='16' height='16'/></div>";
                    }
                    //else if (status == "7")
                    //{
                    //    e.Row.Cells[11].Text = "<div style=width:40px;><img src='images/JCR/Hold.png' Alt='' width='24' height='24'/></div>";
                    //}
                }
                catch (Exception ex)
                { }


            

                //WA
                try
                {
                    string getWA = (e.Row.Cells[14].Text.Trim() == "&nbsp;" ? "" : e.Row.Cells[14].Text.Trim());
                    string[] WAParts = { };
                    if (getWA.Contains("|"))
                    {
                        WAParts = getWA.Split('|');
                    }
                    e.Row.Cells[14].Attributes.Add("onmouseover", "showProcessInOut(event,'" + RefNo + "','" + WAParts[2].Replace("$", "").Replace("#", "").Replace("*", "") + "','W-A')");
                    e.Row.Cells[14].Attributes.Add("onmouseout", "hideTooltip(event)");
                    if (e.Row.Cells[14].Text.ToString().Contains('*'))
                    {
                        status = e.Row.Cells[14].Text.ToString().Substring(0, 1);
                        if (status == "0")
                            if (e.Row.Cells[14].Text.ToString().Contains('$'))
                                e.Row.Cells[14].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/NotRequired.png' Alt=''  width='16' height='16'/></div>";


                            else
                                e.Row.Cells[14].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/WA_allot.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "1")
                            e.Row.Cells[14].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/WA_WIP.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "2")
                            e.Row.Cells[14].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/WA_WIP_NEAR.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "3")
                            e.Row.Cells[14].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/WA_WIP_Delay.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "4")
                            e.Row.Cells[14].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/CN.png' Alt=''  width='16 height='16'/></div>";
                        else if (status == "5")
                            e.Row.Cells[14].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/CR.png' Alt=''  width='16' height='16'/></div>";


                        else if (status == "6")
                            e.Row.Cells[14].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/OutSkipped.png' Alt=''  width='16' height='16'/></div>";
                    }
                    else
                    {
                        status = e.Row.Cells[14].Text.ToString().Substring(0, 1);
                        if (status == "0")
                            if (e.Row.Cells[14].Text.ToString().Contains('$'))
                                e.Row.Cells[14].Text = "<div style=width:40px;></div>";
                            else
                                e.Row.Cells[14].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/WA_ALLOT.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "1")
                            e.Row.Cells[14].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/WA_WIP.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "2")
                            e.Row.Cells[14].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/WA_WIP_NEAR.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "3")
                            e.Row.Cells[14].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/WA_WIP_DELAY.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "4")
                            e.Row.Cells[14].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/CN.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "5")
                            e.Row.Cells[14].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/CR.png' Alt=''  width='16' height='16'/></div>";


                        else if (status == "6")
                            e.Row.Cells[14].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/OutSkipped.png' Alt=''  width='16' height='16'/></div>";
                    }
                }
                catch (Exception ex)
                { }

                //Wash

                try
                {
                    string getWSH = (e.Row.Cells[15].Text.Trim() == "&nbsp;" ? "" : e.Row.Cells[15].Text.Trim());
                    string[] WSHParts = { };
                    if (getWSH.Contains("|"))
                    {
                        WSHParts = getWSH.Split('|');
                    }
                    e.Row.Cells[15].Attributes.Add("onmouseover", "showProcessInOut(event,'" + RefNo + "','" + WSHParts[2].Replace("#", "").Replace("$", "").Replace("*", "") + "','Wash')");
                    e.Row.Cells[15].Attributes.Add("onmouseout", "hideTooltip(event)");

                    if (e.Row.Cells[15].Text.ToString().Contains('*'))
                    {
                        status = e.Row.Cells[15].Text.ToString().Substring(0, 1);
                        counter = e.Row.Cells[15].Text.ToString().Split('|')[1];
                        if (status == "0")
                        {
                            if (e.Row.Cells[15].Text.ToString().Contains('$'))
                                e.Row.Cells[15].Text = "<div style=width:30px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/NotRequired.png' Alt=''  width='16' height='16'/></div>";
                            else
                                e.Row.Cells[15].Text = "<div style=width:30px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/WASH_allot.png' Alt=''  width='16' height='16'/></div>";
                        }
                        else if (status == "1")
                        {
                            e.Row.Cells[15].Text = "<div style=width:30px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/WASH_WIP.png' Alt=''  width='16' height='16'/></div>";
                        }
                        else if (status == "2")
                        {
                            e.Row.Cells[15].Text = "<div style=width:30px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/WASH_WIP_NEAR.png' Alt=''  width='16' height='16'/></div>";
                        }
                        else if (status == "3")
                        {
                            e.Row.Cells[15].Text = "<div style=width:30px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/WASH_WIP_Delay.png' Alt=''  width='16' height='16'/></div>";
                        }
                        else if (status == "4")
                        {
                            if (counter == "1")
                            {
                                e.Row.Cells[15].Text = "<div style=width:30px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/CN.png' Alt=''  width='16' height='16'/></div>";
                            }
                            else if (counter == "2")
                            {
                                e.Row.Cells[15].Text = "<div style=width:30px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/CN2.png' Alt=''  width='16' height='16'/></div>";
                            }
                            else if (counter == "3")
                            {
                                e.Row.Cells[15].Text = "<div style=width:30px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/CN3.png' Alt=''  width='16' height='16'/></div>";
                            }
                        }
                        else if (status == "5")
                        {
                            if (counter == "1")
                            {
                                e.Row.Cells[15].Text = "<div style=width:30px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/CR.png' Alt=''  width='16' height='16'/></div>";


                            }
                            else if (counter == "2")
                            {
                                e.Row.Cells[15].Text = "<div style=width:30px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/CR2.png' Alt=''  width='16' height='16'/></div>";
                            }
                            else if (counter == "3")
                            {
                                e.Row.Cells[15].Text = "<div style=width:30px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/CR3.png' Alt=''  width='16' height='16'/></div>";
                            }
                        }
                        else if (status == "6")
                        {
                            e.Row.Cells[15].Text = "<div style=width:30px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/OutSkipped.png' Alt=''  width='16' height='16'/></div>";
                        }
                    }
                    else
                    {
                        status = e.Row.Cells[15].Text.ToString().Substring(0, 1);
                        counter = e.Row.Cells[15].Text.ToString().Split('|')[1];
                        if (status == "0")
                        {
                            if (e.Row.Cells[15].Text.ToString().Contains('$'))
                                e.Row.Cells[15].Text = "<div style=width:30px;></div>";
                            else
                                e.Row.Cells[15].Text = "<div style=width:30px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/WASH.png' Alt=''  width='16' height='16'/></div>";
                        }
                        else if (status == "1")
                        {
                            e.Row.Cells[15].Text = "<div style=width:30px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/WASH_WIP.png' Alt=''  width='16' height='16'/></div>";
                        }
                        else if (status == "2")
                        {
                            e.Row.Cells[15].Text = "<div style=width:30px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/WASH_WIP_NEAR.png' Alt=''  width='16' height='16'/></div>";
                        }
                        else if (status == "3")
                        {
                            e.Row.Cells[15].Text = "<div style=width:30px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/WASH_WIP_Delay.png' Alt=''  width='16' height='16'/></div>";
                        }
                        else if (status == "4")
                        {
                            if (counter == "1")
                            {
                                e.Row.Cells[15].Text = "<div style=width:30px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/CN.png' Alt=''  width='16' height='16'/></div>";
                            }
                            else if (counter == "2")
                            {
                                e.Row.Cells[15].Text = "<div style=width:30px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/CN2.png' Alt=''  width='16' height='16'/></div>";
                            }
                            else if (counter == "3")
                            {
                                e.Row.Cells[15].Text = "<div style=width:30px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/CN3.png' Alt=''  width='16' height='16'/></div>";
                            }
                        }
                        else if (status == "5")
                        {
                            if (counter == "1")
                            {
                                e.Row.Cells[15].Text = "<div style=width:30px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/CR.png' Alt=''  width='16' height='16'/></div>";


                            }
                            else if (counter == "2")
                            {
                                e.Row.Cells[15].Text = "<div style=width:30px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/CR2.png' Alt=''  width='16' height='16'/></div>";
                            }
                            else if (counter == "3")
                            {
                                e.Row.Cells[15].Text = "<div style=width:30px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/CR3.png' Alt=''  width='16' height='16'/></div>";
                            }
                        }
                        else if (status == "6")
                        {
                            e.Row.Cells[15].Text = "<div style=width:30px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/OutSkipped.png' Alt=''  width='16' height='16'/></div>";
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
                    e.Row.Cells[16].Attributes.Add("onmouseover", "showProcessInOut(event,'" + RefNo + "','" + FINParts[2].Replace("#", "").Replace("$", "").Replace("*", "") + "','QC')");
                    e.Row.Cells[16].Attributes.Add("onmouseout", "hideTooltip(event)");

                    status = e.Row.Cells[16].Text.ToString().Substring(0, 1);
                    counter = e.Row.Cells[16].Text.ToString().Split('|')[1];


                    if (repeatStatus == 0)
                    {
                        if (status == "0")
                        {
                            e.Row.Cells[16].Text = "<div style=width:40px;></div>";
                        }
                        else if (status == "1")
                        {
                            e.Row.Cells[16].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/QC_WIP.png' Alt='' width='16' height='16'/></div>";
                        }
                        else if (status == "2")
                        {
                            e.Row.Cells[16].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/QC_WIP_Near.png' Alt='' width='16' height='16'/></div>";
                        }
                        else if (status == "3")
                        {
                            e.Row.Cells[16].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/QC_WIP_Delay.png' Alt='' width='16' height='16'/></div>";
                        }
                        else if (status == "4")
                        {
                            if (counter == "1")
                            {
                                e.Row.Cells[16].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/CN.png' Alt='' width='16' height='16'/></div>";
                            }
                            else if (counter == "2")
                            {
                                e.Row.Cells[16].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/CN2.png' Alt='' width='18' height='18'/></div>";
                            }
                            else if (counter == "3")
                            {
                                e.Row.Cells[16].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/CN3.png' Alt='' width='18' height='18'/></div>";
                            }
                        }
                        else if (status == "5")
                        {
                            if (counter == "1")
                            {
                                e.Row.Cells[16].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/CR.png' Alt='' width='16' height='16'/></div>";


                            }
                            else if (counter == "2")
                            {
                                e.Row.Cells[16].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/CR2.png' Alt='' width='18' height='18'/></div>";
                            }
                            else if (counter == "3")
                            {
                                e.Row.Cells[16].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/CR3.png' Alt='' width='18' height='18'/></div>";
                            }
                        }
                        else if (status == "6")
                        {
                            e.Row.Cells[16].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/OutSkipped.png' Alt='' width='16' height='16'/></div>";
                        }
                    }
                    else
                    {
                        if (status == "0")
                        {
                            e.Row.Cells[16].Text = "<div style=width:40px;></div>";
                        }
                        else if (status == "1")
                        {
                            e.Row.Cells[16].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/QC_WIPR.png' Alt='' width='16' height='16'/></div>";
                        }
                        else if (status == "2")
                        {
                            e.Row.Cells[16].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/QC_WIP_NearR.png' Alt='' width='16' height='16'/></div>";
                        }
                        else if (status == "3")
                        {
                            e.Row.Cells[16].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/QC_WIP_DelayR.png' Alt='' width='16' height='16'/></div>";
                        }
                        else if (status == "4")
                        {
                            if (counter == "1")
                            {
                                e.Row.Cells[16].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/CNR.png' Alt='' width='16' height='16'/></div>";
                            }
                            else if (counter == "2")
                            {
                                e.Row.Cells[16].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/CNR2.png' Alt='' width='16' height='16'/></div>";
                            }
                            else if (counter == "3")
                            {
                                e.Row.Cells[16].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/CNR3.png' Alt='' width='16' height='16'/></div>";
                            }
                        }
                        else if (status == "5")
                        {
                            if (counter == "1")
                            {
                                e.Row.Cells[16].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/CRR.png' Alt='' width='16' height='16'/></div>";
                            }
                            else if (counter == "2")
                            {
                                e.Row.Cells[16].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/CRR2.png' Alt='' width='16' height='16'/></div>";
                            }
                            else if (counter == "3")
                            {
                                e.Row.Cells[16].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/CRR3.png' Alt='' width='16' height='16'/></div>";
                            }
                        }
                        else if (status == "6")
                        {
                            e.Row.Cells[16].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/OutSkipped.png' Alt='' width='16' height='16'/></div>";
                        }
                    }


                }
                catch (Exception ex)
                { }

                //PTD Status : 0-Not Define, 1-Within Time, 2-Apporching Time, 3-Delayed
                try
                {
                    string getIFB = (e.Row.Cells[17].Text.Trim() == "&nbsp;" ? "" : e.Row.Cells[17].Text.Trim());
                    if (getIFB == "1")
                        e.Row.Cells[17].ToolTip = "On Time";
                    else if (getIFB == "0")
                        e.Row.Cells[17].ToolTip = "No PDT";
                    else if (getIFB == "2")
                        e.Row.Cells[17].ToolTip = "Approaching";
                    else
                        e.Row.Cells[17].ToolTip = "Delayed";

                    if (e.Row.Cells[17].Text.ToString() == "0")
                        e.Row.Cells[17].Text = "<div style=width:20px;></div>";
                    else if (e.Row.Cells[17].Text.ToString() == "1")
                        e.Row.Cells[17].Text = "<div style=width:20px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/circle_green.png' Alt=''  width='14' height='14'/></div>";

                    else if (e.Row.Cells[17].Text.ToString() == "2")
                        e.Row.Cells[17].Text = "<div style=width:20px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/circle_yellow.png' Alt=''  width='14' height='14'/></div>";

                    else if (e.Row.Cells[17].Text.ToString() == "3")
                        e.Row.Cells[17].Text = "<div style=width:20px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/circle_red.png' Alt=''  width='14' height='14'/></div>";



                }
                catch (Exception ex)
                { }

                //PDT
                try
                {
                    if (e.Row.Cells[18].Text.ToString().Contains('#'))
                    {
                        if (e.Row.Cells[18].Text.ToString().Contains('Y'))
                        {
                            e.Row.Cells[18].Text = "<table border=0 cellspacing=0 cellpadding=0 style=width:75px;color:BLUE;><tr><td>" + e.Row.Cells[18].Text.Replace('#', ' ').Replace('Y', ' ').ToString().Trim() + "</td></tr></table>";
                        }
                        else
                        {
                            e.Row.Cells[18].Text = "<table border=0 cellspacing=0 cellpadding=0 style=width:75px;color:RED;><tr><td>" + e.Row.Cells[18].Text.Replace('#', ' ').Replace('N', ' ').ToString().Trim() + "</td></tr></table>";
                        }
                    }
                    else
                    {
                        e.Row.Cells[18].Text = "<table border=0 cellspacing=0 cellpadding=0 style=width:75px;color:GRAY;><tr><td>" + e.Row.Cells[18].Text.Replace('$', ' ').ToString().Trim() + "</td></tr></table>";
                    }
                }
                catch (Exception ex)
                { }

                try
                {
                    e.Row.Cells[19].Text = "<div style=width:20px;>" + e.Row.Cells[19].Text.ToString() + "</div>";
                }
                catch (Exception ex)
                { }
              
                //Remarks : 0-No Remarks, 1-Remarks,2- GM Remarks
                try
                {
                    if (e.Row.Cells[20].Text.Trim() == "1" || e.Row.Cells[20].Text.Trim() == "2")
                    {
                        e.Row.Cells[20].Attributes.Add("onmouseover", "showRemarks(event,'" + RefNo + "')");
                        e.Row.Cells[20].Attributes.Add("onmouseout", "hideTooltip(event)");
                    }

                    if (e.Row.Cells[20].Text.ToString() == "0")
                        e.Row.Cells[20].Text = "<div style=width:30px;></div>";
                    else if (e.Row.Cells[20].Text.ToString() == "1")
                        e.Row.Cells[20].Text = "<div style=width:30px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/REMARKS_ENTRY_Green.png' Alt=''  width='16' height='16'/></div>";
                    else if (e.Row.Cells[20].Text.ToString() == "2")
                        e.Row.Cells[20].Text = "<div style=width:30px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/REMARKS_ENTRY.png' Alt=''  width='16' height='16'/></div>";
                }
                catch (Exception ex)
                { }
                try
                {

                    DateTime PDTTime = Convert.ToDateTime(getPDTTimeTime(Convert.ToInt16(e.Row.Cells[21].Text)));

                    if (e.Row.Cells[25].Text != "")
                    {
                        if(Convert.ToDateTime(e.Row.Cells[25].Text)< PDTTime)
                        {
                            if(Convert.ToDateTime(e.Row.Cells[25].Text).ToString("dd/MM") !=DateTime.Now.ToString("dd/MM"))
                            {
                                e.Row.Cells[25].Text = "<div>" + Convert.ToDateTime(e.Row.Cells[25].Text).ToString("HH:mm d/M") + "</div>";
                            }
                            else
                            {
                                e.Row.Cells[25].Text = "<div>" + Convert.ToDateTime(e.Row.Cells[25].Text).ToString("HH:mm") + "</div>";
                            }
                        }
                        else
                        {
                            if (Convert.ToDateTime(e.Row.Cells[25].Text).ToString("dd/MM") != DateTime.Now.ToString("dd/MM"))
                            {
                                e.Row.Cells[25].Text = "<div style='color:red'>" + Convert.ToDateTime(e.Row.Cells[25].Text).ToString("HH:mm d/M") + "</div>";
                            }
                            else
                            {
                                e.Row.Cells[25].Text = "<div style='color:red'>" + Convert.ToDateTime(e.Row.Cells[25].Text).ToString("HH:mm") + "</div>";
                            }
                        }

                    }
                    else
                    {
                        e.Row.Cells[25].Text = "<div>" + e.Row.Cells[25].Text + "</div>";

                    }
                    //if (Convert.ToDateTime(e.Row.Cells[25].Text).ToString("dd/MM") != DateTime.Now.ToString("dd/MM"))
                    //{
                    //    if (Convert.ToDateTime(e.Row.Cells[25].Text).ToString("dd/MM") == PDTTime.ToString())
                    //    {
                    //        e.Row.Cells[25].Text = "<div>" + Convert.ToDateTime(e.Row.Cells[25].Text).ToString("HH:mm") + "</div>";
                    //    }
                    //    else
                    //    {
                    //        e.Row.Cells[25].Text = "<div>" + Convert.ToDateTime(e.Row.Cells[25].Text).ToString("HH:mm d/M") + "</div>";

                    //    }
                    //}
                    //else
                    //{
                    //    e.Row.Cells[25].Text = "<div>" + Convert.ToDateTime(e.Row.Cells[25].Text).ToString("HH:mm") + "</div>";
                    //}

                }
                catch (Exception ex)
                {

                }

            }
        }
    }
    private string getPDTTimeTime(int Refid)
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
        FillRemarksTemplate(6, ref ddlSRemarks);
        FillRemarksTemplate(5, ref ddVOutRemarks);
        FillRemarksTemplate(2, ref ddPDTRemarks);
        FillRemarksTemplate(4, ref CmbCancelationRemarks);
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

    protected void Page_Load(object sender, EventArgs e)
    {
        LostCust();
        try
        {
            if (Session[Session["TMLDealercode"] + "-TMLConString"].ToString() == null || Session[Session["TMLDealercode"] + "-TMLConString"].ToString() == "" || Session["ROLE"].ToString() != "WORK MANAGER")
                Response.Redirect("login.aspx");
        }
        catch
        {
            Response.Redirect("login.aspx");
        }
        try
        {
            //lbScroll0.Text = Session["DealerName"].ToString();
            if (!Page.IsPostBack)
            {
                SqlDataSource1.ConnectionString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
                srcServiceType.ConnectionString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
                srcVehicleModel.ConnectionString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
                SqlDataSource2.ConnectionString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
                SqlDataSource4.ConnectionString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
                SqlDataSource5.ConnectionString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
                lbl_currTime.Text = DateTime.Now.ToString("dd MMM yyyy, HH:mm:ss");
                form1.Style.Value = "left:0px;";
             
                TxtDate1.Attributes.Add("readonly", "readonly");
                TxtDate2.Attributes.Add("readonly", "readonly");
                txtRevPDT.Text = DateTime.Now.ToShortDateString();
                getSA();
                getTL();
                TabContainer1.Visible = false;
                if (Page.Request.QueryString["Back"] != null)
                {
                    BackTo = Session["Role"].ToString();
                }
             
                lbl_LoginName.Text = Session["UserId"].ToString() + "&nbsp;&nbsp;";
                
            }

            try
            {
               
                if (!(Request.Form["_EventTarget"] != null && Request.Form["_EventTarget"] == "myClick"))
                {
                    BindGrid();
                    if (Request.Form["__EVENTARGUMENT"].ToString() != null || Request.Form["__EVENTARGUMENT"].ToString() != "")
                    { grdDisplay.SelectedIndex = int.Parse(Request.Form["__EVENTARGUMENT"].ToString()); }
                   
                    if (grdDisplay.SelectedRow.Cells[2].Text.Replace("&nbsp;", "").Trim().Replace("<div style=width:26px;>", "").Replace("</div>", "") != "")
                    {
                        FillRemarksTemplate(6, ref ddlSRemarks);
                        FillRemarksTemplate(5, ref ddVOutRemarks);
                        FillRemarksTemplate(2, ref ddPDTRemarks);
                        FillRemarksTemplate(4, ref CmbCancelationRemarks);
                        string regno = GetRegNo(grdDisplay.SelectedRow.Cells[2].Text.Replace("&nbsp;", "").Trim().Replace("<div style=width:26px;>", "").Replace("</div>", ""));
                        DataTable dt2 = GetAll(grdDisplay.SelectedRow.Cells[2].Text.Replace("&nbsp;", "").Trim().Replace("<div style=width:26px;>", "").Replace("</div>", ""));
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
                        txtcardno.Text = grdDisplay.SelectedRow.Cells[2].Text.Replace("&nbsp;", "").Trim().Replace("<div style=width:26px;>", "").Replace("</div>", "");
                        lblTagNo.Text = grdDisplay.SelectedRow.Cells[2].Text.Replace("&nbsp;", "").Trim().Replace("<div style=width:26px;>", "").Replace("</div>", "");
                        lblTagnoforHold.Text = grdDisplay.SelectedRow.Cells[2].Text.Replace("&nbsp;", "").Trim().Replace("<div style=width:26px;>", "").Replace("</div>", "");
                        lblpdtvehno.Text = regno;
                        lblspvehicleno.Text = regno;
                        int SlNo = 0;
                        SlNo = Convert.ToInt32(grdDisplay.SelectedRow.Cells[21].Text.Replace("&nbsp;", "").Trim());
                        lblRefnoParts.Text = grdDisplay.SelectedRow.Cells[21].Text.Replace("&nbsp;", "").Trim();
                        lblSlNoSA.Text = grdDisplay.SelectedRow.Cells[21].Text.Replace("&nbsp;", "").Trim();
                        lblSlnoforHold.Text = grdDisplay.SelectedRow.Cells[21].Text.Replace("&nbsp;", "").Trim();
                        getSACRM(lblSlNoSA.Text);
                        lblvcvehicleno.Text = regno;
                        lblvovehicleno.Text = regno;
                        lblrefno.Text = grdDisplay.SelectedRow.Cells[21].Text.Replace("&nbsp;", "").Trim();
                        getPDTPDC(grdDisplay.SelectedRow.Cells[21].Text.Replace("&nbsp;", "").Trim());
                        getRemainingTabs(grdDisplay.SelectedRow.Cells[21].Text.Replace("&nbsp;", "").Trim(), grdDisplay.SelectedRow.RowIndex.ToString());
                        getTagCancelation(SlNo);
                        BindPartsGrid(Convert.ToInt32(lblRefnoParts.Text));
                        BindTechnicianList(Convert.ToInt32(grdDisplay.SelectedRow.Cells[21].Text.Trim()), grdDisplay.SelectedRow.Cells[22].Text.Trim(), grdDisplay.SelectedRow.Cells[23].Text.Trim(), grdDisplay.SelectedRow.Cells[24].Text.Trim());
                        txt_TechRemarks.Text = "";
                        TabContainer1.Visible = true;
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

    private static DataTable GetProcessHover(string RefNo, string ProcessId)
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
        try { 
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
        catch (Exception ex)
        {
            return null;
        }
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
        try
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
        catch (Exception ex)
        {
            return null;
           
        }
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
        int RefNo = DataManager.GetRefNo(RegNo, Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
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
            con.Open();
            sda.Fill(dt);

            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("Select", "0"));
            con.Close();
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
               // txtspremarks.Visible = true;
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
            if (Session["ROLE"].ToString() == "SERVICE ADVISOR")
            {
                cmd.CommandText = "GetCountVehicleStatusI";
                cmd.Parameters.AddWithValue("@EmpId", Session["EmpId"].ToString());
                cmd.Parameters.AddWithValue("@isBodyshop", 0);
            }
            else
            {
                cmd.CommandText = "GetCountVehicleStatusI";
                cmd.Parameters.AddWithValue("@EmpId", "0");
                cmd.Parameters.AddWithValue("@isBodyshop", 0);
            }
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
                lblTotalReceived.Text = "0";
                lblVehDel.Text = "0";
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
            con.Open();
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
        using (SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString()))
        {
            SqlCommand cmd = new SqlCommand("GetCurrentProcessInTime", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TagNo", e.Row.Cells[2].Text.ToString());
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            sda.Fill(dt);
            con.Close();
            return dt.Rows[0]["Code"].ToString();
        }
    }

    


    private List<Display>  GetDisplayDate(string Type, string vehiclenumber, int param, Boolean ISCollection)
    {
        string whichDay = string.Empty;
        if (rbType.SelectedIndex == 0)
        {
            whichDay = "1"; // Today
        }
        else if (rbType.SelectedIndex == 2)
        {
            whichDay = "2"; // All Day
        }
        else
        {
            whichDay = "0"; // Previous
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

        if (Session["ROLE"].ToString() == "SERVICE ADVISOR")
        {
            SAId = Session["EmpId"].ToString();
        }
        if (Session["ROLE"].ToString() == "CRM")
        {
            EmpId = Session["EmpId"].ToString();
        }

        using (SqlConnection oConn = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString()))
        {
            SqlCommand cmd = new SqlCommand("", oConn);

            DataSet oDs = new DataSet();
            DataTable oDt = new DataTable();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "JCDisplayI_ForJCR"; // JobControllerDisplayI
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
            oConn.Open();
            // SqlDataAdapter da = new SqlDataAdapter(cmd);

            SqlDataReader dr = cmd.ExecuteReader();
            List<Display> Disp = new List<Display>();
            while (dr.Read())
            {
                Disp.Add(new Display()
                {
                    //Age = dr["Age"].ToString(),
                    Slno = Convert.ToInt32(dr["#"].ToString()),
                    Bay = dr["Bay"].ToString(),
                    VID = Convert.ToInt16(dr["VID"].ToString()),
                    VRN = dr["VRN/VIN"].ToString(),
                    JDPCW = dr["JDP CW"].ToString(),

                    MODEL = dr["MODEL"].ToString(),
                    ST = dr["ST"].ToString(),

                    STATUS = dr["STATUS"].ToString(),
                    REQPARTS = Convert.ToInt16(dr["ReqParts"].ToString()),
                    ALLOTPARTS = Convert.ToInt16(dr["AlootParts"].ToString()),

                    BA = dr["BA"].ToString(),
                    T1 = dr["T1"].ToString(),
                    T2 = dr["T2"].ToString(),

                    T3 = dr["T3"].ToString(),
                    WA = dr["WA"].ToString(),
                    WASH = dr["WSH"].ToString(),
                    QC = dr["QC"].ToString(),

                    PRG = Convert.ToInt32(dr["PRG"].ToString()),
                    PDT = dr["PDT"].ToString(),
                    AGE = Convert.ToInt16(dr["AGE"].ToString()),

                    RMK = dr["RMK"].ToString(),
                    REFNO = Convert.ToInt32(dr["RefNO"].ToString()),
                    T1REM = dr["T1Rem"].ToString(),
                    T2REM = dr["T2Rem"].ToString(),
                    T3REM = dr["T3Rem"].ToString(),
                    ERT = dr["ERT"].ToString()

                });
            }
            try
            {
             
                return Disp;
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
    }

    private void getPDTPDC(string RefNo)
    {
        using (SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString()))
        {
            SqlCommand cmd = new SqlCommand("GetPDTPDC", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RefNo", RefNo);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                lblpdtno.Text = dt.Rows[0]["PDT"].ToString().Replace("#", " ");
            }
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
        using (SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString()))
        {
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
        using (SqlConnection oConn = new SqlConnection(sConnString))
        {
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
                oConn.Open();
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
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        DataTable dt = new DataTable();
        try
        {
            String query = "Select dbo.GetTechnicianForService(@ServiceId) As Technician";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ServiceId", ServiceId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
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
        finally
        {
            con.Close();
        }
    }

    private int InsertRemarks(string Remarks, string ProcessId, string ServiceId, string RegNo)
    {
        SqlConnection Conn = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());

        try
        {
          
            SqlCommand cmd = new SqlCommand("", Conn);
            if (Conn.State != ConnectionState.Open)
            {
                Conn.Open();
            }
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
            imgType.HRef = "JCRDisplayWorks.aspx?typ=1";
        }
        else
        {
            imgType.HRef = "JCRDisplayWorks.aspx?typ=0";
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
            Timer1.Enabled = false;
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
            //lblErrormsg.Text = "Error,PopUp:" + ex.Message;
            //lblErrormsg.ForeColor = Color.Red;
        }
        finally
        {
            oConn.Close();
        }
    }

    #region "HOVERING"

    [System.Web.Services.WebMethod(EnableSession = true)]
    public static string LoadEmployeeInOutTime(string Slno, string EmpId, string Tech)
    {
        DataTable dt = new DataTable();
        dt = GetEmployeeHover(Slno, EmpId);
        string inTime = "";
        string outTime = "";
        string EmpName = "";
        if (dt.Rows.Count > 0)
        {
            inTime = dt.Rows[0][0].ToString().Replace("#", " ");
            outTime = dt.Rows[0][1].ToString().Replace("#", " ");
            EmpName = dt.Rows[0][2].ToString();
        }

        string str = "<span><table class='mydatagrid' id=InOutPnl style=width:100%;><tr style=text-align:center;><th colspan=2 style='color:#a62724;'>&nbsp;&nbsp;" + Tech + ": " + EmpName + "</th></tr><tr><td style=width:90px;>In Time: </td><td style=text-align:center;>" + inTime + "</td></tr><tr><td style=width:90px;>Out Time: </td><td style=text-align:center;>" + outTime + "</td></tr></table></span>";
        return str;
    }

    [System.Web.Services.WebMethod(EnableSession = true)]
    public static string LoadInOutTime(string ServiceId, string ProcessName)
    {
        DataTable dt = new DataTable();
        dt = GetInOutTime(ServiceId, ProcessName);
        string inTime = "";
        string outTime = "";
        if (dt.Rows.Count > 0)
        {
            inTime = dt.Rows[0][0].ToString().Replace("#", " ");
            outTime = dt.Rows[0][1].ToString().Replace("#", " ");
        }

        string str = "<span><table id=InOutPnl style=width:100%;><tr><td style=width:50px;>In Time: </td><td style=text-align:center;>" + inTime + "</td></tr><tr><td style=width:50px;>Out Time: </td><td style=text-align:center;>" + outTime + "</td></tr></table></span>";
        return str;
    }

    [System.Web.Services.WebMethod(EnableSession = true)]
    public static string LoadProcessInOutTime(string RefNo, string ProcessId, string ProcessName)
    {
        DataTable dt = new DataTable();
        dt = GetProcessHover(RefNo, ProcessId);
        string inTime = "";
        string outTime = "";
        string empName = "";
        if (dt.Rows.Count > 0)
        {
            inTime = dt.Rows[0][0].ToString().Replace("#", " ");
            outTime = dt.Rows[0][1].ToString().Replace("#", " ");
            empName = dt.Rows[0][2].ToString().Trim();
        }

        string str = "<span><table class='mydatagrid' id=InOutPnl style=width:100%;><tr font-weight:800;><th colspan=2 style=color:#a62724;>&nbsp;&nbsp;" + ProcessName + " : " + empName + "</th></tr><tr><td style=width:90px;>In Time: </td><td style=text-align:center;>" + inTime + "</td></tr><tr><td style=width:90px;>Out Time: </td><td style=text-align:center;>" + outTime + "</td></tr></table></span>";
        return str;
    }

    [System.Web.Services.WebMethod(EnableSession = true)]
    public static string LoadRegNoHover(string RefNo)
    {
        DataTable dt = new DataTable();
        dt = GetRegNoHover(RefNo);

        string tt1 = ((dt.Rows[0]["RegNo"].ToString().Trim() == "") ? "___________" : dt.Rows[0]["RegNo"].ToString());
        string tt3 = dt.Rows[0]["CustomerName"].ToString();
        string tt4 = dt.Rows[0]["CustomerPhone"].ToString();
        string tt5 = dt.Rows[0]["VehicleModel"].ToString();
        string tt6 = dt.Rows[0]["GateIn"].ToString().Replace("#", " ");
        string tt7 = dt.Rows[0]["Position"].ToString();
        string tt8 = dt.Rows[0]["JobCardNo"].ToString();
        string tt9 = dt.Rows[0]["ChassisNo"].ToString();
        string tt10 = dt.Rows[0]["KMS"].ToString();
        string tt11 = dt.Rows[0]["ServiceAdvisor"].ToString().Replace(",", "");
        string tt12 = dt.Rows[0]["EngineNo"].ToString();
        string tt13 = dt.Rows[0]["Date_Of_Sale"].ToString();
        string tt14 = dt.Rows[0]["Sold_Dealer_Id"].ToString();
        string tt15 = dt.Rows[0]["CurrentStatus"].ToString();
        string tt16 = dt.Rows[0]["GateOut"].ToString().Replace("#"," ");
        string tt17 = dt.Rows[0]["KMS"].ToString();
        string str;
        //if (statusVal == "13" || statusVal == "14" || statusVal == "15")
        //str = "<span><table style='width: 210px;' cellpadding='0' cellspacing='0'><tr bgcolor='#8db4e3'><td style='width: 100px;'>REGISTRATION NO</td><td>:</td><td class='ttipBodyVal'>" + tt1 + "</td></tr><tr><td>CUSTOMER NAME</td><td>:</td><td class='ttipBodyVal'>" + tt3 + "</td><CONTACT NO</td><tr bgcolor='#8db4e3'><td><span>CONTACT NO</span></td><td>:</td><td class='ttipBodyVal'><span>" + tt4 + "</span></td></tr><tr><td style='width: 100px;'>VEHICLE IN TIME</td><td>:</td><td class='ttipBodyVal'>" + tt6 + "</tr><tr bgcolor='#8db4e3'><td style='width: 100px;'>MODEL</td><td>:</td><td class='ttipBodyVal'>&nbsp;" + tt5 + "</tr><tr><td style='width: 100px;'>ADVISOR NAME</td><td>:</td><td class='ttipBodyVal'>" + tt11 + "</tr><tr bgcolor='#8db4e3'><td >CURRENT POSITION</td><td>:</td><td>" + tt7 + "</tr><tr><td style='width: 100px;'>CURRENT STATUS</td><td>:</td><td class='ttipBodyVal'>" + tt15 + "</tr><tr bgcolor='#8db4e3'><td >VEHICLE OUT TIME</td><td>:</td><td>" + tt16 + "</tr><tr><td >CURRENT KMS</td><td>:</td><td class='ttipBodyVal'>" + tt17 + "</tr></table></span>";
        if(tt7=="Delivered")
        str = "<table class='mydatagrid'><tr><td style='color:#a62724'><strong>CUSTOMER NAME</strong></td><td>:</td><td>" + tt3 + "</td></tr><tr><td style='color:#a62724'><strong>CONTACT NO</strong></td><td>:</td><td>" + tt4 + "</td></tr><tr style='height:18px'><td style='color:#a62724'><strong>IN TIME</strong></td><td>:</td><td>" + tt6 + "</td></tr><tr style='height:18px'><td style='color:#a62724'><strong>SA NAME<strong></td><td>:</td><td>" + tt11 + "</td></tr><tr style='height:18px'><td style='color:#a62724'><strong>STAGE<strong></td><td>:</td><td>" + tt7 + "</td></tr><tr style='height:18px'><td><strong>OUT TIME</strong></td><td>:</td><td>" + tt16 + "</td></tr><tr style='height:18px'><td style='color:#a62724'><strong>KMS</strong></td><td>:</td><td>" + tt17 + "</td></tr></table></span>";
        else
            str = "<table class='mydatagrid'><tr class='tbl_th'><td style='color:#a62724'><strong>CUSTOMER NAME</strong></td><td>:</td><td style='padding:4px;'>" + tt3 + "</td></tr><tr style='height:18px'><td style='color:#a62724'><strong>CONTACT NO</strong></td><td>:</td><td style='padding:4px;'>" + tt4 + "</td></tr><tr style='height:18px'><td style='color:#a62724'><strong>IN TIME</strong></td><td>:</td><td style='padding:4px;'>" + tt6 + "</td></tr><tr style='height:18px'><td style='color:#a62724'><strong>SA NAME</strong></td><td>:</td><td style='padding:4px;'>" + tt11 + "</td></tr><tr style='height:18px'><td style='color:#a62724'><strong>STAGE</strong></td><td>:</td><td style='padding:4px;'>" + tt7 + "</td></tr><tr style='height:18px'><td style='color:#a62724'><strong>KMS</strong></td><td>:</td><td style='padding:4px;'>" + tt17 + "</td></tr></table></span>";
        //else
        //    str = "<span><table style='width: 210px;' cellpadding='0' cellspacing='0'><tr bgcolor='#999'><td style='width: 100px;'>REGISTRATION NO</td><td>:</td><td class='ttipBodyVal'>" + tt1 + "</td></tr><tr><td>CUSTOMER NAME</td><td>:</td><td class='ttipBodyVal'>" + tt3 + "</td><CONTACT NO</td><tr bgcolor='#8db4e3'><td><span>CONTACT NO</span></td><td>:</td><td class='ttipBodyVal'><span>" + tt4 + "</span></td></tr><tr><td style='width: 100px;'>VEHICLE IN TIME</td><td>:</td><td class='ttipBodyVal'>" + tt6 + "</tr><tr bgcolor='#8db4e3'><td style='width: 100px;'>MODEL</td><td>:</td><td class='ttipBodyVal'>&nbsp;" + tt5 + "</tr><tr><td style='width: 100px;'>ADVISOR NAME</td><td>:</td><td class='ttipBodyVal'>" + tt11 + "</tr><tr bgcolor='#8db4e3'><td >CURRENT POSITION</td><td>:</td><td>" + tt7 + "</tr><tr><td style='width: 100px;'>CURRENT STATUS</td><td>:</td><td class='ttipBodyVal'>" + tt15 + "</tr><tr bgcolor='#8db4e3'><td style='width: 100px;'>CURRENT KMS</td><td>:</td><td>" + tt17 + "</tr></table></span>";

        return str;
    }

    [System.Web.Services.WebMethod(EnableSession = true)]
    public static string LoadRemarks(string RefNo)
    {
        DataTable dt = new DataTable();
        dt = GetRemarksHover(RefNo);
        string str = "";
        if (dt.Rows.Count > 0)
        {
            str = "<span><table class='mydatagrid' style='width:240px'><tr style=text-align:center;><th style='color:#a62724;width:120px'>&nbsp;&nbsp;Time</th><th style='color:#a62724;width:140px'>&nbsp;&nbsp;Remarks</th></tr>";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                str += "<tr><td style='width:120px'>" + dt.Rows[i][0].ToString().Replace("#", " ") + "</td><td>" + dt.Rows[i][1].ToString() + "</td></tr>";
            }
            str += "</table></span>";
        }
        else
        {
            str = "<table style=width:100px;text-align:center;><tr><th>No Remarks</th></tr></table>";
        }
        return str;
    }

    [System.Web.Services.WebMethod(EnableSession = true)]
    public static string LoadJADetails(string RefNo)
    {
        DataTable dt = new DataTable();
        dt = GetJAHover(RefNo);
        //string str = "<table cellpadding='0' cellspacing='0' border='0'><tr bgcolor='#E4E4E4'><td><strong>Bay #</strong></td><td></td><td align='center' valign='middle'><strong>Team Lead</strong></td><td align='center' valign='middle'></td><td align='center' valign='middle'><strong>In Time</strong></td><td align='center' valign='middle'></td><td align='center' valign='middle'><strong>Allotted</strong></td></tr><tr><td>" + dt.Rows[0][0].ToString() + "</td><td></td><td align='center' valign='middle'>" + dt.Rows[0][3].ToString() + "</td><td align='center' valign='middle'></td><td align='center' valign='middle'> " + dt.Rows[0][1].ToString() + "</td><td align='center' valign='middle'></td><td align='center' valign='middle'>" + dt.Rows[0][2].ToString() + "</td></tr><tr bgcolor='White'><td></td><td></td><td align='center' valign='middle'></td><td align='center' valign='middle'></td><td align='center' valign='middle'></td><td align='center' valign='middle'></td><td align='center' valign='middle'></td></tr><tr bgcolor='#E4E4E4'><td><strong>Technician</strong></td><td width='10px'></td><td align='center' valign='middle'>" + dt.Rows[0][4].ToString() + "</td><td align='center' valign='middle' width='10px'></td><td align='center' valign='middle'>" + dt.Rows[0][7].ToString() + "</td><td align='center' valign='middle' width='10px'></td><td align='center' valign='middle'>" + dt.Rows[0][10].ToString() + "</td></tr><tr><td bgcolor='#E4E4E4'><strong>Jobs</strong></td><td bgcolor='#E4E4E4'></td><td align='center' valign='middle'>" + dt.Rows[0][5].ToString() + "</td><td align='center' valign='middle'></td><td align='center' valign='middle'>" + dt.Rows[0][8].ToString() + "</td><td align='center' valign='middle'></td><td align='center' valign='middle'>" + dt.Rows[0][11].ToString() + "</td></tr><tr><td  bgcolor='#E4E4E4'><strong>Allotted</strong></td><td  bgcolor='#E4E4E4'></td><td align='center' valign='middle'>" + dt.Rows[0][6].ToString() + "</td><td align='center' valign='middle'></td><td align='center' valign='middle'> " + dt.Rows[0][9].ToString() + "</td><td align='center' valign='middle'></td><td align='center' valign='middle'>" + dt.Rows[0][12].ToString() + "</td></tr></table>";
        string str = "<span><table class='mydatagrid'><tr><td style='color:#a62724;'><strong>Bay #</strong></td><td></td><td style='color:#a62724;'><strong>Team Lead</strong></td><td align='center' valign='middle'></td><td style='color:#a62724;'><strong>In Time</strong></td><td></td><td style='color:#a62724;'><strong>Allotted</strong></td></tr><tr><td>" + dt.Rows[0][0].ToString() + "</td><td></td><td align='center' valign='middle'>" + dt.Rows[0][3].ToString() + "</td><td align='center' valign='middle'></td><td align='center' valign='middle'> " + dt.Rows[0][1].ToString() + "</td><td align='center' valign='middle'></td><td align='center' valign='middle'>" + dt.Rows[0][2].ToString() + "</td></tr><tr bgcolor='White'><td></td><td></td><td align='center' valign='middle'></td><td align='center' valign='middle'></td><td align='center' valign='middle'></td><td align='center' valign='middle'></td><td align='center' valign='middle'></td></tr><tr><td style='color:#a62724;'><strong>Technician</strong></td><td width='10px'></td><td align='center' valign='middle'>" + dt.Rows[0][4].ToString() + "</td><td align='center' valign='middle' width='10px'></td><td align='center' valign='middle'>" + dt.Rows[0][8].ToString() + "</td><td align='center' valign='middle' width='10px'></td><td align='center' valign='middle'>" + dt.Rows[0][12].ToString() + "</td></tr><tr><td style='color:#a62724;'><strong>Jobs</strong></td><td></td><td align='center' valign='middle'>" + dt.Rows[0][5].ToString() + "</td><td align='center' valign='middle'></td><td align='center' valign='middle'>" + dt.Rows[0][9].ToString() + "</td><td align='center' valign='middle'></td><td align='center' valign='middle'>" + dt.Rows[0][13].ToString() + "</td></tr><tr><td style='color:#a62724;'><strong>Job Desc</strong></td><td></td><td align='center' valign='middle'>" + dt.Rows[0][6].ToString() + "</td><td align='center' valign='middle'></td><td align='center' valign='middle'>" + dt.Rows[0][10].ToString() + "</td><td align='center' valign='middle'></td><td align='center' valign='middle'>" + dt.Rows[0][14].ToString() + "</td></tr><tr><td style='color:#a62724;'><strong>Allotted</strong></td><td></td><td align='center' valign='middle'>" + dt.Rows[0][7].ToString() + "</td><td align='center' valign='middle'></td><td align='center' valign='middle'> " + dt.Rows[0][11].ToString() + "</td><td align='center' valign='middle'></td><td align='center' valign='middle'>" + dt.Rows[0][15].ToString() + "</td></tr></table></span>";
        return str;
    }

    [System.Web.Services.WebMethod(EnableSession = true)]
    public static string LoadPartsDetails(string RefNo)
    {
        DataTable dt = new DataTable();
        dt = GetPartsHover(RefNo);
        string str = "";
        string AllotedTime = "";
        if (dt.Rows.Count > 0)
        {
            str = "<table class='mydatagrid' cellpadding='0' cellspacing='0' border='0'><tr style='color:#a62724'><td style='padding-left:3px;'><strong>PARTS NAME</strong></td><td style='width:50px;' align='center'><strong>STATUS</strong></td><td><strong>AVAILABILITY TIME</strong></td></tr>";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i % 2 == 0)
                {
                    if (dt.Rows[i]["Alloted"].ToString() == "Y")
                    {
                        AllotedTime = dt.Rows[i]["AllotedDate"].ToString().Replace("#", " ");
                        str = str + "<tr style='height:20px;'><td style='padding-left:3px;'>" + dt.Rows[i]["PartsName"].ToString() + "</td><td style='width:50px;' align='center'>" + dt.Rows[i]["Alloted"].ToString().Replace("N", "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/No.png' Alt=''  width='16' height='16'/>").Replace("Y", "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/CN.png' Alt=''  width='16' height='16'/>") + "</td><td style='padding-left:3px;' align='center'>" + AllotedTime + "</td></tr>";
                    }
                    else
                    {
                        AllotedTime = dt.Rows[i]["AllotedDate"].ToString().Replace("#", " ");
                        str = str + "<tr style='height:20px;'><td style='padding-left:3px;'>" + dt.Rows[i]["PartsName"].ToString() + "</td><td style='width:50px;' align='center'>" + dt.Rows[i]["Alloted"].ToString().Replace("N", "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/No.png' Alt=''  width='16' height='16'/>").Replace("Y", "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/CN.png' Alt=''  width='16' height='16'/>") + "</td><td style='padding-left:3px;' align='center'>" + dt.Rows[i]["AllotedDate"].ToString().Replace("#", " ") + "</td></tr>";
                    }
                }
                else
                {
                    //str = str + "<tr style='height:20px;' bgcolor='#FFFFFF'><td style='padding-left:3px;'>" + dt.Rows[i]["PartsName"].ToString() + "</td><td style='width:50px;' align='center'>" + dt.Rows[i]["Alloted"].ToString().Replace("N", "<img src='images/JCR/No.png' Alt=''  width='16' height='16'/>").Replace("Y", "<img src='images/JCR/CN.png' Alt=''  width='16' height='16'/>") + "</td><td style='padding-left:3px;' align='center'>" + AllotedTime + "</td></tr>";
                    if (dt.Rows[i]["Alloted"].ToString() == "Y")
                    {
                        AllotedTime = dt.Rows[i]["AllotedDate"].ToString().Replace("#", " ");
                        str = str + "<tr style='height:20px;' bgcolor='#FFFFFF'><td style='padding-left:3px;'>" + dt.Rows[i]["PartsName"].ToString() + "</td><td style='width:50px;' align='center'>" + dt.Rows[i]["Alloted"].ToString().Replace("N", "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/No.png' Alt=''  width='16' height='16'/>").Replace("Y", "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/CN.png' Alt=''  width='16' height='16'/>") + "</td><td style='padding-left:3px;' align='center'>" + AllotedTime + "</td></tr>";
                    }
                    else
                    {
                        AllotedTime = dt.Rows[i]["AllotedDate"].ToString().Replace("#", " ");
                        str = str + "<tr style='height:20px;' bgcolor='#FFFFFF'><td style='padding-left:3px;'>" + dt.Rows[i]["PartsName"].ToString() + "</td><td style='width:50px;' align='center'>" + dt.Rows[i]["Alloted"].ToString().Replace("N", "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/No.png' Alt=''  width='16' height='16'/>").Replace("Y", "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/CN.png' Alt=''  width='16' height='16'/>") + "</td><td style='padding-left:3px;' align='center'>" + dt.Rows[i]["AllotedDate"].ToString().Replace("#", " ") + "</td></tr>";
                    }
                }
            }
            str = str + "</table>";
        }
        else
        {
            str = "";
        }
        return str;
    }

    [System.Web.Services.WebMethod(EnableSession = true)]
    public static string LoadSubProcessInOutTime(string RefNo, string SubProcessId, string SubProcessName)
    {
        DataTable dt = new DataTable();
        dt = GetSubProcessHover(RefNo, SubProcessId);
        string inTime = "";
        string outTime = "";
        string empName = "";
        if (dt.Rows.Count > 0)
        {
            inTime = dt.Rows[0][0].ToString().Replace("#", " ");
            outTime = dt.Rows[0][1].ToString().Replace("#", " ");
            empName = dt.Rows[0][2].ToString().Trim();
        }

        string str = "<span><table id=InOutPnl style=width:100%;><tr style=text-align:center;color:steelblue;background-color:silver;><td colspan=2>" + SubProcessName + " : " + empName + "</td></tr><tr><td style=width:50px;>In Time: </td><td style=text-align:center;>" + inTime + "</td></tr><tr><td style=width:50px;>Out Time: </td><td style=text-align:center;>" + outTime + "</td></tr></table></span>";
        return str;
    }

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

    protected void grdParts_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
            GridViewRow dr = (GridViewRow)grdParts.Rows[e.RowIndex];
            int slno = Convert.ToInt32(dr.Cells[0].Text);
            int RefNo = Convert.ToInt32(dr.Cells[1].Text);
            int Status = Convert.ToInt16(dr.Cells[3].Text);
            if (Status == 2)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                lblmsg.CssClass = "ErrMsg";
                lblmsg.Text = "Part is in Used";
            }
            else
            {
                SqlCommand cmddel = new SqlCommand("udpDeletePartsRequisition", con);
                cmddel.CommandType = CommandType.StoredProcedure;
                cmddel.Parameters.AddWithValue("@SlNo", slno);
                con.Open();
                cmddel.ExecuteNonQuery();
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                lblmsg.CssClass = "ScsMsg";
                lblmsg.Text = "Deleted Successfully";
                BindPartsGrid(Convert.ToInt32(lblRefnoParts.Text.ToString()));
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void btnParts_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        try
        {
            if (txtParts.Text.ToString() == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                lblmsg.CssClass = "ErrMsg";
                lblmsg.Text = "Please Enter Parts";
            }
            else
            {
                con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
                cmd = new SqlCommand("udpInsertPartsRequisition", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RefNo", Convert.ToInt32(lblRefnoParts.Text.ToString()));
                cmd.Parameters.AddWithValue("@PartsName", txtParts.Text.ToString());
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd.ExecuteNonQuery();
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                lblmsg.CssClass = "ScsMsg";
                lblmsg.Text = "Inserted Successfully..!";
                BindPartsGrid(Convert.ToInt32(lblRefnoParts.Text.ToString()));
                txtParts.Text = "";
            }
        }
        catch (Exception ex)
        {
            if (con.State == ConnectionState.Open)
                con.Close();
        }
    }

    protected void grdParts_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdParts.PageIndex = e.NewPageIndex;
        BindPartsGrid(Convert.ToInt32(lblRefnoParts.Text.ToString()));
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public static string[] GetCompletionList(string prefixText, int count)
    {
        SqlCommand cmd = new SqlCommand();
        SqlConnection con1 = new SqlConnection(System.Web.HttpContext.Current.Session[HttpContext.Current.Session["TMLDealercode"] + "-TMLConString"].ToString());
        cmd.Connection = con1;
        cmd.CommandText = "udpGetPartsRequisition";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@prefixText", prefixText);
        if (con1.State != ConnectionState.Open)
            con1.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        string[] results = new string[dt.Rows.Count];
        for (int index = 0; index < dt.Rows.Count; index++)
        {
            results[index] = dt.Rows[index][0].ToString();
        }

        return results;
    }

    protected void grdParts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[3].Visible = false;
        }
    }

    protected void btn_SaveTechRemarks_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlCommand cmd = new SqlCommand();
        try
        {
            if (ddlTechList.SelectedValue.ToString() == "0")
            {
                lblmsg.Text = "Please Select Technician. !";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                lblmsg.CssClass = "ErrMsg";
            }
            else if (txt_TechRemarks.Text.ToString() == "")
            {
                lblmsg.Text = "Please Enter Remarks. !";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                lblmsg.CssClass = "ErrMsg";
            }
            else
            {
                con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
                cmd = new SqlCommand("udpJCRTechRemark", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RefNo", Convert.ToInt32(lbl_TechRefId.Text.ToString()));
                cmd.Parameters.AddWithValue("@WorkId", ddlTechList.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Remarks", txt_TechRemarks.Text.ToString());
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd.ExecuteNonQuery();
                lblmsg.Text = "Inserted Successfully..!";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                lblmsg.CssClass = "ScsMsg";
                
                Page_Load(null, null);
                ddlTechList.SelectedValue = "0";
                txt_TechRemarks.Text = "";
            }
        }
        catch (Exception ex)
        {
            if (con.State == ConnectionState.Open)
                con.Close();            
        }
    }

    protected void btn_jcrAllotment_Click(object sender, EventArgs e)
    {
        Response.Redirect("NewJobAllotment.aspx");
    }

    protected void btn_kpiDashboard_Click(object sender, EventArgs e)
    {
        Response.Redirect("LatestVehiclePositionDisplay.aspx?Back=101");
    }

    protected void btn_jcrREPORTS_Click(object sender, EventArgs e) //Added by Pratik for JCR reports addition
    {
        Response.Redirect("JCRReports.aspx");
    }

    protected void btn_position_Click(object sender, EventArgs e)
    {
        Response.Redirect("LatestVehiclePositionDisplay.aspx?Back=101");
    }

   
}

public class Display
{
    public Int32 Slno { get; set; }
    public string Bay { get; set; }
    public Int32 VID { get; set; }
    public string VRN { get; set; }
    public string JDPCW { get; set; }
    public string MODEL { get; set; }
    public string ST { get; set; }
    public string STATUS { get; set; }
    public int REQPARTS { get; set; }
    public int ALLOTPARTS { get; set; }
    public String BA { get; set; }
    public string T1 { get; set; }
    public string T2 { get; set; }
    public string T3 { get; set; }
    public string WA { get; set; }
    public string WASH { get; set; }
    public string QC { get; set; }
    public int PRG { get; set; }
    public string PDT { get; set; }
    public Int16 AGE { get; set; }
    public string RMK { get; set; }
    public Int32 REFNO { get; set; }
    public string T1REM { get; set; }
    public string T2REM { get; set; }
    public string T3REM { get; set; }
    public string ERT { get; set; }
}