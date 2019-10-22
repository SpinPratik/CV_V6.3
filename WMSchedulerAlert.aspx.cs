using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Text;
using System.IO;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Collections.Specialized;

public partial class WMSchedulerAlert : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //SendSchSMSAlertWA(txtRegNO.Text.ToString(),txtCustName.Text.Trim());
        GetFlagForAlert();
    }

    private void GetFlagForAlert()
    {
        string Flag = "";
        string RegNo = "";
        string CustName = "";
        try
        {
            SqlConnection con = new SqlConnection("Data Source = 52.172.185.165; Initial Catalog = CVDemoTest; User ID = dbuser; Password = spin@1234; Connection TimeOut = 1200000; Pooling = False;");
            SqlCommand cmd = new SqlCommand("", con);
            cmd.CommandText = "GetWMSchduleAlerts";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Flag = dt.Rows[i]["Flag"].ToString();
                    RegNo = dt.Rows[i]["RegNo"].ToString();
                    CustName = dt.Rows[i]["CustomerName"].ToString();


                    String PhoneNo = "7892401901";

                    int Flag1 = Int32.Parse(Flag);

                    if (Flag1 == 1)
                    {
                        string Message = " Vehicle is upto for next schdule : " + RegNo + " ,Customer Name : " + CustName;
                        HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("http://login.wishbysms.com/api/sendhttp.php?authkey=78415A5xUuR7kyqi55e6b2f4&mobiles=" + PhoneNo + "&message=" + Message + " &sender=CONCRD&route=4&country=0 ");

                        HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();

                        System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());

                        string responseString = respStreamReader.ReadToEnd();

                        respStreamReader.Close();

                        myResp.Close();
                       
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
        //return Flag;

    }
}