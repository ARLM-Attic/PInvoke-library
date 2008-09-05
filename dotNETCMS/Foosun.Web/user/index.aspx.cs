//=====================================================================
//==                  (C)2007 Foosun Inc.By doNetCMS1.0              ==
//==                     Forum:bbs.foosun.net                        ==
//==                     WebSite:www.foosun.net                      ==
//==                 Address:No.109 HuiMin ST,.ChengDu,China         ==
//==                   Tel:86-28-85098980/66026180                   ==
//==                   QQ:655071,MSN:ikoolls@gmail.com               ==
//==                   Email:Service@foosun.cn                       ==
//==                      Code By WangZhenjiang                      ==
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


public partial class User_index : Foosun.Web.UI.UserPage
{ 
    user us = new user();
    UserMisc rd = new UserMisc();
    Channel crd = new Channel();
    public string URL = "main.aspx";
    public string ChannelList = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Expires = 0;
        Response.CacheControl = "no-cache";
        if (!IsPostBack)
        {
            isManage.InnerHtml = isManagestr();
            messageID.InnerHtml = messageChar();
            reURL();

            rootPublic rd = new rootPublic();
            string u_bUserName = rd.getUserName(Foosun.Global.Current.UserNum);
            int isFriendNum = us.GetUncheckFriendsCount(u_bUserName);
            if (isFriendNum > 0)
            {
                isFriendPass.InnerHtml = "<a href=\"Requestinformation.aspx\" class=\"list_link\" target=\"sys_main\">(<span style=\"color:Red\" title=\"有" + isFriendNum + "个用户加你为好友，需要你验证\">" + isFriendNum + "</span>)</a>";
            }
            else
            {
                isFriendPass.InnerHtml = "(<span title=\"无需要验证的用户\">0</span>)";
            }
            ChannelList = getChannelList();
        }
    }

    protected string getChannelList()
    {
        string liststr = string.Empty;
        IDataReader drs = crd.getModelTempletisConstr();
        int i = 0;
        while (drs.Read())
        {
            liststr += "    linkset[5][" + i + "]='<div><a class=\"menu_ctr\" href=\"channel/list.aspx?ChID="+drs["id"].ToString()+"\" target=\"sys_main\">" + drs["channelName"] + "</a></div>'\r";
            i++;
        }
        drs.Close();
        return liststr;
    }
    /// <summary>
    /// 获得转向参数
    /// </summary>
    protected void reURL()
    {
        string str_URL = Request.QueryString["urls"];
        if (str_URL != null && str_URL != "" && str_URL != string.Empty)
        {
            URL = str_URL.Replace("-------", "&");
        }
    }

    //protected void out_click(object sender, EventArgs e)
    //{
    //    Logout();
    //}

    /// <summary>
    /// 是否管理员
    /// </summary>
    /// <returns></returns>
    protected string isManagestr()
    {
        string _STR = "";
        UserMisc rd = new UserMisc();
        if (rd.getisAdmin() == 1)
        {
            _STR += "<a href=\"../" + Foosun.Config.UIConfig.dirMana + "/index.aspx\" class=\"Lion_1\" target=\"_blank\">管理中心</a>&nbsp;&nbsp;┊";
        }
        return _STR;
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
                liststr += "<a href=\"../" + Foosun.Config.UIConfig.dirUser + "/message/Message_box.aspx?Id=1\" class=\"tbie\" target=\"sys_main\">新短消息(" + dt.Rows.Count + ")</a><bgsound src=\"../sysImages/sound/newmessage.wav\" />";
            }
            else
            {
                liststr += "<a href=\"../" + Foosun.Config.UIConfig.dirUser + "/message/Message_box.aspx?Id=1\"  class=\"Lion_1\" target=\"sys_main\">短消息(0)</a>";
            }
        }
        else
        {
            liststr += "<a href=\"../" + Foosun.Config.UIConfig.dirUser + "/message/Message_box.aspx?Id=1\" class=\"Lion_1\" target=\"_self\">短消息(0)</a>";
        }
        return liststr;
    }
}
