using System;
using System.Data;
using System.IO;
using System.Text;

using Discuz.Common;
using Discuz.Entity;
using Discuz.Config;
using Discuz.Mall.Data;
using Discuz.Forum;

namespace Discuz.Mall
{
    /// <summary>
    /// �����������ӹ��������
    /// </summary>
    public class ShopLinks
    {
        /// <summary>
        /// ����������������
        /// </summary>
        /// <param name="shoplinkinfo">��������������Ϣ</param>
        /// <returns>����������������id</returns>
        public static int CreateShopLink(Shoplinkinfo shoplinkinfo)
        {
            return DbProvider.GetInstance().CreateShopLink(shoplinkinfo);
        }


        /// <summary>
        /// ���µ�����������
        /// </summary>
        /// <param name="shoplinkinfo">��������������Ϣ</param>
        /// <returns>�Ƿ���³ɹ�</returns>
        public static bool UpdateShopLink(Shoplinkinfo shoplinkinfo)
        {
            return DbProvider.GetInstance().UpdateShopLink(shoplinkinfo);
        }


         /// <summary>
        /// ��ȡָ�����̵�����������Ϣ����
        /// </summary>
        /// <param name="shopid">����id</param>
        /// <returns>����������Ϣ����</returns>
        public static ShoplinkinfoCollection GetShopLinkByShopId(int shopid)
        {
            return DTO.GetShopLinkList(DbProvider.GetInstance().GetShopLinkByShopId(shopid));
        }

        /// <summary>
        /// ɾ��ָ��id�ĵ�������������Ϣ
        /// </summary>
        /// <param name="shoplinkidlist">��������id��(��ʽ:1,2,3)</param>
        /// <returns>ɾ��������</returns>
        public static int DeleteShopLink(string shoplinkidlist)
        {
            if (!Utils.IsNumericArray(shoplinkidlist.Split(',')))
            {
                return -1;
            }
            return DbProvider.GetInstance().DeleteShopLink(shoplinkidlist);
        }

        /// <summary>
        /// ����ת��������
        /// </summary>
        public class DTO
        {
            /// <summary>
            /// ��õ�������������Ϣ(DTO)
            /// </summary>
            /// <param name="__idatareader">Ҫת��������</param>
            /// <returns>���ص�������������Ϣ</returns>
            public static Shoplinkinfo GetShopLinkInfo(IDataReader reader)
            {
                if (reader == null)
                {
                    return null;
                }
                Shoplinkinfo shoplinkinfo = new Shoplinkinfo();
                if (reader.Read())
                {
                    shoplinkinfo.Id = Convert.ToInt32(reader["id"].ToString());
                    shoplinkinfo.Displayorder = Convert.ToInt32(reader["displayorder"].ToString());
                    shoplinkinfo.Name = reader["name"].ToString().Trim();
                    shoplinkinfo.Linkshopid = Convert.ToInt32(reader["linkshopid"].ToString().Trim());
                    shoplinkinfo.Shopid = Convert.ToInt32(reader["shopid"].ToString());
                }
                reader.Close();
                return shoplinkinfo;
            }

             /// <summary>
            /// ��õ�������������Ϣ(DTO)
            /// </summary>
            /// <param name="__idatareader">Ҫת��������</param>
            /// <returns>���ص�������������Ϣ</returns>
            public static ShoplinkinfoCollection GetShopLinkList(IDataReader reader)
            {
                ShoplinkinfoCollection shoplinkinfocoll = new ShoplinkinfoCollection();

                while (reader.Read())
                {
                    Shoplinkinfo shoplinkinfo = new Shoplinkinfo();
                    shoplinkinfo.Id = Convert.ToInt32(reader["id"].ToString());
                    shoplinkinfo.Displayorder = Convert.ToInt32(reader["displayorder"].ToString());
                    shoplinkinfo.Name = reader["name"].ToString().Trim();
                    shoplinkinfo.Linkshopid = Convert.ToInt32(reader["linkshopid"].ToString().Trim());
                    shoplinkinfo.Shopid = Convert.ToInt32(reader["shopid"].ToString());

                    shoplinkinfocoll.Add(shoplinkinfo);
                }
                reader.Close();
                return shoplinkinfocoll;
            }


            /// <summary>
            /// ��õ�������������Ϣ(DTO)
            /// </summary>
            /// <param name="__idatareader">Ҫת�������ݱ�</param>
            /// <returns>���ص�������������Ϣ</returns>
            public static Shoplinkinfo[] GetShopLinkInfoArray(DataTable dt)
            {
                if (dt == null || dt.Rows.Count == 0)
                    return null;

                Shoplinkinfo[] shoplinkarray = new Shoplinkinfo[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    shoplinkarray[i] = new Shoplinkinfo();
                    shoplinkarray[i].Id = Convert.ToInt32(dt.Rows[i]["id"].ToString());
                    shoplinkarray[i].Displayorder = Convert.ToInt32(dt.Rows[i]["displayorder"].ToString());
                    shoplinkarray[i].Name = dt.Rows[i]["name"].ToString();
                    shoplinkarray[i].Linkshopid = Convert.ToInt32(dt.Rows[i]["linkshopid"].ToString());
                    shoplinkarray[i].Shopid = Convert.ToInt32(dt.Rows[i]["shopid"].ToString());

                }
                dt.Dispose();
                return shoplinkarray;
            }
        }
    }

   
}
