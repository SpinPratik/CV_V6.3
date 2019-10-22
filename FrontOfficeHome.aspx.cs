using System;
using System.Collections;
using System.Data;
using System.Web.Services;
using System.Web.UI;

public partial class FrontOfficeHome : System.Web.UI.Page
{
    private static ArrayList serviceidList12 = new ArrayList();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lbScroll0.Text = Session["DealerName"].ToString();
            lbl_CurrentPage.Text = "Front Office Home";
            this.Title = "Front Office Home";
            lbl_LoginName.Text = "Welcome, " + Session["UserId"].ToString();
            lblVersion.Text = DataManager.getVersion();
        }
    }

    protected void btnFOE2_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("TagAllotment.aspx", false);
    }

    protected void btnFOD_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("FrontOfficeDisplayStatus.aspx", false);
    }

    public void ShowNotification(string Userid, string title, string info, string Image, string isSticky, string StickTime, string CssClass, string Notificationtype)
    {
        string LatestState = "LoadGitter('" + Userid + "','" + title + "','" + info + "','" + Image + "'," + isSticky + "," + StickTime + ",'" + CssClass + "','" + Notificationtype + "');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Ajax2", LatestState, true);
    }

    [WebMethod]
    public static string GetReply(string userId, string title, string info)
    {
        return "Hello " + userId + "\n title : " + title + "\n info : " + info;
    }

    protected void NotificationTimer_Tick(object sender, EventArgs e)
    {
        getAlertPopup();
    }

    public void ShowNotification(int Slno, int serviceid, int slno, string title, string info, string Image, string isSticky, string StickTime, string CssClass, string Notificationtype)
    {
        string LatestState = "LoadGitter('" + Slno + "','" + serviceid + "','" + slno + "','" + title + "','" + info + "','" + Image + "'," + isSticky + "," + StickTime + ",'" + CssClass + "','" + Notificationtype + "');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Ajax2", LatestState, true);
    }

    [WebMethod]
    public static void GetReply(int Slno, string title, string info)
    {
        DataManager.setProcessedStatus(Slno);
    }

    public void getAlertPopup()
    {
        DataTable alertTable = DataManager.getAlertTemplates("FO");
        if (alertTable.Rows.Count > 0)
        {
            for (int i = 0; i < alertTable.Rows.Count; i++)
            {
                string Header = alertTable.Rows[i]["AlertHeader"].ToString();
                string AlertBody = alertTable.Rows[i]["AlertTemplate"].ToString();
                if (!serviceidList12.Contains(alertTable.Rows[i]["SlNo"].ToString()))
                {
                    ShowNotification(alertTable.Rows[i]["SlNo"].ToString(), Header + " :", AlertBody, "Images/info.png", "true", "5000", "my-sticky-green", "callback");
                }
                if (!serviceidList12.Contains(alertTable.Rows[i]["SlNo"].ToString()))
                {
                    serviceidList12.Add((object)alertTable.Rows[i]["SlNo"].ToString());
                }
            }
        }
    }

    protected void btn_logout_Click(object sender, EventArgs e)
    {
        Response.Redirect("login.aspx");
    }
}