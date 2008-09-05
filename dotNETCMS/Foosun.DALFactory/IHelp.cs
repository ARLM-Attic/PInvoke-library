using System;
using System.Data;
using System.Reflection;
using Foosun.Model;

namespace Foosun.DALFactory
{
    public interface IHelp
    {
        int Str_CheckSql(string Str_HelpID);
        int Str_InsSql(string Str_HelpID, string Str_CnHelpTitle, string Str_CnHelpContent);
        int Str_DelSql(int ID);
        DataTable getHelpID(int id);
        int updatehelp(int Str_HelpID, string Str_CnHelpTitle, string Str_CnHelpContent);
        DataTable GetPage(string _HelpID,int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SQLConditionInfo[] SqlCondition);
        DataTable getHelpID1(string HelpId);
    }
    public sealed partial class DataAccess
    {
        public static IHelp CreateHelp()
        {
            string className = path + ".Help";
            return (IHelp)Assembly.Load(path).CreateInstance(className);
        }
    }
}