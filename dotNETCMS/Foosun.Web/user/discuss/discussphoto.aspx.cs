//=====================================================================
//==                  (C)2007 Foosun Inc.By doNetCMS1.0              ==
//==                     Forum:bbs.foosun.net                        ==
//==                     WebSite:www.foosun.net                      ==
//==                 Address:No.109 HuiMin ST,.ChengDu,China         ==
//==                   Tel:86-28-85098980/66026180                   ==
//==                   QQ:655071,MSN:ikoolls@gmail.com               ==
//==                   Email:Service@foosun.cn                       ==
//==                      Code By WangZhenjiang                      ==
//=====================================================================
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
using Foosun.Model;

public partial class user_discuss_discussphoto : Foosun.Web.UI.UserPage
{
    //数据库连接
    Photo pho = new Photo();
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    #region 初始化
    Discuss dis = new Discuss();
    public string DisIDq = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";

        this.PageNavigator2.OnPageChange += new PageChangeHandler(PageNavigator2_PageChange);
        string DisID = Foosun.Common.Input.Filter(Request.QueryString["DisID"]);
        if (!Page.IsPostBack)
        {
            string PhotoalbumID = "";
            if (Foosun.Common.Input.Filter(Request.QueryString["PhotoalbumID"]) != null)
            {
                PhotoalbumID = Foosun.Common.Input.Filter(Request.QueryString["PhotoalbumID"]);
            }
            else
            {
                PageError("错误", "discussPhotoalbumlist.aspx?DisID=" + DisID + "");
            }
            DataTable dt_phopwd = pho.sel(PhotoalbumID);
            int cut_phopwd = dt_phopwd.Rows.Count;
            if (cut_phopwd == 0)
            {
                PageError("错误", "discussPhotoalbumlist.aspx?DisID=" + DisID + "");

            }
            if (dt_phopwd.Rows[0][0].ToString() != "" && Foosun.Global.Current.UserNum != dt_phopwd.Rows[0][1].ToString())
            {
                this.Panel1.Visible = true;
                this.Panel2.Visible = false;
            }
            else
            {
                this.Panel1.Visible = false;
                this.Panel2.Visible = true;
            }
            Show_cjlist(1);
        }
    }
    #endregion
    /// <summary>
    /// 绑定数据分页
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="PageIndex2"></param>
    /// 
    #region 绑定数据分页
    protected void PageNavigator2_PageChange(object sender, int PageIndex2)
    {
        Show_cjlist(PageIndex2);
    }
    protected void Show_cjlist(int PageIndex2)
    {
        string PhotoalbumID = Foosun.Common.Input.Filter(Request.QueryString["PhotoalbumID"].ToString());
        string DisIDs = Foosun.Common.Input.Filter(Request.QueryString["DisID"]);
        DisIDq = DisIDs;
        int i, j;
        SQLConditionInfo st = new SQLConditionInfo("@PhotoalbumID", PhotoalbumID);
        DataTable cjlistdts = Foosun.CMS.Pagination.GetPage(this.GetType().Name, PageIndex2, 10, out i, out j, st);
        this.PageNavigator2.PageCount = j;
        this.PageNavigator2.PageIndex = PageIndex2;
        this.PageNavigator2.RecordCount = i;
        string dirDumm = Foosun.Config.UIConfig.dirDumm.Trim();
        if (dirDumm != string.Empty)
        {
            dirDumm = "/" + dirDumm;
        }
        if (cjlistdts.Rows.Count > 0)
        {
            cjlistdts.Columns.Add("UserNamess", typeof(string));
            cjlistdts.Columns.Add("PhotoalbumName", typeof(string));
            cjlistdts.Columns.Add("PhotoUrls", typeof(string));
            foreach (DataRow r in cjlistdts.Rows)
            {
                r["UserNamess"] = dis.sel_60(r["UserNum"].ToString());
                r["PhotoalbumName"] = dis.sel_61(r["PhotoalbumID"].ToString());
                r["PhotoUrls"] = (r["PhotoUrl"].ToString().ToLower().Replace("{@userdirfile}", dirDumm + "/" + Foosun.Config.UIConfig.UserdirFile)).Replace("discuss/","").Replace("//","/");
            }
            if (this.Panel2.Visible == true)
            {
                sc.InnerHtml = Show_scs(PhotoalbumID, DisIDs);
            }
            else
            {
                sc.InnerHtml = Show_sc(PhotoalbumID, DisIDs);
            }
            DataList1.DataSource = cjlistdts;
            DataList1.DataBind();
        }
        else
        {
            sc.InnerHtml = Show_sc(PhotoalbumID, DisIDs);
            no.InnerHtml = Show_no();
            this.PageNavigator2.Visible = false;
        }
    }
    #endregion
    /// <summary>
    /// 验证相册秘密
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// 
    #region 验证相册秘密
    protected void open_Click(object sender, EventArgs e)
    {
        string DisIDs = Foosun.Common.Input.Filter(Request.QueryString["DisID"]);
        string PhotoalbumIDs = Foosun.Common.Input.Filter(Request.QueryString["PhotoalbumID"]);
        string pwd = Foosun.Common.Input.Filter(Request.Form["pwd"]);
        DataTable dt = pho.sel(PhotoalbumIDs);
        if (dt.Rows[0][0].ToString() != Foosun.Common.Input.MD5(pwd, true))
        {
            PageError("密码错误", "discussPhotoalbumlist.aspx?DisID=" + DisIDs + "");
        }
        else
        {
            this.Panel1.Visible = false;
            this.Panel2.Visible = true;
        }
    }
    #endregion
    /// <summary>
    /// 前台输出
    /// </summary>
    /// <param name="PhotoalbumIDs">相册编号</param>
    /// <param name="DisID">讨论组编号</param>
    /// <returns></returns>
    /// 
    #region 前台输出
    string Show_scs(string PhotoalbumIDs, string DisID)
    {
        string scs = "<table width=\"100%\"  border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"toptable\">";
        scs += "<tr><td height=\"1\" colspan=\"2\"></td></tr>";
        scs +=
        scs += "<tr><td width=\"57%\"  class=\"sysmain_navi\"  style=\"PADDING-LEFT: 14px\" >相册管理</td>";
        scs += "<td width=\"43%\"  class=\"topnavichar\"  style=\"PADDING-LEFT: 14px\" ><div align=\"left\">位置导航：<a href=\"../main.aspx\" target=\"sys_main\" class=\"list_link\">首页</a><img alt=\"\" src=\"../../sysImages/folder/navidot.gif\" border=\"0\" />相册管理</div></td></tr></table>";
        scs += "<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"3\" cellspacing=\"1\" class=\"Navitable\">";
        scs += "<tr> <td><span class=\"topnavichar\" style=\"PADDING-LEFT: 14px\"><a href=\"discussPhotoalbumlist.aspx?DisID=" + DisID + "\" class=\"menulist\">相册首页</a>　<a href=\"discussphoto_add.aspx?DisID=" + DisID + "\" class=\"menulist\">添加图片</a> &nbsp;&nbsp;<a href=\"discussphotoclass.aspx?DisID=" + DisID + "\" class=\"menulist\">相册分类</a> &nbsp;&nbsp;<a href=\"discussphotofilt.aspx?PhotoalbumID=" + PhotoalbumIDs + "&DisID=" + DisID + "\" class=\"menulist\">幻灯播放</a>&nbsp;&nbsp;<a href=\"discussPhotoalbum.aspx?DisID=" + DisID + "\" class=\"menulist\">添加相册</a></td></tr></table>";
        return scs;
    }
    string Show_sc(string PhotoalbumIDs, string DisID)
    {
        string sc = "<table width=\"100%\"  border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"toptable\">";
        sc += "<tr><td height=\"1\" colspan=\"2\"></td></tr>";
        sc +=
        sc += "<tr><td width=\"57%\"  class=\"sysmain_navi\"  style=\"PADDING-LEFT: 14px\" >相册管理</td>";
        sc += "<td width=\"43%\"  class=\"topnavichar\"  style=\"PADDING-LEFT: 14px\" ><div align=\"left\">位置导航：<a href=\"../main.aspx\" target=\"sys_main\" class=\"list_link\">首页</a><img alt=\"\" src=\"../../sysImages/folder/navidot.gif\" border=\"0\" />相册管理</div></td></tr></table>";
        sc += "<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"3\" cellspacing=\"1\" class=\"Navitable\">";
        sc += "<tr> <td><span class=\"topnavichar\" style=\"PADDING-LEFT: 14px\"><a href=\"discussPhotoalbumlist.aspx?DisID=" + DisID + "\" class=\"menulist\">相册首页</a>　<a href=\"discussphoto_add.aspx?DisID=" + DisID + "\" class=\"menulist\">添加图片</a> &nbsp;&nbsp;<a href=\"discussphotoclass.aspx?DisID=" + DisID + "\" class=\"menulist\">相册分类</a> &nbsp;&nbsp;<a href=\"discussPhotoalbum.aspx?DisID=" + DisID + "\" class=\"menulist\">添加相册</a></td></tr></table>";
        return sc;
    }
    string Show_no()
    {
        string nos = "<table border=0 width='98%' cellpadding=5 cellspacing=1 class='table' align=\"center\">";
        nos = nos + "<tr class='TR_BG_list'>";
        nos = nos + "<td class='navi_link'>没有数据</td>";
        nos = nos + "</tr>";
        nos = nos + "</table>";
        return nos;
    }
    protected string getPhotoURL(string url)
    {
        string s_URL = Foosun.Publish.CommonData.getUrl() + url.Replace("//", "/");
        return s_URL;
    }
    #endregion
}