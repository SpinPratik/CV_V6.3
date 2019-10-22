using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

public class Database
{
    private SqlConnection scon = new SqlConnection(ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString().Trim());
    private SqlCommand scom;

    private void Connect()
    {
        if (scon.State == ConnectionState.Closed)
        {
            scon.Open();
        }
    }

    private void Disconnect()
    {
        if (scon.State == ConnectionState.Open)
        {
            scon.Close();
        }
    }

    public string Insert_JobCodeMaster(DataSet tables)
    {
        Connect();
        //var result="";

        try
        {
            //if (tables.Tables["PARENT_PRODUCT_LINE"] == null)
            //               var result = (from jobcode in tables.Tables["S_PROD_INT"].AsEnumerable()
            //                  join sthrs in tables.Tables["STANDARD_LABOUR_HRS"].AsEnumerable() on jobcode.Field<int>("S_PROD_INT_Id") equals sthrs.Field<int>("S_PROD_INT_Id")
            //                  join bhrs in tables.Tables["BILLING_HRS"].AsEnumerable() on jobcode.Field<int>("S_PROD_INT_Id") equals bhrs.Field<int>("S_PROD_INT_Id")
            //                  join part in tables.Tables["PARTS_ESTIMATE"].AsEnumerable() on jobcode.Field<int>("S_PROD_INT_Id") equals part.Field<int>("S_PROD_INT_Id")
            //                  join aggregate in tables.Tables["AGGREGATE"].AsEnumerable() on jobcode.Field<int>("S_PROD_INT_Id") equals aggregate.Field<int>("S_PROD_INT_Id")
            //                  join jobcodetype in tables.Tables["JOB_CODE_TYPE"].AsEnumerable() on jobcode.Field<int>("S_PROD_INT_Id") equals jobcodetype.Field<int>("S_PROD_INT_Id")
            //                  join joballowed in tables.Tables["MAXIMUM_JOBS_ALLOWED"].AsEnumerable() on jobcode.Field<int>("S_PROD_INT_Id") equals joballowed.Field<int>("S_PROD_INT_Id")
            //                  join eallowed in tables.Tables["EXTRACHARGES_ALLOWED"].AsEnumerable() on jobcode.Field<int>("S_PROD_INT_Id") equals eallowed.Field<int>("S_PROD_INT_Id")
            //                  select new
            //                  {
            //                      ROW_ID = jobcode.Field<string>("ROW_ID"),
            //                      PPL = jobcode.Field<string>("PARENT_PRODUCT_LINE"),
            //                      JOBCODE = jobcode.Field<string>("JOB_CODE"),
            //                      JOBDESC = jobcode.Field<string>("JOB_CODE_DESC"),
            //                      STHRS = sthrs.Field<string>("STANDARD_LABOUR_HRS_Text"),
            //                      BHRS = bhrs.Field<string>("BILLING_HRS_Text"),
            //                      PARTESTIMATE = part.Field<string>("PARTS_ESTIMATE_Text"),
            //                      TYPE = jobcode.Field<string>("TYPE"),
            //                      STATUS = jobcode.Field<string>("STATUS"),
            //                      AGGREGATE = aggregate.Field<string>("AGGREGATE_Text"),
            //                      JOBCODETYPE = jobcodetype.Field<string>("JOB_CODE_TYPE_Text"),
            //                      MAXJOBALLOWED = joballowed.Field<string>("MAXIMUM_JOBS_ALLOWED_Text"),
            //                      EXTRACHARGEALLOWED = eallowed.Field<string>("EXTRACHARGES_ALLOWED_Text"),
            //                  }).ToList();

            //else

            var result = (from jobcode in tables.Tables["S_PROD_INT"].AsEnumerable()
                          join ppl in tables.Tables["PARENT_PRODUCT_LINE"].AsEnumerable() on jobcode.Field<int>("S_PROD_INT_Id") equals ppl.Field<int>("S_PROD_INT_Id")
                          join sthrs in tables.Tables["STANDARD_LABOUR_HRS"].AsEnumerable() on jobcode.Field<int>("S_PROD_INT_Id") equals sthrs.Field<int>("S_PROD_INT_Id")
                          join bhrs in tables.Tables["BILLING_HRS"].AsEnumerable() on jobcode.Field<int>("S_PROD_INT_Id") equals bhrs.Field<int>("S_PROD_INT_Id")
                          join part in tables.Tables["PARTS_ESTIMATE"].AsEnumerable() on jobcode.Field<int>("S_PROD_INT_Id") equals part.Field<int>("S_PROD_INT_Id")
                          join aggregate in tables.Tables["AGGREGATE"].AsEnumerable() on jobcode.Field<int>("S_PROD_INT_Id") equals aggregate.Field<int>("S_PROD_INT_Id")
                          join jobcodetype in tables.Tables["JOB_CODE_TYPE"].AsEnumerable() on jobcode.Field<int>("S_PROD_INT_Id") equals jobcodetype.Field<int>("S_PROD_INT_Id")
                          join joballowed in tables.Tables["MAXIMUM_JOBS_ALLOWED"].AsEnumerable() on jobcode.Field<int>("S_PROD_INT_Id") equals joballowed.Field<int>("S_PROD_INT_Id")
                          join eallowed in tables.Tables["EXTRACHARGES_ALLOWED"].AsEnumerable() on jobcode.Field<int>("S_PROD_INT_Id") equals eallowed.Field<int>("S_PROD_INT_Id")
                          select new
                          {
                              ROW_ID = jobcode.Field<string>("ROW_ID"),
                              PPL = ppl.Field<string>("PARENT_PRODUCT_LINE_Text"),
                              JOBCODE = jobcode.Field<string>("JOB_CODE"),
                              JOBDESC = jobcode.Field<string>("JOB_CODE_DESC"),
                              STHRS = sthrs.Field<string>("STANDARD_LABOUR_HRS_Text"),
                              BHRS = bhrs.Field<string>("BILLING_HRS_Text"),
                              PARTESTIMATE = part.Field<string>("PARTS_ESTIMATE_Text"),
                              TYPE = jobcode.Field<string>("TYPE"),
                              STATUS = jobcode.Field<string>("STATUS"),
                              AGGREGATE = aggregate.Field<string>("AGGREGATE_Text"),
                              JOBCODETYPE = jobcodetype.Field<string>("JOB_CODE_TYPE_Text"),
                              MAXJOBALLOWED = joballowed.Field<string>("MAXIMUM_JOBS_ALLOWED_Text"),
                              EXTRACHARGEALLOWED = eallowed.Field<string>("EXTRACHARGES_ALLOWED_Text"),
                          }).ToList();

            for (int i = 0; i < result.Count; i++)
            {
                scom = new SqlCommand("udpJCInsertJobCodes", scon);
                scom.CommandType = CommandType.StoredProcedure;
                try
                {
                    scom.Parameters.AddWithValue("@RowId", result[i].ROW_ID.ToString());
                }
                catch (Exception ex)
                {
                    scom.Parameters.AddWithValue("@RowId", DBNull.Value);
                }

                try
                {
                    scom.Parameters.AddWithValue("@ParentProductLine", result[i].PPL.ToString());
                }
                catch (Exception ex)
                {
                    scom.Parameters.AddWithValue("@ParentProductLine", DBNull.Value);
                }

                try
                {
                    scom.Parameters.AddWithValue("@JobCode", result[i].JOBCODE.ToString());
                }
                catch (Exception ex)
                {
                    scom.Parameters.AddWithValue("@JobCode", DBNull.Value);
                }

                try
                {
                    scom.Parameters.AddWithValue("@JobCodeDesc", result[i].JOBDESC.ToString());
                }
                catch (Exception ex)
                {
                    scom.Parameters.AddWithValue("@JobCodeDesc", DBNull.Value);
                }

                try
                {
                    scom.Parameters.AddWithValue("@StandardLabourHrs", result[i].STHRS.ToString());
                }
                catch (Exception ex)
                {
                    scom.Parameters.AddWithValue("@StandardLabourHrs", DBNull.Value);
                }

                try
                {
                    scom.Parameters.AddWithValue("@BillingHrs", result[i].BHRS.ToString());
                }
                catch (Exception ex)
                {
                    scom.Parameters.AddWithValue("@BillingHrs", DBNull.Value);
                }

                try
                {
                    scom.Parameters.AddWithValue("@PartsEstimate", result[i].PARTESTIMATE.ToString());
                }
                catch (Exception ex)
                {
                    scom.Parameters.AddWithValue("@PartsEstimate", DBNull.Value);
                }

                try
                {
                    scom.Parameters.AddWithValue("@Type", result[i].TYPE.ToString());
                }
                catch (Exception ex)
                {
                    scom.Parameters.AddWithValue("@Type", DBNull.Value);
                }

                try
                {
                    scom.Parameters.AddWithValue("@Status", result[i].STATUS.ToString());
                }
                catch (Exception ex)
                {
                    scom.Parameters.AddWithValue("@Status", DBNull.Value);
                }

                try
                {
                    scom.Parameters.AddWithValue("@Aggregate", result[i].AGGREGATE.ToString());
                }
                catch (Exception ex)
                {
                    scom.Parameters.AddWithValue("@Aggregate", DBNull.Value);
                }

                try
                {
                    scom.Parameters.AddWithValue("@JobCodeType", result[i].JOBCODETYPE.ToString());
                }
                catch (Exception ex)
                {
                    scom.Parameters.AddWithValue("@JobCodeType", DBNull.Value);
                }

                try
                {
                    scom.Parameters.AddWithValue("@MaxJobAllowed", result[i].MAXJOBALLOWED.ToString());
                }
                catch (Exception ex)
                {
                    scom.Parameters.AddWithValue("@MaxJobAllowed", DBNull.Value);
                }

                try
                {
                    scom.Parameters.AddWithValue("@ExtraChrageAllowed", result[i].EXTRACHARGEALLOWED.ToString());
                }
                catch (Exception ex)
                {
                    scom.Parameters.AddWithValue("@ExtraChrageAllowed", DBNull.Value);
                }

                scom.Parameters.AddWithValue("@LabourType", DBNull.Value);
                scom.Parameters.AddWithValue("@BU", DBNull.Value);

                scom.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            return "Please check after 10 mins, if still issue persist raise the SR to CRM Support Team";
        }
        Disconnect();
        return "Job Code Details Successfully Updated. !";
    }

    public string Insert_PriceListItemsMaster(DataSet tables)
    {
        Connect();
        try
        {
            scom = new SqlCommand("Delete from tbl_PriceListItem", scon);
            scom.ExecuteNonQuery();
            for (int i = 0; i < tables.Tables["S_PRI_LST_ITEM"].Rows.Count; i++)
            {
                scom = new SqlCommand("udpJCInsertPriceListItem", scon);
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@RowId", tables.Tables["S_PRI_LST_ITEM"].Rows[i]["PRI_LST_ID"].ToString());
                scom.Parameters.AddWithValue("@Description", tables.Tables["S_PRI_LST_ITEM"].Rows[i]["NAME"].ToString());
                scom.Parameters.AddWithValue("@StdRateHour", tables.Tables["S_PRI_LST_ITEM"].Rows[i]["STD_PRI_UNIT"].ToString());
                scom.Parameters.AddWithValue("@PremiumRateHour", tables.Tables["S_PRI_LST_ITEM"].Rows[i]["PREMIUM_PRI_AMT"].ToString());
                scom.Parameters.AddWithValue("@ContractRateHour", tables.Tables["S_PRI_LST_ITEM"].Rows[i]["STD_CNTRCT_PRI_AMT"].ToString());
                scom.Parameters.AddWithValue("@PLRowId", tables.Tables["S_PRI_LST_ITEM"].Rows[i]["ROW_ID"].ToString());
                scom.Parameters.AddWithValue("@ProdId", tables.Tables["S_PRI_LST_ITEM"].Rows[i]["PROD_ID"].ToString());
                scom.Parameters.AddWithValue("@ProdName", tables.Tables["S_PRI_LST_ITEM"].Rows[i]["PROD_NAME"].ToString());
                scom.Parameters.AddWithValue("@RLName", tables.Tables["S_PRI_LST_ITEM"].Rows[i]["RLNAME"].ToString());
                scom.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            return "Please check after 10 mins, if still issue persist raise the SR to CRM Support Team";
        }
        Disconnect();
        return "Price List Details Successfully Updated. !";
    }

    public string Insert_tRateListByBuIdMaster(DataSet tables)
    {
        Connect();
        try
        {
            var result = (from sprilst in tables.Tables["S_PRI_LST"].AsEnumerable()
                          join effenddt in tables.Tables["EFF_END_DT"].AsEnumerable() on sprilst.Field<int>("S_PRI_LST_Id") equals effenddt.Field<int>("S_PRI_LST_Id")
                          join descText in tables.Tables["DESC_TEXT"].AsEnumerable() on sprilst.Field<int>("S_PRI_LST_Id") equals descText.Field<int>("S_PRI_LST_Id")
                          select new
                          {
                              NAME = sprilst.Field<string>("NAME"),
                              ROW_ID = sprilst.Field<string>("ROW_ID"),
                              DESC_TEXT = descText.Field<string>("DESC_TEXT_Text"),
                              EFF_START_DT = sprilst.Field<string>("EFF_START_DT"),
                              EFF_END_DT = effenddt.Field<string>("EFF_END_DT_Text"),
                          }).ToList();

            scom = new SqlCommand("Delete from tbl_RateList", scon);
            scom.ExecuteNonQuery();

            for (int i = 0; i < result.Count; i++)
            {
                scom = new SqlCommand("udpJCInsertRateList", scon);
                scom.CommandType = CommandType.StoredProcedure;
                scom.Parameters.AddWithValue("@RowId", result[i].ROW_ID.ToString());
                scom.Parameters.AddWithValue("@RateList", result[i].NAME.ToString());
                try
                {
                    scom.Parameters.AddWithValue("@Description", result[i].DESC_TEXT.ToString());
                }
                catch (Exception ex)
                {
                    scom.Parameters.AddWithValue("@Description", DBNull.Value);
                }
                try
                {
                    scom.Parameters.AddWithValue("@EffectiveFrom", result[i].EFF_START_DT.ToString());
                }
                catch (Exception ex)
                {
                    scom.Parameters.AddWithValue("@EffectiveFrom", DBNull.Value);
                }
                try
                {
                    scom.Parameters.AddWithValue("@EffectiveTo", result[i].EFF_END_DT.ToString());
                }
                catch (Exception ex)
                {
                    scom.Parameters.AddWithValue("@EffectiveTo", DBNull.Value);
                }
                scom.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            return "Please check after 10 mins, if still issue persist raise the SR to CRM Support Team";
        }
        Disconnect();
        return "Rate List Details Successfully Updated. !";
    }

    public string GetRateListIDS()
    {
        Connect();
        try
        {
            SqlDataAdapter sda = new SqlDataAdapter("udpJCFetchRateList", scon);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
                return dt.Rows[0][0].ToString();
            else
                return "";
        }
        catch (Exception ex)
        {
            return "";
        }
        finally
        {
            Disconnect();
        }
    }
}