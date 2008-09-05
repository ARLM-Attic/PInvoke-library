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

public partial class manage_publish_error_GetError : Foosun.Web.UI.ManagePage
{
    public string filename = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)                                               //判断页面是否重载
        {
                                                 //判断用户是否登录
            filename = "../../../logs/public/" + Foosun.Config.UIConfig.Logfilename + "_" + DateTime.Now.Month + DateTime.Now.Day + ".log";
            string filepath = Server.MapPath(filename);
            ShowFileContet(filepath);
        }
    }

    protected void ShowFileContet(string filepath)
    {
        Foosun.CMS.Templet.Templet tpClass = new Foosun.CMS.Templet.Templet();
        try
        {
            FileContent.Text = tpClass.showFileContet(filepath);
        }
        catch
        {
            Response.Write("<span style=\"padding:20px;\">今天(" + DateTime.Now.ToShortDateString() + ")发布没有产生错误!</span>");
            Response.End();
        }
    }
}
