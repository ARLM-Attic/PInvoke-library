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
    /// 店铺友情链接管理操作类
    /// </summary>
    public class ShopLinks
    {
        /// <summary>
        /// 创建店铺友情链接
        /// </summary>
        /// <param name="shoplinkinfo">店铺友情链接信息</param>
        /// <returns>创建店铺友情链接id</returns>
        public static int CreateShopLink(Shoplinkinfo shoplinkinfo)
        {
            return DbProvider.GetInstance().CreateShopLink(shoplinkinfo);
        }


        /// <summary>
        /// 更新店铺友情链接
        /// </summary>
        /// <param name="shoplinkinfo">店铺友情链接信息</param>
        /// <returns>是否更新成功</returns>
        public static bool UpdateShopLink(Shoplinkinfo shoplinkinfo)
        {
            return DbProvider.GetInstance().UpdateShopLink(shoplinkinfo);
        }


         /// <summary>
        /// 获取指定店铺的友情链接信息集合
        /// </summary>
        /// <param name="shopid">店铺id</param>
        /// <returns>友情链接信息集合</returns>
        public static ShoplinkinfoCollection GetShopLinkByShopId(int shopid)
        {
            return DTO.GetShopLinkList(DbProvider.GetInstance().GetShopLinkByShopId(shopid));
        }

        /// <summary>
        /// 删除指定id的店铺友情链接信息
        /// </summary>
        /// <param name="shoplinkidlist">店铺链接id串(格式:1,2,3)</param>
        /// <returns>删除店铺数</returns>
        public static int DeleteShopLink(string shoplinkidlist)
        {
            if (!Utils.IsNumericArray(shoplinkidlist.Split(',')))
            {
                return -1;
            }
            return DbProvider.GetInstance().DeleteShopLink(shoplinkidlist);
        }

        /// <summary>
        /// 数据转换对象类
        /// </summary>
        public class DTO
        {
            /// <summary>
            /// 获得店铺友情链接信息(DTO)
            /// </summary>
            /// <param name="__idatareader">要转换的数据</param>
            /// <returns>返回店铺友情链接信息</returns>
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
            /// 获得店铺友情链接信息(DTO)
            /// </summary>
            /// <param name="__idatareader">要转换的数据</param>
            /// <returns>返回店铺友情链接信息</returns>
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
            /// 获得店铺友情链接信息(DTO)
            /// </summary>
            /// <param name="__idatareader">要转换的数据表</param>
            /// <returns>返回店铺友情链接信息</returns>
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
