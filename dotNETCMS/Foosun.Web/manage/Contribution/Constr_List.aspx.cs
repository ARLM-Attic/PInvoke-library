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

public partial class manage_Contribution_Constr_List : Foosun.Web.UI.ManagePage
{
    public manage_Contribution_Constr_List()
    {
        Authority_Code = "C042";
    }
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
        int i, j;
        DataTable dts = Foosun.CMS.Pagination.GetPage(this.GetType().Name, PageIndex, 10, out i, out j, null);
        this.PageNavigator1.PageCount = j;
        this.PageNavigator1.PageIndex = PageIndex;
        this.PageNavigator1.RecordCount = i;
        if (dts != null && dts.Rows.Count != 0)
        {
            dts.Columns.Add("info", typeof(string));
            dts.Columns.Add("lock", typeof(string));
            dts.Columns.Add("groom", typeof(string));
            dts.Columns.Add("handle", typeof(string));
            dts.Columns.Add("ispassa", typeof(string));
            dts.Columns.Add("ctim", typeof(string));
            foreach (DataRow s in dts.Rows)
            {
                string[] tagss = s["Contrflg"].ToString().Split('|');
                int infos = int.Parse(tagss[0].ToString());
                if (infos == 0)
                {
                    s["info"] = "普通";
                }
                else if (infos == 1)
                {
                    s["info"] = "优先";
                }
                else
                {
                    s["info"] = "加急";
                }


                int l = int.Parse(tagss[2].ToString());
                if (l == 1)
                {
                    s["lock"] = "<img src=\"../../sysImages/folder/yes.gif\" alt=\"未锁定\" border=\"0\" />";
                }
                else
                {
                    s["lock"] = "<img src=\"../../sysImages/folder/no.gif\" alt=\"已锁定\" border=\"0\" />";
                }
                int g = int.Parse(tagss[3].ToString());
                if (g == 1)
                {
                    s["groom"] = "<img src=\"../../sysImages/folder/yes.gif\" alt=\"推荐\" border=\"0\" />";
                }
                else
                {
                    s["groom"] = "<img src=\"../../sysImages/folder/no.gif\" alt=\"非推荐\" border=\"0\" />";
                }
                int isp = int.Parse(s["ispass"].ToString());
                if (isp == 0)
                {
                    s["ispassa"] = "<img src=\"../../sysImages/folder/no.gif\" alt=\"未退稿\" border=\"0\" />";
                }
                else
                {
                    s["ispassa"] = "<img src=\"../../sysImages/folder/yes.gif\" alt=\"已退稿\" border=\"0\" />";
                }
                s["handle"] = "<a href=\"Constr_Edit.aspx?ConID=" + s["ConID"].ToString() + "\" class=\"menulist\">编辑审核</a>┆<a href=\"Constr_ToNews.aspx?ConID=" + s["ConID"].ToString() + "\" class=\"menulist\">直接审核</a>┆<a href=\"Constr_Return.aspx?ConID=" + s["ConID"].ToString() + "\" class=\"menulist\">退稿</a>┆<a href=\"javascript:del('" + s["ConID"].ToString() + "');\" class=\"list_link\">删除</a>┆<input name=\"Checkbox1\" type=\"checkbox\" value=" + s["ConID"].ToString() + "  runat=\"server\" />";
                if (!s.IsNull("creatTime"))
                {
                    s["ctim"] = DateTime.Parse(s["creatTime"].ToString()).ToShortDateString();
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
                    DataTable da_del = con.sel10(chSplit[i]);
                    int isadmidel = int.Parse(da_del.Rows[0]["isadmidel"].ToString());
                    int isuserdel = int.Parse(da_del.Rows[0]["isuserdel"].ToString());
                    if (isadmidel == 0 && isuserdel == 1)
                    {
                        if (con.Delete4(chSplit[i]) == 0)
                        {
                            rd.SaveUserAdminLogs(1, 1, UserNum, "删除投稿", "删除失败");
                            PageError("稿件批量删除失败", "");
                            break;
                        }
                    }
                    else
                    {
                        if (con.Update3(chSplit[i]) == 0)
                        {
                            rd.SaveUserAdminLogs(1, 1, UserNum, "删除投稿", "删除失败");
                            PageError("稿件批量删除失败", "");
                            break;
                        }
                    }
                }
            }
            rd.SaveUserAdminLogs(1, 1, UserNum, "删除投稿", "删除成功");
            PageRight("稿件批量删除成功", "Constr_List.aspx");
        }

    }
    protected void del(string ID)
    {
        rootPublic rd = new rootPublic();
        DataTable da_del = con.sel10(ID);
        int isadmidel = int.Parse(da_del.Rows[0]["isadmidel"].ToString());
        int isuserdel = int.Parse(da_del.Rows[0]["isuserdel"].ToString());
        if (isadmidel == 0 && isuserdel == 1)
        {
            if (con.Delete4(ID) == 0)
            {
                rd.SaveUserAdminLogs(1, 1, UserNum, "删除投稿", "删除失败");
                PageError("稿件删除失败", "");
            }
            else
            {
                rd.SaveUserAdminLogs(1, 1, UserNum, "删除投稿", "删除成功");
                PageRight("稿件删除成功", "");
            }
        }
        else
        {
            if (con.Update3(ID) == 0)
            {
                rd.SaveUserAdminLogs(1, 1, UserNum, "删除投稿", "删除失败");
                PageError("稿件删除失败", "");
            }
            else
            {
                rd.SaveUserAdminLogs(1, 1, UserNum, "删除投稿", "删除成功");
                PageRight("稿件删除成功", "");
            }
        }
    }
}
