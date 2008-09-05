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

public partial class manage_Sys_DefineTable : Foosun.Web.UI.ManagePage
{
    public manage_Sys_DefineTable()
    {
        Authority_Code = "Q033";
    }
    public DataTable dt_class;
    DefineTable def = new DefineTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        string pra = Request.QueryString["pr"];
        if (!IsPostBack)
        {
                       //判断用户是否登录
        }

        #region droplist
        ColumnsType.Items.Clear();
        getClassInfo();
        #endregion


    }
    #region
    protected void getClassInfo()
    {
        string _pr = Request.QueryString["pr"];
        dt_class = def.Sel_DefineInfoId();
        if (dt_class != null)
         {
            ClassRender("0", 0);
            #region display
            for (int i=0;i<dt_class.Rows.Count;i++)
            {
                if (dt_class.Rows[i]["DefineInfoId"].ToString() == _pr)
                {
                    ColumnsType.Items[i].Selected = true;
                    break;
                }
             }
             dt_class.Clear();
             dt_class.Dispose();
        }
        #endregion
     }
    #endregion

    #region
    private void ClassRender(string PID, int Layer)
    {
        DataTable dts = def.Sel_ParentInfoId(PID);
        if(dts.Rows.Count<1)
            return;
        else
        {
            foreach (DataRow r in dts.Rows)
            {
                ListItem it = new ListItem();
                it.Value = r["DefineInfoId"].ToString();
                string stxt = "┝";
                for (int i = 0; i < Layer; i++)
                {
                    stxt += "┉";
                }
                it.Text = stxt + " "+r["DefineName"].ToString();
                this.ColumnsType.Items.Add(it);
                ClassRender(r["DefineInfoId"].ToString(), Layer + 1);
            }
        }
    }
    #endregion

    /// <summary>
    /// 提交数据处理
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    protected void btnData_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            #region  获取数据内容

            #region 取得所属分类的ID,在添加时使其为相应ID下的字段。
            string Str_ColumnsType = Request.Form["ColumnsType"];
            #endregion

            string defCname = DefName.Text.ToString().Trim();
            string defEname = DefEname.Text.ToString().Trim();
            string defColumn = DefType.Text.ToString().Trim();
            string definedvalue = this.definedvalue.Text;
            bool defIsNull = DefIsNull.Checked;
            string defColumns = DefColumns.Text.ToString().Trim();
            string defExp = DefExpr.Text.ToString().Trim();
            int definSelected = DefinSelected(int.Parse(this.DefType.SelectedItem.Value));
            int Isnull = 0;
            if (defIsNull)
                Isnull = 1;
            #endregion
            if (def.sel_defEname(defEname) != 0)
                PageError("英文名称已经存在！", "");

            #region 数据是否有重复
            if (def.sel_defCname(defCname) != 0)
                PageError("添加数据已存在！", "");
            #endregion
            else
            {
                if (def.Add(Str_ColumnsType, defCname, defEname, definSelected, Isnull, defColumns, defExp, definedvalue) != 0)
                    PageRight("添加数据成功!", "DefineTable_Manage.aspx");
                else
                    PageError("添加数据失败!", "");
            }
        }
    }

    /// <summary>
    /// 检测控件类型 类型为:下拉列表\单选按扭\复选按扭返回flg
    /// </summary>
    /// <param name="Num"></param>
    /// <returns></returns>
    protected int DefinSelected(int Num)
    {
        string StrErr = null;
        bool flg = false;
        #region 条件处理
        switch (Num)
        {
            case 2:
            {
                StrErr = "请设置该控件参数,详细请查看帮助!";
                if (this.DefColumns.Text.ToString() == null || this.DefColumns.Text.ToString() == string.Empty)
                    break;
                else
                    flg = true;
                break;
            }
            case 3:
            {
                StrErr = "请设置该控件参数,详细请查看帮助!";
                if (this.DefColumns.Text.ToString() == null || this.DefColumns.Text.ToString() == string.Empty)
                    break;
                else
                    flg = true;
                break;
            }
            case 4:
            {
                StrErr = "请设置该控件参数,详细请查看帮助!";
                if (this.DefColumns.Text.ToString() == null || this.DefColumns.Text.ToString() == string.Empty)
                    break;
                else
                    flg = true;
                break;
            }
            default: flg = true; break;
        }
        #endregion

        if (!flg)
            PageError(StrErr, "");
        return Num;
    }
}
