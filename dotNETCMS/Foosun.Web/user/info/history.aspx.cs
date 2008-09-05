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

public partial class user_info_history : Foosun.Web.UI.UserPage
{
    #region  初始化
    Info inf = new Info();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";
        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_PageChange);
        string ghtype4 = Request.QueryString["ghtype"];
        string typex = Request.QueryString["Type"];
        string Types = Request.QueryString["Types"];  //取得操作类型
        string type = Request.QueryString["type"];
        if (!IsPostBack)
        {
            
            copyright.InnerHtml = CopyRight;
            StartLoad(1, typex, ghtype4);   
        }
        string ID = "";
        if (Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
        {
            ID = Foosun.Common.Input.Filter(Request.QueryString["ID"]);  //取得需要操作的稿件ID
        }
        switch (Types)
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
    #region  获得列表
    protected void PageNavigator1_PageChange(object sender, int PageIndex)
    {
        string ghtype4 = Request.QueryString["ghtype"];
        string typex = Request.QueryString["Type"];
        StartLoad(PageIndex, typex, ghtype4);
    }
    protected void StartLoad(int PageIndex, string typep,string ghtype2)
    {
        DataTable dt = null;
        no.InnerHtml = "";
        int i, j;
        if (ghtype2 == "1")
        {
            SQLConditionInfo sts = new SQLConditionInfo("@UserNum", Foosun.Global.Current.UserNum);
            dt = Foosun.CMS.Pagination.GetPage("user_info_history_1_aspx", PageIndex, 10, out i, out j, sts);
        }
        else if(ghtype2 == "2")
        {
            SQLConditionInfo sts = new SQLConditionInfo("@UserNum", Foosun.Global.Current.UserNum);
            dt = Foosun.CMS.Pagination.GetPage("user_info_history_2_aspx", PageIndex, 10, out i, out j, sts);           
        }
        else if(typep != "0" && typep != null)
        {
            SQLConditionInfo[] sts = new SQLConditionInfo[2];
            sts[0] = new SQLConditionInfo("@UserNum", Foosun.Global.Current.UserNum);
            sts[1] = new SQLConditionInfo("@gtype", int.Parse(typep));
            dt = Foosun.CMS.Pagination.GetPage("user_info_history_3_aspx", PageIndex, 10, out i, out j, sts);
        }
        else 
        {
            SQLConditionInfo sts = new SQLConditionInfo("@UserNum", Foosun.Global.Current.UserNum);
            dt = Foosun.CMS.Pagination.GetPage("user_info_history_4_aspx", PageIndex, 10, out i, out j, sts);           
        }
        this.PageNavigator1.PageCount = j;
        this.PageNavigator1.PageIndex = PageIndex;
        this.PageNavigator1.RecordCount = i;
        if (dt != null && dt.Rows.Count > 0)
        {
            dt.Columns.Add("ghtypes", typeof(string));
            dt.Columns.Add("Moneys", typeof(string));
            dt.Columns.Add("UserName", typeof(string));
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
                dt.Rows[k]["UserName"] = inf.sel_13(dt.Rows[k]["UserNUM"].ToString());
                
            }
            userlists.Visible = true;
        }
        else
        {
            no.InnerHtml = Show_no();
            this.PageNavigator1.Visible = false;
        }

        userlists.DataSource = dt;
        userlists.DataBind();
        userlists.Dispose();
    }
     #endregion
    #region  删除
    protected void PDel()
    {
        string checkboxq = Request.Form["Checkbox1"];
        if (checkboxq == null || checkboxq == String.Empty)
        {
            PageError("请先选择要删除的记录!", "history.aspx");
        }
        else
        {
            string[] chSplit = checkboxq.Split(',');
            for (int i = 0; i < chSplit.Length; i++)
            {
                if (chSplit[i] != "on")
                {
                    if (inf.Delete1(chSplit[i]) == 0)
                    {
                        PageError("批量删除失败", "history.aspx");
                        break;
                    }
                }
            }
            PageRight("批量删除成功", "history.aspx");
        }

    }
    protected void del(string ID)
    {
        if (inf.Delete1(ID) == 0)
        {
            PageError("批量删除失败", "history.aspx");
        }
        else
        {
            PageRight("删除成功!", "history.aspx");
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
        userlists.Visible = false;
        return nos;
    }
}

