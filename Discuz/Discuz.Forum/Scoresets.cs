using System;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;

using Discuz.Cache;
using Discuz.Config;
using Discuz.Common;
using Discuz.Entity;

namespace Discuz.Forum
{
    public class Scoresets
    {

        private static object lockHelper = new object();

        /// <summary>
        /// ��û��ֲ���
        /// </summary>
        /// <returns>���ֲ���</returns>
        public static DataTable GetScoreSet()
        {
            lock (lockHelper)
            {
                DNTCache cache = DNTCache.GetCacheService();
                DataTable dt = cache.RetrieveSingleObject("/Forum/ScoreSet") as DataTable;

                if (dt != null)
                {
                    return dt;
                }
                else
                {
                    DataSet ds = new DataSet();
                    ds.ReadXml(Utils.GetMapPath(BaseConfigs.GetForumPath + "config/scoreset.config"));
                    string[] path = new string[1];
                    path[0] = Utils.GetMapPath(BaseConfigs.GetForumPath + "config/scoreset.config");
                    cache.AddSingleObject("/Forum/ScoreSet", ds.Tables[0], path);
                    return ds.Tables[0];
                }
            }
        }


        /// <summary>
        /// ��û��ֲ���
        /// </summary>
        /// <returns>���ֲ�������</returns>
        public static UserExtcreditsInfo GetScoreSet(int extcredits)
        {
            UserExtcreditsInfo userextcreditsinfo = new UserExtcreditsInfo();
            string extcreditsname = "extcredits" + extcredits;
            DataTable dt = GetScoreSet();

            if (extcredits > 0)
            {
                userextcreditsinfo.Name = dt.Rows[0][extcreditsname].ToString();
                userextcreditsinfo.Unit = dt.Rows[1][extcreditsname].ToString();
                userextcreditsinfo.Rate = Single.Parse(dt.Rows[2][extcreditsname].ToString());
                userextcreditsinfo.Init = Single.Parse(dt.Rows[3][extcreditsname].ToString());
                userextcreditsinfo.Topic = Single.Parse(dt.Rows[4][extcreditsname].ToString());
                userextcreditsinfo.Reply = Single.Parse(dt.Rows[5][extcreditsname].ToString());
                userextcreditsinfo.Digest = Single.Parse(dt.Rows[6][extcreditsname].ToString());
                userextcreditsinfo.Upload = Single.Parse(dt.Rows[7][extcreditsname].ToString());
                userextcreditsinfo.Download = Single.Parse(dt.Rows[8][extcreditsname].ToString());
                userextcreditsinfo.Pm = Single.Parse(dt.Rows[9][extcreditsname].ToString());
                userextcreditsinfo.Search = Single.Parse(dt.Rows[10][extcreditsname].ToString());
                userextcreditsinfo.Pay = Single.Parse(dt.Rows[11][extcreditsname].ToString());
                userextcreditsinfo.Vote = Single.Parse(dt.Rows[12][extcreditsname].ToString());
            }

            return userextcreditsinfo;
        }

        /// <summary>
        /// ��þ��жһ����ʵĿɽ��׻��ֲ���
        /// </summary>
        /// <returns>�һ����ʵĿɽ��׻��ֲ���</returns>
        public static DataTable GetScorePaySet(int type)
        {
            DNTCache cache = DNTCache.GetCacheService();
            DataTable dt = type==0 ?cache.RetrieveObject("/Forum/ScorePaySet") as DataTable:cache.RetrieveObject("/Forum/ScorePaySet1") as DataTable;
            bool pass = true;
            if (dt != null)
            {
                return dt;
            }
            else
            {
                DataTable dtScoreSet = GetScoreSet();
                DataTable dtScorePaySet = new DataTable();
                dtScorePaySet.Columns.Add("id", Type.GetType("System.Int32"));
                dtScorePaySet.Columns.Add("name", Type.GetType("System.String"));
                dtScorePaySet.Columns.Add("rate", Type.GetType("System.Single"));
                for (int i = 1; i < 9; i++)
                {
                    if (type == 0)
                    {
                        pass = (dtScoreSet.Rows[0]["extcredits" + i.ToString()].ToString().Trim() != "") && (dtScoreSet.Rows[2]["extcredits" + i.ToString()].ToString() != "0");
                    }
                    else
                    {
                        pass = (dtScoreSet.Rows[0]["extcredits" + i.ToString()].ToString().Trim() != "");
   
                    
                    }
                    if (pass)
                    {
                        DataRow dr = dtScorePaySet.NewRow();
                        dr["id"] = i;
                        dr["name"] = dtScoreSet.Rows[0]["extcredits" + i.ToString()].ToString().Trim();
                        dr["rate"] = Utils.StrToFloat(dtScoreSet.Rows[2]["extcredits" + i.ToString()].ToString(), 0);
                        dtScorePaySet.Rows.Add(dr);
                    }
                }
                if (type == 0)
                {
                    cache.AddObject("/Forum/ScorePaySet", dtScorePaySet);
                }
                else
                {
                    cache.AddObject("/Forum/ScorePaySet1", dtScorePaySet);
                }
                return dtScorePaySet;
            }
        }

        /// <summary>
        /// ��ȡ���ֲ���ר�õĻ��ֲ���
        /// </summary>
        /// <returns>�ֲ���ר�õĻ��ֲ���</returns>
        public static DataTable GetRateScoreSet()
        {
            DNTCache cache = DNTCache.GetCacheService();
            DataTable dt = cache.RetrieveObject("/Forum/RateScoreSet") as DataTable;

            if (dt != null)
            {
                return dt;
            }
            else
            {
                DataTable dtScoreSet = GetScoreSet();
                DataTable dtRateScoreSet = new DataTable();
                dtRateScoreSet.Columns.Add("id", Type.GetType("System.Int32"));
                dtRateScoreSet.Columns.Add("name", Type.GetType("System.String"));
                dtRateScoreSet.Columns.Add("rate", Type.GetType("System.Single"));

                for (int i = 1; i < 9; i++)
                {
                    DataRow dr = dtRateScoreSet.NewRow();
                    dr["id"] = i;
                    dr["name"] = dtScoreSet.Rows[0]["extcredits" + i.ToString()].ToString().Trim();
                    dr["rate"] = Utils.StrToFloat(dtScoreSet.Rows[2]["extcredits" + i.ToString()].ToString(), 0);
                    dtRateScoreSet.Rows.Add(dr);
                }
                cache.AddObject("/Forum/RateScoreSet", dtRateScoreSet);
                return dtRateScoreSet;
            }
        }

        /// <summary>
        /// ����ǰ̨����ʹ�õ���չ�ֶε�λ
        /// </summary>
        /// <returns>����ǰ̨����ʹ�õ���չ�ֶε�λ</returns>
        public static string[] GetValidScoreUnit()
        {
            DNTCache cache = DNTCache.GetCacheService();
            string[] scoreunit = cache.RetrieveObject("/Forum/ValidScoreUnit") as string[];

            if (scoreunit != null)
            {
                return scoreunit;
            }
            else
            {
                // Ϊ��ǰ̨ģ���еĿɶ���, scoreunit����ЧԪ��Ҳ��Ӧextcredits1 - 8�ֶ�, score[0]����
                scoreunit = new string[9];
                scoreunit[0] = string.Empty;
                DataTable dt = GetScoreSet();
                for (int i = 1; i < 9; i++)
                {
                    if (dt.Rows[1]["extcredits" + i.ToString()].ToString() != string.Empty)
                        scoreunit[i] = dt.Rows[1]["extcredits" + i.ToString()].ToString();
                    else
                        scoreunit[i] = string.Empty;
                }
                dt.Dispose();
                cache.AddObject("/Forum/ValidScoreUnit", scoreunit);
                return scoreunit;
            }
        }

        /// <summary>
        /// ����ǰ̨����ʹ�õ���չ�ֶ�������ʾ����
        /// </summary>
        /// <returns>ǰ̨����ʹ�õ���չ�ֶ���������ʾ����</returns>
        public static string[] GetValidScoreName()
        {
            DNTCache cache = DNTCache.GetCacheService();
            string[] score = cache.RetrieveObject("/Forum/ValidScoreName") as string[];

            if (score != null)
            {
                return score;
            }
            else
            {
                // Ϊ��ǰ̨ģ���еĿɶ���, score����ЧԪ��Ҳ��Ӧextcredits1 - 8�ֶ�, score[0]����
                score = new string[9];
                score[0] = "";

                DataTable dt = GetScoreSet();
                for (int i = 1; i < 9; i++)
                {
                    if (dt.Rows[0]["extcredits" + i.ToString()].ToString() != "")
                    {
                        score[i] = dt.Rows[0]["extcredits" + i.ToString()].ToString();
                    }
                    else
                    {
                        score[i] = "";
                    }
                }
                dt.Dispose();
                cache.AddObject("/Forum/ValidScoreName", score);
                return score;
            }
        }


        /// <summary>
        /// ��û��ֹ���
        /// </summary>
        /// <returns></returns>
        public static string GetScoreCalFormula()
        {
            return GetScoreCalFormula(BaseConfigs.GetForumPath + @"/config/scoreset.config");
        }

        /// <summary>
        /// ��û��ֹ���
        /// </summary>
        /// <param name="configFilePath">���ֹ���</param>
        /// <returns>���ֹ���</returns>
        public static string GetScoreCalFormula(string configFilePath)
        {
            DNTCache cache = DNTCache.GetCacheService();
            string formula = cache.RetrieveObject("/Forum/Scoreset/Formula") as string;

            if (formula != null)
            {
                return formula;
            }
            else
            {
                DataSet ds = new DataSet();
                ds.ReadXml(Utils.GetMapPath(configFilePath));
                formula = ds.Tables["formula"].Rows[0]["formulacontext"].ToString();
                cache.AddObject("/Forum/Scoreset/Formula", formula);
                return formula;
            }
        }

        /// <summary>
        /// ���ؽ��׻���
        /// </summary>
        /// <returns>���׻���</returns>
        public static int GetCreditsTrans()
        {
            DNTCache cache = DNTCache.GetCacheService();
            string creditstrans = cache.RetrieveObject("/Forum/Scoreset/CreditsTrans") as string;

            if (creditstrans != null)
            {
                return Int16.Parse(creditstrans);
            }
            else
            {
                DataSet ds = new DataSet();
                ds.ReadXml(Utils.GetMapPath(BaseConfigs.GetForumPath + @"/config/scoreset.config"));
                creditstrans = ds.Tables["formula"].Rows[0]["creditstrans"].ToString();
                cache.AddObject("/Forum/Scoreset/CreditsTrans", creditstrans);
                return Int16.Parse(creditstrans);
            }
        }

        /// <summary>
        /// ���ػ��ֽ���˰
        /// </summary>
        /// <returns>���ֽ���˰</returns>
        public static float GetCreditsTax()
        {
            DNTCache cache = DNTCache.GetCacheService();
            string creditstax = cache.RetrieveObject("/Forum/Scoreset/CreditsTax") as string;

            if (creditstax != null)
            {
                return float.Parse(creditstax);
            }
            else
            {
                DataSet ds = new DataSet();
                ds.ReadXml(Utils.GetMapPath(BaseConfigs.GetForumPath + @"/config/scoreset.config"));
                creditstax = ds.Tables["formula"].Rows[0]["creditstax"].ToString();
                cache.AddObject("/Forum/Scoreset/CreditsTax", creditstax);
                return float.Parse(creditstax);
            }
        }

        /// <summary>
        /// ת��������
        /// </summary>
        /// <returns>ת��������</returns>
        public static int GetTransferMinCredits()
        {
            DNTCache cache = DNTCache.GetCacheService();
            string transfermincredits = cache.RetrieveObject("/Forum/Scoreset/TransferMinCredits") as string;

            if (transfermincredits != null)
            {
                return Convert.ToInt16(transfermincredits);
            }
            else
            {
                DataSet ds = new DataSet();
                ds.ReadXml(Utils.GetMapPath(BaseConfigs.GetForumPath + @"/config/scoreset.config"));
                transfermincredits = ds.Tables["formula"].Rows[0]["transfermincredits"].ToString();
                cache.AddObject("/Forum/Scoreset/TransferMinCredits", transfermincredits);
                return Convert.ToInt16(transfermincredits);
            }
        }

        /// <summary>
        /// ���ضһ�������
        /// </summary>
        /// <returns>�һ�������</returns>
        public static int GetExchangeMinCredits()
        {
            DNTCache cache = DNTCache.GetCacheService();
            string exchangemincredits = cache.RetrieveObject("/Forum/Scoreset/ExchangeMinCredits") as string;

            if (exchangemincredits != null)
            {
                return Convert.ToInt16(exchangemincredits);
            }
            else
            {
                DataSet ds = new DataSet();
                ds.ReadXml(Utils.GetMapPath(BaseConfigs.GetForumPath + @"/config/scoreset.config"));
                exchangemincredits = ds.Tables["formula"].Rows[0]["exchangemincredits"].ToString();
                cache.AddObject("/Forum/Scoreset/ExchangeMinCredits", exchangemincredits);
                return Convert.ToInt16(exchangemincredits);
            }
        }

        /// <summary>
        /// �������������
        /// </summary>
        /// <returns></returns>
        public static int GetMaxIncPerTopic()
        {
            DNTCache cache = DNTCache.GetCacheService();
            string maxincperthread = cache.RetrieveObject("/Forum/Scoreset/MaxIncPerThread") as string;

            if (maxincperthread != null)
            {
                return Convert.ToInt16(maxincperthread);
            }
            else
            {
                DataSet ds = new DataSet();
                ds.ReadXml(Utils.GetMapPath(BaseConfigs.GetForumPath + @"/config/scoreset.config"));
                maxincperthread = ds.Tables["formula"].Rows[0]["maxincperthread"].ToString();
                cache.AddObject("/Forum/Scoreset/MaxIncPerThread", maxincperthread);
                return Convert.ToInt16(maxincperthread);
            }
        }


        /// <summary>
        /// ��������߳���ʱ��(Сʱ)
        /// </summary>
        /// <returns></returns>
        public static int GetMaxChargeSpan()
        {
            DNTCache cache = DNTCache.GetCacheService();
            string maxchargespan = cache.RetrieveObject("/Forum/Scoreset/MaxChargeSpan") as string;

            if (maxchargespan != null)
            {
                return Convert.ToInt16(maxchargespan);
            }
            else
            {
                DataSet ds = new DataSet();
                ds.ReadXml(Utils.GetMapPath(BaseConfigs.GetForumPath + @"/config/scoreset.config"));
                maxchargespan = ds.Tables["formula"].Rows[0]["maxchargespan"].ToString();
                cache.AddObject("/Forum/Scoreset/MaxChargeSpan", maxchargespan);
                return Convert.ToInt16(maxchargespan);
            }
        }

        /// <summary>
        /// ȷ�ϵ�ǰʱ���Ƿ���ָ����ʱ���б���
        /// </summary>
        /// <param name="timelist">һ���������ʱ��ε��б�(��ʽΪhh:mm-hh:mm)</param>
        /// <param name="vtime">������������������ĵ�һ����ʱ���</param>
        /// <returns>ʱ��δ����򷵻�true,���򷵻�false</returns>
        public static bool BetweenTime(string timelist, out string vtime)
        {
            if (!timelist.Equals(""))
            {
                string[] enabledvisittime = Utils.SplitString(timelist, "\n");

                if (enabledvisittime.Length > 0)
                {
                    string starttime = "";
                    int s = 0;
                    string endtime = "";
                    int e = 0;
                    foreach (string visittime in enabledvisittime)
                    {
                        if (
                            Regex.IsMatch(visittime,
                                          @"^((([0-1]?[0-9])|(2[0-3])):([0-5]?[0-9])-(([0-1]?[0-9])|(2[0-3])):([0-5]?[0-9]))$"))
                        {
                            starttime = visittime.Substring(0, visittime.IndexOf("-"));
                            s = Utils.StrDateDiffMinutes(starttime, 0);

                            endtime =
                                Utils.CutString(visittime, visittime.IndexOf("-") + 1,
                                                visittime.Length - (visittime.IndexOf("-") + 1));
                            e = Utils.StrDateDiffMinutes(endtime, 0);

                            if (DateTime.Parse(starttime) < DateTime.Parse(endtime)) //��ʼʱ��С�ڽ���ʱ��,��Ϊδ��Խ0��
                            {
                                if (s > 0 && e < 0)
                                {
                                    vtime = visittime;
                                    return true;
                                }
                            }
                            else //��ʼʱ����ڽ���ʱ��,��Ϊ��Խ0��
                            {
                                if ((s < 0 && e < 0) || (s > 0 && e > 0 && e > s))
                                {
                                    vtime = visittime;
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            vtime = "";
            return false;
        }

        /// <summary>
        /// ȷ�ϵ�ǰʱ���Ƿ���ָ����ʱ���б���
        /// </summary>
        /// <param name="timelist">һ���������ʱ��ε��б�(��ʽΪhh:mm-hh:mm)</param>
        /// <returns>ʱ��δ����򷵻�true,���򷵻�false</returns>
        public static bool BetweenTime(string timelist)
        {
            string visittime = "";
            return BetweenTime(timelist, out visittime);
        }
    }
}