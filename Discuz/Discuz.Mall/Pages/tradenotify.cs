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
    /// ����״̬֪ͨҳ��
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
                        case "WAIT_BUYER_PAY": // �ȴ���Ҹ���
                            { 
                                goodstradeloginfo.Status = (int)TradeStatusEnum.WAIT_BUYER_PAY; break; 
                            }
                        case "WAIT_SELLER_CONFIRM_TRADE": // �����Ѵ������ȴ�����ȷ��
                            { 
                                goodstradeloginfo.Status = (int)TradeStatusEnum.WAIT_SELLER_CONFIRM_TRADE; break; 
                            }
                        case "WAIT_SYS_CONFIRM_PAY":��// ȷ����Ҹ����У����𷢻�
                            { 
                                goodstradeloginfo.Status = (int)TradeStatusEnum.WAIT_SYS_CONFIRM_PAY; break; 
                            }
                        case "WAIT_SELLER_SEND_GOODS": // ֧�����յ���Ҹ�������ҷ���
                            { 
                                goodstradeloginfo.Status = (int)TradeStatusEnum.WAIT_SELLER_SEND_GOODS; break; 
                            }
                        case "WAIT_BUYER_CONFIRM_GOODS": //  �����ѷ��������ȷ����
                            { 
                                goodstradeloginfo.Status = (int)TradeStatusEnum.WAIT_BUYER_CONFIRM_GOODS; break; 
                            }
                        case "WAIT_SYS_PAY_SELLER": // ���ȷ���յ������ȴ�֧������������
                            { 
                                goodstradeloginfo.Status = (int)TradeStatusEnum.WAIT_SYS_PAY_SELLER; break; 
                            }
                        case "TRADE_FINISHED": // ���׳ɹ�����
                            { 
                                goodstradeloginfo.Status = (int)TradeStatusEnum.TRADE_FINISHED; break; 
                            }
                        case "TRADE_CLOSED": //  ������;�ر�(δ���)
                            { 
                                goodstradeloginfo.Status = (int)TradeStatusEnum.TRADE_CLOSED; break; 
                            }
                        case "WAIT_SELLER_AGREE": //  �ȴ�����ͬ���˿�
                            { 
                                goodstradeloginfo.Status = (int)TradeStatusEnum.WAIT_SELLER_AGREE; break; 
                            }
                        case "SELLER_REFUSE_BUYER": // ���Ҿܾ�����������ȴ�����޸�����
                            { 
                                goodstradeloginfo.Status = (int)TradeStatusEnum.SELLER_REFUSE_BUYER; break; 
                            }
                        case "WAIT_BUYER_RETURN_GOODS": // ����ͬ���˿�ȴ�����˻�
                            { 
                                goodstradeloginfo.Status = (int)TradeStatusEnum.WAIT_BUYER_RETURN_GOODS; break; 
                            }
                        case "WAIT_SELLER_CONFIRM_GOODS": // �ȴ������ջ�
                            { 
                                goodstradeloginfo.Status = (int)TradeStatusEnum.WAIT_SELLER_CONFIRM_GOODS; break;
                            }
                        case "REFUND_SUCCESS": //  �˿�ɹ�
                            { 
                                goodstradeloginfo.Status = (int)TradeStatusEnum.REFUND_SUCCESS; break; 
                            }
                    }

                    goodstradeloginfo.Lastupdate = DateTime.Now;

                    TradeLogs.UpdateTradeLog(goodstradeloginfo, goodstradeloginfo.Status, true);
                }
                HttpContext.Current.Response.Write("success");     //���ظ�֧������Ϣ,�ɹ�
            }
            else
            {
                HttpContext.Current.Response.Write("fail");
            }
        }

        /// <summary>
        /// ��ȡԶ�̷�����ATN���
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
                strResult = "����:" + exp.Message;
            }
            return strResult;
        }

        /// <summary>
        /// ���֧�����
        /// </summary>
        /// <returns></returns>
        private bool CheckPayment()
        {
            AliPayConfigInfo aliPayConfigInfo = TradeConfigs.GetConfig().Alipayconfiginfo;
            string alipay_notify_url = "https://www.alipay.com/cooperate/gateway.do?";
            string key = aliPayConfigInfo.Sign; //partner �Ķ�Ӧ���װ�ȫУ���루������д��
            string _input_charset = aliPayConfigInfo.Inputcharset;
            string partner = aliPayConfigInfo.Partner; 		//partner�������id��������д��

            alipay_notify_url = alipay_notify_url + "service=notify_verify" + "&partner=" + partner + "&notify_id=" + DNTRequest.GetString("notify_id");

            //��ȡ֧����ATN���ؽ����true����ȷ�Ķ�����Ϣ��false ����Ч��
            string responseTxt = Get_Http(alipay_notify_url, 120000);

            //����
            string[] Sortedstr = System.Web.HttpContext.Current.Request.Form.AllKeys;

            AliPayment.QuickSort(Sortedstr, 0, Sortedstr.Length - 1);
            //�����md5ժҪ�ַ���
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
            //����Md5ժҪ
            string mysign = AliPayment.GetMD5(prestr.ToString(), _input_charset);

            //��֤֧������������Ϣ,ǩ���Ƿ���ȷ
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
