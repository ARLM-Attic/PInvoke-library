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
using System.IO;
using Foosun.CMS;
using Foosun.Model;

public partial class user_photo_photoclass : Foosun.Web.UI.UserPage
{
    Photo pho = new Photo();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_PageChange);
        Response.CacheControl = "no-cache";
        if (!IsPostBack)
        {
            Show_jrlist(1);
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
    protected void PageNavigator1_PageChange(object sender, int PageIndex1)
    {
        Show_jrlist(PageIndex1);
    }
    protected void Show_jrlist(int PageIndex1)
    {
        int ia, ja;
        SQLConditionInfo st = new SQLConditionInfo("@UserNum", Foosun.Global.Current.UserNum);
        DataTable jrlistdts = Foosun.CMS.Pagination.GetPage(this.GetType().Name, PageIndex1, 10, out ia, out ja, st);

        this.PageNavigator1.PageCount = ja;
        this.PageNavigator1.PageIndex = PageIndex1;
        this.PageNavigator1.RecordCount = ia;
        if (jrlistdts.Rows.Count>0)
        {
            jrlistdts.Columns.Add("cutAId1", typeof(string));
            jrlistdts.Columns.Add("idc1", typeof(string));
            jrlistdts.Columns.Add("ClassNames", typeof(string)); 
            DataTable jrid = pho.sel_16();

            foreach (DataRow r in jrlistdts.Rows)
            {
                r["cutAId1"] = pho.sel_12(r["UserName"].ToString());
                r["ClassNames"] = "<a class=\"list_link\" href=\"Photoalbumlist.aspx?ClassID=" + r["ClassID"].ToString() + "\">" + r["ClassName"].ToString() + "</a>";
                r["idc1"] = " <a class=\"list_link\" href=\"photoclass_up.aspx?ClassID=" + r["ClassID"].ToString() + "\"><img src=\"../../sysImages/folder/re.gif\" alt=\"修改\" border=\"0\" /></a>&nbsp;<a href=\"#\" onclick=\"javascript:del('" + r["ClassID"].ToString() + "');\" class=\"list_link\"><img src=\"../../sysImages/folder/dels.gif\" alt=\"删除\" border=\"0\" /></a>&nbsp;<input name=\"Checkbox1\" type=\"checkbox\" value=" + r["ClassID"].ToString() + "  runat=\"server\" />";

            }
            sc.InnerHtml = Show_sc();
            Repeater1.DataSource = jrlistdts;
            Repeater1.DataBind();
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
    string Show_sc()
    {
        string sc = "<a href=\"javascript:PDel();\" class=\"topnavichar\">批量删除</a>";
        return sc;
    }
    protected void PDel()
    {
        string checkboxq = Request.Form["Checkbox1"];


        if (checkboxq == null || checkboxq == String.Empty)
        {
            PageError("请先选择要删除的分类!", "photoclass.aspx");
        }
        else
        {
            string[] chSplit = checkboxq.Split(',');
            for (int i = 0; i < chSplit.Length; i++)
            {
                if (chSplit[i] != "on")
                {
                    DataTable pl = pho.sel_15(chSplit[i]);
                    int plcut = pl.Rows.Count;
                    for (int p = 0; p < plcut; p++)
                    {
                        DeleParentFolder(pl.Rows[p]["PhotoalbumUrl"].ToString());
                    }
                    if (pho.Delete_3(chSplit[i]) == 0 || pho.Delete_4(chSplit[i]) == 0)
                    {
                        PageError("批量删除失败", "photoclass.aspx");
                        break;
                    }
                }
            }
            PageRight("批量删除成功", "photoclass.aspx");
        }

    }
    protected void del(string ID)
    {
        DataTable pl = pho.sel_15(ID);
        int plcut=pl.Rows.Count;
        for (int p = 0; p < plcut; p++)
        {
           DeleParentFolder(pl.Rows[p]["PhotoalbumUrl"].ToString());  
        }
        pho.Delete_3(ID);
        pho.Delete_4(ID);
        PageRight("删除成功!", "photoclass.aspx");
    }
    public void DeleParentFolder(string Url)
    {
        try
        {
            DirectoryInfo DelFolder = new DirectoryInfo(System.Web.HttpContext.Current.Server.MapPath(Url).ToString());
            if (DelFolder.Exists)
            {
                DelFolder.Delete();
            }
        }
        catch
        {
        }
    } 

}



