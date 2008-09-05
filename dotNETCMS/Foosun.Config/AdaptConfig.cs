using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Foosun.Config
{
    public class AdaptConfig
    {
        private bool fs_isAdapt;
        /// <summary>
        /// 是否开启整合
        /// </summary>
        public bool isAdapt
        {
            set { fs_isAdapt = value; }
            get { return fs_isAdapt; }
        }       
        private string fs_adaptKey;
        /// <summary>
        /// 整合密码key
        /// </summary>
        public string adaptKey
        {
            set { fs_adaptKey = value; }
            get { return fs_adaptKey; }
        }        
        private string fs_adaptPath;
        /// <summary>
        /// 请求页面地址
        /// </summary>
        public string adaptPath
        {
            set { fs_adaptPath = value; }
            get { return fs_adaptPath; }
        }
        /// <summary>
        /// 构造函数，为字段赋初值
        /// </summary>
        public AdaptConfig(string xmlName)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();                
                xmlDoc.Load(xmlName);
                XmlNode xn = xmlDoc.SelectSingleNode("adapt");
                XmlElement xeIsAdapt = (XmlElement)xn.SelectSingleNode("isAdapt");
                XmlElement xeAdaptKey = (XmlElement)xn.SelectSingleNode("adaptKey");
                XmlElement xePagePath = (XmlElement)xn.SelectSingleNode("adaptPath");
                if (xeIsAdapt.InnerText.ToUpper() == "TRUE")
                {
                    fs_isAdapt = true;
                }
                else
                {
                    fs_isAdapt = false;
                }
                fs_adaptKey = xeAdaptKey.InnerText;
                fs_adaptPath = xePagePath.InnerText;                    
            }
            catch
            {
                //
            }
        }
        /// <summary>
        /// 更新AdaptConfig
        /// </summary>
        /// <returns></returns>
        public bool saveAdaptConfig(string xmlName)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();                
                xmlDoc.Load(xmlName);
                XmlNode xn = xmlDoc.SelectSingleNode("adapt");
                XmlElement xeIsAdapt = (XmlElement)xn.SelectSingleNode("isAdapt");
                XmlElement xeAdaptKey = (XmlElement)xn.SelectSingleNode("adaptKey");
                XmlElement xePagePath = (XmlElement)xn.SelectSingleNode("adaptPath");
                if (fs_isAdapt)
                {
                    xeIsAdapt.InnerText = "true";
                }
                else
                {
                    xeIsAdapt.InnerText = "false";
                }
                xeAdaptKey.InnerText = fs_adaptKey;
                xePagePath.InnerText = fs_adaptPath;
                xmlDoc.Save(xmlName);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
