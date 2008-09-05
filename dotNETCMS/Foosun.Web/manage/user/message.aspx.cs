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

public partial class manage_user_message : Foosun.Web.UI.ManagePage
{
    public manage_user_message()
    {
        Authority_Code = "U033";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            copyright.InnerHtml = CopyRight;            //获取版权信息
        }
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            Foosun.CMS.Message rd = new Foosun.CMS.Message();
            int delNum = rd.clearmessage();
            if (this.CheckBox22.Checked)
            {
                rd.clearmessagerecyle();
            }
            PageRight("消息清除成功。共删除了"+delNum+"条信息!", "");
        }
    }
}
