using System;
using System.Data;
using Discuz.Common;
using Discuz.Forum;
using System.Data.Common;
using Discuz.Config;
using Discuz.Web.UI;

namespace Discuz.Web
{
	/// <summary>
	/// 激活用户页面
	/// </summary>
	public class activationuser : PageBase
	{
		protected override void ShowPage()
		{
			
			pagetitle = "用户帐号激活";

			SetUrl("index.aspx");
			SetMetaRefresh();
			SetShowBackLink(false);
	
			string authstr = Utils.HtmlEncode(DNTRequest.GetString("authstr").Trim()).Replace("'","''");

			if(authstr != null && authstr != "")
			{
                DataTable dt = Users.GetUserIdByAuthStr(authstr);
				if (dt.Rows.Count > 0)
				{
					int uid = Convert.ToInt32(dt.Rows[0][0].ToString());

					//将用户调整到相应的用户组
					if (UserCredits.GetCreditsUserGroupID(0) != null)
					{
						int tmpGroupID = UserCredits.GetCreditsUserGroupID(0).Groupid; //添加注册用户审核机制后需要修改
                        Users.UpdateUserGroup(uid, tmpGroupID);                        
                    }

					//更新激活字段
                    Users.UpdateAuthStr(uid, "", 0);
                   
					AddMsgLine("您当前的帐号已经激活,稍后您将以相应身份返回首页");

					ForumUtils.WriteUserCookie(uid, Utils.StrToInt(DNTRequest.GetString("expires"), -1), config.Passwordkey);
					OnlineUsers.UpdateAction(olid, UserAction.ActivationUser.ActionID, 0, config.Onlinetimeout);
	
				}
				else
				{
					AddMsgLine("您当前的激活链接无效,稍后您将以游客身份返回首页");
					OnlineUsers.DeleteRows(olid);
					ForumUtils.ClearUserCookie();
				}
			}
			else
			{
				AddMsgLine("您当前的激活链接无效,稍后您将以游客身份返回首页");
				OnlineUsers.DeleteRows(olid);
				ForumUtils.ClearUserCookie();
			}
		}

	}
}
