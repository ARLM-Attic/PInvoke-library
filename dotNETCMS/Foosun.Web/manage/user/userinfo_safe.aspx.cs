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

public partial class manage_user_userinfo_safe : Foosun.Web.UI.ManagePage
{
    UserMisc rd = new UserMisc();
    rootPublic pd = new rootPublic();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Expires = 0;
        Response.CacheControl = "no-cache";
        if (!IsPostBack)
        {
            
            copyright.InnerHtml = CopyRight;
            string uids = Foosun.Common.Input.Filter(Request.QueryString["id"]);
            int uid = 0;
            try
            {
                uid = int.Parse(uids);
            }
            catch (Exception US)
            {
                PageError("错误的参数"+US.ToString()+"", "");
            }
            suid.Value = uid.ToString();
            DataTable dt = rd.getPassWord(uid);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    PassQuestion.Text = dt.Rows[0]["PassQuestion"].ToString();
                }
            }
        }
    }

    protected void submitSave(object sender, EventArgs e)
    {
        if (Page.IsValid == true)                       //判断是否验证成功
        {
            string PassQuestion = this.PassQuestion.Text;
            string oldpassword = this.oldpassword.Text;;
            string PassKey = this.PassKey.Text;
            string password = this.password.Text;
            int suid = int.Parse(Foosun.Common.Input.Filter(Request.Form["suid"]));
            if ((PassQuestion != null && PassQuestion != "") && (PassKey != null && PassKey != "") && (password != null && password != ""))
            {
                if (password.ToString() != oldpassword.ToString())
                {
                    PageError("二次密码不一致", "userlist.aspx");
                }
                else
                {
                    //同步更新用户信息
                    Foosun.PlugIn.Passport.DPO_Request request = new Foosun.PlugIn.Passport.DPO_Request(Context);
                    request.PassWord = password;
                    request.UserName = Foosun.Global.Current.UserName;
                    request.ProcessMultiPing("update");

                    if (request.FoundErr)
                    {
                        PageError("同步更新用户信息失败", "userinfo_safe.aspx");
                    }

                    rd.UpdateUserSafe(suid, PassQuestion, PassKey, password);
                    PageRight("安全资料成功！", "userlist.aspx");
                }
            }
            else
            {
                PageError("所有项目必须填写", "");
            }
        }
    }
}
