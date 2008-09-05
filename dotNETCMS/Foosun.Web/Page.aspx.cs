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
using Foosun.Publish;
using Foosun.Model;
using System.Text.RegularExpressions;

namespace Foosun.Web
{
    public partial class Page : Foosun.Web.UI.BasePage
    {
        protected string dimm = Foosun.Config.UIConfig.dirDumm;
        protected string TempletDir = Foosun.Config.UIConfig.dirTemplet;
        public static string gInstallDir = "{$InstallDir}";
        public static string gTempletDir = "{$TempletDir}";
        protected void Page_Load(object sender, EventArgs e)
        {
            //string saveNewsPath = string.Empty;
            string ClassID = Request.QueryString["id"];
            string gChID = Request.QueryString["ChID"];
            int ChID = 0;
            if (gChID != null && gChID != string.Empty)
            {
                if (Foosun.Common.Input.IsInteger(gChID.ToString()))
                {
                    ChID = int.Parse(gChID.ToString());
                }
            }
            string TempletPath = string.Empty;
            string SiteRootPath = Foosun.Common.ServerInfo.GetRootPath() + "\\";
            string strTempletDir = TempletDir;
            string finallyContent = string.Empty;
            if (dimm.Trim() != string.Empty) { dimm = "/" + dimm; }
            CommonData.Initialize();
            if (ChID != 0)
            {
                PubCHClassInfo CHinfo = CommonData.GetCHClassById(int.Parse(ClassID));
                if (CHinfo != null)
                {
                    TempletPath = CHinfo.Templet;
                    TempletPath = TempletPath.Replace("/", "\\").ToLower();
                    TempletPath = TempletPath.ToLower().Replace("{@dirtemplet}", strTempletDir);
                    TempletPath = SiteRootPath.Trim('\\') + TempletPath;
                    Template newschTemplate = new Template(TempletPath, TempType.ChClass);
                    newschTemplate.CHNewsID = 0;
                    newschTemplate.ChID = ChID;
                    newschTemplate.CHClassID = int.Parse(ClassID);
                    newschTemplate.GetHTML();
                    newschTemplate.ReplaceLabels();
                    finallyContent = newschTemplate.FinallyContent;
                    finallyContent = finallyContent.Replace("{#Page_Title}", CHinfo.classCName);
                    finallyContent = finallyContent.Replace("{#Page_MetaKey}", CHinfo.MetaKeywords);
                    finallyContent = finallyContent.Replace("{#Page_MetaDesc}", CHinfo.MetaDescript);
                    finallyContent = finallyContent.Replace("{#Page_Content}", CHinfo.PageContent);
                    Channel cdr = new Channel();
                    IDataReader dr = cdr.getChInfoMenu(ChID);
                    string ChName = string.Empty;
                    if (dr.Read())
                    {
                        ChName = dr["channelName"].ToString();
                    }
                    dr.Close();
                    finallyContent = finallyContent.Replace("{#Page_Navi}", "<a href=\"" + dimm + "/default.aspx\">首页</a> >> <a href=\"default.aspx?ChID=" + ChID.ToString() + "\">" + ChName + "</a> >> " + CHinfo.classCName);
                }
            }
            else
            {
                PubClassInfo info = CommonData.GetClassById(ClassID);
                if (info != null)
                {
                    TempletPath = info.ClassTemplet;
                    TempletPath = TempletPath.Replace("/", "\\").ToLower();
                    TempletPath = TempletPath.ToLower().Replace("{@dirtemplet}", strTempletDir);
                    TempletPath = SiteRootPath.Trim('\\') + TempletPath;
                    Template newsTemplate = new Template(TempletPath, TempType.Class);
                    newsTemplate.NewsID = null;
                    newsTemplate.ClassID = ClassID;
                    newsTemplate.GetHTML();
                    newsTemplate.ReplaceLabels();
                    finallyContent = newsTemplate.FinallyContent;
                    finallyContent = finallyContent.Replace("{#Page_Title}", info.ClassCName);
                    finallyContent = finallyContent.Replace("{#Page_MetaKey}", info.MetaKeywords);
                    finallyContent = finallyContent.Replace("{#Page_MetaDesc}", info.MetaDescript);
                    finallyContent = finallyContent.Replace("{#Page_Content}", info.PageContent);
                    finallyContent = finallyContent.Replace("{#Page_Navi}", "<a href=\"" + dimm + "/\">首页</a> >> " + info.ClassCName);
                }
            }

            if (Regex.Match(finallyContent, @"\</head\>[\s\S]*\<body", RegexOptions.IgnoreCase | RegexOptions.Compiled).Success)
            {
                finallyContent = Regex.Replace(finallyContent, "<body", getjs() + "<body", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            }
            else
            {
                finallyContent = getjs() + finallyContent;
            }
            finallyContent = (finallyContent.Replace(gInstallDir, Foosun.Publish.CommonData.getUrl())).Replace(gTempletDir, TempletDir);
            Response.Write(finallyContent);
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
