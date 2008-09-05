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
using System.Drawing;
using Foosun.CMS;
using Foosun.CMS.Common;

public partial class manage_news_class_site : Foosun.Web.UI.ManagePage
{
    UserMisc rd = new UserMisc();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Response.CacheControl = "no-cache";
            Response.Expires = 0;

            shortcut_id.InnerHtml = ShortcutList();
            returnmenu.InnerHtml = rmenu();
        }
    }

    /// <summary>
    /// 获得返回菜单路径
    /// </summary>
    /// <returns></returns>
    protected string rmenu()
    {
        string liststr = "";
        liststr += "&nbsp;&nbsp;<a href=\"../menu.aspx?" + Request.QueryString + "\" title=\"返回上一级菜单\"><font color=\"red\">返回</font></a>";
        return liststr;
    }

    /// <summary>
    /// 显示快捷菜单
    /// </summary>
    /// <returns></returns>

    protected string ShortcutList()//显示快捷菜单
    {
        IDataReader dr = rd.ShortcutList(UserNum, 1);
        string qflg = "\r<div>\r";
        int i = 0;
        while (dr.Read())
        {
            string QMID = dr["QMID"].ToString();
            string qName = dr["qName"].ToString();
            string FilePath = dr["FilePath"].ToString();
            qflg = qflg + " <li style=\"list-style:none;padding-left:5px;\"><img src='../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/admin/menu_dot_21.gif' alt=\"\" border=\"0\">";
            qflg = qflg + "<a class=\"menulist\" href=\"" + FilePath + "\" target=\"sys_main\">" + qName + "</a></li>";
            i++;
        }
        dr.Close();
        if (i < 1)
        {
            qflg = qflg + "<li style=\"list-style:none;padding-left:5px;\">无快捷方式，<a href=\"sys/shortcut_list.aspx\" target=\"sys_main\"><font color=\"red\">创建</font></a></li>";
        }
        qflg = qflg + "</div>\r";
        return qflg;
    }
}
