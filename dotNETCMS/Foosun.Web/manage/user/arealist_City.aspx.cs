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
using Foosun.Model;

public partial class manage_user_arealist_City : Foosun.Web.UI.ManagePage
{
    public manage_user_arealist_City()
    {
        Authority_Code = "U031";
    }
    Arealist ali = new Arealist();
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
            string Cid = Foosun.Common.Input.Filter(Request.QueryString["Cid"].ToString());
            int ig, js;
            SQLConditionInfo st = new SQLConditionInfo("@Cid", Cid);
            DataTable dts = Foosun.CMS.Pagination.GetPage(this.GetType().Name, PageIndex, 10, out ig, out js, st);
            this.PageNavigator1.PageCount = js;
            this.PageNavigator1.PageIndex = PageIndex;
            this.PageNavigator1.RecordCount = ig;
            if (dts != null && dts.Rows.Count!=0)
            {
                dts.Columns.Add("creatTimes", typeof(string));
                dts.Columns.Add("idc", typeof(string));
                foreach (DataRow s in dts.Rows)
                {

                    s["creatTimes"] = DateTime.Parse(s["creatTime"].ToString()).ToShortDateString();

                    s["idc"] = "<a href=\"arealist_upc.aspx?Cid=" + s["Cid"].ToString() + "\" class=\"list_link\">修改</a>┆<a href=\"#\" onclick=\"javascript:del('" + s["Cid"].ToString() + "');\" class=\"list_link\">删除</a>┆<input name=\"Checkbox1\" type=\"checkbox\" value=" + s["Cid"].ToString() + "  runat=\"server\" />";
                    
                }
            DataList1.DataSource = dts;
            DataList1.DataBind();
            pdel.InnerHtml = Show_pdel();
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
    string Show_pdel()
    {

        string pdel = "<a href=\"javascript:PDel();\" class=\"topnavichar\">批量删除</a>";
        return pdel;
    }
    protected void PDel()
    {
        string checkboxq = Request.Form["Checkbox1"];


        if (checkboxq == null || checkboxq == String.Empty)
        {
            PageError("请先选择要删除的稿件!", "arealist.aspx");
        }
        else
        {
            string[] chSplit = checkboxq.Split(',');
            for (int i = 0; i < chSplit.Length; i++)
            {
                if (chSplit[i] != "on")
                {
                    if (ali.Delete_3(chSplit[i]) == 0)
                    {
                        PageError("批量删除失败", "arealist.aspx");
                        break;
                    }
                }
            }
            PageRight("批量删除成功", "arealist.aspx");
        }

    }
    protected void del(string ID)
    {
        if (ali.Delete_3(ID) == 0)
        {
            PageError("批量删除失败", "arealist.aspx");
        }
        else
        {
            PageRight("删除成功!", "arealist.aspx");
        }
    }
 }        
    
   

