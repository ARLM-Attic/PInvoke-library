using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Web;
using System.Net;
using System.IO;

using Discuz.Common;
#if NET1
#else
using Discuz.Common.Generic;
#endif
using Discuz.Forum;
using Discuz.Config;
using Discuz.Entity;
using Discuz.Mall.Data;
using Discuz.Mall;
using Discuz.Plugin.Payment;

namespace Discuz.Mall.Pages
{
    /// <summary>
    /// 交易状态通知页面
    /// </summary>
    public class tradenotify : PageBase
    {
        protected override void ShowPage()
        {
            if (CheckPayment())
            {
                Goodstradeloginfo goodstradeloginfo = TradeLogs.GetGoodsTradeLogInfo(DNTRequest.GetString("out_trade_no"));

                if (goodstradeloginfo != null && goodstradeloginfo.Id > 0)
                {
                    switch (DNTRequest.GetString("trade_status"))
                    {
                        case "WAIT_BUYER_PAY": // 等待买家付款
                            { 
                                goodstradeloginfo.Status = (int)TradeStatusEnum.WAIT_BUYER_PAY; break; 
                            }
                        case "WAIT_SELLER_CONFIRM_TRADE": // 交易已创建，等待卖家确认
                            { 
                                goodstradeloginfo.Status = (int)TradeStatusEnum.WAIT_SELLER_CONFIRM_TRADE; break; 
                            }
                        case "WAIT_SYS_CONFIRM_PAY":　// 确认买家付款中，暂勿发货
                            { 
                                goodstradeloginfo.Status = (int)TradeStatusEnum.WAIT_SYS_CONFIRM_PAY; break; 
                            }
                        case "WAIT_SELLER_SEND_GOODS": // 支付宝收到买家付款，请卖家发货
                            { 
                                goodstradeloginfo.Status = (int)TradeStatusEnum.WAIT_SELLER_SEND_GOODS; break; 
                            }
                        case "WAIT_BUYER_CONFIRM_GOODS": //  卖家已发货，买家确认中
                            { 
                                goodstradeloginfo.Status = (int)TradeStatusEnum.WAIT_BUYER_CONFIRM_GOODS; break; 
                            }
                        case "WAIT_SYS_PAY_SELLER": // 买家确认收到货，等待支付宝打款给卖家
                            { 
                                goodstradeloginfo.Status = (int)TradeStatusEnum.WAIT_SYS_PAY_SELLER; break; 
                            }
                        case "TRADE_FINISHED": // 交易成功结束
                            { 
                                goodstradeloginfo.Status = (int)TradeStatusEnum.TRADE_FINISHED; break; 
                            }
                        case "TRADE_CLOSED": //  交易中途关闭(未完成)
                            { 
                                goodstradeloginfo.Status = (int)TradeStatusEnum.TRADE_CLOSED; break; 
                            }
                        case "WAIT_SELLER_AGREE": //  等待卖家同意退款
                            { 
                                goodstradeloginfo.Status = (int)TradeStatusEnum.WAIT_SELLER_AGREE; break; 
                            }
                        case "SELLER_REFUSE_BUYER": // 卖家拒绝买家条件，等待买家修改条件
                            { 
                                goodstradeloginfo.Status = (int)TradeStatusEnum.SELLER_REFUSE_BUYER; break; 
                            }
                        case "WAIT_BUYER_RETURN_GOODS": // 卖家同意退款，等待买家退货
                            { 
                                goodstradeloginfo.Status = (int)TradeStatusEnum.WAIT_BUYER_RETURN_GOODS; break; 
                            }
                        case "WAIT_SELLER_CONFIRM_GOODS": // 等待卖家收货
                            { 
                                goodstradeloginfo.Status = (int)TradeStatusEnum.WAIT_SELLER_CONFIRM_GOODS; break;
                            }
                        case "REFUND_SUCCESS": //  退款成功
                            { 
                                goodstradeloginfo.Status = (int)TradeStatusEnum.REFUND_SUCCESS; break; 
                            }
                    }

                    goodstradeloginfo.Lastupdate = DateTime.Now;

                    TradeLogs.UpdateTradeLog(goodstradeloginfo, goodstradeloginfo.Status, true);
                }
                HttpContext.Current.Response.Write("success");     //返回给支付宝消息,成功
            }
            else
            {
                HttpContext.Current.Response.Write("fail");
            }
        }

        /// <summary>
        /// 获取远程服务器ATN结果
        /// </summary>
        /// <param name="a_strUrl"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public String Get_Http(String strUrl, int timeout)
        {
            string strResult;
            try
            {
                HttpWebRequest myReq = (HttpWebRequest)HttpWebRequest.Create(strUrl);
                myReq.Timeout = timeout;
                HttpWebResponse HttpWResp = (HttpWebResponse)myReq.GetResponse();
                Stream myStream = HttpWResp.GetResponseStream();
                StreamReader sr = new StreamReader(myStream, Encoding.UTF8);
                StringBuilder strBuilder = new StringBuilder();
                while (-1 != sr.Peek())
                {
                    strBuilder.Append(sr.ReadLine());
                }
                strResult = strBuilder.ToString();
            }
            catch (Exception exp)
            {
                strResult = "错误:" + exp.Message;
            }
            return strResult;
        }

        /// <summary>
        /// 检查支付结果
        /// </summary>
        /// <returns></returns>
        private bool CheckPayment()
        {
            AliPayConfigInfo aliPayConfigInfo = TradeConfigs.GetConfig().Alipayconfiginfo;
            string alipay_notify_url = "https://www.alipay.com/cooperate/gateway.do?";
            string key = aliPayConfigInfo.Sign; //partner 的对应交易安全校验码（必须填写）
            string _input_charset = aliPayConfigInfo.Inputcharset;
            string partner = aliPayConfigInfo.Partner; 		//partner合作伙伴id（必须填写）

            alipay_notify_url = alipay_notify_url + "service=notify_verify" + "&partner=" + partner + "&notify_id=" + DNTRequest.GetString("notify_id");

            //获取支付宝ATN返回结果，true是正确的订单信息，false 是无效的
            string responseTxt = Get_Http(alipay_notify_url, 120000);

            //排序
            string[] Sortedstr = System.Web.HttpContext.Current.Request.Form.AllKeys;

            AliPayment.QuickSort(Sortedstr, 0, Sortedstr.Length - 1);
            //构造待md5摘要字符串
            StringBuilder prestr = new StringBuilder();
            for (int i = 0; i < Sortedstr.Length; i++)
            {
                if (DNTRequest.GetString(Sortedstr[i]) != "" && Sortedstr[i] != "sign" && Sortedstr[i] != "sign_type")
                {
                    if (i == Sortedstr.Length - 1)
                    {
                        prestr.Append(Sortedstr[i] + "=" + DNTRequest.GetString(Sortedstr[i]));
                    }
                    else
                    {
                        prestr.Append(Sortedstr[i] + "=" + DNTRequest.GetString(Sortedstr[i]) + "&");
                    }
                }
            }

            prestr.Append(key);
            //生成Md5摘要
            string mysign = AliPayment.GetMD5(prestr.ToString(), _input_charset);

            //验证支付发过来的消息,签名是否正确
            if (mysign == DNTRequest.GetString("sign") && responseTxt == "true")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
