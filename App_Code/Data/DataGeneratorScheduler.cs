using System;
using System.Data;
using ASP.App_Code.Data;
using DayPilot.Web.Ui.Events;

using AjaxControlToolkit;
using System.Configuration;
using System.Data.SqlClient;

using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using DayPilot.Utils;

/// <summary>
/// Summary description for DataGenerator
/// </summary>
public class DataGeneratorScheduler
{
    public static DataTable DbGetEvents(DateTime start, int days)
    {
        string str = ConfigurationManager.ConnectionStrings["DayPilot"].ToString();
        SqlConnection oConn = new SqlConnection(str);
        SqlCommand cmd = new SqlCommand("udpGetJobAllotment", oConn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Date", start);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        //dt.Columns.Add("eventstart", typeof(DateTime));
        //dt.Columns.Add("eventend", typeof(DateTime));
        //dt.Columns.Add("name", typeof(string));
        //dt.Columns.Add("id", typeof(string));
        //dt.Columns.Add("resource_id", typeof(string));
        //dt.Columns.Add("EmpName", typeof(string));
        DataRow dr;

        dr = dt.NewRow();
        da.Fill(dt);
        return dt;

        //SqlDataAdapter da = new SqlDataAdapter("SELECT [id], [name], [eventstart], [eventend], [resource_id] FROM [event] WHERE NOT (([eventend] <= @start) OR ([eventstart] >= @end))", ConfigurationManager.ConnectionStrings["DayPilot"].ConnectionString);
        //da.SelectCommand.Parameters.AddWithValue("start", start);
        //da.SelectCommand.Parameters.AddWithValue("end", start.AddDays(days));
        //DataTable dt = new DataTable();
        //da.Fill(dt);
        //return dt;
    }


    public static DataTable GetData()
    {

        DataTable dt;
        dt = new DataTable();
        dt.Columns.Add("eventstart", typeof(DateTime));
        dt.Columns.Add("eventend", typeof(DateTime));
        dt.Columns.Add("name", typeof(string));
        dt.Columns.Add("id", typeof(string));
        dt.Columns.Add("resource_id", typeof(string));
        dt.Columns.Add("EmpName", typeof(string));
        dt.Columns.Add("color", typeof(string));

        DataRow dr;

        dr = dt.NewRow();
        dr["id"] = 1;
        dr["eventstart"] = Convert.ToDateTime("15:00");
        dr["eventend"] = Convert.ToDateTime("15:00");
        dr["name"] = "Event 1";
        dr["resource_id"] = "A";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 2;
        dr["eventstart"] = Convert.ToDateTime("00:00");
        dr["eventend"] = Convert.ToDateTime("00:00").AddDays(1);
        dr["name"] = "Event 2";
        dr["resource_id"] = "B";
        dr["color"] = "green";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 3;
        dr["eventstart"] = Convert.ToDateTime("14:15").AddDays(1);
        dr["eventend"] = Convert.ToDateTime("18:45").AddDays(1);
        dr["name"] = "Event 3";
        dr["resource_id"] = "C";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 4;
        dr["eventstart"] = Convert.ToDateTime("16:30");
        dr["eventend"] = Convert.ToDateTime("17:30");
        dr["name"] = "Sales Dept. Meeting Once Again";
        dr["resource_id"] = "D";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 5;
        dr["eventstart"] = Convert.ToDateTime("8:00");
        dr["eventend"] = Convert.ToDateTime("9:00");
        dr["name"] = "Event 4";
        dr["resource_id"] = "E";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 6;
        dr["eventstart"] = Convert.ToDateTime("14:00");
        dr["eventend"] = Convert.ToDateTime("20:00");
        dr["name"] = "Event 6";
        dr["resource_id"] = "F";
        dt.Rows.Add(dr);


        dr = dt.NewRow();
        dr["id"] = 7;
        dr["eventstart"] = Convert.ToDateTime("11:00");
        dr["eventend"] = Convert.ToDateTime("13:14");
        dr["name"] = "Unicode test: 公曆 (requires Unicode fonts on the client side)";
        dr["color"] = "red";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 8;
        dr["eventstart"] = Convert.ToDateTime("13:14").AddDays(-1);
        dr["eventend"] = Convert.ToDateTime("14:05").AddDays(-1);
        dr["name"] = "Event 8";
        dr["resource_id"] = "G";
        dr["color"] = "green";
        dt.Rows.Add(dr);


        dr = dt.NewRow();
        dr["id"] = 9;
        dr["eventstart"] = Convert.ToDateTime("13:14").AddDays(7);
        dr["eventend"] = Convert.ToDateTime("14:05").AddDays(7);
        dr["name"] = "Event 9";
        dr["resource_id"] = "H";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 10;
        dr["eventstart"] = Convert.ToDateTime("13:14").AddDays(-7);
        dr["eventend"] = Convert.ToDateTime("14:05").AddDays(-7);
        dr["name"] = "Event 10";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 11;
        dr["eventstart"] = Convert.ToDateTime("00:00").AddDays(8);
        dr["eventend"] = Convert.ToDateTime("00:00").AddDays(15);
        dr["name"] = "Event 11";
        dr["resource_id"] = "I";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 12;
        dr["eventstart"] = Convert.ToDateTime("00:00").AddDays(-2);
        dr["eventend"] = Convert.ToDateTime("00:00").AddDays(-1);
        dr["name"] = "Event 12";
        dr["resource_id"] = "J";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 13;
        dr["eventstart"] = DateTime.Now.AddDays(-7);
        dr["eventend"] = DateTime.Now.AddDays(14);
        dr["name"] = "Event 13";
        dr["resource_id"] = "K";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 14;
        dr["eventstart"] = Convert.ToDateTime("7:45:00");
        dr["eventend"] = Convert.ToDateTime("8:30:00").AddDays(1);
        dr["name"] = "Event 14";
        dr["resource_id"] = "L";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 15;
        dr["eventstart"] = Convert.ToDateTime("23:30:00").AddDays(1);
        dr["eventend"] = Convert.ToDateTime("00:15:00").AddDays(3);
        dr["name"] = "Event 15";
        dr["resource_id"] = "A";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 16;
        dr["eventstart"] = Convert.ToDateTime("8:30:00").AddDays(1);
        dr["eventend"] = Convert.ToDateTime("9:00:00").AddDays(1);
        dr["name"] = "Event 16";
        dr["resource_id"] = "B";
        dt.Rows.Add(dr);


        dr = dt.NewRow();
        dr["id"] = 17;
        dr["eventstart"] = Convert.ToDateTime("8:00:00").AddDays(2);
        dr["eventend"] = Convert.ToDateTime("8:00:01").AddDays(2);
        dr["name"] = "Event 17";
        dr["resource_id"] = "C";
        dt.Rows.Add(dr);

        for (int i = 0; i < 10; i++)
        {
            dr = dt.NewRow();
            dr["id"] = 1000 + i;
            dr["eventstart"] = Convert.ToDateTime("2009-12-30T08:00:00");
            dr["eventend"] = Convert.ToDateTime("2009-12-30T19:00:00");
            dr["name"] = "Event " + (1000 + i);
            dr["resource_id"] = "D";
            dt.Rows.Add(dr);
        }

        dt.PrimaryKey = new DataColumn[] { dt.Columns["id"] };

        return dt;

    }

    public static DataTable GetDataNumeric()
    {
        DataTable dt;
        dt = new DataTable();
        dt.Columns.Add("start", typeof(DateTime));
        dt.Columns.Add("end", typeof(DateTime));
        dt.Columns.Add("name", typeof(string));
        dt.Columns.Add("id", typeof(string));
        dt.Columns.Add("column", typeof(string));
        dt.Columns.Add("color", typeof(string));

        DataRow dr;

        dr = dt.NewRow();
        dr["id"] = 1;
        dr["start"] = Convert.ToDateTime("15:00");
        dr["end"] = Convert.ToDateTime("15:00");
        dr["name"] = "Event 1";
        dr["column"] = "0";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 2;
        dr["start"] = Convert.ToDateTime("00:00");
        dr["end"] = Convert.ToDateTime("00:00").AddDays(1);
        dr["name"] = "Event 2";
        dr["column"] = "1";
        dr["color"] = "green";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 3;
        dr["start"] = Convert.ToDateTime("14:15").AddDays(1);
        dr["end"] = Convert.ToDateTime("18:45").AddDays(1);
        dr["name"] = "Event 3";
        dr["column"] = "2";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 4;
        dr["start"] = Convert.ToDateTime("16:30");
        dr["end"] = Convert.ToDateTime("17:30");
        dr["name"] = "Sales Dept. Meeting Once Again";
        dr["column"] = "3";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 5;
        dr["start"] = Convert.ToDateTime("8:00");
        dr["end"] = Convert.ToDateTime("9:00");
        dr["name"] = "Event 4";
        dr["column"] = "4";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 6;
        dr["start"] = Convert.ToDateTime("14:00");
        dr["end"] = Convert.ToDateTime("20:00");
        dr["name"] = "Event 6";
        dr["column"] = "5";
        dt.Rows.Add(dr);


        dr = dt.NewRow();
        dr["id"] = 7;
        dr["start"] = Convert.ToDateTime("11:00");
        dr["end"] = Convert.ToDateTime("13:14");
        dr["name"] = "Unicode test: 公曆 (requires Unicode fonts on the client side)";
        dr["color"] = "red";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 8;
        dr["start"] = Convert.ToDateTime("13:14").AddDays(-1);
        dr["end"] = Convert.ToDateTime("14:05").AddDays(-1);
        dr["name"] = "Event 8";
        dr["column"] = "6";
        dr["color"] = "green";
        dt.Rows.Add(dr);


        dr = dt.NewRow();
        dr["id"] = 9;
        dr["start"] = Convert.ToDateTime("13:14").AddDays(7);
        dr["end"] = Convert.ToDateTime("14:05").AddDays(7);
        dr["name"] = "Event 9";
        dr["column"] = "7";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 10;
        dr["start"] = Convert.ToDateTime("13:14").AddDays(-7);
        dr["end"] = Convert.ToDateTime("14:05").AddDays(-7);
        dr["name"] = "Event 10";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 11;
        dr["start"] = Convert.ToDateTime("00:00").AddDays(8);
        dr["end"] = Convert.ToDateTime("00:00").AddDays(15);
        dr["name"] = "Event 11";
        dr["column"] = "8";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 12;
        dr["start"] = Convert.ToDateTime("00:00").AddDays(-2);
        dr["end"] = Convert.ToDateTime("00:00").AddDays(-1);
        dr["name"] = "Event 12";
        dr["column"] = "9";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 13;
        dr["start"] = DateTime.Now.AddDays(-7);
        dr["end"] = DateTime.Now.AddDays(14);
        dr["name"] = "Event 13";
        dr["column"] = "10";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 14;
        dr["start"] = Convert.ToDateTime("7:45:00");
        dr["end"] = Convert.ToDateTime("8:30:00").AddDays(1);
        dr["name"] = "Event 14";
        dr["column"] = "11";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 15;
        dr["start"] = Convert.ToDateTime("23:30:00").AddDays(1);
        dr["end"] = Convert.ToDateTime("00:15:00").AddDays(3);
        dr["name"] = "Event 15";
        dr["column"] = "A";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 16;
        dr["start"] = Convert.ToDateTime("8:30:00").AddDays(1);
        dr["end"] = Convert.ToDateTime("9:00:00").AddDays(1);
        dr["name"] = "Event 16";
        dr["column"] = "1";
        dt.Rows.Add(dr);


        dr = dt.NewRow();
        dr["id"] = 17;
        dr["start"] = Convert.ToDateTime("8:00:00").AddDays(2);
        dr["end"] = Convert.ToDateTime("8:00:01").AddDays(2);
        dr["name"] = "Event 17";
        dr["column"] = "2";
        dt.Rows.Add(dr);

        for (int i = 0; i < 10; i++)
        {
            dr = dt.NewRow();
            dr["id"] = 1000 + i;
            dr["start"] = Convert.ToDateTime("2009-12-30T08:00:00");
            dr["end"] = Convert.ToDateTime("2009-12-30T19:00:00");
            dr["name"] = "Event " + (1000 + i);
            dr["column"] = "3";
            dt.Rows.Add(dr);

        }

        dt.PrimaryKey = new DataColumn[] { dt.Columns["id"] };

        return dt;

    }

    public static DataTable GetDataGantt()
    {
        DataTable dt;
        dt = new DataTable();
        dt.Columns.Add("start", typeof(DateTime));
        dt.Columns.Add("end", typeof(DateTime));
        dt.Columns.Add("name", typeof(string));
        dt.Columns.Add("id", typeof(string));
        dt.Columns.Add("complete", typeof(int));

        DataRow dr;

        dr = dt.NewRow();
        dr["id"] = 1;
        dr["start"] = DateTime.Today;
        dr["end"] = DateTime.Today.AddDays(1);
        dr["name"] = "Task 1";
        dr["complete"] = 50;
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 2;
        dr["start"] = DateTime.Today.AddDays(0);
        dr["end"] = DateTime.Today.AddDays(4);
        dr["name"] = "Task 2";
        dr["complete"] = 21;
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 3;
        dr["start"] = DateTime.Today.AddDays(3);
        dr["end"] = DateTime.Today.AddDays(7);
        dr["name"] = "Task 3";
        dr["complete"] = 50;
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 4;
        dr["start"] = DateTime.Today.AddDays(4);
        dr["end"] = DateTime.Today.AddDays(7);
        dr["name"] = "Task 4";
        dr["complete"] = 37;
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 5;
        dr["start"] = DateTime.Today.AddDays(7);
        dr["end"] = DateTime.Today.AddDays(10);
        dr["name"] = "Task 5";
        dr["complete"] = 50;
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 6;
        dr["start"] = DateTime.Today.AddDays(10);
        dr["end"] = DateTime.Today.AddDays(15);
        dr["name"] = "Task 6";
        dr["complete"] = 50;
        dt.Rows.Add(dr);

        dt.PrimaryKey = new DataColumn[] { dt.Columns["id"] };

        return dt;

    }

    
    public static DataTable GetDataLarge()
    {
        DataTable dt;
        dt = new DataTable();
        dt.Columns.Add("start", typeof(DateTime));
        dt.Columns.Add("end", typeof(DateTime));
        dt.Columns.Add("name", typeof(string));
        dt.Columns.Add("id", typeof(string));
        dt.Columns.Add("column", typeof(string));
        dt.Columns.Add("allday", typeof(bool));

        DataRow dr;

        for (int i = 0; i < 300; i++ )
        {
            dr = dt.NewRow();
            dr["id"] = i + 1000;
            dr["start"] = Convert.ToDateTime("15:50").AddDays(i);
            dr["end"] = Convert.ToDateTime("15:50").AddDays(i);
            dr["name"] = "Event 1";
            dr["column"] = "D";
            dt.Rows.Add(dr);
        }

        for (int i = 0; i < 300; i++ )
        {
            dr = dt.NewRow();
            dr["id"] = i + 2000;
            dr["start"] = Convert.ToDateTime("15:50").AddDays(i);
            dr["end"] = Convert.ToDateTime("15:50").AddDays(i);
            dr["name"] = "Event 1";
            dr["column"] = "G";
            dt.Rows.Add(dr);
        }

        for (int i = 0; i < 300; i++ )
        {
            dr = dt.NewRow();
            dr["id"] = i + 3000;
            dr["start"] = Convert.ToDateTime("15:50").AddDays(i);
            dr["end"] = Convert.ToDateTime("15:50").AddDays(i);
            dr["name"] = "Event 1";
            dr["column"] = "I";
            dt.Rows.Add(dr);
        }

        dr = dt.NewRow();
        dr["id"] = 2;
        dr["start"] = Convert.ToDateTime("16:00");
        dr["end"] = Convert.ToDateTime("17:00");
        dr["name"] = "Event 2";
        dr["column"] = "A";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 3;
        dr["start"] = Convert.ToDateTime("14:15").AddDays(1);
        dr["end"] = Convert.ToDateTime("18:45").AddDays(1);
        dr["name"] = "Event 3";
        dr["column"] = "A";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 4;
        dr["start"] = Convert.ToDateTime("16:30");
        dr["end"] = Convert.ToDateTime("17:30");
        dr["name"] = "Sales Dept. Meeting Once Again";
        dr["column"] = "B";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 5;
        dr["start"] = Convert.ToDateTime("8:00");
        dr["end"] = Convert.ToDateTime("9:00");
        dr["name"] = "Event4asdfasdfasdfadfasdfasdf";
        dr["column"] = "B";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 6;
        dr["start"] = Convert.ToDateTime("14:00");
        dr["end"] = Convert.ToDateTime("10:00").AddDays(1);
        dr["name"] = "Event 6";
        dr["column"] = "C";
        dt.Rows.Add(dr);


        dr = dt.NewRow();
        dr["id"] = 7;
        dr["start"] = Convert.ToDateTime("11:00");
        dr["end"] = Convert.ToDateTime("13:14");
        dr["name"] = "Unicode test: 公曆";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 8;
        dr["start"] = Convert.ToDateTime("13:14").AddDays(-1);
        dr["end"] = Convert.ToDateTime("14:05").AddDays(-1);
        dr["name"] = "Event 8";
        dr["column"] = "C";
        dt.Rows.Add(dr);  
        
        
        dr = dt.NewRow();
        dr["id"] = 9;
        dr["start"] = Convert.ToDateTime("13:14").AddDays(7);
        dr["end"] = Convert.ToDateTime("14:05").AddDays(7);
        dr["name"] = "Event 9";
        dr["column"] = "C";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 10;
        dr["start"] = Convert.ToDateTime("13:14").AddDays(-7);
        dr["end"] = Convert.ToDateTime("14:05").AddDays(-7);
        dr["name"] = "Event 10";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 11;
        dr["start"] = Convert.ToDateTime("00:00").AddDays(8);
        dr["end"] = Convert.ToDateTime("00:00").AddDays(15);
        dr["name"] = "Event 11";
        dr["column"] = "D";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 12;
        dr["start"] = Convert.ToDateTime("00:00");
        dr["end"] = Convert.ToDateTime("00:00").AddDays(1);
        dr["name"] = "Event 12";
        dr["column"] = "D";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 13;
        dr["start"] = DateTime.Now;
        dr["end"] = DateTime.Now.AddDays(1);
        dr["name"] = "Event 13.";
        dr["column"] = "B";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 14;
        dr["start"] = Convert.ToDateTime("7:45:00");
        dr["end"] = Convert.ToDateTime("8:30:00");
        dr["name"] = "Event 14";
        dr["column"] = "D";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 15;
        dr["start"] = Convert.ToDateTime("23:30:00");
        dr["end"] = Convert.ToDateTime("00:15:00").AddDays(1);
        dr["name"] = "Event 15";
        dr["column"] = "D";
        dt.Rows.Add(dr);
        
        dr = dt.NewRow();
        dr["id"] = 16;
        dr["start"] = Convert.ToDateTime("8:30:00").AddDays(1);
        dr["end"] = Convert.ToDateTime("9:00:00").AddDays(3);
        dr["name"] = "Event 16";
        dr["column"] = "D";
        dt.Rows.Add(dr);


        dr = dt.NewRow();
        dr["id"] = 17;
        dr["start"] = Convert.ToDateTime("8:00:00").AddDays(1);
        dr["end"] = Convert.ToDateTime("8:00:01").AddDays(1);
        dr["name"] = "Event 17";
        dr["column"] = "D";
        dt.Rows.Add(dr);

        dt.PrimaryKey = new DataColumn[] { dt.Columns["id"] };

        return dt;

    }

    public static DataTable GetDataBigOnePerDay()
    {
        DataTable dt;
        dt = new DataTable();
        dt.Columns.Add("start", typeof(DateTime));
        dt.Columns.Add("end", typeof(DateTime));
        dt.Columns.Add("name", typeof(string));
        dt.Columns.Add("id", typeof(string));
        dt.Columns.Add("column", typeof(string));
        dt.Columns.Add("allday", typeof(bool));
        dt.Columns.Add("color", typeof(string));

        DataRow dr;

        DateTime start = new DateTime(DateTime.Today.Year, 1, 1);
        int days = Year.Days(start.Year);
        string resources = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        for(int i = 0; i < days; i++)
        {
            for(int r = 0; r < resources.Length; r++)
            {
                dr = dt.NewRow();
                dr["id"] = Guid.NewGuid().ToString();
                dr["start"] = start.AddDays(i);
                dr["end"] = start.AddDays(i + 1);
                dr["name"] = "Event";
                dr["column"] = resources[r];
                dt.Rows.Add(dr);
            }
        }

        dt.PrimaryKey = new DataColumn[] { dt.Columns["id"] };

        return dt;

    }

    public static DataTable GetDataBigOnePerDayMultiply(int x)
    {
        DataTable dt;
        dt = new DataTable();
        dt.Columns.Add("start", typeof(DateTime));
        dt.Columns.Add("end", typeof(DateTime));
        dt.Columns.Add("name", typeof(string));
        dt.Columns.Add("id", typeof(string));
        dt.Columns.Add("column", typeof(string));
        dt.Columns.Add("allday", typeof(bool));
        dt.Columns.Add("color", typeof(string));

        DataRow dr;

        DateTime start = new DateTime(DateTime.Today.Year, 1, 1);
        int days = Year.Days(start.Year);
        string resources = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        for (int i = 0; i < days; i++)
        {
            for (int r = 0; r < resources.Length; r++)
            {
                for (int j = 0; j < x; j++)
                {
                    dr = dt.NewRow();
                    dr["id"] = Guid.NewGuid().ToString();
                    dr["start"] = start.AddDays(i);
                    dr["end"] = start.AddDays(i + 1);
                    dr["name"] = "Event";
                    dr["column"] = resources[r];
                    dt.Rows.Add(dr);
                }
            }
        }

        dt.PrimaryKey = new DataColumn[] { dt.Columns["id"] };

        return dt;

    }

    public static DataTable GetDataDays()
    {
        DataTable dt;
        dt = new DataTable();
        dt.Columns.Add("start", typeof(DateTime));
        dt.Columns.Add("end", typeof(DateTime));
        dt.Columns.Add("name", typeof(string));
        dt.Columns.Add("id", typeof(string));
        dt.Columns.Add("column", typeof(string));
        dt.Columns.Add("color", typeof(string)); 

        DataRow dr;

        dr = dt.NewRow();
        dr["id"] = 1;
        dr["start"] = Convert.ToDateTime("15:00").AddDays(2);
        dr["end"] = Convert.ToDateTime("16:00").AddDays(2);
        dr["name"] = "Event 1";
        dr["column"] = "A";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 2;
        dr["start"] = Convert.ToDateTime("09:00");
        dr["end"] = Convert.ToDateTime("18:00");
        dr["name"] = "Event 2";
        dr["column"] = "B";
        dr["color"] = "green";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 3;
        dr["start"] = Convert.ToDateTime("14:15").AddDays(1);
        dr["end"] = Convert.ToDateTime("18:00").AddDays(1);
        dr["name"] = "Event 3";
        dr["column"] = "C";
        dt.Rows.Add(dr);


        dr = dt.NewRow();
        dr["id"] = 8;
        dr["start"] = Convert.ToDateTime("13:14").AddDays(-1);
        dr["end"] = Convert.ToDateTime("14:05").AddDays(-1);
        dr["name"] = "Event 8";
        dr["column"] = "G";
        dr["color"] = "green";
        dt.Rows.Add(dr);


        dr = dt.NewRow();
        dr["id"] = 9;
        dr["start"] = Convert.ToDateTime("13:14").AddDays(7);
        dr["end"] = Convert.ToDateTime("14:05").AddDays(7);
        dr["name"] = "Event 9";
        dr["column"] = "H";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 10;
        dr["start"] = Convert.ToDateTime("13:14").AddDays(-7);
        dr["end"] = Convert.ToDateTime("14:05").AddDays(-7);
        dr["name"] = "Event 10";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 14;
        dr["start"] = Convert.ToDateTime("9:00:00").AddDays(-2);
        dr["end"] = Convert.ToDateTime("16:30:00").AddDays(-2);
        dr["name"] = "Event 14";
        dr["column"] = "L";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 16;
        dr["start"] = Convert.ToDateTime("9:00:00").AddDays(1);
        dr["end"] = Convert.ToDateTime("11:00:00").AddDays(1);
        dr["name"] = "Event 16";
        dr["column"] = "B";
        dt.Rows.Add(dr);


        dr = dt.NewRow();
        dr["id"] = 17;
        dr["start"] = Convert.ToDateTime("9:00:00").AddDays(2);
        dr["end"] = Convert.ToDateTime("10:00:00").AddDays(2);
        dr["name"] = "Event 17";
        dr["column"] = "C";
        dt.Rows.Add(dr);

        for (int i = 0; i < 10; i++)
        {
            dr = dt.NewRow();
            dr["id"] = 1000 + i;
            dr["start"] = Convert.ToDateTime("2009-12-30T08:00:00");
            dr["end"] = Convert.ToDateTime("2009-12-30T19:00:00");
            dr["name"] = "Event " + (1000 + i);
            dr["column"] = "D";
            dt.Rows.Add(dr);

        }

        dt.PrimaryKey = new DataColumn[] { dt.Columns["id"] };

        return dt;

    }


    public static DataTable GetDataConcurrent()
    {
        DataTable dt;
        dt = new DataTable();
        dt.Columns.Add("start", typeof(DateTime));
        dt.Columns.Add("end", typeof(DateTime));
        dt.Columns.Add("name", typeof(string));
        dt.Columns.Add("id", typeof(string));
        dt.Columns.Add("column", typeof(string));
        dt.Columns.Add("color", typeof(string));

        DataRow dr;

        dr = dt.NewRow();
        dr["id"] = 1;
        dr["start"] = Convert.ToDateTime("15:00").AddDays(-1);
        dr["end"] = Convert.ToDateTime("15:00");
        dr["name"] = "Event 1";
        dr["column"] = "A";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 2;
        dr["start"] = Convert.ToDateTime("00:00");
        dr["end"] = Convert.ToDateTime("00:00").AddDays(1);
        dr["name"] = "Event 2";
        dr["column"] = "A";
        dr["color"] = "green";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 3;
        dr["start"] = Convert.ToDateTime("14:15").AddDays(1);
        dr["end"] = Convert.ToDateTime("18:45").AddDays(1);
        dr["name"] = "Event 3";
        dr["column"] = "A";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 4;
        dr["start"] = Convert.ToDateTime("16:30");
        dr["end"] = Convert.ToDateTime("17:30");
        dr["name"] = "Sales Dept. Meeting Once Again";
        dr["column"] = "A";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 5;
        dr["start"] = Convert.ToDateTime("8:00");
        dr["end"] = Convert.ToDateTime("9:00");
        dr["name"] = "Event 4";
        dr["column"] = "B";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 6;
        dr["start"] = Convert.ToDateTime("14:00");
        dr["end"] = Convert.ToDateTime("20:00").AddDays(2);
        dr["name"] = "Event 6";
        dr["column"] = "B";
        dt.Rows.Add(dr);


        dr = dt.NewRow();
        dr["id"] = 7;
        dr["start"] = Convert.ToDateTime("11:00");
        dr["end"] = Convert.ToDateTime("13:14");
        dr["name"] = "Unicode test: 公曆 (requires Unicode fonts on the client side)";
        dr["color"] = "red";
        dr["column"] = "B";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 8;
        dr["start"] = Convert.ToDateTime("13:14").AddDays(-1);
        dr["end"] = Convert.ToDateTime("14:05").AddDays(-1);
        dr["name"] = "Event 8";
        dr["column"] = "B";
        dr["color"] = "green";
        dt.Rows.Add(dr);


        dr = dt.NewRow();
        dr["id"] = 9;
        dr["start"] = Convert.ToDateTime("13:14").AddDays(7);
        dr["end"] = Convert.ToDateTime("14:05").AddDays(7);
        dr["name"] = "Event 9";
        dr["column"] = "C";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 10;
        dr["start"] = Convert.ToDateTime("13:14").AddDays(-7);
        dr["end"] = Convert.ToDateTime("14:05").AddDays(-7);
        dr["name"] = "Event 10";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 11;
        dr["start"] = Convert.ToDateTime("00:00").AddDays(8);
        dr["end"] = Convert.ToDateTime("00:00").AddDays(15);
        dr["name"] = "Event 11";
        dr["column"] = "C";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 12;
        dr["start"] = Convert.ToDateTime("00:00").AddDays(-2);
        dr["end"] = Convert.ToDateTime("00:00").AddDays(-1);
        dr["name"] = "Event 12";
        dr["column"] = "C";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 13;
        dr["start"] = DateTime.Now.AddDays(-7);
        dr["end"] = DateTime.Now.AddDays(14);
        dr["name"] = "Event 13";
        dr["column"] = "C";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 14;
        dr["start"] = Convert.ToDateTime("7:45:00");
        dr["end"] = Convert.ToDateTime("8:30:00").AddDays(1);
        dr["name"] = "Event 14";
        dr["column"] = "D";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 15;
        dr["start"] = Convert.ToDateTime("23:30:00").AddDays(1);
        dr["end"] = Convert.ToDateTime("00:15:00").AddDays(3);
        dr["name"] = "Event 15";
        dr["column"] = "D";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 16;
        dr["start"] = Convert.ToDateTime("8:30:00").AddDays(1);
        dr["end"] = Convert.ToDateTime("9:00:00").AddDays(1);
        dr["name"] = "Event 16";
        dr["column"] = "D";
        dt.Rows.Add(dr);


        dr = dt.NewRow();
        dr["id"] = 17;
        dr["start"] = Convert.ToDateTime("8:00:00").AddDays(2);
        dr["end"] = Convert.ToDateTime("8:00:01").AddDays(2);
        dr["name"] = "Event 17";
        dr["column"] = "D";
        dt.Rows.Add(dr);

        dt.PrimaryKey = new DataColumn[] { dt.Columns["id"] };

        return dt;

    }

    public static DataTable GetDataOverlapping()
    {
        DataTable dt;
        dt = new DataTable();
        dt.Columns.Add("start", typeof(DateTime));
        dt.Columns.Add("end", typeof(DateTime));
        dt.Columns.Add("name", typeof(string));
        dt.Columns.Add("id", typeof(string));
        dt.Columns.Add("column", typeof(string));
        dt.Columns.Add("color", typeof(string));

        DataRow dr;

        dr = dt.NewRow();
        dr["id"] = 1;
        dr["start"] = Convert.ToDateTime("00:00");
        dr["end"] = Convert.ToDateTime("00:00").AddDays(10);
        dr["name"] = "Event 1";
        dr["column"] = "A";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 2;
        dr["start"] = Convert.ToDateTime("00:00").AddDays(1);
        dr["end"] = Convert.ToDateTime("00:00").AddDays(3);
        dr["name"] = "Event 2";
        dr["column"] = "A";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 3;
        dr["start"] = Convert.ToDateTime("00:00").AddDays(2);
        dr["end"] = Convert.ToDateTime("00:00").AddDays(3);
        dr["name"] = "Event 3";
        dr["column"] = "A";
        dt.Rows.Add(dr);



        dt.PrimaryKey = new DataColumn[] { dt.Columns["id"] };

        return dt;

    }



}
