using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using Microsoft.Reporting.WebForms;

public partial class reportSample : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        ShowData();
    }

    private void ShowData()
    {
        string str = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(str);
        RptViewer.Reset();
        //datasource
        DataTable dt = getTable();
        ReportDataSource rds = new ReportDataSource("DataSet1", dt);
        RptViewer.LocalReport.DataSources.Add(rds);

        //path
        RptViewer.LocalReport.ReportPath = "Report_sample.rdlc";

        //params
        ReportParameter[] rptparams = new ReportParameter[]
        {
            new ReportParameter("Bodyshop",txtFrom.Text)
        };
        RptViewer.LocalReport.SetParameters(rptparams);
        //refresh
        RptViewer.LocalReport.Refresh();
    }
    private DataTable getTable()
    {
        DataTable dt = new DataTable();
        string str = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(str);
        SqlCommand cmd = new SqlCommand("GetRPTDealerDashboard", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Bodyshop",0);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        sda.Fill(dt);
        return dt;
    }
}