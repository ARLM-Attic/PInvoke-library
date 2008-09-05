using System;
using System.Data;
using System.Data.Common;

using Discuz.Common;
using Discuz.Data;
using Discuz.Entity;

namespace Discuz.Forum
{
    /// <summary>
    /// �����������
    /// </summary>
    public class AdminGroups
    {

        /// <summary>
        /// ��õ�ָ����������Ϣ
        /// </summary>
        /// <returns>��������Ϣ</returns>
        public static AdminGroupInfo[] GetAdminGroupList()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            AdminGroupInfo[] admingroupArray = cache.RetrieveObject("/Forum/AdminGroupList") as AdminGroupInfo[];
            if (admingroupArray == null)
            {
                DataTable dt = DatabaseProvider.GetInstance().GetAdminGroupList();
                admingroupArray = new AdminGroupInfo[dt.Rows.Count];
                AdminGroupInfo admingroup;
                int Index = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    admingroup = new AdminGroupInfo();
                    admingroup.Admingid = short.Parse(dr["admingid"].ToString());
                    admingroup.Alloweditpost = byte.Parse(dr["alloweditpost"].ToString());
                    admingroup.Alloweditpoll = byte.Parse(dr["alloweditpoll"].ToString());
                    admingroup.Allowstickthread = byte.Parse(dr["allowstickthread"].ToString());
                    admingroup.Allowmodpost = byte.Parse(dr["allowmodpost"].ToString());
                    admingroup.Allowdelpost = byte.Parse(dr["allowdelpost"].ToString());
                    admingroup.Allowmassprune = byte.Parse(dr["allowmassprune"].ToString());
                    admingroup.Allowrefund = byte.Parse(dr["allowrefund"].ToString());
                    admingroup.Allowcensorword = byte.Parse(dr["allowcensorword"].ToString());
                    admingroup.Allowviewip = byte.Parse(dr["allowviewip"].ToString());
                    admingroup.Allowbanip = byte.Parse(dr["allowbanip"].ToString());
                    admingroup.Allowedituser = byte.Parse(dr["allowedituser"].ToString());
                    admingroup.Allowmoduser = byte.Parse(dr["allowmoduser"].ToString());
                    admingroup.Allowbanuser = byte.Parse(dr["allowbanuser"].ToString());
                    admingroup.Allowpostannounce = byte.Parse(dr["allowpostannounce"].ToString());
                    admingroup.Allowviewlog = byte.Parse(dr["allowviewlog"].ToString());
                    admingroup.Disablepostctrl = byte.Parse(dr["disablepostctrl"].ToString());
                    admingroupArray[Index] = admingroup;
                    Index++;
                }

                cache.AddObject("/Forum/AdminGroupList", admingroupArray);

                dt.Dispose();
            }
            return admingroupArray;
        }

        /// <summary>
        /// ��õ�ָ����������Ϣ
        /// </summary>
        /// <param name="admingid">������ID</param>
        /// <returns>����Ϣ</returns>
        public static AdminGroupInfo GetAdminGroupInfo(int admingid)
        {
            // ���������id����0
            if (admingid > 0)
            {
                AdminGroupInfo[] admingroupArray = GetAdminGroupList();
                foreach (AdminGroupInfo admingroup in admingroupArray)
                {
                    // ������ڸù������򷵻ظ�����Ϣ
                    if (admingroup.Admingid == admingid)
                    {
                        return admingroup;
                    }
                }
            }
            // ��������ڸ����򷵻�null
            return null;
        }


        /// <summary>
        /// ���ù�������Ϣ
        /// </summary>
        /// <param name="__admingroupsInfo">��������Ϣ</param>
        /// <returns>���ļ�¼��</returns>
        public static int SetAdminGroupInfo(AdminGroupInfo admingroupsInfo)
        {
            return DatabaseProvider.GetInstance().SetAdminGroupInfo(admingroupsInfo);
        }

        /// <summary>
        /// ����һ���µĹ�������Ϣ
        /// </summary>
        /// <param name="__admingroupsInfo">Ҫ��ӵĹ�������Ϣ</param>
        /// <returns>���ļ�¼��</returns>
        public static int CreateAdminGroupInfo(AdminGroupInfo admingroupsInfo)
        {
            return DatabaseProvider.GetInstance().CreateAdminGroupInfo(admingroupsInfo);
        }

        /// <summary>
        /// ɾ��ָ���Ĺ�������Ϣ
        /// </summary>
        /// <param name="admingid">������ID</param>
        /// <returns>���ļ�¼��</returns>
        public static int DeleteAdminGroupInfo(short admingid)
        {
            return DatabaseProvider.GetInstance().DeleteAdminGroupInfo(admingid);
        }
    }//class end


}
