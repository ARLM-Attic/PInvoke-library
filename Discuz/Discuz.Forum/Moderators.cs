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
	/// ����������
	/// </summary>
	public class Moderators
	{
		/// <summary>
		/// ������а�����Ϣ
		/// </summary>
		/// <returns>���а�����Ϣ</returns>
		public static ModeratorInfo[] GetModeratorList()
		{
			Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
			ModeratorInfo[] infoArray = cache.RetrieveObject("/Forum/ModeratorList") as ModeratorInfo[];
			if(infoArray == null)
			{
                DataTable dt = DatabaseProvider.GetInstance().GetModeratorList();
				infoArray = new ModeratorInfo[dt.Rows.Count];
				ModeratorInfo info;
				// idΪ����
				int id = 0;
				foreach(DataRow dr in dt.Rows)
				{
					info = new ModeratorInfo();
					info.Uid = Int32.Parse(dr["uid"].ToString());
					info.Fid = Int16.Parse(dr["fid"].ToString());
					info.Displayorder = Int16.Parse(dr["Displayorder"].ToString());
					info.Inherited = Int16.Parse(dr["inherited"].ToString());
					infoArray[id] = info;
					id++;
				}

				cache.AddObject("/Forum/ModeratorList", infoArray);
			}
			return infoArray;
		}


		/// <summary>
		/// �ж�ָ���û��Ƿ���ָ�����İ���
		/// </summary>
		/// <param name="admingid">������id</param>
		/// <param name="uid">�û�id</param>
		/// <param name="fid">��̳id</param>
		/// <returns>����ǰ�������true, ��������򷵻�false</returns>
		public static bool IsModer(int admingid, int uid, int fid)
		{
			if (admingid == 0)
			{
				return false;
			}
			// �û�Ϊ����Ա���ܰ���ֱ�ӷ�����
			if (admingid == 1 || admingid == 2)
			{
				return true;
			}
			if (admingid == 3)
			{

				// ����ǹ���Ա���ܰ���, ��������ͨ�������ڸð���а���Ȩ��
				ModeratorInfo[] infosinfoArray = GetModeratorList();
				foreach(ModeratorInfo moder in infosinfoArray)
				{
					// ��̳�������д���,�򷵻���
					if (moder.Uid == uid && moder.Fid == fid)
					{
						return true;
					}
				}
			}
			return false;
		}

	}
}
