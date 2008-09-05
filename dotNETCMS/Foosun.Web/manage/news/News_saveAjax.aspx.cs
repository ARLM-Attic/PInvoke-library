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

public partial class manage_news_News_saveAjax : Foosun.Web.UI.ManagePage
{
    ContentManage rd = new ContentManage();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        //string _Content = Request["content"];
        //if (_Content.Trim() != "")
        //{ 
        //    //保存临时新闻
        //  string dl = rd.saveAjaxContent(_Content.ToString());
        //}
    }
}
