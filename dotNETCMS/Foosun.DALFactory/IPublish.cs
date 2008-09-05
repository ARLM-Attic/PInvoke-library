using System;
using System.Data;
using System.Text;
using System.Reflection;
using System.Collections.Generic;
using Foosun.Model;

namespace Foosun.DALFactory
{
    public interface IPublish
    {
        /// <summary>
        /// 最新的若干条新闻
        /// </summary>
        /// <param name="topnum"></param>
        /// <param name="classid">如果为"0"表示所有栏目，否则指定栏目</param>
        /// <returns></returns>
        DataTable GetLastNews(int topnum, string classid);
        DataTable GetLastCHNews(int topnum, int classid, int ChID);
        IDataReader GetSysParam();
        IList<PubClassInfo> GetClassList();
        PubClassInfo GetClassById(string ClassID);
        PubSpecialInfo GetSpecial(string specialID);
        IList<PubSpecialInfo> GetSpecialList();
        IList<PubCHClassInfo> GetCHClassList();
        IList<PubCHSpecialInfo> GetCHSpecialList();
        DataTable GetTodayNews(string siteid, string classid);
        IDataReader GetSinglePageClass(string classid);
        IDataReader GetSingleCHPageClass(int classid);
        IDataReader GetNewsSavePath(string newsid);
        IDataReader GetCHNewsSavePath(int newsid,int ChID);
        string GetSysLabelContent(string labelname);
        /// <summary>
        /// 从频道标签库中获取标签内容
        /// </summary>
        /// <param name="labelname"></param>
        /// <returns></returns>
        string GetChannelSysLabelContent(string labelname);
        IDataReader GetFreeLabelContent(string labelname);
        DataTable ExecuteSql(string sql);
        IDataReader GetTemplatePath();
        IDataReader GetNewsDetail(int id, string newsid);
        IDataReader GetPublishSpecial(string spid, out int ncount);
        IDataReader GetPublishCHSpecial(int ChID,string spid, out int ncount);
        IDataReader GetPublishClass(string siteid, string classid, bool isflag, out int ncount);
        IDataReader GetPublishCHClass(string classid,int ChID, out int ncount);
        IDataReader GetPublishNewsAll(out int ncount);
        IDataReader GetPublishCHNewsAll(string DTable,out int ncount);
        IDataReader GetPublishNewsLast(int topnum, bool unpublish, out int ncount);
        IDataReader GetPublishCHNewsLast(string DTalbe,int topnum, bool unpublish, out int ncount);
        IDataReader GetPublishNewsByTime(DateTime StartTm, DateTime EndTm, out int ncount);
        IDataReader GetPublishCHNewsByTime(string DTable,DateTime StartTm, DateTime EndTm, out int ncount);
        IDataReader GetPublishNewsByID(int minid, int maxid, out int ncount);
        IDataReader GetPublishCHNewsByID(string DTable, int minid, int maxid, out int ncount);
        IDataReader GetPublishNewsByClass(string classid, bool unpublish, bool isdesc, string condition, out int ncount);
        IDataReader GetPublishCHNewsByClass(string DTable, string classid, bool unpublish, bool isdesc, string condition, out int ncount);
        IDataReader GetNewsInfoAndClassInfo(string NewsID, string DataLib);
        IDataReader GetJsPath(string jsid);
        IDataReader GetsClassInfo(string ClassID);
        DataTable GetSysUser(int topnum);
        DataTable GetApiComm(int LoopNumber);
        string GetNewsTag(string newsid);
        void UpdateNewsIsHtml(string tablename, string ishtml, string idfield, IList<string> succeedlist);
        void UpdateCHNewsIsHtml(string tablename, string ishtml, string idfield, IList<string> succeedlist);
        IDataReader GetDiscussInfo(string grouptype, int TopNumber);
        string GetMetaContent(string id, string Str, int num);
        string GetPageTitle(string id, string Str);
        string GetCHPageTitle(int id, string Str, int ChID);
        IDataReader GetNewsFiles(string newsid);
        IDataReader GetPrePage(int id, string datalib, int num, string classid, int ChID);
        int GetCommCount(string newsid, int td,int ChID);
        string GetStyleContent(string styleid);
        string GetCHStyleContent(int styleid, int ChID);
        string GetCHDatable( int ChID); 
        IDataReader GetNaviShowClass(string parentid);
        DataTable GetTopLine(string newsid);
        DataTable Gethistory(int Numday);
        DataTable Gethistory(string Strday);
        string GetClassIDByNewsID(string newsid);
        IDataReader GetTopUser(int topnum, string orderfld);
        DataTable GetPosition(string ClassID, int Num);
        IDataReader GetCHPosition(int id, int Num, int ChID);
        DataTable GetUnRule(string UnID, string SiteID);
        DataTable GetSubUnRule(string NewsID);
        DataTable GetSubClass(string ClassID, int isParent, string OrderBy,string Desc);
        string GetDefinedValue(string dfid, string dfcolumn);
        string GetCHDefinedValue(int ID, string dfcolumn,string DTable);
         /// <summary>
        /// 文字副新闻
        /// </summary>
        /// <param name="TopNum"></param>
        /// <returns></returns>
        DataTable GetTextSubNews(int TopNum);

        IDataReader GetCHDetail(int id, string DTable);
        /// <summary>
        /// 得到搜索字段
        /// </summary>
        IDataReader GetFieldName(int ChID);
        IDataReader GetPositionNavi(int id, string Str, int ChID);
        string GetCHMeta(int id, int Num, int ChID,string Str);
        IDataReader GetFriend(int Type, int Number, int IsAdmin);

        /// <summary>
        /// 根据新闻ID获取域名
        /// </summary>
        /// <param name="NewsID"></param>
        /// <returns></returns>
        string getNewsDomain(string NewsID);

        /// <summary>
        ///  更改新闻是否生成静态页面状态（wxh 20008-6-23）
        /// </summary>
        /// <param name="newsID"></param>
        /// <returns></returns>
        int updateIsHtmlState(string newsID);
    }
    public sealed partial class DataAccess
    {
        public static IPublish CreatePublish()
        {
            string className = path + ".Publish";
            return (IPublish)Assembly.Load(path).CreateInstance(className);
        }
    }
}
