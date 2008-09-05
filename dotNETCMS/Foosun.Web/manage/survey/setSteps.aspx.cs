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

public partial class manage_survey_setSteps : Foosun.Web.UI.ManagePage
{
    public manage_survey_setSteps()
    {
        Authority_Code = "S005";
    }
    Survey sur = new Survey();
    rootPublic rd = new rootPublic();
    protected void Page_Load(object sender, EventArgs e)
    {
        #region 初始代码
        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_PageChange);
        if (!IsPostBack) //判断页面是否重载
        {
            //判断用户是否登录
            copyright.InnerHtml = CopyRight;//获取版权信息
            if (SiteID == "0")
            {
                param_id.InnerHtml = "<a href=\"setParam.aspx\" class=\"list_link\">系统参数设置</a>&nbsp;┊&nbsp;";
            }
            VoteStepsManage(1);  //初始分页数据

            try
            {
                #region 选择主题(新增时)
                DataTable dt = sur.sel_3();
                //调查主题
                this.vote_CNameSe.DataTextField = "Title";
                this.vote_CNameSe.DataValueField = "TID";
                this.vote_CNameSe.DataSource = dt;
                this.vote_CNameSe.DataBind();
                //调用主题
                this.vote_CNameUse.DataTextField = "Title";
                this.vote_CNameUse.DataValueField = "TID";
                this.vote_CNameUse.DataSource = dt;
                this.vote_CNameUse.DataBind();
                #endregion

                #region 选择主题(修改时)
                //调查主题
                this.votecnameEditse.DataTextField = "Title";
                this.votecnameEditse.DataValueField = "TID";
                this.votecnameEditse.DataSource = dt;
                this.votecnameEditse.DataBind();
                //调用主题
                this.votecnameEditue.DataTextField = "Title";
                this.votecnameEditue.DataValueField = "TID";
                this.votecnameEditue.DataSource = dt;
                this.votecnameEditue.DataBind();
                #endregion
            }
            catch { }

            #region pram
            string type = Request.QueryString["type"];
            switch (type)
            {
                case "edit":
                    setStepsEdit();
                    break;
                case "delone":
                    setStepsDel();
                    break;
            }
            #endregion
        }
        #endregion
    }

    /// <summary>
    /// 分页
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="PageIndex"></param>
    /// code by chenzhaohui 

    protected void PageNavigator1_PageChange(object sender, int PageIndex)
    {
        VoteStepsManage(PageIndex);//管理页面分页查询
    }

    /// <summary>
    /// 管理页列表
    /// </summary>
    /// <param name="PageIndex"></param>
    /// code by chenzhaohui

    protected void VoteStepsManage(int PageIndex)//显示投票选项管理页面
    {
        #region 查询操作条件判断语句
        string KeyWord = Foosun.Common.Input.Filter(this.KeyWord.Text.Trim());//关键字
        string type = this.DdlKwdType.SelectedValue;//类型
        int i = 0, j = 0;
        int num = PAGESIZE;//从参数设置里取得每页显示记录的条数
        DataTable dt = null;
        if (KeyWord != "" && KeyWord != null)//如果关键字不为空，则执行下面的查询操作
        {
            switch (type)
            {
                case "choose":
                    dt = Foosun.CMS.Pagination.GetPage("manage_survey_setSteps_1_aspx", PageIndex, num, out i, out j, null);
                    break;
                case "nums":
                    SQLConditionInfo st = new SQLConditionInfo("@SID", "%" + KeyWord + "%");
                    dt = Foosun.CMS.Pagination.GetPage("manage_survey_setSteps_2_aspx", PageIndex, num, out i, out j, st);
                    break;
                case "titles":
                    DataTable dt1 = new DataTable();
                    dt1 = sur.sel_4(KeyWord);
                    if (dt1 != null)
                    {
                        if (dt1.Rows.Count > 0)
                        {
                            for (int l = 0; l < dt1.Rows.Count; l++)
                            {
                                int tid = int.Parse(dt1.Rows[l]["TID"].ToString());
                                SQLConditionInfo st1 = new SQLConditionInfo("@TIDS", "%" + tid + "%");
                                dt = Foosun.CMS.Pagination.GetPage("manage_survey_setSteps_3_aspx", PageIndex, num, out i, out j, st1);
                            }
                        }
                        else
                        {
                            dt = Foosun.CMS.Pagination.GetPage("manage_survey_setSteps_1_aspx", PageIndex, num, out i, out j, null);
                        }
                    }
                    else
                    {
                        dt = Foosun.CMS.Pagination.GetPage("manage_survey_setSteps_1_aspx", PageIndex, num, out i, out j, null);
                    }
                    break;
                case "nunber":
                    SQLConditionInfo st2 = new SQLConditionInfo("@Steps", "%" + KeyWord + "%");
                    dt = Foosun.CMS.Pagination.GetPage("manage_survey_setSteps_4_aspx", PageIndex, num, out i, out j, st2);
                    break;
                case "titleu":
                    DataTable dt2 = new DataTable();
                    dt2 = sur.sel_4(KeyWord);
                    if (dt2 != null)
                    {
                        if (dt2.Rows.Count > 0)
                        {
                            for (int a = 0; a < dt2.Rows.Count; a++)
                            {
                                int tid = int.Parse(dt2.Rows[a]["TID"].ToString());
                                SQLConditionInfo st3 = new SQLConditionInfo("@TIDU", "%" + tid + "%");
                                dt = Foosun.CMS.Pagination.GetPage("manage_survey_setSteps_5_aspx", PageIndex, num, out i, out j, st3);
                            }
                        }
                        else
                        {
                            dt = Foosun.CMS.Pagination.GetPage("manage_survey_setSteps_1_aspx", PageIndex, num, out i, out j, null);
                        }
                    }
                    else
                    {
                        dt = Foosun.CMS.Pagination.GetPage("manage_survey_setSteps_1_aspx", PageIndex, num, out i, out j, null);
                    }
                    break;
            }
        }
        else
        {
            dt = Foosun.CMS.Pagination.GetPage("manage_survey_setSteps_1_aspx", PageIndex, num, out i, out j, null);
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
                dt.Columns.Add("titlesearch", typeof(String));//调查主题
                dt.Columns.Add("num", typeof(String));//步骤
                dt.Columns.Add("titleuse", typeof(String));//调用主题
                dt.Columns.Add("oPerate", typeof(String));//操作

                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    int SID = int.Parse(dt.Rows[k]["SID"].ToString());//自动编号 
                    int TIDS = int.Parse(dt.Rows[k]["TIDS"].ToString());//调查主题
                    int TIDU = int.Parse(dt.Rows[k]["TIDU"].ToString());//调用主题
                    try
                    {
                        //从调查类别表中取类别名
                        string VoteTitleNameS = sur.sel_VoteTitleS_Sql(TIDS);//取得调查主题的值
                        string VoteTitleNameU = sur.sel_VoteTitleU_Sql(TIDU);//取得调用主题的值
                        string VoteStepsNum = sur.sel_VoteSteps_Sql(SID);//取得步骤的值

                        dt.Rows[k]["titlesearch"] = VoteTitleNameS;//将查找出来的值传给调查主题栏
                        dt.Rows[k]["titleuse"] = VoteTitleNameU;//将查找出来的值传给调用主题栏
                        dt.Rows[k]["num"] = "<a href='setSteps.aspx?type=edit&id=" + SID + "' class='list_link' title='点击查看详情或修改'>第" + VoteStepsNum + "步</a>";//将查找出来的值传给步骤栏
                    }
                    catch { }

                    dt.Rows[k]["oPerate"] = "<a href=\"setSteps.aspx?type=edit&id=" + SID + "\"  class=\"list_link\" title=\"修改此项\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/edit.gif\" border=\"0\" alt=\"修改此项\" /></a><a href=\"setSteps.aspx?type=delone&id=" + SID + "\"  class=\"list_link\" title=\"删除此项\" onclick=\"{if(confirm('确认删除吗？')){return true;}return false;}\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/del.gif\" border=\"0\" alt=\"删除此项\" /></a><input type='checkbox' name='vote_checkbox' id='vote_checkbox' value=\"" + SID + "\"/>";
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
        VoteStepsManage(1);
    }

    /// <summary>
    /// 新增加
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// code by chenzhaohui

    protected void Savesteps_ServerClick(object sender, EventArgs e)
    {
        if (Page.IsValid)//判断页面是否通过验证
        {
            //取得设置添加中的表单信息
            string Str_vote_CNameSe = this.vote_CNameSe.SelectedValue;//调查主题
            string Str_StepsN = Foosun.Common.Input.Filter(this.StepsN.Text);//步骤
            string Str_vote_CNameUse = this.vote_CNameUse.SelectedValue;//调用主题

            //检查是否有已经存在的
            if (sur.sel_Str_CheckSql(Str_vote_CNameSe, Str_vote_CNameUse) != 0)
            {
                PageError("对不起，您提交的数据已经存在，请返回重新添加", "setSteps.aspx");
            }
            //判断数据是否为空
            if (Str_vote_CNameSe == null || Str_vote_CNameSe == string.Empty || Str_vote_CNameUse == null || Str_vote_CNameUse == string.Empty)
            {
                PageError("对不起，您提交的数据有空值!\n请选择相应的调查主题和调用主题!", "setSteps.aspx");
            }
            //判断数字类型
            if (!Foosun.Common.Input.IsInteger(Str_StepsN))
            {
                PageError("抱歉，顺序号应为数字型，请确保您输入的值为数字型", "setSteps.aspx");
            }
            //向数据库中写入添加的类别信息
            ///载入数据-刷新页面
            if (sur.Add_Str_InSql_1(Str_vote_CNameSe, Str_vote_CNameUse, Str_StepsN, "0") != 0)
            {
                rd.SaveUserAdminLogs(1, 1, UserNum, "问卷调查系统多步投票", "问卷调查系统新增多步投票成功");
                PageRight("问卷调查系统新增多步投票成功", "setSteps.aspx");
            }
            else
            {
                PageError("意外错误：未知错误", "shortcut_list.aspx");
            }

        }
    }

    /// <summary>
    /// 修改初始页面信息
    /// </summary>
    ///code by chenzhaohui 

    void setStepsEdit()
    {
        int SID = int.Parse(Request.QueryString["ID"]);
        DataTable dt = sur.sel_6(SID);
        if (dt.Rows.Count > 0)
        {
            this.votecnameEditse.Text = dt.Rows[0]["TIDS"].ToString();
            this.NumEdit.Text = dt.Rows[0]["Steps"].ToString();
            this.votecnameEditue.Text = dt.Rows[0]["TIDU"].ToString();
        }
        else
        {
            PageError("未知错误,异常错误", "setSteps.aspx");
        }
    }

    /// <summary>
    /// 删除单个事件
    /// </summary>
    /// code by chenzhaohui

    void setStepsDel()
    {
        int SID = int.Parse(Request.QueryString["ID"]);
        if (SID <= 0)
        {
            PageError("错误的参数传递!", "");
        }
        else
        {
            if (sur.Del_5(SID) == 0)
            {
                PageError("意外错误：未知错误", "");
            }
            else
            {
                rd.SaveUserAdminLogs(1, 1, UserNum, "问卷调查系统多步投票", "删除成功");
                PageRight("删除成功。", "setSteps.aspx");
            }
        }
    }

    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// code by chenzhaohui

    protected void DelP_Click(object sender, EventArgs e)
    {
        string vote_checkbox = Request.Form["vote_checkbox"];
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
                if (sur.Del_6(CheckboxArray[i]) == 0)
                {
                    PageError("删除数据失败,请与管理联系!", "");
                    break;
                }
            }
            rd.SaveUserAdminLogs(1, 1, UserNum, "批量删除投票系统多步投票", "批量删除投票系统多步投票成功");
            PageRight("删除数据成功,请返回继续操作!", "setSteps.aspx");
        }
    }


    /// <summary>
    /// 全部删除
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// code by chenzhaohui

    protected void DelAll_Click(object sender, EventArgs e)
    {
        if (sur.Del_7() == 0)
        {
            PageError("意外错误：未知错误", "");
        }
        else
        {
            rd.SaveUserAdminLogs(1, 1, UserNum, "多步投票", "删除全部成功");
            PageRight("删除全部成功。", "setSteps.aspx");
        }
    }

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// code by chenzhaohui

    protected void SavestepsEdit_ServerClick(object sender, EventArgs e)
    {
        if (Page.IsValid)//判断页面是否通过验证
        {
            int SID = int.Parse(Request.QueryString["ID"]);
            //取得修改中的表单信息
            string Str_votecnameEditse = Foosun.Common.Input.Filter(this.votecnameEditse.Text.Trim());
            string Str_NumEdit = Foosun.Common.Input.Filter(this.NumEdit.Text.Trim());
            string Str_votecnameEditue = Foosun.Common.Input.Filter(this.votecnameEditue.Text.Trim());
            //判断数据是否为空
            if (Str_votecnameEditse == null || Str_votecnameEditse == string.Empty || Str_votecnameEditue == null || Str_votecnameEditue == string.Empty)
            {
                PageError("对不起，您提交的数据有空值!\n请选择相应的调查主题和调用主题!", "setSteps.aspx");
            }
            //判断数字类型
            if (!Foosun.Common.Input.IsInteger(Str_NumEdit))
            {
                PageError("抱歉，顺序号应为数字型，请确保您输入的值为数字型", "setSteps.aspx");
            }

            //刷新页面
            if (sur.Update_1(Str_votecnameEditse, Str_votecnameEditue, Str_NumEdit, SID) != 0)
            {
                rd.SaveUserAdminLogs(1, 1, UserNum, "多步投票修改", "修改成功");
                PageRight("修改成功", "setSteps.aspx");
            }
            else
            {
                PageError("意外错误：未知错误", "");
            }

        }
    }

    /// <summary>
    /// 无内容提示页
    /// </summary>
    /// <returns></returns>
    /// code by chenzhaohui

    string Show_NoContent()
    {
        string type = Request.QueryString["type"];
        string nos = "";
        if (type != "add" && type != "edit")
        {
            nos = "<table border=0 width='98%' align=center cellpadding=5 cellspacing=1 class='table'>";
            nos = nos + "<tr class='TR_BG_list'>";
            nos = nos + "<td class='navi_link'>当前没有满足条件的多步投票！</td>";
            nos = nos + "</tr>";
            nos = nos + "</table>";
        }
        return nos;
    }
}
