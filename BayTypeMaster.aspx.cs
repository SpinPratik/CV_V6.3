using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BayTypeMaster : System.Web.UI.Page
{
    private DataTable dtBayCap = new DataTable();
    private SqlConnection con = new SqlConnection();

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        con.ConnectionString = DataManager.ConStr;
        try
        {
            if (Session["ROLE"] == null || Session["ROLE"].ToString() != "ADMIN")
            {
                Response.Redirect("login.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("login.aspx");
        }
        if (!Page.IsPostBack)
        {
            Session["CURRENT_PAGE"] = "Bay Type Management";
            this.Title = "Bay Type Management";
            FillBackColor();
            BindBayCapGrid();
        }
    }

    protected void BindBayCapGrid()
    {
        try
        {
            string bindStr = "Select * from TblBayType";
            SqlCommand cmdBay = new SqlCommand(bindStr, con);
            SqlDataAdapter sdaBay = new SqlDataAdapter(cmdBay);
            sdaBay.Fill(dtBayCap);
            if (dtBayCap.Rows.Count > 0)
            {
                grdBayCap.DataSource = dtBayCap;
                grdBayCap.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.ForeColor = Color.Red;
            lblMsg.Text = "Error loading data in grid";
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtBayTypeName.Text != "" && txtCapacity.Text != "")
        {
            try
            {
                lblMsg.Text = "";
                string BayName = txtBayTypeName.Text.Trim();
                int capacity = Convert.ToInt32(txtCapacity.Text.Trim());
                string stradd = "insert into tblBayType values('" + BayName + "'," + capacity + ")";
                SqlCommand cmdAdd = new SqlCommand(stradd, con);
                con.Open();
                int x = cmdAdd.ExecuteNonQuery();
                con.Close();
                if (x > 0)
                {
                    BindBayCapGrid();
                    Clear();
                    lblMsg.ForeColor = Color.Green;
                    lblMsg.Text = "Successfully added!";
                }
            }
            catch (Exception ex)
            {
                lblMsg.ForeColor = Color.Red;
                lblMsg.Text = "Error! Data not added";
            }
        }
        else
        {
            lblMsg.ForeColor = Color.Red;
            lblMsg.Text = "Please Enter BayTypeName and Capacity";
        }
    }

    protected void Clear()
    {
        lblMsg.Text = "";
        txtBayTypeName.Text = "";
        txtCapacity.Text = "";
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Clear();
    }

    protected void grdBayCap_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[6].Visible = false;
        }
        catch
        {
        }
    }

    protected void grdBayCap_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdBayCap.EditIndex = e.NewEditIndex;
        BindBayCapGrid();
    }

    protected void grdBayCap_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdBayCap.EditIndex = -1;
        BindBayCapGrid();
    }

    protected void grdBayCap_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            int BayTypId = Convert.ToInt32(grdBayCap.Rows[e.RowIndex].Cells[4].Text);
            string BayTypeNameStr = ((TextBox)(grdBayCap.Rows[e.RowIndex].Cells[1].FindControl("txteditBayType"))).Text;
            int capacityNum = Convert.ToInt32(((TextBox)(grdBayCap.Rows[e.RowIndex].Cells[2].FindControl("txteditCapacity"))).Text);

            string updateStr = "Update TblBayType set BayTypeName='" + BayTypeNameStr + "'" + " , Capacity=" + capacityNum + " where BayTypeId=" + BayTypId;
            SqlCommand cmdUpd = new SqlCommand(updateStr, con);
            con.Open();
            int x = cmdUpd.ExecuteNonQuery();
            con.Close();
            if (x > 0)
            {
                grdBayCap.EditIndex = -1;
                lblMsg.ForeColor = Color.Green;
                lblMsg.Text = "Successfully updated!";
                BindBayCapGrid();
            }
        }
        catch (Exception ex)
        {
            lblMsg.ForeColor = Color.Red;
            lblMsg.Text = "Error while updating data!";
            BindBayCapGrid();
        }
    }

    protected void grdBayCap_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int rowindex = e.RowIndex;
            GridViewRow row = grdBayCap.Rows[rowindex];
            int BayTypeId = Convert.ToInt32(row.Cells[4].Text.ToString());
            string delStr = "Delete from TblBayType where BayTypeId=" + BayTypeId;
            SqlCommand cmdDel = new SqlCommand(delStr, con);
            con.Open();
            int x = cmdDel.ExecuteNonQuery();
            if (x > 0)
            {
                lblMsg.ForeColor = Color.Green;
                lblMsg.Text = "Successfully deleted";
                BindBayCapGrid();
            }
        }
        catch (Exception ex)
        {
            lblMsg.ForeColor = Color.Red;
            lblMsg.Text = "Error occured while deleting data";
            BindBayCapGrid();
        }
    }


    private void FillBackColor()
    {
        try
        {
            if (con.State == ConnectionState.Closed)
                con.Open();

            SqlDataAdapter da = new SqlDataAdapter("udpFetchBayBackColor", con);            
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txtColor.Text = dt.Rows[0][0].ToString().Replace("#", "").Trim();
            }
            else
            {
                txtColor.Text = "";
            }
        }
        catch (Exception ex)
        {            
        }
        finally
        {
            if (con.State == ConnectionState.Open)
                con.Close();
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (txtColor.Text != "" && txtColor.Text.ToString().Trim().Length == 6)
        {
            try
            {
                lblColor.Text = "";

                if (con.State == ConnectionState.Closed)
                    con.Open();

                SqlCommand cmdAdd = new SqlCommand("udpBayBackColor", con);
                cmdAdd.CommandType = CommandType.StoredProcedure;
                cmdAdd.Parameters.AddWithValue("@ColorCode", txtColor.Text.ToString().Trim());
                cmdAdd.ExecuteNonQuery();
                lblColor.ForeColor = Color.Green;
                lblColor.Text = "Successfully Updated !";
            }
            catch (Exception ex)
            {
                lblColor.ForeColor = Color.Red;
                lblColor.Text = "Error! Data not updated";
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }
        else
        {
            lblColor.ForeColor = Color.Red;
            lblColor.Text = "Please Select Color. !";
        }
    }
}