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

public partial class user_discuss_discussactijoin_list : Foosun.Web.UI.UserPage
{
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
        this.PageNavigator2.OnPageChange += new PageChangeHandler(PageNavigator2_PageChange);
        if (!IsPostBack)
        {
            Show_jrlist(1);
        }
        string Type = Request.QueryString["Type"];  //取得操作类型
        string ID = "";
        if (Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
        {
            ID = Foosun.Common.Input.Filter(Request.QueryString["ID"]);  //取得需要操作的稿件ID
        }
        switch (Type)
        {
            case "del":          //删除
                del(ID);
                break;
            case "PDel":            //批量删除
                PDel();
                break;
            default:
                break;
        }
    }
    #endregion
    /// <summary>
    /// 绑定数据分页
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="PageIndex1"></param>
    /// 
    #region 绑定数据分页
    protected void PageNavigator2_PageChange(object sender, int PageIndex1)
    {
        Show_jrlist(PageIndex1);
    }
    protected void Show_jrlist(int PageIndex1)
    {
        int i, j;
        DataTable jrlistdts = Foosun.CMS.Pagination.GetPage(this.GetType().Name, PageIndex1, 10, out i, out j, null);
        this.PageNavigator2.PageCount = j;
        this.PageNavigator2.PageIndex = PageIndex1;
        this.PageNavigator2.RecordCount = i;
        if (jrlistdts != null && jrlistdts.Rows.Count!=0)
        {
            jrlistdts.Columns.Add("cutAId1", typeof(string));
            jrlistdts.Columns.Add("idc1", typeof(string));
            DataTable jrid = dis.sel_16();
            foreach (DataRow r in jrlistdts.Rows)
            {
                int n = (int)jrid.Compute("Count(AId)", "AId='" + r["AId"].ToString() + "'");
                r["cutAId1"] = n;
                r["idc1"] = "<a class=\"list_link\" href=\"discussacti_DC.aspx?AId=" + r["AId"].ToString() + "\"><img src=\"../../sysImages/folder/review.gif\" border=\"0\" alt=\"查看活动信息\" /></a>&nbsp;<a href=\"#\" onclick=\"javascript:del('" + r["PId"].ToString() + "');\" class=\"list_link\"><img src=\"../../sysImages/folder/dels.gif\" alt=\"删除\" border=\"0\" /></a>&nbsp;<input name=\"Checkbox1\" type=\"checkbox\" value=" + r["PId"].ToString() + "  runat=\"server\" />";

            }
            sc.InnerHtml = Show_sc();
            Repeater1.DataSource = jrlistdts;
            Repeater1.DataBind();
        }
        else
        {
            sc.InnerHtml = Show_scs();
            no.InnerHtml = Show_no();
            this.PageNavigator2.Visible = false;
        }
    }
    #endregion 
    /// <summary>
    /// 批量删除
    /// </summary>
    /// 
    #region 批量删除
    protected void PDel()
    {
        string checkboxq = Request.Form["Checkbox1"];
        if (checkboxq == null || checkboxq == String.Empty)
        {
            PageError("请先选择要删除的活动!", "discussacti_list.aspx");
        }
        else
        {
            string[] chSplit = checkboxq.Split(',');
            for (int i = 0; i < chSplit.Length; i++)
            {
                if (chSplit[i] != "on")
                {
                    if (dis.Delete_5(chSplit[i]) == 0)
                    {
                        PageError("批量删除失败", "discussacti_list.aspx");
                        break;
                    }
                }
            }
            PageRight("批量删除成功", "discussacti_list.aspx");
        }
    }
    #endregion
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="ID"></param>
    /// 
    #region 删除
    protected void del(string ID)
    {
        if (dis.Delete_5(ID) == 0)
        {
            PageError("批量删除失败", "discussacti_list.aspx");
        }
        else
        {
            PageRight("删除成功!", "discussacti_list.aspx");
        }
    }
    #endregion 
    /// <summary>
    /// 前台输出
    /// </summary>
    /// <returns></returns>
    /// 
    #region 前台输出
    string Show_sc()
    {
        string sc = "<table width=\"100%\"  border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"toptable\">";
        sc += "<tr><td height=\"1\" colspan=\"2\"></td></tr>";
        sc += "<tr><td width=\"57%\"  class=\"sysmain_navi\"  style=\"PADDING-LEFT: 14px\" >讨论活动管理</td>";
        sc += "<td width=\"43%\"  class=\"topnavichar\"  style=\"PADDING-LEFT: 14px\" ><div align=\"left\">位置导航：<a href=\"../main.aspx\" target=\"sys_main\" class=\"list_link\">首页</a><img alt=\"\" src=\"../../sysImages/folder/navidot.gif\" border=\"0\" /><a href=\"discussacti_list.aspx\" class=\"menulist\">讨论活动管理</a><img alt=\"\" src=\"../../sysImages/folder/navidot.gif\" border=\"0\" />我加入的活动</div></td></tr></table>";
        sc += "<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"3\" cellspacing=\"1\" class=\"Navitable\">";
        sc += "<tr><td><span class=\"topnavichar\" style=\"PADDING-LEFT: 14px\"><a href=\"discussacti_list.aspx\" class=\"menulist\">讨论活动列表</a>　<a href=\"discussactijoin_list.aspx\" class=\"menulist\">我加入的活动</a>&nbsp;&nbsp; <a href=\"discussactiestablish_list.aspx\" class=\"menulist\">我建立的活动</a>&nbsp;&nbsp; <a href=\"discussacti.aspx\" class=\"menulist\">创建活动</a>&nbsp;&nbsp;&nbsp; <a href=\"javascript:PDel();\" class=\"topnavichar\">批量删除</a></span></td></tr></table>";
        return sc;
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
    string Show_no()
    {
        string nos = "<table border=0 width='98%' align=center cellpadding=5 cellspacing=1 class='table'>";
        nos = nos + "<tr class='TR_BG_list'>";
        nos = nos + "<td class='navi_link'>没有数据</td>";
        nos = nos + "</tr>";
        nos = nos + "</table>";
        return nos;
    }
    #endregion 
}
