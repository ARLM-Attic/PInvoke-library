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

public partial class manage_user_discussclass : Foosun.Web.UI.ManagePage
{
    public manage_user_discussclass()
    {
        Authority_Code = "U017";
    }
    Discuss dis = new Discuss();
    UserList UL = new UserList();
    UserMisc rd = new UserMisc();
    protected void Page_Load(object sender, EventArgs e)
    {

        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_PageChange);
        Response.CacheControl = "no-cache";
        if (!IsPostBack)
        {
            Showu_discusslist(1, "");
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
            else
            {
                channelList.InnerHtml = SiteList(SiteID);
            }
        }
        string Type = Request.QueryString["Type"];  //取得操作类型
        string ID = Request.QueryString["ID"];  //取得需要操作的稿件ID

        switch (Type)
        {
            case "del":          //删除
                del(Foosun.Common.Input.Filter(ID.ToString()));
                break;
            case "PDel":            //批量删除
                PDel();
                break;
            default:
                break;
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
    protected void PageNavigator1_PageChange(object sender, int PageIndex)
    {
        Showu_discusslist(PageIndex, null);
    }
    protected void Showu_discusslist(int PageIndex, string titlem)//显示所有讨论组列表
    {

        no.InnerHtml = "";
        int ig, js;
        DataTable dts = null;
        string RequestSiteId = Request.QueryString["SiteID"];
        if (RequestSiteId != null || RequestSiteId == string.Empty)
        {
            string RequestSiteIds = RequestSiteId.ToString();
            SQLConditionInfo[] st = new SQLConditionInfo[2];
            st[0] = new SQLConditionInfo("@titlem", "%" + titlem + "%");
            st[1] = new SQLConditionInfo("@RequestSiteId", RequestSiteIds);
            dts = Foosun.CMS.Pagination.GetPage("manage_user_discussclass_1_aspx", PageIndex, 10, out ig, out js, st);
        }
        else
        {
            SQLConditionInfo sts = new SQLConditionInfo("@titlem", "%" + titlem + "%");
            dts = Foosun.CMS.Pagination.GetPage("manage_user_discussclass_2_aspx", PageIndex, 10, out ig, out js, sts);
        }



        this.PageNavigator1.PageCount = js;
        this.PageNavigator1.PageIndex = PageIndex;
        this.PageNavigator1.RecordCount = ig;
        if (dts != null && dts.Rows.Count != 0)
        {
            dts.Columns.Add("Contents", typeof(string));
            dts.Columns.Add("idc", typeof(string));
            foreach (DataRow s in dts.Rows)
            {
                if (s["Content"].ToString().Length > 20)
                {
                    s["Contents"] = s["Content"].ToString().Substring(20);
                }
                else
                {
                    s["Contents"] = s["Content"].ToString();
                }
                s["idc"] = "<a href=\"#\" onclick=\"javascript:del('" + s["DcID"].ToString() + "');\" class=\"list_link\" title=\"删除\"><img src=\"../../sysImages/folder/no.gif\" border=\"0\" alt=\"删除\" /></a><input name=\"Checkbox1\" type=\"checkbox\" value=" + s["DcID"].ToString() + "  runat=\"server\" />";

            }
            DataList1.Visible = true;
            DataList1.DataSource = dts;
            DataList1.DataBind();
            pdel.InnerHtml = Show_pdel();
        }

        else
        {
            no.InnerHtml = Show_no();
            this.PageNavigator1.Visible = false;
            DataList1.Visible = false;
        }

    }
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
    protected void PDel()
    {
        rootPublic rd = new rootPublic();
        string checkboxq = Request.Form["Checkbox1"];
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
                    if (dis.Delete_1(chSplit[i]) == 0)
                    {
                        rd.SaveUserAdminLogs(1, 1, UserNum, "讨论组分类", "删除失败");
                        PageError("批量删除失败", "");
                        break;
                    }
                }
            }
            rd.SaveUserAdminLogs(1, 1, UserNum, "讨论组分类", "删除成功");
            PageRight("批量删除成功", "");
        }

    }
    protected void del(string ID)
    {
        rootPublic rd = new rootPublic();
        if (dis.Delete_1(ID) == 0)
        {
            rd.SaveUserAdminLogs(1, 1, UserNum, "讨论组分类", "删除失败");
            PageError("批量删除失败", "");
        }
        else
        {
            rd.SaveUserAdminLogs(1, 1, UserNum, "讨论组分类", "删除成功");
            PageRight("删除成功!", "");
        }
    }
    protected void selss_Click(object sender, EventArgs e)
    {
        string nm = Foosun.Common.Input.Filter(Request.Form["dicname"]);
        Showu_discusslist(1, nm);
    }
}



