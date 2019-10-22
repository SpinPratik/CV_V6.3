using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Complain : System.Web.UI.Page
{
    private SqlConnection scon = new SqlConnection(ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString());
    private string SystemStatus = "";
    private static int flag = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Clear();
            FindMax();
            GetDealerDetails();
        }
        lbl_msg.Text = "";
        this.Title = "Support";
        Session["Current_Page"] = "Support";
    }

    private void FindMax()
    {
        try
        {
            scon.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT ISNULL(MAX(ComplainNo), 0) + 1 AS CSRNO FROM tblComplaint", scon);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            lbl_csrno.Text = dt.Rows[0][0].ToString();
            scon.Close();
        }
        catch (Exception ex)
        {
        }
    }

    private void GetDealerDetails()
    {
        try
        {
            SqlConnection con = new SqlConnection(DataManager.ConStr);
            SqlDataAdapter sda = new SqlDataAdapter("select top 1 * from tblDealerDetails order by doi desc", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                lblDealerCode.Text = dt.Rows[0]["D_Code"].ToString();
                lblDealer.Text = dt.Rows[0]["D_Name"].ToString();
                lblAddress.Text = dt.Rows[0]["D_Address"].ToString();
                lblLocation.Text = dt.Rows[0]["D_Location"].ToString();
                lblCity.Text = dt.Rows[0]["D_City"].ToString();
            }
        }
        catch
        {
        }
    }

    private void Clear()
    {
        lbl_csrno.Text = "";
        lbl_date.Text = System.DateTime.Now.ToString();
        rbox_yes.Checked = false;
        rbox_no.Checked = false;
        txt_callreportedby.Text = "";
        foreach (ListItem li in chkHardware.Items)
        {
            li.Selected = false;
        }
        foreach (ListItem li in chkSoftware.Items)
        {
            li.Selected = false;
        }
        lbl_msg.Text = "";
        txt_problem.Text = "";
        rbox_yes.Focus();
    }

    protected void btn_clear_Click(object sender, EventArgs e)
    {
        Clear();
    }

    protected string GetSelectedList(CheckBoxList chl)
    {
        string str = "";
        foreach (ListItem li in chl.Items)
        {
            if (li.Selected)
            {
                str += "<" + li.Value + ">";
            }
        }
        return str;
    }

    protected void btn_send_Click(object sender, EventArgs e)
    {
        if (lbl_csrno.Text.ToString().Trim() == "")
        {
            lbl_csrno.Text = "Please Click On New Button To Generate CSR No. !";
        }
        else if (rbox_yes.Checked == false && rbox_no.Checked == false)
        {
            lbl_msg.Text = "Please Select System Down(Yes/No). !";
        }
        else if (txt_callreportedby.Text.Trim() == "")
        {
            lbl_msg.Text = "Please Enter Error Reporter Name. !";
        }
        else
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT ComplainNo FROM tblComplaint WHERE ComplainNo = " + lbl_csrno.Text.Trim() + "", scon);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                try
                {
                    string softr, hard;
                    softr = GetSelectedList(chkSoftware);
                    hard = GetSelectedList(chkHardware);
                    if (uf1.FileName != "")
                    {
                        long filebytes = uf1.PostedFile.ContentLength;
                        long maxbyte = 5242880;
                        if (filebytes > maxbyte)
                        {
                            lbl_msg.Text = "Attachment file is larger than 5 MB !";
                        }
                        else
                        {
                            if (SaveAttachment() == 0)
                            {
                                Clear();
                                lbl_msg.Text = "Unable to send complain. !";
                            }
                            else if (SendEmail() == 0)
                            {
                                Clear();
                                lbl_msg.Text = "Unable to send complain. !";
                            }
                            else
                            {
                                scon.Open();
                                SqlCommand scom = new SqlCommand("InsertComplain", scon);
                                scom.CommandType = CommandType.StoredProcedure;
                                scom.Parameters.AddWithValue("@ComplainNo", lbl_csrno.Text.ToString());
                                scom.Parameters.AddWithValue("@complaindate", lbl_date.Text.ToString());
                                scom.Parameters.AddWithValue("@dealercode", lblDealerCode.Text);
                                scom.Parameters.AddWithValue("@systemdown", SystemStatus);
                                scom.Parameters.AddWithValue("@hardware", hard);
                                scom.Parameters.AddWithValue("@software", softr);
                                scom.Parameters.AddWithValue("@callreportedby", txt_callreportedby.Text.ToString());
                                scom.Parameters.AddWithValue("@problemreported", txt_problem.Text.ToString());
                                scom.ExecuteNonQuery();
                                scon.Close();
                                Clear();
                                lbl_msg.Text = "Complain Successfully Send. !";
                            }
                        }
                    }
                    else
                    {
                        if (SendEmail() == 0)
                        {
                            Clear();
                            lbl_msg.Text = "Unable to send complain. !";
                        }
                        else
                        {
                            scon.Open();
                            SqlCommand scom = new SqlCommand("InsertComplain", scon);
                            scom.CommandType = CommandType.StoredProcedure;
                            scom.Parameters.AddWithValue("@ComplainNo", lbl_csrno.Text.ToString());
                            scom.Parameters.AddWithValue("@complaindate", lbl_date.Text.ToString());
                            scom.Parameters.AddWithValue("@dealercode", lblDealerCode.Text);
                            scom.Parameters.AddWithValue("@systemdown", SystemStatus);
                            scom.Parameters.AddWithValue("@hardware", hard);
                            scom.Parameters.AddWithValue("@software", softr);
                            scom.Parameters.AddWithValue("@callreportedby", txt_callreportedby.Text.ToString());
                            scom.Parameters.AddWithValue("@problemreported", txt_problem.Text.ToString());
                            scom.ExecuteNonQuery();
                            scon.Close();
                            Clear();
                            lbl_msg.Text = "Complain Successfully Send. !";
                        }
                    }
                }
                catch
                {
                    Clear();
                }
            }
            else
            {
                lbl_msg.Text = "";
                Clear();
                FindMax();
            }
        }
    }

    private int SendEmail()
    {
        try
        {
            DirectoryInfo DestDir = new DirectoryInfo(Server.MapPath("CSRAttachments"));
            string Filename = "";
            if (uf1.FileName != "")
            {
                Filename = lbl_csrno.Text + uf1.FileName;
                if (!DestDir.Exists)
                {
                    DestDir.Create();
                }
                else
                {
                    try
                    {
                        foreach (FileInfo fi in DestDir.GetFiles("*.*"))
                        {
                            fi.Delete();
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
                uf1.SaveAs(DestDir + "\\" + Filename);
            }

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.Port = 587;
            smtpClient.Credentials = new System.Net.NetworkCredential("protrac_b2t_support@spintech.in ", "@protrac@b2t");
            smtpClient.EnableSsl = true;
            MailMessage ReportMail = new MailMessage();
            string status = "";
            string hard = "", soft = "";
            foreach (ListItem li in chkHardware.Items)
            {
                if (li.Selected)
                {
                    if (hard.Length == 0)
                        hard += li.Text.ToString();
                    else
                        hard += ", " + li.Text.ToString();
                }
            }
            foreach (ListItem li in chkSoftware.Items)
            {
                if (li.Selected)
                {
                    if (soft.Length == 0)
                        soft += li.Text.ToString();
                    else
                        soft += ", " + li.Text.ToString();
                }
            }
            if (SystemStatus == "True")
            {
                status = "YES";
            }
            else
            {
                status = "NO";
            }
            string mailBody = "";
            mailBody = "<br>System Down : " + status.Trim();
            if (hard.Length > 0)
            {
                mailBody += "<br><br>Hardware Problem : " + hard.Trim();
            }
            if (soft.Length > 0)
            {
                mailBody += "<br><br>Software Problem : " + soft.Trim();
            }
            mailBody += "<br><br>Problem Description : <br>" + txt_problem.Text.ToString();
            mailBody += "<br><br><br>Reported by : <br>-------------------<br>" + txt_callreportedby.Text.ToString();
            ReportMail.Body = mailBody;
            ReportMail.IsBodyHtml = true;
            ReportMail.To.Add(ConfigurationManager.AppSettings["Email"].ToString());
            ReportMail.From = new MailAddress("protrac_b2t_support@spintech.in", "@protrac_b2t");
            ReportMail.Subject = "ProTRAC TATA CSR - " + lblDealerCode.Text + " Reports AS On :" + DateTime.Now.ToString("dd-MM-yyyy");
            if (uf1.FileName != "")
            {
                ReportMail.Attachments.Add(new Attachment(DestDir + "\\" + Filename));
            }
            smtpClient.Send(ReportMail);
            return 1;
        }
        catch (Exception ex)
        {
            return 0;
        }
    }

    private int SaveAttachment()
    {
        try
        {
            string str = uf1.FileName;
            DirectoryInfo DestDir = new DirectoryInfo("C:\\ProTRAC");
            if (!DestDir.Exists)
                DestDir.Create();
            uf1.SaveAs("C:\\ProTRAC\\" + str);
            FileStream fs = new FileStream("C:\\ProTRAC\\" + str, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            byte[] buffer = br.ReadBytes((int)fs.Length);
            br.Close();
            fs.Close();
            scon.Open();
            SqlCommand scom = new SqlCommand("ComplainAttachments", scon);
            scom.CommandType = CommandType.StoredProcedure;
            scom.Parameters.AddWithValue("@complainno", lbl_csrno.Text.ToString());
            scom.Parameters.AddWithValue("@ProbImage", buffer);
            scom.Parameters.AddWithValue("@Fname", uf1.FileName);
            scom.ExecuteNonQuery();
            scon.Close();
            return 1;
        }
        catch (Exception)
        {
            return 0;
        }
    }

    protected void rbox_yes_CheckedChanged(object sender, EventArgs e)
    {
        SystemStatus = "True";
    }

    protected void rbox_no_CheckedChanged(object sender, EventArgs e)
    {
        SystemStatus = "False";
    }

    protected void btn_logout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();
        Response.Redirect("login.aspx");
    }

    protected void btnBACK_Click(object sender, EventArgs e)
    {
        try
        {
            Session["CURRENT_PAGE"] = null;
            if (Session["ROLE"].ToString() == "ADMIN")
            {
                Response.Redirect("AHome.aspx");
            }
            else if (Session["ROLE"].ToString() == "WORK MANAGER")
            {
                Response.Redirect("DisplayWorks.aspx");
            }
            else if (Session["ROLE"].ToString() == "REPORT")
            {
                Response.Redirect("Reports.aspx");
            }
            else if (Session["ROLE"].ToString() == "JOB SLIP")
            {
                Response.Redirect("JobCardCreation.aspx");
            }
            else if (Session["ROLE"].ToString() == null)
            {
                Response.Redirect("Login.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void btn_New_Click(object sender, EventArgs e)
    {
        Clear();
        FindMax();
    }
}