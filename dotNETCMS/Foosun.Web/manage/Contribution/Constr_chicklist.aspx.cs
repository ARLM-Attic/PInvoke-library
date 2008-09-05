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

public partial class manage_Contribution_Constr_chicklist : Foosun.Web.UI.ManagePage
{
    public manage_Contribution_Constr_chicklist()
    {
        Authority_Code = "C043";
    }
    Constr con = new Constr();
    protected void Page_Load(object sender, EventArgs e)
    {

        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_PageChange);
        Response.CacheControl = "no-cache";
        if (!IsPostBack)
        {
            Showu_Constrlist(1);
        }
        string Type = Request.QueryString["Type"];  //取得操作类型
        string ID = "";
        if (Request.QueryString["ID"] != "" && Request.QueryString["ID"] != null)
        {
            ID = Request.QueryString["ID"];  //取得需要操作的稿件ID
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
        Showu_Constrlist(PageIndex);
    }

    protected void Showu_Constrlist(int PageIndex)//显示所有讨论组列表
    {
        int i, j;

        DataTable dts = Foosun.CMS.Pagination.GetPage(this.GetType().Name, PageIndex, 10, out i, out j, null);

        this.PageNavigator1.PageCount = j;
        this.PageNavigator1.PageIndex = PageIndex;
        this.PageNavigator1.RecordCount = i;
        if (dts != null && dts.Rows.Count != 0)
        {
            dts.Columns.Add("info", typeof(string));
            dts.Columns.Add("constrTime", typeof(string));
            dts.Columns.Add("Sourcetype", typeof(string));
            dts.Columns.Add("constrlevel", typeof(string));
            dts.Columns.Add("userName", typeof(string));
            dts.Columns.Add("islocks", typeof(string));
            dts.Columns.Add("handle", typeof(string));
            foreach (DataRow s in dts.Rows)
            {
                s["constrTime"] = s["creatTime"].ToString();
                s["Sourcetype"] = s["Source"].ToString();
                string[] tagss = s["Contrflg"].ToString().Split('|');
                int infos = int.Parse(tagss[0].ToString());
                if (infos == 0) { s["constrlevel"] = "普通"; }
                else if (infos == 1) { s["constrlevel"] = "优先"; }
                else { s["constrlevel"] = "加急"; }
                rootPublic pd = new rootPublic();
                s["userName"] = "<a class=\"list_link\" target=\"_blank\" href=\"../../" + Foosun.Config.UIConfig.dirUser + "/showuser-" + pd.getUserName(s["UserNum"].ToString()) + ".aspx\">" + pd.getUserName(s["UserNum"].ToString()) + "</a>";

                int l = int.Parse(tagss[2].ToString());
                if (l == 1) { s["islocks"] = "<span class=\"reshow\">是</span>"; }
                else { s["islocks"] = "否"; }
                s["handle"] = "<a href=\"javascript:del('" + s["ConID"].ToString() + "');\" class=\"list_link\">删除</a>┆<input name=\"Checkbox1\" type=\"checkbox\" value=" + s["ConID"].ToString() + "  runat=\"server\" />";
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
            PageError("请先选择要删除的稿件!", "");
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
            PageRight("稿件批量删除成功", "Constr_chicklist.aspx");
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
