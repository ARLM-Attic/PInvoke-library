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

public partial class manage_menu : Foosun.Web.UI.ManagePage
{
    UserMisc rd = new UserMisc();
    public string stype = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            stype = Request.QueryString["type"];
        }
        string type = Request.QueryString["Type"];
        if (type == null)
        {
            Response.Redirect("index.aspx");
        }
        shortcut_id.InnerHtml = ShortcutList(rd.ShortcutList(UserNum, 1));
        menuNavi_id.InnerHtml = menuNavilist(type);
        channelContent.InnerHtml = menuChannelList();
    }

    protected string ShortcutList(IDataReader rd)
    {
        string qflg = "\r<div>\r";
        int i = 0;
        while (rd.Read())
        {
            string QMID = rd["QMID"].ToString();
            string qName = rd["qName"].ToString();
            string FilePath = rd["FilePath"].ToString();
            string usernum = rd["usernum"].ToString();
            qflg = qflg + " <li style=\"list-style:none;padding-left:5px;\"><img src='../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/admin/menu_dot_21.gif' alt=\"\" border=\"0\">";
            qflg = qflg + "<a class=\"menulist\" href=\"" + FilePath + "\" target=\"sys_main\">" + qName + "</a></li>";
            i++;
        }
        rd.Close();
        if (i < 1)
        {
            qflg = qflg + "<li style=\"list-style:none;padding-left:5px;\">无快捷方式，<a href=\"sys/shortcut_list.aspx\" target=\"sys_main\"><font color=\"red\">创建</font></a></li>";
        }
        qflg = qflg + "</div>\r";
        return qflg;
    }

    protected string menuNavilist(string type)
    {
        string tAdminPands = rd.getAdminPopandSupper(UserNum);
        string[] tAdminPandsARR = tAdminPands.Split('|');
        string isSupper = tAdminPandsARR[0];
        string poplist = tAdminPandsARR[1].ToLower().Trim();
        string liststr = "\r<div>\r";
        IDataReader dr = rd.menuNavilist(type, UserNum);
        while (dr.Read())
        {
            if (isSupper != "1")
            {
                if (dr["popCode"].ToString().Trim() != "")
                {
                    if (poplist.IndexOf(dr["popCode"].ToString().Trim().ToLower()) == -1)
                    {
                        continue;
                    }
                    else
                    {
                        liststr += "<li style=\"list-style:none;padding-left:5px;\"><img src='../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/admin/menu_dot_21.gif' alt='' border='0'><a class='menulist' href='" + dr["am_FilePath"].ToString() + "' target='" + dr["am_target"].ToString() + "'>" + dr["am_Name"].ToString() + "</a></li>\r";
                    }
                }
                else
                {
                    liststr += "<li style=\"list-style:none;padding-left:5px;\"><img src='../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/admin/menu_dot_21.gif' alt='' border='0'><a class='menulist' href='" + dr["am_FilePath"].ToString() + "' target='" + dr["am_target"].ToString() + "'>" + dr["am_Name"].ToString() + "</a></li>\r";
                }
            }
            else
            {
                liststr += "<li style=\"list-style:none;padding-left:5px;\"><img src='../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/admin/menu_dot_21.gif' alt='' border='0'><a class='menulist' href='" + dr["am_FilePath"].ToString() + "' target='" + dr["am_target"].ToString() + "'>" + dr["am_Name"].ToString() + "</a></li>\r";
            }
        }
        dr.Close();
        liststr += "</div>";
        return liststr;
    }

    private string menuChannelList()
    {
        Channel md = new Channel();
        IDataReader drs = md.getModelTemplet();
        string channeStr = string.Empty;
        while (drs.Read())
        {
            channeStr += "<li style=\"list-style:none;padding-left:5px;\"><img src='../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/admin/menu_dot_21.gif' alt='' border='0'><a class='menulist' href=\"channel/read.aspx?ChID=" + drs["ID"].ToString() + "\" target=\"menu\" >" + drs["channelName"].ToString() + "</a></li>\r";
        }
        return channeStr;
    }
}