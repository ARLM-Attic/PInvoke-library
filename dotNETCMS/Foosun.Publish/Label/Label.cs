using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using Foosun.Config;

namespace Foosun.Publish
{
    /// <summary>
    /// 标签参数
    /// </summary>
    public struct LabelParameter
    {
        /// <summary>
        /// 参数名称
        /// </summary>
        public string LPName;
        /// <summary>
        /// 参数值
        /// </summary>
        public string LPValue;
    }
    /// <summary>
    /// 标签分类
    /// </summary>
    public enum LabelType { Free, Custom, Class, Channel }
    /// <summary>
    /// 标签类
    /// </summary>
    public class Label
    {
        /// <summary>
        /// 标签名称
        /// </summary>
        protected string _LabelName = string.Empty;
        /// <summary>
        /// 标签种类
        /// </summary>
        protected LabelType _LabelType;
        /// <summary>
        /// 最终的HTML代码
        /// </summary>
        protected string _FinalHtmlCode = string.Empty;
        /// <summary>
        /// 当前的模板类型
        /// </summary>
        protected TempType _TemplateType;
        /// <summary>
        /// 当前的栏目ID
        /// </summary>
        protected string _CurrentClassID = null;
        /// <summary>
        /// 当前的专题ID
        /// </summary>
        protected string _CurrentSpecialID = null;
        /// <summary>
        /// 当前新闻ID
        /// </summary>
        protected string _CurrentNewsID = null;


        /// <summary>
        /// 当前的频道栏目ID
        /// </summary>
        protected int _CurrentCHClassID;
        /// <summary>
        /// 当前的频道专题ID
        /// </summary>
        protected int _CurrentCHSpecialID;
        /// <summary>
        /// 当前频道新闻ID
        /// </summary>
        protected int _CurrentCHNewsID;

        protected int _CurrentChID;

        //-<-wjl 2008-07-22 评论问题
        /// <summary>
        ///是否允许评论
        /// </summary>
        protected bool _isConten;

        /// <summary>
        ///是否允许评论
        /// </summary>
        public bool IsConten
        {
            set
            {
                this._isConten = value;
            }
            get
            {
                return _isConten;
            }
        }
        //--wjl>

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="labelname">标签名称</param>
        public Label(string labelname,LabelType labeltype)
        {
            _LabelName = labelname;
            _LabelType = labeltype;
        }
        /// <summary>
        /// 标签名称
        /// </summary>
        public string LabelName
        {
            get { return _LabelName; }
        }
        /// <summary>
        /// 解析标签种类并返回标签实例
        /// </summary>
        /// <returns>跟距标签类型返回标签实例</returns>
        public static Label GetLabel(string labelname)
        {
            string _labelname = labelname.ToUpper();
            if (Regex.Match( _labelname,@"\{FS_FREE_[^\}]+\}", RegexOptions.Compiled).Success)
            {
                return new FreeLabel(labelname, LabelType.Free);
            }
            else if (Regex.Match(_labelname, @"\{FS_DYN[^\}]+\}", RegexOptions.Compiled).Success)
            {
                return new DynamicLabel(labelname, LabelType.Class);
            }
            else if (Regex.Match(_labelname, @"\{FS_CH\$\d+_[^\}]+\}", RegexOptions.Compiled).Success)
            {
                string getReplaceStr = _labelname.Replace("{FS_CH$", "");
                int pLnamee = getReplaceStr.IndexOf("_");
                string FileChHTML = getReplaceStr.Substring(0, pLnamee);
                ChannelLabel chlbl = new ChannelLabel(labelname, LabelType.Channel);
                chlbl.CHID = int.Parse(FileChHTML);
                return chlbl;
            }
            else if (Regex.Match(_labelname, @"\{FS_[^\}]+\}", RegexOptions.Compiled).Success)
            {
                return new CustomLabel(labelname, LabelType.Custom);
            }
            else
            {
                throw new Exception("标签名称非法");
            }
        }
        /// <summary>
        /// 当前标签的种类
        /// </summary>
        public LabelType MyType
        {
            get { return _LabelType; }
        }
        /// <summary>
        /// 从数据库取得标签的内容
        /// </summary>
        /// <param name="cn"></param>
        public virtual void GetContentFromDB()
        {
        }
        /// <summary>
        /// 生成最终的HTML代码
        /// </summary>
        /// <param name="cn"></param>
        public virtual void MakeHtmlCode()
        {
        }
        /// <summary>
        /// 最终的标签HTML代码
        /// </summary>
        public string FinalHtmlCode
        {
            get { return _FinalHtmlCode; }
        }
        /// <summary>
        /// 设置或获取当前的模板类型
        /// </summary>
        public TempType TemplateType
        {
            set { _TemplateType = value; }
            get { return _TemplateType; }
        }
        /// <summary>
        /// 当前的栏目ID
        /// </summary>
        public string CurrentClassID
        {
            set { _CurrentClassID = value; }
        }
        /// <summary>
        /// 当前的专题ID
        /// </summary>
        public string CurrentSpecialID
        {
            set { _CurrentSpecialID = value; }
        }
        /// <summary>
        /// 当前新闻ID
        /// </summary>
        public string CurrentNewsID
        {
            set { _CurrentNewsID = value; }
        }


        /// <summary>
        /// 当前的频道栏目ID
        /// </summary>
        public int CurrentChClassID
        {
            set { _CurrentCHClassID = value; }
        }
        /// <summary>
        /// 当前的频道专题ID
        /// </summary>
        public int CurrentCHSpecialID
        {
            set { _CurrentCHSpecialID = value; }
        }
        /// <summary>
        /// 当前频道新闻ID
        /// </summary>
        public int CurrentCHNewsID
        {
            set { _CurrentCHNewsID = value; }
        }
        /// <summary>
        /// 当前频道ID
        /// </summary>
        public int CurrentChID
        {
            set { _CurrentChID = value; }
        }
    }
}
