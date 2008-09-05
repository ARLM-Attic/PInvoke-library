using System;
using System.Data;
using System.Data.SqlClient;
using Discuz.Common;
using Discuz.Forum;
using Discuz.Web.UI;
using Discuz.Entity;
using Discuz.Config;

namespace Discuz.Web
{
    /// <summary>
    /// 重置密码页面
    /// </summary>
    public class usercpnewpassword : PageBase
    {
        /// <summary>
        /// 当前用户信息
        /// </summary>
        public UserInfo user = new UserInfo();

        protected override void ShowPage()
        {
            pagetitle = "用户控制面板";

            if (userid == -1)
            {
                AddErrLine("你尚未登录");
                return;
            }
            user = Users.GetUserInfo(userid);

            if (DNTRequest.IsPost())
            {
                string oldpassword = DNTRequest.GetString("oldpassword");
                string newpassword = DNTRequest.GetString("newpassword");
                string newpassword2 = DNTRequest.GetString("newpassword2");

                if (Users.CheckPassword(userid, oldpassword, true) == -1)
                {
                    AddErrLine("你的原密码错误");
                }
                if (newpassword != newpassword2)
                {
                    AddErrLine("新密码两次输入不一致");
                }

                if (newpassword.Equals(""))
                {
                    newpassword = oldpassword;
                }

                if (newpassword.Length < 6)
                {
                    AddErrLine("密码不得少于6个字符");
                }

                if (IsErr())
                {
                    return;
                }
                else
                {
                    //判断是否需要修改安全提问
                    Users.UpdateUserPassword(userid, newpassword);
                    if (DNTRequest.GetString("changesecques") != "")
                    {
                        Users.UpdateUserSecques(userid, DNTRequest.GetInt("question", 0), DNTRequest.GetString("answer"));
                    }
                    ForumUtils.WriteCookie("password",
                                           ForumUtils.SetCookiePassword(Utils.MD5(newpassword), config.Passwordkey));
                    OnlineUsers.UpdatePassword(olid, Utils.MD5(newpassword));

                    SetUrl("usercpnewpassword.aspx");
                    SetMetaRefresh();
                    SetShowBackLink(true);
                    AddMsgLine("修改密码完毕, 同时已经更新了您的登录信息");
                }
            }
        }
    }
}