using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Configuration;
using System.IO;
using System.Data;
using System.Text;

/// <summary>
/// Summary description for TMLServices
/// </summary>

public class TMLServices
{
    public static string securityKey = "";
    
    static TMLServices()
    {
        try
        {
            if (securityKey.Trim() == "")
            {
                GetAuthentication();
            }
        }
        catch (Exception ex)
        {
            GetAuthentication();
        }
    }

    public static void GetAuthentication()
    {
        //ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
        //Uri uri = new Uri("https://tmcrmappsqa.inservices.tatamotors.com/cordys/com.eibus.web.soap.Gateway.wcp?organization=o=B2C,cn=cordys,cn=cbop,o=tatamotors.com");
        //try
        //{
        //    String request = "<SOAP:Envelope xmlns:SOAP=\"http://schemas.xmlsoap.org/soap/envelope/\">" +
        //        "<SOAP:Header>" +
        //        "<wsse:Security xmlns:wsse=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd\">" +
        //            "<wsse:UsernameToken xmlns:wsse=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd\">" +
        //        "<wsse:Username>" + ConfigurationManager.AppSettings["Usercode"].ToString().Trim() + "</wsse:Username>" +
        //                  "<wsse:Password>" + ConfigurationManager.AppSettings["Passkey"].ToString().Trim() + "</wsse:Password>" +
        //            "</wsse:UsernameToken>" +
        //        "</wsse:Security>" +
        //        "</SOAP:Header>" +
        //    "<SOAP:Body>" +
        //        "<samlp:Request IssueInstant=\"2004-12-05T09:21:59Z\" MajorVersion=\"1\" MinorVersion=\"1\" RequestID=\"456789\" xmlns:samlp=\"urn:oasis:names:tc:SAML:1.0:protocol\">" +
        //            "<samlp:AuthenticationQuery>" +
        //                "<saml:Subject xmlns:saml=\"urn:oasis:names:tc:SAML:1.0:assertion\">" +
        //                    "<saml:NameIdentifier Format=\"urn:oasis:names:tc:SAML:1.1:nameid-format:unspecified\">" + ConfigurationManager.AppSettings["Usercode"].ToString().Trim() + "</saml:NameIdentifier>" +
        //                "</saml:Subject>" +
        //            "</samlp:AuthenticationQuery>" +
        //        "</samlp:Request>" +
        //    "</SOAP:Body>" +
        //    "</SOAP:Envelope>";

            ////"<wsse:Username>" + ConfigurationManager.AppSettings["Usercode"].ToString().Trim() + "</wsse:Username>" +
            ////           "<wsse:Password>" + ConfigurationManager.AppSettings["Passkey"].ToString().Trim() + "</wsse:Password>" +

            ////SetSecurityKey(Create_Webrequest(request));
        //}
        //catch (Exception ex) { }
    }

    public static void GetAuthentication(String Username, String Password)
    {
        ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
        Uri uri = new Uri("https://tmcrmapps.inservices.tatamotors.com/cordys/com.eibus.web.soap.Gateway.wcp?organization=o=B2C,cn=cordys,cn=cbop,o=tatamotors.com");
        try
        {
            String request = "<SOAP:Envelope xmlns:SOAP=\"http://schemas.xmlsoap.org/soap/envelope/\">" +
                "<SOAP:Header>" +
                "<wsse:Security xmlns:wsse=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd\">" +
                    "<wsse:UsernameToken xmlns:wsse=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd\">" +
                "<wsse:Username>" + Username.Trim() + "</wsse:Username>" +
                          "<wsse:Password>" + Password.Trim() + "</wsse:Password>" +
                    "</wsse:UsernameToken>" +
                "</wsse:Security>" +
                "</SOAP:Header>" +
            "<SOAP:Body>" +
                "<samlp:Request IssueInstant=\"2004-12-05T09:21:59Z\" MajorVersion=\"1\" MinorVersion=\"1\" RequestID=\"456789\" xmlns:samlp=\"urn:oasis:names:tc:SAML:1.0:protocol\">" +
                    "<samlp:AuthenticationQuery>" +
                        "<saml:Subject xmlns:saml=\"urn:oasis:names:tc:SAML:1.0:assertion\">" +
                            "<saml:NameIdentifier Format=\"urn:oasis:names:tc:SAML:1.1:nameid-format:unspecified\">" + Username.Trim() + "</saml:NameIdentifier>" +
                        "</saml:Subject>" +
                    "</samlp:AuthenticationQuery>" +
                "</samlp:Request>" +
            "</SOAP:Body>" +
            "</SOAP:Envelope>";
            
            SetSecurityKey(Create_Webrequest(request));
        }
        catch (Exception ex) { }
    }


    private static void SetSecurityKey(string xmlString)
    {
        try
        {
            securityKey = xmlString.Substring(xmlString.IndexOf("<Signature xmlns:samlp=\"urn:oasis:names:tc:SAML:1.0:protocol\" xmlns=\"http://www.w3.org/2000/09/xmldsig#\">")).Replace("</samlp:Response></SOAP:Body></SOAP:Envelope>", "");
            if (securityKey != "")
            {
                //flag = 0;
            }
            else
            {
                //flag = -1;
            }
        }
        catch (Exception ex)
        {
            //flag = -1;
        }
    }
       

    public static void Error_Tracker(string ServiceName, string ErrorMessage,string Username)
    {
        if (securityKey != "")
        {
            Uri uri = new Uri("https://tmcrmapps.inservices.tatamotors.com/cordys/com.eibus.web.soap.Gateway.wcp?organization=o=B2C,cn=cordys,cn=cbop,o=tatamotors.com");
            try
            {
                String request = "<SOAP:Envelope xmlns:SOAP=\"http://schemas.xmlsoap.org/soap/envelope/\">" +
                                "<SOAP:Header>" +
                                "<wsse:Security xmlns:wsse=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd\">" +
                                securityKey +
                                "</wsse:Security>" +
                                "</SOAP:Header>" +
                                "<SOAP:Body>" +
                                "<UpdateVtabsErrorTracker xmlns=\"VTABSCordysWSApp\" reply=\"yes\" commandUpdate=\"no\" preserveSpace=\"no\" batchUpdate=\"no\">" +
                                "<tuple>" +
                                    "<new>" +
                                        "<VTABS_ERROR_TRACKER qAccess=\"0\" qConstraint=\"0\" qInit=\"0\" qValues=\"\">" +
                                            "<WS_NAME>" + ServiceName + "</WS_NAME>" +
                                            "<EXCEPTION_MSG>" + ErrorMessage + "</EXCEPTION_MSG>" +
                                            "<WS_INITIATOR>" + Username + "</WS_INITIATOR>" +
                                            "<WS_INITIATOR_EMAILID>sanjay@spintech.in</WS_INITIATOR_EMAILID>" +
                                        "</VTABS_ERROR_TRACKER>" +
                                    "</new>" +
                                "</tuple>" +
                                "</UpdateVtabsErrorTracker>" +
                                "</SOAP:Body>" +
                                "</SOAP:Envelope>";

                Create_Webrequest(request);                
            }
            catch (Exception ex) { }
        }
    }

    private static string Create_Webrequest(string requestData)
    {
        try
        {
            WebRequest request = WebRequest.Create("https://tmcrmapps.inservices.tatamotors.com/cordys/com.eibus.web.soap.Gateway.wcp?organization=o=B2C,cn=cordys,cn=cbop,o=tatamotors.com");
            request.Method = "POST";
            byte[] byteArray = Encoding.UTF8.GetBytes(requestData);
            request.ContentType = "text/xml";
            request.ContentLength = byteArray.Length;
            request.Timeout = 999999;
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
        }
        catch (Exception ex)
        {
            return "";
        }
    }
}