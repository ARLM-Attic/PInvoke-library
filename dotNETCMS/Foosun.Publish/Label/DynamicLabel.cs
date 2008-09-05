using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using Foosun.Config;

namespace Foosun.Publish
{
    public class DynamicLabel : Label
    {
        /// <summary>
        /// 标签内容
        /// </summary>
        private string LabelContent = string.Empty;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="labelname">标签名称</param>
        public DynamicLabel(string labelname, LabelType labeltype)
            : base(labelname, labeltype)
        {
            //LblMassList = new List<LabelMass>();
        }
        
        public override void GetContentFromDB()
        {
            string getType = _LabelName;
            string result = "";
            if (_LabelName.Trim() == "{FS_DynClassLD}")
            {
                result = "[FS:Loop,FS:SiteID=0,FS:LabelType=ClassList,FS:ListType=News,FS:PageStyle=0$$30$]·<a href=\"{#URL}\"><a href=\"{#URL}\">{#Title}</a></a>  <span style=\"color:#999999;font-size:10px\">({#Date:Month}-{#Date:Day})</span>[/FS:Loop]";
            }
            else if (_LabelName.Trim() == "{FS_DynClassLDC}")
            {
                result = "[FS:Loop,FS:SiteID=0,FS:LabelType=ClassList,FS:ListType=News,FS:PageStyle=0$$30$,FS:SubNews=true]·<a href=\"{#URL}\">{#Title}</a>  <span style=\"color:#999999;font-size:10px\">({#Date:Month}-{#Date:Day})</span>[/FS:Loop]";
            }
            else if (_LabelName.IndexOf("{FS_DynClassD_") > -1)
            {
                result = "[FS:Loop,FS:SiteID=0,FS:LabelType=List,FS:Number=10,FS:NewsType=Last,FS:ClassID=" + (_LabelName.Replace("{FS_DynClassD_", "")).TrimEnd('}') + "]·<a href=\"{#URL}\">{#Title}</a>  <span style=\"color:#999999;font-size:10px\">({#Date:Month}-{#Date:Day})</span>[/FS:Loop]";
            }

            else if (_LabelName.IndexOf("{FS_DynClassDC_") > -1)
            {
                result = "[FS:Loop,FS:SiteID=0,FS:LabelType=List,FS:Number=10,FS:NewsType=Last,FS:SubNews=true,FS:ClassID=" + (_LabelName.Replace("{FS_DynClassDC_", "")).TrimEnd('}') + "]·<a href=\"{#URL}\">{#Title}</a>  <span style=\"color:#999999;font-size:10px\">({#Date:Month}-{#Date:Day})</span>[/FS:Loop]";
            }

            else if (_LabelName.IndexOf("{FS_DynClassR_") > -1)
            {
                result = "[FS:unLoop,FS:SiteID=0,FS:LabelType=RSS,FS:ClassID=" + (_LabelName.Replace("{FS_DynClassR_", "")).TrimEnd('}') + "][/FS:unLoop]";
            }
            else if (_LabelName.IndexOf("{FS_DynClassC_") > -1)
            {
                result = "[FS:unLoop,FS:SiteID=0,FS:LabelType=ClassNaviRead,FS:ClassID=" + (_LabelName.Replace("{FS_DynClassC_", "")).TrimEnd('}') + ",FS:ClassTitleNumber=30,FS:ClassNaviTitleNumber=150][/FS:unLoop]";
            }
            else if (_LabelName.IndexOf("{FS_DynClassC_") > -1)
            {
                result = "[FS:unLoop,FS:SiteID=0,FS:LabelType=ClassNaviRead,FS:ClassID=" + (_LabelName.Replace("{FS_DynClassC_", "")).TrimEnd('}') + ",FS:ClassTitleNumber=30,FS:ClassNaviTitleNumber=150][/FS:unLoop]";
            }
            else if (_LabelName.Trim() == "{FS_DynSpecialLD}")
            {
                result = "[FS:Loop,FS:SiteID=0,FS:LabelType=ClassList,FS:ListType=Special,FS:PageStyle=0$$30$]·<a href=\"{#URL}\">{#Title}</a>  <span style=\"color:#999999;font-size:10px\">({#Date:Month}-{#Date:Day})</span>[/FS:Loop]";
            }
            else if (_LabelName.Trim() == "{FS_DynSpecialLDC}")
            {
                result = "[FS:Loop,FS:SiteID=0,FS:LabelType=ClassList,FS:ListType=Special,FS:PageStyle=0$$30$,FS:SubNews=true]·<a href=\"{#URL}\">{#Title}</a>  <span style=\"color:#999999;font-size:10px\">({#Date:Month}-{#Date:Day})</span>[/FS:Loop]";
            }
            else if (_LabelName.IndexOf("{FS_DynSpecialD_") > -1)
            {
                result = "[FS:Loop,FS:SiteID=0,FS:LabelType=List,FS:Number=10,FS:NewsType=Special,FS:SpecialID=" + (_LabelName.Replace("{FS_DynSpecialD_", "")).TrimEnd('}') + "]·<a href=\"{#URL}\">{#Title}</a>  <span style=\"color:#999999;font-size:10px\">({#Date:Month}-{#Date:Day})</span>[/FS:Loop]";
            }
            else if (_LabelName.IndexOf("{FS_DynSpecialDC_") > -1)
            {
                result = "[FS:Loop,FS:SiteID=0,FS:LabelType=List,FS:Number=10,FS:NewsType=Special,FS:SubNews=true,FS:SpecialID=" + (_LabelName.Replace("{FS_DynSpecialDC_", "")).TrimEnd('}') + "]·<a href=\"{#URL}\">{#Title}</a>  <span style=\"color:#999999;font-size:10px\">({#Date:Month}-{#Date:Day})</span>[/FS:Loop]";
            }
            else if (_LabelName.IndexOf("{FS_DynSpecialC_") > -1)
            {
                result = "[FS:unLoop,FS:SiteID=0,FS:LabelType=SpeicalNaviRead,FS:SpecialID=" + (_LabelName.Replace("{FS_DynSpecialC_", "")).TrimEnd('}') + ",FS:SpecialTitleNumber=30,FS:SpecialNaviTitleNumber=150][/FS:unLoop]";
            }
            LabelContent = result;
        }
        /// <summary>
        /// 解析自定义标签内容
        /// </summary>
        protected void ParseLabelConetent()
        {
            //_FinalHtmlCode = LabelContent;
            string pattern = @"\[FS:unLoop,[^\]]+\][\s\S]*?\[/FS:unLoop\]|\[FS:Loop,[^\]]+\][\s\S]*?\[/FS:Loop\]";
            Regex reg = new Regex(pattern, RegexOptions.Compiled);
            string content = LabelContent;
            Match m = reg.Match(content);
            while (m.Success)
            {
                string mass = m.Value.Trim();
                LabelMass labelmass = new LabelMass(mass, this._CurrentClassID, this._CurrentSpecialID, this._CurrentNewsID,this._CurrentChID,this._CurrentCHClassID,this._CurrentCHSpecialID,this._CurrentCHNewsID);
                labelmass.TemplateType = this._TemplateType;
                labelmass.ParseContent();
                string s = labelmass.Parse();
                content = content.Replace(mass, s);
                m = reg.Match(content);
            }
            this._FinalHtmlCode = content;
        }
        /// <summary>
        /// 生成最终的HTML代码
        /// </summary>
        /// <param name="cn"></param>
        public override void MakeHtmlCode()
        {
            ParseLabelConetent();
        }

    }
}
