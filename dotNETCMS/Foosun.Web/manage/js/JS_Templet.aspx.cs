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

public partial class manage_js_JS_Templet : Foosun.Web.UI.ManagePage
{
    public manage_js_JS_Templet()
    {
        Authority_Code = "C055";
    }
    private Foosun.CMS.JSTemplet jt;
    protected void Page_Load(object sender, EventArgs e)
    {

        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_OnPageChange);
        Response.CacheControl = "no-cache"; //清除缓存
        jt = new Foosun.CMS.JSTemplet();
        if (Request.Form["Option"] != null && Request.Form["ID"] != null)
        {
            try
            {
                string id = Request.Form["ID"];
                switch (Request.Form["Option"])
                {
                    case "DeleteJSTmpClass":
                        jt.ClassDelete(id);
                        Response.Write("1%成功删除一个JS模型分类及其子分类和所属JS模型!");
                        break;
                    case "DeleteJSTemplet":
                        jt.Delete(int.Parse(id));
                        Response.Write("1%成功删除一个JS模型!");
                        break;
                }
            }
            catch (Exception ex)
            {
                Response.Write("0%" + ex.Message);
            }
            Response.End();
        }
        if (!IsPostBack)
        {
            DataTable tb = jt.ClassList();
            ClassRender(tb, "0", 0);
            DataListBind(1);
        }
    }

    protected void PageNavigator1_OnPageChange(object sender, int IndexPage)
    {
        DataListBind(IndexPage);
    }


    /// <summary>
    /// 分类管理页
    /// </summary>
    /// <param name="PageIndex"></param>
    /// code by chenzhaohui

    protected void DataListBind(int PageIndex)
    {
        int RCount=0, PCount = 0;
        DataTable tb = jt.GetPage(PageIndex, PAGESIZE, out RCount, out PCount, this.DdlClass.SelectedValue);
        this.PageNavigator1.RecordCount = RCount;
        this.PageNavigator1.PageCount = PCount;
        this.PageNavigator1.PageIndex = PageIndex;
        foreach (DataRow r in tb.Rows)
        {
            TableRow tr = new TableRow();
            tr.CssClass = "TR_BG_list";
            tr.Attributes.Add("onmouseover", "overColor(this)");
            tr.Attributes.Add("onmouseout", "outColor(this)");

            TableCell td1 = new TableCell();
            td1.CssClass = "list_link";
            td1.Text = r["CName"].ToString();
            tr.Cells.Add(td1);
            TableCell td2 = new TableCell();
            td2.CssClass = "list_link";
            td2.HorizontalAlign = HorizontalAlign.Center;
            int n = int.Parse(r["JSTType"].ToString());
            if (n == 0)
                td2.Text = "系统JS模型";
            else if (n == 1)
                td2.Text = "自由JS模型";
            else
                td2.Text = "有"+ r["NumCLS"].ToString() +"个分类,"+ r["NumTMP"].ToString() +"个模型";
            tr.Cells.Add(td2);            
            TableCell td3 = new TableCell();
            td3.CssClass = "list_link";
            td3.Text = r["CreatTime"].ToString();
            td3.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(td3);
            TableCell td4 = new TableCell();
            td4.CssClass = "list_link";
            if (n == 0 || n == 1)
            {
                td4.Text = "<a href=\"JS_Templet_Add.aspx?ID=" + r["id"].ToString() + "\" class=\"list_link\"><img src=\"../../sysImages/"+Foosun.Config.UIConfig.CssPath()+"/sysico/edit.gif\" border=\"0\" alt=\"修改\" /></a> <a href=\"javascript:DeleteTmp(" + r["ID"].ToString() + ");\" class=\"list_link\"><img src=\"../../sysImages/folder/dels.gif\" border=\"0\" alt=\"彻底删除\" /></a>";
            }
            else
            {
                td4.Text = "<a href=\"javascript:GoToClass('" + r["TmpID"].ToString() + "');\" class=\"list_link\"><img src=\"../../sysImages/"+Foosun.Config.UIConfig.CssPath()+"/sysico/enter.gif\" border=\"0\" alt=\"进入\" /></a> <a href=\"JS_Templet_Class.aspx?ID=" + r["id"].ToString() + "\" class=\"list_link\"><img src=\"../../sysImages/"+Foosun.Config.UIConfig.CssPath()+"/sysico/edit.gif\" border=\"0\" alt=\"修改\" /></a> <a href=\"javascript:DeleteClass('" + r["TmpID"].ToString() + "');\" class=\"list_link\"><img src=\"../../sysImages/folder/dels.gif\" border=\"0\" alt=\"彻底删除\" /></a>";
            }
            td4.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(td4);
            this.TableData.Rows.Add(tr);
        }
    }
    /// <summary>
    /// 递归
    /// </summary>
    /// <param name="tb"></param>
    /// <param name="PID"></param>
    /// <param name="Layer"></param>
    private void ClassRender(DataTable tb, string PID, int Layer)
    {
        DataRow[] row = tb.Select("ParentID='" + PID + "'");
        if (row.Length < 1)
            return;
        else
        {
            foreach (DataRow r in row)
            {
                ListItem it = new ListItem();
                it.Value = r["ClassID"].ToString();
                string stxt = "├";
                for (int i = 0; i < Layer; i++)
                {
                    stxt += "─";
                }
                it.Text = stxt + r["CName"].ToString();
                this.DdlClass.Items.Add(it);
                ClassRender(tb, r["ClassID"].ToString(), Layer + 1);
            }
        }
    }

    protected void DdlClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataListBind(1);
    }
}
