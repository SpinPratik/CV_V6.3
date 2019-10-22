using System;
using System.Web.UI;

public partial class DPHome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["Current_Page"] = "CRM Home";
        this.Title = "DP Home";
    }

    protected void btnCRMDisplay_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("DisplayWorks.aspx");
    }

    protected void btnReport_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("DPReports.aspx?Back=111");
    }

    protected void btnCRMDB_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("KPI_New.aspx");
    }
}