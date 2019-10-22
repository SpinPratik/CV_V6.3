using System;
using System.Web.UI;

public partial class JCHome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["Current_Page"] = "JC Home";
        this.Title = "JC Home";
    }

    protected void btnJobController_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("JCRDisplayWorks.aspx");
    }

    protected void btnAppointments_Click(object sender, ImageClickEventArgs e)
    {
        //Response.Redirect("JobAllotment.aspx");
        Response.Redirect("NewJobAllotment.aspx");

    }

    protected void btnCRMDB_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("KPIDashboard.aspx");
    }
}