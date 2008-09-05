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


namespace Foosun.Web.manage
{
    public partial class top : Foosun.Web.UI.ManagePage
    {
        public string apilist1 = "";
        public string URL = "main.aspx";
        UserMisc rd = new UserMisc();
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            if (!IsPostBack)
            {
                
                navi_index.InnerHtml = getMenu();
                apilist1 = getapMenu();
                reURL();
            }
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


        /// <summary>
        /// 得到导航菜单
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        protected string getMenu()
        {
            IDataReader dr = rd.Navilist(UserNum);
            string liststr = "<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" background=\"../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/admin/menu_bg.jpg\">\r";
            liststr += "<tr><td width=\"6\" height=\"38\"></td>";
            liststr += "<td id=\"IDC_DownedBUtton\" class=\"button_down\" onClick=\"CheckBTN1(this,'menu.aspx?type=000000','news/news_list.aspx');\" style=\"width:82px;padding-left:14px;background-repeat:no-repeat;text-align:left;cursor:pointer;\">快捷方式</td>";
            while (dr.Read())
            {
                if (dr["am_ClassID"].ToString() == "000000000006")
                {
                    liststr += "<td class=\"button_down\" onClick=\"CheckBTN1(this,'menu.aspx?Type=" + dr["am_ClassID"].ToString() + "','" + dr["mainURL"].ToString() + "');\" style=\"width:82px;padding-left:14px;background-repeat:no-repeat;text-align:left;cursor:pointer;\">" + dr["am_Name"].ToString() + "</td>";
                }
                else
                {
                    liststr += "<td class=\"button_down\" onClick=\"CheckBTN1(this,'menu.aspx?Type=" + dr["am_ClassID"].ToString() + "','" + dr["mainURL"].ToString() + "');\" style=\"width:82px;padding-left:14px;background-repeat:no-repeat;text-align:left;cursor:pointer;\">" + dr["am_Name"].ToString() + "</td>";
                }
            }
            dr.Close();
            int i = 2;
            Channel md = new Channel();
            IDataReader drs = md.getModelTemplet();
            string liststrmore = string.Empty;
            liststrmore += "linkset[0][0]='<div style=\"padding:2px 15px 2px 2px;\"><a class=\"menu_ctr\" href=\"menu.aspx?Type=000000000006\" target=\"menu\">新闻管理</a></div>'\r";
            liststrmore += "linkset[0][1]='<div style=\"padding:2px 15px 2px 2px;\"> &nbsp; &nbsp; &nbsp;<span class=\"reshow\">以下为自定义频道</span></div>'\r";
            while (drs.Read())
            {
                liststrmore += "linkset[0][" + i + "]='<div style=\"padding:2px 15px 2px 2px;\"><a class=\"menu_ctr\" href=\"channel/read.aspx?ChID=" + drs["ID"].ToString() + "\" target=\"menu\">" + drs["channelName"].ToString() + "</a></div>'\r";
                i++;
            }
            drs.Close();
            liststr += "<script language=\"javascript\">\rif (ie4||ns6)\rdocument.onclick=hidemenu\rlinkset[0]=new Array()\r" + liststrmore + "\r</script></td>";
            liststr += "<td></td></tr></table>";
            return liststr;
        }

        /// <summary>
        /// 得到下拉菜单
        /// </summary>
        /// <returns></returns>
        protected string getMoreMenu()
        {
            string liststr = "<td class=\"button_down\" style=\"width:50px;padding-left:14px;background-repeat:no-repeat;text-align:left;cursor:pointer;\" onmouseover=\"showmenu(event,0,1,false)\" onmouseout=\"delayhidemenu()\"><span class=\"reshow\" title=\"更多频道\"><img src=\"../sysImages/folder/MoreChannel.gif\" border=\"0\"></span><script language=\"javascript\">\rif (ie4||ns6)\rdocument.onclick=hidemenu\rlinkset[0]=new Array()\r";
            return liststr;
        }

        /// <summary>
        /// 得到API菜单
        /// </summary>
        /// <param name="dts"></param>
        /// <returns></returns>
        protected string getapMenu()
        {
            IDataReader dr = rd.aplist(UserNum);
            string liststr = "";
            int i = 0;
            while (dr.Read())
            {
                liststr += "linkset[0][" + i + "]='<div><a class=\"menu_ctr\" href=\"" + dr["am_FilePath"] + "\" target=\"" + dr["am_target"] + "\">" + dr["am_Name"] + "</a></div>'\r";
                i++;
            }
            dr.Close();
            return liststr;
        }
    }

}
