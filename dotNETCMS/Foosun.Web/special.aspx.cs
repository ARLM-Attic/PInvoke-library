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
    public partial class special : Foosun.Web.UI.BasePage
    {
        protected static string dimm = Foosun.Config.UIConfig.dirDumm;
        protected static string TempletDir = Foosun.Config.UIConfig.dirTemplet;
        public static string gInstallDir = "{$InstallDir}";
        public static string gTempletDir = "{$TempletDir}";
        protected void Page_Load(object sender, EventArgs e)
        {
            string SpecialID = Request.QueryString["id"];
            string gPage = Request.QueryString["Page"];
            int strPage = 1;
            string gChID = Request.QueryString["ChID"];
            int ChID = 0;
            if (gChID != null && gChID != string.Empty)
            {
                if (Foosun.Common.Input.IsInteger(gChID.ToString()))
                {
                    ChID = int.Parse(gChID.ToString());
                }
            }
            if (dimm.Trim() != string.Empty) { dimm = "/" + dimm; }
            if (gPage != string.Empty && gPage != null && Foosun.Common.Input.IsInteger(gPage))
            {
                strPage = int.Parse(gPage);
            }
            CommonData.Initialize();
            string getContent = string.Empty;
            if (ChID != 0)
            {
                PubCHSpecialInfo CHinfo = CommonData.GetCHSpecial(int.Parse(SpecialID));
                if (CHinfo != null)
                {
                    getContent = getContentRuslt(ChID,CHinfo.templet, SpecialID, strPage, 0);
                }
            }
            else
            {
                PubSpecialInfo info = CommonData.GetSpecial(SpecialID);
                if (info != null)
                {
                    if (info.isDelPoint == 0)
                    {
                        getContent = getContentRuslt(ChID, info.Templet, SpecialID, strPage, 0);
                    }
                    else
                    {
                        if (Foosun.Global.Current.IsTimeout())
                        {
                            string Url = Foosun.Common.Input.URLEncode(Request.Url.ToString());
                            Response.Redirect(Foosun.Config.UIConfig.dirUser + "/login.aspx?reurl=1&urls=" + Url + "", true);
                        }
                        else
                        {
                            string UserNum = Foosun.Global.Current.UserNum;
                            string IP = Request.ServerVariables["REMOTE_ADDR"];
                            int PointType = info.isDelPoint;
                            //新闻的参数
                            int iPoint = info.iPoint;
                            int Gpoint = info.Gpoint;
                            string GroupNumber = info.GroupNumber;


                            int iuserPoint = 0;
                            int Guserpoint = 0;
                            string userGroupNumber = string.Empty;
                            DynamicTrans dyNews = new DynamicTrans();
                            IDataReader rd = dyNews.getUserInfo(UserNum);
                            if (rd.Read())
                            {
                                iuserPoint = int.Parse(rd["iPoint"].ToString());
                                Guserpoint = int.Parse(rd["Gpoint"].ToString());
                                userGroupNumber = rd["UserGroupNumber"].ToString();
                            }

                            switch (PointType)
                            {
                                case 1:
                                    if (dyNews.getUserNote(UserNum, SpecialID, 2))
                                    {
                                        getContent = getContentRuslt(ChID, info.Templet, SpecialID, strPage, 1);
                                    }
                                    else
                                    {
                                        if (Guserpoint < Gpoint || getGroup(userGroupNumber, GroupNumber) == false)
                                        {
                                            PageError("G币不足,需要G币:" + Gpoint + "<li>或者您所在的组不能浏览</li>", "");
                                        }
                                        else
                                        {
                                            if (dyNews.UpdateHistory(2, SpecialID, 0, Gpoint, UserNum, IP) == 1)
                                            {
                                                getContent = getContentRuslt(ChID, info.Templet, SpecialID, strPage, 1);
                                            }
                                        }
                                    }
                                    break;
                                case 2:
                                    if (dyNews.getUserNote(UserNum, SpecialID, 2))
                                    {
                                        getContent = getContentRuslt(ChID, info.Templet, SpecialID, strPage, 1);
                                    }
                                    else
                                    {
                                        if (iuserPoint < iPoint || getGroup(userGroupNumber, GroupNumber) == false)
                                        {
                                            PageError("积分不足,需要积分:" + iPoint + "<li>或者您所在的组不能浏览</li>", "");
                                        }
                                        else
                                        {
                                            if (dyNews.UpdateHistory(2, SpecialID, iPoint, 0, UserNum, IP) == 1)
                                            {
                                                getContent = getContentRuslt(ChID, info.Templet, SpecialID, strPage, 1);
                                            }
                                        }
                                    }
                                    break;
                                case 3:
                                    if (dyNews.getUserNote(UserNum, SpecialID, 2))
                                    {
                                        getContent = getContentRuslt(ChID, info.Templet, SpecialID, strPage, 1);
                                    }
                                    else
                                    {
                                        if (iuserPoint < iPoint || Guserpoint < Gpoint || getGroup(userGroupNumber, GroupNumber) == false)
                                        {
                                            PageError("您的积分和G币不足,需要积分:" + iPoint + ",G币:" + Gpoint + "<li>或者您所在的组不能浏览</li>", "");
                                        }
                                        else
                                        {
                                            if (dyNews.UpdateHistory(2, SpecialID, iPoint, Gpoint, UserNum, IP) == 1)
                                            {
                                                getContent = getContentRuslt(ChID, info.Templet, SpecialID, strPage, 1);
                                            }
                                        }
                                    }
                                    break;
                                case 4:
                                    if (Guserpoint < Gpoint || getGroup(userGroupNumber, GroupNumber) == false)
                                    {
                                        PageError("您的G币没达到:" + Gpoint + "<li>或者您所在的组不能浏览</li>", "");
                                    }
                                    else
                                    {
                                        getContent = getContentRuslt(ChID, info.Templet, SpecialID, strPage, 1);
                                    }
                                    break;
                                case 5:
                                    if (iuserPoint < iPoint || getGroup(userGroupNumber, GroupNumber) == false)
                                    {
                                        PageError("您的G币没达到:" + iPoint + "<li>或者您所在的组不能浏览</li>", "");
                                    }
                                    else
                                    {
                                        getContent = getContentRuslt(ChID, info.Templet, SpecialID, strPage, 1);
                                    }
                                    break;
                                case 6:
                                    if (iuserPoint < iPoint || Guserpoint < Gpoint || getGroup(userGroupNumber, GroupNumber) == false)
                                    {
                                        PageError("您的积分或G币不足,需要达到积分:" + iPoint + ",G币:" + Gpoint + "<li>或者您所在的组不能浏览</li>", "");
                                    }
                                    else
                                    {
                                        getContent = getContentRuslt(ChID, info.Templet, SpecialID, strPage, 1);
                                    }
                                    break;
                            }
                        }
                    }
                }
            }

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

        protected static string getContentRuslt(int ChID,string Templet, string SpecialID, int strPage,int isPop)
        {
            string TempletPath = string.Empty;
            string SiteRootPath = Foosun.Common.ServerInfo.GetRootPath();
            string strTempletDir = TempletDir;
            string gConenent = string.Empty;
            TempletPath = Templet;
            TempletPath = TempletPath.Replace("/", "\\");
            TempletPath = TempletPath.ToLower().Replace("{@dirtemplet}", strTempletDir);
            TempletPath = SiteRootPath.Trim('\\') + TempletPath;
            Template specialTemplate = null;
            if (ChID != 0)
            {
                specialTemplate = new Template(TempletPath, TempType.Chspecial);
                specialTemplate.CHSpecialID = int.Parse(SpecialID);
                specialTemplate.ChID = ChID;
            }
            else
            {
                specialTemplate = new Template(TempletPath, TempType.Special);
                specialTemplate.SpecialID = SpecialID;
            }
            specialTemplate.GetHTML();
            gConenent = replaceTemplate(specialTemplate, SpecialID, strPage, isPop);
            return gConenent;
        }

        protected bool getGroup(string uGroup, string nGroup)
        {
            return Foosun.Common.Public.CommgetGroup(uGroup, nGroup);
        }


        /// <summary>
        /// 处理模板
        /// </summary>
        /// <param name="tempRe">模板实例</param>
        public static string replaceTemplate(Template tempRe, string SpecialID, int strPage, int isPop)
        {
            tempRe.ReplaceLabels();
            string getPageContent = string.Empty;
            string getPageStr = string.Empty;
            bool getRight = false;
            string FinlContent = string.Empty;
            if (tempRe.MyTempType == TempType.Special || tempRe.MyTempType == TempType.Chspecial)
            {
                FinlContent = tempRe.FinallyContent;
                int pos1 = FinlContent.IndexOf("{Foosun:NewsLIST}");
                int pos2 = FinlContent.IndexOf("{/Foosun:NewsLIST}");
                if (pos2 > pos1 && pos1 > -1)
                {
                    #region 处理分页
                    string PageHead = FinlContent.Substring(0, pos1);
                    string PageEnd = FinlContent.Substring(pos2 + 18);
                    string PageMid = FinlContent.Substring(pos1 + 17, pos2 - pos1 - 17);
                    string pattern = @"\{\$FS\:P[01]\}\{Page\:\d\$[^\$]{0,6}\$\}";
                    Regex reg = new Regex(pattern, RegexOptions.Compiled);
                    Match match = reg.Match(PageMid);
                    if (match.Success)
                    {
                        if (Foosun.Config.verConfig.PublicType == "0" || tempRe.MyTempType == TempType.Chspecial)
                        {
                            string PageStr = match.Value;
                            int posPage = PageStr.IndexOf("}{Page:");

                            string postResult = PageStr.Substring(posPage + 7);
                            postResult = postResult.TrimEnd('}');
                            string[] postResultARR = postResult.Split('$');
                            string postResult_style = postResultARR[0];
                            string postResult_color = postResultARR[1];
                            string postResult_css = postResultARR[2];
                            string postResult_css1 = "";
                            if (postResult_css.Trim() != string.Empty)
                            {
                                postResult_css1 = " class=\"" + postResult_css + "\"";
                            }
                            string[] ArrayCon = reg.Split(PageMid);
                            int n = ArrayCon.Length;
                            if (ArrayCon[n - 1] == null || ArrayCon[n - 1].Trim() == string.Empty)
                                n--;
                            for (int i = 0; i < n; i++)
                            {
                                if (i == (strPage - 1))
                                {
                                    UltiPublish gpl = new UltiPublish(true);
                                    getPageStr = gpl.getPagelist(postResult_style, i, "", "", postResult_color, postResult_css1, n, SpecialID, "special", isPop);
                                    getPageContent = PageHead + ArrayCon[i] + getPageStr + PageEnd;
                                    getRight = true;
                                }
                                if (getRight)
                                {
                                    break;
                                }
                            }
                        }
                        FinlContent = getPageContent;
                    }
                    else
                    {
                        string p1js = "<span style=\"text-align:center;\" id=\"gPtypenowdiv" + DateTime.Now.ToShortDateString() + "\">加载中...</span>";
                        p1js += "<script language=\"javascript\" type=\"text/javascript\">";
                        p1js += "pubajax('" + CommonData.SiteDomain + "/configuration/system/public.aspx','NowStr=" + DateTime.Now.ToShortDateString() + "&ruleStr=1','gPtypenowdiv" + DateTime.Now.ToShortDateString() + "');";
                        p1js += "</script>";
                        FinlContent = tempRe.FinallyContent.Replace("{Foosun:NewsLIST}", "").Replace("{/Foosun:NewsLIST}", "").Replace("{$FS:P1}", p1js);
                    }
                    #endregion
                }
            }
            return FinlContent;
        }

        protected static string getjs()
        {
            string getajaxJS = "<script language=\"javascript\" type=\"text/javascript\" src=\"" + Foosun.Publish.CommonData.getUrl() + "/configuration/js/Prototype.js\"></script>\r\n";
            getajaxJS += "<script language=\"javascript\" type=\"text/javascript\" src=\"" + Foosun.Publish.CommonData.getUrl() + "/configuration/js/jspublic.js\"></script>\r\n";
            getajaxJS += "<!--Created by dotNETCMS v1.0 For Foosun Inc. at " + DateTime.Now + "-->\r\n";
            return getajaxJS;
        }

    }
}