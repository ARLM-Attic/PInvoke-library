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
using System.Text.RegularExpressions;
using Foosun.CMS;
using Foosun.CMS.Common;
using Foosun.Model;

public partial class General_manage : Foosun.Web.UI.ManagePage
{
    public General_manage()
    {
        Authority_Code = "Q018";
    }
    #region 创建示例
    sys rd = new sys();
    rootPublic pd = new rootPublic();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        #region 分页调用函数
        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_PageChange);
        #endregion

        if (!IsPostBack)
        {

            //LoginInfo.CheckPop("权限代码", "0", "1", "9");//权限代码
            copyright.InnerHtml = CopyRight;
            Response.CacheControl = "no-cache";//设置页面无缓存

            #region 取得参数，做出事件

            string type = Request.QueryString["type"];
            switch (type)
            {
                case "del":
                    this.Authority_Code = "Q019";
                    this.CheckAdminAuthority();
                    General_M_Del();//单个删除
                    break;
                case "suo":
                    this.Authority_Code = "Q019";
                    this.CheckAdminAuthority();
                    General_M_Suo();//单个锁定
                    break;
                case "unsuo":
                    this.Authority_Code = "Q019";
                    this.CheckAdminAuthority();
                    General_M_UnSuo();//单个解锁
                    break;
                case "delall":
                    this.Authority_Code = "Q019";
                    this.CheckAdminAuthority();
                    General_DelAll();//删除全部
                    break;
            }
            GenManageList(1);//初始分页数据
            #endregion
        }

    }

    protected void PageNavigator1_PageChange(object sender, int PageIndex)
    {
        GenManageList(PageIndex);
    }

    /// <summary>
    /// 单个删除事件函数
    /// </summary>
    ///Code by ChenZhaoHui

    #region 单个删除事件函数
    protected void General_M_Del()
    {
        int GID = int.Parse(Request.QueryString["ID"]);
        if (GID <= 0)
        {
            PageError("错误的参数传递!", "");
        }
        else
        {
            rd.General_M_Del(GID);
            pd.SaveUserAdminLogs(1, 1, UserNum, "单个删除", "常规管理单个删除成功.ID:" + GID + "");
            PageRight("删除成功。", "General_manage.aspx");
        }
    }
    #endregion


    /// <summary>
    /// 锁定事件函数
    /// </summary>
    ///Code by ChenZhaoHui

    #region 锁定事件函数
    protected void General_M_Suo()
    {
        int GID = int.Parse(Request.QueryString["ID"]);
        if (GID <= 0)
        {
            PageError("错误的参数传递!", "");
        }
        else
        {
            rd.General_M_Suo(GID);
            pd.SaveUserAdminLogs(1, 1, UserNum, "单个锁定", "常规管理单个锁定成功.ID:" + GID + "");
            PageRight("锁定成功。", "General_manage.aspx");
        }
    }
    #endregion

    /// <summary>
    /// 解锁定事件函数
    /// </summary>
    ///Code by ChenZhaoHui

    #region 解锁定事件函数
    protected void General_M_UnSuo()
    {
        int GID = int.Parse(Request.QueryString["ID"]);
        if (GID <= 0)
        {
            PageError("错误的参数传递!", "");
        }
        else
        {
            rd.General_M_UnSuo(GID);
            pd.SaveUserAdminLogs(1, 1, UserNum, "单个解锁", "常规管理单个解锁成功.ID:" + GID + "");
            PageRight("解锁成功。", "General_manage.aspx");
        }
    }
    #endregion

    /// <summary>
    /// 删除全部事件函数
    /// </summary>
    ///Code by ChenZhaoHui

    #region 删除全部事件函数
    protected void General_DelAll()
    {
        rd.General_DelAll();
        pd.SaveUserAdminLogs(1, 1, UserNum, "删除全部", "常规管理全部删除成功");
        PageRight("删除全部成功。", "General_manage.aspx");
    }
    #endregion

    /// <summary>
    /// 批量删除事件
    /// </summary>
    ///Code by ChenZhaoHui

    #region 批量删除事件
    protected void Del_ClickP(object sender, EventArgs e)
    {
        string general_checkbox = Request.Form["general_checkbox"];
        if (general_checkbox == null || general_checkbox == String.Empty)
        {
            PageError("请先选择批量操作的内容!", "");
        }
        else
        {
            String[] CheckboxArray = general_checkbox.Split(',');
            general_checkbox = null;
            for (int i = 0; i < CheckboxArray.Length; i++)
            {
                rd.General_M_Del(int.Parse(CheckboxArray[i].ToString()));
            }
            pd.SaveUserAdminLogs(1, 1, UserNum, "批量删除", "常规管理批量删除成功.ID:" + general_checkbox + "");
            PageRight("删除数据成功,请返回继续操作!", "General_manage.aspx");
        }
    }
    #endregion

    /// <summary>
    /// 批量锁定事件
    /// </summary>
    ///Code by ChenZhaoHui

    #region 批量锁定事件
    protected void Suo_ClickP(object sender, EventArgs e)
    {
        string general_checkbox = Request.Form["general_checkbox"];
        if (general_checkbox == null || general_checkbox == String.Empty)
        {
            PageError("请先选择批量操作的内容!", "");
        }
        else
        {
            String[] CheckboxArray = general_checkbox.Split(',');
            general_checkbox = null;
            for (int i = 0; i < CheckboxArray.Length; i++)
            {
                rd.General_M_Suo(int.Parse(CheckboxArray[i].ToString()));
            }
            pd.SaveUserAdminLogs(1, 1, UserNum, "常规管理数据批量锁定", "常规管理数据批量锁定成功.ID:" + general_checkbox + "");
            PageRight("锁定数据成功,请返回继续操作!", "General_manage.aspx");
        }
    }
    #endregion

    /// <summary>
    /// 批量解锁事件
    /// </summary>
    ///Code by ChenZhaoHui

    #region 批量解锁事件
    protected void Unsuo_ClickP(object sender, EventArgs e)
    {
        string general_checkbox = Request.Form["general_checkbox"];
        if (general_checkbox == null || general_checkbox == String.Empty)
        {
            PageError("请先选择批量操作的内容!", "");
        }
        else
        {
            String[] CheckboxArray = general_checkbox.Split(',');
            general_checkbox = null;
            for (int i = 0; i < CheckboxArray.Length; i++)
            {
                rd.General_M_UnSuo(int.Parse(CheckboxArray[i].ToString()));
            }
            pd.SaveUserAdminLogs(1, 1, UserNum, "常规管理数据批量解锁", "常规管理解锁数据成功.ID:" + general_checkbox + "");
            PageRight("解锁数据成功,请返回继续操作!", "General_manage.aspx");
        }
    }
    #endregion

    /// <summary>
    /// 显示常规管理页面
    /// </summary>
    ///Code by ChenZhaoHui

    #region 显示常规管理页面
    protected void GenManageList(int PageIndex)
    {
        string key = Request.QueryString["key"];
        int i, j;
        DataTable dt = null;
        if (key != null && key != "")
        {
            SQLConditionInfo st = new SQLConditionInfo("@gType", int.Parse(key.ToString()));
            dt = Foosun.CMS.Pagination.GetPage("General_manage_2_aspx", PageIndex, 20, out i, out j, st);
        }
        else
        {
            dt = Foosun.CMS.Pagination.GetPage("General_manage_1_aspx", PageIndex, 20, out i, out j, null);
        }
        #region 从参数设置里取得每页显示记录的条数
        int num = PAGESIZE;
        #endregion

        this.PageNavigator1.PageCount = j;
        this.PageNavigator1.PageIndex = PageIndex;
        this.PageNavigator1.RecordCount = i;
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {

                dt.Columns.Add("Type", typeof(String));//类型
                dt.Columns.Add("stat", typeof(String));//状态
                dt.Columns.Add("oPerate", typeof(String));//操作

                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    int id = int.Parse(dt.Rows[k]["id"].ToString());
                    int gType = int.Parse(dt.Rows[k]["gType"].ToString());
                    int isLock = int.Parse(dt.Rows[k]["isLock"].ToString());

                    #region 判断所添加的是什么类型
                    if (gType >= 0 && gType <= 3)
                    {
                        switch (gType)
                        {
                            case 0:
                                dt.Rows[k]["Type"] = "关键字(TAG)";
                                break;
                            case 1:
                                dt.Rows[k]["Type"] = "来源";
                                break;
                            case 2:
                                dt.Rows[k]["Type"] = "作者";
                                break;
                            case 3:
                                dt.Rows[k]["Type"] = "内部连接";
                                break;
                        }
                    }
                    else
                    {
                        dt.Rows[k]["Type"] = "未知类型";
                    }
                    #endregion

                    #region 判断所添加的常规选项是什么状态
                    switch (isLock)
                    {
                        case 0:
                            dt.Rows[k]["stat"] = "<img src=\"../../sysImages/folder/yes.gif\" border=\"0\">";
                            break;
                        case 1:
                            dt.Rows[k]["stat"] = "<img src=\"../../sysImages/folder/no.gif\" border=\"0\">";
                            break;
                        default:
                            dt.Rows[k]["stat"] = "<img src=\"../../sysImages/folder/yes.gif\" border=\"0\">";
                            break;
                    }
                    #endregion
                    dt.Rows[k]["Cname"] = "<a class=\"list_link\"  href=\"General_Edit_Manage.aspx?Action=edit&id=" + id + "&kkey=" + gType + "\" title=\"点击查看详情或修改\">" + dt.Rows[k]["Cname"].ToString() + "</a>";
                    dt.Rows[k]["oPerate"] = "<a class=\"list_link\"  href=\"General_Edit_Manage.aspx?Action=edit&id=" + id + "&kkey=" + gType + "\" title=\"点击查看详情或修改\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/edit.gif\" border=\"0\" alt=\"修改此常规项\" /></a><a class=\"list_link\" href=\"General_manage.aspx?type=suo&id=" + id + "\" title=\"点击锁定\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/lock.gif\" border=\"0\" alt=\"锁定此常规项\" /></a><a class=\"list_link\" href=\"General_manage.aspx?type=unsuo&id=" + id + "\" title=\"点击解锁\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/unlock.gif\" border=\"0\" alt=\"解锁此常规项\" /></a><a class=\"list_link\" href=\"General_manage.aspx?type=del&id=" + id + "\" title=\"点击删除\" onclick=\"{if(confirm('确认删除吗？')){return true;}return false;}\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/del.gif\" border=\"0\" alt=\"删除此常规项\" /></a><input type='checkbox' name='general_checkbox' id='general_checkbox'value=\"" + id + "\"/>";
                }
                DataList1.DataSource = dt;
                DataList1.DataBind();
            }
            else
            {
                NoContent.InnerHtml = Show_NoContent();
                this.PageNavigator1.Visible = false;
            }
        }
        else
        {
            NoContent.InnerHtml = Show_NoContent();
            this.PageNavigator1.Visible = false;
        }
    }
    #endregion


    /// <summary>
    /// 显示常规管理无内容提示
    /// </summary>
    ///Code by ChenZhaoHui

    string Show_NoContent()
    {

        string nos = "<table border=0 width='98%' align=center cellpadding=5 cellspacing=1 class='table'>";
        nos = nos + "<tr class='TR_BG_list'>";
        nos = nos + "<td class='navi_link'>当前没有记录！</td>";
        nos = nos + "</tr>";
        nos = nos + "</table>";
        return nos;
    }
}