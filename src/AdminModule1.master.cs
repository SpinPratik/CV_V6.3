using System;
using System.Data;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using System.Web.UI;

public partial class AdminModule1 : System.Web.UI.MasterPage
{
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

        try
        {
            if (!Page.IsPostBack)
            {
                lbScroll0.Text = Session["DealerName"].ToString();

                lbl_LoginName.Text = "Welcome, " + Session["UserId"].ToString();
                if (Session["CURRENT_PAGE"] != null)
                    lbl_CurrentPage.Text = Session["CURRENT_PAGE"].ToString();
                else
                    lbl_CurrentPage.Text = "";
                lblVersion.Text = getVersion();
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("login.aspx");
        }
    }
    public string getVersion()
    {
        string version = "";
      
        SqlConnection con = new SqlConnection(Session["ConnectionString"].ToString());
        SqlDataAdapter sda = new SqlDataAdapter("select top 1 * from ProductVersion order by DTM desc", con);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        if (dt.Rows.Count > 0)
            version = dt.Rows[0][0].ToString();
        return version;
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
        return Msg;
    }

    public void ShowNotification(string Userid, string title, string info, string Image, string isSticky, string StickTime, string CssClass, string Notificationtype)
    {
        string LatestState = "LoadGitter('" + Userid + "','" + title + "','" + info + "','" + Image + "'," + isSticky + "," + StickTime + ",'" + CssClass + "','" + Notificationtype + "');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Ajax2", LatestState, true);
    }

    public static void ShowAllNotification(string Userid, string title, string info, string Image, string isSticky, string StickTime, string CssClass, string Notificationtype)
    {
        string LatestState = "LoadGitter('" + Userid + "','" + title + "','" + info + "','" + Image + "'," + isSticky + "," + StickTime + ",'" + CssClass + "','" + Notificationtype + "');";
    }

    protected String GetDeviceAlert()
    {
        SqlConnection con = new SqlConnection(Session["ConnectionString"].ToString());
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

    //protected void btn_Back_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("AHome.aspx");
    //}

    //protected void btn_Back_Click(object sender, ImageClickEventArgs e)
    //{
    //    Session["CURRENT_PAGE"] = null;
    //    Response.Redirect("AHome.aspx");
    //}
}