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
	/// 发件箱页面
	/// </summary>
	public class usercpsentbox : PageBase
    {
        #region 页面变量
#if NET1        
        public PrivateMessageInfoCollection pmlist = new PrivateMessageInfoCollection();
#else
        /// <summary>
        /// 短消息列表
        /// </summary>
        public Discuz.Common.Generic.List<PrivateMessageInfo> pmlist = new Discuz.Common.Generic.List<PrivateMessageInfo>();
#endif	
        /// <summary>
        /// 当前页码
        /// </summary>
		public int pageid;
        /// <summary>
        /// 短消息数
        /// </summary>
		public int pmcount;
        /// <summary>
        /// 分页页数
        /// </summary>
		public int pagecount;
        /// <summary>
        /// 分页页码链接
        /// </summary>
		public string pagenumbers;
        /// <summary>
        /// 用户最大短消息数
        /// </summary>
		public int maxmsg;
        /// <summary>
        /// 已使用的短消息数
        /// </summary>
		public int usedmsgcount;
        /// <summary>
        /// 已使用的短消息条宽度
        /// </summary>
		public int usedmsgbarwidth;
        /// <summary>
        /// 未使用的短消息条宽度
        /// </summary>
		public int unusedmsgbarwidth;
        /// <summary>
        /// 当前用户信息
        /// </summary>
		public UserInfo user = new UserInfo();
        #endregion

        protected override void ShowPage()
		{
			pagetitle = "短消息发件箱";
			
			if (userid == -1)
			{
				AddErrLine("你尚未登录");
				return;
			}
			user = Users.GetUserInfo(userid);

			if (DNTRequest.IsPost())
			{
				if (ForumUtils.IsCrossSitePost())
				{
					AddErrLine("您的请求来路不正确，无法提交。如果您安装了某种默认屏蔽来路信息的个人防火墙软件(如 Norton Internet Security)，请设置其不要禁止来路信息后再试。");
					return;
				}

				string[] pmitemid = Utils.SplitString(DNTRequest.GetFormString("pmitemid"), ",");

				int retval = PrivateMessages.DeletePrivateMessage(userid, pmitemid);

				if (retval == -1)
				{
					AddErrLine("参数无效<br />");
					return;
				}

				SetShowBackLink(false);
				AddMsgLine("删除完毕");
			}
			else
			{
				BindItems();
			}
		}

		/// <summary>
		/// 加载用户当前请求页数的短消息列表并装入DataTable中
		/// </summary>
		private void BindItems()
		{
			//得到当前用户请求的页数
			pageid = DNTRequest.GetInt("page", 1);
			//获取主题总数
			pmcount = PrivateMessages.GetPrivateMessageCount(userid,1);
			//获取总页数
			pagecount = pmcount%16==0 ? pmcount/16 : pmcount/16 + 1;
			if (pagecount == 0)
			{
				pagecount = 1;
			}
			//修正请求页数中可能的错误
			if (pageid < 1)
			{
				pageid = 1;
			}
			if (pageid > pagecount)
			{
				pageid = pagecount;
			}
	
			//取得用户所有短消息总数，第二个参数就传-1
			usedmsgcount = PrivateMessages.GetPrivateMessageCount(userid, -1);
			maxmsg = usergroupinfo.Maxpmnum;
	
			if (maxmsg <= 0)
			{
				usedmsgbarwidth = 0;
				unusedmsgbarwidth = 0;
			}
			else
			{
				usedmsgbarwidth = usedmsgcount * 100 / maxmsg;
				unusedmsgbarwidth = 100 - usedmsgbarwidth;
			}
	
			pmlist = PrivateMessages.GetPrivateMessageCollection(userid, 1, 16, pageid,2);
			pagenumbers = Utils.GetPageNumbers(pageid, pagecount, "usercpsentbox.aspx", 8);
		}
	}
}

