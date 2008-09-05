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
using Foosun.CMS.Common;

public partial class manage_user_userlist : Foosun.Web.UI.ManagePage
{
    public manage_user_userlist()
    {
        Authority_Code = "U001";
    }
    UserList UL = new UserList();
    UserMisc rd = new UserMisc();
    protected void Page_Load(object sender, EventArgs e)
    {
        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_PageChange);
        if (!IsPostBack)
        {

            copyright.InnerHtml = CopyRight;            //获取版权信息
            Response.CacheControl = "no-cache";                        //设置页面无缓存
            #region 判断开始
            string getGroupNumber = Request.QueryString["GroupNumber"];
            if (getGroupNumber != null && getGroupNumber != "") { groupList.InnerHtml = groups(getGroupNumber); }
            else { groupList.InnerHtml = groups(""); }
            if (Foosun.Global.Current.SiteID == "0")
            {
                string getSiteID = Request.QueryString["SiteID"];
                if (getSiteID != null && getSiteID != "") { channelList.InnerHtml = SiteList(getSiteID); }
                else { channelList.InnerHtml = SiteList(SiteID); }
            }
            #endregion 判断结束
            string types = Request.QueryString["type"];
            if (types == "del")
            {
                int id = int.Parse(Request.QueryString["id"]);
                del(id);
            }
            string _userlock = "";
            string _group = "";
            string _iscerts = "";
            string _SiteID = "";
            string userlock = Request.QueryString["usertype"];
            string group = Request.QueryString["GroupNumber"];
            string iscerts = Request.QueryString["iscert"];
            string ReqSite = Request.QueryString["SiteID"];
            if (userlock != null && userlock != "") { _userlock = userlock.ToString(); }
            if (group != null && group != "") { _group = group.ToString(); }
            if (iscerts != null && iscerts != "") { _iscerts = iscerts.ToString(); }
            if (ReqSite != null && ReqSite != "") { _SiteID = SiteID.ToString(); }
            StartLoad(1, "", "", "", "", "", "", "", "", _userlock, _group, _iscerts, _SiteID);
        }
    }

    /// <summary>
    /// 得到站点列表
    /// </summary>
    /// <param name="SessionSiteID"></param>
    /// <returns></returns>
    protected string SiteList(string SessionSiteID)
    {
        string siteStr = "<select class=\"form\" name=\"SiteID\" id=\"SiteID\" onChange=\"getchanelInfo(this)\">\r";
        DataTable crs = rd.getSiteList();
        if (crs != null)
        {
            for (int i = 0; i < crs.Rows.Count; i++)
            {
                string getSiteID = SessionSiteID;
                string SiteID1 = crs.Rows[i]["ChannelID"].ToString();
                if (getSiteID != SiteID1) { siteStr += "<option value=\"" + crs.Rows[i]["ChannelID"] + "\">==" + crs.Rows[i]["CName"] + "==</option>\r"; }
                else { siteStr += "<option value=\"" + crs.Rows[i]["ChannelID"] + "\"  selected=\"selected\">==" + crs.Rows[i]["CName"] + "==</option>\r"; }
            }
            crs.Clear(); crs.Dispose();
        }
        siteStr += "</select>\r";
        return siteStr;
    }

    /// <summary>
    /// 获取会员列表转向菜单
    /// </summary>
    /// <param name="getGroupNumber"></param>
    /// <returns></returns>
    string groups(string getGroupNumber)
    {
        string liststr = "<select class=\"form\" name=\"grouplist\" id=\"grouplist\" onChange=\"getFormInfo(this)\">\r";
        liststr += "<option value=\"\">==所有会员组==</option>\r";
        DataTable rdr = UL.GroupList();
        if (rdr != null)
        {
            for (int i = 0; i < rdr.Rows.Count; i++)
            {
                string GroupNumbers = getGroupNumber.ToString();
                string GroupNumbers1 = rdr.Rows[i]["GroupNumber"].ToString();
                if (GroupNumbers != GroupNumbers1) { liststr += "<option value=\"" + rdr.Rows[i]["GroupNumber"] + "\">==" + rdr.Rows[i]["GroupName"] + "==</option>\r"; }
                else { liststr += "<option value=\"" + rdr.Rows[i]["GroupNumber"] + "\"  selected=\"selected\">==" + rdr.Rows[i]["GroupName"] + "==</option>\r"; }
            }
            rdr.Clear(); rdr.Dispose();
        }
        liststr += "</select>\r";
        return liststr;
    }

    //获得列表
    protected void PageNavigator1_PageChange(object sender, int PageIndex)
    {
        string _userlock = "";
        string _group = "";
        string _iscerts = "";
        string _SiteID = "";
        string userlock = Request.QueryString["usertype"];
        string group = Request.QueryString["GroupNumber"];
        string iscerts = Request.QueryString["iscert"];
        string ReqSite = Request.QueryString["SiteID"];
        if (userlock != null && userlock != "") { _userlock = userlock.ToString(); }
        if (group != null && group != "") { _group = group.ToString(); }
        if (iscerts != null && iscerts != "") { _iscerts = iscerts.ToString(); }
        if (ReqSite != null && ReqSite != "") { _SiteID = SiteID.ToString(); }
        if (iscerts == "1")
        {
            this.Authority_Code = "U009";
            this.CheckAdminAuthority();
        }
        StartLoad(PageIndex, null, null, null, null, null, null, null, null, _userlock, _group, _iscerts, _SiteID);
    }

    protected void StartLoad(int PageIndex, string UName, string RealName, string UserID, string Sex, string siPoint, string biPoint, string sgPoint, string bgPoint, string _userlock, string _group, string _iscerts, string _SiteID)
    {
        int i = 0;
        int j = 0;
        DataTable dt = UL.GetPage(UName, RealName, UserID, Sex, siPoint, biPoint, sgPoint, bgPoint, _userlock, _group, _iscerts, _SiteID, PageIndex, 20, out i, out j, null);
        this.PageNavigator1.PageCount = j;
        this.PageNavigator1.PageIndex = PageIndex;
        this.PageNavigator1.RecordCount = i;
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                dt.Columns.Add("userNames", typeof(string));
                dt.Columns.Add("lock", typeof(string));
                dt.Columns.Add("groupname", typeof(string));
                dt.Columns.Add("op", typeof(string));
                for (int k = 0; dt.Rows.Count > k; k++)
                {
                    //修改者：吴静岚 时间：2008-07-14 
                    //修改会员列表显示超级管理员并可执行删除操作问题 1
                    //<--开始
                    rootPublic rdr = new rootPublic();
                    if (dt.Rows[k]["username"].ToString().Equals(rdr.getUserName(UserNum)))
                    {
                        continue;
                    }
                    //结束  by wjl-->
                    dt.Rows[k]["op"] = "<a href=\"userinfo.aspx?id=" + dt.Rows[k]["id"].ToString() + "\" class=\"list_link\"><img src=\"../../sysimages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/edit.gif\" border=\"0\" alt='修改此会员'></a><a href=\"usermycom.aspx?UserNum=" + dt.Rows[k]["UserNum"] + "\" class=\"list_link\"><img src=\"../../sysimages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/comm.gif\" border=\"0\" alt='会员评论'></a>";

                    //修改者：吴静岚 时间：2008-07-21
                    //修改会员列表超级管理员信息显示问题 
                    //<--开始
                    if (!dt.Rows[k]["username"].ToString().Equals(rdr.getUserName(UserNum)))
                    {
                        //修改者：吴静岚 时间：2008-07-14 
                        //修改会员列表显示超级管理员并可执行删除操作问题 2
                        //<--开始
                        if (dt.Rows[k]["isAdmin"].ToString().Equals("0"))
                        {
                            dt.Rows[k]["op"] += "<a href=\"userlist.aspx?id=" + dt.Rows[k]["id"] + "&type=del\" class=\"list_link\" onClick=\"{if(confirm('确定要删除吗？')){return true;}return false;}\"> <img src=\"../../sysimages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/del.gif\" border=\"0\" alt='删除'></a>";
                        }//结束  by wjl-->
                    }
                    //修改者：吴静岚 时间：2008-07-21
                    //修改会员列表超级管理员信息显示问题 
                    //结束by wjl-->
                    
                    dt.Rows[k]["op"] += "<input type=\"checkbox\" name=\"uid\" value=\"" + dt.Rows[k]["id"] + "\" />&nbsp;<a href=\"pointhistory.aspx?UserNum=" + dt.Rows[k]["UserNum"].ToString() + "\" class=\"list_link\" title=\"查看此会员的冲值记录\">[冲值]</a>&nbsp;<a href=\"usermycom.aspx?UserNum=" + dt.Rows[k]["UserNum"].ToString() + "\" class=\"list_link\" title=\"查看此会员的评论！\">[评论]</a>";
                    if (dt.Rows[k]["isLock"].ToString() == "1") { dt.Rows[k]["lock"] = "<span class=\"tbie\">锁定</span>"; }
                    else { dt.Rows[k]["lock"] = "正常"; }
                    string result = UL.getGroupName(dt.Rows[k]["UserGroupNumber"].ToString());
                    if (result != null && result != "") { dt.Rows[k]["groupname"] = result; }
                    else { dt.Rows[k]["groupname"] = "--"; }
                    string _TmpAdmin = "普通会员";
                    string _classTF = dt.Rows[k]["username"].ToString();
                    if (dt.Rows[k]["isAdmin"].ToString() == "1")
                    {
                        _TmpAdmin = "管理员";
                        _classTF = "<span class=\"reshow\">" + dt.Rows[k]["username"].ToString() + "</span>";
                    }
                    dt.Rows[k]["userNames"] = "<a href=\"../../" + Foosun.Config.UIConfig.dirUser + "/showuser-" + dt.Rows[k]["username"].ToString() + ".aspx\" target=\"_blank\" title=\"" + _TmpAdmin + "&#13;点击查看[" + dt.Rows[k]["username"].ToString() + "]的信息.\" class=\"list_link\">" + _classTF + "</a>";
                }
            }
        }
        userlists.DataSource = dt;                              //设置datalist数据源
        userlists.DataBind();                                   //绑定数据源
    }

    protected void del(int id)
    {
        this.Authority_Code = "U002";
        this.CheckAdminAuthority();
        if (UL.singdel(id) == 0) { PageError("删除失败", "UserList.aspx"); }
        else { PageRight("删除成功", "UserList.aspx"); }
    }

    protected void islock(object sender, EventArgs e)
    {
        this.Authority_Code = "U004";
        this.CheckAdminAuthority();
        string uid = Request.Form["uid"];
        if (uid == "" || uid == null)
        { PageError("请选择一个会员进行操作<br />", ""); }
        if (UL.isLock(uid) == 0) { PageError("锁定失败", "UserList.aspx"); }
        else { PageRight("锁定成功", "UserList.aspx"); }
    }

    protected void unlock(object sender, EventArgs e)
    {
        this.Authority_Code = "U004";
        this.CheckAdminAuthority();
        string uid = Request.Form["uid"];
        if (uid == "" || uid == null) { PageError("请选择一个会员进行操作<br />", ""); }
        if (UL.unLock(uid) == 0) { PageError("解锁失败", "UserList.aspx"); }
        else { PageRight("解锁成功", "UserList.aspx"); }
    }

    protected void dels(object sender, EventArgs e)
    {
        this.Authority_Code = "U002";
        this.CheckAdminAuthority();

        string uid = Request.Form["uid"];
        if (uid == "" || uid == null)
        {
            PageError("请选择一个会员进行操作<br />", "");
        }

        if (UL.dels(uid) == 0)
        {
            PageError("批量删除失败", "UserList.aspx");
        }
        else
        {
            PageRight("批量删除成功", "UserList.aspx");
        }
    }

    protected void bIpoint(object sender, EventArgs e)
    {
        string uid = Request.Form["uid"];
        if (uid == "" || uid == null)
        {
            PageError("请选择一个会员进行操作<br />", "UserList.aspx");
        }
        else
        {
            Response.Redirect("useraction.aspx?PointType=bIpoint&uid=" + uid.Trim() + "");
        }
    }


    protected void sIpoint(object sender, EventArgs e)
    {
        string uid = Request.Form["uid"];
        if (uid == "" || uid == null)
        {
            PageError("请选择一个会员进行操作<br />", "UserList.aspx");
        }
        else
        {
            Response.Redirect("useraction.aspx?PointType=sIpoint&uid=" + uid.Trim() + "");
        }

    }

    protected void bGpoint(object sender, EventArgs e)
    {
        string uid = Request.Form["uid"];
        if (uid == "" || uid == null)
        {
            PageError("请选择一个会员进行操作<br />", "UserList.aspx");
        }
        else
        {
            Response.Redirect("useraction.aspx?PointType=bGpoint&uid=" + uid.Trim() + "");
        }
    }


    protected void sGpoint(object sender, EventArgs e)
    {
        string uid = Request.Form["uid"];
        if (uid == "" || uid == null)
        {
            PageError("请选择一个会员进行操作<br />", "UserList.aspx");
        }
        else
        {
            Response.Redirect("useraction.aspx?PointType=sGpoint&uid=" + uid.Trim() + "");
        }
    }



    protected void Button8_ServerClick(object sender, EventArgs e)
    {
        this.Authority_Code = "U010";
        this.CheckAdminAuthority();
        string ReqName = Request.Form["username"];
        string RealName = Request.Form["realname"];
        string ReqUNum = Request.Form["userNum"];
        string Sex = Request.Form["sex"];
        string siPoint = Request.Form["ipoint"];
        string biPoint = Request.Form["bipoint"];
        string sgPoint = Request.Form["gpoint"];
        string bgPoint = Request.Form["bgpoint"];
        string _userlock = Request.QueryString["userlock"];
        string _group = Request.QueryString["GroupNumber"];
        string _iscerts = Request.QueryString["iscert"];
        string _SiteID = Request.QueryString["SiteID"];
        StartLoad(1, ReqName, RealName, ReqUNum, Sex, siPoint, biPoint, sgPoint, bgPoint, _userlock, _group, _iscerts, _SiteID);
    }
}
