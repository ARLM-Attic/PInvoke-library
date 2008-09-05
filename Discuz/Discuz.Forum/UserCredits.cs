using System;
using System.Data;
using System.Data.Common;

using Discuz.Common;
using Discuz.Data;
using Discuz.Forum;
using Discuz.Config;
using Discuz.Entity;

namespace Discuz.Forum
{
	/// <summary>
	/// UserCreditsFactory ��ժҪ˵����
	/// </summary>
	public class UserCredits
	{

		/// <summary>
		/// ���ϵͳ���õ��ܻ��ּ��㹫ʽ
		/// </summary>
		/// <returns>���㹫ʽ</returns>
		private static string GetCreditsArithmetic(int uid)
		{
            string ArithmeticStr = Scoresets.GetScoreCalFormula();
			if (ArithmeticStr.Equals(""))
			{
				return "0";
			}
			string[] para = {
								"digestposts",
								"posts",
								"oltime",
								"pageviews",
								"extcredits1",
								"extcredits2",
								"extcredits3",
								"extcredits4",
								"extcredits5",
								"extcredits6",
								"extcredits7",
								"extcredits8"
							};


			IDataReader  reader = Users.GetShortUserInfoToReader(uid);
			if (reader != null)
			{
				if (reader.Read())
				{
					for (int i = 0; i < para.Length; i++)
					{
						ArithmeticStr = ArithmeticStr.Replace(para[i],Utils.StrToFloat(reader[para[i]],0).ToString());
					}
				}
				reader.Close();
			}
			return ArithmeticStr;
		}


		/// <summary>
		/// ���ݻ��ֹ�ʽ�����û�����,�����ܷ����䶯Ӱ���п��ܻ�����û��������û���
		/// <param name="uid">�û�ID</param>
		/// </summary>
		public static int UpdateUserCredits(int uid)
		{
			UserInfo tmpUserInfo = Users.GetUserInfo(uid);
			if (tmpUserInfo == null)
			{
				return 0;
			}

            DatabaseProvider.GetInstance().UpdateUserCredits(uid, Scoresets.GetScoreCalFormula());
			tmpUserInfo = Users.GetUserInfo(uid);
			UserGroupInfo tmpUserGroupInfo = UserGroups.GetUserGroupInfo(tmpUserInfo.Groupid);

            if (tmpUserGroupInfo != null && tmpUserGroupInfo.System == 0 && tmpUserGroupInfo.Radminid == 0)
            {
                tmpUserGroupInfo = GetCreditsUserGroupID(tmpUserInfo.Credits);
                DatabaseProvider.GetInstance().UpdateUserGroup(uid, tmpUserGroupInfo.Groupid);
                OnlineUsers.UpdateGroupid(uid, tmpUserGroupInfo.Groupid);
            }
            else 
            {
                //���û�����ɾ�����������Աʱ����������Ӧ���֣������¸��û���������Ϣ
                if (tmpUserGroupInfo != null && tmpUserGroupInfo.Groupid == 7 && tmpUserInfo.Adminid == -1)
                {
                    tmpUserGroupInfo = GetCreditsUserGroupID(tmpUserInfo.Credits);
                    DatabaseProvider.GetInstance().UpdateUserGroup(uid, tmpUserGroupInfo.Groupid);
                    OnlineUsers.UpdateGroupid(uid, tmpUserGroupInfo.Groupid);
                }
            }
			return 1;

		}


		/// <summary>
		/// �����û�����(�����ڵ�������)
		/// </summary>
		/// <param name="uid">�û�ID</param>
		/// <param name="creditsOperationType">���ֲ�������,�緢����</param>
		/// <param name="pos">�ӻ����־(����Ϊ��,����Ϊ��,ͨ��������1����-1)</param>
		private static int UpdateUserCredits(int uid, CreditsOperationType creditsOperationType, int pos)
		{
			return UpdateUserCredits(uid, 1, creditsOperationType, pos);
		}

		/// <summary>
		/// ͨ��ָ��ֵ�����û�����
		/// </summary>
		/// <param name="uid">�û�ID</param>
		/// <param name="values">���ֱ䶯ֵ,Ӧ��֤��һ������Ϊ8������,��Ӧ8����չ���ֵı䶯ֵ</param>
		/// <param name="allowMinus">�Ƿ������۳ɸ���,true����,false�������Ҳ����п۷ַ���-1</param>
		/// <returns></returns>
		private static int UpdateUserCredits(int uid,float[] values, bool allowMinus)
		{
			UserInfo tmpUserInfo = Users.GetUserInfo(uid);
			if (tmpUserInfo == null)
			{
				return 0;
			}
			
			if (values.Length < 8)
			{
				return -1;
			}
			if (!allowMinus)//������۳ɸ���ʱҪ�����жϻ����Ƿ��㹻����
			{
				// ���Ҫ����չ����, �����ж���չ�����Ƿ��㹻����
                if (!DatabaseProvider.GetInstance().CheckUserCreditsIsEnough(uid, values))
                {
                    return -1;
                }
			}

            DatabaseProvider.GetInstance().UpdateUserCredits(uid, values);
			
			///�����û�����
			return UpdateUserCredits(uid);
		}

		/// <summary>
		/// ͨ��ָ��ֵ�����û�����(���ֲ���ʱ����,����-1)
		/// </summary>
		/// <param name="uid">�û�ID</param>
		/// <param name="values">���ֱ䶯ֵ,Ӧ��֤��һ������Ϊ8������,��Ӧ8����չ���ֵı䶯ֵ</param>
		private static int UpdateUserCredits(int uid,float[] values)
		{
			return UpdateUserCredits(uid,values,false);
		}

        /// <summary>
        /// ����û������Ƿ��㹻����(�����ڵ��û�, ������������)
        /// </summary>
        /// <param name="uid">�û�ID</param>
        /// <param name="mount">��������,�������ϴ�2�����������˲���,��ô�˲���ֵӦΪ2</param>
        /// <param name="creditsOperationType">���ֲ�������,�緢����</param>
        /// <param name="pos">�ӻ����־(����Ϊ��,����Ϊ��,ͨ��������1����-1)</param>
        /// <returns></returns>
        public static bool CheckUserCreditsIsEnough(int uid, int mount, CreditsOperationType creditsOperationType, int pos)
        {
            DataTable dt = Scoresets.GetScoreSet();
            DataColumn[] keys = new DataColumn[1];
            keys[0] = dt.Columns["id"];
            dt.PrimaryKey = keys;
            DataRow dr = dt.Rows[(int)creditsOperationType];

            for (int i = 2; i < 10; i++)
            {
                if (Utils.StrToFloat(dr[i], 0) < 0)//ֻҪ�κ�һ��Ҫ�����,��ȥ���ݿ���
                {
                    return DatabaseProvider.GetInstance().CheckUserCreditsIsEnough(uid, dr, pos, mount);
                }
            }
            return true;           
        }

		/// <summary>
		/// �����û�����(�����ڵ��û�,������������)
		/// </summary>
		/// <param name="uid">�û�ID</param>
		/// <param name="mount">��������,�������ϴ�2�����������˲���,��ô�˲���ֵӦΪ2</param>
		/// <param name="creditsOperationType">���ֲ�������,�緢����</param>
		/// <param name="pos">�ӻ����־(����Ϊ��,����Ϊ��,ͨ��������1����-1)</param>
		/// <param name="allowMinus">�Ƿ������۳ɸ���,true����,false�������Ҳ����п۷ַ���-1</param>
		/// <returns></returns>
		private static int UpdateUserCredits(int uid, int mount, CreditsOperationType creditsOperationType, int pos, bool allowMinus)
		{
			if (!Users.Exists(uid))
			{
				return 0;
			}

            DataTable dt = Scoresets.GetScoreSet();
			DataColumn[] keys = new DataColumn[1];
			keys[0] = dt.Columns["id"];
			dt.PrimaryKey = keys;
			DataRow dr = dt.Rows[(int)creditsOperationType];

			// ���Ҫ����չ����, �����ж���չ�����Ƿ��㹻����
            if (pos < 0)
            {
                //������ɾ�������ظ�ʱ
                if (creditsOperationType != CreditsOperationType.PostTopic && creditsOperationType != CreditsOperationType.PostReply)
                {
                    if (!allowMinus && !DatabaseProvider.GetInstance().CheckUserCreditsIsEnough(uid, dr, pos, mount))
                    {
                        return -1;
                    }
                }
            }
            DatabaseProvider.GetInstance().UpdateUserCredits(uid, dr, pos, mount);

			///�����û�����
			return UpdateUserCredits(uid);

		}


		/// <summary>
		/// �����û�����(���۷�ʱ,����û�ʣ���ֵ����,�򲻿�)
		/// </summary>
		/// <param name="uid">�û�ID</param>
		/// <param name="mount">��������,�������ϴ�2�����������˲���,��ô�˲���ֵӦΪ2</param>
		/// <param name="creditsOperationType">���ֲ�������,�緢����</param>
		/// <param name="pos">�ӻ����־(����Ϊ��,����Ϊ��,ͨ��������1����-1)</param>
		private static int UpdateUserCredits(int uid,int mount, CreditsOperationType creditsOperationType, int pos)
		{
			return UpdateUserCredits(uid, mount, creditsOperationType, pos, false);
		}



		/// <summary>
		/// �����û��б�,һ�θ��¶���û��Ļ���
		/// </summary>
		/// <param name="uidlist">�û�ID�б�</param>
		/// <param name="values">��չ����ֵ</param>
		private static int UpdateUserCredits(string uidlist,float[] values)
		{
	

			if (Utils.IsNumericArray(Utils.SplitString(uidlist,","))){
				///���ݹ�ʽ�����û����ܻ���,������
				int reval = 0;
				foreach(string uid in Utils.SplitString(uidlist,","))
				{
					if (Utils.StrToInt(uid,0) > 0)
					{
						reval = reval + UpdateUserCredits(Utils.StrToInt(uid,0),values, true);
					}
				}
				
				return reval;
			}
			return -1;

		}


		
		/// <summary>
		/// �����û��б�,һ�θ��¶���û��Ļ���
		/// </summary>
		/// <param name="uidlist">�û�ID�б�</param>
		/// <param name="creditsOperationType">���ֲ�������,�緢����</param>
		/// <param name="pos">�ӻ����־(����Ϊ��,����Ϊ��,ͨ��������1����-1)</param>
		private static int UpdateUserCredits(string uidlist, CreditsOperationType creditsOperationType,int pos)
		{
			if (Utils.IsNumericArray(Utils.SplitString(uidlist, ",")))
			{
				///���ݹ�ʽ�����û����ܻ���,������
				int reval = 0;
				foreach(string uid in Utils.SplitString(uidlist, ","))
				{
					if (Utils.StrToInt(uid,0) > 0)
					{
						reval = reval + UpdateUserCredits(Utils.StrToInt(uid, 0), 1, creditsOperationType, pos);
					}
				}
				
				return reval;
			}
			return 0;

		}


		/// <summary>
		/// �����û��б�,һ�θ��¶���û��Ļ���(�˷���ֻ��ɾ������ʱʹ�ù�)
		/// </summary>
		/// <param name="uidlist">�û�ID�б�</param>
		/// <param name="creditsOperationType">���ֲ�������,�緢����</param>
		/// <param name="pos">�ӻ����־(����Ϊ��,����Ϊ��,ͨ��������1����-1)</param>
		private static int UpdateUserCredits(int[] uidlist, CreditsOperationType creditsOperationType,int pos)
		{
			///���ݹ�ʽ�����û����ܻ���,������
			int reval = 0;
			for (int i = 0; i < uidlist.Length; i++)
			{
				if (uidlist[i] > 0)
				{
					reval = reval + UpdateUserCredits(uidlist[i], 1, creditsOperationType, pos, true);
				}
			}
			
			return reval;
		}

		/// <summary>
		/// �����û��б�,һ�θ��¶���û��Ļ���(�˷���ֻ��ɾ������ʱʹ�ù�)
		/// </summary>
		/// <param name="uidlist">�û�ID�б�</param>
		/// <param name="mountlist">��������,�������ϴ�2�����������˲���,��ô�˲���ֵӦΪ2,���鳤��Ӧ��uidlist��ͬ</param>
		/// <param name="creditsOperationType">���ֲ�������,�緢����</param>
		/// <param name="pos">�ӻ����־(����Ϊ��,����Ϊ��,ͨ��������1����-1)</param>
		private static int UpdateUserCredits(int[] uidlist, int[] mountlist, CreditsOperationType creditsOperationType, int pos)
		{
			///���ݹ�ʽ�����û����ܻ���,������
			int reval = 0;
			for (int i = 0; i < uidlist.Length; i++)
			{
				if (uidlist[i] > 0)
				{
					reval = reval + UpdateUserCredits(uidlist[i], mountlist[i], creditsOperationType, pos, true);
				}
			}
				
			return reval;
		}
		
		
		/// <summary>
		/// ���ݻ��ֻ�û����û�����Ӧ��ƥ����û������� (���û��ƥ������û��ǻ����û����򷵻�null)
		/// </summary>
		/// <param name="Credits">����</param>
		/// <returns>�û�������</returns>
		public static UserGroupInfo GetCreditsUserGroupID(float Credits)
		{
			UserGroupInfo[] usergroupinfo = UserGroups.GetUserGroupList();
			UserGroupInfo tmpitem = null;

			foreach (UserGroupInfo infoitem in usergroupinfo)
			{
				// �����û����������radminid����0
				if (infoitem.Radminid == 0 && infoitem.System == 0 && (Credits >= infoitem.Creditshigher && Credits <= infoitem.Creditslower))
				{
					if (tmpitem == null || infoitem.Creditshigher > tmpitem.Creditshigher)
					{
						tmpitem = infoitem;
					}
				}
			}

			return tmpitem == null ? new UserGroupInfo() : tmpitem;
		}



		/// <summary>
		/// �û���������ʱ�����û��Ļ���
		/// </summary>
		/// <param name="uid">�û�id</param>
		public static int UpdateUserCreditsByPostTopic(int uid)
		{
			return UpdateUserCredits(uid, CreditsOperationType.PostTopic, 1);

		}

		/// <summary>
		/// �û���������ʱ�����û��Ļ���
		/// </summary>
		/// <param name="uid">�û�id</param>
		/// <param name="values">���ֱ䶯ֵ,Ӧ��֤��һ������Ϊ8������,��Ӧ8����չ���ֵı䶯ֵ</param>
		public static int UpdateUserCreditsByPostTopic(int uid,float[] values)
		{
			return UpdateUserCredits(uid, values);

		}

		/// <summary>
		/// �û�����ظ�ʱ�����û��Ļ���
		/// </summary>
		/// <param name="uid">�û�id</param>
		public static int UpdateUserCreditsByPosts(int uid)
		{
			return UpdateUserCredits(uid, CreditsOperationType.PostReply, 1);
		}

		/// <summary>
		/// �û�����ظ�ʱ�����û��Ļ���
		/// </summary>
		/// <param name="uid">�û�id</param>
		/// <param name="values">�Զ������ֵ�б�</param>
		public static int UpdateUserCreditsByPosts(int uid,float[] values)
		{
			return UpdateUserCredits(uid, values);
		}

		
		/// <summary>
		/// �û�����ظ�ʱ�����û��Ļ���
		/// </summary>
		/// <param name="uid">�û�id</param>
		public static int UpdateUserCreditsByPosts(int uid,int pos)
		{
			return UpdateUserCredits(uid, CreditsOperationType.PostReply, pos);
		}

		/// <summary>
		/// �û����������������ӱ���Ϊ����ʱ�����û��Ļ���
		/// </summary>
		/// <param name="uid">�û�id</param>
		/// <param name="mount">����Ϊ�������������������</param>
		public static int UpdateUserCreditsByDigest(int uid, int mount)
		{
			return UpdateUserCredits(uid, mount, CreditsOperationType.Digest, 1, true);
		}

		/// <summary>
		/// �û����������������ӱ���Ϊ����ʱ�����û��Ļ���
		/// </summary>
		/// <param name="uidlist">�û�id�б�</param>
		/// <param name="mount">����Ϊ�������������������</param>
		public static int UpdateUserCreditsByDigest(string uidlist, int mount)
		{
			if (!uidlist.Equals(""))
			{
				if (Utils.IsNumericArray(uidlist.Split(',')))
				{
					return UpdateUserCredits(uidlist, CreditsOperationType.Digest, 1);
				}
			}
			return 0;
		}

		
		/// <summary>
		/// �û����������������ӱ���Ϊ����ʱ�����û��Ļ���
		/// </summary>
		/// <param name="uid">�û�id</param>
		public static int UpdateUserCreditsByDigest(int uid)
		{
			return UpdateUserCreditsByUploadAttachment(uid, 1);
		}

		/// <summary>
		/// �û��ϴ�����ʱ�����û��Ļ���
		/// </summary>
		/// <param name="uid">�û�id</param>
		/// <param name="mount">�ϴ���������</param>
		public static int UpdateUserCreditsByUploadAttachment(int uid, int mount)
		{
			return UpdateUserCredits(uid, mount, CreditsOperationType.UploadAttachment, 1);
		}

		/// <summary>
		/// �û��ϴ�����ʱ�����û��Ļ���
		/// </summary>
		/// <param name="uid">�û�id</param>
		public static int UpdateUserCreditsByUploadAttachment(int uid)
		{
			return UpdateUserCreditsByUploadAttachment(uid, 1);
		}


		/// <summary>
		/// �û����ظ���ʱ�����û��Ļ���
		/// </summary>
		/// <param name="uid">�û�id</param>
		/// <param name="mount">���ظ�������</param>
		public static int UpdateUserCreditsByDownloadAttachment(int uid, int mount)
		{
			return UpdateUserCredits(uid, mount, CreditsOperationType.DownloadAttachment, -1);
		}

		/// <summary>
		/// �û����ظ���ʱ�����û��Ļ���
		/// </summary>
		/// <param name="uid">�û�id</param>
		public static int UpdateUserCreditsByDownloadAttachment(int uid)
		{
			return UpdateUserCreditsByDownloadAttachment(uid, 1);
		}
		

		/// <summary>
		/// �û����Ͷ���Ϣʱ�����û��Ļ���
		/// </summary>
		/// <param name="uid">�û�id</param>
		public static int UpdateUserCreditsBySendpms(int uid)
		{
			return UpdateUserCredits(uid, CreditsOperationType.SendMessage, 1);
		}


		/// <summary>
		/// �û�����ʱ�����û��Ļ���
		/// </summary>
		/// <param name="uid">�û�id</param>
		public static int UpdateUserCreditsBySearch(int uid)
		{
			return UpdateUserCredits(uid, CreditsOperationType.Search, -1);
		}



		/// <summary>
		/// �û����׳ɹ�ʱ�����û��Ļ���
		/// </summary>
		/// <param name="uid">�û�id</param>
		public static int UpdateUserCreditsByTradefinished(int uid)
		{
			return UpdateUserCredits(uid, CreditsOperationType.TradeSucceed, 1);

		}

		/// <summary>
		/// �û�����ͶƱʱ�����û��Ļ���
		/// </summary>
		/// <param name="uid">�û�id</param>
		public static int UpdateUserCreditsByVotepoll(int uid)
		{
			return UpdateUserCredits(uid, CreditsOperationType.Vote, 1);

		}

		/// <summary>
		/// �û�����������ʱ�����û��Ļ���
		/// </summary>
		/// <param name="useridlist">�û�id</param>
		public static int UpdateUserCreditsByRate(string useridlist,float[] extcreditslist)
		{
			return UpdateUserCredits(useridlist, extcreditslist);

		}

		/// <summary>
		/// ����ɾ����̳����
		/// </summary>
		/// <param name="tuidlist">Ҫɾ���������û�id</param>
		/// <param name="auidlist">Ҫɾ���������Ӧ�ĵĸ�������,Ӧ��tuidlist������ͬ</param>
		public static int UpdateUserCreditsByDeleteTopic(int[] tuidlist,int[] auidlist,int pos)
		{
			return UpdateUserCredits(tuidlist, CreditsOperationType.PostTopic, pos) + UpdateUserCredits(tuidlist, auidlist, CreditsOperationType.UploadAttachment, pos);
		}

		/// <summary>
		/// �����û�Id��ȡ�û�����
		/// </summary>
		/// <param name="uid">�û�id</param>
		/// <returns>�û�����</returns>
		public static int GetUserCreditsByUid(int uid)
		{
			///���ݹ�ʽ�����û����ܻ���,������
			string ExpressionStr = GetCreditsArithmetic(uid);
            return Utils.StrToInt(Math.Floor(Utils.StrToFloat(Arithmetic.ComputeExpression(ExpressionStr), 0)), 0);
		}
    } // end class
}
