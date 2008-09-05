//===========================================================
//==     (c)2007 Foosun Inc. by dotNETCMS 1.0              ==
//==             Forum:bbs.foosun.net                      ==
//==            website:www.foosun.net                     ==
//==     Address:NO.109 HuiMin ST.,Chengdu ,China          ==
//==         TEL:86-28-85098980/66026180                   ==
//==         qq:655071,MSN:ikoolls@gmail.com               ==
//==               Code By JiangDong                       ==
//===========================================================
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class manage_news_Site_List : Foosun.Web.UI.ManagePage
{
    private Foosun.CMS.Site site;
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("../Publish/psf.aspx");
    }

    private void PageNavigator1_PageChage(object sender, int PageIndex)
    {
        DataListBind(PageIndex);
    }

    private void DataListBind(int PageIndex)
    {
        int nRCount, nPCount;
        DataTable tb = Foosun.CMS.Pagination.GetPage(this.GetType().Name, PageIndex, 20, out nRCount, out nPCount);
        this.PageNavigator1.PageCount = nPCount;
        this.PageNavigator1.PageIndex = PageIndex;
        this.PageNavigator1.RecordCount = nRCount;
        this.RptSite.DataSource = tb;
        this.RptSite.DataBind();
    }
    protected void RptSite_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Label lbl1 = (Label)e.Item.FindControl("LblIsURL");
            if (lbl1 != null)
            {
                if (lbl1.Text.Equals("1"))
                    lbl1.Text = "外部";
                else if (lbl1.Text.Equals("0"))
                    lbl1.Text = "系统";
            }
            Label lbl2 = (Label)e.Item.FindControl("LblShowNaviTF");
            if (lbl2 != null)
            {
                if (lbl2.Text.Equals("1"))
                    lbl2.Text = "显示";
                else if (lbl2.Text.Equals("0"))
                    lbl2.Text = "隐藏";
            }
            Label lbl3 = (Label)e.Item.FindControl("LblContrTF");
            if (lbl3 != null)
            {
                if (lbl3.Text.Equals("1"))
                    lbl3.Text = "┆稿";
                else
                    lbl3.Text = "";
            }
            Label lbl4 = (Label)e.Item.FindControl("LblDomain");
            if (lbl4 != null)
            {
                if (!lbl4.Text.Equals(""))
                    lbl4.Text = "┆域";
                else
                    lbl4.Text = "";
            }
        }
    }
}
