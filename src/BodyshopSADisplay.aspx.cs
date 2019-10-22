using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class BodyshopSADisplay : System.Web.UI.Page
{
    private static int miniTabIndex = 6;
    private static int fblank = 0;
    private static string statusVal = "";

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

        lbl_currTime.Text = DateTime.Now.ToString("dd MMM yyyy, HH:mm:ss");
        try
        {
            if (Session["ROLE"] == null || Session["ROLE"].ToString() != "WORK MANAGER" && Session["ROLE"].ToString() != "FRONT OFFICE" && Session["ROLE"].ToString() != "SERVICE ADVISOR" && Session["ROLE"].ToString() != "CRM" && Session["ROLE"].ToString() != "DISPLAY" && Session["ROLE"].ToString() != "BodyShop SA" && Session["ROLE"].ToString() != "BodyShop PositionDisplay")
            {
                Response.Redirect("login.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("login.aspx");
        }
        SqlDataSource4.ConnectionString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        srcServiceType.ConnectionString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        srcVehicleModel.ConnectionString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        try
        {
            if (!Page.IsPostBack)
            {
                lblmsg.CssClass = "reset";
                lblmsg.Text = "";
                TxtDate1.Attributes.Add("readonly", "readonly");
                TxtDate2.Attributes.Add("readonly", "readonly");
                TxtDate1.Text = "";
                TxtDate2.Text = "";
                                //txtRevPDT.Text = DateTime.Now.ToShortDateString();
                                //FillRemarksTemplate(6, ref ddlSRemarks);
                               // FillRemarksTemplate(5, ref ddVOutRemarks);
                FillRemarksTemplate(2, ref ddPDTRemarks);
               // getTL();
                TabContainer1.Visible = false;
                ClientScriptManager script = Page.ClientScript;
                script.RegisterStartupScript(this.GetType(), "Alert", "FormWidth();", true);
                TabPanel4.Visible = false;
                BindGrid();
                //if (Session["ROLE"].ToString() == "WORK MANAGER")
                //{
                //    Session["CURRENT_PAGE"] = "Job Controller";
                //    TabPanel12.Visible = true;
                //    TabPanel10.Visible = false;
                //    TabPanel6.Visible = true;
                //    TabPanel2.Visible = false;
                //    TabPanel3.Visible = false;
                //    TabPanel4.Visible = false;
                //    TabPanel5.Visible = false;
                //    TabPanel7.Visible = true;
                //    TabPanel8.Visible = true;
                //    TabPanel11.Visible = true;
                //    TabPanel9.Visible = true;
                //    TabContainer1.ActiveTabIndex = 0;
                //}
                //else if (Session["ROLE"].ToString() == "FRONT OFFICE")
                //{
                //    Session["CURRENT_PAGE"] = "Vehicle Status Display";
                //    TabPanel12.Visible = false;
                //    TabPanel10.Visible = false;
                //    TabPanel6.Visible = false;
                //    TabPanel2.Visible = true;
                //    TabPanel3.Visible = false;
                //    TabPanel4.Visible = true;
                //    TabPanel5.Visible = true;
                //    TabPanel7.Visible = true;
                //    TabPanel8.Visible = true;
                //    TabPanel11.Visible = false;
                //    TabPanel9.Visible = false;
                //    TabContainer1.ActiveTabIndex = 4;
                //}
                //else 
                //  if (Session["ROLE"].ToString() == "SERVICE ADVISOR")
                //    {
                //    Session["CURRENT_PAGE"] = "Service Advisor Display";
                //    TabPanel12.Visible = false;
                //    TabPanel10.Visible = true;
                //    TabPanel6.Visible = false;
                //    TabPanel2.Visible = true;
                //    TabPanel3.Visible = true;
                //    TabPanel4.Visible = true;
                //    TabPanel5.Visible = true;
                //    TabPanel7.Visible = true;
                //    TabPanel8.Visible = true;
                //    TabContainer1.ActiveTabIndex = 2;
                //    TabPanel11.Visible = false;
                //    TabPanel9.Visible = false;
                //    BindGrid();
                //}
                //else if (Session["ROLE"].ToString() == "CRM" || Session["ROLE"].ToString() == "DISPLAY")
                //{
                //    Session["CURRENT_PAGE"] = "CRM Display";
                //    TabPanel12.Visible = false;
                //    TabPanel10.Visible = false;
                //    TabPanel6.Visible = false;
                //    TabPanel2.Visible = false;
                //    TabPanel3.Visible = false;
                //    TabPanel4.Visible = false;
                //    TabPanel5.Visible = false;
                //    TabPanel7.Visible = false;
                //    TabPanel8.Visible = false;
                //    TabPanel11.Visible = false;
                //    TabPanel9.Visible = false;
                //}
                //lbl_CurrentPage.Text = Session["CURRENT_PAGE"].ToString();
                lbl_LoginName.Text = Session["UserId"].ToString();
                try
                {
                    if (!Page.IsPostBack)
                    {
                        lblmsg.CssClass = "reset";
                        lblmsg.Text = "";
                    }
                    else
                    {
                        if (ScriptManager1.IsInAsyncPostBack)
                        {
                            if (Request["__EVENTTARGET"] == Timer1.ClientID)
                            {
                                BindGrid();
                                TabContainer1.Visible = false;
                                if (rbType.SelectedIndex == 0)
                                {
                                    LinkPrevVehicles("1");
                                }
                                else
                                {
                                    LinkPrevVehicles("0");
                                }
                                PnlDisplay.Update();
                            }
                            else
                            {
                                BindGrid();
                                TabContainer1.Visible = false;
                            }
                        }
                        if (!(Request.Form["_EventTarget"] != null && Request.Form["_EventTarget"] == "myClick"))
                        {
                            lblmsg.CssClass = "reset";
                            lblmsg.Text = "";
                            grdDisplay.SelectedIndex = int.Parse(Request.Form["__EVENTARGUMENT"].ToString());
                            if (grdDisplay.SelectedRow.Cells[1].Text.Replace("&nbsp;", "").Trim() != "")
                            {
                                string regno = GetRegNo(grdDisplay.SelectedRow.Cells[1].Text.Trim());
                                DataTable dt1 = GetAll(grdDisplay.SelectedRow.Cells[1].Text.Trim());
                                lblvehno.Text = regno;
                                lblvehicleUPDTag.Text = regno;
                                txtvehicle.Text = regno;
                                txtCustName.Text = dt1.Rows[0]["CustomerName"].ToString();
                                txtmobile.Text = dt1.Rows[0]["CustomerPhone"].ToString();
                                txtemailid.Text = dt1.Rows[0]["Email"].ToString();
                                txtevehno.Text = regno;
                                lblJCCvehno.Text = regno;
                                lblJCClosing.Text = regno;
                                lbBCRegNo.Text = regno;
                                lbBFRegNo.Text = regno;
                                FillBayConfirmationList(DataManager.GetRefNo(regno,Session[Session["TMLDealercode"] + "-TMLConString"].ToString()).ToString());
                                FillBayFree(DataManager.GetRefNo(regno, Session[Session["TMLDealercode"] + "-TMLConString"].ToString()).ToString());
                                //ddPDTRemarks.SelectedIndex = -1;
                                //txtPDTComment.Visible = false;
                                //lblpdtvehno.Text = regno;
                                //lblspvehicleno.Text = regno;
                                lblpdtvehno.Text = regno;
                              //  lblspvehicleno.Text = regno;
                               // lblvcvehicleno.Text = regno;
                               // lblvovehicleno.Text = regno;
                                lblvcvehicleno.Text = regno;
                                lblvovehicleno.Text = regno;
                                if (Session["ROLE"].ToString() == "CRM")
                                {
                                    TabContainer1.Visible = false;
                                }
                                else
                                {
                                    TabContainer1.Visible = true;
                                }
                            }
                            else
                            {
                                TabContainer1.Visible = false;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    TabContainer1.Visible = false;
                }
            }
            try
            {
                if (ScriptManager1.IsInAsyncPostBack)
                {
                    if (Request["__EVENTTARGET"] == Timer1.ClientID)
                    {
                        BindGrid();
                        TabContainer1.Visible = false;
                        if (rbType.SelectedIndex == 0)
                        {
                            LinkPrevVehicles("1");
                        }
                        else
                        {
                            LinkPrevVehicles("0");
                        }
                        PnlDisplay.Update();
                    }
                    else
                    {
                        BindGrid();
                        TabContainer1.Visible = false;
                    }
                }
                else
                {
                    BindGrid();
                    if (rbType.SelectedIndex == 0)
                    {
                        LinkPrevVehicles("1");
                    }
                    else
                    {
                        LinkPrevVehicles("0");
                    }
                }
                if (!(Request.Form["_EventTarget"] != null && Request.Form["_EventTarget"] == "myClick"))
                {
                    lblmsg.Text = "";
                    lblmsg.CssClass = "reset";
                    BindGrid();
                    grdDisplay.SelectedIndex = int.Parse(Request.Form["__EVENTARGUMENT"].ToString());
                    if (grdDisplay.SelectedRow.Cells[1].Text.Replace("&nbsp;", "").Trim().Replace("<div style=width:26px;>", "").Replace("</div>", "") != "")
                    {
                        string regno = GetRegNo(grdDisplay.SelectedRow.Cells[1].Text.Replace("&nbsp;", "").Trim().Replace("<div style=width:26px;>", "").Replace("</div>", ""));
                        DataTable dt2 = GetAll(grdDisplay.SelectedRow.Cells[1].Text.Replace("&nbsp;", "").Trim().Replace("<div style=width:26px;>", "").Replace("</div>", ""));
                        lblvehicleno.Text = regno;
                        lblvehno.Text = regno;
                        lblJCCvehno.Text = regno;
                        lblvehicleUPDTag.Text = regno;
                        lblJCClosing.Text = regno;
                        txtvehicle.Text = regno;
                        lbBCRegNo.Text = regno;
                        lbBFRegNo.Text = regno;
                        //FillBayConfirmationList(DataManager.GetRefNo(regno, Session[Session["TMLDealercode"] + "-TMLConString"].ToString()).ToString());
                       // FillBayFree(DataManager.GetRefNo(regno, Session[Session["TMLDealercode"] + "-TMLConString"].ToString()).ToString());
                        txtCustName.Text = dt2.Rows[0]["CustomerName"].ToString();
                        txtmobile.Text = dt2.Rows[0]["CustomerPhone"].ToString();
                        txtemailid.Text = dt2.Rows[0]["Email"].ToString();
                        txtevehno.Text = regno;
                        //ddPDTRemarks.SelectedIndex = -1;
                        //txtPDTComment.Visible = false;
                        txtcardno.Text = grdDisplay.SelectedRow.Cells[1].Text.Replace("&nbsp;", "").Trim().Replace("<div style=width:26px;>", "").Replace("</div>", "");
                        //lblpdtvehno.Text = regno;
                        //lblspvehicleno.Text = regno;
                        lblvcvehicleno.Text = regno;
                        lblvovehicleno.Text = regno;
                        lblpdtvehno.Text = regno;
                        //lblRefnoParts.Text = grdDisplay.SelectedRow.Cells[16].Text.Replace("&nbsp;", "").Trim();
                        lblrefno.Text = grdDisplay.SelectedRow.Cells[19].Text.Replace("&nbsp;", "").Trim();
                        getPDTPDC(grdDisplay.SelectedRow.Cells[19].Text.Replace("&nbsp;", "").Trim());
                        //BindPartsGrid(Convert.ToInt32(lblRefnoParts.Text));
                        getRemainingTabs(grdDisplay.SelectedRow.Cells[19].Text.Replace("&nbsp;", "").Trim(), grdDisplay.SelectedRow.RowIndex.ToString());
                        if (Session["ROLE"].ToString() == "CRM")
                        {
                            TabContainer1.Visible = false;
                        }
                        else
                        {
                            TabContainer1.Visible = true;
                        }
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
            lblmsg.Text = "";
            lblmsg.CssClass = "reset";
            Response.Redirect("login.aspx");
        }
    }

    private DataTable GetAll(string TagNo)
    {
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        DataTable dt = new DataTable();
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select Top 1 RegNo,CustomerName,CustomerPhone,LandlineNo, Email, Slno from tblMaster where RFID=@TagNo And Delivered=0 order by SlNo desc";
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
            ddl.Items.Add(new ListItem("--Select--", "0"));
            if (dt.Rows.Count > 0)
            {
                ddl.DataSource = dt;
                ddl.DataValueField = "SlNo";
                ddl.DataTextField = "RemarksTemplate";
                ddl.DataBind();
                //lbl_other.Visible = false;
                //txtspremarks.Visible = false;
                //txt_VORemarks.Visible = false;
            }
            else
            {
                //lbl_other.Visible = true;
                //txtspremarks.Visible = true;
                //txt_VORemarks.Visible = true;
            }
            ddl.Items.Add(new ListItem("Other", "-1"));
        }
        catch (Exception ex) { }
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

    private void FillBayFree(string RefNo)
    {
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        try
        {
            lbBayFree.Text = "";
            SqlCommand cmd = new SqlCommand("udpToBayFree", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ServiceId", RefNo);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                lbBayFree.Text = dt.Rows[0][0].ToString();
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

    private void FillBayConfirmationList(string RefNo)
    {
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        try
        {
            ddlBayConfirmationList.DataSource = null;

            SqlCommand cmd = new SqlCommand("udpBayConfirmationList", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ServiceId", RefNo);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                ddlBayConfirmationList.DataSource = dt;
                ddlBayConfirmationList.DataTextField = "BayName";
                ddlBayConfirmationList.DataValueField = "BayId";
            }
            ddlBayConfirmationList.DataBind();
        }
        catch (Exception ex)
        { }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
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
            if (dt.Rows[0]["RPDTInformed"].ToString() == "1")
            {
                TabPanel7.Enabled = false;
            }
            if (dt.Rows[0]["JCCInformed"].ToString() == "1")
            {
                TabPanel8.Enabled = false;
            }
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["VehicleReady"].ToString().Replace("&nbsp;", "").Trim() == "" || dt.Rows[0]["VehicleReady"].ToString().Trim() == "NULL")  // dt.Rows[0]["VehicleReady"].ToString().Trim() != "" || dt.Rows[0]["VehicleReady"].ToString().Trim() == "NULL"
                {
                    TabPanel6.Enabled = true; //false
                    TabPanel10.Enabled = true;
                    TabPanel1.Enabled = true;
                }
                else
                {

                    TabPanel6.Enabled = false;
                    TabPanel10.Enabled = false;
                    TabPanel1.Enabled = false;
                }

                if (dt.Rows[0][2].ToString().Replace("&nbsp;", "").Trim().ToUpper() == "FALSE" || dt.Rows[0][2].ToString().Replace("&nbsp;", "").Trim() == "" || dt.Rows[0][2].ToString().Trim() == "NULL")
                {
                    TabPanel1.Enabled = true; //false
                    TabPanel10.Enabled = true;

                    if (dt.Rows[0]["VehicleReady"].ToString().Replace("&nbsp;", "").Trim() == "" || dt.Rows[0]["VehicleReady"].ToString().Trim() == "NULL")  // dt.Rows[0]["VehicleReady"].ToString().Trim() != "" || dt.Rows[0]["VehicleReady"].ToString().Trim() == "NULL"
                    {
                        TabPanel6.Enabled = true; //false
                        TabPanel10.Enabled = true;
                        TabPanel1.Enabled = true;
                    }
                    else
                    {
                        TabPanel6.Enabled = false;
                        TabPanel10.Enabled = false;
                        TabPanel1.Enabled = false;
                    }
                }
                else
                {
                    TabPanel1.Enabled = false;
                    TabPanel10.Enabled = false;
                    TabPanel6.Enabled = false;
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

    protected void btnLoad_Click(object sender, EventArgs e)
    {
        try
        {
            if (TxtDate1.Text.Trim() == "" || TxtDate2.Text.Trim() == "" || txtVehicleNumber.Text.Trim() == "" || txtTagNo.Text.Trim() == "")
            {
                lblmsg.Text = "Please enter date range Or VRN/VIN to search";
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
                DataTable Dt = new DataTable();
                Dt = GetDisplayDate(rbType.SelectedValue, txtVehicleNumber.Text, miniTabIndex + 1);
                DataTable HDt = new DataTable();
                HDt = Dt.Copy();
                GridView1.DataSource = HDt;
                GridView1.DataBind();
                grdDisplay.DataSource = Dt;
                grdDisplay.DataBind();
                TxtDate1.Text = "";
                TxtDate2.Text = "";
            }
            else if (TxtDate1.Text.Trim() != "" && TxtDate2.Text.Trim() != "")
            {
                DataTable Dt = new DataTable();
                Dt = GetDisplayDate(rbType.SelectedValue, txtVehicleNumber.Text, miniTabIndex + 1);
                DataTable HDt = new DataTable();
                HDt = Dt.Copy();
                GridView1.DataSource = HDt;
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

    protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DataTable Dt = new DataTable();
            Dt = GetDisplayDate(rbType.SelectedValue, txtVehicleNumber.Text, miniTabIndex + 1);
            DataTable HDt = new DataTable();
            HDt = Dt.Copy();
            GridView1.DataSource = HDt;
            GridView1.DataBind();
            grdDisplay.DataSource = Dt;
            grdDisplay.DataBind();
        }
        catch (Exception ex)
        {
        }
    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {
        Page_Load(null, null);
    }

    private DataTable GetDisplayDate(string Type, string vehiclenumber, int param)
    {
        string whichDay = string.Empty;
        if (rbType.SelectedIndex == 0)
        {
            whichDay = "1"; // Today
        }
        else
        {
            whichDay = "0"; // Previous
        }
        string Aplus = string.Empty;
        //switch (cmbCustType.SelectedValue)
        //{
        //    case "Customer Type":
        //        Aplus = "";
        //        break;

        //    default:
        //        Aplus = cmbCustType.SelectedValue;
        //        break;
        //}
        string ServiceTyp = string.Empty;
        //if (cmbServiceType.SelectedValue == "Service Type")
        //{
        //    ServiceTyp = "";
        //}
        //else
        //{
        //    ServiceTyp = cmbServiceType.SelectedValue;
        //}
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

        string TeamLeadId = string.Empty;
        string SAId = string.Empty;
        //if (cmbTeamLead.SelectedIndex > 0)
        //{
        //    TeamLeadId = cmbTeamLead.SelectedValue.ToString();
        //}
        //else
        //    TeamLeadId = "0";
        string TagNo = string.Empty;
        if (txtTagNo.Text.Trim() == "")
            TagNo = "0";
        else
            TagNo = txtTagNo.Text.Trim();

        if (Session["ROLE"].ToString() == "BodyShop SA")
        {
            SAId = Session["EmpId"].ToString();
        }


        SqlConnection oConn = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlCommand cmd = new SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "JCDisplay_BodyShop";
        cmd.Parameters.AddWithValue("@Day", whichDay);
        cmd.Parameters.AddWithValue("@Bodyshop", 1);
        cmd.Parameters.AddWithValue("@Floor", 0);
        //cmd.Parameters.AddWithValue("@CustomerType", Aplus);
        cmd.Parameters.AddWithValue("@ServiceType", ServiceTyp);
        cmd.Parameters.AddWithValue("@Model", vehicleModel);
        cmd.Parameters.AddWithValue("@Process", ProcessVal);
        cmd.Parameters.AddWithValue("@DateFrom", DateFrom);
        cmd.Parameters.AddWithValue("@DateTo", DateTo);
        cmd.Parameters.AddWithValue("@RegNo", VehicleNo);
        cmd.Parameters.AddWithValue("@Param", "");
        cmd.Parameters.AddWithValue("@SAId", SAId);
       // cmd.Parameters.AddWithValue("@TLId", TeamLeadId);
        cmd.Parameters.AddWithValue("@Tagno", TagNo);
        cmd.Parameters.AddWithValue("@Status", ddlState.SelectedValue.Trim());
        cmd.Parameters.AddWithValue("@indexOn", drpOrderBy.SelectedValue.Trim());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
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

    private void LinkPrevVehicles(string typeLink)
    {
        if (typeLink == "1")
        {
            imgType.HRef = "DisplayWorksI.aspx?typ=1";
        }
        else
        {
            imgType.HRef = "DisplayWorksI.aspx?typ=0";
        }
    }

    protected void srcServiceAdvisor_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {
    }

    protected void grdDisplay_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdDisplay.PageIndex = e.NewPageIndex;
            DataTable Dt = new DataTable();
            Dt = GetDisplayDate(rbType.SelectedValue, txtVehicleNumber.Text, miniTabIndex + 1);
            DataTable HDt = new DataTable();
            HDt = Dt.Copy();
            GridView1.DataSource = HDt;
            GridView1.DataBind();
            grdDisplay.DataSource = Dt;
            grdDisplay.DataBind();
        }
        catch (Exception ex)
        {
        }
    }

    private void OnIButtonClick(object Sender, EventArgs e)
    {
        string VehicleRegNo = string.Empty;
        string ProcessData = string.Empty;
        try
        {
            GridViewRow Gr = (GridViewRow)(Sender as Control).Parent.Parent;
            int Index = Gr.RowIndex;
            VehicleRegNo = Gr.Cells[0].Text;
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

    protected void grdDisplay_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (grdDisplay.DataSource != null || GridView1.DataSource != null)
        {
            string status = "";
            string counter = "";
            string StatusTech = "";
            string RefNo = e.Row.Cells[19].Text.ToString();

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

            //FOR HIDING COLUMNS.
            if (e.Row.RowType != DataControlRowType.Pager)
            {
                // e.Row.Cells[17].Visible = false;
                e.Row.Cells[19].Visible = false;
             //   e.Row.Cells[5].Visible = false;
            }

            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text =  e.Row.Cells[0].Text.ToString();
                e.Row.Cells[1].Text =  e.Row.Cells[1].Text.ToString() ;
                e.Row.Cells[2].Text =  e.Row.Cells[2].Text.ToString() ;
                e.Row.Cells[3].Text =  e.Row.Cells[3].Text.ToString() ;
                e.Row.Cells[4].Text =  e.Row.Cells[4].Text.ToString() ;
                e.Row.Cells[5].Text =  e.Row.Cells[5].Text.ToString() ;
                e.Row.Cells[6].Text =  e.Row.Cells[6].Text.ToString() ;
                e.Row.Cells[7].Text =  e.Row.Cells[7].Text.ToString() ;
                e.Row.Cells[8].Text =  e.Row.Cells[8].Text.ToString();
                e.Row.Cells[9].Text =  e.Row.Cells[9].Text.ToString();
                e.Row.Cells[10].Text =  e.Row.Cells[10].Text.ToString();
                e.Row.Cells[11].Text =  e.Row.Cells[11].Text.ToString() ;
                e.Row.Cells[12].Text = e.Row.Cells[12].Text.ToString();
                e.Row.Cells[13].Text = e.Row.Cells[13].Text.ToString();
                e.Row.Cells[14].Text = e.Row.Cells[14].Text.ToString();
                e.Row.Cells[15].Text = e.Row.Cells[15].Text.ToString();
                e.Row.Cells[16].Text="<div style=Height:25px;width:20px;></div>";
               // e.Row.Cells[15].Text =  e.Row.Cells[15].Text.ToString() ;
                //Row.Cells[16].Text =  e.Row.Cells[16].Text.ToString() ;
                e.Row.Cells[17].Text = e.Row.Cells[17].Text.ToString();
                e.Row.Cells[18].Text = e.Row.Cells[18].Text.ToString();
                e.Row.Cells[19].Text = e.Row.Cells[19].Text.ToString();

            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //SLNO
                e.Row.Cells[0].Text =  e.Row.Cells[0].Text.ToString() ;

                //TAG
                e.Row.Cells[1].Text = e.Row.Cells[1].Text.ToString();

                //REGNO
                if(e.Row.Cells[2].Text.Length>10)
                {
                    int Length = e.Row.Cells[2].Text.Length;
                    e.Row.Cells[2].Text = e.Row.Cells[2].Text.ToString().Substring(Length-10,10);
                }
                else
                e.Row.Cells[2].Text = e.Row.Cells[2].Text.ToString();
                e.Row.Cells[2].Attributes.Add("onmouseover", "showRegNoHover(event,'" + RefNo + "')");
                e.Row.Cells[2].Attributes.Add("onmouseout", "hideTooltip(event)");
                
                //MODEL
                try
                {
                    e.Row.Cells[3].ToolTip = e.Row.Cells[3].Text.ToString();//.Replace("<div style=width:60px;>", "").Replace("</div>", "");
                    if (e.Row.Cells[3].Text.Length > 10)
                        e.Row.Cells[3].Text =  e.Row.Cells[3].Text.ToString().Substring(0, 10) ;
                    else
                        e.Row.Cells[3].Text =  e.Row.Cells[3].Text.ToString() ;
                }
                catch (Exception ex)
                {
                }
                //SType
                try
                {
                    e.Row.Cells[4].Text = e.Row.Cells[4].Text.ToString();
                }
                catch (Exception ex)
                {
                }


                //STAUS - 000-Normal, 100-Ready , 010-Hold , 001-Idle
                try
                {
                    string[] status_str = e.Row.Cells[5].Text.ToString().Split('|');
                    if (status_str[0] == "0-0-0")
                    {
                        e.Row.Cells[5].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205149/jcr/Status.png' Alt=''  width='16' height='16'/>";
                        e.Row.Cells[5].ToolTip = "Work In Progress";
                    }
                    else if (status_str[0] == "1-0-0")
                    {
                        e.Row.Cells[5].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205149/jcr/Status_Ready.png' Alt='' width='16' height='16'/>";
                        e.Row.Cells[5].ToolTip = "Vehicle Ready [" + status_str[1].Trim() + "]";
                    }
                    else if (status_str[0] == "0-1-0")
                    {
                        e.Row.Cells[5].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205149/jcr/Status_HOLD.png' Alt='' width='16' height='16'/>";
                        e.Row.Cells[5].ToolTip = "Hold";
                    }
                    else if (status_str[0] == "0-0-1")
                    {
                        e.Row.Cells[5].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205149/jcr/Status_IDLE.png' Alt='' width='16' height='16'/>";
                        e.Row.Cells[5].ToolTip = "Idle";
                    }
                }
                catch (Exception ex)
                {
                }

                //EstimationPreparation

                if (e.Row.Cells[6].Text.Trim()=="1")
                {
                    e.Row.Cells[6].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN.png' Alt=''  width='16 height='16'/></div>";
                }
                else
                {
                    e.Row.Cells[6].Text = "<div style=width:40px;></div>";
                }

                //InsuranceApproval

                if (e.Row.Cells[7].Text.Trim() == "1")
                {
                    e.Row.Cells[7].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN.png' Alt=''  width='16 height='16'/></div>";
                }
                else
                {
                    e.Row.Cells[7].Text = "<div style=width:40px;></div>";
                }

                //SupplementaryApproval
                if (e.Row.Cells[8].Text.Trim() == "1")
                {
                    e.Row.Cells[8].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN.png' Alt=''  width='16 height='16'/></div>";
                }
                else
                {
                    e.Row.Cells[8].Text = "<div style=width:40px;></div>";
                }


                //dismanting
                try
                {
                    string getDismanting = (e.Row.Cells[9].Text.Trim() == "&nbsp;" ? "" : e.Row.Cells[9].Text.Trim());
                    string[] disParts = { };
                    if (getDismanting.Contains("|"))
                    {
                        disParts = getDismanting.Split('|');
                    }
                    e.Row.Cells[9].Attributes.Add("onmouseover", "showProcessInOut(event,'" + RefNo + "','" + disParts[2] + "','Dismantling')");
                    e.Row.Cells[9].Attributes.Add("onmouseout", "hideTooltip(event)");
                    if (e.Row.Cells[9].Text.ToString().Contains('*'))
                    {
                        status = e.Row.Cells[9].Text.ToString().Substring(0, 1);
                        if (status == "0")
                            if (e.Row.Cells[9].Text.ToString().Contains('$'))
                                e.Row.Cells[9].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/NotRequired.png' Alt=''  width='16' height='16'/></div>";
                            else
                                e.Row.Cells[9].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205142/jcr/dismantle.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "1")
                            e.Row.Cells[9].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205142/jcr/dismantle_WIP.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "2")
                            e.Row.Cells[9].Text = "<div style=width:40px;><img src='images/JCR/dismantle WIP NEAR.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "3")
                            e.Row.Cells[9].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205141/jcr/dismantle_WIP_Delay.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "4")
                            e.Row.Cells[9].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN.png' Alt=''  width='16 height='16'/></div>";
                        else if (status == "5")
                            e.Row.Cells[9].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CR.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "6")
                            e.Row.Cells[9].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/OutSkipped.png' Alt=''  width='16' height='16'/></div>";
                    }
                    else
                    {
                        status = e.Row.Cells[9].Text.ToString().Substring(0, 1);
                        if (status == "0")
                            if (e.Row.Cells[9].Text.ToString().Contains('$'))
                                e.Row.Cells[9].Text = "<div style=width:40px;></div>";
                            else
                                e.Row.Cells[9].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205142/jcr/dismantle.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "1")
                            e.Row.Cells[9].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205142/jcr/dismantle_WIP.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "2")
                            e.Row.Cells[9].Text = "<div style=width:40px;><img src='images/JCR/dismantle WIP NEAR.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "3")
                            e.Row.Cells[9].Text= "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205141/jcr/dismantle_WIP_Delay.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "4")
                            e.Row.Cells[9].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "5")
                            e.Row.Cells[9].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CR.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "6")
                            e.Row.Cells[9].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/OutSkipped.png' Alt=''  width='16' height='16'/></div>";
                    }
                }
                catch (Exception ex)
                { }

                //denting

                try
                {
                    string getDismanting = (e.Row.Cells[10].Text.Trim() == "&nbsp;" ? "" : e.Row.Cells[10].Text.Trim());
                    string[] dentingArr = { };
                    if (getDismanting.Contains("|"))
                    {
                        dentingArr = getDismanting.Split('|');
                    }
                    e.Row.Cells[10].Attributes.Add("onmouseover", "showProcessInOut(event,'" + RefNo + "','" + dentingArr[2] + "','Denting')");
                    e.Row.Cells[10].Attributes.Add("onmouseout", "hideTooltip(event)");
                    if (e.Row.Cells[10].Text.ToString().Contains('*'))
                    {
                        status = e.Row.Cells[10].Text.ToString().Substring(0, 1);
                        if (status == "0")
                            if (e.Row.Cells[10].Text.ToString().Contains('$'))
                                e.Row.Cells[10].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/NotRequired.png' Alt=''  width='16' height='16'/></div>";
                            else
                                e.Row.Cells[10].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205141/jcr/Denting.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "1")
                            e.Row.Cells[10].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205141/jcr/denting_WIP.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "2")
                            e.Row.Cells[10].Text = "<div style=width:40px;><img src='images/JCR/Painting WIP NEAR.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "3")
                            e.Row.Cells[10].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205141/jcr/Denting_WIP_Delay.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "4")
                            e.Row.Cells[10].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN.png' Alt=''  width='16 height='16'/></div>";
                        else if (status == "5")
                            e.Row.Cells[10].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CR.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "6")
                            e.Row.Cells[10].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/OutSkipped.png' Alt=''  width='16' height='16'/></div>";
                    }
                    else
                    {
                        status = e.Row.Cells[10].Text.ToString().Substring(0, 1);
                        if (status == "0")
                            if (e.Row.Cells[10].Text.ToString().Contains('$'))
                                e.Row.Cells[10].Text = "<div style=width:40px;></div>";
                            else
                                e.Row.Cells[10].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205141/jcr/Denting.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "1")
                            e.Row.Cells[10].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205141/jcr/denting_WIP.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "2")
                            e.Row.Cells[10].Text = "<div style=width:40px;><img src='images/JCR/Painting WIP NEAR.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "3")
                            e.Row.Cells[10].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205141/jcr/Denting_WIP_Delay.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "4")
                            e.Row.Cells[10].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "5")
                            e.Row.Cells[10].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CR.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "6")
                            e.Row.Cells[10].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/OutSkipped.png' Alt=''  width='16' height='16'/></div>";
                    }
                }
                catch (Exception ex)
                { }

                //painting
                try
                {
                    string getDismanting = (e.Row.Cells[11].Text.Trim() == "&nbsp;" ? "" : e.Row.Cells[11].Text.Trim());
                    string[] paintingArr = { };
                    if (getDismanting.Contains("|"))
                    {
                        paintingArr = getDismanting.Split('|');
                    }
                    e.Row.Cells[11].Attributes.Add("onmouseover", "showProcessInOut(event,'" + RefNo + "','" + paintingArr[2] + "','Painting')");
                    e.Row.Cells[11].Attributes.Add("onmouseout", "hideTooltip(event)");
                    if (e.Row.Cells[11].Text.ToString().Contains('*'))
                    {
                        status = e.Row.Cells[11].Text.ToString().Substring(0, 1);
                        if (status == "0")
                            if (e.Row.Cells[11].Text.ToString().Contains('$'))
                                e.Row.Cells[11].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/NotRequired.png' Alt=''  width='16' height='16'/></div>";
                            else
                                e.Row.Cells[11].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/Painting.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "1")
                            e.Row.Cells[11].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/Painting_WIP.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "2")
                            e.Row.Cells[11].Text = "<div style=width:40px;><img src='images/JCR/Painting WIP NEAR.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "3")
                            e.Row.Cells[11].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/Painting_WIP_Delay.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "4")
                            e.Row.Cells[11].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN.png' Alt=''  width='16 height='16'/></div>";
                        else if (status == "5")
                            e.Row.Cells[11].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CR.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "6")
                            e.Row.Cells[11].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/OutSkipped.png' Alt=''  width='16' height='16'/></div>";
                    }
                    else
                    {
                        status = e.Row.Cells[11].Text.ToString().Substring(0, 1);
                        if (status == "0")
                            if (e.Row.Cells[11].Text.ToString().Contains('$'))
                                e.Row.Cells[11].Text = "<div style=width:40px;></div>";
                            else
                                e.Row.Cells[11].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/Painting.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "1")
                            e.Row.Cells[11].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/Painting_WIP.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "2")
                            e.Row.Cells[11].Text = "<div style=width:40px;><img src='images/JCR/Painting WIP NEAR.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "3")
                            e.Row.Cells[11].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/Painting_WIP_Delay.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "4")
                            e.Row.Cells[11].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "5")
                            e.Row.Cells[11].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CR.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "6")
                            e.Row.Cells[11].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/OutSkipped.png' Alt=''  width='16' height='16'/></div>";
                    }
                }
                catch (Exception ex)
                { }



                //refitting
                try
                {
                    string getDismanting = (e.Row.Cells[12].Text.Trim() == "&nbsp;" ? "" : e.Row.Cells[12].Text.Trim());
                    string[] REfiArr = { };
                    if (getDismanting.Contains("|"))
                    {
                        REfiArr = getDismanting.Split('|');
                    }
                    e.Row.Cells[12].Attributes.Add("onmouseover", "showProcessInOut(event,'" + RefNo + "','" + REfiArr[2] + "','Refitting')");
                    e.Row.Cells[12].Attributes.Add("onmouseout", "hideTooltip(event)");
                    if (e.Row.Cells[12].Text.ToString().Contains('*'))
                    {
                        status = e.Row.Cells[12].Text.ToString().Substring(0, 1);
                        if (status == "0")
                            if (e.Row.Cells[12].Text.ToString().Contains('$'))
                                e.Row.Cells[12].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/NotRequired.png' Alt=''  width='16' height='16'/></div>";
                            else
                                e.Row.Cells[12].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205146/jcr/Refitting.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "1")
                            e.Row.Cells[12].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205146/jcr/Refitting_WIP.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "2")
                            e.Row.Cells[12].Text = "<div style=width:40px;><img src='images/JCR/Refitting WIP NEAR.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "3")
                            e.Row.Cells[12].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205146/jcr/Refitting_WIP_Delay.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "4")
                            e.Row.Cells[12].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN.png' Alt=''  width='16 height='16'/></div>";
                        else if (status == "5")
                            e.Row.Cells[12].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CR.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "6")
                            e.Row.Cells[12].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/OutSkipped.png' Alt=''  width='16' height='16'/></div>";
                    }
                    else
                    {
                        status = e.Row.Cells[12].Text.ToString().Substring(0, 1);
                        if (status == "0")
                            if (e.Row.Cells[12].Text.ToString().Contains('$'))
                                e.Row.Cells[12].Text = "<div style=width:40px;></div>";
                            else
                                e.Row.Cells[12].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205146/jcr/Refitting.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "1")
                            e.Row.Cells[12].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205146/jcr/Refitting_WIP.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "2")
                            e.Row.Cells[12].Text = "<div style=width:40px;><img src='images/JCR/Refitting WIP NEAR.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "3")
                            e.Row.Cells[12].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205146/jcr/Refitting_WIP_Delay.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "4")
                            e.Row.Cells[12].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "5")
                            e.Row.Cells[12].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CR.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "6")
                            e.Row.Cells[12].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/OutSkipped.png' Alt=''  width='16' height='16'/></div>";
                    }
                }
                catch (Exception ex)
                { }


                //fi

                try
                {
                    string getDismanting = (e.Row.Cells[13].Text.Trim() == "&nbsp;" ? "" : e.Row.Cells[13].Text.Trim());
                    string[] FiArr = { };
                    if (getDismanting.Contains("|"))
                    {
                        FiArr = getDismanting.Split('|');
                    }
                    e.Row.Cells[13].Attributes.Add("onmouseover", "showProcessInOut(event,'" + RefNo + "','" + FiArr[2] + "','Final Inspection')");
                    e.Row.Cells[13].Attributes.Add("onmouseout", "hideTooltip(event)");
                    if (e.Row.Cells[13].Text.ToString().Contains('*'))
                    {
                        status = e.Row.Cells[13].Text.ToString().Substring(0, 1);
                        if (status == "0")
                            if (e.Row.Cells[13].Text.ToString().Contains('$'))
                                e.Row.Cells[13].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/NotRequired.png' Alt=''  width='16' height='16'/></div>";
                            else
                                e.Row.Cells[13].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205146/jcr/QC.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "1")
                            e.Row.Cells[13].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205146/jcr/QC_WIP.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "2")
                            e.Row.Cells[13].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205145/jcr/QC_WIP_Near.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "3")
                            e.Row.Cells[13].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205145/jcr/QC_WIP_Delay.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "4")
                            e.Row.Cells[13].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN.png' Alt=''  width='16 height='16'/></div>";
                        else if (status == "5")
                            e.Row.Cells[13].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CR.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "6")
                            e.Row.Cells[13].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/OutSkipped.png' Alt=''  width='16' height='16'/></div>";
                    }
                    else
                    {
                        status = e.Row.Cells[13].Text.ToString().Substring(0, 1);
                        if (status == "0")
                            if (e.Row.Cells[13].Text.ToString().Contains('$'))
                                e.Row.Cells[13].Text = "<div style=width:40px;></div>";
                            else
                                e.Row.Cells[13].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205146/jcr/QC.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "1")
                            e.Row.Cells[13].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205146/jcr/QC_WIP.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "2")
                            e.Row.Cells[13].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205145/jcr/QC_WIP_Near.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "3")
                            e.Row.Cells[13].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205145/jcr/QC_WIP_Delay.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "4")
                            e.Row.Cells[13].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "5")
                            e.Row.Cells[13].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CR.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "6")
                            e.Row.Cells[13].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/OutSkipped.png' Alt=''  width='16' height='16'/></div>";
                    }
                }
                catch (Exception ex)
                { }

                //REInspection
                try
                {
                    string getDismanting = (e.Row.Cells[14].Text.Trim() == "&nbsp;" ? "" : e.Row.Cells[14].Text.Trim());
                    string[] reinsArr = { };
                    if (getDismanting.Contains("|"))
                    {
                        reinsArr = getDismanting.Split('|');
                    }
                    e.Row.Cells[14].Attributes.Add("onmouseover", "showProcessInOut(event,'" + RefNo + "','" + reinsArr[2] + "','ReInspetion')");
                    e.Row.Cells[14].Attributes.Add("onmouseout", "hideTooltip(event)");
                    if (e.Row.Cells[14].Text.ToString().Contains('*'))
                    {
                        status = e.Row.Cells[14].Text.ToString().Substring(0, 1);
                        if (status == "0")
                            if (e.Row.Cells[14].Text.ToString().Contains('$'))
                                e.Row.Cells[14].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/NotRequired.png' Alt=''  width='16' height='16'/></div>";
                            else
                                e.Row.Cells[14].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205147/jcr/Reinspection.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "1")
                            e.Row.Cells[14].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205147/jcr/Reinspection_WIP.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "2")
                            e.Row.Cells[14].Text = "<div style=width:40px;><img src='images/JCR/Reinspection WIP NEAR.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "3")
                            e.Row.Cells[14].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205146/jcr/Reinspection_WIP_Delay.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "4")
                            e.Row.Cells[14].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN.png' Alt=''  width='16 height='16'/></div>";
                        else if (status == "5")
                            e.Row.Cells[14].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CR.png' Alt=''  width='16' height='16'/></div>";
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
                                e.Row.Cells[14].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205147/jcr/Reinspection.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "1")
                            e.Row.Cells[14].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205147/jcr/Reinspection_WIP.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "2")
                            e.Row.Cells[14].Text = "<div style=width:40px;><img src='images/JCR/WA WIP NEAR.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "3")
                            e.Row.Cells[14].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205146/jcr/Reinspection_WIP_Delay.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "4")
                            e.Row.Cells[14].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "5")
                            e.Row.Cells[14].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CR.png' Alt=''  width='16' height='16'/></div>";
                        else if (status == "6")
                            e.Row.Cells[14].Text = "<div style=width:40px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/OutSkipped.png' Alt=''  width='16' height='16'/></div>";
                    }
                }
                catch (Exception ex)
                { }

                //ROC
                try
                {
                    string[] jcc_str = e.Row.Cells[15].Text.ToString().Trim().Split('|');
                    if (jcc_str[0] == "0-0")
                    {
                        e.Row.Cells[15].Text = "<div style=width:30px;></div>";
                        e.Row.Cells[15].ToolTip = "Not Closed";
                    }
                    else if (jcc_str[0] == "1-0")
                    {
                        e.Row.Cells[15].Text = "<div style=width:30px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205143/jcr/JCC_SKIP.png' Alt=''  width='16' height='16'/></div>";
                        e.Row.Cells[15].ToolTip = "Closed but not informed [" + jcc_str[1].Trim() + "]";
                    }
                    else if (jcc_str[0] == "1-1")
                    {
                        e.Row.Cells[15].Text = "<div style=width:30px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205142/jcr/JCC_ONTIME.png' Alt=''  width='16' height='16'/></div>";
                        e.Row.Cells[15].ToolTip = "Closed and informed [" + jcc_str[1].Trim() + "]";
                    }
                }
                catch (Exception ex)
                { }
                //PTD Status : 0-Not Define, 1-Within Time, 2-Apporching Time, 3-Delayed
                try
                {
                    
                    string getIFB = (e.Row.Cells[16].Text.Trim() == "&nbsp;" ? "" : e.Row.Cells[16].Text.Trim());
                    if (getIFB == "1")
                        e.Row.Cells[16].ToolTip = "On Time";
                    else if (getIFB == "0")
                        e.Row.Cells[16].ToolTip = "No PDT";
                    else if (getIFB == "2")
                        e.Row.Cells[16].ToolTip = "Approaching";
                    else
                        e.Row.Cells[16].ToolTip = "Delayed";

                    if (e.Row.Cells[16].Text.ToString() == "0")
                        e.Row.Cells[16].Text = "<div style=width:20px;></div>";
                    else if (e.Row.Cells[16].Text.ToString() == "1")
                        e.Row.Cells[16].Text = "<div style=width:20px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/circle_green_Small.png' Alt=''  width='14' height='14'/></div>";
                    else if (e.Row.Cells[16].Text.ToString() == "2")
                        e.Row.Cells[16].Text = "<div style=width:20px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205139/jcr/circle_yellow_small.png' Alt=''  width='14' height='14'/></div>";
                    else if (e.Row.Cells[16].Text.ToString() == "3")
                        e.Row.Cells[16].Text = "<div style=width:20px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/circle_red_small.png' Alt=''  width='14' height='14'/></div>";
                }
                catch (Exception ex)
                { }

                try
                {
                    if (e.Row.Cells[17].Text.ToString().Contains('#'))
                    {
                        if (e.Row.Cells[17].Text.ToString().Contains('Y'))
                        {
                            e.Row.Cells[17].Text = "<table border=0 cellspacing=0 cellpadding=0 style=width:75px;color:BLUE;><tr><td>" + e.Row.Cells[17].Text.Replace('#', ' ').Replace('Y', ' ').ToString().Trim() + "</td></tr></table>";
                        }
                        else
                        {
                            e.Row.Cells[17].Text = "<table border=0 cellspacing=0 cellpadding=0 style=width:75px;color:RED;><tr><td>" + e.Row.Cells[17].Text.Replace('#', ' ').Replace('N', ' ').ToString().Trim() + "</td></tr></table>";
                        }
                    }
                    else
                    {
                        e.Row.Cells[17].Text = "<table border=0 cellspacing=0 cellpadding=0 style=width:75px;color:GRAY;><tr><td>" + e.Row.Cells[17].Text.Replace('$', ' ').ToString().Trim() + "</td></tr></table>";
                    }
                }
                catch (Exception ex)
                { }
                //prg
                //e.Row.Cells[11].Text = e.Row.Cells[11].Text.ToString();

                //pdt
                e.Row.Cells[19].Text = e.Row.Cells[19].Text.ToString();
               // e.Row.Cells[13].Text = e.Row.Cells[13].Text.ToString();
                e.Row.Cells[18].Text = e.Row.Cells[18].Text.ToString();
              //  e.Row.Cells[19].Text = e.Row.Cells[19].Text.ToString();
               // e.Row.Cells[20].Text = e.Row.Cells[20].Text.ToString();
            
               
            }
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
                if (txtRevPDT.Text.Trim() != "" && cmbHH.Text.Trim() != "")
                {
                    string tim = txtRevPDT.Text + " " + DateTime.Parse(cmbHH.Text).ToShortTimeString();
                    DateTime dat = new DateTime();
                    if (DateTime.TryParse(tim, out dat))
                    {
                        if (DateTime.Compare(DateTime.Parse(tim), DateTime.Now) < 0)
                        {
                            lblmsg.CssClass = "ErrMsg";
                            lblmsg.Text = "Date and Time must be <br> greater than or todays date";
                            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");


                        }
                        else if (txtPDTComment.Text.Trim() != "")
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
                                //if (ddPDTRemarks.SelectedItem.Text.ToString().Trim() == "Other")
                                //{
                                //    cmd.Parameters.AddWithValue("@Comment", txtPDTComment.Text.Trim());
                                //}
                                //else
                                //{
                                cmd.Parameters.AddWithValue("@Comment", ddPDTRemarks.SelectedItem.Text.ToString().Trim());
                                //}

                                cmd.Parameters.AddWithValue("@RPDTInformed", custInfo);
                                cmd.Parameters.AddWithValue("@UserName", username);
                                SqlParameter spm = new SqlParameter("@msg", SqlDbType.VarChar, 50);
                                spm.Direction = ParameterDirection.Output;
                                spm.Value = "";
                                cmd.Parameters.Add(spm);
                                cmd.CommandType = CommandType.StoredProcedure;
                                oConn.Open();
                                cmd.ExecuteNonQuery();
                                if (spm.Value.ToString() == "Revised PDT Saved Successfully.. !")
                                {
                                    lblmsg.Text = spm.Value.ToString();
                                    lblmsg.CssClass = "ScsMsg";
                                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                                    oConn.Close();
                                    txtRevPDT.Text = DateTime.Now.ToShortDateString();
                                    BindGrid();
                                    txtRevPDT.Text = "";
                                    txtPDTComment.Text = "";
                                    rd_No.Checked = false;
                                    rd_Yes.Checked = false;
                                    cmbHH.Text = "";
                                }
                                else
                                {
                                    lblmsg.Text = spm.Value.ToString();
                                    lblmsg.CssClass = "ErrMsg";
                                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

                                }

                            }
                            else
                            {
                                lblmsg.Text = "Please Select Customer Informed Yes or No";
                                lblmsg.CssClass = "ErrMsg";
                                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                            }
                        }
                        else
                        {
                            lblmsg.Text = "Please Enter RPDT Reason";
                            lblmsg.CssClass = "ErrMsg";
                            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                        }
                    }
                    else
                    {
                        lblmsg.Text = "Invalid Date Provided.";
                        lblmsg.CssClass = "ErrMsg";
                        ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                    }

                }
                else
                {
                    lblmsg.Text = "Please Enter Revised PDT and Time";
                    lblmsg.CssClass = "ErrMsg";
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                }
             }
                
               
            else
            {
                lblmsg.Text = "VRN/VIN Not Provided!";
                lblmsg.CssClass = "ErrMsg";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
            }
        }
        catch (Exception ex)
        {
        }
    }

    private void virtualGrid(GridViewRowEventArgs e)
    {
        GrdHead.Controls.Clear();
        HtmlTable tbl = new HtmlTable();
        HtmlTableRow rw = new HtmlTableRow();
        HtmlTableCell tc = new HtmlTableCell();
        for (int i = 0; i < e.Row.Cells.Count - 3; i++)
        {
            tc = new HtmlTableCell();
            tc.InnerText = e.Row.Cells[i].Text;
            tc.Width = e.Row.Cells[i].Width.ToString();
            tc.BgColor = "silver";
            tc.Attributes.Add("style", "font-style:arial;font-size:small;font-weight:bold;text-align:center;");
            rw.Cells.Add(tc);
        }
        tbl.Rows.Add(rw);
        tbl.Visible = true;
        tbl.Attributes.Add("style", "width:100%");
        GrdHead.Controls.Add(tbl);
    }

    public static DataTable GetInOutTime(string serviceID, string ProcessName)
    {
        DataTable dt = new DataTable();
        SqlConnection con = new SqlConnection(System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["TMLDealercode"] + "-TMLConString"].ToString());
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

  
    private static DataTable GetSubProcessHover(string RefNo, string SubProcessId)
    {
        DataTable dt = new DataTable();
        SqlConnection con = new SqlConnection(System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["TMLDealercode"] + "-TMLConString"].ToString());
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

    protected void btnCloseJCC_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            int informed = 2;
            string BAmt = "0";
            if (lblJCClosing.Text != "")
                BAmt = lblJCClosing.Text.Trim();
            if (RadioCustomerInformed.SelectedIndex != -1)
            {
                informed = Convert.ToInt32(RadioCustomerInformed.SelectedValue);
            }
            //SqlConnection con = new SqlConnection();
            SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
            SqlCommand cmd = new SqlCommand("JobCardClosing", con);
            cmd.Parameters.AddWithValue("@JCOT", DateTime.Now.ToString());
            cmd.Parameters.AddWithValue("@RegNo", BAmt.ToUpper());//lblSelSlNo.Text.Trim()
            cmd.Parameters.AddWithValue("@Informed", informed.ToString());
            cmd.Parameters.AddWithValue("@JCCInfoBy", Session["EmpId"].ToString());
            SqlParameter spm = new SqlParameter("@msg", SqlDbType.VarChar, 50);
            spm.Direction = ParameterDirection.Output;
            spm.Value = "";
            cmd.Parameters.Add(spm);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                con.Open();
            }
            catch { }
            cmd.ExecuteNonQuery();
            BindGrid();
            RadioCustomerInformed.SelectedIndex = -1;
            lblmsg.Text = spm.Value.ToString();
            if (lblmsg.Text.Contains("Successfully"))
            {
                lblmsg.CssClass = "ScsMsg";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

            }
            else
            {
                lblmsg.CssClass = "ErrMsg";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

            }
        }
    }

    private static DataTable GetEmployeeHover(string Slno, string EmpId)
    {
        DataTable dt = new DataTable();
        SqlConnection con = new SqlConnection(System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["TMLDealercode"] + "-TMLConString"].ToString());
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

    private static DataTable GetJAHover(string RefNo)
    {
        DataTable dt = new DataTable();
        SqlConnection con = new SqlConnection(System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["TMLDealercode"] + "-TMLConString"].ToString());
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
        SqlConnection con = new SqlConnection(System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["TMLDealercode"] + "-TMLConString"].ToString());
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

    private static DataTable GetRemarksHover(string RefNo)
    {
        DataTable dt = new DataTable();
        SqlConnection con = new SqlConnection(System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["TMLDealercode"] + "-TMLConString"].ToString());
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

    private void getColors(string colorCode1, string colorCode2, int ColNo, GridViewRowEventArgs e)
    {
        switch (colorCode1)
        {
            case "0":
                e.Row.Cells[ColNo].Attributes.Add("Style", "font-size: large;");
                break;

            case "1":
                if (Convert.ToInt32(colorCode2) > 1)
                {
                    if (ColNo == 16) // FIN
                        e.Row.Cells[ColNo].Attributes.Add("Style", "color:#387C44;background-image: url('img/Process/PK4.png'); background-repeat: no-repeat;background-size:24px 24px;");
                    else if (ColNo == 8 || (ColNo > 9 && ColNo < 15))
                        e.Row.Cells[ColNo].Attributes.Add("Style", "color:#387C44;background-image: url('img/Process/PK.png'); background-repeat: no-repeat;background-size:24px 24px;");
                    else if (ColNo >= 19 && ColNo <= 22)
                    {
                        if (Convert.ToInt32(colorCode2) == 2)
                            e.Row.Cells[ColNo].Attributes.Add("Style", "color:#387C44;background-image: url('img/Process/PK_2.png'); background-repeat: no-repeat;background-size:24px 24px;");
                        else if (Convert.ToInt32(colorCode2) == 3)
                            e.Row.Cells[ColNo].Attributes.Add("Style", "color:#387C44;background-image: url('img/Process/PK_3.png'); background-repeat: no-repeat;background-size:24px 24px;");
                        else if (Convert.ToInt32(colorCode2) == 4)
                            e.Row.Cells[ColNo].Attributes.Add("Style", "color:#387C44;background-image: url('img/Process/PK_4.png'); background-repeat: no-repeat;background-size:24px 24px;");
                        else if (Convert.ToInt32(colorCode2) == 5)
                            e.Row.Cells[ColNo].Attributes.Add("Style", "color:#387C44;background-image: url('img/Process/PK_5.png'); background-repeat: no-repeat;background-size:24px 24px;");
                    }
                    else
                    {
                        if (Convert.ToInt32(colorCode2) == 2)
                            e.Row.Cells[ColNo].Attributes.Add("Style", "color:#387C44;background-image: url('img/Process/PK_2.png'); background-repeat: no-repeat;background-size:24px 24px;");
                        else if (Convert.ToInt32(colorCode2) == 3)
                            e.Row.Cells[ColNo].Attributes.Add("Style", "color:#387C44;background-image: url('img/Process/PK_3.png'); background-repeat: no-repeat;background-size:24px 24px;");
                        else if (Convert.ToInt32(colorCode2) == 4)
                            e.Row.Cells[ColNo].Attributes.Add("Style", "color:#387C44;background-image: url('img/Process/PK_4.png'); background-repeat: no-repeat;background-size:24px 24px;");
                        else if (Convert.ToInt32(colorCode2) == 5)
                            e.Row.Cells[ColNo].Attributes.Add("Style", "color:#387C44;background-image: url('img/Process/PK_5.png'); background-repeat: no-repeat;background-size:24px 24px;");
                    }
                }
                else
                    e.Row.Cells[ColNo].Attributes.Add("Style", "color:#387C44;background-image: url('img/Process/PK.png'); background-repeat: no-repeat;background-size:24px 24px;");
                break;

            case "2": // WORK IN PROGRESS NEAR COMPLETION
                if (Convert.ToInt32(colorCode2) > 1)
                {
                    if (ColNo == 16) // FIN
                        e.Row.Cells[ColNo].Attributes.Add("Style", "color:#387C44;background-image: url('img/Process/PK5.png'); background-repeat: no-repeat;background-size:24px 24px;");
                    else if (ColNo == 8 || (ColNo > 9 && ColNo < 15))
                        e.Row.Cells[ColNo].Attributes.Add("Style", "color:#387C44;background-image: url('img/Process/PK1.png'); background-repeat: no-repeat;background-size:24px 24px;");
                    else if (ColNo >= 19 && ColNo <= 22)
                    {
                        if (Convert.ToInt32(colorCode2) == 2)
                            e.Row.Cells[ColNo].Attributes.Add("Style", "color:#387C44;background-image: url('img/Process/PK1_2.png'); background-repeat: no-repeat;background-size:24px 24px;");
                        else if (Convert.ToInt32(colorCode2) == 3)
                            e.Row.Cells[ColNo].Attributes.Add("Style", "color:#387C44;background-image: url('img/Process/PK1_3.png'); background-repeat: no-repeat;background-size:24px 24px;");
                        else if (Convert.ToInt32(colorCode2) == 4)
                            e.Row.Cells[ColNo].Attributes.Add("Style", "color:#387C44;background-image: url('img/Process/PK1_4.png'); background-repeat: no-repeat;background-size:24px 24px;");
                        else if (Convert.ToInt32(colorCode2) == 5)
                            e.Row.Cells[ColNo].Attributes.Add("Style", "color:#387C44;background-image: url('img/Process/PK1_5.png'); background-repeat: no-repeat;background-size:24px 24px;");
                    }
                    else
                    {
                        if (Convert.ToInt32(colorCode2) == 2)
                            e.Row.Cells[ColNo].Attributes.Add("Style", "color:#387C44;background-image: url('img/Process/PK1_2.png'); background-repeat: no-repeat;background-size:24px 24px;");
                        else if (Convert.ToInt32(colorCode2) == 3)
                            e.Row.Cells[ColNo].Attributes.Add("Style", "color:#387C44;background-image: url('img/Process/PK1_3.png'); background-repeat: no-repeat;background-size:24px 24px;");
                        else if (Convert.ToInt32(colorCode2) == 4)
                            e.Row.Cells[ColNo].Attributes.Add("Style", "color:#387C44;background-image: url('img/Process/PK1_4.png'); background-repeat: no-repeat;background-size:24px 24px;");
                        else if (Convert.ToInt32(colorCode2) == 5)
                            e.Row.Cells[ColNo].Attributes.Add("Style", "color:#387C44;background-image: url('img/Process/PK1_5.png'); background-repeat: no-repeat;background-size:24px 24px;");
                    }
                }
                else
                    e.Row.Cells[ColNo].Attributes.Add("Style", "color:#387C44;background-image: url('img/Process/PK1.png'); background-repeat: no-repeat;background-size:24px 24px;");
                break;

            case "3": // WORK IN PROGRESS BUT DELAYED
                if (Convert.ToInt32(colorCode2) > 1)
                {
                    if (ColNo == 16) // FIN
                        e.Row.Cells[ColNo].Attributes.Add("Style", "color:#387C44;background-image: url('img/Process/PK6.png'); background-repeat: no-repeat;background-size:24px 24px;");
                    else if (ColNo == 8 || (ColNo > 9 && ColNo < 15))
                        e.Row.Cells[ColNo].Attributes.Add("Style", "color:#387C44;background-image: url('img/Process/PK3.png'); background-repeat: no-repeat;background-size:24px 24px;");
                    else if (ColNo >= 19 && ColNo <= 22)
                    {
                        if (Convert.ToInt32(colorCode2) == 2)
                            e.Row.Cells[ColNo].Attributes.Add("Style", "color:#387C44;background-image: url('img/Process/PK3_2.png'); background-repeat: no-repeat;background-size:24px 24px;");
                        else if (Convert.ToInt32(colorCode2) == 3)
                            e.Row.Cells[ColNo].Attributes.Add("Style", "color:#387C44;background-image: url('img/Process/PK3_3.png'); background-repeat: no-repeat;background-size:24px 24px;");
                        else if (Convert.ToInt32(colorCode2) == 4)
                            e.Row.Cells[ColNo].Attributes.Add("Style", "color:#387C44;background-image: url('img/Process/PK3_4.png'); background-repeat: no-repeat;background-size:24px 24px;");
                        else if (Convert.ToInt32(colorCode2) == 5)
                            e.Row.Cells[ColNo].Attributes.Add("Style", "color:#387C44;background-image: url('img/Process/PK3_5.png'); background-repeat: no-repeat;background-size:24px 24px;");
                    }
                    else
                    {
                        if (Convert.ToInt32(colorCode2) == 2)
                            e.Row.Cells[ColNo].Attributes.Add("Style", "color:#387C44;background-image: url('img/Process/PK3_2.png'); background-repeat: no-repeat;background-size:24px 24px;");
                        else if (Convert.ToInt32(colorCode2) == 3)
                            e.Row.Cells[ColNo].Attributes.Add("Style", "color:#387C44;background-image: url('img/Process/PK3_3.png'); background-repeat: no-repeat;background-size:24px 24px;");
                        else if (Convert.ToInt32(colorCode2) == 4)
                            e.Row.Cells[ColNo].Attributes.Add("Style", "color:#387C44;background-image: url('img/Process/PK3_4.png'); background-repeat: no-repeat;background-size:24px 24px;");
                        else if (Convert.ToInt32(colorCode2) == 5)
                            e.Row.Cells[ColNo].Attributes.Add("Style", "color:#387C44;background-image: url('img/Process/PK3_5.png'); background-repeat: no-repeat;background-size:24px 24px;");
                    }
                }
                else
                    e.Row.Cells[ColNo].Attributes.Add("Style", "color:#387C44;background-image: url('img/Process/PK3.png'); background-repeat: no-repeat;background-size:24px 24px;");
                break;

            case "4": // WORK COMPLETED WITHIN TIME
                if (Convert.ToInt32(colorCode2) > 1)
                {
                    if (ColNo == 16) // FIN
                        e.Row.Cells[ColNo].Attributes.Add("Style", "color:#387C44;background-image: url('img/Process/CNR.png'); background-repeat: no-repeat;background-size:24px 24px;");
                    else if (ColNo == 8 || (ColNo > 9 && ColNo < 15)) // JCP
                        e.Row.Cells[ColNo].Attributes.Add("Style", "color:#387C44;background-image: url('img/Process/CN.png'); background-repeat: no-repeat;background-size:24px 24px;");//GREEN
                    else if (ColNo >= 19 && ColNo <= 22)
                    {
                        if (Convert.ToInt32(colorCode2) == 2)
                            e.Row.Cells[ColNo].Attributes.Add("Style", "color:#387C44;background-image: url('img/Process/CN_2.png'); background-repeat: no-repeat;background-size:24px 24px;");
                        else if (Convert.ToInt32(colorCode2) == 3)
                            e.Row.Cells[ColNo].Attributes.Add("Style", "color:#387C44;background-image: url('img/Process/CN_3.png'); background-repeat: no-repeat;background-size:24px 24px;");
                        else if (Convert.ToInt32(colorCode2) == 4)
                            e.Row.Cells[ColNo].Attributes.Add("Style", "color:#387C44;background-image: url('img/Process/CN_4.png'); background-repeat: no-repeat;background-size:24px 24px;");
                        else if (Convert.ToInt32(colorCode2) == 5)
                            e.Row.Cells[ColNo].Attributes.Add("Style", "color:#387C44;background-image: url('img/Process/CN_5.png'); background-repeat: no-repeat;background-size:24px 24px;");
                    }
                    else
                    {
                        if (Convert.ToInt32(colorCode2) == 2)
                            e.Row.Cells[ColNo].Attributes.Add("Style", "color:#387C44;background-image: url('img/Process/CN_2.png'); background-repeat: no-repeat;background-size:24px 24px;");
                        else if (Convert.ToInt32(colorCode2) == 3)
                            e.Row.Cells[ColNo].Attributes.Add("Style", "color:#387C44;background-image: url('img/Process/CN_3.png'); background-repeat: no-repeat;background-size:24px 24px;");
                        else if (Convert.ToInt32(colorCode2) == 4)
                            e.Row.Cells[ColNo].Attributes.Add("Style", "color:#387C44;background-image: url('img/Process/CN_4.png'); background-repeat: no-repeat;background-size:24px 24px;");
                        else if (Convert.ToInt32(colorCode2) == 5)
                            e.Row.Cells[ColNo].Attributes.Add("Style", "color:#387C44;background-image: url('img/Process/CN_5.png'); background-repeat: no-repeat;background-size:24px 24px;");
                    }
                }
                else
                    e.Row.Cells[ColNo].Attributes.Add("Style", "color:#387C44;background-image: url('img/Process/CN.png'); background-repeat: no-repeat;background-size:24px 24px;");//GREEN
                break;

            case "5":   //WORK COMPLETED BUT DELAYED
                if (Convert.ToInt32(colorCode2) > 1)
                {
                    if (ColNo == 16) // FIN
                        e.Row.Cells[ColNo].Attributes.Add("Style", "color:#387C44;background-image: url('img/Process/CRR.png'); background-repeat: no-repeat;background-size:24px 24px;");
                    else if (ColNo == 8 || (ColNo > 9 && ColNo < 15)) // JCP
                        e.Row.Cells[ColNo].Attributes.Add("Style", "color:#387C44;background-image: url('img/Process/CR.png'); background-repeat: no-repeat;background-size:24px 24px;");//GREEN
                    else if (ColNo >= 19 && ColNo <= 22)
                    {
                        if (Convert.ToInt32(colorCode2) == 2)
                            e.Row.Cells[ColNo].Attributes.Add("Style", "color:#387C44;background-image: url('img/Process/CR_2.png'); background-repeat: no-repeat;background-size:24px 24px;");
                        else if (Convert.ToInt32(colorCode2) == 3)
                            e.Row.Cells[ColNo].Attributes.Add("Style", "color:#387C44;background-image: url('img/Process/CR_3.png'); background-repeat: no-repeat;background-size:24px 24px;");
                        else if (Convert.ToInt32(colorCode2) == 4)
                            e.Row.Cells[ColNo].Attributes.Add("Style", "color:#387C44;background-image: url('img/Process/CR_4.png'); background-repeat: no-repeat;background-size:24px 24px;");
                        else if (Convert.ToInt32(colorCode2) == 5)
                            e.Row.Cells[ColNo].Attributes.Add("Style", "color:#387C44;background-image: url('img/Process/CR_5.png'); background-repeat: no-repeat;background-size:24px 24px;");
                    }
                    else
                    {
                        if (Convert.ToInt32(colorCode2) == 2)
                            e.Row.Cells[ColNo].Attributes.Add("Style", "color:#387C44;background-image: url('img/Process/CR_2.png'); background-repeat: no-repeat;background-size:24px 24px;");
                        else if (Convert.ToInt32(colorCode2) == 3)
                            e.Row.Cells[ColNo].Attributes.Add("Style", "color:#387C44;background-image: url('img/Process/CR_3.png'); background-repeat: no-repeat;background-size:24px 24px;");
                        else if (Convert.ToInt32(colorCode2) == 4)
                            e.Row.Cells[ColNo].Attributes.Add("Style", "color:#387C44;background-image: url('img/Process/CR_4.png'); background-repeat: no-repeat;background-size:24px 24px;");
                        else if (Convert.ToInt32(colorCode2) == 5)
                            e.Row.Cells[ColNo].Attributes.Add("Style", "color:#387C44;background-image: url('img/Process/CR_5.png'); background-repeat: no-repeat;background-size:24px 24px;");
                    }
                }
                else
                    e.Row.Cells[ColNo].Attributes.Add("Style", "color:#387C44;background-image: url('img/Process/CR.png'); background-repeat: no-repeat;background-size:24px 24px;");//GREEN
                break;

            case "6": // PROCESS OUT SKIPPED
                e.Row.Cells[ColNo].Attributes.Add("Style", "color:#387C44;background-image: url('img/Process/PS3.png'); background-repeat: no-repeat;background-size:24px 24px;");
                break;

            case "7": //PROCESS SKIPPED
                e.Row.Cells[ColNo].Attributes.Add("Style", "color:#387C44;background-image: url('img/Process/process_skiped.png'); background-repeat: no-repeat;background-size:24px 24px;");
                break;

            case "8": // STARTED NOT IN SCHEDULED TIME

                break;
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
        finally
        {
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
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

    protected void BtnClosePopup_Click(object sender, EventArgs e)
    {
        Timer1.Enabled = true;
    }

    private void OnRemarksButtonClick(object Sender, EventArgs e)
    {
        string Slno = string.Empty;
        string ProcessData = string.Empty;
        try
        {
            GridViewRow Gr = (GridViewRow)(Sender as Control).Parent.Parent;
            Slno = Gr.Cells[0].Text;
            Timer1.Enabled = false;
        }
        catch (Exception ex)
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

    private static DataTable DisplayDt;

    protected void BindGrid()
    {
        DisplayDt = new DataTable();
        DataTable HDt = new DataTable();
        DisplayDt.Clear();
        DisplayDt = GetDisplayDate(rbType.SelectedValue, txtVehicleNumber.Text, miniTabIndex + 1);
        HDt = DisplayDt;
        if (DisplayDt.Rows.Count == 0)
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
            grdDisplay.DataSource = DisplayDt;
            grdDisplay.DataBind();
        }

        fblank = 0;
        for (int fbt = 0; fbt < HDt.Rows.Count; fbt++)
        {
            if (HDt.Rows[fbt][5].ToString().Trim() != "")
            {
                fblank = 1;
            }
        }
        FillVehicleStatus();
        lbVCount.Text = DisplayDt.Rows.Count.ToString();
    }

    protected void btnready_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        lblmsg.CssClass = "reset";
        try
        {
            if (lblvehicleno.Text == "")
            {
                lblmsg.Text = "VRN/VIN is not provided";
                lblmsg.CssClass = "ErrMsg";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
            }
            else if (txtRemarksReady.Text == "")
            {
                lblmsg.Text = "Enter Remarks";
                lblmsg.CssClass = "ErrMsg";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
            }
            else
            {

                string sConnString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
                SqlConnection oConn = new SqlConnection(sConnString);
                SqlCommand cmd = new SqlCommand("UpdateTblMasterVehicleReady", oConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@VehicleReady", DateTime.Now);
                cmd.Parameters.AddWithValue("@ReadyRemarks", txtRemarksReady.Text.Trim());
                cmd.Parameters.AddWithValue("@RegNo", lblvehicleno.Text.Trim().Replace("<font size='4' color='red'> *</font>", ""));
                oConn.Open();
                cmd.ExecuteNonQuery();
                lblmsg.Text = "Vehicle Ready Done";
                lblmsg.CssClass = "ScsMsg";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                txtRemarksReady.Text = "";
                // lblmsg.ForeColor = Color.Green;
                oConn.Close();
                BindGrid();

            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void btnOut_Click(object sender, EventArgs e)
    {
        try
        {
            if (lblvovehicleno.Text != "")
            {
                if ( txt_VORemarks.Text.Trim() == "")
                {
                    lblmsg.Text = "Please Add Remarks.";
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
                    //if (ddVOutRemarks.SelectedItem.Text.ToString().Trim() != "Other")
                    //{
                    //    cmd.Parameters.AddWithValue("@VOoutRemarks", ddVOutRemarks.SelectedItem.Text.ToString().Trim());
                    //}
                    //else
                    //{
                        cmd.Parameters.AddWithValue("@VOoutRemarks", txt_VORemarks.Text.ToString().Trim());
                    //}
                    if (oConn.State != ConnectionState.Open)
                        oConn.Open();
                    cmd.ExecuteNonQuery();
                    if (oConn.State != ConnectionState.Closed)
                        oConn.Close();
                    lblmsg.Text = "Vehicle Out Successfully";
                    lblmsg.CssClass="ScsMsg";
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                    lblvovehicleno.Text = "";
                    txt_VORemarks.Text = "";
                    //txt_VORemarks.Visible = false;
                    BindGrid();
                    btnecncl_Click(null, null);
                }
                else
                {
                    lblmsg.Text = "VRN/VIN. Not Found";
                    lblmsg.CssClass = "ErrMsg";
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                }
            }
            else
            {
                lblmsg.Text = "VRN/VIN is not provided";
                lblmsg.CssClass = "ErrMsg";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void btnCancelation_Click(object sender, EventArgs e)
    {
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

    protected void btnecncl_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        lblmsg.CssClass = "reset";
        TabContainer1.Visible = false;
    }

    //protected void btnPDTsave_Click(object sender, EventArgs e)
    //{
        //lblmsg.Text = "";
        //try
        //{
        //    if (lblpdtvehno.Text.Trim() != "")
        //    {
        //        string tim = txtRevPDT.Text + " " + DateTime.Parse(cmbHH.Text).ToShortTimeString();
        //        DateTime dat = new DateTime();
        //        if (DateTime.TryParse(tim, out dat))
        //        {
        //            if (DateTime.Compare(DateTime.Parse(tim), DateTime.Now) < 0)
        //            {
        //                lblmsg.Text = "Date and Time must be <br> greater than or todays date";
        //                lblmsg.ForeColor = Color.Red;
        //            }
        //            else
        //            {
        //                if (txtRevPDT.Text.Trim() != "" && cmbHH.Text.Trim() != "")
        //                {
        //                    if (txtPDTComment.Text.Trim() != "" || ddPDTRemarks.SelectedValue.Trim() != "0")
        //                    {
        //                        if (rd_Yes.Checked != false || rd_No.Checked != false)
        //                        {
        //                            int custInfo = 0;
        //                            string username = "";
        //                            if (rd_Yes.Checked == true)
        //                            {
        //                                custInfo = 1;
        //                                username = Session["UserId"].ToString();
        //                            }

        //                            DateTime pt = Convert.ToDateTime(txtRevPDT.Text.Trim() + " " + cmbHH.Text.Trim());
        //                            string sConnString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        //                            SqlConnection oConn = new SqlConnection(sConnString);
        //                            SqlCommand cmd = new SqlCommand("UpdateTblMasterRPDT", oConn);
        //                            cmd.Parameters.AddWithValue("@RevisedPromisedTime", pt);
        //                            cmd.Parameters.AddWithValue("@RegNo", lblpdtvehno.Text.Trim().Replace("<font size='4' color='red'> *</font>", ""));
        //                            if (ddPDTRemarks.SelectedItem.Text.ToString().Trim() == "Other")
        //                            {
        //                                cmd.Parameters.AddWithValue("@Comment", txtPDTComment.Text.Trim());
        //                            }
        //                            else
        //                            {
        //                                cmd.Parameters.AddWithValue("@Comment", ddPDTRemarks.SelectedItem.Text.ToString().Trim());
        //                            }

        //                            cmd.Parameters.AddWithValue("@RPDTInformed", custInfo);
        //                            cmd.Parameters.AddWithValue("@UserName", username);
        //                            SqlParameter spm = new SqlParameter("@msg", SqlDbType.VarChar, 50);
        //                            spm.Direction = ParameterDirection.Output;
        //                            spm.Value = "";
        //                            cmd.Parameters.Add(spm);
        //                            cmd.CommandType = CommandType.StoredProcedure;
        //                            oConn.Open();
        //                            cmd.ExecuteNonQuery();
        //                            if (spm.Value.ToString() == "Revised PDT Saved Successfully.. !")
        //                            {
        //                                lblmsg.Text = spm.Value.ToString();
        //                                lblmsg.ForeColor = Color.Green;
        //                                oConn.Close();
        //                                txtRevPDT.Text = DateTime.Now.ToShortDateString();
        //                                BindGrid();
        //                                txtPDTComment.Text = "";
        //                                rd_No.Checked = false;
        //                                rd_Yes.Checked = false;
        //                                cmbHH.Text = "";
        //                            }
        //                            else
        //                            {
        //                                lblmsg.Text = spm.Value.ToString();
        //                                lblmsg.ForeColor = Color.Red;

        //                            }

        //                        }
        //                        else
        //                        {
        //                            lblmsg.Text = "Please Select Customer Informed Yes or No";
        //                            lblmsg.ForeColor = Color.Red;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        lblmsg.Text = "Please Enter RPDT Reason";
        //                        lblmsg.ForeColor = Color.Red;
        //                    }
        //                }
        //                else
        //                {
        //                    lblmsg.Text = "Please Enter Revised PDT and Time";
        //                    lblmsg.ForeColor = Color.Red;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            lblmsg.Text = "Invalid Date Provided.";
        //            lblmsg.ForeColor = Color.Red;
        //        }
        //    }
        //    else
        //    {
        //        lblmsg.Text = "VRN/VIN Not Provided!";
        //        lblmsg.ForeColor = Color.Red;
        //    }
        //}
        //catch (Exception ex)
        //{
        //}
    //}

    protected void drpsptype_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (drpsptype.SelectedIndex == 1)
        //{
        //    lblspprocess.Visible = true;
        //    drpspprocess.Visible = true;
        //    lblServiceAction.Visible = false;
        //    lblServiceRecom.Visible = false;
        //    txtServiceAction.Visible = false;
        //    txtRecomendation.Visible = false;
        //    FillRemarksTemplate(7, ref ddlSRemarks);
        //}
        //else
        //{
        //    lblspprocess.Visible = false;
        //    drpspprocess.Visible = false;
        //    lblServiceAction.Visible = true;
        //    lblServiceRecom.Visible = true;
        //    txtServiceAction.Visible = true;
        //    txtRecomendation.Visible = true;
        //    FillRemarksTemplate(6, ref ddlSRemarks);
        //}
    }

    protected void ddlSRemarks_SelectedIndexChanged(object sender, EventArgs e)
    {
    //    if (ddlSRemarks.SelectedValue == "-1")
    //    {
    //        lbl_other.Visible = true;
    //        txtspremarks.Visible = true;
    //    }
    //    else
    //    {
    //        lbl_other.Visible = false;
    //        txtspremarks.Visible = false;
    //    }
    }

    protected void btnspsave_Click(object sender, EventArgs e)
    {
        //lblmsg.Text = "";
        //try
        //{
        //    if (lblspvehicleno.Text.Trim() != "")
        //    {
        //        if (txtspremarks.Text.Trim() != "" || txtspremarks.Visible == false)
        //        {
        //            SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        //            SqlCommand cmd3 = new SqlCommand("", con);
        //            cmd3.CommandText = "Select TOP 1 SlNo From TblMaster Where Regno = '" + lblspvehicleno.Text.Trim().Replace("<font size='4' color='red'> *</font>", "").Trim() + "' AND Position <> 'Delivered' order by SlNo Desc";

        //            SqlDataAdapter da = new SqlDataAdapter(cmd3);
        //            DataTable dt = new DataTable();
        //            da.Fill(dt);
        //            if (dt.Rows.Count > 0)
        //            {
        //                lblservid.Text = dt.Rows[0]["Slno"].ToString();
        //            }
        //            if (drpsptype.SelectedIndex == 0)
        //            {
        //                string sConnString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        //                SqlConnection oConn = new SqlConnection(sConnString);
        //                SqlCommand cmd = new SqlCommand("InsertTblRemarks", oConn);
        //                cmd.Parameters.AddWithValue("@RefNo", lblservid.Text.Trim());
        //                if (txtspremarks.Visible == true)
        //                    cmd.Parameters.AddWithValue("@Comment", txtspremarks.Text.Trim());
        //                else if (ddlSRemarks.SelectedItem.Text == "--Select--")
        //                {
        //                    lblmsg.Text = "Please Add Remarks.";
        //                    lblmsg.ForeColor = Color.Red;
        //                    return;
        //                }
        //                else
        //                    cmd.Parameters.AddWithValue("@Comment", ddlSRemarks.SelectedItem.Text.Trim());
        //                cmd.Parameters.AddWithValue("@DTM", DateTime.Now);
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                oConn.Open();
        //                cmd.ExecuteNonQuery();
        //                lblmsg.Text = "Saved Successfully";
        //                lblmsg.ForeColor = Color.Green;
        //                oConn.Close();
        //                txtspremarks.Text = "";

        //                if (txtServiceAction.Text.Trim() != "" || txtRecomendation.Text.Trim() != "")
        //                {
        //                    oConn.Open();
        //                    cmd = new SqlCommand("UpdateTblTimeMonitoring", oConn);
        //                    cmd.CommandType = CommandType.StoredProcedure;
        //                    cmd.Parameters.AddWithValue("@ActionTaken", txtServiceAction.Text.Trim());
        //                    cmd.Parameters.AddWithValue("@Recommendation", txtRecomendation.Text.Trim());
        //                    cmd.Parameters.AddWithValue("@Regno", lblspvehicleno.Text.Trim().Replace("<font size='4' color='red'> *</font>", ""));
        //                    cmd.ExecuteNonQuery();
        //                    lblmsg.Text = "Saved Successfully";
        //                    lblmsg.ForeColor = Color.Green;
        //                    con.Close();
        //                    txtServiceAction.Text = "";
        //                    txtRecomendation.Text = "";
        //                }
        //            }
        //            else if (drpsptype.SelectedIndex == 1)
        //            {
        //                string sConnString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        //                SqlConnection oConn = new SqlConnection(sConnString);
        //                SqlCommand cmd = new SqlCommand("InsertTblProcessRemarks", oConn);
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("@ServiceId", lblservid.Text.Trim());
        //                cmd.Parameters.AddWithValue("@ProcessId", drpspprocess.SelectedValue);
        //                cmd.Parameters.AddWithValue("@DateOfRemarks", DateTime.Now);
        //                if (txtspremarks.Visible == true)
        //                    cmd.Parameters.AddWithValue("@Remarks", txtspremarks.Text.Trim());
        //                else if (ddlSRemarks.SelectedItem.Text == "--Select--")
        //                {
        //                    lblmsg.Text = "Please Add Remarks.";
        //                    lblmsg.ForeColor = Color.Red;
        //                    return;
        //                }
        //                else
        //                    cmd.Parameters.AddWithValue("@Remarks", ddlSRemarks.SelectedItem.Text.Trim());
        //                oConn.Open();
        //                cmd.ExecuteNonQuery();
        //                lblmsg.Text = "Saved Successfully";
        //                lblmsg.ForeColor = Color.Green;
        //                oConn.Close();
        //                txtspremarks.Text = "";
        //            }
        //            BindGrid();
        //            drpsptype.SelectedIndex = -1;
        //            ddlSRemarks.SelectedIndex = -1;
        //        }
        //        else
        //        {
        //            lblmsg.Text = "Please Add Remarks!";
        //            lblmsg.ForeColor = Color.Red;
        //        }
        //    }
        //    else
        //    {
        //        lblmsg.Text = "VRN/VIN Not Provided!";
        //        lblmsg.ForeColor = Color.Red;
        //    }
        //}
        //catch (Exception ex)
        //{
        //}
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

    protected void btnPDCsave_Click(object sender, EventArgs e)
    {
    }

    protected void grdDisplay_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        lblmsg.CssClass = "reset";
        lblvehicleno.Text = grdDisplay.SelectedRow.Cells[1].Text.Trim().Replace("<font size='4' color='red'> *</font>", "");
        //lblpdtvehno.Text = grdDisplay.SelectedRow.Cells[1].Text.Trim().Replace("<font size='4' color='red'> *</font>", "");
        //lblspvehicleno.Text = grdDisplay.SelectedRow.Cells[1].Text.Trim().Replace("<font size='4' color='red'> *</font>", "");
        lblvehno.Text = grdDisplay.SelectedRow.Cells[1].Text.Trim().Replace("<font size='4' color='red'> *</font>", "");
    }

    protected void btn_logout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();
        Response.Redirect("login.aspx");
    }

    protected void cmbCustType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //try
        //{
        //    if (cmbCustType.SelectedIndex != 0)
        //    {
        //        cmbServiceType.SelectedIndex = -1;
        //        cmbVehicleModel.SelectedIndex = -1;
        //        cmbProcess.SelectedIndex = -1;
        //        TxtDate1.Text = "";
        //        TxtDate2.Text = "";
        //        txtVehicleNumber.Text = "";
        //        txtTagNo.Text = "";
        //        lblmsg.Text = "";
        //    }
        //    BindGrid();
        //    TabContainer1.Visible = false;
        //}
        //catch (Exception ex)
        //{
        //}
    }

    protected void cmbServiceType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //if (cmbServiceType.SelectedIndex != 0)
            //{
            //   // cmbCustType.SelectedIndex = -1;
            //    cmbVehicleModel.SelectedIndex = -1;
            //    cmbProcess.SelectedIndex = -1;
            //    TxtDate1.Text = "";
            //    TxtDate2.Text = "";
            //    txtVehicleNumber.Text = "";
            //    txtTagNo.Text = "";
            //    lblmsg.Text = "";
            //}
            //BindGrid();
            //TabContainer1.Visible = false;
        }
        catch (Exception ex)
        {
        }
    }

    protected void cmbVehicleModel_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //if (cmbVehicleModel.SelectedIndex != 0)
            //{
            //  //  cmbCustType.SelectedIndex = -1;
            //    cmbServiceType.SelectedIndex = -1;
            //    cmbProcess.SelectedIndex = -1;
            //    TxtDate1.Text = "";
            //    TxtDate2.Text = "";
            //    txtVehicleNumber.Text = "";
            //    txtTagNo.Text = "";
            //    lblmsg.Text = "";
            //}
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
               // cmbCustType.SelectedIndex = -1;
                //cmbServiceType.SelectedIndex = -1;
                //cmbVehicleModel.SelectedIndex = -1;
                //TxtDate1.Text = "";
                //TxtDate2.Text = "";
                //txtVehicleNumber.Text = "";
                //txtTagNo.Text = "";
                //lblmsg.Text = "";
            }
            BindGrid();
            TabContainer1.Visible = false;
        }
        catch (Exception ex)
        {
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            lblmsg.Text = "";
            lblmsg.CssClass = "reset";
            //  cmbCustType.SelectedIndex = -1;
            //cmbServiceType.SelectedIndex = -1;
            cmbVehicleModel.SelectedIndex = -1;
            cmbProcess.SelectedIndex = -1;
            cmbSA.SelectedIndex = -1;
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
                lblmsg.CssClass = "ErrMsg";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

            }
            else if (cs[0] == 0 && cs[1] == 1)
            {
                lblmsg.Text = "Please Select From Date";
                lblmsg.CssClass = "ErrMsg";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

            }
            else if (cs[0] == 0 && cs[1] == 0 && cs[2] == 0 && cs[3] == 0)
            {
                lblmsg.Text = "Please Select Date Range Or VRN/VIN Or Tag No";
                lblmsg.CssClass = "ErrMsg";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

            }
        }
        catch (Exception ex)
        {
            lblLoading.Text = ex.Message;
        }
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
    }

    protected void btnrrcncl_Click(object sender, EventArgs e)
    {
        TabContainer1.Visible = false;
    }

    protected void btnc_Click(object sender, EventArgs e)
    {
        TabContainer1.Visible = false;
    }

    protected void txtRevPDT_TextChanged(object sender, EventArgs e)
    {
        //cmbHH.Text = "";
    }

    protected void btnRefresh_Click1(object sender, EventArgs e)
    {
        TxtDate1.Text = "";
        TxtDate2.Text = "";
        txtVehicleNumber.Text = "";
        cmbProcess.SelectedIndex = -1;
        cmbVehicleModel.SelectedIndex = -1;
        //cmbServiceType.SelectedIndex = -1;
       // cmbCustType.SelectedIndex = -1;
        cmbSA.SelectedIndex = -1;
        txtTagNo.Text = "";
        lblmsg.Text = "";
        lblmsg.CssClass = "reset";
        BindGrid();
       
    }

    #region "HOVERING"

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

    private static DataTable GetRegNoHover(string RefNo)
    {
        SqlConnection con = new SqlConnection(System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["TMLDealercode"] + "-TMLConString"].ToString());
        DataTable dt = new DataTable();

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
        string tt16 = dt.Rows[0]["GateOut"].ToString();
        string tt17 = dt.Rows[0]["KMS"].ToString();
        string str;
            if (tt7 == "Delivered")
            str = "<span><table class='mydatagrid'><tr style='height:18px;color:#a62724 !important;'><td style='color:#a62724 !important;'><strong>CUSTOMER NAME</strong></td><td>:</td><td>" + tt3 + "</td></tr><tr style='height:18px;'><td style='color:#a62724 !important;'><strong>CONTACT NO</strong></td><td>:</td><td>" + tt4 + "</td></tr><tr style='height:18px'><td style='color:#a62724 !important;'><strong>IN TIME</strong></td><td>:</td><td>" + tt6 + "</td></tr><tr style='height:18px'><td style='color:#a62724 !important;'><strong>SA NAME</strong></td><td>:</td><td>" + tt11 + "</td></tr><tr style='height:18px'><td style='color:#a62724 !important;'><strong>STAGE</strong></td><td>:</td><td>" + tt7 + "</td></tr><tr style='height:18px'><td style='color:#a62724 !important;'><strong>OUT TIME</strong></td><td>:</td><td>" + tt16 + "</td></tr><tr style='height:18px'><td><strong>KMS</strong></td><td>:</td><td>" + tt17 + "</td></tr></table></span>";
        else
            str = "<span><table class='mydatagrid'><tr style='height:18px;color:#a62724 !important;'><td style='color:#a62724 !important;'><strong>CUSTOMER NAME</strong></td><td>:</td><td>" + tt3 + "</td></tr><tr style='height:18px'><td style='color:#a62724 !important;'><strong>CONTACT NO</strong></td><td>:</td><td>" + tt4 + "</td></tr><tr style='height:18px'><td style='color:#a62724 !important;'><strong>IN TIME</strong></td><td>:</td><td>" + tt6 + "</td></tr><tr style='height:18px'><td style='color:#a62724 !important;'><strong>SA NAME</strong></td><td>:</td><td>" + tt11 + "</td></tr><tr style='height:18px'><td style='color:#a62724 !important;'><strong>STAGE</strong></td><td>:</td><td>" + tt7 + "</td></tr><tr style='height:18px'><td style='color:#a62724 !important;'><strong>KMS</strong></td><td>:</td><td>" + tt17 + "</td></tr></table></span>";
       
        return str;
    }
    private static DataTable GetProcessHover(string RefNo, string ProcessId)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["TMLDealercode"] + "-TMLConString"].ToString());
            //SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
            SqlCommand cmd = new SqlCommand("GetProcessHover_BodyShop", con);
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
        catch(Exception ex)
        {
            return null;
        }
    }

    [System.Web.Services.WebMethod(EnableSession = true)]
    public static string LoadProcessInOutTime(string RefNo, string ProcessId, string ProcessName)
    {
        DataTable dt = new DataTable();
        dt = GetProcessHover(RefNo, ProcessId);
        string inTime = "";
        string outTime = "";
        if (dt.Rows.Count > 0)
        {
            inTime = dt.Rows[0][0].ToString().Replace("#", " ");
            outTime = dt.Rows[0][1].ToString().Replace("#", " ");
        }
        
        string str = "<span><table class='mydatagrid' id=InOutPnl style=width:100%;><tr style=text-align:center;color:#a62724 !important;font-weight:bold;><td colspan=2><strong><center>" + ProcessName+ "</center></strong></td></tr><tr><td style=width:90px;>In Time: </td><td style=text-align:center;>" + inTime + "</td></tr><tr><td style=width:90px;>Out Time: </td><td style=text-align:center;>" + outTime + "</td></tr></table></span>";
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

        string str = "<span><table class='mydatagrid' id=InOutPnl style=width:100%;><tr style=color:#a62724;><td colspan=2><strong>" + SubProcessName + " : " + empName + "</strong></td></tr><tr><td style=width:90px;>In Time: </td><td style=text-align:center;>" + inTime + "</td></tr><tr><td style=width:90px;>Out Time: </td><td style=text-align:center;>" + outTime + "</td></tr></table></span>";
        return str;
    }

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

        string str = "<span><table class='mydatagrid' id=InOutPnl style=width:100%;><tr style=text-align:center;color:#a62724;font-weight:bold;><td colspan=2><strong>" + Tech + ": " + EmpName + "</strong></td></tr><tr><td style=width:90px;>In Time: </td><td style=text-align:center;>" + inTime + "</td></tr><tr><td style=width:90px;>Out Time: </td><td style=text-align:center;>" + outTime + "</td></tr></table></span>";
        return str;
    }

    [System.Web.Services.WebMethod(EnableSession = true)]
    public static string LoadRemarks(string RefNo)
    {
        DataTable dt = new DataTable();
        dt = GetRemarksHover(RefNo);
        //GridView remGrid = new GridView();
        //remGrid.RowDataBound += new GridViewRowEventHandler(remGrid_RowDataBound);
        //remGrid.DataSource = dt;
        //remGrid.DataBind();
        string str = "";
        if (dt.Rows.Count > 0)
        {
            str = "<span><table class='mydatagrid' style='width:250px'><tr style=text-align:center;color:#a62724;font-weight:bold;><th style='color:#a62724 !important;'>&nbsp;&nbsp;Time</th><th style='color:#a62724 !important;'>&nbsp;&nbsp;Remarks</th></tr>";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                str += "<tr><td>" + dt.Rows[i][0].ToString().Replace("#", " ") + "</td><td>" + dt.Rows[i][1].ToString() + "</td></tr>";
            }
            str += "</table></span>";
        }
        else
        {
            str = "<table class='mydatagrid' style=width:100px;text-align:center;><tr><th>No Remarks</th></tr></table>";
        }
        return str;
    }


    [System.Web.Services.WebMethod(EnableSession = true)]
    public static string LoadJADetails(string RefNo)
    {
        DataTable dt = new DataTable();
        dt = GetJAHover(RefNo);
        string str = "<span><table class='mydatagrid'><tr><td style='color:#a62724 !important;'><strong>Bay #</strong></td><td></td><td style='color:#a62724 !important;'><strong>Team Lead</strong></td><td align='center' valign='middle'></td><td style='color:#a62724 !important;'><strong>In Time</strong></td><td align='center' valign='middle'></td><td style='color:#a62724 !important;'><strong>Allotted</strong></td></tr><tr><td>" + dt.Rows[0][0].ToString() + "</td><td></td><td align='center' valign='middle'>" + dt.Rows[0][3].ToString() + "</td><td align='center' valign='middle'></td><td align='center' valign='middle'> " + dt.Rows[0][1].ToString() + "</td><td align='center' valign='middle'></td><td align='center' valign='middle'>" + dt.Rows[0][2].ToString() + "</td></tr><tr bgcolor='White'><td></td><td></td><td align='center' valign='middle'></td><td align='center' valign='middle'></td><td align='center' valign='middle'></td><td align='center' valign='middle'></td><td align='center' valign='middle'></td></tr><tr><td><strong>Technician</strong></td><td width='10px'></td><td align='center' valign='middle'>" + dt.Rows[0][4].ToString() + "</td><td align='center' valign='middle' width='10px'></td><td align='center' valign='middle'>" + dt.Rows[0][7].ToString() + "</td><td align='center' valign='middle' width='10px'></td><td align='center' valign='middle'>" + dt.Rows[0][10].ToString() + "</td></tr><tr><td><strong>Jobs</strong></td><td></td><td align='center' valign='middle'>" + dt.Rows[0][5].ToString() + "</td><td align='center' valign='middle'></td><td align='center' valign='middle'>" + dt.Rows[0][8].ToString() + "</td><td align='center' valign='middle'></td><td align='center' valign='middle'>" + dt.Rows[0][11].ToString() + "</td></tr><tr><td><strong>Allotted</strong></td><td></td><td align='center' valign='middle'>" + dt.Rows[0][6].ToString() + "</td><td align='center' valign='middle'></td><td align='center' valign='middle'> " + dt.Rows[0][9].ToString() + "</td><td align='center' valign='middle'></td><td align='center' valign='middle'>" + dt.Rows[0][12].ToString() + "</td></tr></table></span>";
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
            str = "<table class='mydatagrid'><tr style='color:#a62724;'><td style='padding-left:3px;'><strong>PARTS NAME</strong></td><td style='width:90px;' align='center'><strong>STATUS</strong></td></tr>";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i % 2 == 0)
                {
                    if (dt.Rows[i]["Alloted"].ToString() == "Y")
                    {
                        AllotedTime = "";
                        str = str + "<tr style='height:20px;' bgcolor='#E4E4E4'><td style='padding-left:3px;'>" + dt.Rows[i]["PartsName"].ToString() + "</td><td style='width:50px;' align='center'>" + dt.Rows[i]["Alloted"].ToString().Replace("N", "<img src='images/JCR/No.png' Alt=''  width='16' height='16'/>").Replace("Y", "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN.png' Alt=''  width='16' height='16'/>") + "</td><td style='padding-left:3px;' align='center'>" + AllotedTime + "</td></tr>";
                    }
                    else
                    {
                        AllotedTime = dt.Rows[i][2].ToString().Replace("#", " ");
                        str = str + "<tr style='height:20px;' bgcolor='#E4E4E4'><td style='padding-left:3px;'>" + dt.Rows[i]["PartsName"].ToString() + "</td><td style='width:50px;' align='center'>" + dt.Rows[i]["Alloted"].ToString().Replace("N", "<img src='images/JCR/No.png' Alt=''  width='16' height='16'/>").Replace("Y", "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN.png' Alt=''  width='16' height='16'/>") + "</td><td style='padding-left:3px;' align='center'>" + AllotedTime + "</td></tr>";
                    }
                }
                else
                {
                    //str = str + "<tr style='height:20px;' bgcolor='#FFFFFF'><td style='padding-left:3px;'>" + dt.Rows[i]["PartsName"].ToString() + "</td><td style='width:50px;' align='center'>" + dt.Rows[i]["Alloted"].ToString().Replace("N", "<img src='images/JCR/No.png' Alt=''  width='16' height='16'/>").Replace("Y", "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN.png' Alt=''  width='16' height='16'/>") + "</td><td style='padding-left:3px;' align='center'>" + AllotedTime + "</td></tr>";
                    if (dt.Rows[i]["Alloted"].ToString() == "Y")
                    {
                        AllotedTime = "";
                        str = str + "<tr style='height:20px;' bgcolor='#FFFFFF'><td style='padding-left:3px;'>" + dt.Rows[i]["PartsName"].ToString() + "</td><td style='width:50px;' align='center'>" + dt.Rows[i]["Alloted"].ToString().Replace("N", "<img src='images/JCR/No.png' Alt=''  width='16' height='16'/>").Replace("Y", "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN.png' Alt=''  width='16' height='16'/>") + "</td><td style='padding-left:3px;' align='center'>" + AllotedTime + "</td></tr>";
                    }
                    else
                    {
                        AllotedTime = dt.Rows[i][2].ToString().Replace("#", " ");
                        str = str + "<tr style='height:20px;' bgcolor='#FFFFFF'><td style='padding-left:3px;'>" + dt.Rows[i]["PartsName"].ToString() + "</td><td style='width:50px;' align='center'>" + dt.Rows[i]["Alloted"].ToString().Replace("N", "<img src='images/JCR/No.png' Alt=''  width='16' height='16'/>").Replace("Y", "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN.png' Alt=''  width='16' height='16'/>") + "</td><td style='padding-left:3px;' align='center'>" + AllotedTime + "</td></tr>";
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

    //static void remGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.Header)
    //    {
    //        e.Row.Cells[0].Text = "Date";
    //        e.Row.Cells[1].Text = "Remarks";
    //    }
    //}

    #endregion

    protected void cmbEmpType_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void getSA()
    {
        cmbSA.Items.Clear();
        cmbSA.Items.Add("--Select--");
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

    protected void getTL()
    {
        //cmbTeamLead.Items.Clear();
        //cmbTeamLead.Items.Add("TeamLead");
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlCommand cmd = new SqlCommand("GetTeamLead", con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        cmd.CommandType = CommandType.StoredProcedure;
        if (con.State != ConnectionState.Open)
            con.Open();
        sda.Fill(dt);
        con.Close();
        //cmbTeamLead.DataSource = dt;
        //cmbTeamLead.DataValueField = "EmpId";
        //cmbTeamLead.DataTextField = "EmpName"; cmbTeamLead.DataBind();
    }

    protected void Timer3_Tick(object sender, EventArgs e)
    {
        lblmsg.ForeColor = Color.Green;
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
                            string str3 = "Update tblMaster set RegNo='" + txtenewvhno.Text.Trim() + "' where SlNo='" + dt.Rows[0][0].ToString() + "'";
                            SqlCommand cmd3 = new SqlCommand(str3, con);
                            cmd3.ExecuteNonQuery();
                            lblmsg.Text = "Updated Successfully";
                            lblmsg.CssClass = "ScsMsg";
                            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

                            txtevehno.Text = txtenewvhno.Text;
                            txtenewvhno.Text = "";
                            BindGrid();
                            btnecncl_Click(null, null);
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
                        lblmsg.Text = "Registration No. Not Exist";
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
            lblmsg.Text = ex.Message;
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
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
                            lblmsg.CssClass = "ScsMsg";
                            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

                        }
                        else
                        {
                            lblmsg.Text = "Update Aborted, Please Try Again.";
                            lblmsg.CssClass = "ErrMsg";
                            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

                        }
                    }

                    catch (Exception ex)
                    {
                        lblmsg.Text = ex.Message;
                    }
                    txtCustName.Text = "";
                    txtmobile.Text = "";
                    txtemailid.Text = "";
                }
                else
                {
                    lblmsg.Text = "VRN/VINt Registered for Servicing";
                    lblmsg.CssClass = "ErrMsg";
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

                    txtvehicle.Focus();
                }
            }
            else
            {
                lblmsg.Text = "Enter VRN/VIN.";
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

    private void FillVehicleStatus()
    {
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());

        try
        {
            SqlCommand cmd = new SqlCommand();
            if (Session["ROLE"].ToString() == "BodyShop SA")
            {
                cmd.CommandText = "GetCountVehicleStatus_Bodyshop";
                cmd.Parameters.AddWithValue("@EmpId", Session["EmpId"].ToString());
            }
            else
            {
                cmd.CommandText = "GetCountVehicleStatus_Bodyshop";
                cmd.Parameters.AddWithValue("@EmpId", "0");
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
                //lbHold.Text = dt.Rows[0][3].ToString().Trim();
            }
            else
            {
                lbUnDelivered.Text = "0";
                lbReady.Text = "0";
                lbIdle.Text = "0";
               // lbHold.Text = "0";
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

    protected void Button4_Click(object sender, EventArgs e)
    {
        txtvehicle.Text = "";
        txtCustName.Text = "";
        txtmobile.Text = "";
        txtemailid.Text = "";
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
                lblmsg.Text = flag.Value.ToString();
                if (lblmsg.Text.Contains("Successfully"))
                {
                    lblmsg.CssClass = "ScsMsg";
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

                }
                else { 
                lblmsg.CssClass = "ErrMsg";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                }
                txtcardno.Text = txtnewcrdno.Text.ToString();
                txtnewcrdno.Text = "";
                BindGrid();
            }
            else
            {
                lblmsg.CssClass = "ErrMsg";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

                lblmsg.Text = "Enter New Card No.";
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

    protected void btncncl_Click(object sender, EventArgs e)
    {
        txtcardno.Text = "";
        txtnewcrdno.Text = "";
        lblvehicleUPDTag.Text = "";
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
        lblmsg.Text = "";
        lblmsg.CssClass = "reset";
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

    protected void ddVOutRemarks_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        lblmsg.CssClass = "reset";
        //if (ddVOutRemarks.SelectedValue == "-1")
        //{
        //    txt_VORemarks.Visible = true;
        //}
        //else
        //{
        //    txt_VORemarks.Visible = false;
        //}
    }

    protected void ddPDTRemarks_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddPDTRemarks.SelectedValue == "-1")
        //{
        //    txtPDTComment.Visible = true;
        //}
        //else
        //{
        //    txtPDTComment.Visible = false;
        //}
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

    protected void btn_BayConfirmationUpdate_Click(object sender, EventArgs e)
    {
        if (ddlBayConfirmationList.Text.ToString().Trim() != "" && lbBCRegNo.Text.ToString().Trim() != "")
        {
            string sConnString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
            SqlConnection con = new SqlConnection(sConnString);
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();

                SqlCommand cmd = new SqlCommand("udpBayConfirm", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ServiceId", DataManager.GetRefNo(lbBCRegNo.Text.ToString().Trim(), Session[Session["TMLDealercode"] + "-TMLConString"].ToString()).ToString());
                cmd.Parameters.AddWithValue("@BayId", ddlBayConfirmationList.SelectedValue.ToString());

                SqlParameter msg = cmd.Parameters.Add("@Flag", SqlDbType.VarChar, 100);
                msg.Direction = ParameterDirection.Output;
                msg.Value = "";
                cmd.ExecuteNonQuery();
                lblmsg.Text = msg.Value.ToString().Replace('#', ' ');
                if (msg.Value.ToString().Contains('#') == true)
                    ddlBayConfirmationList.Items.Clear();
                FillBayConfirmationList(DataManager.GetRefNo(lbBCRegNo.Text.ToString().Trim(), Session[Session["TMLDealercode"] + "-TMLConString"].ToString()).ToString());
                FillBayFree(DataManager.GetRefNo(lbBCRegNo.Text.ToString().Trim(), Session[Session["TMLDealercode"] + "-TMLConString"].ToString()).ToString());
            }
            catch (Exception ex) { }
            finally
            {
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
        }
    }

    protected void btn_BayFreeUpdate_Click(object sender, EventArgs e)
    {
        if (lbBayFree.Text.ToString().Trim() != "" && lbBFRegNo.Text.ToString().Trim() != "")
        {
            string sConnString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
            SqlConnection con = new SqlConnection(sConnString);
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                SqlCommand cmd = new SqlCommand("udpBayFree", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ServiceId", DataManager.GetRefNo(lbBFRegNo.Text.ToString().Trim(), Session[Session["TMLDealercode"] + "-TMLConString"].ToString()).ToString());
                SqlParameter msg = cmd.Parameters.Add("@Flag", SqlDbType.VarChar, 100);
                msg.Direction = ParameterDirection.Output;
                msg.Value = "";
                cmd.ExecuteNonQuery();
                lblmsg.Text = msg.Value.ToString().Replace('#', ' ');
                if (msg.Value.ToString().Contains('#') == true)
                    lbBayFree.Text = string.Empty;
                FillBayConfirmationList(DataManager.GetRefNo(lbBFRegNo.Text.ToString().Trim(), Session[Session["TMLDealercode"] + "-TMLConString"].ToString()).ToString());
                FillBayFree(DataManager.GetRefNo(lbBFRegNo.Text.ToString().Trim(), Session[Session["TMLDealercode"] + "-TMLConString"].ToString()).ToString());
            }
            catch (Exception ex) { }
            finally
            {
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
        }
    }

    protected void Timer2_Tick(object sender, EventArgs e)
    {
    }

    protected void btn_Back_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["ROLE"].ToString() == "SERVICE ADVISOR")
        {
            Response.Redirect("SAHome.aspx");
        }
    }

    //protected void cmbTeamLead_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (cmbTeamLead.SelectedIndex != 0)
    //        {
    //          //  cmbCustType.SelectedIndex = -1;
    //            //cmbServiceType.SelectedIndex = -1;
    //            cmbVehicleModel.SelectedIndex = -1;
    //            TxtDate1.Text = "";
    //            TxtDate2.Text = "";
    //            txtVehicleNumber.Text = "";
    //            txtTagNo.Text = "";
    //            lblmsg.Text = "";
    //        }
    //        BindGrid();
    //        TabContainer1.Visible = false;
    //    }
    //    catch (Exception ex)
    //    {
    //    }
    //}
    protected void drpOrderBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (drpOrderBy.SelectedIndex != -1)
            {
               // cmbCustType.SelectedIndex = -1;
                //cmbServiceType.SelectedIndex = -1;
                cmbVehicleModel.SelectedIndex = -1;
                TxtDate1.Text = "";
                TxtDate2.Text = "";
                txtVehicleNumber.Text = "";
                txtTagNo.Text = "";
               // lblmsg.Text = "";
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
    protected void btnParts_Click(object sender, EventArgs e)
    {
        //SqlConnection con = new SqlConnection();
        //SqlCommand cmd = new SqlCommand();
        //try
        //{
        //    if (txtParts.Text.ToString() == "")
        //    {
        //        lblmsg.Text = "Please Enter Parts";
        //    }
        //    else
        //    {
        //        con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        //        cmd = new SqlCommand("udpInsertPartsRequisition", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@RefNo", Convert.ToInt16(lblRefnoParts.Text.ToString()));
        //        cmd.Parameters.AddWithValue("@PartsName", txtParts.Text.ToString());
        //        if (con.State != ConnectionState.Open)
        //            con.Open();
        //        cmd.ExecuteNonQuery();

        //        lblmsg.ForeColor = Color.Green;
        //        lblmsg.Text = "Inserted Successfully..!";
        //        BindPartsGrid(Convert.ToInt16(lblRefnoParts.Text.ToString()));
        //        txtParts.Text = "";
        //    }
        //}
        //catch (Exception ex)
        //{
        //    if (con.State == ConnectionState.Open)
        //        con.Close();
        //}
    }

    protected void grdParts_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //grdParts.PageIndex = e.NewPageIndex;
        //BindPartsGrid(Convert.ToInt16(lblRefnoParts.Text.ToString()));
    }
    protected void BindPartsGrid(int Refno)
    {
        //SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        //SqlCommand cmd = new SqlCommand("udpListPartsRequisition", con);
        //cmd.Parameters.AddWithValue("@RefId", Refno);
        //cmd.CommandType = CommandType.StoredProcedure;
        //SqlDataAdapter sda = new SqlDataAdapter(cmd);
        //DataTable dtParts = new DataTable();
        //sda.Fill(dtParts);
        //if (dtParts.Rows.Count > 0)
        //{
        //    grdParts.DataSource = dtParts;
        //    grdParts.DataBind();
        //}
        //else
        //{
        //    grdParts.DataSource = null;
        //    grdParts.DataBind();
        //}
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public static string[] GetCompletionList(string prefixText, int count)
    {
        SqlCommand cmd = new SqlCommand();
        SqlConnection con1 = new SqlConnection(System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["TMLDealercode"] + "-TMLConString"].ToString());
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
    protected void grdParts_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //try
        //{
        //    SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        //    GridViewRow dr = (GridViewRow)grdParts.Rows[e.RowIndex];
        //    int slno = Convert.ToInt32(dr.Cells[0].Text);
        //    int RefNo = Convert.ToInt32(dr.Cells[1].Text);
        //    int Status = Convert.ToInt16(dr.Cells[3].Text);
        //    if (Status == 2)
        //    {
        //        lblmsg.ForeColor = Color.Red;
        //        lblmsg.Text = "Part is in Used";
        //    }
        //    else
        //    {
        //        SqlCommand cmddel = new SqlCommand("udpDeletePartsRequisition", con);
        //        cmddel.CommandType = CommandType.StoredProcedure;
        //        cmddel.Parameters.AddWithValue("@SlNo", slno);
        //        con.Open();
        //        cmddel.ExecuteNonQuery();
        //        lblmsg.ForeColor = Color.Green;
        //        lblmsg.Text = "Deleted Successfully";
        //        BindPartsGrid(Convert.ToInt16(lblRefnoParts.Text.ToString()));
        //    }
        //}
        //catch (Exception ex)
        //{
        //}
    }

    
    protected void back_Click1(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("BodyshopSA.aspx");
    }
}