using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserManagement : System.Web.UI.Page
{
    private DataTable dt;
    private SqlDataAdapter da;
    private SqlCommand cmd;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["ROLE"] == null || Session["ROLE"].ToString() != "ADMIN")
            {
                Response.Redirect("login.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("login.aspx");
        }
        SqlDataSource7.ConnectionString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        SqlDataSource2.ConnectionString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        SqlDataSource9.ConnectionString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        SqlDataSource6.ConnectionString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        SqlDataSource8.ConnectionString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        SqlDataSource3.ConnectionString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        SqlDataSource5.ConnectionString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        SqlDataSource10.ConnectionString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        try
        {
            if (!Page.IsPostBack)
            {
                SqlDataSource2.ConnectionString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
                SqlDataSource9.ConnectionString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
                //FillEmpName(cmb_EmployeeName);
                FillUserType(cmb_UserType);
                Session["CURRENT_PAGE"] = "User Management";
                dt = new DataTable();
                Clears();
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("login.aspx");
        }
        Clears();
    }

    

    protected void FillUserType(DropDownList fill_comboBox)
    {
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        try
        {
            DataTable sdt = new DataTable();
            SqlCommand cmd = new SqlCommand("SELECT [TypeId], [TypeDes] FROM [UserType] ORDER BY [TypeId]", con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter ssda = new SqlDataAdapter(cmd);
            con.Open();
            ssda.Fill(sdt);
            fill_comboBox.Items.Clear();
            ListItem lstItem;

            for (int i = 0; i < sdt.Rows.Count; i++)
            {
                lstItem = new ListItem();
                lstItem.Text = sdt.Rows[i]["TypeDes"].ToString();
                lstItem.Value = sdt.Rows[i]["TypeId"].ToString();
                fill_comboBox.Items.Add(lstItem);
            }

        }
        catch (Exception ex) { }
        finally
        {
            con.Close();
        }
    }

    private void Clears()
    {
        err_CMessage.Text = "";
        err_CMessage.CssClass = "reset";
        lbl_DelErr.Text = "";
        lbl_DelErr.CssClass = "reset";
        err_Message.Text = "";
        err_TMessage.Text = "";
        err_Message.CssClass = "reset";
    }

    protected void btn_Clear_Click(object sender, EventArgs e)
    {
        txt_UserName.Text = "";
        txt_Password.Text = "";
        txt_rePassword.Text = "";
        cmb_UserType.SelectedIndex = -1;
        Clears();
        txt_UserName.Focus();
    }

    protected void btn_Create_Click(object sender, EventArgs e)
    {
        if (txt_UserName.Text.Trim().ToString() == "")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + err_Message.ClientID + "').style.display='none'\",5000)</script>");
            err_Message.CssClass = "ErrMsg";
            err_Message.Text = "Enter User Name. !";
            err_Message.Attributes.Add("style", "text-transform:none !important");
            txt_UserName.Focus();
        }
        else if (txt_Password.Text.Trim().ToString() == "")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + err_Message.ClientID + "').style.display='none'\",5000)</script>");
            err_Message.CssClass = "ErrMsg";
            err_Message.Text = "Enter Password. !";
            err_Message.Attributes.Add("style", "text-transform:none !important");
            txt_Password.Focus();
        }
        else if (txt_rePassword.Text.Trim().ToString() == "")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + err_Message.ClientID + "').style.display='none'\",5000)</script>");
            err_Message.CssClass = "ErrMsg";
            err_Message.Text = "Re-enter Password. !";
            err_Message.Attributes.Add("style", "text-transform:none !important");
            txt_rePassword.Focus();
        }
        else if (txt_Password.Text.Trim().ToString() != txt_rePassword.Text.Trim().ToString())
        {
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + err_Message.ClientID + "').style.display='none'\",5000)</script>");
            err_Message.CssClass = "ErrMsg";
            err_Message.Attributes.Add("style", "text-transform:none !important");
            txt_Password.Text = "";
            txt_rePassword.Text = "";
            err_Message.Text = "Password not matched. !";
            txt_Password.Focus();
        }
        else if (cmb_UserType.Text.Trim().ToString() == "")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + err_Message.ClientID + "').style.display='none'\",5000)</script>");
            err_Message.CssClass = "ErrMsg";
            err_Message.Text = "Select user type.!";
            err_Message.Attributes.Add("style", "text-transform:none !important");
            cmb_UserType.Focus();
        }
        else if (cmb_EmployeeName.Text.Trim().ToString() == "")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + err_Message.ClientID + "').style.display='none'\",5000)</script>");
            err_Message.CssClass = "ErrMsg";
            err_Message.Text = "Select employee name.!";
            err_Message.Attributes.Add("style", "text-transform:none !important");
            cmb_UserType.Focus();
        }

        else
        {
            SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
            try
            {
                cmd = new SqlCommand("", con);
                cmd.CommandText = "CheckUserName";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserName", txt_UserName.Text.Trim().ToString());
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                con.Open();

                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count <= 0)
                {
                    cmd = new SqlCommand("", con);
                    cmd.CommandText = "CreateUser";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@userName", txt_UserName.Text.Trim().ToString());
                    cmd.Parameters.AddWithValue("@password", txt_Password.Text.Trim().ToString());
                    cmd.Parameters.AddWithValue("@typeid", cmb_UserType.SelectedValue);
                    cmd.Parameters.AddWithValue("@EmpId", cmb_EmployeeName.SelectedValue);
                    SqlParameter spm = cmd.Parameters.Add("@msg", SqlDbType.VarChar, 50);
                    spm.Direction = ParameterDirection.Output;
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    cmd.ExecuteNonQuery();
                    if (spm.Value.ToString().Contains("Successfull"))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + err_Message.ClientID + "').style.display='none'\",5000)</script>");
                        err_Message.CssClass = "ScsMsg";
                        err_Message.Attributes.Add("style", "text-transform:none !important");
                        err_Message.Text = spm.Value.ToString();
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + err_Message.ClientID + "').style.display='none'\",5000)</script>");
                        err_Message.CssClass = "ErrMsg";
                        err_Message.Attributes.Add("style", "text-transform:none !important");
                        err_Message.Text = spm.Value.ToString();
                    }
                    GridView1.DataBind();
                    GridView2.DataBind();
                    GridView3.DataBind();
                    cmb_DelUserList.DataBind();
                    cmb_uname.DataBind();
                    cmb_Tuname.DataBind();
                    //err_Message.ForeColor = Color.Green;
                    //err_Message.Text = "User successfully created. !";
                    txt_UserName.Text = "";
                    txt_Password.Text = "";
                    txt_rePassword.Text = "";
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + err_Message.ClientID + "').style.display='none'\",5000)</script>");
                    err_Message.CssClass = "ErrMsg";
                    err_Message.Text = "User name aleady exist. !";
                    err_Message.Attributes.Add("style", "text-transform:none !important");
                    txt_UserName.Text = "";
                    txt_Password.Text = "";
                    txt_rePassword.Text = "";
                    txt_UserName.Focus();
                }
            }
            catch (Exception ex) { }
            finally
            {
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
        }
    }

    protected void btn_Change_Click(object sender, EventArgs e)
    {
        err_TMessage.CssClass = "reset";
        err_TMessage.Text = "";
        if (cmb_uname.Text.Trim().ToString() == "")
        {
            err_CMessage.Text = "Select User Name. !";
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + err_CMessage.ClientID + "').style.display='none'\",5000)</script>");
            err_CMessage.CssClass = "ErrMsg";
            err_CMessage.Attributes.Add("style", "text-transform:none !important");
            cmb_uname.Focus();
        }
        else if (txt_NewPass.Text.Trim().ToString() == "")
        {
            err_CMessage.Text = "Enter new Password. !";
            err_CMessage.Attributes.Add("style", "text-transform:none !important");
            err_CMessage.CssClass = "ErrMsg";
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + err_CMessage.ClientID + "').style.display='none'\",5000)</script>");
        
            txt_NewPass.Focus();
        }
        else if (txt_RePass.Text.Trim().ToString() == "")
        {
            err_CMessage.Text = "Re-enter Password. !";
            err_CMessage.Attributes.Add("style", "text-transform:none !important");
            err_CMessage.CssClass = "ErrMsg";
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + err_CMessage.ClientID + "').style.display='none'\",5000)</script>");

            txt_RePass.Focus();
        }
        else if (txt_NewPass.Text.Trim().ToString() != txt_RePass.Text.Trim().ToString())
        {
            txt_NewPass.Text = "";
            txt_RePass.Text = "";
            err_CMessage.Text = "Password not matched. !";
            err_CMessage.Attributes.Add("style", "text-transform:none !important");
            err_CMessage.CssClass = "ErrMsg";
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + err_CMessage.ClientID + "').style.display='none'\",5000)</script>");

            txt_NewPass.Focus();
        }
        else
        {
            SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
            try
            {
                cmd = new SqlCommand("", con);
                cmd.CommandText = "ChangeUserPassword";
             
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@slno", cmb_uname.SelectedValue);
                cmd.Parameters.AddWithValue("@password", txt_NewPass.Text.Trim().ToString());
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd.ExecuteNonQuery();
                GridView1.DataBind();
                GridView2.DataBind();
                GridView3.DataBind();
                cmb_DelUserList.DataBind();
                cmb_uname.DataBind();
                cmb_Tuname.DataBind();
              
                err_CMessage.Text = "Password successfully changed. !";
                err_CMessage.Attributes.Add("style", "text-transform:none !important");
                err_CMessage.CssClass = "ScsMsg";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + err_CMessage.ClientID + "').style.display='none'\",5000)</script>");

                cmb_uname.Text = "";
                txt_NewPass.Text = "";
                txt_RePass.Text = "";
            }
            catch (Exception ex) { }
            finally
            {
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
        }
    }

    protected void btn_Delete_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        try
        {
            string temp = cmb_DelUserList.SelectedItem.Text.ToString();
            if (temp.ToString().ToUpper() != Session["UserId"].ToString().ToUpper())
            {
                cmd = new SqlCommand("", con);
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd.CommandText = "DeleteUser";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@slno", cmb_DelUserList.SelectedValue);
                cmd.ExecuteNonQuery();
                GridView1.DataBind();
                GridView2.DataBind();
                GridView3.DataBind();
                cmb_DelUserList.DataBind();
                cmb_uname.DataBind();
                cmb_Tuname.DataBind();
               
                lbl_DelErr.Text = "User (" + temp.ToString() + ") successfully deleted. !";
                lbl_DelErr.CssClass = "ScsMsg";
                lbl_DelErr.Attributes.Add("style", "text-transform:none !important");
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lbl_DelErr.ClientID + "').style.display='none'\",5000)</script>");

            }
            else
            {
           
                lbl_DelErr.Text = "User (" + temp.ToString() + ") not deleted. !" + temp.ToString();
                lbl_DelErr.CssClass = "ErrMsg";
                lbl_DelErr.Attributes.Add("style", "text-transform:none !important");
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lbl_DelErr.ClientID + "').style.display='none'\",5000)</script>");

            }
        }
        catch (Exception ex) { }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }

    protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
    {
        Clears();
    }

    protected void btn_CPClear_Click(object sender, EventArgs e)
    {
        txt_NewPass.Text = "";
        txt_RePass.Text = "";
        Clears();
        cmb_uname.Focus();
    }

    protected void GridView3_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            err_Message.CssClass = "reset";
            txt_UserName.Text = GridView3.SelectedRow.Cells[2].Text.Trim();
            cmb_UserType.SelectedItem.Text = GridView3.SelectedRow.Cells[3].Text.Trim();
            txt_Password.Text = "";
            txt_rePassword.Text = "";
            txt_Password.Focus();
        }
        catch (Exception ex)
        {
        }
    }

    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Visible = false;
        }
    }

    protected void GridView3_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView3.PageIndex = e.NewPageIndex;
        GridView3.DataBind();
    }

    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            cmb_DelUserList.SelectedItem.Text = GridView2.SelectedRow.Cells[2].Text.Trim();
            cmb_DelUserList.SelectedValue = GridView2.SelectedRow.Cells[1].Text.Trim();
        }
        catch (Exception ex)
        {
        }
        finally
        {
        }
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Visible = false;
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Visible = false;
        }
    }

    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        GridView2.DataBind();
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        err_TMessage.CssClass = "reset";
        err_TMessage.Text = "";
        try
        {
            cmb_uname.SelectedValue = GridView1.SelectedRow.Cells[1].Text.Trim();
            cmb_Tuname.SelectedValue = GridView1.SelectedRow.Cells[1].Text.Trim();
            cmb_Type.SelectedValue = GridView1.SelectedRow.Cells[4].Text.Trim();
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[4].Visible = false;
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[4].Visible = false;
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        err_TMessage.CssClass = "reset";
        err_TMessage.Text = "";
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataBind();
    }

    protected void btn_TClear_Click(object sender, EventArgs e)
    {
        cmb_Tuname.SelectedIndex = -1;
        cmb_Type.SelectedIndex = -1;
        err_TMessage.Text = "";
        err_Message.CssClass = "reset";
    }

    protected void btn_TChange_Click(object sender, EventArgs e)
    {
        if (cmb_Tuname.Text.Trim().ToString() == "")
        {
            err_TMessage.Text = "Select User Name. !";
            err_TMessage.Attributes.Add("style", "text-transform:none !important");
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + err_TMessage.ClientID + "').style.display='none'\",5000)</script>");
            err_TMessage.CssClass = "ErrMsg";
            cmb_Tuname.Focus();
        }
        else if (cmb_Type.Text.Trim().ToString() == "")
        {
            err_TMessage.Text = "Select user type. !";
            err_TMessage.Attributes.Add("style", "text-transform:none !important");
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + err_TMessage.ClientID + "').style.display='none'\",5000)</script>");
            err_TMessage.CssClass = "ErrMsg";
            cmb_Type.Focus();
        }
        else if (cmb_EName.Text.Trim().ToString() == "")
        {
            err_TMessage.Text = "Select employee name. !";
            err_TMessage.Attributes.Add("style", "text-transform:none !important");
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + err_TMessage.ClientID + "').style.display='none'\",5000)</script>");
            err_TMessage.CssClass = "ErrMsg";
            cmb_Type.Focus();
        }
        else
        {
            SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
            try
            {
                cmd = new SqlCommand("", con);
                cmd.CommandText = "ChangeUserType";
                
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@slno", cmb_Tuname.SelectedValue);
                cmd.Parameters.AddWithValue("@typeid", cmb_Type.SelectedValue);
                cmd.Parameters.AddWithValue("@EmpId", cmb_EName.SelectedValue);
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd.ExecuteNonQuery();
                GridView1.DataBind();
                GridView2.DataBind();
                GridView3.DataBind();
                cmb_Type.SelectedIndex = -1;
                cmb_EName.SelectedIndex = -1;
                cmb_Tuname.SelectedIndex = -1;
                cmb_DelUserList.DataBind();
                cmb_uname.DataBind();
                cmb_Tuname.DataBind();
               
                err_TMessage.Text = "User type successfully changed. !";
                err_TMessage.Attributes.Add("style", "text-transform:none !important");
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + err_TMessage.ClientID + "').style.display='none'\",5000)</script>");
                err_TMessage.CssClass = "ScsMsg";
                cmb_uname.Text = "";
                txt_NewPass.Text = "";
                txt_RePass.Text = "";
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