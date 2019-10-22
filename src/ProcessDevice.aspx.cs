using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ProcessDevice : System.Web.UI.Page
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
            if (Session[Session["TMLDealercode"] + "-TMLConString"] == null || Session[Session["TMLDealercode"] + "-TMLConString"].ToString() == "")
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
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        try
        {
            int catid = int.Parse(GridView3.DataKeys[e.RowIndex].Value.ToString());
            string txtdevice = ((TextBox)GridView3.Rows[e.RowIndex].Cells[1].Controls[1]).Text;
            string txtipaddress = ((TextBox)GridView3.Rows[e.RowIndex].Cells[2].Controls[1]).Text;
            string txtmachineid = ((TextBox)GridView3.Rows[e.RowIndex].Cells[3].Controls[1]).Text;
            string drplocname = ((DropDownList)GridView3.Rows[e.RowIndex].Cells[4].Controls[1]).SelectedValue;
            string chkSpeedo = ((System.Web.UI.WebControls.CheckBox)GridView3.Rows[e.RowIndex].Cells[5].Controls[1]).Checked.ToString();
            string EmpType = ((DropDownList)GridView3.Rows[e.RowIndex].Cells[6].Controls[1]).SelectedItem.Text;
            string txtDeviceSN = ((TextBox)GridView3.Rows[e.RowIndex].Cells[7].Controls[1]).Text;
            if (EmpType == "Select")
            {
                EmpType = "";
            }
            
            SqlCommand cmd = new SqlCommand("udpUpdateTblDevices", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SlNo", catid);
            cmd.Parameters.AddWithValue("@DeviceName", txtdevice);
            cmd.Parameters.AddWithValue("@IpAddress", txtipaddress);
            cmd.Parameters.AddWithValue("@MachineId", txtmachineid);
            cmd.Parameters.AddWithValue("@LocationName", drplocname);
            cmd.Parameters.AddWithValue("@IsSpeedoBay", chkSpeedo);
            cmd.Parameters.AddWithValue("@EmpType", EmpType);
            cmd.Parameters.AddWithValue("@DeviceSerialNo", txtDeviceSN);
            SqlParameter msg = cmd.Parameters.Add("@msg", SqlDbType.VarChar, 100);
            SqlParameter Flag = cmd.Parameters.Add("@Flag", SqlDbType.Int);
            msg.Direction = ParameterDirection.Output;
            msg.Value = "";
            Flag.Direction = ParameterDirection.Output;
            Flag.Value = 0;
            con.Open();
            cmd.ExecuteNonQuery();
            if (Flag.Value.ToString() == "0")
            {
                lblmsg1.CssClass = "ErrMsg";
                lblmsg1.Attributes.Add("style", "text-transform:none !important");
                lblmsg1.Text = msg.Value.ToString();
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg1.ClientID + "').style.display='none'\",5000)</script>");
            }
            else
            {
                lblmsg1.CssClass = "ScsMsg";
                lblmsg1.Attributes.Add("style", "text-transform:none !important");
                lblmsg1.Text = msg.Value.ToString();
                GridView3.EditIndex = -1;
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg1.ClientID + "').style.display='none'\",5000)</script>");
                bindgridVal();
            }
           
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg1.ClientID + "').style.display='none'\",5000)</script>");
            lblmsg1.Text = ex.Message;
            lblmsg1.CssClass = "ErrMsg";
            lblmsg1.Attributes.Add("style", "text-transform:none !important");
        }
        finally
        {
            con.Close();
        }
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
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        try
        {
            string str = "select * from tblDevices";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                GridView3.DataSource = dt;
                GridView3.DataBind();
            }
            else
            {
                GridView3.DataSource = this.Get_EmptyDataTable();
                GridView3.DataBind();
                GridView3.Rows[0].Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblmsg1.CssClass = "ErrMsg";
            lblmsg1.Attributes.Add("style", "text-transform:none !important");
            lblmsg1.Text = ex.Message;
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg1.ClientID + "').style.display='none'\",5000)</script>");
        }
        finally
        {
            con.Close();
        }
    }

    protected void GridView3_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        using (SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString()))
        { 
       
        int i = Convert.ToInt32(GridView3.DataKeys[e.RowIndex].Value.ToString());
        string Query = "Delete tblDevices where SlNo= '" + i + "'";
        SqlCommand cmd = new SqlCommand(Query, con);
        con.Open();
        cmd.ExecuteNonQuery();
        bindgridVal();
        con.Close();
        lblmsg1.CssClass = "ScsMsg";
        lblmsg1.Text = "Device record deleted successfully.!";
        lblmsg1.Attributes.Add("style", "text-transform:none !important");
        ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg1.ClientID + "').style.display='none'\",5000)</script>");
        }
    }

    protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());

        try
        {
            if (e.CommandName.Equals("Add"))
            {
               
                TextBox txtdevice = (TextBox)GridView3.FooterRow.FindControl("txtdevice");
                TextBox txtipaddress = (TextBox)GridView3.FooterRow.FindControl("txtipaddress");
                TextBox txtmachineid = (TextBox)GridView3.FooterRow.FindControl("txtmachineid");
                TextBox txtDeviceSN = (TextBox)GridView3.FooterRow.FindControl("txtDeviceSN");
                DropDownList drplocname = (DropDownList)GridView3.FooterRow.FindControl("drplocname");
                System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)GridView3.FooterRow.FindControl("chkSpeedo");
                DropDownList EmpType = (DropDownList)GridView3.FooterRow.FindControl("drpEmpTypeAdd");
                if (EmpType.SelectedValue.ToString() == "Select")
                {
                    EmpType.SelectedItem.Text = "";
                }
                if (txtdevice.Text.Trim() == "")
                {
                    lblmsg1.CssClass = "ErrMsg";
                    lblmsg1.Attributes.Add("style", "text-transform:none !important");
                    lblmsg1.Text = "Please enter device name.!";
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg1.ClientID + "').style.display='none'\",5000)</script>");
                }
                else if (txtipaddress.Text.Trim() == "")
                {
                    lblmsg1.CssClass = "ErrMsg";
                    lblmsg1.Attributes.Add("style", "text-transform:none !important");
                    lblmsg1.Text = "Please enter IP address of the device.!";
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg1.ClientID + "').style.display='none'\",5000)</script>");
                }
                else if (txtmachineid.Text.Trim() == "")
                {
                    lblmsg1.CssClass = "ErrMsg";
                    lblmsg1.Attributes.Add("style", "text-transform:none !important");
                    lblmsg1.Text = "Please enter Machine ID of the device.!";
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg1.ClientID + "').style.display='none'\",5000)</script>");
                }
                else if (txtDeviceSN.Text.Trim() == "")
                {
                    lblmsg1.CssClass = "ErrMsg";
                    lblmsg1.Attributes.Add("style", "text-transform:none !important");
                    lblmsg1.Text = "Please enter device serial no.!";
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg1.ClientID + "').style.display='none'\",5000)</script>");
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("udpInsertTblDevices", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DeviceName", txtdevice.Text.ToString());
                    cmd.Parameters.AddWithValue("@IpAddress", txtipaddress.Text.ToString());
                    cmd.Parameters.AddWithValue("@MachineId", txtmachineid.Text);
                    cmd.Parameters.AddWithValue("@LocationName", drplocname.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@IsSpeedoBay", chk.Checked.ToString());
                    cmd.Parameters.AddWithValue("@EmpType", EmpType.SelectedItem.Text.ToString());
                    cmd.Parameters.AddWithValue("@DeviceSerialNo", txtDeviceSN.Text.ToString());
                    SqlParameter msg = cmd.Parameters.Add("@msg", SqlDbType.VarChar, 100);
                    SqlParameter Flag = cmd.Parameters.Add("@flag", SqlDbType.Int);
                    msg.Direction = ParameterDirection.Output;
                    msg.Value = "";
                    Flag.Direction = ParameterDirection.Output;
                    Flag.Value = 0;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    if (Flag.Value.ToString() == "0")
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg1.ClientID + "').style.display='none'\",5000)</script>");
                        lblmsg1.CssClass = "ErrMsg";
                        lblmsg1.Attributes.Add("style", "text-transform:none !important");
                        lblmsg1.Text = msg.Value.ToString();
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg1.ClientID + "').style.display='none'\",5000)</script>");
                        lblmsg1.CssClass = "ScsMsg";
                        lblmsg1.Attributes.Add("style", "text-transform:none !important");
                        lblmsg1.Text = msg.Value.ToString();
                        bindgridVal();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblmsg1.CssClass = "ErrMsg";
            lblmsg1.Attributes.Add("style", "text-transform:none !important");
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg1.ClientID + "').style.display='none'\",5000)</script>");
            lblmsg1.Text = ex.Message;
        }
        finally
        {
            con.Close();
        }
    }

    protected void GridView3_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
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
       public DataTable Get_EmptyDataTable()
    {
        DataTable dtEmpty = new DataTable();
        //Here ensure that you have added all the column available in your gridview
        dtEmpty.Columns.Add("SLNo", typeof(string));
        dtEmpty.Columns.Add("DeviceName", typeof(string));
        dtEmpty.Columns.Add("IPAddress", typeof(string));
        dtEmpty.Columns.Add("MachineID", typeof(string));
        dtEmpty.Columns.Add("LocationName", typeof(string));
        dtEmpty.Columns.Add("DeviceSlNO", typeof(string));
        dtEmpty.Columns.Add("IsSpeedoBay", typeof(string));
        dtEmpty.Columns.Add("EmployeeType", typeof(string));
        DataRow datatRow = dtEmpty.NewRow();

        //Inserting a new row,datatable .newrow creates a blank row
        dtEmpty.Rows.Add(datatRow);//adding row to the datatable
        return dtEmpty;
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
}