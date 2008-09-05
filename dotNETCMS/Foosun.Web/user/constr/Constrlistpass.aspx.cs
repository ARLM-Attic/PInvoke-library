//===========================================================
//==     (c)2007 Foosun Inc. by dotNETCMS 1.0              ==
//==             Forum:bbs.foosun.net                      ==
//==            website:www.foosun.net                     ==
//==     Address:NO.109 HuiMin ST.,Chengdu ,China          ==
//==         TEL:86-28-85098980/66026180                   ==
//==         qq:655071,MSN:ikoolls@gmail.com               ==
//==            Code By ZhenJiang.Wang                     == 
//===========================================================
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

public partial class user_constr_Constrlistpass : Foosun.Web.UI.UserPage
{
    //联接数据库
    Constr con = new Constr();
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// 
    #region 初始化
    protected void Page_Load(object sender, EventArgs e)
    {
        
        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_PageChange);
        Response.CacheControl = "no-cache";
        if (!IsPostBack)
        {
            Showu_constrlist(1);
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
    #endregion
    /// <summary>
    /// 分页绑定数据
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="PageIndex"></param>
    #region 分页绑定数据
    protected void PageNavigator1_PageChange(object sender, int PageIndex)
    {
        Showu_constrlist(PageIndex);
    }
    protected void Showu_constrlist(int PageIndex)
    {
        SQLConditionInfo st = new SQLConditionInfo("@UserNum", Foosun.Global.Current.UserNum);
        int i, j;
        DataTable dt = Foosun.CMS.Pagination.GetPage(this.GetType().Name, PageIndex, 10, out i, out j, st);
        this.PageNavigator1.PageCount = j;
        this.PageNavigator1.PageIndex = PageIndex;
        this.PageNavigator1.RecordCount = i;
        if (dt != null && dt.Rows.Count!=0)
        {
            if (dt.Rows.Count > 0)
            {
                dt.Columns.Add("isp", typeof(string));
                dt.Columns.Add("cNames", typeof(string));
                dt.Columns.Add("idc", typeof(string));
                dt.Columns.Add("Titles", typeof(string));
                for (int s = 0; s < dt.Rows.Count; s++)
                {
                    dt.Rows[s]["isp"] = "已退稿";
                    dt.Rows[s]["cNames"] = con.sel_cName(dt.Rows[s]["ClassID"].ToString());

                    dt.Rows[s]["idc"] = "<a href=\"javascript:del('" + dt.Rows[s]["ConID"].ToString() + "');\" class=\"list_link\" title=\"删除此投稿\"><img src=\"../../sysImages/folder/dels.gif\" alt=\"删除\" border=\"0\" /></a>┆<input name=\"Checkbox1\" type=\"checkbox\" value=" + dt.Rows[s]["ConID"].ToString() + "  runat=\"server\" />";

                    dt.Rows[s]["Titles"] = "<a href=\"Constrlistpass_DC.aspx?ConID=" + dt.Rows[s]["ConID"].ToString() + "\" class=\"list_link\" title=\"点击查看退稿原因\">" + dt.Rows[s]["Title"].ToString() + "</a>";
                }
            }
            DataList1.DataSource = dt;
            DataList1.DataBind();
        }
        else
        {
            no.InnerHtml = Show_no();
            this.PageNavigator1.Visible = false;
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
        if (con.Delete3(ID)==0)
        {
            PageError("稿件删除失败", "Constrlistpass.aspx");
        }
        else
        {
            PageRight("稿件删除成功", "Constrlistpass.aspx");
        }
    }
    #endregion
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
            PageError("请先选择要锁定的稿件!", "Constrlistpass.aspx");
        }
        else
        {
            string[] chSplit = checkboxq.Split(',');
            for (int i = 0; i < chSplit.Length; i++)
            {
                if (chSplit[i] != "on")
                {
                    if (con.Delete3(chSplit[i]) == 0)
                        {
                            PageError("稿件批量删除失败", "Constrlistpass.aspx");
                            break;
                        }
                }
            }
            PageRight("稿件批量删除成功", "Constrlistpass.aspx");
        }
    }
    #endregion
    /// <summary>
    /// 前台输出
    /// </summary>
    /// <returns></returns>
    /// 
    #region 前台输出
    string Show_no()
    {
        string nos = "<table border=0 width='98%' align=center cellpadding=5 cellspacing=1 class='table'>";
        nos = nos + "<tr class='TR_BG_list'>";
        nos = nos + "<td class='navi_link'>没有投稿</td>";
        nos = nos + "</tr>";
        nos = nos + "</table>";
        return nos;
    }
    #endregion
}