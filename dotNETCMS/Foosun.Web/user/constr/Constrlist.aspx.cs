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

public partial class user_constr_Constrlist : Foosun.Web.UI.UserPage
{
    Constr con = new Constr();
    Foosun.CMS.Common.rootPublic pd = new Foosun.CMS.Common.rootPublic();
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    ///  
    #region 初始化
    protected void Page_Load(object sender, EventArgs e)
    {
        
        Response.CacheControl = "no-cache";
        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_PageChange);
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
            case "Lock":            //锁定
                Lock(ID);
                break;
            case "UnLock":          //解锁
                UnLock(ID);
                break;
            case "del":          //删除
                del(ID);
                break;
            case "PDel":            //批量删除
                PDel();
                break;
            case "PUnlock":         //批量解锁
                PUnlock();
                break;
            case "Plock":           //批量锁定
                Plock();
                break;
            default:
                break;
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
        string UserNum = Foosun.Global.Current.UserNum;
        string ClassID = "";
        if (Request.QueryString["ClassID"] != null && Request.QueryString["ClassID"] != "")
        {
            ClassID = Request.QueryString["ClassID"].ToString();
        }
        int i, j;
        DataTable dt = con.GetPage(UserNum, ClassID, PageIndex, 10, out i, out j, null);
        this.PageNavigator1.PageCount = j;
        this.PageNavigator1.PageIndex = PageIndex;
        this.PageNavigator1.RecordCount = i;
        if (dt != null && dt.Rows.Count != 0)
        {
            dt.Columns.Add("Titles", typeof(string));
            //
            dt.Columns.Add("cNames", typeof(string));
            dt.Columns.Add("idc", typeof(string));
            dt.Columns.Add("isChecks", typeof(string));
            for (int s = 0; s < dt.Rows.Count; s++)
            {

                dt.Rows[s]["Titles"] = "<a href=\"../show/showcontent.aspx?ConID=" + dt.Rows[s]["ConID"].ToString() + "&uid=" + pd.getUserName(dt.Rows[s]["UserNum"].ToString()) + "&ClassID=" + dt.Rows[s]["ClassID"].ToString() + "\" class=\"list_link\" target=\"_blank\" title=\"点击浏览文章\">" + dt.Rows[s]["title"].ToString() + "</a>";
                dt.Rows[s]["cNames"] = "<a href=\"?ClassID=" + dt.Rows[s]["ClassID"].ToString() + "\" class=\"list_link\">" + con.sel_cName(dt.Rows[s]["ClassID"].ToString()) + "</a>";
                string fbs = "";
                string tages2 = "";
                string[] tags = dt.Rows[s]["Contrflg"].ToString().Split('|');
                int fb = int.Parse(tags[1].ToString());
                if (fb == 1)
                {
                    fbs = "<img src=\"../../sysimages/folder/yes.gif\"  title=\"总站投稿\">";
                }
                else
                {
                    fbs = "<img src=\"../../sysimages/folder/no.gif\"  title=\"不是投稿\">";
                }
                int tags1 = int.Parse(tags[2].ToString());
                if (tags1 == 1)
                {
                    tages2 = "<img src=\"../../sysimages/folder/no.gif\" title=\"锁定\">";
                }
                else
                {
                    tages2 = "<img src=\"../../sysimages/folder/yes.gif\" title=\"开放\">";
                }

                int tages3 = int.Parse(tags[3].ToString());
                string tages31 = "";
                if (tages3 == 0)
                {
                    tages31 = "<img src=\"../../sysimages/folder/no.gif\" title=\"不是推荐\">";
                }
                else
                {
                    tages31 = "<img src=\"../../sysimages/folder/yes.gif\" title=\"推荐\">";
                }
                string isChecka = "";
                int p = int.Parse(dt.Rows[s]["isCheck"].ToString());
                if (p == 0)
                {
                    isChecka = "<img src=\"../../sysimages/folder/no.gif\" title=\"总站未采用或不是投稿\">";
                }
                else
                {
                    isChecka = "<img src=\"../../sysimages/folder/yes.gif\" title=\"总站采用的投稿\">";
                }
                string isChecka1 = "";
                if (int.Parse(dt.Rows[s]["ispass"].ToString()) == 1)
                {
                    isChecka1 = "<a href=\"Constrlistpass_DC.aspx?ConID=" + dt.Rows[s]["ConID"].ToString() + "\"><img src=\"../../sysimages/folder/yes.gif\" border=\"0\" class=\"list_link\" title=\"此稿件已被退稿&#13;点击查看退稿理由！\"></a>";
                }
                else
                {
                    isChecka1 = "<img src=\"../../sysimages/folder/no.gif\" title=\"此稿件未被退稿.&#13;或者不是投稿\">";
                }
                dt.Rows[s]["isChecks"] = isChecka + "&nbsp;&nbsp;&nbsp;&nbsp;" + tages2 + "&nbsp;&nbsp;&nbsp;&nbsp;" + fbs + "&nbsp;&nbsp;&nbsp;&nbsp;" + isChecka1 + "&nbsp;&nbsp;&nbsp;&nbsp;" + tages31;
                dt.Rows[s]["idc"] = "<a class=\"list_link\" href=\"Constr_up.aspx?ConID=" + dt.Rows[s]["ConID"].ToString() + "\"><img src=\"../../sysImages/folder/re.gif\" alt=\"修改\" border=\"0\" /></a>&nbsp;<a href=\"#\" onclick=\"javascript:UnLock('" + dt.Rows[s]["ConID"].ToString() + "');\" class=\"list_link\" title=\"锁定此投稿\"><Img src=\"../../sysImages/folder/lock.gif\" title=\"锁定\" border=\"0\"/></a>&nbsp;<a href=\"#\" onclick=\"javascript:Lock('" + dt.Rows[s]["ConID"].ToString() + "');\" class=\"list_link\" title=\"解锁此投稿\"><Img src=\"../../sysImages/folder/unlock.gif\" title=\"解锁\" border=\"0\"/></a>&nbsp;<a href=\"#\" onclick=\"javascript:del('" + dt.Rows[s]["ConID"].ToString() + "');\" class=\"list_link\" title=\"删除此投稿\"><img src=\"../../sysImages/folder/dels.gif\" alt=\"删除\" border=\"0\" /></a>&nbsp;<input name=\"Checkbox1\" type=\"checkbox\" value=" + dt.Rows[s]["ConID"].ToString() + "  runat=\"server\" />";

            }  
            DataList1.DataSource = dt;
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
    /// 锁定
    /// </summary>
    /// <param name="ID"></param>
    /// 
    #region 锁定
    protected void Lock(string ID)
    {
        string[] sel_Tags = con.sel_Tags(ID).Split('|');
        string sel_Tagss = sel_Tags[0].ToString();
        string sel_Tags1 = sel_Tags[1].ToString();
        string sel_Tags2 = "0";
        string sel_Tags3 = sel_Tags[3].ToString();
        string tagsd = sel_Tagss + "|" + sel_Tags1 + "|" + sel_Tags2 + "|" + sel_Tags3;
        if (con.Update_Tage1(tagsd, ID) == 0)
        {
            PageError("锁定失败", "ConstrList.aspx");
        }
        else
        {
            PageRight("锁定成功!", "ConstrList.aspx");
        }
    }
    #endregion
    /// <summary>
    /// 解锁
    /// </summary>
    /// <param name="ID"></param>
    /// 
    #region 解锁
    protected void UnLock(string ID)
    {
        string[] sel_Tags = con.sel_Tags(ID).Split('|');
        string sel_Tagss = sel_Tags[0].ToString();
        string sel_Tags1 = sel_Tags[1].ToString();
        string sel_Tags2 = "1";
        string sel_Tags3 = sel_Tags[3].ToString();
        string tagsd = sel_Tagss + "|" + sel_Tags1 + "|" + sel_Tags2 + "|" + sel_Tags3;
        if (con.Update_Tage1(tagsd, ID) == 0)
        {
            PageError("锁定失败", "ConstrList.aspx");
        }
        else
        {
            PageRight("解锁成功!", "ConstrList.aspx");
        }
    }
    #endregion
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="ID"></param>
    /// 
    #region 删除
    protected void del(string ID)
    {

        if (con.Delete2(ID)==0)
        {
            PageError("删除失败", "ConstrList.aspx");
        }
        else
        {
            PageRight("删除成功!", "ConstrList.aspx");
        }
    }
    #endregion
    /// <summary>
    /// 批量删除稿件
    /// </summary>
    /// 
    #region 批量删除稿件
    protected void PDel()
    {
        string checkboxq = Request.Form["Checkbox1"];
        if (checkboxq == null || checkboxq == String.Empty)
        {
            PageError("请先选择要锁定的稿件!", "ConstrList.aspx");
        }
        else
        {
            string[] chSplit = checkboxq.Split(',');
            for (int i = 0; i < chSplit.Length; i++)
            {
                if (chSplit[i] != "on")
                {
                    if (con.Delete2(chSplit[i])==0)
                    {
                        PageError("稿件批量删除失败", "ConstrList.aspx");
                        break;
                    }
                }
            }
            PageRight("稿件批量删除成功", "ConstrList.aspx");
        }
    }
    #endregion
    /// <summary>
    /// 批量锁定稿件
    /// </summary>
    /// 
    #region 批量锁定稿件
    protected void Plock()
    {
        string checkboxq = Request.Form["Checkbox1"];  
        if (checkboxq == null || checkboxq == String.Empty)
        {
            PageError("请先选择要锁定的稿件!", "ConstrList.aspx");
        }
        else
        {
            string[] chSplit = checkboxq.Split(',');
            for (int i = 0; i < chSplit.Length; i++)
            {
                if (chSplit[i] != "on")
                {
                    string[] sel_Tags = con.sel_Tags(chSplit[i]).Split('|');
                    string sel_Tagss = sel_Tags[0].ToString();
                    string sel_Tags1 = sel_Tags[1].ToString();
                    string sel_Tags2 = "0";
                    string sel_Tags3 = sel_Tags[3].ToString();
                    string tagsd = sel_Tagss + "|" + sel_Tags1 + "|" + sel_Tags2 + "|" + sel_Tags3;
                    if (con.Update_Tage1(tagsd, chSplit[i]) == 0)
                    {

                        PageRight("批量锁定失败!错误原因:<br />", "ConstrList.aspx");
                    }
                }

            } PageRight("批量锁定成功!", "ConstrList.aspx");
        }
    }
    #endregion
    /// <summary>
    /// 批量解锁稿件
    /// </summary>
    /// 
    #region 批量解锁稿件
    protected void PUnlock()
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
                    string[] sel_Tags = con.sel_Tags(chSplit[i]).Split('|');
                    string sel_Tagss = sel_Tags[0].ToString();
                    string sel_Tags1 = sel_Tags[1].ToString();
                    string sel_Tags2 = "1";
                    string sel_Tags3 = sel_Tags[3].ToString();
                    string tagsd = sel_Tagss + "|" + sel_Tags1 + "|" + sel_Tags2 + "|" + sel_Tags3;
                    if (con.Update_Tage1(tagsd, chSplit[i]) == 0)
                    {

                        PageRight("批量解锁失败!错误原因:<br />", "ConstrList.aspx");
                    }
                }
            } 
            PageRight("批量解锁成功!", "");
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
        nos = nos + "<td class='navi_link'>没有投稿</td>";
        nos = nos + "</tr>";
        nos = nos + "</table>";
        return nos;
    }
    #endregion
}