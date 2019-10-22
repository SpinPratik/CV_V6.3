using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class _Default : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
  
    
    protected void Page_Load(object sender, EventArgs e)
    {
        //for (int i = 0; i < gvOrders.Rows.Count; i++)
        //{
        //    Label lblmsg = (Label)(gvOrders.Rows[i].FindControl("lblmsg"));
        //    Label lblspprocess = (Label)(gvOrders.Rows[i].FindControl("lblspprocess"));
        //    Label lblservid = (Label)(gvOrders.Rows[i].FindControl("lblservid"));
        //    Label lblServiceRecom = (Label)(gvOrders.Rows[i].FindControl("lblServiceRecom"));

        //    Label lblServiceAction = (Label)(gvOrders.Rows[i].FindControl("lblServiceAction"));
        //    TextBox lblspvehicleno = (TextBox)(gvOrders.Rows[i].FindControl("lblspvehicleno"));
        //    TextBox txtspremarks = (TextBox)(gvOrders.Rows[i].FindControl("txtspremarks"));
        //    TextBox txtServiceAction = (TextBox)(gvOrders.Rows[i].FindControl("txtServiceAction"));
        //    TextBox txtRecomendation = (TextBox)(gvOrders.Rows[i].FindControl("txtRecomendation"));
        //    DropDownList drpsptype = (DropDownList)(gvOrders.Rows[i].FindControl("drpsptype"));
        //    DropDownList drpspprocess = (DropDownList)(gvOrders.Rows[i].FindControl("drpspprocess"));
        //    DropDownList ddlSRemarks = (DropDownList)(gvOrders.Rows[i].FindControl("ddlSRemarks"));
        // FillRemarksTemplate(6, ref ddlSRemarks);
        //FillRemarksTemplate(5, ref ddVOutRemarks);
        //FillRemarksTemplate(2, ref ddPDTRemarks);
        //FillRemarksTemplate(4, ref CmbCancelationRemarks);
        //SqlDataSource DataSource1 = (SqlDataSource)(gvOrders.Rows[i].FindControl("SqlDataSource1")); 
        //DataSource1.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        // }

        
    
        if (!IsPostBack)
        {
            getTL();
            getSA();
            BindGrid();
        }
    }

    protected void rbType_SelectedIndexChanged(object sender, EventArgs e)
    {
        

        try
        {
            //txtVehicleNumber.Text = "";
            //txtTagNo.Text = "";
            lblmsg.Text = "";
            lblmsg.CssClass = "reset";
            BindGrid();
            //TabContainer1.Visible = false;
        }
        catch (Exception ex)
        {
           
        }
    }

    private void FillVehicleStatus()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

        try
        {
            Session["ROLE"] = "";
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


    protected void btnspsave_Click(object sender, EventArgs e)
    {
        //for (int i = 0; i < gvOrders.Rows.Count; i++)
        //{
        //    Label lblmsg = (Label)(gvOrders.Rows[i].FindControl("lblmsg"));
           Label lblspprocess = (Label)(gvOrders.Rows[gvOrders.SelectedIndex].FindControl("lblspprocess"));
        //    Label lblservid = (Label)(gvOrders.Rows[i].FindControl("lblservid"));
        //    Label lblServiceRecom = (Label)(gvOrders.Rows[i].FindControl("lblServiceRecom"));

        //    Label lblServiceAction = (Label)(gvOrders.Rows[i].FindControl("lblServiceAction"));
        //    TextBox lblspvehicleno = (TextBox)(gvOrders.Rows[i].FindControl("lblspvehicleno"));
        //    TextBox txtspremarks = (TextBox)(gvOrders.Rows[i].FindControl("txtspremarks"));
        //    TextBox txtServiceAction = (TextBox)(gvOrders.Rows[i].FindControl("txtServiceAction"));
        //    TextBox txtRecomendation = (TextBox)(gvOrders.Rows[i].FindControl("txtRecomendation"));
        //    DropDownList drpsptype = (DropDownList)(gvOrders.Rows[i].FindControl("drpsptype"));
        //    DropDownList drpspprocess = (DropDownList)(gvOrders.Rows[i].FindControl("drpspprocess"));
        //    DropDownList ddlSRemarks = (DropDownList)(gvOrders.Rows[i].FindControl("ddlSRemarks"));

            lblmsg.Text = "";
            lblmsg.CssClass = "reset";
            try
            {
                if (lblspvehicleno.Text.Trim() != "")
                {
                    if (txtspremarks.Text.Trim() != "" || txtspremarks.Visible == false)
                    {
                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
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
                            
                            SqlConnection oConn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
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
                            if (conn.State != ConnectionState.Open)
                            {
                                conn.Open();
                            }
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
                                if (conn.State != ConnectionState.Open)
                                {
                                    conn.Open();
                                }
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
                                oConn.Close();

                            }
                        }
                        else if (drpsptype.SelectedIndex == 1)
                        {
                            
                            SqlConnection oConn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
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
                            if (conn.State != ConnectionState.Open)
                            {
                                conn.Open();
                            }
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
                       
                            SqlConnection oConn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
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

                                if (conn.State != ConnectionState.Open)
                                {
                                    conn.Open();
                                }
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
       // }
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

    private void FillRemarksTemplate(int Type, ref DropDownList ddl)
    {
        // 1-JCCRemarks ,2-PDTRemarks,3-Vehicle Cancelation ,4-Vehicle Tag Cancellation,5-Vehicle OUT,6-Service Remarks,7-Process Remarks
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
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
                //txtspremarks.Visible = false;
                //txt_VORemarks.Visible = false;
                //txtCancelationRemark.Visible = false;
            }
            else
            {
                // txtspremarks.Visible = true;
                //txt_VORemarks.Visible = true;
                //txtCancelationRemark.Visible = true;
            }
            ddl.Items.Add(new ListItem("Other", "-1"));
        }
        catch (Exception ex) { }
    }
    protected void BindGrid()
    {
        
        DataSet ds = new DataSet();
        if (conn.State != ConnectionState.Open)
        {
            conn.Open();
        }
        //string cmdstr = "Select OrderID,CustomerID,convert(varchar(10),OrderDate,103) OrderDate,convert(varchar(10),ShippedDate,103) ShippedDate,ShipName from Orders";

        string cmdstr = "JCDisplayI";

        SqlCommand cmd = new SqlCommand(cmdstr, conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Day", 1);
        cmd.Parameters.AddWithValue("@Bodyshop", "0");
        cmd.Parameters.AddWithValue("@Floor", "0");
        cmd.Parameters.AddWithValue("@CustomerType", cmbCustType.SelectedValue);
        cmd.Parameters.AddWithValue("@ServiceType", cmbServiceType.SelectedValue);
        cmd.Parameters.AddWithValue("@Model", cmbVehicleModel.SelectedValue);
        cmd.Parameters.AddWithValue("@Process", cmbProcess.SelectedValue);
        cmd.Parameters.AddWithValue("@DateFrom", TxtDate1.Text.Trim());
        cmd.Parameters.AddWithValue("@DateTo", TxtDate2.Text.Trim());
        cmd.Parameters.AddWithValue("@RegNo", txtVehicleNumber.Text.Trim());
        cmd.Parameters.AddWithValue("@Param", "");
        cmd.Parameters.AddWithValue("@SAId", 0);
        cmd.Parameters.AddWithValue("@TLId", cmbTeamLead.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@Tagno", txtTagNo.Text.Trim());
        cmd.Parameters.AddWithValue("@Status", ddlState.SelectedValue.Trim());
        cmd.Parameters.AddWithValue("@IndexOn", drpOrderBy.SelectedValue.Trim());
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        
        adp.Fill(ds);        
        
        gvOrders.DataSource = ds;
        gvOrders.DataBind();
        conn.Close();
        FillVehicleStatus();
    }
    protected void gvOrders_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        string status = "";
        string counter = "";
        string StatusTech = "";
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //SqlDataSource DataSource1 = (SqlDataSource)e.Row.FindControl("SqlDataSource1");
            //DataSource1.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            Label lblorderID = (Label)e.Row.FindControl("lblorderID");
            Label lblmsg = (Label)e.Row.FindControl("lblmsg");
            //REGNO
            if (e.Row.Cells[3].Text.Length > 10)
            {
                int lenth = e.Row.Cells[3].Text.Length;
                string cells = e.Row.Cells[3].Text.Substring(lenth - 10, 10);
                e.Row.Cells[3].Text = cells;
            }
            else
            {
                e.Row.Cells[3].Text = e.Row.Cells[3].Text.ToString();
            }


            //jdp cw
            try
            {
                if (e.Row.Cells[4].Text.ToString() == "0-0")
                    e.Row.Cells[4].Text = "";
                else if (e.Row.Cells[4].Text.ToString() == "1-0")
                    e.Row.Cells[4].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/JDP.png' Alt=''  width='16' height='16'/>";
                else if (e.Row.Cells[4].Text.ToString() == "0-1")
                    e.Row.Cells[4].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/Customer_Waiting.png' Alt=''  width='16' height='16'/>";
                else if (e.Row.Cells[4].Text.ToString() == "1-1")
                    e.Row.Cells[4].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/JDP_Customer_Waiting.png' Alt=''  width='16' height='16'/>";
            }
            catch (Exception ex)
            {
            }


            //STAUS - 000-Normal, 100-Ready , 010-Hold , 001-Idle
            try
            {
                string[] status_str = e.Row.Cells[7].Text.ToString().Split('|');
                if (status_str[0] == "0-0-0")
                {
                    e.Row.Cells[7].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/Status.png' Alt=''  width='16' height='16'/>";
                    e.Row.Cells[7].ToolTip = "Work In Progress";
                }
                else if (status_str[0] == "1-0-0")
                {
                    e.Row.Cells[7].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/Status_Ready.png' Alt='' width='16' height='16'/>";
                    e.Row.Cells[7].ToolTip = "Vehicle Ready [" + status_str[1].Trim() + "]";
                }
                else if (status_str[0] == "0-1-0")
                {
                    e.Row.Cells[7].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/Status_HOLD.png' Alt='' width='16' height='16'/>";
                    e.Row.Cells[7].ToolTip = "Hold";
                }
                else if (status_str[0] == "0-0-1")
                {
                    e.Row.Cells[7].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/Status_IDLE.png' Alt='' width='16' height='16'/>";
                    e.Row.Cells[7].ToolTip = "Idle";
                }
            }
            catch (Exception ex)
            {
            }
            //jc
            try
            {
                string getJCP = string.Empty;
                string[] JCPParts = { };
                getJCP = (e.Row.Cells[8].Text.Trim() == "&nbsp;" ? "" : e.Row.Cells[8].Text.Trim());
                JCPParts = getJCP.Split('|');
                //e.Row.Cells[8].Attributes.Add("onmouseover", "showProcessInOut(event,'" + RefNo + "','" + JCPParts[2].Replace("*", "") + "','S.A')");
                //e.Row.Cells[8].Attributes.Add("onmouseout", "hideTooltip(event)");

                status = e.Row.Cells[8].Text.ToString().Substring(0, 1);
                if (status == "0")
                {
                    if (e.Row.Cells[8].Text.ToString().Contains("*"))
                        e.Row.Cells[8].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/SA_Allot.png' Alt=''  width='16' height='16'/>";
                    else
                        e.Row.Cells[8].Text = "";
                }
                else if (status == "1")
                    e.Row.Cells[8].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/SA_WIP_ONTIME.png' Alt=''  width='16' height='16'/>";
                else if (status == "2")
                    e.Row.Cells[8].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/SA_WIP_NEAR.png' Alt=''  width='16' height='16'/>";
                else if (status == "3")
                    e.Row.Cells[8].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/SA_WIP_DELAY.png' Alt=''  width='16' height='16'/>";
                else if (status == "4")
                    e.Row.Cells[8].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN.png' Alt=''  width='16' height='16'/>";
                else if (status == "5")
                    e.Row.Cells[8].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CR.png' Alt=''  width='16' height='16'/>";
                else if (status == "6")
                    e.Row.Cells[8].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/OutSkipped.png' Alt=''  width='16' height='16'/>";
            }
            catch (Exception ex)
            {
            }


            try
            {
                string[] status_str = e.Row.Cells[9].Text.ToString().Split('|');
                if (status_str[0] == "0-0-0")
                {

                    e.Row.Cells[9].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205149/jcr/Status.png' Alt=''  width='16' height='16'/>";
                    e.Row.Cells[9].ToolTip = "Work In Progress";
                }
                else if (status_str[0] == "1-0-0")
                {


                    e.Row.Cells[9].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205149/jcr/Status_Ready.png' Alt='' width='16' height='16'/>";
                    e.Row.Cells[9].ToolTip = "Vehicle Ready [" + status_str[1].Trim() + "]";
                }
                else if (status_str[0] == "0-1-0")
                {


                    e.Row.Cells[9].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205149/jcr/Status_HOLD.png' Alt='' width='16' height='16'/>";
                    e.Row.Cells[9].ToolTip = "Hold";
                }
                else if (status_str[0] == "0-0-1")
                {

                    e.Row.Cells[9].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205149/jcr/Status_IDLE.png' Alt='' width='16' height='16'/>";
                    e.Row.Cells[9].ToolTip = "Idle";
                }
            }
            catch (Exception ex)
            {
            }

            //BA
            try
            {
                status = e.Row.Cells[10].Text.ToString().Substring(0, 1);
                StatusTech = e.Row.Cells[10].Text.ToString().Substring(1, 1);
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
                if (getTECH1.Contains("Y"))
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
                        e.Row.Cells[11].Attributes.Add("onmouseover", "showEmpInOut(event,'" + TECH1Parts[2].Replace("*", "").Replace("$", "").Replace("#", "") + "','" + TECH1Parts[1] + "','Tech1')");
                        e.Row.Cells[11].Attributes.Add("onmouseout", "hideTooltip(event)");
                    }
                }

                status = e.Row.Cells[11].Text.ToString().Substring(0, 1);

                if (status == "0")
                {
                    if (StatusTech == "0")
                        e.Row.Cells[11].Text = "";
                    else
                        e.Row.Cells[11].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/TECH_ALLOT.png' Alt=''  width='16' height='16'/>";
                }
                else if (status == "1")
                {
                    e.Row.Cells[11].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/TECH_WIP_ONTIME.png' Alt=''  width='16' height='16'/>";
                }
                else if (status == "2")
                {
                    if (blinkflag == 1)
                        e.Row.Cells[11].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/TECH_WIP_NEAR.png' Alt='' width='16' height='16'/>";
                    else
                        e.Row.Cells[11].Text = "<img id='animg' src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/TECH_WIP_NEAR.png' Alt='' width='16' height='16'/>";
                }
                else if (status == "3")
                {
                    e.Row.Cells[11].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/TECH_WIP_DELAY.png' Alt=''  width='16' height='16'/>";
                }
                else if (status == "4")
                {
                    e.Row.Cells[11].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/CN.png' Alt=''  width='16' height='16'/>";
                }
                else if (status == "5")
                {
                    e.Row.Cells[11].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/CR.png' Alt=''  width='16' height='16'/>";


                }
                else if (status == "6")
                {
                    e.Row.Cells[11].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/OutSkipped.png' Alt='' width='16' height='16'/>";
                }
                //else if (status == "7")
                //{
                //    e.Row.Cells[9].Text = "<img src='images/JCR/Hold.png' Alt='' width='24' height='24'/>";
                //}
            }
            catch (Exception ex)
            { }

            //T2 - Technician2
            try
            {
                string getTECH2 = (e.Row.Cells[12].Text.Trim() == "&nbsp;" ? "" : e.Row.Cells[12].Text.Trim());

                int blinkflag = 0;
                if (getTECH2.Contains("Y"))
                {
                    blinkflag = 1;
                    getTECH2 = getTECH2.Replace("Y", "");
                }

            
                if (getTECH2.Contains("|"))
                {
                    string[] TECH2Parts = { };
                    TECH2Parts = getTECH2.Split('|');
                 
                }
                status = e.Row.Cells[12].Text.ToString().Substring(0, 1);

                if (status == "0")
                {
                    if (Convert.ToInt16(StatusTech) > 1)
                        e.Row.Cells[12].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/TECH_ALLOT.png' Alt=''  width='16' height='16'/>";
                    else
                        e.Row.Cells[12].Text = "";
                }
                else if (status == "1")
                {
                    e.Row.Cells[12].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/TECH_WIP_ONTIME.png' Alt=''  width='16' height='16'/>";
                }
                else if (status == "2")
                {
                    if (blinkflag == 1)
                        e.Row.Cells[12].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/TECH_WIP_NEAR.png' Alt='' width='16' height='16'/>";
                    else
                        e.Row.Cells[12].Text = "<img id='animg' src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/TECH_WIP_NEAR.png' Alt='' width='16' height='16'/>";
                }
                else if (status == "3")
                {
                    e.Row.Cells[12].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/TECH_WIP_DELAY.png' Alt=''  width='16' height='16'/>";
                }
                else if (status == "4")
                {

                    e.Row.Cells[12].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/CN.png' Alt=''  width='16' height='16'/>";

                }
                else if (status == "5")
                {
                    e.Row.Cells[12].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/CR.png' Alt=''  width='16' height='16'/>";


                }
                else if (status == "6")
                {
                    e.Row.Cells[12].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/OutSkipped.png' Alt=''  width='16' height='16'/>";
                }
                //else if (status == "7")
                //{
                //    e.Row.Cells[12].Text = "<img src='images/JCR/Hold.png' Alt='' width='24' height='24'/>";
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

                
                if (getTECH3.Contains("|"))
                {
                    string[] TECH3Parts = { };
                    TECH3Parts = getTECH3.Split('|');
                   
                }

                status = e.Row.Cells[13].Text.ToString().Substring(0, 1);

                if (status == "0")
                {
                    if (Convert.ToInt16(StatusTech) > 2)
                        e.Row.Cells[13].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/TECH_ALLOT.png' Alt=''  width='16' height='16'/>";
                    else
                        e.Row.Cells[13].Text = "";
                }
                else if (status == "1")
                {
                    e.Row.Cells[13].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/TECH_WIP_ONTIME.png' Alt=''  width='16' height='16'/>";
                }
                else if (status == "2")
                {
                    if (blinkflag == 1)
                        e.Row.Cells[13].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/TECH_WIP_NEAR.png' Alt='' width='16' height='16'/>";
                    else
                        e.Row.Cells[13].Text = "<img id='animg' src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/TECH_WIP_NEAR.png' Alt='' width='16' height='16'/>";
                }
                else if (status == "3")
                {
                    e.Row.Cells[13].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/TECH_WIP_DELAY.png' Alt=''  width='16' height='16'/>";
                }
                else if (status == "4")
                {
                    e.Row.Cells[13].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/CN.png' Alt=''  width='16' height='16'/>";
                }
                else if (status == "5")
                {
                    e.Row.Cells[13].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/CR.png' Alt=''  width='16' height='16'/>";


                }
                else if (status == "6")
                {
                    e.Row.Cells[13].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/OutSkipped.png' Alt=''  width='16' height='16'/>";
                }
                //else if (status == "7")
                //{
                //    e.Row.Cells[11].Text = "<img src='images/JCR/Hold.png' Alt='' width='24' height='24'/>";
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
              
                if (e.Row.Cells[14].Text.ToString().Contains("*"))
                {
                    status = e.Row.Cells[14].Text.ToString().Substring(0, 1);
                    if (status == "0")
                        if (e.Row.Cells[14].Text.ToString().Contains("$"))
                            e.Row.Cells[14].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/NotRequired.png' Alt=''  width='16' height='16'/>";


                        else
                            e.Row.Cells[14].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/WA_allot.png' Alt=''  width='16' height='16'/>";
                    else if (status == "1")
                        e.Row.Cells[14].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/WA_WIP.png' Alt=''  width='16' height='16'/>";
                    else if (status == "2")
                        e.Row.Cells[14].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/WA_WIP_NEAR.png' Alt=''  width='16' height='16'/>";
                    else if (status == "3")
                        e.Row.Cells[14].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/WA_WIP_Delay.png' Alt=''  width='16' height='16'/>";
                    else if (status == "4")
                        e.Row.Cells[14].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/CN.png' Alt=''  width='16 height='16'/>";
                    else if (status == "5")
                        e.Row.Cells[14].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/CR.png' Alt=''  width='16' height='16'/>";


                    else if (status == "6")
                        e.Row.Cells[14].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/OutSkipped.png' Alt=''  width='16' height='16'/>";
                }
                else
                {
                    status = e.Row.Cells[14].Text.ToString().Substring(0, 1);
                    if (status == "0")
                        if (e.Row.Cells[14].Text.ToString().Contains("$"))
                            e.Row.Cells[14].Text = "";
                        else
                            e.Row.Cells[14].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/WA_ALLOT.png' Alt=''  width='16' height='16'/>";
                    else if (status == "1")
                        e.Row.Cells[14].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/WA_WIP.png' Alt=''  width='16' height='16'/>";
                    else if (status == "2")
                        e.Row.Cells[14].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/WA_WIP_NEAR.png' Alt=''  width='16' height='16'/>";
                    else if (status == "3")
                        e.Row.Cells[14].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/WA_WIP_DELAY.png' Alt=''  width='16' height='16'/>";
                    else if (status == "4")
                        e.Row.Cells[14].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/CN.png' Alt=''  width='16' height='16'/>";
                    else if (status == "5")
                        e.Row.Cells[14].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/CR.png' Alt=''  width='16' height='16'/>";


                    else if (status == "6")
                        e.Row.Cells[14].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/OutSkipped.png' Alt=''  width='16' height='16'/>";
                }
            }
            catch (Exception ex)
            { }


            //RT
            try
            {
                string getRT = (e.Row.Cells[15].Text.Trim() == "&nbsp;" ? "" : e.Row.Cells[15].Text.Trim());
                string[] RTParts = { };
                if (getRT.Contains("|"))
                {
                    RTParts = getRT.Split('|');
                }
                //e.Row.Cells[15].Attributes.Add("onmouseover", "showSubProcessInOut(event,'" + RefNo + "','" + RTParts[1] + "','Road Test')");
                //e.Row.Cells[15].Attributes.Add("onmouseout", "hideTooltip(event)");

                status = e.Row.Cells[15].Text.ToString().Substring(0, 1);
                counter = e.Row.Cells[15].Text.ToString().Split('|')[1];
                if (status == "0")
                {
                    e.Row.Cells[15].Text = "";
                }
                else if (status == "1" || status == "2" || status == "3")
                {
                    e.Row.Cells[15].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/RT_ONTIME.png' Alt=''  width='16' height='16'/>";
                }
                else if (status == "4" || status == "5")
                {
                    if (counter == "1")
                    {
                        e.Row.Cells[15].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN.png' Alt=''  width='16' height='16'/>";
                    }
                    else if (counter == "2")
                    {
                        e.Row.Cells[15].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN2.png' Alt=''  width='16' height='16'/>";
                    }
                    else if (counter == "3")
                    {
                        e.Row.Cells[15].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN3.png' Alt=''  width='16' height='16'/>";
                    }
                }
                else if (status == "6")
                {
                    e.Row.Cells[15].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/OutSkipped.png' Alt=''  width='16' height='16'/>";
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
              

                if (e.Row.Cells[15].Text.ToString().Contains("*"))
                {
                    status = e.Row.Cells[15].Text.ToString().Substring(0, 1);
                    counter = e.Row.Cells[15].Text.ToString().Split('|')[1];
                    if (status == "0")
                    {
                        if (e.Row.Cells[15].Text.ToString().Contains("$"))
                            e.Row.Cells[15].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/NotRequired.png' Alt=''  width='16' height='16'/>";
                        else
                            e.Row.Cells[15].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/WASH_allot.png' Alt=''  width='16' height='16'/>";
                    }
                    else if (status == "1")
                    {
                        e.Row.Cells[15].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/WASH_WIP.png' Alt=''  width='16' height='16'/>";
                    }
                    else if (status == "2")
                    {
                        e.Row.Cells[15].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/WASH_WIP_NEAR.png' Alt=''  width='16' height='16'/>";
                    }
                    else if (status == "3")
                    {
                        e.Row.Cells[15].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/WASH_WIP_Delay.png' Alt=''  width='16' height='16'/>";
                    }
                    else if (status == "4")
                    {
                        if (counter == "1")
                        {
                            e.Row.Cells[15].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/CN.png' Alt=''  width='16' height='16'/>";
                        }
                        else if (counter == "2")
                        {
                            e.Row.Cells[15].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/CN2.png' Alt=''  width='16' height='16'/>";
                        }
                        else if (counter == "3")
                        {
                            e.Row.Cells[15].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/CN3.png' Alt=''  width='16' height='16'/>";
                        }
                    }
                    else if (status == "5")
                    {
                        if (counter == "1")
                        {
                            e.Row.Cells[15].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/CR.png' Alt=''  width='16' height='16'/>";


                        }
                        else if (counter == "2")
                        {
                            e.Row.Cells[15].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/CR2.png' Alt=''  width='16' height='16'/>";
                        }
                        else if (counter == "3")
                        {
                            e.Row.Cells[15].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/CR3.png' Alt=''  width='16' height='16'/>";
                        }
                    }
                    else if (status == "6")
                    {
                        e.Row.Cells[15].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/OutSkipped.png' Alt=''  width='16' height='16'/>";
                    }
                }
                else
                {
                    status = e.Row.Cells[15].Text.ToString().Substring(0, 1);
                    counter = e.Row.Cells[15].Text.ToString().Split('|')[1];
                    if (status == "0")
                    {
                        if (e.Row.Cells[15].Text.ToString().Contains("$"))
                            e.Row.Cells[15].Text = "";
                        else
                            e.Row.Cells[15].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/WASH.png' Alt=''  width='16' height='16'/>";
                    }
                    else if (status == "1")
                    {
                        e.Row.Cells[15].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/WASH_WIP.png' Alt=''  width='16' height='16'/>";
                    }
                    else if (status == "2")
                    {
                        e.Row.Cells[15].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/WASH_WIP_NEAR.png' Alt=''  width='16' height='16'/>";
                    }
                    else if (status == "3")
                    {
                        e.Row.Cells[15].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/WASH_WIP_Delay.png' Alt=''  width='16' height='16'/>";
                    }
                    else if (status == "4")
                    {
                        if (counter == "1")
                        {
                            e.Row.Cells[15].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/CN.png' Alt=''  width='16' height='16'/>";
                        }
                        else if (counter == "2")
                        {
                            e.Row.Cells[15].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/CN2.png' Alt=''  width='16' height='16'/>";
                        }
                        else if (counter == "3")
                        {
                            e.Row.Cells[15].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/CN3.png' Alt=''  width='16' height='16'/>";
                        }
                    }
                    else if (status == "5")
                    {
                        if (counter == "1")
                        {
                            e.Row.Cells[15].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/CR.png' Alt=''  width='16' height='16'/>";


                        }
                        else if (counter == "2")
                        {
                            e.Row.Cells[15].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/CR2.png' Alt=''  width='16' height='16'/>";
                        }
                        else if (counter == "3")
                        {
                            e.Row.Cells[15].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/CR3.png' Alt=''  width='16' height='16'/>";
                        }
                    }
                    else if (status == "6")
                    {
                        e.Row.Cells[15].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/OutSkipped.png' Alt=''  width='16' height='16'/>";
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
              
                status = e.Row.Cells[16].Text.ToString().Substring(0, 1);
                counter = e.Row.Cells[16].Text.ToString().Split('|')[1];


                if (repeatStatus == 0)
                {
                    if (status == "0")
                    {
                        e.Row.Cells[16].Text = "";
                    }
                    else if (status == "1")
                    {
                        e.Row.Cells[16].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/QC_WIP.png' Alt='' width='16' height='16'/>";
                    }
                    else if (status == "2")
                    {
                        e.Row.Cells[16].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/QC_WIP_Near.png' Alt='' width='16' height='16'/>";
                    }
                    else if (status == "3")
                    {
                        e.Row.Cells[16].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/QC_WIP_Delay.png' Alt='' width='16' height='16'/>";
                    }
                    else if (status == "4")
                    {
                        if (counter == "1")
                        {
                            e.Row.Cells[16].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/CN.png' Alt='' width='16' height='16'/>";
                        }
                        else if (counter == "2")
                        {
                            e.Row.Cells[16].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/CN2.png' Alt='' width='18' height='18'/>";
                        }
                        else if (counter == "3")
                        {
                            e.Row.Cells[16].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/CN3.png' Alt='' width='18' height='18'/>";
                        }
                    }
                    else if (status == "5")
                    {
                        if (counter == "1")
                        {
                            e.Row.Cells[16].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/CR.png' Alt='' width='16' height='16'/>";


                        }
                        else if (counter == "2")
                        {
                            e.Row.Cells[16].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/CR2.png' Alt='' width='18' height='18'/>";
                        }
                        else if (counter == "3")
                        {
                            e.Row.Cells[16].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/CR3.png' Alt='' width='18' height='18'/>";
                        }
                    }
                    else if (status == "6")
                    {
                        e.Row.Cells[16].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/OutSkipped.png' Alt='' width='16' height='16'/>";
                    }
                }
                else
                {
                    if (status == "0")
                    {
                        e.Row.Cells[16].Text = "";
                    }
                    else if (status == "1")
                    {
                        e.Row.Cells[16].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/QC_WIPR.png' Alt='' width='16' height='16'/>";
                    }
                    else if (status == "2")
                    {
                        e.Row.Cells[16].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/QC_WIP_NearR.png' Alt='' width='16' height='16'/>";
                    }
                    else if (status == "3")
                    {
                        e.Row.Cells[16].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/QC_WIP_DelayR.png' Alt='' width='16' height='16'/>";
                    }
                    else if (status == "4")
                    {
                        if (counter == "1")
                        {
                            e.Row.Cells[16].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/CNR.png' Alt='' width='16' height='16'/>";
                        }
                        else if (counter == "2")
                        {
                            e.Row.Cells[16].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/CNR2.png' Alt='' width='16' height='16'/>";
                        }
                        else if (counter == "3")
                        {
                            e.Row.Cells[16].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/CNR3.png' Alt='' width='16' height='16'/>";
                        }
                    }
                    else if (status == "5")
                    {
                        if (counter == "1")
                        {
                            e.Row.Cells[16].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/CRR.png' Alt='' width='16' height='16'/>";
                        }
                        else if (counter == "2")
                        {
                            e.Row.Cells[16].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/CRR2.png' Alt='' width='16' height='16'/>";
                        }
                        else if (counter == "3")
                        {
                            e.Row.Cells[16].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/CRR3.png' Alt='' width='16' height='16'/>";
                        }
                    }
                    else if (status == "6")
                    {
                        e.Row.Cells[16].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/OutSkipped.png' Alt='' width='16' height='16'/>";
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
                    e.Row.Cells[17].Text = "<div style=width:20px;>";
                else if (e.Row.Cells[17].Text.ToString() == "1")
                    e.Row.Cells[17].Text = "<div style=width:20px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/circle_green.png' Alt=''  width='14' height='14'/>";

                else if (e.Row.Cells[17].Text.ToString() == "2")
                    e.Row.Cells[17].Text = "<div style=width:20px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/circle_yellow.png' Alt=''  width='14' height='14'/>";

                else if (e.Row.Cells[17].Text.ToString() == "3")
                    e.Row.Cells[17].Text = "<div style=width:20px;><img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/circle_red.png' Alt=''  width='14' height='14'/>";



            }
            catch (Exception ex)
            { }
            //PDT
            try
            {
                if (e.Row.Cells[18].Text.ToString().Contains("#"))
                {
                    if (e.Row.Cells[18].Text.ToString().Contains("Y"))
                    {
                        e.Row.Cells[18].Text = "<div style=color:BLUE;>" + e.Row.Cells[18].Text.Replace('#', ' ').Replace('Y', ' ').ToString().Trim() + "";
                    }
                    else
                    {
                        e.Row.Cells[18].Text = "<div style=color:RED;>" + e.Row.Cells[18].Text.Replace('#', ' ').Replace('N', ' ').ToString().Trim() + "";
                    }
                }
                else
                {
                    e.Row.Cells[18].Text = "<div style=color:GRAY;>" + e.Row.Cells[18].Text.Replace('$', ' ').ToString().Trim() + "";
                }
            }
            catch (Exception ex)
            { }
            //Remarks : 0-No Remarks, 1-Remarks,2- GM Remarks
            try
            {
              
                if (e.Row.Cells[20].Text.ToString() == "0")
                    e.Row.Cells[20].Text = "";
                else if (e.Row.Cells[20].Text.ToString() == "1")
                    e.Row.Cells[20].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/REMARKS_ENTRY_Green.png' Alt=''  width='16' height='16'/>";
                else if (e.Row.Cells[20].Text.ToString() == "2")
                    e.Row.Cells[20].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/REMARKS_ENTRY.png' Alt=''  width='16' height='16'/>";
            }
            catch (Exception ex)
            { }


        }
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
            
        }
        catch (Exception ex)
        {
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
            
            BindGrid();
          
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
            
        }
        catch (Exception ex)
        {
        }
    }
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
            
        }
        catch (Exception ex)
        {
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
               
            }
            else if (cs[0] == 1 && cs[1] == 1 && cs[2] == 0 && cs[3] == 0)
            {
                BindGrid();
                txtVehicleNumber.Text = "";
                txtTagNo.Text = "";
                
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
          
        }
    }

    protected void getSA()
    {
        cmbSA.Items.Clear();
        

        SqlCommand cmd = new SqlCommand("select 'Service Advisor' EmpName, 0 EmpId union  SELECT EmpName, EmpId FROM tblEmployee WHERE (EmpType IN(SELECT TypeId FROM tblEmployeeType WHERE (EmpType = 'Service Advisor'))) and IsNull(CardNo,0)<>0", conn);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        cmd.CommandType = CommandType.Text;
        if (conn.State != ConnectionState.Open)
            conn.Open();
        sda.Fill(dt);
        conn.Close();
        cmbSA.DataSource = dt;

        cmbSA.DataValueField = "EmpId";
        cmbSA.DataTextField = "EmpName"; cmbSA.DataBind();
    }

    protected void getTL()
    {
        cmbTeamLead.Items.Clear();
        // cmbTeamLead.Items.Add("TeamLead");
       
        SqlCommand cmd = new SqlCommand("select 'TL' EmpName, 0 EmpId union SELECT EmpName, EmpId FROM tblEmployee WHERE (EmpType IN(SELECT TypeId FROM tblEmployeeType WHERE (EmpType = 'Team Lead'))) and CardNo is not null", conn);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        cmd.CommandType = CommandType.Text;
        if (conn.State != ConnectionState.Open)
            conn.Open();
        sda.Fill(dt);
        conn.Close();
        cmbTeamLead.DataSource = dt;
        cmbTeamLead.DataValueField = "EmpId";
        cmbTeamLead.DataTextField = "EmpName"; cmbTeamLead.DataBind();
    }

    //protected void ddl1_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    DataTable dt = new DataTable();
    //    dt = getdata();
    //    int n = dt.Rows.Count;//count of data ffrom database
    //    TextBox[] textBoxes = new TextBox[n];
    //    for (int i = 0; i < n; i++)
    //    {
    //        textBoxes[i] = new TextBox();
    //        textBoxes[i].Text = dt.Rows[i].ToString();
    //        textBoxes[i].Text = dt.Columns[i].ToString();
    //        // Here you can modify the value of the textbox which is at textBoxes[i]}
    //    }
    //    // This adds the controls to the form (you will need to specify thier co-ordinates etc. first)
    //    for (int i = 0; i < n; i++)
    //    {
    //        DynamicText.Controls.Add(textBoxes[i]);

    //    }
    //}

    //protected DataTable getdata()
    //{
    //    DataTable dt = new DataTable();
    //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    //    con.Open();
    //    //Replace with your query or procedure 
    //    SqlCommand cmd = new SqlCommand("select * from tblmaster where slno=@dropdownlistnumber", con);
    //    cmd.Parameters.AddWithValue("@dropdownlistnumber", ddl1.SelectedIndex);
    //    SqlDataAdapter sda = new SqlDataAdapter(cmd);
    //    sda.Fill(dt);
    //    con.Close();
    //    return dt;
    //}
}
