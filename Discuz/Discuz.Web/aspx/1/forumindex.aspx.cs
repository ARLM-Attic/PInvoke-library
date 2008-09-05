using System;
using System.Data;
using System.Web;
using System.Xml;
using Discuz.Common;
using Discuz.Forum;

using Discuz.Config;
using Discuz.Web.UI;
using Discuz.Entity;

#if NET1
#else
using Discuz.Common.Generic;
#endif

namespace Discuz.Web
{
	/// <summary>
	/// 论坛首页
	/// </summary>
	public class forumindex : PageBase
    {
        #region 页面变量
#if NET1
		public IndexPageForumInfoCollection forumlist;
        public OnlineUserInfoCollection onlineuserlist = new OnlineUserInfoCollection();
        public PrivateMessageInfoCollection pmlist = new PrivateMessageInfoCollection();
#else
        /// <summary>
        /// 论坛版块列表
        /// </summary>
        public Discuz.Common.Generic.List<IndexPageForumInfo> forumlist;//= new Discuz.Common.Generic.List<IndexPageForumInfo>();
        /// <summary>
        /// 在线用户列表
        /// </summary>
        public Discuz.Common.Generic.List<OnlineUserInfo> onlineuserlist;// = new Discuz.Common.Generic.List<OnlineUserInfo>();
        /// <summary>
        /// 当前登录的用户短消息列表
        /// </summary>
        public Discuz.Common.Generic.List<PrivateMessageInfo> pmlist;//= new Discuz.Common.Generic.List<PrivateMessageInfo>();
#endif
        /// <summary>
        /// 当前用户最后访问时间
        /// </summary>
		public string lastvisit = "未知";
        /// <summary>
        /// 友情链接列表
        /// </summary>
		public DataTable forumlinklist;
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
        ///  Silverlight广告
        /// </summary>
        public string mediaad;
        /// <summary>
        /// 分类间广告
        /// </summary>
        public string inforumad;
        /// <summary>
        /// 公告数量
        /// </summary>
		public int announcementcount;
        /// <summary>
        /// 在线图例列表
        /// </summary>
		public string onlineiconlist = "";
        /// <summary>
        /// 当前登录用户的简要信息
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
        /// 昨日帖数
        /// </summary>
        public int yesterdayposts;
        /// <summary>
        /// 最高日帖数
        /// </summary>
        public int highestposts;
        /// <summary>
        /// 最高发帖日
        /// </summary>
        public string highestpostsdate;
        /// <summary>
        /// 友情链接数
        /// </summary>
		public int forumlinkcount;
        /// <summary>
        /// 最新注册的用户名
        /// </summary>
		public string lastusername;
        /// <summary>
        /// 最新注册的用户Id
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
        /// 是否已经拥有个人空间
        /// </summary>
		public bool isactivespace; 
        /// <summary>
        /// 是否允许申请个人空间
        /// </summary>
		public bool isallowapply;       
        /// <summary>
        /// 可用的扩展积分显示名称
        /// </summary>
		public string[] score;
        /// <summary>
        /// 弹出导航菜单的HTML代码
        /// </summary>
		public string navhomemenu = "";
        /// <summary>
        /// 是否显示短消息
        /// </summary>
        public bool showpmhint = false;
        /// <summary>
        /// 标签列表
        /// </summary>
        public TagInfo[] taglist;

        #endregion

        protected override void ShowPage()
		{
			pagetitle = "首页";

            score = Scoresets.GetValidScoreName();

            int toframe = DNTRequest.GetInt("f", 1);
            if (toframe == 0)
            {
                ForumUtils.WriteCookie("isframe", 1);
            }
            else
            {
                toframe = Utils.StrToInt(ForumUtils.GetCookie("isframe"), -1);
                if (toframe == -1)
                {
                    toframe = config.Isframeshow;
                }
            }

			if (toframe == 2)
			{
				HttpContext.Current.Response.Redirect(BaseConfigs.GetForumPath + "frame.aspx");
				HttpContext.Current.Response.End();
                return;
			}



			if (config.Rssstatus == 1)
			{
				AddLinkRss("tools/rss.aspx", "最新主题");
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
                if (userinfo == null)
                {
                    userid = -1;
                    ForumUtils.ClearUserCookie("dnt");
                }
                else
                {
				    if (userinfo.Newpm == 0)
				    {
					    base.newpmcount = 0;
				    }
				    lastvisit = userinfo.Lastvisit.ToString();
                    showpmhint = Convert.ToInt32(userinfo.Newsletter) > 4;
                }
			}

			navhomemenu = Caches.GetForumListMenuDivCache(usergroupid, userid, config.Extname);

			totaltopic = 0;
			totalpost = 0;
			todayposts = 0;

			forumlist = Forums.GetForumIndexCollection(config.Hideprivate, usergroupid, config.Moddisplay, out totaltopic, out totalpost, out todayposts);
			forumlinklist = Caches.GetForumLinkList();
			forumlinkcount = forumlinklist.Rows.Count;

			//个人空间控制
            if (config.Enablespace == 1)
            {
                GetSpacePerm();
            }


			// 获得统计信息
			totalusers = Utils.StrToInt(Statistics.GetStatisticsRowItem("totalusers"), 0);
			lastusername = Statistics.GetStatisticsRowItem("lastusername");
			lastuserid = Utils.StrToInt(Statistics.GetStatisticsRowItem("lastuserid"), 0);
            yesterdayposts = Utils.StrToInt(Statistics.GetStatisticsRowItem("yesterdayposts"), 0);
            highestposts = Utils.StrToInt(Statistics.GetStatisticsRowItem("highestposts"), 0);
            highestpostsdate = Statistics.GetStatisticsRowItem("highestpostsdate").ToString().Trim();
            if (todayposts > highestposts)
            {
                highestposts = todayposts;
                highestpostsdate = DateTime.Now.ToString("yyyy-M-d");
            }
            totalonline = onlineusercount;
			showforumonline = false;
			onlineiconlist = Caches.GetOnlineGroupIconList();
			if (totalonline < config.Maxonlinelist || DNTRequest.GetString("showonline") == "yes")
			{
				showforumonline = true;
				//获得在线用户列表和图标
                onlineuserlist = OnlineUsers.GetOnlineUserCollection(out totalonline, out totalonlineguest, out totalonlineuser, out totalonlineinvisibleuser);
			}

			if (DNTRequest.GetString("showonline") == "no")
			{
				showforumonline = false;
			}

			highestonlineusercount = Statistics.GetStatisticsRowItem("highestonlineusercount");
			highestonlineusertime = DateTime.Parse(Statistics.GetStatisticsRowItem("highestonlineusertime")).ToString("yyyy-MM-dd HH:mm");
			// 得到公告
			announcementlist = Announcements.GetSimplifiedAnnouncementList(nowdatetime, "2999-01-01 00:00:00");
			announcementcount = 0;
			if (announcementlist != null)
			{
				announcementcount = announcementlist.Rows.Count;
			}

			///得到广告列表
			///头部
			headerad = Advertisements.GetOneHeaderAd("indexad",0);
			footerad = Advertisements.GetOneFooterAd("indexad",0);
#if NET1
            IndexPageForumInfoCollection topforum = new IndexPageForumInfoCollection();
#else
            List<IndexPageForumInfo> topforum = new List<IndexPageForumInfo>();
#endif

            foreach (IndexPageForumInfo f in forumlist)
            {
                if (f.Layer == 0)
                {
                    topforum.Add(f);
                }
            }

            if (config.Enabletag == 1)
            {
                taglist = ForumTags.GetCachedHotForumTags(config.Hottagcount);
            }
            else
            { 
                taglist = new TagInfo[0];
            }

            inforumad = Advertisements.GetInForumAd("indexad", 0, topforum, templatepath);

			pagewordad = Advertisements.GetPageWordAd("indexad", 0);
			doublead = Advertisements.GetDoubleAd("indexad", 0);
			floatad = Advertisements.GetFloatAd("indexad", 0);
            mediaad = Advertisements.GetMediaAd(templatepath, "indexad", 0);
		}

        private void GetSpacePerm()
        {
            isactivespace = false;
            isallowapply = true;
            if (userinfo.Spaceid > 0)
            {
                isactivespace = true;
            }
            else
            {
                if (userinfo.Spaceid < 0)
                {
                    isallowapply = false;
                }
                else
                {
                    SpaceActiveConfigInfo spaceconfiginfo = SpaceActiveConfigs.GetConfig();
                    if (spaceconfiginfo.AllowPostcount == "1" || spaceconfiginfo.AllowDigestcount == "1" || spaceconfiginfo.AllowScore == "1" || spaceconfiginfo.AllowUsergroups == "1")
                    {
                        if (spaceconfiginfo.AllowPostcount == "1")
                            isallowapply = isallowapply && (Convert.ToInt32(spaceconfiginfo.Postcount) <= userinfo.Posts);
                        if (spaceconfiginfo.AllowDigestcount == "1")
                            isallowapply = isallowapply && (Convert.ToInt32(spaceconfiginfo.Digestcount) <= userinfo.Digestposts);
                        if (spaceconfiginfo.AllowScore == "1")
                            isallowapply = isallowapply && (Convert.ToInt32(spaceconfiginfo.Score) <= userinfo.Credits);
                        if (spaceconfiginfo.AllowUsergroups == "1")
                            isallowapply = isallowapply && (("," + spaceconfiginfo.Usergroups + ",").IndexOf("," + userinfo.Groupid + ",") != -1);
                    }
                    else
                        isallowapply = false;
                }
            }
        }
	}
}
