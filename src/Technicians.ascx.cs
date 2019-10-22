using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Technicians : System.Web.UI.UserControl
{
    public event EventHandler UCButtonClick;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
        public string Tech
        {
            get
            {
                return lbl_Name.InnerText.ToString();
            }
            set
            {
                lbl_Name.InnerText = value;
            }
        }
     public string Employe_Type
    {
        get
        {
            return Emp_Type.Text.ToString();
        }
        set
        {
            Emp_Type.Text = value;
        }
    }
    public string Employe_Status
    {
        get
        {
            return lbl_status.Text.ToString();
        }
        set
        {
            lbl_status.Text = value;
            if (lbl_status.Text == "1")
            {
                Image1.ImageUrl = "https://res.cloudinary.com/deekyp5bi/image/upload/v1484285490/jcr/Mechanic_Green.png";
                //  lbl_status.Text = "";
                //lbl_status.Text = "<img src='images/JCR/circle_green Small.png' style='width:10px;'/>";
            }
            else if (lbl_status.Text == "2")
            {
                Image1.ImageUrl = "https://res.cloudinary.com/deekyp5bi/image/upload/v1484285490/jcr/Mechanic_Yellow.png";
                //lbl_status.Text = "";
                //lbl_status.Text = "<img src='images/JCR/circle_yellow small.png' style='width:10px;'/>";
            }
            else
            {
                lbl_status.Text = "<img src='https://res.cloudinary.com/deekyp5bi/image/upload/v1484205155/jcr/circle_red_small.png' style='width:10px;'/>";
            }
        }
    }
}