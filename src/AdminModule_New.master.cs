using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminModule_New : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string path = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
        //if (path.Contains("WorkTime.aspx"))
        //{
        //    back.OnClientClick = Response.Redirect("RFIDCards.aspx");
        //}

        lbl_currTime.Text = DateTime.Now.ToString("dd MMM yyyy, HH:mm:ss");
        lbl_LoginName.Text = Session["UserId"].ToString();
        try
        {
            //if (Session["ROLE"] == null || Session["ROLE"].ToString() != "ADMIN" && Session["ROLE"].ToString() != "FRONT OFFICE" && Session["ROLE"].ToString() != "SERVICE ADVISOR" && Session["ROLE"].ToString() != "WORK MANAGER" && Session["ROLE"].ToString() != "DISPLAY" && Session["ROLE"].ToString() != "CRM" && Session["ROLE"].ToString() != "SM" && Session["ROLE"].ToString() != "GMSERVICE" && Session["ROLE"].ToString() != "DEALER" && Session["ROLE"].ToString() != "CASHIER" && Session["ROLE"].ToString() != "REPORT" && Session["ROLE"].ToString() != "JOB SLIP" && Session["ROLE"].ToString() != "TEAM LEAD" && Session["ROLE"].ToString() != "BodyShop SA" && Session["ROLE"].ToString() != "BodyShop PositionDisplay")
            //{
            //    Response.Redirect("login.aspx");
            //}
            //if (Request.Url.ToString().Contains("AHome.aspx") || Request.Url.ToString().Contains("FrontOfficeHome.aspx") || Request.Url.ToString().Contains("SAHome.aspx") || Request.Url.ToString().Contains("JCHome.aspx") || Request.Url.ToString().Contains("CRMHome.aspx") || Request.Url.ToString().Contains("SMHome.aspx") || Request.Url.ToString().Contains("GMHome.aspx") || Request.Url.ToString().Contains("DPHome.aspx") || Request.Url.ToString().Contains("Complain.aspx") || Request.Url.ToString().Contains("Cashier.aspx") || Request.Url.ToString().Contains("JobCardOpen.aspx") || Request.Url.ToString().Contains("TeamleadHome.aspx"))
            //{
            //    btn_Back.Visible = false;
            //}
            //else if (Request.Url.ToString().Contains("Reports.aspx") || Request.Url.ToString().Contains("DisplayHome.aspx?Back=222") || Request.Url.ToString().Contains("DisplayHome.aspx?Back=333") || Request.Url.ToString().Contains("SADisplayHome.aspx"))
            //{
            //    if (Session["ROLE"].ToString() == "ADMIN" || Session["ROLE"].ToString() == "CRM" || Session["ROLE"].ToString() == "GMSERVICE" || Session["ROLE"].ToString() == "DEALER" || Session["ROLE"].ToString() == "SM" || Session["ROLE"].ToString() == "DISPLAY")
            //    {
            //        btn_Back.Visible = true;
            //        btn_noBack.Visible = false;
            //    }
            //    else
            //    {
            //        btn_Back.Visible = false;
            //    }
            //}
            //else if (Request.Url.ToString().Contains("DisplayHome.aspx"))
            //{
            //    btn_Back.Visible = false;
            //}
            //else
            //{
            //    btn_Back.Visible = true;
            //    btn_noBack.Visible = false;
            //}
        }
        catch (Exception ex)
        {
            Response.Redirect("login.aspx");
        }

        try
        {
            if (!Page.IsPostBack)
            {
                //lbScroll0.Text = Session["DealerName"].ToString();
                //lbl_LoginName.Text =  Session["UserId"].ToString();
                //lbl_LoginName.ForeColor = System.Drawing.Color.White;
                //if (Session["CURRENT_PAGE"] != null)
                //    lbl_CurrentPage.Text = Session["CURRENT_PAGE"].ToString();
                //else
                //    lbl_CurrentPage.Text = "";
           
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

    protected void back_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["ROLE"].ToString() == "ADMIN")
        {
            Response.Redirect("AdminPage.aspx");
        }
        else if (Session["ROLE"].ToString() == "FRONT OFFICE")
        {
            Response.Redirect("TagAllotment.aspx");
        }
        //else if (Session["ROLE"].ToString() == "SERVICE ADVISOR")
        //{
        //    Response.Redirect("SAHome.aspx");
        //}
        else if (Session["ROLE"].ToString() == "WORK MANAGER")
        {
            Response.Redirect("JCRDisplayWorks.aspx");
        }
        else if (Session["ROLE"].ToString() == "CRM")
        {
            Response.Redirect("CRMHome.aspx");
        }

        else if (Session["ROLE"].ToString() == "GMSERVICE")
        {
            Response.Redirect("GMHome.aspx");
        }
        else if (Session["ROLE"].ToString() == "DEALER")
        {
            Response.Redirect("DPHome.aspx");
        }
        else if (Session["ROLE"].ToString() == "DISPLAY" && Request.Url.ToString().Contains("DisplayHome.aspx?Back=222"))
        {
            Session["Role"] = "SM";
            Response.Redirect("SMHome.aspx");
        }
        else if (Session["ROLE"].ToString() == "DISPLAY" && Request.Url.ToString().Contains("DisplayHome.aspx?Back=333"))
        {
            Session["Role"] = "GMSERVICE";
            Response.Redirect("GMHome.aspx");
        }
        else if (Session["ROLE"].ToString() == "DISPLAY")
        {
            Response.Redirect("DisplayHome.aspx");
        }
        else if (Session["ROLE"].ToString() == "SM" && Request.Url.ToString().Contains("SADisplayHome.aspx"))
        {
            Response.Redirect("SMHome.aspx");
        }
        else if (Session["ROLE"].ToString() == "SM")
        {
            Response.Redirect("SMHome.aspx");
            //Response.Redirect("SADisplayHome.aspx");
        }

    }
}
