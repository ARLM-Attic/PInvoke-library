///************************************************************************************************************
///**********样式管理Code By DengXi****************************************************************************
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
using System.Text.RegularExpressions;

public partial class manage_label_style : Foosun.Web.UI.ManagePage
{
    public manage_label_style()
    {
        Authority_Code = "T017";
    }
    public string Cname = "";
    Foosun.CMS.Label ld = new Foosun.CMS.Label();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";                        //设置页面无缓存
        if (!IsPostBack)
        {

            copyright.InnerHtml = CopyRight;       //获取版权信息
        }
        Op();
        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_PageChange);
        StartLoad(1);
    }


    /// <summary>
    /// 分页
    /// </summary>
    /// <returns>分页</returns>

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
        if (action_s != null && action_s != string.Empty)
        {
            SQLConditionInfo[] st = new SQLConditionInfo[2];
            st[0] = new SQLConditionInfo("@SiteID", SiteID);
            st[1] = new SQLConditionInfo("@Keyword", "%" + Request.QueryString["keyword"].ToString() + "%");
            dt = Foosun.CMS.Pagination.GetPage("manage_label_style_3_aspx", PageIndex, 40, out i, out j, st);
            Cname = "样式名称";
            Back.InnerHtml = "";
        }
        else
        {
            if (str_classid == null || str_classid == "" || str_classid == string.Empty)
            {
                SQLConditionInfo st = new SQLConditionInfo("@SiteID", SiteID);
                dt = Foosun.CMS.Pagination.GetPage("manage_label_style_1_aspx", PageIndex, 20, out i, out j, st);
                tf = true;
                Cname = "分类名称";
                Back.InnerHtml = "";
            }
            else
            {
                SQLConditionInfo[] st = new SQLConditionInfo[2];
                st[0] = new SQLConditionInfo("@ClassID", Foosun.Common.Input.checkID(str_classid));
                st[1] = new SQLConditionInfo("@SiteID", SiteID);
                dt = Foosun.CMS.Pagination.GetPage("manage_label_style_2_aspx", PageIndex, 20, out i, out j, st);
                Cname = "样式名称";
                Back.InnerHtml = " | <a href=\"style.aspx\" class=\"list_link\">返回上一级</a>";
            }
        }

        this.PageNavigator1.PageCount = j;
        this.PageNavigator1.PageIndex = PageIndex;
        this.PageNavigator1.RecordCount = i;
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                //--------------------------------添加列
                dt.Columns.Add("Op", typeof(string));
                dt.Columns.Add("Type", typeof(string));
                dt.Columns.Add("no", typeof(string));
                dt.Columns.Add("contents", typeof(string));
                for (int k = 0; dt.Rows.Count > k; k++)
                {
                    if (tf == false)
                    {
                        dt.Rows[k]["no"] = " 编号：<span style=\"font-size:10px;color:#999999;\" title=\"编号\">" + dt.Rows[k]["styleID"].ToString() + "</span>";

                        dt.Rows[k]["Type"] = "<span style=\"cursor:pointer;\" onclick=\"getReview('" + dt.Rows[k]["id"].ToString() + "');\">" + dt.Rows[k]["StyleName"].ToString() + "</span>";
                        //-----------------------如果分类编号是"99999999",则为系统内置栏目,只能修改,不能删除
                        string tmContent = dt.Rows[k]["Content"].ToString();
                        tmContent = Regex.Replace(tmContent, @"<img(.+?){(.+?)}(.+?)>", "<img src=\"../../SysImages/folder/spic.png\" border=\"0\" title=\"样式中的标签\" />", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                        dt.Rows[k]["contents"] = tmContent;
                        if (str_classid == "99999999")
                        {
                            dt.Rows[k]["Op"] = "<a href=\"javascript:Update('style','" + dt.Rows[k]["styleID"].ToString() + "');\" class='list_link'><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/edit.gif\" border=\"0\" alt=\"修改\" /></a>" + dt.Rows[k]["no"];
                        }
                        else
                        {
                            dt.Rows[k]["Op"] = "<a href=\"javascript:Update('style','" + dt.Rows[k]["styleID"].ToString() + "');\" class='list_link'><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/edit.gif\" border=\"0\" alt=\"修改\" /></a><a href=\"javascript:Del('style','" + dt.Rows[k]["id"].ToString() + "');\" class='list_link'><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/del.gif\" border=\"0\" alt=\"删除到回收站\" /><a href=\"javascript:Dels('style','" + dt.Rows[k]["styleID"].ToString() + "');\" class='list_link'><img src=\"../../sysImages/folder/dels.gif\" border=\"0\" alt=\"彻底删除\" /></a>" + dt.Rows[k]["no"];
                        }

                    }
                    else
                    {
                        int getCount = ld.getClassLabelCount(dt.Rows[k]["ClassID"].ToString(), 1);
                        dt.Rows[k]["Type"] = "<a class=\"list_link\" href=\"style.aspx?ClassID=" + dt.Rows[k]["ClassID"].ToString() + "\" title=\"点击查看此分类下的样式。&#13;此样式分类编号：" + dt.Rows[k]["ClassID"].ToString() + "\">" + dt.Rows[k]["Sname"].ToString() + "</a><span class=\"reshow\" style=\"font-size:10px;\" title=\"此栏目下样式数量\">(" + getCount + ")</span>";
                        if (str_classid == "99999999")
                            dt.Rows[k]["Op"] = "<a href=\"javascript:Update('styleclass','" + dt.Rows[k]["ClassID"].ToString() + "');\" class='list_link'><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/edit.gif\" border=\"0\" alt=\"修改\" /></a>";
                        else
                            dt.Rows[k]["Op"] = "<a href=\"javascript:Update('styleclass','" + dt.Rows[k]["ClassID"].ToString() + "');\" class='list_link'><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/edit.gif\" border=\"0\" alt=\"修改\" /></a><a href=\"javascript:Del('styleclass','" + dt.Rows[k]["ClassID"].ToString() + "');\" class='list_link'><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/del.gif\" border=\"0\" alt=\"删除到回收站\" /></a><a href=\"javascript:Dels('styleclass','" + dt.Rows[k]["ClassID"].ToString() + "');\" class='list_link'><img src=\"../../sysImages/folder/dels.gif\" border=\"0\" alt=\"彻底删除\" /></a>";
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
    /// Code By DengXi 

    protected void Op()
    {
        this.Authority_Code = "T018";
        this.CheckAdminAuthority();
        string str_Op = Request.QueryString["Op"];
        string str_Type = Request.QueryString["type"];
        string str_ID = Request.QueryString["ID"];
        switch (str_Op)
        {
            case "Del":
                switch (str_Type)
                {
                    case "style":
                        delstyle(Foosun.Common.Input.checkID(str_ID));
                        break;
                    case "styleclass":
                        if (ld.getClassLabelCount(str_ID.ToString(), 1) != 0)
                        {
                            PageError("分类下有样式，不能删除样式分类", "style.aspx");
                        }
                        else
                        {
                            delclass(Foosun.Common.Input.checkID(str_ID));
                        }
                        break;
                    default:
                        break;
                }
                break;
            case "Dels":
                switch (str_Type)
                {
                    case "style":
                        delsstyle(Foosun.Common.Input.checkID(str_ID));
                        break;
                    case "styleclass":
                        if (ld.getClassLabelCount(str_ID.ToString(), 1) != 0)
                        {
                            PageError("分类下有样式，不能删除样式分类", "style.aspx");
                        }
                        else
                        {
                            delsclass(Foosun.Common.Input.checkID(str_ID));
                        }
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
    }


    /// <summary>
    /// 删除栏目(彻底删除)
    /// </summary>
    /// <param name="ID">栏目编号</param>
    /// <returns>删除栏目(彻底删除)</returns>
    /// 编写时间2007-04-22   Code By DengXi

    protected void delsclass(string ID)
    {
        this.Authority_Code = "T019";
        this.CheckAdminAuthority();
        Foosun.CMS.Style.Style stClass = new Foosun.CMS.Style.Style();
        stClass.styleClassDel(ID);
        PageRight("彻底删除栏目成功!", "style.aspx");
    }

    /// <summary>
    /// 删除栏目(放入回收站)
    /// </summary>
    /// <param name="ID">栏目编号</param>
    /// <returns>删除栏目(放入回收站)</returns>
    /// 编写时间2007-04-22   Code By DengXi

    protected void delclass(string ID)
    {
        this.Authority_Code = "T019";
        this.CheckAdminAuthority();
        Foosun.CMS.Style.Style stClass = new Foosun.CMS.Style.Style();
        stClass.styleClassRDel(ID);
        PageRight("将栏目放入回收站成功!", "style.aspx");
    }

    /// <summary>
    /// 删除样式(放入回收站)
    /// </summary>
    /// <param name="ID">样式编号</param>
    /// <returns>删除样式(放入回收站)</returns>
    /// 编写时间2007-04-22   Code By DengXi

    protected void delstyle(string ID)
    {
        Foosun.CMS.Style.Style stClass = new Foosun.CMS.Style.Style();
        stClass.styleRdel(ID);
        PageRight("将样式放入回收站成功!", "style.aspx");
    }

    /// <summary>
    /// 删除样式(彻底删除)
    /// </summary>
    /// <param name="ID">样式编号</param>
    /// <returns>删除样式(彻底删除)</returns>
    /// 编写时间2007-04-22   Code By DengXi

    protected void delsstyle(string ID)
    {
        Foosun.CMS.Style.Style stClass = new Foosun.CMS.Style.Style();
        stClass.styleDel(ID);
        PageRight("彻底删除样式成功!", "style.aspx");
    }
}
