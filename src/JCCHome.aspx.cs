using System;
using System.Web.UI;

public partial class JCCHome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["ROLE"] == null || Session["ROLE"].ToString() != "JOB SLIP")
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                lblVersion.Text = DataManager.getVersion();
                lbl_LoginName.Text = "Welcome, " + Session["UserId"].ToString();
                if (Session["CURRENT_PAGE"] != null)
                    lbl_CurrentPage.Text = Session["CURRENT_PAGE"].ToString();
                else
                    lbl_CurrentPage.Text = "";
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("login.aspx");
        }
        lbScroll0.Text = Session["DealerName"].ToString();
        lbl_LoginName.Text = "Welcome " + Session["UserId"].ToString();
    }

    protected void btnJobController_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("DisplayWorks.aspx");
    }

    protected void btnAppointments_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("JobCardCreation.aspx");
    }

    protected void btnBriefDisplay_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("BriefDisplay.aspx");
    }

    protected void btnJCD1_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("JobControllerDisplayI.aspx");
    }

    protected void btnJCD2_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("JobControllerDisplayII.aspx");
    }

    protected void btnFOD_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("FrontOfficeDisplayStatus.aspx");
    }

    protected void btn_logout_Click(object sender, EventArgs e)
    {
        Response.Redirect("login.aspx");
    }
}