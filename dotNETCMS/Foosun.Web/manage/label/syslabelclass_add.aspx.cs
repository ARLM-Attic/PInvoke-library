///************************************************************************************************************
///**********添加标签栏目Code By DengXi************************************************************************
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

public partial class manage_label_syslabelclass_add : Foosun.Web.UI.ManagePage
{
    public manage_label_syslabelclass_add()
    {
        Authority_Code = "T016";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";                        //设置页面无缓存
        if (!IsPostBack)
        {
            
        }
    }

    /// <summary>
    /// 保存分类
    /// </summary>
    /// <returns>保存分类</returns>
    /// 编写时间2007-04-23   Code By DengXi


    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string str_Cname = LabelClassName.Text;
            string str_Content = ClassContent.Text;

            Foosun.Model.LabelClassInfo lbcc = new Foosun.Model.LabelClassInfo();
            lbcc.ClassName = str_Cname;
            lbcc.Content = str_Content;
            lbcc.CreatTime = DateTime.Now;
            lbcc.SiteID = SiteID;
            lbcc.isRecyle = 0;

            int result = 0;

            Foosun.CMS.Label lbc =  new Foosun.CMS.Label();
            result = lbc.LabelClassAdd(lbcc);

            if (result == 1)
                PageRight("添加分类成功!", "SysLabel_List.aspx");
            else
                PageError("添加分类失败!", "");
        }
    }
}
