using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class zzzzz : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        TextBox1.Text = "Hi";
    }
    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
        TextBox1.Text = "Hi how are you";
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        TextBox1.Text = "Hi, how are you";
    }
}