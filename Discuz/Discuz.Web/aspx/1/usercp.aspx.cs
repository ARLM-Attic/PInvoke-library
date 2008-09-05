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
	/// 用户中心
	/// </summary>
	public class usercp : PageBase
    {
        #region 页面变量
        /// <summary>
        /// 当前登录的用户信息
        /// </summary>
		public UserInfo user = new UserInfo();
        /// <summary>
        /// 可用的积分名称
        /// </summary>
		public string[] score;
        /// <summary>
        /// 头像
        /// </summary>
		public string avatar;
        /// <summary>
        /// 头像地址
        /// </summary>
		public string avatarurl;
        /// <summary>
        /// 头像类型
        /// </summary>
		public int avatartype;
        /// <summary>
        /// 头像宽度
        /// </summary>
		public int avatarwidth;
        /// <summary>
        /// 头像高度
        /// </summary>
		public int avatarheight;
        #endregion

        protected override void ShowPage()
		{		
			pagetitle = "用户控制面板";
			
			if (userid == -1)
			{
				AddErrLine("你尚未登录");
				
				return;
			}
			user = Users.GetUserInfo(userid);

			if (!IsErr())
			{
                score = Scoresets.GetValidScoreName();
				avatar = user.Avatar;
				avatarurl = "";
				avatartype = 1;
				avatarwidth = 0;
				avatarheight = 0;
				if (Utils.CutString(avatar, 0, 15).ToLower().Equals(@"avatars\common\"))
				{
					avatartype = 0;
				}
				else if (Utils.CutString(avatar, 0, 7).ToLower().Equals("http://"))
				{
					avatarurl = avatar;
					avatartype = 2;
					avatarwidth = user.Avatarwidth;
					avatarheight = user.Avatarheight;
				}

    		}

		}
	}
}
