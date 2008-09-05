using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Foosun.CMS;
using Foosun.CMS.Common;
using System.IO;
using System.Xml;
using System.Net;

public partial class Manage_main : Foosun.Web.UI.ManagePage
{
    UserMisc rd = new UserMisc();
    rootPublic rdr = new rootPublic();
    ContentManage cd = new ContentManage();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Expires = 0;
        Response.CacheControl = "no-cache";
        if (!IsPostBack)
        {
            welcome.InnerHtml = "欢迎您：" + rdr.getUserName(UserNum) + "!&nbsp;&nbsp;<a class=\"list_link\" href=\"../" + Foosun.Config.UIConfig.dirUser + "/showuser-" + UserName + ".aspx\" target=\"_blank\"><img src=\"../sysImages/folder/myinfo.gif\" border=\"0\" alt=\"查看我的资料\" /></a>";
            copyright.InnerHtml = CopyRight;
            Todaydate.InnerHtml = calendar();
            div_product_1.InnerHtml = productlist();
            div_server_1.InnerHtml = serverlist();
            div_stat_1.InnerHtml = statlist();
            messageID.InnerHtml = messageChar();
            unlockNewsList.InnerHtml = getunlockNews();
            myNewsList.InnerHtml = getmyNewsList();

            //#comment# Warren 9/5/2008
            //LinkFoosunVersionPage();
            //comment end

            //LinkFoosunWeather();
        }
    }

    //public void LinkFoosunWeather()
    //{
    //    #region 连接到天气预报

    //    HttpWebRequest req = (HttpWebRequest)WebRequest.Create(getweather());

    //    try
    //    {
    //        req.Method = "GET";
    //        req.ContentType = "application/x-www-form-urlencoded";
    //        req.AllowAutoRedirect = false;
    //        req.Timeout = 2000;

    //        HttpWebResponse Http_Res = (HttpWebResponse)req.GetResponse();

    //        if (Http_Res.StatusCode.ToString() != "OK")
    //        {
    //            weather.InnerHtml = "";
    //        }
    //        else
    //        {
    //            weather.InnerHtml = "<iframe src=\"" + getweather() + "\" width=\"168\" height=\"54\" frameborder=\"no\" border=\"0\" marginwidth=\"0\" marginheight=\"0\" scrolling=\"no\"></iframe>";
    //        }
    //    }
    //    catch
    //    {
    //        weather.InnerHtml = "<span style=\"padding-left:12px;padding-top:8px;height:20px;\" class=\"reshow\">！获取天气预报失败</span>";
    //    }

    //    #endregion
    //}

    public void LinkFoosunVersionPage()
    {
        #region 链接官方升级页面

        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(Foosun.Config.verConfig.getfoosunURL);

        try
        {
            req.Method = "GET";
            req.ContentType = "application/x-www-form-urlencoded";
            req.AllowAutoRedirect = false;
            req.Timeout = 1500;

            HttpWebResponse Http_Res = (HttpWebResponse)req.GetResponse();

            if (Http_Res.StatusCode.ToString() != "OK")
            {
                checkveriframe.InnerHtml = "";
            }
            else
            {
                checkveriframe.InnerHtml = "<iframe style=\"width:98%;height:20px;\" frameborder=\"no\" border=\"0\" marginwidth=\"0\" marginheight=\"0\" scrolling=\"no\" src=\"" + Foosun.Config.verConfig.getfoosunURL + "\"></iframe>";
            }
        }
        catch
        {
            checkveriframe.InnerHtml = "<span style=\"padding-left:12px;padding-top:8px;height:20px;\" class=\"reshow\">！访问Foosun Inc.官方站失败,无法获取最新补丁及版本信息，<a href=\"http://www.foosun.cn\" target=\"_blank\" class=\"reshow\">点击这里获取最新信息.</a></span>";
        }

        #endregion
    }

    /// <summary>
    /// 得到需要审核的信息
    /// </summary>
    /// <returns></returns>
    protected string getunlockNews()
    {
        string STR = "";
        DataTable dt = cd.getLockNews("0");
        if (dt != null && dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
               DateTime datetimeParse = DateTime.Parse(dt.Rows[i]["CreatTime"].ToString());
               STR += "        <li><a href=\"news/news_add.aspx?ClassID=" + dt.Rows[i]["ClassID"].ToString() + "&NewsID=" + dt.Rows[i]["NewsID"].ToString() + "&EditAction=Edit\" class=\"list_link\">" + dt.Rows[i]["NewsTitle"].ToString() + "</a>&nbsp;<span style=\"font-size:11.5px\">(" + datetimeParse.Day + "日" + datetimeParse.Hour + "时" + ")</span></li>\r";
            }
            dt.Clear(); dt.Dispose();
        }
        return STR;
    }

    protected string getmyNewsList()
    {
        string STR = "";
        DataTable dt = cd.getLockNews(UserName);
        if (dt != null && dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                STR += "        <li><a href=\"news/news_add.aspx?ClassID=" + dt.Rows[i]["ClassID"].ToString() + "&NewsID=" + dt.Rows[i]["NewsID"].ToString() + "&EditAction=Edit\" class=\"list_link\">" + dt.Rows[i]["NewsTitle"].ToString() + "</a>&nbsp;<span style=\"font-size:11.5px\">(" + dt.Rows[i]["CreatTime"].ToString() + ")</span></li>\r";
            }
            dt.Clear(); dt.Dispose();
        }
        return STR;
    }
    /// <summary>
    /// 得到日历
    /// </summary>
    /// <returns></returns>
    protected string calendar()
    {
        string listDay = "";
        DataTable dt = rd.calendar(UserNum);
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listDay += "<div><a href=\"javascript:;\" onclick=\"ShowC('" + dt.Rows[i]["ID"].ToString() + "','ClistID');\" title=" + dt.Rows[i]["Title"].ToString() + "><strong><font color=blue>" + dt.Rows[i]["Title"].ToString() + "</font></strong>(" + DateTime.Parse(dt.Rows[i]["LogDateTime"].ToString()).ToShortDateString() + ")</a>";
                }
            }
            else { listDay += "<font color=blue>今天无备忘录!</font>"; }
            dt.Clear(); dt.Dispose();
        }
        else { listDay += "<font color=blue>今天无备忘录!</font>"; }
        dt.Dispose(); dt.Clear();
        return listDay;
    }

    protected string messageChar()
    {
        string liststr = "";
        DataTable dt = rd.messageChar(UserNum);
        if (dt != null)
        {
            if (dt.Rows.Count > 0) { liststr += "<a href=\"../" + Foosun.Config.UIConfig.dirUser + "/message/Message_box.aspx?Id=1\" class=\"tbie\" target=\"_self\">[新短消息(" + dt.Rows.Count + ")]</a><bgsound src=\"../sysImages/sound/newmessage.wav\" />"; }
            else { liststr += "<a href=\"../" + Foosun.Config.UIConfig.dirUser + "/message/Message_box.aspx?Id=1\"  class=\"list_link\" target=\"_self\">[短消息(0)]</a>"; }
            dt.Clear(); dt.Dispose();
        }
        else { liststr += "<a href=\"../" + Foosun.Config.UIConfig.dirUser + "/message/Message_box.aspx?Id=1\" class=\"list_link\" target=\"_self\">[短消息(0)]</a>"; }
        dt.Dispose(); dt.Clear();
        return liststr;
    }

    /// <summary>
    /// 产品列表
    /// </summary>
    protected string productlist()
    {
        string _Str = "";
        try
        {
            string _xmlPath = "~/xml/products/dotnetcmsversion.xml";
            if (!File.Exists(Server.MapPath(_xmlPath))) { PageError("找不到配置文件(~/xml/products/dotnetcmsversion.xml).<li>可能是虚拟目录配置出错.请修改web.config</li>", ""); }
            string xmlPath = Server.MapPath("~/xml/products/dotnetcmsversion.xml");
            FileInfo finfo = new FileInfo(xmlPath);
            System.Xml.XmlDocument xdoc = new XmlDocument();
            xdoc.Load(xmlPath);
            XmlElement root = xdoc.DocumentElement;
            XmlNodeList elemList = root.GetElementsByTagName("versioncontent");
            _Str = "" + elemList[0].InnerXml + "";
        }
        catch
        {
            _Str = "配置文件有问题。/xml/products/dotnetcmsversion.xml";
        }
        return _Str;
    }

    protected string getweather()
    {
        string _Str = "";
        try
        {
            string xmlPath = Server.MapPath("~/xml/products/weather.xml");
            if (!File.Exists(xmlPath)) { PageError("找不到配置文件.<li>请与系统管理员联系。</li>", ""); }
            FileInfo finfo = new FileInfo(xmlPath);
            System.Xml.XmlDocument xdoc = new XmlDocument();
            xdoc.Load(xmlPath);
            XmlElement root = xdoc.DocumentElement;
            XmlNodeList elemList = root.GetElementsByTagName("versionurl");
            _Str = "" + elemList[0].InnerXml + "";
        }
        catch
        {
            _Str = "配置文件有问题。/xml/products/weather.xml";
        }
        return _Str;
    }

    /// <summary>
    /// 服务器列表支持
    /// </summary>
    /// <returns></returns>
    string serverlist()
    {
        string pliststr = "<table class=\"list_link\">";
        pliststr = pliststr + "<tr><td align=\"right\"><strong>服务器支持</strong></td><td></td></tr>";
        if (checkObject("Scripting.FileSystemObject"))
        {
            pliststr = pliststr + "<tr><td align=\"right\">FSO支持：</td><td>√，<span class=\"reshow\">恭喜您！您使用的服务器满足使用本系统的基本条件！</span></td></tr>";
        }
        else
        {
            pliststr = pliststr + "<tr><td align=\"right\">FSO支持：</td><td>×</td></tr>";
        }
        pliststr = pliststr + "<tr><td align=\"right\">服务器名称：</td><td>" + Server.MachineName + "</td></tr>";
        pliststr = pliststr + "<tr><td align=\"right\">服务器IP：</td><td>" + Request.ServerVariables["LOCAL_ADDR"] + "</td></tr>";
        pliststr = pliststr + "<tr><td align=\"right\">服务器域名：</td><td>" + Request.ServerVariables["SERVER_NAME"] + "</td></tr>";
        pliststr = pliststr + "<tr><td align=\"right\">.NET框架版本：</td><td>" + Environment.Version.ToString() + "</td></tr>";
        pliststr = pliststr + "<tr><td align=\"right\">操作系统：</td><td>" + Environment.OSVersion.ToString() + "</td></tr>";
        pliststr = pliststr + "<tr><td align=\"right\">IIS环境：</td><td>" + Request.ServerVariables["SERVER_SOFTWARE"] + "</td></tr>";
        pliststr = pliststr + "<tr><td align=\"right\">服务器端口：</td><td>" + Request.ServerVariables["SERVER_PORT"] + "</td></tr>";
        pliststr = pliststr + "<tr><td align=\"right\">脚本超时设置：</td><td>" + Server.ScriptTimeout.ToString() + "</td></tr>";
        pliststr = pliststr + "<tr><td align=\"right\">虚拟目录绝对路径：</td><td>" + Request.ServerVariables["APPL_PHYSICAL_PATH"] + "</td></tr>";
        pliststr = pliststr + "<tr><td align=\"right\">HTTPS支持：</td><td>" + Request.ServerVariables["HTTPS"] + "</td></tr>";
        pliststr = pliststr + "<tr><td align=\"right\">seesion总数：</td><td>" + Session.Keys.Count.ToString() + "</td></tr>";
        if (checkObject("JMail.SmtpMail"))
        {
            pliststr = pliststr + "<tr><td align=\"right\">jmail支持：</td><td>√</td></tr>";
        }
        else
        {
            pliststr = pliststr + "<tr><td align=\"right\">jmail支持：</td><td>×</td></tr>";
        }
        if (checkObject("CDONTS.NewMail"))
        {
            pliststr = pliststr + "<tr><td align=\"right\">CDONTS邮件：</td><td>√</td></tr>";
        }
        else
        {
            pliststr = pliststr + "<tr><td align=\"right\">CDONTS邮件：</td><td>×</td></tr>";
        }
        pliststr = pliststr + "<tr><td align=\"right\"><strong>本地浏览器支持</strong></td><td></td></tr>";
        HttpBrowserCapabilities HBC = Request.Browser;
        pliststr = pliststr + "<tr><td align=\"right\">客户端ＩＰ：</td><td>" + Request.ServerVariables["REMOTE_ADDR"] + "</td></tr>";
        pliststr = pliststr + "<tr><td align=\"right\">cookies支持：</td><td>" + HBC.Cookies.ToString() + "</td></tr>";
        pliststr = pliststr + "<tr><td align=\"right\">操作系统：</td><td>" + HBC.Platform.ToString() + "</td></tr>";
        pliststr = pliststr + "<tr><td align=\"right\">浏览器：</td><td>" + HBC.Browser.ToString() + "</td></tr>";
        pliststr = pliststr + "<tr><td align=\"right\">浏览器版本：</td><td>" + HBC.Version.ToString() + "</td></tr>";
        pliststr = pliststr + "<tr><td align=\"right\">VBScript：</td><td>" + HBC.VBScript.ToString() + "</td></tr>";
        pliststr = pliststr + "<tr><td align=\"right\">JavaScript：</td><td>" + HBC.JavaScript.ToString() + "</td></tr>";
        pliststr = pliststr + "<tr><td align=\"right\">ActiveX：</td><td>" + HBC.ActiveXControls.ToString() + "</td></tr>";
        pliststr = pliststr + "<tr><td align=\"right\">JavaApplets：</td><td>" + HBC.JavaApplets.ToString() + "</td></tr>";
        pliststr = pliststr + "<tr><td align=\"right\">语言：</td><td>" + Request.ServerVariables["HTTP_ACCEPT_LANGUAGE"] + "</td></tr>";
        pliststr = pliststr + "<tr><td align=\"right\">框架支持：</td><td>" + HBC.Frames.ToString() + "</td></tr>";
        pliststr = pliststr + "<tr><td align=\"right\">DOM：</td><td>" + HBC.MSDomVersion.ToString() + "</td></tr>";
        pliststr = pliststr + "</table>";

        return pliststr;
    }

    protected string statlist()
    {
        string slist = "<div>";
        slist += "<div style=\"padding-left:5px;padding-top:5px;height:20px;\">新闻总数：" + cd.newsstat(SiteID, "0") + "</div>";
        slist += "<div style=\"padding-left:5px;height:20px;\">本月新闻：" + cd.newsstat(SiteID, "m") + "</div>";
        slist += "<div style=\"padding-left:5px;height:20px;\">上月新闻：" + cd.newsstat(SiteID, "pm") + "</div>";
        slist += "<div style=\"padding-left:5px;height:20px;\">上周新闻：" + cd.newsstat(SiteID, "pz") + "</div>";
        slist += "<div style=\"padding-left:5px;height:20px;\">本周新闻：" + cd.newsstat(SiteID, "z") + "</div>";
        slist += "<div style=\"padding-left:5px;height:20px;\">昨日新闻：" + cd.newsstat(SiteID, "pd") + "</div>";
        slist += "<div style=\"padding-left:5px;height:20px;\">今日新闻：" + cd.newsstat(SiteID, "d") + "</div>";
        slist += "<div style=\"padding-left:5px;height:20px;\">栏目总数：" + cd.newsstat(SiteID, "c") + "</div>";
        slist += "<div style=\"padding-left:5px;height:20px;\">专题总数：" + cd.newsstat(SiteID, "s") + "</div>";
        slist += "<div style=\"padding-left:5px;height:20px;\">管理员数：" + cd.newsstat(SiteID, "a") + "</div>";
        slist += "<div style=\"padding-left:5px;height:20px;\">自定义字段数：" + cd.newsstat(SiteID, "de") + "</div>";
        slist += "<div style=\"padding-left:5px;height:20px;\">视频数：" + cd.newsstat(SiteID, "v") + "</div>";
        slist += "<div style=\"padding-left:5px;height:20px;\">频道数：" + cd.newsstat(SiteID, "mo") + "</div>";
        slist += "<div style=\"padding-left:5px;height:20px;\">投稿数：" + cd.newsstat(SiteID, "co") + "</div>";
        slist += "<div style=\"padding-left:5px;height:20px;\">会员总数：" + cd.newsstat(SiteID, "u") + "</div>";
        slist += "</div>";
        return slist;
    }

    /// <summary>
    /// 检查组件
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    private bool checkObject(string obj)
    {
        try
        {
            object meobj = Server.CreateObject(obj);
            return (true);
        }
        catch
        {
            return (false);
        }
    }
    /// <summary>
    /// 退出
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void linkbutton_Click(object sender, EventArgs e)
    {
        Logout();
    }

    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {

    }
    /// <summary>
    /// 更改密码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void changePass_Click(object sender, EventArgs e)
    {
        Response.Redirect("../" + Foosun.Config.UIConfig.dirUser + "/info/ChangePassword.aspx");
    }

    protected void Calendar2_SelectionChanged(object sender, EventArgs e)
    {
        Response.Redirect("../" + Foosun.Config.UIConfig.dirUser + "/info/Logscreat.aspx",true);
    }
}
