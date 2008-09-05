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
    /// ���̹��������
    /// </summary>
    public class Shops
    {
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="shopinfo">������Ϣ</param>
        /// <returns>��������id</returns>
        public static int CreateShop(Shopinfo shopinfo)
        {
            return DbProvider.GetInstance().CreateShop(shopinfo);
        }


        /// <summary>
        /// ���µ���
        /// </summary>
        /// <param name="shopinfo">������Ϣ</param>
        /// <returns>�����Ƿ�ɹ�</returns>
        public static bool UpdateShop(Shopinfo shopinfo)
        {
            return DbProvider.GetInstance().UpdateShop(shopinfo);
        }


        /// <summary>
        /// ��ȡָ��userid�ĵ�����Ϣ
        /// </summary>
        /// <param name="userid">�û�id</param>
        /// <returns>������Ϣ</returns>
        public static Shopinfo GetShopByUserId(int userid)
        {
            Shopinfo shopinfo = DTO.GetShopInfo(DbProvider.GetInstance().GetShopByUserId(userid));

            //���޸õ���ʱ, �򴴽��õ���
            if (shopinfo == null)
            {
                shopinfo = new Shopinfo();
                shopinfo.Bulletin = "";
                shopinfo.Createdatetime = DateTime.Now;
                shopinfo.Introduce = "";
                shopinfo.Lid = -1;
                shopinfo.Locus = "";
                shopinfo.Logo = "";
                shopinfo.Shopname = "";
                shopinfo.Themeid = 0;
                shopinfo.Themepath = "";
                shopinfo.Uid = userid;
                shopinfo.Username = "";
                Shops.CreateShop(shopinfo);

                shopinfo = Shops.GetShopByUserId(userid);
            }
            return shopinfo;
        }
        
        /// <summary>
        /// ��ȡ���Ż��¿��ĵ�����Ϣ
        /// </summary>
        /// <param name="shoptype">���ŵ���(1:���ŵ���, 2 :�¿�����)</param>
        /// <returns>����json��Ϣ</returns>
        public static string GetShopInfoJson(int shoptype)
        {
            StringBuilder sb_shops = new StringBuilder();
            sb_shops.Append("[");

            IDataReader __idatareader = DbProvider.GetInstance().GetHotOrNewShops(shoptype, 4);

            while (__idatareader.Read())
            {
                sb_shops.Append(string.Format("{{'shopid' : {0}, 'logo' : '{1}', 'shopname' : '{2}', 'uid' : {3}, 'username' : '{4}'}},",
                    __idatareader["shopid"].ToString(),
                    __idatareader["logo"].ToString(),
                    __idatareader["shopname"].ToString().Trim(),
                    __idatareader["uid"].ToString().Trim(),
                    __idatareader["username"].ToString().ToLower()
                    ));
            }
            __idatareader.Close();

            if (sb_shops.ToString().EndsWith(","))
            {
                sb_shops.Remove(sb_shops.Length - 1, 1);
            }
            sb_shops.Append("]");
            return sb_shops.ToString();
        }

        /// <summary>
        /// ����ת��������
        /// </summary>
        public class DTO
        {

            /// <summary>
            /// ��õ�����Ϣ(DTO)
            /// </summary>
            /// <param name="__idatareader">Ҫת��������</param>
            /// <returns>���ص�����Ϣ</returns>
            public static Shopinfo GetShopInfo(IDataReader reader)
            {
                if (reader.Read())
                {

                    Shopinfo shopinfo = new Shopinfo();
                    shopinfo.Shopid = Convert.ToInt32(reader["shopid"].ToString());
                    shopinfo.Logo = reader["logo"].ToString().Trim();
                    shopinfo.Shopname = reader["shopname"].ToString().Trim();
                    shopinfo.Themeid = Convert.ToInt32(reader["themeid"].ToString());
                    shopinfo.Themepath = reader["themepath"].ToString().Trim();
                    shopinfo.Uid = Convert.ToInt32(reader["uid"].ToString());
                    shopinfo.Username = reader["username"].ToString().Trim();
                    shopinfo.Introduce = reader["introduce"].ToString().Trim();
                    shopinfo.Lid = Convert.ToInt32(reader["lid"].ToString());
                    shopinfo.Locus = reader["locus"].ToString().Trim();
                    shopinfo.Bulletin = reader["bulletin"].ToString().Trim();
                    shopinfo.Createdatetime = Convert.ToDateTime(reader["createdatetime"].ToString());
                    shopinfo.Invisible = Convert.ToInt32(reader["invisible"].ToString());
                    shopinfo.Viewcount = Convert.ToInt32(reader["viewcount"].ToString());

                    reader.Close();
                    return shopinfo;
                }
                return null;
            }

            /// <summary>
            /// ��õ�����Ϣ(DTO)
            /// </summary>
            /// <param name="__idatareader">Ҫת�������ݱ�</param>
            /// <returns>���ص�����Ϣ</returns>
            public static Shopinfo[] GetShopInfoArray(DataTable dt)
            {
                if (dt == null || dt.Rows.Count == 0)
                    return null;

                Shopinfo[] shopinfoarray = new Shopinfo[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    shopinfoarray[i] = new Shopinfo();
                    shopinfoarray[i].Shopid = Convert.ToInt32(dt.Rows[i]["shopid"].ToString());
                    shopinfoarray[i].Logo = dt.Rows[i]["logo"].ToString();
                    shopinfoarray[i].Shopname = dt.Rows[i]["shopname"].ToString();
                    shopinfoarray[i].Themeid = Convert.ToInt32(dt.Rows[i]["themeid"].ToString());
                    shopinfoarray[i].Themepath = dt.Rows[i]["themepath"].ToString();
                    shopinfoarray[i].Uid = Convert.ToInt32(dt.Rows[i]["uid"].ToString());
                    shopinfoarray[i].Username = dt.Rows[i]["username"].ToString();
                    shopinfoarray[i].Introduce = dt.Rows[i]["introduce"].ToString();
                    shopinfoarray[i].Lid = Convert.ToInt32(dt.Rows[i]["lid"].ToString());
                    shopinfoarray[i].Locus = dt.Rows[i]["locus"].ToString();
                    shopinfoarray[i].Bulletin = dt.Rows[i]["bulletin"].ToString();
                    shopinfoarray[i].Createdatetime = Convert.ToDateTime(dt.Rows[i]["createdatetime"].ToString());
                    shopinfoarray[i].Invisible = Convert.ToInt32(dt.Rows[i]["invisible"].ToString());
                    shopinfoarray[i].Viewcount = Convert.ToInt32(dt.Rows[i]["viewcount"].ToString());

                }
                dt.Dispose();
                return shopinfoarray;
            }
        }
    }
}
