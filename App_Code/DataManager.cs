using CloudinaryDotNet;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for DataManager
/// </summary>
///

public static class DataManager
{
   
    static public DataTable GetCustomerDetails()
    {
        string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        SqlConnection oConn = new SqlConnection(sConnString);
        SqlCommand cmd = new SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "GetCustomerDetails";
        cmd.Parameters.Clear();

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    //public static string GetDealerDetails()
    //{
    //    try
    //    {
    //        string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
    //        SqlConnection con = new SqlConnection(sConnString);
    //        DataTable dt = new DataTable();
    //        SqlCommand cmd = new SqlCommand();
    //        cmd.CommandText = "udpGetDealerDetails";
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.Connection = con;

    //        SqlDataAdapter da = new SqlDataAdapter(cmd);
    //        da.Fill(dt);
    //        if (dt.Rows.Count != 0)
    //            return dt.Rows[0][0].ToString();
    //        else
    //            return "-Dealer Name, Place-";
    //    }
    //    catch (Exception ex)
    //    {
    //        return ">Dealer Name, Place<";
    //    }
    //}

    public static string car_image(string name)
    {
        Account account = new Account("deekyp5bi", "215538712761161", "RYfkzjckHetqxXQ1f0yV_Pbw2SM");
        Cloudinary cloudinary = new Cloudinary(account);
            // cloudinary.Api.UrlImgUp.Transform(new Transformation().Width(50).Crop("scale")).BuildImageTag("vehicles/Aria.png") %
            //cloudinary.Api.UrlImgUp.Transform(new Transformation().Width(33).Crop("fit")).BuildImageTag("vehicles/ZEST.jpg") px
            var value = cloudinary.Api.UrlImgUp.BuildImageTag("vehicles/" + name);
            string temp = Convert.ToString(value);
            temp = temp.Replace("<", "").Replace("img src=\"", "").Replace("\"/>", "");
        return temp;
     
    }
    
 public static string jcr_image(string name)
    {
        Account account = new Account("deekyp5bi", "215538712761161", "RYfkzjckHetqxXQ1f0yV_Pbw2SM");
        Cloudinary cloudinary = new Cloudinary(account);
        // cloudinary.Api.UrlImgUp.Transform(new Transformation().Width(50).Crop("scale")).BuildImageTag("vehicles/Aria.png") %
        //cloudinary.Api.UrlImgUp.Transform(new Transformation().Width(33).Crop("fit")).BuildImageTag("vehicles/ZEST.jpg") px
        var value = cloudinary.Api.UrlImgUp.BuildImageTag("jcr/" + name);
        string temp = Convert.ToString(value);

        temp = temp.Replace("<", "").Replace("img src=\"", "").Replace("\"/>", "");
        return temp;
    }

    public static string GetDealerDetails(string Connection)
    {
        try
        {
            SqlConnection con = new SqlConnection(Connection);
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "udpGetDealerDetails";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count != 0)
                return dt.Rows[0][0].ToString();
            else
                return "-Dealer Name, Place-";
        }
        catch (Exception ex)
        {
            return ">Dealer Name, Place<";
        }
    }
    public static DataTable getInfo(string type)
    {
        try
        {
            string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
            SqlConnection con = new SqlConnection(sConnString);
            SqlCommand cmd = new SqlCommand("udpGetheaderDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Bodyshop", Convert.ToString(type));
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return new DataTable();
        }
    }

    public static DataTable getDealerData(string bodyshop)
    {
        try
        {
            string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
            SqlConnection con = new SqlConnection(sConnString);
            SqlCommand cmd = new SqlCommand("GetDealerDashboard2", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Bodyshop", Convert.ToString(bodyshop));
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return new DataTable();
        }
    }

    static public DataTable getAlertTemplates(string AlertType)
    {
        string alerttemplate = string.Empty;
        DataTable dt = new DataTable();
        try
        {
            string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
            SqlConnection con = new SqlConnection(sConnString);
            string str = "select SlNo, AlertHeader,AlertTemplate from tblAlertLog where processed=0 and AlertType='" + AlertType + "'";
            SqlDataAdapter ad = new SqlDataAdapter(str, con);
            ad.Fill(dt);
            return dt;
        }
        catch
        {
            return dt;
        }
    }

    static public string getVersion()
    {
        string version = "";
        string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        SqlConnection con = new SqlConnection(sConnString);
        SqlDataAdapter sda = new SqlDataAdapter("select top 1 * from ProductVersion order by DTM desc", con);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        if (dt.Rows.Count > 0)
            version = dt.Rows[0][0].ToString();
        return version;
    }

    public static int GetJobSlipSLNO(string card)
    {
        SqlConnection con = new SqlConnection();
        try
        {
            OpenDBConnection(ref con);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT MAX(Slno) FROM tblMaster WHERE (RFID = " + card.ToString() + ") AND (Delivered = 0) AND (Cancelation = 0)", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                return int.Parse(dt.Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }
        catch (Exception ex)
        {
            return 0;
        }
        finally
        {
            CloseConnection(ref con);
        }
    }

    public static void InsertWorkShopTask(int Refid, int TaskId)
    {
        SqlConnection con = new SqlConnection();
        try
        {
            OpenDBConnection(ref con);
            SqlCommand cmd = new SqlCommand("InsertWorkShopTasks", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RefId ", Refid);
            cmd.Parameters.AddWithValue("@TaskId ", TaskId);

            int I = cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            CloseConnection(ref con);
        }
    }

    public static void UpdateWorkshopTaskTime(int RefId, int ProcTime)
    {
        SqlConnection con = new SqlConnection();
        try
        {
            OpenDBConnection(ref con);
            SqlCommand cmd = new SqlCommand("UPDATE tblMaster SET ProcessTime = " + ProcTime + " where Slno = " + RefId, con);
            int I = cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            CloseConnection(ref con);
        }
    }

    public static void DeleteWorkshopTaskTime(int RefId)
    {
        SqlConnection con = new SqlConnection();
        try
        {
            OpenDBConnection(ref con);
            SqlCommand cmd = new SqlCommand("Delete from tblMasterTask where Refid = " + RefId, con);
            int I = cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            CloseConnection(ref con);
        }
    }

    public static int GetManualTime(int RefId)
    {
        SqlConnection con = new SqlConnection();
        OpenDBConnection(ref con);
        try
        {
            SqlDataAdapter sda = new SqlDataAdapter("SELECT ProcessTime from tblMaster where Slno= " + RefId, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                return int.Parse(dt.Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }
        catch (Exception ex)
        {
            return 0;
        }
        finally
        {
            CloseConnection(ref con);
        }
    }

    public static DataTable GetSelectTask(int RefId)
    {
        SqlConnection con = new SqlConnection();
        OpenDBConnection(ref con);
        DataTable dt = new DataTable();
        try
        {
            SqlDataAdapter sda = new SqlDataAdapter("SELECT tblMasterTask.Taskid, tblTask.TaskName FROM tblMasterTask INNER JOIN tblTask ON tblMasterTask.Taskid = tblTask.TaskId WHERE (tblMasterTask.Refid = " + RefId + ")", con);
            sda.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
        finally
        {
            CloseConnection(ref con);
        }
    }

    static public DataTable GetEmployeeReportDataTable(DateTime DtFrom, DateTime DtTo, string emptype, string floorname, string TLId, string IsBodyshop, string StProc)
    {
        string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        SqlConnection oConn = new SqlConnection(sConnString);
        SqlCommand cmd = new SqlCommand("", oConn);
        if (oConn.State != ConnectionState.Open)
            oConn.Open();
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = StProc;
        cmd.Parameters.AddWithValue("@Datefrom", DtFrom);
        //cmd.Parameters.AddWithValue("@Dateto", DtTo);
        cmd.Parameters.AddWithValue("@EmployeeType", emptype);
        cmd.Parameters.AddWithValue("@IsBodyshop", IsBodyshop);
        cmd.Parameters.AddWithValue("@VehicleStatus", 2);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    static public DataTable GetEmployeeReportDataTable_EMPTMR(DateTime DtFrom, DateTime DtTo, string emptype, string floorname, string TLId, string IsBodyshop, string StProc, int VehicleStatus,string ConnectionString)
    {
       // string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        SqlConnection oConn = new SqlConnection(ConnectionString);
        SqlCommand cmd = new SqlCommand("", oConn);
        if (oConn.State != ConnectionState.Open)
            oConn.Open();
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = StProc;
        cmd.Parameters.AddWithValue("@Datefrom", DtFrom);
        //cmd.Parameters.AddWithValue("@Dateto", DtTo);
        cmd.Parameters.AddWithValue("@EmployeeType", emptype);
        cmd.Parameters.AddWithValue("@IsBodyshop", IsBodyshop);
        cmd.Parameters.AddWithValue("@VehicleStatus", VehicleStatus);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    static public DataTable GetTechnicianReport()
    {
        string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        SqlConnection oConn = new SqlConnection(sConnString);
        SqlCommand cmd = new SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "GetTechReport";
        cmd.Parameters.Clear();

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }


    public static String ConStr = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();

    public static int GetRefNo(string RegNo, string Connectionstring)
    {
        SqlConnection con = new SqlConnection(Connectionstring);
        //OpenDBConnection(ref con);

        string cmdstr = "SELECT top (1) Slno from tblMaster where RegNo = @carnum AND Position <> 'Delivered' order by slno desc";
        SqlCommand cmd = new SqlCommand(cmdstr, con);
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@carnum", RegNo.ToUpper());
        try
        {
            int slno = 0;
            con.Open();
            slno = Convert.ToInt32(cmd.ExecuteScalar());
            return slno;
        }
        catch (Exception ex)
        {
            return 0;
        }
        finally
        {
            CloseConnection(ref con);
            con.Close();
        }
    }

    public static string GetRegNo(int RefNo)
    {
        SqlConnection con = new SqlConnection();
        OpenDBConnection(ref con);

        string cmdstr = "SELECT top (1) RegNo from tblMaster WHERE slno =" + RefNo.ToString();// @RefNo";
        SqlCommand cmd = new SqlCommand(cmdstr, con);
        cmd.Parameters.Clear();
        try
        {
            string RegNo = string.Empty;
            RegNo = cmd.ExecuteScalar().ToString();
            return RegNo;
        }
        catch (Exception ex)
        {
            return string.Empty;
        }
        finally
        {
            CloseConnection(ref con);
        }
    }

    public static String GetModel(string RegNo)
    {
        SqlConnection con = new SqlConnection();
        OpenDBConnection(ref con);

        string cmdstr = "SELECT top (1) Model from tblCustomer WHERE RegNo= @carnum ";
        SqlCommand cmd = new SqlCommand(cmdstr, con);
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@carnum", RegNo);
        try
        {
            String Model = string.Empty;
            Model = cmd.ExecuteScalar().ToString();
            return Model;
        }
        catch (Exception ex)
        {
            return string.Empty;
        }
        finally
        {
            CloseConnection(ref con);
        }
    }

    public static bool GetJobCardTime(int RefNo)
    {
        SqlConnection con = new SqlConnection(ConStr);
        SqlCommand cmd = new SqlCommand("", con);
        SqlDataReader dr = null;
        DateTime retval;
        try
        {
            cmd.CommandText = "SELECT JobCardIn FROM tblMaster WHERE Slno=@RefNo";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@RefNo", RefNo);
            OpenDBConnection(ref con);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                retval = Convert.ToDateTime(dr["JobCardIn"]);
                return true;
            }
            else
                return false;
        }
        catch (Exception ex)
        {
            return false;
        }
        finally
        {
            CloseConnection(ref con);
        }
    }

    public static void OpenDBConnection(ref SqlConnection Connection)
    {
        string constr = ConStr;
        if (Connection == null)
        {
            Connection = new SqlConnection(constr);
        }
        else
            Connection.ConnectionString = constr;

        try
        {
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }
        }
        catch (Exception ex)
        {
        }
    }

    public static void CloseConnection(ref SqlConnection Connection)
    {
        try
        {
            if (Connection.State == ConnectionState.Open)
            {
                Connection.Close();
            }
        }
        catch (Exception ex)
        {
        }
    }

    public static void UpdatetblProcess(int RefNo, String SAName, String SAPhNo, String ServiceType, int ServiceTime, DateTime PromisedTime, bool APlus)
    {
        SqlConnection con = new SqlConnection(DataManager.ConStr);

        try
        {
            OpenDBConnection(ref con);
            SqlCommand cmd = new SqlCommand("", con);
            cmd.CommandText = "UPDATE tblProcess SET SAName=@SAName, SAPhoneNo=@SAPhoneNo,ServiceType=@ServiceType,ServiceTime=@ServiceTime,WS1DT=@WS1DT,PromisedTime=@PromisedTime ,APlus=@APlus WHERE RefNo=@RefNo";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@SAName", SAName);
            cmd.Parameters.AddWithValue("@SAPhoneNo", SAPhNo);
            cmd.Parameters.AddWithValue("@ServiceType", ServiceType);
            cmd.Parameters.AddWithValue("@ServiceTime", ServiceTime);
            cmd.Parameters.AddWithValue("@WS1DT", -ServiceTime);
            cmd.Parameters.AddWithValue("@PromisedTime", PromisedTime);
            cmd.Parameters.AddWithValue("@RefNo", RefNo);
            cmd.Parameters.AddWithValue("@APlus", APlus);
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            CloseConnection(ref con);
        }
    }

    public static int updateColumnMaster2(int RefNo, string UpdateColumnName, object Value)
    {
        SqlConnection con = new SqlConnection(ConStr);
        try
        {
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand(@"UPDATE TblMaster SET " + UpdateColumnName + "=@Values WHERE Slno=@RefNo", con);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Values", Value);
            cmd.Parameters.AddWithValue("@RefNo", RefNo);
            return (Convert.ToInt32(cmd.ExecuteNonQuery()));
        }
        catch (Exception ex)
        {
            return -1;
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
            {
                con.Close();
            }
        }
    }

    public static object GetColMaster(int RefNo, String ColName)
    {
        SqlConnection con = new SqlConnection(ConStr);
        object RetVal;
        try
        {
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand(@"SELECT TOP 1 " + ColName + " FROM TblMaster WHERE SlNo=@RefNo", con);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@RefNo", RefNo);
            RetVal = cmd.ExecuteScalar();
            if (RetVal == DBNull.Value)
                return null;
            else
                return Convert.ToDateTime(RetVal);
        }
        catch (Exception ex)
        {
            return null;
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
            {
                con.Close();
            }
        }
    }

    public static object GetColMasterObj(int RefNo, String ColName)
    {
        SqlConnection con = new SqlConnection(ConStr);
        object RetVal;
        try
        {
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand(@"SELECT TOP 1 " + ColName + " FROM TblMaster WHERE SlNo=@RefNo", con);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@RefNo", RefNo);
            RetVal = cmd.ExecuteScalar();
            if (RetVal == DBNull.Value)
                return null;
            else
                return RetVal;
        }
        catch (Exception ex)
        {
            return null;
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
            {
                con.Close();
            }
        }
    }

    public static void UpdateTblMasterjcAdd(int refNo, int Aplus, int IsMajor, int QuickRepair, int BodyShop, string SAName, string SAPhoneNo, string ServiceType, DateTime PT, int WSTime, bool CustomerWaiting, String Estimate, String KMS, String RONumber, string ServiceDesc)
    {
        SqlConnection con = new SqlConnection();
        try
        {
            OpenDBConnection(ref con);
            string cmstr = "UPDATE tblMaster SET Aplus = @aplus,IsMajor=@IsMajor, QuickRepair= @QuickRepair, BodyShop=@BodyShop, ServiceAdvisor = @saname,JobCardIn = @jcinTime, SAPhone=@saphoneno,ServiceType=@servicetype,PromisedTime=@pt,WorkshopTime=@wstime,CustomerWaiting=@CustomerWaiting,KMS=@KMS,Estimate=@Estimate, JobCardNo=@RONumber,RevisitDesc=@RevisitDesc WHERE Slno = @refno";
            SqlCommand cmd = new SqlCommand(cmstr, con);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@refno", refNo);
            cmd.Parameters.AddWithValue("@Aplus", Aplus);
            cmd.Parameters.AddWithValue("@IsMajor", IsMajor);
            cmd.Parameters.AddWithValue("@QuickRepair", QuickRepair);
            cmd.Parameters.AddWithValue("@BodyShop", BodyShop);
            cmd.Parameters.AddWithValue("@saname", SAName);
            cmd.Parameters.AddWithValue("@saphoneno", SAPhoneNo);
            cmd.Parameters.AddWithValue("@jcinTime", DateTime.Now);
            cmd.Parameters.AddWithValue("@servicetype", ServiceType);
            cmd.Parameters.AddWithValue("@pt", PT);
            cmd.Parameters.AddWithValue("@wstime", WSTime);
            cmd.Parameters.AddWithValue("@CustomerWaiting", CustomerWaiting);
            cmd.Parameters.AddWithValue("@KMS", KMS);
            cmd.Parameters.AddWithValue("@Estimate", Estimate);
            cmd.Parameters.AddWithValue("@RONumber", RONumber);
            cmd.Parameters.AddWithValue("@RevisitDesc", ServiceDesc);

            int I = cmd.ExecuteNonQuery();
            UpdatetblProcess(refNo, SAName, SAPhoneNo, ServiceType, WSTime, PT, (Aplus == 0) ? false : true);
            UpdateTblJCAlertUpdateProcessed(refNo, RONumber, ServiceType);
        }
        catch (Exception ex)
        {
        }
        finally
        {
            CloseConnection(ref con);
        }
    }

    public static void UpdateTblJCAlertUpdateProcessed(int refNo, String RONumber, String ServiceType)
    {
        SqlConnection con = new SqlConnection();
        try
        {
            OpenDBConnection(ref con);
            string cmstr = "UPDATE tblJCAlert SET Processed =1 and RONumber=@RONumber And ServiceType=@ServiceType WHERE Slno = @refno";
            SqlCommand cmd = new SqlCommand(cmstr, con);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@RONumber", refNo);
            cmd.Parameters.AddWithValue("@ServiceType", refNo);
            cmd.Parameters.AddWithValue("@refno", refNo);
            int I = cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            CloseConnection(ref con);
        }
    }

    public static void UpdateTblMasterROUpdated(int refNo, DateTime PT, String Estimate)
    {
        SqlConnection con = new SqlConnection();
        try
        {
            OpenDBConnection(ref con);
            string cmstr = "UPDATE tblMaster SET PromisedTime=@pt,Estimate=@Estimate WHERE Slno = @refno";
            SqlCommand cmd = new SqlCommand(cmstr, con);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@refno", refNo);
            cmd.Parameters.AddWithValue("@pt", PT);
            cmd.Parameters.AddWithValue("@Estimate", Estimate);
            int I = cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            CloseConnection(ref con);
        }
    }

    public static String GetRevisit(String RegNo)
    {
        SqlConnection con = new SqlConnection(ConStr);

        SqlCommand cmd = new SqlCommand("", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        cmd.CommandText = "select dbo.GetRepeated(@RegNo)";
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@RegNo", RegNo);
        DataTable dt = new DataTable();
        da.Fill(dt);
        try
        {
            if (dt.Rows.Count != 0)
                return dt.Rows[0][0].ToString();
        }
        catch
        {
            return "";
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
        return "";
    }

    public static string GetSAPhoneNumber(string SAName)
    {
        SqlConnection con = new SqlConnection();
        try
        {
            OpenDBConnection(ref con);
            string cmdstr = "SELECT SAPhone FROM tblServiceAdvisor WHERE SAName = @saname";
            SqlCommand cmd = new SqlCommand(cmdstr, con);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@saname", SAName);
            string SAPhno = cmd.ExecuteScalar().ToString();
            if (SAPhno != string.Empty)
            {
                return SAPhno;
            }
            return "No PhoneNumber";
        }
        catch (Exception ex)
        {
            return "0";
        }
        finally
        {
            CloseConnection(ref con);
        }
    }

    public static int GetProcessValues(String Process)
    {
        SqlConnection con1 = new SqlConnection(ConStr);
        SqlCommand cmd1 = new SqlCommand("SELECT TimeValue FROM tblProcessValues WHERE ColName=@ColName", con1);
        cmd1.Parameters.Clear();
        cmd1.Parameters.AddWithValue("@ColName", Process);
        SqlDataAdapter da = new SqlDataAdapter(cmd1);
        DataTable dt = new DataTable();
        try
        {
            da.Fill(dt);
            if (dt != null)
                return Convert.ToInt32(dt.Rows[0][0].ToString());
            else
                return 0;
        }
        catch (Exception ex)
        {
            return 0;
        }
    }

    public static void UpdateMasterSTD(int RefNo, string ProcessName)
    {
        SqlConnection con = new SqlConnection();
        try
        {
            OpenDBConnection(ref con);
            string cmdstr = "UPDATE tblMaster SET " + ProcessName + "= @processtime WHERE Slno=@RefNo";
            SqlCommand cmd = new SqlCommand(cmdstr, con);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@processtime", DateTime.Now);
            cmd.Parameters.AddWithValue("@RefNo", RefNo);
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            CloseConnection(ref con);
        }
    }

    public static void UpdateRONumber(int RefNo, string RONumber)
    {
        SqlConnection con = new SqlConnection();
        try
        {
            OpenDBConnection(ref con);
            string cmdstr = "UPDATE tblMaster SET JobCardNO=@JCNo WHERE Slno=@RefNo";
            SqlCommand cmd = new SqlCommand(cmdstr, con);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@JCNo", RONumber);
            cmd.Parameters.AddWithValue("@RefNo", RefNo);
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            CloseConnection(ref con);
        }
    }

    public static int updateColumnProcess(int RefNo, string Position)
    {
        SqlConnection con = new SqlConnection(ConStr);
        try
        {
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand(@"UPDATE TblProcess SET  Position=@Position WHERE RefNo=@RefNo", con);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Position", Position);
            cmd.Parameters.AddWithValue("@RefNo", RefNo);
            return (Convert.ToInt32(cmd.ExecuteNonQuery()));
        }
        catch (Exception ex)
        {
            return 0;
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
            {
                con.Close();
            }
        }
    }

    public static void InsertTotblSMS(string Msg, string Num, string RegNo)
    {
        SqlConnection oConn = new SqlConnection(ConStr);
        string CmdString = "INSERT INTO tblSMS(Message,PhoneNumber,RegNo) VALUES(@Message,@Phone,@RegNo)";
        SqlCommand oCmd = new SqlCommand(CmdString, oConn);
        oCmd.Parameters.Clear();
        oCmd.Parameters.AddWithValue("@Message", Msg);
        oCmd.Parameters.AddWithValue("@phone", Num);
        oCmd.Parameters.AddWithValue("@RegNo", RegNo);
        try
        {
            if (oConn.State != ConnectionState.Open)
            {
                oConn.Open();
            }

            oCmd.ExecuteNonQuery();
        }
        catch (SqlException ex)
        {
        }
        finally
        {
            if (oConn.State == ConnectionState.Open)
                oConn.Close();
        }
    }

    public static string[] GetCustomerDetail(String RegNo)
    {
        SqlConnection con = new SqlConnection(ConStr);
        SqlCommand cmd = new SqlCommand("", con);
        cmd.CommandText = "SELECT Top 1 CustomerName,Customerphone FROM tblMaster WHERE RegNo=@RegNo order by Slno desc";
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@RegNo", RegNo);
        SqlDataReader dr = null;
        string[] cust = { string.Empty, string.Empty };
        try
        {
            if (con.State != ConnectionState.Open)
                con.Open();
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                cust[0] = dr["OwnerName"].ToString();
                cust[1] = dr["OwnerNumber"].ToString();
            }
            return cust;
        }
        catch
        {
            return cust;
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }

    public static void SendGreetings(String RegNo, String sMessage)
    {
        string[] cust = GetCustomerDetail(RegNo);
        if (cust[1] != string.Empty)
        {
            InsertTotblSMS("Dear " + cust[0] + "," + sMessage, cust[1], RegNo);//",Welcome to 'Dealer Name'");
        }
    }

    static public void FillYearCombo(ref DropDownList cmbYear)
    {
        cmbYear.Items.Clear();
        ListItem lstItem;
        for (int i = 2009; i < DateTime.Now.Year + 1; i++)
        {
            lstItem = new ListItem();
            lstItem.Text = i.ToString();
            lstItem.Value = i.ToString();
            cmbYear.Items.Add(lstItem);
        }
        cmbYear.SelectedValue = DateTime.Now.Year.ToString();
    }

    static public void FillMonthCombo(ref DropDownList cmbMonth)
    {
        cmbMonth.Items.Clear();
        ListItem lstItem;
        for (int i = 1; i < 13; i++)
        {
            lstItem = new ListItem();
            lstItem.Text = i.ToString();
            lstItem.Value = i.ToString();
            cmbMonth.Items.Add(lstItem);
        }
        cmbMonth.SelectedValue = DateTime.Now.Month.ToString();
    }

    static public void FillDayCombo(ref DropDownList cmbYear, ref DropDownList cmbMonth, ref DropDownList cmbDay)
    {
        try
        {
            cmbDay.Items.Clear();
            int NoDays;
            int YearSelected;
            int MonthSelected;
            YearSelected = Convert.ToInt16(cmbYear.SelectedValue);
            MonthSelected = Convert.ToInt16(cmbMonth.SelectedValue);
            NoDays = System.DateTime.DaysInMonth(YearSelected, MonthSelected);
            ListItem lstItem;
            for (int i = 1; i < NoDays + 1; i++)
            {
                lstItem = new ListItem();
                lstItem.Text = i.ToString();
                lstItem.Value = i.ToString();
                cmbDay.Items.Add(lstItem);
            }
            if (cmbMonth.SelectedValue == DateTime.Now.Month.ToString())
            {
                cmbDay.SelectedValue = DateTime.Now.Day.ToString();
            }
        }
        catch (Exception ex)
        {
        }
    }

    static public void FillFloorName(ref DropDownList cmbFloorNameFill)
    {
        cmbFloorNameFill.Items.Clear();
        string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        SqlConnection oConn = new SqlConnection(sConnString);

        try
        {
            if (oConn.State != ConnectionState.Open)
            {
                oConn.Open();
            }
            DataSet oDs = new DataSet();

            SqlCommand cmd = new SqlCommand("", oConn);
            cmd.CommandText = "udpGetFloorName";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter ODa = new SqlDataAdapter(cmd);
            ODa.Fill(oDs);

            ListItem lstItem;
            lstItem = new ListItem();
            lstItem.Text = "ALL";
            lstItem.Value = "-1";
            cmbFloorNameFill.Items.Add(lstItem);
            for (int i = 0; i < oDs.Tables[0].Rows.Count; i++)
            {
                lstItem = new ListItem();
                lstItem.Text = oDs.Tables[0].Rows[i]["DeviceName"].ToString();
                lstItem.Value = oDs.Tables[0].Rows[i]["DeviceName"].ToString();
                cmbFloorNameFill.Items.Add(lstItem);
            }
        }
        catch (Exception ex)
        {
        }
        finally
        {
            oConn.Close();
        }
    }

    static public void FillTeamLead(ref DropDownList cmbTeamLeadFill, string sConnStrings)
    {
        cmbTeamLeadFill.Items.Clear();
        //string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        SqlConnection oConn = new SqlConnection(sConnStrings);

        try
        {
            if (oConn.State != ConnectionState.Open)
            {
                oConn.Open();
            }
            DataSet oDs = new DataSet();

            SqlCommand cmd = new SqlCommand("", oConn);
            cmd.CommandText = "GetTeamLead";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter ODa = new SqlDataAdapter(cmd);
            ODa.Fill(oDs);

            ListItem lstItem;
            lstItem = new ListItem();
            lstItem.Text = "ALL";
            lstItem.Value = "-1";
            cmbTeamLeadFill.Items.Add(lstItem);
            for (int i = 0; i < oDs.Tables[0].Rows.Count; i++)
            {
                lstItem = new ListItem();
                lstItem.Text = oDs.Tables[0].Rows[i]["EmpName"].ToString();
                lstItem.Value = oDs.Tables[0].Rows[i]["EmpId"].ToString();
                cmbTeamLeadFill.Items.Add(lstItem);
            }
        }
        catch (Exception ex)
        {
        }
        finally
        {
            oConn.Close();
        }
    }

    static public void FillServiceAdvisor(ref DropDownList cmbToFill, string sConnString)
    {
        cmbToFill.Items.Clear();
        //string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        SqlConnection oConn = new SqlConnection(sConnString);

        try
        {
            if (oConn.State != ConnectionState.Open)
            {
                oConn.Open();
            }
            DataSet oDs = new DataSet();

            SqlCommand cmd = new SqlCommand("", oConn);
            cmd.CommandText = "GetServiceAdvisorList";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter ODa = new SqlDataAdapter(cmd);
            ODa.Fill(oDs);

            ListItem lstItem;
            lstItem = new ListItem();
            lstItem.Text = "ALL";
            lstItem.Value = "-1";
            cmbToFill.Items.Add(lstItem);
            for (int i = 0; i < oDs.Tables[0].Rows.Count; i++)
            {
                lstItem = new ListItem();
                lstItem.Text = oDs.Tables[0].Rows[i]["EmpName"].ToString();
                lstItem.Value = oDs.Tables[0].Rows[i]["EmpName"].ToString();
                cmbToFill.Items.Add(lstItem);
            }
        }
        catch (Exception ex)
        {
        }
        finally
        {
            oConn.Close();
        }
    }

    static public void FillTechnicianNamesNames(ref DropDownList cmbToFill)
    {
        cmbToFill.Items.Clear();
        string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        SqlConnection oConn = new SqlConnection(sConnString);
        if (oConn.State != ConnectionState.Open)
        {
            oConn.Open();
        }
        try
        {
            DataSet oDs = new DataSet();

            SqlDataAdapter ODa = new SqlDataAdapter("select Distinct TechnicianName FROM tblEmployee", oConn);
            ODa.Fill(oDs);

            ListItem lstItem;
            lstItem = new ListItem();
            lstItem.Text = "ALL";
            lstItem.Value = "ALL";
            cmbToFill.Items.Add(lstItem);
            for (int i = 0; i < oDs.Tables[0].Rows.Count; i++)
            {
                lstItem = new ListItem();
                lstItem.Text = oDs.Tables[0].Rows[i]["EmployeeName"].ToString();
                lstItem.Value = oDs.Tables[0].Rows[i]["EmployeeName"].ToString();
                cmbToFill.Items.Add(lstItem);
            }
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    static public void FillWorksManager(ref DropDownList cmbToFill)
    {
        cmbToFill.Items.Clear();
        string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        SqlConnection oConn = new SqlConnection(sConnString);

        try
        {
            if (oConn.State != ConnectionState.Open)
            {
                oConn.Open();
            }
            DataSet oDs = new DataSet();

            SqlDataAdapter ODa = new SqlDataAdapter("select Distinct WorksManager  FROM tblServiceAdvisor", oConn);
            ODa.Fill(oDs);

            ListItem lstItem;
            lstItem = new ListItem();
            lstItem.Text = "ALL";
            lstItem.Value = "";
            cmbToFill.Items.Add(lstItem);
            for (int i = 0; i < oDs.Tables[0].Rows.Count; i++)
            {
                lstItem = new ListItem();
                lstItem.Text = oDs.Tables[0].Rows[i]["WorksManager"].ToString();
                lstItem.Value = oDs.Tables[0].Rows[i]["WorksManager"].ToString();
                cmbToFill.Items.Add(lstItem);
            }
        }
        catch (Exception ex)
        {
            DataManager.SendErrorReport(ex.Message);
        }
        finally
        {
            oConn.Close();
        }
    }

    //************************************ REPORTS *********************************************
    static public void SendErrorReport(string ErrorMessage)
    {
        try
        {
            HttpContext.Current.Items["IsError"] = "Yes";
            HttpContext.Current.Items["ErrorMessage"] = ErrorMessage;
            HttpContext.Current.Server.Transfer("ErrorDisplay.aspx");
        }
        catch (Exception ex)
        {
        }
        finally
        {
        }
    }

    static public DataTable GetFrontOfficeReportDataTable(DateTime DtFrom, DateTime DtTo, string ServiceAdvisor, string StProc)
    {
        string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        SqlConnection oConn = new SqlConnection(sConnString);
        SqlCommand cmd = new SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = StProc;

        cmd.Parameters.AddWithValue("@SAName", ServiceAdvisor);
        cmd.Parameters.AddWithValue("@dateFrom", DtFrom);
        cmd.Parameters.AddWithValue("@ToDate", DtTo);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    static public DataTable GetReportDataTable(DateTime DtFrom, DateTime DtTo, string ServiceAdvisor, string ServiceType, string WorksManager, string regNo, string Deliverystatus, string ServiceTypes, string floorname, string teamleadid, string isbodyshop, string StProc)
    {
        string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        SqlConnection oConn = new SqlConnection(sConnString);
        SqlCommand cmd = new SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = StProc;
        cmd.Parameters.AddWithValue("@regno", regNo);
        cmd.Parameters.AddWithValue("@SAName", ServiceAdvisor);
        cmd.Parameters.AddWithValue("@WorksManager", WorksManager);
        cmd.Parameters.AddWithValue("@Aplus", ServiceType);
        cmd.Parameters.AddWithValue("@Datefrom", DtFrom);
        cmd.Parameters.AddWithValue("@Dateto", DtTo);
        cmd.Parameters.AddWithValue("@DeliveryStatus", Deliverystatus);
        cmd.Parameters.AddWithValue("@ServiceType", ServiceTypes);
        cmd.Parameters.AddWithValue("@FloorName", floorname);
        cmd.Parameters.AddWithValue("@TLId", teamleadid);
        cmd.Parameters.AddWithValue("@IsBodyshop", isbodyshop);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    static public DataTable GetReportDataTable1(DateTime DtFrom, DateTime DtTo, string ServiceAdvisor, string ServiceType, string WorksManager, string regNo, string Deliverystatus, string ServiceTypes, string floorname, string teamleadid, string isbodyshop, string StProc, int Status)
    {
        string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        SqlConnection oConn = new SqlConnection(sConnString);
        SqlCommand cmd = new SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = StProc;
        cmd.Parameters.AddWithValue("@regno", regNo);
        cmd.Parameters.AddWithValue("@SAName", ServiceAdvisor);
        cmd.Parameters.AddWithValue("@WorksManager", WorksManager);
        cmd.Parameters.AddWithValue("@Aplus", ServiceType);
        cmd.Parameters.AddWithValue("@Datefrom", DtFrom);
        cmd.Parameters.AddWithValue("@Dateto", DtTo);
        cmd.Parameters.AddWithValue("@DeliveryStatus", Deliverystatus);
        cmd.Parameters.AddWithValue("@ServiceType", ServiceTypes);
        cmd.Parameters.AddWithValue("@FloorName", floorname);
        cmd.Parameters.AddWithValue("@TLId", teamleadid);
        cmd.Parameters.AddWithValue("@IsBodyshop", isbodyshop);
        cmd.Parameters.AddWithValue("@IsWhiteBoard", Status);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    static public void Fillfloor(ref DropDownList cmbFloor)
    {
        cmbFloor.Items.Clear();
        string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        SqlConnection oConn = new SqlConnection(sConnString);

        try
        {
            if (oConn.State != ConnectionState.Open)
            {
                oConn.Open();
            }
            DataSet oDs = new DataSet();

            SqlCommand cmd = new SqlCommand("", oConn);
            cmd.CommandText = "GetFloorList";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter ODa = new SqlDataAdapter(cmd);
            ODa.Fill(oDs);

            ListItem lstItem;
            lstItem = new ListItem();
            lstItem.Text = "ALL";
            lstItem.Value = "-1";
            cmbFloor.Items.Add(lstItem);
            for (int i = 0; i < oDs.Tables[0].Rows.Count; i++)
            {
                lstItem = new ListItem();
                lstItem.Text = oDs.Tables[0].Rows[i]["FloorName"].ToString();
                lstItem.Value = oDs.Tables[0].Rows[i]["FloorName"].ToString();
                cmbFloor.Items.Add(lstItem);
            }
        }
        catch (Exception ex)
        {
        }
        finally
        {
            oConn.Close();
        }
    }

    static public void FillTeamlead(ref DropDownList cmbTeamLead)
    {
        cmbTeamLead.Items.Clear();
        string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        SqlConnection oConn = new SqlConnection(sConnString);

        try
        {
            if (oConn.State != ConnectionState.Open)
            {
                oConn.Open();
            }
            DataSet oDs = new DataSet();

            SqlCommand cmd = new SqlCommand("", oConn);
            cmd.CommandText = "udpGetTeamLeadList";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter ODa = new SqlDataAdapter(cmd);
            ODa.Fill(oDs);

            ListItem lstItem;
            lstItem = new ListItem();
            lstItem.Text = "ALL";
            lstItem.Value = "-1";
            cmbTeamLead.Items.Add(lstItem);
            for (int i = 0; i < oDs.Tables[0].Rows.Count; i++)
            {
                lstItem = new ListItem();
                lstItem.Text = oDs.Tables[0].Rows[i]["EmpName"].ToString();
                lstItem.Value = oDs.Tables[0].Rows[i]["EmpId"].ToString();
                cmbTeamLead.Items.Add(lstItem);
            }
        }
        catch (Exception ex)
        {
        }
        finally
        {
            oConn.Close();
        }
    }

    static public DataTable GetBayReportDataTable(DateTime DtFrom, DateTime DtTo, string ServiceAdvisor, string ServiceType, string WorksManager, string regNo, string Deliverystatus, string TechnicianName, string StProc)
    {
        string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        SqlConnection oConn = new SqlConnection(sConnString);
        SqlCommand cmd = new SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = StProc;
        cmd.Parameters.AddWithValue("@regno", regNo);
        cmd.Parameters.AddWithValue("@ServiceAd", ServiceAdvisor);
        cmd.Parameters.AddWithValue("@WorksManager", WorksManager);
        cmd.Parameters.AddWithValue("@Aplus", ServiceType);
        cmd.Parameters.AddWithValue("@Datefrom", DtFrom);
        cmd.Parameters.AddWithValue("@Dateto", DtTo);
        cmd.Parameters.AddWithValue("@DeliveryStatus", Deliverystatus);
        cmd.Parameters.AddWithValue("@TechnicianName", TechnicianName);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    static public DataTable GetPerformanceGraph(DateTime DtFrom, DateTime DtTo)
    {
        string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        SqlConnection oConn = new SqlConnection(sConnString);
        SqlCommand cmd = new SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "GetBayavg";
        cmd.Parameters.AddWithValue("@FromDate", DtFrom);
        cmd.Parameters.AddWithValue("@ToDate", DtTo);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    static public DataTable GetServicereport(DateTime DtFrom)
    {
        string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        SqlConnection oConn = new SqlConnection(sConnString);
        SqlCommand cmd = new SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "GetDeliveryReport";
        cmd.Parameters.AddWithValue("@Dtdelivered", DtFrom);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    static public DataTable Forecast(String MonthlyCount)
    {
        string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        SqlConnection oConn = new SqlConnection(sConnString);
        SqlCommand cmd = new SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "ForCast";
        cmd.Parameters.AddWithValue("@Target", MonthlyCount);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    static public DataTable GetCardScanedReportTable(DateTime DtFrom, DateTime DtTo, string ServiceAdvisor, string ServiceType, string WorksManager, string regNo, string Deliverystatus, string DriverName)
    {
        string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        SqlConnection oConn = new SqlConnection(sConnString);
        SqlCommand cmd = new SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "GetnotScannedReport";
        cmd.Parameters.AddWithValue("@regno", regNo);
        cmd.Parameters.AddWithValue("@ServiceAd", ServiceAdvisor);
        cmd.Parameters.AddWithValue("@WorksManager", WorksManager);
        cmd.Parameters.AddWithValue("@Aplus", ServiceType);
        cmd.Parameters.AddWithValue("@Datefrom", DtFrom);
        cmd.Parameters.AddWithValue("@Dateto", DtTo);
        cmd.Parameters.AddWithValue("@DeliveryStatus", Deliverystatus);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    static public DataTable GetRoadtestReportDataTable(DateTime DtFrom, DateTime DtTo, string ServiceAdvisor, string ServiceType, string WorksManager, string regNo, string Deliverystatus, string DriverName)
    {
        string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        SqlConnection oConn = new SqlConnection(sConnString);
        SqlCommand cmd = new SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "RoadTestRpt";
        cmd.Parameters.AddWithValue("@regno", regNo);
        cmd.Parameters.AddWithValue("@ServiceAd", ServiceAdvisor);
        cmd.Parameters.AddWithValue("@WorksManager", WorksManager);
        cmd.Parameters.AddWithValue("@Aplus", ServiceType);
        cmd.Parameters.AddWithValue("@Datefrom", DtFrom);
        cmd.Parameters.AddWithValue("@Dateto", DtTo);
        cmd.Parameters.AddWithValue("@DeliveryStatus", Deliverystatus);
        cmd.Parameters.AddWithValue("@DriverName", DriverName);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    static public DataTable GetSADetails(string frmdate, string todate, string SAName, string ProcedureName)
    {
        string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        SqlConnection oConn = new SqlConnection(sConnString);
        SqlCommand cmd = new SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt1 = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = ProcedureName;

        cmd.Parameters.AddWithValue("@Datefrom", frmdate);
        cmd.Parameters.AddWithValue("@Dateto", todate);
        cmd.Parameters.AddWithValue("@EmpName", SAName);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt1);
            return oDt1;
        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    static public DataTable GetSDDPDD(string frmdate, string todate, string SAName, string floorname, string TLId, string IsBodyshop, string ProcedureName)
    {
        string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        SqlConnection oConn = new SqlConnection(sConnString);
        SqlCommand cmd = new SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt2 = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = ProcedureName;

        cmd.Parameters.AddWithValue("@Datefrom", frmdate);
        cmd.Parameters.AddWithValue("@Dateto", todate);
        cmd.Parameters.AddWithValue("@SAName", SAName);
        cmd.Parameters.AddWithValue("@FloorName", floorname);
        cmd.Parameters.AddWithValue("@TLId", TLId);
        cmd.Parameters.AddWithValue("@IsBodyshop", IsBodyshop);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt2);
            return oDt2;
        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    static public DataTable GetSummaryReport(string ProcedureName)
    {
        string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        SqlConnection oConn = new SqlConnection(sConnString);
        SqlCommand cmd = new SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = ProcedureName;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    static public DataTable GetPartsReport(string DateTimeIn, string Regno, string Parts)
    {
        string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        SqlConnection oConn = new SqlConnection(sConnString);
        SqlCommand cmd = new SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "GetPartsDetail";
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@GateIn", DateTimeIn);
        cmd.Parameters.AddWithValue("@Regno", Regno);
        cmd.Parameters.AddWithValue("@PartName", Parts);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    static public DataTable GetVotingData(string DateTimevote)
    {
        string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        SqlConnection oConn = new SqlConnection(sConnString);
        SqlCommand cmd = new SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "GetVotingDaily";
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@Dateselected", DateTimevote);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    static public DataTable GetPDTAdherance()
    {
        string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        SqlConnection oConn = new SqlConnection(sConnString);
        SqlCommand cmd = new SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "GetPdt";

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    static public DataTable GetProcessTimeDailyTrack()
    {
        string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        SqlConnection oConn = new SqlConnection(sConnString);
        SqlCommand cmd = new SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "GetDailyProcessTime";

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    static public DataTable GetIdleTimeDailyTrack()
    {
        string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        SqlConnection oConn = new SqlConnection(sConnString);
        SqlCommand cmd = new SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "GetDailyIdleTime";

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    static public DataTable GetInstantFeedBack()
    {
        string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        SqlConnection oConn = new SqlConnection(sConnString);
        SqlCommand cmd = new SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "GetVotingPer";

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    static public DataTable GetInstantFeedBackCustomersDetails()
    {
        string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        SqlConnection oConn = new SqlConnection(sConnString);
        SqlCommand cmd = new SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "GetUnSatisfiedList";

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    static public DataTable GetDailyTechnicianPerformance()
    {
        string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        SqlConnection oConn = new SqlConnection(sConnString);
        SqlCommand cmd = new SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "GetTechnicianPerformance";

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    // GetTodayVotingDetails

    static public DataTable GetTodayVotingDetails()
    {
        string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        SqlConnection oConn = new SqlConnection(sConnString);
        SqlCommand cmd = new SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "Select * from dbo.FeedbackSatisfaction";

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    // GetDailyRepeatJobPercentageDetails

    static public DataTable GetDailyRepeatJobPercentageDetails()
    {
        string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        SqlConnection oConn = new SqlConnection(sConnString);
        SqlCommand cmd = new SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "Exec dbo.GetRepeatJobDetails";

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    // GetEmployeePerformance

    static public DataTable GetEmployeePerformance()
    {
        string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        SqlConnection oConn = new SqlConnection(sConnString);
        SqlCommand cmd = new SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "Exec dbo.GetEmpTechPerfrmanceAvg";

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    // GetReVisitCustomersDetails

    static public DataTable GetReVisitCustomersDetails()
    {
        string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        SqlConnection oConn = new SqlConnection(sConnString);
        SqlCommand cmd = new SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "Exec dbo.GetRepeatJobList";

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    // GetVehicleVisitingWeekGraph

    static public DataTable GetVehicleVisitingWeekGraph()
    {
        string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        SqlConnection oConn = new SqlConnection(sConnString);
        SqlCommand cmd = new SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.Text;
        // ProTrackDataSet_GetWeekChart
        cmd.CommandText = "Exec dbo.GetWeekChart";

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    static public DataTable GetRemarksData(string DateFrom, string To, string RegNo)
    {
        string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        SqlConnection oConn = new SqlConnection(sConnString);
        SqlCommand cmd = new SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "GetRemarks";
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@FROM", DateFrom + " 12:00 AM");
        cmd.Parameters.AddWithValue("@To", To + " 11:59 PM");
        cmd.Parameters.AddWithValue("@RegNo", RegNo);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    static public DataTable GetVotingMonthly(string ProcName, string DateTimevote)
    {
        string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        SqlConnection oConn = new SqlConnection(sConnString);
        SqlCommand cmd = new SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = ProcName;
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@Dateselected", DateTimevote);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    static public DataTable GetCapacityData(string DateTimevote)
    {
        string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        SqlConnection oConn = new SqlConnection(sConnString);
        SqlCommand cmd = new SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "BayUtilization";
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@BayInDate", DateTimevote);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    static public DataTable GetTechData(string DateFrom, string DateTo, string TechName)
    {
        DateFrom = DateFrom + " " + "12:00 AM";
        DateTo = DateTo + " " + "11:59 PM";
        string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        SqlConnection oConn = new SqlConnection(sConnString);
        SqlCommand cmd = new SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "TechPerformance";
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@TechName", TechName);
        cmd.Parameters.AddWithValue("@FromDate", DateFrom);
        cmd.Parameters.AddWithValue("@Todate", DateTo);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    static public DataTable GetPtd(string DateFrom, string DateTo)
    {
        DateFrom = DateFrom + " " + "12:00 AM";
        DateTo = DateTo + " " + "11:59 PM";
        string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        SqlConnection oConn = new SqlConnection(sConnString);
        SqlCommand cmd = new SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "GetPromisedVsDelivery";
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@From", DateFrom);
        cmd.Parameters.AddWithValue("@To", DateTo);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    static public DataTable ProcessValues(string DateFrom, string DateTo)
    {
        DateFrom = DateFrom + " " + "12:00 AM";
        DateTo = DateTo + " " + "11:59 PM";
        string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        SqlConnection oConn = new SqlConnection(sConnString);
        SqlCommand cmd = new SqlCommand("", oConn);
        DataSet oDs = new DataSet();
        DataTable oDt = new DataTable();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "GetMinMax";
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@From", DateFrom);
        cmd.Parameters.AddWithValue("@To", DateTo);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(oDt);
            return oDt;
        }
        catch (SqlException ex)
        {
            throw (ex);
        }
        finally
        {
            oConn.Close();
        }
    }

    public static string GetCurrentPageName()
    {
        string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
        System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
        string sRet = oInfo.Name;
        return sRet;
    }

    public static bool GetPageAccess(string PageName, object UserId, object Role)
    {
        return true;
    }

    public static void UpdateTblMasterJobSlipAdd(DateTime PT1, string dealername, string chassis, string engine, string email, string custmor, string phone, string landline, string vehicle, string dealercode, string regno, string card, string ServiceType, DateTime PT, int CustomerWaiting, string Estimate, string KMS, string RONumber, int JDP, int BodyShop, int VAS, int WA, string FloorName, string EmpId)
    {
        SqlConnection con = new SqlConnection();
        try
        {
            OpenDBConnection(ref con);
            SqlCommand cmd = new SqlCommand("UpdateTblMasterJobSlipAdd", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@JobCardInTime", DateTime.Now.ToString());
            cmd.Parameters.AddWithValue("@VehicleModel", vehicle);
            cmd.Parameters.AddWithValue("@card", card);
            cmd.Parameters.AddWithValue("@regno", regno.ToUpper());
            cmd.Parameters.AddWithValue("@servicetype", ServiceType);
            cmd.Parameters.AddWithValue("@ChassisNo", chassis);
            cmd.Parameters.AddWithValue("@EngineNo", engine);
            cmd.Parameters.AddWithValue("@CustomerName", custmor);
            cmd.Parameters.AddWithValue("@Customerphone", phone);
            cmd.Parameters.AddWithValue("@LandlineNo", landline);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@pt", PT.ToString());
            cmd.Parameters.AddWithValue("@CustomerWaiting", CustomerWaiting);
            cmd.Parameters.AddWithValue("@KMS", KMS);
            cmd.Parameters.AddWithValue("@Estimate", Estimate);
            cmd.Parameters.AddWithValue("@RONumber", RONumber);
            cmd.Parameters.AddWithValue("@DealerCode", dealercode);
            cmd.Parameters.AddWithValue("@DealerName", dealername);
            cmd.Parameters.AddWithValue("@Aplus", JDP);
            cmd.Parameters.AddWithValue("@VAS", VAS);
            cmd.Parameters.AddWithValue("@FloorName", FloorName);
            cmd.Parameters.AddWithValue("@WheelAlignment", WA);
            if (PT1.ToString().Trim() != "")
                cmd.Parameters.AddWithValue("@DateOfSale", PT1);
            else
                cmd.Parameters.AddWithValue("@DateOfSale", DateTime.Now.ToString());
            cmd.Parameters.AddWithValue("@BodyShop", BodyShop);
            cmd.Parameters.AddWithValue("@JCOCreatedBy", EmpId);
            int I = cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            CloseConnection(ref con);
        }
    }

    public static void UpdateTblMasterVehicleMap(string regno, string card)
    {
        SqlConnection con = new SqlConnection();
        try
        {
            OpenDBConnection(ref con);

            string cmstr = "UPDATE tblMaster SET RegNo=@regno,DTM='" + DateTime.Now.ToString() + "' WHERE RFID = @card And Delivered=0 And Cancelation=0";
            SqlCommand cmd = new SqlCommand(cmstr, con);
            cmd.Parameters.Clear();

            cmd.Parameters.AddWithValue("@card", card);
            cmd.Parameters.AddWithValue("@regno", regno);
            int I = cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            CloseConnection(ref con);
        }
    }

    public static void UpdateTblMasterVehicleMapChassis(string ChassisNo, string card)
    {
        SqlConnection con = new SqlConnection();
        try
        {
            OpenDBConnection(ref con);
            string cmstr = "UPDATE tblMaster SET ChassisNo=@ChassisNo,DTM='" + DateTime.Now.ToString() + "' WHERE RFID = @card And Delivered=0 And Cancelation=0";
            SqlCommand cmd = new SqlCommand(cmstr, con);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@card", card.Trim());
            cmd.Parameters.AddWithValue("@ChassisNo", ChassisNo.Trim());
            int I = cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            CloseConnection(ref con);
        }
    }

    static public void setProcessedStatus(int slno1)
    {
        int slno = Convert.ToInt32(slno1);
        string sConnString = ConfigurationManager.ConnectionStrings["ProTRAC_ConnectionString"].ToString();
        SqlConnection con = new SqlConnection(sConnString);
        string str = "Update tblAlertLog set Processed=1,AckDateTime='" + DateTime.Now.ToString() + "' where SlNo=" + slno;
        SqlCommand cmd = new SqlCommand(str, con);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
    }
}