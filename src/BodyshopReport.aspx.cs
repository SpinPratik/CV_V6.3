using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using Microsoft.Reporting.WebForms;

public partial class BodyshopReport : System.Web.UI.Page
{
    private static string DealerName;
    private static string Copyright;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session[Session["TMLDealercode"] + "-TMLConString"] == null || Session[Session["TMLDealercode"] + "-TMLConString"].ToString() == "")
                Response.Redirect("login.aspx");

        }
        catch
        {
            Response.Redirect("login.aspx");
        }
        DealerName = GetDealerDetails(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        if (!IsPostBack)
        {
            Copyright = getVersion();
            //  DateTime now = DateTime.Now;
            txtFrom.Text = DateTime.Today.AddDays(1 - DateTime.Today.Day).ToString("yyyy-MM-dd");

            TextTo.Text = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");

            //txtFrom.Attributes.Add("ReadOnly", "Readonly");
            //TextTo.Attributes.Add("ReadOnly", "Readonly");
        }
    }
  
    public string getVersion()
    {
        string version = "";
        string sConnString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        SqlConnection con = new SqlConnection(sConnString);
        SqlDataAdapter sda = new SqlDataAdapter("select top 1 * from ProductVersion order by DTM desc", con);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        if (dt.Rows.Count > 0)
            version = dt.Rows[0][0].ToString();
        return version;
    }
    public string GetDealerDetails(string Connection)
    {
        try
        {
            SqlConnection con = new SqlConnection(Connection);
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

    protected void btnShow_Click(object sender, EventArgs e)
    {
        if (txtFrom.Text.ToString()=="" || TextTo.Text.ToString()=="")
        {
            lblmsg.Text = "Please select From Date and To Date";
            lblmsg.ForeColor = System.Drawing.Color.Red;
        }
        else if ((Convert.ToDateTime(txtFrom.Text.ToString()))>=(Convert.ToDateTime(TextTo.Text.ToString())))
        {
            lblmsg.Text = "Please select valid From Date and To Dates";
            lblmsg.ForeColor = System.Drawing.Color.Red;
        }
        else
        {
            ShowData();
        }
       
    }

    private void ShowData()
    {
        Copyright = getVersion();
        try { 
        //string str = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ConnectionString;
        string str = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        SqlConnection con = new SqlConnection(str);
        RptViewer.Reset();
        //datasource
        DataTable dt = getTable();
        ReportDataSource rds = new ReportDataSource("DataSet1", dt);
        RptViewer.LocalReport.DataSources.Add(rds);
         
        //path
        RptViewer.LocalReport.ReportPath = "TAT_BSRpt.rdlc";
         
            //params
        ReportParameter[] rptparams = new ReportParameter[4];
        //{
        rptparams[0] = new ReportParameter("FromDate", txtFrom.Text);
        rptparams[1]= new ReportParameter("ToDate", TextTo.Text);
            rptparams[2] = new ReportParameter("DealerDetails", DealerName);
            rptparams[3] = new ReportParameter("Copyright", Copyright);
            // };
            RptViewer.LocalReport.SetParameters(rptparams);
        //refresh
        RptViewer.LocalReport.Refresh();
        }
        catch (Exception ex)
        {

        }
    }
    private DataTable getTable()
    {

        DataTable dt = new DataTable();
        // string str = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ConnectionString;
        string str = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        SqlConnection con = new SqlConnection(str);
        SqlCommand cmd = new SqlCommand("udpBodyShopTATreport", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@FromDate", txtFrom.Text);
        cmd.Parameters.AddWithValue("@ToDate", TextTo.Text);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        sda.Fill(dt);
        return dt;
    }
}