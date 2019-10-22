using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TeamLeadHome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["Current_Page"] = "TL Home";
        this.Title = "TL Home";
    }
    protected void btnDisplay_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Baydisplay_TLWise.aspx");
    }
    protected void btnAllotment_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("JobAllotment_TeamLeadWise.aspx");
    }
}