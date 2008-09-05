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

public partial class manage_js_JS_Files : Foosun.Web.UI.ManagePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_PageChange);
        Response.CacheControl = "no-cache";//设置页面无缓存
        if (Request.Form["Option"] != null && Request.Form["Option"].Equals("RemoveJS") && Request.Form["ID"] != null)
        {
            Foosun.CMS.NewsJS nj = new Foosun.CMS.NewsJS();
            try
            {
                int id = int.Parse(Request.Form["id"]);
                nj.RemoveNews(id);
                Response.Write("1%成功移除JS对该新闻的调用!");
            }
            catch(Exception ex)
            {
                Response.Write("0%" + ex.Message);
            }
            Response.End();
            return;
        }

        //批量删除
        if (Request.Form["Option"] != null && Request.Form["Option"].Equals("RemoveAllJS") && Request.Form["idList"] != null)
        {
            Foosun.CMS.NewsJS nj = new Foosun.CMS.NewsJS();
            string[] idList = Request.Form["idList"].Split(',');

            foreach (string s in idList)
            { 
                if(!string.IsNullOrEmpty(s))
                    nj.RemoveNews(Convert.ToInt32(s));
            }
            Response.Write("1%成功移除JS对该新闻的调用!");
            Response.End();
            return;
        }

        if (!IsPostBack)
        {
            if (this.Request.QueryString["JSID"] == null)
                PageError("缺少必要的参数!","");
            this.HidJsID.Value = int.Parse(Request.QueryString["JSID"]).ToString();
            DataListBind(1);
        }
    }
    protected void PageNavigator1_PageChange(object sender, int PageIndex)
    {
        DataListBind(PageIndex);
    }

    /// <summary>
    /// 列表
    /// </summary>
    /// <param name="PageIndex"></param>
    /// code by chenzhaohui

    protected void DataListBind(int PageIndex)
    {
        int id = int.Parse(this.HidJsID.Value);
        int RcdCount = 0, PgCount = 0;
        Foosun.CMS.NewsJS nj = new Foosun.CMS.NewsJS();
        DataTable tb = nj.GetJSFilePage(PageIndex, PAGESIZE, out RcdCount, out PgCount, id);
        this.PageNavigator1.RecordCount = RcdCount;
        this.PageNavigator1.PageCount = PgCount;
        this.PageNavigator1.PageIndex = PageIndex;
        this.RptData.DataSource = tb;
        this.RptData.DataBind();
    }
}
