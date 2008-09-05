using System;
using System.Data;
using System.Data.Common;
using System.Text.RegularExpressions;

using Discuz.Common;
using Discuz.Data;
using Discuz.Config;
using Discuz.Entity;

namespace Discuz.Forum
{
	/// <summary>
	/// �����������
	/// </summary>
	public class Smilies
	{

        public static Regex[] regexSmile = null;

        static Smilies()
        {
            //InitRegexSmilies();
        }

        /// <summary>
        /// ��ʼ�����������������
        /// </summary>
        public static void InitRegexSmilies()
        {
            SmiliesInfo[] smiliesList = Smilies.GetSmiliesListWithInfo();
            int smiliesCount = smiliesList.Length;

            regexSmile = new Regex[smiliesCount];

            for (int i = 0; i < smiliesCount; i++)
            {
                 regexSmile[i] = new Regex(@Regex.Escape(smiliesList[i].Code), RegexOptions.None);

            }
        }

        /// <summary>
        /// ���¼��ز���ʼ�����������������
        /// </summary>
        /// <param name="smiliesList">�����������</param>
        public static void ResetRegexSmilies(SmiliesInfo[] smiliesList)
        {
            int smiliesCount = smiliesList.Length;

            // �����Ŀ��ͬ�����´�������, ���ⷢ������Խ��
            if (regexSmile == null || regexSmile.Length != smiliesCount)
            {
                regexSmile = new Regex[smiliesCount];
            }

            for (int i = 0; i < smiliesCount; i++)
            {
                regexSmile[i] = new Regex(@Regex.Escape(smiliesList[i].Code), RegexOptions.None);

            }
        }

		/// <summary>
		/// �õ����������
		/// </summary>
		/// <returns>���������</returns>
		public static IDataReader GetSmiliesList()
		{
            return DatabaseProvider.GetInstance().GetSmiliesList();
		}


		/// <summary>
		/// �õ����������,�����������
		/// </summary>
		/// <returns>�������</returns>
		public static DataTable GetSmiliesListDataTable()
		{
            return DatabaseProvider.GetInstance().GetSmiliesListDataTable();
		}

		/// <summary>
		/// �õ���������ı��������
		/// </summary>
		/// <returns>�������</returns>
		public static DataTable GetSmiliesListWithoutType()
		{
            return DatabaseProvider.GetInstance().GetSmiliesListWithoutType();
		}

		/// <summary>
		/// �������еı�����Ϣ����ΪSmiliesInfo[],�������������
		/// </summary>
		/// <returns></returns>
		public static SmiliesInfo[] GetSmiliesListWithInfo()
		{
			Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
			SmiliesInfo[] smilieslistinfo = cache.RetrieveObject("/Forum/UI/SmiliesListWithInfo") as SmiliesInfo[];
			if (smilieslistinfo == null)
			{
				DataTable dt = GetSmiliesListWithoutType();
				if (dt == null)
				{
					return null;
				}
				if (dt.Rows.Count <= 0)
				{
					return null;
				}

				smilieslistinfo = new SmiliesInfo[dt.Rows.Count];
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					smilieslistinfo[i] = new SmiliesInfo();
					smilieslistinfo[i].Id = Utils.StrToInt(dt.Rows[i]["id"],0);
					smilieslistinfo[i].Code = dt.Rows[i]["Code"].ToString();
					smilieslistinfo[i].Displayorder = Utils.StrToInt(dt.Rows[i]["Displayorder"],0);
					smilieslistinfo[i].Type = Utils.StrToInt(dt.Rows[i]["Type"],0);
					smilieslistinfo[i].Url = dt.Rows[i]["Url"].ToString();

				}
				cache.AddObject("/Forum/UI/SmiliesListWithInfo", smilieslistinfo);

                //���黺�����¼���ʱ���³�ʼ�����������������
                ResetRegexSmilies(smilieslistinfo);
			}
			return smilieslistinfo;
		}

		/// <summary>
		/// ��ñ�������б�
		/// </summary>
		/// <returns></returns>
		public static DataTable GetSmilieTypes()
		{
            return DatabaseProvider.GetInstance().GetSmilieTypes();
		}

        /// <summary>
        /// ��ȡ�������
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
		public static DataRow GetSmilieTypeById(string id)
		{
            return DatabaseProvider.GetInstance().GetSmilieTypeById(id);
		}

        /// <summary>
        /// ����յı������
        /// </summary>
        /// <returns>��������Ŀձ�������б�</returns>
        public static string ClearEmptySmilieType()
        {
            string emptySmilieList = "";
            DataTable smilieType = GetSmilieTypes();
            foreach (DataRow dr in smilieType.Rows)
            {
                if (DatabaseProvider.GetInstance().GetSmiliesInfoByType(int.Parse(dr["id"].ToString())).Rows.Count == 0)
                {
                    emptySmilieList += dr["code"].ToString() + ",";
                    DbHelper.ExecuteNonQuery(DatabaseProvider.GetInstance().DeleteSmily(int.Parse(dr["id"].ToString())));
                }
            }
            return emptySmilieList.TrimEnd(',');
        }

	}//class
}
