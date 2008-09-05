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

public partial class manage_user_icard : Foosun.Web.UI.ManagePage
{
    public manage_user_icard()
    {
        Authority_Code = "U024";
    }
    UserMisc rd = new UserMisc();
    UserList UL = new UserList();
    protected void Page_Load(object sender, EventArgs e)
    {
        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_PageChange);
        if (!IsPostBack)
        {

            copyright.InnerHtml = CopyRight;            //获取版权信息
            Response.CacheControl = "no-cache";                        //设置页面无缓存
            #region 获得频道列表
            if (SiteID == "0")
            {
                string getSiteID = Request.QueryString["SiteID"];
                if (getSiteID != null && getSiteID != "")
                {
                    channelList.InnerHtml = SiteList(getSiteID);
                }
                else
                {
                    channelList.InnerHtml = SiteList(SiteID);
                }
            }
            #endregion 获得频道列表结束
            if (Request.QueryString["Type"] == "Del")
            {
                dels(Foosun.Common.Input.Filter(Request.QueryString["id"]));
            }
            #region 获得状态参数
            string islock = Request.QueryString["islock"];
            string isuse = Request.QueryString["isuse"];
            string isbuy = Request.QueryString["isbuy"];
            string timeout = Request.QueryString["timeout"];
            string ReqSite = Request.QueryString["SiteID"];
            string _islock = "";
            string _isuse = "";
            string _isbuy = "";
            string _timeout = "";
            string _SiteID = "";
            if (islock != "" && islock != null) { _islock = islock.ToString(); }
            if (isuse != "" && isuse != null) { _isuse = isuse.ToString(); }
            if (isbuy != "" && isbuy != null) { _isbuy = isbuy.ToString(); }
            if (timeout != "" && timeout != null) { _timeout = timeout.ToString(); }
            if (ReqSite != "" && ReqSite != null) { _SiteID = SiteID.ToString(); }
            #endregion 获得状态参数
            StartLoad(1, _islock, _isuse, _isbuy, _timeout, _SiteID, "", "");
        }
    }

    string SiteList(string SessionSiteID)
    {
        string siteStr = "<select class=\"form\" name=\"SiteID\" id=\"SiteID\" onChange=\"getchanelInfo(this)\">\r";
        DataTable crs = rd.getSiteList();
        if (crs != null)
        {
            for (int i = 0; i < crs.Rows.Count; i++)
            {
                string getSiteID = SessionSiteID;
                string SiteID1 = crs.Rows[i]["ChannelID"].ToString();
                if (getSiteID != SiteID1)
                {
                    siteStr += "<option value=\"" + crs.Rows[i]["ChannelID"] + "\">==" + crs.Rows[i]["CName"] + "==</option>\r";
                }
                else
                {
                    siteStr += "<option value=\"" + crs.Rows[i]["ChannelID"] + "\"  selected=\"selected\">==" + crs.Rows[i]["CName"] + "==</option>\r";
                }
            }
        }
        //}
        siteStr += "</select>\r";
        return siteStr;
    }

    /// <summary>
    /// 删除所有点卡
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void delAll(object sender, EventArgs e)
    {
        this.Authority_Code = "U027";
        this.CheckAdminAuthority();
        rd.delALLCARD();
        PageRight("删除所有点卡成功!", "icard.aspx");
    }
    /// <summary>
    /// dels 的摘要说明
    /// 删除点卡
    /// </summary>
    protected void dels(string aId)
    {
        this.Authority_Code = "U027";
        this.CheckAdminAuthority();
        if (aId != null && aId != "")
        {
            rd.ICarddels(aId);
            PageRight("删除点卡成功。", "icard.aspx");
        }
        else
        {
            PageError("请选择至少一个点卡<br />", "icard.aspx");
        }
    }

    /// <summary>
    /// delmul 的摘要说明
    /// 删除多个点卡传递中间函数
    /// </summary>
    protected void delmul(object sender, EventArgs e)
    {
        this.Authority_Code = "U027";
        this.CheckAdminAuthority();
        string ids = Request.Form["cid"];
        dels(ids);
    }

    /// <summary>
    /// islock 的摘要说明
    /// 锁定多个点卡传递中间函数
    /// </summary>
    protected void islock(object sender, EventArgs e)
    {
        this.Authority_Code = "U027";
        this.CheckAdminAuthority();
        string ids = Request.Form["cid"];
        lockActions(ids, 1);
    }

    /// <summary>
    /// unlock 的摘要说明
    /// 取消锁定多个点卡传递中间函数
    /// </summary>
    protected void unlock(object sender, EventArgs e)
    {
        this.Authority_Code = "U027";
        this.CheckAdminAuthority();
        string ids = Request.Form["cid"];
        lockActions(ids, 0);
    }

    /// <summary>
    /// timeout 的摘要说明
    /// 设置为过期传递中间函数
    /// </summary>
    protected void timeout(object sender, EventArgs e)
    {
        this.Authority_Code = "U027";
        this.CheckAdminAuthority();
        string ids = Request.Form["cid"];
        lockActions(ids, 2);
    }


    /// <summary>
    /// isuse 的摘要说明
    /// 设置为已使用传递中间函数
    /// </summary>
    protected void isuse(object sender, EventArgs e)
    {

        string ids = Request.Form["cid"];
        lockActions(ids, 4);
    }

    protected void unisuse(object sender, EventArgs e)
    {
        this.Authority_Code = "U027";
        this.CheckAdminAuthority();
        string ids = Request.Form["cid"];
        lockActions(ids, 5);
    }

    /// <summary>
    /// isbuy 的摘要说明
    /// 设置为已购买传递中间函数
    /// </summary>
    protected void isbuy(object sender, EventArgs e)
    {
        this.Authority_Code = "U027";
        this.CheckAdminAuthority();
        string ids = Request.Form["cid"];
        lockActions(ids, 6);
    }

    protected void unisbuy(object sender, EventArgs e)
    {
        this.Authority_Code = "U027";
        this.CheckAdminAuthority();
        string ids = Request.Form["cid"];
        lockActions(ids, 7);
    }

    /// <summary>
    /// lockActions 的摘要说明
    /// 锁定/解锁动作函数
    /// intlock 0为取消锁定，1为锁定，2为设置为过期，3为设置为不过期，4为设置为已使用，5设置为未使用
    /// </summary>
    protected void lockActions(string cId, int intlock)
    {
        this.Authority_Code = "U027";
        this.CheckAdminAuthority();
        if (cId != null && cId != "")
        {
            string lockstr = "";
            switch (intlock)
            {
                case (0):
                    lockstr = " set islock=0";
                    break;
                case (1):
                    lockstr = " set islock=1";
                    break;
                case (2):
                    lockstr = "000000000";
                    break;
                case (4):
                    lockstr = " set isuse=1";
                    break;
                case (5):
                    lockstr = " set isuse=0";
                    break;
                case (6):
                    lockstr = " set isbuy=1";
                    break;
                case (7):
                    lockstr = " set isbuy=0";
                    break;
                default:
                    break;
            }

            rd.ICardLockAction(cId, lockstr);
            PageRight("更新点卡成功。", "icard.aspx");
        }
        else
        {
            PageError("请选择至少一个点卡<br />", "icard.aspx");
        }
    }


    /// <summary>
    /// PageNavigator1_PageChange 的摘要说明
    /// 分页加载函数
    /// </summary>
    protected void PageNavigator1_PageChange(object sender, int PageIndex)
    {
        string islock = Request.QueryString["islock"];
        string isuse = Request.QueryString["isuse"];
        string isbuy = Request.QueryString["isbuy"];
        string timeout = Request.QueryString["timeout"];
        string ReqSite = Request.QueryString["SiteID"];
        string _islock = "";
        string _isuse = "";
        string _isbuy = "";
        string _timeout = "";
        string _SiteID = "";
        if (islock != "" && islock != null) { _islock = islock.ToString(); }
        if (isuse != "" && isuse != null) { _isuse = isuse.ToString(); }
        if (isbuy != "" && isbuy != null) { _isbuy = isbuy.ToString(); }
        if (timeout != "" && timeout != null) { _timeout = timeout.ToString(); }
        if (ReqSite != "" && ReqSite != null) { _SiteID = SiteID.ToString(); }
        StartLoad(PageIndex, _islock, _isuse, _isbuy, _timeout, _SiteID, "", "");
    }

    /// <summary>
    /// PageNavigator1_PageChange 的摘要说明
    /// 分页加载列表函数
    /// </summary>
    protected void StartLoad(int PageIndex, string _islock, string _isuse, string _isbuy, string _timeout, string _SiteID, string cardnumber, string cardpassword)
    {
        int i, j;
        DataTable dt = rd.GetPage(_islock, _isuse, _isbuy, _timeout, _SiteID, cardnumber, cardpassword, PageIndex, 20, out i, out j, null);
        this.PageNavigator1.PageCount = j;
        this.PageNavigator1.PageIndex = PageIndex;
        this.PageNavigator1.RecordCount = i;
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                //----------------------------------------添加列------------------------------------------------
                dt.Columns.Add("op", typeof(string));
                dt.Columns.Add("islockStr", typeof(string));
                dt.Columns.Add("isBuyStr", typeof(string));
                dt.Columns.Add("isUseStr", typeof(string));
                dt.Columns.Add("isTimeOut", typeof(string));
                dt.Columns.Add("CardPassWords", typeof(string));
                dt.Columns.Add("UserNums", typeof(string));
                //----------------------------------------添加列结束--------------------------------------------
                for (int k = 0; dt.Rows.Count > k; k++)
                {
                    if (dt.Rows[k]["UserNum"].ToString().Trim() != "")
                    {
                        Foosun.CMS.Common.rootPublic pd = new Foosun.CMS.Common.rootPublic();
                        dt.Rows[k]["UserNums"] = "<a href=\"../../" + Foosun.Config.UIConfig.dirUser + "/showuser-" + pd.getUserName(dt.Rows[k]["UserNum"].ToString()) + ".aspx\" target=\"_blank\" class=\"list_link\" title=\"此点卡已经被" + pd.getUserName(dt.Rows[k]["UserNum"].ToString()) + "使用或者购买\"><font color=\"red\">[*]</font></a>";
                    }
                    dt.Rows[k]["op"] = "<a href=\"iCardEdit.aspx?Id=" + dt.Rows[k]["id"] + "\" class='list_link'><img src=\"../../sysimages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/edit.gif\" border=\"0\" alt='修改'></a><a href=\"iCard.aspx?Type=Del&id=" + dt.Rows[k]["id"] + "\" class='list_link'><img src=\"../../sysimages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/del.gif\" border=\"0\" alt='删除' onClick=\"{if(confirm('确定要删除吗？')){return true;}return false;}\"></a><input type=\"checkbox\" name=\"cid\" value=\"" + dt.Rows[k]["id"] + "\" />";
                    string Pstr = dt.Rows[k]["CardPassWord"].ToString();
                    dt.Rows[k]["CardPassWords"] = FSSecurity.FDESEncrypt(Pstr, 0);
                    if (dt.Rows[k]["islock"].ToString() == "1")
                    {
                        dt.Rows[k]["islockStr"] = "<img src=\"../../sysImages/folder/no.gif\" alt=\"已锁定\" border=\"0\" />";
                    }
                    else
                    {
                        dt.Rows[k]["islockStr"] = "<img src=\"../../sysImages/folder/yes.gif\" alt=\"正常\" border=\"0\" /></span>";
                    }
                    if (dt.Rows[k]["isBuy"].ToString() == "1")
                    {
                        dt.Rows[k]["isBuyStr"] = "<img src=\"../../sysImages/folder/yes.gif\" border=\"0\" /></span>";
                    }
                    else
                    {
                        dt.Rows[k]["isBuyStr"] = "<img src=\"../../sysImages/folder/no.gif\" border=\"0\" />";
                    }
                    if (dt.Rows[k]["isUse"].ToString() == "1")
                    {
                        dt.Rows[k]["isUseStr"] = "<img alt=\"使用者：" + dt.Rows[k]["UserNum"].ToString() + "\" src=\"../../sysImages/folder/yes.gif\" border=\"0\" /></span>";
                    }
                    else
                    {
                        dt.Rows[k]["isUseStr"] = "<img src=\"../../sysImages/folder/no.gif\" border=\"0\" />";
                    }

                    if (dt.Rows[k]["TimeOutDate"].ToString().Split('-')[0] != "2099")
                    {
                        DateTime dtime = DateTime.Parse(dt.Rows[k]["TimeOutDate"].ToString());
                        DateTime dnTime = System.DateTime.Now;
                        if (DateTime.Parse(dt.Rows[k]["TimeOutDate"].ToString()) >= System.DateTime.Now)
                        {
                            TimeSpan days = dtime - dnTime;
                            int daysTF = days.Days;
                            string daysTFchar = "";
                            if (daysTF > 0)
                            {
                                daysTFchar = daysTF + "天" + days.Hours + "小时";
                            }
                            else
                            {
                                daysTFchar = days.Hours + "小时";
                            }
                            dt.Rows[k]["isTimeOut"] = "未过期[" + daysTFchar + "]";
                        }
                        else
                        {
                            dt.Rows[k]["isTimeOut"] = "<span class=\"tbie\">已过期</span>";
                        }
                    }
                    else
                    {
                        dt.Rows[k]["isTimeOut"] = "永不过期";
                    }
                }
            }
        }
        cardlists.DataSource = dt;                              //设置datalist数据源
        cardlists.DataBind();                                   //绑定数据源
    }


    protected void DataList1_ItemCommand(object sender, EventArgs e)
    {

    }
    /// <summary>
    /// search_button_Click 的摘要说明
    /// 获取搜索
    /// </summary>
    protected void search_button_Click(object sender, EventArgs e)
    {
        #region 获得表单搜索
        string cardnumber = "";
        string cardpassword = "";
        string islock = Request.QueryString["islock"];
        string isuse = Request.QueryString["isuse"];
        string isbuy = Request.QueryString["isbuy"];
        string timeout = Request.QueryString["timeout"];
        string SiteID = Request.QueryString["SiteID"];
        string _islock = "";
        string _isuse = "";
        string _isbuy = "";
        string _timeout = "";
        string _SiteID = "";
        if (islock != "" && islock != null) { _islock = islock.ToString(); }
        if (isuse != "" && isuse != null) { _isuse = isuse.ToString(); }
        if (isbuy != "" && isbuy != null) { _isbuy = isbuy.ToString(); }
        if (timeout != "" && timeout != null) { _timeout = timeout.ToString(); }
        if (SiteID != "" && SiteID != null) { _SiteID = SiteID.ToString(); }
        if (Request.Form["cardnumber"] != "" && Request.Form["cardnumber"] != null)
        {
            cardnumber = Request.Form["cardnumber"];
        }
        if (Request.Form["cardpassword"] != "" && Request.Form["cardpassword"] != null)
        {
            cardpassword = FSSecurity.FDESEncrypt(Request.Form["cardpassword"], 1);
        }
        StartLoad(1, _islock, _isuse, _isbuy, _timeout, _SiteID, cardnumber, cardpassword);
        #endregion 获得表单搜索
    }
}
