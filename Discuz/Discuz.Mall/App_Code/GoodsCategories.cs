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
    /// 商品分类管理操作类
    /// </summary>
    public class GoodsCategories : WriteFile
    {
        #region 私有变量
        private static volatile GoodsCategories instance = null;
        private static object lockHelper = new object();
        private static string jsonPath = "";
        #endregion

        #region 返回唯一实例
        private GoodsCategories()
        {
            jsonPath = Utils.GetMapPath(BaseConfigs.GetForumPath + "\\javascript\\goodscategories.js");
        }

        public static GoodsCategories GetInstance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockHelper)
                    {
                        if (instance == null)
                        {
                            instance = new GoodsCategories();
                        }
                    }
                }
                return instance;
            }
        }
        #endregion

        /// <summary>
        /// 更新指定商品数据信息
        /// </summary>
        /// <param name="goodsinfo">商品信息</param>
        /// <returns></returns>
        public static void UpdateGoodsCategory(Goodscategoryinfo goodscategoryinfo)
        {
            DbProvider.GetInstance().UpdateGoodscategory(goodscategoryinfo);
        }

        /// <summary>
        /// 删除指定商品分类
        /// </summary>
        /// <param name="categoryid">分类ID</param>
        public static void DeleteGoodsCategory(int categoryid)
        {
            DbProvider.GetInstance().DeleteGoodsCategory(categoryid);
        }

        /// <summary>
        /// 创建商品分类数据信息
        /// </summary>
        /// <param name="goodsinfo">商品信息</param>
        /// <returns>商品分类id</returns>
        public static int CreateGoodsCategory(Goodscategoryinfo goodscategoryinfo)
        {
            return DbProvider.GetInstance().CreateGoodscategory(goodscategoryinfo);
        }

        /// <summary>
        /// 获取指定分类id的分类信息
        /// </summary>
        /// <param name="categoryid">分类id</param>
        /// <returns>分类信息</returns>
        public static Goodscategoryinfo GetGoodsCategoryInfoById(int categoryid)
        {
            return DTO.GetGoodsCategoryInfo(DbProvider.GetInstance().GetGoodsCategoryInfoById(categoryid));
        }

        /// <summary>
        /// 设置商品分类列表中层数(layer)和父列表(parentidlist)字段
        /// </summary>
        public static void SetGoodsCategoryeslayer()
        {
            DataTable dt = DbProvider.GetInstance().GetCategoriesTable();
            foreach (DataRow dr in dt.Rows)
            {
                int layer = 0;
                string parentidlist = "";
                string parentid = dr["parentid"].ToString().Trim();

                //如果是(分类)顶层则直接更新数据库
                if (parentid == "0")
                {
                    DbProvider.GetInstance().UpdateCategoriesInfo(int.Parse(dr["categoryid"].ToString()),
                        layer,
                        "0",
                        HasChild(dt, dr["categoryid"].ToString().Trim()));
                    continue;
                }

                do
                { //更新子分类的层数(layer)和父列表(parentidlist)字段
                    string temp = parentid;

                    parentid = DbProvider.GetInstance().GetCategoriesParentidByID(int.Parse(parentid)).ToString();

                    layer++;
                    if (parentid != "0")
                    {
                        parentidlist = temp + "," + parentidlist;
                    }
                    else
                    {
                        parentidlist = temp + "," + parentidlist;
                        DbProvider.GetInstance().UpdateCategoriesInfo(int.Parse(dr["categoryid"].ToString()),
                            layer,
                            parentidlist.Substring(0, parentidlist.Length - 1),
                            HasChild(dt, dr["categoryid"].ToString().Trim()));
                        break;
                    }
                } while (true);
            }
            dt.Dispose();
        }

        #region  递归指定分类下的所有子分类

        public static string ChildNode = "0";

        /// <summary>
        /// 递归所有子节点并返回字符串
        /// </summary>
        /// <param name="correntfid">当前</param>
        /// <returns>子版块的集合,格式:1,2,3,4,</returns>
        public static string FindChildNode(string currentcid)
        {
            lock (ChildNode)
            {
                IDataReader __idatareader = DbProvider.GetInstance().GetSubGoodsCategories(int.Parse(currentcid));

                ChildNode = ChildNode + "," + currentcid;

                if (__idatareader != null)
                {
                    //有子节点
                    while(__idatareader.Read())
                    {
                        FindChildNode(__idatareader["categoryid"].ToString());
                    }
                    __idatareader.Dispose();
                }
                return ChildNode;
            }
        }

        #endregion

        /// <summary>
        /// 判断当前分类是否还有子分类
        /// </summary>
        /// <param name="dt">商品分类表</param>
        /// <param name="categoryid">商品分类</param>
        /// <returns>是否有子分类</returns>
        public static int HasChild(DataTable dt, string categoryid)
        {
            int haschild = 0;
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["parentid"].ToString().Trim() == categoryid)
                {
                    haschild = 1;
                    break;
                }
            }

            return haschild;
        }

        /// <summary>
        /// 设置显示顺序
        /// </summary>
        public static void SetDispalyorder()
        {
            DataTable dt = DbProvider.GetInstance().GetCategoriesTable();

            if (dt.Rows.Count == 1) return;

            int displayorder = 1;
            string cidlist;
            foreach (DataRow dr in dt.Select("parentid=0"))
            {
                if (dr["parentid"].ToString() == "0")
                {
                    ChildNode = "0";
                    cidlist = ("," + FindChildNode(dr["categoryid"].ToString())).Replace(",0,", "");

                    foreach (string cidstr in cidlist.Split(','))
                    {
                        DbProvider.GetInstance().UpdateGoodsCategoryDisplayorder(displayorder, int.Parse(cidstr));
                        displayorder++;
                    }

                }
            }
        }

      
        /// <summary>
        /// 设置商品分类表中路径(pathlist)字段
        /// </summary>
        public static void SetCategoryPathList()
        {
            string extname = GeneralConfigs.Deserialize(Utils.GetMapPath(BaseConfigs.GetForumPath + "config/general.config")).Extname;
            SetCategoryPathList(true, extname);
        }


        /// <summary>
        /// 按指定的文件扩展名称设置商品分类表中路径(pathlist)字段
        /// </summary>
        /// <param name="extname">扩展名称,如:aspx , html 等</param>
        public static void SetCategoryPathList(bool isaspxrewrite, string extname)
        {
            DataTable dt = DbProvider.GetInstance().GetCategoriesTable();

            GeneralConfigInfo config = GeneralConfigs.GetConfig();
            foreach (DataRow dr in dt.Rows)
            {
                string pathlist = "";

                if (dr["parentidlist"].ToString().Trim() == "0")
                {
                    if (isaspxrewrite)
                    {
                        pathlist = "<a href=\"showgoodslist-" + dr["categoryid"].ToString() + extname + "\">" + dr["categoryname"].ToString().Trim() + "</a>";
                    }
                    else
                    {
                        pathlist = "<a href=\"showgoodslist.aspx?categoryid=" + dr["categoryid"].ToString() + "\">" + dr["categoryname"].ToString().Trim() + "</a>";
                    }
                }
                else
                {
                    foreach (string parentid in dr["parentidlist"].ToString().Trim().Split(','))
                    {
                        if (parentid.Trim() != "")
                        {
                            DataRow[] drs = dt.Select("[categoryid]=" + parentid);
                            if (drs.Length > 0)
                            {
                                if (isaspxrewrite)
                                {
                                    pathlist += "<a href=\"showgoodslist-" + drs[0]["categoryid"].ToString() + extname + "\">" + drs[0]["categoryname"].ToString().Trim() + "</a>";
                                }
                                else
                                {
                                    pathlist += "<a href=\"showgoodslist.aspx?categoryid=" + drs[0]["categoryid"].ToString() + "\">" + drs[0]["categoryname"].ToString().Trim() + "</a>";
                                }
                            }
                        }
                    }
                    if (isaspxrewrite)
                    {
                        pathlist += "<a href=\"showgoodslist-" + dr["categoryid"].ToString() + extname + "\">" + dr["categoryname"].ToString().Trim() + "</a>";
                    }
                    else
                    {
                        pathlist += "<a href=\"showgoodslist.aspx?categoryid=" + dr["categoryid"].ToString() + "\">" + dr["categoryname"].ToString().Trim() + "</a>";
                    }
                }

                DbProvider.GetInstance().SetGoodsCategoryPathList(pathlist, int.Parse(dr["categoryid"].ToString()));
            }
        }


        /// <summary>
        /// 将商品分类表以DataTable的方式存入缓存
        /// </summary>
        /// <returns>商品分类表</returns>
        public static DataTable GetCategoriesTable()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            DataTable dt = cache.RetrieveObject("/Mall/MallSetting/GoodsCategories") as DataTable;
            if (dt == null)
            {
                dt = DbProvider.GetInstance().GetCategoriesTable();
                cache.AddObject("/Mall/MallSetting/GoodsCategories", dt);
            }
            return dt;
        }


        /// <summary>
        /// 更新指定分类的有效商品数量
        /// </summary>
        /// <param name="goodscategoryinfo">指定分类的信息</param>
        /// <returns>更新商品分类数</returns>
        public static bool UpdateCategoryGoodsCount(Goodscategoryinfo goodscategoryinfo)
        {
            if (goodscategoryinfo != null && goodscategoryinfo.Categoryid > 0)
            {
                goodscategoryinfo.Goodscount = Goods.GetGoodsCount(goodscategoryinfo.Categoryid, "");
                GoodsCategories.UpdateGoodsCategory(goodscategoryinfo);
                return true;
            }
            return false;
        }


        /// <summary>
        /// 更新分类的有效商品数量
        /// </summary>
        /// <returns>更新商品分类数</returns>
        public static bool UpdateCategoryGoodsCount()
        {
            Goodscategoryinfo[] goodsArray = DTO.GetGoodsCategoryInfoArray(GoodsCategories.GetCategoriesTable());
            if(goodsArray == null)
                return true;
            foreach (Goodscategoryinfo goodscategoryinfo in goodsArray)
            {
                if (goodscategoryinfo != null && goodscategoryinfo.Categoryid > 0)
                {
                    goodscategoryinfo.Goodscount = Goods.GetGoodsCount(goodscategoryinfo.Categoryid, "");
                    GoodsCategories.UpdateGoodsCategory(goodscategoryinfo);
                }
            }
            return true;
        }

        /// <summary>
        /// 获取指定分类的fid(版块id)字段信息
        /// </summary>
        /// <param name="categoryid">指定的分类id</param>
        /// <returns>版块id</returns>
        public static int GetCategoriesFid(int categoryid)
        {
            int forumid = -1;
            DataTable dt = GetCategoriesTable();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Select("categoryid=" + categoryid))
                { 
                    forumid = Convert.ToInt32(dr["fid"].ToString());
                    break;
                }
            }
            return forumid;
        }

        /// <summary>
        /// 通过指定的论坛版块id获取相应的商品分类
        /// </summary>
        /// <param name="forumid">版块id</param>
        /// <returns>商品分类id</returns>
        public static int GetGoodsCategoryIdByFid(int forumid)
        {
            return DbProvider.GetInstance().GetGoodsCategoryIdByFid(forumid);
        }

        /// <summary>
        /// 获取指定分类id下的所有子分类的json格式信息
        /// </summary>
        /// <param name="categoryid">指定的分类id</param>
        /// <returns>json格式信息串</returns>
        public static string GetSubCategoriesJson(int categoryid)
        {
            return GetGoodsCategoryJsonData(DbProvider.GetInstance().GetSubGoodsCategories(categoryid));
        }


        /// <summary>
        /// 获取指定层数的商品分类 
        /// </summary>
        /// <returns>json格式信息串</returns>
        public static string GetRootGoodsCategoriesJson()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            string goodscategoriesjson = cache.RetrieveObject("/Mall/MallSetting/RootGoodsCategories") as string;
            if (goodscategoriesjson == null)
            {
                goodscategoriesjson = GetGoodsCategoryJsonData(DbProvider.GetInstance().GetGoodsCategoriesByLayer(1));
                cache.AddObject("/Mall/MallSetting/RootGoodsCategories", goodscategoriesjson);
            }
            return goodscategoriesjson;
        }

        /// <summary>
        /// 获取JSON格式的商品分类信息
        /// </summary>
        /// <param name="__idatareader">数据信息</param>
        /// <returns>返回Json数据</returns>
        private static string GetGoodsCategoryJsonData(IDataReader reader)
        {
            StringBuilder sb_categories = new StringBuilder();
            sb_categories.Append("var cats = ");
            sb_categories.Append("[\r\n");

            while (reader.Read())
            {
                sb_categories.Append(string.Format("{{'id' : {0}, 'pid' : {1}, 'pidlist' : '{2}', 'name' : '{3}', 'child' : {4}, 'gcount' :{5}, 'layer' : {6}, 'fid' : {7}}},",
                    reader["categoryid"].ToString(),
                    reader["parentid"].ToString(),
                    reader["parentidlist"].ToString().Trim(),
                    reader["categoryname"].ToString().Trim().Replace("\\", "\\\\").Replace("'", "\\'"),
                    reader["haschild"].ToString().ToLower(),
                    reader["goodscount"].ToString(),
                    reader["layer"].ToString(),
                    reader["fid"].ToString()
                    ));
            }
            reader.Close();

            if (sb_categories.ToString().EndsWith(","))
            {
                sb_categories.Remove(sb_categories.Length - 1, 1);
            }
            sb_categories.Append("\r\n];");
            return sb_categories.ToString();
        }

        /// <summary>
        /// 返回fid与categoryid对应的JSON数据
        /// </summary>
        /// <returns>JSON数据</returns>
        public static string GetGoodsCategoryWithFid()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            string  categoryfid = cache.RetrieveObject("/Mall/MallSetting/GoodsCategoryWithFid") as string;
            if (categoryfid == null)
            {
                categoryfid = "[";

                IDataReader __idatareader = DbProvider.GetInstance().GetGoodsCategoryWithFid();
                if (__idatareader != null)
                {
                    while (__idatareader.Read())
                    {
                        categoryfid += string.Format("{{'fid' : {0}, 'categoryid' : {1}}},",
                            __idatareader["fid"].ToString(),
                            __idatareader["categoryid"].ToString());
                    }
                    __idatareader.Dispose();
                }
                if (categoryfid.EndsWith(","))
                {
                    categoryfid = categoryfid.Substring(0, categoryfid.Length - 1);
                }
                categoryfid += "]";
                cache.AddObject("/Mall/MallSetting/GoodsCategoryWithFid", categoryfid);
            }

            return categoryfid;
        }
        

        /// <summary>
        /// 生成商品分类表的JSON文件
        /// </summary>
        /// <returns>是否写入成功</returns>
        public override bool WriteJsonFile()
        {
            StringBuilder sb_categories = new StringBuilder();
            sb_categories.Append("var cats = ");

            DataTable dt = DbProvider.GetInstance().GetCategoriesTableToJson();
            sb_categories.Append(Utils.DataTableToJSON(dt));

            return base.WriteJsonFile(jsonPath , sb_categories);
        }

        /// <summary>
        /// 获取指定商品分类的parentidlist数据
        /// </summary>
        /// <param name="categoryid">指定的分类id</param>
        /// <returns>parentidlist数据</returns>
        public static string GetParentCategoryList(int categoryid)
        {
            DataTable dt = DbProvider.GetInstance().GetRootCategoryID(categoryid);
            string parentidlist = "";
            if(dt.Rows.Count>0)
            {
                parentidlist = dt.Rows[0]["parentidlist"].ToString().Trim();
            }
            dt.Dispose();
            return parentidlist;
        }

        /// <summary>
        /// 获取指定的商品类型数据(option格式)
        /// </summary>
        /// <returns>商品类型数据</returns>
        public static string GetShopRootCategoryOption()
        {
            StringBuilder sb_category = new StringBuilder();
            DataTable dt = DbProvider.GetInstance().GetRootCategoriesTable();
            foreach (DataRow dr in dt.Rows)
            {
                sb_category.Append("<option value=\"");
                sb_category.Append(dr["categoryid"].ToString());
                sb_category.Append("\">");
                sb_category.Append(dr["categoryname"].ToString().Trim());
                sb_category.Append("</option>");
            }
            dt.Dispose();
            return sb_category.ToString();
        }

        /// <summary>
        /// 获取(根)商品分类信息
        /// </summary>
        /// <returns></returns>
        public static Goodscategoryinfo[] GetShopRootCategory()
        {
            return DTO.GetGoodsCategoryInfoArray(DbProvider.GetInstance().GetRootCategoriesTable());
        }
      
        /// <summary>
        /// 数据转换对象类
        /// </summary>
        public class DTO
        {
            /// <summary>
            /// 获得商品分类信息(DTO)
            /// </summary>
            /// <param name="__idatareader">要转换的数据</param>
            /// <returns>返回商品分类信息</returns>
            public static Goodscategoryinfo GetGoodsCategoryInfo(IDataReader reader)
            {
                if (reader.Read())
                {
                    Goodscategoryinfo goodscategoryinfo = new Goodscategoryinfo();
                    goodscategoryinfo.Categoryid = Convert.ToInt32(reader["categoryid"].ToString());
                    goodscategoryinfo.Parentid = Convert.ToInt32(reader["parentid"].ToString());
                    goodscategoryinfo.Layer = Convert.ToInt16(reader["layer"].ToString());
                    goodscategoryinfo.Parentidlist = reader["parentidlist"].ToString().Trim();
                    goodscategoryinfo.Displayorder = Convert.ToInt16(reader["displayorder"].ToString().Trim());
                    goodscategoryinfo.Categoryname = reader["categoryname"].ToString().Trim();
                    goodscategoryinfo.Haschild = reader["haschild"].ToString() == "True" ? 1 : 0;
                    goodscategoryinfo.Fid = Convert.ToInt32(reader["fid"].ToString());
                    goodscategoryinfo.Pathlist = reader["pathlist"].ToString().Trim().Replace("a><a", "a> &raquo; <a");
                    goodscategoryinfo.Goodscount = Convert.ToInt32(reader["goodscount"].ToString());

                    reader.Close();
                    return goodscategoryinfo;
                }
                return null;
            }

            /// <summary>
            /// 获得商品分类信息(DTO)
            /// </summary>
            /// <param name="dt">要转换的数据表</param>
            /// <returns>返回商品分类信息</returns>
            public static Goodscategoryinfo[] GetGoodsCategoryInfoArray(DataTable dt)
            {
                if (dt == null || dt.Rows.Count == 0)
                    return null;

                Goodscategoryinfo[] goodscategoryinfoarray = new Goodscategoryinfo[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    goodscategoryinfoarray[i] = new Goodscategoryinfo();
                    goodscategoryinfoarray[i].Categoryid = Convert.ToInt32(dt.Rows[i]["categoryid"].ToString());
                    goodscategoryinfoarray[i].Parentid = Convert.ToInt32(dt.Rows[i]["parentid"].ToString());
                    goodscategoryinfoarray[i].Layer = Convert.ToInt32(dt.Rows[i]["layer"].ToString());
                    goodscategoryinfoarray[i].Parentidlist = dt.Rows[i]["parentidlist"].ToString();
                    goodscategoryinfoarray[i].Displayorder = Convert.ToInt16(dt.Rows[i]["displayorder"].ToString().Trim());
                    goodscategoryinfoarray[i].Categoryname = dt.Rows[i]["categoryname"].ToString();
                    goodscategoryinfoarray[i].Haschild = dt.Rows[i]["haschild"].ToString() == "True" ? 1 : 0;
                    goodscategoryinfoarray[i].Fid = Convert.ToInt32(dt.Rows[i]["fid"].ToString());
                    goodscategoryinfoarray[i].Pathlist = dt.Rows[i]["pathlist"].ToString().Replace("a><a", "a> &raquo; <a");
                    goodscategoryinfoarray[i].Goodscount = Convert.ToInt32(dt.Rows[i]["goodscount"].ToString());
                }
                dt.Dispose();
                return goodscategoryinfoarray;
            }
        }
    }

}
