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

public partial class manage_Sys_DefineTable_Edit_List : Foosun.Web.UI.ManagePage
{
    public manage_Sys_DefineTable_Edit_List()
    {
        Authority_Code = "Q033";
    }
    DefineTable def = new DefineTable();
    public DataTable dt_class;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            Start();
        }
    }

    /// <summary>
    /// 初始修改页面信息
    /// </summary>

    #region start
    protected void Start()
    {
        int ID = int.Parse(Request.QueryString["ID"]);
        if (ID <=0)
        {
            PageError("参数错误", "shortcut_list.aspx");
        }
        else
        {
            DataTable dt = new DataTable();
            dt = def.Str_Start_Sql(ID);
            if (dt != null && dt.Rows.Count > 0)
            {
                #region 取值
                getClassInfo_Edit(dt.Rows[0]["defineInfoId"].ToString());//取类别信息
                this.DefName.Text = dt.Rows[0]["defineCname"].ToString();
                this.DefEname.Text = dt.Rows[0]["defineColumns"].ToString();
                this.DefType.Text = dt.Rows[0]["defineType"].ToString();
                this.definedvalue.Text = dt.Rows[0]["definedvalue"].ToString();
                if (dt.Rows[0]["IsNull"].ToString() == "1")
                {
                    this.DefIsNull.Checked = true;
                }
                else
                {
                    this.DefIsNull.Checked = false;
                }
                this.DefColumns.Text = dt.Rows[0]["defineValue"].ToString();
                this.DefExpr.Text = dt.Rows[0]["defineExpr"].ToString();
                #endregion
            }
            else
            {
                PageError("未知错误", "shortcut_list.aspx");
            }
        }
    }
    #endregion

    /// <summary>
    /// 递归
    /// </summary>
    /// <param name="classid"></param>

    #region
    protected void getClassInfo_Edit(string classid)
    {
        dt_class = def.Sel_DefineInfoId();
        if (dt_class != null)
        {
            ClassRender_Edit("0", 0, classid);
        }
        dt_class.Clear();
        dt_class.Dispose();
    }
    private void ClassRender_Edit(string PID, int Layer, string classid)
    {
        DataTable dts = def.Sel_ParentInfoId(PID);
        if (dts.Rows.Count < 1)
            return;
        else
        {
            foreach (DataRow r in dts.Rows)
            {
                ListItem it = new ListItem();
                it.Value = r["DefineInfoId"].ToString();
                if (classid == it.Value)
                {
                    it.Selected = true;
                }
                string stxt = "┝";
                for (int i = 0; i < Layer; i++)
                {
                    stxt += "┉";
                }
                it.Text = stxt + r["DefineName"].ToString();
                this.ColumnsType.Items.Add(it);
                ClassRender_Edit(r["DefineInfoId"].ToString(), Layer + 1, classid);
            }
        }
    }
    #endregion

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param> 


    #region save edit
    protected void btnData_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)//判断页面是否通过验证
        {
            int DefID = int.Parse(Request.QueryString["ID"]);
            if (DefID <= 0)
            {
                PageError("参数错误", "DefineTable_Edit_List.aspx");
            }
            else
            {
                string Str_ColumnsType = Request.Form["ColumnsType"];
                string Str_DefName = Foosun.Common.Input.Filter(this.DefName.Text.Trim());//名称
                string Str_DefEname = Foosun.Common.Input.Filter(this.DefEname.Text.Trim());
                string definedvalue = this.definedvalue.Text;
                string Str_DefType = this.DefType.SelectedValue;
                int Str_DefIsNull = 0;
                if (DefIsNull.Checked)
                {
                    Str_DefIsNull = 1;
                }
                else
                {
                    Str_DefIsNull = 0;
                }
                string Str_DefColumns = Foosun.Common.Input.Filter(this.DefColumns.Text.Trim());
                string Str_DefExpr = Foosun.Common.Input.Filter(this.DefExpr.Text.Trim());
                #region 刷新页面
                if (def.Update(Str_ColumnsType, Str_DefName, Str_DefEname, Str_DefType, Str_DefIsNull, Str_DefColumns, Str_DefExpr, DefID, definedvalue) != 0)
                {
                    PageRight("修改成功", "DefineTable_Manage.aspx");
                }
                else
                {
                    PageError("意外错误：未知错误", "shortcut_list.aspx");
                }
            }
            #endregion
        }
    }
   #endregion
}
