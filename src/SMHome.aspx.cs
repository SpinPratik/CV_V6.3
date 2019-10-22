using System;
using System.Web.UI;

public partial class SMHome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["Current_page"] = "Service Manager Home";
        this.Title = "Service Manager Home";
    }

    protected void btnCRMDisplay_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("DisplayHome.aspx?Back=222");
    }

    protected void btnReport_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Reports.aspx?Back=222");
    }

    protected void btnCRMDB_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("KPI_New.aspx");
    }
}