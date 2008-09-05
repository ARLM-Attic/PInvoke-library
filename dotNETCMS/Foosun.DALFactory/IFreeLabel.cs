using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Data;
using Foosun.Model;

namespace Foosun.DALFactory
{
    public interface IFreeLabel
    {
        IList<FreeLablelDBInfo> GetTables();
        IList<FreeLablelDBInfo> GetFields(string TableName);
        bool IsNameRepeat(int id, string Name);
        bool Add(FreeLabelInfo info);
        bool Update(FreeLabelInfo info);
        bool Delete(int id);
        FreeLabelInfo GetSingle(int id);
        DataTable TestSQL(string Sql);
    }
    public sealed partial class DataAccess
    {
        public static IFreeLabel CreateFreeLabel()
        {
            string className = path + ".FreeLabel";
            return (IFreeLabel)Assembly.Load(path).CreateInstance(className);
        }
    }
}
