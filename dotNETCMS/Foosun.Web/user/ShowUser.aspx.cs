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

public partial class user_ShowUser : Foosun.Web.UI.BasePage
{
    public string URL = "";
    public string UserName = "";
    UserMisc rd = new UserMisc();
    rootPublic pd = new rootPublic();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Expires = 0;
        myinfo.InnerHtml = getinfo();
        Response.CacheControl = "no-cache";
        UserName = Request.QueryString["uid"];
        if (!IsPostBack)
        {
            string u_name = Request.QueryString["uid"];
            if (u_name != null && u_name != "")
            {
                string uID = pd.getUserNameUserNum(Foosun.Common.Input.Filter(u_name.ToString()));
                if (uID == "0")
                {
                    PageError("找不到用户 [" + u_name.ToString() + "] 的信息.<li>原因：此用户未注册或者被管理员删除!</li>", "");
                }
                reURL();
            }
            else
            {
                PageError("参数传递错误。", "");
            }
        }
    }

    protected string getinfo()
    {
        string _STR = "";
        string manageControl = "";
        if (Foosun.Global.Current.UserNum != "" && Foosun.Global.Current.UserNum != null)
        {
            if (rd.getisAdmin() == 1)
            {
                manageControl = "&nbsp;&nbsp;┊&nbsp;&nbsp;<a href=\"../" + Foosun.Config.UIConfig.dirMana + "/index.aspx\" target=\"_top\" class=\"list_link\">管理中心</a>";
            }
            _STR += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;欢迎您!&nbsp;<font color=\"red\">" + Foosun.Global.Current.UserName + "</font>&nbsp;&nbsp;┊&nbsp;&nbsp;" + messageChar() + "&nbsp;&nbsp;┊&nbsp;&nbsp;<a href=\"index.aspx\" class=\"list_link\" target=\"_top\">会员中心</a>" + manageControl;
        }
        else
        {
            _STR += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href=\"login.aspx?urls=" + Request.Url.ToString() + "\" class=\"list_link\">登录</a>&nbsp;&nbsp;,&nbsp;&nbsp;<a href=\"Register.aspx?urls=" + Request.Url.ToString() + "\" class=\"list_link\">注册</a>";
        }
        _STR += "&nbsp;&nbsp;┊&nbsp;&nbsp;<a href=\"Rss/RssFeed.aspx\" class=\"list_link\" target=\"sys_main\"><img src=\"../sysImages/folder/rss.gif\" border=\"0\" title=\"RSS订阅中心.&#13;点击进入RSS订阅页面\" /></a>&nbsp;&nbsp;&nbsp;&nbsp;<a href=\"info/wap.aspx\" class=\"list_link\" target=\"sys_main\"><img src=\"../sysImages/user/wap.gif\" border=\"0\" title=\"wap访问\" /></a>";
        return _STR;
    }
    
    /// <summary>
    /// 获得转向参数
    /// </summary>
    protected void reURL()
    {
        string str_URL = Request.QueryString["urls"];
        if (str_URL != null && str_URL != "" && str_URL != string.Empty)
        {
            URL = str_URL.Replace("F0002S", "&").Replace("F0001S", "?");
        }
        else
        {
            URL = "show/info.aspx?uid=" + Request.QueryString["uid"] + "";
        }
    }

    /// <summary>
    /// 是否有新消息
    /// </summary>
    /// <returns></returns>
    string messageChar()
    {
        string liststr = "";
        DataTable dt = rd.messageChar(Foosun.Global.Current.UserNum);
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                liststr += "<a href=\"index.aspx?urls=message/Message_box.aspx?Id=1\" class=\"tbie\" target=\"_self\">新短消息(" + dt.Rows.Count + ")</a><bgsound src=\"../sysImages/sound/newmessage.wav\" />";
            }
            else
            {
                liststr += "<a href=\"index.aspx?urls=message/Message_box.aspx?Id=1\"  class=\"list_link\" target=\"_self\">短消息(0)</a>";
            }
        }
        else
        {
            liststr += "<a href=\"index.aspx?urls=message/Message_box.aspx?Id=1\" class=\"list_link\" target=\"_self\">短消息(0)</a>";
        }
        return liststr;
    }

}
