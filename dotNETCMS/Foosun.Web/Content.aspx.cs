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
using Foosun.CMS;
using Foosun.Publish;

public partial class news : Foosun.Web.UI.BasePage
{
    protected string dimm = Foosun.Config.UIConfig.dirDumm;
    protected string TempletDir = Foosun.Config.UIConfig.dirTemplet;
    public static string gInstallDir = "{$InstallDir}";
    public static string gTempletDir = "{$TempletDir}";
    protected void Page_Load(object sender, EventArgs e)
    {
        string NewsID = Request.QueryString["id"];
        string gPage = Request.QueryString["Page"];
        string gChID = Request.QueryString["ChID"];
        int strPage = 1;
        if (NewsID != null && NewsID != string.Empty)
        {
            if (dimm.Trim() != string.Empty) { dimm = "/" + dimm; }
            if (gPage != string.Empty && gPage != null && Foosun.Common.Input.IsInteger(gPage))
            {
                strPage = int.Parse(gPage);
            }
            int ChID = 0;
            string DTable = string.Empty;
            if (gChID != null && gChID != string.Empty)
            {
                if (Foosun.Common.Input.IsInteger(gChID.ToString()))
                {
                    ChID = int.Parse(gChID.ToString());
                }
                else
                {
                    PageError("错误的参数", "javascript:history.back();", true);
                }
                Channel CDR = new Channel();
                DTable = CDR.getChannelTable(ChID);
            }
            string FilyContent = string.Empty;
            DynamicTrans dyNews = new DynamicTrans();
            IDataReader rd = dyNews.GetNewsInfo(NewsID, 0, ChID, DTable);
            if (rd.Read())
            {
                Foosun.Publish.CommonData.Initialize();
                string TempletPath = rd["Templet"].ToString();
                TempletPath = TempletPath.ToLower().Replace("{@dirtemplet}", TempletDir);
                TempletPath = dimm + "/" + TempletPath;
                TempletPath = TempletPath.Replace("//", "/");
                TempletPath = Server.MapPath(TempletPath);
                string TempletContent = General.ReadHtml(TempletPath);
                if (rd["isDelPoint"].ToString() == "0")
                {
                    FilyContent = getContent(TempletPath, NewsID, ChID, strPage, 0);
                }
                else
                {
                    #region 判断是否扣点
                    //开始扣点
                    //检查用户是否登陆
                    if (Foosun.Global.Current.IsTimeout())
                    {
                        string Url = Foosun.Common.Input.URLEncode(Request.Url.ToString());
                        Response.Redirect(Foosun.Config.UIConfig.dirUser + "/login.aspx?reurl=1&urls=" + Url + "", true);
                    }
                    else
                    {
                        string UserNum = Foosun.Global.Current.UserNum;
                        string IP = Request.ServerVariables["REMOTE_ADDR"];
                        int PointType = int.Parse(rd["isDelPoint"].ToString());
                        //新闻的参数
                        int iPoint = int.Parse(rd["iPoint"].ToString());
                        int Gpoint = int.Parse(rd["Gpoint"].ToString());
                        string GroupNumber = rd["GroupNumber"].ToString();

                        //用户的参数
                        IDataReader ud = dyNews.getUserInfo(UserNum);
                        int iuserPoint = 0;
                        int Guserpoint = 0;
                        string userGroupNumber = string.Empty;
                        if (ud.Read())
                        {
                            iuserPoint = int.Parse(ud["iPoint"].ToString());
                            Guserpoint = int.Parse(ud["Gpoint"].ToString());
                            userGroupNumber = ud["UserGroupNumber"].ToString();
                        }
                        ud.Close();
                        #region 扣点
                        switch (PointType)
                        {
                            case 1:
                                if (dyNews.getUserNote(UserNum, NewsID,0))
                                {
                                    FilyContent = getContent(TempletPath, NewsID, ChID, strPage, 1);
                                }
                                else
                                {
                                    if (Guserpoint < Gpoint || getGroup(userGroupNumber, GroupNumber) == false)
                                    {
                                        PageError("G币不足,需要G币:" + Gpoint + "<li>或者您所在的组不能浏览</li>", "");
                                    }
                                    else
                                    {
                                        if (dyNews.UpdateHistory(0,NewsID, 0, Gpoint, UserNum, IP) == 1)
                                        {
                                            FilyContent = getContent(TempletPath, NewsID, ChID, strPage, 1);
                                        }
                                    }
                                }
                                break;
                            case 2:
                                if (dyNews.getUserNote(UserNum, NewsID,0))
                                {
                                    FilyContent = getContent(TempletPath, NewsID, ChID, strPage, 1);
                                }
                                else
                                {
                                    if (iuserPoint < iPoint || getGroup(userGroupNumber, GroupNumber) == false)
                                    {
                                        PageError("积分不足,需要积分:" + iPoint + "<li>或者您所在的组不能浏览</li>", "");
                                    }
                                    else
                                    {
                                        if (dyNews.UpdateHistory(0,NewsID, iPoint, 0, UserNum, IP) == 1)
                                        {
                                            FilyContent = getContent(TempletPath, NewsID, ChID, strPage, 1);
                                        }
                                    }
                                }
                                break;
                            case 3:
                                if (dyNews.getUserNote(UserNum, NewsID,0))
                                {
                                    FilyContent = getContent(TempletPath, NewsID, ChID, strPage, 1);
                                }
                                else
                                {
                                    if (iuserPoint < iPoint || Guserpoint < Gpoint || getGroup(userGroupNumber, GroupNumber) == false)
                                    {
                                        PageError("您的积分和G币不足,需要积分:" + iPoint + ",G币:" + Gpoint + "<li>或者您所在的组不能浏览</li>", "");
                                    }
                                    else
                                    {
                                        if (dyNews.UpdateHistory(0,NewsID, iPoint, Gpoint, UserNum, IP) == 1)
                                        {
                                            FilyContent = getContent(TempletPath, NewsID, ChID, strPage, 1);
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
                                    FilyContent = getContent(TempletPath, NewsID, ChID, strPage, 1);
                                }
                                break;
                            case 5:
                                if (iuserPoint < iPoint || getGroup(userGroupNumber, GroupNumber) == false)
                                {
                                    PageError("您的G币没达到:" + iPoint + "<li>或者您所在的组不能浏览</li>", "");
                                }
                                else
                                {
                                    FilyContent = getContent(TempletPath, NewsID, ChID, strPage, 1);
                                }
                                break;
                            case 6:
                                if (iuserPoint < iPoint || Guserpoint < Gpoint || getGroup(userGroupNumber, GroupNumber) == false)
                                {
                                    PageError("您的积分或G币不足,需要达到积分:" + iPoint + ",G币:" + Gpoint + "<li>或者您所在的组不能浏览</li>", "");
                                }
                                else
                                {
                                    FilyContent = getContent(TempletPath, NewsID, ChID, strPage, 1);
                                }
                                break;
                        }
                        #endregion 扣点
                    }
                    #endregion 判断是否扣点
                }
            }
            else
            {
                PageError("找不到记录", "javascript:history.back();", true);
            }
            rd.Close();
            if (Regex.Match(FilyContent, @"\</head\>[\s\S]*\<body", RegexOptions.IgnoreCase | RegexOptions.Compiled).Success)
            {
                FilyContent = Regex.Replace(FilyContent, "<body", getjs() + "<body", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            }
            else
            {
                FilyContent = getjs() + FilyContent;
            }

            FilyContent = (FilyContent.Replace(gInstallDir, Foosun.Publish.CommonData.getUrl())).Replace(gTempletDir, TempletDir);
            Response.Write(FilyContent);
        }
        else
        {
            PageError("错误的参数", "javascript:history.back();", true);
        }
    }

    protected bool getGroup(string uGroup, string nGroup)
    {
        return Foosun.Common.Public.CommgetGroup(uGroup, nGroup);
    }

    /// <summary>
    /// 得到内容
    /// </summary>
    /// <param name="TempletPath"></param>
    /// <param name="NewsID"></param>
    /// <returns></returns>
    protected string getContent(string TempletPath, string NewsID,int ChID, int gPage,int isPop)
    {
        Template newsTemplate = new Template(TempletPath, TempType.News);
        if (ChID != 0)
        {
            newsTemplate.CHNewsID = int.Parse(NewsID);
        }
        else
        {
            newsTemplate.NewsID = NewsID;
        }
        newsTemplate.ChID = ChID;
        newsTemplate.ClassID = null;
        newsTemplate.GetHTML();
        newsTemplate.ReplaceLabels();
        string FinlContent = newsTemplate.FinallyContent;
        string PageContent = string.Empty;
        string getFileContent = string.Empty;
        bool getRight = false;
        if (newsTemplate.MyTempType == TempType.News || newsTemplate.MyTempType == TempType.ChNews)
        {
            int pos1 = FinlContent.IndexOf("<!-FS:STAR=");
            int pos2 = FinlContent.IndexOf("FS:END->");
            if (pos1 > -1)
            {

                string PageHead = FinlContent.Substring(0, pos1);
                string PageEnd = FinlContent.Substring(pos2 + 8);
                string PageMid = FinlContent.Substring(pos1 + 11, pos2 - pos1 - 11);
                string[] ArrayCon = PageMid.Split(new string[] { "[FS:PAGE]" }, StringSplitOptions.RemoveEmptyEntries);
                int n = ArrayCon.Length;
                for (int i = 0; i < n; i++)
                {
                    if (i == (gPage - 1))
                    {
                        
                        PageContent = PageHead + ArrayCon[i] + PageEnd;
                        getFileContent = General.ReplaceResultPage(NewsID, PageContent.Replace("[FS:PAGE]", "").Replace("FS:END->", "").Replace("<!-FS:STAR=", ""), "", "", n, (i + 1),isPop);
                        getRight = true;
                    }
                    if (getRight)
                    {
                        break;
                    }
                }
            }
            else
            {
                getFileContent = FinlContent.Replace("FS:END->", "").Replace("<!-FS:STAR=", "");
            }
        }

        return getFileContent;
    }

    protected string getjs()
    {
        string getajaxJS = "<script language=\"javascript\" type=\"text/javascript\" src=\"" + Foosun.Publish.CommonData.getUrl() + "/configuration/js/Prototype.js\"></script>\r\n";
        getajaxJS += "<script language=\"javascript\" type=\"text/javascript\" src=\"" + Foosun.Publish.CommonData.getUrl() + "/configuration/js/jspublic.js\"></script>\r\n";
        getajaxJS += "<!--Created by dotNETCMS v1.0 For Foosun Inc. at " + DateTime.Now + "-->\r\n";
        return getajaxJS;
    }
}
