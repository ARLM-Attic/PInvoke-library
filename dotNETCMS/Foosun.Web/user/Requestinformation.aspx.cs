//=====================================================================
//==                  (C)2007 Foosun Inc.By doNetCMS1.0              ==
//==                     Forum:bbs.foosun.net                        ==
//==                     WebSite:www.foosun.net                      ==
//==                 Address:No.109 HuiMin ST,.ChengDu,China         ==
//==                   Tel:86-28-85098980/66026180                   ==
//==                   QQ:655071,MSN:ikoolls@gmail.com               ==
//==                   Email:Service@foosun.cn                       ==
//==                      Code By WangZhenjiang                      ==
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

public partial class user_Requestinformation : Foosun.Web.UI.UserPage
{
    public string quname = null;
    Friend fir = new Friend();
    rootPublic rd = new rootPublic();
    protected void Page_Load(object sender, EventArgs e)
    {
        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_PageChange);
        if (!IsPostBack)
        {
            
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            StartLoad(1);
        }      
    }



    protected void PageNavigator1_PageChange(object sender, int PageIndex)
    {
        StartLoad(PageIndex);
    }

    protected void StartLoad(int PageIndex)
    {
        int i, j;
        DataTable dt = Foosun.CMS.Pagination.GetPage(this.GetType().Name, PageIndex, 20, out i, out j, null);
        this.PageNavigator1.PageCount = j;
        this.PageNavigator1.PageIndex = PageIndex;
        this.PageNavigator1.RecordCount = i;
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                dt.Columns.Add("QName", typeof(string));
                dt.Columns.Add("ops", typeof(string));
                for (int k = 0; dt.Rows.Count > k; k++)
                {
                    dt.Rows[k]["QName"] = "<a title=\"点击查看用户信息\" href=\"ShowUser.aspx?uid=" + dt.Rows[k]["qUsername"].ToString() + "\" target=\"_blank\" class=\"list_link\">" + dt.Rows[k]["qUsername"].ToString() + "</a>";
                    dt.Rows[k]["ops"] = "<a href=\"javascript:checkfriend(" + dt.Rows[k]["id"].ToString() + ")\" class=\"list_link\">验证</a>";
                }
             }
             DataList1.Visible = true;
        }
        else
        {
            this.PageNavigator1.Visible = false;
        }
        DataList1.DataSource = dt;                              //设置datalist数据源
        DataList1.DataBind();
        DataList1.Dispose();
    }
}
