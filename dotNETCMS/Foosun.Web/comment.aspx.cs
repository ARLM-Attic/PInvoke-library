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
using Foosun.Model;

public partial class comment : Foosun.Web.UI.BasePage
{
    protected string newLine = "\r\n";
    protected string str_dirMana = Foosun.Config.UIConfig.dirDumm;
    protected string str_Templet = Foosun.Config.UIConfig.dirTemplet;  //获取模板路径
    public static string InstallDir = "{$InstallDir}";
    public static string TempletDir = "{$TempletDir}";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";
        string GetRount = Request.QueryString["commCount"];
        string sNewsID = Request.QueryString["id"];
        string Todays = Request.QueryString["Today"];

        if (GetRount != null && GetRount != string.Empty && sNewsID != null && sNewsID != string.Empty)
        {
            Foosun.CMS.News rd = new Foosun.CMS.News();
            Response.Write(rd.getCommCounts(sNewsID.ToString(), Todays.ToString()));
            Response.End();
        }
        else
        {
            CommentOp();
        }
    }

    /// <summary>
    /// 操作列表
    /// </summary>
    protected void CommentOp()
    {
        string CommentType = Request.QueryString["CommentType"];

        switch (CommentType)
        {
            case "AddComment":
                AddComment(0);
                break;
            case "GetCommentList":
                GetCommentList(0,0);
                break;
            case "GetAddCommentForm":
                Response.Write("Suc$$$" + GetAddCommentForm(CommentType));
                Response.End();
                break;
            case "LoginOut":
                LoginOut();
                break;
            case "getlist":
                GetCommentList(1,1);
                break;
        }
    }

    /// <summary>
    /// 列表
    /// </summary>
    /// <param name="NewsID"></param>

    /// <summary>
    /// 添加评论
    /// </summary>
    protected void AddComment(int num)
    {
        string str_UserNum = Request.QueryString["UserNum"];
        string str_UserPwd = Request.QueryString["UserPwd"];
        string str_NewsID = Foosun.Common.Input.Filter(Request.QueryString["id"]);
        string str_IsQID = Foosun.Common.Input.Filter(Request.QueryString["IsQID"]);
        string str_Content = Foosun.Common.Input.ToHtml(Request.QueryString["Content"]).Replace("?", "？").Replace("$$$", "");
        string SiteID = "0";
        string gChID = Request.QueryString["ChID"];
        int ChID = 0;
        if (gChID != string.Empty && gChID != null)
        {
            ChID = int.Parse(gChID.ToString());
        }
        Foosun.CMS.sys sys = new Foosun.CMS.sys();
        if (str_Content.Length > 200 || str_Content.Length < 2)
        {
            Response.Write("ERR$$$评论内容不能大于200字符，小于2个字符!");
            Response.End();
        }
        string str_commtype = Request.QueryString["commtype"];
        if (str_UserNum == "Guest")
        {
            str_UserNum = "匿名";
            DataTable dt = sys.UserPram();
            if (dt != null)
            {
                if (dt.Rows[0]["UnRegCommTF"].ToString() != "1")
                {
                    Response.Write("ERR$$$系统不允许匿名评论!");
                    Response.End();
                }
            }
            else
            {
                Response.Write("ERR$$$系统参数错误,!");
                Response.End();
            }
        }
        else
        {
            if (str_UserNum.ToLower() != "guest" || str_UserNum!="匿名")
            {
                if (Validate_Session())
                {
                    str_UserNum = Foosun.Global.Current.UserName;
                    SiteID = Foosun.Global.Current.SiteID;
                }
                else
                {
                    GlobalUserInfo info;
                    EnumLoginState state = _UserLogin.PersonLogin(str_UserNum, str_UserPwd, out info);
                    if(state==Foosun.Model.EnumLoginState.Succeed)
                    {
                        Foosun.Global.Current.Set(info);
                        str_UserNum = Foosun.Global.Current.UserName;
                        SiteID = Foosun.Global.Current.SiteID;
                    }
                    else
                    {
                        Response.Write("ERR$$$帐号或密码错误!");
                        Response.End();
                    }
                }
            }
            else
            {
                if(Validate_Session())
                {
                    str_UserNum = Foosun.Global.Current.UserName;
                    SiteID = Foosun.Global.Current.SiteID;
                }
                else
                {
                    //Response.Write("ERR$$$你没有登录!不能发表评论!");
                    //Response.End();
                }
            }
        }
        Foosun.Model.Comment ci = new Foosun.Model.Comment();
        ci.Id = 0;
        ci.Commid = "";
        ci.InfoID = str_NewsID;
        ci.APIID = "0";
        ci.DataLib = Foosun.Config.UIConfig.dataRe + "news";
        ci.Title = "";
        ci.Content = str_Content;
        ci.creatTime = DateTime.Now;
        ci.IP = Request.ServerVariables["REMOTE_ADDR"];
        ci.ChID = ChID;
        if (str_IsQID != null && str_IsQID != "")
            ci.QID = str_IsQID;
        else
            ci.QID = "";
        ci.UserNum = str_UserNum;
        ci.isRecyle = 0;
        int islocks = 0;
        DataTable isl = sys.UserPram();
        if (isl != null && isl.Rows.Count > 0)
        {
            if (Foosun.Common.Input.IsInteger(isl.Rows[0]["CommCheck"].ToString()))
            {
                islocks = int.Parse(isl.Rows[0]["CommCheck"].ToString());
            }
            isl.Clear(); isl.Dispose();
        }
        ci.islock = islocks;
        ci.OrderID = 0;
        ci.GoodTitle = 0;
        ci.isCheck = 0;
        ci.SiteID = SiteID;
        ci.commtype = int.Parse(str_commtype.ToString());

        Foosun.CMS.News news = new Foosun.CMS.News();
        if (news.AddComment(ci) == 1)
        {
            if (islocks == 1)
            {
                Response.Write("ERR$$$发表评论成功，但需要审核!");
            }
            else
            {
                string gCommentType = Request.QueryString["CommentType"];
                if (gCommentType == "getlist")
                {
                    Response.Write("Suc$$$" + CommentList(num, 1));
                }
                else
                {
                    Response.Write("Suc$$$" + CommentList(num, 0));
                }
            }
        }
        else
        {
            Response.Write("ERR$$$发表评论失败!");
        }
        Response.End();
    }

    /// <summary>
    /// 退出登录
    /// </summary>
    protected void LoginOut()
    {
        Logout();
        Response.Write("Suc$$$" + GetAddCommentForm(Request.QueryString["CommentType"]));
        Response.End();
    }

    /// <summary>
    /// 输出评论列表
    /// </summary>
    protected void GetCommentList(int num,int isList)
    {
        Response.Write(CommentList(num, isList));
        Response.End();
    }

    /// <summary>
    /// 取得评论列表
    /// </summary>
    /// <returns></returns>
    protected string CommentList(int num,int isList)
    {
        string NewsID = Foosun.Common.Input.Filter(Request.QueryString["id"]);
        string showdiv = Request.QueryString["showdiv"];
        string gChID = Request.QueryString["ChID"];
        int ChID = 0;
        if (gChID != string.Empty && gChID != null)
        {
            ChID = int.Parse(gChID.ToString());
        }
        string CommentTemplet = Foosun.Publish.General.ReadHtml(GetCommentTemplet());
        string str_Clist = "";
        string str_ClistPage = "";
        if (num == 1)
        {
            CommentTemplet = Foosun.Publish.General.ReadHtml(getCommentContentTemplet());
        }
        if (NewsID != "" && NewsID != null)
        {
            Foosun.CMS.News news = new Foosun.CMS.News();
            DataTable dt = news.getCommentList(NewsID);

            if (dt != null && dt.Rows.Count > 0)
            {
                string curPage = Request.QueryString["page"];    //当前页码
                int pageSize = 10;

                if (Foosun.Common.Input.IsInteger(Foosun.Config.UIConfig.commperPageNum))
                {
                    pageSize = int.Parse(Foosun.Config.UIConfig.commperPageNum);
                }

                int page = 0;                     //每页显示数
                if (num == 1)
                {
                    pageSize = 30;
                }

                if (curPage == "" || curPage == null || curPage == string.Empty) { page = 1; }
                else
                {
                    try { page = int.Parse(curPage); }
                    catch
                    {
                        page = 0;
                    }
                }
                int i, j;
                int Cnt = dt.Rows.Count;

                int pageCount = Cnt / pageSize;
                if (Cnt % pageSize != 0) { pageCount++; }
                if (page > pageCount) { page = pageCount; }
                if (page < 1) { page = 1; }

                bool b_T = false; bool b_P = false; bool b_title = false; bool b_stat = false; bool b_post = false; bool p_list = false;
                if (CommentTemplet.IndexOf("{#Page_CommTitle}") > -1) { b_T = true; }
                if (CommentTemplet.IndexOf("{#Page_CommPages}") > -1) { b_P = true; }
                if (num == 1)
                {
                    if (CommentTemplet.IndexOf("{#Page_PageTitle}") > -1) { b_title = true; }
                    if (CommentTemplet.IndexOf("{#Page_CommStat}") > -1) { b_stat = true; }
                    if (CommentTemplet.IndexOf("{#Page_PostComm}") > -1) { b_post = true; }
                    if (CommentTemplet.IndexOf("{#Page_NewsURL}") > -1) { p_list = true; }
                }

                string tmpContent = "<table style=\"width:90%;\" border=\"0\" cellspacing=\"0\" cellpadding=\"1\"><tr><td style=\"width:32%;text-align:right\">不知所云</td><td><img alt=\"不知所云\" src=\"" + Foosun.Publish.CommonData.getUrl() + "/sysimages/commface/0.gif\" border=\"0\"></td><td style=\"width:70%\"><table width='100%'><tr><td style=\"height:15px;width:" + news.returnCommentGD(NewsID, 0) + "px;background-color:#0000FF\"></td><td>" + news.returnCommentGD(NewsID, 0) + "%</td></tr></table></td></tr>";
                tmpContent += "<tr><td style=\"text-align:right\">不赞成</td><td><img alt=\"不赞成\" src=\"" + Foosun.Publish.CommonData.getUrl() + "/sysimages/commface/1.gif\" border=\"0\"></td><td><table width='100%'><tr><td style=\"padding:inherit;height:15px;width:" + news.returnCommentGD(NewsID, 1) + "px;background-color:#990066\"></td><td>" + news.returnCommentGD(NewsID, 1) + "%</td></tr></table></td></tr>";
                tmpContent += "<tr><td style=\"text-align:right\">中立</td><td><img alt=\"中立\" src=\"" + Foosun.Publish.CommonData.getUrl() + "/sysimages/commface/2.gif\" border=\"0\"></td><td><table width='100%'><tr><td style=\"padding:inherit;height:15px;width:" + news.returnCommentGD(NewsID, 2) + "px;background-color:#FF6600\"></td><td>" + news.returnCommentGD(NewsID, 2) + "%</td></tr></table></td></tr>";
                tmpContent += "<tr><td style=\"text-align:right\">赞成</td><td><img alt=\"赞成\" src=\"" + Foosun.Publish.CommonData.getUrl() + "/sysimages/commface/3.gif\" border=\"0\"></td><td><table width='100%'><tr><td  style=\"padding:inherit;height:15px;width:" + news.returnCommentGD(NewsID, 3) + "px;background-color:#FF0000\"></td><td>" + news.returnCommentGD(NewsID, 3) + "%</td></tr></table></td></tr>";
                tmpContent += "<tr><td style=\"text-align:right\">堪为精典</td><td><img alt=\"堪为精典\" src=\"" + Foosun.Publish.CommonData.getUrl() + "/sysimages/commface/4.gif\" border=\"0\"></td><td><table width='100%'><tr><td style=\"padding:inherit;height:15px;width:" + news.returnCommentGD(NewsID, 4) + "px;background-color:#009900\"></td><td>" + news.returnCommentGD(NewsID, 4) + "%</td></tr></table></td></tr></table>";
                #region 循环条件
                string goodTitle = "";
                for (i = (page - 1) * pageSize, j = 1; i < Cnt && j <= pageSize; i++, j++)
                {
                    int k = Cnt;
                    int k1 = 0;
                    string kfool = "";
                    if (page == 1)
                        k = i + 1;
                    else
                        k = ((page - 1) * pageSize) + j;
                    if (k < 10)
                    {
                        k1 = 0 + (Cnt - k);
                    }
                    else
                    {
                        k1 = (Cnt - k);
                    }
                    if ((k1 + 1) < 10)
                    {
                        kfool = "0" + (k1 + 1).ToString();
                    }
                    else
                    {
                        kfool = (k1 + 1).ToString();
                    }
                    if (b_T)//显示标题
                    {
                        string str_UserName = dt.Rows[i]["UserNum"].ToString();
                        string IPstr = dt.Rows[i]["IP"].ToString();
                        string TmpIP1 = "";
                        string TmpIP = (IPstr.Remove(IPstr.LastIndexOf(".")));
                        if (str_UserName == "匿名")
                        {
                            TmpIP1 = "IP:" + TmpIP.Remove(TmpIP.LastIndexOf(".")) + ".*.*";
                        }
                        if (dt.Rows[i]["GoodTitle"].ToString() == "1")
                        {
                            goodTitle = "<img alt=\"精华评论\" src=\""+Foosun.Publish.CommonData.getUrl() + "/sysImages/normal/best.jpg\" border=\"0\" />&nbsp;";
                        }
                        string commtypes = commtypes = "<img alt=\"中立\" src=\"" + Foosun.Publish.CommonData.getUrl() + "/sysimages/commface/2.gif\" border=\"0\">";
                        string commtype = dt.Rows[i]["commtype"].ToString();
                        switch (commtype)
                        {
                            case "0":
                                commtypes = "<img alt=\"不知所云\" src=\"" + Foosun.Publish.CommonData.getUrl() + "/sysimages/commface/0.gif\" border=\"0\">";
                                break;
                            case "1":
                                commtypes = "<img alt=\"不赞成\" src=\"" + Foosun.Publish.CommonData.getUrl() + "/sysimages/commface/1.gif\" border=\"0\">";
                                break;
                            case "2":
                                commtypes = "<img alt=\"中立\" src=\"" + Foosun.Publish.CommonData.getUrl() + "/sysimages/commface/2.gif\" border=\"0\">";
                                break;
                            case "3":
                                commtypes = "<img alt=\"赞成\" src=\"" + Foosun.Publish.CommonData.getUrl() + "/sysimages/commface/3.gif\" border=\"0\">";
                                break;
                            case "4":
                                commtypes = "<img alt=\"堪为精品\" src=\"" + Foosun.Publish.CommonData.getUrl() + "/sysimages/commface/4.gif\" border=\"0\">";
                                break;
                        }

                        if (str_UserName != "匿名")
                        {
                            str_UserName = "" + commtypes + " <a href=\"" + Foosun.Publish.CommonData.getUrl() + "/" + Foosun.Config.UIConfig.dirUser + "/showuser-" + dt.Rows[i]["UserNum"].ToString() + ".aspx\" target=\"_blank\">" + dt.Rows[i]["UserNum"].ToString() + "</a>  ";
                        }
                        else
                        {
                            str_UserName = "" + commtypes + " 网友  ";
                        }
                        str_Clist += "<div style=\"height:30px;\"><span style=\"color:#990000;\">" + kfool + "楼</span>&nbsp;&nbsp;" + str_UserName;
                        str_Clist += "<span style=\"font-size:10px;\">" + dt.Rows[i]["creatTime"].ToString() + "&nbsp;&nbsp;&nbsp;" + TmpIP1 + "</span></div>\r";

                        if (!dt.Rows[i].IsNull("QID") && dt.Rows[i]["QID"].ToString() != "")
                        {
                            str_Clist += GetQIDInfo(dt, dt.Rows[i]["Commid"].ToString(), dt.Rows[i]["UserNum"].ToString());
                        }
                        else
                        {
                            string str_Content = goodTitle + dt.Rows[i]["Content"].ToString();
                            string Commfiltrchar = "";
                            Foosun.CMS.sys sd = new Foosun.CMS.sys();
                            DataTable sds = sd.UserPram();
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                Commfiltrchar = sds.Rows[0]["Commfiltrchar"].ToString();
                                if (Commfiltrchar.IndexOf(",") > -1)
                                {
                                    string[] CommfiltrcharARR = Commfiltrchar.Split(',');
                                    for (int m = 0; m < CommfiltrcharARR.Length; m++)
                                    {
                                        str_Content = str_Content.Replace(CommfiltrcharARR[m], "***");
                                    }
                                }
                                sds.Clear(); sds.Dispose();
                            }
                            str_Clist += "<div style=\"padding-left:25px;padding-bottom:6px;\">   " + str_Content + "</div>\r";
                        }
                    }
                }
                #endregion 循环条件
                string str_CPage = "";
                if (b_P) //显示分页
                {
                    str_CPage += "<div style=\"width:100%;padding-top:15px;\">\r";
                    if (num == 1)
                    {
                        str_CPage += "<span>" + ShowPageContent(NewsID, Foosun.Publish.CommonData.getUrl(), page, Cnt, pageCount) + "</span>\r";
                        CommentTemplet = CommentTemplet.Replace("{#Page_CommPages}", "");
                    }
                    else
                    {
                        str_CPage += "<span>" + ShowPage(NewsID, page, Cnt, pageCount) + "</span>\r";
                        CommentTemplet = CommentTemplet.Replace("{#Page_CommPages}", str_CPage);
                    }
                    str_CPage += "</div>\r";
                }
                CommentTemplet = CommentTemplet.Replace("{#Page_Commidea}", "<span style=\"width:95%;\">" + tmpContent + "</span>");
                str_ClistPage = str_Clist;
                if (num == 1)
                {
                    str_ClistPage = "<div id=\"CommentlistPage\">" + str_Clist + str_CPage + "</div>";
                }
                CommentTemplet = CommentTemplet.Replace("{#Page_CommTitle}", str_ClistPage);
                string str_PageTitle = "";
                string str_PageTitle1 = "";
                if (num == 1)
                {
                    if (b_title || p_list)
                    {
                        IDataReader nd = news.getNewsInfo(NewsID,ChID);
                        string NewsUrl = "";
                        if (nd.Read())
                        {
                            IDataReader CD = news.getClassInfo(nd["ClassID"].ToString(),ChID);
                            if (CD.Read())
                            {
                                if (p_list)
                                {
                                    if (ChID != 0)
                                    {
                                        NewsUrl = getCHInfoURL(ChID,int.Parse(nd["isDelPoint"].ToString()), int.Parse(nd["id"].ToString()), CD["SavePath"].ToString(), nd["SavePath"].ToString(),nd["FileName"].ToString());
                                        str_PageTitle += "<a href=\"" + NewsUrl + "\">" + nd["Title"].ToString() + "</a>";
                                    }
                                    else
                                    {
                                        NewsUrl = getNewsURL(nd["isDelPoint"].ToString(), nd["NewsID"].ToString(), nd["savePath"].ToString(), CD["SavePath"].ToString() + "/" + CD["SaveClassframe"].ToString(), nd["FileName"].ToString(), nd["FileEXName"].ToString());
                                        str_PageTitle += "<a href=\"" + NewsUrl + "\">" + nd["NewsTitle"].ToString() + "</a>";
                                    }
                                    CommentTemplet = CommentTemplet.Replace("{#Page_NewsURL}", str_PageTitle);
                                }
                                if (ChID != 0)
                                {
                                    str_PageTitle1 += nd["Title"].ToString();
                                }
                                else
                                {
                                    str_PageTitle1 += nd["NewsTitle"].ToString();
                                }
                                CommentTemplet = CommentTemplet.Replace("{#Page_PageTitle}", str_PageTitle1);
                            }
                            else
                            {
                                CommentTemplet = CommentTemplet.Replace("{#Page_NewsURL}", "");
                                CommentTemplet = CommentTemplet.Replace("{#Page_PageTitle}", "");
                            }
                            CD.Close();
                        }
                        else
                        {
                            CommentTemplet = CommentTemplet.Replace("{#Page_NewsURL}", "");
                            CommentTemplet = CommentTemplet.Replace("{#Page_PageTitle}", "");
                        }
                        nd.Close();
                    }
                    if (b_stat)
                    {
                        CommentTemplet = CommentTemplet.Replace("{#Page_CommStat}", "共" + Cnt + "条 显示" + pageSize + "条 ");
                    }
                    if (b_post)
                    {
                        if (num == 1)
                        {
                            string PostCommstr = GetAddCommentForm(Request.QueryString["CommentType"]);
                            CommentTemplet = CommentTemplet.Replace("{#Page_PostComm}", PostCommstr);
                        }
                        else
                        {
                            CommentTemplet = CommentTemplet.Replace("{#Page_PostComm}", "");
                        }
                    }
                }
                dt.Clear(); dt.Dispose();
            }
            else
            {
                string returnstr = "";
                if (num == 1)
                {
                    returnstr = ",<a href=\"javascript:history.back();\">返回</a>";
                }
                CommentTemplet = "<div id=\"CommentlistPage\">当前没有评论信息" + returnstr + "</div>";
            }
        }
        else
        {
            CommentTemplet = "<div style=\"width:100%;\">错误的参数</div>\r";
        }
        string getajaxJS = "<script language=\"javascript\" type=\"text/javascript\" src=\"" + Foosun.Publish.CommonData.getUrl() + "/configuration/js/Prototype.js\"></script>\r\n";
        getajaxJS += "<script language=\"javascript\" type=\"text/javascript\" src=\"" + Foosun.Publish.CommonData.getUrl() + "/configuration/js/jspublic.js\"></script>\r\n";
        getajaxJS += "<!--Created by dotNETCMS v1.0 For Foosun Inc. at " + DateTime.Now + "-->\r\n";
        string getContent = string.Empty;
        if (isList == 1)
        {
            if (Regex.Match(CommentTemplet, @"\</head\>[\s\S]*\<body", RegexOptions.IgnoreCase | RegexOptions.Compiled).Success)
            {
                getContent = Regex.Replace(CommentTemplet, "<body", getajaxJS + "<body", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            }
            else
            {
                getContent = getajaxJS + CommentTemplet;
            }
        }
        else
        {
            getContent = CommentTemplet;
        }
        if (showdiv != null && showdiv != string.Empty)
        {
            getContent = str_ClistPage;
        }
        getContent = (getContent.Replace(InstallDir, Foosun.Publish.CommonData.getUrl())).Replace(TempletDir, str_Templet);
        return getContent;
    }

    /// <summary>
    /// 获取引用的评论
    /// </summary>
    /// <param name="dt">数据表</param>
    /// <param name="Commid">评论编号</param>
    /// <param name="UserName">用户名</param>
    /// <returns></returns>
    protected string GetQIDInfo(DataTable dt, string Commid, string UserName)
    {
        string str_QID = "";
        DataRow[] row = dt.Select("Commid='" + Commid + "'");
        if (row.Length == 1)
        {
            str_QID += "<span>" + UserName + "引用了：" + dt.Rows[0]["UserNum"].ToString() + "</span>\r";
            str_QID += "<br />\r";
            str_QID += "<span>" + dt.Rows[0]["Content"].ToString() + "</span>\r";
        }
        return str_QID;
    }

    /// <summary>
    /// 得到评论表单
    /// </summary>
    protected string GetAddCommentForm(string tmstr)
    {
        Foosun.CMS.sys sys = new Foosun.CMS.sys();
        string NewsID = Foosun.Common.Input.Filter(Request.QueryString["id"]);

        string UserName = "Guest";
        string UserExit = "";

        if(Validate_Session())
        {
            UserName = Foosun.Global.Current.UserName;
            if (tmstr == "getlist")
            {
                UserExit = "<span id=\"loginOutB\"><a href=\"javascript:CommentLoginOut(this.form,'" + Foosun.Publish.CommonData.getUrl() + "');\">注销帐户</a></span>&nbsp;&nbsp;<a hrefs=\"" + Foosun.Publish.CommonData.getUrl() + "/" + Foosun.Config.UIConfig.dirUser + "/index.aspx?url=info/mycom.aspx\">我的评论</a>";
            }
            else
            {
                UserExit = "<a href=\"javascript:CommentLoginOut();\">注销帐户</a>&nbsp;&nbsp;<a href=\"" + Foosun.Publish.CommonData.getUrl() + "/" + Foosun.Config.UIConfig.dirUser + "/index.aspx?urls=info/mycom.aspx\">我的评论</a>";
            }
        }
        else
        {
            DataTable dt = sys.UserPram();
            if (dt != null)
            {
                if (dt.Rows[0]["UnRegCommTF"].ToString() != "1")
                {
                    UserName = "";
                    UserExit = "没帐户？<a href=\"" + Foosun.Publish.CommonData.getUrl() + "/" + Foosun.Config.UIConfig.dirUser + "/register.aspx\">这里注册</a>";
                }
                else
                {
                    UserExit = "<span id=\"isGuest\">(匿名用户请直接使用Guest用户名)</span>";
                }
                dt.Clear(); dt.Dispose();
            }
        }
        string str_CommForm = "<div>\r";
        str_CommForm += "<form action=\"\" method=\"post\" id=\"CommandForm\" name=\"CommandForm\">\r";
        if (Validate_Session())
        {
            str_CommForm += "<div style=\"text-align:left;height:25px;\">用户名" + UserName + " <span style=\"display:none;\"><input name=\"UserNum\" size=\"12\" type=\"text\" value=\"" + UserName + "\"></span>";
            str_CommForm += "   <span style=\"display:none;\">密码 <input name=\"UserPwd\" size=\"12\" type=\"password\"></span> " + UserExit + " </div>\r";
        }
        else
        {
            str_CommForm += "<div style=\"text-align:left;height:25px;\">用户名 <input name=\"UserNum\" size=\"12\" type=\"text\" value=\"\">";
            str_CommForm += "   密码 <input name=\"UserPwd\" size=\"12\" type=\"password\"> " + UserExit + " </div>\r";
        }
        str_CommForm += "<div style=\"text-align:left;height:25px;\">观点：<input type=\"radio\" name=\"commtype\" value=\"0\" />不知所云 <input type=\"radio\" name=\"commtype\" value=\"1\" />不赞成 <input type=\"radio\" checked=\"true\" name=\"commtype\" value=\"2\" />中立 <input type=\"radio\" name=\"commtype\" value=\"3\" />赞成 <input type=\"radio\" name=\"commtype\" value=\"4\" />堪为精品</div>\r";
        str_CommForm += "<div style=\"text-align:left;height:110px;\">\r";
        if (tmstr == "getlist")
        {
            str_CommForm += "<textarea name=\"Content\" style=\"font-size:12px;width:90%\" rows=\"6\" onkeydown=\"javascript:if(event.ctrlKey&&event.keyCode==13){CommandSubmitContent(this.form,'" + Foosun.Publish.CommonData.getUrl() + "','" + NewsID + "');}\"></textarea>\r";
        }
        else
        {
            str_CommForm += "<textarea name=\"Content\" style=\"font-size:12px;width:90%\" rows=\"6\" onkeydown=\"javascript:if(event.ctrlKey&&event.keyCode==13){CommandSubmit(this.form);}\"></textarea>\r";
        }
        str_CommForm += "</div>\r";
        str_CommForm += "<div style=\"text-align:left;height:35px;\">\r";
        if (tmstr == "getlist")
        {
            str_CommForm += "<input name=\"B_CommandSubmit\" type=\"button\" value=\"发表评论\" onclick=\"javascript:CommandSubmitContent(this.form,'" + Foosun.Publish.CommonData.getUrl() + "','" + NewsID + "');\">\r";
        }
        else
        {
            str_CommForm += "<input name=\"B_CommandSubmit\" type=\"button\" value=\"发表评论\" onclick=\"javascript:CommandSubmit(this.form);\">\r";
        }
        str_CommForm += "<input type=\"reset\" name=\"B_CommandReset\" value=\"重新填写\">&nbsp;<span style=\"Color:Red;\">Ctrl+回车</span>&nbsp;提交评论.\r";
        str_CommForm += "<input name=\"IsQID\" type=\"hidden\" value=\"\">\r";
        str_CommForm += "</div>\r";
        str_CommForm += "<div style=\"text-align:left;height:30px;\">请自觉遵守互联网相关政策法规,评论字数2-200字.请不要发广告。您发表的问题不代表本站观点。一切后果由发表者负责</div>\r";
        str_CommForm += "</form>\r";
        str_CommForm += "</div>\r";

        return str_CommForm;
    }

    /// <summary>
    /// 分页
    /// </summary>
    /// <param name="page">当前页码</param>
    /// <param name="Cnt">总记录数</param>
    /// <param name="pageCount">最大页数</param>
    /// <returns></returns>
    protected string ShowPage(string NewsID, int page, int Cnt, int pageCount)
    {
        string urlstr = "共" + Cnt.ToString() + "条记录,共" + pageCount.ToString() + "页,当前第" + page.ToString() + "页   ";
        urlstr = urlstr + "<a href=\"javascript:void(0);\" onclick=\"javascript:GetCommentList('1');\" title=\"首页\" >首页</a> ";
        if ((page - 1) < 1)
            urlstr = urlstr + " <a href=\"javascript:void(0);\" onclick=\"javascript:GetCommentList('1');\" title=\"上一页\" >上一页</a> ";
        else
            urlstr = urlstr + " <a href=\"javascript:void(0);\" onclick=\"javascript:GetCommentList('" + (page - 1) + "');\" title=\"上一页\" >上一页</a> ";
        if ((page + 1) < pageCount)
            urlstr = urlstr + " <a href=\"javascript:void(0);\" onclick=\"javascript:GetCommentList('" + (page + 1) + "');\" title=\"下一页\" >下一页</a> ";
        else
            urlstr = urlstr + " <a href=\"javascript:void(0);\" onclick=\"javascript:GetCommentList('" + pageCount + "');\" title=\"下一页\" >下一页</a> ";
        urlstr = urlstr + " <a href=\"javascript:void(0);\" onclick=\"javascript:GetCommentList('" + pageCount + "');\" title=\"尾页\">尾页</a> ";
        string gChID = Request.QueryString["ChID"];
        int ChID = 0;
        if (gChID != string.Empty && gChID != null)
        {
            ChID = int.Parse(gChID.ToString());
        }
        return urlstr + " <a href=\"" + Foosun.Publish.CommonData.getUrl() + "/Comment.aspx?CommentType=getlist&id=" + NewsID + "&ChID=" + ChID.ToString() + "\" style=\"color:Red;\">查看全部</a>";
    }

    /// <summary>
    /// 分页
    /// </summary>
    /// <param name="page">当前页码</param>
    /// <param name="Cnt">总记录数</param>
    /// <param name="pageCount">最大页数</param>
    /// <returns></returns>
    protected string ShowPageContent(string NewsID, string URLdomain,int page, int Cnt, int pageCount)
    {
        string urlstr = "共<strong>" + Cnt.ToString() + "</strong>条记录,共<strong>" + pageCount.ToString() + "</strong>页,当前第<strong>" + page.ToString() + "</strong>页   ";
        urlstr = urlstr + "<a href=\"javascript:void(0);\" onclick=\"javascript:GetCommentListContent('" + URLdomain + "','" + NewsID + "','1');\" title=\"首页\" >首页</a> ";
        if ((page - 1) < 1)
            urlstr = urlstr + " <a href=\"javascript:void(0);\" onclick=\"javascript:GetCommentListContent('" + URLdomain + "','" + NewsID + "','1');\" title=\"上一页\" >上一页</a> ";
        else
            urlstr = urlstr + " <a href=\"javascript:void(0);\" onclick=\"javascript:GetCommentListContent('" + URLdomain + "','" + NewsID + "','" + (page - 1) + "');\" title=\"上一页\" >上一页</a> ";
        if ((page + 1) < pageCount)
            urlstr = urlstr + " <a href=\"javascript:void(0);\" onclick=\"javascript:GetCommentListContent('" + URLdomain + "','" + NewsID + "','" + (page + 1) + "');\" title=\"下一页\" >下一页</a> ";
        else
            urlstr = urlstr + " <a href=\"javascript:void(0);\" onclick=\"javascript:GetCommentListContent('" + URLdomain + "','" + NewsID + "','" + pageCount + "');\" title=\"下一页\" >下一页</a> ";
        urlstr = urlstr + " <a href=\"javascript:void(0);\" onclick=\"javascript:GetCommentListContent('" + URLdomain + "','" + NewsID + "','" + pageCount + "');\" title=\"尾页\">尾页</a> ";
        return urlstr;
    }
    /// <summary>
    /// 获取新闻页面评论模板路径
    /// </summary>
    /// <returns>返回评论模板路径</returns>
    protected string GetCommentTemplet()
    {

        if (str_dirMana != "" && str_dirMana != null && str_dirMana != string.Empty)//判断虚拟路径是否为空,如果不是则加上//
            str_dirMana = "//" + str_dirMana;
        string str_FilePath = Server.MapPath(str_dirMana + "\\" + str_Templet + "\\Content\\CommentPage.html");
        return str_FilePath;
    }

    /// <summary>
    /// 获得新闻独立评论页面模板路径
    /// </summary>
    /// <returns>返回评论模板路径</returns>
    protected string getCommentContentTemplet()
    {
        if (str_dirMana != "" && str_dirMana != null && str_dirMana != string.Empty)//判断虚拟路径是否为空,如果不是则加上//
            str_dirMana = "//" + str_dirMana;
        string str_FilePath = Server.MapPath(str_dirMana + "\\" + str_Templet + "\\Content\\CommentList.html");
        return str_FilePath;
    }

    /// <summary>
    /// 得到新闻地址
    /// </summary>
    /// <param name="isDelPoint"></param>
    /// <param name="NewsID"></param>
    /// <param name="SavePath"></param>
    /// <param name="SaveClassframe"></param>
    /// <param name="FileName"></param>
    /// <param name="FileEXName"></param>
    /// <returns></returns>
    protected string getNewsURL(string isDelPoint, string NewsID, string SavePath, string SaveClassframe, string FileName, string FileEXName)
    {
        string str_temppath = "";
        if (Foosun.Common.Public.readparamConfig("ReviewType") == "0")
        {
            if (isDelPoint != "0")
            {
                str_temppath = "/content.aspx?id=" + NewsID + "";
            }
            else
            {
                str_temppath = "/" + SaveClassframe + "/" + SavePath + "/" + FileName + FileEXName;
            }
        }
        else
        {
            str_temppath = "/content.aspx?id=" + NewsID + "";
        }
        str_temppath = Foosun.Publish.CommonData.getUrl() + str_temppath.Replace("//", "/");
        return str_temppath;
    }

    /// <summary>
    /// 频道信息地址
    /// </summary>
    public string getCHInfoURL(int ChID, int isDelPoint, int id, string ClassSavePath, string SavePath, string FileName)
    {
        string urls = string.Empty;
        int ishtml = int.Parse(Foosun.Common.Public.readCHparamConfig("isHTML", ChID));
        string Domain = Foosun.Common.Public.readCHparamConfig("bdomain", ChID);
        string linkType = Foosun.Common.Public.readparamConfig("linkTypeConfig");
        string htmldir = Foosun.Common.Public.readCHparamConfig("htmldir", ChID);
        string dirdumm = Foosun.Config.UIConfig.dirDumm;
        if (dirdumm.Trim() != string.Empty)
        {
            dirdumm = "/" + dirdumm;
        }
        if (ishtml != 0 && isDelPoint == 0)
        {
            string flg = string.Empty;
            if (Domain != string.Empty)
            {
                if (linkType == "1")
                {
                    if (Domain.IndexOf("http://") > -1) { flg = Domain; }
                    else { flg = "http://" + Domain; }
                    urls = flg + "/" + ClassSavePath + "/" + SavePath + "/" + FileName;
                }
                else
                {
                    urls = "/" + ClassSavePath + "/" + SavePath + "/" + FileName;
                }
            }
            else
            {
                urls = "/" + htmldir + "/" + ClassSavePath + "/" + SavePath + "/" + FileName;
                urls = urls.Replace("//", "/");
                urls = Foosun.Publish.CommonData.getUrl() + urls;
            }
        }
        else
        {
            urls = Foosun.Publish.CommonData.getUrl() + "/Content.aspx?Id=" + id.ToString() + "&ChID=" + ChID.ToString() + "";
        }
        return urls.ToLower().Replace("{@dirhtml}", Foosun.Config.UIConfig.dirHtml);
    }
}
