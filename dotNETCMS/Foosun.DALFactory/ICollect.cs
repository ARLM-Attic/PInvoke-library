using System;
using System.Text;
using System.Data;
using System.Reflection;
using Foosun.Model;

namespace Foosun.DALFactory
{
    public interface ICollect
    {
        DataTable GetFolderSitePage(int FolderID, int PageIndex, int PageSize, out int RecordCount, out int PageCount);
        void FolderCopy(int id);
        void SiteCopy(int id);
        void FolderDelete(int id);
        void SiteDelete(int id);
        DataTable GetFolder(int id, bool all);
        DataTable GetSite(int id);
        int SiteAdd(CollectSiteInfo st);
        int FolderAdd(string Name, string Description);
        void SiteUpdate(CollectSiteInfo st, int step);
        void FolderUpdate(int id, string Name, string Description);
        DataTable GetRulePage(int PageIndex, int PageSize, out int RecordCount, out int PageCount);
        void RuleDelete(int id);
        int RuleAdd(string Name, string OldStr, string NewStr, int[] AppSites, bool IgnoreCase);
        void RuleUpdate(int RuleID, string Name, string OldStr, string NewStr, int[] AppSites, bool IgnoreCase);
        DataTable GetRule(int id);
        DataTable GetRuleApplyList();
        DataTable SiteList();
        void NewsAdd(CollectNewsInfo newsinfo);
        DataTable GetNewsPage(int PageIndex, int PageSize, out int RecordCount, out int PageCount);
        void NewsDelete(string id);
        CollectNewsInfo GetNews(int id);
        void NewsUpdate(int id, CollectNewsInfo info);
        void StoreNews(bool UnStore, int[] id, out int nSucceed, out int nFailed);
        bool TitleExist(string title);
    }
    public sealed partial class DataAccess
    {
        public static ICollect CreateCollect()
        {
            string className = path + ".Collect";
            return (ICollect)Assembly.Load(path).CreateInstance(className);
        }
    }
}
