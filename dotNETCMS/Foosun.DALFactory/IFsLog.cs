using System;
using System.Reflection;

namespace Foosun.DALFactory
{
    public interface IFsLog
    {
        int Add(int IsManager,string Title,string Content,string IP,string UserNum,string SiteID);
    }
    public sealed partial class DataAccess
    {
        public static IFsLog CreateFsLog()
        {
            string className = path + ".FsLog";
            return (IFsLog)Assembly.Load(path).CreateInstance(className);
        }
    }
}
