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

public partial class manage_publish_logs : Foosun.Web.UI.ManagePage
{
    public manage_publish_logs()
    {
        Authority_Code = "P013";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)                                               //判断页面是否重载
        {
                                                 //判断用户是否登录
            Response.Redirect("error/GetError.aspx");
        }
    }
}
