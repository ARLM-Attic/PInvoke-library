using System;
using System.Data;
using System.Text;
using System.Xml;

using Discuz.Common;
using Discuz.Entity;
using Discuz.Plugin.Mall;

namespace Discuz.Aggregation
{
    /// <summary>
    /// 商品聚合数据类
    /// </summary>
    public class GoodsAggregationData : AggregationData
    {

        #region 得到商品列表

        /// <summary>
        /// 获取聚合页面商品列表信息
        /// </summary>
        /// <param name="type">商品类型(recommend :仅返回推荐商品[商城模式下可用]  , quality_new: 仅返回全新(状态)商品,    quality_old:仅返回二手(状态)商品</param>
        /// <param name="orderby">排序字段(viewcount:按浏览量排序, hotgoods:按商品交易量排序, newgoods:按发布商品时间排序)</param>
        /// <param name="categoryid">商品分类id</param>
        /// <param name="topnumber">获取主题数量</param>
        /// <returns></returns>
        /// <returns></returns>
        public GoodsinfoCollection GetGoodsList(string condition, string orderby, int categoryid, int topnumber)
        {
            if (Utils.StrIsNullOrEmpty(orderby))
            {
                orderby = "goodsid";
            }

            if (Utils.StrIsNullOrEmpty(orderby))
            {
                condition = "";
            }
            else
            {
                condition = condition.ToLower().Trim();
            }

            string cachekey = "/Aggregation/Goods/Goods_" + categoryid + "_" + condition + "_" + orderby + "_List";

            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();

            GoodsinfoCollection __goodsList = cache.RetrieveObject(cachekey) as GoodsinfoCollection;
            if (__goodsList != null)
            {
                return __goodsList;
            }
            else
            {
                switch (condition)
                {
                    case "recommend"://推荐商品
                        {
                            condition = MallPluginProvider.GetInstance().GetGoodsRecommendCondition((int)Discuz.Plugin.Mall.MallPluginBase.OperaCode.MorethanOrEqual, 1);
                            break;
                        }
                   
                    case "quality_new"://全新(状态)商品
                        {
                            condition = MallPluginProvider.GetInstance().GetGoodsQualityCondition((int)Discuz.Plugin.Mall.MallPluginBase.OperaCode.MorethanOrEqual, 1);
                            break;
                        }
                    case "quality_old"://二手(状态)商品
                        {
                            condition = MallPluginProvider.GetInstance().GetGoodsQualityCondition((int)Discuz.Plugin.Mall.MallPluginBase.OperaCode.MorethanOrEqual, 2);
                            break;
                        }
                }

                switch (orderby)
                {
                    case "newgoods"://新发布的商品
                        {
                            orderby = "goodsid";
                            break;
                        }

                    case "viewcount"://按浏览量排序
                        {
                            orderby = "viewcount";
                            break;
                        }
                    case "hotgoods"://热门商品
                        {
                            break;
                        }
                }

                if (orderby == "hotgoods")//热门商品
                {
                    condition = MallPluginProvider.GetInstance().GetGoodsCloseCondition((int)Discuz.Plugin.Mall.MallPluginBase.OperaCode.Equal, 0);
                    condition += MallPluginProvider.GetInstance().GetGoodsExpirationCondition((int)Discuz.Plugin.Mall.MallPluginBase.OperaCode.LessthanOrEqual, 0);
                    condition += MallPluginProvider.GetInstance().GetGoodsDateLineCondition((int)Discuz.Plugin.Mall.MallPluginBase.OperaCode.MorethanOrEqual, 0);
                    condition += MallPluginProvider.GetInstance().GetGoodsRemainCondition((int)Discuz.Plugin.Mall.MallPluginBase.OperaCode.Morethan, 0);
                    condition += MallPluginProvider.GetInstance().GetGoodsDisplayCondition((int)Discuz.Plugin.Mall.MallPluginBase.OperaCode.MorethanOrEqual, 0);
                    __goodsList = MallPluginProvider.GetInstance().GetHotGoods(360, categoryid, topnumber, condition);
                }
                else
                {
                    if (categoryid > 0)
                    {
                        __goodsList = MallPluginProvider.GetInstance().GetGoodsInfoList(categoryid, topnumber, 1, condition, orderby, 1);
                    }
                    else 
                    {
                        __goodsList = MallPluginProvider.GetInstance().GetGoodsInfoList(topnumber, 1, condition, orderby, 1);
                    }
                }

                //声明新的缓存策略接口
                Discuz.Cache.ICacheStrategy ics = new Discuz.Cache.DefaultCacheStrategy();
                ics.TimeOut = 5;
                cache.LoadCacheStrategy(ics);
                cache.AddObject(cachekey, __goodsList);
                cache.LoadDefaultCacheStrategy();
            }

            return __goodsList;
        }



        #endregion

    }
}
