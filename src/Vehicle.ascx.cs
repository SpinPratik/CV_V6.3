using System;

public partial class Vehicle : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public string VehicleColor
    {
        set
        {
            VehicleBG.Style.Add("background-color", value);
        }
    }
    public string idletime
    {
        get
        {
            return lbl_idleTime.Text.ToString();
        }
        set
        {
            lbl_idleTime.Text = value;
        }
    }
    public string VehicleImage
    {
        set
        {
            img_Vehicle.ImageUrl = value;
        }
    }

    public string PDTImage
    {
        set
        {
            img_PDT.ImageUrl = value;
        }
    }

    public string CWJDPImage
    {
        set
        {
            img_CWJDP.ImageUrl = value;
        }
    }

    public string RegNo
    {
        get
        {
            return lbl_RegNo.Text.ToString();
        }
        set
        {
            lbl_RegNo.Text = value;
        }
    }

    public string PDT
    {
        get
        {
            return lbl_PDT.Text.ToString();
        }
        set
        {
            lbl_PDT.Text = value;
        }
    }

    public string GateInTime
    {
        get
        {
            return lbl_GateInTime.Text.ToString();
        }
        set
        {
            lbl_GateInTime.Text = value;
        }
    }

    public string Model
    {
        get
        {
            return lbl_Model.Text.ToString();
        }
        set
        {
            lbl_Model.Text = value;
        }
    }

    public string ServiceAdvisor
    {
        get
        {
            return lbl_ServiceAdvisor.Text.ToString();
        }
        set
        {
            lbl_ServiceAdvisor.Text = value;
        }
    }

}