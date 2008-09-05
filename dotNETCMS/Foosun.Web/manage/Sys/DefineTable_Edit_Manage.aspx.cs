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

public partial class manage_Sys_DefineTable_Edit_Manage : Foosun.Web.UI.ManagePage
{
    public manage_Sys_DefineTable_Edit_Manage()
    {
        Authority_Code = "Q033";
    }

    DefineTable def=new DefineTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            Start();
        }
    }

    /// <summary>
    /// 初始
    /// </summary>

    protected void Start()
    {
        string DefID = Request.QueryString["DefID"];
        if (DefID == null || DefID == "" || DefID == string.Empty)
        {
            PageError("参数错误", "shortcut_list.aspx");
        }
        else
        {
            DataTable dt = new DataTable();
            dt = def.Str_DefID(DefID);
            if (dt != null && dt.Rows.Count > 0)
            {
                #region 取值
                this.PraText.Text = dt.Rows[0]["ParentInfoId"].ToString();
                this.NewText.Text = dt.Rows[0]["DefineName"].ToString();
                #endregion
            }
            else
            {
                PageError("未知错误", "shortcut_list.aspx");
            }
        }
    }

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    #region save
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)//判断页面是否通过验证
        {
            string DefID = Request.QueryString["DefID"];
            if (DefID == null || DefID == "" || DefID == string.Empty)
            {
                PageError("参数错误", "DefineTable_Edit_Manage.aspx");
            }
            else
            {
                string Str_NewText = Foosun.Common.Input.Filter(this.NewText.Text.Trim());//名称               
                #region 刷新页面
                if (def.Update1(Str_NewText, DefID) != 0)
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
