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

public partial class manage_label_FreeLabel_List : Foosun.Web.UI.ManagePage
{
    public manage_label_FreeLabel_List()
    {
        Authority_Code = "T008";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        #region Ajax
        if (Request.Form["Option"] != null && Request.Form["Option"].Equals("DeleteFreeLabel"))
        {
            this.Authority_Code = "T009";
            this.CheckAdminAuthority();
            int id = int.Parse(Request.Form["ID"]);
            Foosun.CMS.FreeLabel fb = new Foosun.CMS.FreeLabel();
            try
            {
                if (fb.Delete(id))
                    Response.Write("1%成功删除该自由标签!");
                else
                    Response.Write("0%删除失败,该自由标签不存在!");
            }
            catch (Exception ex)
            {
                Response.Write("0%操作失败,失败信息:" + ex.Message);
            }
            Response.End();
        }
        #endregion Ajax

        this.PageNavigator1.OnPageChange += new PageChangeHandler(this.PageNavigator1_OnPageChange);
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
        DataTable tb = Foosun.CMS.Pagination.GetPage(this.GetType().Name, PageIndex, PAGESIZE, out nRCount, out nPCount);
        this.PageNavigator1.PageCount = nPCount;
        this.PageNavigator1.RecordCount = nRCount;
        this.PageNavigator1.PageIndex = PageIndex;
        this.RptFreeLabel.DataSource = tb;
        this.RptFreeLabel.DataBind();
    }
}
