using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CustomerDisplay : System.Web.UI.Page
{
    private int i = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

        if (Session[Session["TMLDealercode"] + "-TMLConString"].ToString()=="")
        {
                Response.Redirect("login.aspx");
          }
        }
        catch (Exception ec)
        {
            Response.Redirect("login.aspx");
        }
        
        //SqlDataSource1.ConnectionString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        try
        {
            if (!Page.IsPostBack)
            {
                
                i = grdDisplay.PageIndex;
                if (!Page.IsPostBack)
                {
                    GridDataBind();
                    string PageName = string.Empty;
                    PageName = GetCurrentPageName();
                }
                string disp = "";
                DataTable dispdt = new DataTable();
                SqlDataAdapter dispda = new SqlDataAdapter("Select * from tbl_DisplayStatus", new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString()));
                dispda.Fill(dispdt);
                if (dispdt.Rows.Count > 0)
                {
                    disp = dispdt.Rows[0][0].ToString();
                }
                lblScroll.Text = "<marquee behavior='scroll' direction='left' scrollamount='3'>" + disp.ToString() + "</marquee>";
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("login.aspx");
        }
    }
    public static string GetCurrentPageName()
    {
        string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
        System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
        string sRet = oInfo.Name;
        return sRet;
    }
    protected void grdDisplay_RowDataBound(object sender, GridViewRowEventArgs e)
    {
  
        if (e.Row.RowType != DataControlRowType.Pager)
            e.Row.Style.Value = "text-align:left;";
        //  e.Row.Cells[0].Width = new Unit(150);
        //e.Row.Cells[1].Width = new Unit(260);

        if (e.Row.Cells[1].Text.Length > 15)
        {
            e.Row.Cells[1].ToolTip = e.Row.Cells[1].Text;
            e.Row.Cells[1].Text = e.Row.Cells[1].Text.Substring(0, 12) + "...";
        }
        
        // e.Row.Cells[2].Width = new Unit(100);
        e.Row.Cells[2].Attributes.Add("style", "text-align:left;");
        //e.Row.Cells[3].Width = new Unit(200);
        //e.Row.Cells[4].Width = new Unit(200);

        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = "PDT";
            e.Row.Cells[0].Style.Value = "text-align:left;text-size:18px;padding-left:20px;";
            e.Row.Cells[1].Style.Value = "text-align:left;text-size:18px;text-align:left;margin-left:-5px;";
            e.Row.Cells[1].Text = "Vehicle Number";
           // e.Row.Cells[1].Width = new Unit(230);
            e.Row.Cells[2].Text = "Service advisor";
          
            e.Row.Cells[2].Style.Value = "text-align:left;padding-left:20px;";
            e.Row.Cells[3].Style.Value = "text-align:left;padding-left:20px;";
           
            e.Row.Cells[4].Style.Value = "text-align:left;text-size:18px;padding-left:20px;";
          //  e.Row.Cells[4].Text = "EDT";

        }

        string Remarks = string.Empty;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            DataRowView drv = (DataRowView)e.Row.DataItem;
            int regno = drv.DataView.Table.Columns["Vehicle No"].Ordinal;
            int SA= drv.DataView.Table.Columns["Service Advisor"].Ordinal;
            int PDT = drv.DataView.Table.Columns["PDT"].Ordinal;
            int EDT = drv.DataView.Table.Columns["ERT"].Ordinal;
            int Status = drv.DataView.Table.Columns["Status"].Ordinal;
           // int Position = drv.DataView.Table.Columns["Position"].Ordinal;
           
            e.Row.Cells[regno].CssClass = "resized-splitflap";
            e.Row.Cells[SA].CssClass = "resized-splitflap";
            e.Row.Cells[PDT].CssClass = "resized-splitflap";
            e.Row.Cells[Status].CssClass = "resized-splitflap";
            e.Row.Cells[EDT].CssClass = "resized-splitflap";

            string rpdtstr = e.Row.Cells[PDT].Text.Replace("#", " ");
            if (rpdtstr.Contains("-"))
            {
                e.Row.Cells[PDT].Attributes.Add("style", "text-align:left;color:Orange;");
                e.Row.Cells[PDT].Text = rpdtstr.Replace("-", "");
            }
            else if (rpdtstr.Contains("$"))
            {
                e.Row.Cells[PDT].Attributes.Add("style", "text-align:left;");
                e.Row.Cells[PDT].Text = rpdtstr.Replace("$", "");
            }
            else
            {
                e.Row.Cells[PDT].Attributes.Add("style", "text-align:left;");
                e.Row.Cells[PDT].Text = rpdtstr;
            }

            e.Row.Cells[regno].Attributes.Add("style", "text-align:left;");
            e.Row.Cells[regno].Text = e.Row.Cells[regno].Text.ToUpper();
            if (drv[regno].ToString().Contains('*'))
            {
                string reno = drv[regno].ToString().Replace("*", "");
                if (reno.Length < 10)
                {
                    int len = reno.Length;
                    int NumOfChars = 10 - len;
                   
                    e.Row.Cells[regno].Text = reno+new string(' ',NumOfChars);
                }
                else
                {


                    e.Row.Cells[regno].Text = reno;
                }
            }
            if (drv[regno].ToString().Contains('+'))
            {
                string reno = drv[regno].ToString().Replace("+", "");
                e.Row.Cells[regno].Text = reno;
               
            }


            e.Row.Cells[Status].Style.Value = "text-align:left;";
            if (e.Row.Cells[Status].Text == "Ready")
            {
                e.Row.Cells[Status].Attributes.Add("style", "text-align:center;color:limegreen;");
            }
            else if (e.Row.Cells[Status].Text.Replace("#", "") == "Ready")
            {
                e.Row.Cells[Status].Text = e.Row.Cells[Status].Text.Replace("#", "");
                e.Row.Cells[Status].Attributes.Add("style", "text-align:left;color:limegreen;");
            }
            else if (e.Row.Cells[Status].Text == "Delay")
            {
                e.Row.Cells[Status].Attributes.Add("style", "text-align:left;color:orange;");
            }
            else 
            {
                e.Row.Cells[Status].Attributes.Add("style", "text-align:left;color:white;");
            }
            //e.Row.Cells[Position].Style.Value = "text-align:center;";


            try
            {
                
                e.Row.Cells[EDT].Attributes.Add("style", "text-align:left;");
                //if (Convert.ToDateTime(e.Row.Cells[EDT].Text) < DateTime.Now)
                //{
                //    e.Row.Cells[EDT].Attributes.Add("style", "color:red !IMPORTANT;text-align:center;");
                //    if (Convert.ToDateTime(e.Row.Cells[EDT].Text).ToString("dd/MM") == DateTime.Now.ToString("dd/MM"))
                //    {
                //        e.Row.Cells[EDT].Text = Convert.ToDateTime(e.Row.Cells[EDT].Text).ToString("HH:mm");
                //    }
                //    else
                //    {
                //        e.Row.Cells[EDT].Text = Convert.ToDateTime(e.Row.Cells[EDT].Text).ToString("HH:mm dd/MM");
                //    }
                //}
                //else
                //{
                if (Convert.ToDateTime(e.Row.Cells[EDT].Text).ToString("dd/MM") == DateTime.Now.ToString("dd/MM"))
                {
                    e.Row.Cells[EDT].Text = Convert.ToDateTime(e.Row.Cells[EDT].Text).ToString("HH:mm");
                }
                else
                {
                    e.Row.Cells[EDT].Text = Convert.ToDateTime(e.Row.Cells[EDT].Text).ToString("HH:mm dd/MM");
                }
                //}
            }
            catch (Exception exz)
            {

            }
        }
    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {
      
        try
        {
            i = grdDisplay.PageIndex;
            if (grdDisplay.PageIndex == grdDisplay.PageCount - 1)
            {
                grdDisplay.PageIndex = 0;
                GridDataBind();
            }
            else
            {
                grdDisplay.PageIndex += 1;
                GridDataBind();
            }
        }
        catch
        {
        }
    }

    private void GridDataBind()
    {
        try
        {
            DataTable Dt = new DataTable();
            Dt = LoadData("", "");
            grdDisplay.DataSource = Dt;
            grdDisplay.DataBind();
          //  UpdatePanel1.Update();
            int pc = Dt.Rows.Count % 8;
            int totalpages = 0;
            if (pc > 0)
                totalpages = (Dt.Rows.Count / 8) + 1;
            else
                totalpages = (Dt.Rows.Count / 8);
            lblPgCount.Text = "Page: " + (grdDisplay.PageIndex + 1).ToString() + "/" + totalpages.ToString();
        }
        catch (Exception ex)
        {
        }
    }

    protected void btnLoad_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            GridDataBind();
            Timer1.Enabled = false;
            Timer2.Enabled = true;
        }
        catch (Exception ex)
        {
        }
        finally
        {
        }
    }
     

    private DataTable LoadData(string RegNo, string OwnerName)
    {
        //string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        SqlConnection oConn = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlCommand cmd = new SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "CustomerDisplay";
        cmd.Parameters.AddWithValue("@RegNO", RegNo);
        cmd.Parameters.AddWithValue("@OwnerName", OwnerName);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    protected void Timer2_Tick(object sender, EventArgs e)
    {
        try
        {
            Timer1.Enabled = true;
            Timer2.Enabled = false;
            GridDataBind();
        }
        catch (Exception ex)
        {
        }
    }

    protected void TimerTimeShower_Tick(object sender, EventArgs e)
    {
        lbTime.Text = DateTime.Now.ToString("MMM dd yyyy HH:mm");
        //GridDataBind();
    }
}