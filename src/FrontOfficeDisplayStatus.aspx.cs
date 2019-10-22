using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrontOfficeDisplayStatus : System.Web.UI.Page
{
    private int i = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session[Session["TMLDealercode"] + "-TMLConString"] == null || Session[Session["TMLDealercode"] + "-TMLConString"].ToString() == "")
                Response.Redirect("login.aspx");

        }
        catch
        {
            Response.Redirect("login.aspx");
        }
        SqlDataSource1.ConnectionString = Session[Session["TMLDealercode"] + "-TMLConString"].ToString();
        lbl_currTime.Text = DateTime.Now.ToString("dd MMM yyyy, HH:mm:ss");
        i = gvDisplayStatus.PageIndex;
        if (!IsPostBack)
        {
            //getSA();
            fillSAList("udpSAlIST");
            fillGrid();
            try
            {
                lbl_LoginName.Text = Session["UserId"].ToString() + "&nbsp;&nbsp;";
            }
            catch
            {
                Response.Redirect("login.aspx");
            }

        }
        string disp = "";
        DataTable dispdt = new DataTable();
        SqlDataAdapter dispda = new SqlDataAdapter("Select * from tbl_DisplayStatus", new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString()));
        dispda.Fill(dispdt);
        if (dispdt.Rows.Count > 0)
        {
            disp = dispdt.Rows[0][0].ToString();
        }
        lblSyncTime.Text = DateTime.Now.ToShortTimeString();
       
           
        
    }

    //protected void getSA()
    //{
    //    SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
    //    SqlCommand cmd = new SqlCommand("GetServiceAdvisorList", con);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    SqlDataAdapter sda = new SqlDataAdapter(cmd);
    //    DataTable dt = new DataTable();
    //    sda.Fill(dt);
    //    ddlSA.DataTextField = "EmpName";
    //    ddlSA.DataValueField = "EmpId";
    //    ddlSA.DataSource = dt;
    //    ddlSA.DataBind();
    //}

    protected void ddlSA_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            fillGrid();
        }
        catch (Exception ex)
        {
        }
    }

    protected void fillGrid()
    {
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        DataTable dt = new DataTable();
        SqlDataAdapter sda = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand("GetSADisplay", con);
        cmd.CommandType = CommandType.StoredProcedure;
        if (ddlSA.SelectedItem.Text.Trim() == "ALL" || ddlSA.SelectedItem.Text.Trim()=="--Select--")
            cmd.Parameters.AddWithValue("@SAName", "");
        else
            cmd.Parameters.AddWithValue("@SAName", ddlSA.SelectedItem.Text.Trim());
        if (txtVehicleNo.Text.ToString() == "")
            cmd.Parameters.AddWithValue("@VehicleNo", "");
        else
            cmd.Parameters.AddWithValue("@VehicleNo", txtVehicleNo.Text.ToString());
        if (txtTagNo.Text.ToString() == "")
            cmd.Parameters.AddWithValue("@TagNo", "0");
        else
            cmd.Parameters.AddWithValue("@TagNo", txtTagNo.Text.ToString());
        cmd.Parameters.AddWithValue("@BodyShop", chkBodyShop.Checked);
        con.Open();
        sda = new SqlDataAdapter(cmd);
        sda.Fill(dt);
        gvDisplayStatus.DataSource = dt;
        gvDisplayStatus.DataBind();
        con.Close();
    }

    protected void gvDisplayStatus_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        if (!(chkBodyShop.Checked))
        {
            e.Row.Cells[0].Attributes.Add("style", "text-transform:uppercase;");

            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[1].Style.Value = "text-align:left;text-transform:uppercase;";
                e.Row.Cells[6].Style.Value = "text-align:center;text-transform:uppercase;";
                e.Row.Cells[7].Style.Value = "text-align:center;text-transform:uppercase;";
                e.Row.Cells[8].Style.Value = "text-align:center;text-transform:uppercase;";
                e.Row.Cells[9].Style.Value = "text-align:center;text-transform:uppercase;";
                e.Row.Cells[10].Style.Value = "text-align:center;text-transform:uppercase;";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[0].Text.Contains("#") == true)
                {
                    e.Row.Cells[0].Attributes.Add("style", "background-image:url('img/Q.png'); background-repeat: no-repeat;background-size:37px 37px;background-position:center;text-transform:uppercase;");
                    e.Row.Cells[0].Text = "";
                }


                e.Row.Cells[1].Attributes.Add("style", "text-align:left;text-transform:uppercase;");
                e.Row.Cells[4].Text = e.Row.Cells[4].Text.Replace("#", "<br/>");
                e.Row.Cells[5].Text = e.Row.Cells[5].Text.Replace("#", "<br/>");
                if (e.Row.Cells[10].Text.Contains("@") == true)
                {
                    e.Row.Cells[10].ForeColor = System.Drawing.Color.Red;
                    e.Row.Cells[10].Text = e.Row.Cells[10].Text.Replace("@", "");
                }
                e.Row.Cells[10].Text = e.Row.Cells[10].Text.Replace("#", "<br/>");
                if (e.Row.Cells[6].Text.Trim() == "In Progress")
                {
                   // e.Row.Cells[6].Attributes.Add("style", "background-image:url('img/Process/PK.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:center;background-position:center;");
                    e.Row.Cells[6].Attributes.Add("style", "background-image:url('https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/PK.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:center;background-position:center;");
                }
                else if (e.Row.Cells[6].Text.Trim() == "Not Started")
                {
                    //e.Row.Cells[6].Attributes.Add("style", "background-image:url('img/Process/X.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:center;background-position:center;");
                    e.Row.Cells[6].Attributes.Add("style", "background-image:url('https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/NotRequired.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:center;background-position:center;");
                }
                else if (e.Row.Cells[6].Text.Trim() == "Done" || e.Row.Cells[6].Text.Trim() == "Out Skipped")
                {
                   // e.Row.Cells[6].Attributes.Add("style", "background-image:url('img/Process/CN.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:center;background-position:center;");
                    e.Row.Cells[6].Attributes.Add("style", "background-image:url('https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:center;background-position:center;");
                }
                e.Row.Cells[6].Text = "";

                if (e.Row.Cells[7].Text.Trim() == "In Progress")
                {
                   // e.Row.Cells[7].Attributes.Add("style", "background-image:url('img/Process/PK.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:left;background-position:center;");
                    e.Row.Cells[7].Attributes.Add("style", "background-image:url('https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/PK.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:left;background-position:center;");
                }
                else if (e.Row.Cells[7].Text.Trim() == "Not Started")
                {
                   // e.Row.Cells[7].Attributes.Add("style", "background-image:url('img/Process/X.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:left;background-position:center;");
                    e.Row.Cells[7].Attributes.Add("style", "background-image:url('https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/NotRequired.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:left;background-position:center;");
                }
                else if (e.Row.Cells[7].Text.Trim() == "Done" || e.Row.Cells[7].Text.Trim() == "Out Skipped")
                {
                   // e.Row.Cells[7].Attributes.Add("style", "background-image:url('img/Process/CN.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:left;background-position:center;");
                    e.Row.Cells[7].Attributes.Add("style", "background-image:url('https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:left;background-position:center;");
                }
                e.Row.Cells[7].Text = "";

                if (e.Row.Cells[8].Text.Trim() == "In Progress")
                {
                   // e.Row.Cells[8].Attributes.Add("style", "background-image:url('img/Process/PK.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:center;background-position:center;");
                    e.Row.Cells[8].Attributes.Add("style", "background-image:url('https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/PK.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:center;background-position:center;");
                }
                else if (e.Row.Cells[8].Text.Trim() == "Not Started")
                {
                   // e.Row.Cells[8].Attributes.Add("style", "background-image:url('img/Process/X.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:left;background-position:center;");
                    e.Row.Cells[8].Attributes.Add("style", "background-image:url('https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/NotRequired.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:left;background-position:center;");
                }
                else if (e.Row.Cells[8].Text.Trim() == "Done" || e.Row.Cells[8].Text.Trim() == "Out Skipped")
                {
                   // e.Row.Cells[8].Attributes.Add("style", "background-image:url('img/Process/CN.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:left;background-position:center;");
                    e.Row.Cells[8].Attributes.Add("style", "background-image:url('https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:left;background-position:center;");
                }
                e.Row.Cells[8].Text = "";

                if (e.Row.Cells[9].Text.Trim() == "In Progress")
                {
                   // e.Row.Cells[9].Attributes.Add("style", "background-image:url('img/Process/PK.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:left;background-position:center;");
                    e.Row.Cells[9].Attributes.Add("style", "background-image:url('https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/PK.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:left;background-position:center;");
                }
                else if (e.Row.Cells[9].Text.Trim() == "Not Started")
                {
                  //  e.Row.Cells[9].Attributes.Add("style", "background-image:url('img/Process/X.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:left;background-position:center;");
                    e.Row.Cells[9].Attributes.Add("style", "background-image:url('https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/NotRequired.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:left;background-position:center;");
                }
                else if (e.Row.Cells[9].Text.Trim() == "Done" || e.Row.Cells[9].Text.Trim() == "Out Skipped")
                {
                  //  e.Row.Cells[9].Attributes.Add("style", "background-image:url('img/Process/CN.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:left;background-position:center;");
                    e.Row.Cells[9].Attributes.Add("style", "background-image:url('https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:left;background-position:center;");
                }
                e.Row.Cells[9].Text = "";

                if (e.Row.Cells[10].Text.Trim() == "In Progress")
                {
                   // e.Row.Cells[10].Attributes.Add("style", "background-image:url('img/Process/PK.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:left;background-position:center;");
                    e.Row.Cells[10].Attributes.Add("style", "background-image:url('https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/PK.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:left;background-position:center;");
                }
                else if (e.Row.Cells[10].Text.Trim() == "Not Started")
                {
                  //  e.Row.Cells[10].Attributes.Add("style", "background-image:url('img/Process/X.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:left;background-position:center;");
                    e.Row.Cells[10].Attributes.Add("style", "background-image:url('https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/NotRequired.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:left;background-position:center;");
                }
                else if (e.Row.Cells[10].Text.Trim() == "Done" || e.Row.Cells[10].Text.Trim() == "Out Skipped")
                {
                   // e.Row.Cells[10].Attributes.Add("style", "background-image:url('img/Process/CN.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:left;background-position:center;");
                    e.Row.Cells[10].Attributes.Add("style", "background-image:url('https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:left;background-position:center;");
                }
                e.Row.Cells[10].Text = "";
                e.Row.Cells[11].Text = e.Row.Cells[11].Text.Replace("#", "<br/>");
                if (e.Row.Cells[11].Text.Contains("@"))
                {
                    e.Row.Cells[11].ForeColor = System.Drawing.Color.Red;
                    e.Row.Cells[11].Text = e.Row.Cells[11].Text.Replace("@", "");
                }
                e.Row.Cells[12].Attributes.Add("style", "text-align:left;");
            }
        }
        else
        {
            e.Row.Cells[0].Attributes.Add("style", "text-transform:uppercase;");

            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[1].Style.Value = "text-align:left;text-transform:uppercase;";
                e.Row.Cells[6].Style.Value = "text-align:center;text-transform:uppercase;";
                e.Row.Cells[7].Style.Value = "text-align:center;text-transform:uppercase;";
                e.Row.Cells[8].Style.Value = "text-align:center;text-transform:uppercase;";
                e.Row.Cells[9].Style.Value = "text-align:center;text-transform:uppercase;";
                e.Row.Cells[10].Style.Value = "text-align:center;text-transform:uppercase;";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[0].Text.Contains("#") == true)
                {
                    e.Row.Cells[0].Attributes.Add("style", "background-image:url('img/Q.png'); background-repeat: no-repeat;background-size:37px 37px;background-position:center;text-transform:uppercase;");
                    e.Row.Cells[0].Text = "";
                }


                e.Row.Cells[1].Attributes.Add("style", "text-align:left;text-transform:uppercase;");
                e.Row.Cells[4].Text = e.Row.Cells[4].Text.Replace("#", "<br/>");
               // e.Row.Cells[5].Text = e.Row.Cells[5].Text.Replace("#", "<br/>");
                if (e.Row.Cells[10].Text.Contains("@") == true)
                {
                    e.Row.Cells[10].ForeColor = System.Drawing.Color.Red;
                    e.Row.Cells[10].Text = e.Row.Cells[10].Text.Replace("@", "");
                }
                e.Row.Cells[10].Text = e.Row.Cells[10].Text.Replace("#", "<br/>");
                if (e.Row.Cells[5].Text.Trim() == "In Progress")
                {
                    //e.Row.Cells[5].Attributes.Add("style", "background-image:url('img/Process/PK.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:center;background-position:center;");
                    e.Row.Cells[5].Attributes.Add("style", "background-image:url('https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/PK.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:center;background-position:center;");

                }
                else if (e.Row.Cells[5].Text.Trim() == "Not Started")
                {
                    //e.Row.Cells[5].Attributes.Add("style", "background-image:url('img/Process/X.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:center;background-position:center;");
                    e.Row.Cells[5].Attributes.Add("style", "background-image:url('https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/NotRequired.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:center;background-position:center;");
                }
                else if (e.Row.Cells[5].Text.Trim() == "Done" || e.Row.Cells[5].Text.Trim() == "Out Skipped")
                {
                    //e.Row.Cells[5].Attributes.Add("style", "background-image:url('img/Process/CN.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:center;background-position:center;");
                    e.Row.Cells[5].Attributes.Add("style", "background-image:url('https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:center;background-position:center;");
                }
                e.Row.Cells[5].Text = "";
                if (e.Row.Cells[6].Text.Trim() == "In Progress")
                {
                    //e.Row.Cells[6].Attributes.Add("style", "background-image:url('img/Process/PK.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:center;background-position:center;");
                    e.Row.Cells[6].Attributes.Add("style", "background-image:url('https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/PK.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:center;background-position:center;");
                }
                else if (e.Row.Cells[6].Text.Trim() == "Not Started")
                {
                    //e.Row.Cells[6].Attributes.Add("style", "background-image:url('img/Process/X.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:center;background-position:center;");
                    e.Row.Cells[6].Attributes.Add("style", "background-image:url('https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/NotRequired.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:center;background-position:center;");
                }
                else if (e.Row.Cells[6].Text.Trim() == "Done" || e.Row.Cells[6].Text.Trim() == "Out Skipped")
                {
                   // e.Row.Cells[6].Attributes.Add("style", "background-image:url('img/Process/CN.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:center;background-position:center;");
                    e.Row.Cells[6].Attributes.Add("style", "background-image:url('https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:center;background-position:center;");
                }
                e.Row.Cells[6].Text = "";

                if (e.Row.Cells[7].Text.Trim() == "In Progress")
                {
                    //e.Row.Cells[7].Attributes.Add("style", "background-image:url('img/Process/PK.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:left;background-position:center;");
                    e.Row.Cells[7].Attributes.Add("style", "background-image:url('https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/PK.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:left;background-position:center;");
                }
                else if (e.Row.Cells[7].Text.Trim() == "Not Started")
                {
                   // e.Row.Cells[7].Attributes.Add("style", "background-image:url('img/Process/X.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:left;background-position:center;");
                    e.Row.Cells[7].Attributes.Add("style", "background-image:url('https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/NotRequired.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:left;background-position:center;");
                }
                else if (e.Row.Cells[7].Text.Trim() == "Done" || e.Row.Cells[7].Text.Trim() == "Out Skipped")
                {
                   // e.Row.Cells[7].Attributes.Add("style", "background-image:url('img/Process/CN.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:left;background-position:center;");
                    e.Row.Cells[7].Attributes.Add("style", "background-image:url('https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:left;background-position:center;");
                }
                e.Row.Cells[7].Text = "";

                if (e.Row.Cells[8].Text.Trim() == "In Progress")
                {
                   // e.Row.Cells[8].Attributes.Add("style", "background-image:url('img/Process/PK.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:center;background-position:center;");
                    e.Row.Cells[8].Attributes.Add("style", "background-image:url('https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/PK.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:center;background-position:center;");
                }
                else if (e.Row.Cells[8].Text.Trim() == "Not Started")
                {
                  //  e.Row.Cells[8].Attributes.Add("style", "background-image:url('img/Process/X.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:left;background-position:center;");
                    e.Row.Cells[8].Attributes.Add("style", "background-image:url('https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/NotRequired.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:left;background-position:center;");
                }
                else if (e.Row.Cells[8].Text.Trim() == "Done" || e.Row.Cells[8].Text.Trim() == "Out Skipped")
                {
                   // e.Row.Cells[8].Attributes.Add("style", "background-image:url('img/Process/CN.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:left;background-position:center;");
                    e.Row.Cells[8].Attributes.Add("style", "background-image:url('https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:left;background-position:center;");
                }
                e.Row.Cells[8].Text = "";

                if (e.Row.Cells[9].Text.Trim() == "In Progress")
                {
                    //e.Row.Cells[9].Attributes.Add("style", "background-image:url('img/Process/PK.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:left;background-position:center;");
                    e.Row.Cells[9].Attributes.Add("style", "background-image:url('https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/PK.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:left;background-position:center;");
                }
                else if (e.Row.Cells[9].Text.Trim() == "Not Started")
                {
                    //e.Row.Cells[9].Attributes.Add("style", "background-image:url('img/Process/X.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:left;background-position:center;");
                    e.Row.Cells[9].Attributes.Add("style", "background-image:url('https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/NotRequired.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:left;background-position:center;");
                }
                else if (e.Row.Cells[9].Text.Trim() == "Done" || e.Row.Cells[9].Text.Trim() == "Out Skipped")
                {
                   // e.Row.Cells[9].Attributes.Add("style", "background-image:url('img/Process/CN.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:left;background-position:center;");
                    e.Row.Cells[9].Attributes.Add("style", "background-image:url('https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:left;background-position:center;");
                }
                e.Row.Cells[9].Text = "";

                if (e.Row.Cells[10].Text.Trim() == "In Progress")
                {
                   // e.Row.Cells[10].Attributes.Add("style", "background-image:url('img/Process/PK.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:left;background-position:center;");
                    e.Row.Cells[10].Attributes.Add("style", "background-image:url('https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/PK.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:left;background-position:center;");
                }
                else if (e.Row.Cells[10].Text.Trim() == "Not Started")
                {
                   // e.Row.Cells[10].Attributes.Add("style", "background-image:url('img/Process/X.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:left;background-position:center;");
                    e.Row.Cells[10].Attributes.Add("style", "background-image:url('https://res.cloudinary.com/deekyp5bi/image/upload/v1484205144/jcr/NotRequired.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:left;background-position:center;");
                }
                else if (e.Row.Cells[10].Text.Trim() == "Done" || e.Row.Cells[10].Text.Trim() == "Out Skipped")
                {
                  //  e.Row.Cells[10].Attributes.Add("style", "background-image:url('img/Process/CN.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:left;background-position:center;");
                    e.Row.Cells[10].Attributes.Add("style", "background-image:url('https://res.cloudinary.com/deekyp5bi/image/upload/v1484205140/jcr/CN.png'); background-repeat: no-repeat;background-size:16px 16px;text-align:left;background-position:center;");
                }
                e.Row.Cells[10].Text = "";
                e.Row.Cells[11].Text = e.Row.Cells[11].Text.Replace("#", "<br/>");
                if (e.Row.Cells[11].Text.Contains("@"))
                {
                    e.Row.Cells[11].ForeColor = System.Drawing.Color.Red;
                    e.Row.Cells[11].Text = e.Row.Cells[11].Text.Replace("@", "");
                }
                e.Row.Cells[12].Attributes.Add("style", "text-align:left;");
            }
        }
    }

    protected void gvDisplayStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDisplayStatus.PageIndex = e.NewPageIndex;
        fillGrid();
    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {
        try
        {
            i = gvDisplayStatus.PageIndex;
            if (gvDisplayStatus.PageIndex == gvDisplayStatus.PageCount - 1)
            {
                gvDisplayStatus.PageIndex = 0;
                fillGrid();
            }
            else
            {
                gvDisplayStatus.PageIndex += 1;
                fillGrid();
            }
        }
        catch
        {
        }
        lblSyncTime.Text = DateTime.Now.ToShortTimeString();
    }

    protected void Timer2_Tick(object sender, EventArgs e)
    {
        try
        {
            Timer1.Enabled = true;
            Timer2.Enabled = false;
            fillGrid();
        }
        catch (Exception ex)
        {
        }
    }

    protected void ddlTeamlead_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
    }

    protected void ddlFloorName_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
    }

    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("TagAllotment.aspx");
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        fillGrid();
    }

    protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
    {
        txtTagNo.Text = "";
        txtVehicleNo.Text = "";
        chkBodyShop.Checked = false;
        fillSAList("udpSAlIST");
        
        fillGrid();
    }
    protected void btn_logout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();
        Response.Redirect("login.aspx");
    }

   

    protected void back_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("TagAllotment.aspx");
    }



    protected void chkBodyShop_CheckedChanged(object sender, EventArgs e)
    {
        if(chkBodyShop.Checked)
            fillSAList("udpBodyShopSAList");
        else
            fillSAList("udpSAlIST");
        fillGrid();
    }
    protected void fillSAList(string Procedure)
    {
        SqlConnection con = new SqlConnection(Session[Session["TMLDealercode"] + "-TMLConString"].ToString());
        SqlCommand cmd = new SqlCommand(Procedure, con);

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = Procedure;
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        ddlSA.Items.Clear();
        ddlSA.Items.Add(new ListItem("--Select--", "0"));
        if (dt.Rows.Count != 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ddlSA.Items.Add(new ListItem(dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString()));
            }
        }
    }
}