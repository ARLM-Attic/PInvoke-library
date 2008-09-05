using System;
using System.Text;
using System.IO;
using Discuz.Common;
using Discuz.Config;
using Discuz.Entity;
using Discuz.Common.Generic;
using System.Data;
using Discuz.Data;
using Discuz.Forum.ScheduledEvents;
using Discuz.Cache;

namespace Discuz.Forum
{
    /// <summary>
    /// ��̳��ǩ(Tag)������
    /// </summary>
    public class ForumTags
    {
        /// <summary>
        /// ��̳���ű�ǩ�����ļ�(json��ʽ)�ļ�·��
        /// </summary>
        public const string ForumHotTagJSONCacheFileName = "cache\\tag\\hottags_forum_cache_json.txt";
        /// <summary>
        /// ��̳���ű�ǩ�����ļ�(jsonp��ʽ)�ļ�·��
        /// </summary>
        public const string ForumHotTagJSONPCacheFileName = "cache\\tag\\hottags_forum_cache_jsonp.txt";
      
        /// <summary>
        /// д�����ű�ǩ�����ļ�(json��ʽ)
        /// </summary>
        /// <param name="count">����</param>
        public static void WriteHotTagsListForForumCacheFile(int count)
        {
            string filename = EventManager.RootPath + ForumHotTagJSONCacheFileName;
            List<TagInfo> tags = GetHotTagsListForForum(count);
            Tags.WriteTagsCacheFile(filename, tags, string.Empty, true);
        }

        /// <summary>
        /// д�����ű�ǩ�����ļ�(jsonp��ʽ)
        /// </summary>
        /// <param name="count"></param>
        public static void WriteHotTagsListForForumJSONPCacheFile(int count)
        {
            string filename = EventManager.RootPath + ForumHotTagJSONPCacheFileName;
            List<TagInfo> tags = GetHotTagsListForForum(count);
            Tags.WriteTagsCacheFile(filename, tags, "forumhottag_callback", true);
        }

        /// <summary>
        /// д�������ǩ�����ļ�
        /// </summary>
        /// <param name="tagsArray">��ǩ����</param>
        /// <param name="topicid">����Id</param>
        public static void WriteTopicTagsCacheFile(int topicid)
        {
            StringBuilder dir = new StringBuilder();
            dir.Append(BaseConfigs.GetForumPath);
            dir.Append("cache/topic/magic/");
            dir.Append((topicid / 1000 + 1).ToString());
            dir.Append("/");
            string filename = Utils.GetMapPath(dir.ToString() + topicid.ToString() + "_tags.config");
            List<TagInfo> tags = GetTagsListByTopic(topicid);
            Tags.WriteTagsCacheFile(filename, tags, string.Empty, false);
        }

        /// <summary>
        /// ��ȡ������������Tag
        /// </summary>
        /// <param name="topicid">����Id</param>
        /// <returns>List</returns>
        public static List<TagInfo> GetTagsListByTopic(int topicid)
        {
            List<TagInfo> tags = new List<TagInfo>();

            IDataReader reader = DatabaseProvider.GetInstance().GetTagsListByTopic(topicid);

            while (reader.Read())
            {
                tags.Add(Tags.LoadSingleTagInfo(reader));
            }
            reader.Close();

            return tags;
        }

        /// <summary>
        /// ��ȡ60����̳���ű�ǩ
        /// </summary>
        /// <returns>List</returns>
        public static List<TagInfo> GetHotTagsListForForum()
        {
            return GetHotTagsListForForum(60);
        }

        /// <summary>
        /// ��ȡ��̳���ű�ǩ
        /// </summary>
        /// <param name="count">����</param>
        /// <returns>List</returns>
        public static List<TagInfo> GetHotTagsListForForum(int count)
        {
            List<TagInfo> tags = new List<TagInfo>();

            IDataReader reader = DatabaseProvider.GetInstance().GetHotTagsListForForum(count);

            while (reader.Read())
            {
                tags.Add(Tags.LoadSingleTagInfo(reader));
            }
            reader.Close();

            return tags;
        }    

        /// <summary>
        /// ���ű�ǩ
        /// </summary>
        /// <param name="count">��ǩ��</param>
        /// <returns>TagInfo</returns>
        public static TagInfo[] GetCachedHotForumTags(int count)
        {
            TagInfo[] tags;
            DNTCache cache = DNTCache.GetCacheService();
            tags = cache.RetrieveObject("/Forum/Tag/Hot-" + count) as TagInfo[];
            if (tags != null)
            {
                return tags;
            }

#if NET1
            System.Collections.ArrayList tagList = new System.Collections.ArrayList();
#else
            List<TagInfo> tagList = new List<TagInfo>();
#endif
            

            IDataReader reader = DatabaseProvider.GetInstance().GetHotTagsListForForum(count);

            while (reader.Read())
            {
                tagList.Add(Tags.LoadSingleTagInfo(reader));
            }
            reader.Close();
#if NET1
            tags = (TagInfo[])tagList.ToArray(typeof(TagInfo));
#else
            tags = tagList.ToArray();
#endif


            Discuz.Cache.ICacheStrategy ics = new ForumCacheStrategy();
            ics.TimeOut = 360;
            cache.LoadCacheStrategy(ics);
            cache.AddObject("/Forum/Tag/Hot-" + count, tags);
            cache.LoadDefaultCacheStrategy();    

            return tags;
        }
        /// <summary>
        /// ɾ���������
        /// </summary>
        /// <param name="topicid">����ID</param>
        public static void DeleteTopicTags(int topicid)
        {
            DatabaseProvider.GetInstance().DeleteTopicTags(topicid);
        }



        public static void CreateTopicTags(string[] tagArray, int topicid, int userid, string curdatetime)
        {
            DatabaseProvider.GetInstance().CreateTopicTags(string.Join(" ", tagArray), topicid, userid, curdatetime);
            ForumTags.WriteTopicTagsCacheFile(topicid);
        }
    }
}