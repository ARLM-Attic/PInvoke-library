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

public partial class search : Foosun.Web.UI.BasePage
{
    private string newLine = "\r\n";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";
        string tags = Request.QueryString["tags"];
        tags = tags.Trim();
        if (tags == null || tags == string.Empty || tags == "")
        {
            Response.Write("请填写关键字");
            Response.End();
        }
        else
        {
            SearchOp();
        }
    }

    protected void SearchOp()
    {
        Response.Write(SearchLoad());
        Response.End();
    }

    protected string SearchLoad()
    {
        string SearchTemplet = Foosun.Publish.General.ReadHtml(GetSearchTemplet());

        bool b_C = false;
        bool b_P = false;

        if (SearchTemplet.IndexOf("{#Page_SearchContent}") > -1)
            b_C = true;
        if (SearchTemplet.IndexOf("{#Page_SearchPages}") > -1)
            b_P = true;

        string type = Request.QueryString["type"];
        string tags = Request.QueryString["tags"];
        string cid = Request.QueryString["ChID"];
        int ChID = 0;
        if (cid != null && cid != string.Empty)
        {
            ChID = int.Parse(cid.ToString());
        }
        if (type == string.Empty && type == null)
        {
            Response.Write("请选择搜索类型");
            Response.End();
        }
        string date = Request.QueryString["Date"];
        string classid = Request.QueryString["ClassID"];
        string editor = Request.QueryString["editor"];

        string str_List = "<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"4\" cellspacing=\"1\">";
        str_List += "<tr><td colspan=\"2\">搜索<font color=\"red\"><b>“" + tags + "”</b></font>结果如下：</td></tr>";
        Foosun.Model.Search si = new Foosun.Model.Search();

        si.type = type;
        si.tags = tags;
        si.date = date;
        si.classid = classid;
        si.chid = ChID;

        string curPage = Request.QueryString["page"];
        int page = 0;

        if (curPage == "" || curPage == null || curPage == string.Empty) { page = 1; }
        else
        {
            try { page = int.Parse(curPage); }
            catch
            {
                page = 1;
            }
        }

        int i, j;
        string DTable = string.Empty;
        if (ChID != 0)
        {
            Foosun.CMS.Channel RD = new Foosun.CMS.Channel();
            DTable = RD.getChannelTable(ChID);
        }
        try
        {
            DataTable dt = Foosun.CMS.Search.SearchGetPage(DTable, page, 15, out i, out j, si);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    str_List += getRow(dt.Rows[k], ChID);
                }
                dt.Clear();
                dt.Dispose();
            }
            else
            {
                str_List += "<tr>" + newLine + "<td>没有找到相关的记录。" + newLine + "</td>" + newLine + "</tr>";
            }
            str_List += "</table>";

            string str_Page = "<div>" + ShowPage(page, i, j) + "</div>";


            if (b_C)
            {
                SearchTemplet = SearchTemplet.Replace("{#Page_SearchContent}", str_List);
            }
            if (b_P)
            {
                SearchTemplet = SearchTemplet.Replace("{#Page_SearchPages}", str_Page);
            }
        }

        catch
        {
            SearchTemplet = SearchTemplet.Replace("{#Page_SearchContent}", "没有找到相关的记录");
            SearchTemplet = SearchTemplet.Replace("{#Page_SearchPages}", "");
        }
        return SearchTemplet;
    }

    protected string getRow(DataRow dr, int ChID)
    {
        Foosun.CMS.ContentManage getNewsURL = new Foosun.CMS.ContentManage();
        string str_Row = "";
        string stags = Request.QueryString["tags"];
        string FavNewsUrl = string.Empty;
        string dim = Foosun.Config.UIConfig.dirDumm;
        if (dim.Trim() != string.Empty)
        {
            dim = "/" + Foosun.Config.UIConfig.dirDumm;
        }
        string NewsUrl = "";
        string NewsUrl1 = "";
        if (ChID != 0)
        {
            Channel RD = new Channel();
            string dirHTML = Foosun.Common.Public.readCHparamConfig("htmldir", ChID);
            dirHTML = dirHTML.Replace("{@dirHTML}", Foosun.Config.UIConfig.dirHtml);
            string ClassSave = string.Empty;
            IDataReader cdr = RD.GetClassInfo(int.Parse(dr["ClassID"].ToString()));
            if (cdr.Read())
            {
                ClassSave = cdr["SavePath"].ToString();
            }
            cdr.Close();
            NewsUrl = getNewsURL.getnewsReview(dr["NewsID"].ToString(), "news");//dim + "/" + dirHTML + "/" + ClassSave + "/" + dr["SavePath"].ToString() + "/" + dr["FileName"].ToString();
            NewsUrl = NewsUrl.Replace("//", "/");//NewsUrl.Replace("//", "/");
            NewsUrl1 = "http://" + Request.ServerVariables["SERVER_NAME"] + dim + NewsUrl;
            FavNewsUrl = Foosun.Config.UIConfig.dirUser + "/info/collection.aspx?Type=Add&ChID=" + ChID.ToString() + "&id=" + dr["id"].ToString();
            if (dr["PicURL"].ToString().Length < 5)
            {

                str_Row += "<tr>" + newLine;
                str_Row += "<td colspan=\"2\"><li><a href=\"" + NewsUrl + "\" target=\"_blank\"><span style=\"font-size:14px;\">" + dr["Title"].ToString().Replace(stags, "<span style=\"color:red\">" + stags + "</span>") + "</span></a> " + dr["CreatTime"].ToString() + "</li></td>" + newLine;
                str_Row += "</tr>" + newLine;
                str_Row += "<tr>" + newLine;
                str_Row += "<td colspan=\"2\">" + Foosun.Common.Input.GetSubString(Foosun.Common.Input.FilterHTML((dr["Content"].ToString()).Replace("?", "？")), 200).Replace(stags, "<span style=\"color:red\">" + stags + "</span>") + "<div style=\"color:green\">" + NewsUrl1 + "</div></td>" + newLine;
                str_Row += "</tr>" + newLine;
                str_Row += "<tr>" + newLine;
                str_Row += "<td colspan=\"2\" align=\"left\" valign=\"top\" style=\"height:35px;\"><a href=\"" + FavNewsUrl.Replace("//", "/") + "\" target=\"_blank\">收藏</a> <a href=\"javascript:void(0);\" onclick=\"sendfriend('" + NewsUrl1 + "','" + dr["Title"].ToString() + "');\">推荐给朋友</a></td>" + newLine;
                str_Row += "</tr>" + newLine;
            }
            else
            {
                str_Row += "<tr>" + newLine;
                string gimgr = dim + dr["PicURL"].ToString().ToLower().Replace("{@dirfile}", Foosun.Config.UIConfig.dirFile);
                str_Row += "<td rowspan=\"3\"><img src=\"" + gimgr + "\" height=\"110\" width=\"140\" border=\"0\" /></td>" + newLine;
                str_Row += "<td align=\"left\" width=\"100%\"><li><a href=\"" + NewsUrl + "\" target=\"_blank\"><span style=\"font-size:14px;\">" + dr["Title"].ToString().Replace(stags, "<span style=\"color:red\">" + stags + "</span>") + "</span></a> " + dr["CreatTime"].ToString() + "</li></td>" + newLine;
                str_Row += "</tr>" + newLine;
                str_Row += "<tr>" + newLine;
                str_Row += "<td align=\"left\">" + Foosun.Common.Input.GetSubString(Foosun.Common.Input.FilterHTML((dr["Content"].ToString()).Replace("?", "？")), 200).Replace(stags, "<span style=\"color:red\">" + stags + "</span>") + "<div style=\"color:green\">" + NewsUrl1 + "</div></td>" + newLine;
                str_Row += "</tr>" + newLine;
                str_Row += "<tr>" + newLine;
                str_Row += "<td align=\"left\" valign=\"top\" style=\"height:35px;\"><a href=\"" + FavNewsUrl.Replace("//", "/") + "\" target=\"_blank\">收藏</a> <a href=\"javascript:void(0);\" onclick=\"sendfriend('" + NewsUrl1 + "','" + dr["Title"].ToString() + "');\">推荐给朋友</a></td>" + newLine;
                str_Row += "</tr>" + newLine;
            }
        }
        else
        {
            FavNewsUrl = Foosun.Config.UIConfig.dirUser + "/info/collection.aspx?Type=Add&id=" + dr["NewsID"].ToString();
            string SaveClassframe = Foosun.CMS.Search.getSaveClassframe(dr["ClassID"].ToString());
            switch (dr["NewsType"].ToString()) //0普通，1图片，2标题
            {
                case "1":
                    NewsUrl = getNewsURL.getnewsReview(dr["NewsID"].ToString(), "news");//dim + SaveClassframe + "/" + dr["SavePath"].ToString() + "/" + dr["FileName"].ToString() + dr["FileEXName"].ToString();
                    NewsUrl = NewsUrl.Replace("//", "/");//NewsUrl.Replace("//", "/");
                    NewsUrl1 = "http://" + Request.ServerVariables["SERVER_NAME"] + dim + NewsUrl;// + dim + SaveClassframe + "/" + dr["SavePath"].ToString() + "/" + dr["FileName"].ToString() + dr["FileEXName"].ToString();
                    str_Row += "<tr>" + newLine;
                    string imgr = dim + dr["PicURL"].ToString().ToLower().Replace("{@dirfile}", Foosun.Config.UIConfig.dirFile);
                    str_Row += "<td rowspan=\"3\"><img src=\"" + imgr + "\" height=\"110\" width=\"140\" border=\"0\" /></td>" + newLine;
                    str_Row += "<td align=\"left\" width=\"100%\"><li><a href=\"" + NewsUrl + "\" target=\"_blank\"><span style=\"font-size:14px;\">" + dr["NewsTitle"].ToString().Replace(stags, "<span style=\"color:red\">" + stags + "</span>") + "</span></a> " + dr["CreatTime"].ToString() + "</li></td>" + newLine;
                    str_Row += "</tr>" + newLine;
                    str_Row += "<tr>" + newLine;
                    str_Row += "<td align=\"left\">" + Foosun.Common.Input.GetSubString(Foosun.Common.Input.FilterHTML((dr["Content"].ToString()).Replace("?", "？")), 200).Replace(stags, "<span style=\"color:red\">" + stags + "</span>") + "<div style=\"color:green\">" + NewsUrl1 + "</div></td>" + newLine;
                    str_Row += "</tr>" + newLine;
                    str_Row += "<tr>" + newLine;
                    str_Row += "<td align=\"left\" valign=\"top\" style=\"height:35px;\"><a href=\"" + FavNewsUrl.Replace("//", "/") + "\" target=\"_blank\">收藏</a> <a href=\"javascript:void(0);\" onclick=\"sendfriend('" + NewsUrl1 + "','" + dr["NewsTitle"].ToString() + "');\">推荐给朋友</a></td>" + newLine;
                    str_Row += "</tr>" + newLine;
                    break;
                case "0":
                    NewsUrl = getNewsURL.getnewsReview(dr["NewsID"].ToString(), "news");// dim + SaveClassframe + "/" + dr["SavePath"].ToString() + "/" + dr["FileName"].ToString() + dr["FileEXName"].ToString();
                    NewsUrl = NewsUrl.Replace("//", "/");// NewsUrl.Replace("//", "/");
                    NewsUrl1 = "http://" + Request.ServerVariables["SERVER_NAME"] + dim + NewsUrl; //+ SaveClassframe + "/" + dr["SavePath"].ToString() + "/" + dr["FileName"].ToString() + dr["FileEXName"].ToString();
                    str_Row += "<tr>" + newLine;
                    str_Row += "<td colspan=\"2\"><li><a href=\"" + NewsUrl + "\" target=\"_blank\"><span style=\"font-size:14px;\">" + dr["NewsTitle"].ToString().Replace(stags, "<span style=\"color:red\">" + stags + "</span>") + "</span></a> " + dr["CreatTime"].ToString() + "</li></td>" + newLine;
                    str_Row += "</tr>" + newLine;
                    str_Row += "<tr>" + newLine;
                    str_Row += "<td colspan=\"2\">" + Foosun.Common.Input.GetSubString(Foosun.Common.Input.FilterHTML((dr["Content"].ToString()).Replace("?", "？")), 200).Replace(stags, "<span style=\"color:red\">" + stags + "</span>") + "<div style=\"color:green\">" + NewsUrl1 + "</div></td>" + newLine;
                    str_Row += "</tr>" + newLine;
                    str_Row += "<tr>" + newLine;
                    str_Row += "<td colspan=\"2\" align=\"left\" valign=\"top\" style=\"height:35px;\"><a href=\"" + FavNewsUrl.Replace("//", "/") + "\" target=\"_blank\">收藏</a> <a href=\"javascript:void(0);\" onclick=\"sendfriend('" + NewsUrl1 + "','" + dr["NewsTitle"].ToString() + "');\">推荐给朋友</a></td>" + newLine;
                    str_Row += "</tr>" + newLine;
                    break;
                case "2":
                    NewsUrl = dr["URLaddress"].ToString();
                    str_Row += "<tr>" + newLine;
                    str_Row += "<td colspan=\"2\"><li><a href=\"" + NewsUrl + "\" target=\"_blank\"><span style=\"font-size:14px;\">" + dr["NewsTitle"].ToString() + "</span></a> " + dr["CreatTime"].ToString() + "</li><div style=\"color:green\">" + NewsUrl + "</div></td>" + newLine;
                    str_Row += "</tr>" + newLine;
                    str_Row += "<tr>" + newLine;
                    str_Row += "<td colspan=\"2\" align=\"left\" valign=\"top\" style=\"height:35px;\"><a href=\"" + FavNewsUrl.Replace("//", "/") + "\" target=\"_blank\">收藏</a> <a href=\"javascript:void(0);\" onclick=\"sendfriend('" + NewsUrl + "','" + dr["NewsTitle"].ToString() + "');\">推荐给朋友</a></td>" + newLine;
                    str_Row += "</tr>" + newLine;
                    break;
            }
        }
        return str_Row;
    }

    /// <summary>
    /// 获取搜索模板路径
    /// </summary>
    /// <returns>搜索模板路径</returns>
    protected string GetSearchTemplet()
    {
        string str_dirMana = Foosun.Config.UIConfig.dirDumm;
        string str_Templet = Foosun.Config.UIConfig.dirTemplet;  //获取模板路径

        if (str_dirMana != "" && str_dirMana != null && str_dirMana != string.Empty)//判断虚拟路径是否为空,如果不是则加上//
            str_dirMana = "//" + str_dirMana;
        string str_FilePath = Server.MapPath(str_dirMana + "\\" + str_Templet + "\\Content\\search.html");
        return str_FilePath;
    }

    /// <summary>
    /// 分页
    /// </summary>
    /// <param name="page">当前页码</param>
    /// <param name="Cnt">总记录数</param>
    /// <param name="pageCount">最大页数</param>
    /// <returns></returns>
    protected string ShowPage(int page, int Cnt, int pageCount)
    {
        string urlstr = "共" + Cnt.ToString() + "条记录,共" + pageCount.ToString() + "页,当前第" + page.ToString() + "页   ";
        urlstr += "<a href=\"javascript:GetSearchList('1');\" title=\"首页\" >首页</a> ";
        if ((page - 1) < 1)
        {
            urlstr += " 上一页 ";
        }
        else
        {
            urlstr += " <a href=\"javascript:GetSearchList('" + (page - 1) + "');\" title=\"上一页\" >上一页</a> ";
        }
        for (int i = page; i < (page + 10); i++)
        {
            if (i > pageCount)
            {
                break;
            }
            else
            {
                urlstr += "&nbsp;&nbsp;<a href=\"javascript:GetSearchList('" + i + "');\" title=\"上一页\" >" + i + "</a>&nbsp;&nbsp;";
            }
        }
        if (page == pageCount)
        {
            urlstr += " 下一页 ";
        }
        else
        {
            urlstr += " <a href=\"javascript:GetSearchList('" + (page + 1) + "');\" title=\"下一页\" >下一页</a> ";
        }
        urlstr += " <a href=\"javascript:GetSearchList('" + pageCount + "');\" title=\"尾页\">尾页</a> ";
        return urlstr;
    }
}
