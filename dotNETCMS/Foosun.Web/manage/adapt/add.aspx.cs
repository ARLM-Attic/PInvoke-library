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
    public partial class add : Foosun.Web.UI.ManagePage
    {
        public add()
        {
            Authority_Code = "Q030";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack)
                return;
            //Foosun.Config.API.APIConfig config = Foosun.Config.API.APIConfigs.GetConfig();
            
        }

        protected void submit_Click(object sender, EventArgs e)
        {
            if (!this.IsValid)
                return;
            Foosun.Config.API.APIConfig config = Foosun.Config.API.APIConfigs.GetConfig();
            if (config.ApplicationList == null)
                config.ApplicationList = new Foosun.Config.API.ApplicaitonCollection();
            
            Foosun.Config.API.ApplicationInfo appInfo = new Foosun.Config.API.ApplicationInfo();
            appInfo.AppID = this.TextBoxAppID.Text;
            appInfo.AppUrl = this.Api_Url.Text;
            config.ApplicationList.Add(appInfo);
            Foosun.Config.API.APIConfigs.SaveConfig(config);
            Response.Redirect("adapt.aspx");
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            Foosun.Config.API.APIConfig config = Foosun.Config.API.APIConfigs.GetConfig();
            if (config.ApplicationList == null)
                config.ApplicationList = new Foosun.Config.API.ApplicaitonCollection();
            foreach (Foosun.Config.API.ApplicationInfo _appInfo in config.ApplicationList)
            {
                if (_appInfo.AppID.ToLower() == this.TextBoxAppID.Text.ToLower())
                {
                    args.IsValid = false;
                    break;
                }

            }
        }
    }
}
