using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cashier : System.Web.UI.Page
{
    private SqlConnection con = new SqlConnection(DataManager.ConStr);
    private Decimal Cash;
    private Decimal Card;
    private Decimal Credit;
    private Decimal Cash1;
    private Decimal Card1;
    private Decimal Credit1;
    private Decimal Home;
    private Decimal CustPending;
    private Decimal HomePend;
    private Decimal CustPending1;
    private string userId = "0";
    private decimal BillAmount;
    private decimal BillAmount1;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                lblRefNo.Text = "0";
                if (Session["ROLE"] == null || Session["ROLE"].ToString() != "CASHIER")
                {
                    Response.Redirect("login.aspx");
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("login.aspx");
            }
            rbLsit.SelectedIndex = 0;
            MultiView1.ActiveViewIndex = 0;
        }
        fillgrid();
        fillPendinggrid();
        Session["Current_Page"] = "Bill Amount Details";
        this.Title = "Bill Amount Details";
        string[] str = DataManager.GetDealerDetails(Session["ConnectionString"].ToString()).Split(',');
        Session["CompanyName"] = str[0];
    }

    protected void chkCash_CheckedChanged(object sender, EventArgs e)
    {
        if (chkCash.Checked)
        {
            txtCash.ReadOnly = false;
            txtCash.Text = GetRestAmount().ToString();
            txtCash.Focus();
        }
        else
        {
            txtCash.ReadOnly = true;
            txtCash.Text = "0.00";
        }
    }

    protected void chkCard_CheckedChanged(object sender, EventArgs e)
    {
        if (chkCard.Checked)
        {
            txtCard.ReadOnly = false;
            txtCard.Text = GetRestAmount().ToString();
            txtCard.Focus();
        }
        else
        {
            txtCard.ReadOnly = true;
            txtCard.Text = "0.00";
        }
    }

    protected void btn_logout_Click(object sender, EventArgs e)
    {
        Response.Redirect("login.aspx");
    }

    protected void btn_Support_Click(object sender, EventArgs e)
    {
        Response.Redirect("Complain.aspx");
    }

    protected void grdCashier_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdCashier.PageIndex = e.NewPageIndex;
        fillgrid();
    }

    protected void grdCashier_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[1].Visible = false;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[4].Text.Trim() == "True" || e.Row.Cells[4].Text.Trim() == "1")
            {
                e.Row.Attributes.Add("Style", "background-color:#9CC7F2;color:White;");
            }
        }
        e.Row.Cells[6].Visible = false;
    }

    protected void grdCashier_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtTagNo.Enabled = false;
            if (grdCashier.SelectedRow.Cells[3].Text.ToString() == "&nbsp;")
                txtTagNo.Text = "";
            else
                txtRegNo.Text = grdCashier.SelectedRow.Cells[3].Text.Trim();

            txtTagNo.Text = grdCashier.SelectedRow.Cells[2].Text.Trim();
            lblRefNo.Text = grdCashier.SelectedRow.Cells[1].Text.Trim();
            Session["SlNoNew"] = lblRefNo.Text;
            lblMsg.Text = "";
        }
        catch (Exception ex)
        {
        }
    }

    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (chkCredit.Checked)
        {
            txtCredit.ReadOnly = false;
            txtCredit.Text = GetRestAmount().ToString();
            txtCredit.Focus();
        }
        else
        {
            txtCredit.ReadOnly = true;
            txtCredit.Text = "0.00";
        }
    }

    protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
    {
        if (chkHome.Checked)
        {
            txtHome.ReadOnly = false;
            txtHome.Text = GetRestAmount().ToString();
            txtHome.Focus();
        }
        else
        {
            txtHome.ReadOnly = true;
            txtHome.Text = "0.00";
        }
    }

    protected void CheckBox3_CheckedChanged(object sender, EventArgs e)
    {
        if (chkCustPend.Checked)
        {
            txtCustPend.ReadOnly = false;
            txtCustPend.Text = GetRestAmount().ToString();
            txtCustPend.Focus();
        }
        else
        {
            txtCustPend.ReadOnly = true;
            txtCustPend.Text = "0.00";
        }
    }

    private void fillgrid()
    {
        try
        {
            grdCashier.DataSource = null;
            SqlCommand cmd = new SqlCommand("GetJobCards", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count != 0)
            {
                grdCashier.DataSource = dt;
                grdCashier.DataBind();
            }
            else
            {
                grdCashier.DataSource = null;
                grdCashier.DataBind();
            }
        }
        catch (Exception ex)
        {
        }
    }

    private Int64 GetRestAmount()
    {
        Int64 BillAmount1 = 0;
        Int64 Cash = 0;
        Int64 Card = 0;
        Int64 Credit = 0;
        Int64 HomeDelivery = 0;
        Int64 Pending = 0;
        Int64 Balance = 0;
        try
        {
            BillAmount1 = Convert.ToInt64(txtBillAmount.Text.Trim().ToString());
            txtBillAmount.Text = BillAmount1.ToString();
        }
        catch (Exception e1)
        {
            BillAmount = 0;
            txtBillAmount.Text = "0.00";
            txtBillAmount.Focus();
        }
        try
        {
            Cash = Convert.ToInt64(txtCash.Text.Trim().ToString());
            txtCash.Text = Cash.ToString();
        }
        catch (Exception e1)
        {
            Cash = 0;
            txtCash.Text = "0.00";
            txtCash.Focus();
        }
        try
        {
            Card = Convert.ToInt64(txtCard.Text.Trim().ToString());
            txtCard.Text = Card.ToString();
        }
        catch (Exception e1)
        {
            Card = 0;
            txtCard.Text = "0.00";
            txtCard.Focus();
        }
        try
        {
            Credit = Convert.ToInt64(txtCredit.Text.Trim().ToString());
            txtCredit.Text = Credit.ToString();
        }
        catch (Exception e1)
        {
            Credit = 0;
            txtCredit.Text = "0.00";
            txtCredit.Focus();
        }
        try
        {
            HomeDelivery = Convert.ToInt64(txtHome.Text.Trim().ToString());
            txtHome.Text = HomeDelivery.ToString();
        }
        catch (Exception e1)
        {
            HomeDelivery = 0;
            txtHome.Text = "0.00";
            txtHome.Focus();
        }
        try
        {
            Pending = Convert.ToInt64(txtCustPend.Text.Trim().ToString());
            txtCustPend.Text = Pending.ToString();
        }
        catch (Exception e1)
        {
            Pending = 0;
            txtCustPend.Text = "0.00";
            txtCustPend.Focus();
        }
        Balance = BillAmount1 - (Cash + Card + Credit + HomeDelivery + Pending);
        if (BillAmount1 <= 0)
        {
            txtCash.Text = "0.00";
            txtCard.Text = "0.00";
            txtCredit.Text = "0.00";
            txtHome.Text = "0.00";
            txtCustPend.Text = "0.00";
        }
        return (Balance < 0 ? 0 : Balance);
    }

    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        try
        {
            userId = Session["UserId"].ToString();
            if (txtRegNo.Text.ToString() == "" && txtTagNo.Text.ToString() == "")
            {
                lblMsg.Text = " Please Enter eighter TagNo or VehicleNo..!";
            }
            else if (txtBillAmount.Text.ToString() == "" || txtBillAmount.Text.ToString().Trim() == "0")
            {
                lblMsg.Text = "Please Enter Bill Amount..!";
            }
            else if (txtCash.Text.ToString() == "" && txtCard.Text.ToString() == "" && txtCredit.Text.ToString() == "" && txtHome.Text.ToString() == "" && txtCustPend.Text.ToString() == "")
            {
                lblMsg.Text = "Please Enter Any One Of Credit Details ..!";
            }
            else
            {
                BillAmount = Convert.ToDecimal(txtBillAmount.Text.Trim());
                if (txtCash.Text.Trim() != "")
                    Cash = Convert.ToDecimal(txtCash.Text.Trim());
                else
                    Cash = 0;
                if (txtCard.Text.Trim() != "")

                    Card = Convert.ToDecimal(txtCard.Text.Trim());
                else
                    Card = 0;
                if (txtCredit.Text.Trim() != "")
                    Credit = Convert.ToDecimal(txtCredit.Text.Trim());
                else
                    Credit = 0;
                if (txtHome.Text.Trim() != "")
                    Home = Convert.ToDecimal(txtHome.Text.Trim());
                else
                    Home = 0;
                if (txtCustPend.Text.Trim() != "")
                    CustPending = Convert.ToDecimal(txtCustPend.Text.Trim());
                else
                    CustPending = 0;

                if (GetTotal(BillAmount, Cash, Card, Credit, Home, CustPending))
                {
                    lblMsg.ForeColor = Color.Red;
                    lblMsg.Text = "Specified Total Amount Should Be Match with Bill Amount..! ";
                    txtBillAmount.Focus();
                }

                else
                {
                    SqlCommand cmd1 = new SqlCommand("", con);
                    if (txtTagNo.Text.ToString() != "")
                    {
                        cmd1.CommandText = "UdpGetSlNo";
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("@RFID", txtTagNo.Text.ToString());
                    }
                    else
                    {
                        cmd1.CommandText = "UdpGetSlNoByRegNo";
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("@RegNo", txtRegNo.Text.ToString());
                    }
                    SqlDataAdapter da = new SqlDataAdapter(cmd1);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count == 0)
                    {
                        lblMsg.ForeColor = Color.Red;
                        lblMsg.Text = "There Is No Vehicle With This No Or Already Done..!";
                    }
                    else
                    {
                        Session["SlNoNew"] = dt.Rows[0]["SlNo"].ToString();
                        SqlCommand cmd = new SqlCommand("UdpInsertCashDetails", con);
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RFID", txtTagNo.Text.ToString());
                        cmd.Parameters.AddWithValue("@RegNo", txtRegNo.Text.ToString());
                        cmd.Parameters.AddWithValue("@BillAmount", txtBillAmount.Text.Trim().ToString());

                        if (chkHome.Checked != true && chkCustPend.Checked != true)
                        {
                            if (chkCash.Checked == true && txtCash.Text.Trim() != "")
                                cmd.Parameters.AddWithValue("@Cash", txtCash.Text.Trim().ToString());
                            else
                                cmd.Parameters.AddWithValue("@Cash", DBNull.Value);
                            if (chkCard.Checked == true && txtCard.Text.Trim() != "")
                                cmd.Parameters.AddWithValue("@Card", txtCard.Text.Trim().ToString());
                            else
                                cmd.Parameters.AddWithValue("@Card", DBNull.Value);
                            if (chkCredit.Checked == true && txtCredit.Text.Trim() != "")
                                cmd.Parameters.AddWithValue("@Credit", txtCredit.Text.Trim().ToString());
                            else
                                cmd.Parameters.AddWithValue("@Credit", DBNull.Value);
                            cmd.Parameters.AddWithValue("@HomeDelivery", DBNull.Value);
                            cmd.Parameters.AddWithValue("@PendingCustomer", DBNull.Value);
                            cmd.Parameters.AddWithValue("@BillDate", System.DateTime.Now);
                            cmd.Parameters.AddWithValue("@PaymentDate", System.DateTime.Now);
                            cmd.Parameters.AddWithValue("@UserId", userId);
                            cmd.Parameters.AddWithValue("@Remarks", txtRemarks.Text.Trim().ToString());
                        }
                        else
                        {
                            if (chkCash.Checked == true && txtCash.Text.Trim() != "")
                                cmd.Parameters.AddWithValue("@Cash", txtCash.Text.Trim().ToString());
                            else
                                cmd.Parameters.AddWithValue("@Cash", DBNull.Value);
                            if (chkCard.Checked == true && txtCard.Text.Trim() != "")
                                cmd.Parameters.AddWithValue("@Card", txtCard.Text.Trim().ToString());
                            else
                                cmd.Parameters.AddWithValue("@Card", DBNull.Value);
                            if (chkCredit.Checked == true && txtCredit.Text.Trim() != "")
                                cmd.Parameters.AddWithValue("@Credit", txtCredit.Text.Trim().ToString());
                            else
                                cmd.Parameters.AddWithValue("@Credit", DBNull.Value);
                            if (chkHome.Checked == true && txtHome.Text.Trim() != "")
                                cmd.Parameters.AddWithValue("@HomeDelivery", txtHome.Text.Trim().ToString());
                            else
                                cmd.Parameters.AddWithValue("@HomeDelivery", DBNull.Value);
                            if (chkCustPend.Checked == true && txtCustPend.Text.Trim() != "")
                                cmd.Parameters.AddWithValue("@PendingCustomer", txtCustPend.Text.Trim().ToString());
                            else
                                cmd.Parameters.AddWithValue("@PendingCustomer", DBNull.Value);
                            cmd.Parameters.AddWithValue("@BillDate", System.DateTime.Now);
                            cmd.Parameters.AddWithValue("@PaymentDate", DBNull.Value);
                            cmd.Parameters.AddWithValue("@UserId", userId);
                            if (txtRemarks.Text.ToString() == "")
                                cmd.Parameters.AddWithValue("@Remarks", DBNull.Value);
                            else
                                cmd.Parameters.AddWithValue("@Remarks", txtRemarks.Text.Trim().ToString());
                        }
                        SqlParameter Flag = cmd.Parameters.Add("@Msg", SqlDbType.VarChar, 100);
                        Flag.Direction = ParameterDirection.Output;
                        Flag.Value = "";
                        cmd.ExecuteNonQuery();
                        con.Close();
                        lblMsg.ForeColor = Color.Green;
                        lblMsg.Text = Flag.Value.ToString();
                        Clear();
                        fillgrid();
                        if (lblMsg.Text == "Saved Successfully")
                            Response.Redirect("PrintNewBill.aspx", false);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.ForeColor = Color.Red;
            lblMsg.Text = ex.Message.ToString();
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Clear();
        lblMsg.Text = "";
        fillgrid();
        txtRegNo.Focus();
    }

    private void Clear()
    {
        txtRegNo.Text = "";
        txtTagNo.Text = "";
        txtTagNo.Enabled = true;
        txtBillAmount.Text = "0.00";
        txtCard.Text = "0.00";
        txtCard.ReadOnly = true;
        txtCash.Text = "0.00";
        txtCash.ReadOnly = true;
        txtCustPend.Text = "0.00";
        txtCustPend.ReadOnly = true;
        txtCredit.Text = "0.00";
        txtCredit.ReadOnly = true;
        txtHome.Text = "0.00";
        txtHome.ReadOnly = true;
        chkCash.Checked = false;
        txtRemarks.Text = "";
        chkCard.Checked = false;
        chkCredit.Checked = false;
        chkCustPend.Checked = false;
        chkHome.Checked = false;
        lblRefNo.Text = "0";
    }

    private bool GetTotal(Decimal BillAmount, Decimal Cash, Decimal Card, Decimal Credit, Decimal Home, Decimal CustPend)
    {
        Decimal Total = Cash + Credit + Card + Home + CustPend;
        if (BillAmount == Total)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private bool GetTotalPending(Decimal BillAmountPend, Decimal CashPend, Decimal CardPend, Decimal CreditPend)
    {
        Decimal Total = CashPend + CreditPend + CardPend;
        if (BillAmountPend == Total)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    protected void rbLsit_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbLsit.SelectedIndex == 0)
        {
            MultiView1.ActiveViewIndex = 0;
            lblMsg.Text = "";
        }
        else
        {
            MultiView1.ActiveViewIndex = 1;
            lblMsg1.Text = "";
        }
    }

    protected void GrdPendingList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdPendingList.PageIndex = e.NewPageIndex;
        fillPendinggrid();
    }

    protected void GrdPendingList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[1].Visible = false;
        e.Row.Cells[6].Visible = false;
        e.Row.Cells[7].Visible = false;
    }

    protected void GrdPendingList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblRefNo1.Text = GrdPendingList.SelectedRow.Cells[1].Text.Trim();
            Session["SlNo"] = lblRefNo1.Text.ToString();
            txtPendingTagNo.Text = GrdPendingList.SelectedRow.Cells[2].Text.Trim();
            if (GrdPendingList.SelectedRow.Cells[3].Text.Trim() == "" || GrdPendingList.SelectedRow.Cells[3].Text.Trim() == "&nbsp;")
                txtPendingVehNo.Text = "";
            else
                txtPendingVehNo.Text = GrdPendingList.SelectedRow.Cells[3].Text.Trim();
            lblSlNo.Text = GrdPendingList.SelectedRow.Cells[7].Text.Trim();
            if (GrdPendingList.SelectedRow.Cells[4].Text.Trim() == "" || GrdPendingList.SelectedRow.Cells[4].Text.Trim() == "&nbsp;")
            {
                HomePend = 0;
            }
            else
            {
                HomePend = Convert.ToDecimal(GrdPendingList.SelectedRow.Cells[4].Text.Trim());
            }
            if (GrdPendingList.SelectedRow.Cells[5].Text.Trim() == "" || GrdPendingList.SelectedRow.Cells[5].Text.Trim() == "&nbsp;")
            {
                CustPending1 = 0;
            }
            else
            {
                CustPending1 = Convert.ToDecimal(GrdPendingList.SelectedRow.Cells[5].Text.Trim());
            }
            Decimal Total = HomePend + CustPending1;
            lblMsg1.Text = "";
            String Str1 = Total.ToString();
            String[] Cash = Str1.Split('.');
            String Cash1 = Cash[0];
            txtPendingBillAmt.Text = Cash1;
        }
        catch (Exception ex)
        {
            lblMsg1.ForeColor = Color.Red;
            lblMsg1.Text = ex.Message.ToString();
        }
    }

    private void fillPendinggrid()
    {
        try
        {
            GrdPendingList.DataSource = null;
            SqlCommand cmd = new SqlCommand("GetPendingList", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count != 0)
            {
                GrdPendingList.DataSource = dt;
                GrdPendingList.DataBind();
            }
            else
            {
                GrdPendingList.DataSource = null;
                GrdPendingList.DataBind();
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void txtPendingVehNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Int64 HomeDelivery = 0;
            Int64 PendingCustomer = 0;
            Int64 PendingAmount = 0;
            lblMsg1.Text = "";
            if (txtPendingVehNo.Text.ToString() != "")
            {
                SqlCommand cmd = new SqlCommand("GetBillingAmount", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RegNo", txtPendingVehNo.Text.ToString());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    lblRefNo1.Text = dt.Rows[0]["ServiceId"].ToString();
                    lblSlNo.Text = dt.Rows[0]["SlNo"].ToString();
                    if (dt.Rows[0]["HomeDelivery"].ToString() != "")
                        HomeDelivery = Convert.ToInt64(dt.Rows[0]["HomeDelivery"]);
                    if (dt.Rows[0]["PendingCustomer"].ToString() != "")
                        PendingCustomer = Convert.ToInt64(dt.Rows[0]["PendingCustomer"]);
                    PendingAmount = HomeDelivery + PendingCustomer;
                    txtPendingBillAmt.Text = PendingAmount.ToString();
                }
                else
                {
                    lblMsg1.ForeColor = Color.Red;
                    lblMsg1.Text = "There is No Record With this Vehicle No...!";
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void btnSave1_Click(object sender, EventArgs e)
    {
        try
        {
            if ((txtPendingVehNo.Text.ToString() == "" && txtPendingVehNo.Text.ToString().Trim() != "0") && (txtPendingTagNo.Text.ToString() == "" && txtPendingTagNo.Text.ToString().Trim() != "0"))
            {
                lblMsg1.ForeColor = Color.Red;
                lblMsg1.Text = "Please Enter eighter Tag No Or Vehicle No..!";
            }
            else
            {
                BillAmount1 = Convert.ToDecimal(txtPendingBillAmt.Text.ToString());
                if (txtCashPend.Text.Trim() != "")
                    Cash1 = Convert.ToDecimal(txtCashPend.Text.Trim());
                else
                    Cash1 = 0;
                if (txtCardPend.Text.Trim() != "")

                    Card1 = Convert.ToDecimal(txtCardPend.Text.Trim());
                else
                    Card1 = 0;
                if (txtCreditPend.Text.Trim() != "")
                    Credit1 = Convert.ToDecimal(txtCreditPend.Text.Trim());
                else
                    Credit1 = 0;

                if (GetTotalPending(BillAmount1, Cash1, Card1, Credit1))
                {
                    lblMsg1.ForeColor = Color.Red;
                    lblMsg1.Text = "Specified Total Amount Should Be Match with Bill Amount..! ";
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("UdpBillUpdation", con);
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SlNo", lblSlNo.Text.Trim().ToString());
                    cmd.Parameters.AddWithValue("@ServiceId", lblRefNo1.Text.Trim().ToString());
                    if (chkCashPay.Checked == true && txtCashPend.Text.Trim() != "")
                        cmd.Parameters.AddWithValue("@Cash", txtCashPend.Text.ToString());
                    else
                        cmd.Parameters.AddWithValue("@Cash", DBNull.Value);
                    if (chkCardPay.Checked == true && txtCardPend.Text.Trim() != "")
                        cmd.Parameters.AddWithValue("@Card", txtCardPend.Text.ToString());
                    else
                        cmd.Parameters.AddWithValue("@Card", DBNull.Value);
                    if (chkCreditPay.Checked == true && txtCreditPend.Text.Trim() != "")
                        cmd.Parameters.AddWithValue("@Credit", txtCreditPend.Text.ToString());
                    else
                        cmd.Parameters.AddWithValue("@Credit", DBNull.Value);
                    SqlParameter Flag = cmd.Parameters.Add("@Msg", SqlDbType.VarChar, 100);
                    Flag.Direction = ParameterDirection.Output;
                    Flag.Value = "";
                    cmd.ExecuteNonQuery();
                    lblMsg1.Text = Flag.Value.ToString();
                    con.Close();
                    txtPendingBillAmt.Text = "";
                    txtPendingVehNo.Text = "";
                    ClearPending();
                    fillPendinggrid();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg1.Text = ex.Message.ToString();
        }
    }

    protected void btnClear1_Click(object sender, EventArgs e)
    {
        ClearPending();
        fillPendinggrid();
        lblMsg1.Text = "";
        txtTagNo.Text = "";
        txtPendingTagNo.Text = "";
        txtPendingTagNo.Focus();
    }

    protected void Unnamed1_Click(object sender, EventArgs e)
    {
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        //printWindow = window.open("", "printVersion", "menubar,scrollbars,width=640,height=480,top=0,left=0");
        //printData = document.getElementById("lblReport").innerHTML;
        //printWindow.document.write(printData);
        //printWindow.document.close();
        //printWindow.print();
    }

    protected void chkCashPay_CheckedChanged(object sender, EventArgs e)
    {
        if (chkCashPay.Checked)
        {
            txtCashPend.ReadOnly = false;
            txtCashPend.Text = GetRestAmountPend().ToString();
            txtCashPend.Focus();
        }
        else
        {
            txtCashPend.ReadOnly = true;
            txtCashPend.Text = "0.00";
        }
    }

    protected void chkCardPay_CheckedChanged(object sender, EventArgs e)
    {
        if (chkCardPay.Checked)
        {
            txtCardPend.ReadOnly = false;
            txtCardPend.Text = GetRestAmountPend().ToString();
            txtCardPend.Focus();
        }
        else
        {
            txtCardPend.ReadOnly = true;
            txtCardPend.Text = "0.00";
        }
    }

    protected void chkCreditPay_CheckedChanged(object sender, EventArgs e)
    {
        if (chkCreditPay.Checked)
        {
            txtCreditPend.ReadOnly = false;
            txtCreditPend.Text = GetRestAmountPend().ToString();
            txtCreditPend.Focus();
        }
        else
        {
            txtCreditPend.ReadOnly = true;
            txtCreditPend.Text = "0.00";
        }
    }

    private void ClearPending()
    {
        txtPendingVehNo.Text = "";
        txtPendingBillAmt.Text = "0.00";
        txtCreditPend.Text = "0.00";
        txtCashPend.Text = "0.00";
        txtCardPend.Text = "0.00";
        txtCardPend.ReadOnly = true;
        txtCashPend.ReadOnly = true;
        txtCreditPend.ReadOnly = true;
        chkCardPay.Checked = false;
        chkCashPay.Checked = false;
        chkCreditPay.Checked = false;
        chkCash.Checked = false;
        lblRefNo1.Text = "0";
        lblSlNo.Text = "0";
    }

    private Int64 GetRestAmountPend()
    {
        Int64 BillAmount2 = 0;
        Int64 Cash1 = 0;
        Int64 Card1 = 0;
        Int64 Credit1 = 0;

        Int64 Balance1 = 0;
        try
        {
            BillAmount2 = Convert.ToInt64(txtPendingBillAmt.Text.Trim().ToString());
            txtPendingBillAmt.Text = BillAmount2.ToString();
        }
        catch (Exception e1)
        {
            BillAmount2 = 0;
            txtPendingBillAmt.Text = "0.00";
            txtPendingBillAmt.Focus();
        }

        try
        {
            Cash1 = Convert.ToInt64(txtCashPend.Text.Trim().ToString());
            txtCashPend.Text = Cash1.ToString();
        }
        catch (Exception e1)
        {
            Cash = 0;
            txtCashPend.Text = "0.00";
            txtCashPend.Focus();
        }

        try
        {
            Card1 = Convert.ToInt64(txtCardPend.Text.Trim().ToString());
            txtCardPend.Text = Card1.ToString();
        }
        catch (Exception e1)
        {
            Card = 0;
            txtCardPend.Text = "0.00";
            txtCardPend.Focus();
        }

        try
        {
            Credit1 = Convert.ToInt64(txtCreditPend.Text.Trim().ToString());
            txtCreditPend.Text = Credit1.ToString();
        }
        catch (Exception e1)
        {
            Credit = 0;
            txtCreditPend.Text = "0.00";
            txtCreditPend.Focus();
        }

        Balance1 = BillAmount2 - (Cash1 + Card1 + Credit1);
        if (BillAmount2 <= 0)
        {
            txtCashPend.Text = "0.00";
            txtCardPend.Text = "0.00";
            txtCreditPend.Text = "0.00";
        }
        return (Balance1 < 0 ? 0 : Balance1);
    }

    protected void ImageButton2_Click1(object sender, ImageClickEventArgs e)
    {
        lblMsg.Text = "";
        if (txtTagNo.Text.ToString() == "")
        {
            lblMsg.Text = "Please Enter Tag No..!";
        }
        else
        {
            try
            {
                SqlCommand cmd1 = new SqlCommand("GetRegnoinCashier", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@RFID", txtTagNo.Text.ToString());
                SqlDataAdapter sda = new SqlDataAdapter(cmd1);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    txtRegNo.Text = dt.Rows[0]["RegNo"].ToString();
                }
                else
                {
                    lblMsg.ForeColor = Color.Red;
                    lblMsg.Text = "There is no Vehicle with This TagNo Or Already Done..!";
                }
            }
            catch (Exception ex)
            {
            }
        }
    }

    protected void txtPendingTagNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Int64 HomeDelivery = 0;
            Int64 PendingCustomer = 0;
            Int64 PendingAmount = 0;
            lblMsg1.Text = "";
            if (txtPendingTagNo.Text.ToString() != "")
            {
                SqlCommand cmd = new SqlCommand("GetBillingAmountByTagNo", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TagNo", txtPendingTagNo.Text.ToString());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    lblRefNo1.Text = dt.Rows[0]["ServiceId"].ToString();
                    lblSlNo.Text = dt.Rows[0]["SlNo"].ToString();
                    txtPendingVehNo.Text = dt.Rows[0]["VehicleNo"].ToString();
                    if (dt.Rows[0]["HomeDelivery"].ToString() != "")
                        HomeDelivery = Convert.ToInt64(dt.Rows[0]["HomeDelivery"]);
                    if (dt.Rows[0]["PendingCustomer"].ToString() != "")
                        PendingCustomer = Convert.ToInt64(dt.Rows[0]["PendingCustomer"]);
                    PendingAmount = HomeDelivery + PendingCustomer;
                    txtPendingBillAmt.Text = PendingAmount.ToString();
                }
                else
                {
                    lblMsg1.ForeColor = Color.Red;
                    lblMsg1.Text = "There is No Record With this Tag No...!";
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
}