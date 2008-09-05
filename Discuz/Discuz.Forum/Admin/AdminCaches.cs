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
    /// AdminCacheFactory ��ժҪ˵����
    /// </summary>
    public class AdminCaches
    {
        /// <summary>
        /// �������ù�������Ϣ
        ///</summary>
        public static void ReSetAdminGroupList()
        {

            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/AdminGroupList");
        }

        /// <summary>
        /// ���������û�����Ϣ
        ///</summary>
        public static void ReSetUserGroupList()
        {

            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/UserGroupList");
        }

        /// <summary>
        /// �������ð�����Ϣ
        ///</summary>
        public static void ReSetModeratorList()
        {

            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/ModeratorList");
        }

        /// <summary>
        /// ��������ָ��ʱ���ڵĹ����б�
        ///</summary>
        public static void ReSetAnnouncementList()
        {

            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/AnnouncementList");
        }

        /// <summary>
        /// �������õ�һ������
        ///</summary>
        public static void ReSetSimplifiedAnnouncementList()
        {

            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/SimplifiedAnnouncementList");
        }

        /// <summary>
        /// �������ð�������б�
        ///</summary>
        public static void ReSetForumListBoxOptions()
        {

            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/UI/ForumListBoxOptions");
        }

        /// <summary>
        /// �������ñ���
        ///</summary>
        public static void ReSetSmiliesList()
        {

            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/UI/SmiliesList");

            cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/UI/SmiliesListWithInfo");

        }

        /// <summary>
        /// ������������ͼ��
        ///</summary>
        public static void ReSetIconsList()
        {

            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/UI/IconsList");
        }

        /// <summary>
        /// �����û��Զ����ǩ
        ///</summary>
        public static void ReSetCustomEditButtonList()
        {

            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/UI/CustomEditButtonList");

            cache.RemoveObject("/Forum/UI/CustomEditButtonInfo");
        }

        /// <summary>
        /// ����������̳��������
        ///</summary>
        public static void ReSetConfig()
        {

            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/Setting");
        }

        /// <summary>
        /// ����������̳����
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
        /// �������õ�ַ���ձ�
        ///</summary>
        public static void ReSetSiteUrls()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/Urls");
        }

        /// <summary>
        /// ����������̳ͳ����Ϣ
        ///</summary>
        public static void ReSetStatistics()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/Statistics");
        }


        /// <summary>
        /// ��������ϵͳ����ĸ������ͺʹ�С
        ///</summary>
        public static void ReSetAttachmentTypeArray()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/ForumSetting/AttachmentType");
        }

        /// <summary>
        /// ģ���б��������html
        ///</summary>
        public static void ReSetTemplateListBoxOptionsCache()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/UI/TemplateListBoxOptions");
        }

        /// <summary>
        /// �������������û��б�ͼ��
        /// </summary>
        public static void ReSetOnlineGroupIconList()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/UI/OnlineIconList");
            cache.RemoveObject("/Forum/OnlineIconTable");

        }

        /// <summary>
        /// �����������������б�
        /// </summary>
        public static void ReSetForumLinkList()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/ForumLinkList");
        }


        /// <summary>
        /// �����������ֹ����б�
        /// </summary>
        public static void ReSetBanWordList()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/BanWordList");
        }


        /// <summary>
        /// ��̳�б�
        /// </summary>
        public static void ReSetForumList()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/ForumList");
        }


        /// <summary>
        /// �����û���Ϣ
        /// </summary>
        public static void ReSetOnlineUserTable()
        {
            ;
        }

        /// <summary>
        /// ��̳����RSS��ָ�����RSS
        /// </summary>
        public static void ReSetRss()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/RSS/");
        }


        /// <summary>
        /// ָ�����RSS
        /// </summary>
        /// <param name="fid">���ɣ�</param>
        public static void ReSetForumRssXml(int fid)
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/RSS/Forum" + fid.ToString());
        }


        /// <summary>
        /// ��̳����RSS
        /// </summary>
        public static void ReSetRssXml()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/RSS/Index");
        }


        /// <summary>
        /// ģ��id�б�
        /// </summary>
        public static void ReSetValidTemplateIDList()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/TemplateIDList");
        }


        /// <summary>
        /// ��Ч���û�����չ�ֶ�
        /// </summary>
        public static void ReSetValidScoreName()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/ValidScoreName");
        }


        /// <summary>
        /// ����ѫ���б�
        /// </summary>
        public static void ReSetMedalsList()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/UI/MedalsList");
        }

        /// <summary>
        /// �����������Ӵ������ݱ�ǰ׺
        /// </summary>
        public static void ReSetDBlinkAndTablePrefix()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/BaseSetting/Dbconnectstring");
            cache.RemoveObject("/Forum/BaseSetting/TablePrefix");
        }

        /// <summary>
        /// �����������ӱ�
        /// </summary>
        public static void ReSetLastPostTableName()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/LastPostTableName");
        }


        /// <summary>
        /// ���������б�
        /// </summary>
        public static void ReSetAllPostTableName()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/PostTableName");

        }

        /// <summary>
        /// �������б�
        /// </summary>
        public static void ReSetAdsList()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/Advertisements");
        }

        /// <summary>
        /// ���������û���һ��ִ������������ʱ��
        /// </summary>
        public static void ReSetStatisticsSearchtime()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/StatisticsSearchtime");
        }


        /// <summary>
        /// ���������û���һ�����������Ĵ���
        /// </summary>
        public static void ReSetStatisticsSearchcount()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/StatisticsSearchcount");
        }


        /// <summary>
        /// ���������û�ͷ���б�
        /// </summary>
        public static void ReSetCommonAvatarList()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/CommonAvatarList");
        }

        /// <summary>
        /// �������ø������ַ���
        /// </summary>
        public static void ReSetJammer()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/UI/Jammer");
        }

        /// <summary>
        /// ��������ħ���б�
        /// </summary>
        public static void ReSetMagicList()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/MagicList");
        }

        /// <summary>
        /// �������öһ����ʵĿɽ��׻��ֲ���
        /// </summary>
        public static void ReSetScorePaySet()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/ScorePaySet");
        }


        /// <summary>
        /// �������õ�ǰ���ӱ������Ϣ
        /// </summary>
        public static void ReSetPostTableInfo()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/PostTableName");
            cache.RemoveObject("/Forum/LastPostTableName");
        }


        /// <summary>
        /// ����������Ӧ�������б�
        /// </summary>
        /// <param name="fid"></param>
        public static void ReSetTopiclistByFid(string fid)
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            cache.RemoveObject("/Forum/TopicList/" + fid);
        }



        /// <summary>
        /// ��������ȫ����龫�������б�
        /// </summary>
        /// <param name="count">��������</param>
        public static void ReSetDigestTopicList(int count)
        {
            ReSetFocusTopicList(count, -1, 0, TopicTimeType.All, TopicOrderType.ID, true);
        }

        //��������ָ����龫�������б�[��δ����]
        public static void ReSetDigestTopicList(int count, int fid, TopicTimeType timetype, TopicOrderType ordertype)
        {
            ReSetFocusTopicList(count, -1, fid, timetype, ordertype, true);
        }

        /// <summary>
        /// ��������ȫ��������������б�
        /// </summary>
        /// <param name="count"></param>
        /// <param name="views"></param>
        public static void ReSetHotTopicList(int count, int views)
        {
            ReSetFocusTopicList(count, views, 0, TopicTimeType.All, TopicOrderType.ID, false);
        }

        //��������ָ��������������б�[��δ����]
        public static void ReSetHotTopicList(int count, int views, int fid, TopicTimeType timetype, TopicOrderType ordertype)
        {
            ReSetFocusTopicList(count, views, fid, timetype, ordertype, false);
        }

        /// <summary>
        /// ����������������б�
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
        /// �������л���
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
        /// ����BaseConfig
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
