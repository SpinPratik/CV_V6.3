using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class KPIDashboard : System.Web.UI.Page
{
    private string selectType = "";
    private static string Copyright;
    static string PendingCount = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["ConnectionString"] == null || Session["ConnectionString"].ToString() == "")
                Response.Redirect("login.aspx");
            //else
            //    Connection = Session["ConnectionString"].ToString();

        }
        catch
        {
            Response.Redirect("login.aspx");
        }




        if (!Page.IsPostBack)
        {
            //Copyright = DataManager.getVersion(Session["ConnectionString"].ToString());
            Session["selectType"] = selectType;
            FillGrid(selectType);
            lbMonthYr.Text = DateTime.Now.ToString("MMM-yyyy");
        }
        Session["Current_Page"] = "KPI Dashboard";
        this.Title = "KPI Dashboard";
    }
    protected void FillGrid(string selectType)
    {
        try
        {
            string sConnString = Session["ConnectionString"].ToString();
            SqlConnection con = new SqlConnection(sConnString);
            SqlCommand cmd = new SqlCommand("GetCSMDashboard_new1", con);

            cmd.CommandTimeout = 180;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Bodyshop", "0");
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            grdDPD.DataSource = null;
            grdDPD.DataSource = dt;
            grdDPD.DataBind();
            grdDPD.Rows[10].Visible = false;
        }
        catch (Exception ex)
        {
        }
    }
    protected void grdDPD_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[0].Visible = false;
        

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           // e.Row.Cells[9].Visible = false;
            for (int flag = 1; flag < e.Row.Cells.Count; flag++)
            {
                e.Row.Cells[flag].Width = new Unit(65);
            }
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            
         //   e.Row.Cells[9].Visible = false;
            for (int flag = 1; flag < e.Row.Cells.Count; flag++)
            {
                DateTime dateValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month, int.Parse(e.Row.Cells[flag].Text.Trim()));

                if (dateValue.ToString("ddd") == "Fri" || dateValue.ToString("ddd") == "Sat")
                {
                    if (e.Row.Cells[flag].Text.Length == 1)
                    {
                        e.Row.Cells[flag].Text = "0" + e.Row.Cells[flag].Text + "<br/>&nbsp;&nbsp;&nbsp;&nbsp;" + dateValue.ToString("ddd");
                        //e.Row.Cells[flag].Text = "&nbsp;&nbsp;&nbsp;&nbsp;0" + e.Row.Cells[flag].Text + "&nbsp;&nbsp;&nbsp;&nbsp;<br/>&nbsp;&nbsp;&nbsp;&nbsp;" + dateValue.ToString("ddd") + "&nbsp;&nbsp;&nbsp;&nbsp;";
                    }
                    else
                    {
                        e.Row.Cells[flag].Text = e.Row.Cells[flag].Text + "<br/>&nbsp;&nbsp;&nbsp;&nbsp;" + dateValue.ToString("ddd");
                        //e.Row.Cells[flag].Text = "&nbsp;&nbsp;&nbsp;&nbsp;" + e.Row.Cells[flag].Text + "&nbsp;&nbsp;&nbsp;&nbsp;<br/>&nbsp;&nbsp;&nbsp;&nbsp;" + dateValue.ToString("ddd") + "&nbsp;&nbsp;&nbsp;&nbsp;";
                    }
                }
                else
                {
                    if (e.Row.Cells[flag].Text.Length == 1)
                    {
                        e.Row.Cells[flag].Text = "0" + e.Row.Cells[flag].Text + "<br/>&nbsp;&nbsp;&nbsp;&nbsp;" + dateValue.ToString("ddd");
                        //e.Row.Cells[flag].Text = "&nbsp;&nbsp;&nbsp;0" + e.Row.Cells[flag].Text + "&nbsp;&nbsp;&nbsp;<br/>&nbsp;&nbsp;&nbsp;" + dateValue.ToString("ddd") + "&nbsp;&nbsp;&nbsp;";
                    }
                    else
                    {
                        e.Row.Cells[flag].Text = e.Row.Cells[flag].Text + "<br/>&nbsp;&nbsp;&nbsp;&nbsp;" + dateValue.ToString("ddd");
                        //e.Row.Cells[flag].Text = "&nbsp;&nbsp;&nbsp;" + e.Row.Cells[flag].Text + "&nbsp;&nbsp;&nbsp;<br/>&nbsp;&nbsp;&nbsp;" + dateValue.ToString("ddd") + "&nbsp;&nbsp;&nbsp;";
                    }
                }
                if (dateValue.ToString("ddd") == "Sun")
                {
                    e.Row.Cells[flag].BackColor = System.Drawing.Color.Red;
                    e.Row.Cells[flag].ForeColor = System.Drawing.Color.White;
                }
            }
        }
        if (e.Row.RowType == DataControlRowType.Header)
            e.Row.Font.Bold = false;
        
        if (e.Row.RowIndex == 2 || e.Row.RowIndex == 4 || e.Row.RowIndex == 12 || e.Row.RowIndex == 28 || e.Row.RowIndex == 22 || e.Row.RowIndex == 21)
        {
            for (int flag = 1; flag < e.Row.Cells.Count; flag++)
            {
                e.Row.Cells[flag].ForeColor = Color.White;
                if (Convert.ToDecimal(e.Row.Cells[flag].Text) < 70)
                {
                    e.Row.Cells[flag].BackColor = ColorTranslator.FromHtml("#ED3B3B");
                    // e.Row.Cells[flag].Text = "<table cellpadding=0 cellspacing=0 style=width:100%;table-layout:fixed;white-space:nowrap;><tr><td align=right valign=middle style=color:Maroon;>" + e.Row.Cells[flag].Text + "</td></tr></table>";
                }
                else if (Convert.ToDecimal(e.Row.Cells[flag].Text) < 90)
                {
                    e.Row.Cells[flag].BackColor = ColorTranslator.FromHtml("#E66F00");
                    //e.Row.Cells[flag].Text = "<table cellpadding=0 cellspacing=0 style=width:100%;table-layout:fixed;white-space:nowrap;><tr><td align=right valign=middle style=color:Orange;>" + e.Row.Cells[flag].Text + "</td></tr></table>";
                }
                else
                {
                    e.Row.Cells[flag].BackColor = ColorTranslator.FromHtml("#66A030");
                    // e.Row.Cells[flag].Text = "<table cellpadding=0 cellspacing=0 style=width:100%;table-layout:fixed;white-space:nowrap;><tr><td align=right valign=middle style=color:Green;>" + e.Row.Cells[flag].Text + "</td></tr></table>";
                }

            }
        }
        if (e.Row.RowIndex == 5)
        {
            for (int flag = 1; flag < e.Row.Cells.Count; flag++)
            {
                e.Row.Cells[flag].ForeColor = Color.White;
                if (Convert.ToDecimal(e.Row.Cells[flag].Text) < 12)
                {
                    e.Row.Cells[flag].BackColor = ColorTranslator.FromHtml("#66A030");
                    //e.Row.Cells[flag].Text = "<table cellpadding=0 cellspacing=0 style=width:100%;table-layout:fixed;white-space:nowrap;><tr><td align=right valign=middle style=color:Green;>" + e.Row.Cells[flag].Text + "</td></tr></table>";
                }
                else if (Convert.ToDecimal(e.Row.Cells[flag].Text) < 20)
                {
                    e.Row.Cells[flag].BackColor = ColorTranslator.FromHtml("#E66F00");
                    //e.Row.Cells[flag].Text = "<table cellpadding=0 cellspacing=0 style=width:100%;table-layout:fixed;white-space:nowrap;><tr><td align=right valign=middle style=color:Orange;>" + e.Row.Cells[flag].Text + "</td></tr></table>";
                }
                else
                {
                    e.Row.Cells[flag].BackColor = ColorTranslator.FromHtml("#ED3B3B");
                    //e.Row.Cells[flag].Text = "<table cellpadding=0 cellspacing=0 style=width:100%;table-layout:fixed;white-space:nowrap;><tr><td align=right valign=middle style=color:Maroon;>" + e.Row.Cells[flag].Text + "</td></tr></table>";
                }

            }
        }
        if (e.Row.RowIndex == 8)
        {
            for (int flag = 1; flag < e.Row.Cells.Count; flag++)
            {
                e.Row.Cells[flag].ForeColor = Color.White;
                if (Convert.ToDecimal(e.Row.Cells[flag].Text) < 65)
                {
                    e.Row.Cells[flag].BackColor = ColorTranslator.FromHtml("#ED3B3B");
                    //e.Row.Cells[flag].Text = "<table cellpadding=0 cellspacing=0 style=width:100%;table-layout:fixed;white-space:nowrap;><tr><td align=right valign=middle style=color:Maroon;>" + e.Row.Cells[flag].Text + "</td></tr></table>";
                }
                else if (Convert.ToDecimal(e.Row.Cells[flag].Text) < 80)
                {
                    e.Row.Cells[flag].BackColor = ColorTranslator.FromHtml("#E66F00");
                    // e.Row.Cells[flag].Text = "<table cellpadding=0 cellspacing=0 style=width:100%;table-layout:fixed;white-space:nowrap;><tr><td align=right valign=middle style=color:Orange;>" + e.Row.Cells[flag].Text + "</td></tr></table>";
                }
                else
                {
                    e.Row.Cells[flag].BackColor = ColorTranslator.FromHtml("#66A030");
                    //e.Row.Cells[flag].Text = "<table cellpadding=0 cellspacing=0 style=width:100%;table-layout:fixed;white-space:nowrap;><tr><td align=right valign=middle style=color:Green;>" + e.Row.Cells[flag].Text + "</td></tr></table>";
                }

            }
        }
        if (e.Row.RowIndex >= 13 && e.Row.RowIndex <= 17)
        {

            for (int flag = 1; flag < e.Row.Cells.Count; flag++)
            {
                e.Row.Cells[flag].ForeColor = Color.White;
                if (Convert.ToDecimal(e.Row.Cells[flag].Text) < 60)
                {
                    e.Row.Cells[flag].BackColor = ColorTranslator.FromHtml("#ED3B3B");
                    //e.Row.Cells[flag].Text = "<table cellpadding=0 cellspacing=0 style=width:100%;table-layout:fixed;white-space:nowrap;><tr><td align=right valign=middle style=color:Maroon;>" + e.Row.Cells[flag].Text + "</td></tr></table>";
                }
                else if (Convert.ToDecimal(e.Row.Cells[flag].Text) < 80)
                {
                    e.Row.Cells[flag].BackColor = ColorTranslator.FromHtml("#E66F00");
                    //e.Row.Cells[flag].Text = "<table cellpadding=0 cellspacing=0 style=width:100%;table-layout:fixed;white-space:nowrap;><tr><td align=right valign=middle style=color:Orange;>" + e.Row.Cells[flag].Text + "</td></tr></table>";
                }
                else
                {
                    e.Row.Cells[flag].BackColor = ColorTranslator.FromHtml("#66A030");
                    //e.Row.Cells[flag].Text = "<table cellpadding=0 cellspacing=0 style=width:100%;table-layout:fixed;white-space:nowrap;><tr><td align=right valign=middle style=color:Green;>" + e.Row.Cells[flag].Text + "</td></tr></table>";
                }

            }
        }
        if (e.Row.RowIndex == 18)
        {
            for (int flag = 1; flag < e.Row.Cells.Count; flag++)
            {
                e.Row.Cells[flag].ForeColor = Color.White;
                if (Convert.ToDecimal(e.Row.Cells[flag].Text) < 50 || Convert.ToDecimal(e.Row.Cells[flag].Text) > 110)
                {
                    e.Row.Cells[flag].BackColor = ColorTranslator.FromHtml("#ED3B3B");
                    //e.Row.Cells[flag].Text = "<table cellpadding=0 cellspacing=0 style=width:100%;table-layout:fixed;white-space:nowrap;><tr><td align=right valign=middle style=color:Maroon;>" + e.Row.Cells[flag].Text + "</td></tr></table>";
                }
                else if (Convert.ToDouble(e.Row.Cells[flag].Text) < 80)
                {
                    e.Row.Cells[flag].BackColor = ColorTranslator.FromHtml("#E66F00");
                    //e.Row.Cells[flag].Text = "<table cellpadding=0 cellspacing=0 style=width:100%;table-layout:fixed;white-space:nowrap;><tr><td align=right valign=middle style=color:Orange;>" + e.Row.Cells[flag].Text + "</td></tr></table>";
                }
                else
                {
                    e.Row.Cells[flag].BackColor = ColorTranslator.FromHtml("#66A030");
                    //e.Row.Cells[flag].Text = "<table cellpadding=0 cellspacing=0 style=width:100%;table-layout:fixed;white-space:nowrap;><tr><td align=right valign=middle style=color:Green;>" + e.Row.Cells[flag].Text + "</td></tr></table>";
                }

            }
        }
        if (e.Row.RowIndex == 19)
        {
            for (int flag = 1; flag < e.Row.Cells.Count; flag++)
            {
                e.Row.Cells[flag].ForeColor = Color.White;
                if (Convert.ToDecimal(e.Row.Cells[flag].Text) < 50 || Convert.ToDecimal(e.Row.Cells[flag].Text) > 110)
                {
                    e.Row.Cells[flag].BackColor = ColorTranslator.FromHtml("#ED3B3B");
                    //e.Row.Cells[flag].Text = "<table cellpadding=0 cellspacing=0 style=width:100%;table-layout:fixed;white-space:nowrap;><tr><td align=right valign=middle style=color:Maroon;>" + e.Row.Cells[flag].Text + "</td></tr></table>";
                }
                else if (Convert.ToDouble(e.Row.Cells[flag].Text) < 80)
                {
                    e.Row.Cells[flag].BackColor = ColorTranslator.FromHtml("#E66F00");
                    //e.Row.Cells[flag].Text = "<table cellpadding=0 cellspacing=0 style=width:100%;table-layout:fixed;white-space:nowrap;><tr><td align=right valign=middle style=color:Orange;>" + e.Row.Cells[flag].Text + "</td></tr></table>";
                }
                else
                {
                    e.Row.Cells[flag].BackColor = ColorTranslator.FromHtml("#66A030");
                    //e.Row.Cells[flag].Text = "<table cellpadding=0 cellspacing=0 style=width:100%;table-layout:fixed;white-space:nowrap;><tr><td align=right valign=middle style=color:Green;>" + e.Row.Cells[flag].Text + "</td></tr></table>";
                }

            }
        }
        if (e.Row.RowIndex == 20 || e.Row.RowIndex == 25)
        {
            for (int flag = 1; flag < e.Row.Cells.Count; flag++)
            {
                e.Row.Cells[flag].ForeColor = Color.White;
                if (Convert.ToDecimal(e.Row.Cells[flag].Text) < 45)
                {
                    e.Row.Cells[flag].BackColor = ColorTranslator.FromHtml("#ED3B3B");
                    //e.Row.Cells[flag].Text = "<table cellpadding=0 cellspacing=0 style=width:100%;table-layout:fixed;white-space:nowrap;><tr><td align=right valign=middle style=color:Maroon;>" + e.Row.Cells[flag].Text + "</td></tr></table>";
                }
                else if (Convert.ToDecimal(e.Row.Cells[flag].Text) < 70)
                {
                    e.Row.Cells[flag].BackColor = ColorTranslator.FromHtml("#E66F00");
                    //e.Row.Cells[flag].Text = "<table cellpadding=0 cellspacing=0 style=width:100%;table-layout:fixed;white-space:nowrap;><tr><td align=right valign=middle style=color:Orange;>" + e.Row.Cells[flag].Text + "</td></tr></table>";
                }
                else
                {
                    e.Row.Cells[flag].BackColor = ColorTranslator.FromHtml("#66A030");
                    //e.Row.Cells[flag].Text = "<table cellpadding=0 cellspacing=0 style=width:100%;table-layout:fixed;white-space:nowrap;><tr><td align=right valign=middle style=color:Green;>" + e.Row.Cells[flag].Text + "</td></tr></table>";
                }

            }
        }
        if (e.Row.RowIndex == 26)
        {

            for (int flag = 1; flag < e.Row.Cells.Count; flag++)
            {
                e.Row.Cells[flag].ForeColor = Color.White;
                if (Convert.ToDecimal(e.Row.Cells[flag].Text) < 45 || Convert.ToDecimal(e.Row.Cells[flag].Text) > 110)
                {
                    e.Row.Cells[flag].BackColor = ColorTranslator.FromHtml("#ED3B3B"); ;
                    // e.Row.Cells[flag].Text = "<table cellpadding=0 cellspacing=0 style=width:100%;table-layout:fixed;white-space:nowrap;><tr><td align=right valign=middle style=color:Maroon;>" + e.Row.Cells[flag].Text + "</td></tr></table>";
                }
                else if (Convert.ToDecimal(e.Row.Cells[flag].Text) < 70)
                {
                    e.Row.Cells[flag].BackColor = ColorTranslator.FromHtml("#E66F00");
                    // e.Row.Cells[flag].Text = "<table cellpadding=0 cellspacing=0 style=width:100%;table-layout:fixed;white-space:nowrap;><tr><td align=right valign=middle style=color:Orange;>" + e.Row.Cells[flag].Text + "</td></tr></table>";
                }
                else
                {
                    e.Row.Cells[flag].BackColor = ColorTranslator.FromHtml("#66A030");
                    //e.Row.Cells[flag].Text = "<table cellpadding=0 cellspacing=0 style=width:100%;table-layout:fixed;white-space:nowrap;><tr><td align=right valign=middle style=color:Green;>" + e.Row.Cells[flag].Text + "</td></tr></table>";
                }

            }
        }
        //if (e.Row.RowIndex == 9)
        //{
        //    for (int flag = 1; flag < e.Row.Cells.Count; flag++)
        //    {
        //        if (e.Row.Cells[flag].Text.Trim() != "&nbsp;")
        //        {
        //            e.Row.Cells[flag].ForeColor = Color.Black;
        //            PendingCount = e.Row.Cells[flag].Text.ToString().Split('#')[0];
        //            if (e.Row.Cells[flag].Text.ToString().Split('#')[1] == "3")
        //            {
        //                e.Row.Cells[flag].ForeColor = Color.Black;
        //                // e.Row.Cells[flag].BackColor = ColorTranslator.FromHtml("#ED3B3B");
        //                e.Row.Cells[flag].Text = PendingCount;
        //                //"<table cellpadding=0 cellspacing=0 style=width:100%;table-layout:fixed;white-space:nowrap;><tr><td align=right valign=middle style=color:Maroon;>" + PendingCount + "</td></tr></table>";
        //            }
        //            else if (e.Row.Cells[flag].Text.ToString().Split('#')[1] == "2")
        //            {
        //                e.Row.Cells[flag].ForeColor = Color.Black;
        //                //   e.Row.Cells[flag].BackColor = ColorTranslator.FromHtml("#E66F00");
        //                e.Row.Cells[flag].Text = PendingCount;
        //                //"<table cellpadding=0 cellspacing=0 style=width:100%;table-layout:fixed;white-space:nowrap;><tr><td align=right valign=middle style=color:Orange;>" + PendingCount + "</td></tr></table>";
        //            }
        //            else
        //            {
        //                e.Row.Cells[flag].ForeColor = Color.Black;
        //                // e.Row.Cells[flag].BackColor = ColorTranslator.FromHtml("#66A030");
        //                e.Row.Cells[flag].Text = PendingCount;
        //                //"<table cellpadding=0 cellspacing=0 style=width:100%;table-layout:fixed;white-space:nowrap;><tr><td align=right valign=middle style=color:Green;>" + PendingCount + "</td></tr></table>";
        //            }
        //        }

        //    }
        //}

        //if (e.Row.RowIndex == 10)
        //{
        //    for (int flag = 1; flag < e.Row.Cells.Count; flag++)
        //    {
        //        if (e.Row.Cells[flag].Text.Trim() != "&nbsp;")
        //        {
        //            e.Row.Cells[flag].ForeColor = Color.Black;
        //            PendingCount = e.Row.Cells[flag].Text.ToString().Split('#')[0];
        //            if (e.Row.Cells[flag].Text.ToString().Split('#')[1] == "3")
        //            {
        //                e.Row.Cells[flag].ForeColor = Color.Black;
        //                // e.Row.Cells[flag].BackColor = ColorTranslator.FromHtml("#ED3B3B");
        //                e.Row.Cells[flag].Text = PendingCount;
        //                //"<table cellpadding=0 cellspacing=0 style=width:100%;table-layout:fixed;white-space:nowrap;><tr><td align=right valign=middle style=color:Maroon;>" + PendingCount + "</td></tr></table>";
        //            }
        //            else if (e.Row.Cells[flag].Text.ToString().Split('#')[1] == "2")
        //            {
        //                //e.Row.Cells[flag].BackColor = ColorTranslator.FromHtml("#E66F00");
        //                e.Row.Cells[flag].Text = PendingCount;
        //                //"<table cellpadding=0 cellspacing=0 style=width:100%;table-layout:fixed;white-space:nowrap;><tr><td align=right valign=middle style=color:Orange;>" + PendingCount + "</td></tr></table>";
        //            }
        //            else
        //            {
        //                //  e.Row.Cells[flag].BackColor = ColorTranslator.FromHtml("#66A030");
        //                e.Row.Cells[flag].Text = PendingCount;
        //                //"<table cellpadding=0 cellspacing=0 style=width:100%;table-layout:fixed;white-space:nowrap;><tr><td align=right valign=middle style=color:Green;>" + PendingCount + "</td></tr></table>";
        //            }
        //        }

        //    }
        //}
        //if (e.Row.RowIndex == 11)
        //{
        //    for (int flag = 1; flag < e.Row.Cells.Count; flag++)
        //    {
        //        if (e.Row.Cells[flag].Text.Trim() != "&nbsp;")
        //        {
        //            e.Row.Cells[flag].ForeColor = Color.Black;
        //            PendingCount = e.Row.Cells[flag].Text.ToString().Split('#')[0];
        //            if (e.Row.Cells[flag].Text.ToString().Split('#')[1] == "3")
        //            {
        //                e.Row.Cells[flag].ForeColor = Color.Black;
        //                // e.Row.Cells[flag].BackColor = ColorTranslator.FromHtml("#ED3B3B");
        //                e.Row.Cells[flag].Text = PendingCount;
        //                //"<table cellpadding=0 cellspacing=0 style=width:100%;table-layout:fixed;white-space:nowrap;><tr><td align=right valign=middle style=color:Maroon;>" + PendingCount + "</td></tr></table>";
        //            }
        //            else if (e.Row.Cells[flag].Text.ToString().Split('#')[1] == "2")
        //            {
        //                //e.Row.Cells[flag].BackColor = ColorTranslator.FromHtml("#E66F00");
        //                e.Row.Cells[flag].Text = PendingCount;
        //                //"<table cellpadding=0 cellspacing=0 style=width:100%;table-layout:fixed;white-space:nowrap;><tr><td align=right valign=middle style=color:Orange;>" + PendingCount + "</td></tr></table>";
        //            }
        //            else
        //            {
        //                //  e.Row.Cells[flag].BackColor = ColorTranslator.FromHtml("#66A030");
        //                e.Row.Cells[flag].Text = PendingCount;
        //                //"<table cellpadding=0 cellspacing=0 style=width:100%;table-layout:fixed;white-space:nowrap;><tr><td align=right valign=middle style=color:Green;>" + PendingCount + "</td></tr></table>";
        //            }
        //        }

        //    }
        //}
        //if (e.Row.RowIndex == 9)
        //{
        //    for (int flag = 1; flag < e.Row.Cells.Count; flag++)
        //    {
        //        if (e.Row.Cells[flag].Text.Trim() != "&nbsp;")
        //        {
        //            PendingCount = e.Row.Cells[flag].Text.ToString().Split('#')[0];
        //            if (PendingCount <= 2)
        //            {
        //                e.Row.Cells[flag].BackColor = Color.Red;
        //                e.Row.Cells[flag].Text = PendingCount;
        //                //"<table cellpadding=0 cellspacing=0 style=width:100%;table-layout:fixed;white-space:nowrap;><tr><td align=right valign=middle style=color:Maroon;>" + PendingCount + "</td></tr></table>";
        //            }
        //            else if (e.Row.Cells[flag].Text.ToString().Split('#')[1] == "2")
        //            {
        //                e.Row.Cells[flag].BackColor = Color.Orange;
        //                e.Row.Cells[flag].Text = PendingCount;
        //                //"<table cellpadding=0 cellspacing=0 style=width:100%;table-layout:fixed;white-space:nowrap;><tr><td align=right valign=middle style=color:Orange;>" + PendingCount + "</td></tr></table>";
        //            }
        //            else
        //            {
        //                e.Row.Cells[flag].BackColor = Color.Green;
        //                e.Row.Cells[flag].Text = PendingCount;
        //                //"<table cellpadding=0 cellspacing=0 style=width:100%;table-layout:fixed;white-space:nowrap;><tr><td align=right valign=middle style=color:Green;>" + PendingCount + "</td></tr></table>";
        //            }
        //        }

        //    }
        //}
        if (e.Row.RowIndex == 26 || e.Row.RowIndex == 30 || e.Row.RowIndex == 32)
        {
            for (int flag = 1; flag < e.Row.Cells.Count; flag++)
            {
                if (e.Row.Cells[flag].Text.Trim() != "&nbsp;" || e.Row.Cells[flag].Text.Trim() != "")
                {
                    //=int(Round(Fields!BayIdle.Value).ToString()/60).ToString().PadLeft(2,"0")+
                    //    ":"+int(Round(Fields!BayIdle.Value).ToString() Mod 60).ToString().PadLeft(2,"0")
                    string str1 = e.Row.Cells[flag].Text.Replace(".00", " ");
                    Int64 temp = Convert.ToInt64(str1);
                    Int64 hr = (temp) / 60;
                    Int64 mint = temp % 60;

                    string timeInHours = hr + ":" + mint.ToString("00");
                    e.Row.Cells[flag].Text = timeInHours;
                }
            }
        }


    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
         FillGrid(selectType);
    }

    protected void GenerateAutoSAPerformance(object sender, ImageClickEventArgs e)
    {
        try
        {
            Session["SlNo"] = 65;
            Response.Redirect("ReportsPageForSMDashBoards.aspx", false);
        }
        catch (Exception ex)
        {
        }
    }
    protected void GenerateAutoreportSAWiseDelivery(object sender, ImageClickEventArgs e)
    {
        try
        {
            Session["SlNo"] = 57;
            Response.Redirect("ReportsPageForSMDashBoards.aspx", false);
        }
        catch (Exception ex)
        {
        }
    }
    protected void GenerateAutoreportCarryForwardVehicles(object sender, ImageClickEventArgs e)
    {
        try
        {
            Session["SlNo"] = 12;
            Response.Redirect("ReportsPageForSMDashBoards.aspx", false);
        }
        catch (Exception ex)
        {
        }
    }
    protected void GenerateAutoreportCardScanAdherence(object sender, ImageClickEventArgs e)
    {
        try
        {
            Session["SlNo"] = 69;
            Response.Redirect("ReportsPageForSMDashBoards.aspx", false);
        }
        catch (Exception ex)
        {
        }
    }
    protected void GenerateAutoreportBayTMR(object sender, ImageClickEventArgs e)
    {
        try
        {
            Session["SlNo"] = 54;
            Response.Redirect("ReportsPageForSMDashBoards.aspx", false);
        }
        catch (Exception ex)
        {
        }
    }
    protected void GenerateAutoreportEmployeeEngagement(object sender, ImageClickEventArgs e)
    {
        try
        {
            Session["SlNo"] = 18;
            Response.Redirect("ReportsPageForSMDashBoards.aspx", false);
        }
        catch (Exception ex)
        {
        }
    }
    protected void GenerateAutoreportEmployeeAttendance(object sender, ImageClickEventArgs e)
    {
        try
        {
            Session["SlNo"] = 7;
            Response.Redirect("ReportsPageForSMDashBoards.aspx", false);
        }
        catch (Exception ex)
        {
        }
    }
}