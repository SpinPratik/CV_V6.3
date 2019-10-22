using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Web.UI;

public partial class SyncMaster : System.Web.UI.Page
{
    private Database db = new Database();
    private string responseString = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["ROLE"] == null || Session["ROLE"].ToString() != "ADMIN")
            {
                Response.Redirect("login.aspx");
            }
            Session["Current_Page"] = "Sync Master";
            this.Title = "Sync Master";
        }
        catch (Exception ex)
        {
            Response.Redirect("login.aspx");
        }
        {
            TMLServices.GetAuthentication();
        }
        //if (!Page.IsPostBack)
        //{
        //}
    }  

    protected void btn_JobCode_Click(object sender, ImageClickEventArgs e)
    {
        if (TMLServices.securityKey != "")
        {
            PopupPanel.Visible = true;
            divJobCode.Visible = true;
            divAthuntication.Visible = false;
            pnl_updateStatus.Visible = false;
            txtFromDateJobCode.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
            txtToDateJobCode.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
        }
        else
        {
            PopupPanel.Visible = true;
            divJobCode.Visible = false;
            pnl_updateStatus.Visible = false;
            divAthuntication.Visible = true;
        }
    }

    protected void btn_PriceList_Click(object sender, ImageClickEventArgs e)
    {
        if (TMLServices.securityKey != "")
        {
            string rateListId = db.GetRateListIDS();
            if (rateListId == "")
            {
                PopupPanel.Visible = true;
                pnl_updateStatus.Visible = true;
                divJobCode.Visible = false;
                divAthuntication.Visible = false;
                lbl_Status.Text = "Please, First Update Rate List. !";
            }
            else
            {
                PopupPanel.Visible = true;
                pnl_updateStatus.Visible = true;
                divJobCode.Visible = false;
                divAthuntication.Visible = false;
                lbl_Status.Text = "Updating Price List Details. Please Wait.....";
                String request = "<SOAP:Envelope xmlns:SOAP=\"http://schemas.xmlsoap.org/soap/envelope/\">"
                                + "<SOAP:Header>"
                                + "<wsse:Security xmlns:wsse=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd\">"
                                + TMLServices.securityKey
                                + "</wsse:Security>"
                                + "</SOAP:Header>"
                                + "<SOAP:Body>"
                                + "<GetPriceListItemsByRowIdsMaster xmlns=\"VTABSSblWSApp\" preserveSpace=\"no\" qAccess=\"0\" qValues=\"\">"
                                + "<ROWIDS>"
                                + rateListId
                                + "</ROWIDS>"
                                + "</GetPriceListItemsByRowIdsMaster>"
                                + "</SOAP:Body>"
                                + "</SOAP:Envelope>";

                responseString = Create_Webrequest(request);
                lbl_Status.Text = UpdateDatabase(responseString, "GetPriceListItemsByRowIdsMaster");
            }
        }
        else
        {
            PopupPanel.Visible = true;
            divJobCode.Visible = false;
            pnl_updateStatus.Visible = false;
            divAthuntication.Visible = true;
        }
    }

    protected void btn_RateList_Click(object sender, ImageClickEventArgs e)
    {
        if (TMLServices.securityKey != "")
        {
            PopupPanel.Visible = true;
            pnl_updateStatus.Visible = true;
            divJobCode.Visible = false;
            divAthuntication.Visible = false;
            lbl_Status.Text = "Updating Rate List Details. Please Wait.....";
            String request = "<SOAP:Envelope xmlns:SOAP=\"http://schemas.xmlsoap.org/soap/envelope/\">"
                            + "<SOAP:Header>"
                            + "<wsse:Security xmlns:wsse=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd\">"
                            + TMLServices.securityKey
                            + "</wsse:Security>"
                            + "</SOAP:Header>"
                            + "<SOAP:Body>"
                            + "<GetRateListByBuIdMaster xmlns=\"VTABSSblWSApp\" preserveSpace=\"no\" qAccess=\"0\" qValues=\"\">"
                            + "<BUID>"
                            + ConfigurationManager.AppSettings["OriginId"].ToString()
                            + "</BUID>"
                            + "</GetRateListByBuIdMaster>"
                            + "</SOAP:Body>"
                            + "</SOAP:Envelope>";

            responseString = Create_Webrequest(request);
            lbl_Status.Text = UpdateDatabase(responseString, "GetRateListByBuIdMaster");
        }
        else
        {
            PopupPanel.Visible = true;
            divJobCode.Visible = false;
            pnl_updateStatus.Visible = false;
            divAthuntication.Visible = true;
        }
    }

    private string Create_Webrequest(string requestData)
    {
        try
        {
            //if (TMLServices.securityKey != "")
            //{
            WebRequest request = WebRequest.Create("https://tmcrmapps.inservices.tatamotors.com/cordys/com.eibus.web.soap.Gateway.wcp?organization=o=B2C,cn=cordys,cn=cbop,o=tatamotors.com");
            request.Method = "POST";
            byte[] byteArray = Encoding.UTF8.GetBytes(requestData);
            request.ContentType = "text/xml";
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();
            //if (((HttpWebResponse)response).StatusDescription == "OK")
            //{
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseData = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            response.Close();
            return responseData;
            //}
            //else
            //{
            //    PopupPanel.Visible = true;
            //    divComplaintCode.Visible = false;
            //    divJobCode.Visible = false;
            //    divAthuntication.Visible = true;
            //    return "";
            //}
            //}
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    private string UpdateDatabase(string xmlresponse, string ServiceName)
    {
        try
        {
            string status = "";
            StringReader theReader = new StringReader(xmlresponse.ToString().Replace("&gt;", ">").Replace("&lt;", "<").Replace("xmlns=\"\"", "xmlns=").Replace("&quot;", "\""));
            DataSet ds = new DataSet();
            ds.ReadXml(theReader);
            if (xmlresponse.ToString().Contains("GetRateListByBuIdMasterResponse"))
            {
                status = db.Insert_tRateListByBuIdMaster(ds);
                return status;
            }
            else if (xmlresponse.ToString().Contains("GetPriceListItemsByRowIdsMasterResponse"))
            {
                status = db.Insert_PriceListItemsMaster(ds);
                return status;
            }
            else if (xmlresponse.ToString().Contains("GetJobCodeMaster"))
            {
                status = db.Insert_JobCodeMaster(ds);                
                return status;
            }
            else
            {
                return "UNKNOWN SERVICE..";
            }
        }
        catch (Exception ex)
        {
            try
            {
                TMLServices.Error_Tracker(ServiceName, ex.InnerException.Message.ToString(), Session["UserName"].ToString());
            }
            catch (Exception ex1) { }
            return "Error : " + ex.Message.ToString();
        }
    }

    protected void btn_Close_Click(object sender, EventArgs e)
    {
        lbl_Status.Text = "";
        PopupPanel.Visible = false;
    }
  
    protected void btnFetchJobCode_Click(object sender, EventArgs e)
    {
        if (txtFromDateJobCode.Text.ToString() == "")
        {
            lblErrorJobCode.Text = "Please Enter From Date.";
        }
        else if (txtToDateJobCode.Text.ToString() == "")
        {
            lblErrorJobCode.Text = "Please Enter To Date.";
        }
        else if (Convert.ToDateTime(txtFromDateJobCode.Text.Trim()) > Convert.ToDateTime(txtToDateJobCode.Text.Trim()))
        {
            lblErrorJobCode.Text = "Please Select Proper Date.";
        }
        else
        {
            if (TMLServices.securityKey != "")
            {
                PopupPanel.Visible = true;
                divJobCode.Visible = true;
                divAthuntication.Visible = false;
                lblErrorJobCode.Text = "Updating Current Job Code Details. Please Wait.....";
                String request = "<SOAP:Envelope xmlns:SOAP=\"http://schemas.xmlsoap.org/soap/envelope/\">"
                           + "<SOAP:Header>"
                           + "<wsse:Security xmlns:wsse=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd\">"
                           + TMLServices.securityKey
                           + "</wsse:Security>"
                           + "</SOAP:Header>"
                           + "<SOAP:Body>"
                           + "<GetJobCodeMaster xmlns=\"VTABSSblWSApp\" preserveSpace=\"no\" qAccess=\"0\" qValues=\"\">"
                           + "<BU>TMPC</BU>"
                           + "<FromDate>"
                           + txtFromDateJobCode.Text.Trim()
                           + "</FromDate>"
                           + "<ToDate>"
                           + txtToDateJobCode.Text.Trim()
                           + "</ToDate>"
                           + "</GetJobCodeMaster>" + "</SOAP:Body>" + "</SOAP:Envelope>";
                responseString = Create_Webrequest(request);
                lblErrorJobCode.Text = UpdateDatabase(responseString, "GetJobCodeMaster");
            }
            else
            {
                PopupPanel.Visible = true;
                divJobCode.Visible = false;
                pnl_updateStatus.Visible = false;
                divAthuntication.Visible = true;
            }
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        lbl_Status.Text = "";
        lblErrorJobCode.Text = "";
        PopupPanel.Visible = false;
    }

    protected void btn_Login_Click(object sender, EventArgs e)
    {
        if (txtUserName.Text.Trim() == "")
        {
            lblError_Login.Text = "Please Enter User Name.";
        }
        else if (txtPassword.Text.Trim() == "")
        {
            lblError_Login.Text = "Please Enter Password.";
        }
        else
        {
            TMLServices.GetAuthentication(txtUserName.Text.ToString(), txtPassword.Text.ToString());
            if (TMLServices.securityKey != "")
            {
                Session["UserName"] = txtUserName.Text.ToString();
                PopupPanel.Visible = true;
                pnl_updateStatus.Visible = true;
                lbl_Status.Text = "Server is connected now.Please retry.";
                txtPassword.Text = "";
                txtUserName.Text = "";
                divAthuntication.Visible = false;
                divJobCode.Visible = false;
            }
            else
            {
                lblError_Login.Text = "User Name & Password Not Matched.";
                txtPassword.Text = "";
                txtUserName.Text = "";
                txtUserName.Focus();
            }
        }
    }

    protected void btn_Close_Login_Click(object sender, EventArgs e)
    {
        PopupPanel.Visible = false;
        lblError_Login.Text = "";
    }
 
}