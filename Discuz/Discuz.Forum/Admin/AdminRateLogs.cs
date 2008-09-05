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
	/// AdminRateLogFactory ��ժҪ˵����
	/// ��̨������־���������
	/// </summary>
	public class AdminRateLogs
	{

		/// <summary>
		/// ������ּ�¼
		/// </summary>
		/// <param name="postidlist">����������pid</param>
		/// <param name="userid">������uid</param>
		/// <param name="username">�������û���</param>
		/// <param name="extid">�ֵĻ�������</param>
		/// <param name="score">������ֵ</param>
		/// <param name="reason">��������</param>
		/// <returns>������������</returns>
		public static int InsertLog(string postidlist, int userid, string username, int extid, float score, string reason)
		{
            int reval = 0;
            foreach (string pid in Utils.SplitString(postidlist, ","))
            {
                reval += DatabaseProvider.GetInstance().InsertRateLog(Utils.StrToInt(pid, 0), userid, username, extid, score, reason);
            }
            return reval;
		}


		/// <summary>
		/// ɾ����־
		/// </summary>
		/// <returns></returns>
		public static bool DeleteLog()
		{
            return DatabaseProvider.GetInstance().DeleteRateLog();
		}



		/// <summary>
		/// ��ָ������ɾ����־
		/// </summary>
		/// <param name="condition">��ѯ����</param>
		/// <returns></returns>
		public static bool DeleteLog(string condition)
		{
            return DatabaseProvider.GetInstance().DeleteRateLog(condition);

		}



		/// <summary>
		/// �õ���ǰָ��ҳ����������־��¼(��)
		/// </summary>
		/// <param name="pagesize">��ǰ��ҳ�ĳߴ��С</param>
		/// <param name="currentpage">��ǰҳ��</param>
		/// <returns></returns>
		public static DataTable LogList(int pagesize, int currentpage)
		{
            return DatabaseProvider.GetInstance().RateLogList(pagesize, currentpage, Posts.GetPostTableName());
		}



		/// <summary>
		/// �õ���ǰָ��������ҳ����������־��¼(��)
		/// </summary>
		/// <param name="pagesize">��ǰ��ҳ�ĳߴ��С</param>
		/// <param name="currentpage">��ǰҳ��</param>
		/// <param name="condition">��ѯ����</param>
		/// <returns></returns>
		public static DataTable LogList(int pagesize, int currentpage, string condition)
		{
            return DatabaseProvider.GetInstance().RateLogList(pagesize, currentpage, Posts.GetPostTableName(), condition);
		}



		/// <summary>
		/// �õ�������־��¼��
		/// </summary>
		/// <returns></returns>
		public static int RecordCount()
		{
            return DatabaseProvider.GetInstance().GetRateLogCount();
		}



		/// <summary>
		/// �õ�ָ����ѯ�����µ�������־��
		/// </summary>
		/// <param name="condition">��ѯ����</param>
		/// <returns></returns>
		public static int RecordCount(string condition)
		{
            return DatabaseProvider.GetInstance().GetRateLogCount(condition);
		}
    }
}