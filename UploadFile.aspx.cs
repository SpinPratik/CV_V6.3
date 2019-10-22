using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UploadFile : System.Web.UI.Page
{
    private SqlConnection con = new SqlConnection();
    private static int des1 = 1;
    private string PathofUpload = string.Empty;
    private string UploadFileFolder = string.Empty;
    private static bool DateVal = false;

    protected void Page_Load(object sender, EventArgs e)
    {
       // string scon = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
     
        lblStatus.Text = "";

        try
        {
            if (Session["ROLE"] == null || Session["ROLE"].ToString() != "ADMIN" || Session[Session["TMLDealercode"] + "-TMLConString"].ToString()==null)
            {
                Response.Redirect("login.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("login.aspx");
        }
        con.ConnectionString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        if (!Page.IsPostBack)
        {
            Session["CURRENT_PAGE"] = "File Upload";
        }
        lblStatus.ForeColor = Color.Red;
        lblStatus.Text = "";
    }

    protected string ReplaceHTMLCharacters(string val)
    {
        return val.Replace("&nbsp;", "").Replace("&quot;", "");
    }

    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkSel = (CheckBox)gvPMK.HeaderRow.Cells[0].FindControl("chkAll");
        if (chkSel.Checked)
        {
            for (int i = 0; i < gvPMK.Rows.Count; i++)
            {
                System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)gvPMK.Rows[i].Cells[0].Controls[1];
                if (!chk.Checked)
                    chk.Checked = true;
            }
        }
        else
        {
            for (int i = 0; i < gvPMK.Rows.Count; i++)
            {
                System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)gvPMK.Rows[i].Cells[0].Controls[1];
                if (chk.Checked)
                    chk.Checked = false;
            }
        }
    }

    protected void chkUpload_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkSel = (CheckBox)gvPMK.HeaderRow.Cells[0].FindControl("chkAll");
        if (chkSel.Checked)
            chkSel.Checked = false;
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbooks workbooks = excel.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook wb = excel.Workbooks.Open(Server.MapPath("Customer Data1.xls"), 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 0, 0);
            excel.Visible = true;
        }
        catch (Exception ex)
        {
        }
    }

    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        lblStatus.CssClass = "reset";
        lblStatus.Text = "";
        try
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbooks workbooks = excel.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook wb = excel.Workbooks.Open(Server.MapPath("CARGO-CV.xlsx"), 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 0, 0);
            excel.Visible = true;
        }
        catch (Exception ex)
        {
        }
    }

    //protected void btnJobcodeValidate_Click(object sender, EventArgs e)
    //{
    //    lblStatus.Text = "";
    //    gvPMK.DataSource = null;
    //    gvPMK.DataBind();
    //    try
    //    {
    //        if (FileUploadJobCode.HasFile && FileUploadJobCode.FileName.EndsWith(".xls"))
    //        {
    //            string Filenam = FileUploadJobCode.FileName;
    //            DirectoryInfo DestDir = new DirectoryInfo(Server.MapPath("Tempr"));
    //            if (!DestDir.Exists)
    //                DestDir.Create();
    //            else
    //                foreach (FileInfo fi in DestDir.GetFiles("*.xls"))
    //                {
    //                    fi.Delete();
    //                }
    //            FileUploadJobCode.SaveAs(DestDir + "\\" + Filenam);
    //            String xlrstrCon = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + DestDir + "\\" + Filenam + ";Extended Properties=Excel 8.0;";
    //            DataTable dt = new DataTable();
    //            OleDbDataAdapter da = new OleDbDataAdapter("SELECT [Job Code],[Job Code Desc],[Standard Labour Hrs],[Billing Hrs],[Type],[Parent Product Line],[Status],[Aggregate],[Job Code Type]  FROM [JOB-Std Labour hours$] WHERE RTRIM(LTRIM([Job Code])) IS NOT NULL", xlrstrCon);
    //            da.Fill(dt);
    //            if (dt.Rows.Count > 5000)
    //            {
    //                lblStatus.ForeColor = Color.Red;
    //                lblStatus.Text = "You Can't Upload The Data More Than 5000, Your Selected File Contains " + dt.Rows.Count.ToString() + " Records.!";
    //                return;
    //            }
    //            gvPMK.DataSource = dt;
    //            gvPMK.DataBind();
    //            btnUploadJobcode.Enabled = true;
    //            lblStatus.ForeColor = Color.Green;
    //            lblStatus.Text = "File Validated!";
    //            FileUploadJobCode.FileContent.Close();
    //        }
    //        else
    //        {
    //            lblStatus.ForeColor = Color.Red;
    //            lblStatus.Text = "File is not selected or invalid file selected.";
    //            gvPMK.DataSource = null;
    //            gvPMK.DataBind();
    //            btnUploadJobcode.Enabled = false;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblStatus.ForeColor = Color.Red;
    //        lblStatus.Text = "Invalid file selected";
    //        gvPMK.DataSource = null;
    //        gvPMK.DataBind();
    //        btnUploadJobcode.Enabled = false;
    //    }
    //}

    protected void btnUploadJobcode_Click(object sender, EventArgs e)
    {
        try
        {
            lblStatus.Text = "";
            int c1 = 0;
            int c2 = 0;
            int c3 = 0;
            int c4 = 0;
            int c5 = 0;
            int c6 = 0;
            int c7 = 0;
            int c8 = 0;
            int c9 = 0;
            int c10 = 0; 
            int c11 = 0;
            int c12 = 0;
            int c13 = 0;
            int c14 = 0;
            int c15 = 0;
            int c16 = 0;
            int c17 = 0;
            int c18 = 0;
            int c19 = 0;
            int c20 = 0;
            int c21 = 0;
            int c22 = 0;
            int c23 = 0;
            int c24 = 0;
            int c25 = 0;
            int c26 = 0;
            

            for (int j = 1; j < gvPMK.Rows[0].Cells.Count; j++)
            {
                string str = gvPMK.HeaderRow.Cells[j].Text.Trim().ToString();
                if (str == "Month")
                {
                    c1 = j;
                }
                if (str == "Area")
                {
                    c2 = j;
                }
                if (str == "Dealer")
                {
                    c3 = j;
                }
                if (str == "Division")
                {
                    c4 = j;
                }
                if (str == "CSM Name ")
                {
                    c5 = j;
                }
                if (str == "Chassis No")
                {
                    c6 = j;
                }
                if (str == "Reg No")
                {
                    c7 = j;
                }
                if (str == "LOB")
                {
                    c8 = j;
                }
                if (str == "PPL")
                {
                    c9 = j;
                }
                if (str == "PL")
                {
                    c10 = j;
                }
                if (str == "Priority")
                {
                    c11 = j;
                }
                if (str == "Customer List Type")
                {
                    c12 = j;
                }
                if (str == "Sale Date")
                {
                    c13 = j;
                }
                if (str == "Account Name")
                {
                    c14 = j;
                }
                if (str == "Name")
                {
                    c15 = j;
                }
                if (str == "Main Phone")
                {
                    c16 = j;
                }
                if (str == "PhoneCell")
                {
                    c17 = j;
                }
                if (str == "PhoneOff")
                {
                    c18 = j;
                }
                if (str == "PhoneRes")
                {
                    c19 = j;
                }
                if (str == "Address Line1")
                {
                    c20 = j;
                }
                if (str == "Address Line2")
                {
                    c21 = j;
                }
                if (str == "Last Service KM")
                {
                    c22 = j;
                }
                if (str == "Last Service Date")
                {
                    c23 = j;
                }
                if (str == "Month Difference")
                {
                    c24 = j;
                }
                if (str == "CustContactList")
                {
                    c25 = j;
                }
                if (str == "Month Difference1")
                {
                    c26 = j;
                }
                
            }
            for (int i = 0; i < gvPMK.Rows.Count; i++)
            {
                System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)gvPMK.Rows[i].Cells[0].Controls[1];
                if (chk.Checked)
                {
                    SqlCommand cmd = new SqlCommand("", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "UploadLostCustomer";
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    cmd.Parameters.AddWithValue("@Month", ReplaceHTMLCharacters(gvPMK.Rows[i].Cells[c1].Text.Trim()));
                    cmd.Parameters.AddWithValue("@Area", ReplaceHTMLCharacters(gvPMK.Rows[i].Cells[c2].Text.Trim()));
                    cmd.Parameters.AddWithValue("@Dealer", ReplaceHTMLCharacters(gvPMK.Rows[i].Cells[c3].Text.Trim()));
                    cmd.Parameters.AddWithValue("@Division", ReplaceHTMLCharacters(gvPMK.Rows[i].Cells[c4].Text.Trim()));
                    cmd.Parameters.AddWithValue("@CsmName", ReplaceHTMLCharacters(gvPMK.Rows[i].Cells[c5].Text.Trim()));
                    cmd.Parameters.AddWithValue("@ChassisNo", ReplaceHTMLCharacters(gvPMK.Rows[i].Cells[c6].Text.Trim()));
                    cmd.Parameters.AddWithValue("@RegistrationNo", ReplaceHTMLCharacters(gvPMK.Rows[i].Cells[c7].Text.Trim()));
                    cmd.Parameters.AddWithValue("@LOB", ReplaceHTMLCharacters(gvPMK.Rows[i].Cells[c8].Text.Trim()));
                    cmd.Parameters.AddWithValue("@PPL", ReplaceHTMLCharacters(gvPMK.Rows[i].Cells[c9].Text.Trim()));
                    cmd.Parameters.AddWithValue("@PL", ReplaceHTMLCharacters(gvPMK.Rows[i].Cells[c10].Text.Trim()));
                    cmd.Parameters.AddWithValue("@Priority", ReplaceHTMLCharacters(gvPMK.Rows[i].Cells[c11].Text.Trim()));
                    cmd.Parameters.AddWithValue("@CustomerListType", ReplaceHTMLCharacters(gvPMK.Rows[i].Cells[c12].Text.Trim()));
                    cmd.Parameters.AddWithValue("@SaleDate", ReplaceHTMLCharacters(gvPMK.Rows[i].Cells[c13].Text.Trim()));
                    cmd.Parameters.AddWithValue("@AccountName", ReplaceHTMLCharacters(gvPMK.Rows[i].Cells[c14].Text.Trim()));
                    cmd.Parameters.AddWithValue("@Name", ReplaceHTMLCharacters(gvPMK.Rows[i].Cells[c15].Text.Trim()));
                    cmd.Parameters.AddWithValue("@MainPhoneNumber", ReplaceHTMLCharacters(gvPMK.Rows[i].Cells[c16].Text.Trim()));
                    cmd.Parameters.AddWithValue("@PhoneNoCell", ReplaceHTMLCharacters(gvPMK.Rows[i].Cells[c17].Text.Trim()));
                    cmd.Parameters.AddWithValue("@PhoneNoRes", ReplaceHTMLCharacters(gvPMK.Rows[i].Cells[c18].Text.Trim()));
                    cmd.Parameters.AddWithValue("@PhoneNoOff", ReplaceHTMLCharacters(gvPMK.Rows[i].Cells[c19].Text.Trim()));
                    cmd.Parameters.AddWithValue("@AddressLine1", ReplaceHTMLCharacters(gvPMK.Rows[i].Cells[c20].Text.Trim()));
                    cmd.Parameters.AddWithValue("@AddressLine2", ReplaceHTMLCharacters(gvPMK.Rows[i].Cells[c21].Text.Trim()));
                    cmd.Parameters.AddWithValue("@LastService_KM", ReplaceHTMLCharacters(gvPMK.Rows[i].Cells[c22].Text.Trim()));
                    cmd.Parameters.AddWithValue("@LastServiceDate", ReplaceHTMLCharacters(gvPMK.Rows[i].Cells[c23].Text.Trim()));
                    cmd.Parameters.AddWithValue("@MonthDifference", ReplaceHTMLCharacters(gvPMK.Rows[i].Cells[c24].Text.Trim()));
                    cmd.Parameters.AddWithValue("@CustContactList", ReplaceHTMLCharacters(gvPMK.Rows[i].Cells[c25].Text.Trim()));
                    cmd.Parameters.AddWithValue("@MonthDifference1", ReplaceHTMLCharacters(gvPMK.Rows[i].Cells[c26].Text.Trim()));

                    cmd.ExecuteNonQuery();
                }
            }
            con.Close();
            gvPMK.DataSource = null;
            gvPMK.DataBind();
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblStatus.ClientID + "').style.display='none'\",5000)</script>");
            lblStatus.CssClass = "ScsMsg";
            lblStatus.Text = "Job Code details successfully uploaded";
            lblStatus.Attributes.Add("style", "text-transform:none !important");
            btnJobcodeValidate.Enabled = true;
            btnUploadJobcode.Enabled = false;
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblStatus.ClientID + "').style.display='none'\",5000)</script>");
            lblStatus.CssClass = "ErrMsg";
            lblStatus.Text = ex.Message;
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }

    protected void btnJobcodeValidate_Click(object sender, EventArgs e)
    {
        panel1.Visible = false;
        btnJobcodeValidate.Enabled = true;
        lblStatus.Text = "";
        gvPMK.DataSource = null;
        gvPMK.DataBind();
        try
        {
            if (FileUploadJobCode.HasFile && FileUploadJobCode.FileName.EndsWith(".csv"))
            {
                string Filenam = FileUploadJobCode.FileName;
                DirectoryInfo DestDir = new DirectoryInfo(Server.MapPath("Tempr"));
                if (!DestDir.Exists)
                    DestDir.Create();
                else
                    foreach (FileInfo fi in DestDir.GetFiles("*.csv"))
                    {
                        fi.Delete();
                    }

                FileUploadJobCode.SaveAs(DestDir + "\\" + Filenam);
                string[] data = File.ReadAllLines(DestDir + "\\" + Filenam);
                //string[] data = File.ReadLines(DestDir + "\\" + Filenam).Count(line => !string.IsNullOrWhiteSpace(line)) ;
                DataTable dt = new DataTable();

                string[] col = data[0].Split(',');

                foreach (string s in col)
                {
                    if (s == "Month" || s == "Area" || s == "Dealer" || s == "Division" || s == "CSM Name" || s == "Chassis No" || s == "Reg No" || s == "LOB" || s == "PPL" || s == "PL" || s == "Priority" || s == "Customer List Type" || s == "Sale Date" || s == "Account Name" || s == "Name" || s == "Main Phone" || s == "PhoneCell" || s == "PhoneRes" || s == "PhoneOff" || s == "Address Line1" || s == "Address Line2" || s == "Last Service KM" || s == "Last Service Date" || s == "Month Difference" || s == "CustContactList" || s == "Month Difference1") 
                        dt.Columns.Add(s, typeof(string));
                }
                if (data.Length > 5000)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblStatus.ClientID + "').style.display='none'\",5000)</script>");
                    lblStatus.CssClass = "ErrMsg";
                    lblStatus.Text = "You can't upload the data more than 5000, your selected file contains " + dt.Rows.Count.ToString() + " records.!";
                    lblStatus.Attributes.Add("style", "text-transform:none !important");
                }
                else { 
                for (int i = 1; i < data.Length; i++)
                {
                        string[] row = data[i].Split(',');
                        //DataRowCollection rowCollection = dt.Rows;
                       dt.Rows.Add(row);
                        gvPMK.DataSource = dt;
                        gvPMK.DataBind();
                        btnUploadJobcode.Enabled = true;
                        btnJobcodeValidate.Enabled = false;
                        ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblStatus.ClientID + "').style.display='none'\",5000)</script>");
                     
                        lblStatus.CssClass = "ScsMsg";
                        lblStatus.Text = "File validated!";
                        lblStatus.Attributes.Add("style", "text-transform:none !important");
                    }
                }
            }

            else
            {
                lblStatus.Visible = true;
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblStatus.ClientID + "').style.display='none'\",5000)</script>");
                lblStatus.CssClass = "ErrMsg";
                lblStatus.Text = "File is not selected or invalid file selected (Choose .csv file)";
                lblStatus.Attributes.Add("style", "text-transform:none !important");
                gvPMK.DataSource = null;
                gvPMK.DataBind();
                btnUploadJobcode.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblStatus.ClientID + "').style.display='none'\",5000)</script>");
            lblStatus.CssClass = "ErrMsg";
            lblStatus.Visible = true;
            lblStatus.Attributes.Add("style", "text-transform:none !important");
            lblStatus.Text = "Invalid file selected";
            gvPMK.DataSource = null;
            gvPMK.DataBind();
            btnUploadJobcode.Enabled = false;
        }

    }

}