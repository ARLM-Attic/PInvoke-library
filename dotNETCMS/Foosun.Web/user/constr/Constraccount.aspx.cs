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

public partial class user_constr_Constraccount : Foosun.Web.UI.UserPage
{
    /// <summary>
    /// 初始化
    /// </summary>
    /// 
    #region  初始化
    Constr con = new Constr();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_PageChange);
        Response.CacheControl = "no-cache";
        int cut_sel = con.sel2();
        if (cut_sel == 0)
        {
            addcount.InnerHtml = Show_add();
        }

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
            default:
                break;
        }
    }
    #endregion
    /// <summary>
    /// 数据绑定
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="PageIndex"></param>
    /// 
    #region 数据绑定
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
                dts.Columns.Add("idc", typeof(string));
                for (int s = 0; s < dts.Rows.Count; s++)
                {
                    dts.Rows[s]["idc"] = "<a class=\"list_link\" href=\"Constraccount_up.aspx?ConID=" + dts.Rows[s]["ConID"].ToString() + "\"><img src=\"../../sysImages/folder/re.gif\" alt=\"修改\" border=\"0\" /></a>&nbsp;<a href=\"#\" onclick=\"javascript:del('" + dts.Rows[s]["ConID"].ToString() + "');\" class=\"list_link\"><img src=\"../../sysImages/folder/dels.gif\" alt=\"删除\" border=\"0\" /></a>";

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
    string Show_no()
    {
        string nos = "<table border=0 width='98%' align=center cellpadding=5 cellspacing=1 class='table'>";
        nos = nos + "<tr class='TR_BG_list'>";
        nos = nos + "<td class='navi_link'>没有数据</td>";
        nos = nos + "</tr>";
        nos = nos + "</table>";
        return nos;
    }
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="ID"></param>
    /// 
    #region 删除
    protected void del(string ID)
    {
        if (con.Delete(ID)==0)
        {
            PageError("删除失败", "");
        }
        else
        {
            PageRight("删除成功!", "");
        }
    }
    #endregion
    string Show_add()
    {
        string addc = "<a href=\"Constraccount_add.aspx\" class=\"menulist\">添加账号</a>";
        return addc;
    }
}

