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
    /// 店铺管理操作类
    /// </summary>
    public class Shops
    {
        /// <summary>
        /// 创建店铺
        /// </summary>
        /// <param name="shopinfo">店铺信息</param>
        /// <returns>创建店铺id</returns>
        public static int CreateShop(Shopinfo shopinfo)
        {
            return DbProvider.GetInstance().CreateShop(shopinfo);
        }


        /// <summary>
        /// 更新店铺
        /// </summary>
        /// <param name="shopinfo">店铺信息</param>
        /// <returns>更新是否成功</returns>
        public static bool UpdateShop(Shopinfo shopinfo)
        {
            return DbProvider.GetInstance().UpdateShop(shopinfo);
        }


        /// <summary>
        /// 获取指定userid的店铺信息
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns>店铺信息</returns>
        public static Shopinfo GetShopByUserId(int userid)
        {
            Shopinfo shopinfo = DTO.GetShopInfo(DbProvider.GetInstance().GetShopByUserId(userid));

            //当无该店铺时, 则创建该店铺
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
        /// 获取热门或新开的店铺信息
        /// </summary>
        /// <param name="shoptype">热门店铺(1:热门店铺, 2 :新开店铺)</param>
        /// <returns>店铺json信息</returns>
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
        /// 数据转换对象类
        /// </summary>
        public class DTO
        {

            /// <summary>
            /// 获得店铺信息(DTO)
            /// </summary>
            /// <param name="__idatareader">要转换的数据</param>
            /// <returns>返回店铺信息</returns>
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
            /// 获得店铺信息(DTO)
            /// </summary>
            /// <param name="__idatareader">要转换的数据表</param>
            /// <returns>返回店铺信息</returns>
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
