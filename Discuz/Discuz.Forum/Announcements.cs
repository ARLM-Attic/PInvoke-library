using System;
using System.Data;
using System.Data.Common;

using Discuz.Common;
using Discuz.Data;
using Discuz.Entity;

namespace Discuz.Forum
{
	/// <summary>
	/// ��̳���������
	/// </summary>
	public class Announcements
	{
		/// <summary>
		/// ���ȫ��ָ��ʱ����ڵĹ����б�
		/// </summary>
		/// <param name="starttime">��ʼʱ��</param>
		/// <param name="endtime">����ʱ��</param>
		/// <returns>�����б�</returns>
		public static DataTable GetAnnouncementList(string starttime, string endtime)
		{
			Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
			DataTable dt = cache.RetrieveObject("/Forum/AnnouncementList") as DataTable;
			
			if(dt == null)
			{
                dt = DatabaseProvider.GetInstance().GetAnnouncementList(starttime, endtime);
                cache.AddObject("/Forum/AnnouncementList", dt);
			}
			return dt;
		}

		/// <summary>
		/// ���ȫ��ָ��ʱ����ڵĵ�һ�������б�
		/// </summary>
		/// <param name="starttime">��ʼʱ��</param>
		/// <param name="endtime">����ʱ��</param>
		/// <returns>�����б�</returns>
		public static DataTable GetSimplifiedAnnouncementList(string starttime, string endtime)
		{
			return GetSimplifiedAnnouncementList(starttime, endtime, -1);
		}

		/// <summary>
		/// ���ȫ��ָ��ʱ����ڵ�ǰn�������б�
		/// </summary>
		/// <param name="starttime">��ʼʱ��</param>
		/// <param name="endtime">����ʱ��</param>
		/// <param name="maxcount">����¼��,С��0����ȫ��</param>
		/// <returns>�����б�</returns>
		public static DataTable GetSimplifiedAnnouncementList(string starttime, string endtime, int maxcount)
		{
			Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
			DataTable dt = cache.RetrieveObject("/Forum/SimplifiedAnnouncementList") as DataTable;
			
			if(dt == null)
			{
                dt = DatabaseProvider.GetInstance().GetSimplifiedAnnouncementList(starttime, endtime, maxcount);
				cache.AddObject("/Forum/SimplifiedAnnouncementList", dt);
			}
			return dt;
		}

	}
}
