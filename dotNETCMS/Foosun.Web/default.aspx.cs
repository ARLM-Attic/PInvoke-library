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
using System.Text.RegularExpressions;

namespace Foosun.Web
{
    public partial class _default : Foosun.Web.UI.BasePage
    {
        protected string SiteRootPath = Foosun.Common.ServerInfo.GetRootPath();
        protected string dimm = Foosun.Config.UIConfig.dirDumm;
        protected string TempletDir = Foosun.Config.UIConfig.dirTemplet;
        public static string gInstallDir = "{$InstallDir}";
        public static string gTempletDir = "{$TempletDir}";
        protected void Page_Load(object sender, EventArgs e)
        {
            string gChID = Request.QueryString["ChID"];
            int ChID = 0;
            if (gChID != null && gChID != string.Empty)
            {
                if (Foosun.Common.Input.IsInteger(gChID.ToString()))
                {
                    ChID = int.Parse(gChID.ToString());
                }
            }
            Foosun.Publish.CommonData.Initialize();
            string indexname = "index.html";
            string TempletPath = Foosun.Common.Public.readparamConfig("IndexTemplet");
            if (ChID != 0)
            {
                TempletPath = "/" + Foosun.Config.UIConfig.dirTemplet + "/" + Foosun.Common.Public.readCHparamConfig("channeltemplet", ChID);
            }
            TempletPath = TempletPath.Replace("/", "\\");
            TempletPath = TempletPath.ToLower().Replace("{@dirtemplet}", TempletDir);
            indexname = Foosun.Common.Public.readparamConfig("IndexFileName");
            Foosun.Publish.Template indexTemp = null;
            if (ChID != 0)
            {
                indexname = Foosun.Common.Public.readCHparamConfig("channelindexname", ChID);
                indexTemp = new Foosun.Publish.Template(SiteRootPath.Trim('\\') + TempletPath, Foosun.Publish.TempType.ChIndex);
            }
            else
            {
                indexTemp = new Foosun.Publish.Template(SiteRootPath.Trim('\\') + TempletPath, Foosun.Publish.TempType.Index);
            }
            indexTemp.GetHTML();
            indexTemp.ReplaceLabels();
            string getContent = indexTemp.FinallyContent;

            if (Regex.Match(getContent, @"\</head\>[\s\S]*\<body", RegexOptions.IgnoreCase | RegexOptions.Compiled).Success)
            {
                getContent = Regex.Replace(getContent, "<body", getjs() + "<body", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            }
            else
            {
                getContent = getjs() + getContent;
            }
            getContent = (getContent.Replace(gInstallDir, Foosun.Publish.CommonData.getUrl())).Replace(gTempletDir, TempletDir);
            Response.Write(getContent);
        }

        protected string getjs()
        {
            string getajaxJS = "<script language=\"javascript\" type=\"text/javascript\" src=\"" + Foosun.Publish.CommonData.getUrl() + "/configuration/js/Prototype.js\"></script>\r\n";
            getajaxJS += "<script language=\"javascript\" type=\"text/javascript\" src=\"" + Foosun.Publish.CommonData.getUrl() + "/configuration/js/jspublic.js\"></script>\r\n";
            getajaxJS += "<!--Created by dotNETCMS v1.0 For Foosun Inc. at " + DateTime.Now + "-->\r\n";
            return getajaxJS;
        }

    }
}
