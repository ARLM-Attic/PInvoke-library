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

public partial class manage_Contribution_paymentannals : Foosun.Web.UI.ManagePage
{
    public manage_Contribution_paymentannals()
    {
        Authority_Code = "C046";
    }
    Constr con = new Constr();
    rootPublic pd = new rootPublic();
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
        int i, j;
        DataTable dts = Foosun.CMS.Pagination.GetPage(this.GetType().Name, PageIndex, 20, out i, out j, null);
        this.PageNavigator1.PageCount = j;
        this.PageNavigator1.PageIndex = PageIndex;
        this.PageNavigator1.RecordCount = i;
        if (dts != null && dts.Rows.Count != 0)
        {
            dts.Columns.Add("gusername", typeof(string));
            dts.Columns.Add("moneys", typeof(string));
            dts.Columns.Add("handle", typeof(string));
            dts.Columns.Add("payTimes", typeof(string));


            foreach (DataRow s in dts.Rows)
            {
                string gUName = pd.getUserName(s["userNum"].ToString());
                s["gusername"] = "<a href=\"../../" + Foosun.Config.UIConfig.dirUser + "/showuser-" + gUName + ".aspx\" target=\"_blank\" class=\"list_link\" title=\"点己差看用户信息\">" + gUName + "</a>";
                int Money1 = int.Parse(s["Money"].ToString());
                s["moneys"] = (String.Format("{0:C}", Money1));


                s["handle"] = "<a href=\"javascript:del('" + s["constrPayID"].ToString() + "');\" class=\"list_link\">删除</a>┆<input name=\"Checkbox1\" type=\"checkbox\" value=\"" + s["constrPayID"].ToString() + "\" />";
                if (!s.IsNull("payTime"))
                {
                    s["payTimes"] = s["payTime"].ToString();
                }
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
        int issuper = int.Parse(con.sel31(UserNum));
        if (issuper == 1)
        {
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
                        if (con.Delete6(chSplit[i]) == 0)
                        {
                            rd.SaveUserAdminLogs(1, 1, UserNum, "支付历史", "删除失败");
                            PageError("批量删除失败", "paymentannals.aspx");
                            break;
                        }

                    }
                }
                rd.SaveUserAdminLogs(1, 1, UserNum, "支付历史", "删除成功");
                PageRight("批量删除成功", "paymentannals.aspx");
            }
        }
        else
        {
            PageError("你不是系统管理员不能删除", "paymentannals.aspx");
        }
    }
    protected void del(string ID)
    {
        rootPublic rd = new rootPublic();
        int issuper = int.Parse(con.sel31(UserNum));
        if (issuper == 1)
        {
            if (con.Delete6(ID) == 0)
            {
                rd.SaveUserAdminLogs(1, 1, UserNum, "支付历史", "删除失败");
                PageError("删除失败", "paymentannals.aspx");
            }
            else
            {
                rd.SaveUserAdminLogs(1, 1, UserNum, "支付历史", "删除成功");
                PageRight("删除成功", "paymentannals.aspx");
            }
        }
        else
        {
            PageError("你不是超级管理员不能删除", "paymentannals.aspx");
        }

    }
}
