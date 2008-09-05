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
using Foosun.Model;

public partial class manage_js_JS_GetCode : Foosun.Web.UI.ManagePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache"; //清除缓存
        if (!IsPostBack)  //判断页面是否重载
        {
            if (Request.QueryString["JSID"] == null || Request.QueryString["JSID"].Trim().Equals(""))
            {
                PageError("参数传递错误", "JS_List.aspx");
            }
            int id = int.Parse(Request.QueryString["JSID"]);
            Foosun.CMS.NewsJS nj = new Foosun.CMS.NewsJS();
            Foosun.Model.NewsJSInfo info = nj.GetSingle(id);
            this.CodePath.Value = "<script language=\"javascript\" type=\"text/javascript\" src=\""+ Foosun.Common.ServerInfo.GetRootURI(Request) + info.jssavepath +"/"+ info.jsfilename +".js\"></script>";
        }
    }
}