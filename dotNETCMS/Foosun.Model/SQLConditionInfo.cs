using System;
using System.Collections.Generic;
using System.Text;

namespace Foosun.Model
{
    /// <summary>
    /// SQL查询时所用的条件
    /// </summary>
    [Serializable]
    public class SQLConditionInfo
    {
        protected string pname;
        protected object pvalue;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="paramname">参数名称</param>
        /// <param name="paramvalue">参数的值</param>
        public SQLConditionInfo(string paramname, object paramvalue)
        {
            pname = paramname;
            pvalue = paramvalue;
        }
        /// <summary>
        /// 参数名称
        /// </summary>
        public string name
        {
            get { return pname; }
        }
        /// <summary>
        /// 参数的值
        /// </summary>
        public object value
        {
            get { return pvalue; }
        }
    }
}
