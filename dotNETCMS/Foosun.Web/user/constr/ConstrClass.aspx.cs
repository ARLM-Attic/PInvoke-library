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

public partial class user_constr_ConstrClass : Foosun.Web.UI.UserPage
{
    //数据库连接
    Constr con = new Constr();
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    #region 初始化
    protected void Page_Load(object sender, EventArgs e)
    {
        
        Response.CacheControl = "no-cache";
        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_PageChange);
        if (!IsPostBack)
        {
            Show_ConstrClass(1);
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
    /// 分页
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="PageIndex"></param>
    /// 
    #region 分页
    protected void PageNavigator1_PageChange(object sender, int PageIndex)
    {
        Show_ConstrClass(PageIndex);
    }
    protected void Show_ConstrClass(int PageIndex)
    {
        SQLConditionInfo st = new SQLConditionInfo("@UserNum", Foosun.Global.Current.UserNum);
        int i, j;
        DataTable dt = Foosun.CMS.Pagination.GetPage(this.GetType().Name, PageIndex, 10, out i, out j, st);
        this.PageNavigator1.PageCount = j;
        this.PageNavigator1.PageIndex = PageIndex;
        this.PageNavigator1.RecordCount = i;
        if (dt != null && dt.Rows.Count!=0)
        {
            dt.Columns.Add("Ccids", typeof(string));
            dt.Columns.Add("idc", typeof(string));
            foreach (DataRow s in dt.Rows)
            {
                s["Ccids"] = "<a href=\"Constrlist.aspx?ClassID=" + s["Ccid"].ToString() + "\" class=\"list_link\" >" + s["cName"].ToString() + "</a>";
                s["idc"] = "<a href=\"ConstrClass_up.aspx?Ccid=" + s["Ccid"].ToString() + "\" class=\"list_link\" ><img src=\"../../sysImages/folder/re.gif\" alt=\"修改\" border=\"0\" /></a>&nbsp;<a href=\"#\" onclick=\"javascript:del('" + s["Ccid"].ToString() + "');\" class=\"list_link\"><img src=\"../../sysImages/folder/dels.gif\" alt=\"删除\" border=\"0\" /></a>&nbsp;<input name=\"Checkbox1\" type=\"checkbox\" value=" + s["Ccid"].ToString() + "  runat=\"server\" /></td>";
            }
            DataList1.DataSource = dt;
            DataList1.DataBind();
            delp.InnerHtml = Show_del();
        }
        else
        {
            no.InnerHtml = Show_no();
            this.PageNavigator1.Visible = false;
        }
    }
    #endregion
    /// <summary>
    /// 前台初始化
    /// </summary>
    /// <returns></returns>
    /// 
    #region 前台初始化
    string Show_no()//显示帮助列表
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
            PageError("请先选择要删除的分类!", "");
        }
        else
        {
            string[] chSplit = checkboxq.Split(',');
            for (int i = 0; i < chSplit.Length; i++)
            {
                if (chSplit[i] != "on")
                {
                    DataTable dt = con.sel5(chSplit[i]);
                    int cut = dt.Rows.Count;
                    if (cut > 0)
                    {
                        PageError("稿件分类删除失败请确保该分类下面没有稿件<br>", "");
                    }
                    else
                    {
                        if (con.Delete1(chSplit[i]) == 0)
                        {
                            PageError("稿件分类删除失败<br>", "");
                        }
                    }
                }
            }
            PageRight("稿件分类批量删除成功", "");
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
        if (con.Delete1(ID)!=0)
        {
            PageRight("删除成功", "ConstrClass.aspx");
        }
        else
        {
            PageError("删除失败<br>", "");
        }
    }
    #endregion
}