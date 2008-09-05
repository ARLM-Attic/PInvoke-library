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
    public partial class CustomForm_JS : Foosun.Web.UI.ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.CacheControl = "no-cache"; //清除缓存
            if (!IsPostBack)  //判断页面是否重载
            {
                if (Request.QueryString["ID"] == null || Request.QueryString["ID"].Trim().Equals(""))
                {
                    PageError("参数传递错误", "");
                }
                int id = int.Parse(Request.QueryString["ID"]);
                this.CodePath.Value = "<script language=\"javascript\" charset=\"utf-8\" type=\"text/javascript\" src=\"/customform/CustomFormJS.aspx?CustomFormId="+ id +"\"></script>";
            }
        }
    }
}