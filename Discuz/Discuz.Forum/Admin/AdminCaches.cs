using System;
using System.Data;
using System.Data.Common;

using Discuz.Common;
using Discuz.Data;
using Discuz.Forum;
using Discuz.Config.Provider;
using Discuz.Config;

namespace Discuz.Forum
{
    /// <summary>
    /// AdminCacheFactory 的摘要说明。
    /// </summary>
    public class AdminCaches
    {
        /// <summary>
        /// 重新设置管理组信息
        ///</summary>
        public static void ReSetAdminGroupList()
        {

            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/AdminGroupList");
        }

        /// <summary>
        /// 重新设置用户组信息
        ///</summary>
        public static void ReSetUserGroupList()
        {

            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/UserGroupList");
        }

        /// <summary>
        /// 重新设置版主信息
        ///</summary>
        public static void ReSetModeratorList()
        {

            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/ModeratorList");
        }

        /// <summary>
        /// 重新设置指定时间内的公告列表
        ///</summary>
        public static void ReSetAnnouncementList()
        {

            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/AnnouncementList");
        }

        /// <summary>
        /// 重新设置第一条公告
        ///</summary>
        public static void ReSetSimplifiedAnnouncementList()
        {

            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/SimplifiedAnnouncementList");
        }

        /// <summary>
        /// 重新设置版块下拉列表
        ///</summary>
        public static void ReSetForumListBoxOptions()
        {

            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/UI/ForumListBoxOptions");
        }

        /// <summary>
        /// 重新设置表情
        ///</summary>
        public static void ReSetSmiliesList()
        {

            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/UI/SmiliesList");

            cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/UI/SmiliesListWithInfo");

        }

        /// <summary>
        /// 重新设置主题图标
        ///</summary>
        public static void ReSetIconsList()
        {

            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/UI/IconsList");
        }

        /// <summary>
        /// 重新用户自定义标签
        ///</summary>
        public static void ReSetCustomEditButtonList()
        {

            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/UI/CustomEditButtonList");

            cache.RemoveObject("/Forum/UI/CustomEditButtonInfo");
        }

        /// <summary>
        /// 重新设置论坛基本设置
        ///</summary>
        public static void ReSetConfig()
        {

            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/Setting");
        }

        /// <summary>
        /// 重新设置论坛积分
        ///</summary>
        public static void ReSetScoreset()
        {

            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/Scoreset");
            cache.RemoveObject("/Forum/ValidScoreName");
            cache.RemoveObject("/Forum/Scoreset/CreditsTax");
            cache.RemoveObject("/Forum/Scoreset/CreditsTrans");
            cache.RemoveObject("/Forum/Scoreset/TransferMinCredits");
            cache.RemoveObject("/Forum/Scoreset/ExchangeMinCredits");
            cache.RemoveObject("/Forum/Scoreset/MaxIncPerThread");
            cache.RemoveObject("/Forum/Scoreset/MaxChargeSpan");
            cache.RemoveObject("/Forum/ValidScoreUnit");
        }

        /// <summary>
        /// 重新设置地址对照表
        ///</summary>
        public static void ReSetSiteUrls()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/Urls");
        }

        /// <summary>
        /// 重新设置论坛统计信息
        ///</summary>
        public static void ReSetStatistics()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/Statistics");
        }


        /// <summary>
        /// 重新设置系统允许的附件类型和大小
        ///</summary>
        public static void ReSetAttachmentTypeArray()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/ForumSetting/AttachmentType");
        }

        /// <summary>
        /// 模板列表的下拉框html
        ///</summary>
        public static void ReSetTemplateListBoxOptionsCache()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/UI/TemplateListBoxOptions");
        }

        /// <summary>
        /// 重新设置在线用户列表图例
        /// </summary>
        public static void ReSetOnlineGroupIconList()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/UI/OnlineIconList");
            cache.RemoveObject("/Forum/OnlineIconTable");

        }

        /// <summary>
        /// 重新设置友情链接列表
        /// </summary>
        public static void ReSetForumLinkList()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/ForumLinkList");
        }


        /// <summary>
        /// 重新设置脏字过滤列表
        /// </summary>
        public static void ReSetBanWordList()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/BanWordList");
        }


        /// <summary>
        /// 论坛列表
        /// </summary>
        public static void ReSetForumList()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/ForumList");
        }


        /// <summary>
        /// 在线用户信息
        /// </summary>
        public static void ReSetOnlineUserTable()
        {
            ;
        }

        /// <summary>
        /// 论坛整体RSS及指定版块RSS
        /// </summary>
        public static void ReSetRss()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/RSS/");
        }


        /// <summary>
        /// 指定版块RSS
        /// </summary>
        /// <param name="fid">版块ＩＤ</param>
        public static void ReSetForumRssXml(int fid)
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/RSS/Forum" + fid.ToString());
        }


        /// <summary>
        /// 论坛整体RSS
        /// </summary>
        public static void ReSetRssXml()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/RSS/Index");
        }


        /// <summary>
        /// 模板id列表
        /// </summary>
        public static void ReSetValidTemplateIDList()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/TemplateIDList");
        }


        /// <summary>
        /// 有效的用户表扩展字段
        /// </summary>
        public static void ReSetValidScoreName()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/ValidScoreName");
        }


        /// <summary>
        /// 重设勋章列表
        /// </summary>
        public static void ReSetMedalsList()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/UI/MedalsList");
        }

        /// <summary>
        /// 重设数据链接串和数据表前缀
        /// </summary>
        public static void ReSetDBlinkAndTablePrefix()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/BaseSetting/Dbconnectstring");
            cache.RemoveObject("/Forum/BaseSetting/TablePrefix");
        }

        /// <summary>
        /// 重设最后的帖子表
        /// </summary>
        public static void ReSetLastPostTableName()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/LastPostTableName");
        }


        /// <summary>
        /// 重设帖子列表
        /// </summary>
        public static void ReSetAllPostTableName()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/PostTableName");

        }

        /// <summary>
        /// 重设广告列表
        /// </summary>
        public static void ReSetAdsList()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/Advertisements");
        }

        /// <summary>
        /// 重新设置用户上一次执行搜索操作的时间
        /// </summary>
        public static void ReSetStatisticsSearchtime()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/StatisticsSearchtime");
        }


        /// <summary>
        /// 重新设置用户在一分钟内搜索的次数
        /// </summary>
        public static void ReSetStatisticsSearchcount()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/StatisticsSearchcount");
        }


        /// <summary>
        /// 重新设置用户头象列表
        /// </summary>
        public static void ReSetCommonAvatarList()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/CommonAvatarList");
        }

        /// <summary>
        /// 重新设置干扰码字符串
        /// </summary>
        public static void ReSetJammer()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/UI/Jammer");
        }

        /// <summary>
        /// 重新设置魔力列表
        /// </summary>
        public static void ReSetMagicList()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/MagicList");
        }

        /// <summary>
        /// 重新设置兑换比率的可交易积分策略
        /// </summary>
        public static void ReSetScorePaySet()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/ScorePaySet");
        }


        /// <summary>
        /// 重新设置当前贴子表相关信息
        /// </summary>
        public static void ReSetPostTableInfo()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/PostTableName");
            cache.RemoveObject("/Forum/LastPostTableName");
        }


        /// <summary>
        /// 重新设置相应的主题列表
        /// </summary>
        /// <param name="fid"></param>
        public static void ReSetTopiclistByFid(string fid)
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/TopicList/" + fid);
        }



        /// <summary>
        /// 重新设置全部版块精华主题列表
        /// </summary>
        /// <param name="count">精华个数</param>
        public static void ReSetDigestTopicList(int count)
        {
            ReSetFocusTopicList(count, -1, 0, TopicTimeType.All, TopicOrderType.ID, true);
        }

        //重新设置指定版块精华主题列表[暂未调用]
        public static void ReSetDigestTopicList(int count, int fid, TopicTimeType timetype, TopicOrderType ordertype)
        {
            ReSetFocusTopicList(count, -1, fid, timetype, ordertype, true);
        }

        /// <summary>
        /// 重新设置全部版块热贴主题列表
        /// </summary>
        /// <param name="count"></param>
        /// <param name="views"></param>
        public static void ReSetHotTopicList(int count, int views)
        {
            ReSetFocusTopicList(count, views, 0, TopicTimeType.All, TopicOrderType.ID, false);
        }

        //重新设置指定版块热贴主题列表[暂未调用]
        public static void ReSetHotTopicList(int count, int views, int fid, TopicTimeType timetype, TopicOrderType ordertype)
        {
            ReSetFocusTopicList(count, views, fid, timetype, ordertype, false);
        }

        /// <summary>
        /// 重新设置最近主题列表
        /// </summary>
        /// <param name="count"></param>
        public static void ReSetRecentTopicList(int count)
        {
            ReSetFocusTopicList(count, -1, 0, TopicTimeType.All, TopicOrderType.ID, false);
        }

        private static void ReSetFocusTopicList(int count, int views, int fid, TopicTimeType timetype, TopicOrderType ordertype, bool isdigest)
        {
            string cacheKey = "/Forum/TopicList-{0}-{1}-{2}-{3}-{4}-{5}";
            cacheKey = string.Format(cacheKey,
                count,
                views,
                fid,
                timetype,
                ordertype,
                isdigest
                );

            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject(cacheKey);
        }



        /// <summary>
        /// 更新所有缓存
        /// </summary>
        public static void ReSetAllCache()
        {
            ReSetAdminGroupList();

            ReSetUserGroupList();

            ReSetModeratorList();

            ReSetAnnouncementList();

            ReSetSimplifiedAnnouncementList();

            ReSetForumListBoxOptions();

            ReSetSmiliesList();

            ReSetIconsList();

            ReSetCustomEditButtonList();

            ReSetConfig();

            ReSetScoreset();

            ReSetSiteUrls();

            ReSetStatistics();

            ReSetAttachmentTypeArray();

            ReSetTemplateListBoxOptionsCache();

            ReSetOnlineGroupIconList();

            ReSetForumLinkList();

            ReSetBanWordList();

            ReSetForumList();

            ReSetOnlineUserTable();

            ReSetRss();

            ReSetRssXml();

            ReSetValidTemplateIDList();

            ReSetValidScoreName();

            ReSetMedalsList();

            ReSetDBlinkAndTablePrefix();

            ReSetAllPostTableName();

            ReSetLastPostTableName();

            ReSetAdsList();
            ReSetStatisticsSearchtime();
            ReSetStatisticsSearchcount();
            ReSetCommonAvatarList();
            ReSetJammer();
            ReSetMagicList();
            ReSetScorePaySet();
            ReSetPostTableInfo();
            ReSetDigestTopicList(16);
            ReSetHotTopicList(16, 30);
            ReSetRecentTopicList(16);

            ResetAlbumCategory();

            EditDntConfig();

            OnlineUsers.InitOnlineList();
        }

        /// <summary>
        /// 重设BaseConfig
        /// </summary>
        /// <returns></returns>
        public static bool EditDntConfig()
        {
            BaseConfigInfo config = null;
            string filename = Discuz.Config.DefaultConfigFileManager.ConfigFilePath;//Utils.GetMapPath("/DNT.config");
            try
            {
                config = (BaseConfigInfo)SerializationHelper.Load(typeof(BaseConfigInfo), filename);
            }
            catch
            {
                config = null;
            }
            try
            {
                if (config != null)
                {
                    BaseConfigProvider.SetInstance(config);
                    return true;
                }
            }
            catch
            {
                ;
            }
            if (config == null)
            {
                try
                {
                    BaseConfigInfoCollection bcc = (BaseConfigInfoCollection)SerializationHelper.Load(typeof(BaseConfigInfoCollection), filename);
                    foreach (BaseConfigInfo bc in bcc)
                    {
                        if (Utils.GetTrueForumPath() == bc.Forumpath)
                        {
                            config = bc;
                            break;
                        }
                    }

                    if (config == null)
                    {
                        foreach (BaseConfigInfo bc in bcc)
                        {
                            if (Utils.GetTrueForumPath().StartsWith(bc.Forumpath))
                            {
                                config = bc;
                                break;
                            }
                        }
                    }

                    if (config != null)
                    {
                        BaseConfigProvider.SetInstance(config);
                        return true;
                    }

                }
                catch
                {
                    ;
                }
            }
            return false;

        }


        public static void ResetAlbumCategory()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Space/AlbumCategory");
        }

        public static void ReSetNavPopupMenu()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/ForumListMenuDiv");
        }
    }
}
