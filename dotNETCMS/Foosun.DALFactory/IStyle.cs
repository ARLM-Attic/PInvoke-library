using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Foosun.DALFactory
{
    public interface IStyle
    {
        int sytleClassAdd(Foosun.Model.StyleClassInfo sc);
        int styleClassEdit(Foosun.Model.StyleClassInfo sc);
        void styleClassDel(string id);
        void styleClassRDel(string id);
        int styleAdd(Foosun.Model.StyleInfo sc);
        int styleEdit(Foosun.Model.StyleInfo sc);
        void styleDel(string id);
        void styleRdel(string id);
        DataTable getstyleClassInfo(string id);
        DataTable getstyleInfo(string id);
        DataTable styledefine();
        DataTable styleClassList();
        int styleNametf(string CName);
    }
    public sealed partial class DataAccess
    {
        public static IStyle CreateStyle()
        {
            string className = path + ".Style";
            return (IStyle)Assembly.Load(path).CreateInstance(className);
        }
    }
}
