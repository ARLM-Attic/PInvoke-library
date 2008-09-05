using System;
using System.IO;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Foosun.CMS;
using Foosun.CMS.Common;
using System.Xml;

namespace Foosun.CMS
{
    public class SearchEngine
    {
        /// <summary>
        /// 得到虚拟目录
        /// </summary>
        private static string _dirdumm = Foosun.Config.UIConfig.dirDumm;

        /// <summary>
        /// 是否生成百度搜索协议xml文件
        /// </summary>
        /// <returns>返回1或者0</returns>
        public static string IsBaidu()
        {
            string str = "0";
            if (_dirdumm.Trim() != "") { _dirdumm = "/" + _dirdumm; }
            try
            {

                if (!File.Exists(HttpContext.Current.Server.MapPath(_dirdumm + "/xml/sys/baiduSearch.xml")))
                {
                    throw new Exception("找不到配置文件(" + _dirdumm + "/xml/sys/baiduSearch.xml" + ").<li>可能是虚拟目录配置出错.请修改web.config</li>");
                }
                string xmlPath = HttpContext.Current.Server.MapPath(_dirdumm + "/xml/sys/baiduSearch.xml");
                FileInfo finfo = new FileInfo(xmlPath);
                System.Xml.XmlDocument xdoc = new XmlDocument();
                xdoc.Load(xmlPath);
                XmlElement root = xdoc.DocumentElement;
                XmlNodeList isbaidu1 = root.GetElementsByTagName("isbaidu");
                str = isbaidu1[0].InnerXml;
            }
            catch
            {
                throw new Exception("配置文件出错:" + _dirdumm + "/xml/sys/baiduSearch.xml" + "");
            }
            return str;
        }

        ///// <summary>
        ///// 获得最大临时表数
        ///// </summary>
        ///// <returns></returns>
        //public static int basenumber()
        //{
        //    int intflg = 1000;
        //    if (_dirdumm.Trim() != "") { _dirdumm = "/" + _dirdumm; }
        //    try
        //    {
        //        if (!File.Exists(HttpContext.Current.Server.MapPath(_dirdumm + "/xml/sys/base.xml"))) {throw new Exception("找不到配置文件(" + _dirdumm + "/xml/sys/base.xml" + ").<li>可能是虚拟目录配置出错.请修改web.config</li>"); }
        //        string xmlPath = HttpContext.Current.Server.MapPath(_dirdumm + "/xml/sys/base.xml");
        //        FileInfo finfo = new FileInfo(xmlPath);
        //        System.Xml.XmlDocument xdoc = new XmlDocument();
        //        xdoc.Load(xmlPath);
        //        XmlElement root = xdoc.DocumentElement;
        //        XmlNodeList number1 = root.GetElementsByTagName("number");
        //        intflg = int.Parse(number1[0].InnerXml);
        //    }
        //    catch { throw new Exception("配置文件出错:" + _dirdumm + "/xml/sys/base.xml" + ""); }
        //    return intflg;
        //}

        ///// <summary>
        ///// 获取删除新闻的日期数
        ///// </summary>
        ///// <returns></returns>
        //public static int datenumber()
        //{
        //    int intflg = 100;
        //    if (_dirdumm.Trim() != "") { _dirdumm = "/" + _dirdumm; }
        //    try
        //    {
        //        if (!File.Exists(HttpContext.Current.Server.MapPath(_dirdumm + "/xml/sys/base.xml"))) { throw new Exception("找不到配置文件(" + _dirdumm + "/xml/sys/base.xml" + ").<li>可能是虚拟目录配置出错.请修改web.config</li>"); }
        //        string xmlPath = HttpContext.Current.Server.MapPath(_dirdumm + "/xml/sys/base.xml");
        //        FileInfo finfo = new FileInfo(xmlPath);
        //        System.Xml.XmlDocument xdoc = new XmlDocument();
        //        xdoc.Load(xmlPath);
        //        XmlElement root = xdoc.DocumentElement;
        //        XmlNodeList datenumber1 = root.GetElementsByTagName("datenumber");
        //        intflg = int.Parse(datenumber1[0].InnerXml);
        //    }
        //    catch { throw new Exception("配置文件出错:" + _dirdumm + "/xml/sys/base.xml" + ""); }
        //    return intflg;
        //}

        ///// <summary>
        ///// 获取删除新闻的日期数
        ///// </summary>
        ///// <returns></returns>
        //public static int conditionnumbers()
        //{
        //    int intflg = 100;
        //    if (_dirdumm.Trim() != "") { _dirdumm = "/" + _dirdumm; }
        //    try
        //    {
        //        if (!File.Exists(HttpContext.Current.Server.MapPath(_dirdumm + "/xml/sys/base.xml"))) { throw new Exception("找不到配置文件(" + _dirdumm + "/xml/sys/base.xml" + ").<li>可能是虚拟目录配置出错.请修改web.config</li>"); }
        //        string xmlPath = HttpContext.Current.Server.MapPath(_dirdumm + "/xml/sys/base.xml");
        //        FileInfo finfo = new FileInfo(xmlPath);
        //        System.Xml.XmlDocument xdoc = new XmlDocument();
        //        xdoc.Load(xmlPath);
        //        XmlElement root = xdoc.DocumentElement;
        //        XmlNodeList conditionnumber1 = root.GetElementsByTagName("conditionnumber");
        //        intflg = int.Parse(conditionnumber1[0].InnerXml);
        //    }
        //    catch { throw new Exception("配置文件出错:" + _dirdumm + "/xml/sys/base.xml" + ""); }
        //    return intflg;
        //}

        /// <summary>
        /// 生成百度搜索新闻协议xml文件
        /// </summary>
        public static void RefreshBaidu()
        {
            ContentManage rd = new ContentManage();
            rootPublic pd = new rootPublic();
            int getnumber = 50;
            int getType = 0;
            string updatePeri = "60";
            string website = "www.foosun.net";
            string webmaster = "service@foosun.cn";
            StreamWriter sw = null;
            if (_dirdumm.Trim() != "") { _dirdumm = "/" + _dirdumm; }
            try
            {
                if (!File.Exists(HttpContext.Current.Server.MapPath(_dirdumm + "/xml/sys/baiduSearch.xml")))
                {
                    throw new Exception("找不到配置文件(" + _dirdumm + "/xml/sys/baiduSearch.xml" + ").<li>可能是虚拟目录配置出错.请修改web.config</li>");
                }
                string xmlPath = HttpContext.Current.Server.MapPath(_dirdumm + "/xml/sys/baiduSearch.xml");
                FileInfo finfo = new FileInfo(xmlPath);
                System.Xml.XmlDocument xdoc = new XmlDocument();
                xdoc.Load(xmlPath);
                XmlElement root = xdoc.DocumentElement;
                XmlNodeList number1 = root.GetElementsByTagName("number");
                XmlNodeList searchtype1 = root.GetElementsByTagName("searchtype");
                XmlNodeList updatePeri1 = root.GetElementsByTagName("updatePeri");
                XmlNodeList website1 = root.GetElementsByTagName("website");
                XmlNodeList webmaster1 = root.GetElementsByTagName("webmaster");
                getnumber = int.Parse(number1[0].InnerXml);
                getType = int.Parse(searchtype1[0].InnerXml);
                updatePeri = updatePeri1[0].InnerXml;
                website = website1[0].InnerXml;
                webmaster = webmaster1[0].InnerXml;
            }
            catch
            {
                throw new Exception("配置文件出错:" + _dirdumm + "/xml/sys/baiduSearch.xml");
            }
            string FileName = HttpContext.Current.Server.MapPath("~/baidu.xml");
            sw = File.CreateText(FileName);
            sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r");
            sw.WriteLine("<document>\r");
            sw.WriteLine("  <webMaster>" + webmaster + "</webMaster>\r");
            sw.WriteLine("  <webSite>http://" + website + "</webSite>\r");
            sw.WriteLine("  <updatePeri>" + updatePeri + "</updatePeri>\r");
            string urls = "";
            DataTable dt = rd.getLastFormTB();
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < getnumber; i++)
                {
                    if (dt.Rows.Count > i)
                    {
                        try
                        {
                            IDataReader drs = rd.getNewsID(dt.Rows[i]["NewsID"].ToString());
                            if (drs.Read())
                            {
                                sw.WriteLine("  <item>\r");
                                sw.WriteLine("      <title></title>\r");
                                if (drs["NewsType"].ToString() == "2")
                                {
                                    urls = drs["URLaddress"].ToString();
                                }
                                else
                                {
                                    DataTable dt1 = rd.getClassParam(drs["ClassID"].ToString());
                                    if (dt != null && dt.Rows.Count > 0)
                                    {
                                        if (Foosun.Common.Public.readparamConfig("ReviewType") == "1")
                                        {
                                            urls = "/content-" + drs["NewsID"].ToString() + ".aspx";
                                        }
                                        else
                                        {
                                            if (drs["isDelPoint"].ToString() != "0")
                                            {
                                                urls = "/content-" + drs["NewsID"].ToString() + ".aspx";
                                            }
                                            else
                                            {
                                                urls = "/" + dt1.Rows[0]["SavePath"].ToString() + "/" + dt1.Rows[0]["SaveClassframe"].ToString() + "/" + drs["SavePath"].ToString() + "/" + drs["FileName"].ToString() + drs["FileEXName"].ToString();
                                            }
                                        }
                                        urls = Foosun.Publish.CommonData.SiteDomain + urls.Replace("//", "/");
                                        dt1.Clear(); dt1.Dispose();
                                    }
                                }
                                sw.WriteLine("      <link>" + urls + "</link>\r");
                                sw.WriteLine("      <description>" + Foosun.Common.Input.LostHTML(drs["naviContent"].ToString()) + "</description>\r");
                                sw.WriteLine("      <text>" + Foosun.Common.Input.LostHTML(drs["Content"].ToString()) + "</text>\r");
                                if (drs["PicURL"].ToString().Trim() != "" && drs["PicURL"].ToString().Trim() != null) { sw.WriteLine("      <image>http://" + website + _dirdumm + (drs["PicURL"].ToString()).Replace("{@dirfile}", Foosun.Config.UIConfig.dirFile) + "</image>\r"); }
                                else { sw.WriteLine("      <image></image>\r"); }
                                sw.WriteLine("      <keywords>" + drs["Metakeywords"].ToString().Replace(",", " ") + "</keywords>\r");
                                sw.WriteLine("      <author>" + drs["Author"] + "</author>\r");
                                sw.WriteLine("      <source>" + drs["Souce"] + "</source>\r");
                                sw.WriteLine("      <pubDate>" + drs["CreatTime"] + "</pubDate>\r");
                                sw.WriteLine("  </item>\r");
                            }
                            drs.Close();
                        }
                        catch
                        {
                            continue;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                dt.Clear(); dt.Dispose();
            }
            sw.WriteLine("</document>\r");
            sw.Flush();
            sw.Close(); sw.Dispose();
        }

        ///// <summary>
        ///// 插入临时表
        ///// </summary>
        ///// <param name="Prot">传入的条件</param>
        ///// <param name="NewsID">新闻编号</param>
        ///// <param name="DataTable">新闻所属的表</param>
        //public static void insertTemplTable(string Prot, string NewsID, string DataTable)
        //{
        //    int getnumber = 1000;
        //    int getcondition = 100;
        //    int getdatenumber = 90;
        //    StreamWriter sw = null;
        //    if (_dirdumm.Trim() != "") { _dirdumm = "/" + _dirdumm; }
        //    try
        //    {
        //        if (!File.Exists(HttpContext.Current.Server.MapPath(_dirdumm + "/xml/sys/base.xml"))) { throw new Exception("找不到配置文件(" + _dirdumm + "/xml/sys/base.xml" + ").<li>可能是虚拟目录配置出错.请修改web.config</li>", ""); }
        //        string xmlPath = HttpContext.Current.Server.MapPath(_dirdumm + "/xml/sys/base.xml");
        //        FileInfo finfo = new FileInfo(xmlPath);
        //        System.Xml.XmlDocument xdoc = new XmlDocument();
        //        xdoc.Load(xmlPath);
        //        XmlElement root = xdoc.DocumentElement;
        //        XmlNodeList number1 = root.GetElementsByTagName("number");
        //        XmlNodeList conditionnumber1 = root.GetElementsByTagName("conditionnumber");
        //        XmlNodeList datenumber1 = root.GetElementsByTagName("datenumber");
        //        getnumber = int.Parse(number1[0].InnerXml);
        //        getcondition = int.Parse(conditionnumber1[0].InnerXml);
        //        getdatenumber = int.Parse(datenumber1[0].InnerXml);
        //    }
        //    catch { throw new Exception("配置文件出错:" + _dirdumm + "/xml/sys/baiduSearch.xml" + ""); }
        //    string[] getProt = Prot.Split(',');
        //    //推荐,滚动,热点,幻灯,头条,公告,WAP,精彩 格式如:0,1,1,0,1,0,0,1
        //    try
        //    {
        //        int isRec = int.Parse(getProt[0]);
        //        int isMarquee = int.Parse(getProt[1]);
        //        int isHOT = int.Parse(getProt[2]);
        //        int isFilt = int.Parse(getProt[3]);
        //        int isTT = int.Parse(getProt[4]);
        //        int isAnnouce = int.Parse(getProt[5]);
        //        int isWap = int.Parse(getProt[6]);
        //        int isJC = int.Parse(getProt[7]);
        //    }
        //    catch { throw new Exception("传入的参数有问题"); }
        //    //清除过期的数据
        //    ContentManage rd = new ContentManage();
        //    rd.delTBDateNumber(getdatenumber);
        //    //rd.delTBTypeNumber(Prot, getcondition);
        //}
    }
}