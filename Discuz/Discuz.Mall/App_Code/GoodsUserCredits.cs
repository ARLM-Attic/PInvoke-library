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
    /// 商品用户信用管理操作类
    /// </summary>
    public class GoodsUserCredits
    {

        /// <summary>
        /// 设置用户信用(该方法会在用户进行评价之后调用)
        /// </summary>
        /// <param name="goodsrateinfo">评价信息</param>
        /// <param name="uid">被评价人的uid</param>
        /// <returns></returns>
        public static bool SetUserCredit(Goodsrateinfo goodsrateinfo, int uid)
        {
            //获取被评价人的信用信息
            GoodsusercreditinfoCollection goodsusercreditinfocoll = GetUserCreditList(uid);

            //如果信用表中不存在, 则创建被评价人的信息
            if (goodsusercreditinfocoll.Count == 0)
            {
                //当初始化信息失败时则返回
                if (DbProvider.GetInstance().InitGoodsUserCredit(uid) <= 0)
                {
                    return false;
                }
                //再次获取被评价人的信用信息
                goodsusercreditinfocoll = GetUserCreditList(uid);
            }

            //用于绑定要更新的用户信用
            Goodsusercreditinfo cur_creditinfo = null;
            foreach (Goodsusercreditinfo goodsusercreditinfo in goodsusercreditinfocoll)
            {
                //查找符合条件的用户信用
                if (goodsrateinfo.Uidtype == goodsusercreditinfo.Ratefrom && goodsrateinfo.Ratetype == goodsusercreditinfo.Ratetype)
                {
                    cur_creditinfo = goodsusercreditinfo; break;
                }
            }

            //当不为空, 表示找到了要更新的用户信用信息, 则进行下面的绑定操作
            if (cur_creditinfo != null)
            {
                IDataReader __idatareader = DbProvider.GetInstance().GetGoodsRateCount(uid, goodsrateinfo.Uidtype, goodsrateinfo.Ratetype);

                //绑定新的查询数据
                if (__idatareader.Read())
                {
                    cur_creditinfo.Ratefrom = goodsrateinfo.Uidtype;
                    cur_creditinfo.Ratetype = goodsrateinfo.Ratetype;
                    cur_creditinfo.Oneweek = Convert.ToInt32(__idatareader["oneweek"].ToString());
                    cur_creditinfo.Onemonth = Convert.ToInt32(__idatareader["onemonth"].ToString());
                    cur_creditinfo.Sixmonth = Convert.ToInt32(__idatareader["sixmonth"].ToString());
                    cur_creditinfo.Sixmonthago = Convert.ToInt32(__idatareader["sixmonthago"].ToString());
                    UpdateUserCredit(cur_creditinfo);                    
                }
                __idatareader.Close();
            }

            return true;
        }

        /// <summary>
        /// 获取指定用户id的信用信息(json格式串)
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public static StringBuilder GetUserCreditJsonData(int uid)
        {
            StringBuilder sb_usercreditjson = new StringBuilder();
            sb_usercreditjson.Append("[");

            //获取被评价人的信用信息
            GoodsusercreditinfoCollection goodsusercreditinfocoll = GetUserCreditList(uid);

            //如果信用表中不存在, 则创建被评价人的信息
            if (goodsusercreditinfocoll.Count == 0)
            {
                //当初始化信息失败时则返回
                DbProvider.GetInstance().InitGoodsUserCredit(uid);
                 //再次获取被评价人的信用信息
                goodsusercreditinfocoll = GetUserCreditList(uid);
            }

            foreach (Goodsusercreditinfo __goodsusercreditinfo in goodsusercreditinfocoll)
            {
                sb_usercreditjson.Append(string.Format("{{'id' : {0}, 'uid' : {1}, 'oneweek' : {2}, 'onemonth' : {3}, 'sixmonth' : {4}, 'sixmonthago' : {5}, 'ratefrom' : {6}, 'ratetype' : {7}}},",
                                __goodsusercreditinfo.Id,
                                __goodsusercreditinfo.Uid,
                                __goodsusercreditinfo.Oneweek,
                                __goodsusercreditinfo.Onemonth,
                                __goodsusercreditinfo.Sixmonth,
                                __goodsusercreditinfo.Sixmonthago,
                                __goodsusercreditinfo.Ratefrom,
                                __goodsusercreditinfo.Ratetype
                                ));
            }
            if (sb_usercreditjson.ToString().EndsWith(","))
            {
                sb_usercreditjson.Remove(sb_usercreditjson.Length - 1, 1);
            }
            sb_usercreditjson.Append("]");
            return sb_usercreditjson;
        }

        /// <summary>
        /// 更新用户信用信息
        /// </summary>
        /// <param name="goodsusercreditinfo">要更新的用户信用信息</param>
        /// <returns></returns>
        public static bool UpdateUserCredit(Goodsusercreditinfo goodsusercreditinfo)
        {
            return DbProvider.GetInstance().UpdateGoodsUserCredit(goodsusercreditinfo);
        }

        /// <summary>
        /// 获取指定用户id的信用信息
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        public static GoodsusercreditinfoCollection GetUserCreditList(int userid)
        {
            return DTO.GetGoodsUserCreditList(DbProvider.GetInstance().GetGoodsUserCreditByUid(userid));
        }

        /// <summary>
        /// 获取诚信规则的json数据串
        /// </summary>
        /// <returns></returns>
        public static StringBuilder GetCreditRulesJsonData()
        {
            StringBuilder sb_usercreditjson = new StringBuilder();
            sb_usercreditjson.Append("[");

            IDataReader __idatareader = DbProvider.GetInstance().GetGoodsCreditRules();

            while (__idatareader.Read())
            {
                sb_usercreditjson.Append(string.Format("{{'id' : {0}, 'lowerlimit' : {1}, 'upperlimit' : {2}, 'sellericon' : '{3}', 'buyericon' : '{4}'}},",
                                __idatareader["id"].ToString(),
                                __idatareader["lowerlimit"].ToString(),
                                __idatareader["upperlimit"].ToString(),
                                __idatareader["sellericon"].ToString(),
                                __idatareader["buyericon"].ToString()
                                ));
            }
            __idatareader.Close();
            
            if (sb_usercreditjson.ToString().EndsWith(","))
            {
                sb_usercreditjson.Remove(sb_usercreditjson.Length - 1, 1);
            }
            sb_usercreditjson.Append("]");
            return sb_usercreditjson;
        }


        /// <summary>
        /// 数据转换对象类
        /// </summary>
        public class DTO
        {

            /// <summary>
            /// 获得(商品)用户信用信息(DTO)
            /// </summary>
            /// <param name="__idatareader">要转换的数据</param>
            /// <returns>返回(商品)用户信用信息</returns>
            public static Goodsusercreditinfo GetGoodsUserCreditInfo(IDataReader reader)
            {
                if (reader.Read())
                {
                    Goodsusercreditinfo goodsusercreditsinfo = new Goodsusercreditinfo();
                    goodsusercreditsinfo.Id = Convert.ToInt32(reader["id"].ToString());
                    goodsusercreditsinfo.Uid = Convert.ToInt32(reader["uid"].ToString());
                    goodsusercreditsinfo.Oneweek = Convert.ToInt32(reader["oneweek"].ToString());
                    goodsusercreditsinfo.Onemonth = Convert.ToInt32(reader["onemonth"].ToString());
                    goodsusercreditsinfo.Sixmonth = Convert.ToInt32(reader["sixmonth"].ToString());
                    goodsusercreditsinfo.Sixmonthago = Convert.ToInt32(reader["sixmonthago"].ToString());
                    goodsusercreditsinfo.Ratefrom = Convert.ToInt16(reader["ratefrom"].ToString());
                    goodsusercreditsinfo.Ratetype = Convert.ToInt16(reader["ratetype"].ToString());

                    reader.Close();
                    return goodsusercreditsinfo;
                }
                return null;
            }


            /// <summary>
            /// 获得(商品)用户信用信息(DTO)
            /// </summary>
            /// <param name="__idatareader">要转换的数据</param>
            /// <returns>返回(商品)用户信用信息</returns>
            public static GoodsusercreditinfoCollection GetGoodsUserCreditList(IDataReader reader)
            {
                GoodsusercreditinfoCollection goodsusercreditinfocoll = new GoodsusercreditinfoCollection();

                while (reader.Read())
                {
                    Goodsusercreditinfo goodsusercreditsinfo = new Goodsusercreditinfo();
                    goodsusercreditsinfo.Id = Convert.ToInt32(reader["id"].ToString());
                    goodsusercreditsinfo.Uid = Convert.ToInt32(reader["uid"].ToString());
                    goodsusercreditsinfo.Oneweek = Convert.ToInt32(reader["oneweek"].ToString());
                    goodsusercreditsinfo.Onemonth = Convert.ToInt32(reader["onemonth"].ToString());
                    goodsusercreditsinfo.Sixmonth = Convert.ToInt32(reader["sixmonth"].ToString());
                    goodsusercreditsinfo.Sixmonthago = Convert.ToInt32(reader["sixmonthago"].ToString());
                    goodsusercreditsinfo.Ratefrom = Convert.ToInt16(reader["ratefrom"].ToString());
                    goodsusercreditsinfo.Ratetype = Convert.ToInt16(reader["ratetype"].ToString());

                    goodsusercreditinfocoll.Add(goodsusercreditsinfo);
                }
                reader.Close();
                return goodsusercreditinfocoll;
            }

           
            /// <summary>
            /// 获得(商品)用户信用信息(DTO)
            /// </summary>
            /// <param name="dt">要转换的数据表</param>
            /// <returns>返回(商品)用户信用信息</returns>
            public static Goodsusercreditinfo[] GetGoodsUserCreditArray(DataTable dt)
            {
                if (dt == null || dt.Rows.Count == 0)
                    return null;

                Goodsusercreditinfo[] goodsusercreditsinfoarray = new Goodsusercreditinfo[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    goodsusercreditsinfoarray[i] = new Goodsusercreditinfo();
                    goodsusercreditsinfoarray[i].Id = Convert.ToInt32(dt.Rows[i]["id"].ToString());
                    goodsusercreditsinfoarray[i].Uid = Convert.ToInt32(dt.Rows[i]["uid"].ToString());
                    goodsusercreditsinfoarray[i].Oneweek = Convert.ToInt32(dt.Rows[i]["oneweek"].ToString());
                    goodsusercreditsinfoarray[i].Onemonth = Convert.ToInt32(dt.Rows[i]["onemonth"].ToString());
                    goodsusercreditsinfoarray[i].Sixmonth = Convert.ToInt32(dt.Rows[i]["sixmonth"].ToString());
                    goodsusercreditsinfoarray[i].Sixmonthago = Convert.ToInt32(dt.Rows[i]["sixmonthago"].ToString());
                    goodsusercreditsinfoarray[i].Ratefrom = Convert.ToInt32(dt.Rows[i]["ratefrom"].ToString());
                    goodsusercreditsinfoarray[i].Ratetype = Convert.ToInt32(dt.Rows[i]["ratetype"].ToString());

                }
                dt.Dispose();
                return goodsusercreditsinfoarray;
            }
        }
    }
}
