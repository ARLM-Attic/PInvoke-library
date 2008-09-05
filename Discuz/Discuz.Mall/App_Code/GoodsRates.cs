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
    /// ��Ʒ���۹��������
    /// </summary>
    public class GoodsRates
    {
        /// <summary>
        /// ������Ʒ������Ϣ
        /// </summary>
        /// <param name="goodsrateinfo">Ҫ��������Ʒ������Ϣ</param>
        /// <returns></returns>
        public static int CreateGoodsRate(Goodsrateinfo goodsrateinfo)
        {
            return DbProvider.GetInstance().CreateGoodsRate(goodsrateinfo);
        }

        /// <summary>
        /// ��ȡָ����Ʒ������־id����Ʒ������Ϣ
        /// </summary>
        /// <param name="goodstradelogid">��Ʒ������־id</param>
        /// <returns></returns>
        public static GoodsrateinfoCollection GetGoodsRatesByTradeLogID(int goodstradelogid)
        {
            return DTO.GetGoodsRateInfoList(DbProvider.GetInstance().GetGoodsRateByTradeLogID(goodstradelogid));
        }

        /// <summary>
        /// ָ������Ʒ�����ܷ��������
        /// </summary>
        /// <param name="goodstradelogid">��ǰ��Ʒ����id</param>
        /// <param name="uid">Ҫ�������۵��û�id</param>
        /// <returns></returns>
        public static bool CanRate(int goodstradelogid, int uid)
        {
            foreach (Goodsrateinfo goodsrateinfo in GetGoodsRatesByTradeLogID(goodstradelogid))
            {
                if (goodsrateinfo.Uid == uid)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// ָ������Ʒ����˫���Ƿ�����
        /// </summary>
        /// <param name="goodstradelogid">��ǰ��Ʒ����id</param>
        /// <param name="selleruid">�����û�id</param>
        /// <param name="buyeruid">���</param>
        /// <returns></returns>
        public static bool RateClosed(int goodstradelogid, int selleruid, int buyeruid)
        {
            int ratecount = 0;
            foreach (Goodsrateinfo goodsrateinfo in GetGoodsRatesByTradeLogID(goodstradelogid))
            {
                if (goodsrateinfo.Uid == selleruid || goodsrateinfo.Uid == buyeruid)
                {
                    ratecount = ratecount + 1;
                }
            }
            return ratecount >= 2 ? true : false;
        }
 
        /// <summary>
        /// ��ȡָ����������Ʒ��������(json��ʽ)
        /// </summary>
        /// <param name="uid">�û�id</param>
        /// <param name="uidtype">�û�id����(1:����, 2:���, 3:������)</param>
        /// <param name="ratetype">��������(1:����, 2:����, 3:����)</param>
        /// <param name="filter">���й��˵�����(oneweek:1����, onemonth:1����, sixmonth:������, sixmonthago:����֮ǰ)</param>
        /// <returns></returns>
        public static string GetGoodsRatesJson(int uid, int uidtype, int ratetype, string filter)
        {
            StringBuilder sb_categories = new StringBuilder();
            sb_categories.Append("[");

            IDataReader __idatareader = DbProvider.GetInstance().GetGoodsRates(uid, uidtype, ratetype, filter);
            if (__idatareader != null)
            {
                while (__idatareader.Read())
                {
                    sb_categories.Append(string.Format("{{'id' : {0}, 'goodstradelogid' : {1}, 'message' : '{2}', 'uid' : {3}, 'uidtype' : {4}, 'ratetouid' : {5}, 'username' : '{6}', 'postdatetime' : '{7}', 'goodsid' : {8}, 'goodstitle' : '{9}', 'price' : '{10}', 'ratetype' :{11}, 'ratetousername' : '{12}'}},",
                        __idatareader["id"].ToString(),
                        __idatareader["goodstradelogid"].ToString(),
                        __idatareader["message"].ToString().Trim(),
                        __idatareader["uid"].ToString().Trim(),
                        __idatareader["uidtype"].ToString().ToLower(),
                        __idatareader["ratetouid"].ToString(),
                        __idatareader["username"].ToString().Trim(),
                        Convert.ToDateTime(__idatareader["postdatetime"]).ToString("yyyy-MM-dd HH:mm:ss"),
                        __idatareader["goodsid"].ToString().Trim(),
                        __idatareader["goodstitle"].ToString().Trim(),
                        __idatareader["price"].ToString().ToLower(),
                        __idatareader["ratetype"].ToString(),
                        __idatareader["ratetousername"].ToString()
                        ));
                }
                __idatareader.Dispose();
            }
            if (sb_categories.ToString().EndsWith(","))
            {
                sb_categories.Remove(sb_categories.Length-1,1);
            }
            sb_categories.Append("]");
            return sb_categories.ToString();
        }

        
        /// <summary>
        /// ����ת��������
        /// </summary>
        public class DTO
        {
            /// <summary>
            /// �����Ʒ������Ϣ(DTO)
            /// </summary>
            /// <param name="__idatareader">Ҫת�������ݱ�</param>
            /// <returns>������Ʒ������Ϣ</returns>
            public static Goodsrateinfo GetGoodsRateInfo(IDataReader reader)
            {
                if (reader.Read())
                {
                    Goodsrateinfo goodsratesinfo = new Goodsrateinfo();
                    goodsratesinfo.Id = Convert.ToInt32(reader["id"].ToString());
                    goodsratesinfo.Goodstradelogid = Convert.ToInt32(reader["goodstradelogid"]);
                    goodsratesinfo.Message = reader["message"].ToString().Trim();
                    goodsratesinfo.Explain = reader["explain"].ToString().Trim();
                    goodsratesinfo.Ip = reader["ip"].ToString().Trim();
                    goodsratesinfo.Uid = Convert.ToInt32(reader["uid"].ToString());
                    goodsratesinfo.Uidtype = Convert.ToInt16(reader["uidtype"].ToString());
                    goodsratesinfo.Ratetouid = Convert.ToInt32(reader["ratetouid"].ToString());
                    goodsratesinfo.Username = reader["username"].ToString().Trim();
                    goodsratesinfo.Postdatetime = Convert.ToDateTime(reader["postdatetime"]);
                    goodsratesinfo.Goodsid = Convert.ToInt32(reader["goodsid"].ToString());
                    goodsratesinfo.Goodstitle = reader["goodstitle"].ToString().Trim();
                    goodsratesinfo.Price = Convert.ToDecimal(reader["price"].ToString());
                    goodsratesinfo.Ratetype = Convert.ToInt16(reader["ratetype"].ToString());

                    reader.Close();
                    return goodsratesinfo;
                }
                return null;
            }


            /// <summary>
            /// �����Ʒ������Ϣ(DTO)
            /// </summary>
            /// <param name="__idatareader">Ҫת��������</param>
            /// <returns>������Ʒ������Ϣ</returns>
            public static GoodsrateinfoCollection GetGoodsRateInfoList(IDataReader reader)
            {
                GoodsrateinfoCollection goodsrateinfocoll = new GoodsrateinfoCollection();

                while (reader.Read())
                {
                    Goodsrateinfo goodsratesinfo = new Goodsrateinfo();
                    goodsratesinfo.Id = Convert.ToInt32(reader["id"].ToString());
                    goodsratesinfo.Goodstradelogid = Convert.ToInt32(reader["goodstradelogid"]);
                    goodsratesinfo.Message = reader["message"].ToString().Trim();
                    goodsratesinfo.Explain = reader["explain"].ToString().Trim();
                    goodsratesinfo.Ip = reader["ip"].ToString().Trim();
                    goodsratesinfo.Uid = Convert.ToInt32(reader["uid"].ToString());
                    goodsratesinfo.Uidtype = Convert.ToInt16(reader["uidtype"].ToString());
                    goodsratesinfo.Ratetouid = Convert.ToInt32(reader["ratetouid"].ToString());
                    goodsratesinfo.Username = reader["username"].ToString().Trim();
                    goodsratesinfo.Postdatetime = Convert.ToDateTime(reader["postdatetime"]);
                    goodsratesinfo.Goodsid = Convert.ToInt32(reader["goodsid"].ToString());
                    goodsratesinfo.Goodstitle = reader["goodstitle"].ToString().Trim();
                    goodsratesinfo.Price = Convert.ToDecimal(reader["price"].ToString());
                    goodsratesinfo.Ratetype = Convert.ToInt16(reader["ratetype"].ToString());

                    goodsrateinfocoll.Add(goodsratesinfo);
                }
                reader.Close();

                return goodsrateinfocoll;
            }

            /// <summary>
            /// �����Ʒ������Ϣ(DTO)
            /// </summary>
            /// <param name="dt">Ҫת�������ݱ�</param>
            /// <returns>������Ʒ������Ϣ</returns>
            public static Goodsrateinfo[] GetGoodsRatesInfoArray(DataTable dt)
            {
                if (dt == null || dt.Rows.Count == 0)
                    return null;

                Goodsrateinfo[] goodsratesinfoarray = new Goodsrateinfo[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    goodsratesinfoarray[i] = new Goodsrateinfo();
                    goodsratesinfoarray[i].Id = Convert.ToInt32(dt.Rows[i]["id"].ToString());
                    goodsratesinfoarray[i].Goodstradelogid = Convert.ToInt32(dt.Rows[i]["goodstradelogid"]);
                    goodsratesinfoarray[i].Message = dt.Rows[i]["message"].ToString();
                    goodsratesinfoarray[i].Explain = dt.Rows[i]["explain"].ToString();
                    goodsratesinfoarray[i].Ip = dt.Rows[i]["ip"].ToString();
                    goodsratesinfoarray[i].Uid = Convert.ToInt32(dt.Rows[i]["uid"].ToString());
                    goodsratesinfoarray[i].Uidtype = Convert.ToInt32(dt.Rows[i]["uidtype"].ToString());
                    goodsratesinfoarray[i].Ratetouid = Convert.ToInt32(dt.Rows[i]["ratetouid"].ToString());
                    goodsratesinfoarray[i].Username = dt.Rows[i]["username"].ToString();
                    goodsratesinfoarray[i].Postdatetime = Convert.ToDateTime(dt.Rows[i]["postdatetime"].ToString());
                    goodsratesinfoarray[i].Goodsid = Convert.ToInt32(dt.Rows[i]["goodsid"].ToString());
                    goodsratesinfoarray[i].Goodstitle = dt.Rows[i]["goodstitle"].ToString();
                    goodsratesinfoarray[i].Price = Convert.ToDecimal(dt.Rows[i]["price"].ToString());
                    goodsratesinfoarray[i].Ratetype = Convert.ToInt32(dt.Rows[i]["ratetype"].ToString());

                }
                dt.Dispose();
                return goodsratesinfoarray;
            }
        }
    }
}
