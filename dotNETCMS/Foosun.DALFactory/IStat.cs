using System;
using System.Data;
using System.Reflection;
using Foosun.Model;

namespace Foosun.DALFactory
{
    public interface IStat
    {
        DataTable sel();
        string Str_ClassSql(string id);
        bool Str_classSql(string ID);
        bool Str_statInfo_Sql(string ID);
        bool Str_statContent_Sql(string ID);
        void Str_InSql(stat_Param sp);
        int Stat_Sql();
        void Vote_Sql(string CheckboxArray);
        void Str_statInfo_Sql_1(string CheckboxArray);
        void Str_statContent_Sql_1(string CheckboxArray);
        bool Str_StatSql();
        bool Str_DelAllinfo_Sql();
        bool Str_DelContent_Sql();
        int sel_1(string Str_statid);
        int Str_CheckSql(string Str_Classname);
        int Str_InSql_1(string Str_statid, string Str_Classname, string SiteID);
        int Str_UpdateSql(string Str_ClassnameE, string id);
        int Str_StatSql_1();
        int Str_StatSqlZ();
        DataTable sel_2(string viewid, string SiteID);
        DataTable sel_3(DateTime newtime, string viewid, string SiteID);
        DataTable sel_New(string viewid, string SiteID);
        DataTable sel_Year(int vyear, string viewid, string SiteID);
        DataTable sel_Month(int vmonth, string viewid, string SiteID);
        DataTable sel_return(string viewid, string SiteID);
        DataTable sel_4(int thehour, string vtime, string viewid, string SiteID);
        DataTable sel_5(int vhour, int vday, int vmonth, int vyear, string viewid, string SiteID);
        DataTable sel_day(string viewid, string SiteID);
        DataTable sel_6(string viewid, string SiteID);
        DataTable sel_7(string viewid, string SiteID);
        DataTable sel_8(string viewid, string SiteID);
        DataTable sel_9(string strtheday, string strthetday, string viewid, string SiteID);
        DataTable sel_10(string strdatetwelve, string viewid, string SiteID);
        DataTable sel_vmonth(string viewid, string SiteID);
        DataTable sel_vyear(string viewid, string SiteID);
        DataTable sel_vpage(string viewid, string SiteID);
        DataTable sel_vip(string viewid, string SiteID);
        DataTable sel_vwidth(string viewid, string SiteID);
        DataTable sel_vsoft(string vsoft, string viewid, string SiteID);
        DataTable sel_vOS(string vOS, string viewid, string SiteID);
        DataTable sel_vwhere(string viewid, string SiteID);
        DataTable sel_vcome(string viewid, string SiteID);

        #region 前台调用
        DataTable sel_stat_Param();
        DataTable sel_vip_1(DateTime newtime);
        void Add(stat_Info info);
        DataTable sel_stat_content(string statidz);
        void Add_1(string vdatee, string starttimee, string highttimee, string statidz, string SiteID);
        void Update(int str_fs_today, int str_fs_yesterday, string str_fs_content_data, int str_fs_all, int str_fs_heigh, string str_fs_heightime, string strclassid, string siteID, string strclassids);
        #endregion 

    }
    public sealed partial class DataAccess
    {
        public static IStat CreateStat()
        {
            string className = path + ".Stat";
            return (IStat)Assembly.Load(path).CreateInstance(className);
        }
    }
}