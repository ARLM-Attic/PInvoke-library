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
using Foosun.Model;

public partial class user_manage_logs : Foosun.Web.UI.ManagePage
{
    public user_manage_logs()
    {
        Authority_Code = "Q022";
    }
    user rd = new user();
    protected void Page_Load(object sender, EventArgs e)
    {
        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_PageChange);
        if (!IsPostBack)
        {
            
            copyright.InnerHtml = CopyRight;            //获取版权信息
            Response.CacheControl = "no-cache";                        //设置页面无缓存
            string Type = Request.QueryString["Type"];  //取得操作类型
            switch (Type)
            {
                case "del":            //锁定管理员
                    this.Authority_Code = "Q023";
                    this.CheckAdminAuthority();
                    int ID = int.Parse(Foosun.Common.Input.Filter(Request.QueryString["ID"]));  //取得需要操作的管理员ID
                    Del(ID);
                    break;
                default:
                    break;
            }
            StartLoad(1);
        }
    }
    //-----------------------------------------------------分页开始------------------------------
    protected void PageNavigator1_PageChange(object sender, int PageIndex)
    {
        StartLoad(PageIndex);
    }
    protected void StartLoad(int PageIndex)
    {
        int i, j;
        DataTable dt = null;
        SQLConditionInfo st = new SQLConditionInfo("@UserNum", Foosun.Global.Current.UserNum);
        dt = Foosun.CMS.Pagination.GetPage("user_manage_logs_1_aspx", PageIndex, 20, out i, out j, st);
        this.PageNavigator1.PageCount = j;
        this.PageNavigator1.PageIndex = PageIndex;
        this.PageNavigator1.RecordCount = i;
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                //----------------------------------------添加列------------------------------------------------
                dt.Columns.Add("op", typeof(string));
                //----------------------------------------添加列结束--------------------------------------------
                for (int k = 0; dt.Rows.Count > k; k++)
                {
                    dt.Rows[k]["op"] = "<a href=\"javascript:edit('" + dt.Rows[k]["id"].ToString() + "');\" class='list_link'>修改</a> | <a href=\"javascript:del('" + dt.Rows[k]["id"].ToString() + "');\" class='list_link'>删除</a> ";
                }
            }
        }
        DataList1.DataSource = dt;                              //设置datalist数据源
        DataList1.DataBind();                                   //绑定数据源
    }
    //-----------------------------------------------------分页结束-----------------------------------------------
    protected void Del(int ID)
    {
        rd.UserLogsDels(ID);
        PageRight("删除日历成功。", "logs.aspx");
    }
}
