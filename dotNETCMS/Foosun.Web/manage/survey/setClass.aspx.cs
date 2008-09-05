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
using Foosun.Model;

public partial class manage_survey_setClass : Foosun.Web.UI.ManagePage
{
    public manage_survey_setClass()
    {
        Authority_Code = "S005";
    }
    Survey sur = new Survey();
    rootPublic rd = new rootPublic();
    protected void Page_Load(object sender, EventArgs e)
    {
        #region 分页调用函数
        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_PageChange);
        #endregion
        Response.CacheControl = "no-cache"; //清除缓存
        if (!IsPostBack) //判断页面是否重载
        {
            //判断用户是否登录
            copyright.InnerHtml = CopyRight;//获取版权信息
            if (SiteID == "0")
            {
                param_id.InnerHtml = "<a href=\"setParam.aspx\" class=\"list_link\">系统参数设置</a>&nbsp;┊&nbsp;";
            }
            VoteClassManage(1);//初始分页数据

            #region 单个操作事件获取参数
            string type = Request.QueryString["type"];
            switch (type)
            {
                case "edit":
                    setClassEdit();
                    break;
                case "delone":
                    setClassDel();
                    break;
            }
            #endregion
        }
    }
    /// <summary>
    /// 分页
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="PageIndex"></param>
    ///code by chenzhaohui

    protected void PageNavigator1_PageChange(object sender, int PageIndex)
    {
        VoteClassManage(PageIndex);//管理页面分页查询
    }

    /// <summary>
    /// 初始修改页面数据
    /// </summary>
    /// Code By ChenZhaoHui


    protected void setClassEdit()
    {
        int VID = int.Parse(Request.QueryString["ID"]);
        DataTable dt = sur.Str_VoteSql(VID);
        if (dt.Rows.Count > 0)
        {
            this.ClassNameEdit.Text = dt.Rows[0]["ClassName"].ToString().Trim();
            this.DescriptionE.Value = dt.Rows[0]["Description"].ToString().Trim();
        }
        else
        {
            PageError("未知错误,异常错误", "setClass.aspx");
        }
    }

    /// <summary>
    /// 删除单个事件
    /// </summary>
    ///Code By ChenZhaoHui

    protected void setClassDel()
    {
        int VID = int.Parse(Request.QueryString["ID"]);
        int tid = 0;
        DataTable dt = new DataTable();
        if (VID <= 0)
        {
            PageError("错误的参数传递!", "");
        }
        else
        {
            //删除类别后，相应的该分类下的主题，选项，多部投票都应被删除
            try
            {
                dt = sur.sel_TitleSql(VID);
                tid = int.Parse(dt.Rows[0]["TID"].ToString());//Title的TID
            }
            catch { }
            #region bool
            bool v1 = sur.Str_VoteSql_1(VID);
            bool v2 = sur.Str_VoteTitleSql(VID);
            bool v3 = sur.Str_VoteItemSql(tid);
            bool v4 = sur.Str_VoteStepsSql(tid);
            bool v5 = sur.Str_VoteManageSql(tid);
            #endregion

            if (v1 && v2 && v3 && v4 && v5)
            {
                rd.SaveUserAdminLogs(1, 1, UserNum, "删除投票分类", "删除成功");
                PageRight("删除成功。", "setClass.aspx");
            }
            else
            {
                rd.SaveUserAdminLogs(1, 1, UserNum, "删除投票分类", "删除数据失败");
                PageError("意外错误：未知错误", "setClass.aspx");
            }
        }
    }

    /// <summary>
    /// 显示管理页
    /// </summary>
    /// <param name="PageIndex"></param>
    /// Code By ChenZhaoHui

    protected void VoteClassManage(int PageIndex)//显示投票类别管理页面
    {
        #region 查询分类开始

        string Keywrd = Foosun.Common.Input.Filter(this.KeyWord.Text.Trim());

        int i = 0, j = 0;
        int num = PAGESIZE;//从参数设置里取得每页显示记录的条数
        DataTable dt = null;
        if (Keywrd != "" && Keywrd != null)
        {
            switch (this.DdlKwdType.SelectedValue)
            {
                case "choose":
                    dt = Foosun.CMS.Pagination.GetPage("manage_survey_setClass_1_aspx", PageIndex, num, out i, out j, null);
                    break;
                case "number":
                    SQLConditionInfo st = new SQLConditionInfo("@VID", "%" + Keywrd + "%");
                    dt = Foosun.CMS.Pagination.GetPage("manage_survey_setClass_2_aspx", PageIndex, num, out i, out j, st);//按照id查询
                    break;
                case "classname":
                    SQLConditionInfo sts = new SQLConditionInfo("@ClassName", "%" + Keywrd + "%");
                    dt = Foosun.CMS.Pagination.GetPage("manage_survey_setClass_3_aspx", PageIndex, num, out i, out j, sts);//按照类名查询
                    break;
                case "description":
                    SQLConditionInfo stss = new SQLConditionInfo("@Description", "%" + Keywrd + "%");
                    dt = Foosun.CMS.Pagination.GetPage("manage_survey_setClass_4_aspx", PageIndex, num, out i, out j, stss);
                    break;
                default:
                    break;
            }
        }
        else
        {
            dt = Foosun.CMS.Pagination.GetPage("manage_survey_setClass_1_aspx", PageIndex, num, out i, out j, null);
        }
        #endregion
        this.PageNavigator1.PageCount = j;
        this.PageNavigator1.PageIndex = PageIndex;
        this.PageNavigator1.RecordCount = i;

        if (dt != null)//判断如果dt里面没有内容，将不会显示
        {
            if (dt.Rows.Count > 0)
            {
                //添加列
                dt.Columns.Add("oPerate", typeof(String));//操作
                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    int id = int.Parse(dt.Rows[k]["vid"].ToString());
                    dt.Rows[k]["ClassName"] = "<a href='setClass.aspx?type=edit&id=" + id + "' class='list_link' title='点击查看详情或修改'>" + dt.Rows[k]["ClassName"].ToString() + "</a>";
                    dt.Rows[k]["oPerate"] = "<a href=\"setClass.aspx?type=edit&id=" + id + "\"  class=\"list_link\" title=\"修改此项\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/edit.gif\" border=\"0\" alt=\"修改此项\" /></a><a href=\"setClass.aspx?type=delone&id=" + id + "\"  class=\"list_link\" title=\"删除此项\" onclick=\"{if(confirm('确认删除吗？')){return true;}return false;}\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/del.gif\" border=\"0\" alt=\"删除此项\" /></a><input type='checkbox' name='vote_checkbox' id='vote_checkbox' value=\"" + id + "\"/>";
                }
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
        DataList1.DataSource = dt;
        DataList1.DataBind();
    }
    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// code by chenzhaohui


    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        VoteClassManage(1);//查询事件，其值由相应条件来判断并显示出来
    }

    /// <summary>
    /// 新增类别
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// code by chenzhaohui 

    protected void SaveClass_ServerClick(object sender, EventArgs e)
    {
        if (Page.IsValid)//判断页面是否通过验证
        {
            //取得投票参数设置添加中的表单信息
            string Str_ClassName = Foosun.Common.Input.Filter(this.ClassName.Text.Trim());//类别名称
            string Str_Description = Foosun.Common.Input.Filter(this.Description.Value.Trim());//描述

            //检查是否有已经存在的类别名称
            if (sur.sel_1(Str_ClassName) != 0)
            {
                PageError("对不起，该类别已经存在", "setClass.aspx?type=add");
            }
            //判断类别名称是否为空
            if (Str_ClassName == null || Str_ClassName == string.Empty)
            {
                PageError("对不起，类别名称不能为空，请返回继续添加", "setClass.aspx");
            }
            //向数据库中写入添加的类别信息

            //载入数据-刷新页面
            if (sur.Add(Str_ClassName, Str_Description, SiteID) != 0)
            {
                rd.SaveUserAdminLogs(1, 1, UserNum, "新增投票分类", "新增成功");
                PageRight("问卷调查系统新增类别成功", "setClass.aspx");
            }
            else
            {
                PageError("意外错误：未知错误", "shortcut_list.aspx");
            }

        }
    }

    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    ///code by chenzhaohui 

    protected void DelP_Click(object sender, EventArgs e)
    {
        string vote_checkbox = Request.Form["vote_checkbox"];
        int tid = 0;
        DataTable dt = new DataTable();

        if (vote_checkbox == null || vote_checkbox == String.Empty)
        {
            PageError("请先选择批量操作的内容!", "");
        }
        else
        {
            String[] CheckboxArray = vote_checkbox.Split(',');
            vote_checkbox = null;
            for (int i = 0; i < CheckboxArray.Length; i++)
            {
                try
                {
                    dt = sur.sel_2(CheckboxArray[i]);
                    tid = int.Parse(dt.Rows[0]["TID"].ToString());//Title的TID
                }
                catch { }

                #region 删除分类下的其他信息
                #endregion
                #region 更新数据
                sur.Del_Str_VoteSql(CheckboxArray[i]);
                sur.Del_Str_VoteTitleSql(CheckboxArray[i]);
                sur.Del_Str_VoteItemSql(tid);
                sur.Del_Str_VoteStepsSql(tid);
                sur.Del_Str_VoteManageSql(tid);
                #endregion
            }
            rd.SaveUserAdminLogs(1, 1, UserNum, "批量删除投票分类", "批量删除成功");
            PageRight("删除成功。", "setClass.aspx");
        }
    }

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// code by chenzhaohui

    protected void EditSave_ServerClick(object sender, EventArgs e)
    {
        if (Page.IsValid)//判断页面是否通过验证
        {
            int VID = int.Parse(Request.QueryString["ID"]);
            //取得添加中的表单信息
            string Str_ClassNameE = Foosun.Common.Input.Filter(this.ClassNameEdit.Text.Trim());//类别名称
            string Str_DescriptionE = Foosun.Common.Input.Filter(this.DescriptionE.Value.Trim());//描述

            //判断类别名称是否为空
            if (Str_ClassNameE == null || Str_ClassNameE == string.Empty)
            {
                PageError("对不起，类别名称不能为空，请返回继续修改", "setClass.aspx");
            }

            //刷新页面
            if (sur.Update_Str_InSql(Str_ClassNameE, Str_DescriptionE, VID) != 0)
            {
                rd.SaveUserAdminLogs(1, 1, UserNum, "修改投票分类", "修改成功");
                PageRight("修改成功", "setClass.aspx");
            }
            else
            {
                PageError("意外错误：未知错误", "");
            }

        }
    }

    /// <summary>
    /// 删除全部
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// code by chenzhaohui

    protected void DelAll_Click(object sender, EventArgs e)
    {
        bool v1 = sur.Del_1();
        bool v2 = sur.Del_2();
        bool v3 = sur.Del_3();
        bool v4 = sur.Del_4();
        if (v1 && v2 && v3 && v4)
        {
            rd.SaveUserAdminLogs(1, 1, UserNum, "删除全部投票分类", "删除全部成功");
            PageRight("删除全部成功。", "setClass.aspx");
        }
        else
        {
            PageError("意外错误：未知错误", "");
        }
    }

    /// <summary>
    /// 无内容提示页
    /// </summary>
    /// <returns></returns>
    ///code by chenzhaohui 

    string Show_NoContent()
    {
        string type = Request.QueryString["type"];
        string nos = "";
        if (type != "add" && type != "edit")
        {
            nos = "<table border=0 width='98%' align=center cellpadding=5 cellspacing=1 class='table'>";
            nos = nos + "<tr class='TR_BG_list'>";
            nos = nos + "<td class='navi_link'>当前没有满足条件的分类！</td>";
            nos = nos + "</tr>";
            nos = nos + "</table>";
        }
        return nos;
    }
}
