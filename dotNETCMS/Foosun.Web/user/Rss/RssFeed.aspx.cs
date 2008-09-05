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
using Foosun.Model;

public partial class user_Rss_RssFeed : Foosun.Web.UI.BasePage
{
    Rss rs = new Rss();
    protected void Page_Load(object sender, EventArgs e)
    {
        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_PageChange);
        Response.CacheControl = "no-cache";

        if (!IsPostBack)
        {
           Showu_friendmanage(1);
           string Str_dirMana = Foosun.Config.UIConfig.dirDumm;//虚拟目录

           if (Str_dirMana != "" && Str_dirMana != null && Str_dirMana != string.Empty)//判断虚拟路径是否为空
           {
               Str_dirMana = @"/" + Str_dirMana;
           }
           else
           {
               Str_dirMana = "";
           }
           string str_url = "http://" + Request.ServerVariables["HTTP_HOST"].ToString() + Str_dirMana + "/xml/Content";
           Newsxml.InnerHtml = "最新新闻订阅地址：<a href=\"javascript:copyToClipBoard('" + str_url + "/news.xml');\" class=\"list_link\"><img src=\"../../sysImages/user/rss.gif\" border=\"0\" alt=\"RSS订阅\" /></a>&nbsp;&nbsp;<a class=\"list_link\" href=\"" + str_url + "/news.xml\" target=\"_blank\">" + str_url + "/news.xml</a>";
        }
    }
    protected void PageNavigator1_PageChange(object sender, int PageIndex)
    {
        Showu_friendmanage(PageIndex);
    }
    protected void Showu_friendmanage(int PageIndex)
    {
        string Str_dirMana = Foosun.Config.UIConfig.dirDumm;//虚拟目录
        
        if (Str_dirMana != "" && Str_dirMana != null && Str_dirMana != string.Empty)//判断虚拟路径是否为空
        {
            Str_dirMana = @"/" + Str_dirMana;
        }
        else
        {
            Str_dirMana = "";
        }
        string str_url = "http://" + Request.ServerVariables["HTTP_HOST"].ToString() +Str_dirMana+"/xml/Content";
        int i, j;
        DataTable dts = null;
        string ParentID = Request.QueryString["ParentID"];
        string SiteID = Request.QueryString["SiteID"];
        if (SiteID != "" && SiteID != null)
        {
            SiteID = Request.QueryString["SiteID"].ToString();
        }
        else
        {
            SiteID = "0";
        }
        if (ParentID == "0" || ParentID == null)
        {
            SQLConditionInfo st1 = new SQLConditionInfo("@SiteID", SiteID);
            dts = Foosun.CMS.Pagination.GetPage("user_Rss_RssFeed_1_aspx", PageIndex, 20, out i, out j, st1);
        }
        else 
        {
            SQLConditionInfo[] st = new SQLConditionInfo[2];
            st[0] = new SQLConditionInfo("@ParentID", ParentID.ToString());
            st[1] = new SQLConditionInfo("@SiteID", SiteID);
            dts = Foosun.CMS.Pagination.GetPage("user_Rss_RssFeed_2_aspx", PageIndex, 20, out i, out j, st);
        }

        this.PageNavigator1.PageCount = j;
        this.PageNavigator1.PageIndex = PageIndex;
        this.PageNavigator1.RecordCount = i;
        if (dts.Rows.Count>0)
        {
            dts.Columns.Add("ClassCNames", typeof(string));
            dts.Columns.Add("url",typeof(string));
            dts.Columns.Add("pic", typeof(string));
            dts.Columns.Add("xmllist", typeof(string));
            foreach (DataRow s in dts.Rows)
            {
                if (rs.sel(s["ClassID"].ToString())!=0)
                {
                    s["ClassCNames"] = "<img src=\"../../sysImages/normal/b.gif\" border=\"0\"/><a href=\"?ParentID=" + s["ClassID"].ToString() + "\" class=\"list_link\">" + s["ClassCName"].ToString() + "</a>";
                }
                else
                {
                    s["ClassCNames"] = "<img src=\"../../sysImages/normal/s.gif\" border=\"0\"/>" + s["ClassCName"].ToString() + "";
                }

                string url=str_url + "/" + s["ClassEName"].ToString() + ".xml";
                s["pic"] = "<a href=\"javascript:copyToClipBoard('" + url + "');\" class=\"list_link\"><img src=\"../../sysImages/user/rss.gif\" border=\"0\" alt=\"RSS订阅\" /></a>";
                s["url"] = str_url + "/" + s["ClassEName"].ToString() + ".xml";
                string _getxml = "";
                DataTable xmlList = rs.getxmllist(s["ClassID"].ToString());
                if (xmlList != null)
                {
                    if(xmlList.Rows.Count>0)
                    {
                        for (int m = 0; m < xmlList.Rows.Count; m++)
                        {
                            _getxml += "<li style=\"height:22px;padding-top:2px;float;\"><span style=\"font-size:14px;\" class=\"list_link\">" + xmlList.Rows[m]["NewsTitle"].ToString() + "</span><span style=\"font-size:10px;\">(" + xmlList.Rows[m]["CreatTime"].ToString() + ")</span></li>\r";
                        } 
                    }
                    else
                    {
                        _getxml +="<font color=\"red\">此类无新闻.</font>";
                    }
                }
                s["xmllist"] = _getxml;
            }           
            DataList1.DataSource = dts;
            DataList1.DataBind();
        }
        else
        {
            no.InnerHtml = Show_no();
            this.PageNavigator1.Visible = false;
        }
    }
    string Show_no()
    {
        string nos = "<table border=0 width='98%' align=center cellpadding=5 cellspacing=1 class='table'>";
        nos = nos + "<tr class='TR_BG_list'>";
        nos = nos + "<td class='navi_link'>没有数据</td>";
        nos = nos + "</tr>";
        nos = nos + "</table>";
        return nos;
    }        
}