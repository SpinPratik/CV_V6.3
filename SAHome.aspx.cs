using System;
using System.Web.UI;

public partial class SAHome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["ROLE"] == null || Session["ROLE"].ToString() != "SERVICE ADVISOR")
            {
                Response.Redirect("login.aspx");
            }
            Session["CURRENT_PAGE"] = "SA Home";
            this.Title = "SA Home";
        }
        catch (Exception ex)
        {
            Response.Redirect("login.aspx");
        }
    }

    protected void btnJobController_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("NewJobCardCreation.aspx");
    }

    protected void btnJCDisplay_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("DisplayWorksI.aspx");
    }
}