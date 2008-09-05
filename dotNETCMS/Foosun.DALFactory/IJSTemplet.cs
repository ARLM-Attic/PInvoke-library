using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Reflection;

namespace Foosun.DALFactory
{
    public interface IJSTemplet
    {
        DataTable List();
        DataTable ClassList();
        DataTable GetCustom();
        DataTable GetPage(int PageIndex, int PageSize, out int RecordCount, out int PageCount, string ParentID);
        void Add(string CName, string JSClassid, int JSTType, string JSTContent);
        void Update(int id, string CName, string JSClassid, string JSTContent);
        DataTable GetSingle(int id);
        DataTable GetClass(int id);
        void ClassAdd(string CName, string ParentID, string Description);
        void ClassUpdate(int id, string CName, string ParentID, string Description);
        void Delete(int id);
        void ClassDelete(string id);
        DataTable reviewTempletContent(string tid);
    }
    public sealed partial class DataAccess
    {
        public static IJSTemplet CreateJSTemplet()
        {
            string className = path + ".JSTemplet";
            return (IJSTemplet)Assembly.Load(path).CreateInstance(className);
        }
    }
}
