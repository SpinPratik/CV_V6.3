using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using System.Web.UI;

public partial class AdminModule : System.Web.UI.MasterPage
{
    private static ArrayList serviceidList12 = new ArrayList();
   

    protected void Page_Load(object sender, EventArgs e)
    {
        lbl_currTime.Text = DateTime.Now.ToString("dd MMM yyy, HH:mm:ss");
        try
        {
            if (Session["ROLE"] == null || Session["ROLE"].ToString() != "ADMIN" && Session["ROLE"].ToString() != "FRONT OFFICE" && Session["ROLE"].ToString() != "SERVICE ADVISOR" && Session["ROLE"].ToString() != "WORK MANAGER" && Session["ROLE"].ToString() != "DISPLAY" && Session["ROLE"].ToString() != "CRM" && Session["ROLE"].ToString() != "SM" && Session["ROLE"].ToString() != "GMSERVICE" && Session["ROLE"].ToString() != "DEALER" && Session["ROLE"].ToString() != "CASHIER" && Session["ROLE"].ToString() != "REPORT" && Session["ROLE"].ToString() != "JOB SLIP" && Session["ROLE"].ToString() != "TEAM LEAD")
            {
                Response.Redirect("login.aspx");
            }
            if (Request.Url.ToString().Contains("AHome.aspx") || Request.Url.ToString().Contains("FrontOfficeHome.aspx") || Request.Url.ToString().Contains("SAHome.aspx") || Request.Url.ToString().Contains("JCHome.aspx") || Request.Url.ToString().Contains("CRMHome.aspx") || Request.Url.ToString().Contains("SMHome.aspx") || Request.Url.ToString().Contains("GMHome.aspx") || Request.Url.ToString().Contains("DPHome.aspx") || Request.Url.ToString().Contains("Complain.aspx") || Request.Url.ToString().Contains("Cashier.aspx") || Request.Url.ToString().Contains("JobCardOpen.aspx") || Request.Url.ToString().Contains("TeamleadHome.aspx"))
            {
                //btn_Back.Visible = false;
            }
            else if (Request.Url.ToString().Contains("Reports.aspx") || Request.Url.ToString().Contains("DisplayHome.aspx?Back=222") || Request.Url.ToString().Contains("DisplayHome.aspx?Back=333") || Request.Url.ToString().Contains("SADisplayHome.aspx"))
            {
                if (Session["ROLE"].ToString() == "ADMIN" || Session["ROLE"].ToString() == "CRM" || Session["ROLE"].ToString() == "GMSERVICE" || Session["ROLE"].ToString() == "DEALER" || Session["ROLE"].ToString() == "SM" || Session["ROLE"].ToString() == "DISPLAY")
                {
                    //btn_Back.Visible = true;
                    //btn_noBack.Visible = false;
                }
                else
                {
                    //btn_Back.Visible = false;
                }
            }
            else if (Request.Url.ToString().Contains("DisplayHome.aspx"))
            {
               // btn_Back.Visible = false;
            }
            else
            {
                //btn_Back.Visible = true;
                //btn_noBack.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("login.aspx");
        }

        try
        {
            if (!Page.IsPostBack)
            {
                //lbScroll0.Text = Session["DealerName"].ToString();
                lbl_LoginName.Text = Session["UserId"].ToString();
                //if (Session["CURRENT_PAGE"] != null)
                //    lbl_CurrentPage.Text = Session["CURRENT_PAGE"].ToString();
                //else
                //    lbl_CurrentPage.Text = "";
               
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("login.aspx");
        }
    }

    protected String GetPingDetails(String ip)
    {
        String Msg = "";
        Ping pingSender = new Ping();
        PingOptions options = new PingOptions();
        options.DontFragment = true;
        string data = "Status Checking...";
        byte[] buffer = System.Text.Encoding.ASCII.GetBytes(data);
        int timeout = 120;
        PingReply reply = pingSender.Send(ip, timeout, buffer, options);
        if (reply.Status == IPStatus.Success)
        {
            Msg += ("<font color='Black'>Active</font>");
        }
        else
        {
        }
        return Msg;
    }

    public void ShowNotification(string Userid, string title, string info, string Image, string isSticky, string StickTime, string CssClass, string Notificationtype)
    {
        string LatestState = "LoadGitter('" + Userid + "','" + title + "','" + info + "','" + Image + "'," + isSticky + "," + StickTime + ",'" + CssClass + "','" + Notificationtype + "');";
    }

    protected String GetDeviceAlert()
    {
        SqlConnection con = new SqlConnection(Session["Connectionstring"].ToString());
        String Msg = "<div style='border:none;background-color:#444545; layer-background-color:#003366;' class='bl'><b><font color='Black'>De-Active</font></b><br/><br/>";
        SqlCommand cmd = new SqlCommand("", con);
        cmd.CommandText = "SELECT IPAddress,DeviceName FROM tblDevices";
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        System.Data.DataTable dt = new System.Data.DataTable();
        try
        {
            dt.Clear();
            da.Fill(dt);
            if (dt.Rows.Count != 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (GetPingDetails(dt.Rows[i][0].ToString()) != "<font color='Black'>Active</font>")
                        Msg += "Device Name : " + dt.Rows[i][1].ToString() + "<br/>Address: " + dt.Rows[i][0].ToString() + "<br/>" + GetPingDetails(dt.Rows[i][0].ToString()) + "<br/>";
                }
            }
            else
            {
                Msg += "<br/>" + "No Device Listed!";
            }
        }
        catch (Exception ex)
        {
            Msg += "<br/>" + "No Device Listed!";
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
        return Msg + "</div>";
    }

    protected void btn_logout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();
        Response.Redirect("login.aspx");
    }

    protected void btn_Back_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["ROLE"].ToString() == "ADMIN")
        {
            Response.Redirect("AdminPage.aspx");
        }
        else if (Session["ROLE"].ToString() == "FRONT OFFICE")
        {
            Response.Redirect("FrontOfficeHome.aspx");
        }
        else if (Session["ROLE"].ToString() == "SERVICE ADVISOR")
        {
            Response.Redirect("SAHome.aspx");
        }
        else if (Session["ROLE"].ToString() == "WORK MANAGER")
        {
            Response.Redirect("JCRDisplayWorks.aspx");
        }
        else if (Session["ROLE"].ToString() == "CRM")
        {
            Response.Redirect("CRMHome.aspx");
        }
       
        else if (Session["ROLE"].ToString() == "GMSERVICE")
        {
            Response.Redirect("GMHome.aspx");
        }
        else if (Session["ROLE"].ToString() == "DEALER")
        {
            Response.Redirect("DPHome.aspx");
        }
        else if (Session["ROLE"].ToString() == "DISPLAY" && Request.Url.ToString().Contains("DisplayHome.aspx?Back=222"))
        {
            Session["Role"] = "SM";
            Response.Redirect("SMHome.aspx");
        }
        else if (Session["ROLE"].ToString() == "DISPLAY" && Request.Url.ToString().Contains("DisplayHome.aspx?Back=333"))
        {
            Session["Role"] = "GMSERVICE";
            Response.Redirect("GMHome.aspx");
        }
        else if (Session["ROLE"].ToString() == "DISPLAY")
        {
            Response.Redirect("DisplayHome.aspx");
        }
        else if(Session["ROLE"].ToString() == "SM" && Request.Url.ToString().Contains("SADisplayHome.aspx"))
        {
            Response.Redirect("SMHome.aspx");
        }
        else if (Session["ROLE"].ToString() == "SM")
        {
            Response.Redirect("SMHome.aspx");
           // Response.Redirect("SADisplayHome.aspx");
        }
    }

    protected void back_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("DisplayHome.aspx");
    }
}