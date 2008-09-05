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
    /// 店铺分类管理操作类
    /// </summary>
    public class ShopCategories
    {

        /// <summary>
        /// 创建店铺分类
        /// </summary>
        /// <param name="shopcategoryinfo">店铺分类信息</param>
        /// <param name="targetshopcategoryinfo">要加入的目标分类信息</param>
        /// <param name="addtype">添加方式(1:作为同级分类 2:作为子分类 其它:根结店)</param>
        /// <returns>创建店铺分类id</returns>
        public static int CreateShopCategory(Shopcategoryinfo shopcategoryinfo, Shopcategoryinfo targetshopcategoryinfo, int addtype)
        {
            switch(addtype)
            {
                case 1: //作为同级分类
                    {
                        shopcategoryinfo.Parentid = targetshopcategoryinfo.Parentid;
                        shopcategoryinfo.Parentidlist = targetshopcategoryinfo.Parentidlist;
                        shopcategoryinfo.Layer = targetshopcategoryinfo.Layer;
                        break;
                    }
                case 2: //作为子分类
                    {
                        shopcategoryinfo.Parentid = targetshopcategoryinfo.Categoryid;
                        shopcategoryinfo.Parentidlist = targetshopcategoryinfo.Parentidlist == "0" ? targetshopcategoryinfo.Categoryid.ToString() : targetshopcategoryinfo.Parentidlist + "," + targetshopcategoryinfo.Categoryid;
                        shopcategoryinfo.Layer = targetshopcategoryinfo.Layer + 1;
                        break;
                    }
                default:
                    {
                        shopcategoryinfo.Parentid = 0;
                        shopcategoryinfo.Parentidlist = "0";
                        shopcategoryinfo.Layer = 0;
                        break;
                    }
            }
            return CreateShopCategory(shopcategoryinfo);
        }

        /// <summary>
        /// 创建店铺分类
        /// </summary>
        /// <param name="shopcategoryinfo">店铺分类信息</param>
        /// <returns>创建店铺分类id</returns>
        public static int CreateShopCategory(Shopcategoryinfo shopcategoryinfo)
        {
            int returnval =  DbProvider.GetInstance().CreateShopCategory(shopcategoryinfo);
            SetShopCategoryDispalyorder(shopcategoryinfo.Shopid);
            return returnval;
        }

        /// <summary>
        /// 获取指定店铺的商品分类
        /// </summary>
        /// <param name="shopid">店铺id</param>
        /// <returns>店铺商品分类表</returns>
        public static DataTable GetShopCategoryTable(int shopid)
        {
            return DbProvider.GetInstance().GetShopCategoryTableToJson(shopid);
        }

        /// <summary>
        /// 获取店铺的商品类型数据(json格式)
        /// </summary>
        /// <param name="shopid">店铺id</param>
        /// <returns>店铺的商品类型数据</returns>
        public static string GetShopCategoryJson(DataTable dt)
        {
            StringBuilder sb_category = new StringBuilder();
            sb_category.Append(Utils.DataTableToJSON(dt));
            return sb_category.ToString();
        }

        /// <summary>
        /// 获取店铺的商品类型数据(option格式)
        /// </summary>
        /// <param name="shopid">店铺id</param>
        /// <returns>商品类型数据</returns>
        public static string GetShopCategoryOption(DataTable dt, bool optgroup)
        {
            StringBuilder sb_category = new StringBuilder();
            foreach (DataRow dr in dt.Rows)
            {
                if (optgroup && dr["childcount"].ToString() != "0")
                {
                    sb_category.Append("<optgroup label=\"" + dr["name"].ToString() + "\"></optgroup>");
                }
                else
                {
                    sb_category.Append("<option value=\"");
                    sb_category.Append(dr["categoryid"].ToString());
                    sb_category.Append("\">");
                    sb_category.Append(Utils.GetSpacesString(Utils.StrToInt(dr["layer"].ToString(), 0)));
                    sb_category.Append(dr["name"].ToString().Trim());
                    sb_category.Append("</option>");
                }
            }
            return sb_category.ToString();
        }

        /// <summary>
        /// 获取指定分类id的店铺商品类型数据
        /// </summary>
        /// <param name="categoryid">分类id</param>
        /// <returns>店铺商品类型信息</returns>
        public static Shopcategoryinfo GetShopCategoryByCategoryId(int categoryid)
        {
            return DTO.GetShopCategoryInfo(DbProvider.GetInstance().GetShopCategoryByCategoryId(categoryid));
        }

        /// <summary>
        /// 删除指定的店铺商品分类
        /// </summary>
        /// <param name="categoryid">要删除的店铺商品分类id</param>
        /// <returns>是否删除成功</returns>
        public static bool DeleteCategoryByCategoryId(Shopcategoryinfo shopcategoryinfo)
        {
            if (DbProvider.GetInstance().IsExistSubShopCategory(shopcategoryinfo))
            {
                return false;
            }

            DbProvider.GetInstance().DeleteShopCategory(shopcategoryinfo);
            return true;
        }

        /// <summary>
        /// 移动商品分类
        /// </summary>
        /// <param name="shopcategoryinfo">源店铺商品分类</param>
        /// <param name="targetshopcategoryinfo">目标店铺商品分类</param>
        /// <param name="isaschildnode">是否作为子节点</param>
        /// <returns>是否移动成功</returns>
        public static bool MoveShopCategory(Shopcategoryinfo shopcategoryinfo, Shopcategoryinfo targetshopcategoryinfo, bool isaschildnode)
        {
            DbProvider.GetInstance().MovingShopCategoryPos(shopcategoryinfo, targetshopcategoryinfo, isaschildnode);
            SetShopCategoryDispalyorder(targetshopcategoryinfo.Shopid);
            return true;
        }

        /// <summary>
        /// 设置店铺商品分类显示顺序
        /// </summary>
        public static void SetShopCategoryDispalyorder(int shopid)
        {
            DataTable dt = DbProvider.GetInstance().GetShopCategoryByShopId(shopid);

            //更新店铺商品分类下的子分类数
            foreach (DataRow dr in dt.Rows)
            {
                DbProvider.GetInstance().UpdateShopCategoryChildCount(dt.Select("parentid=" + dr["categoryid"].ToString()).Length, int.Parse(dr["categoryid"].ToString()));
            }

            if (dt.Rows.Count == 1) return;

            int displayorder = 1;
            string categoryidlist;
            foreach (DataRow dr in dt.Select("parentid=0"))
            {
                if (dr["parentid"].ToString() == "0")
                {
                    ChildNode = "0";
                    categoryidlist = ("," + FindChildNode(dr["categoryid"].ToString())).Replace(",0,", "");

                    foreach (string categoryid in categoryidlist.Split(','))
                    {
                        DbProvider.GetInstance().UpdateShopCategoryDisplayOrder(displayorder, int.Parse(categoryid));
                        displayorder++;
                    }

                }
            }
        }


        #region  递归指定论坛版块下的所有子版块

        public static string ChildNode = "0";

        /// <summary>
        /// 递归所有子节点并返回字符串
        /// </summary>
        /// <param name="correntfid">当前</param>
        /// <returns>子版块的集合,格式:1,2,3,4,</returns>
        public static string FindChildNode(string categoryid)
        {
            lock (ChildNode)
            {
                DataTable dt = DbProvider.GetInstance().GetCategoryidInShopByParentid(int.Parse(categoryid));

                ChildNode = ChildNode + "," + categoryid;

                if (dt.Rows.Count > 0)
                {
                    //有子节点
                    foreach (DataRow dr in dt.Rows)
                    {
                        FindChildNode(dr["categoryid"].ToString());
                    }
                    dt.Dispose();
                }
                else
                {
                    dt.Dispose();
                }
                return ChildNode;
            }
        }

        #endregion

        /// <summary>
        /// 更新店铺分类
        /// </summary>
        /// <param name="shopcategoryinfo">店铺分类信息</param>
        /// <returns>是否更新成功</returns>
        public static bool UpdateShopCategory(Shopcategoryinfo shopcategoryinfo)
        {
            return DbProvider.GetInstance().UpdateShopCategory(shopcategoryinfo);
        }

        /// <summary>
        /// 数据转换对象类
        /// </summary>
        public class DTO
        {

            /// <summary>
            /// 获得店铺分类信息(DTO)
            /// </summary>
            /// <param name="__idatareader">要转换的数据</param>
            /// <returns>返回店铺分类信息</returns>
            public static Shopcategoryinfo GetShopCategoryInfo(IDataReader reader)
            {
                if (reader.Read())
                {
                    Shopcategoryinfo shopcategoryinfo = new Shopcategoryinfo();
                    shopcategoryinfo.Categoryid = Convert.ToInt32(reader["categoryid"].ToString());
                    shopcategoryinfo.Parentid = Convert.ToInt32(reader["parentid"].ToString());
                    shopcategoryinfo.Parentidlist = reader["parentidlist"].ToString().Trim();
                    shopcategoryinfo.Layer = Convert.ToInt32(reader["layer"].ToString());
                    shopcategoryinfo.Childcount = Convert.ToInt32(reader["childcount"].ToString());
                    shopcategoryinfo.Syscategoryid = Convert.ToInt32(reader["syscategoryid"].ToString());
                    shopcategoryinfo.Name = reader["name"].ToString().Trim();
                    shopcategoryinfo.Categorypic = reader["categorypic"].ToString().Trim();
                    shopcategoryinfo.Shopid = Convert.ToInt32(reader["shopid"].ToString());
                    shopcategoryinfo.Displayorder = Convert.ToInt32(reader["displayorder"].ToString());

                    reader.Close();
                    return shopcategoryinfo;
                }
                return null;
            }

            /// <summary>
            /// 获得店铺分类信息(DTO)
            /// </summary>
            /// <param name="__idatareader">要转换的数据表</param>
            /// <returns>返回店铺分类信息</returns>
            public static Shopcategoryinfo[] GetShopCategoryArray(DataTable dt)
            {
                if (dt == null || dt.Rows.Count == 0)
                    return null;

                Shopcategoryinfo[] shopcategoryinfoarray = new Shopcategoryinfo[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    shopcategoryinfoarray[i] = new Shopcategoryinfo();
                    shopcategoryinfoarray[i].Categoryid = Convert.ToInt32(dt.Rows[i]["categoryid"].ToString());
                    shopcategoryinfoarray[i].Parentid = Convert.ToInt32(dt.Rows[i]["parentid"].ToString());
                    shopcategoryinfoarray[i].Parentidlist = dt.Rows[i]["parentidlist"].ToString().Trim();
                    shopcategoryinfoarray[i].Layer = Convert.ToInt32(dt.Rows[i]["layer"].ToString());
                    shopcategoryinfoarray[i].Childcount = Convert.ToInt32(dt.Rows[i]["childcount"].ToString());
                    shopcategoryinfoarray[i].Syscategoryid = Convert.ToInt32(dt.Rows[i]["syscategoryid"].ToString());
                    shopcategoryinfoarray[i].Name = dt.Rows[i]["name"].ToString();
                    shopcategoryinfoarray[i].Categorypic = dt.Rows[i]["categorypic"].ToString();
                    shopcategoryinfoarray[i].Shopid = Convert.ToInt32(dt.Rows[i]["shopid"].ToString());
                    shopcategoryinfoarray[i].Displayorder = Convert.ToInt32(dt.Rows[i]["displayorder"].ToString());

                }
                dt.Dispose();
                return shopcategoryinfoarray;
            }

        }
    }

    
}
