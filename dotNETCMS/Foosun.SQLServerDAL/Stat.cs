using System;
using System.Data;
using System.Data.SqlClient;
using Foosun.DALFactory;
using Foosun.Model;
using Foosun.Common;
using System.Text.RegularExpressions;
using System.Text;
using System.Reflection;
using Foosun.DALProfile;
using Foosun.Config;

namespace Foosun.SQLServerDAL
{
    public class Stat : DbBase, IStat
    {
        string isDataBase = Foosun.Config.UIConfig.indeData;
      
        public DataTable sel()
        {
            string Sql = "select * from " + Pre + "stat_Param";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }
        public string Str_ClassSql(string id)
        {
            SqlParameter param = new SqlParameter("@id", id);
            string Sql = "Select classname From " + Pre + "stat_class where statid=@id and SiteID='" + Foosun.Global.Current.SiteID + "'";
            return Convert.ToString(DbHelper.ExecuteScalar( CommandType.Text, Sql, param));
        }
        public bool Str_classSql(string ID)
        {
            SqlParameter param = new SqlParameter("@ID", ID);
            string Sql = "Delete From " + Pre + "stat_class where statid=@ID and SiteID='" + Foosun.Global.Current.SiteID + "'";
            int i = DbHelper.ExecuteNonQuery( CommandType.Text, Sql, param);
            if (i == 0)
            {
                return false;
            }
            return true;
        }
        public bool Str_statInfo_Sql(string ID)
        {
            SqlParameter param = new SqlParameter("@ID", ID);
            string Sql = "Delete From " + Pre + "stat_Info where classid = @ID and SiteID='" + Foosun.Global.Current.SiteID + "'";
            int i = DbHelper.ExecuteNonQuery( CommandType.Text, Sql, param);
            if (i == 0)
            {
                return false;
            }
            return true;
        }
        public bool Str_statContent_Sql(string ID)
        {
            SqlParameter param = new SqlParameter("@ID", ID);
            string Sql = "Delete From " + Pre + "stat_Content where classid = @ID and SiteID='" + Foosun.Global.Current.SiteID + "'";
            int i = DbHelper.ExecuteNonQuery( CommandType.Text, Sql, param);
            if (i == 0)
            {
                return false;
            }
            return true;
        }
        public void Str_InSql(Foosun.Model.stat_Param sp)
        {
            string Sql = "Update " + Pre + "stat_Param Set SystemName=@Str_SystemName,SystemNameE=@Str_SystemNameE,ipCheck=@Str_ipCheck,ipTime=@Str_ipTime,isOnlinestat=@Str_isOnlinestat,pageNum=@Str_pageNum,cookies=@Str_cookies,pointNum=@Str_pointNum,SiteID=@SiteID";
            SqlParameter[] parm = Getstat_Param(sp);
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }

        private SqlParameter[] Getstat_Param(Foosun.Model.stat_Param sp)
        {
            SqlParameter[] parm = new SqlParameter[9];
            parm[0] = new SqlParameter("@Str_SystemName", SqlDbType.NVarChar, 100);
            parm[0].Value = sp.SystemName;
            parm[1] = new SqlParameter("@Str_SystemNameE", SqlDbType.NVarChar, 150);
            parm[1].Value = sp.SystemNameE;
            parm[2] = new SqlParameter("@Str_ipCheck", SqlDbType.TinyInt, 1);
            parm[2].Value = sp.ipCheck;
            parm[3] = new SqlParameter("@Str_ipTime", SqlDbType.Int, 4);
            parm[3].Value = sp.ipTime;
            parm[4] = new SqlParameter("@Str_isOnlinestat", SqlDbType.TinyInt, 1);
            parm[4].Value = sp.isOnlinestat;
            parm[5] = new SqlParameter("@Str_pageNum", SqlDbType.Int, 4);
            parm[5].Value = sp.pageNum;
            parm[6] = new SqlParameter("@Str_cookies", SqlDbType.NVarChar, 30);
            parm[6].Value = sp.cookies;
            parm[7] = new SqlParameter("@Str_pointNum", SqlDbType.Int, 4);
            parm[7].Value = sp.pointNum;
            parm[8] = new SqlParameter("@SiteID", SqlDbType.NVarChar, 12);
            parm[8].Value = sp.SiteID;
            return parm;
        }
        public int Stat_Sql()
        {
            int intnum = 20;
            string Sql = "Select pageNum From " + Pre + "stat_Param where SiteID='" + Foosun.Global.Current.SiteID + "'";//取得参数设置中的每页显示数
            DataTable dt = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    intnum = int.Parse(dt.Rows[0]["pageNum"].ToString());
                }
                dt.Clear(); dt.Dispose();
            }
            return intnum;
        }
        public void Vote_Sql(string CheckboxArray)
        {
            string Sql = "Delete From " + Pre + "stat_class where statid in ('" + CheckboxArray + "') and SiteID='" + Foosun.Global.Current.SiteID + "'";
            DbHelper.ExecuteNonQuery( CommandType.Text, Sql, null);
        }
        public void Str_statInfo_Sql_1(string CheckboxArray)
        {
            string Sql = "Delete From " + Pre + "stat_Info where classid in ('" + CheckboxArray + "') and SiteID='" + Foosun.Global.Current.SiteID + "'";
            DbHelper.ExecuteNonQuery( CommandType.Text, Sql, null);
        }
        public void Str_statContent_Sql_1(string CheckboxArray)
        {
            string Sql = "Delete From " + Pre + "stat_Content where classid in ('" + CheckboxArray + "') and SiteID='" + Foosun.Global.Current.SiteID + "'";
            DbHelper.ExecuteNonQuery( CommandType.Text, Sql, null);
        }
        public bool Str_StatSql()
        {
            string Sql = "Delete From " + Pre + "stat_class where SiteID='" + Foosun.Global.Current.SiteID + "'";
            int i = DbHelper.ExecuteNonQuery( CommandType.Text, Sql, null);
            if (i == 0)
            {
                return false;
            }
            return true;
        }
        public bool Str_DelAllinfo_Sql()
        {
            string Sql = "Delete From " + Pre + "stat_Info where SiteID='" + Foosun.Global.Current.SiteID + "'";
            int i = DbHelper.ExecuteNonQuery( CommandType.Text, Sql, null);
            if (i == 0)
            {
                return false;
            }
            return true;
        }
        public bool Str_DelContent_Sql()
        {
            string Sql = "Delete From " + Pre + "stat_Content where SiteID='" + Foosun.Global.Current.SiteID + "'";
            int i = DbHelper.ExecuteNonQuery( CommandType.Text, Sql, null);
            if (i == 0)
            {
                return false;
            }
            return true;
        }
        public int sel_1(string Str_statid)
        {
            SqlParameter param = new SqlParameter("@statid", Str_statid);
            string Sql = "Select count(statid) From " + Pre + "stat_class where statid = @statid and SiteID='" + Foosun.Global.Current.SiteID + "'";
            return (int)DbHelper.ExecuteScalar( CommandType.Text, Sql,param);
        }
        public int Str_CheckSql(string Str_Classname)
        {
            SqlParameter param = new SqlParameter("@Classname", Str_Classname);
            string Sql = "Select count(classname) From " + Pre + "stat_class Where classname=@Classname and SiteID='" + Foosun.Global.Current.SiteID + "'";
            return (int)DbHelper.ExecuteScalar( CommandType.Text, Sql, param);
        }
        public int Str_InSql_1(string Str_statid, string Str_Classname, string SiteID)
        {
            string Sql = "Insert into " + Pre + "stat_class (statid,classname,SiteID) Values('" + Str_statid + "','" + Str_Classname + "','" + SiteID + "')";
            return DbHelper.ExecuteNonQuery( CommandType.Text, Sql, null);
        }
        public int Str_UpdateSql(string Str_ClassnameE, string id)
        {
            string Sql = "Update " + Pre + "stat_class set classname='" + Str_ClassnameE + "' where statid='" + id + "' and SiteID='" + Foosun.Global.Current.SiteID + "'";
            return DbHelper.ExecuteNonQuery( CommandType.Text, Sql, null);
        }
        public int Str_StatSql_1()
        {
            string Sql = "Delete From " + Pre + "stat_Info where SiteID='" + Foosun.Global.Current.SiteID + "'";
            return DbHelper.ExecuteNonQuery( CommandType.Text, Sql, null);
        }
        public int Str_StatSqlZ()
        {
            string Sql = "Delete From " + Pre + "stat_content where SiteID='" + Foosun.Global.Current.SiteID + "'";
            return DbHelper.ExecuteNonQuery( CommandType.Text, Sql, null);
        }
        public DataTable sel_2(string viewid, string SiteID)
        {
            string Sql = "select vtop,starttime,vhigh,vhightime from " + Pre + "stat_Content where classid='" + viewid + "' and SiteID='" + SiteID + "'";
            return DbHelper.ExecuteTable( CommandType.Text, Sql, null);
        }
        public DataTable sel_3(DateTime newtime, string viewid, string SiteID)
        {
            string Sql = "select vip from " + Pre + "stat_Info where vtime >='" + newtime + "' and classid='" + viewid + "' and SiteID='" + SiteID + "' group by vip";
            return DbHelper.ExecuteTable( CommandType.Text, Sql, null);
        }
        public DataTable sel_New(string viewid, string SiteID)
        {
            string Sql = "Select today,yesterday from " + Pre + "stat_Content where classid='" + viewid + "' and SiteID='" + SiteID + "'";
            return DbHelper.ExecuteTable( CommandType.Text, Sql, null);
        }
        public DataTable sel_Year(int vyear, string viewid, string SiteID)
        {
            string Sql = "Select vyear from " + Pre + "stat_Info where vyear=" + vyear + " and  classid='" + viewid + "' and SiteID='" + SiteID + "'";
            return DbHelper.ExecuteTable( CommandType.Text, Sql, null);
        }
        public DataTable sel_Month(int vmonth, string viewid, string SiteID)
        {
            string Sql = "Select vmonth from " + Pre + "stat_Info where vmonth=" + vmonth + " and classid='" + viewid + "' and SiteID='" + SiteID + "'";
            return DbHelper.ExecuteTable( CommandType.Text, Sql, null);
        }
        public DataTable sel_return(string viewid, string SiteID)
        {
            string Sql = "select vhour,count(id) as allhour from " + Pre + "stat_Info where classid='" + viewid + "' and SiteID='" + SiteID + "' group by vhour";
            return DbHelper.ExecuteTable( CommandType.Text, Sql, null);
        }
        public DataTable sel_5(int vhour, int vday, int vmonth, int vyear, string viewid, string SiteID)
        {
            string Sql = "Select count(id) as vhourcon from " + Pre + "stat_Info where vhour='" + vhour + "' and vday='" + vday + "' and vmonth='" + vmonth + "' and vyear='" + vyear + "' and classid='" + viewid + "' and SiteID='" + SiteID + "'";
            return DbHelper.ExecuteTable( CommandType.Text, Sql, null);
        }
        public DataTable sel_4(int thehour, string vtime, string viewid, string SiteID)
        {
            string Sql = "Select count(id) as vhourcon from " + Pre + "stat_Info where vhour='" + thehour + "' and vtime>'" + vtime + "' and classid='" + viewid + "' and SiteID='" + SiteID + "'";
            return DbHelper.ExecuteTable( CommandType.Text, Sql, null);
        }
        public DataTable sel_day(string viewid, string SiteID)
        {
            string Sql = "Select top 1 vtime from " + Pre + "stat_Info where classid='" + viewid + "' and SiteID='" + SiteID + "' order by id";
            return DbHelper.ExecuteTable( CommandType.Text, Sql, null);
        }
        public DataTable sel_6(string viewid, string SiteID)
        {
            string Sql = "select vday,count(id) as allday from " + Pre + "stat_Info where classid='" + viewid + "' and SiteID='" + SiteID + "' group by vday";
            return DbHelper.ExecuteTable( CommandType.Text, Sql, null);
        }
        public DataTable sel_7(string viewid, string SiteID)
        {
            string Sql = "Select top 1 vtime as vfirst from " + Pre + "stat_Info where classid='" + viewid + "' and SiteID='" + SiteID + "' order by vtime";
            return DbHelper.ExecuteTable( CommandType.Text, Sql, null);
        }
        public DataTable sel_8(string viewid, string SiteID)
        {
            string Sql = "select vweek,count(id) as allweek from " + Pre + "stat_Info where classid='" + viewid + "' and SiteID='" + SiteID + "' group by vweek";
            return DbHelper.ExecuteTable( CommandType.Text, Sql, null);
        }
        public DataTable sel_9(string strtheday, string strthetday, string viewid, string SiteID)
        {
            string Sql = "Select count(id) as vdaycon from " + Pre + "stat_Info where vtime>='" + strtheday + "' and vtime<='" + strthetday + "' and classid='" + viewid + "' and SiteID='" + SiteID + "'";
            return DbHelper.ExecuteTable( CommandType.Text, Sql, null);
        }
        public DataTable sel_10(string strdatetwelve, string viewid, string SiteID)
        {
            string Sql = "select vmonth,count(id) as allmonth from " + Pre + "stat_Info where vtime>='" + strdatetwelve + "' and classid='" + viewid + "' and SiteID='" + SiteID + "' group by vmonth";
            return DbHelper.ExecuteTable( CommandType.Text, Sql, null);
        }
        public DataTable sel_vmonth(string viewid, string SiteID)
        {
            string Sql = "select vmonth,count(id) as allmonth from " + Pre + "stat_Info where classid='" + viewid + "' and SiteID='" + SiteID + "' group by vmonth";
            return DbHelper.ExecuteTable( CommandType.Text, Sql, null);
        }
        public DataTable sel_vyear(string viewid, string SiteID)
        {
            string Sql = "select vyear,count(id) as allyear from " + Pre + "stat_Info where classid='" + viewid + "' and SiteID='" + SiteID + "' group by vyear order by vyear DESC";
            return DbHelper.ExecuteTable( CommandType.Text, Sql, null);
        }
        public DataTable sel_vpage(string viewid, string SiteID)
        {
            string Sql = "select vpage,count(id) as allpage from " + Pre + "stat_Info where classid='" + viewid + "' and SiteID='" + SiteID + "' group by vpage order by count(id) DESC";
            return DbHelper.ExecuteTable( CommandType.Text, Sql, null);
        }
        public DataTable sel_vip(string viewid, string SiteID)
        {
            string Sql = "select vip,count(id) as allip from " + Pre + "stat_Info where classid='" + viewid + "' and SiteID='" + SiteID + "' group by vip order by count(id) DESC";
            return DbHelper.ExecuteTable( CommandType.Text, Sql, null);
        }
        public DataTable sel_vwidth(string viewid, string SiteID)
        {
            string Sql = "select vwidth,count(id) as allwidth from " + Pre + "stat_Info where vwidth<>0 and classid='" + viewid + "' and SiteID='" + SiteID + "' group by vwidth order by vwidth DESC";
            return DbHelper.ExecuteTable( CommandType.Text, Sql, null);
        }
        public DataTable sel_vsoft(string vsoft, string viewid, string SiteID)
        {
            string Sql = "Select count(id) as howsoft from " + Pre + "stat_Info where vsoft='" + vsoft + "' and classid='" + viewid + "' and SiteID='" + SiteID + "'";
            return DbHelper.ExecuteTable( CommandType.Text, Sql, null);
        }
        public DataTable sel_vOS(string vOS, string viewid, string SiteID)
        {
            string Sql = "Select count(id) as howOS from " + Pre + "stat_Info where vOS='" + vOS + "' and classid='" + viewid + "' and SiteID='" + SiteID + "'";
            return DbHelper.ExecuteTable( CommandType.Text, Sql, null);
        }
        public DataTable sel_vwhere(string viewid, string SiteID)
        {
            string Sql = "select vwhere,count(id) as allwhere from " + Pre + "stat_Info where classid='" + viewid + "' and SiteID='" + SiteID + "' group by vwhere order by count(id) DESC";
            return DbHelper.ExecuteTable( CommandType.Text, Sql, null);
        }
        public DataTable sel_vcome(string viewid, string SiteID)
        {
            string Sql = "select vcome,count(id) as allcome from " + Pre + "stat_Info where classid='" + viewid + "' and SiteID='" + SiteID + "' group by vcome order by count(id) DESC";
            return DbHelper.ExecuteTable( CommandType.Text, Sql, null);
        }

        #region 前台调用
        public DataTable sel_stat_Param()
        {
            string Sql = "select * from " + Pre + "stat_Param";
            return DbHelper.ExecuteTable( CommandType.Text, Sql, null);
        }
        public DataTable sel_vip_1(DateTime newtime)
        {
            SqlParameter param = new SqlParameter("@newtime", newtime);
            string Sql = "select vip from " + Pre + "stat_Info where vtime >=@newtime group by vip";
            return DbHelper.ExecuteTable( CommandType.Text, Sql, param);
        }
        public void Add(stat_Info info)
        {
            //bug修改,插入时间有误 周峻平 2008-5-29
            string Sql = "insert into " + Pre + "stat_Info(vyear,vmonth,vday,vhour,vtime,vweek,vip,vwhere,vwheref,vcome,vpage,vsoft,vOS,vwidth,classid,SiteID) values(@str_fs_year,@str_fs_month,@str_fs_day,@str_fs_hour,getdate(),@str_fs_week,@str_fs_ip,@str_fs_country,@str_fs_city,@str_fs_come,@str_fs_page,@str_fs_soft,@str_fs_Os,@str_fs_width,@statid,@SiteID)";
            SqlParameter[] parm = Getstat_Info(info);
            DbHelper.ExecuteNonQuery( CommandType.Text, Sql, parm);
        }
        private SqlParameter[] Getstat_Info(stat_Info info)
        {
            SqlParameter[] parm = new SqlParameter[16];
            parm[0] = new SqlParameter("@str_fs_year", SqlDbType.Int, 4);
            parm[0].Value = info.vyear;
            parm[1] = new SqlParameter("@str_fs_month", SqlDbType.Int, 4);
            parm[1].Value = info.vmonth;
            parm[2] = new SqlParameter("@str_fs_day", SqlDbType.Int, 4);
            parm[2].Value = info.vday;
            parm[3] = new SqlParameter("@str_fs_hour", SqlDbType.Int, 4);
            parm[3].Value = info.vhour;
            parm[4] = new SqlParameter("@str_fs_time", SqlDbType.NVarChar, 4);
            parm[4].Value = info.vtime;
            parm[5] = new SqlParameter("@str_fs_week", SqlDbType.Int, 4);
            parm[5].Value = info.vweek;
            parm[6] = new SqlParameter("@str_fs_ip", SqlDbType.NVarChar, 50);
            parm[6].Value = info.vip;
            parm[7] = new SqlParameter("@str_fs_country", SqlDbType.NVarChar, 250);
            parm[7].Value = info.vwhere;
            parm[8] = new SqlParameter("@str_fs_city", SqlDbType.NVarChar, 50);
            parm[8].Value = info.vwheref;
            parm[9] = new SqlParameter("@str_fs_come", SqlDbType.NVarChar, 250);
            parm[9].Value = info.vcome;
            parm[10] = new SqlParameter("@str_fs_page", SqlDbType.NVarChar, 250);
            parm[10].Value = info.vpage;
            parm[11] = new SqlParameter("@str_fs_soft", SqlDbType.NVarChar, 50);
            parm[11].Value = info.vsoft;
            parm[12] = new SqlParameter("@str_fs_Os", SqlDbType.NVarChar, 50);
            parm[12].Value = info.vOS;
            parm[13] = new SqlParameter("@str_fs_width", SqlDbType.Int, 4);
            parm[13].Value = info.vwidth;
            parm[14] = new SqlParameter("@statid", SqlDbType.NVarChar, 12);
            parm[14].Value = info.classid;
            parm[15] = new SqlParameter("@SiteID", SqlDbType.NVarChar, 12);
            parm[15].Value = info.SiteID;
            return parm;
        }
        public DataTable sel_stat_content(string statidz)
        {
            SqlParameter param = new SqlParameter("@statidz", statidz);
            string Sql = "select * from " + Pre + "stat_Content where classid=@statidz";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public void Add_1(string vdatee, string starttimee, string highttimee, string statidz, string SiteID)
        {
            string Sql = "Insert into " + Pre + "stat_Content(today,yesterday,vdate,vtop,starttime,vhigh,vhightime,classid,SiteID) Values(1,0,'" + vdatee + "',1,'" + starttimee + "',1,'" + highttimee + "','" + statidz + "','" + SiteID + "')";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        public void Update(int str_fs_today, int str_fs_yesterday, string str_fs_content_data, int str_fs_all, int str_fs_heigh, string str_fs_heightime, string strclassid, string siteID, string strclassids)
        {
            string Sql = "Update " + Pre + "stat_Content set today=" + str_fs_today + ",yesterday=" + str_fs_yesterday + ",vdate='" + str_fs_content_data + "',vtop=" + str_fs_all + ",vhigh=" + str_fs_heigh + ",vhightime='" + str_fs_heightime + "',classid='" + strclassid + "',SiteID='" + siteID + "' where classid='" + strclassid + "'";
            //bug修改,没有执行此SQL语句 周峻平 2008-5-29
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        #endregion
    }
}