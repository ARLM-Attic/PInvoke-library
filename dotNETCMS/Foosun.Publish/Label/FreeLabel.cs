using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using Foosun.Config;

namespace Foosun.Publish
{
    /// <summary>
    /// 自由标签
    /// </summary>
    public class FreeLabel : Label
    {
        /// <summary>
        /// 自由标签参数
        /// </summary>
        struct StParam
        {
            /// <summary>
            /// 参数名称
            /// </summary>
            public string name;
            /// <summary>
            /// 所在SQL语句的位置
            /// </summary>
            public int pos;
        }
        /// <summary>
        /// 自由标签的SQL语句
        /// </summary>
        private string LabelSQL = string.Empty;
        /// <summary>
        /// 自由标签样式
        /// </summary>
        private string LabelStyle = string.Empty;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="labelname">自由标签名称</param>
        public FreeLabel(string labelname,LabelType labeltype)
            : base(labelname, labeltype)
        { }
        /// <summary>
        /// 从数据库取得自由标签内容
        /// </summary>
        /// <param name="cn">已打开的数据库连接</param>
        public override void GetContentFromDB()
        {
            IDataReader rd = CommonData.DalPublish.GetFreeLabelContent(_LabelName);
            if (rd.Read())
            {
                LabelSQL = rd.GetString(0);
                if (!rd.IsDBNull(1)) LabelStyle = rd.GetString(1);
            }
            rd.Close();
        }
        /// <summary>
        /// 生成最终的HTML代码
        /// </summary>
        /// <param name="cn"></param>
        public override void MakeHtmlCode()
        {
            if (LabelStyle == string.Empty)
            {
                this._FinalHtmlCode = string.Empty;
                return;
            }
            //try
            //{
                string FinalStr = this.LabelStyle;
                #region 时间处理
                string pattertm = @"\[\$[^\$]+\$\]";
                Regex regex = new Regex(pattertm, RegexOptions.Compiled);
                Match match = regex.Match(FinalStr);
                DateTime Now = DateTime.Now;
                while (match.Success)
                {
                    string tm;
                    string _tm = tm = match.Value;
                    _tm = Regex.Replace(_tm, @"^\[\$|\$\]$", "");
                    _tm = _tm.Replace("YY02", Now.Year.ToString().Remove(0, 2));
                    _tm = _tm.Replace("YY04", Now.Year.ToString());
                    _tm = _tm.Replace("MM", Now.Month.ToString());
                    _tm = _tm.Replace("DD", Now.Day.ToString());
                    _tm = _tm.Replace("HH", Now.Hour.ToString());
                    _tm = _tm.Replace("MI", Now.Minute.ToString());
                    _tm = _tm.Replace("SS", Now.Second.ToString());
                    FinalStr = FinalStr.Replace(tm, _tm);
                    match = regex.Match(FinalStr);
                }
                //[$YY02年MM月DD日$]日期格式:YY02代表2位的年份(如06表示2006年),YY04表示4位数的年份(2006)，MM代表月，DD代表日，HH代表小时，MI代表分，SS代表秒。
                #endregion
                IList<StParam> FieldsList = ParseFields(FinalStr);
                DataTable dt = CommonData.DalPublish.ExecuteSql(LabelSQL);
                //记录不循环记录的编号
                List<int> UnLoopNum = new List<int>();
                #region 先找不循环
                string pattern = @"\{\*(?<n>\d+)(?<c>[\S\s]*?)\*\}";
                Regex reg = new Regex(pattern, RegexOptions.Compiled);
                Match m = reg.Match(FinalStr);
                while (m.Success)
                {
                    string rcd = m.Value;
                    string newrcd = m.Groups["c"].Value.Trim();
                    int rcdindex = Convert.ToInt32(m.Groups["n"].Value);
                    UnLoopNum.Add(rcdindex - 1);
                    if (FieldsList != null)
                    {
                        for (int i = 0; i < FieldsList.Count; i++)
                        {
                            string FieldName = FieldsList[i].name;
                            string FieldValue = string.Empty;
                            if (dt != null && dt.Rows.Count >= rcdindex && dt.Rows[rcdindex - 1][FieldsList[i].pos] != DBNull.Value)
                            {
                                FieldValue = dt.Rows[rcdindex - 1][FieldsList[i].pos].ToString();
                            }
                            newrcd = ReplaceField(newrcd, FieldName, FieldValue);
                            //dt.TableName
                        }
                    }
                    FinalStr = FinalStr.Replace(rcd, newrcd);
                    m = m.NextMatch();
                }
                #endregion 先找不循环
                #region 处理循环
                pattern = @"\{\#[\s\S]*?\#\}";
                Regex r = new Regex(pattern, RegexOptions.Compiled);
                m = r.Match(FinalStr);
                while (m.Success)
                {
                    string mstr = m.Value;
                    string _mstr = mstr.Substring(2, mstr.Length - 4);
                    string reslt = string.Empty;
                    if (dt != null)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (UnLoopNum.Contains(i))
                                continue;
                            UnLoopNum.Add(i);
                            string valstr = _mstr;
                            if (FieldsList != null)
                            {
                                for (int j = 0; j < FieldsList.Count; j++)
                                {
                                    string FieldName = FieldsList[j].name;
                                    string FieldValue = string.Empty;
                                    if (dt.Rows[i][FieldsList[j].pos] != DBNull.Value)
                                    {
                                        FieldValue = dt.Rows[i][FieldsList[j].pos].ToString();
                                    }
                                    valstr = ReplaceField(valstr, FieldName, FieldValue);
                                }
                            }
                            reslt += valstr;
                        }
                    }
                    FinalStr = FinalStr.Replace(mstr, reslt);
                    m = m.NextMatch();
                }
                #endregion 处理循环
                FieldsList.Clear();
                dt.Clear();
                dt.Dispose();
                this._FinalHtmlCode = FinalStr;
            }
            //catch
            //{
            //    this._FinalHtmlCode = string.Empty;
            //}
        
       /// <summary>
       /// 解析SQL语句的字段位置
       /// </summary>
       /// <param name="Input"></param>
       /// <returns></returns>
        private IList<StParam> ParseFields(string Input)
        {
            string SqlFields = Regex.Match(LabelSQL, @"^select\ +(top\ +\d+\ +)?(?<flds>.+)\ +from\ +.+", RegexOptions.Compiled | RegexOptions.IgnoreCase).Groups["flds"].Value.Trim();
            if (SqlFields.Equals(string.Empty))
            {
                return null;
            }
            string[] Fields = null;
            if (SqlFields.IndexOf(",") > 0)
                Fields = SqlFields.Trim().Split(',');
            else
                Fields = new string[] { SqlFields };
            string pattern = @"\[\*(?<fld>[\s\S]+?)\*\]";
            IList<StParam> pms = new List<StParam>();
            Regex reg = new Regex(pattern, RegexOptions.Compiled);
            Match m = reg.Match(Input);
            while (m.Success)
            {
                StParam p;
                p.name = m.Groups["fld"].Value;
                p.pos = -1;
                bool flag = false;
                foreach (StParam sp in pms)
                {
                    if (sp.name.Equals(p.name))
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    for (int i = 0; i < Fields.Length; i++)
                    {
                        if (Fields[i].Trim().Equals(p.name.Trim()))
                        {
                            p.pos = i;
                            pms.Add(p);
                            break;
                        }
                    }
                }
                m = m.NextMatch();
            }
            return pms;
        }
        protected string ReplaceField(string Input,string FieldName,string FieldValue)
        {
            if (Input == null || Input.Trim() == string.Empty)
                return string.Empty;
            string RetVal = Input;
            string pattern = @"\(\#[Ll][Ee][Ff][Tt]\(\[\*" + Regex.Escape(FieldName) + @"\*\]\,(?<n>\d+)\)\#\)";
            Regex r = new Regex(pattern, RegexOptions.Compiled);
            Match mymatch = r.Match(Input);
            if (mymatch.Success)
            {
                int pos = int.Parse(mymatch.Groups["n"].Value);
                FieldValue = Foosun.Common.Input.GetSubString(FieldValue, pos);
                RetVal = Regex.Replace(Input, pattern, FieldValue);
            }
            else
            {

                //pattern = @"fs_news_class";
                //r=new Regex(pattern, RegexOptions.Compiled);
                //mymatch = r.Match(LabelSQL);
                //if (mymatch.Success && (FieldName.ToUpper() == "CLASSID"))
                //{
                //    是栏目链接
                //    string urls=CommonData.getClassURL(FieldValue);
                //    string linkstring = "<a href=\"" + urls + "\">" + FieldName + "</a>";
                //    RetVal = Input.Replace("[*" + FieldName + "*]", urls);
                //}
                //else
                //{
                //    pattern = @"fs_news";
                //    r = new Regex(pattern, RegexOptions.Compiled);
                //    mymatch = r.Match(LabelSQL);
                //    if (mymatch.Success && (FieldName.ToUpper() == "NEWSID"))
                //    {
                //        是新闻链接
                //        string urls = CommonData.getNewsURLFormID(FieldValue, Foosun.Config.DBConfig.CmsConString);
                //        string linkstring = "<a href=\"" + urls + "\">" + FieldName + "</a>";
                //        RetVal = Input.Replace("[*" + FieldName + "*]", urls);
                //    }
                //    else
                //    {
                //        RetVal = Input.Replace("[*" + FieldName + "*]", FieldValue);
                //    }
                //}
                string tabStr = testTable(FieldName);
                if (tabStr == "fs_news_class")
                {
                    string urls = CommonData.getClassURL(FieldValue);
                    string linkstring = "<a href=\"" + urls + "\">" + FieldName + "</a>";
                    RetVal = Input.Replace("[*" + FieldName + "*]", urls);
                }
                else if (tabStr == "fs_news")
                {
                    string urls = CommonData.getNewsURLFormID(FieldValue, Foosun.Config.DBConfig.CmsConString);
                    string linkstring = "<a href=\"" + urls + "\">" + FieldName + "</a>";
                    RetVal = Input.Replace("[*" + FieldName + "*]", urls);
                }
                else
                {
                    RetVal = Input.Replace("[*" + FieldName + "*]", FieldValue);
                }
            }
            return RetVal;
        }

        protected string testTable(string FieldName)
        {
            string returnStr="";
            string pattern = "";
            string fld = FieldName;
            string[] arr = null;
            if (FieldName.IndexOf('.') != -1)
            {
                arr = FieldName.Split('.');
                returnStr=arr[0];
                fld = arr[1];
            }
            else
            {

                pattern = @"fs_news_class";
                Regex r = new Regex(pattern, RegexOptions.Compiled);
                Match mymatch = r.Match(LabelSQL);
                if (mymatch.Success)
                {
                    returnStr = "fs_news_class";
                }
                else
                {
                    pattern = @"fs_news";
                    r = new Regex(pattern, RegexOptions.Compiled);
                    mymatch = r.Match(LabelSQL);
                    if (mymatch.Success)
                    {
                        //是新闻链接
                        returnStr = "fs_news";
                    }
                }
            }
            returnStr = returnStr.ToLower();
            fld = fld.ToLower();
            if (returnStr == "fs_news_class" && fld == "classid")
            {
                returnStr="fs_news_class";
            }
            else if (returnStr == "fs_news" && fld == "newsid")
            {
                returnStr = "fs_news";
            }
            else
            {
                returnStr = "";
            }
           
            return returnStr;
        }
    }
}
