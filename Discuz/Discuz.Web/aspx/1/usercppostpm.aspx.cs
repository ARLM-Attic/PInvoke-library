using System;
using System.Data;
using System.Data.SqlClient;
using Discuz.Common;
using Discuz.Forum;
using Discuz.Web.UI;
using Discuz.Entity;
using Discuz.Config;
using Discuz.Data;

namespace Discuz.Web
{
    /// <summary>
    /// 撰写短消息页面
    /// </summary>
    public class usercppostpm : PageBase
    {
        #region 页面变量

        /// <summary>
        /// 短消息收件人
        /// </summary>
        public string msgto;

        /// <summary>
        /// 短消息标题
        /// </summary>
        public string subject;

        /// <summary>
        /// 短消息内容
        /// </summary>
        public string message;

        /// <summary>
        /// 短消息收件人Id
        /// </summary>
        public int msgtoid = 0;

        /// <summary>
        /// 当前用户信息
        /// </summary>
        public UserInfo user = new UserInfo();

        #endregion

        protected override void ShowPage()
        {
            pagetitle = "撰写短消息";

            if (userid == -1)
            {
                AddErrLine("你尚未登录");

                return;
            }
            user = Users.GetUserInfo(userid);

            if (!CheckPermission())
            {
                return;
            }

            if (DNTRequest.IsPost())
            {
                if (!CheckPermissionAfterPost())
                {
                    return;
                }

                #region 创建并发送短消息

                PrivateMessageInfo pm = new PrivateMessageInfo();

                string curdatetime = Utils.GetDateTime();
                // 收件箱
                if (useradminid == 1)
                {
                    pm.Message = Utils.HtmlEncode(DNTRequest.GetString("message"));
                    pm.Subject = Utils.HtmlEncode(DNTRequest.GetString("subject"));
                }
                else
                {
                    pm.Message =
                   Utils.HtmlEncode(ForumUtils.BanWordFilter(DNTRequest.GetString("message")));
                    pm.Subject =
                        Utils.HtmlEncode(ForumUtils.BanWordFilter(DNTRequest.GetString("subject")));
                }

                if (ForumUtils.HasBannedWord(pm.Message) || ForumUtils.HasBannedWord(pm.Subject))
                {
                    //HasBannedWord 指定的字符串中是否含有禁止词汇

                    AddErrLine("对不起, 您提交的内容包含不良信息, 因此无法提交, 请返回修改!");
                    return;
                }


                if (ForumUtils.HasAuditWord(pm.Message) || ForumUtils.HasAuditWord(pm.Subject))
                {


                    AddErrLine("对不起, 您提交的内容包含不良信息, 因此无法提交, 请返回修改!");
                    return;
                }

                pm.Message = ForumUtils.BanWordFilter(pm.Message);
                pm.Subject = ForumUtils.BanWordFilter(pm.Subject);

                pm.Msgto = DNTRequest.GetString("msgto");
                pm.Msgtoid = msgtoid;
                pm.Msgfrom = username;
                pm.Msgfromid = userid;
                pm.New = 1;
                pm.Postdatetime = curdatetime;


                if (!DNTRequest.GetString("savetousercpdraftbox").Equals(""))
                {
                    // 检查发送人的短消息是否已超过发送人用户组的上限
                    if (PrivateMessages.GetPrivateMessageCount(userid, -1) >= usergroupinfo.Maxpmnum)
                    {
                        AddErrLine("抱歉,您的短消息已达到上限,无法保存到草稿箱");
                        return;
                    }
                    // 只将消息保存到草稿箱
                    pm.Folder = 2;
                    if (UserCredits.UpdateUserCreditsBySendpms(base.userid) == -1)
                    {
                        AddErrLine("您的积分不足, 不能发送短消息");
                        return;
                    }
                    pm.Pmid = PrivateMessages.CreatePrivateMessage(pm, 0);

                    //发送邮件通知
                    if (DNTRequest.GetString("emailnotify") == "on")
                    {
                        SendNotifyEmail(Users.GetUserInfo(msgtoid).Email.Trim(), pm);
                    }

                    SetUrl("usercpdraftbox.aspx");
                    SetMetaRefresh();
                    SetShowBackLink(true);
                    AddMsgLine("已将消息保存到草稿箱");
                }
                else if (!DNTRequest.GetString("savetosentbox").Equals(""))
                {
                    // 检查接收人的短消息是否已超过接收人用户组的上限
                    UserInfo touser = Users.GetUserInfo(msgtoid);
                    //管理组不受接收人短消息上限限制
                    int radminId = UserGroups.GetUserGroupInfo(usergroupid).Radminid;
                    if (!(radminId > 0 && radminId <= 3) && PrivateMessages.GetPrivateMessageCount(msgtoid, -1) >=
                        UserGroups.GetUserGroupInfo(touser.Groupid).Maxpmnum)
                    {
                        AddErrLine("抱歉,接收人的短消息已达到上限,无法接收");
                        return;
                    }

                    if (!Utils.InArray(Convert.ToInt32(touser.Newsletter).ToString(), "2,3,6,7"))
                    {
                        AddErrLine("抱歉,接收人拒绝接收短消息");
                        return;
                    }
                    // 检查发送人的短消息是否已超过发送人用户组的上限
                    if (PrivateMessages.GetPrivateMessageCount(userid, -1) >= usergroupinfo.Maxpmnum)
                    {
                        AddErrLine("抱歉,您的短消息已达到上限,无法保存到发件箱");
                        return;
                    }
                    // 发送消息且保存到发件箱
                    pm.Folder = 0;
                    if (UserCredits.UpdateUserCreditsBySendpms(base.userid) == -1)
                    {
                        AddErrLine("您的积分不足, 不能发送短消息");
                        return;
                    }
                    pm.Pmid = PrivateMessages.CreatePrivateMessage(pm, 1);

                    //发送邮件通知
                    if (DNTRequest.GetString("emailnotify") == "on")
                    {
                        SendNotifyEmail(touser.Email.Trim(), pm);
                    }

                    // 更新在线表中的用户最后发帖时间
                    OnlineUsers.UpdatePostPMTime(olid);

                    SetUrl("usercpsentbox.aspx");
                    SetMetaRefresh();
                    SetShowBackLink(true);
                    AddMsgLine("发送完毕, 且已将消息保存到发件箱");
                }
                else
                {
                    UserInfo touser = Users.GetUserInfo(msgtoid);
                    // 检查接收人的短消息是否已超过接收人用户组的上限,管理组不受接收人短消息上限限制
                    int radminId = UserGroups.GetUserGroupInfo(usergroupid).Radminid;
                    if (!(radminId > 0 && radminId <=3) && PrivateMessages.GetPrivateMessageCount(msgtoid, -1) >=
                        UserGroups.GetUserGroupInfo(touser.Groupid).Maxpmnum)
                    {
                        AddErrLine("抱歉,接收人的短消息已达到上限,无法接收");
                        return;
                    }
                    if (!Utils.InArray(Convert.ToInt32(touser.Newsletter).ToString(), "2,3,6,7"))
                    {
                        AddErrLine("抱歉,接收人拒绝接收短消息");
                        return;
                    }

                    // 发送消息但不保存到发件箱
                    pm.Folder = 0;
                    if (UserCredits.UpdateUserCreditsBySendpms(base.userid) == -1)
                    {
                        AddErrLine("您的积分不足, 不能发送短消息");
                        return;
                    }
                    pm.Pmid = PrivateMessages.CreatePrivateMessage(pm, 0);

                    //发送邮件通知
                    if (DNTRequest.GetString("emailnotify") == "on")
                    {
                        SendNotifyEmail(touser.Email.Trim(), pm);
                    }

                    SetUrl("usercpinbox.aspx");
                    SetMetaRefresh();
                    SetShowBackLink(true);
                    AddMsgLine("发送完毕");
                }

                #endregion
            }

            msgto = Utils.HtmlEncode(DNTRequest.GetString("msgto"));

            msgtoid = DNTRequest.GetInt("msgtoid", 0);
            if (msgtoid > 0)
            {
                msgto = Users.GetUserName(msgtoid).Trim();
            }

            subject = Utils.HtmlEncode(DNTRequest.GetString("subject"));
            message = Utils.HtmlEncode(DNTRequest.GetString("message"));

            string action = DNTRequest.GetQueryString("action").ToLower();
            if (action.CompareTo("re") == 0 || action.CompareTo("fw") == 0) //回复或者转发
            {
                int pmid = DNTRequest.GetQueryInt("pmid", -1);
                if (pmid != -1)
                {
                    PrivateMessageInfo pm = PrivateMessages.GetPrivateMessageInfo(pmid);
                    if (pm != null)
                    {
                        if (pm.Msgtoid == userid || pm.Msgfromid == userid)
                        {
                            if (action.CompareTo("re") == 0)
                            {
                                msgto = Utils.HtmlEncode(pm.Msgfrom);
                            }
                            else
                            {
                                msgto = "";
                            }
                            subject = Utils.HtmlEncode(action) + ":" + pm.Subject;
                            message = Utils.HtmlEncode("> ") + pm.Message.Replace("\n", "\n> ") + "\r\n\r\n";
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 提交后的权限检查
        /// </summary>
        /// <returns></returns>
        private bool CheckPermissionAfterPost()
        {
            if (ForumUtils.IsCrossSitePost())
            {
                AddErrLine("您的请求来路不正确，无法提交。如果您安装了某种默认屏蔽来路信息的个人防火墙软件(如 Norton Internet Security)，请设置其不要禁止来路信息后再试。");
                return false;
            }

            if (DNTRequest.GetString("message").Equals(""))
            {
                AddErrLine("内容不能为空");

                return false;
            }

            if (DNTRequest.GetString("message").Length > 3000)
            {
                AddErrLine("内容不能超过3000字");

                return false;
            }

            if (DNTRequest.GetString("msgto").Equals(""))
            {
                AddErrLine("接收人不能为空");

                return false;
            }

            if (DNTRequest.GetString("subject").Trim().Equals(""))
            {
                AddErrLine("标题不能为空");

                return false;
            }

            if (DNTRequest.GetString("subject").Trim().Length > 60)
            {
                AddErrLine("标题不能超过60字");

                return false;
            }

            // 不能给负责发送新用户注册欢迎信件的用户名称发送消息
            if (DNTRequest.GetString("msgto") == PrivateMessages.SystemUserName)
            {
                AddErrLine("不能给系统发送消息");
                return false;
            }

            msgtoid = Users.GetUserID(DNTRequest.GetString("msgto"));
            if (msgtoid == -1)
            {
                AddErrLine("接收人不是注册用户");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 不论是否提交都有的权限检查
        /// </summary>
        /// <returns></returns>
        private bool CheckPermission()
        {
            // 如果是受灌水限制用户, 则判断是否是灌水
            AdminGroupInfo admininfo = AdminGroups.GetAdminGroupInfo(useradminid);
            if (admininfo == null || admininfo.Disablepostctrl != 1)
            {
                int Interval = Utils.StrDateDiffSeconds(lastpostpmtime, config.Postinterval * 2);
                if (Interval < 0)
                {
                    AddErrLine(string.Format("系统规定发帖或发短消息间隔为{0}秒, 您还需要等待 {1} 秒", (config.Postinterval * 2).ToString(), (Interval * -1).ToString()));
                    return false;
                }
            }

            if (!UserCredits.CheckUserCreditsIsEnough(userid, 1, CreditsOperationType.SendMessage, -1))
            {
                AddErrLine("您的积分不足, 不能发送短消息");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 发送邮件通知
        /// </summary>
        /// <param name="email">接收人邮箱</param>
        /// <param name="privatemessageinfo">短消息对象</param>
        public void SendNotifyEmail(string email, PrivateMessageInfo pm)
        {
            string jumpurl = "http://" + DNTRequest.GetCurrentFullHost() + "/usercpshowpm.aspx?pmid=" + pm.Pmid;
            System.Text.StringBuilder sb_body = new System.Text.StringBuilder("# 论坛短消息: <a href=\"" + jumpurl + "\" target=\"_blank\">" + pm.Subject + "</a>");
            //发送人邮箱
            string cur_email = Users.GetShortUserInfo(userid).Email.Trim();
            sb_body.Append("\r\n");
            sb_body.Append("\r\n");
            sb_body.Append(pm.Message);
            sb_body.Append("\r\n<hr/>");
            sb_body.Append("作 者:" + pm.Msgfrom);
            sb_body.Append("\r\n");
            sb_body.Append("Email:<a href=\"mailto:" + cur_email + "\" target=\"_blank\">" + cur_email + "</a>");
            sb_body.Append("\r\n");
            sb_body.Append("URL:<a href=\"" + jumpurl + "\" target=\"_blank\">" + jumpurl + "</a>");
            sb_body.Append("\r\n");
            sb_body.Append("时 间:" + pm.Postdatetime);
            Emails.SendEmailNotify(email, "[" + config.Forumtitle + "短消息通知]" + pm.Subject, sb_body.ToString());
        }
    } //class end
}