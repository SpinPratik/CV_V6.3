using System;

public partial class PositionDisplay : System.Web.UI.UserControl
{
    public event EventHandler UCButtonClick;
    protected void Page_Load(object sender, EventArgs e)
    {
        //VehicleBG.Attributes.Add("ShowLoadProcessInOutTime(event,'" + dtVehicle.Rows[flag]["Slno"].ToString() + "','" + ProcessName + "')");


    }

    public string VehicleColor
    {
        set
        {
            VehicleBG.Style.Add("border-bottom-color", value);
            VehicleBG.Style.Add("border-right-color", value);
            

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
    public string LastProcess
    {
        set
        {
            if (value != "Gate")
            {
                img_LastProcess.Visible = true;
                img_LastProcess.ImageUrl = value;
            }
            else
            {
                img_LastProcess.Visible = false;
                //Image1.Visible=false;

            }

        }
    }
    //public string CWJDPImage
    //{
    //    set
    //    {
    //        img_CWJDP.ImageUrl = value;
    //    }
    //}
    //public bool PDTCheck
    //{
    //    set
    //    {
    //        lbl_PDT.Visible = value;
    //        Label1.Visible = value;
    //    }
    //}

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
    //public string PDT
    //{
    //    get
    //    {
    //        return lbl_PDT.Text.ToString();
    //    }
    //    set
    //    {
    //        lbl_PDT.Text = value;
    //    }
    //}

    //public string GateInTime
    //{
    //    get
    //    {
    //        return lbl_GateInTime.Text.ToString();
    //    }
    //    set
    //    {
    //        lbl_GateInTime.Text = value;
    //    }
    //}

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

    //protected void ImgButton_Click1(object sender, System.Web.UI.ImageClickEventArgs e)
    //{
    //    //UCButtonClick(sender, e);
    //    UserControlFunction();
    //}
    //public string UserControlFunction()
    //{
    //    lbl_PDT.Visible = true;
    //    return (lbl_PDT.Visible.ToString());



    //    //string text = "This function is in user control";
    //    //return text;
    //}
}