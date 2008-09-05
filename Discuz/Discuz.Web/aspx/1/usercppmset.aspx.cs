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
    /// 短消息基本设置页面
    /// </summary>
    public class usercppmset : PageBase
    {
        /// <summary>
        /// 当前用户信息
        /// </summary>
        public UserInfo user = new UserInfo();

        /// <summary>
        /// 短消息接收设置
        /// </summary>
        public int receivepmsetting;

        protected override void ShowPage()
        {
            pagetitle = "用户控制面板";

            if (userid == -1)
            {
                AddErrLine("你尚未登录");
                return;
            }
            user = Users.GetUserInfo(userid);
            receivepmsetting = (int) user.Newsletter;

            if (DNTRequest.IsPost())
            {
                user.Pmsound = DNTRequest.GetInt("pmsound", 0);


                receivepmsetting = 1;
                foreach (string rpms in DNTRequest.GetString("receivesetting").Split(','))
                {
                    if (rpms != string.Empty)
                    {
                        int tmp = int.Parse(rpms);
                        receivepmsetting = receivepmsetting | tmp;
                    }
                }
                user.Newsletter = (ReceivePMSettingType) receivepmsetting;

                Users.UpdateUserPMSetting(user);

                ForumUtils.WriteCookie("pmsound", user.Pmsound.ToString());

                SetUrl("usercppmset.aspx");
                SetMetaRefresh();
                SetShowBackLink(true);
                AddMsgLine("短消息设置已成功更新");
            }
        }
    }
}