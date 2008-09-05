using System;
using System.Data;
using Discuz.Common;
using Discuz.Entity;
using Discuz.Forum;
using System.Data.Common;
using Discuz.Config;
using Discuz.Web.UI;

namespace Discuz.Web
{
    /// <summary>
    /// 用户管理页面
    /// </summary>
    public class useradmin : PageBase 
    {
        #region 页面变量

        /// <summary>
        /// 操作标题
        /// </summary>
        public string operationtitle;

        /// <summary>
        /// 被操作用户Id
        /// </summary>
        public int operateduid;

        /// <summary>
        /// 被操作用户名
        /// </summary>
        public string operatedusername;

        /// <summary>
        /// 禁止用户类型
        /// </summary>
        public int bantype;

        /// <summary>
        /// 操作类型
        /// </summary>
        public string action;

        #endregion

        private AdminGroupInfo admininfo;
        private ShortUserInfo operateduser;

        protected override void ShowPage()
        {
            pagetitle = "用户管理";
            operationtitle = "操作提示";

            if (userid == -1)
            {
                AddErrLine("请先登录");
                return;
            }
            action = DNTRequest.GetQueryString("action");
            if (ForumUtils.IsCrossSitePost(DNTRequest.GetUrlReferrer(), DNTRequest.GetHost()) || action == "")
            {
                AddErrLine("非法提交");
                return;
            }
            if (action == "")
            {
                AddErrLine("操作类型参数为空");
                return;
            }
            // 如果拥有管理组身份
            admininfo = AdminGroups.GetAdminGroupInfo(useradminid);
            // 如果所属管理组不存在
            if (admininfo == null)
            {
                AddErrLine("你没有管理权限");
                return;
            }
            operateduid = DNTRequest.GetInt("uid", -1);
            if (operateduid == -1)
            {
                AddErrLine("没有选择要操作的用户");
                return;
            }
            operateduser = Users.GetShortUserInfo(operateduid);
            if (operateduser == null)
            {
                AddErrLine("选择的用户不存在");
                return;
            }
            if (operateduser.Adminid > 0)
            {
                AddErrLine("无法对拥有管理权限的用户进行操作, 请管理员登录后台进行操作");
                return;
            }
            operatedusername = operateduser.Username;

            if (!ispost)
            {
                Utils.WriteCookie("reurl", DNTRequest.GetUrlReferrer());
                switch (action)
                {
                    case "banuser":
                        operationtitle = "禁止用户";
                        switch (operateduser.Groupid)
                        {
                            case 4:
                                bantype = 1;
                                break;
                            case 5:
                                bantype = 2;
                                break;
                            case 6:
                                bantype = 3;
                                break;
                            default:
                                bantype = 0;
                                break;
                        }
                        if (!ValidateBanUser())
                        {
                            AddErrLine("您没有禁止用户的权限");
                            return;
                        }
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (action)
                {
                    case "banuser":
                        operationtitle = "禁止用户";
                        DoBanUserOperation();
                        break;
                    default:
                        break;
                }
            }
        }

        private void DoBanUserOperation()
        {
            string actions = string.Empty;
            string reason = DNTRequest.GetString("reason");

            if (reason == string.Empty)
            {
                AddErrLine("请填写操作原因");
                return;
            }
            switch (DNTRequest.GetInt("bantype", -1))
            {
                case 0:
                    //正常状态
                    Users.UpdateUserGroup(operateduid, UserCredits.GetCreditsUserGroupID(operateduser.Credits).Groupid);
                    actions = "解除禁止用户";
                    AddMsgLine("已根据积分将用户归组, 将返回之前页面");
                    break;
                case 1:
                    //禁止发言
                    Users.UpdateUserGroup(operateduid, 4);
                    actions = "禁止用户发言";
                    AddMsgLine("已成功禁止所选用户发言, 将返回之前页面");
                    break;
                case 2:
                    //禁止发言
                    Users.UpdateUserGroup(operateduid, 5);
                    actions = "禁止用户访问";
                    AddMsgLine("已成功禁止所选用户访问, 将返回之前页面");
                    break;
                default:
                    AddErrLine("错误的禁止类型");
                    return;
            }

            AdminModeratorLogs.InsertLog(userid.ToString(), username, usergroupid.ToString(), usergroupinfo.Grouptitle,
                                         DNTRequest.GetIP(),
                                         DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "0", string.Empty, "0",
                                         string.Empty,
                                         actions, reason);

            RedirectURL();
        }

        private void RedirectURL()
        {
            SetShowBackLink(false);
            SetUrl(Utils.UrlDecode(ForumUtils.GetReUrl()));
            SetMetaRefresh();
        }

        private bool ValidateBanUser()
        {
            if (admininfo.Allowbanuser == 1)
            {
                return true;
            }
            return false;
        }
    }
}