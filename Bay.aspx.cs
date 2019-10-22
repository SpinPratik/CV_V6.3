using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;


public partial class Bay : System.Web.UI.Page
{
    private static ArrayList serviceidList10 = new ArrayList();
    private DataTable dt;
    private SqlDataAdapter sda;
    private SqlCommand cmd;
    //private SqlConnection con = new SqlConnection(DataManager.ConStr);

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
        SqlDataSource2.ConnectionString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        
        try
        { 
            if (!Page.IsPostBack)
            {
               
                try
                {
                    Session["CURRENT_PAGE"] = "Bay";
                }
                catch (Exception ex)
                {
                }
                FillBay();
                FillFloor();
                getTeamLead("GetTeamLead", drp_tlid);
                serviceidList10.Clear();
                btn_BayUpdate.Visible = false;
                lbl_msg0.Text = "";
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("login.aspx");
        }
    }

    protected void getTeamLead(string procName, DropDownList drp_tlid)
    {
      
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        try
        {
            DataTable sdt = new DataTable();
            SqlCommand cmd = new SqlCommand(procName, con);
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

    protected void chk_NewBayType_CheckedChanged(object sender, EventArgs e)
    {
        //if (chk_NewBayType.Checked == true)
        //{
        //    dd_BayType.Visible = false;
        //    txt_NewBayType.Visible = true;
        //}
        //else
        //{
        //    txt_NewBayType.Visible = false;
        //    dd_BayType.Visible = true;
        //}
    }

    private void FillFloor()
    {
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        try
        {
            SqlCommand cmd = new SqlCommand("GetFloorList", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            sda.Fill(dt);
            dd_FloorName.Items.Clear();
            dd_FloorName.Items.Add(new ListItem("Floor", "0"));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                    dd_FloorName.Items.Add(new ListItem(dt.Rows[i][0].ToString().Trim(), dt.Rows[i][0].ToString().Trim()));
            }
            else
            {
                dd_FloorName.DataSource = null;
            }
        }
        catch (Exception ex) { }
        finally
        {
            con.Close();
        }
    }

    protected void btn_Create_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        try
        {
            if (txt_BayName.Text.Trim() == "")
            {
                lbl_msg0.Text = "Please enter Bay Name";
                lbl_msg0.Attributes.Add("style", "text-transform:none !important");
                txt_BayName.Focus();
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lbl_msg0.ClientID + "').style.display='none'\",5000)</script>");
                lbl_msg0.CssClass = "ErrMsg";
                return;
            }
            else if (dd_BayType.SelectedValue == "0")
            {
                lbl_msg0.Text = "Please select Bay Type";
                lbl_msg0.Attributes.Add("style", "text-transform:none !important");
                txt_BayName.Focus();
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lbl_msg0.ClientID + "').style.display='none'\",5000)</script>");
                lbl_msg0.CssClass = "ErrMsg";
                return;
            }
           
            else if (DropDownList1.SelectedValue == "")
            {
                lbl_msg0.Text = "Please select Bay Color";
                lbl_msg0.Attributes.Add("style", "text-transform:none !important");
                txt_BayName.Focus();
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lbl_msg0.ClientID + "').style.display='none'\",5000)</script>");
                lbl_msg0.CssClass = "ErrMsg";
                return;
            }
            else
            {
                btn_Create.Visible = true;
                lbl_msg0.Text = "";
                SqlCommand cmd = new SqlCommand("InsertBay", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BayName", txt_BayName.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@BayTypeId", dd_BayType.SelectedValue);
                cmd.Parameters.AddWithValue("@BayStatus", 1);
                cmd.Parameters.AddWithValue("@BayTypeName", dd_BayType.SelectedItem.Text.ToString());
                cmd.Parameters.AddWithValue("@color", DropDownList1.SelectedValue.ToString().Trim());
                cmd.Parameters.AddWithValue("@TeamLeadId", drp_tlid.SelectedValue);
                cmd.Parameters.AddWithValue("@Lift", chk_lift.Checked);
                cmd.Parameters.AddWithValue("@Toolkit", chk_toolkit.Checked);
                SqlParameter spm = cmd.Parameters.Add("@Exist", SqlDbType.VarChar, 50);
                spm.Direction = ParameterDirection.Output;
                spm.Value = "O";
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                if (spm.Value.ToString() == "Inserted Sucessfully.")
                {
                    Clears();
                    lbl_msg0.CssClass = "ScsMsg";
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lbl_msg0.ClientID + "').style.display='none'\",5000)</script>");
                    FillBay();
                    lbl_msg0.Text = "Bay successfully created!";
                    lbl_msg0.Attributes.Add("style", "text-transform:none !important");
                }
                else
                {

                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lbl_msg0.ClientID + "').style.display='none'\",5000)</script>");
                    lbl_msg0.CssClass = "ErrMsg";
                    lbl_msg0.Attributes.Add("style", "text-transform:none !important");
                    lbl_msg0.Text = spm.Value.ToString();
                }
                FillBay();
                FillFloor();
                dd_BayType.DataBind();
                GridView6.SelectedIndex = -1;
            }
        }
        catch (Exception ex)
        {
            lbl_msg0.CssClass = "ErrMsg";
            lbl_msg0.Attributes.Add("style", "text-transform:none !important");
            lbl_msg0.Text = ex.Message;
        }
        finally
        {
            con.Close();
        }
    }

    protected void btn_BayUpdate_Click(object sender, EventArgs e)
    {
        lbl_msg0.Text = "";
        lbl_msg0.CssClass = "reset";
        ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lbl_msg0.ClientID + "').style.display='none'\",5000)</script>");

        if (Page.IsValid)
        {
            SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
            try
            {
                lbl_msg0.Text = "";
                SqlCommand cmd = new SqlCommand("UpdateTblBay", con);
                cmd.CommandType = CommandType.StoredProcedure;
                DateTime dat = new DateTime();
                if (lblBayId.Text.Trim() == "")
                { 
                    lbl_msg0.Text = "Please select Bay Name!";
                    lbl_msg0.Attributes.Add("style", "text-transform:none !important");
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lbl_msg0.ClientID + "').style.display='none'\",5000)</script>");
                lbl_msg0.CssClass = "ErrMsg";
                }
                if (txt_BayName.Text.Trim() == "")
                {
                    lbl_msg0.Text = "Please enter Bay Name!";
                    lbl_msg0.Attributes.Add("style", "text-transform:none !important");
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lbl_msg0.ClientID + "').style.display='none'\",5000)</script>");
                    lbl_msg0.CssClass = "ErrMsg";

                }
                else if (dd_BayType.SelectedValue == "0")
                {
                    lbl_msg0.Text = "Please select Bay Type";
                    lbl_msg0.Attributes.Add("style", "text-transform:none !important");
                    txt_BayName.Focus();
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lbl_msg0.ClientID + "').style.display='none'\",5000)</script>");
                    lbl_msg0.CssClass = "ErrMsg";
                    return;
                }
                else if (DropDownList1.SelectedValue == "")
                {
                    lbl_msg0.Text = "Please select Bay Color";
                    lbl_msg0.Attributes.Add("style", "text-transform:none !important");
                    txt_BayName.Focus();
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lbl_msg0.ClientID + "').style.display='none'\",5000)</script>");
                    lbl_msg0.CssClass = "ErrMsg";
                    return;
                }
                else
                {
                    cmd.Parameters.AddWithValue("@BayId", lblBayId.Text.Trim());
                    cmd.Parameters.AddWithValue("@BayName", txt_BayName.Text.Trim());
                    cmd.Parameters.AddWithValue("@BayTypeId", dd_BayType.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@BayStatus", 1);
                    cmd.Parameters.AddWithValue("@BayTypeName", dd_BayType.SelectedItem.Text.ToString().Trim());
                    //cmd.Parameters.AddWithValue("@NewBayStatus", chk_NewBayType.Checked.ToString());
                    cmd.Parameters.AddWithValue("@color", DropDownList1.SelectedValue);
                    cmd.Parameters.AddWithValue("@toolkit", chk_toolkit.Checked);
                    cmd.Parameters.AddWithValue("@lift", chk_lift.Checked);
                    cmd.Parameters.AddWithValue("@TeamLeadId", drp_tlid.SelectedValue);
                    SqlParameter spm = cmd.Parameters.Add("@Exist", SqlDbType.VarChar, 100);
                    spm.Direction = ParameterDirection.Output;
                    spm.Value = "0";
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    cmd.ExecuteNonQuery();
                    if (spm.Value.ToString() == "Updated Sucessfully.")
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lbl_msg0.ClientID + "').style.display='none'\",5000)</script>");
                       
                        lbl_msg0.CssClass = "ScsMsg";
                        lbl_msg0.Text = "Bay successfully updated!";
                        lbl_msg0.Attributes.Add("style", "text-transform:none !important");
                        btn_Create.Visible = true;
                        btn_BayUpdate.Visible = false;
                        Clears();
                    }
                    else 
                    //if (spm.Value.ToString() == "No" || spm.Value.ToString()=="")
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lbl_msg0.ClientID + "').style.display='none'\",5000)</script>");
                        if(spm.Value.ToString()=="No")
                        {
                            lbl_msg0.Text = "Bay does not exist!";
                            lbl_msg0.Attributes.Add("style", "text-transform:none !important");
                        }
                           
                        else
                        lbl_msg0.Text = spm.Value.ToString();
                        lbl_msg0.CssClass = "ErrMsg";
                        lbl_msg0.Attributes.Add("style", "text-transform:none !important");
                        Clears();
                       
                    }
                    lblBayId.Text = "";
                    FillBay();
                    FillFloor();
                    dd_BayType.DataBind();
                    FillFloor();
                    GridView6.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lbl_msg0.ClientID + "').style.display='none'\",5000)</script>");
                lbl_msg0.CssClass = "ErrMsg";
                lbl_msg0.Attributes.Add("style", "text-transform:none !important");
                lbl_msg0.Text = ex.Message;
            }
            finally
            {
                con.Close();
            }
        }
    }

    protected void btn_Clear0_Click(object sender, EventArgs e)
    {
        Clears();
        lbl_msg0.Text = "";
        lbl_msg0.CssClass = "reset";
    }

    private void Clears()
    {
        
        lblBayId.Text = "";
        txt_BayName.Text = "";
        dd_BayType.SelectedIndex = -1;
       // dd_FloorName.SelectedIndex = -1;
       // chk_Active.Checked = true;
        txt_NewBayType.Text = "";
        //chk_NewBayType.Checked = false;
        txt_NewBayType.Visible = false;
        dd_BayType.Visible = true;
        // dd_FloorName.Visible = true;
        DropDownList1.SelectedValue = "";
        FillBay();
        FillFloor();
        txt_BayName.Focus();
        GridView6.SelectedIndex = -1;
        btn_BayUpdate.Visible = false;
        btn_Create.Visible = true;
        chk_toolkit.Checked = false;
        chk_lift.Checked = false;
        getTeamLead("GetTeamLead", drp_tlid);
    }

    private void FillBay()
    {
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        try
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("GetBays", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            sda.Fill(dt);
            GridView6.DataSource = dt;
            GridView6.DataBind();
        }
        catch (Exception ex) { }
        finally
        {
            con.Close();
        }
    }

    protected void GridView6_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[9].Visible = false;
            //e.Row.Cells[8].Visible = false;
              e.Row.Cells[10].Visible = false;
            e.Row.Cells[11].Visible = false;
            e.Row.Cells[12].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                e.Row.Cells[7].Text = "<table bgcolor=#" + e.Row.Cells[6].Text.ToString() + "><tr><td>&nbsp;</td</tr></table>";
                e.Row.Cells[7].Attributes.Add("style", "text-align: center; width:50px");
            }
            catch (Exception ex) { }
        }
    }

    protected void GridView6_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            btn_Create.Visible = false;
            btn_BayUpdate.Visible = true;
            lbl_msg0.Text = "";
            lbl_msg0.CssClass = "reset";
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lbl_msg0.ClientID + "').style.display='none'\",5000)</script>");
            dd_FloorName.SelectedIndex = -1;
            lblBayId.Text = GridView6.SelectedRow.Cells[1].Text.Trim();
            txt_BayName.Text = GridView6.SelectedRow.Cells[2].Text.Trim();
            dd_BayType.SelectedValue = GridView6.SelectedRow.Cells[4].Text.Trim();
            if (GridView6.SelectedRow.Cells[10].Text.Trim() != "&nbsp;" || GridView6.SelectedRow.Cells[10].Text.Trim() != "0")
            {
                drp_tlid.SelectedValue = GridView6.SelectedRow.Cells[10].Text.Trim();
            }
            else
                drp_tlid.SelectedValue = "0";
            chk_lift.Checked = Convert.ToBoolean(GridView6.SelectedRow.Cells[12].Text.Trim());
            chk_toolkit.Checked = Convert.ToBoolean(GridView6.SelectedRow.Cells[11].Text.Trim());
            // chk_Active.Checked = bool.Parse(GridView6.SelectedRow.Cells[9].Text.ToString());
            DropDownList1.SelectedValue = GridView6.SelectedRow.Cells[6].Text.ToString();

            if (GridView6.SelectedRow.Cells[5].Text.Trim() != "&nbsp;")
            {
                dd_FloorName.SelectedValue = GridView6.SelectedRow.Cells[5].Text.Trim();
            }
        }
        catch (Exception ex)
        { }
    }

    protected void GridView6_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lbl_msg0.ClientID + "').style.display='none'\",5000)</script>");
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());

        try
        {
            lbl_msg0.Text = "";
              SqlCommand cmd = new SqlCommand("DeleteBay", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BayId", GridView6.Rows[e.RowIndex].Cells[1].Text.ToString());
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            Clears();
            lbl_msg0.CssClass="ScsMsg";
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lbl_msg0.ClientID + "').style.display='none'\",5000)</script>");
            lbl_msg0.Text = "Bay Successfully Deleted. !";
            lbl_msg0.Attributes.Add("style", "text-transform:capitalize");
            btn_Create.Visible = true;
            btn_BayUpdate.Visible = false;
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lbl_msg0.ClientID + "').style.display='none'\",5000)</script>");
            lbl_msg0.CssClass = "ErrMsg";
            lbl_msg0.Attributes.Add("style", "text-transform:capitalize");
            lbl_msg0.Text = ex.Message.ToString();
        }
        finally
        {
            con.Close();
        }
    }

    protected void GridView6_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        lbl_msg0.Text = "";
        lbl_msg0.CssClass = "reset";
        GridView6.PageIndex = e.NewPageIndex;
        FillBay();
    }

    protected void btn_next_Click(object sender, EventArgs e)
    {
        Response.Redirect("TemplateMaster.aspx");
    }
}