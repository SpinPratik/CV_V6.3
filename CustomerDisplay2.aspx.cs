using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CustomerDisplay2 : System.Web.UI.Page
{
    private int i = 0;

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
        SqlDataSource1.ConnectionString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        try
        {
            if (!Page.IsPostBack)
            {
                i = grdDisplay.PageIndex;

                if (!Page.IsPostBack)
                {
                    GridDataBind();
                    string PageName = string.Empty;
                    PageName = DataManager.GetCurrentPageName();
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

    protected void btn_logout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();
        Response.Redirect("login.aspx");
    }

    protected void grdDisplay_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager)
            e.Row.Style.Value = "text-align:left;";
        e.Row.Cells[0].Width = new Unit(150);
        e.Row.Cells[0].Attributes.Add("style", "text-align:left;");
        e.Row.Cells[1].Width = new Unit(250);
        e.Row.Cells[2].Width = new Unit(200);

        if (e.Row.Cells[2].Text.Length > 15)
        {
            e.Row.Cells[2].ToolTip = e.Row.Cells[2].Text;
            e.Row.Cells[2].Text = e.Row.Cells[2].Text;
        }
        e.Row.Cells[2].Attributes.Add("style", "text-align:left;");
        e.Row.Cells[3].Width = new Unit(80);
        e.Row.Cells[3].Attributes.Add("style", "text-align:left;");
        e.Row.Cells[4].Width = new Unit(100);
        e.Row.Cells[5].Width = new Unit(70);
        e.Row.Cells[6].Width = new Unit(70);
        e.Row.Cells[7].Width = new Unit(70);
        e.Row.Cells[8].Width = new Unit(70);
        e.Row.Cells[9].Width = new Unit(70);
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = "Reg#";
            e.Row.Cells[1].Text = "Service Advisor";
            e.Row.Cells[2].Text = "Service Type";
            e.Row.Cells[3].Text = "PDT";
            e.Row.Cells[4].Text = "Status";
            e.Row.Cells[5].Text = "VI";
            e.Row.Cells[6].Text = "WS";
            e.Row.Cells[7].Text = "WA";
            e.Row.Cells[8].Text = "QC";
            e.Row.Cells[9].Text = "WSH";
            e.Row.Cells[4].Style.Value = "text-align:left;text-size:10pt;";
            e.Row.Cells[5].Style.Value = "text-align:center;text-size:10pt;";
            e.Row.Cells[6].Style.Value = "text-align:center;text-size:10pt;";
            e.Row.Cells[7].Style.Value = "text-align:center;text-size:10pt;";
            e.Row.Cells[8].Style.Value = "text-align:center;text-size:10pt;";
            e.Row.Cells[9].Style.Value = "text-align:center;text-size:10pt;";
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string str = e.Row.Cells[0].Text.ToString();
            string[] str1 = str.Split('#');
            if (e.Row.Cells[0].Text.Contains("#1"))
            {
                e.Row.Cells[0].Text.Replace("#1", "");
                e.Row.Cells[0].Attributes.Add("Style", "padding-left:20px");
                e.Row.Cells[0].Text = str1[0];
            }

            if (e.Row.Cells[1].Text.Length > 15)
            {
                e.Row.Cells[1].ToolTip = e.Row.Cells[1].Text;
                e.Row.Cells[1].Text = e.Row.Cells[1].Text.Substring(0, 12) + "...";
            }
            if (e.Row.Cells[2].Text.Length > 15)
            {
                e.Row.Cells[2].ToolTip = e.Row.Cells[2].Text;
                e.Row.Cells[2].Text = e.Row.Cells[2].Text.Substring(0, 12) + "...";
            }
            int comp = Convert.ToInt32(e.Row.Cells[3].Text.Split(':').GetValue(0).ToString());

            if (comp < 10)
            {
                e.Row.Cells[3].Text = "0" + e.Row.Cells[3].Text;
            }
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = e.Row.Cells[0].Text.ToUpper();
            string pdtstr = e.Row.Cells[3].Text;
            if (pdtstr.Contains("#"))
                e.Row.Cells[3].Text = pdtstr.Replace("#", "<br>");
            else
                e.Row.Cells[3].Text = pdtstr;
            e.Row.Cells[5].Style.Value = "text-align:center;";
            e.Row.Cells[6].Style.Value = "text-align:center;";
            e.Row.Cells[7].Style.Value = "text-align:center;";
            e.Row.Cells[8].Style.Value = "text-align:center;";
            e.Row.Cells[9].Style.Value = "text-align:center;";
            string rpdtstr = e.Row.Cells[3].Text;
            if (rpdtstr.Contains("#"))
                e.Row.Cells[3].Text = rpdtstr.Replace("#", "<br>");
            else
                e.Row.Cells[3].Text = rpdtstr;
        }

        string Remarks = string.Empty;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[0].Text.Contains('*'))
            {
                string reno = e.Row.Cells[0].Text.Replace("*", "");
                e.Row.Cells[0].Text = reno;
            }
            if (e.Row.Cells[0].Text.Contains('+'))
            {
                string reno = e.Row.Cells[0].Text.Replace("+", "");
                e.Row.Cells[0].Text = reno;
            }
            if (e.Row.Cells[4].Text == "Ready")
            {
                e.Row.Cells[4].Attributes.Add("style", "text-align:left;color:limegreen;");
            }
            else if (e.Row.Cells[4].Text.Replace("#", "") == "Ready")
            {
                e.Row.Cells[4].Text = e.Row.Cells[4].Text.Replace("#", "");
                e.Row.Cells[4].Attributes.Add("style", "text-align:left;color:limegreen;");
            }
            else if (e.Row.Cells[4].Text == "Delay")
            {
                e.Row.Cells[4].Attributes.Add("style", "text-align:left;color:orange;");
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 5; i < 10; i++)
            {
                if (e.Row.Cells[i].Text == "#")
                {
                    e.Row.Cells[i].Text = "";
                    e.Row.Cells[i].VerticalAlign = VerticalAlign.Middle;
                    e.Row.Cells[i].Attributes.Add("Style", "background-image: url('https://res.cloudinary.com/deekyp5bi/image/upload/v1484218921/img/circle_green.png');vertical-align:middle;text-align:center; background-repeat: no-repeat;background-position:center;");
                }
                else if (e.Row.Cells[i].Text == "$")
                {
                    e.Row.Cells[i].Text = "";
                    e.Row.Cells[i].Attributes.Add("Style", "background-image: url('https://res.cloudinary.com/deekyp5bi/image/upload/v1484218921/img/circle_yellow.png');vertical-align:middle; background-repeat: no-repeat;background-position:center;");
                }
                else if (e.Row.Cells[i].Text == "@")
                {
                    e.Row.Cells[i].Text = "";
                    e.Row.Cells[i].Attributes.Add("Style", "background-image: url('https://res.cloudinary.com/deekyp5bi/image/upload/v1484218921/img/circle_Grey.png');vertical-align:middle; background-repeat: no-repeat;background-position:center;");
                }
                else
                {
                    e.Row.Cells[i].Text = "";
                    e.Row.Cells[i].Attributes.Add("Style", "background-image: url('https://res.cloudinary.com/deekyp5bi/image/upload/v1484218921/img/Circle_Blue.png');vertical-align:middle; background-repeat: no-repeat;background-position:center;");
                }
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
        DataTable Dt = new DataTable();
        try
        {
            Dt = LoadData("", "");
            grdDisplay.DataSource = Dt;
            grdDisplay.DataBind();
            UpdatePanel1.Update();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            int pc = Dt.Rows.Count % 10;
            int totalpages = 0;
            if (pc > 0)
                totalpages = (Dt.Rows.Count / 10) + 1;
            else
                totalpages = (Dt.Rows.Count / 10);
            lblPgCount.Text = "Page: " + (grdDisplay.PageIndex + 1).ToString(); //+countval.ToString() + "/"+Dt.Rows.Count.ToString(); //+ "/" + totalpages.ToString()
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
    }

    private DataTable LoadData(string RegNo, string OwnerName)
    {
        
        SqlConnection oConn = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlCommand cmd = new SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "CustomerDisplayI";
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
        lbTime.Text = DateTime.Now.ToString("HH:mm:ss");
    }

    protected void btnBack_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("DisplayHome.aspx");
    }
}