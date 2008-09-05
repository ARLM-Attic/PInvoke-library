using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Foosun.Model;

namespace Foosun.DALFactory
{
    public interface INewsJS
    {
        IList<NewsJSInfo> GetPage(int PageIndex, int PageSize, out int RecordCount, out int PageCount, int JsType);
        void Delete(string id);
        DataTable GetJSFilePage(int PageIndex, int PageSize, out int RecordCount, out int PageCount, int id);
        void RemoveNews(int id);
        NewsJSInfo GetSingle(int id);
        NewsJSInfo GetSingle(string JsID);
        void Update(NewsJSInfo info);
        string Add(NewsJSInfo info);
        string GetJsTmpContent(string jstmpid);
        DataTable GetJSFiles(string jsid);
        //<--修改者：吴静岚 时间： 2008-06-24 解决自由JS调用新闻条数不受限
        /// <summary>
        /// 获取JS调用新闻数
        /// </summary>
        /// <param name="jsid">js编号</param>
        /// <returns>查询结果</returns>
        DataTable GetJSNum(string jsid);
        //wjl-->
    }

    public sealed partial class DataAccess
    {
        public static INewsJS CreateNewsJS()
        {
            string className = path + ".NewsJS";
            return (INewsJS)Assembly.Load(path).CreateInstance(className);
        }
    }
}
