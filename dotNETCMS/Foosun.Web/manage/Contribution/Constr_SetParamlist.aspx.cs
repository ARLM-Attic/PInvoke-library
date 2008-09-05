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

public partial class manage_Contribution_Constr_SetParamlist : Foosun.Web.UI.ManagePage
{
    Constr con = new Constr();
    protected void Page_Load(object sender, EventArgs e)
    {

        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_PageChange);
        Response.CacheControl = "no-cache";
        if (!IsPostBack)
        {
            Showu_discusslist(1);
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

    protected void PageNavigator1_PageChange(object sender, int PageIndex)
    {
        Showu_discusslist(PageIndex);
    }
    protected void Showu_discusslist(int PageIndex)//显示所有讨论组列表
    {
        int ig, js;
        DataTable dts = Foosun.CMS.Pagination.GetPage(this.GetType().Name, PageIndex, 10, out ig, out js, null);


        this.PageNavigator1.PageCount = js;
        this.PageNavigator1.PageIndex = PageIndex;
        this.PageNavigator1.RecordCount = ig;
        if (dts != null && dts.Rows.Count != 0)
        {

            dts.Columns.Add("moneys", typeof(string));
            dts.Columns.Add("idc", typeof(string));
            foreach (DataRow s in dts.Rows)
            {
                decimal Money = decimal.Parse(s["money"].ToString());
                s["moneys"] = (String.Format("{0:C}", Money));
                s["idc"] = "<a class=\"list_link\" href=\"Constr_SetParamup.aspx?PCId=" + s["PCId"].ToString() + "\")\">修改</a>┆<a href=\"#\" onclick=\"javascript:del('" + s["PCId"].ToString() + "');\" class=\"list_link\">删除</a>┆<input name=\"Checkbox1\" type=\"checkbox\" value=" + s["PCId"].ToString() + "  runat=\"server\" />";
            }
            DataList1.DataSource = dts;
            DataList1.DataBind();
            pdels.InnerHtml = Show_pdels();
        }

        else
        {
            no.InnerHtml = Show_no();
            this.PageNavigator1.Visible = false;
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
    string Show_pdels()
    {

        string pdels = "<a href=\"javascript:PDel();\" class=\"topnavichar\">批量删除</a>";
        return pdels;
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
                    if (con.Delete5(chSplit[i]) == 0)
                    {
                        rd.SaveUserAdminLogs(1, 1, UserNum, "稿酬设置", "删除失败");
                        PageError("批量删除失败", "");
                        break;
                    }
                }
            }
            rd.SaveUserAdminLogs(1, 1, UserNum, "稿酬设置", "删除成功");
            PageRight("批量删除成功", "");
        }

    }
    protected void del(string ID)
    {
        rootPublic rd = new rootPublic();
        if (con.Delete5(ID) == 0)
        {
            rd.SaveUserAdminLogs(1, 1, UserNum, "稿酬设置", "删除失败");
            PageError("批量删除失败", "");
        }
        else
        {
            rd.SaveUserAdminLogs(1, 1, UserNum, "稿酬设置", "删除成功");
            PageRight("删除成功!", "");
        }
    }
}
