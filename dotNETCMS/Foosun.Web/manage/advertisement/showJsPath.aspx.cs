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

namespace Foosun.Web.manage.advertisement
{
    public partial class showJsPath : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string adsID = Request.QueryString["adsID"];
            string str_JsCode = "<script language=\"javascript\" src=\"" + Foosun.Publish.CommonData.getUrl() + "/jsfiles/ads/show.aspx?adsID=" + adsID + "\"></script>";
            CodePath.Value = str_JsCode;
        }
    }
}
