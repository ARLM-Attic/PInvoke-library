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
	/// ��̳ͳ����
	/// </summary>
	public class Statistics
	{
		/// <summary>
		/// ���ͳ����
		/// </summary>
		/// <returns>ͳ����</returns>
		public static DataRow GetStatisticsRow()
		{
			Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
			DataRow dr = cache.RetrieveObject("/Forum/Statistics") as DataRow;
			if(dr == null)
			{
                dr = DatabaseProvider.GetInstance().GetStatisticsRow();
				cache.AddObject("/Forum/Statistics", dr);
			}
			return dr;
		}

		/// <summary>
		/// �������е�ͳ���б��浽���ݿ�
		/// </summary>
		public static void SaveStatisticsRow()
		{
            DatabaseProvider.GetInstance().SaveStatisticsRow(GetStatisticsRow());	
        }


		/// <summary>
		/// ��ȡָ������е���������ͳ������
		/// </summary>
		/// <param name="fid"></param>
		/// <param name="topiccount"></param>
		/// <param name="postcount"></param>
		/// <param name="todaypostcount"></param>
		public static void GetPostCountFromForum(int fid, out int topiccount,out int postcount, out int todaypostcount)
		{
			topiccount = 0;
			postcount = 0;
			todaypostcount = 0;
			IDataReader  reader = null;
			if (fid == 0)
			{
                reader = DatabaseProvider.GetInstance().GetAllForumStatistics();
			}
			else
			{
                reader = DatabaseProvider.GetInstance().GetForumStatistics(fid);
			}

			while (reader.Read())
			{
				topiccount = Utils.StrToInt(reader["topiccount"],0);
				postcount = Utils.StrToInt(reader["postcount"],0);
				todaypostcount = Utils.StrToInt(reader["todaypostcount"],0);
			}
			reader.Close();

		}



		/// <summary>
		/// ���ָ�����Ƶ�ͳ����
		/// </summary>
		/// <param name="param">��</param>
		/// <returns>ͳ��ֵ</returns>
		public static string GetStatisticsRowItem(string param)
		{
			return GetStatisticsRow()[param].ToString();
		}


		/// <summary>
		/// �õ���һ��ִ������������ʱ��
		/// </summary>
		/// <returns></returns>
		public static string GetStatisticsSearchtime()
		{
			Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
			string searchtime = cache.RetrieveObject("/Forum/StatisticsSearchtime") as string;
			if (searchtime == null)
			{
				searchtime = DateTime.Now.ToString();
				cache.AddObject("/Forum/StatisticsSearchtime", searchtime);
			}
			return searchtime;
		}

		/// <summary>
		/// �õ��û���һ�����������Ĵ�����
		/// </summary>
		/// <returns></returns>
		public static int GetStatisticsSearchcount()
		{
			Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
			int searchcount = Utils.StrToInt(cache.RetrieveObject("/Forum/StatisticsSearchcount"),0);
			if (searchcount == 0)
			{
				searchcount = 1;
				cache.AddObject("/Forum/StatisticsSearchcount", searchcount);
			}
			return Utils.StrToInt(searchcount,1);
		}


		/// <summary>
		/// ���������û���һ��ִ������������ʱ��
		/// </summary>
		/// <param name="searchtime">����ʱ��</param>
		public static void SetStatisticsSearchtime(string searchtime)
		{
			Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
			cache.RemoveObject("/Forum/StatisticsSearchtime");
			cache.AddObject("/Forum/StatisticsSearchtime", searchtime);
		}

		/// <summary>
		/// �����û���һ�����������Ĵ���Ϊ��ʼֵ��
		/// </summary>
		/// <param name="searchcount">��ʼֵ</param>
		public static void SetStatisticsSearchcount(int searchcount)
		{
			Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
			cache.RemoveObject("/Forum/StatisticsSearchcount");
			cache.AddObject("/Forum/StatisticsSearchcount", searchcount);
		}

		/// <summary>
		/// ����ָ�����Ƶ�ͳ����
		/// </summary>
		/// <param name="param">��Ŀ����</param>
		/// <param name="Value">ָ�����ֵ</param>
		/// <returns>ͳ����</returns>
		public static void SetStatisticsRowItem(string param,int Value)
		{
			GetStatisticsRow()[param] = Value;
		}

		/// <summary>
		/// ����ָ�����Ƶ�ͳ����
		/// </summary>
		/// <param name="param">��Ŀ����</param>
		/// <param name="Value">ָ�����ֵ</param>
		/// <returns>ͳ����</returns>
		public static void SetStatisticsRowItem(string param,string Value)
		{
			GetStatisticsRow()[param] = Value;
		}

		/// <summary>
		/// ����ָ�����Ƶ�ͳ����
		/// </summary>
		/// <param name="param">��Ŀ����</param>
		/// <param name="Value">ָ�����ֵ</param>
		/// <returns>ͳ����</returns>
		public static void SetStatisticsRowItem(string param,DateTime Value)
		{
			GetStatisticsRow()[param] = Value;
		}



		/// <summary>
		/// ����ָ�����Ƶ�ͳ����
		/// </summary>
		/// <param name="param">��Ŀ����</param>
		/// <param name="Value">ָ�����ֵ</param>
		/// <returns>������</returns>
		public static int UpdateStatistics(string param,int intValue)
		{
            return DatabaseProvider.GetInstance().UpdateStatistics(param, intValue);
		}

		/// <summary>
		/// ����ָ�����Ƶ�ͳ����
		/// </summary>
		/// <param name="param">��Ŀ����</param>
		/// <param name="Value">ָ�����ֵ</param>
		/// <returns>������</returns>
		public static int UpdateStatistics(string param,string strValue)
		{
            return DatabaseProvider.GetInstance().UpdateStatistics(param, strValue);
		}



		/// <summary>
		/// ��鲢����60����ͳ�Ƶ�����
		/// </summary>
		/// <param name="maxspm">60��������������������</param>
		/// <returns>û�г������������������true,���򷵻�false</returns>
		public static bool CheckSearchCount(int maxspm)
		{
			if (maxspm == 0)
				return true;
            string searchtime = GetStatisticsSearchtime();
			int searchcount = GetStatisticsSearchcount();
			if (Utils.StrDateDiffSeconds(searchtime,60) > 0)
			{
				SetStatisticsSearchtime(DateTime.Now.ToString());
				SetStatisticsSearchcount(1);
			}
			
			if (searchcount > maxspm)
			{
				return false;
			}

			SetStatisticsSearchcount(searchcount + 1);
			return true;			

		}

		/// <summary>
		/// �ؽ�ͳ�ƻ���
		/// </summary>
		public static void ReSetStatisticsCache()
		{
			Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            DataRow dr = DatabaseProvider.GetInstance().GetStatisticsRow();
			cache.RemoveObject("/Forum/Statistics");
			cache.AddObject("/Forum/Statistics", dr);
		}
	}
}
