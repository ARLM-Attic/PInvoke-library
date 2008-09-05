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

public partial class manage_user_pointhistory : Foosun.Web.UI.ManagePage
{
    public manage_user_pointhistory()
    {
        Authority_Code = "U028";
    }
    UserList UL = new UserList();
    Info inf = new Info();
    UserMisc rd = new UserMisc();
    Foosun.CMS.Common.rootPublic pd = new Foosun.CMS.Common.rootPublic();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";  
        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_PageChange);
        string type = Request.QueryString["type"];
        if (type == null)
        {
            type = "0";
        }
        copyright.InnerHtml = CopyRight;
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
            StartLoad(1, type, "");
        }
       
        string Types = Request.QueryString["Types"];  //取得操作类型
        string ID = Request.QueryString["ID"];  //取得需要操作的稿件ID
        switch (Types)
        {
            case "del":          //删除
                this.Authority_Code = "U029";
                this.CheckAdminAuthority();
                del(Foosun.Common.Input.Filter(ID.ToString()));
                break;
            case "PDel":            //批量删除
                this.Authority_Code = "U029";
                this.CheckAdminAuthority();
                PDel();
                break;
            default:
                break;
        }
    }
    string SiteList(string SessionSiteID)
    {
        string siteStr = "<select name=\"SiteID\" id=\"SiteID\" onChange=\"getchanelInfo(this)\">\r";
        DataTable crs = rd.getSiteList();
        if (crs != null)
        {
            for (int i = 0; i < crs.Rows.Count; i++)
            {
                string getSiteID = SessionSiteID;
                string SiteID1 = crs.Rows[i]["ChannelID"].ToString();

                if (getSiteID != SiteID1){siteStr += "<option value=\"" + crs.Rows[i]["ChannelID"] + "\">==" + crs.Rows[i]["CName"] + "==</option>\r";}
                else{siteStr += "<option value=\"" + crs.Rows[i]["ChannelID"] + "\"  selected=\"selected\">==" + crs.Rows[i]["CName"] + "==</option>\r";}
            }
        }
        siteStr += "</select>\r";
        return siteStr;
    }

    
    protected void PageNavigator1_PageChange(object sender, int PageIndex)
    {
        string type = Request.QueryString["type"];
        if (type == null)
        {
            type = "0";
        }
        StartLoad(PageIndex, type, "");
    }

    protected void StartLoad(int PageIndex,string typep,string UM)
    {
        no.InnerHtml = "";
        int i, j;
        string sel_UM = "";
        if (UM != string.Empty)
        {
            sel_UM = pd.getUserNameUserNum(UM);
        }
        string strUserNum = "";
        if (Request.QueryString["UserNum"] != "" && Request.QueryString["UserNum"] != null)
        {
            strUserNum = Request.QueryString["UserNum"].ToString();
        }
        string RequestSiteId = Request.QueryString["SiteID"];
        DataTable dt = inf.GetPagepoi(typep, UM, sel_UM, RequestSiteId, strUserNum, PageIndex, 20, out i, out j, null);
        this.PageNavigator1.PageCount = j;
        this.PageNavigator1.PageIndex = PageIndex;
        this.PageNavigator1.RecordCount = i;
        if (dt != null&&dt.Rows.Count > 0)
        {
            dt.Columns.Add("ghtypes", typeof(string));
            dt.Columns.Add("Moneys", typeof(string));
            dt.Columns.Add("UserName", typeof(string));
            dt.Columns.Add("op", typeof(string));
            for (int k = 0; dt.Rows.Count > k; k++)
            {
                int ghtype = int.Parse(dt.Rows[k]["ghtype"].ToString());
                if (ghtype == 1)
                {
                    dt.Rows[k]["ghtypes"] = "收入";
                }
                else
                {
                    dt.Rows[k]["ghtypes"] = "支出";
                }
                decimal Money1 = decimal.Parse(dt.Rows[k]["Money"].ToString());
                dt.Rows[k]["Moneys"] = (String.Format("{0:C}", Money1));
                DataTable des = inf.sel_21(dt.Rows[k]["UserNUM"].ToString());
                int cuts1 = des.Rows.Count;
                if (cuts1!=0)
                {
                    dt.Rows[k]["UserName"] = "<a href=\"../../" + Foosun.Config.UIConfig.dirUser + "/showuser-" + des.Rows[0]["UserName"].ToString() + ".aspx\" target=\"_blank\" class=\"list_link\">" + des.Rows[0]["UserName"].ToString() + "</a>";
                }
                else 
                {
                    dt.Rows[k]["UserName"] = "会员已被删除";
                }
                dt.Rows[k]["op"] = "<a href=\"#\" onclick=\"javascript:del('" + dt.Rows[k]["GhID"].ToString() + "');\" title=\"删除\");\" class=\"list_link\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/del.gif\" border=\"0\" alt=\"删除\" /></a><input name=\"Checkbox1\" type=\"checkbox\" value=" + dt.Rows[k]["GhID"].ToString() + "  runat=\"server\" /></td>";
            }
            userlists.Visible = true;
        }      
        else
        {
            dels.InnerHtml = Show_del();
            no.InnerHtml = Show_no();
            this.PageNavigator1.Visible = false;
        }

        userlists.DataSource = dt;
        userlists.DataBind();
        userlists.Dispose();
        dels.InnerHtml = Show_del();
    }

    protected void PDel()
    {
        bool sc = true;
        string checkboxq = Request.Form["Checkbox1"];
        if (checkboxq == null || checkboxq == String.Empty)
        {
            PageError("请先选择要删除的记录!", "");
        }
        else
        {
            string[] chSplit = checkboxq.Split(',');
            for (int i = 0; i < chSplit.Length; i++)
            {
                if (chSplit[i] != "on")
                {
                    if (inf.Delete3(chSplit[i]) == 0)
                    {
                        sc = false;
                        continue;
                    }
                }
            }
            if (sc == false)
            {
                PageRight("批量删除成功。但有部分数据删除失败。", "");
            }
            else
            {
                PageRight("批量删除成功", "");
            }
        }
    }
    protected void del(string ID)
    {
        if (inf.Delete3(ID) == 0)
        {
            PageError("批量删除失败", "");
        }
        else
        {
            PageRight("删除成功!", "");
        }
    }
    string Show_no()
    {

        string nos = "<table border=0 width='98%' align=center cellpadding=5 cellspacing=1 class='table'>";
        nos = nos + "<tr class='TR_BG_list'>";
        nos = nos + "<td class='navi_link'>没有数据</td>";
        nos = nos + "</tr>";
        nos = nos + "</table>";
        userlists.Visible = false;
        return nos;
    }
    string Show_del()
    {
        string del = "<a href=\"pointhistory.aspx?type=0&UserNum=" + Request.QueryString["UserNum"] + "\" class=\"topnavichar\">全部交易</a>&nbsp;┋&nbsp;<a href=\"pointhistory.aspx?type=2&UserNum=" + Request.QueryString["UserNum"] + "\" class=\"topnavichar\">在线充值</a>&nbsp;┋&nbsp;<a href=\"pointhistory.aspx?type=3&UserNum=" + Request.QueryString["UserNum"] + "\" class=\"topnavichar\" >积分兑换</a>&nbsp;┋&nbsp;<a href=\"pointhistory.aspx?type=4&UserNum=" + Request.QueryString["UserNum"] + "\" class=\"topnavichar\" >稿酬</a>&nbsp;┋&nbsp;<a href=\"pointhistory.aspx?type=5&UserNum=" + Request.QueryString["UserNum"] + "\" class=\"topnavichar\" >阅读权限</a>&nbsp;┋&nbsp;<a href=\"pointhistory.aspx?type=1&UserNum=" + Request.QueryString["UserNum"] + "\" class=\"topnavichar\" >捐献</a>&nbsp;┋&nbsp;<a href=\"pointhistory.aspx?type=6&UserNum=" + Request.QueryString["UserNum"] + "\" class=\"topnavichar\" >登录获得</a>&nbsp;┋&nbsp;<a href=\"pointhistory.aspx?type=7&UserNum=" + Request.QueryString["UserNum"] + "\" class=\"topnavichar\" >注册获得</a>&nbsp;┋&nbsp;<a href=\"javascript:PDel();\" class=\"topnavichar\">批量删除</a>&nbsp;┋&nbsp;";  
        return del;
    }
    string Show_dels()
    {
        string dels = "<a href=\"pointhistory.aspx?type=0&UserNum=" + Request.QueryString["UserNum"] + "\" class=\"topnavichar\">全部交易</a>&nbsp;┋&nbsp;<a href=\"pointhistory.aspx?type=2&UserNum=" + Request.QueryString["UserNum"] + "\" class=\"topnavichar\">在线充值</a>&nbsp;┋&nbsp;<a href=\"pointhistory.aspx?type=3&UserNum=" + Request.QueryString["UserNum"] + "\" class=\"topnavichar\" >积分兑换</a>&nbsp;┋&nbsp;<a href=\"pointhistory.aspx?type=4&UserNum=" + Request.QueryString["UserNum"] + "\" class=\"topnavichar\" >稿酬</a>&nbsp;┋&nbsp;<a href=\"pointhistory.aspx?type=5&UserNum=" + Request.QueryString["UserNum"] + "\" class=\"topnavichar\" >阅读权限</a>&nbsp;┋&nbsp;<a href=\"pointhistory.aspx?type=1&UserNum=" + Request.QueryString["UserNum"] + "\" class=\"topnavichar\" >捐献</a>&nbsp;┋&nbsp;<a href=\"pointhistory.aspx?type=6&UserNum=" + Request.QueryString["UserNum"] + "\" class=\"topnavichar\" >登录获得</a>&nbsp;┋&nbsp;<a href=\"pointhistory.aspx?type=7&UserNum=" + Request.QueryString["UserNum"] + "\" class=\"topnavichar\" >注册获得</a>&nbsp;┋&nbsp;";
        return dels;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void selbut_Click(object sender, EventArgs e)
    {

        if (Page.IsValid)
        {
            string UNM = Request.Form["UserNameBox"];
            string ty = this.DropDownList1.SelectedValue;
            StartLoad(1, ty, UNM);
        }
    }
}

