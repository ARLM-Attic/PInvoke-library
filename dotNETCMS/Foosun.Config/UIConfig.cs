using System;
using System.Data;
using System.Configuration;
using System.IO;
using System.Xml;
using System.ComponentModel;

namespace Foosun.Config
{
    public class UIConfig
    {
        public static string WebDAL = ConfigurationManager.AppSettings["WebDAL"];
        public static string dataRe = ConfigurationManager.AppSettings["dataRe"];
        public static string mssql = ConfigurationManager.AppSettings["mssql"];
        //public static string CssPath = ConfigurationManager.AppSettings["manner"];
        public static string CssPath()
        {
            return BaseConfig.GetConfigValue("manner");
        }
        public static string returnCopyRight = verConfig.isTryversion + verConfig.helpcenterStr + verConfig.ForumStr;
        public static string HeadTitle = BaseConfig.GetConfigValue("headTitle");
        public static string sHeight = BaseConfig.GetConfigValue("sHeight");
        public static string sWidth = BaseConfig.GetConfigValue("sWidth");
        public static string isLinkTF = BaseConfig.GetConfigValue("isLinkTF");
        public static string dirMana = BaseConfig.GetConfigValue("dirMana");
        public static string dirUser = BaseConfig.GetConfigValue("dirUser");
        public static string dirDumm = BaseConfig.GetConfigValue("dirDumm");
        public static string UserdirFile = BaseConfig.GetConfigValue("UserdirFile");
        public static string protPass = BaseConfig.GetConfigValue("protPass");
        public static string protRand = BaseConfig.GetConfigValue("protRand");
        public static string dirTemplet = BaseConfig.GetConfigValue("dirTemplet");
        public static string dirSite = BaseConfig.GetConfigValue("dirSite");
        public static string dirFile = BaseConfig.GetConfigValue("dirFile");
        public static string dirHtml = BaseConfig.GetConfigValue("dirHtml");
        public static string saveContent = BaseConfig.GetConfigValue("saveContent");
        public static string publicType = Foosun.Config.verConfig.PublicType;
        public static string indeData = BaseConfig.GetConfigValue("indeData");
        public static string Logfilename = BaseConfig.GetConfigValue("Logfilename");
        public static string dirPige = BaseConfig.GetConfigValue("dirPige");
        public static string dirPigeDate = BaseConfig.GetConfigValue("dirPigeDate");
        public static string publicfreshinfo = BaseConfig.GetConfigValue("publicfreshinfo");
        public static string constPass = BaseConfig.GetConfigValue("constPass");
        public static string filePass = BaseConfig.GetConfigValue("filePass");
        public static string filePath = BaseConfig.GetConfigValue("filePath");
        public static string sqlConnData = BaseConfig.GetConfigValue("sqlConnData");
        public static string smtpserver = BaseConfig.GetConfigValue("smtpserver");
        public static string emailuserName = BaseConfig.GetConfigValue("emailuserName");
        public static string emailuserpwd = BaseConfig.GetConfigValue("emailuserpwd");
        public static string emailfrom = BaseConfig.GetConfigValue("emailfrom");
        public static string copyright = BaseConfig.GetConfigValue("copyRight");
        public static string titlemore = BaseConfig.GetConfigValue("titlemore");
        public static string commperPageNum = BaseConfig.GetConfigValue("commperPageNum");
        public static string splitPageCount = BaseConfig.GetConfigValue("splitPageCount");
        public static string enableAutoPage = BaseConfig.GetConfigValue("enableAutoPage");
        public static string titlenew = BaseConfig.GetConfigValue("titlenew");
        //时间：2008-07-15 修改者：吴静岚
        //增加连接是否弹出新窗口参数设置 开始
        public static string Linktagert = BaseConfig.GetConfigValue("Linktagert");
        public static string Linktagertimg = BaseConfig.GetConfigValue("Linktagertimg");
        //结束
        public string snportpass()
        {
            return BaseConfig.GetConfigValue("portpass");
        }
        /// <summary>
        /// 取得参数配置每页显示记录多少条
        /// </summary>
        /// <returns>返回数值型</returns>
        public static int GetPageSize()
        {

            int n = Convert.ToInt32(BaseConfig.GetConfigValue("PageSize"));
            if (n < 1)
                throw new Exception("每页记录条数不能小于1!");
            return n;
        }

        #region 刷新缓存
        /// <summary>
        /// 刷新缓存
        /// </summary>
        public static void RefurbishCatch()
        {
            UIConfig con = new UIConfig();
            HeadTitle = con.getCatchParam("headTitle");
            sHeight = con.getCatchParam("sHeight");
            sWidth = con.getCatchParam("sWidth");
            isLinkTF = con.getCatchParam("isLinkTF");
            dirMana = con.getCatchParam("dirMana");
            dirUser = con.getCatchParam("dirUser");
            dirDumm = con.getCatchParam("dirDumm");
            UserdirFile = con.getCatchParam("UserdirFile");
            protPass = con.getCatchParam("protPass");
            protRand = con.getCatchParam("protRand");
            dirTemplet = con.getCatchParam("dirTemplet");
            dirSite = con.getCatchParam("dirSite");
            dirFile = con.getCatchParam("dirFile");
            dirHtml = con.getCatchParam("dirHtml");
            saveContent = con.getCatchParam("saveContent");
            indeData = con.getCatchParam("indeData");
            Logfilename = con.getCatchParam("Logfilename");
            dirPige = con.getCatchParam("dirPige");
            dirPigeDate = con.getCatchParam("dirPigeDate");
            publicfreshinfo = con.getCatchParam("publicfreshinfo");
            constPass = con.getCatchParam("constPass");
            filePass = con.getCatchParam("filePass");
            filePath = con.getCatchParam("filePath");
            sqlConnData = con.getCatchParam("sqlConnData");
            smtpserver = con.getCatchParam("smtpserver");
            emailuserName = con.getCatchParam("emailuserName");
            emailuserpwd = con.getCatchParam("emailuserpwd");
            emailfrom = con.getCatchParam("emailfrom");
            copyright = con.getCatchParam("copyRight");
            titlemore = con.getCatchParam("titlemore");
            commperPageNum = con.getCatchParam("commperPageNum");
            splitPageCount = con.getCatchParam("splitPageCount");
            enableAutoPage = con.getCatchParam("enableAutoPage");
            titlenew = con.getCatchParam("titlenew");
        }
        /// <summary>
        /// 刷新缓存
        /// </summary>
        private string getCatchParam(string Target)
        {
            string path = System.Web.HttpContext.Current.Server.MapPath("~/xml/sys/foosun.config");
            System.Xml.XmlDocument xdoc = new XmlDocument();
            xdoc.Load(path);
            XmlElement root = xdoc.DocumentElement;
            XmlNodeList elemList = root.GetElementsByTagName(Target);
            try
            {
                return elemList[0].InnerText;
            }
            catch
            {
                return null;
            }
        }

        #endregion
    }
}
