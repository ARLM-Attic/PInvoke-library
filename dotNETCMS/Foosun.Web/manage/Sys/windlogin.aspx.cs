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
public partial class manage_Sys_windlogin : Foosun.Web.UI.ManagePage
{
    public manage_Sys_windlogin()
    {
        Authority_Code = "Q008";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            if (SiteID != "0") { PageError("您没有配置文件的权限", ""); }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string tb_ = Foosun.CMS.FSSecurity.FDESEncrypt(TextBox1.Text.ToString().Trim(), 1);
        Session["tb_"] = tb_;
        Response.Redirect("const.aspx");
        
    }
}
