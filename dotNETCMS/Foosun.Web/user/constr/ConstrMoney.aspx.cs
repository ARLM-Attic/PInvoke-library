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

public partial class user_constr_ConstrMoney : Foosun.Web.UI.UserPage
{
    //联接数据库
    Constr con = new Constr();
    #region 初始化
    protected void Page_Load(object sender, EventArgs e)
    {
        
        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_PageChange);
        Response.CacheControl = "no-cache";
        if (!IsPostBack)
        {
            Showu_constrlist(1);
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
        Showu_constrlist(PageIndex);
    }
    protected void Showu_constrlist(int PageIndex)
    {
        SQLConditionInfo st = new SQLConditionInfo("@UserNum", Foosun.Global.Current.UserNum);
        int i, j;
        DataTable dts = Foosun.CMS.Pagination.GetPage(this.GetType().Name, PageIndex, 10, out i, out j, st);
        this.PageNavigator1.PageCount = j;
        this.PageNavigator1.PageIndex = PageIndex;
        this.PageNavigator1.RecordCount = i;
        if (dts != null && dts.Rows.Count!=0)
        {
            if (dts.Rows.Count > 0)
            {
                dts.Columns.Add("Moneys", typeof(string));
                for (int s = 0; s < dts.Rows.Count; s++)
                {
                    decimal Money1 = decimal.Parse(dts.Rows[s]["Money"].ToString());
                    dts.Rows[s]["Moneys"] = (String.Format("{0:C}", Money1));   
                }
            } 
            DataList1.DataSource = dts;
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
    /// 前台输出
    /// </summary>
    /// <returns></returns>
    /// 
    #region 前台输出
    string Show_no()
    {
        string nos = "<table border=0 width='98%' align=center cellpadding=5 cellspacing=1 class='table'>";
        nos = nos + "<tr class='TR_BG_list'>";
        nos = nos + "<td class='navi_link'>没有数据</td>";
        nos = nos + "</tr>";
        nos = nos + "</table>";
        return nos;
    }
    #endregion
}