//===========================================================
//==     (c)2007 Foosun Inc. by dotNETCMS 1.0              ==
//==             Forum:bbs.foosun.net                      ==
//==            website:www.foosun.net                     ==
//==     Address:NO.109 HuiMin ST.,Chengdu ,China          ==
//==         TEL:86-28-85098980/66026180                   ==
//==         qq:655071,MSN:ikoolls@gmail.com               ==
//==               Code By ZhaoHui.Chen                    ==
//===========================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Foosun.DALFactory;
using Foosun.Model;

namespace Foosun.CMS
{
    public class Stat
    {
        private IStat dal;
        public Stat()
        {
            dal = Foosun.DALFactory.DataAccess.CreateStat();
        }
        public DataTable sel()
        {
            return dal.sel(); 
        }
        public string Str_ClassSql(string id) 
        {
            return dal.Str_ClassSql(id); 
        }
        public bool Str_classSql(string ID) 
        {
            return dal.Str_classSql(ID); 
        }
        public bool Str_statInfo_Sql(string ID) 
        {
            return dal.Str_statInfo_Sql(ID); 
        }
        public bool Str_statContent_Sql(string ID) 
        {
            return dal.Str_statContent_Sql(ID); 
        }
        public void Str_InSql(stat_Param sp) 
        {
            dal.Str_InSql(sp); 
        }
        public int Stat_Sql() 
        {
            return dal.Stat_Sql(); 
        }
        public void Vote_Sql(string CheckboxArray) 
        {
            dal.Vote_Sql(CheckboxArray);
        }
        public void Str_statInfo_Sql_1(string CheckboxArray) 
        {
            dal.Str_statInfo_Sql_1(CheckboxArray); 
        }
        public void Str_statContent_Sql_1(string CheckboxArray) 
        {
            dal.Str_statContent_Sql_1(CheckboxArray); 
        }
        public bool Str_StatSql() 
        {
            return dal.Str_StatSql(); 
        }
        public bool Str_DelAllinfo_Sql() 
        {
            return dal.Str_DelAllinfo_Sql(); 
        }
        public bool Str_DelContent_Sql() 
        {
            return dal.Str_DelContent_Sql(); 
        }
        public int sel_1(string Str_statid) 
        {
            return dal.sel_1(Str_statid); 
        }
        public int Str_CheckSql(string Str_Classname) 
        {
            return dal.Str_CheckSql(Str_Classname); 
        }
        public int Str_InSql_1(string Str_statid, string Str_Classname, string SiteID) 
        {
            return dal.Str_InSql_1(Str_statid, Str_Classname, SiteID); 
        }
        public int Str_UpdateSql(string Str_ClassnameE, string id) 
        {
            return dal.Str_UpdateSql(Str_ClassnameE, id); 
        }
        public int Str_StatSql_1() 
        {
            return dal.Str_StatSql_1(); 
        }
        public int Str_StatSqlZ() 
        {
            return dal.Str_StatSqlZ(); 
        }
        public DataTable sel_2(string viewid, string SiteID) 
        {
            return dal.sel_2(viewid, SiteID); 
        }
        public DataTable sel_3(DateTime newtime, string viewid, string SiteID) 
        {
            return dal.sel_3(newtime, viewid, SiteID); 
        }
        public DataTable sel_New(string viewid, string SiteID) 
        {
            return dal.sel_New(viewid, SiteID); 
        }
        public DataTable sel_Year(int vyear, string viewid, string SiteID) 
        {
            return dal.sel_Year(vyear, viewid, SiteID); 
        }
        public DataTable sel_Month(int vmonth, string viewid, string SiteID) 
        {
            return dal.sel_Month(vmonth, viewid, SiteID); 
        }
        public DataTable sel_return(string viewid, string SiteID) 
        {
            return dal.sel_return(viewid, SiteID); 
        }
        public DataTable sel_4(int thehour, string vtime, string viewid, string SiteID) 
        {
            return dal.sel_4(thehour, vtime, viewid, SiteID);
        }
        public DataTable sel_5(int vhour, int vday, int vmonth, int vyear, string viewid, string SiteID) 
        {
            return dal.sel_5(vhour, vday, vmonth, vyear, viewid, SiteID); 
        }
        public DataTable sel_day(string viewid, string SiteID) 
        {
            return dal.sel_day(viewid, SiteID); 
        }
        public DataTable sel_6(string viewid, string SiteID) 
        {
            return dal.sel_6(viewid, SiteID); 
        }
        public DataTable sel_7(string viewid, string SiteID) 
        {
            return dal.sel_7(viewid, SiteID); 
        }
        public DataTable sel_8(string viewid, string SiteID) 
        {
            return dal.sel_8(viewid, SiteID); 
        }
        public DataTable sel_9(string strtheday, string strthetday, string viewid, string SiteID) 
        {
            return dal.sel_9(strtheday, strthetday, viewid, SiteID); 
        }
        public DataTable sel_10(string strdatetwelve, string viewid, string SiteID) 
        {
            return dal.sel_10(strdatetwelve,viewid, SiteID); 
        }
        public DataTable sel_vmonth(string viewid, string SiteID) 
        {
            return dal.sel_vmonth(viewid, SiteID); 
        }
        public DataTable sel_vyear(string viewid, string SiteID) 
        {
            return dal.sel_vyear(viewid, SiteID); 
        }
        public DataTable sel_vpage(string viewid, string SiteID) 
        {
            return dal.sel_vpage(viewid, SiteID); 
        }
        public DataTable sel_vip(string viewid, string SiteID) 
        {
            return dal.sel_vip(viewid, SiteID); 
        }
        public DataTable sel_vwidth(string viewid, string SiteID) 
        {
            return dal.sel_vwidth(viewid, SiteID); 
        }
        public DataTable sel_vsoft(string vsoft, string viewid, string SiteID) 
        {
            return dal.sel_vsoft(vsoft,viewid, SiteID);
        }
        public DataTable sel_vOS(string vOS, string viewid, string SiteID) 
        {
            return dal.sel_vOS(vOS,viewid, SiteID); 
        }
        public DataTable sel_vwhere(string viewid, string SiteID) 
        {
            return dal.sel_vwhere(viewid, SiteID); 
        }
        public DataTable sel_vcome(string viewid, string SiteID) 
        {
            return dal.sel_vcome(viewid, SiteID); 
        }

        #region 前台调用
        public DataTable sel_stat_Param() 
        {
            return dal.sel_stat_Param(); 
        }
        public DataTable sel_vip_1(DateTime newtime) 
        {
            return dal.sel_vip_1(newtime); 
        }
        public void Add(stat_Info info) 
        {
            dal.Add(info); 
        }
        public DataTable sel_stat_content(string statidz) 
        {
            return dal.sel_stat_content(statidz); 
        }
        public void Add_1(string vdatee, string starttimee, string highttimee, string statidz, string SiteID) 
        {
            dal.Add_1(vdatee, starttimee, highttimee, statidz, SiteID); 
        }
        public void Update(int str_fs_today, int str_fs_yesterday, string str_fs_content_data, int str_fs_all, int str_fs_heigh, string str_fs_heightime, string strclassid, string siteID, string strclassids) 
        {
            dal.Update(str_fs_today, str_fs_yesterday, str_fs_content_data, str_fs_all, str_fs_heigh, str_fs_heightime, strclassid, siteID, strclassids); 
        }
        #endregion
    }
}