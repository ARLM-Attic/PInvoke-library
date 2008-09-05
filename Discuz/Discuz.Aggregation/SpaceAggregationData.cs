using System;
using System.Data;
using System.Text;
using System.Xml;

using Discuz.Cache;
using Discuz.Common;
using Discuz.Data;
using Discuz.Config;
using Discuz.Entity;
using Discuz.Plugin.Space;

namespace Discuz.Aggregation
{

    /// <summary>
    /// 空间聚合数据类
    /// </summary>
    public class SpaceAggregationData : AggregationData
    {
        /// <summary>
        /// 图片轮换字符串
        /// </summary>
        private static StringBuilder __spaceRotatepic = null;

        /// <summary>
        /// 绑定到聚合首页的日志列表
        /// </summary>
        private static SpaceShortPostInfo[] __spacePostListForWebSite;

        /// <summary>
        /// 绑定到聚合空间首页的日志列表
        /// </summary>
        private static SpaceShortPostInfo[] __spacePostListForSpaceIndex;

        /// <summary>
        /// 绑定到聚合首页的空间列表
        /// </summary>
        private static SpaceConfigInfoExt[] __spaceConfigInfosForWebSite;

        /// <summary>
        /// 绑定到聚合空间首页的空间列表
        /// </summary>
        private static SpaceConfigInfoExt[] __spaceConfigInfosForSpaceIndex;

       
        /// <summary>
        /// 锁变量
        /// </summary>
        private static object lockHelper = new object();


        /// <summary>
        /// 从XML中检索出指定的轮换广告信息
        /// </summary>
        /// <returns></returns>
        public new string GetRotatePicData()
        {
            //当文件未被修改时将直接返回相关记录
            if (__spaceRotatepic != null)
            {
                return __spaceRotatepic.ToString();
            }
            __spaceRotatepic = new StringBuilder();
            __spaceRotatepic.Append(base.GetRotatePicStr("Spaceindex"));

            return __spaceRotatepic.ToString();
        }

  
        #region 从XML中检查出指定的Space信息


        /// <summary>
        /// 清空数据绑定
        /// </summary>
        public override void ClearDataBind()
        {
            __spacePostListForWebSite = null;

            __spacePostListForSpaceIndex = null;

            __spaceRotatepic = null;

            __spaceConfigInfosForWebSite = null;

            __spaceConfigInfosForSpaceIndex = null;
        }


     
        /// <summary>
        /// 从XML文件中获得数据并初始化空间对象数组
        /// </summary>
        /// <param name="showtype"></param>
        /// <returns></returns>
        public SpaceConfigInfoExt[] GetSpaceListFromFile(string nodename)
        {
            //当记录不为空时则直接返回数据
            SpaceConfigInfoExt[] __spaceConfigInfos = null;

            if (nodename == "Website")
            {
                __spaceConfigInfos = __spaceConfigInfosForWebSite;
            }
            else 
            {
                __spaceConfigInfos = __spaceConfigInfosForSpaceIndex;
            }

             if (__spaceConfigInfos != null)
            {
                return __spaceConfigInfos;
            }


            XmlNodeList xmlnodelist = __xmlDoc.DocumentElement.SelectNodes("/Aggregationinfo/Aggregationpage/" + nodename + "/" +nodename + "_spacelist/Space");
            __spaceConfigInfos = new SpaceConfigInfoExt[xmlnodelist.Count];
            int rowcount = 0;

            foreach (XmlNode xmlnode in xmlnodelist)
            {
                    __spaceConfigInfos[rowcount] = new SpaceConfigInfoExt();
                    __spaceConfigInfos[rowcount].Spaceid = (__xmlDoc.GetSingleNodeValue(xmlnode, "spaceid") == null) ? 0 : Convert.ToInt32(__xmlDoc.GetSingleNodeValue(xmlnode, "spaceid"));
                    __spaceConfigInfos[rowcount].Userid = (__xmlDoc.GetSingleNodeValue(xmlnode, "userid") == null) ? 0 : Convert.ToInt32(__xmlDoc.GetSingleNodeValue(xmlnode, "userid").Trim());
                    __spaceConfigInfos[rowcount].Spacetitle = (__xmlDoc.GetSingleNodeValue(xmlnode, "title") == null) ? "" : Utils.RemoveHtml(__xmlDoc.GetSingleNodeValue(xmlnode, "title").Trim());
                    __spaceConfigInfos[rowcount].Description = (__xmlDoc.GetSingleNodeValue(xmlnode, "description") == null) ? "" : Utils.RemoveHtml(__xmlDoc.GetSingleNodeValue(xmlnode, "description").Trim());
                    __spaceConfigInfos[rowcount].Postcount = (__xmlDoc.GetSingleNodeValue(xmlnode, "postcount") == null) ? 0 : Convert.ToInt32(__xmlDoc.GetSingleNodeValue(xmlnode, "postcount"));
                    __spaceConfigInfos[rowcount].Spacepic = (__xmlDoc.GetSingleNodeValue(xmlnode, "pic") == null) ? "" : __xmlDoc.GetSingleNodeValue(xmlnode, "pic").Trim();
                    __spaceConfigInfos[rowcount].Albumcount = (__xmlDoc.GetSingleNodeValue(xmlnode, "albumcount") == null) ? 0 : Convert.ToInt32(__xmlDoc.GetSingleNodeValue(xmlnode, "albumcount"));
                    __spaceConfigInfos[rowcount].Postid = (__xmlDoc.GetSingleNodeValue(xmlnode, "postid") == null) ? 0 : Convert.ToInt32(__xmlDoc.GetSingleNodeValue(xmlnode, "postid"));
                    __spaceConfigInfos[rowcount].Posttitle = (__xmlDoc.GetSingleNodeValue(xmlnode, "posttitle") == null) ? "" : Utils.RemoveHtml(__xmlDoc.GetSingleNodeValue(xmlnode, "posttitle"));
                    rowcount++;
            }

            if (nodename == "Website")
            {
                __spaceConfigInfosForWebSite = __spaceConfigInfos;
            }
            else
            {
                __spaceConfigInfosForSpaceIndex = __spaceConfigInfos ;
            }

            return __spaceConfigInfos;
        }

        #endregion


        #region 得到空间日志数组

        /// <summary>
        /// 得到空间日志数组
        /// </summary>
        /// <param name="nodename">节点名称</param>
        /// <returns></returns>
        public SpaceShortPostInfo[] GetSpacePostList(string nodename)
        {
            SpaceShortPostInfo[] __spacePostList = null;

            if (nodename == "Website")
            {
                __spacePostList = __spacePostListForWebSite;
            }
            else
            {
                __spacePostList = __spacePostListForSpaceIndex;
            }

            //当记录不为空时则直接反回数据
            if (__spacePostList != null)
            {
                return __spacePostList;
            }

            
            XmlNodeList xmlnodelist = __xmlDoc.DocumentElement.SelectNodes("/Aggregationinfo/Aggregationpage/" + nodename + "/" + nodename + "_spacearticlelist/Article");
            __spacePostList = new SpaceShortPostInfo[xmlnodelist.Count];
            int rowcount = 0;

            foreach (XmlNode xmlnode in xmlnodelist)
            {
                    __spacePostList[rowcount] = new SpaceShortPostInfo();
                    __spacePostList[rowcount].Postid = (__xmlDoc.GetSingleNodeValue(xmlnode, "postid") == null) ? 0 : Convert.ToInt32(__xmlDoc.GetSingleNodeValue(xmlnode, "postid"));
                    __spacePostList[rowcount].Author = (__xmlDoc.GetSingleNodeValue(xmlnode, "author") == null) ? "" : __xmlDoc.GetSingleNodeValue(xmlnode, "author").Trim();
                    __spacePostList[rowcount].Uid = (__xmlDoc.GetSingleNodeValue(xmlnode, "uid") == null) ? 0 : Convert.ToInt32(__xmlDoc.GetSingleNodeValue(xmlnode, "uid").Trim());
                    __spacePostList[rowcount].Postdatetime = (__xmlDoc.GetSingleNodeValue(xmlnode, "postdatetime") == null) ? DateTime.Now : Convert.ToDateTime(__xmlDoc.GetSingleNodeValue(xmlnode, "postdatetime").Trim());
                    __spacePostList[rowcount].Title = (__xmlDoc.GetSingleNodeValue(xmlnode, "title") == null) ? "" : Utils.RemoveHtml(__xmlDoc.GetSingleNodeValue(xmlnode, "title"));
                    __spacePostList[rowcount].Views = (__xmlDoc.GetSingleNodeValue(xmlnode, "views") == null) ? 0 : Convert.ToInt32(__xmlDoc.GetSingleNodeValue(xmlnode, "views").Trim());
                    __spacePostList[rowcount].Commentcount = (__xmlDoc.GetSingleNodeValue(xmlnode, "commentcount") == null) ? 0 : Convert.ToInt32(__xmlDoc.GetSingleNodeValue(xmlnode, "commentcount"));
                    rowcount++;
            }


            if (nodename == "Website")
            {
                __spacePostListForWebSite = __spacePostList;
            }
            else
            {
                __spacePostListForSpaceIndex = __spacePostList;
            }

            return __spacePostList;
        }


        #endregion



        #region 得到最近更新的空间列表

        /// <summary>
        /// 得到最近更新的空间列表
        /// </summary>
        /// <param name="count">返回的记录数</param>
        /// <returns></returns>
        public DataTable GetRecentUpdateSpaceList(int count)
        {
            DNTCache cache = DNTCache.GetCacheService();

            //声明新的缓存策略接口
            Discuz.Cache.ICacheStrategy ics = new AggregationCacheStrategy();
            ics.TimeOut = AggregationConfig.GetConfig().RecentUpdateSpaceAggregationListTimeout;
            cache.LoadCacheStrategy(ics);

            DataTable __recentUpdateSpaceList = cache.RetrieveObject("/Space/RecentUpdateSpaceAggregationList") as DataTable;

            if (__recentUpdateSpaceList == null)
            {
                __recentUpdateSpaceList = SpacePluginProvider.GetInstance().GetWebSiteAggRecentUpdateSpaceList(count);

                cache.AddObject("/Space/RecentUpdateSpaceAggregationList", __recentUpdateSpaceList);
            }
            cache.LoadDefaultCacheStrategy();
            return __recentUpdateSpaceList;
        }

        #endregion



        #region 得到相关TOP的空间列表

        /// <summary>
        /// 得到相关TOP的空间列表
        /// </summary>
        /// <param name="orderby">排序字段</param>
        /// <param name="topnumber">返回的记录数</param>
        /// <returns></returns>
        public DataTable GetTopSpaceList(string orderby,int topnumber)
        {
            DataTable __topSpaceList = SpacePluginProvider.GetInstance().GetWebSiteAggTopSpaceList(orderby, topnumber);
            __topSpaceList.Columns.Add("postid", typeof(String));
            __topSpaceList.Columns.Add("posttitle", typeof(String));
            foreach (DataRow dr in __topSpaceList.Rows)
            {
                string[] postinfo = SpacePluginProvider.GetInstance().GetSpaceLastPostInfo(int.Parse(dr["userid"].ToString()));
                dr["postid"] = postinfo[0];
                dr["posttitle"] = Utils.RemoveHtml(postinfo[1].ToString());
            }
            return __topSpaceList;
        }


        /// <summary>
        /// 从缓存中得到指定排序的空间列表
        /// </summary>
        /// <param name="orderby">排序字段</param>
        /// <returns></returns>
        public DataTable GetTopSpaceListFromCache(string orderby)
        {
            DNTCache cache = DNTCache.GetCacheService();
            DataTable __topSpaceList = cache.RetrieveObject("/Space/Top" + orderby + "SpaceList") as DataTable;

            if (__topSpaceList == null)
            {
                //声明新的缓存策略接口
                Discuz.Cache.ICacheStrategy ics = new AggregationCacheStrategy();
                cache.LoadCacheStrategy(ics);
                switch (orderby)
                {
                    //case "postcount":
                    //    {
                    //        ics.TimeOut = AggregationConfig.GetConfig().ToppostcountSpaceListTimeout;
                    //        __topSpaceList = GetTopSpaceList(orderby, AggregationConfig.GetConfig().ToppostcountSpaceListCount);
                    //        break;
                    //    }
                    case "commentcount":
                        {
                            ics.TimeOut = AggregationConfig.GetConfig().TopcommentcountPostListTimeout;
                            __topSpaceList = GetTopSpaceList(orderby, AggregationConfig.GetConfig().TopcommentcountSpaceListCount);
                            break;
                        }
                    case "visitedtimes":
                        {
                            ics.TimeOut = AggregationConfig.GetConfig().TopvisitedtimesSpaceListTimeout;
                            __topSpaceList = GetTopSpaceList(orderby, AggregationConfig.GetConfig().TopvisitedtimesSpaceListCount);
                            break;
                        }
                }
                cache.AddObject("/Space/Top" + orderby + "SpaceList", __topSpaceList);
            }
            cache.LoadDefaultCacheStrategy();
            return __topSpaceList;
        }


        #endregion


        #region 得到相关TOP的空间日志列表

        /// <summary>
        /// 得到指定排序和数量的空间日志列表
        /// </summary>
        /// <param name="orderby">排序字段</param>
        /// <param name="topnumber">返回的记录数</param>
        /// <returns></returns>
        public DataTable GetTopSpacePostList(string orderby, int topnumber)
        {
            return SpacePluginProvider.GetInstance().GetWebSiteAggTopSpacePostList(orderby, topnumber);
        }


        /// <summary>
        /// 从缓存中获得指定排序的空间日志列表
        /// </summary>
        /// <param name="orderby">排序字段</param>
        /// <returns></returns>
        public DataTable GetTopSpacePostListFromCache(string orderby)
        {
            lock (lockHelper)
            {

                DNTCache cache = DNTCache.GetCacheService();
                DataTable __topSpacePostList = cache.RetrieveObject("/Space/Top" + orderby + "PostList") as DataTable;

                if (__topSpacePostList == null)
                {
                    //声明新的缓存策略接口
                    Discuz.Cache.ICacheStrategy ics = new AggregationCacheStrategy();
                    cache.LoadCacheStrategy(ics);
                    switch (orderby)
                    {
                        case "commentcount":
                            {
                                ics.TimeOut = AggregationConfig.GetConfig().TopcommentcountPostListTimeout;
                                __topSpacePostList = GetTopSpacePostList(orderby, AggregationConfig.GetConfig().TopcommentcountPostListCount);
                                break;
                            }
                        case "views":
                            {
                                ics.TimeOut = AggregationConfig.GetConfig().TopviewsPostListTimeout;
                                __topSpacePostList = GetTopSpacePostList(orderby, AggregationConfig.GetConfig().TopviewsPostListCount);
                                break;
                            }
                        default:
                            {
                                orderby = "commentcount";
                                __topSpacePostList = GetTopSpacePostList(orderby, 1);
                                __topSpacePostList.Rows.Clear();
                                break;
                            }
                    }

                    try
                    {
                        cache.AddObject("/Space/Top" + orderby + "PostList", __topSpacePostList);
                    }
                    finally
                    {
                        cache.LoadDefaultCacheStrategy();
                    }
                }

                return __topSpacePostList;
            }
        }


        #endregion

        #region 返回指定页数日志列表
        /// <summary>
        /// 返回指定页数日志列表
        /// </summary>
        /// <param name="pageSize">每页的记录数</param>
        /// <param name="currentPage">当前页号</param>
        /// <returns></returns>
        public DataTable SpacePostsList(int pageSize, int currentPage)
        {
            DataTable dt = SpacePluginProvider.GetInstance().GetWebSiteAggSpacePostsList(pageSize, currentPage);
            foreach (DataRow dr in dt.Rows)
            {
                dr["content"] = Utils.RemoveHtml(dr["content"].ToString());
            }
            return dt;
        }

        #endregion


        #region 返回满足条件的日志数

        /// <summary>
        /// 返回满足条件的日志数
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public int GetSpacePostsCount()
        {
            return SpacePluginProvider.GetInstance().GetWebSiteAggSpacePostsCount();
        }

        #endregion


        #region 返回指定数量的最新评论列表

        /// <summary>
        /// 返回指定数量的最新评论列表
        /// </summary>
        /// <param name="topnumber"></param>
        /// <returns></returns>
        public DataTable GetSpaceTopComments(int topnumber)
        {
            if (SpacePluginProvider.GetInstance() == null)
            { 
                return new DataTable();
            }
            return SpacePluginProvider.GetInstance().GetWebSiteAggSpaceTopComments(topnumber);
        }


        /// <summary>
        /// 最新空间评论数
        /// </summary>
        /// <returns></returns>
        public DataTable GetSpaceTopComments()
        {
            DNTCache cache = DNTCache.GetCacheService();

           

            DataTable __topNComments = cache.RetrieveObject("/Space/SpaceTopNewComments") as DataTable;

            if (__topNComments == null)
            {
                __topNComments = GetSpaceTopComments(AggregationConfig.GetConfig().SpaceTopNewCommentsCount);

                //声明新的缓存策略接口
                Discuz.Cache.ICacheStrategy ics = new AggregationCacheStrategy();
                ics.TimeOut = AggregationConfig.GetConfig().SpaceTopNewCommentsTimeout;
                cache.LoadCacheStrategy(ics);
                cache.AddObject("/Space/SpaceTopNewComments", __topNComments);
                cache.LoadDefaultCacheStrategy();
            }
            return __topNComments;
        }

        #endregion

    }
}
