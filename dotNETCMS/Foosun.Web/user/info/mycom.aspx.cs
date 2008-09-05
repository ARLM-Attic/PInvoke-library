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

public partial class user_info_mycom : Foosun.Web.UI.UserPage
{
    Info inf = new Info();
    Mycom my = new Mycom();
    Foosun.CMS.Common.rootPublic pd = new Foosun.CMS.Common.rootPublic();
    protected void Page_Init(object sernder, EventArgs e)
    {
        #region   初始化
        string UserNum = Foosun.Global.Current.UserNum;
        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_PageChange);
        Response.CacheControl = "no-cache";
        
        string sel_GroupNumber = inf.sel_15(UserNum);
        DataTable dt1 = inf.sel_16(sel_GroupNumber);
        int TopTitle = int.Parse(dt1.Rows[0]["TopTitle"].ToString());
        int GoodTitle = int.Parse(dt1.Rows[0]["GoodTitle"].ToString());
        int CheckTtile = int.Parse(dt1.Rows[0]["CheckTtile"].ToString());
        int OCTF = int.Parse(dt1.Rows[0]["OCTF"].ToString());

        if (TopTitle == 1)
        {
            string ctr1 = "<td><asp:Button ID=\"TopTitle1\" runat=\"server\" Text=\"固顶\" CssClass=\"form\" Width=\"80px\"/></td><td><asp:Button ID=\"TopTitle12\" runat=\"server\" Text=\"解固\"  CssClass=\"form\" Width=\"80px\"/></td>";
            Control ctrl1 = Page.ParseControl(ctr1);
            PlaceHolder1.Controls.Add(ctrl1);
            Button bt1 = (Button)Page.FindControl("TopTitle1");
            bt1.Command += new CommandEventHandler(this.TopTitle);
            Button bt2 = (Button)Page.FindControl("TopTitle12");
            bt2.Command += new CommandEventHandler(this.TopTitlej);
        }
        if (GoodTitle == 1)
        {
            string ctr1 = "<td><asp:Button ID=\"GoodTitle\" runat=\"server\" Text=\"精华\" CssClass=\"form\" Width=\"80px\"/></td> ";
            Control ctrl1 = Page.ParseControl(ctr1);
            PlaceHolder1.Controls.Add(ctrl1);
            Button bt3 = (Button)Page.FindControl("GoodTitle");
            bt3.Command += new CommandEventHandler(this.GoodTitle);
        }
        if (CheckTtile == 1)
        {
            string ctr1 = "<td><asp:Button ID=\"CheckTtile\" runat=\"server\" Text=\"审核\" CssClass=\"form\" Width=\"80px\"/></td> ";
            Control ctrl1 = Page.ParseControl(ctr1);
            PlaceHolder1.Controls.Add(ctrl1);
            Button bt4 = (Button)Page.FindControl("CheckTtile");
            bt4.Command += new CommandEventHandler(this.isCheck);
        }
        if (OCTF == 1)
        {
            string ctr1 = "<td><asp:Button ID=\"OCTF1\" runat=\"server\" Text=\"锁定\" CssClass=\"form\" Width=\"80px\"/></td><td><asp:Button ID=\"OCTF2\" runat=\"server\" Text=\"解锁\" CssClass=\"form\" Width=\"80px\"/></td>";
            Control ctrl1 = Page.ParseControl(ctr1);
            PlaceHolder1.Controls.Add(ctrl1);
            Button bt5 = (Button)Page.FindControl("OCTF1");
            bt5.Command += new CommandEventHandler(this.islock);
            Button bt6 = (Button)Page.FindControl("OCTF2");
            bt6.Command += new CommandEventHandler(this.islocks);
        }
        if (!IsPostBack)
        {
            StartLoad(1, "", "", "", "", "", "");
        }

        string Type = Request.QueryString["Type"];  //取得操作类型
        string ID = "";
        if (Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
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
        #endregion
    }

    protected void PageNavigator1_PageChange(object sender, int PageIndex)
    {
        StartLoad(PageIndex, null, null, null, null, null, null);
    }
    #region  数据绑定
    protected void StartLoad(int PageIndex,string title,string Um, string dtm1,string dtm2, string isCheck, string islock)
    {
        string UserNum = "";
        if (Request.QueryString["UserNum"] != null && Request.QueryString["UserNum"] != "")
        {
            string getName = pd.getUserName(UserNum);
            UserNum = Request.QueryString["UserNum"].ToString();
            if (getName != Foosun.Global.Current.UserName)
            {
                UserNum = Foosun.Global.Current.UserName;
            }
            else
            {
                UserNum = getName;
            }
        }
        else
        {
            UserNum = Foosun.Global.Current.UserName;
        }
        string UserNums1 = Foosun.Global.Current.UserNum;
        DataTable dt4 = inf.sel_17(UserNums1);
        DataTable dt2 = inf.sel_18(dt4.Rows[0]["UserGroupNumber"].ToString());
        int DelSelfTitle = int.Parse(dt2.Rows[0]["DelSelfTitle"].ToString());
        int DelOTitle = int.Parse(dt2.Rows[0]["DelOTitle"].ToString());
        int EditSelfTitle = int.Parse(dt2.Rows[0]["EditSelfTitle"].ToString());
        int EditOtitle = int.Parse(dt2.Rows[0]["EditOtitle"].ToString());
        int ReadTitle = int.Parse(dt2.Rows[0]["ReadTitle"].ToString());
        string SiteID = dt4.Rows[0]["SiteID"].ToString();
        int i, j;
        DataTable dt = inf.GetPage(title, Um, dtm1, dtm2, isCheck, islock, SiteID, UserNum, DelOTitle, EditOtitle, PageIndex, 10, out i, out j, null);

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
                    h["islocks"] = "否";
                }
                else 
                {
                    h["islocks"] = "是";
                }
                if(h["APIID"].ToString()=="0")
                {
                    h["InfoTitle"] = my.sel_2(h["InfoID"].ToString(), h["DataLib"].ToString());
                    h["APIIDTitle"]="新闻";
                }
                if (h["isCheck"].ToString() == "0")
                {
                    h["isChecks"] = "未通过";
                }
                else 
                {
                    h["isChecks"] = "已通过";
                }
                string del1=null;
                string Edit=null;
                string delEdit = null;

                h["Titles"] = "<a href=\"mycom_Look.aspx?Commid=" + h["Commid"].ToString() + "\" class=\"list_link\">" + h["Content"].ToString() + "</a>";

                Edit="<a href=\"mycom_up.aspx?Commid=" + h["Commid"].ToString() + "\" class=\"list_link\">编辑</a>┆<input name=\"Checkbox1\" type=\"checkbox\" value=" + h["Commid"].ToString() + "  runat=\"server\" />";
                del1="<a href=\"#\" onclick=\"javascript:del('" + h["Commid"].ToString() + "');\" class=\"list_link\">删除</a>┆<input name=\"Checkbox1\" type=\"checkbox\" value=" + h["Commid"].ToString() + "  runat=\"server\" />";
                delEdit = "<a href=\"mycom_up.aspx?Commid=" + h["Commid"].ToString() + "\" class=\"list_link\">编辑</a>┆<a href=\"#\" onclick=\"javascript:del('" + h["Commid"].ToString() + "');\" class=\"list_link\">删除</a>┆<input name=\"Checkbox1\" type=\"checkbox\" value=" + h["Commid"].ToString() + "  runat=\"server\" />";

                if ((h["UserNum"].ToString() == Foosun.Global.Current.UserName))
                {
                    if (EditSelfTitle == 1 && DelSelfTitle != 1)
                    {
                        h["Operation"] = Edit;
                    }
                    if (DelSelfTitle == 1 && EditSelfTitle != 1)
                    {
                        h["Operation"] = del1;
                    }
                    if (DelSelfTitle == 1 && EditSelfTitle == 1)
                    {
                        h["Operation"] = delEdit;
                    }
                }
                else
                {
                    if (EditOtitle == 1 && DelOTitle != 1)
                    {
                        h["Operation"] = Edit;
                    }
                    if (EditOtitle != 1 && DelOTitle == 1)
                    {
                        h["Operation"] = del1;
                    }
                    if (EditOtitle == 1 && DelOTitle == 1)
                    {
                        h["Operation"] = del1;
                    }
                }
                    
            }             
            DataList1.Visible = true;
            DataList1.DataSource = dt;
            DataList1.DataBind();
            DataList1.Dispose();
            sc.InnerHtml = Show_scs();
        }      
        else
        {
            string UserNums = Foosun.Global.Current.UserNum;
            string sel_GroupNumberp = inf.sel_15(UserNums);
            DataTable dts1 = inf.sel_16(sel_GroupNumberp);
            int TopTitles = int.Parse(dts1.Rows[0]["TopTitle"].ToString());
            int GoodTitles = int.Parse(dts1.Rows[0]["GoodTitle"].ToString());
            int CheckTtiles = int.Parse(dts1.Rows[0]["CheckTtile"].ToString());
            int OCTFs = int.Parse(dts1.Rows[0]["OCTF"].ToString());
            if (TopTitles == 1)
            {
                Button bt11 = (Button)Page.FindControl("TopTitle1");
                bt11.Visible = false;
                Button bt12 = (Button)Page.FindControl("TopTitle12");
                bt12.Visible = false;
            }
            if (GoodTitles == 1)
            {
                Button bt13 = (Button)Page.FindControl("GoodTitle");
                bt13.Visible = false;
            }
            if (CheckTtiles == 1)
            {
                Button bt14 = (Button)Page.FindControl("CheckTtile");
                bt14.Visible = false;
            }
            if (OCTFs == 1)
            { 
                Button bt15 = (Button)Page.FindControl("OCTF1");
                bt15.Visible = false;
                Button bt16 = (Button)Page.FindControl("OCTF2");
                bt16.Visible = false;                
            }
            no.InnerHtml = show_no();
            sc.InnerHtml = Show_sc();
            this.PageNavigator1.Visible = false;
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
        DataTable dt3 = my.sel_4(my.sel_3(Foosun.Global.Current.UserNum).ToString());
        string checkboxq = Request.Form["Checkbox1"];
        if (checkboxq == null || checkboxq == String.Empty)
        {
            PageError("请先选择要删除的评论!", "mycom.aspx");
        }
        else
        {
            string[] chSplit = checkboxq.Split(',');
            for (int i = 0; i < chSplit.Length; i++)
            {
                if (chSplit[i] != "on")
                {
                    if (dt3.Rows[0]["DelSelfTitle"].ToString() != "1" && dt3.Rows[0]["DelOTitle"].ToString() != "1")
                    {
                        PageError("对不起你没有删除权限不能删除", "mycom.aspx");
                    }
                    if (inf.Delete2(chSplit[i]) == 0)
                    {
                        PageError("批量删除失败", "mycom.aspx");
                        break;
                    }
                }
            }
            PageRight("批量删除成功", "mycom.aspx");
        }

    }
    protected void del(string ID)
    {
        if (inf.Delete2(ID) == 0)
        {
            PageError("批量删除失败", "mycom.aspx");
        }
        else
        {
            PageRight("删除成功!", "mycom.aspx");
        }
    }
    #endregion
    string Show_scs()
    {
        string scs = "<a href=\"mycom.aspx\" class=\"topnavichar\">全部评论</a>&nbsp;&nbsp;&nbsp;<a href=\"mycom.aspx?UserNum=" + Foosun.Global.Current.UserNum + "\" class=\"topnavichar\">我的评论</a>&nbsp;&nbsp;&nbsp;<a href=\"javascript:opencats()\" class=\"topnavichar\">搜　索</a>&nbsp;&nbsp;&nbsp;<a href=\"javascript:PDel();\" class=\"topnavichar\">批量删除</a>";
        return scs;
    }
    string Show_sc()
    {
        string sc = "<a href=\"mycom.aspx\" class=\"topnavichar\">全部评论</a>&nbsp;&nbsp;&nbsp;<a href=\"mycom.aspx?UserNum=" + Foosun.Global.Current.UserNum + "\" class=\"topnavichar\">我的评论</a>&nbsp;&nbsp;&nbsp;<a href=\"javascript:opencats()\" class=\"topnavichar\">搜　索</a>";
        return sc;
    }
    /// <summary>
    /// 固顶
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    #region  固顶
    protected void TopTitle(object sender, EventArgs e)
    {
        string checkboxq = Request.Form["Checkbox1"];
        if (checkboxq == null || checkboxq == String.Empty)
        {
            PageError("请先选择要固顶的评论!", "mycom.aspx");
        }
        else
        {
            string[] chSplit = checkboxq.Split(',');
            for (int i = 0; i < chSplit.Length; i++)
            {
                if (chSplit[i] != "on")
                {
                    if (my.sel_5(chSplit[i]) == "2")
                    {
                        PageError("固顶失败，此项已经固顶不能在操作", "mycom.aspx");
                    }
                    int OrderID = 2;
                    if (my.Update_1(OrderID, chSplit[i]) == 0)
                    {
                        PageError("固顶失败", "mycom.aspx");
                        break;
                    }
                }
            }
            PageRight("成功固顶", "mycom.aspx");
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
    protected void TopTitlej(object sender, EventArgs e)
    {
        string checkboxq = Request.Form["Checkbox1"];
        if (checkboxq == null || checkboxq == String.Empty)
        {
            PageError("请先选择要解固的评论!", "mycom.aspx");
        }
        else
        {
            string[] chSplit = checkboxq.Split(',');
            for (int i = 0; i < chSplit.Length; i++)
            {
                if (chSplit[i] != "on")
                {
                    if (my.sel_5(chSplit[i]) == "0")
                    {
                        PageError("解固失败，此项没有固顶不能进行此操作", "mycom.aspx");
                    }
                    int OrderID = 0;
                    if (my.Update_1(OrderID, chSplit[i]) == 0)
                    {
                        PageError("解固失败", "mycom.aspx");
                        break;
                    }
                }
            }
            PageRight("成功解固", "mycom.aspx");
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
    protected void GoodTitle(object sender, EventArgs e)
    {
        string checkboxq = Request.Form["Checkbox1"];
        if (checkboxq == null || checkboxq == String.Empty)
        {
            PageError("请先选择要设置的评论!", "mycom.aspx");
        }
        else
        {
            string[] chSplit = checkboxq.Split(',');
            for (int i = 0; i < chSplit.Length; i++)
            {
                if (chSplit[i] != "on")
                {
                    if (my.sel_6(chSplit[i]) == "1")
                    {
                        PageError("设置精华帖失败，此项已经是精华帖不能进行此操作", "mycom.aspx");
                    }
                    int GoodTitle = 1;
                    my.Update_2(GoodTitle, chSplit[i]);
                }
            }
            PageRight("设置精华帖成功", "mycom.aspx");
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
    protected void isCheck(object sender, EventArgs e)
    {
        string checkboxq = Request.Form["Checkbox1"];
        if (checkboxq == null || checkboxq == String.Empty)
        {
            PageError("请先选择要审核的评论!", "mycom.aspx");
        }
        else
        {
            string[] chSplit = checkboxq.Split(',');
            for (int i = 0; i < chSplit.Length; i++)
            {
                if (chSplit[i] != "on")
                {
                    if (my.sel_7(chSplit[i]) == "1")
                    {
                        PageError("审核失败，此项已通过审核不能进行此操作", "mycom.aspx");
                    }
                    if (my.Update_3(chSplit[i],1) == 0)
                    {
                        PageError("审核失败", "mycom.aspx");
                        break;
                    }
                }
            }
            PageRight("审核成功", "mycom.aspx");
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
    protected void islock(object sender, EventArgs e)
    {
        string checkboxq = Request.Form["Checkbox1"];
        if (checkboxq == null || checkboxq == String.Empty)
        {
            PageError("请先选择要锁定的评论!", "mycom.aspx");
        }
        else
        {
            string[] chSplit = checkboxq.Split(',');
            for (int i = 0; i < chSplit.Length; i++)
            {
                if (chSplit[i] != "on")
                {
                    if (my.sel_8(chSplit[i]) == "1")
                    {
                        PageError("锁定失败，此项已锁定不能进行此操作", "mycom.aspx");
                    }
                    int islock = 1;
                    if (my.Update_4(islock, chSplit[i]) == 0)
                    {
                        PageError("锁定失败", "mycom.aspx");
                        break;
                    }
                }
            }
            PageRight("锁定成功", "mycom.aspx");
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
    protected void islocks(object sender, EventArgs e)
    {
        string checkboxq = Request.Form["Checkbox1"];
        if (checkboxq == null || checkboxq == String.Empty)
        {
            PageError("请先选择要解锁的评论!", "mycom.aspx");
        }
        else
        {
            string[] chSplit = checkboxq.Split(',');
            for (int i = 0; i < chSplit.Length; i++)
            {
                if (chSplit[i] != "on")
                {
                    if (my.sel_8(chSplit[i]) == "0")
                    {
                        PageError("解锁失败，此项没有被锁定不能进行此操作", "mycom.aspx");
                    }
                    int islock = 0;
                    if (my.Update_4(islock, chSplit[i]) == 0)
                    {
                        PageError("解锁失败", "mycom.aspx");
                        break;
                    }
                }
            }
            PageRight("解锁成功", "mycom.aspx");
        }
    }
    #endregion
    protected void Button8_ServerClick(object sender, EventArgs e)
    {
        string title = Request.Form["Title1"];
        string Um = Request.Form["InfoTitle1"];
        string dtm1 = Request.Form["creatTime1"];
        string dtm2 = Request.Form["creatTime2"];
        string isCheck = this.isCheck1.SelectedValue;
        string islock = this.islock1.SelectedValue;
        StartLoad(1, title, Um, dtm1, dtm2, isCheck, islock);
    }
}