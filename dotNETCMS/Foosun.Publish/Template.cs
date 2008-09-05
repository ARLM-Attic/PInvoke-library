using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using Foosun.Config;

namespace Foosun.Publish
{
    public enum TempType
    { Index, Class, News, Special,ChIndex,ChClass,ChNews,Chspecial }
    /// <summary>
    /// 模板类
    /// </summary>
    public class Template
    {
        /// <summary>
        /// 模板的物理路径和名称
        /// </summary>
        protected string _temppath;
        /// <summary>
        /// 模板类型
        /// </summary>
        protected TempType _temptype;
        /// <summary>
        /// 模板的文件内容
        /// </summary>
        protected string _tempcontent = string.Empty;
        /// <summary>
        /// 模板的最终的内容
        /// </summary>
        protected string _tempfinallyconent = string.Empty;
        /// <summary>
        /// 栏目ID
        /// </summary>
        protected string _classid = null;
        /// <summary>
        /// 当前的专题ID
        /// </summary>
        protected string _specialid = null;
        /// <summary>
        /// 当前新闻ID
        /// </summary>
        protected string _newsid = null;
        /// <summary>
        /// 频道栏目ID
        /// </summary>
        protected int _chclassid;
        /// <summary>
        /// 当前的频道专题ID
        /// </summary>
        protected int _chspecialid;
        /// <summary>
        /// 当前频道新闻ID
        /// </summary>
        protected int _chnewsid;
        /// <summary>
        /// 当前频ID
        /// </summary>
        protected int _chid;
        /// <summary>
        /// 新闻是否允许评论
        /// </summary>
        protected bool _isContent;
        /// <summary>
        /// 构造函数
        /// </summary> 
        /// <param name="temppath">模板物理路径</param>
        /// <param name="TmptType">模板类型</param>
        public Template(string temppath, TempType temptype)
        {
            _temppath = temppath;
            _temptype = temptype;
        }
        /// <summary>
        /// 设置或获取栏目编号
        /// </summary>
        public string ClassID
        {
            set { _classid = value; }
            get { return _classid; }
        }
        /// <summary>
        /// 设置或获取专题编号
        /// </summary>
        public string SpecialID
        {
            set { _specialid = value; }
            get { return _specialid; }
        }
        /// <summary>
        /// 设置或获取新闻编号
        /// </summary>
        public string NewsID
        {
            set { _newsid = value; }
            get { return _newsid; }
        }
        /// <summary>
        /// (频道)设置或获取栏目编号
        /// </summary>
        public int CHClassID
        {
            set { _chclassid = value; }
            get { return _chclassid; }
        }
        /// <summary>
        /// (频道)设置或获取专题编号
        /// </summary>
        public int CHSpecialID
        {
            set { _chspecialid = value; }
            get { return _chspecialid; }
        }
        /// <summary>
        /// (频道)设置或获取新闻编号
        /// </summary>
        public int CHNewsID
        {
            set { _chnewsid = value; }
            get { return _chnewsid; }
        }

        /// <summary>
        /// (频道)id
        /// </summary>
        public int ChID
        {
            set { _chid = value; }
            get { return _chid; }
        }

        //-<-wjl 2008-07-22 评论问题
        /// <summary>
        /// 新闻是否允许评论
        /// </summary>
        public bool IsContent
        {
            set { _isContent = value; }
            get { return _isContent; }
        }
        //--wjl>

        /// <summary> 
        /// 转换模板所有的标签
        /// </summary>
        public void ReplaceLabels()
        {
            string pattern = @"\{FS_[^\}]+\}";
            _tempfinallyconent = _tempcontent;
            Regex reg = new Regex(pattern, RegexOptions.Compiled);
            Match m = reg.Match(_tempfinallyconent);
            while (m.Success)
            {
                string myLabelName = m.Value;  
                Label theLabel = Label.GetLabel(myLabelName);
                theLabel.CurrentClassID = this._classid;
                theLabel.CurrentSpecialID = this._specialid;
                theLabel.CurrentNewsID = this._newsid;
                theLabel.CurrentChClassID = this._chclassid;
                theLabel.CurrentCHSpecialID = this._chspecialid;
                theLabel.CurrentCHNewsID = this._chnewsid;
                theLabel.CurrentChID = this._chid;
                theLabel.TemplateType = this._temptype;
                //-<-wjl 2008-07-22 评论问题
                theLabel.IsConten = this.IsContent; //是否允许评论
                //--wjl>
                theLabel.GetContentFromDB();
                theLabel.MakeHtmlCode();
                string strFinalHtmlCode = theLabel.FinalHtmlCode;
                _tempfinallyconent = _tempfinallyconent.Replace(myLabelName, strFinalHtmlCode);
                m = reg.Match(_tempfinallyconent);
            }
        }
        /// <summary>
        /// 读取模板的内容到变量
        /// </summary>
        public void GetHTML()
        {
            _tempcontent = General.ReadHtml(_temppath);
        }
        /// <summary>
        /// 获取模板的路径
        /// </summary>
        public string TempFilePath
        {
            get { return _temppath; }
        }
        /// <summary>
        /// 获取模板的内容
        /// </summary>
        public string OriginalContent
        {
            get { return _tempcontent; }
        }
        /// <summary>
        /// 获取模板转换标签后的内容
        /// </summary>
        public string FinallyContent
        {
            get { return _tempfinallyconent; }
            set { _tempfinallyconent = value; }
        }
        /// <summary>
        /// 获取当前模板的类型
        /// </summary>
        public TempType MyTempType
        {
            get { return _temptype; }
        }
        /// <summary>
        /// 如果两个模板的路径相同则返回true
        /// </summary>
        /// <param name="t1">比较的模板</param>
        /// <param name="t2">比较的模板</param>
        /// <returns></returns>
        public static bool operator ==(Template t1, Template t2)
        {
            if (t1.TempFilePath.Equals(t2.TempFilePath))
                return true;
            else
                return false;
        }
        /// <summary>
        /// 如果两个模板的路径不相同则返回true
        /// </summary>
        /// <param name="l1">比较的模板</param>
        /// <param name="l2">比较的模板</param>
        /// <returns></returns>
        public static bool operator !=(Template t1, Template t2)
        {
            if (t1.TempFilePath.Equals(t2.TempFilePath))
                return false;
            else
                return true;
        }
        /// <summary>
        /// 重写方法
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode() * 2;
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }
}