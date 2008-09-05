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

public partial class manage_Sys_DefineTable_List : Foosun.Web.UI.ManagePage
{
    public manage_Sys_DefineTable_List()
    {
        Authority_Code = "Q033";
    }
    DefineTable def = new DefineTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        this.PageNavig.OnPageChange+=new PageChangeHandler(PageNavig_OnPageChange);
        if (!IsPostBack)
        {
            
        }
        #region ---
        string action = Request.QueryString["action"];
        if (action == "del")
        {
            DeloNe();
        }
        #endregion
        DataSource(1);
    }

    protected void PageNavig_OnPageChange(object sender, int PageIndex)
    {
        DataSource(PageIndex);
    }

    protected void DataSource(int PageIndex)
    {
        int i = 0;
        int k = 0;

        #region 参数，显示相应ID下的字段
        string defid = Request.QueryString["pr"];
        #endregion
        DataTable dt = def.GetPage(defid, PageIndex, 20, out i, out k, null);
        this.PageNavig.RecordCount = i;
        this.PageNavig.PageIndex = PageIndex;
        this.PageNavig.PageCount = k;
        if (dt != null && dt.Rows.Count > 0)
        {
            dt.Columns.Add("type", typeof(string));
            dt.Columns.Add("IsNullC", typeof(string));
            dt.Columns.Add("operate", typeof(string));
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                dt.Rows[j]["type"] = ControlName(dt.Rows[j]["defineType"].ToString());
                int id = int.Parse(dt.Rows[j]["id"].ToString());
                if (dt.Rows[j]["IsNull"].ToString() == "1")
                {
                    dt.Rows[j]["IsNullC"] = "是";
                }
                else
                {
                    dt.Rows[j]["IsNullC"] = "否";
                }
                dt.Rows[j]["operate"] = "<a class=\"list_link\"  href=\"DefineTable_Edit_List.aspx?id=" + id + "\" title=\"点击查看详情或修改\"><img src=\"../../sysImages/folder/re.gif\" border=\"0\" alt=\"修改此项\" /></a><a class=\"list_link\" href=\"DefineTable_List.aspx?action=del&id=" + id + "\" title=\"点击删除\" onclick=\"{if(confirm('确认删除吗？')){return true;}return false;}\"><img src=\"../../sysImages/folder/del.gif\" border=\"0\" alt=\"删除此项\" /></a><input type='checkbox' name='define_checkbox' id='define_checkbox'value=\"" + id + "\"/>";
            }
        }
        else
        {
            this.noContent.InnerHtml = NoShow();
            this.PageNavig.Visible = false;
        }
        this.DataList1.DataSource = dt;
        this.DataList1.DataBind();
    }

    protected String ControlName(string value)
    {
        string Value=null;
        switch (value)
        {
            case "1": Value = "单行文本框(text)"; break;
            case "2": Value = "下拉列表(select)"; break;
            case "3": Value = "单选按钮(radio)"; break;
            case "4": Value = "复选按钮(checkbox)"; break;
            case "6": Value = "选择图片(img)"; break;
            case "7": Value = "选择文件(files)"; break;
            case "8": Value = "多行文本框(ntext)"; break;
            case "9": Value = "密码框(password)"; break;
            default: Value = "无效"; break;
        }
        return Value;
    }

    #region 提示没内容
    string NoShow()
    {

        string nos = "<table border=0 width='98%' align=center cellpadding=5 cellspacing=1 class='table'>";
        nos = nos + "<tr class='TR_BG_list'>";
        nos = nos + "<td class='navi_link'>当前没有记录！</td>";
        nos = nos + "</tr>";
        nos = nos + "</table>";
        return nos;
    }
    #endregion

    #region DelAll
    protected void delall_Click(object sender, EventArgs e)
    {
        #region 删除相应ID下的字段
        string pr = Request.QueryString["pr"];
        #endregion
        if (pr == null || pr == "" || pr == string.Empty)
        {
            PageError("参数错误", "");
        }
        else
        {
            if (def.Str_Del_Data(pr) != 0)
            {
                PageRight("恭喜,删除成功", "DefineTable_Manage.aspx");
            }
            else
            {
                PageError("抱歉,删除失败", "DefineTable_Manage.aspx");
            }
        }
    }
    #endregion

    #region DelP
    protected void DelP_Click(object sender, EventArgs e)
    {
        string define_checkbox = Request.Form["define_checkbox"];
        if (define_checkbox == null || define_checkbox == String.Empty)
        {
            PageError("请先选择批量操作的内容!", "");
        }
        else
        {
            String[] CheckboxArray = define_checkbox.Split(',');
            define_checkbox = null;
            for (int i = 0; i < CheckboxArray.Length; i++)
            {
                def.Delete(CheckboxArray[i]);
            }
            PageRight("删除数据成功,请返回继续操作!", "DefineTable_Manage.aspx");
        }
        PageError("删除数据失败,请与管理联系!", "");
    }
    #endregion

    #region delOne
    protected void DeloNe()
    {
        int DefID = int.Parse(Request.QueryString["ID"]);
        string pr = Request.QueryString["pr"];
        if (DefID<=0)
        {
            PageError("参数错误", "shortcut_list.aspx");
        }
        else
        {
            if (def.Delete1(DefID) != 0)
            {
                PageRight("恭喜,删除成功", "DefineTable_List.aspx?pr="+ pr +"");
            }
            else
            {
                PageError("抱歉,删除失败", "DefineTable_List.aspx?pr=" + pr + "");
            }
        }
    }
    #endregion
}
