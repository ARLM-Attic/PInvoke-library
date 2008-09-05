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

public partial class manage_news_unnews : Foosun.Web.UI.ManagePage
{
    public manage_news_unnews()
    {
        Authority_Code = "CE02";
    }
    ContentManage nes = new ContentManage();
    protected void Page_Load(object sender, EventArgs e)
    {
        this.PageNavigator1.OnPageChange+=new PageChangeHandler(PageNavigator1_OnPageChange);
        if (!IsPostBack)
            GetunNewsPage(1);
    }

    private void PageNavigator1_OnPageChange(object sender,int pageindex)
    {
        GetunNewsPage(pageindex);
    }

    private void GetunNewsPage(int pageindex)
    {
        int i = 0, j = 0;
        DataTable DT;
        DT = nes.GetPages(pageindex, 20, out i, out j, null);
        this.PageNavigator1.RecordCount = i;
        this.PageNavigator1.PageCount = j;
        this.PageNavigator1.PageIndex = pageindex;
        RptunNews.DataSource = DT;
        RptunNews.DataBind();
        DT.Dispose();
    }
}


//distinct