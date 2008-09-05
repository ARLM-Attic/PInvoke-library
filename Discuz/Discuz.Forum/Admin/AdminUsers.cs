using System;
using System.Data;
using System.Data.Common;

using Discuz.Common;
using Discuz.Data;
using Discuz.Config;
using Discuz.Entity;

namespace Discuz.Forum
{
	/// <summary>
	/// UserFactoryAdmin ��ժҪ˵����
	/// ��̨�û���Ϣ����������
	/// </summary>
	public class AdminUsers : Users
	{
		public AdminUsers()
		{
		}


		/// <summary>
		/// �����û�ȫ����Ϣ
		/// </summary>
		/// <param name="__userinfo"></param>
		/// <returns></returns>
		public static bool UpdateUserAllInfo(UserInfo __userinfo)
		{
            DatabaseProvider.GetInstance().UpdateUserAllInfo(__userinfo);

			//���û����ǰ���(��������)�����Ա
			if ((__userinfo.Adminid == 0) || (__userinfo.Adminid > 3))
			{
				//ɾ���û��ڰ����б����������
                DatabaseProvider.GetInstance().DeleteModerator(__userinfo.Uid);				

				//ͬʱ���°����صİ�����Ϣ
				UpdateForumsFieldModerators(__userinfo.Username);
			}

			#region ����Ϊ���¸��û�����չ��Ϣ

			string signature = Utils.HtmlEncode(ForumUtils.BanWordFilter(__userinfo.Signature));

			UserGroupInfo usergroupinfo = AdminUserGroups.AdminGetUserGroupInfo(__userinfo.Groupid);
            GeneralConfigInfo config = GeneralConfigs.GetConfig();

			PostpramsInfo _postpramsinfo = new PostpramsInfo();
			_postpramsinfo.Usergroupid = usergroupinfo.Groupid;
			_postpramsinfo.Attachimgpost = config.Attachimgpost;
			_postpramsinfo.Showattachmentpath = config.Showattachmentpath;
			_postpramsinfo.Hide = 0;
			_postpramsinfo.Price = 0;
			_postpramsinfo.Sdetail = __userinfo.Signature;
			_postpramsinfo.Smileyoff = 1;
			_postpramsinfo.Bbcodeoff = 1 - usergroupinfo.Allowsigbbcode;
			_postpramsinfo.Parseurloff = 1;
			_postpramsinfo.Showimages = usergroupinfo.Allowsigimgcode;
			_postpramsinfo.Allowhtml = 0;
			_postpramsinfo.Smiliesinfo = Smilies.GetSmiliesListWithInfo();
			_postpramsinfo.Customeditorbuttoninfo = Editors.GetCustomEditButtonListWithInfo();
			_postpramsinfo.Smiliesmax = config.Smiliesmax;
			_postpramsinfo.Signature = 1;
			_postpramsinfo.Onlinetimeout = config.Onlinetimeout;

            DatabaseProvider.GetInstance().UpdateUserField(__userinfo, signature, ForumUtils.CreateAuthStr(20), UBB.UBBToHTML(_postpramsinfo));

			#endregion

			Users.UpdateUserForumSetting(__userinfo);

			return true;

		}

		/// <summary>
		/// �����û���
		/// </summary>
		/// <param name="__userinfo">��ǰ�û���Ϣ</param>
		/// <param name="oldusername">��ǰ�û�������</param>
		/// <returns></returns>
		public static bool UserNameChange(UserInfo __userinfo, string oldusername)
		{
			//���������
            DatabaseProvider.GetInstance().UpdateTopicLastPoster(__userinfo.Uid, __userinfo.Username);
            DatabaseProvider.GetInstance().UpdateTopicPoster(__userinfo.Uid, __userinfo.Username);

			//�������ӱ�
            foreach (DataRow dr in DatabaseProvider.GetInstance().GetTableListIds())
			{
                DatabaseProvider.GetInstance().UpdatePostPoster(__userinfo.Uid, __userinfo.Username, dr["id"].ToString());
			}

			//���¶���Ϣ
            DatabaseProvider.GetInstance().UpdatePMSender(__userinfo.Uid, __userinfo.Username);
            DatabaseProvider.GetInstance().UpdatePMReceiver(__userinfo.Uid, __userinfo.Username);

			//���¹���
            DatabaseProvider.GetInstance().UpdateAnnouncementPoster(__userinfo.Uid, __userinfo.Username);

			//����ͳ�Ʊ��е���Ϣ
            if (DatabaseProvider.GetInstance().HasStatisticsByLastUserId(__userinfo.Uid))
			{

                DatabaseProvider.GetInstance().UpdateStatisticsLastUserName(__userinfo.Uid, __userinfo.Username);
				//���»���
                Discuz.Cache.DNTCache.GetCacheService().RemoveObject("/Forum/Statistics");
			}


			//������̳���������Ϣ
			foreach (DataRow dr in DatabaseProvider.GetInstance().GetModerators(oldusername))
			{
				string moderators = "," + dr["moderators"].ToString().Trim() + ",";
				if (moderators.IndexOf("," + oldusername + ",") >= 0)
				{
                    DatabaseProvider.GetInstance().UpdateModerators(Utils.StrToInt(dr["fid"], 0), dr["moderators"].ToString().Trim().Replace(oldusername, __userinfo.Username));
				}
			}
			return true;
		}


		/// <summary>
		/// ɾ��ָ���û���������Ϣ
		/// </summary>
		/// <param name="uid">ָ�����û�uid</param>
		/// <param name="delposts">�Ƿ�ɾ������</param>
		/// <param name="delpms">�Ƿ�ɾ������Ϣ</param>
		/// <returns></returns>
		public static bool DelUserAllInf(int uid, bool delposts, bool delpms)
		{            
            bool val = DatabaseProvider.GetInstance().DelUserAllInf(uid, delposts, delpms);
            Discuz.Cache.DNTCache.GetCacheService().RemoveObject("/Forum/Statistics");
            return val;
		}


		/// <summary>
		/// ���µ�ǰ�û����ڰ�������еİ�����Ϣ
		/// </summary>
		/// <param name="username">��ǰ�û�������</param>
		public static void UpdateForumsFieldModerators(string username)
		{
			//ɾ�������������û���Ϣ
            DataTable dt = DatabaseProvider.GetInstance().GetModeratorsTable(username);
			if (dt.Rows.Count > 0)
			{
				string updatestr = "";
				foreach (DataRow dr in dt.Rows)
				{
					updatestr = dr["moderators"].ToString().Replace(username + ",", "");
					updatestr = updatestr.Replace("," + username, "");
					updatestr = updatestr.Replace(username, "");
                    DatabaseProvider.GetInstance().UpdateModerators(Utils.StrToInt(dr["fid"], 0), updatestr);
				}
			}
		}


		/// <summary>
		/// �ϲ��û�
		/// </summary>
		/// <param name="srcuid">Դ�û�ID</param>
		/// <param name="targetuid">Ŀ���û�ID</param>
		/// <returns></returns>
		public static bool CombinationUser(int srcuid, int targetuid)
		{
			try
			{
				//���ֺϲ�
				UserInfo __srcuserinfo = Users.GetUserInfo(srcuid);
				UserInfo __targetuserinfo = Users.GetUserInfo(targetuid);
                DatabaseProvider.GetInstance().UpdateUserCredits(targetuid, __srcuserinfo.Credits + __targetuserinfo.Credits, __srcuserinfo.Extcredits1 + __targetuserinfo.Extcredits1, __srcuserinfo.Extcredits2 + __targetuserinfo.Extcredits2,
                                                __srcuserinfo.Extcredits3 + __targetuserinfo.Extcredits3, __srcuserinfo.Extcredits4 + __targetuserinfo.Extcredits4, __srcuserinfo.Extcredits5 + __targetuserinfo.Extcredits5,
                                                __srcuserinfo.Extcredits6 + __targetuserinfo.Extcredits6, __srcuserinfo.Extcredits7 + __targetuserinfo.Extcredits7, __srcuserinfo.Extcredits8 + __targetuserinfo.Extcredits8);

                DatabaseProvider.GetInstance().CombinationUser(Posts.GetPostTableName(), __targetuserinfo, __srcuserinfo);

				//ɾ�����ϲ��û������������Ϣ
				DelUserAllInf(srcuid, true, true);

				return true;
			}
			catch
			{
				return false;
			}
		}


		/// <summary>
		/// ͨ���û����õ�UID
		/// </summary>
		/// <param name="username"></param>
		/// <returns></returns>
		public static int GetuidByusername(string username)
		{
            return DatabaseProvider.GetInstance().GetuidByusername(username);
		}
	}
}