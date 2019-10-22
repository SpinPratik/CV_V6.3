﻿using System;
using System.Web.UI;

public partial class DisplayHome : System.Web.UI.Page
{
    private static string backto = "";

    protected void btn_logout_Click(object sender, EventArgs e)
    {
        Response.Redirect("login.aspx");
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            if (backto == "222")
            {
                Session["ROLE"] = "SM";
                Response.Redirect("SMHome.aspx", false);
            }
            else if (backto == "333")
            {
                Session["ROLE"] = "GMSERVICE";
                Response.Redirect("GMHome.aspx", false);
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void BtnBayDisplay_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("BayDisplay.aspx?Back=112");
    }

    protected void BtnCRMDisplay_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("DisplayWorks.aspx?Back=123");
    }

    protected void BtnCustomerDisplay_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("CustomerDisplay.aspx?Back=678");
    }

    protected void BtnCustomerDisplay1_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("CustomerDisplay2.aspx");
    }

    protected void BtnJobAllotmentDisplay_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("JobAllotmentDisplay.aspx?Back=456");
    }

    protected void BtnPositionDisplay_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("PositionDisplay.aspx?Back=789");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                Session["ROLE"] = "DISPLAY";
                Session["Current_page"] = "Display Home";
                this.Title = "Display Home";
            }
            catch (Exception ex)
            {
                Response.Redirect("login.aspx");
            }
        }
    }
}