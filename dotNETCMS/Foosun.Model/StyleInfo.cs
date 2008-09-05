using System;
using System.Collections.Generic;
using System.Text;

namespace Foosun.Model
{
    [Serializable]
    public class StyleInfo
    {
        public int Id;
        public string styleID;
        public string ClassID;
        public string StyleName;
        public string Content;
        public string Description;
        public DateTime CreatTime;
        public int isRecyle;
        public string SiteID;
    }
    public class StyleClassInfo
    {
        public int Id;
        public string ClassID;
        public string Sname;
        public DateTime CreatTime;
        public string SiteID;
        public int isRecyle;
    }
}
