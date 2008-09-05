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

public partial class manage_Sys_General_Add_Manage : Foosun.Web.UI.ManagePage
{
    public manage_Sys_General_Add_Manage()
    {
        Authority_Code = "Q019";
    }
    sys rd = new sys();
    rootPublic pd = new rootPublic();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Response.CacheControl = "no-cache";//设置页面无缓存

            //LoginInfo.CheckPop("权限代码", "0", "1", "9");//权限代码
            copyright.InnerHtml = CopyRight;
        }
    }

    /// <summary>
    /// 修改页面保存事件
    /// </summary>
    ///Code by ChenZhaoHui

    protected void But_AddNew_ServerClick(object sender, EventArgs e)
    {
        if (Page.IsValid == true)//判断页面是否通过验证
        {
            #region 取得常规添加中的表单信息
            string Str_Sel_Type = Foosun.Common.Input.Filter(this.Sel_Type.Text.Trim());//类型
            string Str_Txt_Name = Foosun.Common.Input.Filter(this.Txt_Name.Text.Trim());//标题
            string Str_Txt_LinkUrl = Foosun.Common.Input.Filter(this.Txt_LinkUrl.Text.Trim());//链接地址
            string Str_Txt_Email = Foosun.Common.Input.Filter(this.Txt_Email.Text.Trim());//电子邮件
            #endregion

            #region 判断数据库中是否存在已经添加了的标题
            DataTable dt = rd.GetGeneralRecord(Str_Txt_Name);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {                    
                    if (dt.Rows[0]["gType"] != null)//判断类型是否相同
                    {
                        for (int i = 0; i < dt.Rows.Count;i++ )
                        {
                            if (Convert.ToInt32(dt.Rows[i]["gType"].ToString()) == Convert.ToInt32(Str_Sel_Type))
                                PageError("对不起，该标题已经存在", "General_Add_Manage.aspx");
                        }
                        
                    }
                    
                }
                dt.Clear(); dt.Dispose();
            }
            #endregion

            rd.insertGeneral(Str_Sel_Type, Str_Txt_Name, Str_Txt_LinkUrl, Str_Txt_Email);
            pd.SaveUserAdminLogs(1, 1, UserNum, "添加常规项成功", "常规选项添加成功.");
            PageRight("添加成功", "General_manage.aspx");

        }
    }
}
