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

namespace Foosun.Web.manage.Sys
{
    public partial class CustomForm_HtmlCode : Foosun.Web.UI.ManagePage
    {
        protected int flag = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.CacheControl = "no-cache"; //清除缓存
            if (!IsPostBack)  //判断页面是否重载
            {
                if (Request.QueryString["id"] != null && Request.QueryString["id"].Trim() != string.Empty)
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    Foosun.CMS.CustomForm cf = new Foosun.CMS.CustomForm();
                    string s = cf.GetHtmlCode(id);
                    this.TxtCode.Text = s;
                    string ps = s.Replace("<input type=\"submit\" value=\" 提交 \" />","");
                    ps = ps.Replace("<input type=\"reset\" value=\" 重写 \" />","");
                    this.TD_Code.InnerHtml = ps;
                }
                else
                {
                    PageError("参数不正确", "");
                }
                if (Request.QueryString["op"] != null && Request.QueryString["op"] == "1")
                    flag = 1;
                DataBind();
            }
        }
    }
}