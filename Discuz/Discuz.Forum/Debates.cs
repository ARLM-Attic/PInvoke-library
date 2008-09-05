using System;
using System.Text;
using Discuz.Data;
using System.Data;
using Discuz.Entity;
using System.Reflection;
using Discuz.Config;
using Discuz.Common;
using System.Web;
using Discuz.Common.Generic;

namespace Discuz.Forum
{
    public class Debates
    {


        /// <summary>
        /// ��ȡ���ӹ۵�
        /// </summary>
        /// <param name="tid">����ID</param>
        /// <returns>Dictionary����</returns>
        public static Dictionary<int, int> GetPostDebateList(int tid)
        {
            Dictionary<int, int> debateList = new Dictionary<int, int>();
            IDataReader reader = DatabaseProvider.GetInstance().GetPostDebate(tid);
            while (reader.Read())
            {
                debateList.Add(Utils.StrToInt(reader["pid"], 0), Utils.StrToInt(reader["opinion"], 0));
            }
            reader.Close();
            return debateList;
        }

        /// <summary>
        /// ��ȡ���۵���չ��Ϣ
        /// </summary>
        /// <param name="tid">����ID</param>
        /// <returns>����������չ��Ϣ</returns>
        public static DebateInfo GetDebateTopic(int tid)
        {
            DebateInfo topicexpand = new DebateInfo();
            IDataReader debatetopic = DatabaseProvider.GetInstance().GetDebateTopic(tid);
            if (debatetopic.Read())
            {
                topicexpand.Positiveopinion = debatetopic["positiveopinion"].ToString();
                topicexpand.Negativeopinion = debatetopic["negativeopinion"].ToString();
                //topicexpand.Positivecolor = debatetopic["positivecolor"].ToString();
                //topicexpand.Negativecolor = debatetopic["negativecolor"].ToString();
                topicexpand.Terminaltime = DateTime.Parse(debatetopic["terminaltime"].ToString());
                topicexpand.Positivediggs = int.Parse(debatetopic["positivediggs"].ToString());
                topicexpand.Negativediggs = int.Parse(debatetopic["negativediggs"].ToString());
                //topicexpand.Positivebordercolor = debatetopic["positivebordercolor"].ToString();
                //topicexpand.Negativebordercolor = debatetopic["negativebordercolor"].ToString();
                topicexpand.Tid = tid;
            }
            debatetopic.Close();
            return topicexpand;
        }

        /// <summary>
        /// ���ص��õ�JSON����
        /// </summary>
        /// <param name="callback">JS�ص�����</param>
        /// <param name="tidlist">����ID�б�</param>
        /// <returns>JS����</returns>
        public static string GetDebatesJsonList(string callback, string tidlist)
        {

            IDataReader reader = null;
            switch (callback)
            {
                case "gethotdebatetopic":
                    string[] debatesrule;
                    int listlength = tidlist.Split(',').Length;
                    if (listlength < 3)
                    {
                        break;
                    }
                    else
                    {
                        debatesrule = tidlist.Split(',');
                        if (debatesrule[0] != "views" && debatesrule[0] != "replies" && Utils.IsNumeric(debatesrule[1]) && Utils.IsNumeric(debatesrule[2]))
                            break;
                    }
                    reader = DatabaseProvider.GetInstance().GetHotDebatesList(debatesrule[0], Utils.StrToInt(debatesrule[1], 0), Utils.StrToInt(debatesrule[2], 0));
                    break;
                case "recommenddebates":
                    if (tidlist == string.Empty)
                    {
                        tidlist = GeneralConfigs.GetConfig().Recommenddebates;
                    }
                    else
                    {
                        if (!Utils.IsNumericList(tidlist))
                        {
                            break;
                        }
                    }

                    reader = DatabaseProvider.GetInstance().GetRecommendDebates(tidlist);
                    break;
                default:
                    break;
            }
            StringBuilder sb = new StringBuilder();
            sb.Append(callback);
            sb.Append("([");
                if (reader.Read())
                {
                    string str = string.Format("{{'title':'{0}','tid','{1}'}},", reader["title"].ToString(), reader["tid"].ToString());
                    sb.Append(str);
                    
                }
                else
                {

                    return "0";
                }

            while (reader.Read())
            {
                string str = string.Format("{{'title':'{0}','tid','{1}'}},", reader["title"].ToString(), reader["tid"].ToString());
                sb.Append(str);
            }
            reader.Close();
            return sb.ToString().Remove(sb.Length - 1) + "])";
        }
        /// <summary>
        /// ��ӵ���
        /// </summary>
        /// <param name="tid">����ID</param>
        /// <param name="message">��������</param>
        public static void CommentDabetas(int tid, string message)
        {
            DatabaseProvider.GetInstance().AddCommentDabetas(tid, int.Parse(Posts.GetPostTableID(tid)), message);

        }


        /// <summary>
        /// ��֤�û����Ƿ�����
        /// </summary>
        /// <param name="userid">�û�id</param>
        /// <param name="tips">��ʾ��Ϣ</param>
        /// <returns>�Ƿ���Զ�,��������</returns>
        public static bool AllowDiggs(int userid, out string tips)
        {
            tips = "�����ڵ��û��鲻����˲���";
            if (UserGroups.GetUserGroupInfo(7).Allowdiggs == 0)
            {
                if (userid == -1)
                {
                  
                    return false;
                }
            
            }
            
            UserInfo userinfo = Users.GetUserInfo(userid);
            UserGroupInfo usergroupinfo = UserGroups.GetUserGroupInfo(userinfo.Groupid);
            if (usergroupinfo.Allowdiggs == 0)
            {
                //tips = "�����ڵ��û��鲻����˲���";
                return false;
            }
            return true;
        }


        /// <summary>
        /// ����Digg
        /// </summary>
        /// <param name="tid">����id</param>
        /// <param name="pid">����ID</param>
        /// <param name="type">�������۵�</param>
        /// <param name="userid">�û�ID</param>
        public static void AddDebateDigg(int tid, int pid, int type, int userid)
        {
            UserInfo userinfo = Users.GetUserInfo(userid);
            DatabaseProvider.GetInstance().AddDebateDigg(tid, pid, type,Utils.GetRealIP(), userinfo);

        }

        /// <summary>
        /// �ж��Ƿ񶥹�
        /// </summary>
        /// <param name="pid">����ID</param>
        /// <param name="userid">�û�ID</param>
        /// <returns>�ж��Ƿ񶥹�</returns>
        public static bool IsDigged(int pid, int userid)
        {
            //�����οͺ���֤��ʽΪ��ɢ��֤,24Сʱ��ֻ�ܶ�һ��
            if (UserGroups.GetUserGroupInfo(7).Allowdiggs != 1)
            {
                return !DatabaseProvider.GetInstance().AllowDiggs(pid, userid);
            }
            else
            {
                if (Utils.GetCookie("debatedigged") == string.Empty)
                {
                    return false;
                }
                string[] pidlist = Utils.GetCookie("debatedigged").Split(',');
                foreach (string s in pidlist)
                {
                    if (pid == Utils.StrToInt(s, 0))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// д���Ѷ���COOKIES
        /// </summary>
        /// <param name="pid">����ID</param>
        public static void WriteCookies(int pid)
        {

            if (Utils.GetCookie("debatedigged") == string.Empty)
            {
                Utils.WriteCookie("debatedigged", pid.ToString(), 1440);
            }
            else
            {
                string newlist = Utils.GetCookie("debatedigged") + "," + pid.ToString();
                Utils.WriteCookie("debatedigged", newlist, 1440);
            }
        }

        /// <summary>
        /// ���ر������������һ����������
        /// </summary>
        /// <param name="postpramsInfo">���ӵĸ�����Ϣ</param>
        /// <param name="debateOpinion">���ӹ۵�</param>
        /// <returns>������</returns>
        public static int GetDebatesPostCount(PostpramsInfo postpramsInfo, int debateOpinion)
        {
            return DatabaseProvider.GetInstance().GetDebatesPostCount(postpramsInfo.Tid, debateOpinion);
        
        }
        /// <summary>
        /// ��ȡ�������������б�
        /// </summary>
        /// <param name="postpramsInfo">���ӵĸ�����Ϣ</param>
        /// <param name="attachmentlist">�����б�</param>
        /// <param name="ismoder">�Ƿ��й���Ȩ��</param>
        /// <returns>���������б�</returns>
        public static List<ShowtopicPagePostInfo> GetPositivePostList(PostpramsInfo postpramsInfo, out List<ShowtopicPageAttachmentInfo> attachmentlist, bool ismoder)
        {
            return GetDebatePostList(postpramsInfo, out attachmentlist, ismoder, 1, new PostOrderType());
        }

        private static List<ShowtopicPagePostInfo> GetDebatePostList(PostpramsInfo postpramsInfo, out List<ShowtopicPageAttachmentInfo> attachmentlist, bool ismoder, int debateOpinion, PostOrderType postOrderType)
        {
            List<ShowtopicPagePostInfo> postcoll = new List<ShowtopicPagePostInfo>();
            attachmentlist = new List<ShowtopicPageAttachmentInfo>();
            StringBuilder attachmentpidlist = new StringBuilder();
            StringBuilder pidlist = new StringBuilder();
            IDataReader reader = DatabaseProvider.GetInstance().GetDebatePostList(postpramsInfo.Tid, debateOpinion, postpramsInfo.Pagesize, postpramsInfo.Pageindex, Posts.GetPostTableName(postpramsInfo.Tid), postOrderType);

            postcoll = Posts.ParsePostList(postpramsInfo, attachmentlist, ismoder, postcoll, reader, attachmentpidlist);

            //���������ֶβ�׼����δȡ�÷�ҳ��Ϣʱ�����������ֶΣ���ȡ���һҳ
            if (postcoll.Count == 0 && postpramsInfo.Pageindex > 1)
            {
                int postcount = DatabaseProvider.GetInstance().ReviseDebateTopicDiggs(postpramsInfo.Tid, debateOpinion);

                postpramsInfo.Pageindex = postcount % postpramsInfo.Pagesize == 0 ? postcount / postpramsInfo.Pagesize : postcount / postpramsInfo.Pagesize + 1;

                reader = DatabaseProvider.GetInstance().GetDebatePostList(postpramsInfo.Tid, debateOpinion, postpramsInfo.Pagesize, postpramsInfo.Pageindex, Posts.GetPostTableName(postpramsInfo.Tid), postOrderType);

                postcoll = Posts.ParsePostList(postpramsInfo, attachmentlist, ismoder, postcoll, reader, attachmentpidlist);
            }

           

            foreach (ShowtopicPagePostInfo post in postcoll)
            {
                pidlist.AppendFormat("{0},", post.Pid);
            }

            Dictionary<int, int> postdiggs = GetPostDiggs(pidlist.ToString().Trim(','));
            foreach (ShowtopicPagePostInfo post in postcoll)
            {
                if (postdiggs.ContainsKey(post.Pid))
                {
                    post.Diggs = postdiggs[post.Pid];
                }
            }
            return postcoll;
        }
        /// <summary>
        /// �������ӱ�����
        /// </summary>
        /// <param name="pidlist">����ID����</param>
        /// <returns>Dictionary</returns>
        public static Dictionary<int, int> GetPostDiggs(string pidlist)
        {
            Dictionary<int, int> result = new Dictionary<int,int>();
            IDataReader reader = DatabaseProvider.GetInstance().GetDebatePostDiggs(pidlist);
            if (reader != null)
            {
                while (reader.Read())
                {
                    result[Convert.ToInt32(reader["pid"])] = Convert.ToInt32(reader["diggs"]);
                }
                reader.Close();
            }
           
            return result;
        }

        /// <summary>
        /// �����������б�
        /// </summary>
        /// <param name="postpramsInfo">���ӵĸ�����Ϣ</param>
        /// <param name="attachmentlist">�����б�</param>
        /// <param name="ismoder">�Ƿ��й���Ȩ��</param>
        /// <returns>���������б�</returns>
        public static List<ShowtopicPagePostInfo> GetNegativePostList(PostpramsInfo postpramsInfo, out List<ShowtopicPageAttachmentInfo> attachmentlist, bool ismoder)
        {
            return GetDebatePostList(postpramsInfo, out attachmentlist, ismoder, 2, new PostOrderType());
        }

        /// <summary>
        /// �õ��û��Ѷ�������PID
        /// </summary>
        /// <param name="tid">����id</param>
        /// <param name="uid">�û�id</param>
        /// <returns>�û��Ѷ�������PID</returns>
        public static string GetUesrDiggs(int tid, int uid)
        {
            //string userdiggs = "";
            StringBuilder sb = new StringBuilder();
            IDataReader reader = DatabaseProvider.GetInstance().GetUesrDiggs(tid, uid);
            while (reader.Read())
            {
                //userdiggs = userdiggs + "|" + reader["pid"].ToString();
                //sb.Append(userdiggs);
                sb.Append("|");
                sb.Append(reader["pid"].ToString());

            }
            reader.Close();
            //return userdiggs;
            return sb.ToString();
        }
    }
}
