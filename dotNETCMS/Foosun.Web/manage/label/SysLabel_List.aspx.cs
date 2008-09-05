///************************************************************************************************************
///**********标签管理Code By DengXi****************************************************************************
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

public partial class manage_label_SysLabel_List : Foosun.Web.UI.ManagePage
{
    public manage_label_SysLabel_List()
    {
        Authority_Code="T010";
    }
    public string Cname = "";
    public string ReloadURL = "";
    Foosun.CMS.Label ld = new Foosun.CMS.Label();
    Foosun.CMS.UserMisc rd = new Foosun.CMS.UserMisc();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";                        //设置页面无缓存
        if (!IsPostBack)
        {
            
            copyright.InnerHtml = CopyRight;
            ReloadURL = "http://passport.foosun.net/libary/dotnetcms/reloadlabel/label.aspx?type=reload";
            if (Foosun.Global.Current.SiteID == "0")
            {
                string getSiteID = Request.QueryString["SiteID"];
                if (getSiteID != null && getSiteID != "") { channelList.InnerHtml = SiteList(getSiteID); }
                else { channelList.InnerHtml = SiteList(Foosun.Global.Current.SiteID); }
            }
        }
        Op();
        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_PageChange);
        StartLoad(1);
    }

    /// <summary>
    /// 得到站点列表
    /// </summary>
    /// <param name="SessionSiteID"></param>
    /// <returns></returns>
    protected string SiteList(string SessionSiteID)
    {
        string siteStr = "<select name=\"SiteID\" id=\"SiteID\" onChange=\"getchanelInfo(this)\">\r";
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
        int i = 0;
        int j = 0;
        string str_classid = Request.QueryString["ClassID"];
        string action_s = Request.QueryString["s"];
        bool tf = false;
        DataTable dt = null;
        string _SiteID = "0";
        string getSiteID = Request.QueryString["SiteID"];
        if (getSiteID != "" && getSiteID != null)
        {
            _SiteID = getSiteID.ToString(); ;
        }
        else
        {
            if (SiteID != "0")
            {
                _SiteID = Foosun.Global.Current.SiteID;
            }
        }
        if (action_s != null && action_s != string.Empty)
        {
            SQLConditionInfo[] st = new SQLConditionInfo[2];
            st[0] = new SQLConditionInfo("@SiteID", _SiteID);
            st[1] = new SQLConditionInfo("@Keyword", "%" + Request.QueryString["keyword"].ToString() + "%");
            dt = Foosun.CMS.Pagination.GetPage("manage_label_SysLabel_List_3_aspx", PageIndex, 40, out i, out j, st);
            Cname = "标签名称";
            Back.InnerHtml = "";
        }
        else
        {
            if (str_classid == null || str_classid == "" || str_classid == string.Empty)
            {
                SQLConditionInfo st = new SQLConditionInfo("@SiteID", _SiteID);
                dt = Foosun.CMS.Pagination.GetPage("manage_label_SysLabel_List_1_aspx", PageIndex, 30, out i, out j, st);
                tf = true;

                Cname = "分类名称";
                Back.InnerHtml = "";
            }
            else
            {
                SQLConditionInfo[] st = new SQLConditionInfo[2];
                st[0] = new SQLConditionInfo("@SiteID", _SiteID);
                st[1] = new SQLConditionInfo("@ClassID", Foosun.Common.Input.Filter(str_classid));
                dt = Foosun.CMS.Pagination.GetPage("manage_label_SysLabel_List_2_aspx", PageIndex, 40, out i, out j, st);

                Cname = "标签名称";
                Back.InnerHtml = "&nbsp;┊&nbsp;<a href=\"SysLabel_List.aspx\" class=\"list_link\">返回上一级</a>";
            }
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
                dt.Columns.Add("Type", typeof(string));
                for (int k = 0; dt.Rows.Count > k; k++)
                {
                    if (tf == false)
                    {
                        string tstr = "";
                        if (dt.Rows[k]["Description"].ToString() != null && dt.Rows[k]["Description"].ToString()!=string.Empty)
                        {
                            tstr = "<div style=\"font-size:11.5px;color:#999999;\">" + dt.Rows[k]["Description"].ToString() + "</div>";
                        }

                        dt.Rows[k]["Type"] = "<span style=\"text-DECORATION:none;cursor:pointer;\" onclick=\"shdivlabel('" + dt.Rows[k]["LabelID"] + "');\" title=\"点击查看标签内容\">" + dt.Rows[k]["Label_Name"].ToString() + "</span>" + tstr + "";
                        if (str_classid == "99999999")
                        {
                            dt.Rows[k]["Op"] = "<a href=\"javascript:Update('Label','" + dt.Rows[k]["LabelID"].ToString() + "');\" class='list_link'><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/review.gif\" border=\"0\" alt=\"查看\" /></a>";
                        }
                        else
                        {
                            dt.Rows[k]["Op"] = "<a href=\"javascript:Update('Label','" + dt.Rows[k]["LabelID"].ToString() + "');\" class='list_link'><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/edit.gif\" border=\"0\" alt=\"修改\" /></a>&nbsp;<a href=\"sysLabel_out.aspx?LabelID=" + dt.Rows[k]["LabelID"].ToString() + "&LabelName=" + dt.Rows[k]["Label_Name"].ToString() + "&type=out\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/out.gif\" border=\"0\" alt=\"导出标签\" /></a>&nbsp;<a href=\"javascript:Bak('" + dt.Rows[k]["LabelID"].ToString() + "');\" class='list_link'><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/bak.gif\" border=\"0\" alt=\"放进备份库,注意:操作成功后此标签将不能使用.\" /></a>&nbsp;<a href=\"javascript:Del('Label','" + dt.Rows[k]["LabelID"].ToString() + "');\" class='list_link'><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/del.gif\" border=\"0\" alt=\"删除到回收站\" />&nbsp;<a href=\"javascript:Dels('Label','" + dt.Rows[k]["LabelID"].ToString() + "');\" class='list_link'><img src=\"../../sysImages/folder/dels.gif\" border=\"0\" alt=\"彻底删除\" /></a>";
                        }
                    }
                    else
                    {
                        int getCount = ld.getClassLabelCount(dt.Rows[k]["ClassID"].ToString(),0);
                        if (dt.Rows[k]["ClassID"].ToString() == "99999999")
                        {
                            dt.Rows[k]["Type"] = "<a class=\"list_link\" href=\"SysLabel_List.aspx?ClassID=" + dt.Rows[k]["ClassID"].ToString() + "&SiteID=" + Request.QueryString["SiteID"] + "\" title=\"点击查看此分类下的标签\"><img src=\"../../sysImages/folder/review.gif\" border=\"0\" /><font color=\"red\">" + dt.Rows[k]["ClassName"].ToString() + "</font></a><span class=\"reshow\" style=\"font-size:10px;\" title=\"此栏目下标签数\">(" + getCount + ")</span>";
                            dt.Rows[k]["Op"] = "<a href=\"javascript:Update('LabelClass','" + dt.Rows[k]["ClassID"].ToString() + "');\" class='list_link'><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/edit.gif\" border=\"0\" alt=\"修改\" /></a>&nbsp;<a href=\"javascript:reload();\" class='list_link'><img src=\"../../sysImages/folder/downlabel.gif\" border=\"0\" title=\"从风讯(Foosun.net)官方重新获取完整的系统标签\" /></a>";
                        }
                        else
                        {
                            dt.Rows[k]["Type"] = "<a class=\"list_link\" href=\"SysLabel_List.aspx?ClassID=" + dt.Rows[k]["ClassID"].ToString() + "&SiteID=" + Request.QueryString["SiteID"] + "\" title=\"点击查看此分类下的标签\"><img src=\"../../sysImages/folder/review.gif\" border=\"0\" />" + dt.Rows[k]["ClassName"].ToString() + "</a><span class=\"reshow\" style=\"font-size:10px;\" title=\"此栏目下标签数\">(" + getCount + ")</span>";
                            dt.Rows[k]["Op"] = "<a href=\"syslable_add.aspx?ClassID=" + dt.Rows[k]["ClassID"].ToString() + "\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/add.gif\" border=\"0\" alt=\"增加标签\" /></a>&nbsp;<a href=\"javascript:Update('LabelClass','" + dt.Rows[k]["ClassID"].ToString() + "');\" class='list_link'><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/edit.gif\" border=\"0\" alt=\"修改\" /></a>&nbsp;<a href=\"javascript:Del('LabelClass','" + dt.Rows[k]["ClassID"].ToString() + "');\" class='list_link'><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/del.gif\" border=\"0\" alt=\"删除到回收站\" /></a>&nbsp;<a href=\"javascript:Dels('LabelClass','" + dt.Rows[k]["ClassID"].ToString() + "');\" class='list_link'><img src=\"../../sysImages/folder/dels.gif\" border=\"0\" alt=\"彻底删除\" /></a>";
                        }
                    }
                }
            }
            DataList1.DataSource = dt;                              //设置datalist数据源
            DataList1.DataBind();                                   //绑定数据源
            dt.Clear();
            dt.Dispose();
        }
    }


    /// <summary>
    /// 执行操作
    /// </summary>
    /// <returns>执行操作</returns>
    /// 编写时间2007-04-23 Code By DengXi 

    protected void Op()
    {
        string str_Op = Request.QueryString["Op"];
        string str_Type = Request.QueryString["type"];
        string str_ID = Request.QueryString["ID"];
        switch (str_Op)
        {
            case "Del":
                switch (str_Type)
                {
                    case "Label":
                        delLabel(Foosun.Common.Input.checkID(str_ID));
                        break;
                    case "LabelClass":
                        delLabelClass(Foosun.Common.Input.checkID(str_ID));
                        break;
                    default:
                        break;
                }
                break;
            case "Dels":
                switch (str_Type)
                {
                    case "Label":
                        delsLabel(Foosun.Common.Input.checkID(str_ID));
                        break;
                    case "LabelClass":
                        delsLabelClass(Foosun.Common.Input.checkID(str_ID));
                        break;
                    default:
                        break;
                }
                break;
            case "Bak":
                LabelBak(Foosun.Common.Input.checkID(str_ID));
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 删除标签(放入回收站)
    /// </summary>
    /// <param name="ID">标签编号</param>
    /// <returns>删除标签(放入回收站)</returns>
    /// 编写时间2007-04-23   Code By DengXi

    protected void delLabel(string ID)
    {
        this.Authority_Code = "T013";
        this.CheckAdminAuthority();
        Foosun.CMS.Label lc = new Foosun.CMS.Label();
        lc.LabelDel(ID);
        PageRight("将标签放入回收站成功!", "SysLabel_List.aspx?ClassID=" + Request.QueryString["ClassID"]);
    }

    /// <summary>
    /// 删除标签(彻底删除)
    /// </summary>
    /// <param name="ID">标签编号</param>
    /// <returns>删除标签(彻底删除)</returns>
    /// 编写时间2007-04-22   Code By DengXi

    protected void delsLabel(string ID)
    {
        this.Authority_Code = "T014";
        this.CheckAdminAuthority();
        Foosun.CMS.Label lc = new Foosun.CMS.Label();
        lc.LabelDels(ID);
        PageRight("彻底删除标签成功!", "SysLabel_List.aspx?ClassID=" + Request.QueryString["ClassID"]);
    }

    /// <summary>
    /// 删除栏目(彻底删除)
    /// </summary>
    /// <param name="ID">栏目编号</param>
    /// <returns>删除栏目(彻底删除)</returns>
    /// 编写时间2007-04-23   Code By DengXi

    protected void delsLabelClass(string ID)
    {
        Foosun.CMS.Label lc = new Foosun.CMS.Label();
        lc.LabelClassDels(ID);
        PageRight("彻底删除栏目成功!", "SysLabel_List.aspx");
    }

    /// <summary>
    /// 删除栏目(放入回收站)
    /// </summary>
    /// <param name="ID">栏目编号</param>
    /// <returns>删除栏目(放入回收站)</returns>
    /// 编写时间2007-04-23   Code By DengXi

    protected void delLabelClass(string ID)
    {
        Foosun.CMS.Label lc = new Foosun.CMS.Label();
        lc.LabelClassDel(ID);
        PageRight("将栏目放入回收站成功!", "SysLabel_List.aspx");
    }

    /// <summary>
    /// 备份标签
    /// </summary>
    /// <param name="ID">标签编号</param>
    /// <returns>备份标签</returns>
    /// 编写时间2007-04-24   Code By DengXi
    
    protected void LabelBak(string ID)
    {
        Foosun.CMS.Label lc = new Foosun.CMS.Label();
        lc.LabelBackUp(ID);
        PageRight("将标签放入备份库成功!", "SysLabel_List.aspx?ClassID=" + Request.QueryString["ClassID"]);
    }
    
}
