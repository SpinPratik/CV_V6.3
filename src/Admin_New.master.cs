using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_New : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lbl_currTime.Text = DateTime.Now.ToString("dd MMM yyyy, HH:mm:ss");
    
        try
        {
            if (Session["ROLE"] == null || Session["ROLE"].ToString() != "ADMIN" && Session["ROLE"].ToString() != "FRONT OFFICE" && Session["ROLE"].ToString() != "SERVICE ADVISOR" && Session["ROLE"].ToString() != "WORK MANAGER" && Session["ROLE"].ToString() != "DISPLAY" && Session["ROLE"].ToString() != "CRM" && Session["ROLE"].ToString() != "SM" && Session["ROLE"].ToString() != "GMSERVICE" && Session["ROLE"].ToString() != "DEALER" && Session["ROLE"].ToString() != "CASHIER" && Session["ROLE"].ToString() != "REPORT" && Session["ROLE"].ToString() != "JOB SLIP" && Session["ROLE"].ToString() != "TEAM LEAD")
            {
                Response.Redirect("login.aspx");
            }
            
        }
        catch (Exception ex)
        {
            Response.Redirect("login.aspx");
        }

        try
        {
            if (!Page.IsPostBack)
            {
                lbl_LoginName.Text = Session["UserId"].ToString();
                lbl_LoginName.ForeColor = System.Drawing.Color.White;
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("login.aspx");
        }
    }

    protected void btn_logout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();
        Response.Redirect("login.aspx");
    }
}
