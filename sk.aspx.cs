using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class sk : System.Web.UI.Page
{
    private SqlConnection dbaseCon = new SqlConnection(ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {        
    }    
}