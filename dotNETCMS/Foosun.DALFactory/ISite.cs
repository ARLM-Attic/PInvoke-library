using System;
using System.Data;
using System.Reflection;
using Foosun.Model;

namespace Foosun.DALFactory
{
    public interface ISite
    {
        int Add(STSite StructSite, string CurrentSiteID, out string SiteID);
        bool Delete(string id, out Exception e, out string[] DelFiles);
        void Update(int id,STSite site);
        void Recyle(string id);
        DataTable List(SiteType sttype);
        IDataReader siteList();
        DataTable GetSingle(int id);
        DataTable GetSiteInfo(string ChannelID);
        int getsiteClassCount(string siteid);
    }
    public sealed partial class DataAccess
    {
        public static ISite CreateSite()
        {
            string className = path + ".Site";
            return (ISite)Assembly.Load(path).CreateInstance(className);
        }
    }
}
