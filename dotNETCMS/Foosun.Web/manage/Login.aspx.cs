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
using Foosun.Model;

public partial class Manage_Login : Foosun.Web.UI.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        userExit();
        if (!IsPostBack)
        {
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Logout();
            string TmUrl = Request.QueryString["urls"];
            if (TmUrl != null && TmUrl != "")
            {
                string tmDir = Foosun.Config.UIConfig.dirMana.Trim() + "/index.aspx";
                string tmDir1 = Foosun.Config.UIConfig.dirMana.Trim() + "/menu.aspx";
                if ((TmUrl.ToString().ToLower()).IndexOf(tmDir.ToLower()) == -1 && (TmUrl.ToString().ToLower()).IndexOf(tmDir1.ToLower()) == -1) { this.HidUrl.Value = TmUrl.ToString(); }
            }
            string[] _protPass = Foosun.Config.UIConfig.protPass.Split(',');

            //#comment#
            //if (_protPass[0] == "1") 
            //{ safeCodeVerify_1.Visible = true; }
            //else 
            //{ safeCodeVerify_1.Visible = false; }
        }
    }


    protected void login_Click(object sender, ImageClickEventArgs e)
    {
        if (Page.IsValid)
        {
            string Account = this.TxtName.Text;
            string PassWord = this.TxtPassword.Text;
            string VerifyCode = this.TxtVerify.Text.Trim();
            //#comment# string SafeCode = this.TxtSafeCode.Text;
            string Urls = this.HidUrl.Value;
            if (Account.Trim() == string.Empty || PassWord.Trim() == string.Empty)
            {
                PageError("用户与密码不能为空,详细请查看帮助说明!", "login.aspx?" + Request.QueryString);
            }
            if (Session["CheckCode"] == null)
            {
                PageError("验证码过期，请返回重新登录!", "login.aspx?" + Urls + "");
            }
            string CheckCode = Session["CheckCode"].ToString().ToUpper();
            Session.Remove("CheckCode");

            if (VerifyCode.ToUpper() != CheckCode)
            {
                PageError("输入验证码错误!", "login.aspx?" + Urls + "");
            }

            //#comment# Warren 5/9/2008
            #region 安全码验证
            //string[] _protPass = Foosun.Config.UIConfig.protPass.Split(',');
            //if (_protPass[0] == "1")
            //{
            //    if (_protPass[1] == "1")
            //    {
            //        if (_protPass[2] == "0")
            //        {
            //            if ((VerifyCode + Foosun.Config.UIConfig.protRand) != SafeCode)
            //            {
            //                PageError("输入的安全码错误!", "login.aspx?" + Urls + "");
            //            }
            //        }
            //        else
            //        {
            //            if ((Foosun.Config.UIConfig.protRand + SafeCode) != SafeCode)
            //            {
            //                PageError("输入的安全码错误!", "login.aspx?" + Urls + "");
            //            }
            //        }
            //    }
            //    else
            //    {
            //        if (Foosun.Config.UIConfig.protRand != SafeCode)
            //        {
            //            PageError("输入的安全码错误!", "login.aspx?" + Urls + "");
            //        }
            //    }
            //}
            #endregion 安全码验证
            //end comment

            GlobalUserInfo info;
            EnumLoginState state = Login(Account, PassWord, out info, true);
            if (state == EnumLoginState.Succeed)
            {
                Foosun.Global.Current.Set(info);
                Session["islogin"]="1";
                if (Urls != null && Urls.Trim() != string.Empty)
                {
                    string MangeStr = Foosun.Config.UIConfig.dirMana + "/index.aspx".ToLower();
                    if (Urls.IndexOf(MangeStr) > 0)
                    {
                        Response.Write("<script language=\"javascript\">window.top.location.href=\"Index.aspx\";</script>");
                        Response.End();
                    }
                    else
                    {
                        Response.Write("<script language=\"javascript\">window.top.location.href=\"Index.aspx?urls=" + Urls + "\";</script>");
                        Response.End();
                    }
                }
                else
                {
                    Response.Write("<script language=\"javascript\">window.top.location.href=\"Index.aspx\";</script>");
                    Response.End();
                }
            }
            else
            {
                LoginResultShow(state);
            }
        }
    }

    /// <summary>
    /// 用户退出清除cookie
    /// </summary>
    private void userExit()
    {
        //得到用户数据
        HttpCookie cook = HttpContext.Current.Request.Cookies["SITEINFO"];
        if (cook != null)
        {
            //设置用户的cookie为过期
            cook.Expires = DateTime.Now.AddDays(-1);
            cook.Value = null;
            Response.Cookies.Add(cook);
        }
    }
}
