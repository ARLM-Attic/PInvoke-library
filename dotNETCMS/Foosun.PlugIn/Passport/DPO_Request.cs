using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
 
using System.Xml;
using System.Net;
using System.Text;
using System.IO;

namespace Foosun.PlugIn.Passport
{
    /// <summary>
    /// 提交整合要求类
    /// </summary>
    public class DPO_Request
    {
         
        private string strXmlPath = string.Empty;
        private string ErrType = string.Empty;
        private string dpo_appid = string.Empty;
        public string UserName = string.Empty;
        public string PassWord = string.Empty;
        public string CookieDate = string.Empty;
        public string EMail = string.Empty;
        public string Question = string.Empty;
        public string Answer = string.Empty;
        public string userip = string.Empty;
        public string Status = string.Empty;
        public System.Collections.Generic.List<string> ErrStr = new System.Collections.Generic.List<string>();
        public bool FoundErr = false;
        public string Sex = string.Empty;
        public string QQ = string.Empty;
        public string MSN = string.Empty;
        public string UserStatus = string.Empty;
        public string TrueName = string.Empty;
        public string Birthday = string.Empty;
        public string TelePhone = string.Empty;
        public string HomePage = string.Empty;
        public string Province = string.Empty;
        public string City = string.Empty;
        public string address = string.Empty;
        System.Xml.XmlDocument XmlDoc = new System.Xml.XmlDocument();
        System.Web.HttpContext context;
        Foosun.Config.API.APIConfig config ;
        public DPO_Request(System.Web.HttpContext context)
        {

            this.context = context;
            config = Foosun.Config.API.APIConfigs.GetConfig();
            
             
            LoadXmlFile();
        }

        /// <summary>
        /// 将值分别提交到每个url中
        /// </summary>
        /// <param name="strType"></param>
        public void ProcessMultiPing(string strType)
        {
            if (config.Enable == false)
                return;
            if (config.ApplicationList != null)               
            {
                foreach (Foosun.Config.API.ApplicationInfo app in config.ApplicationList)
                {
                    if (app.AppUrl.StartsWith( "http://"))
                    {
                        SendPost(app.AppUrl, strType);
                    }
                }
            }
 
        }

        //读取XML模板文件，当值为True时是请求信息模板，反之是返回信息模板
        void LoadXmlFile()
        {
            string filename = string.Empty;
            if (HttpContext.Current == null)
            {
                filename= Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "configuration/config/Request.xml");
            }
            else
            {
                string applicationPath = System.Web.HttpContext.Current.Request.ApplicationPath;
                if (applicationPath.EndsWith("/") == false && applicationPath.EndsWith(@"\") == false)
                    applicationPath += "/";
                applicationPath += "configuration/config/Request.xml";
                filename = System.Web.HttpContext.Current.Server.MapPath(applicationPath);
            }

            XmlDoc.Load(filename);


             
        }
        void SendPost(string Url, string strType)
        {

            ErrType = strType;
            string XMLTemp = string.Empty;
            string strXML = string.Empty;
            string auth_token = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(UserName.ToLower() + config.AppKey,"MD5").Substring(8, 16).ToLower();
            //set XMLTemp = Server.Createstring("Microsoft.XMLDOM")
            SetNodeValue("username", UserName);
            SetNodeValue("action", strType);
            SetNodeValue("syskey", auth_token);
            SetNodeValue("appid", config.AppID);
            if (strType == "reguser" || strType == "update")
            {
                SetNodeValue("password", PassWord);
                SetNodeValue("email", EMail);
                SetNodeValue("question", Question);
                SetNodeValue("answer", Answer);
                SetNodeValue("gender", Sex);
                SetNodeValue("birthday", Birthday);
                SetNodeValue("qq", QQ);
                SetNodeValue("msn", MSN);
                SetNodeValue("telephone", TelePhone);
                SetNodeValue("homepage", HomePage);
                SetNodeValue("userip", userip);
                SetNodeValue("userstatus", UserStatus);
                SetNodeValue("province", Province);
                SetNodeValue("city", City);
                SetNodeValue("address", address);
                SetNodeValue("truename", TrueName);
            }
            else if (strType == "login")
            {
                SetNodeValue("password", PassWord);
                SetNodeValue("savecookie", CookieDate);
                SetNodeValue("userip", userip);
            }
            else
            {
            }
            HttpWebRequest request;
            request = (HttpWebRequest)System.Net.WebRequest.Create(Url);
            request.Method = "POST";
            request.ContentType = "text/xml";
            byte[] buffer1 = Encoding.GetEncoding("GB2312").GetBytes(this.XmlDoc.OuterXml);
            request.ContentLength = buffer1.Length;
            Stream stream1 = request.GetRequestStream();
            stream1.Write(buffer1, 0, buffer1.Length);
            stream1.Close();
            string resultPost = string.Empty;
            try
            {
                HttpWebResponse response1 = (HttpWebResponse)request.GetResponse();
                Stream stream2 = response1.GetResponseStream();
                Encoding encoding1 = Encoding.GetEncoding("GB2312");
                StreamReader reader1 = new StreamReader(stream2, encoding1);
                resultPost = reader1.ReadToEnd();
                reader1.Close();
            }
            catch  
            {
                AddErrStr("同步信息错误，请重试！");

                return;
            }
            System.Xml.XmlDocument xmlResponse = new System.Xml.XmlDocument();
            try
            {
                xmlResponse.LoadXml(resultPost);
            }
            catch  
            {
                AddErrStr("解析返回信息出错，请重试！");

                return;
            }
            XmlNodeList statuses = xmlResponse.GetElementsByTagName("status");
            if (statuses.Count > 0 && statuses.Item(0).InnerText != "0")
            {
                dpo_appid = xmlResponse.GetElementsByTagName("appid").Item(0).InnerText;
                AddErrStr(xmlResponse.GetElementsByTagName("message").Item(0).InnerText);
            }

        }


        /// <summary>
        /// 将读取到XML模板中的各个元素赋值
        /// </summary>
        /// <param name="strNodeName"></param>
        /// <param name="strNodeValue"></param>
        public void SetNodeValue(string strNodeName, string strNodeValue)
        {

            try
            {
                XmlDoc.SelectSingleNode("//" + strNodeName).InnerText = strNodeValue;
            }
            catch
            {
                AddErrStr("写入信息发生错误，请重试！");

                return;
            }


        }

        /// <summary>
        /// 错误处理函数
        /// </summary>
        /// <param name="Message"></param>
        public void AddErrStr(string Message)
        {
            ErrStr.Add(string.Format("{0}提示您：{1}", dpo_appid, Message));

            FoundErr = true;
        }
        /// <summary>
        /// 请求远程系统同步登录状态
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">md5加密后的密码</param>
        /// <param name="returnUrl">同步后返回的URL，如果为空则不转向</param>

        public  void RequestLogin(string username, string password, string returnUrl)
        {

            if (config.Enable == false)
            {
                context.Response.Redirect("/user/index.aspx");
                return;
            }
            if (config.ApplicationList == null)
            {
                context.Response.Redirect("/user/index.aspx");
                return;
            }
            if (!string.IsNullOrEmpty(username))
                username = username.ToLower();
            password = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5").Substring(8, 16).ToLower();

            string output = string.Empty;
            string _temp = string.Empty;
            foreach (Foosun.Config.API.ApplicationInfo app in config.ApplicationList)
            {
                Encoding encoding = Encoding.GetEncoding("GB2312");


                string auth_token = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(
                    string.Concat(username, config.AppKey), "MD5");

                string url = string.Format("{0}{1}username={2}&syskey={3}&password={4}&savecookie={5}",
                    app.AppUrl,
                    app.AppUrl.IndexOf("?") > 0 ? "&" : "?",
                    HttpUtility.UrlEncode(username, encoding),
                    HttpUtility.UrlEncode(auth_token, encoding),
                    HttpUtility.UrlEncode(password, encoding),
                    0);
                _temp = string.Format("<script type='text/javascript' src='{0}' charset='{1}'></script>\r\n", url, "gb2312");
                output += _temp;
            }

            if (!string.IsNullOrEmpty(returnUrl))
                output += "<script type='text/javascript'>window.onload=function() {window.setTimeout(\"location.href='" + returnUrl + "';\",5000); };</script>\r\n";
            else
                output += "<script type='text/javascript'>window.onload=function() {window.setTimeout(\"location.href='/user/index.aspx';\",5000); };</script>\r\n";
            context.Response.Write(output);

        }
        /// <summary>
        /// 请求远程系统同步登出状态
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="returnUrl">同步后返回的URL，如果为空则不转向</param>
        /// <param name="context">当前会话</param>
        public  void RequestLogout(string username, string returnUrl)
        {

            if (config.Enable == false)
            {
                context.Response.Redirect("/user/Login.aspx");
                return;
            }
            if (config.ApplicationList == null)
            {
                context.Response.Redirect("/user/Login.aspx");
                return;
            }
            if (!string.IsNullOrEmpty(username))
                username = username.ToLower();


            string output = string.Empty;
            string _temp = string.Empty;
            foreach (Foosun.Config.API.ApplicationInfo app in config.ApplicationList)
            {
                Encoding encoding = Encoding.GetEncoding("GB2312");


                string auth_token = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(
                    string.Concat(username, config.AppKey), "MD5");

                string url = string.Format("{0}{1}username={2}&syskey={3}",
                    app.AppUrl,
                    app.AppUrl.IndexOf("?") > 0 ? "&" : "?",
                    HttpUtility.UrlEncode(username, encoding),
                    HttpUtility.UrlEncode(auth_token, encoding));
                _temp = string.Format("<script type='text/javascript' src='{0}' charset='{1}'></script>\r\n", url, encoding.EncodingName);
                output += _temp;
            }
            if (!string.IsNullOrEmpty(returnUrl))
                output += "<script type='text/javascript'>window.onload=function() {window.setTimeout(\"location.href='" + returnUrl + "';\",5000); };</script>\r\n";
            else
                output += "<script type='text/javascript'>window.onload=function() {window.setTimeout(\"location.href='/';\",5000); };</script>\r\n";
            context.Response.Write(output);


        }



    }

}