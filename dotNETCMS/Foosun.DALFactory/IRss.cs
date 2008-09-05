using System;
using System.Data;
using System.Reflection;
using Foosun.Model;

namespace Foosun.DALFactory
{
    public interface IRss
    {
        int sel(string ClassID);
        DataTable getxmllist(string ClassID);
    }
    public sealed partial class DataAccess
    {
        public static IRss CreateRss()
        {
            string className = path + ".Rss";
            return (IRss)Assembly.Load(path).CreateInstance(className);
        }
    }
}
