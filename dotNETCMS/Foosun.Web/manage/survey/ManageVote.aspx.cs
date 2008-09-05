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

public partial class manage_survey_ManageVote : Foosun.Web.UI.ManagePage
{
    public manage_survey_ManageVote()
    {
        Authority_Code = "S005";
    }
    rootPublic rd = new rootPublic();
    Survey sur = new Survey();
    protected void Page_Load(object sender, EventArgs e)
    {

        #region 分页调用函数
        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_PageChange);
        #endregion

        if (!IsPostBack)
        {

            copyright.InnerHtml = CopyRight;//获取版权信息
            if (SiteID == "0")
            {
                param_id.InnerHtml = "<a href=\"setParam.aspx\" class=\"list_link\">系统参数设置</a>&nbsp;┊&nbsp;";
            }
            VoteManage(1);  //初始分页数据

            string type = Request.QueryString["type"];
            if (type == "delone")
            {
                DelOneClick();
            }

        }
    }

    /// <summary>
    /// 分页
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="PageIndex"></param>
    /// Code By ChenZhaoHui

    protected void PageNavigator1_PageChange(object sender, int PageIndex)
    {
        VoteManage(PageIndex);//管理页面分页查询
    }
    #region 管理页列表
    protected void VoteManage(int PageIndex)//显示投票选项管理页面
    {
        int i, j;
        DataTable dt = Foosun.CMS.Pagination.GetPage(this.GetType().Name, PageIndex, PAGESIZE, out i, out j, null);
        this.PageNavigator1.PageCount = j;
        this.PageNavigator1.PageIndex = PageIndex;
        this.PageNavigator1.RecordCount = i;

        if (dt != null)//判断如果dt里面没有内容，将不会显示
        {
            if (dt.Rows.Count > 0)
            {
                //添加列
                dt.Columns.Add("item", typeof(String));//选项
                dt.Columns.Add("title", typeof(String));//主题
                dt.Columns.Add("usernum", typeof(String));//会员编号
                dt.Columns.Add("oPerate", typeof(String));//操作

                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    int idr = int.Parse(dt.Rows[k]["rid"].ToString());
                    int idt = int.Parse(dt.Rows[k]["tid"].ToString());
                    int idi = int.Parse(dt.Rows[k]["iid"].ToString());
                    string Str_UserNum = dt.Rows[k]["UserNumber"].ToString();
                    try
                    {
                        #region 取值
                        string VoteTitleName = sur.VoteTitle_Sql(idt);//取得调查主题的值
                        string VoteItemName = sur.VoteItem_Sql(idi);//取得调查选项的值
                        string VoteUserName = sur.VoteUser_Sql(Str_UserNum);//取得会员编号的值
                        //string VoteTitleName = idt.ToString();//取得调查主题的值
                        //string VoteItemName = idi.ToString();//取得调查选项的值
                        //string VoteUserName = Str_UserNum;//取得会员编号的值
                        #endregion
                        #region 赋值
                        dt.Rows[k]["title"] = VoteTitleName;//将查找出来的值传给调查主题栏
                        dt.Rows[k]["item"] = VoteItemName;//将查找出来的值传给调查选项栏
                        dt.Rows[k]["usernum"] = VoteUserName;//将查找出来的值传给会员编号栏
                        #endregion
                   }
                   catch { }

                    dt.Rows[k]["oPerate"] = "<a href=\"ManageVote.aspx?type=delone&id=" + idr + "\" title=\"删除此项\" class=\"list_link\" onclick=\"{if(confirm('确认删除吗？')){return true;}return false;}\"><img src=\"../../sysImages/folder/del.gif\" border=\"0\" alt=\"删除此项\" /></a><input type='checkbox' name='vote_checkbox' id='vote_checkbox' value=\"" + idr + "\"/>";
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
    #endregion

    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// Code By ChenZhaoHui

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
                if (sur.Delete(CheckboxArray[i]) == 0)
                {
                    rd.SaveUserAdminLogs(1, 1, UserNum, "投票管理", "删除数据失败");
                    PageError("删除数据失败,请与管理联系!", "ManageVote.aspx");
                    break;
                }
            }
            rd.SaveUserAdminLogs(1, 1, UserNum, "投票管理", "删除数据成功");
            PageRight("删除数据成功,请返回继续操作!", "ManageVote.aspx");
        }
    }

    /// <summary>
    /// 全部删除
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// Code By ChenZhaoHui

    protected void DelAll_Click(object sender, EventArgs e)
    {
        if (sur.Delete_1() == 0)
        {
            rd.SaveUserAdminLogs(1, 1, UserNum, "投票管理", "参数错误");
            PageError("意外错误：未知错误", "");
        }
        else
        {
            rd.SaveUserAdminLogs(1, 1, UserNum, "投票管理", "删除数据成功");
            PageRight("删除全部成功。", "ManageVote.aspx");
        }
    }

    /// <summary>
    /// 删除单个事件
    /// </summary>
    /// Code By ChenZhaoHui

    void DelOneClick()
    {
        int RID = int.Parse(Request.QueryString["ID"]);
        if (RID <= 0)
        {
            PageError("错误的参数传递!", "");
        }
        else
        {
            if (sur.Delete_2(RID) == 0)
            {
                rd.SaveUserAdminLogs(1, 1, UserNum, "投票管理", "参数错误");
                PageError("意外错误：未知错误", "");
            }
            else
            {
                rd.SaveUserAdminLogs(1, 1, UserNum, "投票管理", "删除数据成功");
                PageRight("删除成功。", "ManageVote.aspx");
            }
        }
    }

    /// <summary>
    /// 无内容提示页
    /// </summary>
    /// <returns></returns>
    /// Code By ChenZhaoHui

    string Show_NoContent()
    {

        string nos = "<table border=0 width='98%' align=center cellpadding=5 cellspacing=1 class='table'>";
        nos = nos + "<tr class='TR_BG_list'>";
        nos = nos + "<td class='navi_link'>当前没有满足条件的投票内容！</td>";
        nos = nos + "</tr>";
        nos = nos + "</table>";
        return nos;
    }
}
