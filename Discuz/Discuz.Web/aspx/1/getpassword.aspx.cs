using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Discuz.Common;
using Discuz.Forum;
using Discuz.Web.UI;

namespace Discuz.Web
{
    /// <summary>
    /// 找回密码页
    /// </summary>
    public class getpassword : PageBase
    {
        protected override void ShowPage()
        {
            pagetitle = "密码找回";
            username = Utils.RemoveHtml(DNTRequest.GetString("username"));

            //如果提交...
            if (DNTRequest.IsPost())
            {
                if (ForumUtils.IsCrossSitePost())
                {
                    AddErrLine("您的请求来路不正确，无法提交。如果您安装了某种默认屏蔽来路信息的个人防火墙软件(如 Norton Internet Security)，请设置其不要禁止来路信息后再试。");
                    return;
                }

                base.SetBackLink("getpassword.aspx?username=" + username);

                if (!Users.Exists(username))
                {
                    AddErrLine("用户不存在");
                    return;
                }

                if (DNTRequest.GetString("email").Equals(""))
                {
                    AddErrLine("电子邮件不能为空");
                    return;
                }

                if (IsErr())
                {
                    return;
                }

                int uid =
                    Users.CheckEmailAndSecques(username, DNTRequest.GetString("email"), DNTRequest.GetInt("question", 0),
                                               DNTRequest.GetString("answer"));
                if (uid != -1)
                {
                    string Authstr = ForumUtils.CreateAuthStr(20);
                    Users.UpdateAuthStr(uid, Authstr, 2);

                    string title = config.Forumtitle + " 取回密码说明";
                    StringBuilder body = new StringBuilder();
                    body.Append(username);
                    body.Append("您好!<br />这封信是由 ");
                    body.Append(config.Forumtitle);
                    body.Append(" 发送的.<br /><br />您收到这封邮件,是因为在我们的论坛上这个邮箱地址被登记为用户邮箱,且该用户请求使用 Email 密码重置功能所致.");
                    body.Append("<br /><br />----------------------------------------------------------------------");
                    body.Append("<br />重要！");
                    body.Append("<br /><br />----------------------------------------------------------------------");
                    body.Append("<br /><br />如果您没有提交密码重置的请求或不是我们论坛的注册用户,请立即忽略并删除这封邮件.只在您确认需要重置密码的情况下,才继续阅读下面的内容.");
                    body.Append("<br /><br />----------------------------------------------------------------------");
                    body.Append("<br />密码重置说明");
                    body.Append("<br /><br />----------------------------------------------------------------------");
                    body.Append("<br /><br />您只需在提交请求后的三天之内,通过点击下面的链接重置您的密码:");
                    body.AppendFormat("<br /><br /><a href={0}/setnewpassword.aspx?uid={1}&id={2} target=_blank>", GetForumPath(), uid, Authstr);
                    body.Append(GetForumPath());
                    body.Append("/setnewpassword.aspx?uid=");
                    body.Append(uid);
                    body.Append("&id=");
                    body.Append(Authstr);
                    body.Append("</a>");
                    body.Append("<br /><br />(如果上面不是链接形式,请将地址手工粘贴到浏览器地址栏再访问)");
                    body.Append("<br /><br />上面的页面打开后,输入新的密码后提交,之后您即可使用新的密码登录论坛了.您可以在用户控制面板中随时修改您的密码.");
                    body.Append("<br /><br />本请求提交者的 IP 为 ");
                    body.Append(DNTRequest.GetIP());
                    body.Append("<br /><br /><br /><br />");
                    body.Append("<br />此致 <br /><br />");
                    body.Append(config.Forumtitle);
                    body.Append(" 管理团队.");
                    body.Append("<br />");
                    body.Append(GetForumPath());
                    body.Append("<br /><br />");

                    Emails.DiscuzSmtpMailToUser(DNTRequest.GetString("email"), title, body.ToString());

                    SetUrl(forumpath);
                    SetMetaRefresh(5);
                    SetShowBackLink(false);
                    AddMsgLine("取回密码的方法已经通过 Email 发送到您的信箱中,<br />请在 3 天之内到论坛修改您的密码.");
                }
                else
                {
                    AddErrLine("用户名,Email 地址或安全提问不匹配,请返回修改.");
                }
            }
        }

        private string GetForumPath()
        {
            return this.Context.Request.Url.ToString().ToLower().Substring(0, this.Context.Request.Url.ToString().ToLower().IndexOf("/aspx/"));
        }
    }
}