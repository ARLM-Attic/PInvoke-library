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

public partial class manage_Sys_table_manage : Foosun.Web.UI.ManagePage
{
    sys rd = new sys();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            copyright.InnerHtml = CopyRight;
        }
        table_list.InnerHtml = tableList();
        if (Foosun.Config.verConfig.PublicType == "1")
        {
            isTRUE.InnerHtml = "&nbsp;┊&nbsp;<a class=\"topnavichar\" href=\"table_list.aspx\">复制新闻表</a>";
        }
        else
        {
            isTRUE.InnerHtml = "&nbsp;┊&nbsp;<a class=\"topnavichar\" href=\"javascript:void(0);\" title=\"您的版本不允许创建新闻表\"><span style=\"color:#999999\">复制新闻表</span></a>";
        }
    }
    string tableList()
    {
        //显示列表开始
        DataTable dt = rd.GetTableList();
        string liststr = "<table width=\"98%\" border=\"0\" align=\"center\" cellpadding=\"5\" cellspacing=\"1\" class=\"table\">";
        liststr = liststr + "<tr class=\"TR_BG\">\r";
        liststr = liststr + "<td width='100'class=\"sys_topBg\">ID</td>\r";
        liststr = liststr + "<td width='60'class=\"sys_topBg\">表名</td>\r";
        liststr = liststr + "<td align='left'class=\"sys_topBg\">创建日期</td>\r";
        liststr = liststr + "</tr>";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string QMID = dt.Rows[i]["id"].ToString();
            string qName = dt.Rows[i]["TableName"].ToString();
            string creattime = dt.Rows[i]["creattime"].ToString();

            liststr = liststr + "<tr class=\"TR_BG_list\">\r";
            liststr = liststr + "<td width='50'class=\"list_link\">" + QMID + "</td>\r";
            liststr = liststr + "<td width='200'class=\"list_link\">" + qName + "</td>\r";
            liststr = liststr + "<td align='left'class=\"list_link\">" + creattime + "</td>\r";
            liststr = liststr + "</tr>";
        }
        liststr = liststr + "</table>";
        return liststr;
        //显示列表结束
    }
}
