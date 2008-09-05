using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web;

namespace Foosun.Config.API
{
    public class APIConfigs
    {
        static string filename = string.Empty;
        private static APIConfig config ;
        private static string m_fileoldchange = string.Empty;
        
        public static string ConfigFilePath
        {
            get
            {
                if (string.IsNullOrEmpty(filename))
                {

                    if (HttpContext.Current == null)
                    {
                        return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "configuration/config/api.config");
                    }
                    else
                    {
                        string applicationPath = System.Web.HttpContext.Current.Request.ApplicationPath;
                        if (applicationPath.EndsWith("/") == false && applicationPath.EndsWith(@"\") == false)
                            applicationPath += "/";
                        applicationPath += "configuration/config/api.config";
                        filename = System.Web.HttpContext.Current.Server.MapPath(applicationPath);
                    }
                }
                return filename;                 
            }
        }
        



        public static APIConfig GetConfig()
        {
            if (System.IO.File.Exists(ConfigFilePath))
                config = ConfigFileManage.LoadConfig(ref m_fileoldchange, ConfigFilePath, typeof(APIConfig)) as APIConfig;
            else
                config = null;
            return config ;

        }

        public static bool SaveConfig(APIConfig apiconfiginfo)
        {
            return ConfigFileManage.SaveConfig(ConfigFilePath, apiconfiginfo);
        }
    }

}
