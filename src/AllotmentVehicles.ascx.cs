using System;

public partial class AllotmentVehicles : System.Web.UI.UserControl
{
    public event EventHandler UCButtonClick;
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public string VehicleColor
    {
        set
        {
            lbl_PDT.Style.Add("color", value);
        }
    }

    public string VehicleImage
    {
        set
        {
            img_Vehicle.ImageUrl = value;
        }
    }
    public string CWJDP
    {
        set
        {
            ImageCWJDP.ImageUrl = value;
        }
    }
    
         public string PDTStatus
    {
        set
        {
            ImagePDT.ImageUrl = value;
        }
    }

    public string RegNo
    {
        get
        {
            return lbl_RegNo.InnerText.ToString();
        }
        set
        {
            lbl_RegNo.InnerText = value;
        }
    }
    public string Slno
    {
        get
        {
            return lbl_Slno.Text.ToString();
        }
        set
        {
            lbl_Slno.Text = value;
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