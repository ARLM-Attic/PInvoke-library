//=====================================================================
//==                  (C)2007 Foosun Inc.By doNetCMS1.0              ==
//==                     Forum:bbs.foosun.net                        ==
//==                     WebSite:www.foosun.net                      ==
//==                 Address:No.109 HuiMin ST,.ChengDu,China         ==
//==                   Tel:86-28-85098980/66026180                   ==
//==                   QQ:655071,MSN:ikoolls@gmail.com               ==
//==                   Email:Service@foosun.cn                       ==
//==                      Code By ChenZhaoHui                        ==
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
using Foosun.CMS.Common;

public partial class manage_news_History_Manage : Foosun.Web.UI.ManagePage
{
    public manage_news_History_Manage()
    {
        Authority_Code = "C048";
    }
    News ns = new News();
    rootPublic log = new rootPublic();
    protected void Page_Load(object sender, EventArgs e)
    {
        #region 分页调用函数
        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_PageChange);
        #endregion
        Response.CacheControl = "no-cache";//设置页面无缓存
        HistoryManageList(1);//分页初始值
        if (!IsPostBack)
        {

            copyright.InnerHtml = CopyRight;
        }
    }
    #region 管理页面分页查询
    protected void PageNavigator1_PageChange(object sender, int PageIndex)
    {
        HistoryManageList(PageIndex);
    }
    #endregion

    /// <summary>
    /// 归档管理页面
    /// </summary>
    /// Code By ChenZhaohui

    protected void HistoryManageList(int PageIndex)
    {

        int i, j;
        DataTable dt = Foosun.CMS.Pagination.GetPage("manage_news_History_Manage_aspx", PageIndex, 20, out i, out j, null);

        this.PageNavigator1.PageCount = j;
        this.PageNavigator1.PageIndex = PageIndex;
        this.PageNavigator1.RecordCount = i;

        #region 判断如果dt里面没有内容，将不会显示
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                dt.Columns.Add("Type", typeof(String));//类型
                dt.Columns.Add("table", typeof(String));//所属表
                dt.Columns.Add("stat", typeof(String));//状态
                dt.Columns.Add("oPerate", typeof(String));//操作
                //----------------------------------------------
                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    int id = int.Parse(dt.Rows[k]["id"].ToString());
                    string NewsType = dt.Rows[k]["NewsType"].ToString();
                    string islock = dt.Rows[k]["isLock"].ToString();
                    string DataLib = dt.Rows[k]["DataLib"].ToString();
                    #region 控制标题显示字数为6个字的截断
                    //if (dt.Rows[k]["NewsTitle"].ToString().Length > 6)
                    //{
                    //    dt.Rows[k]["NewsTitle"] = (dt.Rows[k]["NewsTitle"].ToString().Substring(0, 6).ToString());
                    //}
                    //else
                    //{
                    //    dt.Rows[k]["NewsTitle"] = (dt.Rows[k]["NewsTitle"].ToString());
                    //}
                    #endregion

                    #region 判断新闻的类型，以区分不同
                    switch (NewsType)
                    {
                        case "0":
                            dt.Rows[k]["Type"] = "普通";
                            break;
                        case "1":
                            dt.Rows[k]["Type"] = "图片";
                            break;
                        case "2":
                            dt.Rows[k]["Type"] = "标题";
                            break;
                        default:
                            dt.Rows[k]["Type"] = "普通";
                            break;
                    }
                    #endregion
                    #region 判断新闻所属表
                    dt.Rows[k]["table"] = DataLib;
                    #endregion
                    #region 新闻锁定状态
                    switch (islock)
                    {
                        case "0":
                            dt.Rows[k]["stat"] = "<img src=\"../../sysImages/folder/yes.gif\" border=\"0\">";
                            break;
                        case "1":
                            dt.Rows[k]["stat"] = "<img src=\"../../sysImages/folder/no.gif\" border=\"0\">";
                            break;
                        default:
                            dt.Rows[k]["stat"] = "<img src=\"../../sysImages/folder/yes.gif\" border=\"0\">";
                            break;
                    }
                    #endregion
                    #region 复选框操作
                    dt.Rows[k]["oPerate"] = "<input type='checkbox' name='history_checkbox',id='history_checkbox' value=\"" + id + "\"/>";
                    #endregion
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
    /// 批量删除
    /// </summary>
    /// Code By ChenZhaohui

    protected void Del_ClickP(object sender, EventArgs e)
    {
        this.Authority_Code = "C049";
        this.CheckAdminAuthority();
        string history_checkbox = Request.Form["history_checkbox"];
        if (history_checkbox == null || history_checkbox == string.Empty)
        {
            PageError("请先选择删除操作的新闻!", "");
        }
        else
        {
            String[] CheckboxArray = history_checkbox.Split(',');
            history_checkbox = null;
            for (int i = 0; i < CheckboxArray.Length; i++)
            {
                if (ns.delPP(CheckboxArray[i]) == 0)
                {
                    log.SaveUserAdminLogs(1, 1, UserNum, "新闻数据删除失败", "新闻数据删除失败");
                    PageError("新闻数据删除失败,请与管理联系!", "");
                    break;
                }
            }
            log.SaveUserAdminLogs(1, 1, UserNum, "新闻数据删除成功", "新闻数据删除成功");
            PageRight("新闻数据删除成功,请返回继续操作!", "History_Manage.aspx");
        }
    }
    /// <summary>
    /// 批量锁定
    /// </summary>
    /// Code By ChenZhaohui

    protected void Suo_ClickP(object sender, EventArgs e)
    {
        this.Authority_Code = "C049";
        this.CheckAdminAuthority();
        string history_checkbox = Request.Form["history_checkbox"];
        if (history_checkbox == null || history_checkbox == string.Empty)
        {
            PageError("请先选择锁定操作的新闻!", "");
        }
        else
        {
            String[] CheckboxArray = history_checkbox.Split(',');
            history_checkbox = null;
            for (int i = 0; i < CheckboxArray.Length; i++)
            {
                if (ns.locks(CheckboxArray[i]) == 0)
                {
                    PageError("新闻数据锁定失败,请与管理联系!", "");
                    break;
                }
            }
            log.SaveUserAdminLogs(1, 1, UserNum, "新闻数据锁定成功", "新闻数据锁定成功");
            PageRight("新闻数据锁定成功,请返回继续操作!", "History_Manage.aspx");
        }
    }
    /// <summary>
    /// 批量解锁
    /// </summary>
    /// Code By ChenZhaohui

    protected void Unsuo_ClickP(object sender, EventArgs e)
    {
        this.Authority_Code = "C049";
        this.CheckAdminAuthority();
        string history_checkbox = Request.Form["history_checkbox"];
        if (history_checkbox == null || history_checkbox == string.Empty)
        {
            PageError("请先选择解锁操作的新闻!", "");
        }
        else
        {
            String[] CheckboxArray = history_checkbox.Split(',');
            history_checkbox = null;
            for (int i = 0; i < CheckboxArray.Length; i++)
            {
                if (ns.unlovkc(CheckboxArray[i]) == 0)
                {
                    log.SaveUserAdminLogs(1, 1, UserNum, "新闻数据解锁失败", "新闻数据解锁失败");
                    PageError("新闻数据解锁失败,请与管理联系!", "");
                    break;
                }
            }
            log.SaveUserAdminLogs(1, 1, UserNum, "新闻数据解锁成功", "新闻数据解锁成功");
            PageRight("新闻数据解锁成功,请返回继续操作!", "History_Manage.aspx");
        }
    }
    /// <summary>
    /// 生成索引
    /// </summary>
    /// Code By ChenZhaohui

    protected void Index_ClickP(object sender, EventArgs e)
    {
        this.Authority_Code = "C049";
        this.CheckAdminAuthority();
        Foosun.Control.HProgressBar.Start();
        int getHistoryNum = int.Parse(Foosun.Common.Public.readparamConfig("HistoryNum"));
        try
        {
            Foosun.Control.HProgressBar.Roll("正在发布索引", 0);
            int m = 0;
            int j = 0;
            for (int i = 0; i < getHistoryNum; i++)
            {
                if (Foosun.Publish.General.publishHistryIndex(i))
                {
                    m++;
                }
                else
                {
                    j++;
                }
                Foosun.Control.HProgressBar.Roll("正在发布第" + i + "天,共" + getHistoryNum + ",失败" + j + "个(可能当天没归档新闻。)", ((i + 1) * 100 / getHistoryNum));
            }
            Foosun.Control.HProgressBar.Roll("发布索引成功, 共" + getHistoryNum + ",失败" + j + "个(可能当天没归档新闻。). &nbsp;<a href=\"history_Manage.aspx\">返回</a>", 100);
        }
        catch (Exception ex)
        {
            Foosun.Common.Public.savePublicLogFiles("□□□发布索引", "【错误描述：】\r\n" + ex.ToString(), UserName);
            Foosun.Control.HProgressBar.Roll("发布索引失败。<a href=\"error/geterror.aspx?\">查看日志</a>", 0);
        }
        Response.End();
    }

    /// <summary>
    /// 删除全部
    /// </summary>
    /// Code By ChenZhaohui

    protected void DelAll_ClickP(object sender, EventArgs e)
    {
        this.Authority_Code = "C049";
        this.CheckAdminAuthority();
        int delap = ns.delalpl();
        if (delap == 0)
        {
            log.SaveUserAdminLogs(1, 1, UserNum, "删除全部归档新闻", "新闻数据全部删除失败");
            PageError("新闻数据全部删除失败,请与管理联系!", "");
        }
        log.SaveUserAdminLogs(1, 1, UserNum, "删除全部归档新闻", "新闻数据全部删除成功");
        PageRight("新闻数据全部删除成功,请返回继续操作!", "History_Manage.aspx");
    }

    /// <summary>
    /// 提示无内容显示信息
    /// </summary>
    /// code by chenzhaohui

    string Show_NoContent()
    {

        string nos = "<table border=0 width='98%' align=center cellpadding=5 cellspacing=1 class='table'>";
        nos = nos + "<tr class='TR_BG_list'>";
        nos = nos + "<td class='navi_link'>当前没有被归档的新闻！</td>";
        nos = nos + "</tr>";
        nos = nos + "</table>";
        return nos;
    }
}
