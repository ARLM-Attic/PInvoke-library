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

public partial class user_discuss_discussactiestablish_list : Foosun.Web.UI.UserPage
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
        
        this.PageNavigator3.OnPageChange += new PageChangeHandler(PageNavigator3_PageChange);
        Response.CacheControl = "no-cache";
        if (!IsPostBack)
        {
            Show_cjlist(1);
        }
        string Type = Request.QueryString["Type"];  //取得操作类型
        string ID = "";
        if (Request.QueryString["ID"] != "" && Request.QueryString["ID"] != null)
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
    /// 分页绑定数据
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="PageIndex2"></param>
    /// 
    #region 分页绑定数据
    protected void PageNavigator3_PageChange(object sender, int PageIndex2)
    {
        Show_cjlist(PageIndex2);
    }
    protected void Show_cjlist(int PageIndex2)
    {
        int i, j;
        string cjUserName = Foosun.Global.Current.UserName;
        SQLConditionInfo sts = new SQLConditionInfo("@cjUserName", cjUserName);
        DataTable cjlistdts = Foosun.CMS.Pagination.GetPage(this.GetType().Name, PageIndex2, 10, out i, out j, sts);
        this.PageNavigator3.PageCount = j;
        this.PageNavigator3.PageIndex = PageIndex2;
        this.PageNavigator3.RecordCount = i;
        if (cjlistdts!=null && cjlistdts.Rows.Count != 0)
        {
            cjlistdts.Columns.Add("cutAId2", typeof(string));
            cjlistdts.Columns.Add("idc2", typeof(string));
            DataTable selectcjAId = dis.sel_16();
                foreach (DataRow h in cjlistdts.Rows)
                {
                    int v = (int)selectcjAId.Compute("Count(AId)", "AId='" + h["AId"].ToString() + "'");
                    h["cutAId2"] = v;
                    h["idc2"] = "<a href=\"discussacti_up.aspx?AId=" + h["AId"].ToString() + "\" class=\"list_link\"><img src=\"../../sysImages/folder/re.gif\" alt=\"修改\" border=\"0\" /></a>&nbsp;<a href=\"#\" onclick=\"javascript:del('" + h["AId"].ToString() + "');\" class=\"list_link\"><img src=\"../../sysImages/folder/dels.gif\" alt=\"删除\" border=\"0\" /></a>&nbsp;<input name=\"Checkbox1\" type=\"checkbox\" value=" + h["AId"].ToString() + "  runat=\"server\" /></td>";
                }
            sc.InnerHtml = Show_sc();
            Repeater2.DataSource = cjlistdts;
            Repeater2.DataBind();
        }
        else
        {
            sc.InnerHtml = Show_scs();
            no.InnerHtml = Show_no();
            this.PageNavigator3.Visible = false;
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
            PageError("请先选择要删除的活动!", "");
        }
        else
        {
            string[] chSplit = checkboxq.Split(',');
            for (int i = 0; i < chSplit.Length; i++)
            {
                if (chSplit[i] != "on")
                {
                    int k = dis.sel_64(chSplit[i]);
                    if (k != 0)
                    {
                        if (dis.Delete_3(chSplit[i]) == 0 || dis.Delete_4(chSplit[i]) == 0)
                        {
                            PageError("批量删除失败", "discussacti_list.aspx");
                            break;
                        }
                    }
                    else 
                    {
                        if (dis.Delete_4(chSplit[i]) == 0)
                        {
                            PageError("批量删除失败", "discussacti_list.aspx");
                            break;
                        }
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
        int i = dis.sel_64(ID);
        if (i != 0)
        {
            if (dis.Delete_3(ID) == 0 || dis.Delete_4(ID) == 0)
            {
                PageError("批量删除失败", "discussacti_list.aspx");
            }
            else
            {
                PageRight("删除成功!", "discussacti_list.aspx");
            }
        }
        else 
        {
            if (dis.Delete_4(ID) == 0)
            {
                PageError("批量删除失败", "discussacti_list.aspx");
            }
            else
            {
                PageRight("删除成功!", "discussacti_list.aspx");
            }
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
        sc += "<td width=\"43%\"  class=\"topnavichar\"  style=\"PADDING-LEFT: 14px\" ><div align=\"left\">位置导航：<a href=\"../main.aspx\" target=\"sys_main\" class=\"list_link\">首页</a><img alt=\"\" src=\"../../sysImages/folder/navidot.gif\" border=\"0\" /><a href=\"discussacti_list.aspx\" class=\"menulist\">讨论活动管理</a><img alt=\"\" src=\"../../sysImages/folder/navidot.gif\" border=\"0\" />我建立的活动</div></td></tr></table>";
        sc += "<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"3\" cellspacing=\"1\" class=\"Navitable\">";
        sc += "<tr><td><span class=\"topnavichar\" style=\"PADDING-LEFT: 14px\"><a href=\"discussacti_list.aspx\" class=\"menulist\">讨论活动列表</a>　<a href=\"discussactijoin_list.aspx\" class=\"menulist\">我加入的活动</a>&nbsp;&nbsp; <a href=\"discussactiestablish_list.aspx\" class=\"menulist\">我建立的活动</a>&nbsp;&nbsp; <a href=\"discussacti.aspx\" class=\"menulist\">创建活动</a>&nbsp;&nbsp;&nbsp; <a href=\"javascript:PDel();\" class=\"topnavichar\">批量删除</a></span></td></tr></table>";
        return sc;
    }
    string Show_scs()
    {
        string scs = "<table width=\"100%\"  border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"toptable\">";
        scs += "<tr><td height=\"1\" colspan=\"2\"></td></tr>";
        scs += "<tr><td width=\"57%\"  class=\"sysmain_navi\"  style=\"PADDING-LEFT: 14px\" >讨论活动管理</td>";
        scs += "<td width=\"43%\"  class=\"topnavichar\"  style=\"PADDING-LEFT: 14px\" ><div align=\"left\">位置导航：<a href=\"../main.aspx\" target=\"sys_main\" class=\"list_link\">首页</a><img alt=\"\" src=\"../../sysImages/folder/navidot.gif\" border=\"0\" /><a href=\"discussacti_list.aspx\" class=\"menulist\">讨论活动管理</a><img alt=\"\" src=\"../../sysImages/folder/navidot.gif\" border=\"0\" />我建立的活动</div></td></tr></table>";
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
