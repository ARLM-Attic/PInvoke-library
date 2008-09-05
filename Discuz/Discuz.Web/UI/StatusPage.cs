using System;
using System.Collections.Generic;
using System.Text;
using Discuz.Config;
using Discuz.Common;
using Discuz.Forum;

namespace Discuz.Web.UI
{
    public class StatusPage : PageBase
    {
        public StatusPage()
        {
            this.Load += new EventHandler(Status_Load);
        }

        void Status_Load(object sender, EventArgs e)
        {
            APIConfigInfo apiInfo = APIConfigs.GetConfig();
            if (!apiInfo.Enable)
                return;
            ApplicationInfo appInfo = null;
            ApplicationInfoCollection appcollection = apiInfo.AppCollection;
            foreach (ApplicationInfo newapp in appcollection)
            {
                if (newapp.APIKey == DNTRequest.GetString("api_key"))
                {
                    appInfo = newapp;
                }
            }
            if (appInfo == null)
                return;


            string next = DNTRequest.GetString("next");
            string reurl = string.Format("{0}{1}user_status={2}{3}", appInfo.CallbackUrl, appInfo.CallbackUrl.IndexOf("?") > 0 ? "&" : "?", userid > 0 ? "1" : "0", next == string.Empty ? next : "next=" + next);
            Response.Redirect(reurl);

        }
    }
}
