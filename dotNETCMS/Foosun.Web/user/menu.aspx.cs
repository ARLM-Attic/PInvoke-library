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

public partial class user_menu : Foosun.Web.UI.UserPage
{
    UserMisc rd = new UserMisc();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            shortcut_id.InnerHtml = ShortcutList();
            string type = Request.QueryString["Type"];
        }
    }

    protected string ShortcutList()//显示快捷菜单
    {
        string _Str = "";
        IDataReader dr = rd.ShortcutList(Foosun.Global.Current.UserNum, 0);
        int i = 0;
        while (dr.Read())
        {
            string QMID = dr["QMID"].ToString();
            string qName = dr["qName"].ToString();
            string FilePath = dr["FilePath"].ToString();
            string usernum = dr["usernum"].ToString();
            _Str += "<tr>";
            _Str += " <td width=\"2\"><img src=\"../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/admin/menu_dot_user_21.gif\" alt=\"\" border=\"0\" align=\"right\"></td>";
            _Str += " <td align=\"left\"><a class=\"menulist\" href=\"" + FilePath + "\" target=\"sys_main\">" + qName + "</a></td>";
            _Str += "</tr>";
            i++;
        }
        dr.Close();
        if (i > 0)
            _Str = "<table width=\"100%\" align=\"center\" border=\"0\" cellspacing=\"5\" cellpadding=\"0\">" + _Str + "</table>";
        else
            _Str = "<div style=\"padding-left:12px;\">无快捷方式。<a href=\"info/shortcut.aspx\" target=\"sys_main\"><span class=\"reshow\">创建</span></a></div>";
        return _Str;
    }
}