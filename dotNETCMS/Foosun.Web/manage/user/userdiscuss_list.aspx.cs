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
using Foosun.CMS.Common;
using Foosun.Model;

public partial class manage_user_discuss : Foosun.Web.UI.ManagePage
{
    public manage_user_discuss()
    {
        Authority_Code = "U015";
    }
    /// <summary>
    /// 初始化
    /// </summary> 
    #region 初始化
    Discuss dis = new Discuss();
    UserList UL = new UserList();
    Foosun.CMS.UserMisc rd = new Foosun.CMS.UserMisc();
    protected void Page_Load(object sender, EventArgs e)
    {

        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_PageChange);
        Response.CacheControl = "no-cache";
        if (!IsPostBack)
        {
            if (SiteID == "0")
            {
                string getSiteID = Request.QueryString["SiteID"];
                if (getSiteID != null && getSiteID != "")
                {
                    channelList.InnerHtml = SiteList(getSiteID);
                }
                else
                {
                    channelList.InnerHtml = SiteList(Foosun.Global.Current.SiteID);
                }
            }
            Showu_discusslist(1, "");
        }
        string Type = Request.QueryString["Type"];  //取得操作类型
        string ID = "";
        if (Request.QueryString["ID"] != "" && Request.QueryString["ID"] != null)
        {
            ID = Foosun.Common.Input.Filter(Request.QueryString["ID"]);  //取得需要操作的稿件ID
        }

        switch (Type)
        {
            case "del":          //删除
                del(ID);
                break;
            case "PDel":            //批量删除
                PDel();
                break;
            default:
                break;
        }
    }
    #endregion
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
    /// 数据绑定
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="PageIndex"></param>
    #region 数据绑定
    protected void PageNavigator1_PageChange(object sender, int PageIndex)
    {
        Showu_discusslist(PageIndex, null);
    }
    protected void Showu_discusslist(int PageIndex, string titlem)//显示所有讨论组
    {
        no.InnerHtml = "";
        int ig, js;
        DataTable dts = null;
        string RequestSiteId = Request.QueryString["SiteID"];
        if (RequestSiteId != null || RequestSiteId == string.Empty)
        {
            if (SiteID == "0")
            {
                string RequestSiteIds = RequestSiteId.ToString();
                SQLConditionInfo[] st = new SQLConditionInfo[2];
                st[0] = new SQLConditionInfo("@titlem", "%" + titlem + "%");
                st[1] = new SQLConditionInfo("@RequestSiteId", RequestSiteIds);
                dts = Foosun.CMS.Pagination.GetPage("manage_user_discuss_1_aspx", PageIndex, 20, out ig, out js, st);
            }
            else
            {
                SQLConditionInfo sts = new SQLConditionInfo("@titlem", "%" + titlem + "%");
                dts = Foosun.CMS.Pagination.GetPage("manage_user_discuss_3_aspx", PageIndex, 20, out ig, out js, sts);
            }
        }
        else
        {
            SQLConditionInfo sts = new SQLConditionInfo("@titlem", "%" + titlem + "%");
            dts = Foosun.CMS.Pagination.GetPage("manage_user_discuss_2_aspx", PageIndex, 20, out ig, out js, sts);
        }
        this.PageNavigator1.PageCount = js;
        this.PageNavigator1.PageIndex = PageIndex;
        this.PageNavigator1.RecordCount = ig;
        if (dts != null && dts.Rows.Count != 0)
        {
            dts.Columns.Add("cutDisID", typeof(string));
            dts.Columns.Add("idc", typeof(string));
            dts.Columns.Add("Creatimes", typeof(string));
            DataTable selectDisID = dis.sel_6();
            int p;
            foreach (DataRow s in dts.Rows)
            {
                p = (int)selectDisID.Compute("Count(DisID)", "DisID='" + s["DisID"].ToString() + "'");

                s["cutDisID"] = p;
                s["idc"] = "<a href=\"#\" onclick=\"javascript:del('" + s["DisID"].ToString() + "');\" class=\"list_link\" title=\"删除\"><img src=\"../../sysImages/folder/no.gif\" border=\"0\" alt=\"删除\" /></a><input name=\"Checkbox1\" type=\"checkbox\" value=" + s["DisID"].ToString() + "  runat=\"server\" />";
                s["Creatimes"] = DateTime.Parse(s["Creatime"].ToString()).ToShortDateString();

            }
            DataList1.Visible = true;
            DataList1.DataSource = dts;
            DataList1.DataBind();
            DataList1.Dispose();
            pdel.InnerHtml = Show_pdel();
        }

        else
        {
            no.InnerHtml = Show_no();
            this.PageNavigator1.Visible = false;
            DataList1.Visible = false;
        }
    }
    #endregion
    string Show_no()
    {
        string nos = "<table border=0 width='98%' align=center cellpadding=5 cellspacing=1 class='table'>";
        nos = nos + "<tr class='TR_BG_list'>";
        nos = nos + "<td class='navi_link'>没有数据</td>";
        nos = nos + "</tr>";
        nos = nos + "</table>";
        return nos;
    }
    string Show_pdel()
    {
        string pdel = "<a href=\"javascript:PDel();\" class=\"topnavichar\">批量删除</a>";
        return pdel;
    }
    /// <summary>
    /// 批量删除
    /// </summary>
    /// 
    #region 批量删除
    protected void PDel()
    {
        string checkboxq = Request.Form["Checkbox1"];
        rootPublic rd = new rootPublic();

        if (checkboxq == null || checkboxq == String.Empty)
        {
            PageError("请先选择要锁定的稿件!", "");
        }
        else
        {
            string[] chSplit = checkboxq.Split(',');
            for (int i = 0; i < chSplit.Length; i++)
            {
                if (chSplit[i] != "on")
                {
                    if (dis.Delete_2(chSplit[i]) == 0)
                    {
                        rd.SaveUserAdminLogs(1, 1, UserNum, "删除讨论组", "删除失败");
                        PageError("批量删除失败", "");
                        break;
                    }
                }
            }
            rd.SaveUserAdminLogs(1, 1, UserNum, "删除讨论组", "删除成功");
            PageRight("批量删除成功", "");
        }

    }
    #endregion
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="ID"></param>
    /// 
    #region 删除
    protected void del(string ID)
    {
        rootPublic rd = new rootPublic();
        if (dis.Delete_2(ID) == 0)
        {
            rd.SaveUserAdminLogs(1, 1, UserNum, "删除讨论组", "删除失败");
            PageError("批量删除失败", "");
        }
        else
        {
            rd.SaveUserAdminLogs(1, 1, UserNum, "删除讨论组", "删除成功");
            PageRight("删除成功!", "");
        }
    }
    #endregion
    protected void selss_Click(object sender, EventArgs e)
    {
        string nm = Foosun.Common.Input.Filter(Request.Form["dicname"]);
        Showu_discusslist(1, nm);
    }
}