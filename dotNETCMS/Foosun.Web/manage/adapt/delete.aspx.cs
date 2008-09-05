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

namespace Foosun.Web.manage.adapt
{
    public partial class delete : Foosun.Web.UI.ManagePage
    {
        public delete()
        {
            Authority_Code = "Q030";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            string appid = Request.QueryString["appid"];
            Foosun.Config.API.APIConfig config = Foosun.Config.API.APIConfigs.GetConfig();
            Foosun.Config.API.ApplicationInfo _appInfo=null;
            if (config.ApplicationList != null)
            {
                foreach (Foosun.Config.API.ApplicationInfo appInfo in config.ApplicationList)
                {
                    if (appInfo.AppID == appid)
                    {
                        _appInfo = appInfo;
                        break;
                    }
                }
                if (_appInfo != null)
                {
                    config.ApplicationList.Remove(_appInfo);
                    Foosun.Config.API.APIConfigs.SaveConfig(config);
                }
            }
            Response.Redirect("adapt.aspx");
        }
    }
}
