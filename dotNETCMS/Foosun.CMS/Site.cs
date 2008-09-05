//===========================================================
//==     (c)2007 Foosun Inc. by dotNETCMS 1.0              ==
//==             Forum:bbs.foosun.net                      ==
//==            website:www.foosun.net                     ==
//==     Address:NO.109 HuiMin ST.,Chengdu ,China          ==
//==         TEL:86-28-85098980/66026180                   ==
//==         qq:655071,MSN:ikoolls@gmail.com               ==
//==               Code By JiangDong                       ==
//===========================================================
using System;
using System.Text;
using System.Data;
using System.IO;
using System.Xml;
using Foosun.DALFactory;
using Foosun.Model;

namespace Foosun.CMS
{
    /// <summary>
    /// 频道管理类
    /// </summary>
    public class Site
    {
        private ISite dal;
        /// <summary>
        /// 构造函数
        /// </summary>
        public Site()
        {
            dal = DataAccess.CreateSite();
        }
        /// <summary>
        /// 新增一个频道
        /// </summary>
        /// <param name="site">新增频道的相关信息</param>
        /// <returns>返回新增频道的自增长ＩＤ</returns>
        public int Add(STSite site)
        {
            string SiteID;
            site = FieldsHandle(site);
            int n = dal.Add(site,Foosun.Global.Current.SiteID,out SiteID);
            CreateSiteFile(SiteID, site.EName, site.CName);
            return n;
        }
        /// <summary>
        /// 删除一个或多个频道
        /// </summary>
        /// <param name="id">要删除的频道的编号，如果是多个则以逗号间隔</param>
        public void Delete(string id)
        {
            Exception e;
            string[] DelsPath;
            string rootpath = Foosun.Common.ServerInfo.GetRootPath();
            bool flag = dal.Delete(id, out e, out DelsPath);
            if (!flag)
            {
                throw e;
            }
            else
            {
                foreach (string Path in DelsPath)
                {
                    try
                    {
                        string _Path = rootpath + Path;
                        if (File.Exists(_Path))
                            File.Delete(_Path);
                    }
                    catch
                    {
                        ////////////////////应写日志
                    }
                }
            }
        }
        /// <summary>
        /// 将一个或多个频道放入回收站
        /// </summary>
        /// <param name="id">频道的编号，如果是多个，则以逗号间隔</param>
        public void Recyle(string id)
        {
            dal.Recyle(id);
        }
        /// <summary>
        /// 修改频道信息
        /// </summary>
        /// <param name="id">要修改的频道编号</param>
        /// <param name="site">频道的相关信息</param>
        public void Update(int id,STSite site)
        {
            site = FieldsHandle(site);
            dal.Update(id,site);
            CreateSiteFile(site.ChannelID, site.EName, site.CName);
        }
        /// <summary>
        /// 获取频道的列表
        /// </summary>
        /// <param name="sttype">要获得的频道的类型（为内部还是外部还是所有）</param>
        /// <returns>返回数据集</returns>
        public DataTable List(SiteType sttype)
        {
            return dal.List(sttype);
        }

        /// <summary>
        /// 返回频道列表
        /// </summary>
        /// <returns></returns>
        public IDataReader siteList()
        {
            return dal.siteList();
        }

        /// <summary>
        /// 获取某个单一频道的信息（主要用于频道修改）
        /// </summary>
        /// <param name="id">要获取的频道的编号</param>
        /// <returns>数据集</returns>
        public DataTable GetSingle(int id)
        {
            return dal.GetSingle(id);
        }

        /// <summary>
        /// 获取某个单一频道的信息（主要用于频道修改）
        /// </summary>
        /// <param name="id">要获取的频道的12位随机编号</param>
        /// <returns>数据集</returns>
        public DataTable GetSiteInfo(string ChannelID)
        {
            return dal.GetSiteInfo(ChannelID);
        }

        /// <summary>
        /// 用于新增频道和修改频道时，对相关的值进行设定
        /// </summary>
        /// <param name="site">频道的相关信息</param>
        /// <returns>处理后的信息</returns>
        private STSite FieldsHandle(STSite site)
        {
            if (site.IsURL == 1)
            {
                #region 选择为外部频道时
                site.IndexTemplet = "";
                site.ClassTemplet = "";
                site.ReadNewsTemplet = "";
                site.SpecialTemplet = "";
                site.Domain = "";
                site.isCheck = -1;
                site.Keywords = "";
                site.ContrTF = 0;
                site.UpfileSize = -1;
                site.UpfileType = "";
                site.SaveFileType = -1;
                site.SaveType = -1;
                site.PicSavePath = "";
                site.SaveDirPath = "";
                site.SaveDirRule = "";
                site.SaveFileRule = "";
                site.ClassEXName = "";
                site.SpecialEXName = "";
                site.IndexEXName = "";
                site.NewsEXName = "";
                #endregion 选择为外部频道时
            }
            else
            {
                site.Urladdress = "";
            }
            return site;
        }
        /// <summary>
        /// 建立频道的ＸＭＬ文件
        /// </summary>
        /// <param name="siteid">频道的编号</param>
        /// <param name="ename">频道英文名称</param>
        /// <param name="cname">频道中文名称</param>
        private void CreateSiteFile(string siteid,string ename,string cname)
        {
            string pathroot = Foosun.Common.ServerInfo.GetRootPath() + "\\" + Foosun.Config.UIConfig.dirSite + "\\" + ename;
            if (!Directory.Exists(pathroot))
                Directory.CreateDirectory(pathroot);
            string filepath = pathroot +"\\site.xml";
            if (File.Exists(filepath))
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(filepath);
                XmlNode node = xml.DocumentElement;//移到rss节点 
                node = node.FirstChild;//移到siteparm子节点 
                node = node.FirstChild;//移到item子节点 
                node = node.FirstChild;//移到sitename子节点 
                node.InnerText = cname;//修改内容 
                xml.Save(filepath);//保存 
            }
            else
            {
                XmlTextWriter xmlwrt = new XmlTextWriter(filepath, Encoding.UTF8);
                xmlwrt.Formatting = Formatting.Indented;
                xmlwrt.WriteStartDocument();
                xmlwrt.WriteStartElement("rss");
                xmlwrt.WriteAttributeString("version", "2.0");
                xmlwrt.WriteStartElement("siteparm");
                xmlwrt.WriteStartElement("item");
                xmlwrt.WriteElementString("sitename", cname);
                xmlwrt.WriteElementString("siteid", siteid);
                xmlwrt.WriteEndElement();
                xmlwrt.WriteEndElement();
                xmlwrt.WriteEndElement();
                xmlwrt.WriteEndDocument();
                xmlwrt.Flush();
                xmlwrt.Close();
            }
        }

        /// <summary>
        /// 得到站点栏目数
        /// </summary>
        /// <param name="siteid"></param>
        /// <returns></returns>
        public int getsiteClassCount(string siteid)
        {
            return dal.getsiteClassCount(siteid);
        }
    }
}
