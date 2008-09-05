using System;
using System.Web;
using System.Text;
using System.Data;
using System.Data.Common;

using Discuz.Common;
using Discuz.Data;
using Discuz.Config;
using Discuz.Entity;


namespace Discuz.Forum
{
    /// <summary>
    /// �����û�������
    /// </summary>
    public class OnlineUsers
    {


        private static object SynObject = new object();

        /// <summary>
        /// ��������û�������
        /// </summary>
        /// <returns>�û�����</returns>
        public static int GetOnlineAllUserCount()
        {
            int count = DatabaseProvider.GetInstance().GetOnlineAllUserCount();
            return count == 0 ? 1 : count;
        }

        /// <summary>
        /// ���ػ����������û�����
        /// </summary>
        /// <returns>�����������û�����</returns>
        public static int GetCacheOnlineAllUserCount()
        {

            int count = Utils.StrToInt(Utils.GetCookie("onlineusercount"), 0);
            if (count == 0)
            {
                count = OnlineUsers.GetOnlineAllUserCount();
                Utils.WriteCookie("onlineusercount", count.ToString(), 3);
            }
            return count;

        }

        /// <summary>
        /// ����֮ǰ�����߱��¼(��������Ӧ�ó����ʼ��ʱ������)
        /// </summary>
        /// <returns></returns>
        public static int InitOnlineList()
        {
            return DatabaseProvider.GetInstance().CreateOnlineTable();
        }

        /// <summary>
        /// ��λ���߱�, ���ϵͳδ����, ����Ӧ�ó�����������, �򲻻����´���
        /// </summary>
        /// <returns></returns>
        public static int ResetOnlineList()
        {
            try
            {
                // ȡ�����߱����һ����¼��tickcount�ֶ� (��Ϊ�����ܲ�Ҫ���ر�ȷ)
                //int tickcount = DatabaseProvider.GetInstance().GetLastTickCount();
                // �����������ϵͳ����ʱ��С��10����
                if (System.Environment.TickCount < 600000 && System.Environment.TickCount > 0)
                {
                    return InitOnlineList();
                }
                return -1;
            }
            catch
            {
                try
                {
                    return InitOnlineList();
                }
                catch
                {
                    return -1;
                }
            }

        }

        /// <summary>
        /// �������ע���û�������
        /// </summary>
        /// <returns>�û�����</returns>
        public static int GetOnlineUserCount()
        {
            return DatabaseProvider.GetInstance().GetOnlineUserCount();
        }





        #region ���ݲ�ͬ������ѯ�����û���Ϣ


        /// <summary>
        /// �����û������б�
        /// </summary>
        /// <param name="forumid">���id</param>
        /// <param name="totaluser">ȫ���û���</param>
        /// <param name="guest">�ο���</param>
        /// <param name="user">��¼�û���</param>
        /// <param name="invisibleuser">�����Ա��</param>
        /// <returns>�û������б�DataTable</returns>
        public static DataTable GetForumOnlineUserList(int forumid, out int totaluser, out int guest, out int user, out int invisibleuser)
        {
            DataTable dt = new DataTable();

            lock (SynObject)
            {
                dt = DatabaseProvider.GetInstance().GetForumOnlineUserListTable(forumid);
            }

            totaluser = dt.Rows.Count;
            // ͳ���ο�
            DataRow[] dr = dt.Select("userid=-1");
            if (dr == null)
            {
                guest = 0;
            }
            else
            {
                guest = dr.Length;
            }
            //ͳ�������û�
            dr = dt.Select("invisible=1");
            if (dr == null)
            {
                invisibleuser = 0;
            }
            else
            {
                invisibleuser = dr.Length;
            }
            //ͳ���û�
            user = totaluser - guest;
            //���ص�ǰ���������û���
            return dt;
        }

        /// <summary>
        /// ���������û��б�
        /// </summary>
        /// <param name="totaluser">ȫ���û���</param>
        /// <param name="guest">�ο���</param>
        /// <param name="user">��¼�û���</param>
        /// <param name="invisibleuser">�����Ա��</param>
        /// <returns>���û��б�</returns>
        public static DataTable GetOnlineUserList(int totaluser, out int guest, out int user, out int invisibleuser)
        {
            DataTable dt = DatabaseProvider.GetInstance().GetOnlineUserListTable();

            int highestonlineusercount = Utils.StrToInt(Statistics.GetStatisticsRowItem("highestonlineusercount"), 1);
            if (totaluser > highestonlineusercount)
            {
                if (Statistics.UpdateStatistics("highestonlineusercount", totaluser) > 0)
                {
                    Statistics.UpdateStatistics("highestonlineusertime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    Statistics.ReSetStatisticsCache();
                }

            }
            // ͳ���û�
            DataRow[] dr = dt.Select("userid>0");
            if (dr == null)
            {
                user = 0;
            }
            else
            {
                user = dr.Length;
            }
            //ͳ�������û�
            dr = dt.Select("invisible=1");
            if (dr == null)
            {
                invisibleuser = 0;
            }
            else
            {
                invisibleuser = dr.Length;
            }
            //ͳ���ο�
            if (totaluser > user)
            {
                guest = totaluser - user;
            }
            else
            {
                guest = 0;
            }
            //���ص�ǰ���������û���
            return dt;
        }



        #endregion


        /// <summary>
        /// ���������û�ͼ��
        /// </summary>
        /// <returns>�����û�ͼ��</returns>
        private static DataTable GetOnlineGroupIconTable()
        {

            lock (SynObject)
            {
                Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
                DataTable dt = cache.RetrieveObject("/Forum/OnlineIconTable") as DataTable;
                if (dt != null)
                {
                    return dt;
                }
                else
                {
                    dt = DatabaseProvider.GetInstance().GetOnlineGroupIconTable();
                    cache.AddObject("/Forum/OnlineIconTable", dt);
                    return dt;
                }
            }
        }
        /// <summary>
        /// �����û���ͼ��
        /// </summary>
        /// <param name="groupid">�û���</param>
        /// <returns>�û���ͼ��</returns>
        public static string GetGroupImg(int groupid)
        {
            string img = "";
            DataTable dt = GetOnlineGroupIconTable();
            // ���û��Ҫ��ʾ��ͼ�������򷵻�""
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    // ͼ�����ͳ�ʼΪ:��ͨ�û�
                    // �����ƥ��������Ϊƥ���ͼ��
                    if ((int.Parse(dr["groupid"].ToString()) == 0 && img == "") || (int.Parse(dr["groupid"].ToString()) == groupid))
                    {
                        img = "<img src=\"images\\groupicons\\" + dr["img"].ToString() + "\" />";
                    }
                }
            }
            return img;
        }


        #region �鿴ָ����ĳһ�û�����ϸ��Ϣ
        public static OnlineUserInfo GetOnlineUser(int olid)
        {
            IDataReader reader = DatabaseProvider.GetInstance().GetOnlineUser(olid);
            OnlineUserInfo onlineuserinfo = null;
            if (reader.Read())
            {
                onlineuserinfo = LoadSingleOnlineUser(reader);
            }
            reader.Close();
            return onlineuserinfo;
        }

        /// <summary>
        /// ���ָ���û�����ϸ��Ϣ
        /// </summary>
        /// <param name="userid">�����û�ID</param>
        /// <param name="password">�û�����</param>
        /// <returns>�û�����ϸ��Ϣ</returns>
        private static OnlineUserInfo GetOnlineUser(int userid, string password)
        {
            DataTable dt = DatabaseProvider.GetInstance().GetOnlineUser(userid, password);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                OnlineUserInfo onlineuserinfo = LoadSingleOnlineUser(dr);
                dt.Dispose();
                return onlineuserinfo;
            }
            return null;


        }


        /// <summary>
        /// ���ָ���û�����ϸ��Ϣ
        /// </summary>
        /// <returns>�û�����ϸ��Ϣ</returns>
        private static OnlineUserInfo GetOnlineUserByIP(int userid, string ip)
        {
            DataTable dt = DatabaseProvider.GetInstance().GetOnlineUserByIP(userid, ip);
            if (dt.Rows.Count > 0)
            {
                OnlineUserInfo oluser = LoadSingleOnlineUser(dt.Rows[0]);
                dt.Dispose();
                return oluser;

            }
            return null;


        }



        /// <summary>
        /// ��������û���֤���Ƿ���Ч
        /// </summary>
        /// <param name="olid">�����û�ID</param>
        /// <param name="verifycode">��֤��</param>
        /// <returns>�����û�ID</returns>
        public static bool CheckUserVerifyCode(int olid, string verifycode)
        {
            return DatabaseProvider.GetInstance().CheckUserVerifyCode(olid, verifycode, ForumUtils.CreateAuthStr(5, false));
        }



        #endregion


        #region ����µ������û�
        /// <summary>
        /// ִ�������û������������ӵĲ�����
        /// </summary>
        /// <param name="onlineuserinfo">�����û���Ϣ����</param>
        /// <returns>��ӳɹ��򷵻ظո���ӵ�olid,ʧ���򷵻�0</returns>
        private static int Add(OnlineUserInfo onlineuserinfo, int timeout)
        {
            return DatabaseProvider.GetInstance().AddOnlineUser(onlineuserinfo, timeout);
        }


        /// <summary>
        /// Cookie��û���û�ID�����ĵ��û�ID��Чʱ�����߱�������һ���ο�.
        /// </summary>
        public static OnlineUserInfo CreateGuestUser(int timeout)
        {


            OnlineUserInfo onlineuserinfo = new OnlineUserInfo();

            onlineuserinfo.Userid = -1;
            onlineuserinfo.Username = "�ο�";
            onlineuserinfo.Nickname = "�ο�";
            onlineuserinfo.Password = "";
            onlineuserinfo.Groupid = 7;
            onlineuserinfo.Olimg = GetGroupImg(7);
            onlineuserinfo.Adminid = 0;
            onlineuserinfo.Invisible = 0;
            onlineuserinfo.Ip = DNTRequest.GetIP();
            onlineuserinfo.Lastposttime = "1900-1-1 00:00:00";
            onlineuserinfo.Lastpostpmtime = "1900-1-1 00:00:00";
            onlineuserinfo.Lastsearchtime = "1900-1-1 00:00:00";
            onlineuserinfo.Lastupdatetime = Utils.GetDateTime();
            onlineuserinfo.Action = 0;
            onlineuserinfo.Lastactivity = 0;
            onlineuserinfo.Verifycode = ForumUtils.CreateAuthStr(5, false);

            int olid = Add(onlineuserinfo, timeout);
            onlineuserinfo.Olid = olid;

            return onlineuserinfo;

        }


        /// <summary>
        /// ����һ����Ա��Ϣ�������б��С��û�login.aspx�������û���Ϣ��ʱ,���û������ߵ���������������û������б�
        /// </summary>
        /// <param name="uid"></param>
        private static OnlineUserInfo CreateUser(int uid, int timeout)
        {
            OnlineUserInfo onlineuserinfo = new OnlineUserInfo();
            if (uid > 0)
            {
                ShortUserInfo ui = Users.GetShortUserInfo(uid);
                if (ui != null)
                {
                    onlineuserinfo.Userid = uid;
                    onlineuserinfo.Username = ui.Username.Trim();
                    onlineuserinfo.Nickname = ui.Nickname.Trim();
                    onlineuserinfo.Password = ui.Password.Trim();
                    onlineuserinfo.Groupid = short.Parse(ui.Groupid.ToString());
                    onlineuserinfo.Olimg = GetGroupImg(short.Parse(ui.Groupid.ToString()));
                    onlineuserinfo.Adminid = short.Parse(ui.Adminid.ToString());
                    onlineuserinfo.Invisible = short.Parse(ui.Invisible.ToString());


                    onlineuserinfo.Ip = DNTRequest.GetIP();
                    onlineuserinfo.Lastposttime = "1900-1-1 00:00:00";
                    onlineuserinfo.Lastpostpmtime = "1900-1-1 00:00:00";
                    onlineuserinfo.Lastsearchtime = "1900-1-1 00:00:00";
                    onlineuserinfo.Lastupdatetime = Utils.GetDateTime();
                    onlineuserinfo.Action = 0;
                    onlineuserinfo.Lastactivity = 0;
                    onlineuserinfo.Verifycode = ForumUtils.CreateAuthStr(5, false);


                    int olid = Add(onlineuserinfo, timeout);
                    DatabaseProvider.GetInstance().SetUserOnlineState(uid, 1);
                    onlineuserinfo.Olid = olid;

                    HttpCookie cookie = HttpContext.Current.Request.Cookies["dnt"];
                    if (cookie != null)
                    {
                        cookie.Values["tpp"] = ui.Tpp.ToString();
                        cookie.Values["ppp"] = ui.Ppp.ToString();
                        if (HttpContext.Current.Request.Cookies["dnt"]["expires"] != null)
                        {
                            int expires = Utils.StrToInt(HttpContext.Current.Request.Cookies["dnt"]["expires"].ToString(), 0);
                            if (expires > 0)
                            {
                                cookie.Expires = DateTime.Now.AddMinutes(Utils.StrToInt(HttpContext.Current.Request.Cookies["dnt"]["expires"].ToString(), 0));
                            }
                        }
                    }

                    string cookieDomain = GeneralConfigs.GetConfig().CookieDomain.Trim();
                    if (cookieDomain != string.Empty && HttpContext.Current.Request.Url.Host.IndexOf(cookieDomain) > -1 && ForumUtils.IsValidDomain(HttpContext.Current.Request.Url.Host))
                        cookie.Domain = cookieDomain;
                    HttpContext.Current.Response.AppendCookie(cookie);

                }
            }
            else
            {
                onlineuserinfo = CreateGuestUser(timeout);
            }
            return onlineuserinfo;

        }




        /// <summary>
        /// �û�������Ϣά�����жϵ�ǰ�û������(��Ա�����ο�),�Ƿ��������б��д���,�����������»�Ա�ĵ�ǰ��,����������.
        /// </summary>
        /// <param name="passwordkey">��̳passwordkey</param>
        /// <param name="timeout">���߳�ʱʱ��</param>
        /// <param name="passwd">�û�����</param>
        public static OnlineUserInfo UpdateInfo(string passwordkey, int timeout, int uid, string passwd)
        {

            lock (SynObject)
            {
                OnlineUserInfo onlineuser = new OnlineUserInfo();


                string ip = DNTRequest.GetIP();
                int userid = Utils.StrToInt(ForumUtils.GetCookie("userid"), uid);
                string password = (passwd == string.Empty ? ForumUtils.GetCookiePassword(passwordkey) : ForumUtils.GetCookiePassword(passwd, passwordkey));

                if (password.Length == 0)
                {
                    userid = -1;
                }
                // ��������Base64�����ַ������ɱ��Ƿ��۸�, ֱ�������Ϊ�ο�
                else if (!Utils.IsBase64String(password))
                {
                    userid = -1;
                }

                if (userid != -1)
                {
                    onlineuser = GetOnlineUser(userid, password);

                    //��������ͳ��
                    if (!DNTRequest.GetPageName().EndsWith("ajax.aspx") && GeneralConfigs.GetConfig().Statstatus == 1)
                    {
                        Stats.UpdateStatCount(false, onlineuser != null);
                    }

                    if (onlineuser != null)
                    {

                        if (onlineuser.Ip != ip)
                        {
                            UpdateIP(onlineuser.Olid, ip);

                            onlineuser.Ip = ip;

                            return onlineuser;
                        }
                    }
                    else
                    {

                        // �ж������Ƿ���ȷ
                        userid = Users.CheckPassword(userid, password, false);
                        if (userid != -1)
                        {
                            DeleteRowsByIP(ip);
                            return CreateUser(userid, timeout);
                        }
                        else
                        {
                            // ����������������߱��д����ο�
                            onlineuser = GetOnlineUserByIP(-1, ip);
                            if (onlineuser == null)
                            {
                                return CreateGuestUser(timeout);
                            }
                        }
                    }

                }
                else
                {
                    onlineuser = GetOnlineUserByIP(-1, ip);
                    //��������ͳ��
                    if (!DNTRequest.GetPageName().EndsWith("ajax.aspx") && GeneralConfigs.GetConfig().Statstatus == 1)
                    {
                        Stats.UpdateStatCount(true, onlineuser != null);
                    }

                    if (onlineuser == null)
                    {
                        return CreateGuestUser(timeout);
                    }

                }

                //UpdateLastTime(onlineuser.Olid);

                onlineuser.Lastupdatetime = Utils.GetDateTime();
                return onlineuser;

            }

        }

        /// <summary>
        /// �û�������Ϣά�����жϵ�ǰ�û������(��Ա�����ο�),�Ƿ��������б��д���,�����������»�Ա�ĵ�ǰ��,����������.
        /// </summary>
        /// <param name="timeout">���߳�ʱʱ��</param>
        /// <param name="passwd">�û�����</param>
        public static OnlineUserInfo UpdateInfo(string passwordkey, int timeout)
        {
            return UpdateInfo(passwordkey, timeout, -1, "");
        }

        #endregion


        #region �����û���Ϣ����

        /// <summary>
        /// �����û��ĵ�ǰ�����������Ϣ
        /// </summary>
        /// <param name="olid">�����б�id</param>
        /// <param name="action">����</param>
        /// <param name="inid">����λ�ô���</param>
        /// <param name="timeout">����ʱ��</param>
        public static void UpdateAction(int olid, int action, int inid, int timeout)
        {

            // ����ϴ�ˢ��cookie���С��5����, ��ˢ�����ݿ����ʱ��
            if ((timeout < 0) && (Environment.TickCount - Utils.StrToInt(Utils.GetCookie("lastolupdate"), Environment.TickCount) < 300000))
            {
                Utils.WriteCookie("lastolupdate", Environment.TickCount.ToString());
            }
            else
            {
                UpdateAction(olid, action, inid);
            }

        }
        /// <summary>
        /// �����û��ĵ�ǰ�����������Ϣ
        /// </summary>
        /// <param name="olid">�����б�id</param>
        /// <param name="action">����</param>
        /// <param name="inid">����λ�ô���</param>
        public static void UpdateAction(int olid, int action, int inid)
        {
            DatabaseProvider.GetInstance().UpdateAction(olid, action, inid);
        }


        /// <summary>
        /// �����û��ĵ�ǰ�����������Ϣ
        /// </summary>
        /// <param name="olid">�����б�id</param>
        /// <param name="action">����id</param>
        /// <param name="fid">���id</param>
        /// <param name="forumname">�����</param>
        /// <param name="tid">����id</param>
        /// <param name="topictitle">������</param>
        /// <param name="timeout">��ʱʱ��</param>
        public static void UpdateAction(int olid, int action, int fid, string forumname, int tid, string topictitle, int timeout)
        {
            // ����ϴ�ˢ��cookie���С��5����, ��ˢ�����ݿ����ʱ��
            if ((timeout < 0) && (System.Environment.TickCount - Utils.StrToInt(Utils.GetCookie("lastolupdate"), System.Environment.TickCount) < 300000))
            {
                Utils.WriteCookie("lastolupdate", System.Environment.TickCount.ToString());
            }
            else
            {
                if ((action == UserAction.ShowForum.ActionID) || (action == UserAction.PostTopic.ActionID) || (action == UserAction.ShowTopic.ActionID) || (action == UserAction.PostReply.ActionID))
                {
                    forumname = forumname.Length > 40 ? forumname.Substring(0, 37) + "..." : forumname;
                }

                if ((action == UserAction.ShowTopic.ActionID) || (action == UserAction.PostReply.ActionID))
                {
                    topictitle = topictitle.Length > 40 ? topictitle.Substring(0, 37) + "..." : topictitle;
                }
                DatabaseProvider.GetInstance().UpdateAction(olid, action, fid, forumname, tid, topictitle);
            }

        }

        /// <summary>
        /// �����û����ʱ��
        /// </summary>
        /// <param name="olid">����id</param>
        /// <param name="timeout">��ʱʱ��</param>
        private static void UpdateLastTime(int olid, int timeout)
        {

            // ����ϴ�ˢ��cookie���С��5����, ��ˢ�����ݿ����ʱ��
            if ((timeout < 0) && (System.Environment.TickCount - Utils.StrToInt(Utils.GetCookie("lastolupdate"), System.Environment.TickCount) < 300000))
            {
                Utils.WriteCookie("lastolupdate", System.Environment.TickCount.ToString());
            }
            else
            {
                DatabaseProvider.GetInstance().UpdateLastTime(olid);
            }
        }

        /// <summary>
        /// �����û������ʱ��
        /// </summary>
        /// <param name="olid">����id</param>
        public static void UpdatePostTime(int olid)
        {
            DatabaseProvider.GetInstance().UpdatePostTime(olid);
        }

        /// <summary>
        /// �����û���󷢶���Ϣʱ��
        /// </summary>
        /// <param name="olid">����id</param>
        public static void UpdatePostPMTime(int olid)
        {
            DatabaseProvider.GetInstance().UpdatePostPMTime(olid);
        }

        /// <summary>
        /// �������߱���ָ���û��Ƿ�����
        /// </summary>
        /// <param name="olid">����id</param>
        /// <param name="invisible">�Ƿ�����</param>
        public static void UpdateInvisible(int olid, int invisible)
        {
            DatabaseProvider.GetInstance().UpdateInvisible(olid, invisible);
        }

        /// <summary>
        /// �������߱���ָ���û����û�����
        /// </summary>
        /// <param name="olid">����id</param>
        /// <param name="password">�û�����</param>
        public static void UpdatePassword(int olid, string password)
        {
            DatabaseProvider.GetInstance().UpdatePassword(olid, password);
        }


        /// <summary>
        /// �����û�IP��ַ
        /// </summary>
        /// <param name="olid">����id</param>
        /// <param name="ip">ip��ַ</param>
        public static void UpdateIP(int olid, string ip)
        {
            DatabaseProvider.GetInstance().UpdateIP(olid, ip);
        }

        /// <summary>
        /// �����û��������ʱ��
        /// </summary>
        /// <param name="olid">����id</param>
        public static void UpdateSearchTime(int olid)
        {
            DatabaseProvider.GetInstance().UpdateSearchTime(olid);
        }



        /// <summary>
        /// �����û����û���
        /// </summary>
        /// <param name="userid">�û�ID</param>
        /// <param name="groupid">����</param>
        public static void UpdateGroupid(int userid, int groupid)
        {
            DatabaseProvider.GetInstance().UpdateGroupid(userid, groupid);
        }

        #endregion


        #region ɾ�������������û���Ϣ

        /// <summary>
        /// ɾ������������һ�������û���Ϣ
        /// </summary>
        /// <returns>ɾ������</returns>
        private static int DeleteRowsByIP(string ip)
        {
            return DatabaseProvider.GetInstance().DeleteRowsByIP(ip);
        }

        /// <summary>
        /// ɾ�����߱���ָ������id����
        /// </summary>
        /// <param name="olid">����id</param>
        /// <returns></returns>
        public static int DeleteRows(int olid)
        {
            return DatabaseProvider.GetInstance().DeleteRows(olid);
        }


        #endregion


        #region ��������ķ���

        /// <summary>
        /// ���������û��б�
        /// </summary>
        /// <param name="totaluser">ȫ���û���</param>
        /// <param name="guest">�ο���</param>
        /// <param name="user">��¼�û���</param>
        /// <param name="invisibleuser">�����Ա��</param>
        /// <returns></returns>
#if NET1
        public static OnlineUserInfoCollection GetForumOnlineUserCollection(int forumid, out int totaluser, out int guest, out int user, out int invisibleuser)
        {
            OnlineUserInfoCollection coll = new OnlineUserInfoCollection();
#else
        public static Discuz.Common.Generic.List<OnlineUserInfo> GetForumOnlineUserCollection(int forumid, out int totaluser, out int guest, out int user, out int invisibleuser)
        {
            Discuz.Common.Generic.List<OnlineUserInfo> coll = new Discuz.Common.Generic.List<OnlineUserInfo>();
#endif
            //�����ο�
            guest = 0;
            //���������û�
            invisibleuser = 0;
            //�û�����
            totaluser = 0;

            IDataReader reader = DatabaseProvider.GetInstance().GetForumOnlineUserList(forumid);
            while (reader.Read())
            {
                OnlineUserInfo info = LoadSingleOnlineUser(reader);
                //��ǰ����������û���
                totaluser++;
                if (info.Userid == -1)
                {
                    guest++;
                }
                if (info.Invisible == 1)
                {
                    invisibleuser++;
                }
                coll.Add(info);
            }
            reader.Close();

            //ͳ���û�
            user = totaluser - guest;
            //���ص�ǰ���������û���
            return coll;
        }


        /// <summary>
        /// ���������û��б�
        /// </summary>
        /// <param name="totaluser">ȫ���û���</param>
        /// <param name="guest">�ο���</param>
        /// <param name="user">��¼�û���</param>
        /// <param name="invisibleuser">�����Ա��</param>
        /// <returns></returns>
#if NET1
        public static OnlineUserInfoCollection GetOnlineUserCollection(out int totaluser, out int guest, out int user, out int invisibleuser)
		{
			OnlineUserInfoCollection coll = new OnlineUserInfoCollection();
#else
        public static Discuz.Common.Generic.List<OnlineUserInfo> GetOnlineUserCollection(out int totaluser, out int guest, out int user, out int invisibleuser)
        {
            Discuz.Common.Generic.List<OnlineUserInfo> coll = new Discuz.Common.Generic.List<OnlineUserInfo>();
#endif
            //����ע���û���
            user = 0;
            //���������û���
            invisibleuser = 0;
            //�������û���
            totaluser = 0;
            IDataReader reader = DatabaseProvider.GetInstance().GetOnlineUserList();
            while (reader.Read())
            {
                OnlineUserInfo info = LoadSingleOnlineUser(reader);
                //
                if (info.Userid > 0)
                {
                    user++;
                }
                if (info.Invisible == 1)
                {
                    invisibleuser++;
                }
                totaluser++;
                if (info.Userid > 0 || (info.Userid == -1 && GeneralConfigs.GetConfig().Whosonlinecontract == 0))
                {
                    info.Actionname = UserAction.GetActionDescriptionByID((int)(info.Action));
                    coll.Add(info);
                }
            }
            reader.Close();
            int highestonlineusercount = Utils.StrToInt(Statistics.GetStatisticsRowItem("highestonlineusercount"), 1);
            if (totaluser > highestonlineusercount)
            {
                if (Statistics.UpdateStatistics("highestonlineusercount", totaluser) > 0)
                {
                    Statistics.UpdateStatistics("highestonlineusertime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    Statistics.ReSetStatisticsCache();
                }

            }

            //ͳ���ο�
            if (totaluser > user)
            {
                guest = totaluser - user;
            }
            else
            {
                guest = 0;
            }
            //���ص�ǰ���������û�����
            return coll;
        }


        public static void UpdateOnlineTime(int oltimespan, int uid)
        {
            //Ϊ0����ر�ͳ�ƹ���
            if (oltimespan == 0)
            {
                return;
            }
            if (Utils.GetCookie("lastactivity", "onlinetime") == string.Empty)
            {
                Utils.WriteCookie("lastactivity", "onlinetime", System.Environment.TickCount.ToString());
            }

            if ((System.Environment.TickCount - Utils.StrToInt(Utils.GetCookie("lastactivity", "onlinetime"), System.Environment.TickCount) >= oltimespan * 60 * 1000))
            {
                DatabaseProvider.GetInstance().UpdateOnlineTime(oltimespan, uid);
                Utils.WriteCookie("lastactivity", "onlinetime", System.Environment.TickCount.ToString());

                //�ж��Ƿ�ͬ��oltime (��¼��ĵ�һ��onlinetime���µ�ʱ��������߳���1Сʱ)
                if (Utils.GetCookie("lastactivity", "oltime") == string.Empty || (System.Environment.TickCount - Utils.StrToInt(Utils.GetCookie("lastactivity", "onlinetime"), System.Environment.TickCount) >= 60 * 60 * 1000))
                {
                    DatabaseProvider.GetInstance().SynchronizeOltime(uid);
                    Utils.WriteCookie("lastactivity", "oltime", System.Environment.TickCount.ToString());
                }
            }


        }

        #endregion

        #region Helper
        private static OnlineUserInfo LoadSingleOnlineUser(IDataReader reader)
        {
            OnlineUserInfo info = new OnlineUserInfo();
            info.Olid = Int32.Parse(reader["olid"].ToString());
            info.Userid = Int32.Parse(reader["userid"].ToString());
            info.Ip = reader["ip"].ToString();
            info.Username = reader["username"].ToString();
            //info.Tickcount = Int32.Parse(reader["tickcount"].ToString());
            info.Nickname = reader["nickname"].ToString();
            info.Password = reader["password"].ToString();
            info.Groupid = Int16.Parse(reader["groupid"].ToString());
            info.Olimg = reader["olimg"].ToString();
            info.Adminid = Int16.Parse(reader["adminid"].ToString());
            info.Invisible = Int16.Parse(reader["invisible"].ToString());
            info.Action = Int16.Parse(reader["action"].ToString());
            info.Lastactivity = Int16.Parse(reader["lastactivity"].ToString());
            info.Lastposttime = reader["lastposttime"].ToString();
            info.Lastpostpmtime = reader["lastpostpmtime"].ToString();
            info.Lastsearchtime = reader["lastsearchtime"].ToString();
            info.Lastupdatetime = reader["lastupdatetime"].ToString();
            info.Forumid = Int32.Parse(reader["forumid"].ToString());
            if (reader["forumname"] != DBNull.Value)
            {
                info.Forumname = reader["forumname"].ToString();
            }
            info.Titleid = Int32.Parse(reader["titleid"].ToString());
            if (reader["title"] != DBNull.Value)
            {
                info.Title = reader["title"].ToString();
            }
            info.Verifycode = reader["verifycode"].ToString();
            return info;
        }

        private static OnlineUserInfo LoadSingleOnlineUser(DataRow dr)
        {
            OnlineUserInfo info = new OnlineUserInfo();
            info.Olid = Int32.Parse(dr["olid"].ToString());
            info.Userid = Int32.Parse(dr["userid"].ToString());
            info.Ip = dr["ip"].ToString();
            info.Username = dr["username"].ToString();
            //info.Tickcount = Int32.Parse(reader["tickcount"].ToString());
            info.Nickname = dr["nickname"].ToString();
            info.Password = dr["password"].ToString();
            info.Groupid = Int16.Parse(dr["groupid"].ToString());
            info.Olimg = dr["olimg"].ToString();
            info.Adminid = Int16.Parse(dr["adminid"].ToString());
            info.Invisible = Int16.Parse(dr["invisible"].ToString());
            info.Action = Int16.Parse(dr["action"].ToString());
            info.Lastactivity = Int16.Parse(dr["lastactivity"].ToString());
            info.Lastposttime = dr["lastposttime"].ToString();
            info.Lastpostpmtime = dr["lastpostpmtime"].ToString();
            info.Lastsearchtime = dr["lastsearchtime"].ToString();
            info.Lastupdatetime = dr["lastupdatetime"].ToString();
            info.Forumid = Int32.Parse(dr["forumid"].ToString());
            if (dr["forumname"] != DBNull.Value)
            {
                info.Forumname = dr["forumname"].ToString();
            }
            info.Titleid = Int32.Parse(dr["titleid"].ToString());
            if (dr["title"] != DBNull.Value)
            {
                info.Title = dr["title"].ToString();
            }
            info.Verifycode = dr["verifycode"].ToString();
            return info;
        }
        #endregion


        /// <summary>
        /// ����Uid���Olid
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static int GetOlidByUid(int uid)
        {
            return DatabaseProvider.GetInstance().GetOlidByUid(uid);
        }

        /// <summary>
        /// ɾ�����߱���Uid���û�
        /// </summary>
        /// <param name="uid">Ҫɾ���û���Uid</param>
        /// <returns></returns>
        public static int DeleteUserByUid(int uid)
        {
            int olid = GetOlidByUid(uid);
            return DeleteRows(olid);
        }
    }//class end
}
