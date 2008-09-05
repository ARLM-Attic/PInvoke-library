using System;
using System.Data;
using Discuz.Common;
using Discuz.Forum;
using Discuz.Config;
using Discuz.Entity;
using Discuz.Web.UI;
using System.Text.RegularExpressions;

namespace Discuz.Web
{
    /// <summary>
    /// 注册页
    /// </summary>
    public class register : PageBase
    {
        #region 页面变量
        /// <summary>
        /// 可用的模板列表
        /// </summary>
        public DataTable templatelist;
        /// <summary>
        /// 此变量等于1时创建用户,否则显示填写用户信息界面
        /// </summary>
        public string createuser;
        /// <summary>
        /// 是否同意注册协议
        /// </summary>
        public string agree;
        #endregion

        protected override void ShowPage()
        {
            pagetitle = "用户注册";

            createuser = DNTRequest.GetString("createuser");
            agree = DNTRequest.GetFormString("agree");
            if (config.Rules == 0)
            {
                agree = "true";
            }

            if (userid != -1)
            {
                SetUrl(BaseConfigs.GetForumPath);
                SetMetaRefresh();
                SetShowBackLink(false);
                AddMsgLine("不能重复注册用户");
                ispost = true;
                createuser = "1";
                agree = "yes";

                return;
            }


            templatelist = Templates.GetValidTemplateList();

            if (config.Regstatus != 1)
            {
                AddErrLine("论坛当前禁止新用户注册");
                return;
            }

            if (config.Regctrl > 0)
            {
                ShortUserInfo userinfo = Users.GetShortUserInfoByIP(DNTRequest.GetIP());
                if (userinfo != null)
                {
                    int Interval = Utils.StrDateDiffHours(userinfo.Joindate, config.Regctrl);
                    if (Interval <=0)
                    {
                        AddErrLine("抱歉, 系统设置了IP注册间隔限制, 您必须在 " + (Interval * -1).ToString() + " 小时后才可以注册");
                        return;
                    }
                }
            }

            if (config.Ipregctrl.Trim() != "")
            {
                string[] regctrl = Utils.SplitString(config.Ipregctrl, "\n");
                //string[] userip = Utils.SplitString(DNTRequest.GetIP(),".");
                if (Utils.InIPArray(DNTRequest.GetIP(), regctrl))
                {
                    ShortUserInfo userinfo = Users.GetShortUserInfoByIP(DNTRequest.GetIP());
                    if (userinfo != null)
                    {
                        int Interval = Utils.StrDateDiffHours(userinfo.Joindate, 72);
                        if (Interval < 0)
                        {
                            AddErrLine("抱歉, 系统设置了特殊IP注册限制, 您必须在 " + (Interval * -1).ToString() + " 小时后才可以注册");
                            return;
                        }
                    }
                }
            }


            //如果提交了用户注册信息...
            if (!createuser.Equals("") && ispost)
            {
                SetShowBackLink(true);

                string tmpUsername = DNTRequest.GetString("username");

                string email = DNTRequest.GetString("email").Trim().ToLower();

                string tmpBday = DNTRequest.GetString("bday").Trim();
                if (tmpBday == "")
                {
                    tmpBday = DNTRequest.GetString("bday_y").Trim()
                            + "-"
                            + DNTRequest.GetString("bday_m").Trim()
                            + "-"
                            + DNTRequest.GetString("bday_d").Trim();
                }
                if (tmpBday == "--")
                {
                    tmpBday = "";
                }

                ValidateUserInfo(tmpUsername, email, tmpBday);


                if (IsErr())
                {
                    return;
                }
                
                
                if (Users.Exists(tmpUsername))
                {
                    //如果用户名符合注册规则, 则判断是否已存在
                    AddErrLine("请不要重复提交！");
                    return;
                }
                // 如果找不到0积分的用户组则用户自动成为待验证用户


                UserInfo userinfo = new UserInfo();
                userinfo.Username = tmpUsername;
                userinfo.Nickname = Utils.HtmlEncode(DNTRequest.GetString("nickname"));
                userinfo.Password = Utils.MD5(DNTRequest.GetString("password"));
                userinfo.Secques = ForumUtils.GetUserSecques(DNTRequest.GetInt("question", 0), DNTRequest.GetString("answer"));
                userinfo.Gender = DNTRequest.GetInt("gender", 0);
                userinfo.Adminid = 0;
                userinfo.Groupexpiry = 0;
                userinfo.Extgroupids = "";
                userinfo.Regip = DNTRequest.GetIP();
                userinfo.Joindate = Utils.GetDateTime();
                userinfo.Lastip = DNTRequest.GetIP();
                userinfo.Lastvisit = Utils.GetDateTime();
                userinfo.Lastactivity = Utils.GetDateTime();
                userinfo.Lastpost = Utils.GetDateTime();
                userinfo.Lastpostid = 0;
                userinfo.Lastposttitle = "";
                userinfo.Posts = 0;
                userinfo.Digestposts = 0;
                userinfo.Oltime = 0;
                userinfo.Pageviews = 0;
                userinfo.Credits = 0;
                userinfo.Extcredits1 = Scoresets.GetScoreSet(1).Init;
                userinfo.Extcredits2 = Scoresets.GetScoreSet(2).Init;
                userinfo.Extcredits3 = Scoresets.GetScoreSet(3).Init;
                userinfo.Extcredits4 = Scoresets.GetScoreSet(4).Init;
                userinfo.Extcredits5 = Scoresets.GetScoreSet(5).Init;
                userinfo.Extcredits6 = Scoresets.GetScoreSet(6).Init;
                userinfo.Extcredits7 = Scoresets.GetScoreSet(7).Init;
                userinfo.Extcredits8 = Scoresets.GetScoreSet(8).Init;
                userinfo.Avatarshowid = 0;
                userinfo.Email = email;
                userinfo.Bday = tmpBday;
                userinfo.Sigstatus = DNTRequest.GetInt("sigstatus", 0);

                if (userinfo.Sigstatus != 0)
                {
                    userinfo.Sigstatus = 1;
                }
                userinfo.Tpp = DNTRequest.GetInt("tpp", 0);
                userinfo.Ppp = DNTRequest.GetInt("ppp", 0);
                userinfo.Templateid = DNTRequest.GetInt("templateid", 0);
                userinfo.Pmsound = DNTRequest.GetInt("pmsound", 0);
                userinfo.Showemail = DNTRequest.GetInt("showemail", 0);

                int receivepmsetting = 1;
                foreach (string rpms in DNTRequest.GetString("receivesetting").Split(','))
                {
                    if (rpms != string.Empty)
                    {
                        int tmp = int.Parse(rpms);
                        receivepmsetting = receivepmsetting | tmp;
                    }
                }

                if (config.Regadvance == 0)
                {
                    receivepmsetting = 7;
                }

                userinfo.Newsletter = (ReceivePMSettingType)receivepmsetting;
                userinfo.Invisible = DNTRequest.GetInt("invisible", 0);
                userinfo.Newpm = 0;
                userinfo.Medals = "";
                if (config.Welcomemsg == 1)
                {
                    userinfo.Newpm = 1;
                }
                userinfo.Accessmasks = DNTRequest.GetInt("accessmasks", 0);
                userinfo.Website = Utils.HtmlEncode(DNTRequest.GetString("website"));
                userinfo.Icq = Utils.HtmlEncode(DNTRequest.GetString("icq"));
                userinfo.Qq = Utils.HtmlEncode(DNTRequest.GetString("qq"));
                userinfo.Yahoo = Utils.HtmlEncode(DNTRequest.GetString("yahoo"));
                userinfo.Msn = Utils.HtmlEncode(DNTRequest.GetString("msn"));
                userinfo.Skype = Utils.HtmlEncode(DNTRequest.GetString("skype"));
                userinfo.Location = Utils.HtmlEncode(DNTRequest.GetString("location"));
                if (usergroupinfo.Allowcstatus == 1)
                {
                    userinfo.Customstatus = Utils.HtmlEncode(DNTRequest.GetString("customstatus"));
                }
                else
                {
                    userinfo.Customstatus = "";
                }
                userinfo.Avatar = @"avatars\common\0.gif";
                userinfo.Avatarwidth = 0;
                userinfo.Avatarheight = 0;
                userinfo.Bio = DNTRequest.GetString("bio");
                userinfo.Signature = Utils.HtmlEncode(ForumUtils.BanWordFilter(DNTRequest.GetString("signature")));

                PostpramsInfo postpramsinfo = new PostpramsInfo();
                postpramsinfo.Usergroupid = usergroupid;
                postpramsinfo.Attachimgpost = config.Attachimgpost;
                postpramsinfo.Showattachmentpath = config.Showattachmentpath;
                postpramsinfo.Hide = 0;
                postpramsinfo.Price = 0;
                postpramsinfo.Sdetail = userinfo.Signature;
                postpramsinfo.Smileyoff = 1;
                postpramsinfo.Bbcodeoff = 1 - usergroupinfo.Allowsigbbcode;
                postpramsinfo.Parseurloff = 1;
                postpramsinfo.Showimages = usergroupinfo.Allowsigimgcode;
                postpramsinfo.Allowhtml = 0;
                postpramsinfo.Smiliesinfo = Smilies.GetSmiliesListWithInfo();
                postpramsinfo.Customeditorbuttoninfo = Editors.GetCustomEditButtonListWithInfo();
                postpramsinfo.Smiliesmax = config.Smiliesmax;

                userinfo.Sightml = UBB.UBBToHTML(postpramsinfo);

                //
                userinfo.Authtime = Utils.GetDateTime();

                //邮箱激活链接验证
                if (config.Regverify == 1)
                {
                    userinfo.Authstr = ForumUtils.CreateAuthStr(20);
                    userinfo.Authflag = 1;
                    userinfo.Groupid = 8;
                    SendEmail(tmpUsername, DNTRequest.GetString("password").Trim(), DNTRequest.GetString("email").Trim(), userinfo.Authstr);
                }
                //系统管理员进行后台验证
                else if (config.Regverify == 2)
                {
                    userinfo.Authstr = DNTRequest.GetString("website");
                    userinfo.Groupid = 8;
                    userinfo.Authflag = 1;
                }
                else
                {
                    userinfo.Authstr = "";
                    userinfo.Authflag = 0;
                    userinfo.Groupid = UserCredits.GetCreditsUserGroupID(0).Groupid;
                }
                userinfo.Realname = DNTRequest.GetString("realname");
                userinfo.Idcard = DNTRequest.GetString("idcard");
                userinfo.Mobile = DNTRequest.GetString("mobile");
                userinfo.Phone = DNTRequest.GetString("phone");

                int uid = Users.CreateUser(userinfo);
                userinfo.Uid = uid;
                if (config.Welcomemsg == 1)
                {
                    PrivateMessageInfo privatemessageinfo = new PrivateMessageInfo();

                    string curdatetime = Utils.GetDateTime();
                    // 收件箱
                    privatemessageinfo.Message = config.Welcomemsgtxt;
                    privatemessageinfo.Subject = "欢迎您的加入! (请勿回复本信息)";
                    privatemessageinfo.Msgto = userinfo.Username;
                    privatemessageinfo.Msgtoid = uid;
                    privatemessageinfo.Msgfrom = PrivateMessages.SystemUserName;
                    privatemessageinfo.Msgfromid = 0;
                    privatemessageinfo.New = 1;
                    privatemessageinfo.Postdatetime = curdatetime;
                    privatemessageinfo.Folder = 0;
                    PrivateMessages.CreatePrivateMessage(privatemessageinfo, 0);
                }

                if (config.Regverify == 0)
                {
                    UserCredits.UpdateUserCredits(uid);
                    ForumUtils.WriteUserCookie(userinfo, -1, config.Passwordkey);
                    OnlineUsers.UpdateAction(olid, UserAction.Register.ActionID, 0, config.Onlinetimeout);


                    Statistics.ReSetStatisticsCache();

                    SetUrl("index.aspx");
                    SetMetaRefresh();
                    SetShowBackLink(false);
                    AddMsgLine("注册成功, 返回登录页");
                }
                else
                {
                    SetUrl("index.aspx");
                    SetMetaRefresh(5);
                    SetShowBackLink(false);
                    Statistics.ReSetStatisticsCache();
                    if (config.Regverify == 1)
                    {
                        AddMsgLine("注册成功, 请您到您的邮箱中点击激活链接来激活您的帐号");
                    }

                    if (config.Regverify == 2)
                    {
                        AddMsgLine("注册成功, 但需要系统管理员审核您的帐户后才可登陆使用");
                    }
                }
                agree = "yes";
            }
        }

        /// <summary>
        /// 验证用户信息
        /// </summary>
        /// <param name="username"></param>
        /// <param name="email"></param>
        /// <param name="birthday"></param>
        private void ValidateUserInfo(string username, string email, string birthday)
        {

            #region CheckUserName
            if (username.Equals(""))
            {
                AddErrLine("用户名不能为空");
                return;
            }
            if (username.Length > 20)
            {
                //如果用户名超过20...
                AddErrLine("用户名不得超过20个字符");
                return;
            }
            if (Utils.GetStringLength(username) < 3)
            {
                AddErrLine("用户名不得小于3个字符");
                return;
            }
            if (username.IndexOf("　") != -1)
            {
                //如果用户名符合注册规则, 则判断是否已存在
                AddErrLine("用户名中不允许包含全角空格符");
                return;
            }
            if (username.IndexOf(" ") != -1)
            {
                //如果用户名符合注册规则, 则判断是否已存在
                AddErrLine("用户名中不允许包含空格");
                return;
            }
            if (username.IndexOf(":") != -1)
            {
                //如果用户名符合注册规则, 则判断是否已存在
                AddErrLine("用户名中不允许包含冒号");
                return;
            }
            if (Users.Exists(username))
            {
                //如果用户名符合注册规则, 则判断是否已存在
                AddErrLine("该用户名已存在");
                return;
            }
            if ((!Utils.IsSafeSqlString(username)) || (!Utils.IsSafeUserInfoString(username)))
            {
                AddErrLine("用户名中存在非法字符");
                return;
            }
            // 如果用户名属于禁止名单, 或者与负责发送新用户注册欢迎信件的用户名称相同...
            if (username.Trim() == PrivateMessages.SystemUserName ||
                     ForumUtils.IsBanUsername(username, config.Censoruser))
            {
                AddErrLine("用户名 \"" + username + "\" 不允许在本论坛使用");
                return;
            }
            #endregion

            #region CheckPassword
            // 检查密码
            if (DNTRequest.GetString("password").Equals(""))
            {
                AddErrLine("密码不能为空");
                return;
            }
            if (!DNTRequest.GetString("password").Equals(DNTRequest.GetString("password2")))
            {
                AddErrLine("两次密码输入必须相同");
                return;
            }
            if (DNTRequest.GetString("password").Length < 6)
            {
                AddErrLine("密码不得少于6个字符");
                return;
            }
            #endregion

            #region CheckMail
            if (email.Equals(""))
            {
                AddErrLine("Email不能为空");
                return;
            }

            if (!Utils.IsValidEmail(email))
            {
                AddErrLine("Email格式不正确");
                return;
            }
            if (config.Doublee == 0 && Users.FindUserEmail(email) != -1)
            {
                AddErrLine("Email: \"" + email + "\" 已经被其它用户注册使用");
                return;
            }

            string emailhost = Utils.GetEmailHostName(email);
                // 允许名单规则优先于禁止名单规则
                if (config.Accessemail.Trim() != "")
                {
                    // 如果email后缀 不属于 允许名单
                    if (!Utils.InArray(emailhost, config.Accessemail.Replace("\r\n", "\n"), "\n"))
                    {
                        AddErrLine("Email: \"" + email + "\" 不在本论坛允许范围之类, 本论坛只允许用户使用这些Email地址注册: " +
                                   config.Accessemail.Replace("\n", ",").Replace("\r", ""));
                        return;
                    }
                }
                if (config.Censoremail.Trim() != "")
                {
                    // 如果email后缀 属于 禁止名单
                    if (Utils.InArray(email, config.Censoremail.Replace("\r\n", "\n"), "\n"))
                    {
                        AddErrLine("Email: \"" + email + "\" 不允许在本论坛使用, 本论坛不允许用户使用的Email地址包括: " +
                                   config.Censoremail.Replace("\n", ",").Replace("\r", ""));
                        return;
                    }
                }
            #endregion

            #region CheckRealInfo
            //实名验证
            if (config.Realnamesystem == 1)
            {
                if (DNTRequest.GetString("realname").Trim() == "")
                {
                    AddErrLine("真实姓名不能为空");
                    return;
                }
                if (DNTRequest.GetString("realname").Trim().Length > 10)
                {
                    AddErrLine("真实姓名不能大于10个字符");
                    return;
                }
                if (DNTRequest.GetString("idcard").Trim() == "")
                {
                    AddErrLine("身份证号码不能为空");
                    return;
                }
                if (DNTRequest.GetString("idcard").Trim().Length > 20)
                {
                    AddErrLine("身份证号码不能大于20个字符");
                    return;
                }
                if (DNTRequest.GetString("mobile").Trim() == "" && DNTRequest.GetString("phone").Trim() == "")
                {
                    AddErrLine("移动电话号码或固定电话号码必须填写其中一项");
                    return;
                }
                if (DNTRequest.GetString("mobile").Trim().Length > 20)
                {
                    AddErrLine("移动电话号码不能大于20个字符");
                    return;
                }
                if (DNTRequest.GetString("phone").Trim().Length > 20)
                {
                    AddErrLine("固定电话号码不能大于20个字符");
                    return;
                }
            }
            #endregion

            if (DNTRequest.GetString("idcard").Trim() != "" &&
                !Regex.IsMatch(DNTRequest.GetString("idcard").Trim(), @"^[\x20-\x80]+$"))
            {
                AddErrLine("身份证号码中含有非法字符");
                return;
            }

            if (DNTRequest.GetString("mobile").Trim() != "" &&
                !Regex.IsMatch(DNTRequest.GetString("mobile").Trim(), @"^[\d|-]+$"))
            {
                AddErrLine("移动电话号码中含有非法字符");
                return;
            }

            if (DNTRequest.GetString("phone").Trim() != "" &&
                !Regex.IsMatch(DNTRequest.GetString("phone").Trim(), @"^[\d|-]+$"))
            {
                AddErrLine("固定电话号码中含有非法字符");
                return;
            }


            //用户注册模板中,生日可以单独用一个名为bday的文本框, 也可以分别用bday_y bday_m bday_d三个文本框, 用户可不填写

            if (!Utils.IsDateString(birthday) && !birthday.Equals(""))
            {
                AddErrLine("生日格式错误, 如果不想填写生日请置空");
                return;
            }
            if (DNTRequest.GetString("bio").Length > 500)
            {
                //如果自我介绍超过500...
                AddErrLine("自我介绍不得超过500个字符");
                return;
            }
            if (DNTRequest.GetString("signature").Length > 500)
            {
                //如果签名超过500...
                AddErrLine("签名不得超过500个字符");
                return;
            }
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="emailaddress"></param>
        /// <param name="authstr"></param>
        private void SendEmail(string username, string password, string emailaddress, string authstr)
        {
            Emails.DiscuzSmtpMail(username, emailaddress, password, authstr);
        }
    }
}