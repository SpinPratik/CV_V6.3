using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Configuration;

public partial class AdminRegistration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.transition='all 2s'\")</script>");
      
        lblMsg.CssClass = "Reset";
        try
        {
            if (Session[Session["TMLDealercode"] + "-TMLConString"].ToString() == null || Session[Session["TMLDealercode"] + "-TMLConString"].ToString() == "")
                Response.Redirect("login.aspx");
        }
        catch
        {
            Response.Redirect("login.aspx");
        }
        if (!IsPostBack)
        {
            lblMsg.CssClass = "Reset";
            //ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
            FillStates("UdpGetStates", drp_states);
            selectCity("0");
            getDEalerDetails();
            getWorkTime();
            getDisplayMsg();
            GetSMSSubcription();
            
            
        }
       
    }

public void GetSMSSubcription() //Added by Pratik
    {
        using (SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString()))
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT SMSCount FROM tbl_SMSCount ", con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                int Credits = Convert.ToInt32(dt.Rows[0]["SMSCount"].ToString());
                int TOTSMS = 3200;

                if (Credits > TOTSMS)
                {
                    string message = "Your SMS are over to recharge contact csco@spintech.in";

                    System.Text.StringBuilder sb = new System.Text.StringBuilder();

                    sb.Append("<script type = 'text/javascript'>");

                    sb.Append("window.onload=function(){");

                    sb.Append("alert('");

                    sb.Append(message);

                    sb.Append("')};");

                    sb.Append("</script>");

                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                }

                int diff = 0;
                diff = TOTSMS - Credits;
                if (diff <= 700)
                {
                    string message = "Your SMS are About to finish to recharge contact csco@spintech.in";

                    System.Text.StringBuilder sb = new System.Text.StringBuilder();

                    sb.Append("<script type = 'text/javascript'>");

                    sb.Append("window.onload=function(){");

                    sb.Append("alert('");

                    sb.Append(message);

                    sb.Append("')};");

                    sb.Append("</script>");

                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
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
    protected void btn_reset_Click(object sender, EventArgs e)
    {
        lblMsg.CssClass = "Reset";
        lblMsg.Text = "";
        txt_area.Text = "";
       // txt_dealer.Text = "";
        txt_email.Text = "";
        txt_Fname.Text = "";
        txt_lname.Text = "";
        txt_mobNum.Text = "";
        txt_orgName.Text = "";
        txt_phoneNum.Text = "";
        txt_pincode.Text = "";
        txt_role.Text = "";
        txt_website.Text = "";
        txt_shiftEnd.Text = "";
        txt_shiftStart.Text = "";
        txt_brkEnd.Text = "";
        txt_brkStart.Text = "";
        txt_dsplyMsg.Text = "";
        txt_city.Text = "";
    }

    protected void btn_next_Click(object sender, EventArgs e)
    {
        Response.Redirect("RFIDCards.aspx");
    }

    public void FillStates(string procedureName, DropDownList fill_comboBox)
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
                con.Close();
                fill_comboBox.Items.Clear();
                ListItem lstItem;
                for (int i = 0; i < sdt.Rows.Count; i++)
                {
                    lstItem = new ListItem();
                    lstItem.Text = sdt.Rows[i]["StateName"].ToString();
                    lstItem.Value = sdt.Rows[i]["StateId"].ToString();
                    fill_comboBox.Items.Add(lstItem);
                }

            }
            catch (Exception ex) { }
            finally
            {
                con.Close();
            }
       
    }

    protected void drp_states_SelectedIndexChanged(object sender, EventArgs e)
    {
        selectCity(drp_states.SelectedValue.ToString());
    }

    protected void selectCity(string stateId)
    {
      
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        {
            using (SqlCommand cmd = new SqlCommand("UdpGetDistricts", con))
            {
               
                cmd.CommandType = CommandType.StoredProcedure;
                if(stateId=="" || stateId == null)
                {
                    cmd.Parameters.AddWithValue("@StateId", 1);
                }
                else
                { 
                cmd.Parameters.AddWithValue("@StateId", stateId);
                }
                cmd.Connection = con;
                con.Open();
                drp_district.DataSource = cmd.ExecuteReader();
                drp_district.DataTextField = "DistrictName";
                drp_district.DataValueField = "DistrictId";
                drp_district.DataBind();
                con.Close();
            }
        }

    }

    private bool ValidInterval(string Time1, string Time2)
    {
        try
        {
            if ((Time1.Trim() != ":" && Time2.Trim() != ":") || (Time1.Trim() != "" && Time2.Trim() != ""))
            {
                TimeSpan ts1 = Convert.ToDateTime(Time1.Trim()).TimeOfDay;
                TimeSpan ts2 = Convert.ToDateTime(Time2.Trim()).TimeOfDay;
                double diff = ts2.Subtract(ts1).TotalMinutes;
                if (diff <= 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }
        catch (Exception ex)
        {
            return true;
        }
    }


    protected void btn_submit_Click(object sender, EventArgs e)
    {
        

        if (txt_shiftStart.Text.ToString()=="")
        {
            lblMsg.CssClass = "ErrMsg";
            lblMsg.Attributes.Add("style", "text-transform:none");
            lblMsg.ForeColor = Color.Red;
            lblMsg.Text = "Please enter shift start time";
            lblMsg.ForeColor = System.Drawing.Color.Red;
            txt_shiftStart.Focus();
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
        }

        else if (txt_shiftEnd.Text.ToString() == "")
        {
            lblMsg.CssClass = "ErrMsg";
            lblMsg.Attributes.Add("style", "text-transform:none");
            lblMsg.ForeColor = Color.Red;
            lblMsg.Text = "Please enter shift end time";
            lblMsg.ForeColor = System.Drawing.Color.Red;
            txt_shiftEnd.Focus();
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
        }
        //else if (txt_brkStart.Text.ToString() == "")
        //{
        //    lblMsg.CssClass = "ErrMsg";
        //    lblMsg.ForeColor = Color.Red;
        //    lblMsg.Text = "Please enter Shift End Time";
        //    lblMsg.ForeColor = System.Drawing.Color.Red;
        //    txt_brkStart.Focus();
        //    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
        //}
        //else if (txt_brkEnd.Text.ToString() == "")
        //{
        //    lblMsg.CssClass = "ErrMsg";
        //    lblMsg.ForeColor = Color.Red;
        //    lblMsg.Text = "Please enter Shift End Time";
        //    lblMsg.ForeColor = System.Drawing.Color.Red;
        //    txt_brkEnd.Focus();
        //    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
        //}
      
        //else if (ValidInterval(txt_brkStart.Text.ToString().Trim(), txt_brkEnd.Text.ToString().Trim()) == false)
        //{
        //    lblMsg.CssClass = "ErrMsg";
        //    lblMsg.ForeColor = Color.Red;
        //    lblMsg.Text = "Please, Check Valid Time Between Break1 - Start and End Time !";
        //    lblMsg.ForeColor = System.Drawing.Color.Red;
        //    txt_brkEnd.Focus();
        //    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
        //}
        //else if ((ValidInterval(txt_brkEnd.Text.ToString().Trim(), txt_shiftEnd.Text.ToString()) == false))
        //{
        //    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
        //    lblMsg.CssClass = "ErrMsg";
        //    lblMsg.ForeColor = Color.Red;
        //    lblMsg.Text = "First Break Time should be in between Shift Begin and Shift End Time !";
        //    lblMsg.ForeColor = System.Drawing.Color.Red;
        //    txt_brkEnd.Focus();
        //}
        //DateTime shiftStart = Convert.ToDateTime(txt_shiftStart.Text.ToString());
        //DateTime shiftEnd = Convert.ToDateTime(txt_shiftEnd.Text.ToString());
        //DateTime breakStart = Convert.ToDateTime(txt_brkStart.Text.ToString());
        //DateTime breakEnd = Convert.ToDateTime(txt_brkEnd.Text.ToString());

      //else  if (Convert.ToDateTime(txt_shiftStart.Text.ToString()) > Convert.ToDateTime(txt_shiftEnd.Text.ToString()))
      //  {
      //      lblMsg.CssClass = "ErrMsg";
      //      lblMsg.Text = " Start time should be greater than End Time";
      //      lblMsg.ForeColor = System.Drawing.Color.Red;
      //      txt_brkStart.Focus();
      //      ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
        //}
        //else if (Convert.ToDateTime(txt_brkStart.Text.ToString()) > Convert.ToDateTime(txt_brkEnd.Text.ToString()))
        //{
        //    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
        //    lblMsg.CssClass = "ErrMsg";
        //    lblMsg.Text = " Break Start time should be greater than Break end Time !";
        //    lblMsg.ForeColor = System.Drawing.Color.Red;
        //    txt_brkStart.Focus();
        //}
        //else if (Convert.ToDateTime(txt_brkStart.Text.ToString()) > Convert.ToDateTime(txt_shiftStart.Text.ToString()))
        //{
        //    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
        //    lblMsg.CssClass = "ErrMsg";
        //    lblMsg.Text = " Break Start time should be greater than Shift start Time !";
        //    lblMsg.ForeColor = System.Drawing.Color.Red;
        //    txt_brkStart.Focus();
        //}
        //else if (Convert.ToDateTime(txt_brkEnd.Text.ToString()) < Convert.ToDateTime(txt_shiftEnd.Text.ToString()))
        //{
        //    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
        //    lblMsg.CssClass = "ErrMsg";
        //    lblMsg.Text = " Break Start time should be greater than Break end Time !";
        //    lblMsg.ForeColor = System.Drawing.Color.Red;
        //    txt_brkStart.Focus();
        //}
        else if (txt_dsplyMsg.Text=="")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
            lblMsg.CssClass = "ErrMsg";
            lblMsg.Attributes.Add("style", "text-transform:none");
            lblMsg.Text = "Enter display message";
            lblMsg.ForeColor = System.Drawing.Color.Red;
            txt_dsplyMsg.Focus();
        }
        else if (txt_Fname.Text == "" )
        {
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
            lblMsg.CssClass = "ErrMsg";
            lblMsg.Attributes.Add("style","text-transform:none");
            lblMsg.Text = "Enter authorised person name";
        }
      
        else if (txt_email.Text == "")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
            lblMsg.CssClass = "ErrMsg";
            lblMsg.Attributes.Add("style", "text-transform:none");
            lblMsg.Text = "Enter email";
        }
        else if (txt_mobNum.Text == "")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
            lblMsg.CssClass = "ErrMsg";
            lblMsg.Attributes.Add("style", "text-transform:none");
            lblMsg.Text = "Enter mobile number";
        }
        else if (txt_mobNum.Text != "" && txt_mobNum.Text.Length<10)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
            lblMsg.CssClass = "ErrMsg";
            lblMsg.Attributes.Add("style", "text-transform:none");
            lblMsg.Text = "Enter valid mobile number";
        }
        else if (ValidInterval(txt_shiftStart.Text.ToString().Trim(), txt_brkStart.Text.ToString().Trim()) == false)
        {
            lblMsg.CssClass = "ErrMsg";
            lblMsg.ForeColor = Color.Red;
            lblMsg.Attributes.Add("style", "text-transform:none");
            lblMsg.Text = "Please, check valid time between Shift Start-Break Start time !";
            lblMsg.ForeColor = System.Drawing.Color.Red;
            // txt_brkEnd.Focus();
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
        }
        else if (ValidInterval(txt_brkStart.Text.ToString().Trim(), txt_brkEnd.Text.ToString().Trim()) == false)
        {
            lblMsg.CssClass = "ErrMsg";
            lblMsg.ForeColor = Color.Red;
            lblMsg.Attributes.Add("style", "text-transform:none");
            lblMsg.Text = "Please, check valid time between Break - Start and End time !";
            lblMsg.ForeColor = System.Drawing.Color.Red;
            // txt_brkEnd.Focus();
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
        }
        else if ((ValidInterval(txt_brkEnd.Text.ToString().Trim(), txt_shiftEnd.Text.ToString()) == false))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
            lblMsg.CssClass = "ErrMsg";
            lblMsg.Attributes.Add("style", "text-transform:none");
            lblMsg.ForeColor = Color.Red;
            lblMsg.Text = "First break time should be in between Break End - Shift End time !";
            lblMsg.ForeColor = System.Drawing.Color.Red;
            //txt_brkEnd.Focus();
        }

        else
        {
            using (SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString()))
            { 
                try
                {
                    SqlCommand cmd = new SqlCommand("InsertTblDealerDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@D_Code", txt_dealer.Text.ToString());
                    cmd.Parameters.AddWithValue("@D_Name", txt_orgName.Text.ToString());
                    cmd.Parameters.AddWithValue("@D_Address", txt_area.Text.ToString());
                    cmd.Parameters.AddWithValue("@D_Location", "");
                    cmd.Parameters.AddWithValue("@D_City", txt_city.Text.ToString());
                    if (drp_states.SelectedItem.Text == "--Select--")
                        cmd.Parameters.AddWithValue("@D_State", "");
                    else
                        cmd.Parameters.AddWithValue("@D_State", drp_states.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@D_ZIP", txt_pincode.Text.ToString());
                    cmd.Parameters.AddWithValue("@D_Division", txt_dealer.Text.ToString());
                    cmd.Parameters.AddWithValue("@D_Email", txt_email.Text.ToString());
                    if (drp_district.SelectedItem.Text == "--Select--")
                        cmd.Parameters.AddWithValue("@D_District", "");
                    else
                        cmd.Parameters.AddWithValue("@D_District", drp_district.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@D_Telephoneno", txt_phoneNum.Text.ToString());
                    cmd.Parameters.AddWithValue("@D_WEBSITE", txt_website.Text.ToString());
                    cmd.Parameters.AddWithValue("@D_FIRSTNAME", txt_Fname.Text.ToString());
                    cmd.Parameters.AddWithValue("@D_LASTNAME", txt_lname.Text.ToString());
                    cmd.Parameters.AddWithValue("@ROLE", txt_role.Text.ToString());
                    cmd.Parameters.AddWithValue("@PHONENUMBER", txt_mobNum.Text.ToString());

                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    int i = cmd.ExecuteNonQuery();

                    if (i < 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
                        insertDisplayMessage();
                        InsertShiftDetails();
                        lblMsg.Text = "Inserted successfully";

                        lblMsg.CssClass = "ScsMsg";
                        lblMsg.Attributes.Add("style", "text-transform:none");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");

                        lblMsg.Text = "Dealer details not inserted. Try again";
                        lblMsg.CssClass = "ErrMsg";
                        lblMsg.Attributes.Add("style", "text-transform:none");
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

    }
    protected void insertDisplayMessage()
    {
        ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
        using (SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString())) { 
            try
            {
                SqlCommand cmd = new SqlCommand();
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd = new SqlCommand("UdpInsertDisplayMsg", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DisplayStatus", txt_dsplyMsg.Text.Trim());
                cmd.ExecuteNonQuery();
                //lblMsg.ForeColor = Color.Green;
                //lblMsg.Text = "Message Successfully saved. !";
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
                lblMsg.ForeColor = Color.Red;
                lblMsg.Text = ex.Message.ToString();
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
        }
    }

    protected void InsertShiftDetails()
    {
        ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
      
            //DateTime shiftStart = Convert.ToDateTime(txt_shiftStart.Text.ToString());
            //DateTime shiftEnd = Convert.ToDateTime(txt_shiftEnd.Text.ToString());
            //DateTime breakStart = Convert.ToDateTime(txt_brkStart.Text.ToString());
            //DateTime breakEnd = Convert.ToDateTime(txt_brkEnd.Text.ToString());

            // if (shiftStart > shiftEnd)
            //{
            //    lblMsg.CssClass = "ErrMsg";
            //    lblMsg.Text = " Start time should be greater than End Time";
            //    lblMsg.ForeColor = System.Drawing.Color.Red;
            //    txt_brkStart.Focus();
            //}
            // else if (breakStart > breakEnd)
            //    {
            //        lblMsg.CssClass = "ErrMsg";
            //        lblMsg.Text = " Break Start time should be greater than Break end Time !";
            //        lblMsg.ForeColor = System.Drawing.Color.Red;
            //        txt_brkStart.Focus();
            //    }
            //    else if (breakStart > shiftStart)
            //    {
            //        lblMsg.CssClass = "ErrMsg";
            //        lblMsg.Text = " Break Start time should be greater than Shift start Time !";
            //        lblMsg.ForeColor = System.Drawing.Color.Red;
            //        txt_brkStart.Focus();
            //    }
            //    else if (breakEnd < shiftEnd)
            //    {
            //        lblMsg.CssClass = "ErrMsg";
            //        lblMsg.Text = " Break Start time should be greater than Break end Time !";
            //        lblMsg.ForeColor = System.Drawing.Color.Red;
            //        txt_brkStart.Focus();
            //    }
            //    else

            using (SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString()))
            { 
        try
        {
            SqlCommand cmd = new SqlCommand("InsertTblShiftWorkTime", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@InTime", txt_shiftStart.Text.ToString());
        cmd.Parameters.AddWithValue("@BreakIn",txt_brkStart.Text.ToString());
        cmd.Parameters.AddWithValue("@BreakOut", txt_brkEnd.Text.ToString());
        cmd.Parameters.AddWithValue("@OutTime",txt_shiftEnd.Text.ToString());
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
        cmd.ExecuteNonQuery();
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
    protected void getDisplayMsg()
    {
        try
        {
            
           DataTable dt = new DataTable();
           SqlDataAdapter sda = new SqlDataAdapter("Select * from tbl_DisplayStatus", new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString()));
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txt_dsplyMsg.Text = dt.Rows[0][0].ToString();
            }
            else
            {
                txt_dsplyMsg.Text = "";
            }
        }
        catch { }
    } 

    protected void getDEalerDetails()
    {
        using (SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString()))
        {
        try
        {
        
        SqlCommand cmd = new SqlCommand("GetDealerDetails", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Open();
        sda.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            //drp_states.SelectedValue = dt.Rows[0]["D_State"].ToString();
            //drp_district.DataTextField = dt.Rows[0]["D_District"].ToString();
            txt_dealer.Text= dt.Rows[0]["D_Code"].ToString();
            txt_dealer.ReadOnly = true;
            txt_orgName.Text= dt.Rows[0]["D_Name"].ToString();
            // string State = dt.Rows[0]["D_State"].ToString();
            //drp_states.SelectedValue = dt.Rows[0]["D_State"].ToString();
            if (String.IsNullOrEmpty(dt.Rows[0]["stateid"].ToString()))
            {
                drp_states.SelectedIndex = 0;
            }
            else
                drp_states.SelectedValue = dt.Rows[0]["stateid"].ToString();

            selectCity(drp_states.SelectedValue);
            if (String.IsNullOrEmpty(dt.Rows[0]["District"].ToString()))
            {
                drp_district.SelectedIndex = 0;
            }
            else
                drp_district.SelectedValue = dt.Rows[0]["District"].ToString();
            txt_area.Text= dt.Rows[0]["D_Address"].ToString();
            txt_city.Text= dt.Rows[0]["D_City"].ToString();
            txt_Fname.Text = dt.Rows[0]["D_FIRSTNAME"].ToString();
            txt_lname.Text = dt.Rows[0]["D_LASTNAME"].ToString();
            txt_email.Text= dt.Rows[0]["D_Email"].ToString();
            txt_role.Text= dt.Rows[0]["ROLE"].ToString();
            txt_pincode.Text= dt.Rows[0]["D_ZIP"].ToString();
            txt_phoneNum.Text= dt.Rows[0]["D_Telephoneno"].ToString();
            txt_website.Text= dt.Rows[0]["D_WEBSITE"].ToString();
            txt_mobNum.Text = dt.Rows[0]["PHONENUMBER"].ToString();
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
    protected void getWorkTime()
    {
        using (SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString()))
        {
        con.Open();
        String query = "udpGetWorkTimeforAdmin";
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        cmd.CommandType = CommandType.StoredProcedure;
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        if (dt.Rows.Count != 0)
        {
            txt_shiftStart.Text = dt.Rows[0]["InTime"].ToString();
            txt_brkStart.Text = dt.Rows[0]["BreakIn"].ToString();
            txt_brkEnd.Text = dt.Rows[0]["BreakOut"].ToString();
            txt_shiftEnd.Text = dt.Rows[0]["OutTime"].ToString();
        }

        }
    }
}