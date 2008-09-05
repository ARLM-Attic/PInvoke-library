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

public partial class manage_news_news_review : Foosun.Web.UI.ManagePage
{
    Foosun.CMS.ContentManage rd = new Foosun.CMS.ContentManage();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            string gType = "";
            if (Request.QueryString["type"] != null && Request.QueryString["type"] != string.Empty)
            {
                gType = Request.QueryString["type"].ToString();
            }
            else
            {
                gType = "news";
            }
            string URLS = rd.getnewsReview(Request.QueryString["ID"].ToString(), gType);
            string url = URLS.Replace("//", "/");
            Response.Redirect(url);
        }
    }
}
