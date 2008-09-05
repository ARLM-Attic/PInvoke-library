///************************************************************************************************************
///**********修改样式分类Code By DengXi************************************************************************
///************************************************************************************************************
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

public partial class manage_label_styleclass_edit : Foosun.Web.UI.ManagePage
{
    public manage_label_styleclass_edit()
    {
        Authority_Code = "T019";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";                        //设置页面无缓存
        if (!IsPostBack)
        {
            
            copyright.InnerHtml = CopyRight;        //获取版权信息
            getClassInfo();
        }
    }


    /// <summary>
    /// 获得分类信息
    /// </summary>
    /// <returns>在前台显示分类信息</returns>
    /// 编写时间2007-04-20   Code By DengXi

    protected void getClassInfo()
    {
        string str_ClassID = Foosun.Common.Input.checkID(Request.QueryString["ClassID"]);
        ClassID.Value = str_ClassID;
        string str_ClassName = "";

        Foosun.CMS.Style.Style stClass = new Foosun.CMS.Style.Style();
        DataTable dt = stClass.getstyleClassInfo(str_ClassID);
        
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
                str_ClassName = dt.Rows[0][0].ToString();
            else
                PageError("参数错误!", "");
            dt.Clear();
            dt.Dispose();
        }
        styleClassName.Text = str_ClassName;
    }

    /// <summary>
    /// 保存分类
    /// </summary>
    /// <returns>保存分类</returns>
    /// 编写时间2007-04-20   Code By DengXi
   
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            int result = 0;
            string str_Name = Request.Form["styleClassName"];
            string str_ClassID = Foosun.Common.Input.checkID(Request.Form["ClassID"]);

            Foosun.Model.StyleClassInfo stClass=new Foosun.Model.StyleClassInfo();
            stClass.Sname = str_Name;
            stClass.ClassID = str_ClassID;
            stClass.CreatTime = DateTime.Now;

            Foosun.CMS.Style.Style stcClass = new Foosun.CMS.Style.Style();
            result = stcClass.styleClassEdit(stClass);
            
            if (result == 1)
                PageRight("修改分类成功!", "style.aspx");
            else
                PageError("修改分类失败!", "");
        }
    }
}
