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

public partial class manage_label_SaveStyle : Foosun.Web.UI.ManagePage
{
    public manage_label_SaveStyle()
    {
        Authority_Code = "T018";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        int result = 0;
        Foosun.Model.StyleInfo stClass = new Foosun.Model.StyleInfo();
        string StyleName = Request.QueryString["StyleName"];
        string ClassID = Request.QueryString["ClassID"];
        string Content = Request.QueryString["Content"]+"";
        Foosun.CMS.Style.Style style_Class = new Foosun.CMS.Style.Style();
        if (style_Class.styleNametf(StyleName.ToString()) > 0)
        {
            Response.Write("名称已经存在!");
            Response.End();
        }
        stClass.StyleName = StyleName.ToString();
        stClass.ClassID = ClassID.ToString();
        stClass.Content = Foosun.Common.Input.HtmlDecode(Content);
        stClass.Description = "";
        stClass.CreatTime = DateTime.Now;
        stClass.isRecyle = 0;
        result = style_Class.styleAdd(stClass);
        if (result == 1)
        {
            Response.Write("保存样式成功!");
            Response.End();
        }
        else
        {
            Response.Write("保存样式失败!");
            Response.End();
        }
    }
}
