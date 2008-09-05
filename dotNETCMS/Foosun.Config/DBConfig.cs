using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Web;
namespace Foosun.Config
{
    public class DBConfig
    {
        //public static readonly string CmsConString = ConfigurationManager.ConnectionStrings["foosun"].ConnectionString;
        //public static readonly string HelpConString =ConfigurationManager.ConnectionStrings["HelpKey"].ConnectionString; 
        //public static readonly string CollectConString =  ConfigurationManager.ConnectionStrings["Collect"].ConnectionString;
        public static string CmsConString
        {
            get
            {
                string tstr = ConfigurationManager.ConnectionStrings["foosun"].ConnectionString;
                if (Foosun.Config.UIConfig.WebDAL.ToLower() == "foosun.accessdal")
                {
                    return "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + HttpContext.Current.Server.MapPath(tstr) + ";Persist Security Info=True;";
                }
                else
                {
                    return tstr;
                }
            }
        }
        public static string HelpConString
        {
            get
            {
                string tstr = ConfigurationManager.ConnectionStrings["HelpKey"].ConnectionString;
                if (Foosun.Config.UIConfig.WebDAL.ToLower() == "foosun.accessdal")
                {
                    return "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + HttpContext.Current.Server.MapPath(tstr) + ";Persist Security Info=True;";
                }
                else
                {
                    return tstr;
                }
            }
        }
        public static string CollectConString
        {
            get
            {
                string tstr = ConfigurationManager.ConnectionStrings["Collect"].ConnectionString;
                if (Foosun.Config.UIConfig.WebDAL.ToLower() == "foosun.accessdal")
                {
                    return "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + HttpContext.Current.Server.MapPath(tstr) + ";Persist Security Info=True;";
                }
                else
                {
                    return tstr;
                }
            }
        }
        public static readonly string TableNamePrefix = Foosun.Config.UIConfig.dataRe;
    }
}
