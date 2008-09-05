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

namespace Foosun.Web.Install
{
    public partial class step4 :Foosun.Web.UI.BasePage
    {
        public string gError = string.Empty;
        Foosun.CMS.Install rd = new Foosun.CMS.Install();
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Button1.Attributes.Add("onclick", "return showLoading();");
            gError = Request.QueryString["error"];
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string adminUserName = this.UserName.Text;
            string Password = this.Password.Text;
            string cPassword = this.confimPassword.Text;
            if (adminUserName.Length < 1)
            {
                Response.Redirect("step4.aspx?error=\"请输入管理员用户名\"");
            }
            if (Password.Length < 3)
            {
                Response.Redirect("step4.aspx?error=\"密码不能小于3位\"");
            }
            if (Password != cPassword)
            {
                Response.Redirect("step4.aspx?error=\"2次密码不一致！\"");
            }
            if (rd.InserAdmin(adminUserName, Password) > 0)
            {
                Response.Redirect("step_End.aspx?error=false");
            }
            else
            {
                Response.Redirect("step4.aspx?error=\"安装失败，可能已经有管理员存在！\"");
            }
        }
    }
}
