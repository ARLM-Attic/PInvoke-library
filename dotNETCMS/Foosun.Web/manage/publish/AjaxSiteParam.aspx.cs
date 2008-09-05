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

namespace Foosun.Web.manage.publish
{
    public partial class AjaxSiteParam : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request.QueryString["Type"];
            string userPublishID = Request.QueryString["userPublishID"];
            if (type != null && type.Equals("ShowMessage"))
            {
                Response.Clear();
                string message = Foosun.Publish.UltiPublish.showMessages(Convert.ToInt32(userPublishID));
                Response.ContentType = "text/xml";
                Response.Write("<message>" + message + "</message>");
                Response.End();
            }
        }
    }
}
