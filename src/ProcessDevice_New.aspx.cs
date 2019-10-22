using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ProcessDevice_New : System.Web.UI.Page
{
    private DataTable dt;
    private SqlDataAdapter sda;
    private SqlCommand cmd;
    //private SqlConnection con = new SqlConnection(DataManager.ConStr);
    private static int catid1;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["ConnectionString"] == null || Session["ConnectionString"].ToString() == "")
                Response.Redirect("login.aspx");

        }
        catch
        {
            Response.Redirect("login.aspx");
        }


        try
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    Session["CURRENT_PAGE"] = "Device Management";
                }
                catch (Exception ex)
                {
                }
                bindgridVal();

                lblmsg1.Text = "";
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("login.aspx");
        }
    }

    protected void GridView3_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        SqlConnection con = new SqlConnection(Session["ConnectionString"].ToString());
        try
        {
            int catid = int.Parse(GridView3.DataKeys[e.RowIndex].Value.ToString());
            string txtdevice = ((TextBox)GridView3.Rows[e.RowIndex].Cells[1].Controls[1]).Text;
            string txtipaddress = ((TextBox)GridView3.Rows[e.RowIndex].Cells[2].Controls[1]).Text;
            string txtmachineid = ((TextBox)GridView3.Rows[e.RowIndex].Cells[3].Controls[1]).Text;
            string drplocname = ((DropDownList)GridView3.Rows[e.RowIndex].Cells[5].Controls[1]).SelectedValue;
            string chkSpeedo = ((System.Web.UI.WebControls.CheckBox)GridView3.Rows[e.RowIndex].Cells[6].Controls[1]).Checked.ToString();
            string EmpType = ((DropDownList)GridView3.Rows[e.RowIndex].Cells[7].Controls[1]).SelectedItem.Text;
            string txtDeviceSN = ((TextBox)GridView3.Rows[e.RowIndex].Cells[8].Controls[1]).Text;

            string chk_Isbodyshop = ((System.Web.UI.WebControls.CheckBox)GridView3.Rows[e.RowIndex].Cells[4].Controls[1]).Checked.ToString();
            if (EmpType == "Select")
            {
                EmpType = "";
            }
            con.Open();
            SqlCommand cmd = new SqlCommand("udpUpdateTblDevices_New", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SlNo", catid);
            cmd.Parameters.AddWithValue("@DeviceName", txtdevice);
            cmd.Parameters.AddWithValue("@IpAddress", txtipaddress);
            cmd.Parameters.AddWithValue("@MachineId", txtmachineid);
            cmd.Parameters.AddWithValue("@LocationName", drplocname);
            cmd.Parameters.AddWithValue("@IsSpeedoBay", chkSpeedo);
            cmd.Parameters.AddWithValue("@EmpType", EmpType);
            cmd.Parameters.AddWithValue("@DeviceSerialNo", txtDeviceSN);
             if (chk_Isbodyshop.Contains("1"))
                    {
                        cmd.Parameters.AddWithValue("@isBodyshop", 1);
                        cmd.Parameters.AddWithValue("@LocationName", drplocname);
                    }
            else
            {
                cmd.Parameters.AddWithValue("@isBodyshop", 0);
                cmd.Parameters.AddWithValue("@LocationName", drplocname);
            }
            cmd.Parameters.AddWithValue("@IsBodyshop", chk_Isbodyshop);
            SqlParameter msg = cmd.Parameters.Add("@msg", SqlDbType.VarChar, 100);
            SqlParameter Flag = cmd.Parameters.Add("@Flag", SqlDbType.Int);
            msg.Direction = ParameterDirection.Output;
            msg.Value = "";
            Flag.Direction = ParameterDirection.Output;
            Flag.Value = 0;
            cmd.ExecuteNonQuery();
            if (Flag.Value.ToString() == "0")
            {
                lblmsg1.CssClass = "ErrMsg";
                lblmsg1.Text = msg.Value.ToString();
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg1.ClientID + "').style.display='none'\",5000)</script>");
            }
            else
            {
                lblmsg1.CssClass = "ScsMsg";
                lblmsg1.Text = msg.Value.ToString();
                GridView3.EditIndex = -1;
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg1.ClientID + "').style.display='none'\",5000)</script>");
                bindgridVal();
            }
            con.Close();
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg1.ClientID + "').style.display='none'\",5000)</script>");
            lblmsg1.Text = ex.Message;
            lblmsg1.CssClass = "ErrMsg";
        }
    }

    private void ShowNoResultFounds(DataTable source, GridView GridView3)
    {
        //source.Rows.Add(source.NewRow());
        //GridView3.DataSource = source;
        //GridView3.DataBind();
        //int columnsCount = GridView3.Columns.Count;
        //GridView3.Rows[0].Cells.Clear();
        //GridView3.Rows[0].Cells.Add(new TableCell());
        //GridView3.Rows[0].Cells[0].ColumnSpan = columnsCount;
        //GridView3.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
        //GridView3.Rows[0].Cells[0].ForeColor = System.Drawing.Color.Red;
        //GridView3.Rows[0].Cells[0].Font.Bold = true;
        //GridView3.Rows[0].Cells[0].Text = "NO RESULT FOUND!";
    }

    protected void GridView3_RowEditing(object sender, GridViewEditEventArgs e)
    {
        lblmsg1.CssClass = "reset";
        lblmsg1.Text = "";
        catid1 = e.NewEditIndex;
        GridView3.EditIndex = e.NewEditIndex;
        bindgridVal();
    }

    private void bindgridVal()
    {
        SqlConnection con = new SqlConnection(Session["ConnectionString"].ToString());
        try
        {
            string str = "select * from tblDevices";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                GridView3.DataSource = dt;
                GridView3.DataBind();
            }
            else
            {
                // ShowNoResultFounds(dt, GridView3);
            }
          
        }
        catch (Exception ex)
        {
            //lblmsg1.CssClass = "ErrMsg";
            //lblmsg1.Text = ex.Message;
            //ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg1.ClientID + "').style.display='none'\",5000)</script>");
        }
    }

    protected void GridView3_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SqlConnection con = new SqlConnection(Session["ConnectionString"].ToString());
        con.Open();
        int i = Convert.ToInt32(GridView3.DataKeys[e.RowIndex].Value.ToString());
        string Query = "Delete tblDevices where SlNo= '" + i + "'";
        SqlCommand cmd = new SqlCommand(Query, con);
        cmd.ExecuteNonQuery();
        bindgridVal();
        con.Close();
        lblmsg1.CssClass = "ScsMsg";
        lblmsg1.Text = "Device Record Deleted Successfully.!";
        ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg1.ClientID + "').style.display='none'\",5000)</script>");
    }

    protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        SqlConnection con = new SqlConnection(Session["ConnectionString"].ToString());

        try
        {
            if (e.CommandName.Equals("Add"))
            {
                con.Open();
                TextBox txtdevice = (TextBox)GridView3.FooterRow.FindControl("txtdevice");
                TextBox txtipaddress = (TextBox)GridView3.FooterRow.FindControl("txtipaddress");
                TextBox txtmachineid = (TextBox)GridView3.FooterRow.FindControl("txtmachineid");
                TextBox txtDeviceSN = (TextBox)GridView3.FooterRow.FindControl("txtDeviceSN");
                DropDownList drplocname = (DropDownList)GridView3.FooterRow.FindControl("drplocname");
                DropDownList drplocname1 = (DropDownList)GridView3.FooterRow.FindControl("DropDownList2");
                System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)GridView3.FooterRow.FindControl("chkSpeedo");
                DropDownList EmpType = (DropDownList)GridView3.FooterRow.FindControl("drpEmpTypeAdd");
                System.Web.UI.WebControls.CheckBox chk_Isbodyshop = (System.Web.UI.WebControls.CheckBox)GridView3.FooterRow.FindControl("chk_isbodyshop");
                if (EmpType.SelectedValue.ToString() == "Select")
                {
                    EmpType.SelectedItem.Text = "";
                }
                if (txtdevice.Text.Trim() == "")
                {
                    lblmsg1.CssClass = "ErrMsg";
                    lblmsg1.Text = "Please Enter Device Name.!";
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg1.ClientID + "').style.display='none'\",5000)</script>");
                }
                else if (txtipaddress.Text.Trim() == "")
                {
                    lblmsg1.CssClass = "ErrMsg";
                    lblmsg1.Text = "Please Enter IP Address Of The Device.!";
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg1.ClientID + "').style.display='none'\",5000)</script>");
                }
                else if (txtmachineid.Text.Trim() == "")
                {
                    lblmsg1.CssClass = "ErrMsg";
                    lblmsg1.Text = "Please Enter Machine ID Of The Device.!";
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg1.ClientID + "').style.display='none'\",5000)</script>");
                }
                else if (txtDeviceSN.Text.Trim() == "")
                {
                    lblmsg1.CssClass = "ErrMsg";
                    lblmsg1.Text = "Please Enter Device Serial no.!";
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg1.ClientID + "').style.display='none'\",5000)</script>");
                }
                else if (drplocname.Text.ToString() == "--Select--")
                {
                    lblmsg1.CssClass = "ErrMsg";
                    lblmsg1.Text = "Please Select Location Name.!";
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg1.ClientID + "').style.display='none'\",5000)</script>");

                }
                else
                {
                    SqlCommand cmd = new SqlCommand("udpInsertTblDevices_New", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DeviceName", txtdevice.Text.ToString());
                    cmd.Parameters.AddWithValue("@IpAddress", txtipaddress.Text.ToString());
                    cmd.Parameters.AddWithValue("@MachineId", txtmachineid.Text);
                    
                    cmd.Parameters.AddWithValue("@IsSpeedoBay", chk.Checked.ToString());
                    cmd.Parameters.AddWithValue("@EmpType", EmpType.SelectedItem.Text.ToString());
                    cmd.Parameters.AddWithValue("@DeviceSerialNo", txtDeviceSN.Text.ToString());
                    if (chk_Isbodyshop.Checked==true)
                    {
                        cmd.Parameters.AddWithValue("@isBodyshop", 1);
                        cmd.Parameters.AddWithValue("@LocationName", drplocname1.SelectedValue.ToString());
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@LocationName", drplocname.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@isBodyshop", 0);
                    }
                    
                    SqlParameter msg = cmd.Parameters.Add("@msg", SqlDbType.VarChar, 100);
                    SqlParameter Flag = cmd.Parameters.Add("@flag", SqlDbType.Int);
                    msg.Direction = ParameterDirection.Output;
                    msg.Value = "";
                    Flag.Direction = ParameterDirection.Output;
                    Flag.Value = 0;
                    cmd.ExecuteNonQuery();
                    if (Flag.Value.ToString() == "0")
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg1.ClientID + "').style.display='none'\",5000)</script>");
                        lblmsg1.CssClass = "ErrMsg";
                        lblmsg1.Text = msg.Value.ToString();
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg1.ClientID + "').style.display='none'\",5000)</script>");
                        lblmsg1.CssClass = "ScsMsg";
                        lblmsg1.Text = msg.Value.ToString();
                        bindgridVal();
                    }
                    con.Close();
                }
            }
        }
        catch (Exception ex)
        {
            lblmsg1.CssClass = "ErrMsg";
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg1.ClientID + "').style.display='none'\",5000)</script>");
            lblmsg1.Text = ex.Message;
        }
    }

    protected void GridView3_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        lblmsg1.CssClass = "reset";
        lblmsg1.Text = "";
        GridView3.EditIndex = -1;
        bindgridVal();
    }

    protected void drplocname_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblmsg1.CssClass = "reset";
        lblmsg1.Text = "";
        DropDownList drplocname = (DropDownList)GridView3.FooterRow.FindControl("drplocname");
        System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)GridView3.FooterRow.FindControl("chkSpeedo");
        if (drplocname.SelectedValue == "Workshop")
        {
            chk.Visible = true;
            chk.Checked = true;
        }
        else
        {
            chk.Visible = false;
            chk.Enabled = false;
            chk.Checked = false;
        }
    }

    protected void drplocnameEdit_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList drplocname = (DropDownList)GridView3.Rows[catid1].Cells[4].Controls[1];
        System.Web.UI.WebControls.CheckBox chk1 = (System.Web.UI.WebControls.CheckBox)GridView3.Rows[catid1].Cells[5].Controls[1];
        if (drplocname.SelectedValue == "Workshop")
        {
            chk1.Visible = true;
            chk1.Enabled = true;
            chk1.Checked = true;
        }
        else
        {
            chk1.Visible = false;
            chk1.Enabled = false;
            chk1.Checked = false;
        }
    }
    public void getBodyshopDevices(string procedureName, DropDownList drp_devices)
    {

        SqlConnection con = new SqlConnection(Session["ConnectionString"].ToString());
        try
        {
            DataTable sdt = new DataTable();
            SqlCommand cmd = new SqlCommand(procedureName, con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter ssda = new SqlDataAdapter(cmd);
            ssda.Fill(sdt);
            drp_devices.Items.Clear();
            drp_devices.Items.Add(new ListItem("--Select--", "0"));
            ListItem lstItem;

            for (int i = 0; i < sdt.Rows.Count; i++)
            {
                lstItem = new ListItem();
                lstItem.Text = sdt.Rows[i]["DeviceName"].ToString();
                lstItem.Value = sdt.Rows[i]["SLNo"].ToString();
                drp_devices.Items.Add(lstItem);
            }

        }
        catch (Exception ex) { }
    }

    protected void chk_isbodyshop_CheckedChanged(object sender, EventArgs e)
    {
        lblmsg1.CssClass = "reset";
        lblmsg1.Text = "";
        System.Web.UI.WebControls.CheckBox chk_bodyshop = (System.Web.UI.WebControls.CheckBox)GridView3.FooterRow.FindControl("chk_isbodyshop");
        if (chk_bodyshop.Checked == true)
        {

        DropDownList drp = (DropDownList)GridView3.FooterRow.FindControl("DropDownList2");
            drp.Visible = true;
            DropDownList drp1 = (DropDownList)GridView3.FooterRow.FindControl("drplocname");
            drp1.Visible = false;
            //drp.Items.Clear();
            //getBodyshopDevices("devices_bodyshop", drp);
        }
        else
        {
            DropDownList drp = (DropDownList)GridView3.FooterRow.FindControl("DropDownList2");
            drp.Visible = false;
            DropDownList drp1 = (DropDownList)GridView3.FooterRow.FindControl("drplocname");
            drp1.Visible = true;
            //DropDownList drp = (DropDownList)GridView3.FooterRow.FindControl("drplocname");
            //drp.Items.Clear();
            //getBodyshopDevices("devices", drp);
        }
    }
}