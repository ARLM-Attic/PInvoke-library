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

public partial class manage_survey_setItem : Foosun.Web.UI.ManagePage
{
    public manage_survey_setItem()
    {
        Authority_Code = "S004";
    }
    Survey sur = new Survey();
    rootPublic rd = new rootPublic();
    protected void Page_Load(object sender, EventArgs e)
    {
        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_PageChange);
        if (!IsPostBack) //判断页面是否重载
        {
            //判断用户是否登录
            copyright.InnerHtml = CopyRight;//获取版权信息
            if (SiteID == "0")
            {
                param_id.InnerHtml = "<a href=\"setParam.aspx\" class=\"list_link\">系统参数设置</a>&nbsp;┊&nbsp;";
            }
            VoteItemManage(1);  //初始分页数据

            #region 选择类别,主题(新增选项时)
            DataTable dt = sur.Str_SelectSql();
            //显示类别主题
            if (dt != null)
            {
                foreach (DataRow r in dt.Rows)
                {
                    ListItem lit = new ListItem();
                    lit.Text = "类别:" + r["ClassName"].ToString() + "--" + "主题:" + r["Title"].ToString();
                    lit.Value = r["TID"].ToString();//选项所属主题TID
                    this.vote_CTName.Items.Add(lit);//新增加
                    this.classnameedit.Items.Add(lit);//修改

                }
                dt.Clear();
            }

            #endregion
            string type = Request.QueryString["type"];
            switch (type)
            {
                case "edit":
                    setItemEdit();
                    break;
                case "delone":
                    setItemDel();
                    break;
            }
        }
    }
    /// <summary>
    /// 分页
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="PageIndex"></param>
    /// code by chenzhaohui

    protected void PageNavigator1_PageChange(object sender, int PageIndex)
    {
        VoteItemManage(PageIndex);//管理页面分页查询
    }

    /// <summary>
    /// 修改初始页面信息
    /// </summary>
    ///code by chenzhaohui

    void setItemEdit()
    {
        int IID = int.Parse(Request.QueryString["ID"]);
        DataTable dt = sur.Str_ItemSql(IID);
        if (dt.Rows.Count > 0)
        {
            this.classnameedit.Text = dt.Rows[0]["TID"].ToString();
            this.itemnameedit.Text = dt.Rows[0]["ItemName"].ToString();
            this.valueedit.Text = dt.Rows[0]["ItemValue"].ToString();
            this.itemmodele.Text = dt.Rows[0]["ItemMode"].ToString();
            this.picsurl.Text = dt.Rows[0]["PicSrc"].ToString();
            this.discoloredit.Text = dt.Rows[0]["DisColor"].ToString();
            this.pointqe.Text = dt.Rows[0]["VoteCount"].ToString();
            this.discriptionitem.Value = dt.Rows[0]["ItemDetail"].ToString();
        }
        else
        {
            PageError("未知错误,异常错误", "setItem.aspx");
        }
    }

    /// <summary>
    /// 删除单个事件
    /// </summary>
    ///code by chenzhaohui 

    void setItemDel()
    {
        int IID = int.Parse(Request.QueryString["ID"]);
        if (IID <= 0)
        {
            PageError("错误的参数传递!", "");
        }
        else
        {
            if (sur.Del_Str_ItemSql(IID) == 0)
            {
                PageError("意外错误：未知错误", "");
            }
            else
            {
                rd.SaveUserAdminLogs(1, 1, UserNum, "删除投票选项", "删除成功");
                PageRight("删除成功。", "setItem.aspx");
            }
        }
    }

    /// <summary>
    /// 管理列表页
    /// </summary>
    /// <param name="PageIndex"></param>
    /// code by chenzhaohui

    protected void VoteItemManage(int PageIndex)//显示投票选项管理页面
    {
        #region 查询条件判断语句

        string KeyWord = Foosun.Common.Input.Filter(this.KeyWord.Text.Trim());//关键字
        string type = this.DdlKwdType.SelectedValue;//选择类型
        int i = 0, j = 0;
        int num = PAGESIZE;//从参数设置里取得每页显示记录的条数
        DataTable dt = null;
        if (KeyWord != "" && KeyWord != null)//如果关键字不为空，则执行下面的条件语句
        {
            switch (type)
            {
                case "choose":
                    break;
                case "title":
                    DataTable dt1 = new DataTable();
                    dt1 = sur.SQl_title(KeyWord);
                    if (dt1 != null)
                    {
                        if (dt1.Rows.Count > 0)
                        {
                            for (int l = 0; l < dt1.Rows.Count; l++)
                            {
                                int tid = int.Parse(dt1.Rows[l]["TID"].ToString());
                                SQLConditionInfo st = new SQLConditionInfo("@TID", "%" + tid + "%");
                                dt = Foosun.CMS.Pagination.GetPage("manage_survey_setItem_1_aspx", PageIndex, num, out i, out j, st);
                            }
                        }
                        else
                        {
                            dt = Foosun.CMS.Pagination.GetPage("manage_survey_setItem_2_aspx", PageIndex, num, out i, out j, null);
                        }
                    }
                    else
                    {
                        dt = Foosun.CMS.Pagination.GetPage("manage_survey_setItem_2_aspx", PageIndex, num, out i, out j, null);
                    }
                    break;
                case "ItemNamee":
                    SQLConditionInfo st1 = new SQLConditionInfo("@ItemName", "%" + KeyWord + "%");
                    dt = Foosun.CMS.Pagination.GetPage("manage_survey_setItem_3_aspx", PageIndex, num, out i, out j, st1);
                    break;
                case "ItemValuee":
                    SQLConditionInfo st2 = new SQLConditionInfo("@ItemValue", "%" + KeyWord + "%");
                    dt = Foosun.CMS.Pagination.GetPage("manage_survey_setItem_4_aspx", PageIndex, num, out i, out j, st2);
                    break;
                case "PicSrcc":
                    SQLConditionInfo st3 = new SQLConditionInfo("@PicSrc", "%" + KeyWord + "%");
                    dt = Foosun.CMS.Pagination.GetPage("manage_survey_setItem_5_aspx", PageIndex, num, out i, out j, st3);
                    break;
                case "DisColorr":
                    SQLConditionInfo st4 = new SQLConditionInfo("@DisColor", "%" + KeyWord + "%");
                    dt = Foosun.CMS.Pagination.GetPage("manage_survey_setItem_6_aspx", PageIndex, num, out i, out j, st4);
                    break;
                case "VoteCountt":
                    SQLConditionInfo st5 = new SQLConditionInfo("@VoteCount", "%" + KeyWord + "%");
                    dt = Foosun.CMS.Pagination.GetPage("manage_survey_setItem_7_aspx", PageIndex, num, out i, out j, st5);
                    break;
                case "ItemDetaill":
                    SQLConditionInfo st6 = new SQLConditionInfo("@ItemDetail", "%" + KeyWord + "%");
                    dt = Foosun.CMS.Pagination.GetPage("manage_survey_setItem_8_aspx", PageIndex, num, out i, out j, st6);
                    break;
            }
        }
        else
        {
            dt = Foosun.CMS.Pagination.GetPage("manage_survey_setItem_2_aspx", PageIndex, num, out i, out j, null);
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
                dt.Columns.Add("title", typeof(String));//主题名
                dt.Columns.Add("ItemModel", typeof(String));//选项模式
                dt.Columns.Add("oPerate", typeof(String));//操作

                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    int idt = int.Parse(dt.Rows[k]["tid"].ToString());
                    int idi = int.Parse(dt.Rows[k]["iid"].ToString());
                    int ItemModee = int.Parse(dt.Rows[k]["ItemMode"].ToString());//选项模式
                    try
                    {
                        //从主题表中取主题名
                        string VoteTitleName = sur.VoteTitle_Sql_1(idt);//取得主题的值

                        dt.Rows[k]["title"] = VoteTitleName;//将查找出来的值传给调查主题栏
                    }
                    catch { }
                    switch (ItemModee)
                    {
                        case 1:
                            dt.Rows[k]["ItemModel"] = "文字描述模式";
                            break;
                        case 2:
                            dt.Rows[k]["ItemModel"] = "自主填写模式";
                            break;
                        case 3:
                            dt.Rows[k]["ItemModel"] = "图片模式";
                            break;
                        default:
                            dt.Rows[k]["ItemModel"] = "文字描述模式";
                            break;
                    }
                    dt.Rows[k]["ItemName"] = "<a href='setItem.aspx?type=edit&id=" + idi + "' class='list_link'>" + dt.Rows[k]["ItemName"].ToString() + "</a>";
                    dt.Rows[k]["oPerate"] = "<a href=\"setItem.aspx?type=edit&id=" + idi + "\"  class=\"list_link\" title=\"修改此项\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/edit.gif\" border=\"0\" alt=\"修改此项\" /></a><a href=\"setItem.aspx?type=delone&id=" + idi + "\"  class=\"list_link\" title=\"删除此项\" onclick=\"{if(confirm('确认删除吗？')){return true;}return false;}\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/del.gif\" border=\"0\" alt=\"删除此项\" /></a><input type='checkbox' name='vote_checkbox' id='vote_checkbox' value=\"" + idi + "\"/>";
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
    //查询操作
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        VoteItemManage(1);
    }

    /// <summary>
    /// 新增加
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// code by chenzhaohui

    protected void Saveitem_ServerClick(object sender, EventArgs e)
    {
        if (Page.IsValid)//判断页面是否通过验证
        {
            //取得添加中的表单信息
            string Str_vote_CTName = this.vote_CTName.SelectedValue;//调查类别,主题
            string Str_ItemName = Foosun.Common.Input.Filter(this.ItemName.Text);//选项描述
            string Str_ItemValue = this.ItemValue.Text;//项目符号
            string Str_ItemMode = this.ItemMode.Text;//选项模式
            string Str_PicSrc = this.PicSrc.Text;//图片位置
            string Str_DisColor = Foosun.Common.Input.Filter(this.DisColor.Text);//颜色
            string Str_VoteCount = Foosun.Common.Input.Filter(this.VoteCount.Text);//票数
            string Str_ItemDetail = this.ItemDetail.Value;//详细说明

            //检查是否有已经存在的类别名称
            if (sur.Str_CheckSql(Str_ItemName) != 0)
            {
                PageError("对不起，该选项已经存在", "setItem.aspx");
            }
            //检查选项名不能为空
            if (Str_ItemName == null || Str_ItemName == string.Empty)
            {
                PageError("对不起，选项名不能为空!", "setItem.aspx");
            }
            //检查选项票数是否为数字型
            if (!Foosun.Common.Input.IsInteger(Str_VoteCount))
            {
                PageError("对不起，选项票数只能为数字型，请返回继续添加", "setItem.aspx");
            }
            //向数据库中写入添加的类别信息

            //载入数据-刷新页面
            if (sur.Add_Str_InSql(Str_vote_CTName, Str_ItemName, Str_ItemValue, Str_ItemMode, Str_PicSrc, Str_DisColor, Str_VoteCount, Str_ItemDetail, SiteID) != 0)
            {
                rd.SaveUserAdminLogs(1, 1, UserNum, "问卷调查系统新增选项", "问卷调查系统新增选项成功");
                PageRight("问卷调查系统新增选项成功", "setItem.aspx");
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
                if (sur.Del_Vote_Sql(CheckboxArray[i]) == 0)
                {
                    PageError("删除数据失败,请与管理联系!", "");
                    break;
                }
            }
            rd.SaveUserAdminLogs(1, 1, UserNum, "问卷调查系统批量删除选项", "删除数据成功");
            PageRight("删除数据成功,请返回继续操作!", "setItem.aspx");
        }
    }


    /// <summary>
    /// 删除全部
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    ///code by chenzhaohui

    protected void DelAll_Click(object sender, EventArgs e)
    {
        if (sur.Del_Vote_Sql_1() == 0)
        {
            PageError("意外错误：未知错误", "");
        }
        else
        {
            rd.SaveUserAdminLogs(1, 1, UserNum, "问卷调查系统删除选项", "删除数据成功");
            PageRight("删除全部成功。", "setItem.aspx");
        }
    }

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// code by chenzhaohui

    protected void Editclick_ServerClick(object sender, EventArgs e)
    {
        if (Page.IsValid)//判断页面是否通过验证
        {
            int IID = int.Parse(Request.QueryString["ID"]);
            //取得修改中的表单信息
            string Str_classnameedit = Foosun.Common.Input.Filter(this.classnameedit.Text.Trim());
            string Str_itemnameedit = Foosun.Common.Input.Filter(this.itemnameedit.Text.Trim());
            string Str_valueedit = Foosun.Common.Input.Filter(this.valueedit.Text.Trim());
            string Str_itemmodele = Foosun.Common.Input.Filter(this.itemmodele.Text.Trim());
            string Str_picsurl = Foosun.Common.Input.Filter(this.picsurl.Text.Trim());
            string Str_discoloredit = Foosun.Common.Input.Filter(this.discoloredit.Text.Trim());
            string Str_pointqe = Foosun.Common.Input.Filter(this.pointqe.Text.Trim());
            string Str_discriptionitem = Foosun.Common.Input.Filter(this.discriptionitem.Value.Trim());

            if (Str_itemnameedit == null || Str_itemnameedit == string.Empty)
            {
                PageError("对不起，主题名称不能为空，请返回继续添加", "setItem.aspx");
            }
            //检查选项数是否为数字型
            if (!Foosun.Common.Input.IsInteger(Str_pointqe))
            {
                PageError("对不起，选项票数只能为数字型，请返回继续添加", "setItem.aspx");
            }
            //刷新页面
            if (sur.Update_Str_UpdateSql(Str_classnameedit, Str_itemnameedit, Str_valueedit, Str_itemmodele, Str_picsurl, Str_discoloredit, Str_pointqe, Str_discriptionitem, IID) != 0)
            {
                rd.SaveUserAdminLogs(1, 1, UserNum, "问卷调查系统修改选项", "修改成功");
                PageRight("修改成功", "setItem.aspx");
            }
            else
            {
                PageError("意外错误：未知错误", "shortcut_list.aspx");
            }

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
            nos = nos + "<td class='navi_link'>当前没有满足条件的选项！</td>";
            nos = nos + "</tr>";
            nos = nos + "</table>";
        }
        return nos;
    }
}
