using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class WashDisplay : System.Web.UI.Page
{
  //  private SqlConnection dbaseCon = new SqlConnection(ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session[Session["TMLDealercode"] + "-TMLConString"].ToString() == null || Session[Session["TMLDealercode"] + "-TMLConString"].ToString() == "")
                Response.Redirect("login.aspx");
        }
        catch
        {
            Response.Redirect("login.aspx");
        }
        lbl_currTime.Text = DateTime.Now.ToString("dd MMM yyyy, HH:mm:ss");
        FillVehicleDashboard();
        try { 
        lbl_LoginName.Text = Session["UserId"].ToString();
        }
        catch(Exception ex)
        {
            Response.Redirect("login.aspx");
        }
    }

    private DataTable GetVehicles(string procedureName)
    {
        SqlConnection dbaseCon = new SqlConnection(System.Web.HttpContext.Current.Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        try
        {
            if (dbaseCon.State == ConnectionState.Closed)
                dbaseCon.Open();
            SqlDataAdapter da = new SqlDataAdapter(procedureName, dbaseCon);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@ProcessName", "Wash");
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            if (dbaseCon.State == ConnectionState.Open)
                dbaseCon.Close();
        }
    }

    public void FillVehicleDashboard()
    {
        try
        {
            pnl_Display.Controls.Clear();
            string procName = "";
            string title = "";
            string titleColor = "";
            for (int i = 0; i < 3; i++)
            {
                if (i == 0)
                {
                    procName = "udpVehicleWaitingforProcess";
                    //title = "WAITING FOR PROCESS";
                    title = "Waiting for Process";
                    titleColor = "#5f9eee";
                   
                }
                else if (i == 1)
                {
                    procName = "udpVehicleWorkInProgress";
                    //title = "WORK IN PROGRESS";
                    title = "Work in Progress";
                    titleColor = "#76c187";
                }
                else if (i == 2)
                {
                    procName = "udpVehicleWorkComplete";
                    //title = "WORK COMPLETED";
                    title = "Work Completed";
                    titleColor = "#9e7da6";                    
                }

                var cell = new HtmlTableCell();
                cell.VAlign = "Top";
                cell.Style.Value = "width:33.33%;";

                DataTable dtVehicle = new DataTable();
                dtVehicle = GetVehicles(procName);
                int totalVehicle = dtVehicle.Rows.Count;

                Panel pnl = new Panel();
                pnl.ID = ("pnl_" + i.ToString());
                pnl.Style.Value = "width:100%;height:627px;background-color:#FFFFFF;overflow:auto;";
                for (int j = 0; j < totalVehicle; j++)
                {
                    Vehicle vt = (Vehicle)Page.LoadControl("Vehicle.ascx");
                    vt.RegNo = dtVehicle.Rows[j]["RegNo"].ToString();
                    vt.Model = dtVehicle.Rows[j]["VehicleModel"].ToString();
                    vt.VehicleImage = DataManager.car_image(dtVehicle.Rows[j]["ModelImageUrl"].ToString());
                    vt.PDT = dtVehicle.Rows[j]["PDT"].ToString();
                    vt.PDTImage = DataManager.jcr_image(dtVehicle.Rows[j]["PDTStatus"].ToString());
                    vt.GateInTime = dtVehicle.Rows[j]["GateIn"].ToString();
                    vt.ServiceAdvisor = dtVehicle.Rows[j]["ServiceAdvisor"].ToString();
                    vt.CWJDPImage = DataManager.jcr_image(dtVehicle.Rows[j]["CWJDP"].ToString());
                    pnl.Controls.Add(vt);
                }


                Label lbl = new Label();
                lbl.Style.Value = "width:100%;background-color:" + titleColor + ";vertical-align:middle;color:#FFFFFF !important; font-weight:bold;padding-left:5px;font-size:16px;text-transform:unset !important;";
                lbl.Height = new Unit(24);
                lbl.Width = new Unit(100);
                lbl.Text = "<table style='width: 100%; height: 100%;' border='0' cellspacing='0' cellpadding='0'><tr><td style='white-space:nowrap;'>" + title + "</td><td style='white-space:nowrap;text-align:right;padding-right:15px;'>Count : " + totalVehicle.ToString() + "</td></tr></table>";
                cell.Controls.Add(lbl);

                cell.Controls.Add(pnl);
                pnl_Display.Cells.Add(cell);
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {
        //try
        //{
        //    if (Session["Process"].ToString() == "Wash")
        //        Session["Process"] = "Workshop";
        //    else
        //        Session["Process"] = "Wash";
        //}
        //catch (Exception ex)
        //{
        //    Session["Process"] = "Wash";
        //}
        Page_Load(null, null);
    }

    protected void btn_logout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();
        Response.Redirect("login.aspx");
    }
    protected void btn_Back_Click(object sender, ImageClickEventArgs e)
    {
        
         if (Session["ROLE"].ToString() == "DISPLAY" && Request.Url.ToString().Contains("WashDisplay.aspx") && Session["BACKROLE"].ToString() == "SM")
        {
            Response.Redirect("DisplayHome.aspx?Back=222");
        }
         else if (Session["ROLE"].ToString() == "DISPLAY" && Request.Url.ToString().Contains("WashDisplay.aspx") && Session["BACKROLE"].ToString() == "GMSERVICE")
        {
            Response.Redirect("DisplayHome.aspx?Back=333");
        }
         else if (Session["ROLE"].ToString() == "DISPLAY" && Request.Url.ToString().Contains("WashDisplay.aspx") && Session["BACKROLE"].ToString() == "DISPLAY")
        {
            Response.Redirect("DisplayHome.aspx");
        }
         else if ( Request.Url.ToString().Contains("WashDisplay.aspx") && Session["ROLE"].ToString() == "SM")
         {
             Response.Redirect("SADisplayHome.aspx");
         }
    }

    protected void back_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("DisplayHome.aspx");
    }
}