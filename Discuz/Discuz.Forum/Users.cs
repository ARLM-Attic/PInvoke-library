using System;
using System.Data;
using System.Data.Common;
using System.Text;

using Discuz.Common;
using Discuz.Data;
using Discuz.Config;
using Discuz.Entity;
using System.Collections;
using Discuz.Cache;

namespace Discuz.Forum
{
	/// <summary>
	/// �û�������
	/// </summary>
	public class Users
	{

		/// <summary>
		/// ����ָ���û�����Ϣ
		/// </summary>
		/// <param name="uid">�û�id</param>
		/// <returns>�û���Ϣ</returns>
        public static IDataReader GetUserInfoToReader(int uid)
		{
            return DatabaseProvider.GetInstance().GetUserInfoToReader(uid);
		}

		/// <summary>
		/// ��ȡ����û���Ϣ
		/// </summary>
		/// <param name="uid">��id</param>
		/// <returns>�û������Ϣ</returns>
        public static IDataReader GetShortUserInfoToReader(int uid)
		{
            return DatabaseProvider.GetInstance().GetShortUserInfoToReader(uid);
		}


		/// <summary>
		/// ����ָ���û���������Ϣ
		/// </summary>
		/// <param name="uid">�û�id</param>
		/// <returns>�û���Ϣ</returns>
		public static UserInfo GetUserInfo(int uid)
		{
			UserInfo userinfo = new UserInfo();

            IDataReader reader = DatabaseProvider.GetInstance().GetUserInfoToReader(uid);
			
			if(reader.Read())
			{
				userinfo.Uid = Int32.Parse(reader["uid"].ToString());
				userinfo.Username = reader["username"].ToString();
				userinfo.Nickname = reader["nickname"].ToString();
				userinfo.Password = reader["password"].ToString();
				userinfo.Spaceid = Int32.Parse(reader["spaceid"].ToString());
				userinfo.Secques = reader["secques"].ToString();
				userinfo.Gender = Int32.Parse(reader["gender"].ToString());
				userinfo.Adminid = Int32.Parse(reader["adminid"].ToString());
				userinfo.Groupid = Int16.Parse(reader["groupid"].ToString());
				userinfo.Groupexpiry = Int32.Parse(reader["groupexpiry"].ToString());
				userinfo.Extgroupids = reader["extgroupids"].ToString();
				userinfo.Regip = reader["regip"].ToString();
				userinfo.Joindate = Utils.GetStandardDateTime(reader["joindate"].ToString());
				userinfo.Lastip = reader["lastip"].ToString();
				userinfo.Lastvisit = Utils.GetStandardDateTime(reader["lastvisit"].ToString());
				userinfo.Lastactivity = Utils.GetStandardDateTime(reader["lastactivity"].ToString());
				userinfo.Lastpost = Utils.GetStandardDateTime(reader["lastpost"].ToString());
				userinfo.Lastpostid = Int32.Parse(reader["lastpostid"].ToString());
				userinfo.Lastposttitle = reader["lastposttitle"].ToString();
				userinfo.Posts = Int32.Parse(reader["posts"].ToString());
				userinfo.Digestposts = Int16.Parse(reader["digestposts"].ToString());
				userinfo.Oltime = Int32.Parse(reader["oltime"].ToString());
				userinfo.Pageviews = Int32.Parse(reader["pageviews"].ToString());
				userinfo.Credits = Int32.Parse(reader["credits"].ToString());
				userinfo.Extcredits1 = float.Parse(reader["extcredits1"].ToString());
				userinfo.Extcredits2 = float.Parse(reader["extcredits2"].ToString());
				userinfo.Extcredits3 = float.Parse(reader["extcredits3"].ToString());
				userinfo.Extcredits4 = float.Parse(reader["extcredits4"].ToString());
				userinfo.Extcredits5 = float.Parse(reader["extcredits5"].ToString());
				userinfo.Extcredits6 = float.Parse(reader["extcredits6"].ToString());
				userinfo.Extcredits7 = float.Parse(reader["extcredits7"].ToString());
				userinfo.Extcredits8 = float.Parse(reader["extcredits8"].ToString());
				userinfo.Avatarshowid = Int32.Parse(reader["avatarshowid"].ToString());
				userinfo.Medals = reader["medals"].ToString();
				userinfo.Email = reader["email"].ToString();
				userinfo.Bday = reader["bday"].ToString();
				userinfo.Sigstatus = Int32.Parse(reader["sigstatus"].ToString());
				userinfo.Tpp = Int32.Parse(reader["tpp"].ToString());
				userinfo.Ppp = Int32.Parse(reader["ppp"].ToString());
				userinfo.Templateid = Int16.Parse(reader["templateid"].ToString());
				userinfo.Pmsound = Int32.Parse(reader["pmsound"].ToString());
				userinfo.Showemail = Int32.Parse(reader["showemail"].ToString());
                userinfo.Newsletter = (ReceivePMSettingType)Int32.Parse(reader["newsletter"].ToString());
				userinfo.Invisible = Int32.Parse(reader["invisible"].ToString());
				//__userinfo.Timeoffset = reader["timeoffset"].ToString();
				userinfo.Newpm = Int32.Parse(reader["newpm"].ToString());
				userinfo.Newpmcount = Int32.Parse(reader["newpmcount"].ToString());
				userinfo.Accessmasks = Int32.Parse(reader["accessmasks"].ToString());
				userinfo.Onlinestate = Int32.Parse(reader["onlinestate"].ToString());
				//
				userinfo.Website = reader["website"].ToString();
				userinfo.Icq = reader["icq"].ToString();
				userinfo.Qq = reader["qq"].ToString();
				userinfo.Yahoo = reader["yahoo"].ToString();
				userinfo.Msn = reader["msn"].ToString();
				userinfo.Skype = reader["skype"].ToString();
				userinfo.Location = reader["location"].ToString();
				userinfo.Customstatus = reader["customstatus"].ToString();
				userinfo.Avatar = reader["avatar"].ToString();
				userinfo.Avatarwidth = Int32.Parse(reader["avatarwidth"].ToString());
				userinfo.Avatarheight = Int32.Parse(reader["avatarheight"].ToString());
				userinfo.Bio = reader["bio"].ToString();
				userinfo.Signature = reader["signature"].ToString();
				userinfo.Sightml = reader["sightml"].ToString();
				userinfo.Authstr = reader["authstr"].ToString();
				userinfo.Authtime = reader["authtime"].ToString();
				userinfo.Authflag = Byte.Parse(reader["authflag"].ToString());
                userinfo.Realname = reader["realname"].ToString();
                userinfo.Idcard = reader["idcard"].ToString();
                userinfo.Mobile = reader["mobile"].ToString();
                userinfo.Phone = reader["phone"].ToString();

				reader.Close();
				return userinfo;
			}
			reader.Close();
			return null;
		}

		/// <summary>
		/// ����ָ���û��ļ����Ϣ
		/// </summary>
		/// <param name="uid">�û�id</param>
		/// <returns>�û���Ϣ</returns>
		public static ShortUserInfo GetShortUserInfo(int uid)
		{
			ShortUserInfo userinfo = new ShortUserInfo();

            IDataReader reader = DatabaseProvider.GetInstance().GetShortUserInfoToReader(uid);
			
			if(reader.Read())
			{
				userinfo.Uid = Int32.Parse(reader["uid"].ToString());
				userinfo.Username = reader["username"].ToString();
				userinfo.Nickname = reader["nickname"].ToString();
				userinfo.Password = reader["password"].ToString();
				userinfo.Spaceid = Int32.Parse(reader["spaceid"].ToString());
				userinfo.Secques = reader["secques"].ToString();
				userinfo.Gender = Int32.Parse(reader["gender"].ToString());
				userinfo.Adminid = Int32.Parse(reader["adminid"].ToString());
				userinfo.Groupid = Int16.Parse(reader["groupid"].ToString());
				userinfo.Groupexpiry = Int32.Parse(reader["groupexpiry"].ToString());
				userinfo.Extgroupids = reader["extgroupids"].ToString();
				userinfo.Regip = reader["regip"].ToString();
				userinfo.Joindate = reader["joindate"].ToString();
				userinfo.Lastip = reader["lastip"].ToString();
				userinfo.Lastvisit = reader["lastvisit"].ToString();
				userinfo.Lastactivity = reader["lastactivity"].ToString();
				userinfo.Lastpost = reader["lastpost"].ToString();
				userinfo.Lastpostid = Int32.Parse(reader["lastpostid"].ToString());
				userinfo.Lastposttitle = reader["lastposttitle"].ToString();
				userinfo.Posts = Int32.Parse(reader["posts"].ToString());
				userinfo.Digestposts = Int16.Parse(reader["digestposts"].ToString());
				userinfo.Oltime = Int32.Parse(reader["oltime"].ToString());
				userinfo.Pageviews = Int32.Parse(reader["pageviews"].ToString());
				userinfo.Credits = Int32.Parse(reader["credits"].ToString());
				userinfo.Extcredits1 = float.Parse(reader["extcredits1"].ToString());
				userinfo.Extcredits2 = float.Parse(reader["extcredits2"].ToString());
				userinfo.Extcredits3 = float.Parse(reader["extcredits3"].ToString());
				userinfo.Extcredits4 = float.Parse(reader["extcredits4"].ToString());
				userinfo.Extcredits5 = float.Parse(reader["extcredits5"].ToString());
				userinfo.Extcredits6 = float.Parse(reader["extcredits6"].ToString());
				userinfo.Extcredits7 = float.Parse(reader["extcredits7"].ToString());
				userinfo.Extcredits8 = float.Parse(reader["extcredits8"].ToString());
				userinfo.Avatarshowid = Int32.Parse(reader["avatarshowid"].ToString());
				userinfo.Email = reader["email"].ToString();
				userinfo.Bday = reader["bday"].ToString();
				userinfo.Sigstatus = Int32.Parse(reader["sigstatus"].ToString());
				userinfo.Tpp = Int32.Parse(reader["tpp"].ToString());
				userinfo.Ppp = Int32.Parse(reader["ppp"].ToString());
				userinfo.Templateid = Int16.Parse(reader["templateid"].ToString());
				userinfo.Pmsound = Int32.Parse(reader["pmsound"].ToString());
				userinfo.Showemail = Int32.Parse(reader["showemail"].ToString());
                userinfo.Newsletter = (ReceivePMSettingType)Int32.Parse(reader["newsletter"].ToString());
				userinfo.Invisible = Int32.Parse(reader["invisible"].ToString());
				//__userinfo.Timeoffset = reader["timeoffset"].ToString();
				userinfo.Newpm = Int32.Parse(reader["newpm"].ToString());
				userinfo.Newpmcount = Int32.Parse(reader["newpmcount"].ToString());
				userinfo.Accessmasks = Int32.Parse(reader["accessmasks"].ToString());
				userinfo.Onlinestate = Int32.Parse(reader["onlinestate"].ToString());

				reader.Close();
				return userinfo;
			}
			reader.Close();
			return null;
		}

		/// <summary>
		/// ����IP�����û�
		/// </summary>
		/// <param name="ip">ip��ַ</param>
		/// <returns>�û���Ϣ</returns>
		public static UserInfo GetUserInfoByIP(string ip)
		{
			UserInfo userinfo = new UserInfo();

            IDataReader reader = DatabaseProvider.GetInstance().GetUserInfoByIP(ip);
			if(reader.Read())
			{
				userinfo.Uid = Int32.Parse(reader["uid"].ToString());
				userinfo.Username = reader["username"].ToString();
				userinfo.Nickname = reader["nickname"].ToString();
				userinfo.Password = reader["password"].ToString();
				userinfo.Secques = reader["secques"].ToString();
				userinfo.Gender = Int32.Parse(reader["gender"].ToString());
				userinfo.Adminid = Int32.Parse(reader["adminid"].ToString());
				userinfo.Groupid = Int16.Parse(reader["groupid"].ToString());
				userinfo.Groupexpiry = Int32.Parse(reader["groupexpiry"].ToString());
				userinfo.Extgroupids = reader["extgroupids"].ToString();
				userinfo.Regip = reader["regip"].ToString();
				userinfo.Joindate = reader["joindate"].ToString();
				userinfo.Lastip = reader["lastip"].ToString();
				userinfo.Lastvisit = reader["lastvisit"].ToString();
				userinfo.Lastactivity = reader["lastactivity"].ToString();
				userinfo.Lastpost = reader["lastpost"].ToString();
				userinfo.Lastpostid = Int32.Parse(reader["lastpostid"].ToString());
				userinfo.Lastposttitle = reader["lastposttitle"].ToString();
				userinfo.Posts = Int32.Parse(reader["posts"].ToString());
				userinfo.Digestposts = Int16.Parse(reader["digestposts"].ToString());
				userinfo.Oltime = Int32.Parse(reader["oltime"].ToString());
				userinfo.Pageviews = Int32.Parse(reader["pageviews"].ToString());
				userinfo.Credits = Int32.Parse(reader["credits"].ToString());
				userinfo.Extcredits1 = float.Parse(reader["extcredits1"].ToString());
				userinfo.Extcredits2 = float.Parse(reader["extcredits2"].ToString());
				userinfo.Extcredits3 = float.Parse(reader["extcredits3"].ToString());
				userinfo.Extcredits4 = float.Parse(reader["extcredits4"].ToString());
				userinfo.Extcredits5 = float.Parse(reader["extcredits5"].ToString());
				userinfo.Extcredits6 = float.Parse(reader["extcredits6"].ToString());
				userinfo.Extcredits7 = float.Parse(reader["extcredits7"].ToString());
				userinfo.Extcredits8 = float.Parse(reader["extcredits8"].ToString());
				userinfo.Avatarshowid = Int32.Parse(reader["avatarshowid"].ToString());
				userinfo.Medals = reader["medals"].ToString();
				userinfo.Email = reader["email"].ToString();
				userinfo.Bday = reader["bday"].ToString();
				userinfo.Sigstatus = Int32.Parse(reader["sigstatus"].ToString());
				userinfo.Tpp = Int32.Parse(reader["tpp"].ToString());
				userinfo.Ppp = Int32.Parse(reader["ppp"].ToString());
				userinfo.Templateid = Int16.Parse(reader["templateid"].ToString());
				userinfo.Pmsound = Int32.Parse(reader["pmsound"].ToString());
				userinfo.Showemail = Int32.Parse(reader["showemail"].ToString());
                userinfo.Newsletter = (ReceivePMSettingType)Int32.Parse(reader["newsletter"].ToString());
				userinfo.Invisible = Int32.Parse(reader["invisible"].ToString());
				//__userinfo.Timeoffset = reader["timeoffset"].ToString();
				userinfo.Newpm = Int32.Parse(reader["newpm"].ToString());
				userinfo.Newpmcount = Int32.Parse(reader["newpmcount"].ToString());
				userinfo.Accessmasks = Int32.Parse(reader["accessmasks"].ToString());
				userinfo.Onlinestate = Int32.Parse(reader["onlinestate"].ToString());
				//
				userinfo.Website = reader["website"].ToString();
				userinfo.Icq = reader["icq"].ToString();
				userinfo.Qq = reader["qq"].ToString();
				userinfo.Yahoo = reader["yahoo"].ToString();
				userinfo.Msn = reader["msn"].ToString();
				userinfo.Skype = reader["skype"].ToString();
				userinfo.Location = reader["location"].ToString();
				userinfo.Customstatus = reader["customstatus"].ToString();
				userinfo.Avatar = reader["avatar"].ToString();
				userinfo.Avatarwidth = Int32.Parse(reader["avatarwidth"].ToString());
				userinfo.Avatarheight = Int32.Parse(reader["avatarheight"].ToString());
				userinfo.Bio = reader["bio"].ToString();
				userinfo.Signature = reader["signature"].ToString();
				userinfo.Sightml = reader["sightml"].ToString();
				userinfo.Authstr = reader["authstr"].ToString();
				reader.Close();
				return userinfo;
			}
			reader.Close();
			return null;
		}

		/// <summary>
		/// ����IP�����û�
		/// </summary>
		/// <param name="ip">ip��ַ</param>
		/// <returns>�û���Ϣ</returns>
		public static ShortUserInfo GetShortUserInfoByIP(string ip)
		{
			ShortUserInfo __userinfo = new ShortUserInfo();

            IDataReader reader = DatabaseProvider.GetInstance().GetUserInfoByIP(ip);

			if(reader.Read())
			{
				__userinfo.Uid = Int32.Parse(reader["uid"].ToString());
				__userinfo.Username = reader["username"].ToString();
				__userinfo.Nickname = reader["nickname"].ToString();
				__userinfo.Password = reader["password"].ToString();
				__userinfo.Secques = reader["secques"].ToString();
				__userinfo.Gender = Int32.Parse(reader["gender"].ToString());
				__userinfo.Adminid = Int32.Parse(reader["adminid"].ToString());
				__userinfo.Groupid = Int16.Parse(reader["groupid"].ToString());
				__userinfo.Groupexpiry = Int32.Parse(reader["groupexpiry"].ToString());
				__userinfo.Extgroupids = reader["extgroupids"].ToString();
				__userinfo.Regip = reader["regip"].ToString();
				__userinfo.Joindate = reader["joindate"].ToString();
				__userinfo.Lastip = reader["lastip"].ToString();
				__userinfo.Lastvisit = reader["lastvisit"].ToString();
				__userinfo.Lastactivity = reader["lastactivity"].ToString();
				__userinfo.Lastpost = reader["lastpost"].ToString();
				__userinfo.Lastpostid = Int32.Parse(reader["lastpostid"].ToString());
				__userinfo.Lastposttitle = reader["lastposttitle"].ToString();
				__userinfo.Posts = Int32.Parse(reader["posts"].ToString());
				__userinfo.Digestposts = Int16.Parse(reader["digestposts"].ToString());
				__userinfo.Oltime = Int32.Parse(reader["oltime"].ToString());
				__userinfo.Pageviews = Int32.Parse(reader["pageviews"].ToString());
				__userinfo.Credits = Int32.Parse(reader["credits"].ToString());
				__userinfo.Extcredits1 = float.Parse(reader["extcredits1"].ToString());
				__userinfo.Extcredits2 = float.Parse(reader["extcredits2"].ToString());
				__userinfo.Extcredits3 = float.Parse(reader["extcredits3"].ToString());
				__userinfo.Extcredits4 = float.Parse(reader["extcredits4"].ToString());
				__userinfo.Extcredits5 = float.Parse(reader["extcredits5"].ToString());
				__userinfo.Extcredits6 = float.Parse(reader["extcredits6"].ToString());
				__userinfo.Extcredits7 = float.Parse(reader["extcredits7"].ToString());
				__userinfo.Extcredits8 = float.Parse(reader["extcredits8"].ToString());
				__userinfo.Avatarshowid = Int32.Parse(reader["avatarshowid"].ToString());
				__userinfo.Email = reader["email"].ToString();
				__userinfo.Bday = reader["bday"].ToString();
				__userinfo.Sigstatus = Int32.Parse(reader["sigstatus"].ToString());
				__userinfo.Tpp = Int32.Parse(reader["tpp"].ToString());
				__userinfo.Ppp = Int32.Parse(reader["ppp"].ToString());
				__userinfo.Templateid = Int16.Parse(reader["templateid"].ToString());
				__userinfo.Pmsound = Int32.Parse(reader["pmsound"].ToString());
				__userinfo.Showemail = Int32.Parse(reader["showemail"].ToString());
                __userinfo.Newsletter = (ReceivePMSettingType)Int32.Parse(reader["newsletter"].ToString());
				__userinfo.Invisible = Int32.Parse(reader["invisible"].ToString());
				//__userinfo.Timeoffset = reader["timeoffset"].ToString();
				__userinfo.Newpm = Int32.Parse(reader["newpm"].ToString());
				__userinfo.Newpmcount = Int32.Parse(reader["newpmcount"].ToString());
				__userinfo.Accessmasks = Int32.Parse(reader["accessmasks"].ToString());
				__userinfo.Onlinestate = Int32.Parse(reader["onlinestate"].ToString());
				//

				reader.Close();
				return __userinfo;
			}
			reader.Close();
			return null;
		}

		/// <summary>
		/// �����û�id�����û���
		/// </summary>
		/// <param name="uid">�û�id</param>
		/// <returns>�û���</returns>
		public static string GetUserName(int uid)
		{
            IDataReader reader = DatabaseProvider.GetInstance().GetUserName(uid);
            string username = "";
			if(reader.Read())
			{
				username = reader[0].ToString();
			}
			reader.Close();
			return username;
		}

		/// <summary>
		/// �����û�id�����û�ע������
		/// </summary>
		/// <param name="uid">�û�id</param>
		/// <returns>�û�ע������</returns>
		public static string GetUserJoinDate(int uid)
		{
            IDataReader reader = DatabaseProvider.GetInstance().GetUserJoinDate(uid);
            string userjoindate = "";
			if(reader.Read())
			{
				userjoindate = reader[0].ToString();
			}
			reader.Close();
			return userjoindate;
		}

		/// <summary>
		/// �����û��������û�id
		/// </summary>
		/// <param name="username">�û���</param>
		/// <returns>�û�id</returns>
		public static int GetUserID(string username)
		{
            IDataReader reader = DatabaseProvider.GetInstance().GetUserID(username);
            int userid = -1;
			if(reader.Read())
			{
				userid = Int32.Parse(reader[0].ToString());
			}
			reader.Close();
			return userid;
		}

		/// <summary>
		/// ����û��б�DataTable
		/// </summary>
		/// <param name="pagesize">ÿҳ��¼��</param>
		/// <param name="pageindex">��ǰҳ��</param>
		/// <returns>�û��б�DataTable</returns>
        public static DataTable GetUserList(int pagesize, int pageindex, string orderby, string ordertype)
		{
            DataTable dt = DatabaseProvider.GetInstance().GetUserList(pagesize, pageindex, orderby, ordertype);
		    dt.Columns.Add("grouptitle");
            dt.Columns.Add("olimg");
		    foreach (DataRow dataRow in dt.Rows)
		    {
                UserGroupInfo group = UserGroups.GetUserGroupInfo(Utils.StrToInt(dataRow["groupid"], 0));
                if (group.Color == string.Empty)
                {
                    dataRow["grouptitle"] = group.Grouptitle;
                }
                else
                {
                    dataRow["grouptitle"] = string.Format("<font color='{1}'>{0}</font>", group.Grouptitle, group.Color);
                }
		        dataRow["olimg"] = OnlineUsers.GetGroupImg(Utils.StrToInt(dataRow["groupid"], 0));
		    }
		    return dt;
		}


		/// <summary>
		/// �ж�ָ���û����Ƿ��Ѵ���
		/// </summary>
		/// <param name="uid">�û�id</param>
		/// <returns>����Ѵ��ڸ��û�id�򷵻�true, ���򷵻�false</returns>
		public static bool Exists(int uid)
		{
            return DatabaseProvider.GetInstance().Exists(uid);	
        }

		/// <summary>
		/// �ж�ָ���û����Ƿ��Ѵ���.
		/// </summary>
		/// <param name="username">�û���</param>
		/// <returns>����Ѵ��ڸ��û����򷵻�true, ���򷵻�false</returns>
		public static bool Exists(string username)
		{
            return DatabaseProvider.GetInstance().Exists(username);	
        }


		/// <summary>
		/// �Ƿ���ָ��ip��ַ���û�ע��
		/// </summary>
		/// <param name="ip">ip��ַ</param>
		/// <returns>���ڷ���true,���򷵻�false</returns>
		public static bool ExistsByIP(string ip)
		{
            return DatabaseProvider.GetInstance().ExistsByIP(ip);	
        }

		/// <summary>
		/// ���Email�Ͱ�ȫ��
		/// </summary>
		/// <param name="username">�û���</param>
		/// <param name="email">email</param>
		/// <param name="questionid">����id</param>
		/// <param name="answer">��</param>
		/// <returns>�����ȷ�򷵻��û�id, ���򷵻�-1</returns>
		public static int CheckEmailAndSecques(string username, string email, int questionid, string answer)
		{
            IDataReader reader = DatabaseProvider.GetInstance().CheckEmailAndSecques(username, email, ForumUtils.GetUserSecques(questionid, answer));
			int userid = -1;
			if(reader.Read())
			{
				userid = Int32.Parse(reader[0].ToString());
			}
			reader.Close();
			return userid;
		}


		/// <summary>
		/// �������Ͱ�ȫ��
		/// </summary>
		/// <param name="username">�û���</param>
		/// <param name="password">����</param>
		/// <param name="originalpassword">�Ƿ��MD5����</param>
		/// <param name="questionid">����id</param>
		/// <param name="answer">��</param>
		/// <returns>�����ȷ�򷵻��û�id, ���򷵻�-1</returns>
		public static int CheckPasswordAndSecques(string username, string password, bool originalpassword, int questionid, string answer)
		{
            IDataReader reader = DatabaseProvider.GetInstance().CheckPasswordAndSecques(username, password, originalpassword, ForumUtils.GetUserSecques(questionid, answer));
			int userid = -1;
			if(reader.Read())
			{
				userid = Int32.Parse(reader[0].ToString());
			}
			reader.Close();
			return userid;
		}

		/// <summary>
		/// �������
		/// </summary>
		/// <param name="username">�û���</param>
		/// <param name="password">����</param>
		/// <param name="originalpassword">�Ƿ��MD5����</param>
		/// <returns>�����ȷ�򷵻��û�id, ���򷵻�-1</returns>
		public static int CheckPassword(string username, string password, bool originalpassword)
		{
            IDataReader reader = DatabaseProvider.GetInstance().CheckPassword(username, password, originalpassword);
            int userid = -1;
			if(reader.Read())
			{
				userid = Int32.Parse(reader[0].ToString());
			}
			reader.Close();
			return userid;
		}



		/// <summary>
		/// ���DVBBS����ģʽ������
		/// </summary>
		/// <param name="username">�û���</param>
		/// <param name="password">����</param>
		/// <param name="questionid">����id</param>
		/// <param name="answer">��</param>
		/// <returns>�����ȷ�򷵻��û�id, ���򷵻�-1</returns>
		public static int CheckDvBbsPasswordAndSecques(string username, string password, int questionid, string answer)
		{

            IDataReader reader = DatabaseProvider.GetInstance().CheckDvBbsPasswordAndSecques(username, password);
			int uid = -1;	
			if(reader.Read())
			{
				if (reader["secques"].ToString().Trim() != ForumUtils.GetUserSecques(questionid, answer))
				{
					return -1;
				}
				string pw = reader["password"].ToString().Trim();

				if (pw.Length > 16)
				{
					if (Utils.MD5(password) == pw)
					{
						uid = Utils.StrToInt(reader["uid"].ToString(), -1);
					}
				}
				else
				{
					if (Utils.MD5(password).Substring(8, 16) == pw)
					{
						uid = Utils.StrToInt(reader["uid"].ToString(), -1);
						UpdateUserPassword(uid, password);
					}
				}
			}
			reader.Close();
			return uid;
		}

		/// <summary>
		/// ���DVBBS����ģʽ������
		/// </summary>
		/// <param name="username">�û���</param>
		/// <param name="password">����</param>
		/// <returns>�����ȷ�򷵻��û�id, ���򷵻�-1</returns>
		public static int CheckDvBbsPassword(string username, string password)
		{

            IDataReader reader = DatabaseProvider.GetInstance().CheckDvBbsPasswordAndSecques(username, password);
			int uid = -1;	
			if(reader.Read())
			{
				string pw = reader["password"].ToString().Trim();

				if (pw.Length > 16)
				{
					if (Utils.MD5(password) == pw)
					{
						uid = Utils.StrToInt(reader["uid"].ToString(), -1);
					}
				}
				else
				{
					if (Utils.MD5(password).Substring(8, 16) == pw)
					{
						uid = Utils.StrToInt(reader["uid"].ToString(), -1);
						UpdateUserPassword(uid, password);
					}
				}
			}
			reader.Close();
			return uid;
		}
		

		/// <summary>
		/// �ж��û������Ƿ���ȷ
		/// </summary>
		/// <param name="username">�û���</param>
		/// <param name="password">����</param>
		/// <param name="originalpassword">�Ƿ�ΪδMD5����</param>
		/// <param name="groupid">�û���ID</param>
		/// <param name="adminid">������ID</param>
		/// <returns>�����ȷ�򷵻�uid</returns>
		public static int CheckPassword(string username, string password, bool originalpassword, out int groupid, out int adminid)
		{
            IDataReader reader = DatabaseProvider.GetInstance().CheckPassword(username, password, originalpassword);

			int uid = -1;
			groupid = 7;
			adminid = 0;

			if(reader.Read())
			{
				uid = Utils.StrToInt(reader[0].ToString(), -1);
				groupid = Utils.StrToInt(reader[1].ToString(), -1);				
				adminid = Utils.StrToInt(reader[2].ToString(), -1);
				
			}
			reader.Close();
			return uid;
		}

		/// <summary>
		/// �������
		/// </summary>
		/// <param name="uid">�û�id</param>
		/// <param name="password">����</param>
		/// <param name="originalpassword">�Ƿ��MD5����</param>
		/// <param name="groupid">�û���id</param>
		/// <param name="adminid">����id</param>
		/// <returns>����û�������ȷ�򷵻�uid, ���򷵻�-1</returns>
		public static int CheckPassword(int uid, string password, bool originalpassword, out int groupid, out int adminid)
		{
            IDataReader reader = DatabaseProvider.GetInstance().CheckPassword(uid, password, originalpassword);
			
			uid = -1;
			groupid = 7;
			adminid = 0;

			if(reader.Read())
			{
				uid = Utils.StrToInt(reader[0].ToString(), -1);
				groupid = Utils.StrToInt(reader[1].ToString(), -1);				
				adminid = Utils.StrToInt(reader[2].ToString(), -1);
				
			}
			reader.Close();
			return uid;
		}


		/// <summary>
		/// �ж�ָ���û������Ƿ���ȷ.
		/// </summary>
		/// <param name="uid">�û�ID</param>
		/// <param name="password">�û�����</param>
		/// <returns>����û�������ȷ�򷵻�true, ���򷵻�false</returns>
		public static int CheckPassword(int uid, string password, bool originalpassword)
		{
            IDataReader reader = DatabaseProvider.GetInstance().CheckPassword(uid, password, originalpassword);

			uid = -1;
			if(reader.Read())
			{
				uid = Utils.StrToInt(reader[0].ToString(), -1);
			}
			reader.Close();
			return uid;
		}

		/// <summary>
		/// ����ָ����email�����û��������û�uid
		/// </summary>
		/// <param name="email">email��ַ</param>
		/// <returns>�û�uid</returns>
		public static int FindUserEmail(string email)
		{
            IDataReader reader = DatabaseProvider.GetInstance().FindUserEmail(email);

            int uid = -1;
			if(reader.Read())
			{
				uid = Utils.StrToInt(reader[0].ToString(), -1);
			}
			reader.Close();
			return uid;
		}


		/// <summary>
		/// �õ���̳���û�����
		/// </summary>
		/// <returns>�û�����</returns>
		public static int GetUserCount()
		{
            return DatabaseProvider.GetInstance().GetUserCount();
		}

		/// <summary>
		/// �õ���̳���û�����
		/// </summary>
		/// <returns>�û�����</returns>
		public static int GetUserCountByAdmin()
		{
            return DatabaseProvider.GetInstance().GetUserCountByAdmin();	
        }

		/// <summary>
		/// �������û�.
		/// </summary>
		/// <param name="__userinfo">�û���Ϣ</param>
		/// <returns>�����û�ID, ����Ѵ��ڸ��û����򷵻�-1</returns>
		public static int CreateUser(UserInfo __userinfo)
		{
			if(Exists(__userinfo.Username))
			{
				return -1;
			}

            return DatabaseProvider.GetInstance().CreateUser(__userinfo);
		}

		/// <summary>
		/// ����Ȩ����֤�ַ���
		/// </summary>
		/// <param name="uid">�û�id</param>
		public static void UpdateAuthStr(int uid)
		{
			
			string authstr = ForumUtils.CreateAuthStr(20);

			UpdateAuthStr(uid,authstr,1);
		}

		/// <summary>
		/// ����Ȩ����֤�ַ���
		/// </summary>
		/// <param name="uid">�û�id</param>
		/// <param name="authstr">��֤��</param>
		/// <param name="authflag">��֤��־</param>
		public static void UpdateAuthStr(int uid, string authstr, int authflag)
		{
            DatabaseProvider.GetInstance().UpdateAuthStr(uid, authstr, authflag);
		}


		/// <summary>
		/// ����ָ���û��ĸ�������
		/// </summary>
		/// <param name="__userinfo">�û���Ϣ</param>
		/// <returns>����û���������Ϊfalse, ����Ϊtrue</returns>
		public static bool UpdateUserProfile(UserInfo __userinfo)
		{
			if(!Exists(__userinfo.Uid))
			{
				return false;
			}

            DatabaseProvider.GetInstance().UpdateUserProfile(__userinfo);
			return true;
		}

		/// <summary>
		/// �����û���̳����
		/// </summary>
		/// <param name="__userinfo">�û���Ϣ</param>
		/// <returns>����û��������򷵻�false, ���򷵻�true</returns>
		public static bool UpdateUserForumSetting(UserInfo __userinfo)
		{
			if(!Exists(__userinfo.Uid))
			{
				return false;
			}

            DatabaseProvider.GetInstance().UpdateUserForumSetting(__userinfo);
			return true;
		}

		/// <summary>
		/// �޸��û��Զ�������ֶε�ֵ
		/// </summary>
		/// <param name="uid">�û�id</param>
		/// <param name="extid">��չ�ֶ����(1-8)</param>
		/// <param name="pos">���ӵ���ֵ(�����Ǹ���)</param>
		/// <returns>ִ���Ƿ�ɹ�</returns>
		public static void UpdateUserExtCredits(int uid, int extid, float pos)
		{
            DatabaseProvider.GetInstance().UpdateUserExtCredits(uid, extid, pos);	
        }

		/// <summary>
		/// ���ָ���û���ָ��������չ�ֶε�ֵ
		/// </summary>
		/// <param name="uid">�û�id</param>
		/// <param name="extid">��չ�ֶ����(1-8)</param>
		/// <returns>ֵ</returns>
		public static float GetUserExtCredits(int uid, int extid)
		{
            return DatabaseProvider.GetInstance().GetUserExtCredits(uid, extid);	
        }

		/// <summary>
		/// �����û�ͷ��
		/// </summary>
		/// <param name="uid">�û�id</param>
		/// <param name="avatar">ͷ��</param>
		/// <param name="avatarwidth">ͷ����</param>
		/// <param name="avatarheight">ͷ��߶�</param>
        /// <param name="templateid">ģ��Id</param>
		/// <returns>����û��������򷵻�false, ���򷵻�true</returns>
        public static bool UpdateUserPreference(int uid, string avatar, int avatarwidth, int avatarheight, int templateid)
		{
			if(!Exists(uid))
			{
				return false;
			}

            DatabaseProvider.GetInstance().UpdateUserPreference(uid, avatar, avatarwidth, avatarheight, templateid);
			return true;
		}

		/// <summary>
		/// �����û�����
		/// </summary>
		/// <param name="uid">�û�id</param>
		/// <param name="password">�û�����</param>
		/// <returns>����û��������򷵻�false, ���򷵻�true</returns>
		public static bool UpdateUserPassword(int uid, string password)
		{
			return UpdateUserPassword(uid, password, true);
		}

		/// <summary>
		/// �����û�����
		/// </summary>
		/// <param name="uid">�û�id</param>
		/// <param name="password">����</param>
		/// <param name="originalpassword">�Ƿ��MD5����</param>
		/// <returns>�ɹ�����true����false</returns>
		public static bool UpdateUserPassword(int uid, string password, bool originalpassword)
		{
			if(!Exists(uid))
			{
				return false;
			}

            DatabaseProvider.GetInstance().UpdateUserPassword(uid, password, originalpassword);
			return true;
		}

		/// <summary>
		/// �����û���ȫ����
		/// </summary>
		/// <param name="uid">�û�id</param>
		/// <param name="questionid">����id</param>
		/// <param name="answer">��</param>
		/// <returns>�ɹ�����true����false</returns>
		public static bool UpdateUserSecques(int uid, int questionid, string answer)
		{
			if(!Exists(uid))
			{
				return false;
			}

            DatabaseProvider.GetInstance().UpdateUserSecques(uid, ForumUtils.GetUserSecques(questionid, answer));
            return true;
		}


		/// <summary>
		/// �����û�����¼ʱ��
		/// </summary>
		/// <param name="uid">�û�id</param>
		public static void UpdateUserLastvisit(int uid, string ip)
		{
            DatabaseProvider.GetInstance().UpdateUserLastvisit(uid, ip);
        }

		/// <summary>
		/// �����û���ǰ������״̬
		/// </summary>
		/// <param name="uidlist">�û�uid�б�</param>
		/// <param name="state">��ǰ����״̬(0:����,1:����)</param>
		public static void UpdateUserOnlineState(string uidlist,int state, string activitytime)
		{
			if (!Utils.IsNumericArray(uidlist.Split(',')))
			{
				return;
			}

            switch (state)
			{
				case 0:		//�����˳�
                    DatabaseProvider.GetInstance().UpdateUserOnlineStateAndLastActivity(uidlist, 0, activitytime);
                    break;
				case 1:		//������¼
                    DatabaseProvider.GetInstance().UpdateUserOnlineStateAndLastVisit(uidlist, 1, activitytime);
                    break;
				case 2:		//��ʱ�˳�
                    DatabaseProvider.GetInstance().UpdateUserOnlineStateAndLastActivity(uidlist, 0, activitytime);
                    break;
				case 3:		//�����¼
                    DatabaseProvider.GetInstance().UpdateUserOnlineStateAndLastVisit(uidlist, 0, activitytime); 
                    break;
			}

		}

		/// <summary>
		/// �����û���ǰ������״̬
		/// </summary>
		/// <param name="uid">�û�uid�б�</param>
		/// <param name="state">��ǰ����״̬(0:����,1:����)</param>
		public static void UpdateUserOnlineState(int uid, int state, string activitytime)
		{
			if (uid<1)
			{
				return;
			}

			switch (state)
			{
				case 0:		//�����˳�
					DatabaseProvider.GetInstance().UpdateUserOnlineStateAndLastActivity(uid, 0, activitytime);
                    break;
				case 1:		//������¼
                    DatabaseProvider.GetInstance().UpdateUserOnlineStateAndLastVisit(uid, 1, activitytime);
                    break;
				case 2:		//��ʱ�˳�
                    DatabaseProvider.GetInstance().UpdateUserOnlineStateAndLastActivity(uid, 0, activitytime);
                    break;
				case 3:		//�����¼
                    DatabaseProvider.GetInstance().UpdateUserOnlineStateAndLastVisit(uid, 0, activitytime);
                    break;
			}

		}


		/// <summary>
		/// �����û���ǰ������ʱ������ʱ��
		/// </summary>
		/// <param name="uid">�û�uid</param>
		public static void UpdateUserOnlineTime(int uid, string activitytime)
		{
			if (uid<1)
			{
				return;
			}

            DatabaseProvider.GetInstance().UpdateUserLastActivity(uid, activitytime);	
        }

		/// <summary>
		/// �����û���Ϣ����δ������Ϣ������
		/// </summary>
		/// <param name="uid">�û�ID</param>
		/// <param name="pmnum">����Ϣ����</param>
		/// <returns>���¼�¼����</returns>
		public static int SetUserNewPMCount(int uid,int pmnum)
		{
            return DatabaseProvider.GetInstance().SetUserNewPMCount(uid, pmnum);
		}

		/// <summary>
		/// ����ָ���û���ѫ����Ϣ
		/// </summary>
		/// <param name="uid">�û�id</param>
		/// <param name="medals">ѫ����Ϣ</param>
		public static void UpdateMedals(int uid, string medals)
		{
            DatabaseProvider.GetInstance().UpdateMedals(uid, medals);
		}



		/// <summary>
		/// ���û���δ������Ϣ������Сһ��ָ����ֵ
		/// </summary>
		/// <param name="uid">�û�ID</param>
		/// <param name="subval">����Ϣ��Ҫ��С��ֵ,����Ϊ��</param>
		/// <returns>���¼�¼����</returns>
		public static int DecreaseNewPMCount(int uid,int subval)
		{
            return DatabaseProvider.GetInstance().DecreaseNewPMCount(uid, subval);
		}


		/// <summary>
		/// ���û���δ������Ϣ������һ
		/// </summary>
		/// <param name="uid">�û�ID</param>
		/// <returns>���¼�¼����</returns>
		public static int DecreaseNewPMCount(int uid)
		{
			return DecreaseNewPMCount(uid,1);
		}

		/// <summary>
		/// �õ��û��¶���Ϣ����
		/// </summary>
		/// <param name="uid">�û�id</param>
		/// <returns>�¶���Ϣ��</returns>
		public static int GetUserNewPMCount(int uid)
		{
            return DatabaseProvider.GetInstance().GetUserNewPMCount(uid);	
        }

		/// <summary>
		/// �����û�������
		/// </summary>
		/// <param name="useridlist">uid�б�</param>
		/// <returns></returns>
		public static int UpdateUserDigest(string useridlist)
		{
			if (!Utils.IsNumericArray(Utils.SplitString(useridlist,",")))
			{
				return 0;
			}
            return DatabaseProvider.GetInstance().UpdateUserDigest(useridlist);
		}

		/// <summary>
		/// �����û�SpaceID
		/// </summary>
		/// <param name="spaceid">Ҫ���µ�SpaceId</param>
		/// <param name="userid">Ҫ���µ�UserId</param>
		/// <returns>�Ƿ���³ɹ�</returns>
		public static bool UpdateUserSpaceId(int spaceid,int userid)
		{
			if(!Exists(userid))
			{
				return false;
			}

            DatabaseProvider.GetInstance().UpdateUserSpaceId(spaceid, userid);
			return true;
		}


        public static DataTable GetUserIdByAuthStr(string authstr)
        {
            return DatabaseProvider.GetInstance().GetUserIdByAuthStr(authstr);
        }

        /// <summary>
        /// �����û���
        /// </summary>
        /// <param name="uid">�û�ID</param>
        /// <param name="groupID">�û���ID</param>
        public static void UpdateUserGroup(int uid, int groupID)
        {
            DatabaseProvider.GetInstance().UpdateUserGroup(uid, groupID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Hashtable GetReportUsers()
        {
            DNTCache cache = DNTCache.GetCacheService();
            Hashtable ht = cache.RetrieveObject("/Forum/ReportUsers") as Hashtable;

            if (ht == null)
            {
                ht = new Hashtable();
                string groupidlist = GeneralConfigs.GetConfig().Reportusergroup;

                if (!Utils.IsNumericArray(groupidlist.Split(',')))
                    return ht;

                DataTable dt = DatabaseProvider.GetInstance().GetUsers(groupidlist);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ht[dt.Rows[i]["uid"]] = dt.Rows[i]["username"];
                }
            }

            return ht;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rewritename"></param>
        /// <returns></returns>
        public static int GetUserIdByRewriteName(string rewritename)
        {
            return DatabaseProvider.GetInstance().GetUserIdByRewriteName(rewritename);
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="user"></param>
        public static void UpdateUserPMSetting(UserInfo user)
        {
            DatabaseProvider.GetInstance().UpdateUserPMSetting(user);
        }
    }//class end
}
