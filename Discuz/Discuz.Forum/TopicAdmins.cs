using System;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Text;

using Discuz.Common;
using Discuz.Data;
using Discuz.Config;
using Discuz.Entity;
using System.Collections;

namespace Discuz.Forum
{
    /// <summary>
    /// ������������
    /// </summary>
    public class TopicAdmins
    {
        /// <summary>
        /// ��������ָ���ֶε�����ֵ
        /// </summary>
        /// <param name="topiclist">Ҫ���õ������б�</param>
        /// <param name="field">Ҫ���õ��ֶ�</param>
        /// <param name="intValue">����ֵ</param>
        /// <returns>�����������</returns>
        private static int SetTopicStatus(string topiclist, string field, int intValue)
        {
            if (!Utils.InArray(field.ToLower().Trim(), "displayorder,highlight,digest"))
            {
                return -1;
            }

            if (!Utils.IsNumericArray(topiclist.Split(',')))
            {
                return -1;
            }

            return DatabaseProvider.GetInstance().SetTopicStatus(topiclist, field, intValue);
        }



        /// <summary>
        /// ��������ָ���ֶε�����ֵ
        /// </summary>
        /// <param name="topiclist">Ҫ���õ������б�</param>
        /// <param name="field">Ҫ���õ��ֶ�</param>
        /// <param name="intValue">����ֵ</param>
        /// <returns>�����������</returns>
        private static int SetTopicStatus(string topiclist, string field, byte intValue)
        {
            if (!Utils.InArray(field.ToLower().Trim(), "displayorder,highlight,digest"))
            {
                return -1;
            }

            if (!Utils.IsNumericArray(topiclist.Split(',')))
            {
                return -1;
            }

            return DatabaseProvider.GetInstance().SetTopicStatus(topiclist, field, intValue);
        }



        /// <summary>
        /// ��������ָ���ֶε�����ֵ(�ַ���)
        /// </summary>
        /// <param name="topiclist">Ҫ���õ������б�</param>
        /// <param name="field">Ҫ���õ��ֶ�</param>
        /// <param name="intValue">����ֵ</param>
        /// <returns>�����������</returns>
        private static int SetTopicStatus(string topiclist, string field, string intValue)
        {
            if ((",displayorder,highlight,digest,").IndexOf("," + field.ToLower().Trim() + ",") < 0)
            {
                return -1;
            }

            if (!Utils.IsNumericArray(topiclist.Split(',')))
            {
                return -1;
            }

            return DatabaseProvider.GetInstance().SetTopicStatus(topiclist, field, intValue);
        }


        /// <summary>
        /// �������ö�/����ö�
        /// </summary>
        /// <param name="topiclist">Ҫ���õ������б�</param>
        /// <param name="intValue">�ö�����( 0 Ϊ����ö�)</param>
        /// <returns>�����������</returns>
        public static int SetTopTopicList(int fid, string topiclist, short intValue)
        {
            if (SetTopicStatus(topiclist, "displayorder", intValue) > 0)
            {
                if (ResetTopTopicList(fid) == 1)
                {
                    return 1;
                }
            }
            if (Utils.FileExists(Utils.GetMapPath(BaseConfigs.GetForumPath + "cache/topic/" + fid.ToString() + ".xml")))
            {
                File.Delete(Utils.GetMapPath(BaseConfigs.GetForumPath + "cache/topic/" + fid.ToString() + ".xml"));
            }
            return -1;
        }

        /// <summary>
        /// ���������ö�����
        /// </summary>
        /// <param name="fid">����ID</param>
        /// <param name="intValue"></param>
        /// <returns></returns>
        public static int ResetTopTopicList(int fid)
        {
            DataSet ds = DatabaseProvider.GetInstance().GetTopTopicList(fid);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable topTable = DatabaseProvider.GetInstance().GetShortForums();
                    int[] fidIndex = null;
                    if (topTable != null)
                    {
                        if (topTable.Rows.Count > 0)
                        {
                            fidIndex = new int[Utils.StrToInt(topTable.Rows[0]["fid"], 0) + 1];
                            int index = 0;
                            foreach (DataRow dr in topTable.Rows)
                            {
                                fidIndex[Utils.StrToInt(dr["fid"], 0)] = index;
                                index++;
                            }
                        }
                    }

                    ds.DataSetName = "topics";
                    ds.Tables[0].TableName = "topic";
                    int tidCount = 0;
                    int tid0Count = 0;
                    int tid1Count = 0;
                    int tid2Count = 0;
                    int tid3Count = 0;

                    StringBuilder sbTop3 = new StringBuilder();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if (Utils.StrToInt(dr["displayorder"], 0) == 3)
                        {
                            if (sbTop3.Length > 0)
                            {
                                sbTop3.Append(",");
                            }

                            if (fidIndex != null && fidIndex.Length >= Utils.StrToInt(dr["fid"], 0))
                            {
                                sbTop3.Append(dr["tid"]);
                                tidCount++;
                                topTable.Rows[fidIndex[Utils.StrToInt(dr["fid"], 0)]]["tid3count"] = Utils.StrToInt(topTable.Rows[fidIndex[Utils.StrToInt(dr["fid"], 0)]]["tid3count"], 0) + 1;
                            }

                        }
                        else
                        {
                            if (fidIndex != null && fidIndex.Length >= Utils.StrToInt(dr["fid"], 0))
                            {
                                if (Utils.StrToInt(dr["displayorder"], 0) != 2)
                                {
                                    topTable.Rows[fidIndex[Utils.StrToInt(dr["fid"], 0)]]["tidlist"] = topTable.Rows[fidIndex[Utils.StrToInt(dr["fid"], 0)]]["tidlist"].ToString() + "," + dr["tid"].ToString();
                                    topTable.Rows[fidIndex[Utils.StrToInt(dr["fid"], 0)]]["tidcount"] = Utils.StrToInt(topTable.Rows[fidIndex[Utils.StrToInt(dr["fid"], 0)]]["tidcount"], 0) + 1;
                                }
                                else
                                {
                                    topTable.Rows[fidIndex[Utils.StrToInt(dr["fid"], 0)]]["tid2list"] = topTable.Rows[fidIndex[Utils.StrToInt(dr["fid"], 0)]]["tid2list"].ToString() + "," + dr["tid"].ToString();
                                    topTable.Rows[fidIndex[Utils.StrToInt(dr["fid"], 0)]]["tid2count"] = Utils.StrToInt(topTable.Rows[fidIndex[Utils.StrToInt(dr["fid"], 0)]]["tid2count"], 0) + 1;
                                }
                            }
                        }
                    }

                    if (topTable != null)
                    {
                        if (topTable.Rows.Count > 0)
                        {
                            foreach (DataRow dr in topTable.Rows)
                            {
                                dr["temptidlist"] = sbTop3.ToString() + dr["tidlist"].ToString() + dr["tid2list"].ToString();

                                tid1Count = Utils.StrToInt(dr["tidcount"], 0);
                                tid2Count = Utils.StrToInt(dr["tid2count"], 0);
                                tid3Count = Utils.StrToInt(dr["tid3count"], 0);

                                tid0Count = tid1Count + tid2Count + tid3Count;

                                dr["tidcount"] = tid1Count + tidCount + Utils.StrToInt(dr["tid2count"], 0);

                                string filterexpress = DatabaseProvider.GetInstance().ResetTopTopicListSql(Utils.StrToInt(dr["layer"], 0), dr["fid"].ToString().Trim(), dr["parentidlist"].ToString().Trim());

                                //switch (Utils.StrToInt(dr["layer"], 0))
                                //{
                                //    case 0:
                                //        filterexpress = "[fid]<>" + dr["fid"].ToString().Trim() + " AND (',' + TRIM([parentidlist]) + ',' LIKE '%," + dr["fid"].ToString().Trim() + ",%')";
                                //        break;
                                //    case 1:
                                //        filterexpress = dr["parentidlist"].ToString().Trim();
                                //        filterexpress = "[fid]<>" + dr["fid"].ToString().Trim() + " AND ([fid]=" + filterexpress + " OR (',' + TRIM([parentidlist]) + ',' LIKE '%," + dr["parentidlist"].ToString().Trim() + ",%'))";
                                //        break;
                                //    default:
                                //        filterexpress = dr["parentidlist"].ToString().Trim();
                                //        filterexpress = Utils.CutString(filterexpress, 0, filterexpress.IndexOf(","));
                                //        filterexpress = "[fid]<>" + dr["fid"].ToString().Trim() + " AND ([fid]=" + filterexpress + " OR (',' + TRIM([parentidlist]) + ',' LIKE '%," + filterexpress + ",%'))";
                                //        break;
                                //}

                                foreach (DataRow drTemp in topTable.Select(filterexpress))
                                {
                                    if (!drTemp["tid2list"].ToString().Equals(""))
                                    {
                                        dr["temptidlist"] = dr["temptidlist"].ToString() + drTemp["tid2list"].ToString();
                                        dr["tidcount"] = Utils.StrToInt(drTemp["tid2count"], 0) + Utils.StrToInt(dr["tidcount"], 0);
                                        tid2Count = tid2Count + Utils.StrToInt(drTemp["tid2count"], 0);
                                    }
                                }

                                tid0Count = Utils.StrToInt(dr["tidcount"], 0) - tid0Count;

                                if (ds.Tables.Count == 1)
                                {
                                    ds.Tables.Add();
                                    ds.Tables[1].Columns.Add("tid", Type.GetType("System.String"));
                                    ds.Tables[1].Columns.Add("tidCount", Type.GetType("System.Int32"));
                                    ds.Tables[1].Columns.Add("tid0Count", Type.GetType("System.Int32"));
                                    ds.Tables[1].Columns.Add("tid1Count", Type.GetType("System.Int32"));
                                    ds.Tables[1].Columns.Add("tid2Count", Type.GetType("System.Int32"));
                                    ds.Tables[1].Columns.Add("tid3Count", Type.GetType("System.Int32"));
                                    ds.Tables[1].TableName = "fidtopic";
                                    DataRow dr1 = ds.Tables[1].NewRow();
                                    dr1["tid"] = dr["temptidlist"];
                                    dr1["tidCount"] = dr["tidcount"];
                                    dr1["tid0Count"] = tid0Count;
                                    dr1["tid1Count"] = tid1Count;
                                    dr1["tid2Count"] = tid2Count;
                                    dr1["tid3Count"] = tidCount;
                                    ds.Tables[1].Rows.Add(dr1);
                                }
                                else
                                {
                                    ds.Tables[1].Rows[0]["tid"] = dr["temptidlist"];
                                    ds.Tables[1].Rows[0]["tidCount"] = dr["tidcount"];
                                    ds.Tables[1].Rows[0]["tid0Count"] = tid0Count;
                                    ds.Tables[1].Rows[0]["tid1Count"] = tid1Count;
                                    ds.Tables[1].Rows[0]["tid2Count"] = tid2Count;
                                    ds.Tables[1].Rows[0]["tid3Count"] = tidCount;
                                }

                                DataSet tempDS = new DataSet();
                                tempDS.Tables.Add(ds.Tables[1].Copy());
                                tempDS.DataSetName = "topics";
                                tempDS.Tables[0].TableName = "topic";
                                if (!Directory.Exists(Utils.GetMapPath(BaseConfigs.GetForumPath + "cache/topic/")))
                                    Directory.CreateDirectory(Utils.GetMapPath(BaseConfigs.GetForumPath + "cache/topic/"));
                                tempDS.WriteXml(@Utils.GetMapPath(BaseConfigs.GetForumPath + "cache/topic/" + dr["fid"].ToString() + ".xml"), XmlWriteMode.WriteSchema);
                                tempDS.Clear();
                                tempDS.Dispose();

                                //
                                //									'Ҫ�ĵ�.
                                //
                                //
                                //									ds.Tables.Remove("topic");
                                //									
                                //									ds.Tables[0].TableName="topic";
                                //									ds.WriteXml(@Utils.GetMapPath(BaseConfigFactory.GetForumPath + "topic/" + fid.ToString() + ".xml"),System.Data.XmlWriteMode.WriteSchema);
                            }
                        }
                    }


                    topTable.Dispose();
                    ds.Clear();
                    ds.Dispose();
                    return 1;
                }
            }
            return 0;
        }
        /// <summary>
        /// �����������ʾ
        /// </summary>
        /// <param name="topiclist">Ҫ���õ������б�</param>
        /// <param name="intValue">������ʽ����ɫ( 0 Ϊ���������ʾ)</param>
        /// <returns>�����������</returns>
        public static int SetHighlight(string topiclist, string intValue)
        {
            return SetTopicStatus(topiclist, "highlight", intValue);
        }

        /// <summary>
        /// ���ݵõ�����������û��б�
        /// </summary>
        /// <param name="topiclist">�����б�</param>
        /// <param name="op">����Դ(0:����,1:ɾ��)</param>
        /// <returns>�û��б�</returns>
        private static string GetUserListWithTopiclist(string topiclist, int op)
        {
            if (!Utils.IsNumericArray(topiclist.Split(',')))
            {
                return "";
            }
            StringBuilder useridlist = new StringBuilder();

            GeneralConfigInfo configinfo = GeneralConfigs.GetConfig();
            IDataReader reader = null;
            if (op == 1)
            {
                if (configinfo.Losslessdel != 0)
                {
                    reader = DatabaseProvider.GetInstance().GetUserListWithTopicList(topiclist, configinfo.Losslessdel);
                }
                else
                {
                    reader = DatabaseProvider.GetInstance().GetUserListWithTopicList(topiclist);
                }
            }
            else
            {
                reader = DatabaseProvider.GetInstance().GetUserListWithTopicList(topiclist);
            }

            if (reader != null)
            {
                while (reader.Read())
                {
                    if (!useridlist.ToString().Equals(""))
                    {
                        useridlist.Append(",");
                    }
                    useridlist.Append(reader["posterid"].ToString());

                }
                reader.Close();
            }
            return useridlist.ToString();

        }



        /// <summary>
        /// ���������þ���/�������
        /// </summary>
        /// <param name="topiclist">Ҫ���õ������б�</param>
        /// <param name="intValue">��������( 0 Ϊ�������)</param>
        /// <returns>�����������</returns>
        public static int SetDigest(string topiclist, short intValue)
        {
            int mount = SetTopicStatus(topiclist, "digest", intValue);
            string useridlist = GetUserListWithTopiclist(topiclist, 0);
            if (mount > 0)
            {
                Users.UpdateUserDigest(useridlist);
                if (intValue > 0 && !useridlist.Equals(""))
                {
                    UserCredits.UpdateUserCreditsByDigest(useridlist, mount);
                }
            }
            return mount;
        }

        /// <summary>
        /// ���������ùر�/��
        /// </summary>
        /// <param name="topiclist">Ҫ���õ������б�</param>
        /// <param name="intValue">�ر�/�򿪱�־( 0 Ϊ��,1 Ϊ�ر�)</param>
        /// <returns>�����������</returns>
        public static int SetClose(string topiclist, short intValue)
        {
            if (!Utils.IsNumericArray(topiclist.Split(',')))
            {
                return -1;
            }

            return DatabaseProvider.GetInstance().SetTopicClose(topiclist, intValue);
        }



        /// <summary>
        /// �������ָ���ֶε�����ֵ
        /// </summary>
        /// <param name="topiclist">�����б�</param>
        /// <param name="field">Ҫ���ֵ���ֶ�</param>
        /// <returns>����ָ���ֶε�״̬</returns>
        public static int GetTopicStatus(string topiclist, string field)
        {
            if ((",displayorder,digest,").IndexOf("," + field.ToLower().Trim() + ",") < 0)
            {
                return -1;
            }

            if (!Utils.IsNumericArray(topiclist.Split(',')))
            {
                return -1;
            }

            return DatabaseProvider.GetInstance().GetTopicStatus(topiclist, field);
        }




        /// <summary>
        /// ��������ö�״̬
        /// </summary>
        /// <param name="topiclist">�����б�</param>
        /// <returns>�ö�״̬(�������ⷵ����ʵ״̬,������ⷵ��״ֵ̬�ۼ�)</returns>
        public static int GetDisplayorder(string topiclist)
        {
            return GetTopicStatus(topiclist, "displayorder");
        }

        /// <summary>
        /// ��ø�����ʽ����ɫ
        /// </summary>
        /// <param name="topiclist">�����б�</param>
        /// <returns>������ʽ����ɫ</returns>
        public static int GetHighlight(string topiclist)
        {
            return 0;
        }


        /// <summary>
        /// ������⾫��״̬
        /// </summary>
        /// <param name="topiclist">�����б�</param>
        /// <returns>����״̬(�������ⷵ����ʵ״̬,������ⷵ��״ֵ̬�ۼ�)</returns>
        public static int GetDigest(string topiclist)
        {
            return GetTopicStatus(topiclist, "digest");
        }


        //		/// <summary>
        //		/// �������ر�״̬
        //		/// </summary>
        //		/// <param name="topiclist">�����б�</param>
        //		/// <returns>����״̬(�������ⷵ����ʵ״̬,������ⷵ��״ֵ̬�ۼ�)</returns>
        //		public static int GetClose(string topiclist)
        //		{
        //			return GetTopicStatus(topiclist,"closed");
        //		}


        /// <summary>
        /// �����ݿ���ɾ��ָ������
        /// </summary>
        /// <param name="topiclist">�����б�</param>
        /// <param name="subtractCredits">�Ƿ�����û�����(0������,1����)</param>
        /// <returns>ɾ������</returns>
        public static int DeleteTopics(string topiclist, int subtractCredits, bool reserveAttach)
        {
            if (!Utils.IsNumericArray(topiclist.Split(',')))
            {
                return -1;
            }


            GeneralConfigInfo configinfo = GeneralConfigs.GetConfig();


            DataTable dt = Topics.GetTopicList(topiclist);
            if (dt == null)
            {
                return -1;
            }
            foreach (DataRow dr in dt.Rows)
            {
                if (Utils.StrToInt(dr["digest"], 0) > 0)
                {
                    UserCredits.UpdateUserCreditsByDigest(Utils.StrToInt(dr["posterid"], 0), -1);
                }

            }


            dt = Posts.GetPostList(topiclist);
            if (dt != null)
            {
                int i = 0;
                int[] tuidlist = new int[dt.Rows.Count];
                int[] auidlist = new int[dt.Rows.Count];
                //int fid = -1;
                foreach (DataRow dr in dt.Rows)
                {
                    //fid = Utils.StrToInt(dr["fid"].ToString(), -1);
                    if (Utils.StrDateDiffHours(dr["postdatetime"].ToString(), configinfo.Losslessdel * 24) < 0)
                    {
                        if (Utils.StrToInt(dr["layer"], 0) == 0)
                        {
                            tuidlist[i] = Utils.StrToInt(dr["posterid"], 0);
                        }
                        else
                        {
                            auidlist[i] = Attachments.GetAttachmentCountByPid(Utils.StrToInt(dr["pid"], 0));
                        }
                    }
                    else
                    {
                        tuidlist[i] = 0;
                        auidlist[i] = 0;
                    }
                }
                //DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE [" + BaseConfigFactory.GetTablePrefix + "forums] SET [topics]=[topics]-" + topiclist.Split(',').Length.ToString() + ", [posts]=[posts]-" + dt.Rows.Count.ToString() + " WHERE [fid]=" + fid.ToString());
                UserCredits.UpdateUserCreditsByDeleteTopic(tuidlist, auidlist, -1);

            }

            int reval = 0;
            foreach (string posttableid in Posts.GetPostTableIDList(topiclist))
            {
                reval = DatabaseProvider.GetInstance().DeleteTopicByTidList(topiclist, posttableid,true);
            }
            if (reval > 0 && !reserveAttach)
            {
                Attachments.DeleteAttachmentByTid(topiclist);
            }
            return reval;


        }


        public static int DeleteTopicsWithoutChangingCredits(string topiclist, bool reserveAttach)
        {
            if (!Utils.IsNumericArray(topiclist.Split(',')))
            {
                return -1;
            }
            int reval = -1;
            foreach (string posttableid in Posts.GetPostTableIDList(topiclist))
            {
                reval = DatabaseProvider.GetInstance().DeleteTopicByTidList(topiclist, posttableid,false);
            }
            if (reval > 0 && !reserveAttach)
            {
                Attachments.DeleteAttachmentByTid(topiclist);
            }
            return reval;
        }

        /// <summary>
        /// �����ݿ���ɾ��ָ������
        /// </summary>
        /// <param name="topiclist">�����б�</param>
        /// <returns>ɾ������</returns>
        public static int DeleteTopics(string topiclist, bool reserveAttach)
        {
            return DeleteTopics(topiclist, 1, reserveAttach);
        }

        /// <summary>
        /// ��ɾ��ָ��������
        /// </summary>
        /// <param name="topiclist">�����б�</param>
        /// <param name="toDustbin">ָ������ɾ����ʽ(0��ֱ�Ӵ����ݿ���ɾ��,��ɾ����֮��������Ϣ  1��ֻ�������̳�б���ɾ��(��displayorder�ֶ���Ϊ-1)����������վ��</param>
        /// <returns>ɾ������</returns>
        public static int DeleteTopics(string topiclist, byte toDustbin, bool reserveAttach)
        {
            return toDustbin == 0 ? DeleteTopics(topiclist, reserveAttach) : SetTopicStatus(topiclist, "displayorder", -1);
        }


        /// <summary>
        /// �ָ�����վ�е����⡣
        /// </summary>
        /// <param name="topiclist">�����б�</param>
        /// <returns>�ָ�����</returns>
        public static int RestoreTopics(string topiclist)
        {
            return SetTopicStatus(topiclist, "displayorder", 0);
        }


        /// <summary>
        /// �ƶ����⵽ָ�����
        /// </summary>
        /// <param name="topiclist">Ҫ�ƶ��������б�</param>
        /// <param name="fid">ת���İ��ID</param>
        /// <param name="savelink">�Ƿ���ԭ��鱣������</param>
        /// <returns>���¼�¼��</returns>
        public static int MoveTopics(string topiclist, int fid, int oldfid, bool savelink)
        {
            if (topiclist.Trim() == "")
            {
                return -1;
            }
            string[] tidlist = Utils.SplitString(topiclist, ",");
            int intDelTidCount = 0;
            //int intMovePostCount = 0;
            foreach (string tid in tidlist)
            {
                if (!Utils.IsNumeric(tid))
                {
                    return -1;
                }
            }
            intDelTidCount += DatabaseProvider.GetInstance().DeleteClosedTopics(fid, topiclist);


            //ת������
            //
            MoveTopics(topiclist, fid, oldfid);

            //���������������һ����¼��ԭ���
            if (savelink)
            {
                if (DatabaseProvider.GetInstance().CopyTopicLink(oldfid, topiclist) <= 0)
                {
                    return -2;
                }

                AdminForumStats.ReSetFourmTopicAPost(oldfid);
                Forums.SetRealCurrentTopics(oldfid);

                //				intMovePostCount = tidlist.Length + Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, "SELECT SUM([replies]) AS [totalreplies] FROM [" + BaseConfigFactory.GetTablePrefix + "topics] WHERE [tid] IN (" + topiclist + ")"), 0);

                //				int todaypost = 0;

                //				foreach (string tid in PostFactory.GetPostTableIDList(topiclist))
                //				{
                //					string posttable = PostFactory.GetPostTableName(Utils.StrToInt(tid, 0));
                //					todaypost = todaypost + Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, "SELECT COUNT([pid]) c FROM [" + posttable + "] WHERE [tid] IN (" + topiclist + ") AND DATEDIFF(day,postdatetime,getdate())=0"), 0);
                //
                //					///2.��ԭ��������Ӧ������ת���������¡�
                //					//sql = "UPDATE [" + posttable + "] SET [tid]=(SELECT [tid] AS [c] FROM [" + BaseConfigFactory.GetTablePrefix + "topics] [d] WHERE [d].[closed]=[" + posttable + "].[tid]) WHERE [tid] IN (" + topiclist + ")";
                //					//DbHelper.ExecuteNonQuery(CommandType.Text, sql);
                //				}


                ///������̳����������
                //				StringBuilder sb = new StringBuilder();
                //				sb.Append("UPDATE [" + BaseConfigFactory.GetTablePrefix + "forums] SET [topics]=[topics]-");
                //				sb.Append(tidlist.Length.ToString());
                //				sb.Append(",[posts]=[posts]-");
                //				sb.Append(intMovePostCount.ToString());
                //				sb.Append(",[todayposts]=[todayposts]-");
                //				sb.Append(todaypost.ToString());
                //				sb.Append("WHERE [fid] =");
                //				sb.Append(tidlist.Length.ToString());
                //
                //
                //				///������̳����������

                //				sb.Append(";UPDATE [" + BaseConfigFactory.GetTablePrefix + "forums] SET [topics]=[topics]+");
                //				sb.Append((tidlist.Length - intDelTidCount).ToString());
                //				sb.Append(",[posts]=[posts]+");
                //				sb.Append(intMovePostCount.ToString());
                //				sb.Append(",[todayposts]=[todayposts]+");
                //				sb.Append(todaypost.ToString());
                //				sb.Append("WHERE [fid] =");
                //				sb.Append(fid.ToString());


                //DbHelper.ExecuteNonQuery(CommandType.Text, sql);

                //sb.Append(";UPDATE [" + BaseConfigFactory.GetTablePrefix + "polls] SET [tid]=(SELECT [tid] AS [c] FROM [" + BaseConfigFactory.GetTablePrefix + "topics] WHERE [" + BaseConfigFactory.GetTablePrefix + "topics].[closed]=[" + BaseConfigFactory.GetTablePrefix + "polls].[tid]) WHERE [tid] IN (" + topiclist + ")");
                ///3.��ԭ��¼��closed��Ϊ�¿�����¼��tid,���¼�¼��closedȷ��
                //sb.Append(";UPDATE [" + BaseConfigFactory.GetTablePrefix + "topics] SET [closed]=(SELECT [tid] AS [c] FROM [" + BaseConfigFactory.GetTablePrefix + "topics] [d] WHERE [d].[closed]=[" + BaseConfigFactory.GetTablePrefix + "topics].[tid]) WHERE [tid] IN (" + topiclist + ")");
                //DbHelper.ExecuteNonQuery(CommandType.Text, sql);


                ///4.���µļ�¼��closed��ֵ����Ϊ 0
                //sb.Append(";UPDATE [" + BaseConfigFactory.GetTablePrefix + "topics] SET [closed]=0 WHERE [fid]=@fid AND [closed] IN (" + topiclist + ")");

                //��ԭ����fid��Ϊ�µ�fid
                //				sb.Append(";UPDATE [" + BaseConfigFactory.GetTablePrefix + "topics] SET [fid]=@fid WHERE [tid] IN (" + topiclist + ")");
                //
                //
                //				DbHelper.ExecuteNonQuery(CommandType.Text, sb.ToString(),  parms);
                //

            }
            //
            //			else
            //			{
            //				///�û�ִ������ֱ��ת��
            //				return MoveTopics(topiclist, fid, oldfid);
            //
            //			}
            return 1;
        }

        /// <summary>
        /// �ƶ����⵽ָ�����
        /// </summary>
        /// <param name="topiclist">Ҫ�ƶ��������б�</param>
        /// <param name="fid">ת���İ��ID</param>
        /// <returns>���¼�¼��</returns>
        public static int MoveTopics(string topiclist, int fid, int oldfid)
        {
            if (!Utils.IsNumericArray(topiclist.Split(',')))
            {
                return -1;
            }
            //��������
            foreach (string tid in topiclist.Split(','))
            {
                string posttable = Posts.GetPostTableName(Utils.StrToInt(tid, 0));
                DatabaseProvider.GetInstance().UpdatePost(topiclist, fid, posttable);
            }

            //��������
            int reval = DatabaseProvider.GetInstance().UpdateTopic(topiclist, fid);
            if (reval > 0)
            {
                AdminForumStats.ReSetFourmTopicAPost(fid);
                AdminForumStats.ReSetFourmTopicAPost(oldfid);
                Forums.SetRealCurrentTopics(fid);
                Forums.SetRealCurrentTopics(oldfid);

            }

            //�����ö���
            ResetTopTopicList(fid);
            ResetTopTopicList(oldfid);
            return reval;
        }



        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="topiclist">����id�б�</param>
        /// <param name="fid">Ŀ����id</param>
        /// <returns>���¼�¼��</returns>
        public static int CopyTopics(string topiclist, int fid)
        {
            int tid;
            if (!Utils.IsNumericArray(topiclist.Split(',')))
            {
                return -1;
            }

            int reval = 0;
            TopicInfo topicinfo = null;
            foreach (string topicid in topiclist.Split(','))
            {
                topicinfo = Topics.GetTopicInfo(Utils.StrToInt(topicid, 0));
                if (topicinfo != null)
                {
                    topicinfo.Fid = fid;
                    topicinfo.Readperm = 0;
                    topicinfo.Price = 0;
                    topicinfo.Postdatetime = Utils.GetDateTime();
                    topicinfo.Lastpost = Utils.GetDateTime();
                    topicinfo.Lastposter = Utils.GetDateTime();
                    topicinfo.Views = 0;
                    topicinfo.Replies = 0;
                    topicinfo.Displayorder = 0;
                    topicinfo.Highlight = "";
                    topicinfo.Digest = 0;
                    topicinfo.Rate = 0;
                    topicinfo.Hide = 0;
                    topicinfo.Special = 0;
                    topicinfo.Attachment = 0;
                    topicinfo.Moderated = 0;
                    topicinfo.Closed = 0;
                    tid = Topics.CreateTopic(topicinfo);
                    if (tid > 0)
                    {
                        PostInfo postinfo = Posts.GetPostInfo(tid, Posts.GetFirstPostId(Utils.StrToInt(topicid, 0)));
                        postinfo.Fid = topicinfo.Fid;
                        postinfo.Tid = tid;
                        postinfo.Parentid = 0;
                        postinfo.Layer = 0;
                        postinfo.Postdatetime = Utils.GetDateTime();
                        postinfo.Invisible = 0;
                        postinfo.Attachment = 0;
                        postinfo.Rate = 0;
                        postinfo.Ratetimes = 0;
                        postinfo.Message = UBB.ClearAttachUBB(postinfo.Message);
                        postinfo.Topictitle = topicinfo.Title;
                        int postid = Posts.CreatePost(postinfo);
                        if (postid > 0)
                        {
                            reval++;
                        }
                    }
                }
            }

            return reval;
        }


        /// <summary>
        /// �ָ�����
        /// </summary>
        /// <param name="postidlist">����id�б�</param>
        /// <param name="subject">����</param>
        /// <param name="topicidlist">����id�б�</param>
        /// <returns>���¼�¼��</returns>
        public static int SplitTopics(string postidlist, string subject, string topicidlist)
        {
            int tid = 0;
            //��֤Ҫ�ָ�������Ƿ�Ϊ��ЧPID��
            string[] postid = postidlist.Split(',');
            if (postid.Length <= 0)
            {
                return -1;
            }
            else
            {
                if (!Utils.IsNumericArray(postid))
                {
                    return -1;
                }
            }



            int topicid = Utils.StrToInt(topicidlist, 0); //��Ҫ���ָ������tid	
            TopicInfo topicinfo = Topics.GetTopicInfo(topicid);
            PostInfo postinfo = Posts.GetPostInfo(topicid, Utils.StrToInt(postid[postid.Length - 1], 0));

            PostInfo firstpostinfo = Posts.GetPostInfo(topicid, Utils.StrToInt(postid[0], 0));

            topicinfo.Poster = firstpostinfo.Poster;
            topicinfo.Posterid = firstpostinfo.Posterid;
            topicinfo.Postdatetime = Utils.GetDateTime();
            topicinfo.Displayorder = 0;
            topicinfo.Highlight = "";
            topicinfo.Digest = 0;
            topicinfo.Rate = 0;
            topicinfo.Hide = 0;
            topicinfo.Special = 0;
            topicinfo.Attachment = 0;
            topicinfo.Moderated = 0;
            topicinfo.Closed = 0;
            topicinfo.Views = 0;
            if (topicinfo.Lastpostid != Utils.StrToInt(postid[postid.Length - 1], 0))
            {


                topicinfo.Lastpostid = Utils.StrToInt(postid[postid.Length - 1], 0);
                topicinfo.Lastposterid = postinfo.Posterid;
                topicinfo.Lastpost = postinfo.Postdatetime;
                topicinfo.Lastposter = postinfo.Poster;

                topicinfo.Replies = postid.Length - 1;

                topicinfo.Title = Utils.HtmlEncode(subject);
                tid = Topics.CreateTopic(topicinfo);
                DatabaseProvider.GetInstance().UpdatePostTid(postidlist, tid, Posts.GetPostTableID(tid));
                DatabaseProvider.GetInstance().SetPrimaryPost(subject, tid, postid, Posts.GetPostTableID(tid));


            }

            else
            {
                string list = "";
                //DbHelper.ExecuteDataset(CommandType.Text, "select * from dnt_posts1 where pid not in (" + postidlist + ") and tid=" + topicid + " order by pid desc").Tables[0];

                DataTable dt = DatabaseProvider.GetInstance().GetOtherPostId(postidlist, topicid, int.Parse(Posts.GetPostTableID()));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    list = list + dt.Rows[i]["pid"].ToString() + ",";

                }

                list = list.Substring(0, list.Length - 1);


                topicinfo.Lastpostid = Utils.StrToInt(dt.Rows[0]["pid"].ToString(), 0);
                topicinfo.Lastposterid = Utils.StrToInt(dt.Rows[0]["Posterid"].ToString(), 0);
                topicinfo.Lastpost = dt.Rows[0]["Postdatetime"].ToString();
                topicinfo.Lastposter = dt.Rows[0]["Poster"].ToString();

                topicinfo.Replies = dt.Rows.Count - 1;


                tid = Topics.CreateTopic(topicinfo);



                DatabaseProvider.GetInstance().UpdatePostTid(list, tid, Posts.GetPostTableID(tid));

                DatabaseProvider.GetInstance().SetPrimaryPost(subject, topicinfo.Tid, postid, Posts.GetPostTableID(tid));
                Topics.UpdateTopicTitle(topicinfo.Tid, subject);



            }


            //DatabaseProvider.GetInstance().UpdatePostTid(postidlist, tid, Posts.GetPostTableID(tid));


            //DatabaseProvider.GetInstance().UpdatePostTid(postidlist, tid, Posts.GetPostTableID(tid));
            //DatabaseProvider.GetInstance().SetPrimaryPost(subject, tid, postid, Posts.GetPostTableID(tid));
            Topics.UpdateTopicReplies(topicid);
            Topics.UpdateTopicReplies(tid);

            return tid;








            //���Ҫ���ָ��������Ϣ
            //TopicInfo __topicinfo = Topics.GetTopicInfo(topicid);

            ////�������������һ�����ӵ���Ϣ

            //PostInfo __postinfo = Posts.GetPostInfo(topicid, Utils.StrToInt(postid[postid.Length - 1], 0));

            //PostInfo postinfo = Posts.GetPostInfo(topicid, Utils.StrToInt(postid[0], 0));

            //if (__topicinfo.Lastpostid != Utils.StrToInt(postid[postid.Length - 1], 0))
            //{

            //     __topicinfo.Lastpostid = Utils.StrToInt(postid[postid.Length - 1], 0);

            //}



            //if (__topicinfo != null)
            //{
            //    __topicinfo.Title = Utils.HtmlEncode(subject);
            //    __topicinfo.Poster = postinfo.Poster;
            //    __topicinfo.Posterid = postinfo.Posterid;
            //    __topicinfo.Postdatetime = Utils.GetDateTime();
            //    //__topicinfo.Lastpostid = -1;
            //    __topicinfo.Lastposterid = __postinfo.Posterid;
            //    __topicinfo.Lastpost = __postinfo.Postdatetime;
            //    __topicinfo.Lastposter = __postinfo.Poster;
            //    __topicinfo.Views = 0;
            //    __topicinfo.Replies = postid.Length - 1;
            //    __topicinfo.Displayorder = 0;
            //    __topicinfo.Highlight = "";
            //    __topicinfo.Digest = 0;
            //    __topicinfo.Rate = 0;
            //    __topicinfo.Hide = 0;
            //    __topicinfo.Poll = 0;
            //    __topicinfo.Attachment = 0;
            //    __topicinfo.Moderated = 0;
            //    __topicinfo.Closed = 0;
            //}
            //����������
            //tid = Topics.CreateTopic(__topicinfo);
            ////tid = DatabaseProvider.GetInstance().GetMaxTopicId();

            //if (tid > 0)
            //{

            //    //���ָ���������ӵ�����ID��Ϊ������ID
            //    DatabaseProvider.GetInstance().UpdatePostTid(postidlist, tid, Posts.GetPostTableID(tid));

            //    //���ָ���������ӵĵ�һ����������Ϊ�����������
            //    DatabaseProvider.GetInstance().SetPrimaryPost(subject, tid, postid, Posts.GetPostTableID(tid));


            //    //����ԭ����(���ָ������)����Ϣ
            //    int Replies = Posts.GetPostCount(topicid);
            //    if (Replies > 0)
            //    {
            //        Replies = Replies - 1;
            //    }
            //    DataTable dt = Posts.GetLastPostByTid(topicid);
            //    int lastpostid = 0;
            //    int lastposterid = 0;
            //    string lastposter = "";
            //    DateTime lastpost = DateTime.Parse(Utils.GetDateTime());

            //    if (dt != null)
            //    {
            //        if (dt.Rows.Count > 0)
            //        {
            //            if (Utils.StrToInt(dt.Rows[0]["layer"].ToString(), 0) == 0)
            //            {
            //                Replies = 0;
            //            }

            //            DataRow dr = dt.Rows[0];
            //            lastpostid = Utils.StrToInt(dr["pid"], 0);
            //            lastposterid = Utils.StrToInt(dr["posterid"], 0);
            //            lastpost = DateTime.Parse(dr["Postdatetime"].ToString());
            //            lastposter = dr["poster"].ToString();

            //        }
            //    }

            //    //if (__topicinfo != null)
            //    //{
            //    //    __topicinfo.Title = Utils.HtmlEncode(subject);
            //    //    __topicinfo.Poster = postinfo.Poster;
            //    //    __topicinfo.Posterid = postinfo.Posterid;
            //    //    __topicinfo.Postdatetime = Utils.GetDateTime();
            //    //    if (Utils.StrToInt(postid[postid.Length - 1], 0) == lastpostid)
            //    //    {
            //    //        __topicinfo.Lastpostid = lastpostid;
            //    //    }
            //    //    else
            //    //    {
            //    //        __topicinfo.Lastpostid = Utils.StrToInt(postid[postid.Length - 1], 0);
            //    //    }
            //    //        __topicinfo.Lastposterid = __postinfo.Posterid;
            //    //    __topicinfo.Lastpost = __postinfo.Postdatetime;
            //    //    __topicinfo.Lastposter = __postinfo.Poster;
            //    //    __topicinfo.Views = 0;
            //    //    __topicinfo.Replies = postid.Length - 1;
            //    //    __topicinfo.Displayorder = 0;
            //    //    __topicinfo.Highlight = "";
            //    //    __topicinfo.Digest = 0;
            //    //    __topicinfo.Rate = 0;
            //    //    __topicinfo.Hide = 0;
            //    //    __topicinfo.Poll = 0;
            //    //    __topicinfo.Attachment = 0;
            //    //    __topicinfo.Moderated = 0;
            //    //    __topicinfo.Closed = 0;
            //    //}
            //    ////����������
            //    //Topics.CreateTopic(__topicinfo);
            //    DatabaseProvider.GetInstance().SetNewTopicProperty(topicid, Replies, lastpostid, lastposterid, lastposter, lastpost);
            //    //DbHelper.ExecuteNonQuery("UPDATE `" + BaseConfigs.GetTablePrefix + "topics` SET `lastpostid`=" + Utils.StrToInt(postid[postid.Length - 1], 0)+" where tid="+tid);
            //    //DatabaseProvider.GetInstance().SetNewLastPid(Utils.StrToInt(postid[postid.Length - 1], 0), tid);
            //    /*
            //     * 
            //     * ֻѡ��layerΪ1������ʱ�Ĵ�������
            //    int parentid = 0;
            //    int layerPos = 0;

            //    for (int i=0;i<postid.Length;i++)
            //    {

            //        __postinfo = PostFactory.GetPostInfo(Utils.StrToInt(postid[i],0));
            //        if (i==0)
            //        {
            //            parentid = __postinfo.Pid;
            //            layerPos = 1;
            //        }
            //        else
            //        {
            //            parentid = __postinfo.Parentid;
            //            layerPos = 0;
            //        }

            //        DbParameter[] parms = 
            //                                {
            //                                    DbHelper.MakeInParam("@pid",(DbType)System.Data.SqlDbType.TinyInt,1,__postinfo.Pid),
            //                                    DbHelper.MakeInParam("@parentid",(DbType)System.Data.SqlDbType.TinyInt,1,parentid),
            //                                    DbHelper.MakeInParam("@layerpos",(DbType)System.Data.SqlDbType.TinyInt,1,layerPos),
            //                                    DbHelper.MakeInParam("@tid",(DbType)System.Data.SqlDbType.TinyInt,1,tid)
            //                                };
            //        reval=DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE ["+BaseConfigFactory.GetTablePrefix+"posts + PostFactory.GetPostTableID(tid) + "] SET [layer] = [layer] - @layerpos, [tid] = @tid,[parentid] = @parentid END WHERE [parentid] = @parentid AND [layer] > 0",parms);
            //    }
            //     * 
            //    */
            //}


        }

        /// <summary>
        /// �ϲ�����
        /// </summary>
        /// <param name="topiclist">����id�б�</param>
        /// <param name="othertid">���ϲ�ip</param>
        /// <returns>���¼�¼��</returns>
        public static int MerrgeTopics(string topiclist, int othertid)
        {
            int tid = Utils.StrToInt(topiclist, 0);
            int reval = 0;
            //���Ҫ���ϲ����������Ϣ
            TopicInfo topicinfo = Topics.GetTopicInfo(othertid);
            TopicInfo topicinfo_new = Topics.GetTopicInfo(tid);

            if (topicinfo.Lastpostid == 0)
            {
                DatabaseProvider.GetInstance().UpdateTopicLastPosterId(topicinfo.Tid);
            }
            if (topicinfo_new.Lastpostid == 0)
            {
                DatabaseProvider.GetInstance().UpdateTopicLastPosterId(topicinfo_new.Tid);
            }



            reval = DatabaseProvider.GetInstance().UpdatePostTidToAnotherTopic(othertid, tid, Posts.GetPostTableID(tid));
            reval = DatabaseProvider.GetInstance().UpdatePostTidToAnotherTopic(tid, tid, Posts.GetPostTableID(tid));
            //���¸�������
            reval = DatabaseProvider.GetInstance().UpdateAttachmentTidToAnotherTopic(othertid, tid);

            reval = DatabaseProvider.GetInstance().DeleteTopic(othertid);

            if (topicinfo != null)
            {
                if (Utils.StrToInt(topicinfo_new.Lastpostid, 0) < Utils.StrToInt(topicinfo.Lastpostid, 0))
                {
                    reval = DatabaseProvider.GetInstance().UpdateTopic(tid, topicinfo);
                }
                else
                {
                    reval = DatabaseProvider.GetInstance().UpdateTopicReplies(tid, topicinfo.Replies);
                }
            }

            //����������Ϣ
            PostInfo primarypost = Posts.GetPostInfo(tid, Posts.GetFirstPostId(tid));
            DatabaseProvider.GetInstance().SetPrimaryPost(primarypost.Title, tid, new string[] { primarypost.Pid.ToString() }, Posts.GetPostTableID(tid));
            Topics.UpdateTopic(tid, primarypost.Title, primarypost.Posterid, primarypost.Poster);

            return reval;
        }

        /// <summary>
        /// �޸������б�
        /// </summary>
        /// <param name="topiclist">����id�б�</param>
        /// <returns>���¼�¼��</returns>
        public static int RepairTopicList(string topiclist)
        {
            if (!Utils.IsNumericArray(topiclist.Split(',')))
            {
                return 0;
            }

            int revalcount = 0;

            string[] idlist = Posts.GetPostTableIDList(topiclist);
            string[] tidlist = topiclist.Split(',');
            for (int i = 0; i < idlist.Length; i++)
            {
                string posttable = BaseConfigs.GetTablePrefix + "posts" + (Utils.StrToInt(idlist[i], 0));
                int reval = DatabaseProvider.GetInstance().RepairTopics(tidlist[i], posttable);
                if (reval > 0)
                {
                    revalcount = reval + revalcount;
                    Attachments.UpdateTopicAttachment(topiclist);
                }


            }



            //foreach (string tid in Posts.GetPostTableIDList(topiclist))
            //{
            //    string posttable = BaseConfigs.GetTablePrefix + "posts" + (Utils.StrToInt(tid, 0));


            //    int reval = DatabaseProvider.GetInstance().RepairTopics(topiclist, posttable);
            //    if (reval > 0)
            //    {
            //        revalcount = reval + revalcount;
            //        Attachments.UpdateTopicAttachment(topiclist);
            //    }
            //}
            return revalcount;
        }




        /// <summary>
        /// ���ݵõ���������id���û��б�
        /// </summary>
        /// <param name="postlist">�����б�</param>
        /// <returns>�û��б�</returns>
        private static string GetUserListWithPostlist(int tid, string postlist)
        {
            if (!Utils.IsNumericArray(postlist.Split(',')))
            {
                return "";
            }
            StringBuilder useridlist = new StringBuilder();
            IDataReader reader = DatabaseProvider.GetInstance().GetUserListWithPostList(postlist, Posts.GetPostTableID(tid));
            if (reader != null)
            {
                while (reader.Read())
                {
                    if (!useridlist.ToString().Equals(""))
                    {
                        useridlist.Append(",");
                    }
                    useridlist.Append(reader["posterid"].ToString());

                }
                reader.Close();
            }
            return useridlist.ToString();

        }

        /// <summary>
        /// ��ָ����������
        /// </summary>
        /// <param name="postidlist">�����б�</param>
        /// <param name="score">Ҫ�ӣ����ķ�ֵ�б�</param>
        /// <param name="extcredits">��Ӧ����չ�����б�</param>
        /// <returns>��������</returns>
        public static int RatePosts(int tid, string postidlist, string score, string extcredits, int userid, string username, string reason)
        {
            if (!Utils.IsNumericArray(Utils.SplitString(postidlist, ",")))
            {
                return 0;
            }
            float[] extcreditslist = new float[] { 0, 0, 0, 0, 0, 0, 0, 0 };
            string[] tmpScorelist = Utils.SplitString(score, ",");
            string[] tmpExtcreditslist = Utils.SplitString(extcredits, ",");
            int tempExtc = 0;
            string posttableid = Posts.GetPostTableID(tid);
            for (int i = 0; i < tmpExtcreditslist.Length; i++)
            {
                tempExtc = Utils.StrToInt(tmpExtcreditslist[i], -1);
                if (tempExtc > 0 && tempExtc < extcreditslist.Length)
                {
                    extcreditslist[tempExtc - 1] = Utils.StrToInt(tmpScorelist[i], 0);
                    //if (Utils.StrToInt(tmpScorelist[i], 0) != 0)
                    //{
                    AdminRateLogs.InsertLog(postidlist, userid, username, tempExtc, Utils.StrToInt(tmpScorelist[i], 0), reason);

                    //������Ӧ���ӵĻ�����
                    foreach (string pid in Utils.SplitString(postidlist, ","))
                    {
                        if (pid.Trim() != string.Empty)
                        {
                            SetPostRate(posttableid,
                                        Utils.StrToInt(pid, 0),
                                        Utils.StrToInt(tmpExtcreditslist[i], 0),
                                        Utils.StrToFloat(tmpScorelist[i], (float)0),
                                        true);
                        }
                    }
                    //}
                }
            }


            string useridlist = GetUserListWithPostlist(tid, postidlist);

            return UserCredits.UpdateUserCreditsByRate(useridlist, extcreditslist);

        }

        /// <summary>
        /// �õ�ǰ������ֵͨ��һ���һ���������ɻ��ֺ󣬸�����Ӧ�����е�rate�ֶ�.
        /// </summary>
        /// <param name="postid">����ID</param>
        /// <param name="extid">��չ����ID</param>
        /// <param name="score">����</param>
        /// <param name="israte">trueΪ���֣�falseΪ��������</param>
        public static void SetPostRate(string posttableid, int postid, int extid, float score, bool israte)
        {
            if (score == 0)
            {
                return;
            }

            DataTable scorePaySet = Scoresets.GetRateScoreSet();
            if (scorePaySet.Rows.Count > 0)
            {
                if (scorePaySet.Rows[extid - 1]["name"].ToString().Trim() != "")
                {
                    float rate = Utils.StrToFloat(scorePaySet.Rows[extid - 1]["rate"].ToString(), (float)0);
                    
                    if (rate != 0)
                    {
                        rate = (rate * score);
                    }

                    //���ǳ�������
                    if (!israte)
                    {
                        rate = -1 * rate;
                    }

                   DatabaseProvider.GetInstance().UpdatePostRate(postid, rate, posttableid);
                   IDataReader reader=DatabaseProvider.GetInstance().GetPostInfo(posttableid, postid);
                   while (reader.Read())
                   {
                       if (Utils.StrToInt(reader["layer"], -1) == 0)
                       {
                           DatabaseProvider.GetInstance().SetTopicStatus(reader["tid"].ToString(), "rate", Utils.StrToInt(reader["rate"].ToString(),-1));
                       }
                   
                   }

                }
            }
        }

        /// <summary>
        /// �������״̬
        /// </summary>
        /// <param name="postidlist">����id�б�</param>
        /// <param name="userid">�û�id</param>
        /// <returns>�����ֵ�����id�ַ���</returns>
        public static string CheckRateState(string postidlist, int userid)
        {
            string reval = "";
            string tempreval = "";
            foreach (string pid in Utils.SplitString(postidlist, ","))
            {
                tempreval = DatabaseProvider.GetInstance().CheckRateState(userid, pid);
                if (!tempreval.Equals(""))
                {
                    if (!reval.Equals(""))
                    {
                        reval = reval + ",";
                    }
                    reval = reval + tempreval;
                }

            }
            return reval;

        }


        /// <summary>
        /// ����ָ����������һ�β���
        /// </summary>
        /// <param name="tid">����id</param>
        /// <returns>������־����</returns>
        public static string GetTopicListModeratorLog(int tid)
        {
            string str = "";
            IDataReader reader = null;

            reader = DatabaseProvider.GetInstance().GetTopicListModeratorLog(tid);
            if (reader != null)
            {
                if (reader.Read())
                {
                    str = "�������� " + reader["grouptitle"].ToString() + " " + reader["moderatorname"].ToString() + " �� " + reader["postdatetime"].ToString() + " ִ�� " + reader["actions"].ToString() + " ����";

                }
                reader.Close();
            }
            return str;
        }



        /// <summary>
        /// ������������
        /// </summary>
        /// <param name="topictypeid">��������</param>
        /// <param name="topiclist">Ҫ���õ������б�</param>
        /// <returns></returns>
        public static int ResetTopicTypes(int topictypeid, string topiclist)
        {
            return DatabaseProvider.GetInstance().ResetTopicTypes(topictypeid, topiclist);
        }


        public static void IdentifyTopic(string topiclist, int identify)
        {
            if (!Utils.IsNumericArray(topiclist.Split(',')))
                return;
            DatabaseProvider.GetInstance().IdentifyTopic(topiclist, identify);
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="postidlist"></param>
        /// <param name="userid"></param>
        /// <param name="username"></param>
        /// <param name="reason"></param>
        public static void CancelRatePosts(string ratelogidlist, int tid, string pid, int userid, string username, int groupid, string grouptitle, int forumid, string forumname, string reason)
        {
            if (!Utils.IsNumeric(pid))
            {
                return;
            }

            string posttableid = Posts.GetPostTableID(tid);

            DataTable dt = AdminRateLogs.LogList(ratelogidlist.Split(',').Length, 1, "id IN(" + ratelogidlist + ")");//�õ�Ҫɾ����������־�б�

            int rateduserid = Posts.GetPostInfo(tid, Utils.StrToInt(pid, 0)).Posterid; //�����ֵ��û���UID

            if (rateduserid <= 0)
            {
                return;
            }

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    SetPostRate(posttableid,
                                 Utils.StrToInt(pid, 0),
                                 Utils.StrToInt(dr["extcredits"].ToString(), 0),
                                 Utils.StrToFloat(dr["score"].ToString(), (float)0),
                                 false);

                    DatabaseProvider.GetInstance().UpdateUserCredits(
                        Utils.StrToInt(rateduserid, 0),
                        Utils.StrToInt(dr["extcredits"].ToString(), 0),
                        (-1) * Utils.StrToFloat(dr["score"].ToString(), (float)0)); //��-1��Ҫ���з�ֵ�ķ������
                }
            }


            AdminRateLogs.DeleteLog("[id] IN(" + ratelogidlist + ")");

            //�������������ּ�¼ʱ�������������ص�������Ϣ�ֶ�(rate,ratetimes)
            if (AdminRateLogs.LogList(1, 1, "pid = " + pid).Rows.Count == 0)
            {
                DatabaseProvider.GetInstance().CancelPostRate(pid, posttableid);
            }

            TopicInfo topicinfo = Topics.GetTopicInfo(tid);
            string topictitle = "���ޱ���";
            if (topicinfo != null)
            {
                topictitle = topicinfo.Title;
            }

            DatabaseProvider.GetInstance().InsertModeratorLog(userid.ToString(),
                                                              username,
                                                              groupid.ToString(),
                                                              grouptitle,
                                                              Utils.GetRealIP(),
                                                              Utils.GetDateTime(),
                                                              forumid.ToString(),
                                                              forumname,
                                                              tid.ToString(),
                                                              topictitle,
                                                              "��������",
                                                              reason);
        }

        public static void BumpTopics(string topiclist, int bumptype)
        {

            int lastpostid = 0;
            if (bumptype == 1)
            {
                string  [] tidlist = topiclist.Split(',');
                foreach (string tid in tidlist)
                {
                    lastpostid = DatabaseProvider.GetInstance().GetPostId();
                    DatabaseProvider.GetInstance().SetTopicsBump(tid, lastpostid);
                }
            }
            else
            {

                DatabaseProvider.GetInstance().SetTopicsBump(topiclist, lastpostid);
            }
        }


    } //class end


}