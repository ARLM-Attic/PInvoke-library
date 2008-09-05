using System;
using System.Data;
using Discuz.Common;

#if NET1
#else
using Discuz.Common.Generic;
#endif

using Discuz.Forum;
using Discuz.Entity;
using Discuz.Aggregation;
using Discuz.Plugin.Album;

namespace Discuz.Web
{
    /// <summary>
    /// 聚合首页
    /// </summary>
    public class website : PageBase
    {
        /// <summary>
        /// 论坛推荐主题
        /// </summary>
        public PostInfo[] postlist;
        /// <summary>
        /// 论坛聚合主题
        /// </summary>
        public DataTable topiclist;
        /// <summary>
        /// 用户聚合数据
        /// </summary>
        public DataTable userlist;
        /// <summary>
        /// 论坛新帖
        /// </summary>
        public DataTable newtopiclist;
        /// <summary>
        /// 论坛热帖
        /// </summary>
        public DataTable hottopiclist;
        /// <summary>
        /// 论坛热门版块
        /// </summary>
        public DataTable hotforumlist;
        /// <summary>
        /// 聚合空间数据
        /// </summary>
        public SpaceConfigInfoExt[] spaceconfigs;
        /// <summary>
        /// 聚合相册数据
        /// </summary>
        public AlbumInfo[] albuminfos;
        /// <summary>
        /// 聚合日志数据
        /// </summary>
        public SpaceShortPostInfo[] spacepostlist;
        /// <summary>
        /// 最近更新的空间
        /// </summary>
        public DataTable recentupdatespaceList;
        /// <summary>
        /// 友情链接列表
        /// </summary>
        public DataTable forumlinklist;
        /// <summary>
        /// 友情链接数量
        /// </summary>
        public int forumlinkcount;
        /// <summary>
        /// 公告数量
        /// </summary>
        public int announcementcount;
        /// <summary>
        /// 公告列表
        /// </summary>
        public DataTable announcementlist;
        /// <summary>
        /// 图片轮显数据
        /// </summary>
        public string rotatepicdata = "";
        /// <summary>
        /// 聚合图片信息
        /// </summary>
        public PhotoAggregationInfo photoconfig = AggregationFacade.PhotoAggregation.GetPhotoAggregationInfo();

        public string spaceurlnopage = "";

        public string albumurlnopage = "";

#if NET1
        /// <summary>
        /// 推荐相册列表
        /// </summary>
        public AlbumInfoCollection recommendalbumlist;
        /// <summary>
        /// 相册分类列表
        /// </summary>
        public AlbumCategoryInfoCollection albumcategorylist;
        /// <summary>
        /// 图片列表
        /// </summary>
        public PhotoInfoCollection photolist;
         /// <summary>
        /// 焦点相册列表
        /// </summary>
        public AlbumInfoCollection albumlist;
#else
        /// <summary>
        /// 推荐相册列表
        /// </summary>
        public List<AlbumInfo> recommendalbumlist;
        /// <summary>
        /// 相册分类列表
        /// </summary>
        public Discuz.Common.Generic.List<AlbumCategoryInfo> albumcategorylist;
        /// <summary>
        /// 图片列表
        /// </summary>
        public Discuz.Common.Generic.List<PhotoInfo> photolist;
        /// <summary>
        /// 焦点相册列表
        /// </summary>
        public Discuz.Common.Generic.List<AlbumInfo> albumlist;

#endif

        public ForumAggregationData forumagg = Discuz.Aggregation.AggregationFacade.ForumAggregation;

        public AlbumAggregationData albumagg = Discuz.Aggregation.AggregationFacade.AlbumAggregation;

        public SpaceAggregationData spaceagg = Discuz.Aggregation.AggregationFacade.SpaceAggregation;

        public GoodsAggregationData goodsagg = Discuz.Aggregation.AggregationFacade.GoodsAggregation;

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
        /// 推荐的版块id列表
        /// </summary>
        public int[] forumidarray;
        /// <summary>
        /// 最新空间评论列表
        /// </summary>
        public DataTable topspacecomments;
        /// <summary>
        /// 标签列表
        /// </summary>
        public TagInfo[] taglist;

        public GoodsinfoCollection goodscoll = new GoodsinfoCollection();

        /// <summary>
        /// 分类间广告
        /// </summary>
        public string inforumad;
        /// <summary>
        /// 对联广告
        /// </summary>
        public string doublead;
        /// <summary>
        /// 浮动广告
        /// </summary>
        public string floatad;

        protected override void ShowPage()
        {
            pagetitle = "首页";

            postlist = AggregationFacade.ForumAggregation.GetPostListFromFile("Website");

            topiclist = AggregationFacade.ForumAggregation.GetForumTopicList();

            spaceconfigs = AggregationFacade.SpaceAggregation.GetSpaceListFromFile("Website");

            spacepostlist = AggregationFacade.SpaceAggregation.GetSpacePostList("Website");


            //推荐相册
            recommendalbumlist = AggregationFacade.AlbumAggregation.GetRecommandAlbumList("Website");

            //recentupdatespaceList =
            //    AggregationFacade.SpaceAggregation.GetRecentUpdateSpaceList(AggregationConfig.GetConfig().RecentUpdateSpaceAggregationListCount);


            // 得到公告
            announcementlist = Announcements.GetSimplifiedAnnouncementList(nowdatetime, "2999-01-01 00:00:00");
            announcementcount = 0;
            if (announcementlist != null)
            {
                announcementcount = announcementlist.Rows.Count;
            }

            // 友情链接
            forumlinklist = Caches.GetForumLinkList();

            rotatepicdata = AggregationFacade.BaseAggregation.GetRotatePicData();

            Forums.GetForumIndexCollection(config.Hideprivate, usergroupid, config.Moddisplay, out totaltopic, out totalpost, out todayposts);

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
            OnlineUsers.GetOnlineUserCollection(out totalonline, out totalonlineguest, out totalonlineuser, out totalonlineinvisibleuser);

            highestonlineusercount = Statistics.GetStatisticsRowItem("highestonlineusercount");
            highestonlineusertime = Statistics.GetStatisticsRowItem("highestonlineusertime");

            //相册分类
            if (AlbumPluginProvider.GetInstance() != null)
            {
                albumcategorylist = AlbumPluginProvider.GetInstance().GetAlbumCategory();
            }

            if (AggregationFacade.SpaceAggregation.GetSpaceTopComments() != null)
            {
                topspacecomments = AggregationFacade.SpaceAggregation.GetSpaceTopComments();
            }

            forumidarray = AggregationFacade.ForumAggregation.GetRecommendForumID();

            forumlinklist = Caches.GetForumLinkList();
            forumlinkcount = forumlinklist.Rows.Count;

            if (config.Enabletag == 1)
            {
                taglist = ForumTags.GetCachedHotForumTags(config.Hottagcount);
            }
            else
            {
                taglist = new TagInfo[0];
            }

            doublead = Advertisements.GetDoubleAd("indexad", 0);
            floatad = Advertisements.GetFloatAd("indexad", 0);

        }
    }
}