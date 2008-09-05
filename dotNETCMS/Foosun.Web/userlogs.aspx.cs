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

public partial class userlogs :Foosun.Web.UI.BasePage
{
    UserMisc rd = new UserMisc();
    protected void Page_Load(object sender, EventArgs e)
    {
        string Getid = Request.QueryString["id"];
        if (!Foosun.Common.Input.IsInteger(Getid))
        {
            Response.Write("错误的参数");
            Response.End();
        }
        else
        {
            string divstr = "<div><a href=\"javascript:;\" onclick=\"closediv('ClistID')\">关闭</a></div>";
            Response.Write(divstr + rd.GetUserLogs(int.Parse(Getid)));
            Response.End();
        }
    }
}
