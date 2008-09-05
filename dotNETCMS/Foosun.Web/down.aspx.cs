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

public partial class down : Foosun.Web.UI.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
            getDownInfo();
    }

    /// <summary>
    /// 获取新闻附件下载信息
    /// </summary>
    protected void getDownInfo()
    {
        string id = Foosun.Common.Input.Filter(Request.QueryString["id"]);
        if (id != null && id != "")
        {
            Foosun.CMS.ContentManage news = new Foosun.CMS.ContentManage();
            string DownAdress = news.getNewsAccessory(int.Parse(id));
            DownAdress = DownAdress.ToLower().Replace("{@dirfile}", Foosun.Config.UIConfig.dirFile);
            DownAdress = DownAdress.ToLower().Replace("{@dirtemplet}", Foosun.Config.UIConfig.dirTemplet);
            DownAdress = DownAdress.ToLower().Replace("{@dirdumm}", Foosun.Config.UIConfig.dirDumm);
            DownAdress = DownAdress.ToLower().Replace("{@dirfile}", Foosun.Config.UIConfig.dirFile);
            DownAdress = DownAdress.ToLower().Replace("{@dirhtml}", Foosun.Config.UIConfig.dirHtml);
            DownAdress = DownAdress.ToLower().Replace("{@dirsite}", Foosun.Config.UIConfig.dirSite);
            DownAdress = DownAdress.ToLower().Replace("{@diruser}", Foosun.Config.UIConfig.dirUser);
            //DownAdress = DownAdress.ToLower().Replace("{@dirfile}", Foosun.Config.UIConfig.dirFile);
            Response.Write("<script langauge=\"javascript\">self.location='" + DownAdress + "';</script>");
            Response.End();
        }
        else
        {
            Err();
        }
    }

    protected void Err()
    {
        Response.Write("<script language=\"javascript\">alert('参数传递错误!');history.back();</script>");
        Response.End();
    }
}
