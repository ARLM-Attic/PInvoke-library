///************************************************************************************************************
///**********专题管理Code By DengXi****************************************************************************
///************************************************************************************************************
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
using Foosun.Model;

public partial class manage_news_Special_List : Foosun.Web.UI.ManagePage
{
    public manage_news_Special_List()
    {
        Authority_Code = "C038";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";                        //设置页面无缓存
        if (!IsPostBack)
        {
            
            copyright.InnerHtml = CopyRight;            //获取版权信息
            StartLoad(1);
        }
        string Type = Request.QueryString["Type"];  //取得操作类型
        string ID = Request.QueryString["ID"];  //取得需要操作的专题ID
        string Mode = Request.QueryString["Mode"];
        switch (Type)
        {
            case "Lock":            //锁定专题
                Lock(Foosun.Common.Input.checkID(ID));
                break;
            case "UnLock":          //解锁专题
                UnLock(Foosun.Common.Input.checkID(ID));
                break;
            case "PDel":            //批量删除专题
                this.Authority_Code = "C037";
                this.CheckAdminAuthority();
                PDel(Mode);
                break;
            case "PUnlock":         //批量解锁专题
                this.Authority_Code = "C041";
                this.CheckAdminAuthority();
                PUnlock();
                break;
            case "Plock":           //批量锁定专题
                this.Authority_Code = "C041";
                this.CheckAdminAuthority();
                Plock();
                break;
            case "Publish":
                Publish();
                break;
            default:
                break;
        }
        if (SiteID == "0")
        {
            string getSiteID = Request.QueryString["SiteID"];
            if (getSiteID != null && getSiteID != "")
            {
                channelList.InnerHtml = "&nbsp;&nbsp;" + SiteList(getSiteID);
            }
            else
            {
                channelList.InnerHtml = "&nbsp;&nbsp;" + SiteList(SiteID);
            }
        }
        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_PageChange);
    }


    string SiteList(string SessionSiteID)
    {
        Foosun.CMS.UserMisc ud = new Foosun.CMS.UserMisc();
        string siteStr = "<select class=\"form\" name=\"SiteID\" id=\"SiteID\" onChange=\"getchanelInfo(this)\">\r";
        DataTable crs = ud.getSiteList();
        if (crs != null)
        {
            for (int i = 0; i < crs.Rows.Count; i++)
            {
                string getSiteID = SessionSiteID;
                string SiteID1 = crs.Rows[i]["ChannelID"].ToString();
                if (getSiteID != SiteID1)
                {
                    siteStr += "<option value=\"" + crs.Rows[i]["ChannelID"] + "\">==" + crs.Rows[i]["CName"] + "==</option>\r";
                }
                else
                {
                    siteStr += "<option value=\"" + crs.Rows[i]["ChannelID"] + "\"  selected=\"selected\">==" + crs.Rows[i]["CName"] + "==</option>\r";
                }
            }
        }
        //}
        siteStr += "</select>\r";
        return siteStr;
    }

    /// <summary>
    /// 分页
    /// </summary>
    /// <returns>分页</returns>
    /// Code By DengXi

    protected void PageNavigator1_PageChange(object sender, int PageIndex)
    {
        StartLoad(PageIndex);
    }
    protected void StartLoad(int PageIndex)
    {
        int i, j;
        string site = Request.QueryString["SiteID"];
        if (site != "" && site != null)
        {
            site = Request.QueryString["SiteID"].ToString();
        }
        else
        {
            if (SiteID != "0")
            {
                site = SiteID;
            }
            else
            {
                site = "0";
            }
        }
        SQLConditionInfo st = new SQLConditionInfo("@SiteID", site);
        DataTable dt = Foosun.CMS.Pagination.GetPage(this.GetType().Name, PageIndex, 20, out i, out j, st);

        this.PageNavigator1.PageCount = j;
        this.PageNavigator1.PageIndex = PageIndex;
        this.PageNavigator1.RecordCount = i;
        if (dt != null)
        {
            dt.Columns.Add("Op", typeof(string));
            dt.Columns.Add("Look", typeof(string));
            dt.Columns.Add("Lock", typeof(string));
            dt.Columns.Add("Colum", typeof(string));
            for (int k = 0; k < dt.Rows.Count; k++)
            {
                string strchar = null;
                //取出子类
                dt.Rows[k]["Op"] = "<a href=\"javascript:Update('" + dt.Rows[k]["SpecialID"] + "');\" class=\"list_link\">"+
                                   "<img src=\"../../sysImages/"+Foosun.Config.UIConfig.CssPath()+"/sysico/edit.gif\" border=\"0\" alt=\"修改此专题\" /></a>"+
                                   "<a href=\"javascript:Lock('" + dt.Rows[k]["SpecialID"] + "');\" class=\"list_link\">"+
                                   "<img src=\"../../sysImages/"+Foosun.Config.UIConfig.CssPath()+"/sysico/lock.gif\" border=\"0\" alt=\"锁定此专题\" /></a>"+
                                   "<a href=\"javascript:UnLock('" + dt.Rows[k]["SpecialID"] + "');\" class=\"list_link\">"+
                                   "<img src=\"../../sysImages/"+Foosun.Config.UIConfig.CssPath()+"/sysico/unlock.gif\" border=\"0\" alt=\"解锁此专题\" /></a>"+
                                   "<a href=\"javascript:AddChild('" + dt.Rows[k]["SpecialID"] + "');\" class=\"list_link\">"+
                                   "<img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/addclass.gif\" border=\"0\" alt=\"添加子专题\" /></a>" +
                                   "<a href=\"news_review.aspx?ID=" + dt.Rows[k]["SpecialID"] + "&type=special\" target=\"_blank\" class=\"list_link\">" +
                                   "<img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/review.gif\" border=\"0\" alt=\"添加子专题\" /></a>" +
                                   "<input type=\"checkbox\" value=\"'" + dt.Rows[k]["SpecialID"] + "'\" id=\"S_ID\" name=\"S_ID\" />";
                strchar += "<tr class=\"TR_BG_list\" onmouseover=\"javascript:overColor(this);\" onmouseout=\"javascript:outColor(this);\">";
                strchar += "<td align=\"left\" valign=\"middle\" title=\"专题ID：" + dt.Rows[k]["SpecialID"] + "\">" + dt.Rows[k]["SpecialCName"] + "</td>";
                strchar += "<td align=\"left\" valign=\"middle\" >" + dt.Rows[k]["CreatTime"] + "</td>";
                if (dt.Rows[k]["isLock"].ToString() == "1")
                    dt.Rows[k]["Lock"] = "<font color=\"red\">锁定</a>";
                else
                    dt.Rows[k]["Lock"] = "正常";
                strchar += "<td align=\"left\" valign=\"middle\" >" + dt.Rows[k]["Lock"] + "</td>";
                dt.Rows[k]["Look"] = "<a href=\"news_list.aspx?specialID=" + dt.Rows[k]["SpecialID"] + "\" class=\"list_link\">"+
                                     "<img src=\"../../sysImages/folder/review.gif\" border=\"0\" alt=\"查看所属此专题的所有新闻\" /></a>"+
                                     "(" + GetSpicaelNewsNum(dt.Rows[k]["SpecialID"].ToString()) + ")";
                strchar += "<td align=\"left\" valign=\"middle\" >" + dt.Rows[k]["Look"] + "</td>";
                strchar += "<td align=\"left\" valign=\"middle\" >" + dt.Rows[k]["Op"] + "</td>";
                strchar += "</tr>";
                strchar += ChileList(dt.Rows[k]["SpecialID"].ToString(), "├");
                dt.Rows[k]["Colum"] = strchar;
            }
            DataList1.DataSource = dt;
            DataList1.DataBind();
            dt.Clear();
            dt.Dispose();
        }
    }

    //递归
    protected string ChileList(string Classid, string sign)
    {
        string strchar = null;
        Foosun.CMS.Special sc = new Foosun.CMS.Special();
        DataTable dv = sc.getChildList(Classid);
        sign += "─";
        if (dv != null)
        {
            dv.Columns.Add("Op", typeof(string));
            dv.Columns.Add("Look", typeof(string));
            dv.Columns.Add("Lock", typeof(string));
            for (int pi = 0; pi < dv.Rows.Count; pi++)
            {
                dv.Rows[pi]["Op"] = "<a href=\"javascript:Update('" + dv.Rows[pi]["SpecialID"] + "');\" class=\"list_link\">"+
                                    "<img src=\"../../sysImages/"+Foosun.Config.UIConfig.CssPath()+"/sysico/edit.gif\" border=\"0\" alt=\"修改此专题\" /></a>"+
                                    "<a href=\"javascript:Lock('" + dv.Rows[pi]["SpecialID"] + "');\" class=\"list_link\">"+
                                    "<img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/lock.gif\" border=\"0\" alt=\"锁定此专题\" /></a>" +
                                    "<a href=\"javascript:UnLock('" + dv.Rows[pi]["SpecialID"] + "');\" class=\"list_link\">"+
                                    "<img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/unlock.gif\" border=\"0\" alt=\"解锁此专题\" /></a>" +
                                    "<a href=\"javascript:AddChild('" + dv.Rows[pi]["SpecialID"] + "');\" class=\"list_link\">" +
                                    "<img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/addclass.gif\" border=\"0\" alt=\"添加子专题\" /></a>" +
                                    "<a href=\"news_review.aspx?ID=" + dv.Rows[pi]["SpecialID"] + "&type=special\" target=\"_blank\" class=\"list_link\">" +
                                    "<img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/review.gif\" border=\"0\" alt=\"添加子专题\" /></a>" +
                                    "<input type=\"checkbox\" value=\"'" + dv.Rows[pi]["SpecialID"] + "'\" id=\"S_ID\" name=\"S_ID\" />";
                strchar += "<tr class=\"TR_BG_list\" onmouseover=\"javascript:overColor(this);\" onmouseout=\"javascript:outColor(this);\">";
                strchar += "<td align=\"left\" valign=\"middle\" >" + sign + dv.Rows[pi]["SpecialCName"] + "</td>";
                strchar += "<td align=\"left\" valign=\"middle\" >" + dv.Rows[pi]["CreatTime"] + "</td>";
                if (dv.Rows[pi]["isLock"].ToString() == "1")
                    dv.Rows[pi]["Lock"] = "<font color=\"red\">锁定</a>";
                else
                    dv.Rows[pi]["Lock"] = "正常";
                strchar += "<td align=\"left\" valign=\"middle\">" + dv.Rows[pi]["Lock"] + "</td>";
                dv.Rows[pi]["Look"] = "<a href=\"news_list.aspx?Type=special&specialID=" + dv.Rows[pi]["SpecialID"] + "\" class=\"list_link\" "+
                                      " title=\"查看所属此专题的所有新闻\"><img src=\"../../sysImages/folder/review.gif\" border=\"0\" "+
                                      " alt=\"查看所属此专题的所有新闻\" /></a>(" + GetSpicaelNewsNum(dv.Rows[pi]["SpecialID"].ToString()) + ")";
                strchar += "<td align=\"left\" valign=\"middle\" >" + dv.Rows[pi]["Look"] + "</td>";
                strchar += "<td align=\"left\" valign=\"middle\" >" + dv.Rows[pi]["Op"] + "</td>";
                strchar += "</tr>";
                strchar += ChileList(dv.Rows[pi]["SpecialID"].ToString(), sign);
            }
            dv.Clear();
            dv.Dispose();
        }
        return strchar;
    }


    /// <summary>
    /// 锁定专题
    /// </summary>
    /// <param name="ID">专题编号</param>
    /// <returns>锁定专题</returns>
    /// Code By DengXi

    protected void Lock(string ID)
    {
        Foosun.CMS.Special sc = new Foosun.CMS.Special();
        sc.Lock(ID);
        PageRight("锁定专题成功!", "");
    }

    /// <summary>
    /// 解锁专题
    /// </summary>
    /// <param name="ID">专题编号</param>
    /// <returns>解锁专题</returns>
    /// Code By DengXi
    
    protected void UnLock(string ID)
    {
        Foosun.CMS.Special sc = new Foosun.CMS.Special();
        sc.UnLock(ID);
        PageRight("解锁专题成功!", "");
    }

    /// <summary>
    /// 批量删除专题
    /// </summary>
    /// <param name="Mode">详细的操作,如果参数值是"Re",则就删除到回收站,否则就为彻底删除</param>
    /// <returns>批量删除专题</returns>
    /// Code By DengXi

    protected void PDel(string Mode)
    {
        if (Request.Form["S_ID"] == null || Request.Form["S_ID"] == "")
            PageError("请选择要批量删除的专题!", "special_list.aspx");
        string str_SID = "'" + Request.Form["S_ID"].Replace(",", "','") + "'";
        str_SID = Foosun.Common.Input.Losestr(str_SID);
        if (str_SID == "IsNull")
            PageError("请选择要批量删除的专题!", "special_list.aspx");

        Foosun.CMS.Special sc = new Foosun.CMS.Special();

        if (Mode == "Re")
        {
            sc.PDel(str_SID);
            PageRight("将专题删除到回收站成功!", "special_list.aspx");
        }
        else
        {
            this.Authority_Code = "C0401";
            this.CheckAdminAuthority();
            sc.PDels(str_SID);
            PageRight("彻底删除成功!", "special_list.aspx");
        }
    }

    /// <summary>
    /// 批量锁定专题
    /// </summary>
    /// <returns>批量锁定专题</returns>
    /// Code By DengXi

    protected void Plock()
    {
        string str_SID = Request.Form["S_ID"];
        str_SID = Foosun.Common.Input.Losestr(str_SID);
        if (str_SID == "IsNull")
            PageError("请选择要批量锁定的专题!", "special_list.aspx");
        
        Foosun.CMS.Special sc = new Foosun.CMS.Special();
        sc.PLock(str_SID);
        PageRight("批量锁定成功!", "special_list.aspx");
    }

    /// <summary>
    /// 批量解锁专题
    /// </summary>
    /// <returns>批量解锁专题</returns>
    /// Code By DengXi

    protected void PUnlock()
    {
        string str_SID = Request.Form["S_ID"];
        str_SID = Foosun.Common.Input.Losestr(str_SID);
        if (str_SID == "IsNull")
            PageError("请选择要批量解锁的专题!", "special_list.aspx");

        Foosun.CMS.Special sc = new Foosun.CMS.Special();
        sc.PUnLock(str_SID);
        PageRight("批量解锁成功!<br />如果批量选中的专题还有未解锁的,请先解锁此专题的父专题!", "special_list.aspx");
    }


    protected void Publish()
    {
        string str_SID = Request.Form["S_ID"];
        str_SID = Foosun.Common.Input.Losestr(str_SID);
        if (str_SID == "IsNull")
            PageError("请选择要生成的专题!", "special_list.aspx");
        
        string[] arr_SID = str_SID.Split(',');

        Foosun.Control.HProgressBar.Start();
        Foosun.Publish.General PG = new Foosun.Publish.General();
        try
        {
            Foosun.Control.HProgressBar.Roll("正在发布专题", 0);
            int j = 0;
            int m = arr_SID.Length;
            for (int i = 0; i < m; i++)
            {
                if (PG.publishSingleSpecial(arr_SID[i].ToString().Replace("'", "")))
                    j++;
                Foosun.Control.HProgressBar.Roll("共生成" + m + "个专题，正在发布" + (i + 1) + "个。", ((i + 1) * 100 / m));
            }
            Foosun.Control.HProgressBar.Roll("发布专题成功,成功" + j + "个,<a href=\"../Publish/error/geterror.aspx?\">失败" + (arr_SID.Length - j) + "个(可能有专题有浏览权限)</a>. &nbsp;<a href=\"special_list.aspx\">返回</a>", 100);
        }
        catch (Exception ex)
        {
            Foosun.Common.Public.savePublicLogFiles("□□□发布专题", "【错误描述：】\r\n" + ex.ToString(), UserName);
            Foosun.Control.HProgressBar.Roll("发布专题失败。<a href=\"../publish/error/geterror.aspx?\">查看日志</a>", 0);
        }
        Response.End();
    }


    protected void makeHTML(object sender, EventArgs e)
    {
        string Str = Request.Form["Checkbox1"];
        if (Str == null || Str == String.Empty)
        {
            PageError("请至少选择一项!", "");
        }
        else
        {

        }
    }



    /// <summary>
    /// 获得当前专题下面的新闻数目
    /// </summary>
    /// <param name="ID">专题编号</param>
    /// <returns>获得当前专题下面的新闻数目</returns>
    /// Code By DengXi
    
    protected string GetSpicaelNewsNum(string SID)
    {
        Foosun.CMS.Special sc = new Foosun.CMS.Special();
        string cntnum = sc.getSpicaelNewsNum(SID);
        return cntnum;    
    }
}
