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
    /// 店铺主题管理操作类
    /// </summary>
    public class ShopThemes
    {

        /// <summary>
        /// 创建店铺主题
        /// </summary>
        /// <param name="shopinfo">店铺信息</param>
        /// <returns>创建店铺主题id</returns>
        public static int CreateShop(Shopthemeinfo shopthemeinfo)
        {
            return DbProvider.GetInstance().CreateShopTheme(shopthemeinfo);
        }


        /// <summary>
        /// 更新店铺主题
        /// </summary>
        /// <param name="shopinfo">店铺信息</param>
        /// <returns>更新是否成功</returns>
        public static bool UpdateShop(Shopthemeinfo shopthemeinfo)
        {
            return DbProvider.GetInstance().UpdateShopTheme(shopthemeinfo);
        }

        /// <summary>
        /// 获取指定主题ID的店铺风格信息
        /// </summary>
        /// <param name="themeid">主题ID</param>
        /// <returns>店铺风格信息</returns>
        public static Shopthemeinfo GetShopThemeByThemeId(int themeid)
        {
            return DTO.GetShopThemeInfo(DbProvider.GetInstance().GetShopThemeByThemeId(themeid));
        }

        /// <summary>
        /// 将店铺主题表以DataTable的方式存入缓存
        /// </summary>
        /// <returns>商品分类表</returns>
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
        /// 获取店铺主题信息(option格式)
        /// </summary>
        /// <returns>店铺主题信息</returns>
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
        /// 数据转换对象类
        /// </summary>
        public class DTO
        {

            /// <summary>
            /// 获得店铺主题信息(DTO)
            /// </summary>
            /// <param name="__idatareader">要转换的数据</param>
            /// <returns>返回店铺主题信息</returns>
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
            /// 获得店铺主题信息(DTO)
            /// </summary>
            /// <param name="__idatareader">要转换的数据表</param>
            /// <returns>返回店铺主题信息</returns>
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
