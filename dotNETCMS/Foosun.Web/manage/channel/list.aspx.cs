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

public partial class manage_channel_list : Foosun.Web.UI.ManagePage
{
    public manage_channel_list()
    {
        Authority_Code = "C034";
    }
    UserMisc rd = new UserMisc();
    Channel md = new Channel();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("../Publish/psf.aspx");
    }

    /// <summary>
    /// 缩定频道
    /// </summary>
    /// <param name="ChID"></param>
    /// <param name="isLock"></param>
    protected void ModelStat(string ChID, string isLock)
    {
        md.ModelStat(int.Parse(ChID), int.Parse(isLock));
        if (isLock == "1")
        {
            PageRight("频道已设置为锁定", "list.aspx", true);
        }
        else
        {
            PageRight("频道已设置为开放", "list.aspx", true);
        }
    }

    protected void delModel(string ChID)
    {
        this.Authority_Code = "";
        this.CheckAdminAuthority();
        if (md.getSysCord(int.Parse(ChID)) == 1)
        {
            PageError("系统频道不能删除", "list.aspx", true);
        }
        else
        {
            md.delModel(int.Parse(ChID));
            Response.Write("<script>alert('删除成功');window.top.location.href=\"../index.aspx?urls=channel/list.aspx\"</script>");
            Response.End();
        }
    }

    /// <summary>
    /// PageNavigator1_PageChange 的摘要说明
    /// 分页加载函数
    /// </summary>
    protected void PageNavigator1_PageChange(object sender, int PageIndex)
    {
        StartLoad(PageIndex);
    }

    /// <summary>
    /// PageNavigator1_PageChange 的摘要说明
    /// 分页加载列表函数
    /// </summary>
    protected void StartLoad(int PageIndex)
    {
        int i, j;
        DataTable dt = null;
        dt = Foosun.CMS.Pagination.GetPage(this.GetType().Name, PageIndex, 20, out i, out j, null);
        this.PageNavigator1.PageCount = j;
        this.PageNavigator1.PageIndex = PageIndex;
        this.PageNavigator1.RecordCount = i;
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                //----------------------------------------添加列------------------------------------------------
                dt.Columns.Add("op", typeof(string));
                dt.Columns.Add("islocks", typeof(string));
                dt.Columns.Add("systf", typeof(string));
                //----------------------------------------添加列结束--------------------------------------------
                //定义变量
                string getIsSysTF;
                for (int k = 0; dt.Rows.Count > k; k++)
                {
                    if (!this.CheckAuthority())
                    {
                        dt.Rows.RemoveAt(k);
                    }
                    else
                    {
                        if (dt.Rows[k]["issys"].ToString() == "1")
                        {
                            getIsSysTF = "&nbsp;<span style=\"color:#999999\" title=\"系统频道不能删除\">删除</span>";
                            dt.Rows[k]["systf"] = "<span class=\"tbie\">系统</span>";
                        }
                        else
                        {
                            getIsSysTF = "&nbsp;<a  onClick=\"{if(confirm('确定要删除吗？')){return true;}return false;}\" title=\"点击删除\" href=\"list.aspx?ChID=" + dt.Rows[k]["ID"].ToString() + "&action=del\" class=\"list_link\">删除</a>";
                            dt.Rows[k]["systf"] = "自定义";
                        }
                        string lockStr = "";
                        if (dt.Rows[k]["islock"].ToString() == "1")
                        {
                            lockStr = "<a href=\"list.aspx?ChID=" + dt.Rows[k]["ID"].ToString() + "&isLock=0\"  onClick=\"{if(confirm('确定要开放此频道吗？')){return true;}return false;}\" class=\"reshow\" title=\"点击开放\">已禁用</a>";
                        }
                        else
                        {
                            lockStr = "<a href=\"list.aspx?ChID=" + dt.Rows[k]["ID"].ToString() + "&isLock=1\"  onClick=\"{if(confirm('确定要禁用此频道吗？')){return true;}return false;}\" class=\"list_link\" title=\"点击锁定\">已开放</a>";
                        }
                        //┊&nbsp;<a href=\"list.aspx?ChID=" + dt.Rows[k]["ID"].ToString() + "&action=copy\" class=\"list_link\">复制</a>&nbsp;
                        dt.Rows[k]["op"] = "<a href=\"value_add.aspx?ChID=" + dt.Rows[k]["ID"].ToString() + "\" class=\"list_link\">增加字段</a>&nbsp;┊&nbsp;<a href=\"value.aspx?ChID=" + dt.Rows[k]["ID"].ToString() + "\" class=\"list_link\">字段管理</a>&nbsp;┊&nbsp;<a href=\"channel_add.aspx?ChID=" + dt.Rows[k]["ID"].ToString() + "\" class=\"list_link\">修改</a>&nbsp;┊&nbsp;" + lockStr + "&nbsp;┊" + getIsSysTF + "";
                        if (dt.Rows[k]["islock"].ToString() == "0") { dt.Rows[k]["islocks"] = "正常"; }
                        else { dt.Rows[k]["islocks"] = "<span class=\"tbie\">锁定</span>"; }
                    }
                }
            }
        }
        Channlist.DataSource = dt;                              //设置datalist数据源
        Channlist.DataBind();                                   //绑定数据源
    }


    protected void DataList1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

    }
}
