using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CSMDashboard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FillGrid("0");
        Session["CURRENT_PAGE"] = "CSM Dashboard";
        this.Title = "CSM Dashboard";
        lbMonthYr.Text = "Feb-2013";
    }

    protected void FillGrid(string selectType)
    {
        try
        {
            string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
            SqlConnection con = new SqlConnection(sConnString);
            SqlCommand cmd = new SqlCommand("GetDashboardCSM", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Bodyshop", Convert.ToString(selectType));
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            grdDPD.DataSource = dt;
            grdDPD.DataBind();
        }
        catch (Exception ex)
        {
        }
    }

    protected void grdDPD_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[0].Visible = false;
        if (e.Row.RowType == DataControlRowType.Header)
        {
            for (int flag = 1; flag < e.Row.Cells.Count; flag++)
            {
                DateTime dateValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month, int.Parse(e.Row.Cells[flag].Text.Trim()));
                if (e.Row.Cells[flag].Text.Length == 1)
                {
                    e.Row.Cells[flag].Text = "&nbsp;&nbsp;&nbsp;0" + e.Row.Cells[flag].Text + "&nbsp;&nbsp;&nbsp;<br/>&nbsp;&nbsp;&nbsp;" + dateValue.ToString("ddd") + "&nbsp;&nbsp;&nbsp;";
                }
                else
                {
                    e.Row.Cells[flag].Text = "&nbsp;&nbsp;&nbsp;" + e.Row.Cells[flag].Text + "&nbsp;&nbsp;&nbsp;<br/>&nbsp;&nbsp;&nbsp;" + dateValue.ToString("ddd") + "&nbsp;&nbsp;&nbsp;";
                }
                if (dateValue.ToString("ddd") == "Sun")
                {
                    e.Row.Cells[flag].BackColor = System.Drawing.Color.Red;
                    e.Row.Cells[flag].ForeColor = System.Drawing.Color.White;
                }
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int flag = 0; flag < e.Row.Cells.Count; flag++)
            {
                e.Row.Cells[flag].Width = new Unit("100px");
            }
            if (e.Row.RowIndex == 0 || e.Row.RowIndex == 3)
            {
                for (int flag = 1; flag < e.Row.Cells.Count; flag++)
                {
                    if (e.Row.Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 1)
                        e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 4);
                    else
                        e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 5);
                }
            }
            if (e.Row.RowIndex == 2 || e.Row.RowIndex == 5 || e.Row.RowIndex == 22)
            {
                for (int flag = 1; flag < e.Row.Cells.Count; flag++)
                {
                    int i = e.Row.RowIndex;
                    if (Convert.ToDecimal(e.Row.Cells[flag].Text) < 70)
                    {
                        if (grdDPD.Rows[i - 1].Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 1)
                            grdDPD.Rows[i - 1].Cells[flag].Text = grdDPD.Rows[i - 1].Cells[flag].Text.Substring(0, 4);
                        else if (grdDPD.Rows[i - 1].Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 3)
                            grdDPD.Rows[i - 1].Cells[flag].Text = grdDPD.Rows[i - 1].Cells[flag].Text.Substring(0, 6);
                        else
                            grdDPD.Rows[i - 1].Cells[flag].Text = grdDPD.Rows[i - 1].Cells[flag].Text.Substring(0, 5);
                        if (e.Row.Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 1)
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 4);
                        else if (e.Row.Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 3)
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 6);
                        else
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 5);
                        grdDPD.Rows[i - 1].Cells[flag].BackColor = System.Drawing.ColorTranslator.FromHtml("Red");
                        e.Row.Cells[flag].BackColor = System.Drawing.ColorTranslator.FromHtml("Red");
                    }
                    else if (Convert.ToDecimal(e.Row.Cells[flag].Text) < 90)
                    {
                        if (grdDPD.Rows[i - 1].Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 1)
                            grdDPD.Rows[i - 1].Cells[flag].Text = grdDPD.Rows[i - 1].Cells[flag].Text.Substring(0, 4);
                        else if (grdDPD.Rows[i - 1].Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 3)
                            grdDPD.Rows[i - 1].Cells[flag].Text = grdDPD.Rows[i - 1].Cells[flag].Text.Substring(0, 6);
                        else
                            grdDPD.Rows[i - 1].Cells[flag].Text = grdDPD.Rows[i - 1].Cells[flag].Text.Substring(0, 5);
                        if (e.Row.Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 1)
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 4);
                        else if (e.Row.Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 3)
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 6);
                        else
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 5);
                        grdDPD.Rows[i - 1].Cells[flag].BackColor = System.Drawing.ColorTranslator.FromHtml("Yellow");
                        e.Row.Cells[flag].BackColor = System.Drawing.ColorTranslator.FromHtml("Yellow");
                    }
                    else
                    {
                        if (grdDPD.Rows[i - 1].Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 1)
                            grdDPD.Rows[i - 1].Cells[flag].Text = grdDPD.Rows[i - 1].Cells[flag].Text.Substring(0, 4);
                        else if (grdDPD.Rows[i - 1].Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 3)
                            grdDPD.Rows[i - 1].Cells[flag].Text = grdDPD.Rows[i - 1].Cells[flag].Text.Substring(0, 6);
                        else
                            grdDPD.Rows[i - 1].Cells[flag].Text = grdDPD.Rows[i - 1].Cells[flag].Text.Substring(0, 5);
                        if (e.Row.Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 1)
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 4);
                        else if (e.Row.Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 3)
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 6);
                        else
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 5);
                        grdDPD.Rows[i - 1].Cells[flag].BackColor = System.Drawing.ColorTranslator.FromHtml("LimeGreen");
                        e.Row.Cells[flag].BackColor = System.Drawing.ColorTranslator.FromHtml("LimeGreen");
                    }
                }
            }
            if (e.Row.RowIndex == 9)
            {
                for (int flag = 1; flag < e.Row.Cells.Count; flag++)
                {
                    int i = e.Row.RowIndex;
                    if (Convert.ToDecimal(e.Row.Cells[flag].Text) < 65)
                    {
                        if (grdDPD.Rows[i - 1].Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 1)
                            grdDPD.Rows[i - 1].Cells[flag].Text = grdDPD.Rows[i - 1].Cells[flag].Text.Substring(0, 4);
                        else if (grdDPD.Rows[i - 1].Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 3)
                            grdDPD.Rows[i - 1].Cells[flag].Text = grdDPD.Rows[i - 1].Cells[flag].Text.Substring(0, 6);
                        else
                            grdDPD.Rows[i - 1].Cells[flag].Text = grdDPD.Rows[i - 1].Cells[flag].Text.Substring(0, 5);
                        if (e.Row.Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 1)
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 4);
                        else if (e.Row.Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 3)
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 6);
                        else
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 5);
                        grdDPD.Rows[i - 1].Cells[flag].BackColor = System.Drawing.ColorTranslator.FromHtml("Red");
                        e.Row.Cells[flag].BackColor = System.Drawing.ColorTranslator.FromHtml("Red");
                    }
                    else if (Convert.ToDecimal(e.Row.Cells[flag].Text) < 80)
                    {
                        if (grdDPD.Rows[i - 1].Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 1)
                            grdDPD.Rows[i - 1].Cells[flag].Text = grdDPD.Rows[i - 1].Cells[flag].Text.Substring(0, 4);
                        else if (grdDPD.Rows[i - 1].Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 3)
                            grdDPD.Rows[i - 1].Cells[flag].Text = grdDPD.Rows[i - 1].Cells[flag].Text.Substring(0, 6);
                        else
                            grdDPD.Rows[i - 1].Cells[flag].Text = grdDPD.Rows[i - 1].Cells[flag].Text.Substring(0, 5);
                        if (e.Row.Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 1)
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 4);
                        else if (e.Row.Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 3)
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 6);
                        else
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 5);
                        grdDPD.Rows[i - 1].Cells[flag].BackColor = System.Drawing.ColorTranslator.FromHtml("Yellow");
                        e.Row.Cells[flag].BackColor = System.Drawing.ColorTranslator.FromHtml("Yellow");
                    }
                    else
                    {
                        if (grdDPD.Rows[i - 1].Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 1)
                            grdDPD.Rows[i - 1].Cells[flag].Text = grdDPD.Rows[i - 1].Cells[flag].Text.Substring(0, 4);
                        else if (grdDPD.Rows[i - 1].Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 3)
                            grdDPD.Rows[i - 1].Cells[flag].Text = grdDPD.Rows[i - 1].Cells[flag].Text.Substring(0, 6);
                        else
                            grdDPD.Rows[i - 1].Cells[flag].Text = grdDPD.Rows[i - 1].Cells[flag].Text.Substring(0, 5);
                        if (e.Row.Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 1)
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 4);
                        else if (e.Row.Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 3)
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 6);
                        else
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 5);
                        grdDPD.Rows[i - 1].Cells[flag].BackColor = System.Drawing.ColorTranslator.FromHtml("LimeGreen");
                        e.Row.Cells[flag].BackColor = System.Drawing.ColorTranslator.FromHtml("LimeGreen");
                    }
                }
            }
            if (e.Row.RowIndex == 10)
            {
                for (int flag = 1; flag < e.Row.Cells.Count; flag++)
                {
                    int i = e.Row.RowIndex;
                    if (Convert.ToDecimal(e.Row.Cells[flag].Text) < 70)
                    {
                        if (flag - 1 == 0)
                        {
                            if (e.Row.Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 1)
                            {
                                e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 4);
                            }
                            else if (e.Row.Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 3)
                            {
                                e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 6);
                            }
                            else
                            {
                                e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 5);
                            }
                        }
                        else
                        {
                            e.Row.Cells[flag].Text = "";
                        }
                        e.Row.Cells[flag].Style.Add("border-Style", "none");
                        e.Row.Cells[flag].BackColor = System.Drawing.ColorTranslator.FromHtml("Red");
                    }
                    else if (Convert.ToDecimal(e.Row.Cells[flag].Text) < 90)
                    {
                        if (flag - 1 == 0)
                        {
                            if (e.Row.Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 1)
                            {
                                e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 4);
                            }
                            else if (e.Row.Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 3)
                            {
                                e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 6);
                            }
                            else
                            {
                                e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 5);
                            }
                        }
                        else
                        {
                            e.Row.Cells[flag].Text = "";
                        }
                        e.Row.Cells[flag].Style.Add("border-Style", "none");
                        e.Row.Cells[flag].BackColor = System.Drawing.ColorTranslator.FromHtml("Yellow");
                    }
                    else
                    {
                        if (flag - 1 == 0)
                        {
                            if (e.Row.Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 1)
                            {
                                e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 4);
                            }
                            else if (e.Row.Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 3)
                            {
                                e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 6);
                            }
                            else
                            {
                                e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 5);
                            }
                        }
                        else
                        {
                            e.Row.Cells[flag].Text = "";
                        }
                        e.Row.Cells[flag].Style.Add("border-Style", "none");
                        e.Row.Cells[flag].BackColor = System.Drawing.ColorTranslator.FromHtml("LimeGreen");
                    }
                }
            }
            if (e.Row.RowIndex >= 11 && e.Row.RowIndex <= 14)
            {
                for (int flag = 1; flag < e.Row.Cells.Count; flag++)
                {
                    int i = e.Row.RowIndex;
                    if (Convert.ToDecimal(e.Row.Cells[flag].Text) < 60)
                    {
                        if (flag - 1 == 0)
                        {
                            if (e.Row.Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 1)
                            {
                                e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 4);
                            }
                            else if (e.Row.Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 3)
                            {
                                e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 6);
                            }
                            else
                            {
                                e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 5);
                            }
                        }
                        else
                        {
                            e.Row.Cells[flag].Text = "";
                        }
                        e.Row.Cells[flag].Style.Add("border-Style", "none");
                        e.Row.Cells[flag].BackColor = System.Drawing.ColorTranslator.FromHtml("Red");
                    }
                    else if (Convert.ToDecimal(e.Row.Cells[flag].Text) < 80)
                    {
                        if (flag - 1 == 0)
                        {
                            if (e.Row.Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 1)
                            {
                                e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 4);
                            }
                            else if (e.Row.Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 3)
                            {
                                e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 6);
                            }
                            else
                            {
                                e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 5);
                            }
                        }
                        else
                        {
                            e.Row.Cells[flag].Text = "";
                        }
                        e.Row.Cells[flag].Style.Add("border-Style", "none");
                        e.Row.Cells[flag].BackColor = System.Drawing.ColorTranslator.FromHtml("Yellow");
                    }
                    else
                    {
                        if (flag - 1 == 0)
                        {
                            if (e.Row.Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 1)
                            {
                                e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 4);
                            }
                            else if (e.Row.Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 3)
                            {
                                e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 6);
                            }
                            else
                            {
                                e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 5);
                            }
                        }
                        else
                        {
                            e.Row.Cells[flag].Text = "";
                        }
                        e.Row.Cells[flag].Style.Add("border-Style", "none");
                        e.Row.Cells[flag].BackColor = System.Drawing.ColorTranslator.FromHtml("LimeGreen");
                    }
                }
            }
            if (e.Row.RowIndex == 15)
            {
                for (int flag = 1; flag < e.Row.Cells.Count; flag++)
                {
                    int i = e.Row.RowIndex;
                    if (Convert.ToDecimal(e.Row.Cells[flag].Text) < 1)
                    {
                        if (e.Row.Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 1)
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 4);
                        else if (e.Row.Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 3)
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 6);
                        else
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 5);
                        e.Row.Cells[flag].BackColor = System.Drawing.ColorTranslator.FromHtml("Red");
                    }
                    else if (Convert.ToDecimal(e.Row.Cells[flag].Text) < 2)
                    {
                        if (e.Row.Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 1)
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 4);
                        else if (e.Row.Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 3)
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 6);
                        else
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 5);
                        e.Row.Cells[flag].BackColor = System.Drawing.ColorTranslator.FromHtml("Yellow");
                    }
                    else
                    {
                        if (e.Row.Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 1)
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 4);
                        else if (e.Row.Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 3)
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 6);
                        else
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 5);
                        e.Row.Cells[flag].BackColor = System.Drawing.ColorTranslator.FromHtml("LimeGreen");
                    }
                }
            }
            if (e.Row.RowIndex == 16)
            {
                for (int flag = 1; flag < e.Row.Cells.Count; flag++)
                {
                    int i = e.Row.RowIndex;
                    if (Convert.ToDecimal(e.Row.Cells[flag].Text) < 3)
                    {
                        if (e.Row.Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 1)
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 4);
                        else if (e.Row.Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 3)
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 6);
                        else
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 5);
                        e.Row.Cells[flag].BackColor = System.Drawing.ColorTranslator.FromHtml("Red");
                    }
                    else if (Convert.ToDecimal(e.Row.Cells[flag].Text) < 5)
                    {
                        if (e.Row.Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 1)
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 4);
                        else if (e.Row.Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 3)
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 6);
                        else
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 5);
                        e.Row.Cells[flag].BackColor = System.Drawing.ColorTranslator.FromHtml("Yellow");
                    }
                    else
                    {
                        if (e.Row.Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 1)
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 4);
                        else if (e.Row.Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 3)
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 6);
                        else
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 5);
                        e.Row.Cells[flag].BackColor = System.Drawing.ColorTranslator.FromHtml("LimeGreen");
                    }
                }
            }
            if (e.Row.RowIndex >= 17 && e.Row.RowIndex <= 20)
            {
                for (int flag = 1; flag < e.Row.Cells.Count; flag++)
                {
                    int i = e.Row.RowIndex;
                    if (Convert.ToDecimal(e.Row.Cells[flag].Text) < 45)
                    {
                        if (e.Row.Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 1)
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 4);
                        else if (e.Row.Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 3)
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 6);
                        else
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 5);
                        e.Row.Cells[flag].BackColor = System.Drawing.ColorTranslator.FromHtml("Red");
                    }
                    else if (Convert.ToDecimal(e.Row.Cells[flag].Text) < 70)
                    {
                        if (e.Row.Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 1)
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 4);
                        else if (e.Row.Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 3)
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 6);
                        else
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 5);
                        e.Row.Cells[flag].BackColor = System.Drawing.ColorTranslator.FromHtml("Yellow");
                    }
                    else
                    {
                        if (e.Row.Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 1)
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 4);
                        else if (e.Row.Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 3)
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 6);
                        else
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 5);
                        e.Row.Cells[flag].BackColor = System.Drawing.ColorTranslator.FromHtml("LimeGreen");
                    }
                }
            }

            if (e.Row.RowIndex == 6)
            {
                for (int flag = 1; flag < e.Row.Cells.Count; flag++)
                {
                    if (Convert.ToDecimal(e.Row.Cells[flag].Text) > 20 || Convert.ToDecimal(e.Row.Cells[flag].Text) == 0)
                    {
                        if (e.Row.Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 1)
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 4);
                        else if (e.Row.Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 3)
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 6);
                        else
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 5);
                        e.Row.Cells[flag].BackColor = System.Drawing.ColorTranslator.FromHtml("Red");
                    }
                    else if (Convert.ToDecimal(e.Row.Cells[flag].Text) > 12 && Convert.ToDecimal(e.Row.Cells[flag].Text) < 20)
                    {
                        if (e.Row.Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 1)
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 4);
                        else if (e.Row.Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 3)
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 6);
                        else
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 5);
                        e.Row.Cells[flag].BackColor = System.Drawing.ColorTranslator.FromHtml("Yellow");
                    }
                    else
                    {
                        if (e.Row.Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 1)
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 4);
                        else if (e.Row.Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 3)
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 6);
                        else
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 5);
                        e.Row.Cells[flag].BackColor = System.Drawing.ColorTranslator.FromHtml("LimeGreen");
                    }
                }
            }

            if (e.Row.RowIndex == 7)
            {
                for (int flag = 1; flag < e.Row.Cells.Count; flag++)
                {
                    if (e.Row.Cells[flag].Text.ToString().Split('.')[0].Substring(2, 0) == "1")
                    {
                        if (e.Row.Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 1)
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 4);
                        else if (e.Row.Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 3)
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 6);
                        else
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 5);
                        e.Row.Cells[flag].BackColor = System.Drawing.ColorTranslator.FromHtml("LimeGreen");
                    }
                    else if (e.Row.Cells[flag].Text.ToString().Split('.')[0].Substring(2, 0) == "2")
                    {
                        if (e.Row.Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 1)
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 4);
                        else if (e.Row.Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 3)
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 6);
                        else
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 5);
                        e.Row.Cells[flag].BackColor = System.Drawing.ColorTranslator.FromHtml("Yellow");
                    }
                    else
                    {
                        if (e.Row.Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 1)
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 4);
                        else if (e.Row.Cells[flag].Text.ToString().Split('.')[0].ToString().Length == 3)
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 6);
                        else
                            e.Row.Cells[flag].Text = e.Row.Cells[flag].Text.Substring(0, 5);
                        e.Row.Cells[flag].BackColor = System.Drawing.ColorTranslator.FromHtml("Red");
                    }
                }
            }
        }
        if (e.Row.RowType == DataControlRowType.Header)
            e.Row.Font.Bold = false;
    }

    protected void btnLogout_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("login.aspx");
    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {
        FillGrid("0");
    }
}