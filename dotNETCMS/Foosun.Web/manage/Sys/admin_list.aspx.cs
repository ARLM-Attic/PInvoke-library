///************************************************************************************************************
///**********管理员管理,Code By DengXi*************************************************************************
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
using Foosun.CMS;
using Foosun.Model;

public partial class manage_Sys_admin_list : Foosun.Web.UI.ManagePage
{
    public manage_Sys_admin_list()
    {
        Authority_Code = "Q010";
    }
    Foosun.CMS.UserMisc rd = new Foosun.CMS.UserMisc();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            copyright.InnerHtml = CopyRight;            //获取版权信息
            Response.CacheControl = "no-cache";                        //设置页面无缓存
            if (SiteID == "0")
            {
                string getSiteID = Request.QueryString["SiteID"];
                if (getSiteID != null && getSiteID != "") { channelList.InnerHtml = SiteList(getSiteID); }
                else { channelList.InnerHtml = SiteList(Foosun.Global.Current.SiteID); }
            }
            StartLoad(1);
        }
        string Type = Request.QueryString["Type"];  //取得操作类型
        string ID = Request.QueryString["ID"];  //取得需要操作的管理员ID
        switch (Type)
        {
            case "Lock":            //锁定管理员
                this.Authority_Code = "Q014";
                this.CheckAdminAuthority();
                Lock(Foosun.Common.Input.checkID(ID));
                break;
            case "UnLock":          //解锁管理员
                this.Authority_Code = "Q014";
                this.CheckAdminAuthority();
                UnLock(Foosun.Common.Input.checkID(ID));
                break;
            case "Del":             //删除管理员
                this.Authority_Code = "Q013";
                this.CheckAdminAuthority();
                Del(Foosun.Common.Input.checkID(ID));
                break;
            default:
                break;
        }
        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_PageChange);
    }

    /// <summary>
    /// 得到站点列表
    /// </summary>
    /// <param name="SessionSiteID"></param>
    /// <returns></returns>
    protected string SiteList(string SessionSiteID)
    {
        string siteStr = "<select class=\"form\" name=\"SiteID\" id=\"SiteID\" onChange=\"getchanelInfo(this)\">\r";
        DataTable crs = rd.getSiteList();
        if (crs != null)
        {
            for (int i = 0; i < crs.Rows.Count; i++)
            {
                string getSiteID = SessionSiteID;
                string SiteID1 = crs.Rows[i]["ChannelID"].ToString();
                if (getSiteID != SiteID1) { siteStr += "<option value=\"" + crs.Rows[i]["ChannelID"] + "\">==" + crs.Rows[i]["CName"] + "==</option>\r"; }
                else { siteStr += "<option value=\"" + crs.Rows[i]["ChannelID"] + "\"  selected=\"selected\">==" + crs.Rows[i]["CName"] + "==</option>\r"; }
            }
            crs.Clear(); crs.Dispose();
        }
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
        DataTable dt = null;
        string site = Request.QueryString["SiteID"];
        if (site != "" && site != null)
        {
            SQLConditionInfo st = new SQLConditionInfo("@SiteID", site);
            dt = Foosun.CMS.Pagination.GetPage("manage_Sys_admin_list_1_aspx", PageIndex, 20, out i, out j, st);
        }
        else
        {
            dt = Foosun.CMS.Pagination.GetPage(this.GetType().Name, PageIndex, 20, out i, out j, null);
        }
        this.PageNavigator1.PageCount = j;
        this.PageNavigator1.PageIndex = PageIndex;
        this.PageNavigator1.RecordCount = i;
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                //----------------------------------------添加列------------------------------------------------
                dt.Columns.Add("Op", typeof(string));
                dt.Columns.Add("Super", typeof(string));
                dt.Columns.Add("userNames", typeof(string));
                dt.Columns.Add("Lock", typeof(string));
                for (int k = 0; dt.Rows.Count > k; k++)
                {
                    //----------------------取得数据库中数字,判断输出中文意思-----------------------------------
                    if (dt.Rows[k]["isSuper"].ToString() == "1") { dt.Rows[k]["Super"] = "是"; } else { dt.Rows[k]["Super"] = "否"; }
                    if (dt.Rows[k]["isLock"].ToString() == "1") { dt.Rows[k]["Lock"] = "<font color=\"red\">锁定</font>"; } else { dt.Rows[k]["Lock"] = "正常"; }
                    if (dt.Rows[k]["isSuper"].ToString() == "0")    //判断是否超级管理员,如果是超管,则不显示锁定,解锁,删除功能.
                    {
                        dt.Rows[k]["Op"] = "<a href=\"javascript:Update('" + dt.Rows[k]["UserNum"].ToString() + "');\" class='list_link'><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/edit.gif\" border=\"0\" title=\"修改\" /></a><a href=\"javascript:Del('" + dt.Rows[k]["UserNum"].ToString() + "');\" class='list_link'><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/del.gif\" border=\"0\" title=\"删除\" /></a><a href=\"javascript:Lock('" + dt.Rows[k]["UserNum"].ToString() + "');\" class='list_link'><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/lock.gif\" border=\"0\" title=\"锁定\" /></a><a href=\"javascript:UnLock('" + dt.Rows[k]["UserNum"].ToString() + "');\" class='list_link'><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/unlock.gif\" border=\"0\" title=\"解锁\" /></a><a href=\"admin_POPSet.aspx?UserNum=" + dt.Rows[k]["UserNum"].ToString() + "&id=" + dt.Rows[k]["Id"].ToString() + "\" class='list_link'><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/set.gif\" border=\"0\" title=\"设置权限\" /></a>";
                    }
                    else
                    {
                        dt.Rows[k]["Op"] = "<a href=\"javascript:Update('" + dt.Rows[k]["UserNum"].ToString() + "');\" class='list_link'><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/edit.gif\" border=\"0\" title=\"修改\" /></a>";
                    }
                    Foosun.CMS.Common.rootPublic pd = new Foosun.CMS.Common.rootPublic();
                    dt.Rows[k]["userNames"] = "<a class=\"list_link\" href=\"../../" + Foosun.Config.UIConfig.dirUser + "/showUser.aspx?uid=" + pd.getUserName(dt.Rows[k]["UserNum"].ToString()) + "\" target=\"_blank\">" + pd.getUserName(dt.Rows[k]["UserNum"].ToString()) + "</a>";
                }
            }
            DataList1.DataSource = dt;                              //设置datalist数据源
            DataList1.DataBind();                                   //绑定数据源
            dt.Clear();
            dt.Dispose();
        }
    }

    /// <summary>
    /// 锁定管理员
    /// </summary>
    /// <param name="ID">管理员编号</param>
    /// <returns>锁定管理员</returns>
    /// Code By DengXi

    protected void Lock(string ID)
    {
        Foosun.CMS.Admin ac = new Foosun.CMS.Admin();
        ac.Lock(ID);
        PageRight("锁定管理员成功!", "");
    }

    /// <summary>
    /// 解锁管理员
    /// </summary>
    /// <param name="ID">管理员编号</param>
    /// <returns>解锁管理员</returns>
    /// Code By DengXi

    protected void UnLock(string ID)
    {
        Foosun.CMS.Admin ac = new Foosun.CMS.Admin();
        ac.UnLock(ID);
        PageRight("解锁管理员成功!", "");
    }

    /// <summary>
    /// 删除管理员
    /// </summary>
    /// <param name="ID">管理员编号</param>
    /// <returns>删除管理员</returns>
    /// Code By DengXi    

    protected void Del(string ID)
    {
        Foosun.CMS.Admin ac = new Foosun.CMS.Admin();
        ac.Del(ID);
        PageRight("删除管理员成功!", "");
    }
}
