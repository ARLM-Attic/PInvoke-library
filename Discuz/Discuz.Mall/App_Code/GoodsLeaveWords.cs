using System;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

using Discuz.Common;
using Discuz.Config;
using Discuz.Mall.Data;
using Discuz.Entity;
using Discuz.Cache;
using Discuz.Forum;

namespace Discuz.Mall
{
    /// <summary>
    /// 商品留言管理操作类
    /// </summary>
    public class GoodsLeaveWords
    {
        /// <summary>
        /// 获取指定商品交易日志id的留言信息
        /// </summary>
        /// <param name="goodstradelogid">商品交易日志id</param>
        /// <returns></returns>
        public static GoodsleavewordinfoCollection GetLeaveWordList(int goodstradelogid)
        {
            return DTO.GetGoodsLeaveWordInfoList(DbProvider.GetInstance().GetGoodsLeaveWordListByTradeLogId(goodstradelogid));
        }

        /// <summary>
        /// 获取指定留言id的留言信息
        /// </summary>
        /// <param name="id">留言id</param>
        /// <returns></returns>
        public static Goodsleavewordinfo GetGoodsLeaveWordById(int id)
        {
            return DTO.GetGoodsLeaveWordInfo(DbProvider.GetInstance().GetGoodsLeaveWordById(id));
        }

        /// <summary>
        /// 创建留言
        /// </summary>
        /// <param name="goodsleavewordinfo">要创建的留言信息</param>
        /// <returns></returns>
        public static int CreateLeaveWord(Goodsleavewordinfo goodsleavewordinfo)
        {
            goodsleavewordinfo.Postdatetime = DateTime.Now;
            goodsleavewordinfo.Usesig = 0;
            goodsleavewordinfo.Invisible = 0;
            goodsleavewordinfo.Htmlon = 0;
            goodsleavewordinfo.Smileyoff = 1;
            goodsleavewordinfo.Parseurloff = 1;
            goodsleavewordinfo.Bbcodeoff = 1;
            return DbProvider.GetInstance().CreateGoodsLeaveWord(goodsleavewordinfo);
       }

        /// <summary>
        /// 删除指定留言id的留言信息
        /// </summary>
        /// <param name="id">留言id</param>
        /// <param name="userid">当前留言的提交人</param>
        /// <param name="selleruid">当前商品的卖家</param>
        /// <returns></returns>
        public static bool DeleteLeaveWordById(int id, int userid, int selleruid, int useradminid)
        { 
            //删除留言的操作
            Goodsleavewordinfo goodsleaveword = GetGoodsLeaveWordById(id);

            //当为管理组身份 或 留言的uid与当前用户相同时
            if (useradminid == 1 || (goodsleaveword != null && goodsleaveword.Uid == userid) || selleruid == userid)
            {
                return DbProvider.GetInstance().DeleteGoodsLeaveWordById(id);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 更新指定的留言信息
        /// </summary>
        /// <param name="goodsleavewordinfo">要更新的留言信息</param>
        /// <returns></returns>
        public static bool UpdateLeaveWord(Goodsleavewordinfo goodsleavewordinfo)
        {
            return DbProvider.GetInstance().UpdateGoodsLeaveWord(goodsleavewordinfo);
        }

        /// <summary>
        /// 获取指定分类和条件下的商品列表集合
        /// </summary>
        /// <param name="categoryid">商品分类</param>
        /// <param name="pagesize">页面大小</param>
        /// <param name="pageindex">当前页</param>
        /// <param name="condition">条件</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="ascdesc">排序方式(0:升序, 1:降序)</param>
        /// <returns></returns>
        public static GoodsleavewordinfoCollection GetGoodsLeaveWord(int goodsid, int pagesize, int pageindex, string orderby, int ascdesc)
        {
            GoodsleavewordinfoCollection coll = new GoodsleavewordinfoCollection();

            if (pageindex <= 0)
            {
                return coll;
            }

            return DTO.GetGoodsLeaveWordInfoList(DbProvider.GetInstance().GetGoodsLeaveWordByGid(goodsid, pagesize, pageindex, orderby, ascdesc));
        }


        /// <summary>
        /// 获取指定商品的留言
        /// </summary>
        /// <param name="categoryid">分类id</param>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        public static int GetGoodsLeaveWordCount(int goodsid)
        {
            return DbProvider.GetInstance().GetGoodsLeaveWordCountByGid(goodsid);
        }

        /// <summary>
        /// 获取指定商品的交易日志JSON数据
        /// </summary>
        /// <param name="goodsid">指定商品</param>
        /// <param name="pagesize">页面大小</param>
        /// <param name="pageindex">当前页面</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="ascdesc">排序方式</param>
        /// <returns></returns>
        public static StringBuilder GetLeaveWordJson(int goodsid, int pagesize, int pageindex, string orderby, int ascdesc)
        {
            StringBuilder __leavewordjson = new StringBuilder();
            __leavewordjson.Append("[");
            foreach (Goodsleavewordinfo __goodsleavewordinfo in GetGoodsLeaveWord(goodsid, pagesize, pageindex, orderby, ascdesc))
            {
                __goodsleavewordinfo.Message = ParseSmilies(__goodsleavewordinfo.Message);
                // __goodsleavewordinfo.Message = Utils.HtmlEncode(__goodsleavewordinfo.Message);

                __leavewordjson.Append(string.Format("{{'id' : {0}, 'isbuyer' : {1}, 'uid' : {2}, 'username' : '{3}', 'postdatetime' : '{4}', 'message' : '{5}'}},",
                                __goodsleavewordinfo.Id,
                                __goodsleavewordinfo.Isbuyer == 1 ? "true": "false",
                                __goodsleavewordinfo.Uid,
                                __goodsleavewordinfo.Username,
                                __goodsleavewordinfo.Postdatetime.ToString("yyyy-MM-dd"),
                                __goodsleavewordinfo.Message.Replace("\r\n", "<br />")));
            }
            if (__leavewordjson.ToString().EndsWith(","))
            {
                __leavewordjson.Remove(__leavewordjson.Length - 1, 1);
            }
            __leavewordjson.Append("]");

            return __leavewordjson;
        }

        /// <summary>
        /// 获取指定商品的交易日志JSON数据
        /// </summary>
        /// <param name="goodsid">指定商品</param>
        /// <param name="pagesize">页面大小</param>
        /// <param name="pageindex">当前页面</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="ascdesc">排序方式</param>
        /// <returns></returns>
        public static StringBuilder GetLeaveWordJson(int leavewordid)
        {
            StringBuilder __leavewordjson = new StringBuilder();
            __leavewordjson.Append("[");

            if (leavewordid <= 0)
            {
                __leavewordjson.Append("{{'id' : 0, 'isbuyer' : 0, 'uid' : 0, 'username' : '', 'postdatetime' : '', 'message' : ''}}");
            }
            else
            {
                Goodsleavewordinfo goodsleavewordinfo = GoodsLeaveWords.GetGoodsLeaveWordById(leavewordid);

                if (goodsleavewordinfo == null || goodsleavewordinfo.Id <= 0)
                {
                    __leavewordjson.Append("{{'id' : 0, 'isbuyer' : 0, 'uid' : 0, 'username' : '', 'postdatetime' : '', 'message' : ''}}");
                }
                else
                {
                    __leavewordjson.Append(string.Format("{{'id' : {0}, 'isbuyer' : {1}, 'uid' : {2}, 'username' : '{3}', 'postdatetime' : '{4}', 'message' : '{5}'}}",
                                    goodsleavewordinfo.Id,
                                    goodsleavewordinfo.Isbuyer == 1 ? "true" : "false",
                                    goodsleavewordinfo.Uid,
                                    goodsleavewordinfo.Username,
                                    goodsleavewordinfo.Postdatetime.ToString("yyyy-MM-dd"),
                                    goodsleavewordinfo.Message.Replace("\r\n", "<br />")));
                }
            }
            __leavewordjson.Append("]");
            return __leavewordjson;
        }

        /// <summary>
        /// 转换表情
        /// </summary>
        /// <param name="sDetail">内容</param>
        /// <returns>帖子内容</returns>
        private static string ParseSmilies(string sDetail)
        {
            RegexOptions options = RegexOptions.IgnoreCase;

            SmiliesInfo[] smiliesinfo = Smilies.GetSmiliesListWithInfo(); //表情数组
            int smiliesmax = GeneralConfigs.GetConfig().Smiliesmax;
            if (smiliesinfo == null)
                return sDetail;

            string smilieformatstr = "[smilie]{0}editor/images/smilies/{1}[/smilie]";
            for (int i = 0; i < Smilies.regexSmile.Length; i++)
            {
                if (smiliesmax > 0)
                {
                    sDetail = Smilies.regexSmile[i].Replace(sDetail, string.Format(smilieformatstr, BaseConfigs.GetForumPath, smiliesinfo[i].Url), smiliesmax);
                }
                else
                {
                    sDetail = Smilies.regexSmile[i].Replace(sDetail, string.Format(smilieformatstr, BaseConfigs.GetForumPath, smiliesinfo[i].Url));
                }
            }
            return Regex.Replace(sDetail, @"\[smilie\](.+?)\[\/smilie\]", "<img src=\"$1\" />", options);
        }

        /// <summary>
        /// 数据转换对象类
        /// </summary>
        public class DTO
        {
            /// <summary>
            /// 获得商品留言信息(DTO)
            /// </summary>
            /// <param name="__idatareader">要转换的数据</param>
            /// <returns>返回商品留言信息</returns>
            public static Goodsleavewordinfo GetGoodsLeaveWordInfo(IDataReader reader)
            {
                if (reader.Read())
                {

                    Goodsleavewordinfo goodsleavewordinfo = new Goodsleavewordinfo();
                    goodsleavewordinfo.Id = Convert.ToInt32(reader["id"].ToString());
                    goodsleavewordinfo.Goodsid = Convert.ToInt32(reader["goodsid"].ToString());
                    goodsleavewordinfo.Tradelogid = Convert.ToInt32(reader["tradelogid"].ToString());
                    goodsleavewordinfo.Isbuyer = Convert.ToInt16(reader["isbuyer"].ToString());
                    goodsleavewordinfo.Uid = Convert.ToInt32(reader["uid"].ToString());
                    goodsleavewordinfo.Username = reader["username"].ToString().Trim();
                    goodsleavewordinfo.Message = reader["message"].ToString().Trim();
                    goodsleavewordinfo.Invisible = Convert.ToInt32(reader["invisible"].ToString());
                    goodsleavewordinfo.Ip = reader["ip"].ToString().Trim();
                    goodsleavewordinfo.Usesig = Convert.ToInt32(reader["usesig"].ToString());
                    goodsleavewordinfo.Htmlon = Convert.ToInt32(reader["htmlon"].ToString());
                    goodsleavewordinfo.Smileyoff = Convert.ToInt32(reader["smileyoff"].ToString());
                    goodsleavewordinfo.Parseurloff = Convert.ToInt32(reader["parseurloff"].ToString());
                    goodsleavewordinfo.Bbcodeoff = Convert.ToInt32(reader["bbcodeoff"].ToString());
                    goodsleavewordinfo.Postdatetime = Convert.ToDateTime(reader["postdatetime"].ToString());

                    reader.Close();
                    return goodsleavewordinfo;
                }
                return null;
            }

            /// <summary>
            /// 获得商品留言信息(DTO)
            /// </summary>
            /// <param name="__idatareader">要转换的数据</param>
            /// <returns>返回商品留言信息</returns>
            public static GoodsleavewordinfoCollection GetGoodsLeaveWordInfoList(IDataReader reader)
            {
                GoodsleavewordinfoCollection goodsleavewordinfocoll = new GoodsleavewordinfoCollection();

                while (reader.Read())
                {
                    Goodsleavewordinfo goodsleavewordinfo = new Goodsleavewordinfo();
                    goodsleavewordinfo.Id = Convert.ToInt32(reader["id"].ToString());
                    goodsleavewordinfo.Goodsid = Convert.ToInt32(reader["goodsid"].ToString());
                    goodsleavewordinfo.Tradelogid = Convert.ToInt32(reader["tradelogid"].ToString());
                    goodsleavewordinfo.Isbuyer = Convert.ToInt16(reader["isbuyer"].ToString());
                    goodsleavewordinfo.Uid = Convert.ToInt32(reader["uid"].ToString());
                    goodsleavewordinfo.Username = reader["username"].ToString().Trim();
                    goodsleavewordinfo.Message = reader["message"].ToString().Trim();
                    goodsleavewordinfo.Invisible = Convert.ToInt32(reader["invisible"].ToString());
                    goodsleavewordinfo.Ip = reader["ip"].ToString().Trim();
                    goodsleavewordinfo.Usesig = Convert.ToInt32(reader["usesig"].ToString());
                    goodsleavewordinfo.Htmlon = Convert.ToInt32(reader["htmlon"].ToString());
                    goodsleavewordinfo.Smileyoff = Convert.ToInt32(reader["smileyoff"].ToString());
                    goodsleavewordinfo.Parseurloff = Convert.ToInt32(reader["parseurloff"].ToString());
                    goodsleavewordinfo.Bbcodeoff = Convert.ToInt32(reader["bbcodeoff"].ToString());
                    goodsleavewordinfo.Postdatetime = Convert.ToDateTime(reader["postdatetime"].ToString());

                    goodsleavewordinfocoll.Add(goodsleavewordinfo);
                }
                reader.Close();

                return goodsleavewordinfocoll;
            }


            /// <summary>
            /// 获得商品留言信息(DTO)
            /// </summary>
            /// <param name="dt">要转换的数据表</param>
            /// <returns>返回商品留言信息</returns>
            public static Goodsleavewordinfo[] GetGoodsLeaveWordInfoArray(DataTable dt)
            {
                if (dt == null || dt.Rows.Count == 0)
                    return null;

                Goodsleavewordinfo[] goodsleavewordinfoarray = new Goodsleavewordinfo[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    goodsleavewordinfoarray[i] = new Goodsleavewordinfo();
                    goodsleavewordinfoarray[i].Id = Convert.ToInt32(dt.Rows[i]["id"].ToString());
                    goodsleavewordinfoarray[i].Goodsid = Convert.ToInt32(dt.Rows[i]["goodsid"].ToString());
                    goodsleavewordinfoarray[i].Tradelogid = Convert.ToInt32(dt.Rows[i]["tradelogid"].ToString());
                    goodsleavewordinfoarray[i].Isbuyer = Convert.ToInt32(dt.Rows[i]["isbuyer"].ToString());
                    goodsleavewordinfoarray[i].Uid = Convert.ToInt32(dt.Rows[i]["uid"].ToString());
                    goodsleavewordinfoarray[i].Username = dt.Rows[i]["username"].ToString();
                    goodsleavewordinfoarray[i].Message = dt.Rows[i]["message"].ToString();
                    goodsleavewordinfoarray[i].Invisible = Convert.ToInt32(dt.Rows[i]["invisible"].ToString());
                    goodsleavewordinfoarray[i].Ip = dt.Rows[i]["ip"].ToString();
                    goodsleavewordinfoarray[i].Usesig = Convert.ToInt32(dt.Rows[i]["usesig"].ToString());
                    goodsleavewordinfoarray[i].Htmlon = Convert.ToInt32(dt.Rows[i]["htmlon"].ToString());
                    goodsleavewordinfoarray[i].Smileyoff = Convert.ToInt32(dt.Rows[i]["smileyoff"].ToString());
                    goodsleavewordinfoarray[i].Parseurloff = Convert.ToInt32(dt.Rows[i]["parseurloff"].ToString());
                    goodsleavewordinfoarray[i].Bbcodeoff = Convert.ToInt32(dt.Rows[i]["bbcodeoff"].ToString());
                    goodsleavewordinfoarray[i].Postdatetime = Convert.ToDateTime(dt.Rows[i]["postdatetime"].ToString());

                }
                dt.Dispose();
                return goodsleavewordinfoarray;
            }
        }
    }
}
