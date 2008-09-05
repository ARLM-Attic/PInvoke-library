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
	/// ����Ϣ������
	/// </summary>
	public class PrivateMessages
	{
		/// <summary>
		/// ���������û�ע�Ỷӭ�ż����û�����, ������ͬʱ�������û�ע��
		/// </summary>
		public const string SystemUserName = "ϵͳ";
		
		
		/// <summary>
		/// ���ָ��ID�Ķ���Ϣ������
		/// </summary>
		/// <param name="pmid">����Ϣpmid</param>
		/// <returns>����Ϣ����</returns>
		public static PrivateMessageInfo GetPrivateMessageInfo(int pmid)
		{
			PrivateMessageInfo __privatemessageinfo = new PrivateMessageInfo();

            IDataReader reader = DatabaseProvider.GetInstance().GetPrivateMessageInfo(pmid);
			if(reader.Read())
			{
				__privatemessageinfo.Pmid = pmid;
				__privatemessageinfo.Msgfrom = reader["msgfrom"].ToString();
				__privatemessageinfo.Msgfromid = int.Parse(reader["msgfromid"].ToString());
				__privatemessageinfo.Msgto = reader["msgto"].ToString();
				__privatemessageinfo.Msgtoid = int.Parse(reader["msgtoid"].ToString());
				__privatemessageinfo.Folder = Int16.Parse(reader["folder"].ToString());
				__privatemessageinfo.New = int.Parse(reader["new"].ToString());
				__privatemessageinfo.Subject = reader["subject"].ToString();
				__privatemessageinfo.Postdatetime = reader["postdatetime"].ToString();
				__privatemessageinfo.Message = reader["message"].ToString();
				reader.Close();
				return __privatemessageinfo;
			}
			reader.Close();
			return null;

		}


	

		/// <summary>
		/// �õ����û��Ķ���Ϣ����
		/// </summary>
		/// <param name="userid">�û�ID</param>
		/// <param name="folder">�����ļ���(0:�ռ���,1:������,2:�ݸ���)</param>
		/// <returns>����Ϣ����</returns>
		public static int GetPrivateMessageCount(int userid, int folder)
		{
			return GetPrivateMessageCount(userid,folder,-1);
		}

		/// <summary>
		/// �õ����û��Ķ���Ϣ����
		/// </summary>
		/// <param name="userid">�û�ID</param>
		/// <param name="folder">�����ļ���(0:�ռ���,1:������,2:�ݸ���)</param>
		/// <param name="state">����Ϣ״̬(0:�Ѷ�����Ϣ��1:δ������Ϣ��-1:ȫ������Ϣ)</param>
		/// <returns>����Ϣ����</returns>
		public static int GetPrivateMessageCount(int userid, int folder,int state)
		{
            return DatabaseProvider.GetInstance().GetPrivateMessageCount(userid, folder, state);
		}

		/// <summary>
		/// ��������Ϣ
		/// </summary>
		/// <param name="__privatemessageinfo">����Ϣ����</param>
		/// <param name="savetosentbox">���ö���Ϣ�Ƿ��ڷ����䱣��(0Ϊ������, 1Ϊ����)</param>
		/// <returns>����Ϣ�����ݿ��е�pmid</returns>
		public static int CreatePrivateMessage(PrivateMessageInfo __privatemessageinfo, int savetosentbox)
		{
            return DatabaseProvider.GetInstance().CreatePrivateMessage(__privatemessageinfo, savetosentbox);    
        }


		/// <summary>
		/// ɾ��ָ���û��Ķ���Ϣ
		/// </summary>
		/// <param name="userid">�û�ID</param>
		/// <param name="pmitemid">Ҫɾ���Ķ���Ϣ�б�(����)</param>
		/// <returns>ɾ����¼��</returns>
		public static int DeletePrivateMessage(int userid, string[] pmitemid)
		{
			foreach (string id in pmitemid)
			{
				if (!Utils.IsNumeric(id))
				{
					return -1;
				}
			}

			string pmidlist = String.Join(",",pmitemid);

            int reval = DatabaseProvider.GetInstance().DeletePrivateMessages(userid, pmidlist);
            if (reval > 0)
			{
                int newpmcount = DatabaseProvider.GetInstance().GetNewPMCount(userid);
				Users.SetUserNewPMCount(userid,newpmcount);
			}

			return reval;

		}

		/// <summary>
		/// ɾ��ָ���û���һ������Ϣ
		/// </summary>
		/// <param name="userid">�û�ID</param>
		/// <param name="pmid">Ҫɾ���Ķ���ϢID</param>
		/// <returns>ɾ����¼��</returns>
		public static int DeletePrivateMessage(int userid,int pmid)
		{
            int reval = DatabaseProvider.GetInstance().DeletePrivateMessage(userid, pmid);
			if (reval > 0)
			{
                int newpmcount = DatabaseProvider.GetInstance().GetNewPMCount(userid);
				Users.SetUserNewPMCount(userid,newpmcount);
			}

			return reval;

		}

		/// <summary>
		/// ���ö���Ϣ״̬
		/// </summary>
		/// <param name="pmid">����ϢID</param>
		/// <param name="state">״ֵ̬</param>
		/// <returns>���¼�¼��</returns>
		public static int SetPrivateMessageState(int pmid,byte state)
		{
            return DatabaseProvider.GetInstance().SetPrivateMessageState(pmid, state);
        }

#if NET1

        #region ����Ϣ���Ϻ���

        /// <summary>
        /// ���ָ���û��Ķ���Ϣ�б�
        /// </summary>
        /// <param name="userid">�û�ID</param>
        /// <param name="folder">����Ϣ����(0:�ռ���,1:������,2:�ݸ���)</param>
        /// <param name="pagesize">ÿҳ��ʾ����Ϣ��</param>
        /// <param name="pageindex">��ǰҪ��ʾ��ҳ��</param>
        /// <param name="strwhere">ɸѡ����</param>
        /// <returns>����Ϣ�б�</returns>
	    public static PrivateMessageInfoCollection GetPrivateMessageCollection(int userid, int folder, int pagesize, int pageindex, int inttype)
        {
            PrivateMessageInfoCollection coll = new PrivateMessageInfoCollection();
            IDataReader reader = DatabaseProvider.GetInstance().GetPrivateMessageList(userid, folder, pagesize, pageindex, inttype);
            if (reader != null)
            {

                while (reader.Read())
                {
                    PrivateMessageInfo info = new PrivateMessageInfo();
                    info.Pmid = int.Parse(reader["pmid"].ToString());
                    info.Msgfrom = reader["msgfrom"].ToString();
                    info.Msgfromid = int.Parse(reader["msgfromid"].ToString());
                    info.Msgto = reader["msgto"].ToString();
                    info.Msgtoid = int.Parse(reader["msgtoid"].ToString());
                    info.Folder = Int16.Parse(reader["folder"].ToString());
                    info.New = int.Parse(reader["new"].ToString());
                    info.Subject = reader["subject"].ToString();
                    info.Postdatetime = reader["postdatetime"].ToString();
                    info.Message = reader["message"].ToString();
                    coll.Add(info);
                }
                reader.Close();
            }
            return coll;
        }



        /// <summary>
        /// ���ض̱�����ռ������Ϣ�б�
        /// </summary>
        /// <param name="userid">�û�ID</param>
        /// <param name="pagesize">ÿҳ��ʾ����Ϣ��</param>
        /// <param name="pageindex">��ǰҪ��ʾ��ҳ��</param>
        /// <param name="strwhere">ɸѡ����</param>
        /// <returns></returns>
        public static PrivateMessageInfoCollection GetPrivateMessageCollectionForIndex(int userid, int pagesize, int pageindex, int  inttype)
        {
            PrivateMessageInfoCollection coll = GetPrivateMessageCollection(userid, 0, pagesize, pageindex, inttype);
            if (coll.Count > 0)
            {
                for (int i = 0; i < coll.Count; i++)
                {
                    coll[i].Message = Utils.GetSubString(coll[i].Message, 20, "...");
                }

            }
            return coll;
        }

        #endregion


#else

        #region ����Ϣ���ͺ���

        /// <summary>
        /// ���ָ���û��Ķ���Ϣ�б�
        /// </summary>
        /// <param name="userid">�û�ID</param>
        /// <param name="folder">����Ϣ����(0:�ռ���,1:������,2:�ݸ���)</param>
        /// <param name="pagesize">ÿҳ��ʾ����Ϣ��</param>
        /// <param name="pageindex">��ǰҪ��ʾ��ҳ��</param>
        /// <param name="strwhere">ɸѡ����</param>
        /// <returns>����Ϣ�б�</returns>
        public static Discuz.Common.Generic.List<PrivateMessageInfo> GetPrivateMessageCollection(int userid, int folder, int pagesize, int pageindex, int inttype)
        {
            Discuz.Common.Generic.List<PrivateMessageInfo> coll = new Discuz.Common.Generic.List<PrivateMessageInfo>();
            IDataReader reader = DatabaseProvider.GetInstance().GetPrivateMessageList(userid, folder, pagesize, pageindex, inttype);
            if (reader != null)
            {

                while (reader.Read())
                {
                    PrivateMessageInfo info = new PrivateMessageInfo();
                    info.Pmid = int.Parse(reader["pmid"].ToString());
                    info.Msgfrom = reader["msgfrom"].ToString();
                    info.Msgfromid = int.Parse(reader["msgfromid"].ToString());
                    info.Msgto = reader["msgto"].ToString();
                    info.Msgtoid = int.Parse(reader["msgtoid"].ToString());
                    info.Folder = Int16.Parse(reader["folder"].ToString());
                    info.New = int.Parse(reader["new"].ToString());
                    info.Subject = reader["subject"].ToString();
                    info.Postdatetime = reader["postdatetime"].ToString();
                    info.Message = reader["message"].ToString();
                    coll.Add(info);
                }
                reader.Close();
            }
            return coll;
        }



        /// <summary>
        /// ���ض̱�����ռ������Ϣ�б�
        /// </summary>
        /// <param name="userid">�û�ID</param>
        /// <param name="pagesize">ÿҳ��ʾ����Ϣ��</param>
        /// <param name="pageindex">��ǰҪ��ʾ��ҳ��</param>
        /// <param name="strwhere">ɸѡ����</param>
        /// <returns>�ռ������Ϣ�б�</returns>
        public static Discuz.Common.Generic.List<PrivateMessageInfo> GetPrivateMessageCollectionForIndex(int userid, int pagesize, int pageindex, int  inttype)
        {
            Discuz.Common.Generic.List<PrivateMessageInfo> coll = GetPrivateMessageCollection(userid, 0, pagesize, pageindex, inttype);
            if (coll.Count > 0)
            {
                for (int i = 0; i < coll.Count; i++)
                {
                    coll[i].Message = Utils.GetSubString(coll[i].Message, 20, "...");
                }

            }
            return coll;
        }

        #endregion

#endif

    } //class end
}
