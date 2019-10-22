using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SADisplayHome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                Session["ROLE"] = "SM";
                Session["Current_page"] = "SM Home";
                this.Title = "SM Home";
            }
            catch (Exception ex)
            {
                Response.Redirect("login.aspx");
            }
        }
    }
    protected void BtnCRMDisplay_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("DisplayWorks.aspx?Back=123");
    }
    protected void BtnJobAllotmentDisplay_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("JobAllotmentDisplay.aspx?Back=456");
    }
    protected void BtnFIDisplay_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("FIDisplay.aspx");
    }
    protected void BtnWashDisplay_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("WashDisplay.aspx");
    }
}