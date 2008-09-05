using System;
using System.Data;
using System.Data.Common;

using System.Text;
using Discuz.Common;
using Discuz.Data;
using Discuz.Config;
using Discuz.Entity;
using System.IO;
using Discuz.Common.Generic;
using System.Text.RegularExpressions;

namespace Discuz.Forum
{
    public enum MagicType
    { 
        //˳�򰴴�����
        /// <summary>
        /// html���� ռλ1
        /// </summary>
        HtmlTitle = 1,
        /// <summary>
        /// ħ������ ռλ3
        /// </summary>
        MagicTopic = 2,    
        /// <summary>
        /// ��ǩ, ռλ1
        /// </summary>
        TopicTag = 3,


    }
	/// <summary>
	/// ���������
	/// </summary>
	public class Topics
	{
	    	
		//private static object syncObj = new object();


		/// <summary>
		/// �����û�Id��ȡ��ظ�������������
		/// </summary>
        /// <param name="userId">�û�Id</param>
        /// <returns>�ظ�������������</returns>
		public static int GetTopicsCountbyReplyUserId(int userId)
		{
            return DatabaseProvider.GetInstance().GetTopicsCountbyReplyUserId(userId);	
        }

	

		/// <summary>
		/// �����û�Id��ȡ��������
		/// </summary>
        /// <param name="userId">�û�Id</param>
        /// <returns>��������</returns>
		public static int GetTopicsCountbyUserId(int userId)
		{
            return DatabaseProvider.GetInstance().GetTopicsCountbyUserId(userId);	
        }


     
		/// <summary>
		/// ����������
		/// </summary>
		/// <param name="topicinfo">������Ϣ</param>
		/// <returns>��������ID</returns>
		public static int CreateTopic(TopicInfo topicinfo)
		{
            return DatabaseProvider.GetInstance().CreateTopic(topicinfo);
        }

		
		/// <summary>
		/// ���Ӹ�����������
		/// </summary>
		/// <param name="fpidlist">�����id�б�</param>
		/// <param name="topics">������</param>
		/// <param name="posts">������</param>
		public static void AddParentForumTopics(string fpidlist, int topics, int posts)
		{
            if (fpidlist != "")
            {
                DatabaseProvider.GetInstance().AddParentForumTopics(fpidlist, topics, posts);
            }
        }

		/// <summary>
		/// ���������Ϣ
		/// </summary>
		/// <param name="tid">Ҫ��õ�����ID</param>
		public static TopicInfo GetTopicInfo(int tid)
		{
			return GetTopicInfo(tid, 0, 0);
		}
		
		/// <summary>
		/// ���������Ϣ
		/// </summary>
		/// <param name="tid">Ҫ��õ�����ID</param>
		/// <param name="tid">���ID</param>
		/// <param name="mode">ģʽѡ��, 0=��ǰ����, 1=��һ����, 2=��һ����</param>
		public static TopicInfo GetTopicInfo(int tid, int fid, byte mode)
		{
			TopicInfo topicinfo = new TopicInfo();

            IDataReader reader = DatabaseProvider.GetInstance().GetTopicInfo(tid, fid, mode);
			if (reader.Read())
			{
				topicinfo.Tid = Int32.Parse(reader["tid"].ToString());
				topicinfo.Fid = Int32.Parse(reader["fid"].ToString());
				topicinfo.Iconid = Int32.Parse(reader["iconid"].ToString());
				topicinfo.Title = reader["title"].ToString();
				topicinfo.Typeid = Int32.Parse(reader["typeid"].ToString());
				topicinfo.Readperm = Int32.Parse(reader["readperm"].ToString());
				topicinfo.Price = Int32.Parse(reader["price"].ToString());
				topicinfo.Poster = reader["poster"].ToString();
				topicinfo.Posterid = Int32.Parse(reader["posterid"].ToString());
				topicinfo.Postdatetime = reader["postdatetime"].ToString();
				topicinfo.Lastpost = reader["lastpost"].ToString();
				topicinfo.Lastposter = reader["lastposter"].ToString();
				topicinfo.Lastposterid = Int32.Parse(reader["LastposterID"].ToString());
				topicinfo.Lastpostid = Int32.Parse(reader["LastpostID"].ToString());
				topicinfo.Views = Int32.Parse(reader["views"].ToString());
				topicinfo.Replies = Int32.Parse(reader["replies"].ToString());
				topicinfo.Displayorder = Int32.Parse(reader["displayorder"].ToString());
				topicinfo.Highlight = reader["highlight"].ToString();
				topicinfo.Digest = Int32.Parse(reader["digest"].ToString());
				topicinfo.Rate = Int32.Parse(reader["rate"].ToString());
				topicinfo.Hide = Int32.Parse(reader["hide"].ToString());
				//topicinfo.Poll = Int32.Parse(reader["poll"].ToString());
				topicinfo.Attachment = Int32.Parse(reader["attachment"].ToString());
				topicinfo.Moderated = Int32.Parse(reader["moderated"].ToString());
				topicinfo.Closed = Int32.Parse(reader["closed"].ToString());
				topicinfo.Magic = Int32.Parse(reader["magic"].ToString());
                topicinfo.Identify = Int32.Parse(reader["identify"].ToString());
                topicinfo.Special = byte.Parse(reader["special"].ToString());
				reader.Close();
				return topicinfo;
			}
			else
			{
				reader.Close();
				return null;
			}
		}


       
        /// <summary>
		/// ��������б�
		/// </summary>
		/// <param name="topiclist">����id�б�</param>
		/// <returns>�����б�</returns>
		public static DataTable GetTopicList(string topiclist)
		{
			return GetTopicList(topiclist, -10);
		}

		/// <summary>
		/// ���ָ���������б�
		/// </summary>
		/// <param name="topiclist">����ID�б�</param>
		/// <param name="displayorder">order������( WHERE [displayorder]>��ֵ)</param>
        /// <returns>�����б�</returns>
		public static DataTable GetTopicList(string topiclist, int displayorder)
		{
			if (!Utils.IsNumericArray(topiclist.Split(',')))
			{
				return null;
			}
            return DatabaseProvider.GetInstance().GetTopicList(topiclist, displayorder);
		}

		/// <summary>
		/// �õ��ö�������Ϣ
		/// </summary>
		/// <param name="fid">���ID</param>
		/// <returns>�ö�����</returns>
		public static DataRow GetTopTopicListID(int fid)
		{
			DataRow dr = null;
			string xmlPath = Utils.GetMapPath(BaseConfigs.GetForumPath + "cache/topic/" + fid.ToString() + ".xml");
			if (Utils.FileExists(xmlPath))
			{
				//xmlPath=Utils.GetMapPath(xmlPath);
				DataSet ds = new DataSet();
				ds.ReadXml(@xmlPath,XmlReadMode.ReadSchema);
				if (ds.Tables.Count > 0)
				{
					if (ds.Tables[0].Rows.Count > 0)
					{
						dr = ds.Tables[0].Rows[0];
						if (Utils.CutString(dr["tid"].ToString(), 0, 1).Equals(","))
						{
							dr["tid"] = Utils.CutString(dr["tid"].ToString(), 1);
						}
					}
				}
				
				ds.Dispose();

			}
			return dr;
		}


		/// <summary>
		/// ��������Ļظ���
		/// </summary>
		/// <param name="tid">����ID</param>
		public static void UpdateTopicReplies(int tid)
		{
            DatabaseProvider.GetInstance().UpdateTopicReplies(tid, Posts.GetPostTableID(tid));
		}


		/// <summary>
		/// ����������ʾ��
		/// </summary>
		/// <param name="tid">����ID</param>
//		public static void UpdateTopicViews(int tid)
//		{
//			DbParameter[] parms = {
//									   DbHelper.MakeInParam("@tid",(DbType)(DbType)SqlDbType.Int,4,tid)								   
//								   };
//			DbHelper.ExecuteDataset(CommandType.Text, "UPDATE [" + BaseConfigs.GetTablePrefix + "topics] SET [views]= [views] + 1 WHERE [tid]=@tid", parms);
////			lock (syncObj)
////			{
////				string filePath=Utils.GetMapPath(BaseConfigs.GetForumPath + "topic/views.config");
////				if (Utils.FileExists(filePath))
////				{
////					using (StreamWriter sw = File.AppendText(filePath)) 
////					{
////						sw.WriteLine(tid);
////					}
////				}
////			}
//
//		}
//
		/// <summary>
		/// �õ���ǰ���������(δ�ر�)��������
		/// </summary>
		/// <param name="fid">���ID</param>
		/// <returns>��������</returns>
		public static int GetTopicCount(int fid)
		{
            return DatabaseProvider.GetInstance().GetTopicCount(fid);
        }

		/// <summary>
		/// �õ���ǰ�����(�����Ӱ�)����(δ�ر�)��������
		/// </summary>
		/// <param name="fid">���ID</param>
		/// <returns>��������</returns>
		public static int GetAllTopicCount(int fid)
		{
            return DatabaseProvider.GetInstance().GetAllTopicCount(fid);	
        }

		/// <summary>
		/// �õ���ǰ���������(δ�ر�)��������
		/// </summary>
		/// <param name="fid">���ID</param>
		/// <returns>��������</returns>
        public static int GetTopicCount(int fid, string condition)
		{
            return GetTopicCount(fid, condition, false);	
        }

        /// <summary>
        /// �õ���ǰ�������������
        /// </summary>
        /// <param name="fid">���ID</param>
        /// <returns>��������</returns>
        public static int GetTopicCount(int fid, string condition, bool includeclosedtopic)
        {
            return DatabaseProvider.GetInstance().GetTopicCount(fid, includeclosedtopic ? -1 : 0, condition);
        }

		/// <summary>
		/// �õ�������������������
		/// </summary>
		/// <param name="condition">����</param>
		/// <returns>��������</returns>
		public static int GetTopicCount(string condition)
		{
            return DatabaseProvider.GetInstance().GetTopicCount(condition);	
        }


		/// <summary>
		/// �����������
		/// </summary>
		/// <param name="tid">����id</param>
		/// <param name="topictitle">�±���</param>
		/// <returns>�ɹ�����1�����򷵻�0</returns>
		public static int UpdateTopicTitle(int tid, string topictitle)
		{
            return DatabaseProvider.GetInstance().UpdateTopicTitle(tid, topictitle);	
        }

		/// <summary>
		/// ��������ͼ��id
		/// </summary>
		/// <param name="tid">����id</param>
		/// <param name="iconid">����ͼ��id</param>
		/// <returns>�ɹ�����1�����򷵻�0</returns>
		public static int UpdateTopicIconID(int tid, int iconid)
		{
            return DatabaseProvider.GetInstance().UpdateTopicIconID(tid, iconid);
        }

		/// <summary>
		/// ��������۸�
		/// </summary>
		/// <param name="tid">����id</param>
		/// <returns>�ɹ�����1�����򷵻�0</returns>
		public static int UpdateTopicPrice(int tid)
		{
            return DatabaseProvider.GetInstance().UpdateTopicPrice(tid);	
        }

		/// <summary>
		/// ��������۸�
		/// </summary>
		/// <param name="tid">����id</param>
		/// <param name="price">�۸�</param>
		/// <returns>�ɹ�����1�����򷵻�0</returns>
		public static int UpdateTopicPrice(int tid,int price)
		{
            return DatabaseProvider.GetInstance().UpdateTopicPrice(tid, price);	
        }

		/// <summary>
		/// ���������Ķ�Ȩ��
		/// </summary>
		/// <param name="tid">����id</param>
		/// <param name="readperm">�Ķ�Ȩ��</param>
		/// <returns>�ɹ�����1�����򷵻�0</returns>
		public static int UpdateTopicReadperm(int tid,int readperm)
		{
            return DatabaseProvider.GetInstance().UpdateTopicReadperm(tid, readperm);	
        }

		/// <summary>
		/// ��������Ϊ�ѱ�����
		/// </summary>
		/// <param name="topiclist">����id�б�</param>
		/// <param name="moderated">�������id</param>
		/// <returns>�ɹ�����1�����򷵻�0</returns>
		public static int UpdateTopicModerated(string topiclist,int moderated)
		{
			if (!Utils.IsNumericArray(Utils.SplitString(topiclist,",")))
			{
				return 0;
			}

            return DatabaseProvider.GetInstance().UpdateTopicModerated(topiclist, moderated);
		}

		/// <summary>
		/// ��������
		/// </summary>
		/// <param name="topicinfo">������Ϣ</param>
		/// <returns>�ɹ�����1�����򷵻�0</returns>
		public static int UpdateTopic(TopicInfo topicinfo)
		{
            return DatabaseProvider.GetInstance().UpdateTopic(topicinfo);
		}

		/// <summary>
		/// �ж������б��Ƿ��ڵ�ǰ���
		/// </summary>
		/// <param name="topicidlist">��������</param>
		/// <param name="fid">���ID</param>
		/// <returns>���򷵻�TREU,����FLASH</returns>
		public static bool InSameForum(string topicidlist, int fid)
		{
			if (!Utils.IsNumericArray(Utils.SplitString(topicidlist, ",")))
			{
				return false;
			}
            return DatabaseProvider.GetInstance().InSameForum(topicidlist, fid);	
        }

		/// <summary>
		/// ����������Ϊ��������
		/// </summary>
		/// <param name="tid">����ID</param>
		/// <returns></returns>
		public static int UpdateTopicHide(int tid)
		{
            return DatabaseProvider.GetInstance().UpdateTopicHide(tid);
		}

	

        /// <summary>
        /// ��������б�
        /// </summary>
        /// <param name="forumid">���ID</param>
        /// <param name="pageid">��ǰҳ��</param>
        /// <param name="tpp">ÿҳ������</param>
        /// <returns>�����б�</returns>
        public static DataTable GetTopicList(int forumid, int pageid, int tpp)
        {
            return DatabaseProvider.GetInstance().GetTopicList(forumid, pageid, tpp);
        }


#if NET1

        #region ���⼯�������

        //���õ��������б��м����������������ֶ�
        public static DataTable GetTopicTypeName(DataTable __topiclist)
        {
            DataColumn dc = new DataColumn();
            dc.ColumnName = "topictypename";
            dc.DataType = Type.GetType("System.String");
            dc.DefaultValue = "";
            dc.AllowDBNull = true;
            __topiclist.Columns.Add(dc);

            System.Collections.SortedList __topictypearray = Caches.GetTopicTypeArray();
            object typictypename = null;
            foreach (DataRow dr in __topiclist.Rows)
            {
                typictypename = __topictypearray[Int32.Parse(dr["typeid"].ToString())];
                dr["topictypename"] = (typictypename != null && typictypename.ToString().Trim() != "") ? "[" + typictypename.ToString().Trim() + "]" : "";
            }
            return __topiclist;
        }

        /// <summary>
        /// �����û�Id��ȡ��ظ����������б�
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static MyTopicInfoCollection GetTopicsByReplyUserId(int userId, int pageIndex, int pageSize, int newmin, int hot)
        {
            MyTopicInfoCollection coll = new MyTopicInfoCollection();

            if (pageIndex <= 0)
            {
                return coll;
            }
            IDataReader reader = DatabaseProvider.GetInstance().GetTopicsByReplyUserId(userId, pageIndex, pageSize);
            if(reader != null)
            {
                while (reader.Read())
                {
                    MyTopicInfo topic = LoadSingleMyTopic(newmin, hot, reader);

                    coll.Add(topic);
                }
                reader.Close();
            }

            return coll;
        }

        /// <summary>
        /// �����û�Id��ȡ�����б�
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static MyTopicInfoCollection GetTopicsByUserId(int userId, int pageIndex, int pageSize, int newmin, int hot)
        {
            MyTopicInfoCollection coll = new MyTopicInfoCollection();

            if (pageIndex <= 0)
            {
                return coll;
            }
            IDataReader reader = DatabaseProvider.GetInstance().GetTopicsByUserId(userId, pageIndex, pageSize);

            if(reader != null)
            {
                while (reader.Read())
                {
                    MyTopicInfo topic = LoadSingleMyTopic(newmin, hot, reader);

                    coll.Add(topic);
                }
                reader.Close();
            }

            return coll;
        }



        /// <summary>
        /// ����ö�������Ϣ�б�
        /// </summary>
        /// <param name="fid">���ID</param>
        /// <param name="pagesize">ÿҳ��ʾ������</param>
        /// <param name="pageindex">��ǰҳ��</param>
        /// <param name="tids">����id�б�</param>
        /// <returns>������Ϣ�б�</returns>
        public static ShowforumPageTopicInfoCollection GetTopTopicCollection(int fid, int pagesize, int pageindex, string tids, int autoclose, int topictypeprefix)
        {
            ShowforumPageTopicInfoCollection coll = new ShowforumPageTopicInfoCollection();

            if (pageindex <= 0)
            {
                return coll;
            }

            IDataReader reader = DatabaseProvider.GetInstance().GetTopTopics(fid, pagesize, pageindex, tids);
            if (reader != null)
            {

                System.Collections.SortedList TopicTypeArray = Caches.GetTopicTypeArray();
                StringBuilder closeid = new StringBuilder();
                object TopicTypeName = null;

                while (reader.Read())
                {
                    ShowforumPageTopicInfo info = new ShowforumPageTopicInfo();
                    info.Tid = Int32.Parse(reader["tid"].ToString());
                    info.Fid = Int32.Parse(reader["fid"].ToString());
                    info.Iconid = Int32.Parse(reader["iconid"].ToString());
                    info.Title = reader["title"].ToString();
                    info.Typeid = Int32.Parse(reader["typeid"].ToString());
                    info.Readperm = Int32.Parse(reader["readperm"].ToString());
                    info.Price = Int32.Parse(reader["price"].ToString());
                    info.Poster = reader["poster"].ToString();
                    info.Posterid = Int32.Parse(reader["posterid"].ToString());
                    info.Postdatetime = reader["postdatetime"].ToString();
                    info.Lastpost = reader["lastpost"].ToString();
                    info.Lastposter = reader["lastposter"].ToString();
                    info.Lastposterid = Int32.Parse(reader["LastposterID"].ToString());
                    info.Lastpostid = Int32.Parse(reader["LastpostID"].ToString());
                    info.Views = Int32.Parse(reader["views"].ToString());
                    info.Replies = Int32.Parse(reader["replies"].ToString());
                    info.Displayorder = Int32.Parse(reader["displayorder"].ToString());
                    info.Highlight = reader["highlight"].ToString();
                    info.Digest = Int32.Parse(reader["digest"].ToString());
                    //info.Rate = Int32.Parse(reader["rate"].ToString());
                    info.Hide = Int32.Parse(reader["hide"].ToString());
                    //info.Poll = Int32.Parse(reader["poll"].ToString());
                    info.Special = byte.Parse(reader["special"].ToString());
                    info.Attachment = Int32.Parse(reader["attachment"].ToString());
                    //info.Moderated = Int32.Parse(reader["moderated"].ToString());
                    info.Closed = Int32.Parse(reader["closed"].ToString());
                    info.Special = byte.Parse(reader["special"].ToString());
                    //info.Magic = Int32.Parse(reader["magic"].ToString());
                    //����رձ��
                    if (info.Closed == 0)
                    {

                        if (autoclose > 0)
                        {
                            if (Utils.StrDateDiffHours(info.Postdatetime, autoclose * 24) > 0)
                            {
                                info.Closed = 1;
                                if (closeid.Length > 0)
                                {
                                    closeid.Append(",");
                                }
                                closeid.Append(info.Tid.ToString());
                                info.Folder = "closed";
                            }
                        }


                    }
                    else
                    {
                        info.Folder = "closed";
                        if (info.Closed > 1)
                        {
                            info.Tid = info.Closed;
                            info.Folder = "move";
                        }
                    }

                    if (info.Highlight != "")
                    {
                        info.Title = "<span style=\"" + info.Highlight + "\">" + info.Title + "</span>";
                    }

                    //��չ����
                    if (topictypeprefix > 0 && info.Typeid > 0)
                    {
                        TopicTypeName = TopicTypeArray[info.Typeid];
                        if (TopicTypeName != null && TopicTypeName.ToString().Trim() != "")
                        {
                            info.Topictypename = TopicTypeName.ToString().Trim();
                        }
                    }
                    else
                    {
                        info.Topictypename = "";
                    }
                    //

                    coll.SortAdd(info);
                }
                reader.Close();
                if (closeid.Length > 0)
                {
                    TopicAdmins.SetClose(closeid.ToString(), 1);
                }
            }

            return coll;


        }

        /// <summary>
        /// ���һ��������Ϣ�б�
        /// </summary>
        /// <param name="fid">���ID</param>
        /// <param name="condition">����</param>
        /// <param name="pagesize">ÿҳ��ʾ������</param>
        /// <param name="pageindex">��ǰҳ��</param>
        /// <param name="newmin">������ٷ����ڵ�������Ϊ��������</param>
        /// <param name="hot">���ٸ�������Ϊ����������</param>
        /// <returns>������Ϣ�б�</returns>
        public static ShowforumPageTopicInfoCollection GetTopicCollection(int fid, int pagesize, int pageindex, int startnum, int newmin, int hot, int autoclose, int topictypeprefix, string condition)
        {
            ShowforumPageTopicInfoCollection coll = new ShowforumPageTopicInfoCollection();

            if (pageindex <= 0)
            {
                return coll;
            }

            IDataReader reader = DatabaseProvider.GetInstance().GetTopics(fid, pagesize, pageindex, startnum, condition);
            if (reader != null)
            {

                System.Collections.SortedList TopicTypeArray = Caches.GetTopicTypeArray();
                StringBuilder closeid = new StringBuilder();
                object TopicTypeName = null;

                while (reader.Read())
                {
                    ShowforumPageTopicInfo info = new ShowforumPageTopicInfo();
                    info.Tid = Int32.Parse(reader["tid"].ToString());
                    //info.Fid = Int32.Parse(reader["fid"].ToString());
                    info.Iconid = Int32.Parse(reader["iconid"].ToString());
                    info.Title = reader["title"].ToString();
                    info.Typeid = Int32.Parse(reader["typeid"].ToString());
                    info.Readperm = Int32.Parse(reader["readperm"].ToString());
                    info.Price = Int32.Parse(reader["price"].ToString());
                    info.Poster = reader["poster"].ToString();
                    info.Posterid = Int32.Parse(reader["posterid"].ToString());
                    info.Postdatetime = reader["postdatetime"].ToString();
                    info.Lastpost = reader["lastpost"].ToString();
                    info.Lastposter = reader["lastposter"].ToString();
                    info.Lastposterid = Int32.Parse(reader["LastposterID"].ToString());
                    info.Lastpostid = Int32.Parse(reader["LastpostID"].ToString());
                    info.Views = Int32.Parse(reader["views"].ToString());
                    info.Replies = Int32.Parse(reader["replies"].ToString());
                    info.Displayorder = Int32.Parse(reader["displayorder"].ToString());
                    info.Highlight = reader["highlight"].ToString();
                    info.Digest = Int32.Parse(reader["digest"].ToString());
                    //info.Rate = Int32.Parse(reader["rate"].ToString());
                    info.Hide = Int32.Parse(reader["hide"].ToString());
                    //info.Poll = Int32.Parse(reader["poll"].ToString());
                    info.Special = byte.Parse(reader["special"].ToString());
                    info.Attachment = Int32.Parse(reader["attachment"].ToString());
                    //info.Moderated = Int32.Parse(reader["moderated"].ToString());
                    info.Closed = Int32.Parse(reader["closed"].ToString());
                    //info.Magic = Int32.Parse(reader["magic"].ToString());
                    //����رձ��
                    if (info.Closed == 0)
                    {
                        string oldtopic = ForumUtils.GetCookie("oldtopic") + "D";
                        if (oldtopic.IndexOf("D" + info.Tid.ToString() + "D") == -1 && DateTime.Now.AddMinutes(-1 * newmin) < DateTime.Parse(info.Lastpost))
                        {
                            info.Folder = "new";
                        }
                        else
                        {
                            info.Folder = "old";
                        }


                        if (info.Replies >= hot)
                        {
                            info.Folder += "hot";
                        }

                        if (autoclose > 0)
                        {
                            if (Utils.StrDateDiffHours(info.Postdatetime, autoclose * 24) > 0)
                            {
                                info.Closed = 1;
                                if (closeid.Length > 0)
                                {
                                    closeid.Append(",");
                                }
                                closeid.Append(info.Tid.ToString());
                                info.Folder = "closed";
                            }
                        }


                    }
                    else
                    {
                        info.Folder = "closed";
                        if (info.Closed > 1)
                        {
                            info.Tid = info.Closed;
                            info.Folder = "move";
                        }
                    }

                    if (info.Highlight != "")
                    {
                        info.Title = "<span style=\"" + info.Highlight + "\">" + info.Title + "</span>";
                    }

                    //��չ����
                    if (topictypeprefix > 0 && info.Typeid > 0)
                    {
                        TopicTypeName = TopicTypeArray[info.Typeid];
                        if (TopicTypeName != null && TopicTypeName.ToString().Trim() != "")
                        {
                            info.Topictypename = TopicTypeName.ToString().Trim();
                        }
                    }
                    else
                    {
                        info.Topictypename = "";
                    }
                    //

                    coll.Add(info);
                }
                reader.Close();
                if (closeid.Length > 0)
                {
                    TopicAdmins.SetClose(closeid.ToString(), 1);
                }
            }
            return coll;

        }

        /// <summary>
        /// ��ȡ�����б�
        /// </summary>
        /// <param name="fid">���id</param>
        /// <param name="pagesize">ÿ����ҳ��������</param>
        /// <param name="pageindex">��ҳҳ��</param>
        /// <param name="startnum">�ö�������</param>
        /// <param name="newmin">������ٷ����ڵ�������Ϊ��������</param>
        /// <param name="hot">���ٸ�������Ϊ����������</param>
        /// <param name="condition">����</param>
        /// <param name="orderby">����</param>
        /// <param name="ascdesc">��/����</param>
        /// <returns>�����б�</returns>
        public static ShowforumPageTopicInfoCollection GetTopicCollection(int fid, int pagesize, int pageindex, int startnum, int newmin, int hot, int autoclose, int topictypeprefix, string condition, string orderby, int ascdesc)
        {
            ShowforumPageTopicInfoCollection coll = new ShowforumPageTopicInfoCollection();

            if (pageindex <= 0)
            {
                return coll;
            }

            IDataReader reader = DatabaseProvider.GetInstance().GetTopicsByDate(fid, pagesize, pageindex, startnum, condition, orderby, ascdesc);
            if (reader != null)
            {

                System.Collections.SortedList TopicTypeArray = Caches.GetTopicTypeArray();
                StringBuilder closeid = new StringBuilder();
                object TopicTypeName = null;

                while (reader.Read())
                {
                    ShowforumPageTopicInfo info = new ShowforumPageTopicInfo();
                    info.Tid = Int32.Parse(reader["tid"].ToString());
                    //info.Fid = Int32.Parse(reader["fid"].ToString());
                    info.Iconid = Int32.Parse(reader["iconid"].ToString());
                    info.Title = reader["title"].ToString();
                    info.Typeid = Int32.Parse(reader["typeid"].ToString());
                    info.Readperm = Int32.Parse(reader["readperm"].ToString());
                    info.Price = Int32.Parse(reader["price"].ToString());
                    info.Poster = reader["poster"].ToString();
                    info.Posterid = Int32.Parse(reader["posterid"].ToString());
                    info.Postdatetime = reader["postdatetime"].ToString();
                    info.Lastpost = reader["lastpost"].ToString();
                    info.Lastposter = reader["lastposter"].ToString();
                    info.Lastposterid = Int32.Parse(reader["LastposterID"].ToString());
                    info.Lastpostid = Int32.Parse(reader["LastpostID"].ToString());
                    info.Views = Int32.Parse(reader["views"].ToString());
                    info.Replies = Int32.Parse(reader["replies"].ToString());
                    info.Displayorder = Int32.Parse(reader["displayorder"].ToString());
                    info.Highlight = reader["highlight"].ToString();
                    info.Digest = Int32.Parse(reader["digest"].ToString());
                    //info.Rate = Int32.Parse(reader["rate"].ToString());
                    info.Hide = Int32.Parse(reader["hide"].ToString());
                    //info.Poll = Int32.Parse(reader["poll"].ToString());
                    info.Special = byte.Parse(reader["special"].ToString());
                    info.Attachment = Int32.Parse(reader["attachment"].ToString());
                    //info.Moderated = Int32.Parse(reader["moderated"].ToString());
                    info.Closed = Int32.Parse(reader["closed"].ToString());
                    //info.Magic = Int32.Parse(reader["magic"].ToString());
                    //����رձ��
                    if (info.Closed == 0)
                    {
                        string oldtopic = ForumUtils.GetCookie("oldtopic") + "D";
                        if (oldtopic.IndexOf("D" + info.Tid.ToString() + "D") == -1 && DateTime.Now.AddMinutes(-1 * newmin) < DateTime.Parse(info.Lastpost))
                        {
                            info.Folder = "new";
                        }
                        else
                        {
                            info.Folder = "old";
                        }


                        if (info.Replies >= hot)
                        {
                            info.Folder += "hot";
                        }

                        if (autoclose > 0)
                        {
                            if (Utils.StrDateDiffHours(info.Postdatetime, autoclose * 24) > 0)
                            {
                                info.Closed = 1;
                                if (closeid.Length > 0)
                                {
                                    closeid.Append(",");
                                }
                                closeid.Append(info.Tid.ToString());
                                info.Folder = "closed";
                            }
                        }


                    }
                    else
                    {
                        info.Folder = "closed";
                        if (info.Closed > 1)
                        {
                            info.Tid = info.Closed;
                            info.Folder = "move";
                        }
                    }

                    if (info.Highlight != "")
                    {
                        info.Title = "<span style=\"" + info.Highlight + "\">" + info.Title + "</span>";
                    }

                    //��չ����
                    if (topictypeprefix > 0 && info.Typeid > 0)
                    {
                        TopicTypeName = TopicTypeArray[info.Typeid];
                        if (TopicTypeName != null && TopicTypeName.ToString().Trim() != "")
                        {
                            info.Topictypename = TopicTypeName.ToString().Trim();
                        }
                    }
                    else
                    {
                        info.Topictypename = "";
                    }
                    //

                    coll.Add(info);
                }
                reader.Close();
                if (closeid.Length > 0)
                {
                    TopicAdmins.SetClose(closeid.ToString(), 1);
                }
            }
            return coll;
        }

        /// <summary>
        /// �Է�������,��������ҳ����ʾ���в�ѯ�ĺ���
        /// </summary>
        /// <param name="pagesize">ÿ����ҳ��������</param>
        /// <param name="pageindex">��ҳҳ��</param>
        /// <param name="startnum">�ö�������</param>
        /// <param name="newmin">������ٷ����ڵ�������Ϊ��������</param>
        /// <param name="hot">���ٸ�������Ϊ����������</param>
        /// <param name="condition">����</param>
        /// <returns>�����б�</returns>
        public static ShowforumPageTopicInfoCollection GetTopicCollectionByType(int pagesize, int pageindex, int startnum, int newmin, int hot, int autoclose, int topictypeprefix, string condition, int ascdesc)
        {
            ShowforumPageTopicInfoCollection coll = new ShowforumPageTopicInfoCollection();

            if (pageindex <= 0)
            {
                return coll;
            }
            IDataReader reader = DatabaseProvider.GetInstance().GetTopicsByType(pagesize, pageindex, startnum, condition, ascdesc);
            if (reader != null)
            {

                System.Collections.SortedList TopicTypeArray = Caches.GetTopicTypeArray();
                StringBuilder closeid = new StringBuilder();
                object TopicTypeName = null;

                while (reader.Read())
                {
                    ShowforumPageTopicInfo info = new ShowforumPageTopicInfo();
                    info.Tid = Int32.Parse(reader["tid"].ToString());
                    //info.Fid = Int32.Parse(reader["fid"].ToString());
                    info.Iconid = Int32.Parse(reader["iconid"].ToString());
                    info.Title = reader["title"].ToString();
                    info.Typeid = Int32.Parse(reader["typeid"].ToString());
                    info.Readperm = Int32.Parse(reader["readperm"].ToString());
                    info.Price = Int32.Parse(reader["price"].ToString());
                    info.Poster = reader["poster"].ToString();
                    info.Posterid = Int32.Parse(reader["posterid"].ToString());
                    info.Postdatetime = reader["postdatetime"].ToString();
                    info.Lastpost = reader["lastpost"].ToString();
                    info.Lastposter = reader["lastposter"].ToString();
                    info.Lastposterid = Int32.Parse(reader["LastposterID"].ToString());
                    info.Lastpostid = Int32.Parse(reader["LastpostID"].ToString());
                    info.Views = Int32.Parse(reader["views"].ToString());
                    info.Replies = Int32.Parse(reader["replies"].ToString());
                    info.Displayorder = Int32.Parse(reader["displayorder"].ToString());
                    info.Highlight = reader["highlight"].ToString();
                    info.Digest = Int32.Parse(reader["digest"].ToString());
                    //info.Rate = Int32.Parse(reader["rate"].ToString());
                    info.Hide = Int32.Parse(reader["hide"].ToString());
                    //info.Poll = Int32.Parse(reader["poll"].ToString());
                    info.Special = byte.Parse(reader["special"].ToString());
                    info.Attachment = Int32.Parse(reader["attachment"].ToString());
                    //info.Moderated = Int32.Parse(reader["moderated"].ToString());
                    info.Closed = Int32.Parse(reader["closed"].ToString());
                    //info.Magic = Int32.Parse(reader["magic"].ToString());
                    //����رձ��
                    if (info.Closed == 0)
                    {
                        string oldtopic = ForumUtils.GetCookie("oldtopic") + "D";
                        if (oldtopic.IndexOf("D" + info.Tid.ToString() + "D") == -1 && DateTime.Now.AddMinutes(-1 * newmin) < DateTime.Parse(info.Lastpost))
                        {
                            info.Folder = "new";
                        }
                        else
                        {
                            info.Folder = "old";
                        }


                        if (info.Replies >= hot)
                        {
                            info.Folder += "hot";
                        }

                        if (autoclose > 0)
                        {
                            if (Utils.StrDateDiffHours(info.Postdatetime, autoclose * 24) > 0)
                            {
                                info.Closed = 1;
                                if (closeid.Length > 0)
                                {
                                    closeid.Append(",");
                                }
                                closeid.Append(info.Tid.ToString());
                                info.Folder = "closed";
                            }
                        }


                    }
                    else
                    {
                        info.Folder = "closed";
                        if (info.Closed > 1)
                        {
                            info.Tid = info.Closed;
                            info.Folder = "move";
                        }
                    }

                    if (info.Highlight != "")
                    {
                        info.Title = "<span style=\"" + info.Highlight + "\">" + info.Title + "</span>";
                    }

                    //��չ����
                    if (topictypeprefix > 0 && info.Typeid > 0)
                    {
                        TopicTypeName = TopicTypeArray[info.Typeid];
                        if (TopicTypeName != null && TopicTypeName.ToString().Trim() != "")
                        {
                            info.Topictypename = TopicTypeName.ToString().Trim();
                        }
                    }
                    else
                    {
                        info.Topictypename = "";
                    }
                    //

                    coll.Add(info);
                }
                reader.Close();
                if (closeid.Length > 0)
                {
                    TopicAdmins.SetClose(closeid.ToString(), 1);
                }
            }
            return coll;
        }

		private static MyTopicInfo LoadSingleMyTopic(int newmin, int hot, IDataReader reader)
		{
			MyTopicInfo topic = new MyTopicInfo();
			topic.Tid = Int32.Parse(reader["tid"].ToString());
			topic.Fid = Int32.Parse(reader["fid"].ToString());
			topic.Iconid = Int32.Parse(reader["iconid"].ToString());
			topic.Forumname = Forums.GetForumInfo(topic.Fid).Name;
			topic.Title = reader["title"].ToString();
			topic.Typeid = Int32.Parse(reader["typeid"].ToString());
			topic.Readperm = Int32.Parse(reader["readperm"].ToString());
			topic.Price = Int32.Parse(reader["price"].ToString());
			topic.Poster = reader["poster"].ToString();
			topic.Posterid = Int32.Parse(reader["posterid"].ToString());
			topic.Postdatetime = reader["postdatetime"].ToString();
			topic.Lastpost = reader["lastpost"].ToString();
			topic.Lastposter = reader["lastposter"].ToString();
			topic.Lastposterid = Int32.Parse(reader["LastposterID"].ToString());
			topic.Lastpostid = Int32.Parse(reader["LastpostID"].ToString());
			topic.Views = Int32.Parse(reader["views"].ToString());
			topic.Replies = Int32.Parse(reader["replies"].ToString());
			topic.Displayorder = Int32.Parse(reader["displayorder"].ToString());
			topic.Highlight = reader["highlight"].ToString();
			topic.Digest = Int32.Parse(reader["digest"].ToString());
			//topic.Rate = Int32.Parse(reader["rate"].ToString());
			topic.Hide = Int32.Parse(reader["hide"].ToString());
			//topic.Poll = Int32.Parse(reader["poll"].ToString());
            topic.Special = byte.Parse(reader["special"].ToString());
			topic.Attachment = Int32.Parse(reader["attachment"].ToString());
			//topic.Moderated = Int32.Parse(reader["moderated"].ToString());
			topic.Closed = Int32.Parse(reader["closed"].ToString());
			//info.Magic = Int32.Parse(reader["magic"].ToString());
			//����رձ��
			if (topic.Closed == 0)
			{
				string oldtopic = ForumUtils.GetCookie("oldtopic") + "D";
				if (oldtopic.IndexOf("D" + topic.Tid.ToString() + "D") == -1 && DateTime.Now.AddMinutes(-1 * newmin) < DateTime.Parse(topic.Lastpost))
				{
					topic.Folder = "new";
				}
				else
				{
					topic.Folder = "old";
				}


				if (topic.Replies >= hot)
				{
					topic.Folder += "hot";
				}
			}
			else
			{
				topic.Folder = "closed";
				if (topic.Closed > 1)
				{
					topic.Tid = topic.Closed;
					topic.Folder = "move";
				}
			}

			if (topic.Highlight != "")
			{
				topic.Title = "<span style=\"" + topic.Highlight + "\">" + topic.Title + "</span>";
			}
			return topic;
		}
        /// <summary>
        /// �Է�������,��������ҳ����ʾ���в�ѯ�ĺ���
        /// </summary>
        /// <param name="pagesize">ÿ����ҳ��������</param>
        /// <param name="pageindex">��ҳҳ��</param>
        /// <param name="startnum">�ö�������</param>
        /// <param name="newmin">������ٷ����ڵ�������Ϊ��������</param>
        /// <param name="hot">���ٸ�������Ϊ����������</param>
        /// <param name="condition">����</param>
        /// <param name="orderby">����</param>
        /// <param name="ascdesc">��/����</param>
        /// <returns>�����б�</returns>
        public static ShowforumPageTopicInfoCollection GetTopicCollectionByTypeDate(int pagesize, int pageindex, int startnum, int newmin, int hot, int autoclose, int topictypeprefix, string condition, string orderby, int ascdesc)
        {
            ShowforumPageTopicInfoCollection coll = new ShowforumPageTopicInfoCollection();

            if (pageindex <= 0)
            {
                return coll;
            }

            IDataReader reader = DatabaseProvider.GetInstance().GetTopicsByTypeDate(pagesize, pageindex, startnum, condition, orderby, ascdesc);
            if (reader != null)
            {

                System.Collections.SortedList TopicTypeArray = Caches.GetTopicTypeArray();
                StringBuilder closeid = new StringBuilder();
                object TopicTypeName = null;

                while (reader.Read())
                {
                    ShowforumPageTopicInfo info = new ShowforumPageTopicInfo();
                    info.Tid = Int32.Parse(reader["tid"].ToString());
                    //info.Fid = Int32.Parse(reader["fid"].ToString());
                    info.Iconid = Int32.Parse(reader["iconid"].ToString());
                    info.Title = reader["title"].ToString();
                    info.Typeid = Int32.Parse(reader["typeid"].ToString());
                    info.Readperm = Int32.Parse(reader["readperm"].ToString());
                    info.Price = Int32.Parse(reader["price"].ToString());
                    info.Poster = reader["poster"].ToString();
                    info.Posterid = Int32.Parse(reader["posterid"].ToString());
                    info.Postdatetime = reader["postdatetime"].ToString();
                    info.Lastpost = reader["lastpost"].ToString();
                    info.Lastposter = reader["lastposter"].ToString();
                    info.Lastposterid = Int32.Parse(reader["LastposterID"].ToString());
                    info.Lastpostid = Int32.Parse(reader["LastpostID"].ToString());
                    info.Views = Int32.Parse(reader["views"].ToString());
                    info.Replies = Int32.Parse(reader["replies"].ToString());
                    info.Displayorder = Int32.Parse(reader["displayorder"].ToString());
                    info.Highlight = reader["highlight"].ToString();
                    info.Digest = Int32.Parse(reader["digest"].ToString());
                    //info.Rate = Int32.Parse(reader["rate"].ToString());
                    info.Hide = Int32.Parse(reader["hide"].ToString());
                    //info.Poll = Int32.Parse(reader["poll"].ToString());
                    info.Special = byte.Parse(reader["special"].ToString());
                    info.Attachment = Int32.Parse(reader["attachment"].ToString());
                    //info.Moderated = Int32.Parse(reader["moderated"].ToString());
                    info.Closed = Int32.Parse(reader["closed"].ToString());
                    //info.Magic = Int32.Parse(reader["magic"].ToString());
                    //����رձ��
                    if (info.Closed == 0)
                    {
                        string oldtopic = ForumUtils.GetCookie("oldtopic") + "D";
                        if (oldtopic.IndexOf("D" + info.Tid.ToString() + "D") == -1 && DateTime.Now.AddMinutes(-1 * newmin) < DateTime.Parse(info.Lastpost))
                        {
                            info.Folder = "new";
                        }
                        else
                        {
                            info.Folder = "old";
                        }


                        if (info.Replies >= hot)
                        {
                            info.Folder += "hot";
                        }

                        if (autoclose > 0)
                        {
                            if (Utils.StrDateDiffHours(info.Postdatetime, autoclose * 24) > 0)
                            {
                                info.Closed = 1;
                                if (closeid.Length > 0)
                                {
                                    closeid.Append(",");
                                }
                                closeid.Append(info.Tid.ToString());
                                info.Folder = "closed";
                            }
                        }


                    }
                    else
                    {
                        info.Folder = "closed";
                        if (info.Closed > 1)
                        {
                            info.Tid = info.Closed;
                            info.Folder = "move";
                        }
                    }

                    if (info.Highlight != "")
                    {
                        info.Title = "<span style=\"" + info.Highlight + "\">" + info.Title + "</span>";
                    }

                    //��չ����
                    if (topictypeprefix > 0 && info.Typeid > 0)
                    {
                        TopicTypeName = TopicTypeArray[info.Typeid];
                        if (TopicTypeName != null && TopicTypeName.ToString().Trim() != "")
                        {
                            info.Topictypename = TopicTypeName.ToString().Trim();
                        }
                    }
                    else
                    {
                        info.Topictypename = "";
                    }
                    //

                    coll.Add(info);
                }
                reader.Close();
                if (closeid.Length > 0)
                {
                    TopicAdmins.SetClose(closeid.ToString(), 1);
                }
            }
            return coll;
        }

        #endregion

#else

        #region ���ⷺ�������

        //���õ��������б��м����������������ֶ�
        public static DataTable GetTopicTypeName(DataTable __topiclist)
        {
            DataColumn dc = new DataColumn();
            dc.ColumnName = "topictypename";
            dc.DataType = Type.GetType("System.String");
            dc.DefaultValue = "";
            dc.AllowDBNull = true;
            __topiclist.Columns.Add(dc);

            Discuz.Common.Generic.SortedList<int, object> __topictypearray = Caches.GetTopicTypeArray();
            object typictypename = null;
            foreach (DataRow dr in __topiclist.Rows)
            {
                typictypename = __topictypearray[Int32.Parse(dr["typeid"].ToString())];
                dr["topictypename"] = (typictypename != null && typictypename.ToString().Trim() != "") ? "[" + typictypename.ToString().Trim() + "]" : "";
            }
            return __topiclist;
        }

        /// <summary>
        /// �����û�Id��ȡ��ظ����������б�
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static Discuz.Common.Generic.List<MyTopicInfo> GetTopicsByReplyUserId(int userId, int pageIndex, int pageSize, int newmin, int hot)
        {
            Discuz.Common.Generic.List<MyTopicInfo> coll = new Discuz.Common.Generic.List<MyTopicInfo>();

            if (pageIndex <= 0)
            {
                return coll;
            }
            IDataReader reader = DatabaseProvider.GetInstance().GetTopicsByReplyUserId(userId, pageIndex, pageSize);

            if (reader != null)
            {
                while (reader.Read())
                {
                    MyTopicInfo topic = LoadSingleMyTopic(newmin, hot, reader);

                    coll.Add(topic);
                }
                reader.Close();
            }
            return coll;
        }

        private static MyTopicInfo LoadSingleMyTopic(int newmin, int hot, IDataReader reader)
        {
            MyTopicInfo topic = new MyTopicInfo();
            topic.Tid = Int32.Parse(reader["tid"].ToString());
            topic.Fid = Int32.Parse(reader["fid"].ToString());
            topic.Iconid = Int32.Parse(reader["iconid"].ToString());
            topic.Forumname = Forums.GetForumInfo(topic.Fid).Name;
            topic.Title = reader["title"].ToString();
            topic.Typeid = Int32.Parse(reader["typeid"].ToString());
            topic.Readperm = Int32.Parse(reader["readperm"].ToString());
            topic.Price = Int32.Parse(reader["price"].ToString());
            topic.Poster = reader["poster"].ToString();
            topic.Posterid = Int32.Parse(reader["posterid"].ToString());
            topic.Postdatetime = reader["postdatetime"].ToString();
            topic.Lastpost = reader["lastpost"].ToString();
            topic.Lastposter = reader["lastposter"].ToString();
            topic.Lastposterid = Int32.Parse(reader["LastposterID"].ToString());
            topic.Lastpostid = Int32.Parse(reader["LastpostID"].ToString());
            topic.Views = Int32.Parse(reader["views"].ToString());
            topic.Replies = Int32.Parse(reader["replies"].ToString());
            topic.Displayorder = Int32.Parse(reader["displayorder"].ToString());
            topic.Highlight = reader["highlight"].ToString();
            topic.Digest = Int32.Parse(reader["digest"].ToString());
            //topic.Rate = Int32.Parse(reader["rate"].ToString());
            topic.Hide = Int32.Parse(reader["hide"].ToString());
            //topic.Poll = Int32.Parse(reader["poll"].ToString());
            topic.Attachment = Int32.Parse(reader["attachment"].ToString());
            //topic.Moderated = Int32.Parse(reader["moderated"].ToString());
            topic.Closed = Int32.Parse(reader["closed"].ToString());
            topic.Special = byte.Parse(reader["special"].ToString());
            //info.Magic = Int32.Parse(reader["magic"].ToString());
            //����رձ��
            if (topic.Closed == 0)
            {
                string oldtopic = ForumUtils.GetCookie("oldtopic") + "D";
                if (oldtopic.IndexOf("D" + topic.Tid.ToString() + "D") == -1 && DateTime.Now.AddMinutes(-1 * newmin) < DateTime.Parse(topic.Lastpost))
                {
                    topic.Folder = "new";
                }
                else
                {
                    topic.Folder = "old";
                }


                if (topic.Replies >= hot)
                {
                    topic.Folder += "hot";
                }
            }
            else
            {
                topic.Folder = "closed";
                if (topic.Closed > 1)
                {
                    topic.Tid = topic.Closed;
                    topic.Folder = "move";
                }
            }

            if (topic.Highlight != "")
            {
                topic.Title = "<span style=\"" + topic.Highlight + "\">" + topic.Title + "</span>";
            }
            return topic;
        }

        /// <summary>
        /// �����û�Id��ȡ�����б�
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static Discuz.Common.Generic.List<MyTopicInfo> GetTopicsByUserId(int userId, int pageIndex, int pageSize, int newmin, int hot)
        {
            Discuz.Common.Generic.List<MyTopicInfo> coll = new Discuz.Common.Generic.List<MyTopicInfo>();

            if (pageIndex <= 0)
            {
                return coll;
            }
            IDataReader reader = DatabaseProvider.GetInstance().GetTopicsByUserId(userId, pageIndex, pageSize);

            if (reader != null)
            {
                while (reader.Read())
                {
                    MyTopicInfo topic = LoadSingleMyTopic(newmin, hot, reader);

                    coll.Add(topic);
                }
                reader.Close();
            }
            return coll;
        }



        /// <summary>
        /// ����ö�������Ϣ�б�
        /// </summary>
        /// <param name="fid">���ID</param>
        /// <param name="pagesize">ÿҳ��ʾ������</param>
        /// <param name="pageindex">��ǰҳ��</param>
        /// <param name="tids">����id�б�</param>
        /// <returns>������Ϣ�б�</returns>
        public static ShowforumPageTopicInfoCollection<ShowforumPageTopicInfo> GetTopTopicCollection(int fid, int pagesize, int pageindex, string tids, int autoclose, int topictypeprefix)
        {
            ShowforumPageTopicInfoCollection<ShowforumPageTopicInfo> coll = new ShowforumPageTopicInfoCollection<ShowforumPageTopicInfo>();
            if (pageindex <= 0)
            {
                return coll;
            }

            IDataReader reader = DatabaseProvider.GetInstance().GetTopTopics(fid, pagesize, pageindex, tids);
            if (reader != null)
            {

                Discuz.Common.Generic.SortedList<int, object> TopicTypeArray = Caches.GetTopicTypeArray();
                StringBuilder closeid = new StringBuilder();
                object TopicTypeName = null;

                while (reader.Read())
                {
                    ShowforumPageTopicInfo info = new ShowforumPageTopicInfo();
                    info.Tid = Int32.Parse(reader["tid"].ToString());
                    info.Fid = Int32.Parse(reader["fid"].ToString());
                    info.Iconid = Int32.Parse(reader["iconid"].ToString());
                    info.Title = reader["title"].ToString();
                    info.Typeid = Int32.Parse(reader["typeid"].ToString());
                    info.Readperm = Int32.Parse(reader["readperm"].ToString());
                    info.Price = Int32.Parse(reader["price"].ToString());
                    info.Poster = reader["poster"].ToString();
                    info.Posterid = Int32.Parse(reader["posterid"].ToString());
                    info.Postdatetime = reader["postdatetime"].ToString();
                    info.Lastpost = reader["lastpost"].ToString();
                    info.Lastposter = reader["lastposter"].ToString();
                    info.Lastposterid = Int32.Parse(reader["LastposterID"].ToString());
                    info.Lastpostid = Int32.Parse(reader["LastpostID"].ToString());
                    info.Views = Int32.Parse(reader["views"].ToString());
                    info.Replies = Int32.Parse(reader["replies"].ToString());
                    info.Displayorder = Int32.Parse(reader["displayorder"].ToString());
                    info.Highlight = reader["highlight"].ToString();
                    info.Digest = Int32.Parse(reader["digest"].ToString());
                    info.Rate = Int32.Parse(reader["rate"].ToString());
                    info.Hide = Int32.Parse(reader["hide"].ToString());
                    //info.Poll = Int32.Parse(reader["poll"].ToString());
                    info.Special = byte.Parse(reader["special"].ToString());
                    info.Attachment = Int32.Parse(reader["attachment"].ToString());
                    //info.Moderated = Int32.Parse(reader["moderated"].ToString());
                    info.Closed = Int32.Parse(reader["closed"].ToString());
                    info.Magic = Int32.Parse(reader["magic"].ToString());
                    info.Special = byte.Parse(reader["special"].ToString());
                    //����رձ��
                    if (info.Closed == 0)
                    {

                        if (autoclose > 0)
                        {
                            if (Utils.StrDateDiffHours(info.Postdatetime, autoclose * 24) > 0)
                            {
                                info.Closed = 1;
                                if (closeid.Length > 0)
                                {
                                    closeid.Append(",");
                                }
                                closeid.Append(info.Tid.ToString());
                                info.Folder = "closed";
                            }
                        }


                    }
                    else
                    {
                        info.Folder = "closed";
                        if (info.Closed > 1)
                        {
                            info.Tid = info.Closed;
                            info.Folder = "move";
                        }
                    }

                    if (info.Highlight != "")
                    {
                        info.Title = "<span style=\"" + info.Highlight + "\">" + info.Title + "</span>";
                    }

                    //��չ����
                    if (topictypeprefix > 0 && info.Typeid > 0)
                    {
                        TopicTypeName = TopicTypeArray[info.Typeid];
                        if (TopicTypeName != null && TopicTypeName.ToString().Trim() != "")
                        {
                            info.Topictypename = TopicTypeName.ToString().Trim();
                        }
                    }
                    else
                    {
                        info.Topictypename = "";
                    }
                    //

                    coll.SortAdd(info);
                }
                reader.Close();
                if (closeid.Length > 0)
                {
                    TopicAdmins.SetClose(closeid.ToString(), 1);
                }
            }

            return coll;


        }

        /// <summary>
        /// ���һ��������Ϣ�б�
        /// </summary>
        /// <param name="fid">���ID</param>
        /// <param name="condition">����</param>
        /// <param name="pagesize">ÿҳ��ʾ������</param>
        /// <param name="pageindex">��ǰҳ��</param>
        /// <param name="newmin">������ٷ����ڵ�������Ϊ��������</param>
        /// <param name="hot">���ٸ�������Ϊ����������</param>
        /// <returns>������Ϣ�б�</returns>
        public static ShowforumPageTopicInfoCollection<ShowforumPageTopicInfo> GetTopicCollection(int fid, int pagesize, int pageindex, int startnum, int newmin, int hot, int autoclose, int topictypeprefix, string condition)
        {
            ShowforumPageTopicInfoCollection<ShowforumPageTopicInfo> coll = new ShowforumPageTopicInfoCollection<ShowforumPageTopicInfo>();

            if (pageindex <= 0)
            {
                return coll;
            }

            IDataReader reader = DatabaseProvider.GetInstance().GetTopics(fid, pagesize, pageindex, startnum, condition);
            if (reader != null)
            {

                Discuz.Common.Generic.SortedList<int, object> TopicTypeArray = Caches.GetTopicTypeArray();
                StringBuilder closeid = new StringBuilder();
                object TopicTypeName = null;

                while (reader.Read())
                {
                    ShowforumPageTopicInfo info = new ShowforumPageTopicInfo();
                    info.Tid = Int32.Parse(reader["tid"].ToString());
                    //info.Fid = Int32.Parse(reader["fid"].ToString());
                    info.Iconid = Int32.Parse(reader["iconid"].ToString());
                    info.Title = reader["title"].ToString();
                    info.Typeid = Int32.Parse(reader["typeid"].ToString());
                    info.Readperm = Int32.Parse(reader["readperm"].ToString());
                    info.Price = Int32.Parse(reader["price"].ToString());
                    info.Poster = reader["poster"].ToString();
                    info.Posterid = Int32.Parse(reader["posterid"].ToString());
                    info.Postdatetime = reader["postdatetime"].ToString();
                    info.Lastpost = reader["lastpost"].ToString();
                    info.Lastposter = reader["lastposter"].ToString();
                    info.Lastposterid = Int32.Parse(reader["LastposterID"].ToString());
                    info.Lastpostid = Int32.Parse(reader["LastpostID"].ToString());
                    info.Views = Int32.Parse(reader["views"].ToString());
                    info.Replies = Int32.Parse(reader["replies"].ToString());
                    info.Displayorder = Int32.Parse(reader["displayorder"].ToString());
                    info.Highlight = reader["highlight"].ToString();
                    info.Digest = Int32.Parse(reader["digest"].ToString());
                    info.Rate = Int32.Parse(reader["rate"].ToString());
                    info.Hide = Int32.Parse(reader["hide"].ToString());
                    //info.Poll = Int32.Parse(reader["poll"].ToString());
                    info.Attachment = Int32.Parse(reader["attachment"].ToString());
                    //info.Moderated = Int32.Parse(reader["moderated"].ToString());
                    info.Closed = Int32.Parse(reader["closed"].ToString());
                    info.Magic = Int32.Parse(reader["magic"].ToString());
                    info.Special = byte.Parse(reader["special"].ToString());
                    //����رձ��
                    if (info.Closed == 0)
                    {
                        string oldtopic = ForumUtils.GetCookie("oldtopic") + "D";
                        if (oldtopic.IndexOf("D" + info.Tid.ToString() + "D") == -1 && DateTime.Now.AddMinutes(-1 * newmin) < DateTime.Parse(info.Lastpost))
                        {
                            info.Folder = "new";
                        }
                        else
                        {
                            info.Folder = "old";
                        }


                        if (info.Replies >= hot)
                        {
                            info.Folder += "hot";
                        }

                        if (autoclose > 0)
                        {
                            if (Utils.StrDateDiffHours(info.Postdatetime, autoclose * 24) > 0)
                            {
                                info.Closed = 1;
                                if (closeid.Length > 0)
                                {
                                    closeid.Append(",");
                                }
                                closeid.Append(info.Tid.ToString());
                                info.Folder = "closed";
                            }
                        }


                    }
                    else
                    {
                        info.Folder = "closed";
                        if (info.Closed > 1)
                        {
                            info.Tid = info.Closed;
                            info.Folder = "move";
                        }
                    }

                    if (info.Highlight != "")
                    {
                        info.Title = "<span style=\"" + info.Highlight + "\">" + info.Title + "</span>";
                    }

                    //��չ����
                    if (topictypeprefix > 0 && info.Typeid > 0)
                    {
                        TopicTypeName = TopicTypeArray[info.Typeid];
                        if (TopicTypeName != null && TopicTypeName.ToString().Trim() != "")
                        {
                            info.Topictypename = TopicTypeName.ToString().Trim();
                        }
                    }
                    else
                    {
                        info.Topictypename = "";
                    }
                    //

                    coll.Add(info);
                }
                reader.Close();
                if (closeid.Length > 0)
                {
                    TopicAdmins.SetClose(closeid.ToString(), 1);
                }
            }
            return coll;

        }

        /// <summary>
        /// ��ȡ�����б�
        /// </summary>
        /// <param name="fid">���id</param>
        /// <param name="pagesize">ÿ����ҳ��������</param>
        /// <param name="pageindex">��ҳҳ��</param>
        /// <param name="startnum">�ö�������</param>
        /// <param name="newmin">������ٷ����ڵ�������Ϊ��������</param>
        /// <param name="hot">���ٸ�������Ϊ����������</param>
        /// <param name="condition">����</param>
        /// <param name="orderby">����</param>
        /// <param name="ascdesc">��/����</param>
        /// <returns>�����б�</returns>
        public static ShowforumPageTopicInfoCollection<ShowforumPageTopicInfo> GetTopicCollection(int fid, int pagesize, int pageindex, int startnum, int newmin, int hot, int autoclose, int topictypeprefix, string condition, string orderby, int ascdesc)
        {
            ShowforumPageTopicInfoCollection<ShowforumPageTopicInfo> coll = new ShowforumPageTopicInfoCollection<ShowforumPageTopicInfo>();

            if (pageindex <= 0)
            {
                return coll;
            }

            IDataReader reader = DatabaseProvider.GetInstance().GetTopicsByDate(fid, pagesize, pageindex, startnum, condition, orderby, ascdesc);
            if (reader != null)
            {

                Discuz.Common.Generic.SortedList<int, object> TopicTypeArray = Caches.GetTopicTypeArray();
                StringBuilder closeid = new StringBuilder();
                object TopicTypeName = null;

                while (reader.Read())
                {
                    ShowforumPageTopicInfo info = new ShowforumPageTopicInfo();
                    info.Tid = Int32.Parse(reader["tid"].ToString());
                    //info.Fid = Int32.Parse(reader["fid"].ToString());
                    info.Iconid = Int32.Parse(reader["iconid"].ToString());
                    info.Title = reader["title"].ToString();
                    info.Typeid = Int32.Parse(reader["typeid"].ToString());
                    info.Readperm = Int32.Parse(reader["readperm"].ToString());
                    info.Price = Int32.Parse(reader["price"].ToString());
                    info.Poster = reader["poster"].ToString();
                    info.Posterid = Int32.Parse(reader["posterid"].ToString());
                    info.Postdatetime = reader["postdatetime"].ToString();
                    info.Lastpost = reader["lastpost"].ToString();
                    info.Lastposter = reader["lastposter"].ToString();
                    info.Lastposterid = Int32.Parse(reader["LastposterID"].ToString());
                    info.Lastpostid = Int32.Parse(reader["LastpostID"].ToString());
                    info.Views = Int32.Parse(reader["views"].ToString());
                    info.Replies = Int32.Parse(reader["replies"].ToString());
                    info.Displayorder = Int32.Parse(reader["displayorder"].ToString());
                    info.Highlight = reader["highlight"].ToString();
                    info.Digest = Int32.Parse(reader["digest"].ToString());
                    info.Rate = Int32.Parse(reader["rate"].ToString());
                    info.Hide = Int32.Parse(reader["hide"].ToString());
                    //info.Poll = Int32.Parse(reader["poll"].ToString());                    
                    info.Special = byte.Parse(reader["special"].ToString());
                    info.Attachment = Int32.Parse(reader["attachment"].ToString());
                    //info.Moderated = Int32.Parse(reader["moderated"].ToString());
                    info.Closed = Int32.Parse(reader["closed"].ToString());
                    info.Magic = Int32.Parse(reader["magic"].ToString());
                    //����رձ��
                    if (info.Closed == 0)
                    {
                        string oldtopic = ForumUtils.GetCookie("oldtopic") + "D";
                        if (oldtopic.IndexOf("D" + info.Tid.ToString() + "D") == -1 && DateTime.Now.AddMinutes(-1 * newmin) < DateTime.Parse(info.Lastpost))
                        {
                            info.Folder = "new";
                        }
                        else
                        {
                            info.Folder = "old";
                        }


                        if (info.Replies >= hot)
                        {
                            info.Folder += "hot";
                        }

                        if (autoclose > 0)
                        {
                            if (Utils.StrDateDiffHours(info.Postdatetime, autoclose * 24) > 0)
                            {
                                info.Closed = 1;
                                if (closeid.Length > 0)
                                {
                                    closeid.Append(",");
                                }
                                closeid.Append(info.Tid.ToString());
                                info.Folder = "closed";
                            }
                        }


                    }
                    else
                    {
                        info.Folder = "closed";
                        if (info.Closed > 1)
                        {
                            info.Tid = info.Closed;
                            info.Folder = "move";
                        }
                    }

                    if (info.Highlight != "")
                    {
                        info.Title = "<span style=\"" + info.Highlight + "\">" + info.Title + "</span>";
                    }

                    //��չ����
                    if (topictypeprefix > 0 && info.Typeid > 0)
                    {
                        TopicTypeName = TopicTypeArray[info.Typeid];
                        if (TopicTypeName != null && TopicTypeName.ToString().Trim() != "")
                        {
                            info.Topictypename = TopicTypeName.ToString().Trim();
                        }
                    }
                    else
                    {
                        info.Topictypename = "";
                    }
                    //

                    coll.Add(info);
                }
                reader.Close();
                if (closeid.Length > 0)
                {
                    TopicAdmins.SetClose(closeid.ToString(), 1);
                }
            }
            return coll;
        }

        /// <summary>
        /// �Է�������,��������ҳ����ʾ���в�ѯ�ĺ���
        /// </summary>
        /// <param name="pagesize">ÿ����ҳ��������</param>
        /// <param name="pageindex">��ҳҳ��</param>
        /// <param name="startnum">�ö�������</param>
        /// <param name="newmin">������ٷ����ڵ�������Ϊ��������</param>
        /// <param name="hot">���ٸ�������Ϊ����������</param>
        /// <param name="condition">����</param>
        /// <returns>�����б�</returns>
        public static ShowforumPageTopicInfoCollection<ShowforumPageTopicInfo> GetTopicCollectionByType(int pagesize, int pageindex, int startnum, int newmin, int hot, int autoclose, int topictypeprefix, string condition, int ascdesc)
        {
            ShowforumPageTopicInfoCollection<ShowforumPageTopicInfo> coll = new ShowforumPageTopicInfoCollection<ShowforumPageTopicInfo>();

            if (pageindex <= 0)
            {
                return coll;
            }
            IDataReader reader = DatabaseProvider.GetInstance().GetTopicsByType(pagesize, pageindex, startnum, condition, ascdesc);
            if (reader != null)
            {

                Discuz.Common.Generic.SortedList<int, object> TopicTypeArray = Caches.GetTopicTypeArray();
                StringBuilder closeid = new StringBuilder();
                object TopicTypeName = null;

                while (reader.Read())
                {
                    ShowforumPageTopicInfo info = new ShowforumPageTopicInfo();
                    info.Tid = Int32.Parse(reader["tid"].ToString());
                    //info.Fid = Int32.Parse(reader["fid"].ToString());
                    info.Iconid = Int32.Parse(reader["iconid"].ToString());
                    info.Title = reader["title"].ToString();
                    info.Typeid = Int32.Parse(reader["typeid"].ToString());
                    info.Readperm = Int32.Parse(reader["readperm"].ToString());
                    info.Price = Int32.Parse(reader["price"].ToString());
                    info.Poster = reader["poster"].ToString();
                    info.Posterid = Int32.Parse(reader["posterid"].ToString());
                    info.Postdatetime = reader["postdatetime"].ToString();
                    info.Lastpost = reader["lastpost"].ToString();
                    info.Lastposter = reader["lastposter"].ToString();
                    info.Lastposterid = Int32.Parse(reader["LastposterID"].ToString());
                    info.Lastpostid = Int32.Parse(reader["LastpostID"].ToString());
                    info.Views = Int32.Parse(reader["views"].ToString());
                    info.Replies = Int32.Parse(reader["replies"].ToString());
                    info.Displayorder = Int32.Parse(reader["displayorder"].ToString());
                    info.Highlight = reader["highlight"].ToString();
                    info.Digest = Int32.Parse(reader["digest"].ToString());
                    //info.Rate = Int32.Parse(reader["rate"].ToString());
                    info.Hide = Int32.Parse(reader["hide"].ToString());
                    //info.Poll = Int32.Parse(reader["poll"].ToString());
                    info.Special = byte.Parse(reader["special"].ToString());
                    info.Attachment = Int32.Parse(reader["attachment"].ToString());
                    //info.Moderated = Int32.Parse(reader["moderated"].ToString());
                    info.Closed = Int32.Parse(reader["closed"].ToString());
                    //info.Magic = Int32.Parse(reader["magic"].ToString());
                    //����رձ��
                    if (info.Closed == 0)
                    {
                        string oldtopic = ForumUtils.GetCookie("oldtopic") + "D";
                        if (oldtopic.IndexOf("D" + info.Tid.ToString() + "D") == -1 && DateTime.Now.AddMinutes(-1 * newmin) < DateTime.Parse(info.Lastpost))
                        {
                            info.Folder = "new";
                        }
                        else
                        {
                            info.Folder = "old";
                        }


                        if (info.Replies >= hot)
                        {
                            info.Folder += "hot";
                        }

                        if (autoclose > 0)
                        {
                            if (Utils.StrDateDiffHours(info.Postdatetime, autoclose * 24) > 0)
                            {
                                info.Closed = 1;
                                if (closeid.Length > 0)
                                {
                                    closeid.Append(",");
                                }
                                closeid.Append(info.Tid.ToString());
                                info.Folder = "closed";
                            }
                        }


                    }
                    else
                    {
                        info.Folder = "closed";
                        if (info.Closed > 1)
                        {
                            info.Tid = info.Closed;
                            info.Folder = "move";
                        }
                    }

                    if (info.Highlight != "")
                    {
                        info.Title = "<span style=\"" + info.Highlight + "\">" + info.Title + "</span>";
                    }

                    //��չ����
                    if (topictypeprefix > 0 && info.Typeid > 0)
                    {
                        TopicTypeName = TopicTypeArray[info.Typeid];
                        if (TopicTypeName != null && TopicTypeName.ToString().Trim() != "")
                        {
                            info.Topictypename = TopicTypeName.ToString().Trim();
                        }
                    }
                    else
                    {
                        info.Topictypename = "";
                    }
                    //

                    coll.Add(info);
                }
                reader.Close();
                if (closeid.Length > 0)
                {
                    TopicAdmins.SetClose(closeid.ToString(), 1);
                }
            }
            return coll;
        }

        /// <summary>
        /// �Է�������,��������ҳ����ʾ���в�ѯ�ĺ���
        /// </summary>
        /// <param name="pagesize">ÿ����ҳ��������</param>
        /// <param name="pageindex">��ҳҳ��</param>
        /// <param name="startnum">�ö�������</param>
        /// <param name="newmin">������ٷ����ڵ�������Ϊ��������</param>
        /// <param name="hot">���ٸ�������Ϊ����������</param>
        /// <param name="condition">����</param>
        /// <param name="orderby">����</param>
        /// <param name="ascdesc">��/����</param>
        /// <returns>�����б�</returns>
        public static ShowforumPageTopicInfoCollection<ShowforumPageTopicInfo> GetTopicCollectionByTypeDate(int pagesize, int pageindex, int startnum, int newmin, int hot, int autoclose, int topictypeprefix, string condition, string orderby, int ascdesc)
        {
            ShowforumPageTopicInfoCollection<ShowforumPageTopicInfo> coll = new ShowforumPageTopicInfoCollection<ShowforumPageTopicInfo>();

            if (pageindex <= 0)
            {
                return coll;
            }

            IDataReader reader = DatabaseProvider.GetInstance().GetTopicsByTypeDate(pagesize, pageindex, startnum, condition, orderby, ascdesc);
            if (reader != null)
            {

                Discuz.Common.Generic.SortedList<int, object> TopicTypeArray = Caches.GetTopicTypeArray();
                StringBuilder closeid = new StringBuilder();
                object TopicTypeName = null;

                while (reader.Read())
                {
                    ShowforumPageTopicInfo info = new ShowforumPageTopicInfo();
                    info.Tid = Int32.Parse(reader["tid"].ToString());
                    //info.Fid = Int32.Parse(reader["fid"].ToString());
                    info.Iconid = Int32.Parse(reader["iconid"].ToString());
                    info.Title = reader["title"].ToString();
                    info.Typeid = Int32.Parse(reader["typeid"].ToString());
                    info.Readperm = Int32.Parse(reader["readperm"].ToString());
                    info.Price = Int32.Parse(reader["price"].ToString());
                    info.Poster = reader["poster"].ToString();
                    info.Posterid = Int32.Parse(reader["posterid"].ToString());
                    info.Postdatetime = reader["postdatetime"].ToString();
                    info.Lastpost = reader["lastpost"].ToString();
                    info.Lastposter = reader["lastposter"].ToString();
                    info.Lastposterid = Int32.Parse(reader["LastposterID"].ToString());
                    info.Lastpostid = Int32.Parse(reader["LastpostID"].ToString());
                    info.Views = Int32.Parse(reader["views"].ToString());
                    info.Replies = Int32.Parse(reader["replies"].ToString());
                    info.Displayorder = Int32.Parse(reader["displayorder"].ToString());
                    info.Highlight = reader["highlight"].ToString();
                    info.Digest = Int32.Parse(reader["digest"].ToString());
                    info.Rate = Int32.Parse(reader["rate"].ToString());
                    info.Hide = Int32.Parse(reader["hide"].ToString());
                    //info.Poll = Int32.Parse(reader["poll"].ToString());
                    info.Special = byte.Parse(reader["special"].ToString());
                    info.Attachment = Int32.Parse(reader["attachment"].ToString());
                    //info.Moderated = Int32.Parse(reader["moderated"].ToString());
                    info.Closed = Int32.Parse(reader["closed"].ToString());
                    //info.Magic = Int32.Parse(reader["magic"].ToString());
                    //����رձ��
                    if (info.Closed == 0)
                    {
                        string oldtopic = ForumUtils.GetCookie("oldtopic") + "D";
                        if (oldtopic.IndexOf("D" + info.Tid.ToString() + "D") == -1 && DateTime.Now.AddMinutes(-1 * newmin) < DateTime.Parse(info.Lastpost))
                        {
                            info.Folder = "new";
                        }
                        else
                        {
                            info.Folder = "old";
                        }


                        if (info.Replies >= hot)
                        {
                            info.Folder += "hot";
                        }

                        if (autoclose > 0)
                        {
                            if (Utils.StrDateDiffHours(info.Postdatetime, autoclose * 24) > 0)
                            {
                                info.Closed = 1;
                                if (closeid.Length > 0)
                                {
                                    closeid.Append(",");
                                }
                                closeid.Append(info.Tid.ToString());
                                info.Folder = "closed";
                            }
                        }


                    }
                    else
                    {
                        info.Folder = "closed";
                        if (info.Closed > 1)
                        {
                            info.Tid = info.Closed;
                            info.Folder = "move";
                        }
                    }

                    if (info.Highlight != "")
                    {
                        info.Title = "<span style=\"" + info.Highlight + "\">" + info.Title + "</span>";
                    }

                    //��չ����
                    if (topictypeprefix > 0 && info.Typeid > 0)
                    {
                        TopicTypeName = TopicTypeArray[info.Typeid];
                        if (TopicTypeName != null && TopicTypeName.ToString().Trim() != "")
                        {
                            info.Topictypename = TopicTypeName.ToString().Trim();
                        }
                    }
                    else
                    {
                        info.Topictypename = "";
                    }
                    //

                    coll.Add(info);
                }
                reader.Close();
                if (closeid.Length > 0)
                {
                    TopicAdmins.SetClose(closeid.ToString(), 1);
                }
            }
            return coll;
        }



        public class ShowforumPageTopicInfoCollection<T> : Discuz.Common.Generic.List<T> where T : ShowforumPageTopicInfo, new()
        {
            public ShowforumPageTopicInfoCollection() : base() { }

            public ShowforumPageTopicInfoCollection(System.Collections.Generic.IEnumerable<T> collection) : base(collection) { }

            public ShowforumPageTopicInfoCollection(int capacity) : base(capacity) { }

            public void SortAdd(T value)
            {
                if (this.Count <= 0)
                {
                    this.Add(value);
                }
                else
                {
                    for (int i = 0; i < this.Count; i++)
                    {
                        if ((value.Displayorder) > (this[i].Displayorder))
                        {
                            this.Insert(i, value);
                            return ;
                        }
                    }
                    this.Add(value);

                }
           
            }

        }
        
        #endregion

#endif


        public static void UpdateTopic(int tid, string title, int posterid, string poster)
        {
            DatabaseProvider.GetInstance().UpdateTopic(tid, title, posterid, poster);
        }

        /// <summary>
        /// ���htmltitle
        /// </summary>
        /// <param name="htmltitle"></param>
        /// <param name="topicid"></param>
        public static void WriteHtmlTitleFile(string htmltitle, int topicid)
        {
            StringBuilder dir = new StringBuilder();
            dir.Append(BaseConfigs.GetForumPath);
            dir.Append("cache/topic/magic/");

            if (!Directory.Exists(Utils.GetMapPath(dir.ToString())))
            {
                Utils.CreateDir(Utils.GetMapPath(dir.ToString()));
            }

            dir.Append((topicid / 1000 + 1).ToString());
            dir.Append("/");

            if (!Directory.Exists(Utils.GetMapPath(dir.ToString())))
            {
                Utils.CreateDir(Utils.GetMapPath(dir.ToString()));
            }


            string filename = Utils.GetMapPath(dir.ToString() + topicid.ToString() + "_htmltitle.config");
            try
            {
                using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    Byte[] info = System.Text.Encoding.UTF8.GetBytes(Utils.RemoveUnsafeHtml(htmltitle));
                    fs.Write(info, 0, info.Length);
                    fs.Close();
                }

            }
            catch
            {
            }


        }

        /// <summary>
        /// �����Ӧ��magicֵ
        /// </summary>
        /// <param name="magic">���ݿ���magicֵ</param>
        /// <param name="magicType">magic����</param>
        /// <returns></returns>
        public static int GetMagicValue(int magic, MagicType magicType)
        {
            if (magic == 0)
                return 0;
            string m = magic.ToString();
            switch (magicType)
            { 
                case MagicType.HtmlTitle:
                    if (m.Length >= 2)
                        return Convert.ToInt32(m.Substring(1, 1));
                    break;
                case MagicType.MagicTopic:
                    if (m.Length >= 5)
                        return Convert.ToInt32(m.Substring(2, 3));
                    break;
                case MagicType.TopicTag:
                    if (m.Length >= 6)
                        return Convert.ToInt32(m.Substring(5, 1));
                    break;
        
            }
            return 0;

        }

        /// <summary>
        /// ������Ӧ��magicֵ
        /// </summary>
        /// <param name="magic">ԭʼmagicֵ</param>
        /// <param name="magicType"></param>
        /// <param name="bonusstat"></param>
        /// <returns></returns>
        public static int SetMagicValue(int magic, MagicType magicType, int newmagicvalue)
        {
            string[] m = Utils.SplitString(magic.ToString(), "");
            switch (magicType)
            { 
                case MagicType.HtmlTitle:
                    if (m.Length >= 2)
                    {
                        m[1] = newmagicvalue.ToString().Substring(0, 1);
                        return Utils.StrToInt(string.Join("", m), magic);
                    }
                    else
                    {
                        return Utils.StrToInt(string.Format("1{0}", newmagicvalue.ToString().Substring(0,1)), magic);
                    }
                case MagicType.MagicTopic:
                    if (m.Length >= 5)
                    {
                        string[] t = Utils.SplitString(newmagicvalue.ToString().PadLeft(3, '0'), "");
                        m[2] = t[0];
                        m[3] = t[1];
                        m[4] = t[2];
                        return Utils.StrToInt(string.Join("", m), magic);
                    }
                    else
                    {
                        return Utils.StrToInt(string.Format("1{0}{1}", GetMagicValue(magic, MagicType.HtmlTitle), newmagicvalue.ToString().PadLeft(3, '0').Substring(0, 3)), magic);
                    }
                case MagicType.TopicTag:
                    if (m.Length >= 6)
                    {
                        m[5] = newmagicvalue.ToString().Substring(0, 1);
                        return Utils.StrToInt(string.Join("", m), magic);
                    }
                    else
                    {
                        return Utils.StrToInt(string.Format("1{0}{1}{2}", GetMagicValue(magic, MagicType.HtmlTitle), GetMagicValue(magic, MagicType.MagicTopic).ToString("000"), newmagicvalue.ToString().Substring(0, 1)), magic);
                    }
        
            }
            return magic;
        }

        /// <summary>
        /// ��ȡָ�����ӵ�html����
        /// </summary>
        /// <param name="topicid"></param>
        /// <returns></returns>
        public static string GetHtmlTitle(int topicid)
        {
            StringBuilder dir = new StringBuilder();
            dir.Append(BaseConfigs.GetForumPath);
            dir.Append("cache/topic/magic/");
            dir.Append((topicid / 1000 + 1).ToString());
            dir.Append("/");
            string filename = Utils.GetMapPath(dir.ToString() + topicid.ToString() + "_htmltitle.config");
            if (!File.Exists(filename))
                return "";

            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
                {
                    return sr.ReadToEnd();
                }
            }

        }

        /// <summary>
        /// ���������Tag��ȡ�������(�οͿɼ������)
        /// </summary>
        /// <param name="topicid">����Id</param>
        /// <returns></returns>
        public static List<TopicInfo> GetRelatedTopics(int topicid, int count)
        {
            IDataReader reader = DatabaseProvider.GetInstance().GetRelatedTopics(topicid, count);

            List<TopicInfo> topics = new List<TopicInfo>();
            while (reader.Read())
            {
                TopicInfo topic = new TopicInfo();
                topic.Tid = Utils.StrToInt(reader["linktid"], 0);
                topic.Title = reader["linktitle"].ToString();
                topics.Add(topic);
            }

            reader.Close();
            return topics;
        }

        /// <summary>
        /// ��ȡʹ��ͬһtag�������б�
        /// </summary>
        /// <param name="tagid">TagId</param>
        /// <returns></returns>
        public static int GetTopicsCountWithSameTag(int tagid)
        {
            return DatabaseProvider.GetInstance().GetTopicsCountByTag(tagid);
        }

        /// <summary>
        /// ��ȡʹ��ͬһtag�������б�
        /// </summary>
        /// <param name="tagid">TagId</param>
        /// <returns></returns>
        public static List<MyTopicInfo> GetTopicsWithSameTag(int tagid, int count)
        {
            return GetTopicsWithSameTag(tagid, 1, count);
        }

        /// <summary>
        /// ��ȡʹ��ͬһtag�������б�
        /// </summary>
        /// <param name="tagid">TagId</param>
        /// <param name="pageindex">ҳ��</param>
        /// <param name="pagesize">ҳ��С</param>
        /// <returns></returns>
        public static List<MyTopicInfo> GetTopicsWithSameTag(int tagid, int pageindex, int pagesize)
        {
            IDataReader reader = DatabaseProvider.GetInstance().GetTopicListByTag(tagid, pageindex, pagesize);

            List<MyTopicInfo> topics = new List<MyTopicInfo>();

            while (reader.Read())
            {
                MyTopicInfo topic = new MyTopicInfo();
                topic.Tid = Utils.StrToInt(reader["tid"], 0);
                topic.Title = reader["title"].ToString();
                topic.Poster = reader["poster"].ToString();
                topic.Posterid = Utils.StrToInt(reader["posterid"], -1);
                topic.Fid = Utils.StrToInt(reader["fid"], 0);
                topic.Forumname = Forums.GetForumInfo(Utils.StrToInt(reader["fid"], 0)).Name;
                topic.Postdatetime = reader["postdatetime"].ToString();
                topic.Replies = Utils.StrToInt(reader["replies"], 0);
                topic.Views = Utils.StrToInt(reader["views"], 0);
                topic.Lastposter = reader["lastposter"].ToString();
                topic.Lastposterid = Utils.StrToInt(reader["lastposterid"], -1);
                topic.Lastpost = reader["lastpost"].ToString();
                topics.Add(topic);
            }

            reader.Close();

            return topics;
        }

        /// <summary>
        /// ������������
        /// </summary>
        public static void NeatenRelateTopics()
        {
            DatabaseProvider.GetInstance().NeatenRelateTopics();
        }
        /// <summary>
        /// ɾ���������������¼
        /// </summary>
        /// <param name="topicid"></param>
        public static void DeleteRelatedTopics(int topicid)
        {
            DatabaseProvider.GetInstance().DeleteRelatedTopics(topicid);
        }

        /// <summary>
        /// �޸�ָ�������MagicΪָ��ֵ
        /// </summary>
        /// <param name="tid">����Id</param>
        /// <param name="magic">Magicֵ</param>
        public static void UpdateMagicValue(int tid, int magic)
        {
            DatabaseProvider.GetInstance().UpdateMagicValue(tid, magic);
        }

        /// <summary>
        /// ���ӱ���������չ��Ϣ
        /// </summary>
        /// <param name="debatetopic"></param>
        public static void AddDebateTopic(DebateInfo debatetopic)
        {
            //debatetopic = ReviseDebateTopicColor(debatetopic);
            DatabaseProvider.GetInstance().AddDebateTopic(debatetopic);
        }

        /// <summary>
        /// �����������ͱ�ʶ�ֶ�
        /// </summary>
        /// <param name="topicinfo"></param>
        public static void UpdateSpecial(TopicInfo topicinfo)
        {
            DatabaseProvider.GetInstance().UpdateTopicSpecial(topicinfo.Tid, topicinfo.Special);
        }

        /// <summary>
        /// �����������ɫΪ6λ16���ƵĺϷ���ɫֵ��
        /// </summary>
        /// <param name="debatetopic"></param>
        /// <returns></returns>
        //private static DebateInfo ReviseDebateTopicColor(DebateInfo debatetopic)
        //{
        //    if (Utils.CheckColorValue(debatetopic.Positivecolor))
        //    {
        //        debatetopic.Positivecolor = debatetopic.Positivecolor.Trim().Trim('#');
        //    }
        //    else
        //    {
        //        debatetopic.Positivecolor = GeneralConfigs.GetConfig().Defaultpositivecolor;
        //    }

        //    if (Utils.CheckColorValue(debatetopic.Negativecolor))
        //    {
        //        debatetopic.Negativecolor = debatetopic.Negativecolor.Trim().Trim('#');
        //    }
        //    else
        //    {
        //        debatetopic.Negativecolor = GeneralConfigs.GetConfig().Defaultnegativecolor;
        //    }

        //    if (Utils.CheckColorValue(debatetopic.Positivebordercolor))
        //    {
        //        debatetopic.Positivebordercolor = debatetopic.Positivebordercolor.Trim().Trim('#');
        //    }
        //    else
        //    {
        //        debatetopic.Positivebordercolor = GeneralConfigs.GetConfig().Defaultpositivebordercolor;
        //    }

        //    if (Utils.CheckColorValue(debatetopic.Negativebordercolor))
        //    {
        //        debatetopic.Negativebordercolor = debatetopic.Negativebordercolor.Trim().Trim('#');
        //    }
        //    else
        //    {
        //        debatetopic.Negativebordercolor = GeneralConfigs.GetConfig().Defaultnegativebordercolor;
        //    }

        //    return debatetopic;
        //}
    }
}
