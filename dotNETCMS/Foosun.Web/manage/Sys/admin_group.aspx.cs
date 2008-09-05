///************************************************************************************************************
///**********管理员组管理,Code By DengXi***********************************************************************
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

public partial class manage_Sys_admin_group : Foosun.Web.UI.ManagePage
{
    public manage_Sys_admin_group()
    {
        Authority_Code = "Q016";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_PageChange);
        if (!IsPostBack)
        {
            
            copyright.InnerHtml = CopyRight;                   //获取版权信息
            Response.CacheControl = "no-cache";                        //设置页面无缓存
            StartLoad(1);
        }
        string Type = Request.QueryString["Type"];  //取得操作类型
        string ID = Request.QueryString["ID"];  //取得需要操作的管理员ID
        switch (Type)
        {
            case "Del":             //删除管理员
                this.Authority_Code = "Q018";
                this.CheckAdminAuthority();
                Del(Foosun.Common.Input.checkID(ID));
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 分页
    /// </summary>
    /// <returns>分页</returns>
    ///  Code By DengXi    
    
    protected void PageNavigator1_PageChange(object sender, int PageIndex)
    {
        StartLoad(PageIndex);
    }
    protected void StartLoad(int PageIndex)
    {
        int i, j;
        SQLConditionInfo st = new SQLConditionInfo("@SiteID", SiteID);
        DataTable dt = Foosun.CMS.Pagination.GetPage(this.GetType().Name, PageIndex, 20, out i, out j, st);

        this.PageNavigator1.PageCount = j;
        this.PageNavigator1.PageIndex = PageIndex;
        this.PageNavigator1.RecordCount = i;
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                //----------------------------------------添加列------------------------------------------------
                dt.Columns.Add("Op", typeof(string));
                //----------------------------------------添加列结束--------------------------------------------
                for (int k = 0; dt.Rows.Count > k; k++)
                {
                    dt.Rows[k]["Op"] = "<a href=\"javascript:Update('" + dt.Rows[k]["adminGroupNumber"].ToString() + "');\" class='list_link'><img src=\"../../sysImages/folder/re.gif\" border=\"0\" alt=\"修改\" /></a><a href=\"javascript:Del('" + dt.Rows[k]["adminGroupNumber"].ToString() + "');\" class='list_link'><img src=\"../../sysImages/folder/del.gif\" border=\"0\" alt=\"删除\" /></a>";
                }
            }
            DataList1.DataSource = dt;                              //设置datalist数据源
            DataList1.DataBind();                                   //绑定数据源
            dt.Clear();
            dt.Dispose();
        }
    }


    /// <summary>
    /// 删除管理员
    /// </summary>
    /// <param name="ID">管理员编号</param>
    /// <returns>删除管理员</returns>
    /// Code By DengXi
    
    protected void Del(string ID)
    {
        Foosun.CMS.AdminGroup agc = new Foosun.CMS.AdminGroup();
        agc.Del(ID);
        PageRight("删除管理员组成功!", "");
    }
}
