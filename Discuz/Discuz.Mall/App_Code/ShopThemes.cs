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
    /// ����������������
    /// </summary>
    public class ShopThemes
    {

        /// <summary>
        /// ������������
        /// </summary>
        /// <param name="shopinfo">������Ϣ</param>
        /// <returns>������������id</returns>
        public static int CreateShop(Shopthemeinfo shopthemeinfo)
        {
            return DbProvider.GetInstance().CreateShopTheme(shopthemeinfo);
        }


        /// <summary>
        /// ���µ�������
        /// </summary>
        /// <param name="shopinfo">������Ϣ</param>
        /// <returns>�����Ƿ�ɹ�</returns>
        public static bool UpdateShop(Shopthemeinfo shopthemeinfo)
        {
            return DbProvider.GetInstance().UpdateShopTheme(shopthemeinfo);
        }

        /// <summary>
        /// ��ȡָ������ID�ĵ��̷����Ϣ
        /// </summary>
        /// <param name="themeid">����ID</param>
        /// <returns>���̷����Ϣ</returns>
        public static Shopthemeinfo GetShopThemeByThemeId(int themeid)
        {
            return DTO.GetShopThemeInfo(DbProvider.GetInstance().GetShopThemeByThemeId(themeid));
        }

        /// <summary>
        /// �������������DataTable�ķ�ʽ���뻺��
        /// </summary>
        /// <returns>��Ʒ�����</returns>
        public static DataTable GetShopThemesTable()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            DataTable dt = cache.RetrieveObject("/Mall/MallSetting/ShopTheme") as DataTable;
            if (dt == null)
            {
                dt = DbProvider.GetInstance().GetShopThemes();
                cache.AddObject("/Mall/MallSetting/ShopTheme", dt);
            }
            return dt;
        }

        /// <summary>
        /// ��ȡ����������Ϣ(option��ʽ)
        /// </summary>
        /// <returns>����������Ϣ</returns>
        public static string GetShopThemeOption()
        {
            StringBuilder sb_category = new StringBuilder();
            foreach (DataRow dr in GetShopThemesTable().Rows)
            {
                sb_category.Append("<option value=\"");
                sb_category.Append(dr["themeid"].ToString());
                sb_category.Append("\">");
                sb_category.Append(dr["name"].ToString().Trim());
                sb_category.Append("</option>");
            }
            return sb_category.ToString();
        }


        /// <summary>
        /// ����ת��������
        /// </summary>
        public class DTO
        {

            /// <summary>
            /// ��õ���������Ϣ(DTO)
            /// </summary>
            /// <param name="__idatareader">Ҫת��������</param>
            /// <returns>���ص���������Ϣ</returns>
            public static Shopthemeinfo GetShopThemeInfo(IDataReader reader)
            {
                if (reader.Read())
                {
                    Shopthemeinfo shopthemeinfo = new Shopthemeinfo();
                    shopthemeinfo.Themeid = Convert.ToInt32(reader["themeid"].ToString());
                    shopthemeinfo.Directory = reader["directory"].ToString().Trim();
                    shopthemeinfo.Name = reader["name"].ToString().Trim();
                    shopthemeinfo.Author = reader["author"].ToString().Trim();
                    shopthemeinfo.Createdate = reader["createdate"].ToString().Trim();
                    shopthemeinfo.Copyright = reader["copyright"].ToString().Trim();

                    reader.Close();
                    return shopthemeinfo;
                }
                return null;
            }

            /// <summary>
            /// ��õ���������Ϣ(DTO)
            /// </summary>
            /// <param name="__idatareader">Ҫת�������ݱ�</param>
            /// <returns>���ص���������Ϣ</returns>
            public static Shopthemeinfo[] GetShopThemeInfoArray(DataTable dt)
            {
                if (dt == null || dt.Rows.Count == 0)
                    return null;

                Shopthemeinfo[] __shopthemeinfoarray = new Shopthemeinfo[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    __shopthemeinfoarray[i] = new Shopthemeinfo();
                    __shopthemeinfoarray[i].Themeid = Convert.ToInt32(dt.Rows[i]["themeid"].ToString());
                    __shopthemeinfoarray[i].Directory = dt.Rows[i]["directory"].ToString();
                    __shopthemeinfoarray[i].Name = dt.Rows[i]["name"].ToString();
                    __shopthemeinfoarray[i].Author = dt.Rows[i]["author"].ToString();
                    __shopthemeinfoarray[i].Createdate = dt.Rows[i]["createdate"].ToString();
                    __shopthemeinfoarray[i].Copyright = dt.Rows[i]["copyright"].ToString();
                }
                dt.Dispose();
                return __shopthemeinfoarray;
            }
        }
    }
}
