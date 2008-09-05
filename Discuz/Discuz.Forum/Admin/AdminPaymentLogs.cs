using System;
using System.Data;
using System.Data.Common;

using Discuz.Config;
using Discuz.Data;
using Discuz.Entity;

namespace Discuz.Forum
{
	/// <summary>
	/// AdminPaymentLogFactory ��ժҪ˵����
	/// ���ֽ�����־���������
	/// </summary>
	public class AdminPaymentLogs
	{
		public AdminPaymentLogs()
		{
		}

		
		/// <summary>
		/// ɾ����־
		/// </summary>
		/// <returns></returns>
		public static bool DeleteLog()
		{
            return DatabaseProvider.GetInstance().DeletePaymentLog();
		}



		/// <summary>
		/// ��ָ������ɾ����־
		/// </summary>
		/// <param name="condition">��ѯ����</param>
		/// <returns></returns>
		public static bool DeleteLog(string condition)
		{
            return DatabaseProvider.GetInstance().DeletePaymentLog(condition);
		}



		/// <summary>
		/// �õ���ǰָ��ҳ���Ļ��ֽ�����־��¼(��)
		/// </summary>
		/// <param name="pagesize">��ǰ��ҳ�ĳߴ��С</param>
		/// <param name="currentpage">��ǰҳ��</param>
		/// <returns></returns>
		public static DataTable LogList(int pagesize,int currentpage)
		{
            DataTable dt = DatabaseProvider.GetInstance().GetPaymentLogList(pagesize, currentpage);
			if (dt!=null)
			{

				DataColumn dc=new DataColumn();
				dc.ColumnName="forumname";
				dc.DataType=System.Type.GetType("System.String");
				dc.DefaultValue="";
				dc.AllowDBNull=false;
				dt.Columns.Add(dc);
                DataTable ForumList = DatabaseProvider.GetInstance().GetForumList();
				foreach(DataRow dr in dt.Rows)
				{
					if(dr["fid"].ToString().Trim()!="")
					{
						foreach(DataRow forumdr in ForumList.Select("fid="+dr["fid"].ToString()))
						{
							dr["forumname"]=forumdr["name"].ToString();
							break;
						}
					}
				}
			}
			return dt;
		}

		

		/// <summary>
		/// �õ���ǰָ��������ҳ���Ļ��ֽ�����־��¼(��)
		/// </summary>
		/// <param name="pagesize">��ǰ��ҳ�ĳߴ��С</param>
		/// <param name="currentpage">��ǰҳ��</param>
		/// <param name="condition">��ѯ����</param>
		/// <returns></returns>
		public static DataTable LogList(int pagesize,int currentpage , string condition)
		{
            DataTable dt = DatabaseProvider.GetInstance().GetPaymentLogList(pagesize, currentpage, condition);
			if (dt!=null)
			{

				DataColumn dc=new DataColumn();
				dc.ColumnName="forumname";
				dc.DataType=System.Type.GetType("System.String");
				dc.DefaultValue="";
				dc.AllowDBNull=false;
				dt.Columns.Add(dc);
                DataTable ForumList = DatabaseProvider.GetInstance().GetForumList();
				foreach(DataRow dr in dt.Rows)
				{
					if(dr["fid"].ToString().Trim()!="")
					{
						foreach(DataRow forumdr in ForumList.Select("fid="+dr["fid"].ToString()))
						{
							dr["forumname"]=forumdr["name"].ToString();
							break;
						}
					}
				}
			}
			return dt;
		}



		/// <summary>
		/// �õ����ֽ�����־��¼��
		/// </summary>
		/// <returns></returns>
		public static int RecordCount()
		{
            return DatabaseProvider.GetInstance().GetPaymentLogListCount();
		}


		/// <summary>
		/// �õ�ָ����ѯ�����µĻ��ֽ�����־��
		/// </summary>
		/// <param name="condition">��ѯ����</param>
		/// <returns></returns>
		public static int RecordCount(string condition)
		{
            return DatabaseProvider.GetInstance().GetPaymentLogListCount(condition);
		}
	}
}
