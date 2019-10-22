using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Employee : System.Web.UI.Page
{
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
        modify.Enabled = false;
        btn_delete.Enabled = false;
        getEmpDEtails("searchEmpDEtails");

        if (!IsPostBack)
        {
         
            panel1.Visible = false;
            fillcard();
            MultiView1.ActiveViewIndex = 0;
            getEmpType("GetEmployeeTypeRep", drp_emptype);
            getEmpType("GetEmployeeTypeRep", drp_Emptype1);
            getTeamLead("SelectTeamLead", drp_tlid);
           
            getServiceType_New();
            getvehicleModel();

        }
    }
    protected void getServiceType_New()
    {
        String strConnString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        SqlConnection con = new SqlConnection(strConnString);
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "udpGetServiceTypeForadmin";
        cmd.Connection = con;
        try
        {
            con.Open();
            GridView2.EmptyDataText = "No Records Found";
            GridView2.DataSource = cmd.ExecuteReader();
            GridView2.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
    }
    protected void getServiceTypeByEmpId(int EmpId)
    {
        GridView2.DataSource = null;
        String strConnString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        SqlConnection con = new SqlConnection(strConnString);
        try { 
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "udpGetESTByEmpID";
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@EmpId", EmpId);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Open();
        sda.Fill(dt);
            GridView2.EmptyDataText = "No Records Found";
            GridView2.DataSource = dt;
            GridView2.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
    }
    //protected void OnPaging(object sender, GridViewPageEventArgs e)
    //{
    //    GridView1.PageIndex = e.NewPageIndex;
    //    this.BindGrid();
    //}
    protected void getvehicleModel()
    {
        String strConnString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        SqlConnection con = new SqlConnection(strConnString);
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "GetVehicleModelForEMP";
        cmd.Connection = con;
        try
        {
            con.Open();
            GridView1.EmptyDataText = "No Records Found";
            GridView1.DataSource = cmd.ExecuteReader();
         
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            //throw ex;
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
    }

    protected void getvehicleModelByEmpId(int empid)
    {
        GridView1.DataSource = null;
        String strConnString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        SqlConnection con = new SqlConnection(strConnString);
        try
        {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "udpGetEMByEmpID ";
        cmd.Parameters.AddWithValue("@EmpId", empid);
        cmd.Connection = con;
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Open();
        sda.Fill(dt);
       
            GridView1.EmptyDataText = "No Records Found";
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            //throw ex;
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
    }
    protected DataTable GetModelData()
    {

        using (SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString()))
        {
            SqlCommand cmd = new SqlCommand("GetVehicleModelforAdmin", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da1 = new SqlDataAdapter(cmd);
            DataTable getModel = new DataTable();
            da1.Fill(getModel);
            return getModel;
        }
    }
  

    public void getEmpType(string procedureName, DropDownList drp_emptype)
    {
     
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        try
        {
            DataTable sdt = new DataTable();
            SqlCommand cmd = new SqlCommand(procedureName, con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter ssda = new SqlDataAdapter(cmd);
            con.Open();
            ssda.Fill(sdt);
            drp_emptype.Items.Clear();
            drp_emptype.Items.Add(new ListItem("--Select--","0"));
            ListItem lstItem;

            for (int i = 0; i < sdt.Rows.Count; i++)
            {
                lstItem = new ListItem();
                lstItem.Text = sdt.Rows[i]["EmpType"].ToString();
                lstItem.Value = sdt.Rows[i]["TypeId"].ToString();
                drp_emptype.Items.Add(lstItem);
            }

        }
        catch (Exception ex) { }
        finally
        {
            con.Close();
        }
    }
    protected void getchkModel()
    {
        if (grd_emp.SelectedIndex != -1)
        {
            SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
            try
            {
              
                //  lblempid.Text = grd_emp.SelectedRow.Cells[1].Text.ToString();
                SqlCommand cmd = new SqlCommand("", con);
                DataTable dt = new DataTable();
                cmd = new SqlCommand("udpGetEMByEmpID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpId", lblempid.Text);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                con.Open();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {

                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
        }

    }
    public void getTeamLead(string procedureName, DropDownList drp_tlid)
    {
       SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        try
        {
            DataTable sdt = new DataTable();
            SqlCommand cmd = new SqlCommand(procedureName, con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter ssda = new SqlDataAdapter(cmd);
            con.Open();
            ssda.Fill(sdt);
            drp_tlid.Items.Clear();
            ListItem lstItem;

            for (int i = 0; i < sdt.Rows.Count; i++)
            {
                lstItem = new ListItem();
                lstItem.Text = sdt.Rows[i]["EmpName"].ToString();
                lstItem.Value = sdt.Rows[i]["EmpId"].ToString();
                drp_tlid.Items.Add(lstItem);
            }

        }
        catch (Exception ex) { }
        finally
        {
            con.Close();
        }
    }

    
    protected void drp_emptype_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (drp_emptype.SelectedItem.Text == "Service Advisor")
        //{
        //    txt_crmid.Enabled = true;
        //}
        //else
        //{
        //    txt_crmid.Text = "";
        //    txt_crmid.Enabled = false;
        //}
         if (drp_emptype.SelectedItem.Text == "Electrician" || drp_emptype.SelectedItem.Text=="Mechanic" || drp_emptype.SelectedItem.Text == "Door Technician" || drp_emptype.SelectedItem.Text == "AC Repairing")
        {
           
            getServiceType_New();
            getvehicleModel();
            panel1.Visible = true;
            GridView2.Visible = true;
            GridView1.Visible = true;
        }
        else
        {
            GridView2.Visible = false;
            GridView1.Visible = false;
            panel1.Visible = false;
            GridView1.DataSource = null;
            GridView2.DataSource = null;
            GridView2.DataBind();
            GridView1.DataBind();
         
        }
    }

    protected void modify_Click(object sender, EventArgs e)
    {
        //getServiceType_New();
        lblmsg.CssClass = "reset";
        btn_update.Visible = true;
        btn_submit.Visible = false;
       
        chk_EmpId.Visible = true;
        cmbCardNo.Style.Add("width", "100%");
        existing_div.Visible = true;
        txt_existTagno.Visible = true;
        pnl_empCode.CssClass = "col-md-2";
        if (grd_emp.SelectedIndex != -1)
        {
            SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
            try
            {
                SqlCommand cmd = new SqlCommand("", con);
                lblempid.Text = grd_emp.SelectedRow.Cells[1].Text.ToString();
                DataTable dt = new DataTable();
                cmd = new SqlCommand("udpGetEmpDetailsByEmpID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpId", lblempid.Text.ToString().Trim());
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                con.Open();
                sda.Fill(dt);
                if (dt.Rows.Count != -1)
                {
                    if (MultiView1.ActiveViewIndex > -1)
                    {
                        MultiView1.ActiveViewIndex = 1;
                    }
                    //Empname
                    try
                    {
                        txt_empname.Text = dt.Rows[0][0].ToString();
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show("Name [" + dt.Rows[0][1].ToString() + "] :" + ex.Message.ToString());
                    }
                    //Empcode
                    try
                    {
                        cmbCardNo.Enabled = false;
                        //cmbCardNo.SelectedValue = dt.Rows[0][1].ToString();
                        txt_existTagno.Text = dt.Rows[0][1].ToString();
                    }
                    catch (Exception ex)
                    {

                    }
                    //emptype
                   
                    try
                    {
                        drp_emptype.SelectedValue = dt.Rows[0][2].ToString();

                    }
                    catch (Exception ex)
                    {
                        
                    }
                    if (drp_emptype.SelectedItem.Text == "Electrician" || drp_emptype.SelectedItem.Text == "Mechanic" || drp_emptype.SelectedItem.Text == "Door Technician" || drp_emptype.SelectedItem.Text == "AC Repairing")
                    {
                        panel1.Visible = true;
                        getvehicleModelByEmpId(Convert.ToInt32(lblempid.Text.ToString()));
                        getServiceTypeByEmpId(Convert.ToInt32(lblempid.Text.ToString()));
                    

                    }
                    else if(drp_emptype.SelectedItem.Text=="Service Advisor")
                    {
                        panel1.Visible = false;
                      
                    }
                    else
                    {
                        panel1.Visible = false;
                      
                    }
                    //tlid
                    try
                    {
                        drp_tlid.SelectedValue = dt.Rows[0][3].ToString();
                    }
                    catch (Exception ex)
                    {

                    }
                    //crmuserid
                    try
                    {
                        txt_crmid.Text = dt.Rows[0][4].ToString();
                    }
                    catch (Exception ex)
                    {

                    }
                    //phone
                    try
                    {
                        txt_mobNum.Text = dt.Rows[0][5].ToString();
                    }
                    catch (Exception ex)
                    {

                    }
                    //training
                    try
                    {
                        txt_training.Text = dt.Rows[0][6].ToString();
                    }
                    catch (Exception ex)
                    {

                    }
                    //empcode
                    try
                    {
                        txt_EmpCode.Text= dt.Rows[0][7].ToString();
                    }
                    catch (Exception ex)
                    {

                    }
                   
                   
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
        }


    }

   

    
    protected void btn_close_Click(object sender, EventArgs e)
    {
        lblmsg1.Text = "";
        lblmsg1.CssClass = "reset";
        getEmpDEtails("searchEmpDEtails");
        getServiceType_New();
        //grd_emp.PageIndex = 1;
        btn_add.Enabled = true;
        lblmsg.Text = "";
        clear();
        MultiView1.ActiveViewIndex = 0;
        
    }

    protected void grd_emp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[3].Visible = false;
            //e.Row.Cells[6].Visible = false;
            e.Row.Cells[5].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
            {
          
                for (int i =1; i < e.Row.Cells.Count; i++)
                {
                    if (e.Row.Cells[i].Text.ToString().Contains("1#1") || e.Row.Cells[i].Text.ToString().Contains("1#2"))
                    {
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[i].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/circle_green_small.png' />";
                }
                    else if (e.Row.Cells[i].Text.ToString().Contains("2#1") || e.Row.Cells[i].Text.ToString().Contains("2#2"))
                    {
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[i].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/circle_yellow_small.png' />";
                }
                    else if (e.Row.Cells[i].Text.ToString().Contains("0#1") || e.Row.Cells[i].Text.ToString().Contains("0#2"))
                    {
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[i].Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/circle_red_small.png' />";
                    }
                    else
                    {
                        e.Row.Cells[i].Text = e.Row.Cells[i].Text;
                    }
                    
            }
        }
    }
    protected void grd_emp_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblmsg1.CssClass = "reset";
        lblmsg1.Text = "";
        try
        {
            if (grd_emp.Rows.Count > 0)
            {
                lblempid.Text = grd_emp.SelectedRow.Cells[1].Text.ToString();
                btn_delete.Enabled = true;
                modify.Enabled = true;
                btn_add.Enabled = false;
            }
        }
        catch (Exception ex) { }
    }

    protected void grd_emp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        lblmsg1.CssClass = "reset";
        lblmsg1.Text = "";
        grd_emp.PageIndex = e.NewPageIndex;
        getEmpDEtails("searchEmpDEtails");
    }

   

    private void fillcard()
    {
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlCommand cmd = new SqlCommand("SELECT EnrollmentNo FROM tblRFID where EmpID IS NULL AND Reserved='1' order by EnrollmentNo", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        try
        {
            con.Open();
            da.Fill(dt);
            if (dt.Rows.Count != 0)
            {
                cmbCardNo.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cmbCardNo.Items.Add(new ListItem(dt.Rows[i]["EnrollmentNo"].ToString(), dt.Rows[i]["EnrollmentNo"].ToString()));
                }
            }
            else
            {
                cmbCardNo.Items.Clear();
            }
        }
        catch (Exception ex)
        {
        }
        finally
        {
            con.Close();
        }
    }

    protected void btn_add_Click(object sender, EventArgs e)
    {
        panel1.Visible = false;
        btn_update.Visible = false;
        btn_submit.Visible = true;
        lblmsg.Text = "";
        lblmsg.CssClass = "reset";
        cmbCardNo.Enabled = true;
        chk_EmpId.Visible = false;
        cmbCardNo.Style.Add("width", "100%");
        MultiView1.ActiveViewIndex = 1;
        existing_div.Visible = false;
        txt_existTagno.Visible = false;
        pnl_empCode.CssClass = "col-md-4";
        fillcard();
    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        if (txt_empname.Text == "")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
            lblmsg.CssClass = "ErrMsg";
            lblmsg.Text = "Please enter employee name";
            lblmsg.Attributes.Add("style", "text-transform:none !important");
        }
        else if (txt_EmpCode.Text == "")
        {
            lblmsg.CssClass = "ErrMsg";
            lblmsg.Text = "Please enter employee code";
            lblmsg.Attributes.Add("style", "text-transform:none !important");
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
        }
        else if (txt_mobNum.Text != "" && txt_mobNum.Text.Length < 10)
        {
            lblmsg.CssClass = "ErrMsg";
            lblmsg.Attributes.Add("style", "text-transform:none !important");
            lblmsg.Text = "Invalid mobile number";
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

        }
        else if (drp_emptype.SelectedItem.Text == "Mechanic" && drp_tlid.SelectedItem.Text == "--Select--")
        {
            lblmsg.CssClass = "ErrMsg";
            lblmsg.Text = "Please select Team Lead";
            lblmsg.Attributes.Add("style", "text-transform:none !important");
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

        }
        else {
            SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
            try
        {

         
            SqlCommand cmd = new SqlCommand("GetEmployeeAssigned", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@cardno", cmbCardNo.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@EmpName", txt_empname.Text.ToString());
            cmd.Parameters.AddWithValue("@EmpTypeId", drp_emptype.SelectedValue);
            cmd.Parameters.AddWithValue("@emptype", drp_emptype.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@IsBodyshop", 0);
            cmd.Parameters.AddWithValue("@CRMUserId", txt_crmid.Text.ToString());
            cmd.Parameters.AddWithValue("@PhoneNo", txt_mobNum.Text.ToString());
            cmd.Parameters.AddWithValue("@EmpCode", txt_EmpCode.Text.ToString());
            //cmd.Parameters.AddWithValue("@Training", txt_training.SelectedItem.Text.ToString());


            if (drp_emptype.SelectedItem.Text == "Electrician" || drp_emptype.SelectedItem.Text == "Mechanic" || drp_emptype.SelectedItem.Text == "Door Technician" || drp_emptype.SelectedItem.Text == "AC Repairing")
            {
                cmd.Parameters.AddWithValue("@TeamLeadId", drp_tlid.SelectedValue);
                cmd.Parameters.AddWithValue("@Training", txt_training.SelectedItem.Text.ToString());
                }
            else
            {
                cmd.Parameters.AddWithValue("@TeamLeadId", 0);
                cmd.Parameters.AddWithValue("@Training", DBNull.Value);  //Fixed by Pratik for GM traning value issue

            }
                
            SqlParameter spm = cmd.Parameters.Add("@msg", SqlDbType.VarChar, 50);
            SqlParameter spm1 = cmd.Parameters.Add("@flag", SqlDbType.Int);

            spm.Direction = ParameterDirection.Output;
            spm1.Direction = ParameterDirection.Output;
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            lblmsg.Text = spm.Value.ToString();
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

                if (spm1.Value.ToString() != "0")
                {
                    lblmsg.CssClass = "ScsMsg";
                    lblmsg.Text = spm.Value.ToString();
                    lblmsg.Attributes.Add("style", "text-transform:none !important");
                    if (drp_emptype.SelectedItem.Text == "Electrician" || drp_emptype.SelectedItem.Text == "Mechanic" || drp_emptype.SelectedItem.Text == "Door Technician" || drp_emptype.SelectedItem.Text == "AC Repairing")
                {
                    InsertEMPWithST(Convert.ToInt16(spm1.Value), txt_empname.Text.ToString());
                    InsertEMPWithModel(Convert.ToInt16(spm1.Value), txt_empname.Text.ToString());
                       

                    }
                    lblmsg.CssClass = "ScsMsg";
                    lblmsg.Attributes.Add("style", "text-transform:none !important");
                    lblmsg.Text = spm.Value.ToString();
                    clear();
                    GridView2.Visible = false;
                    GridView1.Visible = false;
                    panel1.Visible = false;
                    GridView1.DataSource = null;
                    GridView2.DataSource = null;
                    //getServiceType_New();
                    //getvehicleModel();
                }
            else
            {
                lblmsg.CssClass = "ErrMsg";
                    lblmsg.Attributes.Add("style", "text-transform:none !important");
                    lblmsg.Text = spm.Value.ToString();
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
            }
                lblmsg.Text = spm.Value.ToString();
                lblmsg.Attributes.Add("style", "text-transform:none !important");

            }
        catch (Exception ex)
        {

        }
            finally
            {
                con.Close();
            }
    }
    }
    public void Afterupdate()
    {
        chk_EmpId.Visible = false;
        cmbCardNo.Enabled = true;
        pnl_empCode.CssClass = "col-md-4";
        existing_div.Visible = false;
        btn_update.Visible = false;
        btn_submit.Visible = true;

    }

    protected void btn_update_Click(object sender, EventArgs e)
    {
        if (drp_emptype.SelectedItem.Text == "Mechanic" && drp_tlid.SelectedItem.Text == "--Select--")
        {
            lblmsg.Text = "Please select Team Lead";
            lblmsg.Attributes.Add("style", "text-transform:none");
            lblmsg.CssClass = "ErrMsg";
            lblmsg.Attributes.Add("style", "text-transform:none !important");
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

        }
        else if ((drp_emptype.SelectedItem.Text == "Mechanic" || drp_emptype.SelectedItem.Text == "Electrician" || drp_emptype.SelectedItem.Text == "Door Technician" || drp_emptype.SelectedItem.Text == "AC Repairing") && txt_training.SelectedItem.Text == "--Select--")
        {
            lblmsg.CssClass = "ErrMsg";
            lblmsg.Text = "Please select Training level";
            lblmsg.Attributes.Add("style", "text-transform:none !important");
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");

        }
        else
        {
            SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
            try
            {
                string change = "";
                if (chk_EmpId.Checked)
                    change = "Yes";
                else
                    change = "No";
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("UpdateTblEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpID", lblempid.Text.ToString());
                cmd.Parameters.AddWithValue("@EmpName", txt_empname.Text.ToString());
                cmd.Parameters.AddWithValue("@EmpTypeId", drp_emptype.SelectedValue);
                cmd.Parameters.AddWithValue("@EmpType", drp_emptype.SelectedItem.Text);
                if (chk_EmpId.Checked)
                {

                    cmd.Parameters.AddWithValue("@NewTagNo", cmbCardNo.SelectedValue);
                    cmd.Parameters.AddWithValue("@changeTag", change);
                    cmd.Parameters.AddWithValue("@OldTagNo", txt_existTagno.Text);
                }

                else
                {
                    cmd.Parameters.AddWithValue("@OldTagNo", txt_existTagno.Text);
                    cmd.Parameters.AddWithValue("@NewTagNo", txt_existTagno.Text);
                    cmd.Parameters.AddWithValue("@changeTag", change);

                }
                cmd.Parameters.AddWithValue("@CRMUserId", txt_crmid.Text.ToString());
                cmd.Parameters.AddWithValue("@PhoneNo", txt_mobNum.Text.ToString());
                cmd.Parameters.AddWithValue("@EmpCode", txt_EmpCode.Text.ToString());
                if (drp_emptype.SelectedItem.Text == "Electrician" || drp_emptype.SelectedItem.Text == "Mechanic" || drp_emptype.SelectedItem.Text == "Door Technician" || drp_emptype.SelectedItem.Text == "AC Repairing")
                {
                    cmd.Parameters.AddWithValue("@TeamLeadId", drp_tlid.SelectedValue);
                    cmd.Parameters.AddWithValue("@Training", txt_training.SelectedItem.Text.ToString());
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Training", "");
                    cmd.Parameters.AddWithValue("@TeamLeadId", 0);
                }
                //  cmd.Parameters.AddWithValue("@TeamLeadId", drp_tlid.SelectedValue);
                SqlParameter spm = cmd.Parameters.Add("@msg", SqlDbType.VarChar, 50);
                spm.Direction = ParameterDirection.Output;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                if (spm.Value.ToString().Contains("Successfully"))
                {
                    if (drp_emptype.SelectedItem.Text == "Electrician" || drp_emptype.SelectedItem.Text == "Mechanic" || drp_emptype.SelectedItem.Text == "Door Technician" || drp_emptype.SelectedItem.Text == "AC Repairing")
                    {
                        DeleteEmpST(lblempid.Text.ToString());
                        int EMPID = Convert.ToInt16(lblempid.Text);
                        InsertEMPWithST(EMPID, txt_empname.Text.ToString());
                        InsertEMPWithModel(EMPID, txt_empname.Text.ToString());
                        clear();
                    }
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                    lblmsg.CssClass = "ScsMsg";
                    lblmsg.Attributes.Add("style", "text-transform:none !important");
                    lblmsg.Text = spm.Value.ToString();
                    clear();
                    GridView2.Visible = false;
                    GridView1.Visible = false;
                    panel1.Visible = false;
                    GridView1.DataSource = null;
                    GridView2.DataSource = null;
                    Afterupdate();
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                    lblmsg.CssClass = "ErrMsg";
                    lblmsg.Text = spm.Value.ToString();
                    lblmsg.Attributes.Add("style", "text-transform:none !important");
                }


            }

            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
        }
    }

    protected void DeleteEmpST(string empid)
    {
        using (SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString()))
        {
            SqlCommand cmd = new SqlCommand("udpDeleteEMPST", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmpId", empid);
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
        }
    }
    
    protected void InsertEMPWithST(int empid, string empname)
    {
        for (int i = 0; i < GridView2.Rows.Count; i++)
        {
            DropDownList tb = (DropDownList)(GridView2.Rows[i].FindControl("ddl_AddService"));
            string val = tb.SelectedValue;
          
        }
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        try
        {
            for (int i = 0; i < GridView2.Rows.Count; i++)
            {
                DropDownList tb = (DropDownList)(GridView2.Rows[i].FindControl("ddl_AddService"));
                string val = tb.SelectedValue;
                SqlCommand cmd = new SqlCommand("UdpInsertEMPWithST", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpId", empid);
                cmd.Parameters.AddWithValue("@EmpName", empname);
                cmd.Parameters.AddWithValue("@STID", GridView2.Rows[i].Cells[0].Text.Trim());
                cmd.Parameters.AddWithValue("@STShotCode", GridView2.Rows[i].Cells[2].Text.Trim());
                if (tb.Text.Trim() == "-1")
                {
                    cmd.Parameters.AddWithValue("@Status", 1);
                }
                else if (tb.Text.Trim() == "0")
                {
                    cmd.Parameters.AddWithValue("@Status", 0);
                }
                else if (tb.Text.Trim() == "1")
                {
                    cmd.Parameters.AddWithValue("@Status", 1);
                }
                else if (tb.Text.Trim() == "2")
                {
                    cmd.Parameters.AddWithValue("@Status", 2);
                }


                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
            }
                
        }
        catch (Exception ex)
        {
            con.Close();
        }
        finally
        {
            con.Close();
        }
    }

    private bool isNumeric(string text, NumberStyles integer)
    {
        throw new NotImplementedException();
    }

    

    protected void InsertEMPWithModel(int empid, string empname)
    {
        //for (int i=0;i<GridView1.Rows.Count;i++)
        //{
        //    DropDownList dl = (DropDownList)(GridView1.Rows[i].FindControl("ddl_AddModel"));
        //}

        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        try
        {

            for(int i=0;i<GridView1.Rows.Count;i++)
            {
                DropDownList dl = (DropDownList)(GridView1.Rows[i].FindControl("ddl_AddModel"));
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("UdpInsertEMPWithModel", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmpId", empid);
                    cmd.Parameters.AddWithValue("@EmpName", empname);
                    cmd.Parameters.AddWithValue("@ModelID", GridView1.Rows[i].Cells[0].Text);
                    cmd.Parameters.AddWithValue("@Model", GridView1.Rows[i].Cells[1].Text);  //Edited by Pratik
                if (dl.Text.Trim() == "-1")
                {
                    cmd.Parameters.AddWithValue("@Status", 1);
                }
                else if (dl.Text.Trim() == "0")
                {
                    cmd.Parameters.AddWithValue("@Status", 0);
                }
                else if (dl.Text.Trim() == "1")
                {
                    cmd.Parameters.AddWithValue("@Status", 1);
                }
                else if (dl.Text.Trim() == "2")
                {
                    cmd.Parameters.AddWithValue("@Status", 2);
                }


                if (con.State != ConnectionState.Open)
                {
                   con.Open();
                }
                cmd.ExecuteNonQuery();
                
            }
        }
        catch (Exception ex)
        {

        }
        finally
        {
            con.Close();
        }

    }

    public  int getEmpCardNO(int Empid)
    {
        using (SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString()))
        {

        SqlCommand cmd = new SqlCommand("select CardNo from tblemployee  where EmpId=@Empid", con);
        cmd.Parameters.AddWithValue("@Empid", Empid);
        DataTable dt = new DataTable();
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
            con.Open();
         sda.Fill(dt);
        return Convert.ToInt16(dt.Rows[0][0]);
        }

    }

  
    protected void btn_delete_Click(object sender, EventArgs e)
    {
        lblmsg1.Attributes.Add("style", "text-transform:none !important");
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        try
        {
                int empCardNO = getEmpCardNO(Convert.ToInt32(lblempid.Text.Trim()));
                SqlCommand cmd = new SqlCommand("UnassignTblEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpID", lblempid.Text.Trim());
                cmd.Parameters.AddWithValue("@OldTagNo", empCardNO);
                cmd.Parameters.AddWithValue("@EmpType", drp_emptype.SelectedItem.Text.ToString());
                SqlParameter spm = cmd.Parameters.Add("@RetVal", SqlDbType.VarChar, 30);
                spm.Direction = ParameterDirection.Output;
                spm.Value = "";
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd.ExecuteNonQuery();
                lblmsg1.CssClass = "ScsMsg";
            lblmsg.Attributes.Add("style", "text-transform:none !important");
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg1.ClientID + "').style.display='none'\",5000)</script>");

            lblmsg1.Text = spm.Value.ToString();
                btn_add.Enabled = true;
            getEmpDEtails("searchEmpDEtails");
        }
        catch (Exception ex)
        { }
        finally
        {
            con.Close();
        }
    }

   

    protected void btn_reset_Click(object sender, EventArgs e)
    {
        lblmsg1.Text = "";
        lblmsg1.CssClass = "reset";
        btn_update.Visible = false;
        btn_submit.Visible = true;
        getvehicleModel();
        getServiceType_New();
        existing_div.Visible = false;
        pnl_empCode.CssClass = "col-md-4";
        lblmsg.Text = "";
        lblmsg.CssClass = "reset";
        clear();
    }
    public void clear()
    {
        
        txt_EmpCode.Text = "";
        txt_empname.Text = "";
        txt_mobNum.Text = "";
        txt_training.SelectedIndex = 0;
        txt_crmid.Text = "";
          drp_tlid.SelectedIndex = -1;
        drp_emptype.SelectedIndex = -1;
        fillcard();

    }
    //lblmsg1
    protected void chk_EmpId_CheckedChanged(object sender, EventArgs e)
    {
        if (chk_EmpId.Checked)
        {
            cmbCardNo.Enabled = true;
        }
        else
        {
            cmbCardNo.Enabled = false;
        }
    }

    protected void grd_emp_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        lblmsg1.CssClass = "reset";
        lblmsg1.Text = "";
        lblmsg.Text = "";
        lblmsg.CssClass = "reset";
        grd_emp.PageIndex = e.NewPageIndex;
        getEmpDEtails("searchEmpDEtails");
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        lblmsg1.CssClass = "reset";
        lblmsg1.Text = "";
        GridView1.PageIndex = e.NewPageIndex;
        getvehicleModel();
    }

    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        lblmsg1.CssClass = "reset";
        lblmsg1.Text = "";
        GridView2.PageIndex = e.NewPageIndex;
        getServiceType_New();

    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        getvehicleModel();
    }
  
 

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager)
        {
           // lblsrvcid.Text = GridView2.SelectedRow.Cells[1].Text.ToString();
            e.Row.Cells[0].Visible = false;

        }
    }

    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {
        getServiceType_New();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            // lblsrvcid.Text = GridView2.SelectedRow.Cells[1].Text.ToString();
            e.Row.Cells[0].Visible = false;

        }
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        getEmpDEtails("searchEmpDEtails");
    }

    private void getEmpDEtails(string procedure)
    {
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        try
        {
            DataTable dt = new DataTable();
            if (procedure == "UdpGetEmpDetails_New")
            {
                SqlCommand cmd = new SqlCommand(procedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                sda.Fill(dt);
                grd_emp.DataSource = dt;
                grd_emp.DataBind();
                con.Close();
            }
            else if (procedure == "searchEmpDEtails")
            {
                SqlCommand cmd = new SqlCommand(procedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EMPNAME", txtEmpname.Text.ToString());
                if (drp_Emptype1.SelectedValue.Trim() == "--Select--" || drp_Emptype1.SelectedValue.Trim() == "" || drp_Emptype1.SelectedValue.Trim() == "0")
                {
                    cmd.Parameters.AddWithValue("@emptype", "");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@emptype", drp_Emptype1.SelectedItem.Text.Trim());
                }

                cmd.Parameters.AddWithValue("@empcode", txt_empcode1.Text.ToString());
             
                cmd.Parameters.AddWithValue("@CardNo", TextBox5.Text);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                sda.Fill(dt);
                grd_emp.DataSource = dt;
                grd_emp.DataBind();
                con.Close();
               // clear1();
            }
        }
        catch (Exception ex) { }
        finally
        {
            if(con.State != ConnectionState.Closed)
            { 
            con.Close();
            }
        }
    }
    public void clear1()
    {
     //   txtEmpname.Text = "";
        // drp_Emptype1.SelectedIndex = 0;
       // drp_level1.SelectedIndex = 0;
        txt_empcode1.Text = "";
        TextBox5.Text = "";
    }
    protected void btn_reset1_Click(object sender, EventArgs e)
    {
        drp_Emptype1.SelectedIndex = 0;
        clear1();
        getEmpDEtails("searchEmpDEtails");
    }
    protected void drp_Emptype1_SelectedIndexChanged(object sender, EventArgs e)
    {
        getEmpDEtails("searchEmpDEtails");
    }
}