using System;
using System.Data;
using System.Data.Common;
using Discuz.Config;
using Discuz.Data;
using Discuz.Entity;

namespace Discuz.Forum
{
	/// <summary>
	/// AdminModeratorManageLogFactory ��ժҪ˵����
	/// ѫ����־����������
	/// </summary>
	public class AdminMedalLogs
	{
		public AdminMedalLogs()
		{
		}

		/// <summary>
		/// ɾ����־
		/// </summary>
		/// <returns></returns>
		public static bool DeleteLog()
		{
            return DatabaseProvider.GetInstance().DeleteMedalLog();
		}


		/// <summary>
		/// ��ָ������ɾ����־
		/// </summary>
		/// <param name="condition">��ѯ����</param>
		/// <returns></returns>
		public static bool DeleteLog(string condition)
		{
            return DatabaseProvider.GetInstance().DeleteMedalLog(condition);
		}


		/// <summary>
		/// �õ���ǰָ��ҳ����ѫ����־��¼(��)
		/// </summary>
		/// <param name="pagesize">��ǰ��ҳ�ĳߴ��С</param>
		/// <param name="currentpage">��ǰҳ��</param>
		/// <returns></returns>
		public static DataTable LogList(int pagesize, int currentpage)
		{
            return DatabaseProvider.GetInstance().GetMedalLogList(pagesize, currentpage);
		}


		/// <summary>
		/// �õ���ǰָ��������ҳ����ѫ����־��¼(��)
		/// </summary>
		/// <param name="pagesize">��ǰ��ҳ�ĳߴ��С</param>
		/// <param name="currentpage">��ǰҳ��</param>
		/// <param name="condition">��ѯ����</param>
		/// <returns></returns>
		public static DataTable LogList(int pagesize, int currentpage, string condition)
		{
            return DatabaseProvider.GetInstance().GetMedalLogList(pagesize, currentpage, condition);
		}


		/// <summary>
		/// �õ�������־��¼��
		/// </summary>
		/// <returns></returns>
		public static int RecordCount()
		{
            return DatabaseProvider.GetInstance().GetMedalLogListCount();
		}


		/// <summary>
		/// �õ�ָ����ѯ�����µ�ѫ����־��
		/// </summary>
		/// <param name="condition">��ѯ����</param>
		/// <returns></returns>
		public static int RecordCount(string condition)
		{
            return DatabaseProvider.GetInstance().GetMedalLogListCount(condition);
		}
	}
}