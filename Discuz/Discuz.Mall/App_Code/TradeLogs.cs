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
    public class TradeLogs
    {
        /// <summary>
        /// 获取交易流水号  
        /// </summary>
        public static string GetOrderID()
        {
            string _out_trade_no;
            //构造订单号 (形如:20080104140009iwGampfQkzFgMZ0yoT)
            _out_trade_no = Discuz.Common.Utils.GetDateTime();
            _out_trade_no = _out_trade_no.Replace("-", "");
            _out_trade_no = _out_trade_no.Replace(":", "");
            _out_trade_no = _out_trade_no.Replace(" ", "");

            string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Random rnd = new Random();
            for (int i = 1; i <= 32; i++)
            {
                _out_trade_no += chars.Substring(rnd.Next(chars.Length), 1);
            }
            return _out_trade_no;
        }

        /// <summary>
        /// 创建商品交易日志
        /// </summary>
        /// <param name="__goodstradelog">要创建的商品交易日志</param>
        /// <returns>创建的商品交易日志id</returns>
        public static int CreateTradeLog(Goodstradeloginfo __goodstradelog)
        {
            //当为支付宝付款方式时,将订单号绑定到tradeno字段
            if (__goodstradelog.Offline == 0)
            {
                __goodstradelog.Tradeno = __goodstradelog.Orderid;
            }

            if (__goodstradelog.Buyermsg.Length > 100)
            {
                __goodstradelog.Buyermsg = __goodstradelog.Buyermsg.Substring(0, 100);
            }

            if (__goodstradelog.Buyercontact.Length > 100)
            {
                __goodstradelog.Buyercontact = __goodstradelog.Buyercontact.Substring(0, 100);
            }

            if (__goodstradelog.Number > 0)
            { 
                //更新商品数量和最近交易信息
                Goodsinfo __goodsinfo = Goods.GetGoodsInfo(__goodstradelog.Goodsid);
                if (__goodsinfo != null && __goodsinfo.Goodsid > 0)
                {
                    //当商品库存变为0(负)库存时
                    if (__goodsinfo.Amount > 0 && (__goodsinfo.Amount - __goodstradelog.Number) <= 0)
                    {
                        DbProvider.GetInstance().UpdateCategoryGoodsCounts(__goodsinfo.Categoryid, __goodsinfo.Parentcategorylist, -1);
                    }

                    __goodsinfo.Totalitems = __goodsinfo.Totalitems + __goodstradelog.Number; //累加总交易量
                    __goodsinfo.Amount = __goodsinfo.Amount - __goodstradelog.Number; //减少当前商品数量
                    __goodsinfo.Tradesum = __goodsinfo.Tradesum + __goodstradelog.Tradesum;  //累加总交易额

                    __goodsinfo.Lastbuyer = __goodstradelog.Buyer;
                    __goodsinfo.Lasttrade = DateTime.Now;

                    Goods.UpdateGoods(__goodsinfo);
                }
            }
            __goodstradelog.Id = DbProvider.GetInstance().CreateGoodsTradeLog(__goodstradelog);

            SendPM(__goodstradelog);

            return __goodstradelog.Id;
        }

        /// <summary>
        /// 更新交易信息
        /// </summary>
        /// <param name="__goodstradelog">要更新的交易信息</param>
        /// <param name="oldstatus">本次更新之前的状态</param>
        /// <returns>是否更新成功</returns>
        public static bool UpdateTradeLog(Goodstradeloginfo __goodstradelog, int oldstatus)
        {
            if (__goodstradelog.Buyermsg.Length > 100)
            {
                __goodstradelog.Buyermsg = __goodstradelog.Buyermsg.Substring(0, 100);
            }

            if (__goodstradelog.Buyercontact.Length > 100)
            {
                __goodstradelog.Buyercontact = __goodstradelog.Buyercontact.Substring(0, 100);
            }

         
            __goodstradelog.Tradesum = __goodstradelog.Number * __goodstradelog.Price + (__goodstradelog.Transportpay == 2 ? __goodstradelog.Transportfee : 0);
 
            //当交易状态发生变化时
            if (__goodstradelog.Status != oldstatus)
            {
                if (__goodstradelog.Number > 0)
                {
                    //获取当前交易的商品信息
                    Goodsinfo __goodsinfo = Goods.GetGoodsInfo(__goodstradelog.Goodsid);


                    //当交易从中途关闭(未完成)状态变为生效(Status: 1为生效, 4为买家已付款等待卖家发货)时更新商品数量)
                    if (oldstatus == 8 && (__goodstradelog.Status == 1 || __goodstradelog.Status == 4))
                    {
                        //当商品库存变为0(负)库存时
                        if (__goodsinfo.Amount > 0 && (__goodsinfo.Amount - __goodstradelog.Number) <= 0)
                        {
                            DbProvider.GetInstance().UpdateCategoryGoodsCounts(__goodsinfo.Categoryid, __goodsinfo.Parentcategorylist, -1);
                        }

                        __goodsinfo.Totalitems = __goodsinfo.Totalitems + __goodstradelog.Number; //累加总交易量
                        __goodsinfo.Amount = __goodsinfo.Amount - __goodstradelog.Number; //减少当前商品数量
                        __goodsinfo.Tradesum = __goodsinfo.Tradesum + __goodstradelog.Tradesum;  //累加总交易额
                    }


                    //当退款成功后(Status = 17, 表示此次交易无效,同时更新商品信息并还原商品数目)
                    //或交易中途关闭,未完成(Status = 8, 更新商品数量)
                    if (__goodstradelog.Status == 17 || __goodstradelog.Status == 8)
                    {
                        //当商品库存从0(负)库存变为有效库存时
                        if (__goodsinfo.Amount <= 0 && (__goodsinfo.Amount + __goodstradelog.Number) > 0)
                        {
                            DbProvider.GetInstance().UpdateCategoryGoodsCounts(__goodsinfo.Categoryid, __goodsinfo.Parentcategorylist, 1);
                        }

                        __goodsinfo.Totalitems = __goodsinfo.Totalitems - __goodstradelog.Number; //减少总交易量
                        __goodsinfo.Amount = __goodsinfo.Amount + __goodstradelog.Number; //还原当前商品数量
                        __goodsinfo.Tradesum = __goodsinfo.Tradesum - __goodstradelog.Tradesum;//减少总交易额
                    }
                  
                    __goodsinfo.Lastbuyer = __goodstradelog.Buyer;
                    __goodsinfo.Lasttrade = DateTime.Now;

                    Goods.UpdateGoods(__goodsinfo);
                }
            }

            return DbProvider.GetInstance().UpdateGoodsTradeLog(__goodstradelog);
        }


        /// <summary>
        /// 更新交易信息
        /// </summary>
        /// <param name="__goodstradelog">要更新的交易信息</param>
        /// <param name="oldstatus">更新之前的状态</param>
        /// <param name="issendpm">更新交易信息成功后, 是否发送短消息</param>
        /// <returns>是否更新成功</returns>
        public static bool UpdateTradeLog(Goodstradeloginfo __goodstradelog, int oldstatus, bool issendpm)
        {
            bool result = UpdateTradeLog(__goodstradelog, oldstatus);
            if (result && issendpm)
            {
                SendPM(__goodstradelog);
            }

            return result;
        }
        
        /// <summary>
        /// 根据交易日志的状态发送相应短消息
        /// </summary>
        /// <param name="__goodstradelog">交易日志信息</param>
        /// <returns>是否发送成功</returns>
        public static bool SendPM(Goodstradeloginfo __goodstradelog)
        {
            string pm_content = "这是由论坛系统自动发送的通知短消息.<BR />";
            string pm_title = "";
            bool issendpm = false;
            int msgtoid = 0;
            string msgto = "";
            string pagename = __goodstradelog.Offline == 1 ? "offlinetrade.aspx" : "onlinetrade.aspx";
            switch ((TradeStatusEnum)__goodstradelog.Status)
            {
                case TradeStatusEnum.UnStart:
                    {
                        pm_title = "[系统消息] 有买家购买您的商品";
                        pm_content = pm_content + string.Format("买家 {0} 购买您的商品 {1}. 但交易尚未生效, 等待您的确认, 请<a href =\"" + pagename + "?goodstradelogid={2}\">点击这里</a>查看详情.",
                                    __goodstradelog.Buyer,
                                    __goodstradelog.Subject,
                                    __goodstradelog.Id);
                        issendpm = true;
                        msgtoid = __goodstradelog.Sellerid;
                        msgto = __goodstradelog.Seller;
                        break;
                    }
                case TradeStatusEnum.WAIT_SELLER_SEND_GOODS:
                    {
                        pm_title = "[系统消息] 买家已付款, 等待您发货";
                        pm_content = pm_content + string.Format("买家 {0} 购买您的商品 {1}. 买家已付款, 等待您发货, 请<a href =\"" + pagename + "?goodstradelogid={2}\">点击这里</a>查看详情.",
                                    __goodstradelog.Buyer,
                                    __goodstradelog.Subject,
                                    __goodstradelog.Id);
                        issendpm = true;
                        msgtoid = __goodstradelog.Sellerid;
                        msgto = __goodstradelog.Seller;
                        break;
                    }
                case TradeStatusEnum.WAIT_BUYER_CONFIRM_GOODS:
                    {
                        pm_title = "[系统消息] 您购买的商品已经发货";
                        pm_content = pm_content + string.Format("您购买的商品 {0} . 卖家 {1} 已发货, 等待您的确认, 请<a href =\"" + pagename + "?goodstradelogid={2}\">点击这里</a>查看详情.",
                                    __goodstradelog.Subject,
                                    __goodstradelog.Seller,
                                    __goodstradelog.Id);
                        msgtoid = __goodstradelog.Buyerid;
                        msgto = __goodstradelog.Buyer;
                        issendpm = true; 
                        issendpm = true; break;
                    }
                case TradeStatusEnum.WAIT_SELLER_AGREE:
                    {
                        pm_title = "[系统消息] 有买家等待你同意退款";
                        pm_content = pm_content + string.Format("买家 {0} 等待你同意退款, 请<a href =\"" + pagename + "?goodstradelogid={1}\">点击这里</a>查看详情.",
                                     __goodstradelog.Buyer,
                                    __goodstradelog.Id);
                        issendpm = true;
                        msgtoid = __goodstradelog.Sellerid;
                        msgto = __goodstradelog.Seller;
                        break; 
                    }
                case TradeStatusEnum.SELLER_REFUSE_BUYER:
                    {
                        pm_title = "[系统消息] 有卖家拒绝您的条件, 等待您修改条件";
                        pm_content = pm_content + string.Format("卖家 {0} 拒绝您的条件, 等待您修改条件, 请<a href =\"" + pagename + "?goodstradelogid={1}\">点击这里</a>查看详情.",
                                     __goodstradelog.Seller,
                                    __goodstradelog.Id);
                        issendpm = true;
                        msgtoid = __goodstradelog.Buyerid;
                        msgto = __goodstradelog.Buyer;
                        break; 
                    }
                case TradeStatusEnum.WAIT_BUYER_RETURN_GOODS:
                    {
                        pm_title = "[系统消息] 有卖家同意退款, 等待您退货";
                        pm_content = pm_content + string.Format("卖家 {0} 同意退款, 等待您退货, 请<a href =\"" + pagename + "?goodstradelogid={1}\">点击这里</a>查看详情.",
                                     __goodstradelog.Seller,
                                    __goodstradelog.Id);
                        msgtoid = __goodstradelog.Buyerid;
                        msgto = __goodstradelog.Buyer;
                        issendpm = true;
                        break; 
                    }
                case TradeStatusEnum.WAIT_SELLER_CONFIRM_GOODS:
                    {
                        pm_title = "[系统消息] 有买家已退货, 等待您收货";
                        pm_content = pm_content + string.Format("买家 {0} 已退货, 等待您收货, 请<a href =\"" + pagename + "?goodstradelogid={1}\">点击这里</a>查看详情.",
                                     __goodstradelog.Buyer,
                                    __goodstradelog.Id);
                        msgtoid = __goodstradelog.Sellerid;
                        msgto = __goodstradelog.Seller;
                        issendpm = true;
                        break; 
                    }
                case TradeStatusEnum.TRADE_FINISHED:
                    {
                        pm_title = "[系统消息] 商品交易已成功完成";
                        pm_content = pm_content + string.Format("商品 {0} 已交易成功, 请<a href =\"goodsrate.aspx?goodstradelogid={1}\">点击这里</a>给对方评分.",
                                    __goodstradelog.Subject,
                                    __goodstradelog.Id);
                        msgtoid = __goodstradelog.Sellerid;
                        msgto = __goodstradelog.Seller;
                        issendpm = true; 
                        break;
                    }
                case TradeStatusEnum.TRADE_CLOSED:
                    {
                        pm_title = "[系统消息] 卖家已取消此次交易, 当前交易关闭";
                        pm_content = pm_content + string.Format("商品 {0} 交易失败, 卖家取消交易, 请<a href =\"goodsrate.aspx?goodstradelogid={1}\">点击这里</a>查看详情.",
                                    __goodstradelog.Subject,
                                    __goodstradelog.Id);
                        msgtoid = __goodstradelog.Sellerid;
                        msgto = __goodstradelog.Seller;
                        issendpm = true;
                        break;
                    }          
                case TradeStatusEnum.REFUND_SUCCESS:
                    {
                        pm_title = "[系统消息] 您购买的商品已成功退款";
                        pm_content = pm_content + string.Format("商品 {0} 已退款成功, 请<a href =\"goodsrate.aspx?goodstradelogid={1}\">点击这里</a>给对方评分.",
                                    __goodstradelog.Subject,
                                    __goodstradelog.Id);
                        msgtoid = __goodstradelog.Buyerid;
                        msgto = __goodstradelog.Buyer;
                        issendpm = true;
                        break;
                    }
            }

            //发送短消息
            if(issendpm)
            {
                PrivateMessageInfo __privatemessageinfo = new PrivateMessageInfo();

                // 收件箱
                __privatemessageinfo.Message = Utils.HtmlEncode(pm_content.ToString());
                __privatemessageinfo.Subject = Utils.HtmlEncode(pm_title);
                __privatemessageinfo.Msgto = msgto;
                __privatemessageinfo.Msgtoid = msgtoid;
                __privatemessageinfo.Msgfrom = "系统";
                __privatemessageinfo.Msgfromid = 0;
                __privatemessageinfo.New = 1;
                __privatemessageinfo.Postdatetime = Utils.GetDateTime();

                PrivateMessages.CreatePrivateMessage(__privatemessageinfo, 0);
            }
            return true;
        }

      
        /// <summary>
        /// 获取指定商品交易日志id的交易信息
        /// </summary>
        /// <param name="goodstradelogid">商品交易日志id</param>
        /// <returns>交易信息</returns>
        public static Goodstradeloginfo GetGoodsTradeLogInfo(int goodstradelogid)
        {
            return DTO.GetGoodsTradeLogInfo(DbProvider.GetInstance().GetGoodsTradeLogByID(goodstradelogid));
        }

        /// <summary>
        /// 根据交易单的流水号来获取交易信息
        /// </summary>
        /// <param name="tradeno">交易单的流水号</param>
        /// <returns>交易信息</returns>
        public static Goodstradeloginfo GetGoodsTradeLogInfo(string tradeno)
        {
            return DTO.GetGoodsTradeLogInfo(DbProvider.GetInstance().GetGoodsTradeLogByTradeNo(tradeno));
        }

        

        /// <summary>
        /// 获取指定商品id和相关条件下的商品交易信息集合
        /// </summary>
        /// <param name="goodsid">商品id</param>
        /// <param name="pagesize">页面大小</param>
        /// <param name="pageindex">当前页</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="ascdesc">排序方式(0:升序, 1:降序)</param>
        /// <returns>商品交易信息集合</returns>
        public static GoodstradeloginfoCollection GetGoodsTradeLog(int goodsid, int pagesize, int pageindex, string orderby, int ascdesc)
        {
            GoodstradeloginfoCollection coll = new GoodstradeloginfoCollection();

            if (pageindex <= 0)
            {
                return coll;
            }

            string condition = DbProvider.GetInstance().SetGoodsTradeStatusCond((int)MallUtils.OperaCode.Equal, 7);
            return DTO.GetGoodsTradeLogInfoList(DbProvider.GetInstance().GetGoodsTradeLogByGid(goodsid, pagesize, pageindex, condition, orderby, ascdesc));
        }


        /// <summary>
        /// 获取指定商品id和条件下的商品交易数
        /// </summary>
        /// <param name="goodsid">商品id</param>
        /// <returns>交易日志数</returns>
        public static int GetGoodsTradeLogCount(int goodsid)
        {
            string condition = DbProvider.GetInstance().SetGoodsTradeStatusCond((int)MallUtils.OperaCode.Equal, 7);
            return DbProvider.GetInstance().GetTradeLogCountByGid(goodsid, condition);
        }

        /// <summary>
        /// 获取交易日志数
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="goodsidlist">商品id列表</param>
        /// <param name="uidtype">用户类型</param>
        /// <param name="filter">过滤方式</param>
        /// <param name="pagesize">页面尺寸</param>
        /// <param name="pageindex">当前页面</param>
        /// <returns>交易日志数</returns>
        public static DataTable GetGoodsTradeLogList(int userid, string goodsidlist, int uidtype, string filter, int pagesize, int pageindex)
        {
            return DbProvider.GetInstance().GetGoodsTradeLogList(userid, goodsidlist, uidtype, filter, pagesize, pageindex);
        }

        /// <summary>
        /// 获取交易日志数
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="goodsidlist">商品id列表</param>
        /// <param name="uidtype">用户类型</param>
        /// <param name="filter">过滤方式</param>
        /// <returns>交易日志数</returns>
        public static int GetGoodsTradeLogCount(int userid, string goodsidlist, int uidtype, string filter)
        {
            return DbProvider.GetInstance().GetGoodsTradeLogCount(userid, goodsidlist, uidtype, filter);
        }

        /// <summary>
        /// 获取指定商品id和相关条件下的商品交易信息(json数据串)
        /// </summary>
        /// <param name="goodsid">商品id</param>
        /// <param name="pagesize">页面大小</param>
        /// <param name="pageindex">当前页面</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="ascdesc">排序方式</param>
        /// <returns>交易json数据</returns>
        public static StringBuilder GetTradeLogJson(int goodsid, int pagesize, int pageindex, string orderby, int ascdesc)
        {
            StringBuilder __tradelogjson = new StringBuilder();
            __tradelogjson.Append("[");
            foreach (Goodstradeloginfo __goodstradeloginfo in GetGoodsTradeLog(goodsid, pagesize, pageindex, orderby, ascdesc))
            {
                __tradelogjson.Append(string.Format("{{'buyerid' : {0}, 'buyer' : '{1}', 'price' : {2}, 'number' : {3}, 'lastupdate' : '{4}', 'buyercredit' : {5}, 'status' : {6}}},",
                                __goodstradeloginfo.Buyerid,
                                __goodstradeloginfo.Buyer, 
                                __goodstradeloginfo.Price, 
                                __goodstradeloginfo.Number, 
                                __goodstradeloginfo.Lastupdate, 
                                __goodstradeloginfo.Buyercredit, 
                                __goodstradeloginfo.Status));
            }
            if (__tradelogjson.ToString().EndsWith(","))
            {
                __tradelogjson.Remove(__tradelogjson.Length - 1, 1);
            }
            __tradelogjson.Append("]");
            return __tradelogjson;
        }

        /// <summary>
        /// 获取指定用户的商品交易统计信息
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns>商品交易统计信息</returns>
        public static Goosdstradestatisticinfo GetTradeStatistic(int userid)
        {
            Goosdstradestatisticinfo goodstradestatistic = null;
            IDataReader __idatareader = DbProvider.GetInstance().GetTradeStatistic(userid);

            //绑定新的查询数据
            if (__idatareader.Read())
            {
                goodstradestatistic = new Goosdstradestatisticinfo();
                goodstradestatistic.Userid = userid;
                goodstradestatistic.Sellerattention = Convert.ToInt32(__idatareader["SellerAttention"].ToString());
                goodstradestatistic.Sellertrading = Convert.ToInt32(__idatareader["SellerTrading"].ToString());
                goodstradestatistic.Sellerrate = Convert.ToInt32(__idatareader["SellerRate"].ToString());
                goodstradestatistic.Sellnumbersum = Convert.ToDecimal(__idatareader["SellNumberSum"].ToString());
                goodstradestatistic.Selltradesum = Convert.ToDecimal(__idatareader["SellTradeSum"].ToString());
                goodstradestatistic.Buyerattention = Convert.ToInt32(__idatareader["BuyerAttention"].ToString());
                goodstradestatistic.Buyertrading = Convert.ToInt32(__idatareader["BuyerTrading"].ToString());
                goodstradestatistic.Buyerrate = Convert.ToInt32(__idatareader["BuyerRate"].ToString());
                goodstradestatistic.Buynumbersum = Convert.ToDecimal(__idatareader["BuyNumberSum"].ToString());
                goodstradestatistic.Buytradesum = Convert.ToDecimal(__idatareader["BuyTradeSum"].ToString());
            }

            __idatareader.Close();
            return goodstradestatistic;
        }

        /// <summary>
        /// 数据转换对象类
        /// </summary>
        public class DTO
        {
            /// <summary>
            /// 获得商品交易信息(DTO)
            /// </summary>
            /// <param name="__idatareader">要转换的数据</param>
            /// <returns>返回商品交易信息</returns>
            public static Goodstradeloginfo GetGoodsTradeLogInfo(IDataReader reader)
            {
                if (reader.Read())
                {
                    Goodstradeloginfo goodstradeloginfo = new Goodstradeloginfo();
                    goodstradeloginfo.Id = Convert.ToInt32(reader["id"].ToString());
                    goodstradeloginfo.Goodsid = Convert.ToInt32(reader["goodsid"].ToString());
                    goodstradeloginfo.Orderid = reader["orderid"].ToString().Trim();
                    goodstradeloginfo.Tradeno = reader["tradeno"].ToString().Trim();
                    goodstradeloginfo.Subject = reader["subject"].ToString().Trim();
                    goodstradeloginfo.Price = Convert.ToDecimal(reader["price"].ToString());
                    goodstradeloginfo.Quality = Convert.ToInt16(reader["quality"].ToString());
                    goodstradeloginfo.Categoryid = Convert.ToInt32(reader["categoryid"].ToString());
                    goodstradeloginfo.Number = Convert.ToInt16(reader["number"].ToString());
                    goodstradeloginfo.Tax = Convert.ToDecimal(reader["tax"].ToString());
                    goodstradeloginfo.Locus = reader["locus"].ToString().Trim();
                    goodstradeloginfo.Sellerid = Convert.ToInt32(reader["sellerid"].ToString());
                    goodstradeloginfo.Seller = reader["seller"].ToString().Trim();
                    goodstradeloginfo.Selleraccount = reader["selleraccount"].ToString().Trim();
                    goodstradeloginfo.Buyerid = Convert.ToInt32(reader["buyerid"].ToString());
                    goodstradeloginfo.Buyer = reader["buyer"].ToString().Trim();
                    goodstradeloginfo.Buyercontact = reader["buyercontact"].ToString().Trim();
                    goodstradeloginfo.Buyercredit = Convert.ToInt16(reader["buyercredit"].ToString());
                    goodstradeloginfo.Buyermsg = reader["buyermsg"].ToString().Trim();
                    goodstradeloginfo.Status = Convert.ToInt16(reader["status"].ToString());
                    goodstradeloginfo.Lastupdate = Convert.ToDateTime(reader["lastupdate"].ToString());
                    goodstradeloginfo.Offline = Convert.ToInt16(reader["offline"].ToString());
                    goodstradeloginfo.Buyername = reader["buyername"].ToString().Trim();
                    goodstradeloginfo.Buyerzip = reader["buyerzip"].ToString().Trim();
                    goodstradeloginfo.Buyerphone = reader["buyerphone"].ToString().Trim();
                    goodstradeloginfo.Buyermobile = reader["buyermobile"].ToString().Trim();
                    goodstradeloginfo.Transport = Convert.ToInt16(reader["transport"].ToString());
                    goodstradeloginfo.Transportpay = Convert.ToInt16(reader["transportpay"].ToString());
                    goodstradeloginfo.Transportfee = Convert.ToDecimal(reader["transportfee"].ToString());
                    goodstradeloginfo.Tradesum = Convert.ToDecimal(reader["tradesum"].ToString());
                    goodstradeloginfo.Baseprice = Convert.ToDecimal(reader["baseprice"].ToString());
                    goodstradeloginfo.Discount = Convert.ToInt16(reader["discount"].ToString());
                    goodstradeloginfo.Ratestatus = Convert.ToInt16(reader["ratestatus"].ToString());
                    goodstradeloginfo.Message = reader["message"].ToString().Trim();

                    reader.Close();
                    return goodstradeloginfo;
                }
                return null;
            }

            /// <summary>
            /// 获得商品交易信息(DTO)
            /// </summary>
            /// <param name="__idatareader">要转换的数据</param>
            /// <returns>返回商品交易信息</returns>
            public static GoodstradeloginfoCollection GetGoodsTradeLogInfoList(IDataReader reader)
            {
                GoodstradeloginfoCollection goodstradeloginfocoll = new GoodstradeloginfoCollection();

                while (reader.Read())
                {
                    Goodstradeloginfo goodstradeloginfo = new Goodstradeloginfo();
                    goodstradeloginfo.Id = Convert.ToInt32(reader["id"].ToString());
                    goodstradeloginfo.Goodsid = Convert.ToInt32(reader["goodsid"].ToString());
                    goodstradeloginfo.Orderid = reader["orderid"].ToString().Trim();
                    goodstradeloginfo.Tradeno = reader["tradeno"].ToString().Trim();
                    goodstradeloginfo.Subject = reader["subject"].ToString().Trim();
                    goodstradeloginfo.Price = Convert.ToDecimal(reader["price"].ToString());
                    goodstradeloginfo.Quality = Convert.ToInt16(reader["quality"].ToString());
                    goodstradeloginfo.Categoryid = Convert.ToInt32(reader["categoryid"].ToString());
                    goodstradeloginfo.Number = Convert.ToInt16(reader["number"].ToString());
                    goodstradeloginfo.Tax = Convert.ToDecimal(reader["tax"].ToString());
                    goodstradeloginfo.Locus = reader["locus"].ToString().Trim();
                    goodstradeloginfo.Sellerid = Convert.ToInt32(reader["sellerid"].ToString());
                    goodstradeloginfo.Seller = reader["seller"].ToString().Trim();
                    goodstradeloginfo.Selleraccount = reader["selleraccount"].ToString().Trim();
                    goodstradeloginfo.Buyerid = Convert.ToInt32(reader["buyerid"].ToString());
                    goodstradeloginfo.Buyer = reader["buyer"].ToString().Trim();
                    goodstradeloginfo.Buyercontact = reader["buyercontact"].ToString().Trim();
                    goodstradeloginfo.Buyercredit = Convert.ToInt16(reader["buyercredit"].ToString());
                    goodstradeloginfo.Buyermsg = reader["buyermsg"].ToString().Trim();
                    goodstradeloginfo.Status = Convert.ToInt16(reader["status"].ToString());
                    goodstradeloginfo.Lastupdate = Convert.ToDateTime(reader["lastupdate"].ToString());
                    goodstradeloginfo.Offline = Convert.ToInt16(reader["offline"].ToString());
                    goodstradeloginfo.Buyername = reader["buyername"].ToString().Trim();
                    goodstradeloginfo.Buyerzip = reader["buyerzip"].ToString().Trim();
                    goodstradeloginfo.Buyerphone = reader["buyerphone"].ToString().Trim();
                    goodstradeloginfo.Buyermobile = reader["buyermobile"].ToString().Trim();
                    goodstradeloginfo.Transport = Convert.ToInt16(reader["transport"].ToString());
                    goodstradeloginfo.Transportpay = Convert.ToInt16(reader["transportpay"].ToString());
                    goodstradeloginfo.Transportfee = Convert.ToDecimal(reader["transportfee"].ToString());
                    goodstradeloginfo.Tradesum = Convert.ToDecimal(reader["tradesum"].ToString());
                    goodstradeloginfo.Baseprice = Convert.ToDecimal(reader["baseprice"].ToString());
                    goodstradeloginfo.Discount = Convert.ToInt16(reader["discount"].ToString());
                    goodstradeloginfo.Ratestatus = Convert.ToInt16(reader["ratestatus"].ToString());
                    goodstradeloginfo.Message = reader["message"].ToString().Trim();

                    goodstradeloginfocoll.Add(goodstradeloginfo);
                }
                reader.Close();

                return goodstradeloginfocoll;
            }

            /// <summary>
            /// 获得商品交易信息(DTO)
            /// </summary>
            /// <param name="dt">要转换的数据表</param>
            /// <returns>返回商品交易信息</returns>
            public static Goodstradeloginfo[] GetGoodsTradeLogInfoArray(DataTable dt)
            {
                if (dt == null || dt.Rows.Count == 0)
                    return null;

                Goodstradeloginfo[] __goodstradeloginfoarray = new Goodstradeloginfo[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    __goodstradeloginfoarray[i] = new Goodstradeloginfo();
                    __goodstradeloginfoarray[i].Id = Convert.ToInt32(dt.Rows[i]["id"].ToString());
                    __goodstradeloginfoarray[i].Goodsid = Convert.ToInt32(dt.Rows[i]["goodsid"].ToString());
                    __goodstradeloginfoarray[i].Orderid = dt.Rows[i]["orderid"].ToString();
                    __goodstradeloginfoarray[i].Tradeno = dt.Rows[i]["tradeno"].ToString();
                    __goodstradeloginfoarray[i].Subject = dt.Rows[i]["subject"].ToString();
                    __goodstradeloginfoarray[i].Price = Convert.ToDecimal(dt.Rows[i]["price"].ToString());
                    __goodstradeloginfoarray[i].Quality = Convert.ToInt32(dt.Rows[i]["quality"].ToString());
                    __goodstradeloginfoarray[i].Categoryid = Convert.ToInt32(dt.Rows[i]["categoryid"].ToString());
                    __goodstradeloginfoarray[i].Number = Convert.ToInt32(dt.Rows[i]["number"].ToString());
                    __goodstradeloginfoarray[i].Tax = Convert.ToDecimal(dt.Rows[i]["tax"].ToString());
                    __goodstradeloginfoarray[i].Locus = dt.Rows[i]["locus"].ToString();
                    __goodstradeloginfoarray[i].Sellerid = Convert.ToInt32(dt.Rows[i]["sellerid"].ToString());
                    __goodstradeloginfoarray[i].Seller = dt.Rows[i]["seller"].ToString();
                    __goodstradeloginfoarray[i].Selleraccount = dt.Rows[i]["selleraccount"].ToString();
                    __goodstradeloginfoarray[i].Buyerid = Convert.ToInt32(dt.Rows[i]["buyerid"].ToString());
                    __goodstradeloginfoarray[i].Buyer = dt.Rows[i]["buyer"].ToString();
                    __goodstradeloginfoarray[i].Buyercontact = dt.Rows[i]["buyercontact"].ToString();
                    __goodstradeloginfoarray[i].Buyercredit = Convert.ToInt32(dt.Rows[i]["buyercredit"].ToString());
                    __goodstradeloginfoarray[i].Buyermsg = dt.Rows[i]["buyermsg"].ToString();
                    __goodstradeloginfoarray[i].Status = Convert.ToInt32(dt.Rows[i]["status"].ToString());
                    __goodstradeloginfoarray[i].Lastupdate = Convert.ToDateTime(dt.Rows[i]["lastupdate"].ToString());
                    __goodstradeloginfoarray[i].Offline = Convert.ToInt32(dt.Rows[i]["offline"].ToString());
                    __goodstradeloginfoarray[i].Buyername = dt.Rows[i]["buyername"].ToString();
                    __goodstradeloginfoarray[i].Buyerzip = dt.Rows[i]["buyerzip"].ToString();
                    __goodstradeloginfoarray[i].Buyerphone = dt.Rows[i]["buyerphone"].ToString();
                    __goodstradeloginfoarray[i].Buyermobile = dt.Rows[i]["buyermobile"].ToString();
                    __goodstradeloginfoarray[i].Transport = Convert.ToInt32(dt.Rows[i]["transport"].ToString());
                    __goodstradeloginfoarray[i].Transportpay = Convert.ToInt32(dt.Rows[i]["transportpay"].ToString());
                    __goodstradeloginfoarray[i].Transportfee = Convert.ToDecimal(dt.Rows[i]["transportfee"].ToString());
                    __goodstradeloginfoarray[i].Tradesum = Convert.ToDecimal(dt.Rows[i]["tradesum"].ToString());
                    __goodstradeloginfoarray[i].Baseprice = Convert.ToDecimal(dt.Rows[i]["baseprice"].ToString());
                    __goodstradeloginfoarray[i].Discount = Convert.ToInt32(dt.Rows[i]["discount"].ToString());
                    __goodstradeloginfoarray[i].Ratestatus = Convert.ToInt32(dt.Rows[i]["ratestatus"].ToString());
                    __goodstradeloginfoarray[i].Message = dt.Rows[i]["message"].ToString();

                }
                dt.Dispose();
                return __goodstradeloginfoarray;
            }
        }
    }
}
