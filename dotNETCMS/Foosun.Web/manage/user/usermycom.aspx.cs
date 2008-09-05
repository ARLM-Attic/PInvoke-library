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

public partial class user_mycom : Foosun.Web.UI.ManagePage
{
    public user_mycom()
    {
        Authority_Code = "U034";
    }
    Mycom my = new Mycom();
    UserList UL = new UserList();
    UserMisc rd = new UserMisc();
    rootPublic pd = new rootPublic();
    protected void Page_Init(object sernder, EventArgs e)
    {
        #region   初始化
        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_PageChange);
        Response.CacheControl = "no-cache";
        
        copyright.InnerHtml = CopyRight;

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
        //DataTable dt5 = my.sel_1();
        //this.APIIDTitle1.DataSource = dt5;
        //this.APIIDTitle1.DataTextField = "API_Name";
        //this.APIIDTitle1.DataValueField = "API_ID";
        //this.APIIDTitle1.DataBind();
        string GoodTitle = Request.QueryString["GoodTitle"];
        if (!Page.IsPostBack)
            StartLoad(1, GoodTitle, "", "", "", "", "", "", "");
        string Type = Request.QueryString["Type"];  //取得操作类型
        string ID = "";
        if (Request.QueryString["ID"] != null)
        {
            ID = Foosun.Common.Input.Filter(Request.QueryString["ID"]);  //取得需要操作的稿件ID
        }
        switch (Type)
        {
            case "del":          //删除
                this.Authority_Code = "U035";
                this.CheckAdminAuthority();
                del(ID);
                break;
            case "PDel":            //批量删除
                this.Authority_Code = "U035";
                this.CheckAdminAuthority();
                PDel();
                break;
            default:
                break;
        }
        #endregion
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
        siteStr += "</select>\r";
        return siteStr;
    }

    protected void PageNavigator1_PageChange(object sender, int PageIndex)
    {
        string GoodTitle = Request.QueryString["GoodTitle"];
        StartLoad(PageIndex, GoodTitle, null, null, null, null, null, null, null);
    }

    #region  数据绑定

    protected void StartLoad(int PageIndex, string GoodTitle2, string UserID, string title, string Um, string dtm1, string dtm2, string isCheck, string islock)
    {
        string UserNum2="";
        if (Request.QueryString["UserNum"] != null && Request.QueryString["UserNum"] != "")
        {
            UserNum2 = Request.QueryString["UserNum"].ToString();
        }
        string RequestSiteId = Request.QueryString["SiteID"];
        string infoID = Request.QueryString["iID"];
        if (infoID != "" && infoID != null) { infoID = infoID.ToString(); }
        string ApiID = Request.QueryString["aID"];
        if (ApiID != "" && ApiID != null) { ApiID = ApiID.ToString(); }
        string DTable = Request.QueryString["TB"];
        if (DTable != "" && DTable != null) { DTable = DTable.ToString(); }
        int i, j;
        DataTable dt = my.GetPage(UserNum2, GoodTitle2, UserID, title, Um, dtm1, dtm2, isCheck, islock, RequestSiteId, infoID, ApiID, DTable, PageIndex, 10, out i, out j, null);
        this.PageNavigator1.PageCount = j;
        this.PageNavigator1.PageIndex = PageIndex;
        this.PageNavigator1.RecordCount = i;
        no.InnerHtml = "";
        if (dt != null && dt.Rows.Count > 0)
        {
            dt.Columns.Add("GoodTitles", typeof(string));
            dt.Columns.Add("OrderIDs", typeof(string));
            dt.Columns.Add("islocks", typeof(string));
            dt.Columns.Add("InfoTitle", typeof(string));
            dt.Columns.Add("APIIDTitle", typeof(string));
            dt.Columns.Add("Operation", typeof(string));
            dt.Columns.Add("isChecks", typeof(string));
            dt.Columns.Add("Titles", typeof(string));
            dt.Columns.Add("UserNames", typeof(string));
            foreach (DataRow h in dt.Rows)
            {
                if (h["GoodTitle"].ToString() == "1")
                {
                    h["GoodTitles"] = "<img src=\"../../sysImages/normal/best.jpg\" border=\"0\" alt=\"精华帖\" />";
                }
                else
                {
                    h["GoodTitles"] = "";
                }
                if (h["UserNum"].ToString() != "匿名")
                {
                    h["UserNames"] = "<a href=\"../../" + Foosun.Config.UIConfig.dirUser + "/showuser-" + h["UserNum"].ToString() + ".aspx\" target=\"_blank\" class=\"list_link\" title=\"查看此用户信息\">" + h["UserNum"].ToString() + "</a>";
                }
                else
                {
                    h["UserNames"] = h["UserNum"].ToString();
                }
                if (h["OrderID"].ToString() == "2")
                {
                    h["OrderIDs"] = "<img src=\"../../sysImages/folder/news_top.gif\" border=\"0\" alt=\"固顶\" />";
                }
                else
                {
                    h["OrderIDs"] = "<img src=\"../../sysImages/folder/news_common.gif\" border=\"0\"/>";
                }
                if (h["islock"].ToString() == "0")
                {
                    h["islocks"] = "<img src=\"../../sysImages/folder/no.gif\" border=\"0\" title=\"正常\" />";
                }
                else 
                {
                    h["islocks"] = "<img src=\"../../sysImages/folder/yes.gif\" border=\"0\" title=\"锁定\" />";
                }
                if(h["APIID"].ToString()=="0")
                {
                    h["InfoTitle"] = my.sel_2(h["InfoID"].ToString(), h["DataLib"].ToString());
                    h["APIIDTitle"]="新闻";
                }
                if (h["isCheck"].ToString() == "0")
                {
                    h["isChecks"] = "<img src=\"../../sysImages/folder/no.gif\" border=\"0\" title=\"未通过\" />";
                }
                else 
                {
                    h["isChecks"] = "<img src=\"../../sysImages/folder/yes.gif\" border=\"0\" title=\"已通过\" />";
                }
                h["Titles"] = "<a href=\"usermycom_Look.aspx?Commid=" + h["Commid"].ToString() + "\" class=\"list_link\">" + h["Title"].ToString() + "</a>";
                string delEdit = null;
                delEdit = "<a href=\"usermycom_up.aspx?Commid=" + h["Commid"].ToString() + "\" class=\"list_link\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/edit.gif\" border=\"0\" alt=\"编辑\" /></a>&nbsp;<a href=\"#\" onclick=\"javascript:del('" + h["Commid"].ToString() + "');\" class=\"list_link\"><img src=\"../../sysimages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/del.gif\" border=\"0\" alt='删除'\"></a>&nbsp;<input name=\"Checkbox1\" type=\"checkbox\" value=" + h["Commid"].ToString() + "  runat=\"server\" />";
                h["Operation"] = delEdit;             
            }
            DataList1.Visible = true;
            DataList1.DataSource = dt;
            DataList1.DataBind();
            DataList1.Dispose();
            sc.InnerHtml = Show_scs();
        }      
        else
        {
            no.InnerHtml = show_no();
            sc.InnerHtml = Show_sc();
            this.PageNavigator1.Visible = false;
            TopTitle1.Visible = false;
            TopTitle12.Visible = false;
            GoodTitle.Visible = false;
            UNGoodTitle.Visible = false;
            CheckTtile.Visible = false;
            OCTF1.Visible = false;
            OCTF2.Visible = false;
            Button3.Visible = false;
            Button4.Visible = false;
        }
    }
    #endregion
    string show_no()
    {   
        string nos = "<table border=0 width='98%' align=center cellpadding=5 cellspacing=1 class='table'>";
        nos = nos + "<tr class='TR_BG_list'>";
        nos = nos + "<td class='navi_link'>没有数据</td>";
        nos = nos + "</tr>";
        nos = nos + "</table>";
        DataList1.Visible = false;
        return nos;
    }
    #region  删除
    protected void PDel()
    {
        string checkboxq = Request.Form["Checkbox1"];
        if (checkboxq == null || checkboxq == String.Empty)
        {
            PageError("请先选择要删除的评论!", "usermycom.aspx");
        }
        else
        {
            string[] chSplit = checkboxq.Split(',');
            for (int i = 0; i < chSplit.Length; i++)
            {
                my.Delete(chSplit[i]);
            }
            PageRight("批量删除成功", "usermycom.aspx");
        }

    }

    protected void del(string ID)
    {
        my.Delete(ID);
        PageRight("删除成功!", "usermycom.aspx");
    }
    #endregion
    string Show_scs()
    {
        string scs = "<a href=\"usermycom.aspx\" class=\"topnavichar\">全部评论</a>&nbsp;┋&nbsp;<a href=\"usermycom.aspx?GoodTitle=1\" class=\"topnavichar\">精华帖</a>&nbsp;┋&nbsp;<a href=\"javascript:opencats()\" class=\"topnavichar\">搜索</a>&nbsp;&nbsp;";
        return scs;
    }
    string Show_sc()
    {
        string sc = "<a href=\"usermycom.aspx\" class=\"topnavichar\">全部评论</a>&nbsp;┋&nbsp;<a href=\"usermycom.aspx?GoodTitle=1\" class=\"topnavichar\">精华帖</a>&nbsp;┋&nbsp;<a href=\"javascript:opencats()\"  class=\"topnavichar\">搜索</a>&nbsp;&nbsp;";
        return sc;
    }
    #region 操作
    protected void Button8_ServerClick(object sender, EventArgs e)
    {
        string ReqUserID = "";
        if (Request.Form["UserNumbox"] != null && Request.Form["UserNumbox"] != "")
        {
            ReqUserID = pd.getUserNameUserNum(Request.Form["UserNumbox"].ToString());
        }
        string title = Request.Form["Title1"];
        string Um = Request.Form["InfoTitle1"];
        string dtm1 = Request.Form["creatTime1"];
        string dtm2 = Request.Form["creatTime2"];
        string isCheck = this.isCheck1.SelectedValue;
        string islock = this.islock1.SelectedValue;
        string GoodTitle1 = Request.QueryString["GoodTitle"];
        StartLoad(1, GoodTitle1, ReqUserID, title, Um, dtm1, dtm2, isCheck, islock);
    }
    #endregion
    /// <summary>
    /// 固顶
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    #region  固顶
    protected void TopTitle1_Click(object sender, EventArgs e)
    {
        string checkboxq = Request.Form["Checkbox1"];
        if (checkboxq == null || checkboxq == String.Empty)
        {
            PageError("请先选择要固顶的评论!", "usermycom.aspx");
        }
        else
        {
            string[] chSplit = checkboxq.Split(',');
            for (int i = 0; i < chSplit.Length; i++)
            {
                if (chSplit[i] != "on")
                {
                    if (my.Update_1(2,chSplit[i]) == 0)
                    {
                        PageError("固顶失败", "usermycom.aspx");
                        break;
                    }
                }
            }
            PageRight("成功固顶", "usermycom.aspx");
        }
    }
    #endregion
    /// <summary>
    /// 解固
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// 
    #region   解固
    protected void TopTitle12_Click(object sender, EventArgs e)
    {
        string checkboxq = Request.Form["Checkbox1"];
        if (checkboxq == null || checkboxq == String.Empty)
        {
            PageError("请先选择要解固的评论!", "usermycom.aspx");
        }
        else
        {
            string[] chSplit = checkboxq.Split(',');
            for (int i = 0; i < chSplit.Length; i++)
            {
                if (chSplit[i] != "on")
                {
                    if (my.Update_1(0, chSplit[i])==0)
                    {
                        PageError("解固失败", "usermycom.aspx");
                        break;
                    }
                }
            }
            PageRight("成功解固", "usermycom.aspx");
        }
    }
    #endregion
    /// <summary>
    /// 设置精华帖
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// 
    #region  设置精华帖
    protected void GoodTitle_Click(object sender, EventArgs e)
    {
        string checkboxq = Request.Form["Checkbox1"];
        if (checkboxq == null || checkboxq == String.Empty)
        {
            PageError("请先选择要设置的评论!", "usermycom.aspx");
        }
        else
        {
            string[] chSplit = checkboxq.Split(',');
            for (int i = 0; i < chSplit.Length; i++)
            {
                if (chSplit[i] != "on")
                {
                    my.Update_2(1, chSplit[i]);
                }
            }
            PageRight("设置精华帖成功", "usermycom.aspx");
        }
    }


    protected void unGoodTitle_Click(object sender, EventArgs e)
    {
        string checkboxq = Request.Form["Checkbox1"];
        if (checkboxq == null || checkboxq == String.Empty)
        {
            PageError("请先选择要设置的评论!", "usermycom.aspx");
        }
        else
        {
            string[] chSplit = checkboxq.Split(',');
            for (int i = 0; i < chSplit.Length; i++)
            {
                if (chSplit[i] != "on")
                {
                    my.Update_2(0, chSplit[i]);
                }
            }
            PageRight("设置精华帖成功", "usermycom.aspx");
        }
    }
    #endregion
    /// <summary>
    /// 审核
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// 
    #region   审核
    protected void CheckTtile_Click(object sender, EventArgs e)
    {
        string checkboxq = Request.Form["Checkbox1"];
        if (checkboxq == null || checkboxq == String.Empty)
        {
            PageError("请先选择要审核的评论!", "usermycom.aspx");
        }
        else
        {
            string[] chSplit = checkboxq.Split(',');
            for (int i = 0; i < chSplit.Length; i++)
            {
                if (chSplit[i] != "on")
                {
                    if (my.Update_3(chSplit[i],1) == 0)
                    {
                        PageError("审核失败", "usermycom.aspx");
                        break;
                    }
                }
            }
            PageRight("审核成功", "usermycom.aspx");
        }
    }

    protected void unCheckTtile_Click(object sender, EventArgs e)
    {
        string checkboxq = Request.Form["Checkbox1"];
        if (checkboxq == null || checkboxq == String.Empty)
        {
            PageError("请先选择要审核的评论!", "usermycom.aspx");
        }
        else
        {
            string[] chSplit = checkboxq.Split(',');
            for (int i = 0; i < chSplit.Length; i++)
            {
                if (chSplit[i] != "on")
                {
                    if (my.Update_3(chSplit[i],0) == 0)
                    {
                        PageError("审核失败", "usermycom.aspx");
                        break;
                    }
                }
            }
            PageRight("取消审核成功", "usermycom.aspx");
        }
    }
    #endregion
    /// <summary>
    /// 锁定
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// 
    #region 锁定
    protected void OCTF1_Click(object sender, EventArgs e)
    {
        string checkboxq = Request.Form["Checkbox1"];
        if (checkboxq == null || checkboxq == String.Empty)
        {
            PageError("请先选择要锁定的评论!", "usermycom.aspx");
        }
        else
        {
            string[] chSplit = checkboxq.Split(',');
            for (int i = 0; i < chSplit.Length; i++)
            {
                if (chSplit[i] != "on")
                {
                    int islock = 1;
                    if (my.Update_4(islock, chSplit[i]) == 0)
                    {
                        PageError("锁定失败", "usermycom.aspx");
                        break;
                    }
                }
            }
            PageRight("锁定成功", "usermycom.aspx");
        }
    }
    #endregion
    /// <summary>
    /// 解锁
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// 
    #region 解锁
    protected void OCTF2_Click(object sender, EventArgs e)
    {
        string checkboxq = Request.Form["Checkbox1"];
        if (checkboxq == null || checkboxq == String.Empty)
        {
            PageError("请先选择要解锁的评论!", "usermycom.aspx");
        }
        else
        {
            string[] chSplit = checkboxq.Split(',');
            for (int i = 0; i < chSplit.Length; i++)
            {
                if (chSplit[i] != "on")
                {
                    int islock = 0;
                    if (my.Update_4(islock, chSplit[i]) == 0)
                    {
                        PageError("解锁失败", "usermycom.aspx");
                        break;
                    }
                }
            }
            PageRight("解锁成功", "usermycom.aspx");
        }
    }
    #endregion

}