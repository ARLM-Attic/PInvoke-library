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

public partial class manage_Sys_General_Edit_Manage : Foosun.Web.UI.ManagePage
{
    public manage_Sys_General_Edit_Manage()
    {
        Authority_Code = "Q019";
    }
    sys rd = new sys();
    rootPublic pd = new rootPublic();
    protected void Page_Load(object sender, EventArgs e)
    {
        string action = Foosun.Common.Input.Filter(Request.QueryString["Action"]);
        if (!IsPostBack)
        {
            Response.CacheControl = "no-cache";//设置页面无缓存

            copyright.InnerHtml = CopyRight;

            #region 判断是否是编辑修改，则初始化页面数据
            if (action == "edit")
            {
                General_M_Edit();
            }
            #endregion
        }

    }

    /// <summary>
    /// 修改事件函数初始化修改页面
    /// </summary>
    ///Code by ChenZhaoHui

    protected void General_M_Edit()
    {
        int GID = int.Parse(Request.QueryString["ID"]);
        DataTable dt = rd.getGeneralIdInfo(GID);
        if (dt.Rows.Count > 0)
        {
            this.Sel_Type.Text = dt.Rows[0]["gType"].ToString();
            this.Txt_Name.Text = dt.Rows[0]["Cname"].ToString();
            this.Txt_LinkUrl.Text = dt.Rows[0]["URL"].ToString();
            this.Txt_Email.Text = dt.Rows[0]["EmailURL"].ToString();
            dt.Clear(); dt.Dispose();
        }
        else
        {
            PageError("未知错误,异常错误", "General_manage.aspx");
        }
    }

    /// <summary>
    /// 修改页面保存事件
    /// </summary>
    ///Code by ChenZhaoHui

    protected void But_AddNewEdit_ServerClick(object sender, EventArgs e)
    {
        if (Page.IsValid)//判断页面是否通过验证
        {
            int GID = int.Parse(Request.QueryString["ID"]);

            #region 取得常规添加中的表单信息
            string Str_Sel_Type = Foosun.Common.Input.Filter(this.Sel_Type.Text.Trim());//类型
            string Str_Txt_Name = Foosun.Common.Input.Filter(this.Txt_Name.Text.Trim());//标题
            // string Str_Txt_isLock = Request.Form["islock"];//锁定
            string Str_Txt_LinkUrl = Foosun.Common.Input.Filter(this.Txt_LinkUrl.Text.Trim());//链接地址
            string Str_Txt_Email = Foosun.Common.Input.Filter(this.Txt_Email.Text.Trim());//电子邮件

            rd.UpdateGeneral(Str_Sel_Type, Str_Txt_Name, Str_Txt_LinkUrl, Str_Txt_Email, GID);
            pd.SaveUserAdminLogs(1, 1, UserNum, "修改常规项成功", "常规选项修改成功.");
            PageRight("添加成功", "General_manage.aspx");

            #endregion 结束
        }
    }
}
