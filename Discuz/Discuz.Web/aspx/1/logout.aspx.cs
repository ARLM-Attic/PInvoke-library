using System;
using System.Data;
using System.Text;
using Discuz.Common;
using Discuz.Forum;
using Discuz.Web.UI;
using Discuz.Config;

namespace Discuz.Web
{
    /// <summary>
    /// 退出页面
    /// </summary>
    public class logout : PageBase
    {
        protected override void ShowPage()
        {
            pagetitle = "用户退出";
            username = "游客";
            int uid = userid;
            userid = -1;
            StringBuilder script = new StringBuilder();
            script.Append("if (top.document.getElementById('leftmenu')){");
            script.Append("		top.frames['leftmenu'].location.reload();");
            script.Append("}");

            base.AddScript(script.ToString());

            string referer = DNTRequest.GetQueryString("reurl");
            if (!DNTRequest.IsPost() || referer != "")
            {
                string r = "";
                if (referer != "")
                {
                    r = referer;
                }
                else
                {
                    if ((DNTRequest.GetUrlReferrer() == "") || (DNTRequest.GetUrlReferrer().IndexOf("login") > -1) ||
                        DNTRequest.GetUrlReferrer().IndexOf("logout") > -1)
                    {
                        r = "index.aspx";
                    }
                    else
                    {
                        r = DNTRequest.GetUrlReferrer();
                    }
                }
                Utils.WriteCookie("reurl", (referer == "" || referer.IndexOf("login.aspx") > -1) ? r : referer);
            }


            SetUrl(Utils.UrlDecode(ForumUtils.GetReUrl()));
            
            SetMetaRefresh();
            SetShowBackLink(false);
            if (DNTRequest.GetString("userkey") == userkey || IsApplicationLogout())
            {
                AddMsgLine("已经清除了您的登录信息, 稍后您将以游客身份返回首页");
                //Users.UpdateOnlineTime(uid);
                OnlineUsers.DeleteRows(olid);
                ForumUtils.ClearUserCookie();
                Utils.WriteCookie(Utils.GetTemplateCookieName(), "", -999999);

                System.Web.HttpCookie cookie = new System.Web.HttpCookie("dntadmin");
                System.Web.HttpContext.Current.Response.AppendCookie(cookie);

                //System.Web.Security.FormsAuthentication.SignOut();

            }
            else
            {
                AddMsgLine("无法确定您的身份, 稍后返回首页");
            }
        }

        /// <summary>
        /// 是否是来自应用程序的登出
        /// </summary>
        /// <returns></returns>
        private bool IsApplicationLogout()
        {
            APIConfigInfo apiconfig = APIConfigs.GetConfig();
            if(!apiconfig.Enable)
                return false;

            int confirm = DNTRequest.GetFormInt("confirm", -1);
            if (confirm != 1)
                return false;        

            return true;
        }
    }
}