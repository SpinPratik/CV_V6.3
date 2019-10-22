using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class ManualEntry : System.Web.UI.Page
{
    private SqlConnection con = new SqlConnection(DataManager.ConStr);
    private SqlCommand cmd;
    DataTable dtVehi = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Session["CURRENT_PAGE"] = "Manual Entry";
            GetProcessDetails();
            drpMode.Items.Clear();
            drpMode.Items.Add(new ListItem("--Select--", "-1"));
            txtDate.Text = DateTime.Now.Date.ToString("MM/dd/yyyy");
            txtTime.Text = DateTime.Now.ToString("HH:mm");
        }
       
    }
    protected void GetProcessDetails()
    {
        try
        {
            SqlCommand cmdVehi = new SqlCommand("udpDemoListDevice", con);
            cmdVehi.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter daVehi = new SqlDataAdapter(cmdVehi);
            DataTable dtVehi = new DataTable();
            daVehi.Fill(dtVehi);

            if (dtVehi.Rows.Count > 0)
            {
                drpProcess.Items.Clear();
                drpProcess.Items.Add(new ListItem("--Select--", "0"));
                drpProcess.DataSource = dtVehi;
                drpProcess.DataTextField = "Device";
                drpProcess.DataValueField = "Slno";
                drpProcess.DataBind();
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void GetVehicleCardNo()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("", con);
            cmd.CommandType = CommandType.StoredProcedure;
            if (drpProcess.SelectedItem.Text == "Gate" && drpMode.SelectedItem.Text=="In")
             cmd.CommandText = "udpDemoUnAssignVehCardList";
            else
                cmd.CommandText = "udpDemoAssignVehCardList";
           
            SqlDataAdapter daVehi = new SqlDataAdapter(cmd);
            daVehi.Fill(dtVehi);
            if (dtVehi.Rows.Count > 0)
            {
                drpVCardNo.Items.Clear();
                drpVCardNo.Items.Add(new ListItem("--Select--", "0"));
                drpVCardNo.DataSource = dtVehi;
                drpVCardNo.DataTextField = "EnrollmentNo";
                drpVCardNo.DataValueField = "EnrollmentNo";
                drpVCardNo.DataBind();
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void GetEmployeeCardNo()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("", con);
            cmd.CommandType = CommandType.StoredProcedure;
            if (drpProcess.SelectedItem.Text == "JobSlip")
                cmd.CommandText = "udpDemoSACardList";
            else if (drpProcess.SelectedItem.Text == "Normal Workshop" || drpProcess.SelectedItem.Text == "Speedo Workshop" || drpProcess.SelectedItem.Text == "Wheel Alignment")
                cmd.CommandText = "udpDemoTechCardList";
            else if (drpProcess.SelectedItem.Text == "Final Inspection")
                cmd.CommandText = "udpDemoFICardList";
            else
                cmd.CommandText = "udpDemoEmpCardList";

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                drpECardNo.Items.Clear();
                drpECardNo.Items.Add(new ListItem("--Select--", "0"));
                drpECardNo.DataSource = dt;
                drpECardNo.DataTextField = "CardNo";
                drpECardNo.DataValueField = "CardNo";
                drpECardNo.DataBind();
            }
        }
        catch (Exception ex)
        {
        }
    }
   protected void Clear()
   {
       GetProcessDetails();
       txtDate.Text = DateTime.Now.Date.ToString("MM/dd/yyyy");
       txtTime.Text = DateTime.Now.ToString("HH:mm");
       drpMode.Items.Clear();
       drpMode.Items.Add(new ListItem("--Select--", "-1"));
       drpCardType.Enabled = false;
       drpVCardNo.Enabled = false;
       drpECardNo.Enabled = false;
       GetVehicleCardNo();
       GetEmployeeCardNo();
   }
   
    protected void GetCardType()
    {
        drpCardType.Items.Clear();
        drpCardType.Items.Add(new ListItem("--Select--", "-1"));
        drpCardType.Items.Add(new ListItem("Vehicle", "0"));
        drpCardType.Items.Add(new ListItem("Employee", "1"));
    }
    protected void drpProcess_SelectedIndexChanged(object sender, EventArgs e)
    
    {
        txtTime.Text = DateTime.Now.ToString("HH:mm");
        drpMode.Items.Clear();
        drpCardType.Enabled = false;
        drpVCardNo.Enabled = false;
        drpECardNo.Enabled = false;
        drpMode.Items.Add(new ListItem("--Select--", "-1"));
        if (drpProcess.SelectedItem.Text == "Gate")
        {
            drpMode.Items.Add(new ListItem("In", "0"));
            drpMode.Items.Add(new ListItem("Out", "1"));
            drpMode.Items.Add(new ListItem("RoadTest In", "2"));
            drpMode.Items.Add(new ListItem("RoadTest Out", "3"));
        }
        else if ((drpProcess.SelectedItem.Text == "Normal Workshop") || (drpProcess.SelectedItem.Text == "Speedo Workshop"))
        {
            drpMode.Items.Add(new ListItem("In", "0"));
            drpMode.Items.Add(new ListItem("Out", "1"));
            drpMode.Items.Add(new ListItem("Hold", "2"));
            drpMode.Items.Add(new ListItem("Un Hold", "3"));
        }
        else
        {
            drpMode.Items.Add(new ListItem("In", "0"));
            drpMode.Items.Add(new ListItem("Out", "1"));
        }
    }
    protected void drpMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtTime.Text = DateTime.Now.ToString("HH:mm");
        if ((drpProcess.SelectedItem.Text == "Gate" && drpMode.SelectedItem.Text == "In") || (drpProcess.SelectedItem.Text == "Gate" && drpMode.SelectedItem.Text == "Out"))
        {
            
            drpCardType.Enabled = true;
            GetCardType();
            drpVCardNo.Enabled = false;
            drpECardNo.Enabled = false;
        }
        else if ((drpProcess.SelectedItem.Text == "JobSlip" && drpMode.SelectedItem.Text == "Out"))
        {

            drpCardType.Enabled = false;
            drpVCardNo.Enabled = false;
            drpECardNo.Enabled = true;
            GetEmployeeCardNo();
        }
        else if (drpProcess.SelectedItem.Text == "Wash")
        {
            drpCardType.Enabled = false;
            drpVCardNo.Enabled = true;
            GetVehicleCardNo();
            drpECardNo.Enabled = false;
        }
        else
        {

            drpCardType.Enabled = false;
            drpVCardNo.Enabled = true;
            drpECardNo.Enabled = true;
            GetVehicleCardNo();
            GetEmployeeCardNo();

        }
    }
    protected void drpCardType_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtTime.Text = DateTime.Now.ToString("HH:mm");
        drpVCardNo.Enabled = false;
        drpECardNo.Enabled = false;
        if (drpCardType.SelectedItem.Text=="Vehicle")
        {
            drpVCardNo.Enabled = true;
            GetVehicleCardNo();
        }
        else if (drpCardType.SelectedItem.Text=="Employee")
        {
            drpECardNo.Enabled = true;
            GetEmployeeCardNo();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
       if(drpProcess.SelectedItem.Text=="--Select--")
       {
           lblMsg.Text="Please Select Process.";
       }
       else if(drpMode.SelectedItem.Text=="--Select--")
       {
           lblMsg.Text="Please Select Function.";
       }
       else if(txtTime.Text.ToString()=="")
       {
            lblMsg.Text="Please Enter Time.";
       }
        else
       {
           try
        {
            DateTime pt = Convert.ToDateTime(txtDate.Text.Trim() + " " + txtTime.Text.Trim());
        SqlCommand cmd = new SqlCommand("udpDemoInsertScanData", con);
        con.Open();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@CardSwipTime", pt);
        cmd.Parameters.AddWithValue("@EmpCardNo",drpECardNo.SelectedValue.Trim());
        cmd.Parameters.AddWithValue("@VehTagNo",drpVCardNo.SelectedValue.Trim());
        cmd.Parameters.AddWithValue("@InOut",drpMode.SelectedValue.Trim());
        cmd.Parameters.AddWithValue("@SlNo",drpProcess.SelectedValue.Trim());
        SqlParameter Msg = cmd.Parameters.Add("@msg", SqlDbType.VarChar, 75);
        Msg.Direction = ParameterDirection.Output;
        Msg.Value = ""; 
        cmd.ExecuteNonQuery();
        lblMsg.Text=Msg.Value.ToString();
        Clear();
           }
        catch(Exception ex)
           {
              lblMsg.Text=ex.Message;
           }
           finally
           {
               con.Close();
           }
       }

    }
    protected void btnclr_Click(object sender, EventArgs e)
    {
        GetProcessDetails();
        txtDate.Text = DateTime.Now.Date.ToString("MM/dd/yyyy");
        txtTime.Text = DateTime.Now.ToString("HH:mm");
        drpMode.Items.Clear();
        drpMode.Items.Add(new ListItem("--Select--", "-1"));
        lblMsg.Text = "";
        drpCardType.Enabled = false;
        drpVCardNo.Enabled = false;
        drpECardNo.Enabled = false;

    }
}