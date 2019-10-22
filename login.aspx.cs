using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

public partial class login : System.Web.UI.Page
{
    private DataTable result;
    private string EmpId;
    //public static string ConnectionStr;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Response.Buffer = true;
            Response.ExpiresAbsolute = System.DateTime.Now.Subtract(new TimeSpan(24, 0, 0));
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            result = new DataTable();
            txt_UserName.Text = "";
            txt_Password.Text = "";
            txt_UserName.Focus();
            //lblVersion.Text = DataManager.getVersion();
        }
    }
  
    protected void txt_DealerCode_TextChanged(object sender, EventArgs e)
    {
        err_Message.CssClass = "reset";
        err_Message.Text = "";
        try
        {

            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + err_Message.ClientID + "').style.display='none'\",5000)</script>");
            string str = ConfigurationManager.ConnectionStrings["Cloud_WMS_ConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(str);
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            string Dealercode = txt_DealerCode.Text;
            Session["TMLDealercode"] = txt_DealerCode.Text.Trim();
            string query = "SELECT DatabaseName FROM Tbl_CurrentDealers WHERE Dealercode=@Dealercode";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@DealerCode", Dealercode);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                // Session[Session["TMLDealercode"] + "-TMLConString"] = "Data Source=spindataserver.czpfmzyw26j6.ap-south-1.rds.amazonaws.com;Initial Catalog=" + dr.GetValue(0).ToString() + ";User ID=spintech_admin;Password=dataserver;Connection TimeOut=1200000; Pooling=False;Max Pool Size=5000;Min Pool Size=1";
                Session[Session["TMLDealercode"] + "-TMLConString"] = "Data Source=52.172.185.165;Initial Catalog=" + dr.GetValue(0).ToString() + ";User ID=dbuser;Password=spin@1234;Connection TimeOut=1200000; Pooling=False;";
                //Session[Session["TMLDealercode"] + "-TMLConString"] = "Data Source=192.168.1.107;Initial Catalog=" + dr.GetValue(0).ToString() + ";User ID=sa;Password=admin@123;Connection TimeOut=1200000; Pooling=False;Max Pool Size=5000;Min Pool Size=1";

              
              //  ConnectionStr = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();

                conn.Close();

            }
            else
            {
                err_Message.CssClass = "ErrMsg";
                txt_UserName.Text = "";
                txt_Password.Text = "";
                err_Message.ForeColor = System.Drawing.Color.Red;
                err_Message.Text = "Please Enter Valid DealerCode";
                txt_DealerCode.Focus();
            }
        }
        catch (Exception ex)
        {

        }
    }

    public static string GetDealerDetails(string Connection)
    {
        try
        {
            //string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
            SqlConnection con = new SqlConnection(System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["TMLDealercode"] + "-TMLConString"].ToString());
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "udpGetDealerDetails";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count != 0)
                return dt.Rows[0][0].ToString();
            else
                return "-Dealer Name, Place-";
        }
        catch (Exception ex)
        {
            return ">Dealer Name, Place<";
        }
    }

    protected void btn_Login_Click(object sender, EventArgs e)
    {
        err_Message.CssClass = "ErrMsg";
        ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + err_Message.ClientID + "').style.display='none'\",5000)</script>");

        if (Session[Session["TMLDealercode"] + "-TMLConString"] == null || Session[Session["TMLDealercode"] + "-TMLConString"].ToString() == "")
        {
            err_Message.Text = "Invalid Dealer Code. !";
        }
        else if (txt_UserName.Text.Trim().ToString() == "")
        {
            err_Message.Text = "Please Enter Username";
            txt_UserName.Focus();
        }
        else if (txt_Password.Text.Trim().ToString() == "")
        {
            err_Message.Text = "Please Enter Password";
            txt_Password.Focus();
        }
        else if (txt_DealerCode.Text.ToString()=="")
        {
            err_Message.Text = "Please Enter Dealer Code";
            txt_DealerCode.Focus();
        }
        else
        {
           Session["DealerName"] = "License To : " + GetDealerDetails(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
            Session["BACKROLE"] = "";
            SqlCommand cmd = new SqlCommand("", new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString()));
            cmd.CommandText = "GetLoginDetails";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserName", txt_UserName.Text.ToString());
            cmd.Parameters.AddWithValue("@Password", txt_Password.Text.ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][1].ToString().ToUpper().Trim() == "ADMIN")
                {
                    Session["EmpId"] = dt.Rows[0]["EmpId"].ToString().Trim();
                    Session["UserId"] = dt.Rows[0][0].ToString().Trim();
                    Session["ROLE"] = "ADMIN";
                    Response.Redirect(dt.Rows[0]["RedirectPage"].ToString().Trim());
                }
                else if (dt.Rows[0][1].ToString().ToUpper().Trim() == "WORK MANAGER")
                {
                    Session["EmpId"] = dt.Rows[0]["EmpId"].ToString().Trim();
                    Session["UserId"] = dt.Rows[0][0].ToString().Trim();
                    Session["ROLE"] = "WORK MANAGER";
                    Response.Redirect(dt.Rows[0]["RedirectPage"].ToString().Trim());
                }
                else if (dt.Rows[0][1].ToString().ToUpper().Trim() == "REPORT")
                {
                    Session["EmpId"] = dt.Rows[0]["EmpId"].ToString().Trim();
                    Session["UserId"] = dt.Rows[0][0].ToString().Trim();
                    Session["ROLE"] = "REPORT";
                    Response.Redirect(dt.Rows[0]["RedirectPage"].ToString().Trim());
                }
                else if (dt.Rows[0][1].ToString().ToUpper().Trim() == "JOB SLIP")
                {
                    Session["UserId"] = dt.Rows[0][0].ToString().Trim();
                    Session["ROLE"] = "JOB SLIP";
                    Response.Redirect(dt.Rows[0]["RedirectPage"].ToString().Trim());
                }
                else if (dt.Rows[0][1].ToString().ToUpper().Trim() == "DISPLAY")
                {
                    Session["EmpId"] = dt.Rows[0]["EmpId"].ToString().Trim();
                    Session["UserId"] = dt.Rows[0][0].ToString().Trim();
                    Session["ROLE"] = "DISPLAY";
                    Session["BACKROLE"] = "DISPLAY";
                    Response.Redirect(dt.Rows[0]["RedirectPage"].ToString().Trim());
                }
                else if (dt.Rows[0][1].ToString().ToUpper().Trim() == "SERVICE ADVISOR")
                {
                    Session["EmpId"] = dt.Rows[0]["EmpId"].ToString().Trim();
                    Session["UserId"] = txt_UserName.Text.Trim().ToString();
                    Session["ROLE"] = "SERVICE ADVISOR";
                    EmpId = dt.Rows[0]["EmpId"].ToString().Trim();
                    getSADetails();
                    Response.Redirect(dt.Rows[0]["RedirectPage"].ToString().Trim());
                }
                else if (dt.Rows[0][1].ToString().ToUpper().Trim() == "TEAM LEAD")
                {
                    Session["EmpId"] = dt.Rows[0]["EmpId"].ToString().Trim();
                    Session["UserId"] = txt_UserName.Text.Trim().ToString();
                    Session["ROLE"] = "TEAM LEAD";
                    EmpId = dt.Rows[0]["EmpId"].ToString().Trim();
                    Response.Redirect(dt.Rows[0]["RedirectPage"].ToString().Trim());
                }
                else if (dt.Rows[0][1].ToString().ToUpper().Trim() == "ROAD TEST")
                {
                    Session["EmpId"] = dt.Rows[0]["EmpId"].ToString().Trim();
                    Session["UserId"] = dt.Rows[0][0].ToString().Trim();
                    Session["ROLE"] = "ROAD TEST";
                    Response.Redirect(dt.Rows[0]["RedirectPage"].ToString().Trim());
                }
                else if (dt.Rows[0][1].ToString().ToUpper().Trim() == "PARTS")
                {
                    Session["EmpId"] = dt.Rows[0]["EmpId"].ToString().Trim();
                    Session["UserId"] = dt.Rows[0][0].ToString().Trim();
                    Session["ROLE"] = "PARTS";
                    Response.Redirect(dt.Rows[0]["RedirectPage"].ToString().Trim());
                }
                else if (dt.Rows[0][1].ToString().ToUpper().Trim() == "FRONT OFFICE")
                {
                    Session["EmpId"] = dt.Rows[0]["EmpId"].ToString().Trim();
                    Session["UserId"] = dt.Rows[0][0].ToString().Trim();
                    Session["ROLE"] = "FRONT OFFICE";
                    Response.Redirect(dt.Rows[0]["RedirectPage"].ToString().Trim());
                }
                else if (dt.Rows[0][1].ToString().ToUpper().Trim() == "WORK MANAGER DISPLAY")
                {
                    Session["EmpId"] = dt.Rows[0]["EmpId"].ToString().Trim();
                    Session["UserId"] = dt.Rows[0][0].ToString().Trim();
                    Session["ROLE"] = "WORK MANAGER DISPLAY";
                    Response.Redirect(dt.Rows[0]["RedirectPage"].ToString().Trim());
                }
                else if (dt.Rows[0][1].ToString().ToUpper().Trim() == "CRM")
                {
                    Session["EmpId"] = dt.Rows[0]["EmpId"].ToString().Trim();
                    Session["UserId"] = dt.Rows[0][0].ToString().Trim();
                    Session["ROLE"] = "CRM";
                    Response.Redirect(dt.Rows[0]["RedirectPage"].ToString().Trim());
                }
                else if (dt.Rows[0][1].ToString().ToUpper().Trim() == "DEALERDASHBOARD")
                {
                    Session["UserId"] = dt.Rows[0][0].ToString().Trim();
                    Session["ROLE"] = "DEALER";
                    Response.Redirect(dt.Rows[0]["RedirectPage"].ToString().Trim());
                }
                else if (dt.Rows[0][1].ToString().ToUpper().Trim() == "GMSERVICE")
                {
                    Session["UserId"] = dt.Rows[0][0].ToString().Trim();
                    Session["ROLE"] = "GMSERVICE";
                    Session["BACKROLE"] = "GMSERVICE";
                    Response.Redirect(dt.Rows[0]["RedirectPage"].ToString().Trim());
                }
                else if (dt.Rows[0][1].ToString().ToUpper().Trim() == "SERVICE MANAGER")
                {
                    Session["UserId"] = dt.Rows[0][0].ToString().Trim();
                    Session["ROLE"] = "SM";
                    Session["BACKROLE"] = "SM";
                    Response.Redirect(dt.Rows[0]["RedirectPage"].ToString().Trim());
                }
                else if (dt.Rows[0][1].ToString().ToUpper().Trim() == "CASHIER")
                {
                    Session["EmpId"] = dt.Rows[0]["EmpId"].ToString().Trim();
                    Session["UserId"] = dt.Rows[0][0].ToString().Trim();
                    Session["Role"] = "CASHIER";
                    Response.Redirect(dt.Rows[0]["RedirectPage"].ToString().Trim());
                }
                else if (dt.Rows[0][1].ToString() == "BodyShop SA")
                {
                    Session["EmpId"] = dt.Rows[0]["EmpId"].ToString().Trim();
                    Session["UserId"] = dt.Rows[0][0].ToString().Trim();
                    Session["Role"] = "BodyShop SA";
                    Response.Redirect(dt.Rows[0]["RedirectPage"].ToString().Trim());
                }
                else if (dt.Rows[0][1].ToString() == "BodyShop PositionDisplay")
                {
                    Session["UserId"] = dt.Rows[0][0].ToString().Trim();
                    Session["Role"] = "BodyShop PositionDisplay";
                    Response.Redirect(dt.Rows[0]["RedirectPage"].ToString().Trim());
                }
                else if (dt.Rows[0][1].ToString() == "Bodyshop TAT Report")
                {
                    Session["UserId"] = dt.Rows[0][0].ToString().Trim();
                    Session["Role"] = "BodyShop PositionDisplay";
                    Response.Redirect(dt.Rows[0]["RedirectPage"].ToString().Trim());
                }
                else 
                {
                    Session["EmpId"] = dt.Rows[0]["EmpId"].ToString().Trim();
                    Session["UserId"] = dt.Rows[0][0].ToString().Trim();
                    Session["ROLE"] = dt.Rows[0][1].ToString().ToUpper().Trim();
                    Response.Redirect(dt.Rows[0]["RedirectPage"].ToString().Trim());
                }
            }
            else
            {
                err_Message.Text = "UserName & Password Not Matched. !";
                txt_UserName.Text = "";
                txt_Password.Text = "";
                txt_UserName.Focus();
            }
        }
    }

  
    protected void btn_Clear_Click(object sender, EventArgs e)
    {
        txt_DealerCode.Text = "";
        err_Message.CssClass = "reset";
        err_Message.Text = "";
        txt_UserName.Text = "";
        txt_Password.Text = "";
        txt_UserName.Focus();
    }

    private void getSADetails()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("select EmpName from tblEmployee where EmpId='" + EmpId + "' and CardNo<>''", new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString()));
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Session["Empname"] = dt.Rows[0]["Empname"].ToString().Trim();
            }
        }
        catch (Exception ex)
        {
        }
    }

   
}