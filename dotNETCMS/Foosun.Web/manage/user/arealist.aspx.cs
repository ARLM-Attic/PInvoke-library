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

public partial class manage_user_arealist : Foosun.Web.UI.ManagePage
{
    public manage_user_arealist()
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
    #region 分页显示
    protected void PageNavigator1_PageChange(object sender, int PageIndex)
    {
        Showu_discusslist(PageIndex);
    }
      protected void Showu_discusslist(int PageIndex)
      {   
            int ig, js;
            DataTable dts = Foosun.CMS.Pagination.GetPage(this.GetType().Name, PageIndex, 10, out ig, out js, null);

            this.PageNavigator1.PageCount = js;
            this.PageNavigator1.PageIndex = PageIndex;
            this.PageNavigator1.RecordCount = ig;
            if (dts != null && dts.Rows.Count!=0)
            {
                dts.Columns.Add("creatTimes", typeof(string));
                dts.Columns.Add("idc", typeof(string));
                dts.Columns.Add("cm", typeof(string));
                foreach (DataRow s in dts.Rows)
                {
                    string show = null;
                    if (!s.IsNull("creatTime"))
                    {
                        s["creatTimes"] = DateTime.Parse(s["creatTime"].ToString()).ToShortDateString();
                    }
                    else 
                    {
                        s["creatTimes"] = s["creatTime"].ToString();
                    }

                    s["idc"] = "<a href=\"arealist_upp.aspx?Cid=" + s["Cid"].ToString() + "\" class=\"list_link\" title=\"修改\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/edit.gif\" border=\"0\" alt=\"修改\" /></a><a href=\"arealist_cadd.aspx?Cid=" + s["Cid"].ToString() + "&type=1\" class=\"list_link\"　title=\"添加小类\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/addclass.gif\" border=\"0\" alt=\"添加小类\" /></a><a href=\"#\" onclick=\"javascript:del('" + s["Cid"].ToString() + "');\" title=\"删除\");\" class=\"list_link\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/del.gif\" border=\"0\" alt=\"删除\" /></a><input name=\"Checkbox1\" type=\"checkbox\" value=" + s["Cid"].ToString() + "  runat=\"server\" />";
                    show += "<tr class=\"TR_BG_list\"  onmouseover=\"overColor(this)\" onmouseout=\"outColor(this)\">";
                    show += "<td align=\"left\" width=\"20%\">" + s["cityName"].ToString() + "</td>";
                    show += "<td align=\"center\" width=\"40%\">" + s["creatTimes"] + "</td>";
                    show += "<td align=\"center\" width=\"40%\">"+s["idc"]+"</td>";
                    show += "</tr>";
                    show += showcity(s["Cid"].ToString(), "├");
                    s["cm"] = show;
                    
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
    #endregion

    #region  递归
    string showcity(string Cid, string shows)
    {
        String show = null;
        shows += "─";
        DataTable dv = ali.sel(Cid);
        dv.Columns.Add("oPerate", typeof(String));
        dv.Columns.Add("creatTimes", typeof(string));
        dv.Columns.Add("idc", typeof(string));    
        foreach (DataRow x in dv.Rows)
        {
            if (!x.IsNull("creatTime"))
            {
                x["creatTimes"] = DateTime.Parse(x["creatTime"].ToString()).ToShortDateString();
            }
            else 
            {
                x["creatTimes"] = x["creatTime"].ToString();
            }

            x["idc"] = "<a href=\"arealist_upc.aspx?Cid=" + x["Cid"].ToString() + "\" class=\"list_link\" title=\"修改\"><img src=\"../../sysImages/folder/re.gif\" border=\"0\" alt=\"修改\" /></a><a href=\"arealist_cadd.aspx?Cid=" + x["Cid"].ToString() + "&type=1\" class=\"list_link\"　title=\"添加小类\"><img src=\"../../sysImages/folder/new.gif\" border=\"0\" alt=\"添加小类\" /></a><a href=\"#\" onclick=\"javascript:del('" + x["Cid"].ToString() + "');\" title=\"删除\");\" class=\"list_link\"><img src=\"../../sysImages/folder/no.gif\" border=\"0\" alt=\"删除\" /></a><input name=\"Checkbox1\" type=\"checkbox\" value=" + x["Cid"].ToString() + "  runat=\"server\" />";
            show += "<tr class=\"TR_BG_list\"  onmouseover=\"overColor(this)\" onmouseout=\"outColor(this)\">";
            show += "<td align=\"left\" width=\"20%\">" + shows + x["cityName"].ToString() + "</td>";
            show += "<td align=\"center\" width=\"40%\">"+x["creatTimes"]+"</td>";
            show += "<td align=\"center\" width=\"40%\">"+x["idc"]+"</td>";
            show += "</tr>";
            show += showcity(x["Cid"].ToString(), shows);
        }
        return show;
    }
    # endregion

    #region 删除
    protected void PDel()
    {
        string checkboxq = Request.Form["Checkbox1"];
        rootPublic rd = new rootPublic();

        if (checkboxq == null || checkboxq == String.Empty)
        {
            PageError("请先选择要删除的项!", "");
        }
        else
        {
            string[] chSplit = checkboxq.Split(',');
            for (int i = 0; i < chSplit.Length; i++)
            {
                if (chSplit[i] != "on")
                {
                    if (ali.Delete_2(chSplit[i]) == 0)
                    {
                        rd.SaveUserAdminLogs(1, 1, UserNum, "地域大类管理", "删除失败");
                        PageError("批量删除失败", "");
                        break;
                    }
                }
            }
            rd.SaveUserAdminLogs(1, 1, UserNum, "地域大类管理", "删除成功");
            PageRight("批量删除成功", "");
        }

    }
    protected void del(string ID)
    {
        rootPublic rd = new rootPublic();
        if (ali.sel_1(ID) == "0")
        {
            if (ali.Delete(ID) == 0)
            {
                rd.SaveUserAdminLogs(1, 1, UserNum, "地域大类管理", "删除失败");
                PageError("批量删除失败", "");
            }
            else
            {
                rd.SaveUserAdminLogs(1, 1, UserNum, "地域大类管理", "删除成功");
                PageRight("删除成功!", "");
            }
        }
        else 
        {
            if (ali.Delete_2(ID) == 0)
            {
                rd.SaveUserAdminLogs(1, 1, UserNum, "地域大类管理", "删除失败");
                PageError("批量删除失败", "");
            }
            else
            {
                rd.SaveUserAdminLogs(1, 1, UserNum, "地域大类管理", "删除成功");
                PageRight("删除成功!", "");
            }
        }
    }
    #endregion

    #region
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
    #endregion
}        
    
   

