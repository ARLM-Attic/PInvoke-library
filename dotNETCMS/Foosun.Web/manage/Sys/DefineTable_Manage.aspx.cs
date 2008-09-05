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

public partial class manage_Sys_DefineTable_Manage : Foosun.Web.UI.ManagePage
{
    public manage_Sys_DefineTable_Manage()
    {
        Authority_Code = "Q032";
    }
    DefineTable def = new DefineTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        this.PageNavigator1.OnPageChange+=new PageChangeHandler(PageNavigator1_OnPageChange);
        if (!IsPostBack)
        {
            
        }
        #region 删除
        string action = Request.QueryString["action"];
        if (action == "delone_class")
        {
            DelOne_Class();
        }
        #endregion
        DataBindSource(1);
         GetParentID();//取父类ID
    }

    protected void PageNavigator1_OnPageChange(object sender, int IndexPage)
    {
        DataBindSource(IndexPage);
    }

    protected void DataBindSource(int PageIndex)
    {
        int i,j;
        DataTable dt = Foosun.CMS.Pagination.GetPage(this.GetType().Name, PageIndex, PAGESIZE, out i, out j, null);
        this.PageNavigator1.RecordCount = j;
        this.PageNavigator1.PageCount = i;
        this.PageNavigator1.PageIndex = PageIndex;
        if (dt != null)
        {
            dt.Columns.Add("newsAdd",typeof(string));
            dt.Columns.Add("Display",typeof(string));
            dt.Columns.Add("operate", typeof(string));//操作
            dt.Columns.Add("Colum", typeof(String));
            for (int p = 0; p < dt.Rows.Count; p++)
            {
                String strchar = null;
                string DefID = dt.Rows[p]["DefineInfoId"].ToString();
                dt.Rows[p]["Display"] = "<a href=\"DefineTable_List.aspx?pr=" + DefID + "\" class=\"topnavichar\">查看该类字段</a>";
                dt.Rows[p]["newsAdd"] = "<a href=\"DefineTable.aspx?pr=" + DefID + "\" class=\"topnavichar\">新增该类字段</a>";
                dt.Rows[p]["operate"] = "<a href=\"DefineTable_Edit_Manage.aspx?DefID=" + DefID + "\"  class=\"list_link\" title=\"修改此项\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/edit.gif\" border=\"0\" alt=\"修改此项\" /></a><a href=\"DefineTable_Manage.aspx?action=delone_class&DefID=" + DefID + "\"  class=\"list_link\" title=\"删除此项\" onclick=\"{if(confirm('确认删除吗？其下的子类和字段也将被删除!')){return true;}return false;}\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/del.gif\" border=\"0\" alt=\"删除此项\" /></a><a href=\"DefineTable_Manage.aspx?action=add_clildclass&parentid=" + DefID + "\" class=\"list_link\" title=\"添加子类\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/addclass.gif\" border=\"0\" alt=\"添加子类\" /></a><input type='checkbox' name='define_checkbox' id='define_checkbox' value=\"" + DefID + "\"/>";
                #region 列表
                strchar += "<tr class=\"TR_BG_list\">";
                strchar += "<td width=\"30%\" align=\"left\" valign=\"middle\">" + dt.Rows[p]["DefineName"] + "</td>";
                strchar += "<td align=\"center\" valign=\"middle\" >" + dt.Rows[p]["Display"] + "</td>";
                strchar += "<td align=\"center\" valign=\"middle\" >" + dt.Rows[p]["newsAdd"] + "</td>";
                strchar += "<td align=\"center\" valign=\"middle\" >" + dt.Rows[p]["operate"] + "</td>";
                strchar += "</tr>";
                strchar += GetChildList(DefID, "┝");
                dt.Rows[p]["Colum"] = strchar;
                #endregion
            }
        }
        DataList1.DataSource = dt;
        DataList1.DataBind();
    }
    #region 递归

    string GetChildList(string Classid, string sign)
    {
        String strchar = null;
        DataTable dv = def.sel_Str(Classid);
        sign += "┉";
        dv.Columns.Add("newsAdd", typeof(string));
        dv.Columns.Add("Display", typeof(string));
        dv.Columns.Add("operate", typeof(string));//操作
        for (int pi = 0; pi < dv.Rows.Count; pi++)
        {
            string DefID = dv.Rows[pi]["DefineInfoId"].ToString();
            dv.Rows[pi]["Display"] = "<a href=\"DefineTable_List.aspx?pr=" + DefID + "\" class=\"topnavichar\">查看该类字段</a>";
            dv.Rows[pi]["newsAdd"] = "<a href=\"DefineTable.aspx?pr=" + DefID + "\" class=\"topnavichar\">新增该类字段</a>";
            dv.Rows[pi]["operate"] = "<a href=\"DefineTable_Edit_Manage.aspx?DefID=" + DefID + "\"  class=\"list_link\" title=\"修改此项\"><img src=\"../../sysImages/folder/re.gif\" border=\"0\" alt=\"修改此项\" /></a><a href=\"DefineTable_Manage.aspx?action=delone_class&DefID=" + DefID + "\"  class=\"list_link\" title=\"删除此项\" onclick=\"{if(confirm('确认删除吗？其下的子类和字段也将被删除!')){return true;}return false;}\"><img src=\"../../sysImages/folder/del.gif\" border=\"0\" alt=\"删除此项\" /></a><a href=\"DefineTable_Manage.aspx?action=add_clildclass&parentid=" + DefID + "\" class=\"list_link\" title=\"添加子类\"><img src=\"../../sysImages/folder/new.gif\" border=\"0\" alt=\"添加子类\" /></a><input type='checkbox' name='define_checkbox' id='define_checkbox' value=\"" + DefID + "\"/>";
            #region 列表
            strchar += "<tr class=\"TR_BG_list\">";
            strchar += "<td width=\"30%\" align=\"left\" valign=\"middle\">" + sign + dv.Rows[pi]["DefineName"] + "</td>";
            strchar += "<td align=\"center\" valign=\"middle\" >" + dv.Rows[pi]["Display"] + "</td>";
            strchar += "<td align=\"center\" valign=\"middle\" >" + dv.Rows[pi]["newsAdd"] + "</td>";
            strchar += "<td align=\"center\" valign=\"middle\" >" + dv.Rows[pi]["operate"] + "</td>";
            strchar += "</tr>";
            #endregion
            strchar += GetChildList(DefID, sign);
        }
        return strchar;

    }
    #endregion
    /// <summary>
    /// 新增分类
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        string _PraText = PraText.Text.ToString().Trim();
        if(_PraText==null||_PraText==String.Empty||_PraText=="无")
            _PraText="0";

        string _NewText = NewText.Text.ToString().Trim();
        if (_NewText == null || _NewText == String.Empty)
            PageError("新增分类不能为空!","");

        if (def.sel_1(_NewText) != 0)
            PageError("此分类已存数据表中!","");

        //检测是否有重复数据
       
      randP:  string rand = Foosun.Common.Rand.Number(12);
        if (def.sel_2(rand) != 0)
            goto randP;

        if (def.Add2(rand, _NewText, _PraText) != 0)
            PageRight("添加字段分类成功!", "DefineTable_Manage.aspx");
        else
            PageError("添加字段分类失败!","");
    }

    #region 取父类ID
    protected void GetParentID()
    {
        string parentid = Request.QueryString["parentid"];//父类编号
        if (parentid == "" || parentid == null || parentid == string.Empty)
        {
            this.PraText.Text = "0";
        }
        else
        {
            this.PraText.Text = Foosun.Common.Input.Filter(parentid);
        }
    }
    #endregion

    #region --------delone-------
    protected void DelOne_Class()
    {
        string DefID = Request.QueryString["DefID"];
        if (DefID == null || DefID == "" || DefID == string.Empty)
        {
            PageError("参数错误", "shortcut_list.aspx");
        }
        else
        {
            def.Delete3(DefID);
            def.Delete4(DefID);
            def.Delete5(DefID);
            PageRight("恭喜,删除成功", "DefineTable_Manage.aspx");
        }
    }
    #endregion

    #region DelAll
    protected void delall_Click(object sender, EventArgs e)
    {
        if (def.Delete6()!=0&&def.Delete7()!=0)
        {
            PageRight("恭喜,删除成功", "DefineTable_Manage.aspx");
        }
        else
        {
            PageError("抱歉,删除失败", "DefineTable_Manage.aspx");
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
                def.Delete8(CheckboxArray[i]);
                def.Delete9(CheckboxArray[i]);
            }
            PageRight("删除数据成功,请返回继续操作!", "DefineTable_Manage.aspx");
        }
        PageError("删除数据失败,请与管理联系!" ,"");
    }
    #endregion
}
