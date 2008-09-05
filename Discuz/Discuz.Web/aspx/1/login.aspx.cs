using System;
using System.Data;
using System.Text;
using Discuz.Common;
using Discuz.Forum;
using Discuz.Config;
using Discuz.Entity;
using Discuz.Web.UI;

namespace Discuz.Web
{
    /// <summary>
    /// 登录
    /// </summary>
    public class login : PageBase
    {
        /// <summary>
        /// 登录所使用的用户名
        /// </summary>
        public string postusername;
        /// <summary>
        /// 登陆时的密码验证信息
        /// </summary>
        public string loginauth = DNTRequest.GetString("loginauth");
        /// <summary>
        /// 登陆时提交的密码
        /// </summary>
        public string postpassword = "";
        /// <summary>
        /// 登陆成功后跳转的链接
        /// </summary>
        public string referer = DNTRequest.GetString("referer");
        /// <summary>
        /// 是否跨页面提交
        /// </summary>
        public bool loginsubmit = DNTRequest.GetString("loginsubmit") == "true" ? true : false;

        protected override void ShowPage()
        {
            pagetitle = "用户登录";

            postusername = Utils.UrlDecode(DNTRequest.GetString("postusername")).Trim();

            if (this.userid != -1)
            {
                SetUrl(BaseConfigs.GetForumPath);
                SetMetaRefresh();
                SetShowBackLink(false);
                AddMsgLine("您已经登录，无须重复登录");
                ispost = true;
                SetLeftMenuRefresh();

                APIConfigInfo apiInfo = APIConfigs.GetConfig();
                if (apiInfo.Enable)
                {
                    APILogin(apiInfo);
                }
            }

            if (LoginLogs.UpdateLoginLog(DNTRequest.GetIP(), false) >= 5)
            {
                AddMsgLine("您已经多次输入密码错误, 请15分钟后再登录");
                loginsubmit = false;
                return;
            }

            //未提交或跨页提交时
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
                Utils.WriteCookie("reurl", (DNTRequest.GetQueryString("reurl") == "" || DNTRequest.GetQueryString("reurl").IndexOf("login.aspx") > -1) ? r : DNTRequest.GetQueryString("reurl"));
            }

            //如果提交...
            if (DNTRequest.IsPost())
            {
                StringBuilder builder = new StringBuilder();
                foreach (string key in System.Web.HttpContext.Current.Request.QueryString.AllKeys)
                {
                    if (key != "postusername")
                    {
                        builder.Append("&");
                        builder.Append(key);
                        builder.Append("=");
                        builder.Append(DNTRequest.GetQueryString(key));
                    }
                }
                base.SetBackLink("login.aspx?postusername=" + Utils.UrlEncode(DNTRequest.GetString("username")) + builder.ToString());


                //如果没输入验证码就要求用户填写
                if (isseccode && DNTRequest.GetString("vcode") == "")
                {
                    postusername = DNTRequest.GetString("username");
                    loginauth = DES.Encode(DNTRequest.GetString("password"), config.Passwordkey).Replace("+", "[");
                    loginsubmit = true;
                    return;
                }

                if (!Users.Exists(DNTRequest.GetString("username")))
                {
                    AddErrLine("用户不存在");
                }

                if (DNTRequest.GetString("password").Equals("") && DNTRequest.GetString("loginauth") == "")
                {
                    AddErrLine("密码不能为空");
                }

                if (IsErr())
                {
                    return;
                }

                if (!Utils.StrIsNullOrEmpty(loginauth))
                {
                    postpassword = DES.Decode(loginauth.Replace("[", "+"), config.Passwordkey);
                }
                else
                {
                    postpassword = DNTRequest.GetString("password");
                }

                if (postusername == "")
                {
                    postusername = DNTRequest.GetString("username");
                }

                int uid = -1;
                if (config.Passwordmode == 1)
                {
                    if (config.Secques == 1 && (!Utils.StrIsNullOrEmpty(loginauth) || !loginsubmit))
                    {
                        uid = Users.CheckDvBbsPasswordAndSecques(postusername,
                                                               postpassword,
                                                               DNTRequest.GetInt("question", 0),
                                                               DNTRequest.GetString("answer"));
                    }
                    else
                    {
                        uid = Users.CheckDvBbsPassword(postusername, postpassword);
                    }
                }
                else
                {
                    if (config.Secques == 1 && (!Utils.StrIsNullOrEmpty(loginauth) || !loginsubmit))
                    {
                        uid = Users.CheckPasswordAndSecques(postusername,
                                                          postpassword,
                                                          true,
                                                          DNTRequest.GetInt("question", 0),
                                                          DNTRequest.GetString("answer"));
                    }
                    else
                    {
                        uid = Users.CheckPassword(postusername, postpassword, true);
                    }
                }


                if (uid != -1)
                {
                    ShortUserInfo userinfo = Users.GetShortUserInfo(uid);
                    if (userinfo.Groupid == 8)
                    {
                        AddErrLine("抱歉, 您的用户身份尚未得到验证");
                        if (config.Regverify == 1)
                        {
                            AddMsgLine("请您到您的邮箱中点击激活链接来激活您的帐号");
                        }

                        if (config.Regverify == 2)
                        {
                            AddMsgLine("您需要等待一些时间, 待系统管理员审核您的帐户后才可登录使用");
                        }
                        loginsubmit = false;
                    }
                    else
                    {
                        if (!Utils.StrIsNullOrEmpty(userinfo.Secques) && loginsubmit && Utils.StrIsNullOrEmpty(DNTRequest.GetString("loginauth")))
                        {
                            loginauth = DES.Encode(DNTRequest.GetString("password"), config.Passwordkey).Replace("+", "[");
                        }
                        else
                        {

                            LoginLogs.DeleteLoginLog(DNTRequest.GetIP());
                            UserCredits.UpdateUserCredits(uid);
                            ForumUtils.WriteUserCookie(
                                    uid,
                                    Utils.StrToInt(DNTRequest.GetString("expires"), -1),
                                    config.Passwordkey,
                                    DNTRequest.GetInt("templateid", 0),
                                    DNTRequest.GetInt("loginmode", -1));
                            OnlineUsers.UpdateAction(olid, UserAction.Login.ActionID, 0);
                            //无延迟更新在线信息
                            oluserinfo = OnlineUsers.UpdateInfo(config.Passwordkey, config.Onlinetimeout);
                            olid = oluserinfo.Olid;
                            Users.UpdateUserLastvisit(uid, DNTRequest.GetIP());

                            string reurl = Utils.UrlDecode(ForumUtils.GetReUrl());
                            if (reurl.IndexOf("register.aspx") < 0)
                            {
                                SetUrl(reurl);
                            }
                            else
                            {
                                SetUrl("index.aspx");
                            }

                            APIConfigInfo apiInfo = APIConfigs.GetConfig();
                            if (apiInfo.Enable)
                            {
                                APILogin(apiInfo);
                            }

                            AddMsgLine("登录成功, 返回登录前页面");

                            username = DNTRequest.GetString("username");
                            userid = uid;
                            usergroupinfo = UserGroups.GetUserGroupInfo(userinfo.Groupid);
                            // 根据用户组得到相关联的管理组id
                            useradminid = usergroupinfo.Radminid;

                            SetMetaRefresh();
                            SetShowBackLink(false);

                            SetLeftMenuRefresh();

                            loginsubmit = false;
                        }
                    }
                }
                else
                {
                    int errcount = LoginLogs.UpdateLoginLog(DNTRequest.GetIP(), true);
                    if (errcount > 5)
                    {
                        AddErrLine("您已经输入密码5次错误, 请15分钟后再试");
                    }
                    else
                    {
                        AddErrLine(string.Format("密码或安全提问第{0}次错误, 您最多有5次机会重试", errcount.ToString()));
                    }
                }
            }
          
        }
#if NET1
        ApplicationInfo appInfo = null;
        private void APILogin(APIConfigInfo apiInfo)
        {
            ApplicationInfoCollection appcollection = apiInfo.AppCollection;
            foreach (ApplicationInfo newapp in appcollection)
            {
				if (newapp.APIKey == DNTRequest.GetString("api_key"))
				{
					appInfo = newapp;
				}
            }
            if (appInfo == null)
                return;

            this.Load += new EventHandler(login_Load);

        }

        void login_Load(object sender, EventArgs e)
        {
            RedirectAPILogin(appInfo);
        }
#else
        private void APILogin(APIConfigInfo apiInfo)
        {
            ApplicationInfo appInfo = null;
            ApplicationInfoCollection appcollection = apiInfo.AppCollection;
            foreach (ApplicationInfo newapp in appcollection)
            {
                if (newapp.APIKey == DNTRequest.GetString("api_key"))
                {
                    appInfo = newapp;
                }
            }
            if (appInfo == null)
                return;

            this.Load += delegate
            {
                RedirectAPILogin(appInfo);
                this.Load += delegate { };
            };

        }


#endif

        private void RedirectAPILogin(ApplicationInfo appInfo)
        {
            string expires = DNTRequest.GetString("expires");
            if (expires == string.Empty)
            {
                expires = Request.Cookies["dnt"]["expires"].ToString();
            }
            //CreateToken
            OnlineUsers.UpdateAction(olid, UserAction.Login.ActionID, 0);
            string next = DNTRequest.GetString("next");
            string time = string.Empty;// = DateTime.Parse(OnlineUsers.GetOnlineUser(olid).Lastupdatetime).ToString("yyyy-MM-dd HH:mm:ss");
            if (OnlineUsers.GetOnlineUser(olid) == null)
            {
                time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                time = DateTime.Parse(OnlineUsers.GetOnlineUser(olid).Lastupdatetime).ToString("yyyy-MM-dd HH:mm:ss");
            }
            string authToken = DES.Encode(string.Format("{0},{1},{2}", olid.ToString(), time, expires), appInfo.Secret.Substring(0, 10)).Replace("+", "[");
            string reurl = string.Format("{0}{1}auth_token={2}{3}", appInfo.CallbackUrl, appInfo.CallbackUrl.IndexOf("?") > 0 ? "&" : "?", authToken, next == string.Empty ? next : "&next=" + next);
            //string reurl = "http://nt.discuz.net/?auth_token=" + authToken;
            Response.Redirect(reurl);
        }

        private void SetLeftMenuRefresh()
        {
            StringBuilder script = new StringBuilder();
            script.Append("if (top.document.getElementById('leftmenu')){");
            script.Append("		top.frames['leftmenu'].location.reload();");
            script.Append("}");

            AddScript(script.ToString());
        }
    }
}