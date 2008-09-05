using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Foosun.DALFactory
{
    public interface IAds
    {
        DataTable list(Foosun.Model.AdsListInfo ali);
        DataTable childlist(string classid);
        void Lock(string id);
        void UnLock(string id);
        void DelAllAds();
        DataTable AdsDt(string id);
        void DelPAds(string id);
        void DelAllAdsClass();
        DataTable adsClassDt(string classid);
        void DelPAdsClass(string classid);
        int AddClass(Foosun.Model.AdsClassInfo aci);
        DataTable getAdsClassInfo(string classid);
        int EditClass(Foosun.Model.AdsClassInfo aci);
        void statDelAll();
        void statDel(string idstr);
        DataTable getAdsClassList();
        DataTable getAdsList(string id);
        string adsAdd(Foosun.Model.AdsInfo ai);
        int adsEdit(Foosun.Model.AdsInfo ai);
        DataTable getAdsDomain();
        DataTable getAdsPicInfo(string col, string tbname, string id);
        DataTable getAdsInfo(string id);
        DataTable get24HourStat(string type, string id);
        DataTable getDayStat(string type, string id, string mday);
        DataTable getMonthStat(string type, string id);
        DataTable getYearStat(string id);
        DataTable getWeekStat(string type, string id);
        DataTable getSourceStat(string id);
        DataTable getDbNull();
        void upStat(string adress, string id);
        void upShowNum(string id);
        void upClickNum(string id, string type);
        void addStat(string id, string ip);
        DataTable getClassAdprice(string classid);
        DataTable getuserG();
        void DelUserG(int Gnum);
    }
    public sealed partial class DataAccess
    {
        public static IAds CreateAds()
        {
            string className = path + ".Ads";
            return (IAds)Assembly.Load(path).CreateInstance(className);
        }
    }
}
