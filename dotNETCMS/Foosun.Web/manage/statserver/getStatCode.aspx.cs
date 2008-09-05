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

public partial class Manage_statserver_getStatCode : Foosun.Web.UI.ManagePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";
        if (!Page.IsPostBack)
        {
            getCode();
        }
    }

    protected void getCode()
    {
        this.copyright.InnerHtml = CopyRight;
        CodePath.Value = "<script src='http://pw.cnzz.com/c.php?id=" + Foosun.Common.Public.readparamConfig("SiteID", "Cnzz") + "&l=2' " +
                         "language='JavaScript' charset='gb2312'></script>";
    }
}
