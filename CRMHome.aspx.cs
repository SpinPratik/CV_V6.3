using System;
using System.Web.UI;

public partial class CRMHome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["Current_Page"] = "CRM Home";
        this.Title = "CRM Home";
    }

    protected void btnCRMDisplay_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("DisplayWorks.aspx");
    }

    protected void btnReport_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Reports.aspx?Back=111");
    }

    protected void btnCRMDB_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("KPI_New.aspx");
    }
}