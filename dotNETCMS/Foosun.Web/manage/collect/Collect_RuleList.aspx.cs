//===========================================================
//==     (c)2007 Foosun Inc. by dotNETCMS 1.0              ==
//==             Forum:bbs.foosun.net                      ==
//==            website:www.foosun.net                     ==
//==     Address:NO.109 HuiMin ST.,Chengdu ,China          ==
//==         TEL:86-28-85098980/66026180                   ==
//==         TEL:86-28-85098980/66026180                   ==
//==         qq:655071,MSN:ikoolls@gmail.com               ==
//==               Code By JiangDong                       ==
//===========================================================
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

public partial class manage_collect_Collect_RuleList : Foosun.Web.UI.ManagePage
{
    private Foosun.CMS.Collect.Collect cl;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form["Option"] != null && !Request.Form["Option"].Trim().Equals("")
            && Request.Form["ID"] != null && !Request.Form["ID"].Trim().Equals(""))
        {
            try
            {
                int id = int.Parse(Request.Form["ID"]);
                if (Request.Form["Option"].Equals("DeleteRule"))
                {
                    cl = new Foosun.CMS.Collect.Collect();
                    cl.RuleDelete(id);
                }
                Response.Write("1%成功删除一个规则");
            }
            catch (Exception ex)
            {
                Response.Write("0%" + ex.Message);
            }
            Response.End();
            return;
        }
        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_OnPageChange);
        if (!Page.IsPostBack)
        {
            ListDataBind(1);
        }
    }
    protected void PageNavigator1_OnPageChange(object sender, int PageIndex)
    {
        ListDataBind(PageIndex);
    }
    private void ListDataBind(int PageIndex)
    {
        int nRCount, nPCount;
        cl = new Foosun.CMS.Collect.Collect();
        this.RptRule.DataSource = cl.GetRulePage(PageIndex, PAGESIZE, out nRCount, out nPCount);
        this.RptRule.DataBind();
        this.PageNavigator1.PageCount = nPCount;
        this.PageNavigator1.RecordCount = nRCount;
        this.PageNavigator1.PageIndex = PageIndex;
    }
}
