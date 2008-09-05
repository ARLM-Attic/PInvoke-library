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

public partial class user_friend_friendmanage : Foosun.Web.UI.UserPage
{
    Friend fri = new Friend();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_PageChange);
        Response.CacheControl = "no-cache";
        if (!IsPostBack)
        {
            Showu_friendmanage(1);
        }
        string Type = Request.QueryString["Type"];  //取得操作类型
        string ID = "";
        if (Request.QueryString["ID"] != null)
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
    protected void PageNavigator1_PageChange(object sender, int PageIndex)
    {
        Showu_friendmanage(PageIndex);
    }
    protected void Showu_friendmanage(int PageIndex)
    {
        int i, j;
        SQLConditionInfo sts = new SQLConditionInfo("@UserNum", Foosun.Global.Current.UserNum);
        DataTable dts = Foosun.CMS.Pagination.GetPage(this.GetType().Name, PageIndex, 10, out i, out j, sts);
        this.PageNavigator1.PageCount = j;
        this.PageNavigator1.PageIndex = PageIndex;
        this.PageNavigator1.RecordCount = i;
        Friend fr=new Friend();
        if (dts.Rows.Count>0)
        {
            dts.Columns.Add("cutAId",typeof(string));
            dts.Columns.Add("idc",typeof(string));
            dts.Columns.Add("CNT",typeof(string));
            foreach (DataRow s in dts.Rows)
            {
                s["cutAId"] =  s["HailFellow"].ToString();
                s["idc"] = "<a href=\"friend_add.aspx?FCID=" + s["HailFellow"].ToString() + "\" class=\"list_link\">添加</a>┆<a href=\"#\" onclick=\"javascript:del('" + s["HailFellow"].ToString() + "');\" class=\"list_link\">删除</a>┆<input name=\"Checkbox1\" type=\"checkbox\" value=" + s["HailFellow"].ToString() + "  runat=\"server\" /></td>";
                s["CNT"] = fr.FriendClassCount(s["HailFellow"].ToString());
            }
            DataList1.DataSource = dts;
            DataList1.DataBind();
            delp.InnerHtml = Show_del();
        }
        else
        {
            no.InnerHtml = Show_no();
            this.PageNavigator1.Visible = false;
        }
    }
    /// <summary>
    /// 批量删除
    /// </summary>
    /// 
    #region 批量删除
    protected void PDel()
    {
        string checkboxq = Request.Form["Checkbox1"];
        if (checkboxq == null || checkboxq == String.Empty)
        {
            PageError("请先选择要删除的好友!", "friendmanage.aspx");
        }
        else
        {
            string[] chSplit = checkboxq.Split(',');
            for (int i = 0; i < chSplit.Length; i++)
            {
                if (chSplit[i] != "on")
                {
                    if (fri.Delete1(chSplit[i]) == 0)
                    {
                        PageError("删除失败<br>", "friendmanage.aspx");
                    }
                }
            }
            PageRight("批量删除成功", "friendmanage.aspx");
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
        if (fri.Delete1(ID) != 0)
        {
            PageRight("删除成功", "friendmanage.aspx");
        }
        else
        {
            PageError("删除失败", "friendmanage.aspx");
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
    string Show_del()//显示帮助列表
    {
        string dels = "<a href=\"javascript:PDel();\" class=\"topnavichar\">批量删除</a>";
        return dels;
    }   
}


