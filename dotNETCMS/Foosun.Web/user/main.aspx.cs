//=====================================================================
//==                  (C)2007 Foosun Inc.By doNetCMS1.0              ==
//==                     Forum:bbs.foosun.net                        ==
//==                     WebSite:www.foosun.net                      ==
//==                 Address:No.109 HuiMin ST,.ChengDu,China         ==
//==                   Tel:86-28-85098980/66026180                   ==
//==                   QQ:655071,MSN:ikoolls@gmail.com               ==
//==                   Email:Service@foosun.cn                       ==
//==                      Code By simplt.Xie                         ==
//=====================================================================
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

public partial class user_main : Foosun.Web.UI.UserPage
{
    rootPublic pd = new rootPublic();
    UserMisc rd = new UserMisc();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Expires = 0;
        Response.CacheControl = "no-cache";
        if (!IsPostBack)
        {
            welcome.InnerHtml = "欢迎您：<strong>" + pd.getUserName(Foosun.Global.Current.UserNum) + "</strong>&nbsp;&nbsp;&nbsp;&nbsp;<a href=\"showuser-" + Foosun.Global.Current.UserName + ".aspx\" target=\"_blank\"><font color=\"red\">浏览我的资料</font></a>&nbsp;&nbsp;&nbsp;" + getDate() + "";
            copyright.InnerHtml = CopyRight;
            Todaydate.InnerHtml = mytodays();
            ContentList.InnerHtml = getContentlist();
            GroupList.InnerHtml = getGroupList();
            frindlist.InnerHtml = getfrindlist();
            //weather.InnerHtml = "<iframe src=\"" + getweather() + "\" width=\"168\" height=\"54\" frameborder=\"no\" border=\"0\" marginwidth=\"0\" marginheight=\"0\" scrolling=\"no\"></iframe>";
        }
    }

    /// <summary>
    /// 得到文章列表
    /// </summary>
    /// <returns></returns>
    protected string getContentlist()
    {
        user rot = new user();
        string flg = "";
        DataTable dt = rot.getContent(Foosun.Global.Current.UserNum);
        if (dt != null && dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string sCheck = "";
                string flgs = dt.Rows[i]["isCheck"].ToString();
                if (flgs == "1") 
                { 
                    sCheck = "<img src=\"../sysImages/folder/scheck.gif\" border=\"0\" title=\"总站采用的文章\" />"; 
                }
                else
                {
                    sCheck = "<img src=\"../sysImages/folder/check.gif\" border=\"0\"/>";
                }
                string picFlg = "";
                if (dt.Rows[i]["PicURL"].ToString().Length > 3){ picFlg = "<font color=\"red\">[图]</font>";}
                else { picFlg = ""; }
                flg += sCheck + "&nbsp;<a href=\"show/showcontent.aspx?ConID=" + dt.Rows[i]["ConID"].ToString() + "&uid=" + pd.getUserName(Foosun.Global.Current.UserNum) + "&ClassID=" + dt.Rows[i]["ClassID"].ToString() + "\" title=\"" + Foosun.Common.Input.HtmlEncode(dt.Rows[i]["Content"].ToString()) + "\" class=\"list_link\">" + dt.Rows[i]["Title"].ToString() + "</a>" + picFlg + "&nbsp;<span style=\"font-size:11.5px\">(" + DateTime.Parse(dt.Rows[i]["creatTime"].ToString()).ToShortDateString() + ")</span><br />\r";  
            }
            dt.Clear(); dt.Dispose();
        }
        return flg;
    }

    /// <summary>
    /// 得到讨论组列表
    /// </summary>
    /// <returns></returns>
    protected string getGroupList()
    {
        user rot = new user();
        string flg = "";
        DataTable dt = rot.getGroup(Foosun.Global.Current.UserName);
        if (dt != null && dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                flg += "    <li><a href=\"discuss/discussTopi_list.aspx?DisID=" + dt.Rows[i]["DisID"].ToString() + "\" title=\"" + Foosun.Common.Input.HtmlEncode(dt.Rows[i]["D_Content"].ToString()) + "\" class=\"list_link\">" + dt.Rows[i]["Cname"].ToString() + "</a>&nbsp;<span style=\"font-size:11.5px\">(" + dt.Rows[i]["Creatime"].ToString() + ")</span></li>\r";
            }
            dt.Clear(); dt.Dispose();
        }
        return flg;
    }

    /// <summary>
    /// 得到好情列表
    /// </summary>
    /// <returns></returns>
    protected string getfrindlist()
    {
        Friend rds = new Friend();
        string flg = "";
        DataTable dt = rds.getFriendList(Foosun.Global.Current.UserNum);
        if (dt != null && dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                flg += "<li style=\"padding-left:1px;\"><a href=\"showUser.aspx?uid=" + dt.Rows[i]["UserName"].ToString() + "\" target=\"_blank\" class=\"list_link\">" + dt.Rows[i]["UserName"].ToString() + "</a>&nbsp;<a href=\"Message/Message_write.aspx?uid=" + dt.Rows[i]["UserName"].ToString() + "\" title=\"发送消息\"><img src=\"../sysImages/folder/msg.gif\" border=\"0\" /></a></li>\r";
            }
            dt.Clear(); dt.Dispose();
        }
        return flg;
    }

    /// <summary>
    /// 得到过期日期
    /// </summary>
    /// <returns></returns>
    protected string getDate()
    {
        string getDateStr = "";
        user rot = new user();
        int Rtime = rot.sel_Rtime(rot.sel_UserGroupNumber(Foosun.Global.Current.UserNum));
        if (Rtime != 0)
        {
            DateTime RegTime = DateTime.Parse(rot.getRegTime(Foosun.Global.Current.UserNum));
            DateTime dateNow = DateTime.Now;
            TimeSpan ts = dateNow - RegTime;
            int days = Rtime-((int)ts.TotalDays);
            getDateStr = "&nbsp;&nbsp;您的帐户还有<font color=\"red\">" + days + "</font>天过期。";
        }
        else{getDateStr = "&nbsp;&nbsp;您的帐户永不过期。";}
        return getDateStr;
    }


    /// <summary>
    /// 是否有新事件
    /// </summary>
    /// <returns></returns>
    protected string mytodays()
    {
        string listDay = "";
        DataTable dt = rd.calendar(Foosun.Global.Current.UserNum);
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listDay += "<div><a href=\"javascript:void(0)\" title=" + dt.Rows[i]["Title"].ToString() + "><strong><font color=blue>" + dt.Rows[i]["Title"].ToString() + "</font></strong>(" + DateTime.Parse(dt.Rows[i]["LogDateTime"].ToString()).ToShortDateString() + ")</a>";
                    listDay += "<br />" + dt.Rows[i]["Content"].ToString() + "</div>";
                }
            }
            else { listDay += "<font color=blue>今天无备忘录!</font>"; }
            dt.Clear(); dt.Dispose();
        }
        else { listDay += "<font color=blue>今天无备忘录!</font>"; }
        dt.Dispose(); dt.Clear();
        return listDay;
    }

    /// <summary>
    /// 是否有新消息
    /// </summary>
    /// <returns></returns>
    protected string messageChar()
    {
        string liststr = "";
        DataTable dt = rd.messageChar(Foosun.Global.Current.UserNum);
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                liststr += "<a href=\"../" + Foosun.Config.UIConfig.dirUser + "/message/Message_box.aspx?Id=1\" class=\"tbie\" target=\"_self\">[新短消息(" + dt.Rows.Count + ")]</a><bgsound src=\"../sysImages/sound/newmessage.wav\" />";
            }
            else
            {
                liststr += "<a href=\"../" + Foosun.Config.UIConfig.dirUser + "/message/Message_box.aspx?Id=1\"  class=\"list_link\" target=\"_self\">[短消息(0)]</a>";
            }
        }
        else
        {
            liststr += "<a href=\"../" + Foosun.Config.UIConfig.dirUser + "/message/Message_box.aspx?Id=1\" class=\"list_link\" target=\"_self\">[短消息(0)]</a>";
        }
        return liststr;
    }


    /// <summary>
    /// 检查服务器组件
    /// </summary>
    /// <param name="obj">传入组件的名称</param>
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
    /// 得到天气预报
    /// </summary>
    /// <returns></returns>
    //protected string getweather()
    //{
    //    string _Str = "";
    //    try
    //    {
    //        string _dirdumm = Foosun.Config.UIConfig.dirDumm;
    //        if (_dirdumm.Trim() != "")
    //        { _dirdumm = "/" + _dirdumm; }
    //        if (!File.Exists(Server.MapPath(_dirdumm + "/xml/products/weather.xml"))) { PageError("找不到配置文件(/xml/products/weather.xml).<li>请与系统管理员联系。</li>", ""); }
    //        string xmlPath = Server.MapPath(_dirdumm + "/xml/products/weather.xml");
    //        FileInfo finfo = new FileInfo(xmlPath);
    //        System.Xml.XmlDocument xdoc = new XmlDocument();
    //        xdoc.Load(xmlPath);
    //        XmlElement root = xdoc.DocumentElement;
    //        XmlNodeList elemList = root.GetElementsByTagName("versionurl");
    //        _Str = "" + elemList[0].InnerXml + "";
    //    }
    //    catch
    //    {
    //        _Str = "配置文件有问题。/xml/products/weather.xml";
    //    }
    //    return _Str;
    //}
}
