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

public partial class manage_publish_siteTask : Foosun.Web.UI.ManagePage
{
    rootPublic logs = new rootPublic();
    Psframe pl = new Psframe();
    protected void Page_Load(object sender, EventArgs e)
    {
        #region
        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_PageChange);
        #endregion
        if (!IsPostBack)
        {
            PageError("此版本没有此功能", "");
            Response.CacheControl = "no-cache"; //设置页面无缓存
            copyright.InnerHtml = CopyRight;
            siteTask_Manage(1);
        }
        #region 删除单个
        string type = Request.QueryString["type"];
        if (type == "del")
        {
            DelOne_Task();
        }
        #endregion
    }
    #region page
    protected void PageNavigator1_PageChange(object sender, int PageIndex)
    {
        siteTask_Manage(PageIndex);
    }

    /// <summary>
    /// 显示任务管理页
    /// </summary>
    /// Code By ChenZhaohui

    protected void siteTask_Manage(int PageIndex)
    {
        int i, j;
        DataTable dt = Pagination.GetPage("manage_publish_siteTask_aspx", PageIndex, PAGESIZE, out i, out j, null);
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
                    //赋值
                    string taskID = dt.Rows[k]["taskID"].ToString();

                    #region
                    dt.Rows[k]["TaskName"] = "<a class=\"list_link\"  href=\"siteTask_edit.aspx?taskid=" + taskID + "\" title=\"点击查看详情或修改\">" + dt.Rows[k]["TaskName"].ToString() + "</a>";
                    #endregion
                    dt.Rows[k]["oPerate"] = "<a class=\"list_link\"  href=\"siteTask_edit.aspx?taskid=" + taskID + "\" title=\"点击查看详情或修改\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/edit.gif\" border=\"0\" alt=\"修改此项\" /></a><a class=\"list_link\" href=\"?type=del&taskid=" + taskID + "\" title=\"点击删除\" onclick=\"{if(confirm('确认删除吗？')){return true;}return false;}\">&nbsp;<img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/del.gif\" border=\"0\" alt=\"删除此项\" /></a><input type='checkbox' name='task_checkbox' id='task_checkbox'value=\"" + taskID + "\"/>";
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
        //否则提示没有内容
        else
        {
            NoContent.InnerHtml = Show_NoContent();
            this.PageNavigator1.Visible = false;
        }
    }
    #endregion

    /// <summary>
    /// 提示没记录显示
    /// </summary>
    /// <returns>取得无记录显示信息</returns>
    /// Code By ChenZhaohui

    #region noshow
    string Show_NoContent()
    {

        string nos = "<table border=0 width='98%' align=center cellpadding=5 cellspacing=1 class='table'>";
        nos = nos + "<tr class='TR_BG_list'>";
        nos = nos + "<td class='navi_link'>当前没有记录！</td>";
        nos = nos + "</tr>";
        nos = nos + "</table>";
        return nos;
    }
    #endregion

    /// <summary>
    /// 删除单个
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// code by chenzhaohui

    protected void DelOne_Task()
    {
        string taskid = Request.QueryString["taskID"];
        if (taskid == null || taskid == "" || taskid == string.Empty)
        {
            logs.SaveUserAdminLogs(1, 1, UserNum, "参数传递错误", "参数传递错误 ID:" + taskid + "");
            PageError("参数错误", "siteTask.aspx");
        }
        else
        {
            if (pl.DelOneTask(taskid) != 0)
            {
                logs.SaveUserAdminLogs(1, 1, UserNum, "删除单个计划任务", "删除单个计划任务成功 ID:" + taskid + "");
                PageRight("删除成功", "siteTask.aspx");
            }
            else
            {
                logs.SaveUserAdminLogs(1, 1, UserNum, "删除单个计划任务", "删除单个计划任务失败 ID:" + taskid + "");
                PageError("删除失败", "siteTask.aspx");
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
        string task_checkbox = Request.Form["task_checkbox"];
        if (task_checkbox == null || task_checkbox == String.Empty)
        {
            PageError("请先选择批量操作的内容!", "");
        }
        else
        {
            String[] CheckboxArray = task_checkbox.Split(',');
            task_checkbox = null;
            for (int i = 0; i < CheckboxArray.Length; i++)
            {
                pl.DelPTask(CheckboxArray[i]);
            }
            logs.SaveUserAdminLogs(1, 1, UserNum, "批量删除计划任务", "删除计划任务成功");
            PageRight("删除数据成功,请返回继续操作!", "siteTask.aspx");
        }
    }

    /// <summary>
    /// 删除全部
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// code by chenzhaohui

    protected void Delall_Click(object sender, EventArgs e)
    {
        if (pl.DelAllTask() != 0)
        {
            logs.SaveUserAdminLogs(1, 1, UserNum, "批量删除计划任务", "删除成功");
            PageRight("删除成功", "siteTask.aspx");
        }
        else
        {
            logs.SaveUserAdminLogs(1, 1, UserNum, "批量删除计划任务", "删除失败");
            PageError("删除失败", "siteTask.aspx");
        }
    }
}
