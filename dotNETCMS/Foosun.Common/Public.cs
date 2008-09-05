using System;
using System.Data;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Text.RegularExpressions;
using Foosun.Global;
using System.Net.Mail;

namespace Foosun.Common
{
    /// <summary>
    /// Class1 的摘要说明
    /// </summary>
    public class Public
    {

        /// <summary>
        /// 得到站点用户IP, IpSTR = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString()
        /// </summary>
        /// <returns></returns>
        public static string getUserIP()
        {
            return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
        }

        /// <summary>
        /// 去除字符串最后一个','号
        /// </summary>
        /// <param name="chr">:要做处理的字符串</param>
        /// <returns>返回已处理的字符串</returns>
        /// /// CreateTime:2007-03-26 Code By DengXi
        public static string Lost(string chr)
        {
            if (chr == null || chr == string.Empty)
            {
                return "";
            }
            else
            {
                chr = chr.Remove(chr.LastIndexOf(","));
                return chr;
            }
        }

        //  
        public static string lostfirst(string chr)
        {
            string flg = "";
            if (chr != string.Empty || chr != null)
            {
                if (chr.Substring(0, 1) == "/")
                    flg = chr.Replace(chr.Substring(0, 1), "");
                else
                    flg = chr;
            }
            return flg;
        }

        //public static void sendMail(string smtpserver,string userName,string pwd, string strfrom, string strto, string subj, string bodys)
        //{
        //    SmtpClient mail = new System.Net.Mail.SmtpClient();
        //    mail.Host = smtpserver;//smtp
        //    mail.Credentials = new System.Net.NetworkCredential(userName, pwd);
        //    //mail.Credentials.GetCredential = new System.Net.NetworkCredential(userName, pwd); ;
        //    //mail.EnableSsl = ssl;//发送连接套接层是否加密 例如用gmail发是加密的 
        //    System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage(strfrom, strto);
        //    message.Body = bodys;
        //    message.Subject = subj;
        //    message.SubjectEncoding = System.Text.Encoding.GetEncoding("gb2312");
        //    message.BodyEncoding = System.Text.Encoding.GetEncoding("gb2312");
        //    message.IsBodyHtml = true;
        //    //message.
        //    mail.Send(message);
        //}

        /// <summary>
        /// 发送电子邮件
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="subj"></param>
        /// <param name="bodys"></param>

        public static void sendMail(string smtpserver, string userName, string pwd, string strfrom, string strto, string subj, string bodys)
        {
            //bug修改当参数不正确时的处理,by arjun
            try
            {
                SmtpClient _smtpClient = new SmtpClient();
                _smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件发送方式
                _smtpClient.Host = smtpserver; ;//指定SMTP服务器
                _smtpClient.Credentials = new System.Net.NetworkCredential(userName, pwd);//用户名和密码

                MailMessage _mailMessage = new MailMessage(strfrom, strto);
                _mailMessage.Subject = subj;//主题
                _mailMessage.Body = bodys;//内容
                _mailMessage.BodyEncoding = System.Text.Encoding.Default;//正文编码
                _mailMessage.IsBodyHtml = true;//设置为HTML格式
                _mailMessage.Priority = MailPriority.High;//优先级
                _smtpClient.Send(_mailMessage);
            }
            catch
            {
                //不处理发送邮件错误
            }
        } 


        /// <summary>
        /// 读取web.config相关数据信息
        /// </summary>
        /// <param name="xmlTargetElement">相关字节</param>
        /// <returns></returns>
        /// 编写时间2007-03-08  y.xiaobin(著)
        public static string getXmlElementValue(string xmlTargetElement)
        {
            return System.Configuration.ConfigurationManager.AppSettings[xmlTargetElement];
        }

        /// <summary>
        /// web.config相关文件操作
        /// 0检测是web.config是否为只读或可写;返回值为:true或false,1把web.config改写为只读;2把web.config改写为可写
        /// 在此函数中自动去根目下寻找web.config
        /// </summary>
        /// <param name="flg">0检测是web.config是否为只读或可写;返回值为:true或false,1把web.config改写为只读;2把web.config改写为可写</param>
        /// 2007-5-9 y.xiaobin
        /// <returns></returns>
        public static bool constReadOnly(int num)
        {
            bool _readonly = false;
            string _config = HttpContext.Current.Server.MapPath(@"~/Web.config");
            FileInfo fi = new FileInfo(_config);
            switch (num)
            {
                case 0: _readonly = fi.IsReadOnly; break;
                case 1:
                    fi.IsReadOnly = true;
                    _readonly = true;
                    break;
                case 2:
                    {
                        fi.IsReadOnly = false;
                        _readonly = false;
                    }
                    break;
                default: throw new Exception("错误参数!");
            }

            return _readonly;

        }

        /// <summary>
        /// web.config相关文件操作
        /// 0检测是web.config是否为只读或可写;返回值为:true或false,1把config改写为只读;2把web.config改写为可写
        /// 在此函数中自动去根目下寻找web.config
        /// </summary>
        /// <param name="flg">0检测是web.config是否为只读或可写;返回值为:true或false,1把web.config改写为只读;2把web.config改写为可写</param>
        /// 2007-5-9 y.xiaobin
        /// <returns></returns>
        public static bool constReadOnly(int num,string strSource)
        {
            bool _readonly = false;
            string _config = HttpContext.Current.Server.MapPath(@"~/" + strSource);
            FileInfo fi = new FileInfo(_config);
            switch (num)
            {
                case 0: _readonly = fi.IsReadOnly; break;
                case 1:
                    fi.IsReadOnly = true;
                    _readonly = true;
                    break;
                case 2:
                    {
                        fi.IsReadOnly = false;
                        _readonly = false;
                    }
                    break;
                default: throw new Exception("错误参数!");
            }

            return _readonly;

        }
        /// <summary>
        /// 保存web.config设置
        /// </summary>
        /// <param name="xmlTargetElement">关键字</param>
        /// <param name="xmlText">value</param>
        /// 2007.05.09 修改 y.xiaobin
        public static void SaveXmlElementValue(string xmlTargetElement, string xmlText)
        {
            string returnInt = null;
            string filename = HttpContext.Current.Server.MapPath("~") + @"/Web.config";
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(filename);
            XmlNodeList topM = xmldoc.DocumentElement.ChildNodes;
            foreach (XmlNode element in topM)
            {
                if (element.Name == "appSettings")
                {
                    XmlNodeList node = element.ChildNodes;
                    if (node.Count > 0)
                    {
                        foreach (XmlNode el in node)
                        {
                            if (el.Name == "add")
                            {
                                if (el.Attributes["key"].InnerXml == xmlTargetElement)
                                {
                                    //保存web.config数据
                                    el.Attributes["value"].Value = xmlText;
                                    xmldoc.Save(HttpContext.Current.Server.MapPath(@"~/Web.config"));
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        returnInt = "Web.Config配置文件未配置";
                    }
                    break;
                }
                else
                {
                    returnInt = "Web.Config配置文件未配置";
                }
            }

            if (returnInt != null)
                throw new Exception(returnInt);
        }

        

        /// <summary>
        /// 删除文件夹,文件
        /// </summary>
        /// <param name="DirPath">文件夹路径</param>
        /// <param name="FilePath">文件路径</param>
        /// <returns>删除</returns>
        /// /// CreateTime:2007-03-28 Code By DengXi    
        public static void DelFile(string DirPath, string FilePath)
        {
            try
            {
                if (System.IO.File.Exists(FilePath))
                {
                    System.IO.File.Delete(FilePath);
                }
                if (System.IO.Directory.Exists(DirPath))
                {
                    System.IO.Directory.Delete(DirPath);
                }
            }
            catch {  }
        }

        /// <summary>
        /// 得到SQL语句的SiID;getstr = " and SiteID='" + Foosun.Global.Current.SiteID + "'"
        /// </summary>
        /// <returns></returns>
        public static string getSessionStr()
        {
            string getstr = "";
            if (Foosun.Global.Current.SiteID != "0"){getstr = " and SiteID='" + Foosun.Global.Current.SiteID + "'";}
            return getstr;
        }

        /// <summary>
        /// 得到频道ID; and ChannelID='" + Foosun.Global.Current.SiteID + "'
        /// </summary>
        /// <returns></returns>
        public static string getCHStr()
        {
            string getstr = "";
            if (Foosun.Global.Current.SiteID != "0") { getstr = " and ChannelID='" + Foosun.Global.Current.SiteID + "'"; }
            return getstr;
        }

        /// <summary>
        /// 生成XML文件
        /// </summary>
        /// <param name="Ename"></param>
        public static void saveClassXML(string Ename)
        {
            StreamWriter sw = null;
            if (Foosun.Global.Current.SiteID != "0")
            {

            }
            else
            {
                string FileName = HttpContext.Current.Server.MapPath("~/xml/Content/" + Ename + ".xml");
                if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/xml/Content")))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/xml/Content"));
                }
                sw = File.CreateText(FileName);
                sw.WriteLine("<?xml version=\"1.0\" encoding=\"gb2312\"?>\r");
                sw.WriteLine("<rss version=\"2.0\">\r");
                sw.WriteLine("<channel>\r");
                sw.WriteLine("<item>\r");
                sw.WriteLine("</item>\r");
                sw.WriteLine("</channel>\r");
                sw.WriteLine("</rss>\r");
                sw.Flush();
                sw.Close(); sw.Dispose();
            }
        }

        /// <summary>
        /// 保存系统参数
        /// </summary>
        /// <param name="siteName"></param>
        /// <param name="siteDomain"></param>
        /// <param name="linkTypeConfig"></param>
        /// <param name="ReviewType"></param>
        /// <param name="IndexTemplet"></param>
        /// <param name="IndexFileName"></param>
        public static bool saveparamconfig(string siteName, string siteDomain, int linkTypeConfig, int ReviewType, string IndexTemplet, string IndexFileName, string LenSearch, string SaveIndexPage, string InsertPicPosition, string collectTF, string HistoryNum, bool publishState, string PageStyle, string PageLinkCount)
        {
            bool sta = false;
            StreamWriter sw = null;
            if (Foosun.Global.Current.SiteID != "0")
            {

            }
            else
            {
                try
                {
                    string FileName = HttpContext.Current.Server.MapPath("~/xml/sys/base.config");
                    if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/xml/sys")))
                    {
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/xml/sys"));
                    }
                    sw = File.CreateText(FileName);
                    sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r");
                    sw.WriteLine("<siteconfig>\r");
                    sw.WriteLine("  <siteinfo>\r");
                    sw.WriteLine("      <sitename>" + siteName + "</sitename>\r");
                    sw.WriteLine("      <siteDomain>" + siteDomain + "</siteDomain>\r");
                    sw.WriteLine("      <linkTypeConfig>" + linkTypeConfig + "</linkTypeConfig>\r");
                    sw.WriteLine("      <ReviewType>" + ReviewType + "</ReviewType>\r");
                    sw.WriteLine("      <IndexTemplet>" + IndexTemplet + "</IndexTemplet>\r");
                    sw.WriteLine("      <IndexFileName>" + IndexFileName + "</IndexFileName>\r");
                    sw.WriteLine("      <LenSearch>" + LenSearch + "</LenSearch>\r");
                    sw.WriteLine("      <SaveIndexPage>" + SaveIndexPage + "</SaveIndexPage>\r");
                    sw.WriteLine("      <InsertPicPosition>" + InsertPicPosition + "</InsertPicPosition>\r");
                    sw.WriteLine("      <collectTF>" + collectTF + "</collectTF>\r");
                    sw.WriteLine("      <HistoryNum>" + HistoryNum + "</HistoryNum>\r");
                    sw.WriteLine("      <publishState>" + publishState + "</publishState>\r");
                    sw.WriteLine("      <PageStyle>" + PageStyle + "</PageStyle>\r");
                    sw.WriteLine("      <PageLinkCount>" + PageLinkCount + "</PageLinkCount>\r");
                    sw.WriteLine("  </siteinfo>\r");
                    sw.WriteLine("</siteconfig>\r");
                    sw.Flush();
                    sw.Close(); sw.Dispose();
                    sta = true;
                }
                catch
                {
                    sta = false;
                }
            }
            return sta;
        }

        /// <summary>
        /// 保存分组刷新参数
        /// </summary>
        /// <param name="classlistNumber"></param>
        /// <param name="infoNumber"></param>
        /// <param name="delinfoNumber"></param>
        /// <param name="specialNumber"></param>
        /// <returns></returns>
        public static bool saveRefreshConfig(string classlistNumber,string infoNumber,string delinfoNumber,string specialNumber)
        {
            bool sta = false;
            StreamWriter sw = null;
            if (Foosun.Global.Current.SiteID != "0")
            {

            }
            else
            {
                try
                {
                    string FileName = HttpContext.Current.Server.MapPath("~/xml/sys/refresh.config");
                    if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/xml/sys")))
                    {
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/xml/sys"));
                    }
                    sw = File.CreateText(FileName);
                    sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r");
                    sw.WriteLine("<siteconfig>\r");
                    sw.WriteLine("  <siteinfo>\r");
                    sw.WriteLine("      <classlistNumber>" + classlistNumber + "</classlistNumber>\r");
                    sw.WriteLine("      <infoNumber>" + infoNumber + "</infoNumber>\r");
                    sw.WriteLine("      <delinfoNumber>" + delinfoNumber + "</delinfoNumber>\r");
                    sw.WriteLine("      <specialNumber>" + specialNumber + "</specialNumber>\r");
                    sw.WriteLine("  </siteinfo>\r");
                    sw.WriteLine("</siteconfig>\r");
                    sw.Flush();
                    sw.Close(); sw.Dispose();
                    sta = true;
                }
                catch
                {
                    sta = false;
                }
            }
            return sta;
        }

        /// <summary>
        /// 读取配置
        /// </summary>
        /// <param name="strTarget">接点名</param>
        /// <returns></returns>
        public static string readparamConfig(string strTarget)
        {
            string rstr = "";
            string xmlPath = null;
            if (HttpContext.Current == null)
            {
                xmlPath = HttpRuntime.AppDomainAppPath + "/xml/sys/base.config";
            }
            else
            {
                xmlPath = HttpContext.Current.Server.MapPath("~/xml/sys/base.config");
            }
            FileInfo finfo = new FileInfo(xmlPath);
            System.Xml.XmlDocument xdoc = new XmlDocument();
            xdoc.Load(xmlPath);
            XmlElement root = xdoc.DocumentElement;
            XmlNodeList elemList = root.GetElementsByTagName(strTarget);
            rstr += elemList[0].InnerXml;
            return rstr;
        }

        /// <summary>
        /// 读取频道配置
        /// </summary>
        public static string readCHparamConfig(string strTarget,int ChID)
        {
            string rstr = "";
            string xmlPath = HttpContext.Current.Server.MapPath("~/xml/sys/Channel/ChParams/CH_" + ChID.ToString() + ".config");
            FileInfo finfo = new FileInfo(xmlPath);
            System.Xml.XmlDocument xdoc = new XmlDocument();
            xdoc.Load(xmlPath);
            XmlElement root = xdoc.DocumentElement;
            XmlNodeList elemList = root.GetElementsByTagName(strTarget);
            rstr += elemList[0].InnerXml;
            return rstr;
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        /// <param name="strTarget">接点名</param>
        /// <returns></returns>
        public static void SaveXmlConfig(string strTarget,string strValue,string strSource)
        {
            string xmlPath = HttpContext.Current.Server.MapPath("~/" + strSource);
            System.Xml.XmlDocument xdoc = new XmlDocument();
            xdoc.Load(xmlPath);
            XmlElement root = xdoc.DocumentElement;
            XmlNodeList elemList = root.GetElementsByTagName(strTarget);
            elemList[0].InnerXml = strValue;
            xdoc.Save(xmlPath);
        }

        /// <summary>
        /// 读取配置
        /// </summary>
        /// <param name="strTarget">接点名</param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string readparamConfig(string strTarget,string type)
        {
            string rstr = "";
            if (type != null && type != string.Empty)
            {
                string xmlPath = null;
                if (HttpContext.Current == null)
                {
                    xmlPath = HttpRuntime.AppDomainAppPath + "/xml/sys/" + type + ".config";
                }
                else
                {
                    xmlPath = HttpContext.Current.Server.MapPath("~/xml/sys/" + type + ".config");
                }
                FileInfo finfo = new FileInfo(xmlPath);
                System.Xml.XmlDocument xdoc = new XmlDocument();
                xdoc.Load(xmlPath);
                XmlElement root = xdoc.DocumentElement;
                XmlNodeList elemList = root.GetElementsByTagName(strTarget);
                rstr += elemList[0].InnerXml;
            }
            else
            {
                rstr = readparamConfig(strTarget);
            }
            return rstr;
        }

        public static void saveLogFiles(int _num, string UserNum, string Title, string Content)
        {
            StreamWriter sw = null;
            DateTime date = DateTime.Now;
            string FileName = date.Year + "-" + date.Month;
            try
            {
                FileName = HttpContext.Current.Server.MapPath("~/Logs/User-" + _num + "-") + FileName + "-" + Foosun.Common.Input.MD5(FileName) + "-s.log";

                #region 检测日志目录是否存在
                if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/Logs")))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Logs"));
                }

                if (!File.Exists(FileName))
                    sw = File.CreateText(FileName);
                else
                {
                    sw = File.AppendText(FileName);
                }
                #endregion

                sw.WriteLine("IP                 :" + Foosun.Common.Public.getUserIP() + "\r");
                sw.WriteLine("title              :" + Title + "\r");
                sw.WriteLine("content            :" + Content);
                sw.WriteLine("usernum&UserName   :" + UserNum + "\r");
                sw.WriteLine("Time               :" + System.DateTime.Now);
                sw.WriteLine("≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡\r");
                sw.Flush();
            }
            finally
            {
                if (sw != null)
                    sw.Close();
            }
        }

        /// <summary>
        /// 发布日志
        /// </summary>
        /// <param name="item"></param>
        /// <param name="errorContent"></param>
        /// <param name="username"></param>
        public static void savePublicLogFiles(string item, string errorContent, string username)
        {
            StreamWriter sw = null;
            DateTime date = DateTime.Now;
            string FileName = Foosun.Config.UIConfig.Logfilename;
            try
            {
                if (HttpContext.Current == null)
                {
                    FileName = HttpRuntime.AppDomainAppPath + "/Logs/public/" + FileName + "_" + date.Month + date.Day + ".log";
                }
                else
                {
                    FileName = HttpContext.Current.Server.MapPath("~/Logs/public/" + FileName + "_" + date.Month + date.Day + ".log");
                }
                #region 检测日志目录是否存在
                string forderPathStr = null;
                if (HttpContext.Current == null)
                {
                    forderPathStr = HttpRuntime.AppDomainAppPath + "/Logs";
                }
                else
                {
                    forderPathStr = HttpContext.Current.Server.MapPath("~/Logs");
                }
                if (!Directory.Exists(forderPathStr))
                {
                    Directory.CreateDirectory(forderPathStr);
                }

                if (!File.Exists(FileName))
                {
                    sw = File.CreateText(FileName);
                }
                else
                {
                    sw = File.AppendText(FileName);
                }
                #endregion
                sw.WriteLine(item);
                sw.WriteLine(errorContent);
                sw.WriteLine("【UserName】" + username + "   【Time】" + System.DateTime.Now);
                sw.WriteLine("≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡\r");
                sw.Flush();
            }
            finally
            {
                if (sw != null)
                    sw.Close();
            }
        }        
        
        /// <summary>
        /// 转换日志
        /// </summary>
        /// <param name="item"></param>
        /// <param name="errorContent"></param>
        /// <param name="username"></param>
        public static void saveConvertLogFiles(string NewsID, string errorContent)
        {
            StreamWriter sw = null;
            DateTime date = DateTime.Now;
            string FileName = Foosun.Config.UIConfig.Logfilename;
            try
            {
                FileName = HttpContext.Current.Server.MapPath("~/Logs/convert_" + FileName + "_" + date.Month + date.Day + ".log");

                #region 检测日志目录是否存在
                if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/Logs")))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Logs"));
                }

                if (!File.Exists(FileName))
                {
                    sw = File.CreateText(FileName);
                }
                else
                {
                    sw = File.AppendText(FileName);
                }
                #endregion
                sw.WriteLine(NewsID);
                sw.WriteLine(errorContent);
                sw.WriteLine("【Time】" + System.DateTime.Now);
                sw.WriteLine("≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡\r");
                sw.Flush();
            }
            finally
            {
                if (sw != null)
                    sw.Close();
            }
        }


        /// <summary> 
        /// 汉字转拼音缩写 
        /// 2004-11-30 
        /// </summary> 
        /// <param name="Input">要转换的汉字字符串</param> 
        /// <returns>拼音缩写</returns> 
        public static string GetPYString(string Input)
        {
            string ret = "";
            foreach (char c in Input)
            {
                if ((int)c >= 33 && (int)c <= 126)
                {//字母和符号原样保留 
                    ret += c.ToString();
                }
                else
                {//累加拼音声母 
                    ret += GetPYChar(c.ToString());
                }
            }
            return ret;
        }

        /// <summary> 
        /// 取单个字符的拼音声母 
        /// 2004-11-30 
        /// </summary> 
        /// <param name="c">要转换的单个汉字</param> 
        /// <returns>拼音声母</returns> 
        private static string GetPYChar(string c)
        {
            byte[] array = new byte[2];
            array = System.Text.Encoding.Default.GetBytes(c);
            int i = (short)(array[0] - '\0') * 256 + ((short)(array[1] - '\0'));
            if (i < 0xB0A1) return "*";
            if (i < 0xB0C5) return "A";
            if (i < 0xB2C1) return "B";
            if (i < 0xB4EE) return "C";
            if (i < 0xB6EA) return "D";
            if (i < 0xB7A2) return "E";
            if (i < 0xB8C1) return "F";
            if (i < 0xB9FE) return "G";
            if (i < 0xBBF7) return "H";
            if (i < 0xBFA6) return "G";
            if (i < 0xC0AC) return "K";
            if (i < 0xC2E8) return "L";
            if (i < 0xC4C3) return "M";
            if (i < 0xC5B6) return "N";
            if (i < 0xC5BE) return "O";
            if (i < 0xC6DA) return "P";
            if (i < 0xC8BB) return "Q";
            if (i < 0xC8F6) return "R";
            if (i < 0xCBFA) return "S";
            if (i < 0xCDDA) return "T";
            if (i < 0xCEF4) return "W";
            if (i < 0xD1B9) return "X";
            if (i < 0xD4D1) return "Y";
            if (i < 0xD7FA) return "Z";
            return "*";
        }

        /// <summary>
        /// 获得样式
        /// </summary>
        /// <param name="formName">表单名</param>
        /// <param name="Dir">xml源</param>
        /// <returns></returns>
        /// by Simplt.xie
        public static string getxmlstylelist(string formName, string Dir)
        {
            string _Str = "<select style=\"width:150px;\" class=\"form\" name=\"" + formName + "\" onchange=\"javascript:getValue(this.value);\">\r";
            string xmlPath = HttpContext.Current.Server.MapPath(Dir);
            FileInfo finfo = new FileInfo(xmlPath);
            System.Xml.XmlDocument xdoc = new XmlDocument();
            xdoc.Load(xmlPath);
            XmlElement root = xdoc.DocumentElement;
            XmlNodeList elemList = root.GetElementsByTagName("stylename");
            XmlNodeList elemList1 = root.GetElementsByTagName("stylevalue");
            for (int i = 0; i < elemList.Count; i++)
            {
                string _i = (i + 1).ToString();
                if (_i.Length < 2) { _i = "0" + _i; }
                _Str += "<option value=\"" + elemList1[i].InnerXml + "\">" + (_i) + ".&nbsp;" + elemList[i].InnerXml + "</option>\r";
            }
            _Str += "</select>\r";
            return _Str;
        }

        /// <summary>
        /// 读取模型配置
        /// </summary>
        /// <param name="Number"></param>
        /// <returns></returns>
        public static string getModelContentType(string Number)
        {
            string disable = string.Empty;
            if (Number.Trim() != "999")
            {
                disable = "disabled=\"disabled\"";
            }
            string _Str = "<select " + disable + " style=\"width:310px;\" class=\"form\" name=\"channelType\">\r";
            string xmlPath = HttpContext.Current.Server.MapPath("~/xml/sys/channel/channelbase.config");
            FileInfo finfo = new FileInfo(xmlPath);
            System.Xml.XmlDocument xdoc = new XmlDocument();
            xdoc.Load(xmlPath);
            XmlElement root = xdoc.DocumentElement;
            XmlNodeList elemList = root.GetElementsByTagName("channelNumber");
            XmlNodeList elemList1 = root.GetElementsByTagName("channelName");
            for (int i = 0; i < elemList.Count; i++)
            {
                if (Number == elemList[i].InnerXml)
                {
                    _Str += "<option value=\"" + elemList[i].InnerXml + "\" selected>" + elemList1[i].InnerXml + "</option>\r";
                }
                else
                {
                    _Str += "<option value=\"" + elemList[i].InnerXml + "\">" + elemList1[i].InnerXml + "</option>\r";
                }
            }
            _Str += "</select>\r";
            return _Str;
        }

        /// <summary>
        /// 读取模型类型默认的自定义字段
        /// </summary>
        /// <param name="Number"></param>
        /// <returns></returns>
        public static string getModelContentField(string Number)
        {
            string _Str = string.Empty;
            string xmlPath = HttpContext.Current.Server.MapPath("~/xml/sys/channel/channelbase.config");
            FileInfo finfo = new FileInfo(xmlPath);
            System.Xml.XmlDocument xdoc = new XmlDocument();
            xdoc.Load(xmlPath);
            XmlElement root = xdoc.DocumentElement;
            XmlNodeList elemList = root.GetElementsByTagName("channelNumber");
            XmlNodeList elemList1 = root.GetElementsByTagName("channelvalue");
            for (int i = 0; i < elemList.Count; i++)
            {
                if (Number.Trim() == elemList[i].InnerXml.Trim())
                {
                    _Str = elemList1[i].InnerXml.Trim();
                    break;
                }
                else
                {
                    continue;
                }
            }
            return _Str;
        }

        /// <summary>
        /// 读取字段配置
        /// </summary>
        /// <param name="Number"></param>
        /// <returns></returns>
        public static string getValueType(string Number)
        {
            string disable = string.Empty;
            if (Number.Trim() != "999")
            {
                disable = "disabled=\"disabled\"";
            }
            string _Str = "<select " + disable + " style=\"width:280px;\" class=\"form\" name=\"vType\" onchange=\"javascript:getValue(this.value);\">\r";
            string xmlPath = HttpContext.Current.Server.MapPath("~/xml/sys/channel/value.config");
            FileInfo finfo = new FileInfo(xmlPath);
            System.Xml.XmlDocument xdoc = new XmlDocument();
            xdoc.Load(xmlPath);
            XmlElement root = xdoc.DocumentElement;
            XmlNodeList elemList = root.GetElementsByTagName("valuetype");
            XmlNodeList elemList1 = root.GetElementsByTagName("valueName");
            string istr = string.Empty;
            for (int i = 0; i < elemList.Count; i++)
            {

                if (i < 9)
                {
                    istr = "0" + (i + 1).ToString()+".";
                }
                else
                {
                    istr = (i + 1).ToString()+".";
                }
                if (Number == elemList[i].InnerXml)
                {
                    _Str += "<option value=\"" + elemList[i].InnerXml + "\" selected>" + istr + elemList1[i].InnerXml + "</option>\r";
                }
                else
                {
                    _Str += "<option value=\"" + elemList[i].InnerXml + "\">" + istr + elemList1[i].InnerXml + "</option>\r";
                }
            }
            _Str += "</select>\r";
            return _Str;
        }


        /// <summary>
        /// 检查当前IP是否是受限IP
        /// </summary>
        /// <param name="LimitedIP">受限的IP，格式如:192.168.1.110|212.235.*.*|232.*.*.*</param>
        /// <returns>返回true表示IP未受到限制</returns>
        static public bool ValidateIP(string LimitedIP)
        {
            string CurrentIP = getUserIP();
            if (LimitedIP == null || LimitedIP.Trim() == string.Empty)
                return true;
            LimitedIP.Replace(".", @"\.");
            LimitedIP.Replace("*", @"[^\.]{1,3}");
            Regex reg = new Regex(LimitedIP, RegexOptions.Compiled);
            Match match = reg.Match(CurrentIP);
            return !match.Success;
        }

        /// <summary>
        /// 判断会员组
        /// </summary>
        /// <param name="uGroup"></param>
        /// <param name="nGroup"></param>
        /// <returns></returns>
        public static bool CommgetGroup(string uGroup, string nGroup)
        {
            bool gf = false;
            if (nGroup == string.Empty)
            {
                gf = true;
            }
            else
            {
                if (nGroup.IndexOf(".") > -1)
                {
                    string[] gARR = nGroup.Split(',');
                    for (int i = 0; i < gARR.Length; i++)
                    {
                        if (uGroup == gARR[i].ToString().Trim())
                        {
                            gf = true;
                        }
                    }
                }
                else
                {
                    if (uGroup == nGroup)
                    {
                        gf = true;
                    }
                }
            }
            return gf;
        }
    }
}
