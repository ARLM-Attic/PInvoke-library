using System;
using System.Data;
using System.Reflection;
using Foosun.Model;

namespace Foosun.DALFactory
{
    public interface IUserList
    {
        string getGroupName(string GroupNumber);
        int singdel(int id);
        int isLock(string id);
        int unLock(string id);
        int dels(string id);
        DataTable GroupList();
        int bIpoint(string uid, int sPoint);
        int sIpoint(string uid, int sPoint);
        int bGpoint(string uid, int sPoint);
        int sGpoint(string uid, int sPoint);
        DataTable GetPage(string UserName, string RealName, string UserNum, string Sex, string siPoint, string biPoint, string sgPoint, string bgPoint, string _userlock, string _group, string _iscerts, string _SiteID, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SQLConditionInfo[] SqlCondition);
    }
    public sealed partial class DataAccess
    {
        public static IUserList CreateUserList()
        {
            string className = path + ".UserList";
            return (IUserList)Assembly.Load(path).CreateInstance(className);
        }
    }
}
