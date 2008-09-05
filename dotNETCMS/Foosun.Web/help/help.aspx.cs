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
using Foosun.CMS.Common;

public partial class help_help : Foosun.Web.UI.BasePage
{
    Help help = new Help();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";
        string HelpID = Request.QueryString["HelpID"];
        if (HelpID != string.Empty && HelpID != null)
        {
            DataTable dt = help.getHelpID1(Foosun.Common.Input.Filter(Request.QueryString["HelpID"].ToString()));
            if (dt != null && dt.Rows.Count > 0)
            {
                title.InnerHtml = "<font color=red>" + dt.Rows[0]["TitleCN"].ToString() + "</font>";
                content.InnerHtml = dt.Rows[0]["ContentCN"].ToString();
                helpid.InnerHtml = dt.Rows[0]["HelpID"].ToString();
            }
        }
        else
        {
            PageError("找不到参数", "");
        }
    }
}
