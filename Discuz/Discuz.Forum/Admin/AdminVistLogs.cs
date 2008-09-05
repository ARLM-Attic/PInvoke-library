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
	/// AdminVistLogFactory ��ժҪ˵����
	/// ��̨������־���������
	/// </summary>
	public class AdminVistLogs
	{
		public AdminVistLogs()
		{
		}


		/// <summary>
		/// �������������־��¼
		/// </summary>
		/// <param name="uid">�û�UID</param>
		/// <param name="username">�û���</param>
		/// <param name="groupid">������ID</param>
		/// <param name="grouptitle">����������</param>
		/// <param name="ip">IP��ַ</param>
		/// <param name="actions">����</param>
		/// <param name="others"></param>
		/// <returns></returns>
		public static bool InsertLog(int uid, string username, int groupid, string grouptitle, string ip, string actions, string others)
		{
			try
			{
                DatabaseProvider.GetInstance().AddVisitLog(uid, username, groupid, grouptitle, ip, actions, others);
				return true;
			}
			catch
			{
				return false;
			}
		}


		/// <summary>
		/// ɾ����־
		/// </summary>
		/// <returns></returns>
		public static bool DeleteLog()
		{
			try
			{
                DatabaseProvider.GetInstance().DeleteVisitLogs();
				return true;
			}
			catch
			{
				return false;
			}
		}


		/// <summary>
		/// ��ָ������ɾ����־
		/// </summary>
		/// <param name="condition">��ѯ����</param>
		/// <returns></returns>
		public static bool DeleteLog(string condition)
		{
			try
			{
                DatabaseProvider.GetInstance().DeleteVisitLogs(condition);
				return true;
			}
			catch
			{
				return false;
			}
		}


		/// <summary>
		/// �õ���ǰָ��ҳ���ĺ�̨������־��¼(��)
		/// </summary>
		/// <param name="pagesize">��ǰ��ҳ�ĳߴ��С</param>
		/// <param name="currentpage">��ǰҳ��</param>
		/// <returns></returns>
		public static DataTable LogList(int pagesize, int currentpage)
		{
            return DatabaseProvider.GetInstance().GetVisitLogList(pagesize, currentpage);
		}


		/// <summary>
		/// �õ���ǰָ��������ҳ���ĺ�̨������־��¼(��)
		/// </summary>
		/// <param name="pagesize">��ǰ��ҳ�ĳߴ��С</param>
		/// <param name="currentpage">��ǰҳ��</param>
		/// <param name="condition">��ѯ����</param>
		/// <returns></returns>
		public static DataTable LogList(int pagesize, int currentpage, string condition)
		{
            return DatabaseProvider.GetInstance().GetVisitLogList(pagesize, currentpage, condition);
		}


		/// <summary>
		/// �õ���̨������־��¼��
		/// </summary>
		/// <returns></returns>
		public static int RecordCount()
		{
            return DatabaseProvider.GetInstance().GetVisitLogCount();
		}


		/// <summary>
		/// �õ�ָ����ѯ�����µĺ�̨������־��¼��
		/// </summary>
		/// <param name="condition">��ѯ����</param>
		/// <returns></returns>
		public static int RecordCount(string condition)
		{
            return DatabaseProvider.GetInstance().GetVisitLogCount(condition);
        }


	}
}