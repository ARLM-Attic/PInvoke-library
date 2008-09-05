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

public partial class user_discuss_discussacti_list : Foosun.Web.UI.UserPage
{
    //联接数据库
    Discuss dis = new Discuss();
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// 
    #region 初始化
    protected void Page_Load(object sender, EventArgs e)
    {
        
        Response.CacheControl = "no-cache";
        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_PageChange);
        sc.InnerHtml = Show_scs();
        if (!IsPostBack)
        {
            Showu_discusslist(1);
        }
    }
    #endregion
    /// <summary>
    /// 分页绑定数据
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="PageIndex"></param>
    /// 
    #region 分页绑定数据
    protected void PageNavigator1_PageChange(object sender, int PageIndex)
    {
        Showu_discusslist(PageIndex);
    }
    protected void Showu_discusslist(int PageIndex)
    {
        int ig, js;
        DataTable dts = Foosun.CMS.Pagination.GetPage(this.GetType().Name, PageIndex, 10, out ig, out js, null);

        this.PageNavigator1.PageCount = js;
        this.PageNavigator1.PageIndex = PageIndex;
        this.PageNavigator1.RecordCount = ig;
        if (dts != null && dts.Rows.Count!=0)
        {
            dts.Columns.Add("cutAId", typeof(string));
            dts.Columns.Add("idc", typeof(string));
            DataTable selectAId = dis.sel_16();
            int p;
            foreach (DataRow s in dts.Rows)
            {
                p = (int)selectAId.Compute("Count(AId)", "AId='" + s["AId"].ToString() + "'");
                s["cutAId"] = p;
                s["idc"] = "<a class=\"list_link\" href=\"discussacti_DC.aspx?AId=" + s["AId"].ToString() + "\">查看活动信息</a>┆<a class=\"list_link\" href=\"discussacti_add.aspx?AId=" + s["AId"].ToString() + "\">加入</a> ";
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
    #endregion
    /// <summary>
    /// 前台输出
    /// </summary>
    /// <returns></returns>
    /// 
    #region 前台输出
    string Show_no()
    {
        string nos = "<table border=0 width='98%' align=center cellpadding=5 cellspacing=1 class='table'>";
        nos = nos + "<tr class='TR_BG_list'>";
        nos = nos + "<td class='navi_link'>没有数据</td>";
        nos = nos + "</tr>";
        nos = nos + "</table>";
        return nos;
    }
    string Show_scs()
    {
        string scs = "<table width=\"100%\"  border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"toptable\">";
        scs += "<tr><td height=\"1\" colspan=\"2\"></td></tr>";
        scs += "<tr><td width=\"57%\"  class=\"sysmain_navi\"  style=\"PADDING-LEFT: 14px\" >讨论活动管理</td>";
        scs += "<td width=\"43%\"  class=\"topnavichar\"  style=\"PADDING-LEFT: 14px\" ><div align=\"left\">位置导航：<a href=\"../main.aspx\" target=\"sys_main\" class=\"list_link\">首页</a><img alt=\"\" src=\"../../sysImages/folder/navidot.gif\" border=\"0\" /><a href=\"discussacti_list.aspx\" class=\"menulist\">讨论活动管理</a><img alt=\"\" src=\"../../sysImages/folder/navidot.gif\" border=\"0\" />我加入的活动</div></td></tr></table>";
        scs += "<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"3\" cellspacing=\"1\" class=\"Navitable\">";
        scs += "<tr><td><span class=\"topnavichar\" style=\"PADDING-LEFT: 14px\"><a href=\"discussacti_list.aspx\" class=\"menulist\">讨论活动列表</a>　<a href=\"discussactijoin_list.aspx\" class=\"menulist\">我加入的活动</a>&nbsp;&nbsp; <a href=\"discussactiestablish_list.aspx\" class=\"menulist\">我建立的活动</a>&nbsp;&nbsp; <a href=\"discussacti.aspx\" class=\"menulist\">创建活动</a></span></td></tr></table>";
        return scs;
    }
    #endregion
}