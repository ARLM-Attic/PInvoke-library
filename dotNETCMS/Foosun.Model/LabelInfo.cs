using System;
using System.Collections.Generic;

namespace Foosun.Model
{
    [Serializable]
    public class LabelInfo
    {
        public int Id;
        public string LabelID;
        public string ClassID;
        public string Label_Name;
        public string Label_Content;
        public string Description;
        public DateTime CreatTime;
        public int isBack;
        public int isRecyle;
        public int isSys;
        public string SiteID;
    }
    public class LabelClassInfo
    {
        public int Id;
        public string ClassID;
        public string ClassName;
        public string Content;
        public DateTime CreatTime;
        public int isRecyle;
        public string SiteID;
    }
}
