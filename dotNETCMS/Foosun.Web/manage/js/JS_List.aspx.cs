using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Drawing;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Foosun.Model;

public partial class manage_js_JS_List : Foosun.Web.UI.ManagePage
{
    public manage_js_JS_List()
    {
        Authority_Code = "C051";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_OnPageChange);
        Response.CacheControl = "no-cache"; //清除缓存
        if (Request.Form["Option"] != null && Request.Form["Option"].Equals("DeleteJS") && Request.Form["JSID"] != null && !Request.Form["JSID"].Trim().Equals(""))
        {
            this.Authority_Code = "C054";
            this.CheckAdminAuthority();
            Foosun.CMS.NewsJS nj = new Foosun.CMS.NewsJS();
            try
            {
                nj.Delete(Request.Form["JSID"].Trim());
                Response.Write("1%成功删除指定JS");
            }
            catch(Exception ex)
            {
                Response.Write("0%" + ex.Message);
            }
            Response.End();
            return;
        }
        if (!IsPostBack)
        {
            this.HidType.Value = "-1";
            this.LnkBtnALL.Enabled = false;
            DataListBind(1);
        }
    }

    /// <summary>
    /// 分页
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="IndexPage"></param>

    protected void PageNavigator1_OnPageChange(object sender, int IndexPage)
    {
        DataListBind(IndexPage);
    }

    /// <summary>
    /// 管理页
    /// </summary>
    /// <param name="PageIndex"></param>
    /// code by chenzhaohui

    protected void DataListBind(int PageIndex)
    {
        int RdCount = 0, PgCount = 0;
        Foosun.CMS.NewsJS nj = new Foosun.CMS.NewsJS();
        IList<NewsJSInfo> ljs = nj.GetPage(PageIndex, PAGESIZE, out RdCount, out PgCount, int.Parse(this.HidType.Value));
        this.PageNavigator1.RecordCount = RdCount;
        this.PageNavigator1.PageCount = PgCount;
        this.PageNavigator1.PageIndex = PageIndex;
        foreach (NewsJSInfo r in ljs)
        {
            TableRow tr = new TableRow();
            tr.CssClass = "TR_BG_list";
            tr.Attributes.Add("onmouseover", "overColor(this)");
            tr.Attributes.Add("onmouseout", "outColor(this)");

            TableCell td1 = new TableCell();
            td1.CssClass = "list_link";
            td1.Text = r.JSName;
            tr.Cells.Add(td1);
            TableCell td2 = new TableCell();
            td2.CssClass = "list_link";
            td2.HorizontalAlign = HorizontalAlign.Center;
            if (r.jsType.Equals(0))
                td2.Text = "系统JS";
            else if (r.jsType.Equals(1))
                td2.Text = "自由JS";
            else
                td2.Text = "未知类型";
            tr.Cells.Add(td2);
            TableCell td3 = new TableCell();
            td3.CssClass = "list_link";
            td3.Text = "<a href=\"javascript:GetJSCode(" + r.Id.ToString() + ");\" class=\"list_link\">代码</a>";
            td3.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(td3);
            TableCell td4 = new TableCell();
            td4.CssClass = "list_link";
            if (!r.jsType.Equals(1))
                td4.Text = r.jsNum.ToString();
            else
                td4.Text = "<a href=\"javascript:ShowNewsJs("+ r.Id.ToString() +");\" class=\"list_link\">" + r.ActualNum.ToString() + "(<font color=\"red\">" + r.jsNum + "</font>)[查看]</a>";
            td4.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(td4);
            TableCell td5 = new TableCell();
            td5.CssClass = "list_link";
            td5.Text = r.CreatTime.ToString();
            td5.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(td5);
            TableCell td6 = new TableCell();
            td6.CssClass = "list_link";
            td6.Text = "<a href=\"JS_Add.aspx?ID=" + r.Id.ToString() + "\" class=\"list_link\"><img src=\"../../sysImages/"+Foosun.Config.UIConfig.CssPath()+"/sysico/edit.gif\" border=\"0\" alt=\"修改\" /></a> <a href=\"javascript:DeleteJS(" + r.Id.ToString() + ");\" class=\"list_link\"><img src=\"../../sysImages/folder/dels.gif\" border=\"0\" alt=\"彻底删除\" /></a> <input type=\"checkbox\" name=\"checkbox\" value=\""+ r.Id.ToString() +"\"/>";
            td6.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(td6);
            this.TabData.Rows.Add(tr);
        }
    }
    protected void LnkBtnALL_Click(object sender, EventArgs e)
    {
        this.HidType.Value = "-1";
        this.LnkBtnALL.Enabled = false;
        this.LnkBtnFree.Enabled = true;
        this.LnkBtnSys.Enabled = true;
        DataListBind(1);

    }
    protected void LnkBtnSys_Click(object sender, EventArgs e)
    {
        this.HidType.Value = "0";
        this.LnkBtnALL.Enabled = true; ;
        this.LnkBtnFree.Enabled = true;
        this.LnkBtnSys.Enabled = false;
        DataListBind(1);
    }
    protected void LnkBtnFree_Click(object sender, EventArgs e)
    {
        this.HidType.Value = "1";
        this.LnkBtnALL.Enabled = true; ;
        this.LnkBtnFree.Enabled = false;
        this.LnkBtnSys.Enabled = true;
        DataListBind(1);
    }
}
