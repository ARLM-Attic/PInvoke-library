using System;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

using Discuz.Common;
using Discuz.Data;
using Discuz.Config;
using Discuz.Entity;
using Discuz.Plugin.Space;
using Discuz.Plugin.Album;
using Discuz.Plugin;

namespace Discuz.Forum
{
	/// <summary>
	/// ����������
	/// </summary>
	public class Searches
	{
		
        private static Regex regexSpacePost = new Regex(@"<SpacePosts>([\s\S]+?)</SpacePosts>");

        private static Regex regexAlbum = new Regex(@"<Albums>([\s\S]+?)</Albums>");

        private static Regex regexForumTopics = new Regex(@"<ForumTopics>([\s\S]+?)</ForumTopics>");
        
        /// <summary>
		/// ������������
		/// </summary>
		/// <param name="cacheinfo">����������Ϣ</param>
		/// <returns>��������id</returns>
		public static int CreateSearchCache(SearchCacheInfo cacheinfo)
		{
            return DatabaseProvider.GetInstance().CreateSearchCache(cacheinfo);
		}
		
		/// <summary>
		/// ����ָ��������������
		/// </summary>
		/// <param name="posttableid">���ӱ�id</param>
		/// <param name="userid">�û�id</param>
		/// <param name="usergroupid">�û���id</param>
		/// <param name="keyword">�ؼ���</param>
		/// <param name="posterid">������id</param>
		/// <param name="type">��������</param>
		/// <param name="searchforumid">�������id</param>
		/// <param name="keywordtype">�ؼ�������</param>
		/// <param name="searchtime">����ʱ��</param>
		/// <param name="searchtimetype">����ʱ������</param>
		/// <param name="resultorder">�������ʽ</param>
		/// <param name="resultordertype">�����������</param>
		/// <returns>����ɹ��򷵻�searchid, ���򷵻�-1</returns>
		public static int Search(int posttableid, int userid, int usergroupid, string keyword, int posterid, string type, string searchforumid, int keywordtype, int searchtime, int searchtimetype, int resultorder, int resultordertype)
		{
            if (posttableid == 0)
            { 
                posttableid = Utils.StrToInt(Posts.GetPostTableID(), 1);
            }
            return DatabaseProvider.GetInstance().Search(posttableid, userid, usergroupid, keyword, posterid, type, searchforumid, keywordtype, searchtime, searchtimetype, resultorder, resultordertype);
		}




	

		/// <summary>
		/// ��ָ�������������DataTable
		/// </summary>
		/// <param name="posttableid">���ӷֱ�id</param>
		/// <param name="searchid">���������searchid</param>
		/// <param name="pagesize">ÿҳ�ļ�¼��</param>
		/// <param name="pageindex">��ǰҳ��</param>
		/// <param name="topiccount">�����¼��</param>
		/// <param name="type">��������</param>
		/// <returns>���������DataTable</returns>
		public static DataTable GetSearchCacheList(int posttableid, int searchid, int pagesize, int pageindex, out int topiccount,string type)
		{
			topiccount = 0;
            DataTable dt = DatabaseProvider.GetInstance().GetSearchCache(searchid);
			if (dt.Rows.Count == 0)
			{
				return new DataTable();
			}
            string cachedidlist = dt.Rows[0][0].ToString();

            Match m;
            switch (type)
            {
                case "spacepost":
                    #region �����ռ���־
                    m = regexSpacePost.Match(cachedidlist);

                    if (m.Success)
                    {
                        string tids = GetCurrentPageTids(m.Groups[1].Value, out topiccount, pagesize, pageindex);

                        if (tids == string.Empty)
                        {
                            return new DataTable();
                        }

                        return SpacePluginProvider.GetInstance() == null ? new DataTable() : SpacePluginProvider.GetInstance().GetResult(pagesize, tids);
                    }
                    #endregion
                    break;
                case "album":
                    #region �������

                    m = regexAlbum.Match(cachedidlist);

                    if (m.Success)
                    {
                        string tids = GetCurrentPageTids(m.Groups[1].Value, out topiccount, pagesize, pageindex);

                        if (tids == string.Empty)
                        {
                            return new DataTable();
                        }

                        return AlbumPluginProvider.GetInstance() == null ? new DataTable() : AlbumPluginProvider.GetInstance().GetResult(pagesize, tids);
                    }
                    #endregion
                    break;
                default:
                    #region ������̳

                    m = regexForumTopics.Match(cachedidlist);

                    if (m.Success)
                    {
                        string tids = GetCurrentPageTids(m.Groups[1].Value, out topiccount, pagesize, pageindex);

                        if (tids == string.Empty)
                        {
                            return new DataTable();
                        }

                        if (type == "digest")
                        {
                            return DatabaseProvider.GetInstance().GetSearchDigestTopicsList(pagesize, tids);
                        }
                        if (type == "post")
                        {
                            return DatabaseProvider.GetInstance().GetSearchPostsTopicsList(pagesize, tids, Posts.GetPostTableName());
                        }
                        else
                        {
                            return DatabaseProvider.GetInstance().GetSearchTopicsList(pagesize, tids);
                        }
                    }
                    #endregion
                    break;

                 

            }


            return new DataTable();
		}

        /// <summary>
        /// ��õ�ǰҳ��Tid�б�
        /// </summary>
        /// <param name="tids">ȫ��Tid�б�</param>
        /// <returns></returns>
        private static string GetCurrentPageTids(string tids, out int topiccount, int pagesize, int pageindex)
        {
            string[] tid = Utils.SplitString(tids, ",");
            topiccount = tid.Length;
            int pagecount = topiccount % pagesize == 0 ? topiccount / pagesize : topiccount / pagesize + 1;
            if (pagecount < 1)
            {
                pagecount = 1;
            }
            if (pageindex  > pagecount)
            {
                pageindex = pagecount;
            }
            int startindex = pagesize * (pageindex - 1);
            StringBuilder strTids = new StringBuilder();
            for (int i = startindex; i < topiccount; i++)
            {
                if (i > startindex + pagesize)
                {
                    break;
                }
                else
                {
                    strTids.Append(tid[i]);
                    strTids.Append(",");
                }
            }
            strTids.Remove(strTids.Length - 1, 1);

            return strTids.ToString();
        }
		
		/// <summary>
		/// ����ȫ������
		/// </summary>
		public static void ConfirmFullTextEnable()
		{
            DatabaseProvider.GetInstance().ConfirmFullTextEnable();
		}

	}
}
