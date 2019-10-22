using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class NewPositionDisplay : System.Web.UI.Page
{
    private static string BackTo = "";
    public DataTable ModelDataTable;
    private static int SAID;
    private DataTable DTSA = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblSyncTime.Text = DateTime.Now.ToString("HH:mm:ss");
            ModelDataTable = GetModelImages();
            SetVehicleCount();
            if (!Page.IsPostBack)
            {
                DTSA = GetServiceAdvisorName();
                SAID = DTSA.Rows.Count;
                tmrGrid_Tick(null, null);
                if (Page.Request.QueryString["Back"] != null)
                {
                    btnBACK.Visible = true;
                    BackTo = Session["Role"].ToString();
                }
                else
                {
                    btnBACK.Visible = false;
                }
            }
        }
        catch (Exception ex)
        { }
    }

    public DataTable GetModelImages()
    {
        DataTable Dt = new DataTable();
        SqlConnection con = new SqlConnection(DataManager.ConStr);
        SqlCommand cmd = new SqlCommand("Select * FROM tblVehicleModel", con);
        try
        {
            SqlDataAdapter Da = new SqlDataAdapter(cmd);
            Da.Fill(Dt);
            return Dt;
        }
        catch (Exception ex)
        {
            return null;
        }
        finally
        {
            con.Close();
        }
    }

    public DataTable GetServiceAdvisorName()
    {
        DataTable Dt = new DataTable();
        SqlConnection con = new SqlConnection(DataManager.ConStr);
        SqlCommand cmd = new SqlCommand("udpGetSAName", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            SqlDataAdapter Da = new SqlDataAdapter(cmd);
            Da.Fill(Dt);
            return Dt;
        }
        catch (Exception ex)
        {
            return null;
        }
        finally
        {
            con.Close();
        }
    }

    private void SetBackImage(ListViewItemEventArgs e)
    {
        string ModelName = string.Empty;
        string ImageName = string.Empty;
        string InTime = string.Empty;
        try
        {
            HiddenField modelImage = new HiddenField();
            modelImage = (HiddenField)e.Item.FindControl("hfModel");
            Label intim = (Label)e.Item.FindControl("InTimeLabel");
            Label Posi = (Label)e.Item.FindControl("lblPosition");
            Label regno = (Label)e.Item.FindControl("RegnoLabel");
            Image ModImg = (Image)e.Item.FindControl("ModelImg");
            ModelName = modelImage.Value;
            if (ModelDataTable.Select("Model ='" + ModelName + "'").Length > 0)
            {
                DataRow[] foundRows = ModelDataTable.Select("Model ='" + ModelName + "'");
                DataRow Dr = foundRows[0];
                ImageName = Dr["ModelImageUrl"].ToString();
            }
            else
            {
                ImageName = "car.png";
            }
            ModImg.ImageUrl = "Images/CarImages/" + ImageName;
            if (regno.Text.Trim() != "")
            {
                int regLen = regno.Text.Trim().Length;
                regno.Text = "<font size='1'>" + ((ModelName.Length > 10) ? ModelName.Substring(0, 10) : ModelName) + "</font>" + "<br/>" + ((regLen > 4) ? regno.Text.Substring(regLen - 4, 4) : regno.Text);
            }
            else
            {
                regno.Text = "<font size='1'>" + ((ModelName.Length > 10) ? ModelName.Substring(0, 10) : ModelName) + "</font>" + "<br>";
            }
            HtmlGenericControl div2 = (HtmlGenericControl)e.Item.FindControl("BackDiv");
            string bgcolor = "#c3d9ff";
            bgcolor = GetPositionColor(Posi.Text.Trim());
            if (intim.Text.Contains('#'))
            {
                div2.Attributes.Add("Style", "background-color:" + bgcolor + ";border:rgba(0,0,0,0.5) 1px solid;box-shadow:1px 2px #000;");
                intim.Text = intim.Text.Substring(0, intim.Text.IndexOf("#"));
            }
            else
            {
                div2.Attributes.Add("Style", "background-color:" + bgcolor + ";border:rgba(0,0,0,0.5) 1px solid;box-shadow:1px 2px #000;");
            }
        }
        catch (Exception ex)
        {
        }
    }

    private void SetIdleBackImage(ListViewItemEventArgs e)
    {
        string ModelName = string.Empty;
        string ImageName = string.Empty;
        string InTime = string.Empty;
        try
        {
            HiddenField modelImage = new HiddenField();
            modelImage = (HiddenField)e.Item.FindControl("hfModel");
            Label intim = (Label)e.Item.FindControl("InTimeLabel");
            Label Posi = (Label)e.Item.FindControl("lblPosition");
            Image ModImg = (Image)e.Item.FindControl("ModelImg");
            Label regno = (Label)e.Item.FindControl("RegnoLabel");
            ModelName = modelImage.Value;
            if (ModelDataTable.Select("Model ='" + ModelName + "'").Length > 0)
            {
                DataRow[] foundRows = ModelDataTable.Select("Model ='" + ModelName + "'");
                DataRow Dr = foundRows[0];
                ImageName = Dr["ModelImageUrl"].ToString();
            }
            else
            {
                ImageName = "car.png";
            }
            ModImg.ImageUrl = "Images/CarImages/" + ImageName;
            if (regno.Text.Trim() != "")
            {
                int regLen = regno.Text.Trim().Length;
                regno.Text = "<font size='1'>" + ((ModelName.Length > 10) ? ModelName.Substring(0, 10) : ModelName) + "</font>" + "<br/>" + ((regLen > 4) ? regno.Text.Substring(regLen - 4, 4) : regno.Text);
            }
            else
            {
                regno.Text = "<font size='1'>" + ((ModelName.Length > 10) ? ModelName.Substring(0, 10) : ModelName) + "</font>" + "<br>";
            }
            HtmlGenericControl div2 = (HtmlGenericControl)e.Item.FindControl("BackDiv");
            string bgcolor = "#c3d9ff";
            bgcolor = GetPositionColor(Posi.Text.Trim());
            if (intim.Text.Contains('#'))
            {
                div2.Attributes.Add("Style", "background-color:" + bgcolor + ";border:rgba(0,0,0,0.5) 1px solid;box-shadow:1px 2px #000;");
                intim.Text = intim.Text.Substring(0, intim.Text.IndexOf("#"));
            }
            else
            {
                div2.Attributes.Add("Style", "background-color:" + bgcolor + ";border:rgba(0,0,0,0.5) 1px solid;box-shadow:1px 2px #000;");
            }
        }
        catch (Exception ex)
        {
        }
    }

    private void SetIdleBackImage1(ListViewItemEventArgs e)
    {
        string ModelName = string.Empty;
        string ImageName = string.Empty;
        string InTime = string.Empty;
        try
        {
            HiddenField modelImage = new HiddenField();
            modelImage = (HiddenField)e.Item.FindControl("hfModel1");
            Label intim = (Label)e.Item.FindControl("InTimeLabel");
            Label Posi = (Label)e.Item.FindControl("lblPosition");
            Image ModImg = (Image)e.Item.FindControl("ModelImg");
            Label regno = (Label)e.Item.FindControl("RegnoLabel");
            ModelName = modelImage.Value;
            if (ModelDataTable.Select("Model ='" + ModelName + "'").Length > 0)
            {
                DataRow[] foundRows = ModelDataTable.Select("Model ='" + ModelName + "'");
                DataRow Dr = foundRows[0];
                ImageName = Dr["ModelImageUrl"].ToString();
            }
            else
            {
                ImageName = "car.png";
            }
            ModImg.ImageUrl = "Images/CarImages/" + ImageName;
            if (regno.Text.Trim() != "")
            {
                int regLen = regno.Text.Trim().Length;
                regno.Text = "<font size='1'>" + ((ModelName.Length > 10) ? ModelName.Substring(0, 10) : ModelName) + "</font>" + "<br/>" + ((regLen > 4) ? regno.Text.Substring(regLen - 4, 4) : regno.Text);
            }
            else
            {
                regno.Text = "<font size='1'>" + ((ModelName.Length > 10) ? ModelName.Substring(0, 10) : ModelName) + "</font>" + "<br>";
            }
            HtmlGenericControl div2 = (HtmlGenericControl)e.Item.FindControl("BackDiv");
            string bgcolor = "#c3d9ff";
            bgcolor = GetPositionColor(Posi.Text.Trim());
            if (intim.Text.Contains('#'))
            {
                div2.Attributes.Add("Style", "background-color:" + bgcolor + ";border:rgba(0,0,0,0.5) 1px solid;box-shadow:1px 2px #000;");
                intim.Text = intim.Text.Substring(0, intim.Text.IndexOf("#"));
            }
            else
            {
                div2.Attributes.Add("Style", "background-color:" + bgcolor + ";border:rgba(0,0,0,0.5) 1px solid;box-shadow:1px 2px #000;");
            }
        }
        catch (Exception ex)
        {
        }
    }

    public string GetPositionColor(string Position)
    {
        string col = "";
        switch (Position)
        {
            case "Gate":
                col = "#fc9292";
                break;

            case "JobSlip":
                col = "#6496FF";
                break;

            case "Wash":
                col = "#FFFA73";
                break;

            case "Workshop":
                col = "Orange";
                break;

            case "WA":
                col = "#6600cc";
                break;

            case "Wheel Alignment":
                col = "#B18CFF";
                break;

            case "VAS":
                col = "#B8B872";
                break;

            case "Final Inspection":
                col = "White";
                break;

            case "RT":
                col = "#20B7FF";
                break;

            case "Road Test":
                col = "#20B7FF";
                break;

            case "Vehicle Ready":
                col = "#A7CC95";
                break;

            case "Vehicle Idle":
                col = "#FFFA73";
                break;
        }
        return col;
    }

    protected void tmrGrid_Tick(object sender, EventArgs e)
    {
        try
        {
            lblSyncTime.Text = DateTime.Now.ToString("HH:mm:ss");
            DTSA = GetServiceAdvisorName();
            if (SAID == DTSA.Rows.Count)
            {
                SAID = 0;
                lblSAName.Text = DTSA.Rows[SAID][1].ToString();
            }
            else
            {
                SAID += 1;
                lblSAName.Text = DTSA.Rows[SAID][1].ToString();
            }
            lstWaiting.DataBind();
            lstJobSlip.DataBind();
            lstFloor.DataBind();
            lstWash.DataBind();
            lstQC.DataBind();
            lstWA.DataBind();
            lstRT.DataBind();
            lstCarReady.DataBind();
            lstIdle1.DataBind();
            lstIdle2.DataBind();
            lstIdle3.DataBind();
            lstIdle4.DataBind();
            SetVehicleCount();
        }
        catch (Exception ex)
        {
        }
    }

    protected void SetVehicleCount()
    {
        lblGateCount.Text = lstWaiting.Items.Count.ToString();
        lblJobSlipCount.Text = lstJobSlip.Items.Count.ToString();
        lblWorkshopCount.Text = lstFloor.Items.Count.ToString();
        lblWashCount.Text = lstWash.Items.Count.ToString();
        lblFICount.Text = lstQC.Items.Count.ToString();
        lblWACount.Text = lstWA.Items.Count.ToString();
        lblRTCount.Text = lstRT.Items.Count.ToString();
        lblVRCount.Text = lstCarReady.Items.Count.ToString();
        lblVIdleCount.Text = (int.Parse(lstIdle1.Items.Count.ToString()) + int.Parse(lstIdle2.Items.Count.ToString()) + int.Parse(lstIdle3.Items.Count.ToString()) + int.Parse(lstIdle4.Items.Count.ToString())).ToString();
        lbWGate.Text = lstWaiting.Items.Count.ToString();
        lbRO.Text = lstJobSlip.Items.Count.ToString();
        lbWash.Text = lstWash.Items.Count.ToString();
        lbWorkshop.Text = lstFloor.Items.Count.ToString();
        lbWA.Text = lstWA.Items.Count.ToString();
        lbFI.Text = lstQC.Items.Count.ToString();
        lbRT.Text = lstRT.Items.Count.ToString();
        lbVR.Text = lstCarReady.Items.Count.ToString();
        lbVI.Text = (int.Parse(lstIdle1.Items.Count.ToString()) + int.Parse(lstIdle2.Items.Count.ToString()) + int.Parse(lstIdle3.Items.Count.ToString()) + int.Parse(lstIdle4.Items.Count.ToString())).ToString();
        lbIdle1.Text = lstIdle1.Items.Count.ToString();
        lbIdle2.Text = lstIdle2.Items.Count.ToString();
        lbIdle3.Text = lstIdle3.Items.Count.ToString();
        lbIdle4.Text = lstIdle4.Items.Count.ToString();
        lbTotal.Text = (int.Parse(lbWGate.Text) + int.Parse(lbRO.Text) + int.Parse(lbWash.Text) + int.Parse(lbWorkshop.Text) + int.Parse(lbWA.Text) + int.Parse(lbFI.Text) + int.Parse(lbRT.Text) + int.Parse(lbVR.Text) + int.Parse(lbVI.Text)).ToString();
    }

    protected void tmrRefresh_Tick(object sender, EventArgs e)
    {
        lstWaiting.DataBind();
        lstJobSlip.DataBind();
    }

    protected void lstWaiting_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        SetBackImage(e);
        Label lbl = (Label)e.Item.FindControl("lblRefNo");
        HtmlGenericControl div2 = (HtmlGenericControl)e.Item.FindControl("BackDiv");
        div2.Attributes.Add("onmouseover", "ShowLoadProcessInOutTime(event,'" + lbl.Text + "','Gate')");
        div2.Attributes.Add("onmouseout", "hideTooltip(event)");
        Label lblTim = (Label)e.Item.FindControl("InTimeLabel");
        Label lblReg = (Label)e.Item.FindControl("RegnoLabel");
        Label lblDv = (Label)e.Item.FindControl("lblDev");
        if (lblDv.Text.Trim() != "0")
        {
            lblReg.Attributes.Add("style", "color:red;");
            lblTim.Attributes.Add("style", "color:red;");
        }
    }

    protected void lstJobSlip_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        SetBackImage(e);
        Label lbl = (Label)e.Item.FindControl("lblRefNo");
        HtmlGenericControl hoverDiv = (HtmlGenericControl)e.Item.FindControl("BackDiv");
        hoverDiv.Attributes.Add("onmouseover", "ShowLoadProcessInOutTime(event,'" + lbl.Text + "','JobSlip')");
        hoverDiv.Attributes.Add("onmouseout", "hideTooltip(event)");
        Label lblTim = (Label)e.Item.FindControl("InTimeLabel");
        Label lblReg = (Label)e.Item.FindControl("RegnoLabel");
        Label lblDv = (Label)e.Item.FindControl("lblDev");
        if (lblDv.Text.Trim() != "0")
        {
            lblReg.Attributes.Add("style", "color:red;");
            lblTim.Attributes.Add("style", "color:red;");
        }
    }

    protected void lstWash_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        SetBackImage(e);
        Label lbl = (Label)e.Item.FindControl("lblRefNo");
        HtmlGenericControl hoverDiv = (HtmlGenericControl)e.Item.FindControl("BackDiv");
        hoverDiv.Attributes.Add("onmouseover", "ShowLoadProcessInOutTime(event,'" + lbl.Text + "','Wash')");
        hoverDiv.Attributes.Add("onmouseout", "hideTooltip(event)");
        Label lblTim = (Label)e.Item.FindControl("InTimeLabel");
        Label lblReg = (Label)e.Item.FindControl("RegnoLabel");
        Label lblDv = (Label)e.Item.FindControl("lblDev");
        if (lblDv.Text.Trim() != "0")
        {
            lblReg.Attributes.Add("style", "color:red;");
            lblTim.Attributes.Add("style", "color:red;");
        }
    }

    protected void lstCarReady_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        SetBackImage(e);
        Label lbl = (Label)e.Item.FindControl("lblRefNo");
        HtmlGenericControl hoverDiv = (HtmlGenericControl)e.Item.FindControl("BackDiv");
        hoverDiv.Attributes.Add("onmouseover", "ShowLoadProcessInOutTime(event,'" + lbl.Text + "','Vehicle Ready')");
        hoverDiv.Attributes.Add("onmouseout", "hideTooltip(event)");
        Label lblTim = (Label)e.Item.FindControl("InTimeLabel");
        Label lblReg = (Label)e.Item.FindControl("RegnoLabel");
        Label lblDv = (Label)e.Item.FindControl("lblDev");
        if (lblDv.Text.Trim() != "0")
        {
            lblReg.Attributes.Add("style", "color:red;");
            lblTim.Attributes.Add("style", "color:red;");
        }
    }

    protected void lstFloor_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        SetBackImage(e);
        Label lbl = (Label)e.Item.FindControl("lblRefNo");
        HtmlGenericControl hoverDiv = (HtmlGenericControl)e.Item.FindControl("BackDiv");
        hoverDiv.Attributes.Add("onmouseover", "ShowWorkshopLoadProcessInOutTime(event,'" + lbl.Text + "','WorkShop')");
        hoverDiv.Attributes.Add("onmouseout", "hideTooltip(event)");
        Label lblTim = (Label)e.Item.FindControl("InTimeLabel");
        Label lblReg = (Label)e.Item.FindControl("RegnoLabel");
        Label lblDv = (Label)e.Item.FindControl("lblDev");
        if (lblDv.Text.Trim() != "0")
        {
            lblReg.Attributes.Add("style", "color:red;");
            lblTim.Attributes.Add("style", "color:red;");
        }
    }

    protected void lstQC_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        SetBackImage(e);
        Label lbl = (Label)e.Item.FindControl("lblRefNo");
        HtmlGenericControl hoverDiv = (HtmlGenericControl)e.Item.FindControl("BackDiv");
        hoverDiv.Attributes.Add("onmouseover", "ShowLoadProcessInOutTime(event,'" + lbl.Text + "','Final Inspection')");
        hoverDiv.Attributes.Add("onmouseout", "hideTooltip(event)");
        lblFICount.Text = (e.Item.Controls.Count > 0 ? (e.Item.Controls.Count / 3) : 0).ToString();
        Label lblTim = (Label)e.Item.FindControl("InTimeLabel");
        Label lblReg = (Label)e.Item.FindControl("RegnoLabel");
        Label lblDv = (Label)e.Item.FindControl("lblDev");
        if (lblDv.Text.Trim() != "0")
        {
            lblReg.Attributes.Add("style", "color:red;");
            lblTim.Attributes.Add("style", "color:red;");
        }
    }

    protected void lstIdle_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        SetIdleBackImage(e);
        Label lbl = (Label)e.Item.FindControl("lblRefNo");
        HtmlGenericControl hoverDiv = (HtmlGenericControl)e.Item.FindControl("BackDiv");
        hoverDiv.Attributes.Add("onmouseover", "ShowLoadIdleInOutTime(event,'" + lbl.Text + "')");
        hoverDiv.Attributes.Add("onmouseout", "hideTooltip(event)");
        Label lblTim = (Label)e.Item.FindControl("InTimeLabel");
        Label lblReg = (Label)e.Item.FindControl("RegnoLabel");
        Label lblDv = (Label)e.Item.FindControl("lblDev");
        if (lblDv.Text.Trim() != "0")
        {
            lblReg.Attributes.Add("style", "color:red;");
            lblTim.Attributes.Add("style", "color:red;");
        }
    }

    protected void lstIdle1_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        SetIdleBackImage1(e);
        Label lbl = (Label)e.Item.FindControl("lblRefNo");
        HtmlGenericControl hoverDiv = (HtmlGenericControl)e.Item.FindControl("BackDiv");
        hoverDiv.Attributes.Add("onmouseover", "ShowLoadIdleInOutTime(event,'" + lbl.Text + "')");
        hoverDiv.Attributes.Add("onmouseout", "hideTooltip(event)");
        Label lblTim = (Label)e.Item.FindControl("InTimeLabel");
        Label lblReg = (Label)e.Item.FindControl("RegnoLabel");
        Label lblDv = (Label)e.Item.FindControl("lblDev");
        if (lblDv.Text.Trim() != "0")
        {
            lblReg.Attributes.Add("style", "color:red;");
            lblTim.Attributes.Add("style", "color:red;");
        }
    }

    protected void lstIdle2_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        SetIdleBackImage1(e);
        Label lbl = (Label)e.Item.FindControl("lblRefNo");
        HtmlGenericControl hoverDiv = (HtmlGenericControl)e.Item.FindControl("BackDiv");
        hoverDiv.Attributes.Add("onmouseover", "ShowLoadIdleInOutTime(event,'" + lbl.Text + "')");
        hoverDiv.Attributes.Add("onmouseout", "hideTooltip(event)");
        Label lblTim = (Label)e.Item.FindControl("InTimeLabel");
        Label lblReg = (Label)e.Item.FindControl("RegnoLabel");
        Label lblDv = (Label)e.Item.FindControl("lblDev");
        if (lblDv.Text.Trim() != "0")
        {
            lblReg.Attributes.Add("style", "color:red;");
            lblTim.Attributes.Add("style", "color:red;");
        }
    }

    protected void lstIdle3_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        SetIdleBackImage1(e);
        Label lbl = (Label)e.Item.FindControl("lblRefNo");
        HtmlGenericControl hoverDiv = (HtmlGenericControl)e.Item.FindControl("BackDiv");
        hoverDiv.Attributes.Add("onmouseover", "ShowLoadIdleInOutTime(event,'" + lbl.Text + "')");
        hoverDiv.Attributes.Add("onmouseout", "hideTooltip(event)");
        Label lblTim = (Label)e.Item.FindControl("InTimeLabel");
        Label lblReg = (Label)e.Item.FindControl("RegnoLabel");
        Label lblDv = (Label)e.Item.FindControl("lblDev");
        if (lblDv.Text.Trim() != "0")
        {
            lblReg.Attributes.Add("style", "color:red;");
            lblTim.Attributes.Add("style", "color:red;");
        }
    }

    protected void lstIdle4_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        SetIdleBackImage1(e);
        Label lbl = (Label)e.Item.FindControl("lblRefNo");
        HtmlGenericControl hoverDiv = (HtmlGenericControl)e.Item.FindControl("BackDiv");
        hoverDiv.Attributes.Add("onmouseover", "ShowLoadIdleInOutTime(event,'" + lbl.Text + "')");
        hoverDiv.Attributes.Add("onmouseout", "hideTooltip(event)");
        Label lblTim = (Label)e.Item.FindControl("InTimeLabel");
        Label lblReg = (Label)e.Item.FindControl("RegnoLabel");
        Label lblDv = (Label)e.Item.FindControl("lblDev");
        if (lblDv.Text.Trim() != "0")
        {
            lblReg.Attributes.Add("style", "color:red;");
            lblTim.Attributes.Add("style", "color:red;");
        }
    }

    protected void lstWA_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        SetBackImage(e);
        Label lbl = (Label)e.Item.FindControl("lblRefNo");
        HtmlGenericControl hoverDiv = (HtmlGenericControl)e.Item.FindControl("BackDiv");
        hoverDiv.Attributes.Add("onmouseover", "ShowLoadProcessInOutTime(event,'" + lbl.Text + "','WA')");
        hoverDiv.Attributes.Add("onmouseout", "hideTooltip(event)");
        Label lblTim = (Label)e.Item.FindControl("InTimeLabel");
        Label lblReg = (Label)e.Item.FindControl("RegnoLabel");
        Label lblDv = (Label)e.Item.FindControl("lblDev");
        if (lblDv.Text.Trim() != "0")
        {
            lblReg.Attributes.Add("style", "color:red;");
            lblTim.Attributes.Add("style", "color:red;");
        }
    }

    protected void lstVAS_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        SetBackImage(e);
        Label lbl = (Label)e.Item.FindControl("lblRefNo");
        HtmlGenericControl hoverDiv = (HtmlGenericControl)e.Item.FindControl("BackDiv");
        hoverDiv.Attributes.Add("onmouseover", "ShowLoadProcessInOutTime(event,'" + lbl.Text + "','VAS')");
        hoverDiv.Attributes.Add("onmouseout", "hideTooltip(event)");
        Label lblTim = (Label)e.Item.FindControl("InTimeLabel");
        Label lblReg = (Label)e.Item.FindControl("RegnoLabel");
        Label lblDv = (Label)e.Item.FindControl("lblDev");
        if (lblDv.Text.Trim() != "0")
        {
            lblReg.Attributes.Add("style", "color:red;");
            lblTim.Attributes.Add("style", "color:red;");
        }
    }

    protected void lstRT_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        SetBackImage(e);
        Label lbl = (Label)e.Item.FindControl("lblRefNo");
        HtmlGenericControl hoverDiv = (HtmlGenericControl)e.Item.FindControl("BackDiv");
        hoverDiv.Attributes.Add("onmouseover", "ShowLoadProcessInOutTime(event,'" + lbl.Text + "','RT')");
        hoverDiv.Attributes.Add("onmouseout", "hideTooltip(event)");
        Label lblTim = (Label)e.Item.FindControl("InTimeLabel");
        Label lblReg = (Label)e.Item.FindControl("RegnoLabel");
        Label lblDv = (Label)e.Item.FindControl("lblDev");
        if (lblDv.Text.Trim() != "0")
        {
            lblReg.Attributes.Add("style", "color:red;");
            lblTim.Attributes.Add("style", "color:red;");
        }
    }

    public static DataTable GetInOutTime(string RefNo)
    {
        DataTable dt = new DataTable();
        SqlConnection con = new SqlConnection(DataManager.ConStr);
        SqlCommand cmd = new SqlCommand("GetPositionIdleHover", con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@RefNo", RefNo);
        if (con.State != ConnectionState.Open)
            con.Open();
        sda.Fill(dt);
        if (con.State != ConnectionState.Closed)
            con.Close();
        return dt;
    }

    public static DataTable GetInOutTime(string RefNo, string ProcessName)
    {
        DataTable dt = new DataTable();
        SqlConnection con = new SqlConnection(DataManager.ConStr);
        SqlCommand cmd = new SqlCommand("GetPositionProcessHover", con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@RefNo", RefNo);
        cmd.Parameters.AddWithValue("@Position", ProcessName);
        if (con.State != ConnectionState.Open)
            con.Open();
        sda.Fill(dt);
        if (con.State != ConnectionState.Closed)
            con.Close();
        return dt;
    }

    [System.Web.Services.WebMethod(EnableSession = true)]
    public static string LoadProcessInOutTime(string RefNo, string ProcessName)
    {
        DataTable dt = new DataTable();
        dt = GetInOutTime(RefNo, ProcessName);
        string str = "";
        if (dt.Rows.Count > 0)
        {
            if (ProcessName == "Vehicle Ready")
            {
                str = "<span><table style=font-style:arial;><tr style=background-color:#c3d9ff;text-align:center;><th style=width:100px;>Employee Type</th><th style=width:100px;>Employee</th><th style=width:100px;>PDT</th><th style=width:100px;>Ready Time</th></tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    str += "<tr><td>" + dt.Rows[i]["EmployeeType"].ToString() + "</td><td>" + dt.Rows[i]["EmpName"].ToString() + "</td><td>" + dt.Rows[i]["InTime"].ToString().Replace("#", " ") + "</td><td>" + dt.Rows[i]["OutTime"].ToString().Replace("#", " ") + "</td></tr>";
                }
                str += "</table></span>";
            }
            else
            {
                str = "<span><table style=font-style:arial;><tr style=background-color:#c3d9ff;text-align:center;><th style=width:100px;>Employee Type</th><th style=width:100px;>Employee</th><th style=width:100px;>In Time</th><th style=width:100px;>Out Time</th></tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    str += "<tr><td>" + dt.Rows[i]["EmployeeType"].ToString() + "</td><td>" + dt.Rows[i]["EmpName"].ToString() + "</td><td>" + dt.Rows[i]["InTime"].ToString().Replace("#", " ") + "</td><td>" + dt.Rows[i]["OutTime"].ToString().Replace("#", " ") + "</td></tr>";
                }
                str += "</table></span>";
            }
        }
        else
        {
            str = "<table style=width:100px;text-align:center;><tr><th>No Details</th></tr></table>";
        }
        return str;
    }

    [System.Web.Services.WebMethod(EnableSession = true)]
    public static string LoadWorkshopProcessInOutTime(string RefNo, string ProcessName)
    {
        DataTable dt = new DataTable();
        dt = GetInOutTime(RefNo, ProcessName);
        string str = "";
        if (dt.Rows.Count > 0)
        {
            if (ProcessName == "Vehicle Ready")
            {
                str = "<span><table style=font-style:arial;><tr style=background-color:#c3d9ff;text-align:center;><th style=width:100px;>Employee Type</th><th style=width:100px;>Employee</th><th style=width:100px;>PDT</th><th style=width:100px;>Ready Time</th></tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    str += "<tr><td>" + dt.Rows[i]["EmployeeType"].ToString() + "</td><td>" + dt.Rows[i]["EmpName"].ToString() + "</td><td>" + dt.Rows[i]["InTime"].ToString().Replace("#", " ") + "</td><td>" + dt.Rows[i]["OutTime"].ToString().Replace("#", " ") + "</td></tr>";
                }
                str += "</table></span>";
            }
            else if (ProcessName == "WorkShop")
            {
                str = "<span><table style=font-style:arial;><tr style=background-color:#c3d9ff;text-align:center;><th style=width:100px;>Employee Type</th><th style=width:100px;>Employee</th><th style=width:100px;>In Time</th><th style=width:100px;>Out Time</th></tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    str += "<tr><td>" + dt.Rows[i]["EmployeeType"].ToString() + "</td><td>" + dt.Rows[i]["EmpName"].ToString() + "</td><td>" + dt.Rows[i]["InTime"].ToString().Replace("#", " ") + "</td><td>" + dt.Rows[i]["OutTime"].ToString().Replace("#", " ") + "</td></tr>";
                }
                str += "</table></span>";
            }
            else
            {
                str = "<span><table style=font-style:arial;><tr style=background-color:#c3d9ff;text-align:center;><th style=width:100px;>Employee Type</th><th style=width:100px;>Employee</th><th style=width:100px;>In Time</th><th style=width:100px;>Out Time</th></tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    str += "<tr><td>" + dt.Rows[i]["EmployeeType"].ToString() + "</td><td>" + dt.Rows[i]["EmpName"].ToString() + "</td><td>" + dt.Rows[i]["InTime"].ToString().Replace("#", " ") + "</td><td>" + dt.Rows[i]["OutTime"].ToString().Replace("#", " ") + "</td></tr>";
                }
                str += "</table></span>";
            }
        }
        else
        {
            str = "<table style=width:100px;text-align:center;><tr><th>No Details</th></tr></table>";
        }
        return str;
    }

    [System.Web.Services.WebMethod(EnableSession = true)]
    public static string LoadIdleInOutTime(string RefNo)
    {
        DataTable dt = new DataTable();
        dt = GetInOutTime(RefNo);
        string str = "";
        if (dt.Rows.Count > 0)
        {
            str = "<span><table style=font-style:arial;><tr style=background-color:#c3d9ff;text-align:center;><th style=width:100px;>Process</th><th style=width:100px;>Time(mins)</th></tr>";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                str += "<tr><td>" + dt.Rows[i]["ProcessName"].ToString() + "</td><td style=text-align:center;>" + dt.Rows[i]["IdleFrom"].ToString() + "</td></tr>";
            }
            str += "</table></span>";
        }
        else
        {
            str = "<table style=width:100px;text-align:center;><tr><th>No Details</th></tr></table>";
        }
        return str;
    }

    protected void btn_logout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();
        Response.Redirect("login.aspx");
    }

    protected void ListView10_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        SetIdleBackImage(e);
        Label lbl = (Label)e.Item.FindControl("lblRefNo");
        HtmlGenericControl hoverDiv = (HtmlGenericControl)e.Item.FindControl("BackDiv");
        hoverDiv.Attributes.Add("onmouseover", "ShowLoadProcessInOutTime(event,'" + lbl.Text + "','Vehicle Idle')");
        hoverDiv.Attributes.Add("onmouseout", "hideTooltip(event)");
    }

    protected void btnBACK_Click(object sender, EventArgs e)
    {
        if (BackTo == "GMSERVICE")
        {
            Response.Redirect("DisplayHome.aspx?Back=333", false);
        }
        else if (BackTo == "SM")
        {
            Response.Redirect("DisplayHome.aspx?Back=222", false);
        }
        else if (BackTo == "DISPLAY")
        {
            Response.Redirect("DisplayHome.aspx", false);
        }
    }
}