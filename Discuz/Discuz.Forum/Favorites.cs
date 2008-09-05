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
	/// �ղؼв�����
	/// </summary>
	public class Favorites
	{

		/// <summary>
		/// �����ղ���Ϣ
		/// </summary>
		/// <param name="uid">�û�ID</param>
		/// <param name="tid">����ID</param>
		/// <returns>�����ɹ����� 1 ���򷵻� 0</returns>	
		public static int CreateFavorites(int uid,int tid)
		{
            return DatabaseProvider.GetInstance().CreateFavorites(uid, tid);
		}

        /// <summary>
        /// �����ղ���Ϣ
        /// </summary>
        /// <param name="uid">�û�ID</param>
        /// <param name="tid">����ID</param>
        /// <param name="type">�ղ�����</param>
        /// <returns>�����ɹ����� 1 ���򷵻� 0</returns>	
        public static int CreateFavorites(int uid, int tid, FavoriteType type)
        {
            return DatabaseProvider.GetInstance().CreateFavorites(uid, tid, (byte)type);
        }
	
		/// <summary>
		/// ɾ��ָ���û����ղ���Ϣ
		/// </summary>
		/// <param name="uid">�û�id</param>
		/// <param name="fitemid">Ҫɾ�����ղ���Ϣid�б�</param>
		/// <returns>ɾ��������������ʱ���� -1</returns>
        public static int DeleteFavorites(int uid, string[] fitemid, FavoriteType type)
		{
			foreach (string id in fitemid)
			{
				if (!Utils.IsNumeric(id))
				{
					return -1;
				}
			}

			string fidlist = String.Join(",",fitemid);

            return DatabaseProvider.GetInstance().DeleteFavorites(uid, fidlist, (byte)type);
		}

		/// <summary>
		/// �õ��û��ղ���Ϣ�б�
		/// </summary>
		/// <param name="uid">�û�id</param>
		/// <param name="pagesize">��ҳʱÿҳ�ļ�¼��</param>
		/// <param name="pageindex">��ǰҳ��</param>
		/// <returns>�û���Ϣ�б�</returns>
        public static DataTable GetFavoritesList(int uid, int pagesize, int pageindex)
        {
            return DatabaseProvider.GetInstance().GetFavoritesList(uid, pagesize, pageindex);
        }
        
		/// <summary>
		/// �õ��û��ղ���Ϣ�б�
		/// </summary>
		/// <param name="uid">�û�id</param>
		/// <param name="pagesize">��ҳʱÿҳ�ļ�¼��</param>
		/// <param name="pageindex">��ǰҳ��</param>
		/// <param name="type">�ղ�����id</param>
		/// <returns>�û���Ϣ�б�</returns>
        public static DataTable GetFavoritesList(int uid, int pagesize, int pageindex, FavoriteType type)
		{
            return DatabaseProvider.GetInstance().GetFavoritesList(uid, pagesize, pageindex, (int)type);
		}

		/// <summary>
		/// �õ��û��ղص�����
		/// </summary>
		/// <param name="uid">�û�id</param>
		/// <returns>�ղ�����</returns>
		public static int GetFavoritesCount(int uid)
		{
            return DatabaseProvider.GetInstance().GetFavoritesCount(uid);	
        }


        /// <summary>
        /// �õ��û����������ղص�����
        /// </summary>
        /// <param name="uid">�û�id</param>
        /// <returns>�ղ�����</returns>
        public static int GetFavoritesCount(int uid, FavoriteType type)
        {
            return DatabaseProvider.GetInstance().GetFavoritesCount(uid, (int)type);
        }

        /// <summary>
        /// �ղؼ����Ƿ������ָ��������
        /// </summary>
        /// <param name="uid">�û�Id</param>
        /// <param name="tid">����Id</param>
        /// <returns></returns>
        public static int CheckFavoritesIsIN(int uid, int tid)
        {
            return CheckFavoritesIsIN(uid, tid, FavoriteType.ForumTopic);
        }

		/// <summary>
		/// �ղؼ����Ƿ������ָ������
		/// </summary>
		/// <param name="uid">�û�id</param>
		/// <param name="tid">��Id</param>
        /// <param name="type">����: ���, ��־, ����</param>
		/// <returns></returns>
		public static int CheckFavoritesIsIN(int uid,int tid, FavoriteType type)
		{
            return DatabaseProvider.GetInstance().CheckFavoritesIsIN(uid, tid, (byte)type);	
        }
	}//class end
}
