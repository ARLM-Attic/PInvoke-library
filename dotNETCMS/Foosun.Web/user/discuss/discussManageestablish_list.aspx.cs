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

public partial class user_discuss_discussManageestablish_list : Foosun.Web.UI.UserPage
{
    Discuss dis = new Discuss();
    Foosun.CMS.Common.rootPublic pd = new Foosun.CMS.Common.rootPublic();
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
    /// 数据绑定分页
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="PageIndex"></param>
    /// 
    #region 数据绑定分页
    protected void PageNavigator3_PageChange(object sender, int PageIndex2)
    {
        Show_cjlist(PageIndex2);
    }
    protected void Show_cjlist(int PageIndex2) //显示我创建的讨论组列表
    {
        int i, j;
        string cjUserName = Foosun.Global.Current.UserName;
        SQLConditionInfo sts = new SQLConditionInfo("@UserName", cjUserName);
        DataTable cjlistdts = Foosun.CMS.Pagination.GetPage(this.GetType().Name, PageIndex2, 10, out i, out j, sts);
        this.PageNavigator3.PageCount = j;
        this.PageNavigator3.PageIndex = PageIndex2;
        this.PageNavigator3.RecordCount = i;
        if (cjlistdts != null && cjlistdts.Rows.Count!=0)
        {
            cjlistdts.Columns.Add("cutDisID2", typeof(string));
            cjlistdts.Columns.Add("idc2", typeof(string));
            cjlistdts.Columns.Add("titles", typeof(string));
            DataTable selectcjDisID = dis.sel_22();
            foreach (DataRow h in cjlistdts.Rows)
            {
                int v = (int)selectcjDisID.Compute("Count(DisID)", "DisID='" + h["DisID"].ToString() + "'");
                h["cutDisID2"] = v;
                //h["idc2"] = "<a href=\"up_discussManage.aspx?DisID=" + h["DisID"].ToString() + "\" class=\"list_link\">修改</a>┆<a class=\"list_link\" href=\"disFundwarehouse.aspx?DisID=" + h["DisID"].ToString() + "\">捐献</a>┆<a class=\"list_link\" href=\"discussPhotoalbumlist.aspx?DisID=" + h["DisID"].ToString() + "\">相册</a>┆<a href=\"#\" onclick=\"javascript:del('" + h["DisID"].ToString() + "');\" class=\"list_link\">删除</a>┆<input name=\"Checkbox1\" type=\"checkbox\" value=" + h["DisID"].ToString() + "  runat=\"server\" /></td>";
                h["idc2"] = "<a href=\"up_discussManage.aspx?DisID=" + h["DisID"].ToString() + "\" class=\"list_link\">修改</a>┆<a class=\"list_link\" href=\"disFundwarehouse.aspx?DisID=" + h["DisID"].ToString() + "\">捐献</a>┆<a href=\"#\" onclick=\"javascript:del('" + h["DisID"].ToString() + "');\" class=\"list_link\">删除</a>┆<input name=\"Checkbox1\" type=\"checkbox\" value=" + h["DisID"].ToString() + "  runat=\"server\" /></td>";
                h["titles"] = "<a href=\"discussTopi_list.aspx?DisID=" + h["DisID"].ToString() + "\" class=\"list_link\">" + h["Cname"].ToString() + "</a>";
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
            PageError("请先选择要删除的讨论组!", "discussManageestablish_list.aspx");
        }
        else
        {
            string[] chSplit = checkboxq.Split(',');
            for (int i = 0; i < chSplit.Length; i++)
            {
                if (chSplit[i] != "on")
                {
                    int k = dis.sel_63(chSplit[i]);
                    if (k != 0)
                    {
                        if (dis.Delete_6(chSplit[i]) == 0 || dis.Delete_7(chSplit[i]) == 0)
                        {
                            PageError("批量删除失败", "discussManageestablish_list.aspx");
                            break;
                        }
                    }
                    else 
                    {
                        if (dis.Delete_7(chSplit[i]) == 0)
                        {
                            PageError("批量删除失败", "discussManageestablish_list.aspx");
                            break;
                        }
                    }
                }
            }
            PageRight("批量删除成功", "discussManageestablish_list.aspx");
        }
    }
    #endregion
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="ID"></param>
    #region 删除
    protected void del(string ID)
    {
        int i = dis.sel_63(ID);
        if (i != 0)
        {
            if (dis.Delete_6(ID) == 0 || dis.Delete_7(ID) == 0)
            {
                PageError("批量删除失败", "discussManageestablish_list.aspx");
            }
            else
            {
                PageRight("删除成功!", "discussManageestablish_list.aspx");
            }
        }
        else 
        {
            if (dis.Delete_7(ID) == 0)
            {
                PageError("批量删除失败", "discussManageestablish_list.aspx");
            }
            else
            {
                PageRight("删除成功!", "discussManageestablish_list.aspx");
            }
        }
    }
    #endregion
    /// <summary>
    /// 前台输出
    /// </summary>
    /// <returns></returns>
    #region 前台输出
    string Show_sc()
    {
        string sc = "<table width=\"100%\"  border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"toptable\">";
        sc += "<tr><td height=\"1\" colspan=\"2\"></td></tr>";
        sc += "<tr><td width=\"57%\"  class=\"sysmain_navi\"  style=\"PADDING-LEFT: 14px\" >讨论活动管理</td>";
        sc += "<td width=\"43%\"  class=\"topnavichar\"  style=\"PADDING-LEFT: 14px\" ><div align=\"left\">位置导航：<a href=\"../main.aspx\" target=\"sys_main\" class=\"list_link\">首页</a><img alt=\"\" src=\"../../sysImages/folder/navidot.gif\" border=\"0\" /><a href=\"discussManage_list.aspx\" class=\"menulist\">讨论组管理</a><img alt=\"\" src=\"../../sysImages/folder/navidot.gif\" border=\"0\" />我建立的讨论组</div></td></tr></table>";
        sc += "<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"3\" cellspacing=\"1\" class=\"Navitable\">";
        sc += "<tr><td><span class=\"topnavichar\" style=\"PADDING-LEFT: 14px\"><a href=\"discussManage_list.aspx\" class=\"menulist\">讨论组列表</a>　<a href=\"discussManagejoin_list.aspx\" class=\"menulist\">我加入的讨论组</a>&nbsp;&nbsp; <a href=\"discussManageestablish_list.aspx\" class=\"menulist\">我建立的讨论组</a>&nbsp;&nbsp; <a href=\"add_discussManage.aspx\" class=\"menulist\">添加讨论组</a>&nbsp;&nbsp;&nbsp; <a href=\"javascript:PDel();\" class=\"topnavichar\">批量删除</a></span></td></tr></table>";
        return sc;
    }
    string Show_scs()
    {
        string scs = "<table width=\"100%\"  border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"toptable\">";
        scs += "<tr><td height=\"1\" colspan=\"2\"></td></tr>";
        scs += "<tr><td width=\"57%\"  class=\"sysmain_navi\"  style=\"PADDING-LEFT: 14px\" >讨论活动管理</td>";
        scs += "<td width=\"43%\"  class=\"topnavichar\"  style=\"PADDING-LEFT: 14px\" ><div align=\"left\">位置导航：<a href=\"../main.aspx\" target=\"sys_main\" class=\"list_link\">首页</a><img alt=\"\" src=\"../../sysImages/folder/navidot.gif\" border=\"0\" /><a href=\"discussManage_list.aspx\" class=\"menulist\">讨论组管理</a><img alt=\"\" src=\"../../sysImages/folder/navidot.gif\" border=\"0\" />我建立的讨论组</div></td></tr></table>";
        scs += "<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"3\" cellspacing=\"1\" class=\"Navitable\">";
        scs += "<tr><td><span class=\"topnavichar\" style=\"PADDING-LEFT: 14px\"><a href=\"discussManage_list.aspx\" class=\"menulist\">讨论组列表</a>　<a href=\"discussManagejoin_list.aspx\" class=\"menulist\">我加入的讨论组</a>&nbsp;&nbsp; <a href=\"discussManageestablish_list.aspx\" class=\"menulist\">我建立的讨论组</a>&nbsp;&nbsp; <a href=\"add_discussManage.aspx\" class=\"menulist\">添加讨论组</a></span></td></tr></table>";
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