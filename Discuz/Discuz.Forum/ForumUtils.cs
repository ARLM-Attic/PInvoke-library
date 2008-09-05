using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Drawing.Drawing2D;

using Discuz.Config;
using Discuz.Entity;
using Discuz.Common;
using Discuz.Data;
using Discuz.Plugin.Preview;

namespace Discuz.Forum
{
	/// <summary>
	/// ������̳�Ķ���
	/// </summary>
	public struct UserAction
	{
      
		#region
		/// <summary>
        /// ���ݶ���ID����ָ���Ķ�������
		/// </summary>
		/// <param name="actionid">����ID</param>
		/// <returns></returns>
		public static string GetActionDescriptionByID(int actionid)
		{
			if (actionid == IndexShow.ActionID)
			{
				return IndexShow.ActionDescription;
			}
				
			if (actionid == ShowForum.ActionID)
			{
				return ShowForum.ActionDescription;
			}
				
			if (actionid == ShowTopic.ActionID)
			{
				return ShowTopic.ActionDescription;
			}
			
			if (actionid == Login.ActionID)
			{
				return Login.ActionDescription;
			}
				
			if (actionid == PostTopic.ActionID)
			{
				return PostTopic.ActionDescription;
			}
				
			if (actionid == PostReply.ActionID)
			{
				return PostReply.ActionDescription;
			}
				
			if (actionid == ActivationUser.ActionID)
			{
				return ActivationUser.ActionDescription;
			}
				
			if (actionid == Register.ActionID)
			{
				return Register.ActionDescription;
			}
			
			return "";
		}

		/// <summary>
		/// ͨ������ID�õ���������
		/// </summary>
        /// <param name="actionid">����ID</param>
		/// <returns></returns>
		private static string GetActionName(int actionid)
		{
			//�������ļ��ж�ȡ
			return "";
		}

        /// <summary>
        /// ͨ������ID�õ���������
        /// </summary>
        /// <param name="actionid">��������</param>
        /// <returns></returns>
		private static string GetActionDescription(int actionid)
		{
			//�������ļ��ж�ȡ
			return "";
		}
	
        /// <summary>
        /// ��ҳ
        /// </summary>
		public struct IndexShow
		{
		
            /// <summary>
            /// ����ID
            /// </summary>
			public static int ActionID
			{
				get 
				{
					return 1;
				}
			}

            /// <summary>
            /// ��������
            /// </summary>
			public static string ActionName
			{
				get 
				{
					string m_actionname = GetActionName(ActionID);
					return m_actionname != "" ? m_actionname : "IndexShow";
				}
			}

            /// <summary>
            /// ��������
            /// </summary>
			public static string ActionDescription
			{
				get 
				{
					string m_actiondescription = GetActionDescription(ActionID);
					return m_actiondescription != "" ? m_actiondescription : "�����ҳ";
				}
			}
		}
			
        /// <summary>
        /// ��ʾ���
        /// </summary>
		public struct ShowForum
		{
            /// <summary>
            /// ����ID
            /// </summary>
			public static int ActionID
			{
				get 
				{
					return 2;
				}
			}

            /// <summary>
            /// ��������
            /// </summary>
			public static string ActionName
			{
				get 
				{
					string m_actionname = GetActionName(ShowTopic.ActionID);
					return m_actionname != "" ? m_actionname : "ShowForum";
				}
			}

            /// <summary>
            /// ��������
            /// </summary>
			public static string ActionDescription
			{
				get 
				{
					string m_actiondescription=GetActionDescription(ShowTopic.ActionID);
					return m_actiondescription!=""?m_actiondescription:"�����̳���";
				}
			}
		}
		    
		/// <summary>
		/// ������ʾ
		/// </summary>
		public struct ShowTopic
		{
            /// <summary>
            /// ����ID
            /// </summary>
			public static int ActionID
			{
				get 
				{
					return 3;
				}
			}

            /// <summary>
            /// ��������
            /// </summary>
			public static string ActionName
			{
				get 
				{
					string m_actionname=GetActionName(ActionID);
					return m_actionname!=""?m_actionname:"ShowTopic";
				}
			}

            /// <summary>
            /// ��������
            /// </summary>
			public static string ActionDescription
			{
				get 
				{
					string m_actiondescription=GetActionDescription(ActionID);
					return m_actiondescription!=""?m_actiondescription:"�������";
				}
			}
		}

		/// <summary>
		/// �û���½
		/// </summary>
		public struct Login
		{
            /// <summary>
            /// ����ID
            /// </summary>
			public static int ActionID
			{
				get 
				{
					return 4;
				}
			}

            /// <summary>
            /// ��������
            /// </summary>
			public static string ActionName
			{
				get 
				{
					string m_actionname=GetActionName(ActionID);
					return m_actionname!=""?m_actionname:"Login";
				}
			}

            /// <summary>
            /// ��������
            /// </summary>
			public static string ActionDescription
			{
				get 
				{
					string m_actiondescription=GetActionDescription(ActionID);
					return m_actiondescription!=""?m_actiondescription:"��̳��½";
				}
			}
		}

		/// <summary>
		/// ��������
		/// </summary>
		public struct PostTopic
		{
            /// <summary>
            /// ����ID
            /// </summary>
			public static int ActionID
			{
				get 
				{
					return 5;
				}
			}

            /// <summary>
            /// ��������
            /// </summary>
			public static string ActionName
			{
				get 
				{
					string m_actionname=GetActionName(ActionID);
					return m_actionname!=""?m_actionname:"PostTopic";
				}
			}

            /// <summary>
            /// ��������
            /// </summary>
			public static string ActionDescription
			{
				get 
				{
					string m_actiondescription=GetActionDescription(ActionID);
					return m_actiondescription!=""?m_actiondescription:"��������";
				}
			}
		}

		/// <summary>
		/// ����ظ�
		/// </summary>
		public struct PostReply
		{
            /// <summary>
            /// ����ID
            /// </summary>
			public static int ActionID
			{
				get 
				{
					return 6;
				}
			}

            /// <summary>
            /// ��������
            /// </summary>
			public static string ActionName
			{
				get 
				{
					string m_actionname=GetActionName(ActionID);
					return m_actionname!=""?m_actionname:"PostReply";
				}
			}

            /// <summary>
            /// ��������
            /// </summary>
			public static string ActionDescription
			{
				get 
				{
					string m_actiondescription=GetActionDescription(ActionID);
					return m_actiondescription!=""?m_actiondescription:"����ظ�";
				}
			}
		}

        /// <summary>
        /// �����û�
        /// </summary>
		public struct ActivationUser
		{
            /// <summary>
            /// ����ID
            /// </summary>
			public static int ActionID
			{
				get 
				{
					return 7;
				}
			}

            /// <summary>
            /// ��������
            /// </summary>
			public static string ActionName
			{
				get 
				{
					string m_actionname=GetActionName(ActionID);
					return m_actionname!=""?m_actionname:"ActivationUser";
				}
			}

            /// <summary>
            /// ��������
            /// </summary>
			public static string ActionDescription
			{
				get 
				{
					string m_actiondescription=GetActionDescription(ActionID);
					return m_actiondescription!=""?m_actiondescription:"�����û��ʻ�";
				}
			}
		}

        /// <summary>
        /// ע��
        /// </summary>
		public struct Register
		{
            /// <summary>
            /// ����ID
            /// </summary>
			public static int ActionID
			{
				get 
				{
					return 8;
				}
			}

            /// <summary>
            /// ��������
            /// </summary>
			public static string ActionName
			{
				get 
				{
					string m_actionname=GetActionName(ActionID);
					return m_actionname!=""?m_actionname:"Register";
				}
			}

            /// <summary>
            /// ��������
            /// </summary>
			public static string ActionDescription
			{
				get 
				{
					string m_actiondescription=GetActionDescription(ActionID);
					return m_actiondescription!=""?m_actiondescription:"ע�����û�";
				}
			}
		}
		#endregion	

	}

	
	
	/// <summary>
	/// ��̳������
	/// </summary>
	public class ForumUtils
	{
        /// <summary>
        /// ��֤�����ɵ�ȡֵ��Χ
        /// </summary>
        private static string[] verifycodeRange = { "0","1","2","3","4","5","6","7","8","9",
                                                    "a","b","c","d","e","f","g",
                                                    "h",    "j","k",    "m","n",
                                                        "p","q",    "r","s","t",
                                                    "u","v","w",    "x","y"
        
                                                  };
        /// <summary>
        /// ������֤����ʹ�õ������������
        /// </summary>
        private static Random verifycodeRandom = new Random();

        /// <summary>
        /// ������������ʽ����
        /// </summary>
        private static Regex r_word;

		/// <summary>
		/// ������̳�û�����cookie����
		/// </summary>
		/// <param name="key">��Կ</param>
		/// <returns></returns>
		public static string GetCookiePassword(string key)
		{
			return DES.Decode(GetCookie("password"), key).Trim();

		}

		/// <summary>
		/// ������̳�û�����cookie����
		/// </summary>
		/// <param name="password">��������</param>
		/// <param name="key">��Կ</param>
		/// <returns></returns>
		public static string GetCookiePassword(string password, string key)
		{
			return DES.Decode(password, key);

		}


		/// <summary>
		/// ������������
		/// </summary>
		/// <param name="password">��������</param>
		/// <param name="key">��Կ</param>
		/// <returns></returns>
		public static string SetCookiePassword(string password, string key)
		{
			//			if (password.Length < 32)
			//			{
			//				password = password.PadRight(32);
			//			}
			return DES.Encode(password, key);
		}


		/// <summary>
		/// �����û���ȫ����𰸵Ĵ洢����
		/// </summary>
		/// <param name="questionid">����id</param>
		/// <param name="answer">��</param>
		/// <returns></returns>
		public static string GetUserSecques(int questionid, string answer)
		{
			if (questionid > 0)
			{
				return Utils.MD5(answer + Utils.MD5(questionid.ToString())).Substring(15, 8);
			}
			return "";
		}
		

        #region Cookies

        /// <summary>
        /// д��̳cookieֵ
        /// </summary>
        /// <param name="strName">��</param>
        /// <param name="strValue">ֵ</param>
        public static void WriteCookie(string strName, string strValue)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["dnt"];
            if (cookie == null)
            {
                cookie = new HttpCookie("dnt");
                cookie.Values[strName] = Utils.UrlEncode(strValue);
            }
            else
            {

                cookie.Values[strName] = Utils.UrlEncode(strValue);
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
            if (cookieDomain != string.Empty && HttpContext.Current.Request.Url.Host.IndexOf(cookieDomain) > -1 && IsValidDomain(HttpContext.Current.Request.Url.Host))
                cookie.Domain = cookieDomain;

            HttpContext.Current.Response.AppendCookie(cookie);

        }


        /// <summary>
        /// дcookieֵ
        /// </summary>
        /// <param name="strName">����</param>
        /// <param name="intValue">ֵ</param>
        public static void WriteCookie(string strName, int intValue)
        {
            WriteCookie(strName, intValue.ToString());
        }



        /// <summary>
        /// д��̳��¼�û���cookie
        /// </summary>
        /// <param name="uid">�û�Id</param>
        /// <param name="expires">cookie��Ч��</param>
        /// <param name="passwordkey">�û�����Key</param>
        /// <param name="templateid">�û���ǰҪʹ�õĽ�����</param>
        /// <param name="invisible">�û���ǰ�ĵ�¼ģʽ(����������)</param>
        public static void WriteUserCookie(int uid, int expires, string passwordkey, int templateid, int invisible)
        {
            UserInfo userinfo = Users.GetUserInfo(uid);
            WriteUserCookie(userinfo, expires, passwordkey, templateid, invisible);
        }

        /// <summary>
        /// д��̳��¼�û���cookie
        /// </summary>
        /// <param name="userinfo">�û���Ϣ</param>
        /// <param name="expires">cookie��Ч��</param>
        /// <param name="passwordkey">�û�����Key</param>
        /// <param name="templateid">�û���ǰҪʹ�õĽ�����</param>
        /// <param name="invisible">�û���ǰ�ĵ�¼ģʽ(����������)</param>
        public static void WriteUserCookie(UserInfo userinfo, int expires, string passwordkey, int templateid, int invisible)
        {
            if (userinfo == null)
            {
                return;
            }
            HttpCookie cookie = new HttpCookie("dnt");
            cookie.Values["userid"] = userinfo.Uid.ToString();
            cookie.Values["password"] = Utils.UrlEncode(SetCookiePassword(userinfo.Password, passwordkey));
            if (Templates.GetTemplateItem(templateid) == null)
            {
                templateid = 0;

                foreach (string strTemplateid in Utils.SplitString(Templates.GetValidTemplateIDList(), ","))
                {

                    if (strTemplateid.Equals(userinfo.Templateid.ToString()))
                    {
                        templateid = userinfo.Templateid;
                        break;
                    }
                }
            }

            cookie.Values["avatar"] = Utils.UrlEncode(userinfo.Avatar.ToString());
            cookie.Values["tpp"] = userinfo.Tpp.ToString();
            cookie.Values["ppp"] = userinfo.Ppp.ToString();
            cookie.Values["pmsound"] = userinfo.Pmsound.ToString();
            if (invisible != 0 || invisible != 1)
            {
                invisible = userinfo.Invisible;
            }
            cookie.Values["invisible"] = invisible.ToString();

            cookie.Values["referer"] = "index.aspx";
            cookie.Values["sigstatus"] = userinfo.Sigstatus.ToString();
            cookie.Values["expires"] = expires.ToString();
            if (expires > 0)
            {
                cookie.Expires = DateTime.Now.AddMinutes(expires);
            }
            string cookieDomain = GeneralConfigs.GetConfig().CookieDomain.Trim();
            if (cookieDomain != string.Empty && HttpContext.Current.Request.Url.Host.IndexOf(cookieDomain) > -1 && IsValidDomain(HttpContext.Current.Request.Url.Host))
            {
                cookie.Domain = cookieDomain;
            }

            HttpContext.Current.Response.AppendCookie(cookie);
            if (templateid > 0)
            {
                Utils.WriteCookie(Utils.GetTemplateCookieName(), templateid.ToString(), 999999);
            }
            //SetCookieExpires(expires);
        }

        /// <summary>
        /// д��̳��¼�û���cookie
        /// </summary>
        /// <param name="uid">�û�Id</param>
        /// <param name="expires">cookie��Ч��</param>
        /// <param name="passwordkey">�û�����Key</param>
        public static void WriteUserCookie(int uid, int expires, string passwordkey)
        {
            WriteUserCookie(uid, expires, passwordkey, 0, -1);
        }

        /// <summary>
        /// д��̳��¼�û���cookie
        /// </summary>
        /// <param name="userinfo">�û���Ϣ</param>
        /// <param name="expires">cookie��Ч��</param>
        /// <param name="passwordkey">�û�����Key</param>
        public static void WriteUserCookie(UserInfo userinfo, int expires, string passwordkey)
        {
            WriteUserCookie(userinfo, expires, passwordkey, 0, -1);
        }

        /// <summary>
        /// �����̳cookieֵ
        /// </summary>
        /// <param name="strName">��</param>
        /// <returns>ֵ</returns>
        public static string GetCookie(string strName)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies["dnt"] != null && HttpContext.Current.Request.Cookies["dnt"][strName] != null)
            {
                return Utils.UrlDecode(HttpContext.Current.Request.Cookies["dnt"][strName].ToString());
            }

            return "";
        }


        /// <summary>
        /// �����̳��¼�û���cookie
        /// </summary>
        public static void ClearUserCookie()
        {
            ClearUserCookie("dnt");
        }

        public static void ClearUserCookie(string cookieName)
        {
            HttpCookie cookie = new HttpCookie(cookieName);
            cookie.Values.Clear();
            cookie.Expires = DateTime.Now.AddYears(-1);
            string cookieDomain = GeneralConfigs.GetConfig().CookieDomain.Trim();
            if (cookieDomain != string.Empty && HttpContext.Current.Request.Url.Host.IndexOf(cookieDomain) > -1 && IsValidDomain(HttpContext.Current.Request.Url.Host))
                cookie.Domain = cookieDomain;
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        #endregion

		/// <summary>
		/// ������֤��
		/// </summary>
		/// <param name="len">����</param>
		/// <returns>��֤��</returns>
		public static string CreateAuthStr(int len)
		{
            int number;
            StringBuilder checkCode = new StringBuilder();

            Random random = new Random();

            for (int i = 0; i < len; i++)
            {
                number = random.Next();

                if (number % 2 == 0)
                {
                    checkCode.Append((char)('0' + (char)(number % 10)));
                }
                else
                {
                    checkCode.Append((char)('A' + (char)(number % 26)));
                }

            }

            return checkCode.ToString();
		}

		/// <summary>
		/// ������֤��
		/// </summary>
		/// <param name="len">����</param>
		/// <param name="OnlyNum">�Ƿ��Ϊ����</param>
        /// <returns>string</returns>
		public static string CreateAuthStr(int len, bool OnlyNum)
		{
			int number;
			StringBuilder checkCode = new StringBuilder();	
		
			for (int i = 0; i < len; i++)
			{
                if (!OnlyNum)
                {
                    number = verifycodeRandom.Next(0, verifycodeRange.Length);
                }
                else
                {
                    number = verifycodeRandom.Next(0, 10);
                }
				checkCode.Append(verifycodeRange[number]);
			}
		
			return checkCode.ToString();
		}

		/// <summary>
		/// �������⻺���־�ļ�
		/// </summary>
        /// <returns>bool</returns>
		public static bool CreateTopicCacheInfoFile()
		{
			string infofilepath = Utils.GetMapPath(GetShowTopicCacheDir() + "/cacheinfo.config");
			try
			{
				using (FileStream fs = new FileStream(infofilepath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
				{
					Byte[] info = System.Text.Encoding.UTF8.GetBytes("<?xml version=\"1.0\"?>");
					fs.Write(info, 0, info.Length);
					fs.Close();
				}
				return true;
			}
			catch
			{
				return false;
			}
		}
		
		/// <summary>
		/// ��HTTP���ָ������id�����⻺���ļ�, ����ĳЩʱ�λ�ִ��ɾ���������⻺���ļ�
		/// </summary>
		/// <param name="tid">����id</param>
		/// <param name="timeout">�����ļ�����Чʱ��</param>
        /// <returns>bool</returns>
		public static bool ResponseTopicCacheFile(int tid, int timeout)
		{
			TimeSpan s;
			if ((System.DateTime.Now.Minute >= System.DateTime.Now.Day) && (System.DateTime.Now.Minute <= System.DateTime.Now.Day + 10))
			{
				string infofilepath = Utils.GetMapPath(GetShowTopicCacheDir() + "/cacheinfo.config");
				if (System.IO.File.Exists(infofilepath))
				{
					s = (System.IO.File.GetCreationTime(infofilepath) - System.DateTime.Now);
				
					if (Math.Abs(s.TotalHours) > 1)
					{
						CreateTopicCacheInfoFile();
						DeleteTimeoutTopicCacheFiles(timeout);
					}
				}
				else
				{
					CreateTopicCacheInfoFile();
				}
			}

			
			string filepath = GetTopicCacheFilename(tid);
			if (System.IO.File.Exists(filepath))
			{
				s = (System.IO.File.GetCreationTime(filepath) - System.DateTime.Now);
				if (timeout > 0 && Math.Abs(s.TotalMinutes) > timeout)
				{
					DeleteTopicCacheFile(tid);
					return false;
				}
				System.Web.HttpContext.Current.Response.WriteFile(filepath);
				System.Web.HttpContext.Current.Response.End();
				return true;
			}
			return false;
		}

		/// <summary>
		/// �������⻺���ļ���
		/// </summary>
		/// <param name="tid">����id</param>
        /// <returns>string</returns>
		public static string GetTopicCacheFilename(int tid)
		{
			return Utils.GetMapPath(GetShowTopicCacheDir() + "/" + tid.ToString() + ".dntcache");
		}

		/// <summary>
		/// ɾ�����й��ڵ����⻺���ļ�
		/// </summary>
		/// <param name="timeout">��ʱʱ��</param>
        /// <returns>bool</returns>
		public static bool DeleteTimeoutTopicCacheFiles(int timeout)
		{
			try
			{
				DirectoryInfo dirinfo = new DirectoryInfo(Utils.GetMapPath(GetShowTopicCacheDir()));
				TimeSpan s;
				foreach (FileSystemInfo file in dirinfo.GetFiles())
				{
					if (file != null && file.Extension.ToLower().Equals(".dntcache"))
					{
						s = (System.IO.File.GetCreationTime(file.FullName) - System.DateTime.Now);
						if (timeout > 0 && Math.Abs(s.TotalMinutes) > (timeout + 1))
						{
							try
							{
								file.Delete();
							}
							catch
							{
								//
							}
						}
					}
				}
			}
			catch
			{
				return false;
			}
			return true;

		}


		/// <summary>
		/// �������⻺���ļ�
		/// </summary>
		/// <param name="tid">����id</param>
		/// <param name="pagetext">������ַ�������</param>
        /// <returns>bool</returns>
		public static bool CreateTopicCacheFile(int tid, string pagetext)
		{
			string filepath = GetTopicCacheFilename(tid);
			try
			{
				using (FileStream fs = new FileStream(filepath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
				{
					Byte[] info = System.Text.Encoding.UTF8.GetBytes(pagetext);
					fs.Write(info, 0, info.Length);
					fs.Close();
					return true;
				}
				
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// ɾ��ָ��id�������οͻ���
		/// </summary>
		/// <param name="tid">����id</param>
        /// <returns>bool</returns>
		public static bool DeleteTopicCacheFile(int tid)
		{
			string filepath = GetTopicCacheFilename(tid);
			try
			{
				System.IO.File.Delete(filepath);
				return true;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// ɾ��ָ��id�б�������οͻ���
		/// </summary>
		/// <param name="tidlist">����id�б�</param>
        /// <returns>int</returns>
		public static int DeleteTopicCacheFile(string tidlist)
		{
			int count = 0;

			string[] strNumber = Utils.SplitString(tidlist, ",");
			foreach (string tid in strNumber)
			{
				if (Utils.IsNumeric(tid) && DeleteTopicCacheFile(Int32.Parse(tid)))
				{
					count++;
				}
			}
			return count;
		}


		/// <summary>
		/// ����"�鿴����"��ҳ�滺��Ŀ¼
		/// </summary>
		/// <returns>����Ŀ¼</returns>
		public static string GetShowTopicCacheDir()
		{
			return GetCacheDir("showtopic");
		}


		/// <summary>
		/// ����ָ��Ŀ¼��ҳ�滺��Ŀ¼
		/// </summary>
		/// <param name="path">Ŀ¼</param>
		/// <returns>����Ŀ¼</returns>
		public static string GetCacheDir(string path)
		{
			path = path.Trim();
			StringBuilder dir = new StringBuilder();
			dir.Append(BaseConfigs.GetForumPath);
			dir.Append("cache/");
			dir.Append(path);
			string cachedir = dir.ToString();
			if (!Directory.Exists(Utils.GetMapPath(cachedir)))
			{
				Utils.CreateDir(Utils.GetMapPath(cachedir));
			}
			return cachedir;
		}

		/// <summary>
		/// �����ϴ�ͷ��
		/// </summary>
		/// <param name="MaxAllowFileSize">��������ͷ���ļ��ߴ�(��λ:KB)</param>
		/// <returns>�����ļ������·��</returns>
		public static string SaveRequestAvatarFile(int userid, int MaxAllowFileSize)
		{

			string filename = Path.GetFileName(HttpContext.Current.Request.Files[0].FileName);
			string fileextname = Path.GetExtension(filename).ToLower();
			string filetype = HttpContext.Current.Request.Files[0].ContentType.ToLower();
			//int filesize = HttpContext.Current.Request.Files[0].ContentLength;
			// �ж� �ļ���չ��/�ļ���С/�ļ����� �Ƿ����Ҫ��
			if ((fileextname.Equals(".jpg") || fileextname.Equals(".gif") || fileextname.Equals(".png")) && (filetype.StartsWith("image")))
			{
				StringBuilder savedir = new StringBuilder();
				savedir.Append(BaseConfigs.GetForumPath);
				savedir.Append("avatars/upload/");
				int t1 = (int)((double)userid / (double)10000);
				savedir.Append(t1);
				savedir.Append("/");
				int t2 = (int)((double)userid / (double)200);
				savedir.Append(t2);
				savedir.Append("/");
				if (!Directory.Exists(Utils.GetMapPath(savedir.ToString())))
				{
					Utils.CreateDir(Utils.GetMapPath(savedir.ToString()));
					//System.IO.Directory.CreateDirectory(Utils.GetMapPath(savedir.ToString()));
				}
				string newfilename = savedir.ToString() + userid.ToString() + fileextname;
				//if (System.IO.File.Exists(Utils.GetMapPath(savedir.ToString() + userid.ToString() + ".jpg")))
				
				if (HttpContext.Current.Request.Files[0].ContentLength <= MaxAllowFileSize)
				{
                    File.Delete(Utils.GetMapPath(savedir.ToString()) + userid.ToString() + ".jpg");
				    File.Delete(Utils.GetMapPath(savedir.ToString()) + userid.ToString() + ".gif");
				    File.Delete(Utils.GetMapPath(savedir.ToString()) + userid.ToString() + ".png");

					HttpContext.Current.Request.Files[0].SaveAs(Utils.GetMapPath(newfilename));
					return newfilename;
				}
			}
			return "";
		}

		/// <summary>
		/// ��ͼƬˮӡ
		/// </summary>
		/// <param name="filename">�ļ���</param>
		/// <param name="watermarkFilename">ˮӡ�ļ���</param>
		/// <param name="watermarkStatus">ͼƬˮӡλ��</param>
		public static void AddImageSignPic(Image img, string filename, string watermarkFilename, int watermarkStatus, int quality, int watermarkTransparency)
		{
			Graphics g = Graphics.FromImage(img);
			//���ø�������ֵ��
			//g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
			//���ø�����,���ٶȳ���ƽ���̶�
			//g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
			Image watermark = new Bitmap(watermarkFilename);

            if (watermark.Height >= img.Height || watermark.Width >= img.Width)
            {
                return;
            }

			ImageAttributes imageAttributes = new ImageAttributes();
			ColorMap colorMap = new ColorMap();

			colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
			colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
			ColorMap[] remapTable = {colorMap};

			imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

			float transparency = 0.5F;
			if (watermarkTransparency >=1 && watermarkTransparency <=10)
			{
				transparency = (watermarkTransparency / 10.0F);
			}

			float[][] colorMatrixElements = {
												new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f},
												new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f},
												new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f},
												new float[] {0.0f,  0.0f,  0.0f,  transparency, 0.0f},
												new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}
											};

			ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);

			imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

			int xpos = 0;
			int ypos = 0;

			switch(watermarkStatus)
			{
				case 1:
					xpos = (int)(img.Width * (float).01);
					ypos = (int)(img.Height * (float).01);
					break;
				case 2:
					xpos = (int)((img.Width * (float).50) - (watermark.Width / 2));
					ypos = (int)(img.Height * (float).01);
					break;
				case 3:
					xpos = (int)((img.Width * (float).99) - (watermark.Width));
					ypos = (int)(img.Height * (float).01);
					break;
				case 4:
					xpos = (int)(img.Width * (float).01);
					ypos = (int)((img.Height * (float).50) - (watermark.Height / 2));
					break;
				case 5:
					xpos = (int)((img.Width * (float).50) - (watermark.Width / 2));
					ypos = (int)((img.Height * (float).50) - (watermark.Height / 2));
					break;
				case 6:
					xpos = (int)((img.Width * (float).99) - (watermark.Width));
					ypos = (int)((img.Height * (float).50) - (watermark.Height / 2));
					break;
				case 7:
					xpos = (int)(img.Width * (float).01);
					ypos = (int)((img.Height * (float).99) - watermark.Height);
					break;
				case 8:
					xpos = (int)((img.Width * (float).50) - (watermark.Width / 2));
					ypos = (int)((img.Height * (float).99) - watermark.Height);
					break;
				case 9:
					xpos = (int)((img.Width * (float).99) - (watermark.Width));
					ypos = (int)((img.Height * (float).99) - watermark.Height);
					break;
			}

			g.DrawImage(watermark, new Rectangle(xpos, ypos, watermark.Width, watermark.Height), 0, 0, watermark.Width, watermark.Height, GraphicsUnit.Pixel, imageAttributes);
			//g.DrawImage(watermark, new System.Drawing.Rectangle(xpos, ypos, watermark.Width, watermark.Height), 0, 0, watermark.Width, watermark.Height, System.Drawing.GraphicsUnit.Pixel);
			
			ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders(); 
			ImageCodecInfo ici = null;
			foreach(ImageCodecInfo codec in codecs)
			{
				if (codec.MimeType.IndexOf("jpeg") > -1)
				{
					ici = codec;
				}
			}
			EncoderParameters encoderParams = new EncoderParameters();
			long[] qualityParam = new long[1];
			if (quality < 0 || quality > 100)
			{
				quality = 80;
			}
			qualityParam[0] = quality;

            EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qualityParam);
			encoderParams.Param[0] = encoderParam;

			if (ici != null)
			{
				img.Save(filename, ici, encoderParams);
			}
			else
			{
				img.Save(filename);
			}
			
			g.Dispose();
			img.Dispose();
			watermark.Dispose();
			imageAttributes.Dispose();
		}


		/// <summary>
		/// ����ͼƬ����ˮӡ
		/// </summary>
		/// <param name="filename">�ļ���</param>
		/// <param name="watermarkText">ˮӡ����</param>
		/// <param name="watermarkStatus">ͼƬˮӡλ��</param>
		public static void AddImageSignText(Image img, string filename, string watermarkText, int watermarkStatus, int quality, string fontname, int fontsize)
		{
			//System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(img);
			//	.FromFile(filename);
			Graphics g = Graphics.FromImage(img);
			Font drawFont = new Font(fontname, fontsize, FontStyle.Regular, GraphicsUnit.Pixel);
			SizeF crSize;
			crSize = g.MeasureString(watermarkText, drawFont);

			float xpos = 0;
			float ypos = 0;

			switch(watermarkStatus)
			{
				case 1:
					xpos = (float)img.Width * (float).01;
					ypos = (float)img.Height * (float).01;
					break;
				case 2:
					xpos = ((float)img.Width * (float).50) - (crSize.Width / 2);
					ypos = (float)img.Height * (float).01;
					break;
				case 3:
					xpos = ((float)img.Width * (float).99) - crSize.Width;
					ypos = (float)img.Height * (float).01;
					break;
				case 4:
					xpos = (float)img.Width * (float).01;
					ypos = ((float)img.Height * (float).50) - (crSize.Height / 2);
					break;
				case 5:
					xpos = ((float)img.Width * (float).50) - (crSize.Width / 2);
					ypos = ((float)img.Height * (float).50) - (crSize.Height / 2);
					break;
				case 6:
					xpos = ((float)img.Width * (float).99) - crSize.Width;
					ypos = ((float)img.Height * (float).50) - (crSize.Height / 2);
					break;
				case 7:
					xpos = (float)img.Width * (float).01;
					ypos = ((float)img.Height * (float).99) - crSize.Height;
					break;
				case 8:
					xpos = ((float)img.Width * (float).50) - (crSize.Width / 2);
					ypos = ((float)img.Height * (float).99) - crSize.Height;
					break;
				case 9:
					xpos = ((float)img.Width * (float).99) - crSize.Width;
					ypos = ((float)img.Height * (float).99) - crSize.Height;
					break;
			}

//			System.Drawing.StringFormat StrFormat = new System.Drawing.StringFormat();
//			StrFormat.Alignment = System.Drawing.StringAlignment.Center;
//
//			g.DrawString(watermarkText, drawFont, new System.Drawing.SolidBrush(System.Drawing.Color.White), xpos + 1, ypos + 1, StrFormat);
//			g.DrawString(watermarkText, drawFont, new System.Drawing.SolidBrush(System.Drawing.Color.Black), xpos, ypos, StrFormat);
			g.DrawString(watermarkText, drawFont, new SolidBrush(Color.White), xpos + 1, ypos + 1);
			g.DrawString(watermarkText, drawFont, new SolidBrush(Color.Black), xpos, ypos);

			ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders(); 
			ImageCodecInfo ici = null;
			foreach(ImageCodecInfo codec in codecs)
			{
				if (codec.MimeType.IndexOf("jpeg") > -1)
				{
					ici = codec;
				}
			}
			EncoderParameters encoderParams = new EncoderParameters();
			long[] qualityParam = new long[1];
			if (quality < 0 || quality > 100)
			{
				quality = 80;
			}
			qualityParam[0] = quality;

            EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qualityParam);
			encoderParams.Param[0] = encoderParam;

			if (ici != null)
			{
				img.Save(filename, ici, encoderParams);
			}
			else
			{
				img.Save(filename);
			}
			g.Dispose();
			//bmp.Dispose();
			img.Dispose();
		}

		/// <summary>
		/// �ж��Ƿ����ϴ����ļ�
		/// </summary>
		/// <returns>�Ƿ����ϴ����ļ�</returns>
		public static bool IsPostFile()
		{
			for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
			{
				if (HttpContext.Current.Request.Files[i].FileName != "")
				{
					return true;
				}
			}
			return false;
		}
		
		
		/// <summary>
		/// �����ϴ����ļ�
		/// </summary>
		/// <param name="forumid">���id</param>
		/// <param name="MaxAllowFileCount">���������ϴ��ļ�����</param>
		/// <param name="MaxSizePerDay">ÿ������ĸ�����С����</param>
		/// <param name="MaxFileSize">�������������ļ��ֽ���</param>/// 
		/// <param name="TodayUploadedSize">�����Ѿ��ϴ��ĸ����ֽ�����</param>
		/// <param name="AllowFileType">������ļ�����, ��string[]��ʽ�ṩ</param>
		/// <param name="config">�������淽ʽ 0=����/��/�մ��벻ͬĿ¼ 1=����/��/��/��̳���벻ͬĿ¼ 2=����̳���벻ͬĿ¼ 3=���ļ����ʹ��벻ͬĿ¼</param>
		/// <param name="watermarkstatus">ͼƬˮӡλ��</param>
		/// <param name="filekey">File�ؼ���Key(��Name����)</param>
		/// <returns>�ļ���Ϣ�ṹ</returns>
        public static AttachmentInfo[] SaveRequestFiles(int forumid, int MaxAllowFileCount, int MaxSizePerDay, int MaxFileSize, int TodayUploadedSize, string AllowFileType, int watermarkstatus, GeneralConfigInfo config, string filekey)
		{
			string[] tmp = Utils.SplitString(AllowFileType, "\r\n");
			string[] AllowFileExtName = new string[tmp.Length];
			int[] MaxSize = new int[tmp.Length];
			

			for (int i = 0; i < tmp.Length; i++)
			{
				AllowFileExtName[i] = Utils.CutString(tmp[i],0, tmp[i].LastIndexOf(","));
				MaxSize[i] = Utils.StrToInt(Utils.CutString(tmp[i], tmp[i].LastIndexOf(",") + 1), 0);
			}
				
			int saveFileCount = 0;
			
			int fCount = HttpContext.Current.Request.Files.Count;

			for (int i = 0; i < fCount; i++)
			{
				if (!HttpContext.Current.Request.Files[i].FileName.Equals("") && HttpContext.Current.Request.Files.AllKeys[i].Equals(filekey))
				{
					saveFileCount++;
				}
			}

			AttachmentInfo[] attachmentinfo = new AttachmentInfo[saveFileCount];
			if (saveFileCount > MaxAllowFileCount)
				return attachmentinfo;

			saveFileCount = 0;

			Random random = new Random(unchecked((int)DateTime.Now.Ticks)); 


			for (int i = 0; i < fCount; i++)
			{
				if (!HttpContext.Current.Request.Files[i].FileName.Equals("") && HttpContext.Current.Request.Files.AllKeys[i].Equals(filekey))
				{
					string filename = Path.GetFileName(HttpContext.Current.Request.Files[i].FileName);
					string fileextname = Utils.CutString(filename, filename.LastIndexOf(".") + 1).ToLower();
					string filetype = HttpContext.Current.Request.Files[i].ContentType.ToLower();
					int filesize = HttpContext.Current.Request.Files[i].ContentLength;
					string newfilename = "";
						
					attachmentinfo[saveFileCount] = new AttachmentInfo();

					attachmentinfo[saveFileCount].Sys_noupload = "";

					// �ж� �ļ���չ��/�ļ���С/�ļ����� �Ƿ����Ҫ��
					if (!(Utils.IsImgFilename(filename) && !filetype.StartsWith("image")))
					{
						int extnameid = Utils.GetInArrayID(fileextname, AllowFileExtName);

						if (extnameid >= 0 && (filesize <= MaxSize[extnameid]) && (MaxFileSize >= filesize /*|| MaxAllSize == 0*/) && (MaxSizePerDay - TodayUploadedSize >= filesize))
						{
							TodayUploadedSize = TodayUploadedSize + filesize;
							string UploadDir = Utils.GetMapPath(BaseConfigs.GetForumPath + "upload/");
							StringBuilder savedir = new StringBuilder("");
							//�������淽ʽ 0=����/��/�մ��벻ͬĿ¼ 1=����/��/��/��̳���벻ͬĿ¼ 2=����̳���벻ͬĿ¼ 3=���ļ����ʹ��벻ͬĿ¼
							if (config.Attachsave == 1)
							{
								savedir.Append(DateTime.Now.ToString("yyyy"));
								savedir.Append(Path.DirectorySeparatorChar);
								savedir.Append(DateTime.Now.ToString("MM"));
								savedir.Append(Path.DirectorySeparatorChar);
								savedir.Append(DateTime.Now.ToString("dd"));
								savedir.Append(Path.DirectorySeparatorChar);
								savedir.Append(forumid.ToString());
								savedir.Append(Path.DirectorySeparatorChar);
							}	
							else if (config.Attachsave == 2)
							{
								savedir.Append(forumid);
								savedir.Append(Path.DirectorySeparatorChar);
							}
							else if (config.Attachsave == 3)
							{
								savedir.Append(fileextname);
								savedir.Append(Path.DirectorySeparatorChar);
							}
							else
							{
								savedir.Append(DateTime.Now.ToString("yyyy"));
								savedir.Append(Path.DirectorySeparatorChar);
								savedir.Append(DateTime.Now.ToString("MM"));
								savedir.Append(Path.DirectorySeparatorChar);
								savedir.Append(DateTime.Now.ToString("dd"));
								savedir.Append(Path.DirectorySeparatorChar);
							}
						

                            newfilename = (Environment.TickCount & int.MaxValue).ToString() +  i.ToString() + random.Next(1000,9999).ToString() + "." + fileextname;
                            
                            //��ʱ�ļ����Ʊ���. ���ڵ�����Զ�̸���֮��,���ϴ���������ʱ�ļ��е�·����Ϣ
                            string tempfilename = "";
                            //��֧��FTP�ϴ������Ҳ��������ظ���ʱ
                            if (FTPs.GetForumAttachInfo.Allowupload == 1 && FTPs.GetForumAttachInfo.Reservelocalattach ==0)
                            {
                                // ���ָ��Ŀ¼������������ʱ·��
                                if (!Directory.Exists(UploadDir + "temp\\"))
                                {
                                    Utils.CreateDir(UploadDir + "temp\\");
                                }
                                tempfilename = "temp\\" + newfilename;
                            }
                            else
                            {
                                // ���ָ��Ŀ¼����������
                                if (!Directory.Exists(UploadDir + savedir.ToString()))
                                {
                                    Utils.CreateDir(UploadDir + savedir.ToString());
                                }
                            }
                            newfilename = savedir.ToString() + newfilename;

							try
							{
								// �����bmp jpg pngͼƬ����
								if ((fileextname == "bmp" || fileextname == "jpg" || fileextname == "jpeg" || fileextname == "png") && filetype.StartsWith("image"))
								{
														
									Image img = Image.FromStream(HttpContext.Current.Request.Files[i].InputStream);
									if (config.Attachimgmaxwidth > 0 && img.Width > config.Attachimgmaxwidth)
									{
										attachmentinfo[saveFileCount].Sys_noupload = "ͼƬ���Ϊ" + img.Width.ToString() + ", ϵͳ����������Ϊ" + config.Attachimgmaxwidth.ToString();
	
									}
									if (config.Attachimgmaxheight > 0 && img.Height > config.Attachimgmaxheight)
									{
										attachmentinfo[saveFileCount].Sys_noupload = "ͼƬ�߶�Ϊ" + img.Width.ToString() + ", ϵͳ��������߶�Ϊ" + config.Attachimgmaxheight.ToString();
									}									
									if (attachmentinfo[saveFileCount].Sys_noupload == "")
									{
										if (watermarkstatus == 0)
										{
                                            //��֧��FTP�ϴ������Ҳ��������ظ���ģʽʱ,�����ϴ�����ʱĿ¼��
                                            if (FTPs.GetForumAttachInfo.Allowupload == 1 && FTPs.GetForumAttachInfo.Reservelocalattach == 0)
                                            {
                                                HttpContext.Current.Request.Files[i].SaveAs(UploadDir + tempfilename);
                                            }
                                            else
                                            {
                                                HttpContext.Current.Request.Files[i].SaveAs(UploadDir + newfilename);
                                            }
											attachmentinfo[saveFileCount].Filesize = filesize;
                             			}
										else
										{
											if (config.Watermarktype == 1 && File.Exists(Utils.GetMapPath(BaseConfigs.GetForumPath + "watermark/" + config.Watermarkpic)))
											{
                                                //��֧��FTP�ϴ������Ҳ��������ظ���ģʽʱ,�����ϴ�����ʱĿ¼��
                                                if (FTPs.GetForumAttachInfo.Allowupload == 1 && FTPs.GetForumAttachInfo.Reservelocalattach == 0)
                                                {
                                                    AddImageSignPic(img, UploadDir + tempfilename, Utils.GetMapPath(BaseConfigs.GetForumPath + "watermark/" + config.Watermarkpic), config.Watermarkstatus, config.Attachimgquality, config.Watermarktransparency);
                                                }
                                                else
                                                {
                                                    AddImageSignPic(img, UploadDir + newfilename, Utils.GetMapPath(BaseConfigs.GetForumPath + "watermark/" + config.Watermarkpic), config.Watermarkstatus, config.Attachimgquality, config.Watermarktransparency);
                                                }
											}
											else
											{
												string watermarkText;
												watermarkText = config.Watermarktext.Replace("{1}", config.Forumtitle);
												watermarkText = watermarkText.Replace("{2}", "http://" + DNTRequest.GetCurrentFullHost() + "/");
												watermarkText = watermarkText.Replace("{3}", Utils.GetDate());
												watermarkText = watermarkText.Replace("{4}", Utils.GetTime());

                                                //��֧��FTP�ϴ������Ҳ��������ظ���ģʽʱ,�����ϴ�����ʱĿ¼��
                                                if (FTPs.GetForumAttachInfo.Allowupload == 1 && FTPs.GetForumAttachInfo.Reservelocalattach == 0)
                                                {
                                                    AddImageSignText(img, UploadDir + tempfilename, watermarkText, config.Watermarkstatus, config.Attachimgquality, config.Watermarkfontname, config.Watermarkfontsize);
                                                }
                                                else
                                                {
                                                    AddImageSignText(img, UploadDir + newfilename, watermarkText, config.Watermarkstatus, config.Attachimgquality, config.Watermarkfontname, config.Watermarkfontsize);
                                                }
											}


                                            //��֧��FTP�ϴ������Ҳ��������ظ���ģʽʱ,���ȡ��ʱĿ¼�µ��ļ���Ϣ
                                            if (FTPs.GetForumAttachInfo.Allowupload == 1 && FTPs.GetForumAttachInfo.Reservelocalattach == 0)
                                            {
                                                // ��ü�ˮӡ����ļ�����
                                                attachmentinfo[saveFileCount].Filesize = new FileInfo(UploadDir + tempfilename).Length;
                                            }
                                            else
                                            {
                                                // ��ü�ˮӡ����ļ�����
                                                attachmentinfo[saveFileCount].Filesize = new FileInfo(UploadDir + newfilename).Length;
                                            }
										}
									}
								}
								else
								{
                                    
									attachmentinfo[saveFileCount].Filesize = filesize;

                                    //��֧��FTP�ϴ������Ҳ��������ظ���ģʽʱ,�����ϴ�����ʱĿ¼��
                                    if (FTPs.GetForumAttachInfo.Allowupload == 1 && FTPs.GetForumAttachInfo.Reservelocalattach == 0)
                                    {
                                        HttpContext.Current.Request.Files[i].SaveAs(UploadDir + tempfilename);
                                    }
                                    else
                                    {
                                        HttpContext.Current.Request.Files[i].SaveAs(UploadDir + newfilename);
                                    }
								}
							}
							catch
							{
                                //���ϴ�Ŀ¼����ʱ�ļ��ж�û���ϴ����ļ�ʱ
                                if (!(Utils.FileExists(UploadDir + tempfilename)) && (!(Utils.FileExists(UploadDir + newfilename))))
								{
                                   
									attachmentinfo[saveFileCount].Filesize = filesize;

                                    //��֧��FTP�ϴ������Ҳ��������ظ���ģʽʱ,�����ϴ�����ʱĿ¼��
                                    if (FTPs.GetForumAttachInfo.Allowupload == 1 && FTPs.GetForumAttachInfo.Reservelocalattach == 0)
                                    {
                                        HttpContext.Current.Request.Files[i].SaveAs(UploadDir + tempfilename);
                                    }
                                    else
                                    {
                                        HttpContext.Current.Request.Files[i].SaveAs(UploadDir + newfilename);
                                    }
								}
        					}
							
							try
							{
							    //�����ļ�Ԥ����ָ������
                                IPreview preview = PreviewProvider.GetInstance(fileextname.Trim());
                                if (preview != null)
                                {

                                    preview.UseFTP = (FTPs.GetForumAttachInfo.Allowupload == 1) ? true : false;

                                    //��֧��FTP�ϴ������Ҳ��������ظ���ģʽʱ
                                    if (FTPs.GetForumAttachInfo.Allowupload == 1 && FTPs.GetForumAttachInfo.Reservelocalattach == 0)
                                    {
                                        preview.OnSaved(UploadDir + tempfilename);
                                    }
                                    else
                                    {
                                        preview.OnSaved(UploadDir + newfilename);
                                    }
                                }
							}
							catch
							{}

                            //��֧��FTP�ϴ�����ʱ,ʹ��FTP�ϴ�Զ�̸���
                            if (FTPs.GetForumAttachInfo.Allowupload == 1)
                            {
                                FTPs ftps = new FTPs();

                                //�����������ظ���ģʽʱ,���ϴ����֮��ɾ������tempfilename�ļ�
                                if (FTPs.GetForumAttachInfo.Reservelocalattach == 0)
                                {
                                    ftps.UpLoadFile(newfilename.Substring(0, newfilename.LastIndexOf("\\")), UploadDir + tempfilename, FTPs.FTPUploadEnum.ForumAttach);
                                }
                                else
                                {
                                    ftps.UpLoadFile(newfilename.Substring(0, newfilename.LastIndexOf("\\")), UploadDir + newfilename, FTPs.FTPUploadEnum.ForumAttach);
                                }
                            }
  
						}
						else
						{
							if (extnameid < 0)
							{
								attachmentinfo[saveFileCount].Sys_noupload = "�ļ���ʽ��Ч";
							}
							else if (MaxSizePerDay - TodayUploadedSize < filesize)
							{
								attachmentinfo[saveFileCount].Sys_noupload = "�ļ����ڽ��������ϴ����ֽ���";
							}
							else if (filesize > MaxSize[extnameid])
							{
								attachmentinfo[saveFileCount].Sys_noupload = "�ļ����ڸ����͸���������ֽ���";
							}
							else
							{
								attachmentinfo[saveFileCount].Sys_noupload = "�ļ����ڵ����ļ������ϴ����ֽ���";
							}
						}
					}
					else
					{
						attachmentinfo[saveFileCount].Sys_noupload = "�ļ���ʽ��Ч";
					}
                    //��֧��FTP�ϴ�����ʱ
                    if (FTPs.GetForumAttachInfo.Allowupload == 1)
                    {
                        attachmentinfo[saveFileCount].Filename = FTPs.GetForumAttachInfo.Remoteurl + "/" + newfilename.Replace("\\","/");
                    }
                    else
                    {
                        attachmentinfo[saveFileCount].Filename = newfilename;
                    }
					attachmentinfo[saveFileCount].Description = fileextname;
					attachmentinfo[saveFileCount].Filetype = filetype;
					attachmentinfo[saveFileCount].Attachment = filename;
					attachmentinfo[saveFileCount].Downloads = 0;
					attachmentinfo[saveFileCount].Postdatetime = DateTime.Now.ToString();
					attachmentinfo[saveFileCount].Sys_index = i;
					saveFileCount++;
				}
			}
			return attachmentinfo;
			
		}

		/// <summary>
		/// ���ط��ʹ�����̳���б�html
		/// </summary>
		/// <param name="count">�����ʾ����</param>
		/// <returns>�б�html</returns>
		public static string GetVisitedForumsOptions(int count)
		{
			if (GetCookie("visitedforums").Trim() == "")
			{
				return "";
			}
			StringBuilder sb = new StringBuilder();
			string[] strfid = Utils.SplitString(GetCookie("visitedforums"), ",");
			for (int fidIndex = 0; fidIndex < strfid.Length; fidIndex++)
			{
				ForumInfo foruminfo = Forums.GetForumInfo(Utils.StrToInt(strfid[fidIndex], 0));
				if (foruminfo != null)
				{
					sb.Append("<option value=\"");
					sb.Append(strfid[fidIndex]);
					sb.Append("\">");
					sb.Append(foruminfo.Name);
					sb.Append("</option>");
				}
			}
			return sb.ToString();
		}


		/// <summary>
		/// �����ѷ��ʰ��id����ʷ��¼cookie
		/// </summary>
		/// <param name="fid">Ҫ����İ��id</param>
		public static void UpdateVisitedForumsOptions(int fid)
		{
			if (GetCookie("visitedforums").Trim() == "")
			{
				WriteCookie("visitedforums", fid.ToString());
			}
			else
			{
				bool fidExists = false;
				string[] strfid = Utils.SplitString(GetCookie("visitedforums"), ",");
				for (int fidIndex = 0; fidIndex < strfid.Length; fidIndex++)
				{
					if (strfid[fidIndex] == fid.ToString())
					{
						fidExists = true;
					}
				}
				if (!fidExists)
				{
					WriteCookie("visitedforums", fid.ToString() + "," + GetCookie("visitedforums"));
				}
			}
			return;
		}

		/// <summary>
		/// �滻ԭʼ�ַ����е����ִ���
		/// </summary>
		/// <param name="text">ԭʼ�ַ���</param>
		/// <returns>�滻��Ľ��</returns>
		public static string BanWordFilter(string text)
		{
			StringBuilder sb = new StringBuilder(text);
			string[,] str = Caches.GetBanWordList();
			int count = str.Length / 2;
            for (int i = 0; i < count; i++)
			{
                if (str[i, 1] != "{BANNED}" && str[i, 1] != "{MOD}")
                {
                    sb = new StringBuilder().Append(Regex.Replace(sb.ToString(), str[i, 0], str[i, 1], Utils.GetRegexCompiledOptions()));//new Regex(();
                    //foreach (Match m in r_word.Matches())
                    //{
                    //    sb.Replace(m.Groups[0].ToString(), str[i, 1]);
                    //}
                }
				//Replace(str[i,0], str[i,1]);
			}
			return sb.ToString();
		}

		/// <summary>
		/// �ж��ַ����Ƿ�������ִ���
		/// </summary>
		/// <param name="text">ԭʼ�ַ���</param>
		/// <returns>��������򷵻�true, ���򷴻�false</returns>
		public static bool InBanWordArray(string text)
		{
			string[,] str = Caches.GetBanWordList();
			int count = str.Length / 2;

            for (int i = 0; i < count; i++)
            {
                r_word = new Regex(str[i, 0], Utils.GetRegexCompiledOptions());
                foreach (Match m in r_word.Matches(text))
                {
                    return true;
                }
            }
			return false;
		}

        /// <summary>
        /// ָ�����ַ������Ƿ��н�ֹ�ʻ�
        /// </summary>
        /// <param name="text">�ַ���</param>
        /// <returns></returns>
        public static bool HasBannedWord(string text)
        {
            string[,] str = Caches.GetBanWordList();
            int count = str.Length / 2;

            for (int i = 0; i < count; i++)
            {
                if (str[i, 1] == "{BANNED}")
                {
                    r_word = new Regex(str[i, 0], Utils.GetRegexCompiledOptions());
                    foreach (Match m in r_word.Matches(text))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// ָ�����ַ������Ƿ�����Ҫ��˴ʻ�
        /// </summary>
        /// <param name="text">�ַ���</param>
        /// <returns>bool</returns>
        public static bool HasAuditWord(string text)
        {
            string[,] str = Caches.GetBanWordList();
            int count = str.Length / 2;

            for (int i = 0; i < count; i++)
            {
                if (str[i, 1] == "{MOD}")
                {
                    r_word = new Regex(str[i, 0], Utils.GetRegexCompiledOptions());
                    foreach (Match m in r_word.Matches(text))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

		/// <summary>
		/// ��������ͼƬhtml
		/// </summary>
		/// <param name="starcount">��������</param>
		/// <param name="starthreshold">���Ƿ�ֵ</param>
		/// <returns>����ͼƬhtml</returns>
		public static string GetStarImg(int starcount, int starthreshold)
		{
			StringBuilder sb = new StringBuilder();
			int len = starcount / (starthreshold * starthreshold);
			for (int i = 0; i < len; i++)
			{
				sb.Append("<img src=\"star_level3.gif\" />");
			}
			starcount = starcount - len * starthreshold * starthreshold;

			len = starcount / starthreshold;
			for (int i = 0; i < len; i++)
			{
				sb.Append("<img src=\"star_level2.gif\" />");
			}
			starcount = starcount - len * starthreshold;

			len = starcount;
			for (int i = 0; i < len; i++)
			{
				sb.Append("<img src=\"star_level1.gif\" />");
			}

			return sb.ToString();
		}

		/// <summary>
		/// ���ص�ǰҳ���Ƿ��ǿ�վ�ύ
		/// </summary>
		/// <returns>��ǰҳ���Ƿ��ǿ�վ�ύ</returns>
		public static bool IsCrossSitePost()
		{
		
			// ��������ύ��Ϊtrue
			if (!DNTRequest.IsPost())
			{
				return true;
			}
			return IsCrossSitePost(DNTRequest.GetUrlReferrer(), DNTRequest.GetHost());
		}
		
		/// <summary>
		/// �ж��Ƿ��ǿ�վ�ύ
		/// </summary>
		/// <param name="urlReferrer">�ϸ�ҳ���ַ</param>
		/// <param name="host">��̳url</param>
        /// <returns>bool</returns>
		public static bool IsCrossSitePost(string urlReferrer, string host)
		{
			if (urlReferrer.Length < 7)
			{
				return true;
			}
			// �Ƴ�http://
//			string tmpReferrer = urlReferrer.Remove(0, 7);
//			if (tmpReferrer.IndexOf(":") > -1)
//				tmpReferrer = tmpReferrer.Substring(0, tmpReferrer.IndexOf(":"));
//			else
//				tmpReferrer = tmpReferrer.Substring(0, tmpReferrer.IndexOf('/'));

			Uri u = new Uri(urlReferrer);
			return u.Host != host;
		}

		/// <summary>
		/// ���Assembly�汾��
		/// </summary>
        /// <returns>string</returns>
		public static string GetAssemblyVersion()
		{
			Assembly myAssembly = Assembly.GetExecutingAssembly();
			FileVersionInfo myFileVersion = FileVersionInfo.GetVersionInfo(myAssembly.Location);
			return string.Format("{0}.{1}.{2}",myFileVersion.FileMajorPart, myFileVersion.FileMinorPart, myFileVersion.FileBuildPart);
		}

		/// <summary>
		/// �������Ƿ����[hide]...[/hide]
		/// </summary>
		/// <param name="str">��������</param>
		/// <returns>�Ƿ����</returns>
		public static bool IsHidePost(string str)
		{
			return (str.IndexOf("[hide]") >= 0) && (str.IndexOf("[/hide]") > 0);
		}

		/// <summary>
		/// ������ʾħ������flash���xhtml�ַ���
		/// </summary>
		/// <param name="magic">ħ������id</param>
        /// <returns>string</returns>
		public static string ShowTopicMagic(int magic)
		{
			if (magic <= 0)
			{
				return "";
			}
			return "\r\n<!-- DNT Magic (ID:" + magic.ToString() + ") -->\r\n<object width=\"400\" height=\"300\" id=\"dntmagic\" classid=\"clsid:d27cdb6e-ae6d-11cf-96b8-444553540000\" codebase=\"http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,0,0\" align=\"middle\" style=\"position:absolute;z-index:111;display:none;\"> \r\n<param name=\"allowScriptAccess\" value=\"sameDomain\" />\r\n<param name=\"movie\" value=\"magic/swf/" + magic.ToString() + ".swf\" />\r\n<param name=\"loop\" value=\"false\" />\r\n<param name=\"menu\" value=\"false\" />\r\n<param name=\"quality\" value=\"high\" />\r\n<param name=\"scale\" value=\"noscale\" />\r\n<param name=\"salign\" value=\"lt\" />\r\n<param name=\"wmode\" value=\"transparent\" /><param name=\"PLAY\" value=\"false\" /> \r\n<embed src=\"magic/swf/" + magic.ToString() + ".swf\" width=\"400\" height=\"300\" loop=\"false\" align=\"middle\" menu=\"false\" quality=\"high\" scale=\"noscale\" salign=\"lt\" wmode=\"transparent\" play=\"false\" allowScriptAccess=\"sameDomain\" type=\"application/x-shockwave-flash\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" /> \r\n</object>\r\n<script language=\"javascript\">\r\nfunction $(id){\r\n\treturn document.getElementById(id);\r\n}\r\nfunction playFlash(){\r\n\tdivElement = $('dntmagic');\r\n\tdivElement.style.display = '';\r\n\tdivElement.style.left = (document.documentElement.clientWidth /2 - parseInt(divElement.offsetWidth)/2)+\"px\";\r\n\tdivElement.style.top = (document.documentElement.clientHeight /2 - parseInt(divElement.offsetHeight)/2 )+\"px\";\r\n\tsetTimeout(\"hiddenFlash()\", 5000);\r\n}\r\n\r\nfunction hiddenFlash() {\r\n\t$('dntmagic').style.display = 'none';\r\n}\r\nplayFlash();\r\n</script>\r\n<!-- DNT Magic End -->\r\n";
		}

        private static RegexOptions options = RegexOptions.IgnoreCase;

        public static Regex[] r = new Regex[4];

        /// <summary>
        /// Ϊ������������������
        /// </summary>
        static ForumUtils()
        {
            r[0] = new Regex(@"(\r\n)", options);
            r[1] = new Regex(@"(\n)", options);
            r[2] = new Regex(@"(\r)", options);
            r[3] = new Regex(@"(<br />)", options);
        }

		/// <summary>
		/// ���������ݼ��ϸ�����
		/// </summary>
		/// <param name="message">��������</param>
		/// <returns>�������������������</returns>
		public static string AddJammer(string message)
		{
			Match m;
			string jammer = Caches.GetJammer();

            m = r[0].Match(message);
            if (m.Success)
            {
                message = message.Replace(m.Groups[0].ToString(), jammer + "\r\n");
            }

            m = r[1].Match(message);
            if (m.Success)
            {
                message = message.Replace(m.Groups[0].ToString(), jammer + "\n");
            }
   
            m = r[2].Match(message);
            if (m.Success)
            {
                message = message.Replace(m.Groups[0].ToString(), jammer + "\r");
            }

            m = r[3].Match(message);
            if (m.Success)
            {
                message = message.Replace(m.Groups[0].ToString(), jammer + "<br />");
            }
          	
			return message + jammer;

		}


		/// <summary>
		/// �Ƿ��ǹ��˵��û���
		/// </summary>
		/// <param name="str"></param>
		/// <param name="stringarray"></param>
        /// <returns>bool</returns>
		public static bool IsBanUsername(string str, string stringarray)
		{
			if (stringarray == null || stringarray == "") 
				return false;

			stringarray = Regex.Escape(stringarray).Replace(@"\*",@"[\s\S]*");
			Regex r;
			foreach (string strarray in Utils.SplitString(stringarray, "\\n"))
			{
                r = new Regex(string.Format("^{0}$", strarray), RegexOptions.IgnoreCase);
				if (r.IsMatch(str) && (!strarray.Trim().Equals("")))
				{
					return true;
				}
			}
			return false;

			//return Utils.IsCompriseStr(str, stringarray, "\n");
		}



		/// <summary>
		/// ��cookie�л�ȡת��ҳ
		/// </summary>
        /// <returns>string</returns>
		public static string GetReUrl()
		{
			if (DNTRequest.GetString("reurl").Trim() != "")
			{
				Utils.WriteCookie("reurl", DNTRequest.GetString("reurl").Trim());
				return DNTRequest.GetString("reurl").Trim();
			}
			else
			{
				if (Utils.GetCookie("reurl") == "")
				{
					return "index.aspx";
				}
				else
				{
					return Utils.GetCookie("reurl");
				}
			}
		}

		/// <summary>
		/// �Ƿ�Ϊ��Ч��
		/// </summary>
		/// <param name="host">����</param>
		/// <returns></returns>
		public static bool IsValidDomain(string host)
		{
			Regex r = new Regex(@"^\d+$");
			if (host.IndexOf(".") == -1)
			{
				return false;
			}
			return r.IsMatch(host.Replace(".", string.Empty)) ? false : true;
		}


		/// <summary>
		/// ����·��url���е���չ��
		/// </summary>
		/// <param name="pathlist">·��url��</param>
		/// <param name="extname">��չ��</param>
        /// <returns>string</returns>
		public static string UpdatePathListExtname(string pathlist, string extname)
		{
			return pathlist.Replace("{extname}", extname);
		}

        public static void CreateTextImage(string filename, string watermarkText, int quality, string fontname, int fontsize, Color fontcolor)
		{
            Font drawFont = new Font(fontname, fontsize, FontStyle.Regular, GraphicsUnit.Pixel);
            
		    Bitmap bmp = new Bitmap(100, 50);
            Graphics g = Graphics.FromImage(bmp);
            SizeF crSize;
            crSize = g.MeasureString(watermarkText, drawFont);
            bmp = new Bitmap((int)crSize.Width, (int)crSize.Height);
            
            g = Graphics.FromImage(bmp);
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.Clear(Color.Transparent);
            g.FillRectangle(new SolidBrush(Color.Transparent), 0, 0, crSize.Width, crSize.Height);
           
            float xpos = 0;
            float ypos = 0;


            //g.DrawString(watermarkText, drawFont, new SolidBrush(Color.White), xpos + 1, ypos + 1);
            g.DrawString(watermarkText, drawFont, new SolidBrush(fontcolor), xpos, ypos);

            bmp.Save(filename, ImageFormat.Png);
            g.Dispose();
            //bmp.Dispose();
            bmp.Dispose();
        }
		
		
	}// end class
}
