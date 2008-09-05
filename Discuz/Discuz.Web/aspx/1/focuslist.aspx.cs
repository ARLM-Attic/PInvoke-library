using System;
using System.Data;
using Discuz.Common;
using Discuz.Forum;
using Discuz.Web.UI;
using Discuz.Entity;

namespace Discuz.Web
{
	/// <summary>
	/// 分栏模式首页
	/// </summary>
	public class focuslist : PageBase
    {
        #region 页面变量
        /// <summary>
        /// 精华主题列表
        /// </summary>
		public DataTable digesttopiclist;
        /// <summary>
        /// 热门主题列表
        /// </summary>
		public DataTable hottopiclist;
        /// <summary>
        /// 当前登录用户上次访问时间
        /// </summary>
		public string lastvisit = "";
        /// <summary>
        /// 在线用户列表
        /// </summary>
		public DataTable onlineuserlist;
        /// <summary>
        /// 获取友情链接列表
        /// </summary>
		public DataTable forumlinklist;

#if NET1
        public PrivateMessageInfoCollection pmlist = new PrivateMessageInfoCollection();
#else
        /// <summary>
        /// 当前登录用户的短消息列表
        /// </summary>
        public Discuz.Common.Generic.List<PrivateMessageInfo> pmlist ;//= new Discuz.Common.Generic.List<PrivateMessageInfo>();
#endif		
        /// <summary>
        /// 公告列表
        /// </summary>
        public DataTable announcementlist;
        /// <summary>
        /// 页内文字广告
        /// </summary>
		public string[] pagewordad;
        /// <summary>
        /// 对联广告
        /// </summary>
		public string doublead;
        /// <summary>
        /// 浮动广告
        /// </summary>
		public string floatad;
        /// <summary>
        /// 公告数量
        /// </summary>
		public int announcementcount;
        /// <summary>
        /// 在线图例列表
        /// </summary>
		public string onlineiconlist = "";
        /// <summary>
        /// 当前登录用户简要信息
        /// </summary>
		public ShortUserInfo userinfo;
        /// <summary>
        /// 总主题数
        /// </summary>
		public int totaltopic;
        /// <summary>
        /// 总帖子数
        /// </summary>
		public int totalpost;
        /// <summary>
        /// 总用户数
        /// </summary>
		public int totalusers;
        /// <summary>
        /// 今日帖数
        /// </summary>
		public int todayposts;
        /// <summary>
        /// 友情链接数
        /// </summary>
		public int forumlinkcount;
        /// <summary>
        /// 最后注册的用户的用户名
        /// </summary>
		public string lastusername;
        /// <summary>
        /// 最后注册的用户的用户Id
        /// </summary>
		public int lastuserid;
        /// <summary>
        /// 总在线用户数
        /// </summary>
		public int totalonline;
        /// <summary>
        /// 总在线注册用户数
        /// </summary>
		public int totalonlineuser;
        /// <summary>
        /// 总在线游客数
        /// </summary>
		public int totalonlineguest;
        /// <summary>
        /// 总在线隐身用户数
        /// </summary>
		public int totalonlineinvisibleuser;
        /// <summary>
        /// 最高在线用户数
        /// </summary>
		public string highestonlineusercount;
        /// <summary>
        /// 最高在线用户数发生时间
        /// </summary>
		public string highestonlineusertime;
        /// <summary>
        /// 是否显示在线列表
        /// </summary>
		public bool showforumonline;
        /// <summary>
        /// 可用的扩展积分显示名称
        /// </summary>
		public string[] score;
        /// <summary>
        /// 是否显示短消息提示
        /// </summary>
        public bool showpmhint = false;
        #endregion

        protected override void ShowPage()
		{
			pagetitle = "首页";

            score = Scoresets.GetValidScoreName();

			if (config.Rssstatus == 1)
			{
				AddLinkRss("tools/rss.aspx", string.Format("{0} 最新主题", config.Forumtitle));
			}

			OnlineUsers.UpdateAction(olid, UserAction.IndexShow.ActionID, 0, config.Onlinetimeout);

			if (newpmcount > 0)
			{
				pmlist = PrivateMessages.GetPrivateMessageCollectionForIndex(userid,5,1,1);
			}

			userinfo = new ShortUserInfo();
			if (userid != -1)
			{
				userinfo = Users.GetShortUserInfo(userid);
				if (userinfo.Newpm == 0)
				{
					base.newpmcount = 0;
				}
				lastvisit = userinfo.Lastvisit.ToString();
                showpmhint = Convert.ToInt32(userinfo.Newsletter) > 4;
			}

			Statistics.GetPostCountFromForum(0,out totaltopic,out totalpost,out todayposts);
			digesttopiclist = Focuses.GetDigestTopicList(16);
            hottopiclist = Focuses.GetHotTopicList(16, 30);
			forumlinklist = Caches.GetForumLinkList();
			forumlinkcount = forumlinklist.Rows.Count;

			// 获得统计信息
			totalusers = Utils.StrToInt(Statistics.GetStatisticsRowItem("totalusers"), 0);
			lastusername = Statistics.GetStatisticsRowItem("lastusername");
			lastuserid = Utils.StrToInt(Statistics.GetStatisticsRowItem("lastuserid"), 0);

			totalonline = onlineusercount;

			showforumonline = false;
			if (totalonline < config.Maxonlinelist || DNTRequest.GetString("showonline") == "yes")
			{
				showforumonline = true;
				onlineuserlist = OnlineUsers.GetOnlineUserList(onlineusercount, out totalonlineguest, out totalonlineuser, out totalonlineinvisibleuser);
				onlineiconlist = Caches.GetOnlineGroupIconList();
			}

			if (DNTRequest.GetString("showonline") == "no")
			{
				showforumonline = false;
			}

			highestonlineusercount = Statistics.GetStatisticsRowItem("highestonlineusercount");
			highestonlineusertime = Statistics.GetStatisticsRowItem("highestonlineusertime");

			// 得到公告
			announcementlist = Announcements.GetSimplifiedAnnouncementList(nowdatetime, "2999-01-01 00:00:00");
			announcementcount = 0;
			if (announcementlist != null)
			{
				announcementcount = announcementlist.Rows.Count;
			}

			///得到广告列表
			headerad = Advertisements.GetOneHeaderAd("indexad",0);
			footerad = Advertisements.GetOneFooterAd("indexad",0);
			pagewordad = Advertisements.GetPageWordAd("indexad",0);
			doublead = Advertisements.GetDoubleAd("indexad",0);
			floatad = Advertisements.GetFloatAd("indexad",0);
		}
	}
}
