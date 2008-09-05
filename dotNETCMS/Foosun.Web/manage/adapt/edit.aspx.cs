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
    public partial class edit : Foosun.Web.UI.ManagePage
    {
        public edit()
        {
            Authority_Code = "Q030";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack) return;
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
                if (_appInfo == null)
                {
                    Response.Redirect("adapt.aspx");
                }
                else
                {
                    this.TextBoxAppID.Text = _appInfo.AppID;
                    this.Api_Url.Text = _appInfo.AppUrl;
                }
            }
        }

        protected void submit_Click(object sender, EventArgs e)
        {
            if (!this.IsValid)
                return;
            string appid = Request.QueryString["appid"];
            Foosun.Config.API.APIConfig config = Foosun.Config.API.APIConfigs.GetConfig();
            Foosun.Config.API.ApplicationInfo _appInfo = null;
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
                    _appInfo.AppID = this.TextBoxAppID.Text;
                    _appInfo.AppUrl = this.Api_Url.Text;
                    Foosun.Config.API.APIConfigs.SaveConfig(config);
                }
            }

             
            
            Response.Redirect("adapt.aspx");
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string appid = Request.QueryString["appid"];
            Foosun.Config.API.APIConfig config = Foosun.Config.API.APIConfigs.GetConfig();
            if (config.ApplicationList == null)
                config.ApplicationList = new Foosun.Config.API.ApplicaitonCollection();
            foreach (Foosun.Config.API.ApplicationInfo _appInfo in config.ApplicationList)
            {
                if (_appInfo.AppID.ToLower() == this.TextBoxAppID.Text.ToLower() && appid.ToLower() != this.TextBoxAppID.Text.ToLower())
                {
                    args.IsValid = false;
                    break;
                }

            }
        }
    }
}
